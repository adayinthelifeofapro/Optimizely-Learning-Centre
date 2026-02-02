using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.Analytics;

/// <summary>
/// Course definition for Optimizely Analytics
/// </summary>
public static class AnalyticsCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "analytics",
        Name = "Optimizely Analytics",
        Description = "Master warehouse-native analytics with Optimizely Analytics",
        LongDescription = "Learn to derive deep analytical insights about product usage and customer behaviour with Optimizely Analytics. Master warehouse-native architecture, exploration templates, funnel analysis, retention tracking, cohort segmentation, dashboards, NetScript, and experimentation analytics integration.",
        Icon = "chart-bar",
        BrandColor = "#7C3AED",
        ContentProviderType = typeof(AnalyticsContentProvider),
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
                Title = "Analytics Documentation",
                Url = "https://support.optimizely.com/hc/en-us/categories/33144620503053-Analytics",
                Icon = "document-text"
            },
            new()
            {
                Title = "Support Centre",
                Url = "https://support.optimizely.com/hc/en-us",
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
