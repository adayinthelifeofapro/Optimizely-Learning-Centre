using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.CMS12;

/// <summary>
/// Content provider for the Optimizely CMS 12 (PaaS) course
/// </summary>
public class CMS12ContentProvider : ILearningContentProvider
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
            BuildContentTypesModule(),
            BuildTemplatesRenderingModule(),
            BuildContentManagementModule(),
            BuildInitializationEventsModule(),
            BuildLocalizationModule(),
            BuildSearchNavigationModule(),
            BuildFormsModule(),
            BuildAccessRightsModule(),
            BuildCachingPerformanceModule(),
            BuildScheduledJobsAdvancedModule(),
            BuildMediaBlobsModule(),
            BuildConfigurationSettingsModule(),
            BuildAddonDevelopmentModule(),
            BuildTestingQAModule(),
            BuildCms11ToCms12MigrationModule()
        };
    }

    #region Module 1: Getting Started

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started with CMS 12",
            Description = "Learn the fundamentals of Optimizely CMS 12, installation, and project setup.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-cms12",
                    ModuleId = "getting-started",
                    Title = "What is Optimizely CMS 12?",
                    Summary = "Discover Optimizely CMS 12 and its capabilities as a .NET-based content management system.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely CMS 12 is and its purpose",
                        "Learn the key features and benefits",
                        "Understand the difference between PaaS and SaaS offerings"
                    },
                    Content = @"
<h2>Introduction to Optimizely CMS 12</h2>
<p>Optimizely CMS 12 (formerly Episerver CMS) is a <strong>powerful enterprise content management system</strong> built on ASP.NET Core. It provides a flexible platform for building websites, intranets, and digital experiences.</p>

<h3>Key Features</h3>
<ul>
    <li><strong>Built on .NET 8+</strong> - Modern, cross-platform, high-performance framework</li>
    <li><strong>Content-first approach</strong> - Strongly-typed content models with rich editing experience</li>
    <li><strong>On-page editing</strong> - WYSIWYG editing directly on the rendered page</li>
    <li><strong>Multi-site support</strong> - Host multiple websites from a single installation</li>
    <li><strong>Multilingual</strong> - Built-in support for content in multiple languages</li>
    <li><strong>Extensible</strong> - Rich API for customization and integration</li>
</ul>

<h3>PaaS vs SaaS</h3>
<p>Optimizely offers two deployment models:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">CMS 12 (PaaS)</th>
            <th class=""px-4 py-2 text-left"">CMS (SaaS)</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Hosting</td><td class=""px-4 py-2"">Self-hosted or DXP Cloud</td><td class=""px-4 py-2"">Fully managed by Optimizely</td></tr>
        <tr><td class=""px-4 py-2"">Architecture</td><td class=""px-4 py-2"">Traditional MVC or headless</td><td class=""px-4 py-2"">Headless-first</td></tr>
        <tr><td class=""px-4 py-2"">Customization</td><td class=""px-4 py-2"">Full code access</td><td class=""px-4 py-2"">Via APIs and configuration</td></tr>
        <tr><td class=""px-4 py-2"">Rendering</td><td class=""px-4 py-2"">Server-side Razor views</td><td class=""px-4 py-2"">Client-side via Graph API</td></tr>
        <tr><td class=""px-4 py-2"">Updates</td><td class=""px-4 py-2"">Manual NuGet upgrades</td><td class=""px-4 py-2"">Automatic, versionless</td></tr>
    </tbody>
</table>

<h3>When to Choose CMS 12 (PaaS)</h3>
<ul>
    <li>You need full control over the codebase and hosting</li>
    <li>You have existing .NET development expertise</li>
    <li>You require server-side rendering for SEO or performance</li>
    <li>You need deep customization of the editorial experience</li>
    <li>You're migrating from an earlier Episerver/Optimizely version</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-prerequisites",
                    ModuleId = "getting-started",
                    Title = "Prerequisites & System Requirements",
                    Summary = "Understand the tools and requirements needed to develop with CMS 12.",
                    Order = 2,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Know the system requirements for CMS 12 development",
                        "Understand which tools to install",
                        "Learn about the Optimizely NuGet feed"
                    },
                    Content = @"
<h2>System Requirements</h2>
<p>Before you begin developing with Optimizely CMS 12, ensure you have the following:</p>

<h3>Development Tools</h3>
<ul>
    <li><strong>.NET 8.0 SDK</strong> (or later) - <a href=""https://dotnet.microsoft.com/download"" target=""_blank"">Download here</a></li>
    <li><strong>Visual Studio 2022</strong> (recommended) or VS Code with C# extension</li>
    <li><strong>SQL Server</strong> - LocalDB (included with VS), SQL Server Express, or full SQL Server</li>
    <li><strong>Node.js</strong> (optional) - For frontend build tools</li>
</ul>

<h3>Optimizely NuGet Feed</h3>
<p>Optimizely packages are hosted on a dedicated NuGet feed. Add this feed to your NuGet configuration:</p>

<h3>Installing the CLI Tools</h3>
<p>The Optimizely CLI helps with project setup and database management:</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "nuget-config",
                            Title = "NuGet.config Setup",
                            Description = "Add the Optimizely NuGet feed to your configuration",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
  <packageSources>
    <add key=""nuget.org"" value=""https://api.nuget.org/v3/index.json"" />
    <add key=""Optimizely"" value=""https://nuget.optimizely.com/feed/packages.svc/"" />
  </packageSources>
</configuration>",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "install-cli",
                            Title = "Install Optimizely CLI",
                            Description = "Install the dotnet CLI tool globally",
                            Type = ExampleType.Code,
                            ExampleContent = @"# Install the Optimizely templates
dotnet new install EPiServer.Templates

# Install the Optimizely CLI tool
dotnet tool install EPiServer.Net.Cli --global

# Verify installation
dotnet-episerver --help",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-create-project",
                    ModuleId = "getting-started",
                    Title = "Creating Your First Project",
                    Summary = "Create a new Optimizely CMS 12 project from scratch.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create a new CMS 12 project using templates",
                        "Understand the project creation options",
                        "Run the initial database setup"
                    },
                    Content = @"
<h2>Creating a New Project</h2>
<p>Optimizely provides project templates that set up a working CMS installation. You can create either an empty project or use the Alloy sample site.</p>

<h3>Option 1: Empty CMS Project</h3>
<p>An empty project gives you a clean starting point with minimal content types:</p>

<h3>Option 2: Alloy Sample Site</h3>
<p>The Alloy template includes sample content types, pages, and styling - great for learning:</p>

<h3>Database Setup</h3>
<p>After creating the project, set up the database using the CLI:</p>

<h3>Running the Site</h3>
<p>Once the database is created, run the project:</p>
<ol>
    <li>Navigate to <code>https://localhost:5001</code> (or the configured port)</li>
    <li>You'll be prompted to create an admin user on first run</li>
    <li>Access the CMS edit interface at <code>/episerver/cms</code></li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "create-empty-project",
                            Title = "Create Empty Project",
                            Description = "Create a new empty CMS 12 project",
                            Type = ExampleType.Code,
                            ExampleContent = @"# Create a new empty CMS project
dotnet new epicmsempty --name MyOptimizely

# Navigate to the project
cd MyOptimizely

# Restore packages
dotnet restore",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "create-alloy-project",
                            Title = "Create Alloy Sample Site",
                            Description = "Create a project with sample content and styling",
                            Type = ExampleType.Code,
                            ExampleContent = @"# Create a new Alloy sample site
dotnet new epi-alloy-mvc --name MyAlloySite

# Navigate to the project
cd MyAlloySite

# Restore packages
dotnet restore",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "setup-database",
                            Title = "Database Setup",
                            Description = "Create and configure the CMS database",
                            Type = ExampleType.Code,
                            ExampleContent = @"# Create the CMS database (uses connection string from appsettings.json)
dotnet-episerver create-cms-database

# Or specify a custom connection string
dotnet-episerver create-cms-database -c ""Server=(localdb)\\MSSQLLocalDB;Database=MyOptimizely;Integrated Security=True""

# Run the application
dotnet run",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-project-structure",
                    ModuleId = "getting-started",
                    Title = "Project Structure Explained",
                    Summary = "Understand the folder structure and key files in a CMS 12 project.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the recommended project structure",
                        "Know the purpose of key configuration files",
                        "Learn where to place different types of code"
                    },
                    Content = @"
<h2>CMS 12 Project Structure</h2>
<p>A well-organized project structure helps maintain clean, scalable code. Here's the recommended layout:</p>

<h3>Folder Structure</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto"">
MyOptimizely/
├── App_Data/                 # Database files, logs, blob storage
├── Business/                 # Business logic, services, helpers
│   ├── Initialization/      # Initialization modules
│   ├── Rendering/           # Custom renderers
│   └── Services/            # Custom services
├── Controllers/              # MVC controllers
├── Models/                   # Content types and view models
│   ├── Blocks/              # Block content types
│   ├── Media/               # Media content types
│   ├── Pages/               # Page content types
│   └── ViewModels/          # View models
├── Views/                    # Razor views
│   ├── Blocks/              # Block partial views
│   ├── Pages/               # Page views
│   └── Shared/              # Layouts and shared partials
├── wwwroot/                  # Static files (CSS, JS, images)
├── appsettings.json          # Application configuration
├── Program.cs                # Application entry point
└── Startup.cs                # Service configuration (if used)
</pre>

<h3>Key Configuration Files</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">File</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">appsettings.json</td><td class=""px-4 py-2"">Connection strings, logging, CMS options</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Program.cs</td><td class=""px-4 py-2"">Application bootstrap, service registration</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">module.config</td><td class=""px-4 py-2"">Module dependencies (for add-ons)</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "appsettings-example",
                            Title = "appsettings.json",
                            Description = "Typical CMS 12 configuration file",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""ConnectionStrings"": {
    ""EPiServerDB"": ""Server=(localdb)\\MSSQLLocalDB;Database=MyOptimizely;Integrated Security=True;MultipleActiveResultSets=True""
  },
  ""EPiServer"": {
    ""Cms"": {
      ""MappedRoles"": {
        ""CmsAdmins"": [""WebAdmins"", ""Administrators""],
        ""CmsEditors"": [""WebEditors""]
      }
    }
  },
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Warning"",
      ""EPiServer"": ""Warning""
    }
  }
}",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "program-cs-example",
                            Title = "Program.cs",
                            Description = "Minimal CMS 12 Program.cs setup",
                            Type = ExampleType.Code,
                            ExampleContent = @"var builder = WebApplication.CreateBuilder(args);

// Add Optimizely CMS services
builder.Services.AddCms();

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapContent();
app.MapControllers();

app.Run();",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-edit-interface",
                    ModuleId = "getting-started",
                    Title = "The Editorial Interface",
                    Summary = "Navigate the CMS edit interface and understand the editorial experience.",
                    Order = 5,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the CMS editorial interface",
                        "Understand the page tree and content structure",
                        "Learn about on-page editing"
                    },
                    Content = @"
<h2>CMS Editorial Interface</h2>
<p>The Optimizely CMS editorial interface provides a powerful environment for content editors to create and manage content.</p>

<h3>Accessing the Interface</h3>
<p>Navigate to <code>/episerver/cms</code> to access the editorial interface. You'll need to log in with an account that has editor permissions.</p>

<h3>Key Areas</h3>
<ul>
    <li><strong>Page Tree</strong> - Hierarchical view of all pages on the site</li>
    <li><strong>Assets Panel</strong> - Manage media files and shared blocks</li>
    <li><strong>Properties Panel</strong> - Edit content properties in forms view</li>
    <li><strong>On-Page Editing</strong> - Edit content directly on the rendered page</li>
    <li><strong>Version History</strong> - View and restore previous versions</li>
    <li><strong>Publishing</strong> - Publish content or schedule for future publication</li>
</ul>

<h3>Edit Modes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Mode</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">On-Page Edit</td><td class=""px-4 py-2"">Edit content directly on the rendered page with inline editing</td></tr>
        <tr><td class=""px-4 py-2"">All Properties</td><td class=""px-4 py-2"">Form-based view showing all content properties</td></tr>
        <tr><td class=""px-4 py-2"">Preview</td><td class=""px-4 py-2"">View the page as visitors will see it</td></tr>
        <tr><td class=""px-4 py-2"">Compare</td><td class=""px-4 py-2"">Compare different versions side by side</td></tr>
    </tbody>
</table>

<h3>Content Status</h3>
<p>Content in the CMS can have different statuses:</p>
<ul>
    <li><strong>Not Published</strong> - Content has never been published</li>
    <li><strong>Published</strong> - Content is live on the site</li>
    <li><strong>Previously Published</strong> - Changes have been made but not yet published</li>
    <li><strong>Scheduled</strong> - Content will be published at a future date</li>
    <li><strong>Expired</strong> - Content has passed its expiration date</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 2: Content Types

    private LearningModule BuildContentTypesModule()
    {
        return new LearningModule
        {
            Id = "content-types",
            Title = "Content Types",
            Description = "Learn to create and configure page types, block types, and media types.",
            Icon = "document-duplicate",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ct-understanding-content",
                    ModuleId = "content-types",
                    Title = "Understanding Content Types",
                    Summary = "Learn the fundamentals of content types in Optimizely CMS.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what content types are",
                        "Know the different categories of content types",
                        "Learn how content types map to .NET classes"
                    },
                    Content = @"
<h2>What are Content Types?</h2>
<p>In Optimizely CMS, a <strong>content type</strong> defines the structure and properties of content. Think of it as a template that defines what data a piece of content can hold.</p>

<h3>The Content Type Hierarchy</h3>
<ul>
    <li><strong>IContent</strong> - The base interface for all content</li>
    <li><strong>IContentData</strong> - Adds property access to content</li>
    <li><strong>PageData</strong> - Base class for pages (routable content)</li>
    <li><strong>BlockData</strong> - Base class for blocks (reusable content components)</li>
    <li><strong>MediaData</strong> - Base class for media files</li>
</ul>

<h3>Content Type Categories</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Base Class</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Pages</td><td class=""px-4 py-2 font-mono text-sm"">PageData</td><td class=""px-4 py-2"">Routable content with URLs (articles, landing pages)</td></tr>
        <tr><td class=""px-4 py-2"">Blocks</td><td class=""px-4 py-2 font-mono text-sm"">BlockData</td><td class=""px-4 py-2"">Reusable content components (teasers, banners)</td></tr>
        <tr><td class=""px-4 py-2"">Media</td><td class=""px-4 py-2 font-mono text-sm"">MediaData</td><td class=""px-4 py-2"">Files with metadata (images, documents, videos)</td></tr>
        <tr><td class=""px-4 py-2"">Folders</td><td class=""px-4 py-2 font-mono text-sm"">ContentFolder</td><td class=""px-4 py-2"">Organize content in the tree</td></tr>
    </tbody>
</table>

<h3>How Content Types Work</h3>
<ol>
    <li>You define a .NET class inheriting from the appropriate base class</li>
    <li>You decorate it with the <code>[ContentType]</code> attribute</li>
    <li>CMS scans for these classes during startup</li>
    <li>For each class, a content type definition is created in the database</li>
    <li>Editors can then create instances of your content type</li>
</ol>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ct-creating-pages",
                    ModuleId = "content-types",
                    Title = "Creating Page Types",
                    Summary = "Learn how to create page types that editors can use to build your site.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create a basic page type",
                        "Use the ContentType attribute",
                        "Define page properties"
                    },
                    Content = @"
<h2>Creating Page Types</h2>
<p>Page types are the foundation of your site structure. Each page type represents a different kind of page (e.g., article, product, landing page).</p>

<h3>Basic Page Type</h3>
<p>A page type is a class that:</p>
<ul>
    <li>Inherits from <code>PageData</code></li>
    <li>Is decorated with <code>[ContentType]</code></li>
    <li>Has public virtual properties for content</li>
</ul>

<h3>Important Attributes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[ContentType]</td><td class=""px-4 py-2"">Marks the class as a content type with display name and description</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">GUID</td><td class=""px-4 py-2"">Unique identifier - allows renaming the class without losing data</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">GroupName</td><td class=""px-4 py-2"">Groups content types in the ""New Page"" dialog</td></tr>
    </tbody>
</table>

