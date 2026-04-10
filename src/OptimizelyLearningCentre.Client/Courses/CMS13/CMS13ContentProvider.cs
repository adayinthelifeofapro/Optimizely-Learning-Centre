using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.CMS13;

/// <summary>
/// Content provider for the Optimizely CMS 13 course
/// </summary>
public class CMS13ContentProvider : ILearningContentProvider
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
            BuildOverviewModule(),
            BuildVisualBuilderModule(),
            BuildContentManagerModule(),
            BuildEditExperienceModule(),
            BuildGraphIntegrationModule(),
            BuildLanguagesModule(),
            BuildContentVariationsModule(),
            BuildFrameworkModule(),
            BuildApplicationsModule(),
            BuildHeadlessPreviewModule(),
            BuildSmoothRebuildModule(),
            BuildMigrationModule()
        };
    }

    #region Module 1: Overview of CMS 13

    private LearningModule BuildOverviewModule()
    {
        return new LearningModule
        {
            Id = "overview",
            Title = "Overview of CMS 13",
            Description = "Discover the new features and enhancements in Optimizely CMS 13.",
            Icon = "server",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ov-introduction",
                    ModuleId = "overview",
                    Title = "What's New in CMS 13",
                    Summary = "Get an overview of the major features and enhancements in CMS 13.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the key features introduced in CMS 13",
                        "Learn about the strong focus on Optimizely Graph integration",
                        "Discover improvements to content management and editing"
                    },
                    Content = @"
<h2>Welcome to CMS 13</h2>
<p>Optimizely CMS 13 represents a significant evolution of the platform, with a <strong>strong focus on integration with Optimizely Graph</strong>. This release introduces enhanced content management, editing, and delivery capabilities that shape the future of content management with Optimizely.</p>

<h3>Key Feature Areas</h3>

<h4>Content Management &amp; Editing</h4>
<p>CMS 13 introduces a modernized content management experience:</p>
<ul>
    <li><strong>New Content Manager</strong> - A search-first approach powered by Optimizely Graph</li>
    <li><strong>Enhanced Visual Builder</strong> - Improved WYSIWYG editing with templates and blueprints</li>
    <li><strong>In-context editing</strong> - Edit content directly within Visual Builder</li>
    <li><strong>Content Variations</strong> - Multiple published versions for A/B testing, personalization, and seasonal content</li>
    <li><strong>DAM Integration</strong> - Browse and select digital assets directly within the editing interface</li>
</ul>

<h4>Graph Integration</h4>
<p>Optimizely Graph is now a core component of CMS 13:</p>
<ul>
    <li><strong>Content retrieval</strong> - Graph is mandatory for content retrieval operations</li>
    <li><strong>Graph C# SDK</strong> - Fluent .NET API with typed deserialization, filtering, and pagination</li>
    <li><strong>External content</strong> - Surface external content sources in the CMS UI</li>
    <li><strong>Search capabilities</strong> - Full-text search, filtering, and faceted navigation</li>
</ul>

<h4>AI &amp; Automation</h4>
<p>Integrated AI capabilities through Optimizely Opal:</p>
<ul>
    <li><strong>Opal Chat</strong> - Generative AI agent orchestration within the CMS</li>
    <li><strong>Content creation</strong> - AI-assisted content generation and optimization</li>
    <li><strong>Translation workflows</strong> - AI-powered translation preserving content structure</li>
</ul>

<h4>Multilingual Support</h4>
<p>Enhanced language management capabilities:</p>
<ul>
    <li><strong>Global fallback languages</strong> - Configure recursive fallback chains</li>
    <li><strong>Auto-translation</strong> - Machine-translate content while preserving structure</li>
    <li><strong>Language context switching</strong> - Dynamic UI updates based on language selection</li>
</ul>

<h4>Developer Experience</h4>
<p>Modernized development platform:</p>
<ul>
    <li><strong>.NET 10 runtime</strong> - Built on the latest .NET platform</li>
    <li><strong>CMS REST API</strong> - Programmatic content management included via <code>AddCms()</code></li>
    <li><strong>Custom elements</strong> - Define custom elements in the Admin UI</li>
    <li><strong>React components</strong> - Support for React-based UI extensions</li>
    <li><strong>Simplified plugin architecture</strong> - Plugin Manager removed in favor of modern patterns</li>
</ul>

"
                },
                new Lesson
                {
                    Id = "ov-graph-mandatory",
                    ModuleId = "overview",
                    Title = "Optimizely Graph - Core Architecture",
                    Summary = "Understand why Optimizely Graph is mandatory in CMS 13 and its role in content delivery.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand why Graph is mandatory in CMS 13",
                        "Learn the difference between content retrieval and search",
                        "Understand how CMS 13 uses Graph internally"
                    },
                    Content = @"
<h2>Optimizely Graph as Core Architecture</h2>
<p>In CMS 13, <strong>Optimizely Graph is mandatory</strong>. It serves as the core architecture for content retrieval and is always used internally by the CMS.</p>

<h3>Why Graph is Required</h3>
<p>Unlike CMS 12 where Graph was optional, CMS 13 is built with Graph at its foundation:</p>
<ul>
    <li><strong>Content Manager</strong> - Requires Graph for its search-first navigation</li>
    <li><strong>Content retrieval</strong> - CMS 13 uses Graph internally to retrieve and structure content</li>
    <li><strong>Performance</strong> - Optimized queries through Graph's caching layer</li>
</ul>

<h3>Two Distinct Capabilities</h3>
<p>Graph operates with two separate capabilities in CMS 13:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Capability</th>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Content Retrieval</td><td class=""px-4 py-2""><span class=""text-green-600"">Required</span></td><td class=""px-4 py-2"">CMS 13 uses Graph internally for content operations</td></tr>
        <tr><td class=""px-4 py-2"">Search</td><td class=""px-4 py-2""><span class=""text-blue-600"">Optional</span></td><td class=""px-4 py-2"">Organizations can use Graph for search or implement their own provider</td></tr>
    </tbody>
</table>

<h3>Getting Started</h3>
<p>To integrate Graph into your CMS 13 application, add this to your startup file:</p>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>services.AddContentGraph()</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Note:</strong> The Graph C# SDK provides a fluent .NET API for querying Optimizely Graph with typed deserialization, filtering, ordering, and pagination support.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "ov-not-included",
                    ModuleId = "overview",
                    Title = "Known Limitations & Migration Considerations",
                    Summary = "Understand the key architectural changes, known limitations, and important considerations when working with or migrating to CMS 13.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand key architectural changes from CMS 12",
                        "Know what has changed in the editing and content management experience",
                        "Plan your adoption with awareness of breaking changes"
                    },
                    Content = @"
<h2>Known Limitations &amp; Migration Considerations</h2>
<p>CMS 13 is now generally available and introduces major architectural changes from CMS 12. Understanding these changes, available features, and remaining considerations is essential for a successful adoption or migration.</p>

<h3>Architectural Changes from CMS 12</h3>

<h4>1. Optimizely Graph is Mandatory</h4>
<p>Unlike CMS 12 where Graph was optional, <strong>Optimizely Graph is now a required component</strong> in CMS 13. It serves as the core architecture for content retrieval and powers the Content Manager's search-first navigation. You must have an active Graph subscription configured for CMS 13 to function.</p>

<h4>2. On-Page Edit (OPE) Replaced by Visual Builder</h4>
<p>Traditional On-Page Edit is <strong>no longer available</strong>. Visual Builder is the default and primary editing experience, offering drag-and-drop composition, real-time preview, autosave, and direct property editing. Plan for editor retraining if your team relies on OPE workflows.</p>

<h4>3. Site Definitions Replaced by Applications</h4>
<p>The concept of Site Definitions has been replaced by <strong>Applications</strong>. This affects routing, multi-site configuration, and how sites are managed in the admin interface. Existing site definition configurations must be migrated to the new Applications model.</p>

<h4>4. Plugin Manager Removed</h4>
<p>The legacy Plugin Manager has been removed in favor of modern .NET dependency injection patterns. Any custom plugins using the old architecture must be refactored to use standard ASP.NET Core service registration.</p>

<h4>5. Opti ID Required</h4>
<p><strong>Opti ID is mandatory</strong> for CMS 13, providing single sign-on (SSO) with multi-factor authentication (MFA) and SCIM provisioning. This replaces the previous authentication mechanisms.</p>

<h3>Features Available in CMS 13 GA</h3>
<p>The following features, which were not available during the pre-release period, are now included in the GA release:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Details</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Optimizely Opal</td><td class=""px-4 py-2""><span class=""text-green-600 font-semibold"">Available</span></td><td class=""px-4 py-2"">AI agent orchestration for content creation, translation, and workflow automation</td></tr>
        <tr><td class=""px-4 py-2"">DAM Integration</td><td class=""px-4 py-2""><span class=""text-green-600 font-semibold"">Available</span></td><td class=""px-4 py-2"">Asset picker for browsing and selecting digital assets within the editing interface</td></tr>
        <tr><td class=""px-4 py-2"">CMS REST API</td><td class=""px-4 py-2""><span class=""text-green-600 font-semibold"">Available</span></td><td class=""px-4 py-2"">Included via <code>AddCms()</code> registration for programmatic content management</td></tr>
        <tr><td class=""px-4 py-2"">Graph C# SDK</td><td class=""px-4 py-2""><span class=""text-green-600 font-semibold"">Available</span></td><td class=""px-4 py-2"">Fluent .NET API with typed deserialization, filtering, ordering, and pagination</td></tr>
        <tr><td class=""px-4 py-2"">Projects</td><td class=""px-4 py-2""><span class=""text-green-600 font-semibold"">Available</span></td><td class=""px-4 py-2"">Content workflow coordination and release management</td></tr>
        <tr><td class=""px-4 py-2"">Forms</td><td class=""px-4 py-2""><span class=""text-green-600 font-semibold"">Available</span></td><td class=""px-4 py-2"">Form creation and integration within Visual Builder</td></tr>
        <tr><td class=""px-4 py-2"">Smooth Rebuild</td><td class=""px-4 py-2""><span class=""text-green-600 font-semibold"">Available</span></td><td class=""px-4 py-2"">Reset Graph sources without downtime during reindexing</td></tr>
    </tbody>
</table>

<h3>Remaining Considerations</h3>

<h4>Content Variations</h4>
<ul>
    <li>Only localizable properties are supported for variations</li>
    <li>Softlinks are not generated for published variations</li>
</ul>

<h4>Search &amp; Navigation Removed</h4>
<p>The legacy Search &amp; Navigation (formerly Find) is no longer available. All search functionality must use Optimizely Graph or a custom search provider.</p>

<h3>Breaking Changes</h3>
<p>CMS 13 includes breaking changes across nine categories. Review these carefully before migrating:</p>
<ul>
    <li><strong>Framework and platform</strong> - .NET 10 runtime, removed APIs</li>
    <li><strong>Content types and properties</strong> - Property definition changes</li>
    <li><strong>Content management and repository</strong> - Repository operation changes</li>
    <li><strong>Sites to Applications and routing</strong> - New routing model</li>
    <li><strong>Security and access control</strong> - Opti ID requirement</li>
    <li><strong>Scheduling, plugins, and events</strong> - Plugin Manager removal</li>
    <li><strong>UI, editors, and shell</strong> - Visual Builder as default</li>
    <li><strong>Localization and import/export</strong> - Translation workflow changes</li>
    <li><strong>Third-party package compatibility</strong> - Package updates required</li>
</ul>

<h3>Planning Your Adoption</h3>
<p>When planning your CMS 13 migration or new implementation, consider:</p>
<ul>
    <li><strong>Breaking changes review</strong> - Thoroughly review the official breaking changes documentation for each category</li>
    <li><strong>Graph subscription</strong> - Ensure your Optimizely Graph subscription and configuration are ready</li>
    <li><strong>Opti ID setup</strong> - Configure Opti ID for authentication before migration</li>
    <li><strong>Editor training</strong> - Visual Builder replaces On-Page Edit; plan retraining for content editors</li>
    <li><strong>Code audit</strong> - Identify custom code using removed APIs (Plugin Manager, Site Definitions, OPE)</li>
    <li><strong>Testing strategy</strong> - Validate all integrations and custom functionality against CMS 13</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Official Documentation:</strong> Refer to the <a href=""https://docs.developers.optimizely.com/content-management-system/v13.0.0-CMS/docs/cms-13-overview"" target=""_blank"" class=""text-blue-600 hover:underline"">CMS 13 documentation</a> for the complete list of breaking changes, migration guides, and API replacement maps.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 2: Visual Builder

    private LearningModule BuildVisualBuilderModule()
    {
        return new LearningModule
        {
            Id = "visual-builder",
            Title = "Visual Builder Enhancements",
            Description = "Master the enhanced Visual Builder with templates, blueprints, and improved editing capabilities.",
            Icon = "paint-brush",
            Order = 2,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "vb-layout-architecture",
                    ModuleId = "visual-builder",
                    Title = "Layout Architecture",
                    Summary = "Understand the data model for Visual Builder experiences and sections.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the separation of layout structure from content data",
                        "Learn about key fields like AllowLayout and AllowAdditionalData",
                        "Understand experiences, sections, and elements"
                    },
                    Content = @"
<h2>Visual Builder Layout Architecture</h2>
<p>CMS 13 introduces a modernized Visual Builder with a clear separation between <strong>layout structure</strong> and <strong>content data</strong>. This separation enables flexible content management while keeping layout definitions distinct from actual content values.</p>

<h3>Key Structural Elements</h3>

<h4>AllowLayout Field</h4>
<p>The <code>AllowLayout</code> field controls whether a content type supports Visual Builder composition. When enabled, the content type can be used as an experience or section in Visual Builder.</p>

<h4>AllowAdditionalData Field</h4>
<p>The <code>AllowAdditionalData</code> field enables supplementary data elements for individual instances. This allows editors to add extra properties to specific content items without modifying the content type definition.</p>

<h4>Pre-defined Content Types</h4>
<p>CMS 13 includes blank experience and section templates that are enabled by default, providing a starting point for content creation.</p>

<h3>Layout Hierarchy</h3>
<p>Visual Builder organizes content in a clear hierarchy:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Element</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><strong>Experiences</strong></td><td class=""px-4 py-2"">The main routable entry point</td><td class=""px-4 py-2"">Top-level pages that visitors navigate to</td></tr>
        <tr><td class=""px-4 py-2""><strong>Sections</strong></td><td class=""px-4 py-2"">Vertical content areas</td><td class=""px-4 py-2"">Organize content within experiences</td></tr>
        <tr><td class=""px-4 py-2""><strong>Elements</strong></td><td class=""px-4 py-2"">The smallest building blocks</td><td class=""px-4 py-2"">Contain the actual content</td></tr>
    </tbody>
</table>

<h3>Block Type Configuration</h3>
<p>Administrators can designate blocks for different contexts:</p>
<ul>
    <li><strong>Element-enabled</strong> - Available within sections</li>
    <li><strong>Section-enabled</strong> - Available within experiences</li>
    <li><strong>Dual-configured</strong> - Supporting both contexts</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "vb-templates-blueprints",
                    ModuleId = "visual-builder",
                    Title = "Templates and Blueprints",
                    Summary = "Learn how to create and manage templates and blueprints for efficient content creation.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create templates from existing content",
                        "Manage blueprints with import/export capabilities",
                        "Understand automatic thumbnail generation"
                    },
                    Content = @"
<h2>Templates and Blueprints</h2>
<p>CMS 13 enhances content creation efficiency through improved template and blueprint management.</p>

<h3>Template System</h3>
<p>Templates allow editors to create reusable content patterns:</p>
<ul>
    <li><strong>Create from existing content</strong> - Turn any piece of content into a template</li>
    <li><strong>Create from content types</strong> - Generate templates from type definitions</li>
    <li><strong>Pre-populated properties</strong> - New instances inherit template values and layouts</li>
    <li><strong>Export/Import workflows</strong> - Share templates across environments</li>
    <li><strong>Automatic thumbnails</strong> - System generates preview images automatically</li>
</ul>

