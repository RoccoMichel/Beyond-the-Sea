using static Beyond_the_Sea.NPC;
using static System.Console;
using System.Numerics;

namespace Beyond_the_Sea // by ROCCO MICHEL | 2024
{
    internal class Program
    {
        static public int saveSlot = 0;
        static void Main()
        {
            // HOLY SHIT I SHOULD FIGURE OUT SOME KIND OF COLOR FADE EFFECT (using lerps???)

            Scenes.Island.Explore(new Vector2(10, 3));

            /* TEST ZONE END */

            Title = "Beyond the Sea";
            DefaultColor();
            do
            {
                Clear();
                Write("WELCOME TO: ");
                SetColor("black", "white");
                WriteLine(" BEYOND THE SEA ");
                DefaultColor();
                Write("a console app by:\n[ROCCO MICHEL] | 2024\n");
                WriteLine("\n\nPress: [ENTER] to Start!");
            } while (!Input.GetKeyDown(Input.KeyCode.ENTER));
            Clear();


            string[] saveStyle =
                ["[player name]", "[player level]", "[player xp]", "[stats (health, att, mag, attDef, magDef)]", "[player location]", "[inventory]"];


            // Creating Character
            Player Player = new();
            Player.PlayerCreator();

            //Game.SaveSlots();

            // START UP


            Enemy[] fight1 = [Templates.Gnome, Templates.Gnome, Templates.Gnome];
            Player.Battle(fight1);

            //END
            return;
        }

        public class Game
        {
            public static void PauseMenu()
            {
                int selected = 0;
                bool _paused = true;

                do
                {
                    // PRINT SCREEN
                    SetColor("black", "white");
                    WriteLine(" PAUSED ");
                    DefaultColor();
                    WriteLine(" RESUME GAME ");
                    WriteLine(" SAVES SLOTS ");
                    WriteLine(" QUIT PROGRAM ");



                    // Inputs
                    ConsoleKey input = ReadKey().Key;

                    switch (input)
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

                        // Select
                        case ConsoleKey.Enter:
                            if (selected == 0) _paused = false; // resume
                            if (selected == 2) SaveSlots(); // manage save slots
                            if (selected == 2 && Confirmation()) Environment.Exit(0); // exit
                            break;
                    }
                } while (_paused);
            }

            static public void SaveSlots()
            {

                int selected = 0;
                bool _selecting = true;
                int error = 0;

                do
                {
                    int[] targetSlots = Data.SaveFile.GetAllSlots();
                    DefaultColor();
                    Clear();

                    // PRINT SCREEN
                    for(int i = 0; i < targetSlots.Length; i++)
                    {
                        CheckColor(i);
                        WriteLine($"\t   [ SLOT {i+1} ]");
                        WriteLine(Data.SaveFile.Display(targetSlots[i]) + '\n');
                    }

                    CheckColor(targetSlots.Length);
                    WriteLine("\t   [NEW SLOT]\n CREATE A NEW SAVE SLOT");
                    DefaultColor();

                    // Help
                    if (selected == targetSlots.Length) 
                        WriteLine($"\n\n\n[HELP]\nMOVE: [W/S] | [Up/Down]Arrow Keys\nCREATE: [ENTER]\nDELETE: [DEL]\nBACK: [TAB]");
                    else
                        WriteLine($"\n\n\n[HELP]\nMOVE: [W/S] | [Up/Down]Arrow Keys\nLOAD: [ENTER]\nDELETE: [DEL]\nBACK: [TAB]");

                    // ERRORS
                    if (error == 1) Error.Display("#000302"); // // // // // // // // // // // //
                    error = 0;

                    // INPUTS
                    ConsoleKey input = ReadKey().Key;
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

                        // Load OR Create new
                        case ConsoleKey.Enter:
                            if (saveSlot != selected && Confirmation())
                            {
                                string[] TEMP = ["REPLACE", "LATER", "with game start up"]; // // // // //

                                if (selected == targetSlots.Length) // NEW FILE
                                    Data.SaveFile.Set(TEMP, -1);
                                else Data.SaveFile.Load(targetSlots[selected]);




                                saveSlot = selected;
                            }
                            else if (saveSlot == selected) error = 1;
                            break;

                        // Delete File
                        case ConsoleKey.Delete:
                            if(selected != targetSlots.Length && Confirmation())
                                Data.SaveFile.Delete(targetSlots[selected]);
                            break;

                        // Exit
                        case ConsoleKey.Tab:
                            _selecting = false;
                            break;
                    }

                    selected = Math.Clamp(selected, 0, targetSlots.Length);

                } while (_selecting);

