using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.FeatureExp;

/// <summary>
/// Content provider for the Optimizely Feature Experimentation course
/// </summary>
public class FeatureExpContentProvider : ILearningContentProvider
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
            BuildFeatureFlagsModule(),
            BuildSdkImplementationModule(),
            BuildUserContextTargetingModule(),
            BuildDecideMethodModule(),
            BuildABTestingModule(),
            BuildTargetedRolloutsModule(),
            BuildMultiArmedBanditsModule(),
            BuildContextualBanditsModule(),
            BuildEventsAnalyticsModule(),
            BuildAdvancedTopicsModule(),
            BuildBestPracticesModule()
        };
    }

    #region Module 1: Getting Started

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started",
            Description = "Learn the fundamentals of Optimizely Feature Experimentation, core concepts, and how to set up your account.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-feature-exp",
                    ModuleId = "getting-started",
                    Title = "What is Feature Experimentation?",
                    Summary = "Discover Optimizely Feature Experimentation and its capabilities for feature flags and A/B testing.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely Feature Experimentation is and its purpose",
                        "Learn the key benefits of feature flags and experimentation",
                        "Understand the difference between Feature Experimentation and Web Experimentation",
                        "Know when to use Feature Experimentation in your projects"
                    },
                    Content = @"
<h2>Introduction to Optimizely Feature Experimentation</h2>
<p>Optimizely Feature Experimentation is a <strong>powerful platform for feature flags and product experimentation</strong> that enables development teams to deploy code safely, run A/B tests, and deliver personalized experiences at scale.</p>

<h3>What is Feature Experimentation?</h3>
<p>Feature Experimentation (formerly known as Optimizely Full Stack) provides a comprehensive SDK-based experimentation solution that integrates directly into your application code. Unlike web-based experimentation tools that modify the DOM, Feature Experimentation works at the code level, giving you complete control over feature delivery and experimentation.</p>

<h3>Key Capabilities</h3>
<ul>
    <li><strong>Feature Flags</strong> - Deploy code behind feature flags to control when and to whom features are released</li>
    <li><strong>A/B Testing</strong> - Run experiments to measure the impact of changes before full rollout</li>
    <li><strong>Targeted Rollouts</strong> - Gradually release features to specific audiences or percentages of users</li>
    <li><strong>Multi-Armed Bandits</strong> - Automatically optimize traffic allocation to best-performing variations</li>
    <li><strong>Remote Configuration</strong> - Modify feature behaviour without deploying new code</li>
</ul>

<h3>Benefits of Feature Experimentation</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Benefit</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Safe Deployments</td><td class=""px-4 py-2"">Deploy code with confidence knowing you can instantly disable features</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Data-Driven Decisions</td><td class=""px-4 py-2"">Make decisions based on real user behaviour and statistical analysis</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Reduced Risk</td><td class=""px-4 py-2"">Test changes with small user groups before full rollout</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Faster Iteration</td><td class=""px-4 py-2"">Decouple deployment from release to ship faster</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Near-Zero Latency</td><td class=""px-4 py-2"">SDK-based decisions happen locally with microsecond performance</td></tr>
    </tbody>
</table>

<h3>Feature Experimentation vs Web Experimentation</h3>
<p>Optimizely offers two experimentation products. Here's how they differ:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Feature Experimentation</th>
            <th class=""px-4 py-2 text-left"">Web Experimentation</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Integration</td><td class=""px-4 py-2"">SDK in your code</td><td class=""px-4 py-2"">JavaScript snippet in browser</td></tr>
        <tr><td class=""px-4 py-2"">Platforms</td><td class=""px-4 py-2"">Any (web, mobile, server, IoT)</td><td class=""px-4 py-2"">Web browsers only</td></tr>
        <tr><td class=""px-4 py-2"">Changes</td><td class=""px-4 py-2"">Code-level changes</td><td class=""px-4 py-2"">Visual/DOM changes</td></tr>
        <tr><td class=""px-4 py-2"">Control</td><td class=""px-4 py-2"">Developer-controlled</td><td class=""px-4 py-2"">Marketer-friendly</td></tr>
        <tr><td class=""px-4 py-2"">Use Cases</td><td class=""px-4 py-2"">Feature releases, backend tests, algorithms</td><td class=""px-4 py-2"">UI/UX tests, copy changes, layouts</td></tr>
    </tbody>
</table>

<h3>When to Use Feature Experimentation</h3>
<ul>
    <li>You want to test backend logic, algorithms, or APIs</li>
    <li>You need feature flags for safe deployments</li>
    <li>You're building mobile apps, server-side applications, or IoT devices</li>
    <li>You want consistent bucketing across platforms</li>
    <li>You need to experiment in microservices architectures</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "gs-core-concepts",
                    ModuleId = "getting-started",
                    Title = "Core Concepts Overview",
                    Summary = "Learn the fundamental concepts: flags, variations, rules, bucketing, and the datafile.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what feature flags are and how they work",
                        "Learn about variations and variables",
                        "Understand rules and how they control flag delivery",
                        "Learn how bucketing ensures consistent user experiences"
                    },
                    Content = @"
<h2>Core Concepts of Feature Experimentation</h2>
<p>Before diving into implementation, it's essential to understand the core concepts that make up the Feature Experimentation platform.</p>

<h3>Feature Flags</h3>
<p>A <strong>feature flag</strong> (also called a feature toggle) is the fundamental building block. It represents a feature in your application that can be turned on or off, or configured with different settings.</p>
<p>Feature flags are more than simple on/off toggles:</p>
<ul>
    <li>They can have multiple <strong>variations</strong> with different configurations</li>
    <li>They contain <strong>variables</strong> for remote configuration</li>
    <li>They are controlled by <strong>rules</strong> that determine who sees what</li>
</ul>

<h3>Variations</h3>
<p>A <strong>variation</strong> represents a specific version or configuration of a feature flag. Each flag has at least two variations:</p>
<ul>
    <li><strong>Off variation</strong> - The default when the flag is disabled</li>
    <li><strong>On variation(s)</strong> - One or more variations when the flag is enabled</li>
</ul>
<p>For example, a ""checkout_flow"" flag might have variations for the ""classic"" checkout and a ""streamlined"" checkout.</p>

<h3>Variables</h3>
<p><strong>Variables</strong> are configurable parameters attached to variations. They allow you to change feature behaviour without modifying code or creating new variations.</p>
<p>Variable types include:</p>
<ul>
    <li><strong>Boolean</strong> - true/false values</li>
    <li><strong>String</strong> - text values</li>
    <li><strong>Integer</strong> - whole numbers</li>
    <li><strong>Double</strong> - decimal numbers</li>
    <li><strong>JSON</strong> - complex structured data</li>
</ul>

<h3>Rules</h3>
<p><strong>Rules</strong> define how flags are delivered to users. There are three types of rules:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Rule Type</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">A/B Test</td><td class=""px-4 py-2"">Randomly assign users to variations to measure impact</td><td class=""px-4 py-2"">Testing which checkout flow converts better</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Targeted Delivery</td><td class=""px-4 py-2"">Roll out a specific variation to targeted users</td><td class=""px-4 py-2"">Release new feature to beta users first</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Multi-Armed Bandit</td><td class=""px-4 py-2"">Automatically shift traffic to best-performing variation</td><td class=""px-4 py-2"">Optimizing headline text for clicks</td></tr>
    </tbody>
</table>

<h3>Bucketing</h3>
<p><strong>Bucketing</strong> is the process of assigning a user to a specific variation. Optimizely uses <strong>deterministic bucketing</strong> with the MurmurHash3 algorithm, which means:</p>
<ul>
    <li>The same user ID always gets the same variation</li>
    <li>Results are consistent across different servers and SDKs</li>
    <li>No need to store assignment state externally</li>
</ul>

<h3>User Context</h3>
<p>A <strong>user context</strong> represents the user for whom you're making decisions. It includes:</p>
<ul>
    <li><strong>User ID</strong> - A unique identifier for the user</li>
    <li><strong>Attributes</strong> - Additional data about the user (location, device, plan, etc.)</li>
</ul>

<h3>Impressions and Decisions</h3>
<ul>
    <li><strong>Decision</strong> - The outcome of evaluating a flag for a user (which variation they receive)</li>
    <li><strong>Impression</strong> - An event recorded when a user qualifies for a rule and receives a decision</li>
</ul>

<h3>Datafile</h3>
<p>The <strong>datafile</strong> is a JSON file containing all your project's configuration:</p>
<ul>
    <li>All feature flags and their variations</li>
    <li>All rules and audience definitions</li>
    <li>All events for tracking</li>
</ul>
<p>SDKs use this datafile to make decisions locally without network calls, enabling near-zero latency.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "concept-flag-structure",
                            Title = "Conceptual Flag Structure",
                            Description = "Visual representation of how a feature flag is structured",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Feature Flag: ""checkout_redesign""
├── Variations
│   ├── ""off"" (default when disabled)
│   ├── ""control"" (original checkout)
│   └── ""treatment"" (new streamlined checkout)
│
├── Variables
│   ├── button_color (string): ""blue""
│   ├── show_progress_bar (boolean): true
│   └── max_items (integer): 10
│
└── Rules
    ├── Rule 1: A/B Test (50% control, 50% treatment)
    │   └── Audience: All users
    └── Rule 2: Targeted Delivery (100% treatment)
        └── Audience: Beta testers",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-datafile-deep-dive",
                    ModuleId = "getting-started",
                    Title = "Understanding the Datafile",
                    Summary = "Deep dive into the datafile - the configuration that powers your experiments.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what the datafile contains",
                        "Learn how the datafile is delivered and updated",
                        "Know how SDKs use the datafile for decisions",
                        "Understand datafile versioning and caching"
                    },
                    Content = @"
<h2>The Datafile Explained</h2>
<p>The <strong>datafile</strong> is the heart of Feature Experimentation's architecture. It's a JSON document that contains your entire project configuration, enabling SDKs to make decisions locally without network calls.</p>

<h3>What's in the Datafile?</h3>
<p>Each environment in your project has its own datafile containing:</p>
<ul>
    <li><strong>Feature flags</strong> - All flags with their variations and variables</li>
    <li><strong>Experiments</strong> - A/B tests with traffic allocation</li>
    <li><strong>Audiences</strong> - Targeting definitions</li>
    <li><strong>Events</strong> - Conversion events for tracking</li>
    <li><strong>Attributes</strong> - User attribute definitions</li>
    <li><strong>Revision number</strong> - Version tracking</li>
</ul>

<h3>How Datafile Delivery Works</h3>
<ol>
    <li>You configure flags and experiments in the Optimizely app</li>
    <li>Changes are compiled into a new datafile</li>
    <li>The datafile is published to Optimizely's CDN (cdn.optimizely.com)</li>
    <li>Your SDK fetches the latest datafile on initialization</li>
    <li>Decisions are made locally using the cached datafile</li>
</ol>

<h3>Datafile URL Format</h3>
<p>Each environment's datafile has a unique URL:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto"">
https://cdn.optimizely.com/datafiles/{SDK_KEY}.json
</pre>

<h3>Why Local Decisions Matter</h3>
<p>Because the SDK makes decisions using the local datafile:</p>
<ul>
    <li><strong>Near-zero latency</strong> - No network round-trip for decisions</li>
    <li><strong>Offline support</strong> - Works even without internet connectivity</li>
    <li><strong>Consistent performance</strong> - Not affected by network conditions</li>
    <li><strong>Reduced infrastructure load</strong> - No decision API to scale</li>
</ul>

<h3>Datafile Updates</h3>
<p>SDKs can be configured to update the datafile automatically:</p>
<ul>
    <li><strong>Polling</strong> - Periodically check for updates (configurable interval)</li>
    <li><strong>Webhooks</strong> - Push-based notifications when datafile changes</li>
    <li><strong>Manual</strong> - You control when to fetch updates</li>
</ul>

<h3>Environments</h3>
<p>Each project can have multiple environments (e.g., Development, Staging, Production), each with its own:</p>
<ul>
    <li>Separate datafile</li>
    <li>Unique SDK key</li>
    <li>Independent flag rules and traffic allocation</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "sample-datafile",
                            Title = "Sample Datafile Structure",
                            Description = "Simplified example of a datafile's JSON structure",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""version"": ""4"",
  ""revision"": ""42"",
  ""projectId"": ""12345678"",
  ""sdkKey"": ""YOUR_SDK_KEY"",
  ""environmentKey"": ""production"",
  ""featureFlags"": [
    {
      ""id"": ""1"",
      ""key"": ""checkout_redesign"",
      ""variations"": [
        { ""id"": ""100"", ""key"": ""off"", ""featureEnabled"": false },
        { ""id"": ""101"", ""key"": ""control"", ""featureEnabled"": true },
        { ""id"": ""102"", ""key"": ""treatment"", ""featureEnabled"": true }
      ],
      ""variables"": [
        { ""id"": ""200"", ""key"": ""button_color"", ""type"": ""string"", ""defaultValue"": ""blue"" }
      ]
    }
  ],
  ""audiences"": [...],
  ""events"": [...],
  ""attributes"": [...]
}",
                            IsInteractive = false,
                            SampleResponse = @"Note: This is a simplified representation.
Actual datafiles contain additional metadata
for traffic allocation, experiments, and
rule evaluation."
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-sdk-architecture",
                    ModuleId = "getting-started",
                    Title = "SDK Architecture",
                    Summary = "Understand the difference between client-side and server-side SDKs.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the role of SDKs in Feature Experimentation",
                        "Know the difference between client-side and server-side SDKs",
                        "Learn when to use each type of SDK",
                        "Understand the available SDK options"
                    },
                    Content = @"
<h2>SDK Architecture Overview</h2>
<p>Optimizely Feature Experimentation provides SDKs for virtually every platform. Understanding the SDK architecture helps you choose the right approach for your application.</p>

<h3>The Role of SDKs</h3>
<p>SDKs are responsible for:</p>
<ul>
    <li>Fetching and caching the datafile</li>
    <li>Evaluating flag rules and making decisions</li>
    <li>Providing consistent bucketing across sessions</li>
    <li>Tracking events and impressions</li>
    <li>Sending analytics data to Optimizely</li>
</ul>

<h3>Server-Side vs Client-Side SDKs</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Server-Side SDKs</th>
            <th class=""px-4 py-2 text-left"">Client-Side SDKs</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Execution</td><td class=""px-4 py-2"">Runs on your servers</td><td class=""px-4 py-2"">Runs in user's browser/app</td></tr>
        <tr><td class=""px-4 py-2"">Security</td><td class=""px-4 py-2"">SDK key hidden from users</td><td class=""px-4 py-2"">SDK key visible in code</td></tr>
        <tr><td class=""px-4 py-2"">User ID</td><td class=""px-4 py-2"">You provide the user ID</td><td class=""px-4 py-2"">Often uses device/session ID</td></tr>
        <tr><td class=""px-4 py-2"">Datafile</td><td class=""px-4 py-2"">Shared across all users</td><td class=""px-4 py-2"">Downloaded per user/session</td></tr>
        <tr><td class=""px-4 py-2"">Consistency</td><td class=""px-4 py-2"">Easier cross-platform consistency</td><td class=""px-4 py-2"">May need sync mechanisms</td></tr>
    </tbody>
</table>

<h3>Available SDKs</h3>
<h4>Server-Side SDKs</h4>
<ul>
    <li><strong>C# / .NET</strong> - For ASP.NET Core, .NET applications</li>
    <li><strong>Java</strong> - For Spring, backend services</li>
    <li><strong>Python</strong> - For Django, Flask, data pipelines</li>
    <li><strong>Ruby</strong> - For Rails applications</li>
    <li><strong>Go</strong> - For Go services</li>
    <li><strong>PHP</strong> - For PHP applications</li>
    <li><strong>Node.js</strong> - For Express, Next.js API routes</li>
</ul>

<h4>Client-Side SDKs</h4>
<ul>
    <li><strong>JavaScript (Browser)</strong> - For web applications</li>
    <li><strong>React</strong> - React-specific with hooks and components</li>
    <li><strong>React Native</strong> - For cross-platform mobile</li>
    <li><strong>Swift</strong> - For iOS applications</li>
    <li><strong>Android</strong> - For Android applications</li>
    <li><strong>Flutter</strong> - For cross-platform mobile</li>
</ul>

<h3>Choosing the Right Approach</h3>
<p><strong>Use Server-Side SDKs when:</strong></p>
<ul>
    <li>Testing backend logic, APIs, or algorithms</li>
    <li>You need to hide experiment configuration from users</li>
    <li>You want decisions made before content reaches the client</li>
    <li>Working with microservices or backend systems</li>
</ul>

<p><strong>Use Client-Side SDKs when:</strong></p>
<ul>
    <li>Testing UI/UX changes in the browser</li>
    <li>Building mobile applications</li>
    <li>You don't have a backend (e.g., static sites)</li>
    <li>You need immediate updates without page reload</li>
</ul>

<p><strong>Use Both when:</strong></p>
<ul>
    <li>You need consistent experiments across web and mobile</li>
    <li>You have both frontend and backend experiments</li>
    <li>You want server-rendered pages with client interactivity</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "sdk-decision-flow",
                            Title = "SDK Decision Flow",
                            Description = "How an SDK makes a feature flag decision",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"SDK Decision Flow:

1. Application calls decide(""flag_key"", userContext)
          │
          ▼
2. SDK looks up flag in cached datafile
          │
          ▼
3. SDK evaluates targeting rules in order:
   ├── Does user match Rule 1 audience? → If yes, evaluate rule
   ├── Does user match Rule 2 audience? → If yes, evaluate rule
   └── Continue until match or end
          │
          ▼
4. For matching rule:
   ├── A/B Test: Hash user ID → Determine bucket → Return variation
   ├── Targeted Delivery: Return configured variation
   └── MAB: Use algorithm to select variation
          │
          ▼
5. SDK returns decision with:
   - enabled: true/false
   - variationKey: ""treatment""
   - variables: { button_color: ""green"" }
          │
          ▼
6. SDK queues impression event for analytics",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-account-setup",
                    ModuleId = "getting-started",
                    Title = "Setting Up Your Account",
                    Summary = "Create your Optimizely account and configure your first project.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Create an Optimizely Feature Experimentation account",
                        "Understand projects and environments",
                        "Find your SDK key",
                        "Navigate the Optimizely app interface"
                    },
                    Content = @"
<h2>Setting Up Your Optimizely Account</h2>
<p>To get started with Feature Experimentation, you need an Optimizely account. Optimizely offers a free tier called <strong>Optimizely Rollouts</strong> that includes feature flags and one A/B test.</p>

<h3>Creating Your Account</h3>
<ol>
    <li>Visit <a href=""https://www.optimizely.com/products/feature-experimentation/"" target=""_blank"">optimizely.com/products/feature-experimentation</a></li>
    <li>Click ""Get Started Free"" or ""Start for Free""</li>
    <li>Complete the registration form</li>
    <li>Verify your email address</li>
    <li>Log in to the Optimizely app</li>
</ol>

<h3>Understanding Projects</h3>
<p>A <strong>project</strong> is a container for your feature flags and experiments. You might have separate projects for:</p>
<ul>
    <li>Different applications (web app, mobile app, backend service)</li>
    <li>Different products or product lines</li>
    <li>Different teams or departments</li>
</ul>

<h3>Environments</h3>
<p>Each project has multiple <strong>environments</strong> (typically Development, Staging, Production). Each environment has:</p>
<ul>
    <li>Its own SDK key</li>
    <li>Its own datafile</li>
    <li>Independent flag configurations and traffic allocation</li>
</ul>
<p>This allows you to test flags in development without affecting production users.</p>

<h3>Finding Your SDK Key</h3>
<ol>
    <li>Log in to <a href=""https://app.optimizely.com"" target=""_blank"">app.optimizely.com</a></li>
    <li>Select your project</li>
    <li>Go to <strong>Settings</strong> (gear icon)</li>
    <li>Select <strong>Environments</strong></li>
    <li>Copy the <strong>SDK Key</strong> for your environment</li>
</ol>
<p>The SDK key looks like: <code>YOUR_SDK_KEY_HERE</code></p>

<h3>Navigating the Optimizely App</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Section</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Flags</td><td class=""px-4 py-2"">Create and manage feature flags</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Events</td><td class=""px-4 py-2"">Define conversion events for tracking</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Audiences</td><td class=""px-4 py-2"">Create targeting audiences</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Reports</td><td class=""px-4 py-2"">View experiment results</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Settings</td><td class=""px-4 py-2"">Project settings, environments, SDK keys</td></tr>
    </tbody>
</table>

<h3>Free Tier Limitations</h3>
<p>The free Optimizely Rollouts plan includes:</p>
<ul>
    <li>Unlimited feature flags</li>
    <li>One A/B test at a time</li>
    <li>Basic analytics</li>
</ul>
<p>Upgrade to a paid plan for unlimited experiments, advanced analytics, and enterprise features.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "sdk-key-usage",
                            Title = "Using Your SDK Key",
                            Description = "How to use the SDK key in your application",
                            Type = ExampleType.Code,
                            ExampleContent = @"// C# / .NET - Initialize with SDK Key
