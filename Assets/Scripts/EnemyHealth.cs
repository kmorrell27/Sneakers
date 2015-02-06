using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	private int health;
	public int maxHealth;
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake() {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void DecreaseHealth(int healthDecrease) {
		health -= healthDecrease;
		if (health <= 0) {
			Die ();
		}
	}
	
	public void IncreaseHealth(int healthIncrease) {
	    health += healthIncrease;
	    if (health > maxHealth) {
	    	health = maxHealth;
	    }
	}
	
	public void Die() {
		Destroy(gameObject);
	}
	
}
