using System;
using System.Text;
using System.Collections.Generic;

namespace ConsoleRPGAdventure;

public abstract class AsciiImage
{
    public string Content { get; }

    protected AsciiImage(string content)
    {
        Content = content;
    }
}


public class SingleColorImage : AsciiImage
{
    public ConsoleColor Color { get; }

    public SingleColorImage(string content, ConsoleColor color=ConsoleColor.White) : base(content)
    {
        Color = color;
    }
}


public class MultiColorImage : AsciiImage
{
    public Dictionary<char, ConsoleColor> ColorMap { get; }

    public MultiColorImage(string content, Dictionary<char, ConsoleColor> colorMap) : base(content)
    {
        ColorMap = colorMap;
    }
}


public class Frame
{
    private const int InnerWidth = 83;
    private const int InnerHeight = 25;
    private const int StartX = 4;
    private const int StartY = 2;
    private const string AnsiReset = "\x1b[0m";     

    private AsciiImage _currentImage;

    public Frame()
    {
        _currentImage = null;
    }

    public void SetImage(AsciiImage image)
    {
        _currentImage = image;
    }

    public void ClearImage()
    {
        _currentImage = null;
    }

    // Displays the current frame 
    public List<string> GetDisplayLines()
    {
        string frame = GenerateFrame();
        string result;

        if (_currentImage == null)
        {
            result = frame;
        }
        else
        {
            result = _currentImage switch
            {
                SingleColorImage sci => OverlayAsciiArt(frame, sci.Content, StartX, StartY, sci.Color),
                MultiColorImage mci => OverlayMultiColorAsciiArt(frame, mci.Content, StartX, StartY, mci.ColorMap),
                _ => frame
            };
        }
        
        return SplitIntoLines(result);
    }

    private static string GenerateFrame()
    {
        var sb = new StringBuilder();

        // Top border of the frame
        sb.Append("                ╒").Append('═', InnerWidth + 4).AppendLine("╕");
        sb.Append("                │ ╔").Append('═', InnerWidth).AppendLine("╗ │");

        // Middle, empty section
        for (int i = 0; i < InnerHeight; i++)
        {
            sb.Append("                │ ║").Append(' ', InnerWidth).AppendLine("║ │");
        }

        // Bottom border of the frame
        sb.Append("                │ ╚").Append('═', InnerWidth).AppendLine("╝ │");
        sb.Append("                ╘").Append('═', InnerWidth + 4).AppendLine("╛");

        return sb.ToString();
    }

    private static string OverlayAsciiArt(string background, string foreground, int x, int y, ConsoleColor? color = null)
    {
        string[] backgroundLines = SplitIntoLines(background).ToArray();
        string[] foregroundLines = SplitIntoLines(foreground).ToArray();

        string colorCode = color.HasValue ? GetAnsiColorCode(color.Value) : null;

        for (int i = 0; i < foregroundLines.Length; i++)
        {
            int targetY = y + i;
            if (targetY < 0 || targetY >= backgroundLines.Length) continue;

            var sb = new StringBuilder();
            char[] backgroundLineChars = backgroundLines[targetY].ToCharArray();
            bool inColoredSection = false;

            for (int charIndex = 0; charIndex < backgroundLineChars.Length; charIndex++)
            {
                int foregroundIndex = charIndex - x;
                char currentChar = backgroundLineChars[charIndex];

                // Check if we're in the foreground range
                if (foregroundIndex >= 0 && foregroundIndex < foregroundLines[i].Length)
                {
                    char foregroundChar = foregroundLines[i][foregroundIndex];

                    // Preserve frame border characters
                    if (foregroundChar == ' ' && IsFrameCharacter(currentChar))
                    {
                        if (inColoredSection && colorCode != null)
                        {
                            sb.Append(AnsiReset);
                            inColoredSection = false;
                        }
                        sb.Append(currentChar);
                    }
                    else
                    {
                        // Start color if needed and not already colored
                        if (colorCode != null && !inColoredSection && foregroundChar != ' ')
                        {
                            sb.Append(colorCode);
                            inColoredSection = true;
                        }
                        sb.Append(foregroundChar);
                    }
                }
                else
                {
                    // Outside foreground range - close color if open
                    if (inColoredSection && colorCode != null)
                    {
                        sb.Append(AnsiReset);
                        inColoredSection = false;
                    }
                    sb.Append(currentChar);
                }
            }

            // Reset color at end of line if still active
            if (inColoredSection && colorCode != null)
            {
                sb.Append(AnsiReset);
            }

            backgroundLines[targetY] = sb.ToString();
        }

        return string.Join(Environment.NewLine, backgroundLines);
    }

