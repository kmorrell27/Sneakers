using UnityEngine;
using System.Collections;

public class shortRadius : MonoBehaviour {

	Dog dog;

	// Use this for initialization
	void Start () {
		dog = this.transform.parent.GetComponent<Dog>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
	    if (col.gameObject.tag == "Player") {
			dog.shouldWalk = true;
		}
	}
}
