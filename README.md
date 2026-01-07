# 📊 Analytics Service - JSONPlaceholder Integration

A backend analytics service built with **ASP.NET Core** that integrates with the public **JSONPlaceholder API** to aggregate, analyze, and expose user-related insights through clean RESTful endpoints.

This project demonstrates backend fundamentals such as async programming, external API integration, data aggregation, clean architecture, and performance-aware design.

---

## ✨ Features

### 🌍 Global Analytics
Aggregated statistics across the entire system:
- Total number of users
- Total number of posts
- Total number of comments
- Total number of todos
- Most active user (by post count)

---

### 👤 User Activity Levels
Classifies each user based on activity:
- **Low** – 0–2 posts
- **Medium** – 3–5 posts
- **High** – 6+ posts

Includes users with zero posts.

---

### 🏆 Top Active Users
Returns a leaderboard of the most active users:
- Sorted by post count (descending)
- Configurable result limit
- Includes activity level classification

---

### 📝 User Task Report (Users & Todos Analytics)
Analyzes task completion statistics for selected users based on Todos data:
- Includes only users with emails ending in **.org** or **.net**
- Calculates total tasks per user
- Calculates completion percentage
- Safely handles users with zero tasks

---

## 🛠 Tech Stack

- C# / .NET 8
- ASP.NET Core Web API
- External API: JSONPlaceholder

---

## 🧱 Architecture Overview

The project follows a clean, layered architecture:

- **Controllers**  
  Handle HTTP requests and responses only.

- **Services**  
  Contain business logic and analytics calculations.

- **Clients**  
  Responsible for communication with the external API.

- **DTOs & Enums**  
  Used as clear data contracts between layers.

---

## 🔌 API Endpoints

### Global Analytics
GET /api/analytic/global

Response example:
{
  "userCount": 10,
  "postCount": 100,
  "commentsCount": 500,
  "mostActiveUser": {
    "id": 1,
    "name": "Leanne Graham"
  }
}

---

### User Activity Levels
GET /api/analytic/users/activity

Response example:
[
  {
    "userId": 1,
    "userName": "Leanne Graham",
    "postsCount": 10,
    "activityLevel": "High"
  },
  {
    "userId": 2,
    "userName": "Ervin Howell",
    "postsCount": 2,
    "activityLevel": "Low"
  }
]

---

### Top Active Users
GET /api/analytic/users/top?limit=3

Query parameters:
- limit (optional)

Response example:
[
  {
    "userId": 1,
    "userName": "Leanne Graham",
    "postsCount": 10,
    "activityLevel": "High"
  }
]

---

### User Task Report
GET /api/analytic/users/tasks-report

Response example:
[
  {
    "name": "Leanne Graham",
    "email": "leanne.org",
    "totalTasks": 20,
    "completionPercentage": 75.0
  },
  {
    "name": "Ervin Howell",
    "email": "ervin.net",
    "totalTasks": 10,
    "completionPercentage": 40.0
  }
]

---

## ⚙️ Key Implementation Details

- All external API calls are fully asynchronous
- No network calls inside loops (avoids N+1 issues)
- Uses GroupBy and Dictionary for efficient aggregation
- Enums are serialized as strings for readable API responses
- Division by zero is handled safely
- Includes users with zero related records

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Internet connection (for JSONPlaceholder API)

### Run the project
dotnet restore
dotnet run

Swagger UI:
https://localhost:<port>/swagger

---

