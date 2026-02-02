using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.Analytics;

/// <summary>
/// Content provider for the Optimizely Analytics course
/// </summary>
public class AnalyticsContentProvider : ILearningContentProvider
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
            // Phase 2: Modules 1-3 (Beginner)
            BuildGettingStartedModule(),
            BuildWarehouseConnectionsModule(),
            BuildDataModelingModule(),
            // Phase 3: Modules 4-6 (Intermediate - Core Features)
            BuildExplorationTemplatesModule(),
            BuildMetricsMeasuresModule(),
            BuildCohortsSegmentationModule(),
            // Phase 4: Modules 7-9 (Intermediate/Advanced)
            BuildDashboardsVisualizationModule(),
            BuildNetScriptSqlModule(),
            BuildExperimentationAnalyticsModule(),
            // Phase 5: Module 10 (Advanced)
            BuildAdvancedTopicsModule()
        };
    }

    #region Module 1: Getting Started

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started",
            Description = "Learn the fundamentals of Optimizely Analytics, understand warehouse-native architecture, and explore the platform interface.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-analytics",
                    ModuleId = "getting-started",
                    Title = "What is Optimizely Analytics?",
                    Summary = "Discover Optimizely Analytics and its warehouse-native approach to product and customer analytics.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Analytics is and its core purpose",
                        "Learn the history and evolution from NetSpring acquisition",
                        "Understand the warehouse-native architecture concept",
                        "Know when to use Optimizely Analytics for your projects"
                    },
                    Content = @"
<h2>Introduction to Optimizely Analytics</h2>
<p>Optimizely Analytics is a <strong>warehouse-native product and customer analytics platform</strong> that enables organisations to derive deep analytical insights about product usage and customer behaviour directly from their existing data warehouses.</p>

<h3>What is Optimizely Analytics?</h3>
<p>Optimizely Analytics (formerly NetSpring, acquired by Optimizely in September 2024) provides a fundamentally different approach to analytics. Rather than extracting and duplicating your data into a separate analytics platform, it works directly with data in your existing data warehouse—Snowflake, BigQuery, Databricks, or Amazon Redshift.</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Key Innovation: Warehouse-Native Architecture</p>
    <p class=""mt-2"">Unlike traditional analytics tools that require data extraction and movement, Optimizely Analytics queries your warehouse directly. This eliminates data duplication, ensures consistency with your single source of truth, and maintains data security.</p>
</div>

<h3>Core Capabilities</h3>
<ul>
    <li><strong>Self-Service Exploration</strong> - Pre-built templates for common analytics patterns: funnels, retention, paths, and segmentation</li>
    <li><strong>Semantic Data Layer</strong> - Define Actors (users) and Events (actions) to enable behavioural analytics without complex SQL</li>
    <li><strong>Rich Visualisations</strong> - Create interactive dashboards and share insights across teams</li>
    <li><strong>NetScript & SQL</strong> - Full support for custom queries and advanced analysis</li>
    <li><strong>Experimentation Integration</strong> - Deep integration with Optimizely's A/B testing and feature experimentation</li>
    <li><strong>Opal AI</strong> - AI-powered insights and automated summaries</li>
</ul>

<h3>Why Warehouse-Native?</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Traditional Analytics</th>
            <th class=""px-4 py-2 text-left"">Warehouse-Native (Optimizely)</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Data Location</td><td class=""px-4 py-2"">Copied to analytics platform</td><td class=""px-4 py-2"">Stays in your warehouse</td></tr>
        <tr><td class=""px-4 py-2"">Data Freshness</td><td class=""px-4 py-2"">Delayed by ETL pipelines</td><td class=""px-4 py-2"">Real-time from warehouse</td></tr>
        <tr><td class=""px-4 py-2"">Data Security</td><td class=""px-4 py-2"">Data leaves your control</td><td class=""px-4 py-2"">Data stays in your warehouse</td></tr>
        <tr><td class=""px-4 py-2"">Cost</td><td class=""px-4 py-2"">Storage + compute duplication</td><td class=""px-4 py-2"">Use existing warehouse investment</td></tr>
        <tr><td class=""px-4 py-2"">Consistency</td><td class=""px-4 py-2"">Multiple copies can diverge</td><td class=""px-4 py-2"">Single source of truth</td></tr>
    </tbody>
</table>

<h3>The NetSpring Heritage</h3>
<p>Optimizely acquired NetSpring in September 2024 and rebranded the platform as Optimizely Analytics in Q1 2025. NetSpring was founded with a vision to solve the fundamental problems with traditional product analytics:</p>
<ul>
    <li>Eliminating the need for complex ETL pipelines</li>
    <li>Preserving data governance and security</li>
    <li>Enabling teams to use their existing warehouse investments</li>
    <li>Making behavioural analytics accessible without deep SQL expertise</li>
</ul>

<h3>When to Use Optimizely Analytics</h3>
<ul>
    <li>You have an existing data warehouse (Snowflake, BigQuery, Databricks, or Redshift)</li>
    <li>You need product and customer behavioural analytics</li>
    <li>Data security and governance are priorities</li>
    <li>You want to eliminate data duplication and ETL complexity</li>
    <li>You're already using or planning to use Optimizely Experimentation</li>
    <li>You need self-service analytics for non-technical teams</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-key-concepts",
                    ModuleId = "getting-started",
                    Title = "Key Concepts & Terminology",
                    Summary = "Learn the fundamental concepts: Actors, Events, Explorations, Dashboards, and the Semantic Layer.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand Actors and Events as the foundation of analytics",
                        "Learn what Explorations and Dashboards are",
                        "Understand the role of the Semantic Layer",
                        "Know the key terminology used throughout the platform"
                    },
                    Content = @"
<h2>Core Concepts in Optimizely Analytics</h2>
<p>Before diving into the platform, it's essential to understand the core concepts that form the foundation of Optimizely Analytics.</p>

<h3>Actors and Events: The Foundation</h3>
<p>The most important distinction in Optimizely Analytics is between <strong>Actors</strong> and <strong>Events</strong>:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Actors</h4>
    <p>Actors represent the entities performing actions in your system—typically users, but could also be accounts, devices, or any other entity you want to analyse. Actors are identified by a unique identifier (like user_id or account_id).</p>

    <h4 class=""font-semibold mt-4"">Events</h4>
    <p>Events represent actions or behaviours that Actors perform. Examples include page views, button clicks, purchases, sign-ups, or any tracked interaction. Each event has a timestamp and is associated with an Actor.</p>
</div>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    Semantic Data Model                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌─────────────┐           ┌─────────────────────────────────┐ │
│  │   ACTORS    │           │            EVENTS               │ │
│  │             │           │                                 │ │
│  │  user_id    │◄─────────►│  event_type    timestamp        │ │
│  │  email      │   1:Many  │  page_viewed   properties       │ │
│  │  created_at │           │  button_click  user_id (FK)     │ │
│  │  attributes │           │  purchase      event_value      │ │
│  │             │           │  sign_up       session_id       │ │
│  └─────────────┘           └─────────────────────────────────┘ │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>The Semantic Layer</h3>
<p>The Semantic Layer is what makes Optimizely Analytics different from traditional BI tools. It provides:</p>
<ul>
    <li><strong>Understanding of Data Meaning</strong> - Analytics knows that certain tables contain user data (Actors) and others contain event streams</li>
    <li><strong>Automatic Relationships</strong> - Once you define which tables are Actors and Events, the platform automatically enables funnels, cohorts, and journey analysis</li>
    <li><strong>No Complex Joins</strong> - Business users can analyse data without writing JOIN statements</li>
</ul>

<h3>Explorations</h3>
<p>Explorations are interactive analysis sessions where you investigate your data. Optimizely Analytics provides pre-built exploration templates:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Template</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example Question</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Event Segmentation</td><td class=""px-4 py-2"">Analyse event frequency and trends</td><td class=""px-4 py-2"">""How many users clicked 'Add to Cart' this week?""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Funnel</td><td class=""px-4 py-2"">Track conversion through steps</td><td class=""px-4 py-2"">""What % of users complete checkout?""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Retention</td><td class=""px-4 py-2"">Measure return behaviour over time</td><td class=""px-4 py-2"">""Do users come back after sign-up?""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Path</td><td class=""px-4 py-2"">Understand user journeys</td><td class=""px-4 py-2"">""What do users do after viewing a product?""</td></tr>
    </tbody>
</table>

<h3>Dashboards</h3>
<p>Dashboards are collections of visualisations organised in a grid view. They're used to:</p>
<ul>
    <li>Monitor operational metrics at a glance</li>
    <li>Share insights with stakeholders</li>
    <li>Track KPIs over time</li>
    <li>Enable drill-down for deeper analysis</li>
</ul>

<h3>Key Terminology Reference</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Term</th>
            <th class=""px-4 py-2 text-left"">Definition</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Dataset</td><td class=""px-4 py-2"">A table from your warehouse configured for use in Analytics</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Cohort</td><td class=""px-4 py-2"">A group of Actors defined by shared characteristics or behaviour</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Metric</td><td class=""px-4 py-2"">A calculated measure based on event data (count, sum, ratio)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Dimension</td><td class=""px-4 py-2"">An attribute used to segment or group data</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">NetScript</td><td class=""px-4 py-2"">Optimizely's analytical programming language for advanced queries</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Quad</td><td class=""px-4 py-2"">A NetScript query expression that represents a SQL query</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-differs-from-bi",
                    ModuleId = "getting-started",
                    Title = "How Analytics Differs from BI Tools",
                    Summary = "Understand what makes Optimizely Analytics different from traditional BI and product analytics tools.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the differences between BI tools and behavioural analytics",
                        "Learn why the semantic layer matters",
                        "Know the advantages of warehouse-native analytics",
                        "Understand when to use Analytics vs traditional BI"
                    },
                    Content = @"
<h2>Analytics vs Traditional BI Tools</h2>
<p>While traditional Business Intelligence (BI) tools like Tableau, Looker, or Power BI are powerful, Optimizely Analytics takes a fundamentally different approach designed specifically for <strong>behavioural and product analytics</strong>.</p>

<h3>The Core Difference: Semantic Understanding</h3>
<p>Traditional BI tools treat all data tables equally—they see rows and columns without understanding what the data represents. Optimizely Analytics maintains a semantic understanding:</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">BI Tools: ""These are tables with data""</p>
    <p class=""font-medium mt-2"">Optimizely Analytics: ""This table contains Users, and this table contains their Actions over time""</p>
</div>

<h3>Comparison Table</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Capability</th>
            <th class=""px-4 py-2 text-left"">Traditional BI Tools</th>
            <th class=""px-4 py-2 text-left"">Optimizely Analytics</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Funnel Analysis</td><td class=""px-4 py-2"">Complex SQL with window functions</td><td class=""px-4 py-2"">Built-in template, no SQL required</td></tr>
        <tr><td class=""px-4 py-2"">Retention Cohorts</td><td class=""px-4 py-2"">Requires data engineering</td><td class=""px-4 py-2"">Automatic with Actor/Event model</td></tr>
        <tr><td class=""px-4 py-2"">Path Analysis</td><td class=""px-4 py-2"">Often not possible</td><td class=""px-4 py-2"">Native support</td></tr>
        <tr><td class=""px-4 py-2"">User Journeys</td><td class=""px-4 py-2"">Manual session stitching</td><td class=""px-4 py-2"">Automatic actor tracking</td></tr>
        <tr><td class=""px-4 py-2"">Data Model</td><td class=""px-4 py-2"">Generic tables and joins</td><td class=""px-4 py-2"">Semantic (Actors & Events)</td></tr>
        <tr><td class=""px-4 py-2"">User Access</td><td class=""px-4 py-2"">SQL knowledge often required</td><td class=""px-4 py-2"">Self-service for business users</td></tr>
    </tbody>
</table>

<h3>Comparison with Product Analytics Tools</h3>
<p>Tools like Amplitude, Mixpanel, or Heap are purpose-built for product analytics. However, they differ from Optimizely Analytics:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">SaaS Product Analytics</th>
            <th class=""px-4 py-2 text-left"">Optimizely Analytics</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Data Storage</td><td class=""px-4 py-2"">Data copied to their cloud</td><td class=""px-4 py-2"">Data stays in your warehouse</td></tr>
        <tr><td class=""px-4 py-2"">Data Governance</td><td class=""px-4 py-2"">Data leaves your control</td><td class=""px-4 py-2"">Full governance maintained</td></tr>
        <tr><td class=""px-4 py-2"">Data Sources</td><td class=""px-4 py-2"">Limited to tracked events</td><td class=""px-4 py-2"">All warehouse data available</td></tr>
        <tr><td class=""px-4 py-2"">Data Latency</td><td class=""px-4 py-2"">Dependent on ingestion</td><td class=""px-4 py-2"">Real-time from warehouse</td></tr>
        <tr><td class=""px-4 py-2"">Custom Metrics</td><td class=""px-4 py-2"">Limited to platform schema</td><td class=""px-4 py-2"">Full SQL/NetScript flexibility</td></tr>
        <tr><td class=""px-4 py-2"">Experimentation</td><td class=""px-4 py-2"">Separate integration needed</td><td class=""px-4 py-2"">Native Optimizely integration</td></tr>
    </tbody>
</table>

<h3>When to Use What</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Use Traditional BI Tools When:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• Creating financial reports and business dashboards</li>
        <li>• Analysing structured business data (sales, inventory, HR)</li>
        <li>• Building executive scorecards</li>
        <li>• Ad-hoc SQL analysis by data teams</li>
    </ul>

    <h4 class=""font-semibold mt-4"">Use Optimizely Analytics When:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• Analysing user behaviour and product usage</li>
        <li>• Building funnels and conversion analysis</li>
        <li>• Understanding retention and churn</li>
        <li>• Mapping user journeys and paths</li>
        <li>• Integrating with A/B testing and experimentation</li>
        <li>• Enabling self-service for product and marketing teams</li>
    </ul>
</div>

<h3>The Best of Both Worlds</h3>
<p>Optimizely Analytics doesn't replace your BI tools—it complements them. You can:</p>
<ul>
    <li>Use the same warehouse for both platforms</li>
    <li>Share data models and definitions</li>
    <li>Export insights from Analytics to BI dashboards</li>
    <li>Let each tool focus on its strengths</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-product-interface",
                    ModuleId = "getting-started",
                    Title = "Product Overview & Interface",
                    Summary = "Navigate the Optimizely Analytics interface and understand its key sections.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the main Analytics interface",
                        "Understand the key sections and their purposes",
                        "Know where to find explorations, dashboards, and settings",
                        "Learn the workspace organisation model"
                    },
                    Content = @"
<h2>Navigating Optimizely Analytics</h2>
<p>The Optimizely Analytics interface is designed for both technical and non-technical users, providing easy access to powerful analytical capabilities.</p>

<h3>Main Navigation</h3>
<p>The interface is organised into several key areas:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────────┐
│  ┌──────────┐                                                       │
│  │   LOGO   │  Home  Explore  Dashboards  Data  Settings            │
│  └──────────┘                                                       │
├─────────────────────────────────────────────────────────────────────┤
│  ┌──────────────────┐  ┌─────────────────────────────────────────┐ │
│  │                  │  │                                         │ │
│  │   Navigation     │  │          Main Content Area              │ │
│  │   Panel          │  │                                         │ │
│  │                  │  │   Explorations, Dashboards,             │ │
│  │   - Recent       │  │   Visualisations, etc.                  │ │
│  │   - Favourites   │  │                                         │ │
│  │   - Categories   │  │                                         │ │
│  │                  │  │                                         │ │
│  └──────────────────┘  └─────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────────┘
</pre>

<h3>Key Sections</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Section</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Key Actions</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Home</td><td class=""px-4 py-2"">Overview and quick access</td><td class=""px-4 py-2"">View recent items, get started guides</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Explore</td><td class=""px-4 py-2"">Create and run analyses</td><td class=""px-4 py-2"">New exploration, templates, saved explorations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Dashboards</td><td class=""px-4 py-2"">View and create dashboards</td><td class=""px-4 py-2"">Create, share, monitor metrics</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data</td><td class=""px-4 py-2"">Configure data sources</td><td class=""px-4 py-2"">Connections, datasets, metrics, cohorts</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Settings</td><td class=""px-4 py-2"">Platform configuration</td><td class=""px-4 py-2"">Users, permissions, integrations</td></tr>
    </tbody>
</table>

<h3>The Explore Section</h3>
<p>The Explore section is where most analysis happens. It includes:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Exploration Templates</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Event Segmentation</strong> - Analyse event frequency, trends, and breakdowns</li>
        <li>• <strong>Funnel</strong> - Track conversion through multi-step processes</li>
        <li>• <strong>Retention</strong> - Measure user return behaviour over time</li>
        <li>• <strong>Path</strong> - Visualise user journeys and navigation patterns</li>
        <li>• <strong>SQL Explore</strong> - Write custom SQL queries</li>
        <li>• <strong>NetScript Explore</strong> - Use the NetScript analytical language</li>
    </ul>
</div>

<h3>The Data Section</h3>
<p>The Data section is where you configure your analytics foundation:</p>

<ul>
    <li><strong>Connections</strong> - Configure warehouse connections (BigQuery, Snowflake, etc.)</li>
    <li><strong>Datasets</strong> - Define which tables to use and their semantic meaning</li>
    <li><strong>Metrics</strong> - Create reusable metric definitions</li>
    <li><strong>Cohorts</strong> - Define user segments for analysis</li>
    <li><strong>Events</strong> - Manage event definitions and properties</li>
</ul>

<h3>Workspace Organisation</h3>
<p>Optimizely Analytics supports organising content for teams:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Favourites</td><td class=""px-4 py-2"">Quick access to frequently used items</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Categories</td><td class=""px-4 py-2"">Group related dashboards and explorations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Search</td><td class=""px-4 py-2"">Find any item across the platform</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recent</td><td class=""px-4 py-2"">Access recently viewed items</td></tr>
    </tbody>
</table>

<h3>Quick Actions</h3>
<p>Common tasks you'll perform regularly:</p>
<ul>
    <li><strong>New Exploration</strong> - Start a new analysis from a template</li>
    <li><strong>Save Exploration</strong> - Save your work for later or sharing</li>
    <li><strong>Add to Dashboard</strong> - Pin visualisations to dashboards</li>
    <li><strong>Share</strong> - Share explorations and dashboards with team members</li>
    <li><strong>Export</strong> - Download results as CSV or PDF</li>
    <li><strong>Summarise with Opal</strong> - Generate AI-powered insights</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-first-exploration",
                    ModuleId = "getting-started",
                    Title = "Creating Your First Exploration",
                    Summary = "Hands-on tutorial: Create your first exploration using the Event Segmentation template.",
                    Order = 5,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create a new exploration from a template",
                        "Select events and configure measures",
                        "Apply filters and groupings",
                        "Save and share your exploration"
                    },
                    Content = @"
<h2>Your First Exploration</h2>
<p>Let's create your first exploration using the Event Segmentation template. This is one of the most commonly used analyses for understanding user behaviour.</p>

<h3>Prerequisites</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium"">Before You Begin:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ Access to an Optimizely Analytics account</li>
        <li>✓ A connected data warehouse with event data</li>
        <li>✓ At least one dataset configured with Actors and Events</li>
    </ul>
</div>

<h3>Step 1: Start a New Exploration</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to the <strong>Explore</strong> section in the main navigation</li>
    <li>Click the <strong>+ New Exploration</strong> button</li>
    <li>Select <strong>Event Segmentation</strong> from the template options</li>
</ol>

<h3>Step 2: Configure Your Events</h3>
<p>The Events module lets you specify what user actions to analyse:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Events Module                                                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │  Event: [Select an event...]              ▼              │    │
│  │                                                          │    │
│  │  Suggested:                                              │    │
│  │    • page_viewed                                         │    │
│  │    • button_clicked                                      │    │
│  │    • purchase_completed                                  │    │
│  │    • sign_up                                             │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  [+ Add Another Event]                                           │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<p>For this tutorial, select an event like <code>page_viewed</code> or any event in your data.</p>

<h3>Step 3: Select Your Measure</h3>
<p>Choose how to count or aggregate the events:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Measure</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Total Events</td><td class=""px-4 py-2"">Count all occurrences</td><td class=""px-4 py-2"">""How many page views total?""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Unique Users</td><td class=""px-4 py-2"">Count distinct actors</td><td class=""px-4 py-2"">""How many users viewed pages?""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Average per User</td><td class=""px-4 py-2"">Events divided by users</td><td class=""px-4 py-2"">""Pages per user on average?""</td></tr>
    </tbody>
</table>

<h3>Step 4: Set Your Time Range</h3>
<p>Configure the date range for your analysis:</p>
<ul>
    <li><strong>Last 7 days</strong> - Recent behaviour analysis</li>
    <li><strong>Last 30 days</strong> - Monthly trends</li>
    <li><strong>Custom Range</strong> - Specific date range</li>
    <li><strong>Comparison</strong> - Compare to previous period</li>
</ul>

<h3>Step 5: Group By (Optional)</h3>
<p>Break down your results by a dimension:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Example Groupings:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>By Day</strong> - See daily trends</li>
        <li>• <strong>By Country</strong> - Geographic breakdown</li>
        <li>• <strong>By Device</strong> - Mobile vs Desktop</li>
        <li>• <strong>By Page URL</strong> - Which pages are viewed most</li>
    </ul>
</div>

<h3>Step 6: Run Your Exploration</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click the <strong>Run</strong> button to execute the query</li>
    <li>The visualisation will display your results</li>
    <li>Hover over data points for details</li>
    <li>Toggle between chart types (line, bar, table)</li>
</ol>

<h3>Step 7: Save Your Exploration</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Save</strong> in the top toolbar</li>
    <li>Give your exploration a descriptive name</li>
    <li>Optionally add to a category</li>
    <li>Choose sharing permissions</li>
</ol>

<h3>Understanding Your Results</h3>
<p>The visualisation shows your event data over time:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Page Views - Last 7 Days

   5000 │                              ╭────╮
        │                         ╭────╯    ╰────╮
   4000 │                    ╭────╯              ╰────╮
        │               ╭────╯                        ╰──
   3000 │          ╭────╯
        │     ╭────╯
   2000 │╭────╯
        │
   1000 │
        │
      0 └─────────────────────────────────────────────────────
         Mon    Tue    Wed    Thu    Fri    Sat    Sun
</pre>

<h3>Next Steps</h3>
<p>Now that you've created your first exploration:</p>
<ul>
    <li>Add it to a dashboard for monitoring</li>
    <li>Try other templates (Funnel, Retention)</li>
    <li>Experiment with filters and segments</li>
    <li>Use Opal AI to summarise your findings</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Warehouse Connections

    private LearningModule BuildWarehouseConnectionsModule()
    {
        return new LearningModule
        {
            Id = "warehouse-connections",
            Title = "Warehouse Connections",
            Description = "Connect Optimizely Analytics to your data warehouse - BigQuery, Snowflake, Databricks, or Amazon Redshift.",
            Icon = "server-stack",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "wc-warehouse-native-architecture",
                    ModuleId = "warehouse-connections",
                    Title = "Understanding Warehouse-Native Architecture",
                    Summary = "Learn how Optimizely Analytics works directly with your data warehouse without data movement.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the warehouse-native architecture model",
                        "Learn how queries are executed in your warehouse",
                        "Understand the benefits of no data movement",
                        "Know the supported data warehouses"
                    },
                    Content = @"
<h2>Warehouse-Native Architecture</h2>
<p>Optimizely Analytics is built on a <strong>warehouse-native architecture</strong>, meaning it queries your data warehouse directly rather than copying data into a separate analytics platform.</p>

<h3>How It Works</h3>
<p>When you run an exploration or view a dashboard in Optimizely Analytics:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────────┐
│                    Query Execution Flow                             │
├─────────────────────────────────────────────────────────────────────┤
│                                                                     │
│  1. User Action                                                     │
│     ┌─────────────────┐                                            │
│     │ Run Exploration │                                            │
│     └────────┬────────┘                                            │
│              ▼                                                      │
│  2. Query Generation                                                │
│     ┌─────────────────┐                                            │
│     │ Analytics       │  Translates exploration                    │
│     │ generates SQL   │  into optimised SQL                        │
│     └────────┬────────┘                                            │
│              ▼                                                      │
│  3. Warehouse Execution                                             │
│     ┌─────────────────┐                                            │
│     │ Your Warehouse  │  Query runs in Snowflake,                  │
│     │ (BigQuery, etc.)│  BigQuery, Databricks, etc.                │
│     └────────┬────────┘                                            │
│              ▼                                                      │
│  4. Results Return                                                  │
│     ┌─────────────────┐                                            │
│     │ Visualisation   │  Results rendered in                       │
│     │ displayed       │  the browser                               │
│     └─────────────────┘                                            │
│                                                                     │
└─────────────────────────────────────────────────────────────────────┘
</pre>

<h3>Key Benefits</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">No Data Movement</td><td class=""px-4 py-2"">Data never leaves your warehouse—no ETL pipelines to maintain</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Single Source of Truth</td><td class=""px-4 py-2"">Analytics uses the same data as your other tools</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data Security</td><td class=""px-4 py-2"">Sensitive data stays within your security perimeter</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data Freshness</td><td class=""px-4 py-2"">Queries reflect real-time warehouse data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cost Efficiency</td><td class=""px-4 py-2"">Leverage existing warehouse investment and compute</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Compliance</td><td class=""px-4 py-2"">Easier GDPR, CCPA, and data residency compliance</td></tr>
    </tbody>
</table>

<h3>Supported Data Warehouses</h3>
<p>Optimizely Analytics supports four major cloud data warehouses:</p>

<div class=""grid grid-cols-2 gap-4 my-4"">
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-semibold"">Google BigQuery</h4>
        <p class=""text-sm mt-2"">Google Cloud's serverless, highly scalable data warehouse</p>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-semibold"">Snowflake</h4>
        <p class=""text-sm mt-2"">Multi-cloud data platform with separation of storage and compute</p>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-semibold"">Databricks</h4>
        <p class=""text-sm mt-2"">Unified analytics platform built on Apache Spark</p>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-semibold"">Amazon Redshift</h4>
        <p class=""text-sm mt-2"">AWS's fully managed petabyte-scale data warehouse</p>
    </div>
</div>

<h3>Connection Requirements</h3>
<p>To connect Optimizely Analytics to your warehouse, you'll need:</p>
<ul>
    <li><strong>Service Account</strong> - A dedicated account for Analytics to use</li>
    <li><strong>Read Access</strong> - Permission to query your data tables</li>
    <li><strong>Write Access</strong> - Permission to a scratch schema for materialisation</li>
    <li><strong>Network Access</strong> - Analytics must be able to reach your warehouse</li>
</ul>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Materialisation Schema</p>
    <p class=""mt-2"">Optimizely Analytics uses a ""scratch space"" schema for materialising repeated queries. This significantly improves performance and reduces warehouse costs by caching intermediate results.</p>
</div>

<h3>What Data Can You Analyse?</h3>
<p>Once connected, you can analyse any data in your warehouse:</p>
<ul>
    <li>Event tracking data (page views, clicks, conversions)</li>
    <li>User/customer tables</li>
    <li>Transaction and order data</li>
    <li>Subscription and billing data</li>
    <li>Support ticket data</li>
    <li>Any business data with user identifiers</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wc-configuring-bigquery",
                    ModuleId = "warehouse-connections",
                    Title = "Configuring Google BigQuery",
                    Summary = "Step-by-step guide to connecting Optimizely Analytics to Google BigQuery.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create a BigQuery service account",
                        "Configure the required permissions",
                        "Connect BigQuery to Optimizely Analytics",
                        "Test the connection"
                    },
                    Content = @"
