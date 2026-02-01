using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.SaaS;

/// <summary>
/// Content provider for the Optimizely CMS (SaaS) course
/// </summary>
public class SaaSContentProvider : ILearningContentProvider
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
            BuildContentModelingModule(),
            BuildVisualBuilderEssentialsModule(),
            BuildVisualBuilderAdvancedModule(),
            BuildRestApiModule(),
            BuildGraphIntegrationModule(),
            BuildAccessRightsModule(),
            BuildAdvancedTopicsModule(),
            BuildWorkflowsPublishingModule(),
            BuildLocalizationModule(),
            BuildMediaManagementModule(),
            BuildWebhooksEventsModule(),
            BuildTroubleshootingModule()
        };
    }

    #region Module 1: Getting Started with CMS (SaaS)

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started with CMS (SaaS)",
            Description = "Learn the fundamentals of Optimizely CMS (SaaS), the powerful headless content management system.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-saas",
                    ModuleId = "getting-started",
                    Title = "What is Optimizely CMS (SaaS)?",
                    Summary = "Discover Optimizely CMS (SaaS) and how it enables headless content management.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely CMS (SaaS) is and its purpose",
                        "Learn the benefits of a headless CMS architecture",
                        "Understand the difference between SaaS and PaaS offerings"
                    },
                    Content = @"
<h2>Introduction to Optimizely CMS (SaaS)</h2>
<p>Optimizely CMS (SaaS) is a <strong>versatile headless CMS</strong> that lets you manage and distribute content across multiple platforms. By separating content management from the presentation layer, you can deliver rich digital experiences on any device or platform.</p>

<h3>What Makes It Headless?</h3>
<p>In a headless architecture, the CMS (the ""body"") is separated from the frontend presentation layer (the ""head""). This separation provides significant benefits:</p>
<ul>
    <li><strong>Multi-channel delivery</strong> - Content can be delivered to websites, mobile apps, IoT devices, and more</li>
    <li><strong>Technology flexibility</strong> - Frontend developers can use any framework (React, Vue, Next.js, etc.)</li>
    <li><strong>API-first approach</strong> - Content is delivered via APIs (REST and GraphQL)</li>
</ul>

<h3>SaaS vs PaaS</h3>
<p>Optimizely offers two deployment models:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">SaaS</th>
            <th class=""px-4 py-2 text-left"">PaaS (CMS 12)</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Hosting</td><td class=""px-4 py-2"">Fully managed by Optimizely</td><td class=""px-4 py-2"">Customer managed</td></tr>
        <tr><td class=""px-4 py-2"">Updates</td><td class=""px-4 py-2"">Automatic, versionless</td><td class=""px-4 py-2"">Manual upgrades</td></tr>
        <tr><td class=""px-4 py-2"">Architecture</td><td class=""px-4 py-2"">Headless-first</td><td class=""px-4 py-2"">Traditional or headless</td></tr>
        <tr><td class=""px-4 py-2"">Customization</td><td class=""px-4 py-2"">Via APIs and configuration</td><td class=""px-4 py-2"">Full code access</td></tr>
    </tbody>
</table>

<h3>Key Benefits</h3>
<ul>
    <li><strong>Scalability</strong> - Add websites and applications without affecting existing sites</li>
    <li><strong>Security</strong> - Content separated from presentation layer reduces attack surface</li>
    <li><strong>Future-proofing</strong> - Flexible architecture adapts to technological changes</li>
    <li><strong>Performance</strong> - Optimized content delivery through Optimizely Graph</li>
    <li><strong>Faster time-to-market</strong> - Focus on content and frontend, not infrastructure</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "gs-architecture",
                    ModuleId = "getting-started",
                    Title = "Architecture Overview",
                    Summary = "Understand the key components that make up the CMS (SaaS) architecture.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Identify the major components of CMS (SaaS)",
                        "Understand how components interact",
                        "Learn the role of Optimizely Graph in content delivery"
                    },
                    Content = @"
<h2>CMS (SaaS) Architecture</h2>
<p>The CMS (SaaS) architecture consists of several interconnected components that work together to provide a complete content management and delivery solution.</p>

<h3>Core Components</h3>

<h4>1. CMS Platform</h4>
<p>The foundation that drives the entire solution. It handles content storage, versioning, workflow management, and provides the APIs for content operations.</p>

<h4>2. CMS UI</h4>
<p>The user interface that provides the editing experience for content editors and administrators. This is where day-to-day content management happens.</p>

<h4>3. Visual Builder</h4>
<p>A powerful WYSIWYG editor that enables content managers to create and design page layouts using drag-and-drop functionality. Visual Builder introduces new concepts:</p>
<ul>
    <li><strong>Experiences</strong> - The main routable entry point for pages</li>
    <li><strong>Sections</strong> - Vertical content areas within experiences</li>
    <li><strong>Elements</strong> - The smallest building blocks containing actual content</li>
</ul>

<h4>4. Opti ID</h4>
<p>Optimizely's identity management system that handles authentication and user management across all Optimizely products.</p>

<h4>5. REST API</h4>
<p>Enables programmatic management of CMS resources including content types, content items, and configuration. Used primarily for:</p>
<ul>
    <li>Content type definitions</li>
    <li>Content CRUD operations</li>
    <li>System configuration</li>
</ul>

<h4>6. Optimizely Graph</h4>
<p>The content delivery layer that provides GraphQL-based content retrieval. Graph is optimized for high-performance, read-heavy operations and is the recommended way to fetch content for your frontend applications.</p>

<h3>Data Flow</h3>
<ol>
    <li>Content is created/edited in the CMS UI or via REST API</li>
    <li>Changes are indexed in Optimizely Graph</li>
    <li>Frontend applications query Graph for content delivery</li>
    <li>Content is rendered on websites, apps, or other channels</li>
</ol>
"
                },
                new Lesson
                {
                    Id = "gs-key-tools",
                    ModuleId = "getting-started",
                    Title = "Key Tools Overview",
                    Summary = "Learn about the three primary tools: Visual Builder, REST API, and Optimizely Graph.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand when to use Visual Builder",
                        "Know the capabilities of the REST API",
                        "Learn how Optimizely Graph delivers content"
                    },
                    Content = @"
<h2>The Three Primary Tools</h2>
<p>CMS (SaaS) provides three primary tools for different aspects of content management and delivery.</p>

<h3>Visual Builder</h3>
<p>Visual Builder provides an <strong>intuitive, drag-and-drop interface</strong> where you can create and organize content with real-time previews.</p>
<p><strong>Use Visual Builder when:</strong></p>
<ul>
    <li>Creating page layouts and designs</li>
    <li>Editing content with immediate visual feedback</li>
    <li>Working with reusable sections and blueprints</li>
    <li>Non-technical users need to manage content</li>
</ul>

<h3>REST API</h3>
<p>The REST API lets developers <strong>programmatically configure</strong> CMS instances, set up content types, and manage resources.</p>
<p><strong>Use the REST API when:</strong></p>
<ul>
    <li>Setting up content type definitions</li>
    <li>Automating content operations</li>
    <li>Integrating with external systems</li>
    <li>Building custom admin tools</li>
</ul>

<h3>Optimizely Graph</h3>
<p>Optimizely Graph facilitates <strong>efficient content retrieval</strong> across platforms using GraphQL, ensuring consistent, structured digital experiences.</p>
<p><strong>Use Optimizely Graph when:</strong></p>
<ul>
    <li>Fetching content for frontend rendering</li>
    <li>Building high-performance applications</li>
    <li>Querying content with complex filters</li>
    <li>Delivering content to multiple channels</li>
</ul>

<h3>Tool Comparison</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Tool</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Content creation & editing</td><td class=""px-4 py-2"">Visual Builder</td></tr>
        <tr><td class=""px-4 py-2"">Content type management</td><td class=""px-4 py-2"">REST API</td></tr>
        <tr><td class=""px-4 py-2"">Content delivery</td><td class=""px-4 py-2"">Optimizely Graph</td></tr>
        <tr><td class=""px-4 py-2"">Automation & integration</td><td class=""px-4 py-2"">REST API</td></tr>
    </tbody>
</table>
"
                },
                new Lesson
                {
                    Id = "gs-user-roles",
                    ModuleId = "getting-started",
                    Title = "User Roles & Workflows",
                    Summary = "Understand the different user roles and their responsibilities in CMS (SaaS).",
                    Order = 4,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Identify the three main user roles",
                        "Understand each role's responsibilities",
                        "Learn the typical development workflow"
                    },
                    Content = @"
<h2>User Roles in CMS (SaaS)</h2>
<p>CMS (SaaS) supports three primary user roles, each with distinct responsibilities and workflows.</p>

<h3>Content Managers</h3>
<p>Content managers are the primary users who create and maintain content. Their responsibilities include:</p>
<ul>
    <li>Creating and editing content using Visual Builder</li>
    <li>Managing digital assets (images, documents, videos)</li>
    <li>Publishing content to live sites</li>
    <li>Creating and using blueprints for consistent layouts</li>
    <li>Collaborating with team members on content</li>
</ul>

<h3>Content Administrators</h3>
<p>Administrators handle system configuration and setup. Their responsibilities include:</p>
<ul>
    <li>Configuring system settings</li>
    <li>Managing languages and localization</li>
    <li>Setting up access rights and permissions</li>
    <li>Creating and managing content types (in collaboration with developers)</li>
    <li>Managing approval sequences and workflows</li>
    <li>Importing and exporting content</li>
</ul>

<h3>Developers</h3>
<p>Developers implement the technical foundation and content delivery. Their workflow follows three stages:</p>
<ol>
    <li><strong>Plan</strong> - Define the site architecture and content model</li>
    <li><strong>Build</strong> - Create content types and configure Visual Builder</li>
    <li><strong>Render</strong> - Build frontend applications that consume content via Graph</li>
</ol>

<h3>Collaboration Model</h3>
<p>These roles work together in a continuous cycle:</p>
<ol>
    <li>Developers define content types and structure</li>
    <li>Administrators configure permissions and workflows</li>
    <li>Content managers create content using the defined structure</li>
    <li>Feedback loops inform improvements to the content model</li>
</ol>
"
                }
            }
        };
    }

    #endregion

    #region Module 2: Content Modeling Fundamentals

    private LearningModule BuildContentModelingModule()
    {
        return new LearningModule
        {
            Id = "content-modeling",
            Title = "Content Modeling Fundamentals",
            Description = "Learn how to design and implement content types that form the foundation of your CMS.",
            Icon = "cube-transparent",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cm-content-types",
                    ModuleId = "content-modeling",
                    Title = "Understanding Content Types",
                    Summary = "Learn what content types are and how they define your content structure.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what a content type is",
                        "Learn how content types define content structure",
                        "Know the relationship between types and instances"
                    },
                    Content = @"
<h2>What Are Content Types?</h2>
<p>A content type in CMS (SaaS) defines the <strong>characteristics and data model schema</strong> of a content item. It serves as the foundation for creating pages, blocks, or experiences by specifying properties that hold information.</p>

<h3>Content Types as Blueprints</h3>
<p>Think of a content type as a blueprint or template. Just as an architectural blueprint defines the structure of a building, a content type defines:</p>
<ul>
    <li><strong>Properties</strong> - The fields that hold data (text, images, links, etc.)</li>
    <li><strong>Validation rules</strong> - Requirements for each property</li>
    <li><strong>Display settings</strong> - How content appears in the editor</li>
    <li><strong>Behaviors</strong> - Whether it's a page, block, or media item</li>
</ul>

<h3>Content Instances</h3>
<p>When you create content based on a content type, you create an <strong>instance</strong> of that type. For example:</p>
<ul>
    <li>""Article Page"" content type defines the structure</li>
    <li>""10 Tips for Better SEO"" is an instance with actual content</li>
</ul>

<h3>Standard Metadata</h3>
<p>Every content type includes standard metadata fields:</p>
<ul>
    <li><code>key</code> - Unique identifier</li>
    <li><code>displayName</code> - Human-readable name</li>
    <li><code>description</code> - Purpose description</li>
    <li><code>baseType</code> - The fundamental type it extends</li>
    <li><code>created</code> / <code>lastModified</code> - Timestamps</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "cm-simple-content-type",
                            Title = "Simple Content Type Definition",
                            Description = "A basic page content type with common properties.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""key"": ""articlePage"",
  ""baseType"": ""_page"",
  ""displayName"": ""Article Page"",
  ""description"": ""A page for blog articles and news"",
  ""properties"": {
    ""title"": {
      ""type"": ""string"",
      ""displayName"": ""Title"",
      ""required"": true
    },
    ""summary"": {
      ""type"": ""string"",
      ""displayName"": ""Summary"",
      ""format"": ""textarea""
    },
    ""publishDate"": {
      ""type"": ""dateTime"",
      ""displayName"": ""Publish Date""
    }
  }
}",
                            SampleResponse = @"Content type 'articlePage' created successfully.

You can now create instances of this content type in the CMS UI
or via the REST API.",
                            Hints = new List<string>
                            {
                                "The 'key' must be unique across all content types",
                                "Base type determines fundamental behavior (page, block, media, etc.)"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-base-types",
                    ModuleId = "content-modeling",
                    Title = "Base Types Explained",
                    Summary = "Understand the different base types and when to use each one.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Know all available base types",
                        "Understand the purpose of each base type",
                        "Choose the right base type for your needs"
                    },
                    Content = @"
<h2>Base Types in CMS (SaaS)</h2>
<p>Every content type inherits from a <strong>base type</strong> that determines its fundamental behavior. Each base type has a corresponding Optimizely Graph schema for querying.</p>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-200""><strong>Important:</strong> You cannot change the base type after you create the content type.</p>
</div>

<h3>Available Base Types</h3>

<h4>_page</h4>
<p>Displayable content with a unique URL representing a webpage. Use for:</p>
<ul>
    <li>Standard website pages</li>
    <li>Landing pages</li>
    <li>Article/blog pages</li>
</ul>

<h4>_component (block)</h4>
<p>Reusable, locale-aware components without URLs. Use for:</p>
<ul>
    <li>Reusable content blocks</li>
    <li>Shared components across pages</li>
    <li>Template sections</li>
</ul>

<h4>_experience</h4>
<p>Extension of page type enhanced for Visual Builder. Use for:</p>
<ul>
    <li>Visual Builder-enabled pages</li>
    <li>Dynamic layouts with sections</li>
</ul>

<h4>_section</h4>
<p>Organizational containers within experiences. System-provided for Visual Builder.</p>

<h4>_media, _image, _video</h4>
<p>Binary data storage types. Use for:</p>
<ul>
    <li>Images and photos (_image)</li>
    <li>Video content (_video)</li>
    <li>Documents and other files (_media)</li>
</ul>

<h4>_folder</h4>
<p>Content organization without versioning. Use for:</p>
<ul>
    <li>Organizing content in hierarchies</li>
    <li>Creating content containers</li>
</ul>

<h3>Graph Schema Mapping</h3>
<p>Each base type maps to specific Graph types for querying. For example, <code>_page</code> types can be queried using the page-related Graph queries.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "cm-base-type-comparison",
                            Title = "Base Type Selection Guide",
                            Description = "Choosing the right base type for different scenarios.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Page with URL - use _page
{
  ""key"": ""productPage"",
  ""baseType"": ""_page"",
  ...
}

// Reusable component - use _component
{
  ""key"": ""heroBlock"",
  ""baseType"": ""_component"",
  ...
}

// Visual Builder page - use _experience
{
  ""key"": ""landingExperience"",
  ""baseType"": ""_experience"",
  ...
}

// Image with metadata - use _image
{
  ""key"": ""productImage"",
  ""baseType"": ""_image"",
  ...
}",
                            SampleResponse = @"Base Type Selection Criteria:

1. Does it need a URL? -> _page or _experience
2. Is it reusable content? -> _component
3. Does it use Visual Builder? -> _experience
4. Is it binary data? -> _media, _image, or _video
5. Just organizing content? -> _folder",
                            Hints = new List<string>
                            {
                                "Choose _experience over _page when using Visual Builder",
                                "Components registered in Graph both standalone and with 'Property' suffix"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-property-types",
                    ModuleId = "content-modeling",
                    Title = "Property Types & Validation",
                    Summary = "Learn about available property types and how to validate content.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Know all available property types",
                        "Understand validation options",
                        "Configure property settings correctly"
                    },
                    Content = @"
<h2>Property Types</h2>
<p>Properties define the fields within your content types. CMS (SaaS) supports a variety of data types to handle different content needs.</p>

<h3>Basic Data Types</h3>
<ul>
    <li><code>string</code> - Text content (single line or multiline)</li>
    <li><code>boolean</code> - True/false values</li>
    <li><code>integer</code> - Whole numbers</li>
    <li><code>float</code> - Decimal numbers</li>
    <li><code>dateTime</code> - Date and time values</li>
</ul>

<h3>Content-Specific Types</h3>
<ul>
    <li><code>richText</code> - Formatted HTML content</li>
    <li><code>contentReference</code> - Reference to other content items</li>
    <li><code>link</code> - URLs with optional text</li>
    <li><code>component</code> - Nested content type</li>
    <li><code>binary</code> - File data</li>
</ul>

<h3>Collection Types</h3>
<ul>
    <li><code>array</code> - Lists of any type (strings, references, components)</li>
</ul>

