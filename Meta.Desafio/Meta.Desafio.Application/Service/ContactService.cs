using Meta.Desafio.Application.Interface;
using Meta.Desafio.Application.Service.Global;
using Meta.Desafio.Domain.Entity;
using Meta.Desafio.Infraestructure.Interface;

namespace Meta.Desafio.Application.Service
{
    /// <summary>Classe de serviço exclusiva da entidade de contato</summary>
    public class ContactService : Service<Contact>, IContactService
    {

        /// <summary>Variável local de repositório do serviço</summary>
        private readonly IContactRepository _repositorio;

        /// <summary>Construtor padrão do serviço</summary>
        /// <param name="repository">Repositório instanciado por injeção de dependência</param>
        public ContactService(IContactRepository repository)
           : base(repository)
        {
            // carrega a variável de repositório do serviço
            _repositorio = repository;
        }

        /// <summary>Método que atualiza o registro de contato</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Dados do registro a ser inserido no banco de dados</param>
        /// <returns>True se o registro foi atualizado com sucesso, caso contrário false</returns>
        public bool UpdateContact(string id, Contact registry)
        {
            // carrega o registro atual
            Contact contact = Get(id);

            // atualiza os dados do registro
            contact.nome = registry.nome;
            contact.canal = registry.canal;
            contact.valor = registry.valor;
            contact.obs = registry.obs;

            // realiza a atualização do registro no banco de dados
            return UpdateRegistry(id, contact);
        }

    }
}