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
        /// <summary>Configura��es da aplica��o</summary>
        public IConfiguration Configuration { get; }

        /// <summary>Construtor padr�o da classe</summary>
        /// <param name="configuration">Conficura��es da aplica��o injetada por depend�ncia</param>
        public Startup(IConfiguration configuration)
        {
            // carrega as configura��o por inje��o de depend�ncia
            Configuration = configuration;
        }

        /// <summary>M�todo utilizado para adicionar os servi�os ao container</summary>
        /// <param name="services">Servi�o que ser� configurado para o conteiner</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // resolve as inje��es de depend�ncia da aplica��o
            ResolveInjecao(services);

            // adiciona os controllers a cole��o de servi�os
            services.AddControllers();

            services.AddSwaggerDocument(document =>
            {
                document.DocumentName = "Meta Desafio";
                document.PostProcess = apiDocument =>
                {
                    apiDocument.Info.Version = "1.0.0.0";
                    apiDocument.Info.Title = "Desafio Meta API";
                    apiDocument.Info.Description = "API para um servi�o de gest�o de contatos";
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

        /// <summary>M�todo que configura o pipeline de solicita��es HTTP</summary>
        /// <param name="app">Contrutor da aplica��o</param>
        /// <param name="env">Ambiente de hospedagem da aplica��o</param>
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

        /// <summary>M�todo que configura a inje��o de depend�ncias da aplica��o</summary>
        /// <param name="services">Servi�o que ser� configurado para o conteiner</param>
        private void ResolveInjecao(IServiceCollection services)
        {
            // adiciona ao escopo da aplica~��o o contexto
            services.AddScoped<IContext, Context>();

            // adiciona ao escopo da aplica��o os servi�o e reposit�rios
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
        }
    }
}
