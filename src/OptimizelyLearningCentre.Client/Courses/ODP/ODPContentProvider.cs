using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.ODP;

/// <summary>
/// Content provider for the Optimizely Data Platform (ODP) course
/// </summary>
public class ODPContentProvider : ILearningContentProvider
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
            BuildDataStructureModule(),
            BuildEventsTrackingModule(),
            BuildWebSdkModule(),
            BuildSegmentsModule(),
            BuildApisModule(),
            BuildIntegrationsModule(),
            BuildPrivacyComplianceModule(),
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
            Description = "Learn the fundamentals of Optimizely Data Platform and understand how unified customer data drives personalisation.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-odp",
                    ModuleId = "getting-started",
                    Title = "What is Optimizely Data Platform?",
                    Summary = "Discover ODP and its capabilities for unifying customer data across channels.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Data Platform (ODP) is and its purpose",
                        "Learn the key benefits of using a Customer Data Platform",
                        "Understand how ODP fits within the Optimizely ecosystem",
                        "Know when and why to use ODP for your projects"
                    },
                    Content = @"
<h2>Introduction to Optimizely Data Platform</h2>
<p>Optimizely Data Platform (ODP) is a <strong>unified Customer Data Platform (CDP)</strong> that harmonises customer data from multiple sources, providing a real-time understanding of your customers' behaviours across all touchpoints.</p>

<h3>What is a Customer Data Platform?</h3>
<p>A Customer Data Platform (CDP) is software that creates a persistent, unified customer database that is accessible to other systems. Unlike traditional data warehouses or CRM systems, a CDP:</p>
<ul>
    <li>Collects first-party data from multiple sources</li>
    <li>Creates unified customer profiles in real-time</li>
    <li>Makes data available for activation across channels</li>
    <li>Operates with minimal IT involvement once configured</li>
</ul>

<div class=""bg-indigo-50 dark:bg-indigo-900/30 border-l-4 border-indigo-500 p-4 my-4"">
    <p class=""font-medium text-indigo-800 dark:text-indigo-200"">Key Concept</p>
    <p class=""text-indigo-700 dark:text-indigo-300"">ODP harmonises customer data from websites, mobile apps, commerce platforms, CRM systems, and 60+ third-party integrations into a single, actionable customer view that updates in real-time.</p>
</div>

<h3>Why ODP?</h3>
<p>Modern customers interact with brands across numerous channels - websites, mobile apps, email, social media, in-store, and more. ODP solves the challenge of understanding these fragmented interactions by:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Challenge</th>
            <th class=""px-4 py-2 text-left"">ODP Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Fragmented customer data</td><td class=""px-4 py-2"">Unified customer profiles across all touchpoints</td></tr>
        <tr><td class=""px-4 py-2"">Delayed data availability</td><td class=""px-4 py-2"">Real-time data processing (typically &lt; 2 minutes)</td></tr>
        <tr><td class=""px-4 py-2"">Generic personalisation</td><td class=""px-4 py-2"">AI-powered predictive segments</td></tr>
        <tr><td class=""px-4 py-2"">Siloed marketing tools</td><td class=""px-4 py-2"">60+ native integrations for activation</td></tr>
        <tr><td class=""px-4 py-2"">Privacy compliance burden</td><td class=""px-4 py-2"">Built-in consent and compliance tools</td></tr>
    </tbody>
</table>

<h3>Key Capabilities</h3>
<ul>
    <li><strong>Data Unification</strong> - Collect and merge customer data from any source into unified profiles</li>
    <li><strong>Identity Resolution</strong> - Match anonymous and known customer identities across devices and channels</li>
    <li><strong>Real-Time Segmentation</strong> - Create dynamic audience segments that update instantly</li>
    <li><strong>Predictive Analytics</strong> - Leverage AI to predict customer behaviour (propensity to buy, churn risk)</li>
    <li><strong>Activation</strong> - Push segments to marketing channels for personalised campaigns</li>
    <li><strong>Compliance</strong> - Manage consent, data deletion, and regulatory requirements (GDPR, CCPA)</li>
</ul>

<h3>ODP in the Optimizely Ecosystem</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    Optimizely Ecosystem                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  DATA COLLECTION              OPTIMIZELY DATA PLATFORM           │
│  ┌──────────────┐            ┌──────────────────────────┐       │
│  │   Website    │───────────▶│                          │       │
│  │   (Web SDK)  │            │   Customer Profiles      │       │
│  ├──────────────┤            │   ┌────────────────┐     │       │
│  │   Mobile     │───────────▶│   │  Identity      │     │       │
│  │   (SDK)      │            │   │  Resolution    │     │       │
│  ├──────────────┤            │   └────────────────┘     │       │
│  │   Commerce   │───────────▶│                          │       │
│  │   Connect    │            │   Real-Time Segments     │       │
│  ├──────────────┤            │   Predictive Analytics   │       │
│  │   CRM/Email  │───────────▶│   Consent Management     │       │
│  │   Systems    │            │                          │       │
│  └──────────────┘            └──────────────────────────┘       │
│                                          │                       │
│                              ACTIVATION  │                       │
│                                          ▼                       │
│  ┌──────────────┐    ┌──────────────┐    ┌──────────────┐       │
│  │ Optimizely   │    │ Optimizely   │    │  External    │       │
│  │    CMS       │    │Experimentation│   │  Channels    │       │
│  │ Visitor Groups│   │  Audiences    │   │ (Ads, Email) │       │
│  └──────────────┘    └──────────────┘    └──────────────┘       │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>When to Use ODP</h3>
<ul>
    <li>You need to unify customer data from multiple sources</li>
    <li>You want real-time personalisation across channels</li>
    <li>You need predictive customer insights (propensity scoring)</li>
    <li>You're using or planning to use other Optimizely products</li>
    <li>You need to comply with privacy regulations (GDPR, CCPA)</li>
    <li>You want to create targeted audience segments for marketing</li>
</ul>

<h3>ODP vs Traditional Solutions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">ODP (CDP)</th>
            <th class=""px-4 py-2 text-left"">Data Warehouse</th>
            <th class=""px-4 py-2 text-left"">CRM</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Data Freshness</td><td class=""px-4 py-2"">Real-time</td><td class=""px-4 py-2"">Batch (hours/days)</td><td class=""px-4 py-2"">Near real-time</td></tr>
        <tr><td class=""px-4 py-2"">Identity Resolution</td><td class=""px-4 py-2"">Built-in</td><td class=""px-4 py-2"">Manual/ETL</td><td class=""px-4 py-2"">Limited</td></tr>
        <tr><td class=""px-4 py-2"">Audience Activation</td><td class=""px-4 py-2"">Native integrations</td><td class=""px-4 py-2"">Requires custom code</td><td class=""px-4 py-2"">Limited channels</td></tr>
        <tr><td class=""px-4 py-2"">Anonymous Data</td><td class=""px-4 py-2"">Full support</td><td class=""px-4 py-2"">Limited</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2"">IT Dependency</td><td class=""px-4 py-2"">Low after setup</td><td class=""px-4 py-2"">High</td><td class=""px-4 py-2"">Medium</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-architecture-overview",
                    ModuleId = "getting-started",
                    Title = "ODP Architecture Overview",
                    Summary = "Understand the core components and how data flows through ODP.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand the main components of ODP",
                        "Learn how data flows from collection to activation",
                        "Understand accounts, tracker IDs, and data segregation",
                        "Know the different regional endpoints"
                    },
                    Content = @"
<h2>ODP Architecture</h2>
<p>Optimizely Data Platform is built on a modern, scalable architecture designed for real-time data processing and activation across multiple channels.</p>

<h3>Core Architecture Components</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    ODP Architecture                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  DATA INGESTION                     DATA PROCESSING              │
│  ┌──────────────┐                  ┌──────────────────┐         │
│  │  Web SDK     │─────────────────▶│                  │         │
│  │  (zaius.js)  │                  │   Ingest         │         │
│  ├──────────────┤                  │   Pipeline       │         │
│  │  REST API    │─────────────────▶│                  │         │
│  │              │                  │   (< 2 min       │         │
│  ├──────────────┤                  │    latency)      │         │
│  │  Mobile SDKs │─────────────────▶│                  │         │
│  │              │                  └────────┬─────────┘         │
│  ├──────────────┤                           │                    │
│  │  Commerce    │──────────────────────────▶│                    │
│  │  Connectors  │                           │                    │
│  ├──────────────┤                           │                    │
│  │  CSV/S3      │──────────────────────────▶│                    │
│  │  Import      │                           ▼                    │
│  └──────────────┘                  ┌──────────────────┐         │
│                                    │   Customer       │         │
│                                    │   Profile        │         │
│                                    │   Database       │         │
│                                    └────────┬─────────┘         │
│                                             │                    │
│  SEGMENTATION                               │                    │
│  ┌──────────────┐                           │                    │
│  │  Real-Time   │◀──────────────────────────┤                    │
│  │  Segments    │                           │                    │
│  ├──────────────┤                           │                    │
│  │  Standard    │◀──────────────────────────┘                    │
│  │  Segments    │                                                │
│  └──────┬───────┘                                                │
│         │                                                        │
│  ACTIVATION                                                      │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │  GraphQL     │  │ Integrations │  │   Webhooks   │          │
│  │  API         │  │  (60+)       │  │              │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Accounts and Data Segregation</h3>
<p>ODP segregates data through <strong>Accounts</strong>, each with a unique <strong>Tracker ID</strong> (also called Public API Key). This separation enables:</p>

<ul>
    <li><strong>Multiple Brands</strong> - Manage different brands with separate data</li>
    <li><strong>Geographic Separation</strong> - Isolate data by region for compliance</li>
    <li><strong>Environment Separation</strong> - Maintain separate test and production environments</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">ODP provides separate <strong>test</strong> and <strong>production</strong> environments, each with its own Tracker ID. This allows you to develop and validate implementations without affecting live data.</p>
</div>

<h3>API Keys</h3>
<p>ODP uses two types of API keys for different purposes:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Key Type</th>
            <th class=""px-4 py-2 text-left"">Also Known As</th>
            <th class=""px-4 py-2 text-left"">Usage</th>
            <th class=""px-4 py-2 text-left"">Security</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Public API Key</td>
            <td class=""px-4 py-2"">Tracker ID</td>
            <td class=""px-4 py-2"">Web SDK, client-side tracking</td>
            <td class=""px-4 py-2"">Safe to expose in browser</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Private API Key</td>
            <td class=""px-4 py-2"">API Token</td>
            <td class=""px-4 py-2"">REST API, GraphQL, server-side</td>
            <td class=""px-4 py-2"">Keep secret, never expose</td>
        </tr>
    </tbody>
</table>

<h3>Regional Endpoints</h3>
<p>ODP provides regional API endpoints to ensure data residency compliance and optimal performance:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Region</th>
            <th class=""px-4 py-2 text-left"">REST API Base URL</th>
            <th class=""px-4 py-2 text-left"">GraphQL Endpoint</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">United States</td>
            <td class=""px-4 py-2 font-mono text-sm"">https://api.zaius.com</td>
            <td class=""px-4 py-2 font-mono text-sm"">/v3/graphql</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Europe</td>
            <td class=""px-4 py-2 font-mono text-sm"">https://api.eu1.odp.optimizely.com</td>
            <td class=""px-4 py-2 font-mono text-sm"">/v3/graphql</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Asia-Pacific</td>
            <td class=""px-4 py-2 font-mono text-sm"">https://api.au1.odp.optimizely.com</td>
            <td class=""px-4 py-2 font-mono text-sm"">/v3/graphql</td>
        </tr>
    </tbody>
</table>

<h3>Data Flow</h3>
<p>Understanding the data flow helps you design effective implementations:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li><strong>Collection</strong> - Events and customer data are sent via SDK, API, or integration</li>
    <li><strong>Ingestion</strong> - Data enters the ingest pipeline with the Tracker ID</li>
    <li><strong>Processing</strong> - Identity resolution merges data into unified profiles (typically &lt; 2 minutes)</li>
    <li><strong>Storage</strong> - Profiles, events, and objects are stored in the customer database</li>
    <li><strong>Segmentation</strong> - Customers are evaluated against segment criteria</li>
    <li><strong>Activation</strong> - Segments are pushed to integrations or queried via API</li>
</ol>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Data Latency</p>
    <p class=""text-blue-700 dark:text-blue-300"">Data typically takes less than 2 minutes to traverse the ODP ingest pipeline. This means events sent to ODP will impact segment membership within a few seconds to minutes.</p>
</div>

<h3>Component Summary</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Web SDK (zaius.js)</td><td class=""px-4 py-2"">JavaScript tracking for websites</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">REST API</td><td class=""px-4 py-2"">Server-side data import and management</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">GraphQL API</td><td class=""px-4 py-2"">Querying customer data and segments</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Ingest Pipeline</td><td class=""px-4 py-2"">Real-time data processing and identity resolution</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Profile Database</td><td class=""px-4 py-2"">Unified customer profiles and event history</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Segment Engine</td><td class=""px-4 py-2"">Real-time and standard segment evaluation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Integrations</td><td class=""px-4 py-2"">Connectors to external platforms</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-odp-ui-overview",
                    ModuleId = "getting-started",
                    Title = "Navigating the ODP Interface",
                    Summary = "Get familiar with the ODP user interface and key navigation areas.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the main sections of the ODP interface",
                        "Understand the organisation of customers, segments, and data",
                        "Know where to find configuration and settings",
                        "Understand the difference between test and production environments"
                    },
                    Content = @"
<h2>The ODP User Interface</h2>
<p>The ODP interface is organised into logical sections that group related functionality. Understanding the navigation helps you work efficiently with customer data, segments, and integrations.</p>

<h3>Main Navigation Areas</h3>
<p>The ODP interface (updated in 2024) groups pages intuitively by function:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Section</th>
            <th class=""px-4 py-2 text-left"">Contains</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Customers</td>
            <td class=""px-4 py-2"">Profiles, Standard Segments, Real-Time Segments, Lists</td>
            <td class=""px-4 py-2"">View and manage customer data and audiences</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Data</td>
            <td class=""px-4 py-2"">Objects, Fields, Events, Identifiers</td>
            <td class=""px-4 py-2"">Configure your data schema</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Integrations</td>
            <td class=""px-4 py-2"">Connected platforms, Data sources</td>
            <td class=""px-4 py-2"">Manage third-party connections</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Analytics</td>
            <td class=""px-4 py-2"">Reports, Dashboards</td>
            <td class=""px-4 py-2"">View customer insights and metrics</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Settings</td>
            <td class=""px-4 py-2"">Account, API Keys, Users, Consent</td>
            <td class=""px-4 py-2"">Configure account-level settings</td>
        </tr>
    </tbody>
</table>

<h3>Customers Section</h3>
<p>The Customers section is where you work with customer data and audiences:</p>

<ul>
    <li><strong>Profiles</strong> - Browse and search individual customer profiles</li>
    <li><strong>Standard Segments</strong> - Create segments using historical data (no time limit)</li>
    <li><strong>Real-Time Segments</strong> - Create segments using recent events (last 28 days)</li>
    <li><strong>Lists</strong> - Manage subscription lists and customer groups</li>
</ul>

<div class=""bg-indigo-50 dark:bg-indigo-900/20 border-l-4 border-indigo-500 p-4 my-4"">
    <p class=""font-medium text-indigo-800 dark:text-indigo-200"">2024 Update</p>
    <p class=""text-indigo-700 dark:text-indigo-300"">Real-time segment membership is now displayed directly on individual customer profile pages, making it easy to see which segments a customer belongs to.</p>
</div>

<h3>Data Section</h3>
<p>The Data section lets you configure how ODP stores and organises information:</p>

<ul>
    <li><strong>Objects</strong> - Define data entities (like ""Orders"" or ""Products"")</li>
    <li><strong>Fields</strong> - Add custom fields to objects</li>
    <li><strong>Events</strong> - View event types and actions being tracked</li>
    <li><strong>Identifiers</strong> - Configure customer identity types</li>
