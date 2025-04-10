# R.O.B.O

## Project Overview
Welcome to **R.O.B.O** (Robot Operations and Behavioral Optimization), a robot control interface designed to manage the movements of a robot’s head and arms. This project provides an intuitive web-based UI for controlling head rotation, head tilt, and the elbow and wrist movements of both left and right arms.

R.O.B.O follows a **Clean Architecture** approach to ensure maintainability, scalability, and separation of concerns:
- **Web UI (`Robo.Web`)**: A Razor Pages application that interacts with users and communicates with the API.
- **API (`Robo.Api`)**: A RESTful service that handles requests from the UI and coordinates with the application layer.
- **Domain (`Robo.Domain`)**: Defines core entities, enums (e.g., `HeadRotation`, `ElbowState`), and business rules.
- **Application (`Robo.Application`)**: Contains DTOs (e.g., `RobotStateDto`) and business logic for orchestrating robot operations.
- **Infrastructure (`Robo.Infrastructure`)**: Manages data persistence.

The Web UI sends commands to the API, which processes them through the Application layer, ensuring the Domain’s integrity, while the Infrastructure layer handles database communication.

## Project Structure
The solution is organized into the following projects:

- **`Robo.Api`**: The RESTful API layer, exposing endpoints for robot control (e.g., `/api/robot/head/rotate`). It receives HTTP requests from the Web UI and delegates to the Application layer.
- **`Robo.Web`**: The front-end Razor Pages application, providing a user-friendly interface for controlling the robot and displaying its state in real-time.
- **`Robo.Domain`**: The core layer with entities (e.g., robot state), enums (e.g., `HeadTilt`, `WristState`), and business rules, independent of external systems.
- **`Robo.Application`**: The application layer with DTOs and use cases, bridging the Domain and external layers like the API and Infrastructure.
- **`Robo.Infrastructure`**: Handles external dependencies, such as the `IHttpClientFactory` for API communication with the robot hardware or simulator.
- **`Robo.Tests`**: Contains unit and integration tests to validate the functionality of the Domain, Application, and API layers.

## Features
R.O.B.O supports the following robot movements:
- **Head Rotation**: Adjusts the head’s horizontal orientation (e.g., values 0-4, corresponding to predefined positions like Left, Center, Right).
- **Head Tilt**: Controls the head’s vertical angle (e.g., values 0-2, such as Up, Neutral, Down).
- **Left Arm Elbow Movement**: Flexes or extends the left elbow (e.g., values 0-3, like Contracted, Relaxed).
- **Left Arm Wrist Movement**: Rotates the left wrist (e.g., values 0-6, representing angles like -90° to 90°).
- **Right Arm Elbow Movement**: Flexes or extends the right elbow (e.g., values 0-3, like Contracted, Relaxed).
- **Right Arm Wrist Movement**: Rotates the right wrist (e.g., values 0-6, representing angles like -90° to 90°).

**Key Highlights:**
- **Real-Time State Management**: The UI updates dynamically, reflecting the robot’s current state without page reloads.
- **Error Handling**: Robust error messages are displayed for network or API failures, persisted across requests using `TempData`.

## Technical Requirements
- **.NET Core Version**: Built with .NET 9.
- **IDE**: JetBrains Rider 2024.
- **SDK**: .NET 9 SDK (download from [dotnet.microsoft.com](https://dotnet.microsoft.com)).
- **Prerequisites**:
    - Windows, macOS, or Linux with the .NET SDK installed.
    - A modern browser (e.g., Chrome, Firefox) for the Web UI.
    - Optional: Docker, if containerization is desired (not configured by default).

## Configuration and Setup

### Running the API (`Robo.Api`)
1. Open a terminal in the `Robo.Api` folder.
2. Restore NuGet packages:
   ```bash
   dotnet restore
   dotnet build
   ```
3. Run the API:
   ```bash
    dotnet run
    ```
    - The API will start at `http://localhost:5146`.
    - Verify it’s running by navigating to `http://localhost:5146/api/robot`.

### Running the Web UI (`Robo.Web`)
1. Open a terminal in the `Robo.Web` folder.
2. Restore NuGet packages:
   ```bash
   dotnet restore
   dotnet build
    ```
3. Run the Web UI:
    ```bash
    dotnet run
    ```
    - The API will start at `http://localhost:5202`.
    - Open `http://localhost:5202` in your browser to access the interface.

### Setting Up the Solution
1. Clone the repository or open the solution (`Robo.sln`) in your IDE.
2. Restore NuGet packages for the entire solution:
   ```bash
   dotnet restore
   dotnet build
    ```
3. Start both projects (API first, then Web UI) using the steps above, either via terminal or IDE multi-startup configuration.