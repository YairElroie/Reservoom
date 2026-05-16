## Task

Create a WPF dialog Window component following this project's conventions:

- Create `Views/$ARGUMENTS.xaml` as a `Window` with `WindowStyle="None"`, `AllowsTransparency="True"`, `WindowStartupLocation="CenterScreen"`, `ResizeMode="NoResize"`
- Create `Views/$ARGUMENTS.xaml.cs` as the code-behind — a `partial class` extending `Window` with `InitializeComponent()` in the constructor
- Use a `<Border>` root element with `CornerRadius="10"`, `Padding="30"`, and a `<DropShadowEffect>` for styling, consistent with `SuccessDialog.xaml`
- Include a close/OK button that calls `Close()` via a click handler in the code-behind
- Do not create a ViewModel — dialogs in this project use code-behind only

## Variants

- **Info dialog** — neutral background (e.g. `#F0F8FF`), blue accent color
- **Success dialog** — green background (e.g. `#F0FFF4`), green icon (`✓`), green accent — see `Views/SuccessDialog.xaml` as the reference implementation
- **Error dialog** — red/pink background (e.g. `#FFF0F0`), red icon (`✕`), red accent color

## Testing

- Run `dotnet run` and trigger the dialog from the relevant UI action
- Confirm the dialog opens centered on screen
- Confirm the OK/Close button dismisses the dialog
- Confirm no errors in the build output (`dotnet build`)

## Review the Work

- **Invoke the wpf-ux-reviewer subagent** to review your work and implement suggestions where needed
