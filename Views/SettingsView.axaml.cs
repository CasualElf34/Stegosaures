using Avalonia.Controls;

namespace SteganographyLSB.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = ViewModels.ThemeManager.Instance;

            var resetButton = this.FindControl<Button>("ResetBtn");
            if (resetButton != null)
            {
                resetButton.Click += (s, e) =>
                {
                    ViewModels.ThemeManager.Instance.SelectedThemeIndex = 0;
                };
            }
        }
    }
}
