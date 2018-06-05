using Abp.Application.Services;

namespace EnadeProject.Interfaces
{
    public interface IService : IApplicationService
    {
        /// <summary>
        ///     Lança uma ExcecaoRegraNegocio caso a entidade não passa na validação das regras de negócio
        /// </summary>
        void BusinessRulesValidation();

        /// <summary>
        ///     Lança exceção caso a entidade não passe pelas validações comuns (tamanho, nulidade, formato e etc)
        /// </summary>
        void EntityValidation();
    }
}