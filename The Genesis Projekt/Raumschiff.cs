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
	public class SchiffBasis
	{
		public string Name { get; set; }
		public int Schild { get; set; }
		public int MaxSchild { get; set; }
		public int Huelle { get; set; }
		public int MaxHuelle { get; set; }
		public int Angriff { get; set; }

		public bool Zerstoert => Huelle <= 0;

		public SchiffBasis(string name, int schild, int huelle, int angriff)
		{
			Name = name;
			Schild = schild;
			MaxSchild = schild;
			Huelle = huelle;
			Angriff = angriff;
		}

		// Universelle Schadensberechnung
		public void SchadenErleiden(int damage)
		{
			if (Schild > 0)
			{
				int absorb = Math.Min(Schild, damage);
				Schild -= absorb;
				damage -= absorb;
			}

			if (damage > 0)
				Huelle -= damage;
		}

		public void Status()
		{
			Console.WriteLine($"{Name} – Schild: {Schild}/{MaxSchild}, Hülle: {Huelle}");
		}
	}


	// ===============================================
	//  JÄGER – kleine, schnelle Einheiten
	// ===============================================

	public class Jaeger : SchiffBasis
	{
		public Jaeger(string name)
			: base(name, schild: 10, huelle: 20, angriff: 8)
		{ }
	}


	// ===============================================
	//  FREGATTE – mittelstarke Einheiten
	// ===============================================

	public class Fregatte : SchiffBasis
	{
		public Fregatte(string name)
			: base(name, schild: 40, huelle: 60, angriff: 18)
		{ }
	}


	// ===============================================
	//  ZERSTÖRER – Boss-Einheit, stark gepanzert
	// ===============================================

	public class Zerstoerer : SchiffBasis
	{
		public Zerstoerer(string name)
			: base(name, schild: 120, huelle: 150, angriff: 30)
		{ }
	}
}