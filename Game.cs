using System;
using System.IO;
using System.Collections.Generic;


namespace SpaceGame
{
	public class Game
	{
		
		public static int GetResponse(string msg)
		{
			int roomno = UI.PromptIntInRange("Enter the room's number: ", 1, 9);
			return roomno;
		}

		//used to get random weapon being used that the monster is guarding.
		public static Weapon GetRandomWeapon(Weapon[] weapons)
		{
			Random random = new Random ();
			int randomNumber = random.Next (1, 9);
			return weapons[randomNumber];
		}

		// create weapons
		public static Weapon[] CreateWeapons()
		{
			Weapon[] a = new Weapon[10];
			
			a[1] = new Weapon("Knife", 5, 1);
			a[2] = new Weapon("Generic Assault Rifle", 15, 2);
			a[3] = new Weapon("Heavy Machine Gun", 25, 4);
			a[4] = new Weapon("Shotgun", 20, 3);
			a[5] = new Weapon("Grenade Launcher", 50, 7);
			a[6] = new Weapon("Rocket Launcher", 50, 7);
			a[7] = new Weapon("Sniper Rifle", 15, 2);
			a[8] = new Weapon("Advanced Energy Weapon", 35, 9);
			a[9] = new Weapon("Chainsaw", 36, 9);
			
			return a;
		}
		
		
		
		//Sets the Default location of the player to a random location on the 9 room map
		public static int GetDefaultLocation()
		{
			Random random = new Random ();
			return random.Next (1, 9);
		}

		
		//shows the User's location when they first need to move rooms.
		public static void ShowWelcomeScreen(int userLocation)
		{
			Console.WriteLine ("------------------\n" +
			                   "|  1  |  2  |  3 |\n" +
			                   "------------------\n" +
			                   "|  4  |  5  |  6 |\n" +
			                   "------------------\n" +
			                   "|  7  |  8  |  9 |\n" +
			                   "------------------\n\n"+
			                   "You are in room #" + userLocation);
		}

