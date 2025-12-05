using System;
using System.IO;
using System.Threading;

namespace The_Genesis_Projekt
{
	internal class Program
	{
		// Spielername
		static string playerName = "";
		// letzter gespeicherter Szenenname (für Slots)
		static string lastScene = "Scene_Prolog";
		// Autosave-Datei
		static string autoSaveFile = "autosave.sav";
		// drei manuelle Speicher-Slots
		static string[] manualSlots = { "save_slot1.sav", "save_slot2.sav", "save_slot3.sav" };

		static void Main(string[] args)
		{
			Console.Title = "The Genesis Projekt";
			MainMenu();
		}

		// Hauptmenük
		static void MainMenu()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("==== GENESIS – HAUPTMENÜ ====\n");
			Console.ResetColor();

			Console.WriteLine("1) Neues Spiel");
			Console.WriteLine("2) Spiel laden");
			Console.WriteLine("3) Spiel speichern");
			Console.WriteLine("4) Beenden");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					StartNewGame();
					break;

				case "2":
					LoadGameMenu();
					break;

				case "3":
					SaveGameMenu();
					break;

				case "4":
					Environment.Exit(0);
					break;

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

			SaveGame("Scene_Prolog"); // Autosave + lastScene setzen

			ShowProlog();
		}


		// ==========================
		// SPEICHERN & LADEN SYSTEM
		// ==========================

		// Autosave + lastScene
		static void SaveGame(string sceneName)
		{
			lastScene = sceneName;
			File.WriteAllText(autoSaveFile, sceneName + ";" + playerName);
		}

		// Manuelles Speichern
		static void SaveGameMenu()
		{
			Console.Clear();
			Console.WriteLine("=== Spiel speichern ===\n");
			Console.WriteLine("Aktuelle Szene: " + lastScene);
			Console.WriteLine("Aktueller Spieler: " + (string.IsNullOrWhiteSpace(playerName) ? "<unbekannt>" : playerName));
			Console.WriteLine();

			Console.WriteLine("In welchen Slot speichern?");
			Console.WriteLine("1) Slot 1");
			Console.WriteLine("2) Slot 2");
			Console.WriteLine("3) Slot 3");
			Console.Write("\nAuswahl: ");

			string input = Console.ReadLine();
			if (!int.TryParse(input, out int slot) || slot < 1 || slot > 3)
			{
				Console.WriteLine("Ungültige Auswahl.");
				Console.ReadKey();
				MainMenu();
				return;
			}

			int index = slot - 1;
			File.WriteAllText(manualSlots[index], lastScene + ";" + playerName);
			Console.WriteLine($"Spielstand in Slot {slot} gespeichert.");
			Console.ReadKey();
			MainMenu();
		}

