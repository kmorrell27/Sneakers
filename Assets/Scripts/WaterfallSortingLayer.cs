using UnityEngine;
using System.Collections;

public class WaterfallSortingLayer : MonoBehaviour {

	public int sortingLayer;
	// Use this for initialization
	void Awake () {
		particleSystem.renderer.sortingLayerName = "Default";
		particleSystem.renderer.sortingOrder = sortingLayer;
	}
}
