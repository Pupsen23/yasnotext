namespace YasnoText.Core.Profiles;

/// <summary>
/// Абстракция для применения темы оформления.
/// Core ничего не знает о WPF, поэтому объявляет интерфейс,
/// а реализация живёт в UI (см. WpfThemeApplier).
///
/// Это даёт два преимущества:
/// 1. Core можно тестировать без WPF-контекста.
/// 2. В будущем тему можно применять не только к WPF
///    (например, к веб-версии).
/// </summary>
public interface IThemeApplier
{
    /// <summary>
    /// Применить тему по её идентификатору профиля.
    /// </summary>
    /// <param name="profileId">Идентификатор: "standard", "low-vision", "dyslexia".</param>
    void Apply(string profileId);
}
