using Meta.Desafio.CRUD.Data;
using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Meta.Desafio.CRUD
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         // resolve as injeções de dependência da aplicação
         ResolveInjection(services);

         services.AddRazorPages();

         services.AddDbContext<Context>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("Context")));
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
         }

         app.UseStaticFiles();
         app.UseRouting();
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapRazorPages();
         });
      }

      /// <summary>Método que configura a injeção de dependências da aplicação</summary>
      /// <param name="services">Serviço que será configurado para o conteiner</param>
      private void ResolveInjection(IServiceCollection services)
      {
         // adiciona ao escopo da aplicação os serviço
         services.AddScoped<IAudienceService, AudienceService>();
         services.AddScoped<IStationService, StationService>();
      }
   }
}
