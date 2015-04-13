using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartGame()
	{
		StartCoroutine("GoToGame");
	}
	
	public void QuitGame()
	{
		Application.Quit ();
	}
	
	IEnumerator GoToGame()
	{
		Camera.main.GetComponentInChildren<ScreenFader>().ClearToBlack();
		yield return new WaitForSeconds(2);
		Application.LoadLevel(1);
	}
}
