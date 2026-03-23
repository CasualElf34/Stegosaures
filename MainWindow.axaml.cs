using Avalonia.Controls;

namespace SteganographyLSB;

public partial class MainWindow : Window
{
public MainWindow()
{
    InitializeComponent();
    // Exemple d'attachement d'événement :
    this.FindControl<Button>("BtnPage1").Click += GoToPage1_Click;
    this.FindControl<Button>("BtnPage2").Click += GoToPage2_Click;
}

private async void GoToPage1_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
{
    var dialog = new Window
    {
        Title = "Test",
        Width = 200,
        Height = 100,
        Content = new TextBlock { Text = "Bouton 1 cliqué !" }
    };
    await dialog.ShowDialog(this);
}
private async void GoToPage2_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
{
    var dialog = new Window
    {
        Title = "Test",
        Width = 200,
        Height = 100,
        Content = new TextBlock { Text = "Bouton 2 cliqué !" }
    };
    await dialog.ShowDialog(this);
    }
}