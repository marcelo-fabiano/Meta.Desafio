using Meta.Desafio.Domain.Entity;
using Meta.Desafio.Domain.Interface;
using Meta.Desafio.Infraestructure.Interface;
using Meta.Desafio.Infraestructure.Repository.Global;
using Microsoft.Extensions.Configuration;

namespace Meta.Desafio.Infraestructure.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {

        /// <summary>Construtor padrão da classe</summary>
        /// <param name="context">Contexto da aplicação</param>
        public ContactRepository(IContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

    }
}