<h2>Connecting Google BigQuery</h2>
<p>Google BigQuery is a popular choice for Optimizely Analytics due to its serverless architecture and powerful SQL capabilities.</p>

<h3>Prerequisites</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium"">Before You Begin:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ Google Cloud Platform (GCP) project with BigQuery enabled</li>
        <li>✓ Administrator privileges in your GCP project</li>
        <li>✓ Event data loaded into BigQuery tables</li>
        <li>✓ Optimizely Analytics account</li>
    </ul>
</div>

<h3>Step 1: Create a Service Account</h3>
<p>Create a dedicated service account for Optimizely Analytics:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code># In Google Cloud Console:
# 1. Navigate to: IAM & Admin → Service Accounts
# 2. Click ""Create Service Account""
# 3. Name it: optimizely-analytics-reader
# 4. Create and download the JSON key file</code></pre>

<h3>Step 2: Create a Custom Role</h3>
<p>Create a custom role with the minimum required permissions:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Permission</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">bigquery.jobs.create</td><td class=""px-4 py-2"">Run queries</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">bigquery.tables.getData</td><td class=""px-4 py-2"">Read table data</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">bigquery.tables.list</td><td class=""px-4 py-2"">List available tables</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">bigquery.tables.get</td><td class=""px-4 py-2"">Get table metadata</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">bigquery.datasets.get</td><td class=""px-4 py-2"">Access dataset information</td></tr>
    </tbody>
</table>

<h3>Step 3: Create a Scratch Dataset</h3>
<p>Create a dataset for Analytics to use for materialisation:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Create a dedicated scratch dataset
CREATE SCHEMA IF NOT EXISTS `your-project.optimizely_scratch`
OPTIONS (
  location = 'US'  -- Match your data location
);

-- Grant write access to the service account
-- Done via IAM in the Cloud Console</code></pre>

<h3>Step 4: Configure the Connection in Analytics</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In Optimizely Analytics, navigate to <strong>Data → Connections</strong></li>
    <li>Click <strong>+ New Connection</strong></li>
    <li>Select <strong>BigQuery</strong></li>
    <li>Upload your service account JSON key file</li>
    <li>Enter your project ID</li>
    <li>Specify the scratch dataset name</li>
    <li>Click <strong>Test Connection</strong></li>
</ol>

<h3>Connection Configuration Form</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  New BigQuery Connection                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Connection Name:    [Production BigQuery          ]            │
│                                                                  │
│  Project ID:         [your-gcp-project-id          ]            │
│                                                                  │
│  Service Account:    [Upload JSON key...           ]  📁        │
│                                                                  │
│  Scratch Dataset:    [optimizely_scratch           ]            │
│                                                                  │
│  Location:           [US                    ▼      ]            │
│                                                                  │
│  ┌──────────────────┐  ┌──────────────────┐                     │
│  │ Test Connection  │  │      Save        │                     │
│  └──────────────────┘  └──────────────────┘                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Performance Optimisation Tips</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Cluster Your Tables</h4>
    <p class=""mt-2"">For best performance, cluster your event tables by:</p>
    <ol class=""list-decimal list-inside mt-2"">
        <li>Event date (partition key)</li>
        <li>Event type</li>
    </ol>
    <pre class=""bg-gray-900 text-green-400 p-3 rounded mt-2 text-sm""><code>CREATE TABLE events
PARTITION BY DATE(event_timestamp)
CLUSTER BY event_type
AS SELECT * FROM raw_events;</code></pre>
</div>

<h3>Troubleshooting</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Connection timeout</td><td class=""px-4 py-2"">Check network/firewall settings</td></tr>
        <tr><td class=""px-4 py-2"">Permission denied</td><td class=""px-4 py-2"">Verify IAM roles on service account</td></tr>
        <tr><td class=""px-4 py-2"">Dataset not found</td><td class=""px-4 py-2"">Ensure dataset exists and location matches</td></tr>
        <tr><td class=""px-4 py-2"">Slow queries</td><td class=""px-4 py-2"">Add table clustering and partitioning</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wc-configuring-snowflake",
                    ModuleId = "warehouse-connections",
                    Title = "Configuring Snowflake",
                    Summary = "Step-by-step guide to connecting Optimizely Analytics to Snowflake.",
                    Order = 3,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create a Snowflake user and role for Analytics",
                        "Configure warehouse and permissions",
                        "Set up the scratch schema",
                        "Connect Snowflake to Optimizely Analytics"
                    },
                    Content = @"
<h2>Connecting Snowflake</h2>
<p>Snowflake's separation of storage and compute makes it an excellent choice for analytics workloads. Here's how to connect it to Optimizely Analytics.</p>

<h3>Prerequisites</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium"">Before You Begin:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ Snowflake account with ACCOUNTADMIN access (for setup)</li>
        <li>✓ Event data loaded into Snowflake tables</li>
        <li>✓ Optimizely Analytics account</li>
    </ul>
</div>

<h3>Step 1: Create a Dedicated User and Role</h3>
<p>Create a dedicated user and role for Optimizely Analytics:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Create a role for Optimizely Analytics
CREATE ROLE IF NOT EXISTS OPTIMIZELY_ANALYTICS_ROLE;

-- Create a user for the connection
CREATE USER IF NOT EXISTS OPTIMIZELY_ANALYTICS_USER
  PASSWORD = 'your-secure-password'
  DEFAULT_ROLE = OPTIMIZELY_ANALYTICS_ROLE
  DEFAULT_WAREHOUSE = OPTIMIZELY_WH;

-- Grant the role to the user
GRANT ROLE OPTIMIZELY_ANALYTICS_ROLE TO USER OPTIMIZELY_ANALYTICS_USER;</code></pre>

<h3>Step 2: Create and Configure a Warehouse</h3>
<p>Create a dedicated warehouse for Analytics queries:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Create a warehouse (start with Medium, adjust based on usage)
CREATE WAREHOUSE IF NOT EXISTS OPTIMIZELY_WH
  WAREHOUSE_SIZE = 'MEDIUM'
  AUTO_SUSPEND = 300
  AUTO_RESUME = TRUE
  INITIALLY_SUSPENDED = TRUE;

-- Grant usage to the Analytics role
GRANT USAGE ON WAREHOUSE OPTIMIZELY_WH TO ROLE OPTIMIZELY_ANALYTICS_ROLE;</code></pre>

<h3>Step 3: Grant Data Access</h3>
<p>Grant read access to your data:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Grant access to the database
GRANT USAGE ON DATABASE YOUR_DATABASE TO ROLE OPTIMIZELY_ANALYTICS_ROLE;

-- Grant access to schemas containing your data
GRANT USAGE ON SCHEMA YOUR_DATABASE.EVENTS TO ROLE OPTIMIZELY_ANALYTICS_ROLE;
GRANT USAGE ON SCHEMA YOUR_DATABASE.USERS TO ROLE OPTIMIZELY_ANALYTICS_ROLE;

-- Grant SELECT on tables
GRANT SELECT ON ALL TABLES IN SCHEMA YOUR_DATABASE.EVENTS TO ROLE OPTIMIZELY_ANALYTICS_ROLE;
GRANT SELECT ON ALL TABLES IN SCHEMA YOUR_DATABASE.USERS TO ROLE OPTIMIZELY_ANALYTICS_ROLE;

-- Grant for future tables
GRANT SELECT ON FUTURE TABLES IN SCHEMA YOUR_DATABASE.EVENTS TO ROLE OPTIMIZELY_ANALYTICS_ROLE;</code></pre>

<h3>Step 4: Create a Scratch Schema</h3>
<p>Create a schema for Analytics to use for materialisation:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Create the scratch schema
CREATE SCHEMA IF NOT EXISTS YOUR_DATABASE.OPTIMIZELY_SCRATCH;

-- Grant full access for materialisation
GRANT ALL PRIVILEGES ON SCHEMA YOUR_DATABASE.OPTIMIZELY_SCRATCH
  TO ROLE OPTIMIZELY_ANALYTICS_ROLE;

-- Grant create table permission
GRANT CREATE TABLE ON SCHEMA YOUR_DATABASE.OPTIMIZELY_SCRATCH
  TO ROLE OPTIMIZELY_ANALYTICS_ROLE;</code></pre>

<h3>Step 5: Configure the Connection</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In Optimizely Analytics, navigate to <strong>Data → Connections</strong></li>
    <li>Click <strong>+ New Connection</strong></li>
    <li>Select <strong>Snowflake</strong></li>
    <li>Enter your connection details:</li>
</ol>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Field</th>
            <th class=""px-4 py-2 text-left"">Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Account</td><td class=""px-4 py-2"">Your Snowflake account identifier (e.g., xy12345.us-east-1)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Username</td><td class=""px-4 py-2"">OPTIMIZELY_ANALYTICS_USER</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Password</td><td class=""px-4 py-2"">The password you set</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Warehouse</td><td class=""px-4 py-2"">OPTIMIZELY_WH</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Database</td><td class=""px-4 py-2"">YOUR_DATABASE</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Scratch Schema</td><td class=""px-4 py-2"">OPTIMIZELY_SCRATCH</td></tr>
    </tbody>
</table>

<h3>Warehouse Sizing Recommendations</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <table class=""min-w-full"">
        <thead>
            <tr>
                <th class=""px-4 py-2 text-left"">Data Volume</th>
                <th class=""px-4 py-2 text-left"">Recommended Size</th>
            </tr>
        </thead>
        <tbody>
            <tr><td class=""px-4 py-2"">< 10M events/day</td><td class=""px-4 py-2"">Small or Medium</td></tr>
            <tr><td class=""px-4 py-2"">10M - 100M events/day</td><td class=""px-4 py-2"">Medium or Large</td></tr>
            <tr><td class=""px-4 py-2"">> 100M events/day</td><td class=""px-4 py-2"">Large or X-Large</td></tr>
        </tbody>
    </table>
    <p class=""mt-2 text-sm"">Start with Medium and adjust based on query latency and costs.</p>
</div>

<h3>Performance Tips</h3>
<ul>
    <li><strong>Clustering</strong> - Cluster event tables by event_date and event_type</li>
    <li><strong>Materialisation</strong> - Enable the scratch schema for query caching</li>
    <li><strong>Auto-suspend</strong> - Use auto-suspend to manage costs</li>
    <li><strong>Query tagging</strong> - Analytics queries are tagged for cost tracking</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wc-configuring-databricks",
                    ModuleId = "warehouse-connections",
                    Title = "Configuring Databricks",
                    Summary = "Step-by-step guide to connecting Optimizely Analytics to Databricks.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure Databricks SQL warehouse",
                        "Set up authentication with personal access token",
                        "Connect Databricks to Optimizely Analytics",
                        "Understand Unity Catalog considerations"
                    },
                    Content = @"
<h2>Connecting Databricks</h2>
<p>Databricks provides a unified analytics platform with SQL analytics capabilities. Here's how to connect it to Optimizely Analytics.</p>

<h3>Prerequisites</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium"">Before You Begin:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ Databricks workspace with SQL warehouse access</li>
        <li>✓ Event data in Delta tables</li>
        <li>✓ Permission to create personal access tokens</li>
        <li>✓ Optimizely Analytics account</li>
    </ul>
</div>

<h3>Step 1: Create a SQL Warehouse</h3>
<p>Ensure you have a SQL warehouse configured for Analytics queries:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Recommended Settings:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Type:</strong> Serverless or Pro SQL warehouse</li>
        <li>• <strong>Size:</strong> Small or Medium to start</li>
        <li>• <strong>Auto-stop:</strong> 10-15 minutes</li>
        <li>• <strong>Scaling:</strong> 1-2 clusters max</li>
    </ul>
</div>

<h3>Step 2: Generate a Personal Access Token</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In Databricks, click your profile icon → <strong>User Settings</strong></li>
    <li>Go to the <strong>Access Tokens</strong> tab</li>
    <li>Click <strong>Generate New Token</strong></li>
    <li>Name it ""Optimizely Analytics"" and set an expiration</li>
    <li>Copy the token (you won't see it again)</li>
</ol>

<h3>Step 3: Gather Connection Details</h3>
<p>You'll need these details from your SQL warehouse:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Detail</th>
            <th class=""px-4 py-2 text-left"">Where to Find</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Server Hostname</td><td class=""px-4 py-2"">SQL Warehouse → Connection Details</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">HTTP Path</td><td class=""px-4 py-2"">SQL Warehouse → Connection Details</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Catalog</td><td class=""px-4 py-2"">Your Unity Catalog name (or ""hive_metastore"")</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Schema</td><td class=""px-4 py-2"">The schema containing your data</td></tr>
    </tbody>
</table>

<h3>Step 4: Create a Scratch Schema</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Create a scratch schema for Analytics materialisation
CREATE SCHEMA IF NOT EXISTS your_catalog.optimizely_scratch;

-- Grant necessary permissions (Unity Catalog)
GRANT CREATE TABLE ON SCHEMA your_catalog.optimizely_scratch
  TO `your-user@company.com`;
GRANT USAGE ON SCHEMA your_catalog.optimizely_scratch
  TO `your-user@company.com`;</code></pre>

<h3>Step 5: Configure the Connection</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>In Optimizely Analytics, navigate to <strong>Data → Connections</strong></li>
    <li>Click <strong>+ New Connection</strong></li>
    <li>Select <strong>Databricks</strong></li>
    <li>Enter your connection details:</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  New Databricks Connection                                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Connection Name:    [Production Databricks        ]            │
│                                                                  │
│  Server Hostname:    [adb-xxx.azuredatabricks.net  ]            │
│                                                                  │
│  HTTP Path:          [/sql/1.0/warehouses/xxx      ]            │
│                                                                  │
│  Personal Access     [••••••••••••••••••••••••••••]            │
│  Token:                                                          │
│                                                                  │
│  Catalog:            [your_catalog                 ]            │
│                                                                  │
│  Scratch Schema:     [optimizely_scratch           ]            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Unity Catalog Considerations</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">If Using Unity Catalog:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Ensure the token has access to the catalog</li>
        <li>• Grant SELECT on tables/schemas to be analysed</li>
        <li>• The scratch schema should be in the same catalog</li>
        <li>• Consider using a service principal for production</li>
    </ul>
</div>

<h3>Best Practices</h3>
<ul>
    <li><strong>Use Delta format</strong> - Optimised for analytical queries</li>
    <li><strong>Partition by date</strong> - Partition event tables by event date</li>
    <li><strong>Z-ordering</strong> - Apply Z-ordering on frequently filtered columns</li>
    <li><strong>Photon</strong> - Enable Photon for faster query execution</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wc-configuring-redshift",
                    ModuleId = "warehouse-connections",
                    Title = "Configuring Amazon Redshift",
                    Summary = "Step-by-step guide to connecting Optimizely Analytics to Amazon Redshift.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure Redshift user and permissions",
                        "Set up network access for Analytics",
                        "Connect Redshift to Optimizely Analytics",
                        "Understand Redshift Serverless considerations"
                    },
                    Content = @"
<h2>Connecting Amazon Redshift</h2>
<p>Amazon Redshift is AWS's managed data warehouse service. Here's how to connect it to Optimizely Analytics.</p>

<h3>Prerequisites</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium"">Before You Begin:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ Amazon Redshift cluster or Redshift Serverless workgroup</li>
        <li>✓ Administrative access to create users and grant permissions</li>
        <li>✓ Network connectivity (public access or VPC peering)</li>
        <li>✓ Optimizely Analytics account</li>
    </ul>
</div>

<h3>Step 1: Create a Dedicated User</h3>
<p>Create a user specifically for Optimizely Analytics:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Create a user for Analytics
CREATE USER optimizely_analytics PASSWORD 'YourSecurePassword123!';

-- Grant connect permission
GRANT CONNECT ON DATABASE your_database TO optimizely_analytics;</code></pre>

<h3>Step 2: Grant Read Access</h3>
<p>Grant SELECT permissions on your data schemas:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Grant usage on schemas
GRANT USAGE ON SCHEMA events TO optimizely_analytics;
GRANT USAGE ON SCHEMA users TO optimizely_analytics;

-- Grant SELECT on all existing tables
GRANT SELECT ON ALL TABLES IN SCHEMA events TO optimizely_analytics;
GRANT SELECT ON ALL TABLES IN SCHEMA users TO optimizely_analytics;

-- Grant for future tables
ALTER DEFAULT PRIVILEGES IN SCHEMA events
  GRANT SELECT ON TABLES TO optimizely_analytics;
ALTER DEFAULT PRIVILEGES IN SCHEMA users
  GRANT SELECT ON TABLES TO optimizely_analytics;</code></pre>

<h3>Step 3: Create a Scratch Schema</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Create the scratch schema
CREATE SCHEMA IF NOT EXISTS optimizely_scratch;

-- Grant full access for materialisation
GRANT ALL ON SCHEMA optimizely_scratch TO optimizely_analytics;

-- Allow creating tables
ALTER DEFAULT PRIVILEGES IN SCHEMA optimizely_scratch
  GRANT ALL ON TABLES TO optimizely_analytics;</code></pre>

<h3>Step 4: Configure Network Access</h3>
<p>Ensure Optimizely Analytics can reach your Redshift cluster:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Option A: Publicly Accessible</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• Enable ""Publicly accessible"" on the cluster</li>
        <li>• Configure security group to allow inbound on port 5439</li>
        <li>• Whitelist Optimizely Analytics IP addresses</li>
    </ul>

    <h4 class=""font-semibold mt-4"">Option B: VPC Peering / PrivateLink</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• Set up VPC peering with Optimizely's VPC</li>
        <li>• Or use AWS PrivateLink for secure private connectivity</li>
        <li>• Contact Optimizely support for VPC details</li>
    </ul>
</div>

<h3>Step 5: Configure the Connection</h3>
<p>In Optimizely Analytics, navigate to <strong>Data → Connections</strong> and create a new Redshift connection:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Field</th>
            <th class=""px-4 py-2 text-left"">Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Host</td><td class=""px-4 py-2"">your-cluster.xxxx.region.redshift.amazonaws.com</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Port</td><td class=""px-4 py-2"">5439</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Database</td><td class=""px-4 py-2"">your_database</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Username</td><td class=""px-4 py-2"">optimizely_analytics</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Password</td><td class=""px-4 py-2"">Your secure password</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Scratch Schema</td><td class=""px-4 py-2"">optimizely_scratch</td></tr>
    </tbody>
</table>

<h3>Redshift Serverless</h3>
<p>If using Redshift Serverless:</p>
<ul>
    <li>Use the workgroup endpoint as the host</li>
    <li>Ensure the workgroup has a publicly accessible endpoint or VPC peering</li>
    <li>The port is typically 5439</li>
    <li>Consider setting up a minimum RPU for consistent performance</li>
</ul>

<h3>Performance Tips</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li><strong>Distribution Keys</strong> - Use appropriate distribution keys for join performance</li>
        <li><strong>Sort Keys</strong> - Add sort keys on timestamp and frequently filtered columns</li>
        <li><strong>Compression</strong> - Ensure columns use appropriate compression encodings</li>
        <li><strong>VACUUM/ANALYZE</strong> - Run regularly to maintain query performance</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "wc-performance-optimization",
                    ModuleId = "warehouse-connections",
                    Title = "Performance Optimisation",
                    Summary = "Best practices for optimising query performance and reducing costs.",
                    Order = 6,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand query performance factors",
                        "Learn table design best practices",
                        "Configure materialisation for cost savings",
                        "Monitor and troubleshoot performance issues"
                    },
                    Content = @"
<h2>Performance Optimisation</h2>
<p>Optimising your warehouse configuration can significantly improve query performance and reduce costs. These best practices apply across all supported warehouses.</p>

<h3>Table Design Best Practices</h3>
<p>How you structure your tables has the biggest impact on query performance:</p>

<h4>1. Partitioning / Clustering by Date</h4>
<p>Event tables should always be partitioned by date:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- BigQuery
CREATE TABLE events
PARTITION BY DATE(event_timestamp)
AS SELECT * FROM raw_events;

-- Snowflake (automatic clustering)
ALTER TABLE events CLUSTER BY (DATE(event_timestamp), event_type);

-- Redshift (sort key)
CREATE TABLE events (
  event_timestamp TIMESTAMP,
  event_type VARCHAR(100),
  ...
) SORTKEY (event_timestamp, event_type);</code></pre>

<h4>2. Cluster by Event Type</h4>
<p>Add event_type as a secondary clustering column for queries that filter by event:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Warehouse</th>
            <th class=""px-4 py-2 text-left"">Approach</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">BigQuery</td><td class=""px-4 py-2"">CLUSTER BY event_type</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Snowflake</td><td class=""px-4 py-2"">CLUSTER BY (date, event_type)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Databricks</td><td class=""px-4 py-2"">ZORDER BY (event_type)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Redshift</td><td class=""px-4 py-2"">Compound SORTKEY</td></tr>
    </tbody>
</table>

<h3>Materialisation</h3>
<p>Optimizely Analytics uses a scratch schema to materialise intermediate query results. This significantly improves performance for repeated queries.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">How Materialisation Works:</h4>
    <ol class=""list-decimal list-inside mt-2 space-y-1"">
        <li>First query: Full computation, result stored in scratch schema</li>
        <li>Subsequent queries: Results read from materialised table</li>
        <li>Tables auto-expire after configurable period</li>
        <li>Dashboard refreshes reuse materialised data</li>
    </ol>
</div>

<h4>Enabling Materialisation</h4>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Create a scratch schema in your warehouse</li>
    <li>Grant write permissions to the Analytics connection</li>
    <li>Configure the schema in connection settings</li>
    <li>Enable materialisation in platform settings</li>
</ol>

<h3>Query Performance Tips</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Tip</th>
            <th class=""px-4 py-2 text-left"">Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Limit date ranges</td><td class=""px-4 py-2"">Reduces data scanned dramatically</td></tr>
        <tr><td class=""px-4 py-2"">Use specific event filters</td><td class=""px-4 py-2"">Leverages clustering for faster scans</td></tr>
        <tr><td class=""px-4 py-2"">Avoid SELECT *</td><td class=""px-4 py-2"">Reduces data transfer</td></tr>
        <tr><td class=""px-4 py-2"">Pre-aggregate where possible</td><td class=""px-4 py-2"">Smaller tables = faster queries</td></tr>
        <tr><td class=""px-4 py-2"">Use appropriate warehouse size</td><td class=""px-4 py-2"">Balance speed vs cost</td></tr>
    </tbody>
</table>

<h3>Monitoring Performance</h3>
<p>Track query performance to identify optimisation opportunities:</p>

<ul>
    <li><strong>Query Inspector</strong> - View generated SQL for any exploration</li>
    <li><strong>Warehouse Metrics</strong> - Monitor query duration and data scanned</li>
    <li><strong>Cost Attribution</strong> - Analytics queries are tagged for tracking</li>
</ul>

<h3>Troubleshooting Slow Queries</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Performance Troubleshooting Checklist                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  □ Is the table partitioned by date?                            │
│  □ Is there clustering on event_type?                           │
│  □ Is the date range unnecessarily wide?                        │
│  □ Is the warehouse sized appropriately?                        │
│  □ Is materialisation enabled?                                  │
│  □ Are there complex joins that could be simplified?            │
│  □ Has VACUUM/ANALYZE been run recently (Redshift)?             │
│  □ Is automatic clustering up to date (Snowflake)?              │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Cost Management</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Cost Reduction Strategies:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Auto-suspend</strong> - Configure warehouses to suspend when idle</li>
        <li>• <strong>Materialisation</strong> - Cache results to avoid re-computation</li>
        <li>• <strong>Right-sizing</strong> - Use smaller warehouses for lighter workloads</li>
        <li>• <strong>Scheduling</strong> - Run heavy dashboards during off-peak hours</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 3: Data Modeling

    private LearningModule BuildDataModelingModule()
    {
        return new LearningModule
        {
            Id = "data-modeling",
            Title = "Data Modeling",
            Description = "Create semantic models with Actors and Events, configure datasets, and design optimal schemas for analytics.",
            Icon = "cube-transparent",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "dm-actors-events",
                    ModuleId = "data-modeling",
                    Title = "Understanding Actors and Events",
                    Summary = "Master the foundational concept of Actors (users) and Events (actions) that powers behavioural analytics.",
                    Order = 1,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Deeply understand the Actor concept and its role",
                        "Understand Events and their relationship to Actors",
                        "Learn how the semantic model enables analytics",
                        "Know how to identify Actors and Events in your data"
                    },
                    Content = @"
<h2>The Actor-Event Model</h2>
<p>The foundation of Optimizely Analytics is the <strong>semantic understanding</strong> of your data as Actors and Events. This is what enables powerful behavioural analytics without complex SQL.</p>

<h3>What is an Actor?</h3>
<p>An Actor is the entity performing actions in your system. Most commonly, this is a user, but it can be any entity you want to analyse:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Common Actor Types:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>User</strong> - Individual people using your product (user_id)</li>
        <li>• <strong>Account</strong> - Business accounts in B2B scenarios (account_id)</li>
        <li>• <strong>Device</strong> - Anonymous tracking before login (device_id)</li>
        <li>• <strong>Session</strong> - Website sessions (session_id)</li>
        <li>• <strong>Organisation</strong> - Company-level analytics (org_id)</li>
    </ul>
</div>

<h3>Actor Properties</h3>
<p>Actors have properties (attributes) that describe them:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  ACTORS TABLE (users)                                           │
├─────────────────────────────────────────────────────────────────┤
│  user_id     │ email              │ plan    │ created_at        │
│──────────────┼────────────────────┼─────────┼───────────────────│
│  usr_001     │ alice@example.com  │ premium │ 2024-01-15        │
│  usr_002     │ bob@company.com    │ free    │ 2024-02-20        │
│  usr_003     │ carol@startup.io   │ premium │ 2024-03-05        │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>What is an Event?</h3>
<p>An Event represents an action or occurrence associated with an Actor. Events are timestamped records of things that happened:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Common Event Types:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>page_viewed</strong> - User viewed a page</li>
        <li>• <strong>button_clicked</strong> - User clicked a button or CTA</li>
        <li>• <strong>form_submitted</strong> - User submitted a form</li>
        <li>• <strong>purchase_completed</strong> - User made a purchase</li>
        <li>• <strong>feature_used</strong> - User engaged with a feature</li>
        <li>• <strong>error_occurred</strong> - An error happened for the user</li>
    </ul>
</div>

<h3>Event Properties</h3>
<p>Events have properties that provide context about the action:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────────────────┐
│  EVENTS TABLE                                                               │
├─────────────────────────────────────────────────────────────────────────────┤
│  event_id  │ user_id │ event_type      │ timestamp           │ properties  │
│────────────┼─────────┼─────────────────┼─────────────────────┼─────────────│
│  evt_001   │ usr_001 │ page_viewed     │ 2024-04-01 10:00:00 │ {page: '/'}  │
│  evt_002   │ usr_001 │ button_clicked  │ 2024-04-01 10:05:00 │ {btn: 'cta'} │
│  evt_003   │ usr_001 │ purchase        │ 2024-04-01 10:10:00 │ {amt: 99.00} │
│  evt_004   │ usr_002 │ page_viewed     │ 2024-04-01 11:00:00 │ {page: '/'}  │
└─────────────────────────────────────────────────────────────────────────────┘
</pre>

<h3>The Actor-Event Relationship</h3>
<p>Every Event is linked to an Actor through a foreign key relationship:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
                    1:Many Relationship
┌─────────────┐                      ┌─────────────────────┐
│   ACTORS    │                      │       EVENTS        │
│             │                      │                     │
│  user_id ───┼──────────────────────┼─► user_id (FK)      │
│  email      │      One Actor       │   event_type        │
│  plan       │        has           │   timestamp         │
│  created_at │      Many Events     │   properties        │
│             │                      │                     │
└─────────────┘                      └─────────────────────┘
</pre>

<h3>Why This Matters</h3>
<p>The Actor-Event model enables powerful analytics automatically:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Analysis Type</th>
            <th class=""px-4 py-2 text-left"">How Actor-Event Enables It</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Funnel Analysis</td><td class=""px-4 py-2"">Track same Actor through sequence of Events</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Retention</td><td class=""px-4 py-2"">See if Actors return to perform Events over time</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cohorts</td><td class=""px-4 py-2"">Group Actors by shared properties or behaviours</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">User Journeys</td><td class=""px-4 py-2"">Follow an Actor's Events chronologically</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Segmentation</td><td class=""px-4 py-2"">Break down Events by Actor attributes</td></tr>
    </tbody>
</table>

<h3>Identifying Actors and Events in Your Data</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Questions to Ask:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Who</strong> is performing actions? → This is your Actor</li>
        <li>• <strong>What</strong> actions are being recorded? → These are your Events</li>
        <li>• <strong>How</strong> are they connected? → The foreign key relationship</li>
        <li>• <strong>When</strong> did things happen? → Event timestamps</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dm-creating-datasets",
                    ModuleId = "data-modeling",
                    Title = "Creating Datasets",
                    Summary = "Configure warehouse tables as datasets in Optimizely Analytics.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create a new dataset from a warehouse table",
                        "Configure primary keys and required columns",
                        "Set dataset type (Actor or Event)",
                        "Manage multiple datasets"
                    },
                    Content = @"
