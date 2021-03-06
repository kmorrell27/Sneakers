﻿using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
	
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	
	//current progress
	public float barDisplay;
	
	Vector2 pos = new Vector2(10,50);
	Vector2 size = new Vector2(96,32);
	
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private PlayerHealth health;
	
	void Start() {
		health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	void OnGUI()
	{
		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y), emptyTex, progress_empty);
		
		GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), fullTex, progress_full);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
		
		GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);
		
		GUI.EndGroup();
		GUI.EndGroup();
	}
	
	void Update()
	{
		//the player's health
		barDisplay = (float)health.getHealth() / (float)health.getMaxHealth();
	}
	
}