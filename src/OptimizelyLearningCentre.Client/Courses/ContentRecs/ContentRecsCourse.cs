using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.ContentRecs;

/// <summary>
/// Course definition for Optimizely Content Recommendations
/// </summary>
public static class ContentRecsCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "contentrecs",
        Name = "Optimizely Content Recommendations",
        Description = "Master AI-powered content personalisation with Optimizely Content Recommendations",
        LongDescription = "Learn to implement intelligent content personalisation using Optimizely Content Recommendations. Master NLP-driven content analysis, visitor profiling, tracking implementation, widget configuration, flows and sections, A/B testing, analytics dashboards, and integration with Optimizely Data Platform (ODP).",
        Icon = "sparkles",
        BrandColor = "#7C3AED",
        ContentProviderType = typeof(ContentRecsContentProvider),
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
                Title = "Content Recommendations Docs",
                Url = "https://docs.developers.optimizely.com/recommendations/docs/content-recommendations",
                Icon = "document-text"
            },
            new()
            {
                Title = "Support Centre",
                Url = "https://support.optimizely.com/hc/en-us/categories/34797041979789-Content-Recommendations",
                Icon = "lifebuoy"
            },
            new()
            {
                Title = "Recommendations Portal",
                Url = "https://docs.developers.optimizely.com/recommendations/docs",
                Icon = "globe-alt"
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
