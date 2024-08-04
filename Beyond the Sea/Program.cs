namespace Beyond_the_Sea // by ROCCO MICHEL | 2024
{
    internal class Program
    {
        static void Main()
        {
            string[] saveStyle = ["[player level]", "[player xp]", "[stats (health, att, mag, attDef, magDef)]", "[player location]", "[inventory]"];
            Data.SaveFile.Set(saveStyle);
            Data.SaveFile.Read();
            Console.Write(Data.SaveFile.Time());

            Thread.Sleep(99999999);

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

        /*MAIN END*/

        /*CLASSES START*/

        // ITEM CLASS
        class Item
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
            public enum ItemTypes { FOOD, ARMOUR, MELEEWEAPON, MAGICWEAPON, VALUEABLE}

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

        // SHOPKEEPER CLASS
        class Shopkeeper
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
                    if (error == 1) DisplayError("NA"); // // // // // // // // // // // // //
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

        // ENEMY CLASS
        class Enemy
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

        // PLAYER CLASS
        class Player
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

            public List<Item> inventory = [];

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
                int succsessEXP = 0;
                foreach (Enemy enemy in enemies) succsessEXP += random.Next(5, 10) * enemy.level;


                do
                {
                    DefaultColor();
                    Console.Clear();

                    // PRINT SCREEN
                    // [ITEM]
                    if (menu == 2) { Inventory(); menu = -1; }


                    // Title
                    Console.Write($"\t    ");
                    SetColor("black", "white");
                    Console.WriteLine(" BATTLE ");
                    DefaultColor();

                    // Enemies
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (menu != -1) CheckColor(i, false);
                        if (menu != -1 && i == selected) Console.Write($">{enemies[i].name}< | HP: {enemies[i].health}");
                        else Console.Write($" {enemies[i].name}  | HP: {enemies[i].health}");
                        if (menu == 3 && i == selected) Console.Write($"\t[Lv.{enemies[i].level}]");
                        Console.Write('\n');
                    }

                    // Menu
                    CheckColor(0, true);
                    Console.Write("\n\n\n[MELEE]\t");
                    CheckColor(1, true);
                    Console.Write("[MAGIC]\t");
                    CheckColor(2, true);
                    Console.Write("[ITEM]\t");
                    CheckColor(3, true);
                    Console.Write("[INSPECT]\n");

                    // health
                    SetColor("green", "black");
                    Console.Write("HP: ");
                    PrintSquares(10, 10);
                    DefaultColor();

                    // Help
                    Console.WriteLine($"\n\n\n[HELP]");
                    if (menu == -1) Console.WriteLine("MOVE: A/D | Left/Right Arrow Keys\nSELECT: [SPACE]");
                    if (menu == 0 || menu == 1 || menu == 3) Console.WriteLine("MOVE: W/S | Up/Down Arrow Keys\nSELECT: [SPACE]\nBACK: [TAB]");

                    // ERRORS
                    if (error == 1) DisplayError("NA"); // // // // // // // // // // // // //
                    error = 0;

                    // INPUTS
                    ConsoleKey input = Console.ReadKey().Key;
                    switch (input)
                    {
                        // Navigation
                        // buttom menu
                        case ConsoleKey.LeftArrow: if (menu == -1)selected--; break;
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
                    else selected = Math.Clamp(selected, 0, enemies.Length-1);
                } while (_fighting);

                // VICTORY
                Console.Clear();
                Console.Write("YOU WON!\nYou got: [");
                SetColor("green", "black");
                Console.Write($"+{succsessEXP} EXP");
                exp += succsessEXP;
                DefaultColor();
                Console.WriteLine($"]\n{exp} / {(int)100 * (level * 1.45)} EXP\n\nPress [ANY KEY] to continue.");
                Console.ReadKey();

                DefaultColor();
                Console.Clear();

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

            public void Inventory()
            {
                string[] itemDisciption = ["- RARITY | NAME | Lv -", "- DISCRIPTION -", "- Bonuses? -\n\n\n"];
                int page = 1;
                int error = 0;
                int selected = 0;
                bool _browsing = true;

                do
                {
                    DefaultColor();
                    Console.Clear();

                    // PAGE PRINT
                    Console.Write($"[{page}/{((int)inventory.Count / 15)+1}]\t      ");
                    SetColor("black", "white");
                    Console.WriteLine(" INVETORY ");
                    DefaultColor();
                    for (int i = 0; i < 15; i++)
                    {
                        if (i + (page - 1) * 15 == selected) SetColor("green", "black");

                        if (inventory.Count - 1 >= i + (page - 1) * 15 && inventory[0] != null) Console.Write($"[{inventory[i + (page - 1) * 15].icon}]\t");
                        else Console.Write("[...]\t");

                        if ((i + 1) % 5 == 0) Console.Write("\n\n\n");
                        DefaultColor();
                    }

                    if (inventory.Count - 1 >= selected)
                    {
                        itemDisciption[0] = $"[{inventory[selected].rarity}] {inventory[selected].name} | {inventory[selected].level}";
                        itemDisciption[1] = inventory[selected].description;
                        itemDisciption[2] = $"{inventory[selected].bonuses[0]}\n{inventory[selected].bonuses[1]}\n{inventory[selected].bonuses[2]}\n";
                    }
                    else
                    {
                        itemDisciption[0] = "[EMPTY]";
                        itemDisciption[1] = string.Empty;
                        itemDisciption[2] = "\n\n\n";
                    }

                    Console.WriteLine(itemDisciption[0]);
                    Console.WriteLine(itemDisciption[1]);
                    Console.WriteLine(itemDisciption[2]);
                    Console.WriteLine("\n\n[HELP]\nMOVE: W/A/S/D | Arrow Keys\nFLIP PAGE: E/Q\nSELECT: [SPACE]\nBACK: [TAB]");

                    // ERRORS
                    if (error == 1) DisplayError("NA"); // // // // // // // // // // // //
                    error = 0;

                    // INPUTS
                    ConsoleKey input = Console.ReadKey().Key;
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
                            if(page != 1) selected -= 15;
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

                DefaultColor();
                Console.Clear();
            }
            
            public void Statistics()
            {
                Console.Clear();
                Console.Write($"[{name}] | Lv: [{level}]\n");
                Console.Write($"{exp} of {(int)100 * (level * 1.45f)} EXP\t(until next level)\n");

                Console.Write("\nHealth:\t\t");
                PrintSquares((int)(health - 10) / 10, 9);

                Console.Write("\nMelee Attack:\t");
                PrintSquares((int)(meleeDamage - 10) / 10, 9);

                Console.Write("\nMelee Defense:\t");
                PrintSquares((int)(meleeDefense - 10) / 10, 9);

                Console.Write("\nMagic Attack:\t");
                PrintSquares((int)(magicDamage - 10) / 10, 9);

                Console.Write("\nMagic Defense:\t");
                PrintSquares((int)(magicDefense - 10) / 10, 9);

                Console.Write("\nPress [ANY KEY] to return");
                Console.ReadKey();
                Console.Clear();
                DefaultColor();
            }
            
            public void PlayerCreator()
            {
                bool _done = false;
                int avaiablePoints = 8 + level;
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
                    Console.Clear();
                    DefaultColor();
                    Console.Write("Enter your Character's Name: ");
                    SetColor("darkblue", "cyan");
#pragma warning disable CS8600 // while statment checks if it is null
                    userName = Console.ReadLine();
#pragma warning restore CS8600 // so no warning needed
                    DefaultColor();
                } while (string.IsNullOrWhiteSpace(userName));

                // PLAYER STATS
                selected = 0;
                while (!_done)
                {
                    Console.Clear();
                    Console.Write($"[{userName}] | POINTS LEFT: [{avaiablePoints}]\n");

                    CheckColor(0);
                    Console.Write("\nHealth:\t\t");
                    PrintSquares(healthPoints, 9);
                    CheckColor(1);
                    Console.Write("\nMelee Attack:\t");
                    PrintSquares(meleeAttPoints, 9);
                    CheckColor(2);
                    Console.Write("\nMelee Defense:\t");
                    PrintSquares(meleeDefPoints, 9);
                    CheckColor(3);
                    Console.Write("\nMagic Attack:\t");
                    PrintSquares(magicAttPoints, 9);
                    CheckColor(4);
                    Console.Write("\nMagic Defense:\t");
                    PrintSquares(magicDefPoints, 9);
                    DefaultColor();
                    Console.WriteLine("\nPRESS [ENTER] to FINISH");
                    Console.WriteLine("\n\n[HELP]");
                    Console.WriteLine("To Move: W/S | Up/Down Arrow Keys");
                    Console.WriteLine("To Invest: Points A/D | Left/Right Arrow Keys | Plus/Minus Keys\n");

                    if (error == 1) DisplayError("#010201"); // SPEND POINTS FIRST
                    else if (error == 2) DisplayError("##010201"); // NO POINTS LEFT

                    error = 0;
                    ConsoleKey input = Console.ReadKey().Key;
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
                            if (avaiablePoints < 8 + level) investPoints(-1); break;
                        case ConsoleKey.LeftArrow:
                            if (avaiablePoints < 8 + level) investPoints(-1); break;
                        case ConsoleKey.OemMinus:
                            if (avaiablePoints < 8 + level) investPoints(-1); break;
                        case ConsoleKey.D:
                            if (avaiablePoints > 0) investPoints(1);
                            else error = 2; break;
                        case ConsoleKey.RightArrow:
                            if (avaiablePoints > 0) investPoints(1);
                            else error = 2; break;
                        case ConsoleKey.OemPlus:
                            if (avaiablePoints > 0) investPoints(1);
                            else error = 2; break;

                        case ConsoleKey.Enter:
                            if (avaiablePoints == 0 || healthPoints == 9 && meleeAttPoints == 9 &&
                                meleeDefPoints == 9 && magicAttPoints == 9 && magicDefPoints == 9)
                                _done = true;
                            else error = 1;
                            break;
                    }
                    selected = Math.Clamp(selected, 0, 4);
                    
                    DefaultColor();

                    void investPoints(int amount)
                    {
                        if (avaiablePoints == 0) error = 2;
                        switch (selected)
                        {
                            case 0:
                                if (amount < 0 && healthPoints > 0 || amount > 0 && healthPoints < 9)
                                {
                                    healthPoints += amount;
                                    avaiablePoints -= amount;
                                }
                                break;
                            case 1:
                                if (amount < 0 && meleeAttPoints > 0 || amount > 0 && meleeAttPoints < 9)
                                {
                                    meleeAttPoints += amount;
                                    avaiablePoints -= amount;
                                }
                                break;
                            case 2:
                                if (amount < 0 && meleeDefPoints > 0 || amount > 0 && meleeDefPoints < 9)
                                {
                                    meleeDefPoints += amount;
                                    avaiablePoints -= amount;
                                }
                                break;
                            case 3:
                                if (amount < 0 && magicAttPoints > 0 || amount > 0 && magicAttPoints < 9)
                                {
                                    magicAttPoints += amount;
                                    avaiablePoints -= amount;
                                }
                                break;
                            case 4:
                                if (amount < 0 && magicDefPoints > 0 || amount > 0 && magicDefPoints < 9)
                                {
                                    magicDefPoints += amount;
                                    avaiablePoints -= amount;
                                }
                                break;
                        }

                        healthPoints = Math.Clamp(healthPoints, 0, 9);
                        meleeAttPoints = Math.Clamp(meleeAttPoints, 0, 9);
                        meleeDefPoints = Math.Clamp(meleeDefPoints, 0, 9);
                        magicAttPoints = Math.Clamp(magicAttPoints, 0, 9);
                        magicDefPoints = Math.Clamp(magicDefPoints, 0, 9);

                    }
                    void CheckColor(int value)
                    {
                        if (selected == value) SetColor("green", "black");
                        else DefaultColor();
                    }
                }

                // APPLIENG VALUES
                DefaultColor();
                SetValues(userName, 10*(healthPoints+1), 10*(meleeAttPoints+1),10*(meleeDefPoints+1),10*(magicAttPoints+1),10*(magicDefPoints+1));
                Console.Clear();
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
                DisplayError("#000101");
                return;
            } 
            for (int i = 0; i < size + 1; i++)
            {
                if (i < filled) Console.Write("|");
                else if (i < size) Console.Write("-");
            }
            Console.Write($"[{filled}]");
        }

        static public void DisplayError(string CODE)
        {
            //
            // PLACEMENT IN CODE: At the buttom of your loop.
            // Best you save what error in an int and then use a
            // switch to set string code in the end.
            // BUT BEFORE any key checking. Else it is instatly removed
            // 
            // NAMING: #Class Number, Method Number, Number (just to sort you choose)
            // Exmaple: 18th Class, 3rd Method, 1st Number
            // would look like: #180301
            // Use: "NA" as a placeholder
            // 
            // No need to Organize in the switch unless very bored

            SetColor("white", "red");
            switch (CODE)
            {
                case "NA":
                    Console.Write("\n NO ERROR MESSAGE AVAILABLE, PLEACE CONTACT DEV! ");
                    break;

                // Program.cs
                case "#000101":
                    Console.Write("\n PrintSquares() int filled > int size, PLEACE CONTACT DEV! ");
                    break;
                case "#010201":
                    Console.Write("\n YOU NEED TO SPEND ALL YOUR POINTS FIRST! ");
                    break;
                case "#010202":
                    Console.Write("\n YOU DO NOT HAVE ANY POINTS LEFT! ");
                    break;

                //  Data.cs
                case "#D10101":
                    Console.Write("\n SAVE FILE NOT FOUND ");
                    break;

                case "#D10201":
                    Console.Write("\n FILE NOT FOUND ");
                    break;
            }
            Console.Write("\n\n");
            DefaultColor();
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