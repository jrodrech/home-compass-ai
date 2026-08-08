# Recommendation Engine

## Purpose

`RecommendationEngine` evaluates the compatibility between a `BuyerProfile` and a `Property`.

It produces a `RecommendationScore` based on business rules that currently represent the buyer's explicit requirements:

* Maximum budget
* Minimum number of bedrooms
* Accessibility requirements

The engine is intentionally implemented in the Domain layer because these rules represent business behavior rather than technical or presentation concerns.

## Why RecommendationEngine Is a Domain Service

`RecommendationEngine` is a Domain Service because the recommendation decision does not naturally belong to a single domain entity.

The evaluation requires information from both:

* `BuyerProfile`
* `Property`

Neither object should be responsible for calculating the compatibility of itself with the other.

Placing this behavior inside `BuyerProfile` would make the buyer responsible for evaluating properties. Placing it inside `Property` would make the property responsible for evaluating buyers.

Neither responsibility accurately represents the meaning of those entities.

The operation is instead a domain operation involving multiple domain objects. `RecommendationEngine` therefore provides a natural home for this behavior.

## Domain Responsibility

The engine owns the rules that determine the current deterministic recommendation score.

The current weighting is:

| Criterion                                      |  Weight |
| ---------------------------------------------- | ------: |
| Property is within buyer's maximum budget      |      40 |
| Property satisfies minimum bedroom requirement |      30 |
| Property satisfies accessibility requirement   |      30 |
| **Total**                                      | **100** |

A criterion either contributes its full weight or contributes zero.

The engine does not persist recommendations, retrieve properties, communicate with external services, or expose HTTP endpoints.

Those responsibilities belong to other architectural layers.

## Deterministic Behavior

The current implementation is intentionally deterministic.

Given the same `BuyerProfile` and `Property`, `RecommendationEngine` must always produce the same `RecommendationScore`.

This property is important because it makes the domain behavior:

* predictable;
* testable;
* independent of infrastructure;
* independent of external services.

The automated tests therefore focus on business outcomes rather than implementation details.

## Why the Logic Does Not Belong in the API

The API layer should expose application capabilities to external consumers.

It should not contain the business rules that determine whether a property is a good match.

Keeping the scoring logic in the Domain layer means that the same recommendation rules can eventually be invoked by:

* an HTTP API;
* a background process;
* a scheduled recommendation job;
* an administrative tool;
* another application interface.

None of those consumers needs to duplicate the business rules.

## Why the Logic Does Not Belong in Persistence

The recommendation score is not a database concern.

Entity Framework Core, repositories, database queries, and persistence mappings should provide mechanisms for storing and retrieving domain information.

They should not determine the meaning of a property recommendation.

Keeping `RecommendationEngine` independent of persistence also allows the domain rules to be tested without requiring a database.

## Current Boundary

The current engine evaluates explicit, structured buyer requirements.

It does not attempt to understand natural-language preferences, infer user intent, or call an AI model.

For example, the engine can evaluate:

* maximum budget = $120,000;
* minimum bedrooms = 3;
* accessibility required = true.

It does not currently interpret a statement such as:

> "I want a quiet neighborhood close to good schools with enough space for my family."

Understanding such preferences belongs to a future capability responsible for translating human input into structured domain information.

## Future AI Integration

The future HomeCompass AI architecture may introduce AI-assisted recommendation capabilities.

That does not mean that `RecommendationEngine` should directly depend on an LLM provider.

The current domain service establishes an important architectural boundary:

**Domain rules should remain independent from the AI infrastructure used to obtain or enrich information.**

A future application flow may therefore look conceptually like:

```text
User preferences
       |
       v
Preference understanding
       |
       v
Structured BuyerProfile
       |
       v
RecommendationEngine
       |
       v
RecommendationScore
```

An AI component may eventually help interpret or enrich the buyer's preferences, but the deterministic domain rules remain explicit and independently testable.

This separation also allows the AI implementation to change without requiring the core recommendation rules to change.

## Testing Strategy

`RecommendationEngine` is tested as a domain component.

Tests verify business outcomes such as:

* a property satisfying all requirements receives a score of 100;
* a property outside the budget loses the budget points;
* a property with insufficient bedrooms loses the bedroom points;
* a property without required accessibility features loses the accessibility points;
* a buyer who does not require accessibility is not penalized;
* invalid inputs are rejected.

The tests use builders to create domain objects with meaningful defaults while allowing individual scenarios to override only the relevant condition.

This keeps the tests focused on the business rule being evaluated.

## Architectural Principle

The recommendation engine follows the principle:

> Business decisions belong in the Domain layer when they represent domain behavior and do not naturally belong to a single entity.

The implementation should remain small and focused.

If recommendation behavior becomes significantly more complex, the domain model should be evolved deliberately rather than turning `RecommendationEngine` into a general-purpose orchestration component.
