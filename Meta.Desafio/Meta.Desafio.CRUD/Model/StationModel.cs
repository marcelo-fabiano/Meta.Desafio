using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meta.Desafio.CRUD.Model
{
   /// <summary>Classe do objeto de emissora</summary>
   public class StationModel
   {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      /// <summary>Identificado da emissora</summary>
      public string Id { get; set; }

      [DisplayName("Nome da Emissora")]
      [Required(AllowEmptyStrings = false, ErrorMessage = "O nome da emissora é obrigatório!")]
      [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Existem caracteres inválidos")]
      /// <summary>Nome da emissora</summary>
      public string Nome { get; set; }
   }
}