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
			Color newColor = Random.ColorHSV( 0, 1, 0.5f, 1, 1, 1, 1, 1 ); ;
			GetComponent<SpriteRenderer>().color = newColor;
			GetComponentInChildren<TextMesh>().color = newColor;
			yield return new WaitForSeconds( 1 );
		}
	}
}
