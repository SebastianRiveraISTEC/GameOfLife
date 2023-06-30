using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using GameOfLife.view;

namespace GameOfLife.Model
{
    public class Game : UIElement
    {
        public Game()
        {
            lowerLimit = 2;
            upperLimit = 3;
            reproduction = 3;
            maintain1 = 2;
            maintain2 = 3;
            started = false;
            paused = false;
            created = false;
            ticks = 0;
        }

        public Game(int _lowerLimit, int _upperLimit, int _reproduction, int _maintain1, int _maintain2)
        {
            lowerLimit = _lowerLimit;
            upperLimit = _upperLimit;
            reproduction = _reproduction;
            maintain1 = _maintain1;
            maintain2 = _maintain2;
            started = false;
            paused = false;
            created = false;
            ticks = 0;
            createEnabled = true;
            startEnabled = false;
            pauseEnabled = false;
            resetEnabled = false;
            stepEnabled = false;
        }


        public DispatcherTimer _timer;




        //properties
        public static readonly DependencyProperty lowerLimitProperty =
            DependencyProperty.Register("lowerLimit", typeof(int), typeof(Game), new PropertyMetadata(2), new ValidateValueCallback(validateValueCallback));

        public static readonly DependencyProperty upperLimitProperty =
            DependencyProperty.Register("upperLimit", typeof(int), typeof(Game), new PropertyMetadata(3), new ValidateValueCallback(validateValueCallback));

        public static readonly DependencyProperty reproductionProperty =
            DependencyProperty.Register("reproduction", typeof(int), typeof(Game), new PropertyMetadata(3), new ValidateValueCallback(validateValueCallback));

        public static readonly DependencyProperty maintain1Property =
            DependencyProperty.Register("maintain1", typeof(int), typeof(Game), new PropertyMetadata(2), new ValidateValueCallback(validateValueCallback));

        public static readonly DependencyProperty maintain2Property =
            DependencyProperty.Register("maintain2", typeof(int), typeof(Game), new PropertyMetadata(3), new ValidateValueCallback(validateValueCallback));

        public static readonly DependencyProperty createdProperty =
            DependencyProperty.Register("created", typeof(bool), typeof(Game), new PropertyMetadata(false), new ValidateValueCallback(validateBoolValueCallback));

        public static readonly DependencyProperty startedProperty =
            DependencyProperty.Register("started", typeof(bool), typeof(Game), new PropertyMetadata(false), new ValidateValueCallback(validateBoolValueCallback));

        public static readonly DependencyProperty pausedProperty =
            DependencyProperty.Register("paused", typeof(bool), typeof(Game), new PropertyMetadata(false), new ValidateValueCallback(validateBoolValueCallback));

        public static readonly DependencyProperty ticksProperty =
            DependencyProperty.Register("ticks", typeof(int), typeof(Game), new PropertyMetadata(0), new ValidateValueCallback(validateValueCallback));

        public static readonly DependencyProperty createEnabledProperty =
            DependencyProperty.Register("createEnabled", typeof(bool), typeof(Game), new PropertyMetadata(true), new ValidateValueCallback(validateBoolValueCallback));

        public static readonly DependencyProperty startEnabledProperty =
            DependencyProperty.Register("startEnabled", typeof(bool), typeof(Game), new PropertyMetadata(false), new ValidateValueCallback(validateBoolValueCallback));

        public static readonly DependencyProperty pauseEnabledProperty =
            DependencyProperty.Register("pauseEnabled", typeof(bool), typeof(Game), new PropertyMetadata(false), new ValidateValueCallback(validateBoolValueCallback));

        public static readonly DependencyProperty resetEnabledProperty =
            DependencyProperty.Register("resetEnabled", typeof(bool), typeof(Game), new PropertyMetadata(false), new ValidateValueCallback(validateBoolValueCallback));

        public static readonly DependencyProperty stepEnabledProperty =
            DependencyProperty.Register("stepEnabled", typeof(bool), typeof(Game), new PropertyMetadata(false), new ValidateValueCallback(validateBoolValueCallback));

