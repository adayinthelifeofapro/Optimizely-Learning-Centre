using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.ContentRecs;

/// <summary>
/// Content provider for the Optimizely Content Recommendations course
/// </summary>
public class ContentRecsContentProvider : ILearningContentProvider
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
            BuildContentProcessingModule(),
            BuildTrackingImplementationModule(),
            BuildVisitorProfilesModule(),
            BuildFlowsSectionsModule(),
            BuildWidgetDeliveriesModule(),
            BuildCmsIntegrationModule(),
            BuildDashboardsAnalyticsModule(),
            BuildGoalsAbTestingModule(),
            BuildOdpIntegrationModule(),
            BuildEmailTriggeredCampaignsModule()
        };
    }

    #region Module 1: Getting Started

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started",
            Description = "Learn the fundamentals of Optimizely Content Recommendations and understand how AI-powered personalisation works.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-content-recommendations",
                    ModuleId = "getting-started",
                    Title = "What is Content Recommendations?",
                    Summary = "Discover Optimizely Content Recommendations and its AI-powered personalisation capabilities.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Content Recommendations is and its purpose",
                        "Learn how NLP-driven personalisation works",
                        "Understand the key benefits of content recommendations",
                        "Know when to use Content Recommendations for your projects"
                    },
                    Content = @"
<h2>Introduction to Optimizely Content Recommendations</h2>
<p>Optimizely Content Recommendations is an <strong>AI-powered marketing solution</strong> that automatically generates personalised content feeds for each visitor based on their individual site activity and interests.</p>

<h3>What is Content Recommendations?</h3>
<p>Content Recommendations uses <strong>Natural Language Processing (NLP)</strong> to understand the meaning of each piece of content at a granular level. It builds a real-time interest profile for each visitor based on their interactions with NLP-generated topics, then uses this information to recommend articles, blog posts, or other content that is most relevant to each visitor's unique interest profile.</p>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Key Concept</p>
    <p class=""text-blue-700 dark:text-blue-300"">Content Recommendations anonymously tracks visitor activity on your website to build a profile for each visitor. The intelligent algorithms then analyse those unique profiles and deliver the most relevant content to each visitor in real-time.</p>
</div>

<h3>How It Works at a High Level</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li><strong>Content Ingestion</strong> - Your website content is crawled and processed</li>
    <li><strong>NLP Analysis</strong> - Topics are automatically extracted from each piece of content</li>
    <li><strong>Visitor Tracking</strong> - Anonymous visitor behaviour is tracked via JavaScript</li>
    <li><strong>Profile Building</strong> - Interest profiles are built based on content interactions</li>
    <li><strong>Recommendations</strong> - Personalised content is delivered via widgets or API</li>
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
        <tr><td class=""px-4 py-2 font-medium"">Automated Personalisation</td><td class=""px-4 py-2"">No manual tagging or categorisation required - NLP does it automatically</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Real-Time Profiles</td><td class=""px-4 py-2"">Visitor interests update instantly as they browse your content</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Increased Engagement</td><td class=""px-4 py-2"">Relevant recommendations keep visitors on your site longer</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Content Intelligence</td><td class=""px-4 py-2"">Understand your content landscape with automated topic analysis</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cross-Channel</td><td class=""px-4 py-2"">Deliver recommendations on web, email, and via API</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Privacy-Friendly</td><td class=""px-4 py-2"">Anonymous first-party tracking without requiring login</td></tr>
    </tbody>
</table>

<h3>Use Cases</h3>
<ul>
    <li><strong>Media & Publishing</strong> - Recommend related articles to readers based on reading history</li>
    <li><strong>B2B Marketing</strong> - Surface relevant whitepapers, case studies, and resources to prospects</li>
    <li><strong>Corporate Websites</strong> - Guide visitors to relevant content based on their interests</li>
    <li><strong>Knowledge Bases</strong> - Help users discover related documentation and guides</li>
    <li><strong>Email Marketing</strong> - Include personalised content recommendations in newsletters</li>
</ul>

<h3>Content Recommendations vs Manual Curation</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Content Recommendations</th>
            <th class=""px-4 py-2 text-left"">Manual Curation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Personalisation</td><td class=""px-4 py-2"">Individual visitor level</td><td class=""px-4 py-2"">Same for all visitors</td></tr>
        <tr><td class=""px-4 py-2"">Scalability</td><td class=""px-4 py-2"">Automatic for all content</td><td class=""px-4 py-2"">Manual effort per page</td></tr>
        <tr><td class=""px-4 py-2"">Freshness</td><td class=""px-4 py-2"">New content auto-included</td><td class=""px-4 py-2"">Requires manual updates</td></tr>
        <tr><td class=""px-4 py-2"">Maintenance</td><td class=""px-4 py-2"">Self-maintaining</td><td class=""px-4 py-2"">Ongoing effort required</td></tr>
        <tr><td class=""px-4 py-2"">Intelligence</td><td class=""px-4 py-2"">AI-driven topic matching</td><td class=""px-4 py-2"">Human judgement</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-how-it-works",
                    ModuleId = "getting-started",
                    Title = "How Content Recommendations Works",
                    Summary = "Understand the technical process behind visitor profiling and content matching.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the content processing pipeline",
                        "Learn how visitor profiles are built",
                        "Understand topic matching and relevance scoring",
                        "Know how recommendations are generated in real-time"
                    },
                    Content = @"
<h2>The Content Recommendations Engine</h2>
<p>Content Recommendations operates through a sophisticated pipeline that processes your content, tracks visitor behaviour, and delivers personalised recommendations in real-time.</p>

<h3>The Processing Pipeline</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                 Content Recommendations Pipeline                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌──────────────┐    ┌──────────────┐    ┌──────────────┐      │
│  │   Content    │───▶│     NLP      │───▶│    Topic     │      │
│  │   Crawling   │    │  Processing  │    │   Database   │      │
│  └──────────────┘    └──────────────┘    └──────────────┘      │
│                                                 │                │
│                                                 ▼                │
│  ┌──────────────┐    ┌──────────────┐    ┌──────────────┐      │
│  │   Visitor    │───▶│   Profile    │◀───│    Topic     │      │
│  │   Tracking   │    │   Building   │    │   Matching   │      │
│  └──────────────┘    └──────────────┘    └──────────────┘      │
│                             │                                    │
│                             ▼                                    │
│                      ┌──────────────┐                           │
│                      │Recommendations│                           │
│                      │   Delivery   │                           │
│                      └──────────────┘                           │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Step 1: Content Processing</h3>
<p>When content is ingested into the system, two things happen:</p>
<ul>
    <li><strong>Visual Capture</strong> - A copy of all visual components of the content is made</li>
    <li><strong>Metadata Extraction</strong> - Available metadata (title, URL, image, publish date) is captured and stored</li>
</ul>

<h3>Step 2: NLP Topic Extraction</h3>
<p>Content Recommendations applies Natural Language Processing to automatically read and extract topics from every piece of content. Each content item is assigned its own <strong>weighted topic cloud</strong>.</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Topic Weighting</p>
    <p class=""text-blue-700 dark:text-blue-300"">Topics are weighted based on:</p>
    <ul class=""mt-2 text-blue-700 dark:text-blue-300"">
        <li><strong>Higher weight</strong> - Topic appears frequently in the article</li>
        <li><strong>Lower weight</strong> - Topic appears frequently across many articles</li>
        <li>The final weight is calculated considering both factors</li>
    </ul>
</div>

<h3>Step 3: Visitor Tracking</h3>
<p>The ip.js tracking SDK monitors visitor activity:</p>
<ul>
    <li><strong>Visitor ID</strong> - A unique UUID4 stored in the 'iv' cookie (persists 2 years)</li>
    <li><strong>Session ID</strong> - A short-lived session ID in the 'is' cookie (30 minutes)</li>
    <li><strong>Page Interactions</strong> - Which content pages the visitor views</li>
    <li><strong>Referrer Data</strong> - Where visitors came from</li>
    <li><strong>UTM Parameters</strong> - Marketing campaign tracking</li>
</ul>

<h3>Step 4: Profile Building</h3>
<p>As visitors interact with content, their interest profile is built in real-time:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Visitor Profile Example:
┌─────────────────────────────────────────┐
│ Visitor ID: a1b2c3d4-e5f6-7890-...      │
├─────────────────────────────────────────┤
│ Interest Topics:                        │
│   • Digital Marketing      ████████ 85% │
│   • Content Strategy       ██████   60% │
│   • SEO Optimisation       █████    50% │
│   • Email Marketing        ████     40% │
│   • Analytics              ███      30% │
├─────────────────────────────────────────┤
│ Content Interactions: 23                │
│ Sessions: 5                             │
│ First Visit: 2024-01-15                 │
│ Last Visit: 2024-02-01                  │
└─────────────────────────────────────────┘
</pre>

<h3>Step 5: Real-Time Recommendations</h3>
<p>When a visitor loads a page with a recommendation widget:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>The widget SDK retrieves the visitor's interest profile</li>
    <li>Content is matched against the visitor's top interest topics</li>
    <li>Flows and sections filter what content is eligible</li>
    <li>Ranked recommendations are returned and displayed</li>
</ol>

<h3>Profile Evolution</h3>
<p>The visitor's interest profile is dynamic and changes with each interaction:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Behaviour</th>
            <th class=""px-4 py-2 text-left"">Profile Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Views content about Topic A</td><td class=""px-4 py-2"">Topic A interest increases</td></tr>
        <tr><td class=""px-4 py-2"">Spends time on Topic B content</td><td class=""px-4 py-2"">Topic B gains higher weight</td></tr>
        <tr><td class=""px-4 py-2"">Ignores Topic C recommendations</td><td class=""px-4 py-2"">Topic C interest may decrease over time</td></tr>
        <tr><td class=""px-4 py-2"">Returns multiple times for Topic D</td><td class=""px-4 py-2"">Topic D becomes a strong interest signal</td></tr>
    </tbody>
</table>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Key Insight</p>
    <p class=""text-green-700 dark:text-green-300"">As the visitor's interest profile changes with more content consumption, the corresponding recommendations change appropriately. This creates a continuously improving personalised experience.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-architecture-overview",
                    ModuleId = "getting-started",
                    Title = "Architecture Overview",
                    Summary = "Understand the key components and how they work together.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the main components of Content Recommendations",
                        "Learn the relationship between tracking, flows, sections, and deliveries",
                        "Understand the role of the Content Recommendations portal",
                        "Know the different deployment options"
                    },
                    Content = @"
<h2>Content Recommendations Architecture</h2>
<p>Content Recommendations consists of several interconnected components that work together to deliver personalised content experiences.</p>

<h3>Core Components</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│              Content Recommendations Architecture                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  YOUR WEBSITE                      CONTENT RECS SERVICE         │
│  ┌──────────────────┐              ┌──────────────────┐         │
│  │                  │              │                  │         │
│  │  ┌────────────┐  │   Tracking   │  ┌────────────┐  │         │
│  │  │ Tracking   │──┼─────────────▶│  │  Profile   │  │         │
│  │  │ Script     │  │              │  │  Engine    │  │         │
│  │  │ (ip.js)    │  │              │  └────────────┘  │         │
│  │  └────────────┘  │              │        │         │         │
│  │                  │              │        ▼         │         │
│  │  ┌────────────┐  │   Widgets    │  ┌────────────┐  │         │
│  │  │ Recommend  │◀─┼──────────────┤  │  Content   │  │         │
│  │  │ Widgets    │  │              │  │  Matching  │  │         │
│  │  └────────────┘  │              │  └────────────┘  │         │
│  │                  │              │        │         │         │
│  │  ┌────────────┐  │   Crawling   │        ▼         │         │
│  │  │ Content    │──┼─────────────▶│  ┌────────────┐  │         │
│  │  │ Pages      │  │              │  │    NLP     │  │         │
│  │  └────────────┘  │              │  │  Processing│  │         │
│  │                  │              │  └────────────┘  │         │
│  └──────────────────┘              └──────────────────┘         │
│                                                                  │
│  CONTENT RECOMMENDATIONS PORTAL (UI)                            │
│  ┌──────────────────────────────────────────────────────┐       │
│  │  Content  │  Flows  │  Sections  │  Deliveries  │ Analytics │
│  └──────────────────────────────────────────────────────┘       │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Component Descriptions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Tracking Script (ip.js)</td><td class=""px-4 py-2"">JavaScript SDK that tracks visitor behaviour and manages cookies</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Profile Engine</td><td class=""px-4 py-2"">Builds and maintains visitor interest profiles from tracked interactions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">NLP Processing</td><td class=""px-4 py-2"">Extracts topics and creates weighted topic clouds from content</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Content Matching</td><td class=""px-4 py-2"">Matches visitor profiles to relevant content based on topic similarity</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recommendation Widgets</td><td class=""px-4 py-2"">Display components that render personalised recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Portal UI</td><td class=""px-4 py-2"">Management interface for configuring flows, sections, and deliveries</td></tr>
    </tbody>
</table>

<h3>Key Concepts</h3>

<h4>Sources</h4>
<p>Sources are the origins of content ingested into the system. This is typically your website domain (e.g., www.example.com) or an RSS feed.</p>

<h4>Content Items</h4>
<p>A content item is a piece of text-based content with an associated identifier (URL). Content items include articles, blog posts, and other rich text pages.</p>

<h4>Topics</h4>
<p>Topics are keywords or concepts extracted from content via NLP. Each content item has many topics with different weights.</p>

<h4>Sections</h4>
<p>Sections are content categories that group related content together. For example, ""Financial Blog Posts"" or ""Product Documentation"".</p>

<h4>Flows</h4>
<p>Flows are rules that determine how content is organised into sections. They define criteria for including, excluding, or featuring content.</p>

<h4>Deliveries</h4>
<p>Deliveries are the configuration for how and where recommendations are displayed. Each widget is linked to a specific delivery.</p>

<h3>Deployment Options</h3>
<div class=""grid grid-cols-1 md:grid-cols-3 gap-4 my-4"">
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h5 class=""font-medium"">Manual Script</h5>
        <p class=""text-sm"">Add tracking and widget scripts directly to your HTML</p>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h5 class=""font-medium"">Tag Manager</h5>
        <p class=""text-sm"">Deploy via Google Tag Manager or OneTrust</p>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h5 class=""font-medium"">CMS Package</h5>
        <p class=""text-sm"">Install NuGet package for Optimizely CMS integration</p>
    </div>
</div>

<h3>Regional API Endpoints</h3>
<p>Content Recommendations provides regional API endpoints for optimal performance:</p>
<ul>
    <li><strong>Americas</strong> - Default endpoint</li>
    <li><strong>EMEA</strong> - Europe, Middle East, Africa</li>
    <li><strong>APAC</strong> - Asia-Pacific</li>
    <li><strong>Canada</strong> - Canadian data residency</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-prerequisites-requirements",
                    ModuleId = "getting-started",
                    Title = "Prerequisites & Requirements",
                    Summary = "Understand what you need before implementing Content Recommendations.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Know the prerequisites for Content Recommendations",
                        "Understand licensing and subscription requirements",
                        "Learn about metadata requirements",
                        "Understand technical requirements for your website"
                    },
                    Content = @"
<h2>Prerequisites for Content Recommendations</h2>
<p>Before implementing Content Recommendations, ensure you have the following prerequisites in place.</p>

<h3>Subscription Requirements</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Required</p>
    <ul class=""mt-2 text-yellow-700 dark:text-yellow-300"">
        <li>✓ Active subscription to Optimizely Content Recommendations service</li>
        <li>✓ Configuration information for your environment</li>
        <li>✓ User login credentials for the Content Recommendations portal</li>
    </ul>
</div>

<h3>Technical Requirements</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Requirement</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Public Internet Access</td><td class=""px-4 py-2"">Your website must be accessible from the public internet so Content Recommendations can crawl and index your content</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">JavaScript Support</td><td class=""px-4 py-2"">Visitors must have JavaScript enabled for tracking and widgets to function</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">HTTPS</td><td class=""px-4 py-2"">Recommended for secure tracking and cookie handling</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Metadata Implementation</td><td class=""px-4 py-2"">Open Graph tags should be present on content pages</td></tr>
    </tbody>
</table>

<h3>For Optimizely CMS Integration</h3>
<p>If you're integrating with Optimizely CMS, you'll also need:</p>
<ul>
    <li><strong>Optimizely CMS 11+</strong> (CMS 12 recommended for latest features)</li>
    <li><strong>RenderRequiredClientResources</strong> in your HTML head element</li>
    <li><strong>NuGet Feed Access</strong> for the EPiServer.Personalization.Content.UI package</li>
</ul>

<h3>Metadata Requirements</h3>
<p>Content Recommendations uses Open Graph metadata to display recommendations. Ensure your content pages include:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- Required metadata --&gt;
&lt;meta property=""og:title"" content=""Your Article Title"" /&gt;
&lt;meta property=""og:description"" content=""Article description..."" /&gt;
&lt;meta property=""og:image"" content=""https://example.com/image.jpg"" /&gt;

&lt;!-- Recommended metadata --&gt;
&lt;meta property=""og:url"" content=""https://example.com/article"" /&gt;
&lt;meta property=""og:type"" content=""article"" /&gt;
&lt;meta property=""article:published_time"" content=""2024-01-15"" /&gt;
</pre>

<h3>Content Requirements</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Content Eligibility</p>
    <p class=""text-blue-700 dark:text-blue-300"">For best results with Content Recommendations:</p>
    <ul class=""mt-2 text-blue-700 dark:text-blue-300"">
        <li>✓ Text-based content (articles, blog posts, documentation)</li>
        <li>✓ Sufficient text for NLP topic extraction (minimum ~200 words recommended)</li>
        <li>✓ Unique URLs for each content item</li>
        <li>✓ Consistent metadata across content pages</li>
    </ul>
</div>

<h3>Browser Cookie Support</h3>
<p>Content Recommendations uses first-party cookies for visitor identification:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cookie</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Duration</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">iv</td><td class=""px-4 py-2"">Visitor ID (idio visitor)</td><td class=""px-4 py-2"">2 years</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">is</td><td class=""px-4 py-2"">Session ID</td><td class=""px-4 py-2"">30 minutes</td></tr>
    </tbody>
</table>

<h3>Pre-Implementation Checklist</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>☐ Content Recommendations subscription active</li>
        <li>☐ Portal credentials received</li>
        <li>☐ Website publicly accessible</li>
        <li>☐ Open Graph metadata implemented</li>
        <li>☐ Widget deliveries configured in portal (before CMS installation)</li>
        <li>☐ Cookie consent mechanism in place (if required by regulations)</li>
    </ul>
</div>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Important</p>
    <p class=""text-red-700 dark:text-red-300"">Before installing Content Recommendations, make sure you have widget deliveries set up in the portal. Otherwise, you will not see any widget deliveries in the drop-down widget selector when configuring blocks in the CMS.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Content Processing & NLP

    private LearningModule BuildContentProcessingModule()
    {
        return new LearningModule
        {
            Id = "content-processing",
            Title = "Content Processing & NLP",
            Description = "Learn how content is ingested, processed, and analysed using Natural Language Processing.",
            Icon = "cpu-chip",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cp-content-ingestion",
                    ModuleId = "content-processing",
                    Title = "Content Ingestion",
                    Summary = "Learn how Content Recommendations crawls and ingests your website content.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the content ingestion process",
                        "Learn what happens when content is crawled",
                        "Know the difference between sources and content items",
                        "Understand how content is stored and indexed"
                    },
                    Content = @"
<h2>Content Ingestion Process</h2>
<p>Content ingestion is the process of gathering your website content for analysis. When Content Recommendations crawls your site, it captures and processes each piece of content to enable personalised recommendations.</p>

<h3>What is Ingestion?</h3>
<p>Ingestion is defined as the process of gathering content for topic analysis. When a URL is added to your website where Content Recommendations is implemented, that content is automatically ingested into the system.</p>

<h3>What Happens During Ingestion</h3>
<p>When content is ingested, two primary things occur:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li><strong>Visual Capture</strong> - Content Recommendations makes a copy of all the visual components of the content</li>
        <li><strong>Metadata Extraction</strong> - Available metadata in the page source code is captured and stored (title, URL, image, publish date, etc.)</li>
    </ol>
</div>

<h3>Sources vs Content Items</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Concept</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Source</td><td class=""px-4 py-2"">The origin of content ingested into the system</td><td class=""px-4 py-2"">www.example.com, RSS feed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Content Item</td><td class=""px-4 py-2"">A piece of text-based content with an associated identifier</td><td class=""px-4 py-2"">A blog post, article, or documentation page</td></tr>
    </tbody>
</table>

<h3>Content Item Characteristics</h3>
<ul>
    <li>Each content item has a unique identifier (typically its URL)</li>
    <li>Content items are text-based: articles, blogs, rich text pages</li>
    <li>A content item can contain many topics with different weights</li>
    <li>Visual elements (images) are captured for display in widgets</li>
</ul>

<h3>Automatic vs Manual Ingestion</h3>
<p>Content can be ingested in two ways:</p>

<div class=""grid grid-cols-1 md:grid-cols-2 gap-4 my-4"">
    <div class=""bg-blue-50 dark:bg-blue-900/20 p-4 rounded-lg"">
        <h4 class=""font-medium text-blue-800 dark:text-blue-200"">Automatic Ingestion</h4>
        <p class=""text-blue-700 dark:text-blue-300 text-sm mt-2"">Content is automatically discovered and ingested when visitors view pages with the tracking script installed.</p>
    </div>
    <div class=""bg-green-50 dark:bg-green-900/20 p-4 rounded-lg"">
        <h4 class=""font-medium text-green-800 dark:text-green-200"">Sitemap/RSS Ingestion</h4>
        <p class=""text-green-700 dark:text-green-300 text-sm mt-2"">Content can be bulk-imported via XML sitemaps or RSS feeds for faster initial indexing.</p>
    </div>
</div>

<h3>Content Eligibility</h3>
<p>Not all pages are suitable for Content Recommendations. Ideal content includes:</p>
<ul>
    <li>✓ Articles and blog posts</li>
    <li>✓ Documentation and guides</li>
    <li>✓ News and press releases</li>
    <li>✓ Case studies and whitepapers</li>
    <li>✓ Educational content</li>
</ul>

<p>Pages typically excluded:</p>
<ul>
    <li>✗ Navigation/menu pages</li>
    <li>✗ Login/authentication pages</li>
    <li>✗ Search results pages</li>
    <li>✗ Shopping cart/checkout pages</li>
    <li>✗ User profile pages</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cp-nlp-topic-extraction",
                    ModuleId = "content-processing",
                    Title = "NLP Topic Extraction",
                    Summary = "Understand how Natural Language Processing extracts and weights topics from your content.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand how NLP analyses content",
                        "Learn how topics are extracted from text",
                        "Understand topic weighting algorithms",
                        "Know how topic clouds are created"
                    },
                    Content = @"
