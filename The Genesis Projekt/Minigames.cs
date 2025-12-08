using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Genesis_Projekt
{
	internal class Minigames
	{
		public static class TicTacToe
		{
			// Spielfeld: 0–8
			private static char[] board;
			private static char player = 'X';
			private static char ai = 'O';

			public static bool Play()
			{
				InitializeBoard();

				while (true)
				{
					Console.Clear();
					DrawBoard();

					// Spielerzug
					Console.Write("\nPosition wählen (1–9): ");
					string input = Console.ReadLine();

					if (!int.TryParse(input, out int pos) || pos < 1 || pos > 9)
						continue;

					pos -= 1; // Board Index

					if (board[pos] != ' ')
						continue;

					board[pos] = player;

					if (CheckWin(player))
					{
						Console.Clear();
						DrawBoard();
						Console.WriteLine("\n✓ Test bestanden – Mustererkennung erfolgreich.");
						Console.ReadKey();
						return true;
					}

					if (IsBoardFull())
					{
						Console.Clear();
						DrawBoard();
						Console.WriteLine("\n✗ Unentschieden – Test nicht bestanden.");
						Console.ReadKey();
						return false;
					}

					// KI / einfache Logik
					MakeAIMove();

					if (CheckWin(ai))
					{
						Console.Clear();
						DrawBoard();
						Console.WriteLine("\n✗ KI hat gewonnen – Test fehlgeschlagen.");
						Console.ReadKey();
						return false;
					}

					if (IsBoardFull())
					{
						Console.Clear();
						DrawBoard();
						Console.WriteLine("\n✗ Unentschieden – Test nicht bestanden.");
						Console.ReadKey();
						return false;
					}
				}
			}

			private static void InitializeBoard()
			{
				board = new char[9];
				for (int i = 0; i < 9; i++)
					board[i] = ' ';
			}

			private static void DrawBoard()
			{
				Console.WriteLine("=== TIC TAC TOE – PRIMÄRTEST ===\n");
				Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
				Console.WriteLine("---+---+---");
				Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
				Console.WriteLine("---+---+---");
				Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
			}

			private static void MakeAIMove()
			{
				Random rng = new Random();

				while (true)
				{
					int pos = rng.Next(0, 9);
					if (board[pos] == ' ')
					{
						board[pos] = ai;
						break;
					}
				}
			}

			private static bool CheckWin(char p)
			{
				int[,] wins =
				{
			{0,1,2}, {3,4,5}, {6,7,8},
			{0,3,6}, {1,4,7}, {2,5,8},
			{0,4,8}, {2,4,6}
		};

				for (int i = 0; i < wins.GetLength(0); i++)
				{
					if (board[wins[i, 0]] == p &&
						board[wins[i, 1]] == p &&
						board[wins[i, 2]] == p)
						return true;
				}

				return false;
			}

			private static bool IsBoardFull()
			{
				foreach (char c in board)
					if (c == ' ')
						return false;
				return true;
			}
		}
	}
}