<h2>Creating Datasets</h2>
<p>A Dataset in Optimizely Analytics is a configured connection to a table in your data warehouse. It defines which tables are available for analysis and how they should be interpreted.</p>

<h3>Dataset Types</h3>
<p>When creating a dataset, you specify its type:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Examples</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Actor</td><td class=""px-4 py-2"">Represents entities (users, accounts)</td><td class=""px-4 py-2"">users, accounts, devices</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Event</td><td class=""px-4 py-2"">Represents actions with timestamps</td><td class=""px-4 py-2"">events, page_views, transactions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Lookup</td><td class=""px-4 py-2"">Reference data for enrichment</td><td class=""px-4 py-2"">products, campaigns, countries</td></tr>
    </tbody>
</table>

<h3>Creating an Actor Dataset</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Datasets</strong></li>
    <li>Click <strong>+ New Dataset</strong></li>
    <li>Select your warehouse connection</li>
    <li>Choose the table (e.g., <code>users</code>)</li>
    <li>Set the dataset type to <strong>Actor</strong></li>
    <li>Configure the primary key (e.g., <code>user_id</code>)</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  New Actor Dataset                                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:           [Users                           ]             │
│                                                                  │
│  Connection:     [Production BigQuery     ▼       ]             │
│                                                                  │
│  Table:          [analytics.users         ▼       ]             │
│                                                                  │
│  Dataset Type:   (●) Actor  ( ) Event  ( ) Lookup               │
│                                                                  │
│  Primary Key:    [user_id                 ▼       ]             │
│                                                                  │
│  Display Name                                                    │
│  Column:         [email                   ▼       ]             │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Creating an Event Dataset</h3>
<p>Event datasets require additional configuration for timestamps and event types:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Create a new dataset and select your events table</li>
    <li>Set the dataset type to <strong>Event</strong></li>
    <li>Configure the timestamp column</li>
    <li>Specify the event type column</li>
    <li>Link to the Actor dataset</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  New Event Dataset                                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:           [Events                          ]             │
│                                                                  │
│  Table:          [analytics.events        ▼       ]             │
│                                                                  │
│  Dataset Type:   ( ) Actor  (●) Event  ( ) Lookup               │
│                                                                  │
│  ── Event Configuration ──────────────────────────────────────  │
│                                                                  │
│  Timestamp:      [event_timestamp         ▼       ]             │
│                                                                  │
│  Event Type:     [event_name              ▼       ]             │
│                                                                  │
│  ── Actor Relationship ───────────────────────────────────────  │
│                                                                  │
│  Actor Dataset:  [Users                   ▼       ]             │
│                                                                  │
│  Actor Column:   [user_id                 ▼       ]             │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Column Selection</h3>
<p>Choose which columns to expose in Analytics:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Best Practices:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• Include columns needed for analysis</li>
        <li>• Exclude PII columns if not needed</li>
        <li>• Add friendly display names for clarity</li>
        <li>• Set appropriate data types</li>
    </ul>
</div>

<h3>Dataset Preview</h3>
<p>After configuration, preview your dataset to verify:</p>
<ul>
    <li>Column mappings are correct</li>
    <li>Data types are properly inferred</li>
    <li>Sample data looks as expected</li>
    <li>Relationships will work correctly</li>
</ul>

<h3>Managing Datasets</h3>
<p>Over time, you'll manage multiple datasets:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Action</th>
            <th class=""px-4 py-2 text-left"">When to Use</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Edit</td><td class=""px-4 py-2"">Update columns, relationships, or settings</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Refresh</td><td class=""px-4 py-2"">Detect new columns added to the source table</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Duplicate</td><td class=""px-4 py-2"">Create a similar dataset with modifications</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Delete</td><td class=""px-4 py-2"">Remove unused datasets (careful—affects explorations)</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dm-semantic-configuration",
                    ModuleId = "data-modeling",
                    Title = "Semantic Configuration",
                    Summary = "Configure the semantic layer with event types, timestamps, and actor relationships.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure event type mappings",
                        "Set up timestamp handling",
                        "Define actor relationships",
                        "Understand how semantics enable analytics features"
                    },
                    Content = @"
<h2>Semantic Configuration</h2>
<p>The semantic configuration tells Optimizely Analytics how to interpret your data. This is what enables features like funnels, retention, and path analysis without custom SQL.</p>

<h3>Event Type Configuration</h3>
<p>The Event Type column identifies what kind of action each event represents:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Event Types in Your Data                                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  event_type column contains values like:                        │
│                                                                  │
│    • page_viewed                                                │
│    • button_clicked                                             │
│    • form_submitted                                             │
│    • purchase_completed                                         │
│    • sign_up                                                    │
│    • feature_enabled                                            │
│                                                                  │
│  Analytics uses this to:                                        │
│    ✓ Populate event dropdowns                                   │
│    ✓ Enable funnel step selection                               │
│    ✓ Power event segmentation                                   │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Timestamp Configuration</h3>
<p>The timestamp column is essential for time-based analysis:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Timestamp Requirements:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• Must be a datetime/timestamp type</li>
        <li>• Should be in UTC (or consistent timezone)</li>
        <li>• Used for time-based filtering and grouping</li>
        <li>• Powers retention and funnel timing</li>
    </ul>
</div>

<h4>Partition Column</h4>
<p>If your table is partitioned, specify the partition column:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Example: Table partitioned by date
-- Partition column: event_date
-- Timestamp column: event_timestamp

-- Analytics uses partition column for efficient filtering:
WHERE event_date BETWEEN '2024-01-01' AND '2024-01-31'
  AND event_timestamp >= '2024-01-01 00:00:00'</code></pre>

<h3>Actor Relationship Configuration</h3>
<p>Link events to their actors for behavioural analysis:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Actor Dataset</td><td class=""px-4 py-2"">Which dataset contains actors</td><td class=""px-4 py-2"">Users</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Actor Column</td><td class=""px-4 py-2"">Foreign key in events table</td><td class=""px-4 py-2"">user_id</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Relationship Type</td><td class=""px-4 py-2"">How events relate to actors</td><td class=""px-4 py-2"">Many-to-One</td></tr>
    </tbody>
</table>

<h3>Multiple Actor Types</h3>
<p>You can have multiple actor relationships for different analyses:</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Example: B2B SaaS Product</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>User Actor</strong> - Individual user behaviour</li>
        <li>• <strong>Account Actor</strong> - Company-level analysis</li>
        <li>• <strong>Device Actor</strong> - Anonymous pre-login tracking</li>
    </ul>
    <p class=""mt-2 text-sm"">Each actor type enables different analytical perspectives on the same events.</p>
</div>

<h3>Event Properties</h3>
<p>Configure additional event properties for richer analysis:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Event Properties Configuration                                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Property Name    │ Column        │ Type      │ Use In          │
│───────────────────┼───────────────┼───────────┼─────────────────│
│  Page URL         │ page_url      │ String    │ Grouping        │
│  Button ID        │ element_id    │ String    │ Filtering       │
│  Revenue          │ order_total   │ Number    │ Sum/Average     │
│  Product Category │ category      │ String    │ Segmentation    │
│  Session ID       │ session_id    │ String    │ Session analysis│
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>How Semantics Enable Features</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Required Semantics</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Funnels</td><td class=""px-4 py-2"">Event types + Actor + Timestamps</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Retention</td><td class=""px-4 py-2"">Actor + Timestamps</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Paths</td><td class=""px-4 py-2"">Event types + Actor + Timestamps</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cohorts</td><td class=""px-4 py-2"">Actor + Properties</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Segmentation</td><td class=""px-4 py-2"">Event types + Properties</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dm-relationships-joins",
                    ModuleId = "data-modeling",
                    Title = "Relationships & Joins",
                    Summary = "Connect datasets through relationships for cross-table analysis.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create relationships between datasets",
                        "Understand join types and when to use them",
                        "Configure lookup tables for enrichment",
                        "Avoid common relationship pitfalls"
                    },
                    Content = @"
<h2>Relationships & Joins</h2>
<p>Connecting datasets through relationships enables rich analysis that spans multiple tables without writing complex SQL joins.</p>

<h3>Relationship Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Many-to-One</td><td class=""px-4 py-2"">Many events belong to one actor</td><td class=""px-4 py-2"">Events → Users</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">One-to-One</td><td class=""px-4 py-2"">Each record maps to exactly one other</td><td class=""px-4 py-2"">User → Profile</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Many-to-Many</td><td class=""px-4 py-2"">Complex relationships via junction</td><td class=""px-4 py-2"">Users ↔ Groups</td></tr>
    </tbody>
</table>

<h3>Creating a Relationship</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Datasets</strong></li>
    <li>Select the dataset to configure</li>
    <li>Go to the <strong>Relationships</strong> tab</li>
    <li>Click <strong>+ Add Relationship</strong></li>
    <li>Select the target dataset and join columns</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Configure Relationship                                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  From Dataset:   Events                                         │
│  From Column:    [product_id              ▼       ]             │
│                                                                  │
│          ────────────────────────                               │
│                     ▼                                            │
│                                                                  │
│  To Dataset:     [Products                ▼       ]             │
│  To Column:      [id                      ▼       ]             │
│                                                                  │
│  Relationship    ( ) One-to-One                                 │
│  Type:           (●) Many-to-One                                │
│                  ( ) Many-to-Many                               │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Lookup Tables</h3>
<p>Lookup tables enrich your events with additional context:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Common Lookup Tables:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Products</strong> - Product name, category, price</li>
        <li>• <strong>Campaigns</strong> - Campaign name, channel, budget</li>
        <li>• <strong>Countries</strong> - Country name, region, timezone</li>
        <li>• <strong>Plans</strong> - Subscription tier details</li>
    </ul>
</div>

<h4>Example: Product Lookup</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
EVENTS TABLE                          PRODUCTS TABLE (Lookup)
┌──────────────────────────┐          ┌───────────────────────────┐
│ event_id │ product_id    │          │ id    │ name    │ category│
│──────────┼───────────────│          │───────┼─────────┼─────────│
│ evt_001  │ prod_123      │─────────►│prod_123│ Widget │ Hardware│
│ evt_002  │ prod_456      │─────────►│prod_456│ Gadget │ Software│
└──────────────────────────┘          └───────────────────────────┘

With relationship configured, you can:
  • Group events by product.category
  • Filter events where product.name = 'Widget'
  • Segment by product attributes
</pre>

<h3>Join Behaviour</h3>
<p>Understanding how joins work in Analytics:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Join Type</th>
            <th class=""px-4 py-2 text-left"">Behaviour</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Left Join</td><td class=""px-4 py-2"">Keep all events, enrich where possible</td><td class=""px-4 py-2"">Product details on purchases</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Inner Join</td><td class=""px-4 py-2"">Only events with matching lookup</td><td class=""px-4 py-2"">Only known products</td></tr>
    </tbody>
</table>

<h3>Common Pitfalls</h3>
<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium"">Avoid These Issues:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Duplicate joins</strong> - Don't create multiple paths to the same table</li>
        <li>• <strong>Wrong cardinality</strong> - Misconfigured relationship types cause incorrect counts</li>
        <li>• <strong>Missing foreign keys</strong> - Ensure join columns have matching values</li>
        <li>• <strong>Circular relationships</strong> - Avoid A→B→C→A cycles</li>
    </ul>
</div>

<h3>Testing Relationships</h3>
<p>Verify relationships work correctly:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Create a simple exploration using both datasets</li>
    <li>Group by a column from the lookup table</li>
    <li>Verify counts match expectations</li>
    <li>Check for NULL values from failed joins</li>
</ol>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dm-schema-best-practices",
                    ModuleId = "data-modeling",
                    Title = "Schema Design Best Practices",
                    Summary = "Design optimal data schemas for effective analytics.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Design event schemas for analytics",
                        "Structure actor tables effectively",
                        "Handle common data modelling challenges",
                        "Plan for scale and performance"
                    },
                    Content = @"
<h2>Schema Design Best Practices</h2>
<p>A well-designed data schema makes analytics easier, faster, and more reliable. Follow these best practices when setting up your data for Optimizely Analytics.</p>

<h3>Event Table Design</h3>
<p>Your event table is the heart of behavioural analytics. Design it carefully:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Recommended Event Table Structure
CREATE TABLE events (
    -- Identity
    event_id        STRING NOT NULL,        -- Unique event identifier

    -- Actor Reference
    user_id         STRING NOT NULL,        -- Foreign key to actors
    anonymous_id    STRING,                 -- For pre-login tracking

    -- Event Classification
    event_type      STRING NOT NULL,        -- e.g., 'page_viewed'
    event_category  STRING,                 -- e.g., 'engagement'

    -- Timing
    event_timestamp TIMESTAMP NOT NULL,     -- When it happened
    event_date      DATE NOT NULL,          -- Partition column

    -- Context
    session_id      STRING,                 -- Session grouping
    page_url        STRING,                 -- Where it happened
    referrer        STRING,                 -- Traffic source

    -- Properties (denormalised for performance)
    device_type     STRING,                 -- mobile, desktop, tablet
    country         STRING,                 -- Geo location
    browser         STRING,                 -- User agent info

    -- Business Properties
    revenue         DECIMAL(10,2),          -- For revenue events
    product_id      STRING,                 -- For product events

    -- Flexible Properties
    properties      JSON                    -- Additional event data
)
PARTITION BY event_date
CLUSTER BY event_type;</code></pre>

<h3>Actor Table Design</h3>
<p>Actor tables should capture identity and attributes:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Recommended Actor (User) Table Structure
CREATE TABLE users (
    -- Identity
    user_id         STRING NOT NULL PRIMARY KEY,

    -- Profile
    email           STRING,
    display_name    STRING,

    -- Lifecycle
    created_at      TIMESTAMP NOT NULL,
    first_seen_at   TIMESTAMP,
    last_seen_at    TIMESTAMP,

    -- Segmentation Attributes
    plan_type       STRING,                 -- free, pro, enterprise
    account_id      STRING,                 -- For B2B
    country         STRING,

    -- Computed Attributes
    lifetime_value  DECIMAL(10,2),
    total_purchases INTEGER,

    -- Status
    is_active       BOOLEAN,
    churned_at      TIMESTAMP
);</code></pre>

<h3>Naming Conventions</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <table class=""min-w-full"">
        <thead>
            <tr>
                <th class=""px-4 py-2 text-left"">Element</th>
                <th class=""px-4 py-2 text-left"">Convention</th>
                <th class=""px-4 py-2 text-left"">Example</th>
            </tr>
        </thead>
        <tbody>
            <tr><td class=""px-4 py-2"">Tables</td><td class=""px-4 py-2"">Lowercase, plural</td><td class=""px-4 py-2"">users, events, products</td></tr>
            <tr><td class=""px-4 py-2"">Columns</td><td class=""px-4 py-2"">snake_case</td><td class=""px-4 py-2"">user_id, created_at</td></tr>
            <tr><td class=""px-4 py-2"">Event Types</td><td class=""px-4 py-2"">snake_case verbs</td><td class=""px-4 py-2"">page_viewed, purchase_completed</td></tr>
            <tr><td class=""px-4 py-2"">Boolean</td><td class=""px-4 py-2"">is_ or has_ prefix</td><td class=""px-4 py-2"">is_active, has_verified_email</td></tr>
        </tbody>
    </table>
</div>

<h3>Handling Common Challenges</h3>

<h4>1. Anonymous to Known User</h4>
<p>Track users before and after login:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Store both IDs on events
anonymous_id    STRING,         -- Device/cookie ID
user_id         STRING,         -- NULL until login

-- Create identity mapping table
CREATE TABLE identity_mappings (
    anonymous_id    STRING,
    user_id         STRING,
    merged_at       TIMESTAMP
);</code></pre>

<h4>2. Slowly Changing Dimensions</h4>
<p>Track attribute changes over time:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Option 1: Snapshot table
CREATE TABLE user_snapshots (
    user_id         STRING,
    plan_type       STRING,
    snapshot_date   DATE,
    PRIMARY KEY (user_id, snapshot_date)
);

-- Option 2: Store on events
-- Denormalise current values onto each event</code></pre>

<h4>3. High Cardinality Properties</h4>
<p>Handle properties with many unique values:</p>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <ul class=""space-y-1"">
        <li>• Use lookup tables for product_id, campaign_id</li>
        <li>• Avoid grouping by very high cardinality columns</li>
        <li>• Consider bucketing or categorising values</li>
    </ul>
</div>

<h3>Performance Considerations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Partition by date</td><td class=""px-4 py-2"">Dramatically reduces data scanned</td></tr>
        <tr><td class=""px-4 py-2"">Cluster by event_type</td><td class=""px-4 py-2"">Speeds up filtered queries</td></tr>
        <tr><td class=""px-4 py-2"">Denormalise key attributes</td><td class=""px-4 py-2"">Reduces joins needed</td></tr>
        <tr><td class=""px-4 py-2"">Use appropriate types</td><td class=""px-4 py-2"">Smaller storage, faster queries</td></tr>
        <tr><td class=""px-4 py-2"">Avoid wide JSON columns</td><td class=""px-4 py-2"">Extract frequently used fields</td></tr>
    </tbody>
</table>

<h3>Schema Evolution</h3>
<p>Plan for changes over time:</p>
<ul>
    <li><strong>Add columns</strong> - Easy, just add and Analytics will detect</li>
    <li><strong>Rename columns</strong> - Update dataset configuration</li>
    <li><strong>New event types</strong> - Automatically available</li>
    <li><strong>Remove columns</strong> - Update dataset, check explorations</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 4: Exploration Templates

    private LearningModule BuildExplorationTemplatesModule()
    {
        return new LearningModule
        {
            Id = "exploration-templates",
            Title = "Exploration Templates",
            Description = "Master self-service exploration templates for event segmentation, funnels, retention, and path analysis.",
            Icon = "magnifying-glass-circle",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "et-introduction",
                    ModuleId = "exploration-templates",
                    Title = "Introduction to Explorations",
                    Summary = "Understand the exploration system and available templates for self-service analytics.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what explorations are and how they work",
                        "Know the available exploration templates",
                        "Learn the common exploration interface elements",
                        "Understand when to use each template type"
                    },
                    Content = @"
<h2>Introduction to Explorations</h2>
<p>Explorations are the primary way to analyse data in Optimizely Analytics. They provide <strong>self-service templates</strong> that enable anyone to perform sophisticated behavioural analysis without writing SQL.</p>

<h3>What is an Exploration?</h3>
<p>An exploration is an interactive analysis session that lets you investigate your data using pre-built templates. Each template is optimised for specific analytical questions.</p>

<h3>Available Templates</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Template</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Key Question</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Event Segmentation</td><td class=""px-4 py-2"">Analyse event frequency and trends</td><td class=""px-4 py-2"">How often does X happen?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Funnel</td><td class=""px-4 py-2"">Track conversion through steps</td><td class=""px-4 py-2"">What % complete the flow?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Retention</td><td class=""px-4 py-2"">Measure return behaviour</td><td class=""px-4 py-2"">Do users come back?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Path</td><td class=""px-4 py-2"">Visualise user journeys</td><td class=""px-4 py-2"">What do users do next?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">SQL Explore</td><td class=""px-4 py-2"">Custom SQL queries</td><td class=""px-4 py-2"">Any custom analysis</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">NetScript Explore</td><td class=""px-4 py-2"">Advanced analytical queries</td><td class=""px-4 py-2"">Complex computations</td></tr>
    </tbody>
</table>

<h3>Common Interface Elements</h3>
<p>All exploration templates share common interface components:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────────────┐
│  Exploration Interface                                                  │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌──────────────────────┐  ┌─────────────────────────────────────────┐ │
│  │                      │  │                                         │ │
│  │  CONFIGURATION       │  │           VISUALISATION                 │ │
│  │  PANEL               │  │                                         │ │
│  │                      │  │   Charts, tables, graphs                │ │
│  │  • Events            │  │   showing your results                  │ │
│  │  • Measures          │  │                                         │ │
│  │  • Segmentation      │  │                                         │ │
│  │  • Time Range        │  │                                         │ │
│  │  • Filters           │  │                                         │ │
│  │                      │  │                                         │ │
│  └──────────────────────┘  └─────────────────────────────────────────┘ │
│                                                                         │
│  ┌───────────────────────────────────────────────────────────────────┐ │
│  │  [Run]  [Save]  [Share]  [Add to Dashboard]  [Summarise with AI]  │ │
│  └───────────────────────────────────────────────────────────────────┘ │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
</pre>

<h3>Configuration Modules</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Events Module</h4>
    <p class=""mt-1"">Select which user actions to analyse</p>

    <h4 class=""font-semibold mt-3"">Measures Module</h4>
    <p class=""mt-1"">Choose how to count or aggregate (total, unique, average)</p>

    <h4 class=""font-semibold mt-3"">Segmentation Module</h4>
    <p class=""mt-1"">Break down by cohorts or attributes (""Grouped by"")</p>

    <h4 class=""font-semibold mt-3"">Visualisation Module</h4>
    <p class=""mt-1"">Configure chart type, colours, and display options</p>
</div>

<h3>When to Use Each Template</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Question Type</th>
            <th class=""px-4 py-2 text-left"">Template</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">""How many users did X this week?""</td><td class=""px-4 py-2"">Event Segmentation</td></tr>
        <tr><td class=""px-4 py-2"">""What % of users complete checkout?""</td><td class=""px-4 py-2"">Funnel</td></tr>
        <tr><td class=""px-4 py-2"">""Do users return after signing up?""</td><td class=""px-4 py-2"">Retention</td></tr>
        <tr><td class=""px-4 py-2"">""What do users do after viewing a product?""</td><td class=""px-4 py-2"">Path</td></tr>
        <tr><td class=""px-4 py-2"">""Custom analysis not covered above""</td><td class=""px-4 py-2"">SQL/NetScript</td></tr>
    </tbody>
</table>

<h3>Saving and Sharing</h3>
<p>After creating an exploration:</p>
<ul>
    <li><strong>Save</strong> - Keep for later reference</li>
    <li><strong>Share</strong> - Send link to team members</li>
    <li><strong>Add to Dashboard</strong> - Pin to a monitoring dashboard</li>
    <li><strong>Export</strong> - Download as CSV or PDF</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-event-segmentation",
                    ModuleId = "exploration-templates",
                    Title = "Event Segmentation Analysis",
                    Summary = "Analyse event frequency, trends, and breakdowns using the Event Segmentation template.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create event segmentation analyses",
                        "Apply different measure types",
                        "Use grouping and filtering",
                        "Interpret event trend visualisations"
                    },
                    Content = @"
<h2>Event Segmentation Analysis</h2>
<p>Event Segmentation is the most versatile exploration template. It answers questions about <strong>how often events occur</strong> and <strong>how they trend over time</strong>.</p>

<h3>Core Use Cases</h3>
<ul>
    <li>Track daily/weekly/monthly active users</li>
    <li>Monitor feature adoption over time</li>
    <li>Compare event frequency across segments</li>
    <li>Identify trends and anomalies</li>
    <li>Measure KPIs like page views or purchases</li>
</ul>

<h3>Configuration Options</h3>

<h4>1. Events Selection</h4>
<p>Choose which events to analyse:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Events: [ page_viewed        ▼ ]  ← Select one or more events
        [ + Add Another Event  ]

Options:
  • Any Event - All events combined
  • Specific Event - e.g., ""purchase_completed""
  • Multiple Events - Compare several events
</pre>

<h4>2. Measure Selection</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Measure</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Total Events</td><td class=""px-4 py-2"">Count all occurrences</td><td class=""px-4 py-2"">50,000 page views</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Unique Users</td><td class=""px-4 py-2"">Count distinct actors</td><td class=""px-4 py-2"">10,000 unique visitors</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Events per User</td><td class=""px-4 py-2"">Average frequency</td><td class=""px-4 py-2"">5 pages per user</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sum of Property</td><td class=""px-4 py-2"">Total of numeric value</td><td class=""px-4 py-2"">$25,000 revenue</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Average of Property</td><td class=""px-4 py-2"">Mean of numeric value</td><td class=""px-4 py-2"">$50 average order</td></tr>
    </tbody>
</table>

<h4>3. Group By (Segmentation)</h4>
<p>Break down results by dimension:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>By Time:</strong> Day, Week, Month, Hour</p>
    <p><strong>By Property:</strong> Device type, Country, Page URL</p>
    <p><strong>By Actor Attribute:</strong> Plan type, Account, Sign-up date</p>
</div>

<h4>4. Filters</h4>
<p>Narrow your analysis:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Filters:
  └─ Where [country] [equals] [United Kingdom]
     AND [device_type] [equals] [mobile]
</pre>

<h3>Example Analysis: Weekly Active Users</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Weekly Active Users - Last 8 Weeks                             │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Configuration:                                                  │
│    Event: Any Event                                             │
│    Measure: Unique Users                                        │
│    Group By: Week                                               │
│                                                                  │
│  Result:                                                         │
│                                                                  │
│   25k │              ╭──────╮                                   │
│       │         ╭────╯      ╰────╮                              │
│   20k │    ╭────╯                ╰────╮                         │
│       │╭───╯                          ╰───                      │
│   15k ││                                                        │
│       │                                                         │
│   10k │                                                         │
│       └─────────────────────────────────────────────────        │
│        W1   W2   W3   W4   W5   W6   W7   W8                   │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Advanced Techniques</h3>

<h4>Comparing Segments</h4>
<p>Add multiple Group By dimensions to compare:</p>
<ul>
    <li>Free vs Premium users</li>
    <li>Mobile vs Desktop</li>
    <li>New vs Returning users</li>
</ul>

