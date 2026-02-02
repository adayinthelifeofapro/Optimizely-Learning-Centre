using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.ConfiguredCommerce;

/// <summary>
/// Content provider for the Optimizely Configured Commerce course
/// </summary>
public class ConfiguredCommerceContentProvider : ILearningContentProvider
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
            BuildSpireCMSModule(),
            BuildCatalogManagementModule(),
            BuildPricingInventoryModule(),
            BuildCartCheckoutModule(),
            BuildB2BAccountsModule(),
            BuildOrdersFulfillmentModule(),
            BuildIntegrationsAPIsModule(),
            BuildCustomizationModule(),
            BuildAdvancedTopicsModule()
        };
    }

    #region Module 1: Getting Started with Configured Commerce

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started with Configured Commerce",
            Description = "Learn the fundamentals of Optimizely Configured Commerce, understand its architecture, and set up your development environment.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-what-is-configured-commerce",
                    ModuleId = "getting-started",
                    Title = "What is Configured Commerce?",
                    Summary = "Discover Optimizely Configured Commerce and its capabilities for building B2B e-commerce solutions.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Configured Commerce is and its purpose",
                        "Learn the key benefits of using Configured Commerce for B2B",
                        "Understand the difference between Configured Commerce and Commerce Connect",
                        "Know when to use Configured Commerce for your projects"
                    },
                    Content = @"
<h2>Introduction to Optimizely Configured Commerce</h2>
<p>Optimizely Configured Commerce (formerly known as B2B Commerce Cloud or Insite Commerce) is a <strong>SaaS-based, headless B2B e-commerce platform</strong> designed specifically for manufacturers, distributors, and wholesalers who need robust business-to-business commerce capabilities.</p>

<h3>What is Configured Commerce?</h3>
<p>Configured Commerce is a cloud-native e-commerce solution that provides comprehensive B2B functionality out-of-the-box. Unlike traditional e-commerce platforms that require extensive custom development, Configured Commerce allows businesses to configure and customise their storefronts with minimal coding while still offering deep extensibility for complex requirements.</p>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">Key Differentiator</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">Configured Commerce is purpose-built for B2B commerce, with native support for complex pricing, account hierarchies, purchase orders, quotes, and ERP integrations that B2B businesses require.</p>
</div>

<h3>Key Capabilities</h3>
<ul>
    <li><strong>B2B-First Design</strong> - Account hierarchies, purchase orders, quotes, requisitions, and budget management built-in</li>
    <li><strong>Headless Architecture</strong> - API-first design enabling flexible frontend implementations</li>
    <li><strong>Spire CMS</strong> - Modern React/Redux-based frontend with visual page building</li>
    <li><strong>Complex Pricing</strong> - Customer-specific pricing, contract pricing, volume discounts, and real-time ERP pricing</li>
    <li><strong>Self-Service Portal</strong> - Comprehensive account management for B2B buyers</li>
    <li><strong>Multi-Site Support</strong> - Run multiple storefronts with shared or separate catalogs</li>
    <li><strong>PIM Integration</strong> - Native Product Information Management capabilities</li>
</ul>

<h3>Configured Commerce vs Commerce Connect</h3>
<p>Optimizely offers two commerce platforms. Here's how they compare:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Configured Commerce</th>
            <th class=""px-4 py-2 text-left"">Commerce Connect</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Primary Focus</td><td class=""px-4 py-2"">B2B commerce</td><td class=""px-4 py-2"">B2C/B2B commerce</td></tr>
        <tr><td class=""px-4 py-2"">Deployment</td><td class=""px-4 py-2"">SaaS (cloud-only)</td><td class=""px-4 py-2"">PaaS/Self-hosted</td></tr>
        <tr><td class=""px-4 py-2"">Frontend</td><td class=""px-4 py-2"">Spire CMS (React/Redux)</td><td class=""px-4 py-2"">Optimizely CMS (Razor/.NET)</td></tr>
        <tr><td class=""px-4 py-2"">Architecture</td><td class=""px-4 py-2"">Headless/API-first</td><td class=""px-4 py-2"">Integrated with CMS</td></tr>
        <tr><td class=""px-4 py-2"">B2B Features</td><td class=""px-4 py-2"">Native, comprehensive</td><td class=""px-4 py-2"">Available, requires setup</td></tr>
        <tr><td class=""px-4 py-2"">Customisation</td><td class=""px-4 py-2"">Configuration + TypeScript/C# handlers</td><td class=""px-4 py-2"">Full .NET Core development</td></tr>
    </tbody>
</table>

<h3>Target Use Cases</h3>
<p>Configured Commerce is ideal for:</p>
<ul>
    <li><strong>Manufacturers</strong> - Selling directly to distributors or end customers</li>
    <li><strong>Distributors</strong> - Managing complex product catalogs with customer-specific pricing</li>
    <li><strong>Wholesalers</strong> - Supporting high-volume ordering and account management</li>
    <li><strong>Multi-Branch Operations</strong> - Companies with multiple locations or warehouses</li>
    <li><strong>ERP-Centric Businesses</strong> - Organisations requiring tight ERP integration</li>
</ul>

<h3>Platform Components</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Commerce Engine</strong> - Core e-commerce functionality (cart, checkout, orders)</li>
        <li><strong>Spire CMS</strong> - React-based frontend and content management</li>
        <li><strong>Admin Console</strong> - Backend administration interface</li>
        <li><strong>REST APIs</strong> - Storefront and Admin APIs for integrations</li>
        <li><strong>Integration Framework</strong> - Handlers and pipelines for customisation</li>
    </ol>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-architecture-overview",
                    ModuleId = "getting-started",
                    Title = "Architecture Overview",
                    Summary = "Understand the 4-tier architecture and core components of Configured Commerce.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the 4-tier architecture of Configured Commerce",
                        "Learn about the headless commerce approach",
                        "Understand how Spire CMS connects to the commerce engine",
                        "Know the key architectural components and their roles"
                    },
                    Content = @"
<h2>Configured Commerce Architecture</h2>
<p>Optimizely Configured Commerce is built on a <strong>4-tier, headless architecture</strong> designed for scalability, flexibility, and extensibility. This architecture separates concerns and allows different components to be customised independently.</p>

<h3>The 4-Tier Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                     PRESENTATION TIER                            │
│  ┌─────────────────┐  ┌─────────────────┐  ┌────────────────┐  │
│  │   Spire CMS     │  │  Mobile Apps    │  │  Custom Heads  │  │
│  │  (React/Redux)  │  │  (Flutter SDK)  │  │  (Any Frontend)│  │
│  └─────────────────┘  └─────────────────┘  └────────────────┘  │
├─────────────────────────────────────────────────────────────────┤
│                        API TIER                                  │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │              REST APIs (Storefront + Admin)              │   │
│  │         JSON-based, RESTful, Handler-driven             │   │
│  └─────────────────────────────────────────────────────────┘   │
├─────────────────────────────────────────────────────────────────┤
│                     BUSINESS LOGIC TIER                          │
│  ┌───────────┐  ┌───────────┐  ┌───────────┐  ┌───────────┐   │
│  │ Handlers  │  │ Pipelines │  │  Plugins  │  │  Services │   │
│  └───────────┘  └───────────┘  └───────────┘  └───────────┘   │
├─────────────────────────────────────────────────────────────────┤
│                       DATA TIER                                  │
│  ┌───────────────────────────────────────────────────────────┐ │
│  │     SQL Server Database + Elasticsearch Search Index      │ │
│  └───────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Tier Descriptions</h3>

<h4>1. Presentation Tier</h4>
<p>The topmost layer handles user interfaces and experiences:</p>
<ul>
    <li><strong>Spire CMS</strong> - The default React/Redux-based storefront with visual CMS capabilities</li>
    <li><strong>Mobile Apps</strong> - Native mobile applications built with the Flutter/Dart SDK</li>
    <li><strong>Custom Frontends</strong> - Any JavaScript framework or native app consuming the APIs</li>
</ul>

<h4>2. API Tier</h4>
<p>RESTful APIs that expose commerce functionality:</p>
<ul>
    <li><strong>Storefront API</strong> - Customer-facing operations (cart, checkout, products, accounts)</li>
    <li><strong>Admin API</strong> - Backend management operations (product management, configuration)</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">API-First Design</p>
    <p class=""text-blue-700 dark:text-blue-300"">Every feature in Configured Commerce is accessible via REST APIs, making it truly headless. The Spire CMS storefront consumes the same APIs available to custom integrations.</p>
</div>

<h4>3. Business Logic Tier</h4>
<p>The core processing layer containing:</p>
<ul>
    <li><strong>Handlers</strong> - Process API requests and responses, enabling customisation of any endpoint</li>
    <li><strong>Pipelines</strong> - Reusable business logic chains (pricing, inventory, promotions)</li>
    <li><strong>Plugins</strong> - Modular components (tax calculators, payment gateways, shipping providers)</li>
    <li><strong>Services</strong> - Core business operations and domain logic</li>
</ul>

<h4>4. Data Tier</h4>
<p>Persistent storage and search infrastructure:</p>
<ul>
    <li><strong>SQL Server</strong> - Primary database for transactional data (orders, customers, products)</li>
    <li><strong>Elasticsearch</strong> - Search index for product discovery and faceted navigation</li>
</ul>

<h3>Headless Commerce Benefits</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Frontend Flexibility</td><td class=""px-4 py-2"">Use any frontend technology or multiple frontends</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Omnichannel Ready</td><td class=""px-4 py-2"">Power web, mobile, kiosks, and IoT from one backend</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Scalability</td><td class=""px-4 py-2"">Scale presentation and commerce tiers independently</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Integration</td><td class=""px-4 py-2"">Easy to connect with external systems via APIs</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Future-Proof</td><td class=""px-4 py-2"">Swap frontends without changing commerce logic</td></tr>
    </tbody>
</table>

<h3>Technology Stack</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Technology</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Backend Runtime</td><td class=""px-4 py-2"">.NET 8.0+</td></tr>
        <tr><td class=""px-4 py-2"">ORM</td><td class=""px-4 py-2"">Entity Framework Core</td></tr>
        <tr><td class=""px-4 py-2"">Spire Frontend</td><td class=""px-4 py-2"">React 18 / Redux</td></tr>
        <tr><td class=""px-4 py-2"">Mobile SDK</td><td class=""px-4 py-2"">Flutter / Dart</td></tr>
        <tr><td class=""px-4 py-2"">Search</td><td class=""px-4 py-2"">Elasticsearch</td></tr>
        <tr><td class=""px-4 py-2"">Database</td><td class=""px-4 py-2"">SQL Server</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-cloud-environment",
                    ModuleId = "getting-started",
                    Title = "Cloud Environment Setup",
                    Summary = "Understand the cloud infrastructure and environment provisioning process.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the cloud environment structure",
                        "Learn about the different environments (Dev, QA, Prod)",
                        "Know how deployments work in Configured Commerce",
                        "Understand the provisioning process"
                    },
                    Content = @"
<h2>Cloud Environment Setup</h2>
<p>Optimizely Configured Commerce runs exclusively as a <strong>SaaS (Software as a Service)</strong> solution in the Optimizely cloud. Understanding the environment structure is crucial for effective development and deployment.</p>

<h3>Environment Structure</h3>
<p>Each Configured Commerce implementation typically includes multiple environments:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <table class=""min-w-full"">
        <thead>
            <tr>
                <th class=""px-4 py-2 text-left"">Environment</th>
                <th class=""px-4 py-2 text-left"">Purpose</th>
                <th class=""px-4 py-2 text-left"">Typical Use</th>
            </tr>
        </thead>
        <tbody>
            <tr><td class=""px-4 py-2 font-medium"">Development (Dev)</td><td class=""px-4 py-2"">Active development work</td><td class=""px-4 py-2"">Feature development, debugging</td></tr>
            <tr><td class=""px-4 py-2 font-medium"">Quality Assurance (QA)</td><td class=""px-4 py-2"">Testing and validation</td><td class=""px-4 py-2"">Integration testing, UAT</td></tr>
            <tr><td class=""px-4 py-2 font-medium"">Staging (optional)</td><td class=""px-4 py-2"">Pre-production validation</td><td class=""px-4 py-2"">Final testing, training</td></tr>
            <tr><td class=""px-4 py-2 font-medium"">Production (Prod)</td><td class=""px-4 py-2"">Live customer-facing site</td><td class=""px-4 py-2"">Real transactions</td></tr>
        </tbody>
    </table>
</div>

<h3>Environment URLs</h3>
<p>Environments follow a predictable URL pattern:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Development:  https://[customer]-dev.inscm.io
QA:           https://[customer]-qa.inscm.io
Staging:      https://[customer]-stg.inscm.io
Production:   https://[customer].inscm.io (or custom domain)
</pre>

<h3>Provisioning Process</h3>
<p>New environments are provisioned by Optimizely following these steps:</p>
<ol class=""list-decimal list-inside space-y-2 my-4"">
    <li><strong>Request Submission</strong> - Partner or customer submits provisioning request</li>
    <li><strong>Infrastructure Setup</strong> - Cloud resources are allocated and configured</li>
    <li><strong>Base Installation</strong> - Configured Commerce platform is installed</li>
    <li><strong>Access Provisioning</strong> - Admin accounts and GitHub access are created</li>
    <li><strong>Initial Configuration</strong> - Basic settings and integrations are configured</li>
</ol>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important Note</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Environment provisioning is handled by Optimizely. Developers cannot self-provision environments. Contact your Optimizely representative or submit a support ticket for new environment requests.</p>
</div>

<h3>Deployment Workflow</h3>
<p>Configured Commerce uses <strong>GitHub-based deployments</strong>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│    Local     │────▶│    GitHub    │────▶│   Optimizely │
│ Development  │     │  Repository  │     │    Cloud     │
└──────────────┘     └──────────────┘     └──────────────┘
       │                     │                    │
   Git Commit           Merge to            Auto-Deploy
   and Push             Branch              (CI/CD Pipeline)
</pre>

<h4>Deployment Branches</h4>
<ul>
    <li><strong>develop</strong> - Deploys to Development environment</li>
    <li><strong>qa</strong> - Deploys to QA environment</li>
    <li><strong>staging</strong> - Deploys to Staging environment</li>
    <li><strong>main/master</strong> - Deploys to Production environment</li>
</ul>

<h3>Admin Console Access</h3>
<p>The Admin Console is the backend management interface accessible at:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
https://[environment-url]/admin
</pre>

<p>From the Admin Console you can:</p>
<ul>
    <li>Manage products, categories, and catalogs</li>
    <li>Configure pricing and promotions</li>
    <li>View and manage orders</li>
    <li>Manage customer accounts</li>
    <li>Configure system settings</li>
    <li>Run scheduled jobs</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-local-development",
                    ModuleId = "getting-started",
                    Title = "Local Development Environment",
                    Summary = "Set up your local development environment for Configured Commerce customisation.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Install the required development tools",
                        "Clone and configure the project repository",
                        "Understand the project structure",
                        "Run the Spire development server"
                    },
                    Content = @"
<h2>Local Development Environment</h2>
<p>While Configured Commerce runs in the cloud, you'll develop customisations locally and deploy them via GitHub. This lesson covers setting up your local development environment.</p>

<h3>Prerequisites</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium"">Required Software:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ <strong>Node.js 18+</strong> (for Spire frontend development)</li>
        <li>✓ <strong>npm or yarn</strong> (package manager)</li>
        <li>✓ <strong>.NET 8.0 SDK</strong> (for server-side extensions)</li>
        <li>✓ <strong>Git</strong> (version control)</li>
        <li>✓ <strong>VS Code</strong> (recommended IDE)</li>
        <li>✓ <strong>GitHub access</strong> (to your project repository)</li>
    </ul>
</div>

<h3>Step 1: Clone the Repository</h3>
<p>Your Configured Commerce project will have a GitHub repository provided by Optimizely:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
# Clone your project repository
git clone https://github.com/YourOrg/your-commerce-project.git

# Navigate to the project
cd your-commerce-project

# Install dependencies
npm install
</pre>

<h3>Step 2: Project Structure</h3>
<p>A typical Configured Commerce project has this structure:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
your-commerce-project/
├── FrontEnd/
│   ├── modules/
│   │   ├── blueprints/
│   │   │   └── your-blueprint/
│   │   │       ├── src/
│   │   │       │   ├── Overrides/      # Widget overrides
│   │   │       │   ├── Widgets/        # Custom widgets
│   │   │       │   └── Handlers/       # Custom handlers
│   │   │       └── package.json
│   │   └── client-framework/
│   └── config/
├── Extensions/
│   ├── Handlers/                       # Server-side handlers
│   ├── Pipelines/                      # Custom pipelines
│   └── Plugins/                        # Custom plugins
├── package.json
└── README.md
</pre>

<h3>Step 3: Configure Environment</h3>
<p>Create or update the environment configuration to point to your development environment:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// FrontEnd/config/settings.js
module.exports = {
    apiUrl: 'https://your-company-dev.inscm.io/api/v1',
    siteId: 'your-site-id',
    // Other configuration options
};
</pre>

<h3>Step 4: Run the Development Server</h3>
<p>Start the Spire development server for frontend development:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
# Navigate to your blueprint
cd FrontEnd/modules/blueprints/your-blueprint

# Start development server
npm start

# The site will be available at http://localhost:3000
</pre>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">Development Mode</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">The local development server runs the Spire frontend but connects to your cloud environment's APIs. This means you're working with real data from your development environment.</p>
</div>

<h3>VS Code Extensions</h3>
<p>Recommended extensions for Configured Commerce development:</p>
<ul>
    <li><strong>ES7+ React/Redux/React-Native snippets</strong> - React development helpers</li>
    <li><strong>TypeScript Importer</strong> - Auto-import TypeScript modules</li>
    <li><strong>Prettier</strong> - Code formatting</li>
    <li><strong>ESLint</strong> - JavaScript/TypeScript linting</li>
    <li><strong>GitLens</strong> - Enhanced Git integration</li>
</ul>

