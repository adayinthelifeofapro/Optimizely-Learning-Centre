using OptimizelyLearningCentre.Client.Models.Learning;
using OptimizelyLearningCentre.Client.Services;

namespace OptimizelyLearningCentre.Client.Courses.CMP;

/// <summary>
/// Content provider for the Optimizely Content Marketing Platform course
/// </summary>
public class CMPContentProvider : ILearningContentProvider
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
            BuildCampaignsModule(),
            BuildTasksWorkflowsModule(),
            BuildLibraryDAMModule(),
            BuildContentCreationModule(),
            BuildPublishingModule(),
            BuildWorkRequestsModule(),
            BuildAnalyticsModule(),
            BuildCollaborationModule(),
            BuildOpalIntegrationModule()
        };
    }

    #region Module 1: Getting Started with CMP

    private LearningModule BuildGettingStartedModule()
    {
        return new LearningModule
        {
            Id = "getting-started",
            Title = "Getting Started with CMP",
            Description = "Learn the fundamentals of Optimizely Content Marketing Platform.",
            Icon = "academic-cap",
            Order = 1,
            Difficulty = ModuleDifficulty.Beginner,
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "gs-what-is-cmp",
                    ModuleId = "getting-started",
                    Title = "What is Optimizely CMP?",
                    Summary = "Discover Optimizely Content Marketing Platform and how it transforms your marketing operations.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what Optimizely CMP is and its purpose",
                        "Learn how CMP brings marketing teams together",
                        "Discover the key benefits of using CMP"
                    },
                    Content = @"
<h2>Introduction to Optimizely CMP</h2>
<p>Optimizely Content Marketing Platform (CMP) is an <strong>AI-powered workspace</strong> purpose-built for marketers. It brings teams together to share plans, collaborate on assets, and execute campaigns from a single, unified platform.</p>

<h3>What Makes CMP Special?</h3>
<p>CMP lets you see the work your marketing organization is doing, including the entire content workflow - from brief to finalized content. Key characteristics include:</p>
<ul>
    <li><strong>Unified Planning</strong> - Plan campaigns, tasks, and content in one place</li>
    <li><strong>Team Collaboration</strong> - Bring marketing, creative, and stakeholders together</li>
    <li><strong>Workflow Automation</strong> - Configure approval processes and publishing workflows</li>
    <li><strong>Multi-Channel Publishing</strong> - Publish to social media, CMS, email, and more</li>
</ul>

<h3>Core Capabilities</h3>
<p>CMP offers best-in-class capabilities for modern marketing teams:</p>
<ul>
    <li><strong>Campaign Management</strong> - Plan and organize marketing initiatives</li>
    <li><strong>Task Management</strong> - Track work from creation to completion</li>
    <li><strong>Content Creation</strong> - Create and edit content with the omnichannel editor</li>
    <li><strong>Digital Asset Management</strong> - Store, organize, and distribute marketing assets</li>
    <li><strong>Analytics</strong> - Measure content performance and engagement</li>
</ul>

<h3>Key Benefits</h3>
<p>Organizations using Optimizely CMP experience:</p>
<ul>
    <li><strong>Streamlined Operations</strong> - Eliminate silos between marketing teams</li>
    <li><strong>Faster Time-to-Market</strong> - Reduce campaign completion time</li>
    <li><strong>Brand Consistency</strong> - Ensure on-brand content across channels</li>
    <li><strong>Better Visibility</strong> - See all marketing work in one place</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "gs-example-1",
                            Title = "CMP Core Modules",
                            Description = "Explore the main modules available in CMP.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"CMP Core Modules:

1. Campaigns
   - Plan marketing initiatives
   - Organize related work
   - Track campaign progress

2. Tasks
   - Create and assign work
   - Move through workflows
   - Collaborate on content

3. Library (DAM)
   - Store digital assets
   - Organize with folders and tags
   - Track asset usage

4. Calendar
   - View scheduled content
   - Plan publication dates
   - Coordinate team activities

5. Analytics
   - Measure performance
   - Track engagement
   - Generate reports",
                            SampleResponse = @"Each module in CMP serves a specific purpose:

- Campaigns provide the strategic layer for organizing marketing initiatives
- Tasks are where the actual work gets done
- The Library centralizes all your digital assets
- Calendar gives you visibility into your content schedule
- Analytics helps you understand what's working

These modules work together to give you complete visibility into your marketing operations.",
                            Hints = new List<string>
                            {
                                "Start with Campaigns to organize your marketing initiatives",
                                "Tasks automatically inherit campaign context when created within a campaign"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-navigation",
                    ModuleId = "getting-started",
                    Title = "Navigating CMP",
                    Summary = "Learn how to navigate the CMP interface and find what you need.",
                    Order = 2,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the main CMP interface",
                        "Understand the different views available",
                        "Find campaigns, tasks, and assets quickly"
                    },
                    Content = @"
<h2>The CMP Interface</h2>
<p>The CMP interface is designed to give you quick access to all your marketing work. Understanding the navigation will help you work more efficiently.</p>

<h3>Home Page</h3>
<p>The CMP home page displays performance views of your published content, efficiency views of your organization across tasks, and progress toward content marketing goals. You'll find:</p>
<ul>
    <li><strong>Content Marketing Performance Index (CMPI)</strong> - Overall performance score</li>
    <li><strong>Quick Actions</strong> - Create campaigns, tasks, and access recent work</li>
    <li><strong>Activity Feed</strong> - Recent activity across your organization</li>
    <li><strong>My Work</strong> - Tasks and campaigns assigned to you</li>
</ul>

<h3>Main Navigation</h3>
<p>The left sidebar provides access to main modules:</p>
<ul>
    <li><strong>Home</strong> - Dashboard and quick access</li>
    <li><strong>Campaigns</strong> - All campaigns and sub-campaigns</li>
    <li><strong>Tasks</strong> - Work items and content</li>
    <li><strong>Calendar</strong> - Timeline and scheduling views</li>
    <li><strong>Library</strong> - Digital asset management</li>
    <li><strong>Analytics</strong> - Performance metrics</li>
</ul>

<h3>View Options</h3>
<p>CMP offers multiple ways to view your work:</p>
<ul>
    <li><strong>List View</strong> - Detailed list with sorting and filtering</li>
    <li><strong>Calendar View</strong> - Work organized by scheduled dates</li>
    <li><strong>Timeline View</strong> - Gantt chart for long-term planning</li>
    <li><strong>Board View</strong> - Kanban-style agile boards</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "gs-glossary",
                    ModuleId = "getting-started",
                    Title = "CMP Terminology",
                    Summary = "Learn the key terms and concepts used throughout CMP.",
                    Order = 3,
                    EstimatedMinutes = 6,
                    LearningObjectives = new List<string>
                    {
                        "Understand CMP terminology",
                        "Learn the difference between campaigns, tasks, and events",
                        "Know what workflows and fields are"
                    },
                    Content = @"
<h2>Essential CMP Terminology</h2>
<p>Understanding these key terms will help you work effectively in CMP.</p>

<h3>Core Concepts</h3>
<table>
    <tr>
        <td><strong>Campaign</strong></td>
        <td>A container for organizing related marketing work. Campaigns can have tasks, events, milestones, and sub-campaigns.</td>
    </tr>
    <tr>
        <td><strong>Task</strong></td>
        <td>A piece of work that needs to be completed. Tasks move through workflows and can contain content.</td>
    </tr>
    <tr>
        <td><strong>Event</strong></td>
        <td>A scheduled occurrence, such as a product launch or webinar, that doesn't require workflow steps.</td>
    </tr>
    <tr>
        <td><strong>Milestone</strong></td>
        <td>A significant date or achievement within a campaign.</td>
    </tr>
</table>

<h3>Organization Concepts</h3>
<table>
    <tr>
        <td><strong>Workflow</strong></td>
        <td>A series of sequential steps that content moves through before publishing (e.g., Draft > Review > Approve > Publish).</td>
    </tr>
    <tr>
        <td><strong>Field</strong></td>
        <td>Metadata attached to campaigns, tasks, or assets. Includes labels, dropdowns, checkboxes, and more.</td>
    </tr>
    <tr>
        <td><strong>Brief</strong></td>
        <td>A document describing the objectives, audience, and requirements for a campaign or task.</td>
    </tr>
    <tr>
        <td><strong>Template</strong></td>
        <td>A reusable structure for creating campaigns, tasks, or work requests consistently.</td>
    </tr>
</table>

<h3>Content & Assets</h3>
<table>
    <tr>
        <td><strong>Library</strong></td>
        <td>The digital asset management system where all marketing assets are stored.</td>
    </tr>
    <tr>
        <td><strong>Asset</strong></td>
        <td>Any digital file stored in the Library (images, videos, documents, etc.).</td>
    </tr>
    <tr>
        <td><strong>Rendition</strong></td>
        <td>A version of an asset optimized for a specific channel or purpose.</td>
    </tr>
    <tr>
        <td><strong>Channel</strong></td>
        <td>A destination for publishing content (social media, CMS, email, etc.).</td>
    </tr>
</table>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "gs-glossary-example",
                            Title = "Campaign Structure Example",
                            Description = "See how CMP concepts work together.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Example: Product Launch Campaign

Campaign: ""Q2 Product Launch""
├── Brief: Target audience, key messages, objectives
├── Sub-Campaign: ""Social Media Push""
│   └── Tasks:
│       ├── ""Create launch announcement"" (Workflow: Draft > Review > Approve)
│       └── ""Design social graphics"" (Workflow: Brief > Design > Review)
├── Sub-Campaign: ""Email Marketing""
│   └── Tasks:
│       ├── ""Write launch email"" (Workflow: Draft > Review > Approve > Send)
│       └── ""Create landing page"" (Workflow: Draft > Review > Publish)
├── Event: ""Launch Day"" (May 15)
└── Milestone: ""All content approved"" (May 10)",
                            SampleResponse = @"This structure shows:

1. A parent campaign organizes all launch activities
2. Sub-campaigns group related work (social, email)
3. Each task has its own workflow defining the approval process
4. Events mark important dates without requiring workflow steps
5. Milestones track key achievements within the campaign

Fields and labels can be applied at any level to categorize and filter work."
                        }
                    }
                },
                new Lesson
                {
                    Id = "gs-first-steps",
                    ModuleId = "getting-started",
                    Title = "Your First Steps in CMP",
                    Summary = "Create your first campaign and task to get hands-on experience.",
                    Order = 4,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create your first campaign",
                        "Add a task to your campaign",
                        "Understand basic campaign settings"
                    },
                    Content = @"
<h2>Getting Started with Your First Campaign</h2>
<p>The best way to learn CMP is by creating your first campaign and task. Let's walk through the process.</p>

<h3>Creating a Campaign</h3>
<ol>
    <li>Click the <strong>+</strong> button in the top toolbar</li>
    <li>Select <strong>Campaign</strong></li>
    <li>Enter a campaign name and description</li>
    <li>Set start and end dates (optional for open-ended campaigns)</li>
    <li>Choose a color to identify your campaign in the calendar</li>
    <li>Add labels and custom fields as needed</li>
    <li>Click <strong>Create Campaign</strong></li>
