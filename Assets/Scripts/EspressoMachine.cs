﻿using UnityEngine;
using System.Collections;

public class EspressoMachine : MonoBehaviour {

	//variabel for the max amount of maintenance
	public int MaxMaintenance = 20;
	//variable for the current amount maintenance
	public int CurrentMaintenance;
	
	public PlayerScript myPlayerScript;

	// Use this for initialization
	void Start () {
		myPlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>(); //set the player script
		SetCurrentMaintenance(); //call the function to set the current maintenance
	}
	//function to set the amount of current maintenance
	void SetCurrentMaintenance()
	{
		//set the current maintenance to the max, "fixing it"
		CurrentMaintenance = MaxMaintenance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//ienumerator for stocking fresh milk
	IEnumerator MaintainEspresso()
	{
		//after a few seconds
		yield return new WaitForSeconds(2f);
		//add half of maintenance to the espresso
		CurrentMaintenance += 10;
		//check that if its more than 20, set it to 20
		if(CurrentMaintenance > 20)
		{
			CurrentMaintenance = 20;
		}
	}
	
	//function to try to queue espresso into the current drink
	void TryQueueEspresso()
	{
		//if its not broken
		if(CurrentMaintenance > 0)
		{
			//lessen the maintenance by one
			CurrentMaintenance -= 1;
			//add one to the queue
			myPlayerScript.PEspresso++;
		}
		//otherwise
		else
		{
			print ("Machine is Broken");
		}
	}
}