<h3>Blueprint Management</h3>
<p>Blueprints have been enhanced with dedicated management capabilities:</p>
<ul>
    <li><strong>Separate export/import</strong> - Blueprints are now distinct content types</li>
    <li><strong>Custom thumbnails</strong> - Support for auto-generated or custom preview images</li>
    <li><strong>Dedicated interface</strong> - Administrators can rename, delete, and organize blueprints</li>
</ul>

<h3>Creating a Template</h3>
<p>To create a template from existing content:</p>
<ol>
    <li>Navigate to the content item you want to use as a template</li>
    <li>Open the context menu and select ""Save as Template""</li>
    <li>Provide a name and optional thumbnail</li>
    <li>The template becomes available in the blueprint selector</li>
</ol>

<h3>Using Templates</h3>
<p>When creating new content:</p>
<ol>
    <li>Click ""Create New"" in Content Manager</li>
    <li>The blueprint selector modal appears with available templates</li>
    <li>Select a template to pre-populate the new content</li>
    <li>Modify as needed and publish</li>
</ol>
"
                },
                new Lesson
                {
                    Id = "vb-editing-features",
                    ModuleId = "visual-builder",
                    Title = "Enhanced Editing Features",
                    Summary = "Explore the new editing capabilities in Visual Builder including property highlighting and copy/paste.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use direct property editing within Visual Builder",
                        "Understand property highlighting for visual feedback",
                        "Leverage copy/paste operations for rows and columns"
                    },
                    Content = @"
<h2>Enhanced Editing Features</h2>
<p>Visual Builder in CMS 13 includes several improvements to the editing experience.</p>

<h3>Direct Property Editing</h3>
<p>Experience and section properties are now editable inline within Visual Builder. You no longer need to switch to separate views to modify content properties.</p>

<h3>Property Highlighting</h3>
<p>Interactive visual feedback connects panel fields to their preview representations:</p>
<ul>
    <li><strong>Hover state</strong> - Pale blue highlighting shows which field is being referenced</li>
    <li><strong>Click state</strong> - Dark blue highlighting indicates the selected field</li>
    <li><strong>Field names</strong> - Displayed contextually to help identify properties</li>
</ul>

<h3>Media Asset Editing</h3>
<p>Full editing support for media properties:</p>
<ul>
    <li>Edit custom attributes directly</li>
    <li>Configure preview settings</li>
    <li>Autosave functionality preserves changes</li>
</ul>

<h3>Copy/Paste Operations</h3>
<p>Rows and columns support powerful duplication features:</p>
<ul>
    <li><strong>Complete duplication</strong> - Copy rows and columns with all nested content</li>
    <li><strong>Style preservation</strong> - Styling and asset references are maintained</li>
    <li><strong>User confirmation</strong> - Feedback messages confirm successful operations</li>
</ul>

<h3>Shared Block Integration</h3>
<p>Section-enabled shared blocks can be dragged into experiences with consistent editing workflows. Publishing experiences with unmodified shared blocks remains permitted.</p>

<h3>Bulk Actions</h3>
<p>Content operations preserve layout data:</p>
<ul>
    <li>Content type exports include layout data</li>
    <li>Imports recreate layout structures intact</li>
    <li>Copying content items duplicates complete layout information</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Limitation:</strong> TinyMCE editors within Visual Builder cannot receive dropped blocks of any type, preventing unintended composition conflicts.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "vb-aspnet-mvc",
                    ModuleId = "visual-builder",
                    Title = "ASP.NET MVC Support",
                    Summary = "Learn how to use Visual Builder with ASP.NET MVC without requiring Optimizely Graph.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand tag helpers for Visual Builder rendering",
                        "Use HTML helpers for non-composable patterns",
                        "Implement automatic preview updates during editing"
                    },
                    Content = @"
<h2>ASP.NET MVC Support</h2>
<p>Visual Builder extends to ASP.NET MVC environments without requiring Optimizely Graph for rendering.</p>

<h3>Tag Helpers</h3>
<p>CMS 13 provides tag helpers for rendering Visual Builder compositions:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>&lt;epi-outline&gt;
    &lt;epi-component /&gt;
    &lt;epi-grid&gt;
        &lt;epi-row&gt;
            &lt;epi-column /&gt;
        &lt;/epi-row&gt;
    &lt;/epi-grid&gt;
&lt;/epi-outline&gt;</code></pre>

<h4>Available Tag Helpers</h4>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Tag Helper</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>epi-outline</code></td><td class=""px-4 py-2"">Container for Visual Builder content</td></tr>
        <tr><td class=""px-4 py-2""><code>epi-component</code></td><td class=""px-4 py-2"">Renders individual components</td></tr>
        <tr><td class=""px-4 py-2""><code>epi-grid</code></td><td class=""px-4 py-2"">Grid container for layout</td></tr>
        <tr><td class=""px-4 py-2""><code>epi-row</code></td><td class=""px-4 py-2"">Row within a grid</td></tr>
        <tr><td class=""px-4 py-2""><code>epi-column</code></td><td class=""px-4 py-2"">Column within a row</td></tr>
    </tbody>
</table>

<h3>HTML Helpers</h3>
<p>For non-composable patterns, HTML helpers follow established patterns from earlier CMS versions.</p>

<h3>Custom Tag Names</h3>
<p>Visual Builder supports custom tag names for flexible markup generation, allowing you to match your frontend framework's requirements.</p>

<h3>Automatic Preview Updates</h3>
<p>When editing content in Visual Builder, previews update automatically without manual refresh, providing immediate visual feedback.</p>

<h3>Custom Elements</h3>
<p>Citizen developers can define custom elements directly within the Admin UI using existing editors and fields. This enables non-developers to create new element types without writing code.</p>
"
                }
            }
        };
    }

    #endregion

    #region Module 3: Content Manager

    private LearningModule BuildContentManagerModule()
    {
        return new LearningModule
        {
            Id = "content-manager",
            Title = "Content Manager",
            Description = "Learn about the new Content Manager powered by Optimizely Graph.",
            Icon = "folder-open",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cm-setup",
                    ModuleId = "content-manager",
                    Title = "Setting Up Content Manager",
                    Summary = "Configure Content Manager with Optimizely Graph integration.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Enable Graph Service in DXP Portal",
                        "Configure authentication keys",
                        "Set up services in Startup.cs"
                    },
                    Content = @"
<h2>Setting Up Content Manager</h2>
<p>Content Manager requires Optimizely Graph integration. Without Graph, Content Manager cannot function.</p>

<h3>Setup Steps</h3>

<h4>1. Enable Graph Service</h4>
<p>In the DXP Portal, navigate to the API tab and enable the Graph Service for your environment.</p>

<h4>2. Retrieve Authentication Keys</h4>
<p>From the DXP Portal, obtain your authentication keys for Graph integration.</p>

<h4>3. Configure appSettings.json</h4>
<p>Add the gateway address and credentials to your application configuration:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>{
  ""Optimizely"": {
    ""Graph"": {
      ""GatewayAddress"": ""https://graph.optimizely.com"",
      ""SingleKey"": ""your-single-key"",
      ""AppKey"": ""your-app-key"",
      ""Secret"": ""your-secret""
    }
  }
}</code></pre>

<h4>4. Register Services</h4>
<p>In your <code>Startup.cs</code> or <code>Program.cs</code>, register the required services:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>services.AddContentGraph()
        .AddContentManager();</code></pre>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Important:</strong> The order of service calls matters. Always call <code>AddContentGraph()</code> before <code>AddContentManager()</code>.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "cm-search-navigation",
                    ModuleId = "content-manager",
                    Title = "Search-First Navigation",
                    Summary = "Explore Content Manager's search-first approach to finding and managing content.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the search-first navigation paradigm",
                        "Use filtering capabilities effectively",
                        "Navigate content with Grid and List views"
                    },
                    Content = @"
<h2>Search-First Navigation</h2>
<p>Content Manager implements a <strong>search-first approach</strong> to content navigation, powered by Optimizely Graph. This is a fundamental shift from traditional tree-based navigation.</p>

<h3>Enhanced Search</h3>
<p>Search is at the core of Content Manager, making it easy to find content across your entire site:</p>
<ul>
    <li>Full-text search across all content</li>
    <li>Instant results as you type</li>
    <li>Search within specific content areas</li>
</ul>

<h3>Content Filtering</h3>
<p>Three filter categories help you narrow down content:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Filter</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><strong>Content Type</strong></td><td class=""px-4 py-2"">Select from types in the current list (multiple selections supported)</td></tr>
        <tr><td class=""px-4 py-2""><strong>Status</strong></td><td class=""px-4 py-2"">Filter by content status (published, draft, etc.)</td></tr>
        <tr><td class=""px-4 py-2""><strong>Language</strong></td><td class=""px-4 py-2"">Multi-language filtering with added language column</td></tr>
    </tbody>
</table>

<p>Server requests are debounced at 200ms per checkbox interaction for optimal performance.</p>

<h3>View Options</h3>

<h4>List View</h4>
<ul>
    <li>Toggle columns on/off</li>
    <li>Name column is required, others optional</li>
    <li>Horizontal scrolling accommodates many columns</li>
    <li>Column order matches selector arrangement</li>
</ul>

<h4>Grid View</h4>
<ul>
    <li>Image and PDF assets display with gray background borders</li>
    <li>Non-media content fills containers without borders</li>
    <li>Selected items show blue border indicators</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "cm-details-editing",
                    ModuleId = "content-manager",
                    Title = "Content Details and In-Context Editing",
                    Summary = "Use the details panel and in-context editing for efficient content management.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Access content details quickly",
                        "Use in-context editing for faster workflows",
                        "Navigate external content sources"
                    },
                    Content = @"
<h2>Content Details and Editing</h2>
<p>Content Manager provides efficient tools for viewing and editing content without leaving the main interface.</p>

<h3>Content Details Panel</h3>
<p>Access detailed information about any content item through the ""View details"" option in each item's menu:</p>
<ul>
    <li><strong>Asset thumbnail</strong> - Preview image or placeholder icon</li>
    <li><strong>Content title and icon</strong> - Quick identification</li>
    <li><strong>Type-specific properties</strong> - Relevant metadata for the content type</li>
    <li><strong>Edit option</strong> - Quick link to edit (external link for non-CMS content)</li>
</ul>
<p>The details panel is available in Grid view only and appears as a right-side drawer.</p>

<h3>In-Context Editing</h3>
<p>Click <strong>Edit</strong> to open an iframe-based dialog for editing content:</p>
<ul>
    <li>The iframe preloads for speed</li>
    <li>Persists when closed rather than being destroyed</li>
    <li>Improves subsequent access performance</li>
</ul>

<h3>Page Creation</h3>
<p>Create new pages directly from Content Manager:</p>
<ul>
    <li>Blueprint selector modal with contextual templates</li>
    <li>Page tree navigation with search functionality</li>
    <li>Permission-based access control</li>
    <li>Warning for nodes exceeding 10,000 direct children</li>
    <li>Auto-redirect to Edit UI after creation</li>
</ul>

<h3>Content Source Navigation</h3>
<p>The left-side panel displays available content sources:</p>
<ul>
    <li>CMS content appears by default</li>
    <li>External sources appear when configured</li>
    <li>External sources show ""shadow"" content types from Graph (e.g., <code>cmp_PublicImageAsset</code>)</li>
    <li>Filtering is not available for external source lists</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "cm-image-selector",
                    ModuleId = "content-manager",
                    Title = "Image Asset Selection",
                    Summary = "Use the streamlined image selector within Visual Builder.",
                    Order = 4,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Use the focused image-only browser",
                        "Integrate with Optimizely CMP",
                        "Replace images efficiently"
                    },
                    Content = @"
<h2>Image Asset Selection</h2>
<p>Visual Builder includes a streamlined content selector specifically designed for image selection.</p>

<h3>Key Features</h3>
<ul>
    <li><strong>Focused browsing</strong> - Image-only view filters out non-image content</li>
    <li><strong>CMP integration</strong> - Access Optimizely CMP assets if available</li>
    <li><strong>Single image support</strong> - Designed for single image references</li>
    <li><strong>Previous image highlighting</strong> - Current image is highlighted during replacement</li>
</ul>

<h3>Streamlined Interface</h3>
<p>The image selector is intentionally simplified:</p>
<ul>
    <li>No multi-select functionality</li>
    <li>No create/upload buttons within the selector</li>
    <li>Quick selection and replacement workflow</li>
</ul>

<h3>Usage</h3>
<ol>
    <li>Click on an image property field in Visual Builder</li>
    <li>The image selector opens, showing available images</li>
    <li>If replacing, the current image is highlighted</li>
    <li>Select the new image</li>
    <li>The selector closes and the property updates</li>
</ol>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Tip:</strong> Upload new images through the standard media management interface before using them in Visual Builder.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 4: Edit Experience

    private LearningModule BuildEditExperienceModule()
    {
        return new LearningModule
        {
            Id = "edit-experience",
            Title = "Edit Experience Improvements",
            Description = "Explore the enhanced editing capabilities in CMS 13.",
            Icon = "pencil-square",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ed-visual-builder-default",
                    ModuleId = "edit-experience",
                    Title = "Visual Builder as Default",
                    Summary = "Understand the shift to Visual Builder as the primary editing interface.",
                    Order = 1,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Understand why On-Page Edit is disabled",
                        "Learn the benefits of Visual Builder editing",
                        "Configure content types for Visual Builder"
                    },
                    Content = @"
<h2>Visual Builder as Default Editor</h2>
<p>In CMS 13, <strong>On-Page Edit (OPE) is disabled</strong> in favor of Visual Builder. This represents a significant shift in the editing paradigm.</p>

<h3>Why This Change?</h3>
<p>Visual Builder provides a more consistent and powerful editing experience:</p>
<ul>
    <li><strong>Unified interface</strong> - All editing happens in one place</li>
    <li><strong>Layout control</strong> - Direct manipulation of page structure</li>
    <li><strong>Preview integration</strong> - Real-time preview as you edit</li>
    <li><strong>Mobile-friendly</strong> - Better support for responsive editing</li>
</ul>

<h3>Content Type Requirements</h3>
<p>To use Visual Builder's full feature set, your content types need to be configured as experiences. Non-experience content can still be edited but without dynamic layout capabilities.</p>

<h3>Interface Simplification</h3>
<p>The toolbar no longer displays redundant View and Preview buttons, as Visual Builder provides integrated preview functionality. This streamlines the interface and reduces confusion about which view to use.</p>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Migration Note:</strong> If your existing workflows rely heavily on OPE, plan to adapt your editor training and content type configurations for Visual Builder.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "ed-audience-viewing",
                    ModuleId = "edit-experience",
                    Title = "Audience Viewing",
                    Summary = "Preview content as different audience segments.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Preview pages as specific audience segments",
                        "Understand audience integration with visitor groups",
                        "Access audience viewing through the context menu"
                    },
                    Content = @"
<h2>Audience Viewing</h2>
<p>Content creators can preview pages as specific audience segments to understand how tailored content displays to different visitors.</p>

<h3>How It Works</h3>
<p>This feature reintroduces audience functionality similar to CMS 12's visitor groups:</p>
<ul>
    <li>Available for in-process sites on PaaS with audiences installed</li>
    <li>Accessible through a context menu located next to the Publish button</li>
    <li>Allows switching between different audience perspectives</li>
</ul>

<h3>Use Cases</h3>
<ul>
    <li><strong>Personalization testing</strong> - Verify that personalized content appears correctly</li>
    <li><strong>Segment preview</strong> - See how different user groups experience your content</li>
    <li><strong>QA workflows</strong> - Test audience targeting before publishing</li>
</ul>

<h3>Accessing Audience View</h3>
<ol>
    <li>Navigate to the content you want to preview</li>
    <li>Open the context menu next to the Publish button</li>
    <li>Select the audience segment to preview as</li>
    <li>The page refreshes to show the audience-specific view</li>
</ol>
"
                },
                new Lesson
                {
                    Id = "ed-file-uploads",
                    ModuleId = "edit-experience",
                    Title = "Enhanced File Uploads",
                    Summary = "Understand the improved file upload capabilities and security restrictions.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand increased upload size limits",
                        "Learn about file extension whitelisting",
                        "Handle upload errors effectively"
                    },
                    Content = @"