</ol>

<h3>Campaign Tabs</h3>
<p>Once created, your campaign has several tabs:</p>
<ul>
    <li><strong>Brief</strong> - Document campaign objectives and requirements</li>
    <li><strong>Fields</strong> - View and edit metadata</li>
    <li><strong>Activities</strong> - Add tasks, events, milestones, and sub-campaigns</li>
    <li><strong>Content</strong> - View all content associated with the campaign</li>
    <li><strong>History</strong> - Track changes and activity</li>
</ul>

<h3>Adding Your First Task</h3>
<ol>
    <li>Open your campaign</li>
    <li>Go to the <strong>Activities</strong> tab</li>
    <li>Click <strong>Add > Task</strong></li>
    <li>Enter the task title and description</li>
    <li>Select a workflow (determines approval steps)</li>
    <li>Assign an owner and due date</li>
    <li>Click <strong>Create</strong></li>
</ol>

<h3>Quick Tips</h3>
<ul>
    <li>Use parent campaigns to organize related sub-campaigns</li>
    <li>Tasks inherit campaign labels and fields</li>
    <li>Set realistic due dates to keep work on track</li>
    <li>Add collaborators who need visibility into the campaign</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "gs-first-campaign",
                            Title = "Creating a Blog Campaign",
                            Description = "Example of creating a simple blog content campaign.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Campaign Setup:
  Name: ""Monthly Blog Content - January""
  Description: ""Blog posts for January covering product updates and industry trends""
  Start Date: January 1, 2025
  End Date: January 31, 2025
  Color: Blue
  Labels: Content Type: Blog, Department: Marketing

Tasks to Add:
  1. ""Product Update Roundup""
     - Workflow: Editorial Review
     - Owner: Content Writer
     - Due: January 10

  2. ""Industry Trends 2025""
     - Workflow: Editorial Review
     - Owner: Content Writer
     - Due: January 20

  3. ""Customer Success Story""
     - Workflow: Customer Approval
     - Owner: Content Writer
     - Due: January 28",
                            SampleResponse = @"Campaign created successfully!

