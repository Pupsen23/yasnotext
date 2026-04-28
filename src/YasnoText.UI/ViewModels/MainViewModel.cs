using System.Collections.ObjectModel;
using System.Windows.Input;
using YasnoText.Core.Profiles;

namespace YasnoText.UI.ViewModels;

/// <summary>
/// Главный ViewModel окна. Хранит коллекцию профилей,
/// текущий активный профиль и команду переключения.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly IThemeApplier _themeApplier;
    private ProfileItemViewModel? _activeProfile;

    public MainViewModel(IThemeApplier themeApplier)
    {
        _themeApplier = themeApplier;

        // Сопоставляем встроенные профили с горячими клавишами.
        var hotkeys = new Dictionary<string, string>
        {
            { "standard", "Ctrl+1" },
            { "low-vision", "Ctrl+2" },
            { "dyslexia", "Ctrl+3" }
        };

        Profiles = new ObservableCollection<ProfileItemViewModel>(
            BuiltInProfiles.All.Select(p => new ProfileItemViewModel(
                p,
                hotkeys.TryGetValue(p.Id, out var hk) ? hk : string.Empty)));

        SelectProfileCommand = new RelayCommand(p =>
        {
            if (p is ProfileItemViewModel vm)
            {
                ActivateProfile(vm);
            }
        });

        // По умолчанию активен Стандартный профиль.
        ActivateProfile(Profiles.First());
    }

    /// <summary>Список всех доступных профилей.</summary>
    public ObservableCollection<ProfileItemViewModel> Profiles { get; }

    /// <summary>Активный профиль. Изменение вызывает применение темы.</summary>
    public ProfileItemViewModel? ActiveProfile
    {
        get => _activeProfile;
        private set
        {
            if (SetProperty(ref _activeProfile, value))
            {
                OnPropertyChanged(nameof(StatusText));
            }
        }
    }

    /// <summary>Команда переключения профиля. Параметр — ProfileItemViewModel.</summary>
    public ICommand SelectProfileCommand { get; }

    /// <summary>Текст для строки состояния: "Профиль: Стандартный · 14pt".</summary>
    public string StatusText
    {
        get
        {
            if (ActiveProfile == null)
            {
                return "Готов к работе";
            }

            var profile = ActiveProfile.Profile;
            return $"Профиль: {profile.Name} · {profile.FontSize}pt";
        }
    }

    /// <summary>Активирует профиль по идентификатору. Используется для горячих клавиш.</summary>
    public void ActivateById(string profileId)
    {
        var target = Profiles.FirstOrDefault(p => p.Profile.Id == profileId);
        if (target != null)
        {
            ActivateProfile(target);
        }
    }

    private void ActivateProfile(ProfileItemViewModel profileVm)
    {
        // Снимаем флаг IsActive со всех профилей.
        foreach (var p in Profiles)
        {
            p.IsActive = false;
        }

        // Активируем выбранный.
        profileVm.IsActive = true;
        ActiveProfile = profileVm;

        // Применяем тему.
        _themeApplier.Apply(profileVm.Profile.Id);
    }
}
