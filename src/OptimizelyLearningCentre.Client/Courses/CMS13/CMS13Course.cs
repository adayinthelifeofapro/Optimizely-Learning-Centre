using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.CMS13;

/// <summary>
/// Course definition for Optimizely CMS 13
/// </summary>
public static class CMS13Course
{
    public static CourseDefinition Definition => new()
    {
        Id = "cms13",
        Name = "Optimizely CMS 13",
        Description = "Learn to build and manage content with Optimizely CMS 13 and integrated Optimizely Graph",
        LongDescription = "Master Optimizely CMS 13 with its deep Optimizely Graph integration. Learn about the enhanced Visual Builder, Content Manager, content variations, multilingual improvements, and the migration path from CMS 12.",
        Icon = "server",
        BrandColor = "#8B5CF6",
        ContentProviderType = typeof(CMS13ContentProvider),
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
                Title = "CMS 13 Documentation",
                Url = "https://docs.developers.optimizely.com/content-management-system/v13.0.0-CMS/docs/cms-13-overview",
                Icon = "document-text"
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
