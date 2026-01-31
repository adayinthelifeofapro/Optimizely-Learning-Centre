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
            BuildRestAPIModule()
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
                }
            }
        };
    }

    #endregion
}
