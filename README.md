# GroupSpace Demo Project
## Core compoents
 1. Accounts.groups 
 2. API 
 3. Front End Demo - Using Angular

## First, Accounts.groups Module.
In this part, we want to provide the user with some features such as logging in and out, creating a new account, confirming their account, and some other things.
Also, this part helps different applications to obtain the necessary permissions to communicate with the API.

## How does this part give the necessary permissions to other applications in order to access the API in a secure way?
### Here, we talk about  OAuth   OpenID Connect and IdentityServer4
#### OAuth 2.0 :
is the industry-standard protocol for authorization. OAuth 2.0 focuses on client developer simplicity while providing 
specific authorization flows for web applications, desktop applications, mobile phones, and living room devices. 

#### OpenID Connect:
OpenID Connect 1.0 is a simple identity layer on top of the OAuth 2.0 protocol. It allows Clients to verify the identity of the End-User based on the authentication performed by an Authorization Server, as well as to obtain basic profile information about the End-User in an interoperable and REST-like manner.

OpenID Connect allows clients of all types, including Web-based, mobile, and JavaScript clients, to request and receive information about authenticated sessions and end-users. 
The specification suite is extensible, allowing participants to use optional features such as encryption of identity data, 
discovery of OpenID Providers, and session management, when it makes sense for them.

#### IdentityServer4:
IdentityServer is a free, open source OpenID Connect and OAuth 2.0 framework for ASP.NET Core. Founded and maintained by Dominick Baier and Brock Allen, 
IdentityServer4 incorporates all the protocol implementations and extensibility points needed to integrate token-based authentication, single-sign-on 
and API access control in your applications. 
IdentityServer4 is officially certified by the OpenID Foundation and thus spec-compliant and interoperable. 
It is part of the .NET Foundation, and operates under their code of conduct. It is licensed under Apache 2 (an OSI approved license).

## Api.
Various applications, whether they  SPA or mobile apps, communicate with the API to modify, create or delete the Resources.
The API checks the token on each request to ensure that the application communicating with it has the necessary permissions.
### Core  things used to create they API
#### Three Layer Architecture
Each layer of the layered architecture pattern has a specific role and responsibility within the application. 
For example, a presentation layer would be responsible for handling all user interface and browser communication logic, 
whereas a business layer would be responsible for executing specific business rules associated with the request. 
Each layer in the architecture forms an abstraction around the work that needs to be done to satisfy a particular business request.
#### REST API 
in this Api, I follow REST architectural style and A REST API (also known as RESTful API) is an application programming interface (API or web API) that conforms to the constraints of REST architectural style and allows for interaction with RESTful web services. REST stands for representational state transfer and was created by computer scientist Roy Fielding.
#### Communication between Accounts.Groups And Api in a secure way
When the user creates his account, we need to send some of his information like Sub(user identifier) to save in API Database 
so, we need [Account.GroupSpace] to communicate with the API.
I use HttpClient to send the request to the API and I Secure communication between them using JWT. 

#### API Authentication
The API authenticate 'requests' by checking the Bearer Token,
In the API we have two schema.
1. Bearer Authenticate  Schema, Api Used that schema to validate Requests that come from different clients.
2. IdpServerSchema, Api Used that schema to validate Requests that come from IdpServer directly.

#### API Authorization
The API verifies that 'user' have permission to access or manipulate the relevant data.
I use Role-based authorization and Policy-based authorization
##### 1. Role-based authorization:
Role-based authorization checks are declarative???the developer embeds them within their code, against a controller or an action within a controller, specifying roles which the current user must be a member of to access the requested resource.[Authorize(Roles = "Admin")]
##### 2. Policy-based authorization:
These building blocks support the expression of authorization evaluations in code. The result is a richer, reusable, testable authorization structure.
An authorization policy consists of one or more requirements. Register it as part of the authorization service configuration.
 see Authorization Folder for the code.
 
 #### Real Time chat - SignalR
 ASP.NET SignalR is a library for  developers that simplifies the process of adding real-time web functionality to applications.
 SignalR provides a simple API for creating server-to-client remote procedure calls (RPC) that call JavaScript functions in client browsers (and other client platforms) from server-side and vice versa
 
