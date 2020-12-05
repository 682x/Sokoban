using System;
using static System.Console;

namespace Bme121
{
    static class Sokoban
    {      
        static int curRow ; // Find the row coordinate of the user
        static int curColumn ; // Find the column  coordinate of the user
        public static bool hasWon= false; // is hasWon = True, means Win 
        static int numberOfTarget; //Calculate the number of goals (Winning point)
        
        static int[ , ] map = new int[8,8]; //Create a Sokoban map
        
        public static void CreateMap()
        {
			for (int i = 0; i < 8; i++)
			 for (int j = 0; j < 8; j++)
				map[i, j] = GetTile(i, j);
			bool[] taken = new bool[72];
			Array.Fill(taken, true);
			taken[4 * 8 + 4] = false;
			int owo = 0;
			while (owo < 8)
			{
				Random rand = new Random();
				int x = rand.Next(1, 7);
				int y = rand.Next(1, 7);
				if (taken[x * 8 + y] && CheckTile(x, y))
				{
					map [x, y] = 2 + (owo % 2) * 2;
					taken[x * 8 + y] = false;
					owo++;
				}
			}
			map[4,4] = 3;
		}
		
		static int GetTile(int x, int y)
		{
			Random rand = new Random();
			
			int tile = rand.Next(0, 10);
			if (x == 0 || x == 7 || y == 0 || y == 7) tile -= 2;
			if (tile < 7) return 0;
			else return 1;
		}
		
		static bool CheckTile(int x, int y)
		{
			int open = 0;
			if (map[x - 1, y] == 0) open++;
			if (map[x + 1, y] == 0) open++;
			if (map[x, y - 1] == 0) open++;
			if (map[x, y + 1] == 0) open++;
			if (open > 2) return true;
			else return false;
		}
        
        // Draw the Sokoban map
        public static void Draw()
        {
            
            string pd = "\u0023"; // pound sign (Wall)
            string at = "\u0040"; // at sign (Player)
            string bO = "\u004F"; // capital O (boxes being pushed)
            string pl = "\u002B"; // plus sign(target locations)
            string sp = " ";     // space (None)  
            string bX = "\u0058"; // capital X (a box successfully pushed to its target location.)
            Clear();
            
            for (int i = 0; i < 8; i++)
            {
				for (int j = 0; j < 8; j++)
				{
					switch(map[i, j])
					{
						case 1:
							Write(pd);
							break;
						case 2:
							Write(bO);
							break;
						case 3:
							Write(at);
							break;
						case 4:
							Write(pl);
							break;
						case 5:
							Write(bX);
							break;
						default:
							Write(sp);
							break;
					}
				}
				WriteLine();
			}
        }
        
        //Find the user current location
        public static void FindUser()
        {
            for (int i=0; i<map.GetLength(0);i++)
            {
                for (int j=0; j<map.GetLength(1);j++)
                {
                    if(map[i,j]==3) 
                    {
                        curRow    = i;
                        curColumn = j;
                    }
                    
                }
            }
        }
        
