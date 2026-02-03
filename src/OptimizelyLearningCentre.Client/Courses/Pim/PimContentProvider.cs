using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.Pim;

/// <summary>
/// Content provider for the Optimizely Product Information Management (PIM) course
/// </summary>
public class PimContentProvider : ILearningContentProvider
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
            BuildPropertiesDataModellingModule(),
            BuildProductTemplatesModule(),
            BuildCatalogCategoryModule(),
            BuildProductManagementModule(),
            BuildVariantsRelationshipsModule(),
            BuildDigitalAssetsModule(),
            BuildImportExportApiModule(),
            BuildMultiLanguageModule(),
            BuildWorkflowsRolesModule()
        };
    }

    #region Module 1: Getting Started with PIM

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "pim-getting-started",
            Title = "Getting Started with PIM",
            Description = "Learn the fundamentals of Optimizely Product Information Management, understand its purpose, architecture, and how it fits within the Optimizely ecosystem.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-what-is-pim",
                    ModuleId = "pim-getting-started",
                    Title = "What is Optimizely PIM?",
                    Summary = "Discover what Product Information Management is, why it matters, and how Optimizely PIM serves as a centralised hub for product data.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Product Information Management (PIM) is and why organisations need it",
                        "Learn the core value proposition of Optimizely PIM",
                        "Understand how PIM fits within the broader Optimizely ecosystem",
                        "Know when to use PIM for your commerce projects"
                    },
                    Content = @"
<h2>Introduction to Optimizely PIM</h2>
<p>Optimizely Product Information Management (PIM) is a <strong>centralised, cloud-based platform</strong> that allows organisations to consolidate, manage, enrich, and distribute product information across their entire business. It serves as the <strong>single source of truth</strong> for all product data, ensuring consistency and accuracy across every channel.</p>

<h3>What is Product Information Management?</h3>
<p>Product Information Management (PIM) is a discipline and toolset focused on managing the data needed to market and sell products through distribution channels. In modern commerce, product data is scattered across ERP systems, spreadsheets, supplier feeds, and various internal databases. PIM brings all of this together into one authoritative system.</p>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Why PIM Matters</p>
    <p class=""text-orange-700 dark:text-orange-300"">Without PIM, businesses often struggle with inconsistent product descriptions, missing attributes, outdated pricing, and disconnected data across channels. PIM eliminates these issues by providing a governed, structured approach to product data management.</p>
</div>

<h3>Key Benefits of Optimizely PIM</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Single Source of Truth</td><td class=""px-4 py-2"">Centralise all product data in one authoritative system</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data Quality</td><td class=""px-4 py-2"">System-enforced validation ensures accurate and complete data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Multi-Channel Publishing</td><td class=""px-4 py-2"">Publish consistent product data to websites, apps, and marketplaces</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Workflow Automation</td><td class=""px-4 py-2"">Built-in approval workflows for data enrichment and publishing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Collaboration</td><td class=""px-4 py-2"">Role-based access enables teams to collaborate on product data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Scalability</td><td class=""px-4 py-2"">Manage thousands of SKUs with bulk operations and imports</td></tr>
    </tbody>
</table>

<h3>PIM within the Optimizely Ecosystem</h3>
<p>Optimizely PIM is natively integrated with the broader Optimizely product suite, particularly Configured Commerce (B2B Commerce Cloud). Here's how PIM fits into the ecosystem:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    Optimizely Commerce Ecosystem                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌────────────┐    ┌─────────────────┐    ┌─────────────────┐   │
│  │  ERP / 3rd │───▶│  Optimizely PIM │───▶│   Configured    │   │
│  │   Party    │    │  (Central Hub)  │    │    Commerce     │   │
│  │  Systems   │    │                 │    │   (Storefront)  │   │
│  └────────────┘    │  • Properties   │    └─────────────────┘   │
│                    │  • Templates    │                           │
│  ┌────────────┐    │  • Categories   │    ┌─────────────────┐   │
│  │  Supplier  │───▶│  • Variants     │───▶│   Customised    │   │
│  │   Feeds    │    │  • Assets       │    │    Commerce     │   │
│  └────────────┘    │  • Workflows    │    │   (Storefront)  │   │
│                    └─────────────────┘    └─────────────────┘   │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>When to Use Optimizely PIM</h3>
<ul>
    <li><strong>Large product catalogues</strong> — Hundreds or thousands of SKUs requiring structured management</li>
    <li><strong>Multiple data sources</strong> — Product data coming from ERPs, suppliers, and internal teams</li>
    <li><strong>Multi-channel commerce</strong> — Publishing product information to multiple storefronts or marketplaces</li>
    <li><strong>Team collaboration</strong> — Multiple people or departments responsible for product data</li>
    <li><strong>Data quality requirements</strong> — Need for validation, completeness tracking, and governance</li>
    <li><strong>Optimizely Configured Commerce</strong> — Using or planning to use Optimizely's B2B commerce platform</li>
</ul>

<h3>PIM vs Spreadsheets</h3>
<p>Many organisations start managing product data in spreadsheets, but this approach quickly breaks down at scale:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Spreadsheets</th>
            <th class=""px-4 py-2 text-left"">Optimizely PIM</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Data Validation</td><td class=""px-4 py-2"">Manual, error-prone</td><td class=""px-4 py-2"">System-enforced rules</td></tr>
        <tr><td class=""px-4 py-2"">Collaboration</td><td class=""px-4 py-2"">Version conflicts, no roles</td><td class=""px-4 py-2"">Role-based, concurrent editing</td></tr>
        <tr><td class=""px-4 py-2"">Workflows</td><td class=""px-4 py-2"">Email-based, ad hoc</td><td class=""px-4 py-2"">Built-in approval pipelines</td></tr>
        <tr><td class=""px-4 py-2"">Publishing</td><td class=""px-4 py-2"">Manual copy/paste</td><td class=""px-4 py-2"">Automated sync to commerce</td></tr>
        <tr><td class=""px-4 py-2"">Digital Assets</td><td class=""px-4 py-2"">Separate storage, no link</td><td class=""px-4 py-2"">Integrated asset management</td></tr>
        <tr><td class=""px-4 py-2"">Audit Trail</td><td class=""px-4 py-2"">None</td><td class=""px-4 py-2"">Full change tracking</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-architecture-overview",
                    ModuleId = "pim-getting-started",
                    Title = "PIM Architecture & Components",
                    Summary = "Understand the architecture of Optimizely PIM, its core components, and how data flows through the system.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the cloud-based architecture of Optimizely PIM",
                        "Learn about the core components: properties, templates, categories, and products",
                        "Understand how data flows from source systems through PIM to commerce",
                        "Know the key integration points with Configured Commerce"
                    },
                    Content = @"
<h2>PIM Architecture Overview</h2>
<p>Optimizely PIM is a <strong>cloud-hosted, SaaS application</strong> that operates as a centralised data hub. It receives product information from various sources, provides tools to enrich and validate that data, and then publishes it to downstream commerce platforms.</p>

<h3>Core Architectural Components</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li><strong>Properties & Property Groups</strong> — Define the metadata schema for products (dimensions, descriptions, attributes)</li>
        <li><strong>Product Templates</strong> — Structure which properties apply to specific product types (shoes, electronics, etc.)</li>
        <li><strong>Categories</strong> — Organise products into browsable hierarchies with unlimited nesting levels</li>
        <li><strong>Products & Variants</strong> — The actual product records with parent-child variant relationships</li>
        <li><strong>Digital Assets</strong> — Images, documents, and media files associated with products</li>
        <li><strong>Workflows</strong> — Approval pipelines that govern data enrichment and publishing</li>
    </ol>
</div>

<h3>Data Flow Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────────┐     ┌───────────────────────────────────────┐     ┌──────────────┐
│  DATA SOURCES │     │         OPTIMIZELY PIM                │     │  COMMERCE    │
│              │     │                                        │     │              │
│ ┌──────────┐ │     │  ┌────────────┐   ┌───────────────┐  │     │ ┌──────────┐ │
│ │   ERP    │─┼────▶│  │  Import    │──▶│   Product     │  │     │ │Configured│ │
│ └──────────┘ │     │  │  Engine    │   │   Database    │  │     │ │ Commerce │ │
│              │     │  └────────────┘   │               │  │     │ └──────────┘ │
│ ┌──────────┐ │     │                   │  ┌─────────┐  │  │     │              │
│ │ Supplier │─┼────▶│  ┌────────────┐   │  │Properties│  │──┼────▶│ ┌──────────┐ │
│ │  Feeds   │ │     │  │  Service   │──▶│  │Templates│  │  │     │ │Customised│ │
│ └──────────┘ │     │  │  API       │   │  │Products │  │  │     │ │ Commerce │ │
│              │     │  └────────────┘   │  │Variants │  │  │     │ └──────────┘ │
│ ┌──────────┐ │     │                   │  │Assets   │  │  │     │              │
│ │ Manual   │─┼────▶│  ┌────────────┐   │  └─────────┘  │  │     │ ┌──────────┐ │
│ │  Entry   │ │     │  │  UI Editor │──▶│               │  │     │ │  Other   │ │
│ └──────────┘ │     │  └────────────┘   └───────────────┘  │     │ │ Channels │ │
└──────────────┘     └───────────────────────────────────────┘     └──────────────┘
</pre>

<h3>Integration with Configured Commerce</h3>
<p>PIM connects to Optimizely Configured Commerce through a dedicated <strong>PIM sync job</strong> that runs within the commerce platform. This sync job:</p>
<ul>
    <li>Pulls approved product data from PIM into Configured Commerce</li>
    <li>Synchronises categories, properties, pricing, and inventory data</li>
    <li>Maps PIM product templates to commerce product structures</li>
    <li>Transfers digital assets and their product associations</li>
    <li>Handles incremental updates to avoid full catalogue reimports</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Integration Setup</p>
    <p class=""text-orange-700 dark:text-orange-300"">Before using PIM with Configured Commerce, you must set up the PIM sync job in the commerce Admin Console and configure the integration settings for each website that will receive product data.</p>
</div>

<h3>Service API</h3>
<p>Optimizely PIM exposes a <strong>REST-based Service API</strong> that allows external systems to programmatically interact with product data. The API supports:</p>
<ul>
    <li><strong>Authentication</strong> — Token-based auth using AppKey and AppSecret credentials</li>
    <li><strong>Data Retrieval</strong> — Read product, category, and template information</li>
    <li><strong>Data Import</strong> — Trigger bulk import operations and monitor job status</li>
    <li><strong>Status Monitoring</strong> — Check import job progress and completion</li>
</ul>

<h3>System Settings</h3>
<p>PIM provides system-level configuration options that affect the entire platform:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Language Configuration</td><td class=""px-4 py-2"">Define which languages are available for product data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Image Sizing</td><td class=""px-4 py-2"">Configure image dimensions and thumbnails for products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Commerce Version</td><td class=""px-4 py-2"">Set the target Configured Commerce version for sync compatibility</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">API Credentials</td><td class=""px-4 py-2"">Manage AppKey/AppSecret pairs for Service API access</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-navigating-dashboard",
                    ModuleId = "pim-getting-started",
                    Title = "Navigating the PIM Dashboard",
                    Summary = "Learn to navigate the PIM interface, understand the dashboard, and find your way around the key areas of the application.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the main areas of the PIM interface",
                        "Understand the dashboard and its product statistics",
                        "Know how to access products, categories, templates, and settings",
                        "Use the dashboard to track product completeness and status"
                    },
                    Content = @"
<h2>The PIM Dashboard</h2>
<p>When you log in to Optimizely PIM, you are greeted with the <strong>Dashboard</strong> — a centralised overview of your product catalogue's health and status. The dashboard provides quick visibility into how your product data is progressing through the enrichment and publishing pipeline.</p>

<h3>Dashboard Components</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Product Statistics by Status</strong> — A visual breakdown showing how many products are in each stage of the pipeline (Draft, In Progress, Ready for Approval, Approved, Published)</li>
        <li><strong>Completeness Indicators</strong> — Shows the percentage of products with all required properties filled in</li>
        <li><strong>Pending Actions</strong> — Highlights products that require attention, such as those awaiting approval or missing required data</li>
        <li><strong>Collection Widgets</strong> — Quick-access links to saved filter sets for common product views</li>
    </ul>
</div>

<h3>Main Navigation Areas</h3>
<p>The PIM interface is organised into several primary areas, accessible from the main navigation:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Area</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Key Actions</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Dashboard</td><td class=""px-4 py-2"">Overview of catalogue health</td><td class=""px-4 py-2"">View statistics, access pending items</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Products</td><td class=""px-4 py-2"">Manage all product records</td><td class=""px-4 py-2"">Create, edit, import, export, approve products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Categories</td><td class=""px-4 py-2"">Organise product hierarchy</td><td class=""px-4 py-2"">Create, edit, reorder categories</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Assets</td><td class=""px-4 py-2"">Manage digital assets</td><td class=""px-4 py-2"">Upload, organise, assign assets to products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data Setup</td><td class=""px-4 py-2"">Configure data model</td><td class=""px-4 py-2"">Manage properties, templates, property groups</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Settings</td><td class=""px-4 py-2"">System configuration</td><td class=""px-4 py-2"">Languages, image sizes, API credentials, team members</td></tr>
    </tbody>
</table>

<h3>Product Status Pipeline</h3>
<p>Products in PIM move through a defined status pipeline that the dashboard tracks:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌────────┐    ┌─────────────┐    ┌──────────────┐    ┌──────────┐    ┌───────────┐
│ Draft  │───▶│ In Progress │───▶│  Ready for   │───▶│ Approved │───▶│ Published │
│        │    │             │    │   Approval   │    │          │    │           │
└────────┘    └─────────────┘    └──────────────┘    └──────────┘    └───────────┘
     ▲                                  │                                    │
     │                                  │     (Rejected)                     │
     └──────────────────────────────────┘                                    │
                                                                             │
                                                         Synced to Commerce ◀┘
</pre>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Dashboard Tip</p>
    <p class=""text-orange-700 dark:text-orange-300"">Use the dashboard status breakdown to quickly identify bottlenecks in your product data pipeline. If many products are stuck in ""Ready for Approval"", it may indicate your approval process needs more resources or faster turnaround.</p>
</div>

<h3>Quick Actions from the Dashboard</h3>
<ul>
    <li><strong>Click a status segment</strong> — Jump directly to a filtered list of products in that status</li>
    <li><strong>View incomplete products</strong> — Quickly find products missing required properties</li>
    <li><strong>Access collections</strong> — Navigate to saved filter sets for your team's common workflows</li>
    <li><strong>Check recent activity</strong> — See what changes have been made recently across the catalogue</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-implementation-strategy",
                    ModuleId = "pim-getting-started",
                    Title = "Recommended Implementation Strategy",
                    Summary = "Learn the recommended approach for implementing Optimizely PIM, from initial configuration through data loading to ongoing maintenance.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the three-phase implementation approach for PIM",
                        "Know which configuration steps to complete before loading data",
                        "Learn best practices for initial data migration into PIM",
                        "Plan for ongoing maintenance and publishing workflows"
                    },
                    Content = @"
<h2>PIM Implementation Strategy</h2>
<p>Optimizely recommends a <strong>three-phase approach</strong> to implementing PIM. Following this structured strategy ensures that your data model is properly configured before loading product data, and that your team is prepared for ongoing maintenance.</p>

