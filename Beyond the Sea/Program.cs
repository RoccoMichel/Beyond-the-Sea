using System.Globalization;
using System.Numerics;
using static Beyond_the_Sea.NPC;
using static System.Console;

namespace Beyond_the_Sea // by ROCCO MICHEL | 2025
{
    internal class Program
    {
        static public int saveSlot = 0;

        static void Main()
        {
            // HOLY SHIT I SHOULD FIGURE OUT SOME KIND OF COLOR FADE EFFECT (using lerps???)

            Scenes.Island.Explore(new Vector2(10, 3), Scenes.Levels.C0_1);

            /* TEST ZONE END */

            Game.SaveSlots();

            Title = "Beyond the Sea";
            DefaultColor();
            do
            {
                Clear();
                Write("WELCOME TO: ");
                SetColor(ConsoleColor.Black, ConsoleColor.White);
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
                    SetColor(ConsoleColor.Black, ConsoleColor.White);
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
                    if (selected == value) SetColor(ConsoleColor.Green, ConsoleColor.Black);
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
                        SetColor(ConsoleColor.Black, ConsoleColor.White);
                        WriteLine(" >[CONFIRM]< ");
                        DefaultColor();
                        WriteLine("  |CANCEL | ");
                    }
                    else
                    {
                        WriteLine("  |CONFIRM|  ");
                        SetColor(ConsoleColor.Black, ConsoleColor.White);
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

        static public void SetColor(ConsoleColor foreground, ConsoleColor background)
        {
            ForegroundColor = foreground;
            BackgroundColor = background;
        }
        
        static public void DefaultColor()
        {
            SetColor(ConsoleColor.White, ConsoleColor.Black);
        }
    }
    internal class Items
    {
        // AVAILABLE ITEMS
        readonly public Item apple = new()
        {
            name = "APPLE",
            icon = " ó ",
            description = "A delicious looking, fresh, red Apple. Just waiting to be eaten",
            level = 1,
            effectiveness = 5,
            type = Item.ItemTypes.FOOD,
            rarity = Item.Rarities.COMMON
        };


        // ITEM PARENT CLASS
        public class Item
        {
            public string name = "NULL";
            public string icon = "...";
            public string description = "[DISCRIPTION HERE]";
            public string[] bonuses = new string[3];

            public int level = 1;
            public int effectiveness = 1;
            public ItemTypes type = ItemTypes.FOOD;
            public Rarities rarity = Rarities.COMMON;

            public enum Rarities { COMMON, UNCOMMON, RAR, EPIC, MYTHIC, LEGENDARY, unobtainable }
            public enum ItemTypes { FOOD, ARMOUR, MELEEWEAPON, MAGICWEAPON, VALUEABLE }

            public void Use(Player player)
            {
                switch (type)
                {
                    // Healing
                    case ItemTypes.FOOD:
                        player.health += effectiveness;
                        break;
                    // Defense
                    case ItemTypes.ARMOUR:

                        player.meleeDefense += effectiveness;
                        player.magicDefense += effectiveness;
                        break;
                    // Melee Damage
                    case ItemTypes.MELEEWEAPON:
                        player.meleeDamage += effectiveness;
                        break;
                    // Magic Damage
                    case ItemTypes.MAGICWEAPON:
                        player.magicDamage += effectiveness;
                        break;
                }
            }
        }
    }
    internal class Player
    {
        // VARIABLES
        public string name = "YOU";
        public float health;
        public float maxHealth;
        public float meleeDamage;
        public float meleeDefense;
        public float magicDamage;
        public float magicDefense;

        public int level = 1;
        public float exp; // exp req for next lvl is: (int)100*(level*1.45)

        public List<Items.Item> inventory = [];

        public void SetValues(string name, float health, float meleeDamage, float meleeDefense, float magicDamage, float magicDefense)
        {
            this.name = name; this.health = health; maxHealth = health; this.meleeDamage = meleeDamage; this.meleeDefense = meleeDefense; this.magicDamage = magicDamage; this.magicDefense = magicDefense;
        }

        public void Battle(Enemy[] enemies)
        {
            Random random = new();
            bool _fighting = true;
            int selected = 0;
            int menu = -1;
            int error = 0;
            int successEXP = 0;
            foreach (Enemy enemy in enemies) successEXP += random.Next(5, 10) * enemy.level;


            do
            {
                Program.DefaultColor();
                Clear();

                // PRINT SCREEN
                // [ITEM]
                if (menu == 2) { Inventory(); menu = -1; }


                // Title
                Write($"\t    ");
                Program.SetColor(ConsoleColor.Black, ConsoleColor.White);
                WriteLine(" BATTLE ");
                Program.DefaultColor();

                // Enemies
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (menu != -1) CheckColor(i, false);
                    if (menu != -1 && i == selected) Write($">{enemies[i].name}< | HP: {enemies[i].health}");
                    else Write($" {enemies[i].name}  | HP: {enemies[i].health}");
                    if (menu == 3 && i == selected) Write($"\t[Lv.{enemies[i].level}]");
                    Write('\n');
                }

                // Menu
                CheckColor(0, true);
                Write("\n\n\n[MELEE]\t");
                CheckColor(1, true);
                Write("[MAGIC]\t");
                CheckColor(2, true);
                Write("[ITEM]\t");
                CheckColor(3, true);
                Write("[INSPECT]\n");

                // health
                Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                Write("HP: ");
                Program.PrintSquares(10, 10);
                Program.DefaultColor();

                // Help
                WriteLine($"\n\n\n[HELP]");
                if (menu == -1) WriteLine("MOVE: A/D | Left/Right Arrow Keys\nSELECT: [SPACE]");
                if (menu == 0 || menu == 1 || menu == 3) WriteLine("MOVE: W/S | Up/Down Arrow Keys\nSELECT: [SPACE]\nBACK: [TAB]");

                // ERRORS
                if (error == 1) Error.Display("NA"); // // // // // // // // // // // // //
                error = 0;

                // INPUTS
                ConsoleKey input = ReadKey().Key;
                switch (input)
                {
                    // Navigation
                    // bottom menu
                    case ConsoleKey.LeftArrow: if (menu == -1) selected--; break;
                    case ConsoleKey.RightArrow: if (menu == -1) selected++; break;
                    case ConsoleKey.A: if (menu == -1) selected--; break;
                    case ConsoleKey.D: if (menu == -1) selected++; break;
                    // enemy
                    case ConsoleKey.UpArrow: if (menu != -1) selected--; break;
                    case ConsoleKey.DownArrow: if (menu != -1) selected++; break;
                    case ConsoleKey.W: if (menu != -1) selected--; break;
                    case ConsoleKey.S: if (menu != -1) selected++; break;

                    // Selecting
                    case ConsoleKey.Spacebar:
                        if (menu == -1) // menu
                        {
                            menu = selected;
                            selected = 0;
                        }
                        else // enemies
                        {
                            if (menu == 0) enemies[selected].TakeDamage(meleeDamage);
                            if (menu == 1) enemies[selected].TakeDamage(magicDamage);

                            menu = -1;
                            selected = 0;
                        }
                        break;


                    // Leaving
                    case ConsoleKey.Tab: menu = -1; selected = 0; break;

                    // DEBUG?
                    case ConsoleKey.X: _fighting = false; break;
                }

                if (menu == -1) selected = Math.Clamp(selected, 0, 3);
                else selected = Math.Clamp(selected, 0, enemies.Length - 1);

                // Victory check
                bool victory = true;
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.health <= 0 && victory) victory = true;
                    else victory = false;
                }
                if (_fighting) _fighting = !victory;
            } while (_fighting);

