using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.ProductRecs;

/// <summary>
/// Content provider for the Optimizely Product Recommendations course
/// </summary>
public class ProductRecsContentProvider : ILearningContentProvider
{
    private List<LearningModule>? _modules;

    public async Task<List<LearningModule>> GetModulesAsync()
    {
        if (_modules == null)
        {
            _modules = BuildModules();
            LinkLessons(_modules);
        }
        return await Task.FromResult(_modules);
    }

    public async Task<LearningModule?> GetModuleAsync(string moduleId)
    {
        var modules = await GetModulesAsync();
        return modules.FirstOrDefault(m => m.Id == moduleId);
    }

    public async Task<Lesson?> GetLessonAsync(string lessonId)
    {
        var modules = await GetModulesAsync();
        return modules.SelectMany(m => m.Lessons).FirstOrDefault(l => l.Id == lessonId);
    }

    private void LinkLessons(List<LearningModule> modules)
    {
        foreach (var module in modules)
        {
            var orderedLessons = module.Lessons.OrderBy(l => l.Order).ToList();
            for (int i = 0; i < orderedLessons.Count; i++)
            {
                if (i > 0)
                    orderedLessons[i].PreviousLessonId = orderedLessons[i - 1].Id;
                if (i < orderedLessons.Count - 1)
                    orderedLessons[i].NextLessonId = orderedLessons[i + 1].Id;
            }
        }
    }

    private List<LearningModule> BuildModules()
    {
        return new List<LearningModule>
        {
            BuildGettingStartedModule(),
            BuildCatalogFeedModule(),
            BuildTrackingImplementationModule(),
            BuildServerToServerAPIModule(),
            BuildJavaScriptAPIModule(),
            BuildWidgetsStrategiesModule(),
            BuildAlgorithmsFiltersModule(),
            BuildMerchandisingCampaignsModule(),
            BuildEmailRecommendationsModule(),
            BuildReportingBestPracticesModule()
        };
    }

    #region Module 1: Getting Started

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started with Product Recommendations",
            Description = "Learn the fundamentals of Optimizely Product Recommendations and understand how machine learning powers personalised shopping experiences.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-what-is-product-recs",
                    ModuleId = "getting-started",
                    Title = "What is Product Recommendations?",
                    Summary = "Discover Optimizely Product Recommendations and its role in e-commerce personalisation.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Product Recommendations is and its purpose",
                        "Learn how machine learning drives personalised product suggestions",
                        "Understand the key benefits for e-commerce businesses",
                        "Know where Product Recommendations fits in the Optimizely DXP"
                    },
                    Content = @"
<h2>Introduction to Optimizely Product Recommendations</h2>
<p>Optimizely Product Recommendations is a <strong>machine learning-powered personalisation solution</strong> that delivers relevant product suggestions to visitors on your e-commerce website. It analyses visitor behaviour, order history, and crowd purchasing patterns to predict customer intent and serve contextually appropriate recommendations in real-time.</p>

<h3>What is Product Recommendations?</h3>
<p>Product Recommendations is part of the <strong>Optimizely Digital Experience Platform (DXP)</strong> and combines machine learning, artificial intelligence, and statistical analysis to personalise the shopping experience. The system tracks visitor interactions across your site, builds behavioural profiles, and uses sophisticated algorithms to determine which products are most likely to interest each individual visitor.</p>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Key Concept</p>
    <p class=""text-amber-800 dark:text-amber-200"">Product Recommendations automatically suggests products of interest based on website interaction, order history, visitor profiles, and intelligent algorithms. No manual product curation is required — the system learns and adapts from visitor behaviour.</p>
</div>

<h3>How It Works at a High Level</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li><strong>Catalog Import</strong> — Your product catalog is exported via an RSS feed to the Recommendations engine</li>
    <li><strong>Visitor Tracking</strong> — JavaScript or server-side tracking captures visitor interactions across page types</li>
    <li><strong>Behavioural Profiling</strong> — The ML engine builds profiles from browsing, searching, and purchasing patterns</li>
    <li><strong>Algorithm Processing</strong> — Strategies composed of stacked algorithms determine which products to recommend</li>
    <li><strong>Widget Delivery</strong> — Recommendations are displayed via configurable widgets on web pages and in emails</li>
</ol>

<h3>Key Benefits</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Increased Revenue</td><td class=""px-4 py-2"">Personalised suggestions drive higher conversion rates and average order values</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Automated Personalisation</td><td class=""px-4 py-2"">ML algorithms continuously learn from behaviour — no manual product curation needed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cross-Channel</td><td class=""px-4 py-2"">Deliver recommendations on web pages and in personalised email campaigns</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Real-Time Adaptation</td><td class=""px-4 py-2"">Recommendations update instantly as visitor behaviour changes during a session</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Merchandising Control</td><td class=""px-4 py-2"">Business users can refine or override algorithmic suggestions with merchandising rules</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">No Duplicate Recommendations</td><td class=""px-4 py-2"">Sequential widget generation ensures no product appears in more than one widget per page</td></tr>
    </tbody>
</table>

<h3>Product Recommendations vs Manual Curation</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Product Recommendations</th>
            <th class=""px-4 py-2 text-left"">Manual Curation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Personalisation</td><td class=""px-4 py-2"">Individual visitor level</td><td class=""px-4 py-2"">Same for all visitors</td></tr>
        <tr><td class=""px-4 py-2"">Scalability</td><td class=""px-4 py-2"">Automatic across entire catalog</td><td class=""px-4 py-2"">Manual effort per page/product</td></tr>
        <tr><td class=""px-4 py-2"">Adaptability</td><td class=""px-4 py-2"">Real-time learning from behaviour</td><td class=""px-4 py-2"">Static until manually updated</td></tr>
        <tr><td class=""px-4 py-2"">Maintenance</td><td class=""px-4 py-2"">Self-maintaining algorithms</td><td class=""px-4 py-2"">Ongoing manual effort required</td></tr>
        <tr><td class=""px-4 py-2"">Intelligence</td><td class=""px-4 py-2"">Crowd behaviour + individual profiling</td><td class=""px-4 py-2"">Human judgement only</td></tr>
    </tbody>
</table>

<h3>Where It Fits in the Optimizely DXP</h3>
<p>Product Recommendations integrates with several Optimizely products:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Commerce Connect (Customized Commerce)</strong> — Native integration via the <code>EPiServer.Personalization.Commerce</code> NuGet package</li>
    <li><strong>Configured Commerce</strong> — Integration for B2B e-commerce storefronts</li>
    <li><strong>Optimizely CMS</strong> — Recommendation widgets can be embedded in CMS content</li>
    <li><strong>Optimizely Data Platform (ODP)</strong> — Behavioural data can enrich ODP customer profiles</li>
    <li><strong>Email Service Providers</strong> — ESP-agnostic email recommendations for any email platform</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-how-it-works",
                    ModuleId = "getting-started",
                    Title = "How Product Recommendations Works",
                    Summary = "Understand the end-to-end architecture from tracking to recommendation delivery.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the complete recommendation pipeline",
                        "Learn how tracking, ML, and widgets work together",
                        "Understand the role of the Personalization Portal",
                        "Know how visitor identification and cookies function"
                    },
                    Content = @"
<h2>The Product Recommendations Engine</h2>
<p>Product Recommendations operates through a multi-component architecture that processes your catalog, tracks visitor behaviour, applies machine learning algorithms, and delivers personalised suggestions through configurable widgets.</p>

<h3>Architecture Overview</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│              Product Recommendations Architecture                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌──────────────┐    ┌──────────────┐    ┌──────────────┐      │
│  │   Catalog    │───▶│   Product    │───▶│   Algorithm  │      │
│  │  Feed (RSS)  │    │   Database   │    │    Engine    │      │
│  └──────────────┘    └──────────────┘    └──────────────┘      │
│                                                 │                │
│                                                 ▼                │
│  ┌──────────────┐    ┌──────────────┐    ┌──────────────┐      │
│  │   Visitor    │───▶│  Behavioural │───▶│   Strategy   │      │
│  │   Tracking   │    │   Profiles   │    │  Processing  │      │
│  └──────────────┘    └──────────────┘    └──────────────┘      │
│                                                 │                │
│                                                 ▼                │
│                      ┌──────────────────────────────┐           │
│                      │   Widget Delivery (Web/Email) │           │
│                      └──────────────────────────────┘           │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Core Components</h3>

<h4>1. Catalog Feed</h4>
<p>Your product catalog is exported as an RSS 2.0 XML feed containing product details (title, description, price, category, stock, images). A scheduled job regularly exports the catalog so the Recommendations engine always has up-to-date product data.</p>

<h4>2. Tracking Component</h4>
<p>The tracking system captures visitor interactions across different page types — product views, category browsing, search queries, basket additions, and completed orders. This data is sent as JSON payloads to the Recommendations servers.</p>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Visitor Identification</p>
    <p class=""text-amber-800 dark:text-amber-200"">Product Recommendations assigns each visitor a <strong>Consolidated User ID (CUID)</strong> stored in the <code>peerius_user</code> cookie. This allows the system to track behaviour across sessions and link web activity to email addresses for email recommendations.</p>
</div>

<h4>3. Machine Learning Engine</h4>
<p>The ML engine processes behavioural data using algorithms developed by Optimizely's data scientists. It analyses:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Individual behaviour</strong> — What this visitor has viewed, searched for, and purchased</li>
    <li><strong>Crowd behaviour</strong> — What other visitors with similar patterns have purchased</li>
    <li><strong>Product relationships</strong> — Category associations, complementary products, and alternatives</li>
    <li><strong>Popularity trends</strong> — Best sellers, trending products, and new arrivals</li>
</ul>

<h4>4. Strategy & Algorithm Stack</h4>
<p>Each widget uses a strategy composed of stacked algorithms. The engine processes algorithms sequentially:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Widget Strategy Processing:

Algorithm 1: ""Recently Viewed by Visitor""
  ├── Match found? → Add to recommendations
  └── No match?   → Try next algorithm

Algorithm 2: ""Frequently Bought Together""
  ├── Match found? → Add to recommendations
  └── No match?   → Try next algorithm

Algorithm 3: ""Best Sellers in Category""
  ├── Match found? → Add to recommendations
  └── No match?   → Try next algorithm

...up to 11 algorithms per widget

Fallback: ""Fallback Product Set""
  └── Always returns products to fill remaining slots
</pre>

<h4>5. Widget Delivery</h4>
<p>Recommendations are delivered through configurable widgets that can be placed on any page. Widgets generate recommendations sequentially — if a product appears in the first widget on a page, it will not appear in subsequent widgets, preventing duplicate recommendations.</p>

<h3>The Personalization Portal</h3>
<p>The Personalization Portal (also known as Smart Manager) is the web-based administration interface where business users configure and manage recommendations:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Configure widgets and strategies</li>
    <li>Create merchandising campaigns and rules</li>
    <li>Set up email recommendation templates</li>
    <li>Configure triggered message campaigns</li>
    <li>View performance reports and analytics dashboards</li>
    <li>Manage user roles and permissions</li>
</ul>

<h3>Data Flow Summary</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Stage</th>
            <th class=""px-4 py-2 text-left"">Input</th>
            <th class=""px-4 py-2 text-left"">Output</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Catalog Import</td><td class=""px-4 py-2"">RSS feed from Commerce</td><td class=""px-4 py-2"">Product database in Recs engine</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Tracking</td><td class=""px-4 py-2"">Visitor page interactions</td><td class=""px-4 py-2"">Behavioural profile data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ML Processing</td><td class=""px-4 py-2"">Profiles + product data</td><td class=""px-4 py-2"">Ranked product suggestions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Strategy Evaluation</td><td class=""px-4 py-2"">Algorithm stack + filters</td><td class=""px-4 py-2"">Filtered recommendation set</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Widget Rendering</td><td class=""px-4 py-2"">Recommendation set</td><td class=""px-4 py-2"">Displayed product cards</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-onboarding-setup",
                    ModuleId = "getting-started",
                    Title = "Onboarding and Account Setup",
                    Summary = "Learn the onboarding process, account provisioning, and Personalization Portal access.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the onboarding and provisioning process",
                        "Know how to access the Personalization Portal",
                        "Learn about user roles and permissions",
                        "Understand the setup workflow from requirements to go-live"
                    },
                    Content = @"
<h2>Onboarding and Account Setup</h2>
<p>Optimizely Product Recommendations requires a dedicated environment that is provisioned as part of the onboarding process. Unlike self-service tools, the setup involves coordination with Optimizely to configure the backend service and provide access credentials.</p>

<h3>Onboarding Process</h3>
<p>The typical onboarding workflow follows these stages:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li><strong>Requirements Analysis</strong> — Define your recommendation strategy, identify page types for tracking, and determine integration method (native, S2S API, or JavaScript)</li>
        <li><strong>Service Order</strong> — Your account manager facilitates the service order for a Product Recommendations environment</li>
        <li><strong>Environment Provisioning</strong> — Optimizely provisions the backend service and configures your account</li>
        <li><strong>Credentials Delivery</strong> — Configuration keys, tracking scripts, and portal credentials are sent to your technical contact via email</li>
        <li><strong>Catalog Feed Setup</strong> — Export your product catalog and configure the scheduled feed</li>
        <li><strong>Tracking Implementation</strong> — Implement visitor tracking across your site pages</li>
        <li><strong>Widget Configuration</strong> — Configure recommendation widgets and strategies in the Portal</li>
        <li><strong>Testing & Go-Live</strong> — Validate tracking data, verify recommendations, and deploy to production</li>
    </ol>
</div>

<h3>Accessing the Personalization Portal</h3>
<p>The Personalization Portal (Smart Manager) is the web-based interface for managing your Product Recommendations configuration. It can be accessed at:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
https://smartmanager.peerius.com/admin
https://smartmanager.peerius.episerver.net/admin
</pre>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Note</p>
    <p class=""text-blue-700 dark:text-blue-300"">The portal URLs reference the legacy ""Peerius"" brand name. Peerius was the recommendation engine company acquired by Optimizely (then EPiServer) and integrated into the platform.</p>
</div>

<h3>User Roles</h3>
<p>Opti ID provides the following user roles specific to Product Recommendations:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Role</th>
            <th class=""px-4 py-2 text-left"">Permissions</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Product Recs Editor</td><td class=""px-4 py-2"">View everything related to Product Recommendations and manage campaigns, widgets, and strategies</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product Recs Viewer</td><td class=""px-4 py-2"">View Product Recommendations reports (read-only access)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email Recs Editor</td><td class=""px-4 py-2"">View Email Content, Product Recommendations, and Triggers; manage campaigns and create templates</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email Recs Viewer</td><td class=""px-4 py-2"">View Email Product Recommendations and Trigger reports (read-only)</td></tr>
    </tbody>
</table>

<h3>Campaign Workflow Roles</h3>
<p>Within the portal, campaign management uses a two-role approval workflow:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Editor</strong> — Can view, create, and edit campaigns but cannot approve or launch them</li>
    <li><strong>Reviewer</strong> — Can approve, reject, and make campaigns go live</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Product Recommendations requires a separate licence from Optimizely. It is not included by default with Commerce Connect or CMS licences. Contact your Optimizely account manager to order the service.</p>
</div>

<h3>Configuration Keys</h3>
<p>After provisioning, you will receive several configuration items:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Client Name/ID</strong> — Your unique identifier in the Recommendations system</li>
    <li><strong>Tracking Script</strong> — JavaScript snippet for client-side tracking</li>
    <li><strong>API Keys</strong> — Authentication credentials for server-to-server API integration</li>
    <li><strong>Feed URL</strong> — Endpoint where your catalog feed should be published</li>
    <li><strong>Portal Credentials</strong> — Login details for the Personalization Portal</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-integration-methods",
                    ModuleId = "getting-started",
                    Title = "Integration Methods Overview",
                    Summary = "Compare the three integration approaches: Native, Server-to-Server API, and JavaScript API.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the three integration methods available",
                        "Know the advantages and trade-offs of each approach",
                        "Determine which integration method suits your project",
                        "Understand how native integration works with Commerce Connect"
                    },
                    Content = @"
<h2>Integration Methods</h2>
<p>Optimizely Product Recommendations offers three distinct integration methods. The choice depends on your platform, technical requirements, and desired level of control.</p>

