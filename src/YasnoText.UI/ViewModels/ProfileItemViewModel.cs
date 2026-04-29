using YasnoText.Core.Profiles;

namespace YasnoText.UI.ViewModels;

/// <summary>
/// Обёртка над ReadingProfile для отображения в UI.
/// Хранит ссылку на профиль и флаг IsActive,
/// который позволяет подсвечивать активную карточку.
/// </summary>
public class ProfileItemViewModel : ViewModelBase
{
    private bool _isActive;

    public ProfileItemViewModel(ReadingProfile profile, string hotkey)
    {
        Profile = profile;
        Hotkey = hotkey;
    }

    /// <summary>Исходная модель профиля.</summary>
    public ReadingProfile Profile { get; }

    /// <summary>Название профиля для отображения.</summary>
    public string Name => Profile.Name;

    /// <summary>Текст с горячей клавишей: "Активен · Ctrl+1" или просто "Ctrl+1".</summary>
    public string HotkeyLabel => IsActive ? $"Активен · {Hotkey}" : Hotkey;

    /// <summary>Сама горячая клавиша без префикса.</summary>
    public string Hotkey { get; }

    /// <summary>Признак активного профиля. Влияет на подсветку карточки.</summary>
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (SetProperty(ref _isActive, value))
            {
                // HotkeyLabel зависит от IsActive — нужно тоже обновить.
                OnPropertyChanged(nameof(HotkeyLabel));
            }
        }
    }
}
