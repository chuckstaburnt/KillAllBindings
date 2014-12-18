using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllBindings
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath;
            string[] allFilesArray;
            string[] vsssccFilesArray;
            string[] slnFilesArray;

            System.Console.WriteLine("====================================================");
            System.Console.WriteLine("===================KillAllBindings==================\n");
            System.Console.WriteLine("Removes read only flags, deletes .vssscc files,");
            System.Console.WriteLine("and removes source control bindings from sln files.");
            System.Console.WriteLine("====================================================\n");
            
            // Get path from user, build array of sub-directories.
            do
            {
                System.Console.Write("Source code path: ");
                rootPath = Console.ReadLine();
            } while (Directory.Exists(rootPath) == false);
            System.Console.WriteLine("Path exists.\n");

            // Check to ensure the user understands what this does before continuing. Exits immediately if don't enter y or Y.
            System.Console.Write("This program modifies source files! Are you sure you wish to continue? (y/n): ");
            string verify = Console.ReadLine();
            if (verify == "y" || verify == "Y")
            {
                System.Console.WriteLine("Continuing.\n");
            }
            else
            {
                System.Environment.Exit(1);
            }

            // Remove read only flags from ALL files.
            System.Console.WriteLine("Removing read only flags from all files in {0}.\n", rootPath);
            allFilesArray = System.IO.Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);
            int FlagCount = 0;
            foreach (string file in allFilesArray)
            {
                FileAttributes attributes = File.GetAttributes(file);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes = attributes & ~FileAttributes.ReadOnly;
                    File.SetAttributes(file, attributes);
                    System.Console.WriteLine("Removed read only flag for {0}.", file);
                    FlagCount++;
                }
                else
                {
                    System.Console.WriteLine("Skipping {0}. Not read only.", file);
                }
            }
            System.Console.WriteLine("All read only flags removed.");

            // Delete all .vssscc files.
            vsssccFilesArray = System.IO.Directory.GetFiles(rootPath, "*.vssscc", SearchOption.AllDirectories);
            int vcount = vsssccFilesArray.Count();
            foreach (string file in vsssccFilesArray)
            {
                System.Console.WriteLine("Deleting {0}.", file);
                File.Delete(file);
            }

            // Build a list of solution files.
            System.Console.WriteLine("Looking for .sln files.\n");
            slnFilesArray = System.IO.Directory.GetFiles(rootPath, "*.sln", SearchOption.AllDirectories);
            int scount = slnFilesArray.Count();
            System.Console.WriteLine("Found {0} solution files.\n", scount);

            // Remove source control binding section.
            int editedSlnFileCount = 0;
            foreach (string file in slnFilesArray)
            {
                System.Console.WriteLine("File: {0}", file);
                int start = ReturnLineNumber("TeamFoundationVersionControl", file);
                int end = ReturnLineNumber("EndGlobalSection", file);
                if (start == -1 || start == 0 || end == -1 || start > end)
                {
                    System.Console.WriteLine("Skipping. No bindings found.\n");
                }
                else
                {
                    string directory = Path.GetDirectoryName(file);
                    string fileName = Path.GetFileName(file);
                    string tempFileName = "temp.sln";
                    string tempFilePath = Path.Combine(directory, tempFileName);
                    int count = 0;
                    editedSlnFileCount++;
                    System.Console.WriteLine("Binding exists from lines {0} to {1}.", start, end);
                    System.Console.WriteLine("Creating {0}.", tempFileName);
                    foreach (string line in File.ReadAllLines(file))
                    {
                        count++;
                        if (count < start || count > end)
                        {
                            using (System.IO.StreamWriter tempFile = new System.IO.StreamWriter(tempFilePath, true))
                            {
                                tempFile.WriteLine(line);
                            }
                        }
                    }
                    System.Console.WriteLine("Temp file created.");
                    System.Console.WriteLine("Deleting {0}.", file);
                    File.Delete(file);
                    System.Console.WriteLine("Renaming {0} to {1}.\n", tempFileName, fileName);
                    File.Move(tempFilePath, file);
                }
            }

            System.Console.WriteLine("=========================================");
            System.Console.WriteLine("Results:");
            System.Console.WriteLine("Removed read only flag from {0} files.", FlagCount);
            System.Console.WriteLine("Deleted {0} vssscc files.", vcount);
            System.Console.WriteLine("Re-wrote {0} out of {1} solution files.", editedSlnFileCount, scount);
            System.Console.Write("Done. Press enter to close.\n");
            System.Console.WriteLine("=========================================");
            System.Console.ReadLine(); // Using this as a hold open.
        }

        // Returns linenumber of the first instance of a keyword in a text file.
        static int ReturnLineNumber(string keyWord, string file)
        {
            int lineNumber = 0;
            foreach (string line in File.ReadAllLines(file))
            {
                lineNumber++;
                if (line.Contains(keyWord))
                {
                    return lineNumber;
                }
            }
            return -1;
        }
    }
}