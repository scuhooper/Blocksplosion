﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public static int speed = 5;

	public bool bIsColliding = false;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = FindObjectOfType<GameManager>().direction * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