<h2>Natural Language Processing in Content Recommendations</h2>
<p>Content Recommendations uses advanced NLP technology to automatically read, understand, and extract meaningful topics from every piece of content on your website.</p>

<h3>What is NLP Topic Extraction?</h3>
<p>NLP (Natural Language Processing) is the AI technology that enables Content Recommendations to understand what your content is about without manual tagging or categorisation. It reads content the same way modern search engines parse and index web pages.</p>

<h3>How NLP Analyses Content</h3>
<p>The NLP engine examines multiple elements of each page:</p>
<ul>
    <li><strong>Page Text</strong> - The main body content of the page</li>
    <li><strong>Headers</strong> - H1, H2, H3 tags for structural understanding</li>
    <li><strong>Metadata</strong> - Title, description, and Open Graph tags</li>
    <li><strong>URLs</strong> - Path structure can indicate topic relevance</li>
</ul>

<h3>Topic Assignment</h3>
<p>After analysis, each piece of content is assigned a <strong>weighted topic cloud</strong> - a collection of topics with associated relevance scores.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Example Topic Cloud for an Article:
┌─────────────────────────────────────────┐
│ Article: ""Getting Started with SEO""    │
├─────────────────────────────────────────┤
│ Topics:                                 │
│   • Search Engine Optimisation   0.92  │
│   • Digital Marketing            0.78  │
│   • Content Strategy             0.65  │
│   • Google                       0.54  │
│   • Website Traffic              0.48  │
│   • Keywords                     0.45  │
│   • Meta Tags                    0.42  │
│   • Backlinks                    0.38  │
└─────────────────────────────────────────┘
</pre>

<h3>Topic Weighting Algorithm</h3>
<p>Topics are weighted based on two primary factors:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Factor</th>
            <th class=""px-4 py-2 text-left"">Impact on Weight</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Frequency in Article</td><td class=""px-4 py-2"">Higher weight - Topic appears many times in this specific content</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Frequency Across Articles</td><td class=""px-4 py-2"">Lower weight - Topic appears in many other documents (less unique)</td></tr>
    </tbody>
</table>

<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">TF-IDF Concept</p>
    <p class=""text-purple-700 dark:text-purple-300"">This weighting approach is similar to TF-IDF (Term Frequency-Inverse Document Frequency) used in information retrieval. Topics that are prominent in a specific article but not ubiquitous across all content receive the highest weights.</p>
</div>

<h3>Knowledge Graph</h3>
<p>Content Recommendations uses a predefined <strong>knowledge graph</strong> to understand relationships between topics. This enables:</p>
<ul>
    <li>Recognition of synonyms and related terms</li>
    <li>Understanding of topic hierarchies</li>
    <li>Contextual disambiguation of terms</li>
    <li>Cross-language topic mapping</li>
</ul>

<h3>Topic Quality Factors</h3>
<p>The quality of topic extraction depends on:</p>
<ul>
    <li><strong>Content Length</strong> - More text provides better context for accurate topic extraction</li>
    <li><strong>Content Quality</strong> - Well-written, focused content yields clearer topics</li>
    <li><strong>Unique Content</strong> - Avoid boilerplate text that appears on every page</li>
    <li><strong>Semantic Clarity</strong> - Clear language and proper structure help NLP understanding</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cp-metadata-requirements",
                    ModuleId = "content-processing",
                    Title = "Metadata Requirements",
                    Summary = "Learn about the Open Graph and metadata requirements for optimal content processing.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the importance of metadata for recommendations",
                        "Learn the required Open Graph tags",
                        "Know how to implement metadata correctly",
                        "Understand how metadata affects widget display"
                    },
                    Content = @"
<h2>Metadata Requirements</h2>
<p>Content Recommendations relies on properly implemented metadata to display attractive and informative recommendations. Open Graph tags are the primary source of this metadata.</p>

<h3>Why Metadata Matters</h3>
<p>Metadata serves two crucial purposes:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li><strong>Widget Display</strong> - Images, titles, and descriptions shown in recommendation widgets come from metadata</li>
    <li><strong>Content Understanding</strong> - Metadata helps NLP better understand what content is about</li>
</ol>

<h3>Required Open Graph Tags</h3>
<p>The following metadata should be present on all content pages eligible for recommendations:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- Image - Displayed in recommendation widgets --&gt;
&lt;meta property=""og:image"" content=""https://example.com/images/article-image.jpg"" /&gt;

&lt;!-- Title - Primary text shown in recommendations --&gt;
&lt;meta property=""og:title"" content=""Your Article Title Here"" /&gt;

&lt;!-- Description - Supporting text in recommendations --&gt;
&lt;meta property=""og:description"" content=""A brief description of the article content..."" /&gt;
</pre>

<h3>Recommended Additional Tags</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- Canonical URL --&gt;
&lt;meta property=""og:url"" content=""https://example.com/blog/article-slug"" /&gt;

&lt;!-- Content Type --&gt;
&lt;meta property=""og:type"" content=""article"" /&gt;

&lt;!-- Publish Date --&gt;
&lt;meta property=""article:published_time"" content=""2024-01-15T09:00:00Z"" /&gt;

&lt;!-- Author (optional) --&gt;
&lt;meta property=""article:author"" content=""John Smith"" /&gt;

&lt;!-- Section/Category (optional) --&gt;
&lt;meta property=""article:section"" content=""Technology"" /&gt;
</pre>

<h3>Metadata Format Reference</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Format</th>
            <th class=""px-4 py-2 text-left"">Notes</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">og:image</td><td class=""px-4 py-2"">Absolute URL</td><td class=""px-4 py-2"">Recommended: 1200x630px minimum</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">og:title</td><td class=""px-4 py-2"">Plain text</td><td class=""px-4 py-2"">60-90 characters recommended</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">og:description</td><td class=""px-4 py-2"">Plain text</td><td class=""px-4 py-2"">150-200 characters recommended</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">og:url</td><td class=""px-4 py-2"">Absolute URL</td><td class=""px-4 py-2"">Canonical URL of the page</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">article:published_time</td><td class=""px-4 py-2"">ISO 8601</td><td class=""px-4 py-2"">Used for freshness sorting</td></tr>
    </tbody>
</table>

<h3>Multiple Values</h3>
<p>If you have multiple values for a metadata item, Content Recommendations collects them all. For example, multiple images:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;meta property=""og:image"" content=""https://example.com/image1.jpg"" /&gt;
&lt;meta property=""og:image"" content=""https://example.com/image2.jpg"" /&gt;
</pre>

<h3>Fallback Behaviour</h3>
<p>If Open Graph tags are missing, Content Recommendations will attempt to use:</p>
<ul>
    <li><strong>Image</strong> - First suitable image found on the page</li>
    <li><strong>Title</strong> - The HTML &lt;title&gt; tag or first &lt;h1&gt;</li>
    <li><strong>Description</strong> - Meta description tag or extracted from content</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Best Practice</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Always implement explicit Open Graph tags rather than relying on fallbacks. This ensures consistent, high-quality display in recommendation widgets and when content is shared on social media.</p>
</div>

<h3>Image Guidelines</h3>
<ul>
    <li>Use high-quality images (minimum 1200x630 pixels)</li>
    <li>Ensure images are publicly accessible</li>
    <li>Use absolute URLs (https://)</li>
    <li>Avoid text-heavy images that don't scale well</li>
    <li>Use consistent aspect ratios across your content</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cp-data-epi-type-elements",
                    ModuleId = "content-processing",
                    Title = "Data-epi-type Elements",
                    Summary = "Learn how to control content scraping with data-epi-type attributes.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of data-epi-type attributes",
                        "Learn how to mark content for NLP extraction",
                        "Know how to specify custom titles",
                        "Understand how to exclude content from scraping"
                    },
                    Content = @"
<h2>Controlling Content Scraping with data-epi-type</h2>
<p>The <code>data-epi-type</code> attribute gives you fine-grained control over what content is extracted for NLP analysis and how it's identified in the system.</p>

<h3>Why Use data-epi-type?</h3>
<p>While Content Recommendations automatically extracts content, you may need to:</p>
<ul>
    <li>Specify exactly which text should be analysed for topics</li>
    <li>Define a specific title different from og:title or h1</li>
    <li>Exclude non-unique elements (disclaimers, footers, navigation)</li>
</ul>

<h3>Content Attribute</h3>
<p>Use <code>data-epi-type=""content""</code> to mark the main content area for topic extraction:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;article data-epi-type=""content""&gt;
    &lt;h1&gt;Article Title&lt;/h1&gt;
    &lt;p&gt;This is the main article content that will be
    analysed by NLP for topic extraction...&lt;/p&gt;

    &lt;h2&gt;Section Heading&lt;/h2&gt;
    &lt;p&gt;More content here...&lt;/p&gt;
&lt;/article&gt;

&lt;!-- This sidebar will NOT be included in NLP analysis --&gt;
&lt;aside&gt;
    &lt;h3&gt;Related Links&lt;/h3&gt;
    &lt;ul&gt;...&lt;/ul&gt;
&lt;/aside&gt;
</pre>

<h3>Title Attribute</h3>
<p>Use <code>data-epi-type=""title""</code> to specify the exact title Content Recommendations should use:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;!-- When og:title, h1, and title tag are different --&gt;
&lt;h1 data-epi-type=""title""&gt;The Definitive Title to Use&lt;/h1&gt;
</pre>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">When to Use data-epi-type=""title""</p>
    <p class=""text-blue-700 dark:text-blue-300"">Use this when your &lt;og:title&gt;, &lt;h1&gt;, and &lt;title&gt; tags contain different values and you want to explicitly specify which one Content Recommendations should use as the primary title.</p>
</div>

<h3>Common Use Cases</h3>

<h4>1. Excluding Boilerplate Content</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;main data-epi-type=""content""&gt;
    &lt;!-- Only this content is analysed --&gt;
    &lt;article&gt;...&lt;/article&gt;
&lt;/main&gt;

&lt;!-- These are automatically excluded --&gt;
&lt;header&gt;...navigation...&lt;/header&gt;
&lt;footer&gt;...copyright, links...&lt;/footer&gt;
&lt;div class=""disclaimer""&gt;...legal text...&lt;/div&gt;
</pre>

<h4>2. Excluding Repeated Elements</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;article data-epi-type=""content""&gt;
    &lt;h1&gt;Article Title&lt;/h1&gt;
    &lt;p&gt;Unique article content...&lt;/p&gt;
&lt;/article&gt;

&lt;!-- Newsletter signup appears on every page - excluded --&gt;
&lt;div class=""newsletter-signup""&gt;
    &lt;p&gt;Subscribe to our newsletter...&lt;/p&gt;
&lt;/div&gt;
</pre>

<h3>Best Practices</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Do</th>
            <th class=""px-4 py-2 text-left"">Don't</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Mark the main article/content area</td><td class=""px-4 py-2"">Mark the entire body element</td></tr>
        <tr><td class=""px-4 py-2"">Exclude navigation and footers</td><td class=""px-4 py-2"">Include sidebar ads in content</td></tr>
        <tr><td class=""px-4 py-2"">Use consistent markup across pages</td><td class=""px-4 py-2"">Have different structures per template</td></tr>
        <tr><td class=""px-4 py-2"">Test with Content Dashboard</td><td class=""px-4 py-2"">Assume scraping works correctly</td></tr>
    </tbody>
</table>

<h3>Metadata Still Applies</h3>
<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Important</p>
    <p class=""text-green-700 dark:text-green-300"">Any metadata such as &lt;og:title&gt; will still be picked up and saved against the content in addition to the main title of the page itself. The data-epi-type attributes complement, not replace, your Open Graph metadata.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cp-reprocessing-content",
                    ModuleId = "content-processing",
                    Title = "Reprocessing Content",
                    Summary = "Learn when and how to reprocess content after changes.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand when content reprocessing is needed",
                        "Learn how to trigger content reprocessing",
                        "Know the limitations of reprocessing",
                        "Understand the impact on existing recommendations"
                    },
                    Content = @"
<h2>Reprocessing Content</h2>
<p>After content is initially ingested, there are situations where you may need to reprocess it to reflect changes or improvements.</p>

<h3>When to Reprocess Content</h3>
<p>Consider reprocessing content when:</p>

<ul>
    <li><strong>NLP Changes</strong> - You want to re-evaluate the topics picked up due to NLP improvements</li>
    <li><strong>Content Scraping Fixes</strong> - You've added data-epi-type attributes to exclude non-unique content elements</li>
    <li><strong>URL Changes</strong> - Content has moved to new URLs (redirects)</li>
    <li><strong>Content Updates</strong> - Editors have made significant changes to the content</li>
    <li><strong>Metadata Updates</strong> - New or changed Open Graph tags have been added</li>
</ul>

<h3>How to Reprocess Content</h3>
<p>Content can be reprocessed through the Content Recommendations portal:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to <strong>Content &gt; Content List</strong> in the portal</li>
        <li>Find the content item(s) you want to reprocess</li>
        <li>Select the item(s) using checkboxes</li>
        <li>Choose the <strong>Reprocess</strong> action from the bulk actions menu</li>
        <li>Confirm the reprocessing request</li>
    </ol>
</div>

<h3>Bulk Reprocessing</h3>
<p>You can reprocess multiple items at once:</p>
<ul>
    <li>Use filters to find content matching specific criteria</li>
    <li>Select all filtered items</li>
    <li>Apply the reprocess action to the entire selection</li>
</ul>

<h3>Important Limitations</h3>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Flow Re-evaluation</p>
    <p class=""text-red-700 dark:text-red-300"">Content is evaluated against flows only when it is first imported. Editing a flow or reprocessing content does <strong>not</strong> re-evaluate that content against flows. Flow changes only affect newly ingested content.</p>
</div>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Section Membership</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">A flow will never move content out of a section. Removing content from sections can only be accomplished manually through the content list using filters and bulk actions.</p>
</div>

<h3>What Reprocessing Updates</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Updated</th>
            <th class=""px-4 py-2 text-left"">Not Updated</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Topic extraction and weights</td><td class=""px-4 py-2"">Flow/section assignments</td></tr>
        <tr><td class=""px-4 py-2"">Metadata (title, image, description)</td><td class=""px-4 py-2"">Historical interaction data</td></tr>
        <tr><td class=""px-4 py-2"">Content body text</td><td class=""px-4 py-2"">Visitor profile associations</td></tr>
        <tr><td class=""px-4 py-2"">URL references</td><td class=""px-4 py-2"">Conversion/goal data</td></tr>
    </tbody>
</table>

<h3>Processing Time</h3>
<p>Reprocessing is not instant:</p>
<ul>
    <li>Content enters a processing queue</li>
    <li>Processing time depends on queue depth and content complexity</li>
    <li>Large bulk reprocessing jobs may take several hours</li>
    <li>Monitor the Content Dashboard for processing status</li>
</ul>

<h3>Best Practices</h3>
<ul>
    <li>Reprocess after implementing data-epi-type markup changes</li>
    <li>Schedule bulk reprocessing during low-traffic periods</li>
    <li>Verify changes in the Content Dashboard after processing completes</li>
    <li>Use filters to target specific content rather than reprocessing everything</li>
</ul>
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
            Description = "Learn how to implement and validate visitor tracking on your website.",
            Icon = "signal",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ti-tracking-sdk",
                    ModuleId = "tracking-implementation",
                    Title = "The ip.js Tracking SDK",
                    Summary = "Understand how the Content Recommendations tracking SDK works.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose and capabilities of the ip.js SDK",
                        "Learn about the command queue system",
                        "Understand visitor and session management",
                        "Know what data is collected by the tracker"
                    },
                    Content = @"
<h2>The ip.js Tracking SDK</h2>
<p>The ip.js SDK is a lightweight, asynchronous JavaScript library that forms the core of Content Recommendations' tracking capabilities. It enables visitor tracking, session management, and event logging.</p>

<h3>SDK Overview</h3>
<p>The tracking SDK is responsible for:</p>
<ul>
    <li><strong>Visitor Identification</strong> - Assigning and maintaining unique visitor IDs</li>
    <li><strong>Session Management</strong> - Tracking browsing sessions</li>
    <li><strong>Data Collection</strong> - Capturing page views, referrers, and UTM parameters</li>
    <li><strong>Event Logging</strong> - Recording custom events and conversions</li>
</ul>

