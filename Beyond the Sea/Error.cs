namespace Beyond_the_Sea
{
    internal class Error
    {
        static public void Display(string CODE)
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

            Program.SetColor("white", "red");
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

                case "#D10102":
                    Console.Write("\n COULDN'T WRITE TO SAVE\nACTION MIGHT BE BLOCKED BY ANITVIRUS ");
                    break;

                case "#D10201":
                    Console.Write("\n FILE NOT FOUND ");
                    break;
            }
            Console.Write("\n\n");
            Program.DefaultColor();
        }
    }
}