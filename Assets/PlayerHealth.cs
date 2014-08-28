using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	private int health = 100;
	private int maxHealth = 100;

	// Use this for initialization
	public void increaseHealth(int newHealth) {
		health += newHealth;
	}
	
	public void decreaseHealth(int newHealth) {
		health -= newHealth;
	}
	
	public void setHealth(int newHealth) {
		health = newHealth;
	}
	
	public int getHealth() {
		return health;
	}
	
	public void increaseMaxHealth(int newHealth) {
		maxHealth += newHealth;
	}
	
	public void setMaxHealth(int newHealth) {
		maxHealth = newHealth;
	}
	
	public int getMaxHealth() {
		return maxHealth;
	}
}
