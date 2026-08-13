# SkillMatch API

**SkillMatch** is a scalable Backend Web API for a smart job-matching and recruitment platform.
It connects job seekers with employers through skill-based algorithms, resume parsing, and AI-assisted candidate evaluation — built with OOP-first design principles.

---

## Key Features

- **Authentication & Authorization**
  - Custom JWT implementation (Access Token + Refresh Token) without ASP.NET Core Identity.
  - Role-based access control (`Candidate`, `Employer`, `Admin`).
  - Secure password hashing via `PBKDF2` / `BCrypt`.

- **File Upload & Document Processing**
  - Resume/CV uploads (`PDF` / `DOCX`) and profile picture management.
  - File extension, MIME type, and size validation.
  - Automated text and skill extraction from uploaded PDF resumes.

- **AI-Powered Job Matching Engine**
  - Match Score (%) calculation comparing candidate skills against job requirements.
  - AI integration (Google Gemini / OpenAI) for CV summarization and fit analysis.

- **Advanced Search & Filtering**
  - Filter jobs and candidates by skills, location, experience level, and salary range.
  - Built-in Pagination, Sorting, and Dynamic Filtering support.

- **Scalable Architecture**
  - OOP-first design: Encapsulation, Inheritance, Polymorphism, and Abstraction applied throughout.
  - Generic Repository + Unit of Work for decoupled and testable data access.
  - Base entity and base service classes enable adding new features with minimal code changes.

---

## Tech Stack

| Domain                | Technologies Used                                            |
| :-------------------- | :----------------------------------------------------------- |
| **Framework**         | .NET 8 / ASP.NET Core Web API                                |
| **Database**          | Microsoft SQL Server                                         |
| **ORM**               | Entity Framework Core 8                                      |
| **Patterns**          | Repository Pattern, Unit of Work, Generic Repository         |
| **Authentication**    | Custom JWT (JSON Web Tokens) — no ASP.NET Core Identity      |
| **AI Integration**    | HttpClient, Google Gemini API / OpenAI API                   |
| **File Storage**      | Local File Storage / Azure Blob Storage                      |
| **Documentation**     | Swagger / OpenAPI                                            |
| **Utility Libraries** | AutoMapper, FluentValidation, PdfPig (PDF Parsing)           |

---

## OOP Design Principles

The project is built around the four pillars of Object-Oriented Programming:

### Encapsulation

Each layer exposes only what is necessary through interfaces. Internal implementation details (`DbContext`, EF queries, AI HTTP calls) are hidden behind contracts.

```csharp
// The service layer never touches DbContext directly.
// It only interacts through IUnitOfWork.
public class JobService : IJobService
{
    private readonly IUnitOfWork _unitOfWork;
    // DbContext is fully encapsulated inside Infrastructure
}
```

### Abstraction

`IGenericRepository<T>` defines a contract for all data access. `IJobService`, `ICvParserService`, etc., define contracts for all business operations. Controllers depend on abstractions — never on concrete classes.

```csharp
public interface IGenericRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

### Inheritance

A `BaseEntity` class provides common auditing fields. All domain entities inherit from it, ensuring consistency without code duplication. The `GenericRepository<T>` is inherited by specialized repositories that extend its behavior.

```csharp
// BaseEntity.cs — Shared fields for all entities
public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

// Domain entities inherit the base
public class JobPosting : BaseEntity { /* ... */ }
public class CandidateProfile : BaseEntity { /* ... */ }
public class CompanyProfile : BaseEntity { /* ... */ }

// Specialized repository inherits generic behavior
public class JobRepository : GenericRepository<JobPosting>, IJobRepository
{
    public Task<IEnumerable<JobPosting>> GetBySkillsAsync(IEnumerable<int> skillIds) { /* ... */ }
}
```

### Polymorphism

The file storage and AI service integrations are swappable at runtime through dependency injection. The service layer calls `IFileStorageService.SaveAsync()` without knowing whether it is saving locally or to Azure Blob Storage.

```csharp
public interface IFileStorageService
{
    Task<string> SaveAsync(IFormFile file, string folder);
    Task DeleteAsync(string filePath);
}

