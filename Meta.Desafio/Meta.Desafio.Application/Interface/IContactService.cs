using Meta.Desafio.Domain.Entity;
using Meta.Desafio.Domain.Interface;

namespace Meta.Desafio.Application.Interface
{
    /// <summary>Interface exclusiva do serviço de contatos</summary>
    public interface IContactService : IService<Contact>
    {
        /// <summary>Método que atualiza o registro de contato</summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="registry">Dados do registro a ser atualizado no banco de dados</param>
        /// <returns>True se o registro foi atualizado com sucesso, caso contrário false</returns>
        bool UpdateContact(string id, Contact registry);

    }
}