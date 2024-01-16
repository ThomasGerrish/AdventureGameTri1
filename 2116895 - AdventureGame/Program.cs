using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Numerics;

class Location
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, Location> Directions { get; set; } = new Dictionary<string, Location>();
    public bool IsLocked { get; set; }
    public List<string> HiddenItems { get; set; } = new List<string>();
    public bool HasElectricalSwitches { get; set; } = false;
    public bool AreLightsOn { get; set; } = false; // To track the state of lights upstairs
    public bool HasBeenSearched { get; set; } = false;
    public bool IsBellFixed { get; set; } = false;
}

class Game
{
    public static List<Location> AllLocations { get; } = new List<Location>();
    public static Player player; // Added to reference the player

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to The House on Gables Hill");
        Console.WriteLine("You find yourself inside the lobby of the house");
        Console.WriteLine();
        Console.WriteLine("The commands you can use are:");
        Console.WriteLine();
        Console.WriteLine("Help - Display available options");
        Console.WriteLine("Look - Observe your surroundings");
        Console.WriteLine("Move - Move to a specific room");
        Console.WriteLine("Search - Search the current location");
        Console.WriteLine("Use - Use an item from your inventory");
        Console.WriteLine("Inventory - View your items");
        Location lobby = new Location
        {
            Name = "Lobby",
            Description = "You are in the Lobby. Around you, a large chandelier illuminates the room.",
            HiddenItems = new List<string> { "Heavy Iron Key", "Wooden Broom" } 
        };

    
        Location basementStairs = new Location
        {
            Name = "Basement Stairs",
            Description = "You stand at the top of a dark staircase leading to the basement. You can see an electrical fuse box, But its out of reach. If only you had something to poke it",
            HasElectricalSwitches = true, // Set this property
            IsLocked = true

        };
    
        Location diningRoom = new Location
        {
            Name = "Dining Room",
            Description = "You are in the Dining Room, a dimly lit room with a long wooden table, You can see the door to the kitchen",
            HiddenItems = new List<string> { "Iron Poker"}
        };
        Location kitchen = new Location
        {
            Name = "Kitchen",
            Description = "You are in the Kitchen.It smells heavily of Garlic, Maybe that would be useful....",
             HiddenItems = new List<string> { "Garlic", "Garlic Press" }
        };
        Location lounge = new Location
        {
            Name = "Lounge",
            Description = "You are in the Lounge, a cozy room with a fireplace. Its embers are getting low,",
            HiddenItems = new List<string> {"Brass Key"}
        };
        Location interiorHall = new Location
        {
            Name = "Interior Hall",
            Description = "You are in the Downstairs Hall, lined with portraits and dimly lit, you can see two open doors leading to bedrooms."
        };
        Location smallBedroom1 = new Location
        {
            Name = "Small Bedroom 1",
            Description = "You are in a small, neatly kept bedroom."
        };
        Location smallBedroom2 = new Location
        {
            Name = "Small Bedroom 2",
            Description = "You find yourself in a small bedroom, with a view of the gardens."
        };
        Location driveway = new Location
        {
            Name = "Driveway",
            Description = "The Door is tightly locked, You can't go that way."
        };
        Location basement = new Location
        {
            Name = "Basement",
            Description = "You find yourself in a dingy, damp basement. There looks like theres a part of a bell on the table",
            HiddenItems = new List<string> { "Bell Striker" },
        };
        Location hallwayStairs = new Location
        {
            Name = "Hallway Stairs",
            Description = "The Stairs leading to the 2nd Floor."
        };
        Location upperHall = new Location
        {
            Name = "Upper Hall",
            Description = "You are in the upstairs hallway."
        };
        Location bellTower = new Location
        {
            Name = "Bell Tower",
            Description = "You are in the belltower you see A large bell, missing its striker. If rumors are true, all you have to do is ring this, and eternal reward will be yours.",
            IsLocked = true
        };
        Location upstairsBathroom = new Location
        {
            Name = "Bathroomr",
            Description = "You are in the Bathroom, someone really should clean this."
        };
        Location upstairsBedroom1 = new Location
        {
            Name = "Bedroom",
            Description = "You are in a large bedroom its weridly empty"
        };
        Location upstairsBedroom2 = new Location
        {
            Name = "Bedroom",
            Description = "You are in the a slightly smaller bedroom. Again empty. This seems odd."
        };
        Location study = new Location
        {
            Name = "Study",
            Description = "You are in the Study, The walls are lined with books, papers stacked high.",
            HiddenItems = new List<string> { "Silver Key" },
            IsLocked = true
        };