Your campaign now appears in:
- The Campaigns list (filter by ""Content Type: Blog"")
- The Calendar view (blue blocks in January)
- Your Home page (if you're the owner)

Next steps:
1. Fill out the campaign brief with objectives and target audience
2. Assign task owners and add collaborators
3. Start working on tasks - they'll move through workflows automatically
4. Monitor progress in the Activities tab",
                            Hints = new List<string>
                            {
                                "You can duplicate campaigns to quickly set up recurring content",
                                "Use labels consistently to make filtering and reporting easier"
                            }
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 2: Campaigns

    private LearningModule BuildCampaignsModule()
    {
        return new LearningModule
        {
            Id = "campaigns",
            Title = "Campaign Management",
            Description = "Master campaign planning and organization in Optimizely CMP.",
            Icon = "flag",
            Order = 2,
            Difficulty = ModuleDifficulty.Beginner,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "camp-understanding",
                    ModuleId = "campaigns",
                    Title = "Understanding Campaigns",
                    Summary = "Learn how campaigns work and how to structure them effectively.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of campaigns",
                        "Learn about campaign hierarchy",
                        "Know when to use campaigns vs. tasks"
                    },
                    Content = @"
<h2>Campaigns in CMP</h2>
<p>Campaigns are integral to how you plan work in Optimizely CMP. They provide structure and context for all related marketing activities.</p>

<h3>What is a Campaign?</h3>
<p>A campaign is a container that:</p>
<ul>
    <li>Groups related marketing activities together</li>
    <li>Provides context and objectives through briefs</li>
    <li>Contains tasks, events, milestones, and sub-campaigns</li>
    <li>Tracks progress toward marketing goals</li>
</ul>

<h3>Campaign Hierarchy</h3>
<p>CMP supports nested campaign structures:</p>
<ul>
    <li><strong>Parent Campaign</strong> - Top-level initiative (e.g., ""Annual Brand Campaign"")</li>
    <li><strong>Sub-Campaign</strong> - Focused efforts within parent (e.g., ""Q1 Social Media"")</li>
    <li><strong>Activities</strong> - Tasks, events, and milestones within any campaign</li>
</ul>

<h3>When to Use Campaigns</h3>
<p>Create a campaign when you have:</p>
<ul>
    <li>Multiple related tasks that share a common goal</li>
    <li>Work that spans multiple team members or departments</li>
    <li>A marketing initiative with defined objectives</li>
    <li>Activities that need to be tracked together for reporting</li>
</ul>

<h3>Campaign vs. Standalone Tasks</h3>
<p>Use <strong>standalone tasks</strong> for:</p>
<ul>
    <li>Ad-hoc requests that don't fit a campaign</li>
    <li>One-off content pieces</li>
    <li>Quick updates or fixes</li>
</ul>
<p>Use <strong>campaigns</strong> for:</p>
<ul>
    <li>Coordinated marketing efforts</li>
    <li>Multi-channel initiatives</li>
    <li>Work requiring strategic planning</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "camp-creating",
                    ModuleId = "campaigns",
                    Title = "Creating and Managing Campaigns",
                    Summary = "Learn how to create, configure, and manage campaigns.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create campaigns with proper configuration",
                        "Set up parent-child campaign relationships",
                        "Manage campaign settings and metadata"
                    },
                    Content = @"
<h2>Creating Campaigns</h2>
<p>Creating well-structured campaigns sets your team up for success.</p>

<h3>Campaign Creation Steps</h3>
<ol>
    <li>Click <strong>+</strong> in the toolbar and select <strong>Campaign</strong></li>
    <li>Enter a clear, descriptive <strong>campaign name</strong></li>
    <li>Write a <strong>description</strong> summarizing the campaign's purpose</li>
    <li>Set <strong>start and end dates</strong> (leave open-ended if ongoing)</li>
    <li>Choose a <strong>color</strong> for calendar visibility</li>
    <li>Apply <strong>labels</strong> for categorization</li>
    <li>Optionally select a <strong>parent campaign</strong></li>
</ol>

<h3>Parent Campaigns</h3>
<p>Use parent campaigns to:</p>
<ul>
    <li>Organize similar campaigns across teams, markets, or business lines</li>
    <li>Create a hierarchy of related activities</li>
    <li>Roll up reporting from sub-campaigns</li>
    <li>Coordinate broader marketing initiatives</li>
</ul>

<h3>Campaign Settings</h3>
<p>After creation, configure these settings:</p>
<ul>
    <li><strong>Brief Template</strong> - Select a template to structure your campaign brief</li>
    <li><strong>Default Workflow</strong> - Set the workflow for new tasks</li>
    <li><strong>Collaborators</strong> - Add team members who need access</li>
    <li><strong>Custom Fields</strong> - Add metadata specific to this campaign</li>
</ul>

<h3>Campaign Brief</h3>
<p>The brief tab documents your campaign's:</p>
<ul>
    <li>Objectives and goals</li>
    <li>Target audience</li>
    <li>Key messages</li>
    <li>Success metrics</li>
    <li>Budget and resources</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "camp-hierarchy",
                            Title = "Campaign Hierarchy Example",
                            Description = "See how to structure a complex marketing initiative.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Parent Campaign: ""2025 Annual Marketing Plan""
├── Sub-Campaign: ""Q1 Brand Awareness""
│   ├── Sub-Campaign: ""Social Media Q1""
│   │   └── Tasks: Weekly posts, engagement campaigns
│   ├── Sub-Campaign: ""Content Marketing Q1""
│   │   └── Tasks: Blog posts, whitepapers
│   └── Event: ""Industry Conference"" (March 15)
├── Sub-Campaign: ""Q2 Product Launch""
│   ├── Sub-Campaign: ""Launch Communications""
│   ├── Sub-Campaign: ""Sales Enablement""
│   └── Milestone: ""Launch Day"" (May 1)
├── Sub-Campaign: ""Q3 Customer Engagement""
└── Sub-Campaign: ""Q4 Holiday Campaign""",
                            SampleResponse = @"This hierarchy enables:

- Top-level visibility into the annual plan
- Quarterly sub-campaigns for focused planning
- Channel-specific sub-campaigns within quarters
- Roll-up reporting at any level
- Clear ownership and accountability

Pro tip: Use consistent naming conventions across your hierarchy for easier filtering and reporting."
                        }
                    }
                },
                new Lesson
                {
                    Id = "camp-activities",
                    ModuleId = "campaigns",
                    Title = "Campaign Activities",
                    Summary = "Learn how to add and manage tasks, events, and milestones within campaigns.",
                    Order = 3,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Add tasks, events, and milestones to campaigns",
                        "Understand the difference between activity types",
                        "Manage activities effectively"
                    },
                    Content = @"
<h2>Campaign Activities</h2>
<p>Activities are the building blocks of your campaigns. CMP supports several activity types.</p>

<h3>Activity Types</h3>
<table>
    <tr>
        <td><strong>Tasks</strong></td>
        <td>Work items that move through workflows. Used for content creation, approvals, and deliverables.</td>
    </tr>
    <tr>
        <td><strong>Events</strong></td>
        <td>Scheduled occurrences that don't require workflow steps. Used for launches, webinars, meetings.</td>
    </tr>
    <tr>
        <td><strong>Milestones</strong></td>
        <td>Significant dates or achievements. Used to mark key deliverables or deadlines.</td>
    </tr>
    <tr>
        <td><strong>Sub-Campaigns</strong></td>
        <td>Nested campaigns for organizing complex initiatives into manageable parts.</td>
    </tr>
</table>

<h3>Adding Activities</h3>
<ol>
    <li>Open your campaign</li>
    <li>Go to the <strong>Activities</strong> tab</li>
    <li>Click <strong>Add</strong> and select the activity type</li>
    <li>Fill in the required details</li>
    <li>Click <strong>Create</strong></li>
</ol>

<h3>Managing Activities</h3>
<p>The Activities tab shows all campaign work in customizable views:</p>
<ul>
    <li><strong>List View</strong> - Detailed table with sorting and filtering</li>
    <li><strong>Board View</strong> - Kanban-style organization</li>
    <li><strong>Timeline View</strong> - Gantt chart showing dates</li>
    <li><strong>Calendar View</strong> - Monthly/weekly schedule</li>
</ul>

<h3>Best Practices</h3>
<ul>
    <li>Use consistent naming conventions for activities</li>
    <li>Set realistic due dates based on workflow complexity</li>
    <li>Add dependencies between related tasks</li>
    <li>Use milestones to track key deliverables</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "camp-views",
                    ModuleId = "campaigns",
                    Title = "Campaign Views and Filters",
                    Summary = "Learn how to view and filter campaigns to find what you need.",
                    Order = 4,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Use different views to see campaign data",
                        "Apply filters to find specific campaigns",
                        "Save custom views for quick access"
                    },
                    Content = @"
<h2>Campaign Views</h2>
<p>CMP provides multiple ways to view and organize your campaigns.</p>

<h3>Available Views</h3>
<ul>
    <li><strong>List View</strong> - Traditional table format with sortable columns</li>
    <li><strong>Calendar View</strong> - Visual calendar showing campaign dates</li>
    <li><strong>Timeline View</strong> - Gantt chart for project planning</li>
    <li><strong>Board View</strong> - Agile-inspired kanban boards</li>
</ul>

<h3>Filtering Campaigns</h3>
<p>Use filters to narrow down your view:</p>
<ul>
    <li><strong>Date Range</strong> - Filter by start/end dates</li>
    <li><strong>Status</strong> - Active, completed, on hold</li>
    <li><strong>Owner</strong> - Filter by campaign owner</li>
    <li><strong>Labels</strong> - Filter by any applied labels</li>
    <li><strong>Custom Fields</strong> - Filter by field values</li>
</ul>

<h3>Saving Custom Views</h3>
<p>Create saved views for common filter combinations:</p>
<ol>
    <li>Apply your desired filters</li>
    <li>Click <strong>Save View</strong></li>
    <li>Name your view</li>
    <li>Access it from the Views dropdown</li>
</ol>

<h3>Board View Grouping</h3>
<p>In Board view, you can group campaigns by:</p>
<ul>
    <li>Status</li>
    <li>Owner</li>
    <li>Due Date</li>
    <li>Custom Labels</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "camp-filter-example",
                            Title = "Creating a Saved View",
                            Description = "Example of creating a useful saved view.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Saved View: ""My Active Campaigns Q1""

Filters Applied:
  - Status: Active
  - Owner: [Current User]
  - Date Range: Jan 1 - Mar 31, 2025
  - Label: ""Department: Marketing""

View Settings:
  - View Type: List
  - Sort: Due Date (Ascending)
  - Columns: Name, Status, Owner, Due Date, Progress",
                            SampleResponse = @"This saved view is now available in your Views dropdown.

Benefits of saved views:
- Quick access to frequently needed data
- Consistent filtering across sessions
- Shareable with team members
- Helps focus on relevant work

Tip: Create views for different contexts like ""My Campaigns"", ""Team Campaigns"", ""At Risk"", etc."
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 3: Tasks & Workflows

    private LearningModule BuildTasksWorkflowsModule()
    {
        return new LearningModule
        {
            Id = "tasks-workflows",
            Title = "Tasks & Workflows",
            Description = "Learn how to manage tasks and configure workflows in CMP.",
            Icon = "clipboard-document-list",
            Order = 3,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "campaigns" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "task-basics",
                    ModuleId = "tasks-workflows",
                    Title = "Task Fundamentals",
                    Summary = "Learn how tasks work and how to create them effectively.",
                    Order = 1,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Understand how tasks work in CMP",
                        "Create tasks with proper configuration",
                        "Know the different ways to create tasks"
                    },
                    Content = @"
<h2>Tasks in CMP</h2>
<p>You accomplish work through tasks in CMP. Each task moves through a workflow from creation to completion.</p>

<h3>What is a Task?</h3>
<p>A task represents a piece of work that needs to be done. Tasks can:</p>
<ul>
    <li>Contain content created in the omnichannel editor</li>
    <li>Move through approval workflows</li>
    <li>Be assigned to team members</li>
    <li>Track progress and history</li>
</ul>

<h3>Creating Tasks</h3>
<p>You can create tasks from multiple places:</p>
<ul>
    <li><strong>Campaign Activities</strong> - Create tasks within a campaign context</li>
    <li><strong>Quick Create (+)</strong> - Create standalone tasks from the toolbar</li>
    <li><strong>List/Calendar Views</strong> - Create tasks directly in views</li>
    <li><strong>Marketplace</strong> - Create tasks from content ideas</li>
    <li><strong>Work Requests</strong> - Convert requests into tasks</li>
</ul>

<h3>Task Components</h3>
<p>Each task includes:</p>
<ul>
    <li><strong>Title</strong> - Clear description of the work</li>
    <li><strong>Brief</strong> - Detailed requirements and context</li>
    <li><strong>Fields</strong> - Metadata like labels, dates, assignees</li>
    <li><strong>Content</strong> - The actual deliverable</li>
    <li><strong>Workflow</strong> - Steps the task moves through</li>
    <li><strong>History</strong> - Record of all changes</li>
</ul>

<h3>Task from Campaign</h3>
<p>When you create a task within a campaign:</p>
<ul>
    <li>The task inherits campaign labels and fields</li>
    <li>It appears in the campaign's Activities tab</li>
    <li>Campaign context is automatically linked</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "task-workflows",
                    ModuleId = "tasks-workflows",
                    Title = "Understanding Workflows",
                    Summary = "Learn how workflows guide content from creation to publication.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Understand how workflows work",
                        "Learn about workflow steps and assignments",
                        "Know when to use different workflow types"
                    },
                    Content = @"
<h2>Workflows in CMP</h2>
<p>Workflows define the process your content follows from creation to completion. They ensure consistent quality and proper approvals.</p>

<h3>What is a Workflow?</h3>
<p>A workflow is a series of sequential steps that content must pass through. Each step can include:</p>
<ul>
    <li>Specific tasks to complete</li>
    <li>Required approvals</li>
    <li>Assigned reviewers</li>
    <li>Quality checkpoints</li>
</ul>

<h3>Workflow Structure</h3>
<p>A typical workflow includes:</p>
<ol>
    <li><strong>Draft</strong> - Initial content creation</li>
    <li><strong>Review</strong> - Editorial or peer review</li>
    <li><strong>Approval</strong> - Manager or stakeholder sign-off</li>
    <li><strong>Publish</strong> - Content goes live</li>
</ol>

<h3>Moving Through Steps</h3>
<p>To advance a task through a workflow:</p>
<ol>
    <li>Complete the work for the current step</li>
    <li>Click the step completion button</li>
    <li>Add any required notes or approvals</li>
    <li>The task moves to the next step</li>
</ol>

<h3>Workflow Notifications</h3>
<p>CMP notifies team members when:</p>
<ul>
    <li>A task is assigned to their step</li>
    <li>A task is waiting for their approval</li>
    <li>A task is sent back for revisions</li>
    <li>A task is completed</li>
</ul>

<h3>Custom Workflows</h3>
<p>Custom workflows are for teams with complex requirements such as:</p>
<ul>
    <li>Multiple approval layers</li>
    <li>Agency or external review</li>
    <li>Legal or compliance checks</li>
    <li>Multi-region localization</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "workflow-example",
                            Title = "Example Workflow Structure",
                            Description = "See how a content approval workflow is structured.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Workflow: ""Editorial Content Approval""

Steps:
1. Brief
   - Assignee: Content Strategist
   - Actions: Fill out content brief, define requirements

2. Draft
   - Assignee: Content Writer
   - Actions: Create content based on brief

3. Editorial Review
   - Assignee: Editor
   - Actions: Review for quality, grammar, brand voice

4. Stakeholder Approval
   - Assignee: Marketing Manager
   - Actions: Approve or request changes

5. Final Review
   - Assignee: Editor
   - Actions: Final check before publishing

6. Publish
   - Assignee: Content Manager
   - Actions: Schedule and publish content",
                            SampleResponse = @"This workflow ensures:

- Clear handoffs between team members
- Quality checks at multiple stages
- Stakeholder visibility and approval
- Consistent process for all content

Workflows can be customized per content type. For example, social media content might use a shorter workflow, while legal documents need additional review steps."
                        }
                    }
                },
                new Lesson
                {
                    Id = "workflow-admin",
                    ModuleId = "tasks-workflows",
                    Title = "Creating and Managing Workflows",
                    Summary = "Learn how to create and configure workflows (admin).",
                    Order = 3,
                    EstimatedMinutes = 11,
                    LearningObjectives = new List<string>
                    {
                        "Create new workflows",
                        "Configure workflow steps and assignments",
                        "Manage workflow settings"
                    },
                    Content = @"
<h2>Workflow Administration</h2>
<p>Workflows are configured by administrators. This lesson covers how to create and manage them.</p>

<h3>Accessing Workflows</h3>
<ol>
    <li>Click your <strong>avatar</strong> in the top right</li>
    <li>Select <strong>Workflows</strong></li>
    <li>View existing workflows or click <strong>Create Workflow</strong></li>
</ol>

<h3>Creating a Workflow</h3>
<ol>
    <li>Click <strong>Create Workflow</strong></li>
    <li>Enter a <strong>workflow name</strong></li>
    <li>Add a <strong>description</strong></li>
    <li>Click <strong>Add Step</strong> to build your workflow</li>
</ol>

<h3>Configuring Steps</h3>
<p>For each step, configure:</p>
<ul>
    <li><strong>Title</strong> - Name of the step (e.g., ""Review"")</li>
    <li><strong>Assignee</strong> - User or team responsible</li>
    <li><strong>Required Actions</strong> - What must be done to complete</li>
    <li><strong>Permissions</strong> - Who can move to next step</li>
</ul>

<h3>Step Assignment Options</h3>
<ul>
    <li><strong>Specific User</strong> - Always assigned to same person</li>
    <li><strong>Team</strong> - Any team member can complete</li>
    <li><strong>Unassigned</strong> - Task owner assigns each time</li>
    <li><strong>Previous Step Owner</strong> - Same as previous step</li>
</ul>

<h3>Advanced Options</h3>
<ul>
    <li><strong>Parallel Steps</strong> - Multiple steps that can run simultaneously</li>
    <li><strong>Conditional Steps</strong> - Steps that only appear under certain conditions</li>
    <li><strong>Mandatory Fields</strong> - Required fields before completing a step</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "task-content",
                    ModuleId = "tasks-workflows",
                    Title = "Content in Tasks",
                    Summary = "Learn how to create and manage content within tasks.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Add content to tasks",
                        "Use the content editor",
                        "Manage content versions"
                    },
                    Content = @"
<h2>Content Within Tasks</h2>
<p>Tasks can contain the actual content you're creating, whether it's a blog post, social media content, or marketing copy.</p>

<h3>The Content Tab</h3>
<p>Each task has a Content tab where you can:</p>
<ul>
    <li>Create new content using the omnichannel editor</li>
    <li>Attach files and assets from the Library</li>
    <li>View content history and versions</li>
    <li>Preview content for different channels</li>
</ul>

<h3>Content Types</h3>
<p>CMP supports various content types:</p>
<ul>
    <li><strong>Rich Text</strong> - Blog posts, articles, landing pages</li>
    <li><strong>Social Posts</strong> - Content for social platforms</li>
    <li><strong>Email</strong> - Email marketing content</li>
    <li><strong>Documents</strong> - PDFs, presentations</li>
</ul>

<h3>Version History</h3>
<p>CMP tracks content versions automatically:</p>
<ul>
    <li>See who made changes and when</li>
    <li>Compare different versions</li>
    <li>Restore previous versions if needed</li>
</ul>

<h3>Comments and Feedback</h3>
<p>Team members can leave comments on content:</p>
<ul>
    <li>Inline comments on specific text</li>
    <li>General task comments</li>
    <li>Threaded discussions</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 4: Library & DAM

    private LearningModule BuildLibraryDAMModule()
    {
        return new LearningModule
        {
            Id = "library-dam",
            Title = "Library & Digital Asset Management",
            Description = "Master digital asset management in Optimizely CMP.",
            Icon = "photo",
            Order = 4,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "dam-overview",
                    ModuleId = "library-dam",
                    Title = "Introduction to the Library",
                    Summary = "Learn how the CMP Library serves as your digital asset hub.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand the purpose of the Library",
                        "Learn about asset types and organization",
                        "Know the key Library features"
                    },
                    Content = @"
<h2>The CMP Library</h2>
<p>The Library is CMP's Digital Asset Management (DAM) system. It stores, organizes, manages, and retrieves all your digital assets.</p>

<h3>What is the Library?</h3>
<p>The Library provides:</p>
<ul>
    <li>Centralized storage for all marketing assets</li>
    <li>Organization through folders and tags</li>
    <li>Search and filtering capabilities</li>
    <li>Version control for assets</li>
    <li>Asset usage tracking</li>
</ul>

<h3>Supported Asset Types</h3>
<ul>
    <li><strong>Images</strong> - PNG, JPEG, GIF, SVG</li>
    <li><strong>Videos</strong> - MP4, MOV, and more</li>
    <li><strong>Documents</strong> - PDF, Word, PowerPoint</li>
    <li><strong>Audio</strong> - MP3, WAV</li>
    <li><strong>Design Files</strong> - PSD, AI, InDesign</li>
</ul>

<h3>Key Features</h3>
<ul>
    <li><strong>Folder Organization</strong> - Create hierarchical folder structures</li>
    <li><strong>Tagging</strong> - Apply tags for flexible categorization</li>
    <li><strong>Metadata</strong> - Add title, description, alt text, attribution</li>
    <li><strong>Renditions</strong> - Auto-generate optimized versions</li>
    <li><strong>Usage Tracking</strong> - See where assets are used</li>
</ul>

<h3>Accessing the Library</h3>
<p>Access the Library from:</p>
<ul>
    <li>Main navigation sidebar</li>
    <li>Asset picker in content editors</li>
    <li>Task attachments</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "dam-upload",
                    ModuleId = "library-dam",
                    Title = "Uploading and Organizing Assets",
                    Summary = "Learn how to upload assets and organize them effectively.",
                    Order = 2,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Upload assets to the Library",
                        "Create and manage folders",
                        "Apply tags and metadata"
                    },
                    Content = @"
<h2>Uploading Assets</h2>
<p>Getting assets into the Library is straightforward.</p>

<h3>Upload Methods</h3>
<ul>
    <li><strong>Drag and Drop</strong> - Drag files directly into the Library</li>
    <li><strong>Upload Button</strong> - Click to browse and select files</li>
    <li><strong>Bulk Upload</strong> - Upload multiple files at once</li>
</ul>

<h3>Duplicate Detection</h3>
<p>CMP can identify duplicate assets upon upload:</p>
<ul>
    <li>Prevents accidental duplicates</li>
    <li>Links to existing assets if found</li>
    <li>Keeps your Library clean</li>
</ul>

<h3>Folder Organization</h3>
<p>Create folders to organize assets:</p>
<ol>
    <li>Navigate to the Library</li>
    <li>Click <strong>New Folder</strong></li>
    <li>Name your folder</li>
    <li>Drag assets into folders or upload directly</li>
</ol>

<h3>Folder Best Practices</h3>
<ul>
    <li>Use consistent naming conventions</li>
    <li>Create logical hierarchies (by campaign, brand, type)</li>
    <li>Don't nest too deeply (3-4 levels max)</li>
    <li>Use tags for cross-cutting categories</li>
</ul>

<h3>Tagging Assets</h3>
<p>Tags provide flexible categorization:</p>
<ul>
    <li>Assets can have multiple tags</li>
    <li>CMP suggests tags automatically</li>
    <li>Filter and search by tags</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "dam-structure",
                            Title = "Library Folder Structure",
                            Description = "Example of a well-organized Library structure.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Library Folder Structure:

/Brand Assets
  /Logos
    /Primary
    /Secondary
    /Partner Logos
  /Brand Guidelines
  /Fonts

/Campaigns
  /2025
    /Q1 Spring Campaign
    /Q2 Product Launch
  /Evergreen

/Product Images
  /Product Line A
  /Product Line B

/Stock Photography
  /People
  /Office
  /Nature

/Templates
  /Social Media
  /Email
  /Presentations",
                            SampleResponse = @"This structure provides:

- Clear separation between asset types
- Campaign-specific folders for project assets
- Reusable brand assets in one location
- Easy navigation for team members

Combine folders with tags for maximum flexibility:
- Tag ""hero-image"" across multiple folders
- Tag ""approved"" for ready-to-use assets
- Tag by product, audience, or channel"
                        }
                    }
                },
                new Lesson
                {
                    Id = "dam-metadata",
                    ModuleId = "library-dam",
                    Title = "Asset Metadata and Details",
                    Summary = "Learn how to add and manage asset metadata.",
                    Order = 3,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Add metadata to assets",
                        "Understand metadata fields",
                        "Manage asset expiry dates"
                    },
                    Content = @"
<h2>Asset Metadata</h2>
<p>Metadata helps you describe, organize, and manage your assets effectively.</p>

<h3>Standard Metadata Fields</h3>
<table>
    <tr>
        <td><strong>Title</strong></td>
        <td>The name used to identify the asset</td>
    </tr>
    <tr>
        <td><strong>Description</strong></td>
        <td>Detailed explanation of the asset's content and purpose</td>
    </tr>
    <tr>
        <td><strong>Alt Text</strong></td>
        <td>Alternative text for accessibility</td>
    </tr>
    <tr>
        <td><strong>Attribution</strong></td>
        <td>Credit information for the creator or source</td>
    </tr>
    <tr>
        <td><strong>Expiry Date</strong></td>
        <td>Date after which the asset shouldn't be used</td>
    </tr>
</table>

<h3>Editing Metadata</h3>
<ol>
    <li>Click on an asset to open the details panel</li>
    <li>Edit fields in the <strong>Details</strong> tab</li>
    <li>Changes save automatically</li>
</ol>

<h3>Asset Expiry</h3>
<p>Set expiry dates for assets with limited usage rights:</p>
<ul>
    <li>Stock photos with licensing periods</li>
    <li>Time-sensitive campaign materials</li>
    <li>Seasonal content</li>
</ul>

<h3>Custom Metadata</h3>
<p>Administrators can add custom metadata fields:</p>
<ul>
    <li>Product categories</li>
    <li>Region or market</li>
    <li>Usage rights level</li>
    <li>Campaign association</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "dam-search",
                    ModuleId = "library-dam",
                    Title = "Finding and Using Assets",
                    Summary = "Learn how to search, filter, and use assets from the Library.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Search for assets effectively",
                        "Use filters to narrow results",
                        "Add assets to content and tasks"
                    },
                    Content = @"
<h2>Finding Assets</h2>
<p>CMP provides powerful search and filtering to help you find assets quickly.</p>

<h3>Search</h3>
<p>Search by:</p>
<ul>
    <li>Asset title and description</li>
    <li>Tags and metadata</li>
    <li>File name</li>
    <li>Content within documents</li>
</ul>

<h3>Filtering</h3>
<p>Narrow results by:</p>
<ul>
    <li><strong>Type</strong> - Images, videos, documents</li>
    <li><strong>Owner</strong> - Who uploaded the asset</li>
    <li><strong>Date</strong> - Created or modified date</li>
    <li><strong>Tags</strong> - Applied tags</li>
    <li><strong>Folder</strong> - Location in Library</li>
</ul>

<h3>View Options</h3>
<ul>
    <li><strong>Grid View</strong> - Thumbnail preview</li>
    <li><strong>List View</strong> - Detailed table</li>
    <li><strong>Category View</strong> - Organized by type</li>
</ul>

<h3>Sorting</h3>
<p>Sort by:</p>
<ul>
    <li>Title (A-Z or Z-A)</li>
    <li>Type</li>
    <li>Owner</li>
    <li>Last Modified</li>
    <li>Date Created</li>
</ul>

<h3>Using Assets</h3>
<p>Once you find an asset, you can:</p>
<ul>
    <li>Add it to task content</li>
    <li>Attach it to a task or campaign</li>
    <li>Download it for offline use</li>
    <li>Share a link with others</li>
    <li>View where it's currently used</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "dam-renditions",
                    ModuleId = "library-dam",
                    Title = "Asset Renditions and Versions",
                    Summary = "Learn about renditions and version control for assets.",
                    Order = 5,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Understand asset renditions",
                        "Use version control features",
                        "Track asset usage"
                    },
                    Content = @"
<h2>Renditions</h2>
<p>Renditions are different versions of an asset optimized for specific uses.</p>

<h3>What are Renditions?</h3>
<p>CMP automatically creates renditions for different platforms:</p>
<ul>
    <li><strong>Website</strong> - Optimized for web performance</li>
    <li><strong>Social Media</strong> - Sized for each platform</li>
    <li><strong>Email</strong> - Compressed for email clients</li>
    <li><strong>Thumbnail</strong> - Small preview images</li>
</ul>

<h3>Rendition Benefits</h3>
<ul>
    <li>Improved website performance</li>
    <li>Reduced bandwidth usage</li>
    <li>Consistent quality across channels</li>
    <li>No manual resizing needed</li>
</ul>

<h2>Version Control</h2>
<p>The Library tracks all versions of your assets.</p>

<h3>Version History</h3>
<p>For each asset, you can:</p>
<ul>
    <li>See all previous versions</li>
    <li>Compare versions side-by-side</li>
    <li>Revert to a previous version</li>
    <li>See who made changes</li>
</ul>

<h3>Uploading New Versions</h3>
<ol>
    <li>Open the asset details</li>
    <li>Click <strong>Upload New Version</strong></li>
    <li>Select the updated file</li>
    <li>The new version becomes current</li>
</ol>

<h2>Asset Usage Tracking</h2>
<p>See where assets are used across CMP:</p>
<ul>
    <li>Which tasks use the asset</li>
    <li>Published content locations</li>
    <li>CMS pages (with integration)</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 5: Content Creation

    private LearningModule BuildContentCreationModule()
    {
        return new LearningModule
        {
            Id = "content-creation",
            Title = "Content Creation",
            Description = "Learn to create content using CMP's omnichannel editor.",
            Icon = "pencil-square",
            Order = 5,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "tasks-workflows" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "oc-intro",
                    ModuleId = "content-creation",
                    Title = "Introduction to the Omnichannel Editor",
                    Summary = "Learn how the omnichannel editor enables multi-channel content creation.",
                    Order = 1,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Understand the omnichannel editor concept",
                        "Know what content types are supported",
                        "Learn the benefits of omnichannel authoring"
                    },
                    Content = @"
<h2>The Omnichannel Editor</h2>
<p>The omnichannel editor lets you create content for multiple destinations from a single interface.</p>

<h3>What is Omnichannel Authoring?</h3>
<p>Omnichannel authoring means:</p>
<ul>
    <li>Create content once for multiple channels</li>
    <li>Preview how content looks on each platform</li>
    <li>Publish to web, social, email, and more</li>
    <li>Maintain consistency across channels</li>
</ul>

<h3>Key Features</h3>
<ul>
    <li><strong>Familiar Editing</strong> - Similar to Google Docs or Microsoft Word</li>
    <li><strong>Inline Comments</strong> - Collaborate directly in the content</li>
    <li><strong>AI Assistance</strong> - Get help from Opal while writing</li>
    <li><strong>Channel Previews</strong> - See how content renders on each platform</li>
    <li><strong>Multi-Locale Support</strong> - Work in multiple languages</li>
</ul>

<h3>Supported Destinations</h3>
<ul>
    <li>Web pages and landing pages</li>
    <li>Blog posts and articles</li>
    <li>Social media posts (LinkedIn, Facebook, Instagram, Twitter)</li>
    <li>Email newsletters</li>
    <li>Sales enablement materials</li>
    <li>PDFs and documents</li>
</ul>

<h3>Why Use Omnichannel?</h3>
<ul>
    <li>Write once, publish everywhere</li>
    <li>Consistent messaging across channels</li>
    <li>Faster content production</li>
    <li>Built-in approval workflows</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "oc-editing",
                    ModuleId = "content-creation",
                    Title = "Editing Content",
                    Summary = "Learn how to create and edit content in the omnichannel editor.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Use the content editor effectively",
                        "Format text and add media",
                        "Use components and templates"
                    },
                    Content = @"
<h2>Working in the Editor</h2>
<p>The omnichannel editor provides a rich editing experience for all content types.</p>

<h3>Basic Editing</h3>
<ul>
    <li><strong>Text Formatting</strong> - Bold, italic, headings, lists</li>
    <li><strong>Links</strong> - Add hyperlinks to text</li>
    <li><strong>Media</strong> - Insert images, videos from Library</li>
    <li><strong>Tables</strong> - Create structured data</li>
</ul>

<h3>Using Components</h3>
<p>Components are reusable content blocks:</p>
<ul>
    <li>Import components from the Library</li>
    <li>Customize messaging for different audiences</li>
    <li>Save new components for future use</li>
    <li>Maintain consistency across content</li>
</ul>

<h3>Templates</h3>
<p>Use templates for consistent content structure:</p>
<ul>
    <li>Pre-designed layouts for content types</li>
    <li>Brand-approved formatting</li>
    <li>Locked elements ensure compliance</li>
    <li>Editable sections for customization</li>
</ul>

<h3>Collaboration Features</h3>
<ul>
    <li><strong>Real-Time Editing</strong> - Work with teammates simultaneously</li>
    <li><strong>Inline Comments</strong> - Leave feedback on specific text</li>
    <li><strong>Threaded Discussions</strong> - Have conversations about changes</li>
    <li><strong>Version History</strong> - See all changes and who made them</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "oc-social",
                    ModuleId = "content-creation",
                    Title = "Creating Social Media Content",
                    Summary = "Learn to create and preview content for social platforms.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Create content for social platforms",
                        "Preview posts for each channel",
                        "Understand platform-specific requirements"
                    },
                    Content = @"
<h2>Social Media Content</h2>
<p>CMP streamlines social media content creation with platform-specific tools.</p>

<h3>Supported Platforms</h3>
<ul>
    <li><strong>LinkedIn</strong> - Professional posts and articles</li>
    <li><strong>Facebook</strong> - Page posts and stories</li>
    <li><strong>Instagram</strong> - Posts, stories, reels</li>
    <li><strong>Twitter/X</strong> - Tweets and threads</li>
    <li><strong>YouTube</strong> - Video content</li>
</ul>

<h3>Creating Social Posts</h3>
<ol>
    <li>Create a task with social media workflow</li>
    <li>Open the content editor</li>
    <li>Select the target platform(s)</li>
    <li>Write your content</li>
    <li>Add images or videos</li>
    <li>Preview for each platform</li>
</ol>

<h3>Platform Previews</h3>
<p>See exactly how your content will appear:</p>
<ul>
    <li>Character counts and limits</li>
    <li>Image cropping and sizing</li>
    <li>Hashtag formatting</li>
    <li>Link previews</li>
</ul>

<h3>Best Practices</h3>
<ul>
    <li>Tailor content for each platform's audience</li>
    <li>Use platform-appropriate image sizes</li>
    <li>Consider character limits when writing</li>
    <li>Include relevant hashtags</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "oc-social-example",
                            Title = "Multi-Platform Post",
                            Description = "Example of adapting content for different platforms.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Content Adaptation Example:

LinkedIn (Professional):
""We're excited to announce our new AI-powered
analytics dashboard! 📊

Designed for enterprise marketing teams, it
provides real-time insights into campaign
performance across all channels.

Read the full announcement: [link]

#MarketingAnalytics #AIMarketing #Optimizely""

Twitter/X (Concise):
""📊 Just launched: AI-powered analytics for
marketers! Real-time insights across all your
channels. Check it out → [link]

#MarketingTech #AI""

Instagram (Visual-first):
[Eye-catching graphic of dashboard]
""Marketing analytics just got smarter. 🚀

Our new AI dashboard gives you the insights
you need, when you need them.

Link in bio! 📲""",
                            SampleResponse = @"Each version is optimized for its platform:

- LinkedIn: Professional tone, detailed information, industry hashtags
- Twitter: Concise, emoji usage, direct call-to-action
- Instagram: Visual focus, conversational tone, bio link reference

CMP lets you manage all versions in one task while maintaining platform-specific optimization."
                        }
                    }
                },
                new Lesson
                {
                    Id = "oc-templates",
                    ModuleId = "content-creation",
                    Title = "Brand Templates",
                    Summary = "Learn to use and create brand templates for consistent content.",
                    Order = 4,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Use brand templates for content creation",
                        "Understand locked vs. editable elements",
                        "Create new templates (admin)"
                    },
                    Content = @"
