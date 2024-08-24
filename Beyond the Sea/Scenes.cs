using System.Numerics;

namespace Beyond_the_Sea
{
    internal class Scenes
    {
        public static class Island
        {
            public static void Explore(Vector2 startingPosition)
            {
                bool _exploring = true;
                Vector2 playerPosition = startingPosition;

                // 20 char per line, 6 lines total
                string map = @"___________________
_____..........____
__....__________$__
__$______""""________
_$.___........_____
___________________";

                do
                {
                    Program.DefaultColor();
                    Console.Clear();

                    // Print map
                    Vector2 location = Vector2.Zero;
                    foreach (char c in map)
                    {
                        if (location != playerPosition) Console.Write(c);

                        else Console.Write('§');
                        if (c == '\n')
                        {
                            location.X = 0;
                            location.Y++;
                        }
                        else location.X++;
                    }

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
                    }

                    float posX = Math.Clamp(playerPosition.X, 0, location.X-1);
                    float posY = Math.Clamp(playerPosition.Y, 0, location.Y);
                    playerPosition = new Vector2(posX, posY);

                } while (_exploring);
            }
        }
    }
}
