using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

	//event handler for breaking all machines
	public delegate void BreakAllMachines();
	public static event BreakAllMachines myBreakAll;
	
	//event handler for fixing all machines
	public delegate void FixAllMachines();
	public static event FixAllMachines myFixAll;

	//variable for how many complaints the player can take
	public static int PlayerHealth = 3;
	
	//variable for if the player should have infinite resources
	public static bool InfiniteResources = false;

	int currentWave = 1; //variable for tracking what wave it is NOW
	int enemiesToSpawn; //enemies total of the wave
	int enemiesNeedSpawn; //enemies still needing to spawn
	public static int enemiesKilled; //enemies of wave killed
	bool waveComplete = false; //is the wave complete
	public static bool canStart; //can the next wave start
	bool readyToSpawn = true; //is it ready to spawn
	bool endWaveRunning = false; //end the wave running currently
	
	//bool for if the game is running
	public static bool running;
	
	public GameObject enemy; //variable for enemy

	public int[] EnemiesEachWave; //array for setting how many enemies to spawn each wave
	
	public Transform[] spawnLocations; //array of transforms, which are spawn locations inserted publicly
	
	public static bool FinishedGUIShow;
	
	// Update is called once per frame
	void Update () {

		//if checkdead is false, meaning the player isnt dead, continue playing the game
		if (!CheckDead ()) 
		{
			if (canStart) 
			{
				StartWave (); //if it can start, start it
			}
			if (!waveComplete && running) 
			{  //if its not complete,
				Spawn (); //continue spawning
			}
			if (CheckComplete () && running) 
			{ //checkif its complete,
				if (!endWaveRunning) 
				{ //if it is, and we arent ending the wave running,
					StartCoroutine ("EndWave"); // end the current wave
				}
			}
		}
		else 
		{
			//do whatever needs to be done for if the player is dead
			
			//show the "you died gui"
		}
		
	}
	
	//function to break all the machines
	public static void BreakMachines()
	{
		myBreakAll();
	}
	
	public static void FixMachines()
	{
		myFixAll();
	}
	
	//function to start the game
	public static void BeginTheGame()
	{
		running = true;
		canStart = true;
	}
	
	void StartWave() //start wave set
	{
		canStart = false; //doesnt need to start now
		waveComplete = false; //not complete yet
		//reset how many enemies you've killed
		enemiesKilled = 0;

		//set the amount of enemies needed to spawn during each wave, set in inspector in an array
		enemiesToSpawn = EnemiesEachWave [currentWave - 1];

		enemiesNeedSpawn = enemiesToSpawn; // set the enemies we need to spawn to the amount to spawn
	}
	
	void Spawn() //spawn loop
	{
		int waveLength = 10; //length of wave in seconds
		float spawnEvery = waveLength / enemiesToSpawn; //calculation for how often i want to spawn the enemies
		
		if (readyToSpawn && enemiesNeedSpawn != 0)  //if its ready to spawn, and you still need to spawn enemies, continue
		{
			int myRandom = Random.Range (0, 4); //pick a random
			Instantiate (enemy, spawnLocations[myRandom].transform.position, Quaternion.identity);//instantiate the enemy at a random spawnpoint
			enemiesNeedSpawn--; //lessen the enemies needed to spawn
			readyToSpawn = false; //say its not ready to spawn another yet
			StartCoroutine("WaitSpawn", spawnEvery); //start the wait for the next spawn, based on the interval of spawning
		} 
	}
	
	IEnumerator WaitSpawn(float spawnEvery) //timer for waiting between spawns
	{
		yield return new WaitForSeconds(spawnEvery);
		readyToSpawn = true; //tell its ready to spawn
	}
	
	bool CheckComplete() //check if the wave is complete
	{
		//check if enemies youve killed is the same as the amount spawned
		if (enemiesKilled == enemiesToSpawn)  //if enemies youve killed is equal to the max, the wave is done
		{
			waveComplete = true;
			return true;
		} 
		else 
		{
			return false; //otherwise, its not.
		}
	}
	
	void WhenPlayerFinishes()
	{
		print ("YOU WON!"); //do final win
		//show the end gui
		FinishedGUIShow = true;
	}	
	
	public void GoToMainMenu()
	{
		Application.LoadLevel(0);
	}
	
	IEnumerator EndWave() //end the wave
	{
		endWaveRunning = true; //end wave is running
		if (currentWave == EnemiesEachWave.Length)  //if the wave is 6,
		{
			WhenPlayerFinishes();
		} 
		else 
		{
			print ("Wave " + currentWave.ToString () + " Complete!"); //otherwise say which wave you won
			currentWave++; //add to current wave
			yield return new WaitForSeconds (3); //wait a few seconds
			canStart = true; //ready to start
		}
		endWaveRunning = false; //end wave running no longer
	}

	//function to check every frame if enough people have complained to the manager
	bool CheckDead()
	{
		//if players health 
		if (PlayerHealth <= 0) 
		{
			return true;
		} 
		else 
		{
			return false;
		}
	}
}