</ul>

<h3>Finding Your API Keys</h3>
<p>API keys are essential for integration. To find them:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Settings</strong> in the main navigation</li>
    <li>Select <strong>API Keys</strong> or <strong>Account Settings</strong></li>
    <li>You'll see both Public (Tracker ID) and Private API keys</li>
</ol>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Security Reminder</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Your Private API Key should never be shared or exposed in client-side code. Only use the Public API Key (Tracker ID) in browser-based implementations.</p>
</div>

<h3>Test vs Production Environments</h3>
<p>ODP provides separate environments for development and production:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Environment</th>
            <th class=""px-4 py-2 text-left"">Use For</th>
            <th class=""px-4 py-2 text-left"">Tracker ID</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Test</td>
            <td class=""px-4 py-2"">Development, QA, validation</td>
            <td class=""px-4 py-2"">Separate from production</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Production</td>
            <td class=""px-4 py-2"">Live customer data</td>
            <td class=""px-4 py-2"">Your main Tracker ID</td>
        </tr>
    </tbody>
</table>

<p>You can switch between environments using the environment selector in the ODP interface. Always verify you're in the correct environment before making changes.</p>

<h3>Getting Help</h3>
<p>Within the ODP interface, you can access:</p>
<ul>
    <li><strong>Documentation</strong> - Links to developer docs and guides</li>
    <li><strong>Support</strong> - Access to Optimizely support channels</li>
    <li><strong>Account Info</strong> - View your subscription and usage</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-implementation-roadmap",
                    ModuleId = "getting-started",
                    Title = "Implementation Roadmap",
                    Summary = "Plan your ODP implementation journey from setup to activation.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the typical ODP implementation phases",
                        "Know what to configure before tracking data",
                        "Plan your data collection strategy",
                        "Understand the path to segment activation"
                    },
                    Content = @"
<h2>Planning Your ODP Implementation</h2>
<p>A successful ODP implementation follows a structured approach, starting with configuration and progressing through data collection to activation.</p>

<h3>Implementation Phases</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                ODP Implementation Roadmap                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  Phase 1: SETUP                                                  │
│  ├─▶ Account configuration                                       │
│  ├─▶ API key setup                                               │
│  └─▶ Schema planning (objects, fields, events)                   │
│                                                                  │
│  Phase 2: DATA COLLECTION                                        │
│  ├─▶ JavaScript tag implementation                               │
│  ├─▶ Historical data import (CSV, API)                           │
│  └─▶ Integration setup (Commerce, CRM, etc.)                     │
│                                                                  │
│  Phase 3: ENRICHMENT                                             │
│  ├─▶ Custom events and fields                                    │
│  ├─▶ Identity resolution tuning                                  │
│  └─▶ Third-party data connections                                │
│                                                                  │
│  Phase 4: SEGMENTATION                                           │
│  ├─▶ Standard segment creation                                   │
│  ├─▶ Real-time segment configuration                             │
│  └─▶ Predictive segment exploration                              │
│                                                                  │
│  Phase 5: ACTIVATION                                             │
│  ├─▶ CMS Visitor Group integration                               │
│  ├─▶ Experimentation connection                                  │
│  └─▶ External channel activation                                 │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Phase 1: Setup Checklist</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>☐ Obtain access to your ODP account</li>
        <li>☐ Identify your Tracker ID (Public API Key) for test and production</li>
        <li>☐ Secure your Private API Key</li>
        <li>☐ Plan your data schema (what events and fields you need)</li>
        <li>☐ Define your customer identifier strategy</li>
        <li>☐ Review consent and privacy requirements</li>
    </ul>
</div>

<h3>Phase 2: Data Collection Strategy</h3>
<p>Decide which data collection methods you'll use:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
            <th class=""px-4 py-2 text-left"">When to Use</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Web SDK (JavaScript)</td>
            <td class=""px-4 py-2"">Website tracking</td>
            <td class=""px-4 py-2"">Page views, user actions, e-commerce</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">REST API</td>
            <td class=""px-4 py-2"">Server-side events</td>
            <td class=""px-4 py-2"">Backend systems, batch imports</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">CSV Import</td>
            <td class=""px-4 py-2"">Historical data</td>
            <td class=""px-4 py-2"">Initial data migration</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Native Integrations</td>
            <td class=""px-4 py-2"">Third-party platforms</td>
            <td class=""px-4 py-2"">CRM, email, advertising</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Commerce Connect</td>
            <td class=""px-4 py-2"">E-commerce data</td>
            <td class=""px-4 py-2"">Optimizely Commerce users</td>
        </tr>
    </tbody>
</table>

<h3>Essential Events to Track</h3>
<p>At minimum, consider tracking these event types:</p>

<ul>
    <li><strong>Pageviews</strong> - Basic site activity</li>
    <li><strong>Customer Identification</strong> - Login, registration</li>
    <li><strong>Product Interactions</strong> - Views, add to cart, wishlist</li>
    <li><strong>Orders</strong> - Purchases, returns (via API, not Web SDK)</li>
    <li><strong>Navigation</strong> - Search, filtering</li>
    <li><strong>Custom Actions</strong> - Any business-specific events</li>
</ul>

<h3>Path to Activation</h3>
<p>Once data is flowing, you can activate it through:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li><strong>CMS Visitor Groups</strong> - Personalise content based on ODP segments</li>
    <li><strong>Feature Experimentation</strong> - Target experiments to specific audiences</li>
    <li><strong>Web Experimentation</strong> - A/B testing with ODP segments</li>
    <li><strong>Email/Marketing</strong> - Export segments to marketing platforms</li>
    <li><strong>Advertising</strong> - Sync audiences to ad platforms</li>
</ol>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Pro Tip</p>
    <p class=""text-green-700 dark:text-green-300"">Start with basic pageview tracking and customer identification. Once that's working, incrementally add more event types and fields. This approach lets you validate your implementation at each step.</p>
</div>

<h3>Success Metrics</h3>
<p>Track these metrics to measure your implementation success:</p>
<ul>
    <li>Number of customer profiles created</li>
    <li>Events tracked per day</li>
    <li>Identity merge rate (anonymous to known)</li>
    <li>Segment population sizes</li>
    <li>Activation usage (campaigns, experiments)</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Data Structure and Customers

    private LearningModule BuildDataStructureModule()
    {
        return new LearningModule
        {
            Id = "data-structure",
            Title = "Data Structure & Customers",
            Description = "Master ODP's data model including customers, objects, fields, identifiers, and how data is organised.",
            Icon = "circle-stack",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ds-core-data-model",
                    ModuleId = "data-structure",
                    Title = "Understanding the ODP Data Model",
                    Summary = "Learn how ODP organises customer data, events, objects, and fields.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the core components of ODP's data model",
                        "Learn how customers, events, and objects relate to each other",
                        "Understand the concept of fields and metadata",
                        "Know how data is structured for querying and segmentation"
                    },
                    Content = @"
<h2>The ODP Data Model</h2>
<p>At its core, ODP organises data around several fundamental components that work together to create a complete picture of your customers and their interactions.</p>

<h3>Core Data Components</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    ODP Data Model                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │                      CUSTOMERS                           │    │
│  │  The central entity - unified profiles of individuals    │    │
│  │  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐     │    │
│  │  │ Identifiers │  │  Attributes │  │   Consent   │     │    │
│  │  │ (email, id) │  │ (name, etc) │  │  Preferences│     │    │
│  │  └─────────────┘  └─────────────┘  └─────────────┘     │    │
│  └─────────────────────────────────────────────────────────┘    │
│                              │                                   │
│                              │ linked via identifiers            │
│                              ▼                                   │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │                       EVENTS                             │    │
│  │  Historical records of customer actions over time        │    │
│  │  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐     │    │
│  │  │ Event Type  │  │Event Action │  │  Timestamp  │     │    │
│  │  │ (pageview)  │  │  (view)     │  │    (ts)     │     │    │
│  │  └─────────────┘  └─────────────┘  └─────────────┘     │    │
│  └─────────────────────────────────────────────────────────┘    │
│                              │                                   │
│                              │ reference                         │
│                              ▼                                   │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │                  OBJECTS & FIELDS                        │    │
│  │  Metadata storage like database tables                   │    │
│  │  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐     │    │
│  │  │  Products   │  │   Orders    │  │   Custom    │     │    │
│  │  │   Object    │  │   Object    │  │   Objects   │     │    │
│  │  └─────────────┘  └─────────────┘  └─────────────┘     │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Customers</h3>
<p>At the core of ODP are <strong>Customers</strong> - unified profiles representing individual people who interact with your brand. Each customer profile can contain:</p>

<ul>
    <li><strong>Identifiers</strong> - Ways to recognise the customer (email, customer ID, phone, VUID)</li>
    <li><strong>Attributes</strong> - Descriptive data (name, location, preferences)</li>
    <li><strong>Consent Status</strong> - Communication preferences and opt-in/out status</li>
    <li><strong>List Memberships</strong> - Subscription lists the customer belongs to</li>
    <li><strong>Segment Memberships</strong> - Audiences the customer qualifies for</li>
</ul>

<div class=""bg-indigo-50 dark:bg-indigo-900/30 border-l-4 border-indigo-500 p-4 my-4"">
    <p class=""font-medium text-indigo-800 dark:text-indigo-200"">Key Concept</p>
    <p class=""text-indigo-700 dark:text-indigo-300"">In ODP, a ""customer"" profile only shows users that have been <strong>identified</strong>. Anonymous visitors tracked only by pageview events won't appear in customer profiles until they're identified (e.g., through login or form submission).</p>
</div>

<h3>Events</h3>
<p>Events are historical records of customer actions over time. They are classified by:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Examples</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Event Type</td>
            <td class=""px-4 py-2"">Category of the event</td>
            <td class=""px-4 py-2"">pageview, product, order, account, email</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Event Action</td>
            <td class=""px-4 py-2"">Specific action within the type</td>
            <td class=""px-4 py-2"">view, add_to_cart, purchase, login, open</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Timestamp (ts)</td>
            <td class=""px-4 py-2"">When the event occurred</td>
            <td class=""px-4 py-2"">2024-01-15T10:30:00Z</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Custom Fields</td>
            <td class=""px-4 py-2"">Additional metadata</td>
            <td class=""px-4 py-2"">product_id, page_url, revenue</td>
        </tr>
    </tbody>
</table>

<h3>Objects and Fields</h3>
<p>Objects are similar to database tables - they store metadata about entities in your system. ODP includes built-in objects and supports custom objects.</p>

<h4>Built-in Objects</h4>
<ul>
    <li><strong>Customers</strong> - Customer profile data</li>
    <li><strong>Events</strong> - Event records and metadata</li>
    <li><strong>Products</strong> - Product catalogue information</li>
    <li><strong>Orders</strong> - Order and transaction data</li>
</ul>

<h4>Custom Objects</h4>
<p>You can create custom objects for business-specific data, such as:</p>
<ul>
    <li>Loyalty programme tiers</li>
    <li>Store locations</li>
    <li>Content categories</li>
    <li>Custom product attributes</li>
</ul>

<h3>Fields</h3>
<p>Fields are the attributes within objects. Each field has:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Name</td><td class=""px-4 py-2"">Unique identifier for the field (snake_case)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Display Name</td><td class=""px-4 py-2"">Human-readable label</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data Type</td><td class=""px-4 py-2"">String, Number, Boolean, Date, etc.</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Description</td><td class=""px-4 py-2"">Documentation for the field</td></tr>
    </tbody>
</table>

<h3>Data Relationships</h3>
<p>Understanding how data relates helps you build effective queries and segments:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Customer ──┬── has many ──▶ Events
           │
           ├── has many ──▶ Identifiers
           │
           ├── belongs to ──▶ Lists
           │
           └── qualifies for ──▶ Segments

Event ────── references ──▶ Products (via product_id)

Order ────── belongs to ──▶ Customer
           │
           └── contains ──▶ Line Items (Products)
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ds-customer-profiles",
                    ModuleId = "data-structure",
                    Title = "Customer Profiles and Identity",
                    Summary = "Deep dive into customer profiles, identifiers, and identity resolution.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand what comprises a customer profile",
                        "Learn the different types of customer identifiers",
                        "Understand how identity resolution works",
                        "Know how anonymous visitors become known customers"
                    },
                    Content = @"
<h2>Customer Profiles in ODP</h2>
<p>A customer profile is the unified view of an individual across all their interactions with your brand. ODP builds these profiles by collecting data from multiple sources and resolving identities.</p>

<h3>Anatomy of a Customer Profile</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    Customer Profile                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  IDENTIFIERS                                                     │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ email: john.doe@example.com                              │    │
│  │ customer_id: CUST-12345                                  │    │
│  │ vuid: a1b2c3d4-e5f6-7890-abcd-ef1234567890              │    │
│  │ phone: +1-555-123-4567                                   │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  ATTRIBUTES                                                      │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ first_name: John                                         │    │
│  │ last_name: Doe                                           │    │
│  │ city: London                                             │    │
│  │ country: United Kingdom                                  │    │
│  │ lifetime_value: 1250.00                                  │    │
│  │ first_purchase_date: 2023-06-15                          │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  CONSENT                                                         │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ email_marketing: opted_in                                │    │
│  │ sms_marketing: opted_out                                 │    │
│  │ push_notifications: not_set                              │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  SEGMENTS                                                        │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ • High Value Customers                                   │    │
│  │ • UK Shoppers                                            │    │
│  │ • Newsletter Subscribers                                 │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  RECENT EVENTS                                                   │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ 2024-01-20 10:30 - pageview (product page)              │    │
│  │ 2024-01-20 10:32 - product (add_to_cart)                │    │
│  │ 2024-01-20 10:35 - order (purchase)                     │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Customer Identifiers</h3>
<p>Identifiers are the keys used to recognise and merge customer data. ODP supports multiple identifier types:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Identifier</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">When to Use</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">VUID</td>
            <td class=""px-4 py-2"">Anonymous visitor ID (cookie-based)</td>
            <td class=""px-4 py-2"">Automatically set by Web SDK</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">email</td>
            <td class=""px-4 py-2"">Customer email address</td>
            <td class=""px-4 py-2"">Login, registration, checkout</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">customer_id</td>
            <td class=""px-4 py-2"">Your system's customer ID</td>
            <td class=""px-4 py-2"">When syncing from CRM/backend</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">phone</td>
            <td class=""px-4 py-2"">Phone number</td>
            <td class=""px-4 py-2"">SMS campaigns, mobile apps</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Custom</td>
            <td class=""px-4 py-2"">Any custom identifier</td>
            <td class=""px-4 py-2"">Loyalty IDs, member numbers</td>
        </tr>
    </tbody>
</table>

<h3>Identity Resolution</h3>
<p>Identity resolution is the process of merging data from different sources into a single customer profile. ODP handles this automatically based on shared identifiers.</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">How Identity Resolution Works</p>
    <p class=""text-blue-700 dark:text-blue-300"">When a new event arrives with an identifier, ODP checks if that identifier is already associated with an existing profile. If so, the event is added to that profile. If not, a new profile may be created.</p>
</div>

<h4>Identity Merge Example</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Scenario: Anonymous visitor becomes a known customer

Step 1: Anonymous Visit
┌─────────────────────────────────────┐
│ Profile A (anonymous)               │
│ vuid: abc123                        │
│ Events: 5 pageviews                 │
└─────────────────────────────────────┘

Step 2: User Logs In (email identified)
┌─────────────────────────────────────┐
│ Profile A (now identified)          │
│ vuid: abc123                        │
│ email: john@example.com             │
│ Events: 5 pageviews + login         │
└─────────────────────────────────────┘

