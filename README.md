# Lab2 - ASP.NET Core Web API

## Project Overview

This is a simple ASP.NET Core 8.0 **Web API** project that demonstrates:

- Creating an empty .NET project
- Building 2 data models (Student & Course)
- Implementing service layer with dependency injection
- Creating RESTful API controllers with 3 endpoints each
- In-memory data storage using static lists

---

## Part 1: Creating Your First Empty .NET Project

### Step 1: Create the Project

```bash
dotnet new web -n Lab2
cd Lab2
```

### Step 2: Verify the Project Structure

```
Lab2/
├── Lab2.csproj          # Project file
├── Program.cs           # Application startup configuration
├── appsettings.json     # Configuration
└── .gitignore
```

### Step 3: Run the Project

```bash
dotnet run
```

---

## Part 2: Project Structure

The project is organized into logical folders:

```
Lab2/
├── Controllers/
│   ├── HomeController.cs
│   ├── StudentController.cs
│   └── CourseController.cs
├── Models/
│   ├── Student.cs
│   ├── Course.cs
│   └── ErrorViewModel.cs
├── Interfaces/
│   ├── IStudentService.cs
│   └── ICourseService.cs
├── Services/
│   ├── StudentService.cs
│   └── CourseService.cs
├── Program.cs
└── appsettings.json
```

---

## Part 2a: Models

### Student Model

**File:** `Models/Student.cs`

```csharp
namespace Lab2.Models;

public class Student
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Level { get; set; }
}
```

**Properties:**

- `Id`: Auto-assigned when created
- `Name`: Student's name (required)
- `Level`: Academic level (1-4)

### Course Model

**File:** `Models/Course.cs`

```csharp
namespace Lab2.Models;

public class Course
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int CreditHours { get; set; }
}
```

**Properties:**

- `Id`: Auto-assigned when created
- `Title`: Course name (required)
- `CreditHours`: Credit hours per course

---

## Part 2b: Interfaces (Contracts)

### IStudentService

**File:** `Interfaces/IStudentService.cs`

```csharp
using Lab2.Models;

namespace Lab2.Interfaces;

public interface IStudentService
{
    IEnumerable<Student> GetAllStudents();
    Student? GetStudentById(int id);
    Student AddStudent(Student student);
}
```

### ICourseService

**File:** `Interfaces/ICourseService.cs`

```csharp
using Lab2.Models;

namespace Lab2.Interfaces;

public interface ICourseService
{
    IEnumerable<Course> GetAllCourses();
    Course? GetCourseById(int id);
    Course AddCourse(Course course);
}
```

---

## Part 2c: Services (Business Logic)

### StudentService

**File:** `Services/StudentService.cs`

```csharp
public class StudentService : IStudentService
{
    // Static list persists data across requests
    private static readonly List<Student> students = new()
    {
        new Student { Id = 1, Name = "Mohamed", Level = 4 },
        new Student { Id = 2, Name = "Shahd", Level = 3 }
    };

    private static int nextId = 3;

    public IEnumerable<Student> GetAllStudents() => students;
    public Student? GetStudentById(int id) => students.FirstOrDefault(s => s.Id == id);

    public Student AddStudent(Student student)
    {
        student.Id = nextId++;
        students.Add(student);
        return student;
    }
}
```

### CourseService

**File:** `Services/CourseService.cs`

- Same pattern as StudentService
- Manages a static list of courses
- Implements `ICourseService` interface

---

## Part 3: Controllers with 3 Endpoints Each

### HomeController (1 endpoint)

**File:** `Controllers/HomeController.cs`

| Endpoint | Method | Description                    |
| -------- | ------ | ------------------------------ |
| `/`      | GET    | Welcome message with API guide |

**Example Response:**

```json
{
  "message": "Hello! Welcome to the Lab2 API",
  "endpoints": {
    "students": "Go to /students to view all students",
    "courses": "Go to /courses to view all courses"
  }
}
```

---

### StudentController (3 endpoints)

**File:** `Controllers/StudentController.cs`

| Endpoint         | Method | Description                  |
| ---------------- | ------ | ---------------------------- |
| `/students`      | GET    | Get all students             |
| `/students/{id}` | GET    | Get a specific student by ID |
| `/students`      | POST   | Create a new student         |

#### Examples:

**1. GET /students** - List all students

