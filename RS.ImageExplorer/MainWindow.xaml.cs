using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace RS.ImageExplorer
{
    public partial class MainWindow : Window
    {
        private List<string> imagePaths;
        private int currentIndex = 0;
        private List<string> selectedImages = new List<string>();
        
        private MainViewModel viewModel; 

        public MainWindow()
        {
            InitializeComponent();

            // Crear una instancia del ViewModel
            // Asignar el ViewModel al DataContext de la ventana
            viewModel = new MainViewModel();
            this.DataContext = viewModel;

        }

        private void LoadImages(string folderPath)
        {
            imagePaths = Directory.GetFiles(folderPath, "*.*")
                .Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("png"))
                .ToList();

            viewModel.CurrentFolder = folderPath;
            viewModel.TotalImages = imagePaths.Count;

            if (imagePaths.Any())
            {
                DisplayImage(imagePaths[currentIndex]);
                UpdateSelectionDisplay();
            }
        }

        private void DisplayImage(string imagePath)
        {
            MainImage.Source = new BitmapImage(new Uri(imagePath));
            viewModel.CurrentImage = Path.GetFileName(imagePath);
            viewModel.CurrentPosition = currentIndex + 1; 
            UpdateSelectionIndicator();
        }

        private void NextImage()
        {
            if (currentIndex < imagePaths.Count - 1)
            {
                currentIndex++;
                DisplayImage(imagePaths[currentIndex]);

                // Buscar la imagen actual en la lista y establecer el SelectedItem
                var selectedItem = SelectedImagesList.Items.Cast<BitmapImage>().FirstOrDefault(i => i.UriSource.LocalPath == imagePaths[currentIndex]);
                if (selectedItem != null)
                {
                    SelectedImagesList.SelectedItem = selectedItem;
                    viewModel.IsSelected = true;
                }
                else 
                {
                    SelectedImagesList.SelectedItem = null;
                    viewModel.IsSelected = false;
                }
            }
        }

        private void PreviousImage()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayImage(imagePaths[currentIndex]);

                // Buscar la imagen actual en la lista y establecer el SelectedItem
                var selectedItem = SelectedImagesList.Items.Cast<BitmapImage>().FirstOrDefault(i => i.UriSource.LocalPath == imagePaths[currentIndex]);
                if (selectedItem != null)
                {
                    SelectedImagesList.SelectedItem = selectedItem;
                    viewModel.IsSelected= true;
                }
                else
                {
                    SelectedImagesList.SelectedItem = null;
                    viewModel.IsSelected = false;
                }
            }
        }

        private void ToggleFullScreen()
        {
            if (WindowStyle == WindowStyle.None)
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
            }
        }

        private void ToggleSelection()
        {
            string currentImage = imagePaths[currentIndex];
            if (selectedImages.Contains(currentImage))
            {
                selectedImages.Remove(currentImage);
                viewModel.IsSelected = false;
            }
            else
            {
                selectedImages.Add(currentImage);
                viewModel.IsSelected = true;
            }
            UpdateSelectionDisplay();
            UpdateSelectionIndicator();
        }

        private void UpdateSelectionDisplay()
        {
            SelectedImagesList.ItemsSource = null;
            SelectedImagesList.ItemsSource = selectedImages.Select(path => new BitmapImage(new Uri(path)));
        }

        private void UpdateSelectionIndicator()
        {
            if (imagePaths != null && imagePaths.Any())
            {
                string currentImage = imagePaths[currentIndex];
                SelectionIndicator.Visibility = selectedImages.Contains(currentImage) ? Visibility.Visible : Visibility.Collapsed;

                // Ajustar la posición del indicador en función del tamaño de la imagen
                Canvas.SetLeft(SelectionIndicator, MainImage.ActualWidth - SelectionIndicator.Width - 10);
                Canvas.SetTop(SelectionIndicator, 10);
            }
            else
            {
                SelectionIndicator.Visibility = Visibility.Collapsed;
            }
        }

        private void CopySelectedImages(string destinationFolder)
        {
            foreach (string imagePath in selectedImages)
            {
                string fileName = Path.GetFileName(imagePath);
                string destinationPath = Path.Combine(destinationFolder, fileName);
                File.Copy(imagePath, destinationPath, true);
            }
            MessageBox.Show("Imágenes seleccionadas copiadas exitosamente.", "Operación completada", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Manejadores de eventos

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousImage();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextImage();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleSelection();
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleFullScreen();
        }

        private void MainImage_MouseClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                ToggleSelection();
            }
        }


        private void SelectedImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedImagesList.SelectedItem != null)
            {
                BitmapImage selectedImage = (BitmapImage)SelectedImagesList.SelectedItem;
                currentIndex = imagePaths.IndexOf(selectedImage.UriSource.LocalPath);
                DisplayImage(imagePaths[currentIndex]);
            }
        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que generó el evento
            Button deleteButton = sender as Button;

            // Obtener el elemento de la lista asociado al botón (el DataContext)
            var listItem = deleteButton.DataContext as BitmapImage;

            // Remover el elemento de la lista SelectedImages
            selectedImages.Remove(listItem.UriSource.LocalPath);

            // Actualizar la fuente de datos de la ListBox
            SelectedImagesList.ItemsSource = null;
            SelectedImagesList.ItemsSource = selectedImages.Select(path => new BitmapImage(new Uri(path)));

            UpdateSelectionDisplay();

            // Actualizar el indicador de selección de la imagen actual
            UpdateSelectionIndicator();
        }


        private void MainImage_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            
            if (e.Delta > 0)
            {
                PreviousImage();
            }
            else
            {
                NextImage();
            }
            e.Handled = true;
        }

        private void LoadFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadImages(dialog.FolderName);
            }
        }

        private void CopySelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            if (dialog.ShowDialog() == true)
            {
                CopySelectedImages(dialog.FolderName);
            }
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyDown(e);
        }

        private void SelectedImagesList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyDown(e);
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Previene que el ListBox maneje la tecla espaciadora
            }
        }
        private void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    PreviousImage();
                    break;
                case Key.Right:
                    NextImage();
                    break;
                case Key.Enter:
                    ToggleFullScreen();
                    break;
                case Key.Space:
                    ToggleSelection();
                    break;
                case Key.F11:
                    LoadFolderButton_Click(null, null);
                    break;
                case Key.F12:
                    CopySelectedButton_Click(null, null);
                    break;
            }
        }
    }
}