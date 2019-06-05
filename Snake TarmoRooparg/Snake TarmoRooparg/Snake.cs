using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SnakeGame
{
    class Snake
    {
        Shape snakeShape;
        double cellSize;
        int cellCount;

        Direction direction;

        public Snake(
            Shape snakeShape,
            double cellSize,
            int cellCount)
        {
            this.snakeShape = snakeShape;
            this.cellSize = cellSize;
            this.cellCount = cellCount;
        }

        public void Init()
        {
            snakeShape.Height = cellSize;
            snakeShape.Width = cellSize;
            double coord = cellCount * cellSize / 2;
            Canvas.SetTop(snakeShape, coord);
            Canvas.SetLeft(snakeShape, coord);

            ChangeDirection(Direction.up);
        }

        public void ChangeDirection(Direction newDirection)
        {
            direction = newDirection;
        }

        private bool Border(double x)
        {

            if (x > 479 || x < 0)
            {
                if (MessageBox.Show("Game Over!\n\nAgain?","",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    snakeShape.Height = cellSize;
                    snakeShape.Width = cellSize;
                    double coord = cellCount * cellSize / 2;
                    Canvas.SetTop(snakeShape, coord);
                    Canvas.SetLeft(snakeShape, coord);

                    ChangeDirection(Direction.up);
                }
                else
                {
                    Application.Current.Shutdown();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Move()
        {
            if (direction == Direction.up ||
               direction == Direction.down)
            {
                double currentTop = Canvas.GetTop(snakeShape);
                double newTop = direction == Direction.up
                    ? currentTop - cellSize
                    : currentTop + cellSize;
                Canvas.SetTop(snakeShape, newTop);

                if (Border(newTop) == true)
                    Canvas.SetTop(snakeShape, newTop);
            }

            if (direction == Direction.left ||
                direction == Direction.right)
            {
                double currentLeft = Canvas.GetLeft(snakeShape);
                double newLeft = direction == Direction.left
                    ? currentLeft - cellSize
                    : currentLeft + cellSize;
                Canvas.SetLeft(snakeShape, newLeft);

                if (Border(newLeft) == true)
                    Canvas.SetLeft(snakeShape, newLeft);
            }
        }
    }
}