        //setters and getters
        public int lowerLimit
        {
            get { return (int)GetValue(lowerLimitProperty); }
            set { SetValue(lowerLimitProperty, value); }
        }

        public int upperLimit
        {
            get { return (int)GetValue(upperLimitProperty); }
            set { SetValue(upperLimitProperty, value); }
        }

        public int reproduction
        {
            get { return (int)GetValue(reproductionProperty); }
            set { SetValue(reproductionProperty, value); }
        }

        public int maintain1
        {
            get { return (int)GetValue(maintain1Property); }
            set { SetValue(maintain1Property, value); }
        }

        public int maintain2
        {
            get { return (int)GetValue(maintain2Property); }
            set { SetValue(maintain2Property, value); }
        }

        public bool created
        {
            get { return (bool)GetValue(createdProperty); }
            set { SetValue(createdProperty, value); }
        }

        public bool started
        {
            get { return (bool)GetValue(startedProperty); }
            set { SetValue(startedProperty, value); }
        }
        public bool paused
        {
            get { return (bool)GetValue(pausedProperty); }
            set { SetValue(pausedProperty, value); }
        }

        public int ticks
        {
            get { return (int)GetValue(ticksProperty); }
            set { SetValue(ticksProperty, value); }
        }

        public bool createEnabled
        {
            get { return (bool)GetValue(createEnabledProperty); }
            set { SetValue(createEnabledProperty, value); }
        }

        public bool startEnabled
        {
            get { return (bool)GetValue(startEnabledProperty); }
            set { SetValue(startEnabledProperty, value); }
        }

        public bool pauseEnabled
        {
            get { return (bool)GetValue(pauseEnabledProperty); }
            set { SetValue(pauseEnabledProperty, value); }
        }

        public bool resetEnabled
        {
            get { return (bool)GetValue(resetEnabledProperty); }
            set { SetValue(resetEnabledProperty, value); }
        }

        public bool stepEnabled
        {
            get { return (bool)GetValue(stepEnabledProperty); }
            set { SetValue(stepEnabledProperty, value); }
        }

        public static bool validateValueCallback(object value)
        {
            if ((int)value >= 0) return true;
            return false;
        }

        public static bool validateBoolValueCallback(object value)
        {
            if ((bool)value == true || (bool)value == false) return true;
            return false;
        }
    }

    public class GameBoard : Game
    {
        public GameBoard() : base()
        {
            board = new Cell[boardSize, boardSize];
            CreationDelay = 100;
            aliveCells = 0;
        }
        public GameBoard(int _lowerLimit, int _upperLimit, int _reproduction, int _maintain1, int _maintain2) : base(_lowerLimit, _upperLimit, _reproduction, _maintain1, _maintain2)
        {
            board = new Cell[boardSize, boardSize];
            CreationDelay = 100;
            aliveCells = 0;
        }
        //consts
        public static int boardSize = 102;
        public const int cellSize = 9;
        public int CreationDelay;
        //

        public int aliveCells
        {
            get { return (int)GetValue(aliveCellsProperty); }
            set { SetValue(aliveCellsProperty, value); }
        }

        public static readonly DependencyProperty aliveCellsProperty =
    DependencyProperty.Register("aliveCells", typeof(int), typeof(GameBoard), new PropertyMetadata(0), new ValidateValueCallback(validateValueCallback));



        MainWindow mw = (MainWindow)Application.Current.MainWindow;/// <summary>
        /// /////////////////
        /// </summary>

        

        public Cell[,] board;

