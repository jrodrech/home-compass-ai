# Architecture Decisions

## ADR-001: Use .NET 10 default OpenAPI dependency

Date: 2026-07-12

### Context

The ASP.NET Core Web API template for .NET 10.0.9 introduces
Microsoft.AspNetCore.OpenApi, which currently depends on
Microsoft.OpenApi 2.0.0.

NuGet reports a known vulnerability in Microsoft.OpenApi 2.0.0.

### Decision

Keep the default Microsoft.AspNetCore.OpenApi dependency.

### Reasoning

- The dependency is introduced by the official ASP.NET Core template.
- No newer compatible package version is currently available.
- Manually overriding the transitive dependency could create
  compatibility issues.
- The project is currently a development/portfolio POC.

### Future Action

Review this dependency when Microsoft releases an updated package
or when preparing a production deployment.