<h3>Validation Options</h3>
<p>Properties support various validations:</p>
<ul>
    <li><code>required</code> - Property must have a value</li>
    <li><code>maxLength</code> - Maximum length for strings/arrays</li>
    <li><code>pattern</code> - Regex pattern matching</li>
    <li><code>minimum</code> / <code>maximum</code> - Numeric bounds</li>
    <li><code>enum</code> - Predefined allowed values</li>
    <li><code>allowedTypes</code> - Restrict content references</li>
</ul>

<h3>Property Attributes</h3>
<p>Each property can have additional attributes:</p>
<ul>
    <li><code>displayName</code> - Label shown in the UI</li>
    <li><code>description</code> - Help text for editors</li>
    <li><code>format</code> - Display format (textarea, etc.)</li>
    <li><code>localized</code> - Enable per-language values</li>
    <li><code>group</code> - Organize properties in the UI</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "cm-property-examples",
                            Title = "Property Configuration Examples",
                            Description = "Various property type configurations with validation.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""properties"": {
    ""title"": {
      ""type"": ""string"",
      ""displayName"": ""Page Title"",
      ""required"": true,
      ""maxLength"": 100,
      ""localized"": true
    },
    ""body"": {
      ""type"": ""richText"",
      ""displayName"": ""Body Content"",
      ""indexingType"": ""searchable""
    },
    ""category"": {
      ""type"": ""string"",
      ""displayName"": ""Category"",
      ""enum"": [""News"", ""Blog"", ""Product"", ""Support""]
    },
    ""rating"": {
      ""type"": ""integer"",
      ""displayName"": ""Rating"",
      ""minimum"": 1,
      ""maximum"": 5
    },
    ""relatedPages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""contentReference"",
        ""allowedTypes"": [""articlePage""]
      },
      ""maxLength"": 5
    },
    ""featuredImage"": {
      ""type"": ""contentReference"",
      ""allowedTypes"": [""_image""],
      ""displayName"": ""Featured Image""
    }
  }
}",
                            SampleResponse = @"Property Configuration Summary:
- title: Required localized string, max 100 chars
- body: Searchable rich text content
- category: Dropdown with 4 options
- rating: Integer between 1-5
- relatedPages: Up to 5 article references
- featuredImage: Single image reference",
                            Hints = new List<string>
                            {
                                "Use 'localized: true' for content that needs translation",
                                "indexingType affects how content appears in Graph queries"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "cm-best-practices",
                    ModuleId = "content-modeling",
                    Title = "Content Modeling Best Practices",
                    Summary = "Learn strategies for designing effective and maintainable content models.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Design modular, reusable content types",
                        "Plan for scalability and maintenance",
                        "Avoid common content modeling mistakes"
                    },
                    Content = @"
<h2>Content Modeling Best Practices</h2>
<p>A well-designed content model is crucial for a successful CMS implementation. Follow these best practices to create maintainable, scalable content structures.</p>

<h3>1. Start with User Needs</h3>
<ul>
    <li>Identify what content editors need to create</li>
    <li>Map out the content workflows</li>
    <li>Consider the frontend requirements</li>
</ul>

<h3>2. Design for Reusability</h3>
<ul>
    <li>Create components that can be used across multiple pages</li>
    <li>Use base types appropriately</li>
    <li>Avoid duplicating properties across content types</li>
</ul>

<h3>3. Keep It Simple</h3>
<ul>
    <li>Don't over-engineer the model</li>
    <li>Start with essential properties, add more later</li>
    <li>Use clear, descriptive names</li>
</ul>

<h3>4. Plan for Indexing</h3>
<p>Configure indexing appropriately for Graph:</p>
<ul>
    <li><code>default</code> - Indexed but not filterable/searchable</li>
    <li><code>queryable</code> - Allows filtering and sorting</li>
    <li><code>searchable</code> - Full-text search enabled</li>
    <li><code>disabled</code> - Excluded from Graph</li>
</ul>

<h3>5. Reserved Names</h3>
<p>Avoid these reserved content type names:</p>
<ul>
    <li>String, Int, Float, DateTime, JSON, Boolean</li>
    <li>RichText, SearchableRichText, Link</li>
    <li>ContentReference, ContentUrl</li>
</ul>

<h3>6. Documentation</h3>
<ul>
    <li>Use description fields to guide editors</li>
    <li>Document the purpose of each content type</li>
    <li>Maintain a content model diagram</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 3: Visual Builder Essentials

    private LearningModule BuildVisualBuilderEssentialsModule()
    {
        return new LearningModule
        {
            Id = "visual-builder-essentials",
            Title = "Visual Builder Essentials",
            Description = "Master the fundamentals of Visual Builder for creating dynamic page layouts.",
            Icon = "paint-brush",
            Order = 3,
            Difficulty = ModuleDifficulty.Beginner,
            Prerequisites = new[] { "content-modeling" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "vb-introduction",
                    ModuleId = "visual-builder-essentials",
                    Title = "Introduction to Visual Builder",
                    Summary = "Discover Visual Builder and its role in content creation.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Visual Builder is",
                        "Know the key concepts and terminology",
                        "Navigate the Visual Builder interface"
                    },
                    Content = @"
<h2>What is Visual Builder?</h2>
<p>Visual Builder is the editor interface in CMS (SaaS) that makes <strong>content creation and layout building intuitive and accessible</strong> to non-technical users. It provides a drag-and-drop experience with real-time previews.</p>

<h3>Why Visual Builder?</h3>
<p>Visual Builder bridges the gap between developers and content creators:</p>
<ul>
    <li>Developers define the building blocks (content types, styles)</li>
    <li>Content managers assemble layouts without coding</li>
    <li>Changes are immediately visible in preview</li>
</ul>

<h3>Key Concepts</h3>

<h4>Building Blocks</h4>
<p>Visual Builder uses three hierarchical building blocks:</p>
<ol>
    <li><strong>Experiences</strong> - The page-level container</li>
    <li><strong>Sections</strong> - Major content areas within experiences</li>
    <li><strong>Elements</strong> - Individual content components</li>
</ol>

<h4>Layouts</h4>
<p>Two layout types organize content:</p>
<ul>
    <li><strong>Outline</strong> - A flat list of sections (used by experiences)</li>
    <li><strong>Grid</strong> - Rows, columns, and elements (used by sections)</li>
</ul>

<h3>Interface Overview</h3>
<ul>
    <li><strong>Outline Panel</strong> - View and reorder sections</li>
    <li><strong>Properties Panel</strong> - Edit selected item properties</li>
    <li><strong>Preview Area</strong> - See changes in real-time</li>
    <li><strong>Toolbar</strong> - Access tools and settings</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "vb-experiences",
                    ModuleId = "visual-builder-essentials",
                    Title = "Experiences: The Routable Entry Point",
                    Summary = "Learn how experiences serve as the foundation for Visual Builder pages.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what experiences are",
                        "Know how experiences relate to pages",
                        "Create and configure experiences"
                    },
                    Content = @"
<h2>Understanding Experiences</h2>
<p>An experience is the <strong>main routable entry point</strong> of Visual Builder. It extends the traditional page concept with access to the layout system through compositions.</p>

<h3>Experience vs Page</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Traditional Page</th>
            <th class=""px-4 py-2 text-left"">Experience</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">URL Routing</td><td class=""px-4 py-2"">Yes</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2"">Visual Builder</td><td class=""px-4 py-2"">No</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2"">Layout System</td><td class=""px-4 py-2"">Fixed</td><td class=""px-4 py-2"">Dynamic (Outline)</td></tr>
        <tr><td class=""px-4 py-2"">Sections Support</td><td class=""px-4 py-2"">No</td><td class=""px-4 py-2"">Yes</td></tr>
    </tbody>
</table>

<h3>Outline Layout</h3>
<p>Experiences use the <strong>outline layout type</strong>, which is a flat, ordered list of sections. The outline provides:</p>
<ul>
    <li>Easy section reordering via drag-and-drop</li>
    <li>Clear visual hierarchy</li>
    <li>Simple section management</li>
</ul>

<h3>Experience Properties</h3>
<p>Experiences can have their own properties in addition to sections:</p>
<ul>
    <li>SEO metadata (title, description)</li>
    <li>Page-level settings</li>
    <li>Custom fields defined in the content type</li>
</ul>

<h3>Saving as Blueprints</h3>
<p>Entire experiences can be saved as <strong>blueprints</strong> - reusable templates that content managers can use to quickly create new pages with predefined layouts.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "vb-experience-type",
                            Title = "Experience Content Type Definition",
                            Description = "Define an experience type for Visual Builder.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""key"": ""landingPageExperience"",
  ""baseType"": ""_experience"",
  ""displayName"": ""Landing Page"",
  ""description"": ""A flexible landing page with Visual Builder support"",
  ""properties"": {
    ""metaTitle"": {
      ""type"": ""string"",
      ""displayName"": ""SEO Title"",
      ""maxLength"": 60
    },
    ""metaDescription"": {
      ""type"": ""string"",
      ""displayName"": ""SEO Description"",
      ""format"": ""textarea"",
      ""maxLength"": 160
    }
  }
}",
                            SampleResponse = @"Experience type created successfully.

This experience will appear in Visual Builder with:
- Full outline layout support
- Section management capabilities
- Custom SEO properties",
                            Hints = new List<string>
                            {
                                "Use _experience base type for Visual Builder pages",
                                "Keep page-level properties minimal - use sections for content"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "vb-sections",
                    ModuleId = "visual-builder-essentials",
                    Title = "Sections: Grid Layouts",
                    Summary = "Master sections and their grid-based layout system.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand section structure",
                        "Work with rows and columns",
                        "Configure section layouts"
                    },
                    Content = @"
<h2>Understanding Sections</h2>
<p>Sections are <strong>vertical content chunks</strong> within an experience. They extend block functionality with access to the layout system through a grid composition.</p>

<h3>Grid Layout System</h3>
<p>Sections use the <strong>grid layout type</strong>, which provides a hierarchical structure:</p>
<ol>
    <li><strong>Rows</strong> - Horizontal containers</li>
    <li><strong>Columns</strong> - Vertical divisions within rows</li>
    <li><strong>Elements</strong> - Content within columns</li>
</ol>

<h3>Working with the Grid</h3>
<p>The grid system allows flexible layouts:</p>
<ul>
    <li>Add multiple rows per section</li>
    <li>Configure column widths (equal, 2/3-1/3, etc.)</li>
    <li>Place multiple elements per column</li>
    <li>Apply styles at any level</li>
</ul>

<h3>Section Types</h3>
<p>Developers can create custom section types with:</p>
<ul>
    <li>Pre-defined layouts</li>
    <li>Custom properties</li>
    <li>Specific styling options</li>
</ul>

<h3>Section Behaviors</h3>
<p>Sections can be configured to:</p>
<ul>
    <li>Allow specific element types only</li>
    <li>Have required elements</li>
    <li>Include section-level properties (background, spacing)</li>
</ul>

<h3>Saving as Blueprints</h3>
<p>Frequently used section configurations can be saved as blueprints for reuse across pages.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "vb-section-structure",
                            Title = "Section Grid Structure",
                            Description = "Example of a section with rows, columns, and elements.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Section Structure in Visual Builder

Section: ""Hero Section""
├── Row 1 (Full Width)
│   └── Column 1 (100%)
│       └── Element: Hero Image
│
├── Row 2 (Two Column)
│   ├── Column 1 (60%)
│   │   └── Element: Heading
│   │   └── Element: Rich Text
│   │
│   └── Column 2 (40%)
│       └── Element: Call-to-Action Button
│
└── Row 3 (Three Column)
    ├── Column 1 (33%)
    │   └── Element: Feature Card
    ├── Column 2 (33%)
    │   └── Element: Feature Card
    └── Column 3 (33%)
        └── Element: Feature Card",
                            SampleResponse = @"This section layout demonstrates:
- Multiple rows with different configurations
- Variable column widths
- Nested elements within columns
- Common landing page pattern",
                            Hints = new List<string>
                            {
                                "Column widths are typically percentages that add up to 100%",
                                "Elements are the leaf nodes - they cannot contain other elements"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "vb-elements",
                    ModuleId = "visual-builder-essentials",
                    Title = "Elements: Building Blocks",
                    Summary = "Learn about elements, the smallest content units in Visual Builder.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what elements are",
                        "Know element limitations",
                        "Create element content types"
                    },
                    Content = @"
<h2>Understanding Elements</h2>
<p>Elements are the <strong>smallest building blocks</strong> in Visual Builder. They contain the actual content data and are terminal nodes - they cannot have children.</p>

<h3>Element Characteristics</h3>
<ul>
    <li>Extension of block content type</li>
    <li>Restricted property types (no nested compositions)</li>
    <li>Leaf nodes in the experience tree</li>
    <li>Direct content containers</li>
</ul>

<h3>Common Element Types</h3>
<ul>
    <li><strong>Heading</strong> - Title text with level options</li>
    <li><strong>Rich Text</strong> - Formatted content with WYSIWYG editor</li>
    <li><strong>Image</strong> - Single image with alt text</li>
    <li><strong>Button</strong> - Call-to-action with link</li>
    <li><strong>Video</strong> - Embedded video content</li>
    <li><strong>Testimonial</strong> - Quote with attribution</li>
</ul>

<h3>Creating Elements</h3>
<p>To create an element type:</p>
<ol>
    <li>Go to Settings > Content Types</li>
    <li>Create a new Block Type</li>
    <li>Add your properties</li>
    <li>Enable ""Available for composition in Visual Builder""</li>
    <li>Enable ""Display as Element""</li>
</ol>

<h3>Element vs Section-Enabled Block</h3>
<p>Blocks can be configured as:</p>
<ul>
    <li><strong>Elements</strong> - No grid layout, simple content</li>
    <li><strong>Sections</strong> - Have grid layout for complex compositions</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "vb-element-definition",
                            Title = "Element Content Type",
                            Description = "Define a simple element for Visual Builder.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""key"": ""testimonialElement"",
  ""baseType"": ""_component"",
  ""displayName"": ""Testimonial"",
  ""description"": ""Customer testimonial with quote and attribution"",
  ""compositionBehaviors"": [""element""],
  ""properties"": {
    ""quote"": {
      ""type"": ""string"",
      ""displayName"": ""Quote"",
      ""format"": ""textarea"",
      ""required"": true
    },
    ""authorName"": {
      ""type"": ""string"",
      ""displayName"": ""Author Name"",
      ""required"": true
    },
    ""authorTitle"": {
      ""type"": ""string"",
      ""displayName"": ""Author Title""
    },
    ""authorImage"": {
      ""type"": ""contentReference"",
      ""displayName"": ""Author Photo"",
      ""allowedTypes"": [""_image""]
    }
  }
}",
                            SampleResponse = @"Element type created successfully.