<h2>Enhanced File Uploads</h2>
<p>CMS 13 includes significant improvements to file upload functionality, including increased limits and better security.</p>

<h3>Increased Size Limits</h3>
<p>Default upload size limits have been increased to align with Microsoft's recommended Kestrel and Azure settings. This allows for larger media assets without custom configuration.</p>

<h3>Improved Error Messages</h3>
<p>When uploads fail, you now receive specific feedback about the issue:</p>
<ul>
    <li>Clear message: ""Upload failed because file exceeded the size limit of X""</li>
    <li>HTTP 413 status for size-related failures</li>
    <li>Actionable information for troubleshooting</li>
</ul>

<h3>Security: File Extension Whitelist</h3>
<p>File upload restrictions now enforce a <strong>whitelist of allowed extensions</strong> for enhanced security:</p>
<ul>
    <li>Applies to asset uploads</li>
    <li>Applies to property editors</li>
    <li>Applies to drag-and-drop functionality (including TinyMCE)</li>
</ul>

<p>Unsupported extensions trigger clear error messages, making it easy to understand why an upload was rejected.</p>

<h3>UI Terminology Changes</h3>
<p>""Blocks"" have been renamed to ""Shared Blocks"" in the user interface to better distinguish them from other content types.</p>

<h3>Additional Information</h3>
<p>Content GUID (Globally Unique Identifier) now displays alongside content ID, making it easier to reference specific content items in code or APIs.</p>
"
                },
                new Lesson
                {
                    Id = "ed-version-gadget",
                    ModuleId = "edit-experience",
                    Title = "Version Gadget and Scheduling",
                    Summary = "Learn about version gadget enhancements and scheduled publishing.",
                    Order = 4,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "View scheduled publication dates in the version gadget",
                        "Understand version tracking improvements",
                        "Configure translation support"
                    },
                    Content = @"
<h2>Version Gadget and Scheduling</h2>
<p>The Version gadget in CMS 13 includes enhancements for better visibility into content scheduling and versioning.</p>

<h3>Scheduled Publication Display</h3>
<p>The Version gadget now prominently displays scheduled publication dates:</p>
<ul>
    <li>Label changed from ""Scheduled for publish on"" to ""Scheduled to publish on""</li>
    <li>Clear visual indication of when content will go live</li>
    <li>Helps content teams coordinate publication timing</li>
</ul>

<h3>Version Tracking</h3>
<p>Improvements to version visibility help you understand your content's history:</p>
<ul>
    <li>Clear version numbering</li>
    <li>Publication date tracking</li>
    <li>Author information</li>
</ul>

<h3>Translation Support</h3>
<p>Translation functionality is now configured by default on DXP deployments:</p>
<ul>
    <li>Automatic content translation in deployed projects</li>
    <li>Integration with the languages system</li>
    <li>Streamlined localization workflows</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Tip:</strong> Use scheduled publishing to coordinate content releases across time zones and ensure consistent go-live times for campaigns.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 5: Graph Integration

    private LearningModule BuildGraphIntegrationModule()
    {
        return new LearningModule
        {
            Id = "graph-integration",
            Title = "Optimizely Graph Integration",
            Description = "Deep dive into how CMS 13 integrates with Optimizely Graph for content delivery.",
            Icon = "circle-stack",
            Order = 5,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gi-search-modes",
                    ModuleId = "graph-integration",
                    Title = "Search Modes and Capabilities",
                    Summary = "Explore the different search modes available through the Graph SDK.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand content search vs typed search vs untyped search",
                        "Choose the right search mode for your use case",
                        "Leverage advanced search features"
                    },
                    Content = @"
<h2>Search Modes and Capabilities</h2>
<p>The Graph SDK supports three distinct search approaches, each suited for different scenarios.</p>

<h3>1. Content Search</h3>
<p>Type-safe filtering that loads data through <code>IContentLoader</code>:</p>
<ul>
    <li>Returns <code>IContent</code> instances</li>
    <li>Includes permission handling</li>
    <li>Includes URL resolution</li>
    <li>Best for: CMS-integrated scenarios where you need full content objects</li>
</ul>

<h3>2. Untyped Search</h3>
<p>Accepts type and fragments as strings:</p>
<ul>
    <li>Returns raw <code>JsonElement</code> objects</li>
    <li>Maximum flexibility</li>
    <li>Best for: Dynamic queries or prototyping</li>
</ul>

<h3>3. Typed Search</h3>
<p>Deserializes responses to provided contracts:</p>
<ul>
    <li>Compile-time type safety</li>
    <li>Custom data contracts</li>
    <li>Best for: API responses or when you need specific data shapes</li>
</ul>

<h3>Advanced Search Features</h3>
<p>All search modes support powerful features:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Full-text search</td><td class=""px-4 py-2"">Search across all content with term highlighting</td></tr>
        <tr><td class=""px-4 py-2"">Filters &amp; operators</td><td class=""px-4 py-2"">Logical connectors and fuzzy matching</td></tr>
        <tr><td class=""px-4 py-2"">Locale support</td><td class=""px-4 py-2"">Filter by language and locale</td></tr>
        <tr><td class=""px-4 py-2"">Sorting</td><td class=""px-4 py-2"">Standard and semantic sorting options</td></tr>
        <tr><td class=""px-4 py-2"">Pagination</td><td class=""px-4 py-2"">Cursor-based pagination for large result sets</td></tr>
        <tr><td class=""px-4 py-2"">Autocomplete</td><td class=""px-4 py-2"">Suggestions as users type</td></tr>
        <tr><td class=""px-4 py-2"">Facets</td><td class=""px-4 py-2"">Aggregations for filtering UI</td></tr>
        <tr><td class=""px-4 py-2"">Boosting</td><td class=""px-4 py-2"">Prioritize certain results</td></tr>
    </tbody>
</table>
"
                },
                new Lesson
                {
                    Id = "gi-security",
                    ModuleId = "graph-integration",
                    Title = "Security and Authorization",
                    Summary = "Implement proper security and authorization with Graph integration.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure single-key and HMAC authentication",
                        "Use FilterForVisitor for authorization",
                        "Respect Do Not Track settings"
                    },
                    Content = @"
<h2>Security and Authorization</h2>
<p>Graph integration in CMS 13 includes robust security features to protect your content.</p>

<h3>Authentication Options</h3>

<h4>Single-Key Authentication</h4>
<p>Simple API key authentication suitable for public content:</p>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>{
  ""Optimizely"": {
    ""Graph"": {
      ""SingleKey"": ""your-public-single-key""
    }
  }
}</code></pre>

<h4>HMAC Authentication</h4>
<p>More secure option using AppKey and Secret for sensitive operations:</p>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>{
  ""Optimizely"": {
    ""Graph"": {
      ""AppKey"": ""your-app-key"",
      ""Secret"": ""your-secret""
    }
  }
}</code></pre>

<h3>Authorization</h3>
<p>Use <code>FilterForVisitor</code> to apply content permissions:</p>
<ul>
    <li>Accepts principal argument for user context</li>
    <li>Accepts locale argument for language filtering</li>
    <li>Respects CMS access rights</li>
</ul>

<h3>Privacy and Tracking</h3>
<p>Graph includes integrated tracking that respects user privacy:</p>
<ul>
    <li>Honors ""Do Not Track"" browser settings</li>
    <li>Built-in metrics and observability</li>
    <li>Configurable tracking behavior</li>
</ul>

<h3>Performance Features</h3>
<ul>
    <li><strong>Optional caching layer</strong> - Reduce redundant queries</li>
    <li><strong>Pinned results</strong> - Ensure specific content appears in search</li>
    <li><strong>Optimized synchronization</strong> - Prevents unnecessary uploads of unchanged content</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "gi-external-content",
                    ModuleId = "graph-integration",
                    Title = "External Content Integration",
                    Summary = "Connect external content sources to your CMS through Graph.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Surface external content types in the CMS UI",
                        "Create content type bindings",
                        "Use Visual Builder with external data sources"
                    },
                    Content = @"
<h2>External Content Integration</h2>
<p>CMS 13 enables connecting external content sources (particularly Optimizely Graph) to the CMS interface.</p>

<h3>Key Capabilities</h3>
<ul>
    <li><strong>Surface external content</strong> - Developers can display specific content types in the CMS UI</li>
    <li><strong>Content reuse</strong> - Editors can use external items when composing pages and blocks</li>
    <li><strong>Shadow types</strong> - External sources create ""shadow"" content types automatically</li>
</ul>

<h3>Content Type Binding</h3>
<p>The system includes APIs for creating bindings between content types:</p>
<ul>
    <li>Map properties between existing types including nested blocks</li>
    <li>Full CRUD operations on bindings</li>
    <li>Import/export capabilities</li>
    <li>UI management within content type editing</li>
</ul>

<h3>Visual Builder Integration</h3>
<p>External content integrates with Visual Builder:</p>
<ul>
    <li>Block property binding with data sources</li>
    <li>Drag external content into compositions</li>
    <li>Consistent editing experience</li>
</ul>

<h3>Global Contract Indexing</h3>
<p>Global contracts enable consistent searching across CMS and external sources:</p>
<ul>
    <li>CMS content is indexed to Graph using Global Contracts</li>
    <li>Experience, page, section, and block instances inherit the <code>Item</code> contract</li>
    <li>Media instances inherit both <code>AssetItem</code> and <code>ImageItem</code> contracts</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "gi-indexing",
                    ModuleId = "graph-integration",
                    Title = "Indexing and Performance",
                    Summary = "Optimize content indexing and understand performance improvements.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Monitor indexing with enhanced reporting",
                        "Understand optimized synchronization",
                        "Use Smooth Rebuild for maintenance"
                    },
                    Content = @"
<h2>Indexing and Performance</h2>
<p>CMS 13 includes significant improvements to content indexing performance and monitoring.</p>

<h3>Enhanced Indexing Reporting</h3>
<p>The indexing process now provides detailed metrics:</p>
<ul>
    <li>Real-time progress updates</li>
    <li>Content counts and status</li>
    <li>Error tracking and reporting</li>
    <li>Duration metrics</li>
</ul>

<h3>Optimized Synchronization</h3>
<p>Performance improvements for large datasets:</p>
<ul>
    <li><strong>Document hash values</strong> - Prevents unnecessary uploads of unchanged content</li>
    <li><strong>Incremental updates</strong> - Only changed content is re-indexed</li>
    <li><strong>Batch processing</strong> - Efficient handling of large content volumes</li>
</ul>

<h3>Smooth Rebuild</h3>
<p>A new feature for resetting Graph sources without downtime:</p>
<ul>
    <li>Reset CMS Optimizely Graph source while serving live traffic</li>
    <li>No impact during the reset process</li>
    <li>Verify changes before committing</li>
    <li>Option to abandon changes with no impact</li>
</ul>

<h4>Smooth Rebuild Controls</h4>
<ul>
    <li>Creating deployment slots</li>
    <li>Abandoning slots</li>
    <li>Committing slots</li>
    <li>Rebuilding Graph instances</li>
    <li>Progress monitoring</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Note:</strong> Smooth Rebuild allows resetting Graph sources without downtime. Ensure your Optimizely Graph instance is properly configured before using this feature.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 6: Languages

    private LearningModule BuildLanguagesModule()
    {
        return new LearningModule
        {
            Id = "languages",
            Title = "Multilingual Enhancements",
            Description = "Master the enhanced multilingual capabilities in CMS 13.",
            Icon = "language",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "lang-fallback",
                    ModuleId = "languages",
                    Title = "Global Fallback Languages",
                    Summary = "Configure global fallback languages for enhanced multi-language support.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure global fallback languages",
                        "Understand recursive fallback evaluation",
                        "Prevent circular fallback configurations"
                    },
                    Content = @"
<h2>Global Fallback Languages</h2>
<p>CMS 13 introduces the ability to configure global fallback languages, enhancing multi-language support for both SaaS and PaaS deployments with Optimizely Graph integration.</p>

<h3>Core Capabilities</h3>
<ul>
    <li><strong>Single fallback per language</strong> - Each language can have one designated fallback</li>
    <li><strong>Recursive evaluation</strong> - Fallbacks chain (e.g., fr-BE → nl-BE → nl)</li>
    <li><strong>Circular prevention</strong> - System validates to prevent circular fallbacks</li>
    <li><strong>Automatic cleanup</strong> - Fallback references are cleaned when languages are removed</li>
</ul>

<h3>Example Configuration</h3>
<p>Consider this fallback chain:</p>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>French (Belgium) → Dutch (Belgium) → Dutch
fr-BE            → nl-BE           → nl</code></pre>

<p>If content doesn't exist in <code>fr-BE</code>, the system looks for <code>nl-BE</code>. If that also doesn't exist, it falls back to <code>nl</code>.</p>

<h3>Admin UI Configuration</h3>
<p>Administrators can configure fallbacks through the admin interface:</p>
<ul>
    <li>Apply global fallback settings with user notifications</li>
    <li>Trigger re-indexing of affected content to Optimizely Graph</li>
    <li>Visualize content node configurations for re-indexing</li>
</ul>

<h3>Graph Indexing</h3>
<p>When content is published or deleted, the system manages fallback variants in Graph:</p>
<ul>
    <li>Indexes instances and fallback variants together</li>
    <li>Sets <code>locale</code> to the content instance's locale</li>
    <li>Indexes new fallback versions before deleting old ones (prevents data loss)</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "lang-translation",
                    ModuleId = "languages",
                    Title = "Translation Workflows",
                    Summary = "Use enhanced translation capabilities for efficient localization.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Duplicate content with structures and layouts",
                        "Use auto-translation for quick localization",
                        "Export content for external translation providers"
                    },
                    Content = @"
<h2>Translation Workflows</h2>
<p>CMS 13 provides enhanced tools for efficient content localization.</p>

<h3>Content Duplication Options</h3>

<h4>Full Content Copy</h4>
<p>Copy source content with all structures and layouts intact:</p>
<ul>
    <li>Preserves all content values</li>
    <li>Maintains layout structure</li>
    <li>Ideal for starting translations with existing content as reference</li>
</ul>

<h4>Structure-Only Copy</h4>
<p>Preserve layouts while removing content values:</p>
<ul>
    <li>Maintains page structure</li>
    <li>Clears translatable content fields</li>
    <li>Ready for fresh translation input</li>
</ul>

<h3>Auto-Translation</h3>
<p>Machine-translate content while maintaining structure:</p>
<ul>
    <li>Controlled by feature flag (disabled by default)</li>
    <li>Automatic language variant creation</li>
    <li>Structure preservation during translation</li>
    <li>Automatic context switching post-translation</li>
    <li>Error handling prevents version creation on failures</li>
    <li>Draft content can be translated without prior publication</li>
</ul>

<h3>JSON Export</h3>
<p>Export translatable content for external translation providers:</p>
<ul>
    <li>Standardized JSON format</li>
    <li>Includes all translatable fields</li>
    <li>Compatible with translation management systems</li>
</ul>

<h3>Translation Initiation Points</h3>
<p>Start translations from multiple locations:</p>
<ul>
    <li>Page tree context menus</li>
    <li>Toolbar's yellow ribbon notifications</li>
    <li>Language Selector dropdown</li>
</ul>
<p>The dialog auto-completes source/target languages based on context.</p>
"
                },
                new Lesson
                {
                    Id = "lang-context",
                    ModuleId = "languages",
                    Title = "Language Context Switching",
                    Summary = "Navigate languages efficiently with improved context switching.",
                    Order = 3,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Use global language context switching",
                        "Understand dynamic UI updates",
                        "Navigate multi-language content efficiently"
                    },
                    Content = @"
<h2>Language Context Switching</h2>
<p>The CMS 13 UI dynamically reflects content availability based on the selected language.</p>

<h3>Dynamic UI Updates</h3>
<p>When you switch languages, the interface updates to show:</p>
<ul>
    <li>Content available in the selected language</li>
    <li>Visual indicators for missing translations</li>
    <li>Appropriate creation options based on language permissions</li>
</ul>

<h3>Content Availability</h3>
<p>The system prevents creation in languages not enabled for specific content items, ensuring you only work with valid language configurations.</p>

