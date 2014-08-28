using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {

	private CharacterController2D controller;
	private bool grounded;
	private Vector3 velocity;
	public bool aggro = false;
	private Transform player;
	private Transform pos;
	public float jumpHeight = 9f;
	public float horizontalSpeed = 3f;
	public float gravity = -25f;
	private float cooldown = 0f;
	private bool hasJumped = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Awake() {
		controller = GetComponent<CharacterController2D>();
		pos = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		bool grounded = controller.isGrounded;
		Vector3 velocity = controller.velocity;
		if (cooldown > 0) {
			cooldown = cooldown - Time.deltaTime;
		} else {
			cooldown = 0;
		}
		if (grounded & hasJumped) {
			velocity.y = 0;
			cooldown = 0.2f;
			hasJumped = false;
		}
		if (aggro && grounded && cooldown == 0) {
			Vector3 distance = player.position - pos.position;
			velocity.x = Mathf.Sign (distance.x) * horizontalSpeed;
			velocity.y = jumpHeight;
			if (Mathf.Sign (velocity.x) == Mathf.Sign(pos.localScale.x)) {
				pos.localScale = new Vector3(pos.localScale.x * -1, pos.localScale.y, pos.localScale.z);
			}
			hasJumped = true;
		} else if (grounded) {
			velocity.x = 0;
		}
		// apply gravity before moving
		velocity.y += gravity * Time.deltaTime;
		
		controller.move(velocity * Time.deltaTime);
	}
}
