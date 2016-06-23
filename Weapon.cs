using System;

namespace SpaceGame
{
	public class Weapon
	{
		public  string name  { get; set; }
		public int power { get; set; }
		public int missrate{get;set;}
		
		public Weapon(string name, int power, int missrate)
		{
			this.name = name;
			this.power = power;
			this.missrate = missrate;
		}//close Weapon()
	}//close Weapon class
}//close namespace