<h3>Phase 1: Configuration</h3>
<p>The first phase focuses on setting up the data model that will govern your product information. This phase should be completed <strong>before any product data is loaded</strong>.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Define Property Groups</strong> — Create logical groupings for your product attributes (e.g., Dimensions, Branding, Technical Specs)</li>
        <li><strong>Configure Properties</strong> — Define each product attribute with its data type, validation rules, and requirements</li>
        <li><strong>Create Product Templates</strong> — Build templates that define which properties apply to each product type</li>
        <li><strong>Set Up Categories</strong> — Design your category hierarchy to match how customers browse products</li>
        <li><strong>Configure System Settings</strong> — Set languages, image sizes, and integration parameters</li>
        <li><strong>Define Roles & Permissions</strong> — Set up team members with appropriate roles and template assignments</li>
    </ol>
</div>

<h3>Phase 2: Data Loading</h3>
<p>Once the data model is configured, you can begin importing product data from your existing systems.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Import Products</strong> — Load product records from ERP, supplier feeds, or spreadsheets</li>
        <li><strong>Assign Categories</strong> — Map products to the category hierarchy</li>
        <li><strong>Import Digital Assets</strong> — Upload images, documents, and associate them with products</li>
        <li><strong>Set Up Variants</strong> — Configure variant types and create parent-child relationships</li>
        <li><strong>Import Translations</strong> — Load multi-language content if applicable</li>
        <li><strong>Create Related Products</strong> — Define product relationships and associations</li>
    </ol>
</div>

<h3>Phase 3: Maintenance & Publishing</h3>
<p>With data loaded, your team transitions to ongoing maintenance and the publishing workflow.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Enrich Product Data</strong> — Complete and enhance product information across the catalogue</li>
        <li><strong>Review & Approve</strong> — Use the workflow to review completeness and approve products</li>
        <li><strong>Publish to Commerce</strong> — Sync approved products to Configured Commerce storefronts</li>
        <li><strong>Monitor & Maintain</strong> — Track sync jobs, fix data issues, and update products as needed</li>
    </ol>
</div>

<h3>Integration Setup Checklist</h3>
<p>Before going live with PIM and Configured Commerce, ensure the following integration steps are complete:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Step</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Where</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Set up PIM sync job</td><td class=""px-4 py-2"">Configure the scheduled job that pulls data from PIM</td><td class=""px-4 py-2"">Configured Commerce Admin</td></tr>
        <tr><td class=""px-4 py-2"">Configure website integration</td><td class=""px-4 py-2"">Map PIM to specific commerce websites</td><td class=""px-4 py-2"">Configured Commerce Admin</td></tr>
        <tr><td class=""px-4 py-2"">Define related product types</td><td class=""px-4 py-2"">Configure which relationship types to sync</td><td class=""px-4 py-2"">PIM Settings</td></tr>
        <tr><td class=""px-4 py-2"">Set commerce version</td><td class=""px-4 py-2"">Ensure PIM targets the correct commerce version</td><td class=""px-4 py-2"">PIM Settings</td></tr>
        <tr><td class=""px-4 py-2"">Configure languages</td><td class=""px-4 py-2"">Enable languages in commerce, then sync to PIM</td><td class=""px-4 py-2"">Both systems</td></tr>
    </tbody>
</table>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Best Practice</p>
    <p class=""text-orange-700 dark:text-orange-300"">Always complete the configuration phase fully before loading data. Changing property types or template structures after data has been loaded can require significant rework and may result in data loss.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Properties & Data Modelling

    private LearningModule BuildPropertiesDataModellingModule()
    {
        return new LearningModule
        {
            Id = "pim-properties-data-modelling",
            Title = "Properties & Data Modelling",
            Description = "Learn how to define and configure the property schema that underpins all product data in PIM, including property groups, control types, and validation rules.",
            Icon = "adjustments-horizontal",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-property-groups",
                    ModuleId = "pim-properties-data-modelling",
                    Title = "Understanding Property Groups",
                    Summary = "Learn how property groups organise product attributes into logical collections for easier management and data entry.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what property groups are and why they are important",
                        "Learn how to create and organise property groups",
                        "Know best practices for structuring property groups",
                        "Understand how property groups affect the product editing experience"
                    },
                    Content = @"
<h2>Property Groups in Optimizely PIM</h2>
<p>Property groups are <strong>logical containers</strong> that organise related product properties together. They serve as the foundation of your data model and directly impact how team members interact with product data in the PIM interface.</p>

<h3>What are Property Groups?</h3>
<p>Think of property groups as folders for your product attributes. Instead of presenting all product properties in a single flat list, property groups allow you to organise them into meaningful categories such as ""Dimensions"", ""Branding"", ""Technical Specifications"", or ""Marketing Content"".</p>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Why Groups Matter</p>
    <p class=""text-orange-700 dark:text-orange-300"">Well-designed property groups make data entry faster and more intuitive. When a merchandiser needs to update product dimensions, they can navigate directly to the ""Dimensions"" group rather than scrolling through hundreds of unrelated properties.</p>
</div>

<h3>Creating Property Groups</h3>
<p>Property groups are created in the <strong>Data Setup</strong> area of PIM. Each group requires:</p>
<ul>
    <li><strong>Name</strong> — A clear, descriptive name that team members will recognise</li>
    <li><strong>Description</strong> — An optional explanation of what properties belong in this group</li>
    <li><strong>Order</strong> — Controls the display sequence in the product editor</li>
</ul>

<h3>Best Practices for Property Groups</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Explanation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Keep groups focused</td><td class=""px-4 py-2"">Each group should contain 5-15 related properties. Too many dilutes the grouping benefit.</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Use business-friendly names</td><td class=""px-4 py-2"">Name groups for the business concepts they represent, not technical terms.</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Consider workflows</td><td class=""px-4 py-2"">Group properties that are typically updated by the same team or at the same time.</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Plan for filtering</td><td class=""px-4 py-2"">Property groups are used in the filtering system, so thoughtful grouping aids product discovery.</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Include a ""Common"" group</td><td class=""px-4 py-2"">Have a group for universal properties like name, SKU, and description that apply to all products.</td></tr>
    </tbody>
</table>

<h3>Example Property Group Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Property Groups
├── Common Attributes
│   ├── Product Name
│   ├── SKU
│   ├── Short Description
│   └── Long Description
├── Dimensions & Weight
│   ├── Width
│   ├── Height
│   ├── Depth
│   └── Weight
├── Branding
│   ├── Brand Name
│   ├── Manufacturer
│   └── Country of Origin
├── Technical Specifications
│   ├── Material
│   ├── Colour
│   ├── Voltage
│   └── Wattage
└── Marketing
    ├── Marketing Copy
    ├── Key Features
    └── Target Audience
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-property-types-controls",
                    ModuleId = "pim-properties-data-modelling",
                    Title = "Property Types & Control Types",
                    Summary = "Master the different property types available in PIM, including text fields, dropdowns, numbers, dates, and multi-select options.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Know all available property control types in PIM",
                        "Understand when to use each control type",
                        "Learn which control types support multi-language",
                        "Configure properties with appropriate control types for your data"
                    },
                    Content = @"
<h2>Property Types & Control Types</h2>
<p>Every property in PIM has a <strong>control type</strong> that determines how users interact with the data and what kind of values can be stored. Choosing the right control type is critical for data quality and user experience.</p>

<h3>Available Control Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Control Type</th>
            <th class=""px-4 py-2 text-left"">Data Stored</th>
            <th class=""px-4 py-2 text-left"">Multi-Language</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Text Field</td><td class=""px-4 py-2"">Short text string</td><td class=""px-4 py-2"">✅ Yes</td><td class=""px-4 py-2"">Product names, SKUs, short descriptions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Text Area</td><td class=""px-4 py-2"">Long text</td><td class=""px-4 py-2"">✅ Yes</td><td class=""px-4 py-2"">Full descriptions, marketing copy</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Rich Text</td><td class=""px-4 py-2"">Formatted HTML</td><td class=""px-4 py-2"">✅ Yes</td><td class=""px-4 py-2"">Detailed product descriptions with formatting</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Number</td><td class=""px-4 py-2"">Numeric value</td><td class=""px-4 py-2"">❌ No</td><td class=""px-4 py-2"">Dimensions, weights, quantities</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Date</td><td class=""px-4 py-2"">Date value</td><td class=""px-4 py-2"">❌ No</td><td class=""px-4 py-2"">Launch dates, expiry dates</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Date/Time</td><td class=""px-4 py-2"">Date and time</td><td class=""px-4 py-2"">❌ No</td><td class=""px-4 py-2"">Precise timestamps for events</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Boolean</td><td class=""px-4 py-2"">True/False</td><td class=""px-4 py-2"">❌ No</td><td class=""px-4 py-2"">Flags: is featured, is hazardous, is active</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Dropdown</td><td class=""px-4 py-2"">Single selection</td><td class=""px-4 py-2"">❌ No</td><td class=""px-4 py-2"">Brand, colour, material (predefined list)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Radio Button</td><td class=""px-4 py-2"">Single selection</td><td class=""px-4 py-2"">❌ No</td><td class=""px-4 py-2"">Size category, condition, rating tier</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Multi-Select</td><td class=""px-4 py-2"">Multiple selections</td><td class=""px-4 py-2"">❌ No</td><td class=""px-4 py-2"">Tags, certifications, applicable markets</td></tr>
    </tbody>
</table>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Multi-Language Note</p>
    <p class=""text-orange-700 dark:text-orange-300"">Only text-based control types (Text Field, Text Area, Rich Text) support multi-language values. List-based controls (Dropdown, Radio Button, Multi-Select) and numeric/date types do not support multi-language because their values are language-independent.</p>
</div>

<h3>Configuring a Property</h3>
<p>When creating a property, you define the following settings:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Name</strong> — The display name for the property</li>
        <li><strong>Control Type</strong> — How the property is rendered in the editor</li>
        <li><strong>Property Group</strong> — Which group the property belongs to</li>
        <li><strong>Multi-Language</strong> — Whether the property value varies by language</li>
        <li><strong>Requirement Level</strong> — Required, Recommended, or Optional</li>
        <li><strong>Validation Rules</strong> — Constraints on the data (e.g., min/max length, pattern matching)</li>
        <li><strong>Predefined Values</strong> — For dropdown, radio, and multi-select controls</li>
    </ul>
</div>

<h3>Choosing the Right Control Type</h3>
<p>Follow these guidelines when selecting a control type:</p>
<ul>
    <li>Use <strong>Text Field</strong> for short, free-form text that needs translation (product names, titles)</li>
    <li>Use <strong>Dropdown</strong> when there's a fixed list of options and only one can be selected (brand, material)</li>
    <li>Use <strong>Multi-Select</strong> when products can have multiple values from a list (certifications, features)</li>
    <li>Use <strong>Number</strong> for any measurable value (weight, width, voltage)</li>
    <li>Use <strong>Boolean</strong> for simple yes/no flags (is featured, is discontinued)</li>
    <li>Use <strong>Rich Text</strong> sparingly — only when formatting is needed (detailed descriptions)</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-validation-rules",
                    ModuleId = "pim-properties-data-modelling",
                    Title = "Validation Rules & Data Quality",
                    Summary = "Learn how to enforce data quality through validation rules, requirement levels, and system-enforced governance.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the three levels of data governance in PIM",
                        "Configure validation rules on properties",
                        "Set requirement levels (Required, Recommended, Optional)",
                        "Use completeness tracking to monitor data quality"
                    },
                    Content = @"
<h2>Data Quality & Governance in PIM</h2>
<p>One of the most powerful aspects of Optimizely PIM is its <strong>system-enforced data governance</strong>. Unlike spreadsheets where data quality relies on human discipline, PIM enforces quality rules at every level of the data model.</p>

<h3>Three Levels of Governance</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li>
            <strong>Property-Level Governance</strong>
            <p class=""ml-6 text-gray-600 dark:text-gray-400"">Defines valid data types, formats, and constraints at the property level. Invalid data cannot be imported or saved.</p>
        </li>
        <li>
            <strong>Template-Level Governance</strong>
            <p class=""ml-6 text-gray-600 dark:text-gray-400"">Product templates enforce which properties are required, recommended, or optional for each product type. Missing required properties prevent publication.</p>
        </li>
        <li>
            <strong>Workflow-Level Governance</strong>
            <p class=""ml-6 text-gray-600 dark:text-gray-400"">Approval workflows ensure that products are reviewed before publishing. Products must pass through defined status stages.</p>
        </li>
    </ol>
</div>

<h3>Property Validation Rules</h3>
<p>Validation rules are configured directly on properties to constrain what values can be entered:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Validation Type</th>
            <th class=""px-4 py-2 text-left"">Applies To</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Min/Max Length</td><td class=""px-4 py-2"">Text fields</td><td class=""px-4 py-2"">Product name must be 5-100 characters</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Min/Max Value</td><td class=""px-4 py-2"">Number fields</td><td class=""px-4 py-2"">Weight must be between 0.1 and 10000</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Pattern Matching</td><td class=""px-4 py-2"">Text fields</td><td class=""px-4 py-2"">SKU must match pattern: ABC-####</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Predefined Values</td><td class=""px-4 py-2"">Lists</td><td class=""px-4 py-2"">Colour must be one of: Red, Blue, Green, Black</td></tr>
    </tbody>
</table>

<h3>Requirement Levels</h3>
<p>Each property on a template can be set to one of three requirement levels:</p>
<ul>
    <li><strong>Required</strong> — Must be filled in before the product can be approved and published. Shown prominently in the editor.</li>
    <li><strong>Recommended</strong> — Should be filled in for completeness but does not block approval. Contributes to completeness percentage.</li>
    <li><strong>Optional</strong> — Nice to have but not necessary. Does not affect completeness calculations.</li>
</ul>

<h3>Completeness Tracking</h3>
<p>PIM automatically calculates <strong>product completeness</strong> based on the requirement levels defined in templates:</p>
<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Completeness Calculation</p>
    <p class=""text-orange-700 dark:text-orange-300"">Completeness percentage = (filled required + recommended properties) / (total required + recommended properties) × 100%. Optional properties do not factor into the calculation.</p>
</div>

<p>This completeness score is visible on:</p>
<ul>
    <li>The product detail page</li>
    <li>Product list views</li>
    <li>The dashboard statistics</li>
    <li>Export reports</li>
</ul>

<h3>Import-Time Validation</h3>
<p>Validation rules are also enforced during data imports. If imported data violates a validation rule:</p>
<ul>
    <li>The specific field value is rejected</li>
    <li>The import log records the validation error</li>
    <li>Other valid fields on the same product are still imported</li>
    <li>The product is flagged for manual review</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-configuring-properties",
                    ModuleId = "pim-properties-data-modelling",
                    Title = "Configuring Properties Step by Step",
                    Summary = "Walk through the process of creating and configuring properties with predefined values, validation, and multi-language support.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create new properties in PIM step by step",
                        "Configure predefined value lists for dropdown and multi-select properties",
                        "Set up multi-language support for text properties",
                        "Import properties in bulk using spreadsheets"
                    },
                    Content = @"
<h2>Creating & Configuring Properties</h2>
<p>Properties are created and managed in the <strong>Data Setup</strong> section of PIM. This lesson walks through the process of creating properties individually and in bulk.</p>

<h3>Creating a Property Manually</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to <strong>Data Setup</strong> in the main navigation</li>
        <li>Select the <strong>Properties</strong> tab</li>
        <li>Click <strong>Add Property</strong></li>
        <li>Enter the property <strong>Name</strong> (e.g., ""Brand"")</li>
        <li>Select the <strong>Control Type</strong> (e.g., Dropdown)</li>
        <li>Assign to a <strong>Property Group</strong> (e.g., ""Branding"")</li>
        <li>Set the <strong>Multi-Language</strong> toggle if applicable</li>
        <li>Configure <strong>Predefined Values</strong> if using a list control</li>
        <li>Save the property</li>
    </ol>
</div>