        // Add all locations to the AllLocations list
        AllLocations.Add(lobby);
        AllLocations.Add(basementStairs);
        AllLocations.Add(diningRoom);
        AllLocations.Add(kitchen);
        AllLocations.Add(lounge);
        AllLocations.Add(interiorHall);
        AllLocations.Add(smallBedroom1);
        AllLocations.Add(smallBedroom2);
        AllLocations.Add(hallwayStairs);
        AllLocations.Add(bellTower);
        AllLocations.Add(driveway);
        AllLocations.Add(upperHall);
        AllLocations.Add(upstairsBathroom);
        AllLocations.Add(upstairsBedroom1);
        AllLocations.Add(upstairsBedroom2);
        AllLocations.Add(study);
     
        //Connections between locations
        lobby.Directions.Add("Dining Room", diningRoom);
        lobby.Directions.Add("Lounge", lounge);
        lounge.Directions.Add("Lobby", lobby);
        lobby.Directions.Add("Interior Hall", interiorHall);
        interiorHall.Directions.Add("Lobby", lobby);
        lobby.Directions.Add("Basement Stairs", basementStairs);
        lobby.Directions.Add("Stairs", hallwayStairs);
        hallwayStairs.Directions.Add("Lobby", lobby);
        hallwayStairs.Directions.Add("Upstairs Hallway", upperHall);
        interiorHall.Directions.Add("Small Bedroom 1", smallBedroom1);
        interiorHall.Directions.Add("Small Bedroom 2", smallBedroom2);
        smallBedroom1.Directions.Add("The Hallway", interiorHall);
        smallBedroom2.Directions.Add("The Hallway", interiorHall);
        diningRoom.Directions.Add("Kitchen", kitchen);
        diningRoom.Directions.Add("Lobby", lobby);
        kitchen.Directions.Add("Dining Room", diningRoom);
        basementStairs.Directions.Add("Basement", basement);
        basementStairs.Directions.Add("Lobby", lobby);
        basement.Directions.Add("Basement Stairs", basementStairs);
        upperHall.Directions.Add("Hallway Stairs", hallwayStairs);
        upperHall.Directions.Add("Bell Tower", bellTower);
        bellTower.Directions.Add("Upper Hall", upperHall);
        upperHall.Directions.Add("Bathroom", upstairsBathroom);
        upstairsBathroom.Directions.Add("Upper Hall", upperHall);
        upperHall.Directions.Add("First Bedroom", upstairsBedroom1);
        upstairsBedroom1.Directions.Add("Upper Hall", upperHall);
        upperHall.Directions.Add("Second Bedroom", upstairsBedroom2);
        upstairsBedroom2.Directions.Add("Upper Hall", upperHall);
        upperHall.Directions.Add("Study", study);
        study.Directions.Add("Upper Hall", upperHall);
        // The Driveway is inaccessible, so it's not added


        player = new Player(lobby); 

        while (!player.HasWon && !player.IsDead)
        {
            player.StartTurn();
            DisplayHelp(); // Show available commands

            Console.WriteLine("\nWhat do you want to do? (Type a command or 'help' for a list of commands)");
            string input = Console.ReadLine().ToLower();


            if (input == "help")
            {
                DisplayHelp();
            }
            else if (input == "look")
            {
                Console.WriteLine(player.CurrentLocation.Description);
            }
            else if (input.StartsWith("move")) 
            {
                string roomName = input.Substring(4).Trim(); 
                if (string.IsNullOrEmpty(roomName))
                {
                    player.ListPossibleDestinations();
                }
                else
                {
                    Console.WriteLine("Attempting to move to: " + roomName);
                    player.Move(roomName); 
                }
            }
            else if (input == "search")
            {
                player.Search();
            }
            else if (input == "use")
            {
                player.Use();
            }
            else if (input == "inventory")
            {
                player.DisplayInventory();
            }
            else if (input == "exit")
            {
                break; // Exit the game loop
            }
            else if (input == "collect")
            {
                player.Collect();
                
            }    
            else
            {
                Console.WriteLine("Invalid command. Type 'help' for a list of commands.");
            }
        }

