using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.Commerce;

/// <summary>
/// Content provider for the Optimizely Commerce Connect course
/// </summary>
public class CommerceContentProvider : ILearningContentProvider
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
            BuildCatalogManagementModule(),
            BuildProductManagementModule(),
            BuildPricingInventoryModule(),
            BuildOrdersCheckoutModule(),
            BuildCustomersOrganizationsModule(),
            BuildMarketsLocalizationModule(),
            BuildMarketingPromotionsModule(),
            BuildPaymentsShippingModule(),
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
            Description = "Learn the fundamentals of Optimizely Commerce Connect, understand its architecture, and set up your development environment.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-commerce-connect",
                    ModuleId = "getting-started",
                    Title = "What is Commerce Connect?",
                    Summary = "Discover Optimizely Commerce Connect and its capabilities for building e-commerce solutions.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Commerce Connect is and its purpose",
                        "Learn the key benefits of using Commerce Connect",
                        "Understand how Commerce Connect integrates with Optimizely CMS",
                        "Know when to use Commerce Connect for your projects"
                    },
                    Content = @"
<h2>Introduction to Optimizely Commerce Connect</h2>
<p>Optimizely Commerce Connect is a <strong>powerful digital commerce platform</strong> that combines a robust e-commerce engine with Optimizely CMS, enabling you to create and deploy full-featured e-commerce websites with integrated content management capabilities.</p>

<h3>What is Commerce Connect?</h3>
<p>Commerce Connect (formerly known as EPiServer Commerce) provides a comprehensive e-commerce solution built on top of ASP.NET Core. It integrates seamlessly with Optimizely CMS to deliver unified commerce and content experiences, allowing you to manage products, orders, customers, and marketing campaigns alongside your website content.</p>

<h3>Key Capabilities</h3>
<ul>
    <li><strong>Catalog Management</strong> - Create and manage product catalogs with complex hierarchies of categories, products, and variants</li>
    <li><strong>Order Processing</strong> - Handle shopping carts, checkout workflows, payment processing, and order fulfilment</li>
    <li><strong>Customer Management</strong> - Store customer records, manage organisations, and track purchase history</li>
    <li><strong>Multi-Market Support</strong> - Configure market-specific pricing, currencies, languages, and shipping options</li>
    <li><strong>Marketing & Promotions</strong> - Create discounts, campaigns, and promotional strategies</li>
    <li><strong>Inventory Management</strong> - Track stock levels across multiple warehouses</li>
</ul>

<h3>Benefits of Commerce Connect</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Unified Platform</td><td class=""px-4 py-2"">Manage content and commerce in a single interface</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Flexible Architecture</td><td class=""px-4 py-2"">Extensible system with provider-based integrations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Enterprise Ready</td><td class=""px-4 py-2"">Scalable solution for B2C and B2B scenarios</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Rich APIs</td><td class=""px-4 py-2"">Comprehensive APIs for custom integrations and headless commerce</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Multi-Site Support</td><td class=""px-4 py-2"">Run multiple storefronts from a single installation</td></tr>
    </tbody>
</table>

<h3>Commerce Connect vs Other Solutions</h3>
<p>Commerce Connect differentiates itself by providing deep integration with Optimizely's content management capabilities:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Commerce Connect</th>
            <th class=""px-4 py-2 text-left"">Standalone E-commerce</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Content Integration</td><td class=""px-4 py-2"">Native CMS integration</td><td class=""px-4 py-2"">Separate systems</td></tr>
        <tr><td class=""px-4 py-2"">Personalisation</td><td class=""px-4 py-2"">Built-in with Optimizely</td><td class=""px-4 py-2"">Third-party tools required</td></tr>
        <tr><td class=""px-4 py-2"">Experimentation</td><td class=""px-4 py-2"">A/B testing included</td><td class=""px-4 py-2"">Additional integration needed</td></tr>
        <tr><td class=""px-4 py-2"">Development</td><td class=""px-4 py-2"">.NET Core, familiar patterns</td><td class=""px-4 py-2"">Varies by platform</td></tr>
        <tr><td class=""px-4 py-2"">Deployment</td><td class=""px-4 py-2"">DXP Cloud or self-hosted</td><td class=""px-4 py-2"">Varies by platform</td></tr>
    </tbody>
</table>

<h3>When to Use Commerce Connect</h3>
<ul>
    <li>You need a unified content and commerce experience</li>
    <li>Your team has .NET Core expertise</li>
    <li>You require B2B or B2C e-commerce capabilities</li>
    <li>You want personalisation and experimentation built-in</li>
    <li>You need multi-site or multi-market support</li>
    <li>You're already using or planning to use Optimizely CMS</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-system-architecture",
                    ModuleId = "getting-started",
                    Title = "System Architecture Overview",
                    Summary = "Understand the architecture and core subsystems of Commerce Connect.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the five main subsystems of Commerce Connect",
                        "Learn how the data model relationships work",
                        "Understand how Commerce Connect integrates with CMS",
                        "Know the key architectural components"
                    },
                    Content = @"
<h2>Commerce Connect Architecture</h2>
<p>Optimizely Commerce Connect is built on a modular architecture that organises functionality across <strong>five primary subsystems</strong>, each handling specific aspects of e-commerce operations.</p>

<h3>The Five Core Subsystems</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ol class=""list-decimal list-inside space-y-3"">
        <li><strong>Catalogs</strong> - Manage product schemas, categories, products, variants, and data imports</li>
        <li><strong>Orders</strong> - Process shopping carts, checkout, payments, shipping, and order fulfilment</li>
        <li><strong>Customers</strong> - Manage customer accounts, organisations, roles, and permissions</li>
        <li><strong>Markets</strong> - Configure market-specific settings, currencies, languages, and regions</li>
        <li><strong>Marketing & Campaigns</strong> - Build promotions, discounts, and marketing campaigns</li>
    </ol>
</div>

<h3>Architecture Diagram</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌─────────────────────────────────────────────────────────────────┐
│                    Optimizely Commerce Connect                   │
├─────────────────────────────────────────────────────────────────┤
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐             │
│  │   Catalogs  │  │   Orders    │  │  Customers  │             │
│  │  - Products │  │  - Carts    │  │  - Contacts │             │
│  │  - Variants │  │  - Checkout │  │  - Orgs     │             │
│  │  - Categories│ │  - Payments │  │  - Groups   │             │
│  └─────────────┘  └─────────────┘  └─────────────┘             │
│  ┌─────────────┐  ┌─────────────┐                              │
│  │   Markets   │  │  Marketing  │                              │
│  │  - Regions  │  │  - Promos   │                              │
│  │  - Currencies│ │  - Campaigns│                              │
│  │  - Languages │ │  - Discounts│                              │
│  └─────────────┘  └─────────────┘                              │
├─────────────────────────────────────────────────────────────────┤
│                      Optimizely CMS                              │
│              (Content Management, Rendering, Search)            │
├─────────────────────────────────────────────────────────────────┤
│                      ASP.NET Core                                │
│                (Dependency Injection, MVC, APIs)                │
└─────────────────────────────────────────────────────────────────┘
</pre>

<h3>Data Model Relationships</h3>
<p>Commerce Connect uses a relational data model with two primary relationship types:</p>

<h4>One-to-Many Relationships</h4>
<p>Fixed relationships where one entity connects to multiple others:</p>
<ul>
    <li>Orders contain multiple order lines (line items)</li>
    <li>Catalogs contain multiple categories and products</li>
    <li>Products have multiple variants (SKUs)</li>
    <li>Shipments contain multiple line items</li>
</ul>

<h4>One-to-Many (Optional) Relationships</h4>
<p>Flexible relationships where association isn't required:</p>
<ul>
    <li>Customers may or may not belong to organisations</li>
    <li>Products may or may not have inventory records</li>
    <li>Line items may or may not have discounts applied</li>
</ul>

<h3>Integration with Optimizely CMS</h3>
<p>Commerce Connect content types inherit from CMS content, allowing:</p>
<ul>
    <li><strong>Unified Content Model</strong> - Products, categories, and variants are content items</li>
    <li><strong>Shared Rendering</strong> - Use CMS templates and partial views for commerce content</li>
    <li><strong>Content Search</strong> - Products are indexed in Optimizely Search & Navigation</li>
    <li><strong>Personalisation</strong> - Apply visitor groups and personalisation to commerce content</li>
    <li><strong>Versioning</strong> - Draft and publish product content like any CMS page</li>
</ul>

<h3>Key Namespaces</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Namespace</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">EPiServer.Commerce.Catalog</td><td class=""px-4 py-2"">Catalog, product, and category management</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">EPiServer.Commerce.Order</td><td class=""px-4 py-2"">Order processing and shopping cart</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">EPiServer.Commerce.Customers</td><td class=""px-4 py-2"">Customer and organisation management</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">EPiServer.Commerce.Marketing</td><td class=""px-4 py-2"">Promotions and discounts</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Mediachase.Commerce</td><td class=""px-4 py-2"">Core commerce types and markets</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-development-environment",
                    ModuleId = "getting-started",
                    Title = "Setting Up Your Development Environment",
                    Summary = "Install and configure the tools needed for Commerce Connect development.",
                    Order = 3,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Install the required development tools",
                        "Configure Visual Studio for Commerce Connect",
                        "Set up the Optimizely NuGet feed",
                        "Create your first Commerce Connect project"
                    },
                    Content = @"
<h2>Development Environment Setup</h2>
<p>Before you can start developing with Commerce Connect, you need to set up your development environment with the required tools and configurations.</p>

<h3>Prerequisites</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <p class=""font-medium"">Required Software:</p>
    <ul class=""mt-2 space-y-1"">
        <li>✓ <strong>.NET 8.0 SDK</strong> or later</li>
        <li>✓ <strong>Visual Studio 2022</strong> (or VS Code with C# extension)</li>
        <li>✓ <strong>SQL Server 2019+</strong> (or SQL Server Express / LocalDB)</li>
        <li>✓ <strong>IIS</strong> or <strong>IIS Express</strong></li>
        <li>✓ <strong>Node.js</strong> (for frontend tooling)</li>
    </ul>
</div>

<h3>Step 1: Install Visual Studio</h3>
<p>Download and install Visual Studio 2022 with these workloads:</p>
<ul>
    <li>ASP.NET and web development</li>
    <li>.NET desktop development</li>
    <li>Data storage and processing (for SQL Server tools)</li>
</ul>

<h3>Step 2: Configure the Optimizely NuGet Feed</h3>
<p>Commerce Connect packages are distributed via Optimizely's NuGet feed. Add it to your NuGet configuration:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code># Option 1: Add via Visual Studio
# Tools → NuGet Package Manager → Package Manager Settings
# Package Sources → Add:
# Name: Optimizely
# Source: https://nuget.optimizely.com/feed/packages.svc/

# Option 2: Add via nuget.config file
&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;
&lt;configuration&gt;
  &lt;packageSources&gt;
    &lt;add key=""nuget.org"" value=""https://api.nuget.org/v3/index.json"" /&gt;
    &lt;add key=""Optimizely"" value=""https://nuget.optimizely.com/feed/packages.svc/"" /&gt;
  &lt;/packageSources&gt;
&lt;/configuration&gt;</code></pre>

<h3>Step 3: Create a New Commerce Connect Project</h3>
<p>The easiest way to get started is using the Optimizely templates:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code># Install the Optimizely templates
dotnet new install EPiServer.Templates

# Create a new Commerce Connect project
dotnet new epicommerce -n MyCommerceStore

# Navigate to the project
cd MyCommerceStore

# Restore packages
dotnet restore

# Build the project
dotnet build</code></pre>

<h3>Step 4: Configure the Database</h3>
<p>Update the connection string in <code>appsettings.json</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>{
  ""ConnectionStrings"": {
    ""EPiServerDB"": ""Server=(localdb)\\MSSQLLocalDB;Database=MyCommerceStore;Trusted_Connection=True;MultipleActiveResultSets=True""
  }
}</code></pre>

<h3>Step 5: Run Database Migrations</h3>
<p>Commerce Connect uses Entity Framework migrations for database setup:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code># Create and update the database
dotnet-episerver create-cms-database MyCommerceStore.csproj -S (localdb)\MSSQLLocalDB -E
dotnet-episerver create-commerce-database MyCommerceStore.csproj -S (localdb)\MSSQLLocalDB -E</code></pre>

<h3>Step 6: Create an Admin User</h3>
<p>Run the project and navigate to the setup page to create your admin account:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code># Run the application
dotnet run

# Navigate to: https://localhost:5001/episerver/cms
# Follow the setup wizard to create an admin user</code></pre>

<h3>Project Structure Overview</h3>
<p>A typical Commerce Connect project has this structure:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
MyCommerceStore/
├── Business/               # Business logic and helpers
├── Controllers/            # MVC controllers
├── Features/              # Feature-based organisation
│   ├── Cart/              # Shopping cart functionality
│   ├── Checkout/          # Checkout process
│   ├── Product/           # Product pages
│   └── Search/            # Search functionality
├── Infrastructure/        # DI and startup configuration
├── Models/                # View models and DTOs
│   ├── Catalog/           # Product content types
│   └── Pages/             # CMS page types
├── Views/                 # Razor views
├── wwwroot/               # Static files
├── appsettings.json       # Configuration
└── Program.cs             # Application entry point
</pre>

<h3>Verify Your Installation</h3>
<p>Once running, verify access to:</p>
<ul>
    <li><strong>CMS Edit Mode</strong>: <code>/episerver/cms</code></li>
    <li><strong>Commerce Manager</strong>: <code>/episerver/commerce</code></li>
    <li><strong>Admin Mode</strong>: <code>/episerver/admin</code></li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "NuGet Configuration",
                            Description = "Complete nuget.config for Commerce Connect development",
                            ExampleContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
  <packageSources>
    <clear />
    <add key=""nuget.org"" value=""https://api.nuget.org/v3/index.json"" />
    <add key=""Optimizely"" value=""https://nuget.optimizely.com/feed/packages.svc/"" />
  </packageSources>
</configuration>",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-key-concepts",
                    ModuleId = "getting-started",
                    Title = "Key Commerce Concepts",
                    Summary = "Learn the fundamental concepts: catalogs, entries, SKUs, orders, and customers.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the catalog hierarchy (catalog → category → product → variant)",
                        "Learn the difference between products, variants, and SKUs",
                        "Understand the order lifecycle",
                        "Know the customer data model"
                    },
                    Content = @"
<h2>Fundamental Commerce Concepts</h2>
<p>Before diving into implementation, it's essential to understand the core concepts that form the foundation of Commerce Connect.</p>

<h3>Catalog Hierarchy</h3>
<p>Commerce Connect organises products using a hierarchical structure:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Catalog (Root Container)
└── Category (Node)
    ├── Category (Nested Node)
    │   ├── Product
    │   │   ├── Variant (SKU)
    │   │   └── Variant (SKU)
    │   └── Product
    │       └── Variant (SKU)
    └── Product
        ├── Variant (SKU)
        └── Variant (SKU)
</pre>

<h4>Catalog Entry Types</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Catalog</td><td class=""px-4 py-2"">Top-level container for all entries</td><td class=""px-4 py-2"">""Fashion Catalog"", ""Electronics Store""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category (Node)</td><td class=""px-4 py-2"">Groups products logically</td><td class=""px-4 py-2"">""Men's Clothing"", ""Laptops""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product</td><td class=""px-4 py-2"">Displayable merchandise</td><td class=""px-4 py-2"">""Oxford Shirt"", ""MacBook Pro""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Variant (SKU)</td><td class=""px-4 py-2"">Purchasable unit with price</td><td class=""px-4 py-2"">""Blue Oxford Shirt - Large""</td></tr>
    </tbody>
</table>

<h3>Products vs Variants vs SKUs</h3>
<p>Understanding the distinction between these terms is crucial:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4>Fashion Industry Example</h4>
    <ul class=""space-y-2"">
        <li><strong>Product</strong>: A sweatshirt style (the displayable item with description, images)</li>
        <li><strong>Variant</strong>: A specific colour of that sweatshirt (inherits from product)</li>
        <li><strong>SKU</strong>: The combination of size + colour (the sellable unit with a specific price)</li>
    </ul>
</div>

<p>Key points:</p>
<ul>
    <li><strong>Enrichment</strong> is primarily done at the product level (descriptions, marketing content)</li>
    <li><strong>Images and colour</strong> are typically added at the variant level</li>
    <li><strong>Price and inventory</strong> are tracked at the SKU level</li>
    <li>The <strong>SKU is the sellable unit</strong> that gets added to the cart</li>
</ul>

<h3>Order Lifecycle</h3>
<p>Orders progress through several states:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Shopping Cart → Checkout → Payment → Order Placed → Processing → Shipped → Completed
     │              │          │           │            │           │
     ▼              ▼          ▼           ▼            ▼           ▼
  ICart        Validation  IPurchaseOrder  Inventory  Shipment   Return?
  ILineItem    Address     IPayment        Update     Status     IReturnOrderForm
  IShipment    Shipping    Transaction                Tracking
</pre>

<h4>Order Components</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">ICart</td><td class=""px-4 py-2"">Shopping cart before purchase</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">IOrderForm</td><td class=""px-4 py-2"">Container for shipments and payments</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">ILineItem</td><td class=""px-4 py-2"">Individual item in the cart/order</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">IShipment</td><td class=""px-4 py-2"">Group of items being shipped together</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">IPayment</td><td class=""px-4 py-2"">Payment information and status</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">IPurchaseOrder</td><td class=""px-4 py-2"">Completed order after checkout</td></tr>
    </tbody>
</table>

<h3>Customer Data Model</h3>
<p>Commerce Connect uses a flexible customer model:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
CustomerContact (Individual Customer)
├── Addresses (Shipping/Billing)
├── Customer Groups (Price Groups)
├── Order History
└── Organization (Optional - for B2B)
    ├── Child Organizations
    ├── Members
    └── Credit Limits
</pre>

<h4>Customer Concepts</h4>
<ul>
    <li><strong>CustomerContact</strong> - Represents an individual customer or user</li>
    <li><strong>Organization</strong> - For B2B scenarios, groups customers together</li>
    <li><strong>Customer Groups</strong> - Used for price differentiation and permissions</li>
    <li><strong>Addresses</strong> - Reusable shipping and billing addresses</li>
</ul>

<h3>Markets</h3>
<p>Markets allow you to configure region-specific settings:</p>
<ul>
    <li><strong>Currencies</strong> - Supported payment currencies</li>
    <li><strong>Languages</strong> - Content languages for the market</li>
    <li><strong>Countries</strong> - Shipping destinations</li>
    <li><strong>Payment Methods</strong> - Available payment options</li>
    <li><strong>Shipping Methods</strong> - Available shipping options</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-commerce-manager",
                    ModuleId = "getting-started",
                    Title = "Navigating Commerce Manager",
                    Summary = "Learn to use Commerce Manager for catalog, order, and customer administration.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the Commerce Manager interface",
                        "Understand the different administration areas",
                        "Learn to manage catalogs, orders, and customers",
                        "Configure marketing campaigns and promotions"
                    },
                    Content = @"
<h2>Commerce Manager Interface</h2>
<p>Commerce Manager is the back-office administration interface for managing your e-commerce operations. Access it at <code>/episerver/commerce</code>.</p>

<h3>Main Navigation Areas</h3>
<p>Commerce Manager is divided into several key sections:</p>

<div class=""grid grid-cols-1 md:grid-cols-2 gap-4 my-4"">
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold text-lg mb-2"">📦 Catalog Management</h4>
        <ul class=""space-y-1 text-sm"">
            <li>Create and edit catalogs</li>
            <li>Manage categories and products</li>
            <li>Configure product variants</li>
            <li>Import/export catalog data</li>
            <li>Manage assets and media</li>
        </ul>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold text-lg mb-2"">🛒 Order Management</h4>
        <ul class=""space-y-1 text-sm"">
            <li>View and search orders</li>
            <li>Process order fulfilment</li>
            <li>Handle returns and refunds</li>
            <li>Manage shipments</li>
            <li>View payment transactions</li>
        </ul>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold text-lg mb-2"">👥 Customer Management</h4>
        <ul class=""space-y-1 text-sm"">
            <li>View customer accounts</li>
            <li>Manage organisations (B2B)</li>
            <li>Configure customer groups</li>
            <li>View purchase history</li>
            <li>Manage addresses</li>
        </ul>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold text-lg mb-2"">📣 Marketing</h4>
        <ul class=""space-y-1 text-sm"">
            <li>Create promotions</li>
            <li>Configure discounts</li>
            <li>Manage campaigns</li>
            <li>Set up coupon codes</li>
            <li>View promotion reports</li>
        </ul>
    </div>
</div>

<h3>Catalog Management Tasks</h3>
<h4>Creating a New Catalog</h4>
<ol class=""list-decimal list-inside space-y-2 my-4"">
    <li>Navigate to <strong>Catalog Management</strong></li>
    <li>Click <strong>Create New Catalog</strong></li>
    <li>Enter catalog name and configure settings</li>
    <li>Set default language and currency</li>
    <li>Configure meta classes for custom properties</li>
    <li>Save the catalog</li>
</ol>

<h4>Adding Products</h4>
<ol class=""list-decimal list-inside space-y-2 my-4"">
    <li>Select the target category in the catalog tree</li>
    <li>Click <strong>New Entry</strong> → <strong>Product</strong></li>
    <li>Fill in product details (name, code, description)</li>
    <li>Add images and assets</li>
    <li>Create variants with pricing</li>
    <li>Configure inventory and availability</li>
    <li>Publish the product</li>
</ol>

<h3>Order Management Tasks</h3>
<h4>Processing an Order</h4>
<ol class=""list-decimal list-inside space-y-2 my-4"">
    <li>Navigate to <strong>Order Management</strong></li>
    <li>Search or filter to find the order</li>
    <li>Review order details and payment status</li>
    <li>Create shipment(s) for line items</li>
    <li>Update tracking information</li>
    <li>Complete the shipment</li>
</ol>

<h3>Administration Settings</h3>
<p>Key configuration areas in Commerce Manager:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Location</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Markets</td><td class=""px-4 py-2"">Administration → Markets</td><td class=""px-4 py-2"">Configure regional settings</td></tr>
        <tr><td class=""px-4 py-2"">Warehouses</td><td class=""px-4 py-2"">Administration → Warehouses</td><td class=""px-4 py-2"">Set up inventory locations</td></tr>
        <tr><td class=""px-4 py-2"">Payment Methods</td><td class=""px-4 py-2"">Administration → Payments</td><td class=""px-4 py-2"">Configure payment options</td></tr>
        <tr><td class=""px-4 py-2"">Shipping Methods</td><td class=""px-4 py-2"">Administration → Shipping</td><td class=""px-4 py-2"">Set up shipping options</td></tr>
        <tr><td class=""px-4 py-2"">Taxes</td><td class=""px-4 py-2"">Administration → Taxes</td><td class=""px-4 py-2"">Configure tax jurisdictions</td></tr>
    </tbody>
</table>

