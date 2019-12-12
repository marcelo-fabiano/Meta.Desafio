using Meta.Desafio.Application.Interface;
using Meta.Desafio.Application.Service;
using Meta.Desafio.Domain.Interface;
using Meta.Desafio.Infraestructure;
using Meta.Desafio.Infraestructure.Interface;
using Meta.Desafio.Infraestructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.Generation.Processors;

namespace Meta.Desafio.WebApi
{
    /// <summary></summary>
    public class Startup
    {
        /// <summary>Configurações da aplicação</summary>
        public IConfiguration Configuration { get; }

        /// <summary>Construtor padrão da classe</summary>
        /// <param name="configuration">Conficurações da aplicação injetada por dependência</param>
        public Startup(IConfiguration configuration)
        {
            // carrega as configuração por injeção de dependência
            Configuration = configuration;
        }

        /// <summary>Método utilizado para adicionar os serviços ao container</summary>
        /// <param name="services">Serviço que será configurado para o conteiner</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // resolve as injeções de dependência da aplicação
            ResolveInjecao(services);

            // adiciona os controllers a coleção de serviços
            services.AddControllers();

            services.AddSwaggerDocument(document =>
            {
                document.DocumentName = "Meta Desafio";
                document.PostProcess = apiDocument =>
                {
                    apiDocument.Info.Version = "1.0.0.0";
                    apiDocument.Info.Title = "Desafio Meta API";
                    apiDocument.Info.Description = "API para um serviço de gestão de contatos";
                    apiDocument.Info.TermsOfService = "Nenhum";
                    apiDocument.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Marcelo Fabiano",
                        Email = "marcelo.fabiano@globo.com",
                        Url = "https://github.com/marcelo-fabiano/Meta.Desafio"
                    };
                    apiDocument.Info.License = new NSwag.OpenApiLicense
                    {
                        Name= "MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };
                document.OperationProcessors.Add(new ApiVersionProcessor { IncludedVersions = new[] { "1.0.0.0" } });
            });
        }

        /// <summary>Método que configura o pipeline de solicitações HTTP</summary>
        /// <param name="app">Contrutor da aplicação</param>
        /// <param name="env">Ambiente de hospedagem da aplicação</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>Método que configura a injeção de dependências da aplicação</summary>
        /// <param name="services">Serviço que será configurado para o conteiner</param>
        private void ResolveInjecao(IServiceCollection services)
        {
            // adiciona ao escopo da aplica~ção o contexto
            services.AddScoped<IContext, Context>();

            // adiciona ao escopo da aplicação os serviço e repositórios
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
        }
    }
}
