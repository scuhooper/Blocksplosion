using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	public GameObject particles;
	int numberOfHits;

	TextMesh text;
	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<TextMesh>();
		sprite = GetComponent<SpriteRenderer>();

		ChangeColor();

		numberOfHits = FindObjectOfType<GameManager>().roundNumber;
		text.text = numberOfHits.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		numberOfHits--;
		if ( numberOfHits <= 0 )
		{
			Explosion.Explode( transform.position, particles );
			Destroy( gameObject );
		}
		else
		{
			text.text = numberOfHits.ToString();
			ChangeColor();
		}
	}

	void ChangeColor()
	{
		Color newColor = Random.ColorHSV( 0, 1, 0.5f, 1, 1, 1, 1, 1 ); ;
		sprite.color = newColor;
		text.color = newColor;
	}

	public void LowerBlock()
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
