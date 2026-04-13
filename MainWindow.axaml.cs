using System;
using Avalonia.Controls;

namespace SteganographyLSB;

public partial class MainWindow : Window
{
    public MainWindow()
    {

        Console.WriteLine("MainWindow instanciée");

        InitializeComponent();
        WindowState = WindowState.Maximized;
        DataContext = ViewModels.ThemeManager.Instance;

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
        SettingsBtn.Click += (s, e) =>
        {
            MainContent.Content = new Views.SettingsView();
        };
    }
}