<h3>Overview of Integration Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
            <th class=""px-4 py-2 text-left"">Tracking</th>
            <th class=""px-4 py-2 text-left"">Rendering</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Native Integration</td>
            <td class=""px-4 py-2"">Optimizely Commerce Connect</td>
            <td class=""px-4 py-2"">Server-side (C# attributes)</td>
            <td class=""px-4 py-2"">Server-side or client-side</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Server-to-Server API</td>
            <td class=""px-4 py-2"">Any website or application</td>
            <td class=""px-4 py-2"">Server-side (HTTP/JSON)</td>
            <td class=""px-4 py-2"">Server-side or client-side</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">JavaScript API</td>
            <td class=""px-4 py-2"">Any website (client-side)</td>
            <td class=""px-4 py-2"">Client-side (JavaScript)</td>
            <td class=""px-4 py-2"">Client-side (JavaScript)</td>
        </tr>
    </tbody>
</table>

<h3>1. Native Integration (Commerce Connect)</h3>
<p>The native integration uses the <code>EPiServer.Personalization.Commerce</code> NuGet package to tightly integrate with Optimizely Commerce Connect (Customized Commerce). It provides:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Server-side tracking via <code>CommerceTrackingAttribute</code> on controller actions</li>
    <li>Automatic catalog export via scheduled jobs</li>
    <li>Built-in recommendation content types for CMS integration</li>
    <li>Helper methods for retrieving and rendering recommendations</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Native integration example - tracking a product page
[CommerceTracking(TrackingType.Product)]
public ViewResult Product(ProductPage currentPage)
{
    var recommendations = this.GetRecommendationGroups();
    // Each group has an Area name and ContentReferences
    return View(new ProductViewModel
    {
        Recommendations = recommendations
    });
}
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Output Caching</p>
    <p class=""text-amber-800 dark:text-amber-200"">If you use output caching on pages with recommendations, you <strong>must</strong> use the client-side tracking API (version 2.1.0+). Server-side tracking breaks with cached responses because users see cached recommendations without their behaviour being tracked.</p>
</div>

<h3>2. Server-to-Server (S2S) API</h3>
<p>The S2S API lets you integrate any website or application with Product Recommendations. Tracking data is sent from your server to the Recommendations servers, and recommendations are returned in the response.</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Platform-agnostic — works with any backend technology</li>
    <li>JSON-based request/response format</li>
    <li>Your server manages cookies (CUID) directly</li>
    <li>Full control over data sent and received</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// S2S API conceptual flow
POST /tracker/product
{
  ""site"": ""your-client-name"",
  ""type"": ""product"",
  ""lang"": ""en"",
  ""refCode"": ""SKU-12345"",
  ""user"": ""CUID-value-from-cookie"",
  ""url"": ""https://yoursite.com/products/blue-widget""
}

// Response includes recommendations
{
  ""smartRecs"": [
    {
      ""widget"": ""alternatives"",
      ""items"": [
        { ""refCode"": ""SKU-67890"", ""title"": ""Red Widget"", ... }
      ]
    }
  ]
}
</pre>

<h3>3. JavaScript API</h3>
<p>The JavaScript API provides client-side tracking and recommendation rendering entirely in the browser. It uses the <code>PeeriusCallbacks</code> JavaScript variable for callbacks.</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>No server-side code changes required</li>
    <li>Flexible control over tracking and widget appearance</li>
    <li>JSON-based page tracking via JavaScript</li>
    <li>Recommendations delivered via callback functions</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// JavaScript API example
var PeeriusCallbacks = {
    smartRecs: function(jsonData) {
        // jsonData contains recommendation arrays
        jsonData.smartRecs.forEach(function(widget) {
            widget.items.forEach(function(item) {
                // Render each recommended product
                console.log(item.title, item.refCode, item.price);
            });
        });
    }
};
</pre>

<h3>Choosing the Right Method</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Consideration</th>
            <th class=""px-4 py-2 text-left"">Native</th>
            <th class=""px-4 py-2 text-left"">S2S API</th>
            <th class=""px-4 py-2 text-left"">JavaScript</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Platform</td><td class=""px-4 py-2"">Commerce Connect only</td><td class=""px-4 py-2"">Any</td><td class=""px-4 py-2"">Any</td></tr>
        <tr><td class=""px-4 py-2"">Setup Complexity</td><td class=""px-4 py-2"">Low (NuGet install)</td><td class=""px-4 py-2"">Medium</td><td class=""px-4 py-2"">Low</td></tr>
        <tr><td class=""px-4 py-2"">Cookie Management</td><td class=""px-4 py-2"">Automatic</td><td class=""px-4 py-2"">Manual (your server)</td><td class=""px-4 py-2"">Automatic</td></tr>
        <tr><td class=""px-4 py-2"">Output Caching</td><td class=""px-4 py-2"">Requires client-side API</td><td class=""px-4 py-2"">No issues</td><td class=""px-4 py-2"">No issues</td></tr>
        <tr><td class=""px-4 py-2"">Rendering Control</td><td class=""px-4 py-2"">Server + client</td><td class=""px-4 py-2"">Server + client</td><td class=""px-4 py-2"">Client only</td></tr>
        <tr><td class=""px-4 py-2"">SEO Impact</td><td class=""px-4 py-2"">Server-rendered content</td><td class=""px-4 py-2"">Server-rendered content</td><td class=""px-4 py-2"">Client-only (not crawled)</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Catalog Feed & Data Import

    private LearningModule BuildCatalogFeedModule()
    {
        return new LearningModule
        {
            Id = "catalog-feed",
            Title = "Catalog Feed & Data Import",
            Description = "Learn how to export your product catalog to the Recommendations engine using RSS feeds and understand the data requirements.",
            Icon = "circle-stack",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-catalog-feed-spec",
                    ModuleId = "catalog-feed",
                    Title = "Catalog Feed Specification",
                    Summary = "Understand the RSS 2.0 feed format used to export product data to the Recommendations engine.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the RSS 2.0 feed format for Product Recommendations",
                        "Learn the overall feed structure and XML elements",
                        "Know how the scheduled export job works",
                        "Understand the importance of feed quality for recommendation accuracy"
                    },
                    Content = @"
<h2>Catalog Feed Specification</h2>
<p>The quality of your product recommendations depends directly on the quality of your catalog feed. The feed provides the Recommendations engine with all the product data it needs — titles, descriptions, prices, categories, images, and stock levels. If the feed is not set up correctly, incorrect images, prices, or out-of-stock products may appear in recommendations.</p>

<h3>Feed Format</h3>
<p>The most commonly used format is <strong>XML in RSS 2.0</strong> (Really Simple Syndication). RSS is the preferred format for Optimizely Product Recommendations because it is widely supported, human-readable, and easy to validate.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;?xml version=""1.0"" encoding=""UTF-8""?&gt;
&lt;rss version=""2.0"" xmlns:p=""http://www.peerius.com/feeds""&gt;
  &lt;channel&gt;
    &lt;title&gt;Your Store - Product Feed&lt;/title&gt;
    &lt;link&gt;https://www.yourstore.com&lt;/link&gt;
    &lt;description&gt;Product catalog feed for recommendations&lt;/description&gt;
    &lt;pubDate&gt;Mon, 03 Feb 2025 10:00:00 GMT&lt;/pubDate&gt;

    &lt;item&gt;
      &lt;title&gt;Blue Running Shoes&lt;/title&gt;
      &lt;description&gt;Lightweight running shoes with cushioned sole&lt;/description&gt;
      &lt;pubDate&gt;Fri, 15 Nov 2024 00:00:00 GMT&lt;/pubDate&gt;
      &lt;guid&gt;SKU-SHOE-001&lt;/guid&gt;
      &lt;link&gt;https://www.yourstore.com/shoes/blue-running-shoes&lt;/link&gt;
      &lt;category&gt;Footwear &gt; Running &gt; Road Running&lt;/category&gt;
      &lt;p:unitPrice&gt;89.99&lt;/p:unitPrice&gt;
      &lt;p:salePrice&gt;69.99&lt;/p:salePrice&gt;
      &lt;p:currency&gt;GBP&lt;/p:currency&gt;
      &lt;p:stock&gt;42&lt;/p:stock&gt;
      &lt;p:imageUrl&gt;https://www.yourstore.com/images/shoe-001.jpg&lt;/p:imageUrl&gt;
    &lt;/item&gt;

    &lt;!-- More items... --&gt;
  &lt;/channel&gt;
&lt;/rss&gt;
</pre>

<h3>Feed Structure</h3>
<p>An RSS feed has two main sections:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Section</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Contains</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Channel</td><td class=""px-4 py-2"">Feed-level metadata</td><td class=""px-4 py-2"">Title, link, description, publication date</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Items</td><td class=""px-4 py-2"">Individual products</td><td class=""px-4 py-2"">Product details (one &lt;item&gt; per product)</td></tr>
    </tbody>
</table>

<h3>Scheduled Export Job</h3>
<p>In Optimizely Commerce Connect, a <strong>scheduled job</strong> regularly exports the product catalog from Commerce to be picked up by the product feed in Product Recommendations. This ensures the Recommendations engine always has current product data including:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>New products added to the catalog</li>
    <li>Updated prices and sale prices</li>
    <li>Stock level changes</li>
    <li>Category structure updates</li>
    <li>Removed or discontinued products</li>
</ul>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Feed Quality Matters</p>
    <p class=""text-amber-800 dark:text-amber-200"">The Recommendations engine can only recommend products that are in the feed, have stock greater than zero, and are marked as recommendable. Always validate your feed after making changes to your catalog structure.</p>
</div>

<h3>The Peerius XML Namespace</h3>
<p>Product Recommendations uses a custom XML namespace (<code>xmlns:p=""http://www.peerius.com/feeds""</code>) for elements specific to the recommendation engine, such as pricing, stock, and image URLs. Standard RSS elements (title, description, link, etc.) use the default namespace.</p>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-mandatory-feed-elements",
                    ModuleId = "catalog-feed",
                    Title = "Mandatory Feed Elements",
                    Summary = "Learn about the required elements every product must include in the catalog feed.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Know all mandatory item elements in the RSS feed",
                        "Understand the mandatory channel elements",
                        "Learn the correct format for categories and pricing",
                        "Understand the importance of GUID consistency"
                    },
                    Content = @"
<h2>Mandatory Feed Elements</h2>
<p>The RSS product feed contains both channel-level and item-level elements. Several of these are mandatory — without them, products will not be imported correctly or will be excluded from recommendations entirely.</p>

<h3>Mandatory Channel Elements</h3>
<p>Three channel elements must be included in every feed:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Element</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">&lt;title&gt;</td><td class=""px-4 py-2"">Name of the feed</td><td class=""px-4 py-2"">My Store - Product Feed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;link&gt;</td><td class=""px-4 py-2"">URL of your website</td><td class=""px-4 py-2"">https://www.mystore.com</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;pubDate&gt;</td><td class=""px-4 py-2"">Publication date of the feed</td><td class=""px-4 py-2"">Mon, 03 Feb 2025 10:00:00 GMT</td></tr>
    </tbody>
</table>

<h3>Mandatory Item Elements</h3>
<p>Each <code>&lt;item&gt;</code> in the feed must include the following elements:</p>

<h4>1. Title</h4>
<p>The product title is used as the label for links in recommendation widgets. It should convey enough information for a user to understand what the product is.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;title&gt;Blue Running Shoes - Size 10&lt;/title&gt;
</pre>

<h4>2. Description</h4>
<p>A text description of the product, typically a few sentences. Ideally plain text without HTML. Descriptions should stand alone and provide enough context for users to understand the product.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;description&gt;Lightweight road running shoes with cushioned midsole
and breathable mesh upper. Suitable for daily training runs.&lt;/description&gt;
</pre>

<h4>3. Publication Date (pubDate)</h4>
<p>The publication date of the item. This is valuable information for calculating recommendations (e.g., ""new arrivals"" algorithms). If your data does not support pubDate, you can provide an empty string.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;pubDate&gt;Fri, 15 Nov 2024 00:00:00 GMT&lt;/pubDate&gt;
</pre>

<h4>4. GUID (Unique Identifier)</h4>
<p>A unique identifier for the product within your catalog. This is the critical connector between your website data and the Recommendations engine.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;guid&gt;SKU-SHOE-001&lt;/guid&gt;
</pre>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Critical Requirement</p>
    <p class=""text-red-700 dark:text-red-300"">The GUID (refCode) in the feed <strong>must match exactly</strong> the product reference codes used on your website for tracking. Mismatches prevent the system from linking viewing and purchasing behaviour to specific products, which degrades recommendation quality and reporting accuracy.</p>
</div>

<h4>5. Link</h4>
<p>The URL of the product details page on your website. This provides the landing URL for recommendation widget links.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;link&gt;https://www.yourstore.com/shoes/blue-running-shoes&lt;/link&gt;
</pre>

<h4>6. Category</h4>
<p>The product category is critical — the majority of recommendation algorithms use it. Categories must match the category structure (breadcrumb) of your website. Use the greater-than symbol (<code>&gt;</code>) to separate category levels.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- Single category --&gt;
&lt;category&gt;Footwear &gt; Running &gt; Road Running&lt;/category&gt;

&lt;!-- Multiple categories for the same product --&gt;
&lt;category&gt;Footwear &gt; Running &gt; Road Running&lt;/category&gt;
&lt;category&gt;Sale &gt; Clearance &gt; Footwear&lt;/category&gt;
</pre>

<h4>7. Price</h4>
<p>Pricing information uses the Peerius namespace:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;p:unitPrice&gt;89.99&lt;/p:unitPrice&gt;
&lt;p:salePrice&gt;69.99&lt;/p:salePrice&gt;   &lt;!-- Optional: defaults to unitPrice --&gt;
&lt;p:currency&gt;GBP&lt;/p:currency&gt;
</pre>

<h4>8. Stock / Quantity</h4>
<p>The number of products available. Products with zero stock are excluded from recommendations.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;p:stock&gt;42&lt;/p:stock&gt;

&lt;!-- Alternative: use boolean in-stock flag --&gt;
&lt;p:inStock&gt;true&lt;/p:inStock&gt;
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-optional-feed-elements",
                    ModuleId = "catalog-feed",
                    Title = "Optional Feed Elements and Custom Attributes",
                    Summary = "Learn about optional elements and how to include custom product attributes in the feed.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Know the optional item elements available in the feed",
                        "Understand how to add custom attributes for filtering",
                        "Learn about image URL requirements",
                        "Understand multi-locale feed considerations"
                    },
                    Content = @"
<h2>Optional Feed Elements and Custom Attributes</h2>
<p>Beyond the mandatory elements, the RSS feed supports several optional elements and custom attributes that enhance recommendation quality and enable advanced filtering.</p>

<h3>Common Optional Elements</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Element</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">&lt;p:imageUrl&gt;</td><td class=""px-4 py-2"">Product image for widgets</td><td class=""px-4 py-2"">https://cdn.store.com/img/shoe.jpg</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;p:brand&gt;</td><td class=""px-4 py-2"">Product brand name</td><td class=""px-4 py-2"">Nike</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;p:rating&gt;</td><td class=""px-4 py-2"">Product rating score</td><td class=""px-4 py-2"">4.5</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;p:reviewCount&gt;</td><td class=""px-4 py-2"">Number of reviews</td><td class=""px-4 py-2"">127</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;p:isRecommendable&gt;</td><td class=""px-4 py-2"">Whether product can be recommended</td><td class=""px-4 py-2"">true</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;p:newFrom&gt;</td><td class=""px-4 py-2"">Date when product became ""new""</td><td class=""px-4 py-2"">2025-01-15</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">&lt;p:newTo&gt;</td><td class=""px-4 py-2"">Date when product stops being ""new""</td><td class=""px-4 py-2"">2025-03-15</td></tr>
    </tbody>
</table>

<h3>Custom Attributes</h3>
<p>You can include custom product attributes in the feed that can then be used for filtering in widget strategies. Custom attributes are defined using the Peerius namespace:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;item&gt;
  &lt;title&gt;Blue Running Shoes&lt;/title&gt;
  &lt;!-- ... standard elements ... --&gt;

  &lt;!-- Custom attributes --&gt;
  &lt;p:attribute name=""colour""&gt;Blue&lt;/p:attribute&gt;
  &lt;p:attribute name=""gender""&gt;Unisex&lt;/p:attribute&gt;
  &lt;p:attribute name=""material""&gt;Mesh&lt;/p:attribute&gt;
  &lt;p:attribute name=""season""&gt;Spring/Summer&lt;/p:attribute&gt;
  &lt;p:attribute name=""margin""&gt;35.5&lt;/p:attribute&gt;
&lt;/item&gt;
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Custom Attribute Use Cases</p>
    <p class=""text-amber-800 dark:text-amber-200"">Custom attributes are powerful for merchandising. For example, you can create a rule to only recommend products with a margin above 30%, or filter recommendations to show only products matching the visitor's preferred brand.</p>
</div>

<h3>Image URL Requirements</h3>
<p>Product images are displayed in recommendation widgets. For best results:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Use HTTPS URLs for all images</li>
    <li>Provide consistent image dimensions across products</li>
    <li>Use CDN-hosted images for fast loading</li>
    <li>Include a fallback/placeholder image for products without images</li>
    <li>Ensure images are publicly accessible (no authentication required)</li>
</ul>

<h3>Multi-Locale Feeds</h3>
<p>For multi-language or multi-currency sites, you may need separate feeds per locale. Key considerations:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Each locale should have its own feed with localised titles, descriptions, and prices</li>
    <li>Language settings in the feed must match the language configuration in the Personalization Portal</li>
    <li>GUIDs should be consistent across locale feeds for the same product</li>
    <li>Currency should match the target market for each feed</li>
</ul>

<h3>Complete Feed Example</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;?xml version=""1.0"" encoding=""UTF-8""?&gt;
&lt;rss version=""2.0"" xmlns:p=""http://www.peerius.com/feeds""&gt;
  &lt;channel&gt;
    &lt;title&gt;Sport Store - UK Product Feed&lt;/title&gt;
    &lt;link&gt;https://www.sportstore.co.uk&lt;/link&gt;
    &lt;description&gt;Full product catalog for recommendations&lt;/description&gt;
    &lt;pubDate&gt;Mon, 03 Feb 2025 10:00:00 GMT&lt;/pubDate&gt;

    &lt;item&gt;
      &lt;title&gt;Blue Running Shoes - Size 10&lt;/title&gt;
      &lt;description&gt;Lightweight road running shoes&lt;/description&gt;
      &lt;pubDate&gt;Fri, 15 Nov 2024 00:00:00 GMT&lt;/pubDate&gt;
      &lt;guid&gt;SKU-SHOE-001&lt;/guid&gt;
      &lt;link&gt;https://www.sportstore.co.uk/shoes/blue-running&lt;/link&gt;
      &lt;category&gt;Footwear &gt; Running &gt; Road&lt;/category&gt;
      &lt;p:unitPrice&gt;89.99&lt;/p:unitPrice&gt;
      &lt;p:salePrice&gt;69.99&lt;/p:salePrice&gt;
      &lt;p:currency&gt;GBP&lt;/p:currency&gt;
      &lt;p:stock&gt;42&lt;/p:stock&gt;
      &lt;p:imageUrl&gt;https://cdn.sportstore.co.uk/shoe-001.jpg&lt;/p:imageUrl&gt;
      &lt;p:brand&gt;RunFast&lt;/p:brand&gt;
      &lt;p:rating&gt;4.5&lt;/p:rating&gt;
      &lt;p:attribute name=""colour""&gt;Blue&lt;/p:attribute&gt;
      &lt;p:attribute name=""gender""&gt;Unisex&lt;/p:attribute&gt;
    &lt;/item&gt;

  &lt;/channel&gt;
&lt;/rss&gt;
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-feed-validation",
                    ModuleId = "catalog-feed",
                    Title = "Feed Validation and Troubleshooting",
                    Summary = "Learn how to validate your catalog feed and resolve common issues.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Know how to validate feed structure and content",
                        "Understand common feed issues and their impact",
                        "Learn troubleshooting techniques for feed problems",
                        "Understand feed discrepancy reporting"
                    },
                    Content = @"
<h2>Feed Validation and Troubleshooting</h2>
<p>A correctly structured and complete catalog feed is essential for high-quality recommendations. Feed issues can result in missing products, incorrect prices, broken images, or poor recommendation relevance.</p>

<h3>Feed Validation Checklist</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>Valid RSS 2.0 XML structure with correct Peerius namespace declaration</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>All mandatory channel elements present (title, link, pubDate)</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>All items have mandatory elements (title, description, guid, link, category, price, stock)</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>GUIDs are unique across all items in the feed</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>GUIDs match the refCodes used in website tracking</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>Categories match the website breadcrumb structure using &gt; separator</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>Image URLs are accessible via HTTPS</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>Products with stock &gt; 0 are included</span>
        </li>
        <li class=""flex items-start gap-2"">
            <span class=""text-green-500 font-bold"">✓</span>
            <span>Feed encoding is UTF-8</span>
        </li>
    </ul>
</div>

<h3>Common Feed Issues</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Impact</th>
            <th class=""px-4 py-2 text-left"">Resolution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">GUID mismatch</td><td class=""px-4 py-2"">Products not linked to tracking data</td><td class=""px-4 py-2"">Ensure refCodes match between feed and site tracking</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Missing categories</td><td class=""px-4 py-2"">Algorithms cannot find related products</td><td class=""px-4 py-2"">Add category elements matching site breadcrumbs</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Zero stock products</td><td class=""px-4 py-2"">Out-of-stock items recommended</td><td class=""px-4 py-2"">Set stock to 0 or exclude from feed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Broken image URLs</td><td class=""px-4 py-2"">Missing images in widgets</td><td class=""px-4 py-2"">Validate all image URLs are HTTPS and accessible</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Stale feed data</td><td class=""px-4 py-2"">Outdated prices or discontinued products shown</td><td class=""px-4 py-2"">Verify scheduled export job runs regularly</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category mismatch</td><td class=""px-4 py-2"">Wrong category-based recommendations</td><td class=""px-4 py-2"">Align feed categories with website category structure</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Encoding issues</td><td class=""px-4 py-2"">Special characters displayed incorrectly</td><td class=""px-4 py-2"">Use UTF-8 encoding and properly escape XML entities</td></tr>
    </tbody>
</table>

<h3>Feed Discrepancy Reporting</h3>
<p>The Personalization Portal provides discrepancy reports that highlight issues between your feed and the Recommendations engine's expectations. Check these reports regularly to identify:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Products in the feed that have no tracking data (never viewed on site)</li>
    <li>Products tracked on site that are not in the feed</li>
    <li>Products with mismatched data between feed and tracking</li>
    <li>Feed import errors or rejected items</li>
</ul>

<h3>Troubleshooting Workflow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Problem: Recommendations not showing for a product

Step 1: Check the product exists in the feed
  └── Is the GUID present? → If not, add to feed

Step 2: Check the product is recommendable
  └── Is stock > 0? → If not, update stock
  └── Is isRecommendable true? → If not, update flag