This testimonial element will be available in Visual Builder
to drag into any section column.",
                            Hints = new List<string>
                            {
                                "compositionBehaviors: ['element'] marks it as an element",
                                "Keep elements focused on a single purpose"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 4: Visual Builder Advanced

    private LearningModule BuildVisualBuilderAdvancedModule()
    {
        return new LearningModule
        {
            Id = "visual-builder-advanced",
            Title = "Working with Visual Builder",
            Description = "Advanced Visual Builder techniques including templates, styles, and blueprints.",
            Icon = "adjustments-horizontal",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "visual-builder-essentials" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "vba-display-templates",
                    ModuleId = "visual-builder-advanced",
                    Title = "Display Templates & Styles",
                    Summary = "Configure display options and styling for Visual Builder content.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand display templates",
                        "Configure style settings",
                        "Apply styles at different levels"
                    },
                    Content = @"
<h2>Display Templates</h2>
<p>Display templates define the <strong>visual rendering options</strong> available to content managers. Developers create templates that content managers can select in Visual Builder.</p>

<h3>Template Association</h3>
<p>Templates can be associated with:</p>
<ul>
    <li><strong>Base types</strong> - experience, section, component</li>
    <li><strong>Content types</strong> - Specific content type keys</li>
    <li><strong>Node types</strong> - row, column</li>
</ul>

<h3>Style Settings</h3>
<p>Each template can expose settings that content managers can configure:</p>

<h4>Setting Editor Types</h4>
<ul>
    <li><strong>Select (dropdown)</strong> - Single selection from predefined options</li>
    <li><strong>Checkbox</strong> - Toggle true/false values</li>
</ul>

<h3>Applying Styles</h3>
<p>Styles can be applied at multiple levels:</p>
<ul>
    <li><strong>Experience level</strong> - Page-wide settings</li>
    <li><strong>Section level</strong> - Section background, spacing</li>
    <li><strong>Row/Column level</strong> - Layout adjustments</li>
    <li><strong>Element level</strong> - Component-specific styling</li>
</ul>

<h3>Style Inheritance</h3>
<p>Styles cascade from parent to child, allowing for:</p>
<ul>
    <li>Consistent theming across sections</li>
    <li>Override capability at lower levels</li>
    <li>Reduced configuration repetition</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "vba-style-config",
                            Title = "Display Template with Styles",
                            Description = "Configure a display template with style settings.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""key"": ""heroSectionTemplate"",
  ""displayName"": ""Hero Section"",
  ""baseType"": ""section"",
  ""contentTypes"": [""heroSection""],
  ""settings"": [
    {
      ""key"": ""backgroundColor"",
      ""displayName"": ""Background Color"",
      ""editor"": ""select"",
      ""options"": [
        { ""value"": ""white"", ""label"": ""White"" },
        { ""value"": ""gray"", ""label"": ""Light Gray"" },
        { ""value"": ""primary"", ""label"": ""Brand Primary"" },
        { ""value"": ""dark"", ""label"": ""Dark"" }
      ],
      ""default"": ""white""
    },
    {
      ""key"": ""fullWidth"",
      ""displayName"": ""Full Width"",
      ""editor"": ""checkbox"",
      ""default"": false
    },
    {
      ""key"": ""paddingSize"",
      ""displayName"": ""Vertical Padding"",
      ""editor"": ""select"",
      ""options"": [
        { ""value"": ""small"", ""label"": ""Small"" },
        { ""value"": ""medium"", ""label"": ""Medium"" },
        { ""value"": ""large"", ""label"": ""Large"" }
      ],
      ""default"": ""medium""
    }
  ]
}",
                            SampleResponse = @"Display template settings will appear in Visual Builder
when editors select this section, allowing them to:
- Choose background color from brand palette
- Toggle full-width display
- Select padding size",
                            Hints = new List<string>
                            {
                                "Keep style options aligned with your design system",
                                "Default values ensure consistent baseline appearance"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "vba-blueprints",
                    ModuleId = "visual-builder-advanced",
                    Title = "Creating & Using Blueprints",
                    Summary = "Learn to create and manage reusable layout templates.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand blueprint functionality",
                        "Create blueprints from existing content",
                        "Use blueprints effectively"
                    },
                    Content = @"
<h2>Understanding Blueprints</h2>
<p>Blueprints are <strong>reusable layout templates</strong> that content managers can create directly in the CMS. They enable rapid content creation with consistent layouts.</p>

<h3>How Blueprints Work</h3>
<p>When you use a blueprint to create content:</p>
<ol>
    <li>Visual Builder copies the entire layout structure</li>
    <li>A new, independent content instance is created</li>
    <li>The new content is <strong>not connected</strong> to the original blueprint</li>
    <li>Changes to the blueprint don't affect existing content</li>
</ol>

<h3>Blueprint Types</h3>
<ul>
    <li><strong>Experience blueprints</strong> - Complete page templates</li>
    <li><strong>Section blueprints</strong> - Reusable section layouts</li>
</ul>

<h3>Creating Blueprints</h3>
<p>To save content as a blueprint:</p>
<ol>
    <li>Design your experience or section layout</li>
    <li>Configure all settings and styles</li>
    <li>Add placeholder content if needed</li>
    <li>Use ""Save as Blueprint"" option</li>
    <li>Give it a descriptive name</li>
</ol>

<h3>Blueprint Best Practices</h3>
<ul>
    <li>Create blueprints for commonly used layouts</li>
    <li>Use clear naming conventions</li>
    <li>Include example content to guide editors</li>
    <li>Document when each blueprint should be used</li>
    <li>Review and update blueprints regularly</li>
</ul>

<h3>Blueprint Storage</h3>
<p>Blueprints are stored in a dedicated blueprints folder within the CMS, making them easy to find and manage.</p>
"
                },
                new Lesson
                {
                    Id = "vba-composition-queries",
                    ModuleId = "visual-builder-advanced",
                    Title = "Visual Builder Composition Queries",
                    Summary = "Query Visual Builder content using Optimizely Graph.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand composition structure in Graph",
                        "Query experiences with nested content",
                        "Use explicit and recursive queries"
                    },
                    Content = @"
<h2>Querying Visual Builder Content</h2>
<p>Visual Builder content is indexed in Optimizely Graph as <strong>composition models</strong>. Understanding the structure is key to building effective queries.</p>

<h3>Composition Structure</h3>
<p>When indexed, experiences become queryable structures containing:</p>
<ul>
    <li><strong>CompositionNode</strong> - Base type for all nodes</li>
    <li><strong>CompositionStructureNode</strong> - Structural elements with children</li>
    <li><strong>CompositionComponentNode</strong> - Actual content components</li>
</ul>

<h3>Node Properties</h3>
<p>Each CompositionNode includes:</p>
<ul>
    <li><code>type</code> - Content type (e.g., HeroSection)</li>
    <li><code>nodeType</code> - Category (experience, section, row, column, component)</li>
    <li><code>displayName</code> - The node's label</li>
    <li><code>key</code> - Unique identifier</li>
</ul>

<h3>Query Approaches</h3>

<h4>Explicit Queries</h4>
<p>Define fixed nested levels when structure depth is known:</p>
<pre>experience → section → row → column → component</pre>

<h4>Recursive Queries</h4>
<p>Use GraphQL's <code>@recursive</code> directive for variable-depth structures, avoiding repetitive nesting.</p>

