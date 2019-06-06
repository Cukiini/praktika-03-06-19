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
using System.Windows.Threading;


namespace SnakeGame
{

    public partial class MainWindow : Window
    {
        const double CellSize = 30D;
        const int CellCount = 16;

        DispatcherTimer timer;
        Random rnd = new Random();
        GameStatus gameStatus;
        SnakeParts snakeParts = new SnakeParts();
        

        Direction snakeDirection;
        int foodRow;
        int foodCol;
        List<UIElement> snakePart = new List<UIElement>();
        int score;

        public MainWindow()
        {
            InitializeComponent();
            DrawBoardBackground();
            InitSnake();
            InitFood();
            ChangeScore(0);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Tick += Timer_Tick;
            timer.Start();

            ChangeGameStatus(GameStatus.Ongoing);
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
                    SetShape(r, row, col);
                    board.Children.Add(r);

                    color = color == color1 ? color2 : color1;
                }
            }
        }

        private void ChangeGameStatus(GameStatus newGameStatus)
        {
            gameStatus = newGameStatus;
            lblGameStatus.Content =
                $"Status: {gameStatus}";
        } 
         
        private void InitSnake()
        {
            snakeShape.Height = CellSize;
            snakeShape.Width = CellSize;
            int index = CellCount / 2;
            snakeParts.Row = index;
            snakeParts.Col = index;
            SetShape(snakeShape, snakeParts.Row, snakeParts.Col);

            ChangeSnakeDirection(Direction.Up);
        }

        private void ChangeSnakeDirection(Direction direction)
        {
            snakeDirection = direction;
            lblSnakeDirection.Content =
                $"Direction: {direction}";
        }

        private void MoveSnake()
        {
            switch (snakeDirection)
            {
                case Direction.Up:
                    snakeParts.Row--;
                    break;
                case Direction.Down:
                    snakeParts.Row++;
                    break;
                case Direction.Left:
                    snakeParts.Col--;
                    break;
                case Direction.Right:
                    snakeParts.Col++;
                    break;
            }
            bool outOfBoundaries =
                (snakeParts.Row < 0 || snakeParts.Row >= CellCount ||
                snakeParts.Col < 0 || snakeParts.Col >= CellCount);

            if (outOfBoundaries)
            {
                ChangeGameStatus(GameStatus.GameOver);
                return;
            }
            
            bool food =
            snakeParts.Row == foodRow &&
            snakeParts.Col == foodCol;
            if (food)
            {
                ChangeScore(score + 1);
                InitFood();
                SnakeBodyChanges();
            }


            SetShape(snakeShape, snakeParts.Row, snakeParts.Col);
        }          

        private void SetShape(
            Shape shape, int row, int col)
        {
            double top = row * CellSize;
            double left = col * CellSize;

            Canvas.SetTop(shape, top);
            Canvas.SetLeft(shape, left);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (gameStatus != GameStatus.Ongoing)
            {
                return;
            }

            MoveSnake();
        }

        private void SnakeBodyChanges()
        {
            double X = board.Width / CellCount;
            double snakeCurrTop = Canvas.GetTop(board);
            double snakeCurrLeft = Canvas.GetLeft(board);

            Rectangle bodyPart = new Rectangle();
            bodyPart.Width = X;
            bodyPart.Height = X;
            bodyPart.Fill = Brushes.DodgerBlue;
            bodyPart.RadiusX = CellSize;
            bodyPart.RadiusY = CellSize;
            Panel.SetZIndex(bodyPart, 9);
            Canvas.SetTop(bodyPart, snakeCurrTop);
            Canvas.SetLeft(bodyPart, snakeCurrLeft);
            board.Children.Add(bodyPart);

            snakePart.Add(bodyPart);

            foreach (UIElement body in snakePart)
            {
                body.Visibility = Visibility.Collapsed;
            }

            int lastBodyPart = snakePart.Count - 1;
            for (int i = 0; i <= score; i++)
            {
                snakePart[lastBodyPart].Visibility = Visibility.Visible;
                lastBodyPart--;
            }
            snakePart[snakePart.Count - 1].Visibility = Visibility.Collapsed;

        }

        private void Window_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (gameStatus != GameStatus.Ongoing)
            {
                return;
            }

            Direction direction;
            switch (e.Key)
            {
                case Key.W:
                    direction = Direction.Up;
                    break;
                case Key.S:
                    direction = Direction.Down;
                    break;
                case Key.A:
                    direction = Direction.Left;
                    break;
                case Key.D:
                    direction = Direction.Right;
                    break;
                default:
                    return;
            }
            ChangeSnakeDirection(direction);
        }

        private void ChangeScore(int newPoints)
        {
            score = newPoints;
            lblScore.Content =
                $"Points: {score}";
        }

        private void InitFood()
        {
            foodShape.Height = CellSize;
            foodShape.Width = CellSize;

            foodRow = rnd.Next(0, CellCount);
            foodCol = rnd.Next(0, CellCount);
            SetShape(foodShape, foodRow, foodCol);
        }        

    }
}