		// make sure that the user supplys a valid response
		public static bool isValidMoveResponse (int response, int userLocation)
		{			
			
			Room thisRoom;
			
			if (userLocation == 1)
				thisRoom = new Room (1, 0, 0, 4, 2);
			else if (userLocation == 2)
				thisRoom = new Room (2, 1, 0, 5, 3);
			else if (userLocation == 3)
				thisRoom = new Room (3, 2, 0, 6, 0);
			else if (userLocation == 4)
				thisRoom = new Room (4, 0, 1, 7, 5);
			else if (userLocation == 5)
				thisRoom = new Room (5, 4, 2, 8, 6);
			else if (userLocation == 6)
				thisRoom = new Room (6, 5, 3, 9, 0);
			else if (userLocation == 7)
				thisRoom = new Room (7, 0, 4, 0, 8);
			else if (userLocation == 8)
				thisRoom = new Room (8, 7, 5, 0, 9);
			else
				thisRoom = new Room (9, 8, 6, 0, 0);
			
			if (response == userLocation) {
				// show error: can not move to the same room
				return false;
			}
			else {
				
				bool found = false;
				//tests to see if the user's input is a possible move
				for(int i = 0; i < thisRoom.possibleMoves.Count; i++)
				{
					if(thisRoom.possibleMoves[i].Equals(response))
					{
						found = true;
					}
				}
				
				return found;
			}
		}
		//Battle method. Takes a lot of parameters and returns player
		public static Player Battle (Player x, Monster y, Weapon[] weapons)
		{
			/*Monster thisMonster;*/
			Weapon currentWeapon;
			string monsterMove = "";
			string playerResponse = "";
			bool chargeUp = false;
			while (y.health > 0 && x.health > 0) {	
				Random rando = new Random ();//create the new random
				int weaponFailure = rando.Next (1, 1000);//random chooses whether or not the player's weapon hits or misses
				//gets players response before if and else statements in battle


				playerResponse = getAttackResponse ("Attack or Defend?");
				while ((!playerResponse.Equals ("attack"))&&(!playerResponse.Equals ("defend"))) 
				{
					Console.WriteLine ("invalid response");
					playerResponse = getAttackResponse ("Attack or Defend?");
				}//close while player input is invalid

				if (chargeUp == true) {
		
					if (playerResponse.Equals ("attack")) 
					{
						//if(x.inventory.Count != 1)//if the players inventory isnt 1,
						//{							//ask what weapon they want to use.
							currentWeapon = x.getNewWeapon ();
						//}
						//if your weapon misfires, and the monster already charged up, you will take huge amounts of damage
						if (weaponFailure % currentWeapon.missrate != 0) {
							Console.WriteLine ("Your weapon missfired!");
							Console.WriteLine ("The " + y.name + " winds up and attacks you twice!");
							x.health = x.health - (y.damage * 2);
						}//close if

						else {
							Console.WriteLine ("You and your opponent both attacked each other. Woah! The monster's " +
								"attack seems to have done double the damage! " +
								"I would be careful....");
							x.health = x.health - (y.damage * 2);
							y.health = y.health - currentWeapon.power;
						}//close else


						Console.WriteLine ("Player health: " + x.health + ", Monster " +
							"Health: " + y.health);
					}//close if
					else {
						Console.WriteLine ("You defended the " + y.name + "'s charged attack! " +
							"The attack was so powerful that it seems as if it still did some damage. " +
							"Good thing you defended.");
						x.health = x.health - (y.damage / 2);
						Console.WriteLine ("Player health: " + x.health + ", Monster " +
							"Health: " + y.health);
						//if you defend, you only lose 1/4 of the health you would if you attacked
						//this is where defending would come in handy
					}//close else statement
					chargeUp = false;//monster does not charge up repeatedly
				}//close if statement chargeup == true

				//this else statement will execute as long as the monster isn't charging up. 
				else {
					//start the actual battle now.....
					Random random = new Random ();//create the new random
					int rand = random.Next (1, 100);//random generated to choose the monster's move

					//statements set the monsters move
					if (rand % 3 == 0) {//monster has 33% change of attacking normally
						monsterMove = "attack";//set monster move to attack
					}//close if
					else if (rand % 5 == 0) {//monster has 25% change of defending
						monsterMove = "defend";//set monster move to defend
					}//close else if
					else if (rand % 17 == 0) {//monster has 5% chance to do nothing
						monsterMove = "nothing";
					}//close else if
					else {
						monsterMove = "charge";
						chargeUp = true;
					}//close else statement

					//begin the battle after choosing the monsters actions and player actions
					if (playerResponse.Equals ("attack") && monsterMove.Equals ("attack")) 
					{
						//if(x.inventory.Count != 1)//if the players inventory isnt 1,
						//{							//ask what weapon they want to use.
							currentWeapon = x.getNewWeapon ();
						//}
						if (weaponFailure % currentWeapon.missrate != 0) {
							Console.WriteLine ("Your weapon missfired!");
							Console.WriteLine ("The " + y.name + " attacks you!");
							x.health = x.health - y.damage;
						}//close if statement

						else {
							// both attack, both lose health
							y.health = y.health - (currentWeapon.power / 2);
							x.health = x.health - (y.damage);
							Console.WriteLine ("You were both hit!");
						}//close else
						//display battle stats
						Console.WriteLine ("Player health: " + x.health + ", Monster " +
							"Health: " + y.health);
					}//close if
					
					else if (playerResponse.Equals ("attack") && monsterMove.Equals ("defend")) 
					{
						//if(x.inventory.Count != 1)//if the players inventory isnt 1,
						//{							//ask what weapon they want to use.
							currentWeapon = x.getNewWeapon ();
						//}
						if (weaponFailure % currentWeapon.missrate != 0) {
							Console.WriteLine ("Your weapon missfired!");
							Console.WriteLine ("Your attack on the " + y.name + " was futile!");
						}//close if statement
						else {
							// Monster defends, player attacks - loses half of player's attack
							y.health = y.health - (currentWeapon.power / 2);
							Console.WriteLine (y.name + " defended your attack!");
						}//close else


						Console.WriteLine ("Player health: " + x.health + ", Monster " +
							"Health: " + y.health);
					}//close else if
					
					else if (playerResponse.Equals ("defend") && monsterMove.Equals ("attack")) {
						// monster attacks, player defends - lose half of monster's attack
						x.health = x.health - (y.damage / 2);
						Console.WriteLine ("You defended " + y.name + "'s attack");
						Console.WriteLine ("Player health: " + x.health + ", Monster " +
							"Health: " + y.health);
					}//close else if
					else if (playerResponse.Equals ("defend") && monsterMove.Equals ("defend")) {
						// remove HALF of the monsters attack from the player's health
						Console.WriteLine ("You defended " + y.name + "'s attack");
						Console.WriteLine ("Player health: " + x.health + ", Monster " +
							"Health: " + y.health);
					}//close else if
					else if (playerResponse.Equals ("attack") && monsterMove.Equals ("charge")) {
						//if(x.inventory.Count != 1)//if the players inventory isnt 1,
						//{							//ask what weapon they want to use.
							currentWeapon = x.getNewWeapon ();
						//}
						if (weaponFailure % currentWeapon.missrate != 0) {
							Console.WriteLine ("Your weapon missfired!");
							Console.WriteLine ("It seems as if the " + y.name + " is up to something....");
						}//close if statement
						else {
							y.health = y.health - (currentWeapon.power);
							Console.WriteLine ("Your attack hit " + y.name + ".");
							if (y.health > 0) {
								Console.WriteLine ("It seems as if " + y.name + " is doing something....");
							}//close if
						}
						chargeUp = true;
					} else if (playerResponse.Equals ("attack") && monsterMove.Equals ("nothing")) {
						//if(x.inventory.Count != 1)//if the players inventory isnt 1,
						//{							//ask what weapon they want to use.
							currentWeapon = x.getNewWeapon ();
						//}
						if (weaponFailure % currentWeapon.missrate != 0) {
							Console.WriteLine ("Your weapon missfired!");
							Console.WriteLine ("It seems as if the " + y.name + " was confused anyway....");
						}//close if statement

						else {
							//just random so that the monster may die faster
							y.health = y.health - (currentWeapon.power / 2);
							Console.WriteLine ("You caught " + y.name + " off guard!");
							Console.WriteLine ("Player health: " + x.health + ", Monster " +
								"Health: " + y.health);
						}
					}//close else if
					else if (playerResponse.Equals ("defend") && monsterMove.Equals ("charge")) {
						Console.WriteLine ("You brace yourself, ready to defend.... Nothing Happens?");
						Console.WriteLine ("The monster seems as if it is planning something....");
						chargeUp = true;
					}//close else if
					else if (playerResponse.Equals ("defend") && monsterMove.Equals ("nothing")) {
						Console.WriteLine ("You brace yourself for your enemy's attack...");
						Console.WriteLine ("Nothing happens. It must've gotten confused.");
					}//close else if
					else {   //if both parties in the battle defend, nothing happens.
						Console.WriteLine ("You both defended, and nothing happened");
						Console.WriteLine ("Player health: " + x.health + ", Monster " +
							"Health: " + y.health);
					}//close else

				}//close else statement for Battle
			}//close while statement
			if (y.health <= 0)
				x.experience = x.experience + y.expgain;
			/// does something at the end of each battle: get a weapon, get health, or get nothing

		
			Random randoms = new Random ();//create the new random
			int randomNumber = randoms.Next (1, 3);
			if (randomNumber == 1) {
				Weapon thisweapon = GetRandomWeapon (weapons);
				Console.WriteLine ("You found a Weapon on the ground!");
				x.AddWeapon (thisweapon);
				Console.WriteLine (thisweapon.name + " has been added to your inventory.");
			}//close if
			else if (randomNumber == 2) {
				Console.WriteLine ("No wonder that monster put up such a fight... You found a health pack!");
				x.health = x.health + 20;
				Console.WriteLine ("Your health has been increated by 20 HP!");
			}//close else if
			else {
				Console.WriteLine ("The monster wasn't holding anything. ");
			}//close else


			if (x.health > 0 && y.name.Equals("SUPREME ALIEN") && y.health < 0) 
			{
				x.gameOver = true;
			}//close if

			return x;


		}//close battle



