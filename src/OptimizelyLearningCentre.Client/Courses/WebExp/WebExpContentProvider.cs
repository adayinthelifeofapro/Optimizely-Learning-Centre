using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.WebExp;

/// <summary>
/// Content provider for the Optimizely Web Experimentation course
/// </summary>
public class WebExpContentProvider : ILearningContentProvider
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
            BuildSnippetImplementationModule(),
            BuildVisualEditorModule(),
            BuildAudienceTargetingModule(),
            BuildEventsTrackingModule(),
            BuildJavaScriptAPIModule(),
            BuildStatsEngineModule(),
            BuildAdvancedExperimentsModule(),
            BuildIntegrationsModule(),
            BuildRestAPIModule(),
            BuildTroubleshootingModule(),
            BuildTestingMethodologyModule(),
            BuildQAPreviewModule(),
            BuildProjectJavaScriptModule()
        };
    }

    #region Module 1: Getting Started

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started with Web Experimentation",
            Description = "Learn the fundamentals of Optimizely Web Experimentation and A/B testing.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-webexp",
                    ModuleId = "getting-started",
                    Title = "What is Web Experimentation?",
                    Summary = "Discover Optimizely Web Experimentation and how it enables data-driven optimization.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Web Experimentation is",
                        "Learn the benefits of A/B testing",
                        "Discover how experimentation drives business results"
                    },
                    Content = @"
<h2>Introduction to Optimizely Web Experimentation</h2>
<p>Optimizely Web Experimentation is a <strong>powerful A/B testing and experimentation platform</strong> that enables you to test changes on your website and measure their impact on visitor behavior. By running controlled experiments, you can make data-driven decisions that improve conversion rates, user engagement, and overall website performance.</p>

<h3>What is A/B Testing?</h3>
<p>A/B testing (also known as split testing) is a method of comparing two or more versions of a webpage to determine which one performs better. Visitors are randomly assigned to different variations, and their behavior is tracked to measure which version achieves your goals most effectively.</p>

<h3>Key Capabilities</h3>
<ul>
    <li><strong>Visual Editor</strong> - Make changes to your site without writing code</li>
    <li><strong>JavaScript API</strong> - Programmatic control for advanced testing scenarios</li>
    <li><strong>Audience Targeting</strong> - Show experiments to specific visitor segments</li>
    <li><strong>Stats Engine</strong> - Statistically rigorous analysis of experiment results</li>
    <li><strong>Multi-page Experiments</strong> - Test changes across entire user journeys</li>
</ul>

<h3>Benefits of Experimentation</h3>
<ul>
    <li><strong>Data-driven decisions</strong> - Replace guesswork with statistical evidence</li>
    <li><strong>Reduced risk</strong> - Test changes before rolling out to all users</li>
    <li><strong>Continuous improvement</strong> - Iteratively optimize your digital experience</li>
    <li><strong>Customer insights</strong> - Learn what resonates with your audience</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "gs-experiment-types",
                    ModuleId = "getting-started",
                    Title = "Types of Experiments",
                    Summary = "Learn about the different experiment types available in Web Experimentation.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand A/B tests vs multivariate tests",
                        "Learn when to use redirect experiments",
                        "Discover multi-page funnel tests"
                    },
                    Content = @"
<h2>Experiment Types</h2>
<p>Optimizely Web Experimentation supports several types of experiments, each suited for different testing scenarios.</p>

<h3>A/B Tests</h3>
<p>The most common experiment type. Compare two or more variations of a page element or layout against each other.</p>
<ul>
    <li>Test different headlines, images, or button text</li>
    <li>Compare different page layouts</li>
    <li>Visitors are randomly assigned to one variation</li>
</ul>

<h3>Multivariate Tests (MVT)</h3>
<p>Test multiple variables simultaneously to understand how they interact.</p>
<ul>
    <li>Test combinations of headlines AND images together</li>
    <li>Identify the best-performing combination</li>
    <li>Requires more traffic than simple A/B tests</li>
</ul>

<h3>Redirect Experiments</h3>
<p>Direct visitors to completely different URLs to test entirely new page designs.</p>
<ul>
    <li>Test a complete page redesign</li>
    <li>Compare different landing page approaches</li>
    <li>Useful when changes are too complex for the Visual Editor</li>
</ul>

<h3>Multi-page (Funnel) Tests</h3>
<p>Test changes across multiple pages to optimize entire user journeys.</p>
<ul>
    <li>Test consistent experiences across a checkout flow</li>
    <li>Visitors see the same variation on all pages</li>
    <li>Measure impact on complete conversion funnels</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "gs-exp-types-example",
                            Title = "Choosing the Right Experiment Type",
                            Description = "Guidelines for selecting the appropriate experiment type.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Experiment Type Selection Guide:

A/B Test - Use when:
- Testing a single change (headline, button, image)
- Comparing 2-4 variations
- You have moderate traffic levels

Multivariate Test - Use when:
- Testing multiple elements together
- You want to find optimal combinations
- You have HIGH traffic volumes

Redirect Experiment - Use when:
- Testing a complete page redesign
- Changes cannot be made via Visual Editor
- Comparing entirely different approaches

Multi-page Test - Use when:
- Testing a user journey/funnel
- Consistency matters across pages
- Measuring end-to-end conversion",
                            SampleResponse = @"Example scenarios:

1. Testing a new CTA button color = A/B Test
2. Testing headline + image + CTA combinations = Multivariate
3. Testing a new checkout page design = Redirect
4. Testing a 3-step signup flow = Multi-page",
                            Hints = new List<string>
                            {
                                "Start with simple A/B tests before moving to multivariate",
                                "Multi-page tests require the same number of variations on each page"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-six-steps",
                    ModuleId = "getting-started",
                    Title = "Six Steps to Create an Experiment",
                    Summary = "Learn the workflow for creating and launching experiments.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the experiment creation workflow",
                        "Learn what each step involves",
                        "Know how to properly launch an experiment"
                    },
                    Content = @"
<h2>The Experiment Workflow</h2>
<p>Creating an experiment in Optimizely Web Experimentation follows six key steps:</p>

<h3>Step 1: Configure URL Targeting</h3>
<p>Define which pages your experiment will run on. You can target:</p>
<ul>
    <li>Specific URLs</li>
    <li>URL patterns using wildcards</li>
    <li>Regular expressions for complex matching</li>
</ul>

<h3>Step 2: Create Variations</h3>
<p>Build the different versions you want to test:</p>
<ul>
    <li>Use the Visual Editor for no-code changes</li>
    <li>Add custom JavaScript or CSS</li>
    <li>Configure redirect URLs if needed</li>
</ul>

<h3>Step 3: Add Audiences</h3>
<p>Define which visitors should see the experiment:</p>
<ul>
    <li>Target by device, browser, or location</li>
    <li>Use cookies or custom attributes</li>
    <li>Create custom JavaScript conditions</li>
</ul>

<h3>Step 4: Add Metrics</h3>
<p>Define how you'll measure success:</p>
<ul>
    <li>Click events on specific elements</li>
    <li>Page views of confirmation pages</li>
    <li>Custom events for complex tracking</li>
</ul>

<h3>Step 5: Set Traffic Distribution</h3>
<p>Configure how visitors are split between variations:</p>
<ul>
    <li>Equal distribution is common (50/50 for two variations)</li>
    <li>Reduce traffic to new variations for safety</li>
    <li>Hold out a percentage as a control group</li>
</ul>