<h4>Using Formulas</h4>
<p>Combine measures for calculated metrics:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>Conversion Rate = (Unique Users who Purchase) / (Unique Users who View Product) × 100</code></pre>

<h4>Time Comparisons</h4>
<p>Compare to previous period:</p>
<ul>
    <li>This week vs Last week</li>
    <li>This month vs Same month last year</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-funnel-analysis",
                    ModuleId = "exploration-templates",
                    Title = "Funnel Analysis",
                    Summary = "Track conversion through multi-step processes using the Funnel template.",
                    Order = 3,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Create funnel analyses with multiple steps",
                        "Configure funnel conversion windows",
                        "Identify drop-off points",
                        "Use segmentation to compare funnels"
                    },
                    Content = @"
<h2>Funnel Analysis</h2>
<p>Funnel analysis tracks how users progress through a sequence of steps, revealing where they <strong>convert</strong> and where they <strong>drop off</strong>.</p>

<h3>Common Funnel Examples</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>E-commerce:</strong> Product View → Add to Cart → Checkout → Purchase</li>
        <li><strong>SaaS Signup:</strong> Landing Page → Start Trial → Complete Onboarding</li>
        <li><strong>Content:</strong> Article View → Scroll 50% → Scroll 100% → Share</li>
        <li><strong>B2B:</strong> Demo Request → Demo Completed → Proposal Sent → Deal Won</li>
    </ul>
</div>

<h3>Creating a Funnel</h3>

<h4>Step 1: Define Your Steps</h4>
<p>Add events in the order users should complete them:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Funnel Steps                                                    │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Step 1: [ product_viewed           ▼ ]  ≡ (drag to reorder)   │
│                                                                  │
│  Step 2: [ add_to_cart              ▼ ]  ≡                      │
│                                                                  │
│  Step 3: [ checkout_started         ▼ ]  ≡                      │
│                                                                  │
│  Step 4: [ purchase_completed       ▼ ]  ≡                      │
│                                                                  │
│  [ + Add Step ]                                                  │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h4>Step 2: Configure Conversion Window</h4>
<p>Set the maximum time allowed between steps:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Window</th>
            <th class=""px-4 py-2 text-left"">Use When</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Same Session</td><td class=""px-4 py-2"">Immediate actions (checkout flow)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">1 Day</td><td class=""px-4 py-2"">Short consideration purchases</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">7 Days</td><td class=""px-4 py-2"">Standard e-commerce</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">30 Days</td><td class=""px-4 py-2"">B2B or high-value purchases</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Custom</td><td class=""px-4 py-2"">Specific business requirements</td></tr>
    </tbody>
</table>

<h4>Step 3: Choose Counting Method</h4>
<ul>
    <li><strong>Unique Users</strong> - Count each user once (most common)</li>
    <li><strong>Total Conversions</strong> - Count all completions</li>
</ul>

<h3>Reading Funnel Results</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  E-commerce Checkout Funnel                                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ████████████████████████████████████████  Product Viewed       │
│  10,000 users (100%)                       ▼ 40% drop-off       │
│                                                                  │
│  ████████████████████████                  Add to Cart          │
│  6,000 users (60%)                         ▼ 33% drop-off       │
│                                                                  │
│  ████████████████                          Checkout Started     │
│  4,000 users (40%)                         ▼ 25% drop-off       │
│                                                                  │
│  ████████████                              Purchase Completed   │
│  3,000 users (30%)                                              │
│                                                                  │
│  Overall Conversion Rate: 30%                                   │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Analysing Drop-offs</h3>
<p>Investigate where users leave:</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Click on a drop-off to explore:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Who dropped off?</strong> - View user cohort</li>
        <li>• <strong>What did they do instead?</strong> - Path analysis</li>
        <li>• <strong>Common attributes?</strong> - Segment by device, location</li>
    </ul>
</div>

<h3>Comparing Funnels by Segment</h3>
<p>Use the Segmentation module to compare conversion rates:</p>
<ul>
    <li>Mobile vs Desktop users</li>
    <li>New vs Returning visitors</li>
    <li>Different traffic sources</li>
    <li>Geographic regions</li>
</ul>

<h3>Funnel Best Practices</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Why</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Keep steps to 5-7 max</td><td class=""px-4 py-2"">Too many steps are hard to analyse</td></tr>
        <tr><td class=""px-4 py-2"">Use meaningful events</td><td class=""px-4 py-2"">Each step should represent real progress</td></tr>
        <tr><td class=""px-4 py-2"">Set appropriate windows</td><td class=""px-4 py-2"">Match your user journey timing</td></tr>
        <tr><td class=""px-4 py-2"">Segment for insights</td><td class=""px-4 py-2"">Averages hide important differences</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-retention-analysis",
                    ModuleId = "exploration-templates",
                    Title = "Retention Analysis",
                    Summary = "Measure user engagement and return behaviour over time.",
                    Order = 4,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create retention analyses",
                        "Understand retention metrics (N-day, Unbounded)",
                        "Interpret retention curves and cohort tables",
                        "Identify factors that improve retention"
                    },
                    Content = @"
<h2>Retention Analysis</h2>
<p>Retention analysis measures whether users <strong>come back</strong> after their initial engagement. It's essential for understanding long-term product health and user engagement.</p>

<h3>Why Retention Matters</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Product-Market Fit</strong> - Are users finding value?</li>
        <li>• <strong>Growth Efficiency</strong> - Retained users don't need re-acquisition</li>
        <li>• <strong>Revenue Predictability</strong> - Retained users drive recurring revenue</li>
        <li>• <strong>Benchmark Performance</strong> - Compare against industry standards</li>
    </ul>
</div>

<h3>Retention Configuration</h3>

<h4>Starting Event (Cohort Entry)</h4>
<p>When does retention measurement begin?</p>
<ul>
    <li><strong>First Time:</strong> User's first occurrence of the event</li>
    <li><strong>Any Time:</strong> Any occurrence during the period</li>
</ul>

<h4>Return Event</h4>
<p>What action defines ""coming back""?</p>
<ul>
    <li><strong>Same Event:</strong> User performs the starting event again</li>
    <li><strong>Different Event:</strong> User performs a specific action</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Retention Configuration                                         │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Starting Event:  [ sign_up                     ▼ ]             │
│  (●) First time user performs event                             │
│  ( ) Any time user performs event                               │
│                                                                  │
│  Return Event:    [ any_active_event            ▼ ]             │
│                                                                  │
│  Retention Type:  [N-Day Retention              ▼ ]             │
│                                                                  │
│  Time Periods:    Day 1, Day 7, Day 14, Day 30                  │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Retention Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Definition</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">N-Day</td><td class=""px-4 py-2"">User returns on exactly day N</td><td class=""px-4 py-2"">Strict daily engagement</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Unbounded</td><td class=""px-4 py-2"">User returns on or before day N</td><td class=""px-4 py-2"">General engagement tracking</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Bracket</td><td class=""px-4 py-2"">User returns within a range</td><td class=""px-4 py-2"">Weekly/monthly patterns</td></tr>
    </tbody>
</table>

<h3>Reading Retention Results</h3>

<h4>Retention Curve</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  30-Day Retention Curve                                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  100%│●                                                         │
│      │                                                          │
│   75%│  ●                                                       │
│      │    ●                                                     │
│   50%│      ●●                                                  │
│      │        ●●●                                               │
│   25%│           ●●●●●●●●●●●●●●●●●●●●●●●●●●                     │
│      │                                                          │
│    0%└──────────────────────────────────────────────────────    │
│       D1  D3  D7     D14       D21        D30                   │
│                                                                  │
│  Key Insights:                                                   │
│  • D1 Retention: 65%                                            │
│  • D7 Retention: 35%                                            │
│  • D30 Retention: 20%                                           │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h4>Cohort Retention Table</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Retention by Sign-up Week                                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Cohort    │ Users │ W1   │ W2   │ W3   │ W4   │ W5            │
│────────────┼───────┼──────┼──────┼──────┼──────┼────────       │
│  Jan 1-7   │ 1,000 │ 40%  │ 30%  │ 25%  │ 22%  │ 20%          │
│  Jan 8-14  │ 1,200 │ 45%  │ 35%  │ 28%  │ 25%  │ --           │
│  Jan 15-21 │ 1,100 │ 42%  │ 32%  │ 26%  │ --   │ --           │
│  Jan 22-28 │ 1,300 │ 48%  │ 38%  │ --   │ --   │ --           │
│  Jan 29+   │ 900   │ 50%  │ --   │ --   │ --   │ --           │
│                                                                  │
│  ↑ Retention improving over time! Recent cohorts perform better │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Improving Retention</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Investigation Strategies:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Segment by onboarding completion</strong> - Do onboarded users retain better?</li>
        <li>• <strong>Compare power users</strong> - What do retained users do differently?</li>
        <li>• <strong>Analyse churned cohorts</strong> - What's common among those who leave?</li>
        <li>• <strong>Time to value</strong> - How quickly do users reach their ""aha moment""?</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-path-analysis",
                    ModuleId = "exploration-templates",
                    Title = "Path Analysis",
                    Summary = "Visualise user journeys and understand navigation patterns.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create path analyses to visualise user journeys",
                        "Configure path starting and ending points",
                        "Interpret Sankey diagrams and path flows",
                        "Identify common and problematic paths"
                    },
                    Content = @"
<h2>Path Analysis</h2>
<p>Path analysis visualises the <strong>sequences of actions</strong> users take, helping you understand navigation patterns, discover common journeys, and identify unexpected behaviour.</p>

<h3>When to Use Path Analysis</h3>
<ul>
    <li>Understanding how users navigate your product</li>
    <li>Discovering common paths to conversion</li>
    <li>Finding unexpected user journeys</li>
    <li>Identifying navigation problems</li>
    <li>Optimising user flows</li>
</ul>

<h3>Path Configuration</h3>

<h4>Starting Point</h4>
<p>Where does the path begin?</p>
<ul>
    <li><strong>Specific Event:</strong> e.g., ""After users view the homepage""</li>
    <li><strong>Any Event:</strong> All possible starting points</li>
    <li><strong>Session Start:</strong> Beginning of each session</li>
</ul>

<h4>Ending Point (Optional)</h4>
<p>Where should the path end?</p>
<ul>
    <li><strong>Specific Event:</strong> e.g., ""Paths leading to purchase""</li>
    <li><strong>No End:</strong> Show all subsequent events</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Path Configuration                                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Starting Point:                                                 │
│  (●) Specific Event  [ homepage_viewed          ▼ ]             │
│  ( ) Any Event                                                  │
│  ( ) Session Start                                              │
│                                                                  │
│  Ending Point:                                                   │
│  ( ) Specific Event  [ purchase_completed       ▼ ]             │
│  (●) None (show all paths)                                      │
│                                                                  │
│  Path Depth:  [ 5 steps                         ▼ ]             │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Reading Path Visualisations</h3>

<h4>Sankey Diagram</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  User Paths from Homepage                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│               Step 1           Step 2           Step 3          │
│                                                                  │
│            ┌─────────────► Product View ────► Add to Cart       │
│  Homepage ─┤   45%              │ 30%                            │
│    100%    │                    │                                │
│            ├─────────────► Search ──────────► Product View      │
│            │   25%         │ 60%                                │
│            │               │                                     │
│            ├─────────────► Category ────────► Product View      │
│            │   20%              │ 50%                            │
│            │                                                     │
│            └─────────────► Exit                                 │
│                10%                                               │
│                                                                  │
│  Width of flow = percentage of users taking that path           │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Path Analysis Techniques</h3>

<h4>Forward Paths (""What do users do after..."")</h4>
<p>Start from a specific event and see what happens next:</p>
<ul>
    <li>After viewing a product, do users add to cart or leave?</li>
    <li>After signing up, what's the first action?</li>
    <li>After an error, do users retry or abandon?</li>
</ul>

<h4>Reverse Paths (""How did users get to..."")</h4>
<p>Set an ending point and see how users arrived:</p>
<ul>
    <li>What paths lead to purchase?</li>
    <li>How do users discover key features?</li>
    <li>What triggers support requests?</li>
</ul>

<h3>Filtering Paths</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Narrow down to specific paths:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Include Event:</strong> Paths that go through a specific event</li>
        <li>• <strong>Exclude Event:</strong> Paths that avoid an event</li>
        <li>• <strong>Segment:</strong> Paths for specific user groups</li>
    </ul>
</div>

<h3>Common Insights from Path Analysis</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Pattern</th>
            <th class=""px-4 py-2 text-left"">Implication</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">High exit rate after step</td><td class=""px-4 py-2"">Potential UX issue or confusion</td></tr>
        <tr><td class=""px-4 py-2"">Unexpected popular path</td><td class=""px-4 py-2"">Feature being used in new way</td></tr>
        <tr><td class=""px-4 py-2"">Long winding paths</td><td class=""px-4 py-2"">Users struggling to find what they need</td></tr>
        <tr><td class=""px-4 py-2"">Short paths to conversion</td><td class=""px-4 py-2"">Effective onboarding or navigation</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-custom-explorations",
                    ModuleId = "exploration-templates",
                    Title = "Building Custom Explorations",
                    Summary = "Combine templates and advanced features for custom analyses.",
                    Order = 6,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Combine multiple analysis techniques",
                        "Save and reuse exploration configurations",
                        "Create calculated metrics in explorations",
                        "Export and schedule exploration results"
                    },
                    Content = @"
<h2>Building Custom Explorations</h2>
<p>While templates cover common use cases, you'll often need to <strong>customise</strong> explorations for specific business questions. This lesson covers advanced techniques.</p>

<h3>Combining Techniques</h3>
<p>Layer multiple analysis approaches in a single exploration:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Example: Conversion Analysis</h4>
    <ol class=""list-decimal list-inside mt-2 space-y-1"">
        <li>Start with a Funnel to see overall conversion</li>
        <li>Add segmentation by traffic source</li>
        <li>Filter to specific date range</li>
        <li>Compare against previous period</li>
    </ol>
</div>

<h3>Calculated Metrics</h3>
<p>Create metrics derived from other values:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Create Calculated Metric                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:     [Conversion Rate                      ]              │
│                                                                  │
│  Formula:                                                        │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │  (Unique Users who Purchase / Unique Users who View)    │   │
│  │  × 100                                                   │   │
│  └─────────────────────────────────────────────────────────┘   │
│                                                                  │
│  Format:   [ Percentage (%)                      ▼ ]            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h4>Common Calculated Metrics</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">Formula</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Conversion Rate</td><td class=""px-4 py-2"">Conversions / Visitors × 100</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ARPU</td><td class=""px-4 py-2"">Total Revenue / Unique Users</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Pages per Session</td><td class=""px-4 py-2"">Page Views / Sessions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cart Abandonment</td><td class=""px-4 py-2"">(Carts - Purchases) / Carts × 100</td></tr>
    </tbody>
</table>

<h3>Saving Explorations</h3>
<p>Save your work for reuse and sharing:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Save</strong> after configuring your exploration</li>
    <li>Give it a descriptive name</li>
    <li>Assign to a category (e.g., ""Marketing"", ""Product"")</li>
    <li>Set permissions for who can view/edit</li>
</ol>

<h3>Scheduling and Alerts</h3>
<p>Automate exploration delivery:</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <h4 class=""font-semibold"">Schedule Options</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Daily</strong> - Receive results every morning</li>
        <li>• <strong>Weekly</strong> - Summary at end of week</li>
        <li>• <strong>Monthly</strong> - Monthly report delivery</li>
    </ul>

    <h4 class=""font-semibold mt-4"">Alert Conditions</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• Metric drops below threshold</li>
        <li>• Conversion rate changes significantly</li>
        <li>• Anomaly detected in data</li>
    </ul>
</div>

<h3>Exporting Results</h3>
<p>Share exploration results outside Analytics:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Format</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">CSV</td><td class=""px-4 py-2"">Further analysis in Excel or other tools</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">PDF</td><td class=""px-4 py-2"">Sharing visualisations in reports</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Link</td><td class=""px-4 py-2"">Direct access to live exploration</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Embed</td><td class=""px-4 py-2"">Include in internal dashboards</td></tr>
    </tbody>
</table>

<h3>Using Opal AI</h3>
<p>Leverage AI to understand your explorations:</p>

<ul>
    <li><strong>Summarise</strong> - Get a plain-language summary of results</li>
    <li><strong>Explain</strong> - Understand why metrics changed</li>
    <li><strong>Suggest</strong> - Get recommendations for next steps</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Opal AI Summary                                                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ""Your conversion rate increased 15% this week compared to      │
│  last week. The main driver was a 25% increase in mobile        │
│  conversions, likely due to the checkout flow improvements      │
│  deployed on Tuesday. Desktop conversions remained flat.        │
│                                                                  │
│  Recommendation: Consider applying similar optimisations to     │
│  the desktop experience to see comparable gains.""               │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 5: Metrics & Measures

    private LearningModule BuildMetricsMeasuresModule()
    {
        return new LearningModule
        {
            Id = "metrics-measures",
            Title = "Metrics & Measures",
            Description = "Define and configure metrics, understand measurement types, and create custom KPIs for your business.",
            Icon = "calculator",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "mm-events-vs-metrics",
                    ModuleId = "metrics-measures",
                    Title = "Events vs Metrics",
                    Summary = "Understand the fundamental difference between events (data inputs) and metrics (interpretations).",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the difference between events and metrics",
                        "Know when to use events vs metrics",
                        "Learn how metrics are calculated from events",
                        "Understand the role of metrics in analysis"
                    },
                    Content = @"
<h2>Events vs Metrics</h2>
<p>Understanding the distinction between <strong>Events</strong> and <strong>Metrics</strong> is fundamental to effective analytics in Optimizely.</p>

<h3>The Core Distinction</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Events = Raw Data</h4>
    <p class=""mt-1"">Events are the raw records of things that happened. They track specific visitor behaviours like clicks, page views, form submissions, and purchases.</p>

    <h4 class=""font-semibold mt-4"">Metrics = Interpretation</h4>
    <p class=""mt-1"">Metrics are how you interpret and aggregate that event data. They answer business questions by counting, summing, or calculating from events.</p>
</div>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Events → Metrics Relationship                                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  EVENTS (What Happened)              METRICS (What It Means)    │
│  ─────────────────────               ──────────────────────     │
│                                                                  │
│  purchase_completed  ───────────►    Total Revenue              │
│  event_value: $50                    Sum of event_value         │
│  event_value: $75                    = $125                     │
│                                                                  │
│  purchase_completed  ───────────►    Conversion Rate            │
│  (2 users purchased)                 Purchasers / Visitors      │
│  page_viewed                         = 2 / 100 = 2%             │
│  (100 users visited)                                            │
│                                                                  │
│  button_clicked      ───────────►    Clicks per User            │
│  (500 clicks by                      Total Clicks / Users       │
│   200 users)                         = 500 / 200 = 2.5          │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Events in Detail</h3>
<p>Events have several key characteristics:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Characteristic</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Always On</td><td class=""px-4 py-2"">Events track continuously once configured</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Reusable</td><td class=""px-4 py-2"">Same event can be used in multiple metrics</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Timestamped</td><td class=""px-4 py-2"">Every event has a time it occurred</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Have Properties</td><td class=""px-4 py-2"">Events carry additional context (page URL, amount)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Linked to Actors</td><td class=""px-4 py-2"">Every event belongs to a user/account</td></tr>
    </tbody>
</table>

<h3>Metrics in Detail</h3>
<p>Metrics define how to interpret events:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Event Selection</td><td class=""px-4 py-2"">Which event(s) to measure</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Aggregation</td><td class=""px-4 py-2"">How to combine values (count, sum, average)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Filters</td><td class=""px-4 py-2"">Which events to include</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Formatting</td><td class=""px-4 py-2"">How to display (percentage, currency)</td></tr>
    </tbody>
</table>

<h3>When to Create Metrics</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Create a reusable metric when:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• You use the same calculation repeatedly</li>
        <li>• Multiple team members need the same KPI</li>
        <li>• You want consistent definitions across analyses</li>
        <li>• You need the metric in experiments</li>
    </ul>
</div>

<h3>Example: Building a Revenue Metric</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Event: purchase_completed
  └── Properties:
      ├── order_total: $50.00
      ├── product_id: ""SKU123""
      └── user_id: ""user_456""

Metric: Total Revenue
  └── Configuration:
      ├── Event: purchase_completed
      ├── Aggregation: Sum of order_total
      ├── Filter: order_total > 0
      └── Format: Currency ($)

Result: $50.00 + $75.00 + $125.00 = $250.00 Total Revenue
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mm-metric-types",
                    ModuleId = "metrics-measures",
                    Title = "Types of Metrics",
                    Summary = "Learn the different metric types: unique conversions, total, revenue, value, and ratio.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand each metric type and its use case",
                        "Know when to use unique vs total metrics",
                        "Learn how to configure revenue and value metrics",
                        "Understand ratio metrics for calculated KPIs"
                    },
                    Content = @"
<h2>Types of Metrics</h2>
<p>Optimizely Analytics provides several metric types, each designed for specific analytical needs.</p>

<h3>Metric Type Overview</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">What It Measures</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Unique Conversions</td><td class=""px-4 py-2"">Distinct users who performed event</td><td class=""px-4 py-2"">500 users purchased</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Total Conversions</td><td class=""px-4 py-2"">Total number of event occurrences</td><td class=""px-4 py-2"">750 total purchases</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Total Revenue</td><td class=""px-4 py-2"">Sum of revenue property</td><td class=""px-4 py-2"">$50,000 revenue</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Total Value</td><td class=""px-4 py-2"">Sum of any numeric property</td><td class=""px-4 py-2"">1,500 items in carts</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Ratio</td><td class=""px-4 py-2"">One metric divided by another</td><td class=""px-4 py-2"">Revenue per visitor</td></tr>
    </tbody>
</table>

<h3>Unique Conversions</h3>
<p>Counts the number of <strong>distinct users</strong> who triggered an event at least once.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Data:
  User A: purchase × 3 times
  User B: purchase × 1 time
  User C: purchase × 2 times

Unique Conversions = 3 users (A, B, C)
Total Conversions = 6 events
</pre>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Use Unique Conversions for:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Conversion rate calculations</li>
        <li>• Counting how many users reached a goal</li>
        <li>• Measuring reach or adoption</li>
    </ul>
</div>

<h3>Total Conversions</h3>
<p>Counts <strong>every occurrence</strong> of an event, including multiple from the same user.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Use Total Conversions for:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Page view counts</li>
        <li>• Click counts</li>
        <li>• Total orders (for repeat purchase businesses)</li>
    </ul>
</div>

<h3>Total Revenue</h3>
<p>Sums the <strong>revenue property</strong> across all events.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Configuration:
  Event: purchase_completed
  Property: order_total (or revenue tag)
  Aggregation: Sum

Data:
  Purchase 1: order_total = $50
  Purchase 2: order_total = $75
  Purchase 3: order_total = $100

Total Revenue = $225
</pre>

<h3>Total Value</h3>
<p>Like Total Revenue, but for <strong>any numeric property</strong>—not just revenue.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Use Total Value for:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Items added to cart (sum of quantity)</li>
        <li>• Points earned (sum of points)</li>
        <li>• Time spent (sum of duration)</li>
    </ul>
</div>

<h3>Ratio Metrics</h3>
<p>Calculate a <strong>ratio between two values</strong>—powerful for business KPIs.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Ratio Metric Configuration                                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Metric Name:  [Revenue per Visitor                ]            │
│                                                                  │
│  Numerator:    [Total Revenue                      ▼]           │
│                                                                  │
│  Denominator:  [Unique Visitors                    ▼]           │
│                                                                  │
│  Result:       $50,000 / 10,000 visitors = $5.00 per visitor    │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h4>Common Ratio Metrics</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">Numerator</th>
            <th class=""px-4 py-2 text-left"">Denominator</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Conversion Rate</td><td class=""px-4 py-2"">Unique Purchases</td><td class=""px-4 py-2"">Unique Visitors</td></tr>
        <tr><td class=""px-4 py-2"">Revenue per User</td><td class=""px-4 py-2"">Total Revenue</td><td class=""px-4 py-2"">Unique Users</td></tr>
        <tr><td class=""px-4 py-2"">AOV (Average Order Value)</td><td class=""px-4 py-2"">Total Revenue</td><td class=""px-4 py-2"">Total Orders</td></tr>
        <tr><td class=""px-4 py-2"">Add-to-Cart Rate</td><td class=""px-4 py-2"">Unique Add to Carts</td><td class=""px-4 py-2"">Unique Product Views</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mm-creating-custom-metrics",
                    ModuleId = "metrics-measures",
                    Title = "Creating Custom Metrics",
                    Summary = "Build reusable custom metrics that reflect your business KPIs.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create a new custom metric",
                        "Configure metric properties and filters",
                        "Test and validate metrics",
                        "Organise metrics for team use"
                    },
                    Content = @"
<h2>Creating Custom Metrics</h2>
<p>Custom metrics let you define <strong>reusable KPIs</strong> that your team can use consistently across explorations, dashboards, and experiments.</p>

<h3>When to Create Custom Metrics</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>✓ You need the same measurement in multiple places</li>
        <li>✓ Multiple team members should use the same definition</li>
        <li>✓ You want to track a KPI in experiments</li>
        <li>✓ The calculation is complex enough to warrant saving</li>
    </ul>
</div>

<h3>Creating a Metric</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Metrics</strong></li>
    <li>Click <strong>+ New Metric</strong></li>
    <li>Select the metric type</li>
    <li>Configure the event and properties</li>
    <li>Set formatting options</li>
    <li>Save and name your metric</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Create New Metric                                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:         [Checkout Completion Rate           ]            │
│                                                                  │
│  Description:  [Percentage of users who complete checkout       │
│                 after starting it                    ]          │
│                                                                  │
│  ── Metric Definition ──────────────────────────────────────── │
│                                                                  │
│  Type:         (●) Ratio  ( ) Unique  ( ) Total  ( ) Value     │
│                                                                  │
│  Numerator:                                                      │
│    Event:      [ purchase_completed            ▼ ]              │
│    Measure:    [ Unique Users                  ▼ ]              │
│                                                                  │
│  Denominator:                                                    │
│    Event:      [ checkout_started              ▼ ]              │
│    Measure:    [ Unique Users                  ▼ ]              │
│                                                                  │
│  ── Display Options ─────────────────────────────────────────  │
│                                                                  │
│  Format:       [ Percentage                    ▼ ]              │
│  Decimals:     [ 2                             ▼ ]              │
│                                                                  │
│  ┌──────────────────┐  ┌──────────────────┐                     │
│  │     Cancel       │  │       Save       │                     │
│  └──────────────────┘  └──────────────────┘                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Adding Filters</h3>
<p>Narrow your metric to specific events:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Metric: Mobile Conversion Rate

Filters:
  └─ Where [device_type] [equals] [mobile]

This metric only counts conversions from mobile users
</pre>

<h3>Testing Your Metric</h3>
<p>Before saving, validate your metric works correctly:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Preview</strong> to see sample values</li>
    <li>Verify the numbers make sense</li>
    <li>Check against known values if available</li>
    <li>Test in an exploration before using in experiments</li>
</ol>

<h3>Metric Organisation</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Benefit</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Use clear, descriptive names</td><td class=""px-4 py-2"">Easy to find and understand</td></tr>
        <tr><td class=""px-4 py-2"">Add descriptions</td><td class=""px-4 py-2"">Document the business meaning</td></tr>
        <tr><td class=""px-4 py-2"">Categorise by team/area</td><td class=""px-4 py-2"">Keep metrics organised</td></tr>
        <tr><td class=""px-4 py-2"">Version with dates if needed</td><td class=""px-4 py-2"">Track definition changes</td></tr>
    </tbody>
