using Meta.Desafio.Application.Interface;
using Meta.Desafio.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meta.Desafio.WebApi.Controllers
{
    /// <summary>Classe do controller de contatos</summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        /// <summary>Interface de configuracao da aplicação</summary>
        private readonly IConfiguration _configuration;

        /// <summary>Variáveis de serviço utilizadas no controller</summary>
        private readonly IContactService _contactService;

        /// <summary>Construtor padrão da classe</summary>
        public ContactController(IContactService contactService, IConfiguration configuration)
        {
            // carrega as variáveis de serviço por injeção de dependência
            _configuration = configuration;
            _contactService = contactService;
        }

        /// <summary>Retorna um único objeto do tipo Contato</summary>
        /// <param name="idContato">Identificador único de objetos do tipo Contato</param>
        /// <example>api/get?idContato=[Identificador do contato]</example>
        /// <returns>Retorna o <seealso cref="Contact">Contato</seealso> cadastrado no banco de dados com o identificador informado</returns>
        /// <response code="200">Requisição executada com sucesso</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpGet("{idContato}")]
        public async Task<IActionResult> Get(string idContato)
        {
         // carrega o resultado da busca pelo contato
         var result = await _contactService.GetAsync(idContato);

         // se foi encontrado um resultado, então retorna o registro encontrado
         if (result != null) return Ok(result);
              
            // caso contrário retorna que o registro não pode ser encontrado
            return NotFound();
        }

        /// <summary>Retorna uma lista de registros de acordo com o informado nos parâmetros: page e size. Se estes parâmetros não forem passados na consulta, os seguintes valores padrão serão utilizados: page = 0 e size = 10</summary>
        /// <param name="page">Página onde se encontra o subconjunto de registros desejado</param>
        /// <param name="size">Quantidade de registros a ser retornada em uma única página</param>
        /// <example>api/get</example>
        /// <returns>Retorna a lista paginada de <seealso cref="Contact">Contatos</seealso> cadastrados no banco de dados</returns>
        /// <response code="200">Requisição executada com sucesso</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpGet("{page?}/{size?}")]
        public async Task<IActionResult> Get([FromRoute] int page = 0, [FromRoute] int size = 10)
        {
            // carrega o resultado da busca pela lista de contatos
            var result = await _contactService.GetPagedListAsync(page, size);

            // se a lista possuir itens, então retorna o resultado encontrado
            if (result?.Count() > 0) return Ok(result);

            // caso contrário retorna que a lista não pode ser encontrado
            return NotFound();
        }

        /// <summary>Cria um novo objeto do tipo Contato</summary>
        /// <param name="contact">Dados do contato a ser inserido no banco de dados</param>
        /// <example>api/post</example>
        /// <response code="201">Registro inserido com sucesso</response>
        /// <response code="400">Requisição não pode ser executada</response>
        /// <response code="401">Requisição requer autenticação</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            try
            {
                // se os dados de contato não foram preenchidos, retorna um badrequest(400)
                if (contact == null) return BadRequest("Não foi informado o contato a ser inserido.");

                // instancia o registro que será inserido no banco de dados
                var registry = contact.ContactCreate(contact.nome, contact.canal, contact.valor, contact.obs);

                // carrega o resultado da inserção do registro
                var result = await _contactService.InsertRegistryAsync(registry);

                // retorna o resultado da execução do método
                return Created(nameof(ContactController), null);
            }
            catch(ValidationException ex) // se ocorreu erro de validação dos campos
            {
                // retorna o resultado da execução do método
                return BadRequest(ex.Message);
            }
            catch (Exception ex) // se aconteceu erro de execução
            {
                // retorna o resultado da execução do método
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Altera um objeto do tipo Contato</summary>
        /// <param name="contact">Dados do contato a ser atualizado no banco de dados</param>
        /// <example>api/put</example>
        /// <response code="204">Requisição sem conteúdo</response>
        /// <response code="400">Requisição não pode ser executada</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Contact contact)
        {
            try
            {
                // se os dados de contato não foram preenchidos, retorna um badrequest(400)
                if (contact == null) return BadRequest("Não foi informado o contato a ser inserido.");

                // carrega o registro da ser alterado
                var registry = _contactService.Get(contact.id);

                // se o registro não foi encontrado, então retorna status de não encontrado
                if (registry == null) return NotFound($"Registro com id '{contact.id}' não encontrado");

                // atualiza os campos do registro para alteração
                registry.ContactUpdate(contact.nome, contact.canal, contact.valor, contact.obs);

                // carrega o resultado da alteração dos dados do registro
                var result = await _contactService.UpdateRegistryAsync(registry.id, registry);

                // se o resultado foi bem sucedido
                if (result)
                    return NoContent(); // retorna resposta sem conteúdo
                else
                    return BadRequest("Não foi possível atualizar o registro"); // retorna resposta de que não foi possível atualizar
            }
            catch (ValidationException ex) // se ocorreu erro de validação dos campos
            {
                // retorna o resultado da execução do método
                return BadRequest(ex.Message);
            }
            catch (Exception ex) // se aconteceu erro de execução
            {
                // retorna o resultado da execução do método
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Apaga um objeto do tipo Contato</summary>
        /// <param name="idContato">Identificador único de objetos do tipo Contato</param>
        /// <example>api/delete?idContato=[Identificador do contato]</example>
        /// <response code="204">Requisição sem conteúdo</response>
        /// <response code="401">Requisição requer autenticação</response>
        /// <response code="404">Requisição não encontrada</response>
        [HttpDelete("{idContato}")]
        public async Task<IActionResult> Delete(string idContato)
        {
            try
            {
                // carrega o registro da ser alterado
                var registry = _contactService.Get(idContato);

                // se o registro não foi encontrado, então retorna status de não encontrado
                if (registry == null) return NotFound($"Registro com id '{idContato}' não encontrado");

                // carrega o resultado da exclusão do registro
                var result = await _contactService.DeleteRegistryAsync(idContato);

                // se o resultado foi bem sucedido
                if (result)
                    return NoContent(); // retorna resposta sem conteúdo
                else
                    return BadRequest("Não foi possível excluir o registro"); // retorna resposta de que não foi possível excluir
            }
            catch (Exception ex) // se aconteceu erro de execução
            {
                // retorna o resultado da execução do método
                return BadRequest(ex.Message);
            }
        }
    }
}