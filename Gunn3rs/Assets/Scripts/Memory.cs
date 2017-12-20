using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour {
	public static Memory instance = null;
	public string winner;
	public string winnerName;
	public string winnerType;
	public string loserName;
	public string p1Type;
	public string p2Type;
	public float p1Strength;
	public float p1Speed;
	public float p1Range;
	public float p1Energy;
	public float p2Strength;
	public float p2Speed;
	public float p2Range;
	public float p2Energy;
	public bool isP2IA;
	public string namep1;
	public string namep2;
	public int scorep1;
	public int scorep2;
	public bool doingSetup;
	public int[] rounds = {0,0,0,0,0};

	void Awake() {
		if(instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
