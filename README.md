# RFMoneyMatters

An interactive, gamified financial-education platform for teens (12–17), built with a .NET 8 Web API backend, Next.js frontend, and PostgreSQL database.

---

## Table of Contents

- [Project Overview](#project-overview)  
- [Tech Stack](#tech-stack)  
- [Prerequisites](#prerequisites)  
- [Environment Variables](#environment-variables)  
- [Backend Setup](#backend-setup)  
- [Database Migrations](#database-migrations)  
- [Frontend Setup](#frontend-setup)  
- [Running the Application](#running-the-application)  

---

## Project Overview

RFMoneyMatters delivers:
- **Lessons & quizzes** on core financial topics.  
- **Challenges & rewards** to motivate good money habits (streaks, coins).  
- **Expense tracking & goal-setting** modules.  
- **AI-powered chat assistant** for on-demand budgeting and quiz hints.  

This repo contains:  
1. **Backend**: ASP .NET Core Web API 
2. **Frontend**: Next.js app under `frontend/money-matters-frontend`  
3. **Database**: PostgreSQL schema with EF Core migrations  

---

## Tech Stack

- **Backend**: .NET 8, ASP .NET Core, Entity Framework Core, JWT Auth  
- **Frontend**: Next.js (React), Tailwind CSS, Framer Motion, Axios  
- **Database**: PostgreSQL (AWS RDS)  
- **DevOps**: GitHub Actions CI, CORS policy for local dev  

---

## Prerequisites

- **.NET 8 SDK**  
- **Node.js** v16+  
- **npm** or **yarn**  
- **PostgreSQL** (local or remote)  
- Optional: Docker for local Postgres  

---

## Environment Variables

Create a `.env` (frontend) and `appsettings.json` (or better, `appsettings.Development.json`) for the backend.

### Backend: `appsettings.json`
```json
{
  "Logging": {
    "LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=<DB_HOST>;Port=5432;Database=rf-money;Username=<USER>;Password=<PASS>;SSL Mode=Require;Trust Server Certificate=true"
  },
  "TokenKey": "<YOUR_LONG_RANDOM_KEY>"
}
```

DefaultConnection: your Postgres URI
TokenKey: used to sign JWT tokens (keep secret!)

Frontend: frontend/.env.local
NEXT_PUBLIC_API_URL=http://localhost:5116/api

## Backend Setup
1. Clone repo
  git clone https://github.com/your-org/RFMoneyMatters.git
  cd RFMoneyMatters

2. Configure appsettings.json or use User Secrets

3. Restore & build
  cd RFMoneyMatters
  dotnet restore
  dotnet build

## Database Migrations
  Migrations live in the /Migrations folder.
  
  Add migration (if you change the model):
  dotnet ef migrations add <YourMigrationName>
  
  Apply to database:
  dotnet ef database update

## Frontend Setup
  1. Navigate to the frontend folder
  cd frontend/money-matters-frontend

  2. Install dependencies
  npm install or yarn

  3. Install Redux
  npm install @reduxjs/toolkit react-redux
  or
  yarn add @reduxjs/toolkit react-redux

## Running the Application

  1. Start Backend
  From the RFMoneyMatters folder:
  dotnet run

  By default it listens on https://localhost:5116/

  2. Start Frontend
  From frontend/money-matters-frontend:
  npm run dev

  Open http://localhost:3000 in your browser.

#### You should now be able to register, login, take quizzes, track expenses, and chat with the AI assistant.