using OptimizelySDK;

// The SDK key identifies your project and environment
var sdkKey = ""YOUR_SDK_KEY"";

// Create the Optimizely client
var optimizely = OptimizelyFactory.NewDefaultInstance(sdkKey);

// Verify the client is ready
if (optimizely.IsValid)
{
    Console.WriteLine(""Optimizely client initialized successfully!"");
}
else
{
    Console.WriteLine(""Failed to initialize Optimizely client"");
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 2: Feature Flags Fundamentals

    private LearningModule BuildFeatureFlagsModule()
    {
        return new LearningModule
        {
            Id = "feature-flags",
            Title = "Feature Flags Fundamentals",
            Description = "Master the creation and management of feature flags, variations, and variables.",
            Icon = "flag",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ff-creating-first-flag",
                    ModuleId = "feature-flags",
                    Title = "Creating Your First Feature Flag",
                    Summary = "Learn to create and configure a feature flag in the Optimizely app.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create a new feature flag in the Optimizely app",
                        "Understand flag naming conventions and best practices",
                        "Configure basic flag settings",
                        "Enable and disable a flag"
                    },
                    Content = @"
<h2>Creating Your First Feature Flag</h2>
<p>Feature flags are the foundation of Optimizely Feature Experimentation. Let's walk through creating your first flag.</p>

<h3>Step-by-Step: Creating a Flag</h3>
<ol>
    <li>Log in to <a href=""https://app.optimizely.com"" target=""_blank"">app.optimizely.com</a></li>
    <li>Select your project</li>
    <li>Click <strong>Flags</strong> in the left navigation</li>
    <li>Click <strong>Create New Flag</strong></li>
    <li>Enter a <strong>Flag Key</strong> (e.g., <code>checkout_redesign</code>)</li>
    <li>Add an optional <strong>Description</strong></li>
    <li>Click <strong>Create Flag</strong></li>
</ol>

<h3>Flag Key Best Practices</h3>
<p>The flag key is a unique identifier used in your code. Follow these conventions:</p>
<ul>
    <li><strong>Use snake_case</strong> - e.g., <code>new_checkout_flow</code></li>
    <li><strong>Be descriptive</strong> - The key should indicate what the flag controls</li>
    <li><strong>Use prefixes</strong> - Group related flags: <code>checkout_</code>, <code>search_</code>, <code>profile_</code></li>
    <li><strong>Avoid version numbers</strong> - Use <code>new_header</code> not <code>header_v2</code></li>
    <li><strong>Keep it short but clear</strong> - Balance brevity with clarity</li>
</ul>

<h3>Flag States</h3>
<p>A feature flag has two primary states:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">State</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">SDK Returns</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Off</td><td class=""px-4 py-2"">Flag is disabled for all users</td><td class=""px-4 py-2""><code>enabled: false</code></td></tr>
        <tr><td class=""px-4 py-2 font-medium"">On</td><td class=""px-4 py-2"">Flag is enabled, rules determine variation</td><td class=""px-4 py-2""><code>enabled: true</code> (if user qualifies)</td></tr>
    </tbody>
</table>

<h3>Default Behaviour</h3>
<p>When a flag has no rules or the user doesn't match any rules:</p>
<ul>
    <li>If the flag is <strong>Off</strong>: Returns <code>enabled: false</code></li>
    <li>If the flag is <strong>On</strong> with no matching rules: Returns <code>enabled: false</code></li>
</ul>
<p>You must add rules to actually deliver the flag to users.</p>

<h3>Environments</h3>
<p>Remember that flags are configured per environment. A flag might be:</p>
<ul>
    <li><strong>On</strong> in Development for testing</li>
    <li><strong>On</strong> in Staging with limited rollout</li>
    <li><strong>Off</strong> in Production until ready</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "check-flag-csharp",
                            Title = "Checking a Flag in C#",
                            Description = "Basic code to check if a flag is enabled",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

// Initialize the client (typically done once at startup)
var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Create a user context
var user = optimizely.CreateUserContext(""user-123"");

// Check if the flag is enabled for this user
var decision = user.Decide(""checkout_redesign"");

if (decision.Enabled)
{
    // Show the new checkout experience
    Console.WriteLine(""Showing new checkout"");
}
else
{
    // Show the original checkout experience
    Console.WriteLine(""Showing original checkout"");
}",
                            IsInteractive = false,
                            SampleResponse = @"// If flag is enabled for user-123:
Showing new checkout

// If flag is disabled:
Showing original checkout"
                        }
                    }
                },
                new Lesson
                {
                    Id = "ff-variations-explained",
                    ModuleId = "feature-flags",
                    Title = "Flag Variations",
                    Summary = "Understand how variations allow multiple configurations of a feature flag.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what variations are and why they're useful",
                        "Create and configure multiple variations",
                        "Know when to use simple on/off vs multiple variations",
                        "Access variation information in your code"
                    },
                    Content = @"
<h2>Understanding Flag Variations</h2>
<p>Variations allow you to define multiple versions of a feature. Instead of just on/off, you can have distinct configurations that users can be assigned to.</p>

<h3>Default Variations</h3>
<p>Every new flag comes with two default variations:</p>
<ul>
    <li><strong>off</strong> - The variation returned when the flag is disabled</li>
    <li><strong>on</strong> - The default variation when the flag is enabled</li>
</ul>

<h3>When to Use Multiple Variations</h3>
<p>Use additional variations when:</p>
<ul>
    <li>You want to test more than two options (e.g., three different button colours)</li>
    <li>You need a ""control"" group that sees the original experience</li>
    <li>You want to progressively test variations before full rollout</li>
    <li>Different user segments need different configurations</li>
</ul>

<h3>Creating Additional Variations</h3>
<ol>
    <li>Open your flag in the Optimizely app</li>
    <li>Click <strong>Variations</strong></li>
    <li>Click <strong>Add Variation</strong></li>
    <li>Enter a <strong>Variation Key</strong> (e.g., <code>streamlined</code>, <code>express</code>)</li>
    <li>Configure any variables for this variation</li>
    <li>Save the changes</li>
</ol>

<h3>Variation Keys</h3>
<p>Like flag keys, variation keys should be:</p>
<ul>
    <li>Descriptive: <code>blue_button</code>, <code>compact_layout</code></li>
    <li>Consistent: Use the same naming pattern across your project</li>
    <li>Meaningful: Avoid generic names like <code>variation_1</code>, <code>variation_2</code></li>
</ul>

<h3>Example: Checkout Flow Variations</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Variation Key</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">off</td><td class=""px-4 py-2"">Flag disabled</td><td class=""px-4 py-2"">Emergency kill switch</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">control</td><td class=""px-4 py-2"">Original checkout (3 steps)</td><td class=""px-4 py-2"">Baseline for comparison</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">streamlined</td><td class=""px-4 py-2"">New checkout (2 steps)</td><td class=""px-4 py-2"">Test simplified flow</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">express</td><td class=""px-4 py-2"">One-click checkout</td><td class=""px-4 py-2"">Test fastest option</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "variations-csharp",
                            Title = "Working with Variations in C#",
                            Description = "Access the variation key in your code",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"");

var decision = user.Decide(""checkout_flow"");

// Check which variation the user received
Console.WriteLine($""Enabled: {decision.Enabled}"");
Console.WriteLine($""Variation: {decision.VariationKey}"");

// Handle different variations
switch (decision.VariationKey)
{
    case ""control"":
        // Show original 3-step checkout
        ShowClassicCheckout();
        break;
    case ""streamlined"":
        // Show new 2-step checkout
        ShowStreamlinedCheckout();
        break;
    case ""express"":
        // Show one-click checkout
        ShowExpressCheckout();
        break;
    default:
        // Flag is off or user doesn't qualify
        ShowClassicCheckout();
        break;
}",
                            IsInteractive = false,
                            SampleResponse = @"Enabled: True
Variation: streamlined"
                        }
                    }
                },
                new Lesson
                {
                    Id = "ff-variables-config",
                    ModuleId = "feature-flags",
                    Title = "Flag Variables for Remote Configuration",
                    Summary = "Use variables to configure feature behaviour without code changes.",
                    Order = 3,
                    EstimatedMinutes = 14,
                    LearningObjectives = new List<string>
                    {
                        "Understand what flag variables are",
                        "Know the available variable types",
                        "Create and configure variables",
                        "Access variable values in your code"
                    },
                    Content = @"
<h2>Flag Variables</h2>
<p><strong>Variables</strong> are configurable parameters attached to feature flags. They allow you to change feature behaviour remotely without deploying new code or creating new variations.</p>

<h3>Why Use Variables?</h3>
<ul>
    <li><strong>Remote Configuration</strong> - Change values without code deployment</li>
    <li><strong>A/B Test Parameters</strong> - Test different values (e.g., button colour, text)</li>
    <li><strong>Gradual Tuning</strong> - Adjust values based on real-world performance</li>
    <li><strong>Personalisation</strong> - Different values for different user segments</li>
</ul>

<h3>Variable Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">C# Type</th>
            <th class=""px-4 py-2 text-left"">Example Values</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Boolean</td><td class=""px-4 py-2""><code>bool</code></td><td class=""px-4 py-2""><code>true</code>, <code>false</code></td><td class=""px-4 py-2"">Toggle sub-features</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">String</td><td class=""px-4 py-2""><code>string</code></td><td class=""px-4 py-2""><code>""blue""</code>, <code>""Buy Now""</code></td><td class=""px-4 py-2"">Text, colours, URLs</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Integer</td><td class=""px-4 py-2""><code>int</code></td><td class=""px-4 py-2""><code>5</code>, <code>100</code></td><td class=""px-4 py-2"">Counts, limits, sizes</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Double</td><td class=""px-4 py-2""><code>double</code></td><td class=""px-4 py-2""><code>0.15</code>, <code>99.99</code></td><td class=""px-4 py-2"">Percentages, prices</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">JSON</td><td class=""px-4 py-2""><code>object</code></td><td class=""px-4 py-2"">Complex objects</td><td class=""px-4 py-2"">Structured config</td></tr>
    </tbody>
</table>

<h3>Creating Variables</h3>
<ol>
    <li>Open your flag in the Optimizely app</li>
    <li>Click <strong>Variables</strong></li>
    <li>Click <strong>Add Variable</strong></li>
    <li>Enter a <strong>Variable Key</strong> (e.g., <code>button_color</code>)</li>
    <li>Select the <strong>Type</strong></li>
    <li>Set the <strong>Default Value</strong></li>
    <li>Optionally set different values per variation</li>
</ol>

<h3>Variable Values Per Variation</h3>
<p>Each variation can have different variable values. This is powerful for A/B testing:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Variation</th>
            <th class=""px-4 py-2 text-left"">button_color</th>
            <th class=""px-4 py-2 text-left"">button_text</th>
            <th class=""px-4 py-2 text-left"">show_badge</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">control</td><td class=""px-4 py-2"">blue</td><td class=""px-4 py-2"">Add to Cart</td><td class=""px-4 py-2"">false</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">treatment_a</td><td class=""px-4 py-2"">green</td><td class=""px-4 py-2"">Buy Now</td><td class=""px-4 py-2"">true</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">treatment_b</td><td class=""px-4 py-2"">orange</td><td class=""px-4 py-2"">Get It Now</td><td class=""px-4 py-2"">true</td></tr>
    </tbody>
</table>

<h3>Default vs Variation Values</h3>
<p>When you create a variable, you set a <strong>default value</strong>. This is used when:</p>
<ul>
    <li>A variation doesn't override the variable</li>
    <li>As a fallback in your code</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "variables-csharp",
                            Title = "Accessing Variables in C#",
                            Description = "Read variable values from a flag decision",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.Entity;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"");

var decision = user.Decide(""product_page"");

if (decision.Enabled)
{
    // Get typed variable values
    var buttonColor = decision.Variables.GetValue<string>(""button_color"");
    var buttonText = decision.Variables.GetValue<string>(""button_text"");
    var showBadge = decision.Variables.GetValue<bool>(""show_badge"");
    var maxItems = decision.Variables.GetValue<int>(""max_items"");
    var discount = decision.Variables.GetValue<double>(""discount_rate"");

    Console.WriteLine($""Button Color: {buttonColor}"");
    Console.WriteLine($""Button Text: {buttonText}"");
    Console.WriteLine($""Show Badge: {showBadge}"");
    Console.WriteLine($""Max Items: {maxItems}"");
    Console.WriteLine($""Discount Rate: {discount}"");

    // Use the values in your UI
    RenderProductPage(new ProductPageConfig
    {
        ButtonColor = buttonColor ?? ""blue"",
        ButtonText = buttonText ?? ""Add to Cart"",
        ShowBadge = showBadge,
        MaxItems = maxItems,
        DiscountRate = discount
    });
}",
                            IsInteractive = false,
                            SampleResponse = @"Button Color: green
Button Text: Buy Now
Show Badge: True
Max Items: 10
Discount Rate: 0.15"
                        },
                        new LessonExample
                        {
                            Id = "json-variables-csharp",
                            Title = "Using JSON Variables in C#",
                            Description = "Access complex JSON variable values",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using System.Text.Json;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"");

var decision = user.Decide(""homepage_config"");

if (decision.Enabled)
{
    // Get JSON variable as a JsonElement or deserialize to a type
    var heroConfig = decision.Variables.GetValue<JsonElement>(""hero_section"");

    // Access nested properties
    var headline = heroConfig.GetProperty(""headline"").GetString();
    var subheadline = heroConfig.GetProperty(""subheadline"").GetString();
    var ctaText = heroConfig.GetProperty(""cta_text"").GetString();
    var ctaUrl = heroConfig.GetProperty(""cta_url"").GetString();

    Console.WriteLine($""Headline: {headline}"");
    Console.WriteLine($""CTA: {ctaText}"");

    // Or deserialize to a strongly-typed class
    // var config = JsonSerializer.Deserialize<HeroConfig>(heroConfig);
}",
                            IsInteractive = false,
                            SampleResponse = @"Headline: Welcome to Our New Experience
CTA: Get Started"
                        }
                    }
                },
                new Lesson
                {
                    Id = "ff-flag-rules",
                    ModuleId = "feature-flags",
                    Title = "Understanding Flag Rules",
                    Summary = "Learn how rules control flag delivery through A/B tests, rollouts, and bandits.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand the three types of flag rules",
                        "Know how rule evaluation order works",
                        "Learn when to use each rule type",
                        "Understand rule status and lifecycle"
                    },
                    Content = @"
<h2>Flag Rules</h2>
<p><strong>Rules</strong> determine how your flag is delivered to users. Without rules, a flag won't be delivered to anyone, even if it's enabled.</p>

<h3>Rule Types</h3>
<p>There are three types of rules:</p>

<h4>1. A/B Test</h4>
<p>Randomly assigns users to variations to measure impact:</p>
<ul>
    <li>Users are randomly bucketed into variations</li>
    <li>Statistical analysis measures differences</li>
    <li>Best for measuring the impact of changes</li>
    <li>Requires sufficient traffic for significance</li>
</ul>

<h4>2. Targeted Delivery (Rollout)</h4>
<p>Delivers a specific variation to targeted users:</p>
<ul>
    <li>No random assignment - all matching users get the same variation</li>
    <li>Can target by percentage or audience</li>
    <li>Best for gradual rollouts or feature releases</li>
    <li>No statistical comparison</li>
</ul>

<h4>3. Multi-Armed Bandit (MAB)</h4>
<p>Automatically optimises traffic allocation:</p>
<ul>
    <li>Starts with even distribution</li>
    <li>Shifts traffic to better-performing variations</li>
    <li>Best for optimisation over experimentation</li>
    <li>Good for short-term campaigns</li>
</ul>

<h3>Rule Evaluation Order</h3>
<p>Rules are evaluated in order from top to bottom:</p>
<ol>
    <li>SDK checks if the user matches Rule 1's audience</li>
    <li>If yes, apply Rule 1 (user is bucketed into a variation)</li>
    <li>If no, check Rule 2's audience</li>
    <li>Continue until a match is found or no more rules</li>
    <li>If no rules match, flag returns <code>enabled: false</code></li>
</ol>

<h3>Rule Priority Best Practice</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Order</th>
            <th class=""px-4 py-2 text-left"">Rule Type</th>
            <th class=""px-4 py-2 text-left"">Reason</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1st</td><td class=""px-4 py-2"">A/B Test</td><td class=""px-4 py-2"">Experiments need clean traffic, not affected by rollouts</td></tr>
        <tr><td class=""px-4 py-2"">2nd</td><td class=""px-4 py-2"">Targeted Delivery (specific)</td><td class=""px-4 py-2"">Target specific audiences like beta users</td></tr>
        <tr><td class=""px-4 py-2"">3rd</td><td class=""px-4 py-2"">Targeted Delivery (general)</td><td class=""px-4 py-2"">Catch-all for remaining users</td></tr>
    </tbody>
</table>

<h3>Rule Status</h3>
<p>Each rule has a status:</p>
<ul>
    <li><strong>Draft</strong> - Rule is being configured, not active</li>
    <li><strong>Running</strong> - Rule is actively delivering the flag</li>
    <li><strong>Paused</strong> - Rule is temporarily stopped</li>
    <li><strong>Archived</strong> - Rule is no longer needed</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "rule-evaluation",
                            Title = "Rule Evaluation Example",
                            Description = "How rules are evaluated for a user",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Flag: ""new_search_algorithm""
Enabled: true

Rules (evaluated in order):
┌─────────────────────────────────────────────────────────────┐
│ Rule 1: A/B Test                                            │
│ Status: Running                                             │
│ Audience: All Users                                         │
│ Variations: control (50%), treatment (50%)                  │
│                                                             │
│ → User ""user-123"" matches audience                         │
│ → Hash(""user-123"") = 0.42                                   │
│ → 0.42 < 0.50, so bucket = control                          │
│ → Return: enabled=true, variation=""control""                 │
└─────────────────────────────────────────────────────────────┘

Rule 2: Targeted Delivery (not evaluated - Rule 1 matched)
Audience: Beta Users
Variation: treatment (100%)",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ff-lifecycle-management",
                    ModuleId = "feature-flags",
                    Title = "Flag Lifecycle Management",
                    Summary = "Best practices for managing flags from creation through retirement.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand the flag lifecycle stages",
                        "Learn best practices for flag management",
                        "Know when and how to clean up old flags",
                        "Implement a flag governance process"
                    },
                    Content = @"
<h2>Flag Lifecycle Management</h2>
<p>Feature flags have a lifecycle from creation to retirement. Proper management prevents ""flag debt"" and keeps your codebase clean.</p>

<h3>Flag Lifecycle Stages</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Stage</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Actions</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Creation</td><td class=""px-4 py-2"">Flag is created, code is written</td><td class=""px-4 py-2"">Define flag, add to code</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Development</td><td class=""px-4 py-2"">Flag is tested in development</td><td class=""px-4 py-2"">Configure rules, test variations</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Rollout</td><td class=""px-4 py-2"">Flag is gradually released</td><td class=""px-4 py-2"">Increase rollout percentage</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Active</td><td class=""px-4 py-2"">Flag is fully rolled out</td><td class=""px-4 py-2"">Monitor, may run experiments</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Permanent</td><td class=""px-4 py-2"">Feature is permanent, flag can be removed</td><td class=""px-4 py-2"">Remove flag from code</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Archived</td><td class=""px-4 py-2"">Flag is no longer needed</td><td class=""px-4 py-2"">Archive in Optimizely</td></tr>
    </tbody>
</table>

<h3>Flag Debt</h3>
<p><strong>Flag debt</strong> accumulates when flags remain in code after they're no longer needed:</p>
<ul>
    <li>Code becomes harder to understand</li>
    <li>More conditional branches to maintain</li>
    <li>Potential for bugs when flag state changes</li>
    <li>Increased complexity in testing</li>
</ul>

<h3>When to Remove a Flag</h3>
<p>Remove a flag from your code when:</p>
<ul>
    <li>The feature has been fully rolled out to 100% of users</li>
    <li>The decision to keep the feature is permanent</li>
    <li>The experiment has concluded and a winner is chosen</li>
    <li>The feature was removed entirely</li>
</ul>

<h3>Flag Removal Process</h3>
<ol>
    <li><strong>Verify 100% rollout</strong> - Confirm all users see the winning variation</li>
    <li><strong>Update code</strong> - Remove the flag check, keep the winning code path</li>
    <li><strong>Deploy</strong> - Release the simplified code</li>
    <li><strong>Archive flag</strong> - Mark the flag as archived in Optimizely</li>
    <li><strong>Document</strong> - Record the decision and date</li>
</ol>

