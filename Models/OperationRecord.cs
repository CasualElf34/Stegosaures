using System;
using System.ComponentModel.DataAnnotations;

namespace SteganographyLSB.Models;

public class OperationRecord
{
    [Key]
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string OperationType { get; set; } = string.Empty; // "Encode" or "Decode"
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public int MessageLength { get; set; }
}