<h3>Tips for Effective Use</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li><strong>Use filters and search</strong> - Large catalogs benefit from filtering and search functionality</li>
        <li><strong>Bulk operations</strong> - Use import/export for large-scale changes</li>
        <li><strong>Draft mode</strong> - Products can be edited in draft before publishing</li>
        <li><strong>Keyboard shortcuts</strong> - Learn shortcuts for common operations</li>
        <li><strong>Scheduled publishing</strong> - Plan product launches with scheduled publish dates</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Catalog Management

    private LearningModule BuildCatalogManagementModule()
    {
        return new LearningModule
        {
            Id = "catalog-management",
            Title = "Catalog Management",
            Description = "Master catalog structure, content types, categories, and how to organize your product data effectively.",
            Icon = "rectangle-stack",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cm-catalog-structure",
                    ModuleId = "catalog-management",
                    Title = "Understanding Catalog Structure",
                    Summary = "Learn how catalogs are organized with categories, products, and variants.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the hierarchical catalog structure",
                        "Learn the purpose of catalogs, nodes, and entries",
                        "Understand how products relate to variants",
                        "Know the role of primary and secondary categorizations"
                    },
                    Content = @"
<h2>Catalog Structure in Commerce Connect</h2>
<p>Commerce Connect uses a hierarchical model to organize product data. Understanding this structure is fundamental to building effective e-commerce solutions.</p>

<h3>The Catalog Hierarchy</h3>
<p>Every e-commerce site has at least one catalog, which serves as the root container for all your products:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
📁 Catalog (CatalogContent)
├── 📂 Category/Node (NodeContent)
│   ├── 📂 Subcategory (NodeContent)
│   │   ├── 📦 Product (ProductContent)
│   │   │   ├── 🏷️ Variant (VariationContent)
│   │   │   └── 🏷️ Variant (VariationContent)
│   │   └── 📦 Product (ProductContent)
│   └── 📦 Product (ProductContent)
└── 📂 Category (NodeContent)
    └── 📦 Product (ProductContent)
</pre>

<h3>Catalog Components</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Base Class</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Catalog</td><td class=""px-4 py-2 font-mono text-sm"">CatalogContent</td><td class=""px-4 py-2"">Root container, defines languages and default settings</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Category (Node)</td><td class=""px-4 py-2 font-mono text-sm"">NodeContent</td><td class=""px-4 py-2"">Organizes products into navigable groups</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Product</td><td class=""px-4 py-2 font-mono text-sm"">ProductContent</td><td class=""px-4 py-2"">Displayable item with marketing content</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Variant/SKU</td><td class=""px-4 py-2 font-mono text-sm"">VariationContent</td><td class=""px-4 py-2"">Purchasable item with price and inventory</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Bundle</td><td class=""px-4 py-2 font-mono text-sm"">BundleContent</td><td class=""px-4 py-2"">Collection of products sold together</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Package</td><td class=""px-4 py-2 font-mono text-sm"">PackageContent</td><td class=""px-4 py-2"">Group of variants with combined pricing</td></tr>
    </tbody>
</table>

<h3>Multi-Level Product Hierarchies</h3>
<p>Commerce Connect supports complex product structures where products can have child products:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-bold"">Fashion Example</h4>
    <pre class=""text-sm mt-2"">
📦 ""Classic Oxford Shirt"" (Product - Style)
├── 📦 ""Blue Oxford"" (Product - Colour Variant)
│   ├── 🏷️ ""Blue Oxford - S"" (SKU)
│   ├── 🏷️ ""Blue Oxford - M"" (SKU)
│   └── 🏷️ ""Blue Oxford - L"" (SKU)
└── 📦 ""White Oxford"" (Product - Colour Variant)
    ├── 🏷️ ""White Oxford - S"" (SKU)
    ├── 🏷️ ""White Oxford - M"" (SKU)
    └── 🏷️ ""White Oxford - L"" (SKU)
    </pre>
</div>

<h3>Where to Store Data</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Data Type</th>
            <th class=""px-4 py-2 text-left"">Level</th>
            <th class=""px-4 py-2 text-left"">Rationale</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Marketing description</td><td class=""px-4 py-2"">Product</td><td class=""px-4 py-2"">Shared across all variants</td></tr>
        <tr><td class=""px-4 py-2"">Brand, category info</td><td class=""px-4 py-2"">Product</td><td class=""px-4 py-2"">Common to all variants</td></tr>
        <tr><td class=""px-4 py-2"">Colour images</td><td class=""px-4 py-2"">Variant (colour level)</td><td class=""px-4 py-2"">Specific to colour option</td></tr>
        <tr><td class=""px-4 py-2"">Price</td><td class=""px-4 py-2"">SKU</td><td class=""px-4 py-2"">Each size/colour combo has own price</td></tr>
        <tr><td class=""px-4 py-2"">Inventory</td><td class=""px-4 py-2"">SKU</td><td class=""px-4 py-2"">Tracked per sellable unit</td></tr>
    </tbody>
</table>

<h3>Categorization Types</h3>
<h4>Primary Categorization</h4>
<p>Each product has one <strong>primary categorization</strong> that determines:</p>
<ul>
    <li>The product's default URL structure</li>
    <li>The ""home"" category in navigation</li>
    <li>Breadcrumb generation</li>
</ul>

<h4>Secondary Categorizations</h4>
<p>Products can appear in <strong>multiple categories</strong> without duplicating data:</p>
<ul>
    <li>A ""Blue Dress"" might appear in ""Dresses"" and ""Blue Items""</li>
    <li>A ""Sale Item"" might appear in its normal category and ""Sale""</li>
    <li>Cross-sell categories like ""New Arrivals"" or ""Best Sellers""</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Product with Variants",
                            Description = "Example product structure with colour and size variants",
                            ExampleContent = @"// Product hierarchy example
public class ShirtProduct : ProductContent
{
    [Display(Name = ""Brand"")]
    public virtual string Brand { get; set; }

    [Display(Name = ""Material"")]
    public virtual string Material { get; set; }

    [Display(Name = ""Care Instructions"")]
    public virtual XhtmlString CareInstructions { get; set; }
}

public class ShirtVariant : VariationContent
{
    [Display(Name = ""Colour"")]
    public virtual string Colour { get; set; }

    [Display(Name = ""Size"")]
    public virtual string Size { get; set; }

    [Display(Name = ""Colour Image"")]
    public virtual ContentReference ColourImage { get; set; }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-catalog-content-types",
                    ModuleId = "catalog-management",
                    Title = "Creating Catalog Content Types",
                    Summary = "Learn to define custom product, variant, and category content types.",
                    Order = 2,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Create custom product content types",
                        "Define variant content types with attributes",
                        "Create category content types",
                        "Use appropriate property types for commerce content"
                    },
                    Content = @"
<h2>Defining Catalog Content Types</h2>
<p>Like CMS pages, commerce content types are defined as C# classes that inherit from base commerce types. This gives you full control over the properties and behaviour of your products.</p>

<h3>Base Content Types</h3>
<p>Commerce Connect provides these base classes for catalog content:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Base Class</th>
            <th class=""px-4 py-2 text-left"">Use For</th>
            <th class=""px-4 py-2 text-left"">Key Features</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">ProductContent</td><td class=""px-4 py-2"">Displayable products</td><td class=""px-4 py-2"">Can have child variants, SEO properties</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">VariationContent</td><td class=""px-4 py-2"">Purchasable SKUs</td><td class=""px-4 py-2"">Price, inventory, can be added to cart</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">NodeContent</td><td class=""px-4 py-2"">Categories</td><td class=""px-4 py-2"">Can have child nodes and entries</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">BundleContent</td><td class=""px-4 py-2"">Product bundles</td><td class=""px-4 py-2"">Contains multiple entries sold together</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">PackageContent</td><td class=""px-4 py-2"">Variant packages</td><td class=""px-4 py-2"">Group of variants with combined pricing</td></tr>
    </tbody>
</table>

<h3>Creating a Product Content Type</h3>
<p>Here's how to create a custom product type:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Models.Catalog
{
    [CatalogContentType(
        GUID = ""5A6B7C8D-9E0F-1A2B-3C4D-5E6F7A8B9C0D"",
        DisplayName = ""Fashion Product"",
        Description = ""Product type for fashion items"")]
    public class FashionProduct : ProductContent
    {
        [Display(Name = ""Brand"", Order = 10)]
        public virtual string Brand { get; set; }

        [Display(Name = ""Description"", Order = 20)]
        public virtual XhtmlString LongDescription { get; set; }

        [Display(Name = ""Main Image"", Order = 30)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MainImage { get; set; }

        [Display(Name = ""Size Guide"", Order = 40)]
        public virtual ContentArea SizeGuide { get; set; }

        [Display(Name = ""Available Colours"", Order = 50)]
        [SelectMany(SelectionFactoryType = typeof(ColourSelectionFactory))]
        public virtual IList&lt;string&gt; AvailableColours { get; set; }
    }
}</code></pre>

<h3>Creating a Variant Content Type</h3>
<p>Variants represent the purchasable units:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[CatalogContentType(
    GUID = ""6B7C8D9E-0F1A-2B3C-4D5E-6F7A8B9C0D1E"",
    DisplayName = ""Fashion Variant"",
    Description = ""Variant type for fashion items"")]
public class FashionVariant : VariationContent
{
    [Display(Name = ""Colour"", Order = 10)]
    [Required]
    public virtual string Colour { get; set; }

    [Display(Name = ""Size"", Order = 20)]
    [Required]
    [SelectOne(SelectionFactoryType = typeof(SizeSelectionFactory))]
    public virtual string Size { get; set; }

    [Display(Name = ""Colour Swatch"", Order = 30)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference ColourSwatch { get; set; }

    [Display(Name = ""Weight (kg)"", Order = 40)]
    public virtual decimal Weight { get; set; }
}</code></pre>

<h3>Creating a Category Content Type</h3>
<p>Categories (nodes) organize your products:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[CatalogContentType(
    GUID = ""7C8D9E0F-1A2B-3C4D-5E6F-7A8B9C0D1E2F"",
    DisplayName = ""Fashion Category"",
    Description = ""Category for fashion products"")]
public class FashionCategory : NodeContent
{
    [Display(Name = ""Category Image"", Order = 10)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference CategoryImage { get; set; }

    [Display(Name = ""Description"", Order = 20)]
    public virtual XhtmlString Description { get; set; }

    [Display(Name = ""Featured Products"", Order = 30)]
    [AllowedTypes(typeof(FashionProduct))]
    public virtual ContentArea FeaturedProducts { get; set; }

    [Display(Name = ""Show in Navigation"", Order = 40)]
    public virtual bool ShowInNavigation { get; set; } = true;
}</code></pre>

<h3>Important Attributes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">[CatalogContentType]</td><td class=""px-4 py-2"">Identifies class as commerce content type</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">[AvailableContentTypes]</td><td class=""px-4 py-2"">Restricts which child types are allowed</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">[UIHint(UIHint.Image)]</td><td class=""px-4 py-2"">Shows image picker in editor</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">[SelectOne] / [SelectMany]</td><td class=""px-4 py-2"">Dropdown or multi-select from selection factory</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">[AllowedTypes]</td><td class=""px-4 py-2"">Restricts content area to specific types</td></tr>
    </tbody>
</table>

<h3>Restricting Child Types</h3>
<p>Control which variants can be created under a product:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[CatalogContentType(GUID = ""..."")]
[AvailableContentTypes(
    Availability.Specific,
    Include = new[] { typeof(FashionVariant) })]
public class FashionProduct : ProductContent
{
    // Only FashionVariant can be created under this product
}</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Complete Product Type",
                            Description = "Full example of a product content type with all common properties",
                            ExampleContent = @"using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Models.Catalog
{
    [CatalogContentType(
        GUID = ""12345678-1234-1234-1234-123456789012"",
        DisplayName = ""Standard Product"",
        Description = ""Standard product with common e-commerce properties"")]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(StandardVariant) })]
    public class StandardProduct : ProductContent
    {
        #region Product Information

        [Display(Name = ""Brand"", GroupName = ""Product Info"", Order = 10)]
        public virtual string Brand { get; set; }

        [Display(Name = ""Short Description"", GroupName = ""Product Info"", Order = 20)]
        [StringLength(200)]
        public virtual string ShortDescription { get; set; }

        [Display(Name = ""Full Description"", GroupName = ""Product Info"", Order = 30)]
        public virtual XhtmlString FullDescription { get; set; }

        #endregion

        #region Media

        [Display(Name = ""Main Image"", GroupName = ""Media"", Order = 10)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MainImage { get; set; }

        [Display(Name = ""Gallery Images"", GroupName = ""Media"", Order = 20)]
        public virtual ContentArea GalleryImages { get; set; }

        [Display(Name = ""Product Video"", GroupName = ""Media"", Order = 30)]
        [UIHint(UIHint.Video)]
        public virtual ContentReference ProductVideo { get; set; }

        #endregion

        #region SEO

        [Display(Name = ""Meta Title"", GroupName = ""SEO"", Order = 10)]
        [StringLength(60)]
        public virtual string MetaTitle { get; set; }

        [Display(Name = ""Meta Description"", GroupName = ""SEO"", Order = 20)]
        [StringLength(160)]
        public virtual string MetaDescription { get; set; }

        #endregion
    }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-working-with-catalogs",
                    ModuleId = "catalog-management",
                    Title = "Working with Catalogs Programmatically",
                    Summary = "Learn to create, query, and manage catalog content using the Commerce APIs.",
                    Order = 3,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Use IContentRepository to work with catalog content",
                        "Query products and variants using the API",
                        "Create and update catalog entries programmatically",
                        "Understand the ReferenceConverter for catalog references"
                    },
                    Content = @"
<h2>Working with Catalogs Programmatically</h2>
<p>Commerce Connect provides several services for working with catalog content. Understanding these APIs is essential for building custom commerce functionality.</p>

<h3>Key Services</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Service</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">IContentRepository</td><td class=""px-4 py-2"">Standard CMS content operations (Get, Save, Delete)</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IContentLoader</td><td class=""px-4 py-2"">Read-only content loading (faster for queries)</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ReferenceConverter</td><td class=""px-4 py-2"">Convert between codes and ContentReferences</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IRelationRepository</td><td class=""px-4 py-2"">Manage product-variant and categorization relations</td></tr>
    </tbody>
</table>

<h3>Loading Catalog Content</h3>
<p>Load products and variants using <code>IContentLoader</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class ProductService
{
    private readonly IContentLoader _contentLoader;
    private readonly ReferenceConverter _referenceConverter;

    public ProductService(
        IContentLoader contentLoader,
        ReferenceConverter referenceConverter)
    {
        _contentLoader = contentLoader;
        _referenceConverter = referenceConverter;
    }

    // Load by ContentReference
    public ProductContent GetProduct(ContentReference contentLink)
    {
        return _contentLoader.Get&lt;ProductContent&gt;(contentLink);
    }

    // Load by product code
    public ProductContent GetProductByCode(string code)
    {
        var contentLink = _referenceConverter.GetContentLink(code);
        return _contentLoader.Get&lt;ProductContent&gt;(contentLink);
    }

    // Get all variants for a product
    public IEnumerable&lt;VariationContent&gt; GetVariants(ProductContent product)
    {
        return _contentLoader
            .GetChildren&lt;VariationContent&gt;(product.ContentLink);
    }
}</code></pre>

<h3>The ReferenceConverter</h3>
<p>Commerce Connect uses codes to identify catalog entries. The <code>ReferenceConverter</code> bridges between codes and <code>ContentReference</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Get ContentReference from code
ContentReference productRef = _referenceConverter.GetContentLink(""PRODUCT-123"");

// Get code from ContentReference
string code = _referenceConverter.GetCode(productRef);

// Check if code exists
bool exists = _referenceConverter.GetContentLink(""PRODUCT-123"") != ContentReference.EmptyReference;</code></pre>

<h3>Querying Categories</h3>
<p>Navigate the category tree:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Get root catalog
var catalogs = _contentLoader.GetChildren&lt;CatalogContent&gt;(
    _referenceConverter.GetRootLink());

// Get top-level categories in a catalog
var topCategories = _contentLoader.GetChildren&lt;NodeContent&gt;(
    catalog.ContentLink);

// Get subcategories
var subCategories = _contentLoader.GetChildren&lt;NodeContent&gt;(
    parentCategory.ContentLink);

// Get products in a category
var products = _contentLoader.GetChildren&lt;ProductContent&gt;(
    category.ContentLink);</code></pre>

<h3>Creating Catalog Content</h3>
<p>Create new products and variants programmatically:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CatalogCreationService
{
    private readonly IContentRepository _contentRepository;
    private readonly ReferenceConverter _referenceConverter;

    public ContentReference CreateProduct(
        ContentReference parentCategory,
        string code,
        string name)
    {
        // Get writeable clone or create new
        var product = _contentRepository
            .GetDefault&lt;FashionProduct&gt;(parentCategory);

        product.Code = code;
        product.Name = name;
        product.DisplayName = name;

        // Save and publish
        return _contentRepository.Save(
            product,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }

    public ContentReference CreateVariant(
        ContentReference parentProduct,
        string code,
        string colour,
        string size)
    {
        var variant = _contentRepository
            .GetDefault&lt;FashionVariant&gt;(parentProduct);

        variant.Code = code;
        variant.Name = $""{colour} - {size}"";
        variant.Colour = colour;
        variant.Size = size;

        return _contentRepository.Save(
            variant,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }
}</code></pre>

<h3>Working with Relations</h3>
<p>Use <code>IRelationRepository</code> for product-variant and categorization relations:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>private readonly IRelationRepository _relationRepository;

// Get all variants linked to a product
var variants = _relationRepository
    .GetChildren&lt;ProductVariation&gt;(product.ContentLink)
    .Select(r => r.Child);

// Get parent products for a variant
var parents = _relationRepository
    .GetParents&lt;ProductVariation&gt;(variant.ContentLink)
    .Select(r => r.Parent);

// Get categories for an entry
var categories = _relationRepository
    .GetParents&lt;NodeEntryRelation&gt;(entry.ContentLink)
    .Select(r => r.Parent);</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Product Service Example",
                            Description = "Complete service for working with products",
                            ExampleContent = @"using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;

public class ProductService
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentRepository _contentRepository;
    private readonly ReferenceConverter _referenceConverter;
    private readonly IRelationRepository _relationRepository;

    public ProductService(
        IContentLoader contentLoader,
        IContentRepository contentRepository,
        ReferenceConverter referenceConverter,
        IRelationRepository relationRepository)
    {
        _contentLoader = contentLoader;
        _contentRepository = contentRepository;
        _referenceConverter = referenceConverter;
        _relationRepository = relationRepository;
    }

    public T GetEntryByCode<T>(string code) where T : EntryContentBase
    {
        var contentLink = _referenceConverter.GetContentLink(code);
        if (contentLink == ContentReference.EmptyReference)
            return null;

        return _contentLoader.Get<T>(contentLink);
    }

    public IEnumerable<VariationContent> GetVariantsForProduct(
        ProductContent product)
    {
        return _contentLoader
            .GetChildren<VariationContent>(product.ContentLink)
            .Where(v => v.IsAvailableInCurrentMarket());
    }

    public IEnumerable<NodeContent> GetCategoriesForEntry(
        EntryContentBase entry)
    {
        return _relationRepository
            .GetParents<NodeEntryRelation>(entry.ContentLink)
            .Select(r => _contentLoader.Get<NodeContent>(r.Parent));
    }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-categorizations",
                    ModuleId = "catalog-management",
                    Title = "Managing Categorizations",
                    Summary = "Learn to assign products to categories and manage primary/secondary categorizations.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand primary vs secondary categorizations",
                        "Add and remove category assignments",
                        "Set the primary category for an entry",
                        "Query products by category"
                    },
                    Content = @"
<h2>Managing Categorizations</h2>
<p>Categorizations define how products are organized within the catalog tree. Commerce Connect supports both primary and secondary categorizations, allowing products to appear in multiple categories.</p>

<h3>Primary vs Secondary Categories</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Limit</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Primary</td><td class=""px-4 py-2"">Defines the canonical URL and breadcrumb path</td><td class=""px-4 py-2"">One per entry</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Secondary</td><td class=""px-4 py-2"">Additional category appearances (cross-sell, promotions)</td><td class=""px-4 py-2"">Unlimited</td></tr>
    </tbody>
</table>

<h3>The NodeEntryRelation Class</h3>
<p>Categorizations are represented by <code>NodeEntryRelation</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class NodeEntryRelation : NodeRelation
{
    public ContentReference Parent { get; set; }  // Category
    public ContentReference Child { get; set; }   // Entry
    public int SortOrder { get; set; }            // Display order
    public bool IsPrimary { get; set; }           // Primary flag
}</code></pre>

<h3>Adding Category Assignments</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CategorizationService
{
    private readonly IRelationRepository _relationRepository;

    public CategorizationService(IRelationRepository relationRepository)
    {
        _relationRepository = relationRepository;
    }

    // Add entry to a category
    public void AddToCategory(
        ContentReference entry,
        ContentReference category,
        bool isPrimary = false,
        int sortOrder = 0)
    {
        var relation = new NodeEntryRelation
        {
            Parent = category,
            Child = entry,
            IsPrimary = isPrimary,
            SortOrder = sortOrder
        };

        _relationRepository.UpdateRelation(relation);
    }

    // Remove from category
    public void RemoveFromCategory(
        ContentReference entry,
        ContentReference category)
    {
        var relation = new NodeEntryRelation
        {
            Parent = category,
            Child = entry
        };

        _relationRepository.RemoveRelation(relation);
    }
}</code></pre>

<h3>Setting the Primary Category</h3>
<p>When changing the primary category, you need to update the <code>IsPrimary</code> flag:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public void SetPrimaryCategory(
    ContentReference entry,
    ContentReference newPrimaryCategory)
{
    // Get all current categorizations
    var relations = _relationRepository
        .GetParents&lt;NodeEntryRelation&gt;(entry)
        .ToList();

    foreach (var relation in relations)
    {
        // Update the relation
        var updatedRelation = new NodeEntryRelation
        {
            Parent = relation.Parent,
            Child = relation.Child,
            SortOrder = relation.SortOrder,
            IsPrimary = relation.Parent == newPrimaryCategory
        };

        _relationRepository.UpdateRelation(updatedRelation);
    }

    // If new category isn't already assigned, add it
    if (!relations.Any(r => r.Parent == newPrimaryCategory))
    {
        AddToCategory(entry, newPrimaryCategory, isPrimary: true);
    }
}</code></pre>

<h3>Getting Products by Category</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Get all entries in a category (children)
var entries = _relationRepository
    .GetChildren&lt;NodeEntryRelation&gt;(categoryRef)
    .OrderBy(r => r.SortOrder)
    .Select(r => _contentLoader.Get&lt;EntryContentBase&gt;(r.Child));

// Get categories for an entry (parents)
var categories = _relationRepository
    .GetParents&lt;NodeEntryRelation&gt;(entryRef)
    .Select(r => new
    {
        Category = _contentLoader.Get&lt;NodeContent&gt;(r.Parent),
        IsPrimary = r.IsPrimary
    });

// Get primary category only
var primaryRelation = _relationRepository
    .GetParents&lt;NodeEntryRelation&gt;(entryRef)
    .FirstOrDefault(r => r.IsPrimary);

var primaryCategory = primaryRelation != null
    ? _contentLoader.Get&lt;NodeContent&gt;(primaryRelation.Parent)
    : null;</code></pre>

<h3>Practical Examples</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-bold"">Use Case: Product in Multiple Categories</h4>
    <p>A ""Red Summer Dress"" might appear in:</p>
    <ul class=""mt-2 space-y-1"">
        <li><strong>Primary:</strong> Women → Dresses → Summer Dresses</li>
        <li><strong>Secondary:</strong> Women → New Arrivals</li>
        <li><strong>Secondary:</strong> Women → Sale Items</li>
        <li><strong>Secondary:</strong> Colour Collections → Red</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Categorization Service",
                            Description = "Complete service for managing product categorizations",
                            ExampleContent = @"using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;
using EPiServer;

public class CategorizationService
{
    private readonly IRelationRepository _relationRepository;
    private readonly IContentLoader _contentLoader;

    public CategorizationService(
        IRelationRepository relationRepository,
        IContentLoader contentLoader)
    {
        _relationRepository = relationRepository;
        _contentLoader = contentLoader;
    }

    public void AddToCategory(
        ContentReference entry,
        ContentReference category,
        bool isPrimary = false)
    {
        var relation = new NodeEntryRelation
        {
            Parent = category,
            Child = entry,
            IsPrimary = isPrimary,
            SortOrder = GetNextSortOrder(category)
        };

        _relationRepository.UpdateRelation(relation);
    }

    public void RemoveFromCategory(
        ContentReference entry,
        ContentReference category)
    {
        var relation = new NodeEntryRelation
        {
            Parent = category,
            Child = entry
        };

        _relationRepository.RemoveRelation(relation);
    }

    public NodeContent GetPrimaryCategory(ContentReference entry)
    {
        var primaryRelation = _relationRepository
            .GetParents<NodeEntryRelation>(entry)
            .FirstOrDefault(r => r.IsPrimary);

        return primaryRelation != null
            ? _contentLoader.Get<NodeContent>(primaryRelation.Parent)
            : null;
    }

    public IEnumerable<NodeContent> GetAllCategories(ContentReference entry)
    {
        return _relationRepository
            .GetParents<NodeEntryRelation>(entry)
            .Select(r => _contentLoader.Get<NodeContent>(r.Parent));
    }

    private int GetNextSortOrder(ContentReference category)
    {
        var maxOrder = _relationRepository
            .GetChildren<NodeEntryRelation>(category)
            .Max(r => (int?)r.SortOrder) ?? 0;

        return maxOrder + 10;
    }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-import-export",
                    ModuleId = "catalog-management",
                    Title = "Catalog Import and Export",
                    Summary = "Learn to import and export catalog data for bulk operations.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand catalog import/export formats",
                        "Use Commerce Manager for bulk imports",
                        "Build custom import solutions",
                        "Handle import validation and errors"
                    },
                    Content = @"
<h2>Catalog Import and Export</h2>
<p>For large catalogs or integration with external systems (PIM, ERP), you'll need to import and export catalog data. Commerce Connect supports several approaches.</p>

<h3>Import Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
            <th class=""px-4 py-2 text-left"">Format</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Commerce Manager UI</td><td class=""px-4 py-2"">Manual imports, small catalogs</td><td class=""px-4 py-2"">XML, CSV</td></tr>
        <tr><td class=""px-4 py-2"">Catalog Import API</td><td class=""px-4 py-2"">Automated imports, CI/CD</td><td class=""px-4 py-2"">XML</td></tr>
        <tr><td class=""px-4 py-2"">Custom Import Job</td><td class=""px-4 py-2"">Complex transformations, external systems</td><td class=""px-4 py-2"">Any</td></tr>
        <tr><td class=""px-4 py-2"">Service API</td><td class=""px-4 py-2"">Real-time updates, integrations</td><td class=""px-4 py-2"">JSON</td></tr>
    </tbody>
</table>

<h3>XML Catalog Format</h3>
<p>The standard Commerce Connect import format:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;
&lt;Catalog name=""Fashion"" startDate=""2024-01-01"" endDate=""2099-12-31""&gt;
  &lt;Nodes&gt;
    &lt;Node code=""mens-clothing"" name=""Men's Clothing""&gt;
      &lt;SeoInfo&gt;
        &lt;Title&gt;Men's Clothing&lt;/Title&gt;
        &lt;Description&gt;Shop our men's collection&lt;/Description&gt;
      &lt;/SeoInfo&gt;
    &lt;/Node&gt;
  &lt;/Nodes&gt;
  &lt;Entries&gt;
    &lt;Entry code=""SHIRT-001"" entryType=""Product""&gt;
      &lt;Name&gt;Classic Oxford Shirt&lt;/Name&gt;
      &lt;MetaData&gt;
        &lt;MetaField name=""Brand""&gt;
          &lt;Data&gt;Oxford Co&lt;/Data&gt;
        &lt;/MetaField&gt;
      &lt;/MetaData&gt;
    &lt;/Entry&gt;
    &lt;Entry code=""SHIRT-001-BLUE-M"" entryType=""Variation""&gt;
      &lt;Name&gt;Classic Oxford Shirt - Blue - M&lt;/Name&gt;
      &lt;MetaData&gt;
        &lt;MetaField name=""Colour""&gt;&lt;Data&gt;Blue&lt;/Data&gt;&lt;/MetaField&gt;
        &lt;MetaField name=""Size""&gt;&lt;Data&gt;M&lt;/Data&gt;&lt;/MetaField&gt;
      &lt;/MetaData&gt;
    &lt;/Entry&gt;
  &lt;/Entries&gt;
  &lt;Relations&gt;
    &lt;CatalogEntryRelation&gt;
      &lt;ParentEntryCode&gt;SHIRT-001&lt;/ParentEntryCode&gt;
      &lt;ChildEntryCode&gt;SHIRT-001-BLUE-M&lt;/ChildEntryCode&gt;
      &lt;RelationType&gt;ProductVariation&lt;/RelationType&gt;
    &lt;/CatalogEntryRelation&gt;
  &lt;/Relations&gt;
  &lt;Associations&gt;
    &lt;!-- Cross-sell, up-sell relationships --&gt;
  &lt;/Associations&gt;
&lt;/Catalog&gt;</code></pre>

<h3>Using the Import API</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>using Mediachase.Commerce.Catalog.ImportExport;

public class CatalogImportService
{
    private readonly CatalogImportExport _importExport;

    public CatalogImportService(CatalogImportExport importExport)
    {
        _importExport = importExport;
    }

    public void ImportCatalog(Stream catalogStream)
    {
        // Import with options
        _importExport.Import(
            catalogStream,
            new ImportOptions
            {
                ContinueOnError = true,
                PersistChanges = true
            });
    }

    public void ExportCatalog(string catalogName, Stream outputStream)
    {
        _importExport.Export(
            catalogName,
            outputStream,
            new ExportOptions());
    }
}</code></pre>

<h3>Custom Import Job</h3>
<p>For complex scenarios, create a scheduled job:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[ScheduledPlugIn(
    DisplayName = ""Product Import Job"",
    Description = ""Imports products from external PIM"")]
public class ProductImportJob : ScheduledJobBase
{
    private readonly IContentRepository _contentRepository;
    private readonly ReferenceConverter _referenceConverter;
    private readonly IPriceService _priceService;
    private readonly IInventoryService _inventoryService;

    public ProductImportJob(
        IContentRepository contentRepository,
        ReferenceConverter referenceConverter,
        IPriceService priceService,
        IInventoryService inventoryService)
    {
        _contentRepository = contentRepository;
        _referenceConverter = referenceConverter;
        _priceService = priceService;
        _inventoryService = inventoryService;
    }

    public override string Execute()
    {
        var importData = FetchFromExternalSystem();
        var imported = 0;
        var errors = 0;

        foreach (var item in importData)
        {
            try
            {
                ImportProduct(item);
                imported++;
            }
            catch (Exception ex)
            {
                OnStatusChanged($""Error importing {item.Code}: {ex.Message}"");
                errors++;
            }
        }

        return $""Imported {imported} products. {errors} errors."";
    }

    private void ImportProduct(ExternalProduct data)
    {
        // Find or create product
        var contentLink = _referenceConverter.GetContentLink(data.Code);

        ProductContent product;
        if (contentLink == ContentReference.EmptyReference)
        {
            product = CreateNewProduct(data);
        }
        else
        {
            product = UpdateExistingProduct(contentLink, data);
        }

        // Update pricing
        UpdatePricing(product.ContentLink, data.Prices);

        // Update inventory
        UpdateInventory(product.ContentLink, data.Inventory);
    }
}</code></pre>

<h3>Best Practices</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li><strong>Validate before import</strong> - Check data quality before processing</li>
        <li><strong>Use transactions</strong> - Wrap bulk operations in transactions where possible</li>
        <li><strong>Handle errors gracefully</strong> - Log errors and continue processing</li>
        <li><strong>Schedule during off-peak</strong> - Run large imports during low-traffic periods</li>
        <li><strong>Incremental updates</strong> - Import only changed data when possible</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 3: Product Management

    private LearningModule BuildProductManagementModule()
    {
        return new LearningModule
        {
            Id = "product-management",
            Title = "Product Management",
            Description = "Learn to create, configure, and manage products, variants, bundles, and packages in Commerce Connect.",
            Icon = "cube",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pm-product-variants",
                    ModuleId = "product-management",
                    Title = "Products and Variants",
                    Summary = "Understand the relationship between products and their purchasable variants.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the product-variant relationship",
                        "Configure variant attributes and properties",
                        "Work with multi-level product hierarchies",
                        "Best practices for variant organization"
                    },
                    Content = @"
<h2>Understanding Products and Variants</h2>
<p>In Commerce Connect, products and variants work together to represent your merchandise. Products are displayable items with marketing content, while variants (SKUs) are the actual purchasable units.</p>

<h3>The Product-Variant Relationship</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
📦 Product (Displayable)
│   ├── Name, Description, Images
│   ├── Marketing Content
│   ├── SEO Information
│   └── Brand, Category, Attributes
│
├── 🏷️ Variant 1 (Purchasable)
│       ├── Code/SKU
│       ├── Price
│       ├── Inventory
│       └── Variant-specific attributes (Size: S, Colour: Blue)
│
├── 🏷️ Variant 2 (Purchasable)
│       ├── Code/SKU
│       ├── Price
│       ├── Inventory
│       └── Variant-specific attributes (Size: M, Colour: Blue)
│
└── 🏷️ Variant 3 (Purchasable)
        └── ...
</pre>

<h3>Why Separate Products and Variants?</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Reason</th>
            <th class=""px-4 py-2 text-left"">Explanation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Content Reuse</td><td class=""px-4 py-2"">Write description once, apply to all variants</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Display Logic</td><td class=""px-4 py-2"">Show one product page with variant selector</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Search Results</td><td class=""px-4 py-2"">One search result per product, not per SKU</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Price Flexibility</td><td class=""px-4 py-2"">Different sizes/colours can have different prices</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Inventory Tracking</td><td class=""px-4 py-2"">Track stock per variant, not per product</td></tr>
    </tbody>
</table>

<h3>Defining Variant Attributes</h3>
<p>Variants typically differ by one or more attributes:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class ApparelVariant : VariationContent
{
    [Display(Name = ""Size"", Order = 10)]
    [Required]
    [SelectOne(SelectionFactoryType = typeof(SizeSelectionFactory))]
    public virtual string Size { get; set; }

    [Display(Name = ""Colour"", Order = 20)]
    [Required]
    public virtual string Colour { get; set; }

    [Display(Name = ""Colour Code"", Order = 30)]
    [RegularExpression(@""^#[0-9A-Fa-f]{6}$"")]
    public virtual string ColourCode { get; set; }

    [Display(Name = ""Weight (kg)"", Order = 40)]
    [Range(0.01, 100)]
    public virtual decimal Weight { get; set; }

    // Computed display name
    public override string DisplayName =>
        $""{base.DisplayName} - {Colour} - {Size}"";
}</code></pre>

<h3>Working with Variants Programmatically</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class VariantService
{
    private readonly IContentLoader _contentLoader;
    private readonly IRelationRepository _relationRepository;

    // Get all variants for a product
    public IEnumerable&lt;VariationContent&gt; GetVariants(ProductContent product)
    {
        return _contentLoader
            .GetChildren&lt;VariationContent&gt;(product.ContentLink);
    }

    // Get variants by attribute
    public IEnumerable&lt;ApparelVariant&gt; GetVariantsByColour(
        ProductContent product,
        string colour)
    {
        return GetVariants(product)
            .OfType&lt;ApparelVariant&gt;()
            .Where(v => v.Colour.Equals(colour,
                StringComparison.OrdinalIgnoreCase));
    }

    // Get available sizes for a colour
    public IEnumerable&lt;string&gt; GetAvailableSizes(
        ProductContent product,
        string colour)
    {
        return GetVariantsByColour(product, colour)
            .Select(v => v.Size)
            .Distinct()
            .OrderBy(s => GetSizeOrder(s));
    }

    // Get variant matrix (colour × size)
    public Dictionary&lt;string, Dictionary&lt;string, ApparelVariant&gt;&gt;
        GetVariantMatrix(ProductContent product)
    {
        return GetVariants(product)
            .OfType&lt;ApparelVariant&gt;()
            .GroupBy(v => v.Colour)
            .ToDictionary(
                g => g.Key,
                g => g.ToDictionary(v => v.Size));
    }
}</code></pre>

<h3>Multi-Level Hierarchies</h3>
<p>For complex products, you can nest products within products:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <pre class=""text-sm"">
📦 ""Designer Handbag"" (Style - Level 1)
├── 📦 ""Designer Handbag - Leather"" (Material - Level 2)
│   ├── 🏷️ ""Leather - Black - Small"" (SKU)
│   ├── 🏷️ ""Leather - Black - Medium"" (SKU)
│   └── 🏷️ ""Leather - Tan - Small"" (SKU)
└── 📦 ""Designer Handbag - Canvas"" (Material - Level 2)
    ├── 🏷️ ""Canvas - Navy - Small"" (SKU)
    └── 🏷️ ""Canvas - Navy - Medium"" (SKU)
    </pre>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Variant Selection Factory",
                            Description = "Selection factory for size dropdown",
                            ExampleContent = @"public class SizeSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(
        ExtendedMetadata metadata)
    {
        return new List<SelectItem>
        {
            new SelectItem { Text = ""Extra Small"", Value = ""XS"" },
            new SelectItem { Text = ""Small"", Value = ""S"" },
            new SelectItem { Text = ""Medium"", Value = ""M"" },
            new SelectItem { Text = ""Large"", Value = ""L"" },
            new SelectItem { Text = ""Extra Large"", Value = ""XL"" },
            new SelectItem { Text = ""2X Large"", Value = ""2XL"" }
        };
    }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "pm-bundles-packages",
                    ModuleId = "product-management",
                    Title = "Bundles and Packages",
                    Summary = "Learn to create product bundles and packages for special offers.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the difference between bundles and packages",
                        "Create and configure bundles",
                        "Work with packages and combined pricing",
                        "Handle inventory for bundles"
                    },
                    Content = @"
<h2>Bundles and Packages</h2>
<p>Commerce Connect provides two ways to sell multiple products together: <strong>Bundles</strong> and <strong>Packages</strong>. Understanding when to use each is important for effective merchandising.</p>

<h3>Bundles vs Packages</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Bundle</th>
            <th class=""px-4 py-2 text-left"">Package</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Contains</td><td class=""px-4 py-2"">Entries (products, variants, other bundles)</td><td class=""px-4 py-2"">Variants only</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Pricing</td><td class=""px-4 py-2"">Sum of component prices (or custom)</td><td class=""px-4 py-2"">Single package price</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Inventory</td><td class=""px-4 py-2"">Based on component availability</td><td class=""px-4 py-2"">Own inventory tracking</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Cart Display</td><td class=""px-4 py-2"">Individual items visible</td><td class=""px-4 py-2"">Single line item</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Use Case</td><td class=""px-4 py-2"">""Frequently bought together""</td><td class=""px-4 py-2"">Pre-packaged sets, gift boxes</td></tr>
    </tbody>
</table>

<h3>Creating a Bundle</h3>
<p>Bundles are collections of entries sold together:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[CatalogContentType(
    GUID = ""11111111-1111-1111-1111-111111111111"",
    DisplayName = ""Gift Bundle"")]
public class GiftBundle : BundleContent
{
    [Display(Name = ""Bundle Description"")]
    public virtual XhtmlString Description { get; set; }

    [Display(Name = ""Savings Message"")]
    public virtual string SavingsMessage { get; set; }

    [Display(Name = ""Bundle Image"")]
    [UIHint(UIHint.Image)]
    public virtual ContentReference BundleImage { get; set; }
}</code></pre>

<h4>Managing Bundle Entries</h4>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class BundleService
{
    private readonly IRelationRepository _relationRepository;
    private readonly IContentLoader _contentLoader;

    // Get entries in a bundle
    public IEnumerable&lt;EntryContentBase&gt; GetBundleEntries(
        BundleContent bundle)
    {
        return _relationRepository
            .GetChildren&lt;BundleEntry&gt;(bundle.ContentLink)
            .OrderBy(r => r.SortOrder)
            .Select(r => _contentLoader.Get&lt;EntryContentBase&gt;(r.Child));
    }

    // Add entry to bundle
    public void AddToBundle(
        ContentReference bundle,
        ContentReference entry,
        int quantity = 1)
    {
        var relation = new BundleEntry
        {
            Parent = bundle,
            Child = entry,
            Quantity = quantity,
            SortOrder = GetNextSortOrder(bundle)
        };

        _relationRepository.UpdateRelation(relation);
    }

    // Calculate bundle price
    public Money GetBundlePrice(BundleContent bundle, MarketId market)
    {
        var entries = GetBundleEntries(bundle);
        var currency = _currentMarket.GetCurrentMarket().DefaultCurrency;

        decimal total = 0;
        foreach (var entry in entries)
        {
            var price = _priceService.GetPrice(entry, market);
            if (price != null)
            {
                total += price.UnitPrice.Amount;
            }
        }

        return new Money(total, currency);
    }
}</code></pre>

<h3>Creating a Package</h3>
<p>Packages have their own pricing and inventory:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[CatalogContentType(
    GUID = ""22222222-2222-2222-2222-222222222222"",
    DisplayName = ""Gift Set Package"")]
public class GiftSetPackage : PackageContent
{
    [Display(Name = ""Package Contents Description"")]
    public virtual XhtmlString ContentsDescription { get; set; }

    [Display(Name = ""Package Value"")]
    [UIHint(""Money"")]
    public virtual decimal PackageValue { get; set; }
}</code></pre>

<h3>Bundle Examples</h3>
<div class=""grid grid-cols-1 md:grid-cols-2 gap-4 my-4"">
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold"">🎁 Gift Bundle</h4>
        <p class=""text-sm mt-2"">""Complete Skincare Set""</p>
        <ul class=""text-sm mt-2"">
            <li>• Cleanser (1x)</li>
            <li>• Toner (1x)</li>
            <li>• Moisturiser (1x)</li>
            <li>• Eye Cream (1x)</li>
        </ul>
        <p class=""text-sm mt-2 text-green-600"">Save 15% vs buying separately</p>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold"">📦 Pre-Packed Set</h4>
        <p class=""text-sm mt-2"">""Starter Tool Kit""</p>
        <ul class=""text-sm mt-2"">
            <li>• Hammer</li>
            <li>• Screwdriver Set</li>
            <li>• Pliers</li>
            <li>• Tape Measure</li>
        </ul>
        <p class=""text-sm mt-2"">Fixed price: £49.99</p>
    </div>
</div>

<h3>Handling Bundle Inventory</h3>
<p>Bundle availability depends on all component items:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public bool IsBundleInStock(BundleContent bundle, string warehouseCode)
{
    var entries = GetBundleEntries(bundle);

    foreach (var entry in entries)
    {
        if (entry is VariationContent variant)
        {
            var inventory = _inventoryService.Get(
                variant.Code, warehouseCode);

            if (inventory == null || inventory.PurchaseAvailableQuantity <= 0)
            {
                return false; // Bundle unavailable if any component is out
            }
        }
    }

    return true;
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pm-product-associations",
                    ModuleId = "product-management",
                    Title = "Product Associations",
                    Summary = "Configure cross-sells, up-sells, and related product relationships.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand association types (cross-sell, up-sell, accessories)",
                        "Create and manage product associations",
                        "Display related products on product pages",
                        "Use associations for personalization"
                    },
                    Content = @"
<h2>Product Associations</h2>
<p>Associations link products together for cross-selling, up-selling, and showing related items. These relationships drive product recommendations and increase average order value.</p>

<h3>Association Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Cross-Sell</td><td class=""px-4 py-2"">Complementary products</td><td class=""px-4 py-2"">""Customers also bought""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Up-Sell</td><td class=""px-4 py-2"">Higher-value alternatives</td><td class=""px-4 py-2"">""Consider upgrading to...""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Accessories</td><td class=""px-4 py-2"">Add-on products</td><td class=""px-4 py-2"">""Complete your purchase""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Related</td><td class=""px-4 py-2"">Similar products</td><td class=""px-4 py-2"">""You might also like""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Replacement</td><td class=""px-4 py-2"">Substitute products</td><td class=""px-4 py-2"">When product is discontinued</td></tr>
    </tbody>
</table>

<h3>Association Data Model</h3>
<p>Associations are managed through <code>Association</code> and <code>AssociationGroup</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Associations are grouped by type
AssociationGroup (e.g., ""CrossSell"")
├── Association (Source → Target with SortOrder)
├── Association (Source → Target with SortOrder)
└── Association (Source → Target with SortOrder)</code></pre>

<h3>Working with Associations</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class AssociationService
{
    private readonly IAssociationRepository _associationRepository;
    private readonly IContentLoader _contentLoader;

    public AssociationService(
        IAssociationRepository associationRepository,
        IContentLoader contentLoader)
    {
        _associationRepository = associationRepository;
        _contentLoader = contentLoader;
    }

    // Get associated products by type
    public IEnumerable&lt;EntryContentBase&gt; GetAssociations(
        EntryContentBase entry,
        string associationType)
    {
        var associations = _associationRepository
            .GetAssociations(entry.ContentLink)
            .Where(a => a.Group.Name.Equals(
                associationType,
                StringComparison.OrdinalIgnoreCase))
            .OrderBy(a => a.SortOrder);

        foreach (var association in associations)
        {
            var target = _contentLoader.Get&lt;EntryContentBase&gt;(
                association.Target);
            if (target != null)
            {
                yield return target;
            }
        }
    }

    // Get cross-sells
    public IEnumerable&lt;EntryContentBase&gt; GetCrossSells(
        EntryContentBase entry)
    {
        return GetAssociations(entry, ""CrossSell"");
    }

    // Get up-sells
    public IEnumerable&lt;EntryContentBase&gt; GetUpSells(
        EntryContentBase entry)
    {
        return GetAssociations(entry, ""UpSell"");
    }
}</code></pre>

<h3>Creating Associations</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public void CreateAssociation(
    ContentReference source,
    ContentReference target,
    string groupName,
    int sortOrder = 0)
{
    // Get or create association group
    var group = _associationRepository
        .GetAssociationGroups()
        .FirstOrDefault(g => g.Name == groupName);

    if (group == null)
    {
        group = new AssociationGroup
        {
            Name = groupName,
            Description = $""{groupName} associations"",
            SortOrder = 0
        };
        _associationRepository.SaveAssociationGroup(group);
    }

    // Create the association
    var association = new Association
    {
        Source = source,
        Target = target,
        Group = group,
        SortOrder = sortOrder
    };

    _associationRepository.UpdateAssociation(association);
}</code></pre>

<h3>Displaying Associations</h3>
<p>Example Razor view for showing cross-sells:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>@model ProductViewModel

@if (Model.CrossSells.Any())
{
    &lt;section class=""cross-sells mt-8""&gt;
        &lt;h3 class=""text-xl font-bold mb-4""&gt;
            Customers Also Bought
        &lt;/h3&gt;
        &lt;div class=""grid grid-cols-2 md:grid-cols-4 gap-4""&gt;
            @foreach (var product in Model.CrossSells.Take(4))
            {
                &lt;partial name=""_ProductCard"" model=""product"" /&gt;
            }
        &lt;/div&gt;
    &lt;/section&gt;
}</code></pre>

<h3>Best Practices</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li><strong>Limit displayed associations</strong> - Show 4-6 items maximum</li>
        <li><strong>Filter by availability</strong> - Only show in-stock items</li>
        <li><strong>Use analytics</strong> - Track which associations convert</li>
        <li><strong>Automate with AI</strong> - Consider Optimizely's AI recommendations</li>
        <li><strong>Bidirectional where appropriate</strong> - If A cross-sells B, B might cross-sell A</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Complete Association Service",
                            Description = "Full service for managing product associations",
                            ExampleContent = @"using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer;

public class ProductAssociationService
{
    private readonly IAssociationRepository _associationRepository;
    private readonly IContentLoader _contentLoader;

    public ProductAssociationService(
        IAssociationRepository associationRepository,
        IContentLoader contentLoader)
    {
        _associationRepository = associationRepository;
        _contentLoader = contentLoader;
    }

    public IEnumerable<T> GetAssociations<T>(
        ContentReference source,
        string groupName,
        int maxItems = 10) where T : EntryContentBase
    {
        return _associationRepository
            .GetAssociations(source)
            .Where(a => a.Group.Name.Equals(groupName,
                StringComparison.OrdinalIgnoreCase))
            .OrderBy(a => a.SortOrder)
            .Take(maxItems)
            .Select(a => _contentLoader.Get<T>(a.Target))
            .Where(e => e != null);
    }

    public void SetAssociations(
        ContentReference source,
        string groupName,
        IEnumerable<ContentReference> targets)
    {
        // Remove existing
        var existing = _associationRepository
            .GetAssociations(source)
            .Where(a => a.Group.Name == groupName);

        foreach (var association in existing)
        {
            _associationRepository.RemoveAssociation(association);
        }

        // Add new
        int sortOrder = 0;
        foreach (var target in targets)
        {
            CreateAssociation(source, target, groupName, sortOrder++);
        }
    }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "pm-product-assets",
                    ModuleId = "product-management",
                    Title = "Managing Product Assets",
                    Summary = "Learn to handle product images, documents, and media assets.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Upload and organize product images",
                        "Create image variants for different contexts",
                        "Attach documents (PDFs, spec sheets)",
                        "Best practices for product media"
                    },
                    Content = @"
<h2>Managing Product Assets</h2>
<p>Product images and media are crucial for e-commerce success. Commerce Connect integrates with the CMS media system while providing commerce-specific asset management features.</p>

<h3>Asset Storage Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Location</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Global Assets Folder</td><td class=""px-4 py-2"">Shared assets (logos, icons)</td></tr>
        <tr><td class=""px-4 py-2"">Catalog Assets Folder</td><td class=""px-4 py-2"">Product-specific images</td></tr>
        <tr><td class=""px-4 py-2"">External DAM</td><td class=""px-4 py-2"">Enterprise digital asset management</td></tr>
        <tr><td class=""px-4 py-2"">CDN URLs</td><td class=""px-4 py-2"">Third-party hosted images</td></tr>
    </tbody>
</table>

<h3>Product Image Properties</h3>
<p>Define image properties on your product types:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class FashionProduct : ProductContent
{
    // Single main image
    [Display(Name = ""Main Image"", GroupName = ""Images"", Order = 10)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference MainImage { get; set; }

    // Image gallery using ContentArea
    [Display(Name = ""Gallery Images"", GroupName = ""Images"", Order = 20)]
    [AllowedTypes(typeof(ImageData))]
    public virtual ContentArea GalleryImages { get; set; }

    // Multiple images using list
    [Display(Name = ""Additional Images"", GroupName = ""Images"", Order = 30)]
    public virtual IList&lt;ContentReference&gt; AdditionalImages { get; set; }

    // Video content
    [Display(Name = ""Product Video"", GroupName = ""Media"", Order = 40)]
    [UIHint(UIHint.Video)]
    public virtual ContentReference ProductVideo { get; set; }
}</code></pre>

<h3>Working with Images Programmatically</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class ProductImageService
{
    private readonly IContentLoader _contentLoader;
    private readonly IUrlResolver _urlResolver;

    public ProductImageService(
        IContentLoader contentLoader,
        IUrlResolver urlResolver)
    {
        _contentLoader = contentLoader;
        _urlResolver = urlResolver;
    }

    // Get main image URL
    public string GetMainImageUrl(ProductContent product)
    {
        if (ContentReference.IsNullOrEmpty(product.MainImage))
            return ""/images/placeholder.jpg"";

        return _urlResolver.GetUrl(product.MainImage);
    }

    // Get all gallery images
    public IEnumerable&lt;ProductImage&gt; GetGalleryImages(
        ProductContent product)
    {
        if (product.GalleryImages == null)
            yield break;

        foreach (var item in product.GalleryImages.Items)
        {
            var image = _contentLoader.Get&lt;ImageData&gt;(
                item.ContentLink);

            if (image != null)
            {
                yield return new ProductImage
                {
                    Url = _urlResolver.GetUrl(image.ContentLink),
                    AltText = image.Name,
                    ContentReference = image.ContentLink
                };
            }
        }
    }

    // Get resized image URL (if using ImageResizer)
    public string GetResizedImageUrl(
        ContentReference imageRef,
        int width,
        int height)
    {
        var baseUrl = _urlResolver.GetUrl(imageRef);
        return $""{baseUrl}?width={width}&height={height}&mode=crop"";
    }
}</code></pre>

<h3>Image Best Practices</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <h4 class=""font-bold"">Recommended Image Specifications</h4>
    <ul class=""mt-2 space-y-1"">
        <li><strong>Main Product Image:</strong> 1200×1200px minimum, square ratio</li>
        <li><strong>Thumbnails:</strong> 400×400px</li>
        <li><strong>Gallery Images:</strong> 1200×1600px (portrait) or 1600×1200px (landscape)</li>
        <li><strong>Format:</strong> WebP with JPEG fallback</li>
        <li><strong>File Size:</strong> Under 200KB for web display</li>
    </ul>
</div>

<h3>Managing Documents</h3>
<p>Attach PDFs, spec sheets, and other documents:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class TechnicalProduct : ProductContent
{
    [Display(Name = ""Specification Sheet"", GroupName = ""Documents"")]
    [AllowedTypes(typeof(PdfFile))]
    public virtual ContentReference SpecSheet { get; set; }

    [Display(Name = ""User Manual"", GroupName = ""Documents"")]
    [AllowedTypes(typeof(PdfFile))]
    public virtual ContentReference UserManual { get; set; }

    [Display(Name = ""Safety Information"", GroupName = ""Documents"")]
    public virtual ContentArea SafetyDocuments { get; set; }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pm-product-availability",
                    ModuleId = "product-management",
                    Title = "Product Availability and Markets",
                    Summary = "Control which products are available in different markets and time periods.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure product availability dates",
                        "Restrict products to specific markets",
                        "Handle product visibility settings",
                        "Work with product status"
                    },
                    Content = @"
<h2>Product Availability</h2>
<p>Commerce Connect provides multiple ways to control when and where products are available for purchase. Understanding these controls is essential for proper catalog management.</p>

<h3>Availability Controls</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Control</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Level</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Start/Stop Publish</td><td class=""px-4 py-2"">Time-based visibility</td><td class=""px-4 py-2"">Entry</td></tr>
        <tr><td class=""px-4 py-2"">Market Availability</td><td class=""px-4 py-2"">Geographic restrictions</td><td class=""px-4 py-2"">Entry</td></tr>
        <tr><td class=""px-4 py-2"">Inventory</td><td class=""px-4 py-2"">Stock availability</td><td class=""px-4 py-2"">Variant</td></tr>
        <tr><td class=""px-4 py-2"">Publication Status</td><td class=""px-4 py-2"">Draft vs Published</td><td class=""px-4 py-2"">Entry</td></tr>
    </tbody>
</table>

<h3>Date-Based Availability</h3>
<p>Control when products are visible:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Set availability dates programmatically
public void SetAvailabilityDates(
    EntryContentBase entry,
    DateTime startDate,
    DateTime? endDate = null)
{
    var writeable = entry.CreateWritableClone&lt;EntryContentBase&gt;();

    writeable.StartPublish = startDate;
    writeable.StopPublish = endDate;

    _contentRepository.Save(writeable, SaveAction.Publish);
}

// Check if currently available
public bool IsCurrentlyAvailable(EntryContentBase entry)
{
    var now = DateTime.UtcNow;

    if (entry.StartPublish.HasValue && entry.StartPublish > now)
        return false;

    if (entry.StopPublish.HasValue && entry.StopPublish < now)
        return false;

    return true;
}</code></pre>

<h3>Market Availability</h3>
<p>Restrict products to specific markets:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class MarketAvailabilityService
{
    private readonly IMarketService _marketService;
    private readonly IContentRepository _contentRepository;

    // Check if entry is available in market
    public bool IsAvailableInMarket(
        EntryContentBase entry,
        MarketId marketId)
    {
        // CatalogContent has a Markets property
        if (entry is CatalogContentBase catalogContent)
        {
            var markets = catalogContent.MarketFilter;

            // Empty means available in all markets
            if (markets == null || !markets.Any())
                return true;

            return markets.Contains(marketId.Value);
        }

        return true;
    }

    // Set market availability
    public void SetMarketAvailability(
        ContentReference entryRef,
        IEnumerable&lt;MarketId&gt; markets)
    {
        var entry = _contentRepository
            .Get&lt;EntryContentBase&gt;(entryRef)
            .CreateWritableClone&lt;EntryContentBase&gt;();

        if (entry is CatalogContentBase catalogContent)
        {
            // Set the market filter
            var marketFilter = markets
                .Select(m => m.Value)
                .ToList();

            // Apply filter...
        }

        _contentRepository.Save(entry, SaveAction.Publish);
    }
}

// Extension method for easy checking
public static class EntryExtensions
{
    public static bool IsAvailableInCurrentMarket(
        this EntryContentBase entry)
    {
        var currentMarket = ServiceLocator.Current
            .GetInstance&lt;ICurrentMarket&gt;()
            .GetCurrentMarket();

        // Check market availability logic
        return true; // Simplified
    }
}</code></pre>

<h3>Filtering Available Products</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public IEnumerable&lt;ProductContent&gt; GetAvailableProducts(
    ContentReference categoryRef,
    MarketId market)
{
    var allProducts = _contentLoader
        .GetChildren&lt;ProductContent&gt;(categoryRef);

    return allProducts
        .Where(p => IsPublished(p))
        .Where(p => IsCurrentlyAvailable(p))
        .Where(p => IsAvailableInMarket(p, market))
        .Where(p => HasAvailableVariants(p, market));
}

private bool HasAvailableVariants(ProductContent product, MarketId market)
{
    var variants = _contentLoader
        .GetChildren&lt;VariationContent&gt;(product.ContentLink);

    return variants.Any(v =>
        IsPublished(v) &&
        IsCurrentlyAvailable(v) &&
        IsAvailableInMarket(v, market) &&
        HasInventory(v));
}</code></pre>

<h3>Publication Status</h3>
<p>Products can be in different states:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Visible to Visitors</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Draft</td><td class=""px-4 py-2"">No</td><td class=""px-4 py-2"">Work in progress</td></tr>
        <tr><td class=""px-4 py-2"">Published</td><td class=""px-4 py-2"">Yes</td><td class=""px-4 py-2"">Live on site</td></tr>
        <tr><td class=""px-4 py-2"">Previously Published</td><td class=""px-4 py-2"">Yes (old version)</td><td class=""px-4 py-2"">Has unpublished changes</td></tr>
        <tr><td class=""px-4 py-2"">Expired</td><td class=""px-4 py-2"">No</td><td class=""px-4 py-2"">Past stop publish date</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 4: Pricing & Inventory

    private LearningModule BuildPricingInventoryModule()
    {
        return new LearningModule
        {
            Id = "pricing-inventory",
            Title = "Pricing & Inventory",
            Description = "Master pricing strategies, inventory management, and warehouse operations in Commerce Connect.",
            Icon = "currency-pound",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pi-pricing-overview",
                    ModuleId = "pricing-inventory",
                    Title = "Pricing System Overview",
                    Summary = "Understand how Commerce Connect handles pricing with markets, currencies, and customer groups.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the multi-dimensional pricing model",
                        "Learn the five pricing dimensions (market, currency, quantity, date, customer)",
                        "Know the difference between IPriceService and IPriceDetailService",
                        "Understand how prices are matched and selected"
                    },
                    Content = @"
<h2>Pricing System Overview</h2>
<p>Commerce Connect implements a <strong>provider-based pricing system</strong> that supports complex pricing scenarios including multiple markets, currencies, quantity breaks, and customer-specific pricing.</p>

<h3>The Five Pricing Dimensions</h3>
<p>Every price in Commerce Connect is defined by five contextual factors:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Dimension</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Market</td><td class=""px-4 py-2"">Geographic/business region</td><td class=""px-4 py-2"">UK, US, Europe</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Currency</td><td class=""px-4 py-2"">Payment currency</td><td class=""px-4 py-2"">GBP, USD, EUR</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Quantity</td><td class=""px-4 py-2"">Minimum quantity threshold</td><td class=""px-4 py-2"">1, 10, 100 units</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Date/Time</td><td class=""px-4 py-2"">Valid period</td><td class=""px-4 py-2"">Sale prices, future prices</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Customer</td><td class=""px-4 py-2"">Who the price applies to</td><td class=""px-4 py-2"">All, VIP group, specific customer</td></tr>
    </tbody>
</table>

<h3>Price Matching Logic</h3>
<p>When multiple prices qualify for a purchase, Commerce Connect automatically applies the <strong>lowest price</strong>:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-bold"">Example: Price Selection</h4>
    <p class=""mt-2"">Customer buying 15 units of SKU-001 in UK market:</p>
    <ul class=""mt-2 space-y-1"">
        <li>• Base price (all customers, qty 1): £10.00</li>
        <li>• Volume price (all customers, qty 10): £9.00</li>
        <li>• Trade price (Trade group, qty 1): £8.50</li>
        <li class=""text-green-600 font-medium"">→ System selects £8.50 (lowest matching price)</li>
    </ul>
</div>

<h3>The IPriceValue Interface</h3>
<p>Prices are represented by <code>IPriceValue</code>:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public interface IPriceValue
{
    CatalogKey CatalogKey { get; }           // SKU identifier
    MarketId MarketId { get; }               // Market
    CustomerPricing CustomerPricing { get; } // Customer/group
    Money UnitPrice { get; }                 // Amount + currency
    decimal MinQuantity { get; }             // Quantity threshold
    DateTime ValidFrom { get; }              // Start date
    DateTime? ValidUntil { get; }            // End date (optional)
}</code></pre>

<h3>Pricing Services</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Service</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">IPriceService</td><td class=""px-4 py-2"">Optimised price retrieval</td><td class=""px-4 py-2"">Product pages, cart, checkout</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IPriceDetailService</td><td class=""px-4 py-2"">Price administration</td><td class=""px-4 py-2"">Commerce Manager, import jobs</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p><strong>Important:</strong> <code>IPriceService</code> caches and optimises data. Use it for customer-facing functionality. <code>IPriceDetailService</code> returns exact user input without caching—use it only for administrative interfaces.</p>
</div>

<h3>Customer Pricing Levels</h3>
<p>Prices can target three customer levels:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// 1. All customers (default)
var allCustomersPricing = CustomerPricing.AllCustomers;

// 2. Specific customer group
var groupPricing = new CustomerPricing(
    CustomerPricing.PriceType.PriceGroup,
    ""TradeCustomers"");

// 3. Individual customer
var individualPricing = new CustomerPricing(
    CustomerPricing.PriceType.UserName,
    ""john.smith@company.com"");</code></pre>

<h3>Key Namespaces</h3>
<ul>
    <li><code>Mediachase.Commerce.Pricing</code> - Core pricing types</li>
    <li><code>EPiServer.Commerce.Catalog</code> - Catalog key and references</li>
    <li><code>Mediachase.Commerce</code> - Money, MarketId</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pi-working-with-prices",
                    ModuleId = "pricing-inventory",
                    Title = "Working with Prices",
                    Summary = "Learn to retrieve, display, and manage product prices programmatically.",
                    Order = 2,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Retrieve prices using IPriceService",
                        "Get the best price for a customer and quantity",
                        "Create and update prices programmatically",
                        "Handle currency formatting"
                    },
                    Content = @"
<h2>Working with Prices</h2>
<p>Learn to retrieve and manage prices using Commerce Connect's pricing APIs.</p>

<h3>Getting Prices with IPriceService</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class PriceService
{
    private readonly IPriceService _priceService;
    private readonly ICurrentMarket _currentMarket;

    public PriceService(
        IPriceService priceService,
        ICurrentMarket currentMarket)
    {
        _priceService = priceService;
        _currentMarket = currentMarket;
    }

    // Get the best price for a variant
    public Money? GetPrice(VariationContent variant)
    {
        var market = _currentMarket.GetCurrentMarket();
        var currency = market.DefaultCurrency;

        var priceFilter = new PriceFilter
        {
            Currencies = new[] { currency },
            CustomerPricing = new[] { CustomerPricing.AllCustomers },
            Quantity = 1,
            ReturnCustomerPricing = false
        };

        var prices = _priceService.GetPrices(
            market.MarketId,
            DateTime.UtcNow,
            new CatalogKey(variant.Code),
            priceFilter);

        return prices
            .OrderBy(p => p.UnitPrice.Amount)
            .FirstOrDefault()?.UnitPrice;
    }

    // Get price for specific quantity
    public Money? GetPriceForQuantity(
        VariationContent variant,
        decimal quantity)
    {
        var market = _currentMarket.GetCurrentMarket();
        var currency = market.DefaultCurrency;

        var priceFilter = new PriceFilter
        {
            Currencies = new[] { currency },
            Quantity = quantity,
            ReturnCustomerPricing = false
        };

        var prices = _priceService.GetPrices(
            market.MarketId,
            DateTime.UtcNow,
            new CatalogKey(variant.Code),
            priceFilter);

        return prices
            .Where(p => p.MinQuantity <= quantity)
            .OrderBy(p => p.UnitPrice.Amount)
            .FirstOrDefault()?.UnitPrice;
    }
}</code></pre>

<h3>Getting All Prices for a Product</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Get all prices for display (e.g., in Commerce Manager)
public IEnumerable&lt;IPriceValue&gt; GetAllPrices(string code)
{
    var catalogKey = new CatalogKey(code);

    return _priceDetailService.List(catalogKey);
}

// Get price range for a product
public (Money? MinPrice, Money? MaxPrice) GetPriceRange(
    ProductContent product)
{
    var variants = _contentLoader
        .GetChildren&lt;VariationContent&gt;(product.ContentLink);

    var prices = variants
        .Select(v => GetPrice(v))
        .Where(p => p.HasValue)
        .Select(p => p.Value)
        .ToList();

    if (!prices.Any())
        return (null, null);

    return (
        prices.OrderBy(p => p.Amount).First(),
        prices.OrderByDescending(p => p.Amount).First()
    );
}</code></pre>

<h3>Setting Prices</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class PriceManagementService
{
    private readonly IPriceDetailService _priceDetailService;

    // Set a simple price
    public void SetPrice(
        string code,
        MarketId market,
        Currency currency,
        decimal amount)
    {
        var priceValue = new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = market,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = 0,
            UnitPrice = new Money(amount, currency),
            ValidFrom = DateTime.UtcNow,
            ValidUntil = null
        };

        _priceDetailService.Save(new[] { priceValue });
    }

    // Set tiered pricing
    public void SetTieredPricing(
        string code,
        MarketId market,
        Currency currency,
        Dictionary&lt;decimal, decimal&gt; quantityPrices)
    {
        var prices = quantityPrices.Select(kvp => new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = market,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = kvp.Key,
            UnitPrice = new Money(kvp.Value, currency),
            ValidFrom = DateTime.UtcNow
        });

        _priceDetailService.Save(prices);
    }

    // Set sale price with dates
    public void SetSalePrice(
        string code,
        MarketId market,
        Currency currency,
        decimal amount,
        DateTime startDate,
        DateTime endDate)
    {
        var priceValue = new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = market,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = 0,
            UnitPrice = new Money(amount, currency),
            ValidFrom = startDate,
            ValidUntil = endDate
        };

        _priceDetailService.Save(new[] { priceValue });
    }
}</code></pre>

<h3>Displaying Prices</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Format price for display
public string FormatPrice(Money price)
{
    return price.ToString(); // Uses currency formatting
}

// Display with sale price
public class PriceViewModel
{
    public Money? OriginalPrice { get; set; }
    public Money? SalePrice { get; set; }
    public bool IsOnSale => SalePrice.HasValue &&
        SalePrice.Value.Amount < OriginalPrice?.Amount;
    public int SavingsPercent => IsOnSale
        ? (int)((1 - SalePrice.Value.Amount / OriginalPrice.Value.Amount) * 100)
        : 0;
}</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Complete Pricing Service",
                            Description = "Full service for retrieving product prices",
                            ExampleContent = @"using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce;
using Mediachase.Commerce.Pricing;

public class ProductPricingService
{
    private readonly IPriceService _priceService;
    private readonly ICurrentMarket _currentMarket;
    private readonly IContentLoader _contentLoader;

    public ProductPricingService(
        IPriceService priceService,
        ICurrentMarket currentMarket,
        IContentLoader contentLoader)
    {
        _priceService = priceService;
        _currentMarket = currentMarket;
        _contentLoader = contentLoader;
    }

    public PriceResult GetPrice(
        VariationContent variant,
        decimal quantity = 1,
        CustomerPricing customerPricing = null)
    {
        var market = _currentMarket.GetCurrentMarket();
        var currency = market.DefaultCurrency;

        var filter = new PriceFilter
        {
            Currencies = new[] { currency },
            CustomerPricing = customerPricing != null
                ? new[] { customerPricing, CustomerPricing.AllCustomers }
                : new[] { CustomerPricing.AllCustomers },
            Quantity = quantity,
            ReturnCustomerPricing = true
        };

        var prices = _priceService.GetPrices(
            market.MarketId,
            DateTime.UtcNow,
            new CatalogKey(variant.Code),
            filter).ToList();

        var bestPrice = prices
            .Where(p => p.MinQuantity <= quantity)
            .OrderBy(p => p.UnitPrice.Amount)
            .FirstOrDefault();

        var listPrice = prices
            .Where(p => p.CustomerPricing == CustomerPricing.AllCustomers)
            .Where(p => p.MinQuantity == 0)
            .FirstOrDefault();

        return new PriceResult
        {
            Price = bestPrice?.UnitPrice,
            ListPrice = listPrice?.UnitPrice,
            IsDiscounted = bestPrice != null && listPrice != null
                && bestPrice.UnitPrice.Amount < listPrice.UnitPrice.Amount
        };
    }
}

public class PriceResult
{
    public Money? Price { get; set; }
    public Money? ListPrice { get; set; }
    public bool IsDiscounted { get; set; }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "pi-inventory-management",
                    ModuleId = "pricing-inventory",
                    Title = "Inventory Management",
                    Summary = "Learn to track and manage product inventory across warehouses.",
                    Order = 3,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Understand the inventory data model",
                        "Work with warehouses and inventory records",
                        "Check stock availability",
                        "Update inventory programmatically"
                    },
                    Content = @"
<h2>Inventory Management</h2>
<p>Commerce Connect provides a flexible inventory system that tracks stock levels across multiple warehouses, supporting both basic and complex fulfilment scenarios.</p>

<h3>Inventory Data Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Warehouse
├── Inventory Record (SKU A)
│   ├── In-Stock Quantity
│   ├── Reserved Quantity
│   ├── Reorder Min Quantity
│   ├── Backorder Quantity
│   └── Preorder Quantity
├── Inventory Record (SKU B)
└── ...
</pre>

<h3>Key Inventory Concepts</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Concept</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Warehouse</td><td class=""px-4 py-2"">Physical or logical inventory location</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">In-Stock Quantity</td><td class=""px-4 py-2"">Total physical inventory</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Reserved Quantity</td><td class=""px-4 py-2"">Allocated to pending orders</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Available Quantity</td><td class=""px-4 py-2"">In-Stock minus Reserved</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Backorder</td><td class=""px-4 py-2"">Quantity on order from supplier</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Preorder</td><td class=""px-4 py-2"">Available for future delivery</td></tr>
    </tbody>
</table>

<h3>The IInventoryService</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class InventoryService
{
    private readonly IInventoryService _inventoryService;
    private readonly IWarehouseRepository _warehouseRepository;

    public InventoryService(
        IInventoryService inventoryService,
        IWarehouseRepository warehouseRepository)
    {
        _inventoryService = inventoryService;
        _warehouseRepository = warehouseRepository;
    }

    // Check if variant is in stock
    public bool IsInStock(string code)
    {
        var warehouses = _warehouseRepository.List()
            .Where(w => w.IsActive && w.IsFulfillmentCenter);

        foreach (var warehouse in warehouses)
        {
            var inventoryKey = new InventoryKey(
                code,
                warehouse.Code);

            var inventory = _inventoryService.Get(inventoryKey);

            if (inventory != null &&
                inventory.PurchaseAvailableQuantity > 0)
            {
                return true;
            }
        }

        return false;
    }

    // Get available quantity
    public decimal GetAvailableQuantity(string code)
    {
        var warehouses = _warehouseRepository.List()
            .Where(w => w.IsActive && w.IsFulfillmentCenter);

        decimal total = 0;

        foreach (var warehouse in warehouses)
        {
            var inventoryKey = new InventoryKey(
                code,
                warehouse.Code);

            var inventory = _inventoryService.Get(inventoryKey);

            if (inventory != null)
            {
                total += inventory.PurchaseAvailableQuantity;
            }
        }

        return total;
    }

    // Get inventory status
    public InventoryStatus GetStatus(string code)
    {
        var available = GetAvailableQuantity(code);

        if (available > 10)
            return InventoryStatus.InStock;
        if (available > 0)
            return InventoryStatus.LowStock;

        // Check for backorder/preorder
        var inventory = GetTotalInventory(code);
        if (inventory?.BackorderAvailableQuantity > 0)
            return InventoryStatus.Backorder;
        if (inventory?.PreorderAvailableQuantity > 0)
            return InventoryStatus.Preorder;

        return InventoryStatus.OutOfStock;
    }
}

public enum InventoryStatus
{
    InStock,
    LowStock,
    OutOfStock,
    Backorder,
    Preorder
}</code></pre>

<h3>Updating Inventory</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Set inventory levels
public void SetInventory(
    string code,
    string warehouseCode,
    decimal inStockQuantity)
{
    var record = new InventoryRecord
    {
        CatalogEntryCode = code,
        WarehouseCode = warehouseCode,
        InStockQuantity = inStockQuantity,
        ReservedQuantity = 0,
        ReorderMinQuantity = 5,
        BackorderQuantity = 0,
        PreorderQuantity = 0,
        BackorderAvailabilityDate = null,
        PreorderAvailabilityDate = null,
        IsTracked = true
    };

    _inventoryService.Save(new[] { record });
}

// Reserve inventory for an order
public bool ReserveInventory(
    string code,
    string warehouseCode,
    decimal quantity)
{
    var request = new InventoryRequest
    {
        WarehouseCode = warehouseCode,
        RequestType = InventoryRequestType.Purchase,
        Items = new[]
        {
            new InventoryRequestItem
            {
                CatalogEntryCode = code,
                Quantity = quantity,
                WarehouseCode = warehouseCode
            }
        }
    };

    var response = _inventoryService.Request(request);
    return response.IsSuccess;
}</code></pre>

<h3>Working with Warehouses</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Get all active warehouses
public IEnumerable&lt;IWarehouse&gt; GetActiveWarehouses()
{
    return _warehouseRepository.List()
        .Where(w => w.IsActive);
}

// Get fulfilment centres
public IEnumerable&lt;IWarehouse&gt; GetFulfilmentCentres()
{
    return _warehouseRepository.List()
        .Where(w => w.IsActive && w.IsFulfillmentCenter);
}

// Get pickup locations
public IEnumerable&lt;IWarehouse&gt; GetPickupLocations()
{
    return _warehouseRepository.List()
        .Where(w => w.IsActive && w.IsPickupLocation);
}</code></pre>

<h3>Displaying Stock Status</h3>
<p>Example Razor component for stock display:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>@model InventoryStatus

@switch (Model)
{
    case InventoryStatus.InStock:
        &lt;span class=""text-green-600""&gt;
            ✓ In Stock
        &lt;/span&gt;
        break;
    case InventoryStatus.LowStock:
        &lt;span class=""text-orange-600""&gt;
            ⚠ Low Stock - Order Soon
        &lt;/span&gt;
        break;
    case InventoryStatus.OutOfStock:
        &lt;span class=""text-red-600""&gt;
            ✗ Out of Stock
        &lt;/span&gt;
        break;
    case InventoryStatus.Backorder:
        &lt;span class=""text-blue-600""&gt;
            📦 Available on Backorder
        &lt;/span&gt;
        break;
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pi-tiered-pricing",
                    ModuleId = "pricing-inventory",
                    Title = "Tiered and Volume Pricing",
                    Summary = "Implement quantity-based pricing for wholesale and bulk discounts.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Set up tiered pricing structures",
                        "Display volume discounts to customers",
                        "Calculate savings for bulk orders",
                        "Handle B2B pricing scenarios"
                    },
                    Content = @"
<h2>Tiered and Volume Pricing</h2>
<p>Commerce Connect supports tiered pricing where unit prices decrease as order quantities increase. This is essential for B2B scenarios and bulk discount programs.</p>

<h3>How Tiered Pricing Works</h3>
<p>Prices are defined with a <code>MinQuantity</code> threshold. The system selects the price where the order quantity meets or exceeds the threshold:</p>

<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-bold"">Example Tier Structure</h4>
    <table class=""mt-2 w-full"">
        <tr><td>1-9 units:</td><td class=""text-right"">£10.00 each</td></tr>
        <tr><td>10-49 units:</td><td class=""text-right"">£9.00 each</td></tr>
        <tr><td>50-99 units:</td><td class=""text-right"">£8.00 each</td></tr>
        <tr><td>100+ units:</td><td class=""text-right"">£7.00 each</td></tr>
    </table>
</div>

<h3>Setting Up Tiered Prices</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public void SetupTieredPricing(
    string code,
    MarketId market,
    Currency currency)
{
    var tiers = new[]
    {
        new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = market,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = 0,
            UnitPrice = new Money(10.00m, currency),
            ValidFrom = DateTime.UtcNow
        },
        new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = market,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = 10,
            UnitPrice = new Money(9.00m, currency),
            ValidFrom = DateTime.UtcNow
        },
        new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = market,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = 50,
            UnitPrice = new Money(8.00m, currency),
            ValidFrom = DateTime.UtcNow
        },
        new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = market,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = 100,
            UnitPrice = new Money(7.00m, currency),
            ValidFrom = DateTime.UtcNow
        }
    };

    _priceDetailService.Save(tiers);
}</code></pre>