            // VICTORY
            Clear();
            Write("YOU WON!\nYou got: [");
            Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
            Write($"+{successEXP} EXP");
            exp += successEXP;
            Program.DefaultColor();
            WriteLine($"]\n{exp} / {(int)100 * (level * 1.45)} EXP\n\nPress [ANY KEY] to continue.");
            ReadKey();

            Program.DefaultColor();
            Clear();

            void CheckColor(int value, bool _menu)
            {
                if (_menu)
                {
                    if (menu == -1)
                    {
                        if (selected == value) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                        else Program.DefaultColor();
                    }
                    else
                    {
                        if (menu == value) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                        else Program.DefaultColor();
                    }
                }
                else
                {
                    if (selected == value) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                    else Program.DefaultColor();
                }
            }
        }

        public void Inventory()
        {
            string[] itemDescription = ["- RARITY | NAME | Lv -", "- DISCRIPTION -", "- Bonuses? -\n\n\n"];
            int page = 1;
            int error = 0;
            int selected = 0;
            bool _browsing = true;

            do
            {
                Program.DefaultColor();
                Clear();

                // PAGE PRINT
                Write($"[{page}/{((int)inventory.Count / 15) + 1}]\t      ");
                Program.SetColor(ConsoleColor.Black, ConsoleColor.White);
                WriteLine(" INVETORY ");
                Program.DefaultColor();
                for (int i = 0; i < 15; i++)
                {
                    if (i + (page - 1) * 15 == selected) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);

                    if (inventory.Count - 1 >= i + (page - 1) * 15 && inventory[0] != null) Write($"[{inventory[i + (page - 1) * 15].icon}]\t");
                    else Write("[...]\t");

                    if ((i + 1) % 5 == 0) Write("\n\n\n");
                    Program.DefaultColor();
                }

                if (inventory.Count - 1 >= selected)
                {
                    itemDescription[0] = $"[{inventory[selected].rarity}] {inventory[selected].name} | {inventory[selected].level}";
                    itemDescription[1] = inventory[selected].description;
                    itemDescription[2] = $"{inventory[selected].bonuses[0]}\n{inventory[selected].bonuses[1]}\n{inventory[selected].bonuses[2]}\n";
                }
                else
                {
                    itemDescription[0] = "[EMPTY]";
                    itemDescription[1] = string.Empty;
                    itemDescription[2] = "\n\n\n";
                }

                WriteLine(itemDescription[0]);
                WriteLine(itemDescription[1]);
                WriteLine(itemDescription[2]);
                WriteLine("\n\n[HELP]\nMOVE: W/A/S/D | Arrow Keys\nFLIP PAGE: E/Q\nSELECT: [SPACE]\nBACK: [TAB]");

                // ERRORS
                if (error == 1) Error.Display("NA"); // // // // // // // // // // // //
                error = 0;

                // INPUTS
                ConsoleKey input = ReadKey().Key;
                switch (input)
                {
                    // Navigating
                    case ConsoleKey.UpArrow:
                        if (selected > 4 + (page - 1) * 15) selected -= 5; break;
                    case ConsoleKey.DownArrow:
                        if (selected < 10 + (page - 1) * 15) selected += 5; break;
                    case ConsoleKey.LeftArrow:
                        selected--; break;
                    case ConsoleKey.RightArrow:
                        selected++; break;
                    case ConsoleKey.W:
                        if (selected > 4 + (page - 1) * 15) selected -= 5; break;
                    case ConsoleKey.S:
                        if (selected < 10 + (page - 1) * 15) selected += 5; break;
                    case ConsoleKey.A:
                        selected--; break;
                    case ConsoleKey.D:
                        selected++; break;

                    // Page
                    case ConsoleKey.E:
                        if (((int)inventory.Count / 15) + 1 != page) selected += 15;
                        page++;
                        break;
                    case ConsoleKey.Q:
                        if (page != 1) selected -= 15;
                        page--;
                        break;

                    // Using
                    case ConsoleKey.Spacebar:
                        if (inventory.Count - 1 >= selected) inventory[selected].Use(this);
                        break;

                    // Leaving
                    case ConsoleKey.Tab: _browsing = false; break;
                }
                page = Math.Clamp(page, 1, ((int)inventory.Count / 15) + 1);
                selected = Math.Clamp(selected, (15 * page) - 15, (15 * page) - 1);

            } while (_browsing);

