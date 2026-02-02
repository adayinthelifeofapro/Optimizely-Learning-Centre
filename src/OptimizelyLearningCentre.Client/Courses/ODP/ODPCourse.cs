using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.ODP;

/// <summary>
/// Course definition for Optimizely Data Platform (ODP)
/// </summary>
public static class ODPCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "odp",
        Name = "Optimizely Data Platform",
        Description = "Master customer data management with Optimizely Data Platform (ODP)",
        LongDescription = "Learn to harness the power of Optimizely Data Platform (ODP) - a unified customer data platform that enables real-time customer understanding, AI-powered segmentation, and personalised multi-channel experiences. Master data structures, event tracking, Web SDK implementation, REST and GraphQL APIs, real-time segments, integrations with CMS and Commerce, privacy compliance, and advanced activation strategies.",
        Icon = "circle-stack",
        BrandColor = "#6366F1",
        ContentProviderType = typeof(ODPContentProvider),
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
                Title = "ODP Documentation",
                Url = "https://docs.developers.optimizely.com/optimizely-data-platform/docs/welcome",
                Icon = "document-text"
            },
            new()
            {
                Title = "ODP API Reference",
                Url = "https://docs.developers.optimizely.com/optimizely-data-platform/reference/introduction",
                Icon = "code-bracket"
            },
            new()
            {
                Title = "Support Centre",
                Url = "https://support.optimizely.com/hc/en-us/categories/25425312908301-Data-Platform",
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
