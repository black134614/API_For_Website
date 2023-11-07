using API.Configurations;
using API.Helpers.SignalRConfig;
using API.Helpers.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Setting DBContexts
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// AutoMapper Settings
builder.Services.AddAutoMapperConfiguration();

// Add Authentication
builder.Services.AddAuthenticationConfigufation(builder.Configuration);

// RepositoryAccessor and Service
builder.Services.AddDependencyInjectionConfiguration();

// Swagger Config
builder.Services.AddSwaggerGenConfiguration();

builder.Services.AddSignalR();

// Aspose Config
AsposeUtility.Install();

//Exception Handling Middleware Error
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000",
                "http://tiendungcpc.com").AllowCredentials());
app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<NotificationHub>("/notificationHub");
        // endpoints.MapHub<TicketHub>("/ticketHub");
    });

app.Run();


app.MapControllers();

app.Run();
