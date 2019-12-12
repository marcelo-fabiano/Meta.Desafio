using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meta.Desafio.Domain.Interface
{
    /// <summary>Interface de repositório da aplicação</summary>
    /// <typeparam name="TEntity">Entidade relacionada ao repositório</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        #region "  Controles de atualização no banco de dados  "

        /// <summary>Método que insere o registro da entidade no banco de dados</summary>
        /// <param name="registry">Registro que será inserida</param>
        /// <returns>Identificador da entidade</returns>
        TEntity InsertRegistry(TEntity registry);

        /// <summary>Método que atualiza os dados do registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Registro que será atualizada</param>
        /// <returns>True caso o registro da entidade tenha sido atualizado com sucesso, caso contrário false</returns>
        bool UpdateRegistry(string id, TEntity registry);

        /// <summary>Método que deleta o registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns>True caso o registro da entidade tenha sido deletado com sucesso, caso contrário false</returns>
        bool DeleteRegistry(string id);

        #endregion

        #region "  Controles assíncronos de atualização no banco de dados  "

        /// <summary>Método assíncrono que insere o registro da entidade no banco de dados</summary>
        /// <param name="registry">Registro que será inserida</param>
        /// <returns>Identificador da entidade</returns>
        Task<TEntity> InsertRegistryAsync(TEntity registry);

        /// <summary>Método assíncrono que atualiza os dados do registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Registro que será atualizada</param>
        /// <returns>True caso o registro da entidade tenha sido atualizado com sucesso, caso contrário false</returns>
        Task<bool> UpdateRegistryAsync(string id, TEntity registry);

        /// <summary>Método assíncrono que deleta o registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns>True caso o registro da entidade tenha sido deletado com sucesso, caso contrário false</returns>
        Task<bool> DeleteRegistryAsync(string id);

        #endregion

        #region "  Consultas ao banco de dados  "

        /// <summary>Método que retorna o registro do identificador</summary>
        /// <param name="id">Identificador do registro da entidade</param>
        /// <returns>Caso seja encontrado retorna os dados do registro da entidade, caso contrário nulo</returns>
        TEntity Get(string id);

        /// <summary>Método que retorna uma lista com os registros da entidade</summary>
        /// <returns>Retorna a lista com os registros da entidade</returns>
        IEnumerable<TEntity> GetList();

        /// <summary>Método que retorna uma lista paginada dos registros da entidade</summary>
        /// <param name="page">Número da página que será retornada</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <returns>Retorna a lista paginada com os registros da entidade</returns>
        IEnumerable<TEntity> GetPagedList(int page, int size);

        #endregion

        #region "  Consultas ao banco de dados assíncronos  "

        /// <summary>Método assíncrono que retorna o registro do identificador</summary>
        /// <param name="id">Identificador do registro da entidade</param>
        /// <returns>Caso seja encontrado retorna os dados do registro da entidade, caso contrário nulo</returns>
        Task<TEntity> GetAsync(string id);

        /// <summary>Método assíncrono que retorna uma lista com os registros da entidade</summary>
        /// <returns>Retorna a lista com os registros da entidade</returns>
        Task<IEnumerable<TEntity>> GetListAsync();

        /// <summary>Método assíncrono que retorna uma lista paginada dos registros da entidade</summary>
        /// <param name="page">Número da página que será retornada</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <returns>Retorna a lista paginada com os registros da entidade</returns>
        Task<IEnumerable<TEntity>> GetPagedListAsync(int page, int size);

        #endregion

    }
}