<h3>How the SDK Loads</h3>
<p>The SDK is designed to be non-blocking and loads asynchronously:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;script&gt;
(function(i,s,o,g,r,a,m){i['IdiomObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//js.idio.co/YOUR_CLIENT_ID.js','_iaq');

_iaq.push(['_setClientId', 'YOUR_CLIENT_ID']);
_iaq.push(['_trackPageview']);
&lt;/script&gt;
</pre>

<h3>Command Queue System</h3>
<p>The SDK uses a global <code>window._iaq</code> array as a command queue. This allows you to push tracking commands before the SDK has fully loaded, ensuring no events are missed.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Commands can be queued immediately
_iaq.push(['_setClientId', 'YOUR_CLIENT_ID']);
_iaq.push(['_trackPageview']);

// The SDK processes the queue when loaded
// Commands execute in order
</pre>

<h3>Visitor Identity Management</h3>
<p>The SDK generates and manages visitor identification:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cookie</th>
            <th class=""px-4 py-2 text-left"">Name</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Duration</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">iv</td><td class=""px-4 py-2"">idio visitor</td><td class=""px-4 py-2"">Unique visitor identifier (UUID4)</td><td class=""px-4 py-2"">2 years</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">is</td><td class=""px-4 py-2"">idio session</td><td class=""px-4 py-2"">Session identifier</td><td class=""px-4 py-2"">30 minutes (sliding)</td></tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">First-Party Cookies</p>
    <p class=""text-blue-700 dark:text-blue-300"">Content Recommendations uses first-party cookies, meaning they are set on your domain. This provides better privacy compliance and resilience against third-party cookie blocking.</p>
</div>

<h3>Data Collected</h3>
<p>The SDK automatically captures:</p>
<ul>
    <li><strong>Page URL</strong> - The canonical URL of the current page</li>
    <li><strong>Referrer</strong> - Where the visitor came from</li>
    <li><strong>UTM Parameters</strong> - Campaign tracking parameters (utm_source, utm_medium, etc.)</li>
    <li><strong>Timestamp</strong> - When the interaction occurred</li>
    <li><strong>User Agent</strong> - Browser and device information</li>
</ul>

<h3>Tracking Call (ia.gif)</h3>
<p>When tracking data is sent, it's transmitted via a pixel request to <code>ia.gif</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Request: ia.gif?c=CLIENT_ID&v=VISITOR_ID&s=SESSION_ID&u=PAGE_URL&r=REFERRER...
Response: 200 OK (1x1 transparent GIF)
</pre>

<h3>Cross-Platform Integration</h3>
<p>If your site uses Optimizely Marketing Automation and has set the <code>_madid</code> cookie (Marketing Automation Device ID), the tracking SDK will automatically detect it and include the device ID in tracking data as <code>epi_device_id</code>.</p>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Automatic Integration</p>
    <p class=""text-green-700 dark:text-green-300"">No configuration is required for cross-platform tracking. If the _madid cookie exists, it will be automatically included in all tracking events, enabling cross-product visitor identification.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ti-manual-deployment",
                    ModuleId = "tracking-implementation",
                    Title = "Manual Script Deployment",
                    Summary = "Learn how to manually install the tracking script on your website.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Know where to place the tracking script",
                        "Understand the required configuration",
                        "Learn how to customise tracking behaviour",
                        "Implement the script correctly"
                    },
                    Content = @"
<h2>Manual Script Deployment</h2>
<p>The most direct way to implement Content Recommendations tracking is to add the script directly to your website's HTML.</p>

<h3>Basic Implementation</h3>
<p>Add the following script to your website, preferably in the <code>&lt;head&gt;</code> section or just before the closing <code>&lt;/body&gt;</code> tag:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;script&gt;
(function(i,s,o,g,r,a,m){i['IdiomObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//js.idio.co/YOUR_CLIENT_ID.js','_iaq');

_iaq.push(['_setClientId', 'YOUR_CLIENT_ID']);
_iaq.push(['_trackPageview']);
&lt;/script&gt;
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Replace Placeholder</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Replace <code>YOUR_CLIENT_ID</code> with your actual Content Recommendations client ID, provided by Optimizely during setup.</p>
</div>

<h3>Script Placement</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Placement</th>
            <th class=""px-4 py-2 text-left"">Pros</th>
            <th class=""px-4 py-2 text-left"">Cons</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">&lt;head&gt;</td><td class=""px-4 py-2"">Loads earliest, captures all events</td><td class=""px-4 py-2"">May slightly delay page rendering</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Before &lt;/body&gt;</td><td class=""px-4 py-2"">Doesn't block rendering</td><td class=""px-4 py-2"">May miss very early events</td></tr>
    </tbody>
</table>

<h3>Tracking All Pages</h3>
<p>For tracking to work effectively, the script should be present on <strong>every page</strong> of your website. Use a shared layout, master page, or template to ensure consistent deployment.</p>

<h3>Single Page Applications (SPAs)</h3>
<p>For SPAs where page navigation doesn't trigger full page loads, you need to manually track page views on route changes:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Call this when the route changes in your SPA
function trackPageView() {
    _iaq.push(['_trackPageview', {
        url: window.location.href,
        title: document.title
    }]);
}

// React example with useEffect
useEffect(() => {
    trackPageView();
}, [location.pathname]);

// Vue Router example
router.afterEach((to) => {
    trackPageView();
});
</pre>

<h3>Custom Tracking Parameters</h3>
<p>You can pass additional data with page views:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
_iaq.push(['_trackPageview', {
    url: 'https://example.com/article/123',
    title: 'Article Title',
    referrer: 'https://google.com',
    custom: {
        author: 'John Smith',
        category: 'Technology'
    }
}]);
</pre>

<h3>Excluding Pages from Tracking</h3>
<p>To exclude certain pages from tracking, simply don't include the script on those pages, or conditionally prevent tracking:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Only track if not on excluded pages
if (!window.location.pathname.startsWith('/admin')) {
    _iaq.push(['_trackPageview']);
}
</pre>

<h3>Content Security Policy (CSP)</h3>
<p>If your site uses CSP headers, ensure you allow the tracking script domain:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Content-Security-Policy: script-src 'self' js.idio.co;
                         img-src 'self' *.idio.co;
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ti-gtm-deployment",
                    ModuleId = "tracking-implementation",
                    Title = "Google Tag Manager Deployment",
                    Summary = "Deploy tracking via Google Tag Manager for easier management.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Set up tracking in Google Tag Manager",
                        "Configure triggers for page tracking",
                        "Test the implementation",
                        "Understand consent management integration"
                    },
                    Content = @"
<h2>Deploying via Google Tag Manager</h2>
<p>Google Tag Manager (GTM) provides a flexible way to deploy and manage the Content Recommendations tracking script without modifying your website's code directly.</p>

<h3>Step 1: Create a New Tag</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In your GTM workspace, navigate to <strong>Tags</strong></li>
    <li>Click <strong>New</strong> to create a new tag</li>
    <li>Click <strong>Tag Configuration</strong> and choose <strong>Custom HTML</strong></li>
</ol>

<h3>Step 2: Add the Tracking Script</h3>
<p>In the HTML field, paste the tracking script:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;script&gt;
(function(i,s,o,g,r,a,m){i['IdiomObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//js.idio.co/YOUR_CLIENT_ID.js','_iaq');

_iaq.push(['_setClientId', 'YOUR_CLIENT_ID']);
_iaq.push(['_trackPageview']);
&lt;/script&gt;
</pre>

<h3>Step 3: Configure the Trigger</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Triggering</strong></li>
    <li>Select the <strong>All Pages</strong> trigger to load the script on every page</li>
    <li>Alternatively, create a custom trigger for specific pages</li>
</ol>

<h3>Custom Page Triggers</h3>
<p>To track only specific pages, create a new trigger:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Go to <strong>Triggers</strong> &gt; <strong>New</strong></li>
        <li>Choose <strong>Page View</strong> as the trigger type</li>
        <li>Select <strong>Some Page Views</strong></li>
        <li>Set conditions based on Page URL or Page Path</li>
    </ol>
</div>

<p>Example conditions:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Variable</th>
            <th class=""px-4 py-2 text-left"">Condition</th>
            <th class=""px-4 py-2 text-left"">Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Page Path</td><td class=""px-4 py-2"">starts with</td><td class=""px-4 py-2"">/blog/</td></tr>
        <tr><td class=""px-4 py-2"">Page Path</td><td class=""px-4 py-2"">matches RegEx</td><td class=""px-4 py-2"">^/articles/.*</td></tr>
        <tr><td class=""px-4 py-2"">Page URL</td><td class=""px-4 py-2"">does not contain</td><td class=""px-4 py-2"">/admin</td></tr>
    </tbody>
</table>

<h3>Consent Management Integration</h3>
<p>If you use OneTrust or another consent management platform, you can integrate tracking with consent:</p>

<h4>OneTrust Integration</h4>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In OneTrust, click <strong>Add New</strong></li>
    <li>Paste the tracking script into the <strong>Custom Script</strong> field</li>
    <li>Assign the script to a consent category (e.g., ""Performance & Analytics Cookies"")</li>
    <li>The script will only fire if the user has given consent for this category</li>
</ol>

<h4>GTM Consent Mode</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example: Only track if analytics consent is granted
if (window.dataLayer) {
    window.dataLayer.push(function() {
        if (this.get('analytics_storage') === 'granted') {
            _iaq.push(['_trackPageview']);
        }
    });
}
</pre>

<h3>Testing in GTM</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Preview</strong> in the top right of GTM</li>
    <li>Enter your website URL</li>
    <li>Navigate to pages and verify the tag fires</li>
    <li>Check the Tag Assistant panel for confirmation</li>
</ol>

<h3>Publishing</h3>
<p>Once testing is complete:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Submit</strong> in GTM</li>
    <li>Add a version name and description</li>
    <li>Click <strong>Publish</strong></li>
</ol>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Version Control</p>
    <p class=""text-green-700 dark:text-green-300"">GTM maintains a version history, making it easy to roll back changes if issues arise after deployment.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ti-cms-package",
                    ModuleId = "tracking-implementation",
                    Title = "CMS NuGet Package Installation",
                    Summary = "Install the Content Recommendations package for Optimizely CMS.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Install the EPiServer.Personalization.Content.UI package",
                        "Configure the required settings",
                        "Understand RenderRequiredClientResources",
                        "Set up the integration correctly"
                    },
                    Content = @"
<h2>CMS NuGet Package Installation</h2>
<p>For Optimizely CMS implementations, the recommended approach is to install the Content Recommendations NuGet package, which provides seamless integration with your CMS.</p>

<h3>Package Name</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4 font-mono"">
EPiServer.Personalization.Content.UI
</div>

<h3>Prerequisites</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Before Installing</p>
    <ul class=""mt-2 text-yellow-700 dark:text-yellow-300"">
        <li>✓ Optimizely CMS 11 or CMS 12+ installed</li>
        <li>✓ Website uses <code>RenderRequiredClientResources</code> in the HTML head element</li>
        <li>✓ Website is publicly accessible on the internet</li>
        <li>✓ Widget deliveries are already set up in the Content Recommendations portal</li>
    </ul>
</div>

<h3>Installation</h3>
<p>Install via NuGet Package Manager or CLI:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
# Package Manager Console
Install-Package EPiServer.Personalization.Content.UI

# .NET CLI
dotnet add package EPiServer.Personalization.Content.UI
</pre>

<h3>Configuration (CMS 12+)</h3>
<p>Add the required settings to your <code>appsettings.json</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""EPiServer"": {
    ""Personalization"": {
      ""Content"": {
        ""Environment"": ""production"",
        ""ClientId"": ""YOUR_CLIENT_ID"",
        ""ClientName"": ""YOUR_CLIENT_NAME"",
        ""ApiToken"": ""YOUR_API_TOKEN""
      }
    }
  }
}
</pre>

<h3>Configuration (CMS 11)</h3>
<p>Add the settings to your <code>web.config</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;appSettings&gt;
    &lt;add key=""episerver:personalization.content.environment"" value=""production"" /&gt;
    &lt;add key=""episerver:personalization.content.clientid"" value=""YOUR_CLIENT_ID"" /&gt;
    &lt;add key=""episerver:personalization.content.clientname"" value=""YOUR_CLIENT_NAME"" /&gt;
    &lt;add key=""episerver:personalization.content.apitoken"" value=""YOUR_API_TOKEN"" /&gt;
&lt;/appSettings&gt;
</pre>

<h3>Configuration Values</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Environment</td><td class=""px-4 py-2"">The Content Recommendations environment (e.g., ""production"", ""staging"")</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ClientId</td><td class=""px-4 py-2"">Your unique client identifier</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ClientName</td><td class=""px-4 py-2"">Your client name</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ApiToken</td><td class=""px-4 py-2"">Authentication token for API access</td></tr>
    </tbody>
</table>

<h3>RenderRequiredClientResources</h3>
<p>Ensure your layout view includes the required client resources call in the <code>&lt;head&gt;</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;head&gt;
    @Html.RequiredClientResources(""Header"")
    &lt;!-- or for Razor Pages --&gt;
    @Html.RenderRequiredClientResources()
&lt;/head&gt;
</pre>

<p>This is where the tracking script will be automatically injected by the package.</p>

<h3>What the Package Provides</h3>
<ul>
    <li><strong>Automatic Tracking</strong> - Script injection handled automatically</li>
    <li><strong>Content Recommendation Block</strong> - Drag-and-drop block for editors</li>
    <li><strong>Widget Selector</strong> - Choose from configured deliveries in the CMS</li>
    <li><strong>Admin Interface</strong> - Configuration management in CMS admin</li>
</ul>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Important</p>
    <p class=""text-red-700 dark:text-red-300"">Before installing Content Recommendations, make sure you have widget deliveries set up in the portal. Otherwise, you will not see any widget deliveries in the drop-down widget selector when configuring recommendation blocks.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ti-validating-tracking",
                    ModuleId = "tracking-implementation",
                    Title = "Validating Tracking",
                    Summary = "Learn how to verify that tracking is working correctly.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use browser developer tools to verify tracking",
                        "Check for the ia.gif tracking call",
                        "Verify cookie creation",
                        "Troubleshoot common issues"
                    },
                    Content = @"
<h2>Validating Tracking Installation</h2>
<p>After implementing the tracking script, it's essential to verify that it's working correctly before relying on the data.</p>

<h3>Using Browser Developer Tools</h3>
<p>The primary way to validate tracking is through your browser's developer tools (F12).</p>

<h4>Step 1: Check Network Requests</h4>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Open Chrome DevTools (F12 or right-click &gt; Inspect)</li>
    <li>Go to the <strong>Network</strong> tab</li>
    <li>In the filter box, search for <code>ia.gif</code></li>
    <li>Refresh the page or navigate to a content page</li>
    <li>Look for the ia.gif request</li>
</ol>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Success Indicator</p>
    <p class=""text-green-700 dark:text-green-300"">If <code>ia.gif</code> shows up with a status of <strong>200</strong>, the tracking installation was successful. The request sends visitor information to Content Recommendations.</p>
</div>

<h4>Step 2: Verify Cookies</h4>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In Chrome DevTools, select the <strong>Application</strong> tab</li>
    <li>Expand <strong>Storage</strong> &gt; <strong>Cookies</strong></li>
    <li>Select your website's domain</li>
    <li>Look for the following cookies:</li>
</ol>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cookie Name</th>
            <th class=""px-4 py-2 text-left"">Expected Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">iv</td><td class=""px-4 py-2"">UUID format (e.g., a1b2c3d4-e5f6-7890-abcd-ef1234567890)</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">is</td><td class=""px-4 py-2"">Session identifier</td></tr>
    </tbody>
</table>

<h3>Verifying the Request Details</h3>
<p>Click on the ia.gif request to inspect its details:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Request URL Parameters:
- c: Client ID
- v: Visitor ID (from iv cookie)
- s: Session ID (from is cookie)
- u: Current page URL
- r: Referrer URL
- t: Timestamp
</pre>

<h3>Common Issues and Solutions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Possible Cause</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">No ia.gif request</td><td class=""px-4 py-2"">Script not loaded</td><td class=""px-4 py-2"">Check script placement and client ID</td></tr>
        <tr><td class=""px-4 py-2"">Script error in console</td><td class=""px-4 py-2"">Invalid client ID</td><td class=""px-4 py-2"">Verify client ID with Optimizely</td></tr>
        <tr><td class=""px-4 py-2"">No iv cookie</td><td class=""px-4 py-2"">Cookie blocked</td><td class=""px-4 py-2"">Check cookie consent, CSP headers</td></tr>
        <tr><td class=""px-4 py-2"">ia.gif returns 403</td><td class=""px-4 py-2"">Authentication issue</td><td class=""px-4 py-2"">Verify API credentials</td></tr>
        <tr><td class=""px-4 py-2"">Intermittent tracking</td><td class=""px-4 py-2"">Ad blocker</td><td class=""px-4 py-2"">Test with ad blockers disabled</td></tr>
    </tbody>
</table>

<h3>Console Commands for Debugging</h3>
<p>You can check the tracking SDK status in the browser console:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Check if the SDK is loaded
console.log(typeof _iaq);  // Should be 'function' or 'object'

// Check the command queue
console.log(_iaq.q);  // Shows queued commands

// Check cookies via JavaScript
console.log(document.cookie);  // Look for 'iv=' and 'is='
</pre>

<h3>Testing Checklist</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>☐ ia.gif request visible in Network tab</li>
        <li>☐ ia.gif returns status 200</li>
        <li>☐ iv cookie created on your domain</li>
        <li>☐ is cookie created for the session</li>
        <li>☐ No JavaScript errors in console</li>
        <li>☐ Tracking works on multiple pages</li>
        <li>☐ Tracking works across page navigation</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ti-cross-platform-tracking",
                    ModuleId = "tracking-implementation",
                    Title = "Cross-Platform Tracking",
                    Summary = "Understand how Content Recommendations integrates with other Optimizely products.",
                    Order = 6,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand cross-platform visitor identification",
                        "Learn about Marketing Automation integration",
                        "Know how device IDs are shared",
                        "Understand the benefits of unified tracking"
                    },
                    Content = @"
<h2>Cross-Platform Tracking</h2>
<p>Content Recommendations can automatically integrate with other Optimizely products to provide unified visitor tracking across your digital ecosystem.</p>

<h3>Marketing Automation Integration</h3>
<p>If your site uses Optimizely Marketing Automation, the Content Recommendations tracking SDK automatically detects and integrates with it.</p>

<h4>How It Works</h4>
<p>When Marketing Automation is active, it sets a cookie called <code>_madid</code> (Marketing Automation Device ID). The Content Recommendations SDK:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Detects the presence of the _madid cookie</li>
    <li>Reads the device ID value</li>
    <li>Includes it in tracking data as <code>epi_device_id</code></li>
</ol>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Zero Configuration</p>
    <p class=""text-green-700 dark:text-green-300"">No configuration is required for this integration. If the _madid cookie exists, it will be automatically included in all tracking events.</p>
</div>

<h3>Benefits of Cross-Platform Tracking</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Unified Profiles</td><td class=""px-4 py-2"">Link content interests with marketing automation data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Better Personalisation</td><td class=""px-4 py-2"">Use combined data for more relevant experiences</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cross-Channel Insights</td><td class=""px-4 py-2"">Understand visitor behaviour across touchpoints</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email Targeting</td><td class=""px-4 py-2"">Use content interests for email personalisation</td></tr>
    </tbody>
</table>

<h3>ODP Integration</h3>
<p>When integrated with Optimizely Data Platform (ODP), Content Recommendations can:</p>
<ul>
    <li>Push visitor interest profiles to ODP customer records</li>
    <li>Populate the top 3 topic interests on customer profiles</li>
    <li>Enable segment creation based on content interests</li>
    <li>Trigger campaigns based on content engagement</li>
</ul>

<h3>Cookie Summary</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cookie</th>
            <th class=""px-4 py-2 text-left"">Product</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">iv</td><td class=""px-4 py-2"">Content Recommendations</td><td class=""px-4 py-2"">Visitor ID</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">is</td><td class=""px-4 py-2"">Content Recommendations</td><td class=""px-4 py-2"">Session ID</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">_madid</td><td class=""px-4 py-2"">Marketing Automation</td><td class=""px-4 py-2"">Device ID (shared)</td></tr>
    </tbody>
</table>

<h3>Data Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│    Website      │     │    Content      │     │      ODP        │
│   Visitor       │────▶│ Recommendations │────▶│  Customer       │
│                 │     │   (Profiles)    │     │   Profiles      │
└─────────────────┘     └─────────────────┘     └─────────────────┘
        │                       │                       │
        │     ┌─────────────────┴───────────┐          │
        └────▶│   Marketing Automation      │◀─────────┘
              │   (Campaigns & Emails)      │
              └─────────────────────────────┘
</pre>

<h3>Privacy Considerations</h3>
<p>Cross-platform tracking respects privacy settings:</p>
<ul>
    <li>All cookies are first-party</li>
    <li>Data is anonymised by default</li>
    <li>Consent management is respected across products</li>
    <li>Users can opt out via cookie preferences</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 4: Visitor Profiles

    private LearningModule BuildVisitorProfilesModule()
    {
        return new LearningModule
        {
            Id = "visitor-profiles",
            Title = "Visitor Profiles & Interest Tracking",
            Description = "Understand how visitor profiles are built and how interests are tracked over time.",
            Icon = "user-circle",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "vp-visitor-identity",
                    ModuleId = "visitor-profiles",
                    Title = "Visitor Identity Management",
                    Summary = "Understand how visitors are identified and tracked across sessions.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand how visitor IDs are generated and stored",
                        "Learn about anonymous vs identified visitors",
                        "Know how returning visitors are recognised",
                        "Understand the visitor identity lifecycle"
                    },
                    Content = @"
<h2>Visitor Identity Management</h2>
<p>Content Recommendations uses a robust visitor identification system to track individuals across sessions and build comprehensive interest profiles over time.</p>

<h3>Anonymous Visitor Tracking</h3>
<p>Content Recommendations tracks visitors <strong>anonymously</strong> by default. No personally identifiable information (PII) is required to build and maintain visitor profiles.</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Privacy First</p>
    <p class=""text-blue-700 dark:text-blue-300"">Visitors are identified by a randomly generated UUID, not by personal data like email or name. Interest profiles are built from aggregated content interactions, not from personal information.</p>
</div>

<h3>Visitor ID Generation</h3>
<p>When a visitor first arrives on your site:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>The tracking SDK checks for an existing <code>iv</code> cookie</li>
    <li>If none exists, a new UUID4 is generated</li>
    <li>The UUID is stored in the <code>iv</code> (idio visitor) cookie</li>
    <li>This ID persists for 2 years</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example visitor ID (UUID4 format)
iv = ""a1b2c3d4-e5f6-7890-abcd-ef1234567890""

// Cookie properties
Domain: .yourdomain.com
Path: /
Expires: 2 years from creation
HttpOnly: No (accessible by JavaScript)
Secure: Yes (on HTTPS sites)
SameSite: Lax
</pre>

<h3>Session Management</h3>
<p>In addition to visitor identity, sessions are tracked separately:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Visitor (iv)</th>
            <th class=""px-4 py-2 text-left"">Session (is)</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Purpose</td><td class=""px-4 py-2"">Long-term identity</td><td class=""px-4 py-2"">Current browsing session</td></tr>
        <tr><td class=""px-4 py-2"">Duration</td><td class=""px-4 py-2"">2 years</td><td class=""px-4 py-2"">30 minutes (sliding)</td></tr>
        <tr><td class=""px-4 py-2"">Renewal</td><td class=""px-4 py-2"">On each visit</td><td class=""px-4 py-2"">On each page view</td></tr>
        <tr><td class=""px-4 py-2"">Used for</td><td class=""px-4 py-2"">Interest profile building</td><td class=""px-4 py-2"">Session-level analytics</td></tr>
    </tbody>
</table>

<h3>Returning Visitor Recognition</h3>
<p>When a visitor returns to your site:</p>
<ul>
    <li>The SDK reads the existing <code>iv</code> cookie</li>
    <li>The visitor is linked to their existing profile</li>
    <li>New interactions are added to their interest history</li>
    <li>Recommendations are personalised immediately based on past behaviour</li>
</ul>

<h3>Identity Linking</h3>
<p>When visitors log in or provide their email, you can link anonymous profiles to known identities:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Link visitor to email address
_iaq.push(['_setEmail', 'user@example.com']);

// Link to Marketing Automation ID
_iaq.push(['_setMaId', 'ma-device-id-12345']);

// Link to custom identifier
_iaq.push(['_setUserId', 'customer-123']);
</pre>

<h3>Cross-Device Considerations</h3>
<p>By default, visitor profiles are device-specific because cookies don't transfer between devices. To enable cross-device profiles:</p>
<ul>
    <li>Prompt users to log in on each device</li>
    <li>Use identity linking to merge profiles</li>
    <li>Integrate with ODP for unified customer profiles</li>
</ul>

<h3>Profile Data Retention</h3>
<p>Content Recommendations retains visitor profile data according to these guidelines:</p>
<ul>
    <li><strong>Active profiles</strong> - Maintained as long as the visitor returns</li>
    <li><strong>Inactive profiles</strong> - May be archived after extended inactivity</li>
    <li><strong>Data export</strong> - Available via API for compliance requests</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "vp-session-management",
                    ModuleId = "visitor-profiles",
                    Title = "Session Management",
                    Summary = "Learn how sessions are tracked and used for analytics.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand session definition and duration",
                        "Learn how session data is used",
                        "Know the difference between visits and sessions",
                        "Understand session timeout behaviour"
                    },
                    Content = @"
