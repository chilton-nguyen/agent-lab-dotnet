namespace SocOps.Services;

public interface IThemeService
{
    string CurrentTheme { get; }
    Task InitializeAsync();
    Task ToggleThemeAsync();
    event Action<string>? OnThemeChanged;
}