</table>

<h3>Example Metrics Library</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">E-commerce Metrics:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Conversion Rate</strong> - Purchasers / Visitors</li>
        <li>• <strong>AOV</strong> - Revenue / Orders</li>
        <li>• <strong>Cart Abandonment</strong> - (Carts - Purchases) / Carts</li>
        <li>• <strong>Revenue per Visitor</strong> - Revenue / Visitors</li>
    </ul>

    <p class=""font-medium mt-4"">SaaS Metrics:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Activation Rate</strong> - Activated / Signed Up</li>
        <li>• <strong>Feature Adoption</strong> - Feature Users / All Users</li>
        <li>• <strong>Trial Conversion</strong> - Subscribed / Trial Started</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mm-metric-presets",
                    ModuleId = "metrics-measures",
                    Title = "Metric Presets",
                    Summary = "Configure default formatting and experiment settings that apply everywhere.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what metric presets are",
                        "Configure formatting presets",
                        "Set experiment-specific settings",
                        "Apply presets consistently across use cases"
                    },
                    Content = @"
<h2>Metric Presets</h2>
<p>Metric Presets let you define <strong>formatting and experiment settings once</strong> at the metric level, so those settings automatically apply everywhere the metric is used.</p>

<h3>The Problem Presets Solve</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Without presets, you'd need to:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Re-add ""$"" currency symbol every time</li>
        <li>• Fix percentage formatting repeatedly</li>
        <li>• Reconfigure improvement direction for experiments</li>
        <li>• Set up alerts from scratch each time</li>
    </ul>
    <p class=""mt-4 font-medium"">With presets, these settings are defined once and applied automatically.</p>
</div>

<h3>Formatting Presets</h3>
<p>Control how metric values are displayed:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Options</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Number Format</td><td class=""px-4 py-2"">Number, Currency, Percentage</td><td class=""px-4 py-2"">$1,234.56</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Currency</td><td class=""px-4 py-2"">USD, GBP, EUR, etc.</td><td class=""px-4 py-2"">£1,234.56</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Decimal Places</td><td class=""px-4 py-2"">0, 1, 2, 3, 4</td><td class=""px-4 py-2"">12.34%</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Thousands Separator</td><td class=""px-4 py-2"">Comma, Space, None</td><td class=""px-4 py-2"">1,000,000</td></tr>
    </tbody>
</table>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Formatting Preset                                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Metric: Total Revenue                                          │
│                                                                  │
│  Format Type:     (●) Currency  ( ) Number  ( ) Percentage      │
│                                                                  │
│  Currency:        [ USD ($)                        ▼ ]          │
│                                                                  │
│  Decimal Places:  [ 2                              ▼ ]          │
│                                                                  │
│  Preview:         $12,345.67                                    │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Experiment Presets</h3>
<p>Configure how the metric behaves in A/B tests:</p>

<h4>Improvement Direction</h4>
<p>Does ""higher is better"" or ""lower is better""?</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Increase is Good (↑):</strong></p>
    <ul class=""space-y-1"">
        <li>• Conversion rate</li>
        <li>• Revenue</li>
        <li>• Engagement</li>
    </ul>

    <p class=""mt-4""><strong>Decrease is Good (↓):</strong></p>
    <ul class=""space-y-1"">
        <li>• Bounce rate</li>
        <li>• Page load time</li>
        <li>• Error rate</li>
    </ul>
</div>

<h4>Alert Thresholds</h4>
<p>Set when to notify on metric changes:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Alert Configuration:
  └─ Notify when metric drops more than [10%] from baseline
  └─ Notify when metric exceeds [+20%] (possible tracking issue)
</pre>

<h3>Applying Presets</h3>
<p>Once configured, presets apply automatically:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>In explorations - Values display with correct format</li>
    <li>In dashboards - Consistent appearance</li>
    <li>In experiments - Correct improvement direction</li>
    <li>In alerts - Thresholds already configured</li>
</ol>

<h3>Benefits of Presets</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Consistency</td><td class=""px-4 py-2"">Same metric looks the same everywhere</td></tr>
        <tr><td class=""px-4 py-2"">Time Saving</td><td class=""px-4 py-2"">No repeated configuration</td></tr>
        <tr><td class=""px-4 py-2"">Accuracy</td><td class=""px-4 py-2"">Reduces human error in settings</td></tr>
        <tr><td class=""px-4 py-2"">Governance</td><td class=""px-4 py-2"">Central control of definitions</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mm-ratio-metrics",
                    ModuleId = "metrics-measures",
                    Title = "Ratio Metrics",
                    Summary = "Build advanced ratio metrics for sophisticated business KPIs.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create ratio metrics with custom numerators/denominators",
                        "Configure ratio metrics for experiments",
                        "Understand statistical considerations for ratios",
                        "Build common business ratio metrics"
                    },
                    Content = @"
<h2>Ratio Metrics</h2>
<p>Ratio metrics are among the most powerful metric types, allowing you to create <strong>calculated KPIs</strong> by dividing one metric by another.</p>

<h3>Ratio Metric Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
                    Numerator
Ratio Metric = ─────────────────
                   Denominator

Example:
                    Unique Purchasers
Conversion Rate = ─────────────────────
                    Unique Visitors

                    500 purchasers
                = ───────────────── = 5%
                    10,000 visitors
</pre>

<h3>Creating a Ratio Metric</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Metrics → + New Metric</strong></li>
    <li>Select <strong>Ratio</strong> as the metric type</li>
    <li>Configure the numerator (top of fraction)</li>
    <li>Configure the denominator (bottom of fraction)</li>
    <li>Set formatting options</li>
</ol>

<h3>Numerator & Denominator Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Option</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Unique Users</td><td class=""px-4 py-2"">Count of distinct users who did something</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Total Events</td><td class=""px-4 py-2"">Count of all event occurrences</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sum of Value</td><td class=""px-4 py-2"">Sum of a numeric property</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Another Metric</td><td class=""px-4 py-2"">Reference an existing metric</td></tr>
    </tbody>
</table>

<h3>Common Ratio Metric Examples</h3>

<h4>Conversion Rate</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Numerator:   Unique Users who purchase_completed
Denominator: Unique Users who visited (any event)
Format:      Percentage (%)
</pre>

<h4>Average Order Value (AOV)</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Numerator:   Sum of order_total (revenue)
Denominator: Total purchase_completed events
Format:      Currency ($)
</pre>

<h4>Revenue per Visitor</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Numerator:   Sum of order_total (revenue)
Denominator: Unique Visitors
Format:      Currency ($)
</pre>

<h4>Feature Adoption Rate</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Numerator:   Unique Users who feature_used
Denominator: Unique Users who logged_in
Format:      Percentage (%)
</pre>

<h3>Using Different Events</h3>
<p>For A/B tests, you can select different events for numerator and denominator:</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Add-to-Cart Rate by Product Views:</p>
    <pre class=""bg-gray-900 text-green-400 p-3 rounded mt-2 text-sm"">
Numerator:   add_to_cart events
Denominator: product_viewed events

This measures the conversion from viewing to carting</pre>
</div>

<h3>Statistical Considerations</h3>
<p>Ratio metrics have special considerations in experiments:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Consideration</th>
            <th class=""px-4 py-2 text-left"">Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Variance in denominator</td><td class=""px-4 py-2"">Can affect statistical significance</td></tr>
        <tr><td class=""px-4 py-2"">Outliers</td><td class=""px-4 py-2"">Large values can skew averages</td></tr>
        <tr><td class=""px-4 py-2"">Sample size</td><td class=""px-4 py-2"">Need enough events in both parts</td></tr>
        <tr><td class=""px-4 py-2"">Delta method</td><td class=""px-4 py-2"">Stats Engine uses proper ratio statistics</td></tr>
    </tbody>
</table>

<h3>Best Practices</h3>
<ul>
    <li><strong>Use meaningful denominators</strong> - Ensure the denominator represents the relevant population</li>
    <li><strong>Check for outliers</strong> - Consider capping extreme values</li>
    <li><strong>Test before using</strong> - Validate ratios produce sensible values</li>
    <li><strong>Document clearly</strong> - Explain what the ratio measures</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 6: Cohorts & Segmentation

    private LearningModule BuildCohortsSegmentationModule()
    {
        return new LearningModule
        {
            Id = "cohorts-segmentation",
            Title = "Cohorts & Segmentation",
            Description = "Create user cohorts based on behaviour, attributes, and custom formulas for targeted analysis.",
            Icon = "user-group",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cs-understanding-cohorts",
                    ModuleId = "cohorts-segmentation",
                    Title = "Understanding Cohorts",
                    Summary = "Learn what cohorts are and how they enable powerful user segmentation.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what cohorts are in analytics",
                        "Know the different types of cohorts",
                        "Learn why cohorts are valuable for analysis",
                        "Understand cohort vs segment distinction"
                    },
                    Content = @"
<h2>Understanding Cohorts</h2>
<p>A cohort is a <strong>group of users who share a common characteristic</strong>. Cohort analysis is a powerful technique that lets you compare how different groups of users behave over time.</p>

<h3>What is a Cohort?</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Think of cohorts as ""clubs"" that users belong to:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Users who signed up in January</li>
        <li>• Users who came from paid advertising</li>
        <li>• Users who completed onboarding</li>
        <li>• Users on the premium plan</li>
    </ul>
    <p class=""mt-4"">Each ""club"" can be tracked and compared separately.</p>
</div>

<h3>Types of Cohorts</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Based On</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Acquisition</td><td class=""px-4 py-2"">When user first appeared</td><td class=""px-4 py-2"">January sign-ups vs February</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Behavioural</td><td class=""px-4 py-2"">Actions users have taken</td><td class=""px-4 py-2"">Users who completed onboarding</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Attribute</td><td class=""px-4 py-2"">User properties</td><td class=""px-4 py-2"">Premium vs Free users</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Predictive</td><td class=""px-4 py-2"">Likelihood scores</td><td class=""px-4 py-2"">High churn risk users</td></tr>
    </tbody>
</table>

<h3>Cohorts vs Segments</h3>
<p>The terms are often used interchangeably, but there's a subtle difference:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Cohorts vs Segments                                             │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  COHORT                           SEGMENT                        │
│  ──────                           ───────                        │
│  A defined group of ACTORS        A breakdown of DATA            │
│  (users/accounts)                 (events/metrics)               │
│                                                                  │
│  ""Show me users who...""          ""Break down this metric by...""│
│                                                                  │
│  Example:                         Example:                       │
│  Users who signed up              Events grouped by              │
│  in January                       country                        │
│                                                                  │
│  Used in: Performed by            Used in: Grouped by            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Why Cohorts Matter</h3>
<ul>
    <li><strong>Reveal Hidden Patterns</strong> - Averages can hide important differences between user groups</li>
    <li><strong>Track Progress Over Time</strong> - See if newer users behave differently than older ones</li>
    <li><strong>Identify Success Factors</strong> - What do retained users have in common?</li>
    <li><strong>Target Interventions</strong> - Focus efforts on specific user groups</li>
</ul>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Example Insight:</p>
    <p class=""mt-2"">""Users who complete onboarding within 24 hours have 3x higher retention than those who don't.""</p>
    <p class=""mt-2"">This insight comes from comparing the ""Completed Onboarding"" cohort against those who didn't.</p>
</div>

<h3>Cohorts in Optimizely Analytics</h3>
<p>In Analytics, cohorts appear in several places:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Location</th>
            <th class=""px-4 py-2 text-left"">How Cohorts Are Used</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Segmentation Module</td><td class=""px-4 py-2"">Filter explorations to specific cohorts</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Retention Analysis</td><td class=""px-4 py-2"">Define entry cohorts for retention</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Funnel Analysis</td><td class=""px-4 py-2"">Compare conversion by cohort</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data → Cohorts</td><td class=""px-4 py-2"">Create and manage saved cohorts</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cs-behavioral-cohorts",
                    ModuleId = "cohorts-segmentation",
                    Title = "Behavioural Cohorts",
                    Summary = "Create cohorts based on user actions and behaviour patterns.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create cohorts based on event behaviour",
                        "Configure frequency and recency conditions",
                        "Combine multiple behavioural criteria",
                        "Use behavioural cohorts in analysis"
                    },
                    Content = @"
<h2>Behavioural Cohorts</h2>
<p>Behavioural cohorts group users based on <strong>actions they have (or haven't) taken</strong>. This is powerful because behaviour often predicts future outcomes better than demographics.</p>

<h3>Creating a Behavioural Cohort</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Cohorts</strong></li>
    <li>Click <strong>+ New Cohort</strong></li>
    <li>Select <strong>Behavioural</strong> template</li>
    <li>Define the behaviour criteria</li>
    <li>Save and name your cohort</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  New Behavioural Cohort                                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:   [Power Users                              ]            │
│                                                                  │
│  Definition: Users who...                                        │
│                                                                  │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │  [ performed ▼ ]  [ feature_used ▼ ]                     │    │
│  │  [ at least  ▼ ]  [ 10 ] times                          │    │
│  │  [ in the last ▼ ] [ 30 days ▼ ]                        │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  [ + Add Another Condition ]                                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Behaviour Conditions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Condition</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Performed</td><td class=""px-4 py-2"">Users who performed 'purchase'</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Did Not Perform</td><td class=""px-4 py-2"">Users who never completed onboarding</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Performed First Time</td><td class=""px-4 py-2"">Users whose first action was 'signup'</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Frequency</td><td class=""px-4 py-2"">Users who logged in 5+ times</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recency</td><td class=""px-4 py-2"">Users active in last 7 days</td></tr>
    </tbody>
</table>

<h3>Combining Conditions</h3>
<p>Create complex cohorts with AND/OR logic:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
""High-Value Engaged Users""

Users who:
  ├── performed 'purchase' at least 2 times in last 90 days
  │
  AND
  │
  └── performed 'feature_used' at least 10 times in last 30 days
</pre>

<h3>Common Behavioural Cohorts</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Engagement Cohorts</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Power Users</strong> - 10+ sessions in last 30 days</li>
        <li>• <strong>Casual Users</strong> - 1-3 sessions in last 30 days</li>
        <li>• <strong>Dormant Users</strong> - No activity in 60+ days</li>
    </ul>

    <h4 class=""font-semibold mt-4"">Lifecycle Cohorts</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>New Users</strong> - First seen in last 7 days</li>
        <li>• <strong>Activated Users</strong> - Completed key action after signup</li>
        <li>• <strong>Churned Users</strong> - Previously active, now inactive</li>
    </ul>

    <h4 class=""font-semibold mt-4"">Feature Cohorts</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Feature Adopters</strong> - Used specific feature</li>
        <li>• <strong>Non-Adopters</strong> - Never used the feature</li>
    </ul>
</div>

<h3>Using Behavioural Cohorts</h3>
<p>Once created, use cohorts in your analysis:</p>

<ul>
    <li><strong>Filter explorations</strong> - ""Show me page views for Power Users only""</li>
    <li><strong>Compare cohorts</strong> - ""How does retention differ between activated vs non-activated?""</li>
    <li><strong>Identify patterns</strong> - ""What do churned users have in common?""</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cs-formula-cohorts",
                    ModuleId = "cohorts-segmentation",
                    Title = "Formula Cohorts",
                    Summary = "Create cohorts using arithmetic and logical operators for advanced segmentation.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create formula-based cohorts",
                        "Use arithmetic operators in cohort definitions",
                        "Apply logical operations to combine criteria",
                        "Build complex segmentation rules"
                    },
                    Content = @"
<h2>Formula Cohorts</h2>
<p>Formula cohorts let you define user groups using <strong>arithmetic and logical operators</strong>. This enables sophisticated segmentation based on calculated values.</p>

<h3>When to Use Formula Cohorts</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Use formulas when you need:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Calculations on user properties</li>
        <li>• String matching (email domains)</li>
        <li>• Date-based logic</li>
        <li>• Complex conditional criteria</li>
    </ul>
</div>

<h3>Creating a Formula Cohort</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Cohorts</strong></li>
    <li>Click <strong>+ New Cohort</strong></li>
    <li>Select <strong>Formula</strong> template</li>
    <li>Write your formula expression</li>
    <li>Test and save</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Formula Cohort                                                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:   [Enterprise Customers                     ]            │
│                                                                  │
│  Formula:                                                        │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │  email LIKE '%@enterprise.com'                          │    │
│  │  OR                                                      │    │
│  │  (plan_type = 'enterprise' AND account_seats >= 50)     │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  Preview: 1,245 users match                                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Available Operators</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Operator</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">=, !=, >, <, >=, <=</td><td class=""px-4 py-2"">age >= 25</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">AND, OR, NOT</td><td class=""px-4 py-2"">plan = 'pro' AND active = true</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">LIKE</td><td class=""px-4 py-2"">email LIKE '%@company.com'</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IN</td><td class=""px-4 py-2"">country IN ('US', 'UK', 'CA')</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IS NULL, IS NOT NULL</td><td class=""px-4 py-2"">phone IS NOT NULL</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">BETWEEN</td><td class=""px-4 py-2"">ltv BETWEEN 100 AND 500</td></tr>
    </tbody>
</table>

<h3>Formula Examples</h3>

<h4>Email Domain Matching</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Users from specific company domains
email LIKE '%@bigcorp.com'
OR email LIKE '%@bigcorp.co.uk'</code></pre>

<h4>Calculated Value Threshold</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- High-value customers (LTV above average)
lifetime_value > 500
AND total_orders >= 3</code></pre>

<h4>Date-Based Cohort</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- Users who signed up this quarter
created_at >= '2024-01-01'
AND created_at < '2024-04-01'</code></pre>

<h4>Geographic Segment</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>-- EMEA region users
country IN ('UK', 'DE', 'FR', 'ES', 'IT', 'NL', 'BE', 'SE', 'NO')
OR region = 'EMEA'</code></pre>

<h3>Testing Formulas</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Before saving, always:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Preview the user count</li>
        <li>• Check if count is reasonable</li>
        <li>• Verify with sample users</li>
        <li>• Test edge cases</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cs-conditional-cohorts",
                    ModuleId = "cohorts-segmentation",
                    Title = "Conditional Cohorts",
                    Summary = "Create cohorts using property-based conditions with AND/OR logic.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create conditional cohorts based on properties",
                        "Use AND/OR logic effectively",
                        "Combine multiple conditions",
                        "Understand condition evaluation order"
                    },
                    Content = @"
<h2>Conditional Cohorts</h2>
<p>Conditional cohorts let you define user groups using <strong>property-based conditions</strong> with a visual AND/OR builder—no formula writing required.</p>

<h3>Creating a Conditional Cohort</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Cohorts</strong></li>
    <li>Click <strong>+ New Cohort</strong></li>
    <li>Select <strong>Conditional on Property</strong> template</li>
    <li>Add conditions using the visual builder</li>
    <li>Save your cohort</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Conditional Cohort Builder                                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:   [Active Premium Users                     ]            │
│                                                                  │
│  Include users where:                                            │
│                                                                  │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │  [plan_type     ▼]  [equals     ▼]  [premium    ▼]     │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  [ AND ▼ ]                                                       │
│                                                                  │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │  [is_active     ▼]  [equals     ▼]  [true       ▼]     │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  [ + Add Condition ]                                             │
│                                                                  │
│  Preview: 5,678 users match                                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Condition Operators</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Operator</th>
            <th class=""px-4 py-2 text-left"">Use For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">equals</td><td class=""px-4 py-2"">Exact match (plan = 'pro')</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">does not equal</td><td class=""px-4 py-2"">Exclusion (status != 'churned')</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">contains</td><td class=""px-4 py-2"">Partial string match</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">is greater than</td><td class=""px-4 py-2"">Numeric threshold (ltv > 100)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">is in list</td><td class=""px-4 py-2"">Multiple values (country in UK, US, CA)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">is set / is not set</td><td class=""px-4 py-2"">Null checks</td></tr>
    </tbody>
</table>

<h3>AND vs OR Logic</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">AND = All conditions must be true</h4>
    <p class=""mt-1"">plan = 'premium' AND country = 'UK'</p>
    <p class=""text-sm text-gray-600 dark:text-gray-400"">Only users who are BOTH premium AND in UK</p>

    <h4 class=""font-semibold mt-4"">OR = Any condition can be true</h4>
    <p class=""mt-1"">plan = 'premium' OR plan = 'enterprise'</p>
    <p class=""text-sm text-gray-600 dark:text-gray-400"">Users who are premium OR enterprise (or both)</p>
</div>

<h3>Nested Conditions</h3>
<p>Combine AND and OR for complex logic:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
(plan = 'premium' OR plan = 'enterprise')
AND
(country = 'UK' OR country = 'US')
AND
is_active = true

→ Active premium/enterprise users in UK or US
</pre>

<h3>Example Conditional Cohorts</h3>

<h4>High-Value Segment</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
lifetime_value > 500
AND total_purchases > 3
AND last_purchase_date > 30 days ago
</pre>

<h4>At-Risk Customers</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
is_active = true
AND days_since_last_login > 14
AND (support_tickets > 2 OR satisfaction_score < 3)
</pre>

<h4>Target Audience</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
age >= 25 AND age <= 45
AND (interest contains 'technology' OR interest contains 'business')
AND marketing_opt_in = true
</pre>

<h3>Best Practices</h3>
<ul>
    <li><strong>Start simple</strong> - Begin with one or two conditions</li>
    <li><strong>Preview often</strong> - Check user counts as you add conditions</li>
    <li><strong>Document logic</strong> - Add description explaining the cohort</li>
    <li><strong>Test edge cases</strong> - Verify the cohort includes/excludes correctly</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cs-cohort-analysis-techniques",
                    ModuleId = "cohorts-segmentation",
                    Title = "Cohort Analysis Techniques",
                    Summary = "Apply cohort analysis for acquisition, behaviour, and retention insights.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Perform acquisition cohort analysis",
                        "Analyse behavioural patterns across cohorts",
                        "Compare cohort performance over time",
                        "Identify actionable insights from cohort data"
                    },
                    Content = @"
<h2>Cohort Analysis Techniques</h2>
<p>Now that you can create cohorts, let's explore how to <strong>analyse them effectively</strong> to uncover actionable insights.</p>

<h3>Acquisition Cohort Analysis</h3>
<p>Group users by when they first appeared (signed up, first purchase, etc.):</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Acquisition Cohort Retention                                    │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Sign-up Week │ Users │ Week 1 │ Week 2 │ Week 3 │ Week 4      │
│───────────────┼───────┼────────┼────────┼────────┼────────      │
│  Jan 1-7      │ 1,000 │  45%   │  32%   │  28%   │  25%        │
│  Jan 8-14     │ 1,200 │  48%   │  35%   │  30%   │  27%        │
│  Jan 15-21    │ 1,100 │  52%   │  40%   │  35%   │  31%        │ ↑
│  Jan 22-28    │ 1,300 │  55%   │  43%   │  37%   │  --         │
│                                                                  │
│  Insight: Retention is improving! Later cohorts perform better. │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">What Acquisition Analysis Reveals:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Are product improvements working? (Later cohorts should perform better)</li>
        <li>• Seasonal patterns (holiday sign-ups might behave differently)</li>
        <li>• Impact of marketing campaigns on user quality</li>
    </ul>
</div>

<h3>Behavioural Cohort Comparison</h3>
<p>Compare users based on actions they've taken:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Comparing Cohorts: Onboarded vs Not Onboarded                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Metric              │ Completed │ Did Not     │ Difference    │
│                      │ Onboarding│ Complete    │               │
│──────────────────────┼───────────┼─────────────┼───────────────│
│  30-Day Retention    │    62%    │    18%      │   +244%       │
│  Avg Sessions/Week   │    4.2    │    1.1      │   +282%       │
│  Conversion Rate     │    8.5%   │    1.2%     │   +608%       │
│  Avg Revenue/User    │   $45     │    $8       │   +463%       │
│                                                                  │
│  Insight: Onboarding completion is a massive predictor of       │
│           success. Invest in improving onboarding!              │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Segment Performance Analysis</h3>
<p>Compare business metrics across different cohorts:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cohort</th>
            <th class=""px-4 py-2 text-left"">Conversion</th>
            <th class=""px-4 py-2 text-left"">AOV</th>
            <th class=""px-4 py-2 text-left"">LTV</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Organic Search</td><td class=""px-4 py-2"">4.2%</td><td class=""px-4 py-2"">$85</td><td class=""px-4 py-2"">$320</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Paid Social</td><td class=""px-4 py-2"">2.8%</td><td class=""px-4 py-2"">$65</td><td class=""px-4 py-2"">$180</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email Campaign</td><td class=""px-4 py-2"">8.5%</td><td class=""px-4 py-2"">$120</td><td class=""px-4 py-2"">$450</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Referral</td><td class=""px-4 py-2"">6.2%</td><td class=""px-4 py-2"">$95</td><td class=""px-4 py-2"">$380</td></tr>
    </tbody>
</table>

<h3>Finding Actionable Insights</h3>

<h4>Pattern: High-Value Behaviour</h4>
<p>Identify what successful users do differently:</p>
<ul>
    <li>What features do retained users engage with?</li>
    <li>How quickly do they reach key milestones?</li>
    <li>What's their session frequency?</li>
</ul>

<h4>Pattern: Churn Indicators</h4>
<p>Find warning signs before users leave:</p>
<ul>
    <li>Declining session frequency</li>
    <li>Reduced feature usage</li>
    <li>Support ticket increases</li>
</ul>

<h4>Pattern: Conversion Drivers</h4>
<p>Understand what leads to conversion:</p>
<ul>
    <li>Which pages do converters visit?</li>
    <li>What content do they engage with?</li>
    <li>How many touches before purchase?</li>
</ul>

