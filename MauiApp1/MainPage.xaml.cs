using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;

namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        private bool isPlayerX = true; // true: X, false: O
        private string[,] board = new string[3, 3];

        public MainPage()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = string.Empty;
                }
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            // Check if the button is already clicked or if the game is over
            if (string.IsNullOrEmpty(button.Text))
            {
                button.Text = isPlayerX ? "X" : "O";
                board[Grid.GetRow(button), Grid.GetColumn(button)] = button.Text;

                if (CheckForWinner())
                {
                    lblResult.Text = $"Player {(isPlayerX ? "X" : "O")} wins!";
                    DisableButtons();
                }
                else if (IsBoardFull())
                {
                    lblResult.Text = "It's a draw!";
                }
                else
                {
                    isPlayerX = !isPlayerX;
                    lblResult.Text = $"Player {(isPlayerX ? "X" : "O")}'s turn";
                }
            }
        }

        private bool CheckForWinner()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != string.Empty && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }
            }

            // Check columns
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] != string.Empty && board[0, j] == board[1, j] && board[1, j] == board[2, j])
                {
                    return true;
                }
            }

            // Check diagonals
            if (board[0, 0] != string.Empty && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }

            if (board[0, 2] != string.Empty && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }

            return false;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == string.Empty)
                    {
                        return false; // There is an empty space, board is not full
                    }
                }
            }

            return true; // No empty spaces, board is full
        }

        private void DisableButtons()
        {
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = false; // Disable all buttons
                }
            }
        }

    }
}
