namespace Beyond_the_Sea
{
    /// <summary>
    /// Data class handles File necessary File
    /// creation/deletion/writing/reading/ect...
    /// Includes 'Save' Class
    /// </summary>
    internal class Data
    {
        static public class Save
        {
            private static readonly string directory = Directory.GetCurrentDirectory();
            private static readonly string fileName = "save.txt";

            public static readonly string filePath = Path.Combine(directory, fileName);

            /// <summary>
            /// Completely Overwrite save.txt by setting it as a new sting array
            /// </summary>
            /// <param name="contents">String[] for every Line you want written</param>
            static public void Set(string[] contents)
            {
                try
                {
                    File.WriteAllLines(filePath, contents);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    Console.WriteLine("Executed 'SaveSet'");
                }
            }

            /// <summary>
            /// Get Method for save.txt
            /// </summary>
            /// <returns>A String[] of very line in the save.txt file</returns>
            static public string[] Get()
            {
                return File.ReadAllLines(filePath);
            }

            /// <summary>
            /// Read out save.txt using Console.WriteLine()
            /// </summary>
            static public void Read()
            {
                string[] contents = File.ReadAllLines(filePath);

                foreach (string line in contents) Console.WriteLine(line);
            }
        }
    }
}