<h3>Certification Requirement</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Important</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Optimizely recommends obtaining the <strong>Certified Optimizely Configured Commerce Developer</strong> certification before working on production implementations. This ensures you understand the platform's architecture and best practices.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Spire CMS Fundamentals

    private LearningModule BuildSpireCMSModule()
    {
        return new LearningModule
        {
            Id = "spire-cms",
            Title = "Spire CMS Fundamentals",
            Description = "Master the Spire CMS frontend framework, including widgets, pages, blueprints, and the React/Redux architecture.",
            Icon = "cube",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-understanding-spire",
                    ModuleId = "spire-cms",
                    Title = "Understanding Spire CMS",
                    Summary = "Learn the fundamentals of Spire CMS and its React/Redux architecture.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Spire CMS is and its purpose",
                        "Learn the React/Redux architecture used by Spire",
                        "Understand how Spire differs from Classic CMS",
                        "Know the key concepts: widgets, pages, and blueprints"
                    },
                    Content = @"
<h2>Understanding Spire CMS</h2>
<p>Spire CMS is the <strong>modern frontend framework</strong> for Optimizely Configured Commerce. Built on React and Redux, it provides a component-based architecture for building responsive, performant B2B storefronts.</p>

<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">Classic CMS End of Life</p>
    <p class=""text-red-700 dark:text-red-300"">Classic CMS (the AngularJS-based predecessor) reached end-of-life on January 1, 2025. All new development should use Spire CMS.</p>
</div>

<h3>What is Spire CMS?</h3>
<p>Spire is both a <strong>Content Management System</strong> and a <strong>reference storefront implementation</strong>. It uses:</p>
<ul>
    <li><strong>React 18</strong> - Component-based UI library</li>
    <li><strong>Redux</strong> - State management for predictable data flow</li>
    <li><strong>TypeScript</strong> - Type-safe JavaScript for better developer experience</li>
    <li><strong>REST APIs</strong> - All data fetched from Configured Commerce APIs</li>
</ul>

<h3>Key Concepts</h3>

<h4>Widgets</h4>
<p>Widgets are the building blocks of Spire pages. Each widget is a React component that:</p>
<ul>
    <li>Renders a specific piece of UI (header, product list, cart summary)</li>
    <li>Can be configured by content editors in the CMS</li>
    <li>May connect to Redux state for data</li>
    <li>Can be customised or replaced by developers</li>
</ul>

<h4>Pages</h4>
<p>Pages are containers that hold widgets. Each page type corresponds to a specific URL pattern:</p>
<ul>
    <li><strong>ProductDetail</strong> - Individual product pages</li>
    <li><strong>ProductList</strong> - Category/search result pages</li>
    <li><strong>Cart</strong> - Shopping cart page</li>
    <li><strong>Checkout</strong> - Checkout flow pages</li>
    <li><strong>MyAccount</strong> - Account management pages</li>
</ul>

<h4>Blueprints</h4>
<p>Blueprints are complete site templates that include:</p>
<ul>
    <li>Page templates and layouts</li>
    <li>Widget configurations</li>
    <li>Styling and themes</li>
    <li>Custom handlers and overrides</li>
</ul>

<h3>Architecture Diagram</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                      SPIRE CMS ARCHITECTURE                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │                    React Components                        │  │
│  │  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐     │  │
│  │  │ Widget  │  │ Widget  │  │ Widget  │  │ Widget  │     │  │
│  │  └────┬────┘  └────┬────┘  └────┬────┘  └────┬────┘     │  │
│  └───────┼────────────┼────────────┼────────────┼──────────┘  │
│          │            │            │            │              │
│  ┌───────▼────────────▼────────────▼────────────▼──────────┐  │
│  │                    Redux Store                            │  │
│  │  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐     │  │
│  │  │ State   │  │ Actions │  │Reducers │  │Selectors│     │  │
│  │  └─────────┘  └─────────┘  └─────────┘  └─────────┘     │  │
│  └──────────────────────────┬───────────────────────────────┘  │
│                             │                                   │
│  ┌──────────────────────────▼───────────────────────────────┐  │
│  │                    Handler Chains                          │  │
│  │  LoadProduct → ValidateProduct → ApplyPricing → Render    │  │
│  └──────────────────────────┬───────────────────────────────┘  │
│                             │                                   │
│  ┌──────────────────────────▼───────────────────────────────┐  │
│  │                   REST API Calls                           │  │
│  │  GET /products  |  POST /cart  |  GET /account            │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Spire vs Classic CMS</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Spire CMS</th>
            <th class=""px-4 py-2 text-left"">Classic CMS (EOL)</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Framework</td><td class=""px-4 py-2"">React/Redux</td><td class=""px-4 py-2"">AngularJS</td></tr>
        <tr><td class=""px-4 py-2"">Language</td><td class=""px-4 py-2"">TypeScript</td><td class=""px-4 py-2"">JavaScript</td></tr>
        <tr><td class=""px-4 py-2"">Extension</td><td class=""px-4 py-2"">Overrides &amp; Injection</td><td class=""px-4 py-2"">Directives</td></tr>
        <tr><td class=""px-4 py-2"">Handlers</td><td class=""px-4 py-2"">TypeScript</td><td class=""px-4 py-2"">C#</td></tr>
        <tr><td class=""px-4 py-2"">Build</td><td class=""px-4 py-2"">Webpack</td><td class=""px-4 py-2"">Gulp</td></tr>
        <tr><td class=""px-4 py-2"">State</td><td class=""px-4 py-2"">Redux</td><td class=""px-4 py-2"">Angular scopes</td></tr>
    </tbody>
</table>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">Modern Development</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">Spire brings modern frontend development practices to B2B commerce, with component-based architecture, type safety, and a rich ecosystem of React tooling.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-pages-and-widgets",
                    ModuleId = "spire-cms",
                    Title = "Pages and Widgets",
                    Summary = "Deep dive into Spire's page and widget system.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand how pages are structured in Spire",
                        "Learn about the different widget types",
                        "Know how widgets are configured in the CMS",
                        "Understand the widget lifecycle"
                    },
                    Content = @"
<h2>Pages and Widgets</h2>
<p>In Spire CMS, <strong>pages are composed of widgets</strong>. Understanding this composition model is fundamental to building and customising Configured Commerce storefronts.</p>

<h3>Content Items</h3>
<p>Optimizely collectively refers to widgets and pages as <strong>ContentItems</strong>. Each ContentItem is defined in a TypeScript file that includes:</p>
<ul>
    <li>Component rendering logic</li>
    <li>CMS field definitions (for editor configuration)</li>
    <li>Default values and validation rules</li>
</ul>