<h3>Configuring Predefined Values</h3>
<p>For Dropdown, Radio Button, and Multi-Select controls, you must define the list of valid values:</p>
<ul>
    <li><strong>Add values individually</strong> — Enter each value one at a time in the property editor</li>
    <li><strong>Import values</strong> — Upload a list of values from a spreadsheet for large lists</li>
    <li><strong>Template-specific values</strong> — Override the default value list for specific product templates</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Template-Specific Values</p>
    <p class=""text-orange-700 dark:text-orange-300"">A property like ""Size"" might have different valid values depending on the product template. A ""Shoes"" template might allow UK 3-12, while a ""Shirts"" template allows S, M, L, XL. PIM supports this through template-specific property values.</p>
</div>

<h3>Bulk Property Import</h3>
<p>For large data models, you can import properties and their configurations in bulk using a structured spreadsheet:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Column</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Property Name</td><td class=""px-4 py-2"">Name of the property</td><td class=""px-4 py-2"">Brand</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Control Type</td><td class=""px-4 py-2"">Type of editor control</td><td class=""px-4 py-2"">Dropdown</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Property Group</td><td class=""px-4 py-2"">Group assignment</td><td class=""px-4 py-2"">Branding</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Multi-Language</td><td class=""px-4 py-2"">Language support flag</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Values</td><td class=""px-4 py-2"">Predefined value list</td><td class=""px-4 py-2"">Nike;Adidas;Puma;Reebok</td></tr>
    </tbody>
</table>

<h3>Property Configuration Best Practices</h3>
<ul>
    <li><strong>Plan before creating</strong> — Map out all your properties and groups on paper or in a spreadsheet before configuring PIM</li>
    <li><strong>Start with common properties</strong> — Create universal properties (name, SKU, description) first</li>
    <li><strong>Use consistent naming</strong> — Follow a naming convention (e.g., always use ""Product Name"" not sometimes ""Name"" and sometimes ""Product Title"")</li>
    <li><strong>Limit predefined values</strong> — For dropdowns, keep the value list manageable. If a list exceeds 50 items, consider whether a text field with validation is more appropriate</li>
    <li><strong>Document your model</strong> — Maintain a data dictionary that describes each property's purpose and acceptable values</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 3: Product Templates

    private LearningModule BuildProductTemplatesModule()
    {
        return new LearningModule
        {
            Id = "pim-product-templates",
            Title = "Product Templates",
            Description = "Master product templates — the blueprints that define which properties and requirements apply to different types of products in your catalogue.",
            Icon = "document-duplicate",
            Order = 3,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-understanding-templates",
                    ModuleId = "pim-product-templates",
                    Title = "Understanding Product Templates",
                    Summary = "Learn what product templates are, how they define product structure, and why they are essential for data governance.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what product templates are and their role in PIM",
                        "Learn how templates enforce data quality through property requirements",
                        "Understand the relationship between templates, properties, and products",
                        "Know the difference between the Starter Template and custom templates"
                    },
                    Content = @"
<h2>Product Templates in PIM</h2>
<p>Product templates are the <strong>blueprints</strong> that define the structure and requirements for similar products. They determine which properties are available for a product type, which are required, and what constitutes a ""complete"" product record.</p>

<h3>What is a Product Template?</h3>
<p>A product template is a named collection of property assignments with associated requirement levels. When you create a product and assign it to a template, that product inherits all the properties defined by the template.</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium mb-2"">Example: A ""Running Shoes"" template might include:</p>
    <ul class=""space-y-1"">
        <li>✅ <strong>Required:</strong> Product Name, SKU, Brand, Size Range, Price, Main Image</li>
        <li>⚠️ <strong>Recommended:</strong> Colour, Material, Weight, Marketing Description</li>
        <li>ℹ️ <strong>Optional:</strong> Care Instructions, Video URL, Season</li>
    </ul>
</div>

<h3>The Starter Template</h3>
<p>Every PIM instance includes a <strong>Starter Template</strong> that comes pre-configured with a set of universal properties. This template:</p>
<ul>
    <li>Contains common properties that apply to virtually all products</li>
    <li>Cannot be deleted but can be customised</li>
    <li>Serves as the base for all new products until a custom template is assigned</li>
    <li>Properties from the Starter Template are available across all templates</li>
</ul>

<h3>Templates and Data Quality</h3>
<p>Templates are the primary mechanism for enforcing data quality at the product level:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Template Feature</th>
            <th class=""px-4 py-2 text-left"">Quality Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Required Properties</td><td class=""px-4 py-2"">Products cannot be approved without all required fields completed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recommended Properties</td><td class=""px-4 py-2"">Contribute to completeness % but don't block approval</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Template-Specific Values</td><td class=""px-4 py-2"">Override property value lists for context-appropriate options</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Completeness Calculation</td><td class=""px-4 py-2"">Automatically calculated per template requirements</td></tr>
    </tbody>
</table>

<h3>Template Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────┐
│                    STARTER TEMPLATE                   │
│  (Universal properties: Name, SKU, Description...)    │
├─────────────┬─────────────┬─────────────────────────┤
│  Running    │  Shirts     │  Electronics            │
│  Shoes      │  Template   │  Template               │
│  Template   │             │                         │
│             │             │                         │
│ + Size Range│ + Collar    │ + Voltage               │
│ + Cushioning│ + Fabric    │ + Wattage               │
│ + Drop (mm) │ + Fit Type  │ + Battery Life          │
│ + Terrain   │ + Sleeve    │ + Connectivity          │
└─────────────┴─────────────┴─────────────────────────┘
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-creating-templates",
                    ModuleId = "pim-product-templates",
                    Title = "Creating & Managing Templates",
                    Summary = "Step through the process of creating product templates, assigning properties, and managing template lifecycle.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create a new product template from scratch",
                        "Assign properties to templates with requirement levels",
                        "Configure template-specific property values",
                        "Import templates in bulk using spreadsheets"
                    },
                    Content = @"
<h2>Creating & Managing Product Templates</h2>
<p>Product templates are created and managed in the <strong>Data Setup</strong> area of PIM. Getting your templates right is one of the most important steps in the PIM configuration phase.</p>

<h3>Creating a Template</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to <strong>Data Setup</strong> → <strong>Product Templates</strong></li>
        <li>Click <strong>Add Template</strong></li>
        <li>Enter a <strong>Template Name</strong> (e.g., ""Running Shoes"")</li>
        <li>Optionally add a <strong>Description</strong> explaining what products this template is for</li>
        <li>Save the template</li>
    </ol>
</div>

<h3>Assigning Properties to Templates</h3>
<p>Once a template is created, you assign properties and set their requirement levels:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Open the template in the editor</li>
        <li>Click <strong>Add Properties</strong></li>
        <li>Select properties from the available list (filtered by property group)</li>
        <li>For each property, set the <strong>Requirement Level</strong>:
            <ul class=""ml-6 mt-1"">
                <li><strong>Required</strong> — Must be filled before approval</li>
                <li><strong>Recommended</strong> — Contributes to completeness</li>
                <li><strong>Optional</strong> — Available but not tracked</li>
            </ul>
        </li>
        <li>Save the template</li>
    </ol>
</div>

<h3>Template-Specific Property Values</h3>
<p>For list-based properties (Dropdown, Radio Button, Multi-Select), you can override the default value list on a per-template basis:</p>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Example: Size Property</p>
    <p class=""text-orange-700 dark:text-orange-300"">The ""Size"" property might have a global value list of all possible sizes. On the ""Running Shoes"" template, you restrict it to UK 3-12. On the ""Children's Shoes"" template, you restrict it to UK 1-6. This prevents data entry errors while reusing the same property.</p>
</div>

<h3>Bulk Template Import</h3>
<p>For complex catalogues with many templates, PIM supports bulk import using a multi-sheet spreadsheet:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Sheet</th>
            <th class=""px-4 py-2 text-left"">Content</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Templates</td><td class=""px-4 py-2"">Template names and descriptions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Property Assignments</td><td class=""px-4 py-2"">Which properties belong to which template, with requirement levels</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Template-Specific Values</td><td class=""px-4 py-2"">Overridden value lists for list-based properties per template</td></tr>
    </tbody>
</table>

<h3>Template Management Tips</h3>
<ul>
    <li><strong>Start broad, refine later</strong> — Create a few broad templates initially and split them as you identify specific needs</li>
    <li><strong>Don't over-template</strong> — If two product types share 90%+ of the same properties, consider using one template with optional properties rather than two separate templates</li>
    <li><strong>Use meaningful names</strong> — Template names should clearly indicate what products they cover (""Industrial Pumps"" not ""Template 3"")</li>
    <li><strong>Review required properties carefully</strong> — Too many required properties slow down data entry; too few compromise data quality</li>
    <li><strong>Document template usage</strong> — Keep a record of which product lines use which templates for onboarding new team members</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-template-auditing",
                    ModuleId = "pim-product-templates",
                    Title = "Template Auditing & Completeness",
                    Summary = "Learn how to audit products against template requirements and use completeness tracking to drive data quality improvements.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use completeness tracking to identify incomplete products",
                        "Audit products against template requirements",
                        "Generate reports on data quality by template",
                        "Drive data enrichment using completeness insights"
                    },
                    Content = @"
<h2>Template Auditing & Completeness</h2>
<p>Once templates are configured and products are assigned, PIM provides powerful auditing tools to track data quality across your entire catalogue.</p>

<h3>Completeness Tracking</h3>
<p>Every product's completeness is automatically calculated based on its assigned template:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p><strong>Completeness Formula:</strong></p>
    <p class=""font-mono text-sm mt-2"">Completeness % = (Filled Required + Filled Recommended) / (Total Required + Total Recommended) × 100%</p>
</div>

<p>Products with 100% completeness have all required and recommended properties filled. Products with lower completeness scores need attention before they can be published.</p>

<h3>Auditing Incomplete Products</h3>
<p>PIM provides several ways to find and fix incomplete products:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">How It Works</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Dashboard Statistics</td><td class=""px-4 py-2"">Shows completeness distribution across the catalogue</td><td class=""px-4 py-2"">High-level overview</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product List Filtering</td><td class=""px-4 py-2"">Filter products by completeness range or missing properties</td><td class=""px-4 py-2"">Finding specific gaps</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Export & Analysis</td><td class=""px-4 py-2"">Export product data with completeness scores for offline analysis</td><td class=""px-4 py-2"">Bulk review and reporting</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Empty Value Filters</td><td class=""px-4 py-2"">Use ""is empty"" filter condition to find products missing specific properties</td><td class=""px-4 py-2"">Targeted enrichment</td></tr>
    </tbody>
</table>

<h3>Driving Data Enrichment</h3>
<p>Completeness tracking is not just a reporting tool — it's a driver for structured data enrichment:</p>
<ul>
    <li><strong>Assign enrichment tasks by template</strong> — Different team members can focus on different product templates</li>
    <li><strong>Prioritise by impact</strong> — Focus on required properties first, then recommended</li>
    <li><strong>Use collections</strong> — Create saved filter sets (collections) for ""Incomplete Running Shoes"" or ""Missing Marketing Copy""</li>
    <li><strong>Track progress over time</strong> — Monitor how completeness scores improve as the team enriches data</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Enrichment Workflow Tip</p>
    <p class=""text-orange-700 dark:text-orange-300"">Create a collection for each template that filters for products below 80% completeness. Assign these collections to specific team members as their enrichment targets. This turns abstract data quality goals into concrete, assignable tasks.</p>
</div>

<h3>Template Modification Considerations</h3>
<p>When modifying templates that already have products assigned:</p>
<ul>
    <li><strong>Adding a new required property</strong> — All existing products will immediately show as less complete</li>
    <li><strong>Removing a property</strong> — Existing data for that property is retained but no longer visible in the template view</li>
    <li><strong>Changing requirement level</strong> — Completeness percentages recalculate automatically for all affected products</li>
    <li><strong>Changing template-specific values</strong> — Products with values no longer in the list may need manual correction</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 4: Catalog & Category Management

    private LearningModule BuildCatalogCategoryModule()
    {
        return new LearningModule
        {
            Id = "pim-catalog-categories",
            Title = "Catalog & Category Management",
            Description = "Learn how to organise products into browsable category hierarchies, manage category details, and structure your catalogue for optimal customer experience.",
            Icon = "folder-open",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-category-hierarchies",
                    ModuleId = "pim-catalog-categories",
                    Title = "Understanding Category Hierarchies",
                    Summary = "Learn how PIM organises products into category trees with unlimited nesting levels for flexible catalogue structures.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose and structure of category hierarchies in PIM",
                        "Learn how categories relate to products and commerce storefronts",
                        "Know the difference between PIM categories and commerce navigation",
                        "Plan a category hierarchy that serves both internal and customer needs"
                    },
                    Content = @"
<h2>Category Hierarchies in PIM</h2>
<p>Categories in Optimizely PIM provide a <strong>hierarchical structure</strong> for organising products into logical groups. Unlike flat tag-based systems, PIM supports <strong>unlimited levels of nesting</strong>, allowing you to create deep, multi-level category trees that mirror your business structure.</p>

<h3>What are Categories?</h3>
<p>Categories are tree-structured containers that group related products together. They serve multiple purposes:</p>
<ul>
    <li><strong>Organisation</strong> — Group products logically for internal management</li>
    <li><strong>Navigation</strong> — When synced to commerce, categories become the browsable navigation structure</li>
    <li><strong>Filtering</strong> — Use categories as filters to quickly find products in PIM</li>
    <li><strong>Permissions</strong> — Categories can help scope team member responsibilities</li>
</ul>

<h3>Category Tree Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Product Catalogue
├── Clothing
│   ├── Men's
│   │   ├── Shirts
│   │   ├── Trousers
│   │   └── Outerwear
│   ├── Women's
│   │   ├── Dresses
│   │   ├── Tops
│   │   └── Skirts
│   └── Children's
│       ├── Boys
│       └── Girls
├── Footwear
│   ├── Running
│   ├── Casual
│   └── Formal
├── Accessories
│   ├── Bags
│   ├── Watches
│   └── Jewellery
└── Electronics
    ├── Audio
    ├── Computing
    └── Smart Home
</pre>

<h3>Categories and Commerce Integration</h3>
<p>When PIM syncs with Configured Commerce, your category hierarchy translates directly into the storefront navigation:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">PIM Concept</th>
            <th class=""px-4 py-2 text-left"">Commerce Equivalent</th>
            <th class=""px-4 py-2 text-left"">Notes</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Category Tree</td><td class=""px-4 py-2"">Product Navigation</td><td class=""px-4 py-2"">Becomes the browse-by-category menu</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category Name</td><td class=""px-4 py-2"">Category Display Name</td><td class=""px-4 py-2"">Shown to customers in navigation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category URL Segment</td><td class=""px-4 py-2"">URL Path</td><td class=""px-4 py-2"">Forms the category page URL</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category Image</td><td class=""px-4 py-2"">Category Banner/Thumbnail</td><td class=""px-4 py-2"">Displayed on category pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product Assignment</td><td class=""px-4 py-2"">Product Listing</td><td class=""px-4 py-2"">Products appear in assigned categories</td></tr>
    </tbody>
</table>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Multiple Category Assignment</p>
    <p class=""text-orange-700 dark:text-orange-300"">A single product can be assigned to multiple categories. For example, a ""Smart Watch"" might appear in both ""Accessories > Watches"" and ""Electronics > Smart Home"". This allows customers to find products through multiple navigation paths.</p>
</div>

<h3>Planning Your Category Hierarchy</h3>
<p>Consider these factors when designing your category structure:</p>
<ul>
    <li><strong>Customer browsing patterns</strong> — How do your customers look for products? Design categories around their mental model.</li>
    <li><strong>Catalogue size</strong> — Deeper hierarchies help manage large catalogues; simpler structures work for smaller ones.</li>
    <li><strong>SEO considerations</strong> — Category URLs should be meaningful and keyword-rich.</li>
    <li><strong>Cross-selling opportunities</strong> — Consider whether products need to appear in multiple categories.</li>
    <li><strong>Seasonal or promotional groupings</strong> — You may need categories for temporary collections (e.g., ""Summer Sale"").</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-creating-managing-categories",
                    ModuleId = "pim-catalog-categories",
                    Title = "Creating & Managing Categories",
                    Summary = "Step through creating categories, configuring details, adding images, and assigning products to categories.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create and nest categories within the hierarchy",
                        "Configure category details including descriptions and URL segments",
                        "Add images to categories for visual representation",
                        "Assign and remove products from categories"
                    },
                    Content = @"