Step 3: Verify GUID consistency
  └── Does feed GUID match the refCode in tracking?
  └── Compare: feed &lt;guid&gt; vs tracking refCode

Step 4: Check category mapping
  └── Does the feed category match site breadcrumbs?
  └── Are categories using &gt; separator?

Step 5: Check the Personalization Portal
  └── View feed import status and discrepancy reports
  └── Verify product appears in the product database
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Pro Tip</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">When troubleshooting, always check feed issues first before investigating tracking or widget configuration. The majority of recommendation problems originate from feed data quality issues.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 3: Tracking Implementation

    private LearningModule BuildTrackingImplementationModule()
    {
        return new LearningModule
        {
            Id = "tracking-implementation",
            Title = "Tracking Implementation",
            Description = "Master visitor tracking across page types to feed the machine learning algorithms with behavioural data.",
            Icon = "signal",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-tracking-fundamentals",
                    ModuleId = "tracking-implementation",
                    Title = "Tracking Fundamentals",
                    Summary = "Understand the core concepts of visitor tracking, session management, and behavioural data collection.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand why tracking is essential for recommendations",
                        "Learn how CUID cookies identify visitors across sessions",
                        "Know the minimum tracking requirements",
                        "Understand the tracking data flow from site to Recommendations servers"
                    },
                    Content = @"
<h2>Tracking Fundamentals</h2>
<p>Tracking is the foundation of Product Recommendations. It follows the user journey across your site, storing session and user behaviours that enrich the machine learning algorithms. Without tracking, the system cannot build visitor profiles or generate personalised recommendations.</p>

<h3>Why Tracking Matters</h3>
<p>Tracking serves two critical purposes:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li><strong>Personalised Recommendations</strong> — Individual visitor behaviour drives which products are suggested</li>
    <li><strong>KPI Measurement</strong> — Tracking data feeds into analytics dashboards to measure recommendation performance (revenue, clicks, conversion)</li>
</ol>

<h3>How Tracking Works</h3>
<p>With Commerce Connect native integration, a JSON payload is sent to an API endpoint on the Recommendations servers containing information about the page, such as:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Page type (product, category, home, basket, etc.)</li>
    <li>Page URL</li>
    <li>Product reference codes (refCodes)</li>
    <li>Search terms (for search pages)</li>
    <li>Basket contents (for basket/checkout pages)</li>
    <li>Order details (for order confirmation pages)</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Tracking Data Flow:

┌──────────────┐     ┌──────────────────┐     ┌──────────────────┐
│   Visitor    │────▶│   Your Website   │────▶│  Recommendations │
│   Browser    │     │   (Tracking)     │     │     Servers      │
└──────────────┘     └──────────────────┘     └──────────────────┘
                            │                         │
                     JSON payload with:          Processes data:
                     - Page type                 - Updates profile
                     - Product codes             - Runs algorithms
                     - User CUID                 - Returns recs
                     - URL, search terms
</pre>

<h3>Visitor Identification (CUID)</h3>
<p>Product Recommendations identifies visitors using a <strong>Consolidated User ID (CUID)</strong> stored in the <code>peerius_user</code> cookie. This cookie:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Is generated automatically by Product Recommendations if not present</li>
    <li>Persists across sessions to maintain visitor profile continuity</li>
    <li>Links web browsing behaviour to email addresses for email recommendations</li>
    <li>Is a first-party cookie set on your domain</li>
</ul>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Minimum Tracking Requirements</p>
    <p class=""text-amber-800 dark:text-amber-200"">Optimizely recommends tracking all points of the user journey. However, as a minimum you should track <strong>product pages</strong>, <strong>basket pages</strong>, and <strong>order pages</strong> — plus any other pages that should show recommendations.</p>
</div>

<h3>IP Address Tracking</h3>
<p>IP address tracking is enabled by default but is only used for geolocation — the IP address itself is not stored. You can disable this via the configuration setting:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// appsettings.json
{
  ""episerver"": {
    ""personalization"": {
      ""SkipUserHostTracking"": true  // Disable IP-based geolocation
    }
  }
}
</pre>

<h3>User Email vs Pseudonymous ID</h3>
<p>By default, the user's email address is included in tracking data to link web behaviour with email campaigns. You can switch to pseudonymous identifiers instead:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// appsettings.json
{
  ""episerver"": {
    ""personalization"": {
      ""UsePseudonymousUserId"": true  // Use anonymous IDs instead of email
    }
  }
}
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">If you switch between email and pseudonymous tracking on an existing implementation, coordinate the change with the Optimizely Recommendations team to avoid data mapping issues with existing visitor profiles.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-page-type-tracking",
                    ModuleId = "tracking-implementation",
                    Title = "Page Type Tracking",
                    Summary = "Learn the predefined page types and what data to track on each.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Know all predefined tracking page types",
                        "Understand what data each page type sends",
                        "Learn the tracking payload structure for each page type",
                        "Understand how different page types feed different algorithms"
                    },
                    Content = @"
<h2>Page Type Tracking</h2>
<p>Product Recommendations defines specific page types that correspond to stages in the customer journey. Each page type sends different data to the Recommendations engine, which feeds into different algorithms.</p>

<h3>Predefined Page Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Page Type</th>
            <th class=""px-4 py-2 text-left"">Key Data Sent</th>
            <th class=""px-4 py-2 text-left"">Algorithms Fed</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Home</td><td class=""px-4 py-2"">URL, visitor CUID</td><td class=""px-4 py-2"">Personalised, popular, trending</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product</td><td class=""px-4 py-2"">refCode, category, URL</td><td class=""px-4 py-2"">Alternatives, cross-sell, viewed together</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category</td><td class=""px-4 py-2"">Category path, URL</td><td class=""px-4 py-2"">Category best sellers, trending in category</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Search</td><td class=""px-4 py-2"">Search term, URL</td><td class=""px-4 py-2"">Search-relevant, popular for search term</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Basket</td><td class=""px-4 py-2"">Basket items (refCodes, quantities)</td><td class=""px-4 py-2"">Complementary, frequently bought together</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Checkout</td><td class=""px-4 py-2"">Checkout items, total</td><td class=""px-4 py-2"">Completes conversion funnel tracking</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Order</td><td class=""px-4 py-2"">Order items, total, order ID</td><td class=""px-4 py-2"">Post-purchase, frequently bought together</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Wishlist</td><td class=""px-4 py-2"">Wishlist items (refCodes)</td><td class=""px-4 py-2"">Wishlist-based personalisation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Brand</td><td class=""px-4 py-2"">Brand name, URL</td><td class=""px-4 py-2"">Brand affinity, popular by brand</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Attribute</td><td class=""px-4 py-2"">Attribute name/value, URL</td><td class=""px-4 py-2"">Attribute-based filtering</td></tr>
    </tbody>
</table>

<h3>Product Page Tracking Payload</h3>
<p>The product page is the most critical page type to track. It captures which specific products a visitor is interested in:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Product page tracking payload
{
  ""site"": ""your-client-name"",
  ""type"": ""product"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/shoes/blue-running-shoes"",
  ""refCode"": ""SKU-SHOE-001"",
  ""category"": ""Footwear > Running > Road"",
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Basket Page Tracking Payload</h3>
<p>Basket tracking captures the products a visitor intends to purchase, which powers cross-sell and complementary product algorithms:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Basket page tracking payload
{
  ""site"": ""your-client-name"",
  ""type"": ""basket"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/basket"",
  ""basket"": {
    ""items"": [
      { ""refCode"": ""SKU-SHOE-001"", ""qty"": 1, ""price"": 69.99 },
      { ""refCode"": ""SKU-SOCK-005"", ""qty"": 2, ""price"": 9.99 }
    ],
    ""total"": 89.97,
    ""currency"": ""GBP""
  },
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Order Page Tracking Payload</h3>
<p>Order tracking captures completed purchases, which is essential for training the ""frequently bought together"" and ""post-purchase"" algorithms:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Order confirmation page tracking payload
{
  ""site"": ""your-client-name"",
  ""type"": ""order"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/order/confirmation"",
  ""order"": {
    ""orderNo"": ""ORD-2025-00142"",
    ""items"": [
      { ""refCode"": ""SKU-SHOE-001"", ""qty"": 1, ""price"": 69.99 },
      { ""refCode"": ""SKU-SOCK-005"", ""qty"": 2, ""price"": 9.99 }
    ],
    ""total"": 89.97,
    ""currency"": ""GBP""
  },
  ""user"": ""CUID-abc123""
}
</pre>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">refCode Consistency</p>
    <p class=""text-blue-700 dark:text-blue-300"">You must ensure that each product <code>refCode</code> is consistent across all page types. If the product page tracks <code>SKU-SHOE-001</code> but the basket tracks <code>shoe-001</code>, the system cannot link viewing and purchasing behaviour, which degrades recommendation quality.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-native-commerce-integration",
                    ModuleId = "tracking-implementation",
                    Title = "Native Commerce Connect Integration",
                    Summary = "Implement server-side tracking using the Commerce Connect native integration package.",
                    Order = 3,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Install and configure the EPiServer.Personalization.Commerce package",
                        "Implement tracking using CommerceTrackingAttribute",
                        "Retrieve recommendations via GetRecommendationGroups()",
                        "Understand the ITrackingService for custom tracking scenarios"
                    },
                    Content = @"
<h2>Native Commerce Connect Integration</h2>
<p>The native integration is the simplest way to implement Product Recommendations if you are using Optimizely Commerce Connect (Customized Commerce). The <code>EPiServer.Personalization.Commerce</code> NuGet package provides server-side tracking attributes, recommendation retrieval helpers, and automatic catalog export.</p>

<h3>Installation</h3>
<p>Install the integration package via NuGet:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
dotnet add package EPiServer.Personalization.Commerce
</pre>

<h3>Configuration</h3>
<p>Add the Recommendations configuration to your <code>appsettings.json</code>:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""episerver"": {
    ""personalization"": {
      ""ClientName"": ""your-client-name"",
      ""Enabled"": true,
      ""SkipUserHostTracking"": false,
      ""UsePseudonymousUserId"": false
    }
  }
}
</pre>

<h3>Tracking with CommerceTrackingAttribute</h3>
<p>The simplest way to add tracking is by decorating your controller actions with the <code>[CommerceTracking]</code> attribute. The attribute sends tracking data to the Recommendations servers during <code>OnActionExecuting</code>, so recommendations are available by the time your action method runs.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
using EPiServer.Personalization.Commerce.Tracking;

public class ProductController : PageController&lt;ProductPage&gt;
{
    // Track product page views
    [CommerceTracking(TrackingType.Product)]
    public ViewResult Index(ProductPage currentPage)
    {
        var recommendations = this.GetRecommendationGroups();
        return View(new ProductViewModel
        {
            CurrentPage = currentPage,
            Recommendations = recommendations
        });
    }
}

public class StartPageController : PageController&lt;StartPage&gt;
{
    // Track home page views
    [CommerceTracking(TrackingType.Home)]
    public ViewResult Index(StartPage currentPage)
    {
        var recommendations = this.GetRecommendationGroups();
        return View(new StartPageViewModel
        {
            CurrentPage = currentPage,
            Recommendations = recommendations
        });
    }
}

public class SearchController : PageController&lt;SearchPage&gt;
{
    // Track search page views
    [CommerceTracking(TrackingType.Search)]
    public ViewResult Index(SearchPage currentPage, string q)
    {
        var recommendations = this.GetRecommendationGroups();
        return View(new SearchViewModel
        {
            CurrentPage = currentPage,
            SearchTerm = q,
            Recommendations = recommendations
        });
    }
}
</pre>

<h3>Available TrackingType Values</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">TrackingType</th>
            <th class=""px-4 py-2 text-left"">Use On</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Home</td><td class=""px-4 py-2"">Start/home page</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Product</td><td class=""px-4 py-2"">Product detail pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Category</td><td class=""px-4 py-2"">Category listing pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Search</td><td class=""px-4 py-2"">Search results pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Basket</td><td class=""px-4 py-2"">Shopping basket/cart page</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Checkout</td><td class=""px-4 py-2"">Checkout pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Order</td><td class=""px-4 py-2"">Order confirmation page</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Wishlist</td><td class=""px-4 py-2"">Wishlist page</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Brand</td><td class=""px-4 py-2"">Brand landing pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">TrackingType.Attribute</td><td class=""px-4 py-2"">Attribute-filtered pages</td></tr>
    </tbody>
</table>

<h3>Retrieving Recommendations</h3>
<p>The <code>GetRecommendationGroups()</code> extension method returns a collection of recommendation groups. Each group has:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Area</strong> — The widget name/area (e.g., ""alternatives"", ""cross-sell"")</li>
    <li><strong>ContentReferences</strong> — References to the recommended product content items</li>
</ul>

<h3>Using ITrackingService</h3>
<p>For more control, inject <code>ITrackingService</code> directly to send custom tracking data:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
using EPiServer.Personalization.Commerce.Tracking;

public class CustomTrackingController : Controller
{
    private readonly ITrackingService _trackingService;

    public CustomTrackingController(ITrackingService trackingService)
    {
        _trackingService = trackingService;
    }

    public async Task&lt;IActionResult&gt; TrackCustomPage()
    {
        var trackingData = new CommerceTrackingData
        {
            PageType = TrackingType.Product,
            // ... configure tracking data
        };

        var result = await _trackingService.TrackAsync(trackingData);
        // result contains recommendations
        return View();
    }
}
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Recommendation Availability</p>
    <p class=""text-amber-800 dark:text-amber-200"">When using <code>CommerceTrackingAttribute</code>, recommendations are available immediately in your action method because the attribute processes during <code>OnActionExecuting</code> — before your action method runs.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-custom-tracking-attributes",
                    ModuleId = "tracking-implementation",
                    Title = "Custom Tracking Attributes",
                    Summary = "Learn how to include custom data in tracking requests for advanced filtering.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand how to set custom tracking attributes",
                        "Know the supported data types for custom attributes",
                        "Learn practical use cases for custom tracking data",
                        "Implement custom attributes in Commerce Connect integration"
                    },
                    Content = @"
<h2>Custom Tracking Attributes</h2>
<p>Beyond standard tracking data, you can include custom attributes in tracking requests. These allow you to pass additional context about the visitor or page to the Recommendations engine, enabling more sophisticated filtering and personalisation.</p>

<h3>Setting Custom Attributes</h3>
<p>Use the <code>SetCustomAttribute</code> extension method on <code>CommerceTrackingData</code> to add custom data:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
using EPiServer.Personalization.Commerce.Tracking;

[CommerceTracking(TrackingType.Product)]
public ViewResult Index(ProductPage currentPage)
{
    // Access the tracking data for the current request
    var trackingData = HttpContext.GetCommerceTrackingData();

    // Set custom attributes
    trackingData.SetCustomAttribute(""customerTier"", ""Gold"");
    trackingData.SetCustomAttribute(""preferredBrand"", ""Nike"");
    trackingData.SetCustomAttribute(""loyaltyPoints"", 2500);
    trackingData.SetCustomAttribute(""memberSince"", DateTime.Parse(""2022-03-15""));

    var recommendations = this.GetRecommendationGroups();
    return View(new ProductViewModel
    {
        CurrentPage = currentPage,
        Recommendations = recommendations
    });
}
</pre>

<h3>Supported Data Types</h3>
<p>Custom attributes are limited to the following data types:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">C# Types</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">String</td><td class=""px-4 py-2"">string</td><td class=""px-4 py-2"">""Gold"", ""Nike"", ""UK""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Integer</td><td class=""px-4 py-2"">int, long, short</td><td class=""px-4 py-2"">2500, 42, 100</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Decimal</td><td class=""px-4 py-2"">decimal, double, float</td><td class=""px-4 py-2"">29.99, 0.15, 4.5</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Date/Time</td><td class=""px-4 py-2"">DateTime</td><td class=""px-4 py-2"">2025-01-15T00:00:00</td></tr>
    </tbody>
</table>

<h3>Use Cases for Custom Attributes</h3>

<h4>Customer Segmentation</h4>
<p>Pass customer tier or segment information to filter recommendations based on customer value:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
trackingData.SetCustomAttribute(""customerTier"", ""Premium"");
// Widgets can be configured to show higher-margin products for Premium customers
</pre>

<h4>Geographic Filtering</h4>
<p>Pass location data to recommend region-appropriate products:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
trackingData.SetCustomAttribute(""region"", ""Europe"");
trackingData.SetCustomAttribute(""country"", ""UK"");
// Recommend products available for delivery in the visitor's region
</pre>

<h4>Duty-Free / Travel Retail</h4>
<p>A practical example from travel retail — pass flight information to filter recommendations:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
trackingData.SetCustomAttribute(""flightType"", ""International"");
trackingData.SetCustomAttribute(""destination"", ""EU"");
// Show duty-free eligible products based on flight destination
</pre>

<h4>B2B Account Context</h4>
<p>For B2B scenarios, pass account-level data to personalise recommendations:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
trackingData.SetCustomAttribute(""industry"", ""Manufacturing"");
trackingData.SetCustomAttribute(""accountSize"", ""Enterprise"");
trackingData.SetCustomAttribute(""contractTier"", ""Tier1"");
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Connecting Tracking to Widgets</p>
    <p class=""text-amber-800 dark:text-amber-200"">Custom tracking attributes become available as filter criteria in widget configuration. Once you send custom attributes in tracking, you can configure widgets in the Personalization Portal to filter recommendations based on those attribute values.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-click-tracking",
                    ModuleId = "tracking-implementation",
                    Title = "Click Tracking and Verification",
                    Summary = "Implement click tracking to measure recommendation effectiveness and verify your tracking setup.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of click tracking for recommendations",
                        "Implement click tracking via RecommendationId",
                        "Learn how to verify tracking is working correctly",
                        "Know the common troubleshooting steps for tracking issues"
                    },
                    Content = @"
<h2>Click Tracking and Verification</h2>
<p>Click tracking connects visitor actions back to specific recommendations, enabling the system to measure which recommendations drive engagement and conversions. Without click tracking, the analytics dashboards cannot accurately report on recommendation performance.</p>

<h3>How Click Tracking Works</h3>
<p>When a recommendation is displayed, each recommended product has a unique <code>RecommendationId</code>. When a visitor clicks on a recommended product, this ID is passed back to the Recommendations servers to record the click event.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Click Tracking Flow:

1. Widget displays product with RecommendationId = ""rec-abc-123""

2. Visitor clicks the recommended product

3. Click event sends RecommendationId back to Recommendations servers

4. System records: ""Recommendation rec-abc-123 was clicked""

5. Subsequent purchase tracking links the order to the recommendation
   └── This enables revenue attribution reporting
</pre>