<h3>Page Types</h3>
<p>Spire includes these standard page types:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Page Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">URL Pattern</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">HomePage</td><td class=""px-4 py-2"">Site landing page</td><td class=""px-4 py-2"">/</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ProductListPage</td><td class=""px-4 py-2"">Category/search results</td><td class=""px-4 py-2"">/products, /category/*</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ProductDetailPage</td><td class=""px-4 py-2"">Single product view</td><td class=""px-4 py-2"">/product/*</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">CartPage</td><td class=""px-4 py-2"">Shopping cart</td><td class=""px-4 py-2"">/cart</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">CheckoutShippingPage</td><td class=""px-4 py-2"">Shipping selection</td><td class=""px-4 py-2"">/checkout/shipping</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">CheckoutPaymentPage</td><td class=""px-4 py-2"">Payment information</td><td class=""px-4 py-2"">/checkout/payment</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">MyAccountPage</td><td class=""px-4 py-2"">Account dashboard</td><td class=""px-4 py-2"">/my-account</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">OrderHistoryPage</td><td class=""px-4 py-2"">Past orders</td><td class=""px-4 py-2"">/my-account/orders</td></tr>
    </tbody>
</table>

<h3>Widget Categories</h3>

<h4>Basic Widgets</h4>
<p>Simple widgets without Redux connection:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example: Simple banner widget
import * as React from ""react"";

const Banner: React.FC<BannerProps> = ({ title, subtitle, imageUrl }) => {
    return (
        <div className=""banner"">
            <img src={imageUrl} alt={title} />
            <h1>{title}</h1>
            <p>{subtitle}</p>
        </div>
    );
};

export default Banner;
</pre>

<h4>Redux-Connected Widgets</h4>
<p>Widgets that connect to the Redux store for data:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example: Cart summary widget connected to Redux
import * as React from ""react"";
import { connect } from ""react-redux"";
import { ApplicationState } from ""@insite/client-framework/Store/ApplicationState"";

const mapStateToProps = (state: ApplicationState) => ({
    cart: state.pages.cart.data,
    isLoading: state.pages.cart.isLoading,
});

const CartSummary: React.FC<CartSummaryProps> = ({ cart, isLoading }) => {
    if (isLoading) return <Spinner />;

    return (
        <div className=""cart-summary"">
            <p>Items: {cart?.lineItems?.length || 0}</p>
            <p>Total: {cart?.orderTotal}</p>
        </div>
    );
};

export default connect(mapStateToProps)(CartSummary);
</pre>

<h4>CMS Field Widgets</h4>
<p>Widgets that expose configurable fields to content editors:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Widget definition with CMS fields
const definition: WidgetDefinition = {
    group: ""Common"",
    allowedContexts: [""HomePage"", ""ContentPage""],
    fieldDefinitions: [
        {
            name: ""title"",
            displayName: ""Title"",
            editorTemplate: ""TextField"",
            defaultValue: ""Welcome"",
        },
        {
            name: ""backgroundColor"",
            displayName: ""Background Color"",
            editorTemplate: ""ColorPickerField"",
            defaultValue: ""#ffffff"",
        },
    ],
};
</pre>

<h3>Widget Zones</h3>
<p>Pages contain <strong>widget zones</strong> where widgets can be placed. Common zones include:</p>
<ul>
    <li><strong>Header</strong> - Site navigation, logo, search</li>
    <li><strong>Main Content</strong> - Primary page content</li>
    <li><strong>Sidebar</strong> - Filters, related products</li>
    <li><strong>Footer</strong> - Links, contact info</li>
</ul>

<h3>Widget Lifecycle</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Mount</strong> - Widget component is mounted to the DOM</li>
        <li><strong>Handler Chain</strong> - Data loading handlers execute</li>
        <li><strong>Redux Update</strong> - Store is updated with data</li>
        <li><strong>Re-render</strong> - Widget re-renders with new data</li>
        <li><strong>User Interaction</strong> - Actions dispatched on user events</li>
        <li><strong>Unmount</strong> - Cleanup when navigating away</li>
    </ol>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-blueprints-and-themes",
                    ModuleId = "spire-cms",
                    Title = "Blueprints and Themes",
                    Summary = "Learn how to work with blueprints and customise site themes.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand what blueprints are and how they work",
                        "Learn how to create and customise blueprints",
                        "Know how to apply themes and styling",
                        "Understand the override system"
                    },
                    Content = @"
<h2>Blueprints and Themes</h2>
<p>Blueprints are the foundation of Spire site customisation. They provide a way to create <strong>complete, reusable site configurations</strong> that can be deployed to different environments.</p>

<h3>What is a Blueprint?</h3>
<p>A blueprint is a self-contained package that includes:</p>
<ul>
    <li><strong>Widget Overrides</strong> - Customised versions of standard widgets</li>
    <li><strong>Custom Widgets</strong> - Brand new widgets</li>
    <li><strong>Handlers</strong> - Custom frontend handlers</li>
    <li><strong>Styling</strong> - CSS/SCSS themes</li>
    <li><strong>Configuration</strong> - Site-specific settings</li>
</ul>

<h3>Blueprint Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
FrontEnd/modules/blueprints/your-blueprint/
├── src/
│   ├── Overrides/
│   │   ├── Widgets/
│   │   │   └── Header.tsx          # Override the Header widget
│   │   └── Pages/
│   │       └── ProductDetail.tsx   # Override product detail page
│   ├── Widgets/
│   │   └── CustomBanner.tsx        # New custom widget
│   ├── Handlers/
│   │   └── CustomProductLoader.ts  # Custom handler
│   └── Styles/
│       └── theme.scss              # Custom styling
├── package.json
└── webpack.config.js
</pre>

<h3>Override System</h3>
<p>Spire uses a file-based override system. To override a widget from Content-Library:</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Override Pattern</p>
    <p class=""text-blue-700 dark:text-blue-300"">Place your override file at: <code class=""bg-blue-100 dark:bg-blue-800 px-1 rounded"">[blueprint]/src/Overrides/[originalPath]</code></p>
</div>

<p>Example: To override the ProductDetail widget:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
# Original location:
Content-Library/src/Widgets/Product/ProductDetail.tsx

# Override location:
your-blueprint/src/Overrides/Widgets/Product/ProductDetail.tsx
</pre>

<h3>Creating a Custom Widget</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// your-blueprint/src/Widgets/PromoBanner.tsx
import * as React from ""react"";
import WidgetModule from ""@insite/client-framework/Types/WidgetModule"";
import WidgetProps from ""@insite/client-framework/Types/WidgetProps"";

interface PromoBannerProps extends WidgetProps {
    fields: {
        promoText: string;
        linkUrl: string;
        backgroundColor: string;
    };
}

const PromoBanner: React.FC<PromoBannerProps> = ({ fields }) => {
    const { promoText, linkUrl, backgroundColor } = fields;

    return (
        <div style={{ backgroundColor }} className=""promo-banner"">
            <a href={linkUrl}>{promoText}</a>
        </div>
    );
};

const widgetModule: WidgetModule = {
    component: PromoBanner,
    definition: {
        group: ""Marketing"",
        displayName: ""Promo Banner"",
        allowedContexts: [""HomePage"", ""ProductListPage""],
        fieldDefinitions: [
            {
                name: ""promoText"",
                displayName: ""Promotion Text"",
                editorTemplate: ""TextField"",
                defaultValue: ""Special Offer!"",
            },
            {
                name: ""linkUrl"",
                displayName: ""Link URL"",
                editorTemplate: ""TextField"",
                defaultValue: ""/"",
            },
            {
                name: ""backgroundColor"",
                displayName: ""Background Color"",
                editorTemplate: ""ColorPickerField"",
                defaultValue: ""#ff6600"",
            },
        ],
    },
};

export default widgetModule;
</pre>

<h3>Theming</h3>
<p>Spire supports styling through SCSS. Create theme files in your blueprint:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// your-blueprint/src/Styles/theme.scss

// Brand colors
$primary-color: #10B981;
$secondary-color: #1f2937;
$accent-color: #f59e0b;

// Typography
$font-family-base: 'Inter', sans-serif;
$font-size-base: 16px;

// Apply styles
.site-header {
    background-color: $primary-color;
    font-family: $font-family-base;
}

.btn-primary {
    background-color: $primary-color;
    &:hover {
        background-color: darken($primary-color, 10%);
    }
}
</pre>

<h3>Best Practices</h3>
<ul>
    <li><strong>Prefer overrides</strong> - Override existing widgets rather than replacing entire pages</li>
    <li><strong>Keep overrides minimal</strong> - Only change what's necessary to maintain upgradeability</li>
    <li><strong>Use TypeScript</strong> - Leverage type safety for fewer runtime errors</li>
    <li><strong>Follow naming conventions</strong> - Match Optimizely's patterns for consistency</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-cms-admin-interface",
                    ModuleId = "spire-cms",
                    Title = "CMS Admin Interface",
                    Summary = "Navigate the Spire CMS admin interface for content management.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the CMS admin interface",
                        "Edit pages and configure widgets",
                        "Publish content changes",
                        "Manage page templates"
                    },
                    Content = @"
<h2>CMS Admin Interface</h2>
<p>The Spire CMS admin interface allows content editors to manage pages, configure widgets, and publish changes without developer involvement.</p>

<h3>Accessing the CMS</h3>
<p>The CMS editor is accessed by adding <code>/ContentAdmin</code> to your site URL:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
https://your-site.inscm.io/ContentAdmin
</pre>

<h3>CMS Dashboard</h3>
<p>The dashboard provides quick access to:</p>
<ul>
    <li><strong>Pages</strong> - Browse and edit site pages</li>
    <li><strong>Page Templates</strong> - Manage page configurations</li>
    <li><strong>Site Settings</strong> - Global site configuration</li>
    <li><strong>Publishing Queue</strong> - Review pending changes</li>
</ul>

<h3>Page Editor</h3>
<p>The page editor is where content changes are made:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-medium mb-2"">Editor Features</h4>
    <ul class=""space-y-1"">
        <li>✓ Visual page preview</li>
        <li>✓ Widget drag-and-drop</li>
        <li>✓ Inline editing</li>
        <li>✓ Device preview (desktop, tablet, mobile)</li>
        <li>✓ Version history</li>
        <li>✓ Compare changes</li>
    </ul>
</div>

<h3>Editing Widgets</h3>
<p>To edit a widget on a page:</p>
<ol class=""list-decimal list-inside space-y-2 my-4"">
    <li>Navigate to the page in the CMS</li>
    <li>Click on a widget to select it</li>
    <li>The properties panel opens on the right</li>
    <li>Edit the widget's field values</li>
    <li>Preview changes in real-time</li>
    <li>Save the page</li>
</ol>

<h3>Widget Properties Panel</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────┐
│      WIDGET PROPERTIES              │
├─────────────────────────────────────┤
│ Widget: Product Carousel            │
│                                     │
│ Title:                              │
│ ┌─────────────────────────────────┐ │
│ │ Featured Products               │ │
│ └─────────────────────────────────┘ │
│                                     │
│ Number of Products:                 │
│ ┌───────┐                           │
│ │   8   │                           │
│ └───────┘                           │
│                                     │
│ Auto-Rotate:                        │
│ [✓] Enable                          │
│                                     │
│ Rotation Speed:                     │
│ ┌───────┐                           │
│ │  5s   │                           │
│ └───────┘                           │
│                                     │
│ [  Save  ]  [  Cancel  ]            │
└─────────────────────────────────────┘
</pre>

<h3>Publishing Workflow</h3>
<p>Changes in Spire CMS follow a draft/publish workflow:</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Edit</strong> - Make changes to page content</li>
        <li><strong>Preview</strong> - Review changes in preview mode</li>
        <li><strong>Save Draft</strong> - Save changes without publishing</li>
        <li><strong>Publish</strong> - Make changes live on the site</li>
    </ol>
</div>

<h3>Page Templates</h3>
<p>Page templates define the default widget configuration for page types. When a new page is created, it inherits widgets from its template.</p>

<h4>Template Management</h4>
<ul>
    <li><strong>Create Template</strong> - Design a new page layout</li>
    <li><strong>Edit Template</strong> - Modify widget placement and defaults</li>
    <li><strong>Apply Template</strong> - Reset a page to its template configuration</li>
</ul>

<h3>Content Restrictions</h3>
<p>Widget definitions can specify where they're allowed:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Widget only allowed on specific page types
allowedContexts: [""HomePage"", ""ProductListPage""],

// Widget restricted to specific zones
allowedZones: [""Header"", ""Footer""],
</pre>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 3: Product Catalog Management

    private LearningModule BuildCatalogManagementModule()
    {
        return new LearningModule
        {
            Id = "catalog-management",
            Title = "Product Catalog Management",
            Description = "Learn to manage product catalogs, categories, product data, search, and customer-specific catalogs in Configured Commerce.",
            Icon = "rectangle-stack",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-catalog-structure",
                    ModuleId = "catalog-management",
                    Title = "Catalog Structure",
                    Summary = "Understand how catalogs, categories, and products are organised.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the catalog hierarchy",
                        "Learn about categories and subcategories",
                        "Know how products relate to categories",
                        "Understand multi-catalog scenarios"
                    },
                    Content = @"
<h2>Catalog Structure</h2>
<p>The product catalog is the foundation of your Configured Commerce storefront. Understanding its structure is essential for effective product management.</p>

<h3>Catalog Hierarchy</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Website
└── Catalog(s)
    └── Categories
        └── Subcategories
            └── Products
                └── Variants (if applicable)
</pre>

<h3>Catalogs</h3>
<p>A catalog is the top-level container for products. Most sites use a single catalog, but multi-catalog scenarios include:</p>
<ul>
    <li><strong>Customer-Specific Catalogs</strong> - Different product sets for different customer segments</li>
    <li><strong>Regional Catalogs</strong> - Products available in specific regions</li>
    <li><strong>Seasonal Catalogs</strong> - Time-limited product collections</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Catalog Assignment</p>
    <p class=""text-blue-700 dark:text-blue-300"">Catalogs can be assigned to websites or customer segments. A customer sees products from catalogs assigned to their account or the default website catalog.</p>
</div>

<h3>Categories</h3>
<p>Categories organise products into logical groups. They support:</p>
<ul>
    <li><strong>Unlimited Nesting</strong> - Categories can contain subcategories</li>
    <li><strong>Multiple Assignment</strong> - Products can belong to multiple categories</li>
    <li><strong>SEO Fields</strong> - URL slugs, meta descriptions, titles</li>
    <li><strong>Display Options</strong> - Featured products, sort order, filters</li>
</ul>

<h3>Category Properties</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Name</td><td class=""px-4 py-2"">Display name for the category</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Short Description</td><td class=""px-4 py-2"">Brief category description</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">URL Segment</td><td class=""px-4 py-2"">URL-friendly slug</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Sort Order</td><td class=""px-4 py-2"">Display sequence in navigation</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Is Active</td><td class=""px-4 py-2"">Whether category is visible</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Image</td><td class=""px-4 py-2"">Category thumbnail image</td></tr>
    </tbody>
</table>

<h3>Products</h3>
<p>Products are the items customers purchase. Key product concepts:</p>

<h4>Product Types</h4>
<ul>
    <li><strong>Simple Products</strong> - Single SKU items without variations</li>
    <li><strong>Configurable Products</strong> - Products with variants (size, colour, etc.)</li>
    <li><strong>Kit/Bundle Products</strong> - Products composed of other products</li>
</ul>

<h4>Product Relationships</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Product
├── Categories (many-to-many)
├── Variants (one-to-many)
├── Related Products (many-to-many)
├── Cross-Sells (many-to-many)
├── Images (one-to-many)
├── Documents (one-to-many)
└── Specifications (one-to-many)
</pre>

<h3>Admin Console Navigation</h3>
<p>Access catalog management in the Admin Console:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Log into Admin Console</li>
    <li>Navigate to <strong>Catalog &gt; Categories</strong></li>
    <li>Browse or search the category tree</li>
    <li>Click a category to view/edit products</li>
</ol>

<h3>Best Practices</h3>
<ul>
    <li><strong>Plan your hierarchy</strong> - Design category structure before importing products</li>
    <li><strong>Keep it shallow</strong> - Limit nesting to 3-4 levels for usability</li>
    <li><strong>Use clear names</strong> - Categories should be self-explanatory</li>
    <li><strong>Consider search</strong> - Users may search instead of browse</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-product-data-management",
                    ModuleId = "catalog-management",
                    Title = "Product Data Management",
                    Summary = "Learn to manage product attributes, specifications, images, and documents.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Manage product attributes and specifications",
                        "Handle product images and documents",
                        "Work with product variants",
                        "Import and export product data"
                    },
                    Content = @"
<h2>Product Data Management</h2>
<p>Effective product data management ensures customers can find and evaluate products easily. Configured Commerce provides comprehensive tools for managing rich product information.</p>

<h3>Product Attributes</h3>
<p>Every product has core attributes:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Required</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Product Number</td><td class=""px-4 py-2"">Unique identifier (SKU)</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Name</td><td class=""px-4 py-2"">Display name</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Short Description</td><td class=""px-4 py-2"">Brief product summary</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Long Description</td><td class=""px-4 py-2"">Full HTML description</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">URL Segment</td><td class=""px-4 py-2"">URL-friendly slug</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">UPC</td><td class=""px-4 py-2"">Universal Product Code</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Manufacturer</td><td class=""px-4 py-2"">Product manufacturer</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Brand</td><td class=""px-4 py-2"">Product brand</td><td class=""px-4 py-2"">No</td></tr>
    </tbody>
</table>

<h3>Custom Properties</h3>
<p>Extend product data with custom properties:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Custom property types available:
- Text (string values)
- Number (integers, decimals)
- Date/DateTime
- Boolean (yes/no)
- List (predefined options)
- Rich Text (HTML content)
</pre>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">PIM Integration</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">Custom properties can be synced with Optimizely PIM or external systems via the integration framework. Properties can be marked as ""externally managed"" to prevent overwriting during sync.</p>
</div>

<h3>Specifications</h3>
<p>Specifications are structured product details displayed in tables:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Specification Group: Technical Details
├── Weight: 2.5 kg
├── Dimensions: 30cm x 20cm x 10cm
├── Material: Stainless Steel
└── Warranty: 2 Years

Specification Group: Electrical
├── Voltage: 220-240V
├── Power: 1500W
└── Frequency: 50/60Hz
</pre>

<h3>Product Images</h3>
<p>Configure multiple images per product:</p>
<ul>
    <li><strong>Primary Image</strong> - Main product image for listings</li>
    <li><strong>Alternate Images</strong> - Additional views, angles, details</li>
    <li><strong>Variant Images</strong> - Images specific to product variants</li>
</ul>

<h4>Image Best Practices</h4>
<ul>
    <li>Use consistent dimensions (e.g., 1000x1000 pixels)</li>
    <li>Provide multiple angles for physical products</li>
    <li>Include lifestyle/contextual images</li>
    <li>Optimise file sizes for web performance</li>
</ul>

<h3>Product Documents</h3>
<p>Attach documents to products:</p>
<ul>
    <li><strong>Spec Sheets</strong> - Technical specifications PDFs</li>
    <li><strong>User Manuals</strong> - Product documentation</li>
    <li><strong>Safety Data Sheets</strong> - MSDS for chemicals</li>
    <li><strong>CAD Files</strong> - Technical drawings</li>
</ul>

<h3>Product Variants</h3>
<p>Variants represent different versions of a product:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Parent Product: Industrial Gloves
├── Variant 1: Size Small, Color Black (SKU: GLV-S-BLK)
├── Variant 2: Size Medium, Color Black (SKU: GLV-M-BLK)
├── Variant 3: Size Large, Color Black (SKU: GLV-L-BLK)
├── Variant 4: Size Small, Color Yellow (SKU: GLV-S-YLW)
└── ...
</pre>

<h4>Variant Attributes</h4>
<p>Common variant differentiators:</p>
<ul>
    <li>Size (S, M, L, XL or numerical)</li>
    <li>Colour</li>
    <li>Material</li>
    <li>Package Quantity</li>
</ul>

<h3>Data Import/Export</h3>
<p>Bulk operations are supported via:</p>
<ul>
    <li><strong>Spreadsheet Import</strong> - Excel/CSV files for product data</li>
    <li><strong>API Integration</strong> - REST APIs for programmatic updates</li>
    <li><strong>PIM Sync</strong> - Automated sync from Optimizely PIM</li>
    <li><strong>ERP Integration</strong> - Real-time or batch sync from ERP</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-product-search-filtering",
                    ModuleId = "catalog-management",
                    Title = "Product Search and Filtering",
                    Summary = "Configure search indexing, faceted navigation, and search optimisation.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand how product search works",
                        "Configure search indexing",
                        "Set up faceted navigation filters",
                        "Optimise search results"
                    },
                    Content = @"
<h2>Product Search and Filtering</h2>
<p>Effective search and filtering helps B2B buyers find products quickly. Configured Commerce uses <strong>Elasticsearch</strong> to power product discovery.</p>

<h3>Search Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    SEARCH FLOW                                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  User Query                                                      │
│      │                                                           │
│      ▼                                                           │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │              ELASTICSEARCH                                 │  │
│  │  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐     │  │
│  │  │ Product │  │  Full   │  │ Faceted │  │ Suggest │     │  │
│  │  │  Index  │  │  Text   │  │ Search  │  │  ions   │     │  │
│  │  └─────────┘  └─────────┘  └─────────┘  └─────────┘     │  │
│  └──────────────────────────────────────────────────────────┘  │
│      │                                                           │
│      ▼                                                           │
│  Search Results (Products, Facets, Suggestions)                 │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Search Index</h3>
<p>The product search index includes:</p>
<ul>
    <li><strong>Product Names</strong> - Weighted for relevance</li>
    <li><strong>Descriptions</strong> - Full-text searchable</li>
    <li><strong>SKU/Product Numbers</strong> - Exact match capability</li>
    <li><strong>Brand/Manufacturer</strong> - Filterable attributes</li>
    <li><strong>Specifications</strong> - Technical data searchable</li>
    <li><strong>Custom Properties</strong> - Configured as searchable</li>
</ul>

<h3>Indexing Configuration</h3>
<p>Configure which fields are indexed and how:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Searchable</td><td class=""px-4 py-2"">Field is included in full-text search</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Filterable</td><td class=""px-4 py-2"">Field can be used as a facet filter</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Sortable</td><td class=""px-4 py-2"">Results can be sorted by this field</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Boost</td><td class=""px-4 py-2"">Relative importance for relevance</td></tr>
    </tbody>
</table>

<h3>Faceted Navigation</h3>
<p>Facets allow users to filter search results by attribute values:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
FILTERS
─────────────────
Category
☐ Power Tools (45)
☐ Hand Tools (128)
☐ Safety Equipment (67)

Brand
☐ DeWalt (34)
☐ Milwaukee (28)
☐ Bosch (19)

Price Range
☐ Under £50 (89)
☐ £50 - £100 (156)
☐ Over £100 (67)

In Stock
☑ Show only in-stock
</pre>
</div>

<h3>Configuring Facets</h3>
<p>Facets are configured in the Admin Console under Search Settings:</p>
<ul>
    <li><strong>Facet Name</strong> - Display label for the filter</li>
    <li><strong>Source Field</strong> - Which product attribute to facet on</li>
    <li><strong>Facet Type</strong> - Checkbox, range slider, text input</li>
    <li><strong>Sort Order</strong> - Alphabetical, count, custom</li>
    <li><strong>Show Count</strong> - Display result counts per value</li>
</ul>

<h3>Search Suggestions</h3>
<p>Autocomplete suggestions help users find products:</p>
<ul>
    <li><strong>Product Suggestions</strong> - Matching product names</li>
    <li><strong>Category Suggestions</strong> - Relevant categories</li>
    <li><strong>Search History</strong> - User's recent searches</li>
    <li><strong>Popular Searches</strong> - Common search terms</li>
</ul>

<h3>Search Optimisation Tips</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li>✓ Include common synonyms in product data</li>
        <li>✓ Add industry-specific terminology</li>
        <li>✓ Include part numbers users might search for</li>
        <li>✓ Use consistent naming conventions</li>
        <li>✓ Configure appropriate field boosting</li>
        <li>✓ Regular reindex after bulk updates</li>
    </ul>
</div>

<h3>Rebuilding the Search Index</h3>
<p>The search index can be rebuilt from Admin Console:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Navigate to <strong>Admin &gt; Jobs</strong></li>
    <li>Find the <strong>Product Index Rebuild</strong> job</li>
    <li>Click <strong>Run Now</strong></li>
    <li>Monitor job progress</li>
</ol>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-customer-specific-catalogs",
                    ModuleId = "catalog-management",
                    Title = "Customer-Specific Catalogs",
                    Summary = "Implement B2B catalog personalisation with customer-specific product access.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand customer-specific catalog scenarios",
                        "Configure catalog restrictions",
                        "Implement product visibility rules",
                        "Test customer catalog access"
                    },
                    Content = @"
<h2>Customer-Specific Catalogs</h2>
<p>B2B commerce often requires showing different products to different customers. Configured Commerce supports sophisticated <strong>catalog personalisation</strong> to meet this need.</p>

<h3>Why Customer-Specific Catalogs?</h3>
<p>Common B2B scenarios requiring restricted catalogs:</p>
<ul>
    <li><strong>Contractual Products</strong> - Customer can only order items under their contract</li>
    <li><strong>Authorised Distributors</strong> - Products available only to certain partner tiers</li>
    <li><strong>Regional Restrictions</strong> - Products available in specific markets</li>
    <li><strong>Restricted Items</strong> - Controlled substances, age-restricted products</li>
    <li><strong>Custom Products</strong> - Items manufactured for specific customers</li>
</ul>

<h3>Catalog Assignment Levels</h3>
<p>Catalogs can be assigned at multiple levels:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Assignment Hierarchy (most specific wins)
┌─────────────────────────────────────────┐
│ 1. User (Individual)                    │ ← Most specific
├─────────────────────────────────────────┤
│ 2. Bill-To Customer                     │
├─────────────────────────────────────────┤
│ 3. Customer Segment                     │
├─────────────────────────────────────────┤
│ 4. Website Default                      │ ← Least specific
└─────────────────────────────────────────┘
</pre>

<h3>Configuration Options</h3>

<h4>Restricted Catalog Mode</h4>
<p>When enabled, customers see only products from their assigned catalogs:</p>
<ul>
    <li>Products not in assigned catalogs are hidden</li>
    <li>Search results are filtered</li>
    <li>Direct URLs return 404</li>
</ul>

<h4>Additive Catalog Mode</h4>
<p>Customer-specific catalogs add to the base catalog:</p>
<ul>
    <li>Customer sees default catalog products</li>
    <li>Plus products from assigned catalogs</li>
    <li>Used for ""premium"" product access</li>
</ul>

<h3>Setting Up Customer Catalogs</h3>
<ol class=""list-decimal list-inside space-y-2 my-4"">
    <li>Create the catalog in Admin Console</li>
    <li>Add products to the catalog</li>
    <li>Assign catalog to customer/segment</li>
    <li>Configure catalog mode (restricted/additive)</li>
    <li>Test with customer account</li>
</ol>

<h3>Catalog Assignment Example</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Customer: Acme Manufacturing
Assigned Catalogs:
  ├── Base Industrial Catalog (default)
  ├── Contract Pricing Catalog (customer-specific)
  └── Premium Tools Catalog (segment: Gold Partners)

Resulting Product Access:
  ✓ All products from Base Industrial Catalog
  ✓ Contract-specific products for Acme
  ✓ Premium products (Gold Partner benefit)
</pre>
</div>

<h3>API Behaviour</h3>
<p>When authenticated users access product APIs:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
GET /api/v1/products
Authorization: Bearer [user-token]

// Response includes only products from:
// - Catalogs assigned to the user
// - Catalogs assigned to user's bill-to customer
// - Catalogs assigned to user's customer segments
// - Default website catalog (if applicable)
</pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Security Note</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Catalog restrictions are enforced at the API level, ensuring customers cannot access restricted products even through direct API calls.</p>
</div>

<h3>Testing Catalog Access</h3>
<p>To verify customer catalog configuration:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Create a test user assigned to the customer</li>
    <li>Log in as that user on the storefront</li>
    <li>Verify visible products match expectations</li>
    <li>Test search to ensure restricted products don't appear</li>
    <li>Try direct URL access to restricted products</li>
</ol>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 4: Pricing and Inventory

    private LearningModule BuildPricingInventoryModule()
    {
        return new LearningModule
        {
            Id = "pricing-inventory",
            Title = "Pricing and Inventory",
            Description = "Master pricing strategies, customer-specific pricing, real-time ERP integration, and inventory management.",
            Icon = "currency-pound",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-pricing-strategies",
                    ModuleId = "pricing-inventory",
                    Title = "Pricing Strategies",
                    Summary = "Learn about base pricing, price lists, and pricing hierarchies.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the pricing hierarchy in Configured Commerce",
                        "Configure base prices and price lists",
                        "Implement volume-based pricing",
                        "Set up promotional pricing"
                    },
                    Content = @"
<h2>Pricing Strategies</h2>
<p>Configured Commerce provides a sophisticated pricing engine that supports the complex pricing requirements of B2B commerce, from simple list prices to dynamic, customer-specific pricing.</p>

<h3>Pricing Hierarchy</h3>
<p>Prices are resolved using a hierarchy where more specific prices override general ones:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Price Resolution Order (first match wins)
┌─────────────────────────────────────────┐
│ 1. Customer + Product Price             │ ← Most specific
├─────────────────────────────────────────┤
│ 2. Customer Price List                  │
├─────────────────────────────────────────┤
│ 3. Customer Segment Price List          │
├─────────────────────────────────────────┤
│ 4. Contract Pricing                     │
├─────────────────────────────────────────┤
│ 5. Sale/Promotional Price               │
├─────────────────────────────────────────┤
│ 6. Volume/Quantity Break Price          │
├─────────────────────────────────────────┤
│ 7. Base Product Price                   │ ← Least specific
└─────────────────────────────────────────┘
</pre>

<h3>Base Pricing</h3>
<p>Every product requires a base price. This is the default price shown to anonymous users or customers without special pricing.</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Price Field</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">List Price</td><td class=""px-4 py-2"">MSRP or recommended retail price</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Base Price</td><td class=""px-4 py-2"">Standard selling price</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Unit of Measure</td><td class=""px-4 py-2"">Each, Pack, Case, etc.</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Currency</td><td class=""px-4 py-2"">Price currency code</td></tr>
    </tbody>
</table>

<h3>Price Lists</h3>
<p>Price lists group products with special pricing that can be assigned to customers or segments:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Price List: Gold Partner Pricing
├── Product A: £45.00 (Base: £50.00) - 10% discount
├── Product B: £180.00 (Base: £200.00) - 10% discount
└── Product C: £90.00 (Base: £100.00) - 10% discount

Price List: Contractor Pricing
├── Product A: £42.50 (Base: £50.00) - 15% discount
├── Product D: £75.00 (Base: £85.00) - 12% discount
└── Product E: £320.00 (Base: £350.00) - 9% discount
</pre>
</div>

<h3>Volume/Quantity Break Pricing</h3>
<p>Offer discounts based on quantity ordered:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Quantity</th>
            <th class=""px-4 py-2 text-left"">Unit Price</th>
            <th class=""px-4 py-2 text-left"">Savings</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1-9</td><td class=""px-4 py-2"">£10.00</td><td class=""px-4 py-2"">-</td></tr>
        <tr><td class=""px-4 py-2"">10-49</td><td class=""px-4 py-2"">£9.00</td><td class=""px-4 py-2"">10%</td></tr>
        <tr><td class=""px-4 py-2"">50-99</td><td class=""px-4 py-2"">£8.00</td><td class=""px-4 py-2"">20%</td></tr>
        <tr><td class=""px-4 py-2"">100+</td><td class=""px-4 py-2"">£7.00</td><td class=""px-4 py-2"">30%</td></tr>
    </tbody>
</table>

<h3>Promotional Pricing</h3>
<p>Time-limited sale prices with date ranges:</p>
<ul>
    <li><strong>Start Date</strong> - When the promotion begins</li>
    <li><strong>End Date</strong> - When the promotion expires</li>
    <li><strong>Sale Price</strong> - The discounted price</li>
    <li><strong>Show Original</strong> - Display crossed-out original price</li>
</ul>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">Price Display</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">Configured Commerce can show ""Was/Now"" pricing to highlight discounts and create urgency for promotional items.</p>
</div>

<h3>Multi-Currency Pricing</h3>
<p>Support for international B2B customers:</p>
<ul>
    <li>Define prices in multiple currencies</li>
    <li>Automatic currency conversion with exchange rates</li>
    <li>Currency-specific price lists</li>
    <li>Display prices in customer's preferred currency</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-realtime-pricing",
                    ModuleId = "pricing-inventory",
                    Title = "Real-Time Pricing Integration",
                    Summary = "Implement real-time pricing from ERP systems.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand real-time pricing architecture",
                        "Configure ERP pricing integration",
                        "Handle pricing API responses",
                        "Implement fallback pricing strategies"
                    },
                    Content = @"
<h2>Real-Time Pricing Integration</h2>
<p>Many B2B businesses maintain pricing in their ERP system as the single source of truth. Configured Commerce supports <strong>real-time pricing integration</strong> to fetch current prices from external systems.</p>

<h3>Why Real-Time Pricing?</h3>
<ul>
    <li><strong>Single Source of Truth</strong> - ERP maintains all pricing logic</li>
    <li><strong>Complex Calculations</strong> - Leverage ERP's pricing engine</li>
    <li><strong>Contract Pricing</strong> - Customer-specific negotiated prices</li>
    <li><strong>Dynamic Pricing</strong> - Market-based price adjustments</li>
    <li><strong>Real-Time Currency</strong> - Current exchange rates</li>
</ul>

<h3>Real-Time Pricing Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│   Storefront │────▶│  Configured  │────▶│     ERP      │
│   (Spire)    │     │   Commerce   │     │   System     │
└──────────────┘     └──────────────┘     └──────────────┘
       │                    │                    │
    Request            Pricing API          Price Lookup
    Product            Handler Call         (SAP, Oracle,
    Price                                    Dynamics, etc.)
       │                    │                    │
       ▼                    ▼                    ▼
  Display Price ◀─── Return Price ◀─── Calculate Price
</pre>

<h3>Configuration Settings</h3>
<p>Real-time pricing is configured in the Admin Console:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Enable Real-Time Pricing</td><td class=""px-4 py-2"">Master toggle for the feature</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ERP Endpoint URL</td><td class=""px-4 py-2"">API endpoint for pricing calls</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Authentication</td><td class=""px-4 py-2"">API key, OAuth, or basic auth</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Timeout (ms)</td><td class=""px-4 py-2"">Maximum wait time for response</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Cache Duration</td><td class=""px-4 py-2"">How long to cache prices</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Fallback Behavior</td><td class=""px-4 py-2"">What to do if ERP unavailable</td></tr>
    </tbody>
</table>

<h3>Pricing Request</h3>
<p>When real-time pricing is enabled, Configured Commerce sends requests to your ERP:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example pricing request
POST /api/pricing
{
    ""customerId"": ""ACME-001"",
    ""currency"": ""GBP"",
    ""products"": [
        { ""productNumber"": ""SKU-12345"", ""quantity"": 10 },
        { ""productNumber"": ""SKU-67890"", ""quantity"": 5 }
    ],
    ""warehouse"": ""UK-MAIN""
}

// Expected response
{
    ""prices"": [
        {
            ""productNumber"": ""SKU-12345"",
            ""unitPrice"": 45.00,
            ""extendedPrice"": 450.00,
            ""currency"": ""GBP""
        },
        {
            ""productNumber"": ""SKU-67890"",
            ""unitPrice"": 120.00,
            ""extendedPrice"": 600.00,
            ""currency"": ""GBP""
        }
    ]
}
</pre>

<h3>Fallback Strategies</h3>
<p>When real-time pricing fails, fallback options include:</p>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li><strong>Use Cached Price</strong> - Show last known good price</li>
        <li><strong>Use Base Price</strong> - Fall back to stored base price</li>
        <li><strong>Hide Price</strong> - Show ""Call for Price"" message</li>
        <li><strong>Block Purchase</strong> - Prevent adding to cart</li>
    </ul>
</div>

<h3>Performance Considerations</h3>
<ul>
    <li><strong>Batch Requests</strong> - Group multiple products into single API calls</li>
    <li><strong>Intelligent Caching</strong> - Cache prices with appropriate TTL</li>
    <li><strong>Async Loading</strong> - Load prices after page render</li>
    <li><strong>Circuit Breaker</strong> - Prevent cascade failures</li>
</ul>

<h3>Custom Integration Handler</h3>
<p>Implement custom pricing logic by extending the pricing handler:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Server-side handler for real-time pricing
public class CustomPricingHandler : HandlerBase
{
    public override int Order => 500;

    public override async Task&lt;GetPricingResult&gt; Execute(
        GetPricingParameter parameter)
    {
        // Call your ERP's pricing API
        var erpPrices = await _erpService.GetPricesAsync(
            parameter.CustomerId,
            parameter.Products
        );

        // Map to Configured Commerce format
        return MapToResult(erpPrices);
    }
}
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-inventory-management",
                    ModuleId = "pricing-inventory",
                    Title = "Inventory Management",
                    Summary = "Configure stock levels, warehouses, and availability rules.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure inventory tracking",
                        "Set up multiple warehouses",
                        "Implement availability rules",
                        "Handle backorder and preorder scenarios"
                    },
                    Content = @"
<h2>Inventory Management</h2>
<p>Configured Commerce provides comprehensive inventory management to track stock levels across multiple warehouses and control product availability on the storefront.</p>

<h3>Inventory Concepts</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Term</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">On Hand</td><td class=""px-4 py-2"">Physical quantity in warehouse</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Available</td><td class=""px-4 py-2"">Quantity available for sale (On Hand - Reserved)</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Reserved</td><td class=""px-4 py-2"">Quantity allocated to orders</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">On Order</td><td class=""px-4 py-2"">Quantity on purchase orders</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Safety Stock</td><td class=""px-4 py-2"">Minimum inventory threshold</td></tr>
    </tbody>
</table>

<h3>Warehouse Configuration</h3>
<p>Configure multiple warehouses to track inventory by location:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Warehouse Configuration
├── UK-MAIN (Primary)
│   ├── Location: London
│   ├── Ship To: UK, Ireland
│   └── Priority: 1
├── UK-NORTH (Secondary)
│   ├── Location: Manchester
│   ├── Ship To: UK
│   └── Priority: 2
└── EU-CENTRAL
    ├── Location: Amsterdam
    ├── Ship To: EU
    └── Priority: 1
</pre>
</div>

<h3>Availability Display</h3>
<p>Configure how inventory is shown to customers:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Display Option</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Exact Quantity</td><td class=""px-4 py-2"">""24 in stock""</td></tr>
        <tr><td class=""px-4 py-2"">Stock Ranges</td><td class=""px-4 py-2"">""10-25 available""</td></tr>
        <tr><td class=""px-4 py-2"">Status Only</td><td class=""px-4 py-2"">""In Stock"" / ""Out of Stock""</td></tr>
        <tr><td class=""px-4 py-2"">Lead Time</td><td class=""px-4 py-2"">""Ships in 2-3 days""</td></tr>
        <tr><td class=""px-4 py-2"">Hidden</td><td class=""px-4 py-2"">No availability shown</td></tr>
    </tbody>
</table>

<h3>Stock Policies</h3>

<h4>Allow Backorder</h4>
<p>When enabled, customers can order products even when out of stock:</p>
<ul>
    <li>Backorder messaging on product page</li>
    <li>Expected availability date</li>
    <li>Partial shipment options</li>
</ul>

<h4>Allow Preorder</h4>
<p>For products not yet released:</p>
<ul>
    <li>Preorder button instead of ""Add to Cart""</li>
    <li>Release date display</li>
    <li>Preorder limit per customer</li>
</ul>

<h4>Purchase Controls</h4>
<ul>
    <li><strong>Minimum Order Quantity</strong> - Require minimum qty per product</li>
    <li><strong>Maximum Order Quantity</strong> - Limit qty per order</li>
    <li><strong>Quantity Increments</strong> - Force multiples (e.g., cases of 12)</li>
</ul>

<h3>Inventory Update Methods</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Integration Options</p>
    <ul class=""mt-2 space-y-1 text-blue-700 dark:text-blue-300"">
        <li>✓ <strong>Manual Entry</strong> - Update via Admin Console</li>
        <li>✓ <strong>Spreadsheet Import</strong> - Bulk update via CSV/Excel</li>
        <li>✓ <strong>API Integration</strong> - Push updates via REST API</li>
        <li>✓ <strong>Real-Time Sync</strong> - Live ERP integration</li>
    </ul>
</div>

<h3>Low Stock Alerts</h3>
<p>Configure notifications when inventory drops below thresholds:</p>
<ul>
    <li>Email alerts to inventory managers</li>
    <li>Dashboard warnings in Admin Console</li>
    <li>Integration triggers for replenishment systems</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-realtime-inventory",
                    ModuleId = "pricing-inventory",
                    Title = "Real-Time Inventory Integration",
                    Summary = "Implement real-time inventory from ERP systems.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure real-time inventory integration",
                        "Handle inventory API responses",
                        "Implement multi-warehouse availability",
                        "Set up fallback behaviour"
                    },
                    Content = @"
<h2>Real-Time Inventory Integration</h2>
<p>Like pricing, inventory can be fetched in real-time from ERP systems to ensure customers see accurate stock levels.</p>

<h3>Real-Time Inventory Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│   Product    │────▶│  Inventory   │────▶│     ERP      │
│    Page      │     │   Handler    │     │   System     │
└──────────────┘     └──────────────┘     └──────────────┘
       │                    │                    │
   Display Qty         API Call            Stock Lookup
       │                    │                    │
       ▼                    ▼                    ▼
  ""24 in stock"" ◀── Response ◀── Current Stock Levels
</pre>

<h3>Configuration</h3>
<p>Enable real-time inventory in Admin Console:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Enable Real-Time Inventory</td><td class=""px-4 py-2"">Master toggle</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Inventory Endpoint</td><td class=""px-4 py-2"">ERP API URL</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Check at Add to Cart</td><td class=""px-4 py-2"">Verify before adding</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Check at Checkout</td><td class=""px-4 py-2"">Final verification</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Cache Duration</td><td class=""px-4 py-2"">How long to cache levels</td></tr>
    </tbody>
</table>

<h3>Inventory API Request</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example inventory request
POST /api/inventory
{
    ""products"": [""SKU-12345"", ""SKU-67890""],
    ""warehouses"": [""UK-MAIN"", ""UK-NORTH""],
    ""customerId"": ""ACME-001""
}

// Expected response
{
    ""inventory"": [
        {
            ""productNumber"": ""SKU-12345"",
            ""warehouses"": [
                { ""id"": ""UK-MAIN"", ""available"": 24 },
                { ""id"": ""UK-NORTH"", ""available"": 12 }
            ],
            ""totalAvailable"": 36
        },
        {
            ""productNumber"": ""SKU-67890"",
            ""warehouses"": [
                { ""id"": ""UK-MAIN"", ""available"": 0 },
                { ""id"": ""UK-NORTH"", ""available"": 5 }
            ],
            ""totalAvailable"": 5
        }
    ]
}
</pre>

<h3>Multi-Warehouse Logic</h3>
<p>When multiple warehouses have stock, determine availability based on:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-medium mb-2"">Warehouse Selection Rules</h4>
    <ol class=""list-decimal list-inside space-y-1"">
        <li>Customer's preferred warehouse</li>
        <li>Nearest warehouse by shipping address</li>
        <li>Warehouse with highest availability</li>
        <li>Warehouse with lowest shipping cost</li>
    </ol>
</div>

<h3>Cart Validation</h3>
<p>Validate inventory at key points:</p>

<ul>
    <li><strong>Add to Cart</strong> - Check if requested qty available</li>
    <li><strong>Cart Update</strong> - Revalidate when qty changes</li>
    <li><strong>Checkout Start</strong> - Verify cart before payment</li>
    <li><strong>Order Submit</strong> - Final verification (may reduce qty)</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Checkout Inventory Handling</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Configure what happens when inventory drops during checkout: show warning, reduce quantity automatically, or require customer action.</p>
</div>

<h3>Fallback Options</h3>
<p>When real-time inventory fails:</p>
<ul>
    <li><strong>Use Cached Data</strong> - Show last known levels</li>
    <li><strong>Use Stored Inventory</strong> - Fall back to Commerce database</li>
    <li><strong>Assume Available</strong> - Allow purchase, handle backorder</li>
    <li><strong>Show Unavailable</strong> - Conservative approach, prevent purchase</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 5: Shopping Cart and Checkout

    private LearningModule BuildCartCheckoutModule()
    {
        return new LearningModule
        {
            Id = "cart-checkout",
            Title = "Shopping Cart and Checkout",
            Description = "Learn about cart management, checkout workflows, shipping, and payment processing.",
            Icon = "shopping-cart",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-cart-management",
                    ModuleId = "cart-checkout",
                    Title = "Cart Management",
                    Summary = "Understand cart operations, saved carts, and cart-level features.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the cart data model",
                        "Implement cart operations",
                        "Work with saved carts and wishlists",
                        "Handle cart-level promotions"
                    },
                    Content = @"
<h2>Cart Management</h2>
<p>The shopping cart is central to the commerce experience. Configured Commerce provides a robust cart system with B2B-specific features like saved carts, requisition lists, and multi-ship support.</p>

<h3>Cart Data Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Cart
├── Cart Lines (Line Items)
│   ├── Product
│   ├── Quantity
│   ├── Unit Price
│   ├── Extended Price
│   └── Promotions Applied
├── Shipping Information
│   ├── Shipping Address(es)
│   └── Shipping Method(s)
├── Billing Information
│   └── Payment Method(s)
├── Promotions
│   ├── Cart-Level Discounts
│   └── Promotion Codes
└── Totals
    ├── Subtotal
    ├── Shipping
    ├── Tax
    ├── Discounts
    └── Grand Total
</pre>

<h3>Cart Operations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Operation</th>
            <th class=""px-4 py-2 text-left"">API Endpoint</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Get Cart</td><td class=""px-4 py-2 font-mono"">GET /cart</td><td class=""px-4 py-2"">Retrieve current cart</td></tr>
        <tr><td class=""px-4 py-2"">Add Item</td><td class=""px-4 py-2 font-mono"">POST /cart/cartlines</td><td class=""px-4 py-2"">Add product to cart</td></tr>
        <tr><td class=""px-4 py-2"">Update Qty</td><td class=""px-4 py-2 font-mono"">PATCH /cart/cartlines/{id}</td><td class=""px-4 py-2"">Change line quantity</td></tr>
        <tr><td class=""px-4 py-2"">Remove Item</td><td class=""px-4 py-2 font-mono"">DELETE /cart/cartlines/{id}</td><td class=""px-4 py-2"">Remove from cart</td></tr>
        <tr><td class=""px-4 py-2"">Clear Cart</td><td class=""px-4 py-2 font-mono"">DELETE /cart</td><td class=""px-4 py-2"">Empty the cart</td></tr>
        <tr><td class=""px-4 py-2"">Apply Promo</td><td class=""px-4 py-2 font-mono"">POST /cart/promotions</td><td class=""px-4 py-2"">Add promotion code</td></tr>
    </tbody>
</table>

<h3>Saved Carts</h3>
<p>B2B buyers often need to save carts for later. Saved cart features include:</p>
<ul>
    <li><strong>Save Current Cart</strong> - Preserve cart state with a name</li>
    <li><strong>Load Saved Cart</strong> - Restore a previously saved cart</li>
    <li><strong>Share Cart</strong> - Share saved cart with colleagues</li>
    <li><strong>Merge Carts</strong> - Combine multiple saved carts</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">B2B Use Case</p>
    <p class=""text-blue-700 dark:text-blue-300"">A warehouse manager saves a cart of weekly supplies, then loads it each Monday to quickly reorder common items.</p>
</div>

<h3>Requisition Lists</h3>
<p>Requisition lists are similar to wishlists but designed for B2B:</p>
<ul>
    <li>Create multiple lists per user</li>
    <li>Quickly add list items to cart</li>
    <li>Share lists within organisation</li>
    <li>Import lists from CSV/Excel</li>
</ul>

<h3>Cart Features Configuration</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Quote from Cart</td><td class=""px-4 py-2"">Request a formal quote instead of purchasing</td></tr>
        <tr><td class=""px-4 py-2"">Order Notes</td><td class=""px-4 py-2"">Allow notes on cart and line items</td></tr>
        <tr><td class=""px-4 py-2"">Requested Delivery</td><td class=""px-4 py-2"">Customer can specify delivery date</td></tr>
        <tr><td class=""px-4 py-2"">PO Number</td><td class=""px-4 py-2"">Capture purchase order reference</td></tr>
        <tr><td class=""px-4 py-2"">Cost Centre</td><td class=""px-4 py-2"">Assign order to cost centre</td></tr>
    </tbody>
</table>

<h3>Mini Cart</h3>
<p>The mini cart widget shows a quick summary:</p>
<ul>
    <li>Item count badge</li>
    <li>Subtotal display</li>
    <li>Quick view of recent additions</li>
    <li>Links to full cart and checkout</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-checkout-workflows",
                    ModuleId = "cart-checkout",
                    Title = "Checkout Workflows",
                    Summary = "Configure single-page and multi-page checkout experiences.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand checkout workflow options",
                        "Configure checkout steps",
                        "Implement guest checkout",
                        "Handle checkout validation"
                    },
                    Content = @"
<h2>Checkout Workflows</h2>
<p>Configured Commerce supports multiple checkout workflows to match your business requirements and user preferences.</p>

<h3>Checkout Types</h3>

<h4>Single-Page Checkout</h4>
<p>All checkout steps on one page:</p>
<ul>
    <li>Faster completion for experienced buyers</li>
    <li>All information visible at once</li>
    <li>Ideal for B2B repeat customers</li>
</ul>

<h4>Multi-Page Checkout</h4>
<p>Step-by-step guided process:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Shipping Address</li>
    <li>Shipping Method</li>
    <li>Payment Information</li>
    <li>Order Review</li>
    <li>Confirmation</li>
</ol>

<h3>Checkout Configuration</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Checkout Settings
├── Checkout Type: Single Page / Multi-Page
├── Guest Checkout: Enabled / Disabled
├── Address Validation: Required / Optional
├── Tax Calculation: Real-time / Estimated
├── Shipping Selection: Before Payment / After
└── Order Review: Required / Optional
</pre>

<h3>Guest Checkout</h3>
<p>Allow purchases without account creation:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Enable Guest Checkout</td><td class=""px-4 py-2"">Allow non-registered purchases</td></tr>
        <tr><td class=""px-4 py-2"">Offer Registration</td><td class=""px-4 py-2"">Prompt to create account post-purchase</td></tr>
        <tr><td class=""px-4 py-2"">Guest Pricing</td><td class=""px-4 py-2"">Which price level for guests</td></tr>
        <tr><td class=""px-4 py-2"">Guest Payment</td><td class=""px-4 py-2"">Allowed payment methods</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">B2B Consideration</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Many B2B sites disable guest checkout to ensure orders are associated with approved customer accounts and receive correct pricing.</p>
</div>

<h3>Address Management</h3>
<p>Checkout address handling:</p>
<ul>
    <li><strong>Saved Addresses</strong> - Select from address book</li>
    <li><strong>New Address</strong> - Enter new shipping/billing</li>
    <li><strong>Address Validation</strong> - Verify against postal service</li>
    <li><strong>Ship-To Restrictions</strong> - Enforce approved addresses only</li>
</ul>

<h3>Multi-Ship Orders</h3>
<p>Split orders to multiple shipping addresses:</p>
<ul>
    <li>Assign items to different addresses</li>
    <li>Different shipping methods per shipment</li>
    <li>Separate tracking per shipment</li>
    <li>Useful for distribution to branches</li>
</ul>

<h3>Checkout Validation</h3>
<p>Validation occurs at multiple stages:</p>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-2"">
        <li><strong>Cart Validation</strong> - Products available, pricing current</li>
        <li><strong>Address Validation</strong> - Complete, valid address</li>
        <li><strong>Shipping Validation</strong> - Method available for address</li>
        <li><strong>Payment Validation</strong> - Payment method accepted, limits OK</li>
        <li><strong>Final Validation</strong> - Order totals, inventory check</li>
    </ol>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-shipping-configuration",
                    ModuleId = "cart-checkout",
                    Title = "Shipping Configuration",
                    Summary = "Set up shipping carriers, rates, and delivery options.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure shipping carriers",
                        "Set up shipping rate calculation",
                        "Implement shipping rules and restrictions",
                        "Handle special delivery requirements"
                    },
                    Content = @"
<h2>Shipping Configuration</h2>
<p>Configured Commerce provides flexible shipping configuration to handle various carrier integrations, rate calculations, and B2B-specific shipping requirements.</p>

<h3>Shipping Components</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Shipping Configuration
├── Carriers (FedEx, UPS, DHL, etc.)
├── Services (Ground, Express, Overnight)
├── Rate Calculation Methods
├── Shipping Rules & Restrictions
└── Delivery Options
</pre>

<h3>Carrier Integration</h3>
<p>Integrate with major shipping carriers:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Carrier</th>
            <th class=""px-4 py-2 text-left"">Features</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">FedEx</td><td class=""px-4 py-2"">Real-time rates, tracking, labels</td></tr>
        <tr><td class=""px-4 py-2"">UPS</td><td class=""px-4 py-2"">Real-time rates, tracking, labels</td></tr>
        <tr><td class=""px-4 py-2"">DHL</td><td class=""px-4 py-2"">International, express options</td></tr>
        <tr><td class=""px-4 py-2"">USPS/Royal Mail</td><td class=""px-4 py-2"">Postal services</td></tr>
        <tr><td class=""px-4 py-2"">Custom</td><td class=""px-4 py-2"">Own fleet, third-party logistics</td></tr>
    </tbody>
</table>

<h3>Rate Calculation Methods</h3>

<h4>Real-Time Rates</h4>
<p>Fetch live rates from carrier APIs:</p>
<ul>
    <li>Accurate pricing based on weight/dimensions</li>
    <li>Delivery time estimates</li>
    <li>Service availability by address</li>
</ul>

<h4>Table Rates</h4>
<p>Configure rate tables based on:</p>
<ul>
    <li>Order total (e.g., free shipping over £100)</li>
    <li>Weight/volume</li>
    <li>Destination zone</li>
    <li>Product type</li>
</ul>

<h4>Flat Rate</h4>
<p>Simple fixed shipping fees:</p>
<ul>
    <li>Per order flat rate</li>
    <li>Per item flat rate</li>
    <li>Per weight unit flat rate</li>
</ul>

<h3>Shipping Rules</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Example Rules
├── Free Shipping
│   └── Orders over £500 → Free Ground Shipping
├── Exclusions
│   └── Hazmat items → Ground Only, No Air
├── Surcharges
│   └── Oversized items → +£25 handling fee
└── Restrictions
    └── International → Express Only
</pre>
</div>

<h3>B2B Shipping Features</h3>
<ul>
    <li><strong>Carrier Accounts</strong> - Ship using customer's carrier account</li>
    <li><strong>Collect Shipping</strong> - Customer arranges own pickup</li>
    <li><strong>Will Call</strong> - Customer picks up from warehouse</li>
    <li><strong>Freight Shipping</strong> - LTL/FTL for large orders</li>
    <li><strong>Scheduled Delivery</strong> - Specific delivery date/time</li>
</ul>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">Third-Party Shipping</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">B2B customers often have negotiated carrier rates. Configured Commerce can use their carrier account numbers for billing shipping directly to them.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-payment-processing",
                    ModuleId = "cart-checkout",
                    Title = "Payment Processing",
                    Summary = "Configure payment gateways, methods, and B2B payment terms.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Integrate payment gateways",
                        "Configure B2B payment methods",
                        "Implement purchase order payments",
                        "Handle payment security (PCI compliance)"
                    },
                    Content = @"
<h2>Payment Processing</h2>
<p>Configured Commerce supports multiple payment methods essential for B2B commerce, from credit cards to purchase orders and credit terms.</p>

<h3>Payment Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
            <th class=""px-4 py-2 text-left"">B2B Relevance</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Credit Card</td><td class=""px-4 py-2"">Immediate payment</td><td class=""px-4 py-2"">Common</td></tr>
        <tr><td class=""px-4 py-2"">Purchase Order</td><td class=""px-4 py-2"">Pay on terms</td><td class=""px-4 py-2"">Very Common</td></tr>
        <tr><td class=""px-4 py-2"">Account Credit</td><td class=""px-4 py-2"">Invoice to account</td><td class=""px-4 py-2"">Very Common</td></tr>
        <tr><td class=""px-4 py-2"">ACH/Wire</td><td class=""px-4 py-2"">Bank transfer</td><td class=""px-4 py-2"">Large orders</td></tr>
        <tr><td class=""px-4 py-2"">PayPal</td><td class=""px-4 py-2"">Online payment</td><td class=""px-4 py-2"">Less common</td></tr>
    </tbody>
</table>

<h3>Payment Gateway Integration</h3>
<p>Configured Commerce integrates with Spreedly as the payment gateway abstraction:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Payment Flow
┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│   Checkout   │────▶│   Spreedly   │────▶│   Gateway    │
│    Page      │     │   (Abstraction) │   │ (Stripe,etc.)│
└──────────────┘     └──────────────┘     └──────────────┘
       │                    │                    │
   Card Entry          Tokenize &           Process
   (iFrame)            Route               Payment
</pre>

<h3>Spreedly Benefits</h3>
<ul>
    <li><strong>PCI Compliance</strong> - Card data never touches your servers</li>
    <li><strong>Gateway Agnostic</strong> - Switch gateways without code changes</li>
    <li><strong>Multiple Gateways</strong> - Route by card type, region, etc.</li>
    <li><strong>Stored Cards</strong> - Save payment methods securely</li>
</ul>

<h3>Purchase Order Payments</h3>
<p>Essential for B2B: pay by purchase order number</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">PO Configuration</p>
    <ul class=""mt-2 space-y-1 text-blue-700 dark:text-blue-300"">
        <li>✓ Require PO number at checkout</li>
        <li>✓ Validate PO format</li>
        <li>✓ Check against credit limit</li>
        <li>✓ Set payment terms (Net 30, Net 60)</li>
        <li>✓ Approval workflow for new customers</li>
    </ul>
</div>

<h3>Credit Terms</h3>
<p>Configure payment terms per customer:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Terms</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Net 30</td><td class=""px-4 py-2"">Payment due in 30 days</td></tr>
        <tr><td class=""px-4 py-2"">Net 60</td><td class=""px-4 py-2"">Payment due in 60 days</td></tr>
        <tr><td class=""px-4 py-2"">2/10 Net 30</td><td class=""px-4 py-2"">2% discount if paid in 10 days</td></tr>
        <tr><td class=""px-4 py-2"">Due on Receipt</td><td class=""px-4 py-2"">Immediate payment required</td></tr>
    </tbody>
</table>

<h3>Credit Limits</h3>
<p>Manage customer credit exposure:</p>
<ul>
    <li>Set credit limit per customer</li>
    <li>Track outstanding balance</li>
    <li>Block orders exceeding limit</li>
    <li>Credit hold/release workflow</li>
</ul>

<h3>3D Secure</h3>
<p>Additional authentication for card payments:</p>
<ul>
    <li>Reduces fraud liability</li>
    <li>Required in some regions (PSD2 in Europe)</li>
    <li>Configurable challenge thresholds</li>
</ul>

<h3>Security Considerations</h3>
<div class=""bg-red-50 dark:bg-red-900/20 border-l-4 border-red-500 p-4 my-4"">
    <p class=""font-medium text-red-800 dark:text-red-200"">PCI Compliance</p>
    <p class=""text-red-700 dark:text-red-300"">Never store raw credit card numbers. Use tokenization through Spreedly or your payment gateway. Configured Commerce is designed for PCI-DSS compliance when properly configured.</p>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 6: B2B Account Management

    private LearningModule BuildB2BAccountsModule()
    {
        return new LearningModule
        {
            Id = "b2b-accounts",
            Title = "B2B Account Management",
            Description = "Configure customer accounts, hierarchies, user roles, and budget management for B2B commerce.",
            Icon = "building-office",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-customer-accounts",
                    ModuleId = "b2b-accounts",
                    Title = "Customer Accounts",
                    Summary = "Configure customer account creation, authentication, and SSO.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure customer account settings",
                        "Implement account registration workflows",
                        "Set up Single Sign-On (SSO)",
                        "Manage authentication options"
                    },
                    Content = @"
<h2>Customer Accounts</h2>
<p>Customer accounts are the foundation of B2B commerce in Configured Commerce. They enable personalized pricing, order history, account hierarchies, and self-service capabilities.</p>

<h3>Account Components</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Customer Account
├── User (Login credentials)
├── Bill-To Customer (Business entity)
├── Ship-To Addresses
├── Payment Methods
├── Order History
├── Saved Lists & Carts
└── Account Settings
</pre>

<h3>Account Registration</h3>
<p>Configure how new accounts are created:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Registration Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Open Registration</td><td class=""px-4 py-2"">Anyone can create an account</td></tr>
        <tr><td class=""px-4 py-2"">Approval Required</td><td class=""px-4 py-2"">Admin approves new accounts</td></tr>
        <tr><td class=""px-4 py-2"">Invitation Only</td><td class=""px-4 py-2"">Admin creates accounts</td></tr>
        <tr><td class=""px-4 py-2"">ERP Sync</td><td class=""px-4 py-2"">Accounts created from ERP</td></tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">B2B Registration</p>
    <p class=""text-blue-700 dark:text-blue-300"">Most B2B sites use Approval Required or Invitation Only to ensure only legitimate business customers get access to wholesale pricing.</p>
</div>

<h3>Authentication Options</h3>
<ul>
    <li><strong>Username/Password</strong> - Traditional login</li>
    <li><strong>Email Link</strong> - Passwordless authentication</li>
    <li><strong>SSO</strong> - Single Sign-On integration</li>
</ul>

<h3>Single Sign-On (SSO)</h3>
<p>Configured Commerce supports multiple SSO providers:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Provider</th>
            <th class=""px-4 py-2 text-left"">Protocol</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Microsoft Entra ID (Azure AD)</td><td class=""px-4 py-2"">OpenID Connect</td></tr>
        <tr><td class=""px-4 py-2"">Google Workspace</td><td class=""px-4 py-2"">OpenID Connect</td></tr>
        <tr><td class=""px-4 py-2"">Facebook</td><td class=""px-4 py-2"">OAuth 2.0</td></tr>
        <tr><td class=""px-4 py-2"">Custom OIDC</td><td class=""px-4 py-2"">OpenID Connect</td></tr>
        <tr><td class=""px-4 py-2"">SAML</td><td class=""px-4 py-2"">SAML 2.0</td></tr>
    </tbody>
</table>

<h3>SSO Configuration</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
SSO Settings
├── Provider: Microsoft Entra ID
├── Client ID: [your-client-id]
├── Client Secret: [encrypted]
├── Authority: https://login.microsoftonline.com/{tenant}
├── Redirect URI: https://your-site.inscm.io/signin-oidc
├── Auto-Create Users: Yes/No
└── Default Role: Buyer
</pre>

<h3>Password Policies</h3>
<p>Configure password requirements:</p>
<ul>
    <li>Minimum length</li>
    <li>Complexity requirements (uppercase, numbers, symbols)</li>
    <li>Password expiration</li>
    <li>Password history (prevent reuse)</li>
    <li>Account lockout after failed attempts</li>
</ul>

<h3>My Account Features</h3>
<p>Self-service account management:</p>
<ul>
    <li>Profile information</li>
    <li>Address book</li>
    <li>Saved payment methods</li>
    <li>Order history</li>
    <li>Invoices and statements</li>
    <li>Account users (admin only)</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-account-hierarchies",
                    ModuleId = "b2b-accounts",
                    Title = "Account Hierarchies",
                    Summary = "Configure Bill-To/Ship-To relationships and organizational structures.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand Bill-To and Ship-To concepts",
                        "Configure organizational hierarchies",
                        "Set up multi-location accounts",
                        "Manage account relationships"
                    },
                    Content = @"
<h2>Account Hierarchies</h2>
<p>B2B organizations are complex. A single customer might have multiple locations, departments, and purchasing arrangements. Configured Commerce models these relationships through account hierarchies.</p>

<h3>Key Concepts</h3>

<h4>Bill-To Customer</h4>
<p>The business entity responsible for payment:</p>
<ul>
    <li>Legal entity name</li>
    <li>Payment terms and credit limit</li>
    <li>Invoice recipient</li>
    <li>Master account for pricing</li>
</ul>

<h4>Ship-To Customer</h4>
<p>Delivery locations under a Bill-To:</p>
<ul>
    <li>Physical shipping address</li>
    <li>Local contact information</li>
    <li>Delivery instructions</li>
    <li>May have unique pricing</li>
</ul>

<h3>Hierarchy Example</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Acme Corporation (Bill-To)
├── Acme HQ (Ship-To)
│   ├── Address: 123 Main St, London
│   └── Users: John (Admin), Sarah (Buyer)
├── Acme Manchester (Ship-To)
│   ├── Address: 456 Industrial Park, Manchester
│   └── Users: Mike (Buyer)
└── Acme Edinburgh (Ship-To)
    ├── Address: 789 Tech Center, Edinburgh
    └── Users: Lisa (Buyer), Tom (Approver)
</pre>

<h3>User Assignment</h3>
<p>Users belong to the hierarchy:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Assignment</th>
            <th class=""px-4 py-2 text-left"">Access</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Bill-To Level</td><td class=""px-4 py-2"">All Ship-To locations</td></tr>
        <tr><td class=""px-4 py-2"">Ship-To Level</td><td class=""px-4 py-2"">Only assigned location(s)</td></tr>
        <tr><td class=""px-4 py-2"">Multi-Ship-To</td><td class=""px-4 py-2"">Specific subset of locations</td></tr>
    </tbody>
</table>

<h3>Pricing at Each Level</h3>
<p>Pricing can be configured at multiple levels:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Price Resolution
1. Ship-To specific price (if exists)
2. Bill-To customer price (if exists)
3. Customer segment price list
4. Default price
</pre>
</div>

<h3>Multi-Location Features</h3>
<ul>
    <li><strong>Location Switcher</strong> - Users switch between assigned Ship-Tos</li>
    <li><strong>Location-Specific Inventory</strong> - See stock at nearest warehouse</li>
    <li><strong>Location-Specific Pricing</strong> - Regional pricing variations</li>
    <li><strong>Centralized Ordering</strong> - Order for multiple locations</li>
</ul>

<h3>Admin Configuration</h3>
<p>Manage hierarchies in Admin Console:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Navigate to Customers</li>
    <li>Create/Edit Bill-To customer</li>
    <li>Add Ship-To locations</li>
    <li>Assign users to appropriate level</li>
    <li>Configure pricing per level</li>
</ol>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-user-roles-permissions",
                    ModuleId = "b2b-accounts",
                    Title = "User Roles and Permissions",
                    Summary = "Configure user roles, permissions, and approval workflows.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the role-based permission system",
                        "Configure standard B2B roles",
                        "Implement custom roles",
                        "Set up approval workflows"
                    },
                    Content = @"
<h2>User Roles and Permissions</h2>
<p>B2B organizations need granular control over who can do what. Configured Commerce provides a flexible role-based access control (RBAC) system.</p>

<h3>Standard Roles</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Role</th>
            <th class=""px-4 py-2 text-left"">Capabilities</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Administrator</td><td class=""px-4 py-2"">Full account management, user CRUD, all permissions</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Approver</td><td class=""px-4 py-2"">Approve/reject orders, view all orders, manage budgets</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Buyer</td><td class=""px-4 py-2"">Create orders, view own orders, manage cart</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Requisitioner</td><td class=""px-4 py-2"">Create requisitions (require approval), limited purchasing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Browser</td><td class=""px-4 py-2"">View products and prices, no purchasing</td></tr>
    </tbody>
</table>

<h3>Permission Categories</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Permissions
├── Account Management
│   ├── Manage Users
│   ├── Manage Addresses
│   └── View Account Settings
├── Ordering
│   ├── Create Orders
│   ├── Approve Orders
│   ├── View All Orders
│   └── Request Quotes
├── Financial
│   ├── View Invoices
│   ├── Make Payments
│   └── Manage Budgets
└── Self-Service
    ├── Manage Own Profile
    ├── View Order History
    └── Track Shipments
</pre>
</div>

<h3>Approval Workflows</h3>
<p>Configure when orders require approval:</p>

<h4>Approval Triggers</h4>
<ul>
    <li><strong>Order Amount</strong> - Orders over £X require approval</li>
    <li><strong>User Role</strong> - Requisitioners always need approval</li>
    <li><strong>Product Type</strong> - Certain products require approval</li>
    <li><strong>Budget Exceeded</strong> - Over budget limit</li>
</ul>

<h4>Approval Flow</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Order Created (Requisitioner)
       │
       ▼
  Pending Approval
       │
       ▼
Approver Notification
       │
   ┌───┴───┐
   ▼       ▼
Approve  Reject
   │       │
   ▼       ▼
 Order   Return to
Submit   Requisitioner
</pre>

<h3>Custom Roles</h3>
<p>Create roles tailored to your organization:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Navigate to Admin &gt; Roles</li>
    <li>Click ""Create New Role""</li>
    <li>Name the role descriptively</li>
    <li>Select permissions from checklist</li>
    <li>Save and assign to users</li>
</ol>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">Example Custom Role</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">""Warehouse Manager"" - Can view orders, manage ship-to addresses, track shipments, but cannot place orders or manage users.</p>
</div>

<h3>Role Assignment</h3>
<p>Users can have:</p>
<ul>
    <li><strong>Single Role</strong> - Most users</li>
    <li><strong>Multiple Roles</strong> - Permissions are combined</li>
    <li><strong>Location-Specific Roles</strong> - Admin at one location, Buyer at another</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-budget-management",
                    ModuleId = "b2b-accounts",
                    Title = "Budget Management",
                    Summary = "Configure budget calendars, spending limits, and approval thresholds.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Set up budget calendars",
                        "Configure spending limits",
                        "Implement budget-based approvals",
                        "Track budget utilization"
                    },
                    Content = @"
<h2>Budget Management</h2>
<p>Many B2B organizations control spending through budgets. Configured Commerce provides tools to set limits, track spending, and enforce budget-based approvals.</p>

<h3>Budget Components</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Budget System
├── Budget Calendars (Fiscal periods)
├── Budget Limits (Per user/role/dept)
├── Spending Tracking
├── Enforcement Rules
└── Reporting
</pre>

<h3>Budget Calendars</h3>
<p>Define fiscal periods for budget tracking:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Period Type</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Annual</td><td class=""px-4 py-2"">Jan 1 - Dec 31</td></tr>
        <tr><td class=""px-4 py-2"">Quarterly</td><td class=""px-4 py-2"">Q1: Jan-Mar, Q2: Apr-Jun, etc.</td></tr>
        <tr><td class=""px-4 py-2"">Monthly</td><td class=""px-4 py-2"">Each calendar month</td></tr>
        <tr><td class=""px-4 py-2"">Custom</td><td class=""px-4 py-2"">Fiscal year Apr 1 - Mar 31</td></tr>
    </tbody>
</table>

<h3>Budget Assignment</h3>
<p>Budgets can be assigned at different levels:</p>
<ul>
    <li><strong>User Level</strong> - Individual spending limit</li>
    <li><strong>Role Level</strong> - All Buyers share a pool</li>
    <li><strong>Ship-To Level</strong> - Location-based budgets</li>
    <li><strong>Bill-To Level</strong> - Organization-wide limit</li>
    <li><strong>Cost Centre</strong> - Department budgets</li>
</ul>

<h3>Budget Configuration</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Example: Marketing Department Budget

Budget Name: Marketing Q1 2025
Period: Jan 1 - Mar 31, 2025
Limit: £50,000
Assignment: Cost Centre = ""Marketing""

Users:
├── Sarah (Marketing Manager) - Full access
├── John (Marketing Exec) - £5,000 limit
└── Lisa (Marketing Assist) - £1,000 limit, needs approval
</pre>
</div>

<h3>Enforcement Rules</h3>
<p>What happens when budget is exceeded:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Rule</th>
            <th class=""px-4 py-2 text-left"">Behavior</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Hard Limit</td><td class=""px-4 py-2"">Block order, cannot exceed</td></tr>
        <tr><td class=""px-4 py-2"">Soft Limit</td><td class=""px-4 py-2"">Warning, allow with approval</td></tr>
        <tr><td class=""px-4 py-2"">Warning Only</td><td class=""px-4 py-2"">Notify but allow order</td></tr>
        <tr><td class=""px-4 py-2"">Track Only</td><td class=""px-4 py-2"">No enforcement, reporting only</td></tr>
    </tbody>
</table>

<h3>Budget Dashboard</h3>
<p>Users see their budget status:</p>
<ul>
    <li>Current period budget</li>
    <li>Amount spent</li>
    <li>Amount remaining</li>
    <li>Pending orders against budget</li>
    <li>Budget utilization percentage</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Checkout Integration</p>
    <p class=""text-blue-700 dark:text-blue-300"">During checkout, users see their remaining budget and get warnings if the order would exceed their limit.</p>
</div>

<h3>Budget Reporting</h3>
<p>Available reports:</p>
<ul>
    <li>Budget vs Actual by period</li>
    <li>Spending by user/department</li>
    <li>Budget utilization trends</li>
    <li>Orders requiring budget approval</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 7: Orders and Fulfillment

    private LearningModule BuildOrdersFulfillmentModule()
    {
        return new LearningModule
        {
            Id = "orders-fulfillment",
            Title = "Orders and Fulfillment",
            Description = "Master order processing, quotes, requisitions, purchase orders, and order history management.",
            Icon = "clipboard-document-list",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-order-processing",
                    ModuleId = "orders-fulfillment",
                    Title = "Order Processing",
                    Summary = "Understand the order lifecycle and status management.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the order lifecycle",
                        "Configure order statuses",
                        "Process orders in the admin console",
                        "Handle order modifications"
                    },
                    Content = @"
<h2>Order Processing</h2>
<p>Orders are the core transaction records in Configured Commerce. Understanding the order lifecycle and status management is essential for effective order processing.</p>

<h3>Order Lifecycle</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Order Lifecycle
┌──────────────────────────────────────────────────────────────────┐
│                                                                   │
│  Cart → Order Submitted → Processing → Shipped → Complete        │
│           │                    │          │                       │
│           │                    │          └─► Partially Shipped   │
│           │                    │                                  │
│           │                    └─► On Hold                        │
│           │                                                       │
│           └─► Pending Approval (if required)                      │
│                      │                                            │
│               ┌──────┴──────┐                                     │
│               ▼             ▼                                     │
│           Approved      Rejected → Back to Cart                   │
│               │                                                   │
│               └─► Processing                                      │
│                                                                   │
│  At any stage: → Cancelled / → Return Requested                  │
│                                                                   │
└──────────────────────────────────────────────────────────────────┘
</pre>

<h3>Order Statuses</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Submitted</td><td class=""px-4 py-2"">Order received, awaiting processing</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Pending Approval</td><td class=""px-4 py-2"">Awaiting internal approval</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Processing</td><td class=""px-4 py-2"">Being prepared for shipment</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">On Hold</td><td class=""px-4 py-2"">Paused (payment, inventory, etc.)</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Shipped</td><td class=""px-4 py-2"">Items dispatched</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Partially Shipped</td><td class=""px-4 py-2"">Some items shipped</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Complete</td><td class=""px-4 py-2"">All items delivered</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Cancelled</td><td class=""px-4 py-2"">Order cancelled</td></tr>
    </tbody>
</table>

<h3>Order Data Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Order
├── Order Header
│   ├── Order Number
│   ├── Order Date
│   ├── Customer (Bill-To)
│   ├── Status
│   └── PO Number
├── Order Lines
│   ├── Product / SKU
│   ├── Quantity
│   ├── Unit Price
│   ├── Extended Price
│   └── Line Status
├── Shipments
│   ├── Ship-To Address
│   ├── Shipping Method
│   ├── Tracking Number
│   └── Shipment Lines
├── Payments
│   ├── Payment Method
│   ├── Amount
│   └── Transaction Reference
└── Totals
    ├── Subtotal
    ├── Shipping
    ├── Tax
    ├── Discounts
    └── Grand Total
</pre>

<h3>Admin Order Management</h3>
<p>Process orders in the Admin Console:</p>
<ul>
    <li><strong>View Orders</strong> - Search and filter order list</li>
    <li><strong>Order Details</strong> - Full order information</li>
    <li><strong>Update Status</strong> - Move through lifecycle</li>
    <li><strong>Add Notes</strong> - Internal comments</li>
    <li><strong>Create Shipments</strong> - Record shipping details</li>
    <li><strong>Process Refunds</strong> - Handle returns</li>
</ul>

<h3>ERP Integration</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Order Sync</p>
    <p class=""text-blue-700 dark:text-blue-300"">Orders are typically synced to ERP systems for fulfillment. Status updates flow back from ERP to keep customers informed of their order progress.</p>
</div>

<h3>Order Notifications</h3>
<p>Automated emails at key stages:</p>
<ul>
    <li>Order confirmation</li>
    <li>Order approved/rejected</li>
    <li>Shipment notification with tracking</li>
    <li>Delivery confirmation</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-quotes-requisitions",
                    ModuleId = "orders-fulfillment",
                    Title = "Quotes and Requisitions",
                    Summary = "Implement quote workflows and requisition management.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure quote request workflows",
                        "Manage quote responses",
                        "Implement requisition lists",
                        "Convert quotes and requisitions to orders"
                    },
                    Content = @"
<h2>Quotes and Requisitions</h2>
<p>B2B commerce often involves negotiation and internal approval before purchasing. Configured Commerce supports both quote requests and requisition workflows.</p>

<h3>Quote Requests</h3>
<p>Customers can request formal quotes instead of ordering directly:</p>

<h4>Quote Workflow</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Quote Workflow
┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│   Customer  │───▶│   Request   │───▶│   Sales     │
│   Cart      │    │   Quote     │    │   Review    │
└─────────────┘    └─────────────┘    └─────────────┘
                                             │
                   ┌─────────────────────────┤
                   │                         │
                   ▼                         ▼
            ┌─────────────┐          ┌─────────────┐
            │   Quote     │          │   Quote     │
            │   Sent      │          │   Declined  │
            └──────┬──────┘          └─────────────┘
                   │
          ┌────────┴────────┐
          ▼                 ▼
    ┌─────────────┐  ┌─────────────┐
    │  Customer   │  │   Quote     │
    │  Accepts    │  │   Expires   │
    └──────┬──────┘  └─────────────┘
           │
           ▼
    ┌─────────────┐
    │   Order     │
    │   Created   │
    └─────────────┘
</pre>

<h3>Quote Features</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Quote Expiration</td><td class=""px-4 py-2"">Quotes valid for configurable period</td></tr>
        <tr><td class=""px-4 py-2"">Line-Level Pricing</td><td class=""px-4 py-2"">Adjust price per line item</td></tr>
        <tr><td class=""px-4 py-2"">Quote Notes</td><td class=""px-4 py-2"">Add terms, conditions, notes</td></tr>
        <tr><td class=""px-4 py-2"">Quote History</td><td class=""px-4 py-2"">Track all quotes per customer</td></tr>
        <tr><td class=""px-4 py-2"">Email Quotes</td><td class=""px-4 py-2"">Send PDF quote to customer</td></tr>
    </tbody>
</table>

<h3>Requisitions</h3>
<p>Requisitions are internal purchase requests that require approval before becoming orders:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Requisition Flow

Requisitioner → Creates Requisition
                      │
                      ▼
              Pending Approval
                      │
           ┌──────────┴──────────┐
           ▼                     ▼
       Approved              Rejected
           │                     │
           ▼                     ▼
    Convert to Order      Back to Requester
</pre>
</div>

<h3>Requisition Use Cases</h3>
<ul>
    <li><strong>Budget Control</strong> - Ensure purchases fit department budget</li>
    <li><strong>Procurement Policy</strong> - Enforce purchasing rules</li>
    <li><strong>Audit Trail</strong> - Document approval chain</li>
    <li><strong>Decentralised Ordering</strong> - Let departments request, central team orders</li>
</ul>

<h3>Requisition Configuration</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Enable Requisitions</td><td class=""px-4 py-2"">Turn on requisition workflow</td></tr>
        <tr><td class=""px-4 py-2"">Approval Rules</td><td class=""px-4 py-2"">When approval is required</td></tr>
        <tr><td class=""px-4 py-2"">Approvers</td><td class=""px-4 py-2"">Who can approve requisitions</td></tr>
        <tr><td class=""px-4 py-2"">Auto-Approve Limit</td><td class=""px-4 py-2"">Amount under which auto-approved</td></tr>
    </tbody>
</table>

<h3>Quick Order / Saved Lists</h3>
<p>Related features for efficient ordering:</p>
<ul>
    <li><strong>Quick Order Pad</strong> - Enter SKUs directly for fast ordering</li>
    <li><strong>Saved Lists</strong> - Reusable product lists</li>
    <li><strong>Order Templates</strong> - Pre-configured carts for repeat orders</li>
    <li><strong>Copy Previous Order</strong> - Duplicate past orders</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-purchase-orders",
                    ModuleId = "orders-fulfillment",
                    Title = "Purchase Orders",
                    Summary = "Configure purchase order payment and management.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure PO payment method",
                        "Implement PO validation rules",
                        "Handle credit terms",
                        "Manage PO-based orders"
                    },
                    Content = @"
<h2>Purchase Orders</h2>
<p>Purchase order (PO) payment is essential for B2B commerce. Customers order on account and pay later according to agreed terms.</p>

<h3>PO Payment Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
PO Payment Flow
┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│  Customer   │───▶│  Submit PO  │───▶│   Order     │
│  Checkout   │    │   Number    │    │  Created    │
└─────────────┘    └─────────────┘    └─────────────┘
                                             │
                                             ▼
                                      ┌─────────────┐
                                      │  Invoice    │
                                      │  Generated  │
                                      └──────┬──────┘
                                             │
                                             ▼
                                      ┌─────────────┐
                                      │  Payment    │
                                      │  Due Date   │
                                      │  (Net 30)   │
                                      └──────┬──────┘
                                             │
                                             ▼
                                      ┌─────────────┐
                                      │  Payment    │
                                      │  Received   │
                                      └─────────────┘
</pre>

<h3>PO Configuration</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Require PO Number</td><td class=""px-4 py-2"">Must enter PO to checkout</td></tr>
        <tr><td class=""px-4 py-2"">PO Format Validation</td><td class=""px-4 py-2"">Regex pattern for PO numbers</td></tr>
        <tr><td class=""px-4 py-2"">Credit Check</td><td class=""px-4 py-2"">Verify credit limit before order</td></tr>
        <tr><td class=""px-4 py-2"">Credit Hold</td><td class=""px-4 py-2"">Block orders when over limit</td></tr>
    </tbody>
</table>

<h3>Credit Terms</h3>
<p>Common B2B payment terms:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<table class=""min-w-full"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Terms</th>
            <th class=""px-4 py-2 text-left"">Meaning</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Net 30</td><td class=""px-4 py-2"">Payment due within 30 days</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Net 60</td><td class=""px-4 py-2"">Payment due within 60 days</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Net 90</td><td class=""px-4 py-2"">Payment due within 90 days</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">2/10 Net 30</td><td class=""px-4 py-2"">2% discount if paid in 10 days, else net 30</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Due on Receipt</td><td class=""px-4 py-2"">Pay immediately upon delivery</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">COD</td><td class=""px-4 py-2"">Cash on delivery</td></tr>
    </tbody>
</table>
</div>

<h3>Credit Management</h3>
<ul>
    <li><strong>Credit Limit</strong> - Maximum outstanding balance</li>
    <li><strong>Credit Available</strong> - Limit minus current balance</li>
    <li><strong>Credit Hold</strong> - Flag accounts exceeding limit</li>
    <li><strong>Credit Review</strong> - Periodic reassessment</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">ERP Integration</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Credit limits and customer balances are typically managed in ERP and synced to Configured Commerce. Real-time credit checks may call ERP APIs.</p>
</div>

<h3>Invoice Management</h3>
<p>Customers can view and manage invoices:</p>
<ul>
    <li>View open invoices</li>
    <li>See payment due dates</li>
    <li>Download invoice PDFs</li>
    <li>Make payments against invoices</li>
    <li>View payment history</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-order-history-reorder",
                    ModuleId = "orders-fulfillment",
                    Title = "Order History and Reordering",
                    Summary = "Enable order history, tracking, and quick reorder functionality.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure order history display",
                        "Implement order tracking",
                        "Enable quick reorder features",
                        "Set up order notifications"
                    },
                    Content = @"
<h2>Order History and Reordering</h2>
<p>B2B customers frequently reorder the same products. Configured Commerce provides comprehensive order history and easy reordering capabilities.</p>

<h3>Order History Features</h3>
<ul>
    <li><strong>Order List</strong> - View all past orders with filtering</li>
    <li><strong>Order Details</strong> - Full breakdown of any order</li>
    <li><strong>Order Search</strong> - Find by order number, PO, date, product</li>
    <li><strong>Status Tracking</strong> - Current status and history</li>
    <li><strong>Shipment Tracking</strong> - Carrier tracking integration</li>
</ul>

<h3>Order History Page</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
ORDER HISTORY                                    [Search] [Filter]
─────────────────────────────────────────────────────────────────
Order #     Date        Status      Total       Actions
─────────────────────────────────────────────────────────────────
ORD-2025-001  Jan 15, 2025  Complete    £1,234.56   [View] [Reorder]
ORD-2025-002  Jan 18, 2025  Shipped     £567.89     [View] [Track]
ORD-2025-003  Jan 22, 2025  Processing  £890.12     [View]
─────────────────────────────────────────────────────────────────
                                        [Load More]
</pre>
</div>

<h3>Reorder Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Reorder Entire Order</td><td class=""px-4 py-2"">Add all items to cart</td></tr>
        <tr><td class=""px-4 py-2"">Reorder Single Item</td><td class=""px-4 py-2"">Add specific product to cart</td></tr>
        <tr><td class=""px-4 py-2"">Modify and Reorder</td><td class=""px-4 py-2"">Adjust quantities before adding</td></tr>
        <tr><td class=""px-4 py-2"">Schedule Reorder</td><td class=""px-4 py-2"">Set up recurring order</td></tr>
    </tbody>
</table>

<h3>Shipment Tracking</h3>
<p>Integrate carrier tracking for visibility:</p>
<ul>
    <li>Display tracking numbers</li>
    <li>Link to carrier tracking page</li>
    <li>Show estimated delivery</li>
    <li>Track multiple shipments per order</li>
</ul>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Shipment Tracking
─────────────────────────────────────────────────────
Shipment 1 of 2
Carrier: FedEx Ground
Tracking: 1234567890
Status: In Transit
Est. Delivery: Jan 25, 2025
[Track on FedEx Website]
─────────────────────────────────────────────────────
</pre>

<h3>Recently Purchased</h3>
<p>Highlight frequently purchased items:</p>
<ul>
    <li>Show on homepage or dedicated page</li>
    <li>Quick add to cart</li>
    <li>View purchase frequency</li>
    <li>Last purchase date and price</li>
</ul>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">B2B Efficiency</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">Reorder functionality significantly improves B2B customer efficiency. Many B2B buyers reorder 80% of the same products regularly.</p>
</div>

<h3>Order Notifications</h3>
<p>Keep customers informed:</p>
<ul>
    <li>Order confirmation email</li>
    <li>Shipment notification with tracking</li>
    <li>Delivery confirmation</li>
    <li>Invoice available notification</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 8: Integrations and APIs

    private LearningModule BuildIntegrationsAPIsModule()
    {
        return new LearningModule
        {
            Id = "integrations-apis",
            Title = "Integrations and APIs",
            Description = "Learn to work with REST APIs, ERP integration, PIM sync, and third-party integrations.",
            Icon = "arrow-path",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-rest-api-overview",
                    ModuleId = "integrations-apis",
                    Title = "REST API Overview",
                    Summary = "Understand the Storefront and Admin REST APIs.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the API architecture",
                        "Work with the Storefront API",
                        "Use the Admin API",
                        "Implement API authentication"
                    },
                    Content = @"
<h2>REST API Overview</h2>
<p>Configured Commerce exposes comprehensive REST APIs for both storefront operations and administrative functions. These APIs power the Spire CMS frontend and enable custom integrations.</p>

<h3>API Categories</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">API</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Base URL</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Storefront API</td><td class=""px-4 py-2"">Customer-facing operations</td><td class=""px-4 py-2"">/api/v1/</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Admin API</td><td class=""px-4 py-2"">Backend management</td><td class=""px-4 py-2"">/api/v1/admin/</td></tr>
    </tbody>
</table>

<h3>Storefront API Endpoints</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Storefront API Resources
├── /products         - Product catalog
├── /categories       - Category navigation
├── /cart             - Shopping cart operations
├── /orders           - Order management
├── /accounts         - Customer accounts
├── /wishlists        - Saved lists
├── /quotes           - Quote requests
├── /invoices         - Invoice access
└── /sessions         - Authentication
</pre>
</div>

<h3>Admin API Endpoints</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Admin API Resources
├── /products         - Product CRUD
├── /customers        - Customer management
├── /orders           - Order processing
├── /promotions       - Promotion management
├── /websites         - Site configuration
└── /jobs             - Scheduled jobs
</pre>
</div>

<h3>Authentication</h3>
<p>API authentication methods:</p>

<h4>Session-Based (Storefront)</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Login to get session
POST /api/v1/sessions
{
    ""userName"": ""user@example.com"",
    ""password"": ""password123""
}

// Response includes session cookie
// Subsequent requests include cookie automatically
</pre>

<h4>API Key (Admin/Integration)</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Include API key in header
GET /api/v1/admin/products
Authorization: Bearer {api-key}
</pre>

<h3>Request/Response Format</h3>
<p>All APIs use JSON:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// GET Product
GET /api/v1/products/SKU-12345

// Response
{
    ""product"": {
        ""id"": ""abc-123"",
        ""productNumber"": ""SKU-12345"",
        ""name"": ""Industrial Widget"",
        ""shortDescription"": ""High-quality widget"",
        ""pricing"": {
            ""unitPrice"": 45.00,
            ""currency"": ""GBP""
        },
        ""inventory"": {
            ""available"": 156
        }
    }
}
</pre>

<h3>HTTP Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Operation</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">GET</td><td class=""px-4 py-2"">Retrieve</td><td class=""px-4 py-2"">GET /products</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">POST</td><td class=""px-4 py-2"">Create</td><td class=""px-4 py-2"">POST /cart/cartlines</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">PATCH</td><td class=""px-4 py-2"">Update</td><td class=""px-4 py-2"">PATCH /cart/cartlines/{id}</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">PUT</td><td class=""px-4 py-2"">Replace</td><td class=""px-4 py-2"">PUT /products/{id}</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">DELETE</td><td class=""px-4 py-2"">Remove</td><td class=""px-4 py-2"">DELETE /cart/cartlines/{id}</td></tr>
    </tbody>
</table>

<h3>API Documentation</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">OpenAPI Specification</p>
    <p class=""text-blue-700 dark:text-blue-300"">Full API documentation is available in the Optimizely developer portal with OpenAPI/Swagger specifications for testing and code generation.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-erp-integration",
                    ModuleId = "integrations-apis",
                    Title = "ERP Integration",
                    Summary = "Connect Configured Commerce with ERP systems.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand ERP integration patterns",
                        "Configure product and customer sync",
                        "Implement order integration",
                        "Handle real-time vs batch sync"
                    },
                    Content = @"
<h2>ERP Integration</h2>
<p>ERP integration is critical for B2B commerce. Configured Commerce supports multiple integration patterns to connect with systems like SAP, Oracle, Microsoft Dynamics, and others.</p>

<h3>Integration Patterns</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Pattern</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
            <th class=""px-4 py-2 text-left"">Timing</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Batch Sync</td><td class=""px-4 py-2"">Product catalog, customers</td><td class=""px-4 py-2"">Scheduled (hourly, daily)</td></tr>
        <tr><td class=""px-4 py-2"">Real-Time</td><td class=""px-4 py-2"">Pricing, inventory, credit</td><td class=""px-4 py-2"">On-demand</td></tr>
        <tr><td class=""px-4 py-2"">Event-Driven</td><td class=""px-4 py-2"">Order submission</td><td class=""px-4 py-2"">Triggered by actions</td></tr>
        <tr><td class=""px-4 py-2"">Webhook</td><td class=""px-4 py-2"">Status updates from ERP</td><td class=""px-4 py-2"">Push from ERP</td></tr>
    </tbody>
</table>

<h3>Integration Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌────────────────────────────────────────────────────────────────┐
│                   CONFIGURED COMMERCE                           │
├────────────────────────────────────────────────────────────────┤
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐        │
│  │   Product    │  │   Customer   │  │    Order     │        │
│  │    Sync      │  │    Sync      │  │    Submit    │        │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘        │
│         │                 │                  │                 │
│         └────────────┬────┴──────────────────┘                │
│                      │                                         │
│              ┌───────▼───────┐                                │
│              │  Integration   │                                │
│              │    Layer       │                                │
│              │  (Handlers)    │                                │
│              └───────┬───────┘                                │
└──────────────────────┼─────────────────────────────────────────┘
                       │
              ┌────────▼────────┐
              │   Middleware    │
              │  (Optional)     │
              │  Azure Logic    │
              │  MuleSoft, etc. │
              └────────┬────────┘
                       │
              ┌────────▼────────┐
              │      ERP        │
              │  SAP, Oracle,   │
              │  Dynamics, etc. │
              └─────────────────┘
</pre>

<h3>Data Flow: Products</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
ERP → Configured Commerce (Batch)

Data Elements:
├── Product Number (SKU)
├── Description
├── Pricing
├── Category Assignment
├── Inventory Levels
├── Images (URLs)
└── Specifications
</pre>
</div>

<h3>Data Flow: Customers</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
ERP → Configured Commerce (Batch)

Data Elements:
├── Customer ID
├── Company Name
├── Bill-To / Ship-To
├── Payment Terms
├── Credit Limit
├── Price List Assignment
└── Contact Information
</pre>
</div>

<h3>Data Flow: Orders</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Configured Commerce → ERP (Real-Time)

Order Submitted:
├── Order Header
├── Line Items
├── Shipping Info
├── Payment Info
└── Customer Reference

ERP → Configured Commerce (Webhook)

Status Updates:
├── Order Acknowledged
├── Shipping Tracking
├── Invoice Created
└── Order Complete
</pre>
</div>

<h3>Integration Jobs</h3>
<p>Scheduled jobs for batch integration:</p>
<ul>
    <li><strong>Product Import</strong> - Pull products from ERP</li>
    <li><strong>Customer Import</strong> - Sync customer accounts</li>
    <li><strong>Inventory Update</strong> - Refresh stock levels</li>
    <li><strong>Price Update</strong> - Update pricing data</li>
    <li><strong>Order Export</strong> - Push orders to ERP</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Integration Complexity</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">ERP integration is often the most complex part of a B2B commerce implementation. Plan for thorough testing and consider using integration middleware for complex scenarios.</p>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-pim-integration",
                    ModuleId = "integrations-apis",
                    Title = "PIM Integration",
                    Summary = "Integrate with Optimizely Product Information Management.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand PIM integration architecture",
                        "Configure PIM sync jobs",
                        "Manage externally managed properties",
                        "Handle PIM publish workflow"
                    },
                    Content = @"
<h2>PIM Integration</h2>
<p>Optimizely Product Information Management (PIM) provides a centralised platform for managing product data. Configured Commerce has native integration with PIM.</p>

<h3>PIM Overview</h3>
<p>Optimizely PIM provides:</p>
<ul>
    <li><strong>Centralised Product Data</strong> - Single source of truth</li>
    <li><strong>Rich Content Management</strong> - Descriptions, images, videos</li>
    <li><strong>Workflow & Approval</strong> - Content review processes</li>
    <li><strong>Multi-Channel Publishing</strong> - Publish to commerce, print, etc.</li>
    <li><strong>Translation Management</strong> - Multi-language support</li>
</ul>

<h3>Integration Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                                                                  │
│  ┌───────────────┐                    ┌───────────────┐        │
│  │      ERP      │───── Products ────▶│     PIM       │        │
│  │               │                    │               │        │
│  └───────────────┘                    └───────┬───────┘        │
│                                               │                 │
│                                        Enrich & Approve         │
│                                               │                 │
│                                               ▼                 │
│                                       ┌───────────────┐        │
│                                       │   Publish     │        │
│                                       └───────┬───────┘        │
│                                               │                 │
│                                               ▼                 │
│                                       ┌───────────────┐        │
│                                       │  Configured   │        │
│                                       │   Commerce    │        │
│                                       └───────────────┘        │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>PIM Sync Jobs</h3>
<p>Configured Commerce includes specific jobs for PIM integration:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Job</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">PIM: Sync Setup Data</td><td class=""px-4 py-2"">Initial setup of languages, relationships, properties</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">PIM: Establish New Products</td><td class=""px-4 py-2"">Push new Commerce products to PIM</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">PIM: Refresh Published Products</td><td class=""px-4 py-2"">Pull approved products from PIM</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">PIM: Publish Approved Products</td><td class=""px-4 py-2"">Publish PIM changes to Commerce</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">PIM: Sync Product Data</td><td class=""px-4 py-2"">Sync externally managed properties</td></tr>
    </tbody>
</table>

<h3>Externally Managed Properties</h3>
<p>Some properties are managed outside PIM (e.g., in ERP):</p>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Example</p>
    <p class=""text-blue-700 dark:text-blue-300"">Product pricing might be managed in ERP, synced to Commerce, and also pushed to PIM for reporting - but PIM doesn't edit it.</p>
</div>

<h3>Property Configuration</h3>
<ul>
    <li><strong>PIM Managed</strong> - Edited in PIM, published to Commerce</li>
    <li><strong>Externally Managed</strong> - Managed elsewhere, synced through Commerce</li>
    <li><strong>Commerce Only</strong> - Not synced to/from PIM</li>
</ul>

<h3>Workflow Integration</h3>
<p>PIM approval workflow:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Product created/updated in PIM</li>
    <li>Content team enriches data</li>
    <li>Reviewer approves changes</li>
    <li>Product marked ""Ready to Publish""</li>
    <li>Sync job publishes to Commerce</li>
    <li>Product live on storefront</li>
</ol>

<h3>Best Practices</h3>
<ul>
    <li>Run sync jobs during off-peak hours</li>
    <li>Set Refresh and Publish jobs to run overnight</li>
    <li>Configure property groups before loading data</li>
    <li>Use PIM for content, ERP for transactional data</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-third-party-integrations",
                    ModuleId = "integrations-apis",
                    Title = "Third-Party Integrations",
                    Summary = "Connect payment gateways, shipping carriers, and other services.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Integrate payment gateways",
                        "Connect shipping carriers",
                        "Implement tax calculation services",
                        "Configure analytics and tracking"
                    },
                    Content = @"
<h2>Third-Party Integrations</h2>
<p>Configured Commerce integrates with numerous third-party services to provide complete commerce functionality.</p>

<h3>Payment Gateways</h3>
<p>Through Spreedly, Configured Commerce supports 100+ payment gateways:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Gateway</th>
            <th class=""px-4 py-2 text-left"">Features</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Stripe</td><td class=""px-4 py-2"">Cards, ACH, 3D Secure</td></tr>
        <tr><td class=""px-4 py-2"">Authorize.Net</td><td class=""px-4 py-2"">Cards, eCheck</td></tr>
        <tr><td class=""px-4 py-2"">Braintree</td><td class=""px-4 py-2"">Cards, PayPal, Venmo</td></tr>
        <tr><td class=""px-4 py-2"">Adyen</td><td class=""px-4 py-2"">Global payments, local methods</td></tr>
        <tr><td class=""px-4 py-2"">PayPal</td><td class=""px-4 py-2"">PayPal, Pay Later</td></tr>
    </tbody>
</table>

<h3>Shipping Carriers</h3>
<p>Real-time rates and tracking from major carriers:</p>
<ul>
    <li><strong>FedEx</strong> - Rates, tracking, labels</li>
    <li><strong>UPS</strong> - Rates, tracking, labels</li>
    <li><strong>DHL</strong> - International shipping</li>
    <li><strong>USPS</strong> - US postal service</li>
    <li><strong>Custom Carriers</strong> - Via plugin API</li>
</ul>

<h3>Tax Calculation</h3>
<p>Integrate tax calculation services:</p>
<ul>
    <li><strong>Avalara AvaTax</strong> - US sales tax automation</li>
    <li><strong>Vertex</strong> - Enterprise tax solution</li>
    <li><strong>TaxJar</strong> - Sales tax compliance</li>
    <li><strong>Custom</strong> - Via tax calculator plugin</li>
</ul>

<h3>Analytics & Tracking</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Analytics Integrations
├── Google Analytics 4
├── Adobe Analytics
├── Optimizely Data Platform (ODP)
├── Custom tracking pixels
└── Tag Manager support
</pre>
</div>

<h3>ODP Integration</h3>
<p>Optimizely Data Platform integration enables:</p>
<ul>
    <li><strong>Unified Customer Profiles</strong> - Cross-channel data</li>
    <li><strong>B2B Segmentation</strong> - Company-level targeting</li>
    <li><strong>Behavioural Tracking</strong> - Browse and purchase history</li>
    <li><strong>Campaign Integration</strong> - Triggered messaging</li>
</ul>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">2025 Update</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">ODP now includes a B2B schema specifically for Configured Commerce, enabling company-level (Bill-To/Ship-To) segmentation and targeting.</p>
</div>

<h3>Other Integrations</h3>
<ul>
    <li><strong>Address Validation</strong> - USPS, Google, Loqate</li>
    <li><strong>Email Marketing</strong> - Mailchimp, HubSpot, Klaviyo</li>
    <li><strong>Live Chat</strong> - Intercom, Zendesk, Drift</li>
    <li><strong>Reviews</strong> - Bazaarvoice, Yotpo</li>
    <li><strong>Search</strong> - Algolia, Elasticsearch, Coveo</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 9: Customization and Extension

    private LearningModule BuildCustomizationModule()
    {
        return new LearningModule
        {
            Id = "customization",
            Title = "Customization and Extension",
            Description = "Extend Configured Commerce with custom handlers, pipelines, widgets, and API extensions.",
            Icon = "wrench-screwdriver",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-handler-chains",
                    ModuleId = "customization",
                    Title = "Handler Chains",
                    Summary = "Customise API behaviour using frontend handler chains.",
                    Order = 1,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand how handler chains work",
                        "Create custom handlers",
                        "Inject handlers into existing chains",
                        "Override or replace existing handlers"
                    },
                    Content = @"
<h2>Handler Chains</h2>
<p>Handlers are the primary extension point for customising Configured Commerce behaviour. Both frontend (Spire) and backend use handler chains to process requests.</p>

<h3>Handler Chain Concept</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Handler Chain Execution
┌─────────────────────────────────────────────────────────────────┐
│                                                                  │
│  Request ──▶ Handler 1 ──▶ Handler 2 ──▶ Handler 3 ──▶ Response │
│              (Order: 100)  (Order: 200)  (Order: 300)           │
│                                                                  │
│  Each handler can:                                               │
│  • Modify the request                                            │
│  • Add/modify data                                               │
│  • Short-circuit the chain                                       │
│  • Do nothing (pass through)                                     │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Frontend Handlers (Spire)</h3>
<p>Spire handlers are TypeScript classes that process storefront actions:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Custom handler to modify product loading
import { Handler } from ""@insite/client-framework/HandlerCreator"";
import { GetProductApiV1Parameter } from ""@insite/client-framework/Services/ProductServiceV1"";

export const CustomProductHandler: Handler<GetProductApiV1Parameter> = {
    handler: async (props) => {
        const { parameter, dispatch, getState } = props;

        // Add custom logic before loading product
        console.log(`Loading product: ${parameter.productId}`);

        // Continue chain (optional - can short-circuit)
        return false; // Return false to continue, true to stop
    },
    order: 450, // Execute order (lower = earlier)
};

export default CustomProductHandler;
</pre>

<h3>Injecting Handlers</h3>
<p>Register custom handlers in your blueprint:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Blueprint handler registration
// Location: your-blueprint/src/Handlers/index.ts

import { addHandler } from ""@insite/client-framework/HandlerCreator"";
import CustomProductHandler from ""./CustomProductHandler"";

// Add to existing chain
addHandler(""GetProduct"", CustomProductHandler);
</pre>

<h3>Common Handler Chains</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Chain</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">GetProduct</td><td class=""px-4 py-2"">Load product details</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">AddToCart</td><td class=""px-4 py-2"">Add item to cart</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">UpdateCart</td><td class=""px-4 py-2"">Modify cart contents</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">SubmitOrder</td><td class=""px-4 py-2"">Process order submission</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">LoadSession</td><td class=""px-4 py-2"">Initialise user session</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">SearchProducts</td><td class=""px-4 py-2"">Product search</td></tr>
    </tbody>
</table>

<h3>Handler Order</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Execution Order</p>
    <p class=""text-blue-700 dark:text-blue-300"">Handlers execute in ascending order number. Use orders like 450, 550 to insert between standard handlers (which use 100, 200, 300, etc.).</p>
</div>

<h3>Replacing Handlers</h3>
<p>Replace an existing handler entirely:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
import { replaceHandler } from ""@insite/client-framework/HandlerCreator"";
import MyCustomAddToCart from ""./MyCustomAddToCart"";

// Replace the standard AddToCart handler
replaceHandler(""AddToCart"", ""AddProductToCartHandler"", MyCustomAddToCart);
</pre>

<h3>Best Practices</h3>
<ul>
    <li>Prefer adding handlers over replacing</li>
    <li>Keep handlers focused on single responsibility</li>
    <li>Use appropriate order numbers</li>
    <li>Handle errors gracefully</li>
    <li>Consider performance impact</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-server-side-extensions",
                    ModuleId = "customization",
                    Title = "Server-Side Extensions",
                    Summary = "Extend backend functionality with pipelines and plugins.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand server-side extension points",
                        "Create custom pipelines",
                        "Implement plugins",
                        "Deploy server-side customisations"
                    },
                    Content = @"
<h2>Server-Side Extensions</h2>
<p>Server-side customisations extend the Configured Commerce backend using C#/.NET. These run in the cloud environment and handle business logic.</p>

<h3>Extension Points</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Extension Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Handler Chains</td><td class=""px-4 py-2"">Customise API processing</td></tr>
        <tr><td class=""px-4 py-2"">Pipelines</td><td class=""px-4 py-2"">Reusable business logic</td></tr>
        <tr><td class=""px-4 py-2"">Plugins</td><td class=""px-4 py-2"">Modular components (tax, shipping, etc.)</td></tr>
        <tr><td class=""px-4 py-2"">Scheduled Jobs</td><td class=""px-4 py-2"">Background processing</td></tr>
        <tr><td class=""px-4 py-2"">Webhooks</td><td class=""px-4 py-2"">External event handling</td></tr>
    </tbody>
</table>

<h3>Pipeline Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Pipeline Execution
┌────────────────────────────────────────────────────────────────┐
│                                                                 │
│  Input ──▶ Pipe 1 ──▶ Pipe 2 ──▶ Pipe 3 ──▶ Output            │
│            (Validate)  (Process)  (Format)                     │
│                                                                 │
│  Pipelines are reusable chains called by handlers              │
│                                                                 │
└────────────────────────────────────────────────────────────────┘
</pre>

<h3>Standard Pipelines</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Available Pipelines
├── Account Pipelines
├── Cart Pipelines
├── Catalog Pipelines
├── Order Pipelines
├── Pricing Pipelines
├── Promotions Pipelines
├── Search Pipelines
└── Shipping Pipelines
</pre>
</div>

<h3>Creating a Custom Pipeline</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Custom pipeline pipe (C#)
public class ValidateMinimumOrderPipe : IPipe&lt;SubmitCartParameter, SubmitCartResult&gt;
{
    private readonly ICartService _cartService;

    public ValidateMinimumOrderPipe(ICartService cartService)
    {
        _cartService = cartService;
    }

    public int Order =&gt; 150; // Execute order

    public SubmitCartResult Execute(
        IUnitOfWork unitOfWork,
        SubmitCartParameter parameter,
        SubmitCartResult result)
    {
        var cart = _cartService.GetCart(parameter.CartId);

        if (cart.OrderTotal &lt; 50.00m)
        {
            result.ResultCode = ResultCode.Error;
            result.SubCode = ""MinimumOrderNotMet"";
            result.Message = ""Minimum order amount is £50"";
            return result;
        }

        return result; // Continue pipeline
    }
}
</pre>

<h3>Plugin Development</h3>
<p>Plugins are modular components for specific functions:</p>

<h4>Tax Calculator Plugin</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
public class CustomTaxCalculator : ITaxCalculator
{
    public TaxCalculationResult CalculateTax(TaxCalculationParameter parameter)
    {
        // Custom tax logic
        var taxRate = GetTaxRateForRegion(parameter.ShipToAddress);
        var taxAmount = parameter.OrderSubtotal * taxRate;

        return new TaxCalculationResult
        {
            TaxAmount = taxAmount,
            TaxLines = new[] { new TaxLine { Amount = taxAmount } }
        };
    }
}
</pre>

<h3>Deployment</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Cloud Deployment</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Server-side extensions are deployed via GitHub. Push to the appropriate branch (develop, qa, main) and the CI/CD pipeline builds and deploys automatically.</p>
</div>

<h3>Project Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Extensions/
├── Handlers/
│   └── CustomOrderHandler.cs
├── Pipelines/
│   └── ValidateMinimumOrderPipe.cs
├── Plugins/
│   └── CustomTaxCalculator.cs
├── Jobs/
│   └── CustomSyncJob.cs
└── Extensions.csproj
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-widget-customization",
                    ModuleId = "customization",
                    Title = "Widget Customization",
                    Summary = "Override and create custom Spire widgets.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Override existing widgets",
                        "Create new custom widgets",
                        "Configure widget CMS fields",
                        "Style widgets appropriately"
                    },
                    Content = @"
<h2>Widget Customization</h2>
<p>Widgets are the UI components of Spire CMS. You can override existing widgets or create entirely new ones to customize the storefront.</p>

<h3>Override Strategy</h3>
<p>Choose the right approach based on your needs:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Approach</th>
            <th class=""px-4 py-2 text-left"">When to Use</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">CSS Override</td><td class=""px-4 py-2"">Styling changes only</td></tr>
        <tr><td class=""px-4 py-2"">Partial Override</td><td class=""px-4 py-2"">Modify specific parts</td></tr>
        <tr><td class=""px-4 py-2"">Full Override</td><td class=""px-4 py-2"">Replace entire widget</td></tr>
        <tr><td class=""px-4 py-2"">New Widget</td><td class=""px-4 py-2"">New functionality</td></tr>
    </tbody>
</table>

<h3>Overriding a Widget</h3>
<p>Place override file in the correct location:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
# Original widget location
modules/content-library/src/Widgets/Header/MainNavigation.tsx

# Override location in your blueprint
modules/blueprints/your-blueprint/src/Overrides/Widgets/Header/MainNavigation.tsx
</pre>

<h3>Custom Widget Example</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// your-blueprint/src/Widgets/QuickOrderWidget.tsx
import * as React from ""react"";
import WidgetModule from ""@insite/client-framework/Types/WidgetModule"";
import WidgetProps from ""@insite/client-framework/Types/WidgetProps"";
import { useState } from ""react"";

interface QuickOrderWidgetProps extends WidgetProps {
    fields: {
        title: string;
        maxItems: number;
        showPrices: boolean;
    };
}

const QuickOrderWidget: React.FC<QuickOrderWidgetProps> = ({ fields }) => {
    const [items, setItems] = useState<string[]>([]);

    const handleAddItem = (sku: string) => {
        if (items.length < fields.maxItems) {
            setItems([...items, sku]);
        }
    };

    return (
        <div className=""quick-order-widget"">
            <h3>{fields.title}</h3>
            <div className=""quick-order-inputs"">
                {/* Input fields for SKU entry */}
            </div>
            <button onClick={() => addAllToCart(items)}>
                Add All to Cart
            </button>
        </div>
    );
};

