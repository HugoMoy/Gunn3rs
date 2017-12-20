using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

	// Use this for initialization
	public Text winner;
	public Image winnerImage;
	public Sprite fish;
	public Sprite rogue;
 	void Awake() {
		 Debug.Log(Memory.instance.winnerName);
		 if(Memory.instance.winnerType == "rogue") {
			 winnerImage.sprite = rogue;
		 }
		 else {
			 winnerImage.sprite = fish;
		 }
		winner.text = Memory.instance.winnerName + " as "+ Memory.instance.winnerType 
						+ " finally destroyed " + Memory.instance.loserName
						+ "\r\n he win " + Random.Range(70,150) + " points in the ladder";
						
		
		

	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void backToMenu() {
		SceneManager.LoadScene("StartScene");
	}
}
