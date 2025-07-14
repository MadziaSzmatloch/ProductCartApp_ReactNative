
# ProductCartApplication
This application allows users to browse a list of products and manage their cart by adding or removing items. It consists of an API and a React Native frontend that provides a simple interface for interacting with the API.

## Gataway
The API gateway is built using the Yarp.ReverseProxy package. YARP maps all endpoints exposed by the Cart API and the Product API.

## Product Module
This module manages the list of available products that users can add to their cart. Clean Architecture principles are applied to improve code readability and maintainability. Products are stored in a PostgreSQL database using Entity Framework Core. The MediatR library is used for inter-module communication.

## Cart Module
The Cart module handles all cart-related actions and also follows Clean Architecture principles. It uses MediatR for decoupled communication between components. Event sourcing is implemented using Marten with PostgreSQL to persist cart events. The CQRS pattern is used to separate commands from queries. A scheduled job using the Hangfire package automatically deletes carts that are inactive for 15 minutes.

## Docker 
The entire project is orchestrated using Docker Compose, which includes:
- Gateway API
- Cart API
- Product API
- PostgreSQL database for products
- PostgreSQL database for carts

## React Native App 
he list of products is stored using AsyncStorage to enable continued interaction even when the API is temporarily unavailable.