        if (player.HasWon)
        {
            Console.WriteLine("Congratulations! You have found the treasure and won the game!");
        }
        else if (player.IsDead)
        {
            Console.WriteLine("Game Over. You are dead.");
        }
    }


    static void DisplayHelp()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;

        Console.WriteLine("Commands:");
        Console.WriteLine("  - 'help' : Display this help message");
        Console.WriteLine("  - 'look' : Look around");
        Console.WriteLine("  - 'move' : Type Move ROOMNAME to move to a different location");
        Console.WriteLine("  - 'search' : Search the current location");
        Console.WriteLine("  - 'collect' : Collect an item found in the current location");
        Console.WriteLine("  - 'use' : Use an item from your inventory");
        Console.WriteLine("  - 'inventory' : List your current inventory");
        Console.WriteLine("  - 'exit' : Close the game");
        Console.ResetColor(); // Reset the text colour to default
    }
}

class Player
{
    private int health = 100;
    private List<string> inventory = new List<string>();
    public Location CurrentLocation { get; private set; }
    public bool HasWon { get; private set; }
    public bool IsDead => health <= 0;
    public int Turn { get; private set; } = 0;



    public Player(Location startingLocation)
    {
        CurrentLocation = startingLocation;
    }
    public void StartTurn()
    {
    
        Console.WriteLine("\nTurn: " + Turn);
        Console.WriteLine("Location: " + CurrentLocation.Name);
        Console.WriteLine(CurrentLocation.Description);

        if (IsDead)
        {
            Console.WriteLine("You are dead.");
            return;
        }

        if (health < 100)
        {
            Console.WriteLine("Health: " + health);
        }

        DisplayInventory();
    }
    public void Move(string roomName)
    {
        string lowerRoomName = roomName.ToLower();
        var matchedLocation = CurrentLocation.Directions
            .FirstOrDefault(d => d.Key.ToLower() == lowerRoomName).Value;

        if (matchedLocation == null)
        {
            Console.WriteLine("There is no room by that name here.");
            return;
        }

        if (matchedLocation.Name == "Basement Stairs" && matchedLocation.IsLocked)
        {
            if (inventory.Contains("Heavy Iron Key"))
            {
                Console.ForegroundColor = ConsoleColor.Red; //set the text colour to red
                Console.WriteLine("The Basement Stairs are locked. You might try using the Heavy Iron Key.");
                Console.ResetColor(); // Reset the text color to default

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Basement Stairs are locked. You need a key to unlock it.");
                Console.ResetColor(); 

            }
            return; // Prevents moving to the location
        }

        // Check if the player is trying to move to the "Hallway Stairs" and lights are not on
        if (matchedLocation.Name == "Hallway Stairs" && !Game.AllLocations.Any(loc => loc.AreLightsOn))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("It is too dark to see what you're doing... Maybe you can find a way to turn on the power. You head back to the lobby.");
            Console.ResetColor(); 
            return; 


        }

