# HomeCompass AI - Architecture Decisions

## Purpose

This document records the main architectural decisions made during the development of HomeCompass AI.

The objective is to build an AI-powered real estate personal advisor that can evolve from a portfolio project into a potential real product.

The application should demonstrate modern software architecture practices while maintaining realistic development complexity.

---

# ADR-001: Start with a Modular Monolith Architecture

## Date

2026-07-12

## Context

HomeCompass AI is expected to grow in capabilities:

- property management
- buyer profiles
- recommendation engine
- AI interpretation
- machine learning models
- external integrations
  A microservices architecture could support these capabilities, but introducing distributed systems too early would add unnecessary complexity:

- service communication
- deployment overhead
- infrastructure requirements
- operational complexity
  The initial goal is to build a functional product foundation while learning modern architecture practices.

## Decision

HomeCompass AI will initially be implemented as a **Modular Monolith**.

The application will have clear internal boundaries between business capabilities while remaining a single deployable application.

Initial conceptual modules:

```
HomeCompass

├── Property Module
├── Buyer Profile Module
├── Recommendation Module
├── AI Module
└── Data Access Module
```

## Consequences

Positive:

- Faster development
- Easier local deployment
- Simpler debugging
- Clear architectural boundaries
- Future extraction into microservices remains possible
  Negative:

- Requires discipline to maintain module boundaries
- Does not provide immediate microservices experience

## Future Evolution

If the product requires independent scaling or deployment, selected modules may become independent services.

Possible future candidates:

- Recommendation Service
- AI Processing Service
  The extraction decision should be driven by business needs, not technology preference.

---

# ADR-002: Use PostgreSQL with JSONB for Flexible AI-Oriented Data

## Date

2026-07-12

## Context

A real estate recommendation system needs to represent both structured and human-oriented information.

Traditional relational data works well for:

- property information
- financial calculations
- transactions
- reporting
  However, user preferences and lifestyle information are naturally flexible.

Examples:

- accessibility requirements
- lifestyle preferences
- personal priorities
- AI-generated context
- conversation-derived information
  These concepts may evolve frequently.

## Decision

The application will use PostgreSQL with a hybrid relational/document approach.

Structured data will use relational tables.

Flexible AI-oriented data will use PostgreSQL JSONB columns.

Example:

```
BuyerProfile

Id
Name
AnnualIncome
MaximumMonthlyPayment
ProfileData (JSONB)
```

Example JSONB content:

```
{
  "accessibility": {
    "needsGoodLighting": true,
    "needsElevator": true
  },
  "lifestyle": {
    "prefersQuietAreas": true,
    "nearHealthcare": true
  }
}
```

## Consequences

Positive:

- Strong relational capabilities
- Flexible schema evolution
- Natural fit for AI context
- Practice with modern database design
  Negative:

- Requires careful decisions about what belongs in relational columns versus JSON documents
- Queries over JSON data require additional consideration

---

# ADR-003: Separate AI Understanding from Business Decisions

## Date

2026-07-12

## Context

Large Language Models are excellent at understanding human language but should not directly control business decisions.

Example:

A user may say:

"I want somewhere peaceful because I work remotely and I do not like driving too much."

An AI model can interpret this into structured preferences.

However, the recommendation engine should apply deterministic business rules and machine learning models.

## Decision

The system will separate responsibilities:

```
User Language

      |
      v

LLM Layer
(understanding and extraction)

      |
      v

Structured User Profile

      |
      v

Recommendation Engine

      |
      +----------------+
      |                |
      v                v

 Business Rules     ML.NET Models
```

## Consequences

Positive:

- More predictable recommendations
- Easier testing
- Better explainability
- Clear separation of AI and business logic
  Negative:

- Requires additional design work
- Requires maintaining contracts between AI output and application logic

---

# Architectural Principles

HomeCompass AI follows these principles:

1. Build the simplest architecture that supports the current goal.
2. Avoid premature microservices.
3. Preserve future evolution paths.
4. Keep business rules independent from infrastructure.
5. Use AI to augment human decision-making, not replace deterministic logic.
6. Favor explainable recommendations.
7. Optimize for learning and maintainability.