<h3>Accessing Component Data</h3>
<p>Use GraphQL fragments to access component-specific fields like headings, rich text, images, and custom properties.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "vba-composition-query",
                            Title = "Experience Composition Query",
                            Description = "Query an experience with its nested sections and elements.",
                            Type = ExampleType.Query,
                            ExampleContent = @"{
  LandingPageExperience {
    items {
      Name
      _metadata {
        url {
          default
        }
      }
      composition {
        nodeType
        nodes {
          ... on CompositionStructureNode {
            type
            nodeType
            nodes {
              ... on CompositionStructureNode {
                type
                nodes {
                  ... on CompositionComponentNode {
                    type
                    component {
                      ... on HeadingElement {
                        text
                        level
                      }
                      ... on RichTextElement {
                        content {
                          html
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}",
                            SampleResponse = @"{
  ""data"": {
    ""LandingPageExperience"": {
      ""items"": [
        {
          ""Name"": ""Homepage"",
          ""_metadata"": {
            ""url"": {
              ""default"": ""/""
            }
          },
          ""composition"": {
            ""nodeType"": ""experience"",
            ""nodes"": [
              {
                ""type"": ""HeroSection"",
                ""nodeType"": ""section"",
                ""nodes"": [...]
              }
            ]
          }
        }
      ]
    }
  }
}",
                            Hints = new List<string>
                            {
                                "Use fragments to keep queries maintainable",
                                "@recursive directive simplifies deep structures"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "vba-developer-config",
                    ModuleId = "visual-builder-advanced",
                    Title = "Developer Configuration",
                    Summary = "Configure Visual Builder for your frontend application.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Set up Visual Builder preview",
                        "Configure applications and hosts",
                        "Troubleshoot common issues"
                    },
                    Content = @"
<h2>Developer Configuration</h2>
<p>To fully enable Visual Builder capabilities, developers need to configure several settings in both the CMS and the frontend application.</p>

<h3>Preview Configuration</h3>
<p>Live preview allows content managers to see changes in real-time:</p>
<ol>
    <li>Navigate to <strong>Settings > Applications</strong></li>
    <li>Configure your frontend application URL</li>
    <li>Enable preview token settings</li>
    <li>Set up the preview endpoint in your frontend</li>
</ol>

<h3>Website Configuration</h3>
<p>In <strong>Settings > Manage Websites</strong>:</p>
<ol>
    <li>Create a new website entry</li>
    <li>Set the frontend URL</li>
    <li>Configure the start page</li>
    <li>Add host configurations</li>
</ol>

<h3>Graph Synchronization</h3>
<p>After configuration changes:</p>
<ol>
    <li>Run ""Optimizely Graph Full Synchronization""</li>
    <li>Verify content appears in Graph</li>
    <li>Test queries in GraphQL playground</li>
</ol>

<h3>Troubleshooting</h3>
<p>Common issues and solutions:</p>
<ul>
    <li><strong>404 in preview</strong> - Enable preview tokens in Applications settings</li>
    <li><strong>Content not in Graph</strong> - Run full synchronization</li>
    <li><strong>Styles not applying</strong> - Verify display template configuration</li>
    <li><strong>Elements not available</strong> - Check composition behavior settings</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 5: REST API Fundamentals

    private LearningModule BuildRestApiModule()
    {
        return new LearningModule
        {
            Id = "rest-api",
            Title = "REST API Fundamentals",
            Description = "Learn to manage content programmatically using the CMS REST API.",
            Icon = "code-bracket-square",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-modeling" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "api-introduction",
                    ModuleId = "rest-api",
                    Title = "Introduction to the REST API",
                    Summary = "Overview of the CMS REST API and its capabilities.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand REST API purpose and scope",
                        "Know when to use REST API vs Graph",
                        "Learn API conventions"
                    },
                    Content = @"
<h2>CMS REST API Overview</h2>
<p>The CMS (SaaS) REST API enables <strong>programmatic management</strong> of your CMS resources. It's designed for resource management operations, not content delivery.</p>

<h3>When to Use REST API</h3>
<ul>
    <li>Creating and managing content types</li>
    <li>CRUD operations on content items</li>
    <li>System configuration and setup</li>
    <li>Automation and integration scenarios</li>
    <li>Building custom admin tools</li>
</ul>

<h3>When to Use Optimizely Graph</h3>
<ul>
    <li>High-performance content delivery</li>
    <li>Frontend data fetching</li>
    <li>Complex content queries</li>
    <li>Read-heavy operations</li>
</ul>

<h3>API Base URL</h3>
<p>All API calls use the base URL:</p>
<pre>https://api.cms.optimizely.com</pre>

<h3>API Conventions</h3>
<ul>
    <li><strong>Content-Type</strong>: <code>application/json</code></li>
    <li><strong>PATCH requests</strong>: Use <code>application/merge-patch+json</code></li>
    <li><strong>Rate limiting</strong>: 100 requests per 10 seconds per IP</li>
</ul>

<h3>Available Resources</h3>
<ul>
    <li>Content types (definitions)</li>
    <li>Content items</li>
    <li>Display templates</li>
    <li>Property formats and groups</li>
    <li>OAuth tokens</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "api-authentication",
                    ModuleId = "rest-api",
                    Title = "Authentication & API Keys",
                    Summary = "Learn how to authenticate with the CMS REST API.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create API keys in the CMS",
                        "Obtain access tokens",
                        "Use bearer authentication"
                    },
                    Content = @"
<h2>API Authentication</h2>
<p>The CMS REST API requires <strong>OAuth 2.0 authentication</strong> using JSON Web Tokens (JWT).</p>

<h3>Creating API Keys</h3>
<ol>
    <li>Go to <strong>Settings > API Keys</strong></li>
    <li>Click <strong>Create API Key</strong></li>
    <li>Enter a name (letters, numbers, hyphens, underscores only)</li>
    <li>Optionally enable Impersonation</li>
    <li>Click <strong>Create API Key</strong></li>
    <li>Save the Client ID and Secret securely</li>
</ol>

<div class=""bg-yellow-50 dark:bg-yellow-900/20 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-200""><strong>Important:</strong> The secret is only shown once. Store it securely.</p>
</div>

<h3>Obtaining Access Tokens</h3>
<p>Request a token from the OAuth endpoint:</p>
<pre>POST https://api.cms.optimizely.com/oauth/token</pre>

<h3>Required Scope</h3>
<p>The API requires the <code>api:admin</code> scope, which is included by default in access tokens.</p>

<h3>Using the Token</h3>
<p>Include the token in the Authorization header:</p>
<pre>Authorization: Bearer &lt;your-access-token&gt;</pre>

<h3>Token Expiration</h3>
<p>Tokens expire after a set period. Your application should handle token refresh.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "api-auth-request",
                            Title = "Token Request",
                            Description = "Request an access token using client credentials.",
                            Type = ExampleType.Code,
                            ExampleContent = @"POST https://api.cms.optimizely.com/oauth/token
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials
&client_id=YOUR_CLIENT_ID
&client_secret=YOUR_CLIENT_SECRET",
                            SampleResponse = @"{
  ""access_token"": ""eyJhbGciOiJSUzI1NiIsInR5cCI..."",
  ""token_type"": ""Bearer"",
  ""expires_in"": 3600,
  ""scope"": ""api:admin""
}",
                            Hints = new List<string>
                            {
                                "Store tokens securely, never in client-side code",
                                "Implement token refresh before expiration"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "api-content-types",
                    ModuleId = "rest-api",
                    Title = "Managing Content Types",
                    Summary = "Create and manage content types via the REST API.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create content types via API",
                        "Update existing content types",
                        "Manage properties and validation"
                    },
                    Content = @"
<h2>Content Type Management</h2>
<p>The REST API allows you to programmatically manage content types, enabling automation and version control of your content model.</p>

<h3>API Endpoint</h3>
<pre>https://api.cms.optimizely.com/preview3/contenttypes</pre>

<h3>Operations</h3>
<ul>
    <li><strong>GET</strong> - List or retrieve content types</li>
    <li><strong>POST</strong> - Create new content types</li>
    <li><strong>PATCH</strong> - Update existing content types</li>
    <li><strong>DELETE</strong> - Remove content types</li>
</ul>

<h3>Creating a Content Type</h3>
<p>Send a POST request with the content type definition:</p>
<ul>
    <li><code>key</code> - Unique identifier</li>
    <li><code>baseType</code> - Base type to inherit from</li>
    <li><code>displayName</code> - UI display name</li>
    <li><code>properties</code> - Property definitions</li>
</ul>

<h3>Updating Content Types</h3>
<p>Use PATCH with JSON Merge Patch format. Only include changed properties:</p>
<ul>
    <li>Set a value to update it</li>
    <li>Set a value to <code>null</code> to remove it</li>
    <li>Omit properties to leave unchanged</li>
</ul>

<h3>Concurrency Control</h3>
<p>Use ETags for optimistic locking:</p>
<ol>
    <li>Get the ETag from a GET response</li>
    <li>Include <code>If-Match: ""etag-value""</code> in PATCH/DELETE</li>
    <li>Handle 412 Precondition Failed for conflicts</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "api-create-content-type",
                            Title = "Create Content Type",
                            Description = "Create a new page content type via REST API.",
                            Type = ExampleType.Code,
                            ExampleContent = @"POST https://api.cms.optimizely.com/preview3/contenttypes
Authorization: Bearer <token>
Content-Type: application/json

{
  ""key"": ""blogPost"",
  ""baseType"": ""_page"",
  ""displayName"": ""Blog Post"",
  ""description"": ""A blog post with author and categories"",
  ""properties"": {
    ""title"": {
      ""type"": ""string"",
      ""displayName"": ""Title"",
      ""required"": true,
      ""indexingType"": ""searchable""
    },
    ""author"": {
      ""type"": ""string"",
      ""displayName"": ""Author Name""
    },
    ""content"": {
      ""type"": ""richText"",
      ""displayName"": ""Content"",
      ""indexingType"": ""searchable""
    },
    ""publishDate"": {
      ""type"": ""dateTime"",
      ""displayName"": ""Publish Date""
    },
    ""featuredImage"": {
      ""type"": ""contentReference"",
      ""displayName"": ""Featured Image"",
      ""allowedTypes"": [""_image""]
    }
  }
}",
                            SampleResponse = @"HTTP/1.1 201 Created
Location: /preview3/contenttypes/blogPost
ETag: ""abc123""

{
  ""key"": ""blogPost"",
  ""baseType"": ""_page"",
  ""displayName"": ""Blog Post"",
  ...
}",
                            Hints = new List<string>
                            {
                                "Save the ETag for future update operations",
                                "Content types can't be renamed - delete and recreate instead"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "api-content-crud",
                    ModuleId = "rest-api",
                    Title = "Content CRUD Operations",
                    Summary = "Create, read, update, and delete content items.",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Create content items via API",
                        "Retrieve and query content",
                        "Update and delete content"
                    },
                    Content = @"
<h2>Content Operations</h2>
<p>The Content API allows full CRUD operations on content items within your CMS.</p>

<h3>API Endpoint</h3>
<pre>https://api.cms.optimizely.com/preview3/experimental/content</pre>

<h3>Creating Content</h3>
<p>POST request with content data:</p>
<ul>
    <li><code>key</code> - UUID for the content item</li>
    <li><code>contentType</code> - Type of content to create</li>
    <li><code>locale</code> - Language code (e.g., ""en"")</li>
    <li><code>container</code> - Parent container UUID</li>
    <li><code>status</code> - ""draft"" or ""published""</li>
    <li><code>displayName</code> - Name shown in the tree</li>
    <li><code>properties</code> - Content values</li>
</ul>

<h3>Reading Content</h3>
<p>GET request with optional parameters:</p>
<ul>
    <li>By key: <code>/content/{key}</code></li>
    <li>With locale: <code>?locale=en</code></li>
    <li>With version: <code>?version=draft</code></li>
</ul>

<h3>Updating Content</h3>
<p>PATCH request with changed properties only:</p>
<ul>
    <li>Include <code>If-Match</code> header with ETag</li>
    <li>Use <code>application/merge-patch+json</code> content type</li>
</ul>

<h3>Publishing Content</h3>
<p>Special endpoint for publishing:</p>
<pre>POST /content/{key}:publish</pre>

<h3>Deleting Content</h3>
<p>DELETE request removes content:</p>
<ul>
    <li>Include ETag for concurrency</li>
    <li>Consider soft-delete workflows</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "api-create-content",
                            Title = "Create Content Item",
                            Description = "Create and publish a blog post.",
                            Type = ExampleType.Code,
                            ExampleContent = @"POST https://api.cms.optimizely.com/preview3/experimental/content
Authorization: Bearer <token>
Content-Type: application/json

{
  ""key"": ""019003fe597f70c8b9b5f6231c74ed96"",
  ""contentType"": ""blogPost"",
  ""locale"": ""en"",
  ""container"": ""43f936c99b234ea397b261c538ad07c9"",
  ""status"": ""published"",
  ""displayName"": ""Getting Started with Headless CMS"",
  ""properties"": {
    ""title"": ""Getting Started with Headless CMS"",
    ""author"": ""Jane Developer"",
    ""content"": ""<p>Welcome to our guide...</p>"",
    ""publishDate"": ""2024-01-15T10:00:00Z""
  }
}",
                            SampleResponse = @"HTTP/1.1 201 Created
Location: /preview3/experimental/content/019003fe597f70c8b9b5f6231c74ed96

{
  ""key"": ""019003fe597f70c8b9b5f6231c74ed96"",
  ""contentType"": ""blogPost"",
  ""locale"": ""en"",
  ""status"": ""published"",
  ...
}",
                            Hints = new List<string>
                            {
                                "The container is the parent folder UUID - find it in Settings",
                                "UUIDs should have hyphens removed when used in queries"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 6: Graph Integration

    private LearningModule BuildGraphIntegrationModule()
    {
        return new LearningModule
        {
            Id = "graph-integration",
            Title = "Content Delivery with Graph",
            Description = "Learn to deliver CMS content through Optimizely Graph.",
            Icon = "bolt",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "visual-builder-essentials" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gi-overview",
                    ModuleId = "graph-integration",
                    Title = "Connecting SaaS to Graph",
                    Summary = "Understand how CMS (SaaS) content flows to Optimizely Graph.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the content indexing process",
                        "Know how content types map to Graph",
                        "Configure Graph synchronization"
                    },
                    Content = @"
<h2>CMS to Graph Integration</h2>
<p>Optimizely Graph is the recommended way to <strong>deliver content</strong> from CMS (SaaS) to your frontend applications. Content is automatically indexed in Graph for efficient querying.</p>

<h3>How Content Flows to Graph</h3>
<ol>
    <li>Content is created/edited in CMS</li>
    <li>On publish, content is indexed in Graph</li>
    <li>Graph schema is automatically updated for new types</li>
    <li>Frontend queries Graph for content</li>
</ol>

<h3>Content Type Mapping</h3>
<p>Each CMS content type gets a corresponding Graph type:</p>
<ul>
    <li>Page types become queryable at their type name</li>
    <li>Components register as both standalone and with ""Property"" suffix</li>
    <li>Standard fields are available on all types</li>
</ul>

<h3>Synchronization</h3>
<p>Content sync happens automatically, but you can trigger manual sync:</p>
<ul>
    <li>Navigate to Scheduled Jobs</li>
    <li>Run ""Optimizely Graph Full Synchronization""</li>
    <li>Use after bulk changes or configuration updates</li>
</ul>

<h3>Graph Schema</h3>
<p>The Graph schema reflects your content model:</p>
<ul>
    <li>Each content type is a GraphQL type</li>
    <li>Properties become fields</li>
    <li>References resolve to related content</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "gi-indexing",
                    ModuleId = "graph-integration",
                    Title = "Content Indexing Configuration",
                    Summary = "Configure how content properties are indexed in Graph.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand indexing types",
                        "Configure property indexing",
                        "Control what content appears in Graph"
                    },
                    Content = @"
<h2>Indexing Configuration</h2>
<p>Control how your content properties are indexed in Graph to optimize query performance and search capabilities.</p>

<h3>Indexing Types</h3>
<p>Properties support four indexing levels:</p>

<h4>Default</h4>
<p>Property is indexed but:</p>
<ul>
    <li>Cannot be used for filtering</li>
    <li>Cannot be used for sorting</li>
    <li>Not included in full-text search</li>
</ul>
<p><em>Use for: Properties only needed in output</em></p>

<h4>Queryable</h4>
<p>Enhanced indexing that allows:</p>
<ul>
    <li>Filtering in where clauses</li>
    <li>Sorting in orderBy</li>
    <li>Not in full-text search</li>
</ul>
<p><em>Use for: Properties used to filter/sort (dates, categories)</em></p>

<h4>Searchable</h4>
<p>Full indexing that enables:</p>
<ul>
    <li>Filtering and sorting</li>
    <li>Full-text search inclusion</li>
    <li>Relevance scoring</li>
</ul>
<p><em>Use for: Content that should be found via search (titles, body text)</em></p>

<h4>Disabled</h4>
<p>Property is excluded from Graph entirely:</p>
<ul>
    <li>Not queryable</li>
    <li>Not returned in results</li>
</ul>
<p><em>Use for: Internal properties, sensitive data</em></p>

<h3>Access Control</h3>
<p>The <strong>SearchIndexer</strong> role determines what gets indexed. Remove Read access for this role on specific content to exclude it from Graph.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "gi-indexing-config",
                            Title = "Indexing Configuration Example",
                            Description = "Configure indexing types for different properties.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""properties"": {
    ""title"": {
      ""type"": ""string"",
      ""displayName"": ""Title"",
      ""indexingType"": ""searchable""
    },
    ""category"": {
      ""type"": ""string"",
      ""displayName"": ""Category"",
      ""indexingType"": ""queryable""
    },
    ""viewCount"": {
      ""type"": ""integer"",
      ""displayName"": ""View Count"",
      ""indexingType"": ""default""
    },
    ""internalNotes"": {
      ""type"": ""string"",
      ""displayName"": ""Internal Notes"",
      ""indexingType"": ""disabled""
    }
  }
}",
                            SampleResponse = @"Indexing Configuration:
- title: Full-text searchable, can filter/sort
- category: Can filter and sort, not in search
- viewCount: Output only, no filtering
- internalNotes: Excluded from Graph",
                            Hints = new List<string>
                            {
                                "Searchable indexing has higher storage/compute cost",
                                "Use queryable for enum-like values used in filters"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "gi-querying",
                    ModuleId = "graph-integration",
                    Title = "Querying CMS Content",
                    Summary = "Build effective GraphQL queries for CMS content.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Write queries for CMS content types",
                        "Use filtering and sorting",
                        "Handle content references"
                    },
                    Content = @"
<h2>Querying CMS Content</h2>
<p>With content indexed in Graph, you can build powerful queries to retrieve exactly what your frontend needs.</p>

<h3>Basic Query Structure</h3>
<p>Each content type is queryable by name:</p>
<pre>
{
  BlogPost {
    items {
      title
      author
      publishDate
    }
  }
}
</pre>

<h3>Filtering</h3>
<p>Use the <code>where</code> clause for filtering:</p>
<ul>
    <li>Equality: <code>status: { eq: ""published"" }</code></li>
    <li>Contains: <code>title: { contains: ""guide"" }</code></li>
    <li>Date ranges: <code>publishDate: { gte: ""2024-01-01"" }</code></li>
    <li>In list: <code>category: { in: [""News"", ""Blog""] }</code></li>
</ul>

<h3>Sorting</h3>
<p>Use <code>orderBy</code> to control result order:</p>
<pre>orderBy: { publishDate: DESC }</pre>

<h3>Pagination</h3>
<p>Use <code>limit</code> and <code>skip</code> for pagination:</p>
<pre>limit: 10, skip: 20</pre>

<h3>Content References</h3>
<p>Expand references to get related content:</p>
<pre>
featuredImage {
  url
  altText
}
</pre>

<h3>Localization</h3>
<p>Query specific locales:</p>
<pre>locale: { eq: ""en"" }</pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "gi-query-example",
                            Title = "Blog Post Query",
                            Description = "Query blog posts with filtering, sorting, and references.",
                            Type = ExampleType.Query,
                            ExampleContent = @"{
  BlogPost(
    where: {
      _and: [
        { status: { eq: ""published"" } }
        { publishDate: { lte: ""2024-12-31"" } }
      ]
    }
    orderBy: { publishDate: DESC }
    limit: 10
    locale: en
  ) {
    items {
      _metadata {
        key
        url {
          default
        }
      }
      title
      author
      publishDate
      content {
        html
      }
      featuredImage {
        url {
          default
        }
      }
    }
    total
  }
}",
                            SampleResponse = @"{
  ""data"": {
    ""BlogPost"": {
      ""items"": [
        {
          ""_metadata"": {
            ""key"": ""abc123"",
            ""url"": { ""default"": ""/blog/headless-cms-guide"" }
          },
          ""title"": ""Getting Started with Headless CMS"",
          ""author"": ""Jane Developer"",
          ""publishDate"": ""2024-01-15T10:00:00Z"",
          ""content"": { ""html"": ""<p>Welcome...</p>"" },
          ""featuredImage"": {
            ""url"": { ""default"": ""/media/hero.jpg"" }
          }
        }
      ],
      ""total"": 42
    }
  }
}",
                            Hints = new List<string>
                            {
                                "_metadata provides system fields like URL and key",
                                "Use total for pagination calculations"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "gi-recursive-queries",
                    ModuleId = "graph-integration",
                    Title = "Explicit vs Recursive Queries",
                    Summary = "Master different approaches for querying nested Visual Builder content.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Choose the right query approach",
                        "Use the @recursive directive",
                        "Handle complex compositions"
                    },
                    Content = @"
<h2>Query Approaches for Visual Builder</h2>
<p>Visual Builder content creates nested structures that require special query techniques.</p>

<h3>Explicit Queries</h3>
<p>Define each level of nesting explicitly:</p>
<ul>
    <li>Best when structure depth is known</li>
    <li>More verbose but precise control</li>
    <li>Easier to understand and debug</li>
</ul>

<h3>Recursive Queries</h3>
<p>Use GraphQL's <code>@recursive</code> directive:</p>
<ul>
    <li>Handles variable-depth structures</li>
    <li>More concise query syntax</li>
    <li>Useful for deeply nested content</li>
</ul>

<h3>When to Use Each</h3>

<h4>Use Explicit When:</h4>
<ul>
    <li>Structure is fixed and known</li>
    <li>You need precise control over fields</li>
    <li>Performance optimization is needed</li>
</ul>

<h4>Use Recursive When:</h4>
<ul>
    <li>Structure depth varies</li>
    <li>Content managers can create arbitrary nesting</li>
    <li>You want simpler query maintenance</li>
</ul>

<h3>Performance Considerations</h3>
<ul>
    <li>Limit recursion depth when possible</li>
    <li>Be mindful of query complexity</li>
    <li>Cache results appropriately</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "gi-recursive-query",
                            Title = "Recursive Composition Query",
                            Description = "Use @recursive to query nested Visual Builder structures.",
                            Type = ExampleType.Query,
                            ExampleContent = @"{
  LandingPageExperience {
    items {
      Name
      composition {
        ... CompositionFields @recursive(depth: 10)
      }
    }
  }
}

fragment CompositionFields on CompositionNode {
  type
  nodeType
  key
  ... on CompositionStructureNode {
    nodes {
      ... CompositionFields
    }
  }
  ... on CompositionComponentNode {
    component {
      ... on HeadingElement {
        text
        level
      }
      ... on ButtonElement {
        text
        url
      }
    }
  }
}",
                            SampleResponse = @"The @recursive directive automatically
expands nested structures up to 10 levels deep,
returning all composition nodes with their
component data in a single query.",
                            Hints = new List<string>
                            {
                                "Fragments keep recursive queries maintainable",
                                "Set depth limit to prevent excessive nesting"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 7: Access Rights & Administration

    private LearningModule BuildAccessRightsModule()
    {
        return new LearningModule
        {
            Id = "access-rights",
            Title = "Access Rights & Administration",
            Description = "Learn to configure permissions and manage users in CMS (SaaS).",
            Icon = "shield-check",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ar-permissions",
                    ModuleId = "access-rights",
                    Title = "Understanding Permissions",
                    Summary = "Learn the six permission types and how they control access.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Know all six permission types",
                        "Understand permission inheritance",
                        "Apply appropriate permissions"
                    },
                    Content = @"
<h2>Permission Types</h2>
<p>CMS (SaaS) uses six permission levels to control what users can do with content.</p>

<h3>The Six Permissions</h3>

<h4>Read</h4>
<p>View content as a reader. Without Read permission, content is invisible to the user.</p>

<h4>Create</h4>
<p>Generate new content under a content item. Users need Create on the parent to add children.</p>

<h4>Change</h4>
<p>Modify existing content. Allows editing but not publishing or deleting.</p>

<h4>Delete</h4>
<p>Remove content permanently. Use carefully - deleted content may not be recoverable.</p>

<h4>Publish</h4>
<p>Make content live. Essential for content to appear on the public site.</p>

<h4>Administer</h4>
<p>Full control including:</p>
<ul>
    <li>Create approval sequences</li>
    <li>Set access rights on the item</li>
    <li>Manage language properties</li>
</ul>

<h3>Permission Inheritance</h3>
<p>Permissions cascade down the content tree:</p>
<ul>
    <li>Child items inherit parent permissions by default</li>
    <li>You can break inheritance on any item</li>
    <li>Explicit permissions override inherited ones</li>
</ul>

<h3>Permission Combinations</h3>
<p>Common combinations:</p>
<ul>
    <li><strong>Viewer</strong>: Read only</li>
    <li><strong>Editor</strong>: Read, Create, Change</li>
    <li><strong>Publisher</strong>: Read, Create, Change, Publish</li>
    <li><strong>Full Control</strong>: All permissions</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "ar-user-groups",
                    ModuleId = "access-rights",
                    Title = "Built-in User Groups",
                    Summary = "Understand the default user groups and their roles.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Know the built-in groups",
                        "Understand each group's purpose",
                        "Assign users appropriately"
                    },
                    Content = @"
<h2>Default User Groups</h2>
<p>CMS (SaaS) includes four built-in groups with predefined permission sets.</p>

<h3>Administrators</h3>
<p>Full system access for developers and technical admins:</p>
<ul>
    <li>All permissions on all content</li>
    <li>System configuration access</li>
    <li>Content type management</li>
    <li>API key management</li>
</ul>

<h3>Content Admins</h3>
<p>Administrative functions without editing:</p>
<ul>
    <li>Manage settings and configuration</li>
    <li>Set up access rights</li>
    <li>Configure languages and workflows</li>
    <li>No direct content editing</li>
</ul>

<h3>Content Editors</h3>
<p>Day-to-day content work:</p>
<ul>
    <li>Create and edit content</li>
    <li>Publish content</li>
    <li>Manage assets</li>
    <li>Use Visual Builder</li>
</ul>

<h3>Everyone</h3>
<p>Anonymous/public access:</p>
<ul>
    <li>Read access to published content</li>
    <li>Used for public website visitors</li>
    <li>No editing capabilities</li>
</ul>

<h3>Group Best Practices</h3>
<ul>
    <li>Assign users to groups rather than individual permissions</li>
    <li>Create custom groups for specific needs</li>
    <li>Keep Administrator membership limited</li>
    <li>Document group purposes</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "ar-custom-roles",
                    ModuleId = "access-rights",
                    Title = "Creating Custom Roles",
                    Summary = "Build custom roles for specific organizational needs.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create roles in Opti ID Admin Center",
                        "Configure role attributes",
                        "Sync roles with CMS"
                    },
                    Content = @"
<h2>Custom Role Management</h2>
<p>When built-in groups don't fit your needs, create custom roles in the Opti ID Admin Center.</p>

<h3>Creating a Role</h3>
<ol>
    <li>Access the Opti ID Admin Center</li>
    <li>Navigate to Roles management</li>
    <li>Click Create New Role</li>
    <li>Configure:
        <ul>
            <li>Role name</li>
            <li>Description (optional)</li>
            <li>Product: Optimizely CMS</li>
            <li>Target instances</li>
        </ul>
    </li>
    <li>Save the role</li>
</ol>

<h3>Assigning Roles</h3>
<p>Roles can be assigned:</p>
<ul>
    <li><strong>To individuals</strong> - Direct assignment in Opti ID</li>
    <li><strong>To groups</strong> - Assign role to a group, all members inherit</li>
</ul>

<h3>Role Syncing</h3>
<p>Roles sync to CMS when users authenticate. The sync process:</p>
<ol>
    <li>User logs into CMS</li>
    <li>Opti ID validates credentials</li>
    <li>User's roles are synced</li>
    <li>Permissions apply immediately</li>
</ol>

<h3>Role Strategy</h3>
<ul>
    <li>Group-based assignment simplifies management</li>
    <li>Create roles for job functions, not individuals</li>
    <li>Document the purpose of each custom role</li>
    <li>Review role assignments periodically</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "ar-configuration",
                    ModuleId = "access-rights",
                    Title = "Configuring Access Rights",
                    Summary = "Set up access rights on content and configure language permissions.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure access rights on content",
                        "Set up language-specific permissions",
                        "Manage inheritance settings"
                    },
                    Content = @"
<h2>Configuring Access Rights</h2>
<p>Administrators manage permissions through the Settings interface or directly on content items.</p>

<h3>Via Settings</h3>
<ol>
    <li>Navigate to <strong>Settings > Set Access Rights</strong></li>
    <li>Select a content item in the tree</li>
    <li>Configure permissions:
        <ul>
            <li>Add/remove user groups</li>
            <li>Set permission levels</li>
            <li>Control inheritance</li>
        </ul>
    </li>
    <li>Apply to subitems if needed</li>
</ol>

<h3>Via Content Context Menu</h3>
<p>Editors with Administer rights can:</p>
<ol>
    <li>Select content in the tree</li>
    <li>Open Publish Changes menu</li>
    <li>Access Set Access Rights</li>
    <li>Configure for that item only</li>
</ol>

<h3>Inheritance Options</h3>
<ul>
    <li><strong>Inherit from parent</strong> - Use parent's permissions</li>
    <li><strong>Break inheritance</strong> - Set custom permissions</li>
    <li><strong>Apply to subitems</strong> - Push changes down the tree</li>
</ul>

<h3>Language-Specific Access</h3>
<p>Configure in <strong>Settings > Manage Website Languages</strong>:</p>
<ul>
    <li>Enable languages for the site</li>
    <li>Assign language permissions to groups</li>
    <li>Users only see languages they have access to</li>
</ul>

<h3>Graph Indexing Access</h3>
<p>The <strong>SearchIndexer</strong> role controls what appears in Graph:</p>
<ul>
    <li>Remove Read for SearchIndexer to exclude content</li>
    <li>Useful for internal or draft content</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 8: Advanced Topics & Best Practices

    private LearningModule BuildAdvancedTopicsModule()
    {
        return new LearningModule
        {
            Id = "advanced-topics",
            Title = "Advanced Topics & Best Practices",
            Description = "Master advanced concepts and learn best practices for CMS (SaaS) implementations.",
            Icon = "rocket-launch",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "rest-api", "graph-integration", "access-rights" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "adv-multisite",
                    ModuleId = "advanced-topics",
                    Title = "Multi-site Configuration",
                    Summary = "Configure multiple websites within a single CMS instance.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand multi-site architecture",
                        "Configure multiple websites",
                        "Manage hosts and domains"
                    },
                    Content = @"
<h2>Multi-site Configuration</h2>
<p>CMS (SaaS) supports running multiple websites from a single instance, sharing content and resources while maintaining separate identities.</p>

<h3>Multi-site Benefits</h3>
<ul>
    <li>Share content types across sites</li>
    <li>Reuse assets and components</li>
    <li>Centralized management</li>
    <li>Cost-effective scaling</li>
</ul>

<h3>Configuration Steps</h3>
<ol>
    <li>Navigate to <strong>Settings > Manage Websites</strong></li>
    <li>Create a new website entry</li>
    <li>Configure:
        <ul>
            <li>Website name</li>
            <li>Start page</li>
            <li>URL structure</li>
        </ul>
    </li>
    <li>Add host configurations</li>
</ol>

<h3>Host Configuration</h3>
<p>Each site can have multiple hosts:</p>
<ul>
    <li><strong>Production host</strong> - Live site URL</li>
    <li><strong>Preview host</strong> - Editor preview URL</li>
    <li><strong>Edit host</strong> - CMS edit mode URL</li>
</ul>

<h3>Content Sharing</h3>
<p>Sites can share content through:</p>
<ul>
    <li>Global content folders</li>
    <li>Shared assets library</li>
    <li>Cross-site references</li>
</ul>

<h3>Considerations</h3>
<ul>
    <li>Plan content structure carefully</li>
    <li>Define clear ownership</li>
    <li>Consider SEO implications</li>
    <li>Manage permissions per site</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "adv-sync-jobs",
                    ModuleId = "advanced-topics",
                    Title = "Content Synchronization",
                    Summary = "Understand and manage content sync between CMS and Graph.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand sync processes",
                        "Run synchronization jobs",
                        "Troubleshoot sync issues"
                    },
                    Content = @"
<h2>Content Synchronization</h2>
<p>Content synchronization ensures your content in CMS is properly indexed and available in Optimizely Graph.</p>

<h3>Automatic Sync</h3>
<p>Content is automatically synced when:</p>
<ul>
    <li>Content is published</li>
    <li>Content is unpublished</li>
    <li>Content is deleted</li>
</ul>

<h3>Manual Sync Jobs</h3>
<p>Run full sync through Scheduled Jobs:</p>
<ul>
    <li><strong>Optimizely Graph Full Synchronization</strong> - Re-indexes all content</li>
</ul>

<h3>When to Run Manual Sync</h3>
<ul>
    <li>After bulk content imports</li>
    <li>After content type changes</li>
    <li>After configuration updates</li>
    <li>When troubleshooting missing content</li>
</ul>

<h3>Sync Troubleshooting</h3>
<p>Content not appearing in Graph? Check:</p>
<ol>
    <li>Content is published, not draft</li>
    <li>SearchIndexer has Read access</li>
    <li>Content type is not excluded</li>
    <li>Indexing type is not ""disabled""</li>
    <li>Run full synchronization</li>
</ol>

<h3>Sync Performance</h3>
<ul>
    <li>Full sync can take time for large sites</li>
    <li>Run during off-peak hours if possible</li>
    <li>Monitor sync job status</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "adv-performance",
                    ModuleId = "advanced-topics",
                    Title = "Performance Optimization",
                    Summary = "Optimize your CMS (SaaS) implementation for best performance.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Optimize content model design",
                        "Improve query performance",
                        "Implement caching strategies"
                    },
                    Content = @"
<h2>Performance Optimization</h2>
<p>Optimize your CMS (SaaS) implementation for the best user experience and efficient resource usage.</p>

<h3>Content Model Optimization</h3>
<ul>
    <li><strong>Keep content types focused</strong> - Don't overload with properties</li>
    <li><strong>Use appropriate indexing</strong> - Only ""searchable"" when needed</li>
    <li><strong>Normalize references</strong> - Avoid deep nesting</li>
</ul>

<h3>Query Optimization</h3>
<ul>
    <li><strong>Request only needed fields</strong> - Don't over-fetch</li>
    <li><strong>Use pagination</strong> - Limit result sets</li>
    <li><strong>Apply filters early</strong> - Reduce data processing</li>
    <li><strong>Avoid n+1 queries</strong> - Fetch related content in one query</li>
</ul>

<h3>Caching Strategies</h3>
<ul>
    <li><strong>CDN caching</strong> - Cache Graph responses at edge</li>
    <li><strong>Application caching</strong> - Cache in your frontend app</li>
    <li><strong>Cache invalidation</strong> - Refresh on content changes</li>
</ul>

<h3>Visual Builder Performance</h3>
<ul>
    <li>Keep section nesting reasonable</li>
    <li>Optimize element complexity</li>
    <li>Limit elements per page</li>
</ul>

<h3>Image Optimization</h3>
<ul>
    <li>Use appropriate image sizes</li>
    <li>Leverage image transformation URLs</li>
    <li>Implement lazy loading</li>
</ul>

<h3>API Rate Limiting</h3>
<p>Remember the limit: 100 requests per 10 seconds per IP</p>
<ul>
    <li>Batch operations when possible</li>
    <li>Implement request queuing</li>
    <li>Handle 429 responses gracefully</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "adv-migration",
                    ModuleId = "advanced-topics",
                    Title = "Migration to CMS (SaaS)",
                    Summary = "Plan and execute a migration from CMS 12 to CMS (SaaS).",
                    Order = 4,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Understand migration considerations",
                        "Plan a migration strategy",
                        "Execute content migration"
                    },
                    Content = @"
<h2>Migration to CMS (SaaS)</h2>
<p>Migrating from CMS 12 (PaaS) to CMS (SaaS) requires careful planning and execution.</p>

<h3>Key Differences</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">CMS 12 (PaaS)</th>
            <th class=""px-4 py-2 text-left"">CMS (SaaS)</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Hosting</td><td class=""px-4 py-2"">Self-managed</td><td class=""px-4 py-2"">Optimizely-managed</td></tr>
        <tr><td class=""px-4 py-2"">Content Types</td><td class=""px-4 py-2"">C# code</td><td class=""px-4 py-2"">JSON definitions</td></tr>
        <tr><td class=""px-4 py-2"">Rendering</td><td class=""px-4 py-2"">Server-side</td><td class=""px-4 py-2"">Headless/API-based</td></tr>
        <tr><td class=""px-4 py-2"">Customization</td><td class=""px-4 py-2"">Full code access</td><td class=""px-4 py-2"">Configuration/API</td></tr>
    </tbody>
</table>

<h3>Migration Planning</h3>
<ol>
    <li><strong>Audit existing content</strong>
        <ul>
            <li>Content types and properties</li>
            <li>Content volume and complexity</li>
            <li>Custom functionality</li>
        </ul>
    </li>
    <li><strong>Design new content model</strong>
        <ul>
            <li>Map C# types to JSON definitions</li>
            <li>Plan Visual Builder structure</li>
            <li>Identify customization gaps</li>
        </ul>
    </li>
    <li><strong>Plan frontend rebuild</strong>
        <ul>
            <li>Choose frontend framework</li>
            <li>Design Graph queries</li>
            <li>Implement rendering components</li>
        </ul>
    </li>
</ol>

<h3>Content Migration</h3>
<ul>
    <li>Export content from CMS 12</li>
    <li>Transform to SaaS format</li>
    <li>Import via REST API</li>
    <li>Verify and validate</li>
</ul>

<h3>Gradual Migration</h3>
<p>Consider a phased approach:</p>
<ol>
    <li>Start with new content in SaaS</li>
    <li>Migrate section by section</li>
    <li>Maintain both systems during transition</li>
    <li>Full cutover when ready</li>
</ol>

<h3>Migration Tools</h3>
<ul>
    <li>REST API for content type creation</li>
    <li>Content API for content import</li>
    <li>Custom scripts for transformation</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 9: Workflows & Publishing

    private LearningModule BuildWorkflowsPublishingModule()
    {
        return new LearningModule
        {
            Id = "workflows-publishing",
            Title = "Workflows & Publishing",
            Description = "Master content workflows, approval processes, scheduling, and publishing in Optimizely CMS (SaaS).",
            Icon = "arrow-path",
            Order = 9,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started", "content-modeling" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "wp-content-states",
                    ModuleId = "workflows-publishing",
                    Title = "Understanding Content States",
                    Summary = "Learn about the different states content can be in and how transitions work.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the content lifecycle states",
                        "Learn how content transitions between states",
                        "Identify when content becomes visible to visitors"
                    },
                    Content = @"
<h2>Content States in CMS (SaaS)</h2>
<p>Content in Optimizely CMS (SaaS) moves through various states during its lifecycle. Understanding these states is crucial for managing content effectively.</p>

<h3>Primary Content States</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">State</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Visible to Visitors</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Draft</td><td class=""px-4 py-2"">Work in progress, not yet ready for review</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Ready for Review</td><td class=""px-4 py-2"">Content is complete and awaiting approval</td><td class=""px-4 py-2"">No</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Published</td><td class=""px-4 py-2"">Content is live and visible</td><td class=""px-4 py-2"">Yes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Scheduled</td><td class=""px-4 py-2"">Approved but waiting for publish date</td><td class=""px-4 py-2"">No (until date)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Expired</td><td class=""px-4 py-2"">Past its unpublish date</td><td class=""px-4 py-2"">No</td></tr>
    </tbody>
</table>

<h3>State Transitions</h3>
<p>Content moves through states based on user actions and system rules:</p>
<ul>
    <li><strong>Draft → Ready for Review</strong> - Author submits for approval</li>
    <li><strong>Ready for Review → Published</strong> - Approver publishes content</li>
    <li><strong>Ready for Review → Draft</strong> - Approver requests changes</li>
    <li><strong>Published → Draft</strong> - Creating a new draft version</li>
    <li><strong>Published → Expired</strong> - Unpublish date reached</li>
</ul>

<h3>Version Handling</h3>
<p>When you edit published content, a new draft version is created while the published version remains visible. This allows you to:</p>
<ul>
    <li>Make changes without affecting the live site</li>
    <li>Preview changes before publishing</li>
    <li>Collaborate on updates safely</li>
    <li>Roll back to previous versions if needed</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Content is only indexed in Optimizely Graph when it reaches the Published state. Draft and review content is not available for frontend queries.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "wp-approval-workflows",
                    ModuleId = "workflows-publishing",
                    Title = "Approval Workflows",
                    Summary = "Configure and use approval workflows for content governance.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Set up approval workflows for content",
                        "Understand single and multi-step approvals",
                        "Configure notifications for workflow participants"
                    },
                    Content = @"
<h2>Approval Workflows</h2>
<p>Approval workflows ensure content quality and compliance by requiring review before publication. CMS (SaaS) supports flexible workflow configurations.</p>

<h3>Workflow Types</h3>

<h4>Simple Approval</h4>
<p>A single approver reviews and publishes content:</p>
<ol>
    <li>Author creates content and marks as ""Ready for Review""</li>
    <li>Approver receives notification</li>
    <li>Approver reviews and either publishes or requests changes</li>
</ol>

<h4>Sequential Approval</h4>
<p>Multiple approvers review in a specific order:</p>
<ol>
    <li>Author submits content</li>
    <li>First approver (e.g., Editor) reviews</li>
    <li>Second approver (e.g., Legal) reviews</li>
    <li>Final approver (e.g., Publisher) publishes</li>
</ol>

<h3>Configuring Workflows</h3>
<p>Workflows are configured based on:</p>
<ul>
    <li><strong>Content Type</strong> - Different types may require different workflows</li>
    <li><strong>Content Location</strong> - Sections of the site may have unique requirements</li>
    <li><strong>User Roles</strong> - Permissions determine who can approve</li>
</ul>

<h3>Workflow Notifications</h3>
<p>Keep stakeholders informed with automatic notifications:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Event</th>
            <th class=""px-4 py-2 text-left"">Recipients</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Content submitted for review</td><td class=""px-4 py-2"">Assigned approvers</td></tr>
        <tr><td class=""px-4 py-2"">Changes requested</td><td class=""px-4 py-2"">Content author</td></tr>
        <tr><td class=""px-4 py-2"">Content approved</td><td class=""px-4 py-2"">Author, stakeholders</td></tr>
        <tr><td class=""px-4 py-2"">Content published</td><td class=""px-4 py-2"">Author, stakeholders</td></tr>
    </tbody>
</table>

<h3>Best Practices</h3>
<ul>
    <li>Keep workflows as simple as possible while meeting compliance needs</li>
    <li>Define clear approval criteria for reviewers</li>
    <li>Set reasonable SLAs for approval turnaround</li>
    <li>Use role-based assignments rather than specific users</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "wp-workflow-config",
                            Title = "Workflow Configuration Example",
                            Description = "Example of a multi-step approval workflow configuration",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""workflow"": {
    ""name"": ""Marketing Content Approval"",
    ""contentTypes"": [""MarketingPage"", ""BlogPost""],
    ""steps"": [
      {
        ""name"": ""Editorial Review"",
        ""approvers"": [""EditorRole""],
        ""required"": true
      },
      {
        ""name"": ""Legal Compliance"",
        ""approvers"": [""LegalRole""],
        ""required"": true,
        ""conditions"": {
          ""hasDisclaimer"": true
        }
      },
      {
        ""name"": ""Final Approval"",
        ""approvers"": [""PublisherRole""],
        ""required"": true
      }
    ]
  }
}",
                            Hints = new List<string>
                            {
                                "Conditional steps only trigger when conditions are met",
                                "Use role-based approvers for flexibility",
                                "Mark critical steps as required"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "wp-scheduling",
                    ModuleId = "workflows-publishing",
                    Title = "Scheduled Publishing",
                    Summary = "Learn to schedule content for future publication and expiration.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Schedule content for future publication",
                        "Set content expiration dates",
                        "Manage time-sensitive content campaigns"
                    },
                    Content = @"
<h2>Scheduled Publishing</h2>
<p>Scheduled publishing allows you to prepare content in advance and have it automatically go live at a specified time. This is essential for time-sensitive campaigns, announcements, and coordinated releases.</p>

<h3>Setting a Publish Date</h3>
<p>When publishing content, you can specify a future date and time:</p>
<ol>
    <li>Complete your content and mark it ready for review</li>
    <li>During approval, select ""Schedule for later""</li>
    <li>Choose the publish date and time</li>
    <li>Select the appropriate timezone</li>
    <li>Confirm the scheduled publication</li>
</ol>

<h3>Setting an Expiration Date</h3>
<p>Content can also be set to automatically expire:</p>
<ul>
    <li><strong>Unpublish on date</strong> - Content is removed from the live site</li>
    <li><strong>Archive on date</strong> - Content is moved to archive state</li>
    <li><strong>Delete on date</strong> - Content is permanently removed (use with caution)</li>
</ul>

<h3>Common Use Cases</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Scenario</th>
            <th class=""px-4 py-2 text-left"">Publish</th>
            <th class=""px-4 py-2 text-left"">Expire</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Product launch</td><td class=""px-4 py-2"">Launch date/time</td><td class=""px-4 py-2"">Not set</td></tr>
        <tr><td class=""px-4 py-2"">Holiday promotion</td><td class=""px-4 py-2"">Promotion start</td><td class=""px-4 py-2"">Promotion end</td></tr>
        <tr><td class=""px-4 py-2"">Event registration</td><td class=""px-4 py-2"">Registration opens</td><td class=""px-4 py-2"">Event date</td></tr>
        <tr><td class=""px-4 py-2"">Legal notice</td><td class=""px-4 py-2"">Effective date</td><td class=""px-4 py-2"">Superseded date</td></tr>
    </tbody>
</table>

<h3>Timezone Considerations</h3>
<div class=""bg-yellow-50 dark:bg-yellow-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">⚠️ Important</p>
    <p>Always verify the timezone when scheduling content, especially for global campaigns. Content will publish based on the selected timezone, which may differ from your local time.</p>
</div>

<h3>Viewing Scheduled Content</h3>
<p>The CMS UI provides views to manage scheduled content:</p>
<ul>
    <li><strong>Scheduled Queue</strong> - All content waiting to be published</li>
    <li><strong>Expiring Soon</strong> - Content approaching expiration</li>
    <li><strong>Calendar View</strong> - Visual timeline of scheduled activities</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "wp-versioning",
                    ModuleId = "workflows-publishing",
                    Title = "Version History & Rollback",
                    Summary = "Manage content versions and restore previous states when needed.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Navigate and compare content versions",
                        "Restore content to a previous version",
                        "Understand version retention policies"
                    },
                    Content = @"
