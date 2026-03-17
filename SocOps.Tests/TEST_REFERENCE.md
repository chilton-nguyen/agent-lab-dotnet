# Retro Theme Test Suite - Complete Reference

## Quick Reference: All 88 Failing Tests

### RetroColorPaletteTests (11 tests)
Validates color palette definitions in `app.css`

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `AppCssFile_Contains_RetroColorVariables_Cyan` | `#00FFFF` cyan color in CSS |
| 2 | `AppCssFile_Contains_RetroColorVariables_HotPink` | `#FF1493` hot pink in CSS |
| 3 | `AppCssFile_Contains_RetroColorVariables_ElectricPurple` | `#7B00FF` purple in CSS |
| 4 | `AppCssFile_Contains_RetroColorVariables_LimeGreen` | `#00FF00` lime green in CSS |
| 5 | `AppCssFile_Contains_RetroColorVariables_DarkBackground` | Dark color for backgrounds |
| 6 | `AppCssFile_Contains_TextCyan_UtilityClass` | `.text-retro-cyan` or similar class |
| 7 | `AppCssFile_Contains_TextHotPink_UtilityClass` | `.text-retro-pink` or similar class |
| 8 | `AppCssFile_Contains_BackgroundRetroClasses` | `.bg-retro-*` or `.bg-neon-*` classes |
| 9 | `AppCssFile_Contains_BorderRetroClasses` | `.border-retro-*` or `.border-neon-*` classes |
| 10 | `AppCssFile_BgRetro_Dark_UsesCorrectColor` | `.bg-retro-dark` uses dark color value |
| 11 | `AppCssFile_TextRetroColors_UseNeonValues` | Text colors use neon values |

### RetroGlowEffectsTests (10 tests)
Validates neon glow effect implementations

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `AppCssFile_Contains_NeonGlowEffect_Class` | `.glow` or `.neon-glow` class exists |
| 2 | `AppCssFile_NeonGlowEffect_UsesCyanColor` | Glow effect uses cyan color |
| 3 | `AppCssFile_Contains_BoxShadowUtilityClasses_WithGlow` | Box-shadow glow utilities exist |
| 4 | `AppCssFile_Contains_TextShadowUtilityForGlow` | Text-shadow glow utilities exist |
| 5 | `AppCssFile_GlowEffect_DefinedWithBoxShadow` | Glow uses box-shadow property |
| 6 | `AppCssFile_Contains_CyanGlowClass` | `.glow-cyan` or similar exists |
| 7 | `AppCssFile_Contains_PinkGlowClass` | `.glow-pink` or similar exists |
| 8 | `AppCssFile_Contains_PurpleGlowClass` | `.glow-purple` or similar exists |
| 9 | `AppCssFile_GlowEffect_UsesMultipleLayers` | Glow has multiple shadow layers |
| 10 | `AppCssFile_GlowEffect_HasSpread` | Glow includes spread radius |

### RetroTypographyTests (10 tests)
Validates retro font and text styling

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `AppCssFile_DefinesRetroFont_InRootOrBodySelector` | Monospace font in html/body/root |
| 2 | `AppCssFile_DefinesHeadingRetroStyle` | h1-h6 heading styles exist |
| 3 | `AppCssFile_HeadingFontWeight_IsResponsive` | Heading font-weight or text-transform |
| 4 | `AppCssFile_Contains_MonospaceUtilityClass` | `.font-mono` or similar utility |
| 5 | `AppCssFile_ContainsRetroFontFamily_UtilityClasses` | Font family utilities (arcade, press-start, etc.) |
| 6 | `AppCssFile_DefinesPixelOrBlockyFontClass` | `.font-pixel` or `.font-8bit` class |
| 7 | `AppCssFile_Contains_LetterSpacingForRetro` | Letter-spacing property defined |
| 8 | `AppCssFile_TextShadowUsedForRetroEffect` | Text-shadow with pixel offset |
| 9 | `AppCssFile_Contains_AllCapsUtilityClass` | `.uppercase` or text-transform |
| 10 | `AppCssFile_FontSizeUtilityClasses_IncludeRetroSizes` | Large text sizes (3xl-7xl) |

