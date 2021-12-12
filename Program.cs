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
            string file = Path.Combine(currentDirectory, @"..\..\..\input.txt");
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
            foreach(List<string> item in connections.Values)
            {
                item.Remove("start");
            }
            connections.Remove("end");
            foreach(var item in connections)
            {
                Console.Write($"Key: {item.Key} Connects to: ");
                foreach(var str in item.Value)
                {
                    Console.Write($"{str} ");
                }
                Console.WriteLine();
            }

            // This is the way
            bool exit;
            HashSet<string> traveledPaths = new();
            do
            {
                exit = true;
                // start at start
                List<List<string>> currentPaths = new();
                foreach(var item in connections)
                {
                    if(item.Key == "start")
                    {
                        foreach(string val in item.Value)
                        currentPaths.Add(new List<string>{"start",val});
                    }
                }

                // foreach(List<string> Path in currentPaths)
                for(int i = 0; i < currentPaths.Count; i++)
                {
                    List<string> Path = currentPaths[i];
                    if(Path[^1].Equals("end"))
                    {
                        continue;
                    }
                    foreach(var item in connections)
                    {
                        if(Path[^1].Equals(item.Key))
                        {
                            int ctr = item.Value.Count;
                            for(int j = 0; j < ctr; j++)
                            {
                                List<string> temp = new(Path);
                                if(item.Value[j].Any(char.IsUpper) || !temp.Contains(item.Value[j]))
                                {
                                    temp.Add(item.Value[j]);
                                    currentPaths.Add(temp);
                                }
                            }
                            currentPaths.Remove(Path);
                            i=-1;
                        }
                    }
                }

                foreach(List<string> item in currentPaths)
                {
                    if(item[^1].Equals("end"))
                    {
                        string temp = string.Join(',', item);
                        if(traveledPaths.Add(temp))
                        {
                            exit = false;
                        }
                    }
                }
            } while (!exit);
            foreach(var item in traveledPaths)
            {
                Console.WriteLine($"Path {item}");
            }
            Console.WriteLine($"Number of paths are {traveledPaths.Count}");
        }
    }
}
