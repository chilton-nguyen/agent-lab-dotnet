using Microsoft.JSInterop;
using Moq;
using SocOps.Services;
using Xunit;

namespace SocOps.Tests.Services;

public class ThemeServiceTests
{
    private readonly Mock<IJSRuntime> _jsRuntimeMock;

    public ThemeServiceTests()
    {
        _jsRuntimeMock = new Mock<IJSRuntime>();
    }

    private ThemeService CreateService() => new ThemeService(_jsRuntimeMock.Object);

    [Fact]
    public async Task InitializeAsync_WhenNoThemeInLocalStorage_DefaultsToDark()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync((string)null!);

        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getSystemPreference", It.IsAny<object[]>()))
            .ReturnsAsync((string)null!);

        var service = CreateService();

        // Act
        await service.InitializeAsync();

        // Assert
        Assert.Equal("dark", service.CurrentTheme);
    }

    [Fact]
    public async Task InitializeAsync_WhenLocalStorageHasLight_AppliesLightTheme()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync("light");

        var service = CreateService();

        // Act
        await service.InitializeAsync();

        // Assert
        Assert.Equal("light", service.CurrentTheme);
    }

    [Fact]
    public async Task InitializeAsync_WhenLocalStorageHasDark_AppliesDarkTheme()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync("dark");

        var service = CreateService();

        // Act
        await service.InitializeAsync();

        // Assert
        Assert.Equal("dark", service.CurrentTheme);
    }

    [Fact]
    public async Task ToggleThemeAsync_FromDark_SwitchesToLightAndSaves()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync("dark");

        var service = CreateService();
        await service.InitializeAsync();

        // Act
        await service.ToggleThemeAsync();

        // Assert
        Assert.Equal("light", service.CurrentTheme);
        _jsRuntimeMock.Verify(
            js => js.InvokeAsync<object>("themeInterop.setTheme", It.Is<object[]>(args => args.Length > 0 && (string)args[0] == "light")),
            Times.Once);
    }

    [Fact]
    public async Task ToggleThemeAsync_FromLight_SwitchesToDarkAndSaves()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync("light");

        var service = CreateService();
        await service.InitializeAsync();

        // Act
        await service.ToggleThemeAsync();

        // Assert
        Assert.Equal("dark", service.CurrentTheme);
        _jsRuntimeMock.Verify(
            js => js.InvokeAsync<object>("themeInterop.setTheme", It.Is<object[]>(args => args.Length > 0 && (string)args[0] == "dark")),
            Times.Once);
    }

    [Fact]
    public async Task CurrentTheme_ReturnsCorrectValue_AfterInitialization()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync("light");

        var service = CreateService();

        // Act
        await service.InitializeAsync();

        // Assert
        Assert.Equal("light", service.CurrentTheme);
    }

    [Fact]
    public async Task CurrentTheme_ReturnsCorrectValue_AfterToggle()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync("dark");

        var service = CreateService();
        await service.InitializeAsync();

        // Act
        await service.ToggleThemeAsync();

        // Assert
        Assert.Equal("light", service.CurrentTheme);

        // Toggle again
        await service.ToggleThemeAsync();
        Assert.Equal("dark", service.CurrentTheme);
    }

    [Fact]
    public async Task OnThemeChanged_FiresWhenThemeIsToggled()
    {
        // Arrange
        _jsRuntimeMock
            .Setup(js => js.InvokeAsync<string>("themeInterop.getStoredTheme", It.IsAny<object[]>()))
            .ReturnsAsync("dark");

        var service = CreateService();
        await service.InitializeAsync();

        string? receivedTheme = null;
        service.OnThemeChanged += theme => receivedTheme = theme;

        // Act
        await service.ToggleThemeAsync();

        // Assert
        Assert.NotNull(receivedTheme);
        Assert.Equal("light", receivedTheme);
    }
}
