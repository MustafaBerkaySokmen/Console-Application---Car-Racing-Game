using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        const int ROAD_WIDTH = 4;
        const int ROAD_HEIGHT = 10;
        const int CAR_WIDTH = 3;
        const int CAR_HEIGHT = 2;
        const int INITIAL_SPEED = 50;
        const int MIN_SPEED = 25;

        int carX = ROAD_WIDTH / 2;
        int carY = ROAD_HEIGHT - 3;

        List<int> obstacleX = new List<int>();
        List<int> obstacleY = new List<int>();

        bool gameRunning = true;
        int score = 0;
        Random random = new Random();
        int speed = INITIAL_SPEED;

        while (gameRunning)
        {
            Console.Clear();

            // Draw the road
            for (int y = 0; y < ROAD_HEIGHT; y++)
            {
                for (int x = 0; x < ROAD_WIDTH; x++)
                {
                    if (y == carY && x == carX)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("###");
                    }
                    else if (obstacleX.Contains(x) && obstacleY[obstacleX.IndexOf(x)] == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("# #");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("---");
                    }
                }
                Console.WriteLine();
            }

            // Move the car left or right
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow && carX > 0)
                {
                    carX--;
                }
                else if (key.Key == ConsoleKey.RightArrow && carX < ROAD_WIDTH - 1)
                {
                    carX++;
                }
            }

            // Add new obstacles
            if (random.Next(0, 10) < 3)
            {
                obstacleX.Add(random.Next(0, ROAD_WIDTH));
                obstacleY.Add(0);
            }

            // Move the obstacles down and detect collisions
            for (int i = 0; i < obstacleX.Count; i++)
            {
                obstacleY[i]++;
                if (obstacleY[i] == carY && obstacleX[i] == carX)
                {
                    Console.Clear();
                    Console.WriteLine("Çarptın! Devam etmek için 'T'ye basın.");
                    gameRunning = false;
                    break;
                }
            }

            // Remove obstacles that have gone off the screen
            for (int i = 0; i < obstacleX.Count; i++)
            {
                if (obstacleY[i] == ROAD_HEIGHT)
                {
                    obstacleX.RemoveAt(i);
                    obstacleY.RemoveAt(i);
                    score++;
                    i--;
                }
            }

            // Show the score
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, ROAD_HEIGHT + 1);
            Console.WriteLine("Skor: {0}", score);

            // Pause for a moment to slow down the road
            System.Threading.Thread.Sleep(speed);

            // Decrease the speed of the road over time
            if (speed > MIN_SPEED)
            {
                speed--;
            }
        }

        // Prompt the user to play again
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.T)
            {
                carX = ROAD_WIDTH / 2;
                carY = ROAD_HEIGHT - 3;
                obstacleX.Clear();
                obstacleY.Clear();
                gameRunning = true;
                score = 0;
                speed = INITIAL_SPEED;
                break;
            }
        }
    }
}