<h2>Session Management</h2>
<p>Sessions group visitor interactions into discrete browsing periods, enabling session-level analytics and behaviour analysis.</p>

<h3>What is a Session?</h3>
<p>A session represents a single browsing period where a visitor actively engages with your website. Sessions help distinguish between:</p>
<ul>
    <li>A visitor viewing 5 pages in one sitting</li>
    <li>A visitor making 5 separate visits over a week</li>
</ul>

<h3>Session Timeout</h3>
<p>The session cookie (<code>is</code>) uses a <strong>30-minute sliding window</strong>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Session Timeline Example:

10:00 - Visitor arrives → Session starts
10:05 - Views article A → Session extended to 10:35
10:15 - Views article B → Session extended to 10:45
10:20 - Views article C → Session extended to 10:50

[Visitor leaves]

11:00 - Visitor returns → New session starts
</pre>

<h3>Session Data Captured</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Data Point</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Session ID</td><td class=""px-4 py-2"">Unique identifier for the session</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Start Time</td><td class=""px-4 py-2"">When the session began</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Page Views</td><td class=""px-4 py-2"">Number and sequence of pages viewed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Entry Page</td><td class=""px-4 py-2"">First page of the session</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Referrer</td><td class=""px-4 py-2"">Traffic source for the session</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">UTM Parameters</td><td class=""px-4 py-2"">Campaign tracking data</td></tr>
    </tbody>
</table>

<h3>Session vs Visit Terminology</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Session</strong> - A continuous browsing period (technical term)</p>
    <p><strong>Visit</strong> - Often used interchangeably with session in analytics</p>
    <p><strong>Page View</strong> - A single page load within a session</p>
    <p><strong>Interaction</strong> - Any tracked event (page view, click, conversion)</p>
</div>

<h3>Session Analytics Uses</h3>
<p>Session data enables important analytics:</p>
<ul>
    <li><strong>Engagement Depth</strong> - How many pages per session</li>
    <li><strong>Session Duration</strong> - How long visitors stay</li>
    <li><strong>Bounce Rate</strong> - Single-page sessions</li>
    <li><strong>Content Journeys</strong> - Path through content</li>
    <li><strong>Conversion Attribution</strong> - Which session led to conversion</li>
</ul>

<h3>Session-Based Triggers</h3>
<p>Content Recommendations supports in-session triggers that fire based on session behaviour:</p>
<ul>
    <li><strong>Abandoned Browse</strong> - Left without converting</li>
    <li><strong>High Engagement</strong> - Viewed many pages</li>
    <li><strong>Topic Focus</strong> - Multiple pages on same topic</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">For in-session triggers, only one in-session trigger is allowed per session. If multiple trigger criteria are met simultaneously, trigger prioritisation determines which one fires.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "vp-building-interest-profiles",
                    ModuleId = "visitor-profiles",
                    Title = "Building Interest Profiles",
                    Summary = "Understand how visitor interest profiles are constructed from interactions.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand how interactions build profiles",
                        "Learn how topics are aggregated",
                        "Know how profile strength is calculated",
                        "Understand profile evolution over time"
                    },
                    Content = @"
<h2>Building Interest Profiles</h2>
<p>Content Recommendations builds a unique interest profile for each visitor by aggregating the topics from all content they interact with.</p>

<h3>Profile Construction Process</h3>
<ol class=""list-decimal list-inside space-y-3"">
    <li><strong>Visitor views content</strong> - Page view is tracked</li>
    <li><strong>Content topics retrieved</strong> - NLP-extracted topics for that page</li>
    <li><strong>Topics added to profile</strong> - Visitor's interest profile is updated</li>
    <li><strong>Weights calculated</strong> - Topic importance in profile is recalculated</li>
</ol>

<h3>Profile Data Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Visitor Interest Profile
┌─────────────────────────────────────────┐
│ Visitor ID: a1b2c3d4-e5f6-7890-...      │
├─────────────────────────────────────────┤
│ Interest Topics (weighted):             │
│                                         │
│   Digital Marketing         ████████ 85│
│   Content Strategy          ██████   62│
│   SEO                       █████    51│
│   Email Marketing           ████     43│
│   Analytics                 ███      35│
│   Social Media              ███      32│
│   Conversion Optimisation   ██       28│
│   ...                                   │
├─────────────────────────────────────────┤
│ Total Interactions: 47                  │
│ Unique Topics: 156                      │
│ Profile Age: 45 days                    │
└─────────────────────────────────────────┘
</pre>

<h3>Topic Aggregation</h3>
<p>When a visitor views multiple pieces of content, topics are aggregated:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Content Viewed</th>
            <th class=""px-4 py-2 text-left"">Topics Added</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">""SEO Best Practices""</td><td class=""px-4 py-2"">SEO (0.9), Google (0.6), Keywords (0.5)</td></tr>
        <tr><td class=""px-4 py-2"">""Email Marketing Guide""</td><td class=""px-4 py-2"">Email Marketing (0.85), Conversion (0.4)</td></tr>
        <tr><td class=""px-4 py-2"">""SEO for Beginners""</td><td class=""px-4 py-2"">SEO (0.8), Google (0.5), Content (0.4)</td></tr>
    </tbody>
</table>

<p>Result: SEO becomes a strong interest signal because it appears in multiple interactions.</p>

<h3>Recency and Frequency</h3>
<p>Profile weights consider both recency and frequency:</p>
<ul>
    <li><strong>Frequency</strong> - Topics from multiple interactions gain strength</li>
    <li><strong>Recency</strong> - Recent interactions may carry more weight</li>
    <li><strong>Engagement Depth</strong> - Time on page can influence weight</li>
</ul>

<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">Real-Time Updates</p>
    <p class=""text-purple-700 dark:text-purple-300"">Interest profiles are updated in real-time as visitors browse. This means recommendations can change within the same session based on evolving interests.</p>
</div>

<h3>Profile Quality Factors</h3>
<p>Profile quality improves with:</p>
<ul>
    <li><strong>More interactions</strong> - Larger sample of interests</li>
    <li><strong>Diverse content</strong> - Broader topic coverage</li>
    <li><strong>Recent activity</strong> - Fresh engagement signals</li>
    <li><strong>Consistent patterns</strong> - Clear interest themes</li>
</ul>

<h3>Cold Start Problem</h3>
<p>New visitors have no profile history, creating a ""cold start"" challenge:</p>
<ul>
    <li>First page view: No personalisation possible</li>
    <li>Second page view: Basic profile begins to form</li>
    <li>3-5 page views: Meaningful personalisation emerges</li>
    <li>10+ page views: Strong profile for accurate recommendations</li>
</ul>

<p>Deliveries can be configured to show ""unpersonalised"" content (popular or trending) for visitors with insufficient profile data.</p>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "vp-topic-weighting",
                    ModuleId = "visitor-profiles",
                    Title = "Topic Weighting in Profiles",
                    Summary = "Learn how topic interests are prioritised within visitor profiles.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand how topic weights are calculated",
                        "Learn about interest decay over time",
                        "Know how competing interests are balanced",
                        "Understand weight normalisation"
                    },
                    Content = @"
<h2>Topic Weighting in Profiles</h2>
<p>Not all topics in a visitor's profile carry equal weight. The weighting system ensures that the most relevant interests drive recommendations.</p>

<h3>Weight Calculation Factors</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Factor</th>
            <th class=""px-4 py-2 text-left"">Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Interaction Frequency</td><td class=""px-4 py-2"">Topics appearing in many viewed articles gain weight</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Topic Weight in Content</td><td class=""px-4 py-2"">Stronger topics in content contribute more</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recency</td><td class=""px-4 py-2"">Recent interactions may have higher influence</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Engagement Signals</td><td class=""px-4 py-2"">Deep engagement can boost topic weights</td></tr>
    </tbody>
</table>

<h3>Weight Visualisation</h3>
<p>The Insight Dashboard displays profile weights as a heat map:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Interest Profile Heat Map:

 ██████████████████████  Digital Marketing (92)
 ███████████████████     Content Strategy (78)
 ██████████████          SEO (65)
 █████████               Email Marketing (48)
 ███████                 Analytics (42)
 █████                   Social Media (35)
 ████                    PPC Advertising (28)
 ███                     Web Design (22)
 ██                      JavaScript (15)
</pre>

<h3>Interest Decay</h3>
<p>Over time, older interests may decay if not reinforced:</p>
<ul>
    <li>Active topics (recently engaged) maintain high weight</li>
    <li>Dormant topics (not engaged recently) may decrease</li>
    <li>This prevents stale interests from dominating recommendations</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Dynamic Profiles</p>
    <p class=""text-blue-700 dark:text-blue-300"">As the visitor's interest profile changes with more content consumption, the corresponding recommendations change appropriately. This creates a continuously improving personalised experience.</p>
</div>

<h3>Competing Interests</h3>
<p>When visitors have diverse interests, the system balances them:</p>
<ul>
    <li>Recommendations draw from multiple strong interest areas</li>
    <li>Variety is introduced to avoid repetitive suggestions</li>
    <li>Recent interests may be prioritised for freshness</li>
</ul>

<h3>Topic Relationships</h3>
<p>The knowledge graph understands topic relationships:</p>
<ul>
    <li><strong>Parent/Child</strong> - ""Digital Marketing"" contains ""Email Marketing""</li>
    <li><strong>Related</strong> - ""SEO"" is related to ""Content Marketing""</li>
    <li><strong>Synonyms</strong> - ""B2B"" and ""Business-to-Business"" are equivalent</li>
</ul>

<p>This enables recommendations even for topics not directly in the profile but related to strong interests.</p>

<h3>Normalisation</h3>
<p>Profile weights are normalised to enable comparison:</p>
<ul>
    <li>Weights are typically scaled 0-100</li>
    <li>Allows comparison across visitors with different interaction volumes</li>
    <li>Enables threshold-based segmentation</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "vp-profile-analytics",
                    ModuleId = "visitor-profiles",
                    Title = "Profile Data & Analytics",
                    Summary = "Explore how to view and analyse visitor profile data.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Navigate profile data in the dashboard",
                        "Understand profile analytics views",
                        "Know how to search and filter profiles",
                        "Use profile data for insights"
                    },
                    Content = @"
<h2>Profile Data & Analytics</h2>
<p>The Content Recommendations portal provides tools to explore and analyse visitor profiles, helping you understand your audience and validate personalisation effectiveness.</p>

<h3>Accessing Profile Data</h3>
<p>Profile data is available in the Insight Dashboard under the <strong>Profiles</strong> view:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Insight</strong> in the portal</li>
    <li>Select the <strong>Profiles</strong> view</li>
    <li>Browse or search for specific profiles</li>
</ol>

<h3>Profile List View</h3>
<p>The Profiles view shows individuals that interacted with your content, ordered from most to least interaction:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Column</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Profile ID</td><td class=""px-4 py-2"">The visitor's unique identifier</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Interactions</td><td class=""px-4 py-2"">Total content interactions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Top Interests</td><td class=""px-4 py-2"">Highest weighted topics</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">First Seen</td><td class=""px-4 py-2"">When the profile was created</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Last Active</td><td class=""px-4 py-2"">Most recent interaction</td></tr>
    </tbody>
</table>

<h3>Individual Profile View</h3>
<p>Clicking on a profile reveals detailed information:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-medium"">Profile Details Include:</h4>
    <ul class=""mt-2 space-y-1"">
        <li><strong>Profile Created Date</strong> - When first tracked</li>
        <li><strong>First Interaction Date</strong> - First content view</li>
        <li><strong>Last Interaction Date</strong> - Most recent activity</li>
        <li><strong>Identifiers</strong> - Content Recommendations ID, email, MA ID</li>
        <li><strong>Conversions</strong> - Goals achieved by this visitor</li>
        <li><strong>Interest Profile</strong> - Full topic heat map</li>
    </ul>
</div>

<h3>Interest Profile Heat Map</h3>
<p>The heat map visualisation shows topic interest strength:</p>
<ul>
    <li><strong>Hot colours</strong> (red/orange) - Strong interests</li>
    <li><strong>Warm colours</strong> (yellow) - Moderate interests</li>
    <li><strong>Cool colours</strong> (blue/green) - Weak interests</li>
</ul>

<h3>Searching Profiles</h3>
<p>You can search for specific profiles by:</p>
<ul>
    <li>Visitor ID (from the iv cookie)</li>
    <li>Email address (if linked)</li>
    <li>Marketing Automation ID</li>
    <li>Custom identifiers</li>
</ul>

<h3>Filtering Profiles</h3>
<p>Filter profiles to find specific segments:</p>
<ul>
    <li>By date range (first seen, last active)</li>
    <li>By interaction count</li>
    <li>By top interest topics</li>
    <li>By conversion status</li>
</ul>

<h3>Using Profile Insights</h3>
<p>Profile analytics can inform content strategy:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Insight</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Common interest patterns</td><td class=""px-4 py-2"">Create content bundles for these topics</td></tr>
        <tr><td class=""px-4 py-2"">High-value profile characteristics</td><td class=""px-4 py-2"">Target similar visitors</td></tr>
        <tr><td class=""px-4 py-2"">Interest gaps</td><td class=""px-4 py-2"">Create content for underserved topics</td></tr>
        <tr><td class=""px-4 py-2"">Conversion patterns</td><td class=""px-4 py-2"">Identify content that drives conversions</td></tr>
    </tbody>
</table>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Privacy Note</p>
    <p class=""text-green-700 dark:text-green-300"">Profile data is anonymised by default. Even when viewing individual profiles, you're seeing aggregated behaviour data, not personal information, unless the visitor has been explicitly identified through login or form submission.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 5: Flows & Sections

    private LearningModule BuildFlowsSectionsModule()
    {
        return new LearningModule
        {
            Id = "flows-sections",
            Title = "Flows & Sections",
            Description = "Learn how to organise content using flows and sections for targeted recommendations.",
            Icon = "funnel",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "fs-understanding-sections",
                    ModuleId = "flows-sections",
                    Title = "Understanding Sections",
                    Summary = "Learn what sections are and how they organise content for recommendations.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of sections",
                        "Learn how sections group content",
                        "Know when to use multiple sections",
                        "Understand section-delivery relationships"
                    },
                    Content = @"
<h2>Understanding Sections</h2>
<p>Sections are <strong>content categories</strong> that group related content together for targeted recommendations. They define the pools of content from which recommendations are drawn.</p>

<h3>What is a Section?</h3>
<p>A section is a category of content defined by the parameters in assigned flows. Think of sections as ""buckets"" that hold content matching specific criteria.</p>

<div class=""bg-blue-50 dark:bg-blue-900/30 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Key Concept</p>
    <p class=""text-blue-700 dark:text-blue-300"">Sections are content pools assigned to widget deliveries to control what content is shown and where. This setup enables flexible targeting and management of recommendations across various parts of your website.</p>
</div>

<h3>Section Examples</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Section Name</th>
            <th class=""px-4 py-2 text-left"">Content Type</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Blog Posts</td><td class=""px-4 py-2"">All blog content</td><td class=""px-4 py-2"">Blog page recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Financial Articles</td><td class=""px-4 py-2"">Finance-related content</td><td class=""px-4 py-2"">Finance section widget</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product Resources</td><td class=""px-4 py-2"">Product documentation</td><td class=""px-4 py-2"">Product page recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Gated Content</td><td class=""px-4 py-2"">Premium resources</td><td class=""px-4 py-2"">Lead generation widgets</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recent News</td><td class=""px-4 py-2"">News from last 30 days</td><td class=""px-4 py-2"">News section widget</td></tr>
    </tbody>
</table>

<h3>Why Use Sections?</h3>
<ul>
    <li><strong>Content Control</strong> - Ensure only appropriate content appears in specific locations</li>
    <li><strong>Relevance</strong> - Match content types to page contexts</li>
    <li><strong>Editorial Control</strong> - Curate what's eligible for recommendations</li>
    <li><strong>Compliance</strong> - Exclude certain content from specific regions or audiences</li>
</ul>

<h3>Section Hierarchy</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
                    All Content
                         │
           ┌─────────────┼─────────────┐
           ▼             ▼             ▼
       Blog Posts    Resources    Case Studies
           │             │             │
      ┌────┴────┐   ┌────┴────┐       │
      ▼         ▼   ▼         ▼       ▼
  Technology  Marketing  Guides  Whitepapers  All Cases
</pre>

<h3>Sections and Deliveries</h3>
<p>Sections are assigned to widget deliveries to control recommendations:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Create sections that group your content logically</li>
    <li>Configure flows to populate sections with content</li>
    <li>Assign sections to delivery widgets</li>
    <li>Widgets only recommend content from assigned sections</li>
</ol>

<h3>Multiple Sections</h3>
<p>A single piece of content can belong to multiple sections if it matches the criteria for each. For example, a ""Marketing Technology Trends"" article might belong to both ""Marketing"" and ""Technology"" sections.</p>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">You must use sections for content pooling when configuring recommendation widgets through deliveries. Without sections, you cannot control which content appears in your widgets.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "fs-creating-sections",
                    ModuleId = "flows-sections",
                    Title = "Creating Sections",
                    Summary = "Learn how to create and configure sections in the portal.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Navigate to section management",
                        "Create new sections",
                        "Configure section properties",
                        "Understand section naming conventions"
                    },
                    Content = @"
<h2>Creating Sections</h2>
<p>Sections are created and managed in the Content Recommendations portal. This lesson walks through the section creation process.</p>

<h3>Accessing Section Management</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Log in to the Content Recommendations portal</li>
    <li>Navigate to <strong>Content</strong> in the main menu</li>
    <li>Select <strong>Sections</strong></li>
</ol>

<h3>Creating a New Section</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li>Click <strong>Create Section</strong> or <strong>+ New</strong></li>
        <li>Enter a <strong>Title</strong> for the section</li>
        <li>Enter a <strong>Description</strong> explaining the section's purpose</li>
        <li>Click <strong>Save</strong></li>
    </ol>
</div>

<h3>Section Properties</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Tips</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Title</td><td class=""px-4 py-2"">Display name for the section</td><td class=""px-4 py-2"">Use descriptive names</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Description</td><td class=""px-4 py-2"">Explains what content belongs here</td><td class=""px-4 py-2"">Document the inclusion criteria</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ID</td><td class=""px-4 py-2"">System-generated identifier</td><td class=""px-4 py-2"">Used in API calls</td></tr>
    </tbody>
</table>

<h3>Naming Conventions</h3>
<p>Adopt consistent naming for easier management:</p>
<ul>
    <li><strong>By Content Type</strong> - ""Blog Posts"", ""Case Studies"", ""Whitepapers""</li>
    <li><strong>By Topic</strong> - ""Marketing Content"", ""Technology Articles""</li>
    <li><strong>By Audience</strong> - ""Enterprise Resources"", ""SMB Content""</li>
    <li><strong>By Region</strong> - ""EMEA Content"", ""APAC Articles""</li>
    <li><strong>By Date</strong> - ""2024 Content"", ""Recent (30 days)""</li>
</ul>

<h3>Section Planning</h3>
<p>Before creating sections, plan your content architecture:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Questions to Consider:
┌─────────────────────────────────────────┐
│ 1. What content types do you have?      │
│ 2. Where will recommendations appear?   │
│ 3. What content should be grouped?      │
│ 4. Are there content restrictions?      │
│ 5. Do you need topic-based sections?    │
│ 6. Are there time-based requirements?   │
└─────────────────────────────────────────┘
</pre>

<h3>Editing Sections</h3>
<p>To edit an existing section:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Content &gt; Sections</strong></li>
    <li>Click on the section to edit</li>
    <li>Modify the title or description</li>
    <li>Click <strong>Save</strong></li>
</ol>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Note</p>
    <p class=""text-blue-700 dark:text-blue-300"">Sections themselves don't contain rules for what content belongs in them. That's the job of <strong>Flows</strong>, which we'll cover in the next lesson. Sections are simply named containers that flows populate with content.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "fs-understanding-flows",
                    ModuleId = "flows-sections",
                    Title = "Understanding Flows",
                    Summary = "Learn what flows are and how they route content into sections.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of flows",
                        "Learn flow structure and components",
                        "Know the available flow actions",
                        "Understand flow evaluation"
                    },
                    Content = @"
<h2>Understanding Flows</h2>
<p>Flows are <strong>rules</strong> that determine how content is organised into sections. They define criteria for including, excluding, or featuring content in your recommendations.</p>

<h3>What is a Flow?</h3>
<p>A flow organises content by using a set of rules that, when matched, trigger a defined action. Think of flows as automated content routing rules.</p>

<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">Flow Structure</p>
    <p class=""text-purple-700 dark:text-purple-300"">IF [content matches criteria] THEN [perform action]</p>
</div>

<h3>Flow Components</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Criteria (Filters)</td><td class=""px-4 py-2"">Conditions that content must match</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Actions</td><td class=""px-4 py-2"">What happens when criteria match</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Target Sections</td><td class=""px-4 py-2"">Which sections receive the content</td></tr>
    </tbody>
</table>

