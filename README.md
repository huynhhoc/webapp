# Simple eCommerce Website README

Welcome to our simple eCommerce website! This website is built using the Model-View-Controller (MVC) architecture, making it easy to manage and organize different aspects of the application. Below, we'll provide an overview of the website's structure, database setup, and instructions on how to get started.


# Introduction

Our simple eCommerce website is designed to showcase the basic functionalities of an online store. Users can browse through a list of products on the Home page and, if granted administrative privileges, manage (Create, Read, Update, Delete) products through the Admin page.
Features

    Home Page: Displays a list of available products for users to browse.
    Admin Page: Enables authorized users to perform CRUD operations on products (Create, Read, Update, Delete).

# Database Setup

Our website utilizes a database to store and manage product information. To set up the database, we use Entity Framework's migration system. Follow the steps below to migrate the database:

    Open the Package Manager Console:

    mathematica

Tools > NuGet Package Manager > Package Manager Console

Run the following command to scaffold the initial migration:

```

Add-Migration InitialCreate

```

Apply the migration to create the database:

```
    Update-Database
```
These commands will create the necessary tables and schema for the product data in the database.
Running the Application

To run the eCommerce website locally, follow these steps:

    Clone this repository to your local machine.

    Open the project in your preferred Integrated Development Environment (IDE) that supports .NET development.

    Build the solution to restore NuGet packages and compile the code.

    Set the startup project to the main web application.

    Run the application by pressing F5 or using the "Start Debugging" option.

    Access the website through your preferred web browser at: http://localhost:<port>/ (replace <port> with the port number shown in your IDE).

# Contributing

We welcome contributions to improve and expand the functionality of our simple eCommerce website. If you'd like to contribute, please follow these steps:

    Fork the repository on GitHub.

    Create a new branch from the main branch for your changes.

    Make your changes and commit them to your branch.

    Push your branch to your forked repository.

    Open a pull request to the main branch of our repository, describing the changes you've made.

    We'll review your pull request and collaborate with you if any changes or improvements are needed.

# Support

If you encounter any issues or have questions regarding the website, please feel free to contact our support team. We're here to help and ensure a smooth experience for everyone using our eCommerce website.

Thank you for using our simple eCommerce website! We hope you find it useful and easy to navigate. Happy shopping!