<h3>Improved Language Dropdown</h3>
<p>The language selector includes usability enhancements:</p>
<ul>
    <li><strong>Search functionality</strong> - Quickly find languages in large lists</li>
    <li><strong>Scrollbar support</strong> - Navigate long language lists easily</li>
    <li><strong>Clear visual hierarchy</strong> - Primary vs. fallback languages</li>
</ul>

<h3>Best Practices</h3>
<ul>
    <li>Set a primary language as your default view</li>
    <li>Use search to quickly switch between distant languages</li>
    <li>Pay attention to visual indicators for missing translations</li>
    <li>Use the yellow ribbon notifications to identify translation needs</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 7: Content Variations

    private LearningModule BuildContentVariationsModule()
    {
        return new LearningModule
        {
            Id = "content-variations",
            Title = "Content Variations",
            Description = "Learn how to create and manage multiple content variations for experimentation.",
            Icon = "document-duplicate",
            Order = 7,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cv-overview",
                    ModuleId = "content-variations",
                    Title = "Understanding Content Variations",
                    Summary = "Learn about the content variations feature for experimentation and personalization.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of content variations",
                        "Learn about delta-based storage architecture",
                        "Manage variations through the API and UI"
                    },
                    Content = @"
<h2>Understanding Content Variations</h2>
<p>Content Variations enables multiple published versions of the same content item within a single language. This is essential for experimentation and personalization.</p>

<h3>Key Capabilities</h3>

<h4>API Management</h4>
<ul>
    <li>List, create, and delete content variations</li>
    <li>Save draft changes and publish/unpublish variations</li>
    <li>Promote variations as the default published version</li>
</ul>

<h4>User Experience</h4>
<ul>
    <li>View and switch between variations in Edit view</li>
    <li>Create new variations from existing content or as empty versions</li>
    <li>Update and preview variations during editing</li>
    <li>Auto-save functionality applies to variations</li>
</ul>

<h3>Delta-Based Storage</h3>
<p>Variations use an efficient <strong>delta-based storage architecture</strong>:</p>
<ul>
    <li><strong>Initial state</strong> - Variations contain no property data initially</li>
    <li><strong>Only changes stored</strong> - Only explicitly modified fields are tracked</li>
    <li><strong>Complex properties</strong> - When modifying values within complex properties, the entire property data is copied</li>
</ul>

<h3>Independent Lifecycle</h3>
<p>Each variation has its own:</p>
<ul>
    <li>Language association</li>
    <li>Version history</li>
    <li>Publishing schedule</li>
</ul>
<p>Variations can be published independently from their source content, provided the source is published.</p>

<h3>Unique Identifiers</h3>
<p>Each variation receives a unique identifier following this format:</p>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>Guid_Status_Language_VariantKey</code></pre>
"
                },
                new Lesson
                {
                    Id = "cv-working",
                    ModuleId = "content-variations",
                    Title = "Working with Variations",
                    Summary = "Create, edit, and promote content variations in practice.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create new content variations",
                        "Switch between variations while editing",
                        "Promote successful variations to the original"
                    },
                    Content = @"
<h2>Working with Variations</h2>
<p>Learn the practical workflows for managing content variations in CMS 13.</p>

<h3>Creating Variations</h3>
<p>You can create new variations in two ways:</p>
<ol>
    <li><strong>From existing content</strong> - Clone the current content as a starting point</li>
    <li><strong>Empty version</strong> - Start fresh with only the base structure</li>
</ol>

<h3>Editing Variations</h3>
<p>When editing variations:</p>
<ul>
    <li>Select the variation from the variation selector in Edit view</li>
    <li>Make changes to the content</li>
    <li>Auto-save preserves your work automatically</li>
    <li>Preview changes before publishing</li>
</ul>

<h3>Publishing Variations</h3>
<p>Variations can be published independently:</p>
<ul>
    <li>The source content must be published first</li>
    <li>Each variation has its own publication status</li>
    <li>Scheduled publishing is supported for variations</li>
</ul>

<h3>Promoting Variations</h3>
<p>When a variation proves successful, you can promote it using <strong>""Copy changes to Original""</strong>:</p>
<ul>
    <li>Merges variation modifications back into the primary content</li>
    <li>Creates a draft of the original if needed</li>
    <li>Preserves the original variation for reference</li>
</ul>

<h3>Graph Integration</h3>
<p>The system indexes all content variations to Optimizely Graph:</p>
<ul>
    <li>Includes unpublished drafts</li>
    <li>Enables use in previews and experiments</li>
    <li>Each variation is uniquely identifiable in queries</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Current Limitations:</strong></p>
    <ul class=""text-yellow-800 dark:text-yellow-100 mt-2"">
        <li>Only localizable properties are supported</li>
        <li>Softlinks aren't generated for published variations</li>
    </ul>
</div>
"
                },
                new Lesson
                {
                    Id = "cv-api",
                    ModuleId = "content-variations",
                    Title = "Variations API Deep Dive",
                    Summary = "Explore the programmatic API for managing content variations.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use IContentVariantRepository for variation management",
                        "Create and modify variations programmatically",
                        "Query and filter variations effectively"
                    },
                    Content = @"
<h2>Variations API Deep Dive</h2>
<p>CMS 13 provides a comprehensive API for managing content variations programmatically, enabling sophisticated experimentation and personalization scenarios.</p>

<h3>IContentVariantRepository</h3>
<p>The central interface for variation management:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public interface IContentVariantRepository
{
    // List all variations for a content item
    IEnumerable&lt;ContentVariant&gt; List(ContentReference contentLink);

    // Create a new variation
    ContentVariant Create(ContentReference contentLink, string variantKey);

    // Delete a variation
    void Delete(ContentReference contentLink, string variantKey);

    // Get a specific variation
    ContentVariant Get(ContentReference contentLink, string variantKey);
}</code></pre>

<h3>Creating Variations Programmatically</h3>
<p>Create variations for A/B testing or personalization:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public class VariationService
{
    private readonly IContentVariantRepository _variantRepository;
    private readonly IContentRepository _contentRepository;

    public void CreateExperimentVariation(ContentReference contentLink)
    {
        // Create a new variation with a unique key
        var variant = _variantRepository.Create(
            contentLink,
            $""experiment-{Guid.NewGuid():N}""
        );

        // Get the variation as editable content
        var content = _contentRepository.Get&lt;IContent&gt;(
            variant.ContentLink
        ).CreateWritableClone();

        // Modify properties
        if (content is IContentData pageData)
        {
            pageData.Property[""Heading""].Value = ""Variation B Heading"";
        }

        // Save the variation
        _contentRepository.Save(content, SaveAction.Publish);
    }
}</code></pre>

<h3>Querying Variations</h3>
<p>Retrieve and filter variations:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// List all variations for a page
var variations = _variantRepository.List(pageReference);

foreach (var variant in variations)
{
    Console.WriteLine($""Key: {variant.VariantKey}"");
    Console.WriteLine($""Status: {variant.Status}"");
    Console.WriteLine($""Created: {variant.Created}"");
}</code></pre>

<h3>Variation Lifecycle Management</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Operation</th>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Create</td><td class=""px-4 py-2""><code>Create()</code></td><td class=""px-4 py-2"">Creates a new variation with specified key</td></tr>
        <tr><td class=""px-4 py-2"">Read</td><td class=""px-4 py-2""><code>Get()</code></td><td class=""px-4 py-2"">Retrieves a specific variation</td></tr>
        <tr><td class=""px-4 py-2"">List</td><td class=""px-4 py-2""><code>List()</code></td><td class=""px-4 py-2"">Lists all variations for content</td></tr>
        <tr><td class=""px-4 py-2"">Delete</td><td class=""px-4 py-2""><code>Delete()</code></td><td class=""px-4 py-2"">Removes a variation permanently</td></tr>
        <tr><td class=""px-4 py-2"">Promote</td><td class=""px-4 py-2"">Copy to Original</td><td class=""px-4 py-2"">Merge variation changes to primary content</td></tr>
    </tbody>
</table>

<h3>Best Practices</h3>
<ul>
    <li><strong>Naming conventions</strong> - Use descriptive variant keys that indicate purpose (e.g., <code>experiment-cta-color</code>)</li>
    <li><strong>Cleanup</strong> - Delete variations after experiments conclude to maintain database hygiene</li>
    <li><strong>Permissions</strong> - Ensure proper access rights for variation management</li>
    <li><strong>Auditing</strong> - Track variation creation and modifications for compliance</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "cv-graph",
                    ModuleId = "content-variations",
                    Title = "Graph Integration for Variations",
                    Summary = "Learn how content variations are indexed and queried through Optimizely Graph.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand how variations are indexed to Graph",
                        "Query variations using GraphQL",
                        "Use variations for experimentation with Graph"
                    },
                    Content = @"
<h2>Graph Integration for Variations</h2>
<p>Content variations are automatically indexed to Optimizely Graph, enabling powerful querying capabilities for experimentation and personalization.</p>

<h3>Automatic Indexing</h3>
<p>When variations are created or modified, CMS 13 automatically indexes them to Graph:</p>
<ul>
    <li><strong>Published variations</strong> - Indexed as separate documents</li>
    <li><strong>Draft variations</strong> - Also indexed for preview scenarios</li>
    <li><strong>Unique identifiers</strong> - Each variation has a distinct Graph ID</li>
</ul>

<h3>Variation Identifier Format</h3>
<p>Variations in Graph follow this identifier pattern:</p>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>Guid_Status_Language_VariantKey

Example:
a1b2c3d4-5678-90ab-cdef-123456789abc_Published_en_experiment-v1</code></pre>

<h3>Querying Variations with GraphQL</h3>
<p>Retrieve specific variations:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>query GetVariations($contentId: String!) {
  ArticlePage(
    where: {
      _metadata: {
        key: { eq: $contentId }
      }
    }
  ) {
    items {
      _metadata {
        key
        displayName
        version
      }
      Heading
      MainBody
      _variations {
        key
        status
      }
    }
  }
}</code></pre>

<h3>Filtering by Variation</h3>
<p>Query a specific variation for A/B testing:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>query GetExperimentVariation {
  ArticlePage(
    where: {
      _variations: {
        key: { eq: ""experiment-cta-blue"" }
      }
    }
  ) {
    items {
      Heading
      CallToActionText
      CallToActionUrl
    }
  }
}</code></pre>

<h3>Experimentation Workflow</h3>
<ol>
    <li><strong>Create variations</strong> in the CMS for your experiment</li>
    <li><strong>Variations are indexed</strong> automatically to Graph</li>
    <li><strong>Frontend queries</strong> Graph for the appropriate variation based on user segment</li>
    <li><strong>Track results</strong> using your analytics platform</li>
    <li><strong>Promote winner</strong> using ""Copy changes to Original""</li>
</ol>

<h3>Performance Considerations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Consideration</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Index Size</td><td class=""px-4 py-2"">Each variation increases index size; clean up unused variations</td></tr>
        <tr><td class=""px-4 py-2"">Query Complexity</td><td class=""px-4 py-2"">Filtering by variation adds minimal overhead</td></tr>
        <tr><td class=""px-4 py-2"">Sync Time</td><td class=""px-4 py-2"">Variations sync to Graph within standard indexing intervals</td></tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Tip:</strong> Use Graph's caching layer to improve performance when serving variations to high-traffic pages.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 8: Framework Changes

    private LearningModule BuildFrameworkModule()
    {
        return new LearningModule
        {
            Id = "framework",
            Title = "Framework and Infrastructure",
            Description = "Understand the infrastructure changes and .NET 10 upgrade in CMS 13.",
            Icon = "cog",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "fw-net10",
                    ModuleId = "framework",
                    Title = ".NET 10 Runtime",
                    Summary = "Understand the upgrade to .NET 10 and its implications.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the .NET 10 upgrade",
                        "Plan your migration path",
                        "Leverage new .NET features"
                    },
                    Content = @"
<h2>.NET 10 Runtime</h2>
<p>CMS 13 now operates on the <strong>.NET 10 runtime</strong>, representing a significant platform modernization.</p>

<h3>Why .NET 10?</h3>
<p>.NET 10 brings several benefits:</p>
<ul>
    <li><strong>Performance improvements</strong> - Faster startup and runtime performance</li>
    <li><strong>Security updates</strong> - Latest security patches and features</li>
    <li><strong>Modern language features</strong> - C# 13+ capabilities</li>
    <li><strong>Long-term support</strong> - Extended support timeline</li>
</ul>

<h3>Migration Considerations</h3>
<p>When upgrading from CMS 12 to CMS 13, consider:</p>
<ul>
    <li>Update your project target framework to .NET 10</li>
    <li>Review NuGet package compatibility</li>
    <li>Test third-party integrations</li>
    <li>Update CI/CD pipelines</li>
</ul>

<h3>Project File Updates</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>&lt;Project Sdk=""Microsoft.NET.Sdk.Web""&gt;
  &lt;PropertyGroup&gt;
    &lt;TargetFramework&gt;net10.0&lt;/TargetFramework&gt;
    &lt;Nullable&gt;enable&lt;/Nullable&gt;
    &lt;ImplicitUsings&gt;enable&lt;/ImplicitUsings&gt;
  &lt;/PropertyGroup&gt;
&lt;/Project&gt;</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Tip:</strong> Review the .NET 10 breaking changes documentation before upgrading to identify any code changes needed.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "fw-admin-changes",
                    ModuleId = "framework",
                    Title = "Admin Interface Changes",
                    Summary = "Learn about changes to the administrative interface.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the relocated cloud license management",
                        "Understand the Plugin Manager removal",
                        "Adapt to new scheduled job patterns"
                    },
                    Content = @"
<h2>Admin Interface Changes</h2>
<p>CMS 13 includes several changes to the administrative interface.</p>

<h3>Cloud License Management</h3>
<p>The cloud license management interface has been relocated:</p>
<ul>
    <li><strong>Old location:</strong> Settings > Manage Websites > Cloud License</li>
    <li><strong>New location:</strong> Settings > Cloud License (dedicated menu item)</li>
</ul>

<p>The new interface is under the ""Admin (Framework)"" section and replicates previous functionality while adding quality-of-life improvements.</p>

<h3>Plugin Manager Removal</h3>
<p>The Plugin Manager has been discontinued:</p>
<ul>
    <li>The Plugin Manager UI and backend components have been removed</li>
    <li>The feature was underutilized and has been eliminated</li>
    <li>Scheduled jobs no longer depend on the obsoleted <code>EPiServer.PlugIn</code> system</li>
    <li>Custom property types no longer depend on the plugin system</li>
</ul>

<h3>Implications for Existing Code</h3>
<p>If you have code that relies on the plugin system:</p>
<ul>
    <li>Review scheduled jobs for plugin dependencies</li>
    <li>Update custom property type registrations</li>
    <li>Consider modern dependency injection patterns instead</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Migration Note:</strong> If you have custom scheduled jobs using the <code>[ScheduledPlugIn]</code> attribute, you'll need to update them to use the modern registration pattern.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "fw-import-export",
                    ModuleId = "framework",
                    Title = "Import and Export Improvements",
                    Summary = "Explore the enhanced import and export capabilities for large sites.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Handle exports exceeding 2GB",
                        "Monitor export progress with client-side polling",
                        "Identify and resolve import failures"
                    },
                    Content = @"
<h2>Import and Export Improvements</h2>
<p>CMS 13 includes enhanced capabilities for handling large site exports and imports.</p>

<h3>Large File Support</h3>
<p>The system now handles files exceeding 2GB:</p>
<ul>
    <li>Surpasses previous file size limitations</li>
    <li>Enables full-site exports for large content repositories</li>
    <li>Background processing for long operations</li>
</ul>

<h3>Background Processing</h3>
<p>Export operations are no longer dependent on a single continuous request:</p>
<ul>
    <li>Works around Azure's 230-second timeout constraint</li>
    <li>Client-side polling monitors export progress</li>
    <li>Updated UI labels reflect operation status (e.g., ""Upload is in progress"")</li>
</ul>