<h3>Available Flow Actions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Action</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Approve</td><td class=""px-4 py-2"">If criteria match, approve the content item for recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Move to Bin</td><td class=""px-4 py-2"">If criteria match, move content to the Bin (exclude from recommendations)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Feature</td><td class=""px-4 py-2"">If criteria match, give content priority in recommendations (even if visitor already interacted)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Add to Sections</td><td class=""px-4 py-2"">Assign matching content to one or more sections</td></tr>
    </tbody>
</table>

<h3>Flow Criteria Types</h3>
<p>Flows can filter content based on:</p>
<ul>
    <li><strong>URL Patterns</strong> - Path contains ""/blog/"", starts with ""/resources/""</li>
    <li><strong>Metadata</strong> - Article type, author, category</li>
    <li><strong>Topics</strong> - Contains topic ""Marketing"", excludes topic ""Internal""</li>
    <li><strong>Date</strong> - Published within last 30 days</li>
    <li><strong>Custom Attributes</strong> - Any captured metadata field</li>
</ul>

<h3>Flow Evaluation</h3>
<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Critical: One-Time Evaluation</p>
    <p class=""text-red-700 dark:text-red-300"">Content is evaluated against each flow <strong>only when it is first imported</strong>. Editing a flow or reprocessing content does NOT re-evaluate that content against flows. Flow changes only affect newly ingested content.</p>
</div>

<h3>Flow Structure Rules</h3>
<ul>
    <li>Flows follow a clear, flat structure</li>
    <li>Flows do not allow nesting</li>
    <li>Create multiple flows for different tasks</li>
    <li>Bundle related flows in a section</li>
</ul>

<h3>Example Flow Logic</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Flow: ""Blog Posts to Blog Section""
┌─────────────────────────────────────────┐
│ CRITERIA:                               │
│   URL contains ""/blog/""                 │
│   AND URL does not contain ""/draft/""   │
│   AND Has topic (any)                   │
│                                         │
│ ACTIONS:                                │
│   ✓ Approve                             │
│   ✓ Add to Section: ""Blog Posts""        │
└─────────────────────────────────────────┘
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "fs-creating-flows",
                    ModuleId = "flows-sections",
                    Title = "Creating Flows",
                    Summary = "Step-by-step guide to creating flows in the portal.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create flows in the portal",
                        "Configure flow criteria",
                        "Set up flow actions",
                        "Test and validate flows"
                    },
                    Content = @"
<h2>Creating Flows</h2>
<p>This lesson walks through creating flows to route content into sections.</p>

<h3>Accessing Flow Management</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Log in to the Content Recommendations portal</li>
    <li>Navigate to <strong>Content</strong> in the main menu</li>
    <li>Select <strong>Flows</strong></li>
</ol>

<h3>Creating a New Flow</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li>Click <strong>Create Flow</strong></li>
        <li>The Create Flow view appears</li>
        <li>Configure criteria and actions (detailed below)</li>
        <li>Click <strong>Save</strong></li>
    </ol>
</div>

<h3>Step 1: Define Criteria</h3>
<p>Add filters to specify which content this flow should match. Build filters from widest to narrowest:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Example: Creating a ""Technology Blog"" flow

Filter 1 (Widest): URL contains ""/blog/""
    ↓ narrows to blog posts only

Filter 2: Topics include ""Technology""
    ↓ narrows to technology-related blogs

Filter 3: Published date within 365 days
    ↓ narrows to recent content

Result: Recent technology blog posts
</pre>

<h3>Available Filter Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Filter Type</th>
            <th class=""px-4 py-2 text-left"">Options</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">URL</td><td class=""px-4 py-2"">Contains, Starts with, Matches regex</td><td class=""px-4 py-2"">URL contains ""/articles/""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Topic</td><td class=""px-4 py-2"">Includes, Excludes</td><td class=""px-4 py-2"">Topics include ""Marketing""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Date</td><td class=""px-4 py-2"">Within last X days, Before, After</td><td class=""px-4 py-2"">Published within 30 days</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Metadata</td><td class=""px-4 py-2"">Equals, Contains</td><td class=""px-4 py-2"">og:type equals ""article""</td></tr>
    </tbody>
</table>

<h3>Step 2: Configure Actions</h3>
<p>Select what happens when content matches the criteria:</p>

<ul>
    <li><strong>Approve</strong> - Enable this to allow matching content in recommendations</li>
    <li><strong>Feature</strong> - Enable to prioritise this content (optional)</li>
    <li><strong>Move to Bin</strong> - Enable to exclude matching content</li>
    <li><strong>Add to Sections</strong> - Select which sections receive this content</li>
</ul>

<h3>Step 3: Assign to Sections</h3>
<p>Click the <strong>Add to Sections</strong> field and select from existing sections. Content matching this flow will be added to all selected sections.</p>

<h3>Flow Example: Complete Configuration</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Flow Name: ""Marketing Resources""

CRITERIA:
├── URL contains ""/resources/""
├── Topics include ""Marketing""
└── Published within last 180 days

ACTIONS:
├── ✓ Approve
├── ✗ Feature (disabled)
├── ✗ Move to Bin (disabled)
└── ✓ Add to Sections:
    ├── ""Marketing Content""
    └── ""All Resources""
</pre>

<h3>Testing Flows</h3>
<p>After creating a flow, verify it works:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Content &gt; Content List</strong></li>
    <li>Filter by the target section</li>
    <li>Verify expected content appears</li>
    <li>Check that excluded content is absent</li>
</ol>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Remember</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Flows only evaluate content at import time. Existing content won't be re-evaluated when you create or modify a flow. To apply a new flow to existing content, you would need to manually move content or wait for it to be re-imported.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "fs-best-practices",
                    ModuleId = "flows-sections",
                    Title = "Flow Best Practices",
                    Summary = "Learn strategies for effective flow configuration.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Apply widest-to-narrowest filtering",
                        "Avoid common flow mistakes",
                        "Organise flows effectively",
                        "Maintain flow configurations"
                    },
                    Content = @"
<h2>Flow Best Practices</h2>
<p>Well-designed flows ensure your content is properly organised and recommendations are relevant. Follow these best practices for effective flow management.</p>

<h3>1. Filter from Widest to Narrowest</h3>
<p>Build flow criteria by starting with broad filters and progressively narrowing:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
GOOD: Widest to Narrowest
┌─────────────────────────────────┐
│ 1. URL contains ""/blog/""        │ ← Wide: all blog content
│ 2. Topics include ""Technology"" │ ← Narrower: tech blogs
│ 3. Published last 90 days       │ ← Narrowest: recent tech blogs
└─────────────────────────────────┘

BAD: Random Order
┌─────────────────────────────────┐
│ 1. Published last 90 days       │ ← Could match anything
│ 2. URL contains ""/blog/""        │ ← Now narrows
│ 3. Topics include ""Technology"" │ ← Final filter
└─────────────────────────────────┘
</pre>

<h3>2. Use Clear Naming Conventions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Pattern</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">[Content Type] to [Section]</td><td class=""px-4 py-2"">""Blog Posts to Blog Section""</td></tr>
        <tr><td class=""px-4 py-2"">[Topic] [Content Type]</td><td class=""px-4 py-2"">""Marketing Articles""</td></tr>
        <tr><td class=""px-4 py-2"">Exclude [Content Type]</td><td class=""px-4 py-2"">""Exclude Internal Pages""</td></tr>
        <tr><td class=""px-4 py-2"">Feature [Campaign]</td><td class=""px-4 py-2"">""Feature Q4 Campaign""</td></tr>
    </tbody>
</table>

<h3>3. Create Exclusion Flows</h3>
<p>Use ""Move to Bin"" flows to explicitly exclude unwanted content:</p>
<ul>
    <li>Admin/internal pages</li>
    <li>Outdated content</li>
    <li>Draft or staging content</li>
    <li>Error pages</li>
    <li>Duplicate content</li>
</ul>

<h3>4. Don't Over-Complicate</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Keep It Simple</p>
    <p class=""text-blue-700 dark:text-blue-300"">Flows follow a clear structure and don't allow nesting. Create multiple simple flows for different tasks rather than one complex flow trying to do everything.</p>
</div>

<h3>5. Document Your Flows</h3>
<p>Use the description field to document:</p>
<ul>
    <li>What the flow is intended to match</li>
    <li>Why it was created</li>
    <li>Which sections it populates</li>
    <li>Any dependencies or considerations</li>
</ul>

<h3>6. Understand Limitations</h3>
<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Key Limitations</p>
    <ul class=""mt-2 text-red-700 dark:text-red-300"">
        <li>Flows only evaluate at import time</li>
        <li>Flows cannot remove content from sections (only add)</li>
        <li>Reprocessing doesn't re-evaluate flows</li>
        <li>Section removal must be done manually</li>
    </ul>
</div>

<h3>7. Regular Maintenance</h3>
<p>Periodically review your flows:</p>
<ul>
    <li>Remove flows for discontinued content types</li>
    <li>Update date-based flows as time passes</li>
    <li>Verify sections contain expected content</li>
    <li>Check for orphaned or unused sections</li>
</ul>

<h3>8. Test Before Relying</h3>
<p>Before launching recommendations with new flows:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Create the flow</li>
    <li>Add new test content that should match</li>
    <li>Verify it appears in the correct sections</li>
    <li>Check exclusion flows work as expected</li>
</ol>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 6: Widget Deliveries

    private LearningModule BuildWidgetDeliveriesModule()
    {
        return new LearningModule
        {
            Id = "widget-deliveries",
            Title = "Widget Deliveries",
            Description = "Configure and deploy recommendation widgets on your website.",
            Icon = "rectangle-group",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "wd-understanding-deliveries",
                    ModuleId = "widget-deliveries",
                    Title = "Understanding Deliveries",
                    Summary = "Learn what deliveries are and how they serve recommendations.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of deliveries",
                        "Learn how deliveries connect sections to widgets",
                        "Know the types of deliveries available",
                        "Understand the delivery-widget relationship"
                    },
                    Content = @"
<h2>Understanding Deliveries</h2>
<p>Deliveries are the configuration layer that controls how and where recommendations are displayed on your website. They connect content sections to the widgets that render recommendations.</p>

<h3>What is a Delivery?</h3>
<p>A delivery defines:</p>
<ul>
    <li><strong>Which content</strong> - The sections from which recommendations are drawn</li>
    <li><strong>How it's presented</strong> - Template and layout settings</li>
    <li><strong>Where it appears</strong> - Widget placement configuration</li>
    <li><strong>Who sees it</strong> - Personalised vs unpersonalised content rules</li>
</ul>

<h3>Delivery Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────┐
│                      DELIVERY                                │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  Content Sources              Widget Output                  │
│  ┌───────────────┐           ┌───────────────┐              │
│  │   Section A   │──────────▶│               │              │
│  │   Section B   │           │    Widget     │──────▶ Page  │
│  │   Section C   │──────────▶│               │              │
│  └───────────────┘           └───────────────┘              │
│                                                              │
│  Configuration:                                              │
│  • Template/Layout                                           │
│  • Number of items                                           │
│  • Personalised vs Unpersonalised                           │
│  • Ranking rules                                             │
│                                                              │
└─────────────────────────────────────────────────────────────┘
</pre>

<h3>Types of Deliveries</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Widget Delivery</td><td class=""px-4 py-2"">Renders recommendations in a visual widget</td><td class=""px-4 py-2"">Website sidebars, article footers</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">API Delivery</td><td class=""px-4 py-2"">Returns recommendations via API</td><td class=""px-4 py-2"">Custom implementations, headless sites</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email Delivery</td><td class=""px-4 py-2"">Provides recommendations for email templates</td><td class=""px-4 py-2"">Newsletter personalisation</td></tr>
    </tbody>
</table>

<h3>Widget Characteristics</h3>
<p>Content Recommendation widgets are dynamic blocks rendered by the ip.js SDK:</p>
<ul>
    <li><strong>Real-time</strong> - Recommendations fetched when page loads</li>
    <li><strong>Personalised</strong> - Based on individual visitor profiles</li>
    <li><strong>Configurable</strong> - Multiple templates and layouts available</li>
    <li><strong>Responsive</strong> - Adapt to different screen sizes</li>
</ul>

<h3>Widget Placement Options</h3>
<p>Widgets can be placed in various locations:</p>
<ul>
    <li>Article footers (""You might also like"")</li>
    <li>Sidebars (""Related content"")</li>
    <li>Homepage sections (""Recommended for you"")</li>
    <li>Category pages (""Popular in this category"")</li>
    <li>Exit intent overlays</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Portal Access</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">The Deliveries menu item requires extended user rights. Contact your Content Recommendations administrator if you don't see this option.</p>
</div>

<h3>Delivery Key</h3>
<p>Each delivery has a unique key used to identify it:</p>
<ul>
    <li>Used in widget embed codes</li>
    <li>Referenced in API calls</li>
    <li>Configured in CMS blocks</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wd-creating-deliveries",
                    ModuleId = "widget-deliveries",
                    Title = "Creating Widget Deliveries",
                    Summary = "Step-by-step guide to creating and configuring deliveries.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Navigate to delivery management",
                        "Create a new widget delivery",
                        "Configure delivery settings",
                        "Assign sections to deliveries"
                    },
                    Content = @"
<h2>Creating Widget Deliveries</h2>
<p>This lesson walks through creating a widget delivery to serve recommendations on your website.</p>

<h3>Accessing Delivery Management</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Log in to the Content Recommendations portal</li>
    <li>Navigate to <strong>Engage</strong> in the main menu</li>
    <li>Select <strong>Deliveries</strong></li>
</ol>

<h3>Creating a New Delivery</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li>Click <strong>Create Delivery</strong> or <strong>+ New</strong></li>
        <li>Select the delivery type (Widget, API, or Email)</li>
        <li>Enter a name for the delivery</li>
        <li>Configure the settings (detailed below)</li>
        <li>Click <strong>Save</strong></li>
    </ol>
</div>

<h3>Configuring the Delivery</h3>
<p>The Edit Delivery view contains several configuration areas:</p>

<h4>1. Basic Settings</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Name</td><td class=""px-4 py-2"">Display name for the delivery</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Delivery Key</td><td class=""px-4 py-2"">Unique identifier (auto-generated or custom)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Status</td><td class=""px-4 py-2"">Active or Inactive</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Number of Items</td><td class=""px-4 py-2"">How many recommendations to show</td></tr>
    </tbody>
</table>

<h4>2. Content Sources (Sections)</h4>
<p>Drag sections onto the content areas:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────┐
│ PERSONALISED CONTENT                    │
│ (For visitors with profile data)        │
│ ┌─────────────────────────────────────┐ │
│ │ Drag sections here                  │ │
│ │ • Blog Posts                        │ │
│ │ • Resources                         │ │
│ └─────────────────────────────────────┘ │
├─────────────────────────────────────────┤
│ UNPERSONALISED CONTENT                  │
│ (For new visitors without profile)      │
│ ┌─────────────────────────────────────┐ │
│ │ Drag sections here                  │ │
│ │ • Popular Content                   │ │
│ │ • Featured Articles                 │ │
│ └─────────────────────────────────────┘ │
└─────────────────────────────────────────┘
</pre>

<h4>3. Template Selection</h4>
<p>Choose a template for how recommendations are displayed:</p>
<ul>
    <li>Grid layout (2, 3, or 4 columns)</li>
    <li>List/vertical layout</li>
    <li>Carousel/slider</li>
    <li>Custom templates</li>
</ul>

<h3>Personalised vs Unpersonalised Areas</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Two Content Areas</p>
    <ul class=""mt-2 text-blue-700 dark:text-blue-300"">
        <li><strong>Personalised Content</strong> - Sections for visitors who have existing profile data. Recommendations are tailored to their interests.</li>
        <li><strong>Unpersonalised Content</strong> - Sections shown to first-time visitors with no profile. Typically popular or trending content.</li>
    </ul>
</div>

<h3>Getting the Widget Code</h3>
<p>After saving the delivery, you can get the embed code:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click on the delivery to open it</li>
    <li>Look for the <strong>Embed Code</strong> or <strong>Widget Code</strong> section</li>
    <li>Copy the HTML/JavaScript snippet</li>
    <li>Paste into your website where you want recommendations</li>
</ol>

<h3>Example Widget Code</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;div data-idio-widget=""YOUR_DELIVERY_KEY""&gt;&lt;/div&gt;

&lt;!-- Or with additional options --&gt;
&lt;div data-idio-widget=""YOUR_DELIVERY_KEY""
     data-idio-count=""4""
     data-idio-template=""grid""&gt;
&lt;/div&gt;
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wd-personalised-unpersonalised",
                    ModuleId = "widget-deliveries",
                    Title = "Personalised vs Unpersonalised Content",
                    Summary = "Understand how to handle new visitors vs returning visitors.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the cold start problem",
                        "Configure unpersonalised fallback content",
                        "Balance personalisation with content discovery",
                        "Know when to use each content type"
                    },
                    Content = @"
<h2>Personalised vs Unpersonalised Content</h2>
<p>Deliveries allow you to configure different content strategies for visitors with profile data versus those without.</p>

<h3>The Cold Start Problem</h3>
<p>New visitors present a challenge because they have no interest profile yet. Without profile data, true personalisation isn't possible.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4>Visitor Profile Development:</h4>
    <ul class=""mt-2 space-y-2"">
        <li><strong>0 page views</strong> - No profile, cannot personalise</li>
        <li><strong>1 page view</strong> - Minimal profile, limited signals</li>
        <li><strong>2-5 page views</strong> - Profile forming, basic personalisation</li>
        <li><strong>5+ page views</strong> - Good profile, meaningful personalisation</li>
    </ul>
</div>

<h3>Two Content Areas in Deliveries</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Area</th>
            <th class=""px-4 py-2 text-left"">When Used</th>
            <th class=""px-4 py-2 text-left"">Content Strategy</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Personalised</td><td class=""px-4 py-2"">Returning visitors with profile data</td><td class=""px-4 py-2"">Sections matched to interests</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Unpersonalised</td><td class=""px-4 py-2"">New visitors, first visit</td><td class=""px-4 py-2"">Popular, trending, or curated content</td></tr>
    </tbody>
</table>

<h3>Strategies for Unpersonalised Content</h3>
<ul>
    <li><strong>Popular Content</strong> - Most viewed or shared articles</li>
    <li><strong>Trending</strong> - Currently hot topics</li>
    <li><strong>Editorial Picks</strong> - Curated featured content</li>
    <li><strong>Recent Content</strong> - Latest published articles</li>
    <li><strong>Evergreen Content</strong> - Timeless, always-relevant pieces</li>
</ul>

<h3>Configuration Example</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Delivery: ""Homepage Recommendations""

PERSONALISED CONTENT:
├── Section: ""All Blog Posts""
├── Section: ""Resources""
└── Section: ""Case Studies""
    → Recommendations based on visitor's topic interests

UNPERSONALISED CONTENT:
├── Section: ""Popular This Week""
└── Section: ""Editor's Picks""
    → Shown to new visitors without profile
</pre>

<h3>Balancing the Experience</h3>
<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">Best Practice</p>
    <p class=""text-purple-700 dark:text-purple-300"">Even for personalised visitors, consider including a mix of personalised and trending/popular content. This helps visitors discover content outside their typical interests and prevents filter bubbles.</p>
</div>

<h3>Transition from Unpersonalised to Personalised</h3>
<p>The system automatically transitions visitors:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>First visit: Unpersonalised content shown</li>
    <li>Visitor views content, profile begins building</li>
    <li>Second page view onwards: Personalisation kicks in</li>
    <li>Profile strengthens with each interaction</li>
</ol>

<h3>Testing Both Experiences</h3>
<p>To test the unpersonalised experience:</p>
<ul>
    <li>Open an incognito/private browser window</li>
    <li>Clear cookies to reset your profile</li>
    <li>Visit the page with the widget</li>
    <li>Observe the unpersonalised recommendations</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wd-widget-templates",
                    ModuleId = "widget-deliveries",
                    Title = "Widget Placement & Templates",
                    Summary = "Learn about widget layouts and placement strategies.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Know the available widget templates",
                        "Understand placement best practices",
                        "Learn responsive design considerations",
                        "Customise widget appearance"
                    },
                    Content = @"
<h2>Widget Placement & Templates</h2>
<p>The visual presentation of recommendations affects engagement. Choose appropriate templates and placements for your content and design.</p>

<h3>Available Templates</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Template</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Grid</td><td class=""px-4 py-2"">Cards in 2-4 column grid</td><td class=""px-4 py-2"">Article footers, wide areas</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">List</td><td class=""px-4 py-2"">Vertical stack of items</td><td class=""px-4 py-2"">Sidebars, narrow spaces</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Carousel</td><td class=""px-4 py-2"">Horizontal slider</td><td class=""px-4 py-2"">Homepage features</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Minimal</td><td class=""px-4 py-2"">Text links only</td><td class=""px-4 py-2"">In-content suggestions</td></tr>
    </tbody>
</table>

<h3>Placement Strategies</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Page Layout with Widget Placements:

┌─────────────────────────────────────────────────────┐
│                    HEADER                            │
├───────────────────────────────────┬─────────────────┤
│                                   │    SIDEBAR      │
│                                   │ ┌─────────────┐ │
│         MAIN CONTENT              │ │   Widget    │ │
│                                   │ │   (List)    │ │
│         Article body...           │ │             │ │
│                                   │ └─────────────┘ │
│                                   │                 │
├───────────────────────────────────┴─────────────────┤
│              ARTICLE FOOTER WIDGET                   │
│         ┌───────┐ ┌───────┐ ┌───────┐              │
│         │ Rec 1 │ │ Rec 2 │ │ Rec 3 │              │
│         └───────┘ └───────┘ └───────┘              │
├─────────────────────────────────────────────────────┤
│                    FOOTER                            │
└─────────────────────────────────────────────────────┘
</pre>