// Registered at startup — swap without changing any business logic
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
// builder.Services.AddScoped<IFileStorageService, AzureBlobStorageService>();

public interface IAiMatchingService
{
    Task<MatchResultDto> AnalyzeAsync(string cvText, string jobDescription);
}

// Can switch provider without changing service layer
builder.Services.AddScoped<IAiMatchingService, GeminiAiService>();
// builder.Services.AddScoped<IAiMatchingService, OpenAiService>();
```

---

## Project Structure

```
SkillMatch/
│
├── SkillMatch.API/                              # ASP.NET Core Web API Project
│   │
│   ├── Controllers/                             # HTTP Endpoints — thin, no business logic
│   │   ├── AuthController.cs
│   │   ├── JobsController.cs
│   │   ├── ProfilesController.cs
│   │   └── MatchingController.cs
│   │
│   ├── Core/                                    # Domain Models & Contracts
│   │   │
│   │   ├── Entities/                            # Domain Entity Classes
│   │   │   ├── Base/
│   │   │   │   └── BaseEntity.cs                # Abstract base: Id, CreatedAt, UpdatedAt
│   │   │   ├── ApplicationUser.cs               # : BaseEntity
│   │   │   ├── CandidateProfile.cs              # : BaseEntity
│   │   │   ├── CompanyProfile.cs                # : BaseEntity
│   │   │   ├── JobPosting.cs                    # : BaseEntity
│   │   │   ├── Skill.cs                         # : BaseEntity
│   │   │   ├── JobApplication.cs                # : BaseEntity
│   │   │   └── ResumeDocument.cs                # : BaseEntity
│   │   │
│   │   ├── Enums/
│   │   │   ├── ApplicationStatus.cs
│   │   │   ├── ExperienceLevel.cs
│   │   │   └── UserRole.cs
│   │   │
│   │   ├── DTOs/                                # Data Transfer Objects
│   │   │   ├── Auth/
│   │   │   │   ├── RegisterDto.cs
│   │   │   │   ├── LoginDto.cs
│   │   │   │   └── AuthResponseDto.cs
│   │   │   ├── Jobs/
│   │   │   │   ├── CreateJobDto.cs
│   │   │   │   ├── UpdateJobDto.cs
│   │   │   │   └── JobResponseDto.cs
│   │   │   ├── Candidates/
│   │   │   │   ├── CandidateProfileDto.cs
│   │   │   │   └── UpdateProfileDto.cs
│   │   │   └── Matching/
│   │   │       ├── MatchResultDto.cs
│   │   │       └── RecommendedJobDto.cs
│   │   │
│   │   └── Interfaces/                          # All Contracts (Abstraction Layer)
│   │       ├── Repositories/
│   │       │   ├── IEntity.cs                   # Marker interface: Id property
│   │       │   ├── IGenericRepository.cs        # CRUD contract for all entities
│   │       │   ├── ICandidateRepository.cs      # Extends IGenericRepository<CandidateProfile>
│   │       │   ├── IJobRepository.cs            # Extends IGenericRepository<JobPosting>
│   │       │   └── IUnitOfWork.cs               # Aggregates all repositories
│   │       └── Services/
│   │           ├── IAuthService.cs
│   │           ├── IJobService.cs
│   │           ├── ICandidateService.cs
│   │           ├── ICvParserService.cs
│   │           ├── IAiMatchingService.cs
│   │           └── IFileStorageService.cs
│   │
│   ├── Infrastructure/                          # Concrete Implementations
│   │   │
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   └── Migrations/
│   │   │
│   │   ├── Repositories/                        # Repository Implementations (Inheritance)
│   │   │   ├── GenericRepository.cs             # Base implementation of IGenericRepository<T>
│   │   │   ├── CandidateRepository.cs           # : GenericRepository<CandidateProfile>
│   │   │   ├── JobRepository.cs                 # : GenericRepository<JobPosting>
│   │   │   └── UnitOfWork.cs                    # Implements IUnitOfWork
│   │   │
│   │   └── Services/                            # Service Implementations (Polymorphism)
│   │       ├── AuthService.cs                   # Custom JWT — no ASP.NET Core Identity
│   │       ├── JobService.cs
│   │       ├── CandidateService.cs
│   │       ├── LocalFileStorageService.cs       # : IFileStorageService
│   │       ├── AzureBlobStorageService.cs       # : IFileStorageService (swappable)
│   │       ├── PdfCvParserService.cs            # : ICvParserService
│   │       ├── GeminiAiService.cs               # : IAiMatchingService
│   │       └── OpenAiService.cs                 # : IAiMatchingService (swappable)
│   │
│   ├── Helpers/
│   │   ├── MappingProfile.cs                    # AutoMapper Profiles
│   │   ├── ApiResponse.cs                       # Unified API Response Wrapper
│   │   ├── PaginationParams.cs                  # Reusable pagination model
│   │   └── JwtHelper.cs                         # Token generation & validation utilities
│   │
│   ├── Middlewares/
│   │   └── ExceptionHandlingMiddleware.cs       # Global Exception Handler
│   │
│   ├── Program.cs                               # Entry Point & DI Registration
│   └── appsettings.json
│
└── SkillMatch.Tests/                            # Unit & Integration Tests
    ├── Repositories/
    ├── Services/
    └── Controllers/