<h3>Cohort Analysis Workflow</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Define the question</strong> - What do you want to learn?</li>
        <li><strong>Create relevant cohorts</strong> - Groups that matter for your question</li>
        <li><strong>Choose metrics</strong> - What measures will reveal the answer?</li>
        <li><strong>Compare cohorts</strong> - Look for meaningful differences</li>
        <li><strong>Identify patterns</strong> - What's consistent across the data?</li>
        <li><strong>Take action</strong> - What should you do based on findings?</li>
    </ol>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 7: Dashboards & Visualization

    private LearningModule BuildDashboardsVisualizationModule()
    {
        return new LearningModule
        {
            Id = "dashboards-visualization",
            Title = "Dashboards & Visualization",
            Description = "Build interactive dashboards, create visualisations, and share insights across your organisation.",
            Icon = "presentation-chart-bar",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "dv-creating-dashboards",
                    ModuleId = "dashboards-visualization",
                    Title = "Creating Dashboards",
                    Summary = "Build interactive dashboards with tiles organised in a grid view.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create a new dashboard",
                        "Add tiles from explorations",
                        "Arrange and resize tiles",
                        "Configure dashboard settings"
                    },
                    Content = @"
<h2>Creating Dashboards</h2>
<p>Dashboards in Optimizely Analytics are collections of visualisations organised in a <strong>grid view</strong>. They're used to monitor key metrics, share insights, and enable data-driven decision making.</p>

<h3>When to Use Dashboards</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Operational Monitoring</strong> - Track KPIs at a glance</li>
        <li>• <strong>Team Reporting</strong> - Share metrics with stakeholders</li>
        <li>• <strong>Executive Summaries</strong> - High-level business views</li>
        <li>• <strong>Analysis Workspaces</strong> - Related explorations together</li>
    </ul>
</div>

<h3>Creating a New Dashboard</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Dashboards</strong> in the main navigation</li>
    <li>Click <strong>+ New Dashboard</strong></li>
    <li>Give your dashboard a name</li>
    <li>Optionally add a description and category</li>
    <li>Click <strong>Create</strong></li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  New Dashboard                                                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Name:        [Weekly Marketing Metrics           ]             │
│                                                                  │
│  Description: [Key performance indicators for the               │
│                marketing team                      ]            │
│                                                                  │
│  Category:    [Marketing                        ▼]              │
│                                                                  │
│  ┌──────────────────┐  ┌──────────────────┐                     │
│  │     Cancel       │  │      Create      │                     │
│  └──────────────────┘  └──────────────────┘                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Adding Tiles</h3>
<p>Tiles are the building blocks of dashboards. Add them in two ways:</p>

<h4>Option 1: From an Exploration</h4>
<ol class=""list-decimal list-inside space-y-1"">
    <li>Create or open an exploration</li>
    <li>Click <strong>Add to Dashboard</strong></li>
    <li>Select the target dashboard</li>
    <li>The visualisation becomes a tile</li>
</ol>

<h4>Option 2: From Dashboard Edit Mode</h4>
<ol class=""list-decimal list-inside space-y-1"">
    <li>Open the dashboard</li>
    <li>Click <strong>Edit</strong></li>
    <li>Click <strong>+ Add Tile</strong></li>
    <li>Choose from saved explorations</li>
</ol>

<h3>Dashboard Grid</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Weekly Marketing Metrics                           [Edit] [⋮]  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌─────────────────────┐  ┌─────────────────────┐               │
│  │                     │  │                     │               │
│  │   Website Traffic   │  │  Conversion Rate    │               │
│  │      125,432        │  │       4.2%          │               │
│  │     ↑ 12%           │  │      ↑ 0.3%         │               │
│  │                     │  │                     │               │
│  └─────────────────────┘  └─────────────────────┘               │
│                                                                  │
│  ┌───────────────────────────────────────────────┐              │
│  │                                               │              │
│  │          Traffic by Channel (Line Chart)     │              │
│  │                                               │              │
│  │   📈                                          │              │
│  │                                               │              │
│  └───────────────────────────────────────────────┘              │
│                                                                  │
│  ┌───────────────────┐  ┌───────────────────────┐               │
│  │  Top Landing      │  │   Campaign            │               │
│  │  Pages (Table)    │  │   Performance         │               │
│  │                   │  │                       │               │
│  └───────────────────┘  └───────────────────────┘               │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Arranging Tiles</h3>
<p>In edit mode, you can:</p>
<ul>
    <li><strong>Drag</strong> - Move tiles to different positions</li>
    <li><strong>Resize</strong> - Drag tile edges to make larger/smaller</li>
    <li><strong>Delete</strong> - Remove tiles from dashboard</li>
    <li><strong>Reorder</strong> - Change the visual hierarchy</li>
</ul>

<h3>Tile Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Metric Card</td><td class=""px-4 py-2"">Single KPI with change indicator</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Line Chart</td><td class=""px-4 py-2"">Trends over time</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Bar Chart</td><td class=""px-4 py-2"">Comparisons across categories</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Table</td><td class=""px-4 py-2"">Detailed data with multiple columns</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Pie/Donut</td><td class=""px-4 py-2"">Part-of-whole relationships</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Funnel</td><td class=""px-4 py-2"">Conversion flow visualisation</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dv-visualization-options",
                    ModuleId = "dashboards-visualization",
                    Title = "Visualisation Options",
                    Summary = "Choose the right chart types and configure visual properties.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Choose appropriate chart types for data",
                        "Configure chart colours and labels",
                        "Customise axes and legends",
                        "Create clear, readable visualisations"
                    },
                    Content = @"
<h2>Visualisation Options</h2>
<p>Choosing the right visualisation makes your data <strong>easier to understand</strong> and more actionable. Let's explore the options available.</p>

<h3>Choosing the Right Chart Type</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Question</th>
            <th class=""px-4 py-2 text-left"">Best Chart</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">How does X change over time?</td><td class=""px-4 py-2"">Line chart</td></tr>
        <tr><td class=""px-4 py-2"">How do categories compare?</td><td class=""px-4 py-2"">Bar chart</td></tr>
        <tr><td class=""px-4 py-2"">What's the part of a whole?</td><td class=""px-4 py-2"">Pie or donut chart</td></tr>
        <tr><td class=""px-4 py-2"">How do two variables relate?</td><td class=""px-4 py-2"">Scatter plot</td></tr>
        <tr><td class=""px-4 py-2"">What's the single KPI?</td><td class=""px-4 py-2"">Metric card</td></tr>
        <tr><td class=""px-4 py-2"">What's the detailed breakdown?</td><td class=""px-4 py-2"">Table</td></tr>
        <tr><td class=""px-4 py-2"">How do users progress?</td><td class=""px-4 py-2"">Funnel chart</td></tr>
    </tbody>
</table>

<h3>Line Charts</h3>
<p>Best for showing trends over time:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Daily Active Users                                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  10k │                           ╭─────╮                        │
│      │                      ╭────╯     ╰────╮                   │
│   8k │                 ╭────╯               ╰────╮              │
│      │            ╭────╯                         ╰──            │
│   6k │       ╭────╯                                             │
│      │  ╭────╯                                                  │
│   4k │──╯                                                       │
│      └──────────────────────────────────────────────────────    │
│       Mon  Tue  Wed  Thu  Fri  Sat  Sun                        │
│                                                                  │
│  Options: Area fill, Multiple series, Smoothing                 │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Bar Charts</h3>
<p>Best for comparing categories:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Revenue by Channel                                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Organic   ████████████████████████████  $45,000               │
│  Paid      ██████████████████           $32,000                │
│  Email     █████████████                $24,000                │
│  Social    ████████                     $15,000                │
│  Referral  ████                         $8,000                 │
│                                                                  │
│  Options: Horizontal/Vertical, Stacked, Grouped                 │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Customising Visualisations</h3>

<h4>Colours</h4>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-1"">
        <li>• <strong>Series Colours</strong> - Assign specific colours to data series</li>
        <li>• <strong>Colour Palettes</strong> - Use pre-defined or custom palettes</li>
        <li>• <strong>Conditional Colours</strong> - Change colour based on value (red for negative)</li>
    </ul>
</div>

<h4>Labels and Legends</h4>
<ul>
    <li><strong>Data Labels</strong> - Show values on chart elements</li>
    <li><strong>Legend Position</strong> - Top, bottom, left, right, or hidden</li>
    <li><strong>Axis Labels</strong> - Clear labels for X and Y axes</li>
</ul>

<h4>Axes Configuration</h4>
<ul>
    <li><strong>Scale</strong> - Linear or logarithmic</li>
    <li><strong>Range</strong> - Auto or custom min/max</li>
    <li><strong>Formatting</strong> - Number format, currency, percentage</li>
</ul>

<h3>Best Practices</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Do</th>
            <th class=""px-4 py-2 text-left"">Don't</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Use consistent colours for series</td><td class=""px-4 py-2"">Use too many colours</td></tr>
        <tr><td class=""px-4 py-2"">Start Y-axis at zero for bars</td><td class=""px-4 py-2"">Truncate axes to exaggerate</td></tr>
        <tr><td class=""px-4 py-2"">Add clear titles</td><td class=""px-4 py-2"">Leave charts untitled</td></tr>
        <tr><td class=""px-4 py-2"">Use line charts for trends</td><td class=""px-4 py-2"">Use pie charts for trends</td></tr>
        <tr><td class=""px-4 py-2"">Keep legends readable</td><td class=""px-4 py-2"">Hide important context</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dv-sharing-collaboration",
                    ModuleId = "dashboards-visualization",
                    Title = "Sharing & Collaboration",
                    Summary = "Share dashboards with team members and configure permissions.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Share dashboards with individuals and teams",
                        "Configure view and edit permissions",
                        "Use comments for collaboration",
                        "Share filtered views via URLs"
                    },
                    Content = @"
<h2>Sharing & Collaboration</h2>
<p>Dashboards are most valuable when shared. Optimizely Analytics provides multiple ways to <strong>collaborate and distribute insights</strong>.</p>

<h3>Sharing Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Share Link</td><td class=""px-4 py-2"">Quick sharing with specific people</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Permission Groups</td><td class=""px-4 py-2"">Team-wide access management</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Public Link</td><td class=""px-4 py-2"">View-only access for anyone with link</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Scheduled Email</td><td class=""px-4 py-2"">Regular delivery to inboxes</td></tr>
    </tbody>
</table>

<h3>Permission Levels</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Permission Levels                                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  VIEW        Can see dashboard and interact with filters        │
│              Cannot edit tiles, settings, or sharing            │
│                                                                  │
│  EDIT        Can modify tiles, add new tiles, change layout     │
│              Cannot change sharing permissions                   │
│                                                                  │
│  ADMIN       Full control including sharing and deletion        │
│              Can grant permissions to others                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Sharing a Dashboard</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Open the dashboard</li>
    <li>Click the <strong>Share</strong> button</li>
    <li>Add people or groups</li>
    <li>Select permission level</li>
    <li>Click <strong>Share</strong></li>
</ol>

<h3>URL Parameters & Filters</h3>
<p>Share dashboards with filters pre-applied:</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Filter Persistence in URLs</p>
    <p class=""mt-2"">When you apply filters or parameters to a dashboard, the URL updates to include those settings. Copy and share this URL, and recipients will see the same filtered view.</p>
    <pre class=""bg-gray-900 text-green-400 p-3 rounded mt-2 text-sm"">
/dashboard/abc123?date_range=last_30_days&region=EMEA</pre>
</div>

<h3>Comments & Collaboration</h3>
<p>Add comments to discuss insights:</p>
<ul>
    <li>Click on any tile to add a comment</li>
    <li>@mention team members</li>
    <li>View comment history</li>
    <li>Resolve discussions when addressed</li>
</ul>

<h3>Categories & Organisation</h3>
<p>Keep dashboards organised for discovery:</p>
<ul>
    <li><strong>Categories</strong> - Group by team, project, or function</li>
    <li><strong>Favourites</strong> - Star frequently used dashboards</li>
    <li><strong>Naming</strong> - Use clear, consistent naming conventions</li>
</ul>

<h3>Best Practices for Sharing</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Why</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Use groups over individuals</td><td class=""px-4 py-2"">Easier to manage as teams change</td></tr>
        <tr><td class=""px-4 py-2"">Document dashboard purpose</td><td class=""px-4 py-2"">Helps others understand context</td></tr>
        <tr><td class=""px-4 py-2"">Limit edit access</td><td class=""px-4 py-2"">Prevents accidental changes</td></tr>
        <tr><td class=""px-4 py-2"">Review permissions regularly</td><td class=""px-4 py-2"">Remove access when not needed</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dv-exporting-reports",
                    ModuleId = "dashboards-visualization",
                    Title = "Exporting Reports",
                    Summary = "Export dashboards and data in various formats for external use.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Export dashboards as PDF",
                        "Download data as CSV",
                        "Schedule automated exports",
                        "Understand export limitations"
                    },
                    Content = @"
<h2>Exporting Reports</h2>
<p>Sometimes you need to share insights <strong>outside of Optimizely Analytics</strong>—in presentations, spreadsheets, or reports. Here's how to export your data.</p>

<h3>Export Formats</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Format</th>
            <th class=""px-4 py-2 text-left"">Contents</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">PDF</td><td class=""px-4 py-2"">Entire dashboard as document</td><td class=""px-4 py-2"">Presentations, sharing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">CSV</td><td class=""px-4 py-2"">Raw data per tile</td><td class=""px-4 py-2"">Further analysis in Excel</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">PNG/Image</td><td class=""px-4 py-2"">Individual chart as image</td><td class=""px-4 py-2"">Embedding in documents</td></tr>
    </tbody>
</table>

<h3>Exporting a Dashboard</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Open the dashboard</li>
    <li>Click the <strong>Export</strong> button (or ⋮ menu)</li>
    <li>Select format (PDF or CSV)</li>
    <li>For CSV, a file is generated for each tile</li>
    <li>Download begins automatically</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Export Dashboard                                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Format:                                                         │
│    (●) PDF - Single file with all visualisations                │
│    ( ) CSV - Separate file for each tile's data                 │
│                                                                  │
│  PDF Options:                                                    │
│    [ ] Include title page                                       │
│    [✓] Include timestamps                                       │
│    [ ] Landscape orientation                                    │
│                                                                  │
│  ┌──────────────────┐  ┌──────────────────┐                     │
│  │     Cancel       │  │     Export       │                     │
│  └──────────────────┘  └──────────────────┘                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Exporting Individual Tiles</h3>
<p>Export a single tile's data or visualisation:</p>
<ol class=""list-decimal list-inside space-y-1"">
    <li>Hover over the tile</li>
    <li>Click the ⋮ menu</li>
    <li>Select <strong>Export Data (CSV)</strong> or <strong>Download Image</strong></li>
</ol>

<h3>Scheduled Exports</h3>
<p>Set up automatic delivery:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Schedule Configuration:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Frequency</strong> - Daily, Weekly, Monthly</li>
        <li>• <strong>Time</strong> - When to generate and send</li>
        <li>• <strong>Recipients</strong> - Email addresses to send to</li>
        <li>• <strong>Format</strong> - PDF or CSV attachment</li>
    </ul>
</div>

<h3>Export Limitations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Limitation</th>
            <th class=""px-4 py-2 text-left"">Workaround</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">PDF may not capture interactive elements</td><td class=""px-4 py-2"">Share live link instead</td></tr>
        <tr><td class=""px-4 py-2"">Large datasets may be truncated in CSV</td><td class=""px-4 py-2"">Filter to smaller date ranges</td></tr>
        <tr><td class=""px-4 py-2"">Formatting may differ from live view</td><td class=""px-4 py-2"">Preview before sharing</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "dv-ai-summaries-opal",
                    ModuleId = "dashboards-visualization",
                    Title = "AI Summaries with Opal",
                    Summary = "Use Opal AI to generate automated insights from your dashboards and explorations.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Generate AI summaries from explorations",
                        "Interpret Opal's insights",
                        "Use AI for dashboard commentary",
                        "Understand AI capabilities and limitations"
                    },
                    Content = @"
<h2>AI Summaries with Opal</h2>
<p>Opal is Optimizely's AI assistant that can <strong>automatically analyse and summarise</strong> your data, turning complex visualisations into actionable insights.</p>

<h3>What Opal Can Do</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Summarise Results</strong> - Plain-language summary of exploration findings</li>
        <li>• <strong>Identify Patterns</strong> - Highlight trends and anomalies</li>
        <li>• <strong>Suggest Next Steps</strong> - Recommend follow-up analyses</li>
        <li>• <strong>Explain Changes</strong> - Describe what drove metric movements</li>
    </ul>
</div>

<h3>Generating a Summary</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Open an exploration or dashboard tile</li>
    <li>Click the <strong>Summarise with Opal</strong> button (✨ icon)</li>
    <li>Opal analyses the data and generates insights</li>
    <li>Review the summary</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Opal AI Summary                                                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  📊 Exploration: Weekly Conversion Rate                         │
│                                                                  │
│  ── Summary ──────────────────────────────────────────────────  │
│                                                                  │
│  ""Your conversion rate increased 15% this week compared to      │
│  last week (from 3.2% to 3.7%). The primary driver was a 23%    │
│  improvement in mobile conversions, which rose from 2.1% to     │
│  2.6%.                                                           │
│                                                                  │
│  Desktop conversions remained relatively flat at 4.8%.          │
│                                                                  │
│  The improvement correlates with the checkout flow update       │
│  deployed on Tuesday, which streamlined the mobile experience."" │
│                                                                  │
│  ── Recommendations ──────────────────────────────────────────  │
│                                                                  │
│  • Consider applying similar UX improvements to desktop         │
│  • Monitor tablet conversions, which show a slight decline      │
│  • Set up an alert for conversion rate drops below 3%           │
│                                                                  │
│  ┌──────────────────┐  ┌──────────────────┐                     │
│  │  Copy Summary    │  │   Ask Follow-up  │                     │
│  └──────────────────┘  └──────────────────┘                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Using Opal Effectively</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Scenario</th>
            <th class=""px-4 py-2 text-left"">How Opal Helps</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Sharing with executives</td><td class=""px-4 py-2"">Generate concise, jargon-free summaries</td></tr>
        <tr><td class=""px-4 py-2"">Understanding anomalies</td><td class=""px-4 py-2"">AI identifies potential causes</td></tr>
        <tr><td class=""px-4 py-2"">Experiment results</td><td class=""px-4 py-2"">Plain-language A/B test interpretation</td></tr>
        <tr><td class=""px-4 py-2"">Regular reporting</td><td class=""px-4 py-2"">Auto-generate commentary for dashboards</td></tr>
    </tbody>
</table>

<h3>Asking Follow-up Questions</h3>
<p>You can ask Opal for more details:</p>
<ul>
    <li>""Why did mobile conversions improve?""</li>
    <li>""What segments drove the change?""</li>
    <li>""How does this compare to last month?""</li>
    <li>""What should we monitor going forward?""</li>
</ul>

<h3>Limitations to Understand</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Opal's AI summaries are:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Based on the data shown, not external knowledge</li>
        <li>• Suggestions, not definitive conclusions</li>
        <li>• Best used as starting points for investigation</li>
        <li>• Subject to the same data quality limitations</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 8: NetScript & SQL

    private LearningModule BuildNetScriptSqlModule()
    {
        return new LearningModule
        {
            Id = "netscript-sql",
            Title = "NetScript & SQL",
            Description = "Master NetScript, the powerful analytical language, and leverage full SQL support for advanced analysis.",
            Icon = "code-bracket",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ns-introduction-netscript",
                    ModuleId = "netscript-sql",
                    Title = "Introduction to NetScript",
                    Summary = "Understand NetScript, the powerful analytical programming language.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand what NetScript is and why it exists",
                        "Know when to use NetScript vs templates",
                        "Learn the basic syntax and concepts",
                        "Access the NetScript editor"
                    },
                    Content = @"
<h2>Introduction to NetScript</h2>
<p>NetScript is a <strong>powerful analytical programming language</strong> unique to Optimizely Analytics. It allows you to express complex analytical computations that go beyond what templates can offer.</p>

<h3>What is NetScript?</h3>
<p>NetScript is designed specifically for analytics. Unlike general-purpose languages, it understands analytical concepts natively:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Key Insight:</p>
    <p class=""mt-2"">NetScript programs don't manipulate data directly—they <strong>manipulate SQL queries</strong>. NetScript understands the semantics of your data and generates optimised SQL automatically.</p>
</div>

<h3>Why NetScript?</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Approach</th>
            <th class=""px-4 py-2 text-left"">Pros</th>
            <th class=""px-4 py-2 text-left"">Cons</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Templates</td><td class=""px-4 py-2"">Easy, no code required</td><td class=""px-4 py-2"">Limited to predefined patterns</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Raw SQL</td><td class=""px-4 py-2"">Maximum flexibility</td><td class=""px-4 py-2"">Complex for analytics, verbose</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">NetScript</td><td class=""px-4 py-2"">Concise analytics syntax</td><td class=""px-4 py-2"">Learning curve</td></tr>
    </tbody>
</table>

<h3>When to Use NetScript</h3>
<ul>
    <li>Complex calculations not available in templates</li>
    <li>Custom aggregations and transformations</li>
    <li>Multi-step analytical workflows</li>
    <li>Reusable analytical components</li>
    <li>When you need more control than templates provide</li>
</ul>

<h3>Accessing NetScript</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Explore</strong></li>
    <li>Click <strong>+ New Exploration</strong></li>
    <li>Select <strong>NetScript Explore</strong></li>
    <li>The NetScript editor opens</li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  NetScript Editor                                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  // Your first NetScript                                        │
│  events                                                          │
│    |> filter(event_type == ""page_viewed"")                       │
│    |> group(by: date)                                           │
│    |> aggregate(count())                                        │
│                                                                  │
├─────────────────────────────────────────────────────────────────┤
│  [Run]  [Format]  [Inspect SQL]  [Save]                         │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Basic Syntax</h3>
<p>NetScript uses a pipe-based syntax similar to functional programming:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Start with a data source
events
  |> filter(condition)        // Filter rows
  |> group(by: dimension)     // Group by columns
  |> aggregate(measure)       // Calculate aggregates</code></pre>

<h3>Core Concepts</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Concept</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Quad</td><td class=""px-4 py-2"">A query expression that represents SQL</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">|></td><td class=""px-4 py-2"">Pipe operator - passes result to next function</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">filter</td><td class=""px-4 py-2"">Filter rows based on condition</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">group</td><td class=""px-4 py-2"">Group by one or more columns</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">aggregate</td><td class=""px-4 py-2"">Calculate sum, count, avg, etc.</td></tr>
    </tbody>
</table>

<h3>NetScript vs Templates</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Remember:</p>
    <p class=""mt-2"">Templates are built on NetScript. When you use a template, it generates NetScript behind the scenes. Learning NetScript means understanding what templates do and being able to go beyond them.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ns-understanding-quads",
                    ModuleId = "netscript-sql",
                    Title = "Understanding Quads",
                    Summary = "Learn about Quads, the core concept of NetScript query expressions.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Quads are",
                        "Learn how Quads represent SQL queries",
                        "Chain operations on Quads",
                        "Inspect the generated SQL"
                    },
                    Content = @"
<h2>Understanding Quads</h2>
<p>The central concept in NetScript is the <strong>Quad</strong>—a query expression that represents a SQL query. Understanding Quads is key to mastering NetScript.</p>

<h3>What is a Quad?</h3>
<p>A Quad is not the data itself—it's a <strong>representation of a query</strong> that will produce data when executed.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// This is a Quad - it represents a query, not data
events
  |> filter(event_type == ""purchase"")

// When executed, the Quad generates SQL:
// SELECT * FROM events WHERE event_type = 'purchase'
</pre>

<h3>Quad Operations</h3>
<p>Operations on Quads transform the underlying query:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Each operation transforms the Quad
events                              // Quad 1: SELECT * FROM events
  |> filter(amount > 100)           // Quad 2: ... WHERE amount > 100
  |> group(by: country)             // Quad 3: ... GROUP BY country
  |> aggregate(sum(amount))         // Quad 4: ... SELECT SUM(amount)
</pre>

<h3>How NetScript Generates SQL</h3>
<p>The NetScript compiler takes your Quad expression and uses the schema to generate optimised SQL:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  NetScript → SQL Compilation                                     │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  NetScript:                                                      │
│  ──────────                                                      │
│  events                                                          │
│    |> filter(event_type == ""purchase"" && amount > 50)           │
│    |> group(by: date(timestamp))                                │
│    |> aggregate(                                                │
│         total_revenue = sum(amount),                            │
│         order_count = count()                                   │
│       )                                                          │
│                                                                  │
│  Generated SQL:                                                  │
│  ──────────────                                                  │
│  SELECT                                                          │
│    DATE(timestamp) as date,                                     │
│    SUM(amount) as total_revenue,                                │
│    COUNT(*) as order_count                                      │
│  FROM events                                                     │
│  WHERE event_type = 'purchase' AND amount > 50                  │
│  GROUP BY DATE(timestamp)                                       │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Inspecting Generated SQL</h3>
<p>Always check what SQL your NetScript generates:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Inspect Query</strong> in the editor</li>
    <li>View the <strong>SQL</strong> tab to see generated SQL</li>
    <li>View the <strong>NetScript</strong> tab for the intermediate form</li>
    <li>View <strong>Warehouse SQL</strong> for warehouse-specific syntax</li>
</ol>

<h3>Semantic Understanding</h3>
<p>NetScript understands your data semantics:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">When you filter on actor properties...</p>
    <pre class=""bg-gray-900 text-green-400 p-3 rounded mt-2 text-sm""><code>events |> filter(user.plan == ""premium"")</code></pre>
    <p class=""mt-3"">NetScript automatically generates the proper JOIN:</p>
    <pre class=""bg-gray-900 text-green-400 p-3 rounded mt-2 text-sm""><code>SELECT * FROM events e
JOIN users u ON e.user_id = u.id
WHERE u.plan = 'premium'</code></pre>
</div>

<h3>Chaining Operations</h3>
<p>Build complex analyses by chaining operations:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>events
  |> filter(event_type == ""purchase"")     // 1. Filter to purchases
  |> filter(timestamp > date(""2024-01-01"")) // 2. This year only
  |> group(by: user_id)                    // 3. Per user
  |> aggregate(total = sum(amount))        // 4. Total per user
  |> filter(total > 1000)                  // 5. High spenders only
  |> sort(by: total, desc: true)           // 6. Highest first
  |> limit(100)                            // 7. Top 100</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ns-sql-support",
                    ModuleId = "netscript-sql",
                    Title = "SQL Support in Analytics",
                    Summary = "Use direct SQL queries when you need maximum flexibility.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Write SQL queries in Analytics",
                        "Know when SQL is better than NetScript",
                        "Understand SQL integration with Analytics features",
                        "Convert between SQL and NetScript"
                    },
                    Content = @"
<h2>SQL Support in Analytics</h2>
<p>While NetScript is powerful, sometimes you need the <strong>full flexibility of SQL</strong>. Optimizely Analytics supports direct SQL queries against your warehouse.</p>

<h3>When to Use SQL</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Complex joins</strong> not easily expressed in NetScript</li>
        <li>• <strong>Warehouse-specific functions</strong> (BigQuery, Snowflake syntax)</li>
        <li>• <strong>Existing queries</strong> you want to reuse</li>
        <li>• <strong>Performance tuning</strong> with specific optimisations</li>
        <li>• <strong>Familiarity</strong> - if you know SQL well</li>
    </ul>
</div>

<h3>Creating a SQL Exploration</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Explore</strong></li>
    <li>Click <strong>+ New Exploration</strong></li>
    <li>Select <strong>SQL Explore</strong></li>
    <li>Write your SQL query</li>
    <li>Click <strong>Run</strong></li>
</ol>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  SQL Exploration                                                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  SELECT                                                          │
│    DATE(event_timestamp) as event_date,                         │
│    COUNT(DISTINCT user_id) as unique_users,                     │
│    COUNT(*) as total_events,                                    │
│    SUM(revenue) as total_revenue                                │
│  FROM analytics.events                                          │
│  WHERE event_type = 'purchase'                                  │
│    AND event_timestamp >= DATE_SUB(CURRENT_DATE(), INTERVAL 30 DAY)
│  GROUP BY 1                                                      │
│  ORDER BY 1                                                      │
│                                                                  │
├─────────────────────────────────────────────────────────────────┤
│  [Run]  [Format]  [Save]  [Add to Dashboard]                    │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>SQL Best Practices</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Why</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Always filter by date</td><td class=""px-4 py-2"">Prevents full table scans</td></tr>
        <tr><td class=""px-4 py-2"">Use qualified table names</td><td class=""px-4 py-2"">Avoids ambiguity</td></tr>
        <tr><td class=""px-4 py-2"">Limit result rows</td><td class=""px-4 py-2"">Better performance in UI</td></tr>
        <tr><td class=""px-4 py-2"">Use column aliases</td><td class=""px-4 py-2"">Clearer visualisations</td></tr>
    </tbody>
</table>

<h3>Converting Between SQL and NetScript</h3>
<p>You can often convert between the two:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
-- SQL Version
SELECT
  event_type,
  COUNT(DISTINCT user_id) as users
