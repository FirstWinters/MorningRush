using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EncounterEnemy : MonoBehaviour {

	//variable for the player that the enemy will #stalk
	public GameObject player;
	//variable for the second encounter enemy
	public EncounterEnemy2 EncounterEnemySecond;
	
	//bools to pause the player
	public bool paused;
	public bool done;
	
	public ParticleSystem winBurst;
	public ParticleSystem loseBurst;
	
	//speed
	int speed = 40;
	
	//variable for actual part of the enemy that we will rotate, as opposed to the text
	public GameObject EnemyText;

	// Use this for initialization
	void Start () {
	
		//turn on infinite resources
		WaveManager.InfiniteResources = true;
	
		//find the player
		player = GameObject.FindGameObjectWithTag("Player");
		
		//face the player
		transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
		//set the rotation of the text so it's not getting all janky with the box
		EnemyText.transform.rotation = Quaternion.Euler (0, 0, 0);
		
		//call teh function to set duh text of duh enemay
		InitialText ();
	
	}
	
	// Update is called once per frame
	void Update () {

		//if they arent paused
		if(!paused)
		{
			//i like to move it move it
			MoveToPlayer ();
		}
		
		//if they hit y position 2,
		if(transform.position.y < -2.8f && !done)
		{
			//turn on the pause
			paused = true;
			done = true;
		}
	
	}
	
	//function to move the enemy towards the player
	void MoveToPlayer()
	{
			//#duh
			transform.Translate (Vector3.up/speed);
	}
	
	//function to set the text above the enemy so player knows what the enemy wantz
	void InitialText()
	{
		//variable for setting the string
		string bbluvdoll = "I WANT COFFEE. ONE PART ESPRESSO, ONE PART MILK.";
		//Set the text over the enemy to their drink desires
		EnemyText.GetComponentInChildren<Text> ().text = bbluvdoll;
		
	}
	
	//function to set the text above the enemy so player knows what the enemy wantz
	void EncouragementText()
	{
		//variable for setting the string
		string bbluvdoll = "THIS ISN'T WHAT I ASKED FOR. ONE ESPRESSO. ONE MILK.";
		//Set the text over the enemy to their drink desires
		EnemyText.GetComponentInChildren<Text> ().text = bbluvdoll;
		
	}
	
	//function for when the enemy has been satisfied with their order
	public void LeaveTheStore()
	{
		//unpause him
		paused = false;
		
		//faster
		speed = 20;
		
		//begin the coroutine for destroying this object
		StartCoroutine ("BoomDestroy");
		//disable his trigger and rigidbody so nothing hits the enemy
		GetComponent<BoxCollider2D> ().enabled = false;

		//Change the text of the enemy to done
		EnemyText.GetComponentInChildren<Text> ().text = "Thanks a ton!";
		
		//exit stage right

		//Reset the rotation of the text again because it'll be flipped
		EnemyText.transform.localRotation = Quaternion.Euler (0, 0, 90);
		//rotate towards right
		transform.rotation = Quaternion.Euler(0,0,-90);
		
	}
	
	//function to check if the drink coming into the enemy matches their desires
	public void CheckDrink(int e, int m, int s, int v)
	{
		//if all the ingredients of the drink coming in match those of the enemy,
		if (e == 1 && m == 1 && s == 0 && v == 0) 
		{
			//they're good and they can leave the store, with happy as true
			LeaveTheStore();
			winBurst.Emit (20);
		}
		//otherwise
		else 
		{
			//the enemy is not satisfied, do whatever unsatisfied enemies do
			EncouragementText();
			loseBurst.Emit (20);
		}
	}
	
	//boom boom boom boom boom
	IEnumerator BoomDestroy()
	{	
		
		//wait a few seconds
		yield return new WaitForSeconds(10f);
		
		//turn off infinite resources
		WaveManager.InfiniteResources = false;
		
		//break the machines
		WaveManager.BreakMachines();
		
		//start the second encounter
		EncounterEnemySecond.Unpause();
		
		Destroy (this.gameObject);
	}
}
