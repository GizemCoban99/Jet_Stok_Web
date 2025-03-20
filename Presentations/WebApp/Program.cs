using AutoMapper;
using BusinessLayer.Services;
using BusinessLayer.Services.Marketplaces;
using EntityLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

builder.Services.AddSession(option =>
{
    //Süre 1 dk olarak belirlendi
    option.IdleTimeout = TimeSpan.FromMinutes(30);
});

//test

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

#region Services

// Add services to the container.

builder.Services.AddSingleton<UserService, UserService>();
builder.Services.AddSingleton<PermissionService, PermissionService>();
builder.Services.AddSingleton<RoleService, RoleService>();
builder.Services.AddSingleton<GenericTypeService, GenericTypeService>();
builder.Services.AddSingleton<GenericValueService, GenericValueService>();
builder.Services.AddSingleton<MarketplacesService, MarketplacesService>();
builder.Services.AddSingleton<MarketplaceCommissionRateService, MarketplaceCommissionRateService>();
builder.Services.AddSingleton<MarketplaceOperationRateService, MarketplaceOperationRateService>();
#endregion



//authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
{
    o.Cookie.Name = "IJAS13T23O45K9";
    o.SlidingExpiration = true;
    o.ExpireTimeSpan = TimeSpan.FromDays(1);
});


#region Route Values

//Admin
var routePages = new Dictionary<string, string>();
routePages.Add("/Admin/Login", "PomeloAdmin/login");

routePages.Add("/Admin/AccessDenied", "PomeloAdmin/accessdenied");

routePages.Add("/Admin/Dashboard", "PomeloAdmin/dashboard");


routePages.Add("/Admin/User/Users", "PomeloAdmin/users");
routePages.Add("/Admin/User/UserAddOrUpdate", "PomeloAdmin/user-add-or-update/{id?}");

routePages.Add("/Admin/Role/Roles", "PomeloAdmin/roles");
routePages.Add("/Admin/Role/RoleAddOrUpdate", "PomeloAdmin/role-add-or-update/{id?}");

routePages.Add("/Admin/GenericType/GenericTypes", "PomeloAdmin/generic-types");
routePages.Add("/Admin/GenericType/GenericTypeAddOrUpdate", "PomeloAdmin/generic-type-add-or-update/{id?}");

routePages.Add("/Admin/GenericValue/GenericValues", "PomeloAdmin/generic-values/{id}");
routePages.Add("/Admin/GenericValue/GenericValueAddOrUpdate", "PomeloAdmin/generic-value-add-or-update/{typeId}/{id}");
//Admin Bitiş

//Web Sitesi

routePages.Add("/Referance", "referanslar");
routePages.Add("/Contact", "bize-ulasin");
routePages.Add("/ContactResult", "bize-ulasin-tesekkur");
routePages.Add("/Campaign", "kampanyalar");
routePages.Add("/Pricing", "fiyatlandirma");
routePages.Add("/PricingEcommerce", "e-ticaret-fiyatlari");
routePages.Add("/PricingDropshipping", "dropshipping-fiyatlari");
routePages.Add("/MarketplaceIntegration", "entegrasyonlar/{url}");
routePages.Add("/MarketPlaceIntegrationDetailRedirect", "entegrasyonlar/{base_url}/{url}");
routePages.Add("/MarketplaceIntegrationDetail", "{url}");
routePages.Add("/XmlIntegrations", "xml-entegrasyonlar");
routePages.Add("/XmlIntegrationDetail", "xml-entegrasyon/{url}");

routePages.Add("/ECommerce", "e-ticaret");
routePages.Add("/Dealer", "bayilerimiz");
routePages.Add("/PartnerSystem", "bayilik-sistemi");
routePages.Add("/Blog", "blogs/{p?}");
routePages.Add("/BlogDetail", "blog/{url}");

routePages.Add("/CustomPage", "iade-sartlari|kullanim-kosullari|gizlilik-sozlesmesi|hakkimizda");
routePages.Add("/Payment", "odeme-bildirimi");
routePages.Add("/Sss", "sikca-sorulan-sorular");
routePages.Add("/Support", "destek-merkezi");
routePages.Add("/SupportDetail", "destek-merkezi/{url}");
routePages.Add("/CommissionCalculation", "komisyon-hesaplama|{market}-komisyon-hesaplama");
routePages.Add("/Features", "ozellikler");
routePages.Add("/SiteMap", "sitemap.xml");
routePages.Add("/Career", "kariyer");
routePages.Add("/ECommerceForm", "e-ticaret-form");
routePages.Add("/MarketPlaceForm", "pazaryeri-form");
routePages.Add("/RegisterForm", "kayit-form/{id}");
routePages.Add("/CampaignDetail", "kampanya/{url}");

routePages.Add("/BuyPackage", "satin-al/{id}");
routePages.Add("/IPara/IParaResult", "ipara");
routePages.Add("/OrderSuccess", "basarili");
routePages.Add("/EcommerceOrderSuccess", "e-ticaret-basarili");

//Web Sitesi Bitiş

#if DEBUG
builder.Services.AddRazorPages().AddRazorPagesOptions(option =>
{
    foreach (var r in routePages)
    {
        if (r.Value.Split('|').Length > 1)
        {
            var splitData = r.Value.Split('|');
            foreach (var s in splitData)
            {
                option.Conventions.AddPageRoute(r.Key, s);
            }

        }
        else
            option.Conventions.AddPageRoute(r.Key, r.Value);
    }
}).AddRazorRuntimeCompilation();
#else
builder.Services.AddRazorPages().AddRazorPagesOptions(option =>
{
    foreach (var r in routePages)
    {
        if (r.Value.Split('|').Length > 1)
        {
            var splitData = r.Value.Split('|');
            foreach (var s in splitData)
            {
                option.Conventions.AddPageRoute(r.Key, s);
            }

        }
        else
            option.Conventions.AddPageRoute(r.Key, r.Value);

    }
});
#endif


#endregion



var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{

    app.UseExceptionHandler("/Error");
}

app.UseResponseCompression();

app.UseCookiePolicy();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseResponseCompression();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
