using OptimizelyLearningCentre.Client.Courses.SaaS.Components;
using OptimizelyLearningCentre.Client.Models.Course;

namespace OptimizelyLearningCentre.Client.Courses.ProductRecs;

/// <summary>
/// Course definition for Optimizely Product Recommendations
/// </summary>
public static class ProductRecsCourse
{
    public static CourseDefinition Definition => new()
    {
        Id = "product-recs",
        Name = "Optimizely Product Recommendations",
        Description = "Master personalised product recommendations powered by machine learning and behavioural tracking",
        LongDescription = "Learn to implement and optimise Optimizely Product Recommendations for e-commerce personalisation. Master catalog feeds, visitor tracking, recommendation widgets, algorithm strategies, merchandising campaigns, email recommendations, triggered messages, and the Personalization Portal to deliver relevant product suggestions that increase engagement and revenue.",
        Icon = "shopping-bag",
        BrandColor = "#F59E0B",
        ContentProviderType = typeof(ProductRecsContentProvider),
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
                Title = "Product Recommendations Docs",
                Url = "https://docs.developers.optimizely.com/recommendations/v1.1.0-product-recommendations/docs/personalization",
                Icon = "document-text"
            },
            new()
            {
                Title = "Commerce Connect Integration",
                Url = "https://docs.developers.optimizely.com/commerce-connect/docs/recommendations",
                Icon = "shopping-cart"
            },
            new()
            {
                Title = "Recommendations Portal",
                Url = "https://docs.developers.optimizely.com/recommendations/v1.1.0-product-recommendations/docs",
                Icon = "chart-bar"
            },
            new()
            {
                Title = "Support Centre",
                Url = "https://support.optimizely.com/hc/en-us/articles/4413200703501-Optimizely-Product-Recommendations",
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
