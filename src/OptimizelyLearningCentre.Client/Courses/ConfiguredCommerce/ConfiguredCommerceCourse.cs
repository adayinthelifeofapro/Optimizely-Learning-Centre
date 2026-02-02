using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.ConfiguredCommerce;

/// <summary>
/// Course definition for Optimizely Configured Commerce (B2B Commerce Cloud)
/// </summary>
public static class ConfiguredCommerceCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "configured-commerce",
        Name = "Optimizely Configured Commerce",
        Description = "Master B2B e-commerce development with Optimizely Configured Commerce",
        LongDescription = "Learn to build powerful B2B e-commerce solutions with Optimizely Configured Commerce (B2B Commerce Cloud). Master Spire CMS, catalog management, pricing strategies, checkout workflows, B2B account management, ERP/PIM integrations, and platform customization using handlers and pipelines.",
        Icon = "building-storefront",
        BrandColor = "#10B981",
        ContentProviderType = typeof(ConfiguredCommerceContentProvider),
        InteractivePanelType = typeof(CodePanel),
        NavItems = new List<CourseNavItem>
        {
            new()
            {
                Id = "home",
                Title = "Home",
                Icon = "home",
                Route = "",
                IsShared = true,
                Order = 0,
                Category = "Main"
            },
            new()
            {
                Id = "learn",
                Title = "Learn",
                Icon = "academic-cap",
                Route = "learn",
                IsShared = true,
                Order = 1,
                Category = "Main"
            },
            new()
            {
                Id = "settings",
                Title = "Settings",
                Icon = "cog-6-tooth",
                Route = "settings",
                IsShared = false,
                Order = 10,
                Category = "Settings"
            }
        },
        ExternalLinks = new List<ExternalLink>
        {
            new()
            {
                Title = "Configured Commerce Docs",
                Url = "https://docs.developers.optimizely.com/configured-commerce",
                Icon = "document-text"
            },
            new()
            {
                Title = "Spire CMS Docs",
                Url = "https://docs.developers.optimizely.com/configured-commerce/docs/understanding-5x-architecture-spire",
                Icon = "cube"
            },
            new()
            {
                Title = "REST API Reference",
                Url = "https://docs.developers.optimizely.com/configured-commerce/reference/getting-started-with-the-b2b-commerce-rest-apis",
                Icon = "code-bracket"
            },
            new()
            {
                Title = "Support Centre",
                Url = "https://support.optimizely.com/hc/en-us/sections/4413199588621-Optimizely-Configured-Commerce",
                Icon = "lifebuoy"
            },
            new()
            {
                Title = "Developer Community",
                Url = "https://world.optimizely.com/",
                Icon = "users"
            },
            new()
            {
                Title = "Optimizely Academy",
                Url = "https://academy.optimizely.com/",
                Icon = "academic-cap"
            }
        }
    };
}
