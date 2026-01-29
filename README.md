# 🌍 Travel Buddy

A modern, full-stack travel planning application that helps users organize their trips, manage itineraries, and track budgets all in one place.

## ✨ Features

- **Trip Planning**: Create and organize travel plans with detailed information
- **Itinerary Management**: Plan daily activities and schedules
- **Budget Tracking**: Set and monitor your travel budget across different categories
- **Travel Preferences**: Customize trips based on travel style, companions, and interests
- **Modern UI**: Clean, responsive design with smooth animations and intuitive navigation

## 🛠️ Tech Stack

### Frontend
- **Angular 21** - Modern web framework
- **PrimeNG** - UI component library
- **Tailwind CSS** - Utility-first CSS framework
- **TypeScript** - Type-safe JavaScript
- **RxJS** - Reactive programming

### Backend
- **.NET 10.0** - Web API framework
- **Entity Framework Core** - ORM for database operations
- **SQLite** - Lightweight database
- **C#** - Backend programming language

## 📁 Project Structure

```
Travel-Buddy/
├── frontend/
│   └── TravelBuddy/
│       ├── src/
│       │   ├── app/
│       │   │   ├── components/
│       │   │   │   └── navbar/          # Navigation component
│       │   │   └── modules/
│       │   │       ├── home/            # Home page
│       │   │       ├── new-trip/        # Trip creation page
│       │   │       └── all-trips/       # Trips list page
│       │   └── styles.css               # Global styles
│       ├── angular.json
│       └── package.json
│
└── backend/
    └── TravelBuddy/
        ├── Controllers/                  # API controllers
        ├── Program.cs                    # Application entry point
        ├── TravelBuddy.csproj
        └── appsettings.json

```

## 🚀 Getting Started

### Prerequisites

- **Node.js** (v20 or higher)
- **npm** (v10 or higher)
- **.NET 10.0 SDK**

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/heaji-lee/Travel-Buddy.git
   cd Travel-Buddy
   ```

2. **Setup Frontend**
   ```bash
   cd frontend/TravelBuddy
   npm install
   ```

3. **Setup Backend**
   ```bash
   cd backend/TravelBuddy
   dotnet restore
   ```

### Running the Application

#### Frontend (Angular)
```bash
cd frontend/TravelBuddy
npm start
```
The frontend will run on `http://localhost:4200`

#### Backend (.NET API)
```bash
cd backend/TravelBuddy
dotnet run
```
The API will run on `https://localhost:5001` (or the port specified in launchSettings.json)

## 📦 Available Scripts

### Frontend
- `npm start` - Start development server
- `npm run build` - Build for production
- `npm test` - Run unit tests
- `npm run watch` - Build with file watching

### Backend
- `dotnet run` - Start the API server
- `dotnet build` - Build the project
- `dotnet test` - Run tests

## 🎨 Key Features Detail

### Trip Creation
Create comprehensive trip plans with:
- Trip name and destination
- Start and end dates
- Travel companions selection
- Travel style preferences (Budget, Luxury, Adventure, Relaxation)
- Interest categories (Culture, Nature, Food, History, etc.)

### Budget Management
- Set total trip budget
- Automatic allocation across categories:
  - ✈️ Transportation
  - 🏨 Accommodation
  - 🍽️ Food & Dining
  - 🎭 Activities
  - 🛍️ Shopping
  - 💵 Miscellaneous

### Modern UI/UX
- Smooth animations and transitions
- Responsive design for all screen sizes
- Intuitive step-by-step form with accordion layout
- Hover effects and visual feedback
- Clean, aesthetic color scheme

## 🔧 Configuration

### Frontend Configuration
- Tailwind CSS configuration in `tailwind.config.js`
- Angular configuration in `angular.json`
- Prettier settings in `package.json`

### Backend Configuration
- API settings in `appsettings.json`
- Database connection strings
- CORS policies

## 🔜 Next Steps (In Progress)

- Build backend API controllers
- Connect frontend forms to the backend
- Implement persistent database (EF Core + PostgreSQL)
- Make the app fully responsive for mobile devices
- Add user authentication and profiles
- Integrate Google Maps API to show destinations and activities
- Integrate Weather API to display live weather for trip locations
- Improve UI/UX animations