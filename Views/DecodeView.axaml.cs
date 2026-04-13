using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using SteganographyLSB.ViewModels;

namespace SteganographyLSB.Views;

public partial class DecodeView : UserControl
{
    public DecodeView()
    {
        InitializeComponent();
        DataContext = new DecodeViewModel();
    }

    private async void OnSelectImageClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Image To Decode",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }
        });

        if (files.Count >= 1)
        {
            var vm = (DecodeViewModel)DataContext!;
            vm.LoadImage(files[0].Path.LocalPath);
        }
    }
}
