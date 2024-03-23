# CMH Interviews

## Introduction

Welcome to the CMHInterviews. This repository contains the source code for a .NET application built with ASP.NET Core 6.0. The application provides an API endpoint designed to retrieve the total number of interviews scheduled on a specific date within the next 14 days.

## Architecture Overview

The solution architecture is structured around the following layers:

### Controller Layer: 
The controller layer consists of classes responsible for handling incoming HTTP requests and returning appropriate responses. Controllers delegate the processing of requests to service classes and orchestrate the flow of data between the client and the application.

### Service Layer: 
The service layer encapsulates the application's business logic. It includes services such as InterviewService, which orchestrates interactions between the controller and the repository layer.

### Repository Layer: 
The repository layer is responsible for interacting with external data sources, such as APIs or databases. The InterviewRepository class communicates with the external API to fetch interview data.

### HTTP Client Layer: 
This layer contains the HttpClientService, which handles HTTP requests and responses. It is used by the repository layer to communicate with the external API.

### The solution is organized into two separate projects:

### Source Code Project: 
This project contains the source code for the CMHInterviews application. It includes the implementation of controllers, services, repositories, HTTP client logic, and other components necessary for the application to function. The source code project is structured according to the layered architecture described above.

### Test Case Project: 
The test case project is dedicated to unit testing the functionality of the CMHInterviews application. It contains test cases implemented using the xUnit testing framework to verify the correctness and robustness of various components within the application. Test cases cover scenarios across all layers of the application, ensuring that each component behaves as expected under different conditions. Tests are organized to validate the behavior of individual components, including controllers, services, repositories, and the HTTP client layer.

## Functionality
The main functionality of the application is to provide an API endpoint that accepts a date and returns the total number of scheduled interviews for that date within the next 14 days. The application retrieves interview data from an external API and processes it to determine the number of scheduled interviews.

## Dockerization
The solution includes a Dockerfile to containerize the application. The Dockerfile specifies the steps to build and run the application within a Docker container, ensuring consistency and portability across different environments.

## Testing
The solution includes unit tests implemented with xUnit.net. Test cases cover various scenarios, including successful retrieval of interview data, handling of null dates, and error conditions. The tests ensure that the application behaves as expected under different circumstances and that errors are appropriately handled.

Feel free to explore the source code and documentation to learn more about how the application works and how to run it locally or in a containerized environment.
