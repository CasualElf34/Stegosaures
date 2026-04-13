using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using SteganographyLSB.ViewModels;

namespace SteganographyLSB.Views;

public partial class EncodeView : UserControl
{
    public EncodeView()
    {
        InitializeComponent();
        DataContext = new EncodeViewModel();
    }

    private async void OnSelectImageClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Image",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }
        });

        if (files.Count >= 1)
        {
            var vm = (EncodeViewModel)DataContext!;
            vm.LoadImage(files[0].Path.LocalPath);
        }
    }

    private async void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        // This is a bit tricky with RelayCommand, but we can call it manually or use a service
        // For simplicity, let's just use the VM method if needed or handle picking here
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Encoded Image",
            DefaultExtension = "png",
            SuggestedFileName = "encoded_image.png",
            FileTypeChoices = new[] { FilePickerFileTypes.ImagePng }
        });

        if (file != null)
        {
            var vm = (EncodeViewModel)DataContext!;
            await vm.EncodeAndSaveCommand.ExecuteAsync(file.Path.LocalPath);
        }
    }
}
