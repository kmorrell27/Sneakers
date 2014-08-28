using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public enum Menu {
		MainMenu,
		NewGame,
		Continue
	}

	public Menu currentMenu;

	void OnGUI () {

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		if(currentMenu == Menu.MainMenu) {

			GUILayout.Box("Sneakers");
			GUILayout.Space(10);

			if(GUILayout.Button("New Game")) {
				Game.current = new Game();
				currentMenu = Menu.NewGame;
			}

			if(GUILayout.Button("Continue")) {
				SaveLoad.Load();
				currentMenu = Menu.Continue;
			}

			if(GUILayout.Button("Quit")) {
				Application.Quit();
			}
		}

		else if (currentMenu == Menu.NewGame) {

			GUILayout.Box("Name Your Characters");
			GUILayout.Space(10);

			GUILayout.Label("What's your name?");
			Game.current.name = GUILayout.TextField(Game.current.name, 20);
			GUILayout.Label("Boy or girl?");
			Game.current.gender = GUILayout.SelectionGrid(Game.current.gender, new string[] {"Boy", "Girl"}, 2, "toggle");
			Game.current.zone = 1;

			if(GUILayout.Button("Start game")) {
				//Save the current Game as a new saved Game
				SaveLoad.Save();
				//Move on to game...
				Application.LoadLevel(1);
			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}

		}

		else if (currentMenu == Menu.Continue) {
			
			GUILayout.Box("Select Save File");
			GUILayout.Space(10);
			
			foreach(Game g in SaveLoad.savedGames) {
				if(GUILayout.Button(g.name + " - " + g.zoneString())) {
					Game.current = g;
					//Move on to game...
					Application.LoadLevel(1);
				}

			}

			GUILayout.Space(10);
			if(GUILayout.Button("Cancel")) {
				currentMenu = Menu.MainMenu;
			}
			
		}

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}
}
