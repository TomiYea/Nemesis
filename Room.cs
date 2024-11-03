using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis
{
    internal class Room
    {
        public string Name { get; set; }
        public char Color { get; set; }
        public string Token { get; set; }
        public int Items { get; set; }
        public bool Fire {  get; set; }
        public bool Malfunctioning { get; set; }
        public bool Discovered { get; set; }

        static List<Room> Class1 { get; set; }
        static List<Room> Class2 { get; set; }
        static public Room[] InPlay { get; set; }
        static List<string[]> Tokens { get; set; }
        public Room(string n, char c) 
        { 
            Name = n;
            Color = c;
            Discovered = false;
        }
        public Room(string n)
        {
            Name = n;
            Discovered = true;
        }

        static Random rng = new Random();
        static public void Shuffle(List<Room> c)
        {
            int n = c.Count;
            Room aux;
            int j;
            for (int i = 0; i < n - 1; i++)
            {
                j = rng.Next(i, n);
                aux = c[i];
                c[i] = c[j];
                c[j] = aux;
            }
        }
        static public void Shuffle(List<string[]> c)
        {
            int n = c.Count;
            string[] aux;
            int j;
            for (int i = 0; i < n - 1; i++)
            {
                j = rng.Next(i, n);
                aux = c[i];
                c[i] = c[j];
                c[j] = aux;
            }
        }

        static public void Load_Rooms()
        {
            Class1 = new List<Room>();
            Class1.Add(new Room("Armory", 'R'));
            Class1.Add(new Room("Comms Room", 'Y'));
            Class1.Add(new Room("Emergency Room", 'G'));
            Class1.Add(new Room("Evacuation Section A", 'W'));
            Class1.Add(new Room("Evacuation Section B", 'W'));
            Class1.Add(new Room("Fire Control System", 'Y'));
            Class1.Add(new Room("Generator", 'Y'));
            Class1.Add(new Room("Laboratory", 'G'));
            Class1.Add(new Room("Nest", 'B'));
            Class1.Add(new Room("Storage", 'R'));
            Class1.Add(new Room("Surgery", 'G'));

            Class2 = new List<Room>();
            Class2.Add(new Room("Airlock Control", 'Y'));
            Class2.Add(new Room("Cabins", 'W'));
            Class2.Add(new Room("Canteen", 'G'));
            Class2.Add(new Room("Command Center", 'R'));
            Class2.Add(new Room("Engine Control Room", 'Y'));
            Class2.Add(new Room("Hatch Control System", 'W'));
            Class2.Add(new Room("Monitoring Room", 'R'));
            Class2.Add(new Room("Room Covered in Slime", 'B'));
            Class2.Add(new Room("Shower Room", 'W'));
        }

        static public void Load_Tokens()
        {
            Tokens = new List<string[]>();
            Tokens.Add(["2", "noise"]);
            Tokens.Add(["3", "noise"]);
            Tokens.Add(["1", "door"]);
            Tokens.Add(["2", "door"]);
            Tokens.Add(["3", "door"]);
            Tokens.Add(["4", "door"]);
            Tokens.Add(["1", "malf"]);
            Tokens.Add(["1", "malf"]);
            Tokens.Add(["2", "malf"]);
            Tokens.Add(["2", "malf"]);
            Tokens.Add(["2", "malf"]);
            Tokens.Add(["2", "malf"]);
            Tokens.Add(["3", "malf"]);
            Tokens.Add(["4", "malf"]);
            Tokens.Add(["1", "fire"]);
            Tokens.Add(["2", "fire"]);
            Tokens.Add(["1", "silence"]);
            Tokens.Add(["1", "silence"]);
            Tokens.Add(["3", "slime"]);
            Tokens.Add(["4", "slime"]);
        }

        static public void Create_Map()
        {
            InPlay = new Room[21];
            InPlay[0] = new Room("Cockpit");
            InPlay[20] = new Room("Engine #1");
            InPlay[19] = new Room("Engine #2");
            InPlay[18] = new Room("Engine #3");
            InPlay[10] = new Room("Hibernatorium");

            Load_Rooms();
            Load_Tokens();

            Shuffle(Class1);
            for (int i = 1; i < 5; i++) { InPlay[i] = Class1[i-1]; }
            InPlay[8] = Class1[4];
            InPlay[9] = Class1[5];
            InPlay[11] = Class1[6];
            InPlay[12] = Class1[7];
            for (int i = 15; i < 18;  i++) { InPlay[i] = Class1[i - 7]; }

            Shuffle(Class2);
            for (int i = 5; i < 8; i++) { InPlay[i] = Class2[i - 5]; }
            for (int i = 13; i < 15; i++) { InPlay[i] = Class2[i - 10]; }

            Shuffle(Tokens);
            for (int i = 1; i < 18; i++)
            {
                if(i != 10) 
                { 
                    InPlay[i].Token = Tokens[i][1];
                    int items = int.Parse(Tokens[i][0]);
                    if (Program.n_players != 1) { InPlay[i].Items = items; }
                    else { InPlay[i].Items = items / 2; if (items % 2 > 0) { InPlay[i].Items++; } }
                }
            }
        }
    }
}
