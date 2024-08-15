using static Beyond_the_Sea.Program;

namespace Beyond_the_Sea
{
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
                if (error == 1) Error.Display("NA"); // // // // // // // // // // // // //
                error = 0;

                // INPUTS
                ConsoleKey input = Console.ReadKey().Key;
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
            Console.Clear();
            Console.Write("YOU WON!\nYou got: [");
            SetColor("green", "black");
            Console.Write($"+{successEXP} EXP");
            exp += successEXP;
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
            string[] itemDescription = ["- RARITY | NAME | Lv -", "- DISCRIPTION -", "- Bonuses? -\n\n\n"];
            int page = 1;
            int error = 0;
            int selected = 0;
            bool _browsing = true;

            do
            {
                DefaultColor();
                Console.Clear();

                // PAGE PRINT
                Console.Write($"[{page}/{((int)inventory.Count / 15) + 1}]\t      ");
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

                Console.WriteLine(itemDescription[0]);
                Console.WriteLine(itemDescription[1]);
                Console.WriteLine(itemDescription[2]);
                Console.WriteLine("\n\n[HELP]\nMOVE: W/A/S/D | Arrow Keys\nFLIP PAGE: E/Q\nSELECT: [SPACE]\nBACK: [TAB]");

                // ERRORS
                if (error == 1) Error.Display("NA"); // // // // // // // // // // // //
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
                Console.Write($"[{userName}] | POINTS LEFT: [{availablePoints}]\n");

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

                if (error == 1) Error.Display("#010201"); // SPEND POINTS FIRST
                else if (error == 2) Error.Display("##010201"); // NO POINTS LEFT

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
                        if (availablePoints < 8 + level) investPoints(-1); break;
                    case ConsoleKey.LeftArrow:
                        if (availablePoints < 8 + level) investPoints(-1); break;
                    case ConsoleKey.OemMinus:
                        if (availablePoints < 8 + level) investPoints(-1); break;
                    case ConsoleKey.D:
                        if (availablePoints > 0) investPoints(1);
                        else error = 2; break;
                    case ConsoleKey.RightArrow:
                        if (availablePoints > 0) investPoints(1);
                        else error = 2; break;
                    case ConsoleKey.OemPlus:
                        if (availablePoints > 0) investPoints(1);
                        else error = 2; break;

                    case ConsoleKey.Enter:
                        if (availablePoints == 0 || healthPoints == 9 && meleeAttPoints == 9 &&
                            meleeDefPoints == 9 && magicAttPoints == 9 && magicDefPoints == 9)
                            _done = true;
                        else error = 1;
                        break;
                }
                selected = Math.Clamp(selected, 0, 4);

                DefaultColor();

                void investPoints(int amount)
                {
                    if (availablePoints == 0) error = 2;
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
            SetValues(userName, 10 * (healthPoints + 1), 10 * (meleeAttPoints + 1), 10 * (meleeDefPoints + 1), 10 * (magicAttPoints + 1), 10 * (magicDefPoints + 1));
            Console.Clear();
        }
    }
}