const widgetModule: WidgetModule = {
    component: QuickOrderWidget,
    definition: {
        group: ""Custom"",
        displayName: ""Quick Order"",
        allowedContexts: [""Header"", ""ProductListPage"", ""MyAccountPage""],
        fieldDefinitions: [
            {
                name: ""title"",
                displayName: ""Title"",
                editorTemplate: ""TextField"",
                defaultValue: ""Quick Order"",
            },
            {
                name: ""maxItems"",
                displayName: ""Maximum Items"",
                editorTemplate: ""IntegerField"",
                defaultValue: 10,
            },
            {
                name: ""showPrices"",
                displayName: ""Show Prices"",
                editorTemplate: ""CheckboxField"",
                defaultValue: true,
            },
        ],
    },
};

export default widgetModule;
</pre>

<h3>Field Editor Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Editor</th>
            <th class=""px-4 py-2 text-left"">Data Type</th>
            <th class=""px-4 py-2 text-left"">Use For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">TextField</td><td class=""px-4 py-2"">string</td><td class=""px-4 py-2"">Short text</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">TextAreaField</td><td class=""px-4 py-2"">string</td><td class=""px-4 py-2"">Long text</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">RichTextField</td><td class=""px-4 py-2"">string (HTML)</td><td class=""px-4 py-2"">Formatted content</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IntegerField</td><td class=""px-4 py-2"">number</td><td class=""px-4 py-2"">Whole numbers</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">CheckboxField</td><td class=""px-4 py-2"">boolean</td><td class=""px-4 py-2"">Yes/No options</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">DropDownField</td><td class=""px-4 py-2"">string</td><td class=""px-4 py-2"">Selection list</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ColorPickerField</td><td class=""px-4 py-2"">string (hex)</td><td class=""px-4 py-2"">Colours</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ImagePickerField</td><td class=""px-4 py-2"">string (URL)</td><td class=""px-4 py-2"">Images</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">LinkField</td><td class=""px-4 py-2"">object</td><td class=""px-4 py-2"">URLs/Links</td></tr>
    </tbody>
