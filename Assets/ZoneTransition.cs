using UnityEngine;
using System.Collections;

public class ZoneTransition : MonoBehaviour {

	public int zone;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2d(Collider2D col) {
		Application.LoadLevel (zone);
	}
}