<h2>Brand Templates</h2>
<p>Brand templates ensure consistent, on-brand content across your organization.</p>

<h3>What are Brand Templates?</h3>
<p>Templates provide:</p>
<ul>
    <li>Pre-designed layouts</li>
    <li>Approved brand elements</li>
    <li>Consistent formatting</li>
    <li>Guardrails for compliance</li>
</ul>

<h3>Using Templates</h3>
<ol>
    <li>Start a new piece of content</li>
    <li>Select a template from the library</li>
    <li>Edit the editable sections</li>
    <li>Locked elements stay as designed</li>
</ol>

<h3>Locked vs. Editable</h3>
<p>Designers can lock specific elements:</p>
<ul>
    <li><strong>Locked</strong> - Cannot be changed (logos, brand colors, required text)</li>
    <li><strong>Editable</strong> - Can be customized (copy, images, CTAs)</li>
</ul>

<h3>Template Types</h3>
<ul>
    <li><strong>Social Media</strong> - Platform-specific post templates</li>
    <li><strong>Email</strong> - Newsletter and campaign templates</li>
    <li><strong>Presentation</strong> - Slide deck templates</li>
    <li><strong>Document</strong> - Report and brief templates</li>
</ul>

<h3>Benefits</h3>
<ul>
    <li>Even non-designers can create on-brand content</li>
    <li>Faster content production</li>
    <li>Consistent brand experience</li>
    <li>Reduced review cycles</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 6: Publishing

    private LearningModule BuildPublishingModule()
    {
        return new LearningModule
        {
            Id = "publishing",
            Title = "Publishing & Channels",
            Description = "Learn to publish content across multiple channels from CMP.",
            Icon = "paper-airplane",
            Order = 6,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "content-creation" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "pub-overview",
                    ModuleId = "publishing",
                    Title = "Publishing Overview",
                    Summary = "Understand how CMP publishes content to various channels.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand CMP's publishing capabilities",
                        "Learn about available channels",
                        "Know the publishing workflow"
                    },
                    Content = @"
<h2>Publishing from CMP</h2>
<p>CMP is designed to publish content to virtually any channel from a single platform.</p>

<h3>Publishing Capabilities</h3>
<ul>
    <li>Direct publishing to connected platforms</li>
    <li>Scheduled publishing</li>
    <li>Multi-channel publishing from single content</li>
    <li>Preview before publishing</li>
</ul>

<h3>Supported Channels</h3>
<ul>
    <li><strong>Social Media</strong> - LinkedIn, Facebook, Instagram, Twitter/X</li>
    <li><strong>Content Management</strong> - Optimizely CMS, WordPress, Drupal</li>
    <li><strong>Email</strong> - Marketing automation platforms</li>
    <li><strong>Sales Enablement</strong> - Seismic, Highspot</li>
    <li><strong>Custom Channels</strong> - Via JSON feeds</li>
</ul>

<h3>Publishing Workflow</h3>
<ol>
    <li>Create content in the omnichannel editor</li>
    <li>Move through approval workflow</li>
    <li>Preview for target channels</li>
    <li>Schedule or publish immediately</li>
    <li>Track performance in Analytics</li>
</ol>

<h3>Channel Configuration</h3>
<p>Administrators connect channels in settings:</p>
<ul>
    <li>Authenticate with each platform</li>
    <li>Configure publishing options</li>
    <li>Set up default settings</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "pub-social",
                    ModuleId = "publishing",
                    Title = "Social Media Publishing",
                    Summary = "Learn to publish content to social media platforms.",
                    Order = 2,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Connect social media accounts",
                        "Publish and schedule social posts",
                        "Manage multiple social accounts"
                    },
                    Content = @"
