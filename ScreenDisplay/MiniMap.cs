using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleRPGAdventure;

public class MiniMap
{
    private const string GreenColor = "\x1b[92m";
    private const string ResetColor = "\x1b[0m";

    // Get the apt symbol for the minimap
    private string GetMapSymbolForArea(Area area)
    {
        // Determine which exits are available
        bool hasNorth = area.North != null;
        bool hasSouth = area.South != null;
        bool hasEast = area.East != null;
        bool hasWest = area.West != null;

        // Hardcoded lines depending on area exits
        if (hasNorth && hasSouth && hasEast && hasWest) return " ┼ "; 
        if (hasSouth && hasEast && hasWest) return " ┬ ";
        if (hasNorth && hasEast && hasWest) return " ┴ ";
        if (hasNorth && hasSouth && hasWest) return " ┤ ";
        if (hasNorth && hasSouth && hasEast) return " ├ ";
        if (hasNorth && hasSouth) return " │ ";
        if (hasEast && hasWest) return " ─ ";
        if (hasSouth && hasEast) return " ┌ ";
        if (hasSouth && hasWest) return " ┐ ";
        if (hasNorth && hasEast) return " └ ";
        if (hasNorth && hasWest) return " ┘ ";
        if (hasNorth || hasSouth) return " │ "; 
        if (hasEast || hasWest) return " ─ "; 

        return " · "; // isolated area with no exits (should not happen)
    }

    private int GetVisibleLength(string text)
    {
        return Regex.Replace(text, @"\x1b\[[0-9;]*m", "").Length;
    }

    public List<string> GenerateMapLines(Map map, Player player)
    {
        var areas = map.GetAreas().ToList();
        if (!areas.Any()) return new List<string>();

        var mapContentLines = new List<string>();
        int minX = areas.Min(a => a.X);
        int maxX = areas.Max(a => a.X);
        int minY = areas.Min(a => a.Y);
        int maxY = areas.Max(a => a.Y);

        for (int y = maxY; y >= minY; y--)
        {
            var lineBuilder = new StringBuilder();
            for (int x = minX; x <= maxX; x++)
            {
                Area? areaAtCoord = areas.FirstOrDefault(a => a.X == x && a.Y == y);

                if (areaAtCoord == null)
                {
                    lineBuilder.Append("   ");
                }
                else
                {
                    if (areaAtCoord == player.CurrentLocation)
                    {
                        lineBuilder.Append($"{GreenColor} @ {ResetColor}");
                    }
                    else
                    {
                        lineBuilder.Append(GetMapSymbolForArea(areaAtCoord));
                    }
                }
            }
            mapContentLines.Add(lineBuilder.ToString());
        }

        // Frame for the minimap
        var framedMapLines = new List<string>();
        int maxWidth = mapContentLines.Max(GetVisibleLength);
        framedMapLines.Add("╔" + new string('═', maxWidth) + "╗");
        foreach (var line in mapContentLines)
        {
            int visibleLength = GetVisibleLength(line);
            int padding = maxWidth - visibleLength;
            framedMapLines.Add("║" + line + new string(' ', padding) + "║");
        }
        framedMapLines.Add("╚" + new string('═', maxWidth) + "╝");

        return framedMapLines;
    }
}