<h3>Implementation with Query Strings</h3>
<p>The standard implementation passes the <code>RecommendationId</code> via a query string parameter in the product link URL:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- Recommendation widget template --&gt;
&lt;a href=""{{url}}&amp;recommendationId={{id}}""
   data-url=""{{#attributes}}{{url}}{{/attributes}}&amp;recommendationId={{id}}""
   class=""recommendation-item""&gt;
    &lt;img src=""{{imageUrl}}"" alt=""{{title}}"" /&gt;
    &lt;span&gt;{{title}}&lt;/span&gt;
    &lt;span&gt;{{price}}&lt;/span&gt;
&lt;/a&gt;
</pre>

<p>When the visitor lands on the product page, the tracking code reads the <code>recommendationId</code> from the query string and includes it in the tracking payload:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Product page tracking with click attribution
{
  ""site"": ""your-client-name"",
  ""type"": ""product"",
  ""refCode"": ""SKU-SHOE-001"",
  ""recommendationId"": ""rec-abc-123"",  // Links this view to the recommendation click
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Verifying Tracking</h3>
<p>Use these methods to verify your tracking implementation is working correctly:</p>

<h4>For Native and S2S Integrations</h4>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Open browser developer tools (Network tab)</li>
    <li>Navigate to a tracked page on your site</li>
    <li>Look for the request to the Recommendations API endpoint</li>
    <li>Verify the JSON payload contains the correct page type, refCodes, and user CUID</li>
    <li>Check the response contains <code>smartRecs</code> data with recommendation arrays</li>
</ol>

<h4>For JavaScript API Integrations</h4>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Place breakpoints in the <code>smartRecsClick</code> and <code>smartRecsSendClick</code> functions</li>
    <li>Click on a recommended product</li>
    <li>Verify the ID passed to the function matches the ID from the <code>smartRecs</code> response</li>
</ol>

<h3>Troubleshooting Tracking Issues</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Symptom</th>
            <th class=""px-4 py-2 text-left"">Possible Cause</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Empty smartRecs response</td><td class=""px-4 py-2"">No widgets activated for page type</td><td class=""px-4 py-2"">Activate widgets in Portal → Configuration → Activation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recommendations not personalised</td><td class=""px-4 py-2"">CUID cookie not set or lost</td><td class=""px-4 py-2"">Verify peerius_user cookie is present and persistent</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Wrong products recommended</td><td class=""px-4 py-2"">refCode mismatch between pages</td><td class=""px-4 py-2"">Check refCode consistency across all page types</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">No click attribution in reports</td><td class=""px-4 py-2"">RecommendationId not passed</td><td class=""px-4 py-2"">Verify query string parameter is included in links</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Tracking not firing</td><td class=""px-4 py-2"">Missing tracking attribute/script</td><td class=""px-4 py-2"">Verify CommerceTracking attribute or JS is on the page</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Stale recs with caching</td><td class=""px-4 py-2"">Output caching breaking tracking</td><td class=""px-4 py-2"">Switch to client-side tracking API (v2.1.0+)</td></tr>
    </tbody>
</table>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Testing Tip</p>
    <p class=""text-amber-800 dark:text-amber-200"">When testing recommendations in a development environment, remember that the ML algorithms need sufficient behavioural data to generate good recommendations. In a new or low-traffic environment, you may see fallback products rather than personalised suggestions until enough data has been collected.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 4: Server-to-Server API Integration

    private LearningModule BuildServerToServerAPIModule()
    {
        return new LearningModule
        {
            Id = "server-to-server-api",
            Title = "Server-to-Server API Integration",
            Description = "Implement Product Recommendations using the platform-agnostic Server-to-Server API.",
            Icon = "server",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-s2s-api-overview",
                    ModuleId = "server-to-server-api",
                    Title = "S2S API Overview",
                    Summary = "Understand the Server-to-Server API architecture, endpoints, and authentication.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the S2S API architecture and when to use it",
                        "Learn the API endpoint structure and base URLs",
                        "Know the authentication and request format requirements",
                        "Understand the JSON request/response payload structure"
                    },
                    Content = @"
<h2>Server-to-Server API Overview</h2>
<p>The Server-to-Server (S2S) API lets you integrate <strong>any website or application</strong> with Optimizely Product Recommendations. Unlike the native Commerce Connect integration, the S2S API is platform-agnostic — you can use it with any backend technology (Node.js, Python, Java, PHP, .NET, etc.).</p>

<h3>When to Use the S2S API</h3>
<ul class=""list-disc list-inside space-y-1"">
    <li>Your site is not built on Optimizely Commerce Connect</li>
    <li>You need full control over the tracking and recommendation data flow</li>
    <li>You want to integrate recommendations into a headless or custom architecture</li>
    <li>You need server-side rendering of recommendations for SEO</li>
    <li>You want to integrate with mobile apps or other non-web channels</li>
</ul>

<h3>API Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
S2S API Flow:

┌──────────────┐     ┌──────────────────┐     ┌──────────────────┐
│   Visitor    │────▶│   Your Server    │────▶│  Recommendations │
│   Browser    │     │   (Backend)      │     │     API          │
└──────────────┘     └──────────────────┘     └──────────────────┘
       │                     │                         │
  1. Page request       2. Build JSON            3. Returns JSON
                           tracking payload         with recommendations
       │                     │                         │
       ◀─────────── 4. Render page with recommendations ─┘
</pre>

<h3>Base URL and Endpoints</h3>
<p>The S2S API uses a single tracking endpoint that handles both tracking and recommendation retrieval in one request:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
POST https://recs.peerius.com/tracker/v2/{client-name}

Headers:
  Content-Type: application/json
  Accept: application/json
</pre>

<h3>Request Structure</h3>
<p>Every S2S API request follows this general structure:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""product"",          // Page type being tracked
  ""lang"": ""en"",               // Language code
  ""url"": ""https://..."",       // Current page URL
  ""user"": ""CUID-value"",       // Visitor's CUID from cookie
  ""ip"": ""203.0.113.42"",       // Visitor's IP (optional, for geolocation)

  // Page-type-specific fields
  ""refCode"": ""SKU-001"",       // Product refCode (for product pages)
  ""category"": ""Shoes > Running"", // Category (for category pages)
  ""searchTerm"": ""running shoes"", // Search query (for search pages)

  // Customer identification (optional, v1.4+)
  ""customer"": {
    ""customerID"": ""cust-12345"",
    ""segmentID"": ""premium""
  }
}
</pre>

<h3>Response Structure</h3>
<p>The API response includes recommendation data in the <code>smartRecs</code> array:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""user"": ""CUID-abc123"",
  ""smartRecs"": [
    {
      ""widget"": ""alternatives"",
      ""items"": [
        {
          ""id"": ""rec-001"",
          ""refCode"": ""SKU-67890"",
          ""title"": ""Red Running Shoes"",
          ""url"": ""https://store.com/shoes/red-running"",
          ""imageUrl"": ""https://cdn.store.com/red-shoes.jpg"",
          ""price"": 79.99,
          ""salePrice"": 64.99,
          ""category"": ""Footwear > Running""
        },
        // ... more items
      ]
    },
    {
      ""widget"": ""cross-sell"",
      ""items"": [ /* ... */ ]
    }
  ]
}
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Combined Tracking + Recommendations</p>
    <p class=""text-amber-800 dark:text-amber-200"">The S2S API combines tracking and recommendation retrieval in a single request. When you send tracking data for a page, the response automatically includes recommendations for the widgets configured for that page type.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-s2s-tracking-requests",
                    ModuleId = "server-to-server-api",
                    Title = "Tracking Requests by Page Type",
                    Summary = "Learn the specific request formats for each page type in the S2S API.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Know the request format for each page type",
                        "Understand which fields are required vs optional per page type",
                        "Learn how basket and order tracking payloads are structured",
                        "Implement variant tracking for product variants"
                    },
                    Content = @"
<h2>Tracking Requests by Page Type</h2>
<p>Each page type requires a specific set of fields in the tracking request. This lesson covers the request format for all supported page types.</p>

<h3>Home Page</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
POST /tracker/v2/your-client-name
{
  ""site"": ""your-client-name"",
  ""type"": ""home"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/"",
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Product Page</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""product"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/shoes/blue-running"",
  ""refCode"": ""SKU-SHOE-001"",
  ""category"": ""Footwear > Running > Road"",
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Category Page</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""category"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/category/running-shoes"",
  ""category"": ""Footwear > Running"",
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Search Page</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""search"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/search?q=running+shoes"",
  ""searchTerm"": ""running shoes"",
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Basket Page</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""basket"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/basket"",
  ""basket"": {
    ""items"": [
      { ""refCode"": ""SKU-SHOE-001"", ""qty"": 1, ""price"": 69.99 },
      { ""refCode"": ""SKU-SOCK-005"", ""qty"": 2, ""price"": 9.99 }
    ],
    ""total"": 89.97,
    ""currency"": ""GBP""
  },
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Order Confirmation Page</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""order"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/order/confirmation"",
  ""order"": {
    ""orderNo"": ""ORD-2025-00142"",
    ""items"": [
      { ""refCode"": ""SKU-SHOE-001"", ""qty"": 1, ""price"": 69.99 },
      { ""refCode"": ""SKU-SOCK-005"", ""qty"": 2, ""price"": 9.99 }
    ],
    ""total"": 89.97,
    ""currency"": ""GBP""
  },
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Wishlist Page</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""wishlist"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/wishlist"",
  ""wishlist"": {
    ""items"": [
      { ""refCode"": ""SKU-SHOE-001"" },
      { ""refCode"": ""SKU-JACKET-003"" }
    ]
  },
  ""user"": ""CUID-abc123""
}
</pre>

<h3>Variant Tracking</h3>
<p>When tracking product variants (size, colour, etc.), include both the parent product and variant information:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""product"",
  ""lang"": ""en"",
  ""url"": ""https://www.yourstore.com/shoes/blue-running?size=10"",
  ""refCode"": ""SKU-SHOE-001"",
  ""variant"": {
    ""refCode"": ""SKU-SHOE-001-SIZE10"",
    ""attributes"": {
      ""size"": ""10"",
      ""colour"": ""Blue""
    }
  },
  ""user"": ""CUID-abc123""
}
</pre>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Checkout Page</p>
    <p class=""text-blue-700 dark:text-blue-300"">Checkout page tracking follows the same format as basket tracking but with <code>""type"": ""checkout""</code>. It completes the conversion funnel tracking between basket and order.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-s2s-response-handling",
                    ModuleId = "server-to-server-api",
                    Title = "Response Handling and Error Codes",
                    Summary = "Learn how to process recommendation responses and handle errors from the S2S API.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Parse the smartRecs response structure",
                        "Handle empty recommendation results gracefully",
                        "Understand API error codes and their meanings",
                        "Implement robust error handling in your integration"
                    },
                    Content = @"
<h2>Response Handling and Error Codes</h2>
<p>The S2S API returns recommendation data alongside the tracking acknowledgement. Understanding the response structure is critical for correctly rendering recommendations on your pages.</p>

<h3>Successful Response Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
HTTP/1.1 200 OK
Content-Type: application/json

{
  ""user"": ""CUID-abc123"",
  ""smartRecs"": [
    {
      ""widget"": ""alternatives"",
      ""title"": ""You May Also Like"",
      ""position"": ""product-page-1"",
      ""items"": [
        {
          ""id"": ""rec-001"",
          ""refCode"": ""SKU-67890"",
          ""title"": ""Red Running Shoes"",
          ""url"": ""https://store.com/shoes/red-running"",
          ""imageUrl"": ""https://cdn.store.com/red-shoes.jpg"",
          ""price"": 79.99,
          ""salePrice"": 64.99,
          ""currency"": ""GBP"",
          ""category"": ""Footwear > Running"",
          ""brand"": ""RunFast"",
          ""attributes"": {
            ""colour"": ""Red"",
            ""rating"": ""4.3""
          }
        },
        {
          ""id"": ""rec-002"",
          ""refCode"": ""SKU-11111"",
          ""title"": ""Green Trail Shoes"",
          // ... more fields
        }
      ]
    },
    {
      ""widget"": ""cross-sell"",
      ""title"": ""Complete Your Kit"",
      ""position"": ""product-page-2"",
      ""items"": [ /* ... */ ]
    }
  ]
}
</pre>

<h3>Processing the Response</h3>
<p>Here is a typical server-side processing pattern in C#:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// C# example: Processing S2S API response
public class RecommendationResponse
{
    public string User { get; set; }
    public List&lt;WidgetResponse&gt; SmartRecs { get; set; }
}

public class WidgetResponse
{
    public string Widget { get; set; }
    public string Title { get; set; }
    public string Position { get; set; }
    public List&lt;RecommendedItem&gt; Items { get; set; }
}

public class RecommendedItem
{
    public string Id { get; set; }        // RecommendationId for click tracking
    public string RefCode { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string Currency { get; set; }
    public string Category { get; set; }
}

// Usage
var response = await httpClient.PostAsJsonAsync(apiUrl, trackingPayload);
var recsResponse = await response.Content
    .ReadFromJsonAsync&lt;RecommendationResponse&gt;();

foreach (var widget in recsResponse.SmartRecs)
{
    Console.WriteLine($""Widget: {widget.Widget} - {widget.Items.Count} items"");
    foreach (var item in widget.Items)
    {
        // Build recommendation HTML/component for each item
        // Include item.Id in link for click tracking
    }
}
</pre>

<h3>Handling Empty Recommendations</h3>
<p>A widget may return an empty <code>items</code> array if no suitable recommendations are available. Always handle this case:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Handle empty results
foreach (var widget in recsResponse.SmartRecs)
{
    if (widget.Items == null || widget.Items.Count == 0)
    {
        // Hide the widget section or show fallback content
        continue;
    }
    // Render recommendations
}
</pre>

<h3>Error Codes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">HTTP Status</th>
            <th class=""px-4 py-2 text-left"">Meaning</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">200 OK</td><td class=""px-4 py-2"">Success — tracking recorded, recommendations returned</td><td class=""px-4 py-2"">Process response normally</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">400 Bad Request</td><td class=""px-4 py-2"">Invalid request format or missing fields</td><td class=""px-4 py-2"">Check JSON payload structure</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">401 Unauthorized</td><td class=""px-4 py-2"">Invalid or missing client credentials</td><td class=""px-4 py-2"">Verify client name and API keys</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">404 Not Found</td><td class=""px-4 py-2"">Client name not found</td><td class=""px-4 py-2"">Verify the client name in the URL</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">500 Server Error</td><td class=""px-4 py-2"">Recommendations server error</td><td class=""px-4 py-2"">Retry with exponential backoff; show fallback content</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">503 Unavailable</td><td class=""px-4 py-2"">Service temporarily unavailable</td><td class=""px-4 py-2"">Retry later; show page without recommendations</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Resilience Pattern</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Always implement timeout and fallback logic for the S2S API call. Recommendations should enhance the page but never block it from loading. If the API is slow or unavailable, render the page without recommendations rather than making the visitor wait.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-s2s-cookie-management",
                    ModuleId = "server-to-server-api",
                    Title = "Cookie Management in S2S",
                    Summary = "Learn how to manage the peerius_user cookie and visitor identification in S2S integrations.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand why cookie management is your responsibility in S2S",
                        "Implement CUID cookie creation and persistence",
                        "Handle new vs returning visitors correctly",
                        "Link customer IDs for cross-device identification"
                    },
                    Content = @"
<h2>Cookie Management in S2S Integrations</h2>
<p>In the native and JavaScript integrations, the <code>peerius_user</code> cookie is managed automatically. However, with the Server-to-Server API, <strong>cookie management is your responsibility</strong>. You must read, create, and persist the CUID cookie to maintain visitor profile continuity.</p>

<h3>The peerius_user Cookie</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Cookie Name</td><td class=""px-4 py-2"">peerius_user</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Contains</td><td class=""px-4 py-2"">Customer User ID (CUID)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Scope</td><td class=""px-4 py-2"">First-party cookie on your domain</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Purpose</td><td class=""px-4 py-2"">Links visitor sessions and behavioural data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Persistence</td><td class=""px-4 py-2"">Should be set as a persistent cookie (e.g., 2 years)</td></tr>
    </tbody>
</table>

<h3>Cookie Management Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
S2S Cookie Management:

1. Incoming Request
   └── Check for peerius_user cookie
       ├── Cookie exists → Read CUID value
       └── Cookie missing → First visit
           └── Send tracking request without user field
               └── API response includes new CUID
                   └── Set peerius_user cookie with returned CUID

2. Subsequent Requests
   └── Read CUID from peerius_user cookie
       └── Include in ""user"" field of tracking request
           └── API responds with recommendations
</pre>

<h3>Implementation Example (C# / ASP.NET Core)</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
public class RecommendationService
{
    private const string CuidCookieName = ""peerius_user"";
    private readonly HttpClient _httpClient;

    public async Task&lt;RecommendationResponse&gt; TrackAndGetRecommendations(
        HttpContext httpContext, TrackingPayload payload)
    {
        // Step 1: Read existing CUID from cookie
        var cuid = httpContext.Request.Cookies[CuidCookieName];

        if (!string.IsNullOrEmpty(cuid))
        {
            payload.User = cuid;
        }

        // Step 2: Send tracking request to S2S API
        var response = await _httpClient.PostAsJsonAsync(
            ""https://recs.peerius.com/tracker/v2/your-client"",
            payload);

        var result = await response.Content
            .ReadFromJsonAsync&lt;RecommendationResponse&gt;();

        // Step 3: Store returned CUID in cookie (new or refreshed)
        if (!string.IsNullOrEmpty(result?.User))
        {
            httpContext.Response.Cookies.Append(CuidCookieName, result.User,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    MaxAge = TimeSpan.FromDays(730) // 2 years
                });
        }

        return result;
    }
}
</pre>

<h3>Customer Identification (v1.4+)</h3>
<p>From API version 1.4, you can include customer identification in tracking requests to link behaviour across devices:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""site"": ""your-client-name"",
  ""type"": ""product"",
  ""user"": ""CUID-abc123"",
  ""customer"": {
    ""customerID"": ""customer-email@example.com"",
    ""segmentID"": ""premium-members""
  },
  // ... other fields
}
</pre>
<p>The <code>customerID</code> links the anonymous CUID to a known customer identity, enabling:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>Cross-device profile merging (same customer on desktop and mobile)</li>
    <li>Email recommendation targeting (linking web behaviour to email address)</li>
    <li>Customer segment-based filtering in widget strategies</li>