Step 3: User Returns on Different Device
┌─────────────────────────────────────┐     ┌─────────────────┐
│ Profile A                           │     │ New Session     │
│ vuid: abc123                        │ ◀───│ vuid: xyz789    │
│ email: john@example.com             │     │ email: john@... │
│ (profiles merge on email match)     │     └─────────────────┘
└─────────────────────────────────────┘

Result: Unified Profile
┌─────────────────────────────────────┐
│ Profile A (merged)                  │
│ vuid: abc123, xyz789                │
│ email: john@example.com             │
│ Events: All events from both VUIDs  │
└─────────────────────────────────────┘
</pre>

<h3>Anonymous vs Identified Customers</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Anonymous</th>
            <th class=""px-4 py-2 text-left"">Identified</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Identifier</td>
            <td class=""px-4 py-2"">VUID only</td>
            <td class=""px-4 py-2"">Email, customer_id, or other</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">In Customer Profiles</td>
            <td class=""px-4 py-2"">No</td>
            <td class=""px-4 py-2"">Yes</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Cross-Device Tracking</td>
            <td class=""px-4 py-2"">No</td>
            <td class=""px-4 py-2"">Yes</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Events Tracked</td>
            <td class=""px-4 py-2"">Yes</td>
            <td class=""px-4 py-2"">Yes</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Segment Targeting</td>
            <td class=""px-4 py-2"">Real-time only</td>
            <td class=""px-4 py-2"">Full segmentation</td>
        </tr>
    </tbody>
</table>

<h3>Best Practices for Identity</h3>
<ul>
    <li><strong>Identify Early</strong> - Encourage login or email capture to identify customers</li>
    <li><strong>Use Consistent IDs</strong> - Use the same customer_id across all systems</li>
    <li><strong>Validate Email</strong> - Ensure email addresses are valid before sending to ODP</li>
    <li><strong>Respect Privacy</strong> - Only collect identifiers you have consent for</li>
    <li><strong>Avoid PII in Custom Fields</strong> - Keep sensitive data in designated identifier fields</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ds-objects-fields",
                    ModuleId = "data-structure",
                    Title = "Working with Objects and Fields",
                    Summary = "Learn to configure objects and custom fields to extend your data schema.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the built-in objects in ODP",
                        "Learn how to create custom objects",
                        "Know the different field types available",
                        "Create custom fields for your business needs"
                    },
                    Content = @"
<h2>Objects and Fields in ODP</h2>
<p>Objects in ODP are like database tables - they define the structure of your data. Fields are the columns within those tables. ODP provides built-in objects and allows you to create custom ones.</p>

<h3>Built-in Objects</h3>
<p>ODP includes several pre-configured objects that cover common use cases:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Object</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Key Fields</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">customers</td>
            <td class=""px-4 py-2"">Customer profile data</td>
            <td class=""px-4 py-2"">email, first_name, last_name, city</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">events</td>
            <td class=""px-4 py-2"">Event records</td>
            <td class=""px-4 py-2"">ts, type, action, vuid</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">products</td>
            <td class=""px-4 py-2"">Product catalogue</td>
            <td class=""px-4 py-2"">product_id, name, price, category</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">orders</td>
            <td class=""px-4 py-2"">Order records</td>
            <td class=""px-4 py-2"">order_id, total, items, timestamp</td>
        </tr>
    </tbody>
</table>

<h3>Field Types</h3>
<p>When creating custom fields, you can choose from several data types:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example Values</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">String</td>
            <td class=""px-4 py-2"">Text values</td>
            <td class=""px-4 py-2"">""Premium"", ""blue""</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Number</td>
            <td class=""px-4 py-2"">Numeric values (integer or decimal)</td>
            <td class=""px-4 py-2"">42, 99.99</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Boolean</td>
            <td class=""px-4 py-2"">True/false values</td>
            <td class=""px-4 py-2"">true, false</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Timestamp</td>
            <td class=""px-4 py-2"">Date and time</td>
            <td class=""px-4 py-2"">2024-01-15T10:30:00Z</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Array</td>
            <td class=""px-4 py-2"">List of values</td>
            <td class=""px-4 py-2"">[""tag1"", ""tag2""]</td>
        </tr>
    </tbody>
</table>

<h3>Creating Custom Fields</h3>
<p>Custom fields allow you to store business-specific data. Here are common custom field examples:</p>

<h4>Customer Fields</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Example custom customer fields
{
  ""loyalty_tier"": ""gold"",           // String
  ""total_orders"": 15,                // Number
  ""is_vip"": true,                    // Boolean
  ""last_purchase_date"": ""2024-01-15"", // Timestamp
  ""interests"": [""sports"", ""tech""]    // Array
}
</pre>

<h4>Event Fields</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Example custom event fields
{
  ""campaign_id"": ""winter_sale_2024"",
  ""discount_applied"": 15.00,
  ""payment_method"": ""credit_card"",
  ""device_type"": ""mobile""
}
</pre>

<h3>Creating Custom Objects</h3>
<p>When built-in objects don't meet your needs, you can create custom objects. Examples include:</p>

<ul>
    <li><strong>Store Locations</strong> - For multi-location businesses</li>
    <li><strong>Content Categories</strong> - For media and publishing</li>
    <li><strong>Subscription Plans</strong> - For SaaS businesses</li>
    <li><strong>Event Tickets</strong> - For entertainment venues</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Planning Tip</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Before creating custom objects and fields, map out your data requirements. Consider what data you need for segmentation, what fields are required for integrations, and what reports you'll need.</p>
</div>

<h3>Field Naming Conventions</h3>
<p>Follow these conventions for consistent field naming:</p>

<ul>
    <li>Use <strong>snake_case</strong> for field names (e.g., <code>first_name</code>, <code>last_purchase_date</code>)</li>
    <li>Keep names descriptive but concise</li>
    <li>Use prefixes for related fields (e.g., <code>shipping_address</code>, <code>shipping_city</code>)</li>
    <li>Avoid reserved words and special characters</li>
</ul>

<h3>Schema Management in the UI</h3>
<p>To manage objects and fields in the ODP interface:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Data</strong> in the main menu</li>
    <li>Select <strong>Objects</strong> to view or create objects</li>
    <li>Select <strong>Fields</strong> to add fields to existing objects</li>
    <li>Use <strong>Identifiers</strong> to manage customer identity types</li>
</ol>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ds-lists-consent",
                    ModuleId = "data-structure",
                    Title = "Lists and Consent Management",
                    Summary = "Manage subscription lists and customer consent preferences.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of lists in ODP",
                        "Learn how to manage subscription lists",
                        "Understand consent management frameworks",
                        "Know how to track and respect customer preferences"
                    },
                    Content = @"
<h2>Lists and Consent in ODP</h2>
<p>Lists help you organise customers into groups for targeted communications, while consent management ensures you respect customer preferences and comply with privacy regulations.</p>

<h3>Understanding Lists</h3>
<p>Lists in ODP are collections of customers grouped for specific purposes:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">List Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Newsletter</td>
            <td class=""px-4 py-2"">Email subscriptions</td>
            <td class=""px-4 py-2"">Weekly newsletter, product updates</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">SMS</td>
            <td class=""px-4 py-2"">Text message subscriptions</td>
            <td class=""px-4 py-2"">Promotional texts, alerts</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Preference</td>
            <td class=""px-4 py-2"">Interest-based groups</td>
            <td class=""px-4 py-2"">Sports enthusiasts, tech lovers</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Programme</td>
            <td class=""px-4 py-2"">Loyalty/membership</td>
            <td class=""px-4 py-2"">VIP members, loyalty programme</td>
        </tr>
    </tbody>
</table>

<h3>List Membership States</h3>
<p>A customer's relationship with a list can be:</p>

<ul>
    <li><strong>Subscribed</strong> - Actively opted in to the list</li>
    <li><strong>Unsubscribed</strong> - Opted out of the list</li>
    <li><strong>Unknown</strong> - No preference recorded</li>
</ul>

<h3>Managing Lists via API</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Subscribe a customer to a list
POST /v3/lists/{list_id}/subscriptions
{
  ""identifiers"": {
    ""email"": ""john@example.com""
  },
  ""status"": ""subscribed""
}

// Unsubscribe a customer
POST /v3/lists/{list_id}/subscriptions
{
  ""identifiers"": {
    ""email"": ""john@example.com""
  },
  ""status"": ""unsubscribed""
}
</pre>

<h3>Consent Management</h3>
<p>ODP provides a consent framework to track customer permissions across different channels and purposes.</p>

<h4>Consent Channels</h4>
<ul>
    <li><strong>Email</strong> - Permission to send marketing emails</li>
    <li><strong>SMS</strong> - Permission to send text messages</li>
    <li><strong>Push</strong> - Permission for web/app push notifications</li>
    <li><strong>Phone</strong> - Permission for phone calls</li>
    <li><strong>Direct Mail</strong> - Permission for postal communications</li>
</ul>

<h4>Consent Status Values</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Meaning</th>
            <th class=""px-4 py-2 text-left"">Can Contact?</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">opted_in</td>
            <td class=""px-4 py-2"">Customer gave explicit consent</td>
            <td class=""px-4 py-2"">Yes</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">opted_out</td>
            <td class=""px-4 py-2"">Customer explicitly declined</td>
            <td class=""px-4 py-2"">No</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">not_set</td>
            <td class=""px-4 py-2"">No preference recorded</td>
            <td class=""px-4 py-2"">Depends on your policy</td>
        </tr>
    </tbody>
</table>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Compliance Warning</p>
    <p class=""text-red-700 dark:text-red-300"">Under GDPR and similar regulations, you may need explicit opt-in before marketing communications. The ""not_set"" status should generally be treated as ""do not contact"" in regions with strict privacy laws.</p>
</div>

<h3>Tracking Consent via API</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Update consent preferences
POST /v3/customers
{
  ""identifiers"": {
    ""email"": ""john@example.com""
  },
  ""attributes"": {
    ""consent"": {
      ""email"": ""opted_in"",
      ""sms"": ""opted_out"",
      ""push"": ""not_set""
    }
  }
}
</pre>

<h3>Best Practices for Consent</h3>
<ul>
    <li><strong>Capture Consent Source</strong> - Record where and when consent was given</li>
    <li><strong>Provide Easy Opt-Out</strong> - Make unsubscribing simple and immediate</li>
    <li><strong>Sync Across Systems</strong> - Keep consent status consistent across platforms</li>
    <li><strong>Audit Trail</strong> - Maintain history of consent changes</li>
    <li><strong>Regular Review</strong> - Periodically verify consent data is current</li>
</ul>

<h3>Lists vs Segments</h3>
<p>While both lists and segments group customers, they serve different purposes:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Lists</th>
            <th class=""px-4 py-2 text-left"">Segments</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Membership</td>
            <td class=""px-4 py-2"">Explicit subscription</td>
            <td class=""px-4 py-2"">Based on criteria/rules</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Updates</td>
            <td class=""px-4 py-2"">Manual or API-driven</td>
            <td class=""px-4 py-2"">Automatic based on data</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Purpose</td>
            <td class=""px-4 py-2"">Communication preferences</td>
            <td class=""px-4 py-2"">Targeting and personalisation</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Example</td>
            <td class=""px-4 py-2"">Newsletter subscribers</td>
            <td class=""px-4 py-2"">High-value customers</td>
        </tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 3: Events and Tracking

    private LearningModule BuildEventsTrackingModule()
    {
        return new LearningModule
        {
            Id = "events-tracking",
            Title = "Events & Tracking",
            Description = "Master event tracking in ODP to capture customer behaviour and build rich profiles.",
            Icon = "bolt",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "et-understanding-events",
                    ModuleId = "events-tracking",
                    Title = "Understanding Events in ODP",
                    Summary = "Learn the anatomy of events and how they capture customer behaviour.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand what events are and how they work in ODP",
                        "Learn the structure of an event (type, action, fields)",
                        "Know the difference between standard and custom events",
                        "Understand event timing and data latency"
                    },
                    Content = @"
<h2>Events in ODP</h2>
<p>Events are the foundation of customer behaviour tracking in ODP. They capture <strong>what customers do, when they do it, and the context around those actions</strong>.</p>

<h3>Event Anatomy</h3>
<p>Every event in ODP consists of these core components:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                        ODP Event Structure                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  REQUIRED FIELDS                                                 │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ type: ""product""           ← Category of event           │    │
│  │ action: ""add_to_cart""     ← Specific action             │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  AUTOMATIC FIELDS (added by SDK)                                 │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ ts: ""2024-01-15T10:30:00Z"" ← Timestamp                  │    │
│  │ vuid: ""abc123...""          ← Visitor ID (cookie)        │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  OPTIONAL FIELDS (context)                                       │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ product_id: ""SKU-12345""    ← Related product            │    │
│  │ quantity: 2                 ← Custom field               │    │
│  │ page_url: ""/products/...""  ← Page context               │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
│  IDENTIFIERS (optional, for stitching)                           │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ email: ""john@example.com""  ← Links to customer profile  │    │
│  │ customer_id: ""CUST-123""    ← Your system's ID           │    │
│  └─────────────────────────────────────────────────────────┘    │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Event Types and Actions</h3>
<p>Events are classified by <strong>type</strong> (category) and <strong>action</strong> (specific behaviour):</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Event Type</th>
            <th class=""px-4 py-2 text-left"">Common Actions</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">pageview</td>
            <td class=""px-4 py-2"">(default)</td>
            <td class=""px-4 py-2"">Track page visits</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">product</td>
            <td class=""px-4 py-2"">detail, add_to_cart, remove_from_cart, add_to_wishlist</td>
            <td class=""px-4 py-2"">E-commerce interactions</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">account</td>
            <td class=""px-4 py-2"">login, logout, register, update</td>
            <td class=""px-4 py-2"">Account activities</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">customer</td>
            <td class=""px-4 py-2"">login, identified</td>
            <td class=""px-4 py-2"">Identity events</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">navigation</td>
            <td class=""px-4 py-2"">search, filter, sort</td>
            <td class=""px-4 py-2"">Site navigation</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">order</td>
            <td class=""px-4 py-2"">purchase, refund, cancel</td>
            <td class=""px-4 py-2"">Transaction events</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">email</td>
            <td class=""px-4 py-2"">sent, opened, clicked, bounced</td>
            <td class=""px-4 py-2"">Email engagement</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">custom</td>
            <td class=""px-4 py-2"">Any custom action</td>
            <td class=""px-4 py-2"">Business-specific tracking</td>
        </tr>
    </tbody>
</table>

<h3>Standard vs Custom Events</h3>
<p>ODP provides predefined event types for common scenarios, but you can also create custom events:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Standard Events</th>
            <th class=""px-4 py-2 text-left"">Custom Events</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Definition</td>
            <td class=""px-4 py-2"">Pre-built in ODP</td>
            <td class=""px-4 py-2"">You define type and action</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Fields</td>
            <td class=""px-4 py-2"">Expected fields documented</td>
            <td class=""px-4 py-2"">Any fields you need</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Integration</td>
            <td class=""px-4 py-2"">Auto-recognised by some tools</td>
            <td class=""px-4 py-2"">May need mapping</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Examples</td>
            <td class=""px-4 py-2"">pageview, product.add_to_cart</td>
            <td class=""px-4 py-2"">video.watched, form.submitted</td>
        </tr>
    </tbody>
</table>

<div class=""bg-indigo-50 dark:bg-indigo-900/30 border-l-4 border-indigo-500 p-4 my-4"">
    <p class=""font-medium text-indigo-800 dark:text-indigo-200"">Best Practice</p>
    <p class=""text-indigo-700 dark:text-indigo-300"">Use standard event types when they match your use case, as they integrate better with ODP's built-in features and reports. Use custom events for business-specific actions that don't fit standard categories.</p>
</div>

<h3>Event Data Latency</h3>
<p>Understanding data latency helps you set expectations for real-time features:</p>

<ul>
    <li><strong>Event Reception</strong> - Events are received immediately by ODP</li>
    <li><strong>Processing Pipeline</strong> - Typically takes less than 2 minutes</li>
    <li><strong>Segment Impact</strong> - Events affect segment membership after processing</li>
    <li><strong>Real-Time Segments</strong> - Use events from the last 28 days</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Note</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">The API can only classify a customer using data that has made it through the ODP ingest pipeline. If you query segment membership immediately after sending an event, the event may not yet be reflected.</p>
</div>

<h3>Event Best Practices</h3>
<ul>
    <li><strong>Be Consistent</strong> - Use the same event type/action combinations across your implementation</li>
    <li><strong>Include Context</strong> - Add relevant fields like product_id, page_url, category</li>
    <li><strong>Identify When Possible</strong> - Include email or customer_id to link events to profiles</li>
    <li><strong>Don't Over-Track</strong> - Focus on meaningful actions, not every micro-interaction</li>
    <li><strong>Document Your Schema</strong> - Maintain documentation of your event types and fields</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-standard-events",
                    ModuleId = "events-tracking",
                    Title = "Standard Event Types",
                    Summary = "Deep dive into ODP's built-in event types for common tracking scenarios.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Master pageview and navigation events",
                        "Implement product and e-commerce events",
                        "Track account and customer events",
                        "Understand when to use each event type"
                    },
                    Content = @"
