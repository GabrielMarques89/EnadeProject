using System;
using System.Linq.Expressions;
using System.Reflection;
using EnadeProject.Model.Filter.Support;
using EnadeProject.NHibernate.EntityMappings.FrameWork_Entities;

namespace EnadeProject.Common.Helpers
{
    public class ExpressionHelper
    {
        /// <summary>
        ///     Com um <see cref="PropertyInfo" /> e um valor, cria um lambda que representa a opera
        /// </summary>
        /// <param name="property"></param>
        /// <param name="convertedValue"></param>
        /// <param name="criterio"></param>
        /// <returns></returns>
        public Expression<Func<T, bool>> GenerateLambdaOperationExpression<T>(PropertyInfo property, object convertedValue,
            Criterio criterio) where T : EntidadeBase
        {
            //TODO: Implementar diferentes operacoes
            var item = Expression.Parameter(typeof(object), "item");
            var memberExpression = Expression.Property(Expression.Convert(item, typeof(T)), property.Name);

            var method = GetApplyableMethod<T>(property, criterio);
            var searchExpression = Expression.Constant(convertedValue, property.PropertyType);
            var methodExpression = Expression.Call(memberExpression, method, searchExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(methodExpression, item);
            return lambda;
        }

        private MethodInfo GetApplyableMethod<T>(PropertyInfo property, Criterio criterio)
        {
            MethodInfo method = null;
            var tipo = property.PropertyType;
            var isStringType = tipo == typeof(string);
            switch (criterio)
            {
                case Criterio.Igual:
                    
                    method = tipo.GetMethod("Equals", new[] {tipo});
                    break;
                case Criterio.Contem:
                    if (isStringType){
                        method = tipo.GetMethod("Contains", new []{property.PropertyType});
                    }
                    break;
                case Criterio.IniciaCom:
                    if (isStringType)
                    {
                        method = tipo.GetMethod("StartsWith", new[] {property.PropertyType});
                    }
                    break;
                case Criterio.TerminaCom:
                    if (isStringType)
                    {
                        method = tipo.GetMethod("EndsWith", new[] {property.PropertyType});
                    }
                    break;
            }

            if (method == null)
            {
                throw new ArgumentException(
                    $"O operador {criterio.ToString()} é inválido. O Método não foi encontrado na reflexão da classe {typeof(T).Name}.");
            }

            return method;
        }
    }
}
