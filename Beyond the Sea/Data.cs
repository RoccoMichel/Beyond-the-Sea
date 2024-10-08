﻿using System.Globalization;

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
                    else Console.WriteLine(ex.Message);
                }
            }

            /// <summary>
            /// Deletes a save file in saveSlot
            /// </summary>
            /// <param name="saveSlot">number in file</param>
            static public void Delete(int saveSlot)
            {
                string savePath = Path.Combine(directory, $"save{saveSlot}.json");
                if (Exists(saveSlot)) Console.WriteLine("FILE REMOVED");
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
                    Console.Write(ex.ToString());
                    Error.Display("#D10102");
                }
                finally
                {
                    Console.WriteLine("Executed 'Data.SaveFile.Set'");
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
                    foreach (string line in contents) Console.WriteLine(line);
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
}
