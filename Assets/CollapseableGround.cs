using UnityEngine;
using System.Collections;

public class CollapseableGround : MonoBehaviour {

	private Transform myTransform;
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake() {
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Player") {
			if (col.transform.position.y > myTransform.position.y) {
				Destroy(gameObject);
			}
		}
	}
}