<h3>Governance Best Practices</h3>
<ul>
    <li><strong>Set owners</strong> - Every flag should have an owner responsible for its lifecycle</li>
    <li><strong>Add expiry dates</strong> - Use descriptions to note expected removal dates</li>
    <li><strong>Regular audits</strong> - Review flags quarterly to identify stale ones</li>
    <li><strong>Naming conventions</strong> - Use prefixes to indicate flag type (<code>exp_</code>, <code>release_</code>, <code>ops_</code>)</li>
    <li><strong>Document decisions</strong> - Record why a flag was created and when it should be removed</li>
</ul>

<h3>Types of Flags by Longevity</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Lifespan</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Release Flag</td><td class=""px-4 py-2"">Weeks</td><td class=""px-4 py-2"">New feature rollout</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Experiment Flag</td><td class=""px-4 py-2"">Days to weeks</td><td class=""px-4 py-2"">A/B test for conversion</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Ops Flag</td><td class=""px-4 py-2"">Permanent</td><td class=""px-4 py-2"">Kill switch for service</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Permission Flag</td><td class=""px-4 py-2"">Permanent</td><td class=""px-4 py-2"">Premium feature access</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "flag-removal-csharp",
                            Title = "Flag Removal Example",
                            Description = "Before and after removing a flag from code",
                            Type = ExampleType.Code,
                            ExampleContent = @"// BEFORE: Code with flag
var decision = user.Decide(""new_checkout"");
if (decision.Enabled && decision.VariationKey == ""streamlined"")
{
    ShowStreamlinedCheckout();
}
else
{
    ShowClassicCheckout();
}

// AFTER: Flag removed (streamlined won, now permanent)
ShowStreamlinedCheckout();

// The flag check is removed entirely
// Classic checkout code can be deleted
// Flag is archived in Optimizely",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 3: SDK Implementation

    private LearningModule BuildSdkImplementationModule()
    {
        return new LearningModule
        {
            Id = "sdk-implementation",
            Title = "SDK Implementation",
            Description = "Learn to implement the Optimizely SDK in your applications, with focus on C#/.NET.",
            Icon = "code-bracket",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started", "feature-flags" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "sdk-choosing",
                    ModuleId = "sdk-implementation",
                    Title = "Choosing the Right SDK",
                    Summary = "Select the appropriate SDK for your platform and architecture.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the SDK options available",
                        "Choose between client-side and server-side SDKs",
                        "Know the requirements for each SDK",
                        "Understand SDK versioning"
                    },
                    Content = @"
<h2>Choosing the Right SDK</h2>
<p>Optimizely provides SDKs for most major platforms. Choosing the right one depends on your architecture, platform, and use case.</p>

<h3>Server-Side SDKs</h3>
<p>Server-side SDKs run on your backend servers and make decisions before content reaches the user.</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">SDK</th>
            <th class=""px-4 py-2 text-left"">Package</th>
            <th class=""px-4 py-2 text-left"">Min Version</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">C# / .NET</td><td class=""px-4 py-2""><code>Optimizely.SDK</code></td><td class=""px-4 py-2"">.NET 6.0+</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Java</td><td class=""px-4 py-2""><code>com.optimizely.ab:core-api</code></td><td class=""px-4 py-2"">Java 8+</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Python</td><td class=""px-4 py-2""><code>optimizely-sdk</code></td><td class=""px-4 py-2"">Python 3.8+</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Node.js</td><td class=""px-4 py-2""><code>@optimizely/optimizely-sdk</code></td><td class=""px-4 py-2"">Node 14+</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Go</td><td class=""px-4 py-2""><code>github.com/optimizely/go-sdk</code></td><td class=""px-4 py-2"">Go 1.13+</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Ruby</td><td class=""px-4 py-2""><code>optimizely-sdk</code></td><td class=""px-4 py-2"">Ruby 2.7+</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">PHP</td><td class=""px-4 py-2""><code>optimizely/optimizely-sdk</code></td><td class=""px-4 py-2"">PHP 7.4+</td></tr>
    </tbody>
</table>

<h3>Client-Side SDKs</h3>
<p>Client-side SDKs run in the user's browser or on their device.</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">SDK</th>
            <th class=""px-4 py-2 text-left"">Package</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">JavaScript</td><td class=""px-4 py-2""><code>@optimizely/optimizely-sdk</code></td><td class=""px-4 py-2"">Web browsers</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">React</td><td class=""px-4 py-2""><code>@optimizely/react-sdk</code></td><td class=""px-4 py-2"">React applications</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Swift</td><td class=""px-4 py-2""><code>OptimizelySwiftSDK</code></td><td class=""px-4 py-2"">iOS apps</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Android</td><td class=""px-4 py-2""><code>com.optimizely.ab:android-sdk</code></td><td class=""px-4 py-2"">Android apps</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Flutter</td><td class=""px-4 py-2""><code>optimizely_flutter_sdk</code></td><td class=""px-4 py-2"">Cross-platform mobile</td></tr>
    </tbody>
</table>

<h3>Decision Factors</h3>
<p>Consider these factors when choosing:</p>
<ul>
    <li><strong>Where does the decision happen?</strong> - Backend (server-side) vs frontend (client-side)</li>
    <li><strong>Security requirements</strong> - Server-side keeps SDK keys hidden</li>
    <li><strong>Latency requirements</strong> - Server-side can include decision in initial render</li>
    <li><strong>Platform</strong> - What language/framework are you using?</li>
    <li><strong>Consistency</strong> - Same user across platforms? Consider server-side for consistency</li>
</ul>

<h3>This Course Focus</h3>
<p>This course focuses primarily on the <strong>C#/.NET SDK</strong> for server-side implementation, with examples in JavaScript/React for client-side scenarios.</p>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "sdk-dotnet-install",
                    ModuleId = "sdk-implementation",
                    Title = "C#/.NET SDK Installation",
                    Summary = "Install and configure the Optimizely SDK in your .NET application.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Install the Optimizely NuGet package",
                        "Configure the SDK in your application",
                        "Set up dependency injection",
                        "Handle SDK initialization"
                    },
                    Content = @"
<h2>Installing the C#/.NET SDK</h2>
<p>The Optimizely SDK for .NET is distributed via NuGet and supports .NET 6.0 and later.</p>

<h3>Installation</h3>
<p>Install the SDK using the NuGet Package Manager, .NET CLI, or Package Manager Console:</p>

<h3>Supported .NET Versions</h3>
<ul>
    <li>.NET 6.0</li>
    <li>.NET 7.0</li>
    <li>.NET 8.0</li>
    <li>.NET 9.0+</li>
</ul>

<h3>Dependencies</h3>
<p>The SDK has minimal dependencies:</p>
<ul>
    <li><code>Newtonsoft.Json</code> - JSON serialization</li>
    <li><code>MurmurHash.Net</code> - Consistent bucketing</li>
    <li><code>NLog</code> (optional) - Logging</li>
</ul>

<h3>SDK Components</h3>
<p>The package includes:</p>
<ul>
    <li><code>Optimizely</code> - Main client class</li>
    <li><code>OptimizelyFactory</code> - Factory for creating clients</li>
    <li><code>OptimizelyUserContext</code> - User context for decisions</li>
    <li><code>OptimizelyDecision</code> - Decision result</li>
    <li><code>OptimizelyConfig</code> - Configuration access</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "install-nuget",
                            Title = "Install via NuGet",
                            Description = "Install the Optimizely SDK package",
                            Type = ExampleType.Code,
                            ExampleContent = @"# .NET CLI
dotnet add package Optimizely.SDK

# Package Manager Console
Install-Package Optimizely.SDK

# Or add to your .csproj file:
<PackageReference Include=""Optimizely.SDK"" Version=""4.*"" />",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "di-registration",
                            Title = "Dependency Injection Setup",
                            Description = "Register Optimizely in ASP.NET Core DI container",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Program.cs or Startup.cs
using OptimizelySDK;

var builder = WebApplication.CreateBuilder(args);

// Option 1: Register as singleton (recommended for server apps)
builder.Services.AddSingleton<Optimizely>(sp =>
{
    var sdkKey = builder.Configuration[""Optimizely:SdkKey""];
    return OptimizelyFactory.NewDefaultInstance(sdkKey);
});

// Option 2: Using a factory for more control
builder.Services.AddSingleton<IOptimizelyFactory, OptimizelyFactory>();
builder.Services.AddSingleton<Optimizely>(sp =>
{
    var factory = sp.GetRequiredService<IOptimizelyFactory>();
    var config = sp.GetRequiredService<IConfiguration>();
    return factory.NewDefaultInstance(config[""Optimizely:SdkKey""]);
});

var app = builder.Build();

// Verify SDK is ready on startup
var optimizely = app.Services.GetRequiredService<Optimizely>();
if (!optimizely.IsValid)
{
    throw new InvalidOperationException(""Failed to initialize Optimizely SDK"");
}",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "appsettings-config",
                            Title = "Configuration in appsettings.json",
                            Description = "Store SDK key in configuration",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"{
  ""Optimizely"": {
    ""SdkKey"": ""YOUR_SDK_KEY_HERE"",
    ""DatafilePollingInterval"": 60,
    ""EventFlushInterval"": 30
  }
}

// For different environments, use:
// appsettings.Development.json
// appsettings.Production.json
// Or environment variables: Optimizely__SdkKey",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sdk-initialization",
                    ModuleId = "sdk-implementation",
                    Title = "SDK Initialization and Configuration",
                    Summary = "Configure SDK options for datafile handling, logging, and event dispatching.",
                    Order = 3,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand SDK initialization options",
                        "Configure datafile polling",
                        "Set up logging and error handling",
                        "Configure event dispatching"
                    },
                    Content = @"
<h2>SDK Initialization</h2>
<p>The Optimizely SDK can be initialized with various configuration options to control its behaviour.</p>

<h3>Initialization Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono text-sm"">NewDefaultInstance(sdkKey)</td><td class=""px-4 py-2"">Auto-fetches datafile, default settings</td><td class=""px-4 py-2"">Most common usage</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">NewDefaultInstance(sdkKey, fallback)</td><td class=""px-4 py-2"">With fallback datafile</td><td class=""px-4 py-2"">Offline support</td></tr>
        <tr><td class=""px-4 py-2 font-mono text-sm"">new Optimizely(datafile)</td><td class=""px-4 py-2"">From local datafile string</td><td class=""px-4 py-2"">Testing, custom fetching</td></tr>
    </tbody>
</table>

<h3>Configuration Options</h3>
<p>When creating an SDK instance, you can configure:</p>

<h4>Datafile Management</h4>
<ul>
    <li><strong>Polling Interval</strong> - How often to check for datafile updates (default: 5 minutes)</li>
    <li><strong>Blocking Timeout</strong> - Max time to wait for initial datafile fetch</li>
    <li><strong>Auto Update</strong> - Whether to automatically update the datafile</li>
</ul>

<h4>Event Handling</h4>
<ul>
    <li><strong>Event Dispatcher</strong> - Custom dispatcher for sending events</li>
    <li><strong>Flush Interval</strong> - How often to flush event queue</li>
    <li><strong>Batch Size</strong> - Number of events per batch</li>
</ul>

<h4>Logging</h4>
<ul>
    <li><strong>Logger</strong> - Custom logger implementation</li>
    <li><strong>Log Level</strong> - Minimum log level (Error, Warning, Info, Debug)</li>
</ul>

<h3>SDK Readiness</h3>
<p>Always check if the SDK is ready before making decisions:</p>
<ul>
    <li><code>IsValid</code> - Returns true if SDK initialized successfully</li>
    <li>If not valid, decisions will return default values</li>
    <li>Events won't be tracked if SDK is invalid</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "advanced-init",
                            Title = "Advanced Initialization",
                            Description = "Configure SDK with custom options",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.Config;
using OptimizelySDK.Logger;
using OptimizelySDK.Event.Dispatcher;

// Create a custom configuration
var configManager = new HttpProjectConfigManager.Builder()
    .WithSdkKey(""YOUR_SDK_KEY"")
    .WithPollingInterval(TimeSpan.FromMinutes(1))
    .WithBlockingTimeoutPeriod(TimeSpan.FromSeconds(15))
    .WithAutoUpdate(true)
    .Build();

// Create custom event dispatcher (optional)
var eventDispatcher = new DefaultEventDispatcher(
    new Logger(LogLevel.INFO),
    TimeSpan.FromSeconds(30)  // Flush interval
);

// Initialize with custom configuration
var optimizely = new Optimizely(
    configManager: configManager,
    eventDispatcher: eventDispatcher,
    logger: new Logger(LogLevel.INFO),
    errorHandler: new DefaultErrorHandler()
);

// Wait for SDK to be ready
if (optimizely.IsValid)
{
    Console.WriteLine(""Optimizely SDK initialized successfully"");

    // Get current configuration revision
    var config = optimizely.GetOptimizelyConfig();
    Console.WriteLine($""Datafile revision: {config?.Revision}"");
}
else
{
    Console.WriteLine(""SDK failed to initialize"");
}",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "fallback-datafile",
                            Title = "Using a Fallback Datafile",
                            Description = "Initialize with a local fallback datafile",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

// Load fallback datafile from embedded resource or file
var fallbackDatafile = await File.ReadAllTextAsync(""datafile.json"");

// Initialize with fallback
// SDK will use fallback if network fetch fails
var optimizely = OptimizelyFactory.NewDefaultInstance(
    ""YOUR_SDK_KEY"",
    fallbackDatafile
);

// The SDK will:
// 1. Try to fetch the latest datafile from CDN
// 2. If fetch fails, use the fallback datafile
// 3. Continue polling for updates in background

if (optimizely.IsValid)
{
    var config = optimizely.GetOptimizelyConfig();
    Console.WriteLine($""Using datafile revision: {config?.Revision}"");
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sdk-user-context",
                    ModuleId = "sdk-implementation",
                    Title = "Creating User Context",
                    Summary = "Create user contexts with IDs and attributes for decision-making.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create user contexts with user IDs",
                        "Add user attributes for targeting",
                        "Understand user ID strategies",
                        "Update user attributes dynamically"
                    },
                    Content = @"
<h2>User Context</h2>
<p>A <strong>user context</strong> represents the user for whom you're making feature flag decisions. It's required for all decision methods.</p>

<h3>Creating a User Context</h3>
<p>Use <code>CreateUserContext</code> to create a context with a user ID:</p>

<h3>User ID Strategies</h3>
<p>The user ID should be a consistent identifier for the user:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Strategy</th>
            <th class=""px-4 py-2 text-left"">Example</th>
            <th class=""px-4 py-2 text-left"">Pros</th>
            <th class=""px-4 py-2 text-left"">Cons</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Database ID</td><td class=""px-4 py-2""><code>user-12345</code></td><td class=""px-4 py-2"">Consistent across sessions</td><td class=""px-4 py-2"">Requires authentication</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Email Hash</td><td class=""px-4 py-2""><code>sha256(email)</code></td><td class=""px-4 py-2"">Works pre-login</td><td class=""px-4 py-2"">Need email first</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Device ID</td><td class=""px-4 py-2""><code>device-abc123</code></td><td class=""px-4 py-2"">No authentication needed</td><td class=""px-4 py-2"">Different per device</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Session ID</td><td class=""px-4 py-2""><code>session-xyz</code></td><td class=""px-4 py-2"">Works for anonymous</td><td class=""px-4 py-2"">New ID each session</td></tr>
    </tbody>
</table>

<h3>User Attributes</h3>
<p>Attributes are additional data about the user used for:</p>
<ul>
    <li><strong>Audience targeting</strong> - Target users based on attributes</li>
    <li><strong>Results segmentation</strong> - Analyse results by segment</li>
    <li><strong>Personalisation</strong> - Used by CMAB for personalisation</li>
</ul>

<h3>Common Attribute Types</h3>
<ul>
    <li><strong>Geographic</strong>: country, region, city</li>
    <li><strong>Demographic</strong>: age_group, gender</li>
    <li><strong>Behavioural</strong>: plan_type, lifetime_value, is_returning</li>
    <li><strong>Technical</strong>: device_type, browser, app_version</li>
</ul>

<h3>Important Considerations</h3>
<ul>
    <li>User ID must be a non-empty string</li>
    <li>Same user ID = same bucketing (deterministic)</li>
    <li>Attributes are optional but enable targeting</li>
    <li>Attribute values should match audience conditions exactly</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "create-user-context",
                            Title = "Creating User Context",
                            Description = "Create user contexts with IDs and attributes",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Simple user context with just ID
var simpleUser = optimizely.CreateUserContext(""user-12345"");

// User context with attributes
var userWithAttributes = optimizely.CreateUserContext(
    userId: ""user-12345"",
    attributes: new UserAttributes
    {
        { ""country"", ""UK"" },
        { ""plan_type"", ""premium"" },
        { ""age"", 28 },
        { ""is_returning_customer"", true },
        { ""lifetime_value"", 150.50 }
    }
);

// Make a decision with the user context
var decision = userWithAttributes.Decide(""premium_features"");

Console.WriteLine($""User: {userWithAttributes.GetUserId()}"");
Console.WriteLine($""Enabled: {decision.Enabled}"");
Console.WriteLine($""Variation: {decision.VariationKey}"");",
                            IsInteractive = false,
                            SampleResponse = @"User: user-12345
Enabled: True
Variation: enhanced"
                        },
                        new LessonExample
                        {
                            Id = "aspnet-user-context",
                            Title = "User Context in ASP.NET Core",
                            Description = "Create user context from authenticated user",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(""api/[controller]"")]
public class ProductController : ControllerBase
{
    private readonly Optimizely _optimizely;

    public ProductController(Optimizely optimizely)
    {
        _optimizely = optimizely;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        // Get user ID from authenticated user
        var userId = User.FindFirst(""sub"")?.Value
            ?? HttpContext.Connection.Id; // Fallback for anonymous

        // Build attributes from various sources
        var attributes = new UserAttributes
        {
            { ""country"", GetCountryFromRequest() },
            { ""device_type"", GetDeviceType() },
            { ""is_authenticated"", User.Identity?.IsAuthenticated ?? false }
        };

        // Add claim-based attributes for authenticated users
        if (User.Identity?.IsAuthenticated == true)
        {
            attributes[""plan_type""] = User.FindFirst(""plan"")?.Value ?? ""free"";
            attributes[""account_age_days""] = GetAccountAgeDays();
        }

        var user = _optimizely.CreateUserContext(userId, attributes);
        var decision = user.Decide(""new_product_layout"");

        if (decision.Enabled)
        {
            return Ok(GetProductsWithNewLayout());
        }
        return Ok(GetProductsWithClassicLayout());
    }

    private string GetCountryFromRequest()
    {
        return Request.Headers[""CF-IPCountry""].FirstOrDefault() ?? ""unknown"";
    }

    private string GetDeviceType()
    {
        var userAgent = Request.Headers[""User-Agent""].ToString();
        return userAgent.Contains(""Mobile"") ? ""mobile"" : ""desktop"";
    }

    private int GetAccountAgeDays() => 365; // Implement actual logic
    private object GetProductsWithNewLayout() => new { layout = ""new"" };
    private object GetProductsWithClassicLayout() => new { layout = ""classic"" };
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sdk-javascript-quickstart",
                    ModuleId = "sdk-implementation",
                    Title = "JavaScript SDK Quickstart",
                    Summary = "Get started with the JavaScript SDK for browser-based applications.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Install the JavaScript SDK",
                        "Initialize the SDK in a browser environment",
                        "Create user context and make decisions",
                        "Understand client-side considerations"
                    },
                    Content = @"
<h2>JavaScript SDK for Browser</h2>
<p>The JavaScript SDK runs in the browser and is useful for client-side feature flags and experiments.</p>

<h3>Installation Options</h3>
<ul>
    <li><strong>npm</strong> - For bundled applications</li>
    <li><strong>CDN</strong> - For quick integration</li>
</ul>

<h3>Client-Side Considerations</h3>
<ul>
    <li><strong>SDK Key Exposure</strong> - The SDK key is visible in client code</li>
    <li><strong>Datafile Size</strong> - Datafile is downloaded per user</li>
    <li><strong>Initial Load</strong> - Consider showing loading state while SDK initializes</li>
    <li><strong>User ID</strong> - Need a strategy for anonymous users</li>
</ul>

<h3>Async Initialization</h3>
<p>The JavaScript SDK initializes asynchronously. You should wait for it to be ready before making decisions.</p>

<h3>User ID Strategies for Browser</h3>
<ul>
    <li><strong>Authenticated users</strong> - Use their account ID</li>
    <li><strong>Anonymous users</strong> - Generate and store a UUID in localStorage</li>
    <li><strong>Session-based</strong> - Use sessionStorage for per-session IDs</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "js-sdk-install",
                            Title = "JavaScript SDK Installation",
                            Description = "Install and initialize the JavaScript SDK",
                            Type = ExampleType.Code,
                            ExampleContent = @"// Install via npm
// npm install @optimizely/optimizely-sdk

import { createInstance } from '@optimizely/optimizely-sdk';

// Initialize the SDK
const optimizely = createInstance({
  sdkKey: 'YOUR_SDK_KEY',
  // Optional: provide a fallback datafile for offline support
  // datafile: cachedDatafile,
});

