using CosmenticFormulaApp.Application;
using CosmenticFormulaApp.Infrastructure;
using CosmenticFormulaApp.Web.Services;
using CosmenticFormulaApp.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add application layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add web services
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IUIStateService, UIStateService>();
builder.Services.AddScoped<ISignalRService, SignalRService>();

// Add SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapHub<FormulaUpdateHub>("/formulaUpdateHub");
app.MapFallbackToPage("/_Host");

app.Run();