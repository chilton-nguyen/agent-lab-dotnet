using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroInteractiveElementsTests
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
    public void BingoSquareCssFile_ButtonElement_HasRetroHoverState()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"button\s*\{", cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_HoverState_ChangesBackgroundToRetroColor()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@":hover\s*\{[\s\S]*?background-color:\s*(?:#00FFFF|#FF1493|#7B00FF|#00FF00)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_ActiveState_HasRetroFeedback()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@":active\s*\{|\\[aria-pressed=true\\]\s*\{",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_FocusState_DefinesRetroOutline()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@":focus\s*\{|:focus-visible\s*\{",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_FocusState_UsesRetroColor()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"(:focus|:focus-visible)\s*\{[\s\S]*?(?:outline|box-shadow|border).*(?:#00FFFF|#FF1493|#7B00FF|#00FF00)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void GameScreenCssFile_BackButton_HasRetroStyling()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "GameScreen.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"button\s*\{|\..*btn|\.back-button",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void GameScreenCssFile_Header_ContainsRetroTextColor()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "GameScreen.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"header\s*\{[\s\S]*?color:\s*(?:#00FFFF|#FF1493|#7B00FF|#00FF00|#[fF]{2}[fF]{2}[fF]{2})",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void BingoSquareCssFile_DisabledState_HasRetrAppearance()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@":disabled|\\[disabled\\]|button:disabled",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_Contains_ButtonUtilityClasses()
    {
        // Arrange
        var appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
        var cssContent = ReadCssFile(appCssPath);

        // Act & Assert
        Assert.Matches(@"\.btn|button|\.button",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ButtonHoverState_DefinesTransition()
    {
        // Arrange
        var appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
        var cssContent = ReadCssFile(appCssPath);

        // Act & Assert
        Assert.Matches(@"transition:|\.transition-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void RetroTheme_ButtonStates_AllDefineRetroColors()
    {
        // Arrange
        var componentCssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(componentCssPath);
        var buttonStates = new[] { ":hover", ":active", ":focus", ":disabled" };
        var statesWithRetroColors = 0;

        // Act
        foreach (var state in buttonStates)
        {
            var pattern = $@"{state}\s*\{{[\s\S]*?(?:color|background-color|border|box-shadow):\s*(?:#00FFFF|#FF1493|#7B00FF|#00FF00|#[fF]{{2}}[fF]{{2}}[fF]{{2}})";
            if (Regex.IsMatch(cssContent, pattern, RegexOptions.IgnoreCase))
            {
                statesWithRetroColors++;
            }
        }

        // Assert
        Assert.True(statesWithRetroColors >= 2, $"Expected at least 2 button states with retro colors, found {statesWithRetroColors}");
    }

    [Fact]
    public void BingoSquareCssFile_MarkedSquare_UsesRetroHighlight()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "BingoSquare.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"\.marked|\\[aria-pressed=true\\]|\.selected",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void GameScreenCssFile_BingoNotification_HasRetroStyle()
    {
        // Arrange
        var cssPath = Path.Combine(GetProjectRoot(), "SocOps", "Components", "GameScreen.razor.css");
        var cssContent = ReadCssFile(cssPath);

        // Act & Assert
        Assert.Matches(@"\.bingo|\.notification|\.alert|\.success",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_CursorProperty_DefinedForInteractiveElements()
    {
        // Arrange
        var appCssPath = Path.Combine(GetProjectRoot(), "SocOps", "wwwroot", "css", "app.css");
        var cssContent = ReadCssFile(appCssPath);

        // Act & Assert
        Assert.Matches(@"cursor:|pointer",
            cssContent, RegexOptions.IgnoreCase);
    }
}