// Wait for SDK to be ready
optimizely.onReady().then(() => {
  console.log('Optimizely SDK is ready');

  // Get or create a user ID
  let userId = localStorage.getItem('optimizely_user_id');
  if (!userId) {
    userId = crypto.randomUUID();
    localStorage.setItem('optimizely_user_id', userId);
  }

  // Create user context
  const user = optimizely.createUserContext(userId, {
    country: 'UK',
    device_type: /Mobile/i.test(navigator.userAgent) ? 'mobile' : 'desktop'
  });

  // Make a decision
  const decision = user.decide('new_homepage');

  if (decision.enabled) {
    console.log('Showing new homepage');
    showNewHomepage();
  } else {
    console.log('Showing original homepage');
    showOriginalHomepage();
  }
});",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "js-cdn-install",
                            Title = "CDN Installation",
                            Description = "Quick setup using CDN script tag",
                            Type = ExampleType.Code,
                            ExampleContent = @"<!-- Add to your HTML -->
<script src=""https://unpkg.com/@optimizely/optimizely-sdk/dist/optimizely.browser.umd.min.js""></script>

<script>
  // SDK is available as window.optimizelySdk
  const optimizely = window.optimizelySdk.createInstance({
    sdkKey: 'YOUR_SDK_KEY'
  });

  optimizely.onReady().then(() => {
    const userId = localStorage.getItem('user_id') || 'anonymous-' + Date.now();
    const user = optimizely.createUserContext(userId);

    const decision = user.decide('hero_banner');

    if (decision.enabled) {
      document.getElementById('hero').className = decision.variationKey;
    }
  });
</script>",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "sdk-react-quickstart",
                    ModuleId = "sdk-implementation",
                    Title = "React SDK Implementation",
                    Summary = "Implement Feature Experimentation in React applications using hooks and providers.",
                    Order = 6,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Set up the React SDK provider",
                        "Use the useDecision hook",
                        "Handle loading and error states",
                        "Implement feature flags in React components"
                    },
                    Content = @"
<h2>React SDK</h2>
<p>The React SDK provides hooks and components specifically designed for React applications, making it easy to integrate feature flags into your components.</p>

<h3>Key Features</h3>
<ul>
    <li><strong>OptimizelyProvider</strong> - Context provider for the SDK</li>
    <li><strong>useDecision</strong> - Hook for feature flag decisions</li>
    <li><strong>useTrackEvent</strong> - Hook for tracking events</li>
    <li><strong>withOptimizely</strong> - HOC for class components</li>
</ul>

<h3>Provider Setup</h3>
<p>Wrap your application with <code>OptimizelyProvider</code> at the root level to make the SDK available throughout your component tree.</p>

<h3>Loading States</h3>
<p>The SDK needs to fetch the datafile before making decisions. The React SDK provides utilities to handle this loading state gracefully.</p>

<h3>Auto-Update</h3>
<p>By default, the React SDK will re-render components when the datafile updates. You can control this behaviour with the <code>autoUpdate</code> option.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "react-provider-setup",
                            Title = "React Provider Setup",
                            Description = "Set up OptimizelyProvider in your React app",
                            Type = ExampleType.Code,
                            ExampleContent = @"// npm install @optimizely/react-sdk

// App.tsx
import { OptimizelyProvider, createInstance } from '@optimizely/react-sdk';

const optimizely = createInstance({
  sdkKey: 'YOUR_SDK_KEY',
});

// Get or generate user ID
const userId = localStorage.getItem('user_id') || crypto.randomUUID();
localStorage.setItem('user_id', userId);

function App() {
  return (
    <OptimizelyProvider
      optimizely={optimizely}
      user={{
        id: userId,
        attributes: {
          plan_type: 'premium',
          country: 'UK'
        }
      }}
    >
      <Router>
        <Routes>
          <Route path=""/"" element={<HomePage />} />
          <Route path=""/products"" element={<ProductsPage />} />
        </Routes>
      </Router>
    </OptimizelyProvider>
  );
}

export default App;",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "react-use-decision",
                            Title = "Using the useDecision Hook",
                            Description = "Make feature flag decisions in React components",
                            Type = ExampleType.Code,
                            ExampleContent = @"import { useDecision } from '@optimizely/react-sdk';

function CheckoutButton() {
  // useDecision returns [decision, clientReady, didTimeout]
  const [decision, clientReady] = useDecision('checkout_button');

  // Handle loading state
  if (!clientReady) {
    return <button disabled>Loading...</button>;
  }

  // Get variable values
  const buttonColor = decision.variables.button_color as string || 'blue';
  const buttonText = decision.variables.button_text as string || 'Checkout';

  return (
    <button
      style={{ backgroundColor: buttonColor }}
      className={`checkout-btn ${decision.variationKey}`}
    >
      {buttonText}
    </button>
  );
}

