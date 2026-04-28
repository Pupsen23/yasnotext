using System.Windows;
using System.Windows.Input;
using YasnoText.UI.Themes;
using YasnoText.UI.ViewModels;

namespace YasnoText.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        // Создаём ViewModel и связываем с окном через DataContext.
        // Все биндинги в XAML работают через этот контекст.
        _viewModel = new MainViewModel(new WpfThemeApplier());
        DataContext = _viewModel;

        // Регистрируем горячие клавиши Ctrl+1, Ctrl+2, Ctrl+3.
        InputBindings.Add(new KeyBinding(
            new RelayCommand(() => _viewModel.ActivateById("standard")),
            Key.D1, ModifierKeys.Control));

        InputBindings.Add(new KeyBinding(
            new RelayCommand(() => _viewModel.ActivateById("low-vision")),
            Key.D2, ModifierKeys.Control));

        InputBindings.Add(new KeyBinding(
            new RelayCommand(() => _viewModel.ActivateById("dyslexia")),
            Key.D3, ModifierKeys.Control));
    }
}