<h2>Creating & Managing Categories</h2>
<p>Category management in PIM is performed through the <strong>Categories</strong> section of the main navigation. Here you can build, edit, reorder, and delete categories throughout the hierarchy.</p>

<h3>Creating a Category</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to <strong>Categories</strong> in the main navigation</li>
        <li>To create a <strong>top-level category</strong>, click <strong>Add Category</strong> at the root level</li>
        <li>To create a <strong>sub-category</strong>, navigate to the parent category and click <strong>Add Sub-Category</strong></li>
        <li>Enter the <strong>Category Name</strong> (e.g., ""Men's Clothing"")</li>
        <li>Configure additional details (description, URL segment, image)</li>
        <li>Save the category</li>
    </ol>
</div>

<h3>Category Details</h3>
<p>Each category can have the following details configured:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Field</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Name</td><td class=""px-4 py-2"">Display name in PIM and commerce</td><td class=""px-4 py-2"">Men's Running Shoes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Description</td><td class=""px-4 py-2"">Category description for commerce pages</td><td class=""px-4 py-2"">Explore our range of men's running shoes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">URL Segment</td><td class=""px-4 py-2"">URL-friendly slug</td><td class=""px-4 py-2"">mens-running-shoes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Image</td><td class=""px-4 py-2"">Visual representation for listings</td><td class=""px-4 py-2"">category-running.jpg</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sort Order</td><td class=""px-4 py-2"">Display order among siblings</td><td class=""px-4 py-2"">1, 2, 3...</td></tr>
    </tbody>
</table>

<h3>Assigning Products to Categories</h3>
<p>Products can be assigned to categories in several ways:</p>
<ul>
    <li><strong>From the product</strong> — Open a product and assign it to categories in its detail page</li>
    <li><strong>From the category</strong> — Open a category and add products to it</li>
    <li><strong>Via import</strong> — Include category assignments in your product import file</li>
    <li><strong>Bulk operations</strong> — Select multiple products and assign them to a category simultaneously</li>
</ul>

<h3>Reordering and Reorganising</h3>
<p>Categories can be reorganised at any time:</p>
<ul>
    <li><strong>Reorder siblings</strong> — Drag categories up or down within the same level</li>
    <li><strong>Move to a new parent</strong> — Relocate a category (and its sub-categories) to a different position in the hierarchy</li>
    <li><strong>Delete a category</strong> — Remove a category (products remain in the system but lose that category assignment)</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Impact on Commerce</p>
    <p class=""text-orange-700 dark:text-orange-300"">Changes to the category hierarchy in PIM are reflected in Configured Commerce after the next sync job runs. Be mindful of reorganising categories on live sites, as it may affect customer navigation and bookmarked URLs.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-category-best-practices",
                    ModuleId = "pim-catalog-categories",
                    Title = "Category Strategy & Best Practices",
                    Summary = "Learn best practices for designing effective category structures that serve both internal teams and external customers.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Design category hierarchies optimised for customer browsing",
                        "Balance depth vs breadth in category structures",
                        "Handle seasonal, promotional, and cross-functional categories",
                        "Maintain category health as the catalogue grows"
                    },
                    Content = @"
<h2>Category Strategy & Best Practices</h2>
<p>A well-designed category hierarchy makes the difference between a catalogue that is easy to navigate and one that frustrates both internal teams and customers. This lesson covers strategies for building effective category structures.</p>

<h3>Depth vs Breadth</h3>
<p>One of the fundamental decisions in category design is the balance between deep hierarchies and broad ones:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Approach</th>
            <th class=""px-4 py-2 text-left"">Pros</th>
            <th class=""px-4 py-2 text-left"">Cons</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Deep (Many Levels)</td>
            <td class=""px-4 py-2"">Precise organisation, small product lists per category</td>
            <td class=""px-4 py-2"">More clicks to navigate, harder to maintain</td>
            <td class=""px-4 py-2"">Large catalogues (10,000+ products)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Broad (Few Levels)</td>
            <td class=""px-4 py-2"">Simple navigation, easy to maintain</td>
            <td class=""px-4 py-2"">Large product lists per category, less precise</td>
            <td class=""px-4 py-2"">Smaller catalogues (< 1,000 products)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Balanced (3-4 Levels)</td>
            <td class=""px-4 py-2"">Good mix of precision and simplicity</td>
            <td class=""px-4 py-2"">Requires careful planning</td>
            <td class=""px-4 py-2"">Most catalogues</td>
        </tr>
    </tbody>
</table>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Recommended Approach</p>
    <p class=""text-orange-700 dark:text-orange-300"">For most catalogues, aim for 3-4 levels of depth with 5-15 sub-categories per level. This keeps navigation intuitive while providing enough specificity. If any single category contains more than 200 products, consider adding sub-categories.</p>
</div>

<h3>Category Design Principles</h3>
<ul>
    <li><strong>Think like your customer</strong> — Name and organise categories based on how customers search, not how your warehouse is arranged</li>
    <li><strong>Be consistent</strong> — Use the same naming conventions and hierarchy patterns throughout the tree</li>
    <li><strong>Avoid overlap</strong> — Each product should have a clear ""home"" category, even if it also appears in secondary categories</li>
    <li><strong>Plan for growth</strong> — Leave room in the hierarchy for new product lines without requiring a restructure</li>
    <li><strong>Use URL segments wisely</strong> — Keep URL segments short, lowercase, and keyword-rich for SEO</li>
</ul>

<h3>Common Category Patterns</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium mb-2"">By Product Type (most common):</p>
    <p class=""text-sm"">Clothing → Men's → Shirts → Casual Shirts</p>
    <p class=""font-medium mb-2 mt-3"">By Use Case:</p>
    <p class=""text-sm"">Running → Shoes → Trail Running Shoes</p>
    <p class=""font-medium mb-2 mt-3"">By Brand:</p>
    <p class=""text-sm"">Nike → Running → Air Max Series</p>
    <p class=""font-medium mb-2 mt-3"">Hybrid (recommended for most B2B):</p>
    <p class=""text-sm"">Industrial Equipment → Pumps → Centrifugal Pumps → Brand X Models</p>
</div>

<h3>Maintenance Tips</h3>
<ul>
    <li>Review your category structure quarterly as the catalogue evolves</li>
    <li>Archive categories with no products rather than leaving them empty</li>
    <li>Monitor category page performance in commerce analytics to identify underperforming structures</li>
    <li>Involve merchandising and marketing teams in category planning, not just technical teams</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 5: Product Management

    private LearningModule BuildProductManagementModule()
    {
        return new LearningModule
        {
            Id = "pim-product-management",
            Title = "Product Management",
            Description = "Master the core product management workflows in PIM, including creating, editing, importing, and managing products through their lifecycle.",
            Icon = "cube",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-creating-products",
                    ModuleId = "pim-product-management",
                    Title = "Creating & Editing Products",
                    Summary = "Learn to create products individually, assign templates and categories, and edit product data through the PIM interface.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create new product records in PIM",
                        "Assign product templates and categories to products",
                        "Edit product properties using the grid and detail views",
                        "Understand how product data is structured in PIM"
                    },
                    Content = @"
<h2>Creating & Editing Products in PIM</h2>
<p>Products are the core entities in PIM. Each product record represents a single item in your catalogue, complete with all the properties defined by its assigned template, category assignments, digital assets, and variant relationships.</p>

<h3>Creating a Product</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to <strong>Products</strong> in the main navigation</li>
        <li>Click <strong>Add Product</strong></li>
        <li>Enter the <strong>Product Name</strong> and <strong>SKU</strong></li>
        <li>Select a <strong>Product Template</strong> (determines available properties)</li>
        <li>Assign to one or more <strong>Categories</strong></li>
        <li>Fill in the available properties as needed</li>
        <li>Save the product</li>
    </ol>
</div>

<h3>The Product Detail Page</h3>
<p>Each product has a detail page that organises all its data into sections:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Section</th>
            <th class=""px-4 py-2 text-left"">Content</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">General Information</td><td class=""px-4 py-2"">Product name, SKU, template, status, completeness</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Property Groups</td><td class=""px-4 py-2"">Properties organised by their assigned groups</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Categories</td><td class=""px-4 py-2"">Category assignments and hierarchy positions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Assets</td><td class=""px-4 py-2"">Associated images and documents</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Variants</td><td class=""px-4 py-2"">Child variant products (if applicable)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Related Products</td><td class=""px-4 py-2"">Product associations and relationships</td></tr>
    </tbody>
</table>

<h3>Editing in Grid View vs Detail View</h3>
<p>PIM provides two editing modes for product data:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Grid View (Product List)</p>
    <ul class=""mt-2 space-y-1"">
        <li>Spreadsheet-like interface for rapid data entry</li>
        <li>Edit multiple products simultaneously</li>
        <li>View and compare products side-by-side</li>
        <li>Best for bulk updates to specific properties</li>
    </ul>
</div>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4 mt-3"">
    <p class=""font-medium"">Detail View (Product Page)</p>
    <ul class=""mt-2 space-y-1"">
        <li>Full product record with all sections visible</li>
        <li>Access to assets, variants, and relationships</li>
        <li>Complete property group navigation</li>
        <li>Best for comprehensive editing of a single product</li>
    </ul>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Multi-Edit Tip</p>
    <p class=""text-orange-700 dark:text-orange-300"">The grid view is particularly powerful for multi-edit workflows. Select multiple products, then update a property across all of them simultaneously. This is significantly faster than editing each product individually.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-bulk-operations",
                    ModuleId = "pim-product-management",
                    Title = "Bulk Product Operations",
                    Summary = "Master bulk operations including multi-edit, bulk import, bulk export, and batch status changes for efficient catalogue management.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Perform multi-edit operations across multiple products",
                        "Import products in bulk from spreadsheets",
                        "Export products for offline analysis and editing",
                        "Execute batch status changes and category assignments"
                    },
                    Content = @"
<h2>Bulk Product Operations</h2>
<p>Managing product catalogues at scale requires efficient bulk operations. PIM provides several mechanisms for working with products in bulk, from multi-edit in the UI to file-based import and export.</p>

<h3>Multi-Edit in the Grid</h3>
<p>The grid view supports bulk editing through multi-select:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to the product list (grid view)</li>
        <li>Apply filters to narrow down the products you want to edit</li>
        <li>Select multiple products using checkboxes</li>
        <li>Choose a property to update</li>
        <li>Enter the new value — it applies to all selected products</li>
        <li>Save the changes</li>
    </ol>
</div>

<h3>Bulk Import</h3>
<p>PIM supports importing products from structured files in the following formats:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Format</th>
            <th class=""px-4 py-2 text-left"">Extensions</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Excel</td><td class=""px-4 py-2"">.xls, .xlsx</td><td class=""px-4 py-2"">Human-readable data with formatting</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">CSV</td><td class=""px-4 py-2"">.csv</td><td class=""px-4 py-2"">Large datasets, system-generated files</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">XML</td><td class=""px-4 py-2"">.xml</td><td class=""px-4 py-2"">Structured data with related entities (assets, categories)</td></tr>
    </tbody>
</table>

<h3>Import Workflow</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Prepare the file</strong> — Structure your data with columns matching PIM properties</li>
        <li><strong>Upload the file</strong> — Navigate to Products → Import and upload your file</li>
        <li><strong>Map columns</strong> — Map spreadsheet columns to PIM properties (saved for reuse)</li>
        <li><strong>Review mapping</strong> — Verify the mapping preview looks correct</li>
        <li><strong>Execute import</strong> — Run the import and monitor progress</li>
        <li><strong>Review results</strong> — Check the import log for errors and warnings</li>
    </ol>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Saved Data Mappings</p>
    <p class=""text-orange-700 dark:text-orange-300"">PIM saves your column-to-property mapping logic so you can reuse it for future imports. This is invaluable when you receive regular data feeds from suppliers or ERP systems with consistent file structures.</p>
</div>

<h3>Bulk Export</h3>
<p>Export products from PIM for offline analysis, sharing with external teams, or reimport after modification:</p>
<ul>
    <li><strong>Export all products</strong> — Full catalogue export</li>
    <li><strong>Export filtered list</strong> — Export only products matching current filters</li>
    <li><strong>Export selected products</strong> — Export specifically selected products</li>
    <li><strong>Choose properties</strong> — Export current view columns or all properties</li>
    <li><strong>Include related data</strong> — Optionally include assets, categories, and related products</li>
</ul>

<h3>Batch Status Changes</h3>
<p>Update the status of multiple products simultaneously:</p>
<ul>
    <li>Select products in the grid view</li>
    <li>Choose the target status from the batch actions menu</li>
    <li>Confirm the status change</li>
    <li>Products that don't meet the requirements for the target status are flagged</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-product-status-lifecycle",
                    ModuleId = "pim-product-management",
                    Title = "Product Status & Lifecycle",
                    Summary = "Understand the product status pipeline, lifecycle management, and how products move from draft through to published.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand each stage of the product status pipeline",
                        "Know the requirements for moving products between statuses",
                        "Manage product archiving and lifecycle",
                        "Use status-based workflows effectively"
                    },
                    Content = @"
<h2>Product Status & Lifecycle</h2>
<p>Every product in PIM follows a defined <strong>status pipeline</strong> that tracks its progression from initial creation through to publication on commerce storefronts. Understanding this pipeline is essential for managing product data efficiently.</p>

<h3>The Status Pipeline</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────┐     ┌─────────────┐     ┌────────────────┐     ┌──────────┐     ┌───────────┐
│  Draft   │────▶│ In Progress │────▶│  Submitted for │────▶│ Approved │────▶│ Published │
│          │     │             │     │   Approval     │     │          │     │           │
└──────────┘     └─────────────┘     └────────────────┘     └──────────┘     └───────────┘
                                           │                      ▲
                                           │     ┌──────────┐     │
                                           └────▶│ Rejected │─────┘
                                                 └──────────┘
</pre>

<h3>Status Definitions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Who Can Set</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Draft</td><td class=""px-4 py-2"">Product is newly created, data entry not started</td><td class=""px-4 py-2"">System (on creation)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">In Progress</td><td class=""px-4 py-2"">Product is being enriched with data</td><td class=""px-4 py-2"">Merchandiser, Manager, Admin</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Submitted for Approval</td><td class=""px-4 py-2"">Data entry complete, awaiting review</td><td class=""px-4 py-2"">Merchandiser, Manager, Admin</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Approved</td><td class=""px-4 py-2"">Data reviewed and approved for publishing</td><td class=""px-4 py-2"">Manager, Admin</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Rejected</td><td class=""px-4 py-2"">Data needs corrections before re-submission</td><td class=""px-4 py-2"">Manager, Admin</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Published</td><td class=""px-4 py-2"">Product data synced to commerce platform</td><td class=""px-4 py-2"">System (via sync job)</td></tr>
    </tbody>
</table>

<h3>Archiving Products</h3>
<p>Products that are no longer active can be <strong>archived</strong> rather than deleted:</p>
<ul>
    <li>Archived products are hidden from default product lists</li>
    <li>Product data is preserved for historical reference</li>
    <li>Archived products can be unarchived if needed later</li>
    <li>Archiving does not remove the product from commerce — you must separately deactivate it in the commerce platform</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Lifecycle Best Practice</p>
    <p class=""text-orange-700 dark:text-orange-300"">Establish clear guidelines for when products should be archived versus deleted. In most cases, archiving is preferred because it preserves the data record. Only delete products that were created in error.</p>
