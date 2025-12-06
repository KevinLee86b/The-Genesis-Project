using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json;

namespace The_Genesis_Projekt
{
	internal class Program
	{
		// Spielername
		static string playerName = "";
		// letzter gespeicherter Szenenname
		static string lastScene = "Scene_Prolog";
		// JSON-Speicherdatei
		static string saveFile = "savegame.json";

		// Datenstruktur für JSON
		class SaveData
		{
			public string PlayerName { get; set; }
			public string LastScene { get; set; }
		}

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
			Console.WriteLine("2) Weiterspielen (letzter Speicherstand)");
			Console.WriteLine("3) Kapitel auswählen");
			Console.WriteLine("4) Beenden");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					StartNewGame();
					break;

				case "2":
					if (TryLoadGame())
					{
						Console.WriteLine($"Spiel geladen. Willkommen zurück, Commander {playerName}!");
						Thread.Sleep(800);
						ContinueFromScene();
					}
					else
					{
						Console.WriteLine("Kein gültiger Speicherstand gefunden.");
						Console.ReadKey();
						MainMenu();
					}
					break;

				case "3":
					ChapterSelectMenu();
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

			SaveGame("Scene_Prolog");
			ShowProlog();
		}


		// SPEICHERN & LADEN (JSON)


		static void SaveGame(string sceneName)
		{
			lastScene = sceneName;
			var data = new SaveData
			{
				PlayerName = playerName,
				LastScene = lastScene
			};

			try
			{
				string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(saveFile, json);
			}
			catch
			{
				// Wenn Speichern schiefgeht, Spiel soll trotzdem weiterlaufen
			}
		}

