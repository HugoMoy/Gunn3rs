using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// Use this for initialization
	float hp = 100;
	int num = 1;
	float bulletSpeed = 3f;
	float bulletStrength = 10f;
	bool bounce = false;
	float bulletRange;
	float ReloadDelay;
	float scale = 10;
	float lastShotDate;

	public GameObject bullet;

	float totalEnergy;
	Rigidbody2D playerRB;
	void Start () {
		playerRB = GetComponent<Rigidbody2D>();
	}

	public void initialize(float _range, float _speed, float _strength) {
		bulletRange = _range;
		bulletSpeed = _speed;
		bulletStrength = _strength;
		Debug.Log("str "+ bulletStrength + " spd "+ bulletSpeed + " range "+ bulletRange);
	}
	// Update is called once per frame

	public void LoseHp(float dmg){
		hp -= dmg;
		GameManager.gm.updateHp(hp, num);
		Vector2 vel = new Vector2(0,0);
		playerRB.velocity = vel;
		isLostRound();
		Debug.Log("Aie aie aie " + hp);
		//GameObject.Find("LifeImage").GetComponent<Life>().changeHp(hp);
	}

	public void isLostRound(){
		if(hp <= 0){
			GameManager.gm.roundWon(2);
		}
	}
	public void shot()
 	{
		if((Time.time - lastShotDate > ReloadDelay)) {
			if(GameManager.gm.canShoot(num)){	
				Vector3 posOffset = new Vector3(transform.position.x,transform.position.y);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 targetPosition = ray.origin;
				Vector3 shotDirection = targetPosition - posOffset;
				Vector3 shotPosition = new Vector3(posOffset.x + shotDirection.normalized.x, posOffset.y + shotDirection.normalized.y, 0f);
				GameObject bulletInstance;
				float dist = Vector3.Distance(posOffset, shotPosition);
				shotPosition = new Vector3(posOffset.x + shotDirection.normalized.x*(0.2f/dist), posOffset.y + shotDirection.normalized.y*(0.2f/dist), 0f);
				Debug.Log("Distance : " + Vector3.Distance(posOffset, shotPosition));
				bulletInstance = Instantiate(bullet, shotPosition, Quaternion.identity) as GameObject;
				// set the speed and strength of the bullet 
				bulletInstance.GetComponent<Bullet>().setLayer(num);
				bulletInstance.GetComponent<Bullet>().setParameters(bulletStrength*10, bulletRange, true, shotPosition, num);
				Rigidbody2D rbshot = bulletInstance.GetComponent<Rigidbody2D>();
				rbshot.velocity = new Vector2(shotDirection.normalized.x*(bulletSpeed/dist) * 5, shotDirection.normalized.y*(bulletSpeed/dist) * 5);
				Debug.Log("Velocity " + rbshot.velocity);
				bulletInstance.transform.localRotation = Quaternion.identity;
				Debug.Log("Shoot !!!");
				lastShotDate = Time.time;
			}
		}
 	}
	void FixedUpdate () {
		if(Memory.instance.doingSetup) return;
		if(Input.GetKeyDown(KeyCode.Space)){
			shot();
			Debug.Log(transform.position);
		}


		Vector2 nextmove = new Vector2(0,0);
		if(Input.GetKey(KeyCode.S)){
			nextmove.y--;
		}
		else if(Input.GetKey(KeyCode.Z)){
			nextmove.y++;
		}
		if(Input.GetKey(KeyCode.Q)){
			nextmove.x--;
		}
		if(Input.GetKey(KeyCode.D)){
			nextmove.x++;
		}
		Vector2 nextP = new Vector2(this.transform.position.x + nextmove.x/scale, this.transform.position.y + nextmove.y/scale);
		this.transform.position = nextP;
	}
}
