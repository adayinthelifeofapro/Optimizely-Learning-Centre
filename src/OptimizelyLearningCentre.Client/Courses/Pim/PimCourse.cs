using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.Pim;

public static class PimCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "pim",
        Name = "Optimizely PIM",
        Description = "Master product data management with Optimizely Product Information Management",
        LongDescription = "Learn to centralise, enrich, and distribute product information with Optimizely Product Information Management (PIM). Master product data modelling, properties and templates, catalog and category management, product variants, digital assets, import/export workflows, API integration, multi-language support, approval workflows, and role-based administration.",
        Icon = "clipboard-document-list",
        BrandColor = "#E97627",
        ContentProviderType = typeof(PimContentProvider),
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
                Title = "PIM Documentation",
                Url = "https://support.optimizely.com/hc/en-us/sections/4710619205517-Optimizely-Product-Information-Management-PIM",
                Icon = "document-text"
            },
            new()
            {
                Title = "PIM Getting Started",
                Url = "https://webhelp.optimizely.com/latest/en/b2b-commerce/pim/gettingstartedwithepipim.htm",
                Icon = "rocket-launch"
            },
            new()
            {
                Title = "PIM API (GitHub)",
                Url = "https://github.com/episerver/pim-api",
                Icon = "code-bracket"
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
