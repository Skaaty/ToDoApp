# 📝 ToDoApp - WebApi 

![License](https://img.shields.io/badge/license-MIT-blue)

A fully-featured **task-tracking REST API** written in **ASP.NET Core** with Entity Framework Core, AutoMapper and JWT authentication (access- & refresh-tokens).  
Easily clone, run & extend the code for your own projects.

---

## ✨ Features

| Area | What you get |
|------|--------------|
| **Domain** | Task lists → Task items → Tags, plus optional notifications |
| **Auth**  | Register / log in, short-lived **access-tokens** (15 min) |
| **Security** | BCrypt password hash, HMAC-SHA256 JWTs, per-user data filtering |
| **API docs** | Live **Swagger UI** + OpenAPI 3 schema |
| **Persistence** | EF Core 7, SQLite by default (swap for SQL Server/MySQL/Postgres easily) |
| **Mapping** | AutoMapper profiles for every DTO → Entity direction |
| **Tests** | Example xUnit integration test shows how to spin up the API in-memory |
| **Dev UX** | `appsettings.Development.json` with a placeholder JWT key – `dotnet run` works out-of-the-box |

---

## 🏗️ Tech Stack

* **ASP.NET Core** – Minimal-hosting model  
* **Entity Framework Core** – code-first migrations  
* **AutoMapper** – DTO ↔︎ Entity mappings  
* **System.IdentityModel.Tokens.Jwt** – JWT handling  
* **Swashbuckle.AspNetCore** – Swagger UI / OpenAPI  

---

## 🚀 Quick start (local dev)

> Prerequisites: **.NET SDK** & **SQLite** CLI (or change the provider).

```bash
git clone https://github.com/Skaaty/ToDoApp.git
cd ToDoApp/TodoApi

# 👇 1. install EF tools (once)
dotnet tool install --global dotnet-ef

# 👇 2. create dev DB & tables
dotnet ef database update

# 👇 3. run – automatically uses appsettings.Development.json
dotnet run