<h2>Standard Event Types in Detail</h2>
<p>ODP provides several standard event types optimised for common tracking scenarios. Using these correctly ensures compatibility with ODP's features and integrations.</p>

<h3>Pageview Events</h3>
<p>The simplest event type - automatically tracked when the JavaScript tag is implemented.</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Pageview event (automatic with JS tag)
zaius.event(""pageview"");

// Pageview with additional context
zaius.event(""pageview"", {
    page_url: window.location.href,
    page_title: document.title,
    referrer: document.referrer,
    category: ""product-listing""
});
</pre>

<p>ODP automatically parses page path information from pageview events.</p>

<h3>Account Events</h3>
<p>Track user account activities with the <code>account</code> event type:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Action</th>
            <th class=""px-4 py-2 text-left"">When to Use</th>
            <th class=""px-4 py-2 text-left"">Key Fields</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">register</td>
            <td class=""px-4 py-2"">New account creation</td>
            <td class=""px-4 py-2"">email, customer_id</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">login</td>
            <td class=""px-4 py-2"">User signs in</td>
            <td class=""px-4 py-2"">email, customer_id</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">logout</td>
            <td class=""px-4 py-2"">User signs out</td>
            <td class=""px-4 py-2"">-</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">update</td>
            <td class=""px-4 py-2"">Profile updated</td>
            <td class=""px-4 py-2"">Changed fields</td>
        </tr>
    </tbody>
</table>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Login event
zaius.event(""account"", {
    action: ""login""
});

// Registration with customer identification
zaius.event(""account"", {
    action: ""register"",
    email: ""john@example.com"",
    customer_id: ""CUST-12345""
});
</pre>

<h3>Customer Identification Events</h3>
<p>Use the <code>customer</code> event type to link anonymous visitors to known identities:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Identify customer on login
zaius.event(""customer"", {
    action: ""login"",
    customer_id: ""CUST-12345"",
    email: ""john@example.com""
});

// Identify from form submission
zaius.event(""customer"", {
    action: ""identified"",
    email: ""jane@example.com""
});
</pre>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Identity Stitching</p>
    <p class=""text-blue-700 dark:text-blue-300"">When you send an event with an identifier (email, customer_id), ODP links the current VUID to that customer profile. This ""stitches"" the anonymous browsing history to the known customer.</p>
</div>

<h3>Product Events</h3>
<p>Track e-commerce product interactions with the <code>product</code> event type:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Action</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Required Fields</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">detail</td>
            <td class=""px-4 py-2"">View product details page</td>
            <td class=""px-4 py-2"">product_id</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">add_to_cart</td>
            <td class=""px-4 py-2"">Add product to cart</td>
            <td class=""px-4 py-2"">product_id</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">remove_from_cart</td>
            <td class=""px-4 py-2"">Remove from cart</td>
            <td class=""px-4 py-2"">product_id</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">add_to_wishlist</td>
            <td class=""px-4 py-2"">Add to wishlist</td>
            <td class=""px-4 py-2"">product_id</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">remove_from_wishlist</td>
            <td class=""px-4 py-2"">Remove from wishlist</td>
            <td class=""px-4 py-2"">product_id</td>
        </tr>
    </tbody>
</table>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Product detail view
zaius.event(""product"", {
    action: ""detail"",
    product_id: ""SKU-12345""
});

// Add to cart
zaius.event(""product"", {
    action: ""add_to_cart"",
    product_id: ""SKU-12345"",
    quantity: 2,
    price: 29.99
});

// Add to wishlist
zaius.event(""product"", {
    action: ""add_to_wishlist"",
    product_id: ""SKU-12345""
});
</pre>

<h3>Navigation Events</h3>
<p>Track how users navigate and search your site:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Search event
zaius.event(""navigation"", {
    action: ""search"",
    search_term: ""winter jackets"",
    category: ""outerwear"",
    results_count: 45
});

// Filter applied
zaius.event(""navigation"", {
    action: ""filter"",
    filter_type: ""price"",
    filter_value: ""50-100""
});

// Sort changed
zaius.event(""navigation"", {
    action: ""sort"",
    sort_by: ""price_low_high""
});
</pre>

<h3>Order Events (Server-Side Recommended)</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Optimizely does not recommend sending order events via the Web SDK because ad blockers can interfere with client-side tracking. Use the REST API or server-side integration for reliable order data.</p>
</div>

<p>Order events should be sent server-side via the REST API:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Order event (send via REST API, not Web SDK)
POST /v3/events
{
    ""type"": ""order"",
    ""action"": ""purchase"",
    ""identifiers"": {
        ""email"": ""john@example.com""
    },
    ""data"": {
        ""order_id"": ""ORD-98765"",
        ""total"": 149.99,
        ""currency"": ""USD"",
        ""items"": [
            { ""product_id"": ""SKU-123"", ""quantity"": 2, ""price"": 49.99 },
            { ""product_id"": ""SKU-456"", ""quantity"": 1, ""price"": 50.01 }
        ]
    }
}
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-custom-events",
                    ModuleId = "events-tracking",
                    Title = "Creating Custom Events",
                    Summary = "Design and implement custom events for business-specific tracking needs.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Know when to create custom events",
                        "Design effective custom event schemas",
                        "Implement custom events with the Web SDK",
                        "Follow naming conventions and best practices"
                    },
                    Content = @"
<h2>Custom Events in ODP</h2>
<p>When standard event types don't cover your tracking needs, you can create custom events with your own type and action combinations.</p>

<h3>When to Use Custom Events</h3>
<p>Create custom events for:</p>

<ul>
    <li><strong>Business-Specific Actions</strong> - Unique to your industry or workflow</li>
    <li><strong>Content Engagement</strong> - Video plays, document downloads, podcast listens</li>
    <li><strong>Feature Usage</strong> - Tool interactions, configurator usage, calculator submissions</li>
    <li><strong>Form Submissions</strong> - Lead forms, surveys, feedback</li>
    <li><strong>App-Specific</strong> - Mobile app actions, in-app purchases</li>
</ul>

<h3>Custom Event Format</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Basic custom event structure
zaius.event(""your_event_type"", {
    action: ""your_event_action"",
    custom_field_1: ""value1"",
    custom_field_2: ""value2""
});
</pre>

<h3>Custom Event Examples</h3>

<h4>Video Engagement</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Video started
zaius.event(""video"", {
    action: ""play"",
    video_id: ""vid-12345"",
    video_title: ""Product Demo"",
    video_duration: 180
});

// Video completed
zaius.event(""video"", {
    action: ""complete"",
    video_id: ""vid-12345"",
    watch_time: 175,
    completion_rate: 0.97
});
</pre>

<h4>Form Submissions</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Contact form submitted
zaius.event(""form"", {
    action: ""submit"",
    form_id: ""contact-us"",
    form_name: ""Contact Request"",
    source_page: window.location.pathname
});

// Newsletter signup
zaius.event(""form"", {
    action: ""subscribe"",
    form_id: ""newsletter-footer"",
    email: userEmail  // Also identifies the customer
});
</pre>

<h4>Content Downloads</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Document download
zaius.event(""content"", {
    action: ""download"",
    content_id: ""whitepaper-2024-trends"",
    content_type: ""whitepaper"",
    content_title: ""2024 Industry Trends Report"",
    file_format: ""pdf""
});
</pre>

<h4>Feature Interactions</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Product configurator used
zaius.event(""configurator"", {
    action: ""configure"",
    product_category: ""custom-furniture"",
    selections: JSON.stringify({
        material: ""oak"",
        finish: ""natural"",
        size: ""large""
    }),
    estimated_price: 899.99
});

// Calculator completed
zaius.event(""calculator"", {
    action: ""complete"",
    calculator_type: ""mortgage"",
    loan_amount: 250000,
    result_shown: true
});
</pre>

<h3>Naming Conventions</h3>
<p>Follow these conventions for consistent, maintainable event schemas:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Element</th>
            <th class=""px-4 py-2 text-left"">Convention</th>
            <th class=""px-4 py-2 text-left"">Examples</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Event Type</td>
            <td class=""px-4 py-2"">Noun, lowercase, snake_case</td>
            <td class=""px-4 py-2"">video, form, content, quiz</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Event Action</td>
            <td class=""px-4 py-2"">Verb, lowercase, snake_case</td>
            <td class=""px-4 py-2"">play, submit, download, complete</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Field Names</td>
            <td class=""px-4 py-2"">Descriptive, lowercase, snake_case</td>
            <td class=""px-4 py-2"">video_id, completion_rate</td>
        </tr>
    </tbody>
</table>

<h3>Schema Design Best Practices</h3>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Do</p>
    <ul class=""mt-2 text-green-700 dark:text-green-300 space-y-1"">
        <li>✓ Plan your event schema before implementing</li>
        <li>✓ Be consistent with naming across all events</li>
        <li>✓ Include context fields (IDs, categories, values)</li>
        <li>✓ Document your custom events</li>
        <li>✓ Start simple and add fields as needed</li>
    </ul>
</div>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Avoid</p>
    <ul class=""mt-2 text-red-700 dark:text-red-300 space-y-1"">
        <li>✗ Creating too many granular event types</li>
        <li>✗ Including sensitive data (passwords, full card numbers)</li>
        <li>✗ Using inconsistent naming conventions</li>
        <li>✗ Sending high-volume events (mouse moves, scrolls)</li>
        <li>✗ Changing event schemas frequently</li>
    </ul>
</div>

<h3>Testing Custom Events</h3>
<p>Verify your custom events are working correctly:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Use browser developer tools to monitor network requests to ODP</li>
    <li>Check the ODP interface for incoming events</li>
    <li>Test in your test environment before production</li>
    <li>Verify fields appear as expected in customer profiles</li>
</ol>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "et-event-implementation",
                    ModuleId = "events-tracking",
                    Title = "Event Implementation Strategies",
                    Summary = "Learn patterns for implementing robust event tracking across your site.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Implement event tracking for common scenarios",
                        "Handle Single Page Applications (SPAs)",
                        "Manage consent and event gating",
                        "Debug and validate event tracking"
                    },
                    Content = @"
<h2>Event Implementation Patterns</h2>
<p>Successful event tracking requires careful implementation. This lesson covers common patterns and solutions for real-world scenarios.</p>

<h3>Basic Implementation Pattern</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Ensure SDK is loaded before sending events
if (typeof zaius !== 'undefined') {
    zaius.event(""pageview"");
} else {
    // Queue events until SDK loads
    window.zaiusQueue = window.zaiusQueue || [];
    window.zaiusQueue.push([""event"", ""pageview""]);
}
</pre>

<h3>Click Event Tracking</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Track button clicks
document.querySelectorAll('[data-track-click]').forEach(element => {
    element.addEventListener('click', function() {
        const eventType = this.dataset.eventType || 'interaction';
        const eventAction = this.dataset.eventAction || 'click';
        const eventData = JSON.parse(this.dataset.eventData || '{}');

        zaius.event(eventType, {
            action: eventAction,
            ...eventData,
            element_text: this.textContent.trim(),
            page_url: window.location.href
        });
    });
});
</pre>

<h4>HTML Usage</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
&lt;button
    data-track-click
    data-event-type=""cta""
    data-event-action=""click""
    data-event-data='{""cta_name"": ""signup_hero""}'&gt;
    Sign Up Now
&lt;/button&gt;
</pre>

<h3>Single Page Application (SPA) Tracking</h3>
<p>SPAs require special handling since page loads don't trigger traditional pageview events:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// React Router example
import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';

function usePageTracking() {
    const location = useLocation();

    useEffect(() => {
        // Track virtual pageview on route change
        if (typeof zaius !== 'undefined') {
            zaius.event(""pageview"", {
                page_url: window.location.href,
                page_path: location.pathname,
                page_title: document.title
            });
        }
    }, [location]);
}
</pre>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Vue Router example
router.afterEach((to, from) => {
    if (typeof zaius !== 'undefined') {
        zaius.event(""pageview"", {
            page_url: window.location.href,
            page_path: to.path,
            page_title: to.meta.title || document.title
        });
    }
});
</pre>

<h3>Consent-Gated Tracking</h3>
<p>Respect user consent before tracking:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Check consent before tracking
function trackEvent(type, data) {
    // Check if user has consented to analytics
    if (!hasAnalyticsConsent()) {
        console.log('Tracking blocked - no consent');
        return;
    }

    if (typeof zaius !== 'undefined') {
        zaius.event(type, data);
    }
}

function hasAnalyticsConsent() {
    // Example: Check your consent management platform
    return window.CookieConsent?.accepted?.analytics === true;
}
</pre>

<h3>E-commerce Data Layer Integration</h3>
<p>If you use Google Tag Manager's data layer, you can integrate with ODP:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Listen to data layer events
window.dataLayer = window.dataLayer || [];
const originalPush = window.dataLayer.push;

window.dataLayer.push = function(...args) {
    const result = originalPush.apply(this, args);

    args.forEach(data => {
        if (data.event === 'add_to_cart') {
            zaius.event(""product"", {
                action: ""add_to_cart"",
                product_id: data.ecommerce.items[0].item_id,
                product_name: data.ecommerce.items[0].item_name,
                price: data.ecommerce.items[0].price
            });
        }
    });

    return result;
};
</pre>

<h3>Form Tracking Pattern</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Track form submissions
document.querySelectorAll('form[data-track-submit]').forEach(form => {
    form.addEventListener('submit', function(e) {
        const formId = this.id || this.dataset.formId;
        const formData = new FormData(this);

        // Don't include password or sensitive fields
        const trackData = {};
        for (let [key, value] of formData.entries()) {
            if (!key.includes('password') && !key.includes('card')) {
                trackData[key] = value;
            }
        }

        zaius.event(""form"", {
            action: ""submit"",
            form_id: formId,
            // Include email for identification if present
            email: formData.get('email'),
            fields_submitted: Object.keys(trackData)
        });
    });
});
</pre>

<h3>Debugging Events</h3>
<p>Use these techniques to validate your implementation:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Enable debug mode
localStorage.setItem('zaius_debug', 'true');

// Log all outgoing events
const originalEvent = zaius.event;
zaius.event = function(type, data) {
    console.log('[ODP Event]', type, data);
    return originalEvent.apply(this, arguments);
};
</pre>