</ul>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Privacy Consideration</p>
    <p class=""text-amber-800 dark:text-amber-200"">Only include the <code>customerID</code> when the visitor has authenticated and consented to personalisation. For anonymous visitors, the CUID-based tracking provides recommendations without requiring any personally identifiable information.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 5: JavaScript API Integration

    private LearningModule BuildJavaScriptAPIModule()
    {
        return new LearningModule
        {
            Id = "javascript-api",
            Title = "JavaScript API Integration",
            Description = "Implement client-side tracking and recommendation rendering using the JavaScript API.",
            Icon = "code-bracket",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-js-api-setup",
                    ModuleId = "javascript-api",
                    Title = "JavaScript API Setup",
                    Summary = "Set up the JavaScript tracking script and understand the PeeriusCallbacks architecture.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Install the JavaScript tracking script on your pages",
                        "Understand the PeeriusCallbacks variable and its role",
                        "Configure the basic tracking setup",
                        "Know when the JavaScript API is preferred over other methods"
                    },
                    Content = @"
<h2>JavaScript API Setup</h2>
<p>The JavaScript API provides a client-side integration method that handles both tracking and recommendation delivery entirely in the browser. It uses JSON-based page tracking and delivers recommendations through JavaScript callbacks.</p>

<h3>Installing the Tracking Script</h3>
<p>Add the Optimizely Recommendations tracking script to your page template, typically in the <code>&lt;head&gt;</code> or before the closing <code>&lt;/body&gt;</code> tag:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- Optimizely Product Recommendations Tracking Script --&gt;
&lt;script type=""text/javascript""
    src=""//your-client-name.peerius.com/tracker/peerius.page""
    async&gt;&lt;/script&gt;
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Script URL</p>
    <p class=""text-amber-800 dark:text-amber-200"">The tracking script URL contains your client name, provided during onboarding. The script is loaded asynchronously so it does not block page rendering.</p>
</div>

<h3>The PeeriusCallbacks Variable</h3>
<p>The <code>PeeriusCallbacks</code> JavaScript variable is the central integration point. You define it <strong>before</strong> the tracking script loads, and it provides callback functions that the script calls with tracking data and recommendations:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;script type=""text/javascript""&gt;
var PeeriusCallbacks = {

    // Called to provide tracking data for the current page
    track: {
        type: ""product"",           // Page type
        lang: ""en"",                // Language
        refCode: ""SKU-SHOE-001"",   // Product code (product pages)
        category: ""Footwear > Running > Road""
    },

    // Called when recommendations are returned
    smartRecs: function(jsonData) {
        // Process and render recommendations
        renderRecommendations(jsonData);
    },

    // Called when a recommendation is clicked
    smartRecsClick: function(itemId, widgetName) {
        // Handle click tracking
        console.log(""Clicked recommendation:"", itemId, ""in"", widgetName);
    }
};
&lt;/script&gt;

&lt;!-- Load after PeeriusCallbacks is defined --&gt;
&lt;script src=""//your-client-name.peerius.com/tracker/peerius.page""
    async&gt;&lt;/script&gt;
</pre>

<h3>Tracking Script Deployment Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Manual</td><td class=""px-4 py-2"">Add script tag directly to page templates</td><td class=""px-4 py-2"">Full control, custom builds</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">CMS NuGet</td><td class=""px-4 py-2"">Install via Optimizely CMS package</td><td class=""px-4 py-2"">Optimizely CMS sites</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Google Tag Manager</td><td class=""px-4 py-2"">Deploy via GTM container</td><td class=""px-4 py-2"">Marketing-managed deployments</td></tr>
    </tbody>
</table>

<h3>Lifecycle</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
JavaScript API Lifecycle:

1. Page starts loading
2. PeeriusCallbacks variable is defined with tracking data
3. Tracking script loads asynchronously
4. Script reads PeeriusCallbacks.track data
5. Sends tracking request to Recommendations servers
6. Receives recommendation response
7. Calls PeeriusCallbacks.smartRecs() with recommendation data
8. Your code renders recommendations in the DOM
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-js-json-tracking",
                    ModuleId = "javascript-api",
                    Title = "JSON Tracking Implementation",
                    Summary = "Implement JSON-based tracking for different page types using the JavaScript API.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Implement tracking for product, category, search, and basket pages",
                        "Structure the PeeriusCallbacks.track object for each page type",
                        "Handle dynamic page content and SPA navigation",
                        "Include customer identification in JavaScript tracking"
                    },
                    Content = @"
<h2>JSON Tracking Implementation</h2>
<p>The JavaScript API uses the <code>PeeriusCallbacks.track</code> object to send page-specific tracking data. The tracking object structure varies by page type.</p>

<h3>Product Page Tracking</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
var PeeriusCallbacks = {
    track: {
        type: ""product"",
        lang: ""en"",
        refCode: ""SKU-SHOE-001"",
        category: ""Footwear > Running > Road""
    },
    smartRecs: function(jsonData) { renderRecommendations(jsonData); }
};
</pre>

<h3>Category Page Tracking</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
var PeeriusCallbacks = {
    track: {
        type: ""category"",
        lang: ""en"",
        category: ""Footwear > Running""
    },
    smartRecs: function(jsonData) { renderRecommendations(jsonData); }
};
</pre>

<h3>Search Page Tracking</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
var PeeriusCallbacks = {
    track: {
        type: ""search"",
        lang: ""en"",
        searchTerm: ""running shoes""
    },
    smartRecs: function(jsonData) { renderRecommendations(jsonData); }
};
</pre>

<h3>Basket Page Tracking</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
var PeeriusCallbacks = {
    track: {
        type: ""basket"",
        lang: ""en"",
        basket: {
            items: [
                { refCode: ""SKU-SHOE-001"", qty: 1, price: 69.99 },
                { refCode: ""SKU-SOCK-005"", qty: 2, price: 9.99 }
            ],
            total: 89.97,
            currency: ""GBP""
        }
    },
    smartRecs: function(jsonData) { renderRecommendations(jsonData); }
};
</pre>

<h3>Order Confirmation Tracking</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
var PeeriusCallbacks = {
    track: {
        type: ""order"",
        lang: ""en"",
        order: {
            orderNo: ""ORD-2025-00142"",
            items: [
                { refCode: ""SKU-SHOE-001"", qty: 1, price: 69.99 },
                { refCode: ""SKU-SOCK-005"", qty: 2, price: 9.99 }
            ],
            total: 89.97,
            currency: ""GBP""
        }
    },
    smartRecs: function(jsonData) { /* Optional: post-purchase recs */ }
};
</pre>

<h3>Customer Identification (Optional)</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
var PeeriusCallbacks = {
    track: {
        type: ""product"",
        lang: ""en"",
        refCode: ""SKU-SHOE-001"",
        customer: {
            customerID: ""customer@example.com"",
            segmentID: ""premium""
        }
    },
    smartRecs: function(jsonData) { renderRecommendations(jsonData); }
};
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Single Page Applications (SPAs)</p>
    <p class=""text-amber-800 dark:text-amber-200"">For SPAs where the page does not fully reload on navigation, you need to re-trigger tracking when the route changes. Update the <code>PeeriusCallbacks.track</code> object and call the tracking function manually after each route change.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-js-recommendations-callbacks",
                    ModuleId = "javascript-api",
                    Title = "Recommendations with Callbacks",
                    Summary = "Process recommendation responses and render personalised product suggestions.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Process the smartRecs callback data structure",
                        "Render recommendation widgets dynamically",
                        "Implement click tracking in JavaScript",
                        "Handle multiple widgets on a single page"
                    },
                    Content = @"
<h2>Recommendations with Callbacks</h2>
<p>When the tracking script sends data to the Recommendations servers, the response triggers the <code>PeeriusCallbacks.smartRecs</code> callback with recommendation data. This is where you render the personalised product suggestions.</p>

<h3>The smartRecs Callback Data</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Structure passed to PeeriusCallbacks.smartRecs
{
  ""smartRecs"": [
    {
      ""widget"": ""alternatives"",
      ""title"": ""You May Also Like"",
      ""items"": [
        {
          ""id"": ""rec-001"",           // RecommendationId for click tracking
          ""refCode"": ""SKU-67890"",
          ""title"": ""Red Running Shoes"",
          ""url"": ""https://store.com/shoes/red-running"",
          ""imageUrl"": ""https://cdn.store.com/red-shoes.jpg"",
          ""price"": 79.99,
          ""salePrice"": 64.99,
          ""currency"": ""GBP"",
          ""category"": ""Footwear > Running"",
          ""attributes"": { ""colour"": ""Red"", ""brand"": ""RunFast"" }
        },
        // ... more items
      ]
    },
    {
      ""widget"": ""cross-sell"",
      ""title"": ""Complete Your Kit"",
      ""items"": [ /* ... */ ]
    }
  ]
}
</pre>

<h3>Rendering Recommendations</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
var PeeriusCallbacks = {
    track: { type: ""product"", refCode: ""SKU-SHOE-001"" },

    smartRecs: function(jsonData) {
        if (!jsonData || !jsonData.smartRecs) return;

        jsonData.smartRecs.forEach(function(widget) {
            var container = document.getElementById(""recs-"" + widget.widget);
            if (!container) return;

            // Set widget title
            var titleEl = container.querySelector("".widget-title"");
            if (titleEl) titleEl.textContent = widget.title;

            // Render each recommended item
            var itemsHtml = widget.items.map(function(item) {
                var displayPrice = item.salePrice || item.price;
                var hasDiscount = item.salePrice &amp;&amp; item.salePrice &lt; item.price;

                return '&lt;a href=""' + item.url +
                    '?recommendationId=' + item.id +
                    '"" class=""rec-item"" data-rec-id=""' + item.id + '""&gt;' +
                    '  &lt;img src=""' + item.imageUrl + '"" alt=""' + item.title + '"" /&gt;' +
                    '  &lt;h4&gt;' + item.title + '&lt;/h4&gt;' +
                    (hasDiscount ?
                        '  &lt;span class=""price-was""&gt;' + item.currency + ' ' + item.price + '&lt;/span&gt;' : '') +
                    '  &lt;span class=""price""&gt;' + item.currency + ' ' + displayPrice + '&lt;/span&gt;' +
                    '&lt;/a&gt;';
            }).join('');

            container.querySelector("".widget-items"").innerHTML = itemsHtml;
            container.style.display = ""block"";
        });
    }
};
</pre>

<h3>Click Tracking</h3>
<p>Implement click tracking to measure which recommendations drive engagement:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Add click event listeners for recommendation items
document.addEventListener(""click"", function(e) {
    var recItem = e.target.closest("".rec-item"");
    if (recItem) {
        var recId = recItem.getAttribute(""data-rec-id"");
        // The tracking script automatically handles click tracking
        // if the recommendationId is in the URL query string
        console.log(""Recommendation clicked:"", recId);
    }
});
</pre>

<h3>HTML Container Structure</h3>
<p>Prepare containers in your page templates for each widget:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- Alternatives widget container --&gt;
&lt;div id=""recs-alternatives"" class=""recommendation-widget"" style=""display:none""&gt;
    &lt;h3 class=""widget-title""&gt;&lt;/h3&gt;
    &lt;div class=""widget-items""&gt;&lt;/div&gt;
&lt;/div&gt;

&lt;!-- Cross-sell widget container --&gt;
&lt;div id=""recs-cross-sell"" class=""recommendation-widget"" style=""display:none""&gt;
    &lt;h3 class=""widget-title""&gt;&lt;/h3&gt;
    &lt;div class=""widget-items""&gt;&lt;/div&gt;
&lt;/div&gt;
</pre>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">No Duplicate Recommendations</p>
    <p class=""text-blue-700 dark:text-blue-300"">Product Recommendations generates recommendations for widgets sequentially. If a product appears in the first widget on a page, it will not appear in any subsequent widgets. This prevents duplicate recommendations across widgets on the same page.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-js-client-side-rendering",
                    ModuleId = "javascript-api",
                    Title = "Client-Side Rendering Patterns",
                    Summary = "Explore advanced rendering patterns including templates, responsive layouts, and carousel widgets.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Implement template-based rendering for recommendations",
                        "Create responsive recommendation layouts",
                        "Build carousel/slider widgets for recommendations",
                        "Handle loading states and progressive enhancement"
                    },
                    Content = @"
<h2>Client-Side Rendering Patterns</h2>
<p>Beyond basic rendering, there are several patterns to create polished, production-ready recommendation widgets. This lesson covers templates, responsive layouts, and common UI patterns.</p>

<h3>Template-Based Rendering</h3>
<p>Use a template approach for cleaner, more maintainable rendering:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Recommendation item template
function renderItem(item) {
    var template = document.getElementById(""rec-item-template"");
    var clone = template.content.cloneNode(true);

    clone.querySelector("".rec-image"").src = item.imageUrl;
    clone.querySelector("".rec-image"").alt = item.title;
    clone.querySelector("".rec-title"").textContent = item.title;
    clone.querySelector("".rec-price"").textContent =
        item.currency + "" "" + (item.salePrice || item.price).toFixed(2);
    clone.querySelector("".rec-link"").href =
        item.url + ""?recommendationId="" + item.id;

    // Show sale badge if discounted
    if (item.salePrice &amp;&amp; item.salePrice &lt; item.price) {
        clone.querySelector("".rec-badge"").style.display = ""block"";
        clone.querySelector("".rec-original-price"").textContent =
            item.currency + "" "" + item.price.toFixed(2);
    }

    return clone;
}
</pre>

<h3>HTML Template</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;template id=""rec-item-template""&gt;
    &lt;a class=""rec-link rec-card""&gt;
        &lt;div class=""rec-image-container""&gt;
            &lt;img class=""rec-image"" src="""" alt="""" loading=""lazy"" /&gt;
            &lt;span class=""rec-badge"" style=""display:none""&gt;Sale&lt;/span&gt;
        &lt;/div&gt;
        &lt;div class=""rec-details""&gt;
            &lt;h4 class=""rec-title""&gt;&lt;/h4&gt;
            &lt;div class=""rec-pricing""&gt;
                &lt;span class=""rec-original-price""&gt;&lt;/span&gt;
                &lt;span class=""rec-price""&gt;&lt;/span&gt;
            &lt;/div&gt;
        &lt;/div&gt;
    &lt;/a&gt;
&lt;/template&gt;
</pre>

<h3>Responsive Grid Layout</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
/* Responsive recommendation grid */
.recommendation-widget {
    padding: 1.5rem 0;
}

.widget-title {
    font-size: 1.25rem;
    font-weight: 600;
    margin-bottom: 1rem;
}

.widget-items {
    display: grid;
    gap: 1rem;
    grid-template-columns: repeat(2, 1fr);   /* Mobile: 2 columns */
}

@media (min-width: 768px) {
    .widget-items {
        grid-template-columns: repeat(3, 1fr); /* Tablet: 3 columns */
    }
}

@media (min-width: 1024px) {
    .widget-items {
        grid-template-columns: repeat(4, 1fr); /* Desktop: 4 columns */
    }
}

.rec-card {
    border: 1px solid #e5e7eb;
    border-radius: 0.5rem;
    overflow: hidden;
    text-decoration: none;
    color: inherit;
    transition: box-shadow 0.2s;
}

.rec-card:hover {
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}
</pre>

<h3>Loading State Pattern</h3>
<p>Show skeleton placeholders while recommendations load:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Show loading skeletons initially
function showLoadingState(containerId, count) {
    var container = document.getElementById(containerId);
    var skeletons = '';
    for (var i = 0; i &lt; count; i++) {
        skeletons += '&lt;div class=""rec-skeleton""&gt;' +
            '&lt;div class=""skeleton-image""&gt;&lt;/div&gt;' +
            '&lt;div class=""skeleton-text""&gt;&lt;/div&gt;' +
            '&lt;div class=""skeleton-text short""&gt;&lt;/div&gt;' +
            '&lt;/div&gt;';
    }
    container.querySelector("".widget-items"").innerHTML = skeletons;
    container.style.display = ""block"";
}

// Call before tracking script loads
showLoadingState(""recs-alternatives"", 4);
showLoadingState(""recs-cross-sell"", 4);
</pre>

<h3>Server-Side vs Client-Side Rendering</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Server-Side</th>
            <th class=""px-4 py-2 text-left"">Client-Side (JS API)</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">SEO</td><td class=""px-4 py-2"">Content crawlable by search engines</td><td class=""px-4 py-2"">Not indexed (rendered after JS)</td></tr>
        <tr><td class=""px-4 py-2"">Performance</td><td class=""px-4 py-2"">Adds server processing time</td><td class=""px-4 py-2"">Non-blocking, async loading</td></tr>
        <tr><td class=""px-4 py-2"">Caching</td><td class=""px-4 py-2"">Requires careful cache strategy</td><td class=""px-4 py-2"">Works with any caching</td></tr>
        <tr><td class=""px-4 py-2"">Personalisation</td><td class=""px-4 py-2"">Immediate on first render</td><td class=""px-4 py-2"">Brief delay while JS loads</td></tr>
    </tbody>
</table>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Progressive Enhancement</p>
    <p class=""text-amber-800 dark:text-amber-200"">Always design recommendation widgets as an enhancement to the page. If JavaScript is disabled or the API is unavailable, the page should still function normally — recommendation containers should be hidden by default and only shown when data is available.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 6: Widgets & Strategies

    private LearningModule BuildWidgetsStrategiesModule()
    {
        return new LearningModule
        {
            Id = "widgets-strategies",
            Title = "Widgets & Strategies",
            Description = "Configure recommendation widgets and define strategies that control which products are recommended.",
            Icon = "squares-2x2",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-understanding-widgets",
                    ModuleId = "widgets-strategies",
                    Title = "Understanding Widgets",
                    Summary = "Learn what widgets are, how they display recommendations, and how sequential generation works.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what a recommendation widget is",
                        "Learn how widgets relate to strategies and algorithms",
                        "Know how sequential widget generation prevents duplicates",
                        "Understand widget placement on different page types"
                    },
                    Content = @"
<h2>Understanding Widgets</h2>
<p>A widget is a <strong>configurable screen element that displays product recommendations</strong>, either on a web page or in an email message. Widgets are the visible output of the Recommendations engine — they are where visitors actually see and interact with personalised product suggestions.</p>

<h3>Widget Anatomy</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────┐
│  Widget: ""You May Also Like""                            │
│  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐   │
│  │  [img]  │  │  [img]  │  │  [img]  │  │  [img]  │   │
│  │ Product │  │ Product │  │ Product │  │ Product │   │
│  │  £69.99 │  │  £49.99 │  │  £89.99 │  │  £34.99 │   │
│  └─────────┘  └─────────┘  └─────────┘  └─────────┘   │
└─────────────────────────────────────────────────────────┘
         ▲               ▲               ▲
    Algorithm 1     Algorithm 2     Fallback Set
    (Viewed Together) (Same Category)  (Best Sellers)
</pre>

<h3>Widget Components</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Name</td><td class=""px-4 py-2"">Widget identifier used in API responses (e.g., ""alternatives"", ""cross-sell"")</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Title</td><td class=""px-4 py-2"">Display title shown to visitors (e.g., ""You May Also Like"")</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Number of Recommendations</td><td class=""px-4 py-2"">Total products the widget should display (e.g., 4, 6, 8)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Channel</td><td class=""px-4 py-2"">Where the widget is available (web, email, or both)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Position</td><td class=""px-4 py-2"">Where on the page the widget appears</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Strategy</td><td class=""px-4 py-2"">Stack of algorithms that determine which products to recommend</td></tr>
    </tbody>
</table>

<h3>Sequential Widget Generation</h3>
<p>A critical feature of Product Recommendations is <strong>sequential widget generation</strong>. When multiple widgets are configured for the same page:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Recommendations are generated for the <strong>first widget</strong> in the list</li>
    <li>Then the <strong>second widget</strong>, and so on</li>
    <li>For a single page view, across all widgets, <strong>there are no duplicated recommendations</strong></li>
    <li>If a product is recommended for the first widget, it cannot appear in any other widget on that page</li>
</ol>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Widget Order Matters</p>
    <p class=""text-amber-800 dark:text-amber-200"">Because widgets are processed sequentially, the order in which you arrange them determines priority. The first widget gets the best algorithm matches; subsequent widgets get products that the earlier widgets did not claim. Arrange widgets with your highest-priority strategy first.</p>
</div>

<h3>Pre-Defined Widgets</h3>
<p>Optimizely provides a set of pre-defined widgets that cover common recommendation scenarios. These can be edited and customised in the Personalization Portal:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Alternatives</strong> — Products from the same category as the viewed product</li>
    <li><strong>Cross-Sell</strong> — Products from different categories that complement the viewed product</li>
    <li><strong>Recently Viewed</strong> — Products the visitor has previously viewed</li>
    <li><strong>Popular Products</strong> — Best-selling products across the site</li>
    <li><strong>Trending</strong> — Products gaining popularity over a recent time period</li>
    <li><strong>Personalised</strong> — Products tailored to the individual visitor's behaviour profile</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-widget-configuration",
                    ModuleId = "widgets-strategies",
                    Title = "Widget Configuration",
                    Summary = "Learn how to configure widgets in the Personalization Portal including activation and placement.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure widget properties in the Personalization Portal",
                        "Activate widgets on specific page types",
                        "Arrange widget order for optimal sequential generation",
                        "Understand the widget activation workflow"
                    },
                    Content = @"
<h2>Widget Configuration</h2>
<p>Widgets are configured in the Personalization Portal under <strong>Configuration &gt; Widgets</strong>. This lesson covers the configuration options and activation process.</p>

