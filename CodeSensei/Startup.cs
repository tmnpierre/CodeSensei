﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Bot.Builder;
using CodeSenseiChatbot.Adapters;
using CodeSensei.Bots.Interfaces;
using CodeSensei.Bots.Utilities;
using CodeSensei.Data.Contexts;
using CodeSensei.Data.Implementations;
using CodeSensei.Data.Repositories.Interfaces;
using CodeSensei.Services;
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
        services.AddSingleton<IFeedbackService, FeedbackService>();
        services.AddTransient<IBot, CodeSensei.Bots.Utilities.CodeSenseiChatbot>();
        services.AddScoped<IRepository<FeedbackRecord>, FeedbackRepository>();
        services.AddDbContext<FeedbackContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("FeedbackDatabase")));
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
