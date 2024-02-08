using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KrestikiNoliki
{
    public partial class MainWindow : Window
    {
        private enum Player { X, O };
        private Player currentPlayer;
        private List<Button> cells;
        private Random random;
        private bool isGameOver;

        public MainWindow()
        {
            InitializeComponent();
            currentPlayer = Player.X;
            cells = new List<Button> { Knopka1, Knopka2, Knopka3, Knopka4, Knopka5, Knopka6, Knopka7, Knopka8, Knopka9 };
            random = new Random();
            isGameOver = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button cell = (Button)sender;
            if (!isGameOver && cell.Content == "")
            {
                cell.Content = currentPlayer;
                cell.IsEnabled = false;
                CheckGameStatus();
                if (!isGameOver)
                {
                    currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
                    MakeRobotMove();
                }
            }
        }

        private void MakeRobotMove()
        {
            List<Button> availableCells = new List<Button>();
            foreach (Button cell in cells)
            {
                if (cell.Content == null)
                {
                    availableCells.Add(cell);
                }
            }

            if (availableCells.Count > 0)
            {

                Button randomCell = availableCells[random.Next(availableCells.Count)];
                randomCell.Content = currentPlayer.ToString();
                randomCell.IsEnabled = false;
                CheckGameStatus();
                currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
            }
        }

        private void CheckGameStatus()
        {
            for (int i = 0; i < 3; i++)
            {
                if (CheckRow(i))
                {
                    EndGame(currentPlayer.ToString() + " Харош чел!");
                    return;
                }
            }


            for (int i = 0; i < 3; i++)
            {
                if (CheckColumn(i))
                {
                    EndGame(currentPlayer.ToString() + " Ну лол поздравляю!");
                    return;
                }
            }

            if (CheckDiagonal() || CheckAntiDiagonal())
            {
                EndGame(currentPlayer.ToString() + " Хз выиграл!");
                return;
            }

            if (CheckTie())
            {
                EndGame("Вы сосете бибу");
            }
        }

        private bool CheckRow(int row)
        {
            int startIndex = row * 3;
            return (!string.IsNullOrEmpty(cells[startIndex].Content.ToString()) &&
                    cells[startIndex].Content.ToString() == cells[startIndex + 1].Content.ToString() &&
                    cells[startIndex + 1].Content.ToString() == cells[startIndex + 2].Content.ToString());
        }

        private bool CheckColumn(int column)
        {
            return (!string.IsNullOrEmpty(cells[column].Content.ToString()) &&
                    cells[column].Content.ToString() == cells[column + 3].Content.ToString() &&
                    cells[column + 3].Content.ToString() == cells[column + 6].Content.ToString());
        }

        private bool CheckDiagonal()
        {
            return (!string.IsNullOrEmpty(cells[0].Content.ToString()) &&
                    cells[0].Content.ToString() == cells[4].Content.ToString() &&
                    cells[4].Content.ToString() == cells[8].Content.ToString());
        }

        private bool CheckAntiDiagonal()
        {
            return (!string.IsNullOrEmpty(cells[2].Content.ToString()) &&
                    cells[2].Content.ToString() == cells[4].Content.ToString() &&
                    cells[4].Content.ToString() == cells[6].Content.ToString());
        }

        private bool CheckTie()
        {
            foreach (Button cell in cells)
            {
                if (string.IsNullOrEmpty(cell.Content.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
            private void EndGame(string result)
            {
                
            }

            private void NewGame_Click(object sender, RoutedEventArgs e)
            {
                currentPlayer = Player.X;
                foreach (Button cell in cells)
                {
                    cell.Content = null;
                    cell.IsEnabled = true;
                }
                
            }
        }
    }
