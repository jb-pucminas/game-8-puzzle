using System;
using System.Collections.Generic;
public class Puzzle15
{
	int[,] matriz = new int[4, 4];
	List<int> matrizRange = new List<int>();
	int[] zeroPosition = new int[2];
	public Puzzle15()
	{
		Random randomizer = new Random();
		for (int i = 0; i < 16; i++)
		{
			matrizRange.Add(i);
		}
		for (int l = 0; l < matriz.GetLength(0); l++)
		{
			Console.WriteLine();
			for (int c = 0; c < matriz.GetLength(1); c++)
			{
				int number = randomizer.Next(matrizRange.Count);
				matriz[l, c] = matrizRange[number];
				matrizRange.Remove(matrizRange[number]);
			}
		}



		matriz[0, 0] = 1;
		matriz[0, 1] = 2;
		matriz[0, 2] = 3;
		matriz[0, 3] = 4;

		matriz[1, 0] = 5;
		matriz[1, 1] = 6;
		matriz[1, 2] = 7;
		matriz[1, 3] = 8;

		matriz[2, 0] = 9;
		matriz[2, 1] = 10;
		matriz[2, 2] = 11;
		matriz[2, 3] = 12;

		matriz[3, 0] = 13;
		matriz[3, 1] = 14;
		matriz[3, 2] = 0;
		matriz[3, 3] = 15;

		show();
	}
	void show()
	{
		Console.Clear();
		for (int l = 0; l < matriz.GetLength(0); l++)
		{
			Console.WriteLine();
			for (int c = 0; c < matriz.GetLength(1); c++)
			{
				if (matriz[l, c] == 0)
				{
					Console.Write("    |");
				}
				else if (matriz[l, c] < 10)
				{
					Console.Write("  " + matriz[l, c] + " |");
				}
				else
				{
					Console.Write(" " + matriz[l, c] + " |");
				}
			}
		}

		bool isWinner = winVerify();
		if (isWinner)
		{
			Console.WriteLine("\nVenceu");
		}
		else
		{
			findZero();
		}
	}
	void findZero()
	{
		for (int l = 0; l < matriz.GetLength(0); l++)
		{
			for (int c = 0; c < matriz.GetLength(1); c++)
			{
				if (matriz[l, c] == 0)
				{
					zeroPosition[0] = l;
					zeroPosition[1] = c;
					break;
				}
			}
		}
		movement();
	}
	void movement()
	{
		Console.WriteLine();
		Console.Write("Qual o movimento? ");
		string answer = Console.ReadLine();
		int answerInt = Int32.Parse(answer);
		for (int l = 0; l < matriz.GetLength(0); l++)
		{

			for (int c = 0; c < matriz.GetLength(1); c++)
			{
				if (matriz[l, c] == answerInt)
				{
					matriz[l, c] = 0;
				}
			}
		}
		matriz[zeroPosition[0], zeroPosition[1]] = answerInt;
	}

	bool winVerify()
	{
		List<int> numberOrder = new List<int>();
		for (int l = 0; l < matriz.GetLength(0); l++)
		{
			for (int c = 0; c < matriz.GetLength(1); c++)
			{
				if (matriz[l, c] != 0)
				{
					numberOrder.Add(matriz[l, c]);
				}
			}
		}

		int refNumber = 0;
		bool winner = true;
		for (int i = 0; i < numberOrder.Count; i++)
		{
			if (refNumber < numberOrder[i])
			{
				refNumber = numberOrder[i];
			}
			else
			{
				winner = false;
				break;
			}
		}

		if (winner == true && matriz[3, 3] == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}