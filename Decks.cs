using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis
{
    internal static class Decks
    {
        static public Dictionary<char, List<string>> Item_Decks;

        static public List<string> Events_Deck;
        static public List<string> Events_Discard;

        static public List<string> Weaknesses;

        static public List<string> Attacks_Deck;

        static public List<string> Wounds_Deck;
        static public List<string> Wounds_Discard;

        static public List<string> Personal_Objectives;
        static public List<string> Coorporate_Objectives;
        static public List<string> Solo_Coop_Objectives;

        static public List<int> Contaminations;
        static public bool[] Infections;

        static public List<string> Coordinates;

        static public bool[] Engines;

        static Random rng = new Random();
        static public void Shuffle(List<string> c)
        {
            int n = c.Count;
            string aux;
            int j;
            for (int i = 0; i < n - 1; i++)
            {
                j = rng.Next(i, n);
                aux = c[i];
                c[i] = c[j];
                c[j] = aux;
            }
        }

        static public void Create_Item_Decks()
        {
            Item_Decks = new Dictionary<char, List<string>>();
            Item_Decks['R'] = new List<string>();
            Item_Decks['Y'] = new List<string>();
            Item_Decks['G'] = new List<string>();

            Item_Decks['R'].Add("Prototype: Pistol {3} (You can reroll once every Combat roll you make with this Weapon.)");
            Item_Decks['R'].Add("Prototype: Shotgun {2} (You always deal at least 1 Injury, except on a “blank”. If you roll “1 hit” or “2 hits”, you deal 1 additional Injury.)");
            Item_Decks['R'].Add("Prototype: Rifle {6} (Every time you roll “2 hits” you can discard 1 additional Ammo to deal 1 additional Injury.)");
            Item_Decks['R'].Add("Extended Magazine {Add to Energy Weapon}* (Add 2 AMMO to one of your Energy Weapons. From now on, this Weapon has +2 AMMO capacity.)");
            Item_Decks['R'].Add("Self-Destruct Key {1, One Use Only}* (If you are in a Room with a Computer, initiate or stop the Self-Destruct sequence (unless if the Self-Destruct marker is at any yellow space).)");
            Item_Decks['R'].Add("Evacuation Key {1, One Use Only}* (If you are in an Evacuation Section Room, Lock or Unlock 1 of the Escape Pods in this Evacuation Section.)");
            Item_Decks['R'].Add("Comms Key {1, One Use Only}* (If you are in a Room with a Computer, look at one of the Objective cards of 1 Character with a Signal marker on their Character board.)");
            for (int i = 0; i < 11;  i++) { Item_Decks['R'].Add("Energy Charge {1, One Use Only, “electric”} (Fully load AMMO in one Energy Weapon. |OR| Open / Close 1 Door in any Corridor connected to the Room you are in.)"); }
            for (int i = 0; i < 4; i++) { Item_Decks['R'].Add("Grenade {1, One Use Only} (Choose 1 Room with an Intruder (the one you are in or neighboring). The chosen Intruder suffers 2 Injuries. All other Characters and Intruders in the targeted Room (including you) suffer 1 Injury / Serious Wound.)"); }
            for (int i = 0; i < 3; i++) { Item_Decks['R'].Add("Smoke Grenade {1, One Use Only} (Use in the Room you are in. All other Characters in the Room discard 1 Action card each. You move to a neighboring Room and Intruders don’t Attack you during that Movement.)"); }
            for (int i = 0; i < 3; i++) { Item_Decks['R'].Add("Recon Drone {1, One Use Only}* (Check 1 unexplored Room tile and look at its Exploration token.)"); }
            for (int i = 0; i < 2; i++) { Item_Decks['R'].Add("Decoy {1, One Use Only} (Choose 1 neighboring Room. Take all Intruders from neighboring Rooms (even in Combat) and move them to this Room (normal rules of Movement apply). Each Intruder in Combat performs 1 Attack before leaving its Room.)"); }

            Item_Decks['Y'].Add("Nemesis Plans {1, One Use Only}* (Check any 2 unexplored Room tiles (without looking at their Exploration token).)");
            Item_Decks['Y'].Add("Space Suit {1, One Use Only}* (If you are in a Yellow Room, discard all your Action cards and Move your Character to any other Yellow Room.)");
            Item_Decks['Y'].Add("Technical Corridors Plans {1, One Use Only}* (If you are in a Room with a Technical Corridors Entrance, move your Character to any other Room with a Technical Corridors Entrance.)");
            for (int i = 0; i < 3; i++) { Item_Decks['Y'].Add("Energy Charge {1, One Use Only, “electric”} (Fully load AMMO in one Energy Weapon. |OR| Open / Close 1 Door in any Corridor connected to the Room you are in.)"); }
            for (int i = 0; i < 3; i++) { Item_Decks['Y'].Add("Clothes {1, One Use Only, “cloth”}* (Discard a Slime marker. |OR| Dress 1 Serious Wound.)"); }
            for (int i = 0; i < 7; i++) { Item_Decks['Y'].Add("Chemicals {1, One Use Only, “fire”}* (Fully load AMMO in a Flamethrower.)"); }
            for (int i = 0; i < 4; i++) { Item_Decks['Y'].Add("Fire Extinguisher {1, One Use Only, Heavy} (Discard a Fire marker from the Room you are in. |OR| Use on 1 Intruder in the Room you are in - it Retreats.)"); }
            for (int i = 0; i < 6; i++) { Item_Decks['Y'].Add("Tools {1, One Use Only, “screwdriver”}* (Discard a Malfunction marker from the Room you are in. |OR| Repair / Break the Engine in the Engine Room you are in. |OR| Open / Close 1 Door in any Corridor connected to the Room you are in.)"); }
            for (int i = 0; i < 4; i++) { Item_Decks['Y'].Add("Duct Tape {1, One Use Only}* (Discard a Malfunction marker from the Room you are in. |OR| Repair / Break the Engine in the Engine Room you are in. |OR| Use to add 1 Heavy Item to 1 Heavy Item in your Character’s Hand (effectively carrying 2 Heavy Items in 1 Hand).)"); }

            for (int i = 0; i < 7; i++) { Item_Decks['G'].Add("Medkit {1, One Use Only, “health”}* (Dress 1 Serious Wound. |OR| Heal 1 Dressed Serious Wound.)"); }
            for (int i = 0; i < 5; i++) { Item_Decks['G'].Add("Synthetic Food {1, One Use Only}* (Draw 2 Action cards from your deck.)"); }
            for (int i = 0; i < 3; i++) { Item_Decks['G'].Add("Adrenaline Injection {1, One Use Only} (Draw 1 card. You may play all your Actions in this single turn. Then, you must pass.)"); }
            for (int i = 0; i < 2; i++) { Item_Decks['G'].Add("Military Drugs {1, One Use Only} (Discard any number of cards from your hand (even Contamination cards) and draw up to the same number +1.)"); }
            for (int i = 0; i < 3; i++) { Item_Decks['G'].Add("Clothes {1, One Use Only, “cloth”}* (Discard a Slime marker. |OR| Dress 1 Serious Wound.)"); }
            for (int i = 0; i < 7; i++) { Item_Decks['G'].Add("Bandages {1, One Use Only, “cloth”}* (Dress 1 Serious Wound. |OR| Heal all Light Wounds.)"); }
            for (int i = 0; i < 3; i++) { Item_Decks['G'].Add("Alcohol {1, One Use Only, “fire”}* (Scan and remove 1 Contamination card from your hand. If it was INFECTED, take 1 Contamination card.)"); }

            Shuffle(Item_Decks['R']);
            Shuffle(Item_Decks['Y']);
            Shuffle(Item_Decks['G']);
        }

        static public string Search(char c)
        {
            if (c == 'W')
            {
                while (true)
                {
                    Console.WriteLine("Pick a color (R, Y, G)");
                    string command = Console.ReadLine();
                    if (command == "R" || command == "r") { c = 'R'; break; }
                    else if (command == "Y" || command == "y") { c = 'Y'; break; }
                    else if (command == "G" || command == "g") { c = 'G'; break; }
                    else { Console.WriteLine("I will touch you."); }
                }
            }
            string opt1 = Item_Decks[c][0];
            string opt2 = Item_Decks[c][1];
            Item_Decks[c].RemoveRange(0, 2);
            Console.WriteLine("1: " + opt1);
            Console.WriteLine("2: " + opt2);
            while (true)
            {
                Console.Write("Make your choice: ");
                string command = Console.ReadLine();
                if (command == "1") { Item_Decks[c].Add(opt2); return opt1; }
                else if (command == "2") { Item_Decks[c].Add(opt1); return opt2; }
                else { Console.WriteLine("I am inside your walls."); }
            }
        }

        static public void Create_Events_Deck()
        {
            Events_Deck = new List<string>();
            Events_Discard = new List<string>();
            Events_Deck.Add("Egg Protection [B, Q] {2} (Resolve an Encounter for each Character who is in the Nest or is carrying an intruder Egg.)");
            Events_Deck.Add("Hatching [A, B] {3} (Discard 1 Egg from the Intruder board. Any Characters in the Nest Room whose players have no Action cards left are Infested by a Larva. If no Character was Infested, put a Larva token into the Intruder bag.)");
            Events_Deck.Add("Regeneration [B, Q] {1} (Each Intruder on the board Heals 2 Injuries.)");
            Events_Deck.Add("Damage [C, A] {2} (Put a Malfunction marker in each Room with an Adult Intruder, Breeder, or Queen.)");
            Events_Deck.Add("Lurking [C, A] {4} (Remove from the board all Intruders which are not in a Room with a Character. Put their respective tokens into the Intruder bag.)");
            Events_Deck.Add("Eclosion [C, B, Q] {1} (Each Character with a Larva on their Character board dies (place a Creeper in their Room). Every Character draws 4 cards from their deck and Scans all drawn Contamination cards. If they have at least 1 INFECTED card, put a Larva on their Character board. Discard drawn cards.)");
            Events_Deck.Add("Noise in Technical Corridors [A, B] {4} (Place a Noise marker in the Technical Corridor, if there isn't already one there. If there is, each Character in a Room with a Technical Corridor Entrance performs a Noise roll.");
            Events_Deck.Add("Nest [C, B, Q] {3} (If the Nest Room is explored, place a Noise marker in each Corridor that is connected to the Nest. Do not place a Noise marker if one is already present.)");
            Events_Deck.Add("Flammable Compounds [B, Q] {4} (Place a Fire marker in the Hibernatorium. If the Hibernatorium is already on fire, place a Fire marker in each neighboring Room. Fire does not spread through Closed Doors or Technical Corridors.)");
            Events_Deck.Add("Bulkheads Open [A, B] {1} (Open all Doors (except Destroyed Doors).)");
            Events_Deck.Add("Hunt [B, Q] {3} (Move every Adult Intruder not in Combat to a neighboring Room with a Character, if possible. If there are several Rooms to choose from, move the Adult Intruder to the Room with the lowest Room Number.)");
            Events_Deck.Add("Hunt [C, B, Q] {2} (Move every Adult Intruder not in Combat to a neighboring Room with a Character, if possible. If there are several Rooms to choose from, move the Adult Intruder to the Room with the lowest Room Number.)");
            Events_Deck.Add("Scent of Prey [C, A] {3} (Place a Noise marker in every Corridor connected to a Room containing a Character with a Slime marker (except Corridors that already have Noise markers).)");
            Events_Deck.Add("Malfunction [A, B] {2} (Place a Malfunction marker in the explored Room with the lowest Room Number. Then, shuffle this card back into the Events deck.)");
            Events_Deck.Add("Consuming Fire [C, B, Q] {4} (Set the Item Counter to 0 in each Room with a Fire marker. Place a Fire marker in each Room neighboring a Room with a Fire marker. Fire does not spread through Closed Doors or Technical Corridors.)");
            Events_Deck.Add("Damaging Fire [C, A] {1} (Place a Malfunction marker in each Room with a Fire marker. Place a Fire marker in each Room neighboring a Room with a Fire marker. Fire does not spread through Closed Doors or Technical Corridors.)");
            Events_Deck.Add("Coolant Leak [A, B, Q] {1} (If there is a Malfunction marker in the Generator Room, start the Self-Destruct countdown. REMOVE this Event from the game and reshuffle the Events deck (including the discard pile).)");
            Events_Deck.Add("Life Support Failure [A, B, Q] {2} (Place a Malfunction marker in each explored Green Room. REMOVE this Event from the game and reshuffle the Events deck (including the discard pile).)");
            Events_Deck.Add("Evacuation Pod Ejection [A, B, Q] {3} (Launch the Escape Pod token with the lowest number. REMOVE this Event from the game and reshuffle the Events deck (including the discard pile).)");
            Events_Deck.Add("Short Circuit [A, B, Q] {4} (Place a Malfunction marker on each explored Yellow Room with a Computer. REMOVE this Event from the game and reshuffle the Events deck (including the discard pile).)");
            Shuffle(Events_Deck);
        }

        static public string Draw_Event(char c)
        {
            if (Events_Deck.Count == 0)
            {
                Events_Deck.AddRange(Events_Discard);
                Events_Discard.Clear();
                Shuffle(Events_Deck);
            }
            string command;
            string card = Events_Deck[0];
            if (c == 'e')
            {
                Events_Deck.RemoveAt(0);
                Console.WriteLine(card + "\nProceed with Event? (y/n)");
                while (true)
                {
                    command = Console.ReadLine();
                    if (command == "y")
                    {
                        if (card.Contains("REMOVE"))
                        {
                            Events_Deck.AddRange(Events_Discard);
                            Events_Discard.Clear();
                            Shuffle(Events_Deck);
                        }
                        else if (card.StartsWith("Malf"))
                        {
                            Events_Deck.Add(card);
                            Shuffle(Events_Deck);
                        }
                        else if (card.StartsWith("Consuming"))
                        {
                            int room;
                            Events_Discard.Add(card);
                            Console.WriteLine("What rooms are on fire?");
                            while (true)
                            {
                                Console.WriteLine("Select a room. (\"end\" to end)");
                                command = Console.ReadLine();
                                if (int.TryParse(command, out room))
                                {
                                    if (room >= 0 && room < 21)
                                    {
                                        Room.InPlay[room].Items = 0;
                                    }
                                    else { Console.WriteLine("I am in your room."); }
                                }
                                else if (command == "end") { break; }
                                else { Console.WriteLine("I am in your room."); }
                            }
                        }
                        else { Events_Discard.Add(card); }
                        break;
                    }
                    else if (command == "n") { Events_Discard.Add(card); break; }
                    else { Console.WriteLine("Come kiss me on my hot mouth."); }
                }
            }
            else if (c == 'r')
            {
                Console.WriteLine(card + "\nKeep or Delay? (k/d)");
                while (true)
                {
                    command = Console.ReadLine();
                    if (command == "d")
                    {
                        Events_Deck.RemoveAt(0);
                        Events_Deck.Add(card);
                        Console.WriteLine("Event placed at the bottom of the deck.");
                        break;
                    }
                    else if (command == "k") { Console.WriteLine("Event kept at the top of the deck."); break; }
                    else { Console.WriteLine("You can't save them."); }
                }
            }
            return card;
        }

        static public void Create_Weakness()
        {
            List<string> deck = new List<string>();
            deck.Add("Vulnerability to Fire (When an Intruder suffers an Injury from Fire, Molotov Cocktail or Flamethrower, it suffers 1 additional Injury.)");
            deck.Add("Vulnerability to Energy (Any Energy Weapon Attack that deals at least 1 Injury deals 1 additional Injury.)");
            deck.Add("Reaction to Danger (The value of tokens when checking Surprise Attack is reduced by 1 (to a minimum of 1).)");
            deck.Add("The Way of Moving (Intruders other than the Queen and Breeders cannot destroy Closed Doors.)");
            deck.Add("Vital Places (“blank” Combat die results against Adult Intruders are treated as “1 hit” instead.)");
            deck.Add("Species on the Brink of Extinction (The amount of Injuries needed to kill any Intruder is decreased by 1.)");
            deck.Add("The Way of Fighting (If an Adult Intruder attacks you with a “Bite”, it deals a Light Wound instead of a Serious Wound.)");
            deck.Add("Susceptibility to Phosphates (Each Intruder affected by the Fire Extinguisher or Fire Control System retreats and suffers 1 Injury.)");

            Weaknesses = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                int pos = rng.Next(deck.Count);
                Weaknesses.Add(deck[pos]);
                deck.RemoveAt(pos);
            }
        }

        static public string Find_Weakness(int c)
        {
            return Weaknesses[c - 1];
        }

        static public void Create_Attacks_Deck()
        {
            string card;
            string[] hp;
            Attacks_Deck = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                card = "Scratch ";
                hp = ["{2}", "{3}", "{5}", "{6}"];
                card += hp[i];
                card += " [C, A, B, Q] (The Character suffers 1 Light Wound and gets 1 Contamination card.)";
                Attacks_Deck.Add(card);
            }
            for (int i = 0; i < 4; i++)
            {
                card = "Claw Attack ";
                hp = ["{4}", "{3}", "{5}", "{>}"];
                card += hp[i];
                card += " [A, B, Q] (The Character suffers 2 Light Wounds and gets 1 Contamination card.)";
                Attacks_Deck.Add(card);
            }
            for (int i = 0; i < 4; i++)
            {
                card = "Bite ";
                hp = ["{4}", "{4}", "{>}", "{6}"];
                card += hp[i];
                card += " [A, B, Q] (If the Character has 2 Serious Wounds they die. If not, they suffer 1 Serious Wound.)";
                Attacks_Deck.Add(card);
            }
            for (int i = 0; i < 1; i++)
            {
                card = "Slime ";
                hp = ["{5}"];
                card += hp[i];
                card += " [C, A, B, Q] (The Character gets a Slime marker and 1 Contamination card.)";
                Attacks_Deck.Add(card);
            }
            for (int i = 0; i < 1; i++)
            {
                card = "Summoning ";
                hp = ["{3}"];
                card += hp[i];
                card += " [C, Q] (Draw 1 Intruder token from the Intruder bag and place this Intruder in the Room. This new Intruder doesn’t perform any attacks (including Surprise Attacks) during the Action that summoned it.)";
                Attacks_Deck.Add(card);
            }
            for (int i = 0; i < 2; i++)
            {
                card = "Transformation ";
                hp = ["{4}", "{5}"];
                card += hp[i];
                card += " [C] (Replace the attacking Creeper with a Breeder (if available). If the player has no cards in hand, the Breeder makes a Surprise Attack.)";
                Attacks_Deck.Add(card);
            }
            for (int i = 0; i < 2; i++)
            {
                card = "Frenzy ";
                hp = ["{4}", "{3}"];
                card += hp[i];
                card += " [B, Q] (All Characters in the Room who have at least 2 Serious Wounds die. All other Characters in this Room suffer 1 Serious Wound.)";
                Attacks_Deck.Add(card);
            }
            for (int i = 0; i < 2; i++)
            {
                card = "Tail Attack ";
                hp = ["{2}", "{5}"];
                card += hp[i];
                card += " [Q] (If the Character has at least 1 Serious Wound, they die. If not, they suffer 1 Serious Wound.)";
                Attacks_Deck.Add(card);
            }
            Shuffle(Attacks_Deck);
        }

        static public string Draw_Attack()
        {
            string card;
            if (Attacks_Deck.Count == 0)
            {
                Create_Attacks_Deck();
            }
            card = Attacks_Deck[0];
            Attacks_Deck.RemoveAt(0);
            return card;
        }

        static public void Create_Wounds_Deck()
        {
            Wounds_Deck = new List<string>();
            Wounds_Discard = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                Wounds_Deck.Add("Arm (The Character has only 1 Hand slot for Heavy Items / Objects. If they have 2 Heavy Items they must instantly drop 1.)");
            }
            for (int i = 0; i < 3; i++)
            {
                Wounds_Deck.Add("Leg (The Cost of your Escape Movement Action is 2.)");
            }
            for (int i = 0; i < 4; i++)
            {
                Wounds_Deck.Add("Body (You draw up to 4 Action cards instead of 5.)");
            }
            for (int i = 0; i < 3; i++)
            {
                Wounds_Deck.Add("Bleeding (Each time you pass in the Player Phase, your Character suffers 1 Light Wound.)");
            }
            for (int i = 0; i < 3; i++)
            {
                Wounds_Deck.Add("Hand (The Cost of all your Item Actions is increased by 1.)");
            }
            Shuffle(Wounds_Deck);
        }

        static public string Draw_Wound()
        {
            if (Wounds_Deck.Count == 0)
            {
                Wounds_Deck.AddRange(Wounds_Discard);
                Wounds_Discard.Clear();
                Shuffle(Wounds_Deck);
            }
            string card = Wounds_Deck[0];
            Wounds_Deck.RemoveAt(0);
            return card;
        }

        static public void Treat_Wound()
        {
            string command;
            int pos;
            string[] wounds = ["Arm (The Character has only 1 Hand slot for Heavy Items / Objects. If they have 2 Heavy Items they must instantly drop 1.)",
                "Leg (The Cost of your Escape Movement Action is 2.)",
                "Body (You draw up to 4 Action cards instead of 5.)",
                "Bleeding (Each time you pass in the Player Phase, your Character suffers 1 Light Wound.)",
                "Hand (The Cost of all your Item Actions is increased by 1.)"];
            Console.WriteLine("Which Wound? 1 for Arm; 2 for Leg; 3 for Body; 4 for Bleeding; 5 for Hand.");
            while (true)
            {
                command = Console.ReadLine();
                if (int.TryParse(command, out pos))
                {
                    if (pos > 0 && pos < 6) { Wounds_Discard.Add(wounds[pos - 1]); break; }
                    else { Console.WriteLine("Vou tirar as calças."); }
                }
                else { Console.WriteLine("Vou tirar as calças."); }
            }
        }

        static public void Create_Objective_Decks()
        {
            Personal_Objectives = new List<string>();
            Coorporate_Objectives = new List<string>();
            Solo_Coop_Objectives = new List<string>();
            Personal_Objectives.Add("Salvaging the Prime Asset {2+} (The ship must reach Earth. |OR| Your Character is the only survivor.)");
            Personal_Objectives.Add("Quarantine {2+} (The ship must reach Mars |OR| The ship must reach Earth AND the Nest must be destroyed.)");
            Personal_Objectives.Add("The Great Hunt {2+} (Send the Signal AND the ship must be destroyed. |OR| Send the Signal AND the Queen must be killed.)");
            Personal_Objectives.Add("Hoarder {2+} (Finish the game in an Escape Pod AND you must have at least 7 Items. Quest Items count only if they are active.)");
            Personal_Objectives.Add("Relentless Investigator {2+} (Send the Signal AND all Rooms on the ship must be explored.)");
            Personal_Objectives.Add("A Proper Burial {2+} (Send the Signal AND finish the game in an Escape Pod (or Hibernation) with the blue Character Corpse Object.)");
            Coorporate_Objectives.Add("The Right Moment to Strike {2+} (Player 1’s Character cannot survive. |OR| Your Character is the only survivor.)");
            Coorporate_Objectives.Add("Greener Pastures {2+} (Player 2’s Character cannot survive. |OR| Your Character is the only survivor.)");
            Coorporate_Objectives.Add("Extreme Field Biology {2+} (At least 2 Intruder Weaknesses must be discovered.)");
            Coorporate_Objectives.Add("Ab Ovo {2+} (Intruder Egg Weakness must be discovered.)");
            Coorporate_Objectives.Add("Necroscopy {2+} (Send the Signal AND Intruder Carcass Weakness must be discovered.)");
            Coorporate_Objectives.Add("My Precious {2+} (Send the Signal AND finish the game in an Escape Pod (or Hibernation) with an Intruder Egg Object.)");
            Solo_Coop_Objectives.Add("Destination: Earth (The ship must reach Earth.)");
            Solo_Coop_Objectives.Add("Emergency Post-Mortem (Place the blue Character Corpse Object in the Surgery Room.)");
            Solo_Coop_Objectives.Add("First Contact Protocol (At least 2 Intruder Weaknesses must be discovered.)");
            Solo_Coop_Objectives.Add("Cutting Off the Head (Send the Signal AND the Queen must have been killed. |OR|  Send the Signal AND the ship must have been destroyed.)");
            Solo_Coop_Objectives.Add("No Man Left Behind (Send the Signal AND all Rooms on the ship must be explored.)");
            Solo_Coop_Objectives.Add("Special Delivery (Finish the game in an Escape Pod or Hibernation with an Intruder Egg Object.)");
            Solo_Coop_Objectives.Add("Clean-Up Crew (Send the Signal AND the Nest must have been destroyed. |OR| Send the Signal AND the ship must have been destroyed.)");
            if (Program.n_players >= 3)
            {
                Personal_Objectives.Add("The Oldest Friend {3+} (The ship must reach Earth. |OR| Your Character is the only survivor.)");
                Coorporate_Objectives.Add("An Old Feud {3+} (Player 3’s Character cannot survive. |OR| Your Character is the only survivor.)");
            }
            if (Program.n_players >= 4)
            {
                Personal_Objectives.Add("Best Friends Forever {4+} (You and at least one other Character must survive.)");
                Coorporate_Objectives.Add("Hostile Takeover {4+} (Player 4’s Character cannot survive. |OR| Your Character is the only survivor.)");
            }
            if (Program.n_players >= 5)
            {
                Personal_Objectives.Add("Aliens on a Spaceship {5+} (Send the Signal AND the Nest must be destroyed. |OR| Send the Signal AND the ship must be destroyed.)");
                Coorporate_Objectives.Add("The Final Enlightenment {5+} (Player 5’s Character cannot survive. |OR| Your Character is the only survivor.)");
            }
            Shuffle(Personal_Objectives);
            Shuffle(Coorporate_Objectives);
            Shuffle(Solo_Coop_Objectives);
        }

        static public int Get_Objectives_Code(string personal, string coorporate)
        {
            int code = 11;
            string[] pers = ["Sal", "Qua", "The G", "Hoa", "Rel", "A P", "The O", "Bes", "Ali"];
            string[] coorp = ["The R", "Gre", "Ext", "Ab", "Nec", "My", "An", "Hos", "The F"];
            for (int i = 0; i < 9; i++) 
            {
                if (coorporate.StartsWith(coorp[i])) { code += i; }
                if (personal.StartsWith(personal[i])) { code += i * 10; }
            }
            return code;
        }

        static public void Show_Objectives(bool solo)
        {
            int player;
            string command;
            if (solo)
            {
                Console.WriteLine(Solo_Coop_Objectives[0]);
                Console.WriteLine(Solo_Coop_Objectives[1]);
            }
            else
            {
                Console.WriteLine("Which player?");
                while(true)
                {
                    command = Console.ReadLine();
                    if (int.TryParse(command, out player))
                    {
                        if (player > 0 && player <= Program.n_players)
                        {
                            Console.WriteLine(Personal_Objectives[player]);
                            Console.WriteLine(Coorporate_Objectives[player]);
                            Console.WriteLine("Code: " + Get_Objectives_Code(Personal_Objectives[player], Coorporate_Objectives[player]));
                            break;
                        }
                        else { Console.WriteLine("I'm a registed sex offender."); }
                    }
                    else { Console.WriteLine("I'm a registed sex offender."); }
                }
            }
        }

        static public void Present_Objectives()
        {
            if (Program.n_players == 1) 
            {
                Console.Clear();
                Console.WriteLine("Objectives:");
                Console.WriteLine(Solo_Coop_Objectives[0]);
                Console.WriteLine(Solo_Coop_Objectives[1]);
                Console.ReadLine();
            }
            else
            {
                for (int i = 1; i <= Program.n_players; i++) 
                {
                    Console.Clear();
                    Console.WriteLine("Player " + i + "'s Objectives:");
                    Console.ReadLine();
                    Console.WriteLine(Personal_Objectives[i]);
                    Console.WriteLine(Coorporate_Objectives[i]);
                    Console.WriteLine("Code: " + Get_Objectives_Code(Personal_Objectives[i], Coorporate_Objectives[i]));
                    Console.ReadLine();
                }
            }
        }

        static public void Create_Contaminations()
        {
            int ran;
            Contaminations = new List<int>();
            Infections = new bool[27];
            for (int i = 0; i < 27; i++)
            {
                Contaminations.Add(i);
                Infections[i] = false;
            }
            for (int i = 0; i < 7; i++)
            {
                ran = rng.Next(27);
                while (Infections[ran]) 
                { 
                    ran = rng.Next(27);
                }
                Infections[ran] = true;
            }
        }

        static public int Draw_Contamination()
        {
            int card = Contaminations[0];
            Contaminations.RemoveAt(0);
            return card;
        }

        static public bool Scan_Contamination()
        {
            int cont;
            string command;
            Console.WriteLine("Which contamination?");
            while (true)
            {
                command = Console.ReadLine();
                if (int.TryParse(command, out cont))
                {
                    if (cont >= 0 && cont < 27)
                    {
                        return Infections[cont];
                    }
                    else { Console.WriteLine("THE FOG IS COMMING. THE FOG IS COMMING."); }
                }
                else { Console.WriteLine("THE FOG IS COMMING. THE FOG IS COMMING."); }
            }
        }

        static public void Set_Coordinates()
        {
            Coordinates = new List<string> { "Earth", "Mars", "Venus", "Deep Space" };
            Shuffle(Coordinates);
        }

        static public void Check_Coordinates()
        {
            Console.WriteLine("A: " + Coordinates[0]);
            Console.WriteLine("B: " + Coordinates[1]);
            Console.WriteLine("C: " + Coordinates[2]);
            Console.WriteLine("D: " + Coordinates[3]);
        }

        static public void Set_Engines()
        {
            int ran;
            Engines = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                ran = rng.Next(2);
                if (ran == 0) { Engines[i] = true; }
                else { Engines[i] = false; }
            }
        }

        static public void Check_Engine()
        {
            int cont, ran;
            string command;
            Console.WriteLine("Which engine?");
            while (true)
            {
                command = Console.ReadLine();
                if (int.TryParse(command, out cont))
                {
                    if (cont > 0 && cont <= 3)
                    {
                        cont--;
                        if (Engines[cont]) { Console.WriteLine("Working"); }
                        else { Console.WriteLine("Broken"); }
                        break;
                    }
                    else { Console.WriteLine("Sit on my face."); }
                }
                else { Console.WriteLine("Sit on my face."); }
            }
            Console.WriteLine("Would you like to change it? (y/n)");
            while (true)
            {
                command = Console.ReadLine();
                if (command == "y")
                {
                    ran = rng.Next(2);
                    if (ran == 0)
                    {
                        Console.WriteLine("1 to Fix.");
                        Console.WriteLine("2 to Break.");
                        while (true)
                        {
                            command = Console.ReadLine();
                            if (command == "1") { Engines[cont] = true; break; }
                            else if (command == "2") { Engines[cont] = false; break; }
                            else { Console.WriteLine("Shut yo bitch ass up."); }
                        }
                    }
                    else
                    {
                        Console.WriteLine("1 to Break.");
                        Console.WriteLine("2 to Fix.");
                        while (true)
                        {
                            command = Console.ReadLine();
                            if (command == "1") { Engines[cont] = false; break; }
                            else if (command == "2") { Engines[cont] = true; break; }
                            else { Console.WriteLine("Shut yo bitch ass up."); }
                        }
                    }
                    break;
                }
                else if (command == "n") { break; }
                else { Console.WriteLine("The moon landing was fake and I can prove it."); }
            }
        }
    }
}