</table>

<h3>Widget Styling</h3>
<p>Style widgets using CSS modules or styled-components:</p>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Using CSS module
import styles from ""./QuickOrderWidget.module.css"";

<div className={styles.quickOrderWidget}>
    ...
</div>
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-api-extensions",
                    ModuleId = "customization",
                    Title = "API Extensions",
                    Summary = "Create custom API endpoints and extend existing APIs.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create custom API endpoints",
                        "Extend existing API responses",
                        "Implement API authentication",
                        "Handle API versioning"
                    },
                    Content = @"
<h2>API Extensions</h2>
<p>Configured Commerce allows you to create custom API endpoints and extend existing APIs to support custom functionality.</p>

<h3>Custom API Endpoint</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Custom API controller
[ApiController]
[Route(""api/v1/custom"")]
public class CustomApiController : ControllerBase
{
    private readonly ICustomService _customService;

    public CustomApiController(ICustomService customService)
    {
        _customService = customService;
    }

    [HttpGet(""data/{id}"")]
    public async Task&lt;ActionResult&lt;CustomDataResult&gt;&gt; GetCustomData(string id)
    {
        var result = await _customService.GetDataAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost(""process"")]
    [Authorize]
    public async Task&lt;ActionResult&lt;ProcessResult&gt;&gt; ProcessData(
        [FromBody] ProcessRequest request)
    {
        var result = await _customService.ProcessAsync(request);
        return Ok(result);
    }
}
</pre>

