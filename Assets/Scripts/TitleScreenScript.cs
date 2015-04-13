using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

	public AudioSource coffeeMusic;
	public AudioSource snoopMusic;
	
	public void SwitchMusic()
	{
	
		if(coffeeMusic.isPlaying)
		{
			coffeeMusic.Pause ();
			snoopMusic.Play ();
			GetComponentInChildren<ParticleSystem>().Play ();
		}
		else if(snoopMusic.isPlaying)
		{
			snoopMusic.Pause ();
			coffeeMusic.Play ();
			GetComponentInChildren<ParticleSystem>().Stop ();
		}
		
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
