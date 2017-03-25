using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallPickup : Pickup {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.tag == "Ball" )
		{
			FindObjectOfType<GameManager>().IncreaseBallCount();
			Destroy( gameObject );
		}
		else if ( collision.gameObject.tag == "Background" )
			Destroy( this );
	}
}
