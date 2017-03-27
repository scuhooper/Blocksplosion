using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject text;
	public GameObject button;

	// Use this for initialization
	void Start () {
		StartCoroutine( ColorCycle() );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlayButtonClicked()
	{
		SceneManager.LoadScene( "GameScene" );
	}

	IEnumerator ColorCycle()
	{
		while ( true )
		{
			Color newColor = Random.ColorHSV( 0, 1, 0.5f, 1, 1, 1, 1, 1 );
			text.GetComponent<TextMesh>().color = newColor;
			button.GetComponentInChildren<SpriteRenderer>().color = newColor;
			button.GetComponentInChildren<Text>().color = newColor;

			yield return new WaitForSeconds( 1f );
		}
	}
}
