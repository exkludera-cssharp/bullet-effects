using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Utils;
using System.Drawing;

internal class Utils
{
    public static bool HasPermission(CCSPlayerController player, List<string> Permissions, string Team)
    {
        return PermissionCheck(player, Permissions) && AllowedTeam(player, Team);
    }

    public static bool PermissionCheck(CCSPlayerController player, List<string> Permissions)
    {
        if (Permissions.Count <= 0 || Permissions.All(string.IsNullOrEmpty))
            return true;

        foreach (string perm in Permissions)
        {
            if (perm.StartsWith("@") && AdminManager.PlayerHasPermissions(player, perm))
                return true;
            if (perm.StartsWith("#") && AdminManager.PlayerInGroup(player, perm))
                return true;
        }

        return false;
    }

    public static bool AllowedTeam(CCSPlayerController player, string Team)
    {
        Team = Team.ToLower();

        bool isTeamValid =
            (Team == "t" || Team == "terrorist") && player.Team == CsTeam.Terrorist ||
            (Team == "ct" || Team == "counterterrorist") && player.Team == CsTeam.CounterTerrorist ||
            (Team == "" || Team == "both" || Team == "all");

        return isTeamValid;
    }

    public static Vector GetEyePosition(CCSPlayerController player)
    {
        Vector absorigin = player.PlayerPawn.Value!.AbsOrigin!;
        CPlayer_CameraServices camera = player.PlayerPawn.Value!.CameraServices!;

        return new Vector(absorigin.X, absorigin.Y, absorigin.Z + camera.OldPlayerViewOffsetZ);
    }

    private static int colorIndex = 0;
    public static Color ParseColor(string colorValue)
    {
        if (string.IsNullOrEmpty(colorValue) || colorValue.ToLower() == "random")
        {
            var color = rainbowColors[colorIndex];
            colorIndex = (colorIndex + 1) % rainbowColors.Length;
            return color;
        }
        var colorParts = colorValue.Split(' ');
        if (colorParts.Length == 3 &&
            int.TryParse(colorParts[0], out var r) &&
            int.TryParse(colorParts[1], out var g) &&
            int.TryParse(colorParts[2], out var b))
        {
            return Color.FromArgb(255, r, g, b);
        }
        return Color.FromArgb(255, 255, 255, 255);
    }

    static Color[] rainbowColors = {
        Color.FromArgb(255, 255, 255, 255), // White
        Color.FromArgb(255, 255, 0, 0),     // Red
        Color.FromArgb(255, 255, 0, 255),   // Magenta
        Color.FromArgb(255, 255, 255, 0),   // Yellow
        Color.FromArgb(255, 0, 255, 0),     // Green
        Color.FromArgb(255, 0, 255, 255),   // Cyan
        Color.FromArgb(255, 0, 0, 255)      // Blue
    };
}