        public void CreateBoard()
        {
            Trace.WriteLine("Called CreateBoard");
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                Frame frame = mainWindow.FindName("frm") as Frame;
                if (frame != null && frame.Content is Page mainGamePage)
                {
                    Canvas canvasBoard = mainGamePage.FindName("CanvasBoard") as Canvas;
                    if (canvasBoard != null)
                    {
                        created = true;
                        DisableButtons();
                        //aliveCells = 0;

                        canvasBoard.Children.Clear();
                        for (int i = 0; i < boardSize; i++)
                        {
                            for (int j = 0; j < boardSize; j++)
                            {
                                board[i, j] = new Cell();
                                if (i == 0 || i == boardSize - 1 || j == 0 || j == boardSize - 1)
                                {
                                    Rectangle r = new Rectangle
                                    {
                                        Width = cellSize,
                                        Height = cellSize,
                                        Stroke = Brushes.DarkGray,
                                        StrokeThickness = 0.5,
                                        Fill = Brushes.DarkGray
                                    };

                                    board[i, j].Shape = r;
                                    board[i, j].isBorder = true;
                                    Canvas.SetLeft(r, j * cellSize);
                                    Canvas.SetTop(r, i * cellSize);
                                    canvasBoard.Children.Add(r);
                                }
                                else
                                {
                                    board[i, j].State = false;
                                    int[] aux = { j, i };
                                    Rectangle r = new Rectangle
                                    {
                                        Width = cellSize,
                                        Height = cellSize,
                                        Stroke = Brushes.Black,
                                        StrokeThickness = 0.5,
                                        Fill = Brushes.Black,
                                        Tag = aux,
                                    };
                                    r.MouseDown += LeftMouseDown;
                                    board[i, j].Shape = r;
                                    Canvas.SetLeft(r, j * cellSize);
                                    Canvas.SetTop(r, i * cellSize);
                                    canvasBoard.Children.Add(r);
                                }
                            }
                        }
                    }
                }
                else { Trace.WriteLine("Frame was null"); }
            }
            else { Trace.WriteLine("Windows was null"); }
                        
        }

