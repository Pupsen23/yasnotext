using System.Configuration;
using System.Data;
using System.Windows;
using YasnoText.UI.Themes;

namespace YasnoText.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // При старте приложения применяем стандартную тему,
        // чтобы все ресурсы (цвета, шрифты, размеры) были доступны.
        ThemeManager.ApplyTheme("Standard");
    }
}
