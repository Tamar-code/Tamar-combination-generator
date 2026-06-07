# Combination Generator - Full Stack Project

A full-stack application for generating and navigating permutations.

## 📁 Project Structure

```
├── CombinationGenerator-main/     # Angular 19 Client
└── CombinationGeneratorAPI-main/  # .NET 9 API
```

## 🚀 Quick Start

### Prerequisites
- Node.js 20.19+ or 22+
- .NET 9 SDK

### Run API Server
```bash
cd CombinationGeneratorAPI-main/CombinationGeneratorAPI.Api
dotnet run
```
API runs on: `http://localhost:5069`

### Run Angular Client
```bash
cd CombinationGenerator-main
npm install
npm start
```
Client runs on: `http://localhost:4200`

## 🔧 Features

### Client (Angular 19)
- Modern Angular with Signals
- Route Guards for navigation control
- Pagination component
- Error handling with user-friendly messages

### API (.NET 9)
- Minimal API with clean architecture
- In-memory caching for session state
- Efficient permutation generation algorithm

## 📡 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/start` | Start new calculation with `{ n: number }` |
| GET | `/api/next` | Get next permutation |
| GET | `/api/all` | Get paginated list of permutations |

## 👩‍💻 Author
Tamar - Home Assignment Project
