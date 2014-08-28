using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.


	private NonPhysicsPlayerTester controller;		// Reference to the PlayerControl script.


	void Awake()
	{
		// Setting up the references.
		controller = transform.root.GetComponent<NonPhysicsPlayerTester>();
	}


	void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetButtonDown("Fire1"))
		{
			if (controller.shootDir == 0) {
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler (new Vector3(0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(0, speed);
			}
			// If the player is facing right...
			else if(controller.shootDir == 1)
			{
				// ... instantiate the rocket facing right and set its velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
			}
			else if (controller.shootDir == 2) {
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler (new Vector3(180f, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(0, -speed);
			}
			else if (controller.shootDir == 3)
			{
				// Otherwise instantiate the rocket facing left and set its velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,180f,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);
			}
		}
	}
}
