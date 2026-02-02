# Optimizely Learning Centre

An interactive multi-course learning platform for Optimizely products. Built with Blazor WebAssembly, this client-side application provides hands-on tutorials and interactive examples to help developers master Optimizely technologies.

## Available Courses

| Course | Description |
|--------|-------------|
| **Graph** | [Optimizely Graph](https://docs.developers.optimizely.com/content-graph) - GraphQL API for content delivery |
| **Opal** | [Optimizely Opal](https://docs.developers.optimizely.com/opal) - AI-assisted content creation |
| **SaaS** | Optimizely SaaS CMS - Cloud-native content management |
| **CMS 12** | Optimizely CMS 12 - .NET-based content management |
| **CMS 13** | Optimizely CMS 13 - Latest CMS version features |
| **DXP** | Digital Experience Platform - Full DXP capabilities |
| **CMP** | Content Marketing Platform - Marketing content workflows |
| **Web Experimentation** | A/B testing and website optimization |
| **Feature Experimentation** | Feature flags and server-side experimentation |
| **Commerce** | [Optimizely Commerce Connect](https://docs.developers.optimizely.com/commerce-connect) - E-commerce integration |
| **Content Recommendations** | [Content Recommendations](https://docs.developers.optimizely.com/recommendations) - AI-powered content recommendations |
| **ODP** | [Optimizely Data Platform](https://docs.developers.optimizely.com/optimizely-data-platform) - Customer data platform |
| **Configured Commerce** | [Configured Commerce](https://docs.developers.optimizely.com/configured-commerce) - B2B commerce solution |
| **Analytics** | [Optimizely Analytics](https://docs.developers.optimizely.com/analytics) - Web analytics and reporting |

## Features

- **Multi-Course Platform** - Learn multiple Optimizely products from a single application
- **Interactive Learning Modules** - Step-by-step tutorials covering Optimizely concepts
- **Course-Specific Tools** - GraphQL Playground, Query Builder, and other interactive tools
- **Hands-on Examples** - Execute queries and prompts directly within lessons
- **Progress Tracking** - Track your learning progress across courses
- **Pluggable Architecture** - Easily extensible for new courses

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later
- [Node.js](https://nodejs.org/) (for Tailwind CSS compilation)
- An Optimizely Graph instance (for query execution)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-org/opti-graph-learning-centre.git
cd opti-graph-learning-centre
```

### 2. Install npm dependencies

```bash
cd src/OptimizelyLearningCentre.Client
npm install
```

### 3. Build Tailwind CSS

```bash
npm run css:build
```

### 4. Run the application

```bash
dotnet run --project src/OptimizelyLearningCentre.Client/OptimizelyLearningCentre.Client.csproj
```

The application will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:7168

## Development

### Build Commands

```bash
# Build the application
dotnet build src/OptimizelyLearningCentre.Client/OptimizelyLearningCentre.Client.csproj

# Run in development mode
dotnet run --project src/OptimizelyLearningCentre.Client/OptimizelyLearningCentre.Client.csproj

# Watch Tailwind CSS changes (run in a separate terminal)
cd src/OptimizelyLearningCentre.Client && npm run css:watch

# Build minified CSS for production
cd src/OptimizelyLearningCentre.Client && npm run css:build
```

## Project Structure

```
src/OptimizelyLearningCentre.Client/
├── Components/
│   ├── Common/           # Reusable UI components (Badge, Card, JsonViewer, LoadingSpinner)
│   └── Learning/         # Learning-specific components (TryItPanel)
├── Courses/              # Course-specific implementations
│   ├── Graph/            # Optimizely Graph course
│   │   ├── GraphCourse.cs
│   │   ├── GraphContentProvider.cs
│   │   ├── Pages/        # Playground, QueryBuilder, Settings
│   │   └── Components/   # TryItPanel, etc.
│   ├── Opal/             # Opal AI course
│   ├── SaaS/             # SaaS CMS course
│   ├── CMS12/            # CMS 12 course
│   ├── CMS13/            # CMS 13 course
│   ├── DXP/              # DXP course
│   ├── CMP/              # CMP course
│   ├── WebExp/           # Web Experimentation course
│   ├── FeatureExp/       # Feature Experimentation course
│   ├── Commerce/         # Commerce Connect course
│   ├── ContentRecs/      # Content Recommendations course
│   ├── ODP/              # Optimizely Data Platform course
│   ├── ConfiguredCommerce/  # Configured Commerce course
│   └── Analytics/        # Optimizely Analytics course
├── Layout/               # MainLayout and NavMenu
├── Models/
│   ├── Configuration/    # Connection settings
│   ├── Learning/         # Learning module models
│   ├── Query/            # Query definition models
│   ├── Schema/           # Schema introspection models
│   └── UI/               # UI enumerations
├── Pages/                # Shared pages
│   ├── Home.razor        # Landing page
│   └── Learn/            # Learning modules and lessons
├── Services/             # Business logic services
├── State/                # Application state management
├── Styles/               # Source Tailwind CSS
└── wwwroot/              # Static assets and compiled CSS
```

## Technology Stack

- **.NET 10.0** with **Blazor WebAssembly**
- **C# 13+** with nullable reference types
- **Tailwind CSS 3.4** for styling
- **Blazored.LocalStorage** for browser persistence

## Authentication

The application supports three authentication modes for connecting to Optimizely Graph:

| Mode | Description | Use Case |
|------|-------------|----------|
| **HMAC** | App Key + Secret authentication | Secured queries, draft content access |
| **SingleKey** | Public single authentication key | Public read-only access |
| **None** | No authentication | Public endpoints only |

Configure your connection settings in the Settings page of the application.

## Architecture

### Multi-Course System

The platform uses a pluggable course system centered around `CourseDefinition` and `ICourseRegistry`:

- **ICourseRegistry** (Singleton) - Manages all registered courses
- **ICourseContext** - Provides current course context and navigation
- **ILearningContentProvider** - Interface implemented by each course's content provider

Each course provides:
- `CourseDefinition` - Metadata, navigation items, brand colors, external links
- `ContentProvider` - Implements `ILearningContentProvider` with modules and lessons
- Course-specific pages (e.g., Settings, Playground)
- Optional interactive components

### State Management

The application uses property-based state stores implementing `INotifyPropertyChanged`:

- **AppState** - Global app state: connection status, sidebar toggle, loading indicators, notifications
- **QueryState** - Query builder state: current query definition, raw query, variables, execution history

### Core Services

All services are registered as Scoped in the DI container:

- **ILearningService** - Orchestrates learning content across courses
- **ISettingsService** - Persists settings to browser localStorage
- **IGraphQLClient** - Executes GraphQL queries with authentication (Graph course)
- **ISchemaService** - GraphQL schema introspection with caching (Graph course)
- **IQueryBuilderService** - Constructs GraphQL queries from structured definitions (Graph course)

## Adding a New Course

1. Create folder `Courses/{CourseName}/`
2. Add `{CourseName}Course.cs` with a static `Definition` property returning `CourseDefinition`
3. Add `{CourseName}ContentProvider.cs` implementing `ILearningContentProvider`
4. Register the course in `Program.cs`: `registry.RegisterCourse({CourseName}Course.Definition)`
5. Register the content provider: `builder.Services.AddScoped<{CourseName}ContentProvider>()`
6. Add course-specific pages in `Courses/{CourseName}/Pages/` as needed

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

[MIT License](LICENSE)

## Resources

- [Optimizely Developer Portal](https://docs.developers.optimizely.com/)
- [Optimizely Graph Documentation](https://docs.developers.optimizely.com/content-graph)
- [Optimizely Opal Documentation](https://docs.developers.optimizely.com/opal)
- [Optimizely CMS Documentation](https://docs.developers.optimizely.com/content-cloud)
- [Optimizely Feature Experimentation](https://docs.developers.optimizely.com/feature-experimentation)
- [Optimizely Web Experimentation](https://docs.developers.optimizely.com/web-experimentation)
- [Optimizely Commerce Connect](https://docs.developers.optimizely.com/commerce-connect)
- [Optimizely Data Platform](https://docs.developers.optimizely.com/optimizely-data-platform)
- [Optimizely Configured Commerce](https://docs.developers.optimizely.com/configured-commerce)
- [Optimizely Content Recommendations](https://docs.developers.optimizely.com/recommendations)
- [Optimizely Analytics](https://docs.developers.optimizely.com/analytics)
- [Blazor WebAssembly Documentation](https://docs.microsoft.com/aspnet/core/blazor/)
