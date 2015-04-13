using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	Animator myAnim;
	bool covered = false;
	
	// CLEAR IS 0
	// BLACK IS 1
	// WHITE IS 2

	void Awake()
	{
		myAnim = GetComponent<Animator>();
		StartCoroutine("TurnOff");
	}
	
	public void ClearToBlack()
	{
		GetComponent<Image>().enabled = true;
		myAnim.SetInteger("From", 0);
		myAnim.SetInteger("To", 1);
		myAnim.SetBool ("Start", true);
		
		covered = true;
		
		StartCoroutine("TurnOff");
	}
	
	public void BlackToClear()
	{
		GetComponent<Image>().enabled = true;
		myAnim.SetInteger("From", 1);
		myAnim.SetInteger("To", 0);
		myAnim.SetBool ("Start", true);
		
		StartCoroutine("TurnOff");
	}
	
	IEnumerator TurnOff()
	{
		yield return new WaitForSeconds(0.5f);
		myAnim.SetBool ("Start", false);
		yield return new WaitForSeconds(1.5f);
		if(!covered)
		{
			GetComponent<Image>().enabled = false;
		}
	}
}
