using UnityEngine;
using System.Collections;

public class WaterfallSortingLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = "Default";
		particleSystem.renderer.sortingOrder = -1;
	}
}