<h3>Retrieving Tiered Prices</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class TieredPricingService
{
    private readonly IPriceService _priceService;
    private readonly ICurrentMarket _currentMarket;

    // Get all price tiers for display
    public IEnumerable&lt;PriceTier&gt; GetPriceTiers(string code)
    {
        var market = _currentMarket.GetCurrentMarket();
        var currency = market.DefaultCurrency;

        var filter = new PriceFilter
        {
            Currencies = new[] { currency },
            CustomerPricing = new[] { CustomerPricing.AllCustomers },
            ReturnCustomerPricing = false
        };

        var prices = _priceService.GetPrices(
            market.MarketId,
            DateTime.UtcNow,
            new CatalogKey(code),
            filter);

        return prices
            .OrderBy(p => p.MinQuantity)
            .Select(p => new PriceTier
            {
                MinQuantity = p.MinQuantity,
                UnitPrice = p.UnitPrice,
                NextTierQuantity = GetNextTierQuantity(prices, p)
            });
    }

    // Get price for specific quantity
    public Money? GetPriceForQuantity(string code, decimal quantity)
    {
        var tiers = GetPriceTiers(code).ToList();

        var applicableTier = tiers
            .Where(t => t.MinQuantity <= quantity)
            .OrderByDescending(t => t.MinQuantity)
            .FirstOrDefault();

        return applicableTier?.UnitPrice;
    }

    // Calculate savings at each tier
    public IEnumerable&lt;TierSavings&gt; CalculateTierSavings(string code)
    {
        var tiers = GetPriceTiers(code).ToList();
        var basePrice = tiers.FirstOrDefault()?.UnitPrice;

        if (basePrice == null)
            yield break;

        foreach (var tier in tiers.Skip(1))
        {
            var savingsPercent = (1 - tier.UnitPrice.Amount /
                basePrice.Value.Amount) * 100;

            yield return new TierSavings
            {
                MinQuantity = tier.MinQuantity,
                UnitPrice = tier.UnitPrice,
                SavingsPercent = (int)savingsPercent,
                SavingsPerUnit = new Money(
                    basePrice.Value.Amount - tier.UnitPrice.Amount,
                    basePrice.Value.Currency)
            };
        }
    }
}

