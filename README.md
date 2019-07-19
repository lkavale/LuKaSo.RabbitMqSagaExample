# Lukaso RabbitMQ Saga Example
### About the project
This solution introduce example of containerized system, where the microservices using RabbitMQ message broker for internal communication and REST API for communication with the front-end Angular 7 application.
Solution uses Linux Docker containers and contains Docker compose orchestrator config file with system composition and configuration. System demonstrate usage of RabbitMQ and MassTransit for microservice messaging. Collaboration of microservices is realised by sagas with Automatonymous state machines.
### Overview
Solution contains 5 custom ASP.NET Core 2.2 containerised microservices and one container with RabbitMQ. Entire system is orchestrate by Docker compose.
* **Broker** Simulates bridge between broker and this system. Quartz.net job periodically sending new random investments into the system. Broker waits for confirmation of the investments by the system. Order management and one of the strategies confirmation is required for buying the investment. REST API provides interface for adding investments and displaying own portfolio. 
* **OrderManagement** Order management controls the composition of investments in categories and provides REST API for configuration.
* **Portal** Ocelot middleware merging the REST API of the microservices and makes proxy between portal REST API and microservices APIs. The frontend is Angular 7 application with Angular material design.
* **StrategyA/StrategyB** Strategies with the same internal logic, but it can has different configurations. The parameters could be changed via provided REST API
### Build and run
System could be build and run from workspace, or published and run from conainerised technology, or by the Docker compose.
### Usage
System expose ports 80 and 9001. On 80 is available web application of portal microservice and on 9001 is available RabbitMQ management portal.
* **http://localhost/** Portal web application
* **http://localhost/api** REST API
* **http://localhost/swagger/** Swagger UI portal
* **http://localhost:9001** RabbitMQ management portal