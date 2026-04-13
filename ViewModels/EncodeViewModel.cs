using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SteganographyLSB.Services;
using SteganographyLSB.Models;

namespace SteganographyLSB.ViewModels;

public partial class EncodeViewModel : ViewModelBase
{
    private readonly LsbService _lsbService = new();

    [ObservableProperty]
    private string? _imagePath;

    [ObservableProperty]
    private WriteableBitmap? _previewImage;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private string _status = "Select an image to start.";

    [RelayCommand]
    private async Task EncodeAndSave(string outputPath)
    {
        if (PreviewImage == null || string.IsNullOrWhiteSpace(Message))
        {
            Status = "Please select an image and enter a message.";
            return;
        }

        try
        {
            Status = "Encoding...";
            var resultImage = _lsbService.Encode(PreviewImage, Message);
            
            resultImage.Save(outputPath);
            
            // Save to History
            using (var db = new HistoryContext())
            {
                db.Operations.Add(new OperationRecord
                {
                    FileName = Path.GetFileName(outputPath),
                    OperationType = "Encode",
                    MessageLength = Message.Length
                });
                await db.SaveChangesAsync();
            }

            Status = $"Success! Image saved to {Path.GetFileName(outputPath)}";
        }
        catch (Exception ex)
        {
            Status = $"Error: {ex.Message}";
        }
    }

    public void LoadImage(string path)
    {
        try
        {
            ImagePath = path;
            using var stream = File.OpenRead(path);
            var bitmap = new Bitmap(stream);
            var wb = new WriteableBitmap(bitmap.PixelSize, bitmap.Dpi, PixelFormats.Bgra8888, AlphaFormat.Premul);

            using (var fb = wb.Lock())
            {
                bitmap.CopyPixels(new PixelRect(bitmap.PixelSize), fb.Address, fb.RowBytes * fb.Size.Height, fb.RowBytes);

            }
            PreviewImage = wb;
            Status = "Image loaded. Ready to encode.";
        }
        catch (Exception ex)
        {
            Status = $"Failed to load image: {ex.Message}";
        }
    }
}