        //When the user move up
        public static void Up()
        {    
            FindUser(); //Find user
            
            if (curRow == 0) return;
            
            //Check whether the next coordinate is space(if Yes, do sth)
            
           if (map[curRow - 1, curColumn] == 0)
			{
				map[curRow - 1, curColumn] = 3;
				map[curRow, curColumn] = 0;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a target location behind the box (If Yes, do sth)
            
            else if (map[curRow - 1, curColumn] == 2)
			{
				if (curRow == 1) return;
				if (map[curRow - 2, curColumn] == 0)
				{
					map[curRow - 1, curColumn] = 3;
					map[curRow - 2, curColumn] = 2;
					map[curRow, curColumn] = 0;
				}
				else if (map[curRow - 2, curColumn] == 4)
				{
					map[curRow - 1, curColumn] = 3;
					map[curRow - 2, curColumn] = 5;
					map[curRow, curColumn] = 0;
				}
				else return;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a space behind the box (If Yes, do sth)
             
           //update the map
           Draw();
           //check if wins
           WinPoint(); 
        }
        
        
         public static void Down()
        {    
            FindUser();
            
            if (curRow == 7) return;
            
            //Check whether the next coordinate is space(if Yes, do sth)
            
            if (map[curRow + 1, curColumn] == 0)
			{
				map[curRow + 1, curColumn] = 3;
				map[curRow, curColumn] = 0;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a target location behind the box (If Yes, do sth)
            
            else if (map[curRow + 1, curColumn] == 2)
			{
				if (curRow == 6) return;
				if (map[curRow + 2, curColumn] == 0)
				{
					map[curRow + 1, curColumn] = 3;
					map[curRow + 2, curColumn] = 2;
					map[curRow, curColumn] = 0;
				}
				else if (map[curRow + 2, curColumn] == 4)
				{
					map[curRow + 1, curColumn] = 3;
					map[curRow + 2, curColumn] = 5;
					map[curRow, curColumn] = 0;
				}
				else return;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a space behind the box (If Yes, do sth)
            
           
           Draw(); 
           WinPoint();
        }
        
        public static void Left()
        {   
            FindUser();   
            
            if (curColumn == 0) return;        
            
            //Check whether the next coordinate is space(if Yes, do sth)
            
            if (map[curRow, curColumn - 1] == 0)
			{
				map[curRow, curColumn - 1] = 3;
				map[curRow, curColumn] = 0;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a target location behind the box (If Yes, do sth)
            
            //TODO...
            else if (map[curRow, curColumn - 1] == 2)
			{
				if (curColumn == 1) return;
				if (map[curRow, curColumn - 2] == 0)
				{
					map[curRow, curColumn - 1] = 3;
					map[curRow, curColumn - 1] = 3;
					map[curRow, curColumn - 2] = 2;
					map[curRow, curColumn] = 0;
				}
				else if (map[curRow, curColumn - 2] == 4)
				{
					map[curRow, curColumn - 1] = 3;
					map[curRow, curColumn - 2] = 5;
					map[curRow, curColumn] = 0;
				}
				else return;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a space behind the box (If Yes, do sth)
            
            
           Draw();
           WinPoint(); 
        }
        public static void Right()
        {   
            FindUser(); 
            
            if (curColumn == 7) return;          
            
            //Check whether the next coordinate is space(if Yes, do sth)
            
            if (map[curRow, curColumn + 1] == 0)
			{
				map[curRow, curColumn + 1] = 3;
				map[curRow, curColumn] = 0;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a target location behind the box (If Yes, do sth)
            
            else if (map[curRow, curColumn + 1] == 2)
			{
				if (curColumn == 6) return;
				if(map[curRow, curColumn + 2] == 0)
				{
					map[curRow, curColumn + 1] = 3;
					map[curRow, curColumn + 2] = 2;
					map[curRow, curColumn] = 0;
				}
				else if(map[curRow, curColumn + 2] == 4)
				{
					map[curRow, curColumn + 1] = 3;
					map[curRow, curColumn + 2] = 5;
					map[curRow, curColumn] = 0;
				}
				else return;
			}
            
            //Check whether the next coordinate is box and 
            //whether there is a space behind the box (If Yes, do sth)
            
            
           Draw(); 
           WinPoint();
        }
        
        //Calculate the number of target to know the winning points
        public static void CountTarget()
        {
            
           numberOfTarget = 0;
           for (int i = 0; i < 8; i++)
           {
			   for (int j = 0; j < 8; j++)
				if (map[i,j] == 4) numberOfTarget++;
		   }
           
        }
        
        //number of boxes are placed in the right location 
        //Need to calculate the number of Capital X in each round 
        public static void WinPoint()
        {
			CountTarget();
           if (numberOfTarget == 0) hasWon = true;
        }
    }
    
    static class Program
    {
        
        static void Main( )
        {   
            Sokoban.CreateMap();
            Sokoban.CountTarget();
            Sokoban.FindUser();
            Sokoban.Draw();
            
            //input the direction from user
            while(!Sokoban.hasWon)
            {
                WriteLine("Please input w,a,s or d to move the user(@)");
                WriteLine("w for moving up    ; s for moving down; ");
                WriteLine("d for moving right ; a for moving left; ");
                string input = (ReadLine());
                switch(input)
                {
                    case "w":                    
                        Sokoban.Up();
                        break;
                                        
                    case "s": 
                        Sokoban.Down();
                        break; 
                    case "d": 
                        Sokoban.Right();
                        break;
                    
                    case "a": 
                        Sokoban.Left();
                        break;
                }
            
            }
            WriteLine("Congratulations!!!You win.");
        }
    }
}