<h2>Social Media Publishing</h2>
<p>Publish social content directly from CMP to your connected accounts.</p>

<h3>Connecting Accounts</h3>
<p>Administrators connect social accounts:</p>
<ol>
    <li>Go to <strong>Settings > Social Accounts</strong></li>
    <li>Select the platform to connect</li>
    <li>Authenticate with your credentials</li>
    <li>Grant CMP publishing permissions</li>
</ol>

<h3>Supported Platforms</h3>
<table>
    <tr>
        <td><strong>LinkedIn</strong></td>
        <td>Company pages and personal profiles</td>
    </tr>
    <tr>
        <td><strong>Facebook</strong></td>
        <td>Business pages</td>
    </tr>
    <tr>
        <td><strong>Instagram</strong></td>
        <td>Direct publishing (V2 integration)</td>
    </tr>
    <tr>
        <td><strong>Twitter/X</strong></td>
        <td>Business and personal accounts</td>
    </tr>
    <tr>
        <td><strong>Hootsuite</strong></td>
        <td>Send content for publishing</td>
    </tr>
</table>

<h3>Publishing Options</h3>
<ul>
    <li><strong>Publish Now</strong> - Post immediately</li>
    <li><strong>Schedule</strong> - Set a specific date and time</li>
    <li><strong>Draft</strong> - Save without publishing</li>
</ul>

<h3>Scheduling Best Practices</h3>
<ul>
    <li>Consider your audience's time zones</li>
    <li>Use analytics to find optimal posting times</li>
    <li>Maintain consistent posting schedules</li>
    <li>Leave buffer time for last-minute changes</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "pub-cms",
                    ModuleId = "publishing",
                    Title = "CMS Integration",
                    Summary = "Learn how CMP integrates with content management systems.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand CMS publishing from CMP",
                        "Configure CMS connections",
                        "Publish to Optimizely CMS and others"
                    },
                    Content = @"
