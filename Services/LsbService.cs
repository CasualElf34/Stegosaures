using System;
using System.Text;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace SteganographyLSB.Services;

public class LsbService
{
    public unsafe WriteableBitmap Encode(WriteableBitmap source, string message)
    {
        byte[] messageBytes = Encoding.UTF8.GetBytes(message + "\0");
        int totalBitsNeeded = messageBytes.Length * 8;

        using (var buffer = source.Lock())
        {
            int width = source.PixelSize.Width;
            int height = source.PixelSize.Height;
            int totalPixels = width * height;

            // We can store 3 bits per pixel (R, G, B)
            if (totalBitsNeeded > totalPixels * 3)
            {
                throw new InvalidOperationException("Message is too long for this image.");
            }

            byte* ptr = (byte*)buffer.Address;
            int bitIndex = 0;

            for (int i = 0; i < messageBytes.Length; i++)
            {
                byte currentByte = messageBytes[i];
                for (int bit = 7; bit >= 0; bit--)
                {
                    int bitValue = (currentByte >> bit) & 1;
                    
                    // Calculate which pixel and which channel to use
                    int pixelIndex = bitIndex / 3;
                    int channelIndex = bitIndex % 3; // 0=B, 1=G, 2=R (in BGRA8888)

                    if (pixelIndex >= totalPixels) break;

                    byte* pixelPtr = ptr + (pixelIndex * 4) + channelIndex;
                    
                    // Clear the LSB and set it to bitValue
                    *pixelPtr = (byte)((*pixelPtr & 0xFE) | bitValue);
                    
                    bitIndex++;
                }
            }
        }

        return source;
    }

    public unsafe string Decode(WriteableBitmap source)
    {
        StringBuilder messageBuilder = new StringBuilder();
        var bytes = new System.Collections.Generic.List<byte>();

        using (var buffer = source.Lock())
        {
            byte* ptr = (byte*)buffer.Address;
            int width = source.PixelSize.Width;
            int height = source.PixelSize.Height;
            int totalPixels = width * height;

            byte currentByte = 0;
            int bitCount = 0;
            int bitIndex = 0;

            while (true)
            {
                int pixelIndex = bitIndex / 3;
                int channelIndex = bitIndex % 3;

                if (pixelIndex >= totalPixels) break;

                byte* pixelPtr = ptr + (pixelIndex * 4) + channelIndex;
                int bitValue = *pixelPtr & 1;

                currentByte = (byte)((currentByte << 1) | bitValue);
                bitCount++;

                if (bitCount == 8)
                {
                    if (currentByte == 0) break; // End of message
                    bytes.Add(currentByte);
                    currentByte = 0;
                    bitCount = 0;
                }

                bitIndex++;
            }
        }

        return Encoding.UTF8.GetString(bytes.ToArray());
    }
}