        if (matchedLocation.IsLocked)
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("The way to " + matchedLocation.Name + " is locked. Maybe there's a key to unlock it?");
            Console.ResetColor(); 

        }
        else
        {
            CurrentLocation = matchedLocation;
            Console.WriteLine("You move to: " + CurrentLocation.Name);
            Console.WriteLine(CurrentLocation.Description);
        }
        //  handling for moving to the Basement
        if (matchedLocation != null && matchedLocation.Name == "Basement")
        {
            if (inventory.Contains("Crushed Garlic"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine("As you enter the Basement, you notice a giant bat. It seems to be trying to get away from you.");
                Console.WriteLine("It quickly climbs into a hole in the wall, fleeing the basement.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("As you enter the Basement, an Enormous Vampyre Bat attacks you!");
                Console.WriteLine("Without Crushed Garlic to protect you, the bat overpowers you...");
                Console.WriteLine("Game Over. You are dead.");
                health = 0; // Set the player's state to dead
                return; // Exit the method to prevent further game actions
            }
        }
        Turn++;
        Console.ResetColor(); 

    }

    public void ListPossibleDestinations()
    {
        if (CurrentLocation.Directions.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("There are no destinations to go to from here.");
            Console.ResetColor();

            return;
        }

        Console.WriteLine("You can go to the following locations:");
        foreach (var direction in CurrentLocation.Directions)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("  - " + direction.Key);
            Console.ResetColor(); 


        }
    }
    public void Search()
    {
        Console.WriteLine("You search around the " + CurrentLocation.Name + "...");
        CurrentLocation.HasBeenSearched = true;

        bool foundSomething = false;

        if (CurrentLocation.Name == "Basement Stairs" && !CurrentLocation.HasElectricalSwitches)
        {
            CurrentLocation.HasElectricalSwitches = true; // Set the flag to true as the switches are now discovered
            Console.WriteLine("You discover some electrical switches on the wall.");
            foundSomething = true;
        }

        if (CurrentLocation.HiddenItems.Any())
        {
            foreach (var item in CurrentLocation.HiddenItems)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You managed to find: " + item);
                Console.ResetColor();
                foundSomething = true;
            }
        }

        if (!foundSomething)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Despite your best efforts, you find nothing useful.");
            Console.ResetColor();
        }
    }


    public void Use()
    {
        if (inventory.Count == 0)
        {
            Console.WriteLine("You have no items to use.");
            return;
        }

        Console.WriteLine("Which item would you like to use? Here's your inventory:");
        foreach (var item in inventory)
        {
            Console.WriteLine("  - " + item);
        }

        string itemName = Console.ReadLine();

        if (inventory.Contains(itemName))
        {
            bool itemUsedSuccessfully = false;

            // Using the Heavy Iron Key on the door to the Basement Stairs
            if (itemName == "Heavy Iron Key" && CurrentLocation.Name == "Lobby")
            {
                var basementStairs = Game.AllLocations.FirstOrDefault(loc => loc.Name == "Basement Stairs");
                if (basementStairs != null && basementStairs.IsLocked)
                {
                    basementStairs.IsLocked = false;
                    Console.ForegroundColor = ConsoleColor.DarkGreen; 
                    Console.WriteLine("You use the Heavy Iron Key to unlock the door to the Basement Stairs.");
                    Console.ResetColor(); 

                    inventory.Remove(itemName); // Remove the key from inventory after use
                }
            }
            if (inventory.Contains(itemName))
            {
                // Using the Wooden Broom on the Electrical Switches in the Basement Stairs
                if (itemName == "Wooden Broom" && CurrentLocation.Name == "Basement Stairs" && CurrentLocation.HasElectricalSwitches)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen; 

                    Console.WriteLine("You use the Wooden Broom to reach the Electrical Switches and turn them on.");

                    // Turn on the lights in all locations
                    foreach (var location in Game.AllLocations)
                    {
                        location.AreLightsOn = true;
                    }

                    Console.WriteLine("The lights turn on throughout the house.");
                    Console.WriteLine("The broom snaps as you hit the switch. The power is on, but the broom is now broken.");

                    inventory.Remove(itemName); // Remove the broken broom from inventory
                    Console.ResetColor(); 

                }
                if ((itemName == "Garlic" && inventory.Contains("Garlic Press")) ||
        (itemName == "Garlic Press" && inventory.Contains("Garlic")))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen; 
                    Console.WriteLine("You use the Garlic and the Garlic Press to create Crushed Garlic.");
                    Console.ResetColor();


                    // Remove Garlic and Garlic Press, and add Crushed Garlic to inventory
                    inventory.Remove("Garlic");
                    inventory.Remove("Garlic Press");
                    inventory.Add("Crushed Garlic");
                    Console.ForegroundColor = ConsoleColor.DarkGreen; 
                    Console.WriteLine("Crushed Garlic has been added to your inventory.");
                    Console.ResetColor(); 

                    itemUsedSuccessfully = true;
                }
                // Using the Iron Poker in the Lounge
                if (itemName == "Iron Poker" && CurrentLocation.Name == "Lounge")
                {
                    if (!CurrentLocation.HiddenItems.Contains("Brass Key"))
                    {
                        CurrentLocation.HiddenItems.Add("Brass Key");
                        Console.ForegroundColor = ConsoleColor.DarkGreen; 
                        Console.WriteLine("You stir the embers of the fireplace with the Iron Poker, and a Brass Key falls out of the ashes.");
                        Console.ResetColor(); 
                        itemUsedSuccessfully = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red; 
                        Console.WriteLine("Stirring the embers reveals nothing more.");
                        Console.ResetColor(); 

                    }
                }
                // Using the Brass Key to unlock the Study
                if (itemName == "Brass Key")
                {
                    var study = Game.AllLocations.FirstOrDefault(loc => loc.Name == "Study");
                    if (study != null && study.IsLocked)
                    {
                        study.IsLocked = false;
                        Console.ForegroundColor = ConsoleColor.DarkGreen; 
                        Console.WriteLine("You use the Brass Key to unlock the Study.");
                        Console.ResetColor(); 
                        itemUsedSuccessfully = true;
                    }
                }

                else
                {
                    Console.WriteLine("You can't use that here.");
                }
            }
            // Using the Silver Key to unlock the Bell Tower
            if (itemName == "Silver Key")
            {
                var bellTower = Game.AllLocations.FirstOrDefault(loc => loc.Name == "Bell Tower");
                if (bellTower != null && bellTower.IsLocked)
                {
                    bellTower.IsLocked = false;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("You use the Silver Key to unlock the Bell Tower.");
                    Console.ResetColor();
                    itemUsedSuccessfully = true;
                }
            }
            // Using the Bell Striker in the Bell Tower
            if (itemName == "Bell Striker" && CurrentLocation.Name == "Bell Tower")
            {
                CurrentLocation.IsBellFixed = true;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("You use the Bell Striker to fix the bell.");
                Console.ResetColor();
                itemUsedSuccessfully = true;

                // Prompt to ring the bell
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("The bell is now fixed. Would you like to ring it now? (yes/no)");
                Console.ResetColor();
                string response = Console.ReadLine().ToLower();
                if (response == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("You ring the bell loudly. Congratulations, you have won the game!");
                    Console.ResetColor();

                    HasWon = true;
                }
            }

            
            if (itemUsedSuccessfully)
            {
                inventory.Remove(itemName);
              
            }
        }
        else
        {
            Console.WriteLine("You don't have that item.");
        }
    }

    public void DisplayInventory()
    {
        Console.ForegroundColor = ConsoleColor.Blue;

        Console.WriteLine("Inventory:");
        if (inventory.Count == 0)
        {
            Console.WriteLine("  (empty)");
        }
        else
        {
            foreach (var item in inventory)
            {
                Console.WriteLine("  - " + item);
            }
        }
        Console.ResetColor(); 

    }
    public void Collect()
    {
        if (!CurrentLocation.HasBeenSearched)
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("You should probably search the area to find something to pick up.");
            Console.ResetColor(); 
            return;
        }
        if (!CurrentLocation.HiddenItems.Any())
        {
            Console.WriteLine("There are no items to collect here.");
            return;
        }

        Console.WriteLine("Do you want to collect all items? (yes/no)");
        string collectAllResponse = Console.ReadLine().ToLower();

        if (collectAllResponse == "yes")
        {
            foreach (var item in CurrentLocation.HiddenItems)
            {
                inventory.Add(item);
                Console.WriteLine($"You have collected: {item}");
            }
            CurrentLocation.HiddenItems.Clear();
        }
        else
        {
            Console.WriteLine("Which item would you like to collect? Here are the items:");
            foreach (var item in CurrentLocation.HiddenItems)
            {
                Console.WriteLine("  - " + item);
            }

            string itemName = Console.ReadLine();

            if (CurrentLocation.HiddenItems.Contains(itemName))
            {
                inventory.Add(itemName);
                CurrentLocation.HiddenItems.Remove(itemName);
                Console.WriteLine($"You have collected: {itemName}");
            }
            else
            {
                Console.WriteLine("That item is not here.");
            }
        }
    }
}

