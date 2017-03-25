using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = FindObjectOfType<GameManager>().direction * 7;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
