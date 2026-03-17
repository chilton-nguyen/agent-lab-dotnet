using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroGlowEffectsTests
{
    private readonly string _appCssPath;

    public RetroGlowEffectsTests()
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
    public void AppCssFile_Contains_NeonGlowEffect_Class()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.glow|\.neon-glow|\.retro-glow",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_NeonGlowEffect_UsesCyanColor()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"(\.glow|\.neon-glow|\.retro-glow).*\{[\s\S]*?box-shadow:[\s\S]*?#00FFFF|box-shadow:.*#00FFFF",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_BoxShadowUtilityClasses_WithGlow()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.shadow-glow|\.shadow-neon|box-shadow:.*0.*0.*[\d.]+[prx]*.*(?:#00FFFF|#FF1493|#7B00FF|#00FF00)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_TextShadowUtilityForGlow()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.text-shadow-glow|\.text-glow|text-shadow:.*\d+px.*\d+px.*(?:#00FFFF|#FF1493|#00FF00|#7B00FF)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_GlowEffect_DefinedWithBoxShadow()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act
        var hasBoxShadowGlow = Regex.IsMatch(cssContent,
            @"(\.glow|\.neon-glow|\.retro-glow|\.shadow-glow)\s*\{[\s\S]*?box-shadow:",
            RegexOptions.IgnoreCase);

        // Assert
        Assert.True(hasBoxShadowGlow, "Glow effect should be defined using box-shadow property");
    }

    [Fact]
    public void AppCssFile_Contains_CyanGlowClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.glow-cyan|\.shadow-cyan-glow|\.text-shadow-cyan",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_PinkGlowClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.glow-pink|\.shadow-pink-glow|\.text-shadow-pink",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_PurpleGlowClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.glow-purple|\.shadow-purple-glow|\.text-shadow-purple",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_GlowEffect_UsesMultipleLayers()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act
        var glowDefinition = Regex.Match(cssContent,
            @"(\.glow|\.neon-glow|\.retro-glow)\s*\{[\s\S]*?box-shadow:\s*([^}]+)\}",
            RegexOptions.IgnoreCase);

        // Assert
        Assert.True(glowDefinition.Success, "Glow effect should be defined");
        if (glowDefinition.Success)
        {
            var shadowValue = glowDefinition.Groups[2].Value;
            // Multiple shadows are separated by commas
            var shadowCount = Regex.Matches(shadowValue, @"(?:^|,)\s*\d+px").Count;
            Assert.True(shadowCount >= 2, "Glow effect should use multiple shadow layers for better effect");
        }
    }

    [Fact]
    public void AppCssFile_GlowEffect_HasSpread()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"box-shadow:[\s\S]*?\d+px\s+\d+px\s+\d+px\s+\d+px",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_FilterGlowClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.filter-glow|\.blur-glow|filter:\s*drop-shadow",
            cssContent, RegexOptions.IgnoreCase);
    }
}
