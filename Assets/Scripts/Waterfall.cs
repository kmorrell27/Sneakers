using UnityEngine;
using System.Collections;

public class Waterfall : MonoBehaviour {

	public int sortingLayer;
	private ParticleSystem particle;
	// Use this for initialization
	void Awake () {
		particle = GetComponent<ParticleSystem>();
		particle.renderer.sortingLayerName = "Default";
		particle.renderer.sortingOrder = sortingLayer;
	}
	
	public void fadeWaterfall() {
		float rate = particle.emissionRate;
		particle.emissionRate = rate / 4;
	}
	
	public void unfadeWaterfall() {
		float rate = particle.emissionRate;
		particle.emissionRate = rate * 4;
	}
}