    private static string OverlayMultiColorAsciiArt(string background, string foreground, int x, int y, Dictionary<char, ConsoleColor> colorMap)
    {
        string[] backgroundLines = SplitIntoLines(background).ToArray();
        string[] foregroundLines = SplitIntoLines(foreground).ToArray();

        for (int i = 0; i < foregroundLines.Length; i++)
        {
            int targetY = y + i;
            if (targetY < 0 || targetY >= backgroundLines.Length) continue;

            var sb = new StringBuilder();
            char[] backgroundLineChars = backgroundLines[targetY].ToCharArray();
            string currentColorCode = null;

            for (int charIndex = 0; charIndex < backgroundLineChars.Length; charIndex++)
            {
                int foregroundIndex = charIndex - x;
                char currentChar = backgroundLineChars[charIndex];

                // Check if we're in the foreground range
                if (foregroundIndex >= 0 && foregroundIndex < foregroundLines[i].Length)
                {
                    char foregroundChar = foregroundLines[i][foregroundIndex];

                    // Preserve frame border characters
                    if (foregroundChar == ' ' && IsFrameCharacter(currentChar))
                    {
                        ResetColorIfActive(sb, ref currentColorCode);
                        sb.Append(currentChar);
                    }
                    else if (foregroundChar != ' ')
                    {
                        // Apply color based on character
                        string newColorCode = null;
                        if (colorMap != null && colorMap.TryGetValue(foregroundChar, out ConsoleColor color))
                        {
                            newColorCode = GetAnsiColorCode(color);
                        }

                        // Change color if needed
                        if (newColorCode != currentColorCode)
                        {
                            ResetColorIfActive(sb, ref currentColorCode);
                            if (newColorCode != null)
                            {
                                sb.Append(newColorCode);
                                currentColorCode = newColorCode;
                            }
                        }

                        sb.Append(foregroundChar);
                    }
                    else
                    {
                        // Space character
                        ResetColorIfActive(sb, ref currentColorCode);
                        sb.Append(foregroundChar);
                    }
                }
                else
                {
                    // Outside foreground range
                    ResetColorIfActive(sb, ref currentColorCode);
                    sb.Append(currentChar);
                }
            }

            // Reset color at end of line if still active
            ResetColorIfActive(sb, ref currentColorCode);

            backgroundLines[targetY] = sb.ToString();
        }

        return string.Join(Environment.NewLine, backgroundLines);
    }

    // Helper method to avoid code duplication
    private static void ResetColorIfActive(StringBuilder sb, ref string currentColorCode)
    {
        if (currentColorCode != null)
        {
            sb.Append(AnsiReset);
            currentColorCode = null;
        }
    }

    // Extracted common string splitting logic
    private static List<string> SplitIntoLines(string text)
    {
        return new List<string>(text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
    }

    private static string GetAnsiColorCode(ConsoleColor color)
    {
        // Note: I could've have just did "Red" "Blue" as arguments, but I decided to use ConsoleColor as to verify is such color is available.
        return color switch
        {
            ConsoleColor.Black => "\x1b[30m",
            ConsoleColor.DarkRed => "\x1b[31m",
            ConsoleColor.DarkGreen => "\x1b[32m",
            ConsoleColor.DarkYellow => "\x1b[33m",
            ConsoleColor.DarkBlue => "\x1b[34m",
            ConsoleColor.DarkMagenta => "\x1b[35m",
            ConsoleColor.DarkCyan => "\x1b[36m",
            ConsoleColor.Gray => "\x1b[37m",
            ConsoleColor.DarkGray => "\x1b[90m",
            ConsoleColor.Red => "\x1b[91m",
            ConsoleColor.Green => "\x1b[92m",
            ConsoleColor.Yellow => "\x1b[93m",
            ConsoleColor.Blue => "\x1b[94m",
            ConsoleColor.Magenta => "\x1b[95m",
            ConsoleColor.Cyan => "\x1b[96m",
            ConsoleColor.White => "\x1b[97m",
            _ => "\x1b[0m"
        };
    }

    private static bool IsFrameCharacter(char c)
    {
        return c == '║' || c == '│' || c == '╒' || c == '╕' ||
               c == '╘' || c == '╛' || c == '╔' || c == '╗' ||
               c == '╚' || c == '╝' || c == '═';
    }
}

