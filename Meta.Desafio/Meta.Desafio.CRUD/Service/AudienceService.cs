using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Global;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.Extensions.Configuration;

namespace Meta.Desafio.CRUD.Service
{
   public class AudienceService : Service<AudienceModel>, IAudienceService
   {
      public AudienceService(IConfiguration configuration) : base(configuration)
      {
      }
   }
}