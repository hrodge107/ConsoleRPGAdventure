// In Map.cs
using System.Runtime.Intrinsics.Arm;
using ConsoleRPGAdventure;

public class Map
{
    public string Name { get; protected set; }
    public Area StartingArea { get; protected set; }
    private List<Area> _areas = new List<Area>();

    protected Map() { }

    public List<Area> GetAreas() => _areas;

    public void AddAreas(params Area[] areas)
    {
        foreach (var area in areas)
        {
            _areas.Add(area);
        }
    }
}


public class BeginnerMap : Map
{
    public BeginnerMap()
    {
        Name = "The Land of Trial and Error";

        #region Dungeon Arc
        var dungeonStart = new EventArea("Dungeon", BackgroundImage.Dungeon, 0,0, 
            new CutsceneArea("...", "...", "You find yourself in a dark dungeon...","(Why did that hooded figure teleport me here out of all places?)", "...", "(Anyway, I should probably find my way out for now.)"));
        var d2 = new LocationArea("Dungeon", BackgroundImage.Dungeon, 1,0);
        var d3 = new CombatArea("Dungeon", BackgroundImage.Dungeon, 2, 0, (new List<Enemy> { new Spider() }), 1.0);
        var d4 = new LocationArea("Dungeon", BackgroundImage.Dungeon, 3,0);
        var dungeonEnd = new EventArea("Dungeon Exit", BackgroundImage.Door, 4, 0,
            new CutsceneArea("You see the door leading to the outside.", "(I can finally leave this place!)"));

        dungeonStart.ConnectTo(d2, Direction.East);
        d2.ConnectTo(d3, Direction.East);
        d3.ConnectTo(d4, Direction.East);
        d4.ConnectTo(dungeonEnd, Direction.East);

        AddAreas(dungeonStart, d2, d3, d4, dungeonEnd); // <-- minimap
        #endregion

        #region Forest Arc
        var ForestStart = new EventArea("Forest", BackgroundImage.Forest, 4, 1,
            new CutsceneArea("Outside the dungeon was a forest..."));
        var f2 = new CombatArea("Forest", BackgroundImage.Forest, 4, 2, (new List<Enemy> { new SnakeWithLegs() , new Mouse()}), 0.4);
        var f3 = new CombatArea("Mushroom Land", BackgroundImage.MushroomLand, 4, 3, (new List<Enemy> { new Mushroom() }), 0.6); // <-- Branch to Beach
        var f4 = new EventArea("Treehouse", BackgroundImage.TreeHouse, 3, 3, new HealingArea("You decided to rest in a treehouse you stumbled upon."));
        var f5 = new CombatArea("Forest", BackgroundImage.Forest, 2, 3, (new List<Enemy> { new Treant() }), 0.8);
        var ForestToTown = new CombatArea("Forest Outskirts", BackgroundImage.Forest, 2, 2, (new List<Enemy> { new SnakeWithLegs(), new Mouse() }), 0.4);

        dungeonEnd.ConnectTo(ForestStart, Direction.North);
        ForestStart.ConnectTo(f2, Direction.North);
        f2.ConnectTo(f3, Direction.North);
        f3.ConnectTo(f4, Direction.West);
        f4.ConnectTo(f5, Direction.West);
        f5.ConnectTo(ForestToTown, Direction.South);

        AddAreas(ForestStart, f2, f3, f4, f5, ForestToTown); // Update later to branch to mountains / sea
        #endregion

        #region Town Arc
        var TownStart = new EventArea("Town", BackgroundImage.Town, 1, 2, 
            new CutsceneArea("You arrived on a town.", "(Maybe I can buy some items here that will help me.)"));

        var t2 = new LocationArea("Town", BackgroundImage.Town, 0, 2);
        var t3 = new LocationArea("Town", BackgroundImage.Town, 0, 1);
        var Shop = new EventArea("Shop", CutsceneImage.WizardGreeting, 1, 1, new ShopArea());

        var t4 = new EventArea("Floating Castle", BackgroundImage.Castle, 0, 3,
            new CutsceneArea("(You were amazed at the sight of the floating castle.)","(Perhaps you can seek audience with the Queen there.)"));

        var t5 = new EventArea("Floating Castle", EntityImage.Queen, 0, 4, 
            new TreasureArea(0,0,88, "(You entered the castle and see 'someone' sitting at the throne.)","...State yourself, who are 'YOU' to barge in to my castle unannounced?","(Ah, this person must be the owner of the castle... in other words, the Queen!)",
            "\"I am {player}, the Hero summoned to this world to defeat the Demon King.\"","The Hero you say? Come in, I have been expecting you.","So, what's the purpose of you being here?","\"Can I humbly request aid in my journey to defeat the Demon King?\"","...Hmmm, 'aid' you say?","As much I'd like to lend you our Kingdom's Sacred Royal Weapons...","They are currently in use of the Knights stationed near the border, defending against the Demon Army",
            "But perhaps you can borrow it once you rendezvous with them.","\"But where is this Knight Encampment, Queen?\"","Go north, and cross the Great Mountain, then you'll see where my Knights are stationed.","...","...actually it has been weeks since I last heard from them.","... I just hope they're alright.","Anyway, here. All I can do for now is to give you some money from our Kingdom's treasury.", "You can buy equipment to help you at the southern edge of the town.","...Well then, I will not keep you here any longer, {player}.","Farewell, I wish you luck on your journey."));

        ForestToTown.ConnectTo(TownStart, Direction.West);
        TownStart.ConnectTo(t2, Direction.West);
        t2.ConnectTo(t3, Direction.South);
        t3.ConnectTo(Shop, Direction.East);

        t2.ConnectTo(t4, Direction.North);
        t4.ConnectTo(t5, Direction.North);
        
        AddAreas(TownStart, t2,t3,t4,t5,Shop);
        #endregion

        #region Sea Arc
        var SeaStart = new EventArea("Beach", BackgroundImage.Beach, 5, 3,
            new CutsceneArea("You find yourself standing on the beach.","You noticed a sign and read it.", "...", "\"On the other side of the sea is...\"","\"...A treasure chest full of gold coins.\"","\"However, extremely ferocious sea monsters have been lurking the waters here.\"", "\"They can dive in the water to avoid attacks.\"", "\"...So, do you still have courage to take on the treasure?\"", " - End of the sign - ","(Seems challenging, maybe I should try to stock on items first?)"));

        var s2 = new LocationArea("Sea", BackgroundImage.Sea, 6, 3);
        var s3 = new CombatArea("Sea",BackgroundImage.Sea, 6, 2, new List<Enemy> { new Serpent(), new Octopus() }, 0.5);
        var s4 = new CombatArea("Sea", BackgroundImage.Sea, 6, 1, new List<Enemy> { new Serpent(), new Octopus() }, 0.7);
        var s5 = new CombatArea("Sea", BackgroundImage.Sea, 6, 0, new List<Enemy> { new Serpent(), new Octopus() }, 0.4);
        var s6 = new EventArea("Island", BackgroundImage.Island, 7,0 , new HealingArea("You decided to rest on an island."));
        var s7 = new LocationArea("Sea", BackgroundImage.Sea, 8, 0);
        var s8 = new BossArea("Sea", BackgroundImage.Sea, 8, 1, new Shark());

        var SeaEnd = new EventArea("Treasure Cave", BackgroundImage.Chest, 8, 2,
            new TreasureArea(10,5,33,"After the grueling battle with the abyssal beast...", "You finally arrived at the island where the treasure chest was located...", "(Upon opening the treasure... you suddenly feel stronger for some reason.)"));


        f3.ConnectTo(SeaStart, Direction.East);
        SeaStart.ConnectTo(s2, Direction.East);
        s2.ConnectTo(s3, Direction.South);
        s3.ConnectTo(s4, Direction.South);
        s4.ConnectTo(s5, Direction.South);
        s5.ConnectTo(s6, Direction.East);
        s6.ConnectTo(s7, Direction.East);
        s7.ConnectTo(s8, Direction.North);
        s8.ConnectTo(SeaEnd, Direction.North);

        AddAreas(SeaStart,s2,s3,s4,s5,s6,s7,s8,SeaEnd);
        #endregion

        #region Mountain Arc
        var f6 = new CombatArea("Forest", BackgroundImage.Forest, 2, 4, new List<Enemy> {new Goblin(), new Spider() }, 0.3);
        var MountainStart = new EventArea("Mountain Outskirts", BackgroundImage.CloudyMountain ,2, 5, new CutsceneArea("You arrive at the foot of the Great Mountain.","Behind this mountain is said to be the fortress of the Demon King.", "As such, in order to fulfill your mission of slaying the Demon King to save the world...","...You must first pass and challenge the creatures of this mountain."));
        var m2 = new CombatArea("Mountain", BackgroundImage.CloudyMountain, 2, 6, new List<Enemy> { new Yeti() }, 1.0);
        var m3 = new CombatArea("Mountain", BackgroundImage.CloudyMountain, 1, 6, new List<Enemy> { new Yeti(), new Gryphon()}, 0.5);
        var m4 = new CombatArea("Mountain", BackgroundImage.CloudyMountain, 0, 6, new List<Enemy> { new Godzilla() , new Raptor() }, 0.7);
        var m5 = new CombatArea("Mountain", BackgroundImage.CloudyMountain, 0, 7, new List<Enemy> { new Yeti(), new Gryphon(), new Godzilla(), new Raptor() }, 0.5);
        var m8 = new EventArea("Mountain Cave", BackgroundImage.Cave, 0, 8, new HealingArea("You found a cave to rest in for a while."));
        var m9 = new LocationArea("Summit", BackgroundImage.Mountain, 0, 9);
        var m10 = new CombatArea("Summit", BackgroundImage.Mountain, 1, 9, new List<Enemy> { new Godzilla(), new Raptor() }, 0.5);
        var m11 = new CombatArea("Summit", BackgroundImage.Mountain, 1, 10, new List<Enemy> { new Yeti(), new Godzilla(), new Raptor() }, 0.5);
        var m12 = new LocationArea("Summit", BackgroundImage.Mountain, 2, 10);
        var m13 = new BossArea("Summit", BackgroundImage.Mountain, 2, 9, new Bear());
        var m14 = new LocationArea("Summit", BackgroundImage.Mountain, 3, 9);
        var m15 = new CombatArea("Mountain", BackgroundImage.CloudyMountain, 3, 8, new List<Enemy> { new Yeti(), new Gryphon(), new Godzilla(), new Raptor() }, 0.6);
        var m16 = new CombatArea("Mountain", BackgroundImage.CloudyMountain, 3, 7, new List<Enemy> { new Yeti(), new Gryphon(), new Godzilla(), new Raptor() }, 0.8);
        var MountainEnd = new CombatArea("Mountain", BackgroundImage.CloudyMountain, 3, 6, new List<Enemy> { new Yeti(), new Gryphon(), new Godzilla(), new Raptor() }, 0.7);


        f5.ConnectTo(f6, Direction.North);
        f6.ConnectTo(MountainStart, Direction.North);
        MountainStart.ConnectTo(m2, Direction.North);
        m2.ConnectTo(m3, Direction.West);
        m3.ConnectTo(m4, Direction.West);
        m4.ConnectTo(m5, Direction.North);
        m5.ConnectTo(m8, Direction.North);
        m8.ConnectTo(m9, Direction.North);
        m9.ConnectTo(m10, Direction.East);
        m10.ConnectTo(m11, Direction.North);
        m11.ConnectTo(m12, Direction.East);
        m12.ConnectTo(m13, Direction.South);
        m13.ConnectTo(m14, Direction.East);
        m14.ConnectTo(m15, Direction.South);
        m15.ConnectTo(m16, Direction.South);
        m16.ConnectTo(MountainEnd, Direction.South);

        AddAreas(f6, MountainStart, m2, m3, m4, m5, m8, m9, m10, m11, m12, m13, m14, m15, m16, MountainEnd);
        #endregion

        #region Demon King Fortress Arc
        var KnightEncampment = new EventArea("Knight Encampment", BackgroundImage.KnightEncampment, 4, 6,
            new CutsceneArea("After making your way down the Great Mountain, you finally arrived at the Knight Encampment.", "Apparently, knights are stationed here to defend from invasion attempts from the Demon King Fortress."));


        // Outskirts to Gate
        var c1 = new EventArea("Knight Encampment", BackgroundImage.Knight, 4, 5,
            new TreasureArea(20, 8, 0, "(You see a single wounded knight at the encampment.)", "(Suddenly, he came up and talked to you.)", "...", "h-Hey there, are you perhaps the Hero that's supposed to help us defeat the Demon King?", "\"Uh, yeah, I guess that'd be me...\"",
            "...Thank goodness, you came at the right time.", "We... just barely held back the Demon Army from advancing.", "\"What do you mean by 'We,' where are the other knights?\"", "I... I was the only one that survived.", "Everyone... they sacrificed themselves in the defense battle.", "(.. I was too late!)",
            "But their sacrifices will not be in vain because you're here now...", "They let me stay behind for a reason, it was to give you our Kingdom's Sacred Treasures.", "...", "...Now, please use this treasures and defeat the Demon King for us!"));
        
        var c2 = new LocationArea("Outside the Fortress", BackgroundImage.FortressOutside, 5, 5);
        var c3 = new BossArea("Fortress Gate", BackgroundImage.FortressGate, 6, 5, new CastleMonster());

        // Inside
        var c4 = new EventArea("Fortress", BackgroundImage.Dungeon, 7, 5, 
            new CutsceneArea("You finally entered the Fortress...","...?","(...It looks familiar for some reason?)"));
        var c5 = new CombatArea("Fortress", BackgroundImage.Dungeon, 8, 5, new List<Enemy> { new GiantSkeleton() , new Minion()}, 0.7);
        var c6 = new CombatArea("Fortress", BackgroundImage.Dungeon, 8, 6, new List<Enemy> { new Minion() , new Minotaur()}, 0.7);
        var c7 = new CombatArea("Fortress", BackgroundImage.Dungeon, 8, 7, new List<Enemy> { new GiantSkeleton() }, 0.8);
        var c8 = new BossArea("Dungeon", BackgroundImage.Dungeon, 7, 7, new DragonKnight());

        // Final
        var c9 = new EventArea("Fortress", BackgroundImage.Dungeon, 6, 7,
            new CutsceneArea("The path ahead leads to the deepest part of the fortress...","(I guess this is it... that must be where the Demon King is!)"));
        
        var c10 = new LocationArea("Fortress", BackgroundImage.Dungeon, 6, 8);
        var c11 = new LocationArea("Fortress", BackgroundImage.Dungeon, 6, 9);

        var c12 = new EventArea("Throne Room", BackgroundImage.Dungeon, 6, 10,
            new BossCutsceneArea(new Death()));


        MountainEnd.ConnectTo(KnightEncampment, Direction.East);
        KnightEncampment.ConnectTo(c1, Direction.South);
        c1.ConnectTo(c2, Direction.East);
        c2.ConnectTo(c3, Direction.East);

        c3.ConnectTo(c4, Direction.East);
        c4.ConnectTo(c5, Direction.East);
        c5.ConnectTo(c6, Direction.North);
        c6.ConnectTo(c7, Direction.North);
        c7.ConnectTo(c8, Direction.West);

        c8.ConnectTo(c9, Direction.West);
        c9.ConnectTo(c10, Direction.North);

        c10.ConnectTo(c11, Direction.North);
        c11.ConnectTo(c12, Direction.North);


        AddAreas(KnightEncampment, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12);
        #endregion

        StartingArea = dungeonStart; // c4

    }
}