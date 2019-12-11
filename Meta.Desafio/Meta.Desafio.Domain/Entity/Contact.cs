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

        public void ContactUpdate(string nome, string canal, string valor, string obs = null)
        {
            this.nome = nome;
            this.canal = canal;
            this.valor = valor;
            this.obs = obs;

            this.Validate();
        }

        public Contact ContactCreate(string nome, string canal, string valor, string obs = null)
        {
            var result = new Contact() { nome = nome, canal = canal, valor = valor, obs = obs };

            this.Validate();

            return result;
        }

        private void Validate()
        {
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (isValid == false)
            {
                StringBuilder sbrErrors = new StringBuilder();

                foreach (var validationResult in results)
                {
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }

                throw new ValidationException(sbrErrors.ToString());
            }
        }
    }
}