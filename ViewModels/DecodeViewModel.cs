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

    [ObservableProperty]
    private bool _isError = false;

    [RelayCommand]
    private async Task ExtractMessage()
    {
        if (SourceImage == null)
        {
            Status = "Veuillez sélectionner une image.";
            IsError = true;
            return;
        }

        try
        {
            Status = "Décodage en cours...";
            IsError = false;
            DecodedMessage = _lsbService.Decode(SourceImage);

            if (string.IsNullOrWhiteSpace(DecodedMessage))
            {
                Status = "Aucun message n'a été trouvé.";
                IsError = true;
            }
            else
            {
                Status = "Message extrait avec succès !";
                IsError = false;
            }

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
        }
        catch (Exception ex)
        {
            Status = "Erreur lors du décodage : " + ex.Message;
            IsError = true;
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

