using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroTypographyTests
{
    private readonly string _appCssPath;

    public RetroTypographyTests()
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
    public void AppCssFile_DefinesRetroFont_InRootOrBodySelector()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"(html|body|:root)\s*\{[\s\S]*?font-family:\s*[^}]*(?:\'Courier New\'|\'Courier\'|monospace|\'Press Start|\'Orbitron|\'VT323)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_DefinesHeadingRetroStyle()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"h[1-6]\s*\{|\.retro-heading\s*\{",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_HeadingFontWeight_IsResponsive()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"(h[1-6]|\.retro-heading|\.font-retro-bold)\s*\{[\s\S]*?(font-weight:\s*(700|800|900|bold)|text-transform)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_MonospaceUtilityClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.font-mono|\.font-monospace|\.font-retro",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsRetroFontFamily_UtilityClasses()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.font-arcade|\.font-press-start|\.font-orbitron|\.font-courier|\.font-mono",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_DefinesPixelOrBlockyFontClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.font-pixel|\.font-blocky|\.font-8bit",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_LetterSpacingForRetro()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"letter-spacing:\s*[\d.]+(?:px|em)|\.tracking-tight|\.tracking-wide",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_TextShadowUsedForRetroEffect()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"text-shadow:\s*\d+px\s+\d+px",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_AllCapsUtilityClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.uppercase|text-transform:\s*uppercase",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_FontSizeUtilityClasses_IncludeRetroSizes()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        // Should have variety of text sizes for retro 8-bit style
        var hasLargeSizes = Regex.IsMatch(cssContent, @"\.text-(3xl|4xl|5xl|6xl|7xl)");
        Assert.True(hasLargeSizes, "Should have large text sizes for retro aesthetic");
    }
}