<h3>Checklist for Event Implementation</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>☐ JavaScript tag is loading correctly</li>
        <li>☐ Events fire at the right time (not too early, not too late)</li>
        <li>☐ Required fields are populated</li>
        <li>☐ Identifiers are included when available</li>
        <li>☐ SPA navigation triggers virtual pageviews</li>
        <li>☐ Consent is checked before tracking</li>
        <li>☐ Events appear in ODP's test environment</li>
        <li>☐ No sensitive data is being sent</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 4: Web SDK Implementation

    private LearningModule BuildWebSdkModule()
    {
        return new LearningModule
        {
            Id = "web-sdk",
            Title = "Web SDK Implementation",
            Description = "Implement the ODP Web SDK (zaius.js) to track website visitors and collect behavioural data.",
            Icon = "code-bracket",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ws-javascript-tag",
                    ModuleId = "web-sdk",
                    Title = "Implementing the JavaScript Tag",
                    Summary = "Install and configure the ODP JavaScript tracking tag on your website.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Implement the ODP JavaScript tag correctly",
                        "Understand the different installation methods",
                        "Configure the tag for your environment",
                        "Verify the tag is working"
                    },
                    Content = @"
<h2>The ODP JavaScript Tag</h2>
<p>The ODP JavaScript tag (zaius.js) is the foundation of website tracking. It collects visitor data, manages cookies, and sends events to ODP.</p>

<h3>Basic Installation</h3>
<p>Add the ODP JavaScript tag before the closing <code>&lt;/head&gt;</code> tag:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
&lt;script type=""text/javascript""&gt;
    // ODP JavaScript Tag
    var zaius = window['zaius'] || (window['zaius'] = []);
    zaius.methods = ['initialize', 'onload', 'customer', 'entity', 'event', 'subscribe', 'unsubscribe', 'consent', 'identify', 'anonymize'];
    zaius.factory = function(e) {
        return function() {
            var t = Array.prototype.slice.call(arguments);
            t.unshift(e);
            zaius.push(t);
            return zaius;
        };
    };
    for (var i = 0; i &lt; zaius.methods.length; i++) {
        var method = zaius.methods[i];
        zaius[method] = zaius.factory(method);
    }
    zaius.load = function(trackerId, options) {
        zaius.tid = trackerId;
        var script = document.createElement('script');
        script.type = 'text/javascript';
        script.async = true;
        script.src = 'https://d1igp3oop3iho5.cloudfront.net/v2/' + trackerId + '/zaius-min.js';
        var firstScript = document.getElementsByTagName('script')[0];
        firstScript.parentNode.insertBefore(script, firstScript);
    };

    // Initialize with your Tracker ID
    zaius.load('YOUR_TRACKER_ID');
&lt;/script&gt;
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Replace <code>YOUR_TRACKER_ID</code> with your actual ODP Tracker ID (Public API Key). Use your test Tracker ID during development and your production Tracker ID for live sites.</p>
</div>

<h3>Installation Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
            <th class=""px-4 py-2 text-left"">Pros/Cons</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Direct HTML</td>
            <td class=""px-4 py-2"">Simple websites</td>
            <td class=""px-4 py-2"">Simple, fast loading; requires code changes to update</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Google Tag Manager</td>
            <td class=""px-4 py-2"">Marketing-managed sites</td>
            <td class=""px-4 py-2"">Easy updates; adds GTM dependency</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">CMS Integration</td>
            <td class=""px-4 py-2"">Optimizely CMS users</td>
            <td class=""px-4 py-2"">Best integration; requires NuGet package</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Consent Platform</td>
            <td class=""px-4 py-2"">GDPR compliance</td>
            <td class=""px-4 py-2"">Consent-gated; may delay tracking</td>
        </tr>
    </tbody>
</table>

<h3>Google Tag Manager Installation</h3>
<p>To install via GTM:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Create a new <strong>Custom HTML</strong> tag</li>
    <li>Paste the JavaScript tag code (without the &lt;script&gt; tags)</li>
    <li>Set trigger to fire on <strong>All Pages</strong> (or consent-gated trigger)</li>
    <li>Test using GTM Preview mode</li>
    <li>Publish the container</li>
</ol>

<h3>What the Tag Does</h3>
<p>Once loaded, the JavaScript tag automatically:</p>

<ul>
    <li><strong>Creates Cookies</strong> - Sets the VUID cookie to identify visitors</li>
    <li><strong>Tracks Pageviews</strong> - Sends pageview events by default</li>
    <li><strong>Enables SDK Methods</strong> - Makes <code>zaius.event()</code> and other methods available</li>
    <li><strong>Handles Queuing</strong> - Queues events sent before the SDK fully loads</li>
</ul>

<h3>Verifying Installation</h3>
<p>To verify the tag is working:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Open browser Developer Tools (F12)</li>
    <li>Go to the <strong>Network</strong> tab</li>
    <li>Filter by ""zaius"" or ""api.zaius.com""</li>
    <li>Refresh the page</li>
    <li>Look for successful requests (200 status)</li>
</ol>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Check in browser console
console.log(typeof zaius);  // Should output 'object' or 'function'
console.log(zaius.tid);     // Should output your Tracker ID
</pre>

<h3>Cookie Configuration</h3>
<p>The ODP SDK sets these cookies:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cookie</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Duration</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-mono"">vuid</td>
            <td class=""px-4 py-2"">Visitor unique identifier</td>
            <td class=""px-4 py-2"">2 years</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">zai_*</td>
            <td class=""px-4 py-2"">Session and tracking data</td>
            <td class=""px-4 py-2"">Session/Varies</td>
        </tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ws-sdk-methods",
                    ModuleId = "web-sdk",
                    Title = "Web SDK Methods and API",
                    Summary = "Master the zaius.event() and other SDK methods for tracking.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Use zaius.event() to track events",
                        "Identify customers with zaius.identify()",
                        "Manage customer attributes",
                        "Use advanced SDK methods"
                    },
                    Content = @"
<h2>Web SDK Methods</h2>
<p>The ODP Web SDK provides several methods for tracking events, identifying customers, and managing consent.</p>

<h3>zaius.event() - Track Events</h3>
<p>The primary method for sending event data to ODP:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Basic syntax
zaius.event(type, data);

// Pageview (simplest form)
zaius.event(""pageview"");

// Event with data
zaius.event(""product"", {
    action: ""add_to_cart"",
    product_id: ""SKU-12345"",
    quantity: 2,
    price: 49.99
});

// With customer identification
zaius.event(""customer"", {
    action: ""login"",
    email: ""john@example.com"",
    customer_id: ""CUST-123""
});
</pre>

<h3>zaius.identify() - Identify Customers</h3>
<p>Link the current visitor to a customer identity:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Identify by email
zaius.identify({
    email: ""john@example.com""
});

// Identify with multiple identifiers
zaius.identify({
    email: ""john@example.com"",
    customer_id: ""CUST-12345"",
    phone: ""+1-555-123-4567""
});
</pre>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">When to Identify</p>
    <p class=""text-blue-700 dark:text-blue-300"">Call <code>zaius.identify()</code> when you know who the visitor is - after login, form submission, or checkout. This links their anonymous browsing history to their customer profile.</p>
</div>

<h3>zaius.customer() - Update Customer Attributes</h3>
<p>Update attributes on the customer profile:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Update customer attributes
zaius.customer({
    email: ""john@example.com"",
    first_name: ""John"",
    last_name: ""Doe"",
    city: ""London"",
    loyalty_tier: ""gold""
});
</pre>

<h3>zaius.consent() - Manage Consent</h3>
<p>Track customer consent preferences:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Set consent preferences
zaius.consent({
    email: ""opted_in"",
    sms: ""opted_out"",
    push: ""not_set""
});

// Combined with identification
zaius.consent({
    identifiers: {
        email: ""john@example.com""
    },
    email: ""opted_in""
});
</pre>

<h3>zaius.subscribe() / zaius.unsubscribe() - List Management</h3>
<p>Manage subscription list membership:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Subscribe to a list
zaius.subscribe({
    list_id: ""newsletter"",
    email: ""john@example.com""
});

// Unsubscribe from a list
zaius.unsubscribe({
    list_id: ""newsletter"",
    email: ""john@example.com""
});
</pre>

<h3>zaius.anonymize() - Remove Identity</h3>
<p>Clear the current visitor's identity (e.g., on logout):</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Clear identity on logout
function handleLogout() {
    zaius.anonymize();
    // Continue with logout...
}
</pre>

<h3>zaius.onload() - SDK Ready Callback</h3>
<p>Execute code when the SDK is fully loaded:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Wait for SDK to be ready
zaius.onload(function() {
    console.log('ODP SDK loaded');
    // Safe to call SDK methods here

    // Example: Get visitor ID
    var vuid = zaius.VUID;
    console.log('Visitor ID:', vuid);
});
</pre>

<h3>Method Reference Table</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">When to Use</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-mono"">event()</td>
            <td class=""px-4 py-2"">Track events</td>
            <td class=""px-4 py-2"">Any tracked action</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">identify()</td>
            <td class=""px-4 py-2"">Set customer identity</td>
            <td class=""px-4 py-2"">Login, form submission</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">customer()</td>
            <td class=""px-4 py-2"">Update attributes</td>
            <td class=""px-4 py-2"">Profile updates</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">consent()</td>
            <td class=""px-4 py-2"">Set consent status</td>
            <td class=""px-4 py-2"">Consent changes</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">subscribe()</td>
            <td class=""px-4 py-2"">Add to list</td>
            <td class=""px-4 py-2"">Newsletter signup</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">unsubscribe()</td>
            <td class=""px-4 py-2"">Remove from list</td>
            <td class=""px-4 py-2"">Unsubscribe requests</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">anonymize()</td>
            <td class=""px-4 py-2"">Clear identity</td>
            <td class=""px-4 py-2"">Logout</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">onload()</td>
            <td class=""px-4 py-2"">SDK ready callback</td>
            <td class=""px-4 py-2"">Initialisation code</td>
        </tr>
    </tbody>
</table>

<h3>Common Patterns</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Complete login flow
function onUserLogin(user) {
    // Identify the customer
    zaius.identify({
        email: user.email,
        customer_id: user.id
    });

    // Update attributes
    zaius.customer({
        email: user.email,
        first_name: user.firstName,
        last_name: user.lastName
    });

    // Track the login event
    zaius.event(""account"", {
        action: ""login""
    });
}
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ws-advanced-configuration",
                    ModuleId = "web-sdk",
                    Title = "Advanced SDK Configuration",
                    Summary = "Configure the SDK for specific requirements and edge cases.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure SDK options",
                        "Handle consent management integration",
                        "Work with cross-domain tracking",
                        "Troubleshoot common issues"
                    },
                    Content = @"
<h2>Advanced SDK Configuration</h2>
<p>The ODP Web SDK supports various configuration options for complex implementation scenarios.</p>

<h3>Initialisation Options</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// SDK with options
zaius.load('YOUR_TRACKER_ID', {
    // Disable automatic pageview tracking
    autoPageview: false,

    // Set cookie domain (for subdomains)
    cookieDomain: '.example.com',

    // Set cookie path
    cookiePath: '/'
});
</pre>

<h3>Consent Management Integration</h3>
<p>Integrate ODP with your consent management platform:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// OneTrust integration example
window.OptanonWrapper = function() {
    // Check if analytics consent is given
    if (OnetrustActiveGroups.includes('C0002')) {
        // Load ODP only after consent
        zaius.load('YOUR_TRACKER_ID');
    }
};

// Custom consent manager
function initTrackingWithConsent() {
    // Check consent status
    const hasConsent = checkAnalyticsConsent();

    if (hasConsent) {
        zaius.load('YOUR_TRACKER_ID');
    } else {
        // Listen for consent changes
        document.addEventListener('consentUpdated', function(e) {
            if (e.detail.analytics) {
                zaius.load('YOUR_TRACKER_ID');
            }
        });
    }
}
</pre>

<h3>Delayed Initialisation</h3>
<p>Queue events before the SDK loads:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Events are automatically queued
var zaius = window['zaius'] || (window['zaius'] = []);

// Queue events before SDK loads
zaius.push(['event', 'pageview']);
zaius.push(['identify', { email: 'john@example.com' }]);

// SDK will process queued events when loaded
// zaius.load('YOUR_TRACKER_ID');  // Called later
</pre>

<h3>Cross-Domain Tracking</h3>
<p>Share visitor identity across domains:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// On source domain
function getTrackingParams() {
    var params = new URLSearchParams();
    if (zaius.VUID) {
        params.set('_zvuid', zaius.VUID);
    }
    return params.toString();
}

// Add to cross-domain links
var link = 'https://shop.example.com/checkout';
link += '?' + getTrackingParams();

// On destination domain
// SDK will read _zvuid from URL if present
</pre>

<h3>Subdomain Cookie Sharing</h3>
<p>To share cookies across subdomains (www, shop, blog):</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
zaius.load('YOUR_TRACKER_ID', {
    cookieDomain: '.example.com'  // Note the leading dot
});
</pre>

<h3>Disabling Automatic Pageviews</h3>
<p>For SPAs or custom pageview tracking:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Disable automatic pageviews
zaius.load('YOUR_TRACKER_ID', {
    autoPageview: false
});

// Manually track pageviews
function trackPageview() {
    zaius.event(""pageview"", {
        page_url: window.location.href,
        page_title: document.title
    });
}

// Call on route changes (SPA)
trackPageview();
</pre>

<h3>Troubleshooting</h3>
<h4>Common Issues and Solutions</h4>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Possible Cause</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Events not sending</td>
            <td class=""px-4 py-2"">Ad blocker</td>
            <td class=""px-4 py-2"">Test without ad blocker; use server-side for critical events</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">zaius is undefined</td>
            <td class=""px-4 py-2"">SDK not loaded</td>
            <td class=""px-4 py-2"">Check script placement; use zaius.onload()</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">VUID changing</td>
            <td class=""px-4 py-2"">Cookie issues</td>
            <td class=""px-4 py-2"">Check cookie domain; verify HTTPS</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Events in wrong account</td>
            <td class=""px-4 py-2"">Wrong Tracker ID</td>
            <td class=""px-4 py-2"">Verify Tracker ID matches environment</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">No customers appearing</td>
            <td class=""px-4 py-2"">Not identified</td>
            <td class=""px-4 py-2"">Call identify() with email/customer_id</td>
        </tr>
    </tbody>
</table>

<h4>Debug Mode</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Enable debug logging
localStorage.setItem('zaius_debug', 'true');

// Reload page to see debug output in console
location.reload();

// Disable debug mode
localStorage.removeItem('zaius_debug');
</pre>

<h4>Network Inspection</h4>
<p>Use browser DevTools to inspect ODP requests:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Open DevTools (F12)</li>
    <li>Go to Network tab</li>
    <li>Filter by ""zaius"" or ""api.zaius""</li>
    <li>Click on requests to see payload details</li>
    <li>Check for 200 status codes (success)</li>