<h2>CMS Publishing</h2>
<p>CMP integrates with content management systems for seamless web publishing.</p>

<h3>Optimizely CMS Integration</h3>
<p>Native integration with Optimizely CMS provides:</p>
<ul>
    <li>Direct publishing to CMS pages</li>
    <li>Asset synchronization via DAM picker</li>
    <li>Content preview in CMP</li>
    <li>Asset tracking across platforms</li>
</ul>

<h3>Other CMS Platforms</h3>
<p>CMP also integrates with:</p>
<ul>
    <li>WordPress</li>
    <li>Drupal</li>
    <li>Sitecore</li>
    <li>Custom CMS via feeds</li>
</ul>

<h3>Publishing Workflow</h3>
<ol>
    <li>Create content using predefined templates</li>
    <li>Preview how content will appear in CMS</li>
    <li>Complete approval workflow</li>
    <li>Publish directly to CMS</li>
</ol>

<h3>Asset Tracking</h3>
<p>When you use Library assets in CMS:</p>
<ul>
    <li>CMP tracks where assets are used</li>
    <li>View usage in asset details</li>
    <li>Know which version is live</li>
    <li>Update assets across all uses</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "pub-channels",
                    ModuleId = "publishing",
                    Title = "Custom Channels and Feeds",
                    Summary = "Learn to set up custom publishing channels.",
                    Order = 4,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Understand custom channel options",
                        "Set up JSON feeds",
                        "Integrate with other systems"
                    },
                    Content = @"
<h2>Custom Channels</h2>
<p>CMP can publish to virtually any system that accepts content.</p>

<h3>JSON Feeds</h3>
<p>CMP can generate structured JSON feeds for:</p>
<ul>
    <li>Custom applications</li>
    <li>Third-party platforms</li>
    <li>Mobile apps</li>
    <li>Digital signage</li>
</ul>

<h3>Setting Up a Feed</h3>
<ol>
    <li>Create a publishing destination channel</li>
    <li>Configure the feed structure</li>
    <li>Get the feed URL</li>
    <li>Configure your target system to consume the feed</li>
</ol>

<h3>Integration Options</h3>
<ul>
    <li><strong>Webhooks</strong> - Notify systems when content is published</li>
    <li><strong>API Access</strong> - Pull content programmatically</li>
    <li><strong>Scheduled Feeds</strong> - Regular content updates</li>
</ul>

<h3>Use Cases</h3>
<ul>
    <li>Internal communications platforms</li>
    <li>Partner portals</li>
    <li>Mobile applications</li>
    <li>Digital displays and kiosks</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 7: Work Requests

    private LearningModule BuildWorkRequestsModule()
    {
        return new LearningModule
        {
            Id = "work-requests",
            Title = "Work Requests",
            Description = "Learn to manage ad-hoc requests with CMP's work request system.",
            Icon = "inbox-arrow-down",
            Order = 7,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "tasks-workflows" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "wr-overview",
                    ModuleId = "work-requests",
                    Title = "Understanding Work Requests",
                    Summary = "Learn how work requests streamline ad-hoc marketing requests.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand what work requests are",
                        "Know when to use work requests",
                        "Learn the work request workflow"
                    },
                    Content = @"
<h2>Work Requests in CMP</h2>
<p>Work requests simplify handling ad-hoc marketing requests from across your organization.</p>

<h3>What are Work Requests?</h3>
<p>Work requests provide:</p>
<ul>
    <li>A formal intake process for marketing requests</li>
    <li>Customizable forms for different request types</li>
    <li>Routing to appropriate team members</li>
    <li>Tracking from request to completion</li>
</ul>

<h3>Common Use Cases</h3>
<ul>
    <li>Sales team needing marketing materials</li>
    <li>Product team requesting launch support</li>
    <li>HR requesting recruitment marketing</li>
    <li>Customer success needing case studies</li>
</ul>

<h3>Work Request Workflow</h3>
<ol>
    <li>Requester submits a work request form</li>
    <li>Request is routed to appropriate team/person</li>
    <li>Team reviews and triages the request</li>
    <li>Request is accepted, declined, or needs info</li>
    <li>If accepted, converted to task or campaign</li>
    <li>Work is completed through normal workflows</li>
</ol>

<h3>Benefits</h3>
<ul>
    <li>Eliminate scattered email requests</li>
    <li>Capture all required information upfront</li>
    <li>Track all requests in one place</li>
    <li>Measure request volume and turnaround</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "wr-forms",
                    ModuleId = "work-requests",
                    Title = "Request Forms and Templates",
                    Summary = "Learn to create and configure work request forms.",
                    Order = 2,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Create work request form templates",
                        "Configure form fields and logic",
                        "Set up routing rules"
                    },
                    Content = @"
<h2>Work Request Forms</h2>
<p>Administrators create form templates to capture the right information for each request type.</p>

<h3>Creating Form Templates</h3>
<ol>
    <li>Go to <strong>Settings > Form Templates</strong></li>
    <li>Click <strong>Create Template</strong></li>
    <li>Name your template (e.g., ""Content Request"")</li>
    <li>Add form fields</li>
    <li>Configure routing rules</li>
</ol>

<h3>Field Types</h3>
<ul>
    <li><strong>Text</strong> - Single line or rich text</li>
    <li><strong>Dropdown</strong> - Select from options</li>
    <li><strong>Checkbox</strong> - Multiple selections</li>
    <li><strong>Radio Buttons</strong> - Single selection</li>
    <li><strong>Date</strong> - Date picker</li>
    <li><strong>File Upload</strong> - Attach documents</li>
    <li><strong>Creative Asset</strong> - Asset-specific fields</li>
</ul>

<h3>Dynamic Form Logic</h3>
<p>Create forms that adapt based on answers:</p>
<ul>
    <li><strong>Conditional Fields</strong> - Show fields based on selections</li>
    <li><strong>Display Rules</strong> - Hide/show sections</li>
    <li><strong>Dependencies</strong> - Field relationships</li>
</ul>

<h3>Routing Configuration</h3>
<p>Set up intelligent routing:</p>
<ul>
    <li>Route by request type</li>
    <li>Route by department or team</li>
    <li>Route by priority level</li>
    <li>Multiple assignees for team triage</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "wr-form-example",
                            Title = "Content Request Form",
                            Description = "Example work request form structure.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Form Template: ""Content Request""

Fields:
1. Request Title (Text, Required)
2. Request Type (Dropdown, Required)
   - Blog Post
   - Social Media
   - Email Campaign
   - Sales Collateral
   - Other

3. Description (Rich Text, Required)
   ""Please describe what you need...""

4. Target Audience (Text)
5. Key Messages (Rich Text)
6. Deadline (Date, Required)
7. Priority (Radio, Required)
   - Low
   - Medium
   - High
   - Urgent

8. Supporting Materials (File Upload)

Routing Rules:
- If Type = ""Blog Post"" → Content Team
- If Type = ""Social Media"" → Social Team
- If Priority = ""Urgent"" → Team Lead",
                            SampleResponse = @"This form captures:

- Essential information for any content request
- Conditional routing based on request type
- Priority flagging for urgent requests
- Supporting materials for context

Pro tip: Include an ""Other"" option with a text field for requests that don't fit standard categories."
                        }
                    }
                },
                new Lesson
                {
                    Id = "wr-managing",
                    ModuleId = "work-requests",
                    Title = "Managing Work Requests",
                    Summary = "Learn to triage, accept, and process work requests.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Triage incoming requests",
                        "Accept, decline, or request more info",
                        "Convert requests to tasks or campaigns"
                    },
                    Content = @"
<h2>Processing Work Requests</h2>
<p>When requests come in, your team needs to triage and process them efficiently.</p>

<h3>Viewing Requests</h3>
<p>See incoming requests on:</p>
<ul>
    <li>CMP Home page (requests routed to you)</li>
    <li>Work Requests section</li>
    <li>Team inbox</li>
</ul>

<h3>Triage Actions</h3>
<table>
    <tr>
        <td><strong>Accept</strong></td>
        <td>Take on the request and begin work</td>
    </tr>
    <tr>
        <td><strong>Decline</strong></td>
        <td>Reject with explanation</td>
    </tr>
    <tr>
        <td><strong>Request Info</strong></td>
        <td>Ask requester for more details</td>
    </tr>
    <tr>
        <td><strong>Reassign</strong></td>
        <td>Route to another team/person</td>
    </tr>
</table>

<h3>Converting to Work</h3>
<p>Once accepted, create from the request:</p>
<ul>
    <li><strong>Task</strong> - For single deliverables</li>
    <li><strong>Campaign</strong> - For multi-part initiatives</li>
    <li><strong>Event</strong> - For scheduled occurrences</li>
</ul>

<h3>Communication</h3>
<p>Maintain communication with requesters:</p>
<ul>
    <li>Threaded comments on requests</li>
    <li>Status updates as work progresses</li>
    <li>Notifications when complete</li>
</ul>

