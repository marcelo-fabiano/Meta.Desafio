using Meta.Desafio.Domain.Entity;
using Meta.Desafio.Domain.Interface;

namespace Meta.Desafio.Application.Interface
{
    /// <summary>Interface exclusiva do serviço de contatos</summary>
    public interface IContactService : IService<Contact>
    {
    }
}