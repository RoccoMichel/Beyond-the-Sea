using System.Globalization;

namespace Beyond_the_Sea
{
    /// <summary>
    /// Data class handles File necessary File
    /// creation/deletion/writing/reading/ect...
    /// Includes 'Save' Class
    /// </summary>
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
            private static readonly string fileName = "save.txt";

            public static readonly string savePath = Path.Combine(directory, fileName);

            /// <summary>
            /// Completely overwrite save.txt by setting it as a new sting array
            /// </summary>
            /// <param name="contents">String[] for every Line you want written</param>
            static public void Set(string[] contents)
            {
                try
                {
                    // Ensure direcotry exists
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                    File.WriteAllLines(savePath, contents);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Program.DisplayError("#D10101");
                }
                finally
                {
                    Console.WriteLine("Executed 'SaveSet'");
                }
            }

            /// <summary>
            /// Get method for save.txt
            /// </summary>
            /// <returns>A String[] of very line in the save.txt file</returns>
            static public string[] Get()
            {
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
            static public void Read()
            {
                try
                {
                    string[] contents = File.ReadAllLines(savePath);
                    foreach (string line in contents) Console.WriteLine(line);
                }
                catch
                {
                    Program.DisplayError("#D10101");
                }                
            }

            /// <summary>
            /// Get last time save.txt was edited
            /// </summary>
            /// <returns>.ToString of DataTime</returns>
            static public string Time()
            {
                try
                {
                    return File.GetLastWriteTime(savePath).ToString(CultureInfo.InvariantCulture);
                }
                catch
                {
                    return "SAVE FILE TIME NOT FOUND";
                }
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
                    Program.DisplayError("#D10201");
                }
                finally
                {
                    Console.WriteLine($"File Set at {filePath}");
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
                    foreach (string line in contents) Console.WriteLine(line);
                }
                catch
                {
                    Program.DisplayError("#D10101");
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
}
