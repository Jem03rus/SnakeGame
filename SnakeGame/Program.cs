using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        private const int Width = 100;
        private const int Height = 32;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Start();
            }
        }

        private static void Start()
        {
            Walls walls = new Walls(Width, Height);
            walls.Draw();

            Point p = new Point(4, 5, '@');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(Width, Height, '$');
            Point food = foodCreator.CreateFood(snake);
            food.Draw();
            int[,] s = new int[Width, Height];

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    do
                    {
                        food = foodCreator.CreateFood(snake);

                    } while (IsColision(snake, food));

                    food.Draw();
                }
                else
                {
                    for (int i = 0; i < Width; i++)
                    {
                        for (int j = 0; j < Height; j++)
                        {
                            s[i, j] = (int)Figures.EmptySpace;
                        }
                    }

                    foreach (var item in walls.Points)
                    {
                        s[item.x, item.y] = (int)Figures.Barrier;
                    }

                    s[food.x, food.y] = (int)Figures.Destination;

                    foreach (var item in snake.Points)
                    {
                        var first = snake.Points.Last();
                        if (item == first)
                        {
                            s[item.x, item.y] = (int)Figures.StartPosition;
                        }
                        else
                        {
                            s[item.x, item.y] = (int)Figures.Barrier;
                        }
                    }
                    var li = new LeeAlgorithm(s);
                    System.Drawing.Point head = new System.Drawing.Point(0, 0);
                    System.Drawing.Point nextstep;
                    if (!li.PathFound)
                    {
                        food = foodCreator.CreateFood(snake);
                    }
                    else
                    {
                        foreach (var item in li.Path)
                        {
                            if (item == li.Path.Last())
                            {
                                s[item.Item1, item.Item2] = (int)Figures.StartPosition;
                                head = new System.Drawing.Point(item.Item1, item.Item2);
                            }
                            else if (item == li.Path.First())
                                s[item.Item1, item.Item2] = (int)Figures.Destination;
                            else
                                s[item.Item1, item.Item2] = (int)Figures.Path;

                        }

                        nextstep = new System.Drawing.Point(li.Path[li.Path.Count - 2].Item1, li.Path[li.Path.Count - 2].Item2);




                        var dir = GetDirection(head, nextstep);

                        switch (dir)
                        {
                            case Direction.LEFT:
                                snake.HandleKey(ConsoleKey.LeftArrow);
                                break;
                            case Direction.RIGHT:
                                snake.HandleKey(ConsoleKey.RightArrow);
                                break;
                            case Direction.UP:
                                snake.HandleKey(ConsoleKey.UpArrow);
                                break;
                            case Direction.DOWN:
                                snake.HandleKey(ConsoleKey.DownArrow);
                                break;
                        }
                    }
                    int speed = 1;
                    snake.Move();
                    Thread.Sleep(speed);

                }

            }
            WriteGameOver();
            Console.ReadLine();
        }

        private static bool IsColision(Snake snake, Point food)
        {
            foreach (var item in snake.Points)
            {
                if ((item.x == food.x && item.y == food.y) || (item.x + 1 == food.x && item.y == food.y) || (item.x - 1 == food.x && item.y == food.y) || (item.x == food.x && item.y - 1 == food.y) || (item.x == food.x && item.y + 1 == food.y))
                {
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        private static Direction GetDirection(System.Drawing.Point head, System.Drawing.Point nextstep)
        {
            if (head.X == 0 && head.Y == 0)
            {
                throw new ArgumentException("неверная коорд");
            }
            Direction dir;
            if ((nextstep.X > head.X) && (nextstep.Y == head.Y))
            {
                dir = Direction.RIGHT;
            }
            else if ((nextstep.X < head.X) && (nextstep.Y == head.Y))
            {
                dir = Direction.LEFT;
            }
            else if ((nextstep.Y > head.Y) && (nextstep.X == head.X))
            {
                dir = Direction.DOWN;
            }
            else if ((nextstep.Y < head.Y) && (nextstep.X == head.X))
            {
                dir = Direction.UP;
            }
            else
            {
                throw new AggregateException();
            }

            return dir;
        }




        static void WriteGameOver()
        {
            int xOffset = Height;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.WriteLine("GAME OVER");
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
        private static void Print(int[,] array)
        {
            Console.WriteLine("");
            string msg = string.Empty;
            int x = array.GetLength(0);
            int y = array.GetLength(1);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    switch (array[i, j])
                    {
                        case (int)Figures.Path: msg = string.Format("{0,3}", "+"); Console.ForegroundColor = ConsoleColor.Yellow; break;
                        case (int)Figures.StartPosition: msg = string.Format("{0,3}", "s"); Console.ForegroundColor = ConsoleColor.Green; break;
                        case (int)Figures.Destination: msg = string.Format("{0,3}", "d"); Console.ForegroundColor = ConsoleColor.Red; break;
                        case (int)Figures.EmptySpace: msg = string.Format("{0,3}", "'"); Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                        case (int)Figures.Barrier: msg = string.Format("{0,3}", "*"); Console.ForegroundColor = ConsoleColor.Blue; break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                            msg = string.Format("{0,3}", array[i, j]); Console.ForegroundColor = ConsoleColor.DarkGray; break;
                        default:
                            break;
                    }
                    Console.Write(msg);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine(msg);
        }
    }
}