<h3>Opal Integration</h3>
<p>With Opal, you can:</p>
<ul>
    <li>Read work request details automatically</li>
    <li>Auto-accept high-priority requests</li>
    <li>Create campaigns from request information</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 8: Analytics

    private LearningModule BuildAnalyticsModule()
    {
        return new LearningModule
        {
            Id = "analytics",
            Title = "Analytics & Reporting",
            Description = "Learn to measure content performance and generate reports in CMP.",
            Icon = "chart-bar",
            Order = 8,
            Difficulty = ModuleDifficulty.Intermediate,
            Prerequisites = new[] { "getting-started" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "an-overview",
                    ModuleId = "analytics",
                    Title = "Analytics Overview",
                    Summary = "Understand CMP's analytics capabilities and key metrics.",
                    Order = 1,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Understand CMP analytics capabilities",
                        "Learn key performance metrics",
                        "Know how data is collected"
                    },
                    Content = @"
<h2>CMP Analytics</h2>
<p>CMP Analytics shows how your content is performing across channels.</p>

<h3>What Analytics Captures</h3>
<ul>
    <li><strong>Article Views</strong> - Total content views</li>
    <li><strong>Page Views</strong> - Page-level traffic</li>
    <li><strong>Unique Visitors</strong> - Individual readers</li>
    <li><strong>Attention Time</strong> - How long people engage</li>
    <li><strong>Engagement Rate</strong> - Active engagement percentage</li>
</ul>

<h3>How It Works</h3>
<p>CMP uses a tracking pixel on published content to:</p>
<ul>
    <li>Track pageviews and visitors</li>
    <li>Measure scroll depth</li>
    <li>Calculate attention time</li>
    <li>Determine engagement rates</li>
</ul>

<h3>Engagement Measurement</h3>
<p>A visitor is considered ""engaged"" when:</p>
<ul>
    <li>The page is in focus</li>
    <li>They scroll or move the cursor</li>
    <li>Activity within the past 5 seconds</li>
    <li>Time on page exceeds 30 seconds</li>
</ul>

<h3>Key Metrics Explained</h3>
<table>
    <tr>
        <td><strong>Attention Time</strong></td>
        <td>Total time visitors actively engaged with content</td>
    </tr>
    <tr>
        <td><strong>Engagement Rate</strong></td>
        <td>Engaged visits (>30 sec) / Total visits</td>
    </tr>
    <tr>
        <td><strong>Scroll Depth</strong></td>
        <td>How far down the page visitors scroll</td>
    </tr>
    <tr>
        <td><strong>CMPI</strong></td>
        <td>Content Marketing Performance Index (composite score)</td>
    </tr>
</table>
"
                },
                new Lesson
                {
                    Id = "an-dashboard",
                    ModuleId = "analytics",
                    Title = "The CMP Dashboard",
                    Summary = "Learn to use the CMP dashboard for performance insights.",
                    Order = 2,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Navigate the CMP dashboard",
                        "Understand dashboard widgets",
                        "Use the Content Marketing Performance Index"
                    },
                    Content = @"
<h2>The CMP Dashboard</h2>
<p>The dashboard provides a high-level view of your content marketing performance.</p>

<h3>Dashboard Sections</h3>
<ul>
    <li><strong>Performance Views</strong> - Published content metrics</li>
    <li><strong>Efficiency Views</strong> - Team productivity metrics</li>
    <li><strong>Progress Views</strong> - Goal tracking</li>
</ul>

<h3>Content Marketing Performance Index (CMPI)</h3>
<p>The CMPI is a composite score (out of 100) measuring:</p>
<ul>
    <li><strong>Traffic</strong> (25 points) - Volume of visitors</li>
    <li><strong>Engagement</strong> (25 points) - Quality of engagement</li>
    <li><strong>Action</strong> (25 points) - Conversions and actions</li>
    <li><strong>Monetization</strong> (25 points) - Revenue impact</li>
</ul>

<h3>Dashboard Widgets</h3>
<ul>
    <li><strong>Top Performing Content</strong> - Best articles by metrics</li>
    <li><strong>Channel Performance</strong> - Traffic by source</li>
    <li><strong>Team Activity</strong> - Recent completions and updates</li>
    <li><strong>Campaigns by Status</strong> - Progress visualization</li>
</ul>

<h3>Efficiency Dashboards</h3>
<p>Track team productivity:</p>
<ul>
    <li>Tasks completed over time</li>
    <li>Average workflow duration</li>
    <li>Campaign completion rates</li>
    <li>Resource utilization</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "an-reports",
                    ModuleId = "analytics",
                    Title = "Generating Reports",
                    Summary = "Learn to create and run reports in CMP.",
                    Order = 3,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Generate content performance reports",
                        "Create usage and activity reports",
                        "Export data for analysis"
                    },
                    Content = @"
<h2>CMP Reports</h2>
<p>Run and download reports to analyze your CMP data.</p>

<h3>Report Types</h3>
<ul>
    <li><strong>Campaign Performance</strong> - Metrics by campaign</li>
    <li><strong>Content Performance</strong> - Article-level analytics</li>
    <li><strong>Usage Reports</strong> - Platform adoption metrics</li>
    <li><strong>Activity Log</strong> - User actions audit trail</li>
    <li><strong>Library Usage</strong> - Asset utilization</li>
    <li><strong>Storage Usage</strong> - DAM storage consumption</li>
</ul>

<h3>Campaign Performance Reports</h3>
<p>View performance metrics including:</p>
<ul>
    <li>Pageviews per campaign</li>
    <li>Unique visitors</li>
    <li>Attention time</li>
    <li>Engagement rate</li>
    <li>Content by channel</li>
</ul>

<h3>Usage Reporting</h3>
<p>Track platform adoption:</p>
<ul>
    <li>User activity levels</li>
    <li>Campaign creation trends</li>
    <li>Task completion rates</li>
    <li>Workflow interactions</li>
</ul>

<h3>Activity Log</h3>
<p>The Activity Log provides:</p>
<ul>
    <li>Time-stamped record of actions</li>
    <li>User accountability</li>
    <li>Compliance auditing</li>
    <li>Troubleshooting information</li>
</ul>

<h3>Exporting Data</h3>
<p>Export reports in various formats:</p>
<ul>
    <li>CSV for spreadsheet analysis</li>
    <li>PDF for sharing</li>
    <li>Scheduled email delivery</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "an-content",
                    ModuleId = "analytics",
                    Title = "Content Performance Analysis",
                    Summary = "Learn to analyze individual content performance.",
                    Order = 4,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Analyze content performance metrics",
                        "Identify top performing content",
                        "Use insights to improve strategy"
                    },
                    Content = @"
<h2>Content Performance Analysis</h2>
<p>Dive deep into individual content performance to optimize your strategy.</p>

<h3>Content Analytics View</h3>
<p>For each piece of content, see:</p>
<ul>
    <li>Total pageviews and unique visitors</li>
    <li>Average attention time</li>
    <li>Engagement rate</li>
    <li>Scroll depth</li>
    <li>Traffic sources</li>
</ul>

<h3>Performance Comparison</h3>
<p>Compare content performance:</p>
<ul>
    <li>Against other content</li>
    <li>Against campaign averages</li>
    <li>Over different time periods</li>
    <li>Across channels</li>
</ul>

<h3>Identifying Winners</h3>
<p>Look for patterns in top performers:</p>
<ul>
    <li>Common topics or themes</li>
    <li>Effective formats</li>
    <li>Optimal content length</li>
    <li>Best publishing times</li>
</ul>

<h3>Optimization Insights</h3>
<p>Use analytics to improve:</p>
<ul>
    <li>Content topics and angles</li>
    <li>Headlines and titles</li>
    <li>Publishing schedules</li>
    <li>Channel distribution</li>
</ul>
"
                }
            }
        };
    }

    #endregion

    #region Module 9: Collaboration

    private LearningModule BuildCollaborationModule()
    {
        return new LearningModule
        {
            Id = "collaboration",
            Title = "Teams & Collaboration",
            Description = "Learn to work effectively with teams and manage permissions in CMP.",
            Icon = "user-group",
            Order = 9,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "tasks-workflows" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "collab-teams",
                    ModuleId = "collaboration",
                    Title = "Managing Teams",
                    Summary = "Learn to create and manage teams in CMP.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Create and configure teams",
                        "Add users to teams",
                        "Use teams in workflows"
                    },
                    Content = @"
<h2>Teams in CMP</h2>
<p>Teams help organize users and streamline collaboration.</p>

<h3>What are Teams?</h3>
<p>Teams in CMP:</p>
<ul>
    <li>Group users with common responsibilities</li>
    <li>Simplify workflow assignments</li>
    <li>Enable team-based routing</li>
    <li>Provide shared views and access</li>
</ul>

<h3>Creating Teams</h3>
<ol>
    <li>Go to <strong>Settings > Users & Teams</strong></li>
    <li>Click <strong>Create Team</strong></li>
    <li>Name your team</li>
    <li>Add team members</li>
    <li>Optionally set a team avatar</li>
</ol>

<h3>Team Examples</h3>
<ul>
    <li><strong>Content Team</strong> - Writers and editors</li>
    <li><strong>Social Media Team</strong> - Social managers</li>
    <li><strong>Design Team</strong> - Graphic designers</li>
    <li><strong>Approvers</strong> - Managers with approval authority</li>
</ul>

<h3>Using Teams</h3>
<p>Teams can be used to:</p>
<ul>
    <li>Assign workflow steps</li>
    <li>Route work requests</li>
    <li>Share campaign access</li>
    <li>Filter views by team</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "collab-roles",
                    ModuleId = "collaboration",
                    Title = "User Roles and Permissions",
                    Summary = "Understand user roles and how to manage permissions.",
                    Order = 2,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Understand CMP user roles",
                        "Know what each role can do",
                        "Configure custom roles"
                    },
                    Content = @"
<h2>User Roles</h2>
<p>CMP uses roles to control what users can do in the platform.</p>

<h3>Standard Roles</h3>
<table>
    <tr>
        <td><strong>Administrator</strong></td>
        <td>Full access to all features and settings</td>
    </tr>
    <tr>
        <td><strong>Manager</strong></td>
        <td>Create campaigns, manage team work, approve content</td>
    </tr>
    <tr>
        <td><strong>Contributor</strong></td>
        <td>Create tasks, work on assigned items, create content</td>
    </tr>
    <tr>
        <td><strong>Collaborator</strong></td>
        <td>Act on assigned work, approve, comment (cannot create)</td>
    </tr>
    <tr>
        <td><strong>Viewer</strong></td>
        <td>View-only access to shared content</td>
    </tr>
</table>

<h3>Role Capabilities</h3>
<p>Key differences between roles:</p>
<ul>
    <li><strong>Create</strong> - Who can create campaigns and tasks</li>
    <li><strong>Edit</strong> - Who can modify content</li>
    <li><strong>Approve</strong> - Who can move work through workflows</li>
    <li><strong>Publish</strong> - Who can publish to channels</li>
    <li><strong>Admin</strong> - Who can access settings</li>
</ul>

<h3>Custom Roles</h3>
<p>Administrators can create custom roles with specific permissions:</p>
<ul>
    <li>Combine permissions for specific needs</li>
    <li>Match organizational job functions</li>
    <li>Restrict access to sensitive features</li>
</ul>

<h3>Assigning Roles</h3>
<ol>
    <li>Go to <strong>Settings > Users & Teams</strong></li>
    <li>Select a user</li>
    <li>Choose their role</li>
    <li>Save changes</li>
</ol>
"
                },
                new Lesson
                {
                    Id = "collab-fields",
                    ModuleId = "collaboration",
                    Title = "Fields and Labels",
                    Summary = "Learn to create and manage fields for organizing work.",
                    Order = 3,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Understand CMP fields",
                        "Create custom fields",
                        "Use fields for organization and filtering"
                    },
                    Content = @"
<h2>Fields in CMP</h2>
<p>Fields let you manage metadata across campaigns, tasks, and assets.</p>

<h3>What are Fields?</h3>
<p>Fields are metadata you can attach to objects in CMP. They help you:</p>
<ul>
    <li>Categorize and organize work</li>
    <li>Filter and search efficiently</li>
    <li>Generate meaningful reports</li>
    <li>Maintain consistent taxonomy</li>
</ul>

<h3>Field Types</h3>
<table>
    <tr>
        <td><strong>Label</strong></td>
        <td>Color-coded tags (e.g., Department, Content Type)</td>
    </tr>
    <tr>
        <td><strong>Dropdown</strong></td>
        <td>Single selection from options</td>
    </tr>
    <tr>
        <td><strong>Checkbox</strong></td>
        <td>Multiple selections</td>
    </tr>
    <tr>
        <td><strong>Radio Button</strong></td>
        <td>Single selection (always visible)</td>
    </tr>
    <tr>
        <td><strong>Text</strong></td>
        <td>Free-form text entry</td>
    </tr>
    <tr>
        <td><strong>Rich Text</strong></td>
        <td>Formatted text</td>
    </tr>
    <tr>
        <td><strong>Date</strong></td>
        <td>Date picker</td>
    </tr>
