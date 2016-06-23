using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Player
	{
		public string name {get; set;}
		public int health {get; set;}
		public int experience {get; set;}
		public bool gameOver{ get; set; }
		//public List<Weapon> inventory {get; set;} 
		public List<Weapon> inventory = new List<Weapon>();
		
		public Player(string name, int health, int experience, bool wonGame)
		{
			this.name = name;
			this.health = health;
			this.experience = experience;
			this.gameOver = gameOver;
		}//close Player()


		
		public void AddWeapon (Weapon thisWeapon)
		{
			bool unique = true;
			
			for (int i = 0; i< inventory.Count; i++) {
				
				string thisOjectName = inventory [i].name;
				string newObjectName = thisWeapon.name;
				
				if (thisOjectName.Equals(newObjectName)) {
					unique = false;
				}
			}
			
			if (unique) {
				inventory.Add (thisWeapon);
				Console.WriteLine ("You now have a {0}", thisWeapon.name);
			}
		}//close AddWeapon


		// prints a list of weapons in this users inventory
		public Weapon getNewWeapon()
		{
			Console.WriteLine ("Pick a weapon:");
			for (int i =0; i<inventory.Count; i++) {
				Weapon thisWeapon = inventory [i];
				Console.WriteLine ("[{0}] {1}", i + 1, thisWeapon.name);
			}
			
			int response = UI.PromptIntInRange("Enter the weapon's number: ", 1, inventory.Count);
			
			while (response > inventory.Count && response <= 0) {
				
				Console.WriteLine("invalid weapon id. try again:");
				response = UI.PromptIntInRange("Enter the weapon's number: ", 1, inventory.Count);
			}
			
			return inventory[response-1];
		}
		
		
		
		
	}//close class
	
}