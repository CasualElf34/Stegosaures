using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Media;

namespace SteganographyLSB.ViewModels
{
    public partial class ThemeManager : ObservableObject
    {
        private static ThemeManager? _instance;
        public static ThemeManager Instance => _instance ??= new ThemeManager();


        [ObservableProperty]
        private int _selectedThemeIndex = 0;

        partial void OnSelectedThemeIndexChanged(int value)
        {
            OnPropertyChanged(nameof(MainColor));
        }

        public IBrush MainColor => SelectedThemeIndex switch
        {
            1 => Brushes.DodgerBlue,
            2 => Brushes.Orange,
            3 => Brushes.Red,
            _ => Brushes.Teal
        };
    }
}
