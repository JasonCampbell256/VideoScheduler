using Avalonia.Controls;
using Avalonia.Input;
using System;
using VideoScheduler.UI.ViewModels;

namespace VideoScheduler.UI.Views;

public partial class VideoPlayer : UserControl
{
    public VideoPlayer()
    {
        InitializeComponent();
    }

    private void OnDataContextChanged(object sender, EventArgs args)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            
        }
    }

}