using UnityEngine;
using System.Collections;


public class NonPhysicsPlayerTester : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float jumpHeight = 13f;
	public float terminalVelocity = -30f;

	public float startingHeight = 0f;

	[HideInInspector]
	// 0 = up, 1 = right, 2 = down, 3 = left
	public int shootDir = 1;
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	private bool movementLocked = false;
	private PlayerHealth health;





	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
		health = GetComponent<PlayerHealth>();
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		if (hit.collider.tag == "DestructableGround") {
			movementLocked = true;
			Debug.Log("Locked");
			Destroy(hit.collider.gameObject);
		}
		else if (hit.collider.tag == "Enemy") {
			int damage = hit.collider.GetComponent<EnemyDamage>().damage;
			health.decreaseHealth(damage);
		}
		// bail out on plain old ground hits cause they arent very interesting
		else if( hit.normal.y == 1f ) {
			movementLocked = false;
			return;
		}

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{

	}


	void onTriggerExitEvent( Collider2D col )
	{

	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if (_controller.isGrounded) {
			_velocity.y = 0;
		}

		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if( h > 0)
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else if( h < 0)
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded ) {
				if (v < 0)
					_animator.Play (Animator.StringToHash("Duck"));
				else if (v > 0)
					_animator.Play (Animator.StringToHash("LookUp"));
				else
					_animator.Play( Animator.StringToHash( "Idle" ) );
			}
		}
		if (v > 0) {
			shootDir = 0;
		} else {
			if (v < 0 && !_controller.isGrounded) {
				shootDir = 2;
			} else if (transform.localScale.x > 0) {
				shootDir = 1;
			} else {
				shootDir = 3;
			}
		}

		if (movementLocked) {
			normalizedHorizontalSpeed = 0;
		}

		// we can only jump whilst grounded
		if( _controller.isGrounded && Input.GetButtonDown("Jump") && !movementLocked)
		{
			_velocity.y = jumpHeight;
			Debug.Log ("Jumped");
			_animator.Play( Animator.StringToHash( "Jump" ) );
		} else if (!_controller.isGrounded && _velocity.y > 0 && !Input.GetButton ("Jump")) {
			_velocity.y = 0;
		}

		// apply horizontal speed smoothing it
		_velocity.x = runSpeed * normalizedHorizontalSpeed;

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		if (_velocity.y < terminalVelocity) {
			_velocity.y = terminalVelocity;
		}

		_controller.move( _velocity * Time.deltaTime );
	}

	public void LockMovement() {
		movementLocked = true;
	}

	public void UnlockMovement() {
		movementLocked = false;
	}

}