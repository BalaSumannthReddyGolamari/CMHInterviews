# CMHInterviews

## Introduction

Welcome to the CMHInterviews solution! This repository contains the source code for a .NET application designed to retrieve and find the total number of interviews scheduled in the given number of days and the data we are getting from an external API. The solution is built using the ASP.NET Core framework (6.0 V) and follows a layered architecture to maintain separation of concerns and promote scalability and maintainability.

## Architecture Overview

The solution architecture is structured around the following layers:

### Controller Layer: 
The controller layer consists of classes responsible for handling incoming HTTP requests and returning appropriate responses. Controllers delegate the processing of requests to service classes and orchestrate the flow of data between the client and the application.

### Service Layer: 
The service layer contains classes that implement the business logic of the application. Services perform operations such as retrieving interview data, processing it according to business rules, and returning results to the calling controller.

### Repository Layer: 
Repositories are responsible for data access operations. They provide an abstraction over the underlying data storage mechanisms and handle interactions with external data sources. In this solution, repositories retrieve interview data from an external API.

### HTTP Client Layer: 
The HTTP client layer encapsulates the logic for making HTTP requests to external APIs. It provides functionality for sending requests, handling responses, and processing data returned by the API. In this solution, the HTTP client layer interacts with the external interview API to fetch interview data.

### The solution is organized into two separate projects:

### Source Code Project: 
This project contains the source code for the CMHInterviews application. It includes the implementation of controllers, services, repositories, HTTP client logic, and other components necessary for the application to function. The source code project is structured according to the layered architecture described above.

### Test Case Project: 
The test case project is dedicated to unit testing the functionality of the CMHInterviews application. It contains test cases implemented using the xUnit testing framework to verify the correctness and robustness of various components within the application. Test cases cover scenarios across all layers of the application, ensuring that each component behaves as expected under different conditions. Tests are organized to validate the behavior of individual components, including controllers, services, repositories, and the HTTP client layer.