### RetroComponentStylingTests (13 tests)
Validates component CSS files exist and contain retro styling

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `BingoBoardCssFile_Exists` | `BingoBoard.razor.css` file exists |
| 2 | `BingoSquareCssFile_Exists` | `BingoSquare.razor.css` file exists |
| 3 | `GameScreenCssFile_Exists` | `GameScreen.razor.css` file exists |
| 4 | `StartScreenCssFile_Exists` | `StartScreen.razor.css` file exists |
| 5 | `BingoSquareCssFile_ContainsRetroBackgroundColor` | Square uses retro background colors |
| 6 | `BingoSquareCssFile_ContainsHoverEffect` | `:hover` or `:active` states defined |
| 7 | `BingoSquareCssFile_HoverEffect_UsesRetroColor` | Hover changes to neon color |
| 8 | `BingoSquareCssFile_ContainsGlowEffect` | Box-shadow, text-shadow, or filter |
| 9 | `BingoBoardCssFile_ContainsGridStyling` | Grid layout styles exist |
| 10 | `BingoBoardCssFile_ContainsRetroGridBackground` | Grid has dark retro background |
| 11 | `GameScreenCssFile_ContainsRetroHeaderStyling` | Header element styled |
| 12 | `GameScreenCssFile_ContainsRetroButtonStyling` | Button element styled |
| 13 | `StartScreenCssFile_ContainsRetroDisplayText` | Font-size, color, background-color |

### RetroLayoutStylingTests (10 tests)
Validates layout component CSS files exist and use retro styling

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `MainLayoutCssFile_Exists` | `MainLayout.razor.css` file exists |
| 2 | `NavMenuCssFile_Exists` | `NavMenu.razor.css` file exists |
| 3 | `MainLayoutCssFile_ContainsRetroBackgroundColor` | Background has retro color |
| 4 | `MainLayoutCssFile_ContainsDarkThemeColors` | Dark theme colors present |
| 5 | `NavMenuCssFile_ContainsRetroStyling` | Background, color, border defined |
| 6 | `NavMenuCssFile_NavLinksUseRetroColors` | Navigation links styled |
| 7 | `NavMenuCssFile_HoverState_UsesRetroGlow` | Hover has glow or color change |
| 8 | `MainLayoutCssFile_ContainsRetroBackdrop` | Backdrop or pattern effect |
| 9 | `NavMenuCssFile_ActiveLink_HasRetroIndicator` | Active state indicator |
| 10 | `MainLayoutCssFile_ContainsRetroTextColor` | Text uses bright neon colors |

### RetroVisualEffectsTests (13 tests)
Validates retro visual effects like dithering and pixelation

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `AppCssFile_ContainsDitheredBackgroundEffect` | Dithering background effect |
| 2 | `AppCssFile_ContainsPixelOrBlockyEffect` | Pixel rendering or blocky style |
| 3 | `AppCssFile_ContainsImageRenderingPixelated` | `image-rendering: pixelated` |
| 4 | `AppCssFile_ContainsRepeatingPatternClass` | Repeating pattern utilities |
| 5 | `AppCssFile_ContainsAnimationClass` | `@keyframes` or animation classes |
| 6 | `AppCssFile_ContainsGlitchOrScanlineEffect` | Glitch or scanline effect |
| 7 | `AppCssFile_ContainerBgUsesDarkRetroColor` | Container background dark |
| 8 | `AppCssFile_TransitionClassesUseRetroTiming` | Transition or transform classes |
| 9 | `AppCssFile_ContainsBrightnessOrContrastEffect` | Filter effects defined |
| 10 | `AppCssFile_DefinesLineHeightForPixelFonts` | Line-height property set |
| 11 | `AppCssFile_ContainsOutlineFocusEffect` | `:focus` outline defined |
| 12 | `AppCssFile_FocusOutlineUsesRetroColor` | Focus uses neon color |
| 13 | `AppCssFile_ContainsScaleOrTransformEffect` | Transform/scale effects |

