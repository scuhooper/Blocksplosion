using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public int roundNumber = 1;
	int numberOfBalls = 1;
	Vector3 ballLaunchPosition;
	int numberOfBallsCollected;
	int totalBalls;

	Vector3 touchStart;
	Vector3 touchEnd;
	public Vector3 direction;

	TouchPhase tp;

	public GameObject ball;
	public GameObject[] blocks;
	public GameObject extraBallPickup;
	public GameObject topBorder;
	public GameObject bottomBorder;
	public GameObject bumper;
	public GameObject menuButtons;

	float[] rowPositions = new float[8];
	float topRowY = 2.45f;

	bool bIsRoundActive = false;
	bool bIsGameOver = false;

	LineRenderer line;

	Coroutine bumperSpawner;

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
		ballLaunchPosition = new Vector3( 0, -3.15f, -1f );

		line = GetComponent<LineRenderer>();
		line.enabled = false;

		bIsRoundActive = false;
		bIsGameOver = false;

		Color newColor = Random.ColorHSV( 0, 1, 0.5f, 1, 1, 1, 1, 1 ); ;
		topBorder.GetComponent<SpriteRenderer>().color = newColor;
		topBorder.GetComponentInChildren<TextMesh>().text = roundNumber.ToString();
		bottomBorder.GetComponent<SpriteRenderer>().color = newColor;
		menuButtons.SetActive( false );
	}

	// Update is called once per frame
	void Update () {
		if ( !bIsRoundActive && !bIsGameOver )
		{
			if ( Input.GetMouseButtonDown( 0 ) )
			{
				touchStart = Input.mousePosition;
				touchStart.z = -1f;
			}
			if ( Input.GetMouseButton( 0 ) )
			{
				touchEnd = Input.mousePosition;
				touchEnd.z = -1f;
				direction = touchStart - touchEnd;
				direction.Normalize();
				if ( direction.y > .2f )
				{
					line.enabled = true;
					line.SetPosition( 0, ballLaunchPosition );
					line.SetPosition( 1, ballLaunchPosition + direction * 4 );
					line.startColor = Color.white;
					line.endColor = Color.white;
				}
				else
					line.enabled = false;
			}
			if ( Input.GetMouseButtonUp( 0 ) )
			{
				line.enabled = false;
				touchEnd = Input.mousePosition;
				touchEnd.z = -1f;
				direction = touchStart - touchEnd;
				direction.Normalize();
				if ( direction.y > .2f )
				{
					LaunchBalls( direction );
				}
			}

			if ( Input.touches.Length > 0 )
			{
				if ( Input.touches[ 0 ].phase == TouchPhase.Began )
				{
					touchStart = Input.touches[ 0 ].rawPosition;
					touchStart.z = -1f;
				}
				else if ( Input.touches[ 0 ].phase == TouchPhase.Ended )
				{
					line.enabled = false;
					touchEnd = Input.touches[ 0 ].rawPosition;
					touchEnd.z = -1f;
					direction = touchStart - touchEnd;
					direction.Normalize();
					if ( direction.y > .2f )
						LaunchBalls( direction );
				}
				else if ( Input.touches[ 0 ].phase == TouchPhase.Moved || Input.touches[ 0 ].phase == TouchPhase.Stationary )
				{
					touchEnd = Input.touches[ 0 ].rawPosition;
					touchEnd.z = -1f;
					direction = touchStart - touchEnd;
					direction.Normalize();
					if ( direction.y > .2f )
					{
						line.enabled = true;
						line.SetPosition( 0, ballLaunchPosition );
						line.SetPosition( 1, ballLaunchPosition + direction * 4 );
						line.startColor = Color.white;
						line.endColor = Color.white;
					}
					else
						line.enabled = false;
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
			BallNotHittingBox();
			if ( numberOfBallsCollected == 0 )
			{
				ballLaunchPosition = collision.transform.position;
				ballLaunchPosition.y = -3.15f;
				ballLaunchPosition.z = -1f;
			}

			numberOfBallsCollected++;
			Destroy( collision.gameObject );
		}
		else if ( collision.gameObject.tag == "Pickup" )
			Destroy( collision.gameObject );

		if ( numberOfBallsCollected == totalBalls )
		{
			// end current round and start new round
			EndRound();
			if(!bIsGameOver)
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
		StopCoroutine( bumperSpawner );
		bIsRoundActive = false;
		numberOfBallsCollected = 0;
		roundNumber++;

		Color newColor = Random.ColorHSV( 0, 1, 0.5f, 1, 1, 1, 1, 1 ); ;
		topBorder.GetComponent<SpriteRenderer>().color = newColor;
		bottomBorder.GetComponent<SpriteRenderer>().color = newColor;
		topBorder.GetComponentInChildren<TextMesh>().text = roundNumber.ToString();

		Block[] survivingBlocks = FindObjectsOfType<Block>();
		foreach ( Block b in survivingBlocks )
		{
			if ( b.transform.position.y == -2.45f )
			{
				EndGame();
				bIsGameOver = true;
				return;
			}
		}

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
				else if ( rand < .35f )
					Instantiate( blocks[ 1 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if ( rand < .4f )
					Instantiate( blocks[ 2 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if ( rand < .45f )
					Instantiate( blocks[ 3 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if ( rand < .5f )
					Instantiate( blocks[ 4 ], new Vector3( rowPositions[ i ], topRowY, -1 ), transform.rotation );
				else if ( rand < .52f )
					Instantiate( bumper, new Vector3(rowPositions[ i ], topRowY, -1 ), transform.rotation );
			}
		}
	}

	public void EndGame()
	{
		foreach ( Block b in FindObjectsOfType<Block>() )
			Destroy( b.gameObject );
		foreach ( Pickup p in FindObjectsOfType<Pickup>() )
			Destroy( p.gameObject );

		menuButtons.SetActive( true );
	}

	void LaunchBalls( Vector3 dir )
	{
		bIsRoundActive = true;
		bumperSpawner = StartCoroutine( SpawnBumper() );
		StartCoroutine( SpawnBall( dir ) );
	}

	IEnumerator SpawnBall( Vector3 dir )
	{
		int i = 0;
		int ballCount = numberOfBalls;
		Vector3 startPosition = ballLaunchPosition;
		while ( i < ballCount )
		{
			Instantiate( ball, startPosition, Quaternion.Euler( dir ) );
			i++;
			yield return new WaitForSeconds( .08f );
		}
	}

	public void BallNotHittingBox()
	{
		StopCoroutine( bumperSpawner );
		bumperSpawner = StartCoroutine( SpawnBumper() );
	}

	IEnumerator SpawnBumper()
	{
		yield return new WaitForSeconds( 5 );
		Vector3 spawnPoint = FindObjectOfType<Ball>().transform.position;
		spawnPoint.x = 0f;
		spawnPoint.y -= .2f;
		Instantiate( bumper, spawnPoint, Quaternion.Euler( Vector3.zero ) );
		BallNotHittingBox();
	}

	public void OnPlayButtonClicked()
	{
		SceneManager.LoadScene( "GameScene" );
	}

	public void OnExitButtonClicked()
	{
		Application.Quit();
	}
}
