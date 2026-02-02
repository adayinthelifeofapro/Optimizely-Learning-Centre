using OptimizelyLearningCentre.Client.Courses.CMP.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.CMP;

/// <summary>
/// Course definition for Optimizely Content Marketing Platform (CMP)
/// </summary>
public static class CMPCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "cmp",
        Name = "Optimizely Content Marketing Platform",
        Description = "Master content planning, collaboration, and publishing with Optimizely CMP",
        LongDescription = "Learn how to streamline your marketing operations with Optimizely Content Marketing Platform. Master campaign management, workflow automation, digital asset management, and multi-channel publishing to deliver on-brand content at scale.",
        Icon = "calendar-days",
        BrandColor = "#7C3AED",
        ContentProviderType = typeof(CMPContentProvider),
        InteractivePanelType = typeof(CMPPanel),
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
                Title = "CMP Documentation",
                Url = "https://support.optimizely.com/hc/en-us/categories/7956512371085-Content-Marketing-Platform",
                Icon = "document-text"
            },
            new()
            {
                Title = "CMP Developer Docs",
                Url = "https://docs.developers.optimizely.com/content-marketing-platform/docs/get-started-with-omnichannel-content",
                Icon = "code-bracket"
            }
        }
    };
}
