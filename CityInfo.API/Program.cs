using CityInfo.API.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.StaticFiles;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Register servives in IOC

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

//builder.Services.AddProblemDetails(options =>
//{
//    options.CustomizeProblemDetails = ctx =>
//    {
//        ctx.ProblemDetails.Extensions.Clear();
//        //ctx.ProblemDetails.Extensions.Remove("traceId");
//        //ctx.ProblemDetails.Extensions.Remove("exception");
//        //ctx.ProblemDetails.Extensions.Remove("path");
//        //ctx.ProblemDetails.Extensions.Remove("endpoint");
//        //ctx.ProblemDetails.Extensions.Remove("routeValues");
//        //ctx.ProblemDetails.Extensions.Remove("headers");

//        ctx.ProblemDetails.Extensions.Add("RequestId", ctx.HttpContext.TraceIdentifier);
//        ctx.ProblemDetails.Extensions.Add("Server", Environment.MachineName);
//        ctx.ProblemDetails.Extensions.Add("Timestamp", DateTime.UtcNow.ToString("o"));
//        ctx.ProblemDetails.Status = StatusCodes.Status400BadRequest;
//        ctx.ProblemDetails.Detail = ctx.Exception?.Message;
//        ctx.ProblemDetails.Title = "Invalid Operation";
//    };
//});

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

//Configure request pipelines

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ProblemDetailsMiddleware>();

//Attribute based routing
app.MapControllers();

app.Run();