<h3>Widget Configuration Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Widget Name</td><td class=""px-4 py-2"">Internal identifier (used in API responses)</td><td class=""px-4 py-2"">alternatives, cross-sell</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Display Title</td><td class=""px-4 py-2"">Title shown to visitors</td><td class=""px-4 py-2"">""You May Also Like""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Number of Recommendations</td><td class=""px-4 py-2"">Total products to return</td><td class=""px-4 py-2"">4, 6, 8</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Channel</td><td class=""px-4 py-2"">Where widget is available</td><td class=""px-4 py-2"">Web, Email, Both</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recommendation Expected</td><td class=""px-4 py-2"">Whether the widget must always return results</td><td class=""px-4 py-2"">Yes / No</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Position</td><td class=""px-4 py-2"">Page placement identifier</td><td class=""px-4 py-2"">product-page-1, basket-sidebar</td></tr>
    </tbody>
</table>

<h3>Activating Widgets on Pages</h3>
<p>After configuring a widget, you need to activate it on specific page types:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Configuration &gt; Activation</strong> in the Portal</li>
    <li>Select the page type (Home, Category, Product, Search, Basket, etc.)</li>
    <li>Search for and select the widgets you want to activate on that page</li>
    <li>Drag and drop widgets to arrange their order (determines sequential processing priority)</li>
    <li>Save the activation configuration</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Widget Activation Example - Product Detail Page:

Page Type: Product
┌────────────────────────────────────────────┐
│  1. alternatives     (""You May Also Like"")  │  ← Processed first
│  2. cross-sell       (""Complete Your Kit"")   │  ← Processed second
│  3. recently-viewed  (""Recently Viewed"")     │  ← Processed third
└────────────────────────────────────────────┘

Products in widget 1 will NOT appear in widgets 2 or 3.
</pre>

<h3>Default Page Activation</h3>
<p>The default set of 6 widgets are typically activated across these page types:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Page Type</th>
            <th class=""px-4 py-2 text-left"">Typical Widgets</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Home</td><td class=""px-4 py-2"">Personalised, Popular Products, Trending</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category</td><td class=""px-4 py-2"">Category Best Sellers, Trending in Category</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product (PDP)</td><td class=""px-4 py-2"">Alternatives, Cross-Sell, Recently Viewed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Search</td><td class=""px-4 py-2"">Search-Relevant, Popular for Search Term</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Basket</td><td class=""px-4 py-2"">Complementary Products, Frequently Bought Together</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Activation Changes</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Widget activation changes take effect immediately — there is no deployment step. However, changes only apply to new page views; existing cached responses will show the previous configuration until the cache expires.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-default-strategies",
                    ModuleId = "widgets-strategies",
                    Title = "Default Strategies by Page Type",
                    Summary = "Understand the default recommendation strategies configured for each page type.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Know the default strategy for each page type",
                        "Understand how strategies differ for new vs returning visitors",
                        "Learn the rationale behind each default strategy",
                        "Know when and how to customise default strategies"
                    },
                    Content = @"
<h2>Default Strategies by Page Type</h2>
<p>Product Recommendations comes with pre-configured strategies for each page type. These defaults provide a solid foundation that can be customised based on your business goals.</p>

<h3>Home Page Strategy</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Returning Visitors</h4>
    <p>Products based on previous browsing and purchasing behaviour. The ML engine selects products that match the visitor's interest profile.</p>
    <h4 class=""font-semibold mt-3"">New Visitors</h4>
    <p>Popular products across the site. Since there is no behavioural data yet, the system shows best-selling or trending products.</p>
</div>

<h3>Category Page Strategy</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p>Recommend products from the <strong>same category</strong> that the visitor is currently browsing. Algorithms prioritise products that are popular within this category and match the visitor's broader interest profile.</p>
</div>

<h3>Product Display Page (PDP) Strategy</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Alternatives Widget</h4>
    <p>Products from the <strong>same category</strong> as the currently viewed product. These are alternative options the visitor might prefer.</p>
    <h4 class=""font-semibold mt-3"">Cross-Sell Widget</h4>
    <p>Products from <strong>different categories</strong> that complement the viewed product. Based on crowd purchasing patterns — what other customers bought alongside this product.</p>
</div>

<h3>Search Page Strategy</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p>Recommendations that match the <strong>search terms</strong> entered by the visitor. If the search term maps to products, those are prioritised. For generic searches, recommendations fall back to user relevance.</p>
</div>

<h3>Basket Page Strategy</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Complementary products</strong> based on the items in the visitor's basket. This strategy is primarily driven by crowd purchasing behaviour — products that other customers frequently bought alongside the same basket items.</p>
</div>

<h3>Strategy Comparison</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Page Type</th>
            <th class=""px-4 py-2 text-left"">Primary Signal</th>
            <th class=""px-4 py-2 text-left"">Goal</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Home</td><td class=""px-4 py-2"">Visitor profile / popularity</td><td class=""px-4 py-2"">Engage and guide browsing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category</td><td class=""px-4 py-2"">Category context</td><td class=""px-4 py-2"">Help discover within category</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">PDP (Alt)</td><td class=""px-4 py-2"">Same category products</td><td class=""px-4 py-2"">Offer alternatives</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">PDP (X-Sell)</td><td class=""px-4 py-2"">Crowd purchase data</td><td class=""px-4 py-2"">Increase basket size</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Search</td><td class=""px-4 py-2"">Search terms + profile</td><td class=""px-4 py-2"">Aid product discovery</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Basket</td><td class=""px-4 py-2"">Basket contents + crowd data</td><td class=""px-4 py-2"">Increase order value</td></tr>
    </tbody>
</table>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Customisation</p>
    <p class=""text-amber-800 dark:text-amber-200"">Default strategies are a starting point. You can customise them in the Personalization Portal by modifying the algorithm stack, adding filters, or creating entirely new strategies. Use performance reports to identify which strategies drive the best results for your specific business.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-algorithm-stack",
                    ModuleId = "widgets-strategies",
                    Title = "Algorithm Stack and Fallbacks",
                    Summary = "Learn how algorithms are stacked within a strategy and how fallback logic ensures recommendations always appear.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand how algorithms are stacked in a strategy",
                        "Know the sequential evaluation process",
                        "Learn about fallback product sets",
                        "Configure algorithm limits and priorities"
                    },
                    Content = @"
<h2>Algorithm Stack and Fallbacks</h2>
<p>A strategy comprises multiple algorithms arranged in a <strong>stack formation</strong>. The Recommendations engine evaluates algorithms sequentially, using a waterfall approach to fill the required number of recommendation slots.</p>

<h3>How Algorithm Stacking Works</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Strategy: ""Product Page Alternatives"" (8 products needed)

Algorithm Stack:
┌──────────────────────────────────────────────────┐
│  1. ""Frequently Viewed Together""  (max: 3)       │
│     └── Found 3 products ✓                       │
│                                                   │
│  2. ""Same Category Best Sellers""  (max: 3)       │
│     └── Found 3 products ✓                       │
│                                                   │
│  3. ""Recently Trending in Category"" (max: 4)     │
│     └── Found 2 products (only 2 needed) ✓       │
│                                                   │
│  Total: 8 products filled ✓                      │
│                                                   │
│  4. Fallback: ""Site-Wide Best Sellers""           │
│     └── Not needed (all slots filled)            │
└──────────────────────────────────────────────────┘
</pre>

<h3>Algorithm Evaluation Rules</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>The engine starts with <strong>Algorithm 1</strong> and attempts to fill its maximum product count</li>
    <li>If Algorithm 1 cannot find enough products, the engine moves to <strong>Algorithm 2</strong></li>
    <li>This continues down the stack until the widget's total product count is reached</li>
    <li>Each algorithm can specify a <strong>maximum number of products</strong> it contributes</li>
    <li>If all algorithms combined cannot fill the widget, the <strong>fallback product set</strong> fills remaining slots</li>
    <li>You can add a <strong>maximum of 11 algorithms</strong> to a single widget</li>
</ol>

<h3>Configuring Algorithm Limits</h3>
<p>For each algorithm in the stack, you configure:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Maximum Products</td><td class=""px-4 py-2"">Maximum number of products this algorithm can contribute</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Quick Filters</td><td class=""px-4 py-2"">Pre-built filter criteria (up to 10 per algorithm)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Advanced Filters</td><td class=""px-4 py-2"">Custom filter logic (Visual Mode or Code Mode)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Fallback Product Set</td><td class=""px-4 py-2"">Products to use if the algorithm returns no results</td></tr>
    </tbody>
</table>

<h3>Fallback Product Sets</h3>
<p>Every algorithm <strong>must have a fallback product set</strong> to guarantee the widget always returns recommendations. Common fallback strategies:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Site-Wide Best Sellers</strong> — Most popular products across the entire catalog</li>
    <li><strong>Category Best Sellers</strong> — Most popular products within the relevant category</li>
    <li><strong>New Arrivals</strong> — Most recently added products</li>
    <li><strong>Curated List</strong> — Manually selected products for specific campaigns</li>
</ul>

<h3>Stack Design Best Practices</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Most specific first</strong> — Place algorithms with the most personalised/specific logic at the top of the stack</li>
        <li><strong>Broaden as you go down</strong> — Each subsequent algorithm should be broader, acting as a fallback for the one above</li>
        <li><strong>Limit top algorithms</strong> — Set conservative max counts for top algorithms to allow lower ones to contribute variety</li>
        <li><strong>Always have a fallback</strong> — The final entry should be a broad, reliable fallback that always returns products</li>
        <li><strong>Test combinations</strong> — Use the Portal's preview feature to test how different stacks perform</li>
    </ul>
</div>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Algorithm Diversity</p>
    <p class=""text-blue-700 dark:text-blue-300"">A well-designed algorithm stack balances personalisation with discovery. Include at least one personalised algorithm (based on visitor behaviour) and one popularity-based algorithm (based on crowd data) to ensure a mix of targeted and broadly appealing recommendations.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 7: Algorithms & Filters

    private LearningModule BuildAlgorithmsFiltersModule()
    {
        return new LearningModule
        {
            Id = "algorithms-filters",
            Title = "Algorithms & Filters",
            Description = "Understand the recommendation algorithms and learn to apply filters for precise product targeting.",
            Icon = "funnel",
            Order = 7,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-algorithm-types",
                    ModuleId = "algorithms-filters",
                    Title = "Algorithm Types and Logic",
                    Summary = "Explore the different algorithm types developed by Optimizely's data scientists and understand their logic.",
                    Order = 1,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Know the main algorithm categories available",
                        "Understand the logic behind each algorithm type",
                        "Learn which page types each algorithm supports",
                        "Choose the right algorithms for your business goals"
                    },
                    Content = @"
<h2>Algorithm Types and Logic</h2>
<p>Algorithms are the intelligence behind Product Recommendations. Each algorithm has <strong>pre-defined logic</strong> developed by Optimizely's data scientists and machine learning experts. They determine which products a widget recommends based on different signals.</p>

<h3>Algorithm Categories</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Category</th>
            <th class=""px-4 py-2 text-left"">Signal</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Behavioural</td><td class=""px-4 py-2"">Individual visitor actions</td><td class=""px-4 py-2"">Based on what this specific visitor has viewed, searched, or purchased</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Collaborative</td><td class=""px-4 py-2"">Crowd purchasing patterns</td><td class=""px-4 py-2"">Based on what other visitors with similar behaviour purchased</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Content-Based</td><td class=""px-4 py-2"">Product attributes</td><td class=""px-4 py-2"">Based on product category, brand, or attribute similarity</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Popularity</td><td class=""px-4 py-2"">Aggregate site data</td><td class=""px-4 py-2"">Based on overall sales volume, trending metrics, or recency</td></tr>
    </tbody>
</table>

<h3>Common Algorithm Types</h3>

<h4>Behavioural Algorithms</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Algorithm</th>
            <th class=""px-4 py-2 text-left"">Logic</th>
            <th class=""px-4 py-2 text-left"">Page Types</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Recently Viewed</td><td class=""px-4 py-2"">Products this visitor has previously viewed</td><td class=""px-4 py-2"">All pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Personalised for Visitor</td><td class=""px-4 py-2"">Products matching the visitor's interest profile</td><td class=""px-4 py-2"">Home, Category</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Abandoned Basket</td><td class=""px-4 py-2"">Products the visitor added to basket but did not purchase</td><td class=""px-4 py-2"">Home, Category</td></tr>
    </tbody>
</table>

<h4>Collaborative Algorithms</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Algorithm</th>
            <th class=""px-4 py-2 text-left"">Logic</th>
            <th class=""px-4 py-2 text-left"">Page Types</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Frequently Bought Together</td><td class=""px-4 py-2"">Products commonly purchased with the current product</td><td class=""px-4 py-2"">Product, Basket, Checkout</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Frequently Viewed Together</td><td class=""px-4 py-2"">Products commonly viewed in the same session</td><td class=""px-4 py-2"">Product</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Visitors Also Bought</td><td class=""px-4 py-2"">Products purchased by visitors who bought this product</td><td class=""px-4 py-2"">Product, Order</td></tr>
    </tbody>
</table>

<h4>Content-Based Algorithms</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Algorithm</th>
            <th class=""px-4 py-2 text-left"">Logic</th>
            <th class=""px-4 py-2 text-left"">Page Types</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Same Category</td><td class=""px-4 py-2"">Products in the same category as the viewed product</td><td class=""px-4 py-2"">Product, Category</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Different Category</td><td class=""px-4 py-2"">Products from different categories (cross-sell)</td><td class=""px-4 py-2"">Product, Basket</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Same Brand</td><td class=""px-4 py-2"">Products from the same brand</td><td class=""px-4 py-2"">Product, Brand</td></tr>
    </tbody>
</table>

<h4>Popularity Algorithms</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Algorithm</th>
            <th class=""px-4 py-2 text-left"">Logic</th>
            <th class=""px-4 py-2 text-left"">Page Types</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Best Sellers</td><td class=""px-4 py-2"">Products with highest sales volume</td><td class=""px-4 py-2"">All pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Trending</td><td class=""px-4 py-2"">Products gaining popularity over a recent period</td><td class=""px-4 py-2"">All pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">New Arrivals</td><td class=""px-4 py-2"">Most recently added products</td><td class=""px-4 py-2"">All pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category Best Sellers</td><td class=""px-4 py-2"">Top sellers within a specific category</td><td class=""px-4 py-2"">Category, Product</td></tr>
    </tbody>
</table>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Algorithm Selection</p>
    <p class=""text-amber-800 dark:text-amber-200"">Choose algorithms that align with your business goals. For maximising basket size, prioritise collaborative algorithms (Frequently Bought Together). For helping visitors discover alternatives, use content-based algorithms (Same Category). For new visitors with no history, popularity algorithms provide a reliable starting point.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-filters",
                    ModuleId = "algorithms-filters",
                    Title = "Quick Filters and Advanced Filters",
                    Summary = "Apply filters to algorithms for precise control over which products can be recommended.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Apply quick filters to narrow algorithm results",
                        "Build advanced filters using Visual Mode",
                        "Write custom filter logic in Code Mode",
                        "Understand filter evaluation and performance impact"
                    },
                    Content = @"
<h2>Quick Filters and Advanced Filters</h2>
<p>Filters allow you to refine which products an algorithm can recommend. They are applied after the algorithm identifies candidate products but before the results are returned to the widget.</p>

<h3>Quick Filters</h3>
<p>Quick filters are pre-built filter criteria that can be applied to any algorithm. You can add <strong>up to 10 quick filters</strong> per algorithm.</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Filter Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Category Filter</td><td class=""px-4 py-2"">Restrict to specific categories</td><td class=""px-4 py-2"">Only recommend from ""Footwear""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Brand Filter</td><td class=""px-4 py-2"">Include or exclude brands</td><td class=""px-4 py-2"">Only recommend ""Nike"" products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Price Range</td><td class=""px-4 py-2"">Min/max price boundaries</td><td class=""px-4 py-2"">Only products between £20-£100</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">In Stock</td><td class=""px-4 py-2"">Only recommend in-stock items</td><td class=""px-4 py-2"">Stock > 0</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">On Sale</td><td class=""px-4 py-2"">Only products with a sale price</td><td class=""px-4 py-2"">salePrice < unitPrice</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Custom Attribute</td><td class=""px-4 py-2"">Filter by feed custom attributes</td><td class=""px-4 py-2"">colour = ""Blue""</td></tr>
    </tbody>
</table>

<h3>Advanced Filters</h3>
<p>For more complex filtering logic, use Advanced Filters which can be built in two modes:</p>

<h4>Visual Mode</h4>
<p>A drag-and-drop interface for building filter conditions without code:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Visual Mode Filter Example:

IF product.category EQUALS ""Footwear > Running""
AND product.price GREATER_THAN 50
AND product.brand NOT_EQUALS ""Budget Brand""
THEN include in recommendations
</pre>

<h4>Code Mode</h4>
<p>Write filter expressions directly for maximum flexibility:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Code Mode filter examples

// Only recommend products with margin above 30%
item.attribute(""margin"") > 30

// Recommend products in same category but different brand
item.category == context.category AND item.brand != context.brand

// Complex multi-condition filter
(item.price >= 20 AND item.price <= 100)
  AND item.attribute(""season"") == ""Spring/Summer""
  AND item.stock > 5
</pre>

<h3>Filter Operators</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Value Type</th>
            <th class=""px-4 py-2 text-left"">Available Operators</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Text (string)</td><td class=""px-4 py-2"">equals, NOT equals, contains, starts with</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Numeric (price, stock)</td><td class=""px-4 py-2"">equals, NOT equals, greater than, less than, between</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Boolean</td><td class=""px-4 py-2"">equals true, equals false</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Date</td><td class=""px-4 py-2"">before, after, between</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Filter Impact</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Overly restrictive filters can reduce the number of available recommendations, forcing the engine to use fallback products more often. Strike a balance between relevance and availability — test your filters to ensure they do not eliminate too many products from the candidate pool.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-ab-testing",
                    ModuleId = "algorithms-filters",
                    Title = "A/B Testing Recommendations",
                    Summary = "Test different recommendation strategies to measure their impact on business KPIs.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand how to A/B test recommendation strategies",
                        "Set up split tests in the Personalization Portal",
                        "Measure the impact of different algorithm stacks",
                        "Interpret A/B test results for recommendation optimisation"
                    },
                    Content = @"
<h2>A/B Testing Recommendations</h2>
<p>A/B testing allows you to compare different recommendation strategies to determine which drives better business outcomes. You can test different algorithm stacks, filter configurations, and widget placements to optimise your recommendations.</p>

<h3>What Can You Test?</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Test Variable</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Algorithm Stack</td><td class=""px-4 py-2"">""Frequently Bought Together"" first vs ""Same Category Best Sellers"" first</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Number of Products</td><td class=""px-4 py-2"">4 recommendations vs 8 recommendations per widget</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Filter Rules</td><td class=""px-4 py-2"">Same category only vs cross-category recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Widget Placement</td><td class=""px-4 py-2"">Below product description vs sidebar placement</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Personalisation Level</td><td class=""px-4 py-2"">Personalised recommendations vs popular products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">With vs Without Recs</td><td class=""px-4 py-2"">Pages with recommendations vs control group without</td></tr>
    </tbody>
</table>

<h3>Setting Up an A/B Test</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
A/B Test Setup:

