using MongoDB.Driver;
using System;

namespace Meta.Desafio.Domain.Interface
{
    /// <summary>Interface de contexto da aplicação</summary>
    public interface IContext : IDisposable
    {

        /// <summary>Instância de conexão com o banco de dados da aplicação</summary>
        IMongoDatabase Connection { get; }

    }
}