using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroThemeConsistencyTests
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

    private string ReadCssFile(string path)
    {
        if (!File.Exists(path))
        {
            return string.Empty;
        }
        return File.ReadAllText(path);
    }

    [Fact]
    public void AllComponentsAndLayoutsCss_UseRetroColorPalette()
    {
        // Arrange
        var retroColors = new[] { "#00FFFF", "#FF1493", "#7B00FF", "#00FF00" };
        var cssFiles = GetAllProjectCssFiles();
        var colorUsageCount = 0;

        // Act
        foreach (var cssFile in cssFiles)
        {
            var content = ReadCssFile(cssFile);
            foreach (var color in retroColors)
            {
                if (content.Contains(color, StringComparison.OrdinalIgnoreCase))
                {
                    colorUsageCount++;
                    break; // Count file once per color
                }
            }
        }

        // Assert
        Assert.True(colorUsageCount >= 3, $"Expected at least 3 CSS files using retro colors, found {colorUsageCount}");
    }

    [Fact]
    public void AppCssGlobalFile_And_ComponentCssFiles_UseConsistentColorScheme()
    {
        // Arrange
        var appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
        var appCss = ReadCssFile(appCssPath);
        var componentCssFiles = GetComponentCssFiles();

        // Act
        var retroColors = new[] { "#00FFFF", "#FF1493", "#7B00FF", "#00FF00" };
        var appCssHasColors = retroColors.Count(color => appCss.Contains(color, StringComparison.OrdinalIgnoreCase));

        // Assert
        Assert.True(appCssHasColors >= 2, "app.css should define at least 2 retro colors");
    }

    [Fact]
    public void RetroTheme_AppliedToAllGameComponents()
    {
        // Arrange
        var gameComponents = new[] { "BingoSquare", "BingoBoard", "GameScreen" };
        var retroThemeIndicators = new[] { "#00FFFF", "#FF1493", "#7B00FF", "#00FF00", "glow", "shadow" };

        // Act
        var componentsWithTheme = 0;
        foreach (var component in gameComponents)
        {
            var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", $"{component}.razor.css");
            if (File.Exists(cssPath))
            {
                var content = ReadCssFile(cssPath);
                var hasTheme = retroThemeIndicators.Any(indicator => 
                    content.Contains(indicator, StringComparison.OrdinalIgnoreCase));

                if (hasTheme)
                {
                    componentsWithTheme++;
                }
            }
        }

        // Assert
        Assert.True(componentsWithTheme >= 2, $"Expected at least 2 game components with retro theme, found {componentsWithTheme}");
    }

    [Fact]
    public void RetroTheme_AppliedToLayoutComponents()
    {
        // Arrange
        var layoutComponents = new[] { "MainLayout", "NavMenu" };
        var retroThemeIndicators = new[] { "#00FFFF", "#FF1493", "#7B00FF", "#00FF00", "glow", "shadow", "dark" };

        // Act
        var layoutsWithTheme = 0;
        foreach (var layout in layoutComponents)
        {
            var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Layout", $"{layout}.razor.css");
            if (File.Exists(cssPath))
            {
                var content = ReadCssFile(cssPath);
                var hasTheme = retroThemeIndicators.Any(indicator => 
                    content.Contains(indicator, StringComparison.OrdinalIgnoreCase));

                if (hasTheme)
                {
                    layoutsWithTheme++;
                }
            }
        }

        // Assert
        Assert.True(layoutsWithTheme >= 1, $"Expected at least 1 layout component with retro theme, found {layoutsWithTheme}");
    }

    [Fact]
    public void GlowEffect_UsesConsistentBlurAndSpread()
    {
        // Arrange
        var appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
        var appCss = ReadCssFile(appCssPath);

        // Act
        var glowMatches = Regex.Matches(appCss, 
            @"(\.glow|\.neon-glow|box-shadow:.*(?:#00FFFF|#FF1493|#7B00FF|#00FF00))",
            RegexOptions.IgnoreCase);

        // Assert
        Assert.True(glowMatches.Count >= 1, "app.css should define at least one glow effect");
    }

    [Fact]
    public void DarkBackgroundColor_ConsistentallyDefined()
    {
        // Arrange
        var cssFiles = GetAllProjectCssFiles();
        var darkColorPattern = @"(#0{6}|#1[0-9a-f]{5}|#2[0-3][0-9a-f]{4}|rgb\(\s*[0-2]?[0-9]?,)";

        // Act
        var files_with_dark = 0;
        foreach (var cssFile in cssFiles)
        {
            var content = ReadCssFile(cssFile);
            if (Regex.IsMatch(content, darkColorPattern, RegexOptions.IgnoreCase))
            {
                files_with_dark++;
            }
        }

        // Assert
        Assert.True(files_with_dark >= 2, $"Expected at least 2 CSS files with dark backgrounds, found {files_with_dark}");
    }

    private List<string> GetAllProjectCssFiles()
    {
        var cssFiles = new List<string>();
        var rootPath = GetProjectRoot();
        var socOpsPath = Path.Combine(rootPath, "SocOps");

        // Get app.css
        var appCss = Path.Combine(socOpsPath, "wwwroot", "css", "app.css");
        if (File.Exists(appCss))
            cssFiles.Add(appCss);

        // Get component CSS files
        var componentsPath = Path.Combine(socOpsPath, "Components");
        if (Directory.Exists(componentsPath))
        {
            cssFiles.AddRange(Directory.GetFiles(componentsPath, "*.razor.css"));
        }

        // Get layout CSS files
        var layoutPath = Path.Combine(socOpsPath, "Layout");
        if (Directory.Exists(layoutPath))
        {
            cssFiles.AddRange(Directory.GetFiles(layoutPath, "*.razor.css"));
        }

        return cssFiles;
    }

    private List<string> GetComponentCssFiles()
    {
        var cssFiles = new List<string>();
        var componentsPath = Path.Combine(GetProjectRoot(), "SocOps", "Components");
        if (Directory.Exists(componentsPath))
        {
            cssFiles.AddRange(Directory.GetFiles(componentsPath, "*.razor.css"));
        }
        return cssFiles;
    }

    [Fact]
    public void RetroTheme_DefinesMinimumColorCount()
    {
        // Arrange
        var appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
        var appCss = ReadCssFile(appCssPath);
        var requiredColors = new[] { "#00FFFF", "#FF1493", "#7B00FF", "#00FF00" };

        // Act
        var definedColors = requiredColors.Count(color => appCss.Contains(color, StringComparison.OrdinalIgnoreCase));

        // Assert
        Assert.True(definedColors >= 3, $"Expected at least 3 required colors defined, found {definedColors}");
    }

    [Fact]
    public void NeonGlowEffect_DefinedMultipleTimes_ForVariety()
    {
        // Arrange
        var appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
        var appCss = ReadCssFile(appCssPath);

        // Act
        var glowVariations = Regex.Matches(appCss,
            @"(\.glow-|\.shadow-.*glow|box-shadow:.*\d+px.*\d+px.*(?:#00FFFF|#FF1493|#7B00FF|#00FF00))",
            RegexOptions.IgnoreCase).Count;

        // Assert
        Assert.True(glowVariations >= 2, $"Expected at least 2 glow variations, found {glowVariations}");
    }
}