<h2>Version History & Rollback</h2>
<p>Every change to content creates a new version, providing a complete audit trail and the ability to restore previous states.</p>

<h3>Viewing Version History</h3>
<p>Access version history from any content item to see:</p>
<ul>
    <li><strong>Version number</strong> - Sequential identifier</li>
    <li><strong>Date/time</strong> - When the version was created</li>
    <li><strong>Author</strong> - Who made the changes</li>
    <li><strong>Status</strong> - Draft, Published, etc.</li>
    <li><strong>Change summary</strong> - Notes about what changed</li>
</ul>

<h3>Comparing Versions</h3>
<p>The comparison tool helps you understand what changed:</p>
<ul>
    <li><strong>Side-by-side view</strong> - See two versions simultaneously</li>
    <li><strong>Diff highlighting</strong> - Added, removed, and modified content</li>
    <li><strong>Property-level comparison</strong> - Compare specific fields</li>
</ul>

<h3>Restoring a Previous Version</h3>
<p>To restore content to a previous state:</p>
<ol>
    <li>Open the content item's version history</li>
    <li>Select the version you want to restore</li>
    <li>Click ""Restore this version""</li>
    <li>A new draft is created with the old content</li>
    <li>Review and publish the restored version</li>
</ol>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Restoring a version doesn't delete other versions. It creates a new version with the content from the selected historical version, preserving the complete audit trail.</p>
</div>

