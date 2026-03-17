# Retro Theme Implementation - Failing Tests

This directory contains comprehensive xUnit tests for implementing a retro stylistic theme in the SocOps Blazor WebAssembly application.

## Test Structure

The tests are organized into focused test classes to validate different aspects of the retro theme:

### 1. RetroColorPaletteTests.cs
Tests that verify the retro color palette is properly defined and used throughout the application.

**Key Assertions:**
- Neon color variables exist: `#00FFFF` (cyan), `#FF1493` (hot pink), `#7B00FF` (electric purple), `#00FF00` (lime green)
- Dark background colors defined for retro aesthetic
- Color utility classes exist (e.g., `.text-retro-cyan`, `.bg-retro-dark`)
- Border color utility classes for retro styling
- At least 3 of 4 required retro colors are defined

**Expected Failing Tests:**
- 11 tests will fail until color palette is implemented

### 2. RetroGlowEffectsTests.cs
Tests that verify neon glow effects are properly implemented throughout the application.

**Key Assertions:**
- Glow effect classes exist (`.glow`, `.neon-glow`, `.retro-glow`)
- Box-shadow property used for glow effects
- Multiple shadow layers for depth
- Glow variants for each neon color (cyan, pink, purple, lime)
- Text-shadow glow effects defined
- Filter-based glow classes available

**Expected Failing Tests:**
- 10 tests will fail until glow effects are implemented

### 3. RetroTypographyTests.cs
Tests that verify retro typography with monospace/pixel-style fonts.

**Key Assertions:**
- Retro font family defined in root/body selector
- Heading styles defined with bold weight
- Monospace utility classes available
- Retro font family utility classes (arcade, press-start, orbitron, courier)
- Pixel/blocky font styling
- Letter-spacing for retro effect
- Text-shadow for text styling
- Uppercase text utility
- Large text sizes available

**Expected Failing Tests:**
- 10 tests will fail until typography is implemented

### 4. RetroComponentStylingTests.cs
Tests that verify individual game components have retro styling.

**Key Assertions:**
- Component CSS files exist:
  - `BingoSquare.razor.css`
  - `BingoBoard.razor.css`
  - `GameScreen.razor.css`
  - `StartScreen.razor.css`
- BingoSquare uses retro background colors
- BingoSquare has hover/active effects
- BingoSquare contains glow effects
- BingoBoard has grid styling
- BingoBoard has retro background
- GameScreen has header and button styling
- StartScreen has display text styling
- Marked squares use retro colors
- Components collectively use all retro colors

**Expected Failing Tests:**
- 13 tests will fail until component CSS files and styling are created

### 5. RetroLayoutStylingTests.cs
Tests that verify layout components use retro styling.

**Key Assertions:**
- Layout CSS files exist:
  - `MainLayout.razor.css`
  - `NavMenu.razor.css`
- MainLayout has retro background and text colors
- NavMenu contains retro link styling
- Navigation hover states use retro glow
- NavMenu has active link indicator
- NavMenu has border styling
- Consistent dark theme across layouts

**Expected Failing Tests:**
- 10 tests will fail until layout CSS files and styling are created

### 6. RetroVisualEffectsTests.cs
Tests that verify retro visual effects like dithering and pixelation.

**Key Assertions:**
- Dithered background effect classes
- Pixel/blocky text rendering
- Image-rendering property set to pixelated/crisp-edges
- Repeating pattern classes
- Animation classes defined
- Glitch or scanline effects
- Dark container background
- Transition and transform classes
- Brightness/contrast filter effects
- Focus outline in retro colors
- Scale/transform effects
- Color animations with retro colors

**Expected Failing Tests:**
- 13 tests will fail until visual effects are implemented

### 7. RetroThemeConsistencyTests.cs
Tests that verify consistent application of the retro theme across all files.

**Key Assertions:**
- All CSS files use retro color palette
- Global app.css defines core colors
- Game components (BingoSquare, BingoBoard, GameScreen) have retro theme
- Layout components have retro theme
- Glow effects used consistently
- Dark backgrounds defined in multiple files
- Retro colors defined in app.css
- Multiple glow variations for variety

**Expected Failing Tests:**
- 8 tests will fail until full theme consistency is achieved

### 8. RetroInteractiveElementsTests.cs
Tests that verify interactive elements have retro styling and proper state feedback.

**Key Assertions:**
- BingoSquare button has retro styling
- Hover state changes background to retro color
- Active state has retro feedback
- Focus state uses retro outline
- GameScreen back button has retro styling
- Header text uses retro color
- Disabled state has retro appearance
- Button utility classes available
- Transition effects for buttons
- Multiple button states define retro colors
- Marked squares use retro highlight
- Bingo notification has retro style
- Cursor property defined

**Expected Failing Tests:**
- 13 tests will fail until interactive element styling is implemented

## Total Expected Failing Tests: 88

All 88 tests are designed to fail when run against the current codebase because:
1. CSS files for components and layouts don't exist yet
2. Retro color palette is not defined in app.css
3. Glow effects are not implemented
4. Retro typography is not defined
5. Visual effects like dithering are not implemented
6. Interactive element states don't have retro styling

## Running the Tests

### Build the Test Project
```bash
dotnet build SocOps.Tests/SocOps.Tests.csproj
```

### Run All Tests
```bash
dotnet test SocOps.Tests/SocOps.Tests.csproj
```

### Run Specific Test Class
```bash
dotnet test SocOps.Tests/SocOps.Tests.csproj --filter "FullyQualifiedName~RetroColorPaletteTests"
```

### Run with Verbose Output
```bash
dotnet test SocOps.Tests/SocOps.Tests.csproj -v detailed
```

## Test Implementation Details

### File Path Resolution
Tests use a robust `GetProjectRoot()` method that:
- Starts from the current test directory
- Walks up the directory tree until it finds `.git` folder
- This ensures tests work regardless of how they're executed

### CSS File Reading
A helper method `ReadCssFile()` reads CSS content with proper error handling for:
- Missing files (throws FileNotFoundException)
- Proper path resolution across platforms

### Pattern Matching
Tests use `Regex.Matches()` with:
- Case-insensitive matching via `RegexOptions.IgnoreCase`
- Multiline support for complex CSS rules
- Flexible patterns that accept variations in formatting

## Expected Implementation

The tests expect the following files to be created:
- `SocOps/wwwroot/css/app.css` (modified to add retro theme utilities)
- `SocOps/Components/BingoSquare.razor.css` (new)
- `SocOps/Components/BingoBoard.razor.css` (new)
- `SocOps/Components/GameScreen.razor.css` (new)
- `SocOps/Components/StartScreen.razor.css` (new)
- `SocOps/Layout/MainLayout.razor.css` (new)
- `SocOps/Layout/NavMenu.razor.css` (new)

And modifications to these files to include:
- Retro color palette definitions
- Neon glow effects
- Retro typography styling
- Visual effects (dithering, pixelation)
- Interactive element states
- Component-specific retro styling
- Consistent dark theme

## Debugging Tests

If a test fails unexpectedly, check:
1. CSS file exists at the expected path
2. CSS content contains expected patterns (accounting for formatting variations)
3. Regex pattern is flexible enough for the CSS format used
4. File paths are correct for the operating system

Use `dotnet test --diag` for detailed diagnostic output.

## Notes

- Tests focus on CSS file presence and content validation
- Tests use flexible regex patterns to handle CSS formatting variations
- Tests verify both individual elements and consistency across the application
- Tests follow xUnit conventions with public test classes and theory/fact attributes
