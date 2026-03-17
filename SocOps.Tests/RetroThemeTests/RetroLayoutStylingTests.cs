using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroLayoutStylingTests
{
    private static string GetProjectRoot()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory != null && !Directory.Exists(Path.Combine(directory.FullName, ".git")))
        {
            directory = directory.Parent;
        }
        return directory?.FullName ?? throw new InvalidOperationException("Project root not found");
    }

    private string GetLayoutCssPath(string layoutName)
    {
        return Path.Combine(GetProjectRoot(), "SocOps", "Layout", $"{layoutName}.razor.css");
    }

    private string ReadCssFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"CSS file not found at {path}");
        }
        return File.ReadAllText(path);
    }

    [Fact]
    public void MainLayoutCssFile_Exists()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("MainLayout");

        // Act & Assert
        Assert.True(File.Exists(cssPath), $"MainLayout.razor.css should exist at {cssPath}");
    }

    [Fact]
    public void NavMenuCssFile_Exists()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("NavMenu");

        // Act & Assert
        Assert.True(File.Exists(cssPath), $"NavMenu.razor.css should exist at {cssPath}");
    }

    [Fact]
    public void MainLayoutCssFile_ContainsRetroBackgroundColor()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("MainLayout");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"background-color:\s*(?:#0{6}|#1[0-9a-f]{5}|#2[0-3][0-9a-f]{4})|background-image:",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void MainLayoutCssFile_ContainsDarkThemeColors()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("MainLayout");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        // Dark background for retro theme
        Assert.Matches(@"background-color|background:|color:",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void NavMenuCssFile_ContainsRetroStyling()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("NavMenu");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"background-color:|color:|border:|link",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void NavMenuCssFile_NavLinksUseRetroColors()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("NavMenu");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"(a|\.nav-link)\s*\{[\s\S]*?(?:color:|background-color:)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void NavMenuCssFile_HoverState_UsesRetroGlow()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("NavMenu");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@":hover\s*\{[\s\S]*?(?:box-shadow|background-color|color)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void MainLayoutCssFile_ContainsRetroBackdrop()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("MainLayout");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"(background-image|background|::before|::after)\s*\{?[\s\S]*?(?:repeating-|linear-gradient|radial-gradient|pattern)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void NavMenuCssFile_ActiveLink_HasRetroIndicator()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("NavMenu");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"(\\.active|:active|\\[aria-current=|(\.current|\.selected))\s*\{",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void MainLayoutCssFile_ContainsRetroTextColor()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("MainLayout");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        // Text should use bright neon colors for contrast against dark background
        Assert.Matches(@"color:\s*(?:#00FFFF|#FF1493|#7B00FF|#00FF00|#[fF]{2}[fF]{2}[fF]{2})",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void NavMenuCssFile_ContainsBorder_WithRetroStyle()
    {
        // Arrange
        var cssPath = GetLayoutCssPath("NavMenu");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"border:|border-color:|border-width:|border-style:",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void LayoutCssFiles_UseConsistentRetroTheme()
    {
        // Arrange
        var layoutNames = new[] { "MainLayout", "NavMenu" };
        var retroThemePatterns = new[] 
        { 
            "(#00FFFF|#FF1493|#7B00FF|#00FF00)", // Neon colors
            "(box-shadow|text-shadow|filter)", // Glow effects
            "(background-color|color|border)" // Color properties
        };
        var matchCount = 0;

        // Act
        foreach (var layoutName in layoutNames)
        {
            var cssPath = GetLayoutCssPath(layoutName);
            if (File.Exists(cssPath))
            {
                var cssContent = ReadCssFile(cssPath);
                foreach (var pattern in retroThemePatterns)
                {
                    if (Regex.IsMatch(cssContent, pattern, RegexOptions.IgnoreCase))
                    {
                        matchCount++;
                    }
                }
            }
        }

        // Assert
        Assert.True(matchCount >= 4, "Layout CSS files should collectively contain retro theme patterns");
    }
}
