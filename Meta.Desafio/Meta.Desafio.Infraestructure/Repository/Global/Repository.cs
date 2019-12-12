using Meta.Desafio.Domain.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meta.Desafio.Infraestructure.Repository.Global
{
    /// <summary>Classe de repositório padrão da aplicação</summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {

        /// <summary>Interface de configuracao da aplicação</summary>
        private readonly IConfiguration Configuration;

        /// <summary>Contexto global da aplicação</summary>
        public IContext Context { get; private set; }

        /// <summary>Nome da coleção de armazenamento do banco de dados</summary>
        public string CollectionName { get; protected set; }

        /// <summary>Construtor padrão da classe</summary>
        /// <param name="context">Contexto da aplicação</param>
        public Repository(IContext context, IConfiguration configuration)
        {
            // carrega o contexto da aplicação
            Context = context;
            Configuration = configuration;

            // carrega as proprieddaes utilizadas na classe
            CollectionName = typeof(TEntity).Name;
        }

        #region "  Controles de atualização no banco de dados  "

        /// <summary>Método que insere o registro da entidade no banco de dados</summary>
        /// <param name="registry">Registro que será inserida</param>
        /// <returns>Identificador da entidade</returns>
        public TEntity InsertRegistry(TEntity registry)
        {
            // executa o processo de inserção do registro no banco de dados
            Context.Connection.GetCollection<TEntity>(CollectionName).InsertOne(registry);

            // retorna registro com dados atualizados
            return registry;
        }

        /// <summary>Método que atualiza os dados do registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Registro que será atualizada</param>
        /// <returns>True caso o registro da entidade tenha sido atualizado com sucesso, caso contrário false</returns>
        public bool UpdateRegistry(string id, TEntity registry)
        {
            // cria o filtro de pesquisa para o objeto
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("Id", ObjectId.Parse(id));

            // carrega as opções de alteração
            var replaceOptions = new ReplaceOptions { IsUpsert = true };

            // executa o processo de atualização do registro no banco de dados
            var result = Context.Connection.GetCollection<TEntity>(CollectionName).ReplaceOne(filter, registry, replaceOptions);

            // retorna o resultado de execução
            return result.IsAcknowledged && (result.ModifiedCount > 0);
        }

        /// <summary>Método que deleta o registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns>True caso o registro da entidade tenha sido deletado com sucesso, caso contrário false</returns>
        public bool DeleteRegistry(string id)
        {
            // cria o filtro de pesquisa para o objeto
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("Id", ObjectId.Parse(id));

            // executa o processo de atualização do registro no banco de dados
            DeleteResult result = Context.Connection.GetCollection<TEntity>(CollectionName).DeleteOne(filter);

            // retorna o resultado de execução
            return result.IsAcknowledged && (result.DeletedCount > 0);
        }

        #endregion

        #region "  Controles assíncronos de atualização no banco de dados  "

        /// <summary>Método assíncrono que insere o registro da entidade no banco de dados</summary>
        /// <param name="registry">Registro que será inserida</param>
        /// <returns>Identificador da entidade</returns>
        public async Task<TEntity> InsertRegistryAsync(TEntity registry)
        {
            // executa o processo de inserção do registro no banco de dados
            await Context.Connection.GetCollection<TEntity>(CollectionName).InsertOneAsync(registry);

            // retorna registro com dados atualizados
            return registry;
        }

        /// <summary>Método assíncrono que atualiza os dados do registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Registro que será atualizada</param>
        /// <returns>True caso o registro da entidade tenha sido atualizado com sucesso, caso contrário false</returns>
        public async Task<bool> UpdateRegistryAsync(string id, TEntity registry)
        {
            // cria o filtro de pesquisa para o objeto
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("Id", ObjectId.Parse(id));

            // carrega as opções de alteração
            var replaceOptions = new ReplaceOptions { IsUpsert = true };
            
            // executa o processo de atualização do registro no banco de dados
            ReplaceOneResult result = await Context.Connection.GetCollection<TEntity>(CollectionName).ReplaceOneAsync(filter, registry, replaceOptions);

            // retorna o resultado de execução
            return result.IsAcknowledged && (result.ModifiedCount > 0);
        }

        /// <summary>Método assíncrono que deleta o registro da entidade no banco de dados</summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns>True caso o registro da entidade tenha sido deletado com sucesso, caso contrário false</returns>
        public async Task<bool> DeleteRegistryAsync(string id)
        {
            // cria o filtro de pesquisa para o objeto
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("Id", ObjectId.Parse(id));

            // executa o processo de atualização do registro no banco de dados
            DeleteResult result = await Context.Connection.GetCollection<TEntity>(CollectionName).DeleteOneAsync(filter);

            // retorna o resultado de execução
            return result.IsAcknowledged && (result.DeletedCount > 0);
        }

        #endregion

        #region "  Consultas ao banco de dados  "

        /// <summary>Método que retorna o registro do identificador</summary>
        /// <param name="id">Identificador do registro da entidade</param>
        /// <returns>Caso seja encontrado retorna os dados do registro da entidade, caso contrário nulo</returns>
        public TEntity Get(string id)
        {
            // cria o filtro de pesquisa para o objeto
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("Id", ObjectId.Parse(id));

            // retorna o registro que corresponde ao identificador, caso contrário nulo
            return Context.Connection.GetCollection<TEntity>(CollectionName).Find(filter).FirstOrDefault();
        }

        /// <summary>Método que retorna uma lista com os registros da entidade</summary>
        /// <returns>Retorna a lista com os registros da entidade</returns>
        public IEnumerable<TEntity> GetList()
        {
            // retorna a lista de registros da entidade
            return Context.Connection.GetCollection<TEntity>(CollectionName).Find(_ => true).ToList();
        }

        /// <summary>Método que retorna uma lista paginada dos registros da entidade</summary>
        /// <param name="page">Número da página que será retornada</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <returns>Retorna a lista paginada com os registros da entidade</returns>
        public IEnumerable<TEntity> GetPagedList(int page, int size)
        {
            // retorna a lista de registros da entidade
            return Context.Connection.GetCollection<TEntity>(CollectionName)
                     .Find(_ => true)
                     .Skip((page) * size)
                     .Limit(size)
                     .ToList();
        }

        #endregion

        #region "  Consultas ao banco de dados assíncronos  "

        /// <summary>Método assíncrono que retorna o registro do identificador</summary>
        /// <param name="id">Identificador do registro da entidade</param>
        /// <returns>Caso seja encontrado retorna os dados do registro da entidade, caso contrário nulo</returns>
        public async Task<TEntity> GetAsync(string id)
        {
            // cria o filtro de pesquisa para o objeto
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("Id", ObjectId.Parse(id));

            // carrega o resultado da consulta
            var result = await Context.Connection.GetCollection<TEntity>(CollectionName).FindAsync(filter);

            // retorna o registro que corresponde ao identificador, caso contrário nulo
            return result.FirstOrDefault();
        }

        /// <summary>Método assíncrono que retorna uma lista com os registros da entidade</summary>
        /// <returns>Retorna a lista com os registros da entidade</returns>
        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            // carrega o resultado da consulta
            var result = await Context.Connection.GetCollection<TEntity>(CollectionName).FindAsync(_ => true);

            // retorna a lista de registros da entidade
            return result.ToList();
        }

        /// <summary>Método assíncrono que retorna uma lista paginada dos registros da entidade</summary>
        /// <param name="page">Número da página que será retornada</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <returns>Retorna a lista paginada com os registros da entidade</returns>
        public async Task<IEnumerable<TEntity>> GetPagedListAsync(int page, int size)
        {
            // carrega o resultado da consulta
            var result = await Context.Connection.GetCollection<TEntity>(CollectionName)
                     .Find(_ => true).Skip((page) * size).Limit(size).ToListAsync();

            // retorna a lista de registros da entidade
            return result;
        }

        #endregion

        #region "  Implementação do IDisposable  "

        // indicador que identifica se o dispose já foi chamado
        bool disposed = false;

        /// <summary>Implementação do 'Dispose' padrão exigível pelo consumidor</summary>
        void IDisposable.Dispose()
        {
            // executa a remoção de memória da classe
            Dispose(true);

            // requisita que o CLR não chame o finalizador para a classe
            GC.SuppressFinalize(this);
        }

        /// <summary>Implementação protegida do 'Dispose' padrão</summary>
        /// <param name="disposing">Identificador se a classe já se encontra em 'disposing'</param>
        protected virtual void Dispose(bool disposing)
        {
            // se o contexto estiver instanciado, remove de memória
            if (Context != null) Context.Dispose();

            // se estiver marcado como removido de memória, retorna
            if (disposed) return;

            // libera quaisquer outros objetos não gerenciados aqui. 
            disposed = true;
        }

        #endregion

    }
}