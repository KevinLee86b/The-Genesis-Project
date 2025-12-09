using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Genesis_Projekt
{
	internal class Allianz_Schiffe
	{
	}
	public class AllianzSchiff : SchiffBasis
	{
		public char Symbol { get; set; }
		public ConsoleColor Farbe { get; set; }

		// Position im Kampfgrid
		public int Row { get; set; }
		public int Col { get; set; }

		public AllianzSchiff(
			string name,
			int maxSchild,
			int maxHuelle,
			int angriff,
			char symbol,
			ConsoleColor farbe,
			int row,
			int col
		)
			: base(name, maxSchild, maxHuelle, angriff)
		{
			Symbol = symbol;
			Farbe = farbe;
			Row = row;
			Col = col;
		}
	}

	// ==============================================================
	//  Klasse A – SCHWER (200 / 200 / 45)
	// ==============================================================

	public class AllianzA : AllianzSchiff
	{
		public AllianzA(string name, int row, int col)
			: base(name, 400, 200, 45, 'A', ConsoleColor.Red, row, col)
		{
		}
	}

	// ==============================================================
	//  Klasse B – MITTEL (150 / 150 / 30)
	// ==============================================================

	public class AllianzB : AllianzSchiff
	{
		public AllianzB(string name, int row, int col)
			: base(name, 300, 150, 30, 'B', ConsoleColor.Magenta, row, col)
		{
		}
	}

	// ==============================================================
	//  Klasse C – LEICHT (100 / 100 / 15)
	// ==============================================================

	public class AllianzC : AllianzSchiff
	{
		public AllianzC(string name, int row, int col)
			: base(name, 200, 100, 15, 'C', ConsoleColor.Yellow, row, col)
		{
		}
	}
}
