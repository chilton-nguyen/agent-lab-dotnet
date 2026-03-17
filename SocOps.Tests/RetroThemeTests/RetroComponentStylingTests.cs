using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroComponentStylingTests
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

    private string GetComponentCssPath(string componentName)
    {
        return Path.Combine(GetProjectRoot(), "SocOps", "Components", $"{componentName}.razor.css");
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
    public void BingoBoardCssFile_Exists()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoBoard");

        // Act & Assert
        Assert.True(File.Exists(cssPath), $"BingoBoard.razor.css should exist at {cssPath}");
    }

    [Fact]
    public void BingoSquareCssFile_Exists()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoSquare");

        // Act & Assert
        Assert.True(File.Exists(cssPath), $"BingoSquare.razor.css should exist at {cssPath}");
    }

    [Fact]
    public void GameScreenCssFile_Exists()
    {
        // Arrange
        var cssPath = GetComponentCssPath("GameScreen");

        // Act & Assert
        Assert.True(File.Exists(cssPath), $"GameScreen.razor.css should exist at {cssPath}");
    }

    [Fact]
    public void StartScreenCssFile_Exists()
    {
        // Arrange
        var cssPath = GetComponentCssPath("StartScreen");

        // Act & Assert
        Assert.True(File.Exists(cssPath), $"StartScreen.razor.css should exist at {cssPath}");
    }

    [Fact]
    public void BingoSquareCssFile_ContainsRetroBackgroundColor()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoSquare");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"background-color:\s*(?:#00FFFF|#7B00FF|#00FF00|#FF1493|#0{6})|\.bg-retro-|\.bg-neon-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_ContainsHoverEffect()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoSquare");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@":hover\s*\{|:active\s*\{",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_HoverEffect_UsesRetroColor()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoSquare");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@":hover\s*\{[\s\S]*?(?:background-color|color|box-shadow):\s*(?:#00FFFF|#FF1493|#7B00FF|#00FF00)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_ContainsGlowEffect()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoSquare");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"box-shadow:|text-shadow:|filter:|\.glow",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoBoardCssFile_ContainsGridStyling()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoBoard");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"grid|display:.*grid|gap:|\.grid-cols|border",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoBoardCssFile_ContainsRetroGridBackground()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoBoard");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"background-color:\s*(?:#0{6}|#1[0-9a-f]{5}|#2[0-3][0-9a-f]{4})|background-image:\s*(?:repeating-|linear-gradient|radial-gradient)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void GameScreenCssFile_ContainsRetroHeaderStyling()
    {
        // Arrange
        var cssPath = GetComponentCssPath("GameScreen");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"header\s*\{|\.header|background-color:\s*(?:#0{6}|#7B00FF|#FF1493)|color:\s*(?:#00FFFF|#00FF00|#FF1493)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void GameScreenCssFile_ContainsRetroButtonStyling()
    {
        // Arrange
        var cssPath = GetComponentCssPath("GameScreen");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"button\s*\{|\.button|\.btn",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void StartScreenCssFile_ContainsRetroDisplayText()
    {
        // Arrange
        var cssPath = GetComponentCssPath("StartScreen");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"font-size:|color:|background-color:",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_MarkingStateUsesRetroColor()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoSquare");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        // When marked, square should have retro appearance
        Assert.Matches(@"(\[aria-pressed=true\]|\.marked|\.selected)\s*\{[\s\S]*?(?:background-color|color|box-shadow)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoBoardCssFile_ContainsGridBorderStyling()
    {
        // Arrange
        var cssPath = GetComponentCssPath("BingoBoard");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"border:|border-color:|border-width:",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void ComponentCssFiles_UseConsistentRetroColors()
    {
        // Arrange
        var retroColors = new[] { "#00FFFF", "#FF1493", "#7B00FF", "#00FF00" };
        var componentNames = new[] { "BingoSquare", "BingoBoard", "GameScreen" };
        var colorCount = 0;

        // Act
        foreach (var componentName in componentNames)
        {
            var cssPath = GetComponentCssPath(componentName);
            if (File.Exists(cssPath))
            {
                var cssContent = ReadCssFile(cssPath);
                foreach (var color in retroColors)
                {
                    if (cssContent.Contains(color, StringComparison.OrdinalIgnoreCase))
                    {
                        colorCount++;
                    }
                }
            }
        }

        // Assert
        Assert.True(colorCount >= 4, "Component CSS files should use at least 4 retro colors collectively");
    }
}
