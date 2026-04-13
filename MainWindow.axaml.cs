using Avalonia.Controls;

namespace SteganographyLSB;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Affiche EncodeView par défaut
        MainContent.Content = new Views.EncodeView();

        EncodeBtn.Click += (s, e) =>
        {
            MainContent.Content = new Views.EncodeView();
        };
        DecodeBtn.Click += (s, e) =>
        {
            MainContent.Content = new Views.DecodeView();
        };
    }
}