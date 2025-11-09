namespace ConsoleRPGAdventure
{
    public class MapDisplay
    {
        private readonly Frame _frame;
        private readonly MiniMap _miniMap;

        public MapDisplay()
        {
            _frame = new Frame();
            _miniMap = new MiniMap();
        }


        public void ShowGameScreen(Player player, Map currentMap)
        {
            Console.Clear();
            
            for (int i =  0; i < 1; i++) { Console.WriteLine(); } // <-- for spacing

            _frame.SetImage(player.CurrentLocation.BackgroundArt);
            List<string> frameLines = _frame.GetDisplayLines();
            List<string> miniMapLines = _miniMap.GenerateMapLines(currentMap, player);

            ShowFrameAndMiniMap(frameLines, miniMapLines);
        }


        private void ShowFrameAndMiniMap(List<string> leftLines, List<string> rightLines, string separator = "     ")
        {
            int leftWidth = leftLines.Any() ? leftLines[0].Length : 0;
            int maxLines = Math.Max(leftLines.Count, rightLines.Count);

            for (int i = 0; i < maxLines; i++)
            {
                // Get the line from the frame, or pad with spaces if it's shorter
                string leftLine = i < leftLines.Count ? leftLines[i] : new string(' ', leftWidth);

                // Get the line from the map, or an empty string if it's shorter
                string rightLine = i < rightLines.Count ? rightLines[i] : "";

                // Print the lines together with a separator
                Console.WriteLine($"{leftLine}{separator}{rightLine}");
            }
        }


        public void ShowCutscene(AsciiImage art)
        {
            Console.Clear();

            for (int i = 0; i < 1; i++) { Console.WriteLine(); } // <-- for spacing

            _frame.SetImage(art);
            List<string> frameLines = _frame.GetDisplayLines();

            foreach (string line in frameLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}