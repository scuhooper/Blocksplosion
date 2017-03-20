using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine( TimerToChangeColor() );
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator TimerToChangeColor()
	{
		while ( true )
		{
			GetComponent<SpriteRenderer>().color = Random.ColorHSV( 0.2f, 1, 0.2f, 1, 0.2f, 1, 1, 1 );
			yield return new WaitForSeconds( 1 );
		}
	}
}
