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

public partial class DecodeViewModel : ViewModelBase
{
    private readonly LsbService _lsbService = new();

    [ObservableProperty]
    private string? _imagePath;

    [ObservableProperty]
    private WriteableBitmap? _sourceImage;

    [ObservableProperty]
    private string _decodedMessage = string.Empty;

    [ObservableProperty]
    private string _status = "Select an image to decode.";

    [RelayCommand]
    private async Task ExtractMessage()
    {
        if (SourceImage == null)
        {
            Status = "Please select an image.";
            return;
        }

        try
        {
            Status = "Decoding...";
            DecodedMessage = _lsbService.Decode(SourceImage);
            
            // Save to History
            using (var db = new HistoryContext())
            {
                db.Operations.Add(new OperationRecord
                {
                    FileName = Path.GetFileName(ImagePath ?? "unknown"),
                    OperationType = "Decode",
                    MessageLength = DecodedMessage.Length
                });
                await db.SaveChangesAsync();
            }

            Status = string.IsNullOrWhiteSpace(DecodedMessage) ? "No message found." : "Message extracted successfully!";
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
                bitmap.CopyPixels(new PixelRect(bitmap.PixelSize), fb.Address, (int)fb.RowBytes * fb.Size.Height, (int)fb.RowBytes);

            }
            SourceImage = wb;
            Status = "Image loaded.";
        }
        catch (Exception ex)
        {
            Status = $"Failed to load image: {ex.Message}";
        }
    }
}

