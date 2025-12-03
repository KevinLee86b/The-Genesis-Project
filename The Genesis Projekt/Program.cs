namespace The_Genesis_Projekt
{
	internal class Program
	{
		static string playerName = "";
		static string saveFile = "savegame.txt";

		static void Main(string[] args)
		{
			Console.Title = "The Genesis Projekt";
			MainMenu();
		}

		
		// HAUPTMENÜ
		
		static void MainMenu()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("==== GENESIS – HAUPTMENÜ ====\n");
			Console.ResetColor();

			Console.WriteLine("1) Neues Spiel");
			Console.WriteLine("2) Spiel laden");
			Console.WriteLine("3) Beenden");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1": StartNewGame(); break;
				case "2": LoadGame(); break;
				case "3": Environment.Exit(0); break;
				default:
					MainMenu();
					break;
			}
		}

		
		// NEUES SPIEL
		
		static void StartNewGame()
		{
			Console.Clear();
			Console.Write("Gib deinen Namen ein, Commander: ");
			playerName = Console.ReadLine();

			SaveGame("Scene_Prolog");

			ShowProlog();
		}

		
		// SPEICHERN & LADEN
	
		static void SaveGame(string sceneName)
		{
			File.WriteAllText(saveFile, sceneName + ";" + playerName);
		}

		static void LoadGame()
		{
			if (!File.Exists(saveFile))
			{
				Console.WriteLine("Kein Spielstand gefunden!");
				Console.ReadKey();
				MainMenu();
				return;
			}

			string[] data = File.ReadAllText(saveFile).Split(';');
			string sceneName = data[0];
			playerName = data[1];

			Console.WriteLine($"Spiel geladen. Willkommen zurück, Commander {playerName}!");
			Thread.Sleep(800);

			switch (sceneName)
			{
				case "Scene_Prolog": ShowProlog(); break;
				case "Scene_Intro": Scene_Intro(); break;
				case "Scene_Alarm": Scene_Alarm(); break;
			}
		}

		
		// TYPEWRITER
		static void TypeText(string text, int delay = 18)
		{
			foreach (char c in text)
			{
				Console.Write(c);
				Thread.Sleep(delay);
			}
			Console.WriteLine();
		}

		// PROLOG SZENE
		
		static void ShowProlog()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("=== PROLOG ===\n");
			Console.ResetColor();

			TypeText("In einer nicht allzu fernen Zukunft wurde die Menschheit über Generationen in Unwissenheit gehalten.");
			TypeText("Verschwiegen wurde, dass sie von einer uralten Zivilisation abstammt, den Atlanern.");
			TypeText("");

			TypeText("Die Atlaner beherrschten Hyperraumtechnologie, Genumformung und planetare Superwaffen.");
			TypeText("Ihr Versuch, Unsterblichkeit zu erreichen, erschuf ein Genvírus, das außer Kontrolle geriet.");
			TypeText("Um seine Ausbreitung zu stoppen, vernichteten sie ihre eigene Welt.");
			TypeText("");

			TypeText("In der Gegenwart fanden menschliche Machthaber die Eliten, Relikte der Atlaner…");
			TypeText("Sie kombinierten das alte Genvírus mit Nanotechnologie und schufen etwas weitaus Schlimmeres.");
			TypeText("Ihr Plan: Totale Kontrolle über die Menschheit zu erlangen, doch sie scheiterten");
			TypeText("Der Genvirus und die Nanobots mutierten und entwickelten sich zu einem eigenen Organismus");
			TypeText("Ein technobiologischer Superorganismus löschte die Menschheit fast komplett aus.");
			TypeText("");

			
			TypeText("Drei gigantische Superschiffe der Universum-Klasse blieben als letzte Zuflucht der Menschheit und eine Begleitflotte.");
			TypeText("Die Terraumformung auf dem Mars dauert noch zu lange.");
			TypeText("Ihr Ziel: den mysteriösen zehnten Planeten erreichen – Heimat der Grauen.");
			TypeText("Wenn das scheitert ist die letzte Möglichkeit den Alcubierre Antrieb zu starten und ein Artefakt der Blutsvorfahren zu suchen.");
			TypeText("Das Problem, wir haben keinerlei Erfahrung mit diesem System.");
			TypeText("");

			TypeText("Du bist Lieutenant Commander an Bord einer Landefähre.");
			TypeText("Deine Mission: das Flaggschiff GENESIS erreichen.");
			TypeText("Alle evakuieren und so schnell wie möglich die Umlaufbahn verlassen.");
			TypeText("Die Superwaffe soll gezündet werden und wir müssen schnell Abstand zur Erde bekommen.");
			TypeText("");
			TypeText("Hier beginnt deine Geschichte...");

			Console.WriteLine("\nDrücke eine Taste, um fortzufahren...");
			Console.ReadKey();

			SaveGame("Scene_Intro");
			Scene_Intro();
		}

		
		//  INTRO SZENE + SCHIFFSGRAFIK
		
		static void Scene_Intro()
		{
			Console.Clear();

			// Grafik anzeigen
			DrawGenesisTop();
			Console.WriteLine();
			Thread.Sleep(500);

			// Intro-Text
			TypeText("Gedankenverloren ließ ich meinen Blick über die gewaltigen Gondeln der GENESIS schweifen.");
			TypeText("Ihr riesiger Rumpf wirkte beinahe lebendig. Unglaublich was wir noch erschaffen haben.");
			TypeText("Unter mir lag die Erde. Friedlich. Ruhig. So wunderschön und doch voller Tod. Ein letzter schöner und verstörender Moment… ");
			TypeText("Wir waren die Letzten. Und diese Reise ist unsere einzige Hoffnung.");

			Console.WriteLine("\nDrücke eine Taste...");
			Console.ReadKey();

			SaveGame("Scene_Alarm");
			Scene_Alarm();
		}

		
		// ALARM + ENTSCHEIDUNG
		
		static void Scene_Alarm()
		{
			Console.Clear();

			// Alarmton
			Console.Beep(900, 250);
			Console.Beep(900, 250);
			Console.Beep(900, 250);

			TypeText("ALARM! ALARM! ALARM!");
			TypeText("");

			TypeText("Steuermann: Commander! Die Transportsphäre ALPHA weicht vom Flugkorridor ab!");
			TypeText("Steuermann: Sie nimmt direkten Kurs auf die GENESIS – ohne Freigabe!");
			TypeText("");

			TypeText("Was sollen wir tun?");
			Console.WriteLine("1) Warnsignal senden");
			Console.WriteLine("2) Abfangen mit Hilfstriebwerken");
			Console.WriteLine("3) Ignorieren (gefährlich!)");

			Console.Write("\nAuswahl: ");
			string input = Console.ReadLine();

			switch (input)
			{
				case "1":
					TypeText("Warnsignal gesendet! Keine Reaktion…");
					break;

				case "2":
					TypeText("Abfangmanöver eingeleitet! Hilfstriebwerke aktiv!");
					break;

				case "3":
					TypeText("Ignoriert… Möge es nicht unser Ende sein.");
					break;

				default:
					TypeText("Ungültige Eingabe.");
					Scene_Alarm();
					return;
			}

			TypeText("\nFortsetzung folgt...");
			Console.ReadKey();
		}

		
		// SCHIFFSGRAFIK
		
		static void DrawGenesisTop()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;

			Console.WriteLine("                          ________________________________");
			Console.WriteLine("                   _____/                                \\_____");
			Console.WriteLine("                __/                                          \\__");
			Console.WriteLine("             __/                                              \\__");
			Console.WriteLine("           _/                                                   \\_");
			Console.WriteLine("         _/                                                     \\_");
			Console.WriteLine("        |                 ██████████████████████████              |");
			Console.WriteLine("        |            ____/                              \\____     |");
			Console.WriteLine("        |         __/                                      \\__    |");
			Console.WriteLine("         \\_      /                                            \\_ _/");
			Console.WriteLine("           \\____/                                              \\_");

			Console.ResetColor();
		}
	}
}