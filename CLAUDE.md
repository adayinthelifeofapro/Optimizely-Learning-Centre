# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Optimizely Learning Centre is a **Blazor WebAssembly (WASM)** interactive multi-course learning platform for Optimizely products. This is a client-side only application with no backend server.

**Available Courses:** Graph, Opal, SaaS, CMS12, CMS13, DXP, CMP, WebExp, FeatureExp, Commerce, ContentRecs, ODP, ConfiguredCommerce, Analytics

## Build Commands

```bash
# Build the application
dotnet build src/OptimizelyLearningCentre.Client/OptimizelyLearningCentre.Client.csproj

# Run the application (HTTP: localhost:5000, HTTPS: localhost:7168)
dotnet run --project src/OptimizelyLearningCentre.Client/OptimizelyLearningCentre.Client.csproj

# Build Tailwind CSS (minified for production)
cd src/OptimizelyLearningCentre.Client && npm run css:build

# Watch Tailwind CSS (development mode)
cd src/OptimizelyLearningCentre.Client && npm run css:watch
```

**No test framework is currently configured.**

## Architecture

### Technology Stack
- **.NET 10.0** with **Blazor WebAssembly**
- **C# 13+** with nullable reference types enabled
- **Tailwind CSS 3.4** for styling
- **Blazored.LocalStorage** for browser persistence

### Multi-Course Architecture
The platform uses a pluggable course system centered around `CourseDefinition` and `ICourseRegistry`:

**Course Registration Pattern** (`Program.cs`):
```csharp
builder.Services.AddSingleton<ICourseRegistry>(sp => {
    var registry = new CourseRegistry();
    registry.RegisterCourse(GraphCourse.Definition);
    // ... other courses
    return registry;
});
```

**Each course provides:**
- `CourseDefinition` - Metadata, nav items, brand colors, external links
- `ContentProvider` - Implements `ILearningContentProvider` with modules and lessons
- Course-specific pages (e.g., `Settings.razor`, `Playground.razor`)
- Optional interactive components (e.g., `TryItPanel`)

**Course folder structure** (`Courses/{CourseName}/`):
- `{CourseName}Course.cs` - Static `CourseDefinition`
- `{CourseName}ContentProvider.cs` - Module/lesson content (often large files)
- `Pages/` - Course-specific Razor pages
- `Components/` - Course-specific components

### State Management
Property-based state stores implementing `INotifyPropertyChanged`:
- **AppState** - Global: connection status, sidebar toggle, loading, notifications
- **QueryState** - Query builder: query definition, raw query, variables, history

### Core Services (Scoped in DI)
- **ICourseRegistry** (Singleton) - Manages registered courses
- **ICourseContext** - Current course context and navigation
- **ILearningService** - Orchestrates learning content across courses
- **ISettingsService** - Persists settings to browser localStorage

### Graph-Specific Services
- **IGraphQLClient** - Executes queries with HMAC, SingleKey, or no auth
- **ISchemaService** - Schema introspection with caching
- **IQueryBuilderService** - Constructs GraphQL queries from definitions

### Shared vs Course-Specific Pages
**Shared pages** (`Pages/`): Home, Learn/Index, Learn/Module, About, NotFound
**Course pages** (`Courses/{Course}/Pages/`): Settings, Playground, QueryBuilder

## Styling

Tailwind is configured with custom Optimizely brand colors in `tailwind.config.js`:
- `opti-blue`: #0037FF
- `opti-dark`: #1a1a2e
- `opti-accent`: #00D4AA
- `opti-light`: #f8fafc

Source CSS: `Styles/app.css` → Compiled: `wwwroot/css/app.css`

## Adding a New Course

1. Create folder `Courses/{CourseName}/`
2. Add `{CourseName}Course.cs` with static `Definition` property
3. Add `{CourseName}ContentProvider.cs` implementing `ILearningContentProvider`
4. Register in `Program.cs`: `registry.RegisterCourse({CourseName}Course.Definition)`
5. Register content provider: `builder.Services.AddScoped<{CourseName}ContentProvider>()`
6. Add course-specific pages in `Courses/{CourseName}/Pages/`