<h3>Improved Error Visibility</h3>
<p>Import error handling has been enhanced:</p>
<ul>
    <li>Error messages remain visible on the page after import</li>
    <li>Administrators can identify and address import failures quickly</li>
    <li>Clear indication of which items failed and why</li>
</ul>

<h3>Compatibility</h3>
<p>The baseline import functionality carries forward from CMS 12, ensuring:</p>
<ul>
    <li>Export packages from CMS 12 can be imported (check migration guide for specifics)</li>
    <li>Familiar workflows for content migration</li>
    <li>Same core functionality with enhanced reliability</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Tip:</strong> For very large exports, monitor the progress indicators and allow sufficient time for background processing to complete.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "fw-dependency-injection",
                    ModuleId = "framework",
                    Title = "Dependency Injection Changes",
                    Summary = "Understand the shift from service locator patterns to constructor injection in CMS 13.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Migrate from ServiceLocator to constructor injection",
                        "Use IServiceProvider for dynamic resolution",
                        "Understand the deprecation of legacy patterns"
                    },
                    Content = @"
<h2>Dependency Injection Changes</h2>
<p>CMS 13 fully embraces modern .NET dependency injection patterns, deprecating the legacy ServiceLocator approach used in earlier versions.</p>

<h3>What's Changing</h3>
<p>The <code>ServiceLocator</code> pattern is deprecated in favor of constructor injection:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">CMS 12 (Legacy)</th>
            <th class=""px-4 py-2 text-left"">CMS 13 (Modern)</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class=""px-4 py-2""><code>ServiceLocator.Current.GetInstance&lt;T&gt;()</code></td>
            <td class=""px-4 py-2"">Constructor injection</td>
        </tr>
        <tr>
            <td class=""px-4 py-2""><code>EPiServer.ServiceLocation</code></td>
            <td class=""px-4 py-2""><code>Microsoft.Extensions.DependencyInjection</code></td>
        </tr>
    </tbody>
</table>

<h3>Before: ServiceLocator Pattern</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12 approach (deprecated)
public class MyService
{
    public void DoSomething()
    {
        var contentLoader = ServiceLocator.Current
            .GetInstance&lt;IContentLoader&gt;();
        var content = contentLoader.Get&lt;IContent&gt;(contentLink);
    }
}</code></pre>

<h3>After: Constructor Injection</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 13 approach (recommended)
public class MyService
{
    private readonly IContentLoader _contentLoader;

    public MyService(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    public void DoSomething()
    {
        var content = _contentLoader.Get&lt;IContent&gt;(contentLink);
    }
}</code></pre>

<h3>Dynamic Resolution with IServiceProvider</h3>
<p>When you need to resolve services dynamically (e.g., in factories):</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public class ServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T GetService&lt;T&gt;() where T : class
    {
        return _serviceProvider.GetService&lt;T&gt;();
    }

    public T GetRequiredService&lt;T&gt;() where T : class
    {
        return _serviceProvider.GetRequiredService&lt;T&gt;();
    }
}</code></pre>

<h3>Registering Services</h3>
<p>Use standard ASP.NET Core patterns in <code>Startup.cs</code> or <code>Program.cs</code>:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>services.AddScoped&lt;IMyService, MyService&gt;();
services.AddSingleton&lt;ICacheService, CacheService&gt;();
services.AddTransient&lt;IDataProcessor, DataProcessor&gt;();</code></pre>

<h3>Migration Checklist</h3>
<ul>
    <li>Search codebase for <code>ServiceLocator.Current</code></li>
    <li>Replace with constructor injection where possible</li>
    <li>Use <code>IServiceProvider</code> for factory patterns</li>
    <li>Update unit tests to use proper mocking</li>
    <li>Remove references to <code>EPiServer.ServiceLocation</code></li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Warning:</strong> Code using <code>ServiceLocator</code> will still compile but may produce runtime warnings or behave unexpectedly in CMS 13.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "fw-api-changes",
                    ModuleId = "framework",
                    Title = "API Breaking Changes",
                    Summary = "Learn about the significant API changes and deprecated types in CMS 13.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Identify breaking API changes from CMS 12",
                        "Update code using PageReference to ContentReference",
                        "Handle IContentTypeRepository changes"
                    },
                    Content = @"
<h2>API Breaking Changes</h2>
<p>CMS 13 introduces several API changes that require code updates when migrating from CMS 12.</p>

<h3>PageReference → ContentReference</h3>
<p>The <code>PageReference</code> type has been fully deprecated. Use <code>ContentReference</code> everywhere:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">CMS 12</th>
            <th class=""px-4 py-2 text-left"">CMS 13</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>PageReference</code></td><td class=""px-4 py-2""><code>ContentReference</code></td></tr>
        <tr><td class=""px-4 py-2""><code>PageReference.StartPage</code></td><td class=""px-4 py-2""><code>ContentReference.StartPage</code></td></tr>
        <tr><td class=""px-4 py-2""><code>PageReference.EmptyReference</code></td><td class=""px-4 py-2""><code>ContentReference.EmptyReference</code></td></tr>
        <tr><td class=""px-4 py-2""><code>PageReference.RootPage</code></td><td class=""px-4 py-2""><code>ContentReference.RootPage</code></td></tr>
    </tbody>
</table>

<h3>Code Migration Example</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12
public void ProcessPage(PageReference pageRef)
{
    if (pageRef != PageReference.EmptyReference)
    {
        var children = _contentLoader.GetChildren&lt;PageData&gt;(pageRef);
    }
}

// CMS 13
public void ProcessPage(ContentReference contentRef)
{
    if (contentRef != ContentReference.EmptyReference)
    {
        var children = _contentLoader.GetChildren&lt;PageData&gt;(contentRef);
    }
}</code></pre>

<h3>IContentTypeRepository Changes</h3>
<p>The <code>IContentTypeRepository</code> interface is now non-generic:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12
var contentType = contentTypeRepository.Load&lt;ArticlePage&gt;();

// CMS 13
var contentType = contentTypeRepository.Load(typeof(ArticlePage));</code></pre>

<h3>Removed Types and Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Removed</th>
            <th class=""px-4 py-2 text-left"">Replacement</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>ScheduledPlugIn</code> attribute</td><td class=""px-4 py-2"">Modern registration pattern</td></tr>
        <tr><td class=""px-4 py-2""><code>EPiServer.PlugIn</code> namespace</td><td class=""px-4 py-2"">Standard DI patterns</td></tr>
        <tr><td class=""px-4 py-2""><code>SiteDefinition</code></td><td class=""px-4 py-2""><code>IApplicationRepository</code></td></tr>
        <tr><td class=""px-4 py-2""><code>ISiteDefinitionRepository</code></td><td class=""px-4 py-2""><code>IApplicationRepository</code></td></tr>
    </tbody>
</table>

<h3>Scheduled Jobs Migration</h3>
<p>If you have custom scheduled jobs using the plugin attribute:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12 (deprecated)
[ScheduledPlugIn(DisplayName = ""My Job"")]
public class MyScheduledJob : ScheduledJobBase
{
    public override string Execute() { ... }
}

// CMS 13
[ScheduledJob(
    Guid = ""a1b2c3d4-5678-90ab-cdef-123456789abc"",
    Name = ""My Job"")]
public class MyScheduledJob : ScheduledJob
{
    public override DataStatus Execute(CancellationToken cancellationToken)
    { ... }
}</code></pre>

<h3>Migration Strategy</h3>
<ol>
    <li><strong>Compile with warnings</strong> - CMS 13 will flag deprecated usages</li>
    <li><strong>Search and replace</strong> - Use IDE tools for bulk <code>PageReference</code> → <code>ContentReference</code></li>
    <li><strong>Update interfaces</strong> - Review all injected services for API changes</li>
    <li><strong>Test thoroughly</strong> - Changes may have subtle behavioral differences</li>
</ol>

<div class=""bg-red-50 dark:bg-red-900 border-l-4 border-red-400 p-4 my-4"">
    <p class=""text-red-800 dark:text-red-100""><strong>Breaking Change:</strong> Using deprecated APIs may result in compilation errors or runtime exceptions in CMS 13. Address all deprecation warnings before deployment.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 9: Applications Management

    private LearningModule BuildApplicationsModule()
    {
        return new LearningModule
        {
            Id = "applications",
            Title = "Applications Management",
            Description = "Learn about the new Application model that replaces SiteDefinition in CMS 13.",
            Icon = "building-office",
            Order = 9,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "overview" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "app-introduction",
                    ModuleId = "applications",
                    Title = "Introduction to Applications",
                    Summary = "Understand the Application model that replaces SiteDefinition in CMS 13.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the Application model concept",
                        "Learn why SiteDefinition was replaced",
                        "Identify benefits of the new approach"
                    },
                    Content = @"
<h2>Introduction to Applications</h2>
<p>CMS 13 introduces a new <strong>Application model</strong> that replaces the <code>SiteDefinition</code> concept from previous versions. This change provides a more flexible and modern approach to managing multi-tenant and multi-site configurations.</p>

<h3>What's Changing?</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">CMS 12</th>
            <th class=""px-4 py-2 text-left"">CMS 13</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>SiteDefinition</code></td><td class=""px-4 py-2""><code>Application</code></td></tr>
        <tr><td class=""px-4 py-2""><code>ISiteDefinitionRepository</code></td><td class=""px-4 py-2""><code>IApplicationRepository</code></td></tr>
        <tr><td class=""px-4 py-2""><code>SiteDefinition.Current</code></td><td class=""px-4 py-2""><code>IApplicationResolver</code></td></tr>
        <tr><td class=""px-4 py-2""><code>SiteDefinitionResolver</code></td><td class=""px-4 py-2""><code>IApplicationResolver</code></td></tr>
    </tbody>
</table>

<h3>Why the Change?</h3>
<p>The Application model offers several advantages:</p>
<ul>
    <li><strong>Cleaner abstraction</strong> - Better separation of concerns between site configuration and content</li>
    <li><strong>Multi-tenant support</strong> - Improved support for SaaS-like deployments</li>
    <li><strong>Graph alignment</strong> - Better integration with Optimizely Graph's source concept</li>
    <li><strong>Modern patterns</strong> - Follows contemporary .NET architectural patterns</li>
</ul>

<h3>Core Concepts</h3>
<h4>Application</h4>
<p>An Application represents a logical grouping of content with its own:</p>
<ul>
    <li>Start page and root content</li>
    <li>Host bindings (domains)</li>
    <li>Language configurations</li>
    <li>Settings and preferences</li>
</ul>

<h4>Application Resolution</h4>
<p>The system resolves the current application based on:</p>
<ul>
    <li>HTTP request host header</li>
    <li>Request path</li>
    <li>Configuration rules</li>
</ul>

<h3>Migration Path</h3>
<p>Existing <code>SiteDefinition</code> data is migrated automatically during upgrade, but your code needs updates to use the new APIs. The following lessons cover the specific changes needed.</p>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Note:</strong> The underlying data model for sites is preserved during migration. Applications map 1:1 to previous site definitions.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "app-repository",
                    ModuleId = "applications",
                    Title = "IApplicationRepository",
                    Summary = "Learn to perform CRUD operations on applications using the repository interface.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Use IApplicationRepository for application management",
                        "Create, read, update, and delete applications",
                        "Query applications programmatically"
                    },
                    Content = @"
<h2>IApplicationRepository</h2>
<p>The <code>IApplicationRepository</code> interface provides programmatic access to manage applications in CMS 13.</p>

<h3>Interface Overview</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public interface IApplicationRepository
{
    // Query operations
    Application Get(Guid id);
    Application GetByName(string name);
    IEnumerable&lt;Application&gt; List();

    // Write operations
    Guid Save(Application application);
    void Delete(Guid id);
}</code></pre>

<h3>Injecting the Repository</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public class ApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
}</code></pre>

<h3>Listing All Applications</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public IEnumerable&lt;ApplicationInfo&gt; GetAllApplications()
{
    return _applicationRepository.List()
        .Select(app => new ApplicationInfo
        {
            Id = app.Id,
            Name = app.Name,
            StartPageId = app.StartPage,
            Hosts = app.Hosts.Select(h => h.Name).ToList()
        });
}</code></pre>

<h3>Getting a Specific Application</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// By ID
var application = _applicationRepository.Get(applicationId);

// By name
var application = _applicationRepository.GetByName(""corporate-site"");</code></pre>

<h3>Creating a New Application</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public Guid CreateApplication(string name, ContentReference startPage)
{
    var application = new Application
    {
        Name = name,
        StartPage = startPage,
        Hosts = new List&lt;HostDefinition&gt;
        {
            new HostDefinition
            {
                Name = ""www.example.com"",
                Type = HostDefinitionType.Primary
            }
        }
    };

    return _applicationRepository.Save(application);
}</code></pre>

<h3>Updating an Application</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public void UpdateApplicationHosts(Guid appId, IEnumerable&lt;string&gt; hostNames)
{
    var application = _applicationRepository.Get(appId);

    application.Hosts = hostNames.Select(h => new HostDefinition
    {
        Name = h,
        Type = HostDefinitionType.Primary
    }).ToList();

    _applicationRepository.Save(application);
}</code></pre>

<h3>Migration from ISiteDefinitionRepository</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">ISiteDefinitionRepository</th>
            <th class=""px-4 py-2 text-left"">IApplicationRepository</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>Get(Guid id)</code></td><td class=""px-4 py-2""><code>Get(Guid id)</code></td></tr>
        <tr><td class=""px-4 py-2""><code>List()</code></td><td class=""px-4 py-2""><code>List()</code></td></tr>
        <tr><td class=""px-4 py-2""><code>Save(SiteDefinition)</code></td><td class=""px-4 py-2""><code>Save(Application)</code></td></tr>
        <tr><td class=""px-4 py-2""><code>Delete(Guid id)</code></td><td class=""px-4 py-2""><code>Delete(Guid id)</code></td></tr>
    </tbody>
</table>
"
                },
                new Lesson
                {
                    Id = "app-resolver",
                    ModuleId = "applications",
                    Title = "IApplicationResolver",
                    Summary = "Learn to resolve the current application context in requests.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use IApplicationResolver to get current application",
                        "Understand context-based resolution",
                        "Handle multi-tenant scenarios"
                    },
                    Content = @"
<h2>IApplicationResolver</h2>
<p>The <code>IApplicationResolver</code> service resolves the current application based on the HTTP request context, replacing <code>SiteDefinition.Current</code>.</p>

<h3>Interface</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public interface IApplicationResolver
{
    Application GetCurrentApplication();
    Application GetApplicationForRequest(HttpContext context);
    Application GetApplicationByHost(string hostName);
}</code></pre>

<h3>Migration from SiteDefinition.Current</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12 (deprecated)
var currentSite = SiteDefinition.Current;
var startPage = currentSite.StartPage;

// CMS 13
public class MyController : Controller
{
    private readonly IApplicationResolver _applicationResolver;

    public MyController(IApplicationResolver applicationResolver)
    {
        _applicationResolver = applicationResolver;
    }

    public IActionResult Index()
    {
        var currentApp = _applicationResolver.GetCurrentApplication();
        var startPage = currentApp.StartPage;
        // ...
    }
}</code></pre>

<h3>Getting Current Application</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public class NavigationService
{
    private readonly IApplicationResolver _resolver;
    private readonly IContentLoader _contentLoader;

    public NavigationService(
        IApplicationResolver resolver,
        IContentLoader contentLoader)
    {
        _resolver = resolver;
        _contentLoader = contentLoader;
    }

    public IContent GetStartPage()
    {
        var app = _resolver.GetCurrentApplication();
        return _contentLoader.Get&lt;IContent&gt;(app.StartPage);
    }

    public IEnumerable&lt;HostDefinition&gt; GetCurrentHosts()
    {
        return _resolver.GetCurrentApplication().Hosts;
    }
}</code></pre>

<h3>Host-Based Resolution</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Resolve application by specific host
var app = _applicationResolver.GetApplicationByHost(""www.corporate.com"");

// Useful for:
// - URL generation for different sites
// - Cross-site content references
// - Email templates with absolute URLs</code></pre>

