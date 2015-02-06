using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour 
{

	public GameObject explosion;	// Prefab of explosion effect.

	
	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		OnExplode();
		if(col.gameObject.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<EnemyHealth>().DecreaseHealth(1);
			
			
			// Destroy the rocket.
			Destroy (gameObject);
		}
	}
	
	void OnCollisionEnter2D (Collision2D col) 
	{
		OnExplode();
		Debug.Log (col.gameObject.tag);

		if(col.gameObject.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<EnemyHealth>().DecreaseHealth(1);


			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if it hits a destructable wall
		else if(col.gameObject.tag == "DestructableBlock")
		{
			// Destroy the block.
			Destroy (col.gameObject);

			// Destroy the rocket.
			Destroy (gameObject);
		}
		else
		{
			Destroy (gameObject);
		}
	}


}
