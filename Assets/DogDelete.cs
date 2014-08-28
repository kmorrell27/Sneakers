using UnityEngine;
using System.Collections;

public class DogDelete : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D col) {
		GameObject dog = GameObject.Find("Dog");
		Debug.Log ("Whooh");
		if (col.gameObject.name == "Player") {
			Debug.Log ("Yay");
			Destroy(dog);
		}
	}
}
