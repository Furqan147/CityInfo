var builder = WebApplication.CreateBuilder(args);

// Register servives in IOC

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Configure request pipelines

//app.MapGet("/", () => "Hello World!");

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello World....");
});

app.Run();
