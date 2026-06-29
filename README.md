# NewsHub

A modern cross-platform news application built with .NET MAUI, providing users with access to news from various sources around the world.

![.NET](https://img.shields.io/badge/.NET-9.0-purple)
![MAUI](https://img.shields.io/badge/MAUI-Latest-blue)
![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20iOS%20%7C%20Windows%20%7C%20MacCatalyst-lightgrey)
![License](https://img.shields.io/badge/License-MIT-green)

## Table of Contents

- [Features](#features)
- [Screenshots](#screenshots)
- [Requirements](#requirements)
- [Installation](#installation)
- [Setup](#setup)
- [Running](#running)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)

## Features

- **User Authentication**: Secure login and registration using Firebase Authentication
- **Multiple News Sources**: Access to over 100 global news sources
- **Advanced Search**: Search news articles by keywords and topics
- **Category Browsing**: Browse news by categories including general, technology, sports, health, and more
- **User Profile**: Manage personal data and preferences
- **Modern Interface**: Clean and intuitive user interface design
- **Multi-language Support**: Available in Arabic and English
- **Article Bookmarking**: Save favorite articles for offline reading
- **Automatic Updates**: Periodic news content refresh

## Screenshots

Screenshots will be added soon.

## Requirements

### Required Software

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) (with .NET MAUI workload)
- or [Visual Studio Code](https://code.visualstudio.com/) with C# and .NET MAUI extensions

### Platform Requirements

#### Android
- Android SDK (API 21+)
- Android Emulator or physical device

#### iOS
- macOS with Xcode
- iOS Simulator or physical iOS device

#### Windows
- Windows 10 (version 17763+) or Windows 11

#### MacCatalyst
- macOS 11+ (Big Sur)

## Installation

1. Clone the repository

```bash
git clone https://github.com/yourusername/NewsHub.git
cd NewsHub
```

2. Restore NuGet packages

```bash
dotnet restore
```

3. Build the solution

```bash
dotnet build
```

## Configuration

### Firebase Configuration

1. Create a new project in the [Firebase Console](https://console.firebase.google.com/)
2. Enable Authentication with Email/Password provider
3. Enable Realtime Database
4. Obtain the following credentials:
   - API Key
   - Auth Domain
   - Database URL

### NewsAPI Configuration

1. Register an account at [NewsAPI](https://newsapi.org/)
2. Obtain your API Key from the dashboard

### Application Settings

Copy the example configuration file to create your development settings:

```bash
cp NewsApp/appsettings.example.json NewsApp/appsettings.Development.json
```

Edit `appsettings.Development.json` and add your API credentials:

```json
{
  "Firebase": {
    "ApiKey": "your_firebase_api_key_here",
    "AuthDomain": "your_firebase_auth_domain_here",
    "DatabaseUrl": "your_firebase_database_url_here"
  },
  "NewsAPI": {
    "ApiKey": "your_newsapi_api_key_here",
    "ApiKey2": "your_newsapi_api_key_2_here"
  }
}
```

**Security Note**: The `appsettings.Development.json` file is included in `.gitignore` and will not be committed to the repository. Never share your API keys or commit them to version control.

## Running the Application

### Using Visual Studio

1. Open the solution file `NewasApp.sln`
2. Select the target platform from the toolbar (Android, iOS, Windows, or MacCatalyst)
3. Press F5 or click the "Start Debugging" button

### Using Command Line

#### Android
```bash
dotnet build -t:Run -f net9.0-android
```

#### Windows
```bash
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```

#### iOS (requires macOS)
```bash
dotnet build -t:Run -f net9.0-ios
```

#### MacCatalyst (requires macOS)
```bash
dotnet build -t:Run -f net9.0-maccatalyst
```

## Project Structure

```
NewsHub/
├── NewsApp/
│   ├── Config/              # Configuration classes
│   │   ├── FirebaseConfig.cs
│   │   └── NewsAPIConfig.cs
│   ├── Resources/           # Application resources
│   │   ├── Images/
│   │   ├── Fonts/
│   │   ├── Styles/
│   │   └── AppIcon/
│   ├── Views/              # User interface pages
│   │   ├── MainPage.xaml
│   │   ├── Signin.xaml
│   │   ├── Signup.xaml
│   │   ├── Explore.xaml
│   │   ├── ArticalDetails.xaml
│   │   └── ...
│   ├── Platforms/           # Platform-specific configurations
│   │   ├── Android/
│   │   ├── iOS/
│   │   ├── Windows/
│   │   └── MacCatalyst/
│   ├── Utils/              # Utility classes
│   ├── App.xaml            # Application entry point
│   ├── AppShell.xaml       # Navigation shell
│   ├── MauiProgram.cs      # Application initialization
│   ├── appsettings.json    # Application configuration
│   └── appsettings.Development.json
├── .gitignore
├── .env.example            # Environment variables template
└── README.md
```

## Technologies Used

- **.NET MAUI**: Cross-platform application framework
- **C#**: Primary programming language
- **Firebase Authentication**: User authentication service
- **Firebase Realtime Database**: Cloud database solution
- **NewsAPI**: News content aggregation service
- **CommunityToolkit.Maui**: UI component library
- **XAML**: User interface markup language

## Support

For questions, bug reports, or feature requests, please open an issue on the GitHub repository.

## Acknowledgments

- [Firebase](https://firebase.google.com/) for authentication and database services
- [NewsAPI](https://newsapi.org/) for news content aggregation
- [.NET MAUI](https://dotnet.microsoft.com/apps/maui) for the cross-platform framework

---

Built with .NET MAUI