┌─────────────────────────────────────────────────┐
│  Test: ""PDP Cross-Sell Strategy Optimisation""   │
│                                                  │
│  Variant A (50% of traffic):                     │
│    Algorithm Stack:                              │
│    1. Frequently Bought Together (max: 4)        │
│    2. Same Category Best Sellers (max: 4)        │
│    Fallback: Site Best Sellers                   │
│                                                  │
│  Variant B (50% of traffic):                     │
│    Algorithm Stack:                              │
│    1. Visitors Also Bought (max: 4)              │
│    2. Trending in Category (max: 4)              │
│    Fallback: New Arrivals                        │
│                                                  │
│  KPIs: Click-Through Rate, Revenue per Visit,    │
│         Add-to-Cart Rate, Conversion Rate        │
│                                                  │
│  Duration: 2-4 weeks (or until statistical       │
│            significance is reached)              │
└─────────────────────────────────────────────────┘
</pre>

<h3>Measuring Results</h3>
<p>Key metrics to compare across test variants:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">What It Measures</th>
            <th class=""px-4 py-2 text-left"">Higher Is Better?</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Click-Through Rate (CTR)</td><td class=""px-4 py-2"">% of visitors who click a recommendation</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Revenue per Visit</td><td class=""px-4 py-2"">Average revenue attributed to recommendations</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Add-to-Cart Rate</td><td class=""px-4 py-2"">% of recommendation clicks that result in basket additions</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Conversion Rate</td><td class=""px-4 py-2"">% of recommendation interactions leading to purchase</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Average Order Value</td><td class=""px-4 py-2"">Average order total for recommendation-influenced purchases</td><td class=""px-4 py-2"">Yes</td></tr>
    </tbody>
</table>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Statistical Significance</p>
    <p class=""text-amber-800 dark:text-amber-200"">Run A/B tests until you have statistically significant results. For most e-commerce sites, this means at least 2-4 weeks of data with sufficient traffic volume. Making decisions on insufficient data can lead to choosing a worse-performing variant.</p>
</div>

<h3>JavaScript API A/B Testing</h3>
<p>The JavaScript API supports A/B testing through its integration. The tracking script can assign visitors to test groups and report variant-specific results back to the Personalization Portal.</p>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 8: Merchandising & Campaigns

    private LearningModule BuildMerchandisingCampaignsModule()
    {
        return new LearningModule
        {
            Id = "merchandising-campaigns",
            Title = "Merchandising & Campaigns",
            Description = "Create merchandising rules and campaigns to refine or override algorithmic recommendations.",
            Icon = "tag",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-merchandising-rules",
                    ModuleId = "merchandising-campaigns",
                    Title = "Merchandising Rules",
                    Summary = "Learn how to create merchandising rules that refine or override algorithmic recommendations.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the difference between merchandising and hand-pick rules",
                        "Know how master rules scope campaign effects",
                        "Create merchandising rules in the Personalization Portal",
                        "Understand rule priority and evaluation order"
                    },
                    Content = @"
<h2>Merchandising Rules</h2>
<p>Merchandising rules let you <strong>refine or override</strong> the product recommendations generated by algorithms. They give business users control over what products appear in recommendations, enabling them to execute marketing campaigns, promote specific products, or enforce business rules.</p>

<h3>Rule Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Rule Type</th>
            <th class=""px-4 py-2 text-left"">Behaviour</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Merchandising Rule</td>
            <td class=""px-4 py-2"">Adds a filter — only products matching both the algorithm AND the rule are displayed</td>
            <td class=""px-4 py-2"">""Only recommend products with stock > 10""</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Hand-Pick Rule</td>
            <td class=""px-4 py-2"">Overrides the algorithm — specified products are shown regardless of algorithm output</td>
            <td class=""px-4 py-2"">""Always show Product X when Product Y is viewed""</td>
        </tr>
    </tbody>
</table>

<h3>Master Rules</h3>
<p>Master rules define the <strong>scope</strong> of a merchandising campaign — which pages, products, or visitors are affected. Without master rules, campaigns apply globally across all selected widgets.</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Master Rule Scoping:

Campaign: ""Spring Sale Promotion""

Master Rules:
  ├── Page Scope: Apply only on Category pages
  ├── Product Scope: Apply only when viewing ""Spring Collection""
  └── Visitor Scope: Apply to all visitors

Merchandising Rule:
  └── Only recommend products where salePrice is set
      AND category contains ""Spring""
</pre>

<h3>Creating Rules in the Portal</h3>
<p>Navigate to <strong>Configuration &gt; Product Recommendations &gt; Campaigns</strong> to create and manage merchandising campaigns:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Create New Campaign</strong></li>
    <li>Define <strong>Master Rules</strong> to set the campaign scope (optional)</li>
    <li>Select the <strong>widget</strong> to apply rules to</li>
    <li>Choose rule type: <strong>Merchandising</strong> or <strong>Hand-Pick</strong></li>
    <li>Configure the rule conditions using attribute operators</li>
    <li>Save and activate the campaign</li>
</ol>

<h3>Rule Condition Operators</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute Type</th>
            <th class=""px-4 py-2 text-left"">Available Operators</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Text (Color, Brand)</td><td class=""px-4 py-2"">equals, NOT equals</td><td class=""px-4 py-2"">Brand equals ""Nike""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Numeric (Price, Stock)</td><td class=""px-4 py-2"">equals, NOT equals, greater than, less than</td><td class=""px-4 py-2"">salePrice >= 25.00</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Boolean</td><td class=""px-4 py-2"">equals true/false</td><td class=""px-4 py-2"">isOnSale equals true</td></tr>
    </tbody>
</table>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Real-Time Control</p>
    <p class=""text-amber-800 dark:text-amber-200"">Merchandising rules can be switched on or off and changed in real time through the Personalization Portal. This allows marketing teams to respond quickly to promotions, stock changes, or seasonal campaigns without requiring a code deployment.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-campaign-management",
                    ModuleId = "merchandising-campaigns",
                    Title = "Campaign Management",
                    Summary = "Create, manage, and schedule merchandising campaigns for coordinated recommendation control.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create and configure merchandising campaigns",
                        "Understand the campaign approval workflow",
                        "Manage campaign activation and deactivation",
                        "Coordinate multiple campaigns without conflicts"
                    },
                    Content = @"
<h2>Campaign Management</h2>
<p>Campaigns are the container for merchandising rules. They group related rules together, provide scheduling, and use the Editor/Reviewer workflow for controlled deployment.</p>

<h3>Campaign Lifecycle</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Campaign Lifecycle:

  Draft ──▶ Review ──▶ Approved ──▶ Active ──▶ Completed
    │          │           │          │           │
  Editor    Editor     Reviewer   Automatic    Automatic
  creates   submits    approves   (scheduled)  (end date)
            for review
                │
             Rejected ──▶ Draft (revised)
</pre>

<h3>Campaign Configuration</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Campaign Name</td><td class=""px-4 py-2"">Descriptive name for identification</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Start Date</td><td class=""px-4 py-2"">When the campaign becomes active (optional — immediate if blank)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">End Date</td><td class=""px-4 py-2"">When the campaign automatically deactivates (optional)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Target Widget</td><td class=""px-4 py-2"">Which widget(s) the campaign rules apply to</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Master Rules</td><td class=""px-4 py-2"">Scoping rules for pages, products, or visitors</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Merchandising Rules</td><td class=""px-4 py-2"">Filter or override rules for product selection</td></tr>
    </tbody>
</table>

<h3>Example Campaign Scenarios</h3>

<h4>Seasonal Promotion</h4>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Campaign:</strong> ""Summer Sale 2025""</p>
    <p><strong>Duration:</strong> 1 June — 31 August</p>
    <p><strong>Master Rule:</strong> All pages</p>
    <p><strong>Merchandising Rule:</strong> Boost products where <code>attribute(""season"") = ""Summer""</code> AND <code>salePrice</code> is set</p>
</div>

<h4>New Product Launch</h4>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Campaign:</strong> ""New Running Shoe Launch""</p>
    <p><strong>Duration:</strong> Launch week only</p>
    <p><strong>Master Rule:</strong> Product pages in ""Footwear > Running"" category</p>
    <p><strong>Hand-Pick Rule:</strong> Always include ""SKU-NEWSHOE-001"" in position 1 of alternatives widget</p>
</div>

<h4>Clearance Stock</h4>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Campaign:</strong> ""End of Line Clearance""</p>
    <p><strong>Duration:</strong> Ongoing until stock cleared</p>
    <p><strong>Master Rule:</strong> Basket and checkout pages</p>
    <p><strong>Merchandising Rule:</strong> Prioritise products where <code>stock &lt; 20</code> AND <code>salePrice &lt; unitPrice</code></p>
</div>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Campaign Conflicts</p>
    <p class=""text-blue-700 dark:text-blue-300"">When multiple active campaigns target the same widget, they are applied in order of creation. Be mindful of overlapping campaigns — a restrictive merchandising rule in one campaign may conflict with a hand-pick rule in another. Review active campaigns regularly to avoid unintended interactions.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-rule-configuration",
                    ModuleId = "merchandising-campaigns",
                    Title = "Advanced Rule Configuration",
                    Summary = "Master multi-position rules, expressions, and hints for fine-grained recommendation control.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create multi-position rules within a single campaign",
                        "Use expressions and hints to narrow recommendations",
                        "Combine merchandising and hand-pick rules effectively",
                        "Understand rule inheritance and override behaviour"
                    },
                    Content = @"
<h2>Advanced Rule Configuration</h2>
<p>For sophisticated merchandising scenarios, you can add multiple rules to a single campaign, target specific positions within a widget, and use expressions and hints for fine-grained control.</p>

<h3>Multi-Position Rules</h3>
<p>You can apply different rules to different positions within the same widget. This creates a curated experience where each position in the recommendation widget serves a different purpose.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Multi-Position Rule Example:

Widget: ""Alternatives"" (4 products)

Position 1: Hand-Pick
  └── Always show the promoted product of the week

Position 2: Merchandising Rule
  └── salePrice > £25.00
  └── category = same as viewed product

Position 3: Merchandising Rule
  └── salePrice BETWEEN £15.00 AND £24.99

Position 4: Merchandising Rule
  └── salePrice < £15.00

Result: Price-tiered recommendations with a promoted product in slot 1
</pre>

<h3>Expressions and Hints</h3>
<p>In email recommendation campaigns, you can add rules in the <strong>expression</strong> and <strong>hints</strong> sections for each product position to narrow and restrict recommended products. These rules work alongside the configured strategies.</p>

<h4>Expressions</h4>
<p>Expressions define required conditions that recommended products must meet:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Expression examples

// Only recommend products in stock
expression: ""stock > 0""

// Only recommend products with images
expression: ""imageUrl IS NOT NULL""

// Category restriction
expression: ""category CONTAINS 'Footwear'""
</pre>

<h4>Hints</h4>
<p>Hints provide preference signals that influence product selection without being mandatory:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Hint examples

// Prefer higher-margin products
hint: ""margin DESC""

// Prefer products with good ratings
hint: ""rating >= 4.0""

// Prefer newer products
hint: ""pubDate DESC""
</pre>

<h3>Combining Rule Types</h3>
<p>A well-designed campaign often combines multiple rule types:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Campaign: ""Black Friday 2025""

Master Rules:
  ├── Date Range: 25 Nov — 2 Dec 2025
  └── All page types

Widget: ""alternatives""
  ├── Position 1: Hand-Pick → Featured deal product
  ├── Position 2-3: Merchandising → Products where discount > 30%
  └── Position 4: Merchandising → Products where discount > 15%

Widget: ""cross-sell""
  └── All positions: Merchandising → Products in ""Gift Guide"" category
</pre>

<h3>Rule Debugging</h3>
<p>When rules do not produce expected results, check:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Master rule scope</strong> — Is the campaign targeting the correct pages/products?</li>
    <li><strong>Attribute values</strong> — Do the feed attribute values match the rule conditions exactly?</li>
    <li><strong>Rule conflicts</strong> — Are other active campaigns overriding your rules?</li>
    <li><strong>Product availability</strong> — Do enough products match the rule criteria?</li>
    <li><strong>Campaign status</strong> — Is the campaign approved and within its date range?</li>
</ul>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Best Practice</p>
    <p class=""text-amber-800 dark:text-amber-200"">Keep merchandising campaigns focused and time-limited. Permanent algorithmic improvements should be made to the widget strategy itself, not through perpetual campaigns. Use campaigns for promotions, seasonal events, and temporary business needs.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 9: Email Recommendations

    private LearningModule BuildEmailRecommendationsModule()
    {
        return new LearningModule
        {
            Id = "email-recommendations",
            Title = "Email Recommendations",
            Description = "Implement personalised product recommendations in email campaigns and triggered messages.",
            Icon = "envelope",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-email-recs-overview",
                    ModuleId = "email-recommendations",
                    Title = "Email Recommendations Overview",
                    Summary = "Understand how personalised product recommendations work in email campaigns.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand how email recommendations work",
                        "Know how visitor web behaviour links to email personalisation",
                        "Learn about the ESP-agnostic integration approach",
                        "Understand the CUID-to-email linking mechanism"
                    },
                    Content = @"
<h2>Email Recommendations Overview</h2>
<p>Email Product Recommendations enrich email campaigns with <strong>personalised product suggestions</strong> for each individual recipient. Each email is dynamically generated with product recommendations based on the recipient's browsing behaviour on your website.</p>

<h3>How Email Recommendations Work</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Email Recommendations Flow:

1. Visitor browses your website
   └── Behaviour tracked and linked to CUID

2. Visitor identifies themselves (login, checkout, newsletter signup)
   └── Email address linked to CUID

3. Email campaign is sent
   └── Recommendations engine generates personalised products
       for each recipient based on their web behaviour

4. Recipient opens email
   └── Product images and links are dynamically rendered
       at the moment of opening (real-time personalisation)
</pre>

<h3>Key Features</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">ESP-Agnostic</td><td class=""px-4 py-2"">Works with any Email Service Provider (Mailchimp, SendGrid, Campaign Monitor, etc.)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">1-to-1 Personalisation</td><td class=""px-4 py-2"">Each recipient gets unique product recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Real-Time Rendering</td><td class=""px-4 py-2"">Product images and data render at open time, not send time</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">No Complex Integration</td><td class=""px-4 py-2"">Image-based delivery — just include image URLs in email templates</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cross-Device</td><td class=""px-4 py-2"">Links behaviour from any device the visitor uses via CUID</td></tr>
    </tbody>
</table>

<h3>CUID-to-Email Linking</h3>
<p>For email recommendations to work, the system must link a visitor's anonymous CUID (from the <code>peerius_user</code> cookie) to their email address. This happens when:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li>A visitor logs into their account on your site</li>
    <li>A visitor completes a purchase (email captured at checkout)</li>
    <li>A visitor signs up for a newsletter</li>
    <li>Any action where the email is captured and sent to the tracking system</li>
</ul>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Increased Click-Through Rates</p>
    <p class=""text-amber-800 dark:text-amber-200"">Email recommendations dramatically increase click-through rates and engagement because each recipient sees products relevant to their personal interests, not generic product selections. Marketers can construct the entire email around personalised products for true 1-to-1 communication.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-email-strategies",
                    ModuleId = "email-recommendations",
                    Title = "Personalised vs Merchandised Strategies",
                    Summary = "Learn the two categories of email recommendation strategies and when to use each.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand personalised vs merchandised email strategies",
                        "Know the specific strategies available in each category",
                        "Learn how to stack strategies for optimal results",
                        "Choose the right strategy mix for your business"
                    },
                    Content = @"
<h2>Personalised vs Merchandised Strategies</h2>
<p>Email recommendations support two main categories of strategies. The best results come from combining both types in a stacked configuration.</p>

<h3>Strategy Categories</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Category</th>
            <th class=""px-4 py-2 text-left"">Based On</th>
            <th class=""px-4 py-2 text-left"">Requires</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Personalised</td>
            <td class=""px-4 py-2"">Individual visitor's web behaviour linked to their email</td>
            <td class=""px-4 py-2"">CUID-to-email linking (visitor identified on site)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Merchandised</td>
            <td class=""px-4 py-2"">Product-level data (popularity, trends, attributes)</td>
            <td class=""px-4 py-2"">No visitor identification required</td>
        </tr>
    </tbody>
</table>

<h3>Personalised Strategies</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Strategy</th>
            <th class=""px-4 py-2 text-left"">Logic</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Recently Viewed</td><td class=""px-4 py-2"">Products the recipient has viewed on the website</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Abandoned Basket</td><td class=""px-4 py-2"">Products left in the recipient's basket without purchasing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Abandoned Browse</td><td class=""px-4 py-2"">Products viewed but not added to basket</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Personalised for User</td><td class=""px-4 py-2"">Products matching the recipient's overall interest profile</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Post-Purchase</td><td class=""px-4 py-2"">Complementary products based on recent purchases</td></tr>
    </tbody>
</table>

<h3>Merchandised Strategies</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Strategy</th>
            <th class=""px-4 py-2 text-left"">Logic</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Best Sellers</td><td class=""px-4 py-2"">Products with the highest sales volume site-wide</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Best Trending</td><td class=""px-4 py-2"">Products gaining popularity over a recent period</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">New Products</td><td class=""px-4 py-2"">Most recently added products to the catalog</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category Best Sellers</td><td class=""px-4 py-2"">Top sellers within specific categories</td></tr>
    </tbody>
</table>

<h3>Strategy Stacking for Email</h3>
<p>Email strategies use the same stacking principle as web widgets — strategies are evaluated in order, and the next strategy fills remaining slots:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Email Strategy Stack (6 product positions):

1. Recently Viewed (max: 2)
   └── Found 2 products the recipient recently viewed ✓

2. Abandoned Basket (max: 2)
   └── Found 1 product in abandoned basket ✓

3. Best Sellers (max: 3)
   └── Fills remaining 3 positions ✓

Total: 6 products filled ✓
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Strategy Mix</p>
    <p class=""text-amber-800 dark:text-amber-200"">There is no single best strategy stack — it varies by business and what you sell. Adopt a combination of personalised and merchandised strategies. Use personalised strategies first (higher engagement) with merchandised strategies as fallbacks (broad appeal). Test different combinations to find the optimal mix for your audience.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-email-strategy-stacking",
                    ModuleId = "email-recommendations",
                    Title = "Strategy Stacking for Email",
                    Summary = "Design effective strategy stacks for email campaigns to maximise engagement.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Design strategy stacks optimised for email",
                        "Handle recipients with no browsing history",
                        "Use expressions and hints for email positions",
                        "Optimise email recommendations for different campaign types"
                    },
                    Content = @"
<h2>Strategy Stacking for Email</h2>
<p>Effective email recommendations require careful strategy stacking to handle different recipient segments — from highly engaged visitors with rich browsing history to new subscribers with no behavioural data.</p>

<h3>Designing for Different Segments</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Segment: Engaged Visitor (rich browsing history)
  Strategy Stack:
  1. Recently Viewed → Returns 2-3 products
  2. Personalised for User → Returns 2-3 products
  3. Best Sellers → Fills remaining

Result: Highly personalised, relevant recommendations

──────────────────────────────────────────────

Segment: Identified but Low Activity
  Strategy Stack:
  1. Recently Viewed → Returns 0-1 products
  2. Personalised for User → Returns 0-1 products
  3. Best Trending → Returns 2-3 products
  4. Best Sellers → Fills remaining

Result: Mix of limited personalisation + trending products

──────────────────────────────────────────────

Segment: New Subscriber (no browsing history)
  Strategy Stack:
  1. Recently Viewed → Returns 0 products
  2. Personalised for User → Returns 0 products
  3. Best Sellers → Fills all positions

Result: Popular products as introduction to catalog
</pre>

<h3>Expressions and Hints in Email</h3>
<p>For each product position in an email template, you can add expressions and hints to refine results:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Position 1:
  Strategy: Recently Viewed
  Expression: stock > 0 AND imageUrl IS NOT NULL
  Hint: price DESC

Position 2:
  Strategy: Personalised
  Expression: stock > 0
  Hint: salePrice IS NOT NULL  (prefer products on sale)

