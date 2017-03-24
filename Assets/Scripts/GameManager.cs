using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public int roundNumber = 1;
	int numberOfBalls = 1;
	Vector2 ballLaunchPosition = new Vector2( 0, -3.15f );
	int numberOfBallsCollected;
	int totalBalls;

	Vector2 touchStart;
	Vector2 touchEnd;

	TouchPhase tp;

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
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			EndRound();
			StartRound();
		}
		if ( Input.touches.Length > 0 )
		{
			if ( Input.touches[ 0 ].phase == TouchPhase.Began )
				touchStart = Input.touches[ 0 ].rawPosition;
			else if ( Input.touches[ 0 ].phase == TouchPhase.Ended )
				touchEnd = Input.touches[ 0 ].rawPosition;
			else if ( Input.touches[ 0 ].phase == TouchPhase.Moved || Input.touches[ 0 ].phase == TouchPhase.Stationary )
			{
				touchEnd = Input.touches[ 0 ].rawPosition;
				Vector2 direction = touchEnd - touchStart;
				direction.Normalize();
				if ( direction.y > .2f )
				{
					LineRenderer line = new LineRenderer();
					line.SetPosition( 0, touchStart );
					line.SetPosition( 1, touchStart + direction * 4 );
					line.startColor = Color.white;
					line.endColor = Color.white;
				}
			}
		}
	}

	public void IncreaseBallCount()
	{
		numberOfBalls++;
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.tag == "Ball" )
		{
			if ( numberOfBallsCollected == 0 )
				ballLaunchPosition = collision.transform.position;

			numberOfBallsCollected++;
			Destroy( collision.gameObject );
		}
		else if ( collision.gameObject.tag == "Pickup" )
			Destroy( collision.gameObject );
		else if ( collision.gameObject.tag == "Block" )
			EndGame();

		if ( numberOfBallsCollected == totalBalls )
		{
			// end current round and start new round
			EndRound();
			StartRound();
		}
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
			b.LowerBlock();

		Pickup[] survivingPickups = FindObjectsOfType<Pickup>();
		foreach ( Pickup p in survivingPickups )
			p.LowerPickup();
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
				if ( rand < .3f )
					Instantiate( blocks[ 0 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if( rand < .35f )
					Instantiate( blocks[ 1 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if( rand < .4f )
					Instantiate( blocks[ 2 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if( rand < .45f )
					Instantiate( blocks[ 3 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if( rand < .5f )
					Instantiate( blocks[ 4 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
			}
		}
	}

	void EndGame()
	{
		SceneManager.LoadScene( "GameScene" );
	}
}
