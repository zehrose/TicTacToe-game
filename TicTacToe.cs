using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class TicTacToe
    {
        private char[,] board; // Oyun tahtası
        private char currentPlayer; // Şu anki oyuncu ('X' veya 'O')

        public TicTacToe()
        {
            board = new char[3, 3]; // 3x3'lük oyun tahtası
            InitializeBoard();
            currentPlayer = 'X'; // İlk oyuncu başlar
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = '.'; // Boş kareleri '.' ile başlat
        }

        public void PlayGame()
        {
            bool gameEnded = false;

            while (!gameEnded)
            {
                PrintBoard();
                Console.WriteLine($"\nOyuncu {currentPlayer}'in sırası. Hamleni yap (satır ve sütun):");

                int row, col;
                while (true)
                {
                    (row, col) = GetPlayerMove();
                    if (MakeMove(row, col))
                        break;
                    Console.WriteLine("Bu kare dolu! Lütfen başka bir kare seçin.");
                }

                if (CheckWin())
                {
                    PrintBoard();
                    Console.WriteLine($"\nTebrikler! Oyuncu {currentPlayer} kazandı!");
                    gameEnded = true;
                }
                else if (CheckDraw())
                {
                    PrintBoard();
                    Console.WriteLine("\nOyun berabere!");
                    gameEnded = true;
                }
                else
                {
                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X'; // Sırayı değiştir
                }
            }
        }

        private (int, int) GetPlayerMove()
        {
            while (true)
            {
                Console.Write("Satır (0-2): ");
                string? inputRow = Console.ReadLine();
                Console.Write("Sütun (0-2): ");
                string? inputCol = Console.ReadLine();

                if (int.TryParse(inputRow, out int row) && int.TryParse(inputCol, out int col) &&
                    row >= 0 && row < 3 && col >= 0 && col < 3)
                {
                    return (row, col);
                }

                Console.WriteLine("Geçersiz giriş! Lütfen 0-2 arasında bir sayı girin.");
            }
        }

        private bool MakeMove(int row, int col)
        {
            if (board[row, col] == '.')
            {
                board[row, col] = currentPlayer;
                return true;
            }
            return false;
        }

        private bool CheckWin()
        {
            // Satırları, sütunları ve çaprazları kontrol et
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    return true;
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                    return true;
            }
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            return false;
        }

        private bool CheckDraw()
        {
            foreach (var cell in board)
            {
                if (cell == '.')
                    return false;
            }
            return true;
        }

        private void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("   0   1   2");
            Console.WriteLine("  -----------");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i} |");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($" {board[i, j]} |");
                }
                Console.WriteLine("\n  -----------");
            }
        }
    }
}
