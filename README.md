# RS Image Explorer

RS Image Explorer is a powerful and user-friendly WPF application designed for efficient image browsing and management. Built with .NET Core 8, it provides a seamless experience for viewing, selecting, and manipulating images in bulk.

## Features

- **Fast Image Navigation**: Quickly browse through images using arrow keys or mouse wheel.
- **Image Selection**: Select multiple images for bulk operations.
- **Full-Screen Mode**: Toggle full-screen view for immersive image browsing.
- **Image Rotation**: Rotate images clockwise or counter-clockwise with a single click.
- **Folder Loading**: Easily load images from any folder on your system.
- **Bulk Copy**: Copy selected images to a designated folder.
- **Thumbnail View**: View selected images in a convenient thumbnail list.
- **Keyboard Shortcuts**: Efficient navigation and operation through keyboard shortcuts.

## Technology Stack

- **Framework**: .NET Core 8
- **UI Framework**: Windows Presentation Foundation (WPF)
- **Architecture**: Model-View-ViewModel (MVVM)
- **Language**: C#

## Architecture Overview

The application follows the MVVM architectural pattern:

- **Model**: Represents the data and business logic of the application.
- **View**: The user interface, defined in XAML (MainWindow.xaml).
- **ViewModel**: Acts as an intermediary between the Model and View (MainViewModel.cs).

## Key Components

1. **MainWindow.xaml**: Defines the main user interface of the application.
2. **MainWindow.xaml.cs**: Contains the logic for user interactions and UI updates.
3. **MainViewModel.cs**: Implements the ViewModel, managing the application's state and business logic.

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/rs-image-explorer.git
   ```
2. Open the solution in Visual Studio 2022 or later.
3. Build and run the application.

## Usage

1. Launch the application.
2. Click the "Load Folder" button or press F11 to select a folder containing images.
3. Use arrow keys or mouse wheel to navigate through images.
4. Press Space or double-click to select/deselect images.
5. Use the toolbar buttons or keyboard shortcuts for various operations:
   - Enter: Toggle full-screen mode
   - Ctrl+R: Rotate clockwise
   - Ctrl+W: Rotate counter-clockwise
   - F12: Copy selected images to a new folder

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
