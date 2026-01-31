using OptimizelyLearningCentre.Client.Courses.WebExp.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.WebExp;

/// <summary>
/// Course definition for Optimizely Web Experimentation
/// </summary>
public static class WebExpCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "webexp",
        Name = "Web Experimentation",
        Description = "Master A/B testing and experimentation with Optimizely Web Experimentation",
        LongDescription = "Learn to create and run powerful A/B tests with Optimizely Web Experimentation. Master the Visual Editor, JavaScript API, audience targeting, custom events, Stats Engine analytics, and integrations to optimize your website's performance and user experience.",
        Icon = "beaker",
        BrandColor = "#0037FF",
        ContentProviderType = typeof(WebExpContentProvider),
        InteractivePanelType = typeof(WebExpPanel),
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
                Title = "Web Experimentation Docs",
                Url = "https://docs.developers.optimizely.com/web-experimentation/docs/getting-started",
                Icon = "document-text"
            },
            new()
            {
                Title = "Support Help Center",
                Url = "https://support.optimizely.com/hc/en-us/categories/4410283702285-Web-Experimentation",
                Icon = "question-mark-circle"
            },
            new()
            {
                Title = "REST API Reference",
                Url = "https://docs.developers.optimizely.com/web-experimentation/docs/api-reference",
                Icon = "code-bracket"
            }
        }
    };
}