<h3>Version Retention</h3>
<p>Version retention policies help manage storage:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Policy</th>
            <th class=""px-4 py-2 text-left"">Behavior</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Keep all versions</td><td class=""px-4 py-2"">Complete history retained indefinitely</td></tr>
        <tr><td class=""px-4 py-2"">Keep N versions</td><td class=""px-4 py-2"">Oldest versions removed when limit exceeded</td></tr>
        <tr><td class=""px-4 py-2"">Keep for N days</td><td class=""px-4 py-2"">Versions older than threshold removed</td></tr>
        <tr><td class=""px-4 py-2"">Keep published only</td><td class=""px-4 py-2"">Only published versions retained long-term</td></tr>
    </tbody>
</table>

<h3>Best Practices</h3>
<ul>
    <li>Add meaningful change notes when saving versions</li>
    <li>Review versions before major updates</li>
    <li>Use compare feature before restoring</li>
    <li>Set appropriate retention policies for your compliance needs</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 10: Localization & Multi-Language

    private LearningModule BuildLocalizationModule()
    {
        return new LearningModule
        {
            Id = "localization",
            Title = "Localization & Multi-Language",
            Description = "Implement multi-language content strategies with localization features in CMS (SaaS).",
            Icon = "language",
            Order = 10,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-modeling", "visual-builder-essentials" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "loc-language-setup",
                    ModuleId = "localization",
                    Title = "Language Configuration",
                    Summary = "Set up and configure languages for your multi-language site.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure available languages in CMS (SaaS)",
                        "Set master and fallback languages",
                        "Understand language branch structure"
                    },
                    Content = @"
<h2>Language Configuration</h2>
<p>CMS (SaaS) provides robust multi-language support, allowing you to manage content in multiple languages from a single platform.</p>

<h3>Adding Languages</h3>
<p>To add a new language to your CMS instance:</p>
<ol>
    <li>Navigate to Admin > Languages</li>
    <li>Click ""Add Language""</li>
    <li>Select from available language codes (e.g., en-US, fr-FR, de-DE)</li>
    <li>Configure language settings</li>
    <li>Enable the language for content creation</li>
</ol>

<h3>Language Settings</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Language Code</td><td class=""px-4 py-2"">ISO language-region code (e.g., en-US)</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Display Name</td><td class=""px-4 py-2"">Human-readable name in the UI</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Master Language</td><td class=""px-4 py-2"">The primary/default language</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Fallback Language</td><td class=""px-4 py-2"">Language to use when translation is missing</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Enabled</td><td class=""px-4 py-2"">Whether editors can create content</td></tr>
    </tbody>
</table>

<h3>Language Branch Structure</h3>
<p>Content exists in language branches:</p>
<ul>
    <li>Each content item can have multiple language versions</li>
    <li>Language versions share the same content ID but different language codes</li>
    <li>You can publish languages independently</li>
    <li>Not all content needs to exist in all languages</li>
</ul>

<h3>Fallback Languages</h3>
<p>Configure fallback chains for when translations don't exist:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>Example Fallback Chain:
fr-CA (French Canadian)
  ↓ falls back to
fr-FR (French France)
  ↓ falls back to
en-US (English US - Master)</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Fallback behavior can be configured per property type. Some properties (like images) may always use fallback, while text properties might require explicit translation.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "loc-translating-content",
                    ModuleId = "localization",
                    Title = "Translating Content",
                    Summary = "Learn efficient workflows for translating content between languages.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create content translations efficiently",
                        "Use the translation comparison view",
                        "Manage translation workflows"
                    },
                    Content = @"
<h2>Translating Content</h2>
<p>Efficient translation workflows are essential for maintaining multi-language sites. CMS (SaaS) provides tools to streamline the translation process.</p>

<h3>Creating Translations</h3>
<p>To translate existing content:</p>
<ol>
    <li>Open the content item in the master language</li>
    <li>Select the target language from the language selector</li>
    <li>Click ""Create Translation"" (or content will auto-create in draft)</li>
    <li>Translate the content fields</li>
    <li>Submit for review and publish</li>
</ol>

<h3>Translation Comparison View</h3>
<p>The side-by-side view helps translators work efficiently:</p>
<ul>
    <li><strong>Source language</strong> on the left (read-only)</li>
    <li><strong>Target language</strong> on the right (editable)</li>
    <li><strong>Property highlighting</strong> shows untranslated fields</li>
    <li><strong>Character counts</strong> help manage length constraints</li>
</ul>

<h3>Translation Status Indicators</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Meaning</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">🟢 Translated</td><td class=""px-4 py-2"">Content fully translated and published</td></tr>
        <tr><td class=""px-4 py-2"">🟡 Partial</td><td class=""px-4 py-2"">Some fields translated, others using fallback</td></tr>
        <tr><td class=""px-4 py-2"">🔴 Missing</td><td class=""px-4 py-2"">No translation exists</td></tr>
        <tr><td class=""px-4 py-2"">🔵 Outdated</td><td class=""px-4 py-2"">Master updated since translation</td></tr>
    </tbody>
</table>

<h3>Bulk Translation Tools</h3>
<p>For large-scale translation projects:</p>
<ul>
    <li><strong>Export for translation</strong> - Generate XLIFF files for external translators</li>
    <li><strong>Import translations</strong> - Import completed XLIFF files</li>
    <li><strong>Translation reports</strong> - Track translation coverage</li>
</ul>

<h3>Translation Workflows</h3>
<p>Configure separate workflows for translations:</p>
<ul>
    <li>Native speaker review step</li>
    <li>Cultural adaptation review</li>
    <li>Legal compliance check for specific markets</li>
    <li>Final publication approval</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "loc-querying-languages",
                    ModuleId = "localization",
                    Title = "Querying Multi-Language Content",
                    Summary = "Retrieve localized content through Optimizely Graph.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Query content in specific languages",
                        "Handle fallback behavior in queries",
                        "Build language-aware frontends"
                    },
                    Content = @"
<h2>Querying Multi-Language Content</h2>
<p>Optimizely Graph provides powerful capabilities for retrieving content in the correct language for your users.</p>

<h3>Language Parameter</h3>
<p>The <code>locale</code> parameter specifies which language to retrieve:</p>

<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>query GetPageContent {
  ArticlePage(
    locale: fr_FR
    where: { _metadata: { url: { default: { eq: ""/about"" } } } }
  ) {
    items {
      title
      summary
      content
    }
  }
}</code></pre>

<h3>Multiple Languages in One Query</h3>
<p>Retrieve content in multiple languages simultaneously:</p>

<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>query GetPageInMultipleLanguages {
  english: ArticlePage(locale: en_US, limit: 1) {
    items { title }
  }
  french: ArticlePage(locale: fr_FR, limit: 1) {
    items { title }
  }
  german: ArticlePage(locale: de_DE, limit: 1) {
    items { title }
  }
}</code></pre>

<h3>Fallback Behavior</h3>
<p>Graph respects the fallback chain configured in the CMS. If content doesn't exist in the requested language, the fallback language is returned.</p>

<div class=""bg-yellow-50 dark:bg-yellow-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">⚠️ Important</p>
    <p>Check the <code>_metadata.language</code> field in responses to know which language was actually returned (it may be a fallback).</p>
</div>

<h3>Language-Specific Filtering</h3>
<p>Filter to only return content that exists in a specific language (no fallback):</p>

<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>query GetFrenchOnlyContent {
  ArticlePage(
    locale: fr_FR
    where: { _metadata: { language: { name: { eq: ""fr-FR"" } } } }
  ) {
    items {
      title
      _metadata {
        language { name }
      }
    }
  }
}</code></pre>

<h3>Building Language-Aware Frontends</h3>
<ul>
    <li>Detect user language from browser/settings</li>
    <li>Pass locale parameter to all Graph queries</li>
    <li>Provide language switcher UI</li>
    <li>Handle missing translations gracefully</li>
    <li>Consider SEO implications (hreflang tags)</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "loc-query-locale",
                            Title = "Querying with Locale",
                            Description = "Example of querying content in a specific language with metadata",
                            Type = ExampleType.Query,
                            ExampleContent = @"query GetLocalizedPage($url: String!, $locale: Locales!) {
  ArticlePage(
    locale: [$locale]
    where: { _metadata: { url: { default: { eq: $url } } } }
  ) {
    items {
      title
      summary
      content
      _metadata {
        language {
          name
          displayName
        }
        published
      }
    }
  }
}",
                            SampleResponse = @"{
  ""data"": {
    ""ArticlePage"": {
      ""items"": [
        {
          ""title"": ""À propos de nous"",
          ""summary"": ""Découvrez notre entreprise..."",
          ""content"": ""<p>Bienvenue...</p>"",
          ""_metadata"": {
            ""language"": {
              ""name"": ""fr-FR"",
              ""displayName"": ""French (France)""
            },
            ""published"": ""2024-01-15T10:30:00Z""
          }
        }
      ]
    }
  }
}",
                            Hints = new List<string>
                            {
                                "Always check _metadata.language to confirm the returned language",
                                "Use variables for locale to support dynamic language switching",
                                "Consider caching strategies per language"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "loc-best-practices",
                    ModuleId = "localization",
                    Title = "Localization Best Practices",
                    Summary = "Learn strategies for successful multi-language content management.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Design content models for localization",
                        "Establish translation governance",
                        "Optimize the translation workflow"
                    },
                    Content = @"
<h2>Localization Best Practices</h2>
<p>Successful multi-language implementations require planning and consistent practices. These guidelines will help you build scalable localized experiences.</p>

<h3>Content Model Design</h3>
<ul>
    <li><strong>Identify translatable properties</strong> - Not everything needs translation (IDs, codes, etc.)</li>
    <li><strong>Use shared assets wisely</strong> - Images may or may not need localization</li>
    <li><strong>Consider text expansion</strong> - German text is often 30% longer than English</li>
    <li><strong>Design flexible layouts</strong> - Accommodate varying content lengths</li>
</ul>

<h3>Translation Governance</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Recommendation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Translation memory</td><td class=""px-4 py-2"">Maintain glossaries for consistent terminology</td></tr>
        <tr><td class=""px-4 py-2"">Style guides</td><td class=""px-4 py-2"">Create per-language style guidelines</td></tr>
        <tr><td class=""px-4 py-2"">Review process</td><td class=""px-4 py-2"">Include native speakers in review</td></tr>
        <tr><td class=""px-4 py-2"">Update tracking</td><td class=""px-4 py-2"">Flag outdated translations for review</td></tr>
    </tbody>
</table>

<h3>URL Strategy</h3>
<p>Choose a URL strategy for your multi-language site:</p>
<ul>
    <li><strong>Subdirectory</strong>: example.com/fr/, example.com/de/ (recommended)</li>
    <li><strong>Subdomain</strong>: fr.example.com, de.example.com</li>
    <li><strong>Separate domains</strong>: example.fr, example.de</li>
</ul>

<h3>SEO Considerations</h3>
<ul>
    <li>Implement hreflang tags for language alternates</li>
    <li>Create language-specific sitemaps</li>
    <li>Translate meta descriptions and titles</li>
    <li>Consider local keyword research</li>
</ul>

<h3>Performance Optimization</h3>
<ul>
    <li>Cache content per language</li>
    <li>Use CDN with geographic optimization</li>
    <li>Minimize fallback requests</li>
    <li>Preload likely language switches</li>
</ul>

<div class=""bg-green-50 dark:bg-green-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">✅ Success Tip</p>
    <p>Start with your most important markets and expand gradually. It's better to have high-quality translations for key languages than poor translations for many.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 11: Media Management

    private LearningModule BuildMediaManagementModule()
    {
        return new LearningModule
        {
            Id = "media-management",
            Title = "Media Management",
            Description = "Manage images, documents, and other media assets effectively in CMS (SaaS).",
            Icon = "photo",
            Order = 11,
            Difficulty = ModuleDifficulty.Beginner,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "mm-media-library",
                    ModuleId = "media-management",
                    Title = "Media Library Overview",
                    Summary = "Navigate and use the media library to manage digital assets.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the media library interface",
                        "Upload and organize media files",
                        "Search and filter media assets"
                    },
                    Content = @"
<h2>Media Library Overview</h2>
<p>The Media Library is your central hub for managing all digital assets including images, videos, documents, and other files used across your content.</p>

<h3>Accessing the Media Library</h3>
<p>The media library can be accessed from:</p>
<ul>
    <li>Main navigation > Assets > Media</li>
    <li>Any media property picker in content editing</li>
    <li>Visual Builder asset panels</li>
</ul>

<h3>Uploading Media</h3>
<p>Several ways to upload files:</p>
<ul>
    <li><strong>Drag and drop</strong> - Drop files directly into the library</li>
    <li><strong>Upload button</strong> - Browse and select files</li>
    <li><strong>Bulk upload</strong> - Upload multiple files simultaneously</li>
    <li><strong>REST API</strong> - Programmatic uploads</li>
</ul>

<h3>Supported File Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Category</th>
            <th class=""px-4 py-2 text-left"">Formats</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Images</td><td class=""px-4 py-2"">JPG, PNG, GIF, WebP, SVG</td></tr>
        <tr><td class=""px-4 py-2"">Videos</td><td class=""px-4 py-2"">MP4, WebM, MOV</td></tr>
        <tr><td class=""px-4 py-2"">Documents</td><td class=""px-4 py-2"">PDF, DOCX, XLSX, PPTX</td></tr>
        <tr><td class=""px-4 py-2"">Other</td><td class=""px-4 py-2"">ZIP, JSON, XML</td></tr>
    </tbody>
</table>

<h3>Organizing Media</h3>
<p>Keep your media organized with:</p>
<ul>
    <li><strong>Folders</strong> - Create hierarchical folder structures</li>
    <li><strong>Tags</strong> - Apply multiple tags for cross-cutting organization</li>
    <li><strong>Metadata</strong> - Add descriptions, alt text, and custom properties</li>
</ul>

<h3>Search and Filter</h3>
<p>Find assets quickly using:</p>
<ul>
    <li>Full-text search on file names and metadata</li>
    <li>Filter by file type, date, size</li>
    <li>Filter by tags and folders</li>
    <li>Sort by date, name, or size</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "mm-image-optimization",
                    ModuleId = "media-management",
                    Title = "Image Optimization",
                    Summary = "Learn how images are automatically optimized for web delivery.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand automatic image optimization",
                        "Configure image transformation parameters",
                        "Implement responsive images"
                    },
                    Content = @"
<h2>Image Optimization</h2>
<p>CMS (SaaS) automatically optimizes images for web delivery, reducing file sizes while maintaining visual quality.</p>

<h3>Automatic Optimization</h3>
<p>When images are served through Graph, they are automatically:</p>
<ul>
    <li><strong>Compressed</strong> - Reduced file size without visible quality loss</li>
    <li><strong>Format converted</strong> - Served as WebP where supported</li>
    <li><strong>Cached</strong> - Delivered through CDN for fast loading</li>
</ul>

<h3>Image Transformation Parameters</h3>
<p>Add URL parameters to transform images on-the-fly:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Parameter</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>width</code></td><td class=""px-4 py-2"">Resize to width (px)</td><td class=""px-4 py-2""><code>?width=800</code></td></tr>
        <tr><td class=""px-4 py-2""><code>height</code></td><td class=""px-4 py-2"">Resize to height (px)</td><td class=""px-4 py-2""><code>?height=600</code></td></tr>
        <tr><td class=""px-4 py-2""><code>quality</code></td><td class=""px-4 py-2"">Compression quality (1-100)</td><td class=""px-4 py-2""><code>?quality=80</code></td></tr>
        <tr><td class=""px-4 py-2""><code>format</code></td><td class=""px-4 py-2"">Output format</td><td class=""px-4 py-2""><code>?format=webp</code></td></tr>
        <tr><td class=""px-4 py-2""><code>fit</code></td><td class=""px-4 py-2"">Resize mode</td><td class=""px-4 py-2""><code>?fit=crop</code></td></tr>
    </tbody>