public class PriceTier
{
    public decimal MinQuantity { get; set; }
    public Money UnitPrice { get; set; }
    public decimal? NextTierQuantity { get; set; }
}

public class TierSavings
{
    public decimal MinQuantity { get; set; }
    public Money UnitPrice { get; set; }
    public int SavingsPercent { get; set; }
    public Money SavingsPerUnit { get; set; }
}</code></pre>

<h3>Displaying Tier Pricing</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>&lt;div class=""tier-pricing bg-gray-50 p-4 rounded-lg""&gt;
    &lt;h4 class=""font-bold mb-2""&gt;Volume Discounts&lt;/h4&gt;
    &lt;table class=""w-full""&gt;
        &lt;thead&gt;
            &lt;tr&gt;
                &lt;th class=""text-left""&gt;Quantity&lt;/th&gt;
                &lt;th class=""text-right""&gt;Unit Price&lt;/th&gt;
                &lt;th class=""text-right""&gt;Savings&lt;/th&gt;
            &lt;/tr&gt;
        &lt;/thead&gt;
        &lt;tbody&gt;
            @foreach (var tier in Model.Tiers)
            {
                &lt;tr&gt;
                    &lt;td&gt;@tier.MinQuantity+&lt;/td&gt;
                    &lt;td class=""text-right""&gt;@tier.UnitPrice&lt;/td&gt;
                    &lt;td class=""text-right text-green-600""&gt;
                        @if (tier.SavingsPercent > 0)
                        {
                            &lt;span&gt;Save @tier.SavingsPercent%&lt;/span&gt;
                        }
                    &lt;/td&gt;
                &lt;/tr&gt;
            }
        &lt;/tbody&gt;
    &lt;/table&gt;
&lt;/div&gt;</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "pi-customer-pricing",
                    ModuleId = "pricing-inventory",
                    Title = "Customer-Specific Pricing",
                    Summary = "Implement pricing for customer groups and individual customers.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure customer price groups",
                        "Set prices for specific customer groups",
                        "Handle individual customer pricing",
                        "Apply customer context to price lookups"
                    },
                    Content = @"
<h2>Customer-Specific Pricing</h2>
<p>Commerce Connect supports different prices for different customers, enabling trade pricing, loyalty programs, and negotiated contracts.</p>

