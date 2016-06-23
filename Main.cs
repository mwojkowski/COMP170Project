/*
 * Eric, Krunal, Matthew Sarah
 * Space Man Game
 * Version 1.0
 * This version compiles, and gives basic directions for all functions, objects and variables.
 * What needs to be done: Story Line. When you see a monster, what do they say?
 * Names of Monsters?
 * What kind of things should we display to get the player more into the game/plot?
 * The 'Final Boss' method needs to be completed so that there is a final way to end the game. 
 * */
using System;
using System.IO;
using System.Collections.Generic;

namespace SpaceGame
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Plot.Intro();
			//Plot plot = new Plot();
			//bool cutscene = false;
			int userLocation = Game.GetDefaultLocation();
			
			Game.ShowWelcomeScreen(userLocation);


			Player P1 = new Player ("Player", 500, 0, false);


			Weapon[] weapons = Game.CreateWeapons();

			Weapon currentWeapon = new Weapon("Pistol", 5, 1);
			P1.AddWeapon(currentWeapon);
			//AddWeapon will add a certain weapon to a player's inventory if they dont already have that. 

			//took out the set difficulty here. 
				
			int level;


			while(!P1.gameOver)//while the player hasn't won the game,
			{
				Monster[] monsters = Game.createMonsters(P1);
				//if statements all coded with {} due to the fact that we are not sure if we are adding anything else there
				level = Game.getPlayerLevel (P1.experience);
				if(level == 1)
				{
					Console.WriteLine ("You are level " + level);
					P1 = Game.Battle (P1, monsters[1], weapons);
				}//close if
				else if(level == 2)
				{
					Console.WriteLine ("You are level " + level);
					P1 = Game.Battle(P1, monsters[2], weapons);
				}//close else if
				else if(level==3)
				{
					Console.WriteLine ("You are level " + level);
					P1 = Game.Battle (P1, monsters[3], weapons);
				}//close else if
				else if(level==4)
				{
					Console.WriteLine ("You are level " + level);
					Game.Battle (P1, monsters[4], weapons);
				}//close else if
				else
				{
					Console.WriteLine ("You are level " + level);
					Plot.Cutscene ();
					Game.Battle (P1, monsters[5], weapons);
				}//close else

				if(P1.health <= 0)
					P1.gameOver = true;

				if(!P1.gameOver)
				{//after the battle, ask the user where they want to go. 
					Game.ShowWelcomeScreen(userLocation);
					Game.displayStats(P1);
					int response = UI.PromptIntInRange("\nWhere should we go next? ", 1, 9);
					while(!Game.isValidMoveResponse(response, userLocation))
					{
						Console.WriteLine("You cant go there!");
						response = UI.PromptIntInRange("Where should we go? ", 1, 9);
					}//close while statement
					userLocation = response;
				}//close if gameOver
			}//close while game isn't over
			Plot.Endgame(P1);
			
			
			
			Console.ReadLine();
		}//close Main()
	}//close class
}//close namspace