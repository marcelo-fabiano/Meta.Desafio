using Meta.Desafio.Application.Interface;
using Meta.Desafio.Domain.Entity;
using Meta.Desafio.Domain.Entity.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Meta.Desafio.WebApi.Controllers
{
    /// <summary>Classe do controller de contatos</summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        /// <summary>Interface de configuracao da aplicação</summary>
        private readonly IConfiguration _configuracao;

        /// <summary>Variáveis de serviço utilizadas no controller</summary>
        private readonly IContactService _servicoProduto;

        /// <summary>Construtor padrão da classe</summary>
        public ContactController(IContactService servicoProduto, IConfiguration configuracao)
        {
            // carrega as variáveis de serviço por injeção de dependência
            _configuracao = configuracao;
            _servicoProduto = servicoProduto;
        }

        /// <summary>Retorna um único objeto do tipo Contato</summary>
        /// <param name="idContato">Identificador único de objetos do tipo Contato</param>
        /// <example>api/get?idContato=[Identificador do contato]</example>
        /// <returns>Retorna o <seealso cref="Result{TEntity}">Result</seealso> de lista de <seealso cref="Contact">Contatos</seealso> cadastrados no banco de dados</returns>
        /// <response code="200">Requisição executada com sucesso</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpGet]
        [Route("[action]/{idContato}")]
        public Result<Contact> Get(string idContato)
        {
            return new Result<Contact> { ResultContent = new Contact() };
        }

        /// <summary>Retorna uma lista de registros de acordo com o informado nos parâmetros: page e size. Se estes parâmetros não forem passados na consulta, os seguintes valores padrão serão utilizados: page = 0 e size = 10</summary>
        /// <param name="page">Página onde se encontra o subconjunto de registros desejado</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <example>api/get</example>
        /// <returns>Retorna o <seealso cref="Result{TEntity}">Result</seealso> de lista de <seealso cref="Contact">Contatos</seealso> cadastrados no banco de dados</returns>
        /// <response code="200">Requisição executada com sucesso</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpGet]
        [Route("[action]/{page?}/{size?}")]
        public Result<IEnumerable<Contact>> Get(int page = 0, int size = 10)
        {
            // instancia resulatdo de execução
            Result<IEnumerable<Contact>> result = new Result<IEnumerable<Contact>>();

            try
            {
                // carrega informações de retorno
                result.ResultContent = _servicoProduto.GetList();
                result.Sucess = true;
            }
            catch (Exception ex)
            {
                // carrega informações de falha
                result.Sucess = false;
                result.ErrorList.Add(new ErrorInfo() { Message = ex.Message });
            }

            // retorna resultado da execução
            return result;
        }

        /// <summary>Cria um novo objeto do tipo Contato</summary>
        /// <param name="contact">Dados do contato a ser inserido no banco de dados</param>
        /// <example>api/post</example>
        /// <response code="201">Registro inserido com sucesso</response>
        /// <response code="400">Requisição não pode ser executada</response>
        /// <response code="401">Requisição requer autenticação</response>
        [HttpPost]
        public void Post(Contact contact)
        {
            //return new Result<Contact> { ResultContent = contact };
        }

        /// <summary>Altera um objeto do tipo Contato</summary>
        /// <param name="contact">Dados do contato a ser atualizado no banco de dados</param>
        /// <example>api/put</example>
        /// <response code="204">Requisição sem conteúdo</response>
        /// <response code="400">Requisição não pode ser executada</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpPut]
        public void Put(Contact contact)
        {

        }

        /// <summary>Apaga um objeto do tipo Contato</summary>
        /// <param name="idContato">Identificador único de objetos do tipo Contato</param>
        /// <example>api/delete?idContato=[Identificador do contato]</example>
        /// <response code="204">Requisição sem conteúdo</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpDelete]
        public void Delete(string idContato)
        {

        }
    }
}