<h3>Placement Best Practices</h3>
<ul>
    <li><strong>Article Footer</strong> - High-value placement, reader has finished content</li>
    <li><strong>Sidebar</strong> - Visible throughout reading, good for ""related"" content</li>
    <li><strong>In-Content</strong> - Mid-article suggestions for long content</li>
    <li><strong>Homepage</strong> - Personalised sections for returning visitors</li>
    <li><strong>Exit Intent</strong> - Last chance to engage before leaving</li>
</ul>

<h3>Responsive Considerations</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Mobile Optimisation</p>
    <ul class=""mt-2 text-blue-700 dark:text-blue-300"">
        <li>Widgets automatically adapt to screen size</li>
        <li>Grid layouts collapse to fewer columns on mobile</li>
        <li>Consider mobile-first placement decisions</li>
        <li>Test on multiple device sizes</li>
    </ul>
</div>

<h3>Styling Customisation</h3>
<p>Widgets can be styled to match your brand:</p>
<ul>
    <li>CSS classes for custom styling</li>
    <li>Configurable colours and fonts</li>
    <li>Custom image aspect ratios</li>
    <li>Adjustable spacing and padding</li>
</ul>

<h3>Performance Considerations</h3>
<ul>
    <li>Widgets load asynchronously (don't block page render)</li>
    <li>Images are optimised for web delivery</li>
    <li>Consider lazy loading for below-fold widgets</li>
    <li>Monitor Core Web Vitals impact</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wd-api-deliveries",
                    ModuleId = "widget-deliveries",
                    Title = "API Deliveries",
                    Summary = "Learn how to use API deliveries for custom implementations.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand when to use API deliveries",
                        "Configure API deliveries",
                        "Make API calls for recommendations",
                        "Handle API responses"
                    },
                    Content = @"
<h2>API Deliveries</h2>
<p>API deliveries allow you to fetch recommendations programmatically, enabling custom implementations and headless architectures.</p>

<h3>When to Use API Deliveries</h3>
<ul>
    <li><strong>Headless CMS</strong> - Decoupled front-end implementations</li>
    <li><strong>Mobile Apps</strong> - Native app recommendations</li>
    <li><strong>Custom Widgets</strong> - Non-standard UI requirements</li>
    <li><strong>Server-Side Rendering</strong> - SSR applications</li>
    <li><strong>Third-Party Integrations</strong> - External systems needing recommendations</li>
</ul>

<h3>Creating an API Delivery</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Engage &gt; Deliveries</strong></li>
    <li>Click <strong>Create Delivery</strong></li>
    <li>Select <strong>API</strong> as the delivery type</li>
    <li>Configure sections and settings</li>
    <li>Save and copy the <strong>Delivery Key</strong></li>
</ol>

<h3>API Endpoint</h3>
<p>API deliveries are accessed via regional endpoints:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Region</th>
            <th class=""px-4 py-2 text-left"">Endpoint</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Americas (Default)</td><td class=""px-4 py-2 font-mono text-sm"">api.idio.co</td></tr>
        <tr><td class=""px-4 py-2"">EMEA</td><td class=""px-4 py-2 font-mono text-sm"">api-emea.idio.co</td></tr>
        <tr><td class=""px-4 py-2"">APAC</td><td class=""px-4 py-2 font-mono text-sm"">api-apac.idio.co</td></tr>
        <tr><td class=""px-4 py-2"">Canada</td><td class=""px-4 py-2 font-mono text-sm"">api-ca.idio.co</td></tr>
    </tbody>
</table>

<h3>Making an API Request</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example API call
GET https://api.idio.co/1.0/recommend/{delivery_key}
    ?visitor_id={visitor_id}
    &count={number_of_items}

// Headers
Authorization: Bearer {api_token}
Content-Type: application/json

// Example with fetch
const response = await fetch(
    `https://api.idio.co/1.0/recommend/${deliveryKey}?visitor_id=${visitorId}&count=4`,
    {
        headers: {
            'Authorization': `Bearer ${apiToken}`,
            'Content-Type': 'application/json'
        }
    }
);
const recommendations = await response.json();
</pre>

<h3>API Response Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""items"": [
    {
      ""id"": ""content-123"",
      ""url"": ""https://example.com/article-1"",
      ""title"": ""Article Title"",
      ""description"": ""Article description..."",
      ""image"": ""https://example.com/image.jpg"",
      ""published_date"": ""2024-01-15T10:00:00Z"",
      ""topics"": [""Marketing"", ""Strategy""]
    },
    // ... more items
  ],
  ""meta"": {
    ""visitor_id"": ""abc-123"",
    ""count"": 4,
    ""personalised"": true
  }
}
</pre>

<h3>Passing the Visitor ID</h3>
<p>To get personalised recommendations, pass the visitor ID from the <code>iv</code> cookie:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Get visitor ID from cookie
function getVisitorId() {
    const match = document.cookie.match(/iv=([^;]+)/);
    return match ? match[1] : null;
}

// Use in API call
const visitorId = getVisitorId();
if (visitorId) {
    // Make personalised API call
}
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Without a visitor ID, the API will return unpersonalised recommendations. Always try to pass the visitor ID for the best results.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 7: CMS Integration

    private LearningModule BuildCmsIntegrationModule()
    {
        return new LearningModule
        {
            Id = "cms-integration",
            Title = "CMS Integration",
            Description = "Integrate Content Recommendations with Optimizely CMS.",
            Icon = "puzzle-piece",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ci-nuget-installation",
                    ModuleId = "cms-integration",
                    Title = "Installing the NuGet Package",
                    Summary = "Install and configure the Content Recommendations package for Optimizely CMS.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Install the EPiServer.Personalization.Content.UI package",
                        "Configure the required settings",
                        "Understand package dependencies",
                        "Verify successful installation"
                    },
                    Content = @"
<h2>Installing the NuGet Package</h2>
<p>The Content Recommendations NuGet package provides seamless integration with Optimizely CMS, including automatic tracking injection and a drag-and-drop content block.</p>

<h3>Package Information</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Package Name:</strong> <code>EPiServer.Personalization.Content.UI</code></p>
    <p><strong>NuGet Feed:</strong> Optimizely NuGet (nuget.optimizely.com)</p>
</div>

<h3>Prerequisites</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Before Installing</p>
    <ul class=""mt-2 text-yellow-700 dark:text-yellow-300"">
        <li>✓ Optimizely CMS 11 or CMS 12+ installed</li>
        <li>✓ Website uses <code>RenderRequiredClientResources</code> in the HTML head</li>
        <li>✓ Website is publicly accessible on the internet</li>
        <li>✓ <strong>Widget deliveries are already set up in the portal</strong></li>
        <li>✓ You have Content Recommendations credentials</li>
    </ul>
</div>

<h3>Installation via Package Manager Console</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Install-Package EPiServer.Personalization.Content.UI
</pre>

<h3>Installation via .NET CLI</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
dotnet add package EPiServer.Personalization.Content.UI
</pre>

<h3>Configuration for CMS 12+</h3>
<p>Add settings to <code>appsettings.json</code>:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
{
  ""EPiServer"": {
    ""Personalization"": {
      ""Content"": {
        ""Environment"": ""production"",
        ""ClientId"": ""YOUR_CLIENT_ID"",
        ""ClientName"": ""YOUR_CLIENT_NAME"",
        ""ApiToken"": ""YOUR_API_TOKEN""
      }
    }
  }
}
</pre>

<h3>Configuration for CMS 11</h3>
<p>Add settings to <code>web.config</code>:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;appSettings&gt;
    &lt;add key=""episerver:personalization.content.environment"" value=""production"" /&gt;
    &lt;add key=""episerver:personalization.content.clientid"" value=""YOUR_CLIENT_ID"" /&gt;
    &lt;add key=""episerver:personalization.content.clientname"" value=""YOUR_CLIENT_NAME"" /&gt;
    &lt;add key=""episerver:personalization.content.apitoken"" value=""YOUR_API_TOKEN"" /&gt;
&lt;/appSettings&gt;
</pre>

<h3>Configuration Values</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Where to Find</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Environment</td><td class=""px-4 py-2"">Environment name</td><td class=""px-4 py-2"">Portal settings or Optimizely</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ClientId</td><td class=""px-4 py-2"">Unique client identifier</td><td class=""px-4 py-2"">Provided by Optimizely</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ClientName</td><td class=""px-4 py-2"">Client name</td><td class=""px-4 py-2"">Provided by Optimizely</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ApiToken</td><td class=""px-4 py-2"">API authentication token</td><td class=""px-4 py-2"">Portal settings</td></tr>
    </tbody>
</table>

<h3>Verifying Installation</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Build and run your CMS application</li>
    <li>Check the browser developer tools Network tab for <code>ia.gif</code> requests</li>
    <li>Verify the tracking cookies (<code>iv</code>, <code>is</code>) are created</li>
    <li>Check the CMS admin for the Content Recommendations block</li>
</ol>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Important</p>
    <p class=""text-red-700 dark:text-red-300"">Before installing, ensure widget deliveries are configured in the Content Recommendations portal. Otherwise, the widget delivery dropdown in the CMS block will be empty.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ci-configuration-keys",
                    ModuleId = "cms-integration",
                    Title = "Configuration Keys",
                    Summary = "Understand all available configuration options.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Know all configuration options",
                        "Understand optional vs required settings",
                        "Configure environment-specific settings",
                        "Handle secure credential storage"
                    },
                    Content = @"
<h2>Configuration Keys</h2>
<p>The Content Recommendations package supports several configuration options to customise its behaviour.</p>

<h3>Required Configuration</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Key</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Required</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Environment</td><td class=""px-4 py-2"">Content Recommendations environment</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ClientId</td><td class=""px-4 py-2"">Your unique client ID</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ClientName</td><td class=""px-4 py-2"">Your client name</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ApiToken</td><td class=""px-4 py-2"">API authentication token</td><td class=""px-4 py-2"">Yes</td></tr>
    </tbody>
</table>

<h3>Environment-Specific Configuration</h3>
<p>Use different appsettings files for different environments:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// appsettings.Development.json
{
  ""EPiServer"": {
    ""Personalization"": {
      ""Content"": {
        ""Environment"": ""staging"",
        ""ClientId"": ""dev-client-id"",
        ...
      }
    }
  }
}

// appsettings.Production.json
{
  ""EPiServer"": {
    ""Personalization"": {
      ""Content"": {
        ""Environment"": ""production"",
        ""ClientId"": ""prod-client-id"",
        ...
      }
    }
  }
}
</pre>

<h3>Secure Credential Storage</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Security Best Practice</p>
    <p class=""text-blue-700 dark:text-blue-300"">Don't commit API tokens to source control. Use:</p>
    <ul class=""mt-2 text-blue-700 dark:text-blue-300"">
        <li>Environment variables</li>
        <li>Azure Key Vault</li>
        <li>User secrets (development)</li>
        <li>Optimizely DXP configuration</li>
    </ul>
</div>

<h3>Using Environment Variables</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Environment variables override appsettings
EPiServer__Personalization__Content__ApiToken=your-secret-token

// Or in Azure/DXP, configure via portal
</pre>

<h3>User Secrets (Development)</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Initialize user secrets
dotnet user-secrets init

// Set secret
dotnet user-secrets set ""EPiServer:Personalization:Content:ApiToken"" ""your-token""
</pre>

<h3>Verifying Configuration</h3>
<p>Check your configuration is loaded correctly:</p>
<ul>
    <li>Add logging to trace configuration loading</li>
    <li>Check for tracking script in page source</li>
    <li>Verify API calls work in Network tab</li>
    <li>Test widget delivery dropdown in CMS</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ci-recommendation-blocks",
                    ModuleId = "cms-integration",
                    Title = "Content Recommendation Blocks",
                    Summary = "Use the drag-and-drop recommendation block in the CMS.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Add recommendation blocks to pages",
                        "Configure block settings",
                        "Select widget deliveries",
                        "Preview and publish recommendations"
                    },
                    Content = @"
<h2>Content Recommendation Blocks</h2>
<p>The Content Recommendations package includes a CMS block that editors can drag onto pages to display personalised recommendations.</p>

<h3>Adding the Block to a Page</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Open the page in edit mode</li>
    <li>Navigate to a content area that accepts blocks</li>
    <li>Open the block selector (Assets pane)</li>
    <li>Find <strong>Content Recommendations Block</strong></li>
    <li>Drag the block into the content area</li>
</ol>

<h3>Block Configuration</h3>
<p>The block settings include:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Widget Delivery</td><td class=""px-4 py-2"">Select from configured deliveries</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Title</td><td class=""px-4 py-2"">Optional heading above recommendations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Number of Items</td><td class=""px-4 py-2"">Override default item count</td></tr>
    </tbody>
</table>

<h3>Widget Delivery Selection</h3>
<p>The dropdown shows all active widget deliveries from your Content Recommendations portal:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Example Deliveries:</strong></p>
    <ul class=""mt-2 space-y-1"">
        <li>• Homepage Recommendations</li>
        <li>• Article Footer Widget</li>
        <li>• Sidebar Related Content</li>
        <li>• Resource Page Suggestions</li>
    </ul>
</div>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Empty Dropdown?</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">If the widget delivery dropdown is empty, ensure:</p>
    <ul class=""mt-2 text-yellow-700 dark:text-yellow-300"">
        <li>Widget deliveries exist in the portal</li>
        <li>Deliveries are set to Active status</li>
        <li>API credentials are configured correctly</li>
        <li>The CMS can reach the Content Recommendations API</li>
    </ul>
</div>

<h3>Preview Mode</h3>
<p>When previewing a page:</p>
<ul>
    <li>Recommendations may show unpersonalised content (preview uses fresh session)</li>
    <li>Edit mode may show placeholder content</li>
    <li>Published page shows actual personalised recommendations</li>
</ul>

<h3>Multiple Blocks</h3>
<p>You can add multiple recommendation blocks to a single page:</p>
<ul>
    <li>Use different deliveries for different purposes</li>
    <li>Sidebar widget + footer widget combination</li>
    <li>Different sections for different content areas</li>
</ul>

<h3>Block Placement Strategies</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Article Page Layout:
┌─────────────────────────────────────────┐
│            Header                        │
├─────────────────────┬───────────────────┤
│                     │   Sidebar Block   │
│   Article Content   │   (Related)       │
│                     │                   │
├─────────────────────┴───────────────────┤
│         Footer Block (You May Like)     │
└─────────────────────────────────────────┘
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ci-visitor-groups",
                    ModuleId = "cms-integration",
                    Title = "Visitor Groups Integration",
                    Summary = "Combine Content Recommendations with CMS visitor groups.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand visitor groups in Optimizely CMS",
                        "Combine personalisation approaches",
                        "Create targeted experiences",
                        "Use visitor groups with recommendation blocks"
                    },
                    Content = @"
<h2>Visitor Groups Integration</h2>
<p>Optimizely CMS includes Visitor Groups for rule-based personalisation. You can combine this with Content Recommendations for powerful, multi-layered personalisation.</p>

<h3>What are Visitor Groups?</h3>
<p>Visitor Groups are CMS-native criteria for segmenting visitors:</p>
<ul>
    <li>Geographic location</li>
    <li>Time of day</li>
    <li>Referral source</li>
    <li>Number of visits</li>
    <li>User roles/authentication</li>
    <li>Custom criteria</li>
</ul>

<h3>Combining with Content Recommendations</h3>
<p>You can use visitor groups to control which recommendation blocks visitors see:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Scenario: Different recommendations for new vs returning visitors

New Visitors (Visitor Group):
└── Show: Popular Content Block
    └── Delivery: ""Trending This Week""

Returning Visitors (Visitor Group):
└── Show: Personalised Block
    └── Delivery: ""Personalised for You""
</pre>

<h3>Creating Combined Experiences</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li>Create a Visitor Group in CMS Admin (e.g., ""Returning Visitors"")</li>
        <li>Create different Content Recommendation blocks</li>
        <li>Use CMS personalisation to show different blocks to different visitor groups</li>
    </ol>
</div>

<h3>Example Visitor Group Criteria</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Visitor Group</th>
            <th class=""px-4 py-2 text-left"">Criteria</th>
            <th class=""px-4 py-2 text-left"">Recommendation Delivery</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">New Visitors</td><td class=""px-4 py-2"">First visit</td><td class=""px-4 py-2"">Popular/Trending content</td></tr>
        <tr><td class=""px-4 py-2"">Engaged Users</td><td class=""px-4 py-2"">3+ visits</td><td class=""px-4 py-2"">Deep personalisation</td></tr>
        <tr><td class=""px-4 py-2"">Enterprise</td><td class=""px-4 py-2"">Known B2B domains</td><td class=""px-4 py-2"">Enterprise resources</td></tr>
        <tr><td class=""px-4 py-2"">EMEA Region</td><td class=""px-4 py-2"">Geographic location</td><td class=""px-4 py-2"">Region-specific content</td></tr>
    </tbody>
</table>

<h3>ODP Real-Time Segments</h3>
<p>With ODP integration, you can create even more powerful visitor groups:</p>
<ul>
    <li>Based on Content Recommendations topic interests</li>
    <li>Real-time segment membership</li>
    <li>Cross-channel behaviour data</li>
    <li>Updated with minimal delay (&lt;90 seconds)</li>
</ul>

<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">Powerful Combination</p>
    <p class=""text-purple-700 dark:text-purple-300"">Linking CMS Visitor Groups to ODP Real-Time Segments lets you personalise CMS content based on data from across the Optimizely ecosystem, including Content Recommendations interest profiles.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 8: Dashboards & Analytics

    private LearningModule BuildDashboardsAnalyticsModule()
    {
        return new LearningModule
        {
            Id = "dashboards-analytics",
            Title = "Dashboards & Analytics",
            Description = "Monitor and analyse content performance, visitor engagement, and recommendation effectiveness.",
            Icon = "chart-bar",
            Order = 8,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "da-insight-dashboard",
                    ModuleId = "dashboards-analytics",
                    Title = "The Insight Dashboard",
                    Summary = "Navigate the Insight Dashboard to understand topic and visitor performance.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the Insight Dashboard",
                        "Understand topic performance metrics",
                        "Analyse visitor interaction data",
                        "Use filters effectively"
                    },
                    Content = @"
<h2>The Insight Dashboard</h2>
<p>The Insight Dashboard provides visibility into how visitors interact with your content and which topics drive engagement.</p>

<h3>Accessing the Dashboard</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Log in to the Content Recommendations portal</li>
    <li>Navigate to <strong>Insight</strong> in the main menu</li>
    <li>The dashboard overview appears</li>
</ol>

<h3>Dashboard Views</h3>
<p>The Insight Dashboard includes several views:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">View</th>
            <th class=""px-4 py-2 text-left"">Shows</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Topics in Content</td><td class=""px-4 py-2"">Topic interaction counts (highest to lowest)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Content View</td><td class=""px-4 py-2"">Content item interaction counts</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Profiles View</td><td class=""px-4 py-2"">Visitor profiles ordered by interaction</td></tr>
    </tbody>
</table>

<h3>Topic Performance Graph</h3>
<p>The main visualisation plots topics on a graph of <strong>Volume vs Uniques</strong>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Volume (Total Interactions)
    ▲
    │       ● Marketing (High volume, moderate uniques)
    │
    │   ● SEO             ● Content Strategy
    │
    │           ● Email Marketing
    │       ● Analytics
    │   ● Social Media
    └─────────────────────────────────────────▶
                                    Uniques (Unique Visitors)
</pre>

<h3>Understanding the Metrics</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Insight</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Volume</td><td class=""px-4 py-2"">Total interactions with topic</td><td class=""px-4 py-2"">Overall topic popularity</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Uniques</td><td class=""px-4 py-2"">Unique visitors interacting</td><td class=""px-4 py-2"">Topic reach/breadth</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Ranking</td><td class=""px-4 py-2"">Position by interaction count</td><td class=""px-4 py-2"">Relative importance</td></tr>
    </tbody>
</table>

<h3>Topic Ranking Insights</h3>
<p>Topic ranking helps determine whether content is performing as expected:</p>
<ul>
    <li><strong>High-ranking topics</strong> - Confirm these align with business priorities</li>
    <li><strong>Unexpected top topics</strong> - May indicate trending interests</li>
    <li><strong>Underperforming topics</strong> - May need content improvement or promotion</li>
</ul>

<h3>Filtering Data</h3>
<p>Use filters to focus analysis:</p>
<ul>
    <li><strong>Date Range</strong> - Compare time periods</li>
    <li><strong>Sections</strong> - Filter by content sections</li>
    <li><strong>Topics</strong> - Drill into specific topics</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Pro Tip</p>
    <p class=""text-blue-700 dark:text-blue-300"">Compare different time periods to identify trends. Is a topic growing in popularity? Is another declining? This informs content strategy decisions.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "da-content-dashboard",
                    ModuleId = "dashboards-analytics",
                    Title = "The Content Dashboard",
                    Summary = "Analyse content item performance and NLP processing results.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the Content Dashboard",
                        "Understand content metrics",
                        "Analyse NLP processing results",
                        "Identify content performance patterns"
                    },
                    Content = @"
<h2>The Content Dashboard</h2>
<p>The Content Dashboard provides insights into your content inventory, NLP processing, and individual content item performance.</p>