		// Lade-Menü
		static void LoadGameMenu()
		{
			Console.Clear();
			Console.WriteLine("=== Spiel laden ===\n");
			Console.WriteLine("1) Letzten Autosave laden");
			Console.WriteLine("2) Manuell aus Slot laden");
			Console.WriteLine("3) Zurück zum Hauptmenü");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					LoadAutoSave();
					break;
				case "2":
					LoadManualSlot();
					break;
				case "3":
					MainMenu();
					break;
				default:
					LoadGameMenu();
					break;
			}
		}

		static void LoadAutoSave()
		{
			if (!File.Exists(autoSaveFile))
			{
				Console.WriteLine("Kein Autosave gefunden.");
				Console.ReadKey();
				MainMenu();
				return;
			}

			string[] data = File.ReadAllText(autoSaveFile).Split(';');
			if (data.Length >= 1) lastScene = data[0];
			if (data.Length >= 2) playerName = data[1];

			Console.WriteLine($"Autosave geladen. Willkommen zurück, Commander {playerName}!");
			Thread.Sleep(800);

			ContinueFromScene();
		}

		static void LoadManualSlot()
		{
			Console.Clear();
			Console.WriteLine("=== Manuelle Speicherstände ===\n");

			for (int i = 0; i < manualSlots.Length; i++)
			{
				string info = File.Exists(manualSlots[i])
					? File.ReadAllText(manualSlots[i]).Split(';')[0]
					: "<leer>";
				Console.WriteLine($"{i + 1}) {manualSlots[i]}  ->  {info}");
			}

			Console.Write("\nWelchen Slot laden? (1-3): ");
			string input = Console.ReadLine();

			if (!int.TryParse(input, out int slot) || slot < 1 || slot > 3)
			{
				Console.WriteLine("Ungültige Auswahl.");
				Console.ReadKey();
				MainMenu();
				return;
			}

			int index = slot - 1;

			if (!File.Exists(manualSlots[index]))
			{
				Console.WriteLine("In diesem Slot ist kein Spielstand gespeichert.");
				Console.ReadKey();
				MainMenu();
				return;
			}

			string[] data = File.ReadAllText(manualSlots[index]).Split(';');
			if (data.Length >= 1) lastScene = data[0];
			if (data.Length >= 2) playerName = data[1];

			Console.WriteLine($"Slot {slot} geladen. Szene: {lastScene}, Spieler: {playerName}");
			Thread.Sleep(800);

			ContinueFromScene();
		}

		// Szene anhand von lastScene wieder aufrufen
		static void ContinueFromScene()
		{
			switch (lastScene)
			{
				case "Scene_Prolog": ShowProlog(); break;
				case "Scene_Intro": Scene_Intro(); break;
				case "Scene_Alarm": Scene_Alarm(); break;
				case "Scene_Kapitel1": Scene_Kapitel1(); break;
				case "Scene_HeliosKontakt": Scene_HeliosKontakt(); break;
				default:
					Console.WriteLine("Unbekannte Szene im Speicherstand. Zurück zum Hauptmenü.");
					Console.ReadKey();
					MainMenu();
					break;
			}
		}



		// TYPEWRITER
		static void TypeText(string text, int delay = 18)
		{
			bool skip = false;

			for (int i = 0; i < text.Length; i++)
			{
				// Wenn der Spieler eine Taste drückt -> Rest des Textes sofort anzeigen
				if (!skip && Console.KeyAvailable)
				{
					// Taste aus dem Puffer entfernen
					Console.ReadKey(true);
					skip = true;

					// Rest der Zeile am Stück ausgeben
					Console.Write(text.Substring(i));
					break;
				}

				if (!skip)
				{
					Console.Write(text[i]);
					Thread.Sleep(delay);
				}
			}

			Console.WriteLine();

			// Eingabepuffer vollständig leeren,
			// damit keine Tasten in die nächste Zeile "durchrutschen"
			while (Console.KeyAvailable)
				Console.ReadKey(true);
		}

		// PROLOG SZENE

		static void ShowProlog()
		{
			// Autosave an dieser Stelle sinnvoll
			SaveGame("Scene_Prolog");

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
			SaveGame("Scene_Intro");

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


		// SZENE ALARM 

		static void Scene_Alarm()
		{
			SaveGame("Scene_Alarm");

			Console.Clear();

			//  ALARMTON 
			Console.Beep(900, 250);
			Console.Beep(900, 250);
			Console.Beep(900, 250);

			TypeText("ALARM! ALARM! ALARM!");
			TypeText("");

			//  STEUERMANN MELDET 
			TypeText("Steuermann: Commander! Die Transportsphäre ALPHA weicht vom Flugkorridor ab!");
			TypeText("Steuermann: Sie nimmt direkten Kurs auf die GENESIS – ohne Freigabe!");
			TypeText("");

			// ERSTE ENTSCHEIDUNG 
			TypeText("Was sollen wir tun?");
			Console.WriteLine("1) Warnsignal senden");
			Console.WriteLine("2) Abfangen mit Hilfstriebwerken");
			Console.WriteLine("3) Ignorieren (GEFÄHRLICH!)");
			Console.Write("\nAuswahl: ");

			string input = Console.ReadLine();

			switch (input)
			{
				case "1":
					Scene_Alarm_Warnsignal();
					break;

				case "2":
					Scene_Alarm_Abfangen();
					break;

				case "3":
					TypeText("Ignoriert… Möge es nicht unser Ende sein.");
					Thread.Sleep(900);
					GameOver("Die Transportsphäre beschleunigt und kollidiert mit der GENESIS.\n" +
							 "Eine gigantische Explosion zerreißt das Schiff.\n" +
							 "Nichts überlebt.");
					break;

				default:
					TypeText("Ungültige Eingabe.");
					Scene_Alarm();
					break;
			}
		}


		//  TEIL 1: WARNUNG SENDEN 

		static void Scene_Alarm_Warnsignal()
		{
			Console.Clear();

			TypeText("Warnsignal gesendet! Keine Reaktion…");
			TypeText("");

			TypeText("Commander… sollen wir erneut versuchen?");
			Console.WriteLine("1) Noch einmal Warnsignal senden");
			Console.WriteLine("2) Abfangmanöver einleiten");
			Console.Write("\nAuswahl: ");

			string again = Console.ReadLine();

			if (again == "1")
			{
				TypeText("Erneutes Warnsignal wird gesendet…");
				Thread.Sleep(800);
				TypeText("…keine Reaktion. Die Sphäre beschleunigt plötzlich!");
				Thread.Sleep(800);

				GameOver("Die Transportsphäre ALPHA kollidiert frontal mit der GENESIS.\n" +
						 "Durch die fehlenden Schilde wird der gesamte Hangar zerstört.\n" +
						 "Die GENESIS bricht auseinander…");
			}
			else if (again == "2")
			{
				Scene_Alarm_Abfangen();
			}
			else
			{
				TypeText("Ungültige Eingabe.");
				Scene_Alarm_Warnsignal();
			}
		}



		// TEIL 2: ABFANGMANÖVER

		static void Scene_Alarm_Abfangen()
		{
			Console.Clear();

			TypeText("Abfangmanöver eingeleitet! Hilfstriebwerke aktiv!");
			Thread.Sleep(600);

			TypeText("Wir verringern den Abstand… Commander, wie weiter?");
			Console.WriteLine("1) Andocken");
			Console.WriteLine("2) Feuer eröffnen");
			Console.Write("\nAuswahl: ");

			string choice2 = Console.ReadLine();

			if (choice2 == "1")
			{
				TypeText("Andockmanöver eingeleitet…");
				Thread.Sleep(800);
				TypeText("Bewegung im Inneren der Sphäre… das ist nicht menschlich…");

				GameOver("Beim Andocken breitet sich der mutierte Nanovirus explosionsartig aus.\n" +
						 "Innerhalb weniger Sekunden ist die gesamte Crew infiziert.");
			}
			else if (choice2 == "2")
			{
				// Feuer-Grafik
				Console.Clear();
				DrawFiringGraphic();
				Console.Beep(600, 150);
				Console.Beep(650, 150);
				Console.Beep(700, 200);
				Thread.Sleep(1000);

				// Explosion
				Console.Clear();
				DrawExplosionGraphic();
				Console.Beep(200, 300);
				Console.Beep(180, 200);
				Console.Beep(150, 500);

				TypeText("Ziel vernichtet. Commander… die Insassen waren verloren.");
				TypeText("Aber der Virus durfte sich nicht ausbreiten.");
				TypeText("");

				TypeText("Die Brücke ist still. Jeder weiß, es war die einzige richtige Entscheidung.");
				TypeText("");

				TypeText("Fortsetzung folgt…");
				Console.ReadKey();

				SaveGame("Scene_Kapitel1");
				Scene_Kapitel1();
				return;
			}
			else
			{
				TypeText("Ungültige Eingabe.");
				Scene_Alarm_Abfangen();
			}
		}




		//  GAME OVER 

		static void GameOver(string message)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;

			TypeText("=== GAME OVER ===");
			TypeText("");
			TypeText(message);

			Console.ResetColor();
			TypeText("\nWas möchtest du tun?");
			Console.WriteLine("1) Letzten Autosave laden");
			Console.WriteLine("2) Speicherstand laden (Slot)");
			Console.WriteLine("3) Hauptmenü");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					LoadAutoSave();
					break;
				case "2":
					LoadManualSlot();
					break;
				default:
					MainMenu();
					break;
			}
		}




		//  GRAFIKEN 
		static void DrawFiringGraphic()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("      /‾‾‾\\                  /‾‾‾\\");
			Console.WriteLine("     |     |  ----=====>    |     |");
			Console.WriteLine("      \\___/                  \\___/");
			Console.ResetColor();
		}

		static void DrawExplosionGraphic()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("                * * * * * * * * * * *");
			Console.WriteLine("             * * * * * * * * * * * * * *");
			Console.WriteLine("           * * * * *   BOOOOM   * * * * * *");
			Console.WriteLine("             * * * * * * * * * * * * * *");
			Console.WriteLine("              * * * * * * * * * * * * ");
			Console.ResetColor();
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
		//                            KAPITEL 1 
		//                      FLUCHT VON DER ERDE 

		static void Scene_Kapitel1()
		{
			SaveGame("Scene_Kapitel1");

			Console.Clear();

			// KAPITEL-HEADER
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("===================================================");
			Console.WriteLine("                    KAPITEL 1");
			Console.WriteLine("                 FLUCHT VON DER ERDE");
			Console.WriteLine("===================================================");
			Console.ResetColor();
			Console.WriteLine();

			// FLOTTE ZEIGEN
			DrawGenesisFleet();
			TypeText("");
			TypeText("Drei gewaltige Schiffe der UNIVERSUM-Klasse glitten wie stählerne Inseln durch die Dunkelheit.");
			TypeText("Die GENESIS an der Spitze, dahinter ihre Schwesterschiffe, voll mit den letzten Menschen der Erde.");
			TypeText("");

			TypeText("Ich atmete tief durch und betrat die andere Landefähre fast im Laufschritt.");
			TypeText("Jede Sekunde zählte. ALPHA war zerstört. Mit ihr der gesamte kommandierende Stab der GENESIS.");
			TypeText("Meine Fähre wurde durch die Explosion beschädigt. Jetzt verlieren wir schon so viele Ressourcen.");
			TypeText("");

			TypeText("Während die Schleuse sich schloss, krochen Zweifel in meinen Kopf.");
			TypeText("Ich hatte die Fähre vernichtet. Männer und Frauen, die seit Jahren im Dienst der Menscheit waren.");
			TypeText("Und irgendwo in einer anderen Fähre war meine Frau. Ich konnte nur hoffen, dass sie es an Bord schaffte.");
			TypeText("");
			TypeText("Aber für Sentimentalität war keine Zeit. Die GENESIS brauchte jetzt einen klaren Kopf.");
			TypeText("");

			TypeText("Die Landefähre dockte mit einem dumpfen Ruck an. Die Türen glitten zur Seite.");
			TypeText("Der Geruch von erhitztem Metall und ozongesättigter Luft schlug mir entgegen, der Duft eines Kriegsschiffes.");
			TypeText("");

			// GRAFIK: DOCK + WEG ZUR BRÜCKE
			DrawDockToBridge();
			TypeText("");
			TypeText("Ich setzte mich in Bewegung. Gang A-17, vorbei an Technikern, Sanitätern, müden Flüchtlingen.");
			TypeText("Kein Blick blieb lange an mir hängen, aber sie wussten, wer ich war.");
			TypeText("");

			TypeText("Vor dem lift zur Brücke blieb ich kurz stehen.");
			TypeText("Für einen Moment wünschte ich mir, jemand anderes würde jetzt diese Verantwortung tragen.");
			TypeText("Doch die Realität war unerbittlich: Ich hatte die Fähre zerstört. Ich hatte über Leben und Tod entschieden.");
			TypeText("Und genau deshalb musste ich jetzt weitergehen.");
			TypeText("");

			// GRAFIK: LIFT
			DrawLiftPath();
			TypeText("");
			TypeText("Die Türen schlossen sich. Der Lift summte, als er sich nach oben bewegte.");
			TypeText("Deck um Deck zog in Sekundenschnelle an mir vorbei, als wären es nur Kapitel eines Buches, das ich nicht mehr umschreiben konnte.");
			TypeText("");

			TypeText("Ein Signalton. Die Türen öffneten sich zur Brücke der GENESIS.");
			TypeText("");

			// BRÜCKE
			Console.Clear();
			DrawBridgeSilhouette();
			TypeText("");

			TypeText("Alle Köpfe drehten sich zu mir um. Gespräche verstummten. Nur das leise Summen der Systeme blieb.");
			TypeText("");

			TypeText("Eine Frau löste sich aus dem Kreis der Offiziere und kam entschlossenen Schrittes auf mich zu.");
			TypeText("Schwarze Haut, scharf geschnittene Züge, die Uniform tadellos – Lieutenant Sheila Oduro.");
			TypeText("");

			TypeText("Oduro: Commander, ... Captain!");
			TypeText("Ich nickte nur.");
			TypeText("");

			TypeText("Oduro: Sie sind jetzt der ranghöchste Offizier an Bord. Der Admiral, Captain und der gesamte Stab sind mit ALPHA gefallen.\"");
			TypeText("Ein Raunen ging über die Brücke. Alle wussten es aber jetzt war es ausgesprochen.");
			TypeText("");

			TypeText("Ich spürte, wie mir der Boden für einen Moment entglitt.");
			TypeText("Dann zwang ich meine Schultern nach hinten und atmete einmal tief durch.");
			TypeText("");

			TypeText("Interne Kommunikation, Schiffsdurchsage auf alle Decks! Sagte ich.");
			TypeText("Eine junge Offizierin an der Konsole nickte hektisch. Kanal steht, Commander.");
			TypeText("");

			TypeText("Ich trat einen Schritt nach vorn, blickte in das offene Holo-Mikrofon und sprach so klar, wie ich konnte:");
			TypeText("");

			TypeText("Hier spricht Lieutenant Commander…");
			TypeText("…mit sofortiger Wirkung übernehme ich in dieser Notsituation das Kommando über die GENESIS.");
			TypeText("Unser Kurs bleibt unverändert: Flucht aus dem Erdorbit, Sammlung der Flotte, Vorbereitung, Kurs zum 10 Planeten.");
			TypeText("");

			TypeText("Wir haben keinen Plan B.");
			TypeText("Aber solange dieses Schiff atmet, wird es die letzte Hoffnung der Menschheit sein.");
			TypeText("");

			TypeText("Ich beendete die Übertragung. Die Brücke war still aber diesmal fühlte sich die Stille anders an.");
			TypeText("Nicht wie ein Schock. Eher wie… Erwartung.");
			TypeText("");

			TypeText("Oduro trat neben mich und senkte leicht den Kopf.");
			TypeText("Aye, Captain, sagte sie leise.");
			TypeText("");

			TypeText("Zum ersten Mal fühlte sich das Wort nicht falsch an.");
			TypeText("Und genau hier begann unsere eigentliche Reise.");
			TypeText("");

			SaveGame("Scene_HeliosKontakt");
			Scene_HeliosKontakt();
			return;
		}


		// Flotte mit drei Schiffen
		static void DrawGenesisFleet()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("               ___          ___           ___");
			Console.WriteLine("     [GENESIS]/___\\ [ARGOS]/___\\ [HELIOS]/___\\");
			Console.WriteLine("              \\___/        \\___/         \\___/");
			Console.WriteLine("                |             |              |");
			Console.WriteLine("             ~~~~~         ~~~~~          ~~~~~");
			Console.ResetColor();
		}

		// Dock / Landefähre -> Pfeil -> GENESIS-Silhouette
		static void DrawDockToBridge()
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine(" [Erde-Orbit Dock]");
			Console.WriteLine("      /‾‾‾\\");
			Console.WriteLine("     |FÄHRE|  ----->  [ANDOCKRING GENESIS]");
			Console.WriteLine("      \\___/");
			Console.ResetColor();
		}

		// Liftfahrt von Deck zu Brücke
		static void DrawLiftPath()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("      [Deck 7] |");
			Console.WriteLine("               |");
			Console.WriteLine("               v");
			Console.WriteLine("      [Deck 5] |");
			Console.WriteLine("               |");
			Console.WriteLine("               v");
			Console.WriteLine("      [Deck 3] |");
			Console.WriteLine("               |");
			Console.WriteLine("               v");
			Console.WriteLine("      [BRÜCKE]");
			Console.ResetColor();
		}

		// Einfache Brückensilhouette
		static void DrawBridgeSilhouette()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("           __________________________");
			Console.WriteLine("          /                          \\");
			Console.WriteLine("         /  [ PANORAMA-FRONTSCHEIBE ] \\");
			Console.WriteLine("        /______________________________\\");
			Console.WriteLine("        |   O   O   O   O   O   O      ||  <- Stationskonsolen");
			Console.WriteLine("        |                              ||");
			Console.WriteLine("        |       [   ZENTRALE   ]       ||");
			Console.WriteLine("        |______________________________||");
			Console.ResetColor();
		}

		static void Scene_HeliosKontakt()
		{
			SaveGame("Scene_HeliosKontakt");

			Console.Clear();

			// Langer Gefechtsalarm
			for (int i = 0; i < 6; i++)
			{
				Console.Beep(950, 500);
				Console.Beep(650, 400);
			}

			TypeText("ALARM! ALARM! Unbekannte Signatur – Schwesterschiff HELIOS nähert sich!", 15);
			TypeText("");

			TypeText("Offizier Kade: Commander! Wir empfangen eine Notfallverschlüsselung!");
			Thread.Sleep(600);

			TypeText("Kade: Die HELIOS ist...  infiziert. Der Virus hat die Brücke übernommen.");
			TypeText("Kade: Die Besatzung verliert die Kontrolle, ihre Waffensysteme fahren hoch!");
			TypeText("");

			TypeText("Commander: Auf den Schirm damit.");
			Thread.Sleep(400);

			// Verzerrte Übertragung
			Console.Beep(400, 200);
			Console.Beep(300, 200);

			TypeText("HELIOS-Übertragung: „…ÜBERGEBT… EUCH… DIE *GENESIS*… WIDERSTAND… ZWECKLOS…“", 10);
			TypeText("");

			TypeText("Commander: Verbindung trennen!");
			Thread.Sleep(300);

			TypeText("Kade: Commander unsere Schilde sind OFFLINE. Waffen: OFFLINE. Antrieb: OFFLINE.");
			TypeText("Kade: Der Virus versucht unsere Systeme zu infiltrieren und mit dem Kommandostab sind auch die Zugangscodes verloren gegangen.");
			TypeText("");

			TypeText("Commander: Dann holen wir uns die Kontrolle zurück. Optionen! Schnell!");
			TypeText("");

			Console.WriteLine("(1) Notfall-Kontakt zur HELIOS herstellen");
			Console.WriteLine("(2) Eigenes System-Override versuchen");
			Console.WriteLine("(3) Nur beobachten (sehr gefährlich)");
			Console.Write("\nAuswahl: ");

			string input = Console.ReadLine();

			if (input == "1")
			{
			TypeText("Kade: Commander, das Signal ist instabil. Sie antworten nicht… oder wollen nicht.");
			TypeText("Kade: Die HELIOS lädt ihre Partikelkanonen auf!");
			TypeText("");
			TypeText("Commander: Wir haben keine Zeit. Override vorbereiten!");
			Scene_HackMinigame();
			return;
			}
			else if (input == "2")
			{
			TypeText("Commander: Alles klar! Override einleiten!");
			Scene_HackMinigame();
			return;
			}
			else if (input == "3")
			{
			TypeText("Kade: Commander… sie zielen auf uns! Wir müssen handeln!");
			Thread.Sleep(800);

			GameOver("Die HELIOS eröffnet das Feuer. Ohne Schilde ist die GENESIS chancenlos.");
			return;
			}
			else
			{
			TypeText("Ungültige Eingabe.");
			Scene_HeliosKontakt();
			return;
			}
			static void Scene_HackMinigame()
			{
			Console.Clear();

			TypeText("⚠ Zugriff verweigert…");
			TypeText("⚠ Sicherheitssystem aktiviert…");
			TypeText("");
			TypeText("KADE: Commander, das System hat die Waffenkontrolle verriegelt.");
			TypeText("KADE: Wir können sie nur zurückholen, wenn Sie den fehlerhaften Schaltkreis finden!");
			TypeText("");

			Thread.Sleep(600);

				// Zufälligen Schaltkreis erzeugen
			Random rnd = new Random();

				// Symbole eines intakten Schaltkreises
			string[] module = { "A", "B", "C", "D", "E" };

				// Fehler-Symbol, das nicht hineingehört
			string fehlerSymbol = "X";

				// Position des Fehlers bestimmen
			int fehlerPosition = rnd.Next(0, module.Length);

				// Ausgabe vorbereiten
			string[] kreis = new string[module.Length];
			for (int i = 0; i < module.Length; i++)
				kreis[i] = module[i];

				kreis[fehlerPosition] = fehlerSymbol;  // Fehler setzen

				// Spieler darf viele Versuche machen
			while (true)
			{
			Console.Clear();
			TypeText("=== SCHALTKREIS-REPARATUR – FEHLER FINDEN ===", 10);
			Console.WriteLine();

			TypeText("KADE: Ein Modul im Datenpfad ist beschädigt. Finden Sie das fehlerhafte Element!", 5);
			Console.WriteLine();

				// Schaltkreis anzeigen
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("   ");
			for (int i = 0; i < kreis.Length; i++)
			{
			Console.Write($"[{kreis[i]}] ");
			}
			Console.ResetColor();
			Console.WriteLine("\n");

			Console.Write("Position des fehlerhaften Moduls eingeben (1–5): ");
			string input = Console.ReadLine()!;

				// Check if numeric
			if (int.TryParse(input, out int pos))
			{
			pos--; // Spieler gibt 1–5 ein, wir brauchen 0–4

			if (pos == fehlerPosition)
			{
			TypeText("\nKADE: ✔ Fehler erkannt! Schaltkreis wird neu geroutet…", 10);
			Console.Beep(800, 200);

			Thread.Sleep(800);

			TypeText("⚡ Override erfolgreich! Waffen- und Schildsysteme sind wieder online!", 10);
			TypeText("");

			Thread.Sleep(600);

				// Weiter zur nächsten Szene
			Scene_HeliosGefahr();
			return;
			}
			else
			{
			TypeText("\nKADE: ✖ Das war nicht das fehlerhafte Modul!", 10);
			Console.Beep(300, 200);
			Thread.Sleep(600);

			TypeText("KADE: Analyse aktualisiert… versuchen Sie es erneut!", 10);
			Thread.Sleep(600);
			}
			}
			else
			{
			TypeText("\nKADE: Ungültige Eingabe. Bitte eine Zahl von 1 bis 5 eingeben.", 10);
			Thread.Sleep(600);
			}
			}
				//   Szene: HELIOS GEHT IN ANGRIFF
			Scene_HeliosGefahr();
			return;
			}
			static void Scene_HeliosGefahr()
			{
			Console.Clear();

				// Tiefer Alarm – länger & bedrohlich
			Console.Beep(400, 600);
			Console.Beep(350, 600);
			Console.Beep(300, 600);

			TypeText("⚠️  ALARM!  Die HELIOS fährt ihre Waffensysteme hoch!");
			TypeText("⚠️  Infizierte übernehmen die Kontrolle des Schiffes!");
			TypeText("");
			Thread.Sleep(600);

			TypeText("Commander: Statusbericht!");
			Thread.Sleep(500);

			TypeText("Kade: Commander... die HELIOS richtet die Partikelkanonen auf UNS!");
			Thread.Sleep(700);

			TypeText("Commander: Haben wir Zugriff auf unsere Waffen?");
			Thread.Sleep(600);

			TypeText("Kade: Positiv. Alle Systeme wieder hergestellt.");
			TypeText("Commander: Schilde hoch, Waffen aktivieren. ");
			TypeText("");

			TypeText("Kade: Wird ausgeführt.");
			Thread.Sleep(600);

			TypeText("Commander: Ausweichmanöver. Bringt die Kampfmodule online — JETZT!");
			Thread.Sleep(700);

				// Jetzt beginnt der Kampf
			Scene_HeliosKampf();
			}
				//  SZENE: HELIOS KAMPF  

			static void Scene_HeliosKampf()
			{
				Console.Clear();

				// Zwei Schiffe erstellen
				Raumschiff genesis = new Raumschiff("GENESIS", 120, 160, 35);
				Raumschiff helios = new Raumschiff("HELIOS", 100, 130, 28);

				TypeText("⚔️   KAMPF BEGINNT — GENESIS vs. HELIOS  ⚔️", 10);
				Thread.Sleep(800);

				int runde = 1;

				while (!genesis.Zerstoert && !helios.Zerstoert)
				{
					Console.Clear();
					TypeText($"===== RUNDE {runde} =====", 5);
					Console.WriteLine();

					// Beide Schiffe anzeigen
					DrawShipGenesis();
					DrawShipHelios();
					Console.WriteLine();

					genesis.StatusAnzeigen();
					Console.WriteLine();
					helios.StatusAnzeigen();

					Console.WriteLine();
					Console.WriteLine("Aktionen:");
					Console.WriteLine("[1] Angriff");
					Console.WriteLine("[2] Schild verstärken");
					Console.WriteLine("[3] Ausweichmanöver");
					Console.WriteLine("[4] Hackversuch (Riskant)");
					Console.Write("\nAuswahl: ");
					string input = Console.ReadLine();
					Console.WriteLine();

					bool ausgewichen = false;

					//  Spieleraktion 
					if (input == "1")
					{
						TypeText("GENESIS feuert!", 5);
						PlayLaser();
						DrawLaserLeftToRight();
						helios.SchadenErleiden(genesis.Angriff);
						Thread.Sleep(600);
					}
					else if (input == "2")
					{
						TypeText("Schilde werden verstärkt...", 10);
						Console.Beep(400, 200);
						genesis.Schild += 25;

						if (genesis.Schild > genesis.MaxSchild)
						{
							TypeText("⚠️ Überladung! Schild bricht kurz zusammen!", 10);
							genesis.Schild = 10;
							genesis.Huelle -= 10;
						}
					}
					else if (input == "3")
					{
						TypeText("GENESIS führt ein Ausweichmanöver aus!", 10);
						ausgewichen = true;
						Console.Beep(300, 100);
						Console.Beep(350, 100);
						Console.Beep(400, 100);
					}
					else if (input == "4")
					{
						TypeText("HACK-Versuch wird gestartet...", 10);
						Console.Beep(600, 200);

						if (new Random().Next(1, 100) <= 30)
						{
							TypeText("✔ Hack erfolgreich! HELIOS verliert ihr Schild!", 10);
							helios.Schild = 0;
						}
						else
						{
							TypeText("❌ Hack fehlgeschlagen! Selbstschaden!", 10);
							genesis.Huelle -= 25;
						}
					}
					else
					{
						TypeText("Ungültige Eingabe – du verlierst die Aktion!", 10);
					}


					//  Gegnerische Aktion 
					if (!helios.Zerstoert)
					{
						Thread.Sleep(500);
						TypeText("HELIOS erwidert das Feuer!", 5);

						if (!ausgewichen)
						{
							PlayLaserEnemy();
							DrawLaserRightToLeft();
							genesis.SchadenErleiden(helios.Angriff);
						}
						else
						{
							TypeText("→ GENESIS ist ausgewichen! Kein Treffer!", 5);
						}
					}

					Thread.Sleep(900);
					runde++;
				}

				Console.Clear();

				if (genesis.Zerstoert)
				{
					DrawExplosion();
					Console.Beep(200, 500);
					Console.Beep(150, 600);
					TypeText("❌ Die GENESIS wurde zerstört...", 10);
					GameOver("Du konntest der HELIOS nicht standhalten.");
					return;
				}
				else
				{
					DrawExplosion();
					Console.Beep(600, 500);
					Console.Beep(700, 600);
					TypeText("✔ Eine gewaltige Explosion an der Helios! Sieg!", 10);
					TypeText("Die GENESIS treibt beschädigt, aber lebendig weiter...", 10);
					Thread.Sleep(1200);

					Scene_HeliosNachKampf();
					return;
				}
			}



			// Genesis // Helios


			static void DrawShipGenesis()
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("                                                                                ________________________________");
				Console.WriteLine("                                                                         _____/           GENESIS              \\_____");
				Console.WriteLine("                                                                      __/                                          \\__");
				Console.WriteLine("                                                                   __/                                              \\__");
				Console.WriteLine("                                                                 _/                                                   \\_");
				Console.WriteLine("                                                               _/                                                     \\_");
				Console.WriteLine("                                                              |                 ██████████████████████████              |");
				Console.WriteLine("                                                              |            ____/                              \\____     |");
				Console.WriteLine("                                                              |         __/                                      \\__    |");
				Console.WriteLine("                                                               \\_      /                                            \\_ _/");
				Console.WriteLine("                                                                 \\____/                                              \\_");
				Console.ResetColor();
			}

			static void DrawShipHelios()
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("                          _________________________________");
				Console.WriteLine("                   _____/            HELIOS               \\____");
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



			// Laser // Explosion


			static void PlayLaser()
			{
				Console.Beep(900, 100);
				Console.Beep(1000, 80);
			}

			static void PlayLaserEnemy()
			{
				Console.Beep(600, 100);
				Console.Beep(550, 100);
			}

			static void DrawLaserLeftToRight()
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(">>>>>>>>>=======================>");
				Console.ResetColor();
			}

			static void DrawLaserRightToLeft()
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("<========================<<<<<<<<<");
				Console.ResetColor();
			}

			static void DrawExplosion()
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("         * * * * * * * * * * * ");
				Console.WriteLine("       *************************");
				Console.WriteLine("      *************************** ");
				Console.WriteLine("     * * * * *  BOOOOOM  * * * * *");
				Console.WriteLine("     *****************************");
				Console.WriteLine("      **************************");
				Console.WriteLine("        * * * * * * * * * * * *");
				Console.ResetColor();
			}
			static void DrawHeliosDestroyed()
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("                     ____...----...____");
				Console.WriteLine("              __..--'                 `--..__");
				Console.WriteLine("          _.-'       HELIOS (zerstört)       `-._");
				Console.WriteLine("       .-'     ____________███____________        `-.");
				Console.WriteLine("     .'      _/                                \\_      `.");
				Console.WriteLine("    /      _/   *  *   *         *     *         \\_      \\");
				Console.WriteLine("   |     _/     *    *    *   *        *           \\_     |");
				Console.WriteLine("   |    |      ████  *   TRÜMMER   *   ████          |    |");
				Console.WriteLine("    \\    \\_      *     *   *   *      *           _/    /");
				Console.WriteLine("     `.     \\_        *     *     *            _/     .'");
				Console.WriteLine("       `-.     \\__        *   *            __/     .-'");
				Console.WriteLine("          `--..__  `--..______________..--'  __..--'");
				Console.WriteLine("                 `\"\"--...________...--\"\"`");
				Console.ResetColor();

			}
			static void Scene_HeliosNachKampf()
			{
				Console.Clear();
				SaveGame("Scene_HeliosNachKampf");

				// Grafik der zerstörten HELIOS
				DrawHeliosDestroyed();
				TypeText("");

				TypeText("Eine gewaltige Explosion zerreißt die HELIOS. Ein blendender Lichtblitz flutet die Brücke.");
				TypeText("Kurz darauf driftet das einst stolze Schwesterschiff leblos und zerbrochen durch den Raum.");
				TypeText("");

				TypeText("Commander: Schadensbericht!");
				Thread.Sleep(500);

				TypeText("Oduro: Captain… unsere Schilde sind schwer beschädigt.");
				TypeText("Oduro: Die Hülle weist deutliche strukturelle Verluste auf, aber alle Kernsysteme funktionieren stabil.");
				TypeText("Oduro: Wir können weiterkämpfen – aber nicht unbegrenzt.");
				TypeText("");

				Console.Beep(500, 300);
				TypeText("KADE: Commander! Eingehender Funkspruch – von der ARGOS!");
				Thread.Sleep(500);

				TypeText("ARGOS: „GENESIS, hier ist Captain Ren. Wir haben erst jetzt unsere Systeme vollständig hochgefahren.“");
				TypeText("ARGOS: „Viele Module mussten bei laufendem Betrieb integriert werden – das hat uns Zeit gekostet.“");
				TypeText("");

				TypeText("Commander: Gibt es Neuigkeiten zu den Überlebenden der Evakuierung?");
				Thread.Sleep(500);

				TypeText("ARGOS: „Bestätigt. Wir haben mehrere Evakuierungsfähren aufgenommen.“");
				TypeText("ARGOS: „Darunter… Ihre Frau, Captain. Sie lebt und befindet sich in stabiler Verfassung.“");
				TypeText("");

				TypeText("Für einen Moment blieb mir die Luft weg. Die Anspannung fiel von meinen Schultern.");
				TypeText("");

				Console.Beep(400, 200);
				TypeText("KADE: Commander! Wir haben ein Problem!");
				Thread.Sleep(300);

				TypeText("KADE: Durch die Explosion der HELIOS und die massive Druckwelle…");
				TypeText("KADE: ...ist unser Kontakt zur Atlaner-Superwaffe auf der Erdoberfläche abgerissen!");
				TypeText("");

				TypeText("Commander: Was ist mit dem Countdown?");
				Thread.Sleep(300);

				TypeText("KADE: Unklar. Wir wissen weder, ob die Waffe ausgelöst wurde…");
				TypeText("KADE: …noch, ob der Vorgang gestoppt wurde oder außer Kontrolle gerät.");
				TypeText("");

				TypeText("Oduro: Captain… wenn die Superwaffe nicht zündet, dann wird die Erde weiterhin von Milliarden Infizierter überrollt.");
				TypeText("Oduro: Sie haben unsere Technologie infiltriert, können das auch mit der, der Atlanter. Sie wären in der Lage selber Schiffe zu bauen.");
				TypeText("Oduro: Sie könnten die ganze Galaxie überfluten.");
				TypeText("Oduro: Eine Rückkehr oder spätere Kolonisierung wäre dann ebenfalls unmöglich.");
				TypeText("");

				TypeText("ARGOS: „GENESIS, wir warten auf Ihre Einschätzung. Was ist unser nächster Schritt?“");
				TypeText("");

				TypeText("Commander: Optionen?");
				Thread.Sleep(300);

				Console.WriteLine("1) Verbindung zur Atlaner-Waffe erneut aufbauen");
				Console.WriteLine("2) Rückkehr zur Erde, Risiko einer Kontamination");
				Console.WriteLine("3) Sofortiger Aufbruch zum geplanten Fluchtpunkt");
				Console.Write("\nAuswahl: ");

				string auswahl = Console.ReadLine();
				SaveGame("Scene_Entscheidung_Atlaner");

				
				// ERSTE ENTSCHEIDUNG
				

				switch (auswahl)
				{
					case "1":
						Scene_WaffeKontakt();
						return;

					case "2":
						Scene_RueckkehrZurErde();
						return;

					case "3":
						GameOver(
							"Der sofortige Aufbruch verhindert, dass die Atlaner-Superwaffe aktiviert wird.\n" +
							"Der Nanovirus breitet sich ungehindert aus und zerstört jede Zukunft der Menschheit."
						);
						return;

					default:
						TypeText("Ungültige Eingabe.");
						Thread.Sleep(300);
						// Wieder hierher zurück
						Scene_HeliosNachKampf();
						return;
				}


				
				// SZENE: VERBINDUNG ZUR ATLANTER-WAFFE NOCHMAL AUFBAUEN
				
				static void Scene_WaffeKontakt()
				{
					Console.Clear();
					SaveGame("Scene_WaffeKontakt");

					TypeText("KADE: Versuche, die Verbindung zur Atlaner-Superwaffe wieder aufzubauen...");
					Thread.Sleep(700);

					Console.Beep(350, 200);
					Console.Beep(300, 200);

					TypeText("KADE: Keine Verbindung möglich. Die Hyperlink-Struktur ist instabil.");
					TypeText("KADE: Wir erhalten keinerlei Rückmeldung von der Waffe.");
					TypeText("");

					// Zweites Entscheidungsmenü
					Console.WriteLine("(1) Rückkehr zur Erde – Risiko einer Kontamination");
					Console.WriteLine("(2) Sofortiger Aufbruch (HOCHGEFÄHRLICH)");
					Console.Write("\nAuswahl: ");

					string choice = Console.ReadLine();
					SaveGame("Scene_WaffeKontakt");

					switch (choice)
					{
						case "1":
							Scene_RueckkehrZurErde();
							return;

						case "2":
							GameOver(
								"Ohne aktive Superwaffe breitet sich der Nanovirus weiter aus.\n" +
								"Der Aufbruch war eine fatale Entscheidung. Die Menschheit geht unter."
							);
							return;

						default:
							TypeText("Ungültige Eingabe.");
							Thread.Sleep(300);
							Scene_WaffeKontakt();
							return;
					}
				}


				
				// SZENE: RÜCKKEHR ZUR ERDE (RICHTIGE ENTSCHEIDUNG)
				
				static void Scene_RueckkehrZurErde()
				{
					Console.Clear();
					SaveGame("Scene_RueckkehrZurErde");

					TypeText("Oduro: Kurs auf die Erde setzen. Schilde stabilisieren.");
					TypeText("KADE: Nanovirus-Signaturen weiterhin extrem hoch…");
					TypeText("");

					TypeText("ARGOS: GENESIS, wir fliegen an eurer Seite. Wir müssen wissen, ob die Superwaffe versagt hat.");
					TypeText("");

					TypeText("Commander: Haltet alle Sensoren auf maximale Empfindlichkeit. Wir dürfen uns keinen Fehler leisten.");
					TypeText("");

					TypeText("Fortsetzung folgt...");
					Console.ReadKey();
				}
			}
		}
	}
}



