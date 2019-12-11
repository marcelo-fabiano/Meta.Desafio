using System;
using System.Collections.Generic;

namespace Meta.Desafio.Domain.Entity.Global
{
    /// <summary>Classe de resultado a ser enviado nas respostas da api</summary>
    /// <typeparam name="TEntity">Entidade relacionada ao resultado</typeparam>
    public class Result<TEntity> : IDisposable
    {
        /// <summary>Lista de erros, caso ocorram, devolvida na resposta da api</summary>
        public List<ErrorInfo> ErrorList { get; set; }

        /// <summary>Objeto carregado no resultado da api</summary>
        public TEntity ResultContent { get; set; }

        /// <summary>Flag que determina se o resultado da execução da api foi bem sucedido ou não</summary>
        public bool Sucess { get; set; }

        /// <summary>Construtor padrão da classe</summary>
        public Result()
        {
            // instancia a lista de erros
            this.ErrorList = new List<ErrorInfo>();
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
            if (disposed)
                return;

            if (disposing)
            {
                // Libere quaisquer outros objetos gerenciados aqui. 
            }

            // Libere quaisquer outros objetos não gerenciados aqui. 
            disposed = true;
        }

        #endregion

    }
}