<h3>Extending Existing APIs</h3>
<p>Add data to existing API responses using handlers:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Handler to add custom data to product response
public class AddCustomProductDataHandler : HandlerBase&lt;GetProductParameter, GetProductResult&gt;
{
    private readonly ICustomDataService _customDataService;

    public AddCustomProductDataHandler(ICustomDataService customDataService)
    {
        _customDataService = customDataService;
    }

    public override int Order =&gt; 600; // After standard handlers

    public override GetProductResult Execute(
        IUnitOfWork unitOfWork,
        GetProductParameter parameter,
        GetProductResult result)
    {
        if (result.Product == null)
            return result;

        // Add custom properties
        var customData = _customDataService.GetForProduct(result.Product.Id);
        result.Product.Properties[""CustomRating""] = customData.Rating;
        result.Product.Properties[""CustomBadge""] = customData.Badge;

        return result;
    }
}
</pre>

<h3>API Authentication</h3>
<p>Secure custom APIs appropriately:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute</th>
            <th class=""px-4 py-2 text-left"">Access Level</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">[AllowAnonymous]</td><td class=""px-4 py-2"">Public access</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">[Authorize]</td><td class=""px-4 py-2"">Authenticated users</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">[Authorize(Roles = ""Admin"")]</td><td class=""px-4 py-2"">Specific roles</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">[ApiKey]</td><td class=""px-4 py-2"">API key auth</td></tr>
    </tbody>
