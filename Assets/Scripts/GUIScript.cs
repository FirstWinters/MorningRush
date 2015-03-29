using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScript : MonoBehaviour {

	//references to the text components and gui
	public Text ComplaintsText;
	public Text EspressoQueued;
	public Text MilkQueued;
	public Text SugarQueued;
	public Text VanillaQueued;
	
	//references to the bars of the equipment
	public Slider EspressoSlider;
	public Slider MilkSlider;
	public Slider SugarSlider;
	public Slider VanillaSlider;
	
	//refs for the texts
	public Text EspressoCurrentText;
	public Text MilkCurrentText;
	public Text SugarCurrentText;
	public Text VanillaCurrentText;
	
	//references to the empty texts
	public GameObject EspressoFix;
	public GameObject MilkFix;
	public GameObject SugarFix;
	public GameObject VanillaFix;
	
	//panel for the finished gui
	public GameObject FinishedGUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//update gui
		UpdateComplaints();
		UpdateQueue();
		UpdateSliders();
		
		//checking if you need to display the empty clicks
		CheckEmpties();
		
		if(WaveManager.FinishedGUIShow)
		{
			WaveManager.FinishedGUIShow = false;
			ShowFinished();
		}
	
	}
	
	void ShowFinished()
	{
		FinishedGUI.SetActive(true);
	}
	
	//function to check
	void CheckEmpties()
	{
		//check espresso if it needs to say HEY
		if(EspressoMachine.CurrentMaintenance <=5)
		{
			EspressoFix.SetActive(true);
		}
		//else turn it off
		else
		{
			EspressoFix.SetActive(false);
		}
		
		//check milk if it needs to say HEY
		if(MilkStock.CurrentFreshMilk <=5)
		{
			MilkFix.SetActive(true);
		}
		//else turn it off
		else
		{
			MilkFix.SetActive(false);
		}
		
		//check sugar if it needs to say HEY
		if(SugarBag.CurrentSugar <=50)
		{
			SugarFix.SetActive(true);
		}
		//else turn it off
		else
		{
			SugarFix.SetActive(false);
		}
		
		//check vanilla if it needs to say HEY STOP AND CALM DOWN
		if(EspressoMachine.CurrentMaintenance <=5)
		{
			VanillaFix.SetActive(true);
		}
		//else turn it off
		else
		{
			VanillaFix.SetActive(false);
		}
		
	}
	
	//update the sliders 
	void UpdateSliders()
	{
		//set the value to the current and change the texts to the current
		EspressoSlider.value = EspressoMachine.CurrentMaintenance;
		EspressoCurrentText.text = EspressoMachine.CurrentMaintenance.ToString();
		
		MilkSlider.value = MilkStock.CurrentFreshMilk;
		MilkCurrentText.text = MilkStock.CurrentFreshMilk.ToString();
		
		SugarSlider.value = SugarBag.CurrentSugar;
		SugarCurrentText.text = SugarBag.CurrentSugar.ToString();
		
		VanillaSlider.value = VanillaPump.CurrentVanilla;
		VanillaCurrentText.text = VanillaPump.CurrentVanilla.ToString();
	}
	
	void UpdateQueue()
	{
		//set the texts of the resource queues
		EspressoQueued.text = PlayerScript.PEspresso.ToString ();
		MilkQueued.text = PlayerScript.PMilk.ToString ();
		SugarQueued.text = PlayerScript.PSugar.ToString ();
		VanillaQueued.text = PlayerScript.PVanilla.ToString ();
	}
	
	//function for updating the player health component of GUI
	void UpdateComplaints()
	{
		//set the text to a fraction
		ComplaintsText.text = WaveManager.PlayerHealth + "/3";
	}
}