Position 3-6:
  Strategy: Best Sellers
  Expression: stock > 5
  Hint: rating >= 4.0  (prefer well-rated products)
</pre>

<h3>Campaign Type Templates</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Campaign Type</th>
            <th class=""px-4 py-2 text-left"">Recommended Stack</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Weekly Newsletter</td><td class=""px-4 py-2"">Personalised → Best Trending → New Products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Abandoned Cart Email</td><td class=""px-4 py-2"">Abandoned Basket → Cross-Sell → Best Sellers</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Post-Purchase Follow-Up</td><td class=""px-4 py-2"">Post-Purchase → Personalised → Category Best Sellers</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Win-Back Campaign</td><td class=""px-4 py-2"">Recently Viewed → Best Trending → New Products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sale Promotion</td><td class=""px-4 py-2"">Personalised (sale filter) → Best Sellers (sale filter)</td></tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Open-Time Rendering</p>
    <p class=""text-blue-700 dark:text-blue-300"">Email recommendations are rendered at the moment the recipient opens the email, not when it is sent. This means the most current product data (prices, stock, images) is always shown, and recommendations reflect the recipient's latest behaviour.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-triggered-campaigns",
                    ModuleId = "email-recommendations",
                    Title = "Triggered Campaigns",
                    Summary = "Set up automated email triggers based on visitor behaviour patterns.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the difference between in-session and daily triggers",
                        "Configure abandoned basket, browse, and checkout triggers",
                        "Set up daily triggers for targeted discounts and post-purchase",
                        "Monitor triggered campaign performance in the Portal"
                    },
                    Content = @"
<h2>Triggered Campaigns</h2>
<p>Triggered campaigns automatically send personalised emails based on specific visitor behaviour patterns. Unlike manual campaigns, triggers fire automatically when predefined conditions are met.</p>

<h3>Trigger Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">When It Fires</th>
            <th class=""px-4 py-2 text-left"">Timing</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">In-Session</td>
            <td class=""px-4 py-2"">Based on actions within the current browsing session</td>
            <td class=""px-4 py-2"">Shortly after session ends (typically 30-60 minutes)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Daily</td>
            <td class=""px-4 py-2"">Based on accumulated behaviour patterns over time</td>
            <td class=""px-4 py-2"">Processed in daily batch (usually overnight)</td>
        </tr>
    </tbody>
</table>

<h3>In-Session Triggers</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Trigger</th>
            <th class=""px-4 py-2 text-left"">Condition</th>
            <th class=""px-4 py-2 text-left"">Strategy</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Abandoned Basket</td>
            <td class=""px-4 py-2"">Visitor adds products to basket but does not complete purchase</td>
            <td class=""px-4 py-2"">Show abandoned basket items + complementary products</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Abandoned Browse</td>
            <td class=""px-4 py-2"">Visitor views products but does not add to basket</td>
            <td class=""px-4 py-2"">Show browsed products + similar alternatives</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Abandoned Checkout</td>
            <td class=""px-4 py-2"">Visitor enters checkout flow but does not complete order</td>
            <td class=""px-4 py-2"">Show checkout items + incentive to complete</td>
        </tr>
    </tbody>
</table>

<h3>Daily Triggers</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Trigger</th>
            <th class=""px-4 py-2 text-left"">Condition</th>
            <th class=""px-4 py-2 text-left"">Strategy</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Targeted Discounts</td>
            <td class=""px-4 py-2"">Products the visitor has shown interest in are now on sale</td>
            <td class=""px-4 py-2"">Show discounted products the visitor previously viewed</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">High Product Interest</td>
            <td class=""px-4 py-2"">Visitor has repeatedly viewed specific products</td>
            <td class=""px-4 py-2"">Show high-interest products + alternatives</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Post Purchase</td>
            <td class=""px-4 py-2"">Visitor completed a purchase recently</td>
            <td class=""px-4 py-2"">Show complementary products to recent purchase</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Low-in-Stock Abandoned Basket</td>
            <td class=""px-4 py-2"">Abandoned basket products have low remaining stock</td>
            <td class=""px-4 py-2"">Urgency message with low-stock items</td>
        </tr>
    </tbody>
</table>

<h3>Trigger-Specific Strategies</h3>
<p>Triggered campaigns have access to special strategies that are only available for trigger contexts:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Products from in-session trigger</strong> — Returns the actual abandoned products (basket items, browsed products, checkout items)</li>
    <li><strong>Products from daily trigger</strong> — Returns products matching the daily trigger criteria (discounted items, high-interest products, post-purchase complements)</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Triggered Campaign Example: Abandoned Basket

Strategy Stack (6 positions):
1. Products from trigger (max: 3)
   └── Shows the actual abandoned basket items

2. Frequently Bought Together (max: 2)
   └── Shows products commonly bought with basket items

3. Best Sellers (max: 3)
   └── Fills remaining with popular products

Timing: Sent 1 hour after session ends
</pre>

<h3>Monitoring Triggered Campaigns</h3>
<p>In the Personalization Portal's <strong>Reports</strong> section, a dedicated <strong>Triggers dashboard</strong> shows performance metrics:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Revenue</strong> — Total revenue attributed to triggered emails</li>
    <li><strong>Orders</strong> — Number of orders from triggered email clicks</li>
    <li><strong>Clicks</strong> — Total clicks on recommended products in triggered emails</li>
    <li><strong>Click-Through Rate (CTR)</strong> — Percentage of opened triggered emails that generated clicks</li>
</ul>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Trigger Frequency</p>
    <p class=""text-amber-800 dark:text-amber-200"">Configure trigger frequency caps to avoid sending too many automated emails. For example, limit abandoned basket triggers to once per 24 hours per visitor, and set a maximum of 3 triggered emails per week to prevent email fatigue.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 10: Reporting, Analytics & Best Practices

    private LearningModule BuildReportingBestPracticesModule()
    {
        return new LearningModule
        {
            Id = "reporting-best-practices",
            Title = "Reporting, Analytics & Best Practices",
            Description = "Monitor recommendation performance, understand analytics dashboards, and apply best practices.",
            Icon = "chart-bar",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pr-portal-dashboard",
                    ModuleId = "reporting-best-practices",
                    Title = "Personalization Portal Dashboard",
                    Summary = "Navigate the reporting dashboards and understand the key performance indicators.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the Personalization Portal reporting section",
                        "Understand the key performance indicators (KPIs) tracked",
                        "Interpret dashboard metrics for web and email recommendations",
                        "Use date ranges and filters to analyse performance trends"
                    },
                    Content = @"
<h2>Personalization Portal Dashboard</h2>
<p>The Personalization Portal provides comprehensive reporting dashboards that track the performance of your product recommendations across web and email channels. These dashboards help you understand the business impact of recommendations and identify optimisation opportunities.</p>

<h3>Accessing Reports</h3>
<p>Navigate to the <strong>Reports</strong> section in the Personalization Portal. The reporting area is organised into several dashboards:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Dashboard</th>
            <th class=""px-4 py-2 text-left"">What It Shows</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Overview</td><td class=""px-4 py-2"">High-level summary of recommendation performance across all channels</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Web Recommendations</td><td class=""px-4 py-2"">Performance of on-site recommendation widgets</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email Recommendations</td><td class=""px-4 py-2"">Performance of email product recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Triggers</td><td class=""px-4 py-2"">Performance of triggered email campaigns</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Widget Detail</td><td class=""px-4 py-2"">Granular performance data for individual widgets</td></tr>
    </tbody>
</table>

<h3>Key Performance Indicators</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">KPI</th>
            <th class=""px-4 py-2 text-left"">Definition</th>
            <th class=""px-4 py-2 text-left"">Good Benchmark</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Revenue</td><td class=""px-4 py-2"">Total revenue from orders that involved a recommendation click</td><td class=""px-4 py-2"">5-15% of total site revenue</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Orders</td><td class=""px-4 py-2"">Number of orders that included at least one recommendation click</td><td class=""px-4 py-2"">Varies by traffic volume</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Click-Through Rate</td><td class=""px-4 py-2"">% of recommendation impressions that result in a click</td><td class=""px-4 py-2"">2-8% for web widgets</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Conversion Rate</td><td class=""px-4 py-2"">% of recommendation clicks that lead to a purchase</td><td class=""px-4 py-2"">10-25% (varies by sector)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Average Order Value</td><td class=""px-4 py-2"">Average order total for recommendation-influenced purchases</td><td class=""px-4 py-2"">Should be higher than non-rec orders</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Revenue per Click</td><td class=""px-4 py-2"">Average revenue generated per recommendation click</td><td class=""px-4 py-2"">Varies by product price range</td></tr>
    </tbody>
</table>

<h3>Dashboard Filters</h3>
<p>Use filters to drill into specific data:</p>
<ul class=""list-disc list-inside space-y-1"">
    <li><strong>Date Range</strong> — Compare performance across different time periods</li>
    <li><strong>Widget</strong> — View metrics for specific widgets (alternatives, cross-sell, etc.)</li>
    <li><strong>Page Type</strong> — Filter by page type (product, category, home, basket)</li>
    <li><strong>Device</strong> — Compare desktop vs mobile performance</li>
    <li><strong>Channel</strong> — Web vs email recommendations</li>
</ul>

<h3>Interpreting Trends</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Healthy Performance Trends:

Revenue from Recs:  ████████████████████  Steady or growing ✓
Click-Through Rate: ████████████████░░░░  Stable 4-6% ✓
Conversion Rate:    ████████████████████  Above site average ✓

Warning Signs:

Click-Through Rate: ████░░░░░░░░░░░░░░░░  Declining → Review widget placement
Conversion Rate:    ████████░░░░░░░░░░░░  Below site average → Review algorithms
Revenue per Click:  ████████████░░░░░░░░  Dropping → Check product quality/pricing
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Attribution Model</p>
    <p class=""text-amber-800 dark:text-amber-200"">Revenue attribution requires click tracking to be correctly implemented. A purchase is attributed to recommendations only if the visitor clicked a recommendation link before completing the order. Ensure click tracking is properly configured (Module 3) to get accurate revenue reporting.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-performance-monitoring",
                    ModuleId = "reporting-best-practices",
                    Title = "Performance Monitoring and Optimisation",
                    Summary = "Monitor recommendation performance and make data-driven optimisation decisions.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Set up regular performance monitoring routines",
                        "Identify underperforming widgets and strategies",
                        "Make data-driven optimisation decisions",
                        "Track the impact of strategy changes over time"
                    },
                    Content = @"
<h2>Performance Monitoring and Optimisation</h2>
<p>Regular performance monitoring is essential to ensure your recommendations continue to deliver value. This lesson covers monitoring routines, identifying issues, and making data-driven improvements.</p>

<h3>Monitoring Routine</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Weekly Review</h4>
    <ul class=""list-disc list-inside space-y-1 mt-2"">
        <li>Check overall revenue and CTR trends</li>
        <li>Review any significant drops or spikes</li>
        <li>Verify tracking is working (no gaps in data)</li>
        <li>Check feed import status and discrepancy reports</li>
    </ul>

    <h4 class=""font-semibold mt-4"">Monthly Review</h4>
    <ul class=""list-disc list-inside space-y-1 mt-2"">
        <li>Compare widget performance (which widgets drive the most revenue?)</li>
        <li>Review page type performance (which pages have the best CTR?)</li>
        <li>Analyse email recommendation metrics vs web metrics</li>
        <li>Review triggered campaign performance</li>
        <li>Identify optimisation opportunities</li>
    </ul>

    <h4 class=""font-semibold mt-4"">Quarterly Review</h4>
    <ul class=""list-disc list-inside space-y-1 mt-2"">
        <li>Full strategy audit — are algorithms still aligned with business goals?</li>
        <li>Review merchandising campaigns — remove expired, assess ongoing</li>
        <li>Compare performance against previous quarter</li>
        <li>Plan A/B tests for the next quarter</li>
    </ul>
</div>

<h3>Identifying Issues</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Symptom</th>
            <th class=""px-4 py-2 text-left"">Possible Cause</th>
            <th class=""px-4 py-2 text-left"">Investigation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">CTR dropping</td><td class=""px-4 py-2"">Poor relevance or widget fatigue</td><td class=""px-4 py-2"">Review algorithm stack; try different strategies</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Revenue flat despite traffic growth</td><td class=""px-4 py-2"">Low-value products being recommended</td><td class=""px-4 py-2"">Add price or margin filters to algorithms</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">High CTR but low conversion</td><td class=""px-4 py-2"">Recommended products not matching intent</td><td class=""px-4 py-2"">Review algorithm types; test alternatives vs cross-sell focus</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sudden data gaps</td><td class=""px-4 py-2"">Tracking broken on key pages</td><td class=""px-4 py-2"">Check tracking implementation on affected page types</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email recs underperforming</td><td class=""px-4 py-2"">Low CUID-to-email link rate</td><td class=""px-4 py-2"">Review how emails are captured and linked to CUIDs</td></tr>
    </tbody>
</table>

<h3>Optimisation Framework</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Optimisation Cycle:

1. MEASURE
   └── Collect baseline metrics for current configuration

2. ANALYSE
   └── Identify the weakest performing widget or page type

3. HYPOTHESISE
   └── Form a theory on what change would improve performance

4. TEST
   └── Run an A/B test with the proposed change

5. IMPLEMENT
   └── If the test wins, deploy the change to 100% of traffic

6. REPEAT
   └── Move to the next optimisation opportunity
</pre>

<h3>Common Optimisations</h3>
<ul class=""list-disc list-inside space-y-2"">
    <li><strong>Reorder algorithm stack</strong> — Move higher-performing algorithms to the top</li>
    <li><strong>Adjust number of products</strong> — Test 4 vs 6 vs 8 products per widget</li>
    <li><strong>Add price filters</strong> — Ensure recommended products are within an appropriate price range relative to the viewed product</li>
    <li><strong>Improve widget placement</strong> — Move widgets to more visible page positions</li>
    <li><strong>Seasonal strategy updates</strong> — Adjust strategies for seasonal buying patterns</li>
</ul>

<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Change One Thing at a Time</p>
    <p class=""text-amber-800 dark:text-amber-200"">When optimising, change only one variable at a time. If you change the algorithm stack AND the number of products AND the widget placement simultaneously, you cannot determine which change caused the performance shift. Test changes incrementally.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pr-best-practices",
                    ModuleId = "reporting-best-practices",
                    Title = "Best Practices and Troubleshooting",
                    Summary = "Apply best practices for a successful Product Recommendations implementation and resolve common issues.",
                    Order = 3,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Apply implementation best practices for tracking, feeds, and widgets",
                        "Resolve the most common issues in Product Recommendations",
                        "Understand privacy and consent considerations",
                        "Know the key success factors for a recommendations implementation"
                    },
                    Content = @"
<h2>Best Practices and Troubleshooting</h2>
<p>A successful Product Recommendations implementation combines correct technical setup with thoughtful strategy configuration. This lesson consolidates the key best practices and provides a comprehensive troubleshooting guide.</p>

<h3>Implementation Best Practices</h3>

<h4>Catalog Feed</h4>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Keep feed data fresh — run the export job at least daily</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Ensure GUID consistency between feed and tracking (this is the #1 cause of issues)</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Match feed categories to website breadcrumb structure exactly</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Include all products with stock > 0 in the feed</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Use HTTPS for all image URLs</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Include custom attributes that you will use for filtering later</span></li>
        <li class=""flex items-start gap-2""><span class=""text-red-500 font-bold"">✗</span><span>Do not include discontinued or permanently out-of-stock products</span></li>
    </ul>
</div>

<h4>Tracking</h4>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Track all user journey page types (product, category, search, basket, order at minimum)</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Ensure refCode consistency across all page types</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Implement click tracking for revenue attribution</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Use client-side tracking API if using output caching</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Coordinate with Optimizely before switching between email and pseudonymous tracking</span></li>
        <li class=""flex items-start gap-2""><span class=""text-red-500 font-bold"">✗</span><span>Do not track pages that should not show recommendations unless needed for analytics</span></li>
    </ul>
</div>

<h4>Widgets & Strategies</h4>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Order widgets by priority — most important strategy first</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Always configure a fallback product set for every algorithm</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Balance personalisation algorithms with popularity algorithms</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Avoid overly restrictive filters that eliminate too many candidates</span></li>
        <li class=""flex items-start gap-2""><span class=""text-green-500 font-bold"">✓</span><span>Test strategy changes via A/B testing before full rollout</span></li>
        <li class=""flex items-start gap-2""><span class=""text-red-500 font-bold"">✗</span><span>Do not use more than 11 algorithms per widget (system limit)</span></li>
    </ul>
</div>

<h3>Multi-Locale Considerations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Requirement</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Feed per locale</td><td class=""px-4 py-2"">Separate feeds with localised titles, descriptions, and prices</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Language matching</td><td class=""px-4 py-2"">Feed language must match Portal language configuration</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Currency</td><td class=""px-4 py-2"">Each feed should use the correct currency for its market</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">GUID consistency</td><td class=""px-4 py-2"">Same product should use the same GUID across locale feeds</td></tr>
    </tbody>
</table>

<h3>Privacy and Consent</h3>
<ul class=""list-disc list-inside space-y-1"">
    <li>The <code>peerius_user</code> cookie is a first-party cookie — include it in your cookie consent policy</li>
    <li>Use <code>UsePseudonymousUserId</code> if you need to avoid sending email addresses in tracking data</li>
    <li>IP addresses are used for geolocation only and are not stored — disable via <code>SkipUserHostTracking</code> if not needed</li>
    <li>Only send customer email in tracking data when the visitor has consented to personalisation</li>
    <li>Document Product Recommendations cookies and tracking in your privacy policy</li>
</ul>

<h3>Comprehensive Troubleshooting Guide</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Troubleshooting Decision Tree:

Problem: No recommendations showing
├── Check 1: Is tracking working?
│   ├── Yes → Check 2
│   └── No  → Verify tracking script/attribute on page
│
├── Check 2: Are widgets activated for this page type?
│   ├── Yes → Check 3
│   └── No  → Activate in Portal → Configuration → Activation
│
├── Check 3: Is the product in the feed?
│   ├── Yes → Check 4
│   └── No  → Add to feed and wait for import
│
├── Check 4: Does the product have stock > 0?
│   ├── Yes → Check 5
│   └── No  → Update stock in feed
│
├── Check 5: Does the feed GUID match the tracking refCode?
│   ├── Yes → Check 6
│   └── No  → Fix GUID mismatch
│
└── Check 6: Is there enough behavioural data?
    ├── Yes → Contact Optimizely support
    └── No  → Allow time for data collection (new sites need 2-4 weeks)

Problem: Wrong products recommended
├── Check feed categories match site breadcrumbs
├── Verify refCode consistency across page types
├── Review active merchandising campaigns for conflicts
└── Check algorithm stack order and filters

Problem: Revenue not attributed in reports
├── Verify click tracking is implemented
├── Check recommendationId is passed in query string
└── Verify order tracking fires on confirmation page
</pre>

<h3>Key Success Factors</h3>
<div class=""bg-amber-50 dark:bg-amber-900/30 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium text-amber-900 dark:text-amber-100"">Summary</p>
    <p class=""text-amber-800 dark:text-amber-200"">A successful Product Recommendations implementation rests on three pillars: <strong>clean, consistent data</strong> (feed quality and refCode consistency), <strong>comprehensive tracking</strong> (all page types with click tracking), and <strong>well-designed strategies</strong> (balanced algorithm stacks with appropriate filters). Get these three right, and the ML engine will deliver increasingly relevant recommendations as it learns from visitor behaviour over time.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion
}
