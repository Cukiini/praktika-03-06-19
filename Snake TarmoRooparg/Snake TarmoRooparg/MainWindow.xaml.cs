using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake_TarmoRooparg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const double CellSize = 30D;
        const int CellCount = 16;

        public MainWindow()
        {
            InitializeComponent();
            DrawBoardBackground();
            InitSnake();
        }

        private void InitSnake()
        {
            snake.Height = CellSize;
            snake.Width = CellSize;
            double coord = CellCount * CellSize / 2;
            Canvas.SetTop(snake, coord);
            Canvas.SetLeft(snake, coord);
        }

        private void MoveSnake(Direction direction)
        {
            if (direction == Direction.up ||
                direction == Direction.down)
            {
                double currentTop = Canvas.GetTop(snake);
                double newTop = direction == Direction.up
                    ? currentTop - CellSize
                    : currentTop + CellSize;
                Canvas.SetTop(snake, newTop);
            }

            if (direction == Direction.left ||
                direction == Direction.right)
            {
                double currentLeft = Canvas.GetLeft(snake);
                double newLeft = direction == Direction.left
                    ? currentLeft - CellSize
                    : currentLeft + CellSize;
                Canvas.SetLeft(snake, newLeft);
            }

        }
        private void DrawBoardBackground()
        {
            SolidColorBrush color1 = Brushes.LightGreen;
            SolidColorBrush color2 = Brushes.LimeGreen;

            for (int row = 0; row < CellCount; row++)
            {
                SolidColorBrush color =
                    row % 2 == 0 ? color1 : color2;
                for (int col = 0; col < CellCount; col++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = CellSize;
                    r.Height = CellSize;
                    r.Fill = color;
                    Canvas.SetTop(r, row * CellSize);
                    Canvas.SetLeft(r, col * CellSize);
                    board.Children.Add(r);

                    color = color == color1 ? color2 : color1;
                }
            }
        }
        private void Window_KeyDown(
            object sender, KeyEventArgs e)
        {
            Direction direction;
            if(e.Key == Key.W)
            {
                direction = Direction.up;
            }
            else if (e.Key == Key.S)
            {
                direction = Direction.down;
            }
            else if (e.Key == Key.D)
            {
                direction = Direction.right;
            }
            else if (e.Key == Key.A)
            {
                direction = Direction.left;
            }
            else
            {
                return;
            }
            

            MoveSnake(direction);
        }

        public enum Direction
        {
            up,
            down,
            left,
            right
        }
    }
}
