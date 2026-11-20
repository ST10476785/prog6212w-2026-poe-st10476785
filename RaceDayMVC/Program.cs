using RaceDayMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient<ActivityInterface, ActivityService>(client => //Setup for thge activity service to make HTTP requests to the API using the HttpClient class, a url should be provided in the appsettings.json file for the BaseUrl key in the ApiSettings section
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
});

builder.Services.AddHttpClient<AuthenticationInterface, Authenticationcs>(client => // Setup for the authentication service to make HTTP requests to the API using the HttpClient class, a url should be provided in the appsettings json file for the BaseUrl key in the ApiSettings section
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
});

builder.Services.AddHttpClient<CategoryServices>(client => // setup for the category service to make HTTP requests to the API using the HttpClient class
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
});

builder.Services.AddHttpClient<EventEnrollmentService>(client => // setup for the event enrollment service to make HTTP requests to the API using the HttpClient class
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
});



builder.Services.AddHttpClient<ResultServices>(client => // setup for the result service to make HTTP requests to the API using the HttpClient class
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();