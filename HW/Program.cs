using System.Diagnostics;
using System.Runtime.CompilerServices;
using HW.Models;
using HW.Models.MailKit;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.WriteIndented = true; });

builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("SmtpConfig"));
builder.Services.AddSingleton<MELProtocolLogger>();
builder.Services.AddScoped<IEmailSender, MailKitSmtpEmailSender>();
builder.Services.AddHostedService<ProductAddedEventHandler>();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IEmailSender,MailKitSmtpEmailSender>();
 
var app = builder.Build();

app.MapControllers();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Catalog/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//Задание 9
app.Use(async (HttpContext context, Func<Task> next) =>
{
    var userAgent = context.Request.Headers.UserAgent.ToString();
    if (!userAgent.Contains("Edg"))
    {
        context.Response.ContentType = "text/plain; charset=UTF-8";
        await context.Response.WriteAsync("Ваш браузер не поддерживается");
        return;
    }
});

app.Use(async (context, next) =>
{
    IHeaderDictionary headers = context.Request.Headers;
    if (!headers.AcceptLanguage.ToString().Contains("ru"))
    {
        await context.Response.WriteAsync(
                      "This language is not supported yet.");
        return; //прерываем выполнение конвейера
    }
    await next();
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Categories}/{id?}");

app.Run();

