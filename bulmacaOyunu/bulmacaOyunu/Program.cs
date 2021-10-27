using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bulmacaOyunu
{
    class Program
    {
        private static readonly Random getRandom = new Random();
        public static char[,] Maze = new char[10, 10];
        public static bool[,] mazePointsAvailable = new bool[10, 10];


        public static void printMaze(bool showBombs = false)
        {
            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    if (!showBombs)
                    {
                        if(Maze[i,j] == '2')
                        {
                            Console.Write(1 + " ");
                            continue;
                        }
                    }
                    Console.Write(Maze[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
        public static void placeBombs()
        {
            int row = generateDifferentNumber(new int[0], 0, 9);
            int col = generateDifferentNumber(new int[0], 0, 10);
            while(Maze[row,col] != '1')
            {
                col = generateDifferentNumber(new int[0], 0, 10);
            }
            Maze[row, col] = '2';

        }
        public static void createPath(int startingPoint)
        {
            Maze[9, startingPoint] = '1';
            Maze[8, startingPoint] = '1';
            int verticalIndex = 8, horizontalIndex = startingPoint;

            while(verticalIndex != 0)
            {
                int newDirection = generateDifferentNumber(new int[0], 0,3);
                switch (newDirection)
                {
                    case 0:
                        if(horizontalIndex != 0)
                        {
                            Maze[verticalIndex, --horizontalIndex] = '1';
                        }
                    break;

                    case 1:
          
                        Maze[--verticalIndex, horizontalIndex] = '1';
                        break;

                    case 2:
                        if (horizontalIndex != 9)
                        {
                            Maze[verticalIndex, ++horizontalIndex] = '1';
                        }
                        break;
                }
                    

            }

        }

        public static int generateDifferentNumber(int[] arr, int min, int max)
        {
            int newNumber = getRandom.Next(min, max);
 
            while (arr.Contains(newNumber))
            {
                newNumber = getRandom.Next(min, max);
            }
            


            return newNumber;
        }


        static void Main(string[] args)
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    Maze[i, j] = '0';
                    mazePointsAvailable[i, j] = true;
                }
            }

            //printMaze(Maze);
            int[] marks = new int[3] { -1, -1, -1 };

            for (int i = 0; i < 3; i++)
            {
                marks[i] = generateDifferentNumber(marks, 0, 10);
                createPath(marks[i]);
            }                       
            placeBombs();
            placeBombs();

            char command = '0';
            int points = 0;
            bool showBombs = false;
            int posX = -1;
            int posY = 9;
            bool starting = true;
            int startX = -1;
            bool gameActive = true;
            while (gameActive)
            {
                if (starting)
                {
                    printMaze();
                    int counter = 1;
                    for(int i = 0; i < 10; i++)
                    {
                        if(Maze[9,i] == '1')
                        {
                            marks[counter-1] = i;
                            Console.Write(counter++ + " ");
                        }
                        else { Console.Write("- "); }
                    }
                    Console.Write("\n");
                    command = Console.ReadKey().KeyChar;
                    Console.Write("Basilan tus:" + command);
                    if (command != '1' && command != '2' && command != '3'){
                        continue;
                    }
                    starting = false;
                    startX = marks[(int)Char.GetNumericValue(command)-1];
                    Console.Clear();
                    
                    posX = startX;
                    posY = 9;
                    Maze[posY, posX] = 'K';
                }
                switch (command)
                {
                    case 'w':
                        if(Maze[posY-1,posX] == '0')
                        {
                            Console.WriteLine("Opps!! Burada duvar varmış. Gitti 1 puan :(");
                            points -= 1;
                        } else if(Maze[posY - 1, posX] == '2')
                        {
                            //patladi
                            points = 0;
                            Console.WriteLine("BOOM!!");
                            gameActive = false;
                            showBombs = true;
                        } else
                        {
                            Maze[posY--, posX] = '1';
                            Maze[posY, posX] = 'K';
                            if (mazePointsAvailable[posY, posX])
                            {
                                points++;
                                mazePointsAvailable[posY, posX] = false;

                            }
                            if(posY == 0)
                            {
                                Console.WriteLine("Kazandınız");
                                gameActive = false;
                            }
                        }
                        break;
                    case 'a':

                        if (posX == 0)
                        {
                           //hareket yok
                        }
                        else if (Maze[posY, posX-1] == '0')
                        {
                            Console.WriteLine("Opps!! Burada duvar varmış. Gitti 1 puan :(");
                            points -= 1;
                        }
                        else if (Maze[posY, posX-1] == '2')
                        {
                            //patladi
                            points = 0;
                            Console.WriteLine("BOOM!!");
                            gameActive = false;
                            showBombs = true;
                        }
                        else
                        {
                            Maze[posY, posX--] = '1';
                            Maze[posY, posX] = 'K';
                            if (mazePointsAvailable[posY, posX])
                            {
                                points++;
                                mazePointsAvailable[posY, posX] = false;

                            }
                            if (posY == 9)
                            {
                                if (startX == posX)
                                {
                                    Maze[posY, posX] = '1';
                                    starting = true;
                                    continue;
                                }
                            }

                        }
                        break;
                    case 's':
                        if (posY == 8)
                        {
                            if (startX == posX)
                            {
                                Maze[posY, posX] = '1';
                                starting = true;
                                continue;
                            }
                        }
                        else if (Maze[posY + 1, posX] == '0')
                        {
                            Console.WriteLine("Opps!! Burada duvar varmış. Gitti 1 puan :(");
                            points -= 1;
                        }
                        else if (Maze[posY + 1, posX] == '2')
                        {
                            //patladi
                            points = 0;
                            Console.WriteLine("BOOM!!");
                            gameActive = false;
                            showBombs = true;
                        }
                        else
                        {
                            Maze[posY++, posX] = '1';
                            Maze[posY, posX] = 'K';
                            if (mazePointsAvailable[posY, posX])
                            {
                                points++;
                                mazePointsAvailable[posY, posX] = false;

                            }

                        }
                        break;
                    case 'd':
                        if (posX == 9)
                        {
                            //no move
                        }
                        else if (Maze[posY, posX+1] == '0')
                        {
                            Console.WriteLine("Opps!! Burada duvar varmış. Gitti 1 puan :(");
                            points -= 1;
                        }
                        else if (Maze[posY , posX+1] == '2')
                        {
                            //patladi
                            points = 0;
                            Console.WriteLine("BOOM!!");
                            gameActive = false;
                            showBombs = true;
                        }
                        else
                        {
                            Maze[posY, posX++] = '1';
                            Maze[posY, posX] = 'K';
                            if (mazePointsAvailable[posY, posX])
                            {
                                points++;
                                mazePointsAvailable[posY, posX] = false;

                            }
                            if (posY == 9)
                            {
                                if (startX == posX)
                                {
                                    Maze[posY, posX] = '1';
                                    starting = true;
                                    continue;
                                }
                            }
                        }
                        break;
                    case 'G':
                        showBombs = !showBombs;
                        break;
                }
                Console.WriteLine("Puan : " + points);
                printMaze(showBombs);

                command = Console.ReadKey().KeyChar;

                Console.Clear();

            }

            
        }
    }
}