</div>

<h3>Product Completeness and Status</h3>
<p>Product completeness directly affects the status pipeline:</p>
<ul>
    <li>Products with missing <strong>required</strong> properties cannot be moved to ""Submitted for Approval""</li>
    <li>Approvers can see completeness scores to make informed decisions</li>
    <li>Products published with less than 100% recommended completeness may have gaps in their commerce listings</li>
    <li>The dashboard tracks products at each status stage, showing where bottlenecks exist</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-search-filter-collections",
                    ModuleId = "pim-product-management",
                    Title = "Search, Filters & Collections",
                    Summary = "Master PIM's powerful search and filtering system, including creating saved collections for streamlined product management.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Use full-text search to find products quickly",
                        "Apply property-based, status, and category filters",
                        "Create and manage collections (saved filter sets)",
                        "Combine filters for precise product discovery"
                    },
                    Content = @"
<h2>Search, Filters & Collections</h2>
<p>As your catalogue grows, finding and managing specific products becomes critical. PIM provides a powerful <strong>search and filtering system</strong> with the ability to save commonly used filter combinations as <strong>collections</strong>.</p>

<h3>Full-Text Search</h3>
<p>PIM's search bar provides real-time full-text search across all product data:</p>
<ul>
    <li>Search by product name, SKU, or any text property</li>
    <li>Results update in real-time as you type</li>
    <li>Search can be combined with active filters for precision</li>
</ul>

<h3>Filter Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Filter Type</th>
            <th class=""px-4 py-2 text-left"">How It Works</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Property Filters</td><td class=""px-4 py-2"">Filter on any configured property value</td><td class=""px-4 py-2"">Brand = ""Nike""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Status Filters</td><td class=""px-4 py-2"">Filter by product pipeline status</td><td class=""px-4 py-2"">Status = ""In Progress""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category Filters</td><td class=""px-4 py-2"">Filter by assigned category</td><td class=""px-4 py-2"">Category = ""Men's Clothing""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Template Filters</td><td class=""px-4 py-2"">Filter by product template</td><td class=""px-4 py-2"">Template = ""Running Shoes""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Completeness Filters</td><td class=""px-4 py-2"">Filter by data completeness range</td><td class=""px-4 py-2"">Completeness < 80%</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Empty Value Filters</td><td class=""px-4 py-2"">Find products missing specific properties</td><td class=""px-4 py-2"">Description ""is empty""</td></tr>
    </tbody>
</table>

<h3>Combining Filters</h3>
<p>Multiple filters can be applied simultaneously for precise product discovery:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium"">Example: Find all Nike running shoes that are in progress and missing descriptions</p>
    <ul class=""mt-2 space-y-1"">
        <li>Brand = ""Nike""</li>
        <li>Category = ""Running""</li>
        <li>Status = ""In Progress""</li>
        <li>Description = ""is empty""</li>
    </ul>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Filter Conflicts</p>
    <p class=""text-orange-700 dark:text-orange-300"">PIM prevents conflicting filter combinations. For example, you cannot filter for Brand = ""Nike"" AND Brand = ""Adidas"" simultaneously, as no product can match both conditions. The system will alert you if your filter combination is contradictory.</p>
</div>

<h3>Collections</h3>
<p>Collections are <strong>named, saved filter sets</strong> that can be reused and shared with team members:</p>
<ul>
    <li><strong>Create a collection</strong> — Apply your desired filters, then save them as a named collection</li>
    <li><strong>Quick access</strong> — Collections appear in the sidebar for one-click activation</li>
    <li><strong>Dashboard integration</strong> — Collections can be added as dashboard widgets for quick navigation</li>
    <li><strong>Team sharing</strong> — Collections are visible to all team members (not customer-facing)</li>
</ul>

<h3>Useful Collection Examples</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Collection Name</th>
            <th class=""px-4 py-2 text-left"">Filters</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Needs Approval</td><td class=""px-4 py-2"">Status = ""Submitted for Approval""</td><td class=""px-4 py-2"">Manager's review queue</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Incomplete Products</td><td class=""px-4 py-2"">Completeness < 80%</td><td class=""px-4 py-2"">Data enrichment priority list</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Missing Images</td><td class=""px-4 py-2"">Main Image = ""is empty""</td><td class=""px-4 py-2"">Asset team's work queue</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">New This Month</td><td class=""px-4 py-2"">Created Date > 30 days ago</td><td class=""px-4 py-2"">Recent product additions</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 6: Product Variants & Relationships

    private LearningModule BuildVariantsRelationshipsModule()
    {
        return new LearningModule
        {
            Id = "pim-variants-relationships",
            Title = "Product Variants & Relationships",
            Description = "Learn to manage product variants, parent-child hierarchies, and related product associations for complex catalogue structures.",
            Icon = "squares-2x2",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-understanding-variants",
                    ModuleId = "pim-variants-relationships",
                    Title = "Understanding Product Variants",
                    Summary = "Learn the parent-child variant model in PIM, including variant types, how variants are structured, and when to use them.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the parent-child variant model in PIM",
                        "Know when to use variants versus separate products",
                        "Learn how variant types work as reusable templates",
                        "Understand how variants translate to commerce storefronts"
                    },
                    Content = @"
<h2>Product Variants in PIM</h2>
<p>Product variants allow you to represent <strong>different versions of the same product</strong> within a single product grouping. For example, a shirt may come in multiple sizes and colours — each size/colour combination is a variant with its own SKU, while sharing the same base product information.</p>

<h3>The Parent-Child Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────┐
│           PARENT PRODUCT                         │
│  ""Classic Oxford Shirt""                          │
│  Shared: Description, Brand, Material, Images    │
├─────────────┬─────────────┬─────────────────────┤
│  VARIANT 1  │  VARIANT 2  │  VARIANT 3          │
│  Blue / S   │  Blue / M   │  Blue / L           │
│  SKU: OX-BS │  SKU: OX-BM │  SKU: OX-BL         │
├─────────────┼─────────────┼─────────────────────┤
│  VARIANT 4  │  VARIANT 5  │  VARIANT 6          │
│  White / S  │  White / M  │  White / L           │
│  SKU: OX-WS │  SKU: OX-WM │  SKU: OX-WL         │
└─────────────┴─────────────┴─────────────────────┘
</pre>

<h3>Parent vs Child Products</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Parent Product</th>
            <th class=""px-4 py-2 text-left"">Child (Variant) Product</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Purpose</td><td class=""px-4 py-2"">Represents the product grouping</td><td class=""px-4 py-2"">Represents a specific purchasable variation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">SKU</td><td class=""px-4 py-2"">May have a parent SKU</td><td class=""px-4 py-2"">Has a unique SKU per variant</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Shared Data</td><td class=""px-4 py-2"">Holds common data (description, brand)</td><td class=""px-4 py-2"">Inherits parent data, adds variant-specific values</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Commerce Display</td><td class=""px-4 py-2"">Product detail page</td><td class=""px-4 py-2"">Selection options on the detail page</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Pricing/Inventory</td><td class=""px-4 py-2"">May or may not have pricing</td><td class=""px-4 py-2"">Has variant-specific pricing and stock levels</td></tr>
    </tbody>
</table>

<h3>Variant Types</h3>
<p>PIM uses <strong>variant types</strong> as reusable templates that define which properties differentiate variants:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium mb-2"">Example Variant Types:</p>
    <ul class=""space-y-1"">
        <li><strong>Size</strong> — Variants differ by size (S, M, L, XL)</li>
        <li><strong>Colour</strong> — Variants differ by colour (Red, Blue, Green)</li>
        <li><strong>Size + Colour</strong> — Variants differ by both (creates a matrix of variants)</li>
        <li><strong>Packaging</strong> — Variants differ by pack size (1-pack, 6-pack, 12-pack)</li>
    </ul>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">When to Use Variants</p>
    <p class=""text-orange-700 dark:text-orange-300"">Use variants when products share the same description and imagery but differ in specific attributes like size, colour, or packaging. If two products have substantially different descriptions or images, they should be separate products rather than variants.</p>
</div>

<h3>Default Variant</h3>
<p>Each parent product can specify a <strong>default variant SKU</strong> — the variant that is displayed by default when a customer views the product on the commerce storefront. This is typically the most popular or representative variant.</p>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-creating-variants",
                    ModuleId = "pim-variants-relationships",
                    Title = "Creating & Managing Variants",
                    Summary = "Step through creating variant types, generating child products, and managing variant data across your catalogue.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create variant types for your product catalogue",
                        "Generate child variant products from a parent",
                        "Manage variant-specific data and properties",
                        "Import variants in bulk using spreadsheets"
                    },
                    Content = @"
<h2>Creating & Managing Variants</h2>
<p>Creating variants in PIM involves two steps: first define the <strong>variant type</strong> (the template for how products vary), then <strong>generate variants</strong> on specific parent products.</p>

<h3>Step 1: Create a Variant Type</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to <strong>Data Setup</strong> → <strong>Variant Types</strong></li>
        <li>Click <strong>Add Variant Type</strong></li>
        <li>Enter a name (e.g., ""Size and Colour"")</li>
        <li>Select the <strong>variant properties</strong> — the properties that differentiate variants (e.g., Size, Colour)</li>
        <li>Save the variant type</li>
    </ol>
</div>

<h3>Step 2: Generate Variants on a Parent Product</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Open the parent product in the detail view</li>
        <li>Navigate to the <strong>Variants</strong> section</li>
        <li>Select the <strong>Variant Type</strong> to use</li>
        <li>Configure the variant property values:
            <ul class=""ml-6 mt-1"">
                <li>Size: S, M, L, XL</li>
                <li>Colour: Blue, White, Black</li>
            </ul>
        </li>
        <li>Click <strong>Generate Variants</strong> — PIM creates all combinations (12 variants in this case)</li>
        <li>Review and adjust the generated variant SKUs</li>
        <li>Set the <strong>default variant</strong></li>
    </ol>
</div>

<h3>Variant Management</h3>
<p>Once created, variants can be managed individually or in bulk:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Action</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Edit variant data</td><td class=""px-4 py-2"">Update variant-specific properties (price, weight, images)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Add new variants</td><td class=""px-4 py-2"">Add variants to an existing parent (e.g., new colour)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Remove variants</td><td class=""px-4 py-2"">Delete variants that are no longer available</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Change default</td><td class=""px-4 py-2"">Update which variant displays by default in commerce</td></tr>
    </tbody>
</table>

<h3>Importing Variants in Bulk</h3>
<p>For large catalogues, variants can be imported via spreadsheet:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <p class=""font-medium mb-2"">Import file structure:</p>
    <table class=""min-w-full text-sm"">
        <thead>
            <tr>
                <th class=""px-2 py-1 text-left"">Parent SKU</th>
                <th class=""px-2 py-1 text-left"">Variant SKU</th>
                <th class=""px-2 py-1 text-left"">Size</th>
                <th class=""px-2 py-1 text-left"">Colour</th>
                <th class=""px-2 py-1 text-left"">Price</th>
            </tr>
        </thead>
        <tbody>
            <tr><td class=""px-2 py-1"">OX-SHIRT</td><td class=""px-2 py-1"">OX-BS</td><td class=""px-2 py-1"">S</td><td class=""px-2 py-1"">Blue</td><td class=""px-2 py-1"">49.99</td></tr>
            <tr><td class=""px-2 py-1"">OX-SHIRT</td><td class=""px-2 py-1"">OX-BM</td><td class=""px-2 py-1"">M</td><td class=""px-2 py-1"">Blue</td><td class=""px-2 py-1"">49.99</td></tr>
            <tr><td class=""px-2 py-1"">OX-SHIRT</td><td class=""px-2 py-1"">OX-BL</td><td class=""px-2 py-1"">L</td><td class=""px-2 py-1"">Blue</td><td class=""px-2 py-1"">49.99</td></tr>
        </tbody>
    </table>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Variant Import Tip</p>
    <p class=""text-orange-700 dark:text-orange-300"">When importing variants, the parent product must already exist in PIM (or be included in the same import). The parent SKU column links child variants to their parent product.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-related-products",
                    ModuleId = "pim-variants-relationships",
                    Title = "Related Products & Associations",
                    Summary = "Learn to create product relationships and associations for cross-selling, upselling, and accessory recommendations.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the types of product relationships in PIM",
                        "Create and manage related product associations",
                        "Import related product data in bulk",
                        "Know how related products sync to commerce storefronts"
                    },
                    Content = @"
<h2>Related Products & Associations</h2>
<p>Beyond the parent-child variant relationship, PIM supports <strong>product associations</strong> that link products together for cross-selling, upselling, and accessory recommendations on commerce storefronts.</p>

<h3>Types of Product Relationships</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Relationship Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Commerce Use</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Accessories</td><td class=""px-4 py-2"">Products that complement the main product</td><td class=""px-4 py-2"">""Customers also bought"" section</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cross-Sell</td><td class=""px-4 py-2"">Alternative or similar products</td><td class=""px-4 py-2"">""You may also like"" section</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Upsell</td><td class=""px-4 py-2"">Higher-value alternatives</td><td class=""px-4 py-2"">""Upgrade to"" suggestions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Replacement</td><td class=""px-4 py-2"">Products that replace the current product</td><td class=""px-4 py-2"">Successor product links</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Spare Parts</td><td class=""px-4 py-2"">Replacement parts for the main product</td><td class=""px-4 py-2"">Spare parts section (B2B)</td></tr>
    </tbody>
</table>

<h3>Creating Product Relationships</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Open the product detail page</li>
        <li>Navigate to the <strong>Related Products</strong> section</li>
        <li>Click <strong>Add Related Product</strong></li>
        <li>Select the <strong>relationship type</strong></li>
        <li>Search for and select the related product(s)</li>
        <li>Save the associations</li>
    </ol>
</div>

<h3>Importing Related Products</h3>
<p>Related product data can also be imported in bulk through the standard import process. Include a column mapping the source product SKU to the related product SKU with the relationship type.</p>

