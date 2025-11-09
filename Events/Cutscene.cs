using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    internal class Cutscene
    {
        // These are hard coded scenes
        public static Player Introduction(Map currentMap)
        {
            Player player;

            bool firstRun = true;
            string playerName;
            var display = new MapDisplay();

            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog("Hello, World!", 1);
            OutputHelper.CutsceneLog("...", 1);
            OutputHelper.CutsceneLog(".......", 1);
            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("Oh, hello there!",1);
            OutputHelper.CutsceneLog("Glad you're finally awake.");
            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog("........", 2);
            OutputHelper.CutsceneLog("I know you're probably confused right now...");
            OutputHelper.CutsceneLog("But please don't worry, I will explain things later...");

            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("So first... would you mind telling me your name?");

            while (true)
            {
                display.ShowCutscene(CutsceneImage.WizardDefault);
                OutputHelper.CutsceneLog("Here, write your name on this book.");

                // Show the open book and get player name
                ShowBook(CutsceneImage.Book, 19, 31);
                Console.SetCursorPosition(41, 35);
                Console.ForegroundColor = ConsoleColor.Cyan;
                playerName = Console.ReadLine();
                Console.ResetColor();

                // Close the book
                OutputHelper.CutsceneLog("Finished? Let me see it.");

                if (firstRun)
                {
                    EraseBook(CutsceneImage.Book, 19, 31);
                    ShowBook(CutsceneImage.ClosedBook, 19, 31);
                    OutputHelper.CutsceneLog("Can you kindly hand me the book back now please?", 2);
                    firstRun = false;
                }

                display.ShowCutscene(CutsceneImage.WizardRead);
                OutputHelper.CutsceneLog("...", 1);
                OutputHelper.CutsceneLog("...", 1);

                if (string.IsNullOrEmpty(playerName))
                {
                    OutputHelper.CutsceneLog("Hey, at least write something!");
                    continue;
                }
                else if (playerName.Length >= 20)
                {
                    OutputHelper.CutsceneLog("That name is a bit long, can I call you something shorter instead?");
                    continue;
                }
                else
                {
                    OutputHelper.CutsceneLog($"\"{playerName}\"", 1, ConsoleColor.Cyan);
                    OutputHelper.CutsceneLog("An interesting name, I guess.");
                }

                string decision = OutputHelper.CutsceneGetInput("Are you sure that's your name (Y/N)?").ToUpper().Trim();

                if (decision == "Y" || decision == "YES")
                {
                    break;
                }
                else
                {
                    OutputHelper.CutsceneLog("No?");
                    display.ShowCutscene(CutsceneImage.WizardPoint);
                    OutputHelper.CutsceneLog("Ok, let's try that again.");
                }
            }

            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog($"Well then, nice to meet you, {playerName}!");

            string explanationChoice;

            // Game Explanation
            OutputHelper.CutsceneLog("Now, I shall explain how the game works.");
            
            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("1. You can explore the map using the [ARROW KEYS].");
            display.ShowCutscene(CutsceneImage.WizardDefault);
            OutputHelper.CutsceneLog("But of course, you cannot move during a cutscene like this or while fighting.");
            OutputHelper.CutsceneLog("While exploring you can also press [S] to save.");
            OutputHelper.CutsceneLog("You probably know what happens if you die without saving, right? So keep that in mind.");

            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("2. You can encounter enemies while exploring.");
            display.ShowCutscene(CutsceneImage.WizardDefault);
            OutputHelper.CutsceneLog("You need to either slay them or run away.");
            OutputHelper.CutsceneLog("To fight the enemy, you need to timely hit their weakspots using [ENTER] or [SPACEBAR].");
            OutputHelper.CutsceneLog("Note that different enemies have different speed and size, making the combat difficult.");
            OutputHelper.CutsceneLog("Don't worry, there'll be items you can use to slow down or expose the enemy's weakspots.");
            OutputHelper.CutsceneLog("Such items are displayed on the right side of the combat screen with their [KEYBINDS].");
            OutputHelper.CutsceneLog("You can buy more items using the 'gold' you gain by slaying monsters.");

            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("Ah, speaking of combat. I will let you choose what role you take.");
            display.ShowCutscene(CutsceneImage.WizardDefault);
            OutputHelper.CutsceneLog("Each role has their advantages and disadvantages.");

            while (true)
            {
                OutputHelper.CutsceneLog("Here, I'll just let you read the details of each and choose from it.");

                ShowBook(CutsceneImage.RoleBook, 19, 31);
                Console.SetCursorPosition(40, 43);

                Console.ForegroundColor = ConsoleColor.Cyan;
                string roleChoice = Console.ReadLine().ToUpper().Trim();
                Console.ResetColor();

                switch (roleChoice)
                {
                    case "A":
                        player = new Swordsman(playerName, currentMap.StartingArea);
                        break;

                    case "B":
                        player = new Ranger(playerName, currentMap.StartingArea);
                        break;

                    case "C":
                        player = new Wizard(playerName, currentMap.StartingArea);
                        break;

                    case "IAMTHEBONEOFMYSWORD": // <-- tester
                        player = new Admin(playerName, currentMap.StartingArea);
                        break;

                    default:
                        display.ShowCutscene(CutsceneImage.WizardRead);
                        OutputHelper.CutsceneLog("Sorry, please only choose from the three roles (A, B, C) specified...");
                        continue;
                }
                EraseBook(CutsceneImage.RoleBook, 19, 31);
                break;
            }


            display.ShowCutscene(CutsceneImage.WizardRead);
            OutputHelper.CutsceneLog("...");
            OutputHelper.CutsceneLog("Oh, you chose that role huh?");
            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog($"A fine choice, {playerName}. Now let your journey begin!");
            OutputHelper.CutsceneLog("You are now about to be summoned to another world to defeat the Demon King!");

            return player;



            //OutputHelper.CutsceneLog("You know, I didn't know that Undertale had a similar theme and combat mechanic as this game.", 99);
            //OutputHelper.CutsceneLog("But please note that was unintentional because I haven't played that game before lol",99);
        }

        public static void ShowBook(AsciiImage bookImage, int startCol, int startRow)
        {
            string[] bookLines = bookImage.Content.Split('\n');

            for (int i = 0; i < bookLines.Length; i++)
            {
                Console.SetCursorPosition(startCol, startRow + i);

                if (bookImage is SingleColorImage singleColor)
                {
                    Console.ForegroundColor = singleColor.Color;
                    Console.Write(bookLines[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(bookLines[i]);
                }
            }
        }
        public static void EraseBook(AsciiImage bookImage, int startCol, int startRow)
        {
            string[] bookLines = bookImage.Content.Split('\n');

            for (int i = 0; i < bookLines.Length; i++)
            {
                Console.SetCursorPosition(startCol, startRow + i);
                Console.Write(new string(' ', bookLines[i].Length));
            }
        }
    
        
        // tester
        public static void ShowArt(AsciiImage art)
        {
            var display = new MapDisplay();
            display.ShowCutscene(art);
            Console.ReadKey();
            Console.Clear();
        }

        public static void FinalBossCutscene(Player player)
        {
            var display = new MapDisplay();

            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog("Oh, hello there again!", 1);
            OutputHelper.CutsceneLog("I see you've made it this far...");

            display.ShowCutscene(CutsceneImage.WizardDefault);
            OutputHelper.CutsceneLog("...", 1);
            OutputHelper.CutsceneLog($"Tell me, {player.Name}...", 1);

            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("How are you enjoying this 'game' so far?", 1);

            string response = OutputHelper.CutsceneGetInput("Did you enjoy it? (Y/N)").ToUpper().Trim();

            if (response == "Y" || response == "YES")
            {
                display.ShowCutscene(CutsceneImage.WizardGreeting);
                OutputHelper.CutsceneLog("I'm glad you enjoyed it!");
                OutputHelper.CutsceneLog("...", 1);
            }
            else
            {
                display.ShowCutscene(CutsceneImage.WizardDefault);
                OutputHelper.CutsceneLog("Oh? That's unfortunate...");
                OutputHelper.CutsceneLog("...", 1);
            }

            display.ShowCutscene(CutsceneImage.WizardRead);
            OutputHelper.CutsceneLog("Well, you see...", 2);
            OutputHelper.CutsceneLog("This whole journey...", 1);
            OutputHelper.CutsceneLog("Everything you've experienced...", 1);

            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("It was all just a game I created for my own amusement.", 1);

            display.ShowCutscene(CutsceneImage.WizardDefault);
            OutputHelper.CutsceneLog("The 'Demon King' you were searching for?", 1);

            display.ShowCutscene(EntityImage.Death);
            OutputHelper.CutsceneLog("That would be ME.", 2, ConsoleColor.Red);

            OutputHelper.CutsceneLog("I am the Game Master of this world.", 2);
            OutputHelper.CutsceneLog("The one who summoned you here.", 2);
            OutputHelper.CutsceneLog("The one who watched your every move.", 2);

            OutputHelper.CutsceneLog("In fact, you could say that...", 2);
            OutputHelper.CutsceneLog("Everything was all according to my plan.", 2);

            OutputHelper.CutsceneLog("...", 2);
            OutputHelper.CutsceneLog("And now, for the FINAL ACT...", 2, ConsoleColor.Red);

            OutputHelper.CutsceneLog("Let's see if you can defeat the true master of this game!", 1);
            OutputHelper.CutsceneLog("Come, Hero!", 1);
            OutputHelper.CutsceneLog("Show me everything you've learned!", 2);

            OutputHelper.CutsceneLog("\t\t\t== THE FINAL BATTLE BEGINS! ==", 2, ConsoleColor.Yellow);
        }
    }
}