FROM events
WHERE timestamp >= '2024-01-01'
GROUP BY event_type

-- NetScript Equivalent
events
  |> filter(timestamp >= date(""2024-01-01""))
  |> group(by: event_type)
  |> aggregate(users = count_distinct(user_id))
</pre>

<h3>Integration with Analytics Features</h3>
<p>SQL explorations integrate with other Analytics features:</p>
<ul>
    <li>Add SQL results to dashboards</li>
    <li>Save and share SQL explorations</li>
    <li>Use Opal to summarise SQL results</li>
    <li>Export SQL results to CSV/PDF</li>
</ul>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Note:</p>
    <p class=""mt-2"">SQL explorations don't have access to the semantic layer (Actors/Events), so features like automatic funnel analysis aren't available. Use templates or NetScript when you need semantic features.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ns-advanced-query-techniques",
                    ModuleId = "netscript-sql",
                    Title = "Advanced Query Techniques",
                    Summary = "Master filters, aggregations, joins, and window functions.",
                    Order = 4,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Use advanced filter conditions",
                        "Apply multiple aggregations",
                        "Join datasets effectively",
                        "Use window functions for advanced analysis"
                    },
                    Content = @"
<h2>Advanced Query Techniques</h2>
<p>Now that you understand the basics, let's explore <strong>advanced techniques</strong> for complex analytical queries.</p>

<h3>Advanced Filtering</h3>
<p>Combine multiple conditions with boolean logic:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Multiple conditions
events
  |> filter(
       event_type == ""purchase""
       && amount > 100
       && (country == ""US"" || country == ""UK"")
       && timestamp >= date(""2024-01-01"")
     )

// Using IN for multiple values
events
  |> filter(event_type in [""purchase"", ""refund"", ""exchange""])

// Pattern matching
events
  |> filter(page_url like ""%/products/%"")</code></pre>

<h3>Multiple Aggregations</h3>
<p>Calculate several metrics at once:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>events
  |> filter(event_type == ""purchase"")
  |> group(by: [date(timestamp), country])
  |> aggregate(
       total_revenue = sum(amount),
       order_count = count(),
       unique_customers = count_distinct(user_id),
       avg_order_value = avg(amount),
       max_order = max(amount)
     )</code></pre>

<h3>Joining Datasets</h3>
<p>Combine data from multiple sources:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Join events with user attributes
events
  |> join(users, on: user_id)
  |> filter(users.plan == ""enterprise"")
  |> group(by: event_type)
  |> aggregate(count())

// Join with products for enrichment
events
  |> filter(event_type == ""purchase"")
  |> join(products, on: product_id)
  |> group(by: products.category)
  |> aggregate(revenue = sum(amount))</code></pre>

<h3>Window Functions</h3>
<p>Perform calculations across related rows:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Running total
events
  |> filter(event_type == ""purchase"")
  |> group(by: date(timestamp))
  |> aggregate(daily_revenue = sum(amount))
  |> window(
       running_total = cumsum(daily_revenue),
       over: order_by(date)
     )

// Rank users by revenue
events
  |> filter(event_type == ""purchase"")
  |> group(by: user_id)
  |> aggregate(total = sum(amount))
  |> window(
       rank = rank(),
       over: order_by(total, desc: true)
     )</code></pre>

<h3>Subqueries and CTEs</h3>
<p>Break complex queries into steps:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Define intermediate results
let high_value_users = events
  |> filter(event_type == ""purchase"")
  |> group(by: user_id)
  |> aggregate(total = sum(amount))
  |> filter(total > 1000)

// Use in main query
events
  |> filter(user_id in high_value_users.user_id)
  |> group(by: event_type)
  |> aggregate(count())</code></pre>

<h3>Time-based Analysis</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Compare periods
let this_week = events
  |> filter(timestamp >= date_add(today(), -7))
  |> aggregate(count = count())

let last_week = events
  |> filter(
       timestamp >= date_add(today(), -14)
       && timestamp < date_add(today(), -7)
     )
  |> aggregate(count = count())

// Calculate change
select(
  this_week = this_week.count,
  last_week = last_week.count,
  change_pct = (this_week.count - last_week.count) / last_week.count * 100
)</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ns-custom-computations",
                    ModuleId = "netscript-sql",
                    Title = "Custom Computations",
                    Summary = "Build reusable functions and complex analytical computations.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create reusable NetScript functions",
                        "Build complex multi-step computations",
                        "Parameterise queries for flexibility",
                        "Optimise query performance"
                    },
                    Content = @"
<h2>Custom Computations</h2>
<p>NetScript allows you to build <strong>reusable, parameterised computations</strong> for complex analytical needs.</p>

<h3>Defining Functions</h3>
<p>Create reusable logic with functions:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Define a reusable function
fn revenue_by_period(start_date, end_date) =
  events
    |> filter(event_type == ""purchase"")
    |> filter(timestamp >= start_date && timestamp < end_date)
    |> aggregate(revenue = sum(amount))

// Use the function
let jan_revenue = revenue_by_period(date(""2024-01-01""), date(""2024-02-01""))
let feb_revenue = revenue_by_period(date(""2024-02-01""), date(""2024-03-01""))</code></pre>

<h3>Parameterised Queries</h3>
<p>Make queries flexible with parameters:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Parameters can be changed at runtime
@param event_filter: string = ""purchase""
@param days_back: int = 30
@param min_amount: float = 0

events
  |> filter(event_type == @event_filter)
  |> filter(timestamp >= date_add(today(), -@days_back))
  |> filter(amount >= @min_amount)
  |> group(by: date(timestamp))
  |> aggregate(total = sum(amount))</code></pre>

<h3>Complex Multi-Step Analysis</h3>
<p>Build sophisticated analyses step by step:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Step 1: Identify user first purchase dates
let first_purchases = events
  |> filter(event_type == ""purchase"")
  |> group(by: user_id)
  |> aggregate(first_purchase = min(timestamp))

// Step 2: Calculate days to first purchase
let users_with_signup = first_purchases
  |> join(users, on: user_id)
  |> select(
       user_id,
       days_to_purchase = datediff(first_purchase, users.created_at)
     )

// Step 3: Analyse distribution
users_with_signup
  |> group(by: case(
       days_to_purchase == 0 => ""Same day"",
       days_to_purchase <= 7 => ""First week"",
       days_to_purchase <= 30 => ""First month"",
       else => ""Later""
     ))
  |> aggregate(users = count())</code></pre>

<h3>Performance Optimisation</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Optimisation Tips:</h4>
    <ul class=""mt-2 space-y-2"">
        <li>• <strong>Filter early</strong> - Apply date filters before joins</li>
        <li>• <strong>Use materialisation</strong> - Cache intermediate results</li>
        <li>• <strong>Limit columns</strong> - Only select what you need</li>
        <li>• <strong>Check the SQL</strong> - Inspect generated query for issues</li>
    </ul>
</div>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Good: Filter before aggregation
events
  |> filter(timestamp >= date(""2024-01-01""))  // Filter first
  |> filter(event_type == ""purchase"")
  |> aggregate(sum(amount))

// Avoid: Aggregating then filtering (if possible)
events
  |> aggregate(sum(amount))  // Processes all data
  |> filter(...)             // Then filters</code></pre>

<h3>Saving and Reusing</h3>
<p>Save your NetScript explorations for reuse:</p>
<ul>
    <li>Save as exploration for ad-hoc analysis</li>
    <li>Add to dashboards for monitoring</li>
    <li>Create metrics from NetScript calculations</li>
    <li>Share with team members</li>
</ul>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Pro Tip:</p>
    <p class=""mt-2"">When building complex queries, start simple and add complexity incrementally. Run and verify at each step before adding more operations.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 9: Experimentation Analytics

    private LearningModule BuildExperimentationAnalyticsModule()
    {
        return new LearningModule
        {
            Id = "experimentation-analytics",
            Title = "Experimentation Analytics",
            Description = "Integrate with Optimizely Experimentation, analyse A/B tests, and leverage Stats Engine for trustworthy results.",
            Icon = "beaker",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ea-warehouse-native-experimentation",
                    ModuleId = "experimentation-analytics",
                    Title = "Warehouse-Native Experimentation",
                    Summary = "Integrate experiment data with your warehouse for unified analysis.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand warehouse-native experimentation",
                        "Configure experiment data integration",
                        "Connect Optimizely Experimentation with Analytics",
                        "Benefits of unified experiment analysis"
                    },
                    Content = @"
<h2>Warehouse-Native Experimentation</h2>
<p>Warehouse-Native Experimentation Analytics brings Optimizely Analytics capabilities to your A/B tests and experiments, enabling <strong>deeper analysis</strong> while keeping data in your warehouse.</p>

<h3>What is Warehouse-Native Experimentation?</h3>
<p>Instead of analysing experiments only in the Optimizely Experimentation UI, you can:</p>
<ul>
    <li>Send experiment decision data to your warehouse</li>
    <li>Analyse experiments alongside all your other data</li>
    <li>Use Analytics' exploration templates on experiments</li>
    <li>Apply Stats Engine to any experiment source</li>
</ul>

<h3>Integration Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Warehouse-Native Experimentation                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌──────────────────┐         ┌──────────────────┐             │
│  │   Optimizely     │         │   Your Data      │             │
│  │  Experimentation │         │   Warehouse      │             │
│  │                  │────────►│                  │             │
│  │  • Web Exp       │  Export │  • Decisions     │             │
│  │  • Feature Exp   │         │  • Conversions   │             │
│  └──────────────────┘         │  • Business Data │             │
│                               └────────┬─────────┘             │
│                                        │                        │
│                                        ▼                        │
│                               ┌──────────────────┐             │
│                               │   Optimizely     │             │
│                               │   Analytics      │             │
│                               │                  │             │
│                               │  • Stats Engine  │             │
│                               │  • Explorations  │             │
│                               │  • Dashboards    │             │
│                               └──────────────────┘             │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Setting Up Integration</h3>
<p>Export experiment data to your warehouse:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Export Options:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Decision Notification Listeners</strong> - Real-time export to any warehouse</li>
        <li>• <strong>BigQuery Direct Share</strong> - Native BigQuery integration</li>
        <li>• <strong>Snowflake Export</strong> - Direct Snowflake delivery</li>
        <li>• <strong>S3 Export</strong> - Export via S3 to any warehouse</li>
        <li>• <strong>Fivetran</strong> - ETL pipeline option</li>
    </ul>
</div>

<h3>Required Data Schema</h3>
<p>Your experiment data needs these fields:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Field</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">experiment_id</td><td class=""px-4 py-2"">Unique experiment identifier</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">variation_id</td><td class=""px-4 py-2"">Which variation the user saw</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">user_id</td><td class=""px-4 py-2"">The actor (matches your users table)</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">timestamp</td><td class=""px-4 py-2"">When the decision was made</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">is_holdback</td><td class=""px-4 py-2"">Whether user is in control group</td></tr>
    </tbody>
</table>

<h3>Configuring in Analytics</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data → Datasets</strong></li>
    <li>Add your experiment decisions table</li>
    <li>Configure the semantic fields (actor, experiment, variation)</li>
    <li>Link to your events dataset</li>
</ol>

<h3>Benefits of Warehouse-Native</h3>
<ul>
    <li><strong>Unified Analysis</strong> - Experiments with all your business data</li>
    <li><strong>Any Metric</strong> - Analyse experiments with warehouse metrics</li>
    <li><strong>Deep Segmentation</strong> - Segment by any user attribute</li>
    <li><strong>Data Security</strong> - Data stays in your warehouse</li>
    <li><strong>Historical Analysis</strong> - Query past experiments anytime</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ea-stats-engine-integration",
                    ModuleId = "experimentation-analytics",
                    Title = "Stats Engine Integration",
                    Summary = "Leverage Optimizely's powerful Stats Engine for trustworthy experiment results.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand Optimizely's Stats Engine",
                        "Know the different statistical methods available",
                        "Configure Stats Engine for your experiments",
                        "Interpret statistical results correctly"
                    },
                    Content = @"
<h2>Stats Engine Integration</h2>
<p>Optimizely's <strong>Stats Engine</strong> is a proprietary statistical framework designed for trustworthy, continuous experimentation. It's available in Analytics for warehouse-native experiments.</p>

<h3>What is Stats Engine?</h3>
<p>Stats Engine was developed in partnership with Stanford University to solve common problems with A/B testing:</p>
<ul>
    <li>Allows continuous monitoring without inflating false positives</li>
    <li>Provides intuitive probability-based results</li>
    <li>Controls for multiple comparisons (false discovery rate)</li>
    <li>Works with any sample size</li>
</ul>

<h3>Statistical Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Sequential</td><td class=""px-4 py-2"">Monitor results continuously</td><td class=""px-4 py-2"">Most experiments</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Bayesian</td><td class=""px-4 py-2"">Probability-focused analysis</td><td class=""px-4 py-2"">Decision-focused teams</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Fixed Horizon</td><td class=""px-4 py-2"">Classical hypothesis testing</td><td class=""px-4 py-2"">Regulated environments</td></tr>
    </tbody>
</table>

<h3>Sequential Analysis</h3>
<p>The default and most common approach:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Key Features:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Check results anytime without statistical penalty</li>
        <li>• Get valid conclusions even with early stopping</li>
        <li>• False positive rate controlled at 5%</li>
        <li>• Uses Stats Accelerator for faster results</li>
    </ul>
</div>

<h3>Bayesian Analysis</h3>
<p>Answers: ""What's the probability this variation is better?""</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Bayesian Results Example                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Variation B vs Control                                         │
│                                                                  │
│  Probability to be Best:     92%                                │
│  Expected Lift:              +8.5%                              │
│  Credible Interval:          [+3.2%, +13.8%]                    │
│                                                                  │
│  Interpretation: ""There's a 92% chance Variation B is better   │
│  than Control, with an expected improvement of 8.5%""            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Fixed Horizon</h3>
<p>Classical approach for regulated environments:</p>
<ul>
    <li>Pre-determined sample size required</li>
    <li>Results only valid at experiment end</li>
    <li>Traditional p-value and confidence intervals</li>
    <li>Suitable for auditable, documented testing</li>
</ul>

<h3>Configuring Stats Engine</h3>
<p>In Analytics experiment analysis:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Select your statistical method</li>
    <li>Set confidence level (typically 95%)</li>
    <li>Configure metrics and their improvement direction</li>
    <li>Set up guardrail metrics if needed</li>
</ol>

<h3>Experiment on Any Data</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Powerful Capability:</p>
    <p class=""mt-2"">You can use Stats Engine to analyse experiments from <strong>any source</strong>—not just Optimizely. As long as your warehouse has experiment ID, variation ID, user ID, and timestamp, Stats Engine can analyse it.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ea-ab-test-analysis",
                    ModuleId = "experimentation-analytics",
                    Title = "A/B Test Analysis",
                    Summary = "Analyse A/B test results with metrics, segments, and insights.",
                    Order = 3,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Analyse experiment results in Analytics",
                        "Understand lift, significance, and confidence",
                        "Segment experiment results for deeper insights",
                        "Make data-driven decisions from experiments"
                    },
                    Content = @"
<h2>A/B Test Analysis</h2>
<p>Once your experiment data is in Analytics, you can perform <strong>comprehensive analysis</strong> to understand what happened and why.</p>

<h3>Experiment Analysis View</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Experiment: Checkout Flow Redesign                              │
│  Status: Running (Day 14 of 21)                                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  PRIMARY METRIC: Conversion Rate                                │
│  ────────────────────────────────────────────────────────────── │
│                                                                  │
│  Variation      │ Visitors │ Conversions │ Rate   │ vs Control │
│  ───────────────┼──────────┼─────────────┼────────┼────────────│
│  Control        │  15,234  │     458     │ 3.01%  │    --      │
│  Variation A    │  15,189  │     512     │ 3.37%  │  +12.0% ✓  │
│  Variation B    │  15,201  │     489     │ 3.22%  │  +7.0%     │
│                                                                  │
│  ✓ = Statistically significant at 95% confidence               │
│                                                                  │
│  [View Details]  [Segment Results]  [Export]                    │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Key Metrics to Understand</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">What It Tells You</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Lift</td><td class=""px-4 py-2"">Percentage change vs control</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Confidence</td><td class=""px-4 py-2"">Likelihood result is real, not chance</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Confidence Interval</td><td class=""px-4 py-2"">Range where true effect likely lies</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sample Size</td><td class=""px-4 py-2"">Users in each variation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Statistical Power</td><td class=""px-4 py-2"">Ability to detect a real effect</td></tr>
    </tbody>
</table>

<h3>Segmenting Results</h3>
<p>Break down experiment results by segments to find insights:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Common Segments:</h4>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Device Type</strong> - Does the change work on mobile?</li>
        <li>• <strong>User Type</strong> - New vs returning users</li>
        <li>• <strong>Geography</strong> - Regional differences</li>
        <li>• <strong>Traffic Source</strong> - Organic vs paid</li>
        <li>• <strong>User Plan</strong> - Free vs premium</li>
    </ul>
</div>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Segmented Results: Variation A vs Control

Segment          │ Lift   │ Confidence │ Insight
─────────────────┼────────┼────────────┼──────────────────────────
Overall          │ +12.0% │    97%     │ Winner!
Desktop          │ +15.2% │    98%     │ Strong effect
Mobile           │ +6.1%  │    72%     │ Weaker, not significant
New Users        │ +18.5% │    95%     │ Especially helps new users
Returning Users  │ +4.2%  │    65%     │ Less impact on existing

Insight: The new checkout works best for new users on desktop.
Consider optimising further for mobile users.
</pre>

<h3>Multi-Metric Analysis</h3>
<p>Look beyond the primary metric:</p>
<ul>
    <li><strong>Primary Metric</strong> - What you're trying to improve</li>
    <li><strong>Secondary Metrics</strong> - Related metrics to monitor</li>
    <li><strong>Guardrail Metrics</strong> - Metrics that shouldn't get worse</li>
</ul>

<h3>Making Decisions</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Decision Framework:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ <strong>Ship</strong> - Significant positive lift, no guardrail violations</li>
        <li>✗ <strong>Don't ship</strong> - No significant lift or guardrail violations</li>
        <li>? <strong>Iterate</strong> - Mixed results suggest refinement needed</li>
        <li>↻ <strong>Extend</strong> - Promising but not yet significant</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ea-srm-health-checks",
                    ModuleId = "experimentation-analytics",
                    Title = "SRM Health Checks",
                    Summary = "Detect and diagnose Sample Ratio Mismatch issues in experiments.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Sample Ratio Mismatch (SRM) is",
                        "Know why SRM is dangerous for experiments",
                        "Identify common causes of SRM",
                        "Use Analytics' SRM detection"
                    },
                    Content = @"
<h2>SRM Health Checks</h2>
<p>Sample Ratio Mismatch (SRM) is a <strong>critical data quality issue</strong> that can invalidate experiment results. Optimizely Analytics automatically detects SRM to protect your experiments.</p>

<h3>What is SRM?</h3>
<p>SRM occurs when the actual distribution of users across variations doesn't match the expected distribution.</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Expected (50/50 split):
  Control:     50% of users
  Variation:   50% of users

Actual (SRM detected!):
  Control:     55% of users  ← 5% more than expected
  Variation:   45% of users  ← 5% less than expected

This difference is statistically unlikely to occur by chance,
indicating a problem with the experiment setup.
</pre>

<h3>Why SRM is Dangerous</h3>
<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium"">If you have SRM, your results may be invalid:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• The groups aren't comparable anymore</li>
        <li>• Winners might actually be losers</li>
        <li>• Statistical tests become unreliable</li>
        <li>• You could make costly wrong decisions</li>
    </ul>
</div>

<h3>Common Causes of SRM</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cause</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Bot Filtering</td><td class=""px-4 py-2"">Bots blocked differently per variation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Redirect Issues</td><td class=""px-4 py-2"">Redirect variation loses users</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Page Load Errors</td><td class=""px-4 py-2"">Variation code fails to load</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Logging Bugs</td><td class=""px-4 py-2"">Events not tracked correctly</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Audience Overlap</td><td class=""px-4 py-2"">Users qualifying for multiple experiments</td></tr>
    </tbody>
</table>

<h3>SRM Detection in Analytics</h3>
<p>Analytics automatically checks for SRM:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  ⚠️  Sample Ratio Mismatch Detected                             │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Expected Split: 50% / 50%                                      │
│  Actual Split:   55.2% / 44.8%                                  │
│                                                                  │
│  Chi-squared test p-value: 0.0001                               │
│  Status: SIGNIFICANT MISMATCH                                   │
│                                                                  │
│  ⚠️  Results may be unreliable. Investigate before deciding.    │
│                                                                  │
│  [Learn More]  [Investigate]  [Dismiss Warning]                 │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Investigating SRM</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Check the experiment implementation for bugs</li>
    <li>Look for logging discrepancies</li>
    <li>Review bot filtering settings</li>
    <li>Check for page load failures</li>
    <li>Verify redirect implementations</li>
</ol>

<h3>What to Do If You Have SRM</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Don't trust the results</strong> - They may be invalid</li>
        <li>• <strong>Investigate the cause</strong> - Find what's wrong</li>
        <li>• <strong>Fix the issue</strong> - Correct the implementation</li>
        <li>• <strong>Restart the experiment</strong> - With fresh data</li>
        <li>• <strong>Document learnings</strong> - Prevent future occurrences</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ea-cuped-advanced-statistics",
                    ModuleId = "experimentation-analytics",
                    Title = "CUPED & Advanced Statistics",
                    Summary = "Use variance reduction techniques for faster, more reliable experiment results.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand CUPED variance reduction",
                        "Know how CUPED speeds up experiments",
                        "Configure CUPED in Analytics",
                        "Understand outlier management"
                    },
                    Content = @"
<h2>CUPED & Advanced Statistics</h2>
<p>CUPED (Controlled-experiment Using Pre-Experiment Data) is a <strong>variance reduction technique</strong> that can make your experiments reach conclusions faster while maintaining statistical validity.</p>

<h3>What is CUPED?</h3>
<p>CUPED uses pre-experiment behaviour to reduce the noise in your experiment metrics:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">The Idea:</p>
    <p class=""mt-2"">Users who purchased a lot before the experiment will likely purchase a lot during the experiment (regardless of variation). CUPED removes this ""pre-existing difference"" to isolate the true experiment effect.</p>
</div>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  CUPED Variance Reduction                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Without CUPED:                                                  │
│  High variance in data → Need more samples → Longer experiment  │
│                                                                  │
│  With CUPED:                                                     │
│  Adjust for pre-experiment behaviour → Lower variance           │
│  → Need fewer samples → Faster results!                         │
│                                                                  │
│  Typical improvement: 20-40% faster to reach significance       │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>How CUPED Works</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Collect pre-experiment metric values for each user</li>
    <li>During analysis, adjust experiment metrics based on pre-experiment behaviour</li>
    <li>The adjustment removes variance unrelated to the experiment</li>
    <li>Smaller confidence intervals and faster conclusions</li>
</ol>

<h3>Benefits of CUPED</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Faster experiments</td><td class=""px-4 py-2"">20-40% reduction in required sample size</td></tr>
        <tr><td class=""px-4 py-2"">More sensitive</td><td class=""px-4 py-2"">Detect smaller effects reliably</td></tr>
        <tr><td class=""px-4 py-2"">No bias</td><td class=""px-4 py-2"">Still statistically valid</td></tr>
        <tr><td class=""px-4 py-2"">Automatic</td><td class=""px-4 py-2"">Analytics handles the math</td></tr>
    </tbody>
</table>

<h3>Configuring CUPED</h3>
<p>In Analytics experiment settings:</p>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Enable CUPED for your experiment</li>
    <li>Specify the pre-experiment period (typically 7-30 days)</li>
    <li>Select which metrics to apply CUPED to</li>
    <li>Analytics calculates adjusted results automatically</li>
</ol>

<h3>Outlier Management</h3>
<p>Extreme values can distort experiment results. Analytics provides outlier management:</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Outlier Capping:</p>
    <p class=""mt-2"">Values above a configurable threshold are capped to prevent single extreme observations from skewing results. For example, cap revenue at the 99th percentile.</p>
</div>

<h3>When to Use These Techniques</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Technique</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">CUPED</td><td class=""px-4 py-2"">Most experiments, especially revenue metrics</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Outlier Capping</td><td class=""px-4 py-2"">Revenue, order value, time metrics</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Both Combined</td><td class=""px-4 py-2"">Maximum variance reduction</td></tr>
    </tbody>
</table>

