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
	private Transform curTransform;
	private BoxCollider2D bc;
	private bool justHit;
	private SpriteRenderer renderer;
	private bool invincibilityFrames;




	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
		health = GetComponent<PlayerHealth>();
		curTransform = GetComponent<Transform>();
		bc = GetComponent<BoxCollider2D>();
		renderer = GetComponent<SpriteRenderer>();
		
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		
		if (hit.collider.tag != "Untagged")
			Debug.Log (hit.collider.tag);

		// bail out on plain old ground hits cause they arent very interesting
		else if( hit.normal.y == 1f ) {
			movementLocked = false;
			return;
		}

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}
	
	void onControllerCollidedEvent() {
	
	}

	void onTriggerEnterEvent( Collider2D col )
	{
		if (col.tag == "Enemy") {
			if (invincibilityFrames) {
				// I don't care, let's pass through them.
				return;
			}
			// Find where it's coming from.
			Vector3 center = col.bounds.center;
			bool right = ((center.x - bc.bounds.center.x) > 0);
			PlayerHit(right);
			int damage = col.GetComponent<EnemyDamage>().damage;
			health.decreaseHealth(damage);
			StartCoroutine(BlinkPlayer());
		}	
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
			// Once we've landed the player can move again.
			if (justHit) {
				UnlockMovement();
			}
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

		// If we've locked the player's movement, keep their x the same.
		if (movementLocked) {
			normalizedHorizontalSpeed = Mathf.Sign(_velocity.x);
		}

		// we can only jump whilst grounded
		if( _controller.isGrounded && Input.GetButtonDown("Jump") && !movementLocked)
		{
			_velocity.y = jumpHeight;
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
	
	public void PlayerHit(bool right) {
		LockMovement();
		float x = right ? -30f : 30f;
		float y = 20f;
		Vector3 _velocity = new Vector3(x, y, 0f);
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		if (_velocity.y < terminalVelocity) {
			_velocity.y = terminalVelocity;
		}
		justHit = true;
		_controller.move (_velocity * Time.deltaTime);
	}

	public void UnlockMovement() {
		movementLocked = false;
	}
	
	public IEnumerator BlinkPlayer() {
		invincibilityFrames = true;
		gameObject.layer = LayerMask.NameToLayer("PlayerBlink");
		for(var n = 0; n < 10; n++)
		{
			renderer.enabled = true;
			yield return new WaitForSeconds(0.1f);
			renderer.enabled = false;
			yield return new WaitForSeconds(0.1f);
		}
		renderer.enabled = true;
		// And then undo it.
		invincibilityFrames = false;
		gameObject.layer = LayerMask.NameToLayer("Player");
		
	}
}