</table>

<h3>Creating Fields</h3>
<ol>
    <li>Go to <strong>Settings > Fields</strong></li>
    <li>Click <strong>Add Field</strong></li>
    <li>Select the field type</li>
    <li>Enter the field name</li>
    <li>Add options (for labels, dropdowns, etc.)</li>
    <li>Configure where the field appears</li>
</ol>

<h3>Label Colors</h3>
<p>For label fields:</p>
<ul>
    <li>Assign colors for visual identification</li>
    <li>Colors appear in timeline and calendar</li>
    <li>Help quickly identify work types</li>
</ul>

<h3>Field Settings</h3>
<ul>
    <li><strong>Mandatory</strong> - Required at creation or completion</li>
    <li><strong>Module Display</strong> - Where the field appears</li>
    <li><strong>Active/Inactive</strong> - Enable or disable fields</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "collab-fields-example",
                            Title = "Common Field Examples",
                            Description = "See examples of useful custom fields.",
                            Type = ExampleType.Configuration,
                            ExampleContent = @"Example Fields:

1. Department (Label)
   Options: Marketing, Sales, Product, HR, Finance
   Colors: Blue, Green, Purple, Orange, Gray
   Mandatory: At Task Creation

2. Content Type (Label)
   Options: Blog Post, Social, Email, Video, Infographic
   Mandatory: At Task Creation

3. Target Audience (Dropdown)
   Options: Enterprise, SMB, Developers, Marketing Leaders

4. Region (Checkbox)
   Options: North America, EMEA, APAC, LATAM
   (Multiple regions can be selected)

5. Campaign Theme (Label)
   Options: Product Launch, Brand Awareness, Lead Gen, Retention

6. Priority (Dropdown)
   Options: Low, Medium, High, Critical
   Mandatory: Yes",
                            SampleResponse = @"These fields enable:

- Filtering by department or content type
- Reporting on content mix
- Assigning based on region
- Prioritizing work effectively

Best practices:
- Start with essential fields
- Add more as needs emerge
- Keep option lists manageable
- Review and clean up periodically"
                        }
                    }
                }
            }
        };
    }

    #endregion

    #region Module 10: Opal Integration

    private LearningModule BuildOpalIntegrationModule()
    {
        return new LearningModule
        {
            Id = "opal-integration",
            Title = "Opal AI in CMP",
            Description = "Learn how Optimizely Opal enhances CMP with AI capabilities.",
            Icon = "sparkles",
            Order = 10,
            Difficulty = ModuleDifficulty.Advanced,
            Prerequisites = new[] { "content-creation", "work-requests" },
            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = "opal-overview",
                    ModuleId = "opal-integration",
                    Title = "Opal in CMP Overview",
                    Summary = "Discover how Opal AI capabilities integrate with CMP.",
                    Order = 1,
                    EstimatedMinutes = 8,
                    LearningObjectives = new List<string>
                    {
                        "Understand Opal's role in CMP",
                        "Learn about AI-powered features",
                        "Know how credits work"
                    },
                    Content = @"
<h2>Opal AI in CMP</h2>
<p>Optimizely Opal brings AI capabilities directly into your CMP workflows.</p>

<h3>What Can Opal Do in CMP?</h3>
<ul>
    <li><strong>Campaign Creation</strong> - Create entire campaigns with AI assistance</li>
    <li><strong>Content Generation</strong> - Generate blogs, social posts, emails</li>
    <li><strong>Brief Generation</strong> - Auto-generate campaign briefs</li>
    <li><strong>Workflow Assistance</strong> - Move tasks through workflows</li>
    <li><strong>Translation</strong> - Translate content to other languages</li>
</ul>

<h3>Accessing Opal</h3>
<p>Look for the <strong>""Ask Opal""</strong> button:</p>
<ul>
    <li>In the main CMP interface</li>
    <li>Within the content editor</li>
    <li>On campaigns and tasks</li>
</ul>

<h3>Credit-Based Model</h3>
<p>Opal features use a credit-based billing model:</p>
<ul>
    <li>Usage incurs credit charges</li>
    <li>Monitor usage in admin settings</li>
    <li>Credits apply across Optimizely products</li>
</ul>

<h3>Key Benefits</h3>
<ul>
    <li>Faster content creation</li>
    <li>Consistent brand voice</li>
    <li>Automated routine tasks</li>
    <li>Intelligent suggestions</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "opal-campaigns",
                    ModuleId = "opal-integration",
                    Title = "Creating Campaigns with Opal",
                    Summary = "Learn to use Opal for AI-powered campaign creation.",
                    Order = 2,
                    EstimatedMinutes = 10,
                    LearningObjectives = new List<string>
                    {
                        "Create campaigns using Opal",
                        "Generate campaign briefs with AI",
                        "Get task suggestions from Opal"
                    },
                    Content = @"
<h2>AI-Powered Campaign Creation</h2>
<p>Opal can help you create entire on-brand campaigns from a simple prompt.</p>

<h3>Creating a Campaign with Opal</h3>
<ol>
    <li>Click <strong>Ask Opal</strong></li>
    <li>Describe the campaign you need</li>
    <li>Opal creates the campaign structure</li>
    <li>Review and refine the generated content</li>
</ol>

<h3>What Opal Generates</h3>
<ul>
    <li><strong>Campaign Setup</strong> - Name, dates, description</li>
    <li><strong>Campaign Brief</strong> - Objectives, audience, key messages</li>
    <li><strong>Suggested Tasks</strong> - Work items to execute the campaign</li>
    <li><strong>Content Ideas</strong> - Blogs, social posts, emails</li>
</ul>

<h3>Brief Generation</h3>
<p>Opal can generate comprehensive briefs including:</p>
<ul>
    <li>Campaign objectives</li>
    <li>Target audience profiles</li>
    <li>Key messages and talking points</li>
    <li>Success metrics</li>
</ul>

<h3>Task Suggestions</h3>
<p>Based on your campaign, Opal suggests:</p>
<ul>
    <li>Content pieces to create</li>
    <li>Channels to target</li>
    <li>Timeline recommendations</li>
    <li>Resource requirements</li>
</ul>
",
                    Examples = new List<LessonExample>
                    {
                        new LessonExample
                        {
                            Id = "opal-campaign-example",
                            Title = "Campaign Creation Prompt",
                            Description = "Example of using Opal to create a campaign.",
                            Type = ExampleType.Prompt,
                            ExampleContent = @"Create a product launch campaign for our new
AI-powered analytics dashboard.

The launch is scheduled for March 15th.
Target audience is enterprise marketing teams.
We need social media, email, and blog content.
The key message is ""Analytics made simple with AI"".",
                            SampleResponse = @"I've created your campaign: ""AI Analytics Dashboard Launch""

Campaign Brief:
- Objective: Generate awareness and drive sign-ups for the new AI analytics dashboard
- Target Audience: Enterprise marketing teams (CMOs, Marketing Directors, Marketing Ops)
- Key Message: Analytics made simple with AI
- Launch Date: March 15, 2025

Suggested Tasks:
1. Blog Post: ""Introducing AI-Powered Analytics"" (Due: March 8)
2. Product Demo Video (Due: March 10)
3. Launch Email Sequence (3 emails) (Due: March 12)
4. Social Media Campaign:
   - LinkedIn announcement posts (Due: March 13)
   - Twitter thread (Due: March 13)
5. Press Release (Due: March 14)

Would you like me to create these tasks and generate draft content for any of them?",
                            Hints = new List<string>
                            {
                                "Be specific about your target audience for better results",
                                "Include key dates and constraints in your prompt"
                            }
                        }
                    }
                },
                new Lesson
                {
                    Id = "opal-content",
                    ModuleId = "opal-integration",
                    Title = "Content Generation with Opal",
                    Summary = "Learn to generate content using Opal in CMP.",
                    Order = 3,
                    EstimatedMinutes = 9,
                    LearningObjectives = new List<string>
                    {
                        "Generate content with Opal",
                        "Edit and refine AI-generated content",
                        "Maintain brand voice with AI"
                    },
                    Content = @"
<h2>AI Content Generation</h2>
<p>Opal helps you create on-brand content quickly and efficiently.</p>

<h3>Content Types</h3>
<p>Generate various content formats:</p>
<ul>
    <li>Blog posts and articles</li>
    <li>Social media posts</li>
    <li>Email campaigns</li>
    <li>Product descriptions</li>
    <li>Marketing copy</li>
</ul>

<h3>Using Opal for Content</h3>
<ol>
    <li>Open a task or the content editor</li>
    <li>Click <strong>Ask Opal</strong></li>
    <li>Describe the content you need</li>
    <li>Review and edit the generated content</li>
    <li>Iterate with follow-up prompts</li>
</ol>

<h3>Brand Voice</h3>
<p>Opal uses your configured instructions to:</p>
<ul>
    <li>Match your brand tone</li>
    <li>Follow style guidelines</li>
    <li>Use approved terminology</li>
    <li>Maintain consistency</li>
</ul>

<h3>Editing AI Content</h3>
<p>Always review and refine generated content:</p>
<ul>
    <li>Verify facts and claims</li>
    <li>Add specific details</li>
    <li>Adjust tone as needed</li>
    <li>Include company-specific information</li>
</ul>
"
                },
                new Lesson
                {
                    Id = "opal-workflows",
                    ModuleId = "opal-integration",
                    Title = "Opal in Workflows",
                    Summary = "Learn how Opal assists with workflow management.",
                    Order = 4,
                    EstimatedMinutes = 7,
                    LearningObjectives = new List<string>
                    {
                        "Use Opal to manage workflow steps",
                        "Automate work request handling",
                        "Get AI assistance during reviews"
                    },
                    Content = @"
<h2>Opal Workflow Assistance</h2>
<p>Opal can help manage tasks as they move through workflows.</p>

<h3>Workflow Actions</h3>
<p>Opal can:</p>
<ul>
    <li>Move tasks forward through steps</li>
    <li>Send tasks backward for revisions</li>
    <li>Un-complete previous steps</li>
    <li>Add comments and feedback</li>
</ul>

<h3>Work Request Automation</h3>
<p>Opal can read work request details:</p>
<ul>
    <li>Form submission content</li>
    <li>Assignees and priority</li>
    <li>Request status</li>
</ul>

<h3>Auto-Accept High Priority</h3>
<p>Configure Opal to automatically:</p>
<ul>
    <li>Accept high-priority requests</li>
    <li>Create campaigns from request info</li>
    <li>Assign to appropriate teams</li>
</ul>

<h3>AI Translation</h3>
<p>Use Opal to translate content:</p>
<ul>
    <li>Translate to any locale</li>
    <li>Work directly in the omnichannel editor</li>
    <li>Maintain formatting and structure</li>
</ul>

<h3>Best Practices</h3>
<ul>
    <li>Review AI actions before publishing</li>
    <li>Use automation for routine tasks</li>
    <li>Keep humans in the loop for critical decisions</li>
</ul>
"
                }
            }
        };
    }

    #endregion
}
