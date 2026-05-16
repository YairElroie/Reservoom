---
name: dialog-family-conventions
description: Visual and code conventions for the SuccessDialog/ErrorDialog/InfoDialog family — use when reviewing or authoring any new dialog variant
metadata:
  type: project
---

Dialogs in Reservoom use code-behind only (no ViewModel). They share these structural conventions:

**Window attributes:** `WindowStyle="None"`, `AllowsTransparency="True"`, `WindowStartupLocation="CenterScreen"`, `ResizeMode="NoResize"`, `Background="Transparent"`, fixed `Height="220" Width="360"`.

**Root layout:** Single `<Border>` with `CornerRadius="10"`, `Padding="30"`, a `<DropShadowEffect BlurRadius="20" Opacity="0.2" ShadowDepth="0"/>`, and a `<StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">`.

**Content order:** Icon TextBlock (FontSize=40, FontWeight=Bold, accent color) → Title TextBlock (FontSize=18, FontWeight=SemiBold, Margin="0,10,0,5") → Message TextBlock (FontSize=13, Foreground=#666666, TextAlignment=Center) → OK Button (Width=90, Margin="0,20,0,0", Padding="10,6").

**Button template pattern:** Two nested Borders (Shadow/Face) with press-down animation (Margin="0,0,0,4" → "0,4,0,0" on IsPressed), IsMouseOver darkens Face background. Foreground=White, BorderThickness=0.

**Per-dialog themes:**
- SuccessDialog: Background=#F0FFF4, accent=#4CAF50, button Face=#E91E8C (pink — intentional brand color, NOT green), Shadow=#9C1256, Hover=#C2185B. Title is hardcoded "Reservation Made!". No x:Name on title.
- ErrorDialog: Background=#FFF0F0, accent=#F44336, button Face=#F44336, Shadow=#B71C1C, Hover=#D32F2F. Title hardcoded, only MessageText has x:Name.
- InfoDialog: Background=#F0F8FF, accent=#2196F3, button Face=#2196F3, Shadow=#1565C0, Hover=#1976D2. Both TitleText and MessageText have x:Name (most flexible variant).

**Code-behind pattern:** Constructor accepts `string message` (and optionally `string title`) with defaults; sets named TextBlock `.Text` directly after `InitializeComponent()`. `OkButton_Click` calls `Close()`.

**Known gaps in the family (recorded for future review):**
- No keyboard handling: Escape key does not close dialogs; Enter does not activate OK button.
- No `AutomationProperties.Name` on icon TextBlocks (screen-reader gap).
- Button `Background` property set on the element is redundant when a full ControlTemplate override is present — the template ignores `TemplateBinding Background` because it hardcodes colors.
- SuccessDialog button is pink (#E91E8C), not green — deliberate brand choice, not a bug.
- No `IsDefault="True"` on OK button; no `DialogResult` set before Close() which limits `ShowDialog()` return value usage.
- Fixed window size (220x360) may clip long dynamic messages.