</ol>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 5: Segments and Audiences

    private LearningModule BuildSegmentsModule()
    {
        return new LearningModule
        {
            Id = "segments",
            Title = "Segments & Audiences",
            Description = "Create and manage customer segments for targeted personalisation and marketing.",
            Icon = "user-group",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "seg-understanding-segments",
                    ModuleId = "segments",
                    Title = "Understanding Segments in ODP",
                    Summary = "Learn the fundamentals of segmentation and how ODP categorises customers.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand what segments are and their purpose",
                        "Know the difference between standard and real-time segments",
                        "Learn about predictive segments",
                        "Understand segment activation options"
                    },
                    Content = @"
<h2>Segmentation in ODP</h2>
<p>Segmentation is the process of grouping customers based on shared characteristics or behaviours. In ODP, segments (also called audiences) are dynamic groups that update automatically as customer data changes.</p>

<div class=""bg-indigo-50 dark:bg-indigo-900/30 border-l-4 border-indigo-500 p-4 my-4"">
    <p class=""font-medium text-indigo-800 dark:text-indigo-200"">Terminology Note</p>
    <p class=""text-indigo-700 dark:text-indigo-300"">The terms ""audiences"" and ""segments"" are interchangeable in ODP. Optimizely is updating the UI to display all segments as audiences.</p>
</div>

<h3>Types of Segments</h3>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Data Window</th>
            <th class=""px-4 py-2 text-left"">Update Speed</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Standard Segments</td>
            <td class=""px-4 py-2"">Historical (no limit)</td>
            <td class=""px-4 py-2"">Batch (daily/scheduled)</td>
            <td class=""px-4 py-2"">Long-term customer groups</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Real-Time Segments</td>
            <td class=""px-4 py-2"">Last 28 days</td>
            <td class=""px-4 py-2"">Near real-time (< 90 sec)</td>
            <td class=""px-4 py-2"">Current behaviour targeting</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Predictive Segments</td>
            <td class=""px-4 py-2"">ML-generated</td>
            <td class=""px-4 py-2"">Model refresh schedule</td>
            <td class=""px-4 py-2"">Propensity scoring</td>
        </tr>
    </tbody>
</table>

<h3>Standard Segments</h3>
<p>Standard segments use historical data with no time restrictions. They're ideal for:</p>

<ul>
    <li><strong>Customer Lifecycle</strong> - New customers, loyal customers, churned</li>
    <li><strong>Purchase History</strong> - High spenders, category buyers, one-time purchasers</li>
    <li><strong>Demographics</strong> - Location, company size, industry</li>
    <li><strong>Engagement</strong> - Active users, dormant users</li>
</ul>

<h3>Real-Time Segments</h3>
<p>Real-time segments use events from the last 28 days and update within 90 seconds. Perfect for:</p>

<ul>
    <li><strong>Current Intent</strong> - Browsing specific products, in checkout</li>
    <li><strong>Recent Actions</strong> - Abandoned cart today, viewed pricing page</li>
    <li><strong>Live Personalisation</strong> - On-site content targeting</li>
    <li><strong>Triggered Campaigns</strong> - Real-time email triggers</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Real-Time Freshness</p>
    <p class=""text-blue-700 dark:text-blue-300"">Real-time segments reflect customer behaviour within 90 seconds. This allows for immediate personalisation based on current browsing activity.</p>
</div>

<h3>Predictive Segments</h3>
<p>ODP uses machine learning to automatically create predictive segments:</p>

<ul>
    <li><strong>Propensity to Buy</strong> - Likelihood of making a purchase</li>
    <li><strong>Churn Risk</strong> - Likelihood of becoming inactive</li>
    <li><strong>Customer Lifetime Value</strong> - Predicted future value</li>
    <li><strong>Next Best Action</strong> - Recommended engagement</li>
</ul>

<h3>Segment Activation</h3>
<p>Once created, segments can be activated across multiple channels:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    Segment Activation Channels                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  OPTIMIZELY PRODUCTS                                             │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │   CMS        │  │    Web       │  │   Feature    │          │
│  │  Visitor     │  │Experimentation│ │Experimentation│         │
│  │   Groups     │  │   Audiences  │  │   Audiences  │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
│                                                                  │
│  EXTERNAL CHANNELS                                               │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │  Google Ads  │  │   Facebook   │  │    Email     │          │
│  │  Audiences   │  │   Audiences  │  │  Platforms   │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
│                                                                  │
│  API ACCESS                                                      │
│  ┌──────────────┐  ┌──────────────┐                             │
│  │   GraphQL    │  │   REST API   │                             │
│  │   Queries    │  │   Exports    │                             │
│  └──────────────┘  └──────────────┘                             │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Segment Use Cases</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Use Case</th>
            <th class=""px-4 py-2 text-left"">Segment Type</th>
            <th class=""px-4 py-2 text-left"">Example Definition</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Welcome Campaign</td>
            <td class=""px-4 py-2"">Standard</td>
            <td class=""px-4 py-2"">First purchase in last 30 days</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Cart Abandonment</td>
            <td class=""px-4 py-2"">Real-Time</td>
            <td class=""px-4 py-2"">Added to cart, no purchase in 1 hour</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Win-Back</td>
            <td class=""px-4 py-2"">Predictive</td>
            <td class=""px-4 py-2"">High churn risk + previous purchaser</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">VIP Treatment</td>
            <td class=""px-4 py-2"">Standard</td>
            <td class=""px-4 py-2"">Lifetime value > £1000</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Browse Retargeting</td>
            <td class=""px-4 py-2"">Real-Time</td>
            <td class=""px-4 py-2"">Viewed product category today</td>
        </tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "seg-creating-segments",
                    ModuleId = "segments",
                    Title = "Creating Segments",
                    Summary = "Build standard and real-time segments using the ODP interface.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create standard segments in ODP",
                        "Build real-time segments",
                        "Understand segment criteria and conditions",
                        "Test and validate segment membership"
                    },
                    Content = @"
<h2>Creating Segments in ODP</h2>
<p>ODP provides a visual segment builder and GraphQL-based definitions for creating both standard and real-time segments.</p>

<h3>Creating Standard Segments</h3>
<p>Navigate to <strong>Customers → Standard Segments</strong> in the ODP interface:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Create Segment</strong></li>
    <li>Enter a segment name and description</li>
    <li>Define your criteria using the visual builder</li>
    <li>Preview the estimated audience size</li>
    <li>Save the segment</li>
</ol>

<h4>Segment Criteria Types</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Criteria Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Customer Attributes</td>
            <td class=""px-4 py-2"">Profile field values</td>
            <td class=""px-4 py-2"">city = 'London'</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Event History</td>
            <td class=""px-4 py-2"">Actions taken</td>
            <td class=""px-4 py-2"">purchased in last 90 days</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Aggregations</td>
            <td class=""px-4 py-2"">Calculated metrics</td>
            <td class=""px-4 py-2"">total_orders > 5</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">List Membership</td>
            <td class=""px-4 py-2"">Subscription status</td>
            <td class=""px-4 py-2"">subscribed to newsletter</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Segment Membership</td>
            <td class=""px-4 py-2"">Other segments</td>
            <td class=""px-4 py-2"">member of 'VIP Customers'</td>
        </tr>
    </tbody>
</table>

<h3>Creating Real-Time Segments</h3>
<p>Navigate to <strong>Customers → Real-Time Segments</strong>:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Click <strong>Create Real-Time Segment</strong></li>
    <li>Enter a segment ID (used in API calls)</li>
    <li>Add a description</li>
    <li>Define the segment using GraphQL syntax</li>
    <li>Save and activate</li>
</ol>

<h4>Real-Time Segment Definition Example</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# Abandoned Cart Segment
# Customers who added to cart but didn't purchase in 2 hours

{
  ""conditions"": [{
    ""type"": ""customer"",
    ""path"": [""events""],
    ""filter"": {
      ""type"": ""product"",
      ""action"": ""add_to_cart""
    },
    ""time_window"": ""2h"",
    ""has"": true
  }, {
    ""type"": ""customer"",
    ""path"": [""events""],
    ""filter"": {
      ""type"": ""order"",
      ""action"": ""purchase""
    },
    ""time_window"": ""2h"",
    ""has"": false
  }]
}
</pre>

<h3>Segment Operators</h3>
<p>Use these operators when building segment criteria:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Operator</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Data Types</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-mono"">equals</td>
            <td class=""px-4 py-2"">Exact match</td>
            <td class=""px-4 py-2"">String, Number</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">not_equals</td>
            <td class=""px-4 py-2"">Not equal to</td>
            <td class=""px-4 py-2"">String, Number</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">contains</td>
            <td class=""px-4 py-2"">Substring match</td>
            <td class=""px-4 py-2"">String</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">greater_than</td>
            <td class=""px-4 py-2"">Greater than value</td>
            <td class=""px-4 py-2"">Number, Date</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">less_than</td>
            <td class=""px-4 py-2"">Less than value</td>
            <td class=""px-4 py-2"">Number, Date</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">in</td>
            <td class=""px-4 py-2"">Value in list</td>
            <td class=""px-4 py-2"">String, Number</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">is_set</td>
            <td class=""px-4 py-2"">Field has a value</td>
            <td class=""px-4 py-2"">Any</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">is_not_set</td>
            <td class=""px-4 py-2"">Field is empty</td>
            <td class=""px-4 py-2"">Any</td>
        </tr>
    </tbody>
</table>

<h3>Combining Conditions</h3>
<p>Use AND/OR logic to combine multiple criteria:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# High-Value UK Customers
# (lifetime_value > 1000) AND (country = 'UK')

AND:
  - lifetime_value > 1000
  - country equals 'UK'

# Engaged or Recent Buyer
# (email_opened in last 7 days) OR (purchased in last 30 days)

OR:
  - email_opened in last 7 days
  - order.purchase in last 30 days
</pre>

<h3>Testing Segment Membership</h3>
<p>After creating a segment, verify it's working:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Customers → Profiles</strong></li>
    <li>Search for a customer you expect to be in the segment</li>
    <li>View their profile to see segment memberships</li>
    <li>For real-time segments, check the GraphQL API response</li>
</ol>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Best Practice</p>
    <p class=""text-green-700 dark:text-green-300"">Start with a broad segment definition and refine it. Preview the audience size to ensure your criteria aren't too restrictive.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "seg-realtime-api",
                    ModuleId = "segments",
                    Title = "Real-Time Segments API",
                    Summary = "Query and manage real-time segments programmatically.",
                    Order = 3,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Query segment membership via GraphQL",
                        "Create segments via the REST API",
                        "Understand API authentication and endpoints",
                        "Implement real-time personalisation"
                    },
                    Content = @"
<h2>Real-Time Segments API</h2>
<p>ODP provides APIs for querying segment membership and managing segment definitions programmatically.</p>

<h3>Querying Segment Membership</h3>
<p>Use the GraphQL API to check if a customer belongs to specific segments:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# GraphQL Query - Check segment membership
query {
  customer(vuid: ""abc123-def456-..."") {
    audiences {
      edges {
        node {
          name
          state
        }
      }
    }
  }
}

# Response
{
  ""data"": {
    ""customer"": {
      ""audiences"": {
        ""edges"": [
          { ""node"": { ""name"": ""high_value_customers"", ""state"": ""qualified"" } },
          { ""node"": { ""name"": ""abandoned_cart"", ""state"": ""qualified"" } }
        ]
      }
    }
  }
}
</pre>

<h3>Authentication</h3>
<p>API requests require your Private API Key in the header:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# HTTP Headers
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

# Endpoint (US)
POST https://api.zaius.com/v3/graphql
</pre>

<h3>Segments REST API</h3>
<p>Create and manage segments programmatically:</p>

<h4>List All Segments</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
GET /v3/segments
x-api-key: YOUR_PRIVATE_API_KEY

# Response
{
  ""segment_ids"": [
    ""high_value_customers"",
    ""abandoned_cart"",
    ""newsletter_subscribers""
  ]
}
</pre>

<h4>Get Segment Definition</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
GET /v3/segments/abandoned_cart
x-api-key: YOUR_PRIVATE_API_KEY

# Response
{
  ""segment_id"": ""abandoned_cart"",
  ""revision"": 3,
  ""description"": ""Customers with abandoned carts"",
  ""definition"": { ... }
}
</pre>

<h4>Create a Segment</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
PUT /v3/segments/new_segment_id
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""description"": ""High intent buyers"",
  ""definition"": {
    ""conditions"": [{
      ""type"": ""customer"",
      ""path"": [""events""],
      ""filter"": {
        ""type"": ""product"",
        ""action"": ""add_to_cart""
      },
      ""time_window"": ""24h"",
      ""has"": true
    }]
  }
}
</pre>

<h4>Delete a Segment</h4>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
DELETE /v3/segments/segment_id
x-api-key: YOUR_PRIVATE_API_KEY

# Returns remaining segment IDs
</pre>

<h3>Real-Time Personalisation Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│              Real-Time Personalisation Flow                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  1. VISITOR ARRIVES                                              │
│     Browser has VUID cookie from ODP SDK                         │
│                              │                                   │
│                              ▼                                   │
│  2. QUERY SEGMENTS                                               │
│     Server calls GraphQL with VUID                               │
│     ┌─────────────────────────────────┐                         │
│     │ POST /v3/graphql                │                         │
│     │ { customer(vuid: ""..."") { ... }│                         │
│     └─────────────────────────────────┘                         │
│                              │                                   │
│                              ▼                                   │
│  3. RECEIVE SEGMENTS                                             │
│     [""high_value"", ""product_interest_tech""]                    │
│                              │                                   │
│                              ▼                                   │
│  4. PERSONALISE CONTENT                                          │
│     Show VIP banner, tech product recommendations                │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Server-Side Implementation Example</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// C# Example - Query segment membership
public async Task&lt;List&lt;string&gt;&gt; GetCustomerSegments(string vuid)
{
    var query = @""
        query {
            customer(vuid: \"""""" + vuid + @"""""") {
                audiences {
                    edges {
                        node { name state }
                    }
                }
            }
        }
    "";

    using var client = new HttpClient();
    client.DefaultRequestHeaders.Add(""x-api-key"", _privateApiKey);

    var content = new StringContent(
        JsonSerializer.Serialize(new { query }),
        Encoding.UTF8,
        ""application/json""
    );

    var response = await client.PostAsync(
        ""https://api.zaius.com/v3/graphql"",
        content
    );

    var result = await response.Content.ReadAsStringAsync();
    // Parse and return segment names
}
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Data Latency Note</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">The API can only classify a customer using data that has made it through the ODP ingest pipeline. Typically, it takes a few seconds for an event sent to ODP to impact segment membership.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "seg-activation",
                    ModuleId = "segments",
                    Title = "Segment Activation",
                    Summary = "Activate segments across Optimizely products and external channels.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Activate segments in Optimizely CMS",
                        "Use segments in experimentation",
                        "Export segments to external platforms",
                        "Build omnichannel personalisation"
                    },
                    Content = @"
<h2>Activating Segments</h2>
<p>ODP segments become powerful when activated across your marketing and personalisation stack.</p>

<h3>Optimizely CMS Integration</h3>
<p>Link ODP real-time segments to CMS Visitor Groups for content personalisation:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Configure ODP integration in CMS (NuGet package)</li>
    <li>Create a Visitor Group in CMS</li>
    <li>Add the ""ODP Segment"" criterion</li>
    <li>Select your ODP segment</li>
    <li>Use the Visitor Group to personalise content</li>
</ol>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Real-Time Capability</p>
    <p class=""text-blue-700 dark:text-blue-300"">ODP real-time segments enable personalisation with data freshness of less than 90 seconds - far more dynamic than traditional visitor groups based on session data.</p>
</div>

<h3>Web Experimentation</h3>
<p>Target A/B tests to specific ODP segments:</p>

<ol class=""list-decimal list-inside space-y-2"">
    <li>Create your experiment in Optimizely Web</li>
    <li>Go to Audiences section</li>
    <li>Add ODP audience targeting</li>
    <li>Select the segment(s) to target</li>
    <li>Run the experiment</li>
</ol>

<h3>Feature Experimentation</h3>
<p>Use ODP segments for feature flag targeting:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// JavaScript SDK example
const user = optimizelyClient.createUserContext('user123', {
    // ODP attributes passed to Feature Experimentation
    odp_segment_qualified: ['high_value', 'tech_interest']
});

const decision = user.decide('new_checkout_flow');

if (decision.enabled) {
    // Show new checkout to high-value tech users
}
</pre>

<h3>External Platform Export</h3>
<p>Export segments to external marketing platforms:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Platform</th>
            <th class=""px-4 py-2 text-left"">Integration Type</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Google Ads</td>
            <td class=""px-4 py-2"">Customer Match</td>
            <td class=""px-4 py-2"">Retargeting, lookalike</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Facebook</td>
            <td class=""px-4 py-2"">Custom Audiences</td>
            <td class=""px-4 py-2"">Social retargeting</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">TikTok</td>
            <td class=""px-4 py-2"">Custom Audiences</td>
            <td class=""px-4 py-2"">Video ad targeting</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Email Platforms</td>
            <td class=""px-4 py-2"">List Sync</td>
            <td class=""px-4 py-2"">Triggered campaigns</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Salesforce</td>
            <td class=""px-4 py-2"">Data Cloud</td>
            <td class=""px-4 py-2"">Sales enablement</td>
        </tr>
    </tbody>
