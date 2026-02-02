using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.Commerce;

/// <summary>
/// Course definition for Optimizely Commerce Connect
/// </summary>
public static class CommerceCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "commerce",
        Name = "Optimizely Commerce Connect",
        Description = "Master e-commerce development with Optimizely Commerce Connect",
        LongDescription = "Learn to build powerful e-commerce solutions with Optimizely Commerce Connect. Master catalog management, product hierarchies, pricing strategies, order processing, customer management, multi-market configurations, promotions, and payment/shipping integrations.",
        Icon = "shopping-cart",
        BrandColor = "#059669",
        ContentProviderType = typeof(CommerceContentProvider),
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
                Title = "Commerce Connect Docs",
                Url = "https://docs.developers.optimizely.com/commerce-connect",
                Icon = "document-text"
            },
            new()
            {
                Title = "Support Centre",
                Url = "https://support.optimizely.com/hc/en-us/categories/4413191384461-Commerce-Connect",
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
