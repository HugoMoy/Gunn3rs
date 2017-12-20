using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	float bulletStrength1 = 1;
	float bulletSpeed1 = 1;
	float bulletRange1 = 1;
	float energyCost1 = 0;
	float bulletStrength2 = 1;
	float bulletSpeed2 = 1;
	float bulletRange2 = 1;
	float energyCost2 = 0;

	public GameObject[] characters;
	public GameObject p1Show;
	public GameObject p2Show;

	public string p1name = "";
	public string p2name = "";
	public int nbPlayers = 1;

	int it1 = 1;
	int it2 = 0;
	int nbCharacters = 2;


	public static ButtonManager UI = null;
	public GameObject options;
	public GameObject menu;
	public GameObject background;

	//public InputField Inputname;
	public Text energy1Text;
	public Text energy2Text;
	public Text name1Text;
	public Text name2Text;
	public InputField nameField1;
	public InputField nameField2;
	public Image[] blocksStrength1;
	public Image[] blocksSpeed1;
	public Image[] blocksRange1;
	public Image[] blocksStrength2;
	public Image[] blocksSpeed2;
	public Image[] blocksRange2;
	public Sprite fullCristal;
	public Sprite emptyCristal;

	//
///

void updateBlock(Image[] block, float level) {
	for(int i =0; i < 5; i++){
		if(i < level) {
			block[i].sprite = fullCristal;
		} else {
			block[i].sprite = emptyCristal;
		}
	}
}
public void Awake(){
	// Singleton
	if(UI == null){
			UI = this;
		}
		else if (UI != this){
			Destroy(gameObject);
		}
	options.SetActive(false);
	menu.SetActive(true);
	DontDestroyOnLoad(gameObject);
}

public void SetName(int player) {
	if(player == 1) {
		Memory.instance.namep1 = nameField1.text;
		name1Text.text = "Player 1 - " + nameField1.text;
	} else {
		Memory.instance.namep2 = nameField2.text;
		name2Text.text = "Player 2 - " + nameField2.text;
	}
}

public void nextCharacter(int player) {
	if(player == 1 ) {
		Debug.Log("Change player 1 it = " +it1);
		Destroy(p1Show);
		p1Show =  Instantiate(characters[((it1++)%nbCharacters)], p1Show.transform.position, Quaternion.identity);
		if(it1 == 0) {
			Memory.instance.p1Type = "fish";
		} else {
			Memory.instance.p1Type = "rogue";
		}
	} else {
		Debug.Log("Change player 2 it = " +it2);
		Destroy(p2Show);
		p2Show =  Instantiate(characters[((it2++)%nbCharacters)], p2Show.transform.position, Quaternion.identity);
		if(it2 == 0) {
			Memory.instance.p2Type = "fish";
		} else {
			Memory.instance.p2Type = "rogue";
		}
	}
}

private bool isEnergyMax(int newEnergy, int player) {
	if(player == 1){
		if(100 < (energyCost1 + newEnergy) ){
			return false;
		}
	} else {
		if(100 < (energyCost2 + newEnergy) ){
			return false;
		}
	}
	
	return true;
}

private void updateEnergyCost() {
	energyCost1 = 20*bulletSpeed1 + 15*bulletStrength1 + 15*bulletRange1;
	energy1Text.text = "Energy Cost : " + energyCost1 + " / 100";
	energyCost2 = 20*bulletSpeed2 + 15*bulletStrength2 + 15*bulletRange2;
	energy2Text.text = "Energy Cost : " + energyCost2 + " / 100";
}

void display() {
	Debug.Log("str1 " + bulletStrength1 +"  / str2 "+ bulletStrength2  + " / spd1 "+ bulletSpeed1  + " / spd2 " +bulletSpeed2 + " / rng1" +bulletRange1+ " / rng2"+ bulletRange2);
}
public void AddSpeed(int player){
		if(isEnergyMax(10, player)) {
			if(player ==1 && bulletSpeed1 <=5){
				bulletSpeed1 += 1;
				updateBlock(blocksSpeed1, bulletSpeed1);
			} else if(player == 2 && bulletSpeed2 <=5) {
				bulletSpeed2 += 1;
				updateBlock(blocksSpeed2, bulletSpeed2);
			}
		}
	updateEnergyCost();
	display();
	}
public void AddStrength(int player){
		if(isEnergyMax(5, player)) {
			if(player ==1 && bulletStrength1 <=5){
				bulletStrength1 += 1;
				updateBlock(blocksStrength1, bulletStrength1);
			} else if(player == 2 && bulletStrength2 <=5) {
				bulletStrength2 += 1;
				updateBlock(blocksStrength2, bulletStrength2);
			}
		}
	updateEnergyCost();
	display();
	}
public void AddRange(int player){
		if(isEnergyMax(10, player)) {
			if(player ==1 && bulletRange1 <= 5){
				bulletRange1 += 1;
				updateBlock(blocksRange1, bulletRange1);
			} else if(player == 2 && bulletRange2 <=5) {
				bulletRange2 += 1;
				updateBlock(blocksRange2, bulletRange2);
			}
		}
	updateEnergyCost();
	display();
}
public void RedSpeed( int player){
		Debug.Log("Red speed");
		if(player == 1 && bulletSpeed1 > 1) {
			bulletSpeed1 -= 1;
			energyCost1 -= 10;
			updateBlock(blocksSpeed1, bulletSpeed1);
		} else if(player == 2 && bulletSpeed2 > 1) {
			bulletSpeed2 -= 1;
			energyCost2 -= 10;
			updateBlock(blocksSpeed2, bulletSpeed2);
		}
	updateEnergyCost();
	display();
}
public void RedStrength( int player) {
	Debug.Log("Red strength");
	if(player == 1 && bulletStrength1 > 1) {
		bulletStrength1 -= 1;
		energyCost1 -= 5;
		updateBlock(blocksStrength1, bulletStrength1);
	}else if(player == 2 && bulletStrength2 > 1) {
		bulletStrength2 -= 1;
		energyCost2 -= 5;
		updateBlock(blocksStrength2, bulletStrength2);
	}
	updateEnergyCost();
	display();
}
public void RedRange( int player){
	Debug.Log("Red range");
	if(player == 1 && bulletRange1 > 1) {
		bulletRange1 -= 1;
		energyCost1 -= 10;
		updateBlock(blocksRange1, bulletRange1);
	}else if(player == 2 && bulletRange2 > 1) {
		bulletRange2 -= 1;
		energyCost2 -= 10;
		updateBlock(blocksRange2, bulletRange2);
	}
	updateEnergyCost();
	display();
}

public void OptionsButton() {
	updateEnergyCost();
	options.SetActive(true);
	background.SetActive(false);
	menu.SetActive(false);
}

public void BackToMenu() {
	options.SetActive(false);
	background.SetActive(true);
	menu.SetActive(true);
	Debug.Log("Back to menu !!");
}

public void StartButton() {
	Debug.Log("New game !!! str p1 " + bulletStrength1);
	Memory.instance.p1Energy = energyCost1;
	Memory.instance.p1Range = bulletRange1;
	Memory.instance.p1Strength = bulletStrength1;
	Memory.instance.p1Speed = bulletSpeed1;
	Memory.instance.p2Energy = energyCost2;
	Memory.instance.p2Range = bulletRange2;
	Memory.instance.p2Strength = bulletStrength2;
	Memory.instance.p2Speed = bulletSpeed2;
	SceneManager.LoadScene("GameScene");
}
public void ExitButton(){
	Application.Quit();
}
}
