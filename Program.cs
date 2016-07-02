/*This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.IO;
using System.Linq;


namespace FolderSync
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\n PenSync is a simple Sync Program to copy files to your External Pen-Drive \n Copyright (C) 2016  Aakash Patel \n This program is free software: you can redistribute it and / or \n it under the terms of the GNU General Public License as published \n the Free Software Foundation, either version 3 of the License, \n (at your option) any later version\n This program is distributed in the hope that it will be useful\n but WITHOUT ANY WARRANTY; without even the implied warranty \n MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See \n GNU General Public License for more details\n You should have received a copy of the GNU General Public \n along with this program.If not, see < http://www.gnu.org/licenses/>. \n\n\n");

            Program o = new Program();        

            Console.WriteLine("Enter the Source Location : For Example : C:\\FolderName");
            string sourceLocation = Console.ReadLine();
            string targetLocation = o.getDrive();                
                     
            string[] files = Directory.EnumerateFiles(sourceLocation, "*.*", SearchOption.AllDirectories).ToArray();
            string destination;
            Boolean check = false;

            foreach (var filepath in files)
            {
                string filename = Path.GetFileName(filepath);
                destination = Path.Combine(targetLocation, filename);

                if (!File.Exists(destination))
                {
                    try
                    {                        
                        File.Copy(filepath, destination);
                        Console.WriteLine("Copying " + filename + " to " + destination);
                        check = true;                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR! " + ex);
                    }

                }
                else if (File.Exists(destination))
                {
                    try
                    {                        
                        DateTime aModified = System.IO.File.GetLastWriteTime(sourceLocation + filename);
                        DateTime bModified = System.IO.File.GetLastWriteTime(targetLocation + filename);
                        if (DateTime.Compare(aModified, bModified) != 0)
                        {                           
                            File.Copy(filepath, destination, true);
                            Console.WriteLine("Copying " + filename + " to " + destination);
                            check = true;
                        }                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR!");                        
                    }
                }
            }

            if(check)
                Console.WriteLine("Sync Successful!");
            else
                Console.WriteLine("Sync Failed!");            
            
            Console.ReadLine();
        }

        public string getDrive()
        {
            DriveInfo[] mydrives = DriveInfo.GetDrives();

            string name = "";

            foreach (DriveInfo mydrive in mydrives)
            {
                if (mydrive.DriveType == DriveType.Removable)
                {
                    Console.WriteLine("Target Location is : " + mydrive.Name);
                    name = mydrive.Name;
                    break;
                }
            }

            return name;
        }
    }
}