<h3>Multi-Tenant Scenarios</h3>
<p>For SaaS or multi-tenant deployments:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public class TenantService
{
    private readonly IApplicationResolver _resolver;

    public string GetTenantIdentifier()
    {
        var app = _resolver.GetCurrentApplication();
        return app.Name; // Or use a custom property
    }

    public bool IsCurrentTenant(string tenantId)
    {
        var app = _resolver.GetCurrentApplication();
        return app.Name.Equals(tenantId, StringComparison.OrdinalIgnoreCase);
    }
}</code></pre>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Important:</strong> <code>GetCurrentApplication()</code> requires an active HTTP context. In background jobs or services without HTTP context, use <code>IApplicationRepository.List()</code> instead.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "app-configuration",
                    ModuleId = "applications",
                    Title = "Application Configuration",
                    Summary = "Configure application settings, domains, and start pages.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure application hosts and domains",
                        "Set start pages and root content",
                        "Manage application-specific settings"
                    },
                    Content = @"
<h2>Application Configuration</h2>
<p>Applications in CMS 13 support rich configuration options for domains, content roots, and custom settings.</p>

<h3>Application Properties</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>Id</code></td><td class=""px-4 py-2"">Unique identifier (GUID)</td></tr>
        <tr><td class=""px-4 py-2""><code>Name</code></td><td class=""px-4 py-2"">Application name for identification</td></tr>
        <tr><td class=""px-4 py-2""><code>StartPage</code></td><td class=""px-4 py-2"">ContentReference to the start page</td></tr>
        <tr><td class=""px-4 py-2""><code>ContentRootId</code></td><td class=""px-4 py-2"">Root content folder reference</td></tr>
        <tr><td class=""px-4 py-2""><code>Hosts</code></td><td class=""px-4 py-2"">Collection of host definitions</td></tr>
        <tr><td class=""px-4 py-2""><code>AssetsRootId</code></td><td class=""px-4 py-2"">Root folder for media assets</td></tr>
    </tbody>
</table>

<h3>Host Configuration</h3>
<p>Configure domains and hosts for your application:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>var application = new Application
{
    Name = ""Corporate Site"",
    StartPage = startPageReference,
    Hosts = new List&lt;HostDefinition&gt;
    {
        new HostDefinition
        {
            Name = ""www.corporate.com"",
            Type = HostDefinitionType.Primary,
            UseSecureConnection = true
        },
        new HostDefinition
        {
            Name = ""corporate.com"",
            Type = HostDefinitionType.Redirect,
            UseSecureConnection = true
        },
        new HostDefinition
        {
            Name = ""*"",
            Type = HostDefinitionType.Undefined // Wildcard
        }
    }
};</code></pre>

<h3>Host Definition Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>Primary</code></td><td class=""px-4 py-2"">Main host used for canonical URLs</td></tr>
        <tr><td class=""px-4 py-2""><code>Redirect</code></td><td class=""px-4 py-2"">Redirects to primary host</td></tr>
        <tr><td class=""px-4 py-2""><code>Edit</code></td><td class=""px-4 py-2"">Host used for editing context</td></tr>
        <tr><td class=""px-4 py-2""><code>Undefined</code></td><td class=""px-4 py-2"">Wildcard matching</td></tr>
    </tbody>
</table>

<h3>Language Configuration</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Configure host-specific languages
var host = new HostDefinition
{
    Name = ""www.corporate.de"",
    Type = HostDefinitionType.Primary,
    Language = new CultureInfo(""de-DE"")
};</code></pre>

<h3>Admin UI Configuration</h3>
<p>Applications can also be configured through the Admin UI:</p>
<ol>
    <li>Navigate to <strong>Admin &gt; Config &gt; Manage Applications</strong></li>
    <li>Select an existing application or create new</li>
    <li>Configure hosts, start page, and settings</li>
    <li>Save changes</li>
</ol>

<h3>Programmatic Settings Access</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public class ApplicationSettingsService
{
    private readonly IApplicationResolver _resolver;

    public bool IsSecureConnectionRequired()
    {
        var app = _resolver.GetCurrentApplication();
        var primaryHost = app.Hosts
            .FirstOrDefault(h => h.Type == HostDefinitionType.Primary);

        return primaryHost?.UseSecureConnection ?? false;
    }

    public string GetPrimaryHostUrl()
    {
        var app = _resolver.GetCurrentApplication();
        var primaryHost = app.Hosts
            .FirstOrDefault(h => h.Type == HostDefinitionType.Primary);

        var scheme = primaryHost?.UseSecureConnection == true ? ""https"" : ""http"";
        return $""{scheme}://{primaryHost?.Name}"";
    }
}</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Best Practice:</strong> Use the Primary host type for your main domain and configure redirects for alternative domains (www vs non-www, old domains, etc.).</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 10: Preview for Headless Sites

    private LearningModule BuildHeadlessPreviewModule()
    {
        return new LearningModule
        {
            Id = "headless-preview",
            Title = "Preview for Headless Sites",
            Description = "Configure and use preview functionality for decoupled frontend applications.",
            Icon = "device-phone-mobile",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "graph-integration" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "hp-overview",
                    ModuleId = "headless-preview",
                    Title = "Headless Preview Overview",
                    Summary = "Understand the preview architecture for decoupled frontend applications.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand headless preview architecture",
                        "Learn how Graph enables preview for decoupled sites",
                        "Identify preview requirements for your frontend"
                    },
                    Content = @"
<h2>Headless Preview Overview</h2>
<p>CMS 13 introduces robust preview capabilities for headless architectures, enabling content editors to preview changes in decoupled frontend applications.</p>

<h3>The Headless Challenge</h3>
<p>In traditional CMS architectures, preview is straightforward—the CMS renders the page. In headless setups:</p>
<ul>
    <li>Content lives in the CMS</li>
    <li>Rendering happens in a separate frontend application</li>
    <li>Preview requires coordination between systems</li>
</ul>

<h3>CMS 13 Preview Architecture</h3>
<p>CMS 13 solves this with a Graph-based approach:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Component</th>
            <th class=""px-4 py-2 text-left"">Role</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">CMS</td><td class=""px-4 py-2"">Stores content, provides edit UI, triggers preview</td></tr>
        <tr><td class=""px-4 py-2"">Optimizely Graph</td><td class=""px-4 py-2"">Indexes draft content, serves preview queries</td></tr>
        <tr><td class=""px-4 py-2"">Frontend</td><td class=""px-4 py-2"">Fetches from Graph, renders preview</td></tr>
        <tr><td class=""px-4 py-2"">Visual Builder</td><td class=""px-4 py-2"">Orchestrates preview in iframe</td></tr>
    </tbody>
</table>

<h3>Preview Flow</h3>
<ol>
    <li><strong>Editor makes changes</strong> in the CMS</li>
    <li><strong>Draft is indexed</strong> to Graph (including unpublished content)</li>
    <li><strong>Preview triggers</strong> - CMS constructs preview URL</li>
    <li><strong>Frontend receives request</strong> with preview token</li>
    <li><strong>Frontend queries Graph</strong> for draft content</li>
    <li><strong>Preview renders</strong> in Visual Builder iframe</li>
</ol>

<h3>Key Capabilities</h3>
<ul>
    <li><strong>Draft preview</strong> - View unpublished content changes</li>
    <li><strong>Variations preview</strong> - Preview content variations</li>
    <li><strong>Cross-origin support</strong> - Frontend can be on different domain</li>
    <li><strong>Secure access</strong> - Preview tokens authenticate requests</li>
</ul>

<h3>Requirements</h3>
<p>To enable headless preview:</p>
<ul>
    <li>Optimizely Graph integration configured</li>
    <li>Frontend capable of receiving preview requests</li>
    <li>Preview URL patterns configured in CMS</li>
    <li>CORS policies allowing CMS-to-frontend communication</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Note:</strong> Headless preview works with any frontend framework (Next.js, Nuxt, Gatsby, etc.) that can query Optimizely Graph.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "hp-urls",
                    ModuleId = "headless-preview",
                    Title = "Configuring Preview URLs",
                    Summary = "Set up preview URL patterns for your headless frontend.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure preview URL patterns",
                        "Implement preview endpoints in your frontend",
                        "Handle authentication for preview requests"
                    },
                    Content = @"
<h2>Configuring Preview URLs</h2>
<p>Preview URL configuration tells CMS 13 how to construct URLs that your frontend application will handle for preview rendering.</p>

<h3>Preview URL Pattern</h3>
<p>Configure in your application settings:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>{
  ""Optimizely"": {
    ""Preview"": {
      ""BaseUrl"": ""https://frontend.example.com"",
      ""PreviewPath"": ""/api/preview"",
      ""ExitPreviewPath"": ""/api/exit-preview""
    }
  }
}</code></pre>

<h3>URL Construction</h3>
<p>CMS 13 constructs preview URLs with these parameters:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Parameter</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>token</code></td><td class=""px-4 py-2"">Encrypted preview authentication token</td></tr>
        <tr><td class=""px-4 py-2""><code>contentId</code></td><td class=""px-4 py-2"">Content GUID being previewed</td></tr>
        <tr><td class=""px-4 py-2""><code>locale</code></td><td class=""px-4 py-2"">Language code for the content</td></tr>
        <tr><td class=""px-4 py-2""><code>variation</code></td><td class=""px-4 py-2"">Variation key (if previewing a variation)</td></tr>
    </tbody>
</table>

<h3>Example Preview URL</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>https://frontend.example.com/api/preview?
  token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
  &amp;contentId=a1b2c3d4-5678-90ab-cdef-123456789abc
  &amp;locale=en
  &amp;variation=experiment-v1</code></pre>

<h3>Frontend Preview Handler (Next.js Example)</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// pages/api/preview.ts
import { NextApiRequest, NextApiResponse } from 'next';

export default async function handler(
  req: NextApiRequest,
  res: NextApiResponse
) {
  const { token, contentId, locale, variation } = req.query;

  // Validate token with CMS
  const isValid = await validatePreviewToken(token as string);
  if (!isValid) {
    return res.status(401).json({ message: 'Invalid token' });
  }

  // Enable preview mode
  res.setPreviewData({
    contentId,
    locale,
    variation,
    token
  });

  // Redirect to the content page
  const path = await resolveContentPath(contentId as string);
  res.redirect(path);
}</code></pre>

<h3>Token Validation</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Validate token against CMS endpoint
async function validatePreviewToken(token: string): Promise&lt;boolean&gt; {
  const response = await fetch(
    `${process.env.CMS_URL}/api/preview/validate`,
    {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ token })
    }
  );
  return response.ok;
}</code></pre>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Security Note:</strong> Always validate preview tokens server-side. Never expose draft content without proper authentication.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "hp-visual-builder",
                    ModuleId = "headless-preview",
                    Title = "Preview in Visual Builder",
                    Summary = "Integrate your headless frontend preview with Visual Builder.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Display headless preview in Visual Builder iframe",
                        "Handle cross-origin communication",
                        "Implement live preview updates"
                    },
                    Content = @"
<h2>Preview in Visual Builder</h2>
<p>Visual Builder displays your headless frontend in an iframe, enabling editors to see changes as they make them.</p>

<h3>Iframe Integration</h3>
<p>Visual Builder loads your frontend preview in an iframe:</p>
<ul>
    <li>Preview URL is loaded with authentication token</li>
    <li>Frontend renders content from Graph</li>
    <li>Changes sync through Graph in near real-time</li>
</ul>

<h3>Cross-Origin Configuration</h3>
<p>Your frontend needs proper CORS and frame policies:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// next.config.js (Next.js)
module.exports = {
  async headers() {
    return [
      {
        source: '/(.*)',
        headers: [
          {
            key: 'X-Frame-Options',
            value: 'ALLOW-FROM https://cms.example.com'
          },
          {
            key: 'Content-Security-Policy',
            value: 'frame-ancestors https://cms.example.com'
          }
        ]
      }
    ];
  }
};</code></pre>

<h3>Communication Protocol</h3>
<p>Visual Builder and your frontend communicate via <code>postMessage</code>:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Frontend: Listen for CMS messages
window.addEventListener('message', (event) => {
  // Verify origin
  if (event.origin !== process.env.CMS_URL) return;

  const { type, payload } = event.data;

  switch (type) {
    case 'content-updated':
      // Refetch content from Graph
      refreshContent(payload.contentId);
      break;
    case 'navigate':
      // Navigate to different content
      router.push(payload.path);
      break;
  }
});

// Frontend: Notify CMS of ready state
window.parent.postMessage({
  type: 'preview-ready',
  payload: { contentId: currentContent.id }
}, process.env.CMS_URL);</code></pre>

<h3>Live Preview Updates</h3>
<p>For real-time updates as editors type:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Poll Graph for changes (simple approach)
useEffect(() => {
  if (!previewMode) return;

  const interval = setInterval(async () => {
    const updatedContent = await fetchFromGraph(contentId);
    if (updatedContent.version !== content.version) {
      setContent(updatedContent);
    }
  }, 2000); // Poll every 2 seconds

  return () => clearInterval(interval);
}, [previewMode, contentId]);</code></pre>

<h3>Best Practices</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Practice</th>
            <th class=""px-4 py-2 text-left"">Recommendation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Authentication</td><td class=""px-4 py-2"">Always validate preview tokens server-side</td></tr>
        <tr><td class=""px-4 py-2"">Caching</td><td class=""px-4 py-2"">Disable caching in preview mode</td></tr>
        <tr><td class=""px-4 py-2"">Error handling</td><td class=""px-4 py-2"">Show friendly errors if content fails to load</td></tr>
        <tr><td class=""px-4 py-2"">Performance</td><td class=""px-4 py-2"">Optimize Graph queries for preview</td></tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Tip:</strong> Add a visual indicator in your frontend when in preview mode, so editors know they're viewing draft content.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 11: Smooth Rebuild

    private LearningModule BuildSmoothRebuildModule()
    {
        return new LearningModule
        {
            Id = "smooth-rebuild",
            Title = "Smooth Rebuild",
            Description = "Reset and rebuild Graph sources without downtime using deployment slots.",
            Icon = "arrow-path-rounded-square",
            Order = 11,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "graph-integration" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "sr-overview",
                    ModuleId = "smooth-rebuild",
                    Title = "Understanding Smooth Rebuild",
                    Summary = "Learn how Smooth Rebuild enables zero-downtime Graph reindexing.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of Smooth Rebuild",
                        "Learn how deployment slots work",
                        "Identify when to use Smooth Rebuild"
                    },
                    Content = @"
<h2>Understanding Smooth Rebuild</h2>
<p>Smooth Rebuild is a CMS 13 feature that allows you to reset your Optimizely Graph source and reindex all content <strong>without any downtime</strong> for your live site.</p>

<h3>Why Smooth Rebuild?</h3>
<p>There are scenarios where you need to completely rebuild your Graph index:</p>
<ul>
    <li><strong>Schema changes</strong> - Adding or modifying content types</li>
    <li><strong>Index corruption</strong> - Recovering from data inconsistencies</li>
    <li><strong>Configuration updates</strong> - Changes to indexing behavior</li>
    <li><strong>Major upgrades</strong> - After significant CMS updates</li>
</ul>

<h3>The Traditional Problem</h3>
<p>Without Smooth Rebuild, reindexing would cause:</p>
<ul>
    <li>Missing content in search results during rebuild</li>
    <li>Broken pages relying on Graph queries</li>
    <li>Need for maintenance windows</li>
    <li>Editor confusion about missing content</li>
</ul>

<h3>How Smooth Rebuild Works</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Step</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Live Traffic</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1. Create Slot</td><td class=""px-4 py-2"">New deployment slot created</td><td class=""px-4 py-2"">Served from original</td></tr>
        <tr><td class=""px-4 py-2"">2. Rebuild</td><td class=""px-4 py-2"">Content indexed to new slot</td><td class=""px-4 py-2"">Served from original</td></tr>
        <tr><td class=""px-4 py-2"">3. Verify</td><td class=""px-4 py-2"">Test new slot content</td><td class=""px-4 py-2"">Served from original</td></tr>
        <tr><td class=""px-4 py-2"">4. Commit</td><td class=""px-4 py-2"">Switch traffic to new slot</td><td class=""px-4 py-2"">Served from new slot</td></tr>
    </tbody>
</table>

<h3>Deployment Slots</h3>
<p>Deployment slots are isolated environments for your Graph source:</p>
<ul>
    <li><strong>Independent index</strong> - Complete copy of your content</li>
    <li><strong>Isolated changes</strong> - Modifications don't affect live traffic</li>
    <li><strong>Instant switching</strong> - Traffic switches atomically</li>
    <li><strong>Rollback capable</strong> - Can abandon slot without impact</li>
</ul>

<h3>When to Use Smooth Rebuild</h3>
<ul>
    <li>After content type schema changes</li>
    <li>When search results seem incomplete</li>
    <li>After major CMS or Graph updates</li>
    <li>When troubleshooting indexing issues</li>
    <li>As part of deployment pipelines</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Note:</strong> Smooth Rebuild allows resetting Graph sources without downtime. Ensure your Optimizely Graph instance is properly configured before using this feature.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "sr-slots",
                    ModuleId = "smooth-rebuild",
                    Title = "Creating and Managing Slots",
                    Summary = "Learn the workflow for creating, verifying, and committing deployment slots.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create a new deployment slot",
                        "Monitor rebuild progress",
                        "Commit or abandon slots"
                    },
                    Content = @"