<h3>Accessing the Dashboard</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Content</strong> in the portal</li>
    <li>Select <strong>Dashboard</strong> or the overview view</li>
</ol>

<h3>Dashboard Metrics</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Total Content Items</td><td class=""px-4 py-2"">Number of ingested content pieces</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Unique Topics</td><td class=""px-4 py-2"">Total distinct topics extracted</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Avg Topics per Item</td><td class=""px-4 py-2"">Average NLP extraction depth</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Processing Status</td><td class=""px-4 py-2"">Content processing queue status</td></tr>
    </tbody>
</table>

<h3>Content List View</h3>
<p>The Content List shows all ingested content with:</p>
<ul>
    <li>Title and URL</li>
    <li>Publish date</li>
    <li>Topic count</li>
    <li>Section membership</li>
    <li>Interaction counts</li>
    <li>Status (approved, binned, etc.)</li>
</ul>

<h3>Individual Content Analysis</h3>
<p>Click on any content item to see detailed information:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Extracted Topics</strong> - All NLP-identified topics with weights</li>
        <li><strong>Metadata</strong> - Title, description, image, URL</li>
        <li><strong>Sections</strong> - Which sections contain this content</li>
        <li><strong>Interactions</strong> - How many times viewed</li>
        <li><strong>Recommendations</strong> - How often recommended and clicked</li>
    </ul>
</div>

<h3>Filtering Content</h3>
<p>Filters help you analyse specific content segments:</p>
<ul>
    <li><strong>By Section</strong> - View content in specific sections</li>
    <li><strong>By Topic</strong> - Find content about specific topics</li>
    <li><strong>By Date</strong> - Recent or older content</li>
    <li><strong>By Status</strong> - Approved, pending, binned</li>
    <li><strong>By Performance</strong> - High/low interaction content</li>
</ul>

<h3>Bulk Actions</h3>
<p>The Content List supports bulk operations:</p>
<ul>
    <li>Select multiple items with checkboxes</li>
    <li><strong>Reprocess</strong> - Rerun NLP analysis</li>
    <li><strong>Move to Bin</strong> - Exclude from recommendations</li>
    <li><strong>Add to Section</strong> - Manually assign to sections</li>
    <li><strong>Remove from Section</strong> - Manually remove from sections</li>
</ul>

<h3>Content Health Indicators</h3>
<p>Watch for these content health signals:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Signal</th>
            <th class=""px-4 py-2 text-left"">Indicates</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Few topics extracted</td><td class=""px-4 py-2"">Thin content</td><td class=""px-4 py-2"">Improve content depth</td></tr>
        <tr><td class=""px-4 py-2"">No interactions</td><td class=""px-4 py-2"">Undiscovered content</td><td class=""px-4 py-2"">Promote or improve SEO</td></tr>
        <tr><td class=""px-4 py-2"">High recommend, low click</td><td class=""px-4 py-2"">Poor metadata/image</td><td class=""px-4 py-2"">Improve og:image/title</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "da-engage-dashboard",
                    ModuleId = "dashboards-analytics",
                    Title = "The Engage Dashboard",
                    Summary = "Monitor delivery and widget performance.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the Engage Dashboard",
                        "Understand delivery metrics",
                        "Monitor widget performance",
                        "Optimise recommendation effectiveness"
                    },
                    Content = @"
<h2>The Engage Dashboard</h2>
<p>The Engage Dashboard tracks how your deliveries and widgets perform, helping you optimise the recommendation experience.</p>

<h3>Accessing the Dashboard</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Engage</strong> in the portal</li>
    <li>View the dashboard overview</li>
</ol>

<h3>Delivery Performance Metrics</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Good Benchmark</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Impressions</td><td class=""px-4 py-2"">Times widget displayed</td><td class=""px-4 py-2"">Depends on traffic</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Clicks</td><td class=""px-4 py-2"">Recommendations clicked</td><td class=""px-4 py-2"">Higher is better</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">CTR</td><td class=""px-4 py-2"">Click-through rate</td><td class=""px-4 py-2"">2-5%+ typical</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Conversions</td><td class=""px-4 py-2"">Goal completions</td><td class=""px-4 py-2"">Depends on goals</td></tr>
    </tbody>
</table>

<h3>Per-Delivery Analysis</h3>
<p>Drill into individual deliveries to see:</p>
<ul>
    <li>Performance trends over time</li>
    <li>Which content gets recommended most</li>
    <li>Which recommended content gets clicked</li>
    <li>Personalised vs unpersonalised performance</li>
</ul>

<h3>Comparing Deliveries</h3>
<p>Compare different deliveries to identify:</p>
<ul>
    <li>Which placements perform best</li>
    <li>Which sections drive most engagement</li>
    <li>Which templates have higher CTR</li>
</ul>

<h3>Optimisation Opportunities</h3>
<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">Improvement Signals</p>
    <table class=""mt-2 w-full text-purple-700 dark:text-purple-300"">
        <tr><td class=""py-1"">Low impressions</td><td class=""py-1"">→</td><td class=""py-1"">Check widget placement/visibility</td></tr>
        <tr><td class=""py-1"">Low CTR</td><td class=""py-1"">→</td><td class=""py-1"">Improve content metadata/images</td></tr>
        <tr><td class=""py-1"">High CTR, low conversion</td><td class=""py-1"">→</td><td class=""py-1"">Content not meeting expectations</td></tr>
    </table>
</div>

<h3>A/B Testing Insights</h3>
<p>If running A/B tests on recommendation widgets, the Engage Dashboard shows:</p>
<ul>
    <li>Performance of each variant</li>
    <li>Statistical significance</li>
    <li>Winning variant recommendations</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "da-content-utilisation",
                    ModuleId = "dashboards-analytics",
                    Title = "Content Utilisation View",
                    Summary = "Understand how effectively your content is being used.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand content utilisation metrics",
                        "Identify underperforming content",
                        "Analyse topic performance zones",
                        "Take action on content insights"
                    },
                    Content = @"
<h2>Content Utilisation View</h2>
<p>The Content Utilisation view helps you understand how effectively your content library is being discovered and consumed.</p>

<h3>The Utilisation Visualisation</h3>
<p>Topics are displayed in coloured zones indicating performance:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Content Utilisation Heat Map:

┌────────────────────────────────────────┐
│  GREEN ZONE (High Performing)          │
│  ██████████████████████████████████    │
│  Topics with strong engagement         │
├────────────────────────────────────────┤
│  BLUE ZONE (Moderate Performing)       │
│  ████████████████████                  │
│  Topics with average engagement        │
├────────────────────────────────────────┤
│  RED ZONE (Low/No Interaction)         │
│  █████████                             │
│  Topics with little or no engagement   │
└────────────────────────────────────────┘
</pre>

<h3>Understanding the Zones</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Zone</th>
            <th class=""px-4 py-2 text-left"">Indicates</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium text-green-600"">Green</td><td class=""px-4 py-2"">High-performing topics</td><td class=""px-4 py-2"">Maintain and expand</td></tr>
        <tr><td class=""px-4 py-2 font-medium text-blue-600"">Blue</td><td class=""px-4 py-2"">Moderate performance</td><td class=""px-4 py-2"">Optimise and promote</td></tr>
        <tr><td class=""px-4 py-2 font-medium text-red-600"">Red</td><td class=""px-4 py-2"">No/low interaction</td><td class=""px-4 py-2"">Investigate or retire</td></tr>
    </tbody>
</table>

<h3>Red Zone Analysis</h3>
<p>Topics in the red zone (no interaction) warrant investigation:</p>
<ul>
    <li><strong>New content</strong> - Recently published, not yet discovered</li>
    <li><strong>Niche topics</strong> - Limited audience but valuable</li>
    <li><strong>Poor discoverability</strong> - SEO or navigation issues</li>
    <li><strong>Irrelevant content</strong> - May not match audience interests</li>
    <li><strong>Outdated content</strong> - No longer relevant</li>
</ul>

<h3>Taking Action</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-medium"">Strategies by Zone:</h4>
    <ul class=""mt-2 space-y-2"">
        <li><strong>Green:</strong> Create more content on these successful topics</li>
        <li><strong>Blue:</strong> Improve metadata, promote content, update articles</li>
        <li><strong>Red:</strong> Promote, improve, or consider retiring</li>
    </ul>
</div>

<h3>Content Gap Analysis</h3>
<p>Use utilisation data to identify content gaps:</p>
<ul>
    <li>Topics visitors search for but don't engage with</li>
    <li>Popular topics with limited content depth</li>
    <li>Emerging topics not yet covered</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Content Strategy Insight</p>
    <p class=""text-blue-700 dark:text-blue-300"">A high red zone percentage indicates potential content strategy issues - either content isn't aligned with audience interests, or discoverability needs improvement.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "da-referrer-analytics",
                    ModuleId = "dashboards-analytics",
                    Title = "Referrer Analytics",
                    Summary = "Analyse traffic sources and campaign performance.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand referrer tracking",
                        "Analyse traffic sources",
                        "Track marketing campaigns",
                        "Optimise acquisition channels"
                    },
                    Content = @"
<h2>Referrer Analytics</h2>
<p>Content Recommendations tracks where visitors come from, helping you understand which traffic sources drive engagement with your content.</p>

<h3>Referrers View</h3>
<p>The Referrers view shows sites that link to your content:</p>
<ul>
    <li>Search engines (Google, Bing)</li>
    <li>Social media platforms</li>
    <li>Partner and referring sites</li>
    <li>Email links</li>
    <li>Direct traffic</li>
</ul>

<h3>Filtering by Referrer</h3>
<p>Filter content performance by referrer to understand:</p>
<ul>
    <li>Which content engages visitors from each source</li>
    <li>Quality of traffic from different referrers</li>
    <li>Content preferences by traffic source</li>
</ul>

<h3>Campaign Tracking</h3>
<p>If you use UTM parameters in your marketing links, the <strong>Referring Campaigns</strong> view tracks campaign performance:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Example URL with UTM parameters:
https://example.com/article?
    utm_source=linkedin&
    utm_medium=social&
    utm_campaign=q4-content-push

Campaign tracked as: ""q4-content-push""
</pre>

<h3>Campaign Analysis</h3>
<p>For each campaign, see:</p>
<ul>
    <li>Total visitors from campaign</li>
    <li>Content they engaged with</li>
    <li>Topics of interest</li>
    <li>Conversion rates</li>
</ul>

<h3>Source vs Content Performance</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Source</th>
            <th class=""px-4 py-2 text-left"">Typical Behaviour</th>
            <th class=""px-4 py-2 text-left"">Recommendation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Organic Search</td><td class=""px-4 py-2"">High intent, specific topics</td><td class=""px-4 py-2"">Deep topic recommendations</td></tr>
        <tr><td class=""px-4 py-2"">Social Media</td><td class=""px-4 py-2"">Broader interests, lower intent</td><td class=""px-4 py-2"">Popular/trending content</td></tr>
        <tr><td class=""px-4 py-2"">Email</td><td class=""px-4 py-2"">Known interests, engaged</td><td class=""px-4 py-2"">Personalised recommendations</td></tr>
        <tr><td class=""px-4 py-2"">Direct</td><td class=""px-4 py-2"">Returning visitors</td><td class=""px-4 py-2"">Continue personalisation</td></tr>
    </tbody>
</table>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Acquisition Insight</p>
    <p class=""text-green-700 dark:text-green-300"">Understanding which content resonates with visitors from each source helps you create better-targeted content and optimise your marketing channel strategy.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 9: Goals & A/B Testing

    private LearningModule BuildGoalsAbTestingModule()
    {
        return new LearningModule
        {
            Id = "goals-ab-testing",
            Title = "Goals & A/B Testing",
            Description = "Track conversions and optimise recommendations through A/B testing.",
            Icon = "beaker",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ga-understanding-goals",
                    ModuleId = "goals-ab-testing",
                    Title = "Understanding Goals",
                    Summary = "Learn what goals are and how they measure recommendation effectiveness.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of goals",
                        "Know what actions can be tracked as conversions",
                        "Learn how goals relate to recommendation performance",
                        "Understand conversion attribution"
                    },
                    Content = @"
<h2>Understanding Goals</h2>
<p>Goals in Content Recommendations help you measure the business impact of your personalisation efforts by tracking desired visitor actions.</p>

<h3>What is a Goal?</h3>
<p>A Goal is a set of behaviours you want visitors to perform. Goals help determine how interested a visitor is in your content and whether recommendations are driving valuable actions.</p>

<h3>Example Goals</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Goal Type</th>
            <th class=""px-4 py-2 text-left"">Examples</th>
            <th class=""px-4 py-2 text-left"">Business Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Lead Generation</td><td class=""px-4 py-2"">Form submission, demo request</td><td class=""px-4 py-2"">Sales pipeline</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Engagement</td><td class=""px-4 py-2"">View 3+ pages, time on site</td><td class=""px-4 py-2"">Brand awareness</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Content Consumption</td><td class=""px-4 py-2"">Download whitepaper, view video</td><td class=""px-4 py-2"">Education, nurturing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Newsletter</td><td class=""px-4 py-2"">Email signup</td><td class=""px-4 py-2"">Audience building</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Contact</td><td class=""px-4 py-2"">Contact form, chat initiation</td><td class=""px-4 py-2"">Direct leads</td></tr>
    </tbody>
</table>

<h3>Goals in the Dashboard</h3>
<p>The Goals section in the portal shows:</p>
<ul>
    <li>Total conversions per goal</li>
    <li>Conversion rate trends</li>
    <li>Content that drives conversions</li>
    <li>Recommendations that led to conversions</li>
</ul>

<h3>Conversion Attribution</h3>
<p>Content Recommendations attributes conversions to understand the recommendation journey:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Conversion Journey Example:

Visitor arrives → Views Article A
                        ↓
            Sees recommendation for Article B
                        ↓
            Clicks recommendation → Views Article B
                        ↓
            Submits demo request form
                        ↓
            GOAL CONVERSION ✓

Attribution: Recommendation to Article B contributed to conversion
</pre>

<h3>Why Goals Matter</h3>
<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">Business Impact</p>
    <p class=""text-purple-700 dark:text-purple-300"">Without goals, you can only measure engagement (clicks, page views). With goals, you can measure whether recommendations drive actual business outcomes.</p>
</div>

<h3>Profile Conversion Data</h3>
<p>Individual visitor profiles show their conversion history:</p>
<ul>
    <li>Which goals they've completed</li>
    <li>When conversions occurred</li>
    <li>Content viewed before conversion</li>
    <li>Recommendations clicked before conversion</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ga-creating-goals",
                    ModuleId = "goals-ab-testing",
                    Title = "Creating Goals",
                    Summary = "Set up goal tracking for your recommendations.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create goals in the portal",
                        "Implement conversion tracking",
                        "Use tag manager for goal tracking",
                        "Test goal configuration"
                    },
                    Content = @"
<h2>Creating Goals</h2>
<p>Goals are tracked by firing conversion events when visitors complete desired actions.</p>

<h3>Goal Configuration in Portal</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Engage</strong> in the portal</li>
    <li>Select <strong>Goals</strong></li>
    <li>Click <strong>Create Goal</strong></li>
    <li>Configure the goal settings</li>
    <li>Save and implement tracking</li>
</ol>

<h3>Goal Settings</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Name</td><td class=""px-4 py-2"">Display name for the goal</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Goal Key</td><td class=""px-4 py-2"">Unique identifier used in tracking code</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Description</td><td class=""px-4 py-2"">What the goal measures</td></tr>
    </tbody>
</table>

<h3>Implementing Conversion Tracking</h3>
<p>Fire a conversion event when a goal is completed:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Track a conversion
_iaq.push(['_trackConversion', 'goal-key']);

// Example: Form submission
document.getElementById('contact-form').addEventListener('submit', function() {
    _iaq.push(['_trackConversion', 'contact-form-submit']);
});

// Example: Button click
document.getElementById('demo-button').addEventListener('click', function() {
    _iaq.push(['_trackConversion', 'demo-request']);
});
</pre>

<h3>Using Google Tag Manager</h3>
<p>Track conversions via GTM:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Create a new Custom HTML tag</li>
    <li>Add the conversion tracking code</li>
    <li>Configure a trigger (form submission, button click, etc.)</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
&lt;script&gt;
// GTM Custom HTML tag for conversion tracking
if (typeof _iaq !== 'undefined') {
    _iaq.push(['_trackConversion', 'goal-key']);
}
&lt;/script&gt;
</pre>

<h3>Common Conversion Triggers</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Action</th>
            <th class=""px-4 py-2 text-left"">GTM Trigger Type</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Form submission</td><td class=""px-4 py-2"">Form Submission trigger</td></tr>
        <tr><td class=""px-4 py-2"">Button click</td><td class=""px-4 py-2"">Click - All Elements trigger</td></tr>
        <tr><td class=""px-4 py-2"">Page view (thank you page)</td><td class=""px-4 py-2"">Page View trigger with URL condition</td></tr>
        <tr><td class=""px-4 py-2"">Scroll depth</td><td class=""px-4 py-2"">Scroll Depth trigger</td></tr>
        <tr><td class=""px-4 py-2"">Time on page</td><td class=""px-4 py-2"">Timer trigger</td></tr>
    </tbody>
</table>

<h3>Testing Goals</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Open browser developer tools (Network tab)</li>
        <li>Complete the goal action</li>
        <li>Look for the conversion tracking call</li>
        <li>Verify the goal key is correct</li>
        <li>Check the portal for the conversion</li>
    </ol>
</div>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Note</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Conversion data may take a few minutes to appear in the portal. Be patient when testing.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ga-ab-testing",
                    ModuleId = "goals-ab-testing",
                    Title = "A/B Testing Recommendation Blocks",
                    Summary = "Optimise recommendations through A/B testing.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand A/B testing for recommendations",
                        "Set up A/B tests",
                        "Analyse test results",
                        "Apply winning variations"
                    },
                    Content = @"
<h2>A/B Testing Recommendation Blocks</h2>
<p>A/B testing allows you to compare different recommendation configurations to find what performs best.</p>

<h3>What Can You Test?</h3>
<ul>
    <li><strong>Widget Placement</strong> - Sidebar vs footer vs in-content</li>
    <li><strong>Design/Template</strong> - Grid vs list vs carousel</li>
    <li><strong>Number of Items</strong> - 3 vs 4 vs 6 recommendations</li>
    <li><strong>Content Sections</strong> - Different section combinations</li>
    <li><strong>Headlines</strong> - ""You might like"" vs ""Related articles""</li>
</ul>

<h3>Built-in A/B Testing</h3>
<p>Content Recommendations includes built-in A/B testing capabilities:</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Feature</p>
    <p class=""text-blue-700 dark:text-blue-300"">Use built-in A/B testing with your recommendation blocks to determine where a block gets the best results on the page. You can monitor a block's performance over the life of your A/B test to determine which placement or design gets the best click-through results.</p>
</div>

<h3>Setting Up an A/B Test</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Create two or more delivery variations</li>
    <li>Configure the A/B test in the portal</li>
    <li>Set traffic allocation (e.g., 50/50)</li>
    <li>Define success metrics (CTR, conversions)</li>
    <li>Launch the test</li>
</ol>

<h3>Key Metrics to Compare</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">What It Tells You</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Click-Through Rate</td><td class=""px-4 py-2"">Which variation attracts more clicks</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Conversion Rate</td><td class=""px-4 py-2"">Which variation drives more goal completions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Engagement Time</td><td class=""px-4 py-2"">Which leads to longer sessions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Pages per Session</td><td class=""px-4 py-2"">Which drives deeper engagement</td></tr>
    </tbody>
</table>

<h3>Statistical Significance</h3>
<p>Before declaring a winner, ensure results are statistically significant:</p>
<ul>
    <li>Run tests long enough to gather sufficient data</li>
    <li>Check confidence levels in results</li>
    <li>Consider sample size requirements</li>
    <li>Account for traffic variations</li>
</ul>

<h3>Applying Results</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Identify the winning variation</li>
    <li>End the test</li>
    <li>Apply the winning configuration to 100% of traffic</li>
    <li>Document learnings for future tests</li>
</ol>

