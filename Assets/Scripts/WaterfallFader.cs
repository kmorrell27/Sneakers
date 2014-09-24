using UnityEngine;
using System.Collections;

public class WaterfallFader : MonoBehaviour {

	private Waterfall wf;
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake() {
		wf = this.GetComponentInParent<Waterfall>();
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			wf.fadeWaterfall();
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			wf.unfadeWaterfall();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
