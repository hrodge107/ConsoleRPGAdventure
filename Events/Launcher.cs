using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    internal class Launcher
    {
        public static string ShowScreen()
        {
            while (true)
            {
                ShowTitle();

                string choice = Console.ReadKey(true).KeyChar.ToString();

                switch (choice)
                {
                    case "1": // <-- new game
                        if (SaveGameManager.SaveFileExists())
                        {
                            if (!SaveGameManager.ConfirmOverwrite())
                            {
                                continue; 
                            }
                        }

                        Console.Clear();
                        OutputHelper.CutsceneLog("(NOTE: Please set your Console Window to fullscreen)", 99,ConsoleColor.Cyan);
                        return choice;

                    case "2": // <-- load
                        if (SaveGameManager.SaveFileExists())
                        {
                            return choice;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n");
                            OutputHelper.StatusMessage("No save file found! Please start a new game first.", ConsoleColor.Red);
                        }
                        break;

                    case "3":
                        return choice; // Exit

                    default:
                        Console.Clear();
                        OutputHelper.CutsceneLog("(Please choose and press from 1, 2, or 3 only!)", 99);
                        break;
                }
            }

            static void ShowTitle()
            {
                Console.Clear();
                Console.WriteLine(@"

                             ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗    ██████╗ ██████╗  ██████╗ 
                            ██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝    ██╔══██╗██╔══██╗██╔════╝ 
                            ██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗      ██████╔╝██████╔╝██║  ███╗
                            ██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝      ██╔══██╗██╔═══╝ ██║   ██║
                            ╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗    ██║  ██║██║     ╚██████╔╝
                             ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝  ╚═╝╚═╝      ╚═════╝ 
                                                                                         
                                 █████╗ ██████╗ ██╗   ██╗███████╗███╗   ██╗████████╗██╗   ██╗██████╗ ███████╗        
                                ██╔══██╗██╔══██╗██║   ██║██╔════╝████╗  ██║╚══██╔══╝██║   ██║██╔══██╗██╔════╝        
                                ███████║██║  ██║██║   ██║█████╗  ██╔██╗ ██║   ██║   ██║   ██║██████╔╝█████╗          
                                ██╔══██║██║  ██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ██║   ██║██╔══██╗██╔══╝          
                                ██║  ██║██████╔╝ ╚████╔╝ ███████╗██║ ╚████║   ██║   ╚██████╔╝██║  ██║███████╗        
                                ╚═╝  ╚═╝╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝      



                                                                                                                                                                             
                                                    ░▀█░░░░░░░█▀▀░▀█▀░█▀█░█▀▄░▀█▀             
                                                    ░░█░░░░░░░▀▀█░░█░░█▀█░█▀▄░░█░             
                                                    ░▀▀▀░▀░░░░▀▀▀░░▀░░▀░▀░▀░▀░░▀░             

                                                    ░▀▀▄░░░░░░█▀▀░█▀█░█▀█░▀█▀░▀█▀░█▀█░█░█░█▀▀ 
                                                    ░▄▀░░░░░░░█░░░█░█░█░█░░█░░░█░░█░█░█░█░█▀▀ 
                                                    ░▀▀▀░▀░░░░▀▀▀░▀▀▀░▀░▀░░▀░░▀▀▀░▀░▀░▀▀▀░▀▀▀ 

                                                    ░▀▀█░░░░░░█▀▀░█░█░▀█▀░▀█▀                 
                                                    ░░▀▄░░░░░░█▀▀░▄▀▄░░█░░░█░                 
                                                    ░▀▀░░▀░░░░▀▀▀░▀░▀░▀▀▀░░▀░                             
                                                                                                                    
                                              ");
            }
        }
    }
}