</table>

<h3>Fit Modes</h3>
<ul>
    <li><strong>contain</strong> - Fit within dimensions, maintain aspect ratio</li>
    <li><strong>cover</strong> - Fill dimensions, crop excess</li>
    <li><strong>crop</strong> - Crop to exact dimensions</li>
    <li><strong>scale-down</strong> - Only shrink, never enlarge</li>
</ul>

<h3>Responsive Images Example</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>&lt;img
  srcset=""
    /image.jpg?width=400 400w,
    /image.jpg?width=800 800w,
    /image.jpg?width=1200 1200w
  ""
  sizes=""(max-width: 600px) 400px,
         (max-width: 1000px) 800px,
         1200px""
  src=""/image.jpg?width=800""
  alt=""Responsive image""
/&gt;</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Transformed images are cached at the CDN edge. The first request generates the transformation; subsequent requests are served from cache.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "mm-metadata-alt-text",
                    ModuleId = "media-management",
                    Title = "Metadata and Accessibility",
                    Summary = "Add metadata and alt text for better SEO and accessibility.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Add and manage media metadata",
                        "Write effective alt text for accessibility",
                        "Understand metadata impact on SEO"
                    },
                    Content = @"
<h2>Metadata and Accessibility</h2>
<p>Proper metadata improves searchability, SEO, and accessibility. Every media asset should have appropriate metadata.</p>

<h3>Core Metadata Fields</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Field</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Required</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Title</td><td class=""px-4 py-2"">Human-readable name</td><td class=""px-4 py-2"">Recommended</td></tr>
        <tr><td class=""px-4 py-2"">Alt Text</td><td class=""px-4 py-2"">Screen reader description</td><td class=""px-4 py-2"">Required (images)</td></tr>
        <tr><td class=""px-4 py-2"">Description</td><td class=""px-4 py-2"">Detailed description</td><td class=""px-4 py-2"">Optional</td></tr>
        <tr><td class=""px-4 py-2"">Copyright</td><td class=""px-4 py-2"">Rights information</td><td class=""px-4 py-2"">Recommended</td></tr>
        <tr><td class=""px-4 py-2"">Tags</td><td class=""px-4 py-2"">Categorization keywords</td><td class=""px-4 py-2"">Recommended</td></tr>
    </tbody>
</table>

<h3>Writing Effective Alt Text</h3>
<p>Alt text is critical for accessibility. Follow these guidelines:</p>

<h4>Do:</h4>
<ul>
    <li>Describe what the image shows, not what it is</li>
    <li>Keep it concise (under 125 characters)</li>
    <li>Include relevant context</li>
    <li>Describe text within images</li>
</ul>

<h4>Don't:</h4>
<ul>
    <li>Start with ""Image of..."" or ""Picture of...""</li>
    <li>Repeat information already in surrounding text</li>
    <li>Use file names as alt text</li>
    <li>Leave decorative images with alt text (use empty alt)</li>
</ul>

<h3>Examples</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Image</th>
            <th class=""px-4 py-2 text-left"">❌ Poor Alt Text</th>
            <th class=""px-4 py-2 text-left"">✅ Good Alt Text</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Team photo</td><td class=""px-4 py-2"">""team.jpg""</td><td class=""px-4 py-2"">""Marketing team celebrating product launch""</td></tr>
        <tr><td class=""px-4 py-2"">Product shot</td><td class=""px-4 py-2"">""Image of product""</td><td class=""px-4 py-2"">""Blue wireless headphones with noise cancellation""</td></tr>
        <tr><td class=""px-4 py-2"">Chart</td><td class=""px-4 py-2"">""Chart""</td><td class=""px-4 py-2"">""Bar chart showing 40% increase in Q4 sales""</td></tr>
    </tbody>
</table>

<h3>SEO Impact</h3>
<ul>
    <li>Alt text helps search engines understand images</li>
    <li>Descriptive file names improve discoverability</li>
    <li>Metadata appears in image search results</li>
    <li>Properly tagged images rank higher</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "mm-using-media-content",
                    ModuleId = "media-management",
                    Title = "Using Media in Content",
                    Summary = "Insert and manage media within your content and Visual Builder.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Insert media into content properties",
                        "Use media in Visual Builder elements",
                        "Query media through Optimizely Graph"
                    },
                    Content = @"
<h2>Using Media in Content</h2>
<p>Media assets are referenced within your content through property pickers and can be queried alongside content through Optimizely Graph.</p>

<h3>Media Property Types</h3>
<p>Content models can include media properties:</p>
<ul>
    <li><strong>ContentReference (Image)</strong> - Single image reference</li>
    <li><strong>ContentReference (Media)</strong> - Any media type</li>
    <li><strong>ContentArea</strong> - Multiple media items</li>
    <li><strong>URL</strong> - Direct URL to external media</li>
</ul>

<h3>Inserting Media in Edit Mode</h3>
<ol>
    <li>Click the media picker button on the property</li>
    <li>Browse or search the media library</li>
    <li>Select the desired asset</li>
    <li>Confirm selection</li>
</ol>

<h3>Media in Visual Builder</h3>
<p>Visual Builder provides rich media handling:</p>
<ul>
    <li><strong>Image elements</strong> - Dedicated image blocks with sizing options</li>
    <li><strong>Background images</strong> - Apply images to sections/elements</li>
    <li><strong>Video elements</strong> - Embedded video players</li>
    <li><strong>Gallery elements</strong> - Multiple image displays</li>
</ul>

<h3>Querying Media via Graph</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>query GetPageWithMedia {
  ArticlePage {
    items {
      title
      heroImage {
        url
        altText: _metadata { displayName }
      }
      galleryImages {
        items {
          url
          _metadata {
            displayName
            mimeType
          }
        }
      }
    }
  }
}</code></pre>

<h3>Image Focal Points</h3>
<p>Set focal points to ensure important parts of images aren't cropped:</p>
<ol>
    <li>Open the image in the media library</li>
    <li>Click ""Set focal point""</li>
    <li>Click on the most important part of the image</li>
    <li>Save the focal point</li>
</ol>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Focal points are respected when images are cropped automatically for different display sizes, ensuring faces or key elements remain visible.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 12: Webhooks & Events

    private LearningModule BuildWebhooksEventsModule()
    {
        return new LearningModule
        {
            Id = "webhooks-events",
            Title = "Webhooks & Events",
            Description = "Implement event-driven integrations using webhooks in CMS (SaaS).",
            Icon = "bolt",
            Order = 12,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "rest-api" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "we-intro-webhooks",
                    ModuleId = "webhooks-events",
                    Title = "Introduction to Webhooks",
                    Summary = "Understand webhooks and event-driven architecture in CMS (SaaS).",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what webhooks are and how they work",
                        "Identify use cases for event-driven integrations",
                        "Learn the webhook event model"
                    },
                    Content = @"
<h2>Introduction to Webhooks</h2>
<p>Webhooks enable real-time integrations by sending HTTP notifications when events occur in CMS (SaaS). Instead of polling for changes, your systems receive instant updates.</p>

<h3>What Are Webhooks?</h3>
<p>A webhook is an HTTP callback that:</p>
<ul>
    <li>Triggers automatically when events occur</li>
    <li>Sends a POST request to your specified URL</li>
    <li>Contains event details in the request body</li>
    <li>Enables real-time system integration</li>
</ul>

<h3>Webhooks vs. Polling</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Webhooks</th>
            <th class=""px-4 py-2 text-left"">Polling</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Timing</td><td class=""px-4 py-2"">Real-time</td><td class=""px-4 py-2"">Delayed (polling interval)</td></tr>
        <tr><td class=""px-4 py-2"">Efficiency</td><td class=""px-4 py-2"">Only when events occur</td><td class=""px-4 py-2"">Constant requests</td></tr>
        <tr><td class=""px-4 py-2"">Complexity</td><td class=""px-4 py-2"">Requires endpoint setup</td><td class=""px-4 py-2"">Simple implementation</td></tr>
        <tr><td class=""px-4 py-2"">Scalability</td><td class=""px-4 py-2"">Highly scalable</td><td class=""px-4 py-2"">Limited by rate limits</td></tr>
    </tbody>
</table>

<h3>Common Use Cases</h3>
<ul>
    <li><strong>Cache invalidation</strong> - Clear frontend cache when content updates</li>
    <li><strong>Search indexing</strong> - Update external search engines</li>
    <li><strong>Notifications</strong> - Alert teams of content changes</li>
    <li><strong>Workflow triggers</strong> - Start external processes</li>
    <li><strong>Analytics</strong> - Track content operations</li>
    <li><strong>Backup/sync</strong> - Replicate content to other systems</li>
</ul>

<h3>Available Event Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Event</th>
            <th class=""px-4 py-2 text-left"">Triggered When</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>content.published</code></td><td class=""px-4 py-2"">Content is published</td></tr>
        <tr><td class=""px-4 py-2""><code>content.unpublished</code></td><td class=""px-4 py-2"">Content is unpublished</td></tr>
        <tr><td class=""px-4 py-2""><code>content.deleted</code></td><td class=""px-4 py-2"">Content is deleted</td></tr>
        <tr><td class=""px-4 py-2""><code>content.moved</code></td><td class=""px-4 py-2"">Content location changes</td></tr>
        <tr><td class=""px-4 py-2""><code>contentType.created</code></td><td class=""px-4 py-2"">New content type defined</td></tr>
        <tr><td class=""px-4 py-2""><code>contentType.updated</code></td><td class=""px-4 py-2"">Content type modified</td></tr>
    </tbody>
</table>
"
                },
                new Lesson
                {
                    Id = "we-configuring-webhooks",
                    ModuleId = "webhooks-events",
                    Title = "Configuring Webhooks",
                    Summary = "Set up and configure webhooks for your CMS instance.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create webhook configurations",
                        "Configure event filters and conditions",
                        "Set up webhook security"
                    },
                    Content = @"
<h2>Configuring Webhooks</h2>
<p>Webhook configuration defines which events trigger notifications and where they're sent.</p>

<h3>Creating a Webhook</h3>
<p>Configure webhooks via the Admin UI or REST API:</p>
<ol>
    <li>Navigate to Admin > Webhooks</li>
    <li>Click ""Add Webhook""</li>
    <li>Configure the webhook settings</li>
    <li>Enable the webhook</li>
    <li>Test the configuration</li>
</ol>

<h3>Webhook Configuration Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Setting</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Name</td><td class=""px-4 py-2"">Descriptive name for the webhook</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">URL</td><td class=""px-4 py-2"">Endpoint to receive webhook calls</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Events</td><td class=""px-4 py-2"">Which events trigger the webhook</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Content Types</td><td class=""px-4 py-2"">Filter to specific content types</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Secret</td><td class=""px-4 py-2"">Shared secret for signature verification</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Enabled</td><td class=""px-4 py-2"">Active/inactive status</td></tr>
    </tbody>
</table>

<h3>Event Filtering</h3>
<p>Filter webhooks to only trigger for specific scenarios:</p>
<ul>
    <li><strong>By content type</strong> - Only ArticlePage publishes</li>
    <li><strong>By event type</strong> - Only publish events</li>
    <li><strong>By content location</strong> - Only content under /news/</li>
</ul>

<h3>Security Configuration</h3>
<p>Secure your webhooks:</p>
<ul>
    <li><strong>HTTPS required</strong> - Use only secure endpoints</li>
    <li><strong>Signature verification</strong> - Validate request authenticity</li>
    <li><strong>IP allowlisting</strong> - Restrict to known CMS IPs</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">⚠️ Important</p>
    <p>Always verify webhook signatures to ensure requests genuinely come from CMS (SaaS) and haven't been tampered with.</p>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "we-config-example",
                            Title = "Webhook Configuration via API",
                            Description = "Create a webhook configuration using the REST API",
                            Type = ExampleType.Code,
                            ExampleContent = @"POST /api/v1/webhooks
Content-Type: application/json
Authorization: Bearer {token}

