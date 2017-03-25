using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void LowerPickup()
	{
		StartCoroutine( MoveBlockDown() );
	}

	IEnumerator MoveBlockDown()
	{
		float newY = transform.position.y - .7f;
		float moveY = .05f;
		while ( transform.position.y > newY )
		{
			transform.position = new Vector2( transform.position.x, transform.position.y - moveY );
			if ( transform.position.y < newY )
			{
				transform.position = new Vector2( transform.position.x, newY );
			}
			yield return null;
		}
	}
}
