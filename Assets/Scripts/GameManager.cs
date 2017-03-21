using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public int roundNumber = 1;
	int numberOfBalls = 1;
	Vector2 ballLaunchPosition;
	int numberOfBallsCollected;
	int totalBalls;

	public GameObject[] blocks;
	public GameObject extraBallPickup;

	float[] rowPositions = new float[8];
	float topRowY = 2.45f;

	// Use this for initialization
	void Start () {
		rowPositions[ 0 ] = -2.45f;
		rowPositions[ 1 ] = -1.75f;
		rowPositions[ 2 ] = -1.05f;
		rowPositions[ 3 ] = -.35f;
		rowPositions[ 4 ] = .35f;
		rowPositions[ 5 ] = 1.05f;
		rowPositions[ 6 ] = 1.75f;
		rowPositions[ 7 ] = 2.45f;

		StartRound();
	}
	
	// Update is called once per frame
	void Update () {
		if ( numberOfBallsCollected == totalBalls )
		{
			// end current round and start new round
			EndRound();
			StartRound();
		}

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			EndRound();
			StartRound();
		}
	}

	public void IncreaseBallCount()
	{
		numberOfBalls++;
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if(numberOfBallsCollected == 0)
			ballLaunchPosition = collision.transform.position;

		numberOfBallsCollected++;
		Destroy( collision.gameObject );
	}

	void StartRound()
	{
		totalBalls = numberOfBalls;
		SetupNextRowOfBlocks();
	}

	void EndRound()
	{
		numberOfBallsCollected = 0;
		roundNumber++;
		Block[] survivingBlocks = FindObjectsOfType<Block>();
		foreach ( Block b in survivingBlocks )
		{
			b.LowerBlock();
		}
	}

	void SetupNextRowOfBlocks()
	{
		int ballPickupIndex = Random.Range( 0, 7 );

		for ( int i = 0; i < rowPositions.Length; i++ )
		{
			if ( i == ballPickupIndex )
				Instantiate( extraBallPickup, new Vector2( rowPositions[ i ], topRowY ), transform.rotation );
			else
			{
				float rand = Random.Range( 0f, 1f );
				if ( rand < .5 )
					Instantiate( blocks[ 0 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if(rand<.6)
					Instantiate( blocks[ 1 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if(rand<.7)
					Instantiate( blocks[ 2 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if(rand<.8)
					Instantiate( blocks[ 3 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if(rand<.9)
					Instantiate( blocks[ 4 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
			}
		}
	}
}
