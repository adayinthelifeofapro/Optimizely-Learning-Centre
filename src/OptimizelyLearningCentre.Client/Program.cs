using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using OptimizelyLearningCentre.Client;
using OptimizelyLearningCentre.Client.Services;
using OptimizelyLearningCentre.Client.State;
using OptimizelyLearningCentre.Client.Courses.Graph;
using OptimizelyLearningCentre.Client.Courses.Opal;
using OptimizelyLearningCentre.Client.Courses.SaaS;
using OptimizelyLearningCentre.Client.Courses.CMS12;
using OptimizelyLearningCentre.Client.Courses.CMS13;
using OptimizelyLearningCentre.Client.Courses.DXP;
using OptimizelyLearningCentre.Client.Courses.CMP;
using OptimizelyLearningCentre.Client.Courses.WebExp;
using OptimizelyLearningCentre.Client.Courses.FeatureExp;
using OptimizelyLearningCentre.Client.Courses.Commerce;
using OptimizelyLearningCentre.Client.Courses.ContentRecs;
using OptimizelyLearningCentre.Client.Courses.ODP;
using OptimizelyLearningCentre.Client.Courses.ConfiguredCommerce;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HTTP Client
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// State Management
builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<QueryState>();

// Course Infrastructure
builder.Services.AddSingleton<ICourseRegistry>(sp =>
{
    var registry = new CourseRegistry();
    registry.RegisterCourse(GraphCourse.Definition);
    registry.RegisterCourse(OpalCourse.Definition);
    registry.RegisterCourse(SaaSCourse.Definition);
    registry.RegisterCourse(CMS12Course.Definition);
    registry.RegisterCourse(CMS13Course.Definition);
    registry.RegisterCourse(DXPCourse.Definition);
    registry.RegisterCourse(CMPCourse.Definition);
    registry.RegisterCourse(WebExpCourse.Definition);
    registry.RegisterCourse(FeatureExpCourse.Definition);
    registry.RegisterCourse(CommerceCourse.Definition);
    registry.RegisterCourse(ContentRecsCourse.Definition);
    registry.RegisterCourse(ODPCourse.Definition);
    registry.RegisterCourse(ConfiguredCommerceCourse.Definition);
    return registry;
});
builder.Services.AddScoped<ICourseContext, CourseContext>();

// Content Providers
builder.Services.AddScoped<GraphContentProvider>();
builder.Services.AddScoped<OpalContentProvider>();
builder.Services.AddScoped<SaaSContentProvider>();
builder.Services.AddScoped<CMS12ContentProvider>();
builder.Services.AddScoped<CMS13ContentProvider>();
builder.Services.AddScoped<DXPContentProvider>();
builder.Services.AddScoped<CMPContentProvider>();
builder.Services.AddScoped<WebExpContentProvider>();
builder.Services.AddScoped<FeatureExpContentProvider>();
builder.Services.AddScoped<CommerceContentProvider>();
builder.Services.AddScoped<ContentRecsContentProvider>();
builder.Services.AddScoped<ODPContentProvider>();
builder.Services.AddScoped<ConfiguredCommerceContentProvider>();

// Core Services
builder.Services.AddScoped<ISettingsService, LocalStorageSettingsService>();
builder.Services.AddScoped<ILearningService, LearningService>();

// Graph-specific Services
builder.Services.AddScoped<IGraphQLClient, GraphQLClient>();
builder.Services.AddScoped<ISchemaService, SchemaService>();
builder.Services.AddScoped<IQueryBuilderService, QueryBuilderService>();

// Logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

await builder.Build().RunAsync();
