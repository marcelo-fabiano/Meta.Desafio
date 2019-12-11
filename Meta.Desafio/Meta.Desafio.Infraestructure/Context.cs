using Microsoft.Extensions.Configuration;
using Meta.Desafio.Domain.Interface;
using System;
using MongoDB.Driver;

namespace Meta.Desafio.Infraestructure
{
    /// <summary>Classe de contexto da aplicação</summary>
    public class Context : IContext
    {
        /// <summary>Interface de configuracao da aplicação</summary>
        private readonly IConfiguration _configuration;

        /// <summary>Variável de conexão com o banco de dados</summary>
        private IMongoDatabase _connection;

        /// <summary>Construtor da classe</summary>
        public Context(IConfiguration configuration)
        {
            // carrega as configuração da aplicação
            _configuration = configuration;
        }

        /// <summary>Instância de conexão com o banco de dados da aplicação</summary>
        public IMongoDatabase Connection
        {
            get
            {
                // se a conexão ainda não foi instanciada, então instancia
                if (_connection == null)
                {
                    // configura a conexão com o banco de dados
                    MongoClient clienteConexao = new MongoClient(_configuration.GetConnectionString("ConexaoMongo"));

                    // conecta o banco de dados da aplicação
                    _connection = clienteConexao.GetDatabase("StoneDesafioDB");
                }

                // retorna instância da conexão com o banco de dados
                return _connection;
            }
        }

        #region "  Implementação do IDisposable  "

        // indicador que identifica se o dispose já foi chamado
        bool disposed = false;

        /// <summary>Implementação do 'Dispose' padrão exigível pelo consumidor</summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Implementação protegida do 'Dispose' padrão</summary>
        /// <param name="disposing">Identificador se a classe já se encontra em 'disposing'</param>
        protected virtual void Dispose(bool disposing)
        {
            // se a classe estiver removida de memória, retorna
            if (disposed) return;

            // Libere quaisquer outros objetos não gerenciados aqui. 
            disposed = true;
        }

        #endregion

    }
}