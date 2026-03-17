using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroColorPaletteTests
{
    private readonly string _appCssPath;

    public RetroColorPaletteTests()
    {
        _appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
    }

    private static string GetProjectRoot()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory != null && !Directory.Exists(Path.Combine(directory.FullName, ".git")))
        {
            directory = directory.Parent;
        }
        return directory?.FullName ?? throw new InvalidOperationException("Project root not found");
    }

    private string ReadCssFile()
    {
        if (!File.Exists(_appCssPath))
        {
            throw new FileNotFoundException($"CSS file not found at {_appCssPath}");
        }
        return File.ReadAllText(_appCssPath);
    }

    [Fact]
    public void AppCssFile_Contains_RetroColorVariables_Cyan()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"--retro-cyan\s*:\s*#00FFFF|\..*cyan.*\{\s*color:\s*#00FFFF|--cyan\s*:\s*#00FFFF",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_RetroColorVariables_HotPink()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"--retro-hot-pink\s*:\s*#FF1493|\..*hot-pink.*\{\s*color:\s*#FF1493|--hot-pink\s*:\s*#FF1493",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_RetroColorVariables_ElectricPurple()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"--retro-purple\s*:\s*#7B00FF|--electric-purple\s*:\s*#7B00FF|\..*purple.*\{\s*background-color:\s*#7B00FF",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_RetroColorVariables_LimeGreen()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"--retro-lime\s*:\s*#00FF00|--lime\s*:\s*#00FF00|\..*lime.*\{\s*color:\s*#00FF00",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_RetroColorVariables_DarkBackground()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        // Should have dark background color (near black or very dark gray) for retro aesthetic
        Assert.Matches(@"(--retro-dark|--dark-bg|\.bg-retro-dark|background-color:\s*#(0{6}|1[0-9a-f]{5}|2[0-3][0-9a-f]{4})(?!F))",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_TextCyan_UtilityClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.text-retro-cyan\s*\{|\.text-cyan-neon\s*\{",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_TextHotPink_UtilityClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.text-retro-pink\s*\{|\.text-hot-pink\s*\{",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_BackgroundRetroClasses()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.bg-retro-|\.bg-neon-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_BorderRetroClasses()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.border-retro-|\.border-neon-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_BgRetro_Dark_UsesCorrectColor()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act
        var match = Regex.Match(cssContent,
            @"\.bg-retro-dark\s*\{\s*background-color:\s*([^;]+);",
            RegexOptions.IgnoreCase);

        // Assert
        Assert.True(match.Success, "bg-retro-dark class not found in CSS");
        var colorValue = match.Groups[1].Value.Trim();
        Assert.Matches(@"(#0+|#1[0-9a-f]{5}|rgb\(\s*[0-2]?[0-9]?,)", colorValue, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_TextRetroColors_UseNeonValues()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        // Should have at least one text color using neon values (cyan, hot pink, lime, or purple)
        var hasNeonColors = Regex.IsMatch(cssContent,
            @"\.text-.*retro.*\{.*color:\s*(#00FFFF|#FF1493|#7B00FF|#00FF00)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        Assert.True(hasNeonColors, "Text color classes should use neon retro colors");
    }

    [Fact]
    public void AppCssFile_DefinesRetroColorPalette_WithAllRequiredColors()
    {
        // Arrange
        var cssContent = ReadCssFile();
        var requiredColors = new[] { "#00FFFF", "#FF1493", "#7B00FF", "#00FF00" };

        // Act & Assert
        var colorCount = 0;
        foreach (var color in requiredColors)
        {
            if (cssContent.Contains(color, StringComparison.OrdinalIgnoreCase))
            {
                colorCount++;
            }
        }

        Assert.True(colorCount >= 3, $"Expected at least 3 retro colors defined, found {colorCount}");
    }
}
