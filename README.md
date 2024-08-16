Welcome to the Validation in DDD course.
=====================
This is the source code for my Pluralsight course about Validation in DDD.
The course is currently under development.
To get notified when it's released, subscribe to my email list: https://enterprisecraftsmanship.com/subscribe
How to Get Started:
--------------
No need to set up a database, just hit F5 and you are good to go!


## FluentValidation Fundamentals by Vladimir Khorikov

- INTRODUCTION:
    - Fluent Validation. (Microsoft .NET.) Combined with DDD.
    - Validation (input validation) is the process of ensuring that some data is correct.
    - Data under validation could be anything, but it's typically data from external sources.
    - Sample application:
        - Student management system. Course. Enrollment. Entity (base.) Grade (enum.) Student.
        - Repository wrappers an in-memory 'database.'
        - e.g.: Student id is set with reflection because the property setter is protected.
            - Client code does not have direct access to this property. ORMs are set up like this.

- VALIDATING INPUT WITH FLUENT VALIDATION:
    - Always keep the domain model (problem domain) seperate from data contracts (public interface.)
    - With complex properties, avoid duplication code by creating a seperate validator.
    - Warning: A chained second validation will also be invoked. Beware null.
    - e.g.: SOmething like .NotNull() should be placed explicitly within each validator, and not within a shared codebase.
    - Inheritance validation. .SetInheritanceValidator(). And then interact with each concrete class.
        - This feature is geared toward validating domain-level classes as opposed to contracts.
    - Rule sets: Combine validation rules. With .options.IncludeRuleSets.
        - The library either *only* invokes either the rule sets or the validation rules.
        - Behind the scenes, fluent validation creates a rule set entitled "default."
        - Can use "default" within the options.IncludeRuleSets("Email", "default");
        - Or: options.IncludeRuleSets("Email").IncludeRulesNotInRuleSet();
    - Why? Enables code reuse.
    - Ensure task-based API versus CRUD-based. (e.g.: CQRS.)
    - .ThrowOnFailures(). List of errors upon errant validation. Or helper method .ValidateAndThrow(request);
        - DOn't use exceptions for validation. Validations *are* expected to occur.

- DIVING DEEPER INTO FLUENT VALIDATION:
    - 