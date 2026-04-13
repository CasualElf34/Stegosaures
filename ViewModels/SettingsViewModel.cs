using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Media;

namespace SteganographyLSB.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _selectedThemeIndex = 0;

        public IBrush MainColor => SelectedThemeIndex switch
        {
            1 => Brushes.DodgerBlue,
            2 => Brushes.Orange,
            3 => Brushes.Red,
            _ => Brushes.Teal
        };
    }
}
