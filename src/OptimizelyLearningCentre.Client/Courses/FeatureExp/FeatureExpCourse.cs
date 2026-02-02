using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.FeatureExp;

/// <summary>
/// Course definition for Optimizely Feature Experimentation
/// </summary>
public static class FeatureExpCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "featureexp",
        Name = "Optimizely Feature Experimentation",
        Description = "Master feature flags, A/B testing, and experimentation with Optimizely Feature Experimentation",
        LongDescription = "Learn to implement powerful experimentation capabilities with Optimizely Feature Experimentation. Master feature flags, A/B testing, multi-armed bandits, targeted rollouts, and SDK implementation across multiple platforms including C#/.NET, JavaScript, and React.",
        Icon = "beaker",
        BrandColor = "#7C3AED",
        ContentProviderType = typeof(FeatureExpContentProvider),
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
                Title = "Feature Experimentation Docs",
                Url = "https://docs.developers.optimizely.com/feature-experimentation/docs/introduction",
                Icon = "document-text"
            },
            new()
            {
                Title = "Support Centre",
                Url = "https://support.optimizely.com/hc/en-us/categories/36676371584525-Feature-Experimentation",
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
