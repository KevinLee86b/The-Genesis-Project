using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Genesis_Projekt
{
	internal class Raumschiff
	{
		// ================================
		//   GRUNDWERTE DES SCHIFFS
		// ================================
		public string Name { get; set; }

		public int MaxSchild { get; set; }
		public int Schild { get; set; }

		public int MaxHuelle { get; set; }
		public int Huelle { get; set; }

		public int Angriff { get; set; }

		public int KritChance { get; set; }
		public int AusweichChance { get; set; }

		// wichtig: exakt dieser Name → Zerstoert
		public bool Zerstoert => Huelle <= 0;


		// ================================
		//   KONSTRUKTOR
		// ================================
		public Raumschiff(string name, int maxSchild, int maxHuelle, int angriff,
						  int krit = 5, int dodge = 5)
		{
			Name = name;

			MaxSchild = maxSchild;
			Schild = maxSchild;

			MaxHuelle = maxHuelle;
			Huelle = maxHuelle;

			Angriff = angriff;

			KritChance = krit;
			AusweichChance = dodge;
		}


		// ================================
		//   SCHADEN ERHALTEN
		// ================================
		public void SchadenErleiden(int menge)
		{
			if (Schild > 0)
			{
				Schild -= menge;

				if (Schild < 0)
				{
					int rest = -Schild;
					Schild = 0;
					Huelle -= rest;
				}
			}
			else
			{
				Huelle -= menge;
			}

			if (Huelle < 0)
				Huelle = 0;
		}


		// ================================
		//   STATUS ANZEIGEN
		// ================================
		public void StatusAnzeigen()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"\n==== STATUS VON {Name} ====");
			Console.ResetColor();

			Console.WriteLine($"Schild : {Schild}/{MaxSchild}");
			Console.WriteLine($"Hülle  : {Huelle}/{MaxHuelle}");
			Console.WriteLine($"Angriff: {Angriff}");
			Console.WriteLine($"Krit   : {KritChance}%");
			Console.WriteLine($"Dodge  : {AusweichChance}%");

			if (Zerstoert)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n⚠️  SCHIFF ZERSTÖRT! ⚠️");
				Console.ResetColor();
			}

			Console.WriteLine();
		}
	}
}