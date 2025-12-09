using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json;
using System.Runtime.CompilerServices;

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
			Console.WriteLine("=================================");
			Console.WriteLine("====== GENESIS – HAUPTMENÜ ======");
			Console.WriteLine("=================================\n");
			Console.ResetColor();

			Console.WriteLine("  1) Neues Spiel                            ");
			Console.WriteLine("  2) Weiterspielen (letzter Speicherstand)  ");
			Console.WriteLine("  3) Kapitel auswählen                      ");
			Console.WriteLine("  4) Beenden                                ");

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
				case "Scene_Prolog":
					ShowProlog();
					break;

				case "Scene_Intro":
					Scene_Intro();
					break;

				case "Scene_Alarm":
					Scene_Alarm();
					break;

				case "Scene_Kapitel1":
					Scene_Kapitel1();
					break;

				case "Scene_HeliosKontakt":
					Scene_HeliosKontakt();
					break;

				case "Scene_HeliosNachKampf":
					Scene_HeliosNachKampf();
					break;

				case "Scene_ZerstoererKampf":
					Scene_ZerstoererKampf();
					break;

				case "Scene_NachZerstoerer":
					Scene_NachZerstoerer();
					break;

				case "Scene_AtlanerMission_Start":
					Scene_AtlanerMission_Start();
					break;

				case "Scene_RueckkehrVomEinsatz_Begin":
					Scene_RueckkehrVomEinsatz();
					break;

				case "Scene_DieSuche":
					Scene_DieSuche();
					break;

				case "Scene_ErstkontaktGrauer":
					Scene_ErstkontaktGrauer();
					break;

				case "Scene_ErstkontaktGrauer_Begin":
					Scene_ErstkontaktGrauer();
					break;

				case "Scene_ErstkontaktGrauer_End":
					Scene_JupiterEuropaKontakt();
					break;

				case "Scene_JupiterEuropaKontakt":
					Scene_JupiterEuropaKontakt();
					break;
				case "Scene_EuropaKolonie":
					Scene_EuropaKolonie();
					break;
				case "Scene_Blutsvaeter_Entrance":
					Scene_Blutsvaeter_Entrance();
					break;
				case "Scene_Return_From_Bloodsfather_Mission":
					Scene_Return_From_Bloodsfather_Mission();
					break;
				case "Scene_AllianzAlarmUndFlottenstart":
					Scene_AllianzAlarmUndFlottenstart();
					break;
				case "Scene_AllianzFlottenschlacht<":
					Scene_AllianzFlottenschlacht();
					break;

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

			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("===========================");
			Console.WriteLine("==== Kapitel auswählen ====");
			Console.WriteLine("===========================\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("   1) --Prolog--");
			Console.WriteLine("   2) --Intro--");
			Console.WriteLine("   3) --Alarm – Transportsphär--");
			Console.WriteLine("   4) --Kapitel 1 – Flucht von der Erde--");
			Console.WriteLine("   5) --Helios-Kontakt--");
			Console.WriteLine("   6) --Nach Helios-Kampf / Atlanter-Entscheidung--");
			Console.WriteLine("   7) --Zerstörerkampf--");
			Console.WriteLine("   8) --Blindfeuer-Gefecht im Trümmerfeld--");
			Console.WriteLine("   9) --Atlanter-Bodenmission--");
			Console.WriteLine("  10) --Rückkehr vom Einsatz--");
			Console.WriteLine("  11) --Die Suche--");
			Console.WriteLine("  12) --Der 10 Planet/Die Grauen--");
			Console.WriteLine("  13) --Kolonie Kontakt--");
			Console.WriteLine("  14) --Kolonie Jupitermonde");
			Console.WriteLine("  15) --Zurück zur Erde--");
			Console.WriteLine("  16) --Rücker zum Jupiter--");
			Console.WriteLine("  17) --Allianz greift die Erde an--");
			Console.WriteLine("  18) --Finale Schlacht bei der Erde--");
			Console.WriteLine("  00) <<<Zurück zum Hauptmenü>>>");

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
					SaveGame("Scene_RueckkehrVomEinsatz_Begin");
					Scene_RueckkehrVomEinsatz();
					break;
				case "11":
					SaveGame("Scene_DieSuche");
					Scene_DieSuche();
					break;
				case "12":
					SaveGame("Scene_ErstkontaktGrauer");
					Scene_ErstkontaktGrauer();
					break;
				case "13":
					SaveGame("Scene_JupiterEuropaKontakt");
					Scene_JupiterEuropaKontakt();
					break;
				case "14":
					SaveGame("Scene_EuropaKolonie");
					Scene_EuropaKolonie();
					break;
				case "15":
					SaveGame("Scene_Blutsvaeter_Entrance");
					Scene_Blutsvaeter_Entrance();
					break;
				case "16":
					SaveGame("Scene_Return_From_Bloodsfather_Mission");
					Scene_Return_From_Bloodsfather_Mission();
					break;
				case "17":
					SaveGame("Scene_AllianzAlarmUndFlottenstart");
					Scene_AllianzAlarmUndFlottenstart();
					break;
				case "18":
					SaveGame("Scene_AllianzFlottenschlacht");
					Scene_AllianzFlottenschlacht();
					break;
				case "19":
					SaveGame("Scene_AllianzKapitel1_Abschluss_Start");
					break;
				case "00":
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
			Console.WriteLine("                        /|\\");
			Console.WriteLine("                         | ");
			Console.WriteLine("                         | ");
			Console.WriteLine("                        \\|/");
			Console.WriteLine("                     [ DECK 12 ]");
			Console.WriteLine("                [ BRÜCKE / DECK 1 ]");
			Console.WriteLine("                        /|\\");
			Console.WriteLine("                         | ");
			Console.WriteLine("                         | ");
			Console.WriteLine("                        \\|/");
			Console.WriteLine("                     [ DECK 24 ]");
			Console.WriteLine("                        /|\\");
			Console.WriteLine("                         | ");
			Console.WriteLine("                         | ");
			Console.WriteLine("                        \\|/");
			Console.WriteLine("                     [ DECK 36 ]");
			Console.WriteLine("                        /|\\");
			Console.WriteLine("                         | ");
			Console.WriteLine("                         | ");
			Console.WriteLine("                        \\|/");
			Console.WriteLine("                 ====== QUERTRANSIT ======");
			Console.WriteLine("                         →  →  →");
			Console.WriteLine("                     [ SEKTION B ]");
			Console.WriteLine("                        /|\\");
			Console.WriteLine("                         | ");
			Console.WriteLine("                         | ");
			Console.WriteLine("                        \\|/");
			Console.WriteLine("                     [ DECK 41 ]");
			Console.WriteLine("                        /|\\");
			Console.WriteLine("                         | ");
			Console.WriteLine("                         | ");
			Console.WriteLine("                        \\|/");
			Console.WriteLine("                     [ DECK 45 ]");
			Console.WriteLine("                        /|\\");
			Console.WriteLine("                         | ");
			Console.WriteLine("                         | ");
			Console.WriteLine("                        \\|/");
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
			Console.WriteLine("             [Offiziers-Louge]");
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
		// ASCII-Grafik: Erde + GENESIS & ARGOS im Orbit
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
		// ASCII-Grafik: Teleporter-Pad 
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
		//  EINGANG: 3-REIHIGES SCHALTKREIS-HACK-PANEL
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
		//  LABYRINTH IN DER ANLAGE (20 x 40) MIT 3 KONSOLEN + AUSGANG
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
		//  ABSCHLUSS: Waffensystem aktiviert – Flucht zum Landungsschiff
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


		//  ASCII / ANZEIGEN FÜR DIE ATLANTER-MISSION

		static void DrawFacilityEntrance()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("           ________________________________");
			Console.WriteLine("          /                                \\");
			Console.WriteLine("         /   MONOLITHISCHER ATLANTER-EINGANG \\");
			Console.WriteLine("        /____________________________________\\");
			Console.WriteLine("        |                                    |");
			Console.WriteLine("        |        ███████████████████         |");
			Console.WriteLine("        |        █  VERSIEGELTES TOR █       |");
			Console.WriteLine("        |        ███████████████████         |");
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
			Console.WriteLine("        |   ████          FREIER ZUGANG      |");
			Console.WriteLine("        |   ████     SCHACHT INS INNERE      |");
			Console.WriteLine("        |   ████                             |");
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

			Scene_RueckkehrVomEinsatz();
			return;
		}
		static void Scene_RueckkehrVomEinsatz()
		{
			// Speicherpunkt vor der Szene
			SaveGame("Scene_RueckkehrVomEinsatz_Begin");

			Console.Clear();
			DrawPlanetStrikeViewFromShuttle();
			TypeText("");
			TypeText("Ich sitze im Landungsschiff, der Körper noch voller Adrenalin.", 10);
			TypeText("Durch das kleine Sichtfenster sehe ich den letzten Nachhall des Lichtimpulses, der über die Erdoberfläche gerast ist.", 10);
			TypeText("Die Atlanter-Waffe hat ausgelöst. Ein greller Riss aus Licht über einem toten Planeten.", 10);
			TypeText("");

			TypeText("Für einen Moment ist alles still im Inneren des Shuttles. Nur das dumpfe Brummen der Triebwerke und unser Atem.", 10);
			TypeText("");

			Console.WriteLine();
			TypeText("Ich tippe auf mein Kom-Interface.", 10);
			TypeText("Commander: GENESIS, hier Landungsschiff. Commander Oduro, kommen Sie.", 10);
			TypeText("");

			TypeText("Ein kurzes Rauschen, dann ihre Stimme. Ruhig, fokussiert, wie immer.", 10);
			TypeText("Oduro: „Hier Commander Oduro. Wir empfangen Sie klar und deutlich, Captain.“", 10);
			TypeText("");

			TypeText("Commander: Statusbericht zur Waffe. Was sehen Sie vom Orbit aus?", 10);
			TypeText("");

			TypeText("Oduro: „Der Lichtimpuls ist komplett über die Oberfläche gelaufen. Wir registrieren massive Strahlen Signaturen, aber keine aktiven Viren und Infizierte.“", 10);
			TypeText("Oduro: „Nach allen Sensorwerten zu urteilen: Die Atlanter-Waffe hat funktioniert.“", 10);
			TypeText("");

			TypeText("Ich lehne mich kurz zurück. Ein Teil von mir hat es nicht glauben wollen, bis sie es ausspricht.", 10);
			TypeText("");

			TypeText("Oduro: „Und, Captain… eine weitere Meldung. Ihre Frau ist wohlauf. Sie befindet sich sicher an Bord der GENESIS.“", 10);
			TypeText("");

			TypeText("Der Knoten in meiner Brust löst sich ein Stück. Ich atme langsam aus.", 10);
			TypeText("Commander: Bestätigt. Danke, Commander.", 10);
			TypeText("");

			TypeText("Oduro: „Noch etwas: Aufgrund der strukturellen Schäden an der Hülle kann die GENESIS keinen Landeanflug riskieren.“", 10);
			TypeText("Oduro: „Sie werden auf der ARGOS landen und von dort per Teleporter zur GENESIS übertragen.“", 10);
			TypeText("");

			TypeText("Commander: Verstanden. Landung auf der ARGOS. Wir sehen uns dann oben.", 10);

			Console.WriteLine("\nDrücke eine Taste, um die Landephase fortzusetzen...");
			Console.ReadKey();

			// Innenraum-Grafik des Landungsschiffs
			Console.Clear();
			DrawLandingShuttleInterior();
			TypeText("");
			TypeText("Wir sitzen angeschnallt in den Sitzen entlang der Wand. Metall, Gurte, Gitterboden, kein überflüssiger Komfort.", 10);
			TypeText("Das Landungsschiff schüttelt sich, als wir die obere Atmosphäre durchbrechen und Kurs auf die ARGOS nehmen.", 10);
			TypeText("");

			Thread.Sleep(900);

			// Andocken an die ARGOS
			Console.Clear();
			DrawArgosDockingPort();
			TypeText("");
			TypeText("Ein dumpfer Schlag geht durch die Hülle, als die Andockkrallen der ARGOS zupacken.", 10);
			TypeText("Pilot: „Andockmanöver abgeschlossen. Druckausgleich läuft.“", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um das Landungsschiff zu verlassen...");
			Console.ReadKey();

			// Dekontamination auf der ARGOS
			Console.Clear();
			DrawDeconChamberArgos();
			TypeText("");
			TypeText("Die erste Schleuse führt uns direkt in eine Dekontaminationszone der ARGOS.", 10);
			TypeText("Lichtfelder scannen unsere Anzüge, feine Sprühnebel legen sich wie kalter Hauch auf die Panzerplatten.", 10);
			TypeText("Jede Nanospur des Virus wird aus den äußeren Schichten herausgebrannt.", 10);
			TypeText("");

			TypeText("Medizinischer Offizier: „Captain, wir empfehlen, die Kampfanzüge hier zu lassen. Zu hohes Restrisiko.“", 10);
			TypeText("");

			TypeText("Commander: Negativ. Ich bleibe im Anzug, bis wir auf der GENESIS sind.", 10);
			TypeText("Commander: Wir haben zu viel gesehen, um jetzt nachlässig zu werden.", 10);
			TypeText("");

			TypeText("Der Offizier nickt nur knapp. Man sieht ihm an, dass er widersprechen möchte, aber er lässt es bleiben.", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um zur Teleporter-Plattform der ARGOS zu gehen...");
			Console.ReadKey();

			// Teleporter-Plattform ARGOS → GENESIS
			Console.Clear();
			DrawTeleporterPadArgos();
			TypeText("");
			TypeText("Die Teleporter-Plattform der ARGOS sieht der der GENESIS ähnlich, nur beengter, provisorischer.", 10);
			TypeText("Energiekabel ziehen sich über den Boden, zusätzliche Feldgeneratoren wurden hastig nachgerüstet.", 10);
			TypeText("");

			TypeText("Techniker ARGOS: „Ziel ist die Haupt-Transportersektion der GENESIS. Dekontaminationsfeld ist dort bereits hochgefahren.“", 10);
			TypeText("Commander: In Ordnung. Team, auf die Plattform. Wir gehen nach Hause.", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um den Transfer zu starten...");
			Console.ReadKey();

			Console.Clear();
			TypeText("Die Feldprojektoren heulen auf. Das Energierauschen kriecht uns unter die Haut.", 10);
			Console.Beep(700, 140);
			Console.Beep(900, 140);
			Thread.Sleep(400);
			TypeText("Die Welt zerreißt in Lichtsplitter, dann fügt sie sich wieder zusammen.", 10);

			// Ankunft auf der GENESIS – zweite Reinigung, Anzug wird zerstört
			Console.Clear();
			DrawTeleporterHubGenesis();
			TypeText("");
			TypeText("Wir materialisieren auf der Transporterplattform der GENESIS.", 10);
			TypeText("Noch bevor ich einen Schritt machen kann, fährt ein zweites Dekontaminationsfeld hoch.", 10);
			TypeText("");

			TypeText("Sicherheits-Offizier GENESIS: „Captain, verbleibende Kampfanzüge sind als Hochrisiko eingestuft.“", 10);
			TypeText("Sicherheits-Offizier: „Die Systeme empfehlen vollständige Demontage und anschließende Vernichtung.“", 10);
			TypeText("");

			TypeText("Ich blicke ein letztes Mal auf meinen Anzug. Er hat uns durch die Hölle getragen.", 10);
			TypeText("Commander: Bestätigt. Prozedur freigegeben.", 10);
			TypeText("");

			TypeText("Automatische Systeme lösen die Verriegelungen. Panzerplatten fahren auf, innere Schichten werden abgezogen.", 10);
			TypeText("In einer Seitenkammer werden die Reste des Anzugs von Energiefeldern erfasst und in glühenden Staub verwandelt.", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um zum Turbolift zu gehen...");
			Console.ReadKey();

			// Turbolift zur Captain-Ebene 
			Console.Clear();
			DrawTurboLiftRoute();
			TypeText("");
			TypeText("Ich betrete den Turbolift. Die Türen schließen sich hinter mir mit einem sanften Zischen.", 10);
			TypeText("Der Lift setzt sich in Bewegung, zuerst nach unten, dann quer durch das Schiff, dann wieder nach oben.", 10);
			TypeText("Jeder Ruck, jeder leise Ton erinnert mich daran, dass ich lebend zurückgekommen bin.", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um auf dem Captain-Deck anzukommen...");
			Console.ReadKey();

			// Captain-Deck & Quartier 
			Console.Clear();
			DrawCaptainsDeckOverview();
			TypeText("");
			TypeText("Das Captain-Deck empfängt mich mit gedämpftem Licht und gedämpfter Stille.", 10);
			TypeText("Hier oben ist nichts vom Lärm der letzten Stunden zu höhren, nur das ruhige Atmen des Schiffes.", 10);
			TypeText("");

			TypeText("Ich gehe den Korridor entlang. Jeder Schritt fühlt sich schwerer an, je näher ich der Tür komme.", 10);
			TypeText("Die Beschriftung neben der Tür leuchtet ruhig: [ CAPTAIN'S QUARTERS ]", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um das Quartier zu betreten...");
			Console.ReadKey();

			Console.Clear();
			DrawCaptainsQuarters();
			TypeText("");
			TypeText("Die Tür gleitet zur Seite.", 10);
			TypeText("Und da steht sie.", 10);
			TypeText("");

			TypeText("Meine Frau. Lebendig. Unversehrt. Mit müden, aber wachen Augen.", 10);
			TypeText("Für einen Moment bleibt alles stehen, die Geräusche der GENESIS, die Gedanken an die Erde, die Schreie der Infizierten.", 10);
			TypeText("");

			TypeText("Ich mache einen Schritt nach vorne. Dann noch einen. Und dann bin ich bei ihr.", 10);
			TypeText("Sie fällt mir in die Arme, als wäre sie nur dafür geschaffen worden, genau hier zu sein.", 10);
			TypeText("");

			TypeText("Ich halte sie fest. Fester, als es jede Vorschrift erlaubt.", 10);
			TypeText("Zum ersten Mal seit langer Zeit geht es nicht um Schiff, Mission oder Menschheit.", 10);
			TypeText("Nur um diesen Moment.", 10);
			TypeText("");

			TypeText("Irgendwann löst sich die Umarmung. Wir setzen uns. Reden kaum.", 10);
			TypeText("Es reicht, dass wir beide noch da sind.", 10);
			TypeText("");

			TypeText("Die Müdigkeit holt mich ein wie eine Welle. Ich weiß, dass der nächste Einsatz kommen wird.", 10);
			TypeText("Die Erde ist nicht mehr dieselbe. Die Menschheit auch nicht. Und ich schon gar nicht.", 10);
			TypeText("");

			TypeText("Aber für ein paar Stunden erlaube ich mir, einfach nur zu atmen.", 10);
			TypeText("Und zu glauben, dass wir vielleicht doch noch eine Chance haben.", 10);
			TypeText("");


			SaveGame("Scene_RueckkehrVomEinsatz_End");
		}
		// ZUSÄTZLICHE ASCII-GRAFIKEN FÜR DIE RÜCKKEHR-SZENE
		static void DrawPlanetStrikeViewFromShuttle()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("        BLICK AUS DEM LANDUNGSSCHIFF – ERDORBIT");
			Console.WriteLine("   -------------------------------------------------");
			Console.WriteLine("         .-\"\"\"\"\"-.           greller Lichtbogen");
			Console.WriteLine("       .'   _   _ '.      ~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine("      /    (_) (_)  \\");
			Console.WriteLine("     |   .--------.  |");
			Console.WriteLine("      \\  '--------'  /");
			Console.WriteLine("       '.          .'");
			Console.WriteLine("         '-.____.-'");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("   Ein gleißender Impuls zieht wie ein Feuerband über die Oberfläche.");
			Console.ResetColor();
		}

		static void DrawLandingShuttleInterior()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("      [ LANDUNGSSCHIFF – INNENRAUM ]");
			Console.WriteLine("   --------------------------------------");
			Console.WriteLine("   | [Sitz]  [Sitz]  [Sitz]  [Sitz]    |");
			Console.WriteLine("   |                                    |");
			Console.WriteLine("   | Gitterboden, angeschnallte Soldaten|");
			Console.WriteLine("   |                                    |");
			Console.WriteLine("   | [Sitz]  [Sitz]  [Sitz]  [Sitz]    |");
			Console.WriteLine("   --------------------------------------");
			Console.ResetColor();
		}

		static void DrawArgosDockingPort()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("             [ ARGOS – ANDOCKSEKTION ]");
			Console.WriteLine("      _________________________________________");
			Console.WriteLine("     |                                         |");
			Console.WriteLine("     |   <==== LANDUNGSSCHIFF ANDOCKT =====>   |");
			Console.WriteLine("     |_________________________________________|");
			Console.ResetColor();
		}

		static void DrawDeconChamberArgos()
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("        ====== ARGOS – DEKONTAMINATIONSZONE ======");
			Console.WriteLine("       |   Ionisationsfelder  |  Sprühdüsen      |");
			Console.WriteLine("       |   [####]     [####]  |  [ Nebel ]       |");
			Console.WriteLine("       |_________________________________________|");
			Console.ResetColor();
		}

		static void DrawTeleporterPadArgos()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("        ARGOS – Taktische Teleporter-Plattform");
			Console.WriteLine("        ======================================");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("             _________________");
			Console.WriteLine("            /                 \\");
			Console.WriteLine("           /   [   PAD   ]     \\");
			Console.WriteLine("          /_____________________\\");
			Console.WriteLine("          |  Zusatzgeneratoren  |");
			Console.WriteLine("          |   provisorische     |");
			Console.WriteLine("          |   Verkabelung       |");
			Console.WriteLine("          |_____________________|");
			Console.ResetColor();
		}

		static void DrawTeleporterHubGenesis()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("           GENESIS – HAUPT-TRANSPORTERSEKTION");
			Console.WriteLine("          ====================================");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("        Kreisförmige Plattform, eingelassene Energieringe,");
			Console.WriteLine("        darüber die Projektoren für das Dekontaminationsfeld.");
			Console.ResetColor();

			Scene_DieSuche();
			return;
		}
		static void Scene_DieSuche()
		{
			// Speicherpunkt vor der Szene
			SaveGame("Scene_DieSuche_Begin");

			Console.Clear();
			TypeText("Langsam wache ich auf. Zum ersten Mal seit langer Zeit fühlt sich das Quartier nicht kalt und leer an.", 10);
			TypeText("Neben mir liegt meine Frau, ruhig atmend. Ein kurzer Moment, in dem all das, was außerhalb der GENESIS lauert, weit weg wirkt.", 10);
			TypeText("");

			TypeText("Ich löse mich vorsichtig aus der Decke, um sie nicht zu wecken, und gehe in die Nasszelle.", 10);
			TypeText("Das Wasser ist warm, die Müdigkeit spült es nur teilweise von mir runter. Der Rest bleibt – Verantwortung lässt sich nicht abwaschen.", 10);
			TypeText("");

			TypeText("Nach der Dusche ziehe ich die frische Uniform an.", 10);
			TypeText("Die neuen Rangabzeichen fühlen sich noch ungewohnt an – Captain.", 10);
			TypeText("Ein Titel, den ich nie haben wollte. Aber jetzt trage ich ihn.", 10);
			TypeText("");

			TypeText("Ich werfe einen letzten Blick auf meine Frau. Sie bewegt sich leicht im Schlaf.", 10);
			TypeText("Dann verlasse ich leise das Quartier. Die Tür gleitet zu.", 10);
			TypeText("");
			Console.WriteLine("\n[Weiter mit Taste]");
			Console.ReadKey();

			// Turbolift-Sequenz zur Brücke (nutzt deine existierende Grafik)
			Console.Clear();
			DrawTurboLiftRoute();
			TypeText("");
			TypeText("Der Turbolift setzt sich in Bewegung. Hinauf, hinunter, quer durchs Schiff – ein stählernes Labyrinth auf Schienen.", 10);
			TypeText("Decks ziehen im Statusdisplay vorbei: 47… 36… Sektion B… 12… 5… 3… 1.", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um auf der Brücke anzukommen...");
			Console.ReadKey();

			Console.Clear();
			DrawBridgeSilhouette();
			TypeText("");

			TypeText("KADE: „Captain auf der Brücke!“", 10);
			TypeText("Die Gespräche verstummen. Augen richten sich auf mich – müde, aber voller Erwartung.", 10);
			TypeText("");

			TypeText("Ich trete zur zentralen Konsole.", 10);
			TypeText("Commander: „Statusbericht.“", 10);
			TypeText("");

			TypeText("KADE: „Alle Hauptsysteme sind wieder einsatzbereit. Schilde stabil, Antrieb im grünen Bereich, Sensoren kalibriert.“", 10);
			TypeText("KADE: „Die Flotte meldet ebenfalls volle Einsatzbereitschaft. Wir warten auf Ihren Befehl, Captain.“", 10);
			TypeText("");

			Console.WriteLine("\n[Weiter mit Taste]");
			Console.ReadKey();

			TypeText("Ich atme tief durch.", 10);
			TypeText("Commander: „Wir beginnen mit der Suche nach dem zehnten Planeten.“", 10);
			TypeText("Commander: „Nutzen Sie die vermutlichen Koordinaten aus den Atlaner-Relikten. Kurs setzen. Unterlichttriebwerke auf Maximum.“", 10);
			TypeText("");

			TypeText("Steuermann: „Aye, Captain. Kurs ist gesetzt. Flotte folgt. Beschleunigung beginnt.“", 10);
			TypeText("");

			// Flugsequenz
			Console.Clear();
			DrawTravelToTenthPlanet();
			TypeText("");
			TypeText("Die Sterne ziehen sich zu stillen Linien, während die GENESIS und ihre Begleiter durch die Dunkelheit schneiden.", 10);
			TypeText("Minuten, Stunden – in einem Schiff wie diesem verschwimmt die Zeit.", 10);
			TypeText("");

			TypeText("Schließlich verlangsamt der Antrieb. Die Anzeigen gehen auf Normalbetrieb.", 10);
			TypeText("Steuermann: „Captain, wir haben die berechnete Position erreicht.“", 10);
			TypeText("");

			Console.Clear();
			DrawEmptySpaceAtTarget();
			TypeText("");

			TypeText("Die Frontscheibe zeigt nur eins: Nichts.", 10);
			TypeText("Kein Planet. Keine Station. Kein Ring. Nur kalte, schwarze Leere.", 10);
			TypeText("");

			TypeText("Wissenschaftsoffizier: „Captain… ich finde nichts. Keine Masse, keine Signatur, keine Abweichung im Gravitationsfeld.“", 10);
			TypeText("KADE: „Die Koordinaten stimmen. Aber es ist, als wäre hier einfach nichts.“", 10);
			TypeText("");

			TypeText("Commander: „Oder jemand will, dass wir glauben, hier sei nichts.“", 10);
			TypeText("");

			TypeText("Ich beuge mich über die Sensorkonsole.", 10);
			TypeText("Commander: „Wenn der Planet getarnt ist, dann finden wir ihn über Muster und Störungen.“", 10);
			TypeText("Commander: „Bringen Sie das erweiterte Sensorscan-Gitter online. Wir suchen nach jeder Anomalie.“", 10);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um das Sensor-Minispiel zu starten...");
			Console.ReadKey();

			// Minispiel: versteckten Planeten im Sensor-Gitter finden
			bool planetGefunden = SensorMinigame_FindHiddenPlanet();

			Console.Clear();

			if (!planetGefunden)
			{
				// Falls wir später einen Misserfolgspfad wollen, könnten wir hier GameOver einbauen.
				// Für jetzt: Er zwingt die Crew, weiterzumachen, bis sie es schaffen.
				TypeText("KADE: „Wir können den Planeten nicht lokalisieren, Captain. Die Daten sind zu diffus.“", 10);
				TypeText("Commander: „Dann fangen wir von vorne an. Wir hören nicht auf, bevor wir ihn gefunden haben.“", 10);
				TypeText("");
				TypeText("Fortsetzung folgt…", 10);

				SaveGame("Scene_DieSuche_End");
				Console.ReadKey();
				return;
			}

			// Erfolg: Planet wird sichtbar
			DrawHiddenPlanetSilhouette();
			TypeText("");

			TypeText("Wissenschaftsoffizier: „Da! Das Muster kollabiert, wir haben eine klare Signatur!“", 10);
			TypeText("KADE: „Masse bestätigt. Atmosphärenwerte im bewohnbaren Bereich. Der Planet war in einem art Tarnfeld eingehüllt.“", 10);
			TypeText("");

			TypeText("Vor uns beginnt sich die Dunkelheit zu verändern.", 10);
			TypeText("Etwas Großes schält sich aus dem Schwarz, zuerst nur eine Krümmung, dann eine komplette Kugel und ein riesiger Ring um den Planeten.", 10);
			TypeText("");

			TypeText("Steuermann: „Kontakt zum Hauptkörper hergestellt. Wir sind in sicherer Distanz.“", 10);
			TypeText("");

			TypeText("Ich starre auf den Bildschirm. Das muss er sein. Der zehnte Planet.", 10);
			TypeText("");

			TypeText("Commander: „Alle Systeme auf Bereitschaft. Wir nehmen Kontakt auf.“", 10);
			TypeText("KADE: „Kommunikationskanäle stehen bereit, Captain.“", 10);
			TypeText("");

			TypeText("Was auch immer uns hier erwartet, es wird alles verändern.", 10);
			TypeText("");

			// Speicherpunkt am Ende der Szene
			SaveGame("Scene_DieSuche_End");
			TypeText("Fortsetzung folgt…", 10);
			Console.ReadKey();
		}
		// SENSOR-MINISPIEL: VERSTECKTEN PLANETEN IM SCAN-GITTER FINDEN
		static bool SensorMinigame_FindHiddenPlanet()
		{
			Console.Clear();
			TypeText("KADE: „Erweitertes Sensorscan-Gitter ist online, Captain.“", 10);
			TypeText("Wissenschaftsoffizier: „Wir sehen nur Rauschen, irgendwo darin muss der Planet versteckt sein.“", 10);
			TypeText("");

			const int rows = 8;
			const int cols = 12;

			char[,] grid = new char[rows, cols];
			bool[,] alreadyScanned = new bool[rows, cols];

			for (int r = 0; r < rows; r++)
				for (int c = 0; c < cols; c++)
					grid[r, c] = '.';

			Random rnd = new Random();
			int planetRow = rnd.Next(0, rows);
			int planetCol = rnd.Next(0, cols);

			int scans = 0;

			while (true)
			{
				Console.Clear();
				DrawSensorScanGrid(grid, rows, cols);
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Ziel: Lokalisieren Sie die versteckte Gravitationsanomalie (den Planeten).");
				Console.ResetColor();
				Console.WriteLine("Geben Sie eine Koordinate ein (z.B. C7) – oder X zum Abbrechen (keine echte Option 😉).");
				Console.WriteLine();

				Console.Write("Eingabe: ");
				string input = Console.ReadLine().Trim().ToUpper();

				if (input == "X")
				{
					TypeText("Commander: „Abbrechen ist keine Option. Wir versuchen es weiter.“", 10);
					Thread.Sleep(600);
					continue;
				}

				if (input.Length < 2 || input.Length > 3)
				{
					TypeText("KADE: „Ungültiges Format. Buchstabe + Zahl, Captain. (z.B. C7)“", 10);
					Thread.Sleep(600);
					continue;
				}

				char rowChar = input[0];
				if (rowChar < 'A' || rowChar >= 'A' + rows)
				{
					TypeText("Wissenschaftsoffizier: „Diese Zeile liegt außerhalb unseres Scanbereichs.“", 10);
					Thread.Sleep(600);
					continue;
				}

				if (!int.TryParse(input.Substring(1), out int colNum))
				{
					TypeText("KADE: „Das ist keine gültige Zahl.“", 10);
					Thread.Sleep(600);
					continue;
				}

				int rIndex = rowChar - 'A';
				int cIndex = colNum - 1;

				if (cIndex < 0 || cIndex >= cols)
				{
					TypeText("Wissenschaftsoffizier: „Spalte außerhalb des Gitterbereichs.“", 10);
					Thread.Sleep(600);
					continue;
				}

				if (alreadyScanned[rIndex, cIndex])
				{
					TypeText("KADE: „Diese Koordinate hatten wir schon im Scan, Captain.“", 10);
					Thread.Sleep(600);
					continue;
				}

				scans++;
				alreadyScanned[rIndex, cIndex] = true;

				Console.WriteLine();
				TypeText("Sensorimpuls läuft…", 10);
				Thread.Sleep(500);

				if (rIndex == planetRow && cIndex == planetCol)
				{
					grid[rIndex, cIndex] = 'P';
					Console.Clear();
					DrawSensorScanGrid(grid, rows, cols);
					Console.WriteLine();
					TypeText("Wissenschaftsoffizier: „Volle Resonanz! Das ist er – der getarnte Planet!“", 10);
					TypeText($"KADE: „Bestätigung. Wir haben ihn nach {scans} Scans gefunden.“", 10);
					Thread.Sleep(800);
					return true;
				}
				else
				{
					// Hinweis über Entfernung
					int distRow = Math.Abs(planetRow - rIndex);
					int distCol = Math.Abs(planetCol - cIndex);
					int dist = distRow + distCol;

					grid[rIndex, cIndex] = 'x';

					if (dist >= 10)
					{
						TypeText("Wissenschaftsoffizier: „Kaum messbare Abweichung. Wenn dort etwas ist, ist es sehr weit entfernt von dieser Koordinate.“", 10);
					}
					else if (dist >= 6)
					{
						TypeText("Wissenschaftsoffizier: „Sehr schwache Gravitationsschwankung. Wir sind weit weg von der Hauptanomalie.“", 10);
					}
					else if (dist >= 3)
					{
						TypeText("KADE: „Es gibt messbare Abweichungen. Wir kommen näher – aber das ist noch nicht der Kernbereich.“", 10);
					}
					else
					{
						TypeText("Wissenschaftsoffizier: „Starke Störung im Feld! Wir sind sehr nah an der Hauptanomalie.“", 10);
					}

					Thread.Sleep(800);
				}
			}
		}

		// ASCII-GRAFIKEN FÜR DIE SZENE & DAS MINISPIEL
		static void DrawTravelToTenthPlanet()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("GENESIS-FLOTTE AUF DEM WEG ZU DEN KOORDINATEN DES 10. PLANETEN");
			Console.WriteLine("    >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("       >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("          >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
			Console.ResetColor();
		}

		static void DrawEmptySpaceAtTarget()
		{
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("                 ZIELGEBIET – LEERER RAUM");
			Console.WriteLine("   -------------------------------------------------");
			Console.WriteLine("                 (keine Planeten sichtbar)");
			Console.WriteLine("                 (kein Ring, keine Station)");
			Console.WriteLine("                 nur kalte, stille Dunkelheit");
			Console.ResetColor();
		}

		static void DrawSensorScanGrid(char[,] grid, int rows, int cols)
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("============= ERWEITERTES SENSOR-SCAN-GITTER =============");
			Console.ResetColor();

			Console.Write("      ");
			for (int c = 1; c <= cols; c++)
			{
				Console.Write($"{c,2} ");
			}
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("     +");
			for (int c = 0; c < cols; c++)
				Console.Write("--");
			Console.WriteLine("+");
			Console.ResetColor();

			for (int r = 0; r < rows; r++)
			{
				char rowLabel = (char)('A' + r);
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write($"  {rowLabel}  |");
				Console.ResetColor();

				for (int c = 0; c < cols; c++)
				{
					char ch = grid[r, c];
					switch (ch)
					{
						case 'P':
							Console.ForegroundColor = ConsoleColor.Green;
							break;
						case 'x':
							Console.ForegroundColor = ConsoleColor.DarkGray;
							break;
						default:
							Console.ForegroundColor = ConsoleColor.DarkBlue;
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
			Console.Write("     +");
			for (int c = 0; c < cols; c++)
				Console.Write("--");
			Console.WriteLine("+");
			Console.ResetColor();
		}

		static void DrawHiddenPlanetSilhouette()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("                 SCHWACHER UMRISS IM SCHWARZEN RAUM");
			Console.WriteLine("                      .-''''''''''-.");
			Console.WriteLine("                   .-'              '-.");
			Console.WriteLine("                  /    dunkle Kugel    \\");
			Console.WriteLine("                 |    im Tarnfeld       |");
			Console.WriteLine("                  \\                    /");
			Console.WriteLine("                   '-.              .-'");
			Console.WriteLine("                      '-.______.-'");
			Console.ResetColor();


			SaveGame("Scene_ErstkontaktGrauer_Begin");
			Scene_ErstkontaktGrauer();
			return;

		}
		static void Scene_ErstkontaktGrauer()
		{
			// Speicherpunkt vor der Szene
			SaveGame("Scene_ErstkontaktGrauer_Begin");

			Console.Clear();
			DrawRingworldView();
			TypeText("", 15);

			TypeText("Ich starre auf den Hauptschirm.", 15);
			TypeText("Vor uns schwebt der Planet, eine gewaltige Kugel, in sanftem Dunkelblau und Grau, halb im Schatten, halb im Licht.", 15);
			TypeText("Doch das ist nicht das, was mich sprachlos macht.", 15);
			TypeText("", 15);

			TypeText("Ein gigantischer Ring umspannt den Planeten.", 15);
			TypeText("Er schließt sich wie eine künstliche Krone um die Welt, durchbrochen von Öffnungen und Durchlässen.", 15);
			TypeText("Auf der Innenseite des Rings erkenne ich Strukturen, Muster… Kontinente?", 15);
			TypeText("Es sieht aus, als wäre die Innenseite des Rings ebenso bewohnt wie die Oberfläche des Planeten.", 15);
			TypeText("", 15);

			TypeText("Die Atmosphäre des Planeten und die des Rings scheinen ineinander überzugehen.", 15);
			TypeText("Als hätte jemand zwei Welten übereinandergelegt, bis sie eine einzige, perfekte Illusion bilden.", 15);
			TypeText("", 15);

			TypeText("Für einen Moment vergesse ich, dass ich auf einer Brücke stehe.", 15);
			TypeText("Alles, was bleibt, ist diese unmögliche Konstruktion, mitten in der Dunkelheit.", 15);
			TypeText("", 15);
			Console.WriteLine("\n[Weiter mit Taste...]");
			Console.ReadKey();
			Console.Clear();

			Console.Beep(500, 180);
			Console.Beep(550, 180);

			Console.Clear();
			DrawGreyFleetAroundPlanet();
			TypeText("", 15);

			TypeText("Ich werde aus meinen Gedanken gerissen, als sich plötzlich dutzende Kontakte vor unserem Bug materialisieren.", 15);
			TypeText("Schiffe, kleiner als die GENESIS, aber in ihrer Anzahl überwältigend.", 15);
			TypeText("", 15);

			TypeText("KADE: \"Captain, dutzende Signaturen! Sie kommen direkt aus dem Ringfeld!\"", 15);
			TypeText("KADE: \"Energiestrukturen fahren hoch, ich sehe, laut Sensoren Schilde und Waffensysteme auf mehreren Frequenzen hochfahren.\"", 15);
			TypeText("", 15);

			TypeText("Bevor ich antworten kann, ertönt ein Signal über die Brücke.", 15);
			Console.Beep(700, 200);
			Console.Beep(650, 200);

			TypeText("KADE: \"Sie rufen uns, Captain.\"", 15);
			TypeText("", 15);
			Console.WriteLine("\n[Weiter mit Taste...]");
			Console.ReadKey();
			Console.Clear();
			Console.Clear();
			DrawGreyTransmissionScreen();
			TypeText("", 15);

			TypeText("Auf dem Schirm erscheint ein Gesicht.", 15);
			TypeText("Ein Grauer im Vordergrund, im diffusen Licht, dahinter vier weitere Silhouetten, nur schemenhaft zu erkennen.", 15);
			TypeText("", 15);

			TypeText("Die Augen sind groß, tiefschwarz und vollkommen reglos.", 15);
			TypeText("Die Haut ist glatt, grau, ohne sichtbare Haare, ohne Narben.", 15);
			TypeText("Sie tragen keinerlei Kleidung, keine Rüstung, keine Uniform, nichts, was auf Rang oder Geschlecht schließen ließe.", 15);
			TypeText("", 15);

			TypeText("Ich muss unwillkürlich schmunzeln, ein kurzer, fehlplatzierter Gedanke:", 15);
			TypeText("Selbst hier draußen sind wir die einzigen, die sich in Stoff und Metall hüllen.", 15);
			TypeText("Doch ich schiebe den Gedanken schnell beiseite. Jetzt ist nicht der Moment dafür.", 15);
			TypeText("", 15);

			TypeText("Der Graue schaut mich direkt an.", 15);
			TypeText("Seine Lippen bewegen sich nicht, doch eine Stimme erklingt.", 15);
			TypeText("Sie ist klar, deutlich und erstaunlich kräftig.", 15);
			TypeText("Ich kann nicht sagen, ob sie über die Lautsprecher kommt… oder direkt in meinem Kopf.", 15);
			TypeText("", 15);

			TypeText("Grauer: \"Was wollt ihr hier?\"", 15);
			TypeText("Grauer: \"Ihr habt hier nichts verloren.\"", 15);
			TypeText("", 15);

			TypeText("Ich spüre, wie mein Herzschlag schneller wird.", 15);
			TypeText("Trotzdem zwinge ich meine Stimme zur Ruhe.", 15);
			TypeText("", 15);

			TypeText("Commander: \"Wir sind hier, um Asyl zu suchen.\"", 15);
			TypeText("Commander: \"Euch ist sicher nicht entgangen, was auf unserem Heimatplaneten geschehen ist.\"", 15);
			TypeText("", 15);

			TypeText("Der Graue blinzelt nicht. Keine sichtbare Regung.", 15);
			TypeText("", 15);

			TypeText("Grauer: \"Wir beobachten euch seit langer Zeit.\"", 15);
			TypeText("Grauer: \"Wir begleiten euch, die angeblich direkten Nachfahren der Blutsväter.\"", 15);
			TypeText("Grauer: \"Und doch überrascht ihr uns immer wieder damit, wie primitiv ihr seid.\"", 15);
			TypeText("", 15);

			TypeText("Seine Worte schneiden tiefer, als ich zugeben möchte.", 15);
			TypeText("", 15);

			TypeText("Grauer: \"Ihr habt eine Seuche entfesselt, die euer Erbe verschlingt.\"", 15);
			TypeText("Grauer: \"Ihr könnt froh sein, dass wir neutral sind.\"", 15);
			TypeText("Grauer: \"Andere Nachfahren der Blutsväter… und manche Allianzen… sind es nicht.\"", 15);
			TypeText("Grauer: \"Im Gegenteil, sie empfinden euch mittlerweile als Gefahr.\"", 15);
			TypeText("", 15);

			TypeText("Ich atme langsam ein, langsam aus.", 15);
			TypeText("Commander: \"Wir sind der letzte Rest der Menschheit von diesem Planeten.\"", 15);
			TypeText("Commander: \"Es bleibt uns nichts anderes übrig.\"", 15);
			TypeText("", 15);

			TypeText("Zum ersten Mal scheint der Graue einen Moment lang innezuhalten.", 15);
			TypeText("", 15);

			TypeText("Grauer: \"Ihr seid hier unerwünscht.\"", 15);
			TypeText("Grauer: \"Es ist besser, wenn ihr jetzt fortgeht.\"", 15);
			TypeText("", 15);

			TypeText("Commander: \"Und wohin sollen wir gehen?\"", 15);
			TypeText("", 15);

			TypeText("Ein kaum wahrnehmbares Zucken geht über seine Gesichtszüge, vermutlich eher Einbildung.", 15);
			TypeText("", 15);

			TypeText("Grauer: \"Ihr habt doch auf einem der Jupiter-Monde eine Siedlung errichtet.\"", 15);
			TypeText("Grauer: \"Eine Kolonie eurer Art.\"", 15);
			TypeText("", 15);

			TypeText("Ich starre ihn an.", 15);
			TypeText("Commander: \"Angesiedelt…?\"", 15);
			TypeText("", 15);

			TypeText("Grauer: \"Ihr wisst gar nichts davon.\"", 15);
			TypeText("Grauer: \"Das wundert mich nicht.\"", 15);
			TypeText("", 15);

			TypeText("Grauer: \"Nun gut. Jetzt habt ihr ein Ziel.\"", 15);
			TypeText("Grauer: \"Entfernt euch so schnell wie möglich aus dem Orbit und vergesst schnell das ihr hier gewesen seid.\"", 15);
			TypeText("Grauer: \"Andernfalls müsst ihr mit Konsequenzen rechnen.\"", 15);
			TypeText("", 15);
			Console.WriteLine("\n[Weiter mit Taste...]");
			Console.ReadKey();
			Console.Clear();
			Console.Beep(400, 200);
			Console.Beep(350, 200);

			Console.Clear();
			DrawTransmissionCut();
			TypeText("", 15);

			TypeText("Der Bildschirm wird schwarz.", 15);
			TypeText("Im nächsten Moment schaltet die Ansicht zurück nach außen.", 15);
			TypeText("", 15);
			Console.WriteLine("\n[Weiter mit Taste...]");
			Console.ReadKey();
			Console.Clear();
			Console.Clear();
			DrawGreyFleetAroundPlanet();
			TypeText("", 15);

			TypeText("Die fremden Schiffe formieren sich vor uns.", 15);
			TypeText("Sie sind deutlich kleiner als die GENESIS, aber jede einzelne Signatur wirkt wie ein Messer an unserer Kehle.", 15);
			TypeText("Technologisch liegen sie mit hoher Wahrscheinlichkeit weit über allem, was wir je gebaut haben.", 15);
			TypeText("", 15);

			TypeText("Oduro: \"Captain… was sollen wir jetzt tun?\"", 15);
			TypeText("Ihre Stimme ist leiser als sonst. Die sonst so sichere Offizierin klingt zum ersten Mal unsicher.", 15);
			TypeText("", 15);

			TypeText("Ich sehe noch einmal auf den Schirm, auf den Planeten, den Ring, die Flotte der Grauen.", 15);
			TypeText("Dann treffe ich die einzige Entscheidung, die uns bleibt.", 15);
			TypeText("", 15);

			TypeText("Commander: \"Kurs auf Jupiter setzen.\"", 15);
			TypeText("Commander: \"Ziel: der Mond mit der Kolonie, von der sie gesprochen haben.\"", 15);
			TypeText("", 15);

			TypeText("Oduro: \"Aye, Captain.\"", 15);
			TypeText("Oduro: \"Kurs auf den Jupiter. Übergabe an den Steuermann.\"", 15);
			TypeText("", 15);

			TypeText("Steuermann: \"Neuer Kurs gesetzt. Flotte übernimmt Formation.\"", 15);
			TypeText("Steuermann: \"Wir verlassen den Orbit des zehnten Planeten.\"", 15);
			TypeText("", 15);
			Console.WriteLine("\n[Weiter mit Taste...]");
			Console.ReadKey();
			Console.Clear();
			Console.Clear();
			DrawFleetCourseToJupiter();
			TypeText("", 15);

			TypeText("Die GENESIS dreht langsam ab.", 15);
			TypeText("Die Schiffe der Grauen bleiben auf der Stelle stehen, wie eine Mauer aus Schatten vor der Ringwelt.", 15);
			TypeText("Dann verschwindet das System aus unserem Sichtfeld.", 15);
			TypeText("", 15);

			TypeText("Vor uns liegt der Weg zum Jupiter.", 15);
			TypeText("Und irgendwo dort draußen, eine Kolonie, von der wir nie etwas wussten.", 15);
			TypeText("", 15);

			// Speicherpunkt nach der Szene
			SaveGame("Scene_ErstkontaktGrauer_End");

			TypeText("Fortsetzung folgt…", 15);
			Console.ReadKey();
		}

		// ASCII-GRAFIKEN FÜR DIE SZENE

		static void DrawRingworldView()
		{
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("                    HAUPTDISPLAY – ZIELWELT");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("                    .-\"\"\"\"\"\"\"\"\"\"-.");
			Console.WriteLine("                 .-'              '-.");
			Console.WriteLine("               .'    PLANETENKUGEL   '.");
			Console.WriteLine("              /                        \\");
			Console.WriteLine("             |                          |");
			Console.WriteLine("              \\                        /");
			Console.WriteLine("               '.                    .'");
			Console.WriteLine("                 '-.            .-'");
			Console.WriteLine("                     '--------'");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine();
			Console.WriteLine("                 ==========================");
			Console.WriteLine("                /                           \\");
			Console.WriteLine("               /      KÜNSTLICHER RING       \\");
			Console.WriteLine("              |  (Durchlässe, Landschaften)   |");
			Console.WriteLine("               \\                             /");
			Console.WriteLine("                \\===========================/");
			Console.ResetColor();
		}

		static void DrawGreyFleetAroundPlanet()
		{
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("                 RINGWELT MIT ORBITALEN KONTAKTEN");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("                     (   PLANET   )");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("                 ========[ RING ]=======");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine();
			Console.WriteLine("      <o>   <o>   <o>   <o>   <o>   <o>   <o>   <o>");
			Console.WriteLine("          kleine Schiffe der Grauen vor unserem Bug");
			Console.ResetColor();
		}

		static void DrawGreyTransmissionScreen()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                ================================");
			Console.WriteLine("                |       HAUPTDISPLAY           |");
			Console.WriteLine("                ================================");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("                |             ____         fgtd    |");
			Console.WriteLine("                |            /    \\           |");
			Console.WriteLine("                |           /  O O \\          |");
			Console.WriteLine("                |           |       |           |");
			Console.WriteLine("                |            |  <> |           |");
			Console.WriteLine("                |            \\____/           |");
			Console.WriteLine("                |        (Schatten im Hintergrund)");
			Console.WriteLine("                |______________________________|");
			Console.ResetColor();
		}

		static void DrawTransmissionCut()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("   [ÜBERTRAGUNG ABGEBROCHEN]");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("   ####################################");
			Console.WriteLine("   #  SIGNALRAUSCHEN  SIGNALRAUSCHEN  #");
			Console.WriteLine("   ####################################");
			Console.ResetColor();
		}

		static void DrawFleetCourseToJupiter()
		{
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("                   FLUGKURS – JUPITER");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("   [ZEHNTER PLANET] ----------------------------*");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("                                                *  (JUPITER)");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine();
			Console.WriteLine("   GENESIS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("   ARGOS   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("   Weitere Schiffe >>>>>>>>>>>>>>>>>>>>>>>>>");
			Console.ResetColor();

			Scene_JupiterEuropaKontakt();
			return;
		}
		static void Scene_JupiterEuropaKontakt()
		{
			SaveGame("Scene_JupiterEuropaKontakt");
			Console.Clear();

			// --- JUPITER ORBIT GRAFIK ---
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                    .-~~~~~~~~~~-.");
			Console.WriteLine("               .-~~     JUPITER     ~~-. ");
			Console.WriteLine("           .-~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~-. ");
			Console.WriteLine("        .-~                                 ~-. ");
			Console.WriteLine("       (      O    GENESIS – Flotte im Orbit    )");
			Console.WriteLine("        '-.                                 .-' ");
			Console.WriteLine("            '-.                         .-'");
			Console.WriteLine("                '-._______________ .-'");
			Console.ResetColor();

			TypeText("Wir erreichten den Orbit des gewaltigen Gasriesen Jupiter.", 15);
			TypeText("Die farbigen Wolkenschichten unter uns wirkten wie lebendige Strudel.", 15);
			TypeText("Einer der Monde, Europa schwebte still im Vordergrund, hell, kalt, funkelnd.", 15);


			TypeText("Kade: Captain… mehrere massive Energiesignaturen direkt unter Europas Eiskruste.", 15);
			TypeText("Kade: Und auch Ganymed und Kallisto senden ungewöhnlich starke Impulse.", 15);
			TypeText("Oduro: Wir sind nicht alleine.", 15);


			// --- ANNÄHERUNGSGRAFIK AN EUROPA ---
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("                     _________ ");
			Console.WriteLine("                _.-''         ''-._ ");
			Console.WriteLine("            _.-'   MOND EUROPA     '-._ ");
			Console.WriteLine("         .-'       *  *  *  *         '-.");
			Console.WriteLine("        (       GENESIS nähert sich      )");
			Console.WriteLine("         '-.                             .-'");
			Console.WriteLine("            '-._______________________.-'");
			Console.ResetColor();

			TypeText("Ich befehle der Flotte, den Mond näher zu überfliegen.", 15);
			TypeText("Europa füllte den Hauptschirm,ein makelloser, glatter Eiskörper.", 15);


			// --- SIGNALSTÖRUNG ---
			Console.Beep(300, 200);
			Console.Beep(250, 200);

			TypeText("Kommunikation: Captain! Unser Signal wird gestört Quelle unbekannt!", 15);
			TypeText("Kade: Die Störung wird stärker… jemand blockiert uns absichtlich.", 15);


			TypeText("Commander: ARGOS, verlegen Sie sich hinter Europa.", 15);
			TypeText("Restliche Flottille: Ringförmige Position einnehmen und Sensornetz verstärken.", 15);


			// --- ENERGIEKONTAKTE ---
			TypeText("Kade: Captain… neue Energiespuren!", 15);
			TypeText("Kade: Mehrere Schiffe erheben sich aus der Jupiter-Atmosphäre!", 15);

			// FLUGGRAFIK
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("           >>>   >>>   >>>   >>>");
			Console.WriteLine("     [UNBEKANNTE SCHIFFE STEIGEN AUF]");
			Console.ResetColor();

			TypeText("Sensoroffizier: Captain… das sind menschliche Schiffstypen.", 15);
			TypeText("Sensoroffizier: Das müssen dutzednde, vielleicht hunderte sein.", 15);


			// --- AUDIOKONTAKT (KEIN BILD) ---
			Console.Beep(500, 300);
			Console.Beep(450, 200);

			TypeText("Eine harte Stimme ertönte über die Lautsprecher, ohne Bild.", 15);

			TypeText("??? (über Audio): Ihr habt hier nichts verloren.", 15);
			TypeText("??? (über Audio): Brecht euren Kurs ab und verlasst diesen Planeten.", 15);

			TypeText("Ich war irritiert: Eine menschliche Stimme. Kein militärischer Ton, aber voller Autorität.", 15);


			TypeText("Kade: Captain! Immer mehr Schiffe tauchen auf, die Atmosphäre hat sie verdeckt!", 15);

			// --- ORBIT FLOTTENPOSITION ---
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("     [Flottenformation im Europa-Orbit]");
			Console.WriteLine();
			Console.WriteLine("               *    *    *");
			Console.WriteLine("           *        O        * ");
			Console.WriteLine("       *     FLOTTE & GENESIS    *");
			Console.WriteLine("           *                *");
			Console.WriteLine("               *    *    *");
			Console.ResetColor();

			TypeText("Oduro: Captain, wir sind umzingelt.", 15);

			// --- DER FÜHRER SPRICHT ---
			TypeText("??? (über Audio): Ich bin Oberkommandant Hale von der Jupiter-Kolonie.", 15);
			TypeText("Hale: Wir kennen eure Signatur. Die Erde ist dank eurem Gesindel gefallen. Ihr seid verseucht.", 15);
			TypeText("Hale: Wir dulden kein Risiko für unsere Bevölkerung.", 15);

			TypeText("Ich antwortete ruhig, aber entschlossen:", 15);
			TypeText("Wir sind die letzten Überlebenden. Wir suchen Schutz… und Antworten.‘", 15);

			TypeText("Hale: Das interessiert mich nicht. Ihr habt unsere Heimat zerstört. Ihr kommt uns nicht zu nahe.", 15);
			Console.WriteLine("\n[Weiter mit Taste...]");
			Console.ReadKey();
			Console.Clear();

			//                      ENTSCHEIDUNG

			Console.Clear();
			TypeText("Oduro: Captain… was sollen wir tun?", 15);
			TypeText("Ich wusste: Wir haben nur eine Chance.", 15);
			TypeText("Die Wahrheit offenlegen.", 15);

			Console.WriteLine("\nWie reagieren Sie?");
			Console.WriteLine("1) Ihm drohen mit der Waffengewalt der Genesis");
			Console.WriteLine("2) Demütig um Hilfe bitten");
			Console.WriteLine("3) Offenbaren, dass Sie Atlanter-Technologie aktivieren können");

			Console.Write("\nAuswahl: ");
			string choice = Console.ReadLine().Trim();

			if (choice == "1")
			{
				GameOver("Hale erklärt euch zur Bedrohung. Seine Verteidigungsflotte vernichtet die GENESIS in Sekunden.");
				return;
			}
			else if (choice == "2")
			{
				GameOver("Hale beendet die Übertragung. Kurz darauf gehen hunderte Schiffe in Angriffsformation.");
				return;
			}
			else if (choice != "3")
			{
				TypeText("Ungültige Eingabe.", 15);
				Scene_JupiterEuropaKontakt();
				return;
			}

			// --- DIE WAHRHEIT ---
			TypeText("Ich atmete tief ein und sprach klar:", 15);
			TypeText("Oberkommandant Hale… ich bin einer der wenigen, die die Systeme der Atlanter bedienen können.g", 15);
			TypeText("Die Atlanter-Anlage auf der Erde funktioniert wieder durch mich.", 15);


			TypeText("Stille. Dann ein hörbares Einatmen über die Verbindung.", 15);

			TypeText("Hale (leise): Ihr… habt die Systeme reaktiviert…?", 15);
			TypeText("Hale: Nach all den Jahrtausenden…", 15);

			TypeText("Seine Stimme veränderte sich. Härte wich Zweifel. Dann Respekt.", 15);

			TypeText("Hale: Dann seid ihr nicht nur Überlebende.", 15);
			TypeText("Hale: Ihr seid Schlüsselträger, der Blutsväter aber wisst vermutlich noch nicht warum.", 15);


			TypeText("Hale: Captain… ich gewähre Ihnen Zugang.", 15);
			TypeText("Hale: Aber nur Ihnen. Die GENESIS bleibt auf Distanz.", 15);
			TypeText("Hale: Wenn wir alles klären, dann sehen wir weiter.", 15);

			TypeText("Ich nickte entschlossen.", 15);
			TypeText("Bereiten Sie den Teleporter vor. Ich komme allein.", 15);


			// --- WEITER ZUR TELEPORTER-SZENE ---
			SaveGame("Scene_EuropaTeleport");
			Scene_EuropaKolonie();
			return;

		}
		static void Scene_EuropaKolonie()
		{
			// Speicherpunkt vor Szene
			SaveGame("Scene_EuropaKolonie");

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("=== TELEPORTER-SEQUENZ – ANKUNFT AUF EUROPA ===\n");
			Console.ResetColor();

			DrawTeleporterPlatform();
			TypeText("Ein greller Lichtblitz, ein Summen – dann spürte ich festen Boden unter meinen Füßen.", 15);
			TypeText("Der Geruch des Metalls war anders als auf der Genesis. Kälter. Steriler. Fremd.", 15);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um weiterzugehen...");
			Console.ReadKey();

			Console.Clear();
			DrawEuropaColonyHall();

			TypeText("Sofort richteten sich mehrere Sicherheitsoffiziere auf mich aus.", 15);
			TypeText("Schwarze Rüstungen. Keine Abzeichen. Keine Embleme. Nur ernste Blicke.", 15);
			TypeText("");
			TypeText("Ein Mann trat hervor – kräftig, älter, mit hartem Gesichtsausdruck.", 15);
			TypeText("„Willkommen auf Europa. Ich bin Hale.“", 15);
			TypeText("");

			TypeText("Ohne ein weiteres Wort bedeutete er mir, ihm zu folgen.", 15);

			Console.WriteLine("\nDrücke eine Taste, um mitzugehen...");
			Console.ReadKey();

			Console.Clear();
			DrawTurboLiftEuropa();

			TypeText("Der Turbolift rauschte nach oben. Niemand sprach. Nur Hales Blick ruhte auf mir.", 15);

			Console.WriteLine("\nTaste drücken...");
			Console.ReadKey();

			Console.Clear();
			DrawEuropaBriefingHall();

			TypeText("Hale führte mich in einen großen, schlichten Saal. Keine Dekoration. Keine Fenster.", 15);
			TypeText("Nur er, ich und einige seiner Stabsmitglieder.", 15);
			TypeText("");

			TypeText("Hale verschränkte die Arme: Sie wollen wissen, wie all das hier zustande kam.", 15);
			TypeText("Ich nickte nur.", 15);
			TypeText("");

			// --- HALE BEGINNT SEINE ERKLÄRUNG ---
			TypeText("Diese Kolonie existiert länger, als Sie vermutlich glauben. Ihre Wurzeln reichen bis ins 20. Jahrhundert, zumindest die Idee.", 15);
			TypeText("Damals erkannten einige von uns, dass die Regierungen kein Interesse daran hatten, die Menschheit wirklich weiterzubringen.", 15);
			TypeText("Es bildete sich ein geheimer Bund, ohne Könige, ohne Präsidenten, ohne Religionen. Einfach Menschen, die verstanden hatten, wohin es führte.", 15);
			TypeText("");

			TypeText("Ja, die elitären Kreise haben die Erde regiert, Politiker waren selten mehr als Marionetten.", 15);
			TypeText("Das war schon immer so. Kaiser, Könige, Präsidenten… Namen ändern sich, Muster bleiben.", 15);
			TypeText("Irgendwelche Arschlöcher, die meinen Gott spielen zu können und alles kontrollieren wollen.", 15);
			TypeText("");

			TypeText("Ich nickte:Das überrascht mich nicht.", 15);
			TypeText("");

			TypeText("Hale fuhr fort: Wir wussten schon vor Jahrzehnten, Jahrhunderten dass die Erde verloren war. Lange bevor der Virus, dieses Monster gefunden wurde.", 15);
			TypeText("Und lange bevor ihr ihn ‚Atlaner-Virus‘ genannt habt. Ein falscher Begriff.", 15);
			TypeText("Es ist kein Virus der Atlanter, es stammt von den Blutsvätern. Unseren genetischen Vorfahren und vermutlich aller anderen Völker in dieser Galaxie.", 15);
			TypeText("");

			TypeText("Wir hatten längst Kontakt zu den Grauen. Das ist für uns nichts Neues.", 15);
			TypeText("Wir wussten auch sofort, dass ihr sie besucht habt. Unsere Schiffe registrieren alles.", 15);
			TypeText("");

			TypeText("Ich schluckte und sagte nichts. Hale beobachtete mich genau.", 15);
			TypeText("");

			TypeText("Parallel zu eurer Mars-Kolonie bauten wir die wahre Kolonie auf, hier.", 15);
			TypeText("Auf Europa. Und auf mehreren Jupitermonden.", 15);
			TypeText("Eine eigene Flotte. Eigene Forschung. Eigene Gesellschaft.", 15);
			TypeText("");

			TypeText("Wir hatten keine Wahl. Die Erde war verloren, noch bevor die Eliten den Virus modifizierten.", 15);
			TypeText("Dieser Virus, den die Blutsväter aus einer anderen Galaxie mitgebracht haben… er hatte bereits ganze Galaxien ausgelöscht.", 15);
			TypeText("");

			TypeText("Die Nanobots haben ihn reaktiviert, aber noch nicht vollständig.", 15);
			TypeText("Ihr hattet Glück, dass die Grauen euch nicht vernichtet haben. Ihr wart an einem Ort, an den ihr kein Recht habt, laur deren Meinung.", 15);
			TypeText("");

			TypeText("Ich runzelte die Stirn: Warum nicht?", 15);
			TypeText("");

			TypeText("Hale trat einen Schritt näher. Weil das kein Planet ist. Was Sie gesehen haben, war eine Einrichtung der Blutsväter.", 15);
			TypeText("Eine Waffe. Wie auf der Erde nur viel größer. Es soll sechs dieser Systeme in unserer Galaxie angeblich geben.", 15);
			TypeText("Sie kann verschieden Impulse senden, alle sechs kombiniert um eine Welle zu erzeugen.", 15);
			TypeText("");
			TypeText("Dieser Impuls kann eingestellt werden. Entweder wird jede Technologie, jedes Leben zerstört oder die ganze Galaxie.", 15);
			TypeText("So ist es laut den Übersetzungen. Und nur die direkten Nachfahren können das aktivieren durch Ihre Gene und.....", 15);
			TypeText("Einen Schlüssel für die galaktischen Waffen. Für die planetaren reichen ihre, unsere Gene.", 15);
			TypeText("");
			TypeText("Deswegen lassen uns Die Grauen und die anderen Völker in Ruhe. Sie können diese Systeme nicht aktivieren.", 15);
			TypeText("Die Blutsväter wussten, wenn diese Seuche, es kann nur aus der Höhle kommen, ausbricht muss man die gesamte Galaxie säubern.", 15);
			TypeText("Diese Seuche passt sich an alles an. Menschen, Tiere, Pflanzen. Verschmelzt alles zu einem riesigen Dämon, wenn komplett aktiviert.", 15);
			TypeText("Die Blutsväter brachten es mit. Haben experimentiert. Versuchten es zu zähmen, wollten die Unsterblichkeit erreichen.", 15);
			TypeText("");

			TypeText("Mir blieb die Stimme weg. Hale wandte sich halb ab.", 15);
			TypeText("");
			TypeText("Ich versuchte das Gespräch fort zu führen: Was ist mit euren Schiffen? Ich sah baugleiche zur Universum Klasse.", 15);
			TypeText("Hale drehte sich zu mir: Die Technologie ist unsere, nicht von den Vorfahren. Klar das Wissen half dabei.", 15);
			TypeText("Die Raumfahrt, die Waffen, Energiequellen und auch Teleportieren. All das haben wir schon länger als sie denken.", 15);
			TypeText("Wie immer gab es Veräter und Spione und gaben es euch weiter an die Erdbewohner.", 15);
			TypeText("Ich sagte erstaunt: Dann habt ihr schon öfters das Sonnensystem verlassen. Das bedeutet...", 15);
			TypeText("Er unterbrach mich...", 15);


			TypeText("...Genug davon. Kommen Sie.", 15);
			TypeText("„Wir müssen einen Test durchführen.", 15);

			Console.WriteLine("\nDrücke eine Taste für den Test...");
			Console.ReadKey();

			// --- GENETISCHER TEST – MINIGAME ---
			Console.Clear();
			TypeText("Hale: „Dieser Test zeigt, ob Sie genetische Spuren der Blutsväter tragen.", 15);
			TypeText("Nur dann konnten Sie die Anlage bedienen.“", 15);
			TypeText("");

			RunGeneticTestMiniGame(); // Minigame aufrufen

			Console.Clear();
			TypeText("Hale starrte auf die Ergebnisse. Seine Miene wurde weicher.", 15);
			TypeText("Wie erwartet… Sie tragen deutliche Anteile der Blutsväter. Natürlich haben wir alle welche in uns. Aber machne, wie Sie mehr.", 15);
			TypeText("Deshalb konnten Sie die Waffe aktivieren. Und deshalb brauchen wir Sie jetzt.", 15);
			TypeText("");

			TypeText("Ich atmete langsam aus. Und was genau ist meine Aufgabe?", 15);
			TypeText("");

			TypeText("Hale: Ihr Schiff, die Genesis, wird repariert. In der Zwischenzeit fliegen Sie mit der Argos.", 15);
			TypeText("Sie kehren zur Erde zurück.se", 15);
			TypeText("Denn Sie haben etwas Entscheidendes vergessen.", 15);
			TypeText("");

			TypeText("Ich runzelte die Stirn: Was denn?", 15);
			TypeText("");

			TypeText("Hale verschränkte die Arme erneut.", 15);
			TypeText("Die Waffe neutralisierte das biologische Virus, aber nicht die Naniten.", 15);
			TypeText("Der zweite Teil des Systems ist speziell dafür gedacht, jegliche technologische Infrastruktur zu löschen.", 15);
			TypeText("Ihr Auftrag: Die Naniten-Kontrollkerne auf der Erde zerstören.", 15);
			TypeText("Nur Sie können die Anlage wieder auslösen, zumindest sind Sie der einzige wo gleichzeitig Soldat ist mit hohem Genanteil", 15);
			TypeText("");

			Console.WriteLine("\nDrücke eine Taste, um zum Teleporter zu gehen...");
			Console.ReadKey();

			Console.Clear();
			DrawTeleporterPlatform();
			TypeText("Zwei Sicherheitskräfte brachten mich zurück zur Plattform.", 15);
			TypeText("Ein Summen baute sich auf. Licht umhüllte meinen Körper.", 15);
			TypeText("Europa verschwand. Und die Argos tauchte vor mir auf.", 15);

			// Speicherpunkt nach der Szene
			SaveGame("Scene_EuropaKolonie_End");

			return;
		}

		// ASCII-GRAFIKEN (Teleporter, Koloniehalle, Turbolift, Saal)

		static void DrawTeleporterPlatform()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("        ____________________________");
			Console.WriteLine("       |                            |");
			Console.WriteLine("       |     ◇  TELEPORTER  ◇       |");
			Console.WriteLine("       |____________________________|");
			Console.WriteLine("              |     |     |");
			Console.WriteLine("            \\ |     |     | /");
			Console.WriteLine("             \\|     |     |/");
			Console.WriteLine("              ▼     ▼     ▼");
			Console.ResetColor();
		}

		static void DrawEuropaColonyHall()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("    _________________________________________________");
			Console.WriteLine("   |                                                 |");
			Console.WriteLine("   |      ███   ███   ███   ███   ███   ███          |");
			Console.WriteLine("   |                                                 |");
			Console.WriteLine("   |      [ EUROPA-KOLONIE – ANKUNFTSHALLE ]         |");
			Console.WriteLine("   |_________________________________________________|");
			Console.ResetColor();
		}

		static void DrawTurboLiftEuropa()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("   _________");
			Console.WriteLine("  |  LIFT  |   ►►►  Steigt auf höhere Ebenen");
			Console.WriteLine("  |_______|");
			Console.WriteLine("     ||");
			Console.WriteLine("     ||");
			Console.WriteLine("     VV");
			Console.ResetColor();
		}

		static void DrawEuropaBriefingHall()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("     _______________________________________________");
			Console.WriteLine("    |                                               |");
			Console.WriteLine("    |          ◇  BRIEFING-SAAL EUROPA  ◇           |");
			Console.WriteLine("    |     (Silhouetten stehen in der Dunkelheit)    |");
			Console.WriteLine("    |_______________________________________________|");
			Console.ResetColor();
		}

		// MINI-GAME – GENETISCHER TEST (X-FINDER)

		static void RunGeneticTestMiniGame()
		{
			Random rng = new Random();

			string[] module = { "A", "B", "C", "D", "E" };
			int pos = rng.Next(0, 5); // Position des X
			module[pos] = "X";

			bool success = false;

			while (!success)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("=== GENETISCHER VERIFIKATIONSTEST – BLUTSVÄTER ===\n");
				Console.ResetColor();

				TypeText("Finden Sie das mutierte Gen-Segment (X).", 15);
				Console.WriteLine();

				for (int i = 0; i < 5; i++)
				{
					Console.Write($"[{module[i]}] ");
				}

				Console.Write("\n\nPosition eingeben (1–5): ");
				string input = Console.ReadLine();

				if (int.TryParse(input, out int choice))
				{
					choice--;

					if (choice == pos)
					{
						TypeText("\nGen-Segment identifiziert…", 15);
						Console.Beep(700, 200);
						Thread.Sleep(500);
						TypeText("DNA-Analyse vollständig. Ergebnis: BLUTSVÄTER-KOMPATIBEL.", 15);
						success = true;
					}
					else
					{
						TypeText("\nFalsches Segment. Analyse neu kalibrieren…", 15);
						Console.Beep(300, 200);
						Thread.Sleep(800);
					}
				}
				else
				{
					TypeText("\nUngültige Eingabe.", 15);
					Thread.Sleep(500);
				}
			}

			Console.WriteLine("\nDrücke eine Taste, um fortzufahren...");
			Console.ReadKey();

			Scene_Blutsvaeter_Entrance();
			return;
		}
		static void Scene_Blutsvaeter_Entrance()
		{
			Console.Clear();
			SaveGame("Scene_Blutsvaeter_Entrance");

			TypeText("ARGOS – Orbit über der vernarbten Erde.", 12);
			TypeText("Das Schiff schneidet ruhig durch den Orbit, während an dem Landungsschiff Wolken entlang gleiten.", 12);
			TypeText("Ich stehe in der Landungsbucht, Kampfanzug verriegelt, Schilde hochgefahren.", 12);
			TypeText("");

			TypeText("Ren (über Interkom): Captain, wir halten stabile Umlaufbahn über der alten Blutsväter-Anlage.", 12);
			TypeText("Ren: Teleporterfelder sind immer noch gestört – Reststrahlung der Waffe. Sie müssen mit dem Landungsschiff runter.", 12);
			TypeText("");

			TypeText("Ich nickte, obwohl Ren mich nicht sehen konnte.", 12);
			TypeText("Commander: Landungsschiff klar machen. Zielkoordinaten: Blutsväter-Komplex Alpha.", 12);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um den Abstieg zur Oberfläche zu starten...");
			Console.ReadKey();

			Console.Clear();
			DrawDropshipDescent();
			TypeText("");
			TypeText("Das Landungsschiff durchbricht die Atmosphäre, Turbulenzen rütteln an der Hülle.", 12);
			TypeText("Unter uns breitet sich ein totes Land aus – verbrannte Städte, verkrümmte Ruinen, schimmernde Nanonebel.", 12);
			TypeText("");

			TypeText("Pilot: Ziel in Sicht, Captain. Alte Einsatzzone. Die Anlage steht noch.", 12);
			TypeText("Commander: Setzen Sie uns direkt vor dem Haupteingang ab.", 12);
			TypeText("");

			Console.WriteLine("Drücke eine Taste, um vor dem Blutsväter-Tor zu landen...");
			Console.ReadKey();

			Console.Clear();
			DrawBlutsvaeterGateClosed();
			TypeText("");
			TypeText("Wir treten aus dem Dropship. Der Wind trägt verbrannten Staub und das ferne Echo vergangener Schlachten.", 12);
			TypeText("Vor uns ragt der monolithische Eingang der Blutsväter-Anlage empor. Die Tür ist verriegelt, ein bläuliches Feld flackert in der Spalte.", 12);
			TypeText("");

			TypeText("KI (über Anzug): „Identifikation erforderlich. Primäre Tests aktiviert.“", 12);
			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste für den ersten Test (Tic-Tac-Toe)...");
			Console.ReadKey();

			// TEST 1 – TIC TAC TOE (Minigame aus deiner Minigames-Klasse)
			Console.Clear();
			TypeText("KI: „Primärtest: Logikanalyse.“", 12);

			// Erster Versuch
			bool passed = Minigames.TicTacToe.Play();

			if (!passed)
			{
				TypeText("KI: „Logiktest gescheitert. Wiederholung wird erzwungen.“", 12);
				Console.WriteLine("Drücke eine Taste, um den Test zu wiederholen...");
				Console.ReadKey();

				// Zweiter Versuch – hier kannst du später auch GameOver draus machen, wenn du willst
				passed = Minigames.TicTacToe.Play();
			}

			TypeText("KI: „Primärtest bestanden. Sekundärer Muster-Test wird vorbereitet…“", 12);
			Console.ReadKey();
			// TEST 2 – X-FINDER 
			RunBlutsvaeterXFinderTest();

			Console.Clear();
			DrawBlutsvaeterGateOpen();
			TypeText("");
			TypeText("Die Tür glitt lautlos zur Seite. Ein kalter Luftzug strich uns entgegen.", 12);
			TypeText("KI: „Genetische Struktur bestätigt. Abkömmling der Blutsväter-Linie erkannt. Zugang wird gewährt…“", 12);
			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um die Anlage zu betreten...");
			Console.ReadKey();
			// LABYRINTH – BLUTSVÄTER-KOMPLEX MIT ANDROIDEN
			RunBlutsvaeterLabyrinth();
			// FINALE FLUCHT & NANITEN
			Scene_Blutsvaeter_Finale();
		}
		//   TEST 2 – X-FINDER: GENETISCHE MUSTERERKENNUNG
		static void RunBlutsvaeterXFinderTest()
		{
			Console.Clear();
			TypeText("KI: „Identifikation fortsetzen. Finden Sie das markierte Modul in jeder Reihe.“", 12);
			TypeText("KI: „Nur bei korrekter Auswahl wird der volle Zugang freigegeben.“", 12);
			TypeText("");

			// Drei Reihen, jeweils genau ein 'X'
			string[][] muster = new string[][]
			{
		new [] { "A", "B", "C", "X", "D" },
		new [] { "C", "D", "X", "B", "A" },
		new [] { "X", "A", "C", "D", "B" }
			};

			for (int row = 0; row < muster.Length; row++)
			{
				bool correct = false;

				while (!correct)
				{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("=== BLUTSVÄTER-MUSTERERKENNUNG ===\n");
					Console.ResetColor();

					Console.WriteLine($"Reihe {row + 1}:");
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write("   ");
					for (int i = 0; i < muster[row].Length; i++)
					{
						Console.Write($"[{muster[row][i]}] ");
					}
					Console.ResetColor();
					Console.WriteLine();
					Console.Write("Position des markierten Moduls (1–5): ");

					string input = Console.ReadLine();
					if (!int.TryParse(input, out int pos) || pos < 1 || pos > 5)
					{
						TypeText("KI: „Eingabe ungültig. Wiederholen.“", 12);
						Thread.Sleep(600);
						continue;
					}

					int idx = pos - 1;
					if (muster[row][idx] == "X")
					{
						TypeText("Die Reihe stabilisiert sich, die Symbole leuchten in einem tiefen Blau.", 12);
						correct = true;
					}
					else
					{
						Console.Beep(300, 180);
						Console.Beep(250, 200);
						TypeText("KI: „Fehler im Muster. Korrektur erforderlich.“", 12);
						Thread.Sleep(700);
					}
				}
			}

			Console.WriteLine();
			TypeText("KI: „Genetische Muster erkannt. Sekundärtest bestanden.“", 12);
			Console.WriteLine("Drücke eine Taste, um fortzufahren...");
			Console.ReadKey();
		}
		//   LABYRINTH – BLUTSVÄTER-ANLAGE MIT ANDROIDEN
		static void RunBlutsvaeterLabyrinth()
		{
			Console.Clear();
			SaveGame("Scene_Blutsvaeter_Labyrinth");

			TypeText("Der Korridor dahinter ist breit, mit glatten, dunklen Wänden, durchzogen von leuchtenden Linien.", 12);
			TypeText("Die Blutsväter hatten nie für Menschen gebaut – alles wirkt zu sauber, zu perfekt, zu tot.", 12);
			TypeText("Wir müssen zum Reaktorkern, zur Kontrollsektion und zur Waffensteuerung, alles tief im Inneren.", 12);
			TypeText("");

			int anzugHuelle = 100;  // Lebens-/Schutzwert

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
			int cols = mapLines.Max(line => line.Length);

			char[,] map = new char[rows, cols];

			int playerR = 0, playerC = 0;

			for (int r = 0; r < rows; r++)
			{
				string line = mapLines[r].PadRight(cols, ' ');

				for (int c = 0; c < cols; c++)
				{
					char ch = line[c];
					map[r, c] = ch;

					if (ch == 'S')
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
				DrawBlutsvaeterMap(map, rows, cols, playerR, playerC);

				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Ziel: Reaktor (1), Kontrollkern (2), Waffensteuerung (3) aktivieren und dann zum Ausgang (E).");
				Console.ResetColor();
				Console.WriteLine($"Anzug-Integrität: {anzugHuelle}%");
				Console.WriteLine($"Konsole 1 (Reaktor):     {(console1Done ? "AKTIV" : "OFFLINE")}");
				Console.WriteLine($"Konsole 2 (Steuerung):   {(console2Done ? "AKTIV" : "OFFLINE")}");
				Console.WriteLine($"Konsole 3 (Waffe):       {(console3Done ? "AKTIV" : "OFFLINE")}");
				Console.WriteLine();
				Console.WriteLine("Steuerung: W/A/S/D = bewegen, H = Konsole hacken, Q = abbrechen (keine Option 😉).");
				Console.Write("Eingabe: ");

				string cmd = Console.ReadLine().Trim().ToUpper();

				if (cmd == "Q")
				{
					TypeText("Commander: Negativ. Wir sind hier unten, bis die Anlage läuft.", 12);
					Thread.Sleep(500);
					continue;
				}

				int newR = playerR;
				int newC = playerC;

				if (cmd == "W") newR--;
				else if (cmd == "S") newR++;
				else if (cmd == "A") newC--;
				else if (cmd == "D") newC++;

				// Konsole benutzen
				if (cmd == "H")
				{
					char tile = map[playerR, playerC];
					if (tile == '1' && !console1Done)
					{
						TypeText("Reaktorkonsole erkannt. Beginne Aktivierungsprozedur…", 12);
						RunBlutsvaeterConsoleHack(1, ref anzugHuelle);
						console1Done = true;
					}
					else if (tile == '2' && !console2Done)
					{
						TypeText("Zentrale Steuerkonsole. Zugriff wird aufgebaut…", 12);
						RunBlutsvaeterConsoleHack(2, ref anzugHuelle);
						console2Done = true;
					}
					else if (tile == '3' && !console3Done)
					{
						TypeText("Waffensteuerung. Letzter Sicherheitskreis wird geöffnet…", 12);
						RunBlutsvaeterConsoleHack(3, ref anzugHuelle);
						console3Done = true;
					}
					else
					{
						TypeText("Keine aktive Konsole an dieser Position.", 12);
					}

					if (anzugHuelle <= 0)
					{
						DrawAndroidWave();
						GameOver("Die Androiden drängen euch zurück und zerreißen die Anzüge. Der Trupp bricht in der Finsternis zusammen.");
						return;
					}

					continue;
				}

				// Bewegung
				if (cmd == "W" || cmd == "A" || cmd == "S" || cmd == "D")
				{
					if (newR < 0 || newR >= rows || newC < 0 || newC >= cols)
						continue;

					if (map[newR, newC] == '#')
					{
						TypeText("Solide Blutsväter-Struktur. Hier kommen wir nicht durch.", 12);
						Thread.Sleep(200);
						continue;
					}

					playerR = newR;
					playerC = newC;

					// Zufalls-Androidenbegegnung
					if (map[playerR, playerC] == ' ' || map[playerR, playerC] == '.')
					{
						if (new Random().Next(0, 100) < 6)  // 6% Chance
						{
							KleineAndroidenBegegnung(ref anzugHuelle);
							if (anzugHuelle <= 0)
							{
								DrawAndroidWave();
								GameOver("Die Androiden überrennen das Team. Der letzte Funke menschlicher Präsenz verlischt in der Anlage.");
								return;
							}
						}
					}

					// Ausgang?
					if (map[playerR, playerC] == 'E')
					{
						if (console1Done && console2Done && console3Done)
						{
							escapeTriggered = true;
						}
						else
						{
							TypeText("Oduro (über Funk): Captain, wir können nicht gehen, bevor alle Systeme aktiv sind!", 12);
							Thread.Sleep(500);
						}
					}
				}
			}
		}


		// Hack an den Konsolen – Du kannst ihn leicht ändern, wenn du willst
		static void RunBlutsvaeterConsoleHack(int level, ref int anzugHuelle)
		{
			// Wir benutzen hier einfach dein X-Finder-Pattern nochmal, aber mit Schaden bei Fehlern
			string[][] muster;

			if (level == 1)
			{
				muster = new string[][]
				{
			new [] { "A", "B", "C", "X", "D" }
				};
			}
			else if (level == 2)
			{
				muster = new string[][]
				{
			new [] { "D", "X", "B", "C", "A" }
				};
			}
			else
			{
				muster = new string[][]
				{
			new [] { "B", "C", "X", "D", "E" }
				};
			}

			foreach (var rowArr in muster)
			{
				bool done = false;

				while (!done)
				{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("=== BLUTSVÄTER-SCHALTKREIS ===\n");
					Console.ResetColor();

					Console.WriteLine("Reihe:");
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write("   ");
					for (int i = 0; i < rowArr.Length; i++)
						Console.Write($"[{rowArr[i]}] ");
					Console.ResetColor();
					Console.WriteLine();
					Console.Write("Fehlerhaftes Modul (Position 1–5): ");

					string input = Console.ReadLine();
					if (!int.TryParse(input, out int pos) || pos < 1 || pos > 5)
					{
						TypeText("Die Schaltung glimmt warnend. Die Anlage akzeptiert die Eingabe nicht.", 12);
						Thread.Sleep(600);
						continue;
					}

					int idx = pos - 1;
					if (rowArr[idx] == "X")
					{
						TypeText("Die Symbole stabilisieren sich, Energielinien pulsen durch den Schaltkreis.", 12);
						done = true;
					}
					else
					{
						Console.Beep(300, 200);
						Console.Beep(250, 200);
						TypeText("Ein kurzer Energieimpuls fährt durch den Anzug. Die Schilde flackern.", 12);
						anzugHuelle -= 10;
						TypeText($"Anzug-Integrität: {anzugHuelle}%.", 12);

						if (anzugHuelle <= 0)
							return;

						Thread.Sleep(700);
					}
				}
			}
		}


		// Androiden-Zwischenkampf
		static void KleineAndroidenBegegnung(ref int anzugHuelle)
		{
			Console.WriteLine();
			DrawAndroidWave();
			TypeText("Zwei humanoide Androiden stürmen aus einem Nebengang, ihre Augen leuchten kaltblau.", 12);
			Console.Beep(600, 120);
			Console.Beep(550, 120);
			TypeText("Wir reißen die Waffen hoch und feuern, Metall splittert, doch einige Treffer schlagen auf unsere Schilde durch.", 12);

			anzugHuelle -= 6;
			if (anzugHuelle < 0) anzugHuelle = 0;

			TypeText($"Anzug-Integrität sinkt auf {anzugHuelle}%.", 12);
			Thread.Sleep(400);
		}
		//   FINALE FLUCHT & NANITEN
		static void Scene_Blutsvaeter_Finale()
		{
			Console.Clear();
			SaveGame("Scene_Blutsvaeter_Finale");

			TypeText("Wir erreichen den inneren Schacht, der direkt zum Sendeturm der Blutsväter-Anlage führt.", 12);
			TypeText("Hinter uns verriegeln sich die Türen, metallische Schritte hallen in der Tiefe – Androiden, noch mehr.", 12);
			TypeText("Reaktor: ONLINE. Steuerung: ONLINE. Naniten-Purge-Waffenplattform: BEREIT.", 12);
			TypeText("");

			TypeText("Oduro (über Funk): Captain, im Orbit registrieren wir massive Energiespitzen. Die Anlage lädt den zweiten Puls.", 12);
			TypeText("Ren: Wenn Sie das auslösen, löscht es jede aktive Nanite-Struktur auf dem Planeten – inklusive aller infizierten Maschinen.", 12);
			TypeText("");

			Console.Beep(300, 200);
			Console.Beep(250, 180);
			DrawAndroidWave();
			TypeText("");
			TypeText("Hinter uns füllt der Gang sich mit laufenden, kletternden Androiden. Metallische Körper, durchzogen von flüssigen Naniten.", 12);
			TypeText("Sie rennen, springen, krallen sich an Wänden fest – wie eine Flut aus Stahl und Licht.", 12);
			TypeText("");

			TypeText("Commander: ARGOS, hier Bodentrupp. Forderung: Landungsschiff an Koordinate Sendeturm – Schwebeposition, sofort!", 12);
			TypeText("Ren: Anflug bestätigt. Bleiben Sie am Leben, Captain.", 12);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um den Aufstieg zum Turm zu beginnen...");
			Console.ReadKey();

			Console.Clear();
			DrawBlutsvaeterTower();
			TypeText("");
			TypeText("Wir sprinten über eine schmale Rampe, die sich wie eine Spirale durch den Sendeturm zieht.", 12);
			TypeText("Unter uns: der gleißende Kern der Anlage. Über uns: der Ausgang zur Oberfläche und das wartende Schiff.", 12);
			TypeText("");

			TypeText("Die erste Welle Androiden erreicht die Rampe. Wir haben nur Sekunden.", 12);
			Console.WriteLine();
			Console.WriteLine("1) Rückwärts feuern und langsam zurückweichen");
			Console.WriteLine("2) Granaten werfen und durchbrechen");
			Console.WriteLine("3) Schilde des Trupps nach vorne verstärken und halten");
			Console.Write("\nAuswahl: ");

			string choice = Console.ReadLine();

			Console.Clear();
			DrawAndroidWave();
			TypeText("");

			if (choice == "1")
			{
				TypeText("Wir drehen uns halb um, feuern Salve um Salve in die Woge aus Metall und Naniten.", 12);
				TypeText("Einige Androiden stürzen in den Abgrund, andere klettern kalt und unerbittlich über ihre Wracks hinweg.", 12);
			}
			else if (choice == "2")
			{
				TypeText("Zwei, drei Granaten rollen zwischen die Beine der vordersten Androiden.", 12);
				Console.Beep(700, 140);
				Console.Beep(900, 180);
				Console.Beep(400, 220);
				TypeText("Die Explosionen reißen eine Bresche in die Metallfront. Gliedmaßen und Komponenten fliegen durch den Schacht.", 12);
			}
			else if (choice == "3")
			{
				TypeText("Wir schieben die Schilde nach vorne, ein leuchtender Keil aus Energie.", 12);
				TypeText("Die ersten Androiden prallen dagegen, Funken sprühen, Nanitenstaub glitzert in der Luft – aber wir halten.", 12);
			}
			else
			{
				TypeText("Ein Moment des Zögerns – doch Instinkt und Training reißen uns herum, die Waffen sprechen.", 12);
			}

			Thread.Sleep(600);

			Console.Clear();
			DrawBlutsvaeterShuttlePickup();
			TypeText("");
			TypeText("Ein Schatten fällt über die Turmöffnung, das Landungsschiff der ARGOS schwebt im Staubsturm.", 12);
			TypeText("Pilot: Rein da! Wir haben nur ein Fenster von ein paar Sekunden!", 12);
			TypeText("Wir springen, Greifarme packen uns und ziehen uns ins Innere, während unter uns die Androiden den Rand erreichen.", 12);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um die Naniten-Reinigung zu sehen...");
			Console.ReadKey();

			Console.Clear();
			TypeText("");
			TypeText("Im Orbit: ARGOS und die übrigen Hilfsschiffe drehen sich aus der direkten Sichtlinie, ihre Sensoren auf die Erde gerichtet.", 12);
			TypeText("Ein weiterer, diesmal fokussierter Lichtstrahl schießt vom Sendeturm in den Himmel, der elektromagnetische Impuls.", 12);
			TypeText("Über die Oberfläche hinweg verlaufen Wellen aus unsichtbarer Energie, Maschinen verstummen, infizierte Systeme brechen zusammen.", 12);
			TypeText("");

			TypeText("Ren (über Kom): Captain… alle Nano-Signaturen brechen ein. Das planetare Netzwerk ist tot.", 12);
			TypeText("");

			SaveGame("Scene_Blutsvaeter_Ende");

			TypeText("Fortsetzung folgt…", 12);
			Console.ReadKey();
		}
		//   ASCII-HILFSMETHODEN FÜR DIE BLUTSVÄTER-MISSION
		static void DrawDropshipDescent()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("        _________");
			Console.WriteLine("    ___/  __   /_______");
			Console.WriteLine("   /   \\_/  \\_/       /");
			Console.WriteLine("  /  LANDUNGSSCHIFF  /");
			Console.WriteLine("  \\_________________/ ");
			Console.WriteLine("        |      |");
			Console.WriteLine("        |      |  <--- Eintritt in die Atmosphäre");
			Console.ResetColor();
		}

		static void DrawBlutsvaeterGateClosed()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("           ________________________________");
			Console.WriteLine("          /                                \\");
			Console.WriteLine("         /   MONOLITHISCHER BLUTSVÄTER-EINGANG \\");
			Console.WriteLine("        /____________________________________\\");
			Console.WriteLine("        |                                    |");
			Console.WriteLine("        |        ███████████████████         |");
			Console.WriteLine("        |        █  VERSIEGELTES TOR █       |");
			Console.WriteLine("        |        ███████████████████         |");
			Console.WriteLine("        |____________________________________|");
			Console.ResetColor();
		}

		static void DrawBlutsvaeterGateOpen()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("           ________________________________");
			Console.WriteLine("          /                                \\");
			Console.WriteLine("         /   MONOLITHISCHER BLUTSVÄTER-EINGANG \\");
			Console.WriteLine("        /____________________________________\\");
			Console.WriteLine("        |                                    |");
			Console.WriteLine("        |   ████          FREIER ZUGANG      |");
			Console.WriteLine("        |   ████     SCHACHT INS INNERE      |");
			Console.WriteLine("        |   ████                             |");
			Console.WriteLine("        |____________________________________|");
			Console.ResetColor();
		}

		static void DrawBlutsvaeterMap(char[,] map, int rows, int cols, int playerR, int playerC)
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("============= BLUTSVÄTER-INNENANLAGE – TAKTISCHE ANSICHT =============");
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

		static void DrawAndroidWave()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("   ANDROIDEN-WELLE");
			Console.WriteLine("   ===============");
			Console.WriteLine("      [=]    [=]    [=]");
			Console.WriteLine("     /||\\   /||\\   /||\\");
			Console.WriteLine("      /\\     /\\     /\\");
			Console.WriteLine("   metallische Schritte, kaltes Licht,");
			Console.WriteLine("   Naniten glimmen in den Gelenken…");
			Console.ResetColor();
		}

		static void DrawBlutsvaeterTower()
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

		static void DrawBlutsvaeterShuttlePickup()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("             ________");
			Console.WriteLine("         ___/  __   /____");
			Console.WriteLine("        /   \\_/  \\_/    /");
			Console.WriteLine("       /  ARGOS-SHUTTLE /");
			Console.WriteLine("       \\______________ /");
			Console.WriteLine("             |  |");
			Console.WriteLine("             |  |   <--- schwebt über dem Turm");
			Console.ResetColor();

			Scene_Return_From_Bloodsfather_Mission();
			return;
		}
		static void Scene_Return_From_Bloodsfather_Mission()
		{
			Console.Clear();
			SaveGame("Scene_Return_From_Bloodsfather_Mission");

			DrawShuttleInterior();
			TypeText("Das Landungsschiff erzittert leicht, als die ARGOS uns im Hangar aufnimmt.", 15);
			TypeText("Doch niemand von uns bewegt sich. Wir bleiben sitzen, erschöpft aber lebendig.", 15);
			TypeText("Die Mission war erfolgreich. Die Erde hat wieder eine Zukunft.", 15);
			TypeText("Schon bald kann man die Erde neu bewohnen. Die Städte müssten weitestgehend in Tackt sein.", 15);
			Console.WriteLine("\nDrücke eine Taste, um fortzufahren...");
			Console.ReadKey();

			Console.Clear();
			DrawShuttleOrbitView();
			TypeText("Ich starre durch die Sichtfenster des Landungsschiffs.", 15);
			TypeText("Europa, Jupiter. Die GENESIS, vollständig repariert. Sie strahlt, als wäre sie neu.", 15);
			TypeText("Ein Schiff, gebaut dachte ich als letzte Hoffnung. Aber laut Hale schon längst bestehende Technologie.", 15);
			Console.WriteLine("\nWeiter mit beliebiger Taste...");
			Console.ReadKey();

			Console.Clear();
			TypeText("Über Kom: Kurs auf Jupiter eingeleitet.'", 15);
			TypeText("Ich lehne mich zurück. Die Müdigkeit sitzt tief… aber wir haben überlebt.", 15);
			Console.WriteLine("\nWeiter...");
			Console.ReadKey();

			// ANKUNFT JUPITER
			Console.Clear();
			DrawArgosApproachJupiter();
			TypeText("Die ARGOS tritt in den Orbit des Gasriesen ein.", 15);
			TypeText("Ein goldenes Leuchten umgibt das Schiff, während die Triebwerke herunterfahren.", 15);
			TypeText("Mein Team steht langsam auf. Wir bewegen uns wie Menschen, die endlich Frieden spüren.", 15);
			Console.WriteLine("\nTaste drücken...");
			Console.ReadKey();

			// ZUM TELEPORTER
			Console.Clear();
			DrawTeleporterRoomArgos();
			TypeText("Techniker: Captain, der Teleporter ist vorbereitet. Zusätzliche Neutralisationssequenz aktiv.", 15);
			TypeText("Ich nicke nur. Worte fallen mir gerade schwer.", 15);
			TypeText("Wir stellen uns in Position. Energiefäden tanzen über unsere Rüstungen.", 15);
			Console.WriteLine("\nTeleport starten...");
			Console.ReadKey();

			// GENESIS – ANKUNFT
			Console.Clear();
			DrawTeleporterArrivalGenesis();
			TypeText("Das Licht fällt in sich zusammen und wir stehen im Teleporterraum der GENESIS.", 15);
			TypeText("Sauber. Hell. Warm. Ich atme endlich wieder frei.", 15);
			Console.WriteLine("\nTaste drücken...");
			Console.ReadKey();

			// ANZUG ABLEGEN
			Console.Clear();
			DrawArmorRoom();
			TypeText("Ich löse die Verriegelungen meines Kampfanzugs.", 15);
			TypeText("Die Serviceroboter nehmen jede Komponente vorsichtig entgegen.", 15);
			TypeText("Zum ersten Mal seit Stunden fühle ich mich wieder wie ein Mensch.", 15);
			Console.WriteLine("\nWeiter...");
			Console.ReadKey();

			// TURBOLIFT
			Console.Clear();
			DrawTurboLift();
			TypeText("Ich betrete den Turbolift. Die Türen schließen sich sanft.", 15);
			TypeText("'Deck für Deck, bis zu den: Wohnquartiere, meldet die Schiffsstimme.", 15);
			TypeText("Für einen Moment bin ich allein mit meinen Gedanken.", 15);
			Console.WriteLine("\nWeiter...");
			Console.ReadKey();

			// QUARTIER – WIEDERSEHEN
			Console.Clear();
			DrawCrewQuarters();
			TypeText("Vor meinem Quartier wartet sie bereits, meine Frau.", 15);
			TypeText("Die Wissenschaftlerin, die mir mehr bedeutet als jedes Schiff, jede Mission.", 15);
			TypeText("Sie lächelt müde. Ich ebenfalls.", 15);

			TypeText("Wir treten aufeinander zu. Keine Worte. Nur eine lange Umarmung.", 12);
			TypeText("Eine Umarmung die sich nach Zuhause anfülltöl. Nach Leben. Nach Zukunft.", 12);

			TypeText("Ich erzähle ihr von der Mission. Von den Androiden. Vom Labyrinth.", 15);
			TypeText("Sie hört zu, ohne mich zu unterbrechen, wärmt meine Hände mit ihren.", 15);

			TypeText("Als ich geendet habe, sagt sie nur leise:", 15);
			TypeText("'Du bist zurück. Das ist alles, was zählt.'", 12);

			TypeText("Ich nicke, lasse mich von ihr in das Quartier führen… und endlich finde ich Ruhe.", 15);

			SaveGame("Scene_Return_From_Bloodsfather_Mission_End");

			Console.WriteLine("\nFORTSETZUNG FOLGT...");
			Console.ReadKey();
		}
		//  ASCII–GRAFIKEN FÜR DIE SZENE 
		static void DrawShuttleInterior()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("   ___________________________________________");
			Console.WriteLine("  |                                           |");
			Console.WriteLine("  |      [ LANDUNGSSCHIFF – INNENANSICHT ]    |");
			Console.WriteLine("  |   Sitzreihen | Flackernde Anzeigen | Crew |");
			Console.WriteLine("  |___________________________________________|");
			Console.ResetColor();
		}

		static void DrawShuttleOrbitView()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("              .      .      .");
			Console.WriteLine("        .           JUPITER            .");
			Console.WriteLine("       .     o                        .");
			Console.WriteLine("          .       EUROPA           .");
			Console.WriteLine("   [SCHIFF]---- schwebt im Orbit ----[ARGOS]");
			Console.ResetColor();
		}

		static void DrawArgosApproachJupiter()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("                    ___");
			Console.WriteLine("           ________/   \\_______");
			Console.WriteLine("         _/                   \\_");
			Console.WriteLine("       _/       ARGOS           \\_");
			Console.WriteLine("       \\__        → Jupiter     __/");
			Console.WriteLine("          \\____________________/");
			Console.ResetColor();
		}

		static void DrawTeleporterRoomArgos()
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("     _________________________________");
			Console.WriteLine("    |   ARGOS – TELEPORTERRAUM       |");
			Console.WriteLine("    |  [ O O O ] Energiefelder       |");
			Console.WriteLine("    |________________________________|");
			Console.ResetColor();
		}

		static void DrawTeleporterArrivalGenesis()
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("     _________________________________");
			Console.WriteLine("    |   GENESIS – TELEPORTERRAUM     |");
			Console.WriteLine("    |  <Materialisationsnebel löst sich>|");
			Console.WriteLine("    |________________________________|");
			Console.ResetColor();
		}

		static void DrawArmorRoom()
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("   [KAMPFANZUG-KAMMER]");
			Console.WriteLine("   Serviceroboter ▶ ▶ ▶ nehmen Teile entgegen");
			Console.ResetColor();
		}

		static void DrawTurboLift()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("   ┌──────────────────────┐");
			Console.WriteLine("   │     TURBOLIFT        │");
			Console.WriteLine("   │  |  |  |  |  |  |     │");
			Console.WriteLine("   │  Fahrstuhl steigt…   │");
			Console.WriteLine("   └──────────────────────┘");
			Console.ResetColor();
		}

		static void DrawCrewQuarters()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("   _____________________________");
			Console.WriteLine("  |       WOHNQUARTIER         |");
			Console.WriteLine("  |   Eine warme, sanfte Tür   |");
			Console.WriteLine("  |_____________________________|");
			Console.ResetColor();

			Scene_AllianzAlarmUndFlottenstart();
			return;
		}


		static void Scene_AllianzAlarmUndFlottenstart()
		{
			Console.Clear();
			SaveGame("Scene_AllianzAlarmUndFlottenstart");

			// --- Roter Alarm mit Sound ---
			PlayAllianceRedAlert();

			Console.ForegroundColor = ConsoleColor.White;
			TypeText("Ich wurde vom Roten Alarm aus dem Schlaf gerissen.", 12);
			TypeText("Das grelle Licht flutete meine riesige Kabine, alles war in tiefes Rot getaucht.", 12);
			TypeText("Der Alarm schrillte durch jede Wand, durch jede Faser meines Körpers.", 12);
			TypeText("");

			TypeText("Über Kom ertönte Oduros Stimme: „Captain, sofort auf die Brücke!“", 12);
			TypeText("Noch halb im Schlaf stolperte ich aus dem Bett, griff nach meiner Uniform.", 12);
			TypeText("Die Bewegungen wurden routiniert, Unterhemd, Hose, Jacke, Rangabzeichen.", 12);
			TypeText("Ein Spritzer kaltes Wasser ins Gesicht brachte die letzten Reste Müdigkeit zum Schweigen.", 12);
			TypeText("Keine Zeit mich von meiner Frau zu verabschieden.", 12);
			TypeText("");

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um zum Turbolift zu rennen...");
			Console.ReadKey();

			// --- Turbolift zur Brücke ---
			Console.Clear();
			DrawTurboliftShortRoute();
			TypeText("");
			TypeText("Ich hetzte in den Korridor, noch während die Türen sich öffneten, und sprang in den Turbolift.", 12);
			TypeText("„Brücke – Deck 1“, knurrte ich. Der Lift setzte sich sofort in Bewegung.", 12);
			TypeText("Stockwerk um Stockwerk flog an mir vorbei, das Summen der Magnetfelder wie ein fernes Donnern.", 12);

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um auf der Brücke anzukommen...");
			Console.ReadKey();

			// --- Brücken-Szene, Hale erklärt die Lage ---
			Console.Clear();
			DrawBridgeViewRedAlert();
			TypeText("");
			TypeText("Die Brücke war im Notlicht getaucht. Rote Streifen zogen sich über die Anzeigen und Konsolen.", 12);
			TypeText("Offiziere arbeiteten schnell, konzentriert, niemand verschwendete eine Bewegung.", 12);
			TypeText("Auf dem Hauptschirm erschien bereits Hale, sein Gesicht hart und angespannt.", 12);
			TypeText("");

			TypeText("Ich: Was ist los?", 12);
			TypeText("Hale: Mehrere Energiesignaturen im Hyperraum. Subraum-Echoes, sehr stark.", 12);
			TypeText("Hale: Es sind Dutzende Schiffe. Eine Allianz von Nachfahren der Blutsväter, mehrere Völker dieser Galaxie, die eine Allianz miteinander haben", 12);
			TypeText("");

			TypeText("Hale: Sie wissen vom Blutsväter-Virus. Von eurem Einsatz. Von den Nanobots, die es reaktiviert haben.", 12);
			TypeText("Hale: Sie haben lange zugesehen, geschützt vom Schatten das wir die direkten Nachfahren sind.", 12);
			TypeText("Hale: Aber jetzt reicht es ihnen. Sie sehen uns als Risiko für die gesamte Galaxie.", 12);
			TypeText("");

			TypeText("Mir wurde kalt. Der Gedanke, dass die Menschheit als Gefahr eingestuft wurde, fraß sich tief in meinen Magen.", 12);
			TypeText("");

			TypeText("Hale: Alle Schiffe auf Gefechtsstation. Wir nehmen Kurs auf die Erde. Wir werden sie schützen, solange wir atmen.", 12);
			TypeText("Hale: Sie bekommen das Kommando über den ersten Kampfverband. Sie haben sich schon mal bwiesen.", 12);
			TypeText("Hale: Ich übernehme den zweiten. Wir müssen sie abwehren, bevor sie die Erde erreichen.", 12);
			TypeText("Hale: Unsere letzte Hoffnung sind die Grauen. Wir senden umgehend ein Hilfsersuchen.", 12);
			TypeText("");

			TypeText("Ich schluckte schwer. Aus dem Schlaf in eine drohende Galaxien-Schlacht, mein Kopf pulsierte.", 12);
			TypeText("Trotzdem nickte ich nur: Verstanden, Sir. Ich übernehme den Verband.", 12);
			TypeText("");

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um den Kampfverband zu formieren...");
			Console.ReadKey();

			// --- Flottenverband formiert sich ---
			Console.Clear();
			DrawFleetFormUp();
			TypeText("");
			TypeText("Nach wenigen Minuten lagen die Befehlsdaten vor mir, Schiffscodes, Staffeln, Formationen.", 12);
			TypeText("Mein Kampfverband war gewaltig: Neben der ARGOS noch vier weitere Trägerschiffe der Universum-Klasse.", 12);
			TypeText("Dutzende Kreuzer, Fregatten, Eskortschiffe, dazu Jägergeschwader, die ich noch nie zuvor gesehen hatte.", 12);
			TypeText("");

			TypeText("Ich: Kommunikation – gesamte Flotte: Kurs Erde. Synchronisieren, Unterlichttriebwerke auf Maximum.", 12);
			TypeText("Oduro: Aye, Captain. Kursdaten werden verteilt. Verband geht in Formation.", 12);
			TypeText("");

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um den Kurs zur Erde zu setzen...");
			Console.ReadKey();

			// --- Kurs Erde & Problem mit der ARGOS ---
			Console.Clear();
			DrawFleetEnRouteToEarth();
			TypeText("");
			TypeText("Die GENESIS führte den Verband an. Die Anzeigen flimmerten, als die Triebwerke hochfuhren.", 12);
			TypeText("Doch nach einigen Minuten meldete sich der Sensoroffizier, die Stirn in Falten gelegt.", 12);
			TypeText("");

			TypeText("Sensoroffizier: Captain... die ARGOS hält den Kurs nicht.", 12);
			TypeText("Ich: Wie meinen Sie das?", 12);
			TypeText("Sensoroffizier: „Sie nimmt nicht Kurs auf die Erde, sondern auf Koordinaten außerhalb des Systems.“", 12);
			TypeText("");

			TypeText("Kommunikation: Wir versuchen, sie zu rufen, keine Antwort. Kanal ist offen, aber...", 12);
			TypeText("Ein Rauschen lag auf der Leitung. Verzerrte, unverständliche Geräusche, als würde jemand sprechen, der längst keiner mehr war.", 12);
			TypeText("");

			TypeText("Ich presste die Lippen zusammen: Stellen Sie Hale durch.", 12);
			TypeText("");

			TypeText("Hale (über Kom): Was ist mit der ARGOS?", 12);
			TypeText("Ich: Sie verlässt das Sonnensystem. Kein Kontakt, nur Störgeräusche.", 12);
			TypeText("");

			TypeText("Hale schwieg einen Moment, dann senkte er den Blick.", 12);
			TypeText("Hale: Wurden Ihre Anzüge und das Landungsschiff nach der Rückkehr dekontaminiert?", 12);
			TypeText("Ich: So gründlich, wie es möglich war. Schilde, Dekontamination, alles nach Vorschrift. Aber bei dem Landungsschiff...", 12);
			TypeText("");

			TypeText("Hale: Dann rechne ich mit dem Schlimmsten.", 12);
			TypeText("Hale: Die Nanobots könnten trotzdem an der Hülle oder in Spalten überlebt haben.", 12);
			TypeText("Hale: Wenn sie die Bordsysteme der ARGOS infiltriert haben, dann kontrollieren sie jetzt die KI und auch die Crew.", 12);
			TypeText("Die Bots können Zellen umprogrammieren, das diese das die fremde Struktur annimt und Viren herstellt.");
			TypeText("");


			TypeText("Mein Magen krampfte. Ein Schiff dieser Klasse, verseucht und unberechenbar, irgendwo in der Galaxie...", 12);
			TypeText("");

			TypeText("Ich: Sollen wir Abfangkurs setzen?", 12);
			TypeText("Hale: Negativ. Wir brauchen jedes Schiff, um die Allianz abzufangen.", 12);
			TypeText("Hale: Wenn die Grauen unsere Anfrage positiv bestätigen, werden sie sich um die ARGOS kümmern.", 12);
			TypeText("Hale: Unsere Aufgabe ist jetzt die Verteidigung der Erde.", 12);
			TypeText("");

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um den Sprung der ARGOS zu sehen...");
			Console.ReadKey();

			// --- ARGOS verlässt das System ---
			Console.Clear();
			DrawArgosJumpAway();
			TypeText("");
			TypeText("Auf einem Nebendisplay wurde die ARGOS vergrößert eingeblendet.", 12);
			TypeText("Energiepeaks stiegen steil an, die Sprungtriebwerke wurden aktiviert.", 12);
			TypeText("Ein gleißender Riss öffnete sich im Raum, verzerrte Sterne, verzogene Konturen.", 12);
			TypeText("Dann war die ARGOS verschwunden und mit ihr jede Möglichkeit, sie im Moment zu verfolgen.", 12);
			TypeText("");

			TypeText("Sensoroffizier: Kontakt verloren. Keine Subraumspur mehr im gültigen Bereich, Captain.", 12);
			TypeText("Hale: Wir können sie orten, keine Sorge. Später können wir sie nachverfolgen. Wenn wir den heutigen Tag überleben.", 12);
			TypeText("");

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um die Allianz-Flotte erscheinen zu lassen...");
			Console.ReadKey();

			// --- Allianz-Flotte erscheint in der Nähe der Erde 
			Console.Clear();
			TypeText("");
			TypeText("Vor uns lag die Erde – vernarbt, aber lebendig.", 12);
			TypeText("Darüber spannten sich bereits die ersten Schiffe des zweiten Kampfverbandes.", 12);
			TypeText("");

			TypeText("Sensoroffizier: Hyperraum-Signaturen im Anflug! Mehrfach. Sie kommen aus allen Winkeln des Raums.", 12);
			TypeText("Oduro: Captain, die Allianzflotte tritt ein!", 12);
			TypeText("");

			TypeText("Ich atmete tief durch. Jetzt gab es kein Zurück mehr.", 12);
			TypeText("Ich: Alle Verbände auf Gefechtsstation. Jäger starten, Schutzformation um die Erde aufbauen.", 12);
			TypeText("Ich: Das hier wird unser schwerster Kampf.", 12);
			TypeText("");

			TypeText("In meinem Hinterkopf blieb nur ein Gedanke: Wenn wir das überstehen, müssen wir die ARGOS finden egal, wohin sie geflohen ist.", 12);
			TypeText("");

			SaveGame("Scene_AllianzAlarm_Ende");

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um die Flottenschlacht zu beginnen...");
			Console.ReadKey();

			Scene_AllianzFlottenschlacht();
			return;


		}
		//  HILFSMETHODEN FÜR DIE SZENE
		static void PlayAllianceRedAlert()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;

			for (int i = 0; i < 3; i++)
			{
				Console.WriteLine("=== R O T E R   A L A R M ===");
				Console.Beep(900, 250);
				Thread.Sleep(150);
				Console.Beep(700, 250);
				Thread.Sleep(150);
				Console.Clear();
			}

			Console.ResetColor();
		}

		static void DrawTurboliftShortRoute()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("========== TURBOLIFT ==========");
			Console.WriteLine("  [ KAPITÄNSDECK ]");
			Console.WriteLine("        ||");
			Console.WriteLine("        ||");
			Console.WriteLine("        \\\\");
			Console.WriteLine("         \\\\");
			Console.WriteLine("          \\\\");
			Console.WriteLine("           \\\\");
			Console.WriteLine("            ||");
			Console.WriteLine("            ||");
			Console.WriteLine("        [ BRÜCKE / DECK 1 ]");
			Console.WriteLine("================================");
			Console.ResetColor();
		}

		static void DrawBridgeViewRedAlert()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("=============== BRÜCKE – ROTER ALARM ===============");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("   Konsolen blinken, rote Warnanzeigen pulsieren...");
			Console.WriteLine();
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                [ HAUPTDISPLAY ]");
			Console.WriteLine("  +------------------------------------------------+");
			Console.WriteLine("  |                                                |");
			Console.WriteLine("  |                    JUPITER                     |");
			Console.WriteLine("  |                                                |");
			Console.WriteLine("  |                                                |");
			Console.WriteLine("  +------------------------------------------------+");
			Console.ResetColor();
		}

		static void DrawFleetFormUp()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("============= FLOTTE FORMATIERT SICH =============");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("        [ GENESIS ]         [ ARGOS ]      [ TRÄGER-03 ]");
			Console.WriteLine("             |                   |              |");
			Console.WriteLine("      <>=====|====<>      <>=====|====<>  <>====|====<>");
			Console.WriteLine("        /  \\                 /  \\            /  \\");
			Console.WriteLine("   [Kreuzer] [Fregatten] [Eskorten]   [Jägerstaffeln]");
			Console.ResetColor();
		}

		static void DrawFleetEnRouteToEarth()
		{
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("                 R A U M – K U R S  E R D E");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("GENESIS  >>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("ARGOS    >>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("TRÄGER   >>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("KREUZER  >>>>>>>>>>>>>>>>>>>>>>>");
			Console.WriteLine("FREGATTEN >>>>>>>>>>>>>>>>>>>>>>");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("\n   .   .   .   .   .   .   .   .   .   .   (Sterne ziehen vorbei)");
			Console.ResetColor();
		}

		static void DrawArgosJumpAway()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("                    ARGOS – SPRUNGSEQUENZ");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("                 ________________________");
			Console.WriteLine("                /                        \\");
			Console.WriteLine("      ARGOS   <    ==============        >");
			Console.WriteLine("                \\________________________/");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine();
			Console.WriteLine("           <<< Raum krümmt sich, Licht verzerrt sich >>>");
			Console.WriteLine("                     < < < S P R U N G > > >");
			Console.ResetColor();
		}
		class FleetShipAllianz
		{
			public string Name;
			public string Typ;
			public bool IstSpieler;
			public bool IstFlaggschiff; // GENESIS darf NICHT zerstört werden
			public int MaxSchild;
			public int Schild;
			public int MaxHuelle;
			public int Huelle;
			public int TorpedoSchaden;
			public int StrahlenSchaden;

			public bool Zerstoert => Huelle <= 0;

			public FleetShipAllianz(string name, string typ, bool istSpieler, bool istFlaggschiff,
				int maxSchild, int maxHuelle, int torpedo, int strahl)
			{
				Name = name;
				Typ = typ;
				IstSpieler = istSpieler;
				IstFlaggschiff = istFlaggschiff;
				MaxSchild = maxSchild;
				Schild = maxSchild;
				MaxHuelle = maxHuelle;
				Huelle = maxHuelle;
				TorpedoSchaden = torpedo;
				StrahlenSchaden = strahl;
			}

			public void SchadenErleiden(int damage)
			{
				if (damage <= 0) return;

				int rest = damage;

				if (Schild > 0)
				{
					int schildTreffer = Math.Min(Schild, rest);
					Schild -= schildTreffer;
					rest -= schildTreffer;
				}

				if (rest > 0)
				{
					Huelle -= rest;
					if (Huelle < 0) Huelle = 0;
				}
			}
		}

		static void Scene_AllianzFlottenschlacht()
		{
			Console.Clear();
			SaveGame("Scene_AllianzFlottenschlacht");

			// Kurze Einleitung
			TypeText("Alarmstufe ROT. Die Allianz der Nachfahren der Blutsväter springt in unser System.", 10);
			TypeText("Dutzende Energiesignaturen tauchen im Subraum auf – Kriegsschiffe, Trägerschiffe, Eskorten.", 10);
			TypeText("Hale: Wir teilen die Flotte auf. Sie übernehmen Verband EINS mit der GENESIS.", 10);
			TypeText("Hale: Ich führe Verband ZWEI. Unsere einzige Chance ist, die erste Angriffswelle zu brechen.", 10);
			TypeText("");
			TypeText("Ich atme tief durch. Wenn die GENESIS fällt, war alles vergebens.", 10);
			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um die Allianz-Flottenschlacht zu beginnen...");
			Console.ReadKey(true);

			RunAllianzFlottenschlacht();
		}
		static void RunAllianzFlottenschlacht()
		{
			Console.Clear();
			Random rng = new Random();

			// Spielerflotte (5 Schiffe)
			List<FleetShipAllianz> spieler = new List<FleetShipAllianz>
	{
        // GENESIS – Universum-Klasse, Flaggschiff
        new FleetShipAllianz("GENESIS", "UNIVERSUM-KLASSE", true, true, 200, 200, 55, 45),

        // Weitere Universum-/Kreuzer-/Fregatten-Kombination
        new FleetShipAllianz("AURORA",  "UNIVERSUM-KLASSE", true, false, 200, 200, 45, 40),
		new FleetShipAllianz("VALKYRIE","KREUZER",          true, false, 150, 150, 35, 30),
		new FleetShipAllianz("HORIZON", "KREUZER",          true, false, 140, 140, 30, 28),
		new FleetShipAllianz("LANCER",  "FREGATTE",         true, false, 110, 110, 22, 18)
	};

			// Allianzflotte (5 Schiffe)
			List<FleetShipAllianz> allianz = new List<FleetShipAllianz>
	{
		new FleetShipAllianz("A-DREAD-1",   "DREADNOUGHT",      false, false, 350, 250, 45, 40),
		new FleetShipAllianz("A-DREAD-2",   "DREADNOUGHT",      false, false, 350, 250, 45, 40),
		new FleetShipAllianz("A-CRUISER-1", "SCHWERER KREUZER", false, false, 200, 180, 34, 30),
		new FleetShipAllianz("A-CRUISER-2", "KREUZER",          false, false, 160, 160, 30, 26),
		new FleetShipAllianz("A-FRIG-1",    "FREGATTE",         false, false, 150, 130, 22, 20)
	};

			FleetShipAllianz genesis = spieler.First(s => s.IstFlaggschiff);
			FleetShipAllianz genesisSchutzSchiff = null; // Schiff, das für einen Zug die GENESIS deckt

			string kampfInfo = "Die Flotten stoßen frontal aufeinander.";
			bool kampfLaeuft = true;

			while (kampfLaeuft)
			{
				Console.Clear();
				DrawAllianceBattlefield(spieler, allianz, kampfInfo);

				// Verlustbedingungen
				if (!spieler.Any(s => !s.Zerstoert))
				{
					GameOver("Alle Schiffe Ihres Verbandes wurden zerstört. Die Allianz durchbricht die Linie.");
					return;
				}

				if (genesis.Zerstoert)
				{
					GameOver("Die GENESIS explodiert in einem Meer aus Licht. Ohne Flaggschiff bricht die Verteidigung zusammen.");
					return;
				}

				// Siegbedingung
				if (!allianz.Any(s => !s.Zerstoert))
				{
					Console.Clear();
					DrawAllianceBattlefield(spieler, allianz, "Die letzten Allianz-Schiffe verglühen im All...");

					TypeText("Hale: Verband ZWEI meldet: Feindliche Angriffswelle gebrochen!", 10);
					TypeText("Oduro: Captain, wir haben es geschafft – die Allianz zieht sich zurück!", 10);
					TypeText("Die Brücke der GENESIS bebt noch immer nach dem feindlichen Beschuss. Doch die Schilde halten.", 10);

					Console.WriteLine();
					Console.WriteLine("Drücke eine Taste, um fortzufahren...");
					Console.ReadKey(true);

					// SPEICHERPUNKT direkt nach der Schlacht
					SaveGame("Scene_AllianzKapitel1_Abschluss_Start");

					// Abschlusssequenz starten
					RunAllianzSiegCinematic(spieler,allianz,genesis);

					return;
				}

				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine();
				Console.WriteLine("=== IHR ZUG ===");
				Console.ResetColor();
				Console.WriteLine("1) Torpedoangriff");
				Console.WriteLine("2) Strahlenangriff");
				Console.WriteLine("3) Fokus-Strahlenfeuer (alle Schiffe auf ein Ziel)");
				Console.WriteLine("4) Schutzmanöver: Ein Schiff deckt die GENESIS für diese Runde");
				Console.Write("Auswahl: ");
				string ausw = Console.ReadLine().Trim();

				if (ausw == "1" || ausw == "2")
				{
					// Angreifer wählen
					var aktiveSpieler = spieler.Where(s => !s.Zerstoert).ToList();
					Console.WriteLine();
					Console.WriteLine("Eigenes Schiff wählen:");
					for (int i = 0; i < aktiveSpieler.Count; i++)
					{
						Console.WriteLine($"{i + 1}) {aktiveSpieler[i].Name} ({aktiveSpieler[i].Typ})");
					}
					Console.Write("Nummer: ");
					if (!int.TryParse(Console.ReadLine(), out int idxAngreifer) ||
						idxAngreifer < 1 || idxAngreifer > aktiveSpieler.Count)
					{
						kampfInfo = "Ungültige Eingabe – Ihre Schiffe zögern.";
					}
					else
					{
						var angreifer = aktiveSpieler[idxAngreifer - 1];

						// Ziel wählen
						var aktiveAllianz = allianz.Where(s => !s.Zerstoert).ToList();
						Console.WriteLine();
						Console.WriteLine("Zielschiff der Allianz wählen:");
						for (int i = 0; i < aktiveAllianz.Count; i++)
						{
							Console.WriteLine($"{i + 1}) {aktiveAllianz[i].Name} ({aktiveAllianz[i].Typ})");
						}
						Console.Write("Nummer: ");
						if (!int.TryParse(Console.ReadLine(), out int idxZiel) ||
							idxZiel < 1 || idxZiel > aktiveAllianz.Count)
						{
							kampfInfo = "Ihr Zielsystem braucht zu lange – keine koordinierte Salve.";
						}
						else
						{
							var ziel = aktiveAllianz[idxZiel - 1];

							if (ausw == "1")
							{
								int dmg = angreifer.TorpedoSchaden + rng.Next(-5, 6);
								if (dmg < 5) dmg = 5;
								AnimateTorpedoStrike(true);
								ziel.SchadenErleiden(dmg);
								kampfInfo = $"{angreifer.Name} feuert Torpedosalven auf {ziel.Name} (Schaden: {dmg}).";
								if (ziel.Zerstoert)
								{
									AnimateExplosion(false);
									kampfInfo += $" {ziel.Name} explodiert in einem grellen Feuerball!";
								}
							}
							else
							{
								int dmg = angreifer.StrahlenSchaden + rng.Next(-4, 5);
								if (dmg < 4) dmg = 4;
								AnimateBeamStrike(true);
								ziel.SchadenErleiden(dmg);
								kampfInfo = $"{angreifer.Name} entfesselt einen Partikelstrahl auf {ziel.Name} (Schaden: {dmg}).";
								if (ziel.Zerstoert)
								{
									AnimateExplosion(false);
									kampfInfo += $" {ziel.Name} wird von innen heraus aufgerissen!";
								}
							}
						}
					}
				}
				else if (ausw == "3")
				{
					// Fokus-Strahlenfeuer (doch unbegrenzt nutzbar, habe dafür stärkere Allianzschilde)
					var aktiveAllianz = allianz.Where(s => !s.Zerstoert).ToList();
					Console.WriteLine();
					Console.WriteLine("Fokus-Ziel der Allianz wählen:");
					for (int i = 0; i < aktiveAllianz.Count; i++)
					{
						Console.WriteLine($"{i + 1}) {aktiveAllianz[i].Name} ({aktiveAllianz[i].Typ})");
					}
					Console.Write("Nummer: ");
					if (!int.TryParse(Console.ReadLine(), out int idxZiel) ||
						idxZiel < 1 || idxZiel > aktiveAllianz.Count)
					{
						kampfInfo = "Die Flotte kann sich nicht auf ein gemeinsames Ziel einigen.";
					}
					else
					{
						var ziel = aktiveAllianz[idxZiel - 1];
						int gesamtSchaden = 0;

						AnimateFocusBeam();
						foreach (var s in spieler.Where(s => !s.Zerstoert))
						{
							int dmg = s.StrahlenSchaden + rng.Next(-3, 4);
							if (dmg < 3) dmg = 3;
							gesamtSchaden += dmg;
						}
						ziel.SchadenErleiden(gesamtSchaden);
						kampfInfo = $"Alle Schiffe bündeln ihre Strahlen auf {ziel.Name}! Gesamtschaden: {gesamtSchaden}.";
						if (ziel.Zerstoert)
						{
							AnimateExplosion(false);
							kampfInfo += $" {ziel.Name} wird praktisch verdampft.";
						}
					}
				}
				else if (ausw == "4")
				{
					// Schutzmanöver
					var schutzKandidaten = spieler.Where(s => !s.Zerstoert && !s.IstFlaggschiff).ToList();
					if (!schutzKandidaten.Any())
					{
						kampfInfo = "Kein Schiff ist in der Lage, die GENESIS zu decken.";
					}
					else
					{
						Console.WriteLine();
						Console.WriteLine("Welches Schiff soll die GENESIS decken?");
						for (int i = 0; i < schutzKandidaten.Count; i++)
						{
							Console.WriteLine($"{i + 1}) {schutzKandidaten[i].Name} ({schutzKandidaten[i].Typ})");
						}
						Console.Write("Nummer: ");
						if (!int.TryParse(Console.ReadLine(), out int idxSchutz) ||
							idxSchutz < 1 || idxSchutz > schutzKandidaten.Count)
						{
							kampfInfo = "Das Schutzmanöver kommt nicht zustande.";
						}
						else
						{
							genesisSchutzSchiff = schutzKandidaten[idxSchutz - 1];
							kampfInfo = $"{genesisSchutzSchiff.Name} wechselt Position und deckt die GENESIS.";
						}
					}
				}
				else
				{
					kampfInfo = "Ihre Befehle sind unklar – die Flotte verharrt.";
				}

				// Allianz schon weg?
				if (!allianz.Any(s => !s.Zerstoert))
					continue;

				// Gegner-Zug
				
				Thread.Sleep(400);
				Console.Clear();
				DrawAllianceBattlefield(spieler, allianz, "Die Allianz bereitet eine Gegenoffensive vor...");
				Thread.Sleep(500);

				var aktiveAllianz2 = allianz.Where(s => !s.Zerstoert).ToList();
				var aktiveSpieler2 = spieler.Where(s => !s.Zerstoert).ToList();
				if (!aktiveAllianz2.Any() || !aktiveSpieler2.Any()) continue;

				// Gegner wählt Angreifer & Ziel
				var gegnerAngreifer = aktiveAllianz2[rng.Next(aktiveAllianz2.Count)];

				// Ziel: bevorzugt GENESIS, sonst zufällig
				FleetShipAllianz gegnerZiel;
				if (!genesis.Zerstoert && rng.NextDouble() < 0.6)
				{
					gegnerZiel = genesis;
				}
				else
				{
					gegnerZiel = aktiveSpieler2[rng.Next(aktiveSpieler2.Count)];
				}

				// Schutzmanöver aktiv? Dann ersten Treffer auf das Schutz-Schiff umlenken
				if (genesisSchutzSchiff != null && gegnerZiel == genesis && !genesisSchutzSchiff.Zerstoert)
				{
					gegnerZiel = genesisSchutzSchiff;
				}

				bool gegnerNutzenTorpedo = rng.NextDouble() < 0.5;
				if (gegnerNutzenTorpedo)
				{
					int dmg = gegnerAngreifer.TorpedoSchaden + rng.Next(-5, 6);
					if (dmg < 5) dmg = 5;
					AnimateTorpedoStrike(false);
					gegnerZiel.SchadenErleiden(dmg);
					kampfInfo = $"Allianz-Schiff {gegnerAngreifer.Name} feuert Torpedos auf {gegnerZiel.Name} (Schaden: {dmg}).";
				}
				else
				{
					int dmg = gegnerAngreifer.StrahlenSchaden + rng.Next(-4, 5);
					if (dmg < 4) dmg = 4;
					AnimateBeamStrike(false);
					gegnerZiel.SchadenErleiden(dmg);
					kampfInfo = $"Allianz-Schiff {gegnerAngreifer.Name} trifft {gegnerZiel.Name} mit einem Strahl (Schaden: {dmg}).";
				}

				if (gegnerZiel.Zerstoert)
				{
					AnimateExplosion(gegnerZiel.IstSpieler); // Spieler-Schiff links, Allianz rechts
					kampfInfo += $" {gegnerZiel.Name} zerbricht in einem Meer aus Trümmern.";
				}

				// Schutzmanöver verbraucht sich nach einer Runde
				genesisSchutzSchiff = null;
			}
		}

		static void DrawAllianceBattlefield(List<FleetShipAllianz> spieler, List<FleetShipAllianz> allianz, string infoZeile)
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("=============== ALLIANZ-FLOTTENSCHLACHT – TAKTISCHE ANSICHT ===============");
			Console.ResetColor();

			// --- STATUS ANZEIGE ---
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Eigene Flotte:");
			Console.ResetColor();
			foreach (var s in spieler)
				DrawShipStatusLine(s);

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Allianz-Flotte:");
			Console.ResetColor();
			foreach (var e in allianz)
				DrawShipStatusLine(e);

			Console.WriteLine();
			Console.WriteLine("---------------------------------------------------------------------------");
			Console.WriteLine(infoZeile);
			Console.WriteLine("---------------------------------------------------------------------------");
			Console.WriteLine();

			// --- ASCII SCHLACHTANSICHT ---
			// Wir bauen für jede Seite einen großen "Bildschirm" aus allen Schiffen untereinander
			List<string> leftLines = new List<string>();
			foreach (var s in spieler)
			{
				string[] ascii = GetShipAscii(s);
				leftLines.AddRange(ascii);
				leftLines.Add(""); // Leerzeile zwischen Schiffen
			}

			List<string> rightLines = new List<string>();
			foreach (var s in allianz)
			{
				string[] ascii = GetShipAscii(s);
				rightLines.AddRange(ascii);
				rightLines.Add("");
			}

			int totalRows = Math.Max(leftLines.Count, rightLines.Count);

			for (int i = 0; i < totalRows; i++)
			{
				string l = (i < leftLines.Count) ? leftLines[i] : "";
				string r = (i < rightLines.Count) ? rightLines[i] : "";

				// Farbe für linke Seite bestimmen (Spieler)
				bool leftIsWreck = spieler.Any(s => s.Zerstoert && GetShipAscii(s).Contains(l));
				Console.ForegroundColor = leftIsWreck ? ConsoleColor.DarkGray : ConsoleColor.Cyan;
				Console.Write(l.PadRight(74));
				Console.ResetColor();

				Console.Write("   ");

				// Farbe für rechte Seite bestimmen (Allianz)
				bool rightIsWreck = allianz.Any(s => s.Zerstoert && GetShipAscii(s).Contains(r));
				Console.ForegroundColor = rightIsWreck ? ConsoleColor.DarkGray : ConsoleColor.Red;
				Console.WriteLine(r);
				Console.ResetColor();
			}
		}

		static void DrawShipStatusLine(FleetShipAllianz s)
		{
			if (s.Zerstoert)
				Console.ForegroundColor = ConsoleColor.DarkGray;
			else if (s.IstSpieler)
				Console.ForegroundColor = ConsoleColor.Cyan;
			else
				Console.ForegroundColor = ConsoleColor.Red;

			string flag = s.IstFlaggschiff ? " [FLAGGSCHIFF]" : "";
			Console.WriteLine($"{s.Name,-12} ({s.Typ,-16}) | Schild: {s.Schild,3}/{s.MaxSchild,-3} | Hülle: {s.Huelle,3}/{s.MaxHuelle,-3}{flag}");
			Console.ResetColor();
		}

		static string[] GetShipAscii(FleetShipAllianz s)
		{
			
			// SPIELER-SEITE (links)
			
			if (s.IstSpieler)
			{
				// WRACK-VARIANTEN FÜR SPIELER
				if (s.Zerstoert)
				{
					if (s.Typ == "UNIVERSUM-KLASSE")
					{
						return new[]
						{
					"                          _______   SCHIFFSWRACK   _______",
					"                   _____/    UNIVERSUM-Klasse (zerstört)  \\__",
					"                __/           Risse, Bruchstellen           \\__",
					"             __/       __   __x___     __x__    __           \\",
					"           _/        x/  \\_/     \\__x_/    \\__x/  \\_         \\",
					"          |      ███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓__       |",
					"          |   __/   Trümmer, offene Sektionen, Funken  \\__x  |",
					"          | _/__x__  Struktur bricht auseinander     ___\\__  |",
					"           \\___x_________ lose Module treiben davon ______\\_|"
				};
					}
					else if (s.Typ == "KREUZER")
					{
						return new[]
						{
					"      ___====x===___",
					"   __/___RUMPFSCHADEN_\\__",
					" <_____  KREUZER-WRACK  __x>",
					"   ¯¯\\_____aufgerissen_/¯¯"
				};
					}
					else if (s.Typ == "SCHWERER KREUZER")
					{
						return new[]
						{
					"     ___========x===___",
					"  __/___SCHWERER KREUZER-WRACK_\\__",
					" <__Struktur gebrochen, Decks offen__>",
					"   ¯¯\\_________Trümmerfeld_______/¯¯"
				};
					}
					else if (s.Typ == "FREGATTE")
					{
						return new[]
						{
					"    __==x__",
					" <__FREGATTE-WRACK__>",
					"    ¯¯==__¯¯"
				};
					}
					else
					{
						return new[]
						{
					"   ___xx___",
					" <__WRACK__>",
					"   ¯¯xx¯¯"
				};
					}
				}

				if (s.Typ == "UNIVERSUM-KLASSE")
				{
					return new[]
					{
				"                          _________________________________",
				"                   _____/         UNIVERSUM-Klasse          \\__",
				"                __/                                          \\__",
				"             __/                                              \\",
				"           _/                                                  \\",
				"          |                 ██████████████████████████___      |",
				"          |            ____/                             \\____ |",
				"          |       ____/                                     \\__|",
				"           \\____/                                            \\_|"
			};
				}
				else if (s.Typ == "KREUZER")
				{
					return new[]
					{
				"      ___========___",
				"   __/______________\\__",
				" <_____   KREUZER   ____>",
				"   ¯¯\\______________/¯¯"
			};
				}
				else if (s.Typ == "SCHWERER KREUZER")
				{
					return new[]
					{
				"     ___============___",
				"  __/__________________\\__",
				" <__SCHWERER  KREUZER__>",
				"   ¯¯\\________________/¯¯"
			};
				}
				else if (s.Typ == "FREGATTE")
				{
					return new[]
					{
				"    __====__",
				" <__FREGATTE__>",
				"    ¯¯====¯¯"
			};
				}
				else
				{
					return new[]
					{
				"   ___==___",
				" <__SCHIFF__>",
				"   ¯¯==¯¯"
			};
				}
			}
			else
			{
				// ALLIANZ-SEITE (rechts)
				if (s.Zerstoert)
				{
					if (s.Typ == "DREADNOUGHT")
					{
						return new[]
						{
					"       /====\\====ZERSTÖRTER=================/====\\",
					"       ███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓███",
					"   ___|    ALLIANZ-DREADNOUGHT – WRACK        |__x",
					"  /====\\====aufgebrochene Sektionen========= /====\\",
					" <______\\\\==Trümmer, brennende Module==//____x__>",
					"  \\_____/=====Struktur teilweise kollabiert==\\__/",
					"       \\___/          x          x            \\_/ ",
					"      ███▓▓   glühende Bruchstellen   ▓▓███      ",
					"      \\_____/====Trümmer treiben im All====\\____/"
				};
					}
					else if (s.Typ == "SCHWERER KREUZER")
					{
						return new[]
						{
					"		  /========WRACK==========\\",
					"         ███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓███",
					"    ___/ ALLIANZ-SCHWERER KREUZER (zerst.) \\___",
					" __/====Rumpf aufgerissen, Systeme offline===\\__",
					"<__x_x_x_x_x_x_x_Trümmer & Rauch_x_x_x_x_x_x__>",
					"  ¯¯\\========================================/¯¯",
					"	      ███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓███",
					"		  \\========WRACK=========/"
				};
					}
					else if (s.Typ == "KREUZER")
					{
						return new[]
						{
					"        /========WRACK======\\",
					"       ███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓███",
					"   ___/ ALLIANZ-KREUZER (zerstört) \\___",
					" _/====Decks offen, Glutnester========\\_",
					"<__x_x_x_x_x_Trümmerwolke_x_x_x_x_x__>",
					"  ¯¯\\==============================/¯¯",
					"       ███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓███",
					"	    \\========WRACK=====/"
				};
					}
					else if (s.Typ == "FREGATTE")
					{
						return new[]
						{
					"      ███▓▓▓▓▓▓",
					"  __/ ALLIANZ-FREG-WRACK \\__",
					"<__x_x_x_Trümmerteile_x_x__>",
					"   ¯¯\\_____aufgebrochen_/¯¯",
					"	   ███▓▓▓▓▓▓"
				};
					}
					else
					{
						return new[]
						{
					"   ___xx___",
					" <__A-WRACK__>",
					"   ¯¯xx¯¯"
				};
					}
				}

				// ALLIANZ-SCHIFFE
				if (s.Typ == "DREADNOUGHT")
				{
					return new[]
					{
				"       /====\\=============================/====\\",
				"       ███████████████████████████████████████████",
				"   ___|          ALLIANZ-DREADNOUGHT         |___",
				"  /====\\====================================/====\\",
				" <______\\\\====<><><><><><><><><><><>====//______>",
				"  \\_____/================================\\_____/",
				"       \\___/                               \\___/",
				"      ███████████████████████████████████████████",
				"      \\_____/============================\\_____/"
			};
				}
				else if (s.Typ == "SCHWERER KREUZER")
				{
					return new[]
					{
				"		  /======================\\",
				"         █████████████████████████",
				"    ___/  ALLIANZ-SCHWERER KREUZER \\___",
				" __/=================================\\__",
				"<__><><><><><><><><><><><><><><><><>__>",
				"  ¯¯\\================================/¯¯",
				"	      █████████████████████████",
				"		  \\======================/"
			};
				}
				else if (s.Typ == "KREUZER")
				{
					return new[]
					{
				"        /===============\\",
				"       ███████████████████",
				"   ___/ ALLIANZ-KREUZER  \\___",
				" _/=========================\\_",
				"<__><><><><><><><><><><><>__>",
				"  ¯¯\\=====================/¯¯",
				"       ███████████████████",
				"	    \\================/"
			};
				}
				else if (s.Typ == "FREGATTE")
				{
					return new[]
					{
				"      ██████████",
				"  __/ ALLIANZ-FREG \\__",
				"<__><><><><><><>__>",
				"   ¯¯\\_________/¯¯",
				"	   ██████████"
			};
				}
				else
				{
					return new[]
					{
				"   ___==___",
				" <__A-SCHIFF__>",
				"   ¯¯==¯¯"
			};
				}
			}
		}
		//  ANIMATIONEN: STRAHL, TORPEDO, EXPLOSION
		static void AnimateBeamStrike(bool fromPlayerSide)
		{
			// Strahlenwaffe – hoher, pulsierender Ton
			for (int frame = 0; frame < 4; frame++)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Magenta;

				if (fromPlayerSide)
					Console.WriteLine("GENESIS:  |||||||||||||||||||||||||||>>>>>>>>>>>>");
				else
					Console.WriteLine("ALLIANZ: <<<<<<<<<<<<|||||||||||||||||||||||||||");

				Console.ResetColor();

				// futuristischer Strahl-Sound
				Console.Beep(950 + frame * 30, 40);
				Thread.Sleep(50);
			}

			Console.Beep(1200, 80); // finaler Treffer
		}

		static void AnimateTorpedoStrike(bool fromPlayerSide)
		{
			// dumpfer Torpedosound
			Console.Beep(220, 120);
			Console.Beep(180, 80);

			for (int frame = 0; frame < 4; frame++)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Yellow;

				if (fromPlayerSide)
					Console.WriteLine("Flotte:   >>>===>   >>>===>   >>>===>");
				else
					Console.WriteLine("Allianz:  <===<<<   <===<<<   <===<<<");

				Console.ResetColor();
				Thread.Sleep(70);

				// steigender Pfeifton
				Console.Beep(300 + frame * 60, 60);
			}
		}

		static void AnimateFocusBeam()
		{
			// Ladeimpuls – tief und bedrohlich
			Console.Beep(160, 150);
			Console.Beep(190, 150);

			// 4 Frames Animation für "Aufladen" + "Feuern"
			for (int frame = 0; frame < 4; frame++)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Cyan;

				// Breites 5-Strahlen-System
				Console.WriteLine("ALLE SCHIFFE BÜNDELN IHRE ENERGIE →");
				Console.WriteLine();
				Console.WriteLine("   |||||||||||||||||||||||||||||||||||||||||||||||>>>>>>>>>>>>>");
				Console.WriteLine("   |||||||||||||||||||||||||||||||||||||||||||||||>>>>>>>>>>>>>");
				Console.WriteLine("   |||||||||||||||||||||||||||||||||||||||||||||||>>>>>>>>>>>>>");
				Console.WriteLine("   |||||||||||||||||||||||||||||||||||||||||||||||>>>>>>>>>>>>>");
				Console.WriteLine("   |||||||||||||||||||||||||||||||||||||||||||||||>>>>>>>>>>>>>");

				Console.ResetColor();

				// 5 verschiedene Frequenzen → 5 Strahlen
				Console.Beep(700 + frame * 40, 40);
				Console.Beep(820 + frame * 40, 40);
				Console.Beep(940 + frame * 40, 40);
				Console.Beep(1060 + frame * 40, 40);
				Console.Beep(1180 + frame * 40, 40);

				Thread.Sleep(80);
			}

			// Finaler Treffer-Impuls
			Console.Beep(1500, 150);
		}

		static void AnimateExplosion(bool leftSide)
		{
			int[] indents = leftSide
				? new[] { 2, 4, 6, 4, 2 }
				: new[] { 40, 42, 44, 42, 40 };

			// dumpfer Einschlag
			Console.Beep(180, 200);

			// Haupt-Explosion
			for (int i = 0; i < indents.Length; i++)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;

				Console.WriteLine(new string(' ', indents[i]) + "        * * * * * * * *");
				Console.WriteLine(new string(' ', indents[i]) + "      * *   B O O M   * *");
				Console.WriteLine(new string(' ', indents[i]) + "        * * * * * * * *");

				Console.ResetColor();

				// Explosion Soundeffekt – schnell, hart, abfallend
				Console.Beep(600 - i * 80, 120);

				Thread.Sleep(80);
			}

			// Trümmerteile fliegen kurz auseinander
			for (int frame = 0; frame < 4; frame++)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.DarkYellow;

				int baseIndent = leftSide ? 2 + frame * 2 : 40 - frame * 2;

				Console.WriteLine(new string(' ', baseIndent) + ".   .      .  *   .");
				Console.WriteLine(new string(' ', baseIndent + 6) + "x     .   x    .");
				Console.WriteLine(new string(' ', baseIndent + 3) + ".   *    .    x   .");

				Console.ResetColor();
				Console.Beep(200 - frame * 20, 60);
				Thread.Sleep(70);
			}

			// Nachhall der Explosion
			Console.Beep(120, 200);
		}
		static void RunAllianzSiegCinematic(List<FleetShipAllianz> spieler,
									List<FleetShipAllianz> allianz,
									FleetShipAllianz genesis)
		{
			// Erst ein kleines Cinematic mit bewegten Schiffen / Explosionen
			ShowFinalBattleCinematic();

			// Dann wechseln wir auf die Brücke der GENESIS
			Console.Clear();
			Console.WriteLine("=== BRÜCKE DER GENESIS – NACH DER SCHLACHT ===");
			Console.WriteLine();
			Thread.Sleep(400);

			// Typewriter-Story, KEINE Anführungszeichen in den Dialogen
			TypeText("Die Brücke der GENESIS ist in rotes Licht getaucht. Rauch hängt in der Luft, Konsolen flackern.", 10);
			TypeText("Crewmitglieder arbeiten an überhitzten Stationen, irgendwo knistert eine beschädigte Leitung.", 10);
			TypeText("", 10);

			TypeText("Kate: Captain, ich habe die Schilde der Allianz-Schiffe analysiert.", 10);
			TypeText("Kate: Ihre Schutzfelder sind unseren weit überlegen, aber ihre übrigen Systeme wirken erstaunlich vertraut.", 10);
			TypeText("Kate: Ich vermute, ihre Technik basiert auf ähnlichen Grundlagen wie unsere aber doch fortschrittlicher.", 10);
			TypeText("", 10);

			TypeText("Oduro: Man merkt,sie haben wenig Kampferfahrung. Ihre Formationen sind zu eng, ihre Salven unkoordiniert.", 10);
			TypeText("Oduro: Sie verlassen sich zu sehr auf ihre Schilde. Wenn wir sie brechen können, ist der Rest verwundbar.", 10);
			TypeText("", 10);

			TypeText("Ich atme tief durch und blicke auf die taktische Projektion. Überlappende Schilde, dichte Energieknoten.", 10);
			TypeText("Ich: Wenn wir ihre Schilde nicht alleine knacken können, dann bündeln wir unsere Waffen.", 10);
			TypeText("", 10);

			TypeText("Ich öffne den Flotten-Komkanal.", 10);
			TypeText("Ich: An alle Schiffe des Verbandes EINS: hier der Captain der GENESIS.", 10);
			TypeText("Ich: Wir gehen auf koordinierte Zielschüsse über. Fokusfeuer auf ihre Schildsysteme.", 10);
			TypeText("", 10);

			TypeText("Ich werfe Kate einen Blick zu.", 10);
			TypeText("Ich: Wo liegen die Hauptenergiezentren der Allianz-Schiffe?", 10);
			TypeText("", 10);

			TypeText("Sensoroffizier: Captain, laut Auswertung konzentrieren sich ihre Reaktorkerne in drei Zonen.", 10);
			TypeText("Sensoroffizier: Eine im vorderen Rumpfsegment, eine unter der zentralen Struktur, eine nahe der Triebwerkssektion.", 10);
			TypeText("", 10);

			TypeText("Ich: Übertragen Sie die Koordinaten an alle Schiffe. Live-Feed auf die Zielcomputer.", 10);
			TypeText("Sensoroffizier: Koordinaten werden an den Verband gesendet, Captain.", 10);
			TypeText("", 10);

			TypeText("Ich: Waffenoffizier, haben Sie die Zielpunkte?", 10);
			TypeText("Waffenoffizier: Bestätigt, Captain. Die Energieknoten sind markiert. Alle Strahlenwaffen können synchronisieren.", 10);
			TypeText("", 10);

			TypeText("Ich: Gut. Kom-Kanal offen halten.", 10);
			TypeText("Ich: An die Flotte; auf mein Zeichen bündeln wir alle Strahlenwaffen auf die übertragene Zielzone.", 10);
			TypeText("Ich: Nicht feuern, bevor alle Systeme bereit sind. Wir feuern gleichzeitig.", 10);
			TypeText("", 10);

			TypeText("Oduro: Das wird ein Feuerwerk.", 10);
			TypeText("Ich: Genau das ist der Plan.", 10);
			TypeText("", 10);

			TypeText("Ein Summen geht durch die Brücke, als die GENESIS und die übrigen Schiffe Energie in ihre Strahllanzen pumpen.", 10);
			TypeText("Warnanzeigen leuchten, die Reaktoren arbeiten an der Grenze.", 10);
			TypeText("", 10);

			TypeText("Waffenoffizier: Alle Batterien geladen. Flotte meldet Feuerbereitschaft.", 10);
			TypeText("Ich: Dann los.", 10);
			TypeText("Ich: An alle Schiffe – Feuer!", 10);
			TypeText("", 10);

			// Kleines Fokus-Strahlenfeuer als Zwischencinematic
			ShowFocusedVolleyCinematic();

			TypeText("Der Hauptschirm wird zu einem Sturm aus Licht. Fünf Strahlenlanzen schießen von jedem unserer Schiffe zugleich.", 10);
			TypeText("Sie treffen dieselbe Stelle auf dem Allianz-Dreadnought. Die Schilde flackern, verzerren, reißen auf.", 10);
			TypeText("Die gebündelte Energie frisst sich durch den Schild und in die gewaltige Hülle.", 10);
			TypeText("", 10);

			TypeText("Die äußere Panzerung glüht weiß, dann bricht sie auf. Etwas im Inneren des Schiffes kollabiert.", 10);
			TypeText("Ein einzelner, gleißender Blitz, dann reißt eine Explosion den Dreadnought in Stücke.", 10);

			// Noch einmal Explosion-Animation auf der Allianz-Seite
			AnimateExplosion(false);

			TypeText("", 10);
			TypeText("Ein Schub aus Trümmern und Plasma rollt auf unsere Formation zu und lässt die Schiffe erzittern.", 10);
			TypeText("Auf der Brücke bricht Jubel aus. Für einen Moment übertönt der Lärm jede Warnanzeige.", 10);
			TypeText("", 10);

			TypeText("Oduro: Dreadnought neutralisiert! Die Flotte meldet mehrere Folgetreffer auf Sekundärziele.", 10);
			TypeText("Kate: Die Methode funktioniert. Wenn wir ihre Schildknoten überladen, kollabiert das gesamte System.", 10);
			TypeText("", 10);

			TypeText("Hale erscheint auf dem Schirm, Schweiß auf der Stirn, doch ein hartes Funkeln in den Augen.", 10);
			TypeText("Hale: Hier Verband ZWEI. Ihre Taktik scheint aufzugehen, Captain.", 10);
			TypeText("Hale: Wir übernehmen das Fokusfeuer-Muster und drücken ihre Linie von der Flanke her auf.", 10);
			TypeText("", 10);

			TypeText("Ich: Verstanden, Hale. Halten Sie den Druck aufrecht. Wir kümmern uns um jeden, der durchbricht.", 10);
			TypeText("", 10);

			TypeText("Hale: Es gibt noch etwas, Captain. Die Allianz hat die Einrichtung der Blutsväter schwer angegriffen, die Grauen.", 10);
			TypeText("Hale: Die Grauen ziehen sich zurück. Es war ein Außenposten für Sie.", 10);
			TypeText("", 10);

			TypeText("Ich runzle die Stirn.", 10);
			TypeText("Ich: Die Grauen verlassen die Einrichtung?", 10);
			TypeText("", 10);

			TypeText("Hale: Bestätigt. Sie lösen ihre Präsenz dort auf. Es war nie ihr Zuhause.", 10);
			TypeText("Hale: Die Einrichtung war für sie nur ein Außenposten um uns zu beobachten und die Technologie der Blutsväter zu schützen.", 10);
			TypeText("Hale: Aber jetzt schließen sie sich unserem Flottenverband an und helfen bei der Verteidigung der Erde.", 10);
			TypeText("", 10);

			TypeText("Oduro: Die Grauen auf unserer Seite. Das verschiebt das Kräfteverhältnis deutlich.", 10);
			TypeText("", 10);

			TypeText("Ich: Dann haben wir endlich eine echte Chance.", 10);
			TypeText("", 10);

			TypeText("Stunden vergehen im Feuer der Schlacht. Explosionen wie Sternenblitze, Trümmerstücke, die an der GENESIS vorbeiziehen.", 10);
			TypeText("Trotz ihrer Überzahl verlieren die Allianz-Schiffe ein Schiff nach dem anderen.", 10);
			TypeText("Ihre Schilde sind mächtig, aber ihre Taktik veraltet und unsere Strahlenwaffen unerbittlich.", 10);
			TypeText("", 10);

			TypeText("Hale meldet sich erneut.", 10);
			TypeText("Hale: Captain, die Allianz-Flotte ist auf dem Rückzug. Die Grauen halten die äußeren Linien.", 10);
			TypeText("Hale: Jetzt ist der Zeitpunkt gekommen, die ARGUS zu verfolgen.", 10);
			TypeText("", 10);

			TypeText("Mein Herz schlägt schneller.", 10);
			TypeText("Ich: Konnten Sie ihren Sprungvektor rekonstruieren?", 10);
			TypeText("", 10);

			TypeText("Hale nickt knapp.", 10);
			TypeText("Hale: Wir haben ihre Signatur nachverfolgt.", 10);
			TypeText("Hale: Ich sende Ihnen eine neue Konfiguration für Ihre Sensoren.", 10);
			TypeText("Hale: Damit kann die GENESIS den Riss im Raum orten, dem die ARGUS erzeugt hat.", 10);
			TypeText("", 10);

			TypeText("Ich spüre eine Gänsehaut. Das Ziel lag plötzlich nicht mehr im Dunkeln.", 10);
			TypeText("", 10);

			TypeText("Hale: Ihre oberste Priorität ist klar, Captain.", 10);
			TypeText("Hale: Sie müssen die ARGUS finden und vernichten, bevor sie den Ursprung des Virus erreicht.", 10);
			TypeText("Hale: Wenn ihre Erbauer die Sprungtechnologie der Blutsväter mit dem Virus verbinden, ist keine Galaxie mehr sicher.", 10);
			TypeText("", 10);

			TypeText("Ich: Verstanden.", 10);
			TypeText("Ich: Wir werden sie aufhalten, was immer es kostet.", 10);
			TypeText("", 10);

			TypeText("Der Schirm wird schwarz. Nur die Sterne bleiben, während die letzten Explosionen im Hintergrund verklingen.", 10);
			TypeText("", 10);

			// Vorbereitung auf den Sprung
			TypeText("Ich: Steuermann, bringen Sie uns an den Rand des Sonnensystems. Maximalgeschwindigkeit unter Licht Triebwerke.", 10);
			TypeText("Steuermann: Kurs gesetzt, Captain. Beschleunigung läuft.", 10);
			TypeText("", 10);

			TypeText("Die GENESIS beschleunigt. Die Sonne schrumpft hinter uns zu einem gleißenden Punkt.", 10);
			TypeText("Vor uns öffnet sich die Leere, nur gefüllt mit dem Nachleuchten der Schlacht.", 10);
			TypeText("", 10);

			TypeText("Ich: Maschinenraum, Status des Sprungkerns?", 10);
			TypeText("Maschinenraum: Sprungkern stabil. Energiekammern aufgeladen. Wir sind bereit, Captain.", 10);
			TypeText("", 10);

			TypeText("Ich: Steuermann, haben Sie die von Hale übermittelten Koordinaten?", 10);
			TypeText("Steuermann: Koordinaten liegen an. Sprungfenster wird berechnet.", 10);
			TypeText("", 10);

			TypeText("Die Sprunggondeln der GENESIS laden sich auf. Gewaltige Energieströme fließen durch das Schiff.", 10);
			TypeText("Außerhalb biegt sich der Raum, als würde etwas Unsichtbares an den Sternen ziehen.", 10);
			TypeText("", 10);

			TypeText("Maschinenraum: Sprungfenster steht. Alle Systeme im grünen Bereich.", 10);
			TypeText("Ich: Dann los. Leiten Sie die Sequenz ein.", 10);
			TypeText("", 10);

			// kleiner Sprung-Cinematic
			ShowJumpCinematic();

			TypeText("Für einen Moment scheint alles zu verstummen. Die Sterne verschwinden, als würde jemand das Universum auslöschen.", 10);
			TypeText("Nur der Puls meines eigenen Herzens ist noch zu hören.", 10);
			TypeText("", 10);

			TypeText("Ich: Die Suche hat begonnen.", 10);
			TypeText("Ich: Kapitel eins endet hier aber unsere Jagd nach der ARGUS hat gerade erst begonnen.", 10);
			TypeText("", 15);

			// Speicherpunkt nach der Schlacht
			SaveGame("Scene_AllianzFlottenschlacht_Ende");

			Console.WriteLine();
			Console.WriteLine("Drücke eine Taste, um Kapitel 1 abzuschließen...");
			Console.ReadKey(true);
		}
		// KURZES CINEMATIC: FLUG DER SCHIFFE + EXPLOSIONEN
		static void ShowFinalBattleCinematic()
		{
			for (int frame = 0; frame < 4; frame++)
			{
				Console.Clear();

				// Spielerflotte links
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("  GENESIS        AURORA        VALKYRIE        HORIZON        LANCER");
				Console.WriteLine();

				string indentPlayer = new string(' ', frame * 2);
				Console.WriteLine(indentPlayer + "  [=====\\____   [====\\____   [===\\___   [==\\___   [=\\__");
				Console.WriteLine(indentPlayer + "  ======>>>>>   =====>>>>   ====>>>    ===>>     ==>");
				Console.ResetColor();

				Console.WriteLine();
				Console.WriteLine();

				// Allianzflotte rechts
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("                              A-DREAD-1    A-DREAD-2    A-CRUISER-1    A-CRUISER-2    A-FRIG-1");
				Console.WriteLine();

				string indentEnemy = new string(' ', Math.Max(0, 30 - frame * 2));
				Console.WriteLine(indentEnemy + "/====\\=============================\\====/");
				Console.WriteLine(indentEnemy + "██████████████████████████████████████████");
				Console.WriteLine(indentEnemy + "\\====/=============================\\====/");
				Console.ResetColor();

				// Kleine Explosionen zwischen den Linien
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine();
				Console.WriteLine(new string(' ', 25) + "*   *   *");
				Console.WriteLine(new string(' ', 23 + frame) + "  *  x  *");
				Console.WriteLine(new string(' ', 27 - frame) + "*   *   *");
				Console.ResetColor();

				Console.Beep(400 + frame * 80, 100);
				Thread.Sleep(220);
			}

			// Abschließende große Explosion rechts
			AnimateExplosion(false);
		}
		// KURZES CINEMATIC: GEBÜNDELTE STRAHLEN
		static void ShowFocusedVolleyCinematic()
		{
			for (int frame = 0; frame < 3; frame++)
			{
				Console.Clear();

				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Flotte: Bündelung aller Strahlenwaffen...");
				Console.WriteLine();
				Console.WriteLine("    ||      ||      ||      ||      ||");
				Console.WriteLine("    ||      ||      ||      ||      ||");
				Console.WriteLine("    ||      ||      ||      ||      ||");
				Console.ResetColor();

				Console.WriteLine();

				Console.ForegroundColor = ConsoleColor.Magenta;
				string beam = new string('>', 10 + frame * 5);
				Console.WriteLine("    ||=======" + beam);
				Console.WriteLine("    ||=======" + beam);
				Console.WriteLine("    ||=======" + beam);
				Console.WriteLine("    ||=======" + beam);
				Console.WriteLine("    ||=======" + beam);
				Console.ResetColor();

				Console.Beep(900 + frame * 80, 120);
				Thread.Sleep(200);
			}
		}
		// KURZES CINEMATIC: SPRUNG DER GENESIS
		static void ShowJumpCinematic()
		{
			for (int frame = 0; frame < 4; frame++)
			{
				Console.Clear();

				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("GENESIS: Sprungvorbereitung läuft...");
				Console.WriteLine();

				string indent = new string(' ', frame * 4);
				Console.WriteLine(indent + "          _________________________________");
				Console.WriteLine(indent + "     ____/        GENESIS – SPRUNGKERn       \\__");
				Console.WriteLine(indent + "  __/   Raum krümmt sich, Licht verzerrt      \\__");
				Console.WriteLine(indent + " /______________________________________________\\");
				Console.ResetColor();

				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine();
				Console.WriteLine(indent + "        ((((((( Raumriss baut sich auf )))))))");
				Console.ResetColor();

				Console.Beep(300 + frame * 150, 150);
				Thread.Sleep(250);
			}

			// letzter Frame – kurzer „Stromausfall“
			Console.Clear();
			Console.Beep(120, 300);
			Thread.Sleep(300);
		}
	}
}
		