		static bool TryLoadGame()
		{
			if (!File.Exists(saveFile))
				return false;

			try
			{
				string json = File.ReadAllText(saveFile);
				var data = JsonSerializer.Deserialize<SaveData>(json);

				if (data == null || string.IsNullOrWhiteSpace(data.LastScene))
					return false;

				playerName = data.PlayerName ?? "";
				lastScene = data.LastScene;
				return true;
			}
			catch
			{
				return false;
			}
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

				// Diese Szenen hängen alle an HeliosNachKampf / Entscheidungsmenü
				case "Scene_HeliosNachKampf":
				case "Scene_Entscheidung_Atlaner":
				case "Scene_WaffeKontakt":
				case "Scene_RueckkehrZurErde":
					Scene_HeliosNachKampf();
					break;

				case "Scene_ZerstoererKampf": Scene_ZerstoererKampf(); break;
				case "Scene_NachZerstoerer": Scene_NachZerstoerer(); break;
				case "Scene_AtlanerMission_Start": Scene_AtlanerMission_Start(); break;

				default:
					Console.WriteLine("Unbekannte Szene im Speicherstand. Zurück zum Hauptmenü.");
					Console.ReadKey();
					MainMenu();
					break;
			}
		}


		// KAPITEL-MENü

		static void ChapterSelectMenu()
		{
			Console.Clear();
			Console.WriteLine("=== Kapitel auswählen ===\n");
			Console.WriteLine("1) Prolog");
			Console.WriteLine("2) Intro");
			Console.WriteLine("3) Alarm – Transportsphäre");
			Console.WriteLine("4) Kapitel 1 – Flucht von der Erde");
			Console.WriteLine("5) Helios-Kontakt");
			Console.WriteLine("6) Nach Helios-Kampf / Atlanter-Entscheidung");
			Console.WriteLine("7) Zerstörerkampf");
			Console.WriteLine("8) Blindfeuer-Gefecht im Trümmerfeld");
			Console.WriteLine("9) Atlanter-Bodenmission");
			Console.WriteLine("10) Zurück zum Hauptmenü");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					SaveGame("Scene_Prolog");
					ShowProlog();
					break;
				case "2":
					SaveGame("Scene_Intro");
					Scene_Intro();
					break;
				case "3":
					SaveGame("Scene_Alarm");
					Scene_Alarm();
					break;
				case "4":
					SaveGame("Scene_Kapitel1");
					Scene_Kapitel1();
					break;
				case "5":
					SaveGame("Scene_HeliosKontakt");
					Scene_HeliosKontakt();
					break;
				case "6":
					SaveGame("Scene_HeliosNachKampf");
					Scene_HeliosNachKampf();
					break;
				case "7":
					SaveGame("Scene_ZerstoererKampf");
					Scene_ZerstoererKampf();
					break;
				case "8":
					SaveGame("Scene_NachZerstoerer");
					Scene_NachZerstoerer();
					break;
				case "9":
					SaveGame("Scene_AtlanerMission_Start");
					Scene_AtlanerMission_Start();
					break;
				case "10":
					MainMenu();
					break;
					
				default:
					MainMenu();
					break;
			}
		}
		// TYPEWRITE
		static void TypeText(string text, int delay = 18)
		{
			bool skip = false;

			for (int i = 0; i < text.Length; i++)
			{
				if (!skip && Console.KeyAvailable)
				{
					Console.ReadKey(true);
					skip = true;
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

			while (Console.KeyAvailable)
				Console.ReadKey(true);
		}
		// PROLOG SZENE
		static void ShowProlog()
		{
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
		// INTRO SZENE + SCHIFFSGRAFIK
		static void Scene_Intro()
		{
			SaveGame("Scene_Intro");

			Console.Clear();
			DrawGenesisTop();
			Console.WriteLine();
			Thread.Sleep(500);

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
			Console.WriteLine("2) Beschleunigen und abfangen!");
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

		static void Scene_Alarm_Abfangen()
		{
			Console.Clear();

			TypeText("Abfangmanöver eingeleitet! Hilfstriebwerke zuschalten!");
			Thread.Sleep(600);

			TypeText("Wir verringern den Abstand… Commander, wie weiter fortfahren? Ich vermute sie wurden infiziert. Gebe aber keine Garantie!");
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
				Console.Clear();
				DrawFiringGraphic();
				Console.Beep(600, 150);
				Console.Beep(650, 150);
				Console.Beep(700, 200);
				Thread.Sleep(1000);

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
		// GAME OVER
		static void GameOver(string message)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;

			TypeText("=== GAME OVER ===");
			TypeText("");
			TypeText(message);

			Console.ResetColor();
			TypeText("\nWas möchtest du tun?");
			Console.WriteLine("1) Letzte Szene wiederholen");
			Console.WriteLine("2) Kapitel auswählen");
			Console.WriteLine("3) Hauptmenü");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					if (TryLoadGame())
					{
						ContinueFromScene();
					}
					else
					{
						Console.WriteLine("Kein gültiger Speicherstand gefunden.");
						Console.ReadKey();
						MainMenu();
					}
					break;
				case "2":
					ChapterSelectMenu();
					break;
				default:
					MainMenu();
					break;
			}
		}
		// GRAFIKEN / SCHIFFSGRAFIK
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
		// KAPITEL 1 – FLUCHT VON DER ERDE
		static void Scene_Kapitel1()
		{
			SaveGame("Scene_Kapitel1");

			Console.Clear();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("===================================================");
			Console.WriteLine("                    KAPITEL 1");
			Console.WriteLine("                 FLUCHT VON DER ERDE");
			Console.WriteLine("===================================================");
			Console.ResetColor();
			Console.WriteLine();

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

			DrawLiftPath();
			TypeText("");
			TypeText("Die Türen schlossen sich. Der Lift summte, als er sich nach oben bewegte.");
			TypeText("Deck um Deck zog in Sekundenschnelle an mir vorbei, als wären es nur Kapitel eines Buches, das ich nicht mehr umschreiben konnte.");
			TypeText("");

			TypeText("Ein Signalton. Die Türen öffneten sich zur Brücke der GENESIS.");
			TypeText("");

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

		static void DrawDockToBridge()
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine(" [Erde-Orbit Dock]");
			Console.WriteLine("      /‾‾‾\\");
			Console.WriteLine("     |FÄHRE|  ----->  [ANDOCKRING GENESIS]");
			Console.WriteLine("      \\___/");
			Console.ResetColor();
		}

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
		// SZENE HELIOS-KONTAKT
		static void Scene_HeliosKontakt()
		{
			SaveGame("Scene_HeliosKontakt");

			Console.Clear();

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

			// Lokale Funktionen

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

				Random rnd = new Random();
				string[] module = { "A", "B", "C", "D", "E" };
				string fehlerSymbol = "X";
				int fehlerPosition = rnd.Next(0, module.Length);

				string[] kreis = new string[module.Length];
				for (int i = 0; i < module.Length; i++)
					kreis[i] = module[i];

				kreis[fehlerPosition] = fehlerSymbol;

				while (true)
				{
					Console.Clear();
					TypeText("=== SCHALTKREIS-REPARATUR – FEHLER FINDEN ===", 10);
					Console.WriteLine();

					TypeText("KADE: Ein Modul im Datenpfad ist beschädigt. Finden Sie das fehlerhafte Element!", 5);
					Console.WriteLine();

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

					if (int.TryParse(input, out int pos))
					{
						pos--;

						if (pos == fehlerPosition)
						{
							TypeText("\nKADE: ✔ Fehler erkannt! Schaltkreis wird neu geroutet…", 10);
							Console.Beep(800, 200);

							Thread.Sleep(800);

							TypeText("⚡ Override erfolgreich! Waffen- und Schildsysteme sind wieder online!", 10);
							TypeText("");

							Thread.Sleep(600);

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

				static void Scene_HeliosGefahr()
				{
					Console.Clear();

					Console.Beep(400, 600);
					Console.Beep(350, 600);
					Console.Beep(300, 600);

					TypeText("⚠️  ALARM!  Die HELIOS fährt ihre Waffensysteme hoch!");
					TypeText("⚠️  Infizierte übernehmen die Kontrolle des Schiffes!");
					TypeText("⚠️  Wir mmüssen sofort angreifen. Der einzige Vorteil ist das Sie noch nicht wissen wie alle Systeme funktionieren!");
					TypeText("⚠️  Sie wissen nicht wie alle Systeme auf 100 Prozent laufen, die KI des Schiffes wird sie zu beginn noch bremsen können.");
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

					Scene_HeliosKampf();
				}

				static void Scene_HeliosKampf()
				{
					Console.Clear();

					Raumschiff genesis = new Raumschiff("GENESIS", 200, 200, 55);
					Raumschiff helios = new Raumschiff("HELIOS", 100, 200, 45);

					TypeText("⚔️   KAMPF BEGINNT — GENESIS vs. HELIOS  ⚔️", 10);
					Thread.Sleep(800);

					int runde = 1;

					while (!genesis.Zerstoert && !helios.Zerstoert)
					{
						Console.Clear();
						TypeText($"===== RUNDE {runde} =====", 5);
						Console.WriteLine();

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
			}
		}

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
		// SZENE HELIOS NACH KAMPF
		static void Scene_HeliosNachKampf()
		{
			Console.Clear();
			SaveGame("Scene_HeliosNachKampf");

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
					Scene_HeliosNachKampf();
					return;
			}

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

				Scene_ZerstoererKampf();
				return;
			}
		}
		// SZENE ZERSTÖRERKAMPF
		static void Scene_ZerstoererKampf()
		{
			Console.Clear();
			SaveGame("Scene_ZerstoererKampf");

			SchiffBasis genesis = new SchiffBasis("GENESIS", schild: 0, huelle: 160, angriff: 28);
			SchiffBasis argos = new SchiffBasis("ARGOS", schild: 200, huelle: 200, angriff: 26);
			Zerstoerer boss = new Zerstoerer("ZERSTÖRER");

			TypeText("Brücke – GENESIS");
			TypeText("");
			TypeText("KADE: Commander! Neuer Kontakt im Trümmerfeld – schwer bewaffnetes Schiff nähert sich!");
			TypeText("Oduro: Er ist kleiner als die HELIOS, aber stark gepanzert. Signatur: Ein Zerstörer. Alles sind infiziert…");
			TypeText("Kade: Er hat sich vermutlich hinter der Helios versteckt.");
			TypeText("");
			TypeText("Commander: Auf den Schirm! Alle Systeme auf Gefechtsbereitschaft!");
			TypeText("");

			Thread.Sleep(800);

			bool ausweichenAktiv = false;
			Random rnd = new Random();

			while (!genesis.Zerstoert && !argos.Zerstoert && !boss.Zerstoert)
			{
				Console.Clear();
				ZeichneKampfSzene(genesis, argos, boss);
				Console.WriteLine();
				ZeigeFlottenStatus(genesis, argos, boss);
				Console.WriteLine();

				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("=== TAKTISCHE OPTIONEN GEGEN DEN ZERSTÖRER ===");
				Console.ResetColor();
				Console.WriteLine("[1] Torpedosalve (GENESIS + ARGOS)");
				Console.WriteLine("[2] Partikelstrahlen – konzentrierter Beschuss");
				Console.WriteLine("[3] Ausweichmanöver – ARGOS deckt die GENESIS");
				Console.WriteLine("[4] ARGOS-Schilde verstärken");
				Console.WriteLine("[5] Nur Status aktualisieren");
				Console.Write("\nAuswahl: ");

				string choice = Console.ReadLine();
				Console.WriteLine();

				ausweichenAktiv = false;

				switch (choice)
				{
					case "1":
						TypeText("Commander: Alle Torpedowerfer – ZIEL ZERSTÖRER! Feuer!", 10);
						TorpedoSalveVonFlotte();
						int torpedoSchaden = 50 + rnd.Next(10, 21);
						boss.SchadenErleiden(torpedoSchaden);
						TypeText($"Treffer! Der Zerstörer verliert {torpedoSchaden} Strukturpunkte!", 10);
						break;

					case "2":
						TypeText("Commander: Partikelstrahlen bündeln – volle Energie auf seine Schwachstellen!", 10);
						PartikelstrahlFlotte();
						int strahlenSchaden = 40 + rnd.Next(5, 26);
						boss.SchadenErleiden(strahlenSchaden);
						TypeText($"Die gebündelten Strahlen reißen die Panzerung des Zerstörers auf ({strahlenSchaden} Schaden).", 10);
						break;

					case "3":
						TypeText("Commander: GENESIS aus der Schusslinie – ARGOS, decken Sie uns!", 10);
						AusweichAnimation();
						ausweichenAktiv = true;
						break;

					case "4":
						TypeText("Oduro: Schilde der ARGOS werden verstärkt…", 10);
						Console.Beep(500, 120);
						Console.Beep(650, 120);
						int schildBoost = 25;
						argos.Schild += schildBoost;
						if (argos.Schild > argos.MaxSchild) argos.Schild = argos.MaxSchild;
						TypeText($"ARGOS-Schilde steigen um {schildBoost} Punkte.", 10);
						break;

					case "5":
						TypeText("Taktische Übersicht wird aktualisiert…", 10);
						break;

					default:
						TypeText("Ungültige Eingabe. Die Crew zögert – wertvolle Sekunden vergehen!", 10);
						break;
				}

				if (boss.Zerstoert) break;

				Thread.Sleep(700);
				TypeText("ATLANTER-ZERSTÖRER: Waffensysteme laden – gegnerisches Feuer kommt!", 10);
				PartikelstrahlGegner();

				int bossDamage = boss.Angriff + rnd.Next(-5, 11);
				if (bossDamage < 15) bossDamage = 15;

				SchiffBasis ziel;

				if (ausweichenAktiv)
				{
					ziel = argos;
					TypeText("ARGOS schiebt sich zwischen GENESIS und den Beschuss!", 10);
				}
				else
				{
					ziel = (rnd.Next(100) < 70) ? genesis : argos;
				}

				WendeSchadenAn(ziel, bossDamage);

				if (genesis.Zerstoert)
				{
					CinematicExplosionGenesis();
					GameOver("Die GENESIS wurde im Feuer des Zerstörers zerrissen – die letzte Hoffnung der Menschheit erlischt.");
					return;
				}

				if (argos.Zerstoert)
				{
					CinematicExplosionArgos();
					GameOver("Die ARGOS wird zerstört, bevor sie die GENESIS weiter schützen kann. Die Flotte zerbricht.");
					return;
				}
			}

			Console.Clear();
			CinematicExplosionZerstoerer();
			TypeText("Der Zerstörer bricht auseinander. Trümmer und brennende Fragmente treiben durch den Raum.", 10);
			Thread.Sleep(800);

			TypeText("Ich atme schwer aus, während die Explosion alle Winkel des Bildschirms erreichen.", 10);
			TypeText("Für einen Moment wirkt der Weltraum wie ein brennender Sturm aus Licht.", 10);
			TypeText("");

			TypeText("Commander: Verbindung zur ARGOS.", 10);
			TypeText("ARGOS-Captain Ren: Wir siehts bei euch aus GENESIS? Status des Ziels?", 10);
			TypeText("Commander: Zerstörer vernichtet. Aber wir wurden vom Orbit der Erde abgedriftet seit den Kämpfen.", 10);
			TypeText("Commander: ARGOS, nehmen Sie sofort Kurs auf die Erde. Schauen sie nach der Waffe, warum sie nicht gezündet hat, bis wir nachrücken.", 10);
			TypeText("Ren: Verstanden. ARGOS kehrt zur Erde zurück.", 10);
			TypeText("");

			TypeText("Interne Kommunikation – alle Maschinendecks.", 10);
			TypeText("Chefingenieur: Antrieb wird stabilisiert. Schildgitter in Rekalibrierung. Sensoren sind seit dem letzten Treffer schwer beschädigt.", 10);
			TypeText("Commander: Priorität auf Antrieb und Hülle. Wir müssen rasch aufschließen.", 10);
			TypeText("");

			TypeText("KADE: Commander… neue Bewegungen im Trümmerfeld.", 10);
			TypeText("Commander: Auf den Schirm.", 10);
			TypeText("");

			TypeText("Oduro: Ich zähle zwei schwere Bomber, eine Fregatte und ein Landungsschiff.", 10);
			TypeText("Oduro: Sie müssen vor der Explosion der HELIOS aus dem Hangar entkommen sein.", 10);
			TypeText("KADE: Die ARGOS ist bereits außer Reichweite. Wir sind allein.", 10);
			TypeText("Oduro: Unsere Schilde sind noch offline und die Sensorphalanx ist teilweise ausgefallen.", 10);
			TypeText("KADE: Wir bekommen keine exakten Zielerfassungen mehr.", 10);
			TypeText("");

			TypeText("Commander: Dann feuern wir auf Sicht. Ohne richtige Zielerfassung wird das ein riesen Spaß.", 10);
			TypeText("Commander: Legen Sie mir das Trümmerfeld als taktisches Gitter auf den Hauptschirm. Uns bleibt nichts anderes übrig.", 10);
			TypeText("Commander: Wir haben die Intelligenz des Nanoviruses weit unterschätzt. Sie wussten ganz genau was wir vor hatten.", 10);
			TypeText("");

			Scene_NachZerstoerer();
		}
		// HILFSMETHODEN KAMPF/ZERSTÖRER
		static void WendeSchadenAn(SchiffBasis ziel, int damage)
		{
			int vorherSchild = ziel.Schild;
			int vorherHuelle = ziel.Huelle;

			ziel.SchadenErleiden(damage);

			int deltaSchild = vorherSchild - ziel.Schild;
			int deltaHuelle = vorherHuelle - ziel.Huelle;

			if (deltaSchild < 0) deltaSchild = 0;
			if (deltaHuelle < 0) deltaHuelle = 0;

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write($"Treffer auf {ziel.Name}: ");
			Console.ResetColor();

			List<string> teile = new List<string>();
			if (deltaSchild > 0) teile.Add($"Schild -{deltaSchild}");
			if (deltaHuelle > 0) teile.Add($"Hülle -{deltaHuelle}");

			Console.WriteLine(string.Join(", ", teile));

			if (ziel.Zerstoert)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{ziel.Name} wird tödlich getroffen!");
				Console.ResetColor();
			}

			Thread.Sleep(500);
		}

		static void ZeichneKampfSzene(SchiffBasis genesis, SchiffBasis argos, SchiffBasis boss)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("                         [ GENESIS ]");
			Console.WriteLine("                        _______________");
			Console.WriteLine("                _______/               \\______");
			Console.WriteLine("             __/                               \\__");
			Console.WriteLine("           _/                                     \\_");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                         [ ARGOS ]");
			Console.WriteLine("                      ___/_______\\___");
			Console.WriteLine("                   __/               \\__");
			Console.WriteLine("                 _/                     \\_");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\n                                      [ ZERSTÖRER ]");
			Console.WriteLine("                                      ____===[###]===____");
			Console.WriteLine("                                  ___/                    \\___");
			Console.WriteLine("                                 /                            \\");
			Console.WriteLine("                                |   ███████    ███████         |");
			Console.WriteLine("                                 \\__                      ____/");
			Console.ResetColor();
		}

		static void ZeigeFlottenStatus(SchiffBasis genesis, SchiffBasis argos, SchiffBasis boss)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"GENESIS   - Schild: {genesis.Schild}/{genesis.MaxSchild}, Hülle: {genesis.Huelle}/{genesis.MaxHuelle}");
			Console.WriteLine($"ARGOS     - Schild: {argos.Schild}/{argos.MaxSchild}, Hülle: {argos.Huelle}/{argos.MaxHuelle}");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"ZERSTÖRER - Schild: {boss.Schild}/{boss.MaxSchild}, Hülle: {boss.Huelle}/{boss.MaxHuelle}");
			Console.ResetColor();
		}

		static void CinematicExplosionGenesis()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("          *** GENESIS EXPLODIERT ***");
			Console.WriteLine("             * * * * * * * * *");
			Console.WriteLine("          ************************");
			Console.WriteLine("       ********  G  E  N  E  S  I  S  ********");
			Console.WriteLine("          ************************");
			Console.WriteLine("             * * * * * * * * *");
			Console.ResetColor();
			Thread.Sleep(1200);
		}

		static void CinematicExplosionArgos()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("          *** ARGOS WIRD ZERISSEN ***");
			Console.WriteLine("             * * * * * * * *");
			Console.WriteLine("          **********************");
			Console.WriteLine("       ********  A  R  G  O  S  ********");
			Console.WriteLine("          **********************");
			Console.WriteLine("             * * * * * * * *");
			Console.ResetColor();
			Thread.Sleep(1200);
		}

		static void CinematicExplosionZerstoerer()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("               *** ZERSTÖRER EXPLODIERT ***");
			Console.WriteLine("                  * * * * * * * * *");
			Console.WriteLine("              ***************************");
			Console.WriteLine("           ********   B  O  O  O  M   ********");
			Console.WriteLine("              ***************************");
			Console.WriteLine("                  * * * * * * * * *");
			Console.ResetColor();
			Thread.Sleep(1200);
		}

		static void TorpedoSalveVonFlotte()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("GENESIS + ARGOS: >>>=====> >>>=====> >>>=====>");
			Console.ResetColor();
			Console.Beep(900, 90);
			Console.Beep(1000, 80);
			Thread.Sleep(200);
		}

		static void PartikelstrahlFlotte()
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("GENESIS:  |||||||||||||||||||||||||||>");
			Console.WriteLine("ARGOS:    |||||||||||||||||||||||||||>");
			Console.ResetColor();
			Console.Beep(700, 150);
			Console.Beep(850, 180);
			Thread.Sleep(250);
		}

		static void PartikelstrahlGegner()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("ZERSTÖRER: <#########################");
			Console.ResetColor();
			Console.Beep(500, 180);
			Console.Beep(400, 200);
			Thread.Sleep(250);
		}

		static void AusweichAnimation()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("GENESIS weicht aus… ARGOS verändert Vektor und deckt das Flaggschiff.");
			Console.ResetColor();
			Console.Beep(300, 80);
			Console.Beep(350, 80);
			Console.Beep(400, 80);
			Thread.Sleep(300);
		}

		static void TorpedoSalveSichtmodus()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(">>>=====>    >>>=====>    >>>=====>");
			Console.ResetColor();
			Console.Beep(900, 80);
			Console.Beep(950, 80);
			Thread.Sleep(200);
		}

		static void PartikelSalveKleinschiffe()
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("<-*-*-*-*-*   <-*-*-*-*-*   <-*-*-*-*-*");
			Console.ResetColor();
			Console.Beep(600, 90);
			Console.Beep(550, 90);
			Thread.Sleep(200);
		}

		static void KleineExplosionImGitter(int row, int col)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"Explosion bei Feld ({(char)('A' + row)}{col + 1})!");
			Console.ResetColor();
			Console.Beep(700, 120);
			Console.Beep(500, 150);
			Thread.Sleep(250);
		}
		// HILFSKLASSE FÜR BLINDFEUER
		class FeindSchiff
		{
			public string Name;
			public char Symbol;
			public int TrefferZumVersenken;
			public int Treffer;
			public List<(int Row, int Col)> Zellen = new List<(int, int)>();

			public bool Lebendig => Treffer < TrefferZumVersenken;

			public bool EnthältZelle(int r, int c)
			{
				foreach (var z in Zellen)
				{
					if (z.Row == r && z.Col == c) return true;
				}
				return false;
			}
		}
		// BLINDFEUER-MINISPIEL
		static void Scene_NachZerstoerer()
		{
			Console.Clear();
			SaveGame("Scene_NachZerstoerer");

			TypeText("Brücke – GENESIS, wenige Minuten nach der Zerstörung des Zerstörers.", 10);
			TypeText("Das Licht der Explosion verblasst, doch das Trümmerfeld bleibt – ein chaotisches Meer aus Metall.", 10);
			TypeText("");

			TypeText("KADE: Commander, die Bomber und die Fregatte nutzen das Trümmerfeld als Deckung.", 10);
			TypeText("KADE: Das Landungsschiff hält sich zurück, wahrscheinlich voller Infizierter.", 10);
			TypeText("Oduro: Unsere Schilde bleiben offline. Wir haben nur noch Hülle und Glück.", 10);
			TypeText("Commander: Dann kämpfen wir mit dem, was uns bleibt.", 10);
			TypeText("Commander: Blindfeuer-Modus aktivieren. Koordinatengitter auf den Hauptschirm!", 10);
			Console.WriteLine();

			SchiffBasis genesis = new SchiffBasis("GENESIS", schild: 0, huelle: 160, angriff: 0);

			const int rows = 20;
			const int cols = 30;
			char[,] grid = new char[rows, cols];

			for (int r = 0; r < rows; r++)
				for (int c = 0; c < cols; c++)
					grid[r, c] = '.';

			List<FeindSchiff> feinde = new List<FeindSchiff>();

			FeindSchiff fregatte = new FeindSchiff
			{
				Name = "FREGATTE",
				Symbol = 'F',
				TrefferZumVersenken = 2,
				Treffer = 0
			};
			int frRow = 5;
			int frColStart = 10;
			for (int c = frColStart; c < frColStart + 6; c++)
			{
				fregatte.Zellen.Add((frRow, c));
				grid[frRow, c] = fregatte.Symbol;
			}
			feinde.Add(fregatte);

			FeindSchiff bomber1 = new FeindSchiff
			{
				Name = "TORPEDO-BOMBER 1",
				Symbol = 'B',
				TrefferZumVersenken = 1,
				Treffer = 0
			};
			int b1Row = 10;
			int b1ColStart = 4;
			for (int c = b1ColStart; c < b1ColStart + 3; c++)
			{
				bomber1.Zellen.Add((b1Row, c));
				grid[b1Row, c] = bomber1.Symbol;
			}
			feinde.Add(bomber1);

			FeindSchiff bomber2 = new FeindSchiff
			{
				Name = "TORPEDO-BOMBER 2",
				Symbol = 'B',
				TrefferZumVersenken = 1,
				Treffer = 0
			};
			int b2Row = 13;
			int b2ColStart = 22;
			for (int c = b2ColStart; c < b2ColStart + 3; c++)
			{
				bomber2.Zellen.Add((b2Row, c));
				grid[b2Row, c] = bomber2.Symbol;
			}
			feinde.Add(bomber2);

			FeindSchiff lander = new FeindSchiff
			{
				Name = "LANDUNGSSCHIFF",
				Symbol = 'L',
				TrefferZumVersenken = 1,
				Treffer = 0
			};
			int lRow = 15;
			int lColStart = 14;
			for (int dr = 0; dr < 2; dr++)
			{
				for (int dc = 0; dc < 4; dc++)
				{
					int rr = lRow + dr;
					int cc = lColStart + dc;
					lander.Zellen.Add((rr, cc));
					grid[rr, cc] = lander.Symbol;
				}
			}
			feinde.Add(lander);

			Random rnd = new Random();

			while (!genesis.Zerstoert && feinde.Exists(f => f.Lebendig))
			{
				Console.Clear();
				ZeichneBlindfeuerBildschirm(grid, rows, cols, genesis);

				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Hinweis: F = Fregatte, B = Bomber, L = Landungsschiff");
				Console.WriteLine("        X = zerstört, o = Fehlschuss");
				Console.ResetColor();
				Console.WriteLine();

				Console.Write("Zielkoordinate eingeben (z.B. C12, X zum Abbrechen): ");
				string eingabe = Console.ReadLine().Trim().ToUpper();

				if (eingabe == "X")
				{
					TypeText("Commander: Gefecht abgebrochen. Hoffen wir, dass wir das nicht bereuen…", 10);
					return;
				}

				if (eingabe.Length < 2 || eingabe.Length > 3)
				{
					TypeText("KADE: Ungültiges Format. Buchstabe + Zahl, Commander. (z.B. C12)", 10);
					Thread.Sleep(600);
					continue;
				}

				char rowChar = eingabe[0];
				if (rowChar < 'A' || rowChar >= 'A' + rows)
				{
					TypeText("KADE: Diese Zeile existiert nicht im Gitter.", 10);
					Thread.Sleep(600);
					continue;
				}

				if (!int.TryParse(eingabe.Substring(1), out int colNum))
				{
					TypeText("KADE: Das ist keine gültige Zahl.", 10);
					Thread.Sleep(600);
					continue;
				}

				int rIndex = rowChar - 'A';
				int cIndex = colNum - 1;

				if (cIndex < 0 || cIndex >= cols)
				{
					TypeText("KADE: Spalte liegt außerhalb unseres Sichtbereichs.", 10);
					Thread.Sleep(600);
					continue;
				}

				char feld = grid[rIndex, cIndex];
				if (feld == 'X' || feld == 'o')
				{
					TypeText("Oduro: Diese Koordinate haben wir bereits beschossen.", 10);
					Thread.Sleep(600);
					continue;
				}

				Console.WriteLine();
				TypeText("Torpedos unterwegs…", 10);
				TorpedoSalveSichtmodus();

				FeindSchiff getroffenesSchiff = null;
				foreach (var f in feinde)
				{
					if (f.Lebendig && f.EnthältZelle(rIndex, cIndex))
					{
						getroffenesSchiff = f;
						break;
					}
				}

				if (getroffenesSchiff != null)
				{
					getroffenesSchiff.Treffer++;

					if (getroffenesSchiff.Treffer >= getroffenesSchiff.TrefferZumVersenken)
					{
						foreach (var z in getroffenesSchiff.Zellen)
						{
							grid[z.Row, z.Col] = 'X';
						}

						KleineExplosionImGitter(rIndex, cIndex);
						TypeText($"Treffer! {getroffenesSchiff.Name} wurde vernichtet!", 10);
					}
					else
					{
						grid[rIndex, cIndex] = 'X';
						TypeText("Solider Treffer! Die Fregatte hält noch, aber ihre Hülle bricht auf!", 10);
					}
				}
				else
				{
					grid[rIndex, cIndex] = 'o';
					TypeText("Fehlschuss – nur Trümmer und leere Dunkelheit.", 10);
				}

				Thread.Sleep(500);

				if (feinde.Exists(f => f.Lebendig))
				{
					Console.WriteLine();
					TypeText("Feindliche Kleinverbände eröffnen das Gegenfeuer auf die GENESIS!", 10);
					PartikelSalveKleinschiffe();

					int gesamtschaden = 0;
					foreach (var f in feinde)
					{
						if (!f.Lebendig) continue;

						switch (f.Symbol)
						{
							case 'F':
								gesamtschaden += 5;
								break;
							case 'B':
								gesamtschaden += 4;
								break;
							case 'L':
								gesamtschaden += 2;
								break;
						}
					}

					if (gesamtschaden < 2) gesamtschaden = 2;

					WendeSchadenAn(genesis, gesamtschaden);

					if (genesis.Zerstoert)
					{
						CinematicExplosionGenesis();
						GameOver("Die GENESIS wird im Blindfeuer-Gefecht zerfetzt. Niemand bleibt zurück, um die Erde zu retten.");
						return;
					}
				}
			}

			Console.Clear();
			ZeichneBlindfeuerBildschirm(grid, rows, cols, genesis);
			Console.WriteLine();

			TypeText("KADE: Commander… keine feindlichen Signaturen mehr. Wir haben das Feld bereinigt.", 10);
			TypeText("Oduro: GENESIS ist schwer beschädigt, aber wir leben noch.", 10);
			TypeText("Commander: Reparaturteams auf alle Decks. Wir kehren zur Erde zurück – egal, was uns dort erwartet.", 10);
			Console.WriteLine();
			TypeText("Fortsetzung folgt…", 10);
			Console.ReadKey();

			SaveGame("Scene_CaptainRest");
			Scene_CaptainRest();
			return;
		}

		static void ZeichneBlindfeuerBildschirm(char[,] grid, int rows, int cols, SchiffBasis genesis)
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("============== TAKTISCHES HAUPTDISPLAY – BLINDFEUER-MODUS ==============");
			Console.ResetColor();

			Console.WriteLine($"GENESIS – Hülle: {genesis.Huelle}/{genesis.MaxHuelle} | Schilde: OFFLINE");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("     ");
			for (int c = 1; c <= cols; c++)
				Console.Write($"{c,2} ");
			Console.WriteLine();
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("    +");
			for (int c = 0; c < cols; c++)
				Console.Write("--");
			Console.WriteLine("+");
			Console.ResetColor();

			for (int r = 0; r < rows; r++)
			{
				char rowLabel = (char)('A' + r);
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write($" {rowLabel}  |");
				Console.ResetColor();

				for (int c = 0; c < cols; c++)
				{
					char ch = grid[r, c];

					switch (ch)
					{
						case 'F':
							Console.ForegroundColor = ConsoleColor.Red;
							break;
						case 'B':
							Console.ForegroundColor = ConsoleColor.Yellow;
							break;
						case 'L':
							Console.ForegroundColor = ConsoleColor.Cyan;
							break;
						case 'X':
							Console.ForegroundColor = ConsoleColor.Green;
							break;
						case 'o':
							Console.ForegroundColor = ConsoleColor.DarkGray;
							break;
						default:
							Console.ForegroundColor = ConsoleColor.DarkGray;
							break;
					}

					Console.Write($" {ch}");
				}

				Console.ResetColor();
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine(" |");
				Console.ResetColor();
			}

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("    +");
			for (int c = 0; c < cols; c++)
				Console.Write("--");
			Console.WriteLine("+");
			Console.ResetColor();
		}
		static void Scene_CaptainRest()

		{
			// Autosave für diese Szene
			SaveGame("Scene_CaptainRest");

			Console.Clear();
			TypeText("Die Brücke der GENESIS wirkt plötzlich größer, jetzt da das Feuer verstummt ist.", 12);
			TypeText("Für einen Moment sagt niemand etwas. Nur das leise Knacken überhitzter Paneele ist zu hören.", 12);
			TypeText("");

			TypeText("Oduro: Keine weiteren Kontakte auf den Nahsensoren. Das Trümmerfeld beruhigt sich.", 12);
			TypeText("Kade: Ich überprüfe die letzten Telemetriedaten… aber es sieht so aus, als hätten wir es geschafft.", 12);
			TypeText("");

			TypeText("Eine schwere Erleichterung geht durch meinen Körper. Die Anspannung der letzten Stunden fällt nur langsam ab.", 12);
			TypeText("");

			TypeText("Commander: Alle Systeme stufenweise aus dem Gefechtsmodus holen.", 12);
			TypeText("Commander: Reparaturteams auf alle Decks. Sobald der Antrieb wieder voll zur Verfügung steht, schließen wir zur ARGOS auf.", 12);
			TypeText("");

			TypeText("Ich atmete einmal tief durch und sehe Oduro an.", 12);
			TypeText("");

			TypeText("Commander: Lieutenant Oduro…", 12);
			TypeText("Oduro: Captain?", 12);
			TypeText("Commander: Sie haben auf der Brücke einen kühlen Kopf bewahrt. Mit sofortiger Wirkung sind Sie mein Erster Offizier.", 12);
			TypeText("");

			TypeText("Für einen winzigen Moment zuckte ein Lächeln über ihr Gesicht, bevor es wieder von Professionalität überdeckt wird.", 12);
			TypeText("Oduro: Aye, Captain.", 12);
			TypeText("");

			TypeText("Commander: Kade.", 12);
			TypeText("Kade: Ja, Captain?", 12);
			TypeText("Commander: Ihre Arbeit an den Sensoren und Systemen hat uns mehr als einmal den Hals gerettet.", 12);
			TypeText("Commander: Sie werden zum Lieutenant Commander befördert. Zweiter Offizier auf der Brücke.", 12);
			TypeText("");

			TypeText("Kade: ...Verstanden, Captain. Ich werde Sie nicht enttäuschen.", 12);
			TypeText("");

			TypeText("Ich werfe einen letzten Blick auf die Frontscheibe, auf das Trümmerfeld, in dem noch vereinzelt Funken verglühender Metallstücke aufleuchten.", 12);
			TypeText("");

			TypeText("Commander: Oduro, sie haben das Kommando. Ich ziehe mich für einen Moment zurück.", 12);
			TypeText("Oduro: Wir halten Sie auf dem Laufenden, Captain.", 12);
			TypeText("");

			Console.WriteLine();
			TypeText("Ich trete in den Turbolift.", 12);
			Console.ReadKey();

			Console.Clear();
			DrawTurboLiftRoute();
			TypeText("");
			TypeText("Die Türen schließen sich mit einem leichten Zischen. Der Lift ruckt an.", 12);
			TypeText("Zuerst geht es dutzende Decks hinab, vorbei an Hangars, Frachträumen und Versorgungsebenen.", 12);
			TypeText("Dann wechselt der Lift die Richtung, schießt quer durch das Innere des Schiffes, wie eine Bahn in einem stählernen Labyrinth.", 12);
			TypeText("Schließlich geht es wieder aufwärts, viele Decks hinauf, bis in den oberen Wohnbereich der Offiziere.", 12);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um auf dem Captain-Deck anzukommen...");
			Console.ReadKey();

			Console.Clear();
			DrawCaptainsDeckOverview();
			TypeText("");
			TypeText("Die Türen öffnen sich. Das Offiziersdeck.", 12);
			TypeText("Die Luft wirkt hier stiller, gedämpfter, als läge ein unsichtbares Schutzfeld über diesem Bereich.", 12);
			TypeText("");

			TypeText("Ich gehe den kurzen Gang hinunter zu meinem neuen Quartier. Die Beschriftung neben der Tür blinkt bereits:", 12);
			TypeText("[ CAPTAIN'S QUARTERS ]", 12);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um das Quartier zu betreten...");
			Console.ReadKey();

			Console.Clear();
			DrawCaptainsQuarters();
			TypeText("");
			TypeText("Das Quartier ist groß, beinahe absurd groß, wenn man bedenkt wie wenig Platz die meisten Menschen noch haben.", 12);
			TypeText("Ein breites Panoramafenster zeigt die dunkle Weite des Alls.", 12);
			TypeText("Trümmerteile ziehen langsam an der GENESIS vorbei, wie rostige Blätter im Wind.", 12);
			TypeText("");

			TypeText("Mein Blick bleibt einen Moment an der weit entfernten Erde hängen. Friedlich. Ruhig. Von hier aus sieht sie aus wie immer.", 12);
			TypeText("Nur wir wissen, was dort unten lauert.", 12);
			TypeText("");

			TypeText("Ich lasse mich auf das Sofa sinken und öffne das kleine Versorgungsmodul an der Wand.", 12);
			TypeText("Eine einfache Mahlzeit, nichts Besonderes, aber warm und nahrhaft.", 12);
			TypeText("Zum ersten Mal seit Stunden spüre ich so etwas wie Hunger.", 12);
			TypeText("");

			TypeText("Nachdem der letzte Bissen verschwunden ist, ziehe ich die Uniformjacke aus und gehe in die Nasszelle.", 12);
			TypeText("Echte Wassertropfen prasseln auf meine Haut, kein Recyclingnebel, kein Spartimer.", 12);
			TypeText("Für einen Moment vergesse ich Schlachten, Viren und alte Zivilisationen.", 12);
			TypeText("");

			TypeText("Zurück im Hauptraum des Quartiers werfe ich einen letzten Blick aus dem Fenster.", 12);
			TypeText("Die GENESIS wirkt auf einmal weniger wie eine Maschine und mehr wie ein lebender Organismus, der durchhalten will, genau wie wir.", 12);
			TypeText("");

			TypeText("Ich lege mich auf das Bett. Der Körper ist erschöpft, der Geist arbeitet weiter.", 12);
			TypeText("Erinnerungen an vergangene Einsätze blitzen auf, an die Zeit in der genoptimierten Infanterie...", 12);
			TypeText("...an die geheimen Anlagen auf der Erde, die wir bewacht haben, ohne zu verstehen, was dort wirklich lag.", 12);
			TypeText("");

			TypeText("Langsam schiebt sich die Müdigkeit wie ein Schleier über meine Gedanken.", 12);
			TypeText("Bevor ich einschlafe, schicke ich meiner Frau eine Nachricht.", 12);
			TypeText("Morgen… oder was immer an Bord eines Schiffes als Morgen gilt… beginnt der nächste Schritt.", 12);
			TypeText("");

			Console.WriteLine();
			TypeText("Die GENESIS hält Kurs. Die ARGOS ist irgendwo voraus, auf dem Weg zur Erde.", 12);
			TypeText("Und tief in mir weiß ich: Ich werde wieder hinuntergehen müssen.", 12);
			TypeText("");

			Console.WriteLine("\nDrücke eine Taste, um zur nächsten Szene (Erde & Flottenaufstellung) zu wechseln...");
			Console.ReadKey();

			// Übergang zur nächsten Szene
			Scene_EarthApproach();
		}
		// ASCII-Grafik: Turbolift-Strecke (lange Route)
		static void DrawTurboLiftRoute()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("                [ BRÜCKE / DECK 1 ]");
			Console.WriteLine("                         |");
			Console.WriteLine("                         v");
			Console.WriteLine("                     [ DECK 12 ]");
			Console.WriteLine("                         |");
			Console.WriteLine("                         v");
			Console.WriteLine("                     [ DECK 24 ]");
			Console.WriteLine("                         |");
			Console.WriteLine("                         v");
			Console.WriteLine("                     [ DECK 36 ]");
			Console.WriteLine("                         |");
			Console.WriteLine("                         v");
			Console.WriteLine("                 ====== QUERTRANSIT ======");
			Console.WriteLine("                         →  →  →");
			Console.WriteLine("                     [ SEKTION B ]");
			Console.WriteLine("                         |");
			Console.WriteLine("                         v");
			Console.WriteLine("                     [ DECK 41 ]");
			Console.WriteLine("                         |");
			Console.WriteLine("                         v");
			Console.WriteLine("                     [ DECK 45 ]");
			Console.WriteLine("                         |");
			Console.WriteLine("                         v");
			Console.WriteLine("                 [ CAPTAIN-DECK / DECK 47 ]");
			Console.ResetColor();
		}
		// ASCII-Grafik: Captain-Deck Übersicht
		static void DrawCaptainsDeckOverview()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                ===============================");
			Console.WriteLine("                |     CAPTAIN-DECK / 47      |");
			Console.WriteLine("                ===============================");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("   [Lift]----[Flur]----[CAPTAIN'S QUARTERS]");
			Console.WriteLine("                  |");
			Console.WriteLine("             [Offiziers-Lounge]");
			Console.ResetColor();
		}

		// ASCII-Grafik: Captain-Quartier (luxuriös)
		static void DrawCaptainsQuarters()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("        ________________________________________________");
			Console.WriteLine("       |                                                |");
			Console.WriteLine("       |                PANORAMA-FENSTER                |");
			Console.WriteLine("       |   _________________________________            |");
			Console.WriteLine("       |  /                                 \\           |");
			Console.WriteLine("       | |   Sterne, Trümmer, ferne Lichter  |          |");
			Console.WriteLine("       |  \\_________________________________/           |");
			Console.WriteLine("       |                                                |");
			Console.WriteLine("       |   [Sofa]           [Tisch]          [Konsolen] |");
			Console.WriteLine("       |     ___              ___                []     |");
			Console.WriteLine("       |    /   \\            /   \\              [__]    |");
			Console.WriteLine("       |                                                |");
			Console.WriteLine("       |     [Schlafbereich]        [Nasszelle]         |");
			Console.WriteLine("       |        ____                 ______             |");
			Console.WriteLine("       |       |Bett|               |DUSCHE|            |");
			Console.WriteLine("       |_______________________________________________ |");
			Console.ResetColor();
		}
		static void Scene_EarthApproach()
		{
			Console.Clear();
			SaveGame("Scene_EarthApproach");

			TypeText("Als ich die Augen öffne, ist das Quartier still.", 12);
			TypeText("Nur das tiefe Brummen der Reaktoren erinnert daran, dass wir weiterhin unterwegs sind.", 12);
			TypeText("");

			TypeText("Nach einer kurzen Dusche und einem knappen Frühstück mache ich mich auf den Weg zurück zur Brücke.", 12);
			TypeText("Die Verantwortung wartet nicht.", 12);
			Console.WriteLine();

			Console.WriteLine("Drücke eine Taste, um auf der Brücke anzukommen...");
			Console.ReadKey();

			Console.Clear();
			DrawEarthOrbitScene();
			TypeText("");
			TypeText("Die Erde dominiert den Ausblick. Eine blaue Kugel, halb im Schatten, halb im Licht.", 12);
			TypeText("Daneben, im weiten Orbit, schwebt die ARGOS schwer, träge und doch bereit, wieder zuzuschlagen.", 12);
			TypeText("Die GENESIS hält in sicherer Entfernung, ihre Hülle gezeichnet, aber intakt.", 12);
			TypeText("");

			TypeText("Oduro: Captain auf der Brücke!", 12);
			TypeText("Ich nicke knapp und trete an die zentrale Konsole.", 12);
			TypeText("");

			TypeText("Kade: ARGOS meldet stabile Umlaufbahn über der Erdoberfläche.", 12);
			TypeText("Kade: Noch immer keine endgültige Bestätigung, ob die Atlaner-Superwaffe ausgelöst wurde oder nicht.", 12);
			TypeText("");

			TypeText("Auf dem zentralen Holo tauchen Datenströme auf – Fragmente, Rauschen, vereinzelte klare Signale.", 12);
			TypeText("Ich erkenne Teile von Koordinaten, Tarnfrequenzen, Zugangscodes, die ich längst vergessen glaubte.", 12);
			TypeText("");

			TypeText("Oduro: Sie kennen diese Muster, oder?", 12);
			TypeText("Ich starre auf die Daten. Zurückversetzt in eine Zeit, in der ich noch kein Captain war.", 12);
			TypeText("");

			TypeText("Commander: Ich war ein sogenannter optimierter Infanterist, lange bevor ich auf Brücken stand.", 12);
			TypeText("Commander: Einer von denen, die die Atlanter-Anlage auf der Erde bewachten.", 12);
			TypeText("Commander: Wir hatten keine Ahnung, was wir da eigentlich schützen.", 12);
			TypeText("");

			TypeText("Kade: Dann sind Sie der Einzige an Bord, der den Komplex wenigstens teilweise aus eigener Sicht kennt.", 12);
			TypeText("Oduro: Wenn wir die Waffe verstehen oder neu aktivieren wollen…", 12);
			TypeText("Commander: ...muss ICH runter.", 12);
			TypeText("");

			TypeText("Ich blicke erneut zur Erde. Ein Planet voller Toter und vielleicht der Schlüssel zu unserer Rettung.", 12);
			TypeText("");

			TypeText("Commander: Wir stellen ein Einsatzteam zusammen. Taktisches Personal, Technik, Medizin, klein, aber spezialisiert.", 12);
			TypeText("Commander: Vorbereitung des Teleporter-Decks. Wir verschwenden keine Zeit.", 12);
			TypeText("Oduro: Ich erhebe Einspruch. Laut Gesetz darf der ranghöchste Offizier nicht an Ausseneinsätzen teilnehmen.", 12);
			TypeText("Commander: Zur Kenntnis genommen, Ich werde dennoch gehen.", 12);
			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um zum Teleporter-Deck zu wechseln...");
			Console.ReadKey();

			Console.Clear();
			DrawTeleporterPad();
			TypeText("");
			TypeText("Das Teleporter-Deck ist hell ausgeleuchtet. Keine Mystik, keine Magie, reine, brutale Technik.", 12);
			TypeText("Ein großer, gepanzerter Ring markiert das eigentliche Transportfeld.", 12);
			TypeText("Konsole um Konsole zeigt Statusanzeigen, Vektorberechnungen und Warnungen.", 12);
			TypeText("");

			TypeText("Techniker: Captain, das Feld ist stabil. Wir können kurze Sprünge in den oberen Atmosphärenbereich wagen.", 12);
			TypeText("Techniker: Alles darunter wird knifflig – Störungen, Ruinen, Nanokontamination in der Luft.", 12);
			TypeText("");

			TypeText("Ich trete auf das Podium. Neben mir das Einsatzteam – jede und jeder mit eigenem Schatten in der Vergangenheit.", 12);
			TypeText("");

			TypeText("Oduro (über Interkom): Brücke an Teleporter-Deck. Alle Systeme im Standby. Wir haben Sie im Fokus, Captain.", 12);
			TypeText("Kade: Koordinaten der alten Atlanter-Anlage sind in das Gitter geladen. So genau, wie es unsere Sensoren zulassen.", 12);
			TypeText("");

			TypeText("Ich atme tief durch. Die Erinnerungen kehren zurück: Beton, Stahl, kalte Luft in unterirdischen Schächten.", 12);
			TypeText("Und die dumpfe Ahnung, dass dort unten etwas liegt, das nie für Menschen gedacht war.", 12);
			TypeText("");

			TypeText("Commander: Hier spricht Captain der GENESIS.", 12);
			TypeText("Commander: Erster Bodeneinsatz zur Atlanter-Anlage beginnt.", 12);
			TypeText("Commander: Egal, was wir dort finden – wir sorgen dafür, dass die Menschheit noch eine Wahl hat.", 12);
			TypeText("");

			TypeText("Techniker: Feldspannung steigt… 20 Prozent… 40… 60…", 12);
			TypeText("Ein leises Summen legt sich über das Podium, die Luft beginnt zu flirren.", 12);
			TypeText("Lichtlinien wandern über den Boden und zeichnen das Transportmuster nach.", 12);
			TypeText("");

			TypeText("Techniker: 80 Prozent… 90… Feld stabil. Bereit zum Transfer, Captain.", 12);
			TypeText("");

			TypeText("Ich nicke dem Team zu.", 12);
			TypeText("Commander: GENESIS, ARGOS… haltet die Position.", 12);
			TypeText("Commander: Wir kommen zurück.", 12);
			TypeText("");

			TypeText("Mit einem letzten Blick zur Erde, projiziert auf dem taktischen Display, trete ich in die Mitte des Transportfeldes.", 12);
			TypeText("Das Licht wird heller, die Konturen verschwimmen.", 12);
			TypeText("");

			Console.ForegroundColor = ConsoleColor.Cyan;
			TypeText("   >>> FORTSETZUNG FOLGT – BODENEINSATZ AUF DER ERDE <<<", 8);
			Console.ResetColor();

			Console.WriteLine("\nDrücke eine Taste, um zum Menü oder zum nächsten Kapitel zurückzukehren...");
			Console.ReadKey();
		}

		// ------------------------------------------
		// ASCII-Grafik: Erde + GENESIS & ARGOS im Orbit
		// ------------------------------------------
		static void DrawEarthOrbitScene()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("                   .-\"\"\"\"\"\"-.");
			Console.WriteLine("                .-'   _   _   '-.");
			Console.WriteLine("              .'    .' `-' `.    '.");
			Console.WriteLine("             /     /  .---.  \\     \\");
			Console.WriteLine("            |     |  /     \\  |     |");
			Console.WriteLine("            |     |  \\_____/  |     |");
			Console.WriteLine("             \\     \\         /     /");
			Console.WriteLine("              '.    '.___.'    .'");
			Console.WriteLine("                '-.         .-'");
			Console.WriteLine("                    '-.__.-'");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine();
			Console.WriteLine("   [GENESIS]");
			Console.WriteLine("    ________________");
			Console.WriteLine(" __/                \\__");
			Console.WriteLine("/   ███████████████   \\");
			Console.WriteLine("\\__                __/");
			Console.WriteLine("   \\______________/");

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine();
			Console.WriteLine("                                  [ARGOS]");
			Console.WriteLine("                                   _____________");
			Console.WriteLine("                                __/           \\__");
			Console.WriteLine("                               /   ████████    _ \\");
			Console.WriteLine("                               \\__           __/ /");
			Console.WriteLine("                                  \\_________/");
			Console.ResetColor();
		}

		// ------------------------------------------
		// ASCII-Grafik: Teleporter-Pad (militärisch / technisch)
		// ------------------------------------------
		static void DrawTeleporterPad()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("          =========================================");
			Console.WriteLine("          |         TACTICAL TRANSPORT DECK       |");
			Console.WriteLine("          =========================================");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("          Kontrollstationen flankieren das Pad,");
			Console.WriteLine("          bewaffnete Sicherheitsposten halten Abstand.");
			Console.WriteLine();
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                _______________________________");
			Console.WriteLine("               /                               \\");
			Console.WriteLine("              /                                 \\");
			Console.WriteLine("             /   [   TRANSPORT-FELD RING   ]    \\");
			Console.WriteLine("            /___________________________________\\");
			Console.WriteLine("            |                                   |");
			Console.WriteLine("            |      (   O   O   O   O   O   )    |");
			Console.WriteLine("            |       \\               /          |");
			Console.WriteLine("            |        \\             /           |");
			Console.WriteLine("            |         '-----------'            |");
			Console.WriteLine("            |__________________________________|");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("            [Konsole A]        [Konsole B]");
			Console.WriteLine("                [##]              [##]");
			Console.WriteLine("                [##]              [##]");
			Console.ResetColor();
		}
		static void Scene_AtlanerMission_Start()
		{
			Console.Clear();
			SaveGame("Scene_AtlanerMission_Start");

			TypeText("Orbit über der Erde – GENESIS, Transportersektion.", 10);
			TypeText("Wir stehen in einer Reihe auf der Transporterplattform, schwere Kampfanzüge schließen sich um unsere Körper.", 10);
			TypeText("Die Helme verriegeln mit einem Zischen, das HUD flackert auf. Schildstatus: aktiv.", 10);
			TypeText("");
			TypeText("Oduro: Captain, die Zielkoordinaten liegen direkt vor dem Haupteingang der Atlanter-Anlage.", 10);
			TypeText("KADE: Wir können nur einmal sauber runter, der Nano-Virus stört die Musterpuffer jetzt schon.", 10);
			TypeText("");
			TypeText("Commander: Alles klar. Trupp, bereit machen zum Sprung. Wir holen uns diese Anlage zurück.", 10);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um den Transport zu starten...");
			Console.ReadKey();

			Console.Clear();
			TypeText("Transporter-Techniker: Energietransfer läuft… Muster werden aufgelöst…", 10);
			Console.Beep(700, 150);
			Console.Beep(900, 150);
			Thread.Sleep(400);

			TypeText("Die Welt zerbricht in Licht. Ein Bruchteil einer Sekunde fühlt es sich an, als würde jeder Knochen auseinandergezogen.", 10);
			Thread.Sleep(600);

			Console.Clear();
			DrawFacilityEntrance();
			TypeText("");
			TypeText("Der Transport löst sich auf und wir stehen im Staub eines verseuchten Planeten. Der Virus schien schon die Athmosphäre zu verändern.", 10);
			TypeText("Vor uns erhebt sich eine gigantische, schwarze Struktur: der Eingangsblock der Atlanter-Anlage.", 10);
			TypeText("Überall in der Ferne: Silhouetten von Ruinen, und in der Luft hängt der metallische Geschmack des Todes.", 10);
			TypeText("");

			TypeText("Squad-Leader: Captain, Türsystem ist tot. Keine Energie.", 10);
			TypeText("Commander: Dann wecken wir das Biest auf. Zum Eingangs-Terminal.", 10);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um das Hack-Terminal zu öffnen...");
			Console.ReadKey();

			Scene_AtlanerMission_EingangHack();
		}

		// -------------------------------------------------------------
		//  EINGANG: 3-REIHIGES SCHALTKREIS-HACK-PANEL
		// -------------------------------------------------------------
		static void Scene_AtlanerMission_EingangHack()
		{
			Console.Clear();
			SaveGame("Scene_AtlanerMission_EingangHack");

			int anzugHuelle = 100;   // einfache Lebensanzeige für den Bodeneinsatz

			TypeText("Wir treten an ein eingelassenes Terminal neben der gewaltigen Tür.", 10);
			TypeText("Atlantische Glyphen flackern auf, als unsere Systeme versuchen, das Interface zu übersetzen.", 10);
			TypeText("KADE (über Com): Captain, die Tür ist mit einem dreifachen Schaltkreis gesichert.", 10);
			TypeText("KADE: Finden Sie in jeder Reihe das fehlerhafte Modul, sonst schickt die Anlage einen Energieimpuls zurück.", 10);
			TypeText("");

			bool success = true;

			for (int level = 1; level <= 3; level++)
			{
				bool ok = DoAtlanerHack(level, ref anzugHuelle);
				if (!ok)
				{
					success = false;
					break;
				}

				if (anzugHuelle <= 0)
				{
					Console.Clear();
					DrawInfectedWave();
					GameOver("Der Rückschlag der Atlanter-Anlage durchschlägt deine Schilde und Rüstung.\n" +
							 "Das Team bricht zusammen, bevor die Tür überhaupt aufgeht.");
					return;
				}
			}

			if (!success)
			{
				Console.Clear();
				DrawInfectedWave();
				GameOver("Der letzte Energieimpuls zerstört eure Schutzsysteme.\n" +
						 "Infizierte strömen heran, als die Dunkelheit über euch hereinbricht.");
				return;
			}

			// Hack erfolgreich
			Console.Clear();
			Console.Beep(900, 140);
			Console.Beep(1100, 180);
			TypeText("Die Glyphen wechseln von tiefem Rot zu kaltem Blau. Der Schaltkreis akzeptiert unsere Eingaben.", 10);
			TypeText("Massive Verriegelungsbolzen lösen sich in der Tiefe, die Tür beginnt, sich zu öffnen.", 10);
			Thread.Sleep(600);

			Console.Clear();
			DrawFacilityEntranceOpen();
			TypeText("");
			TypeText("Eine Wolke aus Staub und jahrtausendelang eingeschlossener Luft strömt uns entgegen.", 10);
			TypeText("Commander: Trupp, rein da. Wir aktivieren die Anlage und das Waffensystem – oder wir sterben hier unten.", 10);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um die Anlage zu betreten...");
			Console.ReadKey();

			Scene_AtlanerMission_Labyrinth();
		}

		// Hilfsmethode: einzelnes Hack-Level
		static bool DoAtlanerHack(int level, ref int anzugHuelle)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"=== ATLANTISCHER SCHALTKREIS – EBENE {level} ===");
			Console.ResetColor();
			Console.WriteLine();

			string[][] muster;

			if (level == 1)
			{
				muster = new string[][]
				{
					new [] { "A", "B", "C", "X", "D" },
					new [] { "C", "D", "X", "B", "A" },
					new [] { "X", "A", "C", "D", "B" }
				};
			}
			else if (level == 2)
			{
				muster = new string[][]
				{
					new [] { "A", "X", "B", "C", "D" },
					new [] { "D", "C", "B", "X", "A" },
					new [] { "B", "C", "X", "D", "E" }
				};
			}
			else
			{
				muster = new string[][]
				{
					new [] { "X", "A", "B", "C", "D" },
					new [] { "A", "B", "C", "D", "X" },
					new [] { "B", "X", "C", "D", "E" }
				};
			}

			// In jeder Reihe ist genau EIN "X" der Fehler
			for (int row = 0; row < muster.Length; row++)
			{
				Console.WriteLine($"Reihe {row + 1}:");
				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.Write("   ");
				for (int i = 0; i < muster[row].Length; i++)
				{
					Console.Write($"[{muster[row][i]}] ");
				}
				Console.ResetColor();
				Console.WriteLine();
				Console.Write($"Fehlerhaftes Modul in Reihe {row + 1}? (Position 1–5): ");

				string input = Console.ReadLine();
				if (!int.TryParse(input, out int pos) || pos < 1 || pos > 5)
				{
					TypeText("Der Schaltkreis vibriert leicht, die Anlage scheint unsere Unentschlossenheit nicht zu mögen…", 10);
					anzugHuelle -= 10;
					continue;
				}

				int idx = pos - 1;
				if (muster[row][idx] == "X")
				{
					TypeText("Die Reihe stabilisiert sich, die Symbole werden statisch.", 10);
				}
				else
				{
					Console.Beep(300, 200);
					Console.Beep(250, 200);
					TypeText("Ein Emissionsimpuls schießt durch den Anzug – Schilde verzerren sich.", 10);
					anzugHuelle -= 15;
					TypeText($"Anzugintegrität sinkt auf {anzugHuelle}%.", 10);

					if (anzugHuelle <= 0)
						return false;
				}

				Console.WriteLine();
			}

			return true;
		}

		// -------------------------------------------------------------
		//  LABYRINTH IN DER ANLAGE (20 x 40) MIT 3 KONSOLEN + AUSGANG
		// -------------------------------------------------------------
		static void Scene_AtlanerMission_Labyrinth()
		{
			Console.Clear();
			SaveGame("Scene_AtlanerMission_Labyrinth");

			TypeText("Der Korridor dahinter ist breit, mit glatten, dunklen Wänden, durchzogen von leuchtenden Linien.", 10);
			TypeText("Die Atlanter hatten nie für Menschen gebaut – alles wirkt zu sauber, zu perfekt, zu tot.", 10);
			TypeText("Wir müssen zum Reaktorkern, zur Kontrollsektion und zur Waffensteuerung, alles tief im Inneren.", 10);
			TypeText("");

			// Einfacher Lebens-/Schutzwert für die Bodentruppe
			int anzugHuelle = 90;

			// Map 20 x 40
			string[] mapLines = new string[]
			{
				"########################################",
				"#S            #                    1  #",
				"# ### ####### # ####### # # ######### #",
				"#   #       #   #     # # #       #   #",
				"### # ##### ##### ### # # ####### # ###",
				"#   #     #     #   #   #       # #   #",
				"# ####### # ### ### ##### ##### # ### #",
				"#       # #   #   #     # #   # #   # #",
				"####### # ### ### ##### # # # # ### # #",
				"#       #       #         # # #   # # #",
				"# ### # ### # ### ######### # ### # # #",
				"# #   #   # #   #         # #   # # # #",
				"# # ##### # ### ######### # ### # # # #",
				"# #     # #   #   2       #   #   #   #",
				"# ##### # ### ####### ### ### ##### ###",
				"#     # #   #       # #   #   #   #   #",
				"##### # ### ####### # # ### # # # ### #",
				"#   # #   #   3   #   #   # # # #   # #",
				"# E       #       #####   #   #     # #",
				"########################################"
			};

			int rows = mapLines.Length;
			int cols = mapLines.Max(line => line.Length);  // längste Zeile als Maßstab

			char[,] map = new char[rows, cols];

			int playerR = 0, playerC = 0;

			for (int r = 0; r < rows; r++)
			{
				string line = mapLines[r].PadRight(cols, ' '); // fehlende Zeichen füllen

				for (int c = 0; c < cols; c++)
				{
					char ch = line[c];
					map[r, c] = ch;

					if (ch == 'S')   // Spielerstart
					{
						playerR = r;
						playerC = c;
					}
				}
			}

			bool console1Done = false;
			bool console2Done = false;
			bool console3Done = false;

			bool escapeTriggered = false;

			while (!escapeTriggered)
			{
				Console.Clear();
				DrawFacilityMap(map, rows, cols, playerR, playerC);
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Ziel: Reaktor (1), Kontrollkern (2), Waffensteuerung (3) aktivieren und dann zum Ausgang (E).");
				Console.ResetColor();
				Console.WriteLine($"Anzug-Integrität: {anzugHuelle}%");
				Console.WriteLine($"Konsole 1 (Reaktor): {(console1Done ? "AKTIV" : "OFFLINE")}");
				Console.WriteLine($"Konsole 2 (Steuerung): {(console2Done ? "AKTIV" : "OFFLINE")}");
				Console.WriteLine($"Konsole 3 (Waffe): {(console3Done ? "AKTIV" : "OFFLINE")}");
				Console.WriteLine();
				Console.WriteLine("Steuerung: W/A/S/D = bewegen, H = Konsole hacken, Q = abbrechen (zurück zur GENESIS ist keine Option 😉).");
				Console.Write("Eingabe: ");
				string cmd = Console.ReadLine().Trim().ToUpper();

				if (cmd == "Q")
				{
					TypeText("Commander: Negativ. Wir sind hier unten, bis die Anlage läuft.", 10);
					Thread.Sleep(500);
					continue;
				}

				int newR = playerR;
				int newC = playerC;

				if (cmd == "W") newR--;
				else if (cmd == "S") newR++;
				else if (cmd == "A") newC--;
				else if (cmd == "D") newC++;

				if (cmd == "H")
				{
					char tile = map[playerR, playerC];
					if (tile == '1' && !console1Done)
					{
						TypeText("Reaktorkonsole erkannt. Beginne Aktivierungsprozedur…", 10);
						bool ok = DoAtlanerHack(1, ref anzugHuelle);
						console1Done = ok;
					}
					else if (tile == '2' && !console2Done)
					{
						TypeText("Zentrale Steuerkonsole. Zugriff wird aufgebaut…", 10);
						bool ok = DoAtlanerHack(2, ref anzugHuelle);
						console2Done = ok;
					}
					else if (tile == '3' && !console3Done)
					{
						TypeText("Waffensteuerung. Letzter Sicherheitskreis wird geöffnet…", 10);
						bool ok = DoAtlanerHack(3, ref anzugHuelle);
						console3Done = ok;
					}
					else
					{
						TypeText("Keine aktive Konsole an dieser Position.", 10);
					}

					if (anzugHuelle <= 0)
					{
						DrawInfectedWave();
						GameOver("Die Atlanter-Systeme grillen eure Anzüge – der Trupp bricht in der Dunkelheit zusammen.");
						return;
					}

					continue;
				}

				// Bewegung prüfen
				if (cmd == "W" || cmd == "A" || cmd == "S" || cmd == "D")
				{
					if (newR < 0 || newR >= rows || newC < 0 || newC >= cols)
						continue;

					if (map[newR, newC] == '#')
					{
						TypeText("Solide Atlanter-Struktur. Hier kommen wir nicht durch.", 10);
						Thread.Sleep(200);
						continue;
					}

					playerR = newR;
					playerC = newC;

					// Chance auf kleine Infiziertenbegegnung in den Korridoren
					if (map[playerR, playerC] == ' ' || map[playerR, playerC] == '.')
					{
						if (new Random().Next(0, 100) < 8) // 8% Chance
						{
							KleineInfiziertenBegegnung(ref anzugHuelle);
							if (anzugHuelle <= 0)
							{
								DrawInfectedWave();
								GameOver("Die Infizierten wälzen sich über das Team und zerreißen die Anzüge.");
								return;
							}
						}
					}

					// Ausgang erreichen?
					if (map[playerR, playerC] == 'E')
					{
						if (console1Done && console2Done && console3Done)
						{
							escapeTriggered = true;
						}
						else
						{
							TypeText("Oduro: Captain, wir können nicht gehen, bevor alle Systeme aktiv sind!", 10);
							Thread.Sleep(500);
						}
					}
				}
			}

			Scene_AtlanerMission_ExitFight();
		}

		// -------------------------------------------------------------
		//  ABSCHLUSS: Waffensystem aktiviert – Flucht zum Landungsschiff
		// -------------------------------------------------------------
		static void Scene_AtlanerMission_ExitFight()
		{
			Console.Clear();
			SaveGame("Scene_AtlanerMission_ExitFight");

			TypeText("Wir erreichen den inneren Schacht, der direkt zum Sendeturm der Atlanter-Anlage führt.", 10);
			TypeText("Hinter uns donnern Türen zu – massive Verriegelungen fahren in die Wände.", 10);
			TypeText("Reaktor: ONLINE. Steuerung: ONLINE. Waffenplattform: BEREIT.", 10);
			TypeText("");

			TypeText("KADE (über Com): Captain, wir sehen massive Energiespitzen im Untergrund. Die Anlage wacht auf.", 10);
			TypeText("Oduro: Und die Infizierten merken es auch. Hörst du das…?", 10);

			Console.Beep(300, 200);
			Console.Beep(250, 180);
			DrawInfectedWave();
			TypeText("");
			TypeText("Hinter uns füllt der Gang sich mit verzerrten Gestalten, verschmolzene Körper, zusätzliche Gliedmaßen, verzogene Münder. Diese schrecklichen Schreie.", 10);
			TypeText("Sie stolpern, kriechen, rennen und immer wieder platzen diese grauen Pusteln und sprühen nanoglitzernden Staub in die Luft.", 10);
			TypeText("");

			TypeText("Commander: GENESIS, hier Bodentrupp. Forderung: Landungsschiff an Koordinate Sendeturm – SCHWEBEPOSITION, sofort!", 10);
			TypeText("Brücke: Anflug bestätigt. Bleibt in Bewegung, Captain!", 10);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um den Aufstieg zum Turm zu beginnen...");
			Console.ReadKey();

			Console.Clear();
			DrawTowerClimb();
			TypeText("");
			TypeText("Wir sprinten über eine schmale Rampe, die sich wie eine Ader durch das Innere des Turms zieht.", 10);
			TypeText("Unter uns: der gleißende Kern der Anlage. Über uns: der Ausgang zur Oberfläche.", 10);
			TypeText("");

			// Kleine Endkampf-Entscheidung
			TypeText("Die erste Welle Infizierter erreicht die Rampe. Wir haben nur Sekunden.", 10);
			Console.WriteLine();
			Console.WriteLine("1) Rückwärts feuern und langsam zurückweichen");
			Console.WriteLine("2) Granaten werfen und durchbrechen");
			Console.WriteLine("3) Schilde des Trupps nach vorne verstärken und halten");
			Console.Write("\nAuswahl: ");

			string choice = Console.ReadLine();

			Console.Clear();
			DrawInfectedWave();
			TypeText("");

			if (choice == "1")
			{
				TypeText("Wir drehen uns halb um, feuern Salve um Salve in die Woge aus Fleisch und Metall.", 10);
				TypeText("Einige der Kreaturen stürzen in den Abgrund, andere klettern über ihre Körper hinweg.", 10);
			}
			else if (choice == "2")
			{
				TypeText("Zwei, drei Granaten rollen zwischen die Beine der vordersten Infizierten.", 10);
				Console.Beep(700, 140);
				Console.Beep(900, 180);
				Console.Beep(400, 220);
				TypeText("Die Explosionen reißen eine Bresche in die Masse, Gliedmaßen und Metallteile fliegen durch den Schacht.", 10);
			}
			else if (choice == "3")
			{
				TypeText("Wir schieben die Schilde nach vorne, ein leuchtender Keil aus Energie.", 10);
				TypeText("Die ersten Kreaturen prallen dagegen, klauen und Zähne schaben darüber, aber wir halten.", 10);
			}
			else
			{
				TypeText("Ein Moment des Zögerns – und trotzdem reißen wir uns zusammen und feuern.", 10);
			}

			Thread.Sleep(600);

			Console.Clear();
			DrawShuttlePickup();
			TypeText("");
			TypeText("Ein Schatten fällt über die Turmöffnung, das Landungsschiff der GENESIS schwebt im Staubsturm.", 10);
			TypeText("Pilot: Los, los, los! Rein da!", 10);
			TypeText("Wir springen, werden von Greifarmen gepackt und ins Innere gezogen, während unter uns die ersten Infizierten den Rand erreichen.", 10);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um die Aktivierung der Atlaner-Waffe zu sehen...");
			Console.ReadKey();

			Console.Clear();
			DrawOrbitalStrike();
			TypeText("");
			TypeText("Im Orbit: GENESIS und ARGOS drehen sich aus der direkten Sichtlinie, ihre Sensoren auf die Erde gerichtet.", 10);
			TypeText("Ein einziger, unfassbar heller Lichtstrahl durchschlägt die Atmosphäre – die Atlanter-Waffe entfesselt ihre Macht.", 10);
			TypeText("Kontinente glühen auf, Städte vergehen, infizierte Schwärme werden in Licht und Wärme aufgelöst.", 10);
			TypeText("");

			TypeText("Über Kom: Commander: Wir haben es getan… aber zu welchem Preis.", 10);
			TypeText("Über Kom: Oduro: Captain… die Erde ist... Aber vielleicht hat die Menschheit jetzt wieder eine Chance.", 10);
			TypeText("");

			SaveGame("Scene_AtlanerMission_Ende");

			TypeText("Fortsetzung folgt…", 10);
			Console.ReadKey();
		}

		// -------------------------------------------------------------
		//  ASCII / ANZEIGEN FÜR DIE ATLANTER-MISSION
		// -------------------------------------------------------------
		static void DrawFacilityEntrance()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("           ________________________________");
			Console.WriteLine("          /                                \\");
			Console.WriteLine("         /   MONOLITHISCHER ATLANTER-EINGANG \\");
			Console.WriteLine("        /____________________________________\\");
			Console.WriteLine("        |                                    |");
			Console.WriteLine("        |        ███████████████████        |");
			Console.WriteLine("        |        █  VERSIEGELTES TOR █      |");
			Console.WriteLine("        |        ███████████████████        |");
			Console.WriteLine("        |____________________________________|");
			Console.ResetColor();
		}

		static void DrawFacilityEntranceOpen()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("           ________________________________");
			Console.WriteLine("          /                                \\");
			Console.WriteLine("         /   MONOLITHISCHER ATLANTER-EINGANG \\");
			Console.WriteLine("        /____________________________________\\");
			Console.WriteLine("        |                                    |");
			Console.WriteLine("        |   ████          FREIER ZUGANG     |");
			Console.WriteLine("        |   ████     SCHACHT INS INNERE     |");
			Console.WriteLine("        |   ████                            |");
			Console.WriteLine("        |____________________________________|");
			Console.ResetColor();
		}

		static void DrawFacilityMap(char[,] map, int rows, int cols, int playerR, int playerC)
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("============= ATLANTER-INNENANLAGE – TAKTISCHE ANSICHT =============");
			Console.ResetColor();
			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					if (r == playerR && c == playerC)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write('P');
						Console.ResetColor();
						continue;
					}

					char ch = map[r, c];
					switch (ch)
					{
						case '#':
							Console.ForegroundColor = ConsoleColor.DarkGray;
							break;
						case '1':
						case '2':
						case '3':
							Console.ForegroundColor = ConsoleColor.Yellow;
							break;
						case 'E':
							Console.ForegroundColor = ConsoleColor.Cyan;
							break;
						case 'S':
							Console.ForegroundColor = ConsoleColor.White;
							break;
						default:
							Console.ForegroundColor = ConsoleColor.DarkBlue;
							break;
					}
					Console.Write(ch);
					Console.ResetColor();
				}
				Console.WriteLine();
			}
		}

		static void DrawInfectedWave()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("   INFIZIERTE WELLE");
			Console.WriteLine("   =================");
			Console.WriteLine("      ( )    ( )    ( )");
			Console.WriteLine("     <|||>  <|||>  <|||>");
			Console.WriteLine("      / \\    / \\    / \\");
			Console.WriteLine("   verzerrte Körper, zusätzliche Arme,");
			Console.WriteLine("   offene Mäuler, aus denen Nanostaub quillt…");
			Console.ResetColor();
		}

		static void KleineInfiziertenBegegnung(ref int anzugHuelle)
		{
			Console.WriteLine();
			DrawInfectedWave();
			TypeText("Aus einem Seitengang stürzen zwei Infizierte, ihre Körper sind grotesk verformt.", 10);
			Console.Beep(500, 120);
			Console.Beep(450, 120);
			TypeText("Wir reißen die Waffen hoch und zerreißen sie in einem Salvenstoß, aber einige Splitter treffen unsere Schilde.", 10);
			anzugHuelle -= 4;
			if (anzugHuelle < 0) anzugHuelle = 0;
			TypeText($"Anzug-Integrität fällt auf {anzugHuelle}%.", 10);
			Thread.Sleep(400);
		}

		static void DrawTowerClimb()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("      ||");
			Console.WriteLine("      ||      STEIGSCHACHT ZUM SENDTURM");
			Console.WriteLine("      ||");
			Console.WriteLine("     /==\\");
			Console.WriteLine("    /====\\");
			Console.WriteLine("   /======\\");
			Console.WriteLine("  /========\\");
			Console.WriteLine(" /==========\\");
			Console.WriteLine("/============\\   <--- schmale Rampe nach oben");
			Console.ResetColor();
		}

		static void DrawShuttlePickup()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("             ________");
			Console.WriteLine("         ___/  __   /____");
			Console.WriteLine("        /   \\_/  \\_/    /");
			Console.WriteLine("       /  LANDUNGSSCHIFF /");
			Console.WriteLine("       \\______________ /");
			Console.WriteLine("             |  |");
			Console.WriteLine("             |  |   <--- schwebt über dem Turm");
			Console.ResetColor();
		}

		static void DrawOrbitalStrike()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("               GENESIS              ARGOS");
			Console.WriteLine("          _______/\\_______      _______/\\_______");
			Console.WriteLine("         /                 \\  /                 \\");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("                    ( ERDE )");
			Console.WriteLine("                  ___/####\\___");
			Console.WriteLine("                 /############\\");
			Console.WriteLine("                 \\############/");
			Console.WriteLine("                  \\___####___/");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine();
			Console.WriteLine("                    ||");
			Console.WriteLine("                    ||   GLEISSENDER ENERGIESTRAHL");
			Console.WriteLine("                    ||");
			Console.ResetColor();
		}
	}
}


