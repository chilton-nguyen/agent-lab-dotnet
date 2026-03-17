using Microsoft.JSInterop;

namespace SocOps.Services;

public class ThemeService : IThemeService
{
    private readonly IJSRuntime _jsRuntime;
    private string _currentTheme = "dark";

    public string CurrentTheme => _currentTheme;

    public event Action<string>? OnThemeChanged;

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        var storedTheme = await _jsRuntime.InvokeAsync<string>("themeInterop.getStoredTheme");
        _currentTheme = storedTheme ?? "dark";
        await _jsRuntime.InvokeAsync<object>("themeInterop.setTheme", _currentTheme);
    }

    public async Task ToggleThemeAsync()
    {
        _currentTheme = _currentTheme == "dark" ? "light" : "dark";
        await _jsRuntime.InvokeAsync<object>("themeInterop.setTheme", _currentTheme);
        OnThemeChanged?.Invoke(_currentTheme);
    }
}