<h3>Relationships and Commerce Sync</h3>
<p>When PIM syncs with Configured Commerce, related product associations are transferred to the storefront:</p>
<ul>
    <li>Relationship types must be configured in PIM Settings to be included in the sync</li>
    <li>Commerce renders related products based on its own widget and page configuration</li>
    <li>Bi-directional relationships (if configured) show the association from both products</li>
    <li>Only approved and published products appear as related products on the storefront</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">B2B Cross-Selling Tip</p>
    <p class=""text-orange-700 dark:text-orange-300"">For B2B catalogues, spare parts and replacement product relationships are particularly valuable. A customer viewing an industrial pump should easily find replacement seals, filters, and maintenance kits. This increases order value and improves the customer experience.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 7: Digital Assets

    private LearningModule BuildDigitalAssetsModule()
    {
        return new LearningModule
        {
            Id = "pim-digital-assets",
            Title = "Digital Assets",
            Description = "Master digital asset management in PIM, including uploading, organising, and associating images and documents with products.",
            Icon = "photo",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-asset-management-overview",
                    ModuleId = "pim-digital-assets",
                    Title = "Digital Asset Management Overview",
                    Summary = "Understand how PIM manages digital assets, including images, documents, and media files associated with products.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the role of digital assets in PIM",
                        "Know the types of assets PIM supports",
                        "Learn how assets relate to products (many-to-many)",
                        "Understand asset metadata: name, URL, tags, and groups"
                    },
                    Content = @"
<h2>Digital Asset Management in PIM</h2>
<p>Digital assets in PIM include <strong>images, documents, manuals, regulatory disclosures, and other files</strong> that customers may need when evaluating or using products. PIM provides a dedicated asset management system that handles these files as first-class entities, independent of products.</p>

<h3>Key Asset Concepts</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Assets are independent entities</strong> — Each asset exists separately from products and can be associated with multiple products</li>
        <li><strong>Many-to-many relationships</strong> — One asset can be linked to many products, and one product can have many assets</li>
        <li><strong>Asset metadata</strong> — Each asset has a name, URL, tag, and group for organisation</li>
        <li><strong>URL hashing</strong> — File names are hashed to ensure unique static URLs, preventing URL changes during updates</li>
    </ul>
</div>

<h3>Types of Digital Assets</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Asset Type</th>
            <th class=""px-4 py-2 text-left"">Examples</th>
            <th class=""px-4 py-2 text-left"">Commerce Use</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Product Images</td><td class=""px-4 py-2"">Main image, gallery images, lifestyle shots</td><td class=""px-4 py-2"">Product detail page, listings, search results</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Specification Sheets</td><td class=""px-4 py-2"">Technical datasheets, spec PDFs</td><td class=""px-4 py-2"">Downloadable documents on product pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Manuals</td><td class=""px-4 py-2"">Installation guides, user manuals</td><td class=""px-4 py-2"">Documentation section</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Regulatory Documents</td><td class=""px-4 py-2"">Safety certifications, compliance docs</td><td class=""px-4 py-2"">Compliance/regulatory section</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Marketing Materials</td><td class=""px-4 py-2"">Brochures, promotional imagery</td><td class=""px-4 py-2"">Marketing content areas</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Videos</td><td class=""px-4 py-2"">Product demos, how-to videos (via URL)</td><td class=""px-4 py-2"">Video player on product pages</td></tr>
    </tbody>
</table>

<h3>Asset Record Structure</h3>
<p>Each asset in PIM has the following metadata:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Asset Record
├── Name          → ""Classic Oxford Shirt - Blue - Main Image""
├── URL           → ""https://cdn.example.com/assets/a1b2c3d4_main.jpg""
├── Tag           → ""primary-image""
├── Group         → ""Product Images""
├── File Hash     → ""a1b2c3d4"" (auto-generated)
└── Associations  → [""OX-SHIRT"", ""OX-SHIRT-BLUE""]
</pre>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Asset Reuse</p>
    <p class=""text-orange-700 dark:text-orange-300"">Because assets are independent entities, a single brand logo image can be associated with every product from that brand — rather than uploading the same image hundreds of times. This dramatically reduces storage and ensures consistency when the asset is updated.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-uploading-organising-assets",
                    ModuleId = "pim-digital-assets",
                    Title = "Uploading & Organising Assets",
                    Summary = "Learn to upload assets individually and in bulk, organise them with folders and tags, and associate them with products.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Upload assets individually and in bulk",
                        "Organise assets using folder structures and tags",
                        "Associate assets with products",
                        "Manage asset metadata and naming conventions"
                    },
                    Content = @"
<h2>Uploading & Organising Assets</h2>
<p>PIM provides several methods for getting assets into the system and keeping them organised for efficient management across your product catalogue.</p>

<h3>Uploading Individual Assets</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to <strong>Assets</strong> in the main navigation</li>
        <li>Click <strong>Upload Asset</strong></li>
        <li>Select the file from your local system</li>
        <li>Enter the asset <strong>Name</strong></li>
        <li>Assign a <strong>Tag</strong> (e.g., ""primary-image"", ""spec-sheet"")</li>
        <li>Assign to an <strong>Asset Group</strong> (folder)</li>
        <li>Save the asset</li>
    </ol>
</div>

<h3>Bulk Asset Upload</h3>
<p>For large volumes of assets, PIM supports bulk upload:</p>
<ul>
    <li><strong>Multi-file upload</strong> — Select multiple files simultaneously in the upload dialog</li>
    <li><strong>Asset metadata import</strong> — Upload a spreadsheet containing asset metadata (names, tags, groups, product associations)</li>
    <li><strong>ZIP archive upload</strong> — Upload a ZIP file containing multiple assets</li>
</ul>

<h3>Organising Assets with Folders</h3>
<p>Assets should be organised into a logical folder structure:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Recommended Folder Structure
├── Product Images
│   ├── Main Images
│   ├── Gallery Images
│   └── Lifestyle Photography
├── Technical Documents
│   ├── Specification Sheets
│   ├── Installation Guides
│   └── User Manuals
├── Regulatory
│   ├── Safety Certificates
│   └── Compliance Documents
├── Marketing
│   ├── Brochures
│   └── Campaign Assets
└── Brand Assets
    ├── Logos
    └── Brand Guidelines
</pre>

<h3>Associating Assets with Products</h3>
<p>Assets can be associated with products in several ways:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">How</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">From the product</td><td class=""px-4 py-2"">Open product → Assets section → Add Asset</td><td class=""px-4 py-2"">Adding a few assets to specific products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">From the asset</td><td class=""px-4 py-2"">Open asset → Associate with products</td><td class=""px-4 py-2"">Linking one asset to many products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Via import</td><td class=""px-4 py-2"">Include asset URLs and product SKUs in import file</td><td class=""px-4 py-2"">Bulk asset-product associations</td></tr>
    </tbody>
</table>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Asset Naming Convention</p>
    <p class=""text-orange-700 dark:text-orange-300"">Establish a consistent naming convention for assets early. A pattern like ""[SKU]-[type]-[sequence]"" (e.g., ""OX-SHIRT-main-01.jpg"") makes it easy to identify assets and automate associations during import.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-asset-updates-maintenance",
                    ModuleId = "pim-digital-assets",
                    Title = "Asset Updates & Maintenance",
                    Summary = "Learn to update existing assets, manage asset versions, and maintain asset quality across your catalogue.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Update existing assets without breaking product associations",
                        "Export and reimport asset metadata for bulk updates",
                        "Manage asset lifecycle and cleanup",
                        "Understand how asset changes propagate to commerce"
                    },
                    Content = @"
<h2>Asset Updates & Maintenance</h2>
<p>Over time, assets need to be updated — product photos are refreshed, spec sheets are revised, and regulatory documents are renewed. PIM supports asset updates while preserving product associations and maintaining URL stability.</p>

<h3>Updating an Asset</h3>
<p>When you need to update an asset (e.g., replace a product image with a higher quality version):</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Navigate to the asset in the Assets section</li>
        <li>Click <strong>Replace File</strong></li>
        <li>Upload the new version of the file</li>
        <li>PIM updates the file while preserving:
            <ul class=""ml-6 mt-1"">
                <li>All product associations</li>
                <li>Asset metadata (name, tag, group)</li>
                <li>The hashed URL (ensures CDN caching is invalidated correctly)</li>
            </ul>
        </li>
    </ol>
</div>

<h3>Bulk Asset Metadata Updates</h3>
<p>To update metadata (tags, groups, names) across many assets:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Export</strong> asset data to a spreadsheet</li>
        <li><strong>Modify</strong> the metadata in the spreadsheet</li>
        <li><strong>Reimport</strong> the updated spreadsheet</li>
        <li>PIM matches assets by URL or identifier and updates the metadata</li>
    </ol>
</div>

<h3>Asset Cleanup</h3>
<p>Periodically review your asset library for:</p>
<ul>
    <li><strong>Orphaned assets</strong> — Assets not associated with any product (may indicate cleanup needed or missing associations)</li>
    <li><strong>Duplicate assets</strong> — Multiple copies of the same file (consolidate to a single asset with multiple product associations)</li>
    <li><strong>Outdated assets</strong> — Old product photos or expired documents that should be replaced or removed</li>
    <li><strong>Missing assets</strong> — Products without any images (use the ""is empty"" filter on the main image property)</li>
</ul>

<h3>Asset Sync to Commerce</h3>
<p>When the PIM sync job runs, asset changes propagate to Configured Commerce:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Change</th>
            <th class=""px-4 py-2 text-left"">Commerce Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">New asset added</td><td class=""px-4 py-2"">Appears on associated product pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Asset file replaced</td><td class=""px-4 py-2"">New image/file displayed on product pages</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Asset removed from product</td><td class=""px-4 py-2"">No longer displayed on that product's page</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Asset deleted</td><td class=""px-4 py-2"">Removed from all associated product pages</td></tr>
    </tbody>
</table>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Image Sizing</p>
    <p class=""text-orange-700 dark:text-orange-300"">PIM system settings allow you to configure image sizing requirements. Set these early in your implementation to ensure all uploaded images meet your commerce storefront's requirements for thumbnails, product detail images, and gallery views.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 8: Import, Export & API Integration

    private LearningModule BuildImportExportApiModule()
    {
        return new LearningModule
        {
            Id = "pim-import-export-api",
            Title = "Import, Export & API Integration",
            Description = "Learn to import and export product data in bulk, use the Service API for automation, and integrate PIM with external systems.",
            Icon = "arrow-path",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-import-export-formats",
                    ModuleId = "pim-import-export-api",
                    Title = "Import & Export File Formats",
                    Summary = "Master the supported file formats for PIM import and export, including Excel, CSV, and XML with their respective strengths.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Know all supported import/export file formats in PIM",
                        "Understand when to use each format",
                        "Structure import files correctly for different data types",
                        "Handle related data (assets, categories) in XML exports"
                    },
                    Content = @"
<h2>Import & Export File Formats</h2>
<p>PIM supports several file formats for data exchange, each suited to different scenarios. Understanding these formats and their capabilities is essential for efficient data management.</p>

<h3>Supported Formats</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Format</th>
            <th class=""px-4 py-2 text-left"">Import</th>
            <th class=""px-4 py-2 text-left"">Export</th>
            <th class=""px-4 py-2 text-left"">Related Data</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Excel (.xlsx/.xls)</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">Limited</td><td class=""px-4 py-2"">Human-readable, team collaboration</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">CSV (.csv)</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">Limited</td><td class=""px-4 py-2"">Large datasets, system integration</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">XML (.xml)</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">✅ Full</td><td class=""px-4 py-2"">Complete data with assets, categories, relationships</td></tr>
    </tbody>
</table>

<h3>Excel/CSV Import Structure</h3>
<p>When importing with Excel or CSV, structure your file with columns matching PIM properties:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <table class=""min-w-full text-sm"">
        <thead>
            <tr>
                <th class=""px-2 py-1 text-left"">SKU</th>
                <th class=""px-2 py-1 text-left"">Product Name</th>
                <th class=""px-2 py-1 text-left"">Brand</th>
                <th class=""px-2 py-1 text-left"">Description</th>
                <th class=""px-2 py-1 text-left"">Price</th>
                <th class=""px-2 py-1 text-left"">Weight</th>
            </tr>
        </thead>
        <tbody>
            <tr><td class=""px-2 py-1"">SKU-001</td><td class=""px-2 py-1"">Widget Pro</td><td class=""px-2 py-1"">Acme</td><td class=""px-2 py-1"">Professional widget...</td><td class=""px-2 py-1"">29.99</td><td class=""px-2 py-1"">0.5</td></tr>
            <tr><td class=""px-2 py-1"">SKU-002</td><td class=""px-2 py-1"">Widget Lite</td><td class=""px-2 py-1"">Acme</td><td class=""px-2 py-1"">Budget-friendly...</td><td class=""px-2 py-1"">19.99</td><td class=""px-2 py-1"">0.3</td></tr>
        </tbody>
    </table>
</div>

<h3>Data Mapping</h3>
<p>During import, PIM maps your file columns to product properties:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Upload the file</li>
        <li>PIM presents a mapping interface showing each column</li>
        <li>Map each column to the corresponding PIM property</li>
        <li>Set the <strong>key column</strong> (usually SKU) for matching existing products</li>
        <li><strong>Save the mapping</strong> for reuse with future imports from the same source</li>
    </ol>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Saved Mappings</p>
    <p class=""text-orange-700 dark:text-orange-300"">PIM saves your data mapping logic so you can reuse it for future imports from the same data source. This is especially valuable for recurring imports from ERP systems or supplier feeds with consistent file structures.</p>
</div>

<h3>Export Options</h3>
<p>When exporting, you can control what data is included:</p>
<ul>
    <li><strong>Scope</strong>: All products, filtered list, or selected products</li>
    <li><strong>Properties</strong>: Current view columns or all properties</li>
    <li><strong>Related data</strong>: Optionally include assets, categories, related products</li>
    <li><strong>Format</strong>: XLSX or XML</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-service-api",
                    ModuleId = "pim-import-export-api",
                    Title = "The PIM Service API",
                    Summary = "Learn to use the PIM Service API for programmatic access, including authentication, data retrieval, and automated imports.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand the PIM Service API architecture and capabilities",
                        "Authenticate with the API using AppKey and AppSecret",
                        "Trigger bulk imports via the API",
                        "Monitor import job status programmatically"
                    },
                    Content = @"
<h2>The PIM Service API</h2>
<p>Optimizely PIM provides a <strong>REST-based Service API</strong> that enables system integrators and developers to programmatically interact with product data. The API is essential for automating imports, integrating with external systems, and building custom data pipelines.</p>

<h3>API Overview</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Capability</th>
            <th class=""px-4 py-2 text-left"">Read-Only</th>
            <th class=""px-4 py-2 text-left"">Write</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Retrieve product data</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">✅</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Query schema/metadata</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">✅</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Trigger data imports</td><td class=""px-4 py-2"">❌</td><td class=""px-4 py-2"">✅</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Check import status</td><td class=""px-4 py-2"">✅</td><td class=""px-4 py-2"">✅</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Import data into PIM</td><td class=""px-4 py-2"">❌</td><td class=""px-4 py-2"">✅</td></tr>
    </tbody>
</table>

<h3>Getting API Credentials</h3>
<p>API access requires credentials that are provisioned through Optimizely Support:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Submit a support ticket to Optimizely requesting PIM API access</li>
        <li>Specify whether you need <strong>read-only</strong> or <strong>write</strong> credentials</li>
        <li>Receive your <strong>AppKey</strong> and <strong>AppSecret</strong></li>
        <li>Store these credentials securely — they provide API access to your PIM data</li>
    </ol>
</div>

<h3>Authentication</h3>
<p>The API uses token-based authentication. First, obtain an access token:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
POST /episerverapi/token

Content-Type: application/x-www-form-urlencoded

grant_type=password&
username={AppKey}&
password={AppSecret}

Response:
{
    ""access_token"": ""eyJhbGciOiJIUzI1NiIs..."",
    ""token_type"": ""bearer"",
    ""expires_in"": 3600
}
</pre>

<h3>Triggering an Import</h3>
<p>With write credentials, you can trigger a bulk import via the API:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
POST /episerverapi/commerce/import/{import_type}

Authorization: Bearer {access_token}
Content-Type: multipart/form-data

Body: [import file]

Response:
{
    ""jobId"": ""abc123-def456-ghi789""
}
</pre>

<h3>Checking Import Status</h3>
<p>Monitor the progress of an import job:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
GET /episerverapi/commerce/task/{jobId}/status

Authorization: Bearer {access_token}

Response:
{
    ""jobId"": ""abc123-def456-ghi789"",
    ""status"": ""InProgress"",
    ""progress"": 65,
    ""recordsProcessed"": 650,
    ""recordsTotal"": 1000,
    ""errors"": 3
}
</pre>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">API Resources</p>
    <p class=""text-orange-700 dark:text-orange-300"">The official PIM API documentation and code samples are available on GitHub at <strong>github.com/episerver/pim-api</strong>. This repository includes authentication examples, import triggers, and status monitoring code in multiple languages.</p>
</div>

