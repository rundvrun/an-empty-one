using Microsoft.AspNetCore.ResponseCompression;
using BlazalR.Server.Model;
using BlazalR.Server.Hubs;
using BlazalR.Server.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddGrpc();
builder.Services.AddResponseCompression(opts =>
{
	opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
		new[] { "application/octet-stream" });
});
builder.Services.AddSingleton<ICounter, InMemoryCounter>();

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// must be added after UseRouting and before UseEndpoints 
app.UseGrpcWeb();

app.MapGrpcService<WeatherService>().EnableGrpcWeb();
app.MapRazorPages();
app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.MapFallbackToFile("index.html");

app.Run();
