using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Animator anim = GetComponent<Animator>();
		anim.speed = 3f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}