<h3>Best Practices</h3>
<ul>
    <li>Enable CUPED by default for revenue and engagement metrics</li>
    <li>Use at least 7 days of pre-experiment data</li>
    <li>Set outlier caps based on your data distribution</li>
    <li>Monitor for excessive capping that might hide real effects</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 10: Advanced Topics & Best Practices

    private LearningModule BuildAdvancedTopicsModule()
    {
        return new LearningModule
        {
            Id = "advanced-topics",
            Title = "Advanced Topics & Best Practices",
            Description = "Explore Opal AI integration, performance optimisation, data governance, and enterprise patterns.",
            Icon = "sparkles",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "at-opal-ai-integration",
                    ModuleId = "advanced-topics",
                    Title = "Opal AI Integration",
                    Summary = "Leverage Optimizely Opal AI for automated insights, exploration generation, and intelligent analysis.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand Opal AI capabilities within Analytics",
                        "Use AI exploration generator for natural language queries",
                        "Leverage AI summaries for exploration insights",
                        "Apply predictive analytics features"
                    },
                    Content = @"
<h2>Opal AI Integration</h2>
<p>Optimizely Opal is an <strong>AI-powered assistant</strong> integrated across the Optimizely One platform, bringing intelligent automation and insights directly into Analytics workflows.</p>

<h3>What is Optimizely Opal?</h3>
<p>Opal is an agent orchestration platform that helps you work smarter with your analytics. It's directly connected to your workflows, fully aware of your data model, and powered by hundreds of specialised tools.</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Key Opal Capabilities:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• <strong>Natural Language Queries</strong> - Ask questions in plain English</li>
        <li>• <strong>Exploration Generation</strong> - Auto-create analyses from prompts</li>
        <li>• <strong>AI Summaries</strong> - Get key takeaways automatically</li>
        <li>• <strong>Predictive Analytics</strong> - Forecast future behaviour</li>
        <li>• <strong>Experiment Review</strong> - AI-powered experiment analysis</li>
    </ul>
</div>

<h3>AI Exploration Generator</h3>
<p>Generate explorations instantly by typing natural-language questions:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  🤖 Opal AI - Exploration Generator                             │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Ask Opal:                                                       │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ ""What's the conversion rate for new users from paid     │    │
│  │  campaigns over the last 30 days, broken down by device?""│    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  [Generate Exploration]                                          │
│                                                                  │
├─────────────────────────────────────────────────────────────────┤
│  Opal is creating an exploration with:                          │
│                                                                  │
│  ✓ Metric: Conversion Rate (unique)                             │
│  ✓ Cohort: New Users                                            │
│  ✓ Filter: Source = Paid Campaign                               │
│  ✓ Date Range: Last 30 days                                     │
│  ✓ Breakdown: Device Type                                       │
│                                                                  │
│  [View Exploration]  [Modify]  [Save]                           │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<div class=""bg-amber-50 dark:bg-amber-900/20 border-l-4 border-amber-500 p-4 my-4"">
    <p class=""font-medium"">📋 Note:</p>
    <p>AI Exploration Generator requires Opti ID authentication. Ensure your organisation has enabled Opal access.</p>
</div>

<h3>AI Exploration Summary</h3>
<p>Get automatic summaries of any exploration with key insights:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  📊 Exploration: Weekly Active Users                            │
│  🤖 AI Summary                                                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Key Takeaways:                                                  │
│                                                                  │
│  1. Weekly active users grew 15% month-over-month, reaching     │
│     an all-time high of 127,450 users this week.                │
│                                                                  │
│  2. Mobile users now represent 68% of weekly actives, up        │
│     from 61% last quarter.                                       │
│                                                                  │
│  3. The Premium tier shows 23% higher engagement than Free      │
│     users, suggesting strong product-market fit for paid.       │
│                                                                  │
│  4. Tuesday and Wednesday remain peak usage days, with 40%      │
│     higher activity than weekends.                               │
│                                                                  │
│  Recommended Actions:                                            │
│  • Investigate mobile conversion optimisation opportunities     │
│  • Consider Tuesday/Wednesday for feature launches              │
│  • Explore Premium upgrade prompts for engaged Free users       │
│                                                                  │
│  [Copy Summary]  [Share]  [Ask Follow-up]                       │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Experiment Review Agent</h3>
<p>Opal can review your experiment configuration and recommend optimisations:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Review Area</th>
            <th class=""px-4 py-2 text-left"">What Opal Checks</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Sample Size</td><td class=""px-4 py-2"">Is the sample sufficient to detect expected effect?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Metric Selection</td><td class=""px-4 py-2"">Are primary/secondary metrics aligned with hypothesis?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Duration</td><td class=""px-4 py-2"">Is runtime sufficient for valid conclusions?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Guardrails</td><td class=""px-4 py-2"">Are critical guardrail metrics configured?</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Segmentation</td><td class=""px-4 py-2"">Should you pre-plan segment analysis?</td></tr>
    </tbody>
</table>

<h3>Predictive Analytics</h3>
<p>Opal analyses historical data and behaviour trends to predict future outcomes:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Predictive Capabilities:</h4>
    <ul class=""mt-2 space-y-2"">
        <li>• <strong>Churn Prediction</strong> - Identify users at risk of leaving</li>
        <li>• <strong>Conversion Likelihood</strong> - Score users by conversion probability</li>
        <li>• <strong>Engagement Forecasting</strong> - Project future activity levels</li>
        <li>• <strong>Revenue Projection</strong> - Estimate upcoming revenue based on trends</li>
        <li>• <strong>Anomaly Detection</strong> - Alert when metrics deviate unexpectedly</li>
    </ul>
</div>

<h3>AI Credit Model</h3>
<p>Opal features use a credit-based system:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Opal Credit Usage:

Feature                    │ Credits per Use
───────────────────────────┼────────────────
Exploration Generator      │     5 credits
AI Summary                 │     2 credits
Experiment Review          │     3 credits
Natural Language Query     │     1 credit
Predictive Analysis        │    10 credits

Organisation Credits: 5,000/month
Used this month: 1,247 credits
Remaining: 3,753 credits
</pre>

<h3>Best Practices for Opal</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Be Specific</strong> - Detailed prompts yield better explorations</li>
        <li>• <strong>Review AI Output</strong> - Always validate generated analyses</li>
        <li>• <strong>Iterate</strong> - Use follow-up questions to refine insights</li>
        <li>• <strong>Combine with Manual</strong> - Use AI as a starting point, then customise</li>
        <li>• <strong>Monitor Credits</strong> - Track usage to stay within allocation</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-performance-optimization",
                    ModuleId = "advanced-topics",
                    Title = "Performance Optimisation",
                    Summary = "Optimise query performance, leverage materialisation, and implement sampling strategies.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand materialisation and its benefits",
                        "Configure query-time sampling for large datasets",
                        "Optimise warehouse configuration for Analytics",
                        "Monitor and reduce warehouse costs"
                    },
                    Content = @"
<h2>Performance Optimisation</h2>
<p>Optimizely Analytics executes queries directly on your warehouse, making <strong>performance optimisation</strong> critical for fast insights and cost management.</p>

<h3>Materialisation</h3>
<p>Analytics can create materialised tables in your warehouse to cache common computations and improve performance:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Settings > General Settings > Materialization                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Materialization Status: ● Enabled                              │
│                                                                  │
│  Schema: analytics_cache                                         │
│  Last Refresh: 2024-01-15 02:00:00 UTC                          │
│  Next Refresh: 2024-01-16 02:00:00 UTC                          │
│                                                                  │
│  Refresh Schedule (cron): 0 2 * * *                             │
│  (Runs daily at 2:00 AM UTC)                                    │
│                                                                  │
│  Cached Data:                                                    │
│  ├── Unique column values    ✓ Cached                           │
│  ├── Common aggregations     ✓ Cached                           │
│  ├── Selector drop-downs     ✓ Cached                           │
│  └── Frequently used joins   ✓ Cached                           │
│                                                                  │
│  [Refresh Now]  [Configure Schedule]  [Clear Cache]             │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<div class=""bg-green-50 dark:bg-green-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold text-gray-900 dark:text-gray-100"">✅ Benefits of Materialisation</p>
    <ul class=""mt-2 space-y-1 text-gray-700 dark:text-gray-300"">
        <li>Faster Drop-downs - Instant loading of unique values</li>
        <li>Reduced Warehouse Load - Cached results avoid re-computation</li>
        <li>Lower Costs - Fewer warehouse queries = lower billing</li>
        <li>Better UX - Responsive interface for users</li>
    </ul>
</div>

<h3>Query-Time Sampling</h3>
<p>For large datasets, enable sampling to run explorations orders of magnitude faster:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Query-Time Sampling (Enterprise Performance Pack)

Without Sampling:
┌────────────────────────────────────────────┐
│ Full Dataset: 500 million events           │
│ Query Time: 45 seconds                     │
│ Warehouse Cost: $2.50                      │
└────────────────────────────────────────────┘

With 1% Sampling:
┌────────────────────────────────────────────┐
│ Sampled Dataset: 5 million events          │
│ Query Time: 0.8 seconds                    │
│ Warehouse Cost: $0.05                      │
│ Statistical Confidence: 99%+              │
└────────────────────────────────────────────┘
</pre>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Key Sampling Advantage:</p>
    <p>Unlike other vendors, Optimizely Analytics samples at <strong>query time</strong>, not ingestion time. Your full data is always available—sampling is a choice per exploration, and you can run unsampled queries when precision matters most.</p>
</div>

<h3>Warehouse-Specific Optimisations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Warehouse</th>
            <th class=""px-4 py-2 text-left"">Optimisation Tips</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">BigQuery</td>
            <td class=""px-4 py-2"">Use partitioned tables by date, cluster by frequently filtered columns</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Snowflake</td>
            <td class=""px-4 py-2"">Configure appropriate warehouse size, enable auto-suspend, use clustering keys</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Databricks</td>
            <td class=""px-4 py-2"">Use Delta tables, enable Auto Optimize, configure Z-ordering</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Redshift</td>
            <td class=""px-4 py-2"">Define distribution and sort keys aligned with common queries</td>
        </tr>
    </tbody>
</table>

<h3>Schema Design for Performance</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Performance-Optimised Schema Pattern:

events_table:
├── event_date (DATE)           -- Partition key (REQUIRED)
├── event_timestamp (TIMESTAMP)
├── actor_id (STRING)           -- Cluster key
├── event_type (STRING)         -- Cluster key
├── session_id (STRING)
└── properties (STRUCT/JSON)

Recommendations:
✓ Partition by date (reduce scan size)
✓ Cluster by actor_id and event_type (common filters)
✓ Use native types (avoid STRING for numbers)
✓ Denormalise where possible (reduce joins)
</pre>

<h3>Monitoring Performance</h3>
<p>Track query performance and costs:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Performance Dashboard                                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Today's Metrics:                                                │
│  ├── Total Queries: 1,247                                       │
│  ├── Avg Query Time: 2.3 seconds                                │
│  ├── P95 Query Time: 8.1 seconds                                │
│  ├── Warehouse Cost: $47.50                                     │
│  └── Cache Hit Rate: 67%                                        │
│                                                                  │
│  Slowest Explorations:                                          │
│  1. User Journey Analysis        │ 45s  │ Full scan             │
│  2. Monthly Cohort Report        │ 32s  │ Large date range      │
│  3. Revenue by Segment           │ 28s  │ Complex joins         │
│                                                                  │
│  Recommendations:                                                │
│  • Enable sampling for 'User Journey Analysis'                  │
│  • Add date filter to 'Monthly Cohort Report'                   │
│  • Review join efficiency in 'Revenue by Segment'               │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Cost Management Best Practices</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Reduce Warehouse Costs:</h4>
    <ul class=""mt-2 space-y-2"">
        <li>• <strong>Enable Materialisation</strong> - Cache frequently accessed data</li>
        <li>• <strong>Use Sampling</strong> - For exploratory analysis, not final reports</li>
        <li>• <strong>Limit Date Ranges</strong> - Only query data you need</li>
        <li>• <strong>Optimise Schedules</strong> - Refresh dashboards during off-peak hours</li>
        <li>• <strong>Review Slow Queries</strong> - Identify and optimise expensive explorations</li>
        <li>• <strong>Right-size Warehouse</strong> - Match compute to workload</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-data-governance",
                    ModuleId = "advanced-topics",
                    Title = "Data Governance",
                    Summary = "Implement security controls, manage permissions, and ensure compliance in Analytics.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure roles and permissions for Analytics users",
                        "Understand object-level access controls",
                        "Implement folder-based permission inheritance",
                        "Ensure compliance with security standards"
                    },
                    Content = @"
<h2>Data Governance</h2>
<p>Optimizely Analytics provides <strong>enterprise-grade governance</strong> capabilities to control access, ensure security, and maintain compliance across your organisation.</p>

<h3>Security Architecture</h3>
<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium"">Key Security Principles:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ <strong>Warehouse-Native</strong> - Data never leaves your warehouse</li>
        <li>✓ <strong>No Data Copy</strong> - Analytics queries in place, no duplication</li>
        <li>✓ <strong>Full Audit Trail</strong> - Every query logged in your warehouse</li>
        <li>✓ <strong>Customer-Controlled</strong> - Revoke access anytime</li>
        <li>✓ <strong>SOC-2 Type II</strong> - Certified security compliance</li>
    </ul>
</div>

<h3>Roles and Permissions</h3>
<p>Analytics uses role-based access control integrated with Opti ID:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  Admin Centre > Roles & Permissions                             │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  System Roles:                                                   │
│  ┌───────────────────┬────────────────────────────────────────┐ │
│  │ Org Admin         │ Full access to all objects and settings│ │
│  │ Analytics Admin   │ Manage Analytics config and users      │ │
│  │ Analyst           │ Create/edit explorations and dashboards│ │
│  │ Viewer            │ View shared content only               │ │
│  │ Data Steward      │ Manage datasets and semantic layer     │ │
│  └───────────────────┴────────────────────────────────────────┘ │
│                                                                  │
│  [Create Custom Role]  [Manage Assignments]                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Object-Level Access Control</h3>
<p>Control access granularly for each object type:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Access Level</th>
            <th class=""px-4 py-2 text-left"">Capabilities</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Can View</td>
            <td class=""px-4 py-2"">View the object, cannot edit or share</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Can Edit</td>
            <td class=""px-4 py-2"">View and edit, cannot share with others</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Full Access</td>
            <td class=""px-4 py-2"">View, edit, and share the object</td>
        </tr>
    </tbody>
</table>

<h3>Folder-Based Permissions</h3>
<p>Organise content into folders with inherited permissions:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Folder Structure with Permissions:

📁 Analytics Catalog
├── 📁 Company-Wide (All Users: Can View)
│   ├── 📊 Daily Active Users
│   ├── 📊 Revenue Dashboard
│   └── 📊 Product Metrics
│
├── 📁 Marketing Team (Marketing Group: Full Access)
│   ├── 📊 Campaign Performance
│   ├── 📊 Attribution Analysis
│   └── 📊 Channel Mix
│
├── 📁 Product Team (Product Group: Full Access)
│   ├── 📊 Feature Adoption
│   ├── 📊 User Journeys
│   └── 📊 Retention Analysis
│
└── 📁 Executive (Executives: Can View, BI Team: Full Access)
    ├── 📊 Board Metrics
    └── 📊 Quarterly Review

Note: Objects inherit folder permissions unless explicitly overridden.
</pre>

<h3>How Users Gain Access</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Access Paths:</h4>
    <ul class=""mt-2 space-y-2"">
        <li>• <strong>Group Membership</strong> - Belong to a group with access</li>
        <li>• <strong>Parent Group</strong> - Belong to a group whose parent has access</li>
        <li>• <strong>Direct Grant</strong> - Explicitly granted access to the object</li>
        <li>• <strong>Role Assignment</strong> - Certain roles (Org Admin) have universal access</li>
        <li>• <strong>Folder Inheritance</strong> - Object inherits from its folder</li>
    </ul>
</div>

<h3>Authentication & SSO</h3>
<p>Analytics uses Okta for enterprise authentication:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Authentication Configuration:

Identity Provider: Okta (Optimizely-managed)
│
├── SSO Options:
│   ├── SAML 2.0 Integration        ✓ Supported
│   ├── Corporate IdP Federation    ✓ Supported
│   └── SCIM User Provisioning      ✓ Supported
│
├── Security Features:
│   ├── Multi-Factor Authentication ✓ Enabled
│   ├── Session Timeout             30 minutes
│   └── IP Allowlisting             ✓ Available
│
└── Compliance:
    ├── SOC-2 Type II               ✓ Certified
    ├── GDPR                        ✓ Compliant
    └── HIPAA                       ✓ Available (Enterprise)
</pre>

<h3>Data Privacy Controls</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Control</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">PII Masking</td>
            <td class=""px-4 py-2"">Hide sensitive columns from non-authorised users</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Row-Level Security</td>
            <td class=""px-4 py-2"">Restrict data access by user attributes (via warehouse)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Column Restrictions</td>
            <td class=""px-4 py-2"">Limit which columns users can query</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Query Audit</td>
            <td class=""px-4 py-2"">Full audit trail in warehouse logs</td>
        </tr>
    </tbody>
</table>

<h3>Governance Best Practices</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Use Groups</strong> - Assign permissions to groups, not individuals</li>
        <li>• <strong>Organise by Team</strong> - Create folders matching organisational structure</li>
        <li>• <strong>Minimal Access</strong> - Grant least privilege needed</li>
        <li>• <strong>Regular Audits</strong> - Review permissions quarterly</li>
        <li>• <strong>Document Policies</strong> - Maintain clear access policies</li>
        <li>• <strong>Use SSO</strong> - Centralise authentication through corporate IdP</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-alerting-monitoring",
                    ModuleId = "advanced-topics",
                    Title = "Alerting & Monitoring",
                    Summary = "Configure guardrail alerts, set up metric notifications, and monitor experiment health.",
                    Order = 4,
                    EstimatedMinutes = 14,
                    LearningObjectives = new List<string>
                    {
                        "Configure guardrail alerts for experiments",
                        "Set up metric threshold notifications",
                        "Monitor experiment health and SRM",
                        "Create actionable alerting strategies"
                    },
                    Content = @"
<h2>Alerting & Monitoring</h2>
<p>Proactive alerting helps you catch issues early and make timely decisions. Optimizely Analytics provides <strong>guardrail alerts</strong> and monitoring tools to keep your experiments and metrics healthy.</p>

<h3>Guardrail Alerts</h3>
<p>Get notified when key metrics decline beyond acceptable thresholds:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│  ⚠️  Guardrail Alert Triggered                                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Experiment: New Checkout Flow                                   │
│  Variation: Simplified Form                                      │
│                                                                  │
│  Guardrail Metric: Error Rate                                    │
│  Threshold: > 2% increase                                        │
│  Current Value: +4.5% vs Control                                │
│                                                                  │
│  Status: ⚠️  THRESHOLD EXCEEDED                                  │
│                                                                  │
│  Recommendation:                                                 │
│  Investigate error logs for the Simplified Form variation.       │
│  Consider pausing the experiment if errors persist.             │
│                                                                  │
│  [View Experiment]  [Pause Variation]  [Dismiss]                │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Types of Guardrails</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Guardrail Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Performance</td>
            <td class=""px-4 py-2"">Protect user experience</td>
            <td class=""px-4 py-2"">Page load time, error rate</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Revenue</td>
            <td class=""px-4 py-2"">Protect business metrics</td>
            <td class=""px-4 py-2"">Revenue per user, AOV</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Engagement</td>
            <td class=""px-4 py-2"">Monitor user behaviour</td>
            <td class=""px-4 py-2"">Session duration, bounce rate</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Quality</td>
            <td class=""px-4 py-2"">Ensure data integrity</td>
            <td class=""px-4 py-2"">SRM detection, null rates</td>
        </tr>
    </tbody>
</table>

<h3>Configuring Alerts</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Alert Configuration:

┌─────────────────────────────────────────────────────────────────┐
│  Create New Alert                                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Alert Name: Revenue Drop Alert                                  │
│                                                                  │
│  Metric: Revenue per User                                        │
│  Condition: Decreases by more than                               │
│  Threshold: [ 5 ] %                                              │
│  Compared to: Control                                            │
│                                                                  │
│  Trigger When:                                                   │
│  ○ Immediately when threshold crossed                           │
│  ● After sustained for [ 24 ] hours                             │
│  ○ With statistical significance                                │
│                                                                  │
│  Notify:                                                         │
│  ☑ Email: team@company.com                                       │
│  ☑ Slack: #experiments-alerts                                    │
│  ☐ Webhook: (not configured)                                     │
│                                                                  │
│  [Save Alert]  [Test Alert]  [Cancel]                           │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>SRM Monitoring</h3>
<p>Sample Ratio Mismatch detection runs automatically:</p>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium"">🚨 SRM Alert Example:</p>
    <p class=""mt-2"">Your experiment ""Homepage Redesign"" has a significant Sample Ratio Mismatch. Expected 50/50 split, actual is 54/46 (p-value: 0.0003). Results may be unreliable.</p>
    <p class=""mt-2 text-sm"">Recommended Action: Investigate implementation before making decisions.</p>
</div>

<h3>Monitoring Goals</h3>
<p>Beyond primary and secondary metrics, use monitoring goals for diagnostic insights:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Experiment: Checkout Flow Redesign

Primary Metric:    Conversion Rate
Secondary Metric:  Revenue per User
Guardrail Metrics: Error Rate, Page Load Time

Monitoring Goals (Diagnostic):
├── Add to Cart Rate      → Is the funnel healthy?
├── Form Abandonment      → Where do users drop off?
├── Support Ticket Rate   → Is the new flow confusing?
├── Return Rate           → Are converted users satisfied?
└── Mobile Conversion     → Does it work across devices?

Monitoring goals don't affect ship decisions but provide
valuable diagnostic information for understanding results.
</pre>

<h3>Notification Channels</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Channel</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Email</td><td class=""px-4 py-2"">Formal notifications, audit trail</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Slack/Teams</td><td class=""px-4 py-2"">Real-time team awareness</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Webhook</td><td class=""px-4 py-2"">Integration with ticketing/monitoring tools</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">In-App</td><td class=""px-4 py-2"">Immediate visibility when using Analytics</td></tr>
    </tbody>
</table>

<h3>Alerting Best Practices</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Effective Alerting Strategy:</h4>
    <ul class=""mt-2 space-y-2"">
        <li>• <strong>Right Thresholds</strong> - Set meaningful limits, not too sensitive</li>
        <li>• <strong>Avoid Alert Fatigue</strong> - Only alert on actionable issues</li>
        <li>• <strong>Define Owners</strong> - Each alert should have a clear owner</li>
        <li>• <strong>Document Responses</strong> - Create runbooks for common alerts</li>
        <li>• <strong>Review Regularly</strong> - Tune alerts based on false positive rate</li>
        <li>• <strong>Layer Alerts</strong> - Warning → Critical escalation paths</li>
    </ul>
</div>

<h3>SDK Notification Listeners</h3>
<p>For deeper integration, use SDK notification listeners to pipe events to external monitoring:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example: Send decision data to monitoring tools
optimizely.notificationCenter.addNotificationListener(
  NotificationType.DECISION,
  (type, userId, attributes, decisionInfo) => {
    // Send to DataDog, New Relic, etc.
    monitoringClient.trackEvent('experiment_decision', {
      experiment: decisionInfo.experimentKey,
      variation: decisionInfo.variationKey,
      userId: userId,
      timestamp: Date.now()
    });
  }
);
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-integration-patterns",
                    ModuleId = "advanced-topics",
                    Title = "Integration Patterns",
                    Summary = "Connect Analytics with CMS, ODP, Experimentation, and other Optimizely products.",
                    Order = 5,
                    EstimatedMinutes = 16,
                    LearningObjectives = new List<string>
                    {
                        "Integrate Analytics with Optimizely Experimentation",
                        "Connect Analytics with ODP and CMS",
                        "Understand the Optimizely One ecosystem",
                        "Build end-to-end analytics workflows"
                    },
                    Content = @"
<h2>Integration Patterns</h2>
<p>Optimizely Analytics connects seamlessly with the broader <strong>Optimizely One</strong> ecosystem, enabling end-to-end workflows from content creation to experimentation analysis.</p>

<h3>The Optimizely One Ecosystem</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                     Optimizely One Platform                     │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌─────────────┐    ┌─────────────┐    ┌─────────────┐         │
│  │     CMS     │    │   Commerce  │    │     CMP     │         │
│  │  (Content)  │←──→│   (Store)   │←──→│ (Marketing) │         │
│  └──────┬──────┘    └──────┬──────┘    └──────┬──────┘         │
│         │                   │                   │                │
│         ▼                   ▼                   ▼                │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │                Optimizely Data Platform (ODP)              │  │
│  │            Unified Customer Data & Segmentation            │  │
│  └───────────────────────────────────────────────────────────┘  │
│         │                   │                   │                │
│         ▼                   ▼                   ▼                │
│  ┌─────────────┐    ┌─────────────┐    ┌─────────────┐         │
│  │   Feature   │    │     Web     │    │  Analytics  │         │
│  │    Exp.     │←──→│    Exp.     │←──→│  (Insights) │         │
│  └─────────────┘    └─────────────┘    └─────────────┘         │
│                                                                  │
│                        🤖 Opal AI                               │
│               (Orchestrating across all products)               │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Analytics + Experimentation</h3>
<p>The primary integration: analyse experiments with warehouse-native depth.</p>

<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <p class=""font-medium"">Warehouse-Native Experimentation Analytics:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Experiment data flows from Web/Feature Experimentation to your warehouse</li>
        <li>• Analytics queries experiment results directly from warehouse</li>
        <li>• Stats Engine provides Bayesian, Sequential, and Fixed Horizon analysis</li>
        <li>• CUPED variance reduction available on warehouse data</li>
    </ul>
</div>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Experimentation Integration Flow:

┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│  Web/Feature    │    │   Your Data     │    │   Optimizely    │
│ Experimentation │───▶│   Warehouse     │◀───│    Analytics    │
└─────────────────┘    └─────────────────┘    └─────────────────┘
        │                      │                      │
   Decisions &           Experiment &            Queries &
   Events sent           behavioural             analysis
   to warehouse          data stored             results

Benefits:
✓ Single source of truth (your warehouse)
✓ Join experiment data with all business data
✓ Custom metrics from any warehouse table
✓ No data export/import needed
</pre>

<h3>Analytics + ODP</h3>
<p>Combine Analytics insights with ODP's customer data platform:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Integration</th>
            <th class=""px-4 py-2 text-left"">Capability</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Cohort Sync</td>
            <td class=""px-4 py-2"">Push Analytics cohorts to ODP for activation</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Profile Enrichment</td>
            <td class=""px-4 py-2"">Enhance ODP profiles with Analytics insights</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Event Unification</td>
            <td class=""px-4 py-2"">Combine ODP events with warehouse data</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Segment Analysis</td>
            <td class=""px-4 py-2"">Analyse ODP segments in Analytics explorations</td>
        </tr>
    </tbody>
</table>

<h3>Analytics + CMS</h3>
<p>Track content performance and visitor journeys:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
CMS + Analytics Integration:

CMS Events Tracked:
├── Page Views        → Which content is popular?
├── Scroll Depth      → How engaged are readers?
├── Video Plays       → Video content performance
├── Form Submissions  → Conversion tracking
├── Click Events      → CTA effectiveness
└── Search Queries    → Content discovery patterns

Analytics Capabilities:
├── Content Performance Dashboard
├── Author/Category Analysis
├── Reader Journey Mapping
├── Content ROI Attribution
└── A/B Test Content Variations
</pre>

<h3>Analytics + Content Recommendations</h3>
<p>Measure recommendation effectiveness:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-semibold"">Integration Benefits:</h4>
    <ul class=""mt-2 space-y-2"">
        <li>• <strong>Topic Interest Tracking</strong> - ODP profiles enriched with topic interests from Content Recs</li>
        <li>• <strong>Recommendation CTR</strong> - Measure click-through on recommended content</li>
        <li>• <strong>Engagement Lift</strong> - Compare engaged vs non-engaged visitors</li>
        <li>• <strong>Conversion Attribution</strong> - Track recommendation impact on conversions</li>
    </ul>
</div>

<h3>API & Webhook Patterns</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Integration Patterns:

1. PUSH Pattern (Analytics → External)
   └── Webhooks notify external systems of events
       Example: Alert Slack when experiment reaches significance

2. PULL Pattern (External → Analytics)
   └── External systems query Analytics data
       Example: BI tool pulls dashboard data via API

3. SYNC Pattern (Bidirectional)
   └── Keep systems in sync
       Example: Cohorts synced between Analytics and ODP

4. STREAM Pattern (Real-time)
   └── Continuous data flow
       Example: SDK events streamed to warehouse
</pre>

<h3>Building End-to-End Workflows</h3>
<p>Example workflow using multiple integrations:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Complete Optimisation Workflow:

1. DISCOVER (Analytics)
   └── Funnel analysis reveals 40% drop at checkout

2. HYPOTHESISE (CMP + Opal)
   └── Marketing team proposes simplified checkout
   └── Opal generates hypothesis and test plan

3. CREATE (CMS)
   └── New checkout page created in CMS
   └── Content ready for testing

4. EXPERIMENT (Web Experimentation)
   └── A/B test deployed: Original vs Simplified
   └── Decision events sent to warehouse

5. ANALYSE (Analytics)
   └── Warehouse-native analysis shows +15% conversion
   └── Stats Engine confirms 95% confidence
   └── No guardrail violations detected

6. ACTIVATE (ODP)
   └── Winner deployed to 100% of users
   └── Cohort of converters created for nurture campaign

7. ITERATE
   └── Analytics identifies next optimisation opportunity
</pre>

<h3>Integration Best Practices</h3>
<div class=""bg-violet-50 dark:bg-violet-900/20 border-l-4 border-violet-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li>• <strong>Single Source of Truth</strong> - Use warehouse as the central data store</li>
        <li>• <strong>Consistent Identifiers</strong> - Use same user IDs across products</li>
        <li>• <strong>Event Taxonomy</strong> - Standardise event naming across integrations</li>
        <li>• <strong>Test Integrations</strong> - Verify data flows before production use</li>
        <li>• <strong>Monitor Health</strong> - Set up alerts for integration failures</li>
        <li>• <strong>Document Flows</strong> - Maintain integration architecture docs</li>
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
