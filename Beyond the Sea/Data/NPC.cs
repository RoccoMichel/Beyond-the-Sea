using static Beyond_the_Sea.Program;

namespace Beyond_the_Sea
{
    internal class NPC
    {
        // SHOPKEEPER CLASS
        public class Shopkeeper
        {
            string name = "NAME";
            int level = 1;

            public void ShopMenu()
            {
                bool _shopping = true;
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

        public static class Conversation
        {
            
            public static void WakeUp()
            {
                WriteLetters("Hey, are you awake?", false);
                Thread.Sleep(1000);
                WriteLetters("\n\nUGGHHHHH\n", 100);
                string longYap = @"Oh my god
thank the lord that you are awake!
This is a miracle. How wonderful.

Ehmmmm...

My name is ";
                WriteWords(longYap, false, 100, 250);
                WriteLetters("EMMA", false, 250);
                WriteWords(", what is yours?", 150);
                Thread.Sleep(750);
            }
        }
        public class Templates
        {
            public static Enemy Gnome = new()
            {
                level = 1,
                health = 100,
                name = "GNOME"
            };
        }
    }

}
