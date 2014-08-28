using UnityEngine;
using System.Collections;

public class FrogAggro : MonoBehaviour {

	private Frog parent;

	// Use this for initialization
	void Start () {
		parent = this.transform.parent.GetComponent<Frog>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			parent.aggro = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			parent.aggro = false;
		}
	}
}
