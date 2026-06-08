# Combination Generator - Full Stack Project

A full-stack application for generating and navigating permutations efficiently.

## 📁 Project Structure

```
Tamar-combination-generator/
├── CombinationGenerator/          # Angular 20 Client
└── CombinationGeneratorAPI/       # .NET 9 API (.NET minimal API)
```

## 🚀 Getting Started

### Prerequisites
- **Node.js** 20.19+ or 22+ (for Angular)
- **.NET 9 SDK** (for C# API)
- **npm** (comes with Node.js)

### Step 1: Clone & Navigate
```bash
git clone <repo-url>
cd Tamar-combination-generator
```

### Step 2: Start the API Server (.NET 9)

Open a terminal and run:
```bash
cd CombinationGeneratorAPI/CombinationGeneratorAPI.Api
dotnet run
```

**API Server Details:**
- **Base URL**: `http://localhost:5069`
- **Health Check**: Navigate to `http://localhost:5069/swagger` to view OpenAPI documentation
- The API will automatically bind to port 5069 (configured in `launchSettings.json`)

### Step 3: Start the Angular Client (in a new terminal)

```bash
cd CombinationGenerator
npm install          # Install dependencies (first time only)
npm start            # Development server
```

**Client Details:**
- **Base URL**: `http://localhost:4200`
- **Browser**: Automatically opens at http://localhost:4200
- The Angular client is configured to proxy API requests to `http://localhost:5069` (see `proxy.conf.json`)

## 🔗 How They Communicate

```
Angular Client (http://localhost:4200)
        ↓
  [proxy.conf.json routes /api to localhost:5069]
        ↓
  .NET API (http://localhost:5069)
```

**Important**: Make sure both servers are running for the application to work.

## 🔧 Features

### Client (Angular 20)
- Modern Angular with **Signals** for reactive state management
- Route guards for secure navigation between components
- Pagination component for browsing large permutation sets
- RTL (Hebrew) support
- User-friendly error handling and loading states
- Session-based state management using `sessionId`

### API (.NET 9)
- **Minimal API** with clean, modular architecture
- **In-memory caching** for session management (1-hour expiration)
- **Efficient permutation algorithm** using index-based calculation
- **Automatic OpenAPI documentation** (Swagger)
- **Input validation** for all parameters
- **CORS support** for secure cross-origin requests
- **MediatR** for query handling (extensible architecture)

## 📡 API Endpoints

| Method | Endpoint | Description | Parameters |
|--------|----------|-------------|------------|
| **POST** | `/api/start` | Initialize calculation session | `n`: number (1-20) |
| **GET** | `/api/next` | Get next permutation in sequence | `sessionId`: string |
| **GET** | `/api/all` | Get paginated permutations | `sessionId`: string, `pageSize`: int, `fromIndex`: long, `page`: int |

### Example Workflow

1. **Start Calculation:**
   ```bash
   POST http://localhost:5069/api/start
   Body: { "n": 3 }
   Response: { "totalCount": "6", "sessionId": "abc123..." }
   ```

2. **Get Next Permutation:**
   ```bash
   GET http://localhost:5069/api/next?sessionId=abc123...
   Response: { "permutation": [1,2,3], "index": "1", "hasMore": true }
   ```

3. **Get All (Paginated):**
   ```bash
   GET http://localhost:5069/api/all?sessionId=abc123...&pageSize=10&fromIndex=0&page=1
   Response: { "items": [...], "hasMore": true, "totalPages": 1 }
   ```

## 🛠️ Troubleshooting

### API Not Responding
- Verify .NET 9 SDK is installed: `dotnet --version`
- Check port 5069 is not in use: `lsof -i :5069` (Mac/Linux) or `netstat -ano | findstr :5069` (Windows)
- Check API logs in terminal for errors

### Client Can't Connect to API
- Verify `http://localhost:5069` is accessible
- Check `CombinationGenerator/proxy.conf.json` has correct API URL
- Ensure both servers are running

### Large n Values Slow
- Permutation generation for n=20 is mathematically complex (20! = 2.4 trillion)
- First load may take a moment; subsequent requests use efficient index-based computation
- Pagination limits results displayed per page

## 🏗️ Architecture Notes

### Separation of Concerns
- **API**: Clean separation using MediatR queries, DTOs, and service interfaces
- **Client**: Component-based architecture with signals-based state management

### Session Management
- Each user gets a unique `sessionId` (GUID)
- Session state is cached in-memory for 1 hour
- Supports concurrent users without state conflicts

### Algorithm Choice
- **Index-based computation**: Computes only required permutation at a given index
- **Avoids memory overhead**: Doesn't pre-compute or store all permutations
- **Efficient for large n**: Handles n=20 without memory exhaustion

## 📝 Development Notes

- Large number handling: Uses `long` for permutation counts (handles 20! correctly)
- Validation: Both client and API validate input (n must be 1-20, pageSize > 0)
- Error handling: Comprehensive error messages in both layers
- Language: Application is fully localized for Hebrew (RTL UI)

## 👩‍💻 Project Info
**Home Assignment for Development Position** | Tamar  
See project requirements in original assignment documentation.