{
  ""name"": ""Cache Invalidation Webhook"",
  ""url"": ""https://api.mysite.com/webhooks/cache-invalidate"",
  ""events"": [
    ""content.published"",
    ""content.unpublished"",
    ""content.deleted""
  ],
  ""contentTypes"": [
    ""ArticlePage"",
    ""ProductPage"",
    ""LandingPage""
  ],
  ""secret"": ""your-webhook-secret-here"",
  ""enabled"": true,
  ""headers"": {
    ""X-Custom-Header"": ""custom-value""
  }
}",
                            SampleResponse = @"{
  ""id"": ""wh_abc123def456"",
  ""name"": ""Cache Invalidation Webhook"",
  ""url"": ""https://api.mysite.com/webhooks/cache-invalidate"",
  ""events"": [""content.published"", ""content.unpublished"", ""content.deleted""],
  ""enabled"": true,
  ""createdAt"": ""2024-01-15T10:30:00Z""
}",
                            Hints = new List<string>
                            {
                                "Store the webhook secret securely - you'll need it for signature verification",
                                "Use content type filters to reduce unnecessary webhook calls",
                                "Test webhooks in a staging environment first"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "we-handling-webhooks",
                    ModuleId = "webhooks-events",
                    Title = "Handling Webhook Events",
                    Summary = "Build reliable webhook receivers that process events correctly.",
                    Order = 3,
                    EstimatedMinutes = 15,
                    LearningObjectives = new List<string>
                    {
                        "Build webhook receiver endpoints",
                        "Verify webhook signatures",
                        "Handle events reliably"
                    },
                    Content = @"
<h2>Handling Webhook Events</h2>
<p>Building a reliable webhook receiver requires proper validation, processing, and error handling.</p>

<h3>Webhook Payload Structure</h3>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>{
  ""id"": ""evt_abc123"",
  ""type"": ""content.published"",
  ""timestamp"": ""2024-01-15T10:30:00Z"",
  ""data"": {
    ""contentId"": ""12345"",
    ""contentType"": ""ArticlePage"",
    ""language"": ""en-US"",
    ""url"": ""/articles/new-article"",
    ""version"": 5
  }
}</code></pre>

<h3>Signature Verification</h3>
<p>Verify that webhooks are from CMS (SaaS):</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>// The signature is sent in the X-Webhook-Signature header
// Compute HMAC-SHA256 of the raw body using your secret
// Compare with the provided signature

const crypto = require('crypto');

function verifySignature(payload, signature, secret) {
  const computed = crypto
    .createHmac('sha256', secret)
    .update(payload)
    .digest('hex');

  return crypto.timingSafeEqual(
    Buffer.from(signature),
    Buffer.from(computed)
  );
}</code></pre>

<h3>Best Practices</h3>
<ul>
    <li><strong>Respond quickly</strong> - Return 200 within 5 seconds, process async</li>
    <li><strong>Idempotent handling</strong> - Same event may be delivered multiple times</li>
    <li><strong>Queue processing</strong> - Use a message queue for reliability</li>
    <li><strong>Log everything</strong> - Track all webhook activity</li>
    <li><strong>Handle failures gracefully</strong> - Don't crash on bad data</li>
</ul>

<h3>Retry Behavior</h3>
<p>CMS (SaaS) retries failed webhooks:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Attempt</th>
            <th class=""px-4 py-2 text-left"">Delay</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1st retry</td><td class=""px-4 py-2"">1 minute</td></tr>
        <tr><td class=""px-4 py-2"">2nd retry</td><td class=""px-4 py-2"">5 minutes</td></tr>
        <tr><td class=""px-4 py-2"">3rd retry</td><td class=""px-4 py-2"">30 minutes</td></tr>
        <tr><td class=""px-4 py-2"">4th retry</td><td class=""px-4 py-2"">2 hours</td></tr>
        <tr><td class=""px-4 py-2"">5th retry</td><td class=""px-4 py-2"">24 hours</td></tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Return a 2xx status code to acknowledge receipt, even if you'll process the event asynchronously. Non-2xx responses trigger retries.</p>
</div>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "we-handler-example",
                            Title = "Webhook Handler (Node.js)",
                            Description = "Complete webhook handler with signature verification",
                            Type = ExampleType.Code,
                            ExampleContent = @"const express = require('express');
const crypto = require('crypto');

const WEBHOOK_SECRET = process.env.WEBHOOK_SECRET;

app.post('/webhooks/cms', express.raw({type: '*/*'}), (req, res) => {
  // Verify signature
  const signature = req.headers['x-webhook-signature'];
  const computed = crypto
    .createHmac('sha256', WEBHOOK_SECRET)
    .update(req.body)
    .digest('hex');

  if (!crypto.timingSafeEqual(Buffer.from(signature), Buffer.from(computed))) {
    return res.status(401).json({ error: 'Invalid signature' });
  }

  // Parse payload
  const event = JSON.parse(req.body);

  // Acknowledge receipt immediately
  res.status(200).json({ received: true });

  // Process asynchronously
  processWebhookEvent(event).catch(err => {
    console.error('Webhook processing failed:', err);
  });
});

async function processWebhookEvent(event) {
  switch (event.type) {
    case 'content.published':
      await invalidateCache(event.data.url);
      break;
    case 'content.deleted':
      await removeFromSearch(event.data.contentId);
      break;
  }
}",
                            Hints = new List<string>
                            {
                                "Always use timing-safe comparison for signatures",
                                "Process webhooks asynchronously to respond within timeout",
                                "Store webhook secret in environment variables, never in code"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "we-integration-patterns",
                    ModuleId = "webhooks-events",
                    Title = "Integration Patterns",
                    Summary = "Learn common patterns for webhook-based integrations.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Implement cache invalidation with webhooks",
                        "Build search index synchronization",
                        "Create notification integrations"
                    },
                    Content = @"
<h2>Integration Patterns</h2>
<p>Webhooks enable powerful integrations. These patterns show common implementations.</p>

<h3>Pattern 1: Cache Invalidation</h3>
<p>Automatically clear CDN/application cache when content changes:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>async function handlePublish(event) {
  const paths = [
    event.data.url,
    '/sitemap.xml',
    '/' // homepage if affected
  ];

  await Promise.all(paths.map(path =>
    cdn.purge(path)
  ));
}</code></pre>

<h3>Pattern 2: Search Index Sync</h3>
<p>Keep external search engines synchronized:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>async function syncToSearch(event) {
  if (event.type === 'content.published') {
    const content = await fetchFullContent(event.data.contentId);
    await searchClient.index({
      id: event.data.contentId,
      title: content.title,
      body: content.content,
      url: event.data.url
    });
  } else if (event.type === 'content.deleted') {
    await searchClient.delete(event.data.contentId);
  }
}</code></pre>

<h3>Pattern 3: Slack Notifications</h3>
<p>Notify teams of content activity:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>async function notifySlack(event) {
  const message = {
    text: `Content ${event.type.split('.')[1]}: ${event.data.url}`,
    blocks: [{
      type: 'section',
      text: {
        type: 'mrkdwn',
        text: `*${event.data.contentType}* was ${event.type.split('.')[1]}\n` +
              `URL: ${event.data.url}\n` +
              `Language: ${event.data.language}`
      }
    }]
  };

  await slack.postMessage('#content-updates', message);
}</code></pre>

<h3>Pattern 4: Static Site Rebuilds</h3>
<p>Trigger static site regeneration:</p>
<ul>
    <li>Receive publish webhook</li>
    <li>Trigger CI/CD pipeline (GitHub Actions, Netlify, Vercel)</li>
    <li>Rebuild affected pages or full site</li>
    <li>Deploy updated static files</li>
</ul>

<h3>Pattern 5: Data Warehouse Sync</h3>
<p>Keep analytics systems updated:</p>
<ul>
    <li>Track content lifecycle events</li>
    <li>Store in data warehouse</li>
    <li>Build content analytics dashboards</li>
    <li>Analyze publishing patterns</li>
</ul>

<div class=""bg-green-50 dark:bg-green-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">✅ Success Tip</p>
    <p>Combine multiple webhook handlers behind a single endpoint that routes events to appropriate processors. This simplifies webhook management.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 13: Troubleshooting & Debugging

    private LearningModule BuildTroubleshootingModule()
    {
        return new LearningModule
        {
            Id = "troubleshooting",
            Title = "Troubleshooting & Debugging",
            Description = "Diagnose and resolve common issues in CMS (SaaS) implementations.",
            Icon = "bug-ant",
            Order = 13,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started", "rest-api", "graph-integration" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ts-common-issues",
                    ModuleId = "troubleshooting",
                    Title = "Common Issues & Solutions",
                    Summary = "Learn to identify and resolve frequently encountered problems.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Identify common CMS (SaaS) issues",
                        "Apply systematic troubleshooting approaches",
                        "Resolve typical configuration problems"
                    },
                    Content = @"
<h2>Common Issues & Solutions</h2>
<p>Understanding common issues helps you quickly diagnose and resolve problems in your CMS (SaaS) implementation.</p>

<h3>Content Not Appearing in Graph</h3>
<p><strong>Symptoms:</strong> Published content isn't returned by Graph queries</p>
<p><strong>Possible Causes:</strong></p>
<ul>
    <li>Content not actually published (still in draft)</li>
    <li>Indexing delay (wait 1-2 minutes)</li>
    <li>Content type not configured for indexing</li>
    <li>Query filters excluding the content</li>
</ul>
<p><strong>Solutions:</strong></p>
<ol>
    <li>Verify publish status in CMS UI</li>
    <li>Check <code>_metadata.status</code> in your query</li>
    <li>Review content type indexing configuration</li>
    <li>Simplify query to remove filters</li>
</ol>

<h3>Authentication Failures</h3>
<p><strong>Symptoms:</strong> 401/403 errors from REST API or Graph</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Error</th>
            <th class=""px-4 py-2 text-left"">Cause</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">401 Unauthorized</td><td class=""px-4 py-2"">Invalid or expired token</td><td class=""px-4 py-2"">Refresh authentication token</td></tr>
        <tr><td class=""px-4 py-2"">403 Forbidden</td><td class=""px-4 py-2"">Insufficient permissions</td><td class=""px-4 py-2"">Check API key scopes</td></tr>
        <tr><td class=""px-4 py-2"">Invalid API Key</td><td class=""px-4 py-2"">Wrong key or environment</td><td class=""px-4 py-2"">Verify key for correct instance</td></tr>
    </tbody>
</table>

<h3>Visual Builder Issues</h3>
<p><strong>Symptoms:</strong> Elements not rendering, styles missing, preview problems</p>
<p><strong>Common causes:</strong></p>
<ul>
    <li>Missing element definitions</li>
    <li>Invalid display template configuration</li>
    <li>Browser caching old assets</li>
    <li>Content type mismatches</li>
</ul>

<h3>Performance Issues</h3>
<p><strong>Symptoms:</strong> Slow page loads, timeout errors, high latency</p>
<p><strong>Investigation steps:</strong></p>
<ol>
    <li>Check Graph query complexity</li>
    <li>Review content reference depth</li>
    <li>Verify CDN configuration</li>
    <li>Monitor API response times</li>
</ol>

<div class=""bg-yellow-50 dark:bg-yellow-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">⚠️ Troubleshooting Tip</p>
    <p>Always check the simplest explanation first. Most issues are caused by content not being published, incorrect environment configuration, or caching.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "ts-debugging-graph",
                    ModuleId = "troubleshooting",
                    Title = "Debugging Graph Queries",
                    Summary = "Diagnose and fix issues with Optimizely Graph queries.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Debug GraphQL query errors",
                        "Use Graph introspection for troubleshooting",
                        "Optimize poorly performing queries"
                    },
                    Content = @"
<h2>Debugging Graph Queries</h2>
<p>Graph queries can fail or return unexpected results for various reasons. Systematic debugging helps identify the cause.</p>

<h3>Common Query Errors</h3>

<h4>Syntax Errors</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code># Error: Expected Name, found }
query {
  ArticlePage {
    items {
      title,  # Trailing comma causes error
    }
  }
}</code></pre>

<h4>Unknown Field</h4>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code># Error: Cannot query field ""tittle"" on type ""ArticlePage""
query {
  ArticlePage {
    items {
      tittle  # Typo in field name
    }
  }
}</code></pre>

<h3>Using Introspection</h3>
<p>Query the schema to verify field names and types:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>query IntrospectType {
  __type(name: ""ArticlePage"") {
    fields {
      name
      type {
        name
        kind
      }
    }
  }
}</code></pre>

<h3>Empty Results Debugging</h3>
<p>When queries return no results:</p>
<ol>
    <li><strong>Remove all filters</strong> - Verify content exists at all</li>
    <li><strong>Add filters one by one</strong> - Find the problematic filter</li>
    <li><strong>Check locale</strong> - Content may not exist in requested language</li>
    <li><strong>Verify status</strong> - Include draft content temporarily</li>
</ol>

<h3>Performance Debugging</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Cause</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Slow queries</td><td class=""px-4 py-2"">Deep nested fragments</td><td class=""px-4 py-2"">Flatten query structure</td></tr>
        <tr><td class=""px-4 py-2"">Timeouts</td><td class=""px-4 py-2"">Too many items requested</td><td class=""px-4 py-2"">Use pagination (limit/skip)</td></tr>
        <tr><td class=""px-4 py-2"">Large payloads</td><td class=""px-4 py-2"">Requesting unnecessary fields</td><td class=""px-4 py-2"">Select only needed fields</td></tr>
    </tbody>
</table>

<h3>Using the Graph Explorer</h3>
<p>The Graph Explorer UI provides:</p>
<ul>
    <li>Syntax highlighting and validation</li>
    <li>Schema documentation</li>
    <li>Query history</li>
    <li>Response inspection</li>
    <li>Variable management</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Use the Graph Explorer to test and refine queries before implementing them in your application. It provides immediate feedback on errors.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "ts-api-debugging",
                    ModuleId = "troubleshooting",
                    Title = "REST API Troubleshooting",
                    Summary = "Diagnose and resolve REST API integration issues.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Interpret API error responses",
                        "Debug request/response issues",
                        "Use API testing tools effectively"
                    },
                    Content = @"
<h2>REST API Troubleshooting</h2>
<p>REST API issues often stem from authentication, request formatting, or permission problems.</p>

<h3>HTTP Status Codes</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Code</th>
            <th class=""px-4 py-2 text-left"">Meaning</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">400 Bad Request</td><td class=""px-4 py-2"">Invalid request format</td><td class=""px-4 py-2"">Check JSON syntax and required fields</td></tr>
        <tr><td class=""px-4 py-2"">401 Unauthorized</td><td class=""px-4 py-2"">Missing/invalid auth</td><td class=""px-4 py-2"">Verify Authorization header</td></tr>
        <tr><td class=""px-4 py-2"">403 Forbidden</td><td class=""px-4 py-2"">Permission denied</td><td class=""px-4 py-2"">Check API key permissions</td></tr>
        <tr><td class=""px-4 py-2"">404 Not Found</td><td class=""px-4 py-2"">Resource doesn't exist</td><td class=""px-4 py-2"">Verify endpoint URL and IDs</td></tr>
        <tr><td class=""px-4 py-2"">409 Conflict</td><td class=""px-4 py-2"">Conflicting operation</td><td class=""px-4 py-2"">Check for concurrent edits</td></tr>
        <tr><td class=""px-4 py-2"">422 Unprocessable</td><td class=""px-4 py-2"">Validation failed</td><td class=""px-4 py-2"">Review validation errors in response</td></tr>
        <tr><td class=""px-4 py-2"">429 Too Many Requests</td><td class=""px-4 py-2"">Rate limited</td><td class=""px-4 py-2"">Implement backoff/retry</td></tr>
        <tr><td class=""px-4 py-2"">500 Server Error</td><td class=""px-4 py-2"">Internal error</td><td class=""px-4 py-2"">Contact support with request ID</td></tr>
    </tbody>
</table>

<h3>Reading Error Responses</h3>
<p>API errors include helpful details:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>{
  ""error"": {
    ""code"": ""VALIDATION_ERROR"",
    ""message"": ""Content validation failed"",
    ""details"": [
      {
        ""field"": ""title"",
        ""error"": ""Title is required""
      },
      {
        ""field"": ""slug"",
        ""error"": ""Slug must be unique""
      }
    ],
    ""requestId"": ""req_abc123""
  }
}</code></pre>

<h3>Using API Testing Tools</h3>
<p>Test API calls with tools like Postman or curl:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto""><code>curl -X GET \
  'https://api.cms.optimizely.com/v1/content' \
  -H 'Authorization: Bearer YOUR_TOKEN' \
  -H 'Content-Type: application/json' \
  -v  # Verbose output shows full request/response</code></pre>

<h3>Debugging Checklist</h3>
<ul>
    <li>☐ Is the endpoint URL correct?</li>
    <li>☐ Is the HTTP method correct (GET/POST/PUT/DELETE)?</li>
    <li>☐ Is the Authorization header present and valid?</li>
    <li>☐ Is the Content-Type header set correctly?</li>
    <li>☐ Is the request body valid JSON?</li>
    <li>☐ Are all required fields included?</li>
    <li>☐ Are IDs and references valid?</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "ts-content-issues",
                    ModuleId = "troubleshooting",
                    Title = "Content & Publishing Issues",
                    Summary = "Resolve issues with content creation, editing, and publishing.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Troubleshoot content creation problems",
                        "Resolve publishing failures",
                        "Fix content display issues"
                    },
                    Content = @"
<h2>Content & Publishing Issues</h2>
<p>Content-related issues can affect editors and end users. Understanding common problems helps resolve them quickly.</p>

<h3>Content Creation Issues</h3>

<h4>""Content Type Not Found""</h4>
<ul>
    <li>Content type may have been deleted or renamed</li>
    <li>User may lack permission to create that type</li>
    <li>Check Admin > Content Types for availability</li>
</ul>

<h4>Validation Errors</h4>
<ul>
    <li>Required fields not filled</li>
    <li>Field value doesn't match validation rules</li>
    <li>Unique constraint violated (duplicate slug)</li>
</ul>

<h3>Publishing Failures</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Cause</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Can't publish</td><td class=""px-4 py-2"">Missing publish permission</td><td class=""px-4 py-2"">Check user role and access rights</td></tr>
        <tr><td class=""px-4 py-2"">Workflow blocked</td><td class=""px-4 py-2"">Pending approval step</td><td class=""px-4 py-2"">Complete required approvals</td></tr>
        <tr><td class=""px-4 py-2"">Parent not published</td><td class=""px-4 py-2"">Required parent content missing</td><td class=""px-4 py-2"">Publish parent content first</td></tr>
        <tr><td class=""px-4 py-2"">Scheduled conflict</td><td class=""px-4 py-2"">Publish date in past</td><td class=""px-4 py-2"">Update schedule or publish immediately</td></tr>
    </tbody>
</table>

<h3>Content Not Displaying</h3>
<p>When published content doesn't appear on the frontend:</p>
<ol>
    <li><strong>Verify publish status</strong> - Check in CMS UI</li>
    <li><strong>Check language</strong> - Content may exist in different language</li>
    <li><strong>Clear caches</strong> - CDN, application, and browser caches</li>
    <li><strong>Check Graph indexing</strong> - Wait for indexing to complete</li>
    <li><strong>Review access rights</strong> - Content may require authentication</li>
</ol>

<h3>Version Conflicts</h3>
<p>When multiple editors work on the same content:</p>
<ul>
    <li>Optimistic locking prevents overwriting changes</li>
    <li>""Content modified by another user"" error appears</li>
    <li>Refresh and merge changes, or create new draft</li>
</ul>

<div class=""bg-green-50 dark:bg-green-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">✅ Prevention Tip</p>
    <p>Use content locking features to prevent conflicts when multiple editors work on the same content. Establish clear ownership for content updates.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "ts-getting-help",
                    ModuleId = "troubleshooting",
                    Title = "Getting Help & Support",
                    Summary = "Learn how to effectively get support for CMS (SaaS) issues.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Know where to find documentation and resources",
                        "Prepare effective support requests",
                        "Use community resources"
                    },
                    Content = @"
<h2>Getting Help & Support</h2>
<p>When you can't resolve an issue yourself, knowing how to get effective help is crucial.</p>

<h3>Documentation Resources</h3>
<ul>
    <li><strong>Optimizely Documentation</strong> - Official docs at docs.developers.optimizely.com</li>
    <li><strong>API Reference</strong> - Complete REST API documentation</li>
    <li><strong>Graph Schema Explorer</strong> - Interactive schema documentation</li>
    <li><strong>Release Notes</strong> - Latest features and changes</li>
</ul>

<h3>Community Resources</h3>
<ul>
    <li><strong>Optimizely World</strong> - Community forums and knowledge base</li>
    <li><strong>GitHub Samples</strong> - Example implementations</li>
    <li><strong>Stack Overflow</strong> - Tag: optimizely-cms</li>
    <li><strong>Partner Network</strong> - Certified implementation partners</li>
</ul>

<h3>Contacting Support</h3>
<p>When filing a support ticket, include:</p>
<ol>
    <li><strong>Environment details</strong> - Instance URL, integration type</li>
    <li><strong>Steps to reproduce</strong> - Exact sequence of actions</li>
    <li><strong>Expected vs actual behavior</strong> - What should happen vs what happens</li>
    <li><strong>Error messages</strong> - Full error text and codes</li>
    <li><strong>Request IDs</strong> - From API error responses</li>
    <li><strong>Screenshots/recordings</strong> - Visual evidence of the issue</li>
    <li><strong>Timing</strong> - When did the issue start?</li>
</ol>

<h3>Support Tiers</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Severity</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Response Time</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Critical</td><td class=""px-4 py-2"">Production down, no workaround</td><td class=""px-4 py-2"">1 hour</td></tr>
        <tr><td class=""px-4 py-2"">High</td><td class=""px-4 py-2"">Major feature impacted</td><td class=""px-4 py-2"">4 hours</td></tr>
        <tr><td class=""px-4 py-2"">Medium</td><td class=""px-4 py-2"">Feature impacted, workaround exists</td><td class=""px-4 py-2"">1 business day</td></tr>
        <tr><td class=""px-4 py-2"">Low</td><td class=""px-4 py-2"">Question or minor issue</td><td class=""px-4 py-2"">2 business days</td></tr>
    </tbody>
</table>

<h3>Self-Service Tools</h3>
<ul>
    <li><strong>Status page</strong> - Check for known outages</li>
    <li><strong>Health checks</strong> - Verify system status</li>
    <li><strong>Audit logs</strong> - Review recent changes</li>
    <li><strong>Activity logs</strong> - Track user actions</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900/30 p-4 rounded-lg my-4"">
    <p class=""font-semibold"">💡 Key Insight</p>
    <p>Before contacting support, check the status page and try reproducing the issue in a clean environment. Many issues are temporary or environment-specific.</p>
</div>
"
                }
            }
        };
    }

    #endregion
}
