using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Global;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.Extensions.Configuration;

namespace Meta.Desafio.CRUD.Service
{
   public class StationService : Service<StationModel>, IStationService
   {
      public StationService(IConfiguration configuration) : base(configuration) 
      {
      }
   }
}