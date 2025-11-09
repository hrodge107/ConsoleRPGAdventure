namespace ConsoleRPGAdventure;

public enum Direction
{
    North, East, South, West
}

public abstract class Area
{
    public string Name { get; set; }
    public AsciiImage BackgroundArt { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public AreaEvent? Event {  get; set; }

    public Area North { get; set; }
    public Area South { get; set; }
    public Area East { get; set; }
    public Area West { get; set; }

    public Area(string name, AsciiImage backgroundArt, int x, int y)
    {
        Name = name;
        BackgroundArt = backgroundArt;
        X = x;
        Y = y;
        Event = null;
    }

    // Exit Connectors

    public void ConnectTo(Area targetArea, Direction direction) // <-- logic being, if an area exit is north, then that target area's exit will be south. So that the user can go back.
    {
        switch (direction)
        {
            case Direction.North:
                this.North = targetArea;
                targetArea.South = this;
                break;
            case Direction.East:
                this.East = targetArea;
                targetArea.West = this;
                break;
            case Direction.South:
                this.South = targetArea;
                targetArea.North = this;
                break;
            case Direction.West:
                this.West = targetArea;
                targetArea.East = this;
                break;
        }
    }

    public void ConnectExits(params (Direction direction, Area targetArea)[] exits)
    {
        // Same logic, but for multiple exits
        foreach (var exit in exits)
        {
            ConnectTo(exit.targetArea, exit.direction);
        }
    }

}


public class LocationArea : Area
{
    public LocationArea(string name, AsciiImage backgroundArt, int x, int y) : base(name, backgroundArt, x, y) { }
}


public class EventArea : Area // <-- maybe add fun side games here too
{
    public EventArea(string name, AsciiImage backgroundArt, int x, int y, AreaEvent eventToAssign) : base(name, backgroundArt, x, y)
    {
        Event = eventToAssign;
    }
}


public class CombatArea : Area
{
    // A list of all enemies that could spawn here.
    public List<Enemy> PotentialEnemies { get; set; }
    public double SpawnChance { get; set; } // Probability of monster spawning (0.0 to 1.0)

    public CombatArea(string name, AsciiImage backgroundArt, int x, int y, List<Enemy> potentialEnemies, double spawnChance = 0.5) : base(name, backgroundArt, x, y)
    {
        PotentialEnemies = potentialEnemies;
        SpawnChance = spawnChance;
    }
}

public class BossArea : Area
{
    public bool IsCleared { get; set; }
    public Enemy Boss { get; set; }

    public BossArea(string name, AsciiImage backgroundArt, int x, int y, Enemy boss) : base(name, backgroundArt, x, y)
    {
        Boss = boss;
    }
}