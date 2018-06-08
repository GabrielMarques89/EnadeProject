using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace EnadeProject.Commons.Extensions
{

    // ReSharper disable once InconsistentNaming
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        /// <summary>
        /// Filtra a propriedade passada como parâmetro (DateTime - Lambda) de acordo com as datas indicadas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="exp"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereByDateRange<T>(this IQueryable<T> source, Expression<Func<T, DateTime>> exp, DateTime? dateFrom, DateTime? dateTo)
        {
            return source.Where(FilterByDateRange(exp, dateFrom, dateTo));
        }

        /// <summary>
        /// Realizar um contains de uma propriedade de um Iqueryable comparativamente a uma lista.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <param name="source">Iqueryable fonte</param>
        /// <param name="exp">Expressão lambda representando a propriedade de filtro</param>
        /// <param name="comparerList">Lista a ser iterada com o comparador Contains</param>
        /// <returns>Uma expressão </returns>
        /// <example>Esse exemplo mostrar como realizar uma chamada ao método <see cref="Contains{T,TEnumerable}"/>.
        /// <code>
        /// var filtro = new FiltroUsuarioPortalDTO{perfis = new long[] { 15, 20}};
        /// var perfisAsStringWithSeparator = filtro.perfis.Select(x => $", {x.ToString()}, ");
        /// var repo = repositorio.Queryable().Contains(x => x.IdPerfis, perfisAsStringWithSeparator).ToList();
        /// </code>
        /// </example>
        /// <exception cref="ArgumentTypeException">Caso o tipo de dados contido no filtro for numérico. Para esse cenário,
        /// é necessário converter o valor numérico em uma string, definindo os separadores.</exception>
        public static IQueryable<T> Contains<T,TEnumerable>(this IQueryable<T> source, Expression<Func<T, string>> exp, IList<TEnumerable> comparerList)
        {
            return ContainsIterandoEmLista(source, exp, comparerList);
        }

        private static IQueryable<TSource> ContainsIterandoEmLista<TSource,TEnumerable>(this IQueryable<TSource> source, Expression<Func<TSource, string>> exp, IList<TEnumerable> enumerable)
        {
            ThrowIfValueType(enumerable);
            var stringQueryable = enumerable.Select(x => x.ToString()).AsQueryable();

            var lambdasContains = OrLambdaWithList(BuildStringContains(exp, stringQueryable).AsQueryable());
            var lambdasStartsWith = OrLambdaWithList(BuildStringStartsWith(exp, stringQueryable).AsQueryable());
            var lambdasEndsWith = OrLambdaWithList(BuildStringEndsWith(exp, stringQueryable).AsQueryable());

            source = source.Where(OrExpression(OrExpression(lambdasContains, lambdasStartsWith), lambdasEndsWith));

            return source;
        }
        private static void ThrowIfValueType<TEnumerable>(IEnumerable<TEnumerable> enumerable)
        {
            if (typeof(TEnumerable).IsSubclassOf(typeof(ValueType)))
            {
                throw new ArgumentException(
                    "Não é permitido tipos derivados de números. Utilize um método que possibilite definir um separador.",
                    nameof(enumerable));
            }
        }

        private static IQueryable<Expression<Func<T, bool>>> CreateLambdaExpressionWithStringMethod<T>(string method,Expression<Func<T, string>> exp,
            IQueryable<string> stringList)
        {
            var listaLambdas = new List<Expression<Func<T, bool>>>();
            foreach (var stringValue in stringList)
            {
                var comparadorEstatico = Expression.Constant(stringValue, typeof(string));
                var metodo = typeof(string).GetMethod(method, new[] {typeof(string)});
                var call = Expression.Call(exp.Body, metodo, comparadorEstatico);
                listaLambdas.Add(Expression.Lambda<Func<T, bool>>(call, exp.Parameters));
            }
            return listaLambdas.AsQueryable();
        }

        private static IQueryable<Expression<Func<T, bool>>> BuildStringStartsWith<T>(Expression<Func<T, string>> exp, IQueryable<string> stringList)
        {
            return CreateLambdaExpressionWithStringMethod("StartsWith", exp, stringList);
        }

        private static IQueryable<Expression<Func<T, bool>>> BuildStringEndsWith<T>(Expression<Func<T, string>> exp, IQueryable<string> stringList)
        {
            return CreateLambdaExpressionWithStringMethod("EndsWith", exp, stringList);
        }

        private static IQueryable<Expression<Func<T, bool>>> BuildStringContains<T>(Expression<Func<T, string>> exp, IQueryable<string> stringList)
        {
            return CreateLambdaExpressionWithStringMethod("Contains", exp, stringList);
        }

        public static Expression<Func<T, bool>> OrLambdaWithList<T>(IQueryable<Expression<Func<T, bool>>> lista)
        {
            if (!lista.Any())
            {
                throw new ArgumentNullException(nameof(lista));
            }

            return lista.Count() == 1 ? lista.First() : OrExpression(lista.First(), OrLambdaWithList(lista.Skip(1)));
        }
        
        /// <summary>
        /// Lambda combinator
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<A,R> Y<A,R>( Func<Func<A,R>, Func<A,R>> f )
        {
            Func<A,R> g = null;
            g = f( a => g(a) );
            return g;
        }
        
        public static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var path = propertyName.Split('.');

            ParameterExpression param = Expression.Parameter(typeof(T));
            var expressions = new List<MemberExpression>();
            for (int idx = 0; idx < path.Length; idx++)
            {
                Expression objectNativate = idx == 0 ? (Expression) param : expressions[idx - 1];
                expressions.Insert(idx, Expression.Property(objectNativate, path[idx]));
            }

            UnaryExpression unaryExpression = Expression.Convert(expressions[expressions.Count-1], typeof(object));
            return Expression.Lambda<Func<T, object>>(unaryExpression, param);
        }

        private static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                       Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        private static Expression<Func<T, bool>> OrExpression<T>(this Expression<Func<T, bool>> expr1,Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        private static Expression<Func<T, bool>> FilterByDateRange<T>(Expression<Func<T, DateTime>> exp, DateTime? beginDate, DateTime? finalDate)
        {
            var resultadoFinalMaiorQueDataDe = HigherOrEquals(exp, beginDate);
            var resultadoFinalMenorQueDataAte = LowerOrEquals(exp, finalDate);

            Expression<Func<T, bool>> expression;
            FinalFilter(resultadoFinalMaiorQueDataDe, resultadoFinalMenorQueDataAte, out expression);
            return expression;
        }

        private static void FinalFilter<T>(Expression<Func<T, bool>> resultadoFinalMaiorQueDataDe, Expression<Func<T, bool>> resultadoFinalMenorQueDataAte,
            out Expression<Func<T, bool>> expression)
        {
            if (resultadoFinalMaiorQueDataDe != null && resultadoFinalMenorQueDataAte != null)
            {
                expression = And(resultadoFinalMaiorQueDataDe, resultadoFinalMenorQueDataAte);
            }else if (resultadoFinalMaiorQueDataDe != null)
            {
                expression = resultadoFinalMaiorQueDataDe;
            }else if (resultadoFinalMenorQueDataAte != null)
            {
                expression = resultadoFinalMenorQueDataAte;
            }
            else
            {
                expression = x => true;
            }
        }

        private static Expression<Func<T,bool>> LowerOrEquals<T>(Expression<Func<T, DateTime>> lambdaThatRepresentsDateTime, DateTime? finalDate)
        {
            if (!finalDate.HasValue) return null;
            var max = Expression.Constant(finalDate.Value.Date.AddDays(1).AddTicks(-1), typeof(DateTime));
            var menorOuIgualQueDataAte = Expression.LessThanOrEqual(lambdaThatRepresentsDateTime.Body, max);
            return Expression.Lambda<Func<T, bool>>(menorOuIgualQueDataAte, lambdaThatRepresentsDateTime.Parameters);
        }

        private static Expression<Func<T,bool>> HigherOrEquals<T>(Expression<Func<T, DateTime>> exp, DateTime? beginDate)
        {
            if (!beginDate.HasValue) return null;
            var from = Expression.Constant(beginDate.Value.Date, typeof(DateTime));
            var maiorOuIgualQueDataDe = Expression.GreaterThanOrEqual(exp.Body, from);
            return Expression.Lambda<Func<T, bool>>(maiorOuIgualQueDataDe, exp.Parameters);
        }


    }

    /// <summary>
    /// Classe que força a ordenação de strings também em ordem numérica
    /// </summary>
    /// <example> Se a pessoa tem várias strings como (Casa1, Casa2, Casa13, Casa23) a ordenação natural de strings Seria - Casa 1, Casa 11, Casa 2, Casa23. Com 
    /// a classe em escopo, temos a organização como Casa1, Casa2, Casa11, Casa23 </example>
    public class SemiNumericComparer : IComparer<string>
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string x, string y);

        public int Compare(string x, string y)
        {
            return StrCmpLogicalW(x, y);
        }
    }
}