### RetroThemeConsistencyTests (8 tests)
Validates consistent application of retro theme across all files

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `AllComponentsAndLayoutsCss_UseRetroColorPalette` | Multiple files use retro colors |
| 2 | `AppCssGlobalFile_And_ComponentCssFiles_UseConsistentColorScheme` | Global and component CSS align |
| 3 | `RetroTheme_AppliedToAllGameComponents` | BingoSquare, Board, Screen have theme |
| 4 | `RetroTheme_AppliedToLayoutComponents` | MainLayout, NavMenu have theme |
| 5 | `GlowEffect_UsesConsistentBlurAndSpread` | Glow effects consistent |
| 6 | `DarkBackgroundColor_ConsistentallyDefined` | Dark backgrounds in multiple files |
| 7 | `RetroTheme_DefinesMinimumColorCount` | At least 3 colors defined |
| 8 | `NeonGlowEffect_DefinedMultipleTimes_ForVariety` | Multiple glow variations |

### RetroInteractiveElementsTests (13 tests)
Validates interactive elements have proper retro styling and state feedback

| # | Test Name | Expected to Validate |
|---|-----------|---------------------|
| 1 | `BingoSquareCssFile_ButtonElement_HasRetroHoverState` | Button hover state |
| 2 | `BingoSquareCssFile_HoverState_ChangesBackgroundToRetroColor` | Hover background color |
| 3 | `BingoSquareCssFile_ActiveState_HasRetroFeedback` | `:active` state feedback |
| 4 | `BingoSquareCssFile_FocusState_DefinesRetroOutline` | `:focus` state defined |
| 5 | `BingoSquareCssFile_FocusState_UsesRetroColor` | Focus uses neon color |
| 6 | `GameScreenCssFile_BackButton_HasRetroStyling` | Back button styled |
| 7 | `GameScreenCssFile_Header_ContainsRetroTextColor` | Header text color |
| 8 | `BingoSquareCssFile_DisabledState_HasRetrAppearance` | `:disabled` state |
| 9 | `AppCssFile_Contains_ButtonUtilityClasses` | Button utility classes |
| 10 | `AppCssFile_ButtonHoverState_DefinesTransition` | Button transition effect |
| 11 | `RetroTheme_ButtonStates_AllDefineRetroColors` | All button states use colors |
| 12 | `BingoSquareCssFile_MarkedSquare_UsesRetroHighlight` | Marked state highlight |
| 13 | `GameScreenCssFile_BingoNotification_HasRetroStyle` | Bingo notification styled |

## Test Execution Expected Output

When running `dotnet test SocOps.Tests/SocOps.Tests.csproj`, before implementation:

```
Failed SocOps.Tests [00:00:00.234]
  RetroColorPaletteTests
    ✗ AppCssFile_Contains_RetroColorVariables_Cyan [...]
    ✗ AppCssFile_Contains_RetroColorVariables_HotPink [...]
    ... (11 tests total)
  
  RetroGlowEffectsTests
    ✗ AppCssFile_Contains_NeonGlowEffect_Class [...]
    ... (10 tests total)
  
  RetroTypographyTests
    ✗ AppCssFile_DefinesRetroFont_InRootOrBodySelector [...]
    ... (10 tests total)
  
  RetroComponentStylingTests
    ✗ BingoBoardCssFile_Exists [...]
    ... (13 tests total)
  
  RetroLayoutStylingTests
    ✗ MainLayoutCssFile_Exists [...]
    ... (10 tests total)
  
  RetroVisualEffectsTests
    ✗ AppCssFile_ContainsDitheredBackgroundEffect [...]
    ... (13 tests total)
  
  RetroThemeConsistencyTests
    ✗ AllComponentsAndLayoutsCss_UseRetroColorPalette [...]
    ... (8 tests total)
  
  RetroInteractiveElementsTests
    ✗ BingoSquareCssFile_ButtonElement_HasRetroHoverState [...]
    ... (13 tests total)

Test Run Unsuccessful.
Total Tests: 88
     Passed: 0
     Failed: 88
     Skipped: 0
```