                void CheckColor(int value)
                {
                    if (selected == value) SetColor("green", "black");
                    else DefaultColor();
                }
            }

            static public bool Confirmation()
            {
                bool choice = false;
                bool _choosing = true;

                do
                {
                    DefaultColor();
                    Clear();

                    // PRINT SCREEN
                    WriteLine("CONFIRM CHOICE\n");
                    if (choice)
                    {
                        SetColor("black", "white");
                        WriteLine(" >[CONFIRM]< ");
                        DefaultColor();
                        WriteLine("  |CANCEL | ");
                    }
                    else
                    {
                        WriteLine("  |CONFIRM|  ");
                        SetColor("black", "white");
                        WriteLine(" >[CANCEL ]< ");
                        DefaultColor();
                    }


                    // HELP
                    WriteLine($"\n\n\n[HELP]\nMOVE: W/S | Up/Down Arrow Keys\nSELECT: [ENTER]\n\n\n");

                    // INPUT
                    ConsoleKey input = ReadKey().Key;

                    switch (input)
                    {
                        case ConsoleKey.W:
                            choice = !choice;
                            break;
                        case ConsoleKey.S:
                            choice = !choice;
                            break;
                        case ConsoleKey.UpArrow:
                            choice = !choice;
                            break;
                        case ConsoleKey.DownArrow:
                            choice = !choice;
                            break;

                        case ConsoleKey.Enter:
                            _choosing = false;
                            break;
                    }

                } while (_choosing);

                Clear();
                Write("working...");
                Thread.Sleep(250);

                return choice;
            }
        }

        /*MAIN END*/

        /*CLASSES START*/

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

                ConsoleKey consoleKey = ReadKey().Key;
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

        // PRINT LETTERS

        /// <summary>
        /// Writes each character within a string. 
        /// At a default speed of 100 ms (1/10 of a second). 
        /// Ends on the next line. 
        /// </summary>
        /// <param name="print">Target array of characters.</param>
        static public void WriteLetters(string print)
        {
            foreach (char c in print)
            {
                Write(c);
                Thread.Sleep(100);
            }

            Write('\n');
        }

        /// <summary>
        /// Writes each character within a string. 
        /// Ends on the next line. 
        /// </summary>
        /// <param name="print">Target array of characters.</param>
        /// <param name="speed">The number of milliseconds between each character.</param>
        static public void WriteLetters(string print, int speed)
        {
            foreach (char c in print)
            {
                Write(c);
                Thread.Sleep(speed);
            }

           Write('\n');
        }

        /// <summary>
        /// Writes each character within a string. 
        /// At a default speed of 100 ms (1/10 of a second). 
        /// </summary>
        /// <param name="print">Target array of characters.</param>
        /// <param name="endLine">If printing should end on the next line.</param>
        static public void WriteLetters(string print, bool endLine)
        {
            foreach (char c in print)
            {
                Write(c);
                Thread.Sleep(100);
            }

            if (endLine) Write('\n');
        }

        /// <summary>
        /// Writes each character within a string. 
        /// </summary>
        /// <param name="print">Target array of characters.</param>
        /// <param name="speed">The number of milliseconds between each character.</param>
        /// <param name="endLine">If printing should end on the next line.</param>
        static public void WriteLetters(string print, bool endLine, int speed)
        {
            foreach (char c in print)
            {
                Write(c);
                Thread.Sleep(speed);
            }

            if (endLine) Write('\n');
        }

        /// <summary>
        /// Writes each character within a string. 
        /// With random pause length based on a min and max value. 
        /// </summary>
        /// <param name="print">Target array of characters.</param>
        /// <param name="endLine">If printing should end on the next line.</param>
        /// <param name="minSpeed">Minimum possible pause length INCLUSIVE (Milliseconds)</param>
        /// <param name="maxSpeed">Maximum possible pause length INCLUSIVE (Milliseconds)</param>
        static public void WriteLetters(string print, bool endLine, int minSpeed, int maxSpeed)
        {
            Random random = new();
            foreach (char c in print)
            {
                Write(c);
                Thread.Sleep(random.Next(minSpeed, maxSpeed + 1));
            }

            if (endLine) Write('\n');
        }

        // PRINT WORDS

        /// <summary>
        /// Writes out a string with pauses at each space or other special character. 
        /// At a default pause of 500ms (1/2 second) between each word. 
        /// Ends on the next Line. 
        /// </summary>
        /// <param name="print">Target string that will be written.</param>
        static public void WriteWords(string print)
        {
            foreach (char c in print)
            {
                Write(c);
                if (c == ' ' || c == '.' || c == '?' || c == '!' || c == '-' || c == '=' || c == '\n') 
                    Thread.Sleep(500);
            }

            Write('\n');
        }

        /// <summary>
        /// Writes out a string with pauses at each space or other special character. 
        /// Ends on the next Line. 
        /// </summary>
        /// <param name="print">Target string that will be written.</param>
        /// <param name="speed">The number of milliseconds between each character.</param>
        static public void WriteWords(string print, int speed)
        {
            foreach (char c in print)
            {
                Write(c);
                if (c == ' ' || c == '.' || c == '?' || c == '!' || c == '-' || c == '=' || c == '\n') 
                    Thread.Sleep(speed);
            }

            Write('\n');
        }

        /// <summary>
        /// Writes out a string with pauses at each space or other special character. 
        /// At a default pause of 500ms (1/2 second) between each word. 
        /// </summary>
        /// <param name="print">Target string that will be written.</param>
        /// <param name="endLine">If printing should end on the next line.</param>
        static public void WriteWords(string print, bool endLine)
        {
            foreach (char c in print)
            {
                Write(c);
                if (c == ' ' || c == '.' || c == '?' || c == '!' || c == '-' || c == '=' || c == '\n') 
                    Thread.Sleep(500);
            }

            if (endLine) Write('\n');
        }

        /// <summary>
        /// Writes out a string with pauses at each space or other special character. 
        /// </summary>
        /// <param name="print">Target string that will be written.</param>
        /// <param name="speed">The number of milliseconds between each character.</param>
        /// <param name="endLine">If printing should end on the next line.</param>
        static public void WriteWords(string print, bool endLine, int speed)
        {
            foreach (char c in print)
            {
                Write(c);
                if (c == ' ' || c == '.' || c == '?' || c == '!' || c == '-' || c == '=' || c == '\n') 
                    Thread.Sleep(speed);
            }

            if (endLine) Write('\n');
        }

        /// <summary>
        /// Writes out a string with random pause length based on 
        /// a min and max value at each space or other special character. 
        /// </summary>
        /// <param name="print">Target string that will be written.</param>
        /// <param name="endLine">If printing should end on the next line.</param>
        /// <param name="minSpeed">Minimum possible pause length INCLUSIVE (Milliseconds)</param>
        /// <param name="maxSpeed">Maximum possible pause length INCLUSIVE (Milliseconds)</param>
        static public void WriteWords(string print, bool endLine, int minSpeed, int maxSpeed)
        {
            Random random = new();
            foreach (char c in print)
            {
                Write(c);
                if (c == ' ' || c == '.' || c == '?' || c == '!' || c == '-' || c == '=' || c == '\n') 
                    Thread.Sleep(random.Next(minSpeed, maxSpeed + 1));
            }

            if (endLine) Write('\n');
        }


        static public void PrintSquares(int filled, int size)
        {
            if (filled > size)
            {
                Error.Display("#000101");
                return;
            } 
            for (int i = 0; i < size + 1; i++)
            {
                if (i < filled) Write("|");
                else if (i < size) Write("-");
            }
            Write($"[{filled}]");
        }

        static public void SetColor(string foreground, string background)
        {
            switch (foreground)
            {
                case "white":
                    ForegroundColor = ConsoleColor.White; break;
                case "black":
                    ForegroundColor = ConsoleColor.Black; break;
                case "gray":
                    ForegroundColor = ConsoleColor.Gray; break;
                case "grey":
                    ForegroundColor = ConsoleColor.Gray; break;
                case "red":
                    ForegroundColor = ConsoleColor.Red; break;
                case "magenta":
                    ForegroundColor = ConsoleColor.Magenta; break;
                case "yellow":
                    ForegroundColor = ConsoleColor.Yellow; break;
                case "green":
                    ForegroundColor = ConsoleColor.Green; break;
                case "blue":
                    ForegroundColor = ConsoleColor.Blue; break;
                case "cyan":
                    ForegroundColor = ConsoleColor.Cyan; break;
                case "darkblue":
                    ForegroundColor = ConsoleColor.DarkBlue; break;
                case "darkcyan":
                    ForegroundColor = ConsoleColor.DarkCyan; break;
                case "darkgray":
                    ForegroundColor = ConsoleColor.DarkGray; break;
                case "darkgrey":
                    ForegroundColor = ConsoleColor.DarkGray; break;
                case "darkgreen":
                    ForegroundColor = ConsoleColor.DarkGreen; break;
                case "darkmagenta":
                    ForegroundColor = ConsoleColor.DarkMagenta; break;
                case "darkred":
                    ForegroundColor = ConsoleColor.DarkRed; break;
                case "darkyellow":
                    ForegroundColor = ConsoleColor.DarkYellow; break;
            }
            switch (background)
            {
                case "white":
                    BackgroundColor = ConsoleColor.White; break;
                case "black":
                    BackgroundColor = ConsoleColor.Black; break;
                case "gray":
                    BackgroundColor = ConsoleColor.Gray; break;
                case "grey":
                    BackgroundColor = ConsoleColor.Gray; break;
                case "red":
                    BackgroundColor = ConsoleColor.Red; break;
                case "magenta":
                    BackgroundColor = ConsoleColor.Magenta; break;
                case "yellow":
                    BackgroundColor = ConsoleColor.Yellow; break;
                case "green":
                    BackgroundColor = ConsoleColor.Green; break;
                case "blue":
                    BackgroundColor = ConsoleColor.Blue; break;
                case "cyan":
                    BackgroundColor = ConsoleColor.Cyan; break;
                case "darkblue":
                    BackgroundColor = ConsoleColor.DarkBlue; break;
                case "darkcyan":
                    BackgroundColor = ConsoleColor.DarkCyan; break;
                case "darkgray":
                    BackgroundColor = ConsoleColor.DarkGray; break;
                case "darkgrey":
                    BackgroundColor = ConsoleColor.DarkGray; break;
                case "darkgreen":
                    BackgroundColor = ConsoleColor.DarkGreen; break;
                case "darkmagenta":
                    BackgroundColor = ConsoleColor.DarkMagenta; break;
                case "darkred":
                    BackgroundColor = ConsoleColor.DarkRed; break;
                case "darkyellow":
                    BackgroundColor = ConsoleColor.DarkYellow; break;
            }
        }
        
        static public void DefaultColor()
        {
            SetColor("white", "black");
        }
    }
}