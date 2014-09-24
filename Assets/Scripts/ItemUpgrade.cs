using UnityEngine;
using System.Collections;

public class ItemUpgrade : MonoBehaviour {

	public MyItemUpgrade upgrade;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter2D() {
		player.AddComponent(upgrade.ToString());
		Destroy(gameObject);
	}
	
	public enum MyItemUpgrade {
		Whistle,
		HighJump,
		DoubleJump
	}
}