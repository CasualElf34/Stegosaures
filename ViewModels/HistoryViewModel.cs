using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SteganographyLSB.Models;

namespace SteganographyLSB.ViewModels;

public partial class HistoryViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<OperationRecord> _operations = new();

    public async Task LoadHistory()
    {
        using (var db = new HistoryContext())
        {
            // Ensure DB exists
            await db.Database.EnsureCreatedAsync();

            var records = await db.Operations
                .OrderByDescending(o => o.Timestamp)
                .ToListAsync();

            Operations.Clear();
            foreach (var record in records)
            {
                Operations.Add(record);
            }
        }
    }

    [RelayCommand]
    private async Task ClearHistory()
    {
        using (var db = new HistoryContext())
        {
            db.Operations.RemoveRange(db.Operations);
            await db.SaveChangesAsync();
        }
        Operations.Clear();
    }
}
