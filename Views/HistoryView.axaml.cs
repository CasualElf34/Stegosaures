using Avalonia;
using Avalonia.Controls;
using SteganographyLSB.ViewModels;

namespace SteganographyLSB.Views;

public partial class HistoryView : UserControl
{
    public HistoryView()
    {
        InitializeComponent();
        DataContext = new HistoryViewModel();
    }

    protected override async void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (DataContext is HistoryViewModel vm)
        {
            await vm.LoadHistory();
        }
    }
}
