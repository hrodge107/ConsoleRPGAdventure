using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    static class CutsceneImage
    {
        public static AsciiImage WizardGreeting = new MultiColorImage(@"





                                                     .-.
                                                    /_  \
                                                   /.:\  \
                                                  /:::|_  .
                                                  \ |:: ) |
                                                   \|::/  |
                                                 ,-´\:|    `--.
                                     _     __.--´    )|   ´    \
                                     \`--'´      )   \|         \
                                     |        _.-\    !    \     \
                                     |     ,-´    `.        `.   |
                                     ! ' .´         `.        \  /
                                     '.  |            \       | /
                                      '| '             \      |/
                                                        !     |", new Dictionary<char, ConsoleColor> { { ':', ConsoleColor.DarkGray }, 
                });


        public static AsciiImage WizardPoint = new MultiColorImage(@"




                                           .-.
                                          / ||       .-.
                                         / _'|      /_  \
                                        . /  \     /.:\  \
                                        | |  /    /:::|_  .
                                         !|  |    \ |:: ) |
                                          |  |     \|::/  |
                                          |  |   ,-´\:|    `--.
                                          |  '--´    )|   ´    \
                                          |      )   \|         \
                                          '.__.--\    !    \     \
                                                  `.        `.   |
                                                    `.        \  /
                                                      \       | /
                                                       \      |/
                                                        !     |", new Dictionary<char, ConsoleColor> { { ':', ConsoleColor.DarkGray }
                });


        public static AsciiImage WizardDefault = new MultiColorImage(@"





                                                     .-.
                                                    /_  \
                                                   /.:\  \
                                                  /:::|_  .
                                                  \ |:: ) |
                                                   \|::/  |
                                                 ,-´\:|    `--.
                                           ,==,-´    )|   ´    \
                                          /   )  )   \|         \
                                          |`-/.--\    !    \     \
                                          |  ||   `.        `.   |
                                          !  ||     `.        \  /
                                          |' |'       \       | /
                                           !|'         \      |/
                                                        !     |", new Dictionary<char, ConsoleColor> { { ':', ConsoleColor.DarkGray }
            });


        public static AsciiImage WizardRead = new MultiColorImage(@"





                                                     .-.
                                      ______        /_  \
                                     ´\     \      /.:\  \
                                     '´\  ∑  \    /:::|_  .
                                      '´\_____\   \ |:: ) |
                                       '(_____(    \|::/  |
                                         \  |    ,-´\:|    `--.
                                         |  |  ,´    )|   ´    \
                                         |  |,´  )   \|         \
                                         |     _.\    !    \     \
                                         '._.-´   `.        `.   |
                                                    `.        \  /
                                                      \       | /
                                                       \      |/
                                                        !     |", new Dictionary<char, ConsoleColor> { { ':', ConsoleColor.DarkGray }
                });


        public static AsciiImage Book = new SingleColorImage(@$"
                  ___,.---------.,_         _,.---------.,___    
         ┌------'´                 `'-._.-'´                 `'--- ¯ ╖
        ┌│                             |                             ║┐
        ││    @ Name:                  |                             ║│
        ││                             |                             ║│
        ││                             |                             ║│
        ││                             |                             ║│
        ││                             |                             ║│
        ││        ___,.---------.,_    |    _,.---------.,___        ║│
        │'------'´                 `'-.|.-'´.--'´¯¯¯¯¯¯`'---.`'======'│
         ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯`-----´¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");


        public static AsciiImage ClosedBook = new SingleColorImage(@"
                     _______________________________
                    .|                              |╖
                    ||                              |║┐
                    ||                              |║│
                    ||                              |║│
                    ||          DEATH NOTE          |║│
                    ||                              |║│
                    ||                              |║│
                    ||                              |║│
                    ||                              |║│
                    ||                              |║│
                    ||                              |║│
                    ||______________________________|║│
                    |/,=============================='│
                     '--------------------------------'");


        public static AsciiImage ItemBook = new SingleColorImage(@$"
                  ___,.---------.,_         _,.---------.,___    
         ┌------'´                 `'-._.-'´                 `'--- ¯ ╖
        ┌│                             |                             ║┐
        ││  A - Time Slow Potion ($10) |  Slows down enemy movement  ║│
        ││  B - Perception Lens  ($10) |  See more targets to hit    ║│
        ││  C - Health Potion    ($ 5) |  Increase current HP        ║│
        ││  D - Strength Up      ($20) |  Permanently increase ATK   ║│
        ││  E - Health Up        ($20) |  Permanently increase Max HP║│
        ││  x - Exit                   |                             ║│
        ││                             |                             ║│
        ││  @ Enter:                   |                             ║│
        ││        ___,.---------.,_    |    _,.---------.,___        ║│
        │'------'´                 `'-.|.-'´.--'´¯¯¯¯¯¯`'---.`'======'│
         ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯`-----´¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");


        public static AsciiImage RoleBook = new SingleColorImage(@$"
                  ___,.---------.,_         _,.---------.,___    
         ┌------'´                 `'-._.-'´                 `'--- ¯ ╖
        ┌│                             |                             ║┐
        ││  A - Swordsman              |  Reduces Enemy DMG, has     ║│
        ││                             |  moderate ATK.              ║│
        ││                             |                             ║│
        ││  B - Ranger                 |  Sees more weakspots, has   ║│
        ││                             |  low atk.                   ║│
        ││                             |                             ║│
        ││  C - Wizard                 |  Has high ATK, but needs    ║│
        ││                             |  to chant spell for 3 turns ║│
        ││  @ Enter:                   |                             ║│
        ││        ___,.---------.,_    |    _,.---------.,___        ║│
        │'------'´                 `'-.|.-'´.--'´¯¯¯¯¯¯`'---.`'======'│
         ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯`-----´¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

    }
}


// new Dictionary<char, ConsoleColor> { { ':', ConsoleColor.DarkCyan }, {'|', ConsoleColor.DarkBlue}, {'/' , ConsoleColor.Blue} , {'\\' , ConsoleColor.Blue}, {'\'', ConsoleColor.DarkBlue}, {'.', ConsoleColor.DarkBlue}, {'´', ConsoleColor.DarkBlue}, {'-', ConsoleColor.DarkBlue}, {'_', ConsoleColor.DarkBlue}, {'`', ConsoleColor.DarkBlue}, {',', ConsoleColor.DarkBlue}, {'!', ConsoleColor.DarkBlue}, {')', ConsoleColor.DarkBlue},