## File Structure

```
SocOps.Tests/
├── RetroThemeTests/
│   ├── RetroColorPaletteTests.cs           ← 11 color validation tests
│   ├── RetroGlowEffectsTests.cs            ← 10 glow effect tests
│   ├── RetroTypographyTests.cs             ← 10 typography tests
│   ├── RetroComponentStylingTests.cs       ← 13 component CSS tests
│   ├── RetroLayoutStylingTests.cs          ← 10 layout CSS tests
│   ├── RetroVisualEffectsTests.cs          ← 13 visual effect tests
│   ├── RetroThemeConsistencyTests.cs       ← 8 consistency tests
│   └── RetroInteractiveElementsTests.cs    ← 13 interactive element tests
├── SocOps.Tests.csproj                      ← Project file
├── README.md                                 ← Main documentation
├── TESTING_GUIDE.md                          ← Test execution guide
└── TEST_SUMMARY.md                           ← Complete reference (this file)
```

## CSS Files Expected to Be Modified/Created

### Modified:
- **SocOps/wwwroot/css/app.css** - Add retro color palette, glow effects, typography, and utilities

### To Be Created:
- **SocOps/Components/BingoSquare.razor.css** - Square button styling with retro colors and glow
- **SocOps/Components/BingoBoard.razor.css** - Grid styling with retro background and borders
- **SocOps/Components/GameScreen.razor.css** - Header, instructions, and game layout styling
- **SocOps/Components/StartScreen.razor.css** - Start screen display text styling
- **SocOps/Layout/MainLayout.razor.css** - Main layout container with retro background
- **SocOps/Layout/NavMenu.razor.css** - Navigation menu with retro links and hover states

## Implementation Checklist

Use this checklist while implementing the retro theme:

### Phase 1: Component CSS Files
- [ ] Create BingoSquare.razor.css with button styling
- [ ] Create BingoBoard.razor.css with grid styling
- [ ] Create GameScreen.razor.css with header and layout
- [ ] Create StartScreen.razor.css with display text
- [ ] Create MainLayout.razor.css with container styling
- [ ] Create NavMenu.razor.css with navigation styling

### Phase 2: Global Color Palette
- [ ] Add retro color variables/utilities to app.css
- [ ] Define cyan (#00FFFF) utilities
- [ ] Define hot pink (#FF1493) utilities
- [ ] Define electric purple (#7B00FF) utilities
- [ ] Define lime green (#00FF00) utilities
- [ ] Define dark background utilities

### Phase 3: Effects and Styling
- [ ] Add glow effect classes with box-shadow
- [ ] Add text-shadow glow utilities
- [ ] Add retro typography (monospace fonts)
- [ ] Add animation/transition effects
- [ ] Add visual effects (dithering, pixelation)
- [ ] Add focus outline styling

### Phase 4: Component Styling
- [ ] Add hover/active states to BingoSquare.razor.css
- [ ] Add marked square highlighting
- [ ] Add game screen header styling
- [ ] Add navigation link styling
- [ ] Add button styling with states
- [ ] Add notification/alert styling

### Phase 5: Polish & Consistency
- [ ] Verify all tests pass
- [ ] Optimize CSS performance
- [ ] Remove duplication
- [ ] Ensure cross-browser compatibility
- [ ] Test responsive behavior

---

**Created**: March 17, 2026  
**Test Framework**: xUnit  
**Total Failing Tests**: 88  
**Target**: All tests green ✓ after implementation