        private void LeftMouseDown(object sender, MouseEventArgs e)
        {
            if (!started)
            {
                int x = ((int[])((Rectangle)sender).Tag)[0];
                int y = ((int[])((Rectangle)sender).Tag)[1];
                var cell = board[y, x];
                FlipCell(cell);
            }
        }
        void FlipCell(Cell cell)
        {
            if (cell.State == true)
            {
                cell.State = false;
                aliveCells--;
            }
            else
            {
                cell.State = true;
                aliveCells++;
            }
            //mw.TextTest.Text = aliveCells.ToString();
            DrawCell(cell);
        }
        void DrawCell(Cell c)
        {
            if (c.State == true)
            {
                c.Shape.Fill = Brushes.White;
            }
            else
            {
                c.Shape.Fill = Brushes.Black;
            }
        }
        void ApplyRules() //update board
        {
            Trace.WriteLine("ApplyRules called");
            List<Cell> CellsToChange = new List<Cell>();

            List<Cell> neighbors;
            for (int i = 1; i < boardSize - 1; i++)
            {
                for (int j = 1; j < boardSize - 1; j++)
                {
                    Cell currentCell = board[i, j];
                    neighbors = GetNeighborList(i, j);
                    int neighborCnt = neighbors.Count(x => x.State == true);

                    //rules
                    if (currentCell.State == true)
                    {
                        if (neighborCnt < lowerLimit)
                        {
                            //flip off
                            currentCell.NextState = false;
                            CellsToChange.Add(currentCell);
                            //aliveCells--;
                        }
                        else if (neighborCnt > upperLimit)
                        {
                            //flip off
                            currentCell.NextState = false;
                            CellsToChange.Add(currentCell);
                            //aliveCells--;
                        }
                        else if (neighborCnt == maintain1 || neighborCnt == maintain2)
                        {
                            //nothing happens (it lives)
                        }
                    }
                    else
                    {
                        if (neighborCnt == reproduction)
                        {
                            //flip on
                            currentCell.NextState = true;
                            CellsToChange.Add(currentCell);
                            //aliveCells++;
                        }
                    }
                    board[i, j] = currentCell;
                }
            }
            foreach (Cell cell in CellsToChange)
            {
                //cell.Update();
                FlipCell(cell);
                DrawCell(cell);
            }
        }
        List<Cell> GetNeighborList(int x, int y)
        {
            List<Cell> ans = new List<Cell>();

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Cell aux = board[x + i, y + j];
                    if (aux.isBorder == false && aux.State == true && Math.Abs(i) + Math.Abs(j) != 0)
                    {
                        ans.Add(aux);
                    }
                }
            }
            return ans;
        }

        public void LoadBoard(Cell[,] newBoard)
        {
            ResetGame();
            Trace.WriteLine("After reset");
            for (int i = 0; i < boardSize - 1; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Cell currentCell = board[i, j];
                    Cell newCell = newBoard[i, j];
                    if(newCell.State == true)
                    {
                        FlipCell(currentCell);
                        //DrawCell(currentCell);
                    }
                    board[i, j] = currentCell;
                }
            }
            Trace.WriteLine("After drawing");
        }

        public void StartGame()
        {
            Trace.WriteLine("StartGame called");
            started = true;
            _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, CreationDelay) };
            _timer.Tick += GameTick;
            _timer.Start();
        }

        private void GameTick(object sender, EventArgs e)
        {
            Trace.WriteLine("GameTick called");
            if (!paused)
            {
                if (aliveCells > 0)
                {
                    ticks++;
                    ApplyRules();
                }
                else
                {
                    Trace.WriteLine("Gametick: No cells left, ending game");
                    _timer.Stop();
                }
            }
        }

        public void PauseGame(bool x)
        {
            Trace.WriteLine("PauseGame called");
            if (x)
            {
                paused = true;
            }
            else
            {
                paused = false;
            }
        }

        public void ResetGame()
        {
            Trace.WriteLine("ResetGame called");
            paused = false;
            started = false;
            if (_timer != null)
            {
                _timer.Stop();
            }
            aliveCells = 0;
            ticks = 0;
            for (int i = 0; i < boardSize - 1; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Cell currentCell = board[i, j];
                    if (currentCell.State == true)
                    {
                        currentCell.State = false;
                        DrawCell(currentCell);
                    }
                    board[i, j] = currentCell;
                }
            }
        }

        public void SingleStep()
        {
            Trace.WriteLine("SingleStep called");
            if (aliveCells > 0)
            {
                ticks++;
                ApplyRules();
            }
        }

        public void DisableButtons()
        {
            //if (!created)
            //{
            //    createEnabled = false;
            //}
            //else
            //{
            //    createEnabled = true;
            //}

            //if(!started && created)
            //{
            //    startEnabled = false;
            //}
            //else
            //{
            //    startEnabled = true;
            //}

            //if(started && !paused && created)
            //{
            //    pauseEnabled = false;
            //}
            //else
            //{
            //    pauseEnabled = true;
            //}

            //if (created)
            //{
            //    resetEnabled = false;
            //}
            //else
            //{
            //    resetEnabled = true;
            //}

            //if(created && started && paused)
            //{
            //    stepEnabled = false;
            //}
            //else
            //{
            //    stepEnabled = true;
            //}
            createEnabled = !created;
            startEnabled = !started && created;
            pauseEnabled = !(started && !paused && created);
            resetEnabled = !created;
            stepEnabled = !(created && started && paused);
        }

        public void btnStartOnClick()
        {
            Trace.WriteLine("btnStartOnClick called");
            if (!started && created)
            {
                StartGame();
            }
            else if (started && paused)
            {
                PauseGame(false);
            }
            DisableButtons();
        }
        public void btnPauseOnClick()
        {
            Trace.WriteLine("btnPauseOnClick called");
            if (!pauseEnabled)
            {
                PauseGame(true);
                DisableButtons();
            }
        }
        public void btnResetOnClick()
        {
            Trace.WriteLine("btnResetOnClick called");
            if (!resetEnabled)
            {
                ResetGame();
                DisableButtons();
            }
        }
        public void btnSingleStep()
        {
            if (!stepEnabled)
            {
                SingleStep();
                DisableButtons();
            }
        }

        public void btnLoadBoard(object parameter)
        {
            Trace.WriteLine("btnLoadBoard called");
            if(parameter != null)
            {
                Cell[,] newBoard = parameter as Cell[,];
                LoadBoard(newBoard);
            }
            else
            {
                Trace.WriteLine("Load Parameter was null :c");
            }
            
        }
    }
}