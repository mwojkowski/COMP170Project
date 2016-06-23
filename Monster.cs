using System;

namespace SpaceGame
{
	public class Monster
	{
		public string name	{ get; set; }
		public string quote	{ get; set;	}
		public int damage	{	get; set;	}
		public int expmet	{	get; set;	}
		public int expgain	{	get; set;	}
		public int health	{	get; set;	}
		
		public Monster(string name, int damage, int expmet, int health, string quote, int expgain)
		{
			this.name = name;
			this.damage = damage;
			this.expmet= expmet;
			this.health = health;
			this.quote = quote;
			this.expgain = expgain;
			
		}//close Monster()
	}//close Monster Class
}