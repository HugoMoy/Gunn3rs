using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gm = null;
	public Vector3 p1pos;
	public float strp1;
	public float spdp1;
	public float rngp1;
	public float strp2;
	public float spdp2;
	public float rngp2;
	public float enep1;
	public float enep2;
	public float entotp1;
	public float entotp2;
	private float enMax = 300;
	
	public string namep1;
	public string namep2;
	private float scorep1;
	private float scorep2;

	public Text p1Text;
	public Text p2Text;
	public Text p1Name;
	public Text p2Name;
	public Text p1NameFollow;
	public Text p2NameFollow;
	public GameObject p1;
	public GameObject p2;
	public GameObject player1;
	public GameObject player2;
	public Transform[] pos;
	public float lastUpDate =0;
	public float updelay = 0.02f;
	public Camera mainC;

	public Sprite rdLost;
	public Sprite rdWon;
	public Sprite rdNotPlayed;
	public Image[] roundsP1;
	public Image[] roundsP2;
	public Scrollbar p1Life;
	public Scrollbar p2Life;
	float levelStartDelay = 1f;
	public GameObject levelImage;
	public Text levelImageText;

	// Use this for initialization
	void Start () {
		
	}
	public void roundWon(int player) {
		if(player == 1) {
			scorep1++;
			Memory.instance.scorep1++;
			Memory.instance.rounds[(int) (scorep1+ scorep2-1)] = 1;
		} else {
			scorep2++;
			Memory.instance.scorep2++;
			Memory.instance.rounds[(int) (scorep1+ scorep2-1)] = 2;
		}
		if(scorep1 >= 3) {
			Memory.instance.winnerName = namep1;
			Memory.instance.winnerType = Memory.instance.p1Type;
			Memory.instance.loserName = namep2;
			gameOver();
		}
		else if(scorep2 >= 3) {
			Memory.instance.winnerName = namep2;
			Memory.instance.winnerType = Memory.instance.p2Type;
			Memory.instance.loserName = namep1;
			gameOver();
		} else {
			Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + scorep1 + "  --  "+scorep2);
			SceneManager.LoadScene("GameScene");
		}
	}
	public void gameOver() {
		SceneManager.LoadScene("EndScene");
	}

	void Awake(){
		if(gm == null){
			gm = this;
		}
		else if (gm != this){
			Destroy(gameObject);
		}
		//DontDestroyOnLoad(gameObject);
		levelImage.SetActive(true);
		//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
		Memory.instance.doingSetup = true;
		levelImageText.text = "Round " + (Memory.instance.scorep1+Memory.instance.scorep2 +1) + " !!!";
		Invoke("HideLevelImage", levelStartDelay);
		initialize();
		placePlayers(); 
    }
 
		void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);
		Memory.instance.doingSetup = false;
		//Set doingSetup to false allowing player to move again.
	}

	void FixedUpdate() {
		//Debug.Log("Update UI! " + enep1);
		textFollowing();
		if(Time.time - lastUpDate > updelay){
			upEnergy();
			updateUI();
			lastUpDate = Time.time;
		}
	}
	public void updateHp(float hp, int player) {
		if(player == 1) {
			p1Life.size = hp/100;
		} else {
			p2Life.size = hp/100;
		}
	}
	void updateUI() {
		p1Text.text = "Strength : "+ strp1 + "\r\n" + 
						"Speed : "+ spdp1 + "\r\n" + 
						"Range : "+ rngp1 + "\r\n" + 
						"Energy : "+ entotp1;
		p2Text.text = "Strength : "+ strp2 + "\r\n" + 
						"Speed : "+ spdp2 + "\r\n" + 
						"Range : "+ rngp2 + "\r\n" + 
						"Energy : "+ entotp2;
		p1Name.text = Memory.instance.namep1;
		p2Name.text = Memory.instance.namep2;
	}
	void upEnergy() {
		if(entotp1 < enMax) {
			entotp1++;
		}
		if(entotp2 < enMax) {
			entotp2++;
		}
	}
	public bool canShoot(int num) {
		if(num==1){
			if (entotp1 - enep1 < 0) {
				return false;
			}
			entotp1 -= enep1;
			return true;
		} else {
			if (entotp2 - enep2 < 0) {
				return false;
			}
			entotp2 -= enep2;
			return true;
		}
	}
	void initialize() {
		strp1 = Memory.instance.p1Strength;
		strp2 = Memory.instance.p2Strength;
		spdp1 = Memory.instance.p1Speed;
		spdp2 = Memory.instance.p2Speed;
		rngp1 = Memory.instance.p1Range;
		rngp2 = Memory.instance.p2Range;
		enep1 = Memory.instance.p1Energy;
		enep2 = Memory.instance.p2Energy;
		scorep1 = Memory.instance.scorep1;
		scorep2 = Memory.instance.scorep2;
		entotp1 = enMax;
		entotp2 = enMax;
		p1Life.size = 1;
		p2Life.size = 1;
		p1NameFollow.text = Memory.instance.namep1;
		p2NameFollow.text = Memory.instance.namep2;
		for(int i =0; i < 5; i++){
			if(Memory.instance.rounds[i] == 1){
				roundsP1[i].sprite = rdWon;
				roundsP2[i].sprite = rdLost;
			} else if(Memory.instance.rounds[i] == 2){
				roundsP2[i].sprite = rdWon;
				roundsP1[i].sprite = rdLost;
			} else {
				roundsP2[i].sprite = rdNotPlayed;
				roundsP1[i].sprite = rdNotPlayed;

			}
		}
		p1Text.text = "Strength : "+ strp1 + "\r\n" + 
						"Speed : "+ spdp1 + "\r\n" + 
						"Range : "+ rngp1 + "\r\n" + 
						"Energy : "+ enMax;
		p2Text.text = "Strength : "+ strp2 + "\r\n" + 
						"Speed : "+ spdp2 + "\r\n" + 
						"Range : "+ rngp2 + "\r\n" + 
						"Energy : "+ enMax;
	}
	void textFollowing() {
		Vector2 pos = p1.transform.position;
		Vector2 npos = mainC.WorldToScreenPoint(pos);
		npos.y += 70f;
		npos.x += 60f;
		p1NameFollow.transform.position = npos;

		pos = p2.transform.position;
		npos = mainC.WorldToScreenPoint(pos);
		npos.y += 70f;
		npos.x += 60f;
		p2NameFollow.transform.position = npos;
	}

	void placePlayers() {
		GameObject player1Instance = Instantiate(player1, pos[0].position, Quaternion.identity) as GameObject;
		player1Instance.GetComponent<Player>().initialize(strp1, spdp1, rngp1);
		p1 = player1Instance;
		//p1pos.position = pos[0].position;
		// Case here where we have a player and a spawn
		GameObject player2Instance = Instantiate(player2, pos[1].position, Quaternion.identity) as GameObject;
		player2Instance.GetComponent<Pawn>().initialize(strp2, spdp2, rngp2);
		p2 = player2Instance;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
