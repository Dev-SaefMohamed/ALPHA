🌍 Hero EcoTrack
Corporate Carbon Footprint Management SaaS (B2B)

Hero EcoTrack is a full-stack carbon management platform built for Egyptian businesses.
The solution consists of two fully independent applications, with zero shared code, designed to operate on the same database while remaining completely decoupled:

Hero Web Dashboard: An MVC application for manual data entry, analytics, and reporting.
Hero API: A fully separate Web API project designed for B2B integrations and automated data ingestion.

Both applications can be deployed, scaled, and updated independently.

🚀 Key Features
🖥️ Hero Web Dashboard (MVC)

Corporate Identity: Secure registration system that automatically creates a corporate profile.
Modern Dark Interface: Responsive Bootstrap 5 design with a sleek SaaS-style layout.
Analytics Dashboard: Real-time charts using Chart.js (Bar, Doughnut, Line).
Smart Forms: Real-time CO₂ estimation during input.
Reporting: One-click export to PDF and CSV.
User Experience Polish: Clean empty states, consistent alerts, custom error pages, and profile management.

🔌 Hero API (Completely Separate Project)

Standalone Deployment: Fully independent from the MVC project.
No shared Services, no shared Models, no shared Layers.

B2B Integrations: Built for ERP, IoT devices, fleet systems, and corporate automation.
Same Database: Uses the same SQL Server database to maintain data consistency.

Endpoints:
POST /api/CorporateApi/emissions
GET /api/CorporateApi/emissions/{companyId}

Security: JWT-based authentication plus API Key validation.
Production-Ready: Designed to avoid deployment conflicts and allow isolated scaling.

🏗️ Architecture
The solution consists of two completely separate applications, each containing its own Services, Models, Controllers, and Infrastructure.

Important Notes

There is no shared code between the Web and API projects.
Each project contains its own:
Services
Models
Repositories (if used)
DTOs
Business logic
Configuration

This ensures:
No deployment conflicts
No dependency collisions
Independent scaling
Independent release cycles
Simpler DevOps pipelines

🔧 Tech Stack
Framework: .NET 8
Projects: MVC + Web API (fully separate)
Database: SQL Server (shared usage, separate logic)
Authentication: ASP.NET Identity (Web) + JWT (API)
UI: Bootstrap 5 Dark Theme
Documentation: Swagger (API)

⚙️ Installation & Setup

Clone the Repository
git clone https://github.com/Dev-SaefMohamed/ALPHA.git

Configure Database
Update the ConnectionStrings in the appsettings.json of both projects.

Apply Migrations (run once)
Choose either project that contains migrations (usually Hero.Web):

Update-Database

Run Projects Independently

Run Dashboard:

cd Hero.Web
dotnet run

Run API:

cd Hero.API
dotnet run

📖 Usage
Web Dashboard

Register corporate accounts

Submit monthly consumption

Visualize emissions

Export reports

API

Test via Swagger or Postman.

Sample Request:

POST https://localhost:7000/api/CorporateApi/emissions
X-API-KEY: YOUR_KEY
Content-Type: application/json

{
  "corporateCompanyId": 12,
  "year": 2025,
  "month": 11,
  "electricityKWh": 2100,
  "fuelLiters": 350
}

🛡️ Security & Localization
Strict data isolation per company
JWT tokens and API Keys for automation
Emission calculations tailored for Egypt (e.g., 0.55 kgCO₂/kWh)