```bash
curl http://localhost:5166/students
```

**2. GET /students/1** - Get student with ID 1

```bash
curl http://localhost:5166/students/1
```

**3. POST /students** - Add a new student

```bash
curl -X POST http://localhost:5166/students \
  -H "Content-Type: application/json" \
  -d '{"name": "Ali", "level": 2}'
```

---

### CourseController (3 endpoints)

**File:** `Controllers/CourseController.cs`

| Endpoint        | Method | Description                 |
| --------------- | ------ | --------------------------- |
| `/courses`      | GET    | Get all courses             |
| `/courses/{id}` | GET    | Get a specific course by ID |
| `/courses`      | POST   | Create a new course         |

#### Examples:

**1. GET /courses** - List all courses

```bash
curl http://localhost:5166/courses
```

**2. GET /courses/1** - Get course with ID 1

```bash
curl http://localhost:5166/courses/1
```

**3. POST /courses** - Add a new course

```bash
curl -X POST http://localhost:5166/courses \
  -H "Content-Type: application/json" \
  -d '{"title": "Database Systems", "creditHours": 4}'
```

---

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK or later
- A terminal/PowerShell

### Clone and Run

```bash
# Clone the repository
git clone https://github.com/yourusername/Lab2.git
cd Lab2

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

The API will start on: **http://localhost:5166**

---

## Testing the API

### Option 1: PowerShell

```powershell
# Get all students
Invoke-WebRequest http://localhost:5166/students -UseBasicParsing | Select-Object -ExpandProperty Content

# Add a student
$body = @{ name = "Ali"; level = 2 } | ConvertTo-Json
Invoke-WebRequest http://localhost:5166/students -Method Post -ContentType "application/json" -Body $body -UseBasicParsing
```

### Option 2: cURL

```bash
# Get all students
curl http://localhost:5166/students

# Add a student
curl -X POST http://localhost:5166/students \
  -H "Content-Type: application/json" \
  -d '{"name": "Ali", "level": 2}'
```

### Option 3: Postman

1. Download [Postman](https://www.postman.com/)
2. Create new request
3. Set to **POST** → URL: `http://localhost:5166/students`
4. Body (JSON): `{"name": "Ali", "level": 2}`
5. Click **Send**

---

## GitHub Submission

### Steps to Submit on GitHub

1. **Create a GitHub Repository**
   - Go to [github.com/new](https://github.com/new)
   - Name it `Lab2`
   - Choose "Add a README file"
   - Create repository

2. **Initialize Git Locally**

   ```bash
   cd Lab2
   git init
   git add .
   git commit -m "Initial commit: ASP.NET Core Web API with Students and Courses"
   ```

3. **Connect to Remote Repository**

   ```bash
   git remote add origin https://github.com/yourusername/Lab2.git
   git branch -M main
   git push -u origin main
   ```

4. **Verify on GitHub**
   - Visit your repository URL: `https://github.com/yourusername/Lab2`
   - You should see all files including this README

---

## Key Concepts Explained

### 1. **Dependency Injection**

- Services are registered in `Program.cs`
- Controllers receive services via constructor
- Makes code testable and loosely coupled

### 2. **Static Collections**

- Data persists across HTTP requests
- Resets when application restarts
- For production, use a Database (SQL Server, PostgreSQL)

### 3. **RESTful Endpoints**

- GET: Retrieve data
- POST: Create new data
- [id]: Path parameter for specific resource

### 4. **Scoped vs Singleton** (in Program.cs)

- `AddScoped<IStudentService, StudentService>()` → New instance per request
- `AddSingleton<T>()` → Single instance for entire app lifecycle

---

## What You've Learned

✅ Creating an empty ASP.NET Core project  
✅ Structuring projects with Models, Services, Controllers  
✅ Using Interfaces for abstraction  
✅ Building RESTful APIs  
✅ Dependency Injection  
✅ Testing APIs with different tools  
✅ Submitting code to GitHub

---

## Summary

This project demonstrates a complete ASP.NET Core Web API with:

- **2 Models**: Student & Course
- **2 Interfaces**: IStudentService & ICourseService
- **2 Services**: StudentService & CourseService
- **3 Controllers**: Home (1 endpoint), Student (3 endpoints), Course (3 endpoints)
- **7 Total Endpoints**: All tested and working

**GitHub Link:** [Your Repository Here]
