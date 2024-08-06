using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Beyond_the_Sea // by ROCCO MICHEL | 2024
{
    internal class Program
    {
        static void Main()
        {
            string[] saveStyle = 
                ["[player name]", "[player level]", "[player xp]", "[stats (health, att, mag, attDef, magDef)]", "[player location]", "[inventory]"];

            Game.SaveSlots();

            // START UP
            Player TestSubject = new();
            Enemy Gnome = new()
            {
                level = 1,
                health = 100,
                name = "GNOME"
            };
            // TESTZONE START
            Shopkeeper john = new();
            john.ShopMenu();


            Enemy[] fight1 = [Gnome, Gnome, Gnome];
            TestSubject.Battle(fight1);

            // TESTZONE END

            Console.Title = "Beyond the Sea";
            DefaultColor();
            do
            {
                Console.Clear();
                Console.Write("WELCOME TO: ");
                SetColor("black", "white");
                Console.WriteLine(" BEYOND THE SEA ");
                DefaultColor();
                Console.Write("a console app by:\n[ROCCO MICHEL] | 2024\n");
                Console.WriteLine("\n\nPress: [ENTER] to Start!");
            } while (!Input.GetKeyDown(Input.KeyCode.ENTER));
            Console.Clear();

            // Creating Character
            Player Player = new();
            Player.PlayerCreator();
            Player.Inventory();
        }

        public class Game
        {
            static public void SaveSlots()
            {
                int[] targetSlots = Data.SaveFile.GetAllSlots();
                int selected = 0;
                bool _selecting = true;

                int error = 0;

                do
                {
                    DefaultColor();
                    Console.Clear();

                    // PRINT SCREEN

                    Console.Write(selected);

                    // Help
                    Console.WriteLine($"\n\n\n[HELP]\nMOVE: W/S | Up/Down Arrow Keys\nLOAD: [SPACE]\nDELETE: [X]\nBACK: [TAB]");

                    // ERRORS
                    if (error == 1) Error.Display("NA"); // // // // // // // // // // // // //
                    error = 0;

                    // INPUTS
                    ConsoleKey input = Console.ReadKey().Key;
                    switch(input)
                    {
                        // Navigating
                        case ConsoleKey.W:
                            selected--;
                            break;
                        case ConsoleKey.UpArrow:
                            selected--;
                            break;
                        case ConsoleKey.S:
                            selected++;
                            break;
                        case ConsoleKey.DownArrow:
                            selected++;
                            break;

                        // Exit
                        case ConsoleKey.Tab:
                            _selecting = false;
                            break;
                    }
                    
                    selected = Math.Clamp(selected, 0, targetSlots.Length);

                } while (_selecting);
            }
        }

        /*MAIN END*/

        /*CLASSES START*/

        // SHOPKEEPER CLASS
        public class Shopkeeper
        {
            string name = "NAME";
            int level = 1;

            public void ShopMenu()
            {
                bool _shoping = true;
                int selected = 0;
                int menu = -1;
                int error = 0;

                do
                {
                    DefaultColor();
                    Console.Clear();

                    // PRINT SCREEN
                    // Title
                    Console.Write($"\t    ");
                    SetColor("black", "white");
                    Console.WriteLine($" SHOPKEEPER [{name} | Lv. {level}]");
                    DefaultColor();

                    // Menu
                    CheckColor(0, true);
                    Console.Write("\n\n\n[BUY]\t");
                    CheckColor(1, true);
                    Console.Write("[SELL]\t");
                    CheckColor(2, true);
                    Console.Write("[CHAT]\t");
                    CheckColor(3, true);
                    Console.Write("[INSPECT]\n");
                    DefaultColor();

                    // Help
                    Console.WriteLine($"\n\n\n[HELP]\nBACK: [TAB]");

                    // ERRORS
                    if (error == 1) Error.Display("NA"); // // // // // // // // // // // // //
                    error = 0;

                    // INPUTS
                    ConsoleKey input = Console.ReadKey().Key;
                    switch (input)
                    {
                        // Navigating
                        case ConsoleKey.LeftArrow: if (menu == -1) selected--; break;
                        case ConsoleKey.RightArrow: if (menu == -1) selected++; break;
                        case ConsoleKey.A: if (menu == -1) selected--; break;
                        case ConsoleKey.D: if (menu == -1) selected++; break;

                        // Selecting
                        case ConsoleKey.Spacebar: if (menu == -1) menu = selected; break;

                        // Leaving
                        case ConsoleKey.Tab:
                            _shoping = false;
                            break;
                    }

                } while (_shoping);

                void CheckColor(int value, bool _menu)
                {
                    if (_menu)
                    {
                        if (menu == -1)
                        {
                            if (selected == value) SetColor("green", "black");
                            else DefaultColor();
                        }
                        else
                        {
                            if (menu == value) SetColor("green", "black");
                            else DefaultColor();
                        }
                    }
                    else
                    {
                        if (selected == value) SetColor("green", "black");
                        else DefaultColor();
                    }
                }
            }
        }

        // INPUT CLASS
        public class Input //!\\ NEEDS TO BE IN AN ACTIVE LOOP //!\\
        {
            public enum KeyCode 
            { 
                A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
                NUM1, NUM2, NUM3, NUM4, NUM5, NUM6, NUM7, NUM8, NUM9, NUM0, 
                SPACE, ENTER, TAB, BACKSPACE, INSERT, DELETE, HOME, END, PAGEUP, PAGEDOWN,
                ARROWUP, ARROWDOWN, ARROWLEFT, ARROWRIGHT, ESC, CLEAR, COMMA, MINUS, PLUS, PERIOD,
                F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, 
                NUMLOCK, SCROLLLOCK, PAUSEBREAK, PRINTSCREEN, 
                NUMPAD0, NUMPAD1, NUMPAD2, NUMPAD3, NUMPAD4, NUMPAD5, NUMPAD6, NUMPAD7, NUMPAD8, NUMPAD9, 
                NUMPADPERIOD, NUMPADSLASH, NUMPADASTERISK, NUMPADMINUS, NUMPADPLUS, NUMPADENTER 
            }
            public static bool GetKeyDown(KeyCode Key)
            {
                // https://learn.microsoft.com/en-us/dotnet/api/system.consolekey?view=net-8.0

                ConsoleKey consoleKey = Console.ReadKey().Key;
                switch (Key)
                {
                    case KeyCode.A:
                        if (consoleKey.Equals(ConsoleKey.A)) return true; break;
                    case KeyCode.B:
                        if (consoleKey.Equals(ConsoleKey.B)) return true; break;
                    case KeyCode.C:
                        if (consoleKey.Equals(ConsoleKey.C)) return true; break;
                    case KeyCode.D:
                        if (consoleKey.Equals(ConsoleKey.D)) return true; break;
                    case KeyCode.E:
                        if (consoleKey.Equals(ConsoleKey.E)) return true; break;
                    case KeyCode.F:
                        if (consoleKey.Equals(ConsoleKey.F)) return true; break;
                    case KeyCode.G:
                        if (consoleKey.Equals(ConsoleKey.G)) return true; break;
                    case KeyCode.H:
                        if (consoleKey.Equals(ConsoleKey.H)) return true; break;
                    case KeyCode.I:
                        if (consoleKey.Equals(ConsoleKey.I)) return true; break;
                    case KeyCode.J:
                        if (consoleKey.Equals(ConsoleKey.J)) return true; break;
                    case KeyCode.K:
                        if (consoleKey.Equals(ConsoleKey.K)) return true; break;
                    case KeyCode.L:
                        if (consoleKey.Equals(ConsoleKey.L)) return true; break;
                    case KeyCode.M:
                        if (consoleKey.Equals(ConsoleKey.M)) return true; break;
                    case KeyCode.N:
                        if (consoleKey.Equals(ConsoleKey.N)) return true; break;
                    case KeyCode.O:
                        if (consoleKey.Equals(ConsoleKey.O)) return true; break;
                    case KeyCode.P:
                        if (consoleKey.Equals(ConsoleKey.P)) return true; break;
                    case KeyCode.Q:
                        if (consoleKey.Equals(ConsoleKey.Q)) return true; break;
                    case KeyCode.R:
                        if (consoleKey.Equals(ConsoleKey.R)) return true; break;
                    case KeyCode.S:
                        if (consoleKey.Equals(ConsoleKey.S)) return true; break;
                    case KeyCode.T:
                        if (consoleKey.Equals(ConsoleKey.T)) return true; break;
                    case KeyCode.U:
                        if (consoleKey.Equals(ConsoleKey.U)) return true; break;
                    case KeyCode.V:
                        if (consoleKey.Equals(ConsoleKey.V)) return true; break;
                    case KeyCode.W:
                        if (consoleKey.Equals(ConsoleKey.W)) return true; break;
                    case KeyCode.X:
                        if (consoleKey.Equals(ConsoleKey.X)) return true; break;
                    case KeyCode.Y:
                        if (consoleKey.Equals(ConsoleKey.Y)) return true; break;
                    case KeyCode.Z:
                        if (consoleKey.Equals(ConsoleKey.Z)) return true; break;
                    case KeyCode.NUM1:
                        if (consoleKey.Equals(ConsoleKey.D1)) return true; break;
                    case KeyCode.NUM2:
                        if (consoleKey.Equals(ConsoleKey.D2)) return true; break;
                    case KeyCode.NUM3:
                        if (consoleKey.Equals(ConsoleKey.D3)) return true; break;
                    case KeyCode.NUM4:
                        if (consoleKey.Equals(ConsoleKey.D4)) return true; break;
                    case KeyCode.NUM5:
                        if (consoleKey.Equals(ConsoleKey.D5)) return true; break;
                    case KeyCode.NUM6:
                        if (consoleKey.Equals(ConsoleKey.D6)) return true; break;
                    case KeyCode.NUM7:
                        if (consoleKey.Equals(ConsoleKey.D7)) return true; break;
                    case KeyCode.NUM8:
                        if (consoleKey.Equals(ConsoleKey.D8)) return true; break;
                    case KeyCode.NUM9:
                        if (consoleKey.Equals(ConsoleKey.D9)) return true; break;
                    case KeyCode.NUM0:
                        if (consoleKey.Equals(ConsoleKey.D0)) return true; break;
                    case KeyCode.CLEAR:
                        if (consoleKey.Equals(ConsoleKey.OemClear)) return true; break;
                    case KeyCode.COMMA:
                        if (consoleKey.Equals(ConsoleKey.OemComma)) return true; break;
                    case KeyCode.PLUS:
                        if (consoleKey.Equals(ConsoleKey.OemPlus)) return true; break;
                    case KeyCode.MINUS:
                        if (consoleKey.Equals(ConsoleKey.OemMinus)) return true; break;
                    case KeyCode.PERIOD:
                        if (consoleKey.Equals(ConsoleKey.OemPeriod)) return true; break;
                    case KeyCode.SPACE:
                        if (consoleKey.Equals(ConsoleKey.Spacebar)) return true; break;
                    case KeyCode.ENTER:
                        if (consoleKey.Equals(ConsoleKey.Enter)) return true; break;
                    case KeyCode.TAB:
                        if (consoleKey.Equals(ConsoleKey.Tab)) return true; break;
                    case KeyCode.BACKSPACE:
                        if (consoleKey.Equals(ConsoleKey.Backspace)) return true; break;
                }
                return false;
            }
        }

        /*CLASSES END*/

        /*UNIVERSAL METHODS*/
        static public void PrintSquares(int filled, int size)
        {
            if (filled > size)
            {
                Error.Display("#000101");
                return;
            } 
            for (int i = 0; i < size + 1; i++)
            {
                if (i < filled) Console.Write("|");
                else if (i < size) Console.Write("-");
            }
            Console.Write($"[{filled}]");
        }

        static public void SetColor(string foreground, string background)
        {
            switch (foreground)
            {
                case "white":
                    Console.ForegroundColor = ConsoleColor.White; break;
                case "black":
                    Console.ForegroundColor = ConsoleColor.Black; break;
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray; break;
                case "grey":
                    Console.ForegroundColor = ConsoleColor.Gray; break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red; break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta; break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green; break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue; break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "darkblue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case "darkcyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case "darkgray":
                    Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case "darkgrey":
                    Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case "darkgreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case "darkmagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                case "darkred":
                    Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case "darkyellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow; break;
            }
            switch (background)
            {
                case "white":
                    Console.BackgroundColor = ConsoleColor.White; break;
                case "black":
                    Console.BackgroundColor = ConsoleColor.Black; break;
                case "gray":
                    Console.BackgroundColor = ConsoleColor.Gray; break;
                case "grey":
                    Console.BackgroundColor = ConsoleColor.Gray; break;
                case "red":
                    Console.BackgroundColor = ConsoleColor.Red; break;
                case "magenta":
                    Console.BackgroundColor = ConsoleColor.Magenta; break;
                case "yellow":
                    Console.BackgroundColor = ConsoleColor.Yellow; break;
                case "green":
                    Console.BackgroundColor = ConsoleColor.Green; break;
                case "blue":
                    Console.BackgroundColor = ConsoleColor.Blue; break;
                case "cyan":
                    Console.BackgroundColor = ConsoleColor.Cyan; break;
                case "darkblue":
                    Console.BackgroundColor = ConsoleColor.DarkBlue; break;
                case "darkcyan":
                    Console.BackgroundColor = ConsoleColor.DarkCyan; break;
                case "darkgray":
                    Console.BackgroundColor = ConsoleColor.DarkGray; break;
                case "darkgrey":
                    Console.BackgroundColor = ConsoleColor.DarkGray; break;
                case "darkgreen":
                    Console.BackgroundColor = ConsoleColor.DarkGreen; break;
                case "darkmagenta":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta; break;
                case "darkred":
                    Console.BackgroundColor = ConsoleColor.DarkRed; break;
                case "darkyellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow; break;
            }
        }
        
        static public void DefaultColor()
        {
            SetColor("white", "black");
        }
    }
}