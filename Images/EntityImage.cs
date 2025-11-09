using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    static class EntityImage
    {
        public static readonly AsciiImage Dragon = new SingleColorImage(@"
                                            ==(W{==========-      /===-                        
                                              ||  (.--.)         /===-_---~~~~~~~~~------____  
                                              | \_,|**|,__      |===-~___                _,-' `
                                 -==\\        `\ ' `--'   ),    `//~\\   ~~~~`---.___.-~~      
                             ______-==|        /`\_. .__/\ \    | |  \\           _-~`         
                       __--~~~  ,-/-==\\      (   | .  |~~~~|   | |   `\        ,'             
                    _-~       /'    |  \\     )__/==0==-\<>/   / /      \      /               
                  .'        /       |   \\      /~\___/~~\/  /' /        \   /'                
                 /  ____  /         |    \`\.__/-~~   \  |_/'  /          \/'                  
                /-'~    ~~~~~---__  |     ~-/~         ( )   /'        _--~`                   
                                  \_|      /        _) | ;  ),   __--~~                        
                                    '~~--_/      _-~/- |/ \   '-~ \                            
                                   {\__--_/}    / \\_>-|)<__\      \                           
                                   /'   (_/  _-~  | |__>--<__|      |                          
                                  |   _/) )-~     | |__>--<__|      |                          
                                  / /~ ,_/       / /__>---<__/      |                          
                                 o-o _//        /-~_>---<__-~      /                           
                                 (^(~          /~_>---<__-      _-~                            
                                ,/|           /__>--<__/     _-~                               
                             ,//('(          |__>--<__|     /                  .----_          
                            ( ( '))          |__>--<__|    |                 /' _---_~\        
                         `-)) )) (           |__>--<__|    |               /'  /     ~\`\      
                        ,/,'//( (             \__>--<__\    \            /'  //        ||      
                      ,( ( ((, ))              ~-__>--<_~-_  ~--____---~' _/'/        /'       
            ", ConsoleColor.DarkRed);

        public static AsciiImage Shark = new SingleColorImage(@"


                                             ,_.
                                           ./  |                                          _-
                                         ./    |                                       _-'/
                      ______.,         ./      /                                     .'  (
                 _---'___._.  '----___/       (                                    ./  /`'
                (,----,_  G \                  \_.                               ./   :
                 \___   ""--_                      ""--._,                       ./    /
                 /^^^^^-__          ,,,,,               ""-._       /|         /     /
                 `,       -        /////                    ""`--__/ (_,    ,_/    ./
                   ""-_,           ''''' __,                            `--'      /
                       ""-_,             \\ `-_                                  (
                           ""-_.          \\   \.                                 \_
                          /    ""--__,      \\   \.                       ____.     ""-._,
                         /        ./ `---____\\   \.______________,---\ (     \,        ""-.,
                        |       ./             \\   \        /\  |     \|       `--______---`
                        |     ./                 \\  \      /_/\_!
                        |   ./                     \\ \
                        |  /                        \_\
                        |_/'", ConsoleColor.Blue);


        public static AsciiImage Mouse = new SingleColorImage(@"




                                       ,     ,_
                                       |`\    `;;,            ,;;'
                                       |  `\    \ '.        .'.'
                                       |    `\   \  '-""""""""-' /
                                       `.     `\ /          |`
                                         `>    /;   _     _ \ 
                                          /   / |       .    ;
                                         <  (`"";\ ()   ~~~  (/_
                                          ';;\  `,     __ _.-'` )
                                            >;\          `   _.'
                                            `;;\          \-'
                                              ;/           \ _
                                              |   ,"""".     .` \
                                              |      _|   '   /
                                               ;    /"")     .;-,
                                                \    /  __   .-'
                                                 \,_/-""`  `-'",ConsoleColor.Yellow);


        public static AsciiImage Death = new MultiColorImage(@"



                                                             .""--..__
                                         _                     []       ``-.._
                                      .'` `'.                  ||__           `-._
                                     /    ,-.\                 ||_ ```---..__     `-.
                                    /    /:::\\               /|//}          ``--._  `.
                                    |    |:::||              |////}                `-. \
                                    |    |:::||             //'///                    `.\
                                    |    |:::||            //  ||'                      `|
                                    /    |:::|/        _,-//\  ||
                                   /`    |:::|`-,__,-'`  |/  \ ||
                                 /`  |   |'' ||           \   |||
                               /`    \   |   ||            |  /||
                             |`       |  |   |)            \ | ||
                            |          \ |   /      ,.__    \| ||
                            /           `         /`    `\   | ||
                           |                     /        \  / ||
                           |                     |        | /  ||
                           /         /           |        `(   ||
                          /          .           /          )  ||
                         |            \          |     ________||
                        /             |          /     `-------.|'`", new Dictionary<char, ConsoleColor> { {':', ConsoleColor.DarkGray} });

        public static AsciiImage Mushroom = new SingleColorImage(@"







                                                     ___
                                                   .')  `.
                                               _,-'¯¯  ,-.\
                                            .-'       (__' `._
                                           / ,--.             `.
                                          |  \_.'  /`-.    _,-. \
                                          \.;::,_  `-._)  (   /  |
                                            ':. ': `--..___ `-'   /
                                              `""|         `--..-' 
                                          /¯¯\  /          |_
                                          \ / /¯,  \   /    _¯\
                                           \\/ /|  o   o    \`.\
                                            \,' |           |  \`.
                                               /   ('¯')     \ /  \
                                               |    ¯¯¯      | \)_/
                                                `-.____..__,--'");

        public static AsciiImage Centaur = new SingleColorImage(@"





                                               _    _
                                               ))  ((
                                              //\__/\\
                                         ,(`  \_(o))_/
                                         (_)    _)/\_
                                              (/ v ` `.
                                         //,  (._ _.|  \
                                         \_/-/ |(|) /`-,\
                                           \_`,|(|) \ ( /  ___
                                             ¯ /(_)-'](,\-'   `\-.
                                              |¯\φ/`¯ `-/       )`)
                                              |            /   / (
                                               \     ,   __\   |  `->  
                                               | `|  /--'\ /\ /    
                                               | /| /    // //
                                               || ||     \\ \\
                                               || ||     !-`!-`
                                               /_)/_)");


        public static AsciiImage Treant = new SingleColorImage(@"

                                                       ,    
                                               _  |'  /  ` /-,
                                               ´\/   '      |
                                                |          /
                                           -\!  /   |      |
                                             \ '  | '    (/     / )
                                              \|          \'|   '/  
                                              \`             `-´_.´
                                              |              ,-´
                                              \      /`\    /
                                             /(\    () ;   /
                                             ' `\  / -´    `- _,
                                             |             ,´ `
                                              \        /  /
                                              |, _ . /')  |
                                              |,||'||,/   /
                                             _! ¯ ¯ '     |   ,--.
                                            /_           _ \_/ /¯`\
                                           `¯  ` ,-     ´ \   /  
                                                .  ,-´  `. \ ¯    
                                               _' /       ` `-    
                                               `-´          ", ConsoleColor.DarkYellow);

        public static AsciiImage Sandworm = new SingleColorImage(@"




                                                _.------.___
                                               / _,.._      `'--.     
                                              /,´   ) `-.        `.
                                             |´  ,-´   __`.        \
                                               .´   ,'´  )|         .
                                              /,-´,' ,.'´ /         |
                                              ` ,´ .´   `/    /     |
                                               / ,´   _.´  _,´      |
                                              / /  \-´__.-´         '
                                             . /    ¯¯,´           /
                                             ' \    ,´            / 
                                              `,|  /            ,´   
                                           (__//  /            /     
                                           `--´  .            /     
                                               /            ,´   
                                              /            /     
                                             .            /     
                                             |            |   ",ConsoleColor.DarkYellow);

        public static readonly AsciiImage Skeleton = new MultiColorImage(@"



                                             _.---._                               
                                            /       \                              
                                           |.-  ,-   |__                           
                                           \ *)  *)  |\_/\                         
                                            )  ^`  (/(\ \-\                        
                                            `v""uuV`  //  |-|                      
                                             |   <..<'  /-/  _______.-._____.-,._  
                                             |    _ ,-.|-|.-(_.-----`-'-----.\¯\¯` 
                                             |   ( (   /\  / )               \)|   
                                             .   //(`._'`-' / )            (_| |_  
                                                // (`._/\`-' /             `-'¯'-) 
                                               /(   `._/ `.-'                / |   
                                              (v)      .-.\-\.-.             | |   
                                              //      (  ( \-)  )            ) |   
                                    _        //        \  `-'  /_           /  |   
                                     \_   __//        ('`--^--'v )          )  |   
                                       \/`/'-`.        \(      )/   _  ,,,  \  /   
                                     /  `(   '`)        \\    //  _/ \`_/   |  |   
                                            `  \ │       \\  //,-'_)-'  _    ) |   
                                        '                 )\/(.-.___.-/ \._  \ |   
                                                        (_(._)_.---._)_)._)_>\/     

        ", new Dictionary<char, ConsoleColor> { {'*',ConsoleColor.Red} });


        public static AsciiImage Bear = new MultiColorImage(@"

                                                                 ssSSSSsss
                                                         ssS$$$$$$$$$$$$$$$$$ss
                                                    ssS$$$$$$$$$$$$$$$$$$$$$$$$sSs
                                                ssS$$$$$$$$$$$$$$$$$$$$$$$$$$$$$sSs
                                           .,ssS$$$Sss|._/_..-((('    )\)>>>      ~\
                                        ,sS$$$$$$$$$$$|$$$$$$$         '~`o        `\
                                      ,$$$$$$$$$$$$$$|$$S$$$$'                        \
                                    ,$$$$$$$$$$$$S$$|$$$$$$$'                      ,s$$$
                                  s$$$$$S$$$$$$$$$S|$$$$$$$$                      $$$$$$
                                _~,$S""""''     ``""S|$$S$$$$$""                   ,$$$$$$$;
                              /~ ,""'             / 'S$$$$$""          |        s$$$$$$$$$$
                           (~'      _,  \==~~)  /     """"""            |       $$$$$$$$$$$$
                            (O\   /O/     \-' /'                     |  |  ,$$$$$$$$$$$$$,
                            `/'  '         _-~                     ===\_-\ $$$$$$$$$$$$$$s
                            (~~~)      _.-~_-   \             \  ,/ =     `""$$$$$$$$$$$$$$$
                           ( `-'  )/>-~  _/-__   |            |,$$$$$/,      `""$$$$$$$$$$$$
                           /V^^^^V~/' _/~/~~  ~~-|            |$$$$$$$$         ""$$$$$$$$$$,
                          /  (^^^^),/' /'        )           /S$$$$$$$;         ,$$$$$$$$$$$,
                        ,$$_  `~~~'.,/'         /     _-ss, /(/-(/-(/'        ,s$$$$$$$$$$$$$
                      ,s$$$$$ssSS$$$'         ,$'.s$$$$$$$$'                  (/-(/-(/-(/-(/'
                     S$$$$$$$$$$$$$$        ,$$$$$$$$$$$$$'
                    (/-(/-(/-(/-(/'      _s$$$$$$$$$$$$$$
                                        (/-(/-(/-(/-(/-'", new Dictionary<char, ConsoleColor> { {'O', ConsoleColor.Cyan} });


        public static readonly AsciiImage Goblin = new SingleColorImage(@"   








                                                       ,      ,
                                                     /(.-""-.)\_
                                                 |\  \/      \/  /|              
                                                 | \ / =.  .= \ / |              
                                                 \( \   o\/o   / )/              
                                                  \_, '-/  \-' ,_/
                                                    /   \__/   \_
                                                    \ \__/\__/ /
                                                  ___\ \|--|/ /___
                                                /`    \      /    `\_
                                               /       '----'       \_
            ", ConsoleColor.Green);


        public static readonly AsciiImage Cat = new SingleColorImage(@"   














                                       ▐▀▄      ▄▀▌   ▄▄▄▄▄▄▄
                                       ▌▒▒▀▄▄▄▄▀▒▒▐▄▀▀▒██▒██▒▀▀▄
                                      ▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▀▄
                                      ▌▒▒▒▒▒▒▒▒▒▒▒▒▒▄▒▒▒▒▒▒▒▒▒▒▒▒▒▀▄
                                    ▀█▒▒█▌▒▒█▒▒▐█▒▒▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▌
                                    ▀▌▒▒▒▒▒▀▒▀▒▒▒▒▒▀▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▐ ▄▄
                                    ▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄█▒█
                                    ▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▀
                                      ▐▄▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄▌
                                       ▀▄▄▀▀▀▀▄▄▀▀▀▀▀▀▄▄▀▀▀▀▀▀▄▄▀
            ", ConsoleColor.White);


        public static AsciiImage GetGrave(string characterName) => new MultiColorImage($@"

                                                 _____  _____
                                                <     `/     |
                                                 >          (
                                                |   _     _  |
                                                |  |_) | |_) |
                                                |  | \ | |   |
                                                |            |
                                 ______.______%_|            |__________  _____
                               _/                                       \|     |
                              |                   {characterName}                  
                              |_____.-._________              ____/|___________|
                                                | * {DateTime.Now:hh:mm}    |
                                                | + {DateTime.Now:MM/dd/yy} |
                                                |            |
                                                |            |
                                                |   _        <
                                                |__/         |
                                                 / `--.      |
                                               %|            |%
                                           |/.%%|          -< @%%%
                                           `\%`@|     v      |@@%@%%
                                         .%%%@@@|%    |    % @@@%%@%%%%
                                    _.%%%%%%@@@@@@%%_/%\_%@@%%@@@@@@@%%%%%%",
            new Dictionary<char, ConsoleColor>
            {
                {'%', ConsoleColor.Green},
                {'@', ConsoleColor.DarkGreen}
            });

        public static void ShowGrave(string characterName, string killerName)
        {
            var display = new MapDisplay();
            Console.Clear();

            display.ShowCutscene(EntityImage.GetGrave(characterName));
            OutputHelper.StatusMessage($"The {killerName} hit you so hard.", ConsoleColor.Red);

            OutputHelper.StatusMessage($"Rest in peace, {characterName}...", ConsoleColor.White);
            Console.ReadKey(true);
        }


        public static AsciiImage Gryphon = new SingleColorImage(@"
                                            //           //
                                           ///          ///
                                          ////         ////
                                          |////       /////
                                          |))//;     /)))//;
                                         /)))))/;   /)))))/;
                                     .---`,))))/;  /)))))))/;
                                 __--\/@-  \`))/; |)))))))/;  
                                (----/    \\\``;  |))))))/;    
                                   ~/-\  \\\\\``   \))))))/;
                                       \\\\\\\\`    |)))))/;
                                       |\\\\\\\\___/))))))/;__-------.
                                       //////|  %%_/))))))/;           \___,
                                      |||||||\   \%%%%%%;:              \_. \
                                      |\\\\\\\\\                        |  | |
                                       \\\\\\\                          |  | |
                                        |\\\\               __|        /   / /
                                        | \\__\     \___----  |       |   / /
                                        |    / |     )     \   \      \  / /
                                        |   /  |    /       \   \      >/ /  ,,
                                        |   |  |   |         |   |    // /  //,
                                        |   |  |   |         |   |   /| |   |\\,
                                     _--'   _--'   |     _---_---'  |  \ \__/\|/
                                    (-(-===(-(-(===/    (-(-=(-(-(==/   \____/", ConsoleColor.DarkYellow);


        public static AsciiImage Demon = new SingleColorImage(@"









                                       , ,, ,                              
                                       | || |    ,/  _____  \.             
                                       \_||_/    ||_/     \_||             
                                         ||       \_| . . |_/              
                                         ||         |  L  |                
                                        ,||         |`==='|                
                                        |>|      ___`>  -<'___             
                                        |>|\    /             \            
                                        \>| \  /  ,    .    .  |           
                                         ||  \/  /| .  |  . |  |           
                                         ||\  ` / | ___|___ |  |     (     
                                      (( || `--'  | _______ |  |     ))  ( 
                                    (  )\|| (  )\ | - --- - | -| (  ( \  ))
                                    (\/  || ))/ ( | -- - -- |  | )) )  \(( 
                                     ( ()||((( ())|         |  |( (( () )  ", ConsoleColor.Red);


        public static AsciiImage Minion = new SingleColorImage(@"











                                                  (_(  │││
                                                  ,o’),└╥┘
                                                  )_/ \ ║
                                                   ,`/ ^║
                                                  ((.(.{_``>
                                                  | `-. ║` ^,  <-.
                                                  \,  /`║ / -^,_.’
                                                     // ║\\   
                                                   "" ` ║,/`",ConsoleColor.Red);



        public static AsciiImage SnakeWithLegs = new SingleColorImage(@"












                                                      _.--.
                                                 __.-´,--. `.
                                                (  o.´    ) |
                                                 `""´ ,--_´  `-.
                                                    (`-( \ ,-( \
                                                   /.´  `.\   `.\
                                                  < |    | >   | >
                                                  | \    / |   / |
                                                   \|    |/    |/", ConsoleColor.Green);



        public static AsciiImage Yeti = new MultiColorImage(@"










                                                   ,---. _.---. 
                                              .^^ (.- ) ´    _ \ ^^.
                                            <´     ` (o`´o )( `/    `>
                                           <  {      {,-,`´ }      }  >
                                          /   /`-/    )  ) /    \-´\   \
                                         /   |   |   !^-´,´     |   |   \
                                        / _\ |    \   `'´      /    | /_ \
                                        \__| /     `""""""´  `""""""´     \ |__/
                                         `-(/       \    \    ).     \)-´
                                                    .\__/ \__/  \
                                                   /   _\    )_  `.
                                                  (_(_(__)  (__)_)_)", new Dictionary<char, ConsoleColor> { {'o', ConsoleColor.Red} });


        public static AsciiImage Godzilla = new MultiColorImage(@"










                                                        _,-}}-._
                                                       /\   }  /\
                                                     _|(O\\_ _/O)                
                                                    _|/  (__''__)                 
                                                  _|\/    WVVVVW                 
                                                 \ _\     \MMMM/_             
                                               _|\_\     _ '---; \_           
                                          /\   \ _\/      \_   /   \          
                                         / (    _\/     \   \  |'VVV   
                                        (  '-,._\_.(      'VVV /      
                                         \         /   _) /   _)     
                                          '....--''\__vvv)\__vvv)", new Dictionary<char, ConsoleColor> { { 'O', ConsoleColor.Red } });


        public static AsciiImage Raptor = new SingleColorImage(@"







                                                          _..-=~=-._
                                                     _.-~'          ~.
                                         __..---~~~~~                 ~.
                                    _.-~~                      _.._     ~.
                                _ -~_                         /    \      ;
                               ( ` '@)                       {      |      :
                               /                             |      |      :
                              /     /}         (  )          |      |     .-
                             /     //-=-~-_-_  |  |          \      ;    .'
                            /     //     | =._-|  }/ / / /_.==\     ; _.'   
                           ( oo  //|     = )  ~| /.__..-='|    \    :'     
                            ====||*|    / /    + )    \   |_.-~`\   :      
                               |*| *   / /    / /      \  |     ([ ])     
                                |*    /_/    / /       (  ]     `/ \'     
                                 |   (((|   /_/      __/_/__    -| |--    _
                                           (((|      -----     __|_|__
                                           '''                 -----
                    ", ConsoleColor.DarkGreen);


        public static AsciiImage Minotaur = new SingleColorImage(@"



                                   -""""\
                                .-""  .`)     (
                               j   .'_+     :[                )      .^--..
                              i    -""       |l                ].    /      i
                             ,"" .:j         `8o  _,,+.,.--,   d|   `:::;    b
                             i  :'|          ""88p;.  (-.""_""-.oP        \.   :
                             ; .  (            >,%%%   f),):8""          \:'  i
                            i  :: j          ,;%%%:; ; ; i:%%%.,        i.   `.
                            i  `: ( ____  ,-::::::' ::j  [:```          [8:   )
                            <  ..``'::::8888oooooo.  :(jj(,;,,,         [8::  <
                            `. ``:.      oo.8888888888:;%%%8o.::.+888+o.:`:'  |
                             `.   `        `o`88888888b`%%%%%88< Y888P""""'-    ;
                               ""`---`.       Y`888888888;;.,""888b.""""""..::::'-'
                                      ""-....  b`8888888:::::.`8888._::-""
                                         `:::. `:::::O:::::::.`%%'|
                                          `.      ""``::::::''    .'
                                            `.                   <
                                              +:         `:   -';
                                               `:         : .::/
                                                ;+_  :::. :..;;;       
                                                ;;;;,;;;;;;;;,;;
                    ", ConsoleColor.DarkYellow);


        public static AsciiImage Octupi = new SingleColorImage(@"





                                                ___
                                             .-'   `'.
                                            /         \
                                            |         ;
                                            |         |           ___.--,
                                   _.._     |0) ~ (0) |    _.---'`__.-( (_.
                            __.--'`_.. '.__.\    '--. \_.-' ,.--'`     `""""`
                           ( ,.--'`   ',__ /./;   ;, '.__.'`    __
                           _`) )  .---.__.' / |   |\   \__..--""""  """"""--.,_
                          `---' .'.''-._.-'`_./  /\ '.  \ _.-~~~````~~~-._`-.__.'
                                | |  .' _.-' |  |  \  \  '.               `~---`
                                 \ \/ .'     \  \   '. '-._)
                                  \/ /        \  \    `=.__`~-.
                                  / /\         `) )    / / `"""".`\
                            , _.-'.'\ \        / /    ( (     / /
                             `--~`   ) )    .-'.'      '.'.  | (
                                    (/`    ( (`          ) )  '-;
                                     `      '-;         (-'
", ConsoleColor.DarkYellow);

        public static AsciiImage Spider = new MultiColorImage(@"










                                               _                
                                          /\  ||\  /|          
                                         //\\ ||\\/_'--._      
                                       -´´  \\||/¯_ /\ __`._    
                                         __ /¯¯¯\/.|/\\--(\/    
                                      ,-°∞ )    //||/_\\-´\\    
                                        _/¯ \  ´/´||¯ //   \\  
                                              ¯¯  // (´     ``-
                                                 (´   `", new Dictionary<char, ConsoleColor> { { '°', ConsoleColor.DarkRed } , { '∞', ConsoleColor.DarkRed } });

        public static AsciiImage Queen = new MultiColorImage(@"




                                       .*.
                                     *' + '*      ,       ,
                                      *'|'*       |`;`;`;`|
                                        |         |:.'.'.'|
                                        |         |:.:.:.:|
                                        |         |::....:|
                                        |        /`   / ` \
                                        |       (   .' ^ \^)
                                        |_.,   (    \    -/(
                                     ,~`|   `~./     )._=/  )  ,,
                                    {   |     (       )|   (.~`  `~,
                                    {   |      \   _.'  \   )       }
                                    ;   |       ;`\      `)-`       }     _.._
                                     '.(\,     ;   `\    / `.       }__.-'__.-'
                                      ( (/-;~~`;     `\_/    ;   .'`  _.-'
                                      `/|\/   .'\.    /o\   ,`~~`~~~~`
                                       \| ` .'   \'--'   '-`
                                        |--',==~~`)       (`~~==,_
                                        ,=~`      `-.     /       `~=,
                                     ,=`             `-._/            `=,`", new Dictionary<char, ConsoleColor> 
        {
            { '*' , ConsoleColor.Yellow},
            { '|', ConsoleColor.Yellow},
            { '+', ConsoleColor.Yellow},
            { 'o', ConsoleColor.Yellow},
            { '.', ConsoleColor.Yellow },
            {':', ConsoleColor.Yellow },
            {';',ConsoleColor.Yellow },
            { '^', ConsoleColor.Cyan}                          
        });

        public static AsciiImage Serpent = new MultiColorImage(@"                                                                __..._                 ~~~
                                                            ..-'     o00.            
                                   ~~~                   .-'            :           
                                                     _..'    <<<      .'__..--<     
                                              ...--""""                 '-.           
                          ~               ..-""                       __.'           
                                        .'<<                ___...--'               
                                       :        ____....---'                        ~~~
                                      :       .'                                    
                                     :       :           _____                      
                                     :    <<:    _..--""""""     """"""--..__              ~~~
                                    :     <<:  .""        >>             """"]--.       
                                    :       '.:                         :      '.     
                    ~~~             :   ~~    '--...___/---""""""""--..___.'        :     
                                     :                 """"---...---""""           :     
                                      '.                <<                     :             ~~
                                        '-.  <<            ~~                :       
                                           '--...                <<       .'        
                                   ~~        :   """"---....._____.....---""""          
                                             '.    '.                               
                      ~~~                      '-..  '.                             ~~~
                                                   '.  :                            
                                                    : .~~~                                                      
                                                   .' .'                              
                                                  `--'", new Dictionary<char, ConsoleColor>
        {

          { 'o', ConsoleColor.Red},
          { '~', ConsoleColor.DarkCyan},
          {'>', ConsoleColor.Red},
          {'<', ConsoleColor.Red },
          {'0', ConsoleColor.Red},
          {'_', ConsoleColor.Green},
          {'.', ConsoleColor.DarkGreen},
          {'-', ConsoleColor.Green},
          {'\'', ConsoleColor.DarkGreen},
          {':', ConsoleColor.Green},
          {'"', ConsoleColor.DarkGreen},
          {']', ConsoleColor.Green},
          {'/', ConsoleColor.DarkGreen}
        });

    }
}