```

---

## Scalability Design Decisions

### Adding a New Entity

Because of the `BaseEntity` + `GenericRepository<T>` chain, adding a new domain entity requires only:

1. Create `NewEntity.cs` inheriting `BaseEntity`.
2. Create `INewEntityRepository.cs` extending `IGenericRepository<NewEntity>`.
3. Create `NewEntityRepository.cs` inheriting `GenericRepository<NewEntity>`.
4. Register in `IUnitOfWork` — one property, one line.
5. Create the service, DTO, and controller.

No changes to any existing classes are needed.

### Swapping External Services

The `IFileStorageService` and `IAiMatchingService` interfaces decouple the business logic from any vendor. Switching from local storage to Azure Blob — or from Gemini to OpenAI — is a single line change in `Program.cs`.

### Pagination & Filtering

All list endpoints accept a `PaginationParams` object and return a standardized `PagedResult<T>`. Adding filtering to any endpoint does not require changes to the repository pattern itself.

---

## Database Schema

### Entity Relationships

```
[Users] 1────1 [CandidateProfile] 1────* [ResumeDocuments]
   |                  |
   |                  └────* [CandidateSkills] *────1 [Skills]
   |                                                     |
   └──1 [CompanyProfile] 1────* [JobPostings] *──────────┘ (JobSkills)
                                    |
                                    └────* [JobApplications]
```

### Inheritance Chain

```
BaseEntity (abstract)
   ├── ApplicationUser
   ├── CandidateProfile
   ├── CompanyProfile
   ├── JobPosting
   ├── Skill
   ├── JobApplication
   └── ResumeDocument
```

---

## Repository & Unit of Work

```csharp
// IEntity.cs — Marker interface
public interface IEntity
{
    int Id { get; set; }
}

// IGenericRepository.cs — Generic contract
public interface IGenericRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<PagedResult<T>> GetPagedAsync(PaginationParams pagination);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

// IUnitOfWork.cs — Aggregates all repositories
public interface IUnitOfWork : IDisposable
{
    ICandidateRepository Candidates { get; }
    IJobRepository Jobs { get; }
    Task<int> CompleteAsync();
}

// GenericRepository.cs — Base implementation
public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();
}

// JobRepository.cs — Specialized repository (Inheritance)
public class JobRepository : GenericRepository<JobPosting>, IJobRepository
{
    public JobRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<JobPosting>> GetBySkillsAsync(IEnumerable<int> skillIds)
        => await _dbSet
            .Where(j => j.RequiredSkills.Any(s => skillIds.Contains(s.Id)))
            .ToListAsync();
}
```

---

## Custom JWT Authentication

The project uses a custom JWT implementation — no dependency on `Microsoft.AspNetCore.Identity`.

```csharp
// JwtHelper.cs
public class JwtHelper
{
    public string GenerateAccessToken(ApplicationUser user, IList<string> roles);
    public string GenerateRefreshToken();
    public ClaimsPrincipal? ValidateToken(string token);
}

