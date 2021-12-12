using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AdventOfCode_2021_Day12
{
    public static class Program
    {
        public static void Main()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(currentDirectory, @"..\..\..\test.txt");
            string path = Path.GetFullPath(file);
            string[] text = File.ReadAllText(path).Replace("\r", "").Split("\n");

            // Find all possible connections start to end.
            // small caves (small letters) may only be
            // visisted once. Big ones (capital letters)
            // may be visited any number of times.
            // Dash '-' means two caves are connected.

            Dictionary<string,List<string>> connections = new();
            for(int i = 0; i < text.Length; i++)
            {
                string[] temp = text[i].Split('-');
                if(connections.ContainsKey(temp[0]))
                {
                    connections[temp[0]].Add(temp[1]);
                }
                else
                {
                    connections[temp[0]] = new List<string> {temp[1]};
                }
                if(connections.ContainsKey(temp[1]))
                {
                    connections[temp[1]].Add(temp[0]);
                }
                else
                {
                    connections[temp[1]] = new List<string> {temp[0]};
                }
            }
            foreach(var item in connections)
            {
                Console.Write($"Key: {item.Key} Connects to: ");
                foreach(var str in item.Value)
                {
                    Console.Write($"{str} ");
                }
                Console.WriteLine();
            }


        }
    }
}
