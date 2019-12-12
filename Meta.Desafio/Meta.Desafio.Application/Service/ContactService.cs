using Meta.Desafio.Application.Interface;
using Meta.Desafio.Application.Service.Global;
using Meta.Desafio.Domain.Entity;
using Meta.Desafio.Infraestructure.Interface;

namespace Meta.Desafio.Application.Service
{
    /// <summary>Classe de serviço exclusiva da entidade de contato</summary>
    public class ContactService : Service<Contact>, IContactService
    {

        /// <summary>Construtor padrão do serviço</summary>
        /// <param name="repository">Repositório instanciado por injeção de dependência</param>
        public ContactService(IContactRepository repository)
           : base(repository)
        {
        }

    }
}