// AuthService.cs
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtHelper _jwtHelper;

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _unitOfWork.Users.FindAsync(u => u.Email == dto.Email);
        // Verify password hash → generate tokens → return response
    }
}
```

---

## Key API Endpoints

### Authentication

| Method | Endpoint               | Description                                  | Access  |
| :----- | :--------------------- | :------------------------------------------- | :------ |
| POST   | `/api/auth/register`   | Register a new Candidate or Employer account | Public  |
| POST   | `/api/auth/login`      | Authenticate and receive JWT access token    | Public  |
| POST   | `/api/auth/refresh`    | Refresh an expired JWT using refresh token   | Public  |
| POST   | `/api/auth/logout`     | Revoke refresh token                         | Auth    |

### Profiles & Resumes

| Method | Endpoint                  | Description                                        | Access    |
| :----- | :------------------------ | :------------------------------------------------- | :-------- |
| GET    | `/api/profiles/me`        | Retrieve current candidate profile and resume info | Candidate |
| PUT    | `/api/profiles/me`        | Update candidate profile information               | Candidate |
| POST   | `/api/profiles/upload-cv` | Upload a CV document (PDF or DOCX)                 | Candidate |

### Jobs & Search

| Method | Endpoint          | Description                                           | Access   |
| :----- | :---------------- | :---------------------------------------------------- | :------- |
| GET    | `/api/jobs`       | Get paginated job listings with filters               | Public   |
| GET    | `/api/jobs/{id}`  | Get details of a specific job posting                 | Public   |
| POST   | `/api/jobs`       | Create a new job posting                              | Employer |
| PUT    | `/api/jobs/{id}`  | Update an existing job posting                        | Employer |
| DELETE | `/api/jobs/{id}`  | Remove a job posting                                  | Employer |

### AI & Matching

| Method | Endpoint                           | Description                                          | Access    |
| :----- | :--------------------------------- | :--------------------------------------------------- | :-------- |
| GET    | `/api/matching/recommended-jobs`   | Fetch AI/skill-based job recommendations             | Candidate |
| POST   | `/api/matching/analyze-cv/{jobId}` | Evaluate candidate CV against a job using AI         | Candidate |

---

## File Upload Security

| Practice             | Implementation                                                          |
| :------------------- | :---------------------------------------------------------------------- |
| Extension Validation | Restrict allowed types strictly to `.pdf`, `.docx`, `.png`, `.jpg`      |
| MIME Type Checking   | Verify MIME type in addition to file extension                          |
| Size Limits          | Enforce a maximum payload size (e.g., 5 MB per file)                   |
| Sanitized Filenames  | Store files using `Guid.NewGuid()` to prevent path traversal attacks    |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, Express, or full instance)
- IDE: Visual Studio 2022 / VS Code / JetBrains Rider

### Setup Steps

**1. Clone the repository**

```bash
git clone https://github.com/your-username/SkillMatch.git
cd SkillMatch
```

**2. Configure app settings**

Update `SkillMatch.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=SkillMatchDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "YOUR_SUPER_SECRET_KEY_HERE_MUST_BE_LONG_ENOUGH",
    "Issuer": "SkillMatchAPI",
    "Audience": "SkillMatchUsers",
    "AccessTokenExpiryMinutes": 60,
    "RefreshTokenExpiryDays": 7
  },
  "AiService": {
    "ApiKey": "YOUR_AI_MODEL_API_KEY",
    "Provider": "Gemini"
  }
}
```

**3. Apply database migrations**

```bash
dotnet ef database update --project SkillMatch.API
```

**4. Run the application**

```bash
dotnet run --project SkillMatch.API
```

**5. Explore API documentation**

```
https://localhost:7123/swagger
```

---

## Contributing

Contributions are welcome. Please fork the repository, create a feature branch, and submit a Pull Request.

---

## License

This project is licensed under the [Mozilla Public License 2.0](LICENSE).