<h3>Customer Pricing Levels</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Level</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">All Customers</td><td class=""px-4 py-2"">Default/public prices</td><td class=""px-4 py-2"">Website visitors</td></tr>
        <tr><td class=""px-4 py-2"">Price Group</td><td class=""px-4 py-2"">Category-based pricing</td><td class=""px-4 py-2"">Trade, VIP, Wholesale</td></tr>
        <tr><td class=""px-4 py-2"">Individual</td><td class=""px-4 py-2"">Negotiated contracts</td><td class=""px-4 py-2"">Specific customer account</td></tr>
    </tbody>
</table>

<h3>Setting Up Customer Groups</h3>
<p>Customer groups are defined in Commerce Manager and assigned to customer contacts:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Assign customer to a price group
public void AssignCustomerToGroup(
    CustomerContact customer,
    string groupName)
{
    // EffectiveCustomerGroup determines pricing
    customer.CustomerGroup = groupName;
    customer.SaveChanges();
}

// Get customer's effective pricing group
public string GetCustomerPriceGroup(CustomerContact customer)
{
    return customer.EffectiveCustomerGroup;
}</code></pre>

<h3>Setting Group Prices</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public void SetGroupPrice(
    string code,
    MarketId market,
    Currency currency,
    string groupName,
    decimal amount)
{
    var priceValue = new PriceDetailValue
    {
        CatalogKey = new CatalogKey(code),
        MarketId = market,
        CustomerPricing = new CustomerPricing(
            CustomerPricing.PriceType.PriceGroup,
            groupName),
        MinQuantity = 0,
        UnitPrice = new Money(amount, currency),
        ValidFrom = DateTime.UtcNow
    };

    _priceDetailService.Save(new[] { priceValue });
}

// Set trade prices for all products
public void SetTradePrice(
    string code,
    decimal tradeDiscount = 0.15m) // 15% trade discount
{
    var market = _currentMarket.GetCurrentMarket();
    var currency = market.DefaultCurrency;

    // Get retail price
    var retailPrice = GetRetailPrice(code);
    if (retailPrice == null) return;

    var tradeAmount = retailPrice.Value.Amount * (1 - tradeDiscount);

    SetGroupPrice(
        code,
        market.MarketId,
        currency,
        ""Trade"",
        tradeAmount);
}</code></pre>

<h3>Retrieving Customer-Specific Prices</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CustomerPricingService
{
    private readonly IPriceService _priceService;
    private readonly ICurrentMarket _currentMarket;
    private readonly CustomerContext _customerContext;

    // Get price for current customer
    public Money? GetPriceForCurrentCustomer(string code)
    {
        var market = _currentMarket.GetCurrentMarket();
        var currency = market.DefaultCurrency;

        // Build customer pricing context
        var customerPricings = new List&lt;CustomerPricing&gt;
        {
            CustomerPricing.AllCustomers
        };

        var customer = _customerContext.CurrentContact;
        if (customer != null)
        {
            // Add individual pricing
            customerPricings.Add(new CustomerPricing(
                CustomerPricing.PriceType.UserName,
                customer.UserId));

            // Add group pricing
            var group = customer.EffectiveCustomerGroup;
            if (!string.IsNullOrEmpty(group))
            {
                customerPricings.Add(new CustomerPricing(
                    CustomerPricing.PriceType.PriceGroup,
                    group));
            }
        }

        var filter = new PriceFilter
        {
            Currencies = new[] { currency },
            CustomerPricing = customerPricings.ToArray(),
            Quantity = 1,
            ReturnCustomerPricing = true
        };

        var prices = _priceService.GetPrices(
            market.MarketId,
            DateTime.UtcNow,
            new CatalogKey(code),
            filter);

        // Return lowest matching price
        return prices
            .OrderBy(p => p.UnitPrice.Amount)
            .FirstOrDefault()?.UnitPrice;
    }

    // Check if customer has special pricing
    public bool HasSpecialPricing(string code)
    {
        var customerPrice = GetPriceForCurrentCustomer(code);
        var publicPrice = GetPublicPrice(code);

        if (customerPrice == null || publicPrice == null)
            return false;

        return customerPrice.Value.Amount < publicPrice.Value.Amount;
    }
}</code></pre>

<h3>Displaying Customer Pricing</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>@if (Model.HasSpecialPricing)
{
    &lt;div class=""price-display""&gt;
        &lt;span class=""text-gray-500 line-through""&gt;
            @Model.PublicPrice
        &lt;/span&gt;
        &lt;span class=""text-green-600 font-bold ml-2""&gt;
            @Model.YourPrice
        &lt;/span&gt;
        &lt;span class=""bg-green-100 text-green-800 text-xs px-2 py-1 rounded ml-2""&gt;
            Your Trade Price
        &lt;/span&gt;
    &lt;/div&gt;
}
else
{
    &lt;div class=""price-display""&gt;
        &lt;span class=""font-bold""&gt;@Model.PublicPrice&lt;/span&gt;
    &lt;/div&gt;
}</code></pre>

<h3>Common Customer Groups</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <ul class=""space-y-2"">
        <li><strong>Trade/Wholesale</strong> - B2B customers with trade discounts</li>
        <li><strong>VIP/Gold/Platinum</strong> - Loyalty tiers</li>
        <li><strong>Staff</strong> - Employee discounts</li>
        <li><strong>Partner</strong> - Business partners</li>
        <li><strong>Contract</strong> - Negotiated pricing agreements</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 5: Orders & Checkout

    private LearningModule BuildOrdersCheckoutModule()
    {
        return new LearningModule
        {
            Id = "orders-checkout",
            Title = "Orders & Checkout",
            Description = "Master the order system, shopping cart management, checkout process, and order fulfilment.",
            Icon = "shopping-bag",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "oc-order-system-overview",
                    ModuleId = "orders-checkout",
                    Title = "Order System Overview",
                    Summary = "Understand the order data model, order types, and the order lifecycle.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the order data model hierarchy",
                        "Know the different order types (Cart, PurchaseOrder, PaymentPlan)",
                        "Learn the order lifecycle stages",
                        "Understand order forms, shipments, and payments"
                    },
                    Content = @"
<h2>Order System Overview</h2>
<p>Commerce Connect's order system handles everything from shopping carts to completed purchases. Understanding this system is essential for building robust e-commerce functionality.</p>

<h3>Order Data Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
IOrderGroup (Base Interface)
├── ICart              → Shopping cart (not yet purchased)
├── IPurchaseOrder     → Completed order
└── IPaymentPlan       → Subscription/recurring order

IOrderGroup contains:
├── IOrderForm[]       → Collection of order forms
│   ├── IShipment[]   → Shipments within the form
│   │   ├── ILineItem[] → Items in the shipment
│   │   └── IOrderAddress → Shipping address
│   └── IPayment[]    → Payments for the form
├── IOrderAddress[]    → Address book
└── Properties         → Custom order properties
</pre>

<h3>Order Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Interface</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Cart</td><td class=""px-4 py-2 font-mono"">ICart</td><td class=""px-4 py-2"">Shopping cart before checkout</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Purchase Order</td><td class=""px-4 py-2 font-mono"">IPurchaseOrder</td><td class=""px-4 py-2"">Completed purchase</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Payment Plan</td><td class=""px-4 py-2 font-mono"">IPaymentPlan</td><td class=""px-4 py-2"">Recurring/subscription orders</td></tr>
    </tbody>
</table>

<h3>Order Components</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">IOrderForm</td><td class=""px-4 py-2"">Container for shipments and payments within an order</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IShipment</td><td class=""px-4 py-2"">Group of items shipping together</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ILineItem</td><td class=""px-4 py-2"">Individual product in the order</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IPayment</td><td class=""px-4 py-2"">Payment method and transaction details</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IOrderAddress</td><td class=""px-4 py-2"">Shipping or billing address</td></tr>
    </tbody>
</table>

<h3>Order Lifecycle</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
┌──────────────────────────────────────────────────────────────┐
│                    ORDER LIFECYCLE                            │
├──────────────────────────────────────────────────────────────┤
│                                                              │
│  ┌─────────┐    ┌──────────┐    ┌────────────────┐          │
│  │  Cart   │ →  │ Checkout │ →  │ Purchase Order │          │
│  │ (ICart) │    │ Process  │    │(IPurchaseOrder)│          │
│  └─────────┘    └──────────┘    └────────────────┘          │
│       │              │                   │                   │
│       ▼              ▼                   ▼                   │
│  • Add items    • Validate         • Payment processing      │
│  • Update qty   • Set address      • Inventory reservation   │
│  • Apply promo  • Select shipping  • Order confirmation      │
│                 • Add payment      • Fulfilment              │
│                                    • Shipping                │
│                                    • Completion              │
│                                                              │
└──────────────────────────────────────────────────────────────┘
</pre>

<h3>Key Order Services</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Service</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">IOrderRepository</td><td class=""px-4 py-2"">Load and save orders</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IOrderGroupFactory</td><td class=""px-4 py-2"">Create new orders, shipments, line items</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IOrderGroupCalculator</td><td class=""px-4 py-2"">Calculate totals and taxes</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IPromotionEngine</td><td class=""px-4 py-2"">Apply discounts and promotions</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IPaymentProcessor</td><td class=""px-4 py-2"">Process payment transactions</td></tr>
    </tbody>
</table>

<h3>Namespaces</h3>
<ul>
    <li><code>EPiServer.Commerce.Order</code> - Core order interfaces and services</li>
    <li><code>Mediachase.Commerce.Orders</code> - Order implementation classes</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "oc-shopping-cart",
                    ModuleId = "orders-checkout",
                    Title = "Shopping Cart Management",
                    Summary = "Learn to create and manage shopping carts, add items, and update quantities.",
                    Order = 2,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Create and retrieve shopping carts",
                        "Add, update, and remove cart items",
                        "Handle multiple shipments",
                        "Calculate cart totals"
                    },
                    Content = @"
<h2>Shopping Cart Management</h2>
<p>The shopping cart is the central component of the e-commerce experience. Commerce Connect provides a flexible cart system that supports complex scenarios.</p>

<h3>Cart Service Example</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CartService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderGroupFactory _orderGroupFactory;
    private readonly ICurrentMarket _currentMarket;
    private readonly ReferenceConverter _referenceConverter;
    private readonly IContentLoader _contentLoader;

    private const string DefaultCartName = ""Default"";

    public CartService(
        IOrderRepository orderRepository,
        IOrderGroupFactory orderGroupFactory,
        ICurrentMarket currentMarket,
        ReferenceConverter referenceConverter,
        IContentLoader contentLoader)
    {
        _orderRepository = orderRepository;
        _orderGroupFactory = orderGroupFactory;
        _currentMarket = currentMarket;
        _referenceConverter = referenceConverter;
        _contentLoader = contentLoader;
    }

    // Get or create cart for current customer
    public ICart GetOrCreateCart(Guid customerId)
    {
        var cart = _orderRepository.Load&lt;ICart&gt;(
            customerId,
            DefaultCartName).FirstOrDefault();

        if (cart == null)
        {
            var market = _currentMarket.GetCurrentMarket();
            cart = _orderGroupFactory.CreateCart(
                customerId,
                DefaultCartName,
                market.MarketId,
                market.DefaultCurrency);
        }

        return cart;
    }

    // Add item to cart
    public ILineItem AddToCart(
        ICart cart,
        string code,
        decimal quantity)
    {
        // Get the first shipment or create one
        var shipment = cart.GetFirstShipment();
        if (shipment == null)
        {
            shipment = _orderGroupFactory.CreateShipment(cart);
            cart.GetFirstForm().Shipments.Add(shipment);
        }

        // Check if item already exists
        var existingItem = shipment.LineItems
            .FirstOrDefault(li => li.Code == code);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            return existingItem;
        }

        // Create new line item
        var lineItem = _orderGroupFactory.CreateLineItem(code, cart);
        lineItem.Quantity = quantity;

        // Get product info
        var variant = GetVariant(code);
        if (variant != null)
        {
            lineItem.DisplayName = variant.DisplayName;
        }

        shipment.LineItems.Add(lineItem);

        return lineItem;
    }

    // Update quantity
    public void UpdateQuantity(
        ICart cart,
        string code,
        decimal newQuantity)
    {
        var lineItem = cart.GetAllLineItems()
            .FirstOrDefault(li => li.Code == code);

        if (lineItem == null) return;

        if (newQuantity <= 0)
        {
            RemoveFromCart(cart, code);
        }
        else
        {
            lineItem.Quantity = newQuantity;
        }
    }

    // Remove item from cart
    public void RemoveFromCart(ICart cart, string code)
    {
        var shipment = cart.GetFirstShipment();
        var lineItem = shipment?.LineItems
            .FirstOrDefault(li => li.Code == code);

        if (lineItem != null)
        {
            shipment.LineItems.Remove(lineItem);
        }
    }

    // Save cart
    public void SaveCart(ICart cart)
    {
        _orderRepository.Save(cart);
    }

    private VariationContent GetVariant(string code)
    {
        var contentLink = _referenceConverter.GetContentLink(code);
        return _contentLoader.Get&lt;VariationContent&gt;(contentLink);
    }
}</code></pre>

<h3>Cart Calculations</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CartCalculationService
{
    private readonly IOrderGroupCalculator _calculator;
    private readonly IPromotionEngine _promotionEngine;

    // Validate and calculate cart
    public CartSummary CalculateCart(ICart cart)
    {
        // Apply promotions
        var rewards = _promotionEngine.Run(cart);

        // Calculate totals
        var totals = _calculator.GetOrderGroupTotals(cart);

        return new CartSummary
        {
            SubTotal = totals.SubTotal,
            ShippingTotal = totals.ShippingTotal,
            TaxTotal = totals.TaxTotal,
            DiscountTotal = totals.OrderDiscountTotal +
                totals.ShippingDiscountTotal,
            Total = totals.Total,
            ItemCount = cart.GetAllLineItems()
                .Sum(li => (int)li.Quantity),
            AppliedPromotions = rewards
                .Where(r => r.Status == FulfillmentStatus.Fulfilled)
                .Select(r => r.Promotion.Name)
                .ToList()
        };
    }
}

public class CartSummary
{
    public Money SubTotal { get; set; }
    public Money ShippingTotal { get; set; }
    public Money TaxTotal { get; set; }
    public Money DiscountTotal { get; set; }
    public Money Total { get; set; }
    public int ItemCount { get; set; }
    public List&lt;string&gt; AppliedPromotions { get; set; }
}</code></pre>

<h3>Working with Multiple Shipments</h3>
<p>Commerce Connect supports split shipments for items going to different addresses:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Create additional shipment
public IShipment CreateNewShipment(ICart cart)
{
    var shipment = _orderGroupFactory.CreateShipment(cart);
    cart.GetFirstForm().Shipments.Add(shipment);
    return shipment;
}

// Move item to different shipment
public void MoveItemToShipment(
    ICart cart,
    string code,
    IShipment targetShipment)
{
    // Find and remove from current shipment
    foreach (var shipment in cart.GetFirstForm().Shipments)
    {
        var lineItem = shipment.LineItems
            .FirstOrDefault(li => li.Code == code);

        if (lineItem != null)
        {
            shipment.LineItems.Remove(lineItem);
            targetShipment.LineItems.Add(lineItem);
            break;
        }
    }
}</code></pre>

<h3>Cart Extension Methods</h3>
<p>Commerce Connect provides useful extension methods:</p>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Get all line items across all shipments
var allItems = cart.GetAllLineItems();

// Get first form
var form = cart.GetFirstForm();

// Get first shipment
var shipment = cart.GetFirstShipment();

// Get total quantity
var totalQty = cart.GetAllLineItems().Sum(li => li.Quantity);</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Title = "Cart Controller",
                            Description = "Example cart controller for API endpoints",
                            ExampleContent = @"[ApiController]
[Route(""api/[controller]"")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;
    private readonly CartCalculationService _calculationService;
    private readonly CustomerContext _customerContext;

    [HttpPost(""add"")]
    public IActionResult AddToCart([FromBody] AddToCartRequest request)
    {
        var customerId = _customerContext.CurrentContactId;
        var cart = _cartService.GetOrCreateCart(customerId);

        _cartService.AddToCart(cart, request.Code, request.Quantity);
        _cartService.SaveCart(cart);

        var summary = _calculationService.CalculateCart(cart);

        return Ok(new
        {
            Success = true,
            Cart = summary
        });
    }

    [HttpPut(""update"")]
    public IActionResult UpdateQuantity([FromBody] UpdateQuantityRequest request)
    {
        var customerId = _customerContext.CurrentContactId;
        var cart = _cartService.GetOrCreateCart(customerId);

        _cartService.UpdateQuantity(cart, request.Code, request.Quantity);
        _cartService.SaveCart(cart);

        var summary = _calculationService.CalculateCart(cart);

        return Ok(summary);
    }

    [HttpDelete(""remove/{code}"")]
    public IActionResult RemoveFromCart(string code)
    {
        var customerId = _customerContext.CurrentContactId;
        var cart = _cartService.GetOrCreateCart(customerId);

        _cartService.RemoveFromCart(cart, code);
        _cartService.SaveCart(cart);

        var summary = _calculationService.CalculateCart(cart);

        return Ok(summary);
    }
}",
                            Type = ExampleType.Code
                        }
                    }
                },
                new Lesson
                {
                    Id = "oc-checkout-process",
                    ModuleId = "orders-checkout",
                    Title = "Checkout Process",
                    Summary = "Implement the checkout flow including addresses, shipping, and order creation.",
                    Order = 3,
                    EstimatedMinutes = 25,
                    LearningObjectives = new List<string>
                    {
                        "Implement multi-step checkout",
                        "Handle shipping and billing addresses",
                        "Select shipping methods",
                        "Convert cart to purchase order"
                    },
                    Content = @"
<h2>Checkout Process</h2>
<p>The checkout process converts a shopping cart into a completed purchase order. A typical checkout involves address collection, shipping selection, payment, and order confirmation.</p>

<h3>Checkout Steps</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Step 1: Cart Review
    └── Validate items, check inventory, update prices

Step 2: Addresses
    └── Collect/select billing and shipping addresses

Step 3: Shipping
    └── Select shipping method for each shipment

Step 4: Payment
    └── Collect payment information, validate

Step 5: Review & Place Order
    └── Confirm details, create purchase order
</pre>

<h3>Checkout Service</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CheckoutService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderGroupCalculator _calculator;
    private readonly IPromotionEngine _promotionEngine;
    private readonly IInventoryProcessor _inventoryProcessor;
    private readonly IOrderGroupFactory _orderGroupFactory;

    // Step 1: Validate cart before checkout
    public CartValidationResult ValidateCart(ICart cart)
    {
        var result = new CartValidationResult { IsValid = true };

        foreach (var lineItem in cart.GetAllLineItems())
        {
            // Check inventory
            var isInStock = CheckInventory(lineItem.Code, lineItem.Quantity);
            if (!isInStock)
            {
                result.IsValid = false;
                result.Errors.Add($""{lineItem.DisplayName} is out of stock"");
            }

            // Check price validity
            var currentPrice = GetCurrentPrice(lineItem.Code);
            if (currentPrice == null)
            {
                result.IsValid = false;
                result.Errors.Add($""{lineItem.DisplayName} is no longer available"");
            }
        }

        return result;
    }

    // Step 2: Set addresses
    public void SetAddresses(
        ICart cart,
        AddressModel billingAddress,
        AddressModel shippingAddress)
    {
        // Create billing address
        var billing = CreateOrderAddress(cart, billingAddress);
        cart.GetFirstForm().BillingAddressId = billing.Id;

        // Set shipping address for each shipment
        var shipping = CreateOrderAddress(cart, shippingAddress);
        foreach (var shipment in cart.GetFirstForm().Shipments)
        {
            shipment.ShippingAddress = shipping;
        }

        _orderRepository.Save(cart);
    }

    private IOrderAddress CreateOrderAddress(
        ICart cart,
        AddressModel model)
    {
        var address = _orderGroupFactory.CreateOrderAddress(cart);

        address.Id = Guid.NewGuid().ToString();
        address.FirstName = model.FirstName;
        address.LastName = model.LastName;
        address.Line1 = model.Line1;
        address.Line2 = model.Line2;
        address.City = model.City;
        address.RegionCode = model.Region;
        address.PostalCode = model.PostalCode;
        address.CountryCode = model.CountryCode;
        address.Email = model.Email;
        address.DaytimePhoneNumber = model.Phone;

        cart.GetFirstForm().Shipments
            .First().ShippingAddress = address;

        return address;
    }

    // Step 3: Set shipping method
    public void SetShippingMethod(
        ICart cart,
        Guid shippingMethodId)
    {
        var shipment = cart.GetFirstShipment();
        shipment.ShippingMethodId = shippingMethodId;

        // Recalculate totals
        _calculator.GetOrderGroupTotals(cart);
        _orderRepository.Save(cart);
    }

    // Step 5: Place order
    public IPurchaseOrder PlaceOrder(ICart cart)
    {
        // Final validation
        var validationResult = ValidateCart(cart);
        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException(
                string.Join("", "", validationResult.Errors));
        }

        // Apply final promotions
        _promotionEngine.Run(cart);

        // Calculate final totals
        _calculator.GetOrderGroupTotals(cart);

        // Reserve inventory
        var inventoryRequests = cart.GetAllLineItems()
            .Select(li => new InventoryRequest
            {
                WarehouseCode = ""default"",
                RequestType = InventoryRequestType.Purchase,
                Items = new[]
                {
                    new InventoryRequestItem
                    {
                        CatalogEntryCode = li.Code,
                        Quantity = li.Quantity
                    }
                }
            });

        // Create purchase order
        var orderReference = _orderRepository.SaveAsPurchaseOrder(cart);
        var purchaseOrder = _orderRepository.Load&lt;IPurchaseOrder&gt;(
            orderReference.OrderGroupId);

        // Set initial status
        purchaseOrder.OrderStatus = OrderStatus.InProgress;
        _orderRepository.Save(purchaseOrder);

        // Delete cart
        _orderRepository.Delete(cart.OrderLink);

        return purchaseOrder;
    }
}</code></pre>

<h3>Address Model</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class AddressModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Line1 { get; set; }

    public string Line2 { get; set; }

    [Required]
    public string City { get; set; }

    public string Region { get; set; }

    [Required]
    public string PostalCode { get; set; }

    [Required]
    public string CountryCode { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Phone { get; set; }
}</code></pre>