<h3>Common API Use Cases</h3>
<ul>
    <li><strong>Nightly ERP sync</strong> — Automated job that exports products from ERP and imports into PIM</li>
    <li><strong>Supplier data integration</strong> — Process supplier data feeds and push them into PIM</li>
    <li><strong>Custom reporting</strong> — Pull product data via API for custom dashboards and analytics</li>
    <li><strong>Workflow automation</strong> — Build custom scripts that trigger actions based on product status changes</li>
    <li><strong>Multi-system orchestration</strong> — Coordinate PIM data with DAM, CMS, and other systems</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "pim-api-auth-example",
                            Title = "API Authentication Example",
                            Description = "Example of authenticating with the PIM Service API using cURL",
                            Type = ExampleType.Code,
                            ExampleContent = @"# Obtain an access token
curl -X POST https://your-pim-instance.com/episerverapi/token \
  -H ""Content-Type: application/x-www-form-urlencoded"" \
  -d ""grant_type=password&username=YOUR_APP_KEY&password=YOUR_APP_SECRET""

# Use the token to query products
curl -X GET https://your-pim-instance.com/episerverapi/commerce/products \
  -H ""Authorization: Bearer YOUR_ACCESS_TOKEN""

# Trigger an import
curl -X POST https://your-pim-instance.com/episerverapi/commerce/import/product \
  -H ""Authorization: Bearer YOUR_ACCESS_TOKEN"" \
  -F ""file=@products.xlsx""

