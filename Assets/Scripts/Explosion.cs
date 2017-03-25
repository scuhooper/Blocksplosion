using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public void Explode( Vector3 position, GameObject particles )
	{
		GameObject explosion = Instantiate( particles, position, Quaternion.Euler( Vector3.zero ) );
		Destroy( explosion, 2f );
	}
}
