using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("1"))
			Application.LoadLevel("PhysicsDemo");
		else if (Input.GetButtonDown("2"))
			Application.LoadLevel("Level 1");
	}
}
