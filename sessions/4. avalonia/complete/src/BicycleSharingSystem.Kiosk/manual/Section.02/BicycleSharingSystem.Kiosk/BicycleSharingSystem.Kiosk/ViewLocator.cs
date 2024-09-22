using System;

using Avalonia.Controls;
using Avalonia.Controls.Templates;

using BicycleSharingSystem.Kiosk.ViewModels;

namespace BicycleSharingSystem.Kiosk;
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var pathName = data.GetType().Namespace!.Replace("ViewModel", "Page");
        var folderName = data.GetType().Name!.Replace("ViewModel", "");
        // var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType($"{pathName}.{folderName}.Index");

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            return control;
        }

        return new TextBlock { Text = "Not Found: " + $"{pathName}.{folderName}.index" };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}