		//gets the user's attack response
		public static string getAttackResponse(string msg)
		{
			Console.WriteLine(msg);
			
			string response = Console.ReadLine().ToLower().Trim ();
			
			if((response.Equals ("attack"))||(response.Equals ("defend")))
				return response;
			else
				return "";
			
		}//close getAttack Response



		//displays player stats
		public static void displayStats(Player x)
		{
			Console.WriteLine();
			Console.WriteLine("Statistics:");
			Console.WriteLine("Name: " + x.name);
			Console.WriteLine("Health: " + x.health);
			Console.WriteLine("Experience: " + x.experience);
			Console.WriteLine();
		}//close displayStats()

		//this method creates the Monster array and returns it in order to pass 
		public static Monster[] createMonsters(Player P1)
		{

			//a[9] = new Weapon("Chainsaw", 36);     - used as example to declare monsters
			Monster[] mon = new Monster[6];


			mon[1] = new Monster("Grunt", 15, 0, 10, "QUOTE", 25);
			mon[2] = new Monster("Brute", 12, 100, 12, "QUOTE", 50);
			mon[3] = new Monster("Elite", 10, 200, 20, "QUOTE", 75);
			mon[4] = new Monster("Prophet", 7, 300, 50, "QUOTE", 100);
			mon[5] = new Monster("SUPREME ALIEN", 50, 400, P1.health, "QUOTE", 500);


			return mon;
		}//close createMonster

		public static int getPlayerLevel(int x)
		{
			if(x >= 0 && x < 100)
				return 1;
			else if(x >= 100 && x < 200)
				return 2;
			else if(x >= 200 && x < 300)
				return 3;
			else if(x >= 300 && x < 400)
				return 4;
			else
				return 5;
		}//close getPlayerLevel


		////////// ----------   NO CODING PAST THIS POINT      -----------     \\\\\\\\\\\\\\\
	}//close game class
}//close namespace