</table>

<h3>Calling Custom APIs from Spire</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Spire handler calling custom API
import { fetch } from ""@insite/client-framework/ServerSideRendering"";

export const loadCustomData: Handler = {
    handler: async ({ parameter, dispatch }) => {
        const response = await fetch(`/api/v1/custom/data/${parameter.id}`);

        if (response.ok) {
            const data = await response.json();
            dispatch({
                type: ""CustomData/SetData"",
                payload: data,
            });
        }
    },
    order: 500,
};
</pre>

<h3>API Best Practices</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <ul class=""space-y-1"">
        <li>✓ Follow REST conventions</li>
        <li>✓ Use appropriate HTTP methods</li>
        <li>✓ Return proper status codes</li>
        <li>✓ Include error details in responses</li>
        <li>✓ Validate input data</li>
        <li>✓ Document your APIs</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 10: Advanced Topics

    private LearningModule BuildAdvancedTopicsModule()
    {
        return new LearningModule
        {
            Id = "advanced-topics",
            Title = "Advanced Topics",
            Description = "Explore multi-site configurations, mobile commerce, analytics, performance, and security.",
            Icon = "rocket-launch",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cc-multi-site-multi-market",
                    ModuleId = "advanced-topics",
                    Title = "Multi-Site and Multi-Market",
                    Summary = "Configure multiple sites and market-specific experiences.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure multiple websites",
                        "Set up market-specific settings",
                        "Implement multi-currency support",
                        "Handle multi-language content"
                    },
                    Content = @"
<h2>Multi-Site and Multi-Market</h2>
<p>Configured Commerce supports running multiple websites from a single installation, each with its own configuration, currencies, languages, and content.</p>

<h3>Multi-Site Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Multi-Site Configuration
┌─────────────────────────────────────────────────────────────────┐
│                    CONFIGURED COMMERCE                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐            │
│  │  Website 1  │  │  Website 2  │  │  Website 3  │            │
│  │  (UK Site)  │  │  (US Site)  │  │  (EU Site)  │            │
│  └──────┬──────┘  └──────┬──────┘  └──────┬──────┘            │
│         │                │                │                     │
│  ┌──────▼──────┐  ┌──────▼──────┐  ┌──────▼──────┐            │
│  │   Domain    │  │   Domain    │  │   Domain    │            │
│  │ uk.site.com │  │ us.site.com │  │ eu.site.com │            │
│  └─────────────┘  └─────────────┘  └─────────────┘            │
│         │                │                │                     │
│         └────────────────┴────────────────┘                    │
│                          │                                      │
│                  Shared Resources:                              │
│                  - Products                                     │
│                  - Customers                                    │
│                  - Backend Systems                              │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Website Configuration</h3>
<p>Each website can have unique settings:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Domain</td><td class=""px-4 py-2"">Website URL</td></tr>
        <tr><td class=""px-4 py-2"">Default Language</td><td class=""px-4 py-2"">Primary site language</td></tr>
        <tr><td class=""px-4 py-2"">Default Currency</td><td class=""px-4 py-2"">Primary currency</td></tr>
        <tr><td class=""px-4 py-2"">Catalog</td><td class=""px-4 py-2"">Assigned product catalog</td></tr>
        <tr><td class=""px-4 py-2"">Warehouses</td><td class=""px-4 py-2"">Available for fulfillment</td></tr>
        <tr><td class=""px-4 py-2"">Blueprint</td><td class=""px-4 py-2"">Spire design/theme</td></tr>
    </tbody>
</table>

<h3>Multi-Currency</h3>
<p>Support multiple currencies across sites or within a site:</p>
<ul>
    <li><strong>Currency Configuration</strong> - Define available currencies</li>
    <li><strong>Exchange Rates</strong> - Set or auto-update rates</li>
    <li><strong>Currency Display</strong> - Format and symbol settings</li>
    <li><strong>Price Entry</strong> - Prices in base or each currency</li>
</ul>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Currency Configuration
├── GBP (Base Currency)
│   └── Exchange Rate: 1.00
├── USD
│   └── Exchange Rate: 1.27
├── EUR
│   └── Exchange Rate: 1.16
└── AUD
    └── Exchange Rate: 1.93
</pre>
</div>

<h3>Multi-Language</h3>
<p>Provide content in multiple languages:</p>
<ul>
    <li><strong>Language Configuration</strong> - Define supported languages</li>
    <li><strong>Translation Management</strong> - UI and content translations</li>
    <li><strong>Product Translations</strong> - Via PIM or direct entry</li>
    <li><strong>Language Detection</strong> - Browser-based or user selection</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">AI Translation</p>
    <p class=""text-blue-700 dark:text-blue-300"">Optimizely is adding AI-powered bulk translation capabilities to PIM and Configured Commerce, enabling faster multi-language content creation.</p>
</div>

<h3>Market-Specific Settings</h3>
<p>Configure by market:</p>
<ul>
    <li>Regional pricing</li>
    <li>Tax configuration</li>
    <li>Shipping providers</li>
    <li>Payment methods</li>
    <li>Product availability</li>
    <li>Legal requirements (GDPR, etc.)</li>
</ul>

<h3>Shared vs Separate</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Shared Across Sites</th>
            <th class=""px-4 py-2 text-left"">Site-Specific</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Product Catalog (optional)</td><td class=""px-4 py-2"">Pricing</td></tr>
        <tr><td class=""px-4 py-2"">Customer Accounts</td><td class=""px-4 py-2"">Content/CMS</td></tr>
        <tr><td class=""px-4 py-2"">Backend Integrations</td><td class=""px-4 py-2"">Branding/Theme</td></tr>
        <tr><td class=""px-4 py-2"">Admin Console</td><td class=""px-4 py-2"">Tax/Shipping Config</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-mobile-commerce",
                    ModuleId = "advanced-topics",
                    Title = "Mobile Commerce",
                    Summary = "Implement mobile commerce with responsive design and mobile SDK.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand mobile commerce options",
                        "Configure responsive design",
                        "Use the Mobile SDK",
                        "Optimise for mobile performance"
                    },
                    Content = @"
