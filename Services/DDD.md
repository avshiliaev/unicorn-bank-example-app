## DDD

### Strategic

More on the topic [here](https://docs.microsoft.com/en-us/azure/architecture/microservices/model/domain-analysis)

During the strategic phase of DDD, you are mapping out the business domain and defining bounded contexts for your domain models.

A bounded context is simply the boundary within a domain where a particular domain model applies. Looking at the previous diagram, we can group functionality according to whether various functions will share a single domain model.

Bounded contexts:
* Accounts
* Transactions
**Bounded contexts are not necessarily isolated from one another.** 

Events can inherit from same interface (well defined APIs and so on), as well as entities in the same bounded context (but hey are better to be inherited from events, not interface in SDK)?.  
Entities across context should not!
See https://docs.microsoft.com/en-us/azure/architecture/microservices/model/domain-analysis#define-bounded-contexts 
> If we tried to create a single model for both of these subsystems, it would be unnecessarily complex. It would also become harder for the model to evolve over time, because any changes will need to satisfy multiple teams working on separate subsystems. Therefore, it's often better to design separate models that represent the same real-world entity (in this case, a drone) in two different contexts. Each model contains only the features and attributes that are relevant within its particular context.

### Tactical

More on the topic [here](https://docs.microsoft.com/en-us/azure/architecture/microservices/model/tactical-ddd)

Tactical DDD is when you define your domain models with more precision. The tactical patterns are applied within a single bounded context. In a microservices architecture, we are particularly interested in the entity and aggregate patterns. Applying these patterns will help us to identify natural boundaries for the services in our application

> As a general principle, a microservice should be no smaller than an aggregate, and no larger than a bounded context. First, we'll review the tactical patterns. Then we'll apply them to the Shipping bounded context in the Drone Delivery application.

**Entities.** An entity is an object with a unique identity that persists over time. For example, in a banking application, customers and accounts would be entities.
* An entity has a unique identifier in the system, which can be used to look up or retrieve the entity. That doesn't mean the identifier is always exposed directly to users. It could be a GUID or a primary key in a database.
* An identity may span multiple bounded contexts, and may endure beyond the lifetime of the application. For example, bank account numbers or government-issued IDs are not tied to the lifetime of a particular application.
* The attributes of an entity may change over time. For example, a person's name or address might change, but they are still the same person.
An entity can hold references to other entities.

**Value objects.** A value object has no identity. It is defined only by the values of its attributes. Value objects are also immutable. To update a value object, you always create a new instance to replace the old one. Value objects can have methods that encapsulate domain logic, but those methods should have no side-effects on the object's state. Typical examples of value objects include colors, dates and times, and currency values.

**Aggregates.** An aggregate defines a consistency boundary around one or more entities. Exactly one entity in an aggregate is the root. Lookup is done using the root entity's identifier. Any other entities in the aggregate are children of the root, and are referenced by following pointers from the root.

The purpose of an aggregate is to model transactional invariants. Things in the real world have complex webs of relationships. Customers create orders, orders contain products, products have suppliers, and so on. If the application modifies several related objects, how does it guarantee consistency? How do we keep track of invariants and enforce them? Traditional applications have often used database transactions to enforce consistency. In a distributed application, however, that's often not feasible. A single business transaction may span multiple data stores, or may be long running, or may involve third-party services. **Ultimately it's up to the application, not the data layer, to enforce the invariants required for the domain.** That's what aggregates are meant to model.

> Note
> **An aggregate might consist of a single entity, without child entities. What makes it an aggregate is the transactional boundary.**

**Domain and application services.** In DDD terminology, a service is an object that implements some logic without holding any state. Evans distinguishes between domain services, which encapsulate domain logic, and application services, which provide technical functionality, such as user authentication or sending an SMS message. Domain services are often used to model behavior that spans multiple entities.















