using CodeSensei.Bots.Interfaces;
using CodeSensei.Data.Contexts;
using CodeSensei.Services;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Bot.Builder;
using CodeSenseiChatbot.Adapters;
using CodeSensei.Data.Repositories.Implementations;
using CodeSensei.Data.Repositories.Interfaces;
using CodeSensei.Data.Models;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();
        services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();
        services.AddSingleton<IFeedbackManager, FeedbackManager>();
        services.AddTransient<IBot, CodeSensei.Bots.Utilities.CodeSenseiChatbot>();
        services.AddDbContext<FeedbackContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("FeedbackDatabase")));
        services.AddDbContext<FeedbackContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("FeedbackDatabase")));
        services.AddScoped<IRepository<FeedbackRecord>, FeedbackRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
