using UnityEngine;
using System.Collections;

public class BridgeCollapse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
