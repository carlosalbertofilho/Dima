using MudBlazor;
using MudBlazor.Utilities;

namespace Dima.Web;

public static class Configuration
{
    public static string BackendUrl { get; set; } = "https://localhost:7064";
    public const string HttpClientName = "Dima";
    public static readonly MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new DefaultTypography()
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        },
        PaletteLight = new PaletteLight
        {
            Primary = new MudColor("#1EFA2D"),
            PrimaryContrastText = new MudColor("#000000"),
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Gray.Lighten4,
            AppbarBackground = new MudColor("#1EFA2D"),
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.White,
            DrawerBackground = Colors.Green.Darken4
        },
        PaletteDark = new PaletteDark()
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            // Background = Colors.LightGreen.Darken4,
            AppbarBackground = Colors.LightGreen.Accent3,
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = new MudColor("#000000")
        }
    };
}