# RS Image Explorer

**RS Image Explorer** is a WPF-based desktop application designed for browsing, viewing, and manipulating image files. The application provides features such as image rotation, deletion, and seamless navigation through a collection of images.

## Features

- **Image Browsing**: Load and browse through a collection of images stored in a specified directory.
- **Image Rotation**: Rotate images clockwise or counterclockwise and save the rotated image back to the file.
- **Image Deletion**: Remove unwanted images from the collection.
- **Smooth Navigation**: Navigate through images with updated indicators for the current position in the collection.
- **Real-time Updates**: Automatically refresh the displayed image after rotation or deletion, ensuring the view is always up to date.

## Installation

1. **Clone the repository:**
   ```sh
   git clone https://github.com/yourusername/image-explorer.git
   ```
2. **Open the solution** in Visual Studio 2022 or later.
3. **Restore NuGet packages** to ensure all dependencies are installed.
4. **Build the solution** to compile the application.

## Usage

- **Load Images**: Select a directory containing your image files. The application will load and display the images, allowing you to navigate through them.
- **Rotate Images**: Use the rotate button to turn images 90 degrees clockwise or counterclockwise. The rotated image will be saved, and the display will refresh to show the updated version.
- **Delete Images**: Use the delete button to remove an image from the collection. The application will automatically update to show the next available image.

## Technical Details

- **Technologies Used**: 
  - .NET Core 8
  - WPF (Windows Presentation Foundation)
- **Language**: C#

## Contributing

Contributions are welcome! If you'd like to improve the application or fix bugs, feel free to fork the repository and submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