<h3>Shipping Method Selection</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class ShippingService
{
    private readonly IShippingCalculator _shippingCalculator;

    public IEnumerable&lt;ShippingMethodViewModel&gt; GetShippingMethods(
        ICart cart)
    {
        var shipment = cart.GetFirstShipment();
        var methods = ShippingManager.GetShippingMethods(
            cart.Currency,
            shipment.ShippingAddress?.CountryCode);

        foreach (var method in methods.ShippingMethod)
        {
            var rate = _shippingCalculator.GetRate(
                shipment,
                method,
                cart.MarketId);

            yield return new ShippingMethodViewModel
            {
                Id = method.ShippingMethodId,
                Name = method.DisplayName,
                Description = method.Description,
                Price = rate?.Money ?? new Money(0, cart.Currency)
            };
        }
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "oc-order-processing",
                    ModuleId = "orders-checkout",
                    Title = "Order Processing",
                    Summary = "Learn to process orders through fulfilment, shipping, and completion.",
                    Order = 4,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Process payments on orders",
                        "Create and manage shipments",
                        "Update order and shipment status",
                        "Handle order completion"
                    },
                    Content = @"
<h2>Order Processing</h2>
<p>Once a purchase order is created, it needs to be processed through payment, fulfilment, and shipping stages.</p>

<h3>Order Status Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
InProgress → AwaitingExchange → PartiallyShipped → Completed
                   ↓
             OnHold / Cancelled
</pre>

<h3>Processing Payments</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class PaymentProcessingService
{
    private readonly IOrderRepository _orderRepository;

    // Process payments on an order
    public PaymentProcessingResult ProcessPayments(
        IPurchaseOrder order)
    {
        var result = new PaymentProcessingResult();

        foreach (var payment in order.GetFirstForm().Payments)
        {
            try
            {
                // Process through payment gateway
                var processed = order.ProcessPayments();

                if (payment.TransactionType ==
                    TransactionType.Authorization.ToString())
                {
                    payment.Status = PaymentStatus.Processed.ToString();
                    result.AuthorizedAmount += payment.Amount;
                }
                else if (payment.TransactionType ==
                    TransactionType.Capture.ToString())
                {
                    payment.Status = PaymentStatus.Processed.ToString();
                    result.CapturedAmount += payment.Amount;
                }

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                payment.Status = PaymentStatus.Failed.ToString();
                result.Errors.Add(ex.Message);
            }
        }

        _orderRepository.Save(order);
        return result;
    }

    // Capture authorized payment
    public void CapturePayment(
        IPurchaseOrder order,
        decimal amount)
    {
        var payment = order.GetFirstForm().Payments
            .FirstOrDefault(p =>
                p.TransactionType == TransactionType.Authorization.ToString() &&
                p.Status == PaymentStatus.Processed.ToString());

        if (payment != null)
        {
            payment.TransactionType = TransactionType.Capture.ToString();
            payment.Amount = amount;

            order.ProcessPayments();
            _orderRepository.Save(order);
        }
    }
}</code></pre>

<h3>Shipment Processing</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class ShipmentProcessingService
{
    private readonly IOrderRepository _orderRepository;

    // Create shipment release
    public void ReleaseShipment(
        IPurchaseOrder order,
        IShipment shipment)
    {
        // Update status
        var orderShipmentStatus = new OrderShipmentStatus();
        orderShipmentStatus.Status = ShipmentStatus.Released;

        shipment.OrderShipmentStatus = orderShipmentStatus.Status;

        _orderRepository.Save(order);
    }

    // Add tracking information
    public void AddTrackingInfo(
        IPurchaseOrder order,
        IShipment shipment,
        string trackingNumber,
        string carrier)
    {
        // Store tracking in shipment properties
        shipment.Properties[""TrackingNumber""] = trackingNumber;
        shipment.Properties[""Carrier""] = carrier;

        _orderRepository.Save(order);
    }

    // Mark as shipped
    public void MarkAsShipped(
        IPurchaseOrder order,
        IShipment shipment)
    {
        shipment.OrderShipmentStatus = ShipmentStatus.Shipped;
        shipment.Properties[""ShippedDate""] = DateTime.UtcNow;

        // Check if all shipments are shipped
        var allShipped = order.GetFirstForm().Shipments
            .All(s => s.OrderShipmentStatus == ShipmentStatus.Shipped);

        if (allShipped)
        {
            order.OrderStatus = OrderStatus.Completed;
        }
        else
        {
            order.OrderStatus = OrderStatus.PartiallyShipped;
        }

        _orderRepository.Save(order);
    }
}

public enum ShipmentStatus
{
    AwaitingInventory,
    Released,
    Picking,
    Packing,
    Shipped,
    Delivered
}</code></pre>

<h3>Order Completion</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class OrderCompletionService
{
    private readonly IOrderRepository _orderRepository;

    // Complete order
    public void CompleteOrder(IPurchaseOrder order)
    {
        // Verify all payments captured
        var payments = order.GetFirstForm().Payments;
        var allCaptured = payments.All(p =>
            p.TransactionType == TransactionType.Capture.ToString() &&
            p.Status == PaymentStatus.Processed.ToString());

        if (!allCaptured)
        {
            throw new InvalidOperationException(
                ""All payments must be captured before completing"");
        }

        // Verify all shipped
        var allShipped = order.GetFirstForm().Shipments
            .All(s => s.OrderShipmentStatus == ShipmentStatus.Shipped);

        if (!allShipped)
        {
            throw new InvalidOperationException(
                ""All shipments must be shipped before completing"");
        }

        order.OrderStatus = OrderStatus.Completed;
        order.Properties[""CompletedDate""] = DateTime.UtcNow;

        _orderRepository.Save(order);
    }

    // Cancel order
    public void CancelOrder(
        IPurchaseOrder order,
        string reason)
    {
        // Void/refund payments
        foreach (var payment in order.GetFirstForm().Payments)
        {
            if (payment.Status == PaymentStatus.Processed.ToString())
            {
                // Handle refund logic
            }
        }

        // Release inventory reservations
        ReleaseInventory(order);

        order.OrderStatus = OrderStatus.Cancelled;
        order.Properties[""CancellationReason""] = reason;
        order.Properties[""CancelledDate""] = DateTime.UtcNow;

        _orderRepository.Save(order);
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "oc-order-search",
                    ModuleId = "orders-checkout",
                    Title = "Searching and Querying Orders",
                    Summary = "Learn to search and retrieve orders using various criteria.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Search orders by customer",
                        "Query orders by status and date",
                        "Implement order history pages",
                        "Use order search for reporting"
                    },
                    Content = @"
<h2>Searching and Querying Orders</h2>
<p>Commerce Connect provides several ways to search and retrieve orders for customer accounts, reporting, and administration.</p>

<h3>Loading Orders by Customer</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class OrderHistoryService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderSearchService _orderSearchService;

    // Get orders for a customer
    public IEnumerable&lt;IPurchaseOrder&gt; GetCustomerOrders(
        Guid customerId,
        int maxResults = 20)
    {
        return _orderRepository.Load&lt;IPurchaseOrder&gt;(customerId)
            .OrderByDescending(o => o.Created)
            .Take(maxResults);
    }

    // Get recent orders
    public IEnumerable&lt;IPurchaseOrder&gt; GetRecentOrders(
        Guid customerId,
        int days = 30)
    {
        var cutoff = DateTime.UtcNow.AddDays(-days);

        return _orderRepository.Load&lt;IPurchaseOrder&gt;(customerId)
            .Where(o => o.Created >= cutoff)
            .OrderByDescending(o => o.Created);
    }

    // Get order by order number
    public IPurchaseOrder GetByOrderNumber(string orderNumber)
    {
        var orderGroupId = int.Parse(orderNumber);
        return _orderRepository.Load&lt;IPurchaseOrder&gt;(orderGroupId);
    }
}</code></pre>

<h3>Advanced Order Search</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class OrderSearchService
{
    // Search orders with criteria
    public OrderSearchResults SearchOrders(OrderSearchCriteria criteria)
    {
        var query = OrderContext.Current.FindPurchaseOrders()
            .OrderByDescending(o => o.Created);

        // Filter by status
        if (criteria.Status.HasValue)
        {
            query = query.Where(o =>
                o.OrderStatus == criteria.Status.Value);
        }

        // Filter by date range
        if (criteria.FromDate.HasValue)
        {
            query = query.Where(o =>
                o.Created >= criteria.FromDate.Value);
        }

        if (criteria.ToDate.HasValue)
        {
            query = query.Where(o =>
                o.Created <= criteria.ToDate.Value);
        }

        // Filter by market
        if (!string.IsNullOrEmpty(criteria.MarketId))
        {
            query = query.Where(o =>
                o.MarketId == new MarketId(criteria.MarketId));
        }

        // Execute with paging
        var totalCount = query.Count();
        var orders = query
            .Skip(criteria.Skip)
            .Take(criteria.Take)
            .ToList();

        return new OrderSearchResults
        {
            Orders = orders,
            TotalCount = totalCount,
            Page = criteria.Skip / criteria.Take + 1,
            PageSize = criteria.Take
        };
    }
}

public class OrderSearchCriteria
{
    public OrderStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string MarketId { get; set; }
    public string CustomerEmail { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 20;
}

public class OrderSearchResults
{
    public IEnumerable&lt;IPurchaseOrder&gt; Orders { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}</code></pre>

<h3>Order History View Model</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class OrderSummaryViewModel
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public Money Total { get; set; }
    public int ItemCount { get; set; }
    public string TrackingNumber { get; set; }
    public IEnumerable&lt;OrderLineViewModel&gt; Lines { get; set; }
}

public static class OrderExtensions
{
    public static OrderSummaryViewModel ToSummaryViewModel(
        this IPurchaseOrder order)
    {
        return new OrderSummaryViewModel
        {
            OrderNumber = order.OrderNumber,
            OrderDate = order.Created.Value,
            Status = order.OrderStatus.ToString(),
            Total = new Money(
                order.GetTotal(),
                order.Currency),
            ItemCount = order.GetAllLineItems().Count(),
            TrackingNumber = order.GetFirstShipment()
                ?.Properties[""TrackingNumber""]?.ToString(),
            Lines = order.GetAllLineItems()
                .Select(li => new OrderLineViewModel
                {
                    Code = li.Code,
                    Name = li.DisplayName,
                    Quantity = (int)li.Quantity,
                    UnitPrice = new Money(li.PlacedPrice, order.Currency)
                })
        };
    }
}</code></pre>

<h3>Customer Order History Page</h3>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>&lt;h2 class=""text-2xl font-bold mb-4""&gt;Order History&lt;/h2&gt;

@if (!Model.Orders.Any())
{
    &lt;p class=""text-gray-500""&gt;You haven't placed any orders yet.&lt;/p&gt;
}
else
{
    &lt;div class=""space-y-4""&gt;
        @foreach (var order in Model.Orders)
        {
            &lt;div class=""border rounded-lg p-4""&gt;
                &lt;div class=""flex justify-between items-start""&gt;
                    &lt;div&gt;
                        &lt;p class=""font-bold""&gt;Order #@order.OrderNumber&lt;/p&gt;
                        &lt;p class=""text-sm text-gray-500""&gt;
                            @order.OrderDate.ToString(""d MMM yyyy"")
                        &lt;/p&gt;
                    &lt;/div&gt;
                    &lt;div class=""text-right""&gt;
                        &lt;p class=""font-bold""&gt;@order.Total&lt;/p&gt;
                        &lt;span class=""badge""&gt;@order.Status&lt;/span&gt;
                    &lt;/div&gt;
                &lt;/div&gt;
                &lt;div class=""mt-4""&gt;
                    &lt;a href=""/account/orders/@order.OrderNumber""
                       class=""text-blue-600 hover:underline""&gt;
                        View Details
                    &lt;/a&gt;
                &lt;/div&gt;
            &lt;/div&gt;
        }
    &lt;/div&gt;
}</code></pre>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 6: Customers & Organizations

    private LearningModule BuildCustomersOrganizationsModule()
    {
        return new LearningModule
        {
            Id = "customers-organizations",
            Title = "Customers & Organizations",
            Description = "Master customer management, B2B organizations, customer groups, and address handling.",
            Icon = "user-group",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "co-customer-overview",
                    ModuleId = "customers-organizations",
                    Title = "Customer System Overview",
                    Summary = "Understand the customer data model and CustomerContact class.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the CustomerContact class",
                        "Learn how customers relate to ASP.NET Identity",
                        "Know the customer properties and metadata",
                        "Understand customer context"
                    },
                    Content = @"
<h2>Customer System Overview</h2>
<p>Commerce Connect provides a comprehensive customer management system that integrates with ASP.NET Core Identity while adding commerce-specific capabilities.</p>

<h3>Customer Data Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
CustomerContact (Commerce Customer)
├── UserId (links to ASP.NET Identity)
├── Email, FirstName, LastName
├── Customer Group (pricing)
├── Addresses[]
│   ├── Shipping Address
│   └── Billing Address
├── Organization (B2B - optional)
└── Custom Properties (MetaFields)
</pre>

<h3>Key Customer Concepts</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Concept</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">CustomerContact</td><td class=""px-4 py-2"">Main customer entity with commerce data</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">CustomerAddress</td><td class=""px-4 py-2"">Saved shipping/billing addresses</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">CustomerGroup</td><td class=""px-4 py-2"">Price group for tiered pricing</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Organization</td><td class=""px-4 py-2"">B2B company/organization entity</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">CustomerContext</td><td class=""px-4 py-2"">Current customer session helper</td></tr>
    </tbody>
</table>

<h3>CustomerContext Usage</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CustomerService
{
    private readonly CustomerContext _customerContext;

    public CustomerService(CustomerContext customerContext)
    {
        _customerContext = customerContext;
    }

    // Get current logged-in customer
    public CustomerContact GetCurrentCustomer()
    {
        return _customerContext.CurrentContact;
    }

    // Get current customer ID
    public Guid? GetCurrentCustomerId()
    {
        return _customerContext.CurrentContactId;
    }

    // Check if user is logged in
    public bool IsAuthenticated()
    {
        return _customerContext.CurrentContact != null;
    }
}</code></pre>

<h3>CustomerContact Properties</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>CustomerContact customer = _customerContext.CurrentContact;

// Basic info
string email = customer.Email;
string firstName = customer.FirstName;
string lastName = customer.LastName;
string fullName = customer.FullName;

// Commerce properties
string customerGroup = customer.CustomerGroup;
string effectiveGroup = customer.EffectiveCustomerGroup;
Guid primaryKeyId = customer.PrimaryKeyId.Value;

// Registration info
DateTime? registered = customer.RegistrationSource;
DateTime created = customer.Created;
DateTime modified = customer.Modified;</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "co-managing-customers",
                    ModuleId = "customers-organizations",
                    Title = "Managing Customers",
                    Summary = "Learn to create, update, and query customer accounts.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create new customer accounts",
                        "Update customer information",
                        "Search and query customers",
                        "Delete customer accounts"
                    },
                    Content = @"
<h2>Managing Customers</h2>
<p>Learn the essential operations for managing customer accounts in Commerce Connect.</p>

<h3>Creating Customers</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CustomerManagementService
{
    // Create new customer
    public CustomerContact CreateCustomer(
        string email,
        string firstName,
        string lastName,
        string userId = null)
    {
        var customer = CustomerContact.CreateInstance();

        customer.UserId = userId ?? email;
        customer.Email = email;
        customer.FirstName = firstName;
        customer.LastName = lastName;
        customer.FullName = $""{firstName} {lastName}"";
        customer.RegistrationSource = DateTime.UtcNow.ToString();

        customer.SaveChanges();

        return customer;
    }

    // Create customer with organization (B2B)
    public CustomerContact CreateB2BCustomer(
        string email,
        string firstName,
        string lastName,
        Organization organization)
    {
        var customer = CreateCustomer(email, firstName, lastName);

        // Add to organization
        customer.OwnerId = organization.PrimaryKeyId;
        customer.SaveChanges();

        return customer;
    }
}</code></pre>

<h3>Updating Customers</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public void UpdateCustomer(
    CustomerContact customer,
    CustomerUpdateModel model)
{
    customer.FirstName = model.FirstName;
    customer.LastName = model.LastName;
    customer.FullName = $""{model.FirstName} {model.LastName}"";

    if (!string.IsNullOrEmpty(model.Phone))
    {
        customer[""Phone""] = model.Phone;
    }

    if (!string.IsNullOrEmpty(model.CustomerGroup))
    {
        customer.CustomerGroup = model.CustomerGroup;
    }

    customer.SaveChanges();
}

public void SetCustomerGroup(
    CustomerContact customer,
    string groupName)
{
    customer.CustomerGroup = groupName;
    customer.SaveChanges();
}</code></pre>

<h3>Searching Customers</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CustomerSearchService
{
    // Find by email
    public CustomerContact FindByEmail(string email)
    {
        return CustomerContext.Current.GetContactByUserId(email)
            ?? CustomerContext.Current.GetContacts()
                .FirstOrDefault(c => c.Email == email);
    }

    // Find by user ID
    public CustomerContact FindByUserId(string userId)
    {
        return CustomerContext.Current.GetContactByUserId(userId);
    }

    // Search customers
    public IEnumerable&lt;CustomerContact&gt; SearchCustomers(
        string searchTerm,
        int maxResults = 50)
    {
        return CustomerContext.Current.GetContacts()
            .Where(c =>
                c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .Take(maxResults);
    }

    // Get customers by group
    public IEnumerable&lt;CustomerContact&gt; GetByGroup(string groupName)
    {
        return CustomerContext.Current.GetContacts()
            .Where(c => c.CustomerGroup == groupName);
    }
}</code></pre>

<h3>Customer Addresses</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CustomerAddressService
{
    // Add address to customer
    public CustomerAddress AddAddress(
        CustomerContact customer,
        AddressModel model,
        bool isPreferred = false)
    {
        var address = CustomerAddress.CreateInstance();

        address.Name = model.Name ?? ""Default"";
        address.FirstName = model.FirstName;
        address.LastName = model.LastName;
        address.Line1 = model.Line1;
        address.Line2 = model.Line2;
        address.City = model.City;
        address.RegionCode = model.Region;
        address.PostalCode = model.PostalCode;
        address.CountryCode = model.CountryCode;
        address.DaytimePhoneNumber = model.Phone;
        address.Email = model.Email ?? customer.Email;

        customer.AddContactAddress(address);

        if (isPreferred)
        {
            customer.PreferredShippingAddressId = address.AddressId;
            customer.PreferredBillingAddressId = address.AddressId;
        }

        customer.SaveChanges();

        return address;
    }

    // Get customer addresses
    public IEnumerable&lt;CustomerAddress&gt; GetAddresses(
        CustomerContact customer)
    {
        return customer.ContactAddresses;
    }

    // Get preferred address
    public CustomerAddress GetPreferredShippingAddress(
        CustomerContact customer)
    {
        if (customer.PreferredShippingAddressId == null)
            return customer.ContactAddresses.FirstOrDefault();

        return customer.ContactAddresses
            .FirstOrDefault(a =>
                a.AddressId == customer.PreferredShippingAddressId);
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "co-organizations",
                    ModuleId = "customers-organizations",
                    Title = "B2B Organizations",
                    Summary = "Learn to manage organizations for B2B commerce scenarios.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create and manage organizations",
                        "Add customers to organizations",
                        "Handle organization hierarchies",
                        "Implement B2B approval workflows"
                    },
                    Content = @"
<h2>B2B Organizations</h2>
<p>Commerce Connect supports B2B scenarios through organizations, allowing you to group customers under companies with shared settings, credit limits, and approval workflows.</p>

<h3>Organization Data Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Organization (Company)
├── Name, Description
├── Business Category
├── Credit Limit
├── CustomerContacts[] (Members)
│   ├── Admin Users
│   ├── Purchasers
│   └── Approvers
├── Addresses[]
└── Child Organizations[]
</pre>

<h3>Creating Organizations</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class OrganizationService
{
    // Create organization
    public Organization CreateOrganization(
        string name,
        string description = null)
    {
        var org = Organization.CreateInstance();

        org.Name = name;
        org.Description = description;

        org.SaveChanges();

        return org;
    }

    // Create child organization
    public Organization CreateChildOrganization(
        Organization parent,
        string name)
    {
        var child = Organization.CreateInstance();

        child.Name = name;
        child.ParentId = parent.PrimaryKeyId;

        child.SaveChanges();

        return child;
    }
}</code></pre>

<h3>Managing Organization Members</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class OrganizationMemberService
{
    // Add member to organization
    public void AddMember(
        Organization organization,
        CustomerContact customer,
        string role = ""Member"")
    {
        customer.OwnerId = organization.PrimaryKeyId;
        customer[""OrganizationRole""] = role;
        customer.SaveChanges();
    }

    // Get organization members
    public IEnumerable&lt;CustomerContact&gt; GetMembers(
        Organization organization)
    {
        return CustomerContext.Current.GetContacts()
            .Where(c => c.OwnerId == organization.PrimaryKeyId);
    }

    // Get organization admins
    public IEnumerable&lt;CustomerContact&gt; GetAdmins(
        Organization organization)
    {
        return GetMembers(organization)
            .Where(c => c[""OrganizationRole""]?.ToString() == ""Admin"");
    }

    // Remove member
    public void RemoveMember(
        CustomerContact customer)
    {
        customer.OwnerId = null;
        customer[""OrganizationRole""] = null;
        customer.SaveChanges();
    }
}</code></pre>

<h3>Organization Addresses</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Add organization address
public void AddOrganizationAddress(
    Organization organization,
    AddressModel model)
{
    var address = OrganizationAddress.CreateInstance();

    address.Name = model.Name;
    address.Line1 = model.Line1;
    address.City = model.City;
    address.PostalCode = model.PostalCode;
    address.CountryCode = model.CountryCode;

    organization.Addresses.Add(address);
    organization.SaveChanges();
}

// Get organization addresses
public IEnumerable&lt;OrganizationAddress&gt; GetOrganizationAddresses(
    Organization organization)
{
    return organization.Addresses;
}</code></pre>

<h3>B2B Features</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Credit Limits</td><td class=""px-4 py-2"">Set spending limits per organization</td></tr>
        <tr><td class=""px-4 py-2"">Purchase Approval</td><td class=""px-4 py-2"">Require manager approval for orders</td></tr>
        <tr><td class=""px-4 py-2"">Budget Management</td><td class=""px-4 py-2"">Track spending against budgets</td></tr>
        <tr><td class=""px-4 py-2"">Role-Based Access</td><td class=""px-4 py-2"">Different permissions per role</td></tr>
        <tr><td class=""px-4 py-2"">Requisition Lists</td><td class=""px-4 py-2"">Shared shopping lists</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "co-customer-groups",
                    ModuleId = "customers-organizations",
                    Title = "Customer Groups and Segmentation",
                    Summary = "Configure customer groups for pricing tiers and segmentation.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create and manage customer groups",
                        "Assign customers to groups",
                        "Use groups for pricing",
                        "Implement customer segmentation"
                    },
                    Content = @"
<h2>Customer Groups and Segmentation</h2>
<p>Customer groups allow you to segment customers for pricing, promotions, and personalization purposes.</p>

<h3>Common Customer Groups</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Group</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Retail</td><td class=""px-4 py-2"">Standard consumer pricing</td></tr>
        <tr><td class=""px-4 py-2"">Trade/Wholesale</td><td class=""px-4 py-2"">B2B trade discounts</td></tr>
        <tr><td class=""px-4 py-2"">VIP/Gold/Platinum</td><td class=""px-4 py-2"">Loyalty program tiers</td></tr>
        <tr><td class=""px-4 py-2"">Staff</td><td class=""px-4 py-2"">Employee discounts</td></tr>
        <tr><td class=""px-4 py-2"">Partner</td><td class=""px-4 py-2"">Business partners</td></tr>
    </tbody>
</table>

<h3>Working with Customer Groups</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CustomerGroupService
{
    // Assign customer to group
    public void AssignToGroup(
        CustomerContact customer,
        string groupName)
    {
        customer.CustomerGroup = groupName;
        customer.SaveChanges();
    }

    // Get effective group (considers organization)
    public string GetEffectiveGroup(CustomerContact customer)
    {
        // EffectiveCustomerGroup handles organization inheritance
        return customer.EffectiveCustomerGroup;
    }

    // Upgrade loyalty tier
    public void UpgradeLoyaltyTier(
        CustomerContact customer,
        decimal totalSpend)
    {
        string newTier = totalSpend switch
        {
            >= 10000 => ""Platinum"",
            >= 5000 => ""Gold"",
            >= 1000 => ""Silver"",
            _ => ""Bronze""
        };

        customer.CustomerGroup = newTier;
        customer.SaveChanges();
    }
}

// Using groups for pricing
public Money GetCustomerPrice(
    CustomerContact customer,
    string productCode)
{
    var group = customer.EffectiveCustomerGroup;

    var pricing = new CustomerPricing(
        CustomerPricing.PriceType.PriceGroup,
        group);

    // Get price for customer's group
    return _priceService.GetPrice(productCode, pricing);
}</code></pre>

<h3>Segmentation for Personalization</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CustomerSegmentService
{
    // Check if customer is in segment
    public bool IsInSegment(
        CustomerContact customer,
        string segmentName)
    {
        return segmentName switch
        {
            ""HighValue"" => GetTotalSpend(customer) > 5000,
            ""NewCustomer"" => IsNewCustomer(customer),
            ""Inactive"" => IsInactive(customer),
            ""Trade"" => customer.EffectiveCustomerGroup == ""Trade"",
            _ => false
        };
    }

    private bool IsNewCustomer(CustomerContact customer)
    {
        var registrationDate = DateTime.Parse(
            customer.RegistrationSource ?? DateTime.MinValue.ToString());
        return registrationDate > DateTime.UtcNow.AddDays(-30);
    }

    private bool IsInactive(CustomerContact customer)
    {
        var lastOrder = GetLastOrderDate(customer);
        return lastOrder < DateTime.UtcNow.AddDays(-90);
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "co-customer-registration",
                    ModuleId = "customers-organizations",
                    Title = "Customer Registration and Authentication",
                    Summary = "Implement customer registration and link with ASP.NET Identity.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Integrate with ASP.NET Core Identity",
                        "Implement customer registration",
                        "Handle login and session",
                        "Manage password reset"
                    },
                    Content = @"
<h2>Customer Registration and Authentication</h2>
<p>Commerce Connect integrates with ASP.NET Core Identity for authentication while maintaining commerce-specific customer data in CustomerContact.</p>

<h3>Registration Flow</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
1. User submits registration form
2. Create ASP.NET Identity user
3. Create CustomerContact linked to Identity user
4. Send confirmation email
5. User confirms and logs in
</pre>

<h3>Registration Service</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class RegistrationService
{
    private readonly UserManager&lt;ApplicationUser&gt; _userManager;
    private readonly SignInManager&lt;ApplicationUser&gt; _signInManager;

    public async Task&lt;RegistrationResult&gt; RegisterCustomer(
        RegistrationModel model)
    {
        var result = new RegistrationResult();

        // Create Identity user
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var identityResult = await _userManager.CreateAsync(
            user, model.Password);

        if (!identityResult.Succeeded)
        {
            result.Errors = identityResult.Errors
                .Select(e => e.Description).ToList();
            return result;
        }

        // Create CustomerContact
        var customer = CustomerContact.CreateInstance();
        customer.UserId = user.Id;
        customer.Email = model.Email;
        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.FullName = $""{model.FirstName} {model.LastName}"";
        customer.RegistrationSource = DateTime.UtcNow.ToString();
        customer.SaveChanges();

        // Send confirmation email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await SendConfirmationEmail(model.Email, token);

        result.Success = true;
        result.CustomerId = customer.PrimaryKeyId.Value;

        return result;
    }
}</code></pre>

<h3>Login and Session</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class LoginService
{
    private readonly SignInManager&lt;ApplicationUser&gt; _signInManager;
    private readonly CustomerContext _customerContext;

    public async Task&lt;LoginResult&gt; Login(
        string email,
        string password,
        bool rememberMe = false)
    {
        var result = await _signInManager.PasswordSignInAsync(
            email, password, rememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            // CustomerContext automatically picks up the logged-in user
            var customer = _customerContext.GetContactByUserId(email);

            return new LoginResult
            {
                Success = true,
                Customer = customer
            };
        }

        return new LoginResult
        {
            Success = false,
            RequiresTwoFactor = result.RequiresTwoFactor,
            IsLockedOut = result.IsLockedOut
        };
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}</code></pre>

<h3>Guest Checkout</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public CustomerContact CreateGuestCustomer(string email)
{
    // Create temporary customer for guest checkout
    var guestId = Guid.NewGuid();

    var customer = CustomerContact.CreateInstance();
    customer.UserId = $""guest_{guestId}"";
    customer.Email = email;
    customer.FullName = ""Guest Customer"";
    customer[""IsGuest""] = true;
    customer.SaveChanges();

    return customer;
}

// Convert guest to registered customer
public async Task ConvertGuestToCustomer(
    CustomerContact guest,
    RegistrationModel model)
{
    // Create Identity user
    var user = new ApplicationUser
    {
        UserName = model.Email,
        Email = model.Email
    };

    await _userManager.CreateAsync(user, model.Password);

    // Update CustomerContact
    guest.UserId = user.Id;
    guest.FirstName = model.FirstName;
    guest.LastName = model.LastName;
    guest[""IsGuest""] = false;
    guest.SaveChanges();
}</code></pre>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 7: Markets & Localization

    private LearningModule BuildMarketsLocalizationModule()
    {
        return new LearningModule
        {
            Id = "markets-localization",
            Title = "Markets & Localization",
            Description = "Configure multi-market commerce with currencies, languages, and regional settings.",
            Icon = "globe-alt",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ml-markets-overview",
                    ModuleId = "markets-localization",
                    Title = "Markets Overview",
                    Summary = "Understand markets and how they enable multi-region commerce.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what markets are in Commerce Connect",
                        "Learn market configuration options",
                        "Know how markets affect pricing and availability",
                        "Understand the ICurrentMarket service"
                    },
                    Content = @"
<h2>Markets Overview</h2>
<p>Markets in Commerce Connect allow you to configure region-specific settings for different geographic areas or business segments. Each market can have its own currencies, languages, countries, and payment/shipping options.</p>

<h3>What is a Market?</h3>
<p>A market is a logical grouping of commerce settings that typically represents:</p>
<ul>
    <li>A geographic region (UK, US, Europe)</li>
    <li>A business segment (B2C, B2B)</li>
    <li>A sales channel (Website, Mobile App)</li>
</ul>

<h3>Market Properties</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">MarketId</td><td class=""px-4 py-2"">Unique identifier</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">MarketName</td><td class=""px-4 py-2"">Display name</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">DefaultCurrency</td><td class=""px-4 py-2"">Primary currency</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">DefaultLanguage</td><td class=""px-4 py-2"">Primary language</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Currencies</td><td class=""px-4 py-2"">Supported currencies</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Languages</td><td class=""px-4 py-2"">Supported languages</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Countries</td><td class=""px-4 py-2"">Shipping countries</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IsEnabled</td><td class=""px-4 py-2"">Active status</td></tr>
    </tbody>
</table>

<h3>Working with Markets</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class MarketService
{
    private readonly IMarketService _marketService;
    private readonly ICurrentMarket _currentMarket;

    public MarketService(
        IMarketService marketService,
        ICurrentMarket currentMarket)
    {
        _marketService = marketService;
        _currentMarket = currentMarket;
    }

    // Get current market
    public IMarket GetCurrentMarket()
    {
        return _currentMarket.GetCurrentMarket();
    }

    // Get all enabled markets
    public IEnumerable&lt;IMarket&gt; GetAllMarkets()
    {
        return _marketService.GetAllMarkets()
            .Where(m => m.IsEnabled);
    }

    // Get market by ID
    public IMarket GetMarket(MarketId marketId)
    {
        return _marketService.GetMarket(marketId);
    }

    // Get market by country
    public IMarket GetMarketForCountry(string countryCode)
    {
        return _marketService.GetAllMarkets()
            .FirstOrDefault(m =>
                m.IsEnabled &&
                m.Countries.Contains(countryCode));
    }
}</code></pre>

<h3>ICurrentMarket Service</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Get current market
var market = _currentMarket.GetCurrentMarket();

// Market properties
MarketId id = market.MarketId;
string name = market.MarketName;
Currency currency = market.DefaultCurrency;
CultureInfo language = market.DefaultLanguage;

// Check market capabilities
var currencies = market.Currencies;
var languages = market.Languages;
var countries = market.Countries;</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ml-configuring-markets",
                    ModuleId = "markets-localization",
                    Title = "Configuring Markets",
                    Summary = "Learn to create and configure markets for different regions.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create new markets",
                        "Configure market currencies and languages",
                        "Set up country mappings",
                        "Manage market settings"
                    },
                    Content = @"
<h2>Configuring Markets</h2>
<p>Markets are typically configured in Commerce Manager, but can also be managed programmatically.</p>

<h3>Market Configuration Example</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-bold"">UK Market</h4>
    <ul class=""mt-2 space-y-1"">
        <li><strong>MarketId:</strong> UK</li>
        <li><strong>Name:</strong> United Kingdom</li>
        <li><strong>Default Currency:</strong> GBP</li>
        <li><strong>Default Language:</strong> en-GB</li>
        <li><strong>Countries:</strong> GB, IE</li>
    </ul>
</div>

<h3>Creating Markets Programmatically</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class MarketConfigurationService
{
    private readonly IMarketService _marketService;

    // Create new market
    public void CreateMarket(MarketConfiguration config)
    {
        var market = new MarketImpl(config.MarketId)
        {
            MarketName = config.Name,
            DefaultCurrency = new Currency(config.DefaultCurrency),
            DefaultLanguage = new CultureInfo(config.DefaultLanguage),
            IsEnabled = config.IsEnabled
        };

        // Add currencies
        foreach (var currency in config.Currencies)
        {
            market.Currencies.Add(new Currency(currency));
        }

        // Add languages
        foreach (var language in config.Languages)
        {
            market.Languages.Add(new CultureInfo(language));
        }

        // Add countries
        foreach (var country in config.Countries)
        {
            market.Countries.Add(country);
        }

        _marketService.UpdateMarket(market);
    }
}

public class MarketConfiguration
{
    public string MarketId { get; set; }
    public string Name { get; set; }
    public string DefaultCurrency { get; set; }
    public string DefaultLanguage { get; set; }
    public List&lt;string&gt; Currencies { get; set; }
    public List&lt;string&gt; Languages { get; set; }
    public List&lt;string&gt; Countries { get; set; }
    public bool IsEnabled { get; set; } = true;
}</code></pre>

<h3>Multi-Site Market Configuration</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Map sites to markets in startup
services.Configure&lt;SiteMarketOptions&gt;(options =>
{
    options.SiteMarketMappings = new Dictionary&lt;string, MarketId&gt;
    {
        { ""uk.mystore.com"", new MarketId(""UK"") },
        { ""us.mystore.com"", new MarketId(""US"") },
        { ""de.mystore.com"", new MarketId(""DE"") }
    };
});</code></pre>

<h3>Market Selection Strategy</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class MarketSelectionService
{
    private readonly IMarketService _marketService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    // Determine market from request
    public IMarket DetermineMarket()
    {
        var context = _httpContextAccessor.HttpContext;

        // 1. Check cookie
        var marketCookie = context.Request.Cookies[""market""];
        if (!string.IsNullOrEmpty(marketCookie))
        {
            var market = _marketService.GetMarket(new MarketId(marketCookie));
            if (market?.IsEnabled == true)
                return market;
        }

        // 2. Check geo-location (IP-based)
        var countryCode = GetCountryFromIP(context);
        var geoMarket = GetMarketForCountry(countryCode);
        if (geoMarket != null)
            return geoMarket;

        // 3. Return default market
        return _marketService.GetAllMarkets()
            .FirstOrDefault(m => m.IsEnabled);
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ml-currencies",
                    ModuleId = "markets-localization",
                    Title = "Working with Currencies",
                    Summary = "Handle multi-currency pricing and display.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the Money type",
                        "Format currency for display",
                        "Handle currency conversion",
                        "Work with exchange rates"
                    },
                    Content = @"
<h2>Working with Currencies</h2>
<p>Commerce Connect uses the <code>Money</code> type for all monetary values, ensuring currency is always tracked alongside amounts.</p>

<h3>The Money Type</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Creating Money values
var price = new Money(99.99m, Currency.GBP);
var usdPrice = new Money(129.99m, new Currency(""USD""));

// Properties
decimal amount = price.Amount;      // 99.99
Currency currency = price.Currency; // GBP

// Formatting
string display = price.ToString();  // £99.99</code></pre>

<h3>Currency Formatting</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CurrencyFormatService
{
    // Format price with market's culture
    public string FormatPrice(Money money, IMarket market)
    {
        var culture = market.DefaultLanguage;
        return money.Amount.ToString(""C"", culture);
    }

    // Format with explicit formatting
    public string FormatPriceExplicit(Money money)
    {
        var symbol = GetCurrencySymbol(money.Currency);
        return $""{symbol}{money.Amount:N2}"";
    }

    private string GetCurrencySymbol(Currency currency)
    {
        return currency.CurrencyCode switch
        {
            ""GBP"" => ""£"",
            ""USD"" => ""$"",
            ""EUR"" => ""€"",
            _ => currency.CurrencyCode
        };
    }
}</code></pre>

<h3>Currency Considerations</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-500 p-4 my-4"">
    <p><strong>Important:</strong> Commerce Connect does not automatically convert between currencies. Prices must be set explicitly for each market's currency.</p>
</div>

<h3>Price by Market/Currency</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class MultiCurrencyPriceService
{
    private readonly IPriceDetailService _priceDetailService;

    // Set prices for multiple markets
    public void SetMultiMarketPrices(
        string code,
        Dictionary&lt;MarketId, Money&gt; prices)
    {
        var priceValues = prices.Select(kvp => new PriceDetailValue
        {
            CatalogKey = new CatalogKey(code),
            MarketId = kvp.Key,
            CustomerPricing = CustomerPricing.AllCustomers,
            MinQuantity = 0,
            UnitPrice = kvp.Value,
            ValidFrom = DateTime.UtcNow
        });

        _priceDetailService.Save(priceValues);
    }

    // Example usage
    public void SetInternationalPrices(string code)
    {
        SetMultiMarketPrices(code, new Dictionary&lt;MarketId, Money&gt;
        {
            { new MarketId(""UK""), new Money(99.99m, ""GBP"") },
            { new MarketId(""US""), new Money(129.99m, ""USD"") },
            { new MarketId(""EU""), new Money(109.99m, ""EUR"") }
        });
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ml-localization",
                    ModuleId = "markets-localization",
                    Title = "Content Localization",
                    Summary = "Localize product content for different languages and regions.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create localized product content",
                        "Handle language fallbacks",
                        "Localize category and navigation",
                        "Best practices for multi-language catalogs"
                    },
                    Content = @"
<h2>Content Localization</h2>
<p>Commerce Connect leverages Optimizely CMS's localization capabilities to provide product content in multiple languages.</p>

<h3>Localized Content Model</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Product Content (Master)
├── English (en) - Master language
│   └── Name, Description, SEO
├── German (de) - Translated
│   └── Name, Description, SEO
├── French (fr) - Translated
│   └── Name, Description, SEO
└── Pricing (not localized - by market)
</pre>

<h3>Loading Localized Content</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class LocalizedContentService
{
    private readonly IContentLoader _contentLoader;

    // Load product in specific language
    public ProductContent GetProduct(
        ContentReference reference,
        CultureInfo language)
    {
        var loaderOptions = new LoaderOptions
        {
            LanguageLoaderOption.FallbackWithMaster(language)
        };

        return _contentLoader.Get&lt;ProductContent&gt;(
            reference, loaderOptions);
    }

    // Load product in current language
    public ProductContent GetProductInCurrentLanguage(
        ContentReference reference)
    {
        return _contentLoader.Get&lt;ProductContent&gt;(reference);
    }

    // Get available languages for product
    public IEnumerable&lt;CultureInfo&gt; GetAvailableLanguages(
        ContentReference reference)
    {
        return _contentLoader.GetExistingLanguages(reference);
    }
}</code></pre>

<h3>Creating Localized Products</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public void CreateLocalizedProduct(
    ContentReference parentCategory,
    Dictionary&lt;CultureInfo, ProductData&gt; localizedData)
{
    // Create in master language first
    var masterData = localizedData[_masterLanguage];

    var product = _contentRepository
        .GetDefault&lt;FashionProduct&gt;(parentCategory, _masterLanguage);

    product.Code = masterData.Code;
    product.Name = masterData.Name;
    product.DisplayName = masterData.DisplayName;

    var reference = _contentRepository.Save(
        product,
        SaveAction.Publish,
        AccessLevel.NoAccess);

    // Create translations
    foreach (var (language, data) in localizedData)
    {
        if (language == _masterLanguage) continue;

        var localized = _contentRepository
            .CreateLanguageBranch&lt;FashionProduct&gt;(
                reference, language);

        localized.Name = data.Name;
        localized.DisplayName = data.DisplayName;
        localized.LongDescription = new XhtmlString(data.Description);

        _contentRepository.Save(
            localized,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }
}</code></pre>

<h3>Language Fallback</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Configure fallback chain
services.Configure&lt;LanguageOptions&gt;(options =>
{
    options.LanguageFallbackChains = new Dictionary&lt;string, string[]&gt;
    {
        { ""de"", new[] { ""en"" } },        // German falls back to English
        { ""fr"", new[] { ""en"" } },        // French falls back to English
        { ""es"", new[] { ""en"" } },        // Spanish falls back to English
        { ""de-AT"", new[] { ""de"", ""en"" }} // Austrian German → German → English
    };
});</code></pre>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 8: Marketing & Promotions

    private LearningModule BuildMarketingPromotionsModule()
    {
        return new LearningModule
        {
            Id = "marketing-promotions",
            Title = "Marketing & Promotions",
            Description = "Create discounts, promotions, coupon codes, and marketing campaigns.",
            Icon = "tag",
            Order = 8,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "mp-promotions-overview",
                    ModuleId = "marketing-promotions",
                    Title = "Promotions System Overview",
                    Summary = "Understand the promotion engine and types of promotions available.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the promotion engine architecture",
                        "Learn the different promotion types",
                        "Know how promotions are applied to orders",
                        "Understand promotion priority and stacking"
                    },
                    Content = @"
<h2>Promotions System Overview</h2>
<p>Commerce Connect includes a powerful promotion engine that supports various discount types, from simple percentage-off deals to complex multi-buy offers.</p>

<h3>Promotion Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
IPromotionEngine
├── Evaluates cart against all promotions
├── Applies qualifying discounts
└── Returns reward results

Promotion Types:
├── Entry-level (product discounts)
├── Order-level (cart discounts)
└── Shipping-level (shipping discounts)
</pre>

<h3>Built-in Promotion Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Percentage Off</td><td class=""px-4 py-2"">Discount by percentage</td><td class=""px-4 py-2"">20% off all shoes</td></tr>
        <tr><td class=""px-4 py-2"">Amount Off</td><td class=""px-4 py-2"">Fixed amount discount</td><td class=""px-4 py-2"">£10 off orders over £50</td></tr>
        <tr><td class=""px-4 py-2"">Buy X Get Y</td><td class=""px-4 py-2"">Multi-buy offers</td><td class=""px-4 py-2"">Buy 2 get 1 free</td></tr>
        <tr><td class=""px-4 py-2"">Free Shipping</td><td class=""px-4 py-2"">Shipping discount</td><td class=""px-4 py-2"">Free shipping over £30</td></tr>
        <tr><td class=""px-4 py-2"">Gift with Purchase</td><td class=""px-4 py-2"">Free item</td><td class=""px-4 py-2"">Free gift bag with purchase</td></tr>
    </tbody>
</table>

<h3>Using the Promotion Engine</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class PromotionService
{
    private readonly IPromotionEngine _promotionEngine;

    public PromotionService(IPromotionEngine promotionEngine)
    {
        _promotionEngine = promotionEngine;
    }

    // Apply promotions to cart
    public IEnumerable&lt;RewardDescription&gt; ApplyPromotions(ICart cart)
    {
        var rewards = _promotionEngine.Run(cart);

        // Get fulfilled rewards
        return rewards.Where(r =>
            r.Status == FulfillmentStatus.Fulfilled);
    }

    // Get potential savings
    public Money GetTotalSavings(ICart cart)
    {
        var rewards = _promotionEngine.Run(cart);

        return rewards
            .Where(r => r.Status == FulfillmentStatus.Fulfilled)
            .Aggregate(
                new Money(0, cart.Currency),
                (total, r) => total + r.SavedAmount);
    }
}</code></pre>

<h3>Promotion Priority</h3>
<p>Promotions are evaluated in priority order:</p>
<ol class=""list-decimal list-inside space-y-1 my-4"">
    <li>Exclusive promotions (if any match, only that one applies)</li>
    <li>Higher priority promotions first</li>
    <li>Best value for customer when multiple apply</li>
</ol>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mp-creating-promotions",
                    ModuleId = "marketing-promotions",
                    Title = "Creating Promotions",
                    Summary = "Learn to create different types of promotions in Commerce Connect.",
                    Order = 2,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Create percentage and amount discounts",
                        "Configure promotion conditions",
                        "Set up multi-buy offers",
                        "Define promotion validity periods"
                    },
                    Content = @"
<h2>Creating Promotions</h2>
<p>Promotions in Commerce Connect are content items that can be created in Commerce Manager or programmatically.</p>

<h3>Promotion Content Types</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Entry-level promotion (product discount)
[ContentType(
    DisplayName = ""Product Percentage Discount"",
    GUID = ""..."")]
public class ProductPercentagePromotion : EntryPromotion
{
    [Display(Name = ""Discount Percentage"")]
    [Range(1, 100)]
    public virtual decimal DiscountPercentage { get; set; }

    [Display(Name = ""Maximum Discount"")]
    public virtual decimal? MaximumDiscount { get; set; }
}

// Order-level promotion
[ContentType(
    DisplayName = ""Order Amount Discount"",
    GUID = ""..."")]
public class OrderAmountPromotion : OrderPromotion
{
    [Display(Name = ""Minimum Order Value"")]
    public virtual decimal MinimumOrderValue { get; set; }

    [Display(Name = ""Discount Amount"")]
    public virtual decimal DiscountAmount { get; set; }
}</code></pre>

<h3>Creating Promotions Programmatically</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class PromotionCreationService
{
    private readonly IContentRepository _contentRepository;
    private readonly ContentReference _campaignFolder;

    // Create percentage discount
    public ContentReference CreatePercentageDiscount(
        string name,
        decimal percentage,
        ContentReference targetCategory,
        DateTime validFrom,
        DateTime validUntil)
    {
        var promotion = _contentRepository
            .GetDefault&lt;ProductPercentagePromotion&gt;(_campaignFolder);

        promotion.Name = name;
        promotion.DiscountPercentage = percentage;
        promotion.Condition.Items = new[]
        {
            new CatalogItemSelection
            {
                Type = CatalogItemSelectionType.Category,
                Items = new[] { targetCategory.ToString() }
            }
        };
        promotion.Campaign.ValidFrom = validFrom;
        promotion.Campaign.ValidUntil = validUntil;
        promotion.IsActive = true;

        return _contentRepository.Save(
            promotion,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }

    // Create ""Buy X Get Y"" promotion
    public ContentReference CreateBuyXGetY(
        string name,
        int buyQuantity,
        int getQuantity,
        ContentReference targetCategory)
    {
        var promotion = _contentRepository
            .GetDefault&lt;BuyXGetYPromotion&gt;(_campaignFolder);

        promotion.Name = name;
        promotion.BuyQuantity = buyQuantity;
        promotion.GetQuantity = getQuantity;
        promotion.DiscountPercentage = 100; // 100% = free
        promotion.Condition.Items = new[]
        {
            new CatalogItemSelection
            {
                Type = CatalogItemSelectionType.Category,
                Items = new[] { targetCategory.ToString() }
            }
        };
        promotion.IsActive = true;

        return _contentRepository.Save(
            promotion,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }
}</code></pre>

<h3>Promotion Conditions</h3>
<p>Promotions can have various conditions:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Condition</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Catalog Items</td><td class=""px-4 py-2"">Specific products or categories</td></tr>
        <tr><td class=""px-4 py-2"">Minimum Quantity</td><td class=""px-4 py-2"">Required quantity in cart</td></tr>
        <tr><td class=""px-4 py-2"">Minimum Order Value</td><td class=""px-4 py-2"">Required cart subtotal</td></tr>
        <tr><td class=""px-4 py-2"">Customer Group</td><td class=""px-4 py-2"">VIP, Trade, etc.</td></tr>
        <tr><td class=""px-4 py-2"">Coupon Code</td><td class=""px-4 py-2"">Requires code entry</td></tr>
        <tr><td class=""px-4 py-2"">Market</td><td class=""px-4 py-2"">Specific markets only</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mp-coupon-codes",
                    ModuleId = "marketing-promotions",
                    Title = "Coupon Codes",
                    Summary = "Implement coupon codes and vouchers for promotions.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create coupon-based promotions",
                        "Validate and apply coupon codes",
                        "Handle single-use and limited-use coupons",
                        "Track coupon usage"
                    },
                    Content = @"
<h2>Coupon Codes</h2>
<p>Coupon codes allow customers to unlock promotions by entering a code at checkout.</p>

<h3>Coupon Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Unlimited Use</td><td class=""px-4 py-2"">Can be used by anyone, any number of times</td></tr>
        <tr><td class=""px-4 py-2"">Limited Total Uses</td><td class=""px-4 py-2"">Maximum redemptions across all customers</td></tr>
        <tr><td class=""px-4 py-2"">Single Use Per Customer</td><td class=""px-4 py-2"">Each customer can use once</td></tr>
        <tr><td class=""px-4 py-2"">Unique Codes</td><td class=""px-4 py-2"">Generated codes, one use each</td></tr>
    </tbody>
</table>

<h3>Working with Coupons</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CouponService
{
    private readonly IPromotionEngine _promotionEngine;
    private readonly IOrderRepository _orderRepository;

    // Apply coupon to cart
    public CouponResult ApplyCoupon(ICart cart, string couponCode)
    {
        // Add coupon to cart
        var form = cart.GetFirstForm();
        if (!form.CouponCodes.Contains(couponCode))
        {
            form.CouponCodes.Add(couponCode);
        }

        // Run promotion engine
        var rewards = _promotionEngine.Run(cart);

        // Check if coupon was applied
        var couponReward = rewards.FirstOrDefault(r =>
            r.Promotion.Coupon?.Code == couponCode);

        if (couponReward?.Status == FulfillmentStatus.Fulfilled)
        {
            _orderRepository.Save(cart);
            return new CouponResult
            {
                Success = true,
                DiscountAmount = couponReward.SavedAmount,
                PromotionName = couponReward.Promotion.Name
            };
        }

        // Remove invalid coupon
        form.CouponCodes.Remove(couponCode);

        return new CouponResult
        {
            Success = false,
            ErrorMessage = GetCouponErrorMessage(couponReward)
        };
    }

    // Remove coupon from cart
    public void RemoveCoupon(ICart cart, string couponCode)
    {
        var form = cart.GetFirstForm();
        form.CouponCodes.Remove(couponCode);
        _orderRepository.Save(cart);
    }

    // Validate coupon without applying
    public bool ValidateCoupon(string couponCode)
    {
        // Check if coupon exists and is valid
        var promotion = GetPromotionByCoupon(couponCode);

        if (promotion == null)
            return false;

        if (!promotion.IsActive)
            return false;

        if (promotion.Campaign.ValidUntil < DateTime.UtcNow)
            return false;

        // Check usage limits
        if (promotion.Coupon.MaxRedemptions.HasValue)
        {
            var usageCount = GetCouponUsageCount(couponCode);
            if (usageCount >= promotion.Coupon.MaxRedemptions)
                return false;
        }

        return true;
    }
}

public class CouponResult
{
    public bool Success { get; set; }
    public Money DiscountAmount { get; set; }
    public string PromotionName { get; set; }
    public string ErrorMessage { get; set; }
}</code></pre>

<h3>Generating Unique Coupon Codes</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CouponGeneratorService
{
    // Generate batch of unique codes
    public IEnumerable&lt;string&gt; GenerateUniqueCodes(
        string prefix,
        int count,
        int codeLength = 8)
    {
        var codes = new HashSet&lt;string&gt;();

        while (codes.Count < count)
        {
            var code = GenerateCode(prefix, codeLength);
            codes.Add(code);
        }

        return codes;
    }

    private string GenerateCode(string prefix, int length)
    {
        const string chars = ""ABCDEFGHJKLMNPQRSTUVWXYZ23456789"";
        var random = new Random();

        var code = new string(
            Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

        return $""{prefix}{code}"";
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mp-campaigns",
                    ModuleId = "marketing-promotions",
                    Title = "Marketing Campaigns",
                    Summary = "Organize promotions into campaigns with scheduling and targeting.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create marketing campaigns",
                        "Schedule campaign periods",
                        "Target specific customer segments",
                        "Track campaign performance"
                    },
                    Content = @"
<h2>Marketing Campaigns</h2>
<p>Campaigns allow you to organize related promotions together with shared scheduling and targeting.</p>

<h3>Campaign Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Campaign
├── Name, Description
├── Valid From / Valid Until
├── Target Markets
├── Target Customer Segments
└── Promotions[]
    ├── Percentage Discount
    ├── Free Shipping
    └── Gift with Purchase
</pre>

<h3>Creating Campaigns</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CampaignService
{
    private readonly IContentRepository _contentRepository;
    private readonly ContentReference _marketingRoot;

    // Create campaign
    public ContentReference CreateCampaign(
        string name,
        DateTime validFrom,
        DateTime validUntil)
    {
        var campaign = _contentRepository
            .GetDefault&lt;SalesCampaign&gt;(_marketingRoot);

        campaign.Name = name;
        campaign.ValidFrom = validFrom;
        campaign.ValidUntil = validUntil;
        campaign.IsActive = true;

        return _contentRepository.Save(
            campaign,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }

    // Add promotion to campaign
    public void AddPromotionToCampaign(
        ContentReference campaign,
        ContentReference promotion)
    {
        var promo = _contentRepository
            .Get&lt;PromotionData&gt;(promotion)
            .CreateWritableClone() as PromotionData;

        promo.ParentLink = campaign;

        _contentRepository.Save(
            promo,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }
}</code></pre>

<h3>Campaign Examples</h3>
<div class=""grid grid-cols-1 md:grid-cols-2 gap-4 my-4"">
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold"">🎄 Christmas Sale</h4>
        <p class=""text-sm mt-2"">Dec 1 - Dec 25</p>
        <ul class=""text-sm mt-2"">
            <li>• 20% off all products</li>
            <li>• Free shipping over £30</li>
            <li>• Free gift wrap</li>
        </ul>
    </div>
    <div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
        <h4 class=""font-bold"">⚡ Flash Sale</h4>
        <p class=""text-sm mt-2"">Friday 6pm - Sunday 6pm</p>
        <ul class=""text-sm mt-2"">
            <li>• 50% off selected items</li>
            <li>• Extra 10% for VIP members</li>
            <li>• Limited stock</li>
        </ul>
    </div>
</div>

<h3>Campaign Performance Tracking</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CampaignReportService
{
    // Get campaign performance
    public CampaignPerformance GetCampaignPerformance(
        ContentReference campaign,
        DateTime from,
        DateTime to)
    {
        var promotions = GetCampaignPromotions(campaign);

        var orders = GetOrdersInPeriod(from, to)
            .Where(o => HasCampaignPromotion(o, promotions));

        return new CampaignPerformance
        {
            OrderCount = orders.Count(),
            Revenue = orders.Sum(o => o.GetTotal()),
            DiscountGiven = orders.Sum(o => o.GetDiscountTotal()),
            AverageOrderValue = orders.Average(o => o.GetTotal()),
            TopSellingProducts = GetTopProducts(orders)
        };
    }
}

public class CampaignPerformance
{
    public int OrderCount { get; set; }
    public decimal Revenue { get; set; }
    public decimal DiscountGiven { get; set; }
    public decimal AverageOrderValue { get; set; }
    public List&lt;ProductSales&gt; TopSellingProducts { get; set; }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 9: Payments & Shipping

    private LearningModule BuildPaymentsShippingModule()
    {
        return new LearningModule
        {
            Id = "payments-shipping",
            Title = "Payments & Shipping",
            Description = "Implement payment gateways, shipping methods, and tax calculations.",
            Icon = "credit-card",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ps-payment-overview",
                    ModuleId = "payments-shipping",
                    Title = "Payment System Overview",
                    Summary = "Understand the payment provider model and payment processing flow.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand the payment provider architecture",
                        "Learn payment transaction types",
                        "Know the payment processing flow",
                        "Understand payment methods vs gateways"
                    },
                    Content = @"
<h2>Payment System Overview</h2>
<p>Commerce Connect uses a provider-based payment system that supports multiple payment gateways and methods.</p>

<h3>Payment Architecture</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
Payment Method (UI Configuration)
    └── Payment Gateway (Provider Implementation)
        └── Payment Processor (Transaction Processing)

Transaction Flow:
1. Authorize → Reserve funds
2. Capture → Collect funds
3. (Optional) Void / Refund
</pre>

<h3>Transaction Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">When Used</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Authorization</td><td class=""px-4 py-2"">Reserve funds on card</td><td class=""px-4 py-2"">At checkout</td></tr>
        <tr><td class=""px-4 py-2"">Capture</td><td class=""px-4 py-2"">Collect authorized funds</td><td class=""px-4 py-2"">At shipment</td></tr>
        <tr><td class=""px-4 py-2"">Sale</td><td class=""px-4 py-2"">Authorize + Capture together</td><td class=""px-4 py-2"">Digital goods</td></tr>
        <tr><td class=""px-4 py-2"">Void</td><td class=""px-4 py-2"">Cancel authorization</td><td class=""px-4 py-2"">Order cancelled</td></tr>
        <tr><td class=""px-4 py-2"">Refund</td><td class=""px-4 py-2"">Return captured funds</td><td class=""px-4 py-2"">Returns</td></tr>
    </tbody>
</table>

<h3>Payment Processing</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class PaymentService
{
    private readonly IOrderRepository _orderRepository;

    // Process payment on cart
    public PaymentProcessingResult ProcessPayment(ICart cart)
    {
        // Validate cart
        var result = cart.ProcessPayments();

        var payments = cart.GetFirstForm().Payments;
        var processedPayment = payments.FirstOrDefault();

        return new PaymentProcessingResult
        {
            Success = processedPayment?.Status ==
                PaymentStatus.Processed.ToString(),
            TransactionId = processedPayment?.TransactionID,
            AuthorizationCode = processedPayment?.AuthorizationCode
        };
    }

    // Capture payment on order
    public void CapturePayment(IPurchaseOrder order)
    {
        var payment = order.GetFirstForm().Payments
            .FirstOrDefault(p =>
                p.TransactionType == TransactionType.Authorization.ToString());

        if (payment != null)
        {
            payment.TransactionType = TransactionType.Capture.ToString();
            order.ProcessPayments();
            _orderRepository.Save(order);
        }
    }

    // Refund payment
    public void RefundPayment(
        IPurchaseOrder order,
        decimal amount)
    {
        var payment = order.GetFirstForm().Payments
            .FirstOrDefault(p =>
                p.TransactionType == TransactionType.Capture.ToString());

        if (payment != null)
        {
            var refund = order.CreatePayment();
            refund.TransactionType = TransactionType.Credit.ToString();
            refund.Amount = amount;
            refund.PaymentMethodId = payment.PaymentMethodId;

            order.GetFirstForm().Payments.Add(refund);
            order.ProcessPayments();
            _orderRepository.Save(order);
        }
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ps-payment-gateway",
                    ModuleId = "payments-shipping",
                    Title = "Implementing Payment Gateways",
                    Summary = "Create custom payment gateway integrations.",
                    Order = 2,
                    EstimatedMinutes = 20,
                    LearningObjectives = new List<string>
                    {
                        "Create a custom payment gateway",
                        "Handle payment callbacks",
                        "Implement authorization and capture",
                        "Handle errors and failures"
                    },
                    Content = @"
<h2>Implementing Payment Gateways</h2>
<p>Creating a custom payment gateway involves implementing the payment gateway interface and registering it with Commerce Connect.</p>

<h3>Payment Gateway Implementation</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class StripePaymentGateway : IPaymentGateway
{
    private readonly StripeClient _stripeClient;

    public StripePaymentGateway(IConfiguration config)
    {
        StripeConfiguration.ApiKey = config[""Stripe:SecretKey""];
        _stripeClient = new StripeClient();
    }

    public PaymentGatewayResult ProcessAuthorization(
        IPayment payment,
        IOrderForm orderForm)
    {
        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(payment.Amount * 100),
                Currency = payment.Currency.CurrencyCode.ToLower(),
                PaymentMethod = payment.Properties[""StripePaymentMethodId""].ToString(),
                Confirm = true,
                CaptureMethod = ""manual""
            };

            var service = new PaymentIntentService(_stripeClient);
            var intent = service.Create(options);

            if (intent.Status == ""requires_capture"")
            {
                payment.TransactionID = intent.Id;
                payment.AuthorizationCode = intent.Id;
                return new PaymentGatewayResult
                {
                    Success = true,
                    ResponseCode = ""AUTHORIZED""
                };
            }

            return new PaymentGatewayResult
            {
                Success = false,
                ResponseCode = intent.Status,
                ResponseMessage = ""Authorization failed""
            };
        }
        catch (StripeException ex)
        {
            return new PaymentGatewayResult
            {
                Success = false,
                ResponseCode = ex.StripeError.Code,
                ResponseMessage = ex.StripeError.Message
            };
        }
    }

    public PaymentGatewayResult ProcessCapture(
        IPayment payment)
    {
        var service = new PaymentIntentService(_stripeClient);
        var intent = service.Capture(payment.TransactionID);

        return new PaymentGatewayResult
        {
            Success = intent.Status == ""succeeded"",
            TransactionID = intent.Id
        };
    }
}</code></pre>

<h3>Registering the Gateway</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// In Startup.cs or Program.cs
services.AddSingleton&lt;IPaymentGateway, StripePaymentGateway&gt;();

// Configure payment method in Commerce Manager
// or programmatically:
PaymentManager.CreatePaymentMethod(new PaymentMethodDto
{
    Name = ""Credit Card (Stripe)"",
    SystemKeyword = ""Stripe"",
    PaymentClassName = typeof(StripePaymentGateway).FullName,
    IsActive = true
});</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ps-shipping-methods",
                    ModuleId = "payments-shipping",
                    Title = "Configuring Shipping Methods",
                    Summary = "Set up shipping methods and calculate shipping costs.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Configure shipping methods",
                        "Implement shipping rate calculation",
                        "Handle multiple shipments",
                        "Create shipping providers"
                    },
                    Content = @"
<h2>Configuring Shipping Methods</h2>
<p>Commerce Connect provides a flexible shipping system with support for multiple shipping providers and methods.</p>

<h3>Shipping Components</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Shipping Method</td><td class=""px-4 py-2"">Customer-facing option (Standard, Express)</td></tr>
        <tr><td class=""px-4 py-2"">Shipping Provider</td><td class=""px-4 py-2"">Carrier integration (Royal Mail, UPS)</td></tr>
        <tr><td class=""px-4 py-2"">Shipping Gateway</td><td class=""px-4 py-2"">Rate calculation logic</td></tr>
    </tbody>
</table>

<h3>Getting Available Shipping Methods</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class ShippingService
{
    private readonly IShippingCalculator _shippingCalculator;

    // Get shipping options for cart
    public IEnumerable&lt;ShippingOption&gt; GetShippingOptions(
        ICart cart,
        IOrderAddress address)
    {
        var shipment = cart.GetFirstShipment();
        var methods = ShippingManager.GetShippingMethods(
            cart.Currency,
            address.CountryCode);

        foreach (var method in methods.ShippingMethod)
        {
            var rate = _shippingCalculator.GetRate(
                shipment,
                method,
                cart.MarketId);

            yield return new ShippingOption
            {
                MethodId = method.ShippingMethodId,
                Name = method.DisplayName,
                Description = method.Description,
                Price = rate?.Money ?? new Money(0, cart.Currency),
                EstimatedDays = GetEstimatedDays(method)
            };
        }
    }

    // Set shipping method on shipment
    public void SetShippingMethod(
        ICart cart,
        Guid shippingMethodId)
    {
        var shipment = cart.GetFirstShipment();
        shipment.ShippingMethodId = shippingMethodId;
    }
}

public class ShippingOption
{
    public Guid MethodId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Money Price { get; set; }
    public int? EstimatedDays { get; set; }
}</code></pre>

<h3>Custom Shipping Calculator</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class WeightBasedShippingGateway : IShippingGateway
{
    public ShippingRate GetRate(
        IShipment shipment,
        ShippingMethodDto method,
        IMarket market)
    {
        // Calculate total weight
        var totalWeight = shipment.LineItems
            .Sum(li => GetItemWeight(li) * li.Quantity);

        // Get rate based on weight
        var rate = GetRateForWeight(
            method,
            totalWeight,
            shipment.ShippingAddress.CountryCode);

        return new ShippingRate(
            method.ShippingMethodId,
            new Money(rate, market.DefaultCurrency));
    }

    private decimal GetRateForWeight(
        ShippingMethodDto method,
        decimal weight,
        string countryCode)
    {
        // Weight-based rate table
        return weight switch
        {
            <= 0.5m => 3.99m,
            <= 1.0m => 5.99m,
            <= 2.0m => 7.99m,
            <= 5.0m => 9.99m,
            _ => 14.99m
        };
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ps-taxes",
                    ModuleId = "payments-shipping",
                    Title = "Tax Configuration",
                    Summary = "Configure tax rates and jurisdictions for orders.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Configure tax jurisdictions",
                        "Set up tax rates by region",
                        "Handle tax-exempt customers",
                        "Display tax in prices"
                    },
                    Content = @"
<h2>Tax Configuration</h2>
<p>Commerce Connect provides tax calculation based on jurisdictions, product categories, and customer tax status.</p>

<h3>Tax Concepts</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Concept</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Tax Jurisdiction</td><td class=""px-4 py-2"">Geographic area with tax rules</td></tr>
        <tr><td class=""px-4 py-2"">Tax Category</td><td class=""px-4 py-2"">Product classification for tax</td></tr>
        <tr><td class=""px-4 py-2"">Tax Rate</td><td class=""px-4 py-2"">Percentage applied</td></tr>
        <tr><td class=""px-4 py-2"">Tax Exempt</td><td class=""px-4 py-2"">Customers not charged tax</td></tr>
    </tbody>
</table>

<h3>Tax Calculation</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class TaxService
{
    private readonly ITaxCalculator _taxCalculator;

    // Calculate tax for cart
    public Money CalculateOrderTax(ICart cart)
    {
        var totals = _taxCalculator.GetTaxTotal(
            cart,
            cart.MarketId,
            cart.Currency);

        return totals;
    }

    // Get tax breakdown
    public IEnumerable&lt;TaxLine&gt; GetTaxBreakdown(ICart cart)
    {
        var address = cart.GetFirstShipment()?.ShippingAddress;
        if (address == null)
            yield break;

        foreach (var lineItem in cart.GetAllLineItems())
        {
            var taxValues = GetTaxValuesForItem(
                lineItem,
                address);

            foreach (var tax in taxValues)
            {
                yield return new TaxLine
                {
                    Name = tax.TaxName,
                    Rate = tax.Percentage,
                    Amount = CalculateTaxAmount(
                        lineItem.PlacedPrice * lineItem.Quantity,
                        tax.Percentage)
                };
            }
        }
    }
}

public class TaxLine
{
    public string Name { get; set; }
    public decimal Rate { get; set; }
    public Money Amount { get; set; }
}</code></pre>

<h3>Tax Display Options</h3>
<div class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg my-4"">
    <h4 class=""font-bold"">Price Display Options</h4>
    <ul class=""mt-2 space-y-1"">
        <li><strong>Tax Inclusive</strong> - Prices include tax (common in EU/UK)</li>
        <li><strong>Tax Exclusive</strong> - Tax added at checkout (common in US)</li>
    </ul>
</div>

<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>// Configure tax display
services.Configure&lt;TaxOptions&gt;(options =>
{
    options.PricesIncludeTax = true; // For EU/UK markets
});

// Display price with/without tax
public class PriceDisplayService
{
    public Money GetDisplayPrice(
        Money price,
        decimal taxRate,
        bool includeTax)
    {
        if (includeTax)
        {
            return price; // Already includes tax
        }
        else
        {
            // Remove tax from display
            var netAmount = price.Amount / (1 + taxRate / 100);
            return new Money(netAmount, price.Currency);
        }
    }
}</code></pre>
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
            Description = "Learn advanced Commerce Connect patterns, performance optimization, and integration strategies.",
            Icon = "beaker",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "at-search-integration",
                    ModuleId = "advanced-topics",
                    Title = "Search and Navigation",
                    Summary = "Integrate product search with Optimizely Search & Navigation.",
                    Order = 1,
                    EstimatedMinutes = 18,
                    LearningObjectives = new List<string>
                    {
                        "Index products in Search & Navigation",
                        "Build faceted product search",
                        "Implement product filtering",
                        "Optimize search performance"
                    },
                    Content = @"
<h2>Search and Navigation Integration</h2>
<p>Commerce Connect integrates with Optimizely Search & Navigation (formerly Find) to provide powerful product search capabilities.</p>

<h3>Product Search Service</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class ProductSearchService
{
    private readonly IClient _searchClient;
    private readonly ICurrentMarket _currentMarket;

    public ProductSearchService(
        IClient searchClient,
        ICurrentMarket currentMarket)
    {
        _searchClient = searchClient;
        _currentMarket = currentMarket;
    }

    // Search products with filters
    public ProductSearchResult Search(ProductSearchRequest request)
    {
        var query = _searchClient.Search&lt;ProductContent&gt;()
            .For(request.Query)
            .Filter(p => p.IsAvailableInCurrentMarket());

        // Category filter
        if (request.CategoryId.HasValue)
        {
            query = query.Filter(p =>
                p.Categories().Match(request.CategoryId.Value));
        }

        // Price filter
        if (request.MinPrice.HasValue || request.MaxPrice.HasValue)
        {
            query = query.Filter(p =>
                p.DefaultPrice().InRange(
                    request.MinPrice ?? 0,
                    request.MaxPrice ?? decimal.MaxValue));
        }

        // Brand filter
        if (request.Brands?.Any() == true)
        {
            query = query.Filter(p =>
                p.Brand.In(request.Brands));
        }

        // Add facets
        query = query
            .TermsFacetFor(p => p.Brand)
            .RangeFacetFor(p => p.DefaultPrice())
            .TermsFacetFor(p => p.Colour);

        // Sorting
        query = request.SortBy switch
        {
            ""price_asc"" => query.OrderBy(p => p.DefaultPrice()),
            ""price_desc"" => query.OrderByDescending(p => p.DefaultPrice()),
            ""name"" => query.OrderBy(p => p.Name),
            _ => query.OrderByRelevance()
        };

        // Execute search
        var results = query
            .Skip(request.Skip)
            .Take(request.Take)
            .GetContentResult();

        return new ProductSearchResult
        {
            Products = results.Items,
            TotalCount = results.TotalMatching,
            Facets = BuildFacets(results)
        };
    }
}</code></pre>

<h3>Faceted Navigation</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class FacetViewModel
{
    public string Name { get; set; }
    public List&lt;FacetValue&gt; Values { get; set; }
}

public class FacetValue
{
    public string Value { get; set; }
    public int Count { get; set; }
    public bool IsSelected { get; set; }
}

// Build facets from search results
private List&lt;FacetViewModel&gt; BuildFacets(
    IContentResult&lt;ProductContent&gt; results)
{
    var facets = new List&lt;FacetViewModel&gt;();

    // Brand facet
    var brandFacet = results.TermsFacetFor(p => p.Brand);
    if (brandFacet != null)
    {
        facets.Add(new FacetViewModel
        {
            Name = ""Brand"",
            Values = brandFacet.Terms
                .Select(t => new FacetValue
                {
                    Value = t.Term,
                    Count = t.Count
                }).ToList()
        });
    }

    return facets;
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-headless-commerce",
                    ModuleId = "advanced-topics",
                    Title = "Headless Commerce with APIs",
                    Summary = "Build headless commerce solutions using Commerce Connect APIs.",
                    Order = 2,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Expose commerce data via APIs",
                        "Build headless storefronts",
                        "Implement cart and checkout APIs",
                        "Handle authentication for APIs"
                    },
                    Content = @"
<h2>Headless Commerce</h2>
<p>Commerce Connect can power headless commerce solutions through its Content Delivery API and custom API endpoints.</p>

<h3>Product API</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[ApiController]
[Route(""api/products"")]
public class ProductApiController : ControllerBase
{
    private readonly IContentLoader _contentLoader;
    private readonly ProductPricingService _pricingService;
    private readonly InventoryService _inventoryService;

    [HttpGet(""{code}"")]
    public ActionResult&lt;ProductDto&gt; GetProduct(string code)
    {
        var product = _productService.GetByCode(code);
        if (product == null)
            return NotFound();

        return Ok(MapToDto(product));
    }

    [HttpGet]
    public ActionResult&lt;PagedResult&lt;ProductDto&gt;&gt; GetProducts(
        [FromQuery] string category,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var products = _productService.GetProducts(category, page, pageSize);

        return Ok(new PagedResult&lt;ProductDto&gt;
        {
            Items = products.Select(MapToDto),
            TotalCount = products.TotalCount,
            Page = page,
            PageSize = pageSize
        });
    }

    private ProductDto MapToDto(ProductContent product)
    {
        var variants = _contentLoader
            .GetChildren&lt;VariationContent&gt;(product.ContentLink);

        return new ProductDto
        {
            Code = product.Code,
            Name = product.DisplayName,
            Description = product.Description?.ToHtmlString(),
            ImageUrl = GetImageUrl(product.MainImage),
            Variants = variants.Select(v => new VariantDto
            {
                Code = v.Code,
                Name = v.DisplayName,
                Price = _pricingService.GetPrice(v),
                InStock = _inventoryService.IsInStock(v.Code)
            }).ToList()
        };
    }
}</code></pre>

<h3>Cart API</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[ApiController]
[Route(""api/cart"")]
public class CartApiController : ControllerBase
{
    private readonly CartService _cartService;

    [HttpGet]
    public ActionResult&lt;CartDto&gt; GetCart()
    {
        var cart = _cartService.GetCurrentCart();
        return Ok(MapToDto(cart));
    }

    [HttpPost(""items"")]
    public ActionResult&lt;CartDto&gt; AddItem([FromBody] AddItemRequest request)
    {
        var cart = _cartService.GetOrCreateCart();
        _cartService.AddToCart(cart, request.Code, request.Quantity);
        return Ok(MapToDto(cart));
    }

    [HttpPut(""items/{code}"")]
    public ActionResult&lt;CartDto&gt; UpdateItem(
        string code,
        [FromBody] UpdateItemRequest request)
    {
        var cart = _cartService.GetCurrentCart();
        _cartService.UpdateQuantity(cart, code, request.Quantity);
        return Ok(MapToDto(cart));
    }

    [HttpDelete(""items/{code}"")]
    public ActionResult&lt;CartDto&gt; RemoveItem(string code)
    {
        var cart = _cartService.GetCurrentCart();
        _cartService.RemoveFromCart(cart, code);
        return Ok(MapToDto(cart));
    }
}</code></pre>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-performance",
                    ModuleId = "advanced-topics",
                    Title = "Performance Optimization",
                    Summary = "Optimize Commerce Connect for high-traffic scenarios.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Cache product data effectively",
                        "Optimize database queries",
                        "Handle high-traffic checkout",
                        "Monitor performance metrics"
                    },
                    Content = @"
<h2>Performance Optimization</h2>
<p>Commerce sites need to handle high traffic efficiently. Here are key strategies for optimizing Commerce Connect.</p>

<h3>Caching Strategies</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Data Type</th>
            <th class=""px-4 py-2 text-left"">Cache Strategy</th>
            <th class=""px-4 py-2 text-left"">Duration</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Product content</td><td class=""px-4 py-2"">CMS output cache</td><td class=""px-4 py-2"">Until publish</td></tr>
        <tr><td class=""px-4 py-2"">Prices</td><td class=""px-4 py-2"">IPriceService cache</td><td class=""px-4 py-2"">Configurable</td></tr>
        <tr><td class=""px-4 py-2"">Inventory</td><td class=""px-4 py-2"">Short TTL cache</td><td class=""px-4 py-2"">30-60 seconds</td></tr>
        <tr><td class=""px-4 py-2"">Shopping cart</td><td class=""px-4 py-2"">No cache (real-time)</td><td class=""px-4 py-2"">N/A</td></tr>
    </tbody>
</table>

<h3>Efficient Data Loading</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class OptimizedProductService
{
    private readonly IContentLoader _contentLoader;
    private readonly IPriceService _priceService;
    private readonly IMemoryCache _cache;

    // Batch load products
    public IEnumerable&lt;ProductViewModel&gt; GetProducts(
        IEnumerable&lt;ContentReference&gt; references)
    {
        // Load all products in one call
        var products = _contentLoader
            .GetItems(references, new LoaderOptions())
            .OfType&lt;ProductContent&gt;();

        // Batch get prices
        var codes = products.SelectMany(p =>
            _contentLoader.GetChildren&lt;VariationContent&gt;(p.ContentLink))
            .Select(v => new CatalogKey(v.Code));

        var prices = _priceService.GetPrices(
            _currentMarket.GetCurrentMarket().MarketId,
            DateTime.UtcNow,
            codes,
            new PriceFilter());

        // Map efficiently
        return products.Select(p => MapWithPrice(p, prices));
    }
}

// Use projections instead of full load
public IEnumerable&lt;ProductSummary&gt; GetProductSummaries(
    ContentReference category)
{
    return _searchClient.Search&lt;ProductContent&gt;()
        .Filter(p => p.Categories().Match(category))
        .Select(p => new ProductSummary
        {
            Code = p.Code,
            Name = p.Name,
            Price = p.DefaultPrice()
        })
        .GetResult();
}</code></pre>

<h3>Best Practices Summary</h3>
<div class=""bg-blue-50 dark:bg-blue-900/20 border-l-4 border-blue-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li>✓ Use <code>IContentLoader</code> batch methods</li>
        <li>✓ Cache expensive calculations</li>
        <li>✓ Use Search for listing pages, not content queries</li>
        <li>✓ Minimize database round-trips</li>
        <li>✓ Use async operations where possible</li>
        <li>✓ Monitor with Application Insights</li>
    </ul>
</div>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-best-practices",
                    ModuleId = "advanced-topics",
                    Title = "Development Best Practices",
                    Summary = "Follow best practices for maintainable Commerce Connect solutions.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Structure commerce projects effectively",
                        "Handle commerce events",
                        "Implement proper error handling",
                        "Write testable commerce code"
                    },
                    Content = @"
<h2>Development Best Practices</h2>
<p>Follow these best practices for building maintainable, scalable Commerce Connect solutions.</p>

<h3>Project Structure</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto"">
MyStore/
├── MyStore.Core/              # Shared business logic
│   ├── Services/             # Business services
│   ├── Extensions/           # Extension methods
│   └── Constants/            # Commerce constants
├── MyStore.Commerce/          # Commerce-specific
│   ├── Models/               # Commerce content types
│   │   ├── Catalog/         # Products, categories
│   │   └── Checkout/        # Order models
│   ├── Services/            # Commerce services
│   ├── Promotions/          # Custom promotions
│   └── PaymentGateways/     # Payment integrations
├── MyStore.Web/               # Web project
│   ├── Features/            # Feature folders
│   │   ├── Cart/
│   │   ├── Checkout/
│   │   └── Product/
│   └── Infrastructure/      # DI, startup
└── MyStore.Tests/             # Test project
</pre>

<h3>Event Handling</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[InitializableModule]
public class CommerceEventsModule : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var events = context.Locate.Advanced
            .GetInstance&lt;IOrderRepositoryCallback&gt;();

        events.PurchaseOrderSaved += OnOrderSaved;
    }

    private void OnOrderSaved(object sender, OrderEventArgs e)
    {
        var order = e.OrderGroup as IPurchaseOrder;
        if (order == null) return;

        // Send confirmation email
        // Update CRM
        // Trigger analytics
    }
}</code></pre>

<h3>Error Handling</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>public class CheckoutService
{
    private readonly ILogger&lt;CheckoutService&gt; _logger;

    public CheckoutResult ProcessCheckout(ICart cart)
    {
        try
        {
            // Validate
            var validation = ValidateCart(cart);
            if (!validation.IsValid)
            {
                return CheckoutResult.ValidationFailed(validation.Errors);
            }

            // Process payment
            var paymentResult = ProcessPayment(cart);
            if (!paymentResult.Success)
            {
                _logger.LogWarning(
                    ""Payment failed for cart {CartId}: {Error}"",
                    cart.OrderLink.OrderGroupId,
                    paymentResult.ErrorMessage);

                return CheckoutResult.PaymentFailed(paymentResult.ErrorMessage);
            }

            // Create order
            var order = CreatePurchaseOrder(cart);

            _logger.LogInformation(
                ""Order {OrderNumber} created successfully"",
                order.OrderNumber);

            return CheckoutResult.Success(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                ""Unexpected error during checkout for cart {CartId}"",
                cart.OrderLink.OrderGroupId);

            return CheckoutResult.UnexpectedError();
        }
    }
}</code></pre>

<h3>Testing Commerce Code</h3>
<pre class=""bg-gray-900 text-green-400 p-4 rounded-lg overflow-x-auto""><code>[TestFixture]
public class CartServiceTests
{
    private Mock&lt;IOrderRepository&gt; _orderRepository;
    private Mock&lt;IOrderGroupFactory&gt; _orderGroupFactory;
    private CartService _sut;

    [SetUp]
    public void Setup()
    {
        _orderRepository = new Mock&lt;IOrderRepository&gt;();
        _orderGroupFactory = new Mock&lt;IOrderGroupFactory&gt;();

        _sut = new CartService(
            _orderRepository.Object,
            _orderGroupFactory.Object,
            Mock.Of&lt;ICurrentMarket&gt;());
    }

    [Test]
    public void AddToCart_NewItem_AddsLineItem()
    {
        // Arrange
        var cart = CreateTestCart();
        var code = ""TEST-001"";
        var quantity = 2;

        // Act
        _sut.AddToCart(cart, code, quantity);

        // Assert
        var lineItem = cart.GetAllLineItems()
            .FirstOrDefault(li => li.Code == code);

        Assert.NotNull(lineItem);
        Assert.AreEqual(quantity, lineItem.Quantity);
    }
}</code></pre>

<h3>Key Takeaways</h3>
<div class=""bg-green-50 dark:bg-green-900/20 border-l-4 border-green-500 p-4 my-4"">
    <ul class=""space-y-2"">
        <li>✓ Use dependency injection consistently</li>
        <li>✓ Keep business logic in services, not controllers</li>
        <li>✓ Handle commerce events for integrations</li>
        <li>✓ Log appropriately for debugging</li>
        <li>✓ Write unit tests for business logic</li>
        <li>✓ Use feature folders for organization</li>
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
