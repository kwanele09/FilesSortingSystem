# FilesSortingSystem

*A Windows desktop application built with .NET MAUI to organize and manage your files efficiently.*

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-512BD4?logo=dotnet&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Windows-blue)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-Active-success)

---

## Table of Contents

- [Project Goal](#project-goal)
- [Features](#features)
- [Why this project?](#why-this-project)
- [Screenshots & Demo](#screenshots--demo)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Architecture & Technologies](#architecture--technologies)
- [Project Structure](#project-structure)
- [Future Enhancements](#future-enhancements)
- [Contributing](#contributing)
- [License](#license)

---

## Project Goal

**FilesSortingSystem** aims to simplify and automate file organization on Windows.  
Over time, users accumulate large numbers of documents, images, videos, and downloads that often become scattered and unstructured.  
This app provides an intuitive and automated way to bring order to that chaos by:

- Allowing users to configure sorting rules (by file extension or type)
- Offering a clean user interface for selecting folders
- Automating file movement based on custom or default rules
- Improving productivity and system organization
- Delivering a modern Windows experience using **.NET MAUI**

---

## Features

- Select a source folder to process  
- Define one or more destination folders (categories)  
- Create rules based on:
  - File extensions (`.jpg`, `.mp4`, `.docx`, etc.)
  - File types (image, video, document, audio)
- Execute the sorting process (moves files into target folders)
- Generate logs and reports of all actions
- Native Windows integration with modern UI via **.NET MAUI**

---

## Screenshots & Demo

### Getting Started

### Prerequisites

- Windows 10 or newer  
- .NET 9 SDK or runtime  
- (Optional) Visual Studio 2022+ with the .NET MAUI workload installed  

### Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/kwanele09/FilesSortingSystem.git

2. Open the solution in Visual Studio and restore NuGet packages.
3. Build the project in **Release** or **Debug** mode.
4. Run the application or publish it to generate an installer.

---

## Usage

1. Launch the app.
2. Choose the folder you want to sort.
3. Configure sorting rules (e.g., `.jpg`, `.png` → *Images*; `.pdf`, `.docx` → *Documents*).
4. Run the sorting operation.
5. Review the log to see which files were moved and where.

---

## Architecture & Technologies

* **UI:** .NET MAUI (XAML + C#)
* **File Operations:** `System.IO` for enumerating, filtering, and moving files
* **Configuration:** JSON or app-based settings storage
* **Logging:** File and UI-based logs for all operations
* **Pattern:** MVVM architecture (ViewModels separate from business logic)
* **Error Handling:** Handles access, permission, and path-length issues gracefully

---

## Project Structure

```
/FilesSortingSystem
  /src
    /Models       → File and rule models
    /ViewModels   → Application logic and bindings
    /Views        → XAML-based UI pages
    /Services     → FileProcessingService, RuleEngine, LoggingService
    /Resources    → Assets, icons, and styles
  /assets         → Screenshots, demo videos
  /Tests          → Unit/integration tests
  README.md
```

---

## Future Enhancements

Below are potential future improvements that would make the application more powerful and appealing to users:

### Core File Handling

* Copy vs. Move toggle
* Multi-folder processing
* Undo/restore for last operations
* Real-time folder monitoring
* Duplicate file detection
* Metadata-based categorization
* Preserve folder hierarchy

### Smart Automation

* AI-assisted file categorization
* Image EXIF-based sorting (by date/location)
* Audio metadata sorting (artist, album, genre)
* Document content scanning (detect “invoice”, “resume”, etc.)
* Smart cleanup suggestions for old or unused files

### User Experience

* Drag-and-drop folder input
* Dark/light mode themes
* System tray mode
* Sorting analytics dashboard
* Exportable detailed logs (CSV, TXT)
* Saved configuration profiles
* Custom rule templates
* Desktop notifications for completed tasks

### Integration & Platform Expansion

* macOS and Linux support (cross-platform .NET MAUI)
* Cloud integration (OneDrive, Google Drive, Dropbox)
* Archive management (`.zip`, `.rar`)
* Windows Explorer context menu integration
* Task Scheduler support for automation

### Developer & Community

* Unit and integration test coverage
* Plugin system for custom logic
* Localization (multi-language support)
* In-app documentation/tutorial
* Auto-updater
* Community rule set sharing

---

## Contributing

Contributions are welcome!
If you’d like to collaborate:

1. Fork the repository
2. Create a new branch:

   ```bash
   git checkout -b feature/YourFeatureName
   ```
3. Make and commit your changes
4. Submit a pull request describing the enhancement or fix

Please maintain consistent code style and follow existing architecture patterns.

---

## License

```
MIT License  
Copyright (c) 2025 Kwanele
```

This project is licensed under the MIT License — see the [LICENSE](./LICENSE) file for details.

---