</table>

<h3>Omnichannel Personalisation Strategy</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│              Omnichannel Segment Activation                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│                    ODP SEGMENT                                   │
│                 ""Abandoned Cart""                                 │
│                         │                                        │
│         ┌───────────────┼───────────────┐                       │
│         │               │               │                       │
│         ▼               ▼               ▼                       │
│  ┌────────────┐  ┌────────────┐  ┌────────────┐                │
│  │   WEBSITE  │  │   EMAIL    │  │    ADS     │                │
│  │ Personalised│ │  Trigger   │  │ Retarget  │                │
│  │  banner    │  │  recovery  │  │  on social │                │
│  │  showing   │  │  email     │  │  platforms │                │
│  │  cart items │  │  after 2hr │  │            │                │
│  └────────────┘  └────────────┘  └────────────┘                │
│                                                                  │
│  Result: Coordinated recovery campaign across channels          │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Best Practices for Activation</h3>
<ul>
    <li><strong>Start Simple</strong> - Begin with 2-3 key segments before expanding</li>
    <li><strong>Coordinate Channels</strong> - Ensure messaging is consistent across touchpoints</li>
    <li><strong>Respect Frequency</strong> - Don't overwhelm customers with the same segment</li>
    <li><strong>Measure Impact</strong> - Track conversion by segment across channels</li>
    <li><strong>Iterate</strong> - Refine segments based on performance data</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 6: APIs (REST and GraphQL)

    private LearningModule BuildApisModule()
    {
        return new LearningModule
        {
            Id = "apis",
            Title = "REST & GraphQL APIs",
            Description = "Master ODP's APIs for server-side integration, data import, and advanced querying.",
            Icon = "server",
            Order = 6,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "api-overview",
                    ModuleId = "apis",
                    Title = "ODP API Overview",
                    Summary = "Understand ODP's REST and GraphQL APIs and when to use each.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the difference between REST and GraphQL APIs",
                        "Know when to use each API type",
                        "Set up API authentication",
                        "Understand regional endpoints"
                    },
                    Content = @"
<h2>ODP API Overview</h2>
<p>ODP provides both REST and GraphQL APIs for server-side integration. Understanding when to use each helps you build efficient integrations.</p>

<h3>API Types Comparison</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">REST API</th>
            <th class=""px-4 py-2 text-left"">GraphQL API</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Best For</td>
            <td class=""px-4 py-2"">Data import, management, exports</td>
            <td class=""px-4 py-2"">Querying, real-time lookups</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Operations</td>
            <td class=""px-4 py-2"">CRUD operations, batch imports</td>
            <td class=""px-4 py-2"">Flexible queries, joins</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Data Volume</td>
            <td class=""px-4 py-2"">Bulk operations supported</td>
            <td class=""px-4 py-2"">Max 1000 per page</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Response</td>
            <td class=""px-4 py-2"">Fixed structure</td>
            <td class=""px-4 py-2"">You define the shape</td>
        </tr>
    </tbody>
</table>

<h3>Authentication</h3>
<p>Both APIs use the same authentication method:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# Required header for all API requests
x-api-key: YOUR_PRIVATE_API_KEY
</pre>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Security Warning</p>
    <p class=""text-red-700 dark:text-red-300"">Your Private API Key grants full access to your ODP account. Never expose it in client-side code, public repositories, or logs.</p>
</div>

<h3>Regional Endpoints</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Region</th>
            <th class=""px-4 py-2 text-left"">REST Base URL</th>
            <th class=""px-4 py-2 text-left"">GraphQL Endpoint</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">US</td>
            <td class=""px-4 py-2 font-mono text-sm"">https://api.zaius.com</td>
            <td class=""px-4 py-2 font-mono text-sm"">/v3/graphql</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">EU</td>
            <td class=""px-4 py-2 font-mono text-sm"">https://api.eu1.odp.optimizely.com</td>
            <td class=""px-4 py-2 font-mono text-sm"">/v3/graphql</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">APAC</td>
            <td class=""px-4 py-2 font-mono text-sm"">https://api.au1.odp.optimizely.com</td>
            <td class=""px-4 py-2 font-mono text-sm"">/v3/graphql</td>
        </tr>
    </tbody>
</table>

<h3>When to Use Each API</h3>
<ul>
    <li><strong>REST API</strong> - Sending events, importing customers, managing products, exports</li>
    <li><strong>GraphQL</strong> - Real-time segment queries, customer lookups, personalisation</li>
    <li><strong>Either</strong> - The right choice depends on your use case</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "api-rest",
                    ModuleId = "apis",
                    Title = "REST API Deep Dive",
                    Summary = "Master the ODP REST API for data import and management.",
                    Order = 2,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Import customers and events via REST API",
                        "Manage products and orders",
                        "Handle batch operations",
                        "Export data for analysis"
                    },
                    Content = @"
<h2>ODP REST API</h2>
<p>The REST API is your primary tool for importing and managing data in ODP.</p>

<h3>Sending Events</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
POST /v3/events
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""type"": ""order"",
  ""action"": ""purchase"",
  ""identifiers"": {
    ""email"": ""john@example.com"",
    ""customer_id"": ""CUST-123""
  },
  ""data"": {
    ""order_id"": ""ORD-456"",
    ""total"": 149.99,
    ""currency"": ""GBP""
  }
}
</pre>

<h3>Creating/Updating Customers</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
POST /v3/profiles
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""identifiers"": {
    ""email"": ""john@example.com""
  },
  ""attributes"": {
    ""first_name"": ""John"",
    ""last_name"": ""Doe"",
    ""city"": ""London"",
    ""loyalty_tier"": ""gold""
  }
}
</pre>

<h3>Batch Event Import</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
POST /v3/events/batch
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

[
  {
    ""type"": ""pageview"",
    ""identifiers"": { ""vuid"": ""abc123"" },
    ""data"": { ""page_url"": ""/products"" }
  },
  {
    ""type"": ""product"",
    ""action"": ""view"",
    ""identifiers"": { ""vuid"": ""abc123"" },
    ""data"": { ""product_id"": ""SKU-001"" }
  }
]
</pre>

<h3>Product Catalog Import</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
POST /v3/objects/products
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""product_id"": ""SKU-12345"",
  ""name"": ""Premium Widget"",
  ""price"": 99.99,
  ""category"": ""Electronics"",
  ""brand"": ""WidgetCo"",
  ""image_url"": ""https://example.com/widget.jpg""
}
</pre>

<h3>Exporting Data</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# Start an export job
POST /v3/exports
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""type"": ""customers"",
  ""filter"": {
    ""segment"": ""high_value_customers""
  },
  ""fields"": [""email"", ""first_name"", ""last_name"", ""lifetime_value""]
}

# Response includes export_id to check status
</pre>

<h3>Common REST Endpoints</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Endpoint</th>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">/v3/events</td><td class=""px-4 py-2"">POST</td><td class=""px-4 py-2"">Send events</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">/v3/profiles</td><td class=""px-4 py-2"">POST</td><td class=""px-4 py-2"">Create/update customers</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">/v3/objects/{type}</td><td class=""px-4 py-2"">POST</td><td class=""px-4 py-2"">Import objects (products)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">/v3/lists/{id}</td><td class=""px-4 py-2"">GET/POST</td><td class=""px-4 py-2"">Manage lists</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">/v3/segments</td><td class=""px-4 py-2"">GET/PUT/DELETE</td><td class=""px-4 py-2"">Manage segments</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">/v3/exports</td><td class=""px-4 py-2"">POST/GET</td><td class=""px-4 py-2"">Export data</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "api-graphql",
                    ModuleId = "apis",
                    Title = "GraphQL API Mastery",
                    Summary = "Query customer data and segments with the GraphQL API.",
                    Order = 3,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Write GraphQL queries for ODP",
                        "Query customer profiles and events",
                        "Fetch segment membership",
                        "Use pagination for large result sets"
                    },
                    Content = @"
<h2>ODP GraphQL API</h2>
<p>GraphQL lets you query exactly the data you need with flexible, efficient requests.</p>

<h3>Basic Query Structure</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
POST /v3/graphql
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""query"": ""your GraphQL query here""
}
</pre>

<h3>Query Customer by Identifier</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
query {
  customer(email: ""john@example.com"") {
    email
    first_name
    last_name
    city
    lifetime_value
    audiences {
      edges {
        node {
          name
          state
        }
      }
    }
  }
}
</pre>

<h3>Query by VUID (Anonymous Visitor)</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
query {
  customer(vuid: ""abc123-def456-..."") {
    vuid
    audiences {
      edges {
        node { name }
      }
    }
    events(first: 10) {
      edges {
        node {
          type
          action
          ts
        }
      }
    }
  }
}
</pre>

<h3>Query Products</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
query {
  products(first: 100) {
    edges {
      node {
        product_id
        name
        price
        category
      }
    }
    pageInfo {
      hasNextPage
      endCursor
    }
  }
}
</pre>

<h3>Customer Events Query</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
query {
  customer(email: ""john@example.com"") {
    events(
      first: 50,
      filter: {
        type: ""order""
      }
    ) {
      edges {
        node {
          type
          action
          ts
          data {
            order_id
            total
          }
        }
      }
    }
  }
}
</pre>

<h3>Pagination with Cursors</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# First page
query {
  customers(first: 100) {
    edges {
      node { email, first_name }
      cursor
    }
    pageInfo {
      hasNextPage
      endCursor
    }
  }
}

# Next page (use endCursor from previous response)
query {
  customers(first: 100, after: ""cursor_value_here"") {
    edges {
      node { email, first_name }
    }
    pageInfo {
      hasNextPage
      endCursor
    }
  }
}
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Performance Note</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">ODP recommends using the Exports API for result sets larger than 1,000 records. GraphQL pagination has higher latency for large datasets.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 7: Integrations

    private LearningModule BuildIntegrationsModule()
    {
        return new LearningModule
        {
            Id = "integrations",
            Title = "Integrations",
            Description = "Connect ODP with Optimizely products and external marketing platforms.",
            Icon = "puzzle-piece",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "int-cms-integration",
                    ModuleId = "integrations",
                    Title = "Optimizely CMS Integration",
                    Summary = "Connect ODP with Optimizely CMS for real-time personalisation.",
                    Order = 1,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Set up ODP integration with Optimizely CMS",
                        "Create Visitor Groups using ODP segments",
                        "Implement real-time personalisation",
                        "Track CMS content engagement in ODP"
                    },
                    Content = @"
<h2>ODP + Optimizely CMS Integration</h2>
<p>Integrate ODP with Optimizely CMS to enable real-time content personalisation based on customer data and segments.</p>

<h3>Integration Overview</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                  ODP + CMS Integration                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  WEBSITE VISITOR                                                 │
│        │                                                         │
│        ▼                                                         │
│  ┌─────────────┐                    ┌─────────────┐             │
│  │ Optimizely  │◀───── VUID ───────│     ODP     │             │
│  │    CMS      │      Cookie       │   Profile   │             │
│  │             │                    │   Segments  │             │
│  └─────────────┘                    └─────────────┘             │
│        │                                   │                     │
│        │ Query segments                    │                     │
│        │ via API                           │                     │
│        ▼                                   │                     │
│  ┌─────────────┐                          │                     │
│  │  Visitor    │──── Uses ODP ────────────┘                     │
│  │   Groups    │     Segments                                   │
│  │             │                                                 │
│  │ (ODP Segment│                                                │
│  │  Criterion) │                                                │
│  └─────────────┘                                                 │
│        │                                                         │
│        ▼                                                         │
│  PERSONALISED CONTENT                                            │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Setup Requirements</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Optimizely CMS 11 or 12</li>
    <li>ODP account with Real-Time Segments</li>
    <li>ODP JavaScript tag on your site</li>
    <li>ODP.CMS NuGet package installed</li>
</ol>

<h3>NuGet Package Installation</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# Install via Package Manager Console
Install-Package Optimizely.DataPlatform.Cms

# Or via .NET CLI
dotnet add package Optimizely.DataPlatform.Cms
</pre>

<h3>Configuration</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// In Startup.cs or Program.cs
services.AddOdpCmsIntegration(options =>
{
    options.PrivateApiKey = Configuration[""ODP:PrivateApiKey""];
    options.TrackerKey = Configuration[""ODP:TrackerKey""];
    options.ApiEndpoint = ""https://api.zaius.com""; // Or EU/APAC endpoint
});
</pre>

<h3>Creating ODP Visitor Groups</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Go to CMS Admin → Visitor Groups</li>
    <li>Create a new Visitor Group</li>
    <li>Add criterion → Select ""ODP Segment""</li>
    <li>Choose the ODP segment to match</li>
    <li>Save the Visitor Group</li>
</ol>

<h3>Using in Content</h3>
<p>Once configured, use Visitor Groups as normal in your CMS content:</p>
<ul>
    <li>Personalise blocks and content areas</li>
    <li>Show/hide content based on ODP segments</li>
    <li>Create targeted landing pages</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Real-Time Updates</p>
    <p class=""text-blue-700 dark:text-blue-300"">ODP segments update in under 90 seconds. This means personalisation can react to visitor behaviour from moments ago, unlike traditional session-based Visitor Groups.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "int-commerce-integration",
                    ModuleId = "integrations",
                    Title = "Commerce Connect Integration",
                    Summary = "Send e-commerce data from Commerce Connect to ODP.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure ODP integration with Commerce Connect",
                        "Send order and product data to ODP",
                        "Track customer purchase behaviour",
                        "Create commerce-based segments"
                    },
                    Content = @"
<h2>ODP + Commerce Connect</h2>
<p>Integrate ODP with Optimizely Commerce Connect to capture e-commerce events and create purchase-based segments.</p>

<h3>Data Flow</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Commerce Data</th>
            <th class=""px-4 py-2 text-left"">ODP Event/Object</th>
            <th class=""px-4 py-2 text-left"">Sync Method</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Product Catalog</td>
            <td class=""px-4 py-2"">Products object</td>
            <td class=""px-4 py-2"">Scheduled job</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Orders</td>
            <td class=""px-4 py-2"">order.purchase event</td>
            <td class=""px-4 py-2"">Real-time webhook</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Customers</td>
            <td class=""px-4 py-2"">Customer profiles</td>
            <td class=""px-4 py-2"">Real-time/batch</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Cart Actions</td>
            <td class=""px-4 py-2"">product.add_to_cart</td>
            <td class=""px-4 py-2"">JavaScript SDK</td>
        </tr>
    </tbody>
</table>

<h3>Configuration</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// appsettings.json
{
  ""ODP"": {
    ""TrackerKey"": ""your-tracker-id"",
    ""PrivateApiKey"": ""your-private-key"",
    ""Endpoint"": ""https://api.zaius.com"",
    ""ProductSyncEnabled"": true,
    ""OrderSyncEnabled"": true
  }
}
</pre>

<h3>Server-Side Order Tracking</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Example: Send order to ODP after checkout
public async Task TrackOrderAsync(IPurchaseOrder order)
{
    var orderEvent = new OdpEvent
    {
        Type = ""order"",
        Action = ""purchase"",
        Identifiers = new Dictionary&lt;string, string&gt;
        {
            { ""email"", order.GetFirstForm().Payments.First().BillingAddress.Email }
        },
        Data = new Dictionary&lt;string, object&gt;
        {
            { ""order_id"", order.OrderNumber },
            { ""total"", order.GetTotal().Amount },
            { ""currency"", order.Currency.CurrencyCode }
        }
    };

    await _odpClient.SendEventAsync(orderEvent);
}
</pre>