// Component with multiple flags
function ProductCard({ product }) {
  const [priceDecision] = useDecision('show_discount_price');
  const [reviewDecision] = useDecision('show_reviews_preview');

  return (
    <div className=""product-card"">
      <h3>{product.name}</h3>

      {priceDecision.enabled && priceDecision.variables.show_original && (
        <span className=""original-price"">${product.originalPrice}</span>
      )}
      <span className=""price"">${product.price}</span>

      {reviewDecision.enabled && (
        <ReviewsPreview productId={product.id} />
      )}
    </div>
  );
}",
                            IsInteractive = false
                        },
                        new LessonExample
                        {
                            Id = "react-track-event",
                            Title = "Tracking Events in React",
                            Description = "Track conversion events using the useTrackEvent hook",
                            Type = ExampleType.Code,
                            ExampleContent = @"import { useTrackEvent } from '@optimizely/react-sdk';

function PurchaseButton({ product, price }) {
  const trackEvent = useTrackEvent();

  const handlePurchase = async () => {
    // Process the purchase
    const result = await processPurchase(product);

    if (result.success) {
      // Track the purchase event with revenue
      trackEvent('purchase', {
        revenue: price * 100, // Revenue in cents
        value: price
      });

      // Track additional events
      trackEvent('add_to_cart');
    }
  };

  return (
    <button onClick={handlePurchase}>
      Buy Now - ${price}
    </button>
  );
}",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 4: User Context and Targeting

    private LearningModule BuildUserContextTargetingModule()
    {
        return new LearningModule
        {
            Id = "user-context-targeting",
            Title = "User Context and Targeting",
            Description = "Master audience targeting with user attributes, segments, and advanced targeting conditions.",
            Icon = "user-group",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "sdk-implementation" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "uct-attributes-deep-dive",
                    ModuleId = "user-context-targeting",
                    Title = "User Attributes Deep Dive",
                    Summary = "Understand how to effectively use user attributes for targeting and personalisation.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand attribute data types and their uses",
                        "Design an effective attribute schema",
                        "Pass attributes consistently across platforms",
                        "Handle missing or null attributes"
                    },
                    Content = @"
<h2>User Attributes Deep Dive</h2>
<p>User attributes are key-value pairs that describe characteristics of your users. They enable powerful targeting and help you analyse experiment results by segment.</p>

<h3>Attribute Data Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Example Values</th>
            <th class=""px-4 py-2 text-left"">Supported Operations</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">String</td><td class=""px-4 py-2""><code>""premium""</code>, <code>""UK""</code></td><td class=""px-4 py-2"">equals, contains, starts_with, ends_with</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Number</td><td class=""px-4 py-2""><code>25</code>, <code>150.50</code></td><td class=""px-4 py-2"">equals, less_than, greater_than, between</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Boolean</td><td class=""px-4 py-2""><code>true</code>, <code>false</code></td><td class=""px-4 py-2"">equals</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Null</td><td class=""px-4 py-2""><code>null</code></td><td class=""px-4 py-2"">exists, not_exists</td></tr>
    </tbody>
</table>

<h3>Designing Your Attribute Schema</h3>
<p>Plan your attributes carefully before implementation:</p>
<ul>
    <li><strong>Be consistent</strong> - Use the same attribute names across all platforms</li>
    <li><strong>Use descriptive names</strong> - <code>subscription_tier</code> is better than <code>tier</code></li>
    <li><strong>Consider cardinality</strong> - Avoid attributes with too many unique values</li>
    <li><strong>Document your schema</strong> - Maintain a list of valid attribute names and values</li>
</ul>

<h3>Common Attribute Categories</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Category</th>
            <th class=""px-4 py-2 text-left"">Example Attributes</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Geographic</td><td class=""px-4 py-2"">country, region, city, timezone</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Account</td><td class=""px-4 py-2"">plan_type, account_age_days, is_trial</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Behavioural</td><td class=""px-4 py-2"">pages_viewed, purchases_count, last_login_days</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Technical</td><td class=""px-4 py-2"">device_type, browser, app_version</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Business</td><td class=""px-4 py-2"">lifetime_value, industry, company_size</td></tr>
    </tbody>
</table>

<h3>Handling Missing Attributes</h3>
<p>When an attribute is missing or null:</p>
<ul>
    <li>Audience conditions using that attribute won't match</li>
    <li>You can use ""exists"" or ""not exists"" conditions</li>
    <li>Consider providing default values in your code</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "comprehensive-attributes",
                            Title = "Comprehensive Attribute Example",
                            Description = "Building a complete user profile with attributes",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

public class OptimizelyUserService
{
    private readonly Optimizely _optimizely;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OptimizelyUserService(
        Optimizely optimizely,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _optimizely = optimizely;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public OptimizelyUserContext CreateUserContext(string userId)
    {
        var user = _userRepository.GetUser(userId);
        var request = _httpContextAccessor.HttpContext?.Request;

        var attributes = new UserAttributes
        {
            // Account attributes
            { ""plan_type"", user?.PlanType ?? ""free"" },
            { ""account_age_days"", user != null ? (DateTime.UtcNow - user.CreatedAt).Days : 0 },
            { ""is_trial"", user?.IsTrial ?? false },
            { ""lifetime_value"", user?.LifetimeValue ?? 0.0 },

            // Geographic attributes
            { ""country"", GetCountryCode(request) },
            { ""region"", GetRegion(request) },

            // Technical attributes
            { ""device_type"", GetDeviceType(request) },
            { ""browser"", GetBrowser(request) },
            { ""is_mobile"", IsMobileDevice(request) },

            // Behavioural attributes
            { ""pages_viewed_session"", GetSessionPageViews() },
            { ""is_returning"", user?.VisitCount > 1 }
        };

        return _optimizely.CreateUserContext(userId, attributes);
    }

    private string GetCountryCode(HttpRequest? request)
    {
        // From Cloudflare header, GeoIP, or default
        return request?.Headers[""CF-IPCountry""].FirstOrDefault()
            ?? request?.Headers[""X-Country-Code""].FirstOrDefault()
            ?? ""unknown"";
    }

    private string GetDeviceType(HttpRequest? request)
    {
        var userAgent = request?.Headers[""User-Agent""].ToString() ?? """";
        if (userAgent.Contains(""Mobile"")) return ""mobile"";
        if (userAgent.Contains(""Tablet"")) return ""tablet"";
        return ""desktop"";
    }

    // ... other helper methods
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "uct-audiences",
                    ModuleId = "user-context-targeting",
                    Title = "Creating Audiences",
                    Summary = "Define audiences to target specific user segments with your experiments.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create audiences in the Optimizely app",
                        "Understand audience condition types",
                        "Combine conditions with AND/OR logic",
                        "Reuse audiences across flags"
                    },
                    Content = @"
<h2>Creating Audiences</h2>
<p>Audiences define which users should be included in your experiments or rollouts based on their attributes.</p>

<h3>Creating an Audience</h3>
<ol>
    <li>Navigate to <strong>Audiences</strong> in the Optimizely app</li>
    <li>Click <strong>Create New Audience</strong></li>
    <li>Enter an <strong>Audience Name</strong> (e.g., ""Premium UK Users"")</li>
    <li>Add conditions using your defined attributes</li>
    <li>Save the audience</li>
</ol>

<h3>Condition Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Condition</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">equals</td><td class=""px-4 py-2"">Exact match</td><td class=""px-4 py-2"">country equals ""UK""</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">does_not_equal</td><td class=""px-4 py-2"">Not equal</td><td class=""px-4 py-2"">plan_type does_not_equal ""free""</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">contains</td><td class=""px-4 py-2"">Substring match</td><td class=""px-4 py-2"">email contains ""@company.com""</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">greater_than</td><td class=""px-4 py-2"">Numeric comparison</td><td class=""px-4 py-2"">age greater_than 18</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">less_than</td><td class=""px-4 py-2"">Numeric comparison</td><td class=""px-4 py-2"">account_age_days less_than 30</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">exists</td><td class=""px-4 py-2"">Attribute is present</td><td class=""px-4 py-2"">subscription_id exists</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">not_exists</td><td class=""px-4 py-2"">Attribute is missing</td><td class=""px-4 py-2"">payment_method not_exists</td></tr>
    </tbody>
</table>

<h3>Combining Conditions</h3>
<p>Use AND/OR logic to create complex audiences:</p>
<ul>
    <li><strong>AND</strong> - All conditions must be true</li>
    <li><strong>OR</strong> - Any condition can be true</li>
    <li><strong>Nested groups</strong> - Combine AND/OR groups</li>
</ul>

<h3>Example Audiences</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Audience</th>
            <th class=""px-4 py-2 text-left"">Conditions</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Premium UK Users</td><td class=""px-4 py-2"">plan_type = ""premium"" AND country = ""UK""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">New Mobile Users</td><td class=""px-4 py-2"">account_age_days < 7 AND device_type = ""mobile""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">High Value Customers</td><td class=""px-4 py-2"">lifetime_value > 500 OR plan_type = ""enterprise""</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Beta Testers</td><td class=""px-4 py-2"">is_beta_tester = true</td></tr>
    </tbody>
</table>

<h3>Audience Reusability</h3>
<p>Audiences can be reused across multiple flags and experiments, providing consistency and reducing duplication.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "audience-conditions-json",
                            Title = "Audience Conditions in Datafile",
                            Description = "How audience conditions look in the datafile",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"// Audience: ""Premium UK Users""
// Conditions: plan_type = ""premium"" AND country = ""UK""

{
  ""id"": ""21234567890"",
  ""name"": ""Premium UK Users"",
  ""conditions"": [
    ""and"",
    [""or"", [""or"", { ""name"": ""plan_type"", ""type"": ""custom_attribute"", ""value"": ""premium"" }]],
    [""or"", [""or"", { ""name"": ""country"", ""type"": ""custom_attribute"", ""value"": ""UK"" }]]
  ]
}

// Audience: ""High Value OR Enterprise""
// Conditions: lifetime_value > 500 OR plan_type = ""enterprise""

{
  ""id"": ""21234567891"",
  ""name"": ""High Value OR Enterprise"",
  ""conditions"": [
    ""or"",
    [""or"", [""or"", { ""name"": ""lifetime_value"", ""type"": ""custom_attribute"", ""value"": 500, ""match"": ""gt"" }]],
    [""or"", [""or"", { ""name"": ""plan_type"", ""type"": ""custom_attribute"", ""value"": ""enterprise"" }]]
  ]
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "uct-targeting-rules",
                    ModuleId = "user-context-targeting",
                    Title = "Targeting with Rules",
                    Summary = "Apply audiences to flag rules to control which users see your experiments.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Apply audiences to flag rules",
                        "Combine multiple audiences in a rule",
                        "Understand the difference between ANY and ALL matching",
                        "Order rules for effective targeting"
                    },
                    Content = @"
<h2>Targeting with Rules</h2>
<p>Once you've created audiences, you can use them in flag rules to control which users are included in your experiments and rollouts.</p>

<h3>Adding Audiences to Rules</h3>
<ol>
    <li>Open your flag and select the environment</li>
    <li>Add or edit a rule (A/B test, delivery, or MAB)</li>
    <li>In the <strong>Audiences</strong> section, search for and add audiences</li>
    <li>Choose the match type: <strong>Any</strong> or <strong>All</strong></li>
    <li>Save the rule</li>
</ol>

<h3>Match Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Match Type</th>
            <th class=""px-4 py-2 text-left"">Logic</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Match ANY</td><td class=""px-4 py-2"">User matches at least one audience (OR)</td><td class=""px-4 py-2"">Broad targeting</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Match ALL</td><td class=""px-4 py-2"">User matches every audience (AND)</td><td class=""px-4 py-2"">Narrow targeting</td></tr>
    </tbody>
</table>

<h3>Rule Evaluation Order</h3>
<p>Remember that rules are evaluated in order. A user is bucketed into the first rule they match:</p>
<ol>
    <li>Check Rule 1's audience conditions → If match, apply Rule 1</li>
    <li>Check Rule 2's audience conditions → If match, apply Rule 2</li>
    <li>Continue until a match or end of rules</li>
    <li>No match = flag returns disabled</li>
</ol>

<h3>Strategic Rule Ordering</h3>
<p>Order your rules strategically:</p>
<ul>
    <li><strong>Most specific first</strong> - Put narrow audiences before broad ones</li>
    <li><strong>Experiments first</strong> - A/B tests should typically be first</li>
    <li><strong>Catch-all last</strong> - General rollout rules at the bottom</li>
</ul>

<h3>Example: Multi-Rule Flag</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Order</th>
            <th class=""px-4 py-2 text-left"">Rule Type</th>
            <th class=""px-4 py-2 text-left"">Audience</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1</td><td class=""px-4 py-2"">A/B Test</td><td class=""px-4 py-2"">Premium Users</td><td class=""px-4 py-2"">Test with engaged users first</td></tr>
        <tr><td class=""px-4 py-2"">2</td><td class=""px-4 py-2"">Delivery</td><td class=""px-4 py-2"">Beta Testers</td><td class=""px-4 py-2"">Always show new feature to testers</td></tr>
        <tr><td class=""px-4 py-2"">3</td><td class=""px-4 py-2"">Delivery</td><td class=""px-4 py-2"">All Users (50%)</td><td class=""px-4 py-2"">Gradual rollout to everyone else</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "targeting-code-example",
                            Title = "Testing Audience Targeting",
                            Description = "Verify audience targeting works as expected",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Test different user profiles
var testUsers = new[]
{
    (""premium-uk"", new UserAttributes { { ""plan_type"", ""premium"" }, { ""country"", ""UK"" } }),
    (""free-uk"", new UserAttributes { { ""plan_type"", ""free"" }, { ""country"", ""UK"" } }),
    (""premium-us"", new UserAttributes { { ""plan_type"", ""premium"" }, { ""country"", ""US"" } }),
    (""beta-tester"", new UserAttributes { { ""is_beta_tester"", true } })
};

foreach (var (userId, attributes) in testUsers)
{
    var user = optimizely.CreateUserContext(userId, attributes);
    var decision = user.Decide(""new_checkout"");

    Console.WriteLine($""User: {userId}"");
    Console.WriteLine($""  Enabled: {decision.Enabled}"");
    Console.WriteLine($""  Variation: {decision.VariationKey}"");
    Console.WriteLine($""  Rule Key: {decision.RuleKey}"");
    Console.WriteLine();
}",
                            IsInteractive = false,
                            SampleResponse = @"User: premium-uk
  Enabled: True
  Variation: treatment
  Rule Key: ab_test_premium

User: free-uk
  Enabled: False
  Variation: off
  Rule Key:

User: premium-us
  Enabled: True
  Variation: control
  Rule Key: ab_test_premium

User: beta-tester
  Enabled: True
  Variation: treatment
  Rule Key: beta_delivery"
                        }
                    }
                },
                new Lesson
                {
                    Id = "uct-qualified-segments",
                    ModuleId = "user-context-targeting",
                    Title = "Qualified Segments",
                    Summary = "Use real-time segments and external data for advanced targeting.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand qualified segments",
                        "Integrate external segment data",
                        "Use real-time audiences",
                        "Handle segment qualification in code"
                    },
                    Content = @"
<h2>Qualified Segments</h2>
<p>Beyond simple attributes, you can use qualified segments for more sophisticated targeting based on external data sources or computed user segments.</p>

<h3>What are Qualified Segments?</h3>
<p>Qualified segments are segment identifiers that indicate a user belongs to specific groups computed outside of Optimizely. These might come from:</p>
<ul>
    <li>Customer Data Platforms (CDPs) like Segment</li>
    <li>Your own user classification systems</li>
    <li>Third-party audience providers</li>
    <li>Real-time computation engines</li>
</ul>

<h3>Using Qualified Segments</h3>
<p>Pass segment identifiers to the user context, then use them for targeting:</p>

<h3>Real-Time Audiences</h3>
<p>Optimizely Real-Time Audiences integrates with external data platforms to automatically qualify users into segments. Supported integrations include:</p>
<ul>
    <li>Segment</li>
    <li>Tealium</li>
    <li>Amplitude</li>
    <li>Mixpanel</li>
</ul>

<h3>Integration Pattern</h3>
<ol>
    <li>External system computes user segments</li>
    <li>Segment IDs are passed to your application</li>
    <li>Your code qualifies the user for those segments</li>
    <li>Optimizely uses segments for targeting</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "qualified-segments-code",
                            Title = "Using Qualified Segments",
                            Description = "Qualify users for segments from external data",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Create user context
var user = optimizely.CreateUserContext(""user-123"", new UserAttributes
{
    { ""country"", ""UK"" },
    { ""plan_type"", ""premium"" }
});

// Qualify user for segments from your CDP or classification system
// These segment IDs should match audiences created in Optimizely

// Example: User is in ""high_value"" and ""tech_enthusiast"" segments
user.QualifyForSegment(""high_value"");
user.QualifyForSegment(""tech_enthusiast"");

// Or qualify for multiple segments at once
// user.QualifyForSegments(new[] { ""high_value"", ""tech_enthusiast"" });

// Now decisions will consider these segments for targeting
var decision = user.Decide(""personalized_recommendations"");

Console.WriteLine($""Enabled: {decision.Enabled}"");
Console.WriteLine($""Variation: {decision.VariationKey}"");

// Check what segments the user is qualified for
var qualifiedSegments = user.GetQualifiedSegments();
Console.WriteLine($""Qualified segments: {string.Join("", "", qualifiedSegments)}"");",
                            IsInteractive = false,
                            SampleResponse = @"Enabled: True
Variation: tech_recommendations
Qualified segments: high_value, tech_enthusiast"
                        }
                    }
                },
                new Lesson
                {
                    Id = "uct-bot-filtering",
                    ModuleId = "user-context-targeting",
                    Title = "Bot Filtering",
                    Summary = "Filter bot traffic to ensure clean experiment data.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand why bot filtering matters",
                        "Configure bot filtering in Optimizely",
                        "Implement additional bot detection",
                        "Handle edge cases"
                    },
                    Content = @"
<h2>Bot Filtering</h2>
<p>Bot traffic can skew your experiment results by adding noise to your data. Optimizely provides built-in bot filtering, but you may want additional protection.</p>

<h3>Why Filter Bots?</h3>
<ul>
    <li><strong>Data quality</strong> - Bots don't represent real user behaviour</li>
    <li><strong>Accurate metrics</strong> - Bot traffic inflates impression and event counts</li>
    <li><strong>Valid statistics</strong> - Statistical significance requires clean data</li>
    <li><strong>Cost efficiency</strong> - Don't use impressions quota on bots</li>
</ul>

<h3>Built-in Bot Filtering</h3>
<p>Optimizely automatically filters known bots using:</p>
<ul>
    <li>User agent detection (known bot user agents)</li>
    <li>IP address analysis</li>
    <li>Behavioural patterns</li>
</ul>

<h3>Enabling Bot Filtering</h3>
<ol>
    <li>Go to <strong>Settings</strong> in your project</li>
    <li>Enable <strong>Bot Filtering</strong></li>
    <li>Choose the filtering level (basic or aggressive)</li>
</ol>

<h3>Additional Bot Detection</h3>
<p>For sensitive experiments, implement additional bot detection:</p>

<h3>Client-Side Detection (JavaScript)</h3>
<ul>
    <li>Check for missing JavaScript execution</li>
    <li>Detect automated browsing patterns</li>
    <li>Use CAPTCHA for high-value actions</li>
</ul>

<h3>Server-Side Detection</h3>
<ul>
    <li>Analyse user agent strings</li>
    <li>Check request patterns and timing</li>
    <li>Implement rate limiting</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "bot-detection-code",
                            Title = "Server-Side Bot Detection",
                            Description = "Exclude bots from experiments in your code",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

public class ExperimentService
{
    private readonly Optimizely _optimizely;
    private readonly IBotDetectionService _botDetection;

    public ExperimentService(Optimizely optimizely, IBotDetectionService botDetection)
    {
        _optimizely = optimizely;
        _botDetection = botDetection;
    }

    public OptimizelyDecision GetDecision(string flagKey, HttpContext context, string userId)
    {
        // Check if request is from a bot
        if (_botDetection.IsBot(context.Request))
        {
            // Return a default decision without tracking
            return CreateDefaultDecision(flagKey);
        }

        // Create user context with bot indicator
        var user = _optimizely.CreateUserContext(userId, new UserAttributes
        {
            { ""is_bot"", false },
            { ""user_agent"", context.Request.Headers[""User-Agent""].ToString() }
        });

        return user.Decide(flagKey);
    }

    private OptimizelyDecision CreateDefaultDecision(string flagKey)
    {
        // Return an off decision for bots
        return new OptimizelyDecision(
            variationKey: ""off"",
            enabled: false,
            variables: new OptimizelyJSON(new Dictionary<string, object>()),
            ruleKey: null,
            flagKey: flagKey,
            userContext: null,
            reasons: new List<string> { ""Bot traffic filtered"" }
        );
    }
}

public interface IBotDetectionService
{
    bool IsBot(HttpRequest request);
}

public class BotDetectionService : IBotDetectionService
{
    private static readonly string[] BotUserAgents = new[]
    {
        ""Googlebot"", ""Bingbot"", ""Slurp"", ""DuckDuckBot"",
        ""Baiduspider"", ""YandexBot"", ""Sogou"", ""facebot"",
        ""ia_archiver"", ""MJ12bot"", ""AhrefsBot"", ""SemrushBot""
    };

    public bool IsBot(HttpRequest request)
    {
        var userAgent = request.Headers[""User-Agent""].ToString();

        // Check against known bot user agents
        if (BotUserAgents.Any(bot =>
            userAgent.Contains(bot, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        // Additional checks
        // - Missing or suspicious headers
        // - Request patterns
        // - IP reputation (if using a service)

        return false;
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

    #region Module 5: The Decide Method

    private LearningModule BuildDecideMethodModule()
    {
        return new LearningModule
        {
            Id = "decide-method",
            Title = "The Decide Method",
            Description = "Master the decide method for feature flag decisions, including options, multiple flags, and decision handling.",
            Icon = "arrow-path",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "user-context-targeting" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "dm-understanding-decide",
                    ModuleId = "decide-method",
                    Title = "Understanding the Decide Method",
                    Summary = "Learn how the decide method evaluates flags and returns decisions.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what the decide method does",
                        "Know what information a decision contains",
                        "Handle enabled vs disabled flags",
                        "Access variation and variable information"
                    },
                    Content = @"
<h2>The Decide Method</h2>
<p>The <code>Decide</code> method is the primary way to get feature flag decisions. It evaluates a flag for a user and returns a comprehensive decision object.</p>

<h3>How Decide Works</h3>
<ol>
    <li>Looks up the flag by key in the datafile</li>
    <li>Evaluates each rule's audience conditions in order</li>
    <li>For the first matching rule, buckets the user into a variation</li>
    <li>Returns a decision with the variation and variable values</li>
    <li>Sends an impression event (unless disabled)</li>
</ol>

<h3>The Decision Object</h3>
<p>The <code>OptimizelyDecision</code> object contains:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Property</th>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Enabled</td><td class=""px-4 py-2"">bool</td><td class=""px-4 py-2"">Whether the flag is enabled for this user</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">VariationKey</td><td class=""px-4 py-2"">string</td><td class=""px-4 py-2"">The key of the assigned variation</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Variables</td><td class=""px-4 py-2"">OptimizelyJSON</td><td class=""px-4 py-2"">Variable values for this variation</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">RuleKey</td><td class=""px-4 py-2"">string</td><td class=""px-4 py-2"">The key of the matched rule</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">FlagKey</td><td class=""px-4 py-2"">string</td><td class=""px-4 py-2"">The flag that was evaluated</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">UserContext</td><td class=""px-4 py-2"">OptimizelyUserContext</td><td class=""px-4 py-2"">The user context used</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">Reasons</td><td class=""px-4 py-2"">List&lt;string&gt;</td><td class=""px-4 py-2"">Reasons explaining the decision</td></tr>
    </tbody>
</table>

<h3>Enabled vs Disabled</h3>
<p>A decision can be disabled for several reasons:</p>
<ul>
    <li>The flag is turned off</li>
    <li>The user doesn't match any rule's audience</li>
    <li>The user is bucketed into the ""off"" variation</li>
    <li>The flag doesn't exist</li>
</ul>

<h3>Using the Decision</h3>
<p>Always check <code>Enabled</code> before using the flag:</p>
<pre class=""bg-gray-100 dark:bg-gray-800 p-4 rounded-lg overflow-x-auto"">
if (decision.Enabled)
{
    // Show the feature
}
else
{
    // Show the default/control experience
}
</pre>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "basic-decide",
                            Title = "Basic Decide Usage",
                            Description = "Make a simple flag decision",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Create user context
var user = optimizely.CreateUserContext(""user-123"", new UserAttributes
{
    { ""plan_type"", ""premium"" },
    { ""country"", ""UK"" }
});

// Get a decision for a flag
var decision = user.Decide(""new_checkout"");

// Inspect the decision
Console.WriteLine($""Flag: {decision.FlagKey}"");
Console.WriteLine($""Enabled: {decision.Enabled}"");
Console.WriteLine($""Variation: {decision.VariationKey}"");
Console.WriteLine($""Rule: {decision.RuleKey}"");

// Use the decision
if (decision.Enabled)
{
    // Get variable values
    var buttonColor = decision.Variables.GetValue<string>(""button_color"") ?? ""blue"";
    var showBanner = decision.Variables.GetValue<bool>(""show_banner"");

    ShowNewCheckout(buttonColor, showBanner);
}
else
{
    ShowOriginalCheckout();
}",
                            IsInteractive = false,
                            SampleResponse = @"Flag: new_checkout
Enabled: True
Variation: treatment
Rule: ab_test_premium_users"
                        }
                    }
                },
                new Lesson
                {
                    Id = "dm-decision-options",
                    ModuleId = "decide-method",
                    Title = "Decision Options",
                    Summary = "Control decision behaviour with options like disabling tracking and including reasons.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand available decision options",
                        "Disable decision event tracking when needed",
                        "Include detailed reasons for debugging",
                        "Exclude variables for performance"
                    },
                    Content = @"
<h2>Decision Options</h2>
<p>The decide method accepts options that modify its behaviour. These are useful for performance optimisation, debugging, and special cases.</p>

<h3>Available Options</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Option</th>
            <th class=""px-4 py-2 text-left"">Description</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">DISABLE_DECISION_EVENT</td><td class=""px-4 py-2"">Don't send an impression event</td><td class=""px-4 py-2"">Pre-fetching, server-side rendering</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">ENABLED_FLAGS_ONLY</td><td class=""px-4 py-2"">Only return decisions for enabled flags</td><td class=""px-4 py-2"">Filtering</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">IGNORE_USER_PROFILE_SERVICE</td><td class=""px-4 py-2"">Don't use stored bucketing</td><td class=""px-4 py-2"">Testing</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">INCLUDE_REASONS</td><td class=""px-4 py-2"">Include detailed decision reasons</td><td class=""px-4 py-2"">Debugging</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">EXCLUDE_VARIABLES</td><td class=""px-4 py-2"">Don't include variable values</td><td class=""px-4 py-2"">Performance</td></tr>
    </tbody>
</table>

<h3>When to Disable Decision Events</h3>
<p>Disable decision events when:</p>
<ul>
    <li><strong>Pre-fetching</strong> - Getting decisions before the user sees the feature</li>
    <li><strong>Server-side rendering</strong> - You'll track on the client instead</li>
    <li><strong>Multiple calls</strong> - You only want to track once</li>
    <li><strong>Testing</strong> - You don't want test data in results</li>
</ul>

<h3>Using Include Reasons</h3>
<p>The <code>INCLUDE_REASONS</code> option provides detailed explanations for debugging. The reasons array will contain messages like:</p>
<ul>
    <li>""User bucketed into variation 'treatment' for rule 'ab_test'""</li>
    <li>""User excluded from audience 'premium_users'""</li>
    <li>""Flag 'my_flag' is turned off""</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "decision-options-code",
                            Title = "Using Decision Options",
                            Description = "Configure decide behaviour with options",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.OptimizelyDecisions;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"");

// Basic decision (sends impression event)
var decision1 = user.Decide(""my_flag"");

// Disable impression tracking
var decision2 = user.Decide(""my_flag"", new[] { OptimizelyDecideOption.DISABLE_DECISION_EVENT });

// Include detailed reasons for debugging
var decision3 = user.Decide(""my_flag"", new[] { OptimizelyDecideOption.INCLUDE_REASONS });
foreach (var reason in decision3.Reasons)
{
    Console.WriteLine($""Reason: {reason}"");
}

// Combine multiple options
var decision4 = user.Decide(""my_flag"", new[]
{
    OptimizelyDecideOption.DISABLE_DECISION_EVENT,
    OptimizelyDecideOption.INCLUDE_REASONS,
    OptimizelyDecideOption.EXCLUDE_VARIABLES
});

// Pre-fetch decisions without tracking, then track later
var prefetchedDecision = user.Decide(""checkout_flow"",
    new[] { OptimizelyDecideOption.DISABLE_DECISION_EVENT });

// Later, when user actually sees the feature...
if (prefetchedDecision.Enabled)
{
    // Track the impression manually
    user.TrackEvent(""checkout_impression"");
    ShowCheckout(prefetchedDecision.VariationKey);
}",
                            IsInteractive = false,
                            SampleResponse = @"Reason: User ""user-123"" is in variation ""treatment"" of experiment ""checkout_test""
Reason: User bucketed into traffic allocation
Reason: Rule ""ab_test_rule"" matched user"
                        }
                    }
                },
                new Lesson
                {
                    Id = "dm-multiple-flags",
                    ModuleId = "decide-method",
                    Title = "Deciding Multiple Flags",
                    Summary = "Efficiently get decisions for multiple flags at once.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Decide multiple specific flags at once",
                        "Decide all flags for a user",
                        "Understand performance benefits",
                        "Filter decisions by enabled status"
                    },
                    Content = @"
<h2>Deciding Multiple Flags</h2>
<p>When you need decisions for multiple flags, use batch methods for efficiency rather than calling decide multiple times.</p>

<h3>DecideForKeys</h3>
<p>Get decisions for specific flags by their keys:</p>

<h3>DecideAll</h3>
<p>Get decisions for all flags in the project:</p>

<h3>Benefits of Batch Decisions</h3>
<ul>
    <li><strong>Single evaluation</strong> - User context evaluated once</li>
    <li><strong>Efficient tracking</strong> - Impression events batched</li>
    <li><strong>Reduced overhead</strong> - Less method call overhead</li>
</ul>

<h3>Filtering Results</h3>
<p>Use the <code>ENABLED_FLAGS_ONLY</code> option to get only enabled flags:</p>

<h3>Use Cases</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Use Case</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-mono"">Decide</td><td class=""px-4 py-2"">Single flag check at point of use</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">DecideForKeys</td><td class=""px-4 py-2"">Known set of flags for a page/feature</td></tr>
        <tr><td class=""px-4 py-2 font-mono"">DecideAll</td><td class=""px-4 py-2"">Initial page load, client hydration</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "multiple-flags-code",
                            Title = "Batch Flag Decisions",
                            Description = "Get decisions for multiple flags efficiently",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.OptimizelyDecisions;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"", new UserAttributes
{
    { ""plan_type"", ""premium"" }
});

// Decide for specific flags
var flagKeys = new[] { ""new_header"", ""checkout_flow"", ""search_algorithm"" };
var decisions = user.DecideForKeys(flagKeys);

foreach (var (flagKey, decision) in decisions)
{
    Console.WriteLine($""{flagKey}: {decision.Enabled} - {decision.VariationKey}"");
}

// Decide all flags
var allDecisions = user.DecideAll();
Console.WriteLine($""Total flags: {allDecisions.Count}"");

// Decide all enabled flags only
var enabledDecisions = user.DecideAll(
    new[] { OptimizelyDecideOption.ENABLED_FLAGS_ONLY }
);
Console.WriteLine($""Enabled flags: {enabledDecisions.Count}"");

// Common pattern: hydrate client-side state
var clientFlags = user.DecideForKeys(
    new[] { ""ui_theme"", ""feature_banner"", ""promo_widget"" },
    new[] { OptimizelyDecideOption.DISABLE_DECISION_EVENT }
);

// Pass to client as JSON
var clientState = clientFlags.ToDictionary(
    kvp => kvp.Key,
    kvp => new
    {
        enabled = kvp.Value.Enabled,
        variation = kvp.Value.VariationKey,
        variables = kvp.Value.Variables.ToDictionary()
    }
);",
                            IsInteractive = false,
                            SampleResponse = @"new_header: True - minimal
checkout_flow: True - streamlined
search_algorithm: False - off
Total flags: 15
Enabled flags: 8"
                        }
                    }
                },
                new Lesson
                {
                    Id = "dm-forced-decisions",
                    ModuleId = "decide-method",
                    Title = "Forced Decisions",
                    Summary = "Override flag decisions for testing and QA purposes.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand when to use forced decisions",
                        "Set forced variations for testing",
                        "Remove forced decisions",
                        "Use forced decisions in QA workflows"
                    },
                    Content = @"
<h2>Forced Decisions</h2>
<p>Forced decisions let you override the normal bucketing for a user, ensuring they see a specific variation. This is essential for testing and QA.</p>

<h3>When to Use Forced Decisions</h3>
<ul>
    <li><strong>QA testing</strong> - Test each variation manually</li>
    <li><strong>Development</strong> - Work on a specific variation</li>
    <li><strong>Demos</strong> - Show a specific experience</li>
    <li><strong>Support</strong> - Reproduce what a user is seeing</li>
</ul>

<h3>How Forced Decisions Work</h3>
<ol>
    <li>Set a forced decision on the user context</li>
    <li>All subsequent decide calls for that flag return the forced variation</li>
    <li>No impression event is sent (user wasn't actually bucketed)</li>
    <li>Forced decision persists until removed or context is discarded</li>
</ol>

<h3>Forced Decision vs Rule-Level Forcing</h3>
<p>You can force at two levels:</p>
<ul>
    <li><strong>Flag-level</strong> - Force a variation for the entire flag</li>
    <li><strong>Rule-level</strong> - Force a variation for a specific rule within the flag</li>
</ul>

<h3>Important Considerations</h3>
<ul>
    <li>Forced decisions don't persist across sessions by default</li>
    <li>They don't affect other users</li>
    <li>They should not be used in production for real users</li>
    <li>Results pages won't show forced impressions</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "forced-decisions-code",
                            Title = "Using Forced Decisions",
                            Description = "Override flag decisions for testing",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.OptimizelyDecisions;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""qa-tester-123"");

// Normal decision (would be bucketed normally)
var normalDecision = user.Decide(""checkout_flow"");
Console.WriteLine($""Normal: {normalDecision.VariationKey}"");

// Force a specific variation for the flag
var context = new OptimizelyDecisionContext(""checkout_flow"", null);
var forcedDecision = new OptimizelyForcedDecision(""streamlined"");
user.SetForcedDecision(context, forcedDecision);

// Now all decisions for this flag return the forced variation
var decision1 = user.Decide(""checkout_flow"");
Console.WriteLine($""Forced: {decision1.VariationKey}""); // Always ""streamlined""

// Force at the rule level (for a specific experiment)
var ruleContext = new OptimizelyDecisionContext(""checkout_flow"", ""ab_test_rule"");
var ruleForced = new OptimizelyForcedDecision(""express"");
user.SetForcedDecision(ruleContext, ruleForced);

// Get the forced decision (for debugging)
var currentForced = user.GetForcedDecision(context);
Console.WriteLine($""Currently forced to: {currentForced?.VariationKey}"");

// Remove the forced decision
user.RemoveForcedDecision(context);

// Or remove all forced decisions
user.RemoveAllForcedDecisions();

// Back to normal bucketing
var finalDecision = user.Decide(""checkout_flow"");
Console.WriteLine($""After removal: {finalDecision.VariationKey}"");",
                            IsInteractive = false,
                            SampleResponse = @"Normal: control
Forced: streamlined
Currently forced to: streamlined
After removal: control"
                        },
                        new LessonExample
                        {
                            Id = "qa-workflow",
                            Title = "QA Testing Workflow",
                            Description = "Implement a QA override mechanism via query string",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.OptimizelyDecisions;
using Microsoft.AspNetCore.Http;

public class OptimizelyQaMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Optimizely _optimizely;

    public OptimizelyQaMiddleware(RequestDelegate next, Optimizely optimizely)
    {
        _next = next;
        _optimizely = optimizely;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check for QA override query parameter
        // Example: ?optimizely_force=checkout_flow:streamlined
        var forceParam = context.Request.Query[""optimizely_force""].ToString();

        if (!string.IsNullOrEmpty(forceParam) && IsQaUser(context))
        {
            var userId = GetUserId(context);
            var user = _optimizely.CreateUserContext(userId);

            // Parse and apply forced decisions
            foreach (var force in forceParam.Split(','))
            {
                var parts = force.Split(':');
                if (parts.Length == 2)
                {
                    var flagKey = parts[0];
                    var variationKey = parts[1];

                    var decisionContext = new OptimizelyDecisionContext(flagKey, null);
                    var forcedDecision = new OptimizelyForcedDecision(variationKey);
                    user.SetForcedDecision(decisionContext, forcedDecision);
                }
            }

            // Store user context for this request
            context.Items[""OptimizelyUser""] = user;
        }

        await _next(context);
    }

    private bool IsQaUser(HttpContext context)
    {
        // Only allow QA overrides for authorised testers
        // Check IP, header, cookie, or authentication
        return context.Request.Headers[""X-QA-Token""] == ""secret-qa-token"";
    }

    private string GetUserId(HttpContext context) => ""qa-user"";
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "dm-decision-reasons",
                    ModuleId = "decide-method",
                    Title = "Understanding Decision Reasons",
                    Summary = "Debug decisions using detailed reasons to understand why users see specific variations.",
                    Order = 5,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Enable and read decision reasons",
                        "Debug unexpected decisions",
                        "Log reasons for troubleshooting",
                        "Build diagnostic tools"
                    },
                    Content = @"
<h2>Decision Reasons</h2>
<p>When debugging why a user sees a particular variation, the <code>INCLUDE_REASONS</code> option provides detailed explanations of the decision process.</p>

<h3>Enabling Reasons</h3>
<p>Pass the <code>INCLUDE_REASONS</code> option to the decide method:</p>

<h3>Common Reason Messages</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Reason</th>
            <th class=""px-4 py-2 text-left"">Meaning</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 text-sm"">""User is bucketed into variation 'X'""</td><td class=""px-4 py-2"">Normal bucketing result</td></tr>
        <tr><td class=""px-4 py-2 text-sm"">""User does not meet audience conditions""</td><td class=""px-4 py-2"">Audience didn't match</td></tr>
        <tr><td class=""px-4 py-2 text-sm"">""Feature flag is not enabled""</td><td class=""px-4 py-2"">Flag is turned off</td></tr>
        <tr><td class=""px-4 py-2 text-sm"">""No rule matched for user""</td><td class=""px-4 py-2"">No rules applied</td></tr>
        <tr><td class=""px-4 py-2 text-sm"">""Forced decision applied""</td><td class=""px-4 py-2"">Override in effect</td></tr>
        <tr><td class=""px-4 py-2 text-sm"">""User not in traffic allocation""</td><td class=""px-4 py-2"">Outside percentage rollout</td></tr>
    </tbody>
</table>

<h3>Debugging Strategy</h3>
<ol>
    <li>Reproduce the issue with specific user ID and attributes</li>
    <li>Get decision with <code>INCLUDE_REASONS</code></li>
    <li>Review the reasons array for unexpected behaviour</li>
    <li>Check audience conditions match user attributes</li>
    <li>Verify rule ordering and traffic allocation</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "debug-decisions",
                            Title = "Debugging Decisions",
                            Description = "Build a diagnostic endpoint for debugging",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.OptimizelyDecisions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(""api/[controller]"")]
public class DiagnosticsController : ControllerBase
{
    private readonly Optimizely _optimizely;

    public DiagnosticsController(Optimizely optimizely)
    {
        _optimizely = optimizely;
    }

    [HttpGet(""decision/{flagKey}"")]
    public IActionResult DebugDecision(
        string flagKey,
        [FromQuery] string userId,
        [FromQuery] string? planType,
        [FromQuery] string? country)
    {
        var attributes = new UserAttributes();
        if (!string.IsNullOrEmpty(planType))
            attributes[""plan_type""] = planType;
        if (!string.IsNullOrEmpty(country))
            attributes[""country""] = country;

        var user = _optimizely.CreateUserContext(userId, attributes);

        var decision = user.Decide(flagKey, new[]
        {
            OptimizelyDecideOption.INCLUDE_REASONS,
            OptimizelyDecideOption.DISABLE_DECISION_EVENT
        });

        return Ok(new
        {
            flagKey = decision.FlagKey,
            enabled = decision.Enabled,
            variationKey = decision.VariationKey,
            ruleKey = decision.RuleKey,
            reasons = decision.Reasons,
            userContext = new
            {
                userId = userId,
                attributes = attributes
            },
            variables = decision.Variables.ToDictionary()
        });
    }
}

// Example response:
// GET /api/diagnostics/decision/checkout_flow?userId=user-123&planType=free&country=UK
// {
//   ""flagKey"": ""checkout_flow"",
//   ""enabled"": false,
//   ""variationKey"": ""off"",
//   ""ruleKey"": null,
//   ""reasons"": [
//     ""Evaluating audiences for rule 'premium_ab_test'"",
//     ""User does not meet audience condition: plan_type = 'premium'"",
//     ""No matching rule found for user""
//   ],
//   ""userContext"": { ""userId"": ""user-123"", ""attributes"": { ""plan_type"": ""free"", ""country"": ""UK"" } },
//   ""variables"": {}
// }",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 6: A/B Testing

    private LearningModule BuildABTestingModule()
    {
        return new LearningModule
        {
            Id = "ab-testing",
            Title = "A/B Testing",
            Description = "Run controlled experiments to measure the impact of feature variations on user behaviour.",
            Icon = "chart-bar",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "decide-method" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ab-introduction",
                    ModuleId = "ab-testing",
                    Title = "Introduction to A/B Testing",
                    Summary = "Understand the fundamentals of A/B testing and when to use it.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what A/B testing is",
                        "Know when to use A/B testing vs other methods",
                        "Learn the key components of an A/B test",
                        "Understand statistical significance"
                    },
                    Content = @"
<h2>Introduction to A/B Testing</h2>
<p>A/B testing (also known as split testing) is a method of comparing two or more versions of a feature to determine which performs better against a defined goal.</p>

<h3>What is A/B Testing?</h3>
<p>In an A/B test:</p>
<ul>
    <li>Users are randomly assigned to different <strong>variations</strong></li>
    <li>Their behaviour is measured against defined <strong>metrics</strong></li>
    <li>Statistical analysis determines if differences are <strong>significant</strong></li>
    <li>The winning variation can be rolled out to all users</li>
</ul>

<h3>A/B Testing vs Other Methods</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Method</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
            <th class=""px-4 py-2 text-left"">Best For</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">A/B Test</td><td class=""px-4 py-2"">Measure impact with statistical rigor</td><td class=""px-4 py-2"">Validating changes before rollout</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Targeted Delivery</td><td class=""px-4 py-2"">Roll out without measuring</td><td class=""px-4 py-2"">Feature releases, gradual rollouts</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Multi-Armed Bandit</td><td class=""px-4 py-2"">Optimise during the test</td><td class=""px-4 py-2"">Short-term campaigns, promotions</td></tr>
    </tbody>
</table>

<h3>Components of an A/B Test</h3>
<ul>
    <li><strong>Hypothesis</strong> - What you expect to happen (e.g., ""A green button will increase clicks"")</li>
    <li><strong>Variations</strong> - Different versions to test (control vs treatment)</li>
    <li><strong>Primary Metric</strong> - The main measure of success</li>
    <li><strong>Secondary Metrics</strong> - Additional measures to watch</li>
    <li><strong>Sample Size</strong> - Number of users needed for significance</li>
    <li><strong>Duration</strong> - How long to run the test</li>
</ul>

<h3>Statistical Significance</h3>
<p>Statistical significance tells you if the difference between variations is real or due to chance:</p>
<ul>
    <li><strong>p-value</strong> - Probability the difference is due to chance (lower is better)</li>
    <li><strong>Confidence level</strong> - Typically 95% (p < 0.05)</li>
    <li><strong>Confidence interval</strong> - Range where the true effect likely falls</li>
</ul>

<h3>When to Use A/B Testing</h3>
<ul>
    <li>You want to <strong>measure impact</strong> before full rollout</li>
    <li>You have enough traffic for statistical significance</li>
    <li>The test can run long enough to collect sufficient data</li>
    <li>You need to justify the change with data</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ab-creating-test",
                    ModuleId = "ab-testing",
                    Title = "Creating an A/B Test",
                    Summary = "Set up your first A/B test in Optimizely Feature Experimentation.",
                    Order = 2,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Create an A/B test rule on a feature flag",
                        "Configure variations and traffic allocation",
                        "Set up metrics for measurement",
                        "Launch and monitor the test"
                    },
                    Content = @"
<h2>Creating an A/B Test</h2>
<p>A/B tests in Feature Experimentation are created as rules on feature flags. Let's walk through the process.</p>

<h3>Step 1: Create or Select a Flag</h3>
<ol>
    <li>Navigate to <strong>Flags</strong> in the Optimizely app</li>
    <li>Create a new flag or select an existing one</li>
    <li>Ensure you have the variations you want to test</li>
</ol>

<h3>Step 2: Add an A/B Test Rule</h3>
<ol>
    <li>Select your environment (e.g., Production)</li>
    <li>Click <strong>Add Rule</strong></li>
    <li>Select <strong>A/B Test</strong></li>
    <li>Give your test a descriptive <strong>name</strong></li>
</ol>

<h3>Step 3: Configure Variations</h3>
<ul>
    <li>Select which variations to include in the test</li>
    <li>Set traffic allocation percentages</li>
    <li>Typically start with 50/50 for two variations</li>
</ul>

<h3>Step 4: Set Up Metrics</h3>
<ul>
    <li><strong>Primary metric</strong> - The main success measure</li>
    <li><strong>Secondary metrics</strong> - Additional measures to track</li>
    <li>Choose from existing events or create new ones</li>
</ul>

<h3>Step 5: Configure Audiences (Optional)</h3>
<ul>
    <li>Target specific user segments</li>
    <li>Or test with all users (no audience)</li>
</ul>

<h3>Step 6: Launch the Test</h3>
<ol>
    <li>Review your configuration</li>
    <li>Click <strong>Run</strong> to start the test</li>
    <li>The test is now live and collecting data</li>
</ol>

<h3>Test Status</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Status</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Draft</td><td class=""px-4 py-2"">Test is being configured</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Running</td><td class=""px-4 py-2"">Test is live and collecting data</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Paused</td><td class=""px-4 py-2"">Test is temporarily stopped</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Concluded</td><td class=""px-4 py-2"">Test has ended</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "ab-test-code",
                            Title = "A/B Test in Code",
                            Description = "Handle A/B test variations in your application",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Create user context
var user = optimizely.CreateUserContext(""user-123"", new UserAttributes
{
    { ""plan_type"", ""premium"" }
});

// Get the A/B test decision
var decision = user.Decide(""checkout_flow"");

// The decision tells us which variation the user is in
Console.WriteLine($""User in variation: {decision.VariationKey}"");
Console.WriteLine($""Rule matched: {decision.RuleKey}"");

// Implement the variations
if (decision.Enabled)
{
    switch (decision.VariationKey)
    {
        case ""control"":
            // Original checkout experience
            ShowClassicCheckout();
            break;

        case ""treatment"":
            // New streamlined checkout
            var steps = decision.Variables.GetValue<int>(""num_steps"") ?? 3;
            ShowStreamlinedCheckout(steps);
            break;

        default:
            // Fallback to original
            ShowClassicCheckout();
            break;
    }
}
else
{
    // Flag is off or user not in test
    ShowClassicCheckout();
}

// When the user completes checkout, track the conversion
user.TrackEvent(""purchase"", new EventTags
{
    { ""revenue"", 9999 }, // Revenue in cents
    { ""value"", 99.99 }
});",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ab-traffic-allocation",
                    ModuleId = "ab-testing",
                    Title = "Traffic Allocation",
                    Summary = "Control how users are distributed across variations.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand traffic allocation",
                        "Configure variation percentages",
                        "Use traffic hold-back",
                        "Ramp up traffic safely"
                    },
                    Content = @"
<h2>Traffic Allocation</h2>
<p>Traffic allocation determines what percentage of users are included in your test and how they're distributed across variations.</p>

<h3>Two Levels of Allocation</h3>
<ol>
    <li><strong>Rule-level allocation</strong> - What % of matching users enter the test</li>
    <li><strong>Variation allocation</strong> - How users are split between variations</li>
</ol>

<h3>Example</h3>
<p>If you set:</p>
<ul>
    <li>Rule allocation: 50% of traffic</li>
    <li>Variation split: 50% control, 50% treatment</li>
</ul>
<p>Then:</p>
<ul>
    <li>50% of users don't enter the test (flag disabled)</li>
    <li>25% of users see control</li>
    <li>25% of users see treatment</li>
</ul>

<h3>Traffic Ramping Strategy</h3>
<p>For risky changes, ramp up traffic gradually:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Phase</th>
            <th class=""px-4 py-2 text-left"">Traffic</th>
            <th class=""px-4 py-2 text-left"">Duration</th>
            <th class=""px-4 py-2 text-left"">Purpose</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1</td><td class=""px-4 py-2"">5%</td><td class=""px-4 py-2"">1-2 days</td><td class=""px-4 py-2"">Catch critical bugs</td></tr>
        <tr><td class=""px-4 py-2"">2</td><td class=""px-4 py-2"">25%</td><td class=""px-4 py-2"">3-5 days</td><td class=""px-4 py-2"">Monitor metrics</td></tr>
        <tr><td class=""px-4 py-2"">3</td><td class=""px-4 py-2"">50%</td><td class=""px-4 py-2"">1-2 weeks</td><td class=""px-4 py-2"">Gather statistical data</td></tr>
        <tr><td class=""px-4 py-2"">4</td><td class=""px-4 py-2"">100%</td><td class=""px-4 py-2"">Until significance</td><td class=""px-4 py-2"">Full test</td></tr>
    </tbody>
</table>

<h3>Consistent Bucketing</h3>
<p>When you change traffic allocation:</p>
<ul>
    <li>Users who were in a variation stay in that variation</li>
    <li>New users may be added to existing variations</li>
    <li>This prevents users from switching variations mid-test</li>
</ul>

<h3>Mutual Exclusion</h3>
<p>If you're running multiple tests, you might want to ensure users are only in one test at a time. Use mutual exclusion groups for this.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "traffic-allocation-visual",
                            Title = "Traffic Allocation Visualization",
                            Description = "How traffic flows through allocation",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Traffic Allocation Example: Checkout A/B Test

Total Users (100%)
│
├─[Rule Allocation: 50%]─────────────────────────────────────┐
│                                                             │
│  ┌─────────────────────────────────────────────────────┐   │
│  │  Users IN Test (50%)                                │   │
│  │                                                     │   │
│  │  ┌──────────────┐    ┌──────────────┐             │   │
│  │  │ Control (50%)│    │Treatment(50%)│              │   │
│  │  │              │    │              │              │   │
│  │  │ 25% of total │    │ 25% of total │              │   │
│  │  │              │    │              │              │   │
│  │  │ Classic      │    │ Streamlined  │              │   │
│  │  │ Checkout     │    │ Checkout     │              │   │
│  │  └──────────────┘    └──────────────┘              │   │
│  │                                                     │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                             │
├─[Not in Rule: 50%]─────────────────────────────────────────┘
│
│  ┌─────────────────────────────────────────────────────┐
│  │  Users NOT in Test (50%)                            │
│  │                                                     │
│  │  Flag returns: enabled = false                      │
│  │  Experience: Default/Original                       │
│  └─────────────────────────────────────────────────────┘",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ab-analyzing-results",
                    ModuleId = "ab-testing",
                    Title = "Analyzing Results",
                    Summary = "Interpret A/B test results and make data-driven decisions.",
                    Order = 4,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Read and interpret the results page",
                        "Understand statistical significance",
                        "Know when to call a test",
                        "Make decisions based on results"
                    },
                    Content = @"
<h2>Analyzing A/B Test Results</h2>
<p>The results page provides statistical analysis of your test. Understanding these metrics is key to making good decisions.</p>

<h3>Key Metrics on Results Page</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Metric</th>
            <th class=""px-4 py-2 text-left"">Description</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Visitors</td><td class=""px-4 py-2"">Unique users who saw each variation</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Conversions</td><td class=""px-4 py-2"">Users who triggered the metric event</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Conversion Rate</td><td class=""px-4 py-2"">Conversions / Visitors</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Improvement</td><td class=""px-4 py-2"">% change vs control</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Statistical Significance</td><td class=""px-4 py-2"">Confidence the result is real</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Confidence Interval</td><td class=""px-4 py-2"">Range of likely true improvement</td></tr>
    </tbody>
</table>

<h3>Understanding Statistical Significance</h3>
<ul>
    <li><strong>95% significance</strong> = 5% chance result is due to random chance</li>
    <li><strong>99% significance</strong> = 1% chance result is due to random chance</li>
    <li>Wait for significance before making decisions</li>
</ul>

<h3>When to Call a Test</h3>
<p>Call a test when:</p>
<ul>
    <li>You've reached statistical significance (typically 95%+)</li>
    <li>You have enough conversions (typically 100+ per variation)</li>
    <li>The test has run long enough (at least one full business cycle)</li>
    <li>Results have stabilised (not still fluctuating)</li>
</ul>

<h3>Common Mistakes</h3>
<ul>
    <li><strong>Peeking</strong> - Checking results too early and stopping prematurely</li>
    <li><strong>Ignoring secondary metrics</strong> - A win on primary might hurt other metrics</li>
    <li><strong>Not waiting for full cycles</strong> - Behaviour varies by day/week</li>
    <li><strong>Small sample sizes</strong> - Results aren't reliable with few users</li>
</ul>

<h3>Decision Framework</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Result</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Treatment wins (significant)</td><td class=""px-4 py-2"">Roll out treatment to 100%</td></tr>
        <tr><td class=""px-4 py-2"">Control wins (significant)</td><td class=""px-4 py-2"">Keep control, iterate on treatment</td></tr>
        <tr><td class=""px-4 py-2"">No significant difference</td><td class=""px-4 py-2"">Consider simpler implementation or other factors</td></tr>
        <tr><td class=""px-4 py-2"">Mixed results (win/lose)</td><td class=""px-4 py-2"">Dig deeper, segment analysis</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "results-interpretation",
                            Title = "Sample Results Interpretation",
                            Description = "How to read and act on A/B test results",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"A/B Test: Checkout Flow Redesign
Duration: 14 days
Primary Metric: Purchase Completion Rate

═══════════════════════════════════════════════════════════════
RESULTS SUMMARY
═══════════════════════════════════════════════════════════════

Variation      Visitors    Conversions    Rate      vs Control
───────────────────────────────────────────────────────────────
Control        12,456      1,495          12.0%     baseline
Treatment      12,389      1,610          13.0%     +8.3%

═══════════════════════════════════════════════════════════════
STATISTICAL ANALYSIS
═══════════════════════════════════════════════════════════════

Statistical Significance: 97.2% ✓
Confidence Interval: +4.1% to +12.5%

Interpretation:
- Treatment shows 8.3% improvement in purchase completion
- 97.2% confidence this is a real effect (not chance)
- True improvement likely between 4.1% and 12.5%

═══════════════════════════════════════════════════════════════
SECONDARY METRICS
═══════════════════════════════════════════════════════════════

Average Order Value:
- Control: $84.50
- Treatment: $82.30 (-2.6%, not significant)

Cart Abandonment:
- Control: 68%
- Treatment: 61% (-10.3%, significant ✓)

═══════════════════════════════════════════════════════════════
RECOMMENDATION
═══════════════════════════════════════════════════════════════

✓ Roll out Treatment
- Primary metric shows significant improvement
- Secondary metrics neutral or positive
- Consider monitoring AOV post-rollout",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ab-best-practices",
                    ModuleId = "ab-testing",
                    Title = "A/B Testing Best Practices",
                    Summary = "Learn proven strategies for running effective A/B tests.",
                    Order = 5,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Design effective experiments",
                        "Avoid common pitfalls",
                        "Calculate required sample size",
                        "Document and share learnings"
                    },
                    Content = @"
<h2>A/B Testing Best Practices</h2>
<p>Follow these best practices to run effective experiments and get reliable results.</p>

<h3>Before the Test</h3>
<ul>
    <li><strong>Document your hypothesis</strong> - ""We believe [change] will [impact] because [reason]""</li>
    <li><strong>Calculate sample size</strong> - Know how many users you need</li>
    <li><strong>Define success metrics</strong> - Primary and secondary metrics</li>
    <li><strong>Set a runtime</strong> - How long will you run the test?</li>
    <li><strong>Get stakeholder alignment</strong> - Agree on decision criteria upfront</li>
</ul>

<h3>During the Test</h3>
<ul>
    <li><strong>Don't peek excessively</strong> - Checking daily increases false positive rate</li>
    <li><strong>Monitor for errors</strong> - Watch for bugs, not just metrics</li>
    <li><strong>Don't change the test</strong> - Modifications invalidate results</li>
    <li><strong>Let it run</strong> - Avoid stopping early based on initial results</li>
</ul>

<h3>After the Test</h3>
<ul>
    <li><strong>Document results</strong> - Record what you learned</li>
    <li><strong>Share learnings</strong> - Help the team learn from all tests</li>
    <li><strong>Implement winning variation</strong> - Or iterate if no winner</li>
    <li><strong>Clean up</strong> - Archive the test, remove flag if permanent</li>
</ul>

<h3>Sample Size Calculation</h3>
<p>You need enough users to detect a meaningful difference. Factors:</p>
<ul>
    <li><strong>Baseline conversion rate</strong> - Current performance</li>
    <li><strong>Minimum detectable effect</strong> - Smallest improvement worth detecting</li>
    <li><strong>Statistical power</strong> - Typically 80%</li>
    <li><strong>Significance level</strong> - Typically 95% (5% false positive rate)</li>
</ul>

<h3>Test One Thing at a Time</h3>
<p>For clear results:</p>
<ul>
    <li>Test one major change per experiment</li>
    <li>If testing multiple changes, use multivariate testing</li>
    <li>Small, incremental tests often beat big redesigns</li>
</ul>

<h3>Common Pitfalls to Avoid</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Pitfall</th>
            <th class=""px-4 py-2 text-left"">Solution</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Stopping tests early</td><td class=""px-4 py-2"">Pre-define runtime and stick to it</td></tr>
        <tr><td class=""px-4 py-2"">Testing too many variations</td><td class=""px-4 py-2"">Limit to 2-4 variations</td></tr>
        <tr><td class=""px-4 py-2"">Ignoring seasonality</td><td class=""px-4 py-2"">Run tests for full business cycles</td></tr>
        <tr><td class=""px-4 py-2"">Not tracking enough metrics</td><td class=""px-4 py-2"">Add secondary and guardrail metrics</td></tr>
        <tr><td class=""px-4 py-2"">Inconsistent user experience</td><td class=""px-4 py-2"">Ensure bucketing is consistent</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "hypothesis-template",
                            Title = "Experiment Documentation Template",
                            Description = "Template for documenting A/B test experiments",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"═══════════════════════════════════════════════════════════════
EXPERIMENT BRIEF: Streamlined Checkout
═══════════════════════════════════════════════════════════════

HYPOTHESIS
───────────────────────────────────────────────────────────────
We believe that reducing checkout from 3 steps to 2 steps
will increase purchase completion rate because users
will experience less friction and abandonment.

METRICS
───────────────────────────────────────────────────────────────
Primary: Purchase completion rate
Secondary: Average order value, Time to purchase
Guardrail: Error rate, Customer support tickets

VARIATIONS
───────────────────────────────────────────────────────────────
Control: Current 3-step checkout
Treatment: New 2-step checkout (shipping + payment combined)

TARGETING
───────────────────────────────────────────────────────────────
Audience: All users
Traffic allocation: 50% / 50%

TEST PARAMETERS
───────────────────────────────────────────────────────────────
Minimum detectable effect: 5% relative improvement
Required sample size: ~20,000 users per variation
Planned runtime: 14 days (2 full weeks)
Start date: 2024-02-01

DECISION CRITERIA
───────────────────────────────────────────────────────────────
Ship Treatment if:
- Primary metric improves >= 5% with 95% significance
- No significant degradation in secondary metrics
- Guardrail metrics remain stable

STAKEHOLDERS
───────────────────────────────────────────────────────────────
Owner: Jane Smith (Product)
Engineering: John Doe
Analytics: Alice Johnson",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 7: Targeted Rollouts

    private LearningModule BuildTargetedRolloutsModule()
    {
        return new LearningModule
        {
            Id = "targeted-rollouts",
            Title = "Targeted Rollouts",
            Description = "Gradually release features to specific user segments with controlled rollouts.",
            Icon = "arrow-trending-up",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "ab-testing" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "tr-introduction",
                    ModuleId = "targeted-rollouts",
                    Title = "Introduction to Targeted Rollouts",
                    Summary = "Understand when and why to use targeted delivery rules.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of targeted rollouts",
                        "Know when to use rollouts vs A/B tests",
                        "Learn rollout strategies"
                    },
                    Content = @"
<h2>Introduction to Targeted Rollouts</h2>
<p>Targeted rollouts (also called flag delivery) let you release features to specific users or percentages of your user base without running an experiment.</p>

<h3>Rollouts vs A/B Tests</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Targeted Rollout</th>
            <th class=""px-4 py-2 text-left"">A/B Test</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Purpose</td><td class=""px-4 py-2"">Release a feature</td><td class=""px-4 py-2"">Measure impact</td></tr>
        <tr><td class=""px-4 py-2"">Assignment</td><td class=""px-4 py-2"">All matching users get same variation</td><td class=""px-4 py-2"">Random assignment</td></tr>
        <tr><td class=""px-4 py-2"">Statistics</td><td class=""px-4 py-2"">No statistical analysis</td><td class=""px-4 py-2"">Full statistical analysis</td></tr>
        <tr><td class=""px-4 py-2"">Best for</td><td class=""px-4 py-2"">Feature releases, beta access</td><td class=""px-4 py-2"">Validating changes</td></tr>
    </tbody>
</table>

<h3>Common Rollout Strategies</h3>
<ul>
    <li><strong>Percentage rollout</strong> - Release to X% of all users</li>
    <li><strong>Audience-based</strong> - Release to specific user segments</li>
    <li><strong>Staged rollout</strong> - Gradually increase percentage over time</li>
    <li><strong>Geographic rollout</strong> - Release by region/country</li>
</ul>

<h3>When to Use Rollouts</h3>
<ul>
    <li>You're confident in the feature and don't need to measure impact</li>
    <li>You want to give early access to beta users</li>
    <li>You're doing a phased release to manage risk</li>
    <li>You're using feature flags as a kill switch</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "tr-percentage-rollouts",
                    ModuleId = "targeted-rollouts",
                    Title = "Percentage Rollouts",
                    Summary = "Release features to a percentage of your user base.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create percentage-based rollouts",
                        "Understand how percentage bucketing works",
                        "Ramp up rollout percentages safely"
                    },
                    Content = @"
<h2>Percentage Rollouts</h2>
<p>Percentage rollouts let you release a feature to a specific percentage of users, with the option to gradually increase over time.</p>

<h3>Creating a Percentage Rollout</h3>
<ol>
    <li>Open your flag and select the environment</li>
    <li>Click <strong>Add Rule</strong> → <strong>Targeted Delivery</strong></li>
    <li>Select the variation to deliver</li>
    <li>Set the <strong>traffic percentage</strong></li>
    <li>Optionally add audience targeting</li>
    <li>Click <strong>Run</strong></li>
</ol>

<h3>How Percentage Rollouts Work</h3>
<ul>
    <li>Users are bucketed based on their user ID (deterministic)</li>
    <li>Same user always gets the same result</li>
    <li>Increasing percentage adds new users, doesn't change existing</li>
</ul>

<h3>Staged Rollout Example</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Day</th>
            <th class=""px-4 py-2 text-left"">Percentage</th>
            <th class=""px-4 py-2 text-left"">Action</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1</td><td class=""px-4 py-2"">5%</td><td class=""px-4 py-2"">Initial release, monitor closely</td></tr>
        <tr><td class=""px-4 py-2"">3</td><td class=""px-4 py-2"">25%</td><td class=""px-4 py-2"">Increase if no issues</td></tr>
        <tr><td class=""px-4 py-2"">5</td><td class=""px-4 py-2"">50%</td><td class=""px-4 py-2"">Continue monitoring</td></tr>
        <tr><td class=""px-4 py-2"">7</td><td class=""px-4 py-2"">100%</td><td class=""px-4 py-2"">Full rollout</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "rollout-code",
                            Title = "Handling Rollout in Code",
                            Description = "Code for percentage-based rollout",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"");

var decision = user.Decide(""new_feature"");

// With a percentage rollout:
// - decision.Enabled will be true for users in the rollout
// - decision.RuleKey will be the delivery rule name
// - No random variation assignment - all users get the configured variation

if (decision.Enabled)
{
    Console.WriteLine($""User is in the {decision.RuleKey} rollout"");
    ShowNewFeature();
}
else
{
    Console.WriteLine(""User is not in the rollout"");
    ShowOriginalFeature();
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "tr-audience-rollouts",
                    ModuleId = "targeted-rollouts",
                    Title = "Audience-Based Rollouts",
                    Summary = "Release features to specific user segments.",
                    Order = 3,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Target rollouts to specific audiences",
                        "Combine audience and percentage targeting",
                        "Prioritise rollout rules"
                    },
                    Content = @"
<h2>Audience-Based Rollouts</h2>
<p>Target your rollouts to specific user segments using audiences.</p>

<h3>Use Cases</h3>
<ul>
    <li><strong>Beta testers</strong> - Give early access to engaged users</li>
    <li><strong>Premium users</strong> - Release features to paying customers first</li>
    <li><strong>Geographic</strong> - Release to one region before others</li>
    <li><strong>Internal users</strong> - Test with employees first</li>
</ul>

<h3>Creating Audience-Based Rollout</h3>
<ol>
    <li>Add a Targeted Delivery rule</li>
    <li>Select your variation</li>
    <li>In <strong>Audiences</strong>, add your target audiences</li>
    <li>Set percentage (usually 100% for audience rollouts)</li>
    <li>Run the rule</li>
</ol>

<h3>Combining Multiple Rules</h3>
<p>You can have multiple delivery rules for layered rollouts:</p>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Order</th>
            <th class=""px-4 py-2 text-left"">Rule</th>
            <th class=""px-4 py-2 text-left"">Audience</th>
            <th class=""px-4 py-2 text-left"">Traffic</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">1</td><td class=""px-4 py-2"">Beta Access</td><td class=""px-4 py-2"">Beta Testers</td><td class=""px-4 py-2"">100%</td></tr>
        <tr><td class=""px-4 py-2"">2</td><td class=""px-4 py-2"">Premium Rollout</td><td class=""px-4 py-2"">Premium Users</td><td class=""px-4 py-2"">50%</td></tr>
        <tr><td class=""px-4 py-2"">3</td><td class=""px-4 py-2"">General Rollout</td><td class=""px-4 py-2"">All Users</td><td class=""px-4 py-2"">10%</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "tr-kill-switches",
                    ModuleId = "targeted-rollouts",
                    Title = "Kill Switches and Rollbacks",
                    Summary = "Use flags as emergency kill switches to disable features instantly.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Implement kill switch patterns",
                        "Perform emergency rollbacks",
                        "Design for failure scenarios"
                    },
                    Content = @"
<h2>Kill Switches and Rollbacks</h2>
<p>One of the most valuable uses of feature flags is the ability to instantly disable a feature if something goes wrong.</p>

<h3>Kill Switch Pattern</h3>
<p>A kill switch lets you instantly turn off a feature for all users:</p>
<ul>
    <li>No code deployment required</li>
    <li>Takes effect within seconds (after datafile update)</li>
    <li>Can be triggered by anyone with access</li>
</ul>

<h3>Implementing Kill Switches</h3>
<ol>
    <li>Wrap the feature in a flag check</li>
    <li>If issues arise, simply turn off the flag</li>
    <li>All users immediately see the fallback experience</li>
</ol>

<h3>Emergency Rollback Process</h3>
<ol>
    <li>Identify the issue</li>
    <li>Navigate to the flag in Optimizely</li>
    <li>Turn off the flag or pause all rules</li>
    <li>Verify the rollback</li>
    <li>Investigate and fix the issue</li>
    <li>Re-enable when ready</li>
</ol>

<h3>Best Practices</h3>
<ul>
    <li>Always have a fallback experience coded</li>
    <li>Test the fallback path regularly</li>
    <li>Document which flags are kill switches</li>
    <li>Set up alerts for when flags are turned off</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "kill-switch-code",
                            Title = "Kill Switch Implementation",
                            Description = "Implement a feature with kill switch capability",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

public class PaymentService
{
    private readonly Optimizely _optimizely;

    public PaymentService(Optimizely optimizely)
    {
        _optimizely = optimizely;
    }

    public async Task<PaymentResult> ProcessPayment(string userId, PaymentRequest request)
    {
        var user = _optimizely.CreateUserContext(userId);

        // Kill switch: new_payment_provider
        // If issues occur, turn off this flag to revert to old provider
        var decision = user.Decide(""new_payment_provider"");

        if (decision.Enabled)
        {
            try
            {
                return await ProcessWithNewProvider(request);
            }
            catch (Exception ex)
            {
                // Log the error - might want to disable flag if errors spike
                _logger.LogError(ex, ""New payment provider failed"");

                // Optionally fall back automatically
                return await ProcessWithOldProvider(request);
            }
        }
        else
        {
            // Flag is off - use original provider
            return await ProcessWithOldProvider(request);
        }
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

    #region Module 8: Multi-Armed Bandits

    private LearningModule BuildMultiArmedBanditsModule()
    {
        return new LearningModule
        {
            Id = "multi-armed-bandits",
            Title = "Multi-Armed Bandits",
            Description = "Use machine learning to automatically optimise traffic allocation to best-performing variations.",
            Icon = "cpu-chip",
            Order = 8,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "ab-testing" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "mab-introduction",
                    ModuleId = "multi-armed-bandits",
                    Title = "Introduction to Multi-Armed Bandits",
                    Summary = "Understand MAB optimization and when to use it.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what multi-armed bandit optimization is",
                        "Know the difference between MAB and A/B testing",
                        "Learn when MAB is the right choice"
                    },
                    Content = @"
<h2>Multi-Armed Bandits</h2>
<p>A Multi-Armed Bandit (MAB) is an optimization algorithm that automatically shifts traffic towards better-performing variations while the test runs.</p>

<h3>How MAB Differs from A/B Testing</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">A/B Test</th>
            <th class=""px-4 py-2 text-left"">Multi-Armed Bandit</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Traffic split</td><td class=""px-4 py-2"">Fixed (e.g., 50/50)</td><td class=""px-4 py-2"">Dynamic, shifts to winners</td></tr>
        <tr><td class=""px-4 py-2"">Goal</td><td class=""px-4 py-2"">Learn which is best</td><td class=""px-4 py-2"">Maximise conversions now</td></tr>
        <tr><td class=""px-4 py-2"">Statistical rigour</td><td class=""px-4 py-2"">High (significance testing)</td><td class=""px-4 py-2"">Lower (optimisation focus)</td></tr>
        <tr><td class=""px-4 py-2"">Best for</td><td class=""px-4 py-2"">Long-term decisions</td><td class=""px-4 py-2"">Short-term campaigns</td></tr>
    </tbody>
</table>

<h3>The MAB Algorithm</h3>
<ol>
    <li>Start with even traffic distribution</li>
    <li>Measure conversion rates for each variation</li>
    <li>Shift more traffic to better performers</li>
    <li>Continue optimising throughout the campaign</li>
</ol>

<h3>When to Use MAB</h3>
<ul>
    <li><strong>Time-limited campaigns</strong> - Sales, promotions, events</li>
    <li><strong>Headlines/copy testing</strong> - Quick optimisation</li>
    <li><strong>High opportunity cost</strong> - Can't afford to show poor variations</li>
    <li><strong>You don't need statistical rigour</strong> - Optimisation over learning</li>
</ul>

<h3>When NOT to Use MAB</h3>
<ul>
    <li>You need to understand <em>why</em> a variation won</li>
    <li>You need statistical significance for stakeholders</li>
    <li>You're making permanent product decisions</li>
    <li>You want to segment results</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "mab-creating",
                    ModuleId = "multi-armed-bandits",
                    Title = "Creating a MAB Optimization",
                    Summary = "Set up and configure a multi-armed bandit experiment.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create a MAB rule in Optimizely",
                        "Configure variations and metrics",
                        "Monitor MAB performance"
                    },
                    Content = @"
<h2>Creating a MAB Optimization</h2>
<p>Setting up a MAB is similar to an A/B test, but the traffic allocation will be managed automatically.</p>

<h3>Steps to Create MAB</h3>
<ol>
    <li>Open your flag and select the environment</li>
    <li>Click <strong>Add Rule</strong> → <strong>Multi-Armed Bandit</strong></li>
    <li>Give your optimization a name</li>
    <li>Select the variations to include</li>
    <li>Choose your <strong>primary metric</strong> (what to optimise for)</li>
    <li>Optionally add audience targeting</li>
    <li>Click <strong>Run</strong></li>
</ol>

<h3>Choosing the Right Metric</h3>
<p>The primary metric is what the algorithm optimises for:</p>
<ul>
    <li>Choose a metric with relatively high conversion rate</li>
    <li>Ensure the metric fires soon after exposure</li>
    <li>Consider using click events for faster optimisation</li>
</ul>

<h3>Monitoring MAB</h3>
<p>On the results page you'll see:</p>
<ul>
    <li>Current traffic allocation per variation</li>
    <li>Conversion rates</li>
    <li>How allocation has changed over time</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "mab-code",
                            Title = "MAB in Code",
                            Description = "Code implementation is identical to other rules",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"");

// MAB decisions work exactly like A/B test decisions
// The SDK doesn't know if it's an A/B test or MAB
var decision = user.Decide(""headline_test"");

if (decision.Enabled)
{
    // Show the variation the MAB algorithm selected
    var headline = decision.Variables.GetValue<string>(""headline"");
    ShowHeadline(headline);
}

// Track the conversion event the MAB is optimising for
user.TrackEvent(""headline_click"");",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 9: Contextual Multi-Armed Bandits

    private LearningModule BuildContextualBanditsModule()
    {
        return new LearningModule
        {
            Id = "contextual-bandits",
            Title = "Contextual Multi-Armed Bandits",
            Description = "Personalise experiences using machine learning that considers user context.",
            Icon = "sparkles",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "multi-armed-bandits" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "cmab-introduction",
                    ModuleId = "contextual-bandits",
                    Title = "Introduction to Contextual Bandits",
                    Summary = "Understand CMAB and how it personalises based on user attributes.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand what CMAB is and how it differs from MAB",
                        "Know how user attributes drive personalisation",
                        "Learn when CMAB is appropriate"
                    },
                    Content = @"
<h2>Contextual Multi-Armed Bandits (CMAB)</h2>
<p>CMAB extends the MAB concept by considering user context (attributes) when selecting variations. Instead of finding one best variation for all users, CMAB finds the best variation <em>for each type of user</em>.</p>

<h3>CMAB vs Standard MAB</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Aspect</th>
            <th class=""px-4 py-2 text-left"">Standard MAB</th>
            <th class=""px-4 py-2 text-left"">Contextual MAB</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Optimisation</td><td class=""px-4 py-2"">One winner for all users</td><td class=""px-4 py-2"">Different winners per user segment</td></tr>
        <tr><td class=""px-4 py-2"">Context</td><td class=""px-4 py-2"">Ignores user attributes</td><td class=""px-4 py-2"">Uses attributes for personalisation</td></tr>
        <tr><td class=""px-4 py-2"">Powered by</td><td class=""px-4 py-2"">Simple optimisation</td><td class=""px-4 py-2"">Opal (Optimizely AI)</td></tr>
    </tbody>
</table>

<h3>How CMAB Works</h3>
<ol>
    <li>You specify user attributes to use for personalisation</li>
    <li>The model learns which variations work best for different user types</li>
    <li>Over time, each user cohort gets optimised variations</li>
</ol>

<h3>Example</h3>
<p>Testing three homepage banners:</p>
<ul>
    <li>Mobile users respond best to Banner A</li>
    <li>Desktop users respond best to Banner C</li>
    <li>UK users respond best to Banner B</li>
</ul>
<p>CMAB learns these patterns and delivers personalised experiences.</p>

<h3>When to Use CMAB</h3>
<ul>
    <li>You suspect different users prefer different variations</li>
    <li>You have meaningful user attributes to use</li>
    <li>You have enough traffic for the model to learn</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "cmab-code",
                            Title = "CMAB with User Attributes",
                            Description = "Ensure you pass attributes for CMAB to use",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// For CMAB, pass the attributes that the model will use for personalisation
var user = optimizely.CreateUserContext(""user-123"", new UserAttributes
{
    // These attributes should match what you configured in the CMAB rule
    { ""device_type"", ""mobile"" },
    { ""country"", ""UK"" },
    { ""user_segment"", ""high_value"" },
    { ""is_returning"", true }
});

// The CMAB algorithm considers these attributes when selecting a variation
var decision = user.Decide(""homepage_banner"");

// Each user may get a different variation based on their attributes
Console.WriteLine($""User type: {user.GetUserId()}"");
Console.WriteLine($""Attributes: device_type=mobile, country=UK"");
Console.WriteLine($""Selected variation: {decision.VariationKey}"");

// Track the event the CMAB is optimising for
user.TrackEvent(""banner_click"");",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 10: Events and Analytics

    private LearningModule BuildEventsAnalyticsModule()
    {
        return new LearningModule
        {
            Id = "events-analytics",
            Title = "Events and Analytics",
            Description = "Track user events and measure the impact of your experiments.",
            Icon = "chart-pie",
            Order = 10,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "decide-method" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "ea-event-tracking",
                    ModuleId = "events-analytics",
                    Title = "Event Tracking Fundamentals",
                    Summary = "Track user actions to measure experiment outcomes.",
                    Order = 1,
                    EstimatedMinutes = 12,
                    LearningObjectives = new List<string>
                    {
                        "Understand what events are and why they matter",
                        "Create events in the Optimizely app",
                        "Track events from your code"
                    },
                    Content = @"
<h2>Event Tracking</h2>
<p>Events are user actions that you track to measure the success of your experiments. They form the basis of your metrics.</p>

<h3>Types of Events</h3>
<ul>
    <li><strong>Click events</strong> - Button clicks, link clicks</li>
    <li><strong>Page views</strong> - Visiting specific pages</li>
    <li><strong>Form submissions</strong> - Signups, contact forms</li>
    <li><strong>Purchases</strong> - Completed transactions</li>
    <li><strong>Custom events</strong> - Any action you want to track</li>
</ul>

<h3>Creating Events</h3>
<ol>
    <li>Navigate to <strong>Events</strong> in the Optimizely app</li>
    <li>Click <strong>Create Event</strong></li>
    <li>Enter an <strong>Event Key</strong> (e.g., <code>purchase</code>)</li>
    <li>Add a description</li>
    <li>Save the event</li>
</ol>

<h3>Event Keys</h3>
<p>Use consistent, descriptive event keys:</p>
<ul>
    <li><code>purchase</code> - Completed purchase</li>
    <li><code>add_to_cart</code> - Added item to cart</li>
    <li><code>signup_complete</code> - Completed signup</li>
    <li><code>feature_used</code> - Used a specific feature</li>
</ul>

<h3>Event Tags</h3>
<p>Add metadata to events for richer analysis:</p>
<ul>
    <li><strong>revenue</strong> - Monetary value (in cents)</li>
    <li><strong>value</strong> - Numeric value</li>
    <li>Custom tags for segmentation</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "track-event-code",
                            Title = "Tracking Events in C#",
                            Description = "Track events with the SDK",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");
var user = optimizely.CreateUserContext(""user-123"");

// Get decision (this records an impression)
var decision = user.Decide(""checkout_flow"");

// Simple event tracking
user.TrackEvent(""add_to_cart"");

// Event with revenue (for purchase events)
user.TrackEvent(""purchase"", new EventTags
{
    { ""revenue"", 4999 }, // Revenue in cents ($49.99)
    { ""value"", 49.99 }   // Optional: readable value
});

// Event with custom tags
user.TrackEvent(""signup_complete"", new EventTags
{
    { ""plan_type"", ""premium"" },
    { ""referral_source"", ""google"" }
});

// Track multiple events for a user journey
user.TrackEvent(""view_product"");
user.TrackEvent(""add_to_cart"");
user.TrackEvent(""begin_checkout"");
user.TrackEvent(""purchase"", new EventTags { { ""revenue"", 9999 } });",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "ea-metrics",
                    ModuleId = "events-analytics",
                    Title = "Metrics and Results",
                    Summary = "Configure metrics and analyse experiment results.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create metrics from events",
                        "Understand metric types",
                        "Read and interpret results"
                    },
                    Content = @"
<h2>Metrics and Results</h2>
<p>Metrics are calculations based on events that measure your experiment's success.</p>

<h3>Metric Types</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Type</th>
            <th class=""px-4 py-2 text-left"">Calculation</th>
            <th class=""px-4 py-2 text-left"">Example</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2 font-medium"">Conversion Rate</td><td class=""px-4 py-2"">Users who converted / Total users</td><td class=""px-4 py-2"">Purchase rate</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Numeric (Sum)</td><td class=""px-4 py-2"">Sum of event values</td><td class=""px-4 py-2"">Total revenue</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Numeric (Average)</td><td class=""px-4 py-2"">Average event value</td><td class=""px-4 py-2"">Avg order value</td></tr>
        <tr><td class=""px-4 py-2 font-medium"">Count</td><td class=""px-4 py-2"">Total event count</td><td class=""px-4 py-2"">Page views</td></tr>
    </tbody>
</table>

<h3>Primary vs Secondary Metrics</h3>
<ul>
    <li><strong>Primary</strong> - The main metric you're trying to improve</li>
    <li><strong>Secondary</strong> - Additional metrics to monitor</li>
    <li><strong>Guardrail</strong> - Metrics that shouldn't get worse</li>
</ul>

<h3>Results Interpretation</h3>
<p>On the results page, look for:</p>
<ul>
    <li><strong>Improvement</strong> - Percentage change vs control</li>
    <li><strong>Significance</strong> - Confidence the result is real</li>
    <li><strong>Confidence interval</strong> - Range of likely true effect</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "ea-integrations",
                    ModuleId = "events-analytics",
                    Title = "Analytics Integrations",
                    Summary = "Send experiment data to analytics platforms.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Integrate with analytics platforms",
                        "Send experiment data to GA4, Amplitude, etc.",
                        "Segment data by experiment variation"
                    },
                    Content = @"
<h2>Analytics Integrations</h2>
<p>Send experiment data to your existing analytics platforms for deeper analysis.</p>

<h3>Supported Integrations</h3>
<ul>
    <li>Google Analytics 4</li>
    <li>Amplitude</li>
    <li>Mixpanel</li>
    <li>Segment</li>
    <li>Adobe Analytics</li>
</ul>

<h3>Integration Approaches</h3>
<ol>
    <li><strong>Built-in integrations</strong> - Configure in Optimizely app</li>
    <li><strong>Notification listeners</strong> - Send data programmatically</li>
    <li><strong>Event forwarding</strong> - Forward events to third parties</li>
</ol>

<h3>Custom Integration Pattern</h3>
<p>Use notification listeners to send data to any analytics platform.</p>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "analytics-integration",
                            Title = "Custom Analytics Integration",
                            Description = "Send experiment data to your analytics platform",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.Notifications;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Add a decision notification listener
optimizely.NotificationCenter.AddNotification(
    NotificationCenter.NotificationType.Decision,
    (type, userId, userAttributes, decisionInfo) =>
    {
        var flagKey = decisionInfo[""flagKey""] as string;
        var variationKey = decisionInfo[""variationKey""] as string;
        var enabled = (bool)decisionInfo[""enabled""];

        // Send to your analytics platform
        Analytics.Track(""Experiment Viewed"", new Dictionary<string, object>
        {
            { ""experiment_id"", flagKey },
            { ""variation_id"", variationKey },
            { ""enabled"", enabled },
            { ""user_id"", userId }
        });

        // Or send to Google Analytics
        // gtag('event', 'experiment_viewed', {
        //     experiment_id: flagKey,
        //     variation_id: variationKey
        // });
    }
);",
                            IsInteractive = false
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 11: Advanced Topics

    private LearningModule BuildAdvancedTopicsModule()
    {
        return new LearningModule
        {
            Id = "advanced-topics",
            Title = "Advanced Topics",
            Description = "Master advanced features like webhooks, notification listeners, and mutual exclusion.",
            Icon = "cog-8-tooth",
            Order = 11,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "events-analytics" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "at-notification-listeners",
                    ModuleId = "advanced-topics",
                    Title = "Notification Listeners",
                    Summary = "React to SDK events with custom logic.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand notification types",
                        "Implement decision listeners",
                        "Use listeners for logging and analytics"
                    },
                    Content = @"
<h2>Notification Listeners</h2>
<p>Notification listeners let you execute custom code when specific SDK events occur.</p>

<h3>Notification Types</h3>
<ul>
    <li><strong>Decision</strong> - When a flag decision is made</li>
    <li><strong>Track</strong> - When an event is tracked</li>
    <li><strong>Log Event</strong> - When events are dispatched</li>
    <li><strong>Config Update</strong> - When datafile updates</li>
</ul>

<h3>Use Cases</h3>
<ul>
    <li>Send experiment data to analytics</li>
    <li>Log decisions for debugging</li>
    <li>Trigger side effects on decisions</li>
    <li>Monitor SDK health</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "notification-listener-code",
                            Title = "Implementing Notification Listeners",
                            Description = "Add listeners for SDK events",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;
using OptimizelySDK.Notifications;

var optimizely = OptimizelyFactory.NewDefaultInstance(""YOUR_SDK_KEY"");

// Decision listener - called when decide() is invoked
optimizely.NotificationCenter.AddNotification(
    NotificationCenter.NotificationType.Decision,
    (type, userId, userAttributes, decisionInfo) =>
    {
        Console.WriteLine($""Decision made for user {userId}"");
        Console.WriteLine($""  Flag: {decisionInfo[""flagKey""]}"");
        Console.WriteLine($""  Variation: {decisionInfo[""variationKey""]}"");
    }
);

// Track listener - called when trackEvent() is invoked
optimizely.NotificationCenter.AddNotification(
    NotificationCenter.NotificationType.Track,
    (eventKey, userId, userAttributes, eventTags, logEvent) =>
    {
        Console.WriteLine($""Event tracked: {eventKey} for user {userId}"");
    }
);

// Config update listener - called when datafile updates
optimizely.NotificationCenter.AddNotification(
    NotificationCenter.NotificationType.OptimizelyConfigUpdate,
    () =>
    {
        var config = optimizely.GetOptimizelyConfig();
        Console.WriteLine($""Datafile updated to revision: {config?.Revision}"");
    }
);",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "at-webhooks",
                    ModuleId = "advanced-topics",
                    Title = "Webhooks and Datafile Updates",
                    Summary = "Use webhooks to trigger actions when configuration changes.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Configure webhooks in Optimizely",
                        "Handle webhook payloads",
                        "Trigger datafile updates"
                    },
                    Content = @"
<h2>Webhooks</h2>
<p>Webhooks notify your server when changes are made in the Optimizely app.</p>

<h3>Webhook Events</h3>
<ul>
    <li><strong>project.datafile_updated</strong> - Datafile has changed</li>
</ul>

<h3>Setting Up Webhooks</h3>
<ol>
    <li>Go to <strong>Settings</strong> → <strong>Webhooks</strong></li>
    <li>Click <strong>Create Webhook</strong></li>
    <li>Enter your endpoint URL</li>
    <li>Select environments to monitor</li>
    <li>Save the webhook</li>
</ol>

<h3>Use Cases</h3>
<ul>
    <li>Trigger cache invalidation</li>
    <li>Update server-side datafile immediately</li>
    <li>Send notifications to Slack/Teams</li>
    <li>Audit configuration changes</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "at-mutual-exclusion",
                    ModuleId = "advanced-topics",
                    Title = "Mutual Exclusion Groups",
                    Summary = "Ensure users are only in one experiment at a time.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand why mutual exclusion matters",
                        "Create mutual exclusion groups",
                        "Design experiment traffic allocation"
                    },
                    Content = @"
<h2>Mutual Exclusion Groups</h2>
<p>Mutual exclusion ensures users are only bucketed into one experiment from a group, preventing interaction effects.</p>

<h3>Why Mutual Exclusion?</h3>
<p>If a user is in multiple experiments that affect the same feature, it's hard to know which experiment caused any observed effect.</p>

<h3>How It Works</h3>
<ol>
    <li>Create a group of related experiments</li>
    <li>Allocate traffic to each experiment</li>
    <li>A user can only be in one experiment from the group</li>
</ol>

<h3>Traffic Allocation Example</h3>
<table class=""min-w-full divide-y divide-gray-200 dark:divide-gray-700 my-4"">
    <thead>
        <tr>
            <th class=""px-4 py-2 text-left"">Experiment</th>
            <th class=""px-4 py-2 text-left"">Traffic</th>
        </tr>
    </thead>
    <tbody>
        <tr><td class=""px-4 py-2"">Checkout Test A</td><td class=""px-4 py-2"">30%</td></tr>
        <tr><td class=""px-4 py-2"">Checkout Test B</td><td class=""px-4 py-2"">30%</td></tr>
        <tr><td class=""px-4 py-2"">Not in any test</td><td class=""px-4 py-2"">40%</td></tr>
    </tbody>
</table>
",
                    Examples = new List<LessonExample>()
                }
            }
        };
    }

    #endregion

    #region Module 12: Best Practices and Deployment

    private LearningModule BuildBestPracticesModule()
    {
        return new LearningModule
        {
            Id = "best-practices",
            Title = "Best Practices and Deployment",
            Description = "Production-ready patterns for implementing Feature Experimentation.",
            Icon = "rocket-launch",
            Order = 12,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "advanced-topics" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "bp-implementation-checklist",
                    ModuleId = "best-practices",
                    Title = "Implementation Checklist",
                    Summary = "A comprehensive checklist for production implementations.",
                    Order = 1,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Follow a structured implementation process",
                        "Avoid common implementation mistakes",
                        "Ensure production readiness"
                    },
                    Content = @"
<h2>Implementation Checklist</h2>
<p>Follow this checklist to ensure a successful Feature Experimentation implementation.</p>

<h3>Planning Phase</h3>
<ul>
    <li>☐ Define experimentation goals and KPIs</li>
    <li>☐ Identify key features to flag</li>
    <li>☐ Plan attribute schema</li>
    <li>☐ Design event taxonomy</li>
    <li>☐ Set up environments</li>
</ul>

<h3>Development Phase</h3>
<ul>
    <li>☐ Install SDK</li>
    <li>☐ Configure DI and initialization</li>
    <li>☐ Implement user context creation</li>
    <li>☐ Add flag checks with fallbacks</li>
    <li>☐ Implement event tracking</li>
    <li>☐ Add error handling</li>
</ul>

<h3>Testing Phase</h3>
<ul>
    <li>☐ Test with forced decisions</li>
    <li>☐ Verify all variations work</li>
    <li>☐ Test fallback behaviour</li>
    <li>☐ Verify event tracking</li>
    <li>☐ Load test SDK integration</li>
</ul>

<h3>Launch Phase</h3>
<ul>
    <li>☐ Configure flags in production</li>
    <li>☐ Set up monitoring and alerts</li>
    <li>☐ Document flag ownership</li>
    <li>☐ Train team on Optimizely app</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "bp-error-handling",
                    ModuleId = "best-practices",
                    Title = "Error Handling and Resilience",
                    Summary = "Build resilient integrations that handle failures gracefully.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Handle SDK initialization failures",
                        "Implement graceful degradation",
                        "Monitor and alert on issues"
                    },
                    Content = @"
<h2>Error Handling</h2>
<p>Your integration should handle failures gracefully without breaking the user experience.</p>

<h3>Key Principles</h3>
<ul>
    <li><strong>Never block on SDK</strong> - Don't let SDK issues break your app</li>
    <li><strong>Always have defaults</strong> - If SDK fails, show a sensible default</li>
    <li><strong>Log and monitor</strong> - Track SDK health metrics</li>
    <li><strong>Timeout appropriately</strong> - Don't wait forever for datafile</li>
</ul>

<h3>Common Failure Scenarios</h3>
<ul>
    <li>SDK initialization fails</li>
    <li>Datafile fetch times out</li>
    <li>Event dispatch fails</li>
    <li>Invalid flag key</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "error-handling-code",
                            Title = "Resilient SDK Integration",
                            Description = "Handle failures without breaking user experience",
                            Type = ExampleType.Code,
                            ExampleContent = @"using OptimizelySDK;

public class ResilientOptimizelyService
{
    private readonly Optimizely? _optimizely;
    private readonly ILogger _logger;

    public ResilientOptimizelyService(IConfiguration config, ILogger<ResilientOptimizelyService> logger)
    {
        _logger = logger;

        try
        {
            var sdkKey = config[""Optimizely:SdkKey""];
            _optimizely = OptimizelyFactory.NewDefaultInstance(sdkKey);

            if (!_optimizely.IsValid)
            {
                _logger.LogWarning(""Optimizely SDK initialized but not valid"");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ""Failed to initialize Optimizely SDK"");
            _optimizely = null;
        }
    }

    public bool IsFeatureEnabled(string flagKey, string userId, UserAttributes? attributes = null)
    {
        // Return sensible default if SDK not available
        if (_optimizely == null || !_optimizely.IsValid)
        {
            _logger.LogWarning(""Optimizely not available, returning default for {FlagKey}"", flagKey);
            return false; // Default to off
        }

        try
        {
            var user = _optimizely.CreateUserContext(userId, attributes);
            var decision = user.Decide(flagKey);
            return decision.Enabled;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ""Error getting decision for {FlagKey}"", flagKey);
            return false; // Default to off on error
        }
    }
}",
                            IsInteractive = false
                        }
                    }
                },
                new Lesson
                {
                    Id = "bp-performance",
                    ModuleId = "best-practices",
                    Title = "Performance Optimisation",
                    Summary = "Optimise SDK performance for production workloads.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand SDK performance characteristics",
                        "Optimise for high-throughput scenarios",
                        "Monitor SDK performance"
                    },
                    Content = @"
<h2>Performance Optimisation</h2>
<p>The Optimizely SDK is designed for high performance, but there are ways to optimise further.</p>

<h3>SDK Performance</h3>
<ul>
    <li>Decisions are made locally using cached datafile</li>
    <li>No network calls for decisions</li>
    <li>Typical decision time: microseconds</li>
    <li>Events are batched and sent asynchronously</li>
</ul>

<h3>Optimisation Tips</h3>
<ul>
    <li><strong>Singleton pattern</strong> - Create one SDK instance per application</li>
    <li><strong>Batch decisions</strong> - Use DecideForKeys for multiple flags</li>
    <li><strong>Exclude variables</strong> - If you don't need them</li>
    <li><strong>Disable events</strong> - For pre-fetching scenarios</li>
</ul>

<h3>Monitoring</h3>
<p>Monitor these metrics:</p>
<ul>
    <li>Decision latency</li>
    <li>Datafile fetch failures</li>
    <li>Event dispatch failures</li>
    <li>SDK error rates</li>
</ul>
",
                    Examples = new List<LessonExample>()
                },
                new Lesson
                {
                    Id = "bp-testing-qa",
                    ModuleId = "best-practices",
                    Title = "Testing and QA Strategies",
                    Summary = "Test your Feature Experimentation integration thoroughly.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Write unit tests for flag-dependent code",
                        "Implement integration tests",
                        "Set up QA environments"
                    },
                    Content = @"
<h2>Testing Strategies</h2>
<p>Ensure your Feature Experimentation integration works correctly across all scenarios.</p>

<h3>Unit Testing</h3>
<ul>
    <li>Mock the Optimizely SDK</li>
    <li>Test each variation path</li>
    <li>Test fallback/default behaviour</li>
    <li>Test event tracking calls</li>
</ul>

<h3>Integration Testing</h3>
<ul>
    <li>Use a test datafile</li>
    <li>Test with different user attributes</li>
    <li>Verify bucketing consistency</li>
</ul>

<h3>QA Process</h3>
<ol>
    <li>Use forced decisions to test each variation</li>
    <li>Verify UI/UX for all variations</li>
    <li>Check events are tracked correctly</li>
    <li>Test with different user segments</li>
</ol>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "unit-test-code",
                            Title = "Unit Testing with Mocks",
                            Description = "Test flag-dependent code with mocked SDK",
                            Type = ExampleType.Code,
                            ExampleContent = @"using Moq;
using OptimizelySDK;
using Xunit;

public class CheckoutServiceTests
{
    [Fact]
    public void ProcessCheckout_StreamlinedVariation_UsesNewFlow()
    {
        // Arrange
        var mockOptimizely = new Mock<Optimizely>();
        var mockUser = new Mock<OptimizelyUserContext>();

        var mockDecision = new OptimizelyDecision(
            variationKey: ""streamlined"",
            enabled: true,
            variables: new OptimizelyJSON(new Dictionary<string, object>()),
            ruleKey: ""ab_test"",
            flagKey: ""checkout_flow"",
            userContext: mockUser.Object,
            reasons: new List<string>()
        );

        mockUser.Setup(u => u.Decide(""checkout_flow"", It.IsAny<OptimizelyDecideOption[]>()))
            .Returns(mockDecision);

        mockOptimizely.Setup(o => o.CreateUserContext(It.IsAny<string>(), It.IsAny<UserAttributes>()))
            .Returns(mockUser.Object);

        var service = new CheckoutService(mockOptimizely.Object);

        // Act
        var result = service.GetCheckoutFlow(""user-123"");

        // Assert
        Assert.Equal(""streamlined"", result);
    }

    [Fact]
    public void ProcessCheckout_FlagDisabled_UsesDefaultFlow()
    {
        // Arrange
        var mockOptimizely = new Mock<Optimizely>();
        var mockUser = new Mock<OptimizelyUserContext>();

        var mockDecision = new OptimizelyDecision(
            variationKey: ""off"",
            enabled: false,
            variables: new OptimizelyJSON(new Dictionary<string, object>()),
            ruleKey: null,
            flagKey: ""checkout_flow"",
            userContext: mockUser.Object,
            reasons: new List<string>()
        );

        mockUser.Setup(u => u.Decide(""checkout_flow"", It.IsAny<OptimizelyDecideOption[]>()))
            .Returns(mockDecision);

        mockOptimizely.Setup(o => o.CreateUserContext(It.IsAny<string>(), It.IsAny<UserAttributes>()))
            .Returns(mockUser.Object);

        var service = new CheckoutService(mockOptimizely.Object);

        // Act
        var result = service.GetCheckoutFlow(""user-123"");

        // Assert
        Assert.Equal(""classic"", result);
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
}
