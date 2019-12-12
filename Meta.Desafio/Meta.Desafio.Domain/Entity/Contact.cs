using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Meta.Desafio.Domain.Entity
{
    /// <summary>Entidade de contatos da aplicação</summary>
    public class Contact
    {
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>Identificador único</summary>
        public string id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo nome é obrigatório")]
        /// <summary>Nome que descreva o contato</summary>
        public string nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo canal é obrigatório")]
        /// <summary>Tipo de canal de contato, podendo ser email, celular ou fixo</summary>
        public string canal { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo valor é obrigatório")]
        /// <summary>Valor para o canal de contato</summary>
        public string valor { get; set; }

        /// <summary>Qualquer observação que seja pertinente</summary>
        public string obs { get; set; }

        /// <summary>Método que atualiza os dados do registro para atualização</summary>
        /// <param name="nome">Nome que descreva o contato</param>
        /// <param name="canal">Tipo de canal de contato, podendo ser email, celular ou fixo</param>
        /// <param name="valor">Valor para o canal de contato</param>
        /// <param name="obs">Qualquer observação que seja pertinente</param>
        public void ContactUpdate(string nome, string canal, string valor, string obs = null)
        {
            // atualiza os dados
            this.nome = nome;
            this.canal = canal;
            this.valor = valor;
            this.obs = obs;

            // valida os campos
            this.Validate();
        }

        /// <summary>Método que cria um registro para inserção no banco de dados</summary>
        /// <param name="nome">Nome que descreva o contato</param>
        /// <param name="canal">Tipo de canal de contato, podendo ser email, celular ou fixo</param>
        /// <param name="valor">Valor para o canal de contato</param>
        /// <param name="obs">Qualquer observação que seja pertinente</param>
        public Contact ContactCreate(string nome, string canal, string valor, string obs = null)
        {
            // instancia o registro a ser inserido
            var result = new Contact() { nome = nome, canal = canal, valor = valor, obs = obs };

            // valida os campos
            this.Validate();

            // retorna o registro criado
            return result;
        }

        /// <summary>Método que valida os campos do registro</summary>
        public void Validate()
        {
            // cria o contexto de validação
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);

            // instancia a lista de resultados de validação
            List<ValidationResult> results = new List<ValidationResult>();

            // carrega se a vlidação foi bem sucedida
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            // se existe algum problema na validação
            if (isValid == false)
            {
                // instancia variável que terá os erros de validação
                StringBuilder sbrErrors = new StringBuilder();

                // varre a lista de erros de validação
                foreach (var validationResult in results)
                {
                    // adiciona o erro de validação na variável de retorno
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }

                // dispara uma exceção com a lista de erros de validação
                throw new ValidationException(sbrErrors.ToString());
            }
        }
    }
}