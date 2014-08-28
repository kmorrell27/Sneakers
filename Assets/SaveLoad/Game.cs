using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game { //don't need ": Monobehaviour" because we are not attaching it to a game object

	public static Game current;
	public string name;
	public int gender;
	public int zone;

	public Game () {
		name = "";
		gender = 0;
		zone = 1;
	}

	public string zoneString() {
		switch (zone) {
			case 1:
				return "Beginning";
			default:
				return "";
		}
	}
}
