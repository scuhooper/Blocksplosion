using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : Pickup {
	bool bHasBeenUsed;

	// Use this for initialization
	void Start () {
		bHasBeenUsed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.tag == "Ball" )
		{
			if ( !bHasBeenUsed )
				bHasBeenUsed = true;

			Vector2 newSpeed = new Vector2( Random.Range( 0f, 1f ), Random.Range( .2f, 1f ) );
			newSpeed.Normalize();
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = newSpeed * 7;
		}
	}

	public override void LowerPickup()
	{
		if ( bHasBeenUsed )
			Destroy( gameObject );
		else
			base.LowerPickup();
	}
}
