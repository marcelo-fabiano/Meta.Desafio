using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meta.Desafio.CRUD.Model
{
   /// <summary>Classe do objeto de audiência</summary>
   public class AudienceModel
   {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      /// <summary>Identificado da audiência</summary>
      public string Id { get; set; }

      [DisplayName("Pontos")]
      [Required(ErrorMessage = "Os pontos de audiência são obrigatórios")]
      [Range(1, int.MaxValue, ErrorMessage = "Pontos de audiência de ser maior que zero")]
      /// <summary>Número de pontos registrados na audiência</summary>
      public int Pontos_Audiencia { get; set; }

      [Required(ErrorMessage = "A data da audiência é obrigatória")]
      [DisplayName("Data")]
      /// <summary>Data e hora do registro de audiência</summary>
      public DateTime Data_Hora_Audiencia { get; set; }

      [DisplayName("Emissora")]
      [Required(AllowEmptyStrings = false, ErrorMessage = "Selecione a emissora.")]
      /// <summary>Emissora do registro de audiência</summary>
      public string Emissora_Audiencia { get; set; }

      [BsonIgnore]
      [DisplayName("Nome Emissora")]
      public string Emissora_Nome { get; set; }

      [BsonIgnore]
      public IList<StationModel> Lista_Emissoras { get; set; }
   }
}