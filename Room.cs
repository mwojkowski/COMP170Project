using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Room
	{//opens Room
		public int name	{get; set;}//Name the Room
		public List<int> possibleMoves {get; set;}
		//list of possible moves with the location of each room		
		

		public Room(int name, int west, int north, int south, int east)
		{
			this.name = name;

			possibleMoves = new List<int>();

			if(west != 0)
				possibleMoves.Add(west);

			if(east != 0)
				possibleMoves.Add(east);

			if(north != 0)
				possibleMoves.Add(north);

			if(south != 0)
				possibleMoves.Add(south);
		}//close Room()

	}//close class
}

