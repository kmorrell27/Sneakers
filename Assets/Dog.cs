using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour {

    // movement config
	public float gravity = -25f;
	public float runSpeed = 10f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	
	private float normalizedHorizontalSpeed = 0;
	
	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	private bool hasJumped;
	public bool shouldWalk = false;
	private int randAnim = 0;
	private AudioSource _audio;
	private bool barked = false;


	public float timer = 0f;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		_animator.Play (Animator.StringToHash("Dog_Walk"));
		_audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		_velocity = _controller.velocity;

		if (shouldWalk) {
			normalizedHorizontalSpeed = -1f;
			_animator.Play (Animator.StringToHash("Dog_Walk"));

		} else {
			normalizedHorizontalSpeed = 0;
			if (timer >= 1.0f) {
				randAnim = Random.Range (0, 5);
				timer = 0f;
				barked = false;
			}
			if (randAnim == 0) {
        		_animator.Play (Animator.StringToHash("Dog_Idle"));
			} else if (randAnim == 1 || randAnim == 2) {
				_animator.Play (Animator.StringToHash("Dog_Scratch"));
			} else {
				_animator.Play (Animator.StringToHash("Dog_Look"));
				if (!barked) {
					_audio.Play();
					barked = true;
				}
			}
		}
		// apply horizontal speed smoothing it
		_velocity.x =  normalizedHorizontalSpeed * runSpeed;
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		
		_controller.move( _velocity * Time.deltaTime );
	}
}
