using Meta.Desafio.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meta.Desafio.Application.Service.Global
{
    /// <summary>Classe de serviço global da aplicação</summary>
    /// <typeparam name="TEntity">Entidade relacionada ao serviço</typeparam>
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {

        /// <summary>Repositório a ser utilizado no serviço</summary>
        private readonly IRepository<TEntity> _repository;

        /// <summary>Construtor padrão do serviço</summary>
        /// <param name="repository">Repositório instanciado por injeção de dependência</param>
        public Service(IRepository<TEntity> repository)
        {
            // carrega o repositório
            _repository = repository;
        }

        #region "  Controle de atualização no banco de dados  "

        /// <summary>Método que insere o registro da entidade no banco de dados</summary>
        /// <param name="registry">Registro que será inserida</param>
        /// <returns>Identificador da entidade</returns>
        public TEntity InsertRegistry(TEntity registry)
        {
            // retorna o resultado da inserção
            return _repository.InsertRegistry(registry);
        }

        /// <summary>Método que atualiza os dados do registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Registro que será atualizada</param>
        /// <returns>True caso o registro da entidade tenha sido atualizado com sucesso, caso contrário false</returns>
        public bool UpdateRegistry(string id, TEntity registry)
        {
            // retorna o resultado da atualização
            return _repository.UpdateRegistry(id, registry);
        }

        /// <summary>Método que deleta o registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns>True caso o registro da entidade tenha sido deletado com sucesso, caso contrário false</returns>
        public bool DeleteRegistry(string id)
        {
            // retorna o resultado da exclusão
            return _repository.DeleteRegistry(id);
        }

        #endregion

        #region "  Controles assíncronos de atualização no banco de dados  "

        /// <summary>Método assíncrono que insere o registro da entidade no banco de dados</summary>
        /// <param name="registry">Registro que será inserida</param>
        /// <returns>Identificador da entidade</returns>
        public async Task<TEntity> InsertRegistryAsync(TEntity registry)
        {
            // retorna o resultado da inserção
            return await _repository.InsertRegistryAsync(registry);
        }

        /// <summary>Método assíncrono que atualiza os dados do registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Registro que será atualizada</param>
        /// <returns>True caso o registro da entidade tenha sido atualizado com sucesso, caso contrário false</returns>
        public async Task<bool> UpdateRegistryAsync(string id, TEntity registry)
        {
            // retorna o resultado da atualização
            return await _repository.UpdateRegistryAsync(id, registry);
        }

        /// <summary>Método assíncrono que deleta o registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns>True caso o registro da entidade tenha sido deletado com sucesso, caso contrário false</returns>
        public async Task<bool> DeleteRegistryAsync(string id)
        {
            // retorna o resultado da exclusão
            return await _repository.DeleteRegistryAsync(id);
        }

        #endregion

        #region "  Consultas ao banco de dados  "

        /// <summary>Método que retorna o registro do identificador</summary>
        /// <param name="id">Identificador do registro da entidade</param>
        /// <returns>Caso seja encontrado retorna os dados do registro da entidade, caso contrário nulo</returns>
        public TEntity Get(string id)
        {
            // retorna o resultado da consulta
            return _repository.Get(id);
        }

        /// <summary>Método que retorna uma lista com os registros da entidade</summary>
        /// <returns>Retorna a lista com os registros da entidade</returns>
        public IEnumerable<TEntity> GetList()
        {
            // retorna o resultado da consulta
            return _repository.GetList();
        }

        /// <summary>Método que retorna uma lista paginada dos registros da entidade</summary>
        /// <param name="page">Número da página que será retornada</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <returns>Retorna a lista paginada com os registros da entidade</returns>
        public IEnumerable<TEntity> GetPagedList(int page, int size)
        {
            // retorna o resultado da consulta
            return _repository.GetPagedList(page, size);
        }

        #endregion

        #region "  Consultas assíncronas ao banco de dados  "

        /// <summary>Método assíncrono que retorna o registro do identificador</summary>
        /// <param name="id">Identificador do registro da entidade</param>
        /// <returns>Caso seja encontrado retorna os dados do registro da entidade, caso contrário nulo</returns>
        public async Task<TEntity> GetAsync(string id)
        {
            // retorna o resultado da consulta
            return await _repository.GetAsync(id);
        }

        /// <summary>Método assíncrono que retorna uma lista com os registros da entidade</summary>
        /// <returns>Retorna a lista com os registros da entidade</returns>
        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            // retorna o resultado da consulta
            return await _repository.GetListAsync();
        }

        /// <summary>Método assíncrono que retorna uma lista paginada dos registros da entidade</summary>
        /// <param name="page">Número da página que será retornada</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <returns>Retorna a lista paginada com os registros da entidade</returns>
        public async Task<IEnumerable<TEntity>> GetPagedListAsync(int page, int size)
        {
            // retorna o resultado da consulta
            return await _repository.GetPagedListAsync(page, size);
        }

        #endregion

    }
}