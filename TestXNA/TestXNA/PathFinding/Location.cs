using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;



namespace TestXNA.PathFinding
{
    class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
       
    }
    public class Pathfinder
    {
        public Objects.Item[][] AImap = new Objects.Item[10][];
        public ArrayList XcorList = new ArrayList();
        public ArrayList YcorList = new ArrayList();
        public bool locke = false;
        public int playerXcor ;
        public int playerYcor;
        public String massage = null;
        
        public Pathfinder() {
            for (int i = 0; i < AImap.Length; i++)
            {
                AImap[i] = new Objects.Item[10];
            }
        }
        public void set()
        {
            Console.Title = "A* Pathfinding";
            locke = true;
            String []mapcomponents=new String[10];
            // draw map
            
            for (int i = 0; i < 10; i++) {
                String a="|";
                for (int j = 0; j < 10; j++) {     
                var code=' ';
                if(AImap[j][i].type == 1 ){
                    code='b';
                }
                else if (AImap[j][i].type == 2)
                {
                    code = 'S';
                }
                else if (AImap[j][i].type == 3)
                {
                    code = 'W';
                }
                else
                {
                    code = ' ';
                }
                    a=a+code;
               }
             a=a+"|";
             mapcomponents[i]=a;
            }
           
            String[] map = new String[]
            {
                "+----------+",
                mapcomponents[0],
                mapcomponents[1],
                mapcomponents[2],
                mapcomponents[3],
                mapcomponents[4],
                mapcomponents[5],
                mapcomponents[6],
                mapcomponents[7],
                mapcomponents[8],
                mapcomponents[9],
                 "+----------+"
            };
           
                // for()
                foreach (var line in map)
                    Console.WriteLine(line);
            

            // algorithm

            Location current = null;
            var start = new Location { X = playerXcor, Y = playerYcor };
            var target = new Location { X = 5, Y = 4};
            var openList = new List<Location>();
            var closedList = new List<Location>();
            int g = 0;

            // start by adding the original position to the open list
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // add the current square to the closed list
                closedList.Add(current);

                // show current square on the map
               // Console.SetCursorPosition(current.X, current.Y);
                //Console.Write('.');
                //Console.SetCursorPosition(current.X, current.Y);
                //System.Threading.Thread.Sleep(100);

                // remove it from the open list
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
                g++;

                foreach(var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) != null)
                        continue;

                    // if it's not in the open list...
                    if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) == null)
                    {
                        // compute its score, set the parent
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;

                        // and add it to the open list
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            // assume path was found; let's show it
            while (current != null)
            {
                //Console.SetCursorPosition(current.X, current.Y);
                XcorList.Add(current.X);
                YcorList.Add(current.Y);
                //Console.Write('_');
               // Console.SetCursorPosition(current.X, current.Y);
                current = current.Parent;
               // System.Threading.Thread.Sleep(1000);
            }
             int[] Xcor = new int[XcorList.Count];
             int[] Ycor = new int[YcorList.Count];
            
            int p = 0;
            int q = 0;
            foreach (Object i in XcorList)
            {
                Xcor[p] = (int)i;
                p++;
            }
           
            foreach (Object i in YcorList)
            {
                Ycor[q] = (int)i;
                q++;
                
            }
            massage = "RIGHT#";
           // Console.Write(massage);
            Thread.Sleep(2000);
            
            for (int i = Xcor.Length-2; i >=0; i--)
            {
                Thread.Sleep(2000);
                if (playerXcor == Xcor[i] && playerYcor == (Ycor[i] - 1)) { massage = "RIGHT#"; }
                else if (playerXcor == Xcor[i] && playerYcor == (Ycor[i] + 1)) { massage = "LEFT#"; }
                else if (playerXcor == (Xcor[i] + 1)) { massage = "UP#"; }
                else if (playerXcor == (Xcor[i] - 1)) { massage = "DOWN#"; }
                Console.Write(massage);
                Thread.Sleep(1000);
                Console.Write("(" + Xcor[i] + "," + Ycor[i] + ")");
                playerXcor = Xcor[i];
                playerYcor = Ycor[i];
            }
            //locke = false;
            // end

           // Console.ReadLine();
        }

        static List<Location> GetWalkableAdjacentSquares(int x, int y, string[] map)
        {
            var proposedLocations = new List<Location>()
            {
                new Location { X = x, Y = y - 1 },
                new Location { X = x, Y = y + 1 },
                new Location { X = x - 1, Y = y },
                new Location { X = x + 1, Y = y },
            };

            return proposedLocations.Where(l => map[l.Y][l.X] == ' ' || map[l.Y][l.X] == 'B').ToList();
        }

        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}