            Program.DefaultColor();
            Clear();
        }

        public void Statistics()
        {
            Clear();
            Write($"[{name}] | Lv: [{level}]\n");
            Write($"{exp} of {(int)100 * (level * 1.45f)} EXP\t(until next level)\n");

            Write("\nHealth:\t\t");
            Program.PrintSquares((int)(health - 10) / 10, 9);

            Write("\nMelee Attack:\t");
            Program.PrintSquares((int)(meleeDamage - 10) / 10, 9);

            Write("\nMelee Defense:\t");
            Program.PrintSquares((int)(meleeDefense - 10) / 10, 9);

            Write("\nMagic Attack:\t");
            Program.PrintSquares((int)(magicDamage - 10) / 10, 9);

            Write("\nMagic Defense:\t");
            Program.PrintSquares((int)(magicDefense - 10) / 10, 9);

            Write("\nPress [ANY KEY] to return");
            ReadKey();
            Clear();
            Program.DefaultColor();
        }

        public void PlayerCreator()
        {
            bool _done = false;
            int availablePoints = 8 + level;
            string userName;

            int error = 0;

            int selected;
            int healthPoints = 0;
            int meleeAttPoints = 0;
            int meleeDefPoints = 0;
            int magicAttPoints = 0;
            int magicDefPoints = 0;

            // PLAYER NAME
            do
            {
                Clear();
                Program.DefaultColor();
                Write("Enter your Character's Name: ");
                Program.SetColor(ConsoleColor.DarkBlue, ConsoleColor.Cyan);
#pragma warning disable CS8600 // while statment checks if it is null
                userName = ReadLine();
#pragma warning restore CS8600 // so no warning needed
                Program.DefaultColor();
            } while (string.IsNullOrWhiteSpace(userName));

            // PLAYER STATS
            selected = 0;
            while (!_done)
            {
                Clear();
                Write($"[{userName}] | POINTS LEFT: [");
                if (error == 2) Program.SetColor(ConsoleColor.Red, ConsoleColor.Black);
                Write($"{availablePoints}");
                Program.DefaultColor();
                WriteLine(']');

                CheckColor(0);
                Write("\nHealth:\t\t");
                Program.PrintSquares(healthPoints, 9);
                CheckColor(1);
                Write("\nMelee Attack:\t");
                Program.PrintSquares(meleeAttPoints, 9);
                CheckColor(2);
                Write("\nMelee Defense:\t");
                Program.PrintSquares(meleeDefPoints, 9);
                CheckColor(3);
                Write("\nMagic Attack:\t");
                Program.PrintSquares(magicAttPoints, 9);
                CheckColor(4);
                                Write("\nMagic Defense:\t");
                Program.PrintSquares(magicDefPoints, 9);
                Program.DefaultColor();
                WriteLine("\nPRESS [ENTER] to FINISH");
                WriteLine("\n\n[HELP]");
                WriteLine("To Move: W/S | Up/Down Arrow Keys");
                WriteLine("To Invest: Points A/D | Left/Right Arrow Keys | Plus/Minus Keys\n");

                if (error == 1) Error.Display("#010201"); // SPEND POINTS FIRST
                // else if (error == 2) Error.Display("#010202"); // NO POINTS LEFT

                error = 0;
                ConsoleKey input = ReadKey().Key;
                switch (input)
                {
                    // Navigating
                    case ConsoleKey.UpArrow:
                        selected--; break;
                    case ConsoleKey.W:
                        selected--; break;
                    case ConsoleKey.DownArrow:
                        selected++; break;
                    case ConsoleKey.S:
                        selected++; break;
                    // Point Investing
                    case ConsoleKey.A:
                        if (availablePoints < 8 + level) InvestPoints(-1); break;
                    case ConsoleKey.LeftArrow:
                        if (availablePoints < 8 + level) InvestPoints(-1); break;
                    case ConsoleKey.OemMinus:
                        if (availablePoints < 8 + level) InvestPoints(-1); break;
                    case ConsoleKey.D:
                        if (availablePoints > 0) InvestPoints(1);
                        else error = 2; break;
                    case ConsoleKey.RightArrow:
                        if (availablePoints > 0) InvestPoints(1);
                        else error = 2; break;
                    case ConsoleKey.OemPlus:
                        if (availablePoints > 0) InvestPoints(1);
                        else error = 2; break;

                    case ConsoleKey.Enter:
                        if (availablePoints == 0 || healthPoints == 9 && meleeAttPoints == 9 &&
                            meleeDefPoints == 9 && magicAttPoints == 9 && magicDefPoints == 9)
                            _done = true;
                        else error = 1;
                        break;
                }
                selected = Math.Clamp(selected, 0, 4);

                Program.DefaultColor();

                void InvestPoints(int amount)
                {
                    switch (selected)
                    {
                        case 0:
                            if (amount < 0 && healthPoints > 0 || amount > 0 && healthPoints < 9)
                            {
                                healthPoints += amount;
                                availablePoints -= amount;
                            }
                            break;
                        case 1:
                            if (amount < 0 && meleeAttPoints > 0 || amount > 0 && meleeAttPoints < 9)
                            {
                                meleeAttPoints += amount;
                                availablePoints -= amount;
                            }
                            break;
                        case 2:
                            if (amount < 0 && meleeDefPoints > 0 || amount > 0 && meleeDefPoints < 9)
                            {
                                meleeDefPoints += amount;
                                availablePoints -= amount;
                            }
                            break;
                        case 3:
                            if (amount < 0 && magicAttPoints > 0 || amount > 0 && magicAttPoints < 9)
                            {
                                magicAttPoints += amount;
                                availablePoints -= amount;
                            }
                            break;
                        case 4:
                            if (amount < 0 && magicDefPoints > 0 || amount > 0 && magicDefPoints < 9)
                            {
                                magicDefPoints += amount;
                                availablePoints -= amount;
                            }
                            break;
                    }

                    if (availablePoints == 0) error = 2;
                    healthPoints = Math.Clamp(healthPoints, 0, 9);
                    meleeAttPoints = Math.Clamp(meleeAttPoints, 0, 9);
                    meleeDefPoints = Math.Clamp(meleeDefPoints, 0, 9);
                    magicAttPoints = Math.Clamp(magicAttPoints, 0, 9);
                    magicDefPoints = Math.Clamp(magicDefPoints, 0, 9);

                }
                void CheckColor(int value)
                {
                    if (selected == value) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                    else Program.DefaultColor();
                }
            }

            // APPLIENG VALUES
            Program.DefaultColor();
            SetValues(userName, 10 * (healthPoints + 1), 10 * (meleeAttPoints + 1), 10 * (meleeDefPoints + 1), 10 * (magicAttPoints + 1), 10 * (magicDefPoints + 1));
            Clear();
        }
    }
    internal class Enemy
    {
        public string name = "ENEMY";
        public float health = 10;
        public float damage = 2;

        public int level = 1;

        public void SetValues(string name, float health, float damage, int level)
        {
            this.name = name; this.health = health; this.damage = damage; this.level = level;
        }

        public void Attack(Player player)
        {
            player.health -= damage;
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
        }
    }
    internal class NPC
    {
        // SHOPKEEPER CLASS
        public class Shopkeeper
        {
            string name = "NAME";

            public void ShopMenu()
            {
                bool _shopping = true;
                int selected = 0;
                int menu = -1;
                int error = 0;

                do
                {
                    Program.DefaultColor();
                    Clear();

                    // PRINT SCREEN
                    // Title
                    Write($"\t    ");
                    Program.SetColor(ConsoleColor.Black, ConsoleColor.White);
                    if (name != string.Empty) WriteLine($" SHOPKEEPER [{name}]");
                    Program.DefaultColor();

                    // Menu
                    CheckColor(0, true);
                    Write("\n\n\n[BUY]\t");
                    CheckColor(1, true);
                    Write("[SELL]\t");
                    CheckColor(2, true);
                    Write("[CHAT]\t");
                    CheckColor(3, true);
                    Write("[INSPECT]\n");
                    Program.DefaultColor();

                    // Help
                    WriteLine($"\n\n\n[HELP]\nBACK: [TAB]");

                    // ERRORS
                    if (error == 1) Error.Display("NA"); // // // // // // // // // // // // //
                    error = 0;

                    // INPUTS
                    ConsoleKey input = ReadKey().Key;
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
                            _shopping = false;
                            break;
                    }

                } while (_shopping);

                void CheckColor(int value, bool _menu)
                {
                    if (_menu)
                    {
                        if (menu == -1)
                        {
                            if (selected == value) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                            else Program.DefaultColor();
                        }
                        else
                        {
                            if (menu == value) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                            else Program.DefaultColor();
                        }
                    }
                    else
                    {
                        if (selected == value) Program.SetColor(ConsoleColor.Green, ConsoleColor.Black);
                        else Program.DefaultColor();
                    }
                }
            }
        }

        public static class Conversation
        {

            public static void WakeUp()
            {
                Program.WriteLetters("Hey, are you awake?", false);
                Thread.Sleep(1000);
                Program.WriteLetters("\n\nUGGHHHHH\n", 100);
                string longYap = @"Oh my god
thank the lord that you are awake!
This is a miracle. How wonderful.

My name is ";
                Program.WriteWords(longYap, false, 100, 250);
                Program.WriteLetters("EMMA", false, 250);
                Program.WriteWords(", what is yours?", 150);
                Thread.Sleep(750);
            }
        }
        public class Templates
        {
            public static Enemy Gnome { get; } = new()
            {
                level = 1,
                health = 100,
                name = "GNOME"
            };
        }
    }
    internal class Scenes
    {
        public static class Island
        {
            public static void Explore(Vector2 startingPosition, Maps.MapData mapData)
            {
                if (mapData == null) return;

                bool _exploring = true;
                Vector2 playerPosition = startingPosition;

                do
                {
                    Program.DefaultColor();
                    Clear();

                    // Print map
                    Write(mapData.header);

                    Vector2 location = Vector2.Zero;
                    foreach (char c in mapData.mapVisual)
                    {
                        if (location != playerPosition) Write(c);
                        else Write('|'); // Player Character

                        if (c == '\n')
                        {
                            location.X = 0;
                            location.Y++;
                        }
                        else location.X++;
                    }

                    Write($"\n{mapData.footer}\n\n");

                    // Help
                    foreach (Vector2 blocked in mapData.blockedArea) Write(blocked.ToString() + ", ");
                    WriteLine($"\n\n[HELP] {playerPosition}\nMOVE: W/A/S/D | Arrow Keys\n\n");

                    // Inputs
                    ConsoleKey input = ReadKey().Key;
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
                    newPos.X = Math.Clamp(playerPosition.X, 0, location.X - 1);
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

                blockedArea = [new(16, 2), new(2, 3), new(1, 2)], // for $ (trees)
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
    internal class Data
    {
        // SAVE CLASS //

        /// <summary>
        /// Everything related to save.txt
        /// </summary>
        static public class SaveFile
        {
#pragma warning disable CS8602
            private static readonly string directory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Data");
#pragma warning restore CS8602

            static public void Load(int saveSlot)
            {
                string savePath = Path.Combine(directory, $"save{saveSlot}.json");
                try
                {
                    //LOAD SOMEHOW GOOD LUCK FUTURE ME!
                }
                catch (Exception ex)
                {
                    if (File.Exists(savePath)) Error.Display("#D10101");
                    else WriteLine(ex.Message);
                }
            }

            /// <summary>
            /// Deletes a save file in saveSlot
            /// </summary>
            /// <param name="saveSlot">number in file</param>
            static public void Delete(int saveSlot)
            {
                string savePath = Path.Combine(directory, $"save{saveSlot}.json");
                if (Exists(saveSlot)) WriteLine("FILE REMOVED");
                File.Delete(savePath);
            }

            /// <summary>
            /// Completely overwrite save.txt by setting it as a new sting array 
            /// Creates a new file if not found, so also a create method
            /// </summary>
            /// <param name="contents">String[] for every Line you want written</param>
            /// <param name="saveSlot">Set -1 for unused slot</param>
            static public void Set(string[] contents, int saveSlot)
            {
                // Look for new unused slot
                if (saveSlot == -1)
                {
                    saveSlot = 0;
                    while (Exists(saveSlot)) saveSlot++;
                }

                string savePath = Path.Combine(directory, $"save{saveSlot}.json");

                try
                {
                    // Ensure directory exists
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                    File.WriteAllLines(savePath, contents);
                }
                catch (Exception ex)
                {
                    Write(ex.ToString());
                    Error.Display("#D10102");
                }
                finally
                {
                    WriteLine("Executed 'Data.SaveFile.Set'");
                }
            }

            /// <summary>
            /// Get method for save.txt
            /// </summary>
            /// <returns>A String[] of very line in the save.txt file</returns>
            static public string[] Get(int saveSlot)
            {
                string savePath = Path.Combine(directory, $"save{saveSlot}.json");

                try
                {
                    return File.ReadAllLines(savePath);
                }
                catch
                {
                    string[] error = ["SAVE", "FILE ", "NOT", "FOUND"];
                    return error;
                }
            }

            /// <summary>
            /// Read out save.txt using Console.WriteLine()
            /// </summary>
            static public void Read(int saveSlot)
            {
                string savePath = Path.Combine(directory, $"save{saveSlot}.json");

                try
                {
                    string[] contents = File.ReadAllLines(savePath);
                    foreach (string line in contents) WriteLine(line);
                }
                catch
                {
                    Error.Display("#D10101");
                }
            }

            /// <summary>
            /// String returns a line of file player name and last time edited 
            /// </summary>
            /// <param name="slots"></param>
            static public string Display(int slots)
            {
                if (Exists(slots))
                    return $"{GetName(slots)} | {Time(slots)}";
                else Error.Display("#D10101"); return string.Empty;
            }

            /// <summary>
            /// Get last time save.txt was edited as a string
            /// </summary>
            /// <returns>.ToString of DataTime</returns>
            static public string Time(int saveSlot)
            {
                string savePath = Path.Combine(directory, $"save{saveSlot}.json");

                try
                {
                    return File.GetLastWriteTime(savePath).ToString(CultureInfo.InvariantCulture);
                }
                catch
                {
                    return "SAVE FILE TIME NOT FOUND";
                }
            }

            /// <summary>
            /// int[] length is amount of slots and each value is
            /// the saveSlot, usable for other methods
            /// </summary>
            /// <returns>number behind save*.json for every file</returns>
            static public int[] GetAllSlots()
            {
                List<int> results = [];

                foreach (string file in Directory.GetFiles(directory, "*.json"))
                {
                    // Get slot number from the filename
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (fileName.StartsWith("save") && int.TryParse(fileName.AsSpan(4), out int slot))
                    {
                        results.Add(slot);
                    }
                }
                results.Sort();

                return [.. results];
            }

            /// <summary>
            /// Returns the name of the Player Character of the save file
            /// </summary>
            /// <param name="saveSlot">save file in question</param>
            /// <returns></returns>
            static public string GetName(int saveSlot)
            {
                return Get(saveSlot)[0];
            }

            /// <summary>
            /// Returns level (int) of Player Character from a save file
            /// </summary>
            /// <param name="saveSlot">save file in question</param>
            /// <returns></returns>
            static public string GetLevel(int saveSlot)
            {
                return Get(saveSlot)[1];
            }

            /// <summary>
            /// Returns a bool depending on the save slots existence.
            /// </summary>
            /// <param name="saveSlot"></param>
            /// <returns></returns>
            static public bool Exists(int saveSlot)
            {
                return File.Exists(Path.Combine(directory, $"save{saveSlot}.json"));
            }
        }



        // CUSTOM FILE CLASS//

        /// <summary>
        /// Set, Get, Read Any file in /Data
        /// </summary>
        /// <param name="fileName">File name with suffix</param>
        public class CustomFile(string fileName)
        {
            private static readonly string directory = Directory.GetCurrentDirectory();
            public readonly string filePath = Path.Combine(directory, fileName);

            /// <summary>
            /// Completely Overwrite this file by setting it as a new sting array
            /// </summary>
            /// <param name="contents">String[] for every Line you want written</param>
            public void Set(string[] contents)
            {
                try
                {
                    File.WriteAllLines(filePath, contents);
                }
                catch
                {
                    Error.Display("#D10201");
                }
                finally
                {
                    WriteLine($"File Set at {filePath}");
                }
            }

            /// <summary>
            /// Get Method for this file
            /// </summary>
            /// <returns>A String[] of very line in the this file file</returns>
            public string[] Get()
            {
                try
                {
                    return File.ReadAllLines(filePath);
                }
                catch
                {
                    string[] error = ["FILE ", "NOT", "FOUND"];
                    return error;
                }
            }

            /// <summary>
            /// Read out this file using Console.WriteLine()
            /// </summary>
            public void Read()
            {
                try
                {
                    string[] contents = File.ReadAllLines(filePath);
                    foreach (string line in contents) WriteLine(line);
                }
                catch
                {
                    Error.Display("#D10101");
                }
            }

            /// <summary>
            /// Get last time this file was edited
            /// </summary>
            /// <returns>.ToString of DataTime</returns>
            public string Time()
            {
                try
                {
                    return File.GetLastWriteTime(filePath).ToString(CultureInfo.InvariantCulture);
                }
                catch
                {
                    return "FILE TIME NOT FOUND";
                }
            }
        }
    }
    internal class Error
    {
        static public void Display(string CODE)
        {
            // PLACEMENT IN CODE: At the bottom of your loop.
            // Best you save what error in an int and then use a
            // switch to set string code in the end.
            // BUT BEFORE any key checking. Else it is instantly removed
            // 
            // NAMING: #Class Number, Method Number, Number (just to sort you choose)
            // Example: 18th Class, 3rd Method, 1st Number
            // would look like: #180301
            // Use: "NA" as a placeholder
            // 
            // No need to Organize in the switch unless very bored

            Program.SetColor(ConsoleColor.White, ConsoleColor.Red);
            Write("\n ");
            switch (CODE)
            {
                case "NA":
                    Write("NO ERROR MESSAGE AVAILABLE, PLEACE CONTACT DEV!");
                    break;

                // Program
                case "#000101":
                    Write("PrintSquares() int filled > int size, PLEACE CONTACT DEV!");
                    break;
                case "#010201":
                    Write("YOU NEED TO SPEND ALL YOUR POINTS FIRST!");
                    break;
                case "#010202":
                    Write("YOU DO NOT HAVE ANY POINTS LEFT!");
                    break;

                case "#000302":
                    Write("ALREADY USING THIS SAVE SLOT");
                    break;

                //  Data
                case "#D10101":
                    Write("SAVE FILE NOT FOUND");
                    break;

                case "#D10102":
                    Write("COULDN'T WRITE TO SAVE \n ACTION MIGHT BE BLOCKED BY ANITVIRUS");
                    break;

                case "#D10201":
                    Write("FILE NOT FOUND");
                    break;
            }
            Write(" \n\n");
            Program.DefaultColor();
        }
    }
}