<h3>Testing Best Practices</h3>
<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <ul class=""text-purple-700 dark:text-purple-300 space-y-2"">
        <li>✓ Test one variable at a time</li>
        <li>✓ Run tests for at least 2 weeks</li>
        <li>✓ Ensure adequate sample size</li>
        <li>✓ Document hypotheses before testing</li>
        <li>✓ Learn from both wins and losses</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 10: ODP Integration

    private LearningModule BuildOdpIntegrationModule()
    {
        return new LearningModule
        {
            Id = "odp-integration",
            Title = "ODP Integration",
            Description = "Connect Content Recommendations with Optimizely Data Platform for enhanced personalisation.",
            Icon = "circle-stack",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "oi-odp-overview",
                    ModuleId = "odp-integration",
                    Title = "ODP Overview",
                    Summary = "Understand Optimizely Data Platform and its integration benefits.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what ODP is",
                        "Know the benefits of integration",
                        "Learn how data flows between products",
                        "Understand the use cases"
                    },
                    Content = @"
<h2>Optimizely Data Platform (ODP) Overview</h2>
<p>ODP is Optimizely's customer data platform that unifies customer data across all touchpoints and enables advanced personalisation across the Optimizely ecosystem.</p>

<h3>What is ODP?</h3>
<p>Optimizely Data Platform (ODP) is a Customer Data Platform (CDP) that:</p>
<ul>
    <li>Collects customer data from multiple sources</li>
    <li>Unifies data into single customer profiles</li>
    <li>Enables real-time segmentation</li>
    <li>Activates data across channels</li>
    <li>Integrates with other Optimizely products</li>
</ul>

<h3>Integration Benefits</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Unified Profiles</td><td class=""px-4 py-2"">Content interests added to customer profiles</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cross-Channel Data</td><td class=""px-4 py-2"">Combine web behaviour with email, mobile, etc.</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Advanced Segmentation</td><td class=""px-4 py-2"">Create segments based on content interests</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Real-Time Updates</td><td class=""px-4 py-2"">Profiles update with minimal delay</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Campaign Activation</td><td class=""px-4 py-2"">Use interests to trigger campaigns</td></tr>
    </tbody>
</table>

<h3>Data Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────┐
│                         DATA FLOW                            │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  Website Visitor                                             │
│        │                                                     │
│        ▼                                                     │
│  Content Recommendations                                     │
│  • Tracks content interactions                              │
│  • Builds interest profile                                  │
│  • Extracts top 3 topic interests                          │
│        │                                                     │
│        ▼                                                     │
│  ODP (Customer Data Platform)                               │
│  • Receives interest data (hourly)                          │
│  • Adds to customer profile                                 │
│  • Enables segmentation                                     │
│        │                                                     │
│        ▼                                                     │
│  Activation                                                  │
│  • Email campaigns                                          │
│  • Web personalisation                                      │
│  • Ad targeting                                             │
│                                                              │
└─────────────────────────────────────────────────────────────┘
</pre>

<h3>Use Cases</h3>
<ul>
    <li><strong>Interest-Based Email</strong> - Send content aligned with profile interests</li>
    <li><strong>Lead Scoring</strong> - Score leads based on content engagement</li>
    <li><strong>Account-Based Marketing</strong> - Understand company interests</li>
    <li><strong>Personalised Ads</strong> - Target ads based on content interests</li>
    <li><strong>CMS Personalisation</strong> - Use ODP segments in visitor groups</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "oi-connecting-to-odp",
                    ModuleId = "odp-integration",
                    Title = "Connecting Content Recommendations to ODP",
                    Summary = "Set up the integration between Content Recommendations and ODP.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure the ODP integration",
                        "Set up API key connection",
                        "Verify data flow",
                        "Troubleshoot connection issues"
                    },
                    Content = @"
<h2>Connecting to ODP</h2>
<p>The integration between Content Recommendations and ODP requires configuration in both platforms.</p>

<h3>Prerequisites</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Required</p>
    <ul class=""mt-2 text-yellow-700 dark:text-yellow-300"">
        <li>✓ Active ODP subscription</li>
        <li>✓ Active Content Recommendations subscription</li>
        <li>✓ Admin access to both platforms</li>
        <li>✓ API delivery configured in Content Recommendations</li>
    </ul>
</div>

<h3>Step 1: Get the Delivery Key</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In Content Recommendations, go to <strong>Engage &gt; Deliveries &gt; API</strong></li>
    <li>Find or create an API delivery</li>
    <li>Copy the <strong>Delivery Key</strong></li>
</ol>

<h3>Step 2: Configure ODP</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In ODP, go to <strong>App Directory</strong></li>
    <li>Find <strong>Optimizely Content Recommendations</strong></li>
    <li>Click <strong>Settings</strong></li>
    <li>Paste the Delivery Key into the <strong>API Key</strong> field</li>
    <li>Save the configuration</li>
</ol>

<h3>Data Sync Schedule</h3>
<p>After configuration, Content Recommendations automatically syncs interest data to ODP:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Details</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Sync Frequency</td><td class=""px-4 py-2"">Hourly</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data Transferred</td><td class=""px-4 py-2"">Top 3 topic interests per visitor</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Profile Matching</td><td class=""px-4 py-2"">Via visitor ID or email</td></tr>
    </tbody>
</table>

<h3>Verifying the Integration</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Wait at least one hour after configuration</li>
    <li>In ODP, go to a customer profile</li>
    <li>Look for the Content Recommendations attributes</li>
    <li>Verify topic interests are populated</li>
</ol>

<h3>Troubleshooting</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">No data appearing</td><td class=""px-4 py-2"">Check API key, wait for hourly sync</td></tr>
        <tr><td class=""px-4 py-2"">Missing profiles</td><td class=""px-4 py-2"">Ensure visitor IDs match between systems</td></tr>
        <tr><td class=""px-4 py-2"">Stale data</td><td class=""px-4 py-2"">Verify Content Recommendations is tracking</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "oi-topic-interests-profiles",
                    ModuleId = "odp-integration",
                    Title = "Topic Interests on Customer Profiles",
                    Summary = "Understand how content interests appear on ODP customer profiles.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Find topic interests on ODP profiles",
                        "Understand the data structure",
                        "Use interests for segmentation",
                        "Analyse interest patterns"
                    },
                    Content = @"
<h2>Topic Interests on Customer Profiles</h2>
<p>Once integrated, Content Recommendations populates ODP customer profiles with topic interest data.</p>

<h3>New Profile Attributes</h3>
<p>After integration, three new attributes appear on ODP customer profiles:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">content_interest_1</td><td class=""px-4 py-2"">Top interest topic</td><td class=""px-4 py-2"">""Digital Marketing""</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">content_interest_2</td><td class=""px-4 py-2"">Second interest topic</td><td class=""px-4 py-2"">""Content Strategy""</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">content_interest_3</td><td class=""px-4 py-2"">Third interest topic</td><td class=""px-4 py-2"">""SEO""</td></tr>
    </tbody>
</table>

<h3>AI-Generated Topics (with Opal)</h3>
<p>When Optimizely Opal is enabled, an additional attribute becomes available:</p>

<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <p class=""font-medium text-purple-800 dark:text-purple-200"">AI Generated Topic Interests</p>
    <p class=""text-purple-700 dark:text-purple-300"">Contains up to 9 additional topics of interest in a comma-separated list, generated by asking generative AI to provide related topics based on those populated by Content Recommendations.</p>
</div>

<h3>Viewing Profile Data</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In ODP, navigate to <strong>Customers</strong></li>
    <li>Search for a specific customer</li>
    <li>View their profile</li>
    <li>Look for the Content Recommendations section/attributes</li>
</ol>

<h3>Update Frequency</h3>
<p>Interest data is updated hourly:</p>
<ul>
    <li>As visitors engage with more content, their top interests may change</li>
    <li>ODP profiles reflect the latest interests from the last sync</li>
    <li>Historical interest data is not maintained (only current top 3)</li>
</ul>

<h3>Using Interests for Segmentation</h3>
<p>Create ODP segments based on content interests:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Example Segment: ""Marketing Enthusiasts""
┌─────────────────────────────────────────┐
│ Criteria:                               │
│   content_interest_1 = ""Marketing""     │
│   OR                                    │
│   content_interest_2 = ""Marketing""     │
│   OR                                    │
│   content_interest_3 = ""Marketing""     │
└─────────────────────────────────────────┘
</pre>

<h3>Segment Use Cases</h3>
<ul>
    <li>Email campaigns targeting specific interests</li>
    <li>Personalised ad audiences</li>
    <li>Sales outreach prioritisation</li>
    <li>Content recommendation refinement</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "oi-real-time-segments",
                    ModuleId = "odp-integration",
                    Title = "Real-Time Segments & Personalisation",
                    Summary = "Use ODP real-time segments for advanced CMS personalisation.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand real-time segments",
                        "Create segments based on content interests",
                        "Connect ODP segments to CMS visitor groups",
                        "Build advanced personalisation scenarios"
                    },
                    Content = @"
<h2>Real-Time Segments & Personalisation</h2>
<p>ODP's real-time segments enable powerful personalisation scenarios that combine Content Recommendations data with CMS visitor groups.</p>

<h3>What are Real-Time Segments?</h3>
<p>Real-Time Segments (RTS) in ODP are audience segments that update with minimal delay:</p>
<ul>
    <li>Updated within 90 seconds of data changes</li>
    <li>React to visitor behaviour in real-time</li>
    <li>Can be used for immediate personalisation</li>
</ul>

<h3>Creating Interest-Based Segments</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In ODP, navigate to <strong>Audiences</strong> or <strong>Segments</strong></li>
    <li>Create a new Real-Time Segment</li>
    <li>Add conditions based on content_interest attributes</li>
    <li>Save and activate the segment</li>
</ol>

<h3>Example Segments</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Segment Name</th>
            <th class=""px-4 py-2 text-left"">Criteria</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Tech Enthusiasts</td><td class=""px-4 py-2"">Interest contains ""Technology""</td><td class=""px-4 py-2"">Show tech content prominently</td></tr>
        <tr><td class=""px-4 py-2"">Business Leaders</td><td class=""px-4 py-2"">Interest contains ""Leadership""</td><td class=""px-4 py-2"">Executive content experience</td></tr>
        <tr><td class=""px-4 py-2"">Marketing Pros</td><td class=""px-4 py-2"">Interest contains ""Marketing""</td><td class=""px-4 py-2"">Marketing tool promotions</td></tr>
    </tbody>
</table>

<h3>Connecting to CMS Visitor Groups</h3>
<p>Link ODP segments to Optimizely CMS visitor groups for content personalisation:</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Powerful Combination</p>
    <p class=""text-blue-700 dark:text-blue-300"">Linking CMS Visitor Groups to ODP Real-Time Segments lets you personalise CMS content based on content interests, with updates reflecting in near real-time.</p>
</div>

<h3>Configuration Steps</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Configure ODP integration in CMS</li>
    <li>Create ODP Real-Time Segment</li>
    <li>Create CMS Visitor Group linked to ODP segment</li>
    <li>Use visitor group for content personalisation</li>
</ol>

<h3>Latency Considerations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Step</th>
            <th class=""px-4 py-2 text-left"">Latency</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Content Recommendations → ODP</td><td class=""px-4 py-2"">Up to 1 hour (hourly sync)</td></tr>
        <tr><td class=""px-4 py-2"">ODP segment update</td><td class=""px-4 py-2"">&lt;90 seconds</td></tr>
        <tr><td class=""px-4 py-2"">CMS visitor group check</td><td class=""px-4 py-2"">Real-time</td></tr>
    </tbody>
</table>

<h3>Advanced Scenarios</h3>
<ul>
    <li>Combine interest data with purchase history</li>
    <li>Create lookalike audiences based on high-value customers</li>
    <li>Trigger nurture campaigns when interests change</li>
    <li>Score leads based on content engagement patterns</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 11: Email & Triggered Campaigns

    private LearningModule BuildEmailTriggeredCampaignsModule()
    {
        return new LearningModule
        {
            Id = "email-triggered-campaigns",
            Title = "Email & Triggered Campaigns",
            Description = "Deliver personalised content recommendations via email and triggered messages.",
            Icon = "envelope",
            Order = 11,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "et-email-recommendations-overview",
                    ModuleId = "email-triggered-campaigns",
                    Title = "Email Recommendations Overview",
                    Summary = "Understand how to deliver personalised content in emails.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand email recommendation capabilities",
                        "Know the types of email recommendations",
                        "Learn integration requirements",
                        "Understand the value proposition"
                    },
                    Content = @"
<h2>Email Recommendations Overview</h2>
<p>Content Recommendations can deliver personalised content suggestions in your email campaigns, extending personalisation beyond your website.</p>

<h3>What are Email Recommendations?</h3>
<p>Email recommendations dynamically insert personalised content suggestions into email templates based on the recipient's interest profile.</p>

<h3>Types of Email Recommendations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Newsletter</td><td class=""px-4 py-2"">Regular email with personalised content</td><td class=""px-4 py-2"">Weekly digest</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Triggered</td><td class=""px-4 py-2"">Sent based on behaviour triggers</td><td class=""px-4 py-2"">Abandoned browse</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Transactional</td><td class=""px-4 py-2"">Personalised section in transaction emails</td><td class=""px-4 py-2"">Order confirmation</td></tr>
    </tbody>
</table>

<h3>How It Works</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Email Recommendation Flow:

1. Email is sent to recipient
         │
         ▼
2. Email client opens email
         │
         ▼
3. Image/content URLs are requested
         │
         ▼
4. Content Recommendations API called
   (with recipient identifier)
         │
         ▼
5. Personalised content returned
   (based on recipient's profile)
         │
         ▼
6. Recipient sees personalised recommendations
</pre>

<h3>Integration Requirements</h3>
<ul>
    <li><strong>Email Service Provider</strong> - Must support dynamic content</li>
    <li><strong>Recipient Identification</strong> - Email addresses linked to profiles</li>
    <li><strong>Email Delivery</strong> - API delivery configured in portal</li>
    <li><strong>Template Integration</strong> - Dynamic content blocks in templates</li>
</ul>

<h3>Benefits</h3>
<div class=""bg-purple-50 dark:bg-purple-900/20 border-l-4 border-purple-500 p-4 my-4"">
    <ul class=""text-purple-700 dark:text-purple-300 space-y-2"">
        <li>✓ Higher email engagement rates</li>
        <li>✓ More relevant content for recipients</li>
        <li>✓ Automated personalisation at scale</li>
        <li>✓ Drive traffic back to website</li>
        <li>✓ Continue website personalisation journey</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-trigger-types",
                    ModuleId = "email-triggered-campaigns",
                    Title = "Trigger Types",
                    Summary = "Learn about daily and in-session triggers for automated campaigns.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand daily triggers",
                        "Understand in-session triggers",
                        "Know when to use each type",
                        "Learn trigger prioritisation"
                    },
                    Content = @"
<h2>Trigger Types</h2>
<p>Optimizely Triggered Messages supports two types of triggers that determine when personalised emails are sent.</p>

<h3>Daily Triggers</h3>
<p>Daily triggers are evaluated once a day and can fire multiple times for the same visitor:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Daily Trigger</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">High Product Interest</td><td class=""px-4 py-2"">Visitor showed strong interest in specific topics</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Post Purchase</td><td class=""px-4 py-2"">Follow-up after a conversion</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Low-in-Stock</td><td class=""px-4 py-2"">Content/products visitor viewed are limited</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Targeted Discounts</td><td class=""px-4 py-2"">Promotional campaigns based on interests</td></tr>
    </tbody>
</table>

<h3>In-Session Triggers</h3>
<p>In-session triggers fire based on real-time behaviour within a browsing session:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">In-Session Trigger</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Abandoned Browse</td><td class=""px-4 py-2"">Visitor left without taking action</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Abandoned Basket</td><td class=""px-4 py-2"">Cart abandonment (e-commerce)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Abandoned Checkout</td><td class=""px-4 py-2"">Left during checkout process</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important Limitation</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Only one in-session trigger is allowed per session. If multiple trigger criteria are met simultaneously, trigger prioritisation determines which one fires.</p>
</div>

<h3>Trigger Prioritisation</h3>
<p>When multiple triggers could fire, prioritisation determines the winner:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Campaigns</strong> in the portal</li>
    <li>View your Triggered Messages campaigns</li>
    <li>Drag to reorder (top = highest priority)</li>
    <li>Campaigns at the bottom have lowest priority</li>
</ol>

<h3>Choosing the Right Trigger</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Scenario</th>
            <th class=""px-4 py-2 text-left"">Recommended Trigger</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Re-engage dormant visitors</td><td class=""px-4 py-2"">Daily: High Interest</td></tr>
        <tr><td class=""px-4 py-2"">Recover abandoned sessions</td><td class=""px-4 py-2"">In-session: Abandoned Browse</td></tr>
        <tr><td class=""px-4 py-2"">Regular content digest</td><td class=""px-4 py-2"">Daily: Targeted Content</td></tr>
        <tr><td class=""px-4 py-2"">Time-sensitive content</td><td class=""px-4 py-2"">In-session: Immediate follow-up</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-setting-up-campaigns",
                    ModuleId = "email-triggered-campaigns",
                    Title = "Setting Up Triggered Campaigns",
                    Summary = "Configure triggered email campaigns step by step.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Set up ESP connection",
                        "Configure triggered campaigns",
                        "Create email templates",
                        "Test campaign delivery"
                    },
                    Content = @"
<h2>Setting Up Triggered Campaigns</h2>
<p>Create a triggered campaign to automatically send personalised emails based on visitor behaviour.</p>

<h3>Prerequisites</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>✓ Email Service Provider (ESP) account</li>
        <li>✓ ESP connection configured in portal</li>
        <li>✓ Email Recommendations campaign created</li>
        <li>✓ Email template with dynamic content blocks</li>
    </ul>
</div>

<h3>Step 1: Configure ESP Connection</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Settings &gt; Integrations</strong></li>
    <li>Select your ESP (e.g., Salesforce Marketing Cloud, Marketo)</li>
    <li>Enter API credentials</li>
    <li>Test the connection</li>
</ol>

<h3>Step 2: Create Email Recommendations Campaign</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Go to <strong>Engage &gt; Email Recommendations</strong></li>
    <li>Create a new campaign</li>
    <li>Select trigger strategy</li>
    <li>Configure content sections</li>
    <li>Set up the recommendation delivery</li>
</ol>

<h3>Step 3: Create Triggered Messages Campaign</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Go to <strong>Engage &gt; Triggered Messages</strong></li>
    <li>Click <strong>Create Campaign</strong></li>
    <li>Select ESP connection</li>
    <li>Choose ESP action (send email, add to list, etc.)</li>
    <li>Link to Email Recommendations campaign</li>
</ol>

<h3>Campaign Configuration</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">ESP Connection</td><td class=""px-4 py-2"">Which email platform to use</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ESP Action</td><td class=""px-4 py-2"">Send mail, add to group, remove from group</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recommendations Campaign</td><td class=""px-4 py-2"">Which content recommendations to include</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Trigger Strategy</td><td class=""px-4 py-2"">When the email should be triggered</td></tr>
    </tbody>
</table>

<h3>Step 4: Design Email Template</h3>
<p>Your email template needs dynamic content blocks that fetch personalised recommendations:</p>
<ul>
    <li>Add recommendation placeholder in template</li>
    <li>Configure dynamic image URLs</li>
    <li>Include tracking parameters</li>
    <li>Test with sample data</li>
</ul>

<h3>Testing</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Before Going Live</p>
    <ol class=""mt-2 text-blue-700 dark:text-blue-300 list-decimal list-inside"">
        <li>Send test emails to yourself</li>
        <li>Verify recommendations appear correctly</li>
        <li>Check links track properly</li>
        <li>Test on multiple email clients</li>
        <li>Verify trigger conditions work</li>
    </ol>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-campaign-reporting",
                    ModuleId = "email-triggered-campaigns",
                    Title = "Triggered Messages Reporting",
                    Summary = "Monitor and analyse triggered campaign performance.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Access triggered message reports",
                        "Understand key metrics",
                        "Analyse campaign performance",
                        "Optimise based on data"
                    },
                    Content = @"
<h2>Triggered Messages Reporting</h2>
<p>Monitor the performance of your triggered email campaigns to understand their impact and optimise for better results.</p>

<h3>Accessing Reports</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Reports</strong> in the portal</li>
    <li>Select <strong>Triggers</strong> dashboard</li>
    <li>View aggregate and campaign-specific data</li>
</ol>

<h3>Key Metrics</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">What It Tells You</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Triggers Fired</td><td class=""px-4 py-2"">Sessions that met trigger criteria</td><td class=""px-4 py-2"">Campaign reach potential</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Emails Sent</td><td class=""px-4 py-2"">Triggered emails actually sent</td><td class=""px-4 py-2"">Delivery volume</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Emails Viewed</td><td class=""px-4 py-2"">Opens/views</td><td class=""px-4 py-2"">Email engagement</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Clicks</td><td class=""px-4 py-2"">Recommendation clicks</td><td class=""px-4 py-2"">Content relevance</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">CTR</td><td class=""px-4 py-2"">Click-through rate</td><td class=""px-4 py-2"">Overall effectiveness</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Revenue</td><td class=""px-4 py-2"">Attributed revenue</td><td class=""px-4 py-2"">Business impact</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Orders</td><td class=""px-4 py-2"">Conversions from emails</td><td class=""px-4 py-2"">Conversion effectiveness</td></tr>
    </tbody>
</table>

<h3>Daily Reports</h3>
<p>The daily report shows:</p>
<ul>
    <li>How many website sessions fired a trigger</li>
    <li>How many triggered emails were viewed</li>
    <li>How many products/content items were engaged</li>
    <li>Conversion and revenue data</li>
</ul>

<h3>Analysing Performance</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Performance Analysis Questions:

┌─────────────────────────────────────────┐
│ Low Triggers Fired?                     │
│ → Review trigger criteria               │
│ → Check tracking implementation         │
├─────────────────────────────────────────┤
│ Low Open Rate?                          │
│ → Improve subject lines                 │
│ → Optimise send timing                  │
├─────────────────────────────────────────┤
│ Low CTR?                                │
│ → Review recommendation relevance       │
│ → Improve email template design         │
├─────────────────────────────────────────┤
│ Low Conversion?                         │
│ → Check landing page experience         │
│ → Review content quality                │
└─────────────────────────────────────────┘
</pre>

<h3>Optimisation Opportunities</h3>
<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Improvement Ideas</p>
    <ul class=""mt-2 text-green-700 dark:text-green-300 space-y-1"">
        <li>A/B test email templates</li>
        <li>Refine trigger timing</li>
        <li>Adjust content sections used</li>
        <li>Test different subject lines</li>
        <li>Optimise for mobile devices</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion
}
