using System;
using System.IO;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Plot
	{
		//This is the intro that plays at the start of the game.
		public static void Intro()
		{
			Console.WriteLine("You are a space marine. Your ability to speak has");
			Console.WriteLine("been taken from you through the ravages of war.");
			Console.WriteLine("A fairy has been provided to speak for you.");
			Console.WriteLine("You have amnesia. You are in a zone filled with");
			Console.WriteLine("hostile alien monsters. You are the only survivor of");
			Console.WriteLine("your crew. Find and Fight the SUPREME ALIEN. Kill him!");
			Console.WriteLine("Only then, will the madness end.");
		}
		
		//This is the cutscene that plays once the payer's exp hits a certain point.
		//The boolean value returned ensures the cutscene won't keep on repeating.
		public static void Cutscene()
		{
			Console.WriteLine("SUPREME ALIEN: Your boss never told you what happened to your father.");
			Console.WriteLine("FAIRY: He told me enough!");
			Console.WriteLine("He told me you killed him!");
			Console.WriteLine("SUPREME ALIEN: No. I am your father.");
			Console.WriteLine("FAIRY: No... that's");
			Console.WriteLine("not true! That's impossible!");
			Console.WriteLine("SUPREME ALIEN: Search your DNA. You know it to be true.");
			Console.WriteLine("FAIRY: NOOOOOOOOO!");         
			Console.WriteLine("NOOOOOOOO!");         
		}
		
		//Returns true, which is used to set gameOver to true either way.
		//Since you've revamping the battle system, Matt, you should probably be the
		//one to give the SUPREME ALIEN his battle stats.
		public static void Endgame(Player P1)
		{
			Console.WriteLine("FAIRY: I've realized the truth.");
			Console.WriteLine("I killed my squad. But now I am on the side of good.");
			if (P1.health > 0) {
				Console.WriteLine("You've won the game!");
			}
			else 
			{
				Console.WriteLine("SUPREME ALIEN: Foolish child, you shouldn't have come.");
				Console.WriteLine("Now I shall devour you and gain ultimate power!");
				Console.WriteLine("The SUPREME ALIEN has gained ultimate power. GAME OVER");            
			}
		}
	}
}
