using UnityEngine;
//use the unity UI
using UnityEngine.UI;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	//variable for the player that the enemy will #stalk
	public GameObject player;
	//variables for what the enemy wants
	public int Espresso = 0;
	public int Milk = 0;
	public int Sugar = 0;
	public int Vanilla = 0;
	//variable for actual part of the enemy that we will rotate, as opposed to the text
	public GameObject EnemyText;
	
	public ParticleSystem winBurst;
	public ParticleSystem loseBurst;
	
	//variable for if the enemy is already leaving
	bool leaving = false;

	//variable for if enemy is satisfied
	bool satisfied = false;

	// Use this for initialization
	void Start () {
	
		//subscribe to the event
		PlayerScript.myMassExodus += LeaveTheStore;
		//face the player
		transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
		//set the rotation of the text so it's not getting all janky with the box
		EnemyText.transform.rotation = Quaternion.Euler (0, 0, 0);

		//run the setting of random desires
		RandomDesires ();

		//call teh function to set duh text of duh enemay
		SetText ();
	}
	
	void OnDisable()
	{
		//unsubscribe to the event
		PlayerScript.myMassExodus -= LeaveTheStore;
	}

	//function to set the text above the enemy so player knows what the enemy wantz
	void SetText()
	{
		//variable for setting the string
		string bbluvdoll = "Q" + Espresso + " W" + Milk + " E" + Sugar + " R" + Vanilla;
		//Set the text over the enemy to their drink desires
		EnemyText.GetComponentInChildren<Text> ().text = bbluvdoll;

	}

	void Update()
	{
		//function to move enemy forward
		MoveToPlayer ();
	}

	//function to move the enemy towards the player
	void MoveToPlayer()
	{
		if(leaving)
		{
			//#duh
			transform.Translate (Vector3.up/20);
		}	
		else 
		{
			//#duh
			transform.Translate (Vector3.up/60);
		}
	}

	//function for when the enemy has been satisfied with their order
	public void LeaveTheStore(bool happy)
	{
		//if they're already leaving, obviously dont worry yourself
		if(!leaving)
		{
			//tell that hes leaving
			leaving = true;

			//begin the coroutine for destroying this object
			StartCoroutine ("BoomDestroy");
			//disable his trigger and rigidbody so nothing hits the enemy
			GetComponent<BoxCollider2D> ().enabled = false;
			if (happy) 
			{
				//Change the text of the enemy to "happy as a clam"
				EnemyText.GetComponentInChildren<Text> ().text = "Thank you!";
			}
			else 
			{
				//Change the text of the enemy to "not happy"
				EnemyText.GetComponentInChildren<Text> ().text = "CAN I SEE THE MANAGER";
			}
			//the customer is satisfied with their experience
			satisfied = true;
			//if this, exit stage right
			if (Random.Range (0, 2) == 1) 
			{
				//Reset the rotation of the text again because it'll be flipped
				EnemyText.transform.localRotation = Quaternion.Euler (0, 0, 90);
				//rotate towards right
				transform.rotation = Quaternion.Euler(0,0,-90);
			}
			//otherwise exit stage left
			else 
			{
				//Reset the rotation of the text again because it'll be flipped
				EnemyText.transform.localRotation = Quaternion.Euler (0, 0, -90);
				//rotate towards left
				transform.rotation = Quaternion.Euler(0,0,90);
			}
		}
	}

	//function to set the random variables for what ingredients the enemy wants and how many of each
	void RandomDesires()
	{
		//setting each of the variables based on  0,1, or 2 random
		Espresso = Random.Range (0, 3);
		Milk = Random.Range (0, 3);
		Sugar = Random.Range (0, 3);
		Vanilla = Random.Range (0, 3);
	}

	//function to check if the drink coming into the enemy matches their desires
	public void CheckDrink(int e, int m, int s, int v)
	{
		//if all the ingredients of the drink coming in match those of the enemy,
		if (Espresso == e && Milk == m && Sugar == s && Vanilla == v) 
		{
			//they're good and they can leave the store, with happy as true
			LeaveTheStore(true);
			winBurst.Emit (20);
		}
		//otherwise
		else 
		{
			//the enemy is not satisfied, do whatever unsatisfied enemies do
			loseBurst.Emit (20);
		}
	}

	//statement for recognizing trigger enters
	void OnTriggerEnter2D (Collider2D c)
	{
		//if the enemy enters the vicinity of the player/his bar
		if (c.tag == "Player")
		{
			//have them complain to the manager
			c.GetComponent<PlayerScript>().FaceConsequences();
			
			//have the enemy leave the store, with happy as false
			LeaveTheStore(false);
		}
	}

	IEnumerator BoomDestroy()
	{
		WaveManager.enemiesKilled++;
		yield return new WaitForSeconds(10f);
		Destroy (this.gameObject);
	}

}
