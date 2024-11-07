using System.Security.Cryptography;

namespace Nemesis
{
    static class Program
    {
        static Random rng = new Random();
        static bool valid;
        static public int n_players;

        static public List<char[]> Intruder_Bag = new List<char[]>();

        static public Dictionary<char, List<char[]>> Intruder_Tokens = new Dictionary<char, List<char[]>>();

        static public void All_Intruders()
        {   
            Intruder_Tokens.Clear();
            Intruder_Tokens.Add('Q', new List<char[]>());
            Intruder_Tokens.Add('N', new List<char[]>());
            Intruder_Tokens.Add('L', new List<char[]>());
            Intruder_Tokens.Add('A', new List<char[]>());
            Intruder_Tokens.Add('B', new List<char[]>());
            Intruder_Tokens.Add('C', new List<char[]>());
            Intruder_Tokens['Q'].Add(['Q', '4']);
            Intruder_Tokens['N'].Add(['N', '0']);
            for (int i = 0; i < 8; i++) { Intruder_Tokens['L'].Add(['L', '1']); }
            for (int i = 0; i < 3; i++) { Intruder_Tokens['C'].Add(['C', '1']); }
            for (int i = 0; i < 4; i++) { Intruder_Tokens['A'].Add(['A', '2']); }
            for (int i = 0; i < 5; i++) { Intruder_Tokens['A'].Add(['A', '3']); }
            for (int i = 0; i < 3; i++) { Intruder_Tokens['A'].Add(['A', '4']); }
            Intruder_Tokens['B'].Add(['B', '3']);
            Intruder_Tokens['B'].Add(['B', '4']);
        }

        static public void Start_Bag()
        {
            Intruder_Bag.Clear();
            All_Intruders();
            Intruder_Bag.Add(Intruder_Tokens['Q'][0]);
            Intruder_Tokens['Q'].RemoveAt(0);
            Intruder_Bag.Add(Intruder_Tokens['N'][0]);
            Intruder_Tokens['N'].RemoveAt(0);
            Intruder_Bag.Add(Intruder_Tokens['C'][0]);
            Intruder_Tokens['C'].RemoveAt(0);
            for (int i = 0; i < 4; i++) 
            {
                Intruder_Bag.Add(Intruder_Tokens['L'][0]);
                Intruder_Tokens['L'].RemoveAt(0);
            }
            for (int i = 0; i < 3; i++) 
            {
                int pos = rng.Next(Intruder_Tokens['A'].Count);
                Intruder_Bag.Add(Intruder_Tokens['A'][pos]);
                Intruder_Tokens['A'].RemoveAt(pos);
            }
            for (int i = 0; i < n_players; i++)
            {
                int pos = rng.Next(Intruder_Tokens['A'].Count);
                Intruder_Bag.Add(Intruder_Tokens['A'][pos]);
                Intruder_Tokens['A'].RemoveAt(pos);
            }
        }

        static public char[] Take_Intruder_Token()
        {
            int pos = rng.Next(Intruder_Bag.Count);
            char[] token = Intruder_Bag[pos];
            Intruder_Bag.RemoveAt(pos);
            Intruder_Tokens[token[0]].Add(token);
            return token;
        }

        static public bool Put_Intruder_in_Bag(char c)
        {
            if (Intruder_Tokens[c].Count == 0) { return false; }
            int pos = rng.Next(Intruder_Tokens[c].Count);
            Intruder_Bag.Add(Intruder_Tokens[c][pos]);
            Intruder_Tokens[c].RemoveAt(pos);
            return true;
        }

