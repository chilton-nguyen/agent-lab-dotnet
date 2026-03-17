using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SocOps.Tests.RetroThemeTests;

public class RetroVisualEffectsTests
{
    private readonly string _appCssPath;

    public RetroVisualEffectsTests()
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
    public void AppCssFile_ContainsDitheredBackgroundEffect()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"(\.bg-dither|\.pattern-dither|background-image:\s*(?:repeating-linear-gradient|repeating-radial-gradient))",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsPixelOrBlockyEffect()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"(\.pixel|\.blocky|image-rendering:\s*(?:pixelated|crisp-edges)|font-family:.*mono)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsImageRenderingPixelated()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"image-rendering:\s*(?:pixelated|crisp-edges|-webkit-optimize-contrast|auto)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsRepeatingPatternClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"\.pattern-|background-image:\s*repeating-(?:linear|radial)-gradient",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsAnimationClass()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"@keyframes|animation:|\.animate-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsGlitchOrScanlineEffect()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"(\.glitch|\.scanline|background-image:.*repeating-linear-gradient\(.*90deg|animation.*infinite)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainerBgUsesDarkRetroColor()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"(\.container|\.main|body|html)\s*\{[\s\S]*?background-color:\s*(?:#0{6}|#1[0-9a-f]{5}|rgb\(\s*[0-2]?[0-9]?)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_TransitionClassesUseRetroTiming()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"transition:|\.transition-|transform:",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsBrightnessOrContrastEffect()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"filter:.*(?:brightness|contrast|saturate|hue-rotate)|\.filter-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_DefinesLineHeightForPixelFonts()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"line-height:\s*[0-9.]+|\.leading-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsOutlineFocusEffect()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@":focus|outline:|\.focus-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_FocusOutlineUsesRetroColor()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@":focus\s*\{[\s\S]*?(?:outline|box-shadow|border-color):\s*(?:#00FFFF|#FF1493|#00FF00|#7B00FF)",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsScaleOrTransformEffect()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"transform:|scale\(|rotate\(|skew\(|translate\(",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_DefinedBackdropFilterClasses()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"backdrop-filter:|filter:|\\.(backdrop|filter)-",
            cssContent, RegexOptions.IgnoreCase);
    }

    [Fact]
    public void AppCssFile_ContainsRetroColorAnimation()
    {
        // Arrange
        var cssContent = ReadCssFile();

        // Act & Assert
        Assert.Matches(@"@keyframes[\s\S]*?(?:background-color|color|box-shadow)",
            cssContent, RegexOptions.IgnoreCase);
    }
}