<h3>Why Properties Must Be Virtual</h3>
<p>Properties must be declared as <code>virtual</code> because CMS creates a proxy class that overrides them to read/write from the underlying data store.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "basic-page-type",
                            Title = "Article Page Type",
                            Description = "A complete page type for articles",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MyOptimizely.Models.Pages
{
    [ContentType(
        GUID = ""a1b2c3d4-e5f6-7890-abcd-ef1234567890"",
        DisplayName = ""Article Page"",
        Description = ""A page for articles and blog posts"",
        GroupName = ""Content"")]
    public class ArticlePage : PageData
    {
        [CultureSpecific]
        [Display(
            Name = ""Title"",
            Description = ""The main title of the article"",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string? Title { get; set; }

        [CultureSpecific]
        [Display(
            Name = ""Introduction"",
            Description = ""A short introduction to the article"",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual string? Introduction { get; set; }

        [CultureSpecific]
        [Display(
            Name = ""Main Body"",
            Description = ""The main content of the article"",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual XhtmlString? MainBody { get; set; }

        [Display(
            Name = ""Published Date"",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual DateTime PublishedDate { get; set; }
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ct-creating-blocks",
                    ModuleId = "content-types",
                    Title = "Creating Block Types",
                    Summary = "Create reusable content blocks that can be placed on pages.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of blocks",
                        "Create block types",
                        "Know when to use blocks vs pages"
                    },
                    Content = @"
<h2>Creating Block Types</h2>
<p>Blocks are reusable content components that can be placed in content areas on pages. They're perfect for:</p>
<ul>
    <li>Teasers and promotional banners</li>
    <li>Call-to-action components</li>
    <li>Navigation elements</li>
    <li>Content that appears on multiple pages</li>
</ul>

<h3>Block vs Page</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Page</th>
            <th class=""px-4 py-2 text-left"">Block</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Has URL</td><td class=""px-4 py-2"">Yes</td><td class=""px-4 py-2"">No (rendered within pages)</td></tr>
        <tr><td class=""px-4 py-2"">Location</td><td class=""px-4 py-2"">Page tree</td><td class=""px-4 py-2"">Assets panel or inline in pages</td></tr>
        <tr><td class=""px-4 py-2"">Reusability</td><td class=""px-4 py-2"">Single location</td><td class=""px-4 py-2"">Can be shared across pages</td></tr>
        <tr><td class=""px-4 py-2"">Base class</td><td class=""px-4 py-2"">PageData</td><td class=""px-4 py-2"">BlockData</td></tr>
    </tbody>
</table>

<h3>Shared vs Local Blocks</h3>
<ul>
    <li><strong>Shared blocks</strong> - Created in Assets panel, can be used on multiple pages</li>
    <li><strong>Local blocks</strong> - Created inline in a ContentArea, belong to that page only</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "teaser-block",
                            Title = "Teaser Block Type",
                            Description = "A promotional teaser block",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MyOptimizely.Models.Blocks
{
    [ContentType(
        GUID = ""b2c3d4e5-f6a7-8901-bcde-f23456789012"",
        DisplayName = ""Teaser Block"",
        Description = ""A promotional teaser with image and link"",
        GroupName = ""Content"")]
    public class TeaserBlock : BlockData
    {
        [CultureSpecific]
        [Display(
            Name = ""Heading"",
            Description = ""The teaser heading"",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string? Heading { get; set; }

        [CultureSpecific]
        [Display(
            Name = ""Text"",
            Description = ""The teaser text"",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [UIHint(UIHint.Textarea)]
        public virtual string? Text { get; set; }

        [Display(
            Name = ""Image"",
            Description = ""The teaser image"",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference? Image { get; set; }

        [Display(
            Name = ""Link"",
            Description = ""The link destination"",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual Url? Link { get; set; }

        [Display(
            Name = ""Link Text"",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        public virtual string? LinkText { get; set; }
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ct-property-types",
                    ModuleId = "content-types",
                    Title = "Built-in Property Types",
                    Summary = "Explore the built-in property types available for content modeling.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Know the built-in property types",
                        "Choose the right property type for your data",
                        "Use UIHints to customize editing experience"
                    },
                    Content = @"
<h2>Built-in Property Types</h2>
<p>Optimizely CMS provides many built-in property types for common data scenarios.</p>

<h3>Text Properties</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Editor</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">string</td><td class=""px-4 py-2"">Single-line text</td><td class=""px-4 py-2"">Titles, short text</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">string + UIHint.Textarea</td><td class=""px-4 py-2"">Multi-line text</td><td class=""px-4 py-2"">Descriptions, summaries</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">XhtmlString</td><td class=""px-4 py-2"">Rich text (TinyMCE)</td><td class=""px-4 py-2"">Main body content</td></tr>
    </tbody>
</table>

<h3>Reference Properties</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ContentReference</td><td class=""px-4 py-2"">Reference to a single content item</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ContentArea</td><td class=""px-4 py-2"">Collection of content items (drag & drop)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Url</td><td class=""px-4 py-2"">Internal or external URL</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">LinkItemCollection</td><td class=""px-4 py-2"">Collection of links with text</td></tr>
    </tbody>
</table>

<h3>Other Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">int, double</td><td class=""px-4 py-2"">Numeric values</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">bool</td><td class=""px-4 py-2"">Checkbox (true/false)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">DateTime</td><td class=""px-4 py-2"">Date and time picker</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">CategoryList</td><td class=""px-4 py-2"">Category selection</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "property-examples",
                            Title = "Property Type Examples",
                            Description = "Various property types in action",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class ProductPage : PageData
{
    // Simple string (single-line text)
    [Display(Name = ""Product Name"")]
    public virtual string? ProductName { get; set; }

    // Multi-line text (textarea)
    [UIHint(UIHint.Textarea)]
    [Display(Name = ""Short Description"")]
    public virtual string? ShortDescription { get; set; }

    // Rich text editor
    [Display(Name = ""Full Description"")]
    public virtual XhtmlString? FullDescription { get; set; }

    // Single content reference (e.g., to an image)
    [UIHint(UIHint.Image)]
    [Display(Name = ""Main Image"")]
    public virtual ContentReference? MainImage { get; set; }

    // Content area for blocks
    [Display(Name = ""Related Content"")]
    public virtual ContentArea? RelatedContent { get; set; }

    // Price (decimal)
    [Display(Name = ""Price"")]
    public virtual decimal Price { get; set; }

    // Featured flag
    [Display(Name = ""Is Featured"")]
    public virtual bool IsFeatured { get; set; }

    // Release date
    [Display(Name = ""Release Date"")]
    public virtual DateTime? ReleaseDate { get; set; }

    // External link
    [Display(Name = ""Manufacturer Website"")]
    public virtual Url? ManufacturerUrl { get; set; }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ct-attributes",
                    ModuleId = "content-types",
                    Title = "Property Attributes",
                    Summary = "Control property behavior with attributes.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use Display attribute to control appearance",
                        "Apply validation attributes",
                        "Restrict content types with AllowedTypes"
                    },
                    Content = @"
<h2>Property Attributes</h2>
<p>Attributes control how properties behave in the editor and validate input.</p>

<h3>Display Attribute</h3>
<p>Controls how the property appears in the editor:</p>
<ul>
    <li><strong>Name</strong> - Display name in the editor</li>
    <li><strong>Description</strong> - Help text shown to editors</li>
    <li><strong>GroupName</strong> - Tab where property appears</li>
    <li><strong>Order</strong> - Sort order within the tab</li>
</ul>

<h3>Common Attributes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[CultureSpecific]</td><td class=""px-4 py-2"">Property has different values per language</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[Required]</td><td class=""px-4 py-2"">Property must have a value</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[StringLength(max)]</td><td class=""px-4 py-2"">Maximum character length</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[Range(min, max)]</td><td class=""px-4 py-2"">Numeric range validation</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[UIHint(hint)]</td><td class=""px-4 py-2"">Custom editor hint</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[AllowedTypes]</td><td class=""px-4 py-2"">Restrict allowed content types</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[Searchable]</td><td class=""px-4 py-2"">Include in search index</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "attribute-examples",
                            Title = "Common Attribute Usage",
                            Description = "Examples of property attributes",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class ArticlePage : PageData
{
    // Culture-specific with validation
    [CultureSpecific]
    [Required]
    [StringLength(100, MinimumLength = 5)]
    [Display(Name = ""Title"", Order = 10)]
    public virtual string? Title { get; set; }

    // Grouped in custom tab with order
    [Display(
        Name = ""SEO Title"",
        Description = ""Title shown in search results"",
        GroupName = ""SEO"",
        Order = 10)]
    [StringLength(60)]
    public virtual string? SeoTitle { get; set; }

    // Content area restricted to specific block types
    [AllowedTypes(typeof(TeaserBlock), typeof(ImageBlock))]
    [Display(Name = ""Sidebar Content"", Order = 100)]
    public virtual ContentArea? Sidebar { get; set; }

    // Content reference restricted to images only
    [UIHint(UIHint.Image)]
    [AllowedTypes(typeof(ImageFile))]
    [Display(Name = ""Hero Image"")]
    public virtual ContentReference? HeroImage { get; set; }

    // Numeric with range
    [Range(1, 10)]
    [Display(Name = ""Priority"")]
    public virtual int Priority { get; set; }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ct-media-types",
                    ModuleId = "content-types",
                    Title = "Media Types",
                    Summary = "Create custom media types for images, videos, and documents.",
                    Order = 6,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the media type system",
                        "Create custom image and video types",
                        "Add metadata to media files"
                    },
                    Content = @"
<h2>Media Types</h2>
<p>Media types allow you to add custom properties to uploaded files like images, videos, and documents.</p>

<h3>Built-in Media Interfaces</h3>
<ul>
    <li><strong>IContentMedia</strong> - Base interface for all media</li>
    <li><strong>IContentImage</strong> - For image files</li>
    <li><strong>IContentVideo</strong> - For video files</li>
</ul>

<h3>Media Type Descriptors</h3>
<p>Use <code>[MediaDescriptor]</code> to specify which file extensions your media type handles.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "image-media-type",
                            Title = "Custom Image Type",
                            Description = "Image type with alt text and copyright",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MyOptimizely.Models.Media
{
    [ContentType(
        GUID = ""c3d4e5f6-a7b8-9012-cdef-345678901234"",
        DisplayName = ""Image File"")]
    [MediaDescriptor(ExtensionString = ""jpg,jpeg,png,gif,webp,svg"")]
    public class ImageFile : ImageData
    {
        [CultureSpecific]
        [Display(
            Name = ""Alt Text"",
            Description = ""Alternative text for accessibility"",
            Order = 10)]
        public virtual string? AltText { get; set; }

        [Display(
            Name = ""Copyright"",
            Description = ""Image copyright information"",
            Order = 20)]
        public virtual string? Copyright { get; set; }

        [Display(
            Name = ""Photographer"",
            Order = 30)]
        public virtual string? Photographer { get; set; }
    }

    [ContentType(
        GUID = ""d4e5f6a7-b8c9-0123-defa-456789012345"",
        DisplayName = ""Video File"")]
    [MediaDescriptor(ExtensionString = ""mp4,webm,ogg"")]
    public class VideoFile : VideoData
    {
        [Display(Name = ""Thumbnail"")]
        [UIHint(UIHint.Image)]
        public virtual ContentReference? Thumbnail { get; set; }

        [Display(Name = ""Duration (seconds)"")]
        public virtual int? Duration { get; set; }

        [CultureSpecific]
        [Display(Name = ""Transcript"")]
        public virtual XhtmlString? Transcript { get; set; }
    }
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 3: Templates & Rendering

    private LearningModule BuildTemplatesRenderingModule()
    {
        return new LearningModule
        {
            Id = "templates-rendering",
            Title = "Templates & Rendering",
            Description = "Learn MVC patterns, controllers, and views for rendering content.",
            Icon = "code-bracket",
            Order = 3,
            Difficulty = ModuleDifficulty.Beginner,
            Prerequisites = new[] { "content-types" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "tr-mvc-overview",
                    ModuleId = "templates-rendering",
                    Title = "MVC Architecture in CMS 12",
                    Summary = "Understand how MVC patterns work with Optimizely CMS.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the MVC pattern in CMS context",
                        "Know how content routing works",
                        "Learn the template resolution process"
                    },
                    Content = @"
<h2>MVC in Optimizely CMS</h2>
<p>Optimizely CMS 12 uses ASP.NET Core MVC for rendering content. The MVC pattern separates:</p>
<ul>
    <li><strong>Model</strong> - Your content types (PageData, BlockData)</li>
    <li><strong>View</strong> - Razor templates that render HTML</li>
    <li><strong>Controller</strong> - Handles requests and prepares data for views</li>
</ul>

<h3>Content Routing</h3>
<p>When a request comes in, CMS uses content routing to:</p>
<ol>
    <li>Match the URL to a page in the content tree</li>
    <li>Find the appropriate controller for that page type</li>
    <li>Execute the controller action</li>
    <li>Render the matching view</li>
</ol>

<h3>Template Resolution</h3>
<p>CMS uses conventions to find templates:</p>
<ul>
    <li>Controller: <code>{PageTypeName}Controller</code> or <code>DefaultPageController</code></li>
    <li>View: <code>Views/{ControllerName}/Index.cshtml</code> or <code>Views/Pages/{PageTypeName}.cshtml</code></li>
</ul>

<h3>Enabling Content Routing</h3>
<p>Content routing is enabled in Program.cs with <code>app.MapContent()</code></p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "map-content",
                            Title = "Enable Content Routing",
                            Description = "Program.cs configuration for content routing",
                            Type = ExampleType.Code,
                            ExampleContent = @"var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Enable Optimizely content routing
app.MapContent();

// Also map regular MVC controllers
app.MapControllers();

app.Run();",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tr-page-controllers",
                    ModuleId = "templates-rendering",
                    Title = "Creating Page Controllers",
                    Summary = "Create controllers that handle page rendering.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create a page controller",
                        "Use PageController<T> base class",
                        "Pass data to views"
                    },
                    Content = @"
<h2>Page Controllers</h2>
<p>Page controllers handle requests for specific page types. They inherit from <code>PageController&lt;T&gt;</code> where T is your page type.</p>

<h3>Controller Conventions</h3>
<ul>
    <li>Name: <code>{PageTypeName}Controller</code></li>
    <li>Location: <code>Controllers/</code> folder</li>
    <li>Base class: <code>PageController&lt;T&gt;</code></li>
    <li>Action: Usually <code>Index</code> for main page rendering</li>
</ul>

<h3>The CurrentPage Property</h3>
<p>The base class provides <code>CurrentPage</code> property containing the page being rendered. This is automatically populated by the CMS routing.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "article-controller",
                            Title = "Article Page Controller",
                            Description = "Controller for the ArticlePage type",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MyOptimizely.Models.Pages;

namespace MyOptimizely.Controllers
{
    public class ArticlePageController : PageController<ArticlePage>
    {
        public IActionResult Index(ArticlePage currentPage)
        {
            // CurrentPage is also available via the base class
            // currentPage == CurrentPage

            // You can perform additional logic here
            // e.g., load related articles, prepare view models

            return View(currentPage);
        }
    }
}",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "controller-with-viewmodel",
                            Title = "Controller with View Model",
                            Description = "Using a view model to pass additional data",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class ArticlePageController : PageController<ArticlePage>
{
    private readonly IContentLoader _contentLoader;

    public ArticlePageController(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    public IActionResult Index(ArticlePage currentPage)
    {
        var viewModel = new ArticleViewModel
        {
            Page = currentPage,
            RelatedArticles = GetRelatedArticles(currentPage),
            PublishedDateFormatted = currentPage.PublishedDate.ToString(""MMMM dd, yyyy"")
        };

        return View(viewModel);
    }

    private IEnumerable<ArticlePage> GetRelatedArticles(ArticlePage page)
    {
        // Load related articles logic
        return _contentLoader
            .GetChildren<ArticlePage>(page.ParentLink)
            .Where(a => a.ContentLink != page.ContentLink)
            .Take(3);
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tr-views",
                    ModuleId = "templates-rendering",
                    Title = "Creating Views",
                    Summary = "Create Razor views to render content.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create Razor views for page types",
                        "Use Html.PropertyFor for on-page editing",
                        "Understand view conventions"
                    },
                    Content = @"
<h2>Razor Views</h2>
<p>Views are Razor templates (.cshtml) that render your content as HTML.</p>

<h3>View Conventions</h3>
<p>Views should be placed in:</p>
<ul>
    <li><code>Views/{ControllerName}/Index.cshtml</code> - For controller-based routing</li>
    <li><code>Views/Pages/{PageTypeName}.cshtml</code> - Convention-based routing</li>
</ul>

<h3>Html.PropertyFor</h3>
<p>Use <code>Html.PropertyFor()</code> to render properties with on-page editing support:</p>
<ul>
    <li>Automatically wraps content in edit markers</li>
    <li>Enables inline editing in edit mode</li>
    <li>Works with all property types</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "article-view",
                            Title = "Article Page View",
                            Description = "Razor view with on-page editing",
                            Type = ExampleType.Code,
                            ExampleContent = @"@model MyOptimizely.Models.Pages.ArticlePage
@using EPiServer.Web.Mvc.Html

@{
    Layout = ""~/Views/Shared/_Layout.cshtml"";
}

<article class=""article"">
    <header>
        @* PropertyFor enables on-page editing *@
        <h1>@Html.PropertyFor(m => m.Title)</h1>

        <p class=""article-meta"">
            Published: @Model.PublishedDate.ToString(""MMMM dd, yyyy"")
        </p>
    </header>

    @if (!string.IsNullOrEmpty(Model.Introduction))
    {
        <div class=""article-intro"">
            @Html.PropertyFor(m => m.Introduction)
        </div>
    }

    <div class=""article-body"">
        @* XhtmlString is automatically rendered as HTML *@
        @Html.PropertyFor(m => m.MainBody)
    </div>
</article>",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tr-block-rendering",
                    ModuleId = "templates-rendering",
                    Title = "Rendering Blocks",
                    Summary = "Create view components and views to render blocks.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create block view components",
                        "Create partial views for blocks",
                        "Render ContentArea properties"
                    },
                    Content = @"
<h2>Rendering Blocks</h2>
<p>Blocks can be rendered using either view components or simple partial views.</p>

<h3>Option 1: Partial View Only</h3>
<p>The simplest approach - just create a partial view:</p>
<ul>
    <li>Location: <code>Views/Shared/Blocks/{BlockTypeName}.cshtml</code></li>
    <li>No controller needed</li>
    <li>Block instance passed directly as model</li>
</ul>

<h3>Option 2: View Component</h3>
<p>For blocks that need additional logic:</p>
<ul>
    <li>Inherit from <code>BlockComponent&lt;T&gt;</code></li>
    <li>Override <code>InvokeComponent</code> method</li>
    <li>Create associated view in <code>Views/Shared/Components/{ComponentName}/Default.cshtml</code></li>
</ul>

<h3>Rendering ContentArea</h3>
<p>Use <code>Html.PropertyFor()</code> on ContentArea properties to render all blocks inside:</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "block-partial-view",
                            Title = "Block Partial View",
                            Description = "Simple partial view for TeaserBlock",
                            Type = ExampleType.Code,
                            ExampleContent = @"@* Views/Shared/Blocks/TeaserBlock.cshtml *@
@model MyOptimizely.Models.Blocks.TeaserBlock
@using EPiServer.Web.Mvc.Html

<div class=""teaser"">
    @if (Model.Image != null)
    {
        <img src=""@Url.ContentUrl(Model.Image)"" alt=""@Model.Heading"" />
    }

    <h3>@Html.PropertyFor(m => m.Heading)</h3>
    <p>@Html.PropertyFor(m => m.Text)</p>

    @if (Model.Link != null)
    {
        <a href=""@Model.Link"" class=""teaser-link"">
            @(Model.LinkText ?? ""Read more"")
        </a>
    }
</div>",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "block-component",
                            Title = "Block View Component",
                            Description = "View component with additional logic",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Components/TeaserBlockComponent.cs
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MyOptimizely.Models.Blocks;

public class TeaserBlockComponent : BlockComponent<TeaserBlock>
{
    private readonly IContentLoader _contentLoader;

    public TeaserBlockComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(TeaserBlock currentBlock)
    {
        var viewModel = new TeaserViewModel
        {
            Block = currentBlock,
            ImageUrl = GetImageUrl(currentBlock.Image)
        };

        return View(viewModel);
    }

    private string? GetImageUrl(ContentReference? imageRef)
    {
        if (imageRef == null) return null;
        // Custom image URL logic
        return $""/contentassets/{imageRef.ID}"";
    }
}",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "render-content-area",
                            Title = "Rendering ContentArea",
                            Description = "Render all blocks in a ContentArea",
                            Type = ExampleType.Code,
                            ExampleContent = @"@* In a page view *@
@model MyOptimizely.Models.Pages.StartPage

<main>
    <div class=""hero-section"">
        @Html.PropertyFor(m => m.HeroArea)
    </div>

    <div class=""main-content"">
        @Html.PropertyFor(m => m.MainContentArea)
    </div>

    <aside class=""sidebar"">
        @* You can also render with custom tag and CSS class *@
        @Html.PropertyFor(m => m.SidebarArea, new {
            Tag = ""aside"",
            CssClass = ""sidebar-blocks""
        })
    </aside>
</main>",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tr-layouts-partials",
                    ModuleId = "templates-rendering",
                    Title = "Layouts and Partial Views",
                    Summary = "Create shared layouts and reusable partial views.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Create shared layout templates",
                        "Use partial views for reusable components",
                        "Understand section rendering"
                    },
                    Content = @"
<h2>Layouts</h2>
<p>Layouts define the common HTML structure shared across pages (header, footer, navigation).</p>

<h3>Layout Location</h3>
<p>Standard location: <code>Views/Shared/_Layout.cshtml</code></p>

<h3>Key Layout Features</h3>
<ul>
    <li><code>@RenderBody()</code> - Where page content is inserted</li>
    <li><code>@RenderSection()</code> - Named sections for scripts, styles</li>
    <li><code>@Html.RequiredClientResources()</code> - CMS editor scripts</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "layout-example",
                            Title = "Base Layout",
                            Description = "Shared layout with CMS support",
                            Type = ExampleType.Code,
                            ExampleContent = @"@* Views/Shared/_Layout.cshtml *@
@using EPiServer.Web.Mvc.Html

<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <title>@ViewBag.Title - My Site</title>
    <link rel=""stylesheet"" href=""~/css/site.css"" />
    @RenderSection(""Styles"", required: false)
    @* Required for on-page editing *@
    @Html.RequiredClientResources(""Header"")
</head>
<body>
    <header>
        @await Html.PartialAsync(""_Navigation"")
    </header>

    <main>
        @RenderBody()
    </main>

    <footer>
        @await Html.PartialAsync(""_Footer"")
    </footer>

    <script src=""~/js/site.js""></script>
    @RenderSection(""Scripts"", required: false)
    @* Required for on-page editing *@
    @Html.RequiredClientResources(""Footer"")
</body>
</html>",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 4: Content Management

    private LearningModule BuildContentManagementModule()
    {
        return new LearningModule
        {
            Id = "content-management",
            Title = "Content Management APIs",
            Description = "Learn to programmatically manage content using IContentRepository and IContentLoader.",
            Icon = "circle-stack",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "templates-rendering" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cm-loading-content",
                    ModuleId = "content-management",
                    Title = "Loading Content",
                    Summary = "Learn to load content using IContentLoader.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use IContentLoader to retrieve content",
                        "Load content by reference, parent, or ancestor",
                        "Understand language-specific loading"
                    },
                    Content = @"
<h2>Loading Content with IContentLoader</h2>
<p><code>IContentLoader</code> provides read-only access to content. It's the primary way to load content in your code.</p>

<h3>Key Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Get&lt;T&gt;(ContentReference)</td><td class=""px-4 py-2"">Load single content item</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">GetChildren&lt;T&gt;(ContentReference)</td><td class=""px-4 py-2"">Load immediate children</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">GetAncestors(ContentReference)</td><td class=""px-4 py-2"">Load all ancestors (for breadcrumbs)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">GetDescendents(ContentReference)</td><td class=""px-4 py-2"">Load all descendant references</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">TryGet&lt;T&gt;(ContentReference, out T)</td><td class=""px-4 py-2"">Safe loading (returns false if not found)</td></tr>
    </tbody>
</table>

<h3>IContentLoader vs IContentRepository</h3>
<ul>
    <li><strong>IContentLoader</strong> - Read-only operations, use for rendering</li>
    <li><strong>IContentRepository</strong> - Read and write operations, use for modifications</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "content-loader-examples",
                            Title = "IContentLoader Examples",
                            Description = "Common content loading patterns",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class ContentService
{
    private readonly IContentLoader _contentLoader;

    public ContentService(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    // Load a specific page by ID
    public ArticlePage? GetArticle(int contentId)
    {
        var reference = new ContentReference(contentId);
        return _contentLoader.Get<ArticlePage>(reference);
    }

    // Safe loading with TryGet
    public ArticlePage? GetArticleSafe(ContentReference reference)
    {
        if (_contentLoader.TryGet<ArticlePage>(reference, out var article))
        {
            return article;
        }
        return null;
    }

    // Get all child pages of a specific type
    public IEnumerable<ArticlePage> GetChildArticles(ContentReference parentRef)
    {
        return _contentLoader.GetChildren<ArticlePage>(parentRef);
    }

    // Build breadcrumb trail
    public IEnumerable<PageData> GetBreadcrumbs(ContentReference pageRef)
    {
        return _contentLoader.GetAncestors(pageRef)
            .OfType<PageData>()
            .Reverse();
    }

    // Load content in a specific language
    public ArticlePage? GetArticleInLanguage(ContentReference reference, string language)
    {
        var culture = new CultureInfo(language);
        return _contentLoader.Get<ArticlePage>(
            reference,
            new LanguageSelector(culture));
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-creating-content",
                    ModuleId = "content-management",
                    Title = "Creating and Saving Content",
                    Summary = "Learn to create, modify, and save content programmatically.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create new content instances",
                        "Save content with different save actions",
                        "Understand writable clones"
                    },
                    Content = @"
<h2>Creating Content with IContentRepository</h2>
<p><code>IContentRepository</code> extends IContentLoader with write operations.</p>

<h3>Creating Content</h3>
<ol>
    <li>Use <code>GetDefault&lt;T&gt;(parentRef)</code> to create a new instance</li>
    <li>Set the Name property and other properties</li>
    <li>Call <code>Save()</code> to persist</li>
</ol>

<h3>Save Actions</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">SaveAction</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Save</td><td class=""px-4 py-2"">Save as draft (not published)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Publish</td><td class=""px-4 py-2"">Save and publish immediately</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">CheckIn</td><td class=""px-4 py-2"">Check in for review</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Schedule</td><td class=""px-4 py-2"">Schedule for future publication</td></tr>
    </tbody>
</table>

<h3>Modifying Existing Content</h3>
<p>Content is read-only by default. To modify, create a writable clone first:</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "create-content",
                            Title = "Creating Content",
                            Description = "Create and save new content",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class ContentCreationService
{
    private readonly IContentRepository _contentRepository;

    public ContentCreationService(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public ContentReference CreateArticle(ContentReference parentRef, string title, string body)
    {
        // Create new article under parent
        var article = _contentRepository.GetDefault<ArticlePage>(parentRef);

        // Set properties
        article.Name = title;  // Name is used for URL segment
        article.Title = title;
        article.MainBody = new XhtmlString(body);
        article.PublishedDate = DateTime.Now;

        // Save and publish
        var savedRef = _contentRepository.Save(
            article,
            SaveAction.Publish,
            AccessLevel.NoAccess);

        return savedRef;
    }

    public void UpdateArticle(ContentReference articleRef, string newTitle)
    {
        // Load the article
        var article = _contentRepository.Get<ArticlePage>(articleRef);

        // Create writable clone
        var writableArticle = article.CreateWritableClone() as ArticlePage;

        // Modify
        writableArticle!.Title = newTitle;

        // Save changes
        _contentRepository.Save(writableArticle, SaveAction.Publish);
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-moving-deleting",
                    ModuleId = "content-management",
                    Title = "Moving and Deleting Content",
                    Summary = "Learn to move, copy, and delete content.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Move content between locations",
                        "Copy content",
                        "Delete content (soft and hard delete)"
                    },
                    Content = @"
<h2>Moving and Deleting Content</h2>
<p>IContentRepository provides methods for moving, copying, and deleting content.</p>

<h3>Moving Content</h3>
<p>Use <code>Move()</code> to relocate content in the tree. Children are moved with the parent.</p>

<h3>Copying Content</h3>
<p>Use <code>Copy()</code> to duplicate content. The copy gets a new ContentReference.</p>

<h3>Deleting Content</h3>
<ul>
    <li><strong>Delete()</strong> - Moves to Trash (can be restored)</li>
    <li><strong>Delete(forceDelete: true)</strong> - Permanently deletes</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "move-delete-content",
                            Title = "Move and Delete Operations",
                            Description = "Moving, copying, and deleting content",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class ContentOperationsService
{
    private readonly IContentRepository _contentRepository;

    public ContentOperationsService(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    // Move content to a new parent
    public void MoveContent(ContentReference contentRef, ContentReference newParentRef)
    {
        _contentRepository.Move(
            contentRef,
            newParentRef,
            AccessLevel.NoAccess,
            AccessLevel.NoAccess);
    }

    // Copy content (creates a duplicate)
    public ContentReference CopyContent(ContentReference sourceRef, ContentReference targetParentRef)
    {
        return _contentRepository.Copy(
            sourceRef,
            targetParentRef,
            AccessLevel.NoAccess,
            AccessLevel.NoAccess,
            copyChildContent: true);
    }

    // Soft delete (moves to trash)
    public void DeleteToTrash(ContentReference contentRef)
    {
        _contentRepository.Delete(contentRef, forceDelete: false);
    }

    // Permanent delete
    public void PermanentlyDelete(ContentReference contentRef)
    {
        _contentRepository.Delete(contentRef, forceDelete: true);
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-versioning",
                    ModuleId = "content-management",
                    Title = "Content Versioning",
                    Summary = "Work with content versions and publishing workflow.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand content versioning",
                        "Work with different versions",
                        "Load specific versions"
                    },
                    Content = @"
<h2>Content Versioning</h2>
<p>Every save creates a new version. CMS maintains a version history for each content item.</p>

<h3>Version Types</h3>
<ul>
    <li><strong>Published</strong> - The live version visible to site visitors</li>
    <li><strong>Draft</strong> - Work in progress, not yet published</li>
    <li><strong>Previously Published</strong> - Older published versions</li>
</ul>

<h3>ContentReference and Versions</h3>
<p>ContentReference can optionally include a WorkID to specify a version:</p>
<ul>
    <li><code>ContentReference(5)</code> - Latest published version of content 5</li>
    <li><code>ContentReference(5, 10)</code> - Specific version (WorkID 10) of content 5</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "versioning-example",
                            Title = "Working with Versions",
                            Description = "Loading and listing content versions",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.DataAccess;

public class VersioningService
{
    private readonly IContentRepository _contentRepository;
    private readonly IContentVersionRepository _versionRepository;

    public VersioningService(
        IContentRepository contentRepository,
        IContentVersionRepository versionRepository)
    {
        _contentRepository = contentRepository;
        _versionRepository = versionRepository;
    }

    // List all versions of content
    public IEnumerable<ContentVersion> GetAllVersions(ContentReference contentRef)
    {
        return _versionRepository.List(contentRef);
    }

    // Load a specific version
    public T? LoadVersion<T>(ContentReference contentRef, int workId) where T : IContent
    {
        var versionRef = new ContentReference(contentRef.ID, workId);
        return _contentRepository.Get<T>(versionRef);
    }

    // Get the published version
    public ContentVersion? GetPublishedVersion(ContentReference contentRef)
    {
        return _versionRepository.List(contentRef)
            .FirstOrDefault(v => v.Status == VersionStatus.Published);
    }

    // Create a new draft from published version
    public ContentReference CreateDraft(ContentReference publishedRef)
    {
        var content = _contentRepository.Get<IContent>(publishedRef);
        var draft = content.CreateWritableClone();
        return _contentRepository.Save(draft, SaveAction.CheckOut);
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-special-references",
                    ModuleId = "content-management",
                    Title = "Special Content References",
                    Summary = "Learn about root pages, start pages, and global assets.",
                    Order = 5,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Know the special content references",
                        "Access site start page",
                        "Find global assets folders"
                    },
                    Content = @"
<h2>Special Content References</h2>
<p>CMS provides several well-known content references for important locations.</p>

<h3>Key References</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Reference</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ContentReference.RootPage</td><td class=""px-4 py-2"">Root of the page tree</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ContentReference.StartPage</td><td class=""px-4 py-2"">Site start page (homepage)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ContentReference.GlobalBlockFolder</td><td class=""px-4 py-2"">Global shared blocks</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ContentReference.SiteBlockFolder</td><td class=""px-4 py-2"">Site-specific blocks</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">SiteDefinition.Current.GlobalAssetsRoot</td><td class=""px-4 py-2"">Global media folder</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">SiteDefinition.Current.SiteAssetsRoot</td><td class=""px-4 py-2"">Site media folder</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "special-refs",
                            Title = "Using Special References",
                            Description = "Access well-known content locations",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class SiteService
{
    private readonly IContentLoader _contentLoader;
    private readonly ISiteDefinitionResolver _siteResolver;

    public SiteService(
        IContentLoader contentLoader,
        ISiteDefinitionResolver siteResolver)
    {
        _contentLoader = contentLoader;
        _siteResolver = siteResolver;
    }

    // Get the site start page
    public StartPage GetStartPage()
    {
        return _contentLoader.Get<StartPage>(ContentReference.StartPage);
    }

    // Get all top-level pages
    public IEnumerable<PageData> GetTopLevelPages()
    {
        return _contentLoader.GetChildren<PageData>(ContentReference.StartPage);
    }

    // Get current site's media folder
    public ContentReference GetSiteMediaFolder()
    {
        var site = _siteResolver.GetByContent(
            ContentReference.StartPage,
            fallbackToWildcard: true);
        return site.SiteAssetsRoot;
    }

    // Upload media to site folder
    public ContentReference GetMediaUploadFolder()
    {
        return SiteDefinition.Current.SiteAssetsRoot;
    }
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 5-11 Placeholders

    private LearningModule BuildInitializationEventsModule()
    {
        return new LearningModule
        {
            Id = "initialization-events",
            Title = "Initialization & Events",
            Description = "Learn about initialization modules and content events.",
            Icon = "bolt",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-management" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ie-initialization-overview",
                    ModuleId = "initialization-events",
                    Title = "Initialization System Overview",
                    Summary = "Understand how CMS initializes and how to hook into the startup process.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the CMS initialization process",
                        "Know when initialization modules run",
                        "Learn the module dependency system"
                    },
                    Content = @"
<h2>CMS Initialization System</h2>
<p>Optimizely CMS uses an initialization system that runs during application startup. This allows you to register services, configure options, and set up event handlers.</p>

<h3>How Initialization Works</h3>
<ol>
    <li>ASP.NET Core starts and calls <code>AddCms()</code></li>
    <li>CMS scans assemblies for <code>IInitializableModule</code> implementations</li>
    <li>Modules are sorted by dependencies</li>
    <li>Each module's <code>Initialize()</code> method is called</li>
    <li>On shutdown, <code>Uninitialize()</code> is called in reverse order</li>
</ol>

<h3>Module Dependencies</h3>
<p>Use <code>[ModuleDependency]</code> to ensure your module runs after CMS core modules:</p>
<ul>
    <li><code>EPiServer.Web.InitializationModule</code> - Core web functionality</li>
    <li><code>EPiServer.Framework.Initialization</code> - Framework services</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ie-creating-modules",
                    ModuleId = "initialization-events",
                    Title = "Creating Initialization Modules",
                    Summary = "Create custom initialization modules to run code at startup.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create an initialization module",
                        "Use the InitializationEngine",
                        "Handle module dependencies correctly"
                    },
                    Content = @"
<h2>Creating Initialization Modules</h2>
<p>Create a class that implements <code>IInitializableModule</code> and decorate it with the appropriate attribute.</p>

<h3>Module Attributes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attribute</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[InitializableModule]</td><td class=""px-4 py-2"">No CMS dependencies needed</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">[ModuleDependency(typeof(...))]</td><td class=""px-4 py-2"">Depends on CMS or other modules</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "init-module-example",
                            Title = "Basic Initialization Module",
                            Description = "A simple initialization module",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace MyOptimizely.Business.Initialization
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class CustomInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            // This runs once during application startup

            // Access services via the service locator
            var contentEvents = context.Locate.Advanced
                .GetInstance<IContentEvents>();

            // Attach event handlers
            contentEvents.PublishedContent += OnContentPublished;

            // Log initialization
            var logger = context.Locate.Advanced
                .GetInstance<ILogger<CustomInitializationModule>>();
            logger.LogInformation(""Custom module initialized"");
        }

        public void Uninitialize(InitializationEngine context)
        {
            // Cleanup - runs on application shutdown
            var contentEvents = context.Locate.Advanced
                .GetInstance<IContentEvents>();

            contentEvents.PublishedContent -= OnContentPublished;
        }

        private void OnContentPublished(object? sender, ContentEventArgs e)
        {
            // Handle content published event
        }
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ie-content-events",
                    ModuleId = "initialization-events",
                    Title = "Content Events",
                    Summary = "React to content changes with content events.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Know the available content events",
                        "Handle publishing, saving, and deleting events",
                        "Understand pre vs post events"
                    },
                    Content = @"
<h2>Content Events</h2>
<p><code>IContentEvents</code> provides events for all content operations. Use these to react to changes.</p>

<h3>Available Events</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Event</th>
            <th class=""px-4 py-2 text-left"">When Fired</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">CreatingContent</td><td class=""px-4 py-2"">Before content is created</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">CreatedContent</td><td class=""px-4 py-2"">After content is created</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">SavingContent</td><td class=""px-4 py-2"">Before content is saved</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">SavedContent</td><td class=""px-4 py-2"">After content is saved</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">PublishingContent</td><td class=""px-4 py-2"">Before content is published</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">PublishedContent</td><td class=""px-4 py-2"">After content is published</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">DeletingContent</td><td class=""px-4 py-2"">Before content is deleted</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">DeletedContent</td><td class=""px-4 py-2"">After content is deleted</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">MovingContent</td><td class=""px-4 py-2"">Before content is moved</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">MovedContent</td><td class=""px-4 py-2"">After content is moved</td></tr>
    </tbody>
</table>

<h3>Pre vs Post Events</h3>
<ul>
    <li><strong>Pre-events</strong> (e.g., SavingContent) - Can cancel the operation</li>
    <li><strong>Post-events</strong> (e.g., SavedContent) - Operation already completed</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "content-events-example",
                            Title = "Content Event Handlers",
                            Description = "Handling various content events",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class ContentEventModule : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var events = context.Locate.Advanced.GetInstance<IContentEvents>();

        events.PublishingContent += OnPublishingContent;
        events.PublishedContent += OnPublishedContent;
        events.SavingContent += OnSavingContent;
    }

    // Pre-event: can cancel or modify
    private void OnPublishingContent(object? sender, ContentEventArgs e)
    {
        if (e.Content is ArticlePage article)
        {
            // Validate before publishing
            if (string.IsNullOrEmpty(article.Title))
            {
                // Cancel the publish operation
                e.CancelAction = true;
                e.CancelReason = ""Title is required"";
            }
        }
    }

    // Post-event: react to completed action
    private void OnPublishedContent(object? sender, ContentEventArgs e)
    {
        // Clear cache, send notifications, etc.
        var logger = LogManager.GetLogger(typeof(ContentEventModule));
        logger.Info($""Content published: {e.Content.Name}"");
    }

    // Modify content before saving
    private void OnSavingContent(object? sender, ContentEventArgs e)
    {
        if (e.Content is PageData page)
        {
            // Auto-set a property
            var writable = page.CreateWritableClone() as PageData;
            // Note: Be careful to avoid infinite loops
        }
    }

    public void Uninitialize(InitializationEngine context)
    {
        var events = context.Locate.Advanced.GetInstance<IContentEvents>();
        events.PublishingContent -= OnPublishingContent;
        events.PublishedContent -= OnPublishedContent;
        events.SavingContent -= OnSavingContent;
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ie-configuring-services",
                    ModuleId = "initialization-events",
                    Title = "Configuring Services",
                    Summary = "Register custom services and configure CMS options.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Register services with dependency injection",
                        "Configure CMS options",
                        "Use IConfigurableModule for service registration"
                    },
                    Content = @"
<h2>Configuring Services</h2>
<p>You can register your own services and configure CMS options using <code>IConfigurableModule</code>.</p>

<h3>Service Registration</h3>
<p>Implement <code>IConfigurableModule</code> to access <code>IServiceCollection</code> during startup:</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "configurable-module",
                            Title = "Configurable Module",
                            Description = "Register services and configure options",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;

[ModuleDependency(typeof(ServiceContainerInitialization))]
public class DependencyInjectionModule : IConfigurableModule
{
    public void ConfigureContainer(ServiceConfigurationContext context)
    {
        // Register custom services
        context.Services.AddScoped<IMyService, MyService>();
        context.Services.AddSingleton<ICacheService, CacheService>();

        // Configure CMS options
        context.Services.Configure<ContentOptions>(options =>
        {
            options.RequireEditAccessToChangedByProperty = true;
        });

        // Configure scheduling options
        context.Services.Configure<SchedulerOptions>(options =>
        {
            options.Enabled = true;
        });

        // Replace a default service
        context.Services.AddSingleton<IContentRenderer, CustomContentRenderer>();
    }

    public void Initialize(InitializationEngine context)
    {
        // Additional initialization after DI is configured
    }

    public void Uninitialize(InitializationEngine context)
    {
    }
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    private LearningModule BuildLocalizationModule()
    {
        return new LearningModule
        {
            Id = "localization",
            Title = "Localization",
            Description = "Master multilingual content and UI localization.",
            Icon = "globe-alt",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-management" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "loc-multilingual-overview",
                    ModuleId = "localization",
                    Title = "Multilingual Content Overview",
                    Summary = "Understand how CMS handles content in multiple languages.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand CMS language architecture",
                        "Know the difference between master and translated versions",
                        "Learn about language fallbacks"
                    },
                    Content = @"
<h2>Multilingual Content in CMS 12</h2>
<p>Optimizely CMS has built-in support for content in multiple languages. Each piece of content can exist in multiple language versions.</p>

<h3>Key Concepts</h3>
<ul>
    <li><strong>Master Language</strong> - The first language version created; holds common properties</li>
    <li><strong>Translated Versions</strong> - Additional language versions of the same content</li>
    <li><strong>Culture-Specific Properties</strong> - Properties marked with <code>[CultureSpecific]</code> have different values per language</li>
    <li><strong>Fallback Languages</strong> - Define which language to use if translation doesn't exist</li>
</ul>

<h3>ILocalizable Interface</h3>
<p>Content types that implement <code>ILocalizable</code> (like PageData and BlockData) support multiple languages.</p>

<h3>Language vs Culture</h3>
<ul>
    <li><strong>Content Language</strong> - The language of the content being edited/viewed</li>
    <li><strong>UI Culture</strong> - The language of the CMS editorial interface</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "loc-managing-languages",
                    ModuleId = "localization",
                    Title = "Managing Website Languages",
                    Summary = "Enable and configure languages for your website.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Enable languages in the CMS",
                        "Configure fallback languages",
                        "Set up language-specific URLs"
                    },
                    Content = @"
<h2>Managing Languages</h2>
<p>Languages are configured in Admin > Config > Manage Website Languages.</p>

<h3>Language Settings</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Enabled</td><td class=""px-4 py-2"">Language available for content creation</td></tr>
        <tr><td class=""px-4 py-2"">Fallback Language</td><td class=""px-4 py-2"">Use this language if translation missing</td></tr>
        <tr><td class=""px-4 py-2"">URL Segment</td><td class=""px-4 py-2"">Language prefix in URLs (e.g., /en/, /sv/)</td></tr>
    </tbody>
</table>

<h3>URL Patterns</h3>
<p>Languages can be indicated in URLs via:</p>
<ul>
    <li><strong>URL Segment</strong>: <code>example.com/en/about</code></li>
    <li><strong>Hostname</strong>: <code>en.example.com/about</code></li>
    <li><strong>Query String</strong>: <code>example.com/about?lang=en</code></li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "loc-loading-content",
                    ModuleId = "localization",
                    Title = "Loading Content in Specific Languages",
                    Summary = "Load content in specific languages using LanguageSelector.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use LanguageSelector to specify language",
                        "Handle missing translations gracefully",
                        "Work with language branches"
                    },
                    Content = @"
<h2>Loading Localized Content</h2>
<p>Use <code>LanguageSelector</code> to load content in a specific language.</p>

<h3>LanguageSelector Options</h3>
<ul>
    <li><code>AutoDetect()</code> - Uses current culture context</li>
    <li><code>Specific(culture)</code> - Load exact language</li>
    <li><code>Fallback(culture, true)</code> - Load with fallback enabled</li>
    <li><code>MasterLanguage()</code> - Load master language version</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "language-loading",
                            Title = "Loading Content by Language",
                            Description = "Load content in specific languages",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.Globalization;
using System.Globalization;

public class LocalizedContentService
{
    private readonly IContentLoader _contentLoader;

    public LocalizedContentService(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    // Load content in current culture
    public T? LoadInCurrentCulture<T>(ContentReference reference) where T : IContent
    {
        return _contentLoader.Get<T>(reference, LanguageSelector.AutoDetect());
    }

    // Load content in specific language
    public T? LoadInLanguage<T>(ContentReference reference, string language)
        where T : IContent
    {
        var culture = new CultureInfo(language);
        return _contentLoader.Get<T>(reference, new LanguageSelector(culture));
    }

    // Load with fallback
    public T? LoadWithFallback<T>(ContentReference reference, string language)
        where T : IContent
    {
        var culture = new CultureInfo(language);
        return _contentLoader.Get<T>(
            reference,
            LanguageSelector.Fallback(culture, enableMasterLanguageFallback: true));
    }

    // Get all language versions
    public IEnumerable<T> GetAllLanguageVersions<T>(ContentReference reference)
        where T : IContent, ILocalizable
    {
        var languages = _contentLoader.GetExistingLanguages(reference);
        return languages.Select(lang =>
            _contentLoader.Get<T>(reference, new LanguageSelector(lang)));
    }

    // Check if translation exists
    public bool HasTranslation(ContentReference reference, string language)
    {
        var culture = new CultureInfo(language);
        var languages = _contentLoader.GetExistingLanguages(reference);
        return languages.Contains(culture);
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "loc-localization-service",
                    ModuleId = "localization",
                    Title = "Localizing the UI",
                    Summary = "Use LocalizationService for UI text translations.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use LocalizationService for translations",
                        "Create XML translation files",
                        "Override system strings"
                    },
                    Content = @"
<h2>Localizing the User Interface</h2>
<p><code>LocalizationService</code> provides translated strings for the UI (not content). Use it for labels, buttons, and messages.</p>

<h3>Translation Files</h3>
<p>Place XML translation files in the <code>lang/</code> folder:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto"">
lang/
├── Labels_en.xml
├── Labels_sv.xml
└── Labels_de.xml
</pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "translation-xml",
                            Title = "Translation XML File",
                            Description = "Create translation files",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<!-- lang/Labels_en.xml -->
<languages>
  <language name=""English"" id=""en"">
    <mysite>
      <common>
        <readmore>Read more</readmore>
        <contactus>Contact us</contactus>
        <searchplaceholder>Search...</searchplaceholder>
      </common>
      <validation>
        <required>This field is required</required>
        <invalidemail>Please enter a valid email</invalidemail>
      </validation>
      <pages>
        <article>
          <byline>Written by {0}</byline>
          <publishedon>Published on {0:d}</publishedon>
        </article>
      </pages>
    </mysite>
  </language>
</languages>",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "using-localization",
                            Title = "Using LocalizationService",
                            Description = "Get translated strings in code and views",
                            Type = ExampleType.Code,
                            ExampleContent = @"// In a controller or service
public class MyController : Controller
{
    private readonly LocalizationService _localization;

    public MyController(LocalizationService localization)
    {
        _localization = localization;
    }

    public IActionResult Index()
    {
        // Get a simple translation
        var readMore = _localization.GetString(""/mysite/common/readmore"");

        // Get with formatting
        var byline = _localization.GetStringByCulture(
            ""/mysite/pages/article/byline"",
            CultureInfo.CurrentUICulture,
            ""John Doe"");

        ViewBag.ReadMore = readMore;
        ViewBag.Byline = byline;
        return View();
    }
}

// In a Razor view
@inject LocalizationService Localization

<a href=""@Model.Link"">
    @Localization.GetString(""/mysite/common/readmore"")
</a>

// Or use the Html helper
<label>@Html.Translate(""/mysite/common/searchplaceholder"")</label>",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    private LearningModule BuildSearchNavigationModule()
    {
        return new LearningModule
        {
            Id = "search-navigation",
            Title = "Search & Navigation",
            Description = "Implement search functionality with Optimizely Search & Navigation.",
            Icon = "magnifying-glass",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-types" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "sn-overview",
                    ModuleId = "search-navigation",
                    Title = "Search & Navigation Overview",
                    Summary = "Understand Optimizely Search & Navigation and its capabilities.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Search & Navigation provides",
                        "Know the licensing and setup requirements",
                        "Learn about automatic indexing"
                    },
                    Content = @"
<h2>Optimizely Search & Navigation</h2>
<p>Search & Navigation (formerly Episerver Find) is a cloud-based search engine built on Elasticsearch. It provides:</p>

<h3>Key Features</h3>
<ul>
    <li><strong>Full-text search</strong> - Search across all content</li>
    <li><strong>Automatic indexing</strong> - Content indexed on publish</li>
    <li><strong>Faceted search</strong> - Filter by categories, dates, etc.</li>
    <li><strong>Unified search</strong> - Search CMS content and Commerce products</li>
    <li><strong>Synonyms & boosting</strong> - Improve search relevance</li>
</ul>

<h3>Setup</h3>
<p>If using DXP, Search & Navigation is included. Otherwise, you need a license and connection to the cloud service.</p>

<h3>Automatic Indexing</h3>
<p>When you install <code>EPiServer.Find.Cms</code>, published content is automatically indexed. The index updates when content is:</p>
<ul>
    <li>Published</li>
    <li>Moved</li>
    <li>Deleted</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "find-setup",
                            Title = "Configure Search & Navigation",
                            Description = "Add Search & Navigation to your project",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"// appsettings.json
{
  ""EPiServer"": {
    ""Find"": {
      ""ServiceUrl"": ""https://demo01.find.episerver.net/xxx"",
      ""DefaultIndex"": ""your_index_name""
    }
  }
}

// Program.cs
builder.Services.AddFind();",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sn-basic-search",
                    ModuleId = "search-navigation",
                    Title = "Building Search Queries",
                    Summary = "Create search queries using the typed search API.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Use IClient to create search queries",
                        "Search for specific content types",
                        "Filter and sort results"
                    },
                    Content = @"
<h2>Building Search Queries</h2>
<p>Use <code>IClient</code> to build typed search queries against your content.</p>

<h3>Query Building Pattern</h3>
<ol>
    <li>Start with <code>Search&lt;T&gt;()</code> for typed queries</li>
    <li>Add <code>For(searchText)</code> for full-text search</li>
    <li>Add <code>Filter()</code> for filtering</li>
    <li>Add <code>OrderBy()</code> for sorting</li>
    <li>Call <code>GetContentResult()</code> to execute</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "search-examples",
                            Title = "Search Query Examples",
                            Description = "Various search query patterns",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework;

public class SearchService
{
    private readonly IClient _searchClient;

    public SearchService(IClient searchClient)
    {
        _searchClient = searchClient;
    }

    // Basic full-text search
    public IContentResult<ArticlePage> SearchArticles(string query)
    {
        return _searchClient.Search<ArticlePage>()
            .For(query)
            .GetContentResult();
    }

    // Search with filtering
    public IContentResult<ArticlePage> SearchRecentArticles(
        string query, DateTime fromDate)
    {
        return _searchClient.Search<ArticlePage>()
            .For(query)
            .Filter(x => x.PublishedDate.GreaterThan(fromDate))
            .OrderByDescending(x => x.PublishedDate)
            .Take(20)
            .GetContentResult();
    }

    // Search across multiple types
    public IContentResult<PageData> SearchAllPages(string query)
    {
        return _searchClient.Search<PageData>()
            .For(query)
            .FilterForVisitor()  // Respect access rights
            .PublishedInCurrentLanguage()  // Current language only
            .GetContentResult();
    }

    // Pagination
    public IContentResult<ArticlePage> SearchWithPaging(
        string query, int page, int pageSize)
    {
        return _searchClient.Search<ArticlePage>()
            .For(query)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .GetContentResult();
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sn-facets",
                    ModuleId = "search-navigation",
                    Title = "Faceted Search",
                    Summary = "Add facets to allow filtering search results.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what facets are",
                        "Add facets to search queries",
                        "Use facets for filtering"
                    },
                    Content = @"
<h2>Faceted Search</h2>
<p>Facets let users filter results by categories, dates, or other properties. They show available options with counts.</p>

<h3>Facet Types</h3>
<ul>
    <li><strong>TermsFacet</strong> - For categories, tags, types</li>
    <li><strong>DateHistogramFacet</strong> - For date ranges</li>
    <li><strong>NumericRangeFacet</strong> - For numeric ranges</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "facet-example",
                            Title = "Adding Facets to Search",
                            Description = "Implement faceted navigation",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class FacetedSearchResult
{
    public IEnumerable<ArticlePage> Articles { get; set; }
    public IEnumerable<TermCount> Categories { get; set; }
    public IEnumerable<TermCount> Authors { get; set; }
}

public FacetedSearchResult SearchWithFacets(
    string query,
    string? categoryFilter = null)
{
    var search = _searchClient.Search<ArticlePage>()
        .For(query)
        .TermsFacetFor(x => x.Category)
        .TermsFacetFor(x => x.Author);

    // Apply category filter if selected
    if (!string.IsNullOrEmpty(categoryFilter))
    {
        search = search.Filter(x => x.Category.Match(categoryFilter));
    }

    var result = search.GetContentResult();

    return new FacetedSearchResult
    {
        Articles = result.Items,
        Categories = result.TermsFacetFor(x => x.Category).Terms,
        Authors = result.TermsFacetFor(x => x.Author).Terms
    };
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sn-advanced-filtering",
                    ModuleId = "search-navigation",
                    Title = "Advanced Filtering & Aggregations",
                    Summary = "Build complex filters and aggregations for sophisticated search experiences.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Combine multiple filters with AND/OR logic",
                        "Create nested and hierarchical facets",
                        "Use aggregations for analytics and dashboards"
                    },
                    Content = @"
<h2>Advanced Filtering & Aggregations</h2>
<p>Beyond basic faceted search, Search & Navigation supports complex filtering scenarios and powerful aggregations for analytics.</p>

<h3>Combining Filters</h3>
<p>Use boolean operators to combine multiple conditions:</p>
<ul>
    <li><strong>AND</strong> - All conditions must match (default)</li>
    <li><strong>OR</strong> - Any condition can match</li>
    <li><strong>NOT</strong> - Exclude matching items</li>
</ul>

<h3>Nested Filters</h3>
<p>For complex content structures, use nested filters to query child objects within content items.</p>

<h3>Date Range Aggregations</h3>
<p>Create date-based groupings for content archives:</p>
<ul>
    <li><strong>Year</strong> - Group by publication year</li>
    <li><strong>Month</strong> - Group by month</li>
    <li><strong>Custom ranges</strong> - Define your own date ranges</li>
</ul>

<h3>Statistical Aggregations</h3>
<p>Calculate statistics across your indexed content:</p>
<ul>
    <li><strong>Count</strong> - Number of matching documents</li>
    <li><strong>Sum/Average</strong> - For numeric properties</li>
    <li><strong>Min/Max</strong> - Find extremes</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "advanced-filter-example",
                            Title = "Complex Filter Queries",
                            Description = "Examples of advanced filtering techniques",
                            Type = ExampleType.Code,
                            ExampleContent = @"public class AdvancedSearchService
{
    private readonly IClient _searchClient;

    // Combine filters with OR logic
    public IContentResult<ArticlePage> SearchMultipleCategories(
        string query, params string[] categories)
    {
        var search = _searchClient.Search<ArticlePage>()
            .For(query);

        if (categories.Any())
        {
            // OR filter - matches any category
            search = search.Filter(x =>
                x.Category.In(categories));
        }

        return search.GetContentResult();
    }

    // Complex AND/OR combinations
    public IContentResult<ArticlePage> SearchWithComplexFilter(
        string query,
        DateTime? fromDate,
        DateTime? toDate,
        string[]? categories,
        string[]? authors)
    {
        var search = _searchClient.Search<ArticlePage>()
            .For(query);

        // Date range filter (AND)
        if (fromDate.HasValue)
            search = search.Filter(x =>
                x.PublishedDate.GreaterThan(fromDate.Value));

        if (toDate.HasValue)
            search = search.Filter(x =>
                x.PublishedDate.LessThan(toDate.Value));

        // Category filter (OR within, AND with other filters)
        if (categories?.Any() == true)
            search = search.Filter(x => x.Category.In(categories));

        // Author filter (OR within)
        if (authors?.Any() == true)
            search = search.Filter(x => x.Author.In(authors));

        return search.GetContentResult();
    }

    // Date histogram aggregation for archives
    public IContentResult<ArticlePage> SearchWithDateHistogram(string query)
    {
        return _searchClient.Search<ArticlePage>()
            .For(query)
            .HistogramFacetFor(x => x.PublishedDate,
                DateInterval.Month)
            .GetContentResult();
    }

    // Exclude specific content
    public IContentResult<ArticlePage> SearchExcluding(
        string query, ContentReference[] excludeIds)
    {
        return _searchClient.Search<ArticlePage>()
            .For(query)
            .Filter(x => !x.ContentLink.In(excludeIds))
            .GetContentResult();
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sn-custom-providers",
                    ModuleId = "search-navigation",
                    Title = "Custom Search Providers",
                    Summary = "Implement custom search providers for external search engines or specialized scenarios.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the search provider architecture",
                        "Create custom ISearchProvider implementations",
                        "Integrate external search engines like Elasticsearch or Azure Search"
                    },
                    Content = @"
<h2>Custom Search Providers</h2>
<p>While Optimizely Search & Navigation (Find) is the primary search solution, you can implement custom search providers for specialized needs or alternative search engines.</p>

<h3>When to Use Custom Providers</h3>
<ul>
    <li>Integration with existing enterprise search (Elasticsearch, Solr)</li>
    <li>Azure Cognitive Search for advanced AI features</li>
    <li>Custom indexing requirements</li>
    <li>Performance optimization for specific scenarios</li>
</ul>

<h3>Search Provider Interface</h3>
<p>Implement <code>ISearchProvider</code> to create a pluggable search component:</p>

<h3>Index Synchronization</h3>
<p>Keep your custom index in sync with CMS content:</p>
<ul>
    <li><strong>Content events</strong> - Subscribe to publish/unpublish events</li>
    <li><strong>Scheduled jobs</strong> - Full re-index on schedule</li>
    <li><strong>Manual triggers</strong> - Admin-initiated sync</li>
</ul>

<h3>Considerations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Find</th>
            <th class=""px-4 py-2 text-left"">Custom Provider</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Setup</td><td class=""px-4 py-2"">Simple configuration</td><td class=""px-4 py-2"">Requires development</td></tr>
        <tr><td class=""px-4 py-2"">Auto-indexing</td><td class=""px-4 py-2"">Built-in</td><td class=""px-4 py-2"">Must implement</td></tr>
        <tr><td class=""px-4 py-2"">Access rights</td><td class=""px-4 py-2"">Automatic</td><td class=""px-4 py-2"">Must implement</td></tr>
        <tr><td class=""px-4 py-2"">Flexibility</td><td class=""px-4 py-2"">Limited to Find API</td><td class=""px-4 py-2"">Full control</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "custom-provider-example",
                            Title = "Custom Search Provider",
                            Description = "Basic structure for a custom search provider",
                            Type = ExampleType.Code,
                            ExampleContent = @"using Azure;
using Azure.Search.Documents;
using EPiServer;
using EPiServer.Core;

public interface ICustomSearchProvider
{
    Task<SearchResults<T>> SearchAsync<T>(string query, SearchOptions options)
        where T : class;
    Task IndexContentAsync(IContent content);
    Task RemoveFromIndexAsync(ContentReference contentLink);
}

public class AzureSearchProvider : ICustomSearchProvider
{
    private readonly SearchClient _searchClient;
    private readonly IContentRepository _contentRepository;

    public AzureSearchProvider(
        SearchClient searchClient,
        IContentRepository contentRepository)
    {
        _searchClient = searchClient;
        _contentRepository = contentRepository;
    }

    public async Task<SearchResults<T>> SearchAsync<T>(
        string query, SearchOptions options) where T : class
    {
        return await _searchClient.SearchAsync<T>(query, options);
    }

    public async Task IndexContentAsync(IContent content)
    {
        var document = MapContentToDocument(content);
        await _searchClient.MergeOrUploadDocumentsAsync(
            new[] { document });
    }

    public async Task RemoveFromIndexAsync(ContentReference contentLink)
    {
        await _searchClient.DeleteDocumentsAsync(
            ""id"", new[] { contentLink.ID.ToString() });
    }

    private SearchDocument MapContentToDocument(IContent content)
    {
        return new SearchDocument(new Dictionary<string, object>
        {
            [""id""] = content.ContentLink.ID.ToString(),
            [""title""] = content.Name,
            [""contentType""] = content.GetType().Name,
            [""url""] = GetContentUrl(content)
        });
    }
}

// Subscribe to content events for real-time indexing
[InitializableModule]
public class SearchIndexingModule : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var events = context.Locate.Advanced
            .GetInstance<IContentEvents>();

        events.PublishedContent += OnContentPublished;
        events.DeletedContent += OnContentDeleted;
    }

    private void OnContentPublished(object sender, ContentEventArgs e)
    {
        var provider = ServiceLocator.Current
            .GetInstance<ICustomSearchProvider>();
        provider.IndexContentAsync(e.Content);
    }

    private void OnContentDeleted(object sender, DeleteContentEventArgs e)
    {
        var provider = ServiceLocator.Current
            .GetInstance<ICustomSearchProvider>();
        provider.RemoveFromIndexAsync(e.ContentLink);
    }
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    private LearningModule BuildFormsModule()
    {
        return new LearningModule
        {
            Id = "forms",
            Title = "Optimizely Forms",
            Description = "Build and customize forms with Optimizely Forms.",
            Icon = "clipboard-document-list",
            Order = 8,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-types" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "forms-overview",
                    ModuleId = "forms",
                    Title = "Optimizely Forms Overview",
                    Summary = "Understand Optimizely Forms and its capabilities.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Forms provides",
                        "Know the built-in form elements",
                        "Learn about form submissions and actors"
                    },
                    Content = @"
<h2>Optimizely Forms</h2>
<p>Optimizely Forms is an add-on that lets editors create forms without developer involvement. Forms can be embedded in any page using a ContentArea.</p>

<h3>Key Features</h3>
<ul>
    <li><strong>Drag-and-drop builder</strong> - Editors create forms visually</li>
    <li><strong>Built-in elements</strong> - Text, email, selection, file upload, etc.</li>
    <li><strong>Validation</strong> - Required fields, patterns, custom validators</li>
    <li><strong>Actors</strong> - Actions triggered on submission (email, webhook)</li>
    <li><strong>Data export</strong> - Export submissions as CSV/Excel</li>
</ul>

<h3>Installation</h3>
<p>Install the NuGet package:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg"">
dotnet add package EPiServer.Forms
dotnet add package EPiServer.Forms.UI
</pre>

<h3>Built-in Elements</h3>
<ul>
    <li>Text input, Textarea, Number</li>
    <li>Selection (dropdown, radio, checkbox)</li>
    <li>File upload</li>
    <li>CAPTCHA</li>
    <li>Hidden field</li>
    <li>Submit button</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "forms-embedding",
                    ModuleId = "forms",
                    Title = "Embedding Forms in Pages",
                    Summary = "Add forms to pages using ContentArea.",
                    Order = 2,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Create a form container property",
                        "Render forms in views",
                        "Style form elements"
                    },
                    Content = @"
<h2>Embedding Forms</h2>
<p>Forms are content items that can be dropped into any ContentArea. The most common approach is to add a dedicated form area to your pages.</p>

<h3>Steps to Embed</h3>
<ol>
    <li>Add a <code>ContentArea</code> property to your page type</li>
    <li>Optionally restrict to form types using <code>[AllowedTypes]</code></li>
    <li>Render the ContentArea in your view</li>
    <li>Editors can then drag forms into the area</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "forms-page-property",
                            Title = "Add Form Area to Page",
                            Description = "Page type with form container",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;

public class ContactPage : PageData
{
    [Display(Name = ""Main Content"")]
    public virtual XhtmlString? MainBody { get; set; }

    // Restrict to only allow FormContainerBlock
    [AllowedTypes(typeof(FormContainerBlock))]
    [Display(Name = ""Contact Form"")]
    public virtual ContentArea? FormArea { get; set; }
}

// In the view
@model ContactPage

<article>
    @Html.PropertyFor(m => m.MainBody)
</article>

<section class=""contact-form"">
    @Html.PropertyFor(m => m.FormArea)
</section>",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "forms-custom-elements",
                    ModuleId = "forms",
                    Title = "Creating Custom Form Elements",
                    Summary = "Extend Forms with custom element types.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create a custom form element",
                        "Add validation to custom elements",
                        "Create the editor and rendering views"
                    },
                    Content = @"
<h2>Custom Form Elements</h2>
<p>You can extend Optimizely Forms by creating custom element types for specialized input scenarios.</p>

<h3>Creating a Custom Element</h3>
<ol>
    <li>Create a class inheriting from an existing element (e.g., <code>TextboxElementBlock</code>)</li>
    <li>Add custom properties for configuration</li>
    <li>Create a view for rendering</li>
    <li>Optionally add custom validation</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "custom-element",
                            Title = "Custom Phone Number Element",
                            Description = "A phone number input with formatting",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Implementation.Elements;

[ContentType(
    DisplayName = ""Phone Number"",
    GUID = ""12345678-1234-1234-1234-123456789012"")]
public class PhoneNumberElementBlock : TextboxElementBlock
{
    [Display(Name = ""Country Code"")]
    public virtual string? DefaultCountryCode { get; set; }

    [Display(Name = ""Placeholder"")]
    public override string PlaceHolder
    {
        get => base.PlaceHolder ?? ""(555) 123-4567"";
        set => base.PlaceHolder = value;
    }

    public override string Validators =>
        $""phone|{base.Validators}"";
}

// View: Views/Shared/ElementBlocks/PhoneNumberElementBlock.cshtml
@model PhoneNumberElementBlock

<div class=""Form__Element"">
    <label for=""@Model.FormElement.Guid"">
        @Model.Label
        @if(Model.IsRequired) { <span class=""required"">*</span> }
    </label>
    <input type=""tel""
           id=""@Model.FormElement.Guid""
           name=""@Model.FormElement.Guid""
           placeholder=""@Model.PlaceHolder""
           data-f-type=""textbox""
           @(Model.IsRequired ? ""required"" : """") />
    <span class=""Form__Element__ValidationError""></span>
</div>",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "forms-actors",
                    ModuleId = "forms",
                    Title = "Form Actors and Submission Handling",
                    Summary = "Handle form submissions with custom actors.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand form actors",
                        "Create custom actors for integrations",
                        "Access submission data programmatically"
                    },
                    Content = @"
<h2>Form Actors</h2>
<p>Actors are triggered when a form is submitted. Built-in actors include:</p>
<ul>
    <li><strong>Send emails</strong> - Email notification to admins or submitter</li>
    <li><strong>Post to URL</strong> - Webhook for external systems</li>
</ul>

<h3>Custom Actors</h3>
<p>Create custom actors for CRM integration, database storage, or other workflows.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "custom-actor",
                            Title = "Custom CRM Integration Actor",
                            Description = "Send submissions to a CRM system",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Forms.Core.PostSubmissionActor;
using EPiServer.Forms.Core.Models;

public class CrmIntegrationActor : PostSubmissionActorBase
{
    private readonly ICrmService _crmService;

    public CrmIntegrationActor(ICrmService crmService)
    {
        _crmService = crmService;
    }

    public override object Run(object input)
    {
        var submission = input as FormSubmission;
        if (submission == null) return null;

        // Get form field values
        var formData = submission.Data;

        // Map to CRM contact
        var contact = new CrmContact
        {
            Email = GetFieldValue(formData, ""email""),
            Name = GetFieldValue(formData, ""name""),
            Company = GetFieldValue(formData, ""company""),
            Message = GetFieldValue(formData, ""message"")
        };

        // Send to CRM
        _crmService.CreateLead(contact);

        return ""Lead created successfully"";
    }

    private string? GetFieldValue(
        IDictionary<string, object> data, string fieldName)
    {
        var key = data.Keys.FirstOrDefault(k =>
            k.Contains(fieldName, StringComparison.OrdinalIgnoreCase));
        return key != null ? data[key]?.ToString() : null;
    }
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    private LearningModule BuildAccessRightsModule()
    {
        return new LearningModule
        {
            Id = "access-rights",
            Title = "Access Rights & Security",
            Description = "Configure access rights, roles, and security.",
            Icon = "shield-check",
            Order = 9,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ar-overview",
                    ModuleId = "access-rights",
                    Title = "Access Rights Overview",
                    Summary = "Understand the CMS access rights model.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the access rights model",
                        "Know the available access levels",
                        "Learn about inheritance"
                    },
                    Content = @"
<h2>Access Rights Model</h2>
<p>Optimizely CMS uses a hierarchical, role-based access control system. Access rights can be set on any content item and are inherited down the tree.</p>

<h3>Access Levels</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Level</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Read</td><td class=""px-4 py-2"">View content</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Create</td><td class=""px-4 py-2"">Create child content</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Change</td><td class=""px-4 py-2"">Edit existing content</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Delete</td><td class=""px-4 py-2"">Delete content</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Publish</td><td class=""px-4 py-2"">Publish content</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Administer</td><td class=""px-4 py-2"">Change access rights</td></tr>
    </tbody>
</table>

<h3>Inheritance</h3>
<p>Rights flow down from parent to children. You can break inheritance at any point to set specific permissions.</p>

<h3>Built-in Groups</h3>
<ul>
    <li><strong>CmsAdmins</strong> - Full admin access</li>
    <li><strong>CmsEditors</strong> - Edit view access</li>
    <li><strong>Everyone</strong> - All authenticated users</li>
    <li><strong>Anonymous</strong> - Unauthenticated visitors</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ar-checking-access",
                    ModuleId = "access-rights",
                    Title = "Checking Access in Code",
                    Summary = "Verify user permissions programmatically.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Check user access to content",
                        "Use IContentSecurityRepository",
                        "Filter content by access rights"
                    },
                    Content = @"
<h2>Checking Access Programmatically</h2>
<p>Use <code>IContentLoader</code> with access checking or <code>IContentSecurityRepository</code> for explicit checks.</p>

<h3>Automatic Filtering</h3>
<p>By default, <code>IContentLoader</code> respects access rights - users only see content they can access.</p>

<h3>Explicit Checks</h3>
<p>Use <code>IContentSecurityRepository</code> for fine-grained permission checks.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "access-checking",
                            Title = "Access Checking Examples",
                            Description = "Check permissions in code",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.Security;

public class ContentAccessService
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentSecurityRepository _securityRepo;

    public ContentAccessService(
        IContentLoader contentLoader,
        IContentSecurityRepository securityRepo)
    {
        _contentLoader = contentLoader;
        _securityRepo = securityRepo;
    }

    // Check if current user can read
    public bool CanRead(ContentReference reference)
    {
        var descriptor = _securityRepo.Get(reference);
        return descriptor.HasAccess(
            PrincipalInfo.CurrentPrincipal,
            AccessLevel.Read);
    }

    // Check if current user can publish
    public bool CanPublish(ContentReference reference)
    {
        var descriptor = _securityRepo.Get(reference);
        return descriptor.HasAccess(
            PrincipalInfo.CurrentPrincipal,
            AccessLevel.Publish);
    }

    // Load content only if user has access
    public T? LoadIfAllowed<T>(ContentReference reference) where T : IContent
    {
        // This automatically checks read access
        if (_contentLoader.TryGet<T>(reference, out var content))
        {
            return content;
        }
        return default;
    }

    // Load bypassing security (admin scenarios)
    public T? LoadBypassingAccess<T>(ContentReference reference) where T : IContent
    {
        return _contentLoader.Get<T>(
            reference,
            new LoaderOptions { BypassAccessCheck = true });
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ar-virtual-roles",
                    ModuleId = "access-rights",
                    Title = "Virtual Roles",
                    Summary = "Create dynamic roles based on conditions.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand virtual roles",
                        "Create custom virtual roles",
                        "Use virtual roles for content access"
                    },
                    Content = @"
<h2>Virtual Roles</h2>
<p>Virtual roles are dynamic roles evaluated at runtime based on conditions. Unlike regular roles, membership is determined by code.</p>

<h3>Built-in Virtual Roles</h3>
<ul>
    <li><strong>Everyone</strong> - All users including anonymous</li>
    <li><strong>Authenticated</strong> - Any logged-in user</li>
    <li><strong>Anonymous</strong> - Not logged in</li>
    <li><strong>Creator</strong> - The content creator</li>
</ul>

<h3>Custom Virtual Roles</h3>
<p>Create custom virtual roles for scenarios like:</p>
<ul>
    <li>Users from specific IP ranges</li>
    <li>Users with specific claims</li>
    <li>Time-based access (office hours only)</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "virtual-role",
                            Title = "Custom Virtual Role",
                            Description = "IP-based virtual role",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Security;
using System.Security.Principal;

public class InternalNetworkRole : VirtualRoleProviderBase
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly string[] _internalIpRanges = { ""10."", ""192.168."" };

    public InternalNetworkRole(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public override bool IsInVirtualRole(
        IPrincipal principal, object context)
    {
        var ipAddress = _httpContext.HttpContext?.Connection
            ?.RemoteIpAddress?.ToString();

        if (string.IsNullOrEmpty(ipAddress))
            return false;

        return _internalIpRanges.Any(range =>
            ipAddress.StartsWith(range));
    }
}

// Register in Program.cs
services.AddVirtualRole<InternalNetworkRole>(""InternalUsers"");",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ar-security-trimming",
                    ModuleId = "access-rights",
                    Title = "Content Security Trimming",
                    Summary = "Filter query results based on user permissions efficiently.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand security trimming patterns",
                        "Implement efficient filtered queries",
                        "Balance security and performance"
                    },
                    Content = @"
<h2>Content Security Trimming</h2>
<p>Security trimming ensures users only see content they're authorized to access. Optimizely provides built-in trimming, but understanding how it works helps optimize performance.</p>

<h3>Automatic Security Trimming</h3>
<p><code>IContentLoader</code> automatically filters results based on current user. However, for large result sets, this can be inefficient.</p>

<h3>Query-Level Filtering</h3>
<p>For better performance with Search & Navigation:</p>
<ul>
    <li><code>FilterForVisitor()</code> - Filters at query time</li>
    <li>Pre-indexed access rights reduce runtime filtering</li>
</ul>

<h3>Performance Considerations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Approach</th>
            <th class=""px-4 py-2 text-left"">Performance</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">IContentLoader default</td><td class=""px-4 py-2"">Good for small sets</td><td class=""px-4 py-2"">Single page, menus</td></tr>
        <tr><td class=""px-4 py-2"">FilterForVisitor()</td><td class=""px-4 py-2"">Better for large sets</td><td class=""px-4 py-2"">Search results</td></tr>
        <tr><td class=""px-4 py-2"">Pre-filtered cache</td><td class=""px-4 py-2"">Best performance</td><td class=""px-4 py-2"">Anonymous public content</td></tr>
    </tbody>
</table>

<h3>Anonymous vs Authenticated</h3>
<p>Consider separate caching strategies:</p>
<ul>
    <li>Anonymous users often share the same permissions - cache aggressively</li>
    <li>Authenticated users may have unique permissions - cache per role, not user</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "security-trimming-example",
                            Title = "Efficient Security Trimming",
                            Description = "Patterns for optimized content filtering",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;

public class SecuredContentService
{
    private readonly IContentLoader _contentLoader;
    private readonly IClient _searchClient;
    private readonly IContentSecurityRepository _securityRepo;

    // Search with query-level security (more efficient)
    public IEnumerable<ArticlePage> SearchWithSecurity(string query)
    {
        return _searchClient.Search<ArticlePage>()
            .For(query)
            .FilterForVisitor()  // Security at query level
            .Take(50)
            .GetContentResult()
            .Items;
    }

    // Get children with manual security check (when needed)
    public IEnumerable<T> GetAccessibleChildren<T>(
        ContentReference parent) where T : IContent
    {
        // Use FilterAccess extension for efficiency
        return _contentLoader.GetChildren<T>(parent)
            .Where(content => HasReadAccess(content.ContentLink));
    }

    private bool HasReadAccess(ContentReference reference)
    {
        var descriptor = _securityRepo.Get(reference);
        return descriptor.HasAccess(
            PrincipalInfo.CurrentPrincipal,
            AccessLevel.Read);
    }

    // For public-only content (no security check needed)
    public IEnumerable<T> GetPublicChildren<T>(
        ContentReference parent) where T : IContent
    {
        // Bypass security for known public content (faster)
        return _contentLoader.GetChildren<T>(
            parent,
            new LoaderOptions { BypassAccessCheck = true });
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ar-authorization-policies",
                    ModuleId = "access-rights",
                    Title = "Authorization Policies",
                    Summary = "Implement ASP.NET Core authorization policies with CMS content.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create custom authorization requirements",
                        "Integrate policies with CMS access rights",
                        "Secure controllers and API endpoints"
                    },
                    Content = @"
<h2>Authorization Policies</h2>
<p>ASP.NET Core authorization policies provide a flexible way to secure your application beyond CMS content permissions.</p>

<h3>Policy-Based Authorization</h3>
<p>Define policies for:</p>
<ul>
    <li>API endpoints</li>
    <li>Admin features</li>
    <li>Custom functionality</li>
    <li>Feature access</li>
</ul>

<h3>Combining CMS and Policy Authorization</h3>
<p>Use both systems together:</p>
<ul>
    <li><strong>CMS Access Rights</strong> - Content-level permissions</li>
    <li><strong>Authorization Policies</strong> - Feature-level permissions</li>
</ul>

<h3>Built-in CMS Policies</h3>
<p>Optimizely provides several built-in policies:</p>
<ul>
    <li><code>EPiServer:CmsAdmin</code> - CMS administrators</li>
    <li><code>EPiServer:CmsEdit</code> - Edit mode access</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "auth-policy-example",
                            Title = "Custom Authorization Policy",
                            Description = "Creating and using authorization policies",
                            Type = ExampleType.Code,
                            ExampleContent = @"using Microsoft.AspNetCore.Authorization;

// Custom requirement
public class MinimumContentCountRequirement : IAuthorizationRequirement
{
    public int MinimumCount { get; }
    public MinimumContentCountRequirement(int count) => MinimumCount = count;
}

// Handler for the requirement
public class MinimumContentCountHandler :
    AuthorizationHandler<MinimumContentCountRequirement>
{
    private readonly IContentRepository _contentRepo;

    public MinimumContentCountHandler(IContentRepository contentRepo)
    {
        _contentRepo = contentRepo;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MinimumContentCountRequirement requirement)
    {
        var userId = context.User.Identity?.Name;
        if (userId == null) return Task.CompletedTask;

        // Check if user has created minimum content
        var contentCount = GetUserContentCount(userId);
        if (contentCount >= requirement.MinimumCount)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private int GetUserContentCount(string userId) => 10; // Simplified
}

// Register policies in Program.cs
builder.Services.AddAuthorization(options =>
{
    // Custom policy
    options.AddPolicy(""ExperiencedEditor"", policy =>
        policy.Requirements.Add(new MinimumContentCountRequirement(50)));

    // Role-based policy
    options.AddPolicy(""ContentAdmin"", policy =>
        policy.RequireRole(""CmsAdmins"", ""WebAdmins""));

    // Claim-based policy
    options.AddPolicy(""PremiumUser"", policy =>
        policy.RequireClaim(""subscription"", ""premium""));
});

// Register handler
builder.Services.AddSingleton<IAuthorizationHandler,
    MinimumContentCountHandler>();

// Use in controller
[Authorize(Policy = ""ExperiencedEditor"")]
public class AdvancedToolsController : Controller
{
    public IActionResult BulkEdit() => View();
}

// Use in Razor Pages
[Authorize(Policy = ""ContentAdmin"")]
public class AdminSettingsModel : PageModel { }",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    private LearningModule BuildCachingPerformanceModule()
    {
        return new LearningModule
        {
            Id = "caching-performance",
            Title = "Caching & Performance",
            Description = "Optimize performance with caching strategies.",
            Icon = "rocket-launch",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "content-management" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cp-caching-overview",
                    ModuleId = "caching-performance",
                    Title = "Caching Overview",
                    Summary = "Understand caching in Optimizely CMS.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand built-in CMS caching",
                        "Know the different cache layers",
                        "Learn cache invalidation patterns"
                    },
                    Content = @"
<h2>Caching in CMS 12</h2>
<p>CMS automatically caches content and other data to improve performance. Understanding these caches helps you optimize your site.</p>

<h3>Built-in Caches</h3>
<ul>
    <li><strong>Content cache</strong> - Loaded content instances are cached</li>
    <li><strong>Property cache</strong> - Property values are cached</li>
    <li><strong>Content type cache</strong> - Type definitions are cached</li>
</ul>

<h3>Cache Invalidation</h3>
<p>CMS automatically invalidates cache when content changes. In load-balanced environments, cache invalidation is synchronized across servers.</p>

<h3>No Built-in Output Caching</h3>
<p>CMS 12 does not have built-in output caching. Use ASP.NET Core response caching or third-party solutions.</p>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "cp-object-cache",
                    ModuleId = "caching-performance",
                    Title = "Object Caching",
                    Summary = "Cache custom objects with IObjectInstanceCache.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Use IObjectInstanceCache for custom caching",
                        "Set cache policies and dependencies",
                        "Handle cache in load-balanced environments"
                    },
                    Content = @"
<h2>Object Caching</h2>
<p>Use <code>IObjectInstanceCache</code> to cache your own objects. For load-balanced environments, use <code>ISynchronizedObjectInstanceCache</code>.</p>

<h3>Cache Policies</h3>
<ul>
    <li><strong>Absolute expiration</strong> - Cache expires at a specific time</li>
    <li><strong>Sliding expiration</strong> - Cache expires after inactivity</li>
    <li><strong>Dependencies</strong> - Cache invalidates when dependencies change</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "object-cache-example",
                            Title = "Using Object Cache",
                            Description = "Cache expensive operations",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Framework.Cache;

public class CachedDataService
{
    private readonly ISynchronizedObjectInstanceCache _cache;
    private readonly IExternalApiClient _apiClient;
    private const string CacheKey = ""ExternalApiData"";

    public CachedDataService(
        ISynchronizedObjectInstanceCache cache,
        IExternalApiClient apiClient)
    {
        _cache = cache;
        _apiClient = apiClient;
    }

    public async Task<ExternalData> GetDataAsync()
    {
        // Try to get from cache
        var cached = _cache.Get(CacheKey) as ExternalData;
        if (cached != null)
        {
            return cached;
        }

        // Load from external source
        var data = await _apiClient.FetchDataAsync();

        // Cache with 5 minute expiration
        var policy = new CacheEvictionPolicy(
            TimeSpan.FromMinutes(5),
            CacheTimeoutType.Absolute);

        _cache.Insert(CacheKey, data, policy);

        return data;
    }

    // Cache with content dependency
    public ArticleData GetArticleData(ContentReference articleRef)
    {
        var cacheKey = $""ArticleData_{articleRef}"";

        var cached = _cache.Get(cacheKey) as ArticleData;
        if (cached != null) return cached;

        var data = BuildArticleData(articleRef);

        // Invalidate when the content changes
        var dependency = new ContentCacheDependency(articleRef);
        var policy = new CacheEvictionPolicy(dependency);

        _cache.Insert(cacheKey, data, policy);

        return data;
    }

    public void InvalidateCache()
    {
        _cache.Remove(CacheKey);
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cp-output-caching",
                    ModuleId = "caching-performance",
                    Title = "Output Caching",
                    Summary = "Implement output caching for HTML responses.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Implement response caching",
                        "Use CacheTagHelper for partial caching",
                        "Invalidate cache on content publish"
                    },
                    Content = @"
<h2>Output Caching</h2>
<p>CMS 12 doesn't include output caching, but you can implement it using ASP.NET Core features or third-party packages.</p>

<h3>Options</h3>
<ul>
    <li><strong>Response Caching Middleware</strong> - Basic ASP.NET Core caching</li>
    <li><strong>.NET 7+ Output Caching</strong> - More flexible caching policies</li>
    <li><strong>Cache Tag Helper</strong> - Cache portions of views</li>
    <li><strong>Third-party</strong> - WebEssentials.AspNetCore.OutputCaching</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "cache-tag-helper",
                            Title = "Partial View Caching",
                            Description = "Cache expensive view sections",
                            Type = ExampleType.Code,
                            ExampleContent = @"@* Cache an expensive section of the view *@
<cache expires-after=""@TimeSpan.FromMinutes(10)"" vary-by=""@Model.ContentLink"">
    @await Component.InvokeAsync(""ExpensiveWidget"", new { page = Model })
</cache>

@* Cache with content-based invalidation *@
@inject IContentCacheKeyCreator CacheKeyCreator

<cache expires-after=""@TimeSpan.FromHours(1)""
       vary-by=""@CacheKeyCreator.CreateCommonCacheKey(Model.ContentLink)"">
    <nav class=""navigation"">
        @await Html.PartialAsync(""_MainNavigation"")
    </nav>
</cache>

// For full page caching, handle publish events
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class CacheInvalidationModule : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var events = context.Locate.Advanced.GetInstance<IContentEvents>();
        events.PublishedContent += (s, e) =>
        {
            // Clear output cache when content is published
            var cache = context.Locate.Advanced
                .GetInstance<IOutputCacheManager>();
            cache.ClearAsync().Wait();
        };
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cp-query-optimization",
                    ModuleId = "caching-performance",
                    Title = "Query Optimization",
                    Summary = "Optimize IContentLoader queries and database access patterns.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Avoid N+1 query problems",
                        "Use batch loading for efficiency",
                        "Profile and analyze query performance"
                    },
                    Content = @"
<h2>Query Optimization</h2>
<p>Inefficient content loading is a common performance issue. Understanding how <code>IContentLoader</code> works helps avoid common pitfalls.</p>

<h3>The N+1 Problem</h3>
<p>Loading content in a loop causes N+1 database queries:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// BAD: N+1 queries
foreach (var reference in references)
{
    var content = _contentLoader.Get&lt;IContent&gt;(reference);
}

// GOOD: Single batch query
var contents = _contentLoader.GetItems(references, defaultLanguage);</code></pre>

<h3>Batch Loading</h3>
<p>Use <code>GetItems()</code> to load multiple content items in a single query:</p>
<ul>
    <li>Up to 100x faster for large lists</li>
    <li>Reduces database round-trips</li>
    <li>Automatic cache utilization</li>
</ul>

<h3>Projection Loading</h3>
<p>When you only need specific properties, consider:</p>
<ul>
    <li>Loading from index (Search & Navigation)</li>
    <li>Custom queries for specific use cases</li>
    <li>Cached projections for repeated access</li>
</ul>

<h3>Profiling Tools</h3>
<ul>
    <li><strong>SQL Server Profiler</strong> - See actual queries</li>
    <li><strong>Application Insights</strong> - Track dependencies</li>
    <li><strong>MiniProfiler</strong> - Add to development</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "query-optimization-example",
                            Title = "Optimized Content Loading",
                            Description = "Patterns for efficient content queries",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.Globalization;

public class OptimizedContentService
{
    private readonly IContentLoader _contentLoader;

    // BAD: N+1 queries
    public IEnumerable<ArticlePage> GetArticlesSlow(
        IEnumerable<ContentReference> refs)
    {
        foreach (var reference in refs)
        {
            // Each iteration = 1 query
            yield return _contentLoader.Get<ArticlePage>(reference);
        }
    }

    // GOOD: Single batch query
    public IEnumerable<ArticlePage> GetArticlesFast(
        IEnumerable<ContentReference> refs)
    {
        var language = ContentLanguage.PreferredCulture;
        return _contentLoader.GetItems(refs, language)
            .OfType<ArticlePage>();
    }

    // GOOD: Efficient navigation loading
    public IEnumerable<PageData> GetMenuItems(ContentReference root)
    {
        // GetChildren is already optimized for common scenarios
        return _contentLoader.GetChildren<PageData>(root)
            .Where(p => p.VisibleInMenu);
    }

    // GOOD: Preload referenced content
    public ArticlePage GetArticleWithReferences(ContentReference reference)
    {
        var article = _contentLoader.Get<ArticlePage>(reference);

        // Batch load all referenced content upfront
        var allRefs = new List<ContentReference>
        {
            article.RelatedArticle1,
            article.RelatedArticle2,
            article.Author
        }.Where(r => !ContentReference.IsNullOrEmpty(r));

        // This populates the cache for subsequent access
        _contentLoader.GetItems(allRefs, ContentLanguage.PreferredCulture);

        return article;
    }

    // For very large operations, use batching
    public async Task ProcessAllArticles(
        Func<ArticlePage, Task> processor)
    {
        const int batchSize = 100;
        var allRefs = GetAllArticleReferences();

        foreach (var batch in allRefs.Chunk(batchSize))
        {
            var articles = _contentLoader.GetItems(batch,
                ContentLanguage.PreferredCulture)
                .OfType<ArticlePage>();

            foreach (var article in articles)
            {
                await processor(article);
            }
        }
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cp-cdn-integration",
                    ModuleId = "caching-performance",
                    Title = "CDN Integration",
                    Summary = "Configure CDN caching and cache invalidation strategies.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure proper cache headers",
                        "Implement CDN cache invalidation",
                        "Handle cache purging on publish"
                    },
                    Content = @"
<h2>CDN Integration</h2>
<p>Content Delivery Networks (CDNs) cache content at edge locations close to users. Proper CDN configuration dramatically improves performance.</p>

<h3>Cache Headers</h3>
<p>Control CDN caching with response headers:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Header</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Cache-Control</td><td class=""px-4 py-2"">Primary caching directive</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Vary</td><td class=""px-4 py-2"">Cache per header value</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Surrogate-Control</td><td class=""px-4 py-2"">CDN-specific directive</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">ETag</td><td class=""px-4 py-2"">Revalidation support</td></tr>
    </tbody>
</table>

<h3>Cache Invalidation Strategies</h3>
<ul>
    <li><strong>Purge on publish</strong> - Clear CDN when content updates</li>
    <li><strong>Soft purge</strong> - Mark stale, serve while revalidating</li>
    <li><strong>Tag-based purge</strong> - Clear related content together</li>
</ul>

<h3>Common CDN Providers</h3>
<ul>
    <li>Azure Front Door / CDN</li>
    <li>Cloudflare</li>
    <li>Fastly</li>
    <li>AWS CloudFront</li>
    <li>Akamai</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "cdn-integration-example",
                            Title = "CDN Cache Configuration",
                            Description = "Setting up CDN caching with invalidation",
                            Type = ExampleType.Code,
                            ExampleContent = @"using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// Action filter to add cache headers
public class CacheHeaderFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.HttpContext.Response.StatusCode == 200)
        {
            var response = context.HttpContext.Response;

            // Public pages - cache for 1 hour, stale for 1 day
            response.Headers[""Cache-Control""] =
                ""public, max-age=3600, stale-while-revalidate=86400"";

            // Surrogate-Control for CDN (longer TTL)
            response.Headers[""Surrogate-Control""] =
                ""max-age=86400"";

            // Vary by encoding only (not by User-Agent)
            response.Headers[""Vary""] = ""Accept-Encoding"";
        }
    }
}

// CDN purge service
public interface ICdnPurgeService
{
    Task PurgeUrlAsync(string url);
    Task PurgeTagAsync(string tag);
}

public class CloudflarePurgeService : ICdnPurgeService
{
    private readonly HttpClient _httpClient;
    private readonly string _zoneId;
    private readonly string _apiToken;

    public async Task PurgeUrlAsync(string url)
    {
        var request = new
        {
            files = new[] { url }
        };

        await _httpClient.PostAsJsonAsync(
            $""zones/{_zoneId}/purge_cache"", request);
    }

    public async Task PurgeTagAsync(string tag)
    {
        var request = new
        {
            tags = new[] { tag }
        };

        await _httpClient.PostAsJsonAsync(
            $""zones/{_zoneId}/purge_cache"", request);
    }
}

// Purge on content publish
[InitializableModule]
public class CdnInvalidationModule : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var events = context.Locate.Advanced
            .GetInstance<IContentEvents>();
        var purgeService = context.Locate.Advanced
            .GetInstance<ICdnPurgeService>();
        var urlResolver = context.Locate.Advanced
            .GetInstance<IUrlResolver>();

        events.PublishedContent += async (s, e) =>
        {
            var url = urlResolver.GetUrl(e.ContentLink);
            if (!string.IsNullOrEmpty(url))
            {
                await purgeService.PurgeUrlAsync(url);
            }
        };
    }
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    private LearningModule BuildScheduledJobsAdvancedModule()
    {
        return new LearningModule
        {
            Id = "scheduled-jobs-advanced",
            Title = "Scheduled Jobs & Advanced Topics",
            Description = "Create scheduled jobs and explore advanced development.",
            Icon = "clock",
            Order = 11,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "initialization-events" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "sja-scheduled-jobs",
                    ModuleId = "scheduled-jobs-advanced",
                    Title = "Creating Scheduled Jobs",
                    Summary = "Build background jobs that run on a schedule.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create a scheduled job",
                        "Configure job scheduling",
                        "Handle job execution and progress"
                    },
                    Content = @"
<h2>Scheduled Jobs</h2>
<p>Scheduled jobs run background tasks on a configurable schedule. Use them for:</p>
<ul>
    <li>Content cleanup and maintenance</li>
    <li>Data imports/exports</li>
    <li>Cache warming</li>
    <li>Integration synchronization</li>
</ul>

<h3>Job Lifecycle</h3>
<ol>
    <li>Job is discovered during startup</li>
    <li>Registered in the admin interface</li>
    <li>Executed manually or on schedule</li>
    <li>Progress and status reported to UI</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "scheduled-job",
                            Title = "Content Cleanup Job",
                            Description = "A job that cleans up old content",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Scheduler;

[ScheduledPlugIn(
    DisplayName = ""Content Cleanup Job"",
    Description = ""Removes expired content older than 30 days"",
    GUID = ""11111111-2222-3333-4444-555555555555"",
    DefaultEnabled = true,
    InitialTime = ""00:30"",
    IntervalType = ScheduledIntervalType.Days,
    IntervalLength = 1)]
public class ContentCleanupJob : ScheduledJobBase
{
    private readonly IContentRepository _contentRepository;
    private bool _stopSignaled;
    private int _processedCount;

    public ContentCleanupJob(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
        IsStoppable = true;
    }

    public override void Stop()
    {
        _stopSignaled = true;
    }

    public override string Execute()
    {
        _stopSignaled = false;
        _processedCount = 0;

        var cutoffDate = DateTime.Now.AddDays(-30);
        var expiredContent = FindExpiredContent(cutoffDate);

        foreach (var content in expiredContent)
        {
            if (_stopSignaled)
            {
                return $""Job stopped. Processed {_processedCount} items."";
            }

            // Delete or archive the content
            _contentRepository.Delete(content.ContentLink, true);
            _processedCount++;

            // Report progress
            OnStatusChanged($""Processed {_processedCount} items..."");
        }

        return $""Cleanup complete. Removed {_processedCount} items."";
    }

    private IEnumerable<IContent> FindExpiredContent(DateTime cutoff)
    {
        // Implementation to find expired content
        yield break;
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sja-configuration",
                    ModuleId = "scheduled-jobs-advanced",
                    Title = "Configuration Options",
                    Summary = "Configure CMS behavior through options classes.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure CMS options in code",
                        "Use appsettings.json for configuration",
                        "Override settings per environment"
                    },
                    Content = @"
<h2>Configuration Options</h2>
<p>CMS settings are exposed through strongly-typed options classes that can be configured in code or appsettings.json.</p>

<h3>Common Options Classes</h3>
<ul>
    <li><code>ContentOptions</code> - Content behavior settings</li>
    <li><code>SchedulerOptions</code> - Job scheduler settings</li>
    <li><code>BlobOptions</code> - Blob storage configuration</li>
    <li><code>UIOptions</code> - Editor UI settings</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "config-options",
                            Title = "Configuring CMS Options",
                            Description = "Configure options in code and appsettings.json",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Program.cs - Configure in code
builder.Services.Configure<ContentOptions>(options =>
{
    options.RequireEditAccessToChangedByProperty = true;
    options.MultiSiteEnabled = true;
});

builder.Services.Configure<SchedulerOptions>(options =>
{
    options.Enabled = true;
    options.PingTime = TimeSpan.FromMinutes(1);
});

builder.Services.Configure<BlobOptions>(options =>
{
    options.DefaultProvider = ""azure"";
});

// appsettings.json
{
  ""EPiServer"": {
    ""Cms"": {
      ""Content"": {
        ""RequireEditAccessToChangedByProperty"": true
      },
      ""Scheduler"": {
        ""Enabled"": true,
        ""PingTime"": ""00:01:00""
      }
    }
  }
}

// Environment-specific: appsettings.Production.json
{
  ""EPiServer"": {
    ""Cms"": {
      ""Scheduler"": {
        ""Enabled"": true
      }
    }
  }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sja-deployment",
                    ModuleId = "scheduled-jobs-advanced",
                    Title = "Deployment to DXP",
                    Summary = "Deploy your CMS 12 site to Optimizely DXP Cloud.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand DXP Cloud deployment",
                        "Configure for cloud environments",
                        "Use deployment APIs and CI/CD"
                    },
                    Content = @"
<h2>Deploying to DXP Cloud</h2>
<p>Optimizely DXP (Digital Experience Platform) is the managed cloud hosting platform for CMS 12.</p>

<h3>DXP Features</h3>
<ul>
    <li><strong>Managed hosting</strong> - Azure-based infrastructure</li>
    <li><strong>Auto-scaling</strong> - Handle traffic spikes</li>
    <li><strong>Multiple environments</strong> - Integration, Preproduction, Production</li>
    <li><strong>Deployment API</strong> - CI/CD integration</li>
    <li><strong>CDN</strong> - Built-in content delivery</li>
</ul>

<h3>Cloud-Specific Configuration</h3>
<ul>
    <li>Use Azure Blob Storage for media</li>
    <li>Configure distributed cache</li>
    <li>Set up proper connection strings</li>
    <li>Enable Application Insights</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "dxp-config",
                            Title = "DXP Configuration",
                            Description = "Configure for DXP Cloud deployment",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"// appsettings.Production.json for DXP
{
  ""ConnectionStrings"": {
    ""EPiServerDB"": ""[Set by DXP]""
  },
  ""EPiServer"": {
    ""Cms"": {
      ""MappedRoles"": {
        ""CmsAdmins"": [""WebAdmins""],
        ""CmsEditors"": [""WebEditors""]
      }
    },
    ""Find"": {
      ""ServiceUrl"": ""[Set by DXP]"",
      ""DefaultIndex"": ""[Set by DXP]""
    }
  },
  ""ApplicationInsights"": {
    ""ConnectionString"": ""[Set by DXP]""
  }
}

// Program.cs - DXP-specific services
if (builder.Environment.IsProduction())
{
    // Use Azure Blob Storage
    builder.Services.AddAzureBlobProvider();

    // Use Azure Service Bus for events
    builder.Services.AddAzureEventProvider();
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sja-best-practices",
                    ModuleId = "scheduled-jobs-advanced",
                    Title = "Best Practices & Tips",
                    Summary = "Learn best practices for CMS 12 development.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Follow CMS development best practices",
                        "Avoid common pitfalls",
                        "Write maintainable code"
                    },
                    Content = @"
<h2>Best Practices</h2>

<h3>Content Types</h3>
<ul>
    <li>Always assign a GUID to content types for safe refactoring</li>
    <li>Use <code>[CultureSpecific]</code> only when needed</li>
    <li>Keep content types focused - avoid ""god objects""</li>
    <li>Use blocks for reusable components</li>
</ul>

<h3>Performance</h3>
<ul>
    <li>Cache expensive operations</li>
    <li>Avoid loading content in loops (N+1 problem)</li>
    <li>Use <code>IContentLoader</code> for read operations</li>
    <li>Minimize calls to external services</li>
</ul>

<h3>Security</h3>
<ul>
    <li>Never bypass access checks without good reason</li>
    <li>Validate all user input</li>
    <li>Use HTTPS everywhere</li>
    <li>Follow principle of least privilege</li>
</ul>

<h3>Code Organization</h3>
<ul>
    <li>Keep controllers thin</li>
    <li>Use dependency injection</li>
    <li>Write unit tests for business logic</li>
    <li>Document complex content models</li>
</ul>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 12: Media & BLOB Management

    private LearningModule BuildMediaBlobsModule()
    {
        return new LearningModule
        {
            Id = "media-blobs",
            Title = "Media & BLOB Management",
            Description = "Manage media assets and configure BLOB storage providers.",
            Icon = "photo",
            Order = 12,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-types" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "mb-media-types",
                    ModuleId = "media-blobs",
                    Title = "Media Types & Assets",
                    Summary = "Work with images, videos, and documents in CMS 12.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand built-in media types",
                        "Create custom media types",
                        "Configure media folders and organization"
                    },
                    Content = @"
<h2>Media Types in CMS 12</h2>
<p>Media assets are content items that represent files. CMS 12 provides built-in types and extensibility for custom media handling.</p>

<h3>Built-in Media Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Base Class</th>
            <th class=""px-4 py-2 text-left"">Extensions</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Images</td><td class=""px-4 py-2 font-mono text-sm"">ImageData</td><td class=""px-4 py-2"">.jpg, .png, .gif, .svg, .webp</td></tr>
        <tr><td class=""px-4 py-2"">Videos</td><td class=""px-4 py-2 font-mono text-sm"">VideoData</td><td class=""px-4 py-2"">.mp4, .webm, .mov</td></tr>
        <tr><td class=""px-4 py-2"">Documents</td><td class=""px-4 py-2 font-mono text-sm"">MediaData</td><td class=""px-4 py-2"">.pdf, .docx, .xlsx</td></tr>
    </tbody>
</table>

<h3>Media Properties</h3>
<ul>
    <li><code>BinaryData</code> - The actual file data</li>
    <li><code>RouteSegment</code> - URL-safe file name</li>
    <li><code>ContentType</code> - MIME type</li>
    <li><code>Thumbnail</code> - Auto-generated thumbnail</li>
</ul>

<h3>Asset Folders</h3>
<p>Organize media in folders using <code>ContentFolder</code>. The root is typically <code>SiteDefinition.Current.GlobalAssetsRoot</code>.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "custom-media-type",
                            Title = "Custom Media Type",
                            Description = "Create a specialized image type with metadata",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

[ContentType(
    DisplayName = ""Brand Image"",
    GUID = ""11111111-2222-3333-4444-555555555555"",
    Description = ""Image with brand-specific metadata"")]
[MediaDescriptor(ExtensionString = ""jpg,jpeg,png,webp"")]
public class BrandImage : ImageData
{
    [Display(Name = ""Alt Text"", Order = 100)]
    [Required]
    public virtual string AltText { get; set; }

    [Display(Name = ""Photographer"", Order = 110)]
    public virtual string Photographer { get; set; }

    [Display(Name = ""Copyright Notice"", Order = 120)]
    public virtual string CopyrightNotice { get; set; }

    [Display(Name = ""Usage Rights"", Order = 130)]
    [SelectOne(SelectionFactoryType = typeof(UsageRightsFactory))]
    public virtual string UsageRights { get; set; }

    [Display(Name = ""Expiration Date"", Order = 140)]
    public virtual DateTime? ExpirationDate { get; set; }

    [Display(Name = ""Tags"", Order = 150)]
    public virtual IList<string> Tags { get; set; }
}

// Custom video type
[ContentType(DisplayName = ""Training Video"")]
[MediaDescriptor(ExtensionString = ""mp4,webm"")]
public class TrainingVideo : VideoData
{
    [Display(Name = ""Duration (seconds)"")]
    public virtual int DurationSeconds { get; set; }

    [Display(Name = ""Transcript"")]
    [UIHint(UIHint.Textarea)]
    public virtual string Transcript { get; set; }

    [Display(Name = ""Chapters"")]
    public virtual ContentArea Chapters { get; set; }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "mb-blob-providers",
                    ModuleId = "media-blobs",
                    Title = "BLOB Provider Architecture",
                    Summary = "Configure where media files are stored.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the BLOB provider architecture",
                        "Configure Azure Blob Storage",
                        "Migrate between providers"
                    },
                    Content = @"
<h2>BLOB Provider Architecture</h2>
<p>BLOB providers determine where media files are physically stored. CMS 12 supports multiple storage backends.</p>

<h3>Built-in Providers</h3>
<ul>
    <li><strong>FileBlobProvider</strong> - Local file system (default)</li>
    <li><strong>AzureBlobProvider</strong> - Azure Blob Storage (recommended for DXP)</li>
</ul>

<h3>Provider Hierarchy</h3>
<p>Providers are registered for specific paths and can be chained. The most specific match wins.</p>

<h3>Choosing a Provider</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Scenario</th>
            <th class=""px-4 py-2 text-left"">Recommended</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Local development</td><td class=""px-4 py-2"">FileBlobProvider</td></tr>
        <tr><td class=""px-4 py-2"">DXP Cloud</td><td class=""px-4 py-2"">AzureBlobProvider (auto-configured)</td></tr>
        <tr><td class=""px-4 py-2"">Self-hosted, single server</td><td class=""px-4 py-2"">FileBlobProvider</td></tr>
        <tr><td class=""px-4 py-2"">Self-hosted, load-balanced</td><td class=""px-4 py-2"">AzureBlobProvider or shared storage</td></tr>
    </tbody>
</table>

<h3>Migration Considerations</h3>
<p>When migrating between providers:</p>
<ul>
    <li>Plan for downtime or staged migration</li>
    <li>Use blob migration tools</li>
    <li>Verify all references after migration</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "azure-blob-config",
                            Title = "Azure Blob Storage Configuration",
                            Description = "Configure Azure Blob Storage for media",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Install: EPiServer.Azure

// appsettings.json
{
  ""EPiServer"": {
    ""Cms"": {
      ""AzureBlobProvidersEnabled"": true
    }
  },
  ""ConnectionStrings"": {
    ""EPiServerAzureBlobs"": ""DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net""
  }
}

// Program.cs
builder.Services.AddAzureBlobProvider(""blobs"", options =>
{
    options.Container = ""optimizely-blobs"";
    options.CreateContainerIfNotExist = true;
});

// Custom blob provider for specific content
public class CustomBlobProvider : IBlobProvider
{
    public Uri Create(Guid id, string extension)
    {
        // Generate storage location
        var path = $""{id:N}{extension}"";
        return new Uri($""custom://blobs/{path}"");
    }

    public Stream Open(Uri id, FileMode mode, FileAccess access)
    {
        // Open stream to blob
        throw new NotImplementedException();
    }

    public void Delete(Uri id)
    {
        // Delete blob
        throw new NotImplementedException();
    }
}

// Register custom provider in Program.cs
services.AddTransient<IBlobProvider, CustomBlobProvider>();",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "mb-image-processing",
                    ModuleId = "media-blobs",
                    Title = "Image Processing",
                    Summary = "Resize, crop, and optimize images on-the-fly.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use ImageResizer for dynamic resizing",
                        "Implement responsive images",
                        "Optimize image delivery performance"
                    },
                    Content = @"
<h2>Image Processing</h2>
<p>CMS 12 supports dynamic image processing through URL parameters, allowing responsive images without pre-generating variants.</p>

<h3>URL-Based Resizing</h3>
<p>Add query parameters to image URLs:</p>
<ul>
    <li><code>?width=400</code> - Resize to width</li>
    <li><code>?height=300</code> - Resize to height</li>
    <li><code>?width=400&height=300&mode=crop</code> - Crop to dimensions</li>
</ul>

<h3>Resize Modes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Mode</th>
            <th class=""px-4 py-2 text-left"">Behavior</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">max</td><td class=""px-4 py-2"">Fit within bounds, maintain aspect ratio</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">crop</td><td class=""px-4 py-2"">Fill exact dimensions, crop excess</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">pad</td><td class=""px-4 py-2"">Fit within bounds, pad to dimensions</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">stretch</td><td class=""px-4 py-2"">Stretch to exact dimensions</td></tr>
    </tbody>
</table>

<h3>Format Conversion</h3>
<p>Convert images to modern formats:</p>
<ul>
    <li><code>?format=webp</code> - Convert to WebP</li>
    <li><code>?quality=80</code> - Adjust quality</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "responsive-images",
                            Title = "Responsive Image Helper",
                            Description = "Generate srcset for responsive images",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Html;

public class ImageService
{
    private readonly IUrlResolver _urlResolver;

    public ImageService(IUrlResolver urlResolver)
    {
        _urlResolver = urlResolver;
    }

    // Generate srcset for responsive images
    public string GetSrcSet(ContentReference imageRef, int[] widths)
    {
        var baseUrl = _urlResolver.GetUrl(imageRef);
        if (string.IsNullOrEmpty(baseUrl)) return string.Empty;

        var srcset = widths.Select(w =>
            $""{baseUrl}?width={w} {w}w"");

        return string.Join("", "", srcset);
    }

    // Get picture element with WebP support
    public IHtmlContent GetPictureElement(
        ContentReference imageRef,
        string alt,
        int[] widths)
    {
        var baseUrl = _urlResolver.GetUrl(imageRef);
        var webpSrcset = GetSrcSetWithFormat(baseUrl, widths, ""webp"");
        var jpgSrcset = GetSrcSetWithFormat(baseUrl, widths, null);

        return new HtmlString($@""
            <picture>
                <source srcset=""""{webpSrcset}"""" type=""""image/webp"""">
                <source srcset=""""{jpgSrcset}"""">
                <img src=""""{baseUrl}?width={widths.Last()}""""
                     alt=""""{alt}""""
                     loading=""""lazy"""">
            </picture>"");
    }

    private string GetSrcSetWithFormat(
        string baseUrl, int[] widths, string? format)
    {
        var srcset = widths.Select(w =>
        {
            var url = $""{baseUrl}?width={w}"";
            if (!string.IsNullOrEmpty(format))
                url += $""&format={format}"";
            return $""{url} {w}w"";
        });

        return string.Join("", "", srcset);
    }
}

// Usage in Razor
@inject ImageService ImageService

<img src=""@Url.ContentUrl(Model.HeroImage)""
     srcset=""@ImageService.GetSrcSet(Model.HeroImage, new[] {400, 800, 1200})""
     sizes=""(max-width: 768px) 100vw, 50vw""
     alt=""@Model.HeroImageAlt""
     loading=""lazy"">",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "mb-management-api",
                    ModuleId = "media-blobs",
                    Title = "Media Management API",
                    Summary = "Create, upload, and manage media programmatically.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Upload media files via API",
                        "Manage media metadata programmatically",
                        "Implement bulk media operations"
                    },
                    Content = @"
<h2>Media Management API</h2>
<p>Use <code>IContentRepository</code> and <code>IBlobFactory</code> to manage media programmatically.</p>

<h3>Common Operations</h3>
<ul>
    <li>Create media from uploaded files</li>
    <li>Update media metadata</li>
    <li>Move and organize media</li>
    <li>Delete media safely</li>
</ul>

<h3>Important Considerations</h3>
<ul>
    <li>Media creates versions like other content</li>
    <li>Deleting media may break references</li>
    <li>Large uploads may need streaming</li>
    <li>Consider async processing for bulk operations</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "media-upload-api",
                            Title = "Media Upload Service",
                            Description = "Upload and manage media via API",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Framework.Blobs;
using EPiServer.Security;

public class MediaUploadService
{
    private readonly IContentRepository _contentRepo;
    private readonly IBlobFactory _blobFactory;
    private readonly ContentAssetHelper _assetHelper;

    public MediaUploadService(
        IContentRepository contentRepo,
        IBlobFactory blobFactory,
        ContentAssetHelper assetHelper)
    {
        _contentRepo = contentRepo;
        _blobFactory = blobFactory;
        _assetHelper = assetHelper;
    }

    // Upload image to global assets
    public ContentReference UploadImage(
        Stream fileStream,
        string fileName,
        ContentReference parentFolder)
    {
        var image = _contentRepo.GetDefault<ImageData>(parentFolder);
        image.Name = fileName;

        // Create blob from stream
        var blob = _blobFactory.CreateBlob(
            image.BinaryDataContainer, Path.GetExtension(fileName));

        using (var blobStream = blob.OpenWrite())
        {
            fileStream.CopyTo(blobStream);
        }

        image.BinaryData = blob;

        // Save and publish
        return _contentRepo.Save(
            image,
            SaveAction.Publish,
            AccessLevel.NoAccess);
    }

    // Upload to content asset folder
    public ContentReference UploadToContentAssets(
        Stream fileStream,
        string fileName,
        ContentReference ownerContent)
    {
        var assetsFolder = _assetHelper.GetOrCreateAssetFolder(ownerContent);
        return UploadImage(fileStream, fileName, assetsFolder);
    }

    // Update media metadata
    public void UpdateMediaMetadata(
        ContentReference mediaRef,
        Action<MediaData> update)
    {
        var media = _contentRepo.Get<MediaData>(mediaRef)
            .CreateWritableClone() as MediaData;

        update(media);

        _contentRepo.Save(media, SaveAction.Publish);
    }

    // Find unused media
    public IEnumerable<ContentReference> FindUnreferencedMedia(
        ContentReference mediaFolder)
    {
        var allMedia = _contentRepo.GetDescendents(mediaFolder);
        var referenced = new HashSet<ContentReference>();

        // Scan all pages for references
        foreach (var pageRef in _contentRepo.GetDescendents(ContentReference.StartPage))
        {
            var page = _contentRepo.Get<IContent>(pageRef);
            // Check all ContentReference properties
            foreach (var prop in page.Property.OfType<PropertyContentReference>())
            {
                if (prop.ContentLink != null)
                    referenced.Add(prop.ContentLink);
            }
        }

        return allMedia.Except(referenced);
    }
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 13: Configuration & Settings

    private LearningModule BuildConfigurationSettingsModule()
    {
        return new LearningModule
        {
            Id = "configuration-settings",
            Title = "Configuration & Settings",
            Description = "Configure your CMS application using modern .NET patterns.",
            Icon = "cog-6-tooth",
            Order = 13,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cs-config-api",
                    ModuleId = "configuration-settings",
                    Title = "Configuration API",
                    Summary = "Use ASP.NET Core configuration with CMS 12.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the IOptions pattern",
                        "Configure CMS settings via appsettings.json",
                        "Create strongly-typed configuration classes"
                    },
                    Content = @"
<h2>Configuration API</h2>
<p>CMS 12 uses ASP.NET Core's configuration system. Settings come from multiple sources with override precedence.</p>

<h3>Configuration Sources (Priority Order)</h3>
<ol>
    <li>Command-line arguments</li>
    <li>Environment variables</li>
    <li><code>appsettings.{Environment}.json</code></li>
    <li><code>appsettings.json</code></li>
    <li>User secrets (development only)</li>
</ol>

<h3>CMS Configuration Sections</h3>
<p>Built-in CMS settings use the <code>EPiServer</code> section:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>{
  ""EPiServer"": {
    ""Cms"": { ... },
    ""Find"": { ... },
    ""Commerce"": { ... }
  }
}</code></pre>

<h3>IOptions Pattern</h3>
<p>Access configuration via dependency injection:</p>
<ul>
    <li><code>IOptions&lt;T&gt;</code> - Singleton, read once at startup</li>
    <li><code>IOptionsSnapshot&lt;T&gt;</code> - Scoped, reloads per request</li>
    <li><code>IOptionsMonitor&lt;T&gt;</code> - Singleton, change notifications</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ioptions-example",
                            Title = "Using IOptions Pattern",
                            Description = "Strongly-typed configuration access",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Configuration class
public class MyFeatureOptions
{
    public const string Section = ""MyFeature"";

    public bool Enabled { get; set; } = true;
    public int MaxItems { get; set; } = 10;
    public string ApiEndpoint { get; set; } = """";
    public List<string> AllowedOrigins { get; set; } = new();
}

// appsettings.json
{
  ""MyFeature"": {
    ""Enabled"": true,
    ""MaxItems"": 25,
    ""ApiEndpoint"": ""https://api.example.com"",
    ""AllowedOrigins"": [""https://site1.com"", ""https://site2.com""]
  }
}

// Register in Program.cs
builder.Services.Configure<MyFeatureOptions>(
    builder.Configuration.GetSection(MyFeatureOptions.Section));

// Validate on startup (optional but recommended)
builder.Services.AddOptions<MyFeatureOptions>()
    .Bind(builder.Configuration.GetSection(MyFeatureOptions.Section))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Use in a service
public class MyFeatureService
{
    private readonly MyFeatureOptions _options;

    public MyFeatureService(IOptions<MyFeatureOptions> options)
    {
        _options = options.Value;
    }

    public bool IsEnabled => _options.Enabled;

    public IEnumerable<Item> GetItems()
    {
        if (!_options.Enabled)
            return Enumerable.Empty<Item>();

        return FetchItems().Take(_options.MaxItems);
    }
}

// Use with change monitoring
public class FeatureMonitor
{
    public FeatureMonitor(IOptionsMonitor<MyFeatureOptions> optionsMonitor)
    {
        optionsMonitor.OnChange(options =>
        {
            Console.WriteLine($""Feature enabled: {options.Enabled}"");
        });
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cs-settings-framework",
                    ModuleId = "configuration-settings",
                    Title = "CMS Settings Framework",
                    Summary = "Create editor-manageable settings using content types.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create settings content types",
                        "Use site-specific settings",
                        "Access settings in code"
                    },
                    Content = @"
<h2>CMS Settings Framework</h2>
<p>For settings that editors should manage, create settings as content types. This provides a UI and version history.</p>

<h3>Settings Content Types</h3>
<p>Create content types without templates that serve as settings containers:</p>
<ul>
    <li>Global settings for the entire installation</li>
    <li>Site-specific settings per site</li>
    <li>Section-specific settings for areas of a site</li>
</ul>

<h3>Settings Location</h3>
<p>Store settings in the Global Assets folder or under the site root depending on scope.</p>

<h3>Caching Considerations</h3>
<p>Settings are content, so they're automatically cached. Changes publish immediately to all servers in a cluster.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "settings-content-type",
                            Title = "Settings Content Type",
                            Description = "Editor-manageable site settings",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Core;
using EPiServer.DataAnnotations;

// Settings content type (no template)
[ContentType(
    DisplayName = ""Site Settings"",
    GUID = ""22222222-3333-4444-5555-666666666666"",
    AvailableInEditMode = true)]
public class SiteSettings : PageData
{
    [Display(Name = ""Site Logo"", GroupName = ""Branding"", Order = 10)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Logo { get; set; }

    [Display(Name = ""Tagline"", GroupName = ""Branding"", Order = 20)]
    public virtual string Tagline { get; set; }

    [Display(Name = ""Contact Email"", GroupName = ""Contact"", Order = 30)]
    [EmailAddress]
    public virtual string ContactEmail { get; set; }

    [Display(Name = ""Social Links"", GroupName = ""Social"", Order = 40)]
    public virtual LinkItemCollection SocialLinks { get; set; }

    [Display(Name = ""Analytics ID"", GroupName = ""Tracking"", Order = 50)]
    public virtual string GoogleAnalyticsId { get; set; }

    [Display(Name = ""Footer Content"", GroupName = ""Layout"", Order = 60)]
    public virtual ContentArea FooterContent { get; set; }
}

// Service to access settings
public interface ISiteSettingsService
{
    SiteSettings GetSettings();
    T GetSettings<T>() where T : class, IContent;
}

public class SiteSettingsService : ISiteSettingsService
{
    private readonly IContentLoader _contentLoader;
    private readonly ISiteDefinitionResolver _siteResolver;

    public SiteSettingsService(
        IContentLoader contentLoader,
        ISiteDefinitionResolver siteResolver)
    {
        _contentLoader = contentLoader;
        _siteResolver = siteResolver;
    }

    public SiteSettings GetSettings()
    {
        return GetSettings<SiteSettings>();
    }

    public T GetSettings<T>() where T : class, IContent
    {
        var site = _siteResolver.GetByContent(
            ContentReference.StartPage, false);

        if (site == null)
            return null;

        // Settings stored as child of start page
        return _contentLoader.GetChildren<T>(site.StartPage)
            .FirstOrDefault();
    }
}

// Usage in controller
public class HomeController : Controller
{
    private readonly ISiteSettingsService _settingsService;

    public HomeController(ISiteSettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public IActionResult Index()
    {
        var settings = _settingsService.GetSettings();
        ViewBag.Tagline = settings?.Tagline;
        return View();
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cs-feature-flags",
                    ModuleId = "configuration-settings",
                    Title = "Feature Flags",
                    Summary = "Implement feature toggles for controlled rollouts.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use Microsoft.FeatureManagement",
                        "Create custom feature filters",
                        "Toggle features for specific users"
                    },
                    Content = @"
<h2>Feature Flags</h2>
<p>Feature flags enable controlled rollouts and A/B testing. Microsoft.FeatureManagement provides a robust framework.</p>

<h3>Benefits</h3>
<ul>
    <li><strong>Safe deployments</strong> - Deploy code without enabling features</li>
    <li><strong>Gradual rollouts</strong> - Enable for percentages of users</li>
    <li><strong>Quick rollbacks</strong> - Disable features without deployment</li>
    <li><strong>A/B testing</strong> - Test variations with real users</li>
</ul>

<h3>Built-in Filters</h3>
<ul>
    <li><code>Percentage</code> - Enable for X% of requests</li>
    <li><code>TimeWindow</code> - Enable between dates</li>
    <li><code>Targeting</code> - Enable for specific users/groups</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "feature-flags-example",
                            Title = "Feature Flags Implementation",
                            Description = "Configure and use feature flags",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Install: Microsoft.FeatureManagement.AspNetCore

// appsettings.json
{
  ""FeatureManagement"": {
    ""NewCheckoutFlow"": true,
    ""BetaFeatures"": {
      ""EnabledFor"": [
        {
          ""Name"": ""Percentage"",
          ""Parameters"": { ""Value"": 10 }
        }
      ]
    },
    ""HolidaySale"": {
      ""EnabledFor"": [
        {
          ""Name"": ""TimeWindow"",
          ""Parameters"": {
            ""Start"": ""2024-12-01T00:00:00Z"",
            ""End"": ""2024-12-31T23:59:59Z""
          }
        }
      ]
    }
  }
}

// Program.cs
builder.Services.AddFeatureManagement()
    .AddFeatureFilter<PercentageFilter>()
    .AddFeatureFilter<TimeWindowFilter>();

// Feature names as constants
public static class FeatureFlags
{
    public const string NewCheckoutFlow = ""NewCheckoutFlow"";
    public const string BetaFeatures = ""BetaFeatures"";
    public const string HolidaySale = ""HolidaySale"";
}

// Use in controller
public class CheckoutController : Controller
{
    private readonly IFeatureManager _featureManager;

    public CheckoutController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public async Task<IActionResult> Index()
    {
        if (await _featureManager.IsEnabledAsync(FeatureFlags.NewCheckoutFlow))
        {
            return View(""NewCheckout"");
        }
        return View(""Checkout"");
    }
}

// Use in Razor
@inject IFeatureManager FeatureManager

<feature name=""@FeatureFlags.HolidaySale"">
    <div class=""sale-banner"">
        Holiday Sale: 20% off everything!
    </div>
</feature>

// Custom feature filter
public class CmsRoleFilter : IFeatureFilter
{
    private readonly IPrincipalAccessor _principalAccessor;

    public CmsRoleFilter(IPrincipalAccessor principalAccessor)
    {
        _principalAccessor = principalAccessor;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var roles = context.Parameters.Get<string[]>(""Roles"");
        var principal = _principalAccessor.Principal;

        return Task.FromResult(
            roles?.Any(r => principal.IsInRole(r)) ?? false);
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "cs-environments",
                    ModuleId = "configuration-settings",
                    Title = "Environment Configuration",
                    Summary = "Manage configuration across development, staging, and production.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Use environment-specific configuration",
                        "Secure sensitive settings",
                        "Configure for DXP environments"
                    },
                    Content = @"
<h2>Environment Configuration</h2>
<p>Different environments need different settings. ASP.NET Core makes this easy with environment-specific files and variable overrides.</p>

<h3>Environment Detection</h3>
<p>The <code>ASPNETCORE_ENVIRONMENT</code> variable determines the environment:</p>
<ul>
    <li><code>Development</code> - Local development</li>
    <li><code>Integration</code> - DXP integration</li>
    <li><code>Preproduction</code> - DXP staging</li>
    <li><code>Production</code> - DXP production</li>
</ul>

<h3>Configuration File Loading</h3>
<p>Files are loaded in order, with later values overriding earlier:</p>
<ol>
    <li><code>appsettings.json</code></li>
    <li><code>appsettings.{Environment}.json</code></li>
</ol>

<h3>Securing Secrets</h3>
<p>Never commit secrets to source control:</p>
<ul>
    <li>Use User Secrets for development</li>
    <li>Use environment variables for production</li>
    <li>Consider Azure Key Vault for DXP</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "environment-config",
                            Title = "Environment-Specific Configuration",
                            Description = "Configure different settings per environment",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"// appsettings.json (shared settings)
{
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Information""
    }
  },
  ""EPiServer"": {
    ""Cms"": {
      ""MappedRoles"": {
        ""CmsAdmins"": ""WebAdmins, Administrators""
      }
    }
  }
}

// appsettings.Development.json
{
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Debug"",
      ""Microsoft.EntityFrameworkCore"": ""Information""
    }
  },
  ""ConnectionStrings"": {
    ""EPiServerDB"": ""Server=(localdb)\\mssqllocaldb;Database=MyApp;Trusted_Connection=True""
  },
  ""DetailedErrors"": true
}

// appsettings.Production.json
{
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Warning""
    }
  },
  ""DetailedErrors"": false
}

// For sensitive values, use User Secrets (development)
// dotnet user-secrets init
// dotnet user-secrets set ""ApiKeys:External"" ""secret-key-here""

// Or environment variables (production)
// ConnectionStrings__EPiServerDB=Server=...

// Program.cs - secure configuration
builder.Configuration
    .AddJsonFile(""appsettings.json"")
    .AddJsonFile($""appsettings.{builder.Environment.EnvironmentName}.json"", optional: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(optional: true);

// Conditional service registration
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddApplicationInsightsTelemetry();
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 14: Add-on Development

    private LearningModule BuildAddonDevelopmentModule()
    {
        return new LearningModule
        {
            Id = "addon-development",
            Title = "Add-on Development",
            Description = "Create reusable add-ons and protected modules.",
            Icon = "puzzle-piece",
            Order = 14,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "initialization-events", "content-types" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ad-architecture",
                    ModuleId = "addon-development",
                    Title = "Add-on Architecture",
                    Summary = "Understand how add-ons are discovered and loaded.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand add-on discovery and loading",
                        "Structure an add-on project",
                        "Register services and content types"
                    },
                    Content = @"
<h2>Add-on Architecture</h2>
<p>Add-ons are NuGet packages that extend CMS functionality. They can provide content types, services, admin interfaces, and more.</p>

<h3>Add-on Discovery</h3>
<p>CMS discovers add-ons through:</p>
<ul>
    <li><strong>Assembly scanning</strong> - Types with CMS attributes</li>
    <li><strong>Service registration</strong> - IConfigureServices implementations</li>
    <li><strong>Initialization modules</strong> - IInitializableModule implementations</li>
</ul>

<h3>Project Structure</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>MyAddon/
├── MyAddon.csproj
├── ServiceCollectionExtensions.cs
├── Models/
│   └── MyContentType.cs
├── Services/
│   └── MyAddonService.cs
├── Initialization/
│   └── MyAddonInitialization.cs
└── Views/
    └── Shared/
        └── Components/</code></pre>

<h3>Package Conventions</h3>
<ul>
    <li>Reference EPiServer packages as dependencies</li>
    <li>Provide extension methods for registration</li>
    <li>Document required configuration</li>
    <li>Version according to SemVer</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "addon-structure",
                            Title = "Add-on Project Structure",
                            Description = "Basic add-on with service registration",
                            Type = ExampleType.Code,
                            ExampleContent = @"// MyAddon.csproj
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <PackageId>MyCompany.MyAddon</PackageId>
    <Version>1.0.0</Version>
    <Authors>My Company</Authors>
    <Description>An Optimizely CMS add-on</Description>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""EPiServer.CMS.Core"" Version=""12.*"" />
  </ItemGroup>
</Project>

// ServiceCollectionExtensions.cs
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyAddon(
        this IServiceCollection services,
        Action<MyAddonOptions>? configure = null)
    {
        // Register options
        if (configure != null)
        {
            services.Configure(configure);
        }

        // Register services
        services.AddScoped<IMyAddonService, MyAddonService>();

        // Register any required CMS services
        services.AddTransient<IContentRepositoryDescriptor,
            MyAddonContentDescriptor>();

        return services;
    }
}

// Options class
public class MyAddonOptions
{
    public bool EnableFeatureX { get; set; } = true;
    public string ApiKey { get; set; } = """";
}

// Consumer usage in Program.cs
builder.Services.AddMyAddon(options =>
{
    options.EnableFeatureX = true;
    options.ApiKey = builder.Configuration[""MyAddon:ApiKey""];
});

// Initialization module for startup logic
[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class MyAddonInitialization : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var logger = context.Locate.Advanced
            .GetInstance<ILogger<MyAddonInitialization>>();
        logger.LogInformation(""MyAddon initialized"");

        // Subscribe to events, configure routes, etc.
    }

    public void Uninitialize(InitializationEngine context) { }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ad-protected-modules",
                    ModuleId = "addon-development",
                    Title = "Protected Modules",
                    Summary = "Create protected modules with embedded resources.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand protected modules",
                        "Embed and serve static resources",
                        "Configure module routing"
                    },
                    Content = @"
<h2>Protected Modules</h2>
<p>Protected modules serve content from <code>/EPiServer/{ModuleName}/</code> paths. They can embed static files, views, and other resources.</p>

<h3>When to Use Protected Modules</h3>
<ul>
    <li>Admin UI extensions</li>
    <li>Custom editors and widgets</li>
    <li>Module-specific static assets</li>
</ul>

<h3>Module Structure</h3>
<p>Resources are embedded in the assembly and served via a file provider.</p>

<h3>Security</h3>
<p>Protected module paths require CMS Edit access by default. Configure custom authorization as needed.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "protected-module",
                            Title = "Protected Module Setup",
                            Description = "Create a protected module with embedded resources",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Project file - embed resources
<Project Sdk=""Microsoft.NET.Sdk"">
  <ItemGroup>
    <EmbeddedResource Include=""module.config"" />
    <EmbeddedResource Include=""ClientResources\**\*"" />
  </ItemGroup>
</Project>

// module.config
<?xml version=""1.0"" encoding=""utf-8""?>
<module>
  <assemblies>
    <add assembly=""MyAddon"" />
  </assemblies>
  <clientResources>
    <add name=""epi-cms.widgets.base""
         path=""ClientResources/Scripts/MyWidget.js""
         resourceType=""Script"" />
  </clientResources>
</module>

// Startup configuration
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyAddon(
        this IServiceCollection services)
    {
        services.Configure<ProtectedModuleOptions>(options =>
        {
            options.Items.Add(new ModuleDetails
            {
                Name = ""MyAddon""
            });
        });

        return services;
    }
}

// Embedded file provider registration
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMyAddon(
        this IApplicationBuilder app)
    {
        var env = app.ApplicationServices
            .GetRequiredService<IWebHostEnvironment>();

        // Serve embedded static files
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new ManifestEmbeddedFileProvider(
                typeof(MyAddonModule).Assembly,
                ""ClientResources""),
            RequestPath = ""/EPiServer/MyAddon""
        });

        return app;
    }
}

// ClientResources/Scripts/MyWidget.js
define([""dojo/_base/declare""], function(declare) {
    return declare([], {
        startup: function() {
            console.log(""MyAddon widget started"");
        }
    });
});",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ad-admin-plugins",
                    ModuleId = "addon-development",
                    Title = "Admin Plugins",
                    Summary = "Add menu items and views to the CMS admin interface.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Add menu items to admin interface",
                        "Create admin views and tools",
                        "Implement scheduled job management"
                    },
                    Content = @"
<h2>Admin Plugins</h2>
<p>Extend the CMS admin interface with custom menu items, views, and tools.</p>

<h3>Menu Providers</h3>
<p>Use <code>IMenuProvider</code> to add menu items to the admin or edit interface.</p>

<h3>Admin Views</h3>
<p>Create Razor views rendered within the CMS admin frame.</p>

<h3>GuiPlugIn Attribute</h3>
<p>The legacy <code>[GuiPlugIn]</code> attribute still works for simple admin pages.</p>

<h3>Best Practices</h3>
<ul>
    <li>Follow CMS styling conventions</li>
    <li>Use appropriate authorization</li>
    <li>Provide helpful error messages</li>
    <li>Log admin actions for audit</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "admin-menu",
                            Title = "Admin Menu and View",
                            Description = "Add a custom admin tool",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer.Shell.Navigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Menu provider
[MenuProvider]
public class MyAddonMenuProvider : IMenuProvider
{
    public IEnumerable<MenuItem> GetMenuItems()
    {
        var section = new SectionMenuItem(""My Tools"",
            ""/global/myaddon"")
        {
            SortIndex = 100
        };

        var item = new UrlMenuItem(""Content Analyzer"",
            ""/global/myaddon/analyzer"",
            ""/MyAddon/Admin/Analyzer"")
        {
            SortIndex = 10
        };

        return new MenuItem[] { section, item };
    }
}

// Admin controller
[Authorize(Policy = ""EPiServer:CmsAdmin"")]
[Route(""[controller]/[action]"")]
public class MyAddonController : Controller
{
    private readonly IContentRepository _contentRepo;
    private readonly IContentTypeRepository _typeRepo;

    public MyAddonController(
        IContentRepository contentRepo,
        IContentTypeRepository typeRepo)
    {
        _contentRepo = contentRepo;
        _typeRepo = typeRepo;
    }

    [Route(""/MyAddon/Admin/Analyzer"")]
    public IActionResult Analyzer()
    {
        var stats = new ContentStats
        {
            TotalPages = CountContent<PageData>(),
            TotalBlocks = CountContent<BlockData>(),
            TotalMedia = CountContent<MediaData>(),
            ContentTypes = _typeRepo.List()
                .Select(t => new ContentTypeInfo
                {
                    Name = t.DisplayName,
                    Count = CountByType(t.ID)
                })
                .OrderByDescending(t => t.Count)
                .Take(10)
                .ToList()
        };

        return View(stats);
    }

    private int CountContent<T>() where T : IContent
    {
        // Simplified - use a more efficient query in practice
        return _contentRepo.GetDescendents(ContentReference.RootPage)
            .Select(r => _contentRepo.TryGet<T>(r, out var c) ? c : null)
            .Count(c => c != null);
    }

    private int CountByType(int typeId)
    {
        // Implementation using Find or direct DB query
        return 0;
    }
}

// View: Views/MyAddon/Analyzer.cshtml
@model ContentStats
@{
    Layout = null; // Use CMS frame
}
<div class=""epi-padding"">
    <h1>Content Analyzer</h1>

    <div class=""epi-card"">
        <h2>Content Statistics</h2>
        <table class=""epi-table"">
            <tr><td>Total Pages</td><td>@Model.TotalPages</td></tr>
            <tr><td>Total Blocks</td><td>@Model.TotalBlocks</td></tr>
            <tr><td>Total Media</td><td>@Model.TotalMedia</td></tr>
        </table>
    </div>
</div>",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ad-packaging",
                    ModuleId = "addon-development",
                    Title = "Packaging & Distribution",
                    Summary = "Package and distribute your add-on via NuGet.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure NuGet package metadata",
                        "Handle versioning correctly",
                        "Publish to NuGet or private feeds"
                    },
                    Content = @"
<h2>Packaging & Distribution</h2>
<p>Distribute your add-on as a NuGet package for easy installation and updates.</p>

<h3>Package Metadata</h3>
<p>Set metadata in the project file:</p>
<ul>
    <li><code>PackageId</code> - Unique package identifier</li>
    <li><code>Version</code> - SemVer version number</li>
    <li><code>Authors</code> - Package authors</li>
    <li><code>Description</code> - What the package does</li>
    <li><code>PackageTags</code> - Searchable tags</li>
</ul>

<h3>Versioning Strategy</h3>
<ul>
    <li><strong>Major</strong> - Breaking changes</li>
    <li><strong>Minor</strong> - New features, backward compatible</li>
    <li><strong>Patch</strong> - Bug fixes only</li>
</ul>

<h3>Distribution Options</h3>
<ul>
    <li><strong>nuget.org</strong> - Public packages</li>
    <li><strong>Azure Artifacts</strong> - Private feeds</li>
    <li><strong>GitHub Packages</strong> - Repository-linked feeds</li>
    <li><strong>Self-hosted</strong> - BaGet or NuGet Server</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "nuget-packaging",
                            Title = "NuGet Package Configuration",
                            Description = "Complete package configuration and build",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"<!-- MyAddon.csproj -->
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>

    <!-- Package metadata -->
    <PackageId>MyCompany.Optimizely.MyAddon</PackageId>
    <Version>1.2.0</Version>
    <Authors>My Company</Authors>
    <Company>My Company Inc.</Company>
    <Description>A useful add-on for Optimizely CMS 12</Description>
    <PackageTags>optimizely;cms;episerver;addon</PackageTags>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <PackageProjectUrl>https://github.com/mycompany/myaddon</PackageProjectUrl>
    <RepositoryUrl>https://github.com/mycompany/myaddon.git</RepositoryUrl>
    <RepositoryType>git</RepositoryType>

    <!-- Package settings -->
    <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
    <IncludeSymbols>true</IncludeSymbols>
    <SymbolPackageFormat>snupkg</SymbolPackageFormat>

    <!-- Documentation -->
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <PackageReadmeFile>README.md</PackageReadmeFile>
  </PropertyGroup>

  <ItemGroup>
    <None Include=""README.md"" Pack=""true"" PackagePath="""" />
    <None Include=""CHANGELOG.md"" Pack=""true"" PackagePath="""" />
  </ItemGroup>

  <ItemGroup>
    <!-- Reference CMS but allow consumer to provide version -->
    <PackageReference Include=""EPiServer.CMS.Core"" Version=""[12.0.0,13.0.0)"" />
  </ItemGroup>
</Project>

<!-- Build and pack -->
dotnet build -c Release
dotnet pack -c Release --no-build -o ./artifacts

<!-- Publish to nuget.org -->
dotnet nuget push ./artifacts/*.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json

<!-- Publish to private feed -->
dotnet nuget push ./artifacts/*.nupkg --source https://pkgs.dev.azure.com/myorg/_packaging/myfeed/nuget/v3/index.json --api-key az

<!-- README.md -->
# MyAddon for Optimizely CMS 12

## Installation
```
dotnet add package MyCompany.Optimizely.MyAddon
```

## Configuration
```csharp
builder.Services.AddMyAddon(options =>
{
    options.EnableFeatureX = true;
});
```",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 15: Testing & Quality Assurance

    private LearningModule BuildTestingQAModule()
    {
        return new LearningModule
        {
            Id = "testing-qa",
            Title = "Testing & Quality Assurance",
            Description = "Write effective tests for CMS applications.",
            Icon = "beaker",
            Order = 15,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "content-types", "templates-rendering" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "tq-unit-testing",
                    ModuleId = "testing-qa",
                    Title = "Unit Testing CMS Code",
                    Summary = "Mock CMS services for isolated unit tests.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Mock IContentLoader and IContentRepository",
                        "Test services that depend on CMS",
                        "Use dependency injection for testability"
                    },
                    Content = @"
<h2>Unit Testing CMS Code</h2>
<p>Unit tests should be fast, isolated, and not require a database. Mock CMS services to achieve this.</p>

<h3>Key Services to Mock</h3>
<ul>
    <li><code>IContentLoader</code> - Content retrieval</li>
    <li><code>IContentRepository</code> - Content CRUD</li>
    <li><code>IUrlResolver</code> - URL generation</li>
    <li><code>IContentTypeRepository</code> - Type metadata</li>
</ul>

<h3>Mocking Frameworks</h3>
<ul>
    <li><strong>Moq</strong> - Popular and well-documented</li>
    <li><strong>NSubstitute</strong> - Fluent syntax</li>
    <li><strong>FakeItEasy</strong> - Simple API</li>
</ul>

<h3>Testing Principles</h3>
<ul>
    <li>Test behavior, not implementation</li>
    <li>One assertion per test (when practical)</li>
    <li>Use descriptive test names</li>
    <li>Arrange-Act-Assert pattern</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "unit-test-example",
                            Title = "Unit Testing with Mocks",
                            Description = "Testing a service that depends on IContentLoader",
                            Type = ExampleType.Code,
                            ExampleContent = @"using Moq;
using Xunit;
using EPiServer;
using EPiServer.Core;

// Service under test
public class ArticleService
{
    private readonly IContentLoader _contentLoader;

    public ArticleService(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    public IEnumerable<ArticlePage> GetFeaturedArticles(
        ContentReference containerRef, int count)
    {
        return _contentLoader.GetChildren<ArticlePage>(containerRef)
            .Where(a => a.IsFeatured)
            .OrderByDescending(a => a.PublishDate)
            .Take(count);
    }
}

// Unit tests
public class ArticleServiceTests
{
    private readonly Mock<IContentLoader> _contentLoaderMock;
    private readonly ArticleService _sut;

    public ArticleServiceTests()
    {
        _contentLoaderMock = new Mock<IContentLoader>();
        _sut = new ArticleService(_contentLoaderMock.Object);
    }

    [Fact]
    public void GetFeaturedArticles_ReturnsFeaturedOnly()
    {
        // Arrange
        var containerRef = new ContentReference(100);
        var articles = new List<ArticlePage>
        {
            CreateArticle(1, ""Featured 1"", true),
            CreateArticle(2, ""Not Featured"", false),
            CreateArticle(3, ""Featured 2"", true)
        };

        _contentLoaderMock
            .Setup(x => x.GetChildren<ArticlePage>(
                containerRef, It.IsAny<LanguageSelector>()))
            .Returns(articles);

        // Act
        var result = _sut.GetFeaturedArticles(containerRef, 10).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, a => Assert.True(a.IsFeatured));
    }

    [Fact]
    public void GetFeaturedArticles_RespectsCount()
    {
        // Arrange
        var containerRef = new ContentReference(100);
        var articles = Enumerable.Range(1, 10)
            .Select(i => CreateArticle(i, $""Article {i}"", true))
            .ToList();

        _contentLoaderMock
            .Setup(x => x.GetChildren<ArticlePage>(
                containerRef, It.IsAny<LanguageSelector>()))
            .Returns(articles);

        // Act
        var result = _sut.GetFeaturedArticles(containerRef, 3);

        // Assert
        Assert.Equal(3, result.Count());
    }

    private ArticlePage CreateArticle(int id, string name, bool featured)
    {
        var article = new Mock<ArticlePage>();
        article.Setup(x => x.ContentLink).Returns(new ContentReference(id));
        article.Setup(x => x.Name).Returns(name);
        article.Setup(x => x.IsFeatured).Returns(featured);
        article.Setup(x => x.PublishDate).Returns(DateTime.Now.AddDays(-id));
        return article.Object;
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tq-integration-testing",
                    ModuleId = "testing-qa",
                    Title = "Integration Testing",
                    Summary = "Test with real CMS services using in-memory databases.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Set up integration test fixtures",
                        "Use in-memory or test databases",
                        "Test content creation and queries"
                    },
                    Content = @"
<h2>Integration Testing</h2>
<p>Integration tests verify that components work together correctly. They use real services but isolated databases.</p>

<h3>Test Fixtures</h3>
<p>Use <code>WebApplicationFactory</code> to create a test server:</p>
<ul>
    <li>Full DI container setup</li>
    <li>Real CMS services</li>
    <li>Test database</li>
</ul>

<h3>Database Options</h3>
<ul>
    <li><strong>SQL Server LocalDB</strong> - Closest to production</li>
    <li><strong>SQL Server container</strong> - CI/CD friendly</li>
    <li><strong>Separate test database</strong> - Isolated data</li>
</ul>

<h3>Test Data Management</h3>
<ul>
    <li>Create test content in setup</li>
    <li>Clean up after tests</li>
    <li>Use transactions for isolation</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "integration-test-example",
                            Title = "Integration Test Setup",
                            Description = "Setting up integration tests with WebApplicationFactory",
                            Type = ExampleType.Code,
                            ExampleContent = @"using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using Xunit;

// Custom factory for tests
public class CmsWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(""Testing"");

        builder.ConfigureServices(services =>
        {
            // Use test database
            // Configure test-specific services
        });
    }
}

// Fixture for sharing factory across tests
public class CmsIntegrationFixture : IDisposable
{
    public CmsWebApplicationFactory Factory { get; }
    public IServiceProvider Services => Factory.Services;

    public CmsIntegrationFixture()
    {
        Factory = new CmsWebApplicationFactory();
    }

    public T GetService<T>() where T : notnull
        => Services.GetRequiredService<T>();

    public void Dispose() => Factory.Dispose();
}

// Integration tests
public class ContentIntegrationTests : IClassFixture<CmsIntegrationFixture>
{
    private readonly CmsIntegrationFixture _fixture;
    private readonly IContentRepository _contentRepo;
    private readonly IContentLoader _contentLoader;

    public ContentIntegrationTests(CmsIntegrationFixture fixture)
    {
        _fixture = fixture;
        _contentRepo = fixture.GetService<IContentRepository>();
        _contentLoader = fixture.GetService<IContentLoader>();
    }

    [Fact]
    public void CanCreateAndLoadContent()
    {
        // Arrange
        var page = _contentRepo.GetDefault<ArticlePage>(
            ContentReference.StartPage);
        page.Name = ""Integration Test Article"";
        page.Title = ""Test Title"";

        // Act
        var savedRef = _contentRepo.Save(page,
            SaveAction.Publish | SaveAction.SkipValidation,
            AccessLevel.NoAccess);

        var loaded = _contentLoader.Get<ArticlePage>(savedRef);

        // Assert
        Assert.NotNull(loaded);
        Assert.Equal(""Test Title"", loaded.Title);

        // Cleanup
        _contentRepo.Delete(savedRef, true, AccessLevel.NoAccess);
    }

    [Fact]
    public async Task ApiEndpointReturnsContent()
    {
        // Arrange
        var client = _fixture.Factory.CreateClient();

        // Act
        var response = await client.GetAsync(""/api/content"");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tq-content-types",
                    ModuleId = "testing-qa",
                    Title = "Testing Content Types",
                    Summary = "Verify content type definitions and validations.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Test content type property configurations",
                        "Verify validation rules",
                        "Test computed properties and methods"
                    },
                    Content = @"
<h2>Testing Content Types</h2>
<p>Content types are the foundation of your CMS. Ensure they're configured correctly with targeted tests.</p>

<h3>What to Test</h3>
<ul>
    <li>Required properties are marked correctly</li>
    <li>Validation attributes work as expected</li>
    <li>Computed properties return correct values</li>
    <li>Default values are set properly</li>
</ul>

<h3>Testing Approaches</h3>
<ul>
    <li>Reflection-based attribute verification</li>
    <li>Instance creation and validation</li>
    <li>Property getter/setter behavior</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "content-type-tests",
                            Title = "Content Type Tests",
                            Description = "Testing content type configurations",
                            Type = ExampleType.Code,
                            ExampleContent = @"using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;

public class ArticlePageTests
{
    [Fact]
    public void Title_IsRequired()
    {
        // Arrange
        var property = typeof(ArticlePage).GetProperty(""Title"");

        // Act
        var required = property?.GetCustomAttribute<RequiredAttribute>();

        // Assert
        Assert.NotNull(required);
    }

    [Fact]
    public void Summary_HasMaxLength()
    {
        // Arrange
        var property = typeof(ArticlePage).GetProperty(""Summary"");

        // Act
        var maxLength = property?.GetCustomAttribute<StringLengthAttribute>();

        // Assert
        Assert.NotNull(maxLength);
        Assert.Equal(500, maxLength.MaximumLength);
    }

    [Fact]
    public void ContentType_HasCorrectGuid()
    {
        // Arrange
        var type = typeof(ArticlePage);

        // Act
        var contentType = type.GetCustomAttribute<ContentTypeAttribute>();

        // Assert
        Assert.NotNull(contentType);
        Assert.NotEqual(Guid.Empty.ToString(), contentType.GUID);
    }

    [Fact]
    public void ComputedProperty_CalculatesCorrectly()
    {
        // Arrange
        var article = new ArticlePage
        {
            Title = ""Test Article"",
            Author = ""John Doe""
        };

        // Act
        var byline = article.FormattedByline;

        // Assert
        Assert.Equal(""By John Doe"", byline);
    }

    [Theory]
    [InlineData("""", false)]
    [InlineData(""Valid Title"", true)]
    public void Validation_WorksCorrectly(string title, bool isValid)
    {
        // Arrange
        var article = new ArticlePage { Title = title };
        var context = new ValidationContext(article);
        var results = new List<ValidationResult>();

        // Act
        var valid = Validator.TryValidateObject(
            article, context, results, true);

        // Assert
        Assert.Equal(isValid, valid || results.All(r =>
            !r.MemberNames.Contains(""Title"")));
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tq-performance",
                    ModuleId = "testing-qa",
                    Title = "Performance Testing",
                    Summary = "Measure and optimize CMS application performance.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Implement performance benchmarks",
                        "Use load testing tools",
                        "Profile and identify bottlenecks"
                    },
                    Content = @"
<h2>Performance Testing</h2>
<p>Performance issues can impact user experience and SEO. Regular testing helps catch problems early.</p>

<h3>Types of Performance Tests</h3>
<ul>
    <li><strong>Benchmarks</strong> - Measure specific operations</li>
    <li><strong>Load tests</strong> - Simulate concurrent users</li>
    <li><strong>Stress tests</strong> - Find breaking points</li>
    <li><strong>Soak tests</strong> - Long-running stability tests</li>
</ul>

<h3>Tools</h3>
<ul>
    <li><strong>BenchmarkDotNet</strong> - Microbenchmarks</li>
    <li><strong>k6</strong> - Load testing</li>
    <li><strong>Apache JMeter</strong> - Comprehensive testing</li>
    <li><strong>dotnet-trace</strong> - Profiling</li>
</ul>

<h3>Key Metrics</h3>
<ul>
    <li>Response time (p50, p95, p99)</li>
    <li>Throughput (requests/second)</li>
    <li>Error rate</li>
    <li>Resource utilization</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "benchmark-example",
                            Title = "BenchmarkDotNet Setup",
                            Description = "Benchmark content loading operations",
                            Type = ExampleType.Code,
                            ExampleContent = @"using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EPiServer;
using EPiServer.Core;

// Run with: dotnet run -c Release
public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ContentLoaderBenchmarks>();
    }
}

[MemoryDiagnoser]
public class ContentLoaderBenchmarks
{
    private IContentLoader _contentLoader;
    private ContentReference _rootRef;
    private List<ContentReference> _references;

    [GlobalSetup]
    public void Setup()
    {
        // Initialize services and test data
        // _contentLoader = ...
        // _rootRef = ContentReference.StartPage;
        // _references = ... (list of content references)
    }

    [Benchmark(Baseline = true)]
    public void GetSingleContent()
    {
        _ = _contentLoader.Get<IContent>(_rootRef);
    }

    [Benchmark]
    public void GetChildren()
    {
        _ = _contentLoader.GetChildren<IContent>(_rootRef).ToList();
    }

    [Benchmark]
    public void GetItems_Sequential()
    {
        foreach (var reference in _references)
        {
            _ = _contentLoader.Get<IContent>(reference);
        }
    }

    [Benchmark]
    public void GetItems_Batch()
    {
        _ = _contentLoader.GetItems(_references, LanguageSelector.AutoDetect()).ToList();
    }
}

// k6 load test script (JavaScript)
/*
import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 50,           // 50 virtual users
  duration: '5m',    // 5 minute test
  thresholds: {
    http_req_duration: ['p(95)<500'],  // 95% under 500ms
    http_req_failed: ['rate<0.01'],    // <1% errors
  },
};

export default function() {
  const res = http.get('https://mysite.com/');

  check(res, {
    'status is 200': (r) => r.status === 200,
    'response time OK': (r) => r.timings.duration < 500,
  });

  sleep(1);
}
*/",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 16: CMS 11 to CMS 12 Migration

    private LearningModule BuildCms11ToCms12MigrationModule()
    {
        return new LearningModule
        {
            Id = "cms11-to-cms12-migration",
            Title = "CMS 11 to CMS 12 Migration",
            Description = "Comprehensive guide to migrating from Optimizely CMS 11 (.NET Framework) to CMS 12 (.NET Core/8+).",
            Icon = "arrow-up-circle",
            Order = 17,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "m12-overview-planning",
                    ModuleId = "cms11-to-cms12-migration",
                    Title = "Migration Overview & Planning",
                    Summary = "Understand the scope of CMS 11 to 12 migration and plan your upgrade strategy.",
                    Order = 1,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand why you should migrate to CMS 12",
                        "Evaluate different migration strategies",
                        "Plan and estimate your migration project"
                    },
                    Content = @"
<h2>Migration Overview & Planning</h2>
<p>Migrating from CMS 11 to CMS 12 is a significant undertaking that involves moving from .NET Framework 4.x to .NET Core/.NET 8+. This lesson helps you understand the scope and plan effectively.</p>

<h3>Why Migrate to CMS 12?</h3>
<p>CMS 12 offers substantial benefits that justify the migration effort:</p>
<ul>
    <li><strong>Performance improvements</strong> - Up to 1,200% faster in certain areas</li>
    <li><strong>Cross-platform support</strong> - Run on Windows, Linux, or containers</li>
    <li><strong>Modern development experience</strong> - Latest C# features and tooling</li>
    <li><strong>Cloud-native architecture</strong> - Better suited for DXP and Kubernetes</li>
    <li><strong>Long-term support</strong> - CMS 11 is in maintenance mode</li>
    <li><strong>Improved security</strong> - Benefits from .NET Core security updates</li>
</ul>

<h3>Migration Strategies</h3>
<p>There are three primary approaches to migration:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Strategy</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Upgrade Assistant</td>
            <td class=""px-4 py-2"">Use Microsoft's tool with Optimizely extensions to automatically convert projects</td>
            <td class=""px-4 py-2"">Simple projects with few customizations</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Vanilla Install + Lift & Shift</td>
            <td class=""px-4 py-2"">Start with a clean CMS 12 project and transfer code gradually</td>
            <td class=""px-4 py-2"">Most projects (recommended)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Complete Rebuild</td>
            <td class=""px-4 py-2"">Redesign the entire solution taking advantage of CMS 12 features</td>
            <td class=""px-4 py-2"">Legacy sites needing redesign anyway</td>
        </tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Community Recommendation</p>
    <p>Most experienced developers recommend the ""Vanilla Install + Lift & Shift"" approach. It's cleaner than the upgrade assistant and gives you better control over the migration process.</p>
</div>

<h3>Key Considerations Before Starting</h3>
<ol>
    <li><strong>Third-party package compatibility</strong> - Check all NuGet packages for .NET Core versions</li>
    <li><strong>Custom code assessment</strong> - Identify .NET Framework-specific code that needs rewriting</li>
    <li><strong>Add-on compatibility</strong> - Verify Optimizely add-ons have CMS 12 versions</li>
    <li><strong>Integration updates</strong> - Third-party integrations may need reconfiguration</li>
    <li><strong>Testing requirements</strong> - Plan for comprehensive QA</li>
    <li><strong>Training needs</strong> - The editing interface has some differences</li>
</ol>

<h3>Timeline Guidance</h3>
<p>Optimizely recommends completing migration within one year of starting. Key phases:</p>
<ul>
    <li><strong>Assessment</strong> - 1-2 weeks (audit current solution)</li>
    <li><strong>Development</strong> - 4-12 weeks (depending on complexity)</li>
    <li><strong>Testing</strong> - 2-4 weeks (thorough QA)</li>
    <li><strong>Deployment</strong> - 1-2 weeks (staged rollout)</li>
</ul>

<h3>What Stays the Same</h3>
<p>Good news - many things don't change:</p>
<ul>
    <li>Database schema (no data migration required)</li>
    <li>Content types and properties</li>
    <li>Most Optimizely APIs</li>
    <li>Content and asset data</li>
    <li>User and role definitions</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "m12-upgrade-assistant",
                    ModuleId = "cms11-to-cms12-migration",
                    Title = "Using the Upgrade Assistant",
                    Summary = "Learn to use Microsoft's Upgrade Assistant with Optimizely extensions.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Install and configure the Upgrade Assistant",
                        "Run the tool on your CMS 11 solution",
                        "Understand what the tool can and cannot do"
                    },
                    Content = @"
<h2>Using the Upgrade Assistant</h2>
<p>The Upgrade Assistant is a Microsoft dotnet tool extended by Optimizely with CMS-specific rules. It automates much of the conversion process.</p>

<h3>Installing the Upgrade Assistant</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code># Install the upgrade assistant globally
dotnet tool install -g upgrade-assistant

# Verify installation
upgrade-assistant --version</code></pre>

<h3>Before Running the Tool</h3>
<p>Prepare your solution:</p>
<ol>
    <li>Commit all changes to source control</li>
    <li>Create a backup of your solution</li>
    <li>Close Visual Studio</li>
    <li>Ensure all NuGet packages are restored</li>
    <li>Review and fix any existing compiler warnings</li>
</ol>

<h3>Running the Upgrade Assistant</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code># Navigate to your solution directory
cd MySolution

# Run on a specific project
upgrade-assistant upgrade MySolution.Web.csproj

# Or run on the entire solution
upgrade-assistant upgrade MySolution.sln</code></pre>

<h3>Interactive vs Non-Interactive Mode</h3>
<p>The tool runs interactively by default, asking for confirmation at each step:</p>
<ul>
    <li><strong>Interactive</strong> - Review each change before applying</li>
    <li><strong>Non-interactive</strong> - Use <code>--non-interactive</code> for automated scenarios</li>
</ul>

<h3>What the Tool Does</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Task</th>
            <th class=""px-4 py-2 text-left"">Automated?</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Update project file format (SDK-style)</td><td class=""px-4 py-2"">✅ Yes</td></tr>
        <tr><td class=""px-4 py-2"">Update NuGet package references</td><td class=""px-4 py-2"">✅ Yes</td></tr>
        <tr><td class=""px-4 py-2"">Replace deprecated APIs (basic)</td><td class=""px-4 py-2"">✅ Yes</td></tr>
        <tr><td class=""px-4 py-2"">Update namespace references</td><td class=""px-4 py-2"">✅ Yes</td></tr>
        <tr><td class=""px-4 py-2"">Create Program.cs/Startup.cs</td><td class=""px-4 py-2"">⚠️ Partial</td></tr>
        <tr><td class=""px-4 py-2"">Complex code refactoring</td><td class=""px-4 py-2"">❌ No</td></tr>
        <tr><td class=""px-4 py-2"">Third-party package updates</td><td class=""px-4 py-2"">❌ No</td></tr>
        <tr><td class=""px-4 py-2"">Configuration migration</td><td class=""px-4 py-2"">⚠️ Partial</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">⚠️ Important</p>
    <p>The Upgrade Assistant is a starting point, not a complete solution. Expect significant manual work after running the tool, especially for custom code and third-party integrations.</p>
</div>

<h3>Common Issues After Running</h3>
<ul>
    <li>Missing namespace references</li>
    <li>Incompatible NuGet packages</li>
    <li>Build errors in Razor views</li>
    <li>Configuration not migrated properly</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "upgrade-assistant-workflow",
                            Title = "Upgrade Assistant Workflow",
                            Description = "Step-by-step upgrade process",
                            Type = ExampleType.Code,
                            ExampleContent = @"# Step 1: Install the upgrade assistant
dotnet tool install -g upgrade-assistant

# Step 2: Navigate to your solution
cd C:\Projects\MySolution

# Step 3: Create a backup branch
git checkout -b upgrade-to-cms12
git add -A
git commit -m ""Pre-upgrade snapshot""

# Step 4: Run the upgrade assistant
upgrade-assistant upgrade MySolution.Web.csproj

# The tool will guide you through these steps:
# 1. Back up project
# 2. Convert project file to SDK style
# 3. Update TFM (Target Framework Moniker)
# 4. Update NuGet packages
# 5. Add template files (Program.cs, etc.)
# 6. Update source code
# 7. Move to next project

# Step 5: Review changes
git diff

# Step 6: Attempt to build
dotnet build

# Step 7: Fix remaining errors manually
# (This is where most of the work happens)

# Step 8: Run tests
dotnet test

# Step 9: Commit successful migration
git add -A
git commit -m ""Upgraded to CMS 12 / .NET 8""",
                            IsInteractive = false,
                            Hints = new List<string>
                            {
                                "Always backup before running the tool",
                                "Run on one project at a time for better control",
                                "Review each change before committing"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "m12-breaking-changes",
                    ModuleId = "cms11-to-cms12-migration",
                    Title = "Breaking Changes & Package Updates",
                    Summary = "Navigate breaking changes and update NuGet packages for CMS 12.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Identify CMS 11 packages that need replacement",
                        "Understand API changes between versions",
                        "Handle removed features and alternatives"
                    },
                    Content = @"
<h2>Breaking Changes & Package Updates</h2>
<p>CMS 12 introduces significant breaking changes as it moves from .NET Framework to .NET Core. Understanding these changes is critical for a successful migration.</p>

<h3>NuGet Package Mapping</h3>
<p>Several CMS 11 packages have been replaced or split in CMS 12:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">CMS 11 Package</th>
            <th class=""px-4 py-2 text-left"">CMS 12 Replacement</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-mono text-sm"">EPiServer.Cms.AspNet</td>
            <td class=""px-4 py-2 font-mono text-sm"">EPiServer.CMS.AspNetCore<br/>EPiServer.CMS.AspNetCore.Templating<br/>EPiServer.CMS.AspNetCore.Routing<br/>EPiServer.CMS.AspNetCore.Mvc<br/>EPiServer.CMS.AspNetCore.HtmlHelpers</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono text-sm"">EPiServer.ServiceLocation.StructureMap</td>
            <td class=""px-4 py-2 font-mono text-sm"">Remove (use built-in DI)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-mono text-sm"">EPiServer.Framework.AspNet</td>
            <td class=""px-4 py-2 font-mono text-sm"">EPiServer.Framework.AspNetCore</td>
        </tr>
    </tbody>
</table>

<h3>Removed Technologies</h3>
<ul>
    <li><strong>WebForms</strong> - Not supported; must convert to MVC/Razor Pages</li>
    <li><strong>WCF Event Provider</strong> - Use Azure Service Bus instead</li>
    <li><strong>StructureMap/Custom DI</strong> - Use built-in ASP.NET Core DI</li>
    <li><strong>System.Web dependencies</strong> - Replace with Microsoft.AspNetCore equivalents</li>
</ul>

<h3>API Changes</h3>

<h4>IHttpActionResult → IActionResult</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// CMS 11
public IHttpActionResult GetData()
{
    return InternalServerError(exception);
}

// CMS 12
public IActionResult GetData()
{
    return StatusCode((int)HttpStatusCode.InternalServerError, error);
}</code></pre>

<h4>HttpContext.Current → IHttpContextAccessor</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// CMS 11
var user = HttpContext.Current.User;

// CMS 12
public class MyService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MyService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void DoSomething()
    {
        var user = _httpContextAccessor.HttpContext?.User;
    }
}</code></pre>

<h4>Json() Method Changes</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// CMS 11
return Json(data, JsonRequestBehavior.AllowGet);

// CMS 12
return new ContentResult
{
    Content = JsonSerializer.Serialize(data),
    ContentType = ""application/json""
};</code></pre>

<h3>Deprecated Packages to Remove</h3>
<p>Common packages that don't support CMS 12:</p>
<ul>
    <li><code>TedGustaf.Episerver.GoogleMapsEditor 1.x</code></li>
    <li><code>EPiServer.ServiceLocation.StructureMap 2.x</code></li>
    <li><code>Semantix.AutoConnect</code></li>
    <li>Any package targeting .NET Framework 4.x only</li>
</ul>

<div class=""bg-red-50 dark:bg-red-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">❌ Common Conflict</p>
    <p>You cannot have both <code>EPiServer.Cms.AspNet</code> and <code>EPiServer.Cms.AspNetCore</code> in the same project. Remove the old package completely before adding the new one.</p>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "package-migration-script",
                            Title = "Package Migration Reference",
                            Description = "Common package replacements",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"<!-- CMS 11 packages to REMOVE -->
<PackageReference Include=""EPiServer.Cms.AspNet"" />
<PackageReference Include=""EPiServer.Framework.AspNet"" />
<PackageReference Include=""EPiServer.ServiceLocation.StructureMap"" />
<PackageReference Include=""System.Web.Http"" />
<PackageReference Include=""Microsoft.AspNet.WebApi"" />

<!-- CMS 12 packages to ADD -->
<PackageReference Include=""EPiServer.CMS.AspNetCore"" Version=""12.*"" />
<PackageReference Include=""EPiServer.CMS.AspNetCore.Templating"" Version=""12.*"" />
<PackageReference Include=""EPiServer.CMS.AspNetCore.Routing"" Version=""12.*"" />
<PackageReference Include=""EPiServer.CMS.AspNetCore.Mvc"" Version=""12.*"" />
<PackageReference Include=""EPiServer.CMS.AspNetCore.HtmlHelpers"" Version=""12.*"" />
<PackageReference Include=""EPiServer.CMS.UI"" Version=""12.*"" />
<PackageReference Include=""EPiServer.CMS.UI.Core"" Version=""12.*"" />
<PackageReference Include=""EPiServer.CMS.TinyMce"" Version=""5.*"" />

<!-- Common replacements -->
<!-- System.Web.Mvc → Microsoft.AspNetCore.Mvc -->
<!-- System.Web.Http → Microsoft.AspNetCore.Mvc -->
<!-- Newtonsoft.Json → System.Text.Json (or keep Newtonsoft) -->

<!-- For legacy config file support (temporary) -->
<PackageReference Include=""EPiServer.CMS.AspNetCore.Migration"" Version=""1.*"" />

<!-- Note: Migration package only works with .NET 5
     For .NET 6+, manually migrate config files -->",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "m12-configuration-startup",
                    ModuleId = "cms11-to-cms12-migration",
                    Title = "Configuration & Startup Migration",
                    Summary = "Migrate from web.config/Global.asax to appsettings.json/Program.cs.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Convert web.config to appsettings.json",
                        "Create Program.cs and Startup.cs",
                        "Register CMS services correctly"
                    },
                    Content = @"
<h2>Configuration & Startup Migration</h2>
<p>One of the biggest changes in CMS 12 is how the application starts up and how configuration is handled.</p>

<h3>Configuration File Changes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">CMS 11</th>
            <th class=""px-4 py-2 text-left"">CMS 12</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">web.config</td><td class=""px-4 py-2 font-mono text-sm"">appsettings.json</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">connectionStrings.config</td><td class=""px-4 py-2 font-mono text-sm"">appsettings.json (ConnectionStrings section)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">episerverFramework.config</td><td class=""px-4 py-2 font-mono text-sm"">appsettings.json (EPiServer section)</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Global.asax.cs</td><td class=""px-4 py-2 font-mono text-sm"">Program.cs / Startup.cs</td></tr>
    </tbody>
</table>

<h3>Connection String Migration</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>&lt;!-- CMS 11: connectionStrings.config --&gt;
&lt;connectionStrings&gt;
  &lt;add name=""EPiServerDB""
       connectionString=""Server=.;Database=MyDb;Trusted_Connection=True"" /&gt;
&lt;/connectionStrings&gt;

// CMS 12: appsettings.json
{
  ""ConnectionStrings"": {
    ""EPiServerDB"": ""Server=.;Database=MyDb;Trusted_Connection=True""
  }
}</code></pre>

<h3>EPiServer Settings Migration</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>&lt;!-- CMS 11: episerver.config --&gt;
&lt;episerver&gt;
  &lt;applicationSettings globalErrorHandling=""Off"" /&gt;
&lt;/episerver&gt;

// CMS 12: appsettings.json
{
  ""EPiServer"": {
    ""Cms"": {
      ""MappedRoles"": {
        ""CmsAdmins"": ""WebAdmins, Administrators"",
        ""CmsEditors"": ""WebEditors""
      }
    }
  }
}</code></pre>

<h3>Global.asax → Program.cs</h3>
<p>The application entry point moves from Global.asax to Program.cs:</p>

<h4>CMS 11 Structure:</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// Global.asax.cs
public class Global : EPiServer.Global
{
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        // Custom startup code
    }
}</code></pre>

<h4>CMS 12 Structure:</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCms();
builder.Services.AddFind();
// Custom services

var app = builder.Build();

// Configure pipeline
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapContent();

app.Run();</code></pre>

<h3>Temporary Migration Support</h3>
<p>For .NET 5, you can use the migration package to read old config files temporarily:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// Only for .NET 5 - migrate to appsettings.json for .NET 6+
builder.Services.AddEpiserverCmsConfiguration();</code></pre>

<div class=""bg-yellow-50 dark:bg-yellow-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">⚠️ Important</p>
    <p>The <code>EPiServer.CMS.AspNetCore.Migration</code> package only works with .NET 5. For .NET 6+, you must manually migrate all XML configuration to appsettings.json.</p>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "complete-startup-migration",
                            Title = "Complete Startup Migration",
                            Description = "Full Program.cs and appsettings.json for CMS 12",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Program.cs - Complete CMS 12 setup
using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Scheduler;
using EPiServer.Web.Routing;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.AddConsole();

// Add CMS services
builder.Services
    .AddCmsAspNetIdentity<ApplicationUser>()
    .AddCms()
    .AddAdminUserRegistration()
    .AddEmbeddedLocalization<Startup>();

// Add Find (if using)
builder.Services.AddFind();

// Add custom services
builder.Services.AddScoped<IMyService, MyService>();

// Configure options
builder.Services.Configure<SchedulerOptions>(options =>
{
    options.Enabled = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(""/Error"");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapContent();
app.MapControllers();
app.MapRazorPages();

app.Run();

// ========================================
// appsettings.json
{
  ""Logging"": {
    ""LogLevel"": {
      ""Default"": ""Warning"",
      ""EPiServer"": ""Warning""
    }
  },
  ""AllowedHosts"": ""*"",
  ""ConnectionStrings"": {
    ""EPiServerDB"": ""Server=(localdb)\\mssqllocaldb;Database=MyCmsDb;Trusted_Connection=True;MultipleActiveResultSets=True""
  },
  ""EPiServer"": {
    ""Cms"": {
      ""MappedRoles"": {
        ""CmsAdmins"": ""WebAdmins, Administrators"",
        ""CmsEditors"": ""WebEditors""
      }
    },
    ""Find"": {
      ""ServiceUrl"": ""https://demo01.find.episerver.net/xxx"",
      ""DefaultIndex"": ""my_index""
    }
  }
}",
                            IsInteractive = false,
                            Hints = new List<string>
                            {
                                "Order of middleware matters - Authentication before Authorization",
                                "MapContent() replaces the old route configuration",
                                "Use appsettings.Development.json for local settings"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "m12-dependency-injection",
                    ModuleId = "cms11-to-cms12-migration",
                    Title = "Dependency Injection Refactoring",
                    Summary = "Migrate from ServiceLocator to constructor injection.",
                    Order = 5,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Replace ServiceLocator patterns with constructor injection",
                        "Register custom services correctly",
                        "Handle DI in views and page types"
                    },
                    Content = @"
<h2>Dependency Injection Refactoring</h2>
<p>CMS 12 uses ASP.NET Core's built-in dependency injection. The old ServiceLocator pattern should be replaced with constructor injection.</p>

<h3>The Core Change</h3>
<p>In CMS 11, you could use <code>ServiceLocator.Current.GetInstance&lt;T&gt;()</code> anywhere. In CMS 12, services should be injected through constructors.</p>

<h3>Service Lifetime</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Lifetime</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Use For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Transient</td><td class=""px-4 py-2"">New instance every time</td><td class=""px-4 py-2"">Lightweight, stateless services</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Scoped</td><td class=""px-4 py-2"">One instance per request</td><td class=""px-4 py-2"">Most CMS services, DbContext</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">Singleton</td><td class=""px-4 py-2"">One instance for app lifetime</td><td class=""px-4 py-2"">Configuration, caching</td></tr>
    </tbody>
</table>

<h3>Migrating Controllers</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// CMS 11 (ServiceLocator)
public class ArticleController : PageController&lt;ArticlePage&gt;
{
    public ActionResult Index(ArticlePage currentPage)
    {
        var loader = ServiceLocator.Current.GetInstance&lt;IContentLoader&gt;();
        var children = loader.GetChildren&lt;PageData&gt;(currentPage.ContentLink);
        return View(currentPage);
    }
}

// CMS 12 (Constructor Injection)
public class ArticleController : PageController&lt;ArticlePage&gt;
{
    private readonly IContentLoader _contentLoader;

    public ArticleController(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    public ActionResult Index(ArticlePage currentPage)
    {
        var children = _contentLoader.GetChildren&lt;PageData&gt;(currentPage.ContentLink);
        return View(currentPage);
    }
}</code></pre>

<h3>Migrating Services</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// CMS 11
public class MyService
{
    public void DoWork()
    {
        var repo = ServiceLocator.Current.GetInstance&lt;IContentRepository&gt;();
        // Use repo
    }
}

// CMS 12
public class MyService : IMyService
{
    private readonly IContentRepository _contentRepository;

    public MyService(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public void DoWork()
    {
        // Use _contentRepository
    }
}

// Register in Program.cs
builder.Services.AddScoped&lt;IMyService, MyService&gt;();</code></pre>

<h3>Injecting into Razor Views</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>@* CMS 11 *@
@{
    var loader = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance&lt;IContentLoader&gt;();
}

@* CMS 12 *@
@inject IContentLoader ContentLoader
@{
    var children = ContentLoader.GetChildren&lt;PageData&gt;(Model.ContentLink);
}</code></pre>

<h3>When ServiceLocator is Still Needed</h3>
<p>Some scenarios still require service location:</p>
<ul>
    <li>Static methods (try to avoid)</li>
    <li>Content types with computed properties</li>
    <li>Some initialization scenarios</li>
</ul>

<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// For these cases, use IServiceProvider
public class ArticlePage : PageData
{
    // Computed property needing service
    public string FullUrl
    {
        get
        {
            var urlResolver = ServiceLocator.Current.GetInstance&lt;IUrlResolver&gt;();
            return urlResolver.GetUrl(ContentLink);
        }
    }
}</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Best Practice</p>
    <p>Minimize ServiceLocator usage. Each usage is technical debt. Consider passing services as parameters or restructuring code to use proper DI.</p>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "di-migration-patterns",
                            Title = "DI Migration Patterns",
                            Description = "Common patterns for migrating to constructor injection",
                            Type = ExampleType.Code,
                            ExampleContent = @"// ========================================
// PATTERN 1: Simple Service Migration
// ========================================

// Before (CMS 11)
public class OldService
{
    public IEnumerable<PageData> GetPages()
    {
        var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
        return loader.GetChildren<PageData>(ContentReference.StartPage);
    }
}

// After (CMS 12)
public interface IPageService
{
    IEnumerable<PageData> GetPages();
}

public class PageService : IPageService
{
    private readonly IContentLoader _contentLoader;

    public PageService(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    public IEnumerable<PageData> GetPages()
    {
        return _contentLoader.GetChildren<PageData>(ContentReference.StartPage);
    }
}

// Register
services.AddScoped<IPageService, PageService>();

// ========================================
// PATTERN 2: View Component Migration
// ========================================

// CMS 12 View Component (replaces partial with ServiceLocator)
public class NavigationViewComponent : ViewComponent
{
    private readonly IContentLoader _contentLoader;

    public NavigationViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    public IViewComponentResult Invoke(ContentReference root)
    {
        var pages = _contentLoader.GetChildren<PageData>(root)
            .Where(p => p.VisibleInMenu);
        return View(pages);
    }
}

// Usage in view
@await Component.InvokeAsync(""Navigation"", new { root = Model.ContentLink })

// ========================================
// PATTERN 3: Initialization Module Migration
// ========================================

// CMS 12 Initialization with DI
[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class MyInitModule : IConfigurableModule
{
    public void ConfigureContainer(ServiceConfigurationContext context)
    {
        // Register services here
        context.Services.AddSingleton<IMyService, MyService>();
    }

    public void Initialize(InitializationEngine context)
    {
        // Get services from the container
        var myService = context.Locate.Advanced.GetInstance<IMyService>();
        myService.Initialize();
    }

    public void Uninitialize(InitializationEngine context) { }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "m12-testing-golive",
                    ModuleId = "cms11-to-cms12-migration",
                    Title = "Testing, Troubleshooting & Go-Live",
                    Summary = "Test your migration thoroughly and deploy successfully.",
                    Order = 6,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create a comprehensive test plan for migration",
                        "Troubleshoot common migration issues",
                        "Plan and execute a successful go-live"
                    },
                    Content = @"
<h2>Testing, Troubleshooting & Go-Live</h2>
<p>Thorough testing and a solid go-live plan are critical for a successful CMS 12 migration.</p>

<h3>Testing Checklist</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Category</th>
            <th class=""px-4 py-2 text-left"">Test Items</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2 font-medium"">Content Rendering</td>
            <td class=""px-4 py-2"">All page types, blocks, and templates render correctly</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Edit Mode</td>
            <td class=""px-4 py-2"">Content editing, property editors, preview mode</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Authentication</td>
            <td class=""px-4 py-2"">Login, logout, role-based access</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Search</td>
            <td class=""px-4 py-2"">Site search, Find integration (if applicable)</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Forms</td>
            <td class=""px-4 py-2"">Optimizely Forms submission and storage</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Scheduled Jobs</td>
            <td class=""px-4 py-2"">All jobs run successfully</td>
        </tr>
        <tr>
            <td class=""px-4 py-2 font-medium"">Performance</td>
            <td class=""px-4 py-2"">Page load times, database queries</td>
        </tr>
    </tbody>
</table>

<h3>Common Migration Issues & Solutions</h3>

<h4>Build Error: CA0055 (Code Analysis)</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>&lt;!-- Add to .csproj to disable code analysis during migration --&gt;
&lt;PropertyGroup&gt;
  &lt;RunCodeAnalysis&gt;false&lt;/RunCodeAnalysis&gt;
&lt;/PropertyGroup&gt;</code></pre>

<h4>Missing Module Registration</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// If modules aren't initializing, ensure they're registered:
builder.Services.AddSitemaps();
app.UseDbLocalizationProvider();</code></pre>

<h4>Razor View Compilation Errors</h4>
<p>Common fixes:</p>
<ul>
    <li>Replace <code>@Html.Raw()</code> with proper encoding</li>
    <li>Update <code>@helper</code> methods to Tag Helpers or View Components</li>
    <li>Fix namespace references in <code>_ViewImports.cshtml</code></li>
</ul>

<h4>JSON Serialization Issues</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// If using Newtonsoft.Json features
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling =
            ReferenceLoopHandling.Ignore;
    });</code></pre>

<h3>DXP Environment Migration</h3>
<p>For DXP customers, after local testing succeeds:</p>
<ol>
    <li>Access the DXP portal</li>
    <li>Use the migration tools to create a new CMS 12 environment</li>
    <li>Deploy your upgraded codebase</li>
    <li>Verify the database connection</li>
    <li>Run comprehensive tests</li>
</ol>

<h3>Go-Live Checklist</h3>
<ul>
    <li>☐ All tests passing</li>
    <li>☐ Performance acceptable or better</li>
    <li>☐ Rollback plan documented</li>
    <li>☐ Team trained on any differences</li>
    <li>☐ Monitoring in place</li>
    <li>☐ Support team briefed</li>
    <li>☐ Communication plan for users</li>
</ul>

<div class=""bg-green-50 dark:bg-green-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">✅ Success Metrics</p>
    <p>A successful migration should show improved page load times (often 2-10x faster), similar or lower error rates, and all existing functionality working correctly.</p>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "migration-troubleshooting",
                            Title = "Common Migration Fixes",
                            Description = "Solutions to frequently encountered issues",
                            Type = ExampleType.Code,
                            ExampleContent = @"// ========================================
// ISSUE 1: DataFactory Not Found
// ========================================

// CMS 11
var page = DataFactory.Instance.GetPage(pageRef);

// CMS 12 - Use IContentLoader (read) or IContentRepository (write)
public class MyService
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentRepository _contentRepository;

    public PageData GetPage(ContentReference pageRef)
    {
        return _contentLoader.Get<PageData>(pageRef);
    }

    public ContentReference SavePage(PageData page)
    {
        return _contentRepository.Save(page, SaveAction.Publish);
    }
}

// ========================================
// ISSUE 2: UrlResolver Changes
// ========================================

// CMS 11
var url = UrlResolver.Current.GetUrl(contentRef);

// CMS 12
public class MyController : Controller
{
    private readonly IUrlResolver _urlResolver;

    public MyController(IUrlResolver urlResolver)
    {
        _urlResolver = urlResolver;
    }

    public string GetUrl(ContentReference contentRef)
    {
        return _urlResolver.GetUrl(contentRef);
    }
}

// ========================================
// ISSUE 3: FilterForVisitor Not Working
// ========================================

// CMS 12 - Ensure the correct extension is used
using EPiServer.Filters;

var filteredChildren = _contentLoader
    .GetChildren<PageData>(parentRef)
    .FilterForDisplay(requirePageTemplate: true, requireVisibleInMenu: false);

// ========================================
// ISSUE 4: PropertyList<T> Migration
// ========================================

// CMS 12 requires explicit registration
builder.Services.AddPropertyListDefinition<MyCustomItem>();

// ========================================
// ISSUE 5: IContentEvents Subscription
// ========================================

// CMS 12 - Subscribe in initialization module
[InitializableModule]
public class EventSubscriptionModule : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        var events = context.Locate.Advanced.GetInstance<IContentEvents>();

        events.PublishedContent += (sender, args) =>
        {
            var logger = context.Locate.Advanced
                .GetInstance<ILogger<EventSubscriptionModule>>();
            logger.LogInformation(""Content published: {ContentLink}"",
                args.ContentLink);
        };
    }

    public void Uninitialize(InitializationEngine context) { }
}

// ========================================
// ISSUE 6: Missing _ViewImports.cshtml
// ========================================

// Create Views/_ViewImports.cshtml with:
@using EPiServer.Core
@using EPiServer.Web
@using EPiServer.Web.Mvc.Html
@using EPiServer.Web.Routing
@using Microsoft.AspNetCore.Mvc.ViewFeatures

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, EPiServer.Web.Mvc",
                            IsInteractive = false,
                            Hints = new List<string>
                            {
                                "Keep a list of all errors encountered - they help future migrations",
                                "Test in a staging environment before production",
                                "Consider a phased rollout if possible"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "mu-content-migration",
                    ModuleId = "migration-upgrades",
                    Title = "Content Migration Strategies",
                    Summary = "Move content between environments and systems.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Export and import content",
                        "Use Data Import for bulk operations",
                        "Migrate content programmatically"
                    },
                    Content = @"
<h2>Content Migration Strategies</h2>
<p>Content migration may be needed for initial go-live, environment sync, or system migrations.</p>

<h3>Migration Scenarios</h3>
<ul>
    <li>Development to staging/production</li>
    <li>Legacy CMS to Optimizely</li>
    <li>Merging sites</li>
    <li>Content restructuring</li>
</ul>

<h3>Available Tools</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Tool</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Export/Import</td><td class=""px-4 py-2"">Small content sets, manual moves</td></tr>
        <tr><td class=""px-4 py-2"">Data Import</td><td class=""px-4 py-2"">Bulk imports, initial migration</td></tr>
        <tr><td class=""px-4 py-2"">Database backup/restore</td><td class=""px-4 py-2"">Full environment copy</td></tr>
        <tr><td class=""px-4 py-2"">Custom scripts</td><td class=""px-4 py-2"">Complex transformations</td></tr>
    </tbody>
</table>

<h3>Considerations</h3>
<ul>
    <li>Content references may need remapping</li>
    <li>Media files need separate migration</li>
    <li>Versioning and history decisions</li>
    <li>Language/localization handling</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "content-migration-script",
                            Title = "Programmatic Content Migration",
                            Description = "Migrate content from external source",
                            Type = ExampleType.Code,
                            ExampleContent = @"using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

public class ContentMigrationService
{
    private readonly IContentRepository _contentRepo;
    private readonly IContentTypeRepository _typeRepo;
    private readonly IBlobFactory _blobFactory;
    private readonly ILogger<ContentMigrationService> _logger;

    public ContentMigrationService(
        IContentRepository contentRepo,
        IContentTypeRepository typeRepo,
        IBlobFactory blobFactory,
        ILogger<ContentMigrationService> logger)
    {
        _contentRepo = contentRepo;
        _typeRepo = typeRepo;
        _blobFactory = blobFactory;
        _logger = logger;
    }

    public async Task<MigrationResult> MigrateFromLegacyAsync(
        IEnumerable<LegacyContent> legacyContent,
        ContentReference parentRef)
    {
        var result = new MigrationResult();

        foreach (var legacy in legacyContent)
        {
            try
            {
                var newContent = await MigrateItemAsync(legacy, parentRef);
                result.Succeeded.Add(newContent.ContentLink);

                _logger.LogInformation(
                    ""Migrated {Title} to {ContentLink}"",
                    legacy.Title, newContent.ContentLink);
            }
            catch (Exception ex)
            {
                result.Failed.Add((legacy.Id, ex.Message));
                _logger.LogError(ex,
                    ""Failed to migrate {Id}"", legacy.Id);
            }
        }

        return result;
    }

    private async Task<IContent> MigrateItemAsync(
        LegacyContent legacy, ContentReference parentRef)
    {
        // Map legacy type to CMS content type
        var contentType = MapContentType(legacy.Type);

        // Create new content
        var newContent = _contentRepo.GetDefault<ArticlePage>(parentRef);
        newContent.Name = legacy.Title;

        // Map properties
        if (newContent is ArticlePage article)
        {
            article.Title = legacy.Title;
            article.MainBody = new XhtmlString(legacy.Body);
            article.PublishDate = legacy.PublishedDate;

            // Migrate images
            if (!string.IsNullOrEmpty(legacy.ImageUrl))
            {
                article.MainImage = await MigrateImageAsync(legacy.ImageUrl);
            }
        }

        // Save as published
        return _contentRepo.Save(newContent,
            SaveAction.Publish | SaveAction.SkipValidation,
            AccessLevel.NoAccess);
    }

    private async Task<ContentReference> MigrateImageAsync(string url)
    {
        // Download and create media
        using var http = new HttpClient();
        var bytes = await http.GetByteArrayAsync(url);

        var image = _contentRepo.GetDefault<ImageData>(
            SiteDefinition.Current.GlobalAssetsRoot);
        image.Name = Path.GetFileName(url);

        var blob = _blobFactory.CreateBlob(
            image.BinaryDataContainer, Path.GetExtension(url));
        blob.Write(new MemoryStream(bytes));
        image.BinaryData = blob;

        var saved = _contentRepo.Save(image,
            SaveAction.Publish, AccessLevel.NoAccess);

        return saved;
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "mu-database-upgrades",
                    ModuleId = "migration-upgrades",
                    Title = "Database Upgrades",
                    Summary = "Handle database schema updates and migrations.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand automatic schema updates",
                        "Handle custom database changes",
                        "Plan for zero-downtime updates"
                    },
                    Content = @"
<h2>Database Upgrades</h2>
<p>CMS automatically manages its database schema, but custom tables and data may need manual migration.</p>

<h3>Automatic Schema Updates</h3>
<p>CMS handles its own schema on startup:</p>
<ul>
    <li>Schema version tracked in database</li>
    <li>Updates applied automatically</li>
    <li>Backwards-compatible changes</li>
</ul>

<h3>Custom Database Changes</h3>
<p>For custom tables and data:</p>
<ul>
    <li>Use Entity Framework migrations</li>
    <li>Use DbUp or FluentMigrator</li>
    <li>Manual SQL scripts with version control</li>
</ul>

<h3>Zero-Downtime Considerations</h3>
<ul>
    <li>Add columns as nullable first</li>
    <li>Avoid renaming or removing columns</li>
    <li>Use feature flags for breaking changes</li>
    <li>Test rollback procedures</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ef-migration",
                            Title = "Entity Framework Migration",
                            Description = "Managing custom tables with EF migrations",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Custom DbContext
public class MyAppDbContext : DbContext
{
    public DbSet<CustomEntity> CustomEntities { get; set; }

    public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
        : base(options) { }
}

// Program.cs - register context
builder.Services.AddDbContext<MyAppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(""EPiServerDB"")));

// Create migration
// dotnet ef migrations add AddCustomEntity -c MyAppDbContext

// Generated migration
public partial class AddCustomEntity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: ""CustomEntities"",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation(""SqlServer:Identity"", ""1, 1""),
                Name = table.Column<string>(maxLength: 255),
                CreatedDate = table.Column<DateTime>()
            },
            constraints: table =>
            {
                table.PrimaryKey(""PK_CustomEntities"", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(""CustomEntities"");
    }
}

// Apply migrations on startup
public class MigrationStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(
        Action<IApplicationBuilder> next)
    {
        return app =>
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider
                .GetRequiredService<MyAppDbContext>();
            db.Database.Migrate();

            next(app);
        };
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "mu-blue-green",
                    ModuleId = "migration-upgrades",
                    Title = "Blue-Green Deployments",
                    Summary = "Deploy updates with zero downtime.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand blue-green deployment pattern",
                        "Configure traffic switching",
                        "Plan rollback procedures"
                    },
                    Content = @"
<h2>Blue-Green Deployments</h2>
<p>Blue-green deployment runs two identical environments, allowing instant switching between versions with zero downtime.</p>

<h3>How It Works</h3>
<ol>
    <li><strong>Blue</strong> - Current production environment</li>
    <li><strong>Green</strong> - New version environment</li>
    <li>Deploy updates to Green</li>
    <li>Test Green thoroughly</li>
    <li>Switch traffic from Blue to Green</li>
    <li>Blue becomes the new staging</li>
</ol>

<h3>DXP Deployment Slots</h3>
<p>DXP provides deployment slots for blue-green:</p>
<ul>
    <li>Primary slot (production)</li>
    <li>Deployment slot (staging)</li>
    <li>Slot swap for zero-downtime switch</li>
</ul>

<h3>Database Considerations</h3>
<p>Both environments share the database, so:</p>
<ul>
    <li>Schema changes must be backwards-compatible</li>
    <li>Use feature flags for breaking changes</li>
    <li>Consider read replicas for heavy loads</li>
</ul>

<h3>Rollback Strategy</h3>
<ul>
    <li>Keep Blue environment ready for instant rollback</li>
    <li>Monitor error rates after switch</li>
    <li>Define rollback criteria upfront</li>
    <li>Practice rollback procedures</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "blue-green-checklist",
                            Title = "Blue-Green Deployment Checklist",
                            Description = "Steps for safe blue-green deployment",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"# Blue-Green Deployment Checklist

## Pre-Deployment
- [ ] Database migrations are backwards-compatible
- [ ] Feature flags configured for new features
- [ ] Smoke tests prepared for Green environment
- [ ] Rollback procedure documented
- [ ] Team notified of deployment window

## Deploy to Green (Staging Slot)
- [ ] Deploy application to deployment slot
- [ ] Run database migrations (if any)
- [ ] Verify application starts correctly
- [ ] Run smoke tests
- [ ] Test critical user journeys
- [ ] Verify integrations work correctly

## Pre-Switch Validation
- [ ] Performance test Green environment
- [ ] Compare response times with Blue
- [ ] Check error rates are acceptable
- [ ] Verify caching is working
- [ ] Test with production-like traffic

## Switch Traffic
- [ ] Notify stakeholders of switch
- [ ] Perform slot swap
- [ ] Verify DNS/routing updated
- [ ] Monitor error rates closely
- [ ] Check application metrics

## Post-Switch Monitoring (15-30 minutes)
- [ ] Error rates within acceptable range
- [ ] Response times normal
- [ ] No unusual exceptions in logs
- [ ] User reports/feedback positive
- [ ] Business metrics unchanged

## If Rollback Needed
1. [ ] Perform immediate slot swap back
2. [ ] Investigate root cause
3. [ ] Document issues found
4. [ ] Plan fixes for next attempt

## Cleanup (after successful deploy)
- [ ] Document deployment outcome
- [ ] Update runbooks if needed
- [ ] Plan Blue environment refresh
- [ ] Schedule next deployment window",
                            IsInteractive = false
                        }
                    }
                }

            }
        };
    }

    #endregion
}