<h2>Mobile Commerce</h2>
<p>Configured Commerce supports mobile commerce through responsive web design and a native mobile SDK for building dedicated apps.</p>

<h3>Mobile Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Approach</th>
            <th class=""px-4 py-2 text-left"">Pros</th>
            <th class=""px-4 py-2 text-left"">Cons</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Responsive Web</td><td class=""px-4 py-2"">Single codebase, easy updates</td><td class=""px-4 py-2"">Limited device features</td></tr>
        <tr><td class=""px-4 py-2"">Progressive Web App</td><td class=""px-4 py-2"">Installable, offline support</td><td class=""px-4 py-2"">iOS limitations</td></tr>
        <tr><td class=""px-4 py-2"">Native App (SDK)</td><td class=""px-4 py-2"">Full device access, performance</td><td class=""px-4 py-2"">Separate development</td></tr>
    </tbody>
</table>

<h3>Responsive Spire CMS</h3>
<p>Spire CMS is built responsive by default:</p>
<ul>
    <li>Mobile-first CSS approach</li>
    <li>Responsive grid system</li>
    <li>Touch-friendly UI components</li>
    <li>Device-specific previews in CMS</li>
</ul>

<h3>Mobile SDK</h3>
<p>Build native mobile apps with the Configured Commerce Mobile SDK:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
<pre>
Mobile SDK Stack
├── Framework: Flutter
├── API Layer: Dart SDK
├── UI Layer: Flutter UI SDK
└── Platforms: iOS + Android
</pre>
</div>

<h3>SDK Features</h3>
<ul>
    <li><strong>Full Commerce</strong> - Browse, cart, checkout</li>
    <li><strong>Account Management</strong> - Login, profile, orders</li>
    <li><strong>Barcode Scanning</strong> - Scan to add products</li>
    <li><strong>Push Notifications</strong> - Order updates, promotions</li>
    <li><strong>Offline Mode</strong> - Basic browsing offline</li>
    <li><strong>Customizable UI</strong> - Branding and theming</li>
</ul>

<h3>SDK Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                       MOBILE APP                                 │
├─────────────────────────────────────────────────────────────────┤
│  ┌─────────────────────────────────────────────────────────┐   │
│  │               Flutter UI Components                      │   │
│  │  (Product List, Cart, Checkout, Account screens)        │   │
│  └─────────────────────────────────────────────────────────┘   │
│                             │                                    │
│  ┌─────────────────────────▼─────────────────────────────────┐ │
│  │                   Dart API SDK                             │ │
│  │  (Type-safe API calls, data models, state management)     │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                             │                                    │
└─────────────────────────────┼────────────────────────────────────┘
                              │
                    ┌─────────▼─────────┐
                    │  Commerce REST    │
                    │      APIs         │
                    └───────────────────┘
</pre>

<h3>Mobile Optimisation Tips</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <ul class=""space-y-1"">
        <li>✓ Optimise images for mobile (responsive images)</li>
        <li>✓ Minimise JavaScript bundle size</li>
        <li>✓ Use lazy loading for off-screen content</li>
        <li>✓ Implement touch-friendly navigation</li>
        <li>✓ Test on actual devices, not just emulators</li>
        <li>✓ Consider simplified checkout for mobile</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-analytics-reporting",
                    ModuleId = "advanced-topics",
                    Title = "Analytics and Reporting",
                    Summary = "Implement analytics, tracking, and ODP integration.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure analytics tracking",
                        "Integrate with ODP",
                        "Use B2B segmentation",
                        "Create commerce reports"
                    },
                    Content = @"
<h2>Analytics and Reporting</h2>
<p>Understanding customer behaviour and business performance requires comprehensive analytics. Configured Commerce integrates with multiple analytics platforms.</p>

<h3>Analytics Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Platform</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Google Analytics 4</td><td class=""px-4 py-2"">Web traffic, e-commerce tracking</td></tr>
        <tr><td class=""px-4 py-2"">Adobe Analytics</td><td class=""px-4 py-2"">Enterprise analytics</td></tr>
        <tr><td class=""px-4 py-2"">Optimizely Data Platform</td><td class=""px-4 py-2"">Customer profiles, B2B segmentation</td></tr>
        <tr><td class=""px-4 py-2"">Built-in Reports</td><td class=""px-4 py-2"">Commerce-specific metrics</td></tr>
    </tbody>
</table>

<h3>Optimizely Data Platform (ODP)</h3>
<p>ODP provides unified customer data and B2B-specific capabilities:</p>

<div class=""bg-emerald-50 dark:bg-emerald-900/20 border-l-4 border-emerald-500 p-4 my-4"">
    <p class=""font-medium text-emerald-800 dark:text-emerald-200"">B2B Schema (2025)</p>
    <p class=""text-emerald-700 dark:text-emerald-300"">ODP now includes a B2B schema for Configured Commerce, enabling segmentation and targeting at the Bill-To and Ship-To level, not just individual users.</p>
</div>

<h3>ODP Integration Features</h3>
<ul>
    <li><strong>Unified Profiles</strong> - Cross-channel customer data</li>
    <li><strong>B2B Segmentation</strong> - Company-level targeting</li>
    <li><strong>Behavioural Tracking</strong> - Browse, cart, purchase events</li>
    <li><strong>Real-Time Data</strong> - Immediate profile updates</li>
    <li><strong>Activation</strong> - Trigger campaigns, personalize</li>
</ul>

<h3>ODP Events</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Tracked Events
├── Product Views
├── Category Views
├── Add to Cart
├── Remove from Cart
├── Checkout Started
├── Order Completed
├── Account Created
├── Account Login
└── Search Performed
</pre>

<h3>B2B Profile Data</h3>
<p>ODP B2B profiles include:</p>
<ul>
    <li>Bill-To customer data</li>
    <li>Ship-To locations</li>
    <li>Purchase history (company level)</li>
    <li>User roles within organization</li>
    <li>Industry/vertical classification</li>
</ul>

<h3>Built-in Commerce Reports</h3>
<p>Admin Console provides standard reports:</p>
<ul>
    <li><strong>Sales Dashboard</strong> - Revenue, orders, trends</li>
    <li><strong>Product Performance</strong> - Best sellers, views, conversion</li>
    <li><strong>Customer Reports</strong> - New vs returning, top customers</li>
    <li><strong>Order Reports</strong> - Status, fulfillment metrics</li>
    <li><strong>Search Analytics</strong> - Popular terms, no-results</li>
</ul>

<h3>Analytics Implementation</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
// Example: Enhanced E-commerce tracking
// Spire handler for tracking purchases
export const TrackPurchaseHandler: Handler = {
    handler: async ({ result }) => {
        if (result.order) {
            // Google Analytics
            gtag('event', 'purchase', {
                transaction_id: result.order.orderNumber,
                value: result.order.orderTotal,
                currency: result.order.currency,
                items: result.order.lineItems.map(item => ({
                    item_id: item.productNumber,
                    item_name: item.productName,
                    price: item.unitPrice,
                    quantity: item.quantity,
                })),
            });

            // ODP
            zaius.event('order', {
                order_id: result.order.orderNumber,
                total: result.order.orderTotal,
            });
        }
    },
    order: 900,
};
</pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cc-performance-security",
                    ModuleId = "advanced-topics",
                    Title = "Performance and Security",
                    Summary = "Optimise performance and implement security best practices.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Optimise site performance",
                        "Implement caching strategies",
                        "Follow security best practices",
                        "Handle compliance requirements"
                    },
                    Content = @"
<h2>Performance and Security</h2>
<p>A fast, secure site is critical for B2B commerce success. This lesson covers performance optimisation and security best practices.</p>

<h3>Performance Optimisation</h3>

<h4>Frontend Performance</h4>
<ul>
    <li><strong>Code Splitting</strong> - Load JavaScript on demand</li>
    <li><strong>Image Optimisation</strong> - Responsive images, WebP format</li>
    <li><strong>Lazy Loading</strong> - Defer off-screen content</li>
    <li><strong>Bundle Analysis</strong> - Identify large dependencies</li>
    <li><strong>CDN Usage</strong> - Static asset delivery</li>
</ul>

<h4>API Performance</h4>
<ul>
    <li><strong>Efficient Queries</strong> - Request only needed fields</li>
    <li><strong>Batch Requests</strong> - Combine multiple API calls</li>
    <li><strong>Response Caching</strong> - Cache appropriate responses</li>
    <li><strong>Pagination</strong> - Limit result sets</li>
</ul>

<h3>Caching Strategies</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Cache Type</th>
            <th class=""px-4 py-2 text-left"">Use For</th>
            <th class=""px-4 py-2 text-left"">TTL</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Browser Cache</td><td class=""px-4 py-2"">Static assets (JS, CSS, images)</td><td class=""px-4 py-2"">Long (1 year)</td></tr>
        <tr><td class=""px-4 py-2"">CDN Cache</td><td class=""px-4 py-2"">Public content, images</td><td class=""px-4 py-2"">Medium (hours)</td></tr>
        <tr><td class=""px-4 py-2"">API Cache</td><td class=""px-4 py-2"">Product data, categories</td><td class=""px-4 py-2"">Short (minutes)</td></tr>
        <tr><td class=""px-4 py-2"">Session Cache</td><td class=""px-4 py-2"">User-specific data</td><td class=""px-4 py-2"">Session duration</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p class=""font-medium text-yellow-800 dark:text-yellow-200"">Cache Invalidation</p>
    <p class=""text-yellow-700 dark:text-yellow-300"">Be careful with caching customer-specific data like pricing. Ensure caches are properly segmented by customer or disabled for personalized content.</p>
</div>

<h3>Security Best Practices</h3>

<h4>Authentication & Authorization</h4>
<ul>
    <li>Strong password policies</li>
    <li>Multi-factor authentication (MFA) option</li>
    <li>Role-based access control</li>
    <li>Session timeout configuration</li>
    <li>Account lockout after failed attempts</li>
</ul>

<h4>Data Protection</h4>
<ul>
    <li>HTTPS everywhere</li>
    <li>PCI-DSS compliance (via Spreedly)</li>
    <li>Sensitive data encryption</li>
    <li>Secure API key storage</li>
    <li>Input validation and sanitization</li>
</ul>

<h4>Infrastructure Security</h4>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Optimizely Cloud Security
├── DDoS Protection
├── Web Application Firewall (WAF)
├── SSL/TLS Encryption
├── Regular Security Patching
├── SOC 2 Compliance
└── Data Center Security
</pre>

<h3>Compliance Considerations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Regulation</th>
            <th class=""px-4 py-2 text-left"">Applies To</th>
            <th class=""px-4 py-2 text-left"">Key Requirements</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">GDPR</td><td class=""px-4 py-2"">EU customers</td><td class=""px-4 py-2"">Consent, data rights, privacy</td></tr>
        <tr><td class=""px-4 py-2"">PCI-DSS</td><td class=""px-4 py-2"">Payment processing</td><td class=""px-4 py-2"">Card data security</td></tr>
        <tr><td class=""px-4 py-2"">CCPA</td><td class=""px-4 py-2"">California consumers</td><td class=""px-4 py-2"">Privacy rights, opt-out</td></tr>
        <tr><td class=""px-4 py-2"">SOC 2</td><td class=""px-4 py-2"">Cloud services</td><td class=""px-4 py-2"">Security controls</td></tr>
    </tbody>
</table>

<h3>Performance Monitoring</h3>
<ul>
    <li><strong>Real User Monitoring (RUM)</strong> - Actual user performance</li>
    <li><strong>Synthetic Monitoring</strong> - Automated performance tests</li>
    <li><strong>API Monitoring</strong> - Response times, errors</li>
    <li><strong>Alerting</strong> - Notify on degradation</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium text-blue-800 dark:text-blue-200"">Core Web Vitals</p>
    <p class=""text-blue-700 dark:text-blue-300"">Monitor and optimise for Google's Core Web Vitals: LCP (loading), FID (interactivity), and CLS (visual stability) to ensure good user experience and SEO.</p>
</div>

<h3>Security Checklist</h3>
<ul>
    <li>☐ HTTPS enabled site-wide</li>
    <li>☐ Strong password policy configured</li>
    <li>☐ API keys stored securely (not in code)</li>
    <li>☐ Admin access restricted by IP/role</li>
    <li>☐ Regular security scans performed</li>
    <li>☐ Incident response plan documented</li>
    <li>☐ Data backup and recovery tested</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion
}