# Check import status
curl -X GET https://your-pim-instance.com/episerverapi/commerce/task/JOB_ID/status \
  -H ""Authorization: Bearer YOUR_ACCESS_TOKEN""",
                            ExpectedResultDescription = "Each API call returns a JSON response with the requested data or job status.",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "pim-automation-integration",
                    ModuleId = "pim-import-export-api",
                    Title = "Automation & External Integration",
                    Summary = "Design automated data pipelines and integrate PIM with ERP, DAM, and other enterprise systems.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Design automated data pipelines between PIM and external systems",
                        "Integrate PIM with ERP systems for product data synchronisation",
                        "Plan DAM and CMS integration strategies",
                        "Handle error recovery and monitoring in automated workflows"
                    },
                    Content = @"
<h2>Automation & External Integration</h2>
<p>PIM is most powerful when integrated into your broader enterprise data ecosystem. This lesson covers strategies for building automated data pipelines that keep PIM synchronised with ERP, DAM, and other systems.</p>

<h3>Integration Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────────────────────────────────────────────────────────────┐
│                    INTEGRATION ARCHITECTURE                       │
│                                                                   │
│  ┌─────────┐     ┌──────────────┐     ┌──────────────────────┐   │
│  │   ERP   │────▶│  Integration │────▶│    Optimizely PIM    │   │
│  │ (Source) │     │    Layer     │     │                      │   │
│  └─────────┘     │              │     │  ┌────────────────┐  │   │
│                  │  • ETL       │     │  │ Service API    │  │   │
│  ┌─────────┐     │  • Transform │     │  └────────────────┘  │   │
│  │   DAM   │────▶│  • Validate  │     │         │            │   │
│  │ (Assets)│     │  • Schedule  │     │         ▼            │   │
│  └─────────┘     │  • Monitor   │     │  ┌────────────────┐  │   │
│                  │              │     │  │  PIM Sync Job  │  │   │
│  ┌─────────┐     └──────────────┘     │  └────────────────┘  │   │
│  │Supplier │                          │         │            │   │
│  │ Feeds   │──────────────────────────│         ▼            │   │
│  └─────────┘                          │  ┌────────────────┐  │   │
│                                       │  │  Configured    │  │   │
│                                       │  │  Commerce      │  │   │
│                                       │  └────────────────┘  │   │
│                                       └──────────────────────┘   │
└──────────────────────────────────────────────────────────────────┘
</pre>

<h3>ERP Integration Patterns</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Pattern</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Frequency</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Nightly Full Sync</td><td class=""px-4 py-2"">Export all products from ERP, import into PIM</td><td class=""px-4 py-2"">Daily (off-hours)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Delta/Incremental Sync</td><td class=""px-4 py-2"">Only sync products changed since last run</td><td class=""px-4 py-2"">Hourly or more frequent</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Event-Driven Sync</td><td class=""px-4 py-2"">Push updates to PIM when ERP records change</td><td class=""px-4 py-2"">Real-time or near real-time</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Manual Trigger</td><td class=""px-4 py-2"">Team member initiates sync on demand</td><td class=""px-4 py-2"">As needed</td></tr>
    </tbody>
</table>

<h3>Building an Automated Pipeline</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Extract</strong> — Pull data from source system (ERP, supplier feed, DAM)</li>
        <li><strong>Transform</strong> — Map source fields to PIM properties, validate data, handle encoding</li>
        <li><strong>Load</strong> — Upload the file to PIM via the Service API import endpoint</li>
        <li><strong>Monitor</strong> — Poll the import status endpoint until completion</li>
        <li><strong>Handle Errors</strong> — Log errors, retry failures, alert on critical issues</li>
        <li><strong>Report</strong> — Send summary reports of what was imported/updated</li>
    </ol>
</div>

<h3>Error Handling Strategies</h3>
<ul>
    <li><strong>Validation before import</strong> — Validate data against PIM's property rules before attempting import to catch issues early</li>
    <li><strong>Partial failure handling</strong> — PIM imports valid records and rejects invalid ones. Design your pipeline to process the rejection log</li>
    <li><strong>Retry logic</strong> — Implement exponential backoff for transient API errors</li>
    <li><strong>Alerting</strong> — Set up alerts for import failures, high error rates, or missing expected data</li>
    <li><strong>Audit logging</strong> — Log every import operation with timestamps, record counts, and error summaries</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Integration Best Practice</p>
    <p class=""text-orange-700 dark:text-orange-300"">Never allow external systems to push data directly into PIM without an intermediate validation and transformation layer. This ""integration layer"" protects PIM from bad data and gives you a single place to manage data mapping, quality rules, and error handling.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 9: Multi-Language & Localisation

    private LearningModule BuildMultiLanguageModule()
    {
        return new LearningModule
        {
            Id = "pim-multi-language",
            Title = "Multi-Language & Localisation",
            Description = "Configure multi-language support, manage translations, and publish localised product data across global markets.",
            Icon = "language",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-language-configuration",
                    ModuleId = "pim-multi-language",
                    Title = "Language Configuration",
                    Summary = "Learn how to configure multi-language support in PIM, including enabling languages and understanding the relationship with Configured Commerce language settings.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand how multi-language support works in PIM",
                        "Configure languages in PIM and Configured Commerce",
                        "Know the relationship between commerce and PIM language settings",
                        "Enable additional languages beyond the default"
                    },
                    Content = @"
<h2>Language Configuration in PIM</h2>
<p>Optimizely PIM supports <strong>multi-language product data</strong>, allowing you to manage product information in multiple languages for global markets. Language configuration in PIM is closely tied to your Configured Commerce language settings.</p>

<h3>How Multi-Language Works</h3>
<p>Multi-language in PIM operates at the <strong>property level</strong>. When a property is flagged as ""multi-language"", each product stores separate values for each enabled language:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Product: ""Classic Oxford Shirt"" (SKU: OX-SHIRT)
├── Product Name
│   ├── English:  ""Classic Oxford Shirt""
│   ├── French:   ""Chemise Oxford Classique""
│   ├── German:   ""Klassisches Oxford-Hemd""
│   └── Spanish:  ""Camisa Oxford Clásica""
├── Description
│   ├── English:  ""A timeless cotton shirt...""
│   ├── French:   ""Une chemise en coton intemporelle...""
│   ├── German:   ""Ein zeitloses Baumwollhemd...""
│   └── Spanish:  ""Una camisa de algodón atemporal...""
├── SKU: OX-SHIRT          (not multi-language)
├── Price: 49.99            (not multi-language)
└── Weight: 0.3             (not multi-language)
</pre>

<h3>Language Setup Process</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Enable languages in Configured Commerce</strong> — Languages must first be configured in the commerce platform</li>
        <li><strong>Sync language configuration to PIM</strong> — PIM inherits the language list from Configured Commerce</li>
        <li><strong>Enable languages in PIM</strong> — Activate specific languages for PIM product data</li>
        <li><strong>Mark properties as multi-language</strong> — Flag which properties need translations</li>
    </ol>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Language Limitation</p>
    <p class=""text-orange-700 dark:text-orange-300"">Only text-based property types support multi-language: Text Field, Text Area, and Rich Text. Number, Date, Date/Time, Boolean, and list-based properties (Dropdown, Radio Button, Multi-Select) do not support multi-language because their values are language-independent.</p>
</div>

<h3>Default Language</h3>
<p>PIM has a default language that serves as the primary data entry language. All product data is initially entered in the default language, and translations are added subsequently for additional languages.</p>

<h3>Language and Commerce Sync</h3>
<p>When PIM syncs with Configured Commerce, language-specific data is transferred per language:</p>
<ul>
    <li>Each enabled language's product data syncs independently</li>
    <li>Commerce displays the appropriate language based on the customer's locale</li>
    <li>Missing translations may result in fallback to the default language (depending on commerce configuration)</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-translation-workflows",
                    ModuleId = "pim-multi-language",
                    Title = "Translation Workflows",
                    Summary = "Master translation workflows in PIM, including export for translation, in-app editing, and bulk translation management.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Export product data for external translation",
                        "Use in-app multi-language editing capabilities",
                        "Import translated content back into PIM",
                        "Track translation completeness across languages"
                    },
                    Content = @"
<h2>Translation Workflows</h2>
<p>Managing translations for hundreds or thousands of products requires structured workflows. PIM provides several approaches to translation management, from in-app editing to bulk export/import.</p>

<h3>Translation Approaches</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Approach</th>
            <th class=""px-4 py-2 text-left"">How It Works</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">In-App Translation</td><td class=""px-4 py-2"">Edit translations directly in PIM's product editor</td><td class=""px-4 py-2"">Small volumes, quick updates</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Export → Translate → Import</td><td class=""px-4 py-2"">Export data, send to translators, reimport</td><td class=""px-4 py-2"">Large volumes, professional translation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">API-Based</td><td class=""px-4 py-2"">Use the Service API to push/pull translations</td><td class=""px-4 py-2"">Integration with TMS systems</td></tr>
    </tbody>
</table>

<h3>In-App Translation</h3>
<p>PIM supports in-app multi-language editing directly within the product detail page:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Open a product in the detail view</li>
        <li>Select the <strong>language selector</strong> to switch to the target language</li>
        <li>Multi-language properties show the default language value as reference</li>
        <li>Enter the translated value in the target language</li>
        <li>Save — the translation is stored alongside the default language value</li>
    </ol>
</div>

<h3>Export → Translate → Import Workflow</h3>
<p>For professional translation at scale:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Export</strong> products with multi-language properties to Excel/CSV</li>
        <li><strong>Send</strong> the export file to your translation agency or team</li>
        <li><strong>Translators</strong> fill in the translation columns for each language</li>
        <li><strong>Import</strong> the completed file back into PIM</li>
        <li><strong>Review</strong> imported translations for accuracy</li>
    </ol>
</div>

<h3>Translation Completeness</h3>
<p>PIM tracks translation completeness per language, similar to property completeness:</p>
<ul>
    <li>Products show how many multi-language properties have been translated per language</li>
    <li>Dashboard can display translation progress across the catalogue</li>
    <li>Filters can find products missing translations for specific languages</li>
</ul>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Publication Requirements</p>
    <p class=""text-orange-700 dark:text-orange-300"">You can configure <strong>""Required to Publish""</strong> settings per language. This prevents products from being published to a language-specific storefront if critical translations are missing. For example, you might require Product Name and Description to be translated before publishing to the French storefront.</p>
</div>

<h3>Multi-Language Export Limitations</h3>
<p>Note that properties using list-based controls (Dropdown, Radio Button, Multi-Select) are excluded from multi-language exports because their values are not translatable — they use the same predefined value regardless of language.</p>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-global-catalogue-strategy",
                    ModuleId = "pim-multi-language",
                    Title = "Global Catalogue Strategy",
                    Summary = "Plan and implement a multi-language, multi-market product catalogue strategy using PIM's localisation capabilities.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Design a global catalogue strategy using PIM",
                        "Handle market-specific product variations",
                        "Plan translation workflows that scale",
                        "Manage regional compliance and regulatory requirements"
                    },
                    Content = @"
<h2>Global Catalogue Strategy</h2>
<p>For organisations selling across multiple markets and languages, PIM provides the foundation for a <strong>unified global product catalogue</strong> that adapts to local requirements while maintaining consistency.</p>

<h3>Global vs Local Product Data</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Data Type</th>
            <th class=""px-4 py-2 text-left"">Scope</th>
            <th class=""px-4 py-2 text-left"">Example</th>
            <th class=""px-4 py-2 text-left"">PIM Handling</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Universal Properties</td><td class=""px-4 py-2"">Global</td><td class=""px-4 py-2"">SKU, weight, dimensions</td><td class=""px-4 py-2"">Non-multi-language properties</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Translatable Content</td><td class=""px-4 py-2"">Per language</td><td class=""px-4 py-2"">Name, description, marketing copy</td><td class=""px-4 py-2"">Multi-language properties</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Market-Specific Pricing</td><td class=""px-4 py-2"">Per market</td><td class=""px-4 py-2"">Currency, price, tax rates</td><td class=""px-4 py-2"">Managed via commerce integration</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Regional Compliance</td><td class=""px-4 py-2"">Per region</td><td class=""px-4 py-2"">Safety certifications, regulatory docs</td><td class=""px-4 py-2"">Region-specific properties and assets</td></tr>
    </tbody>
</table>

<h3>Scaling Translation Workflows</h3>
<p>As your language count grows, translation management becomes a significant operational task. Consider these strategies:</p>
<ul>
    <li><strong>Prioritise languages</strong> — Start with your highest-revenue markets and add languages incrementally</li>
    <li><strong>Use professional translation services</strong> — For marketing content, professional human translation is worth the investment</li>
    <li><strong>Batch translation requests</strong> — Group new/updated products and send translation batches weekly rather than per-product</li>
    <li><strong>Set up translation reviews</strong> — Have in-market team members review translations for quality and cultural appropriateness</li>
    <li><strong>Automate where possible</strong> — Use the Service API to integrate with Translation Management Systems (TMS)</li>
</ul>

<h3>Multi-Market Publishing</h3>
<p>Configure PIM to publish to multiple Configured Commerce instances or websites:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Single PIM, multiple storefronts</strong> — One PIM instance manages product data for all markets</li>
        <li><strong>Language-gated publishing</strong> — Products only publish to a market when required translations are complete</li>
        <li><strong>Market-specific categories</strong> — Category structures can vary by market (though managed from the same PIM)</li>
        <li><strong>Regional compliance gates</strong> — Products require market-specific regulatory documents before publishing</li>
    </ul>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Global Catalogue Tip</p>
    <p class=""text-orange-700 dark:text-orange-300"">Start your global catalogue strategy with a clear matrix showing which products are available in which markets, which languages each market requires, and what market-specific data (pricing, compliance, categories) differs. This matrix becomes your implementation roadmap and ongoing operational guide.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 10: Workflows, Roles & Administration

    private LearningModule BuildWorkflowsRolesModule()
    {
        return new LearningModule
        {
            Id = "pim-workflows-roles",
            Title = "Workflows, Roles & Administration",
            Description = "Master PIM administration including approval workflows, user roles and permissions, dashboard reporting, and publishing to commerce platforms.",
            Icon = "shield-check",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pim-approval-workflows",
                    ModuleId = "pim-workflows-roles",
                    Title = "Approval Workflows & Data Enrichment",
                    Summary = "Master the approval workflow in PIM, from data enrichment through review to publication, ensuring data quality at every stage.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the full data enrichment workflow in PIM",
                        "Know how approval and rejection processes work",
                        "Design effective workflows for your team structure",
                        "Handle rejected products and revision cycles"
                    },
                    Content = @"
<h2>Approval Workflows & Data Enrichment</h2>
<p>PIM includes a structured <strong>data enrichment and approval workflow</strong> that ensures product data meets quality standards before being published to commerce storefronts. This workflow creates accountability and prevents incomplete or inaccurate data from reaching customers.</p>

<h3>The Enrichment Workflow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌────────────────────────────────────────────────────────────────────┐
│                    DATA ENRICHMENT WORKFLOW                         │
│                                                                     │
│  ┌──────────┐    ┌───────────┐    ┌──────────┐    ┌────────────┐  │
│  │  Import  │───▶│  Enrich   │───▶│  Review  │───▶│  Approve   │  │
│  │          │    │           │    │          │    │            │  │
│  │ Products │    │ Fill in   │    │ Check    │    │ Approve or │  │
│  │ enter    │    │ properties│    │ quality  │    │ reject     │  │
│  │ system   │    │ add assets│    │ & compl. │    │            │  │
│  └──────────┘    └───────────┘    └──────────┘    └─────┬──────┘  │
│                                        ▲                 │         │
│                                        │    ┌────────┐   │         │
│                                        └────│Rejected│◀──┘         │
│                                             └────────┘             │
│                                                                     │
│  Roles:                                                             │
│  Merchandiser ────────────────▶  Manager/Admin ─────────────────▶  │
│                                                                     │
└────────────────────────────────────────────────────────────────────┘
</pre>

<h3>Workflow Stages in Detail</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Stage</th>
            <th class=""px-4 py-2 text-left"">Who</th>
            <th class=""px-4 py-2 text-left"">Actions</th>
            <th class=""px-4 py-2 text-left"">Exit Criteria</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Import</td><td class=""px-4 py-2"">System / Importer</td><td class=""px-4 py-2"">Products are imported from external sources</td><td class=""px-4 py-2"">Products exist in PIM with basic data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Enrich</td><td class=""px-4 py-2"">Merchandiser</td><td class=""px-4 py-2"">Fill in properties, add assets, assign categories</td><td class=""px-4 py-2"">All required properties filled, assets attached</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Review</td><td class=""px-4 py-2"">Manager / Admin</td><td class=""px-4 py-2"">Check completeness, accuracy, and consistency</td><td class=""px-4 py-2"">Data meets quality standards</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Approve</td><td class=""px-4 py-2"">Manager / Admin</td><td class=""px-4 py-2"">Approve or reject the product</td><td class=""px-4 py-2"">Product approved for publishing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Publish</td><td class=""px-4 py-2"">System (Sync Job)</td><td class=""px-4 py-2"">PIM sync job pushes data to commerce</td><td class=""px-4 py-2"">Product live on storefront</td></tr>
    </tbody>
</table>

<h3>Handling Rejections</h3>
<p>When a reviewer rejects a product, it re-enters the enrichment stage:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li>Reviewer identifies issues (e.g., missing images, incorrect descriptions)</li>
        <li>Reviewer <strong>rejects</strong> the product, optionally adding notes about what needs fixing</li>
        <li>Product status returns to <strong>In Progress</strong></li>
        <li>The assigned merchandiser sees the rejection and notes</li>
        <li>Merchandiser makes corrections and resubmits for approval</li>
    </ol>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Workflow Efficiency Tip</p>
    <p class=""text-orange-700 dark:text-orange-300"">Minimise rejection cycles by establishing clear data quality guidelines upfront. Document what ""complete"" and ""approved"" look like for each product template, including asset requirements, description standards, and property formatting rules. Share these guidelines with all merchandisers during onboarding.</p>
</div>

<h3>Designing Your Workflow</h3>
<p>Consider these factors when designing your approval workflow:</p>
<ul>
    <li><strong>Team size</strong> — Small teams may have one approver; large teams may need multiple approvers by product category</li>
    <li><strong>Product complexity</strong> — Complex products (e.g., industrial equipment) may need more thorough review than simple products</li>
    <li><strong>Publication cadence</strong> — If products need to go live quickly, streamline the approval process</li>
    <li><strong>Regulatory requirements</strong> — Some industries require formal sign-off on product data before publication</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-user-roles-permissions",
                    ModuleId = "pim-workflows-roles",
                    Title = "User Roles & Permissions",
                    Summary = "Understand the PIM role system, configure team member access, and assign granular permissions based on templates and property values.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Know all PIM user roles and their capabilities",
                        "Configure team members with appropriate roles",
                        "Assign granular permissions based on product templates",
                        "Manage roles through Opti ID Admin Centre"
                    },
                    Content = @"
<h2>User Roles & Permissions</h2>
<p>PIM implements a <strong>role-based access control (RBAC)</strong> system that determines what each team member can see and do. Roles are managed through the <strong>Opti ID Admin Centre</strong>, not within PIM itself.</p>

<h3>Core PIM Roles</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Role</th>
            <th class=""px-4 py-2 text-left"">Access Level</th>
            <th class=""px-4 py-2 text-left"">Key Capabilities</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Merchandiser</td>
            <td class=""px-4 py-2"">Data Entry</td>
            <td class=""px-4 py-2"">Create and edit products, fill in properties, manage assigned product data</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Manager</td>
            <td class=""px-4 py-2"">Management</td>
            <td class=""px-4 py-2"">Everything Merchandiser can do, plus: approve/reject products, manage categories, broader product access</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">PIM_Admin</td>
            <td class=""px-4 py-2"">Full Admin</td>
            <td class=""px-4 py-2"">Full PIM access: modify settings, manage properties and templates, manage team members, approve all products</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">PIM_System</td>
            <td class=""px-4 py-2"">System Admin</td>
            <td class=""px-4 py-2"">Everything PIM_Admin can do, plus: change commerce version, view Hangfire dashboard (background job monitoring)</td>
        </tr>
    </tbody>
</table>

<h3>Additional Role Modifiers</h3>
<p>Base roles can be enhanced with additional capabilities:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Additional Role</th>
            <th class=""px-4 py-2 text-left"">Grants</th>
            <th class=""px-4 py-2 text-left"">Typical Use</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Product Importer</td><td class=""px-4 py-2"">Bulk import capabilities</td><td class=""px-4 py-2"">Team members who handle data feeds from external systems</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Asset Manager</td><td class=""px-4 py-2"">Asset import and management rights</td><td class=""px-4 py-2"">Team members responsible for product imagery and documents</td></tr>
    </tbody>
</table>

<h3>Granular Permission Assignments</h3>
<p>Beyond base roles, PIM supports <strong>fine-grained permissions</strong> based on product templates and property values:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Template-based permissions</strong> — Assign a merchandiser to specific product templates (e.g., ""Sarah can only manage products using the Running Shoes template"")</li>
        <li><strong>Property value-based permissions</strong> — Restrict access based on property values (e.g., ""Tom can only manage products where Brand = Nike"")</li>
        <li><strong>Combined permissions</strong> — Template and property value permissions can be combined for precise access control</li>
    </ul>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Role Assignment Process</p>
    <p class=""text-orange-700 dark:text-orange-300"">Roles are managed in the Opti ID Admin Centre, not within PIM. To assign a role: navigate to Opti ID Admin Centre → find the user → assign the appropriate PIM role → then within PIM, configure their template/property value assignments to scope their access.</p>
</div>

<h3>Role Planning Guidelines</h3>
<ul>
    <li><strong>Start with least privilege</strong> — Give users the minimum role they need to do their job</li>
    <li><strong>Use template assignments</strong> — Rather than giving everyone broad access, scope merchandisers to specific product types</li>
    <li><strong>Separate duties</strong> — The person entering data should not be the same person approving it</li>
    <li><strong>Document role assignments</strong> — Maintain a matrix of who has what role and why</li>
    <li><strong>Review periodically</strong> — Audit role assignments quarterly and remove access for departed team members</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-publishing-commerce",
                    ModuleId = "pim-workflows-roles",
                    Title = "Publishing to Commerce",
                    Summary = "Learn how approved PIM data is published to Configured Commerce storefronts through the sync job, and how to monitor and troubleshoot the process.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand how the PIM sync job works with Configured Commerce",
                        "Configure the sync job and integration settings",
                        "Monitor sync job execution and troubleshoot failures",
                        "Handle data conflicts between PIM and commerce"
                    },
                    Content = @"
<h2>Publishing to Commerce</h2>
<p>The final step in the PIM workflow is <strong>publishing approved product data to commerce storefronts</strong>. This happens through the <strong>PIM sync job</strong>, a scheduled process that runs within Configured Commerce and pulls data from PIM.</p>

<h3>How the Sync Job Works</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────────┐     PIM Sync Job      ┌────────────────────────┐
│              │    (Runs in Commerce)   │                        │
│  Optimizely  │───────────────────────▶│  Configured Commerce   │
│     PIM      │                        │                        │
│              │  Pulls:                │  Receives:             │
│  Approved    │  • Products            │  • Product listings    │
│  Products    │  • Categories          │  • Category navigation │
│  Categories  │  • Assets              │  • Product images      │
│  Assets      │  • Variants            │  • Variant selections  │
│  Variants    │  • Relationships       │  • Related products    │
│              │  • Translations        │  • Multi-language data  │
│              │                        │                        │
└──────────────┘                        └────────────────────────┘
</pre>

<h3>Sync Job Configuration</h3>
<p>The sync job is configured in the <strong>Configured Commerce Admin Console</strong>:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Typical Value</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Schedule</td><td class=""px-4 py-2"">How often the sync runs</td><td class=""px-4 py-2"">Every 15-60 minutes, or nightly</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Target Website</td><td class=""px-4 py-2"">Which commerce website receives the data</td><td class=""px-4 py-2"">Specific website ID</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sync Type</td><td class=""px-4 py-2"">Full sync or incremental (delta) sync</td><td class=""px-4 py-2"">Incremental for regular runs</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Related Product Types</td><td class=""px-4 py-2"">Which relationship types to sync</td><td class=""px-4 py-2"">Accessories, Cross-Sell, etc.</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Language Scope</td><td class=""px-4 py-2"">Which languages to include</td><td class=""px-4 py-2"">All enabled, or specific languages</td></tr>
    </tbody>
</table>

<h3>Monitoring the Sync</h3>
<p>After the sync job runs, monitor its status and results:</p>
<ul>
    <li><strong>Job status</strong> — Check if the sync completed successfully, with warnings, or failed</li>
    <li><strong>Record counts</strong> — How many products, categories, and assets were synced</li>
    <li><strong>Error log</strong> — Details on any products that failed to sync and why</li>
    <li><strong>Hangfire dashboard</strong> — PIM_System users can access the background job monitoring dashboard for detailed execution logs</li>
</ul>

<h3>Troubleshooting Common Issues</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Possible Cause</th>
            <th class=""px-4 py-2 text-left"">Resolution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Products not appearing on site</td><td class=""px-4 py-2"">Products not yet approved in PIM</td><td class=""px-4 py-2"">Approve products and wait for next sync</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Categories out of sync</td><td class=""px-4 py-2"">Category changes made after last sync</td><td class=""px-4 py-2"">Trigger a manual sync or wait for scheduled run</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Images not displaying</td><td class=""px-4 py-2"">Assets not associated with approved products</td><td class=""px-4 py-2"">Check asset-product associations in PIM</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sync job failing</td><td class=""px-4 py-2"">Connection or configuration issue</td><td class=""px-4 py-2"">Check commerce version setting, API credentials, network connectivity</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Stale data on site</td><td class=""px-4 py-2"">Sync running infrequently</td><td class=""px-4 py-2"">Increase sync frequency or trigger manual sync</td></tr>
    </tbody>
</table>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Production Monitoring</p>
    <p class=""text-orange-700 dark:text-orange-300"">Set up alerts for sync job failures in your production environment. A failed sync means your storefront may be displaying outdated product data. Most operations teams configure email or Slack notifications for sync job errors.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pim-dashboard-reporting",
                    ModuleId = "pim-workflows-roles",
                    Title = "Dashboard Reporting & Catalogue Health",
                    Summary = "Use PIM's reporting capabilities to monitor catalogue health, track team productivity, and identify data quality issues.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use the PIM dashboard for catalogue health monitoring",
                        "Track product pipeline progress and bottlenecks",
                        "Generate reports on data quality and completeness",
                        "Build a data governance reporting cadence"
                    },
                    Content = @"
<h2>Dashboard Reporting & Catalogue Health</h2>
<p>The PIM dashboard and reporting capabilities provide visibility into the health of your product catalogue, the progress of your data enrichment efforts, and areas that need attention.</p>

<h3>Key Dashboard Metrics</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">What It Tells You</th>
            <th class=""px-4 py-2 text-left"">Action If Concerning</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Products by Status</td><td class=""px-4 py-2"">Distribution across the pipeline</td><td class=""px-4 py-2"">Large ""Draft"" count = import backlog; large ""Awaiting Approval"" = reviewer bottleneck</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Average Completeness</td><td class=""px-4 py-2"">Overall data quality level</td><td class=""px-4 py-2"">Below 80% = enrichment effort needed</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Products Without Images</td><td class=""px-4 py-2"">Asset coverage gaps</td><td class=""px-4 py-2"">Prioritise asset uploads for these products</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Translation Coverage</td><td class=""px-4 py-2"">Multi-language completeness per language</td><td class=""px-4 py-2"">Low coverage = translation backlog for that market</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Recent Activity</td><td class=""px-4 py-2"">Pace of data enrichment</td><td class=""px-4 py-2"">Declining activity = team may need re-engagement</td></tr>
    </tbody>
</table>

<h3>Building a Reporting Cadence</h3>
<p>Establish a regular reporting rhythm to keep your catalogue healthy:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-3"">
        <li>
            <strong>Daily</strong> — Quick dashboard check: any sync failures? Products stuck in approval?
        </li>
        <li>
            <strong>Weekly</strong> — Review pipeline flow: are products moving through statuses? Any bottlenecks?
        </li>
        <li>
            <strong>Monthly</strong> — Deep analysis: completeness trends, translation coverage, asset gaps, data quality audit
        </li>
        <li>
            <strong>Quarterly</strong> — Strategic review: category structure effectiveness, template adequacy, role assignments, process improvements
        </li>
    </ul>
</div>

<h3>Using Exports for Reporting</h3>
<p>For analysis beyond what the dashboard provides, export product data and analyse it externally:</p>
<ul>
    <li>Export all products with completeness scores for a data quality heatmap</li>
    <li>Export by template to assess which product types need the most enrichment</li>
    <li>Export by category to identify thin categories with few products</li>
    <li>Export with translation data to assess multi-language readiness by market</li>
</ul>

<h3>Data Governance Best Practices</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Define ownership</strong> — Assign a data governance owner responsible for overall catalogue health</li>
        <li><strong>Set quality targets</strong> — Define and communicate target completeness percentages (e.g., 95% for required properties)</li>
        <li><strong>Automate monitoring</strong> — Use the Service API to build automated quality checks and alerts</li>
        <li><strong>Celebrate progress</strong> — Track and share improvements in data quality with the team</li>
        <li><strong>Continuous improvement</strong> — Use monthly reviews to identify process improvements and template refinements</li>
    </ul>
</div>

<div class=""bg-orange-50 dark:bg-orange-900/20 border-l-4 border-orange-500 p-4 my-4"">
    <p class=""font-medium text-orange-800 dark:text-orange-200"">Governance is Ongoing</p>
    <p class=""text-orange-700 dark:text-orange-300"">Data governance is not a one-time setup — it's an ongoing discipline. As your catalogue grows, new products are added, and team members change, your governance processes must evolve. Regular reporting and review ensures your PIM remains a trusted source of truth.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion
}
