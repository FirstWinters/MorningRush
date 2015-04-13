using UnityEngine;
using System.Collections;

public class MilkStock : MonoBehaviour {

	//variabel for the max amount of milk a player has in stock
	public int MaxFreshMilk = 20;
	//variable for the current amount of milk a player can use
	public static int CurrentFreshMilk;
	
	//variable for allowing the milk to decay afer x seconds
	private bool waitSpoil;
	
	GUIScript myGUI;
	
	// Use this for initialization
	void Start () {
		myGUI = GameObject.FindGameObjectWithTag("WaveMan").GetComponent<GUIScript>();
	
		//subscribe to the breakall
		WaveManager.myBreakAll += BreakMachine;
		WaveManager.myFixAll += FixMachine;

		SetCurrentMilkStock(); //call the function to set the current milk
	}
	
	//function to break the machine
	void BreakMachine()
	{
		CurrentFreshMilk = 0;
	}
	
	void FixMachine()
	{
		CurrentFreshMilk = MaxFreshMilk;
	}
	
	//function to set the amount of current milk based off the max milk
	void SetCurrentMilkStock()
	{
		//set the current milk to the max, "filling it"
		CurrentFreshMilk = MaxFreshMilk;
	}
	
	// Update is called once per frame
	void Update () {
		
		//if you dont need to wait,
		if(!waitSpoil && EncounterEnemy2.done)
		{
			//spoil the milk
			StartCoroutine("SpoilMilk");
		}
	
	}
	
	//ienumerator for stocking fresh milk
	IEnumerator StockMilk()
	{
		//make sure the player is busy so they can't shoot
		myGUI.OnBusy(2);
		//after a few seconds
		yield return new WaitForSeconds(2f);
		//stock a full fridge of fresh milk
		CurrentFreshMilk = MaxFreshMilk;
	}
	
	//function to run the queueing of milk into the current drink
	void TryQueueMilk()
	{
		//if you have more than no milk
		if(CurrentFreshMilk > 0)
		{
			//if you dont have infinite
			if(!WaveManager.InfiniteResources)
			{
				//lessen the milk by one
				CurrentFreshMilk -= 1;
			}
			//add one to the queue
			PlayerScript.PMilk++;
		}
		//otherwise
		else
		{
			print ("No fresh milk in the stock");
		}
		
	}
	
	//function for milk spoiling/decaying
	IEnumerator SpoilMilk()
	{
		//flip the bool on
		waitSpoil = true;
		
		//wait a few seconds to spoil the next milk
		yield return new WaitForSeconds(4f);
		
		//if you have at least one milk,
		if(CurrentFreshMilk > 0)
		{
			//spoil the milk
			CurrentFreshMilk--;
		}
		
		//flip the bool back off
		waitSpoil = false;
		
	}
}
