using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SocOps.Components;
using SocOps.Services;
using Xunit;

namespace SocOps.Tests.Components;

public class ThemeToggleTests : BunitContext
{
    private Mock<IThemeService> CreateMockThemeService(string currentTheme = "dark")
    {
        var mock = new Mock<IThemeService>();
        mock.Setup(s => s.CurrentTheme).Returns(currentTheme);
        mock.Setup(s => s.ToggleThemeAsync()).Returns(Task.CompletedTask);
        return mock;
    }

    [Fact]
    public void Renders_ButtonElement()
    {
        var mock = CreateMockThemeService();
        Services.AddSingleton<IThemeService>(mock.Object);

        var cut = Render<ThemeToggle>();

        cut.Find("button");
    }

    [Fact]
    public void Button_HasAriaLabel()
    {
        var mock = CreateMockThemeService();
        Services.AddSingleton<IThemeService>(mock.Object);

        var cut = Render<ThemeToggle>();

        var button = cut.Find("button");
        Assert.Equal("Toggle theme", button.GetAttribute("aria-label"));
    }

    [Fact]
    public void ShowsSunIcon_WhenThemeIsDark()
    {
        var mock = CreateMockThemeService("dark");
        Services.AddSingleton<IThemeService>(mock.Object);

        var cut = Render<ThemeToggle>();

        var button = cut.Find("button");
        Assert.Contains("\u2600\uFE0F", button.InnerHtml);
    }

    [Fact]
    public void ShowsMoonIcon_WhenThemeIsLight()
    {
        var mock = CreateMockThemeService("light");
        Services.AddSingleton<IThemeService>(mock.Object);

        var cut = Render<ThemeToggle>();

        var button = cut.Find("button");
        Assert.Contains("\uD83C\uDF19", button.InnerHtml);
    }

    [Fact]
    public void Click_CallsToggleThemeAsync()
    {
        var mock = CreateMockThemeService();
        Services.AddSingleton<IThemeService>(mock.Object);

        var cut = Render<ThemeToggle>();

        cut.Find("button").Click();

        mock.Verify(s => s.ToggleThemeAsync(), Times.Once);
    }
}