<h3>Step 6: Test and Publish</h3>
<p>QA your experiment before going live:</p>
<ul>
    <li>Use Preview mode to test each variation</li>
    <li>Verify tracking is working correctly</li>
    <li>Publish when ready to start collecting data</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 2: Snippet Implementation

    private LearningModule BuildSnippetImplementationModule()
    {
        return new LearningModule
        {
            Id = "snippet-implementation",
            Title = "Implementing the Snippet",
            Description = "Learn how to properly implement the Optimizely JavaScript snippet on your website.",
            Icon = "code-bracket",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "si-snippet-basics",
                    ModuleId = "snippet-implementation",
                    Title = "The Optimizely Snippet",
                    Summary = "Understand what the snippet is and how it works.",
                    Order = 1,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Understand what the Optimizely snippet does",
                        "Know where to find your snippet",
                        "Learn the importance of proper placement"
                    },
                    Content = @"
<h2>The Optimizely JavaScript Snippet</h2>
<p>The Optimizely snippet is a <strong>one-line JavaScript include</strong> that contains all the logic needed to run experiments on your website. It's unique to your project and includes your project ID.</p>

<h3>What the Snippet Does</h3>
<ul>
    <li>Loads your experiment configuration</li>
    <li>Evaluates visitor targeting conditions</li>
    <li>Applies variation changes to the page</li>
    <li>Tracks visitor behavior and conversions</li>
</ul>

<h3>Finding Your Snippet</h3>
<p>To find your snippet in the Optimizely interface:</p>
<ol>
    <li>Go to <strong>Settings</strong> in your project</li>
    <li>Click <strong>Implementation</strong></li>
    <li>Copy the snippet code</li>
</ol>

<h3>Snippet Format</h3>
<p>Your snippet looks like this:</p>
<pre><code>&lt;script src=""https://cdn.optimizely.com/js/PROJECT_ID.js""&gt;&lt;/script&gt;</code></pre>
<p>Where PROJECT_ID is your unique project identifier.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "si-snippet-example",
                            Title = "Example Snippet",
                            Description = "A sample Optimizely snippet implementation.",
                            Type = ExampleType.Code,
                            ExampleContent = @"<!-- Optimizely Web Experimentation Snippet -->
<script src=""https://cdn.optimizely.com/js/123456789.js""></script>",
                            SampleResponse = @"The snippet loads asynchronously from Optimizely's CDN.
It contains:
- Your project configuration
- All active experiments
- Targeting rules and conditions
- Variation code and changes",
                            Hints = new List<string>
                            {
                                "Never modify the snippet - use it exactly as provided",
                                "Each project has a unique snippet with its own ID"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "si-placement",
                    ModuleId = "snippet-implementation",
                    Title = "Snippet Placement Best Practices",
                    Summary = "Learn where to place the snippet for optimal performance.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Know the correct placement in the HTML",
                        "Understand why placement matters",
                        "Avoid common implementation mistakes"
                    },
                    Content = @"
<h2>Where to Place the Snippet</h2>
<p>Proper snippet placement is <strong>critical for experiment performance</strong>. Incorrect placement can cause page flashing and poor user experience.</p>

<h3>Recommended Placement</h3>
<p>Add the snippet as <strong>high as possible in the &lt;head&gt; tag</strong>:</p>
<ul>
    <li>After the opening &lt;html&gt; tag</li>
    <li>After charset declarations</li>
    <li>Before other scripts and stylesheets</li>
</ul>

<h3>Why Placement Matters</h3>
<p>The snippet must execute before the page renders to prevent <strong>page flashing</strong> - when visitors briefly see the original content before the variation is applied.</p>

<h3>What to Avoid</h3>
<ul>
    <li><strong>Don't place in the body</strong> - Page will flash</li>
    <li><strong>Don't use tag managers</strong> - Most don't support synchronous loading</li>
    <li><strong>Don't modify the snippet</strong> - Can break functionality</li>
    <li><strong>Don't use multiple snippets</strong> - Only one per page</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "si-placement-example",
                            Title = "Correct Snippet Placement",
                            Description = "Example of proper snippet placement in HTML.",
                            Type = ExampleType.Code,
                            ExampleContent = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <!-- Optimizely snippet - place as high as possible -->
    <script src=""https://cdn.optimizely.com/js/123456789.js""></script>

    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>My Website</title>
    <link rel=""stylesheet"" href=""styles.css"">
</head>
<body>
    <!-- Page content -->
</body>
</html>",
                            SampleResponse = @"Key points:
1. Snippet is in the <head> section
2. Placed immediately after charset declaration
3. Before other CSS and JS files
4. No tag manager used

This ensures experiments apply before
the page becomes visible to visitors.",
                            Hints = new List<string>
                            {
                                "The snippet loads synchronously by default for immediate execution",
                                "Google Tag Manager does not support synchronous loading"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "si-verify",
                    ModuleId = "snippet-implementation",
                    Title = "Verifying Your Implementation",
                    Summary = "Learn how to verify the snippet is working correctly.",
                    Order = 3,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Check if the snippet is implemented",
                        "Use browser developer tools",
                        "Troubleshoot common issues"
                    },
                    Content = @"
<h2>Verifying Snippet Implementation</h2>
<p>After adding the snippet, you should verify it's working correctly before launching experiments.</p>

<h3>Using the Browser Console</h3>
<p>Open your browser's developer console and type:</p>
<pre><code>window.optimizely</code></pre>
<p>If the snippet is implemented, you'll see the Optimizely object with available methods.</p>

<h3>Check the Network Tab</h3>
<p>In the Network tab of developer tools:</p>
<ol>
    <li>Filter by ""optimizely""</li>
    <li>Look for your snippet file (e.g., 123456789.js)</li>
    <li>Verify it loads with status 200</li>
</ol>

<h3>Using Preview Mode</h3>
<p>Optimizely's Preview mode lets you test experiments before they go live:</p>
<ul>
    <li>Force yourself into specific variations</li>
    <li>Verify changes are applying correctly</li>
    <li>Check that events are tracking</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "si-verify-example",
                            Title = "Console Verification",
                            Description = "Commands to verify snippet implementation.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Check if Optimizely is loaded
console.log(window.optimizely);

// Get the current state
var state = window.optimizely.get('state');
console.log(state);

// Check active experiments
var activeExperiments = state.getActiveExperimentIds();
console.log('Active experiments:', activeExperiments);

// Check visitor information
var visitor = state.getVisitorId();
console.log('Visitor ID:', visitor);",
                            SampleResponse = @"Expected output when snippet is working:

{push: function, get: function}

Active experiments: ['12345678901', '12345678902']
Visitor ID: 'oeu1234567890.1234567890'

If you see 'undefined', the snippet
is not implemented correctly.",
                            Hints = new List<string>
                            {
                                "The optimizely object must exist before you can query it",
                                "Active experiments only show if you meet targeting conditions"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 3: Visual Editor

    private LearningModule BuildVisualEditorModule()
    {
        return new LearningModule
        {
            Id = "visual-editor",
            Title = "Using the Visual Editor",
            Description = "Master the Visual Editor to create experiment variations without coding.",
            Icon = "paint-brush",
            Order = 3,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ve-introduction",
                    ModuleId = "visual-editor",
                    Title = "Introduction to the Visual Editor",
                    Summary = "Learn the basics of Optimizely's Visual Editor.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what the Visual Editor can do",
                        "Navigate the editor interface",
                        "Know the difference between old and new editors"
                    },
                    Content = @"
<h2>The Visual Editor</h2>
<p>The Visual Editor is a <strong>WYSIWYG editor</strong> that lets you modify your website without writing code. You can change text, images, layouts, and more by simply clicking and editing elements.</p>

<h3>Key Features</h3>
<ul>
    <li><strong>Point-and-click editing</strong> - Select elements directly on your page</li>
    <li><strong>Real-time preview</strong> - See changes as you make them</li>
    <li><strong>No code required</strong> - Marketers can create tests independently</li>
    <li><strong>Custom code option</strong> - Add JavaScript/CSS when needed</li>
</ul>

<h3>New vs Original Visual Editor</h3>
<p>Optimizely offers two versions:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Feature</th>
            <th class=""px-4 py-2 text-left"">New Editor</th>
            <th class=""px-4 py-2 text-left"">Original Editor</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Interface</td><td class=""px-4 py-2"">Overlay on live site</td><td class=""px-4 py-2"">Iframe-based</td></tr>
        <tr><td class=""px-4 py-2"">Interaction</td><td class=""px-4 py-2"">Direct site interaction</td><td class=""px-4 py-2"">Simulated environment</td></tr>
        <tr><td class=""px-4 py-2"">Opal AI</td><td class=""px-4 py-2"">Integrated</td><td class=""px-4 py-2"">Not available</td></tr>
    </tbody>
</table>
"
                },
                new Lesson
                {
                    Id = "ve-making-changes",
                    ModuleId = "visual-editor",
                    Title = "Making Visual Changes",
                    Summary = "Learn how to edit page elements using the Visual Editor.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Edit text and images",
                        "Change element styles",
                        "Rearrange page layouts"
                    },
                    Content = @"
<h2>Editing Elements</h2>
<p>The Visual Editor allows you to make various types of changes to your page elements.</p>

<h3>Text Changes</h3>
<ul>
    <li>Click on any text element to edit</li>
    <li>Change copy, formatting, and links</li>
    <li>Modify font size, color, and weight</li>
</ul>

<h3>Image Changes</h3>
<ul>
    <li>Swap images with new ones</li>
    <li>Adjust image dimensions</li>
    <li>Change alt text and links</li>
</ul>

<h3>Layout Changes</h3>
<ul>
    <li>Hide or show elements</li>
    <li>Move elements to new positions</li>
    <li>Insert new content blocks</li>
</ul>

<h3>Style Changes</h3>
<ul>
    <li>Background colors and images</li>
    <li>Borders and shadows</li>
    <li>Padding and margins</li>
</ul>

<h3>Important Notes</h3>
<p>Changes made in the Visual Editor:</p>
<ul>
    <li>Do NOT modify your actual website code</li>
    <li>Only apply to visitors in that variation</li>
    <li>Are reversible by pausing the experiment</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "ve-custom-code",
                    ModuleId = "visual-editor",
                    Title = "Adding Custom Code",
                    Summary = "Learn to extend the Visual Editor with custom JavaScript and CSS.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Add custom JavaScript to variations",
                        "Use custom CSS for styling",
                        "Understand shared vs variation code"
                    },
                    Content = @"
<h2>Custom Code in Experiments</h2>
<p>When visual changes aren't enough, you can add custom JavaScript and CSS to your experiments.</p>

<h3>Types of Custom Code</h3>
<ul>
    <li><strong>Experiment Code (Shared)</strong> - Runs for ALL variations in the experiment</li>
    <li><strong>Variation Code</strong> - Runs only for a specific variation</li>
    <li><strong>Project JavaScript</strong> - Runs on every page with the snippet</li>
</ul>

<h3>When to Use Custom Code</h3>
<ul>
    <li>Complex DOM manipulations</li>
    <li>Dynamic content changes</li>
    <li>Integration with other scripts</li>
    <li>Changes that the Visual Editor can't make</li>
</ul>

<h3>Code Timing Options</h3>
<ul>
    <li><strong>Synchronous</strong> - Runs immediately, before page is visible (prevents flashing)</li>
    <li><strong>Asynchronous</strong> - Runs after snippet loads (for heavier changes)</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ve-custom-code-example",
                            Title = "Custom Code Examples",
                            Description = "JavaScript and CSS examples for variations.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Variation JavaScript - Change headline dynamically
var headline = document.querySelector('h1.hero-title');
if (headline) {
    headline.textContent = 'New Optimized Headline';
    headline.style.color = '#0037FF';
}

// Wait for element to exist (for dynamic content)
var checkExist = setInterval(function() {
    var element = document.querySelector('.dynamic-element');
    if (element) {
        element.innerHTML = 'Modified Content';
        clearInterval(checkExist);
    }
}, 100);

/* Variation CSS */
.hero-section {
    background-color: #f0f4ff;
    padding: 40px 20px;
}

.cta-button {
    background-color: #0037FF;
    font-size: 18px;
}",
                            SampleResponse = @"Best practices:
1. Always check if elements exist before modifying
2. Use intervals for dynamically loaded content
3. Keep synchronous code lightweight
4. Use CSS for styling, JS for logic

The code runs in the context of the page,
so you have access to all page elements
and JavaScript libraries.",
                            Hints = new List<string>
                            {
                                "Synchronous code should complete in under 100ms",
                                "Use CSS classes instead of inline styles when possible"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 4: Audience Targeting

    private LearningModule BuildAudienceTargetingModule()
    {
        return new LearningModule
        {
            Id = "audience-targeting",
            Title = "Audience Targeting",
            Description = "Learn to target experiments to specific visitor segments.",
            Icon = "users",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "at-introduction",
                    ModuleId = "audience-targeting",
                    Title = "Understanding Audiences",
                    Summary = "Learn the fundamentals of audience targeting in Web Experimentation.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what audiences are",
                        "Learn how audience conditions work",
                        "Know the difference between targeting and segmentation"
                    },
                    Content = @"
<h2>Audiences in Web Experimentation</h2>
<p>Audiences let you <strong>control which visitors see your experiments</strong>. Instead of showing experiments to everyone, you can target specific segments based on various conditions.</p>

<h3>How Audiences Work</h3>
<p>Audiences are defined using <strong>conditions</strong> - rules like ""Browser equals Chrome"" or ""Device is Mobile"". You can combine conditions with AND/OR logic:</p>
<ul>
    <li><strong>OR conditions</strong> - Expand your audience (US OR Canada)</li>
    <li><strong>AND conditions</strong> - Narrow your audience (US AND Mobile)</li>
</ul>

<h3>Targeting vs Segmentation</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Targeting</th>
            <th class=""px-4 py-2 text-left"">Segmentation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Controls who SEES the experiment</td><td class=""px-4 py-2"">Filters results for analysis</td></tr>
        <tr><td class=""px-4 py-2"">Applied before experiment runs</td><td class=""px-4 py-2"">Applied after data collection</td></tr>
        <tr><td class=""px-4 py-2"">Reduces experiment reach</td><td class=""px-4 py-2"">Breaks down full results</td></tr>
    </tbody>
</table>
"
                },
                new Lesson
                {
                    Id = "at-condition-types",
                    ModuleId = "audience-targeting",
                    Title = "Audience Condition Types",
                    Summary = "Explore the different condition types available for targeting.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Learn the built-in condition types",
                        "Understand when to use each type",
                        "Know the limitations of each condition"
                    },
                    Content = @"
<h2>Built-in Condition Types</h2>
<p>Optimizely provides several built-in condition types for audience targeting:</p>

<h3>Browser</h3>
<p>Target visitors using specific browsers: Chrome, Firefox, Safari, Edge, etc.</p>

<h3>Device</h3>
<p>Target by device type: Desktop, Mobile Phone, Tablet, iPhone, iPad.</p>

<h3>Location</h3>
<p>Target by geographic location: Country, Region, City, DMA.</p>

<h3>Language</h3>
<p>Target based on browser language preference.</p>

<h3>Cookie</h3>
<p>Target visitors with specific cookies or cookie values. Options include:</p>
<ul>
    <li>Has any value</li>
    <li>Contains a substring</li>
    <li>Matches regex pattern</li>
</ul>

<h3>Query Parameter</h3>
<p>Target based on URL query parameters (e.g., ?utm_source=google).</p>

<h3>IP Address</h3>
<p>Target specific IP addresses or ranges.</p>

<h3>New/Returning Visitor</h3>
<p>Target first-time visitors vs returning visitors (session-based).</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "at-conditions-example",
                            Title = "Example Audience Conditions",
                            Description = "Common audience targeting scenarios.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Example 1: Mobile Users in the US
Conditions:
  - Device equals ""Mobile Phone""
  AND
  - Location Country equals ""United States""

Example 2: Returning Chrome Users
Conditions:
  - Browser equals ""Chrome""
  AND
  - Visitor Type equals ""Returning""

Example 3: Campaign Traffic
Conditions:
  - Query Parameter ""utm_campaign"" has any value
  OR
  - Query Parameter ""gclid"" has any value

Example 4: Logged-in Users (via cookie)
Conditions:
  - Cookie ""user_logged_in"" equals ""true""",
                            SampleResponse = @"Tips for building audiences:
1. Start broad, then narrow down
2. Use OR to expand reach
3. Use AND to be more specific
4. Test your audience conditions in Preview",
                            Hints = new List<string>
                            {
                                "Cookie conditions are case-sensitive",
                                "Location targeting uses IP-based geolocation"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "at-custom-js",
                    ModuleId = "audience-targeting",
                    Title = "Custom JavaScript Conditions",
                    Summary = "Create advanced targeting conditions with custom JavaScript.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Write custom JavaScript conditions",
                        "Access page and visitor data",
                        "Handle asynchronous conditions"
                    },
                    Content = @"
<h2>Custom JavaScript Targeting</h2>
<p>When built-in conditions aren't enough, you can write <strong>custom JavaScript</strong> to create complex targeting rules.</p>

<h3>How It Works</h3>
<p>Your JavaScript must return a boolean value:</p>
<ul>
    <li><strong>true</strong> - Visitor qualifies for the audience</li>
    <li><strong>false</strong> - Visitor does not qualify</li>
</ul>

<h3>What You Can Access</h3>
<ul>
    <li>DOM elements and content</li>
    <li>JavaScript variables on the page</li>
    <li>Cookies and localStorage</li>
    <li>Data layer values (GTM, etc.)</li>
</ul>

<h3>Important Considerations</h3>
<ul>
    <li>Code runs synchronously by default</li>
    <li>URL targeting still applies alongside custom JS</li>
    <li>Keep conditions lightweight for performance</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "at-customjs-example",
                            Title = "Custom JavaScript Examples",
                            Description = "Real-world custom targeting conditions.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Target users with items in cart
function() {
    var cartCount = document.querySelector('.cart-count');
    return cartCount && parseInt(cartCount.textContent) > 0;
}

// Target based on data layer value
function() {
    return window.dataLayer &&
           window.dataLayer.some(function(item) {
               return item.userType === 'premium';
           });
}

// Target based on localStorage value
function() {
    var prefs = localStorage.getItem('user_preferences');
    if (prefs) {
        var parsed = JSON.parse(prefs);
        return parsed.newsletter_subscriber === true;
    }
    return false;
}

// Target high-value customers
function() {
    var totalSpent = window.customerData?.totalSpent || 0;
    return totalSpent > 1000;
}",
                            SampleResponse = @"Custom JS conditions:
- Must be wrapped in a function
- Must return true or false
- Execute on page load
- Have access to full page context

Use these for targeting based on:
- Dynamic page content
- Custom data layers
- User state information
- Complex business logic",
                            Hints = new List<string>
                            {
                                "Always handle cases where data might not exist",
                                "Use try/catch for error handling in complex conditions"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 5: Events and Tracking

    private LearningModule BuildEventsTrackingModule()
    {
        return new LearningModule
        {
            Id = "events-tracking",
            Title = "Events and Conversion Tracking",
            Description = "Learn to track user actions and measure experiment success.",
            Icon = "cursor-arrow-rays",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "et-introduction",
                    ModuleId = "events-tracking",
                    Title = "Understanding Events",
                    Summary = "Learn the fundamentals of event tracking in Web Experimentation.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what events are",
                        "Learn how conversions are counted",
                        "Know the difference between event types"
                    },
                    Content = @"
<h2>Events in Web Experimentation</h2>
<p>Events are <strong>user actions that you track</strong> to measure experiment success. When visitors perform these actions, they're counted as conversions.</p>

<h3>Types of Events</h3>
<ul>
    <li><strong>Click Events</strong> - Track clicks on specific elements</li>
    <li><strong>Page View Events</strong> - Track visits to specific pages</li>
    <li><strong>Custom Events</strong> - Track any action via JavaScript</li>
</ul>

<h3>How Conversions Work</h3>
<p>Optimizely uses <strong>user-scoped event attribution</strong>:</p>
<ol>
    <li>Visitor is shown a variation</li>
    <li>All subsequent conversions are attributed to that variation</li>
    <li>Attribution continues for the experiment's duration</li>
</ol>

<h3>Event Properties</h3>
<p>Custom events can include additional data:</p>
<ul>
    <li>Revenue values</li>
    <li>Product information</li>
    <li>Custom attributes</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "et-custom-events",
                    ModuleId = "events-tracking",
                    Title = "Custom Event Tracking",
                    Summary = "Implement custom event tracking for complex user actions.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create custom events",
                        "Implement event tracking code",
                        "Track revenue and other properties"
                    },
                    Content = @"
<h2>Custom Event Tracking</h2>
<p>Custom events let you track actions that aren't simple clicks or page views - like form submissions, video plays, or purchases.</p>

<h3>Creating a Custom Event</h3>
<ol>
    <li>Go to Implementation > Events in Optimizely</li>
    <li>Click ""Create New Event""</li>
    <li>Choose ""Custom Event""</li>
    <li>Set an API Name (e.g., ""form_submit"")</li>
</ol>

<h3>The Event API</h3>
<p>Track events using the push API:</p>
<pre><code>window['optimizely'].push({
    type: 'event',
    eventName: 'your_event_name'
});</code></pre>

<h3>Adding Event Properties</h3>
<p>Include additional data with your events:</p>
<pre><code>window['optimizely'].push({
    type: 'event',
    eventName: 'purchase',
    tags: {
        revenue: 9999, // in cents
        value: 99.99
    }
});</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "et-custom-example",
                            Title = "Custom Event Examples",
                            Description = "Common custom event implementations.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Track form submission
document.querySelector('form').addEventListener('submit', function() {
    window['optimizely'] = window['optimizely'] || [];
    window['optimizely'].push({
        type: 'event',
        eventName: 'form_submitted'
    });
});

// Track video play
document.querySelector('video').addEventListener('play', function() {
    window['optimizely'].push({
        type: 'event',
        eventName: 'video_started'
    });
});

// Track purchase with revenue
function trackPurchase(orderTotal, orderId) {
    window['optimizely'].push({
        type: 'event',
        eventName: 'purchase_complete',
        tags: {
            revenue: Math.round(orderTotal * 100), // Convert to cents
            value: orderTotal,
            order_id: orderId
        }
    });
}

// Track scroll depth
var tracked = false;
window.addEventListener('scroll', function() {
    if (!tracked) {
        var scrollPercent = (window.scrollY /
            (document.body.scrollHeight - window.innerHeight)) * 100;
        if (scrollPercent >= 50) {
            window['optimizely'].push({
                type: 'event',
                eventName: 'scrolled_50_percent'
            });
            tracked = true;
        }
    }
});",
                            SampleResponse = @"Event tracking tips:

1. Always initialize the optimizely array:
   window['optimizely'] = window['optimizely'] || [];

2. Revenue should be in cents (9999 = $99.99)

3. Event names must match the API Name
   you set in the Optimizely interface

4. Verify events fire using the Network tab
   or Optimizely's Preview mode",
                            Hints = new List<string>
                            {
                                "Events can be fired from anywhere - your code, tag manager, or Optimizely's variation code",
                                "Use the browser Network tab to verify events are being sent"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "et-click-pageview",
                    ModuleId = "events-tracking",
                    Title = "Click and Pageview Events",
                    Summary = "Master the built-in click and pageview event types for conversion tracking.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create click events using the Visual Editor",
                        "Configure pageview events for conversion tracking",
                        "Use CSS selectors for precise click targeting",
                        "Handle dynamic elements and SPAs"
                    },
                    Content = @"
<h2>Click and Pageview Events</h2>
<p>Click and pageview events are the <strong>most common event types</strong> in Web Experimentation. They're easy to set up and cover most conversion tracking needs.</p>

<h3>Click Events</h3>
<p>Track when visitors click on specific elements:</p>
<ul>
    <li><strong>Visual selection</strong> - Click to select elements in the editor</li>
    <li><strong>CSS selectors</strong> - Target elements precisely with selectors</li>
    <li><strong>Multiple elements</strong> - Track clicks on any matching element</li>
</ul>

<h3>CSS Selector Best Practices</h3>
<p>Write robust selectors that won't break:</p>
<ul>
    <li>Use IDs when available: <code>#submit-button</code></li>
    <li>Use data attributes: <code>[data-action=""buy""]</code></li>
    <li>Avoid positional selectors: <code>div:nth-child(3)</code> is fragile</li>
    <li>Use :contains() for text: <code>button:contains(""Add to Cart"")</code></li>
</ul>

<h3>Pageview Events</h3>
<p>Track when visitors reach specific pages:</p>
<ul>
    <li><strong>Exact match</strong> - <code>https://example.com/thank-you</code></li>
    <li><strong>Substring match</strong> - URL contains <code>/confirmation</code></li>
    <li><strong>Regex match</strong> - <code>/order/[0-9]+/complete</code></li>
</ul>

<h3>Dynamic Content Considerations</h3>
<p>For SPAs and dynamically loaded content:</p>
<ul>
    <li>Use manual page activation for route changes</li>
    <li>Consider MutationObserver for dynamic elements</li>
    <li>Test thoroughly with different navigation patterns</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "et-click-selectors",
                            Title = "Click Event Selector Examples",
                            Description = "CSS selectors for common click tracking scenarios.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Common CSS Selectors for Click Events

// By ID (most reliable)
#add-to-cart-button
#newsletter-signup

// By data attribute (recommended)
[data-track=""cta-click""]
[data-button-type=""purchase""]

// By class with context
.product-card .buy-button
.header-nav .signup-link

// By text content (useful for buttons)
button:contains(""Buy Now"")
a:contains(""Sign Up"")

// By attribute value
input[type=""submit""]
a[href*=""/checkout""]

// Multiple elements (tracks all matches)
.product-listing .add-to-cart

// Avoiding fragile selectors (DON'T use)
// div > div > button (depends on structure)
// .container:nth-child(2) .btn (position-dependent)
// body > main > section > div > a (too specific)",
                            SampleResponse = @"Selector reliability ranking:
1. ID selectors (#id) - Most stable
2. Data attributes ([data-*]) - Developer-controlled
3. Semantic classes (.buy-button) - Usually stable
4. Text content (:contains) - Works but fragile
5. Structural selectors - Avoid when possible

Test selectors in browser console:
document.querySelectorAll('your-selector')",
                            Hints = new List<string>
                            {
                                "Test selectors in browser DevTools before using in Optimizely",
                                "Ask developers to add data-track attributes for important elements"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "et-revenue",
                    ModuleId = "events-tracking",
                    Title = "Revenue Tracking",
                    Summary = "Implement revenue and monetary value tracking for e-commerce experiments.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Track purchase revenue accurately",
                        "Implement value-per-conversion metrics",
                        "Handle different currency scenarios",
                        "Verify revenue data in results"
                    },
                    Content = @"
<h2>Revenue Tracking</h2>
<p>Revenue tracking lets you measure the <strong>monetary impact</strong> of your experiments, not just conversion counts. This is essential for calculating ROI and prioritizing winning variations.</p>

<h3>The Revenue Tag</h3>
<p>Revenue is passed as a <strong>tag</strong> on your event:</p>
<pre><code>window['optimizely'].push({
    type: 'event',
    eventName: 'purchase',
    tags: {
        revenue: 4999  // Amount in CENTS
    }
});</code></pre>

<h3>Important: Cents, Not Dollars</h3>
<p>Revenue must be in the <strong>smallest currency unit</strong> (cents for USD):</p>
<ul>
    <li><code>$49.99</code> → <code>revenue: 4999</code></li>
    <li><code>$100.00</code> → <code>revenue: 10000</code></li>
    <li><code>€25.50</code> → <code>revenue: 2550</code></li>
</ul>

<h3>Revenue Metrics in Results</h3>
<p>With revenue tracking, you'll see:</p>
<ul>
    <li><strong>Total Revenue</strong> - Sum of all revenue per variation</li>
    <li><strong>Revenue per Visitor</strong> - Average revenue across all visitors</li>
    <li><strong>Revenue per Conversion</strong> - Average order value</li>
</ul>

<h3>Additional Value Tags</h3>
<p>You can also pass supplementary data:</p>
<pre><code>tags: {
    revenue: 4999,           // Required for revenue metrics
    value: 49.99,            // Human-readable value
    quantity: 2,             // Number of items
    category: 'electronics'  // Custom segmentation
}</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "et-revenue-example",
                            Title = "E-commerce Revenue Tracking",
                            Description = "Complete implementation for tracking purchase revenue.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Complete e-commerce revenue tracking

// On order confirmation page
function trackOrder(orderData) {
    // Ensure Optimizely is available
    window['optimizely'] = window['optimizely'] || [];

    // Convert dollars to cents and round
    var revenueInCents = Math.round(orderData.total * 100);

    window['optimizely'].push({
        type: 'event',
        eventName: 'purchase_complete',
        tags: {
            revenue: revenueInCents,
            value: orderData.total,
            orderId: orderData.id,
            quantity: orderData.itemCount,
            category: orderData.primaryCategory
        }
    });

    console.log('Optimizely: Tracked purchase', revenueInCents, 'cents');
}

// Example usage with order data
trackOrder({
    id: 'ORD-12345',
    total: 149.99,
    itemCount: 3,
    primaryCategory: 'clothing'
});

// For subscription/recurring revenue
function trackSubscription(plan, monthlyValue) {
    window['optimizely'].push({
        type: 'event',
        eventName: 'subscription_started',
        tags: {
            revenue: Math.round(monthlyValue * 100),
            value: monthlyValue,
            plan: plan
        }
    });
}

// Track add-to-cart value (not revenue, just value)
function trackAddToCart(productPrice) {
    window['optimizely'].push({
        type: 'event',
        eventName: 'add_to_cart',
        tags: {
            value: productPrice  // No 'revenue' - this isn't a purchase
        }
    });
}",
                            SampleResponse = @"Revenue tracking best practices:

1. Always use cents (multiply by 100)
2. Use Math.round() to avoid floating point issues
3. Only use 'revenue' tag for actual purchases
4. Use 'value' for non-purchase monetary values
5. Include orderId to prevent duplicates
6. Test with known values before launch

Debugging tip:
Check Network tab for 'logx.optimizely.com'
requests to verify revenue is being sent.",
                            Hints = new List<string>
                            {
                                "Revenue appears in results within minutes of tracking",
                                "Use order ID to help identify duplicate tracking issues"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "et-debugging",
                    ModuleId = "events-tracking",
                    Title = "Event Debugging and Validation",
                    Summary = "Verify your events are tracking correctly before and after launch.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Use browser dev tools to verify event firing",
                        "Debug common tracking issues",
                        "Validate events in Preview mode",
                        "Understand event batching and timing"
                    },
                    Content = @"
<h2>Event Debugging and Validation</h2>
<p>Proper event validation is <strong>critical</strong> before launching experiments. Incorrect tracking leads to misleading results and poor decisions.</p>

<h3>Console Verification</h3>
<p>Check if events are configured:</p>
<pre><code>// List all configured events
var data = window['optimizely'].get('data');
console.log(data.events);

// Check specific event
var events = data.events;
for (var id in events) {
    console.log(events[id].apiName, events[id]);
}</code></pre>

<h3>Network Tab Inspection</h3>
<p>Watch for requests to <code>logx.optimizely.com</code>:</p>
<ol>
    <li>Open DevTools → Network tab</li>
    <li>Filter by ""logx"" or ""optimizely""</li>
    <li>Trigger your event (click, form submit, etc.)</li>
    <li>Look for POST request with event data</li>
</ol>

<h3>Common Issues</h3>
<ul>
    <li><strong>Event name mismatch</strong> - API name must match exactly</li>
    <li><strong>Timing issues</strong> - Event fires before Optimizely loads</li>
    <li><strong>Selector problems</strong> - Element doesn't match selector</li>
    <li><strong>SPA navigation</strong> - Page not re-activated after route change</li>
</ul>

<h3>Event Callbacks</h3>
<p>Confirm events fire with callbacks:</p>
<pre><code>window['optimizely'].push({
    type: 'addListener',
    filter: { type: 'analytics', name: 'trackEvent' },
    handler: function(event) {
        console.log('Event tracked:', event.data.name);
    }
});</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "et-debug-workflow",
                            Title = "Complete Event Debugging Workflow",
                            Description = "Step-by-step process to verify event tracking.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// STEP 1: Verify Optimizely is loaded
console.log('Optimizely loaded:', typeof window.optimizely !== 'undefined');
console.log('Optimizely object:', window.optimizely);

// STEP 2: Check configured events
var data = window['optimizely'].get('data');
console.table(Object.values(data.events).map(e => ({
    name: e.apiName,
    id: e.id,
    type: e.eventType
})));

// STEP 3: Set up event listener to confirm firing
window['optimizely'].push({
    type: 'addListener',
    filter: { type: 'analytics', name: 'trackEvent' },
    handler: function(event) {
        console.log('%c Event Tracked! ', 'background: green; color: white');
        console.log('Event name:', event.data.name);
        console.log('Event tags:', event.data.tags);
    }
});

// STEP 4: Manually trigger test event
window['optimizely'].push({
    type: 'event',
    eventName: 'your_event_name',
    tags: { test: true }
});

// STEP 5: Check active experiments receiving events
var state = window['optimizely'].get('state');
var activeExps = state.getActiveExperimentIds();
console.log('Active experiments:', activeExps);

// STEP 6: Verify in Network tab
// Filter: logx.optimizely.com
// Look for POST requests after triggering events
// Request payload shows event data being sent",
                            SampleResponse = @"Debugging checklist:

✓ Optimizely object exists
✓ Event is configured (appears in data.events)
✓ Event listener confirms firing
✓ Network request sent to logx.optimizely.com
✓ Active experiment is receiving events
✓ Preview mode shows conversion

Common fixes:
- Event name typo → Check exact API name
- No network request → Event not firing
- Event fires but no conversion → Check experiment targeting
- Duplicate events → Add deduplication logic",
                            Hints = new List<string>
                            {
                                "Use Preview mode's 'Events' tab for visual event debugging",
                                "Events may batch and send together - wait a few seconds"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 6: JavaScript API

    private LearningModule BuildJavaScriptAPIModule()
    {
        return new LearningModule
        {
            Id = "javascript-api",
            Title = "JavaScript API",
            Description = "Master the Optimizely JavaScript API for advanced experimentation.",
            Icon = "command-line",
            Order = 6,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "jsapi-overview",
                    ModuleId = "javascript-api",
                    Title = "API Overview",
                    Summary = "Understand the Optimizely JavaScript API architecture.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the API structure",
                        "Learn the difference between push and get",
                        "Know what data is available"
                    },
                    Content = @"
<h2>The Optimizely JavaScript API</h2>
<p>The JavaScript API provides <strong>programmatic access</strong> to Optimizely's functionality, allowing you to control experiments, access visitor data, and integrate with other systems.</p>

<h3>API Structure</h3>
<p>The API has two main operation types:</p>
<ul>
    <li><strong>PUSH</strong> - Send commands to modify behavior</li>
    <li><strong>GET</strong> - Retrieve state and data</li>
</ul>

<h3>The Push API</h3>
<pre><code>window['optimizely'].push({
    type: 'command_type',
    // additional parameters
});</code></pre>

<h3>The Get API</h3>
<pre><code>var data = window['optimizely'].get('data_type');</code></pre>

<h3>Available Data</h3>
<ul>
    <li>Experiment and variation information</li>
    <li>Visitor ID and attributes</li>
    <li>Page and event configuration</li>
    <li>Audience membership</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "jsapi-state",
                    ModuleId = "javascript-api",
                    Title = "Querying State",
                    Summary = "Learn to retrieve experiment and visitor state information.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Query active experiments",
                        "Get variation assignments",
                        "Access visitor information"
                    },
                    Content = @"
<h2>Querying Optimizely State</h2>
<p>Use the GET API to retrieve information about the current state of experiments and visitors.</p>

<h3>Getting the State Object</h3>
<pre><code>var state = window['optimizely'].get('state');</code></pre>

<h3>Useful State Methods</h3>
<ul>
    <li><code>getActiveExperimentIds()</code> - List of running experiments</li>
    <li><code>getVariationMap()</code> - Experiment to variation mappings</li>
    <li><code>getPageStates()</code> - Information about activated pages</li>
    <li><code>getVisitorId()</code> - Unique visitor identifier</li>
</ul>

<h3>Getting Visitor Data</h3>
<pre><code>var visitor = window['optimizely'].get('visitor');
var visitorId = visitor.visitorId;
var attributes = visitor.custom;</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "jsapi-state-example",
                            Title = "State Query Examples",
                            Description = "Common state queries and their uses.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Get the state object
var state = window['optimizely'].get('state');

// Get active experiment IDs
var activeExperiments = state.getActiveExperimentIds();
console.log('Active experiments:', activeExperiments);

// Get variation map (experiment ID -> variation ID)
var variationMap = state.getVariationMap();
console.log('Variation assignments:', variationMap);

// Check if visitor is in a specific experiment
var experimentId = '12345678901';
var isInExperiment = activeExperiments.indexOf(experimentId) !== -1;

// Get the variation for a specific experiment
var variationId = variationMap[experimentId];

// Get visitor ID for analytics integration
var visitorId = state.getVisitorId();
console.log('Visitor ID:', visitorId);

// Get all campaign states
var campaignStates = state.getCampaignStates();
for (var campaignId in campaignStates) {
    var campaign = campaignStates[campaignId];
    console.log('Campaign:', campaign.campaignName);
    console.log('Variation:', campaign.variation.name);
}",
                            SampleResponse = @"Example output:

Active experiments: ['12345678901', '12345678902']

Variation assignments: {
    '12345678901': '12345678903',
    '12345678902': '12345678904'
}

Visitor ID: 'oeu1609459200.1234567890'

Campaign: Homepage Hero Test
Variation: Variation 1",
                            Hints = new List<string>
                            {
                                "State queries are synchronous and return immediately",
                                "Use variation map to send data to your analytics"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "jsapi-activation",
                    ModuleId = "javascript-api",
                    Title = "Manual Activation",
                    Summary = "Control when experiments activate using the JavaScript API.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand manual activation mode",
                        "Trigger experiments programmatically",
                        "Handle single-page applications"
                    },
                    Content = @"
<h2>Manual Experiment Activation</h2>
<p>By default, experiments activate automatically when the page loads. <strong>Manual activation</strong> lets you control exactly when experiments start.</p>

<h3>When to Use Manual Activation</h3>
<ul>
    <li>Single-page applications (SPAs)</li>
    <li>Dynamic content loading</li>
    <li>Experiments based on user actions</li>
    <li>Content behind authentication</li>
</ul>

<h3>Setting Up Manual Activation</h3>
<ol>
    <li>Create a Page with activation mode set to ""Manual""</li>
    <li>Use the activate API when ready</li>
</ol>

<h3>The Activate API</h3>
<pre><code>window['optimizely'].push({
    type: 'page',
    pageName: 'your_page_name'
});</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "jsapi-activation-example",
                            Title = "Manual Activation Examples",
                            Description = "Triggering experiments in different scenarios.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Activate a specific page/experiment
window['optimizely'].push({
    type: 'page',
    pageName: 'product_detail_page'
});

// Activate on route change (SPA)
window.addEventListener('popstate', function() {
    if (window.location.pathname.includes('/products/')) {
        window['optimizely'].push({
            type: 'page',
            pageName: 'product_page'
        });
    }
});

// Activate after content loads
fetch('/api/content')
    .then(response => response.json())
    .then(data => {
        // Render content
        renderContent(data);

        // Now activate the experiment
        window['optimizely'].push({
            type: 'page',
            pageName: 'dynamic_content_page'
        });
    });

// Activate on user action
document.querySelector('.show-modal').addEventListener('click', function() {
    window['optimizely'].push({
        type: 'page',
        pageName: 'modal_experiment'
    });
});",
                            SampleResponse = @"Manual activation is essential for:

1. SPAs using React, Vue, Angular
   - Activate on route changes
   - Re-activate when components mount

2. Dynamic content
   - Wait for AJAX to complete
   - Activate after content renders

3. Conditional experiments
   - Activate based on user state
   - Trigger on specific interactions",
                            Hints = new List<string>
                            {
                                "The pageName must match exactly what you configured in Optimizely",
                                "You can activate the same page multiple times safely"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 7: Stats Engine

    private LearningModule BuildStatsEngineModule()
    {
        return new LearningModule
        {
            Id = "stats-engine",
            Title = "Stats Engine and Results",
            Description = "Understand statistical analysis and interpret experiment results.",
            Icon = "chart-bar",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "se-introduction",
                    ModuleId = "stats-engine",
                    Title = "Understanding Stats Engine",
                    Summary = "Learn how Optimizely's Stats Engine analyzes experiment results.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Stats Engine does",
                        "Learn about statistical significance",
                        "Know how results are calculated"
                    },
                    Content = @"
<h2>Stats Engine Overview</h2>
<p>Optimizely's Stats Engine is a <strong>proprietary statistical framework</strong> that analyzes experiment results with high rigor while making results easy to interpret.</p>

<h3>How Stats Engine Differs</h3>
<p>Unlike traditional statistics tools, Stats Engine uses <strong>sequential testing</strong>:</p>
<ul>
    <li>Results update continuously as data comes in</li>
    <li>No need to wait for a fixed sample size</li>
    <li>Statistical significance increases over time</li>
    <li>Controls for false positives across all metrics</li>
</ul>

<h3>Key Metrics</h3>
<ul>
    <li><strong>Statistical Significance</strong> - Confidence that results aren't due to chance</li>
    <li><strong>Improvement</strong> - Percentage change vs baseline</li>
    <li><strong>Confidence Interval</strong> - Range of likely true improvement</li>
</ul>

<h3>False Discovery Rate Control</h3>
<p>Stats Engine uses the <strong>Benjamini-Hochberg procedure</strong> to control false discovery rates when testing multiple metrics and variations simultaneously.</p>
"
                },
                new Lesson
                {
                    Id = "se-interpreting",
                    ModuleId = "stats-engine",
                    Title = "Interpreting Results",
                    Summary = "Learn how to read and act on experiment results.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Read the Results page",
                        "Understand when to call a winner",
                        "Make data-driven decisions"
                    },
                    Content = @"
<h2>Reading Experiment Results</h2>
<p>The Results page provides comprehensive data about your experiment's performance.</p>

<h3>Result States</h3>
<ul>
    <li><strong>Winner</strong> - Variation significantly outperforms baseline</li>
    <li><strong>Loser</strong> - Variation significantly underperforms baseline</li>
    <li><strong>Inconclusive</strong> - Not enough evidence either way</li>
</ul>

<h3>When to Declare a Winner</h3>
<p>Consider these factors:</p>
<ol>
    <li>Statistical significance reaches your threshold (typically 90-95%)</li>
    <li>Experiment has run for at least 1-2 business cycles</li>
    <li>Sample size is sufficient for reliable results</li>
    <li>Results are consistent over time</li>
</ol>

<h3>Common Pitfalls</h3>
<ul>
    <li><strong>Peeking</strong> - Making decisions before significance is reached</li>
    <li><strong>Short duration</strong> - Not accounting for day-of-week effects</li>
    <li><strong>Wrong metric</strong> - Optimizing for a metric that doesn't matter</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "se-results-example",
                            Title = "Results Interpretation",
                            Description = "Understanding experiment results data.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Experiment: Homepage CTA Test
Duration: 14 days
Total Visitors: 50,000

Baseline (Original)
- Conversions: 1,250
- Conversion Rate: 5.0%

Variation 1 (New CTA)
- Conversions: 1,425
- Conversion Rate: 5.7%
- Improvement: +14%
- Statistical Significance: 95%
- Confidence Interval: [+8%, +20%]

Interpretation:
- Variation 1 is a WINNER
- We're 95% confident the true improvement is between 8-20%
- Safe to implement this change",
                            SampleResponse = @"Decision framework:

1. Check statistical significance
   - 95%+ = high confidence
   - 90-95% = moderate confidence
   - <90% = need more data

2. Review the confidence interval
   - Does the range include 0?
   - Is the potential upside worth it?

3. Consider business impact
   - What's the revenue impact?
   - Are there implementation costs?

4. Validate with secondary metrics
   - Did other metrics improve too?
   - Any negative side effects?",
                            Hints = new List<string>
                            {
                                "Run experiments for at least 1-2 full weeks to account for weekly patterns",
                                "The confidence interval shows the range of likely true effect sizes"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "se-significance",
                    ModuleId = "stats-engine",
                    Title = "Statistical Significance Deep Dive",
                    Summary = "Understand statistical significance, confidence levels, and common pitfalls.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what 90%, 95%, 99% significance means",
                        "Learn about Type I and Type II errors",
                        "Know when to trust your results",
                        "Avoid common statistical pitfalls"
                    },
                    Content = @"
<h2>Understanding Statistical Significance</h2>
<p>Statistical significance tells you the <strong>probability that your results are real</strong>, not due to random chance. But it's often misunderstood.</p>

<h3>What Significance Really Means</h3>
<p>A 95% significance level means:</p>
<ul>
    <li>If there was NO real difference, there's only a 5% chance of seeing these results</li>
    <li>It does NOT mean there's a 95% chance the variation is better</li>
    <li>It's about ruling out randomness, not confirming success</li>
</ul>

<h3>Type I and Type II Errors</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Error Type</th>
            <th class=""px-4 py-2 text-left"">What Happens</th>
            <th class=""px-4 py-2 text-left"">Consequence</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Type I (False Positive)</td><td class=""px-4 py-2"">Declare winner when there isn't one</td><td class=""px-4 py-2"">Ship a bad change</td></tr>
        <tr><td class=""px-4 py-2"">Type II (False Negative)</td><td class=""px-4 py-2"">Miss a real winner</td><td class=""px-4 py-2"">Abandon a good idea</td></tr>
    </tbody>
</table>

<h3>The Peeking Problem</h3>
<p>Traditional statistics assume you only look at results once. Every time you ""peek"":</p>
<ul>
    <li>You increase false positive rate</li>
    <li>5 peeks at 95% confidence = ~19% false positive rate</li>
    <li>Stats Engine solves this with sequential testing</li>
</ul>

<h3>Choosing Your Significance Threshold</h3>
<ul>
    <li><strong>90%</strong> - Acceptable for low-risk tests</li>
    <li><strong>95%</strong> - Standard for most experiments</li>
    <li><strong>99%</strong> - For high-stakes decisions</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "se-sig-scenarios",
                            Title = "Significance Scenarios",
                            Description = "How to interpret different significance levels.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Scenario 1: Clear Winner
- Significance: 98%
- Improvement: +15%
- Confidence Interval: [+10%, +20%]
→ Strong evidence. Safe to implement.

Scenario 2: Borderline Result
- Significance: 91%
- Improvement: +8%
- Confidence Interval: [-1%, +17%]
→ Weak evidence. Consider running longer.

Scenario 3: Significant but Small Effect
- Significance: 99%
- Improvement: +0.5%
- Confidence Interval: [+0.2%, +0.8%]
→ Real effect, but is it worth implementing?

Scenario 4: Flat Result
- Significance: 45%
- Improvement: +2%
- Confidence Interval: [-5%, +9%]
→ No evidence of difference. Test different hypothesis.

Scenario 5: Significant Loser
- Significance: 97%
- Improvement: -12%
- Confidence Interval: [-18%, -6%]
→ Variation is hurting performance. Stop experiment.",
                            SampleResponse = @"Decision matrix:

High Significance + Large Effect
→ Implement with confidence

High Significance + Small Effect
→ Consider implementation cost vs benefit

Low Significance + Any Effect
→ Don't make decisions yet

Significant Negative
→ Stop immediately, learn from failure",
                            Hints = new List<string>
                            {
                                "Statistical significance doesn't equal practical significance",
                                "Always consider the confidence interval, not just the point estimate"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "se-sample-size",
                    ModuleId = "stats-engine",
                    Title = "Sample Size and Duration Planning",
                    Summary = "Plan experiments with proper sample sizes and duration for reliable results.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Calculate required sample sizes",
                        "Understand minimum detectable effect (MDE)",
                        "Plan experiment duration effectively",
                        "Balance speed with statistical power"
                    },
                    Content = @"
<h2>Planning Sample Size and Duration</h2>
<p>Running experiments with insufficient sample sizes leads to <strong>unreliable results</strong>. Proper planning ensures you can detect real effects.</p>

<h3>Key Concepts</h3>
<ul>
    <li><strong>Minimum Detectable Effect (MDE)</strong> - Smallest improvement you can reliably detect</li>
    <li><strong>Statistical Power</strong> - Probability of detecting a real effect (typically 80%)</li>
    <li><strong>Baseline Conversion Rate</strong> - Your current performance</li>
</ul>

<h3>Sample Size Factors</h3>
<p>Required sample size increases when:</p>
<ul>
    <li>You want to detect smaller effects (lower MDE)</li>
    <li>Your baseline conversion rate is very high or very low</li>
    <li>You need higher confidence (95% vs 90%)</li>
    <li>You have more variations</li>
</ul>

<h3>Duration Considerations</h3>
<ul>
    <li><strong>Minimum 1-2 weeks</strong> - Capture day-of-week effects</li>
    <li><strong>Full business cycles</strong> - Account for weekly/monthly patterns</li>
    <li><strong>Avoid holidays</strong> - Unusual traffic skews results</li>
</ul>

<h3>Sample Size Formula (Simplified)</h3>
<pre><code>n ≈ 16 × (1/MDE²) × (p × (1-p))

Where:
- n = sample size per variation
- MDE = minimum detectable effect (e.g., 0.05 for 5%)
- p = baseline conversion rate</code></pre>

<h3>When to Stop Early</h3>
<ul>
    <li><strong>Clear winner</strong> - Strong significance, consistent over time</li>
    <li><strong>Clear loser</strong> - Significant negative impact</li>
    <li><strong>Futility</strong> - Very unlikely to reach significance</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "se-sample-calc",
                            Title = "Sample Size Planning Examples",
                            Description = "Calculate sample sizes for different scenarios.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Example 1: E-commerce Checkout
- Baseline conversion: 3%
- Want to detect: 10% relative lift (3% → 3.3%)
- Confidence: 95%
- Power: 80%
→ Need: ~85,000 visitors per variation
→ With 10,000 daily visitors: ~17 days minimum

Example 2: Newsletter Signup
- Baseline conversion: 15%
- Want to detect: 5% relative lift (15% → 15.75%)
- Confidence: 95%
- Power: 80%
→ Need: ~50,000 visitors per variation
→ With 5,000 daily visitors: ~20 days minimum

Example 3: High-Traffic Homepage
- Baseline conversion: 8%
- Want to detect: 3% relative lift
- Confidence: 95%
- Power: 80%
→ Need: ~200,000 visitors per variation
→ With 100,000 daily visitors: ~4 days minimum
  BUT run at least 7 days for weekly patterns

Example 4: Low-Traffic B2B Page
- Baseline conversion: 2%
- Want to detect: 25% relative lift
- Confidence: 90% (lower for faster results)
- Power: 80%
→ Need: ~15,000 visitors per variation
→ With 500 daily visitors: ~60 days minimum
  Consider testing bigger changes (larger MDE)",
                            SampleResponse = @"Planning checklist:

1. Know your baseline conversion rate
2. Decide minimum effect worth detecting
3. Calculate required sample size
4. Estimate time to reach sample size
5. Add buffer for weekly patterns
6. Consider if timeline is acceptable
7. If too long: test bigger changes or accept higher MDE",
                            Hints = new List<string>
                            {
                                "Use Optimizely's sample size calculator for accurate estimates",
                                "When in doubt, run longer rather than shorter"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "se-segmentation",
                    ModuleId = "stats-engine",
                    Title = "Segmentation and Advanced Analysis",
                    Summary = "Analyze experiment results across different visitor segments.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create and use result segments",
                        "Identify winning variations per segment",
                        "Avoid segment-based false positives",
                        "Use dimensions for deeper analysis"
                    },
                    Content = @"
<h2>Segmentation in Results Analysis</h2>
<p>Segmentation lets you see how different visitor groups responded to your experiment. This can reveal <strong>hidden insights</strong> that overall results miss.</p>

<h3>Targeting vs Segmentation</h3>
<ul>
    <li><strong>Targeting</strong> - Who SEES the experiment (before)</li>
    <li><strong>Segmentation</strong> - How you ANALYZE results (after)</li>
</ul>

<h3>Built-in Segments</h3>
<p>Optimizely provides automatic segmentation by:</p>
<ul>
    <li>Device type (Desktop, Mobile, Tablet)</li>
    <li>Browser (Chrome, Safari, Firefox, etc.)</li>
    <li>Traffic source (Direct, Referral, Search)</li>
    <li>New vs Returning visitors</li>
</ul>

<h3>Custom Dimensions</h3>
<p>Create custom segments using attributes:</p>
<pre><code>window['optimizely'].push({
    type: 'user',
    attributes: {
        plan_type: 'premium',
        customer_segment: 'enterprise'
    }
});</code></pre>

<h3>The Multiple Comparisons Problem</h3>
<p>Warning: Analyzing many segments increases false positives!</p>
<ul>
    <li>10 segments at 95% confidence ≈ 40% chance of one false positive</li>
    <li>Only trust segment results that were <strong>pre-planned</strong></li>
    <li>Use segment insights to form NEW hypotheses, not declare winners</li>
</ul>

<h3>When Segmentation Helps</h3>
<ul>
    <li>Overall flat, but mobile shows strong positive</li>
    <li>New visitors love it, returning visitors hate it</li>
    <li>Works in US but not in other countries</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "se-segment-analysis",
                            Title = "Segment Analysis Example",
                            Description = "How to properly analyze segmented results.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Experiment: New Product Page Layout
Overall Results: Inconclusive (+2%, 72% significance)

Segment Analysis:
┌─────────────────┬────────────┬──────────────┐
│ Segment         │ Improvement│ Significance │
├─────────────────┼────────────┼──────────────┤
│ Desktop         │ -3%        │ 65%          │
│ Mobile          │ +12%       │ 94%          │
│ Tablet          │ +5%        │ 45%          │
├─────────────────┼────────────┼──────────────┤
│ New Visitors    │ +8%        │ 89%          │
│ Returning       │ -2%        │ 55%          │
├─────────────────┼────────────┼──────────────┤
│ Chrome          │ +3%        │ 68%          │
│ Safari          │ +1%        │ 42%          │
│ Firefox         │ +4%        │ 51%          │
└─────────────────┴────────────┴──────────────┘

Analysis:
1. Mobile shows promising results (pre-planned segment)
2. New visitors trending positive
3. Don't over-interpret browser differences (many comparisons)

Recommended Actions:
- Run mobile-only follow-up experiment
- Investigate why desktop might be negative
- Don't declare browser-specific winners",
                            SampleResponse = @"Segmentation best practices:

1. PRE-PLAN key segments before launch
2. Limit to 3-5 important segments
3. Use segment insights for hypotheses, not decisions
4. Require HIGHER significance for segments
5. Validate segment winners with dedicated tests
6. Document which segments were pre-planned vs explored",
                            Hints = new List<string>
                            {
                                "Pre-registered segments are more trustworthy than post-hoc discoveries",
                                "Consider running segment-specific experiments to validate findings"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 8: Advanced Experiments

    private LearningModule BuildAdvancedExperimentsModule()
    {
        return new LearningModule
        {
            Id = "advanced-experiments",
            Title = "Advanced Experimentation",
            Description = "Master advanced experimentation techniques and patterns.",
            Icon = "beaker",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ae-multipage",
                    ModuleId = "advanced-experiments",
                    Title = "Multi-page Experiments",
                    Summary = "Test changes across multiple pages in a user journey.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create multi-page experiments",
                        "Maintain consistency across pages",
                        "Measure funnel-wide impact"
                    },
                    Content = @"
<h2>Multi-page (Funnel) Experiments</h2>
<p>Multi-page experiments let you test changes across <strong>multiple pages</strong> while ensuring visitors see a consistent experience throughout their journey.</p>

<h3>Use Cases</h3>
<ul>
    <li>Testing a new checkout flow</li>
    <li>Redesigning a multi-step form</li>
    <li>Consistent branding across pages</li>
</ul>

<h3>How It Works</h3>
<ol>
    <li>Create an experiment with multiple pages</li>
    <li>Add the same number of variations to each page</li>
    <li>Visitors bucketed into Variation 1 see Variation 1 on ALL pages</li>
</ol>

<h3>Requirements</h3>
<ul>
    <li>Same number of variations on each page</li>
    <li>Targeting conditions must apply to all pages</li>
    <li>Snippet must be on all pages in the funnel</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "ae-personalization",
                    ModuleId = "advanced-experiments",
                    Title = "Personalization Campaigns",
                    Summary = "Deliver targeted experiences to different audience segments.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand personalization vs experimentation",
                        "Create targeted experiences",
                        "Measure personalization impact"
                    },
                    Content = @"
<h2>Personalization with Optimizely</h2>
<p>While experiments test which variation performs best for ALL visitors, <strong>personalization</strong> delivers different experiences to different audience segments.</p>

<h3>Experiments vs Personalization</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Experiments</th>
            <th class=""px-4 py-2 text-left"">Personalization</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Random assignment</td><td class=""px-4 py-2"">Audience-based assignment</td></tr>
        <tr><td class=""px-4 py-2"">Find one winner for all</td><td class=""px-4 py-2"">Different winner per segment</td></tr>
        <tr><td class=""px-4 py-2"">Temporary (during test)</td><td class=""px-4 py-2"">Permanent (always-on)</td></tr>
    </tbody>
</table>

<h3>Creating Personalized Experiences</h3>
<ol>
    <li>Define your audience segments</li>
    <li>Create experiences for each segment</li>
    <li>Set experience priorities</li>
    <li>Measure performance per segment</li>
</ol>
"
                },
                new Lesson
                {
                    Id = "ae-mutex",
                    ModuleId = "advanced-experiments",
                    Title = "Mutually Exclusive Experiments",
                    Summary = "Run multiple experiments without interference.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand experiment collision",
                        "Create mutually exclusive groups",
                        "Manage experiment traffic"
                    },
                    Content = @"
<h2>Mutually Exclusive Experiments</h2>
<p>When running multiple experiments on the same page, they can <strong>interfere</strong> with each other. Mutually exclusive groups ensure visitors only see one experiment.</p>

<h3>The Problem</h3>
<p>If two experiments both change the same element:</p>
<ul>
    <li>Results become contaminated</li>
    <li>You can't attribute changes accurately</li>
    <li>User experience may be inconsistent</li>
</ul>

<h3>The Solution</h3>
<p>Create <strong>mutually exclusive groups</strong>:</p>
<ul>
    <li>Traffic is divided between experiments</li>
    <li>Each visitor sees only ONE experiment</li>
    <li>Results remain clean and attributable</li>
</ul>

<h3>Traffic Allocation</h3>
<p>With mutual exclusion, you allocate percentages to each experiment:</p>
<ul>
    <li>Experiment A: 50% of traffic</li>
    <li>Experiment B: 50% of traffic</li>
    <li>No visitor sees both</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 9: Integrations

    private LearningModule BuildIntegrationsModule()
    {
        return new LearningModule
        {
            Id = "integrations",
            Title = "Analytics Integrations",
            Description = "Integrate Web Experimentation with analytics and other platforms.",
            Icon = "link",
            Order = 9,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "int-analytics",
                    ModuleId = "integrations",
                    Title = "Analytics Platform Integration",
                    Summary = "Send experiment data to Google Analytics, Adobe Analytics, and more.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Integrate with Google Analytics 4",
                        "Send data to Adobe Analytics",
                        "Use custom integrations"
                    },
                    Content = @"
<h2>Analytics Integrations</h2>
<p>Optimizely can send experiment data to your analytics platform, allowing you to <strong>analyze results alongside other data</strong>.</p>

<h3>Google Analytics 4</h3>
<p>Send experiment and variation data to GA4:</p>
<ul>
    <li>Built-in integration available</li>
    <li>Data appears as custom dimensions</li>
    <li>Analyze experiments with GA4 reports</li>
</ul>

<h3>Adobe Analytics</h3>
<p>Integration with Adobe Analytics enables:</p>
<ul>
    <li>eVar population with experiment data</li>
    <li>Analysis in Adobe workspaces</li>
    <li>Combined with other Adobe data</li>
</ul>

<h3>Custom Integrations</h3>
<p>Use the JavaScript API to send data anywhere:</p>
<pre><code>var state = window['optimizely'].get('state');
var variationMap = state.getVariationMap();
// Send to your analytics system</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "int-ga4-example",
                            Title = "Google Analytics 4 Integration",
                            Description = "Send experiment data to GA4.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Send Optimizely data to GA4
window.optimizely = window.optimizely || [];
window.optimizely.push({
    type: 'addListener',
    filter: {
        type: 'lifecycle',
        name: 'activated'
    },
    handler: function(event) {
        var campaignId = event.data.campaign.id;
        var variationId = event.data.variation.id;
        var campaignName = event.data.campaign.name;
        var variationName = event.data.variation.name;

        // Send to GA4
        gtag('event', 'experiment_impression', {
            'experiment_id': campaignId,
            'experiment_name': campaignName,
            'variation_id': variationId,
            'variation_name': variationName
        });
    }
});

// Alternative: Send on page load
var state = window['optimizely'].get('state');
var campaigns = state.getCampaignStates();

for (var campaignId in campaigns) {
    var campaign = campaigns[campaignId];
    gtag('set', 'user_properties', {
        'optimizely_experiment': campaign.experiment.name,
        'optimizely_variation': campaign.variation.name
    });
}",
                            SampleResponse = @"Integration benefits:

1. Unified analysis
   - See experiment data with all other metrics
   - Build custom reports

2. Deeper segmentation
   - Combine with demographics
   - Analyze by traffic source

3. Long-term tracking
   - Track beyond experiment duration
   - Measure lifetime value impact",
                            Hints = new List<string>
                            {
                                "Use lifecycle listeners to capture experiment activation",
                                "Set experiment data as user properties for cross-session analysis"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "int-tag-managers",
                    ModuleId = "integrations",
                    Title = "Tag Manager Integrations",
                    Summary = "Integrate with Google Tag Manager and other tag management systems.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand tag manager limitations with Optimizely",
                        "Use GTM for event tracking (not snippet loading)",
                        "Configure data layer integration",
                        "Set up custom triggers based on Optimizely events"
                    },
                    Content = @"
<h2>Tag Manager Integration</h2>
<p>While tag managers like GTM are powerful, there are <strong>important limitations</strong> when using them with Optimizely Web Experimentation.</p>

<h3>Critical Warning: Don't Load Snippet via Tag Manager</h3>
<p>The Optimizely snippet should NOT be loaded through Google Tag Manager:</p>
<ul>
    <li>Tag managers load scripts <strong>asynchronously</strong></li>
    <li>This causes <strong>page flashing</strong> - visitors see original content first</li>
    <li>Experiment activation is delayed and unreliable</li>
    <li>Always place snippet directly in <code>&lt;head&gt;</code></li>
</ul>

<h3>What You CAN Do with Tag Managers</h3>
<ul>
    <li>Fire tags based on Optimizely events</li>
    <li>Send experiment data to other platforms</li>
    <li>Track conversions through GTM</li>
    <li>Populate data layer with variation info</li>
</ul>

<h3>Data Layer Integration</h3>
<p>Push experiment data to the data layer for GTM:</p>
<pre><code>window.dataLayer = window.dataLayer || [];
window.dataLayer.push({
    event: 'optimizelyExperiment',
    experimentId: '12345',
    variationId: '67890',
    experimentName: 'Homepage Test'
});</code></pre>

<h3>GTM Trigger Setup</h3>
<p>Create a Custom Event trigger in GTM:</p>
<ol>
    <li>Trigger Type: Custom Event</li>
    <li>Event name: <code>optimizelyExperiment</code></li>
    <li>This trigger fires when Optimizely activates</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "int-gtm-example",
                            Title = "GTM Data Layer Integration",
                            Description = "Push Optimizely data to GTM data layer.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Add to Project JavaScript or page code
// This runs when experiments activate

window.optimizely = window.optimizely || [];
window.optimizely.push({
    type: 'addListener',
    filter: {
        type: 'lifecycle',
        name: 'activated'
    },
    handler: function(event) {
        // Initialize data layer
        window.dataLayer = window.dataLayer || [];

        // Push experiment data
        window.dataLayer.push({
            event: 'optimizely_activated',
            optimizely: {
                experimentId: event.data.campaign.id,
                experimentName: event.data.campaign.name,
                variationId: event.data.variation.id,
                variationName: event.data.variation.name
            }
        });

        console.log('Pushed to dataLayer:', event.data.campaign.name);
    }
});

// GTM Trigger Configuration:
// Trigger Type: Custom Event
// Event name: optimizely_activated
// This trigger: All Custom Events (or add conditions)

// GTM Variable Configuration:
// Variable Type: Data Layer Variable
// Data Layer Variable Name: optimizely.experimentName
// (repeat for experimentId, variationId, variationName)",
                            SampleResponse = @"GTM Integration Architecture:

1. Optimizely snippet loads (in <head>)
2. Experiment activates
3. Lifecycle listener fires
4. Data pushed to dataLayer
5. GTM trigger activates
6. GTM tags fire (analytics, pixels, etc.)

This allows you to:
- Fire conversion pixels per variation
- Send data to multiple analytics tools
- Trigger personalization in other tools
- Record experiment data in your CDP",
                            Hints = new List<string>
                            {
                                "Test the data layer in GTM Preview mode",
                                "Never load the Optimizely snippet through GTM"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "int-heatmaps",
                    ModuleId = "integrations",
                    Title = "Heatmap and Session Recording Tools",
                    Summary = "Integrate with Hotjar, FullStory, and session recording platforms.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Send variation data to session recording tools",
                        "Filter recordings by experiment variation",
                        "Correlate heatmaps with experiment results",
                        "Debug experiments using session replays"
                    },
                    Content = @"
<h2>Heatmap and Session Recording Integration</h2>
<p>Session recording tools like Hotjar and FullStory provide <strong>qualitative insights</strong> that complement Optimizely's quantitative data.</p>

<h3>Why Integrate?</h3>
<ul>
    <li><strong>Understand the ""why""</strong> - See how users interact with variations</li>
    <li><strong>Debug issues</strong> - Watch sessions where things went wrong</li>
    <li><strong>Validate results</strong> - Confirm behavior matches metrics</li>
    <li><strong>Generate hypotheses</strong> - Spot opportunities for new tests</li>
</ul>

<h3>Integration Approach</h3>
<p>Tag sessions with experiment and variation data:</p>
<ol>
    <li>Listen for Optimizely activation</li>
    <li>Send experiment/variation as user attributes</li>
    <li>Filter sessions by these attributes in the tool</li>
</ol>

<h3>Hotjar Integration</h3>
<pre><code>hj('identify', visitorId, {
    'optimizely_experiment': experimentName,
    'optimizely_variation': variationName
});</code></pre>

<h3>FullStory Integration</h3>
<pre><code>FS.setUserVars({
    'optimizelyExperiment_str': experimentName,
    'optimizelyVariation_str': variationName
});</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "int-heatmap-example",
                            Title = "Hotjar and FullStory Integration",
                            Description = "Tag sessions with experiment data.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Universal integration for heatmap/recording tools
window.optimizely = window.optimizely || [];
window.optimizely.push({
    type: 'addListener',
    filter: {
        type: 'lifecycle',
        name: 'activated'
    },
    handler: function(event) {
        var expName = event.data.campaign.name;
        var varName = event.data.variation.name;
        var expId = event.data.campaign.id;
        var varId = event.data.variation.id;

        // Hotjar Integration
        if (typeof hj !== 'undefined') {
            hj('identify', null, {
                'experiment': expName,
                'variation': varName,
                'experiment_id': expId,
                'variation_id': varId
            });

            // Optionally trigger a Hotjar event
            hj('event', 'optimizely_' + expId);
        }

        // FullStory Integration
        if (typeof FS !== 'undefined') {
            FS.setUserVars({
                'optimizelyExperiment_str': expName,
                'optimizelyVariation_str': varName,
                'optimizelyExperimentId_str': expId,
                'optimizelyVariationId_str': varId
            });
        }

        // Clarity Integration (Microsoft)
        if (typeof clarity !== 'undefined') {
            clarity('set', 'experiment', expName);
            clarity('set', 'variation', varName);
        }

        // Lucky Orange Integration
        if (typeof __lo_site_id !== 'undefined' && window._loq) {
            window._loq.push(['custom', {
                'experiment': expName,
                'variation': varName
            }]);
        }
    }
});",
                            SampleResponse = @"Use cases for session analysis:

1. Winning variation analysis
   - Watch sessions from the winner
   - Understand WHY it performed better
   - Document learnings for future tests

2. Losing variation analysis
   - Identify friction points
   - See where users struggled
   - Improve next iteration

3. Inconclusive result analysis
   - Look for behavioral differences
   - Spot technical issues
   - Find segment-specific patterns

4. QA and debugging
   - Verify variations display correctly
   - Check for JavaScript errors
   - Confirm events fire properly",
                            Hints = new List<string>
                            {
                                "Filter by variation in your recording tool to compare behavior",
                                "Look at sessions with conversions vs without to understand drop-off"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "int-crm-cdp",
                    ModuleId = "integrations",
                    Title = "CRM and CDP Integrations",
                    Summary = "Connect experiments with customer data platforms and CRM systems.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Send experiment data to CDPs like Segment",
                        "Use CRM data for audience targeting",
                        "Sync experiment exposure to customer profiles",
                        "Build personalization based on experiment history"
                    },
                    Content = @"
<h2>CRM and CDP Integration</h2>
<p>Integrating with Customer Data Platforms (CDPs) and CRM systems enables <strong>advanced personalization</strong> and deeper customer understanding.</p>

<h3>Why Integrate with CDPs?</h3>
<ul>
    <li><strong>Unified customer view</strong> - Experiment data alongside all other touchpoints</li>
    <li><strong>Long-term tracking</strong> - Measure impact beyond experiment duration</li>
    <li><strong>Cross-channel personalization</strong> - Use experiment learnings everywhere</li>
    <li><strong>Advanced targeting</strong> - Use CDP segments in Optimizely</li>
</ul>

<h3>Integration Patterns</h3>

<h4>Pattern 1: Send Experiment Data to CDP</h4>
<p>Track experiment exposure as an event or trait:</p>
<pre><code>analytics.track('Experiment Viewed', {
    experimentId: '12345',
    variationId: '67890'
});</code></pre>

<h4>Pattern 2: Use CDP Data for Targeting</h4>
<p>Pull customer attributes from CDP for audience conditions:</p>
<pre><code>// Custom JS condition using CDP data
function() {
    return window.customerData?.segment === 'high_value';
}</code></pre>

<h4>Pattern 3: Enrich Customer Profiles</h4>
<p>Add experiment history to customer profiles for future personalization.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "int-cdp-example",
                            Title = "Segment.com Integration",
                            Description = "Send experiment data to Segment CDP.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Segment.com integration for Optimizely Web Experimentation

window.optimizely = window.optimizely || [];
window.optimizely.push({
    type: 'addListener',
    filter: {
        type: 'lifecycle',
        name: 'activated'
    },
    handler: function(event) {
        // Check if Segment analytics is available
        if (typeof analytics === 'undefined') return;

        var campaign = event.data.campaign;
        var variation = event.data.variation;

        // Track experiment exposure event
        analytics.track('Experiment Viewed', {
            experiment_id: campaign.id,
            experiment_name: campaign.name,
            variation_id: variation.id,
            variation_name: variation.name,
            experiment_type: 'web'
        });

        // Also set as user trait for long-term tracking
        analytics.identify({
            ['optimizely_' + campaign.id]: variation.name
        });
    }
});

// Using CDP data for Optimizely targeting
// In custom audience condition:
function() {
    // Check Segment traits (assuming they're exposed)
    var traits = window.analytics?.user?.()?.traits?.() || {};

    // Target high-value customers
    return traits.lifetime_value > 1000 ||
           traits.customer_segment === 'enterprise';
}

// Salesforce/HubSpot integration via Segment
// Events flow: Optimizely → Segment → CRM
// This allows sales team to see experiment exposure in CRM",
                            SampleResponse = @"CDP Integration Benefits:

1. Customer 360 View
   - See which experiments each customer saw
   - Correlate with purchases, support tickets
   - Understand full customer journey

2. Advanced Targeting
   - Use CDP segments as Optimizely audiences
   - Target based on purchase history
   - Personalize for customer lifecycle stage

3. Cross-Channel Learning
   - Apply web experiment learnings to email
   - Inform ad targeting with test results
   - Create consistent experiences

4. Long-Term Measurement
   - Track LTV impact of experiments
   - Measure retention effects
   - Calculate true ROI",
                            Hints = new List<string>
                            {
                                "Use consistent experiment naming for easy analysis across tools",
                                "Consider GDPR/privacy when storing experiment data on customer profiles"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "int-data-warehouse",
                    ModuleId = "integrations",
                    Title = "Data Warehouse Export",
                    Summary = "Export experiment data for advanced analysis in your data warehouse.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand Optimizely data export options",
                        "Configure data warehouse connections",
                        "Build custom reports with raw data",
                        "Combine experiment data with business data"
                    },
                    Content = @"
<h2>Data Warehouse Export</h2>
<p>Exporting experiment data to your data warehouse enables <strong>advanced analysis</strong> that goes beyond Optimizely's built-in reporting.</p>

<h3>Enriched Events Export</h3>
<p>Optimizely offers Enriched Events Export (EEE) for enterprise customers:</p>
<ul>
    <li>Raw event-level data</li>
    <li>Export to S3, then to your warehouse</li>
    <li>Includes decisions, conversions, and custom events</li>
</ul>

<h3>Data Warehouse Options</h3>
<ul>
    <li><strong>BigQuery</strong> - Google's serverless warehouse</li>
    <li><strong>Snowflake</strong> - Popular cloud data platform</li>
    <li><strong>Redshift</strong> - AWS data warehouse</li>
    <li><strong>Databricks</strong> - Unified analytics platform</li>
</ul>

<h3>Event Schema</h3>
<p>Key fields in exported data:</p>
<ul>
    <li><code>visitor_id</code> - Unique visitor identifier</li>
    <li><code>experiment_id</code> - Which experiment</li>
    <li><code>variation_id</code> - Which variation seen</li>
    <li><code>event_name</code> - Conversion event</li>
    <li><code>timestamp</code> - When event occurred</li>
    <li><code>revenue</code> - Revenue value (if applicable)</li>
</ul>

<h3>Use Cases for Raw Data</h3>
<ul>
    <li>Custom attribution models</li>
    <li>Join with internal customer data</li>
    <li>Long-term cohort analysis</li>
    <li>Machine learning on experiment data</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "int-warehouse-example",
                            Title = "Data Warehouse Analysis Queries",
                            Description = "SQL queries for analyzing experiment data.",
                            Type = ExampleType.Code,
                            ExampleContent = @"-- Example SQL queries for experiment analysis in your warehouse

-- 1. Basic conversion rate by variation
SELECT
    variation_name,
    COUNT(DISTINCT visitor_id) as visitors,
    COUNT(DISTINCT CASE WHEN converted = 1 THEN visitor_id END) as conversions,
    ROUND(100.0 * COUNT(DISTINCT CASE WHEN converted = 1 THEN visitor_id END)
        / COUNT(DISTINCT visitor_id), 2) as conversion_rate
FROM experiment_events
WHERE experiment_id = '12345678901'
GROUP BY variation_name;

-- 2. Revenue per visitor by variation
SELECT
    variation_name,
    COUNT(DISTINCT visitor_id) as visitors,
    SUM(revenue) / 100.0 as total_revenue,
    ROUND(SUM(revenue) / 100.0 / COUNT(DISTINCT visitor_id), 2) as rpv
FROM experiment_events
WHERE experiment_id = '12345678901'
GROUP BY variation_name;

-- 3. Long-term LTV analysis (join with customer data)
SELECT
    e.variation_name,
    AVG(c.lifetime_value) as avg_ltv,
    AVG(c.orders_count) as avg_orders,
    AVG(c.days_active) as avg_retention
FROM experiment_events e
JOIN customers c ON e.visitor_id = c.optimizely_visitor_id
WHERE e.experiment_id = '12345678901'
    AND e.timestamp < '2024-01-01'  -- Exposure before cutoff
    AND c.first_order_date >= e.timestamp  -- Became customer after
GROUP BY e.variation_name;

-- 4. Segment-specific analysis
SELECT
    e.variation_name,
    c.customer_segment,
    COUNT(DISTINCT e.visitor_id) as visitors,
    COUNT(DISTINCT CASE WHEN e.converted = 1 THEN e.visitor_id END) as conversions
FROM experiment_events e
JOIN customers c ON e.visitor_id = c.optimizely_visitor_id
WHERE e.experiment_id = '12345678901'
GROUP BY e.variation_name, c.customer_segment;",
                            SampleResponse = @"Data warehouse advantages:

1. Custom Analysis
   - Any metric you can calculate
   - Custom attribution windows
   - Proprietary metrics (LTV, churn risk)

2. Data Joining
   - Connect to CRM data
   - Join with product usage
   - Link to support interactions

3. Historical Analysis
   - Keep data beyond Optimizely retention
   - Analyze long-term impact
   - Build ML models on results

4. Compliance
   - Store data in your infrastructure
   - Apply your data governance
   - Meet regulatory requirements",
                            Hints = new List<string>
                            {
                                "Enriched Events Export requires an enterprise Optimizely plan",
                                "Consider using Segment or similar to get data into your warehouse"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 10: REST API

    private LearningModule BuildRestAPIModule()
    {
        return new LearningModule
        {
            Id = "rest-api",
            Title = "REST API",
            Description = "Use the Optimizely REST API for programmatic management.",
            Icon = "server",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "rest-introduction",
                    ModuleId = "rest-api",
                    Title = "REST API Overview",
                    Summary = "Introduction to the Optimizely REST API.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand REST API capabilities",
                        "Learn authentication methods",
                        "Know rate limits and best practices"
                    },
                    Content = @"
<h2>Optimizely REST API</h2>
<p>The REST API lets you <strong>programmatically manage</strong> your Optimizely projects, experiments, and data.</p>

<h3>Capabilities</h3>
<ul>
    <li>Create and manage experiments</li>
    <li>Configure audiences and events</li>
    <li>Retrieve experiment results</li>
    <li>Manage project settings</li>
</ul>

<h3>Authentication</h3>
<p>The API uses OAuth 2.0 or Personal Access Tokens:</p>
<pre><code>Authorization: Bearer YOUR_ACCESS_TOKEN</code></pre>

<h3>Rate Limits</h3>
<ul>
    <li>General endpoints: 100 requests/minute</li>
    <li>Results endpoints: 20 requests/minute</li>
</ul>

<h3>Use Cases</h3>
<ul>
    <li>Automate experiment creation from CI/CD</li>
    <li>Build custom dashboards</li>
    <li>Integrate with internal tools</li>
    <li>Bulk operations on experiments</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "rest-example",
                            Title = "REST API Examples",
                            Description = "Common API operations.",
                            Type = ExampleType.Code,
                            ExampleContent = @"# List all experiments in a project
curl -X GET \
  'https://api.optimizely.com/v2/experiments?project_id=12345' \
  -H 'Authorization: Bearer YOUR_TOKEN'

# Get experiment results
curl -X GET \
  'https://api.optimizely.com/v2/experiments/67890/results' \
  -H 'Authorization: Bearer YOUR_TOKEN'

# Create a new experiment
curl -X POST \
  'https://api.optimizely.com/v2/experiments' \
  -H 'Authorization: Bearer YOUR_TOKEN' \
  -H 'Content-Type: application/json' \
  -d '{
    ""project_id"": 12345,
    ""name"": ""New Homepage Test"",
    ""type"": ""a/b"",
    ""status"": ""not_started""
  }'

# Update experiment status
curl -X PATCH \
  'https://api.optimizely.com/v2/experiments/67890' \
  -H 'Authorization: Bearer YOUR_TOKEN' \
  -H 'Content-Type: application/json' \
  -d '{
    ""status"": ""running""
  }'",
                            SampleResponse = @"API response example:

{
  ""id"": 67890,
  ""name"": ""New Homepage Test"",
  ""type"": ""a/b"",
  ""status"": ""running"",
  ""created"": ""2024-01-15T10:30:00Z"",
  ""variations"": [
    {
      ""id"": 11111,
      ""name"": ""Original"",
      ""weight"": 5000
    },
    {
      ""id"": 22222,
      ""name"": ""Variation 1"",
      ""weight"": 5000
    }
  ]
}",
                            Hints = new List<string>
                            {
                                "Use Personal Access Tokens for scripts and automation",
                                "The API uses pagination - check for next_page in responses"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "rest-auth",
                    ModuleId = "rest-api",
                    Title = "Authentication and Authorization",
                    Summary = "Securely authenticate with the Optimizely REST API.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Create Personal Access Tokens",
                        "Understand OAuth 2.0 flow",
                        "Manage API credentials securely",
                        "Handle token refresh and expiration"
                    },
                    Content = @"
<h2>API Authentication</h2>
<p>The Optimizely REST API supports two authentication methods: <strong>Personal Access Tokens (PATs)</strong> and <strong>OAuth 2.0</strong>.</p>

<h3>Personal Access Tokens (Recommended for Scripts)</h3>
<p>PATs are the simplest way to authenticate:</p>
<ol>
    <li>Go to Profile → API Access in Optimizely</li>
    <li>Click ""Generate New Token""</li>
    <li>Give it a descriptive name</li>
    <li>Copy and securely store the token</li>
</ol>

<h3>Using Your Token</h3>
<pre><code>curl -H 'Authorization: Bearer YOUR_TOKEN' \
     https://api.optimizely.com/v2/projects</code></pre>

<h3>OAuth 2.0 (For Applications)</h3>
<p>Use OAuth when building applications for multiple users:</p>
<ol>
    <li>Register your application in Optimizely</li>
    <li>Redirect users to authorization URL</li>
    <li>Exchange authorization code for access token</li>
    <li>Refresh tokens before expiration</li>
</ol>

<h3>Token Security Best Practices</h3>
<ul>
    <li><strong>Never commit tokens</strong> to version control</li>
    <li>Use environment variables or secret managers</li>
    <li>Rotate tokens periodically</li>
    <li>Use minimum required permissions</li>
    <li>Revoke unused tokens</li>
</ul>

<h3>Token Scopes</h3>
<p>Tokens can have different permission levels:</p>
<ul>
    <li><strong>Read</strong> - View experiments and results</li>
    <li><strong>Write</strong> - Create and modify experiments</li>
    <li><strong>Admin</strong> - Full project access</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "rest-auth-example",
                            Title = "Authentication Examples",
                            Description = "Secure token management patterns.",
                            Type = ExampleType.Code,
                            ExampleContent = @"# Environment variable approach (recommended)
export OPTIMIZELY_TOKEN='your-token-here'

# In your script
curl -H ""Authorization: Bearer $OPTIMIZELY_TOKEN"" \
     https://api.optimizely.com/v2/projects

# Python example with environment variable
import os
import requests

token = os.environ.get('OPTIMIZELY_TOKEN')
headers = {'Authorization': f'Bearer {token}'}

response = requests.get(
    'https://api.optimizely.com/v2/projects',
    headers=headers
)

# Node.js example
const axios = require('axios');

const client = axios.create({
    baseURL: 'https://api.optimizely.com/v2',
    headers: {
        'Authorization': `Bearer ${process.env.OPTIMIZELY_TOKEN}`
    }
});

// OAuth 2.0 Authorization URL
const authUrl = 'https://app.optimizely.com/oauth2/authorize?' +
    'client_id=YOUR_CLIENT_ID&' +
    'redirect_uri=YOUR_REDIRECT_URI&' +
    'response_type=code&' +
    'scopes=all';

// Exchange code for token
const tokenResponse = await fetch('https://app.optimizely.com/oauth2/token', {
    method: 'POST',
    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    body: new URLSearchParams({
        grant_type: 'authorization_code',
        code: authorizationCode,
        client_id: 'YOUR_CLIENT_ID',
        client_secret: 'YOUR_CLIENT_SECRET'
    })
});",
                            SampleResponse = @"Security checklist:

✓ Token stored in environment variable
✓ Not committed to git (.env in .gitignore)
✓ Using secrets manager in production
✓ Token has minimum required permissions
✓ Token rotation scheduled
✓ Unused tokens revoked

For CI/CD:
- Use GitHub Secrets or similar
- Never echo tokens in logs
- Mask sensitive values in output",
                            Hints = new List<string>
                            {
                                "Personal Access Tokens don't expire but should be rotated regularly",
                                "OAuth tokens expire and need refresh - handle this in your code"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "rest-experiments",
                    ModuleId = "rest-api",
                    Title = "Managing Experiments via API",
                    Summary = "Create, update, and control experiments programmatically.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create experiments via API",
                        "Update experiment configuration",
                        "Start, pause, and archive experiments",
                        "Manage variations programmatically"
                    },
                    Content = @"
<h2>Managing Experiments via API</h2>
<p>The REST API enables <strong>full lifecycle management</strong> of experiments, from creation through archival.</p>

<h3>Experiment Lifecycle</h3>
<ol>
    <li><strong>not_started</strong> - Created but not running</li>
    <li><strong>running</strong> - Actively collecting data</li>
    <li><strong>paused</strong> - Temporarily stopped</li>
    <li><strong>archived</strong> - Completed and stored</li>
</ol>

<h3>Creating an Experiment</h3>
<pre><code>POST /v2/experiments
{
    ""project_id"": 12345,
    ""name"": ""Homepage Hero Test"",
    ""type"": ""a/b"",
    ""holdback"": 0,
    ""metrics"": [{""event_id"": 67890}]
}</code></pre>

<h3>Adding Variations</h3>
<pre><code>POST /v2/variations
{
    ""experiment_id"": 12345,
    ""name"": ""Variation 1"",
    ""weight"": 5000,
    ""actions"": [...]
}</code></pre>

<h3>Controlling Experiment Status</h3>
<pre><code>PATCH /v2/experiments/{id}
{
    ""status"": ""running""
}</code></pre>

<h3>Common Operations</h3>
<ul>
    <li><strong>List experiments</strong> - GET /v2/experiments?project_id=X</li>
    <li><strong>Get experiment details</strong> - GET /v2/experiments/{id}</li>
    <li><strong>Update traffic allocation</strong> - PATCH with weight changes</li>
    <li><strong>Archive experiment</strong> - PATCH status to ""archived""</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "rest-exp-example",
                            Title = "Experiment Management Workflow",
                            Description = "Complete workflow for creating and managing an experiment.",
                            Type = ExampleType.Code,
                            ExampleContent = @"#!/bin/bash
# Complete experiment management workflow

API_BASE=""https://api.optimizely.com/v2""
TOKEN=""$OPTIMIZELY_TOKEN""
PROJECT_ID=""12345678901""

# 1. Create the experiment
EXPERIMENT=$(curl -s -X POST ""$API_BASE/experiments"" \
    -H ""Authorization: Bearer $TOKEN"" \
    -H ""Content-Type: application/json"" \
    -d '{
        ""project_id"": '$PROJECT_ID',
        ""name"": ""API Created Test"",
        ""type"": ""a/b"",
        ""status"": ""not_started"",
        ""holdback"": 0
    }')

EXPERIMENT_ID=$(echo $EXPERIMENT | jq -r '.id')
echo ""Created experiment: $EXPERIMENT_ID""

# 2. Add a variation
curl -s -X POST ""$API_BASE/variations"" \
    -H ""Authorization: Bearer $TOKEN"" \
    -H ""Content-Type: application/json"" \
    -d '{
        ""experiment_id"": '$EXPERIMENT_ID',
        ""name"": ""New Hero Image"",
        ""weight"": 5000
    }'

# 3. Add metrics (events to track)
curl -s -X PATCH ""$API_BASE/experiments/$EXPERIMENT_ID"" \
    -H ""Authorization: Bearer $TOKEN"" \
    -H ""Content-Type: application/json"" \
    -d '{
        ""metrics"": [
            {""event_id"": 98765, ""aggregator"": ""unique""}
        ]
    }'

# 4. Start the experiment
curl -s -X PATCH ""$API_BASE/experiments/$EXPERIMENT_ID"" \
    -H ""Authorization: Bearer $TOKEN"" \
    -H ""Content-Type: application/json"" \
    -d '{""status"": ""running""}'

echo ""Experiment $EXPERIMENT_ID is now running""

# 5. Check status
curl -s ""$API_BASE/experiments/$EXPERIMENT_ID"" \
    -H ""Authorization: Bearer $TOKEN"" | jq '.status'

# 6. Pause if needed
# curl -s -X PATCH ""$API_BASE/experiments/$EXPERIMENT_ID"" \
#     -H ""Authorization: Bearer $TOKEN"" \
#     -d '{""status"": ""paused""}'",
                            SampleResponse = @"API Response Examples:

Create Experiment Response:
{
    ""id"": 12345678901,
    ""name"": ""API Created Test"",
    ""status"": ""not_started"",
    ""created"": ""2024-01-15T10:30:00Z"",
    ""project_id"": 12345
}

Common Status Codes:
- 200: Success
- 201: Created
- 400: Bad request (validation error)
- 401: Unauthorized (token issue)
- 404: Not found
- 429: Rate limited",
                            Hints = new List<string>
                            {
                                "Always check response status codes and handle errors",
                                "Use jq or similar to parse JSON responses in scripts"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "rest-results",
                    ModuleId = "rest-api",
                    Title = "Retrieving Results via API",
                    Summary = "Pull experiment results for custom dashboards and reporting.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Query experiment results",
                        "Understand result data structure",
                        "Build custom reporting dashboards",
                        "Automate result notifications"
                    },
                    Content = @"
<h2>Retrieving Experiment Results</h2>
<p>The Results API provides <strong>programmatic access</strong> to experiment performance data for custom dashboards and automated reporting.</p>

<h3>Results Endpoint</h3>
<pre><code>GET /v2/experiments/{id}/results</code></pre>

<h3>Result Data Structure</h3>
<p>Results include:</p>
<ul>
    <li><strong>metrics</strong> - Performance data per metric</li>
    <li><strong>variations</strong> - Data per variation</li>
    <li><strong>reach</strong> - Visitor counts</li>
    <li><strong>confidence_interval</strong> - Statistical bounds</li>
    <li><strong>is_significant</strong> - Whether result is conclusive</li>
</ul>

<h3>Rate Limiting</h3>
<p>Results endpoints have stricter limits:</p>
<ul>
    <li>20 requests per minute</li>
    <li>Cache results to avoid hitting limits</li>
    <li>Use webhooks for real-time updates</li>
</ul>

<h3>Time-Series Data</h3>
<p>Get results over time with the timeseries endpoint:</p>
<pre><code>GET /v2/experiments/{id}/timeseries?metric_id=X</code></pre>

<h3>Use Cases</h3>
<ul>
    <li>Custom executive dashboards</li>
    <li>Slack/email notifications on significance</li>
    <li>Automated experiment stopping</li>
    <li>Data warehouse integration</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "rest-results-example",
                            Title = "Results Dashboard Integration",
                            Description = "Fetch and process experiment results.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Node.js: Fetch results and send Slack notification

const axios = require('axios');

async function checkExperimentResults(experimentId) {
    const response = await axios.get(
        `https://api.optimizely.com/v2/experiments/${experimentId}/results`,
        { headers: { 'Authorization': `Bearer ${process.env.OPTIMIZELY_TOKEN}` }}
    );

    const results = response.data;

    // Process each metric
    for (const metric of results.metrics) {
        for (const variation of metric.results) {
            if (variation.is_baseline) continue;

            const data = {
                metricName: metric.name,
                variationName: variation.name,
                improvement: variation.lift?.value,
                significance: variation.statistical_significance,
                isSignificant: variation.is_significant
            };

            console.log(`${data.variationName}: ${data.improvement}% lift, ${data.significance}% significance`);

            // Send Slack notification if significant
            if (data.isSignificant && data.significance >= 0.95) {
                await sendSlackNotification(experimentId, data);
            }
        }
    }
}

async function sendSlackNotification(experimentId, data) {
    const emoji = data.improvement > 0 ? '🎉' : '⚠️';
    const message = {
        text: `${emoji} Experiment ${experimentId} reached significance!`,
        blocks: [
            {
                type: 'section',
                text: {
                    type: 'mrkdwn',
                    text: `*${data.variationName}* for *${data.metricName}*\n` +
                          `Improvement: ${data.improvement?.toFixed(1)}%\n` +
                          `Significance: ${(data.significance * 100).toFixed(0)}%`
                }
            }
        ]
    };

    await axios.post(process.env.SLACK_WEBHOOK_URL, message);
}

// Run check every hour
setInterval(() => {
    checkExperimentResults('12345678901');
}, 60 * 60 * 1000);",
                            SampleResponse = @"Results API Response Structure:

{
    ""metrics"": [
        {
            ""name"": ""Purchases"",
            ""results"": [
                {
                    ""name"": ""Original"",
                    ""is_baseline"": true,
                    ""value"": 0.032,
                    ""samples"": 15000
                },
                {
                    ""name"": ""Variation 1"",
                    ""is_baseline"": false,
                    ""value"": 0.038,
                    ""samples"": 15200,
                    ""lift"": {
                        ""value"": 0.187,
                        ""confidence_interval"": [0.08, 0.29]
                    },
                    ""statistical_significance"": 0.96,
                    ""is_significant"": true
                }
            ]
        }
    ],
    ""start_time"": ""2024-01-01T00:00:00Z"",
    ""end_time"": ""2024-01-15T00:00:00Z""
}",
                            Hints = new List<string>
                            {
                                "Cache results to avoid rate limits - data doesn't change frequently",
                                "Use the reach field to check if you have enough visitors"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "rest-automation",
                    ModuleId = "rest-api",
                    Title = "Automation and CI/CD Integration",
                    Summary = "Automate experiment management in your development workflow.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Integrate with CI/CD pipelines",
                        "Automate experiment creation from feature flags",
                        "Build deployment-triggered experiments",
                        "Version control experiment configuration"
                    },
                    Content = @"
<h2>CI/CD Integration</h2>
<p>Integrating Optimizely with your CI/CD pipeline enables <strong>experiment-as-code</strong> workflows and deployment-triggered experiments.</p>

<h3>Experiment-as-Code Benefits</h3>
<ul>
    <li>Version control experiment configuration</li>
    <li>Code review for experiment changes</li>
    <li>Automated deployment of experiments</li>
    <li>Reproducible experiment setup</li>
</ul>

<h3>Common Automation Patterns</h3>

<h4>1. Feature Flag → Experiment</h4>
<p>Automatically create experiments when new features are deployed:</p>
<ul>
    <li>Deploy feature behind flag</li>
    <li>CI/CD creates Optimizely experiment</li>
    <li>Experiment controls flag exposure</li>
</ul>

<h4>2. Deployment Validation</h4>
<p>Run experiments on deployments to validate changes:</p>
<ul>
    <li>Deploy to 10% of traffic</li>
    <li>Monitor key metrics</li>
    <li>Auto-rollback if metrics drop</li>
</ul>

<h4>3. Configuration Sync</h4>
<p>Keep experiment config in code and sync to Optimizely:</p>
<ul>
    <li>YAML/JSON config files</li>
    <li>CI job syncs to Optimizely API</li>
    <li>Changes tracked in git history</li>
</ul>

<h3>GitHub Actions Integration</h3>
<p>Use GitHub Actions to automate experiment workflows in your repository.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "rest-cicd-example",
                            Title = "GitHub Actions Workflow",
                            Description = "Automate experiments with GitHub Actions.",
                            Type = ExampleType.Code,
                            ExampleContent = @"# .github/workflows/optimizely-experiment.yml
name: Deploy and Create Experiment

on:
  push:
    branches: [main]
    paths:
      - 'experiments/**'

jobs:
  deploy-experiment:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Setup Node.js
        uses: actions/setup-node@v3
        with:
          node-version: '18'

      - name: Install dependencies
        run: npm install axios

      - name: Create/Update Experiment
        env:
          OPTIMIZELY_TOKEN: ${{ secrets.OPTIMIZELY_TOKEN }}
          PROJECT_ID: ${{ secrets.OPTIMIZELY_PROJECT_ID }}
        run: |
          node scripts/sync-experiment.js experiments/homepage-test.json

# scripts/sync-experiment.js
const axios = require('axios');
const fs = require('fs');

const config = JSON.parse(fs.readFileSync(process.argv[2]));

async function syncExperiment(config) {
    const api = axios.create({
        baseURL: 'https://api.optimizely.com/v2',
        headers: { 'Authorization': `Bearer ${process.env.OPTIMIZELY_TOKEN}` }
    });

    // Check if experiment exists
    let experiment;
    if (config.id) {
        try {
            const response = await api.get(`/experiments/${config.id}`);
            experiment = response.data;
            console.log(`Updating existing experiment: ${config.id}`);
        } catch (e) {
            if (e.response?.status !== 404) throw e;
        }
    }

    if (experiment) {
        // Update existing
        await api.patch(`/experiments/${config.id}`, {
            name: config.name,
            description: config.description
        });
    } else {
        // Create new
        const response = await api.post('/experiments', {
            project_id: process.env.PROJECT_ID,
            name: config.name,
            type: 'a/b',
            status: 'not_started'
        });
        console.log(`Created experiment: ${response.data.id}`);
    }
}

syncExperiment(config);

# experiments/homepage-test.json
{
    ""name"": ""Homepage Hero Test - Q1 2024"",
    ""description"": ""Testing new hero image and CTA"",
    ""metrics"": [""purchase"", ""add_to_cart""],
    ""traffic_allocation"": 100,
    ""variations"": [
        {""name"": ""Control"", ""weight"": 50},
        {""name"": ""New Hero"", ""weight"": 50}
    ]
}",
                            SampleResponse = @"CI/CD Best Practices:

1. Secrets Management
   - Store tokens in GitHub Secrets
   - Never log sensitive values
   - Use OIDC for keyless auth where possible

2. Environment Strategy
   - Staging project for testing
   - Production project for live experiments
   - Sync config between environments

3. Rollback Strategy
   - Keep previous config versions
   - Ability to quickly pause experiments
   - Automated rollback on metric drops

4. Validation
   - Validate JSON config before sync
   - Check experiment exists before update
   - Verify API responses",
                            Hints = new List<string>
                            {
                                "Use GitHub Secrets to store your Optimizely token",
                                "Consider separate staging and production Optimizely projects"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 11: Troubleshooting and Debugging

    private LearningModule BuildTroubleshootingModule()
    {
        return new LearningModule
        {
            Id = "troubleshooting",
            Title = "Troubleshooting and Debugging",
            Description = "Diagnose and resolve common Web Experimentation issues.",
            Icon = "bug-ant",
            Order = 11,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ts-implementation",
                    ModuleId = "troubleshooting",
                    Title = "Common Implementation Issues",
                    Summary = "Diagnose and fix snippet and implementation problems.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Identify snippet implementation issues",
                        "Debug flashing and timing problems",
                        "Resolve conflicts with other scripts",
                        "Fix caching issues"
                    },
                    Content = @"
<h2>Common Implementation Issues</h2>
<p>Implementation problems are the <strong>most common cause</strong> of experiment failures. Learning to diagnose them quickly saves time and frustration.</p>

<h3>Issue 1: Snippet Not Loading</h3>
<p>Symptoms: <code>window.optimizely</code> is undefined</p>
<ul>
    <li><strong>Check</strong>: Is snippet in the HTML?</li>
    <li><strong>Check</strong>: Network tab shows 200 status?</li>
    <li><strong>Check</strong>: Any JavaScript errors before snippet?</li>
    <li><strong>Check</strong>: Content Security Policy blocking?</li>
</ul>

<h3>Issue 2: Page Flashing (FOOC)</h3>
<p>Symptoms: Original content briefly visible before variation</p>
<ul>
    <li><strong>Cause</strong>: Snippet placed too low in HTML</li>
    <li><strong>Cause</strong>: Snippet loaded via tag manager</li>
    <li><strong>Cause</strong>: Heavy variation code execution</li>
    <li><strong>Fix</strong>: Move snippet higher in &lt;head&gt;</li>
</ul>

<h3>Issue 3: Third-Party Script Conflicts</h3>
<p>Symptoms: JavaScript errors, broken functionality</p>
<ul>
    <li>Other scripts modifying same elements</li>
    <li>Variable name collisions</li>
    <li>Load order dependencies</li>
</ul>

<h3>Issue 4: Caching Problems</h3>
<p>Symptoms: Old variations showing, changes not appearing</p>
<ul>
    <li>CDN caching old snippet version</li>
    <li>Browser caching pages</li>
    <li>Service worker serving stale content</li>
</ul>

<h3>Quick Diagnostic Checklist</h3>
<ol>
    <li>Open DevTools Console - any errors?</li>
    <li>Type <code>window.optimizely</code> - defined?</li>
    <li>Check Network tab for snippet load</li>
    <li>Try incognito/private mode</li>
    <li>Clear cache and hard refresh</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ts-impl-example",
                            Title = "Implementation Diagnostic Script",
                            Description = "Quick diagnostic commands for implementation issues.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Paste in browser console to diagnose implementation issues

console.log('=== Optimizely Implementation Diagnostic ===\n');

// 1. Check if Optimizely is loaded
if (typeof window.optimizely === 'undefined') {
    console.error('❌ Optimizely is NOT loaded');
    console.log('Possible causes:');
    console.log('  - Snippet not in HTML');
    console.log('  - Snippet blocked by CSP');
    console.log('  - JavaScript error before snippet');
} else {
    console.log('✅ Optimizely is loaded');

    // 2. Get snippet info
    var data = window.optimizely.get('data');
    console.log('Project ID:', data.projectId);
    console.log('Revision:', data.revision);

    // 3. Check active experiments
    var state = window.optimizely.get('state');
    var activeExps = state.getActiveExperimentIds();
    console.log('Active experiments:', activeExps.length);
    activeExps.forEach(function(id) {
        var exp = data.experiments[id];
        console.log('  -', exp?.name || id);
    });

    // 4. Check for errors
    var errors = window.optimizely.get('errors');
    if (errors && errors.length > 0) {
        console.warn('⚠️ Optimizely errors:', errors);
    }

    // 5. Check variation map
    var variationMap = state.getVariationMap();
    console.log('Variation assignments:', variationMap);
}

// 6. Check snippet placement
var scripts = document.querySelectorAll('script[src*=""optimizely""]');
if (scripts.length === 0) {
    console.error('❌ Optimizely snippet tag not found in DOM');
} else {
    scripts.forEach(function(s, i) {
        var inHead = s.parentElement.tagName === 'HEAD';
        console.log('Snippet', i + 1, ':', inHead ? '✅ in HEAD' : '⚠️ NOT in HEAD');
    });
}",
                            SampleResponse = @"Expected healthy output:

=== Optimizely Implementation Diagnostic ===

✅ Optimizely is loaded
Project ID: 12345678901
Revision: 156
Active experiments: 2
  - Homepage Hero Test
  - Checkout Button Color
Variation assignments: {12345: 67890, 23456: 78901}
Snippet 1 : ✅ in HEAD

If you see errors, follow the diagnostic
suggestions to identify the root cause.",
                            Hints = new List<string>
                            {
                                "Run diagnostics in incognito mode to rule out extensions",
                                "Check for multiple snippet instances which can cause conflicts"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "ts-activation",
                    ModuleId = "troubleshooting",
                    Title = "Experiment Activation Issues",
                    Summary = "Troubleshoot experiments that won't activate or apply.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Debug URL targeting mismatches",
                        "Identify audience condition failures",
                        "Resolve timing and load order issues",
                        "Handle SPA activation problems"
                    },
                    Content = @"
<h2>Experiment Activation Issues</h2>
<p>When experiments don't activate, visitors don't see your variations. Understanding <strong>why experiments fail to activate</strong> is crucial for debugging.</p>

<h3>Activation Requirements</h3>
<p>For an experiment to activate, ALL of these must be true:</p>
<ol>
    <li>Experiment status is ""Running""</li>
    <li>URL matches page targeting</li>
    <li>Visitor meets audience conditions</li>
    <li>Page/experiment not already activated</li>
    <li>Traffic allocation includes this visitor</li>
</ol>

<h3>Common Activation Failures</h3>

<h4>URL Targeting Mismatch</h4>
<ul>
    <li>Protocol mismatch (http vs https)</li>
    <li>www vs non-www</li>
    <li>Query parameters affecting match</li>
    <li>Trailing slashes</li>
    <li>Case sensitivity</li>
</ul>

<h4>Audience Condition Failures</h4>
<ul>
    <li>Cookie doesn't exist or has wrong value</li>
    <li>Custom JS condition returns false/undefined</li>
    <li>Geolocation detection failing</li>
    <li>Condition evaluated too early</li>
</ul>

<h4>Timing Issues</h4>
<ul>
    <li>Audience data not available when evaluated</li>
    <li>Elements don't exist when variation applies</li>
    <li>SPA route change not triggering re-evaluation</li>
</ul>

<h3>Debugging Activation</h3>
<pre><code>// Check why experiment isn't active
var state = window.optimizely.get('state');
var expId = '12345678901';

// Get decision info
var decision = state.getDecision({
    experimentId: expId
});
console.log('Decision:', decision);</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ts-activation-example",
                            Title = "Activation Debugging Commands",
                            Description = "Debug why an experiment isn't activating.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Debug experiment activation issues

// 1. Check if experiment exists in project
var data = window.optimizely.get('data');
var experimentId = '12345678901'; // Your experiment ID

if (!data.experiments[experimentId]) {
    console.error('Experiment not found in project');
    console.log('Available experiments:', Object.keys(data.experiments));
} else {
    var exp = data.experiments[experimentId];
    console.log('Experiment found:', exp.name);
    console.log('Status:', exp.status); // Must be 'running'
}

// 2. Check URL targeting
var pages = data.pages;
console.log('Current URL:', window.location.href);
for (var pageId in pages) {
    var page = pages[pageId];
    // Check if this page is associated with your experiment
    console.log('Page:', page.name, '| Conditions:', page.conditions);
}

// 3. Check audience conditions
var state = window.optimizely.get('state');
var audiences = data.audiences;

// Manually test custom JS conditions
// (find your audience's custom JS and run it)
function testCustomCondition() {
    // Paste your custom JS condition here
    // It should return true for the experiment to run
    return true;
}
console.log('Custom condition result:', testCustomCondition());

// 4. Check if visitor was bucketed out
var variationMap = state.getVariationMap();
if (!variationMap[experimentId]) {
    console.log('Visitor not bucketed into this experiment');
    console.log('Possible reasons:');
    console.log('  - Traffic allocation excluded them');
    console.log('  - Audience conditions not met');
    console.log('  - URL targeting not matched');
}

// 5. Force into experiment for testing (Preview only!)
// window.optimizely.push({
//     type: 'bucketVisitor',
//     experimentId: experimentId,
//     variationId: 'your-variation-id'
// });",
                            SampleResponse = @"Activation debugging checklist:

1. Experiment status = running? ✓/✗
2. URL matches targeting? ✓/✗
3. Audience conditions met? ✓/✗
4. Not previously bucketed out? ✓/✗
5. Within traffic allocation? ✓/✗

Common fixes:
- URL: Use substring or regex matching
- Audience: Add fallback for missing data
- Timing: Use manual activation for SPAs
- Traffic: Check holdback settings",
                            Hints = new List<string>
                            {
                                "Use Preview mode to force yourself into experiments for testing",
                                "Check the browser console for Optimizely debug messages"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "ts-visual-editor",
                    ModuleId = "troubleshooting",
                    Title = "Visual Editor Problems",
                    Summary = "Resolve issues with the Visual Editor not loading or changes not applying.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Fix editor loading failures",
                        "Handle iframe blocking issues",
                        "Resolve CSP conflicts",
                        "Debug changes not applying"
                    },
                    Content = @"
<h2>Visual Editor Problems</h2>
<p>The Visual Editor can fail to load or function properly due to various security and technical restrictions on your website.</p>

<h3>Editor Won't Load</h3>

<h4>X-Frame-Options Blocking</h4>
<p>Your site may block being loaded in an iframe:</p>
<pre><code>X-Frame-Options: DENY
X-Frame-Options: SAMEORIGIN</code></pre>
<p><strong>Solution</strong>: Whitelist Optimizely domains or use the browser extension.</p>

<h4>Content Security Policy (CSP)</h4>
<p>CSP can block the editor's scripts:</p>
<pre><code>Content-Security-Policy: frame-ancestors 'self'</code></pre>
<p><strong>Solution</strong>: Add Optimizely domains to frame-ancestors.</p>

<h3>Changes Not Applying</h3>

<h4>Selector Issues</h4>
<ul>
    <li>Element's class or ID changed</li>
    <li>Element is dynamically loaded</li>
    <li>Multiple elements match selector</li>
</ul>

<h4>Timing Issues</h4>
<ul>
    <li>Variation code runs before element exists</li>
    <li>Another script modifies element after variation</li>
    <li>React/Angular re-renders and overwrites changes</li>
</ul>

<h3>Browser Extension Alternative</h3>
<p>When iframe loading fails, use the Optimizely browser extension:</p>
<ol>
    <li>Install extension from Chrome/Firefox store</li>
    <li>Navigate to your page directly</li>
    <li>Activate editor via extension</li>
    <li>Edit without iframe restrictions</li>
</ol>

<h3>Recommended CSP Settings</h3>
<pre><code>frame-ancestors 'self' https://app.optimizely.com;
script-src 'self' https://cdn.optimizely.com;</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ts-ve-example",
                            Title = "CSP and Frame Configuration",
                            Description = "Configure your site to allow the Visual Editor.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Check current security headers
// Run in browser console on your site

// 1. Check X-Frame-Options
fetch(window.location.href, { method: 'HEAD' })
    .then(r => {
        var xfo = r.headers.get('X-Frame-Options');
        console.log('X-Frame-Options:', xfo || 'Not set');
        if (xfo === 'DENY' || xfo === 'SAMEORIGIN') {
            console.warn('⚠️ This will block the Visual Editor');
        }
    });

// 2. Check Content-Security-Policy
var csp = document.querySelector('meta[http-equiv=""Content-Security-Policy""]');
console.log('CSP meta tag:', csp ? csp.content : 'Not found');

// Server configuration examples:

// Nginx - Allow Optimizely Visual Editor
// add_header X-Frame-Options ""ALLOW-FROM https://app.optimizely.com"";
// Note: ALLOW-FROM is deprecated, use CSP instead

// Nginx - CSP configuration
// add_header Content-Security-Policy ""frame-ancestors 'self' https://app.optimizely.com https://*.optimizely.com;"";

// Apache - .htaccess
// Header set Content-Security-Policy ""frame-ancestors 'self' https://app.optimizely.com https://*.optimizely.com;""

// Express.js middleware
app.use((req, res, next) => {
    res.setHeader(
        'Content-Security-Policy',
        ""frame-ancestors 'self' https://app.optimizely.com https://*.optimizely.com;""
    );
    next();
});

// For development/staging only - remove X-Frame-Options
// This allows the Visual Editor to load your site in an iframe",
                            SampleResponse = @"Visual Editor troubleshooting flow:

1. Editor blank/not loading?
   → Check X-Frame-Options header
   → Check CSP frame-ancestors
   → Try browser extension instead

2. Can't select elements?
   → Element may be in iframe
   → Element may be in shadow DOM
   → Try using CSS selector mode

3. Changes don't appear live?
   → Element loads dynamically
   → Use 'wait for element' option
   → Add custom JS to wait for element

4. Changes work in editor but not live?
   → Selector changed between sessions
   → Another script overwriting changes
   → Check variation code timing",
                            Hints = new List<string>
                            {
                                "The Optimizely browser extension bypasses most iframe restrictions",
                                "For SPAs, ensure you're testing on the actual route, not just the home page"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "ts-events",
                    ModuleId = "troubleshooting",
                    Title = "Event Tracking Troubleshooting",
                    Summary = "Debug event and conversion tracking issues.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Verify events are firing correctly",
                        "Debug selector-based click events",
                        "Troubleshoot attribution issues",
                        "Fix revenue tracking problems"
                    },
                    Content = @"
<h2>Event Tracking Troubleshooting</h2>
<p>When conversions aren't tracking, your experiment results will be incomplete or misleading. Proper event debugging is essential.</p>

<h3>Events Not Firing</h3>

<h4>Click Events</h4>
<ul>
    <li>CSS selector doesn't match element</li>
    <li>Element is dynamically loaded</li>
    <li>Click is handled by JavaScript (prevented)</li>
    <li>Event listener attached too late</li>
</ul>

<h4>Custom Events</h4>
<ul>
    <li>Event name typo (must match exactly)</li>
    <li>Code not executing</li>
    <li>Optimizely not loaded when event fires</li>
</ul>

<h3>Events Fire But Not Counted</h3>
<ul>
    <li>Visitor not in experiment when event fires</li>
    <li>Event not associated with experiment</li>
    <li>Attribution window expired</li>
    <li>Event deduplicated (already counted)</li>
</ul>

<h3>Revenue Not Tracking</h3>
<ul>
    <li>Revenue not in cents (multiply by 100)</li>
    <li>Revenue value is string, not number</li>
    <li>Tags object malformed</li>
</ul>

<h3>Debugging Approach</h3>
<ol>
    <li>Verify event is configured in Optimizely</li>
    <li>Check event fires (Network tab)</li>
    <li>Confirm visitor is in experiment</li>
    <li>Check event is attached to experiment</li>
    <li>Verify in Preview mode</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ts-events-example",
                            Title = "Event Debugging Workflow",
                            Description = "Step-by-step event troubleshooting.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Complete event debugging workflow

// STEP 1: Check event is configured
var data = window.optimizely.get('data');
var eventName = 'purchase_complete'; // Your event name

var eventFound = false;
for (var id in data.events) {
    if (data.events[id].apiName === eventName) {
        eventFound = true;
        console.log('✅ Event found:', data.events[id]);
        break;
    }
}
if (!eventFound) {
    console.error('❌ Event not found. Available events:');
    for (var id in data.events) {
        console.log('  -', data.events[id].apiName);
    }
}

// STEP 2: Set up listener to see if event fires
window.optimizely.push({
    type: 'addListener',
    filter: {
        type: 'analytics',
        name: 'trackEvent'
    },
    handler: function(e) {
        console.log('🎯 Event tracked:', e.data.name, e.data.tags);
    }
});

// STEP 3: Manually fire test event
window.optimizely.push({
    type: 'event',
    eventName: eventName,
    tags: { test: true, revenue: 100 }
});

// STEP 4: Check network requests
// Look for requests to: logx.optimizely.com
// Filter Network tab by 'logx' or 'optimizely'

// STEP 5: Verify visitor is in experiment
var state = window.optimizely.get('state');
console.log('Active experiments:', state.getActiveExperimentIds());
console.log('Variation map:', state.getVariationMap());

// STEP 6: Check click event selector (for click events)
var selector = 'button.purchase-btn'; // Your selector
var elements = document.querySelectorAll(selector);
console.log('Elements matching selector:', elements.length);
elements.forEach(function(el, i) {
    console.log('  Element', i + 1, ':', el);
    el.style.outline = '3px solid red'; // Highlight for visual check
});

// STEP 7: Revenue format check
var orderTotal = 99.99;
var revenueInCents = Math.round(orderTotal * 100);
console.log('Revenue calculation:', orderTotal, '→', revenueInCents, 'cents');",
                            SampleResponse = @"Event debugging checklist:

✓ Event exists in Optimizely project
✓ Event name matches exactly (case-sensitive)
✓ Event listener confirms firing
✓ Network request sent to logx.optimizely.com
✓ Visitor is in an active experiment
✓ Event is a metric on that experiment
✓ Revenue is in cents (integer)

Common fixes:
- Event name typo → Copy exact apiName from config
- Not firing → Check selector matches element
- Not counting → Verify visitor is in experiment
- Revenue wrong → Multiply by 100, use Math.round()",
                            Hints = new List<string>
                            {
                                "Events won't count if the visitor isn't in an active experiment",
                                "Use Preview mode's Events tab to verify tracking in real-time"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "ts-devtools",
                    ModuleId = "troubleshooting",
                    Title = "Using Browser Developer Tools",
                    Summary = "Master browser dev tools for Optimizely debugging.",
                    Order = 5,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Use Console for Optimizely debugging",
                        "Inspect Network requests",
                        "Debug with Sources panel",
                        "Profile performance issues"
                    },
                    Content = @"
<h2>Browser Developer Tools for Debugging</h2>
<p>Browser DevTools are your <strong>primary debugging weapon</strong>. Mastering them makes troubleshooting significantly faster.</p>

<h3>Console Tab</h3>
<p>Essential for Optimizely state inspection:</p>
<ul>
    <li><code>window.optimizely</code> - Check if loaded</li>
    <li><code>window.optimizely.get('state')</code> - Current state</li>
    <li><code>window.optimizely.get('data')</code> - Project config</li>
    <li>Filter by 'optimizely' to see logs</li>
</ul>

<h3>Network Tab</h3>
<p>Track Optimizely requests:</p>
<ul>
    <li><strong>Snippet load</strong> - cdn.optimizely.com/js/[ID].js</li>
    <li><strong>Event tracking</strong> - logx.optimizely.com</li>
    <li>Filter by 'optimizely' to isolate requests</li>
    <li>Check response status codes</li>
</ul>

<h3>Elements Tab</h3>
<p>Inspect DOM for variation issues:</p>
<ul>
    <li>Check if Optimizely styles applied</li>
    <li>Look for <code>optly_</code> classes</li>
    <li>Verify element selectors match</li>
</ul>

<h3>Sources Tab</h3>
<p>Debug variation JavaScript:</p>
<ul>
    <li>Set breakpoints in variation code</li>
    <li>Find snippet in 'optimizely.com' sources</li>
    <li>Use conditional breakpoints for specific experiments</li>
</ul>

<h3>Application Tab</h3>
<p>Check storage:</p>
<ul>
    <li>Optimizely cookies (visitor ID, bucketing)</li>
    <li>localStorage data</li>
    <li>Clear to reset visitor state</li>
</ul>

<h3>Performance Tab</h3>
<p>Profile Optimizely impact:</p>
<ul>
    <li>Record page load</li>
    <li>Look for long tasks from Optimizely</li>
    <li>Identify slow variation code</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ts-devtools-example",
                            Title = "DevTools Debugging Cheat Sheet",
                            Description = "Quick reference for DevTools debugging.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// === CONSOLE COMMANDS CHEAT SHEET ===

// Basic checks
window.optimizely                    // Is it loaded?
window.optimizely.get('state')       // Current state object
window.optimizely.get('data')        // Project configuration

// Experiment info
var s = window.optimizely.get('state');
s.getActiveExperimentIds()           // Running experiments
s.getVariationMap()                  // Your variation assignments
s.getCampaignStates()                // Detailed campaign info
s.getVisitorId()                     // Your visitor ID

// Debug specific experiment
var expId = '12345678901';
var d = window.optimizely.get('data');
d.experiments[expId]                 // Experiment config
d.experiments[expId].variations      // Variations

// Event debugging
var events = window.optimizely.get('data').events;
Object.values(events).map(e => e.apiName)  // List all event names

// Set up event listener
window.optimizely.push({
    type: 'addListener',
    filter: { type: 'lifecycle', name: 'activated' },
    handler: e => console.log('Activated:', e.data.campaign.name)
});

// === NETWORK TAB FILTERS ===
// Filter: optimizely
// Filter: logx
// Filter: cdn.optimizely

// === COOKIE INSPECTION ===
// Cookie: optimizelyEndUserId = visitor ID
// Cookie: optimizelyBuckets = variation assignments
// Clear these to reset your visitor state

// === PERFORMANCE ANALYSIS ===
// 1. Open Performance tab
// 2. Click Record
// 3. Reload page
// 4. Stop recording
// 5. Search for 'optimizely' in bottom panel
// 6. Look for long tasks (>50ms)

// === CONSOLE FILTERING ===
// Type 'optimizely' in filter box
// Or use: console.log('%cOptimizely Debug', 'color: blue; font-weight: bold')

// === QUICK RESET ===
// Clear Optimizely cookies to test as new visitor:
document.cookie.split(';').forEach(c => {
    if (c.includes('optimizely')) {
        var name = c.split('=')[0].trim();
        document.cookie = name + '=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/';
    }
});
console.log('Optimizely cookies cleared - reload page');",
                            SampleResponse = @"DevTools workflow for common issues:

EXPERIMENT NOT RUNNING:
1. Console: Check window.optimizely exists
2. Console: s.getActiveExperimentIds()
3. Network: Verify snippet loaded (200 status)
4. Console: Check experiment in d.experiments

VARIATION NOT APPLYING:
1. Elements: Search for changed element
2. Console: Check variationMap includes experiment
3. Sources: Set breakpoint in variation code
4. Console: Check for JS errors

EVENTS NOT TRACKING:
1. Network: Filter 'logx', look for POST requests
2. Console: Set up event listener
3. Console: Manually fire test event
4. Network: Verify request sent

PERFORMANCE ISSUES:
1. Performance: Record page load
2. Search: 'optimizely' in call tree
3. Identify: Long tasks or slow code
4. Console: Profile variation execution time",
                            Hints = new List<string>
                            {
                                "Keep DevTools open while testing - it provides real-time feedback",
                                "Use Preserve Log in Network tab to see requests across page navigations"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 12: Testing Methodology

    private LearningModule BuildTestingMethodologyModule()
    {
        return new LearningModule
        {
            Id = "testing-methodology",
            Title = "Testing Methodology and Best Practices",
            Description = "Learn proven methodologies for effective experimentation programs.",
            Icon = "clipboard-document-check",
            Order = 12,
            Difficulty = ModuleDifficulty.Intermediate,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "tm-hypotheses",
                    ModuleId = "testing-methodology",
                    Title = "Forming Strong Hypotheses",
                    Summary = "Create data-driven experiment hypotheses that lead to actionable results.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Structure hypotheses properly",
                        "Base hypotheses on data and insights",
                        "Define measurable success criteria",
                        "Prioritize testing opportunities"
                    },
                    Content = @"
<h2>The Art of Hypothesis Formation</h2>
<p>A strong hypothesis is the foundation of any successful experiment. Without one, you're just randomly changing things and hoping for improvement.</p>

<h3>The Hypothesis Framework</h3>
<p>Use the <strong>If-Then-Because</strong> format:</p>
<blockquote>
<p><strong>If</strong> [we make this change]<br>
<strong>Then</strong> [this metric will improve]<br>
<strong>Because</strong> [this insight/data suggests it]</p>
</blockquote>

<h3>Example Hypotheses</h3>
<p><strong>Weak:</strong> ""Let's test a new button color""</p>
<p><strong>Strong:</strong> ""If we change the CTA button from gray to blue, then click-through rate will increase by 10%, because our heatmaps show users aren't noticing the current button and blue aligns with our brand's action color.""</p>

<h3>Sources for Hypothesis Ideas</h3>
<ul>
    <li><strong>Analytics data</strong> - High drop-off pages, low conversion flows</li>
    <li><strong>User research</strong> - Surveys, interviews, usability tests</li>
    <li><strong>Heatmaps/recordings</strong> - Where users struggle</li>
    <li><strong>Customer support</strong> - Common complaints and questions</li>
    <li><strong>Competitor analysis</strong> - What others are doing differently</li>
    <li><strong>Best practices</strong> - Industry standards (but always test!)</li>
</ul>

<h3>Prioritization Frameworks</h3>

<h4>PIE Framework</h4>
<ul>
    <li><strong>P</strong>otential - How much improvement is possible?</li>
    <li><strong>I</strong>mportance - How valuable is this page/flow?</li>
    <li><strong>E</strong>ase - How easy is it to test?</li>
</ul>

<h4>ICE Framework</h4>
<ul>
    <li><strong>I</strong>mpact - Expected impact if successful</li>
    <li><strong>C</strong>onfidence - How sure are you it will work?</li>
    <li><strong>E</strong>ase - Implementation difficulty</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "tm-hypothesis-template",
                            Title = "Hypothesis Documentation Template",
                            Description = "Template for documenting experiment hypotheses.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"HYPOTHESIS TEMPLATE
==================

Experiment Name: [Descriptive name]
Date Created: [Date]
Owner: [Name]

HYPOTHESIS STATEMENT
--------------------
If we [specific change]
Then [metric] will [increase/decrease] by [expected amount]
Because [supporting evidence/insight]

SUPPORTING DATA
---------------
1. [Data point 1 - e.g., ""Analytics shows 60% cart abandonment""]
2. [Data point 2 - e.g., ""Heatmaps show users miss the checkout button""]
3. [Data point 3 - e.g., ""Survey: 40% say shipping costs unclear""]

SUCCESS METRICS
---------------
Primary: [Main metric to measure]
Secondary: [Supporting metrics]
Guardrail: [Metrics that shouldn't decrease]

PRIORITIZATION SCORE
--------------------
PIE Score: P[1-10] + I[1-10] + E[1-10] = [Total]

Or:

ICE Score: I[1-10] × C[1-10] × E[1-10] = [Total]

RISKS AND CONSIDERATIONS
------------------------
- [Potential negative effects]
- [Technical constraints]
- [Audience considerations]

EXAMPLE FILLED OUT
------------------
Experiment Name: Homepage Hero CTA Test
If we: Change CTA from ""Learn More"" to ""Start Free Trial""
Then: Hero CTA clicks will increase by 15%
Because: User interviews show visitors want to try before committing,
         and ""Learn More"" suggests more reading rather than action

Supporting Data:
1. Current hero CTR: 2.3%
2. User interviews: 7/10 mentioned wanting a trial
3. Competitor analysis: Top 3 competitors use trial language

PIE Score: P(8) + I(10) + E(9) = 27 (High Priority)",
                            SampleResponse = @"Hypothesis quality checklist:

✓ Specific change defined
✓ Measurable success metric
✓ Quantified expected improvement
✓ Evidence-based reasoning
✓ Prioritization score calculated
✓ Guardrail metrics identified

Common mistakes to avoid:
✗ Vague changes (""improve the page"")
✗ No success metric defined
✗ No supporting data/evidence
✗ Testing based on opinions only
✗ Forgetting guardrail metrics",
                            Hints = new List<string>
                            {
                                "Document hypotheses BEFORE building experiments",
                                "A failed experiment with a documented hypothesis still provides learning"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "tm-design",
                    ModuleId = "testing-methodology",
                    Title = "Experiment Design Principles",
                    Summary = "Design experiments that produce valid, actionable results.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Avoid common experimental design flaws",
                        "Control for confounding variables",
                        "Design for statistical power",
                        "Plan for edge cases and segments"
                    },
                    Content = @"
<h2>Principles of Good Experiment Design</h2>
<p>Poor experiment design leads to misleading results. Following these principles ensures your results are <strong>valid and actionable</strong>.</p>

<h3>Principle 1: Test One Variable at a Time</h3>
<p>When testing multiple changes simultaneously:</p>
<ul>
    <li>You can't tell which change caused the result</li>
    <li>Interactions between changes may mask effects</li>
    <li><strong>Exception</strong>: Multivariate tests designed for this</li>
</ul>

<h3>Principle 2: Use Proper Controls</h3>
<ul>
    <li>Always have a baseline/control variation</li>
    <li>Control should be current experience (not a new design)</li>
    <li>Don't modify control during experiment</li>
</ul>

<h3>Principle 3: Account for External Factors</h3>
<p>Confounding variables that can skew results:</p>
<ul>
    <li><strong>Seasonality</strong> - Holiday traffic behaves differently</li>
    <li><strong>Day of week</strong> - Weekend vs weekday patterns</li>
    <li><strong>Marketing campaigns</strong> - Traffic quality changes</li>
    <li><strong>Product changes</strong> - New features affect behavior</li>
</ul>

<h3>Principle 4: Consider the Novelty Effect</h3>
<p>New things get more attention initially:</p>
<ul>
    <li>Early results may be inflated</li>
    <li>Run experiments for at least 2 weeks</li>
    <li>Look for result stability over time</li>
</ul>

<h3>Principle 5: Design for Your Traffic</h3>
<ul>
    <li>Low traffic? Test bigger changes (larger MDE)</li>
    <li>High traffic? Can test subtle changes</li>
    <li>Calculate sample size before launching</li>
</ul>

<h3>Principle 6: Plan Your Segments</h3>
<ul>
    <li>Decide which segments matter BEFORE launch</li>
    <li>Ensure each segment has enough traffic</li>
    <li>Don't slice data too thin post-hoc</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "tm-design-checklist",
                            Title = "Experiment Design Checklist",
                            Description = "Pre-launch checklist for experiment design.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"EXPERIMENT DESIGN CHECKLIST
===========================

HYPOTHESIS VALIDATION
□ Hypothesis follows If-Then-Because format
□ Change is specific and implementable
□ Success metric is defined and measurable
□ Expected effect size is realistic

STATISTICAL DESIGN
□ Sample size calculated
□ Expected duration estimated
□ Traffic allocation decided
□ Significance threshold set (90%/95%/99%)

CONTROL INTEGRITY
□ Control is current experience (not modified)
□ Only ONE variable changed per variation
□ Variation code doesn't affect control

TIMING CONSIDERATIONS
□ Launch avoids holidays/special events
□ Plan to run for full business cycles
□ No major site changes planned during test

SEGMENT PLANNING
□ Key segments identified in advance
□ Segments have sufficient traffic
□ Segment analysis plan documented

GUARDRAIL METRICS
□ Primary metric defined
□ Secondary metrics selected
□ Guardrail metrics identified (shouldn't drop)

TECHNICAL VALIDATION
□ Variations work in all browsers
□ Mobile experience tested
□ No JavaScript errors
□ Page load not significantly impacted

DOCUMENTATION
□ Hypothesis documented
□ Success criteria defined
□ Stakeholders aligned
□ Results reporting plan ready

EXAMPLE: CHECKOUT FLOW TEST
---------------------------
✓ Hypothesis: If we simplify checkout to single page,
  then conversion increases 10%, because 40% abandon
  at shipping step

✓ Sample size: 50,000 visitors per variation
✓ Duration: 3 weeks (covers 2 weekend cycles)
✓ Significance: 95%

✓ Segments to analyze:
  - Mobile vs Desktop
  - New vs Returning
  - Cart value (<$50, $50-100, >$100)

✓ Guardrails:
  - Revenue per visitor (shouldn't drop)
  - Customer support tickets (shouldn't increase)",
                            SampleResponse = @"Design quality indicators:

GOOD DESIGN:
✓ Single variable changed
✓ Clear success metric
✓ Adequate sample size
✓ Full business cycle duration
✓ Pre-planned segments
✓ Guardrail metrics defined

RED FLAGS:
✗ Multiple changes in one variation
✗ Vague success criteria
✗ Too short duration (<1 week)
✗ No sample size calculation
✗ Fishing for segments post-hoc
✗ No guardrails defined",
                            Hints = new List<string>
                            {
                                "Document your design decisions - you'll thank yourself later",
                                "When in doubt, run longer rather than shorter"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "tm-program",
                    ModuleId = "testing-methodology",
                    Title = "Building a Testing Program",
                    Summary = "Establish an ongoing experimentation program in your organization.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create a testing roadmap",
                        "Build organizational buy-in",
                        "Establish testing governance",
                        "Measure program success"
                    },
                    Content = @"
<h2>Building an Experimentation Program</h2>
<p>Moving from occasional tests to a <strong>systematic experimentation program</strong> maximizes the value of your testing efforts.</p>

<h3>Program Maturity Levels</h3>

<h4>Level 1: Ad Hoc</h4>
<ul>
    <li>Occasional experiments</li>
    <li>No formal process</li>
    <li>Limited stakeholder awareness</li>
</ul>

<h4>Level 2: Structured</h4>
<ul>
    <li>Regular testing cadence</li>
    <li>Documented process</li>
    <li>Dedicated resources</li>
</ul>

<h4>Level 3: Scaled</h4>
<ul>
    <li>Multiple teams running experiments</li>
    <li>Centralized governance</li>
    <li>Knowledge sharing systems</li>
</ul>

<h4>Level 4: Optimized</h4>
<ul>
    <li>Experimentation culture</li>
    <li>Data-driven decision making</li>
    <li>Continuous learning</li>
</ul>

<h3>Key Success Metrics</h3>
<ul>
    <li><strong>Test velocity</strong> - Experiments launched per month</li>
    <li><strong>Win rate</strong> - % of tests with significant positive results</li>
    <li><strong>Implementation rate</strong> - % of winners actually shipped</li>
    <li><strong>Business impact</strong> - Revenue/conversion lift from winners</li>
</ul>

<h3>Building Buy-In</h3>
<ul>
    <li>Start with quick wins</li>
    <li>Share results widely</li>
    <li>Calculate and communicate ROI</li>
    <li>Involve stakeholders in hypothesis generation</li>
</ul>

<h3>Governance Structure</h3>
<ul>
    <li><strong>Experiment review</strong> - Approve before launch</li>
    <li><strong>Traffic management</strong> - Avoid collisions</li>
    <li><strong>Quality standards</strong> - Consistent methodology</li>
    <li><strong>Knowledge management</strong> - Document and share learnings</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "tm-program-metrics",
                            Title = "Program Metrics Dashboard",
                            Description = "Metrics to track experimentation program health.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"EXPERIMENTATION PROGRAM DASHBOARD
==================================

VELOCITY METRICS (Monthly)
--------------------------
Experiments Launched:     12
Experiments Completed:    8
Experiments in Queue:     15
Avg. Days to Launch:      5

WIN/LOSS ANALYSIS
-----------------
Winners:                  3 (37.5%)
Losers:                   1 (12.5%)
Inconclusive:             4 (50%)

Industry benchmark: 20-30% win rate is healthy
(Most ideas don't work - that's why we test!)

IMPACT METRICS (YTD)
--------------------
Implemented Winners:      8
Revenue Lift:            $450,000
Conversion Lift:         +2.3 percentage points
Experiments Run:         45

ROI CALCULATION
---------------
Testing Platform Cost:   $50,000/year
Team Time (estimate):    $150,000/year
Total Investment:        $200,000/year

Revenue from Winners:    $450,000
ROI:                    125%

PROGRAM HEALTH INDICATORS
-------------------------
✓ Test velocity stable (10-15/month)
✓ Win rate healthy (>25%)
✓ Winners being implemented (>80%)
✓ Stakeholder satisfaction high
✓ Learning being documented

AREAS FOR IMPROVEMENT
---------------------
⚠ Implementation delay (avg 45 days post-win)
⚠ Documentation incomplete for 30% of tests
⚠ Mobile experiments underrepresented

QUARTERLY GOALS
---------------
1. Reduce implementation delay to <30 days
2. 100% documentation compliance
3. 30% of experiments mobile-focused",
                            SampleResponse = @"Program benchmarks by maturity:

STARTER PROGRAM:
- 5-10 experiments/month
- 15-25% win rate
- Basic documentation

ESTABLISHED PROGRAM:
- 10-20 experiments/month
- 25-35% win rate
- Knowledge base maintained
- ROI tracking in place

ADVANCED PROGRAM:
- 20+ experiments/month
- Testing across multiple teams
- Automated reporting
- Experimentation culture embedded

Key insight: A high win rate (>40%) might
mean you're not testing bold enough ideas!",
                            Hints = new List<string>
                            {
                                "Track and celebrate learnings from failed tests, not just wins",
                                "A 100% win rate means you're not testing ambitious enough ideas"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "tm-documentation",
                    ModuleId = "testing-methodology",
                    Title = "Documentation and Knowledge Sharing",
                    Summary = "Document experiments for organizational learning and knowledge retention.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Document experiment context and results",
                        "Record learnings from both wins and losses",
                        "Share insights across teams",
                        "Build institutional knowledge"
                    },
                    Content = @"
<h2>Documentation and Knowledge Sharing</h2>
<p>Without proper documentation, the same experiments get run repeatedly and valuable learnings are lost when people leave.</p>

<h3>What to Document</h3>

<h4>Before Launch</h4>
<ul>
    <li>Hypothesis and supporting data</li>
    <li>Success criteria and metrics</li>
    <li>Variation descriptions with screenshots</li>
    <li>Technical implementation notes</li>
</ul>

<h4>After Completion</h4>
<ul>
    <li>Results summary</li>
    <li>Statistical details</li>
    <li>Segment analysis</li>
    <li>Learnings and insights</li>
    <li>Recommendations for follow-up</li>
</ul>

<h3>Documentation Best Practices</h3>
<ul>
    <li><strong>Standardize format</strong> - Use templates</li>
    <li><strong>Include visuals</strong> - Screenshots, graphs</li>
    <li><strong>Be specific</strong> - Avoid vague conclusions</li>
    <li><strong>Document failures</strong> - They're valuable too</li>
</ul>

<h3>Knowledge Base Structure</h3>
<ul>
    <li>Searchable by page/feature tested</li>
    <li>Filterable by outcome (win/loss/inconclusive)</li>
    <li>Tagged by hypothesis type</li>
    <li>Linked to related experiments</li>
</ul>

<h3>Sharing Insights</h3>
<ul>
    <li><strong>Monthly reviews</strong> - Team discussion of results</li>
    <li><strong>Stakeholder updates</strong> - Executive summaries</li>
    <li><strong>Cross-team sharing</strong> - Learnings that apply broadly</li>
    <li><strong>Onboarding material</strong> - Past experiments for new team members</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "tm-doc-template",
                            Title = "Experiment Results Template",
                            Description = "Template for documenting experiment results.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"EXPERIMENT RESULTS DOCUMENT
===========================

EXPERIMENT OVERVIEW
-------------------
Name: Homepage Hero CTA Test
ID: EXP-2024-042
Owner: Jane Smith
Duration: Jan 15 - Jan 29, 2024

HYPOTHESIS
----------
If we change the CTA from ""Learn More"" to ""Start Free Trial"",
then hero clicks will increase by 15%,
because user research shows visitors want to try before buying.

VARIATIONS
----------
Control: ""Learn More"" button (gray)
Variation 1: ""Start Free Trial"" button (blue)

[Include screenshots here]

RESULTS SUMMARY
---------------
                  Visitors    Conversions    Rate      Lift
Control           25,412      584           2.30%     -
Variation 1       25,389      731           2.88%     +25.2%

Statistical Significance: 97%
Confidence Interval: [+12%, +38%]

RESULT: ✅ WINNER - Variation 1

SEGMENT ANALYSIS
----------------
                Desktop     Mobile
Control         2.45%       1.98%
Variation 1     3.12%       2.41%

Both segments show improvement. Mobile lift slightly higher
but not statistically significant difference between segments.

KEY LEARNINGS
-------------
1. Action-oriented CTAs outperform informational ones
2. Blue button visibility likely contributed to lift
3. ""Free Trial"" messaging resonates with our audience

RECOMMENDATIONS
---------------
1. Implement Variation 1 site-wide ✓
2. Test ""Start Free Trial"" on other CTAs
3. Explore other action-oriented language
4. A/B test trial length messaging (7-day vs 14-day)

FOLLOW-UP EXPERIMENTS
---------------------
- EXP-2024-048: Pricing page CTA language
- EXP-2024-051: Trial length comparison

IMPLEMENTATION STATUS
---------------------
Winner implemented: Feb 5, 2024
Post-implementation metric check: Confirmed +23% lift

TAGS
----
#homepage #cta #messaging #winner #q1-2024",
                            SampleResponse = @"Documentation checklist:

PRE-LAUNCH:
□ Hypothesis documented
□ Variations described
□ Screenshots captured
□ Success criteria defined

POST-COMPLETION:
□ Results summarized
□ Statistical details included
□ Segments analyzed
□ Learnings extracted
□ Recommendations made
□ Follow-ups identified

AFTER IMPLEMENTATION:
□ Winner confirmed in production
□ Post-implementation metrics verified
□ Knowledge base updated",
                            Hints = new List<string>
                            {
                                "Future you will thank present you for thorough documentation",
                                "Failed experiments are just as valuable to document as winners"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 13: QA and Preview Mode

    private LearningModule BuildQAPreviewModule()
    {
        return new LearningModule
        {
            Id = "qa-preview",
            Title = "QA and Preview Mode",
            Description = "Master quality assurance for reliable experiments.",
            Icon = "magnifying-glass",
            Order = 13,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "qa-preview-basics",
                    ModuleId = "qa-preview",
                    Title = "Preview Mode Fundamentals",
                    Summary = "Use Preview mode to test experiments before going live.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Access and use Preview mode",
                        "Force variations for testing",
                        "Verify changes are applying correctly",
                        "Share preview links with stakeholders"
                    },
                    Content = @"
<h2>Preview Mode Fundamentals</h2>
<p>Preview mode lets you test experiments <strong>before they go live</strong>, ensuring everything works correctly without affecting real visitors.</p>

<h3>Accessing Preview Mode</h3>
<ol>
    <li>Open your experiment in Optimizely</li>
    <li>Click the ""Preview"" button</li>
    <li>A new tab opens with your site and Preview toolbar</li>
</ol>

<h3>Preview Toolbar Features</h3>
<ul>
    <li><strong>Variation selector</strong> - Choose which variation to view</li>
    <li><strong>Page selector</strong> - Switch between experiment pages</li>
    <li><strong>Events tab</strong> - See events firing in real-time</li>
    <li><strong>Audiences tab</strong> - Check audience conditions</li>
    <li><strong>Share button</strong> - Create shareable preview link</li>
</ul>

<h3>Force Link URLs</h3>
<p>Preview generates special URLs to force specific variations:</p>
<pre><code>https://yoursite.com?optimizely_x12345=67890</code></pre>
<p>This bypasses normal bucketing and shows the specified variation.</p>

<h3>What to Test in Preview</h3>
<ul>
    <li>Visual changes appear correctly</li>
    <li>Functionality still works</li>
    <li>Events fire when expected</li>
    <li>No JavaScript errors</li>
    <li>Mobile/responsive behavior</li>
</ul>

<h3>Preview vs Live</h3>
<p>Remember that Preview mode:</p>
<ul>
    <li>Bypasses audience targeting</li>
    <li>Bypasses traffic allocation</li>
    <li>May behave differently than live (caching, etc.)</li>
    <li>Events tracked but not counted in results</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "qa-preview-example",
                            Title = "Preview Mode Workflow",
                            Description = "Step-by-step preview testing process.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"PREVIEW MODE QA WORKFLOW
========================

STEP 1: ACCESS PREVIEW
----------------------
1. Open experiment in Optimizely
2. Click ""Preview"" button
3. Wait for site to load with Preview toolbar

STEP 2: TEST EACH VARIATION
---------------------------
For each variation (including Control):

□ Select variation in toolbar
□ Verify visual changes appear
□ Check all affected elements
□ Test interactive elements (buttons, forms)
□ Scroll through entire page
□ Open browser console - check for errors

STEP 3: TEST EVENTS
-------------------
1. Click ""Events"" tab in Preview toolbar
2. Trigger each conversion action
3. Verify events appear in the list
4. Check event data (revenue, properties)

STEP 4: TEST ON DEVICES
-----------------------
□ Desktop - Chrome
□ Desktop - Safari
□ Desktop - Firefox
□ Mobile - iOS Safari
□ Mobile - Android Chrome
□ Tablet (if applicable)

STEP 5: SHARE FOR REVIEW
------------------------
1. Click ""Share"" in Preview toolbar
2. Copy the preview link
3. Send to stakeholders
4. Note: Link forces specific variation

PREVIEW LINK FORMAT:
https://yoursite.com?optimizely_x[EXPERIMENT_ID]=[VARIATION_ID]

STEP 6: FINAL CHECKS
--------------------
□ All variations tested
□ All devices tested
□ Events tracking verified
□ No console errors
□ Stakeholder approval received
□ Ready to launch",
                            SampleResponse = @"Preview mode tips:

1. TEST IN INCOGNITO
   Regular browsing may have cached
   content or existing cookies

2. CHECK THE CONSOLE
   Open DevTools and watch for
   JavaScript errors

3. VERIFY EVENTS
   Use the Events tab - don't just
   assume they're firing

4. TEST REAL FLOWS
   Don't just look at the page -
   actually click through

5. SHARE SPECIFIC VARIATIONS
   Each stakeholder can get a link
   to their assigned variation",
                            Hints = new List<string>
                            {
                                "Always test in incognito/private mode for cleanest results",
                                "Preview links expire - regenerate if sharing later"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "qa-targeting",
                    ModuleId = "qa-preview",
                    Title = "Testing Targeting and Audiences",
                    Summary = "Validate audience targeting in your experiments.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Test audience conditions",
                        "Verify URL targeting",
                        "Simulate different visitor attributes",
                        "Debug targeting issues"
                    },
                    Content = @"
<h2>Testing Targeting and Audiences</h2>
<p>Targeting mistakes can cause experiments to run on wrong pages or wrong audiences. Thorough testing prevents wasted time and invalid results.</p>

<h3>URL Targeting Verification</h3>
<p>Test that your experiment activates on intended pages:</p>
<ul>
    <li>Visit each URL that should match</li>
    <li>Visit similar URLs that should NOT match</li>
    <li>Test with/without trailing slashes</li>
    <li>Test with/without query parameters</li>
    <li>Test www vs non-www</li>
</ul>

<h3>Audience Condition Testing</h3>
<p>For each audience condition type:</p>

<h4>Browser/Device</h4>
<ul>
    <li>Use actual devices or DevTools device mode</li>
    <li>Test in multiple browsers</li>
</ul>

<h4>Geography</h4>
<ul>
    <li>Use VPN to simulate different locations</li>
    <li>Note: May not match exactly due to IP detection</li>
</ul>

<h4>Cookies</h4>
<ul>
    <li>Set test cookies manually in DevTools</li>
    <li>Verify cookie values match exactly (case-sensitive)</li>
</ul>

<h4>Custom JavaScript</h4>
<ul>
    <li>Run condition code in console</li>
    <li>Verify it returns true/false as expected</li>
    <li>Test edge cases (missing data, etc.)</li>
</ul>

<h3>Audiences Tab in Preview</h3>
<p>The Preview toolbar's Audiences tab shows:</p>
<ul>
    <li>Which audiences you qualify for</li>
    <li>Which conditions passed/failed</li>
    <li>Why you might be excluded</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "qa-targeting-example",
                            Title = "Targeting Test Scenarios",
                            Description = "Test scenarios for different targeting types.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// TESTING TARGETING CONDITIONS

// === URL TARGETING ===
// Your targeting: ""URL contains /products/""

// Should match:
// ✓ https://site.com/products/
// ✓ https://site.com/products/item-1
// ✓ https://site.com/products/?sort=price

// Should NOT match:
// ✗ https://site.com/product/item-1 (missing 's')
// ✗ https://site.com/all-products/
// ✗ https://site.com/about/

// === COOKIE TARGETING ===
// Your targeting: Cookie ""user_type"" equals ""premium""

// Set test cookie in console:
document.cookie = 'user_type=premium; path=/';
// Reload and verify experiment activates

// Test wrong value:
document.cookie = 'user_type=free; path=/';
// Reload and verify experiment does NOT activate

// Clear cookie:
document.cookie = 'user_type=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/';

// === CUSTOM JS CONDITION ===
// Your condition:
function() {
    return window.userData && window.userData.plan === 'enterprise';
}

// Test in console:
// Simulate the data
window.userData = { plan: 'enterprise' };
// Run your condition - should return true
(function() { return window.userData && window.userData.plan === 'enterprise'; })()
// Returns: true

// Test failure case
window.userData = { plan: 'free' };
(function() { return window.userData && window.userData.plan === 'enterprise'; })()
// Returns: false

// Test missing data
delete window.userData;
(function() { return window.userData && window.userData.plan === 'enterprise'; })()
// Returns: false (not undefined due to && short-circuit)

// === DEVICE TARGETING ===
// Use Chrome DevTools Device Mode:
// 1. Open DevTools (F12)
// 2. Click device toolbar icon
// 3. Select device (iPhone, iPad, etc.)
// 4. Reload page
// 5. Verify experiment activates/doesn't activate",
                            SampleResponse = @"Targeting test matrix:

URL TARGETING:
□ Exact match URLs
□ Pattern match URLs
□ Should-not-match URLs
□ Query parameter variations
□ Protocol (http/https)

AUDIENCE CONDITIONS:
□ Browser condition (test 2+ browsers)
□ Device condition (desktop, mobile, tablet)
□ Cookie condition (set/unset/wrong value)
□ Custom JS (true case, false case, error case)
□ Geo condition (if using VPN)

COMBINATION TESTING:
□ All conditions met = activates
□ One condition fails = doesn't activate
□ AND vs OR logic behaves correctly",
                            Hints = new List<string>
                            {
                                "Test the negative cases - make sure experiment DOESN'T run when it shouldn't",
                                "Custom JS conditions should handle missing data gracefully"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "qa-events",
                    ModuleId = "qa-preview",
                    Title = "Event and Metric Validation",
                    Summary = "Ensure events and metrics track correctly before launch.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Verify event configuration",
                        "Test conversion tracking",
                        "Validate revenue tracking",
                        "Check metric calculations"
                    },
                    Content = @"
<h2>Event and Metric Validation</h2>
<p>If events don't track properly, your experiment results will be incomplete or misleading. <strong>Test every event</strong> before launch.</p>

<h3>Event Testing Checklist</h3>
<p>For each event in your experiment:</p>
<ol>
    <li>Understand what triggers the event</li>
    <li>Trigger the action in Preview mode</li>
    <li>Verify event appears in Events tab</li>
    <li>Check event has correct properties</li>
    <li>Verify in Network tab (logx.optimizely.com)</li>
</ol>

<h3>Click Event Testing</h3>
<ul>
    <li>Click the target element</li>
    <li>Verify click registers in Events tab</li>
    <li>Test on different browsers</li>
    <li>Test if element is dynamically loaded</li>
</ul>

<h3>Pageview Event Testing</h3>
<ul>
    <li>Navigate to the target page</li>
    <li>Check event fires on page load</li>
    <li>Test different URL variations</li>
    <li>Test with query parameters</li>
</ul>

<h3>Custom Event Testing</h3>
<ul>
    <li>Trigger the custom event code</li>
    <li>Verify event name matches configuration</li>
    <li>Check tags/properties are included</li>
    <li>Verify revenue format (cents)</li>
</ul>

<h3>Revenue Validation</h3>
<p>Revenue tracking requires special attention:</p>
<ul>
    <li>Verify amount is in cents (not dollars)</li>
    <li>Check value is a number (not string)</li>
    <li>Test with different amounts</li>
    <li>Verify in Network tab payload</li>
</ul>

<h3>Post-Launch Validation</h3>
<p>After launching, verify events are counting:</p>
<ul>
    <li>Check Results page shows conversions</li>
    <li>Verify counts make sense (not zero, not impossibly high)</li>
    <li>Compare to analytics data</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "qa-events-example",
                            Title = "Event Validation Workflow",
                            Description = "Complete event testing process.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// COMPLETE EVENT VALIDATION WORKFLOW

// === STEP 1: LIST ALL METRICS ===
// In Optimizely, note all metrics attached to your experiment
// Example metrics:
// - cta_click (click event)
// - checkout_start (pageview event)
// - purchase_complete (custom event with revenue)

// === STEP 2: SET UP MONITORING ===
// Open DevTools, go to Console, paste:

window.optimizely.push({
    type: 'addListener',
    filter: { type: 'analytics', name: 'trackEvent' },
    handler: function(e) {
        console.log('%c✓ EVENT TRACKED', 'color: green; font-weight: bold');
        console.log('Name:', e.data.name);
        console.log('Tags:', e.data.tags);
    }
});

// Also open Network tab, filter by 'logx'

// === STEP 3: TEST EACH EVENT ===

// Test: cta_click
// Action: Click the CTA button
// Expected: See ""✓ EVENT TRACKED"" with name ""cta_click""
// Verify: Network request to logx.optimizely.com

// Test: checkout_start
// Action: Navigate to /checkout page
// Expected: Event fires on page load
// Verify: Events tab shows pageview

// Test: purchase_complete
// Action: Complete a test purchase (or simulate)
// Expected: Event with revenue tag
// Verify: Revenue is in CENTS

// === STEP 4: VERIFY REVENUE FORMAT ===
// If you see this in the event:
console.log('Revenue check:');
var testRevenue = 99.99;
var correctFormat = Math.round(testRevenue * 100);
console.log('$' + testRevenue + ' should be sent as:', correctFormat, 'cents');

// Manually fire test event with revenue:
window.optimizely.push({
    type: 'event',
    eventName: 'purchase_complete',
    tags: {
        revenue: 9999,  // $99.99 in cents
        value: 99.99    // Human-readable
    }
});

// === STEP 5: VERIFY IN RESULTS (POST-LAUNCH) ===
// After 1 hour of live traffic:
// 1. Go to Results page
// 2. Check conversion counts > 0
// 3. Check revenue totals (if applicable)
// 4. Compare to expected range

// Expected validation:
// If you get 1000 visitors and 3% convert,
// you should see ~30 conversions",
                            SampleResponse = @"Event validation checklist:

CLICK EVENTS:
□ Selector matches correct element
□ Click registers in Events tab
□ Works on all target browsers
□ Works when element loads dynamically

PAGEVIEW EVENTS:
□ Fires on page load
□ URL matching is correct
□ Works with query parameters
□ Works with hash fragments

CUSTOM EVENTS:
□ Event name matches exactly
□ Tags/properties included
□ Revenue in cents (if applicable)
□ Network request visible

REVENUE TRACKING:
□ Value is in cents
□ Value is a number
□ Value is positive
□ Matches expected amount",
                            Hints = new List<string>
                            {
                                "Test events in Preview mode first, then verify they work live",
                                "Revenue must be in cents - $99.99 = 9999"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "qa-cross-browser",
                    ModuleId = "qa-preview",
                    Title = "Cross-Browser and Device Testing",
                    Summary = "Test experiments across browsers and devices.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Test on multiple browsers",
                        "Verify mobile experiences",
                        "Handle browser-specific issues",
                        "Test responsive designs"
                    },
                    Content = @"
<h2>Cross-Browser and Device Testing</h2>
<p>Your experiment might work perfectly in Chrome but break in Safari. Cross-browser testing ensures <strong>all visitors get a working experience</strong>.</p>

<h3>Browser Testing Priority</h3>
<p>Test in order of your traffic share (check analytics):</p>
<ol>
    <li><strong>Chrome</strong> (typically 50-60% of traffic)</li>
    <li><strong>Safari</strong> (especially iOS)</li>
    <li><strong>Firefox</strong></li>
    <li><strong>Edge</strong></li>
    <li><strong>Mobile browsers</strong> (Chrome, Safari)</li>
</ol>

<h3>Common Cross-Browser Issues</h3>

<h4>CSS Issues</h4>
<ul>
    <li>Flexbox/Grid differences</li>
    <li>Font rendering variations</li>
    <li>Animation performance</li>
</ul>

<h4>JavaScript Issues</h4>
<ul>
    <li>ES6+ syntax not supported in older browsers</li>
    <li>API availability differences</li>
    <li>Event handling variations</li>
</ul>

<h4>Mobile-Specific Issues</h4>
<ul>
    <li>Touch vs click events</li>
    <li>Viewport sizing</li>
    <li>iOS Safari quirks</li>
    <li>Android Chrome differences</li>
</ul>

<h3>Testing Tools</h3>
<ul>
    <li><strong>Browser DevTools</strong> - Device emulation mode</li>
    <li><strong>Real devices</strong> - Most accurate testing</li>
    <li><strong>BrowserStack/Sauce Labs</strong> - Cross-browser testing platforms</li>
    <li><strong>Preview links</strong> - Test on any device</li>
</ul>

<h3>Mobile Testing Checklist</h3>
<ul>
    <li>Touch targets are large enough (44x44px min)</li>
    <li>Text is readable without zooming</li>
    <li>Forms work with mobile keyboard</li>
    <li>Scroll behavior is correct</li>
    <li>No horizontal scroll</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "qa-browser-matrix",
                            Title = "Browser Testing Matrix",
                            Description = "Template for tracking cross-browser testing.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"CROSS-BROWSER TESTING MATRIX
============================

Experiment: [Name]
Tester: [Name]
Date: [Date]

DESKTOP BROWSERS
----------------
                    Control     Var 1       Var 2
Chrome (latest)     □ Pass      □ Pass      □ Pass
Safari (latest)     □ Pass      □ Pass      □ Pass
Firefox (latest)    □ Pass      □ Pass      □ Pass
Edge (latest)       □ Pass      □ Pass      □ Pass

MOBILE BROWSERS
---------------
                    Control     Var 1       Var 2
iOS Safari          □ Pass      □ Pass      □ Pass
iOS Chrome          □ Pass      □ Pass      □ Pass
Android Chrome      □ Pass      □ Pass      □ Pass
Android Samsung     □ Pass      □ Pass      □ Pass

TABLET
------
                    Control     Var 1       Var 2
iPad Safari         □ Pass      □ Pass      □ Pass
Android Tablet      □ Pass      □ Pass      □ Pass

TEST CRITERIA
-------------
For each browser/variation, verify:
□ Page loads without errors
□ Variation changes visible
□ Interactive elements work
□ Forms submit correctly
□ Events track properly
□ No layout issues
□ No console errors

ISSUES FOUND
------------
Browser: [Browser name]
Variation: [Which one]
Issue: [Description]
Screenshot: [Link]
Priority: [High/Medium/Low]
Resolution: [How fixed]

EXAMPLE ISSUE:
Browser: Safari iOS 16
Variation: Variation 1
Issue: CTA button too small on iPhone SE
Screenshot: [link]
Priority: Medium
Resolution: Increased button padding from 8px to 12px",
                            SampleResponse = @"Testing priorities:

HIGH PRIORITY (Test first):
- Chrome Desktop (highest traffic)
- Safari iOS (iPhone users)
- Chrome Android

MEDIUM PRIORITY:
- Safari Desktop
- Firefox Desktop
- Edge Desktop

LOWER PRIORITY:
- Older browser versions
- Tablets
- Less common browsers

Pro tip: Check your analytics to see
your actual browser breakdown and
prioritize accordingly.",
                            Hints = new List<string>
                            {
                                "Use Chrome DevTools device mode for quick mobile testing",
                                "Always test on at least one real mobile device before launch"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "qa-checklist",
                    ModuleId = "qa-preview",
                    Title = "Pre-Launch Checklist",
                    Summary = "Complete pre-launch validation before going live.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Follow systematic QA process",
                        "Verify all experiment components",
                        "Get stakeholder approval",
                        "Launch with confidence"
                    },
                    Content = @"
<h2>Pre-Launch Checklist</h2>
<p>A comprehensive pre-launch checklist ensures you don't miss critical issues. Use this checklist <strong>every time</strong> before launching an experiment.</p>

<h3>Why Checklists Matter</h3>
<ul>
    <li>Prevent easily-avoidable mistakes</li>
    <li>Ensure consistent quality</li>
    <li>Document QA completion</li>
    <li>Build stakeholder confidence</li>
</ul>

<h3>Checklist Categories</h3>

<h4>1. Configuration</h4>
<ul>
    <li>Experiment name is descriptive</li>
    <li>Traffic allocation is correct</li>
    <li>Scheduling is set (if using)</li>
</ul>

<h4>2. Targeting</h4>
<ul>
    <li>URL targeting verified</li>
    <li>Audience conditions tested</li>
    <li>Exclusions configured (if needed)</li>
</ul>

<h4>3. Variations</h4>
<ul>
    <li>All variations tested</li>
    <li>Control unchanged</li>
    <li>No JavaScript errors</li>
</ul>

<h4>4. Metrics</h4>
<ul>
    <li>Primary metric attached</li>
    <li>Secondary metrics added</li>
    <li>Events tracking verified</li>
</ul>

<h4>5. Cross-Browser</h4>
<ul>
    <li>Desktop browsers tested</li>
    <li>Mobile browsers tested</li>
    <li>Issues documented/resolved</li>
</ul>

<h4>6. Stakeholder Review</h4>
<ul>
    <li>Preview links shared</li>
    <li>Feedback incorporated</li>
    <li>Approval received</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "qa-launch-checklist",
                            Title = "Complete Pre-Launch Checklist",
                            Description = "Copy and use for every experiment launch.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"EXPERIMENT PRE-LAUNCH CHECKLIST
================================

Experiment: _______________________
Owner: ___________________________
Target Launch Date: _______________

CONFIGURATION
-------------
□ Experiment name is clear and follows naming convention
□ Traffic allocation is set correctly (default: 50/50)
□ No holdback unless intentional
□ Mutual exclusion configured (if needed)
□ Schedule set (if time-limited)

TARGETING
---------
□ URL targeting tested on target pages
□ URL targeting tested on non-target pages (should NOT activate)
□ All audience conditions tested
□ AND/OR logic verified
□ Custom JS conditions handle edge cases

VARIATIONS
----------
□ Control is truly unchanged
□ Variation 1 changes verified
□ [Variation 2 changes verified]
□ [Variation 3 changes verified]
□ No variation affects non-targeted elements
□ Browser console shows no errors in any variation

METRICS & EVENTS
----------------
□ Primary metric is attached
□ Primary event tracking verified
□ Secondary metrics attached
□ All events firing correctly
□ Revenue tracking verified (if applicable)
□ Event names match exactly

CROSS-BROWSER TESTING
---------------------
□ Chrome Desktop - Pass
□ Safari Desktop - Pass
□ Firefox Desktop - Pass
□ iOS Safari - Pass
□ Android Chrome - Pass
□ [Other browsers per traffic analysis]

DOCUMENTATION
-------------
□ Hypothesis documented
□ Success criteria defined
□ Expected duration calculated
□ Sample size sufficient

STAKEHOLDER REVIEW
------------------
□ Preview links sent to stakeholders
□ Feedback received
□ Changes incorporated
□ Final approval received

FINAL CHECKS
------------
□ Experiment is set to NOT RUNNING (will change at launch)
□ All team members aware of launch
□ Monitoring plan in place for first 24 hours
□ Rollback plan documented

LAUNCH APPROVAL
---------------
QA Completed By: _________________ Date: _________
Approved By: ____________________ Date: _________

NOTES:
_____________________________________________________
_____________________________________________________",
                            SampleResponse = @"Launch confidence levels:

GREEN - READY TO LAUNCH:
□ All checklist items complete
□ No open issues
□ Stakeholder approval received

YELLOW - NEEDS REVIEW:
□ Minor issues documented but accepted
□ Some browsers untested (low traffic)
□ Conditional approval

RED - DO NOT LAUNCH:
□ Critical issues unresolved
□ Events not tracking
□ Major browser compatibility problems
□ No stakeholder approval

Never launch on RED status.
Yellow requires documented acceptance.",
                            Hints = new List<string>
                            {
                                "Save this checklist as a template in your project management tool",
                                "Don't skip the checklist even for 'simple' experiments"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 14: Project JavaScript and Extensions

    private LearningModule BuildProjectJavaScriptModule()
    {
        return new LearningModule
        {
            Id = "project-js",
            Title = "Project JavaScript and Extensions",
            Description = "Extend Optimizely with Project JavaScript, helpers, and custom extensions.",
            Icon = "puzzle-piece",
            Order = 14,
            Difficulty = ModuleDifficulty.Advanced,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pjs-fundamentals",
                    ModuleId = "project-js",
                    Title = "Project JavaScript Fundamentals",
                    Summary = "Understand and use Project JavaScript effectively.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand when Project JS runs",
                        "Add global functionality across experiments",
                        "Create reusable utilities",
                        "Manage Project JS safely"
                    },
                    Content = @"
<h2>Project JavaScript Fundamentals</h2>
<p>Project JavaScript runs on <strong>every page</strong> where the Optimizely snippet is installed, making it the place for global utilities and setup code.</p>

<h3>What is Project JavaScript?</h3>
<p>Code that runs for all visitors on all pages, before any experiments execute.</p>

<h3>When Project JS Runs</h3>
<ol>
    <li>Optimizely snippet loads</li>
    <li>Project JavaScript executes</li>
    <li>Page targeting is evaluated</li>
    <li>Experiments activate</li>
    <li>Variation code runs</li>
</ol>

<h3>Use Cases for Project JS</h3>
<ul>
    <li><strong>Utility functions</strong> - Shared helpers for experiments</li>
    <li><strong>Analytics integration</strong> - Send data to analytics on every experiment</li>
    <li><strong>Global listeners</strong> - Track events across all experiments</li>
    <li><strong>Polyfills</strong> - Add missing browser functionality</li>
    <li><strong>Custom attributes</strong> - Set visitor attributes for targeting</li>
</ul>

<h3>Best Practices</h3>
<ul>
    <li><strong>Keep it lightweight</strong> - This runs on every page</li>
    <li><strong>Use namespacing</strong> - Avoid global variable collisions</li>
    <li><strong>Handle errors</strong> - Don't break the page if something fails</li>
    <li><strong>Test thoroughly</strong> - Bugs affect all visitors</li>
</ul>

<h3>Accessing Project JS</h3>
<p>In Optimizely:</p>
<ol>
    <li>Go to Settings → Implementation</li>
    <li>Click ""Project JavaScript""</li>
    <li>Add your code</li>
    <li>Save and publish</li>
</ol>

<h3>Project JS vs Experiment Code</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Project JS</th>
            <th class=""px-4 py-2 text-left"">Experiment Code</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Runs on all pages</td><td class=""px-4 py-2"">Runs on targeted pages</td></tr>
        <tr><td class=""px-4 py-2"">Runs for all visitors</td><td class=""px-4 py-2"">Runs for bucketed visitors</td></tr>
        <tr><td class=""px-4 py-2"">Before experiments</td><td class=""px-4 py-2"">After bucketing</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "pjs-example",
                            Title = "Project JavaScript Examples",
                            Description = "Common Project JavaScript patterns.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// PROJECT JAVASCRIPT EXAMPLES

// Namespace for all custom utilities
window.OptimizelyUtils = window.OptimizelyUtils || {};

// === UTILITY: Wait for Element ===
OptimizelyUtils.waitForElement = function(selector, callback, timeout) {
    timeout = timeout || 5000;
    var startTime = Date.now();

    var check = function() {
        var element = document.querySelector(selector);
        if (element) {
            callback(element);
        } else if (Date.now() - startTime < timeout) {
            requestAnimationFrame(check);
        } else {
            console.warn('OptimizelyUtils: Element not found:', selector);
        }
    };
    check();
};

// === UTILITY: Safe JSON Parse ===
OptimizelyUtils.safeJsonParse = function(str, fallback) {
    try {
        return JSON.parse(str);
    } catch (e) {
        return fallback || null;
    }
};

// === GLOBAL: Track All Experiment Activations ===
window.optimizely.push({
    type: 'addListener',
    filter: { type: 'lifecycle', name: 'activated' },
    handler: function(event) {
        // Send to Google Analytics
        if (typeof gtag !== 'undefined') {
            gtag('event', 'experiment_activated', {
                experiment_id: event.data.campaign.id,
                experiment_name: event.data.campaign.name,
                variation_id: event.data.variation.id,
                variation_name: event.data.variation.name
            });
        }
    }
});

// === GLOBAL: Set Custom Attributes for Targeting ===
// Get user data from your site and make available for targeting
var userData = window.userData || {};
window.optimizely.push({
    type: 'user',
    attributes: {
        customerType: userData.type || 'anonymous',
        cartValue: userData.cartValue || 0,
        pageCount: parseInt(sessionStorage.getItem('pageCount') || 0) + 1
    }
});

// Track page count
sessionStorage.setItem('pageCount',
    parseInt(sessionStorage.getItem('pageCount') || 0) + 1);",
                            SampleResponse = @"Project JS best practices:

1. NAMESPACE EVERYTHING
   Use window.OptimizelyUtils or similar
   to avoid collisions with site code

2. HANDLE ERRORS
   Wrap risky code in try/catch
   Don't break the page

3. KEEP IT LIGHT
   This runs on every page load
   Heavy code = slow pages

4. TEST ON STAGING
   Publish to staging first
   Test thoroughly before production

5. DOCUMENT YOUR CODE
   Future you will thank you
   Include usage examples",
                            Hints = new List<string>
                            {
                                "Project JS bugs affect ALL visitors - test thoroughly!",
                                "Use feature detection, not browser detection"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "pjs-helpers",
                    ModuleId = "project-js",
                    Title = "Custom Helpers and Utilities",
                    Summary = "Build reusable helper functions for experiments.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create element waiting utilities",
                        "Build DOM manipulation helpers",
                        "Implement polling functions",
                        "Share code across experiments"
                    },
                    Content = @"
<h2>Custom Helpers and Utilities</h2>
<p>Building a library of reusable helpers makes experiment development <strong>faster and more reliable</strong>.</p>

<h3>Essential Helpers</h3>

<h4>1. Wait for Element</h4>
<p>The most common need - waiting for dynamically loaded elements.</p>

<h4>2. DOM Ready Check</h4>
<p>Ensure DOM is ready before manipulating.</p>

<h4>3. Poll Until True</h4>
<p>Wait for any condition to become true.</p>

<h4>4. Cookie Helpers</h4>
<p>Get, set, and delete cookies easily.</p>

<h4>5. URL Parameter Helpers</h4>
<p>Read and modify query parameters.</p>

<h3>Helper Design Principles</h3>
<ul>
    <li><strong>Single responsibility</strong> - Each helper does one thing</li>
    <li><strong>Error handling</strong> - Fail gracefully, log warnings</li>
    <li><strong>Timeout protection</strong> - Don't poll forever</li>
    <li><strong>Callback-based</strong> - Support async operations</li>
</ul>

<h3>Using Helpers in Variations</h3>
<pre><code>// In variation code, use your Project JS helpers:
OptimizelyUtils.waitForElement('.hero-title', function(el) {
    el.textContent = 'New Headline';
});</code></pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "pjs-helpers-example",
                            Title = "Complete Helper Library",
                            Description = "Comprehensive helper functions for experiments.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// OPTIMIZELY HELPER LIBRARY
// Add this to Project JavaScript

window.OptimizelyUtils = window.OptimizelyUtils || {};

// === WAIT FOR ELEMENT ===
// Usage: OptimizelyUtils.waitForElement('.selector', function(el) { ... });
OptimizelyUtils.waitForElement = function(selector, callback, options) {
    options = options || {};
    var timeout = options.timeout || 10000;
    var interval = options.interval || 50;
    var startTime = Date.now();

    function check() {
        var elements = document.querySelectorAll(selector);
        if (elements.length > 0) {
            callback(options.all ? elements : elements[0]);
        } else if (Date.now() - startTime < timeout) {
            setTimeout(check, interval);
        } else if (options.onTimeout) {
            options.onTimeout();
        }
    }
    check();
};

// === POLL UNTIL TRUE ===
// Usage: OptimizelyUtils.pollUntil(function() { return condition; }, callback);
OptimizelyUtils.pollUntil = function(condition, callback, options) {
    options = options || {};
    var timeout = options.timeout || 10000;
    var interval = options.interval || 100;
    var startTime = Date.now();

    function check() {
        try {
            if (condition()) {
                callback();
            } else if (Date.now() - startTime < timeout) {
                setTimeout(check, interval);
            } else if (options.onTimeout) {
                options.onTimeout();
            }
        } catch (e) {
            console.warn('OptimizelyUtils.pollUntil error:', e);
        }
    }
    check();
};

// === OBSERVE DOM CHANGES ===
// Usage: OptimizelyUtils.observeDOM(targetSelector, callback);
OptimizelyUtils.observeDOM = function(targetSelector, callback, options) {
    options = options || {};
    var observerOptions = {
        childList: true,
        subtree: options.subtree !== false,
        attributes: options.attributes || false
    };

    OptimizelyUtils.waitForElement(targetSelector, function(target) {
        var observer = new MutationObserver(function(mutations) {
            callback(mutations, observer);
        });
        observer.observe(target, observerOptions);
    });
};

// === COOKIE HELPERS ===
OptimizelyUtils.cookies = {
    get: function(name) {
        var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
        return match ? decodeURIComponent(match[2]) : null;
    },
    set: function(name, value, days) {
        var expires = '';
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = '; expires=' + date.toUTCString();
        }
        document.cookie = name + '=' + encodeURIComponent(value) + expires + '; path=/';
    },
    delete: function(name) {
        this.set(name, '', -1);
    }
};

// === URL PARAMETER HELPERS ===
OptimizelyUtils.params = {
    get: function(name) {
        var params = new URLSearchParams(window.location.search);
        return params.get(name);
    },
    getAll: function() {
        var params = new URLSearchParams(window.location.search);
        var result = {};
        params.forEach(function(value, key) {
            result[key] = value;
        });
        return result;
    }
};",
                            SampleResponse = @"Using helpers in variation code:

// Wait for element, then modify
OptimizelyUtils.waitForElement('.hero-title', function(el) {
    el.textContent = 'New Headline';
    el.style.color = 'blue';
});

// Wait for multiple elements
OptimizelyUtils.waitForElement('.product-card', function(cards) {
    cards.forEach(function(card) {
        // Modify each card
    });
}, { all: true });

// Poll until data is available
OptimizelyUtils.pollUntil(
    function() { return window.userData != null; },
    function() {
        console.log('User data loaded:', window.userData);
    }
);

// Observe DOM for changes (SPAs)
OptimizelyUtils.observeDOM('.product-list', function() {
    // Re-apply changes when products update
});",
                            Hints = new List<string>
                            {
                                "Always include timeout protection in helpers",
                                "Test helpers with elements that don't exist to verify error handling"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "pjs-extensions",
                    ModuleId = "project-js",
                    Title = "Extensions and Custom Integrations",
                    Summary = "Build custom extensions to enhance Optimizely functionality.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create custom audience condition types",
                        "Build analytics integration extensions",
                        "Extend the JavaScript API",
                        "Package and reuse extensions"
                    },
                    Content = @"
<h2>Extensions and Custom Integrations</h2>
<p>Extensions let you add <strong>custom functionality</strong> to Optimizely that goes beyond the built-in features.</p>

<h3>Types of Extensions</h3>

<h4>1. Custom Integrations</h4>
<p>Connect Optimizely to external systems:</p>
<ul>
    <li>Analytics platforms</li>
    <li>CDPs and CRMs</li>
    <li>Heatmap tools</li>
    <li>A/B testing aggregators</li>
</ul>

<h4>2. Custom Audience Conditions</h4>
<p>Create reusable targeting logic:</p>
<ul>
    <li>Company data lookups</li>
    <li>Complex user state checks</li>
    <li>External API-based targeting</li>
</ul>

<h4>3. Utility Extensions</h4>
<p>Enhance development workflow:</p>
<ul>
    <li>Debugging tools</li>
    <li>Preview enhancements</li>
    <li>Code generators</li>
</ul>

<h3>Building Custom Integrations</h3>
<p>The basic pattern for integrations:</p>
<ol>
    <li>Listen for Optimizely lifecycle events</li>
    <li>Extract experiment/variation data</li>
    <li>Send to external system</li>
    <li>Handle errors gracefully</li>
</ol>

<h3>Building Custom Conditions</h3>
<p>Create conditions that can be reused across experiments:</p>
<ol>
    <li>Define the condition function</li>
    <li>Add to Project JavaScript</li>
    <li>Reference in audience targeting</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "pjs-extension-example",
                            Title = "Custom Extensions Examples",
                            Description = "Building custom integrations and conditions.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// CUSTOM EXTENSION EXAMPLES

// === CUSTOM INTEGRATION: Send to Multiple Analytics ===
window.optimizely.push({
    type: 'addListener',
    filter: { type: 'lifecycle', name: 'activated' },
    handler: function(event) {
        var data = {
            experimentId: event.data.campaign.id,
            experimentName: event.data.campaign.name,
            variationId: event.data.variation.id,
            variationName: event.data.variation.name,
            timestamp: Date.now()
        };

        // Google Analytics 4
        if (typeof gtag !== 'undefined') {
            gtag('event', 'experiment_impression', data);
        }

        // Amplitude
        if (typeof amplitude !== 'undefined') {
            amplitude.track('Experiment Viewed', data);
        }

        // Mixpanel
        if (typeof mixpanel !== 'undefined') {
            mixpanel.track('Experiment Viewed', data);
        }

        // Segment
        if (typeof analytics !== 'undefined') {
            analytics.track('Experiment Viewed', data);
        }

        // Custom data layer
        window.dataLayer = window.dataLayer || [];
        window.dataLayer.push({
            event: 'optimizelyActivated',
            ...data
        });
    }
});

// === CUSTOM CONDITION: Company Size Targeting ===
// Add to Project JS, then use in audience custom JS
window.OptimizelyConditions = window.OptimizelyConditions || {};

OptimizelyConditions.isEnterpriseCompany = function() {
    // Check company data from your enrichment service
    var companyData = window.companyData || {};
    return companyData.employees > 1000 ||
           companyData.revenue > 100000000;
};

OptimizelyConditions.isIndustry = function(industries) {
    var companyData = window.companyData || {};
    return industries.includes(companyData.industry);
};

// Usage in audience custom JS:
// OptimizelyConditions.isEnterpriseCompany()
// OptimizelyConditions.isIndustry(['Technology', 'Finance'])

// === CUSTOM CONDITION: Cart Value Targeting ===
OptimizelyConditions.cartValueAbove = function(threshold) {
    var cart = window.shoppingCart || {};
    return (cart.total || 0) > threshold;
};

// === DEBUG EXTENSION: Log All Decisions ===
if (window.location.search.includes('optimizely_debug=true')) {
    window.optimizely.push({
        type: 'addListener',
        filter: { type: 'lifecycle' },
        handler: function(event) {
            console.log('%c[Optimizely Debug]', 'color: blue; font-weight: bold',
                event.name, event.data);
        }
    });
}",
                            SampleResponse = @"Extension architecture:

1. LISTENER PATTERN
   - Subscribe to Optimizely events
   - React to experiment activation
   - Send data to external systems

2. CONDITION PATTERN
   - Define reusable functions
   - Store in namespace
   - Reference in audience targeting

3. UTILITY PATTERN
   - Extend OptimizelyUtils
   - Provide helper functions
   - Use in variation code

Best practices:
- Namespace all code
- Handle missing dependencies
- Log errors but don't break
- Test in isolation first",
                            Hints = new List<string>
                            {
                                "Test integrations with network tab to verify data is sent",
                                "Use debug mode to see extension behavior in real-time"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "pjs-performance",
                    ModuleId = "project-js",
                    Title = "Performance Optimization",
                    Summary = "Optimize JavaScript for minimal performance impact.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Minimize snippet performance impact",
                        "Optimize variation code execution",
                        "Handle async operations efficiently",
                        "Profile and measure performance"
                    },
                    Content = @"
<h2>Performance Optimization</h2>
<p>Poorly optimized experiment code can <strong>slow down your site</strong>, affecting user experience and potentially skewing results.</p>

<h3>Performance Impact Areas</h3>

<h4>1. Snippet Load Time</h4>
<ul>
    <li>Snippet is typically 50-200KB (gzipped)</li>
    <li>Loads synchronously to prevent flashing</li>
    <li>Blocks rendering until complete</li>
</ul>

<h4>2. Project JavaScript</h4>
<ul>
    <li>Runs on every page load</li>
    <li>Heavy code affects all visitors</li>
    <li>Should be minimal and fast</li>
</ul>

<h4>3. Variation Code</h4>
<ul>
    <li>Runs after bucketing</li>
    <li>Can use sync or async execution</li>
    <li>DOM manipulation can be slow</li>
</ul>

<h3>Optimization Best Practices</h3>

<h4>Minimize Synchronous Code</h4>
<ul>
    <li>Keep sync code under 100ms</li>
    <li>Move heavy operations to async</li>
    <li>Use requestAnimationFrame for DOM changes</li>
</ul>

<h4>Efficient DOM Operations</h4>
<ul>
    <li>Batch DOM reads and writes</li>
    <li>Use documentFragment for multiple insertions</li>
    <li>Avoid layout thrashing</li>
</ul>

<h4>Smart Polling</h4>
<ul>
    <li>Use MutationObserver instead of setInterval</li>
    <li>Set reasonable timeouts</li>
    <li>Clean up observers when done</li>
</ul>

<h3>Measuring Performance</h3>
<p>Use browser tools to measure impact:</p>
<ul>
    <li>Performance tab - Timeline analysis</li>
    <li>Lighthouse - Performance audit</li>
    <li>Web Vitals - Core metrics</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "pjs-perf-example",
                            Title = "Performance Optimization Techniques",
                            Description = "Code patterns for optimal performance.",
                            Type = ExampleType.Code,
                            ExampleContent = @"// PERFORMANCE OPTIMIZATION PATTERNS

// === BAD: Layout Thrashing ===
// Don't do this - reads and writes interleaved
function badExample() {
    var elements = document.querySelectorAll('.product');
    elements.forEach(function(el) {
        var height = el.offsetHeight;    // READ
        el.style.height = height + 'px'; // WRITE
        var width = el.offsetWidth;      // READ (forces reflow!)
        el.style.width = width + 'px';   // WRITE
    });
}

// === GOOD: Batched Reads/Writes ===
function goodExample() {
    var elements = document.querySelectorAll('.product');
    var measurements = [];

    // Batch all reads first
    elements.forEach(function(el) {
        measurements.push({
            el: el,
            height: el.offsetHeight,
            width: el.offsetWidth
        });
    });

    // Then batch all writes
    measurements.forEach(function(m) {
        m.el.style.height = m.height + 'px';
        m.el.style.width = m.width + 'px';
    });
}

// === EFFICIENT: Use requestAnimationFrame ===
function efficientDOMChange(element, changes) {
    requestAnimationFrame(function() {
        Object.keys(changes).forEach(function(prop) {
            element.style[prop] = changes[prop];
        });
    });
}

// === MutationObserver vs Polling ===
// BAD: Polling with setInterval
var badPoll = setInterval(function() {
    if (document.querySelector('.dynamic-element')) {
        // do something
        clearInterval(badPoll);
    }
}, 100); // Runs every 100ms even when not needed

// GOOD: MutationObserver (event-driven)
var observer = new MutationObserver(function(mutations, obs) {
    var element = document.querySelector('.dynamic-element');
    if (element) {
        // do something
        obs.disconnect(); // Clean up
    }
});
observer.observe(document.body, { childList: true, subtree: true });

// === MEASURING PERFORMANCE ===
// Add to Project JS for debugging
if (window.location.search.includes('optimizely_perf=true')) {
    var start = performance.now();

    window.optimizely.push({
        type: 'addListener',
        filter: { type: 'lifecycle', name: 'activated' },
        handler: function(event) {
            var duration = performance.now() - start;
            console.log('Optimizely activation time:', duration.toFixed(2), 'ms');
            console.log('Experiment:', event.data.campaign.name);
        }
    });
}

// === ASYNC HEAVY OPERATIONS ===
// Move heavy work out of sync execution
function heavyOperation() {
    // Bad: Heavy sync operation blocks page
    // processLargeData(data);

    // Good: Use setTimeout to defer
    setTimeout(function() {
        processLargeData(data);
    }, 0);

    // Better: Use requestIdleCallback if available
    if ('requestIdleCallback' in window) {
        requestIdleCallback(function() {
            processLargeData(data);
        });
    }
}",
                            SampleResponse = @"Performance checklist:

SNIPPET LOADING:
□ Snippet in <head> (required)
□ No render-blocking before snippet
□ CDN caching enabled

PROJECT JAVASCRIPT:
□ Code is minimal (<50 lines)
□ No heavy computations
□ Event listeners are efficient
□ Errors are caught

VARIATION CODE:
□ Sync code <100ms
□ Heavy work is async
□ DOM operations batched
□ Observers cleaned up

MEASUREMENT:
□ Lighthouse score checked
□ Core Web Vitals monitored
□ No regressions from experiments",
                            Hints = new List<string>
                            {
                                "Use Chrome DevTools Performance tab to profile experiment code",
                                "Test with CPU throttling to catch performance issues early"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion
}