<h2>Creating and Managing Slots</h2>
<p>Learn the complete workflow for managing deployment slots in Smooth Rebuild.</p>

<h3>Accessing Smooth Rebuild</h3>
<p>Navigate to the Smooth Rebuild interface:</p>
<ol>
    <li>Go to <strong>Admin</strong> in the CMS</li>
    <li>Select <strong>Config</strong> &gt; <strong>Optimizely Graph</strong></li>
    <li>Click <strong>Smooth Rebuild</strong></li>
</ol>

<h3>Creating a Deployment Slot</h3>
<p>To initiate a smooth rebuild:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Programmatic slot creation
public class SmoothRebuildService
{
    private readonly IGraphSyncService _graphSync;

    public async Task&lt;string&gt; CreateRebuildSlot()
    {
        var slot = await _graphSync.CreateDeploymentSlot();
        return slot.SlotId;
    }
}</code></pre>

<h3>Monitoring Progress</h3>
<p>Track the rebuild status:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>Creating</code></td><td class=""px-4 py-2"">Slot is being initialized</td></tr>
        <tr><td class=""px-4 py-2""><code>Indexing</code></td><td class=""px-4 py-2"">Content is being indexed to slot</td></tr>
        <tr><td class=""px-4 py-2""><code>Ready</code></td><td class=""px-4 py-2"">Rebuild complete, ready for verification</td></tr>
        <tr><td class=""px-4 py-2""><code>Committing</code></td><td class=""px-4 py-2"">Traffic switching in progress</td></tr>
        <tr><td class=""px-4 py-2""><code>Active</code></td><td class=""px-4 py-2"">Slot is now serving live traffic</td></tr>
    </tbody>
</table>

<h3>Progress Metrics</h3>
<p>The interface displays real-time metrics:</p>
<ul>
    <li>Total content items to index</li>
    <li>Items processed</li>
    <li>Percentage complete</li>
    <li>Estimated time remaining</li>
    <li>Error count</li>
</ul>

<h3>Verifying the Slot</h3>
<p>Before committing, verify the slot content:</p>
<ul>
    <li>Query the slot directly using the preview endpoint</li>
    <li>Check content counts match expected numbers</li>
    <li>Test critical search queries</li>
    <li>Verify all content types are indexed</li>
</ul>

<h3>Committing a Slot</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Commit the slot to go live
await _graphSync.CommitDeploymentSlot(slotId);

// Traffic now served from new slot</code></pre>

<h3>Abandoning a Slot</h3>
<p>If something is wrong, abandon without impact:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Abandon slot (no effect on live traffic)
await _graphSync.AbandonDeploymentSlot(slotId);

// Original index continues serving traffic</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Best Practice:</strong> Always verify content counts and test critical queries before committing a slot to production.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "sr-strategies",
                    ModuleId = "smooth-rebuild",
                    Title = "Rebuild Strategies",
                    Summary = "Learn best practices and strategies for effective smooth rebuilds.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Plan rebuild timing and resources",
                        "Handle large content repositories",
                        "Integrate with deployment pipelines"
                    },
                    Content = @"
<h2>Rebuild Strategies</h2>
<p>Effective smooth rebuild strategies help minimize risk and optimize performance.</p>

<h3>Planning Your Rebuild</h3>
<p>Consider these factors before starting:</p>

<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Factor</th>
            <th class=""px-4 py-2 text-left"">Consideration</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Content volume</td><td class=""px-4 py-2"">Larger sites take longer to rebuild</td></tr>
        <tr><td class=""px-4 py-2"">Content complexity</td><td class=""px-4 py-2"">Rich media and nested blocks add time</td></tr>
        <tr><td class=""px-4 py-2"">Traffic patterns</td><td class=""px-4 py-2"">Consider peak usage when committing</td></tr>
        <tr><td class=""px-4 py-2"">Editor activity</td><td class=""px-4 py-2"">New content during rebuild goes to both</td></tr>
    </tbody>
</table>

<h3>Large Repository Strategies</h3>
<p>For sites with tens of thousands of content items:</p>
<ul>
    <li><strong>Off-peak timing</strong> - Start rebuilds during low-traffic periods</li>
    <li><strong>Batch processing</strong> - System automatically batches for efficiency</li>
    <li><strong>Monitor resources</strong> - Watch CMS server performance during rebuild</li>
    <li><strong>Allow sufficient time</strong> - Don't rush to commit; verify thoroughly</li>
</ul>

<h3>Content Changes During Rebuild</h3>
<p>Content published during a rebuild:</p>
<ul>
    <li>Goes to the <strong>live index</strong> immediately (for current traffic)</li>
    <li>Goes to the <strong>rebuilding slot</strong> as well</li>
    <li>No content is lost or delayed</li>
</ul>

<h3>CI/CD Integration</h3>
<p>Automate smooth rebuilds in your deployment pipeline:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code># Azure DevOps pipeline example
stages:
  - stage: Deploy
    jobs:
      - job: DeployCMS
        steps:
          - script: dotnet publish
          - task: Deploy@1

      - job: RebuildGraph
        dependsOn: DeployCMS
        steps:
          - script: |
              # Create slot
              SLOT_ID=$(curl -X POST $CMS_URL/api/graph/slots)

              # Wait for completion
              while [ ""$(curl $CMS_URL/api/graph/slots/$SLOT_ID/status)"" != ""Ready"" ]; do
                sleep 30
              done

              # Verify content count
              COUNT=$(curl $CMS_URL/api/graph/slots/$SLOT_ID/count)
              if [ $COUNT -lt $MIN_EXPECTED ]; then
                curl -X DELETE $CMS_URL/api/graph/slots/$SLOT_ID
                exit 1
              fi

              # Commit
              curl -X POST $CMS_URL/api/graph/slots/$SLOT_ID/commit</code></pre>

<h3>Troubleshooting</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Resolution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Rebuild stuck</td><td class=""px-4 py-2"">Check server logs, may need to abandon and retry</td></tr>
        <tr><td class=""px-4 py-2"">Missing content</td><td class=""px-4 py-2"">Verify content type indexing configuration</td></tr>
        <tr><td class=""px-4 py-2"">High error count</td><td class=""px-4 py-2"">Review error details, fix content issues</td></tr>
        <tr><td class=""px-4 py-2"">Slow progress</td><td class=""px-4 py-2"">Expected for large sites; monitor resources</td></tr>
    </tbody>
</table>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Success Pattern:</strong> Schedule smooth rebuilds during off-peak hours, allow ample time for completion and verification, and always have a rollback plan (abandon) ready.</p>
</div>
"
                }
            }
        };
    }

    #endregion

    #region Module 12: CMS 12 to CMS 13 Migration

    private LearningModule BuildMigrationModule()
    {
        return new LearningModule
        {
            Id = "migration-12-to-13",
            Title = "CMS 12 to CMS 13 Migration",
            Description = "Comprehensive guide to upgrading from CMS 12 to CMS 13.",
            Icon = "arrow-up-circle",
            Order = 12,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "overview", "framework" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "mig-overview",
                    ModuleId = "migration-12-to-13",
                    Title = "Migration Overview",
                    Summary = "Understand the scope and planning required for CMS 12 to CMS 13 migration.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand breaking changes in CMS 13",
                        "Assess migration complexity for your project",
                        "Plan your migration approach"
                    },
                    Content = @"
<h2>Migration Overview</h2>
<p>Migrating from CMS 12 to CMS 13 requires careful planning due to significant architectural changes.</p>

<h3>Key Breaking Changes Summary</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Area</th>
            <th class=""px-4 py-2 text-left"">Change</th>
            <th class=""px-4 py-2 text-left"">Impact</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Runtime</td><td class=""px-4 py-2"">.NET 10 required</td><td class=""px-4 py-2"">High</td></tr>
        <tr><td class=""px-4 py-2"">Graph</td><td class=""px-4 py-2"">Mandatory integration</td><td class=""px-4 py-2"">High</td></tr>
        <tr><td class=""px-4 py-2"">Sites</td><td class=""px-4 py-2"">SiteDefinition → Application</td><td class=""px-4 py-2"">Medium</td></tr>
        <tr><td class=""px-4 py-2"">References</td><td class=""px-4 py-2"">PageReference deprecated</td><td class=""px-4 py-2"">Medium</td></tr>
        <tr><td class=""px-4 py-2"">DI</td><td class=""px-4 py-2"">ServiceLocator deprecated</td><td class=""px-4 py-2"">Medium</td></tr>
        <tr><td class=""px-4 py-2"">Projects</td><td class=""px-4 py-2"">Must be disabled</td><td class=""px-4 py-2"">High (if used)</td></tr>
        <tr><td class=""px-4 py-2"">On-Page Edit</td><td class=""px-4 py-2"">Disabled, Visual Builder only</td><td class=""px-4 py-2"">Medium</td></tr>
    </tbody>
</table>

<h3>Pre-Migration Assessment</h3>
<p>Before starting, assess your project:</p>
<ol>
    <li><strong>Third-party packages</strong> - Check compatibility with .NET 10 and CMS 13</li>
    <li><strong>Custom code</strong> - Identify ServiceLocator and PageReference usage</li>
    <li><strong>Projects feature</strong> - Determine if you rely on Projects for workflows</li>
    <li><strong>Search implementation</strong> - Plan for mandatory Graph integration</li>
    <li><strong>Site definitions</strong> - Document current SiteDefinition configurations</li>
</ol>

<h3>Migration Phases</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Phase</th>
            <th class=""px-4 py-2 text-left"">Activities</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1. Preparation</td><td class=""px-4 py-2"">Backup, assessment, dependency audit</td></tr>
        <tr><td class=""px-4 py-2"">2. Framework Update</td><td class=""px-4 py-2"">.NET 10, NuGet packages</td></tr>
        <tr><td class=""px-4 py-2"">3. Code Migration</td><td class=""px-4 py-2"">API updates, deprecated code fixes</td></tr>
        <tr><td class=""px-4 py-2"">4. Graph Setup</td><td class=""px-4 py-2"">Configure and test Graph integration</td></tr>
        <tr><td class=""px-4 py-2"">5. Feature Updates</td><td class=""px-4 py-2"">Remove Projects, update editors</td></tr>
        <tr><td class=""px-4 py-2"">6. Testing</td><td class=""px-4 py-2"">Comprehensive validation</td></tr>
    </tbody>
</table>

<h3>Risk Mitigation</h3>
<ul>
    <li><strong>Environment isolation</strong> - Migrate in a separate environment first</li>
    <li><strong>Database backup</strong> - Full backup before any migration steps</li>
    <li><strong>Rollback plan</strong> - Document how to revert if needed</li>
    <li><strong>Staged rollout</strong> - Consider migrating dev → staging → production</li>
</ul>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Note:</strong> CMS 13 is now generally available. Review the <a href=""https://docs.developers.optimizely.com/content-management-system/v13.0.0-CMS/docs/cms-13-overview"" target=""_blank"" class=""text-blue-600 hover:underline"">official upgrade documentation</a> for the latest migration guidance.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "mig-dotnet",
                    ModuleId = "migration-12-to-13",
                    Title = ".NET 10 and Package Updates",
                    Summary = "Upgrade your project to .NET 10 and update NuGet packages.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Update project files to .NET 10",
                        "Upgrade Optimizely NuGet packages",
                        "Resolve dependency conflicts"
                    },
                    Content = @"
<h2>.NET 10 and Package Updates</h2>
<p>The first technical step in migration is upgrading to .NET 10 and updating all packages.</p>

<h3>Update Project File</h3>
<p>Modify your <code>.csproj</code> file:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>&lt;Project Sdk=""Microsoft.NET.Sdk.Web""&gt;
  &lt;PropertyGroup&gt;
    &lt;!-- CMS 12 --&gt;
    &lt;!-- &lt;TargetFramework&gt;net6.0&lt;/TargetFramework&gt; --&gt;
    &lt;!-- &lt;TargetFramework&gt;net8.0&lt;/TargetFramework&gt; --&gt;

    &lt;!-- CMS 13 --&gt;
    &lt;TargetFramework&gt;net10.0&lt;/TargetFramework&gt;
    &lt;Nullable&gt;enable&lt;/Nullable&gt;
    &lt;ImplicitUsings&gt;enable&lt;/ImplicitUsings&gt;
  &lt;/PropertyGroup&gt;
&lt;/Project&gt;</code></pre>

<h3>Install .NET 10 SDK</h3>
<p>Ensure your development environment has .NET 10:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code># Check current SDK version
dotnet --version

# Download .NET 10 SDK from Microsoft
# https://dotnet.microsoft.com/download/dotnet/10.0</code></pre>

<h3>Update NuGet Packages</h3>
<p>Update Optimizely packages to CMS 13 versions:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>&lt;ItemGroup&gt;
  &lt;!-- Core CMS packages --&gt;
  &lt;PackageReference Include=""EPiServer.CMS"" Version=""13.0.0"" /&gt;
  &lt;PackageReference Include=""EPiServer.CMS.AspNetCore"" Version=""13.0.0"" /&gt;
  &lt;PackageReference Include=""EPiServer.CMS.UI"" Version=""13.0.0"" /&gt;

  &lt;!-- Required Graph packages --&gt;
  &lt;PackageReference Include=""EPiServer.ContentGraph"" Version=""13.0.0"" /&gt;
  &lt;PackageReference Include=""EPiServer.ContentGraph.CMS"" Version=""13.0.0"" /&gt;
&lt;/ItemGroup&gt;</code></pre>

<h3>Package Update Commands</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code># Update all Optimizely packages
dotnet add package EPiServer.CMS --version 13.0.0
dotnet add package EPiServer.CMS.AspNetCore --version 13.0.0
dotnet add package EPiServer.CMS.UI --version 13.0.0
dotnet add package EPiServer.ContentGraph --version 13.0.0
dotnet add package EPiServer.ContentGraph.CMS --version 13.0.0

# Or update all packages at once
dotnet restore</code></pre>

<h3>Common Dependency Issues</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Resolution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Version conflicts</td><td class=""px-4 py-2"">Use consistent package versions across all EPiServer packages</td></tr>
        <tr><td class=""px-4 py-2"">Third-party incompatibility</td><td class=""px-4 py-2"">Check for .NET 10 compatible versions or alternatives</td></tr>
        <tr><td class=""px-4 py-2"">Missing dependencies</td><td class=""px-4 py-2"">Run <code>dotnet restore</code> to resolve transitive dependencies</td></tr>
    </tbody>
</table>

<h3>global.json Configuration</h3>
<p>Pin your SDK version for consistent builds:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>{
  ""sdk"": {
    ""version"": ""10.0.100"",
    ""rollForward"": ""latestMinor""
  }
}</code></pre>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Tip:</strong> After updating packages, build the project to identify any compilation errors before proceeding to code migration.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "mig-applications",
                    ModuleId = "migration-12-to-13",
                    Title = "SiteDefinition to Application Migration",
                    Summary = "Update your code from SiteDefinition APIs to the new Application model.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Replace SiteDefinition with Application APIs",
                        "Update ISiteDefinitionRepository usage",
                        "Migrate SiteDefinition.Current patterns"
                    },
                    Content = @"
