using System;

namespace SnakeGame
{
    class FoodCreator
    {
        int mapWidht;
        int mapHeight;
        char sym;

        Random random = new Random();
        public FoodCreator(int mapWidth, int mapHeight, char sym)
        {
            this.mapWidht = mapWidth;
            this.mapHeight = mapHeight;
            this.sym = sym;
        }

        internal Point CreateFood(Snake snake)
        {
            int x;
            int y;
            x = this.random.Next(2, mapWidht - 2);
            y = this.random.Next(2, mapHeight - 2);
            var food = new Point(x, y, '$');
            Console.ForegroundColor = ConsoleColor.Green;
            food.Draw();
            return food;
        }
    }
}