<h3>E-commerce Segments</h3>
<p>With commerce data flowing to ODP, create powerful segments:</p>
<ul>
    <li><strong>Repeat Purchasers</strong> - Orders > 2 in 90 days</li>
    <li><strong>High AOV</strong> - Average order value > £100</li>
    <li><strong>Category Buyers</strong> - Purchased from specific category</li>
    <li><strong>Lapsed Customers</strong> - No order in 180 days</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "int-external-platforms",
                    ModuleId = "integrations",
                    Title = "External Platform Integrations",
                    Summary = "Connect ODP with marketing and advertising platforms.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Export audiences to advertising platforms",
                        "Integrate with email marketing tools",
                        "Connect CRM systems",
                        "Use CDP Audience Sync"
                    },
                    Content = @"
<h2>External Platform Integrations</h2>
<p>ODP offers 60+ native integrations to activate your customer data across marketing channels.</p>

<h3>Advertising Platforms</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Platform</th>
            <th class=""px-4 py-2 text-left"">Integration Type</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Google Ads</td>
            <td class=""px-4 py-2"">Customer Match</td>
            <td class=""px-4 py-2"">Retargeting, lookalike audiences</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Facebook/Meta</td>
            <td class=""px-4 py-2"">Custom Audiences</td>
            <td class=""px-4 py-2"">Social retargeting</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">TikTok</td>
            <td class=""px-4 py-2"">Audience Sync</td>
            <td class=""px-4 py-2"">Video ad targeting</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">LinkedIn</td>
            <td class=""px-4 py-2"">Matched Audiences</td>
            <td class=""px-4 py-2"">B2B advertising</td>
        </tr>
    </tbody>
</table>

<h3>Email & Marketing Platforms</h3>
<ul>
    <li><strong>Braze</strong> - Sync segments and events bidirectionally</li>
    <li><strong>Marketo</strong> - Import leads and activities</li>
    <li><strong>HubSpot</strong> - Contact sync and segmentation</li>
    <li><strong>Salesforce Marketing Cloud</strong> - Journey triggers</li>
</ul>

<h3>CRM Systems</h3>
<ul>
    <li><strong>Salesforce CRM</strong> - Contact and lead enrichment</li>
    <li><strong>Salesforce Data Cloud</strong> - Bidirectional audience sync</li>
</ul>

<h3>CDP Audience Sync</h3>
<p>If you already have a CDP, you can sync external audiences to ODP:</p>
<ul>
    <li><strong>Segment</strong> - Import audiences from Segment</li>
    <li><strong>Tealium</strong> - Sync AudienceStream audiences</li>
    <li><strong>mParticle</strong> - Audience import</li>
    <li><strong>Mixpanel</strong> - Import Mixpanel audiences</li>
</ul>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">CDP Audience Sync Benefit</p>
    <p class=""text-green-700 dark:text-green-300"">Existing CDP users can leverage ODP without platform replacement. Send external segments directly into Optimizely for experimentation and personalisation.</p>
</div>

<h3>Setting Up an Integration</h3>
<ol class=""list-decimal list-inside space-y-2"">
    <li>Navigate to <strong>Integrations</strong> in ODP</li>
    <li>Find and select the platform</li>
    <li>Authenticate with the platform (OAuth or API key)</li>
    <li>Configure sync settings (segments, fields, frequency)</li>
    <li>Activate the integration</li>
</ol>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 8: Privacy and Compliance

    private LearningModule BuildPrivacyComplianceModule()
    {
        return new LearningModule
        {
            Id = "privacy",
            Title = "Privacy & Compliance",
            Description = "Implement ODP in compliance with GDPR, CCPA, and other privacy regulations.",
            Icon = "shield-check",
            Order = 8,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "priv-gdpr-ccpa",
                    ModuleId = "privacy",
                    Title = "GDPR and CCPA Compliance",
                    Summary = "Understand privacy regulations and how ODP supports compliance.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand GDPR and CCPA requirements",
                        "Know how ODP supports privacy compliance",
                        "Implement consent-gated tracking",
                        "Handle data subject requests"
                    },
                    Content = @"
<h2>Privacy Compliance with ODP</h2>
<p>ODP provides tools to help you comply with privacy regulations including GDPR, CCPA, and LGPD.</p>

<h3>Key Regulations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Regulation</th>
            <th class=""px-4 py-2 text-left"">Region</th>
            <th class=""px-4 py-2 text-left"">Key Requirements</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">GDPR</td>
            <td class=""px-4 py-2"">EU/EEA</td>
            <td class=""px-4 py-2"">Consent, right to deletion, data portability</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">CCPA/CPRA</td>
            <td class=""px-4 py-2"">California</td>
            <td class=""px-4 py-2"">Opt-out rights, disclosure, deletion</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">LGPD</td>
            <td class=""px-4 py-2"">Brazil</td>
            <td class=""px-4 py-2"">Similar to GDPR</td>
        </tr>
    </tbody>
</table>

<h3>ODP Privacy Features</h3>
<ul>
    <li><strong>Consent Management</strong> - Track opt-in/opt-out status</li>
    <li><strong>Data Deletion API</strong> - Process deletion requests</li>
    <li><strong>Data Export</strong> - Support data portability requests</li>
    <li><strong>Regional Data Residency</strong> - EU and APAC endpoints</li>
</ul>

<h3>Consent-Gated Tracking</h3>
<p>Only load the ODP SDK after user consent:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Wait for consent before loading ODP
function initOdpWithConsent() {
    if (hasAnalyticsConsent()) {
        zaius.load('YOUR_TRACKER_ID');
    }
}

// Listen for consent changes
document.addEventListener('consentGranted', function() {
    zaius.load('YOUR_TRACKER_ID');
});
</pre>

<h3>Data Deletion Requests</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# GDPR deletion request
POST /v3/privacy/deletion
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""regulation"": ""gdpr"",
  ""identifiers"": {
    ""email"": ""john@example.com""
  }
}

# Check deletion status
GET /v3/privacy/deletion/{request_id}
</pre>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Important</p>
    <p class=""text-red-700 dark:text-red-300"">Deletion requests are irreversible. Always verify the request is legitimate before processing. ODP processes deletions within 30 days as required by GDPR.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "priv-consent-management",
                    ModuleId = "privacy",
                    Title = "Consent Management Implementation",
                    Summary = "Implement consent tracking and honour customer preferences.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Integrate with consent management platforms",
                        "Track consent status in ODP",
                        "Respect consent in marketing campaigns",
                        "Handle consent changes"
                    },
                    Content = @"
<h2>Consent Management</h2>
<p>Proper consent management ensures you respect customer preferences and maintain compliance.</p>

<h3>Consent Status in ODP</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Meaning</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-mono"">opted_in</td>
            <td class=""px-4 py-2"">Explicit consent given</td>
            <td class=""px-4 py-2"">Safe to contact</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">opted_out</td>
            <td class=""px-4 py-2"">Explicitly declined</td>
            <td class=""px-4 py-2"">Do not contact</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono"">not_set</td>
            <td class=""px-4 py-2"">No preference recorded</td>
            <td class=""px-4 py-2"">Depends on your policy</td>
        </tr>
    </tbody>
</table>

<h3>Setting Consent via SDK</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Record consent preferences
zaius.consent({
    email: ""opted_in"",
    sms: ""opted_out"",
    push: ""not_set""
});
</pre>

<h3>Setting Consent via API</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
POST /v3/profiles
x-api-key: YOUR_PRIVATE_API_KEY
Content-Type: application/json

{
  ""identifiers"": {
    ""email"": ""john@example.com""
  },
  ""attributes"": {
    ""consent_email"": ""opted_in"",
    ""consent_sms"": ""opted_out"",
    ""consent_updated_at"": ""2024-01-15T10:30:00Z""
  }
}
</pre>

<h3>CMP Integration (OneTrust Example)</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// OneTrust integration
window.OptanonWrapper = function() {
    // Analytics category (C0002)
    if (OnetrustActiveGroups.includes('C0002')) {
        zaius.load('YOUR_TRACKER_ID');

        zaius.consent({
            analytics: ""opted_in""
        });
    }

    // Marketing category (C0004)
    if (OnetrustActiveGroups.includes('C0004')) {
        zaius.consent({
            email: ""opted_in""
        });
    }
};
</pre>

<h3>Best Practices</h3>
<ul>
    <li><strong>Record Consent Source</strong> - Track where consent was given</li>
    <li><strong>Timestamp Changes</strong> - Record when consent status changed</li>
    <li><strong>Sync Across Systems</strong> - Keep consent consistent</li>
    <li><strong>Audit Trail</strong> - Maintain history of consent</li>
    <li><strong>Default to Safe</strong> - When unsure, don't contact</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 9: Advanced Topics

    private LearningModule BuildAdvancedTopicsModule()
    {
        return new LearningModule
        {
            Id = "advanced",
            Title = "Advanced Topics",
            Description = "Explore advanced ODP features including predictive analytics, mobile SDKs, and data modelling.",
            Icon = "beaker",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "adv-predictive",
                    ModuleId = "advanced",
                    Title = "Predictive Analytics",
                    Summary = "Leverage ODP's AI-powered predictive segments and insights.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand ODP's predictive capabilities",
                        "Use propensity scores for targeting",
                        "Leverage churn prediction",
                        "Apply predictive segments in campaigns"
                    },
                    Content = @"
<h2>Predictive Analytics in ODP</h2>
<p>ODP uses machine learning to predict customer behaviour and automatically create actionable segments.</p>

<h3>Predictive Capabilities</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Prediction Type</th>
            <th class=""px-4 py-2 text-left"">What It Predicts</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Propensity to Buy</td>
            <td class=""px-4 py-2"">Likelihood of purchase</td>
            <td class=""px-4 py-2"">Prioritise high-intent visitors</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Churn Risk</td>
            <td class=""px-4 py-2"">Likelihood of becoming inactive</td>
            <td class=""px-4 py-2"">Win-back campaigns</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Customer Lifetime Value</td>
            <td class=""px-4 py-2"">Predicted future value</td>
            <td class=""px-4 py-2"">VIP treatment, acquisition focus</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Next Best Action</td>
            <td class=""px-4 py-2"">Recommended engagement</td>
            <td class=""px-4 py-2"">Personalised journeys</td>
        </tr>
    </tbody>
</table>

<h3>Using Predictive Segments</h3>
<p>Predictive segments are automatically generated and can be used like any other segment:</p>

<ul>
    <li><strong>High Propensity to Buy</strong> - Show special offers</li>
    <li><strong>At Risk of Churn</strong> - Trigger retention campaign</li>
    <li><strong>High Predicted CLV</strong> - White-glove service</li>
</ul>

<h3>Predictive in Personalisation</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// Query predictive attributes via GraphQL
query {
  customer(email: ""john@example.com"") {
    propensity_to_buy
    churn_risk_score
    predicted_ltv
    next_best_action
  }
}
</pre>

<div class=""bg-indigo-50 dark:bg-indigo-900/30 border-l-4 border-indigo-500 p-4 my-4"">
    <p class=""font-medium text-indigo-800 dark:text-indigo-200"">Data Requirements</p>
    <p class=""text-indigo-700 dark:text-indigo-300"">Predictive models require sufficient historical data to generate accurate predictions. The more customer interactions and transactions you send to ODP, the better the predictions become.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "adv-mobile-sdks",
                    ModuleId = "advanced",
                    Title = "Mobile SDKs",
                    Summary = "Implement ODP tracking in mobile applications.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand ODP mobile SDK options",
                        "Implement React Native SDK",
                        "Track mobile events",
                        "Sync mobile and web identities"
                    },
                    Content = @"
<h2>ODP Mobile SDKs</h2>
<p>ODP provides SDKs for mobile applications to track user behaviour and sync with web data.</p>

<h3>Available SDKs</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">SDK</th>
            <th class=""px-4 py-2 text-left"">Platform</th>
            <th class=""px-4 py-2 text-left"">Installation</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">React Native SDK</td>
            <td class=""px-4 py-2"">iOS & Android</td>
            <td class=""px-4 py-2"">npm package</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">REST API</td>
            <td class=""px-4 py-2"">Any platform</td>
            <td class=""px-4 py-2"">HTTP calls</td>
        </tr>
    </tbody>
</table>

<h3>React Native Installation</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
# Install the SDK
npm install @zaius/react-native-sdk

# iOS additional setup
cd ios && pod install
</pre>

<h3>React Native Usage</h3>
<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
import Zaius from '@zaius/react-native-sdk';

// Initialize
Zaius.initialize('YOUR_TRACKER_ID');

// Track events
Zaius.event('product', {
  action: 'view',
  product_id: 'SKU-123'
});

// Identify user
Zaius.identify({
  email: 'user@example.com'
});
</pre>

<h3>Cross-Device Identity</h3>
<p>Link mobile and web sessions by identifying users:</p>

<pre class=""bg-gray-800 text-gray-100 p-4 rounded-lg overflow-x-auto"">
// On login (both web and mobile)
// Use the same identifier
zaius.identify({
  email: 'user@example.com',
  customer_id: 'CUST-123'
});
// ODP merges the profiles automatically
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "adv-best-practices",
                    ModuleId = "advanced",
                    Title = "Best Practices & Optimisation",
                    Summary = "Apply best practices for ODP implementation success.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Follow implementation best practices",
                        "Optimise data quality",
                        "Monitor and troubleshoot",
                        "Scale your ODP usage"
                    },
                    Content = @"
<h2>ODP Best Practices</h2>
<p>Follow these best practices to maximise the value of your ODP implementation.</p>

<h3>Data Quality</h3>
<ul>
    <li><strong>Validate Emails</strong> - Only send valid email addresses</li>
    <li><strong>Consistent IDs</strong> - Use the same customer_id format everywhere</li>
    <li><strong>Clean Data</strong> - Remove test data from production</li>
    <li><strong>Complete Profiles</strong> - Send all available attributes</li>
</ul>

<h3>Event Tracking</h3>
<ul>
    <li><strong>Track Meaningful Events</strong> - Focus on business-relevant actions</li>
    <li><strong>Include Context</strong> - Add product_id, category, value where relevant</li>
    <li><strong>Server-Side Orders</strong> - Use REST API for transaction events</li>
    <li><strong>Document Your Schema</strong> - Maintain an event dictionary</li>
</ul>

<h3>Segmentation</h3>
<ul>
    <li><strong>Start Simple</strong> - Begin with 3-5 key segments</li>
    <li><strong>Validate Size</strong> - Check segment populations make sense</li>
    <li><strong>Real-Time for Current</strong> - Use real-time for recent behaviour</li>
    <li><strong>Standard for Historical</strong> - Use standard for lifetime metrics</li>
</ul>

<h3>Monitoring Checklist</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li>☐ Event volume trending as expected</li>
        <li>☐ Customer profiles being created</li>
        <li>☐ Identity merge rate is healthy</li>
        <li>☐ Segment populations are stable</li>
        <li>☐ API error rate is low</li>
        <li>☐ Integrations are syncing</li>
    </ul>
</div>

<h3>Performance Tips</h3>
<ul>
    <li><strong>Batch Imports</strong> - Use batch API for bulk operations</li>
    <li><strong>Exports API</strong> - Use exports for large data pulls</li>
    <li><strong>Cache Segment Results</strong> - Don't query on every request</li>
    <li><strong>Async Processing</strong> - Don't block user experience on API calls</li>
</ul>

<h3>Troubleshooting</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Check</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2"">Events not appearing</td>
            <td class=""px-4 py-2"">Network tab, Tracker ID, ad blocker</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">No customers in profiles</td>
            <td class=""px-4 py-2"">Are you identifying users?</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">Segment empty</td>
            <td class=""px-4 py-2"">Criteria too restrictive?</td>
        </tr>
        <tr>
            <td class=""px-4 py-2"">API errors</td>
            <td class=""px-4 py-2"">Key valid? Endpoint correct?</td>
        </tr>
    </tbody>
</table>

<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <p class=""font-medium text-green-800 dark:text-green-200"">Success Metrics</p>
    <p class=""text-green-700 dark:text-green-300"">Track these KPIs to measure ODP success: identified customer rate, segment activation rate, personalisation lift, and campaign performance by segment.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion
}