<h2>SiteDefinition to Application Migration</h2>
<p>The <code>SiteDefinition</code> concept has been replaced by <code>Application</code> in CMS 13. This requires updating your code.</p>

<h3>API Mapping</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">CMS 12</th>
            <th class=""px-4 py-2 text-left"">CMS 13</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>SiteDefinition</code></td><td class=""px-4 py-2""><code>Application</code></td></tr>
        <tr><td class=""px-4 py-2""><code>ISiteDefinitionRepository</code></td><td class=""px-4 py-2""><code>IApplicationRepository</code></td></tr>
        <tr><td class=""px-4 py-2""><code>SiteDefinition.Current</code></td><td class=""px-4 py-2""><code>IApplicationResolver.GetCurrentApplication()</code></td></tr>
        <tr><td class=""px-4 py-2""><code>SiteDefinitionResolver</code></td><td class=""px-4 py-2""><code>IApplicationResolver</code></td></tr>
    </tbody>
</table>

<h3>Migration Example: Static Access</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12 - Static access (deprecated)
public class NavigationHelper
{
    public ContentReference GetStartPage()
    {
        return SiteDefinition.Current.StartPage;
    }
}

// CMS 13 - Dependency injection
public class NavigationHelper
{
    private readonly IApplicationResolver _resolver;

    public NavigationHelper(IApplicationResolver resolver)
    {
        _resolver = resolver;
    }

    public ContentReference GetStartPage()
    {
        return _resolver.GetCurrentApplication().StartPage;
    }
}</code></pre>

<h3>Migration Example: Repository</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12
public class SiteService
{
    private readonly ISiteDefinitionRepository _siteRepo;

    public IEnumerable&lt;SiteDefinition&gt; GetAllSites()
    {
        return _siteRepo.List();
    }
}

// CMS 13
public class SiteService
{
    private readonly IApplicationRepository _appRepo;

    public IEnumerable&lt;Application&gt; GetAllSites()
    {
        return _appRepo.List();
    }
}</code></pre>

<h3>Property Mapping</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">SiteDefinition Property</th>
            <th class=""px-4 py-2 text-left"">Application Property</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>StartPage</code></td><td class=""px-4 py-2""><code>StartPage</code></td></tr>
        <tr><td class=""px-4 py-2""><code>Name</code></td><td class=""px-4 py-2""><code>Name</code></td></tr>
        <tr><td class=""px-4 py-2""><code>Id</code></td><td class=""px-4 py-2""><code>Id</code></td></tr>
        <tr><td class=""px-4 py-2""><code>Hosts</code></td><td class=""px-4 py-2""><code>Hosts</code></td></tr>
        <tr><td class=""px-4 py-2""><code>SiteAssetsRoot</code></td><td class=""px-4 py-2""><code>AssetsRootId</code></td></tr>
        <tr><td class=""px-4 py-2""><code>ContentAssetsRoot</code></td><td class=""px-4 py-2""><code>ContentRootId</code></td></tr>
    </tbody>
</table>

<h3>Search and Replace Patterns</h3>
<p>Use your IDE's search and replace:</p>
<ul>
    <li><code>SiteDefinition.Current</code> → Requires refactoring to inject <code>IApplicationResolver</code></li>
    <li><code>ISiteDefinitionRepository</code> → <code>IApplicationRepository</code></li>
    <li><code>SiteDefinition</code> → <code>Application</code> (in type declarations)</li>
</ul>

<h3>Multi-Site Considerations</h3>
<p>If you have multiple sites:</p>
<ul>
    <li>Data is migrated automatically during upgrade</li>
    <li>Site-to-Application mapping is 1:1</li>
    <li>Host bindings are preserved</li>
    <li>Start pages and asset roots remain unchanged</li>
</ul>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Note:</strong> The static <code>SiteDefinition.Current</code> pattern cannot be directly replaced. You must refactor to use dependency injection with <code>IApplicationResolver</code>.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "mig-graph",
                    ModuleId = "migration-12-to-13",
                    Title = "Graph Integration Requirements",
                    Summary = "Set up mandatory Optimizely Graph integration for CMS 13.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure Graph integration from scratch",
                        "Obtain Graph credentials from DXP Portal",
                        "Verify Graph connectivity"
                    },
                    Content = @"
<h2>Graph Integration Requirements</h2>
<p>Optimizely Graph is <strong>mandatory</strong> in CMS 13. You must configure it for the CMS to function.</p>

<h3>Why Graph is Required</h3>
<ul>
    <li><strong>Content Manager</strong> - Uses Graph for its search-first interface</li>
    <li><strong>Internal retrieval</strong> - CMS uses Graph internally</li>
    <li><strong>Visual Builder</strong> - Relies on Graph for content delivery</li>
</ul>

<h3>Step 1: Enable Graph in DXP Portal</h3>
<ol>
    <li>Log in to the DXP Portal</li>
    <li>Navigate to your environment</li>
    <li>Go to the <strong>API</strong> tab</li>
    <li>Enable the <strong>Graph Service</strong></li>
    <li>Copy your authentication keys</li>
</ol>

<h3>Step 2: Configure appsettings.json</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>{
  ""Optimizely"": {
    ""Graph"": {
      ""GatewayAddress"": ""https://graph.optimizely.com"",
      ""SingleKey"": ""your-single-key-here"",
      ""AppKey"": ""your-app-key-here"",
      ""Secret"": ""your-secret-here""
    }
  }
}</code></pre>

<h3>Step 3: Register Services</h3>
<p>Update your <code>Startup.cs</code> or <code>Program.cs</code>:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>public void ConfigureServices(IServiceCollection services)
{
    services.AddCms()
            .AddContentGraph()    // Required for CMS 13
            .AddContentManager(); // For the new Content Manager
}</code></pre>

<h3>Step 4: Initial Index</h3>
<p>After configuration, trigger initial indexing:</p>
<ol>
    <li>Navigate to <strong>Admin &gt; Config &gt; Optimizely Graph</strong></li>
    <li>Click <strong>Rebuild Index</strong></li>
    <li>Wait for indexing to complete</li>
    <li>Verify content appears in queries</li>
</ol>

<h3>Verification Steps</h3>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Test Graph connectivity
public class GraphHealthCheck
{
    private readonly IGraphClient _graphClient;

    public async Task&lt;bool&gt; CheckConnectivity()
    {
        try
        {
            var result = await _graphClient.Query&lt;object&gt;(@""
                query { __typename }
            "");
            return result != null;
        }
        catch
        {
            return false;
        }
    }
}</code></pre>

<h3>Troubleshooting</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Issue</th>
            <th class=""px-4 py-2 text-left"">Resolution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">401 Unauthorized</td><td class=""px-4 py-2"">Verify API keys are correct</td></tr>
        <tr><td class=""px-4 py-2"">No content indexed</td><td class=""px-4 py-2"">Trigger manual rebuild from admin</td></tr>
        <tr><td class=""px-4 py-2"">Connection timeout</td><td class=""px-4 py-2"">Check network/firewall settings</td></tr>
        <tr><td class=""px-4 py-2"">Content Manager blank</td><td class=""px-4 py-2"">Ensure AddContentManager() is called</td></tr>
    </tbody>
</table>

<div class=""bg-red-50 dark:bg-red-900 border-l-4 border-red-400 p-4 my-4"">
    <p class=""text-red-800 dark:text-red-100""><strong>Critical:</strong> CMS 13 will not function correctly without Graph integration. This is not optional.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "mig-deprecated",
                    ModuleId = "migration-12-to-13",
                    Title = "Removing Deprecated Features",
                    Summary = "Identify and remove deprecated features that are incompatible with CMS 13.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Disable Projects feature",
                        "Remove Plugin Manager dependencies",
                        "Replace On-Page Edit with Visual Builder"
                    },
                    Content = @"
<h2>Removing Deprecated Features</h2>
<p>Several CMS 12 features must be disabled or removed for CMS 13 to function.</p>

<h3>1. Projects Feature</h3>
<p>The Projects feature is <strong>not supported</strong> in CMS 13 and must be disabled:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Disable Projects in configuration
services.Configure&lt;ProjectOptions&gt;(options =>
{
    options.Enabled = false;
});</code></pre>

<h4>Alternative Workflow Options</h4>
<ul>
    <li>Use scheduled publishing for coordinated releases</li>
    <li>Implement custom approval workflows</li>
    <li>Use content variations for staged content</li>
    <li>Consider third-party workflow solutions</li>
</ul>

<h3>2. Plugin Manager</h3>
<p>The Plugin Manager UI and backend have been removed:</p>
<ul>
    <li>Remove any <code>PlugInAttribute</code> decorations</li>
    <li>Update scheduled jobs to modern patterns</li>
    <li>Remove custom property type plugin registrations</li>
</ul>

<h4>Scheduled Job Migration</h4>
<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// CMS 12 (deprecated)
[ScheduledPlugIn(DisplayName = ""My Job"")]
public class MyJob : ScheduledJobBase
{
    public override string Execute() { ... }
}

// CMS 13
[ScheduledJob(
    Guid = ""generate-unique-guid"",
    Name = ""My Job"")]
public class MyJob : ScheduledJob
{
    public override DataStatus Execute(CancellationToken ct)
    {
        // Implementation
        return DataStatus.Succeeded;
    }
}</code></pre>

<h3>3. On-Page Edit (OPE)</h3>
<p>On-Page Edit is disabled in CMS 13. Visual Builder is the primary editing interface:</p>

<ul>
    <li><strong>Remove OPE-specific code</strong> - Any custom OPE integrations</li>
    <li><strong>Update editor training</strong> - Familiarize editors with Visual Builder</li>
    <li><strong>Configure content types</strong> - Ensure types work with Visual Builder</li>
</ul>

<h3>4. ServiceLocator Pattern</h3>
<p>While not completely removed, ServiceLocator is deprecated:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Find all usages
// Search for: ServiceLocator.Current

// Replace with constructor injection or IServiceProvider</code></pre>

<h3>5. PageReference</h3>
<p>Replace all <code>PageReference</code> with <code>ContentReference</code>:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Search and replace:
// PageReference → ContentReference
// PageReference.EmptyReference → ContentReference.EmptyReference
// PageReference.StartPage → ContentReference.StartPage</code></pre>

<h3>Code Audit Checklist</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Search For</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2""><code>ScheduledPlugIn</code></td><td class=""px-4 py-2"">Replace with <code>ScheduledJob</code></td></tr>
        <tr><td class=""px-4 py-2""><code>ServiceLocator.Current</code></td><td class=""px-4 py-2"">Refactor to DI</td></tr>
        <tr><td class=""px-4 py-2""><code>PageReference</code></td><td class=""px-4 py-2"">Replace with <code>ContentReference</code></td></tr>
        <tr><td class=""px-4 py-2""><code>SiteDefinition</code></td><td class=""px-4 py-2"">Replace with <code>Application</code></td></tr>
        <tr><td class=""px-4 py-2""><code>Projects</code> references</td><td class=""px-4 py-2"">Remove or replace with alternatives</td></tr>
    </tbody>
</table>

<div class=""bg-yellow-50 dark:bg-yellow-900 border-l-4 border-yellow-400 p-4 my-4"">
    <p class=""text-yellow-800 dark:text-yellow-100""><strong>Warning:</strong> CMS 13 will fail to start if Projects is enabled. Ensure it is disabled before deployment.</p>
</div>
"
                },
                new Lesson
                {
                    Id = "mig-testing",
                    ModuleId = "migration-12-to-13",
                    Title = "Testing and Validation",
                    Summary = "Comprehensive testing strategies for your migrated CMS 13 application.",
                    Order = 6,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create a migration testing plan",
                        "Validate functionality after migration",
                        "Troubleshoot common post-migration issues"
                    },
                    Content = @"
<h2>Testing and Validation</h2>
<p>Thorough testing is essential after migrating to CMS 13 to ensure everything works correctly.</p>

<h3>Testing Phases</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Phase</th>
            <th class=""px-4 py-2 text-left"">Focus Area</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1. Build Verification</td><td class=""px-4 py-2"">Compilation, dependency resolution</td></tr>
        <tr><td class=""px-4 py-2"">2. Startup Testing</td><td class=""px-4 py-2"">Application launches without errors</td></tr>
        <tr><td class=""px-4 py-2"">3. Functional Testing</td><td class=""px-4 py-2"">Core features work correctly</td></tr>
        <tr><td class=""px-4 py-2"">4. Integration Testing</td><td class=""px-4 py-2"">Third-party integrations function</td></tr>
        <tr><td class=""px-4 py-2"">5. Performance Testing</td><td class=""px-4 py-2"">Response times acceptable</td></tr>
        <tr><td class=""px-4 py-2"">6. User Acceptance</td><td class=""px-4 py-2"">Editors can perform their tasks</td></tr>
    </tbody>
</table>

<h3>Critical Functionality Checklist</h3>

<h4>Content Management</h4>
<ul>
    <li>☐ Content Manager loads and displays content</li>
    <li>☐ Search returns expected results</li>
    <li>☐ Content creation works for all types</li>
    <li>☐ Publishing workflow functions correctly</li>
    <li>☐ Media management works</li>
</ul>

<h4>Visual Builder</h4>
<ul>
    <li>☐ Experiences render correctly</li>
    <li>☐ Section editing works</li>
    <li>☐ Preview displays content</li>
    <li>☐ Templates and blueprints function</li>
</ul>

<h4>Graph Integration</h4>
<ul>
    <li>☐ Content indexes to Graph</li>
    <li>☐ Search queries return results</li>
    <li>☐ Real-time updates sync</li>
    <li>☐ Authentication works</li>
</ul>

<h4>Multi-Site (if applicable)</h4>
<ul>
    <li>☐ All sites resolve correctly</li>
    <li>☐ Host bindings work</li>
    <li>☐ Site-specific content is isolated</li>
</ul>

<h3>Common Post-Migration Issues</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Symptom</th>
            <th class=""px-4 py-2 text-left"">Likely Cause</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">CMS won't start</td><td class=""px-4 py-2"">Projects enabled</td><td class=""px-4 py-2"">Disable Projects feature</td></tr>
        <tr><td class=""px-4 py-2"">Content Manager empty</td><td class=""px-4 py-2"">Graph not configured</td><td class=""px-4 py-2"">Configure Graph, rebuild index</td></tr>
        <tr><td class=""px-4 py-2"">Compilation errors</td><td class=""px-4 py-2"">Deprecated API usage</td><td class=""px-4 py-2"">Update to new APIs</td></tr>
        <tr><td class=""px-4 py-2"">Runtime exceptions</td><td class=""px-4 py-2"">ServiceLocator usage</td><td class=""px-4 py-2"">Refactor to DI</td></tr>
        <tr><td class=""px-4 py-2"">Missing content</td><td class=""px-4 py-2"">Indexing incomplete</td><td class=""px-4 py-2"">Trigger full reindex</td></tr>
    </tbody>
</table>

<h3>Performance Baseline</h3>
<p>Establish performance baselines for comparison:</p>

<pre class=""bg-gray-900 text-gray-100 dark:bg-gray-950 p-4 rounded-lg overflow-x-auto""><code>// Key metrics to measure
- Page load time (frontend)
- API response times
- Graph query latency
- CMS admin interface responsiveness
- Content publishing time</code></pre>

<h3>Go-Live Checklist</h3>
<ol>
    <li>☐ All tests passing</li>
    <li>☐ Editor training completed</li>
    <li>☐ Backup verified and restorable</li>
    <li>☐ Rollback procedure documented</li>
    <li>☐ Monitoring configured</li>
    <li>☐ Support team briefed</li>
</ol>

<div class=""bg-blue-50 dark:bg-blue-900 border-l-4 border-blue-400 p-4 my-4"">
    <p class=""text-blue-800 dark:text-blue-100""><strong>Success Tip:</strong> Plan for a ""hypercare"" period immediately after go-live with increased support availability to quickly address any issues that arise.</p>
</div>
"
                }
            }
        };
    }

    #endregion
}
