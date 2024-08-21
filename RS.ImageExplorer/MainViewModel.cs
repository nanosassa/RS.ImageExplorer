using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RS.ImageExplorer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentFolder;
        public string CurrentFolder
        {
            get { return _currentFolder; }
            set { SetProperty(ref _currentFolder, value); }
        }

        private int _currentPosition;
        public int CurrentPosition
        {
            get { return _currentPosition; }
            set { SetProperty(ref _currentPosition, value); }
        }

        private int _totalImages;
        public int TotalImages
        {
            get { return _totalImages; }
            set { SetProperty(ref _totalImages, value); }
        }

        private string _currentImage;
        public string CurrentImage
        {
            get { return _currentImage; }
            set { SetProperty(ref _currentImage, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private int _selectedCount;
        public int SelectedCount
        {
            get { return _selectedCount; }
            set { SetProperty(ref _selectedCount, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}