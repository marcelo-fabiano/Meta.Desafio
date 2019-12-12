using Meta.Desafio.Domain.Entity;
using Meta.Desafio.Domain.Interface;

namespace Meta.Desafio.Infraestructure.Interface
{
    /// <summary>Interface de repositório exclusiva para a entidade de contato</summary>
    public interface IContactRepository : IRepository<Contact>
    {
    }
}