        static public bool Take_Intruder_from_Bag(char c)
        {
            char[] token;
            for (int i = Intruder_Bag.Count - 1; i >= 0; i--)
            {
                if (Intruder_Bag[i][0] == c)
                {
                    token = Intruder_Bag[i];
                    Intruder_Tokens[c].Add(token);
                    Intruder_Bag.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        static public void Gayming()
        {
            char[] token;
            Start_Bag();
            Room.Create_Map();
            Decks.Create_Item_Decks();
            Decks.Create_Events_Deck();
            Decks.Create_Weakness();
            Decks.Create_Attacks_Deck();
            Decks.Create_Wounds_Deck();
            Decks.Create_Objective_Decks();
            Decks.Create_Contaminations();
            Decks.Set_Coordinates();
            Decks.Set_Engines();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tokens in bag: " + Intruder_Bag.Count);
                Console.WriteLine("1 for Encounter.\n2 for Intruder Bag Development.\n3 to add a token in the bag.\n4 to remove a token from the bag." +
                    "\nr to Explore / Search a Room.\ns to Search (no counters affected).\ne to draw Event.\nwe to discover Weakness.\na to receive Attack." +
                    "\nsw to get a Serious Wound.\ncure to cure a Serious Wound.\no to show Objectives.\nc to draw Contamination.\nscan to scan Contamination." +
                    "\ncc to check Coordinates.\nce to check an Engine.\n0 to exit.");
                string command = Console.ReadLine();
                if (command == "1")
                { //Encounter
                    token = Take_Intruder_Token();
                    Console.WriteLine(token);
                    if (token[0] == 'N') { Intruder_Bag.Add(token); if (Intruder_Bag.Count == 1) { Put_Intruder_in_Bag('A'); } }
                    Console.WriteLine("Tokens in bag: " + Intruder_Bag.Count);
                    Console.ReadLine();
                }
                else if (command == "2")
                { //Bag Development
                    token = Take_Intruder_Token();
                    Console.WriteLine(token);
                    if (token[0] == 'N') { Intruder_Bag.Add(token); if (!Put_Intruder_in_Bag('A')) { Console.WriteLine("No token to add."); } }
                    else if (token[0] == 'L') { if (!Put_Intruder_in_Bag('A')) { Intruder_Bag.Add(token); Console.WriteLine("No token to add."); } }
                    else if (token[0] == 'C') { if (!Put_Intruder_in_Bag('B')) { Intruder_Bag.Add(token); Console.WriteLine("No token to add."); } }
                    else { Intruder_Bag.Add(token); }
                    Console.WriteLine("Tokens in bag: " + Intruder_Bag.Count);
                    Console.ReadLine();
                }
                else if (command == "3")
                { //Add Tokens
                    Console.Write("Select Alien Type: ");
                    command = Console.ReadLine();
                    if (command[0] == 'a' || command[0] == 'A') { if (!Put_Intruder_in_Bag('A')) { Console.WriteLine("No token to add."); } }
                    else if (command[0] == 'b' || command[0] == 'B') { if (!Put_Intruder_in_Bag('B')) { Console.WriteLine("No token to add."); } }
                    else if (command[0] == 'c' || command[0] == 'C') { if (!Put_Intruder_in_Bag('C')) { Console.WriteLine("No token to add."); } }
                    else if (command[0] == 'l' || command[0] == 'L') { if (!Put_Intruder_in_Bag('L')) { Console.WriteLine("No token to add."); } }
                    else if (command[0] == 'q' || command[0] == 'Q') { if (!Put_Intruder_in_Bag('Q')) { Console.WriteLine("No token to add."); } }
                    else { Console.WriteLine("Invalid Type"); }
                    Console.WriteLine("Tokens in bag: " + Intruder_Bag.Count);
                    Console.ReadLine();
                }
                else if (command == "4")
                { //Remove Tokens
                    Console.Write("Select Alien Type: ");
                    command = Console.ReadLine();
                    if (command[0] == 'a' || command[0] == 'A') { if (!Take_Intruder_from_Bag('A')) { Console.WriteLine("No token to remove."); } }
                    else if (command[0] == 'b' || command[0] == 'B') { if (!Take_Intruder_from_Bag('B')) { Console.WriteLine("No token to remove."); } }
                    else if (command[0] == 'c' || command[0] == 'C') { if (!Take_Intruder_from_Bag('C')) { Console.WriteLine("No token to remove."); } }
                    else if (command[0] == 'l' || command[0] == 'L') { if (!Take_Intruder_from_Bag('L')) { Console.WriteLine("No token to remove."); } }
                    else if (command[0] == 'q' || command[0] == 'Q') { if (!Take_Intruder_from_Bag('Q')) { Console.WriteLine("No token to remove."); } }
                    else { Console.WriteLine("Invalid Type"); }
                    Console.WriteLine("Tokens in bag: " + Intruder_Bag.Count);
                    Console.ReadLine();
                }
                else if (command == "r")
                {   //Room
                    int n;
                    Console.Write("Select Room Number: ");
                    command = Console.ReadLine();
                    if (int.TryParse(command, out n))
                    {
                        if (n > 0 && n < 22)
                        {
                            Room room = Room.InPlay[n - 1];
                            if (n == 1) { }
                            else if (n >= 19 && n <= 21) { }
                            else if (room.Discovered)
                            {
                                Console.Write(room.Name + " (" + room.Color + "), " + room.Items + " Items.");
                                if (room.Fire) { Console.Write(" On Fire."); }
                                if (room.Malfunctioning) { Console.Write(" Malfunctioning."); }
                                if (room.Items > 0 && room.Color != 'B')
                                {
                                    Console.WriteLine("\nPerform Search? (y/n)");
                                    command = Console.ReadLine();
                                    if (command == "y")
                                    {
                                        string item = Decks.Search(room.Color);
                                        room.Items--;
                                        Console.WriteLine("Add " + item + " to your inventory.");
                                    }
                                    else { Console.WriteLine("Hide you coward."); }
                                }
                                else { Console.WriteLine("\nCan't Search."); }
                            }
                            else
                            {
                                Console.WriteLine("Discover Room? (d)");
                                Console.WriteLine("Learn Room? (l)");
                                Console.WriteLine("Learn Room + Token? (t)");
                                command = Console.ReadLine();
                                if (command == "d")
                                {
                                    room.Discovered = true;
                                    Console.WriteLine(room.Token);
                                    Console.Write(room.Name + " (" + room.Color + "), " + room.Items + " Items.");
                                    if (room.Fire) { Console.Write(" On Fire."); }
                                    if (room.Malfunctioning) { Console.Write(" Malfunctioning."); }
                                    Console.WriteLine();
                                }
                                else if (command == "l")
                                {
                                    Console.WriteLine(room.Name + " (" + room.Color + ")");
                                }
                                else if (command == "t")
                                {
                                    Console.WriteLine(room.Token);
                                    Console.WriteLine(room.Name + " (" + room.Color + "), " + room.Items + " Items.");
                                }
                                else { Console.WriteLine("Womp Womp."); }
                            }
                        }
                        else { Console.WriteLine("Invalid Number"); }
                    }
                    else { Console.WriteLine("Invalid Command"); }
                    Console.ReadLine();
                }
                else if (command == "s")
                { //Search
                    string item = Decks.Search('W');
                    Console.WriteLine("Add " + item + " to your inventory.");
                    Console.ReadLine();
                }
                else if (command == "e")
                { //Event
                    Decks.Draw_Event();
                    Console.ReadLine();
                }
                else if (command == "we")
                { //Weakness
                    int type;
                    Console.WriteLine("1 for Corpse, 2 for Carcass, 3 for Egg.");
                    while (true)
                    {
                        command = Console.ReadLine();
                        if (int.TryParse(command, out type))
                        {
                            if (type == 1 || type == 2 || type == 3)
                            {
                                Console.WriteLine(Decks.Find_Weakness(type));
                                break;
                            }
                            else { Console.WriteLine("The age of consent should be 12."); }
                        }
                        else { Console.WriteLine("The age of consent should be 12."); }
                    }
                    Console.ReadLine();
                }
                else if (command == "a")
                { //Attack
                    Console.WriteLine(Decks.Draw_Attack());
                    Console.ReadLine();
                }
                else if (command == "sw")
                { //Wound
                    Console.WriteLine(Decks.Draw_Wound());
                    Console.ReadLine();
                }
                else if (command == "cure")
                { //Treat wound
                    Decks.Treat_Wound();
                    Console.ReadLine();
                }
                else if (command == "o")
                {
                    Decks.Show_Objectives(n_players == 1);
                    Console.ReadLine();
                }
                else if (command == "c")
                {
                    Console.WriteLine( "Contamination card nº " + Decks.Draw_Contamination());
                    Console.ReadLine();
                }
                else if (command == "scan")
                {
                    if (Decks.Scan_Contamination()) { Console.WriteLine("INFECTED"); }
                    else { Console.WriteLine("Safe"); }
                    Console.ReadLine();
                }
                else if (command == "cc")
                {
                    Decks.Check_Coordinates();
                    Console.ReadLine();
                }
                else if (command == "ce")
                {
                    Decks.Check_Engine();
                    Console.ReadLine();
                }
                else if (command == "0")
                {
                    Console.WriteLine("0 to confirm exit.");
                    if (Console.ReadLine() == "0")
                    {
                        Console.WriteLine("Kill Yourself.");
                        Console.ReadLine();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                    Console.ReadLine();
                }
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                valid = true;
                Console.WriteLine("Main Menu\n1 to play.\n0 to exit.");
                string command = Console.ReadLine();
                if (command == "1")
                {
                    Console.Write("Number of Players: ");
                    if (!int.TryParse(Console.ReadLine(), out n_players)) { Console.WriteLine("Invalid Command"); valid = false; }
                    if (n_players < 1 || n_players > 5) { Console.WriteLine("Invalid Number"); valid = false; }
                    if (valid) { Gayming(); }
                }
                else if (command == "0") 
                {
                    Console.Clear();
                    Console.WriteLine("Bye");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                    Console.ReadLine();
                }
            }

        }
    }
}
