using System.Numerics;

namespace Beyond_the_Sea
{
    internal class Scenes
    {
        public static class Island
        {
            public static void Explore(Vector2 startingPosition, Maps.MapData mapData)
            {
                bool _exploring = true;
                Vector2 playerPosition = startingPosition;

                do
                {
                    Program.DefaultColor();
                    Console.Clear();

                    // Print map
                    Console.Write(mapData.header);

                    Vector2 location = Vector2.Zero;
                    foreach (char c in mapData.mapVisual)
                    {
                        if (location != playerPosition) Console.Write(c);
                        else Console.Write('|'); // Player Character

                        if (c == '\n')
                        {
                            location.X = 0;
                            location.Y++;
                        }
                        else location.X++;
                    }

                    Console.Write($"\n{mapData.footer}\n\n");

                    // Help
                    foreach (Vector2 blocked in mapData.blockedArea) Console.Write(blocked.ToString()+ ", ");
                    Console.WriteLine($"\n\n[HELP] {playerPosition}\nMOVE: W/A/S/D | Arrow Keys\n\n");

                    // Inputs
                    ConsoleKey input = Console.ReadKey().Key;
                    switch (input)
                    {
                        case ConsoleKey.D:
                            playerPosition.X++;
                            break;
                        case ConsoleKey.S:
                            playerPosition.Y++;
                            break;
                        case ConsoleKey.A:
                            playerPosition.X--;
                            break;
                        case ConsoleKey.W:
                            playerPosition.Y--;
                            break;

                        case ConsoleKey.RightArrow:
                            playerPosition.X++;
                            break;
                        case ConsoleKey.DownArrow:
                            playerPosition.Y++;
                            break;
                        case ConsoleKey.LeftArrow:
                            playerPosition.X--;
                            break;
                        case ConsoleKey.UpArrow:
                            playerPosition.Y--;
                            break;
                    }

                    Vector2 newPos = Vector2.Zero;
                    newPos.X = Math.Clamp(playerPosition.X, 0, location.X-1);
                    newPos.Y = Math.Clamp(playerPosition.Y, 0, location.Y);

                    foreach (Vector2 blockedArea in mapData.blockedArea)
                        if (blockedArea.X == newPos.X && blockedArea.Y == newPos.Y) newPos = playerPosition;

                    playerPosition = newPos;

                } while (_exploring);
            }
        }

        public static Maps Levels { get; set; } = new Maps();

        public class Maps
        {
            // C[chapterIndex]_[Part]
            public class MapData
            {
                public string mapVisual = @"/!\MAP VISUALS MISSING /!\";
                public Vector2[]? blockedArea; // top2bottom & left2right, start at 0
                public string? header; // for headers, sky or other inaccessible rows above the player
                public string? footer; // for footers, underground or other inaccessible rows below the player
            }

            public MapData C0_0 = new()
            {
                header = "THE WILD FOREST\n",
                mapVisual =
@"___________________
_____..........____
__....__________$__
__$______¨¨________
_$.___........_____
___________________",

                blockedArea = [new(16, 2), new (2, 3), new (1, 2)], // for $ (trees)
            };

            public MapData C0_1 = new()
            {
                mapVisual =
@"___________________
......../´\........
''''''''[ ]''''''''
WWWWWWWWWWWWWWWWWWW",

                footer =
@"/\/\/\/\/\/\/\/\/\/
\/\/\/\/\/\/\/\/\/\
/\/\/\/\/\/\/\/\/\/
/\/\/\/\/\/\/\/\/\\",

                blockedArea = [new(8, 1), new(9, 1), new(10, 1), new(8, 2), new(10, 2)], // for house
            };


        }
    }
}
