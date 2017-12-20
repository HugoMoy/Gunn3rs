using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pawn : MonoBehaviour {

	//public GameObject playerPos ;
	private float lastmove;
	private float delay = 0.5f;
	private float hp = 100;
	float bulletSpeed = 1f;
	float bulletStrength;
	float bulletRange;
	int num = 2;
	bool bounce = false;
	float ReloadDelay = 0.4f;
	float scale = 10;
	float lastShotDate;
	public GameObject bullet;

	float totalEnergy;

	void Awake() {
		//playerPos =  GameManager.gm.player1;
	}
	// Use this for initialization
	void Start () {
	}
	public void LoseHp(float dmg){
		hp -= dmg;
		GameManager.gm.updateHp(hp, num);
		isLostRound();
		Debug.Log("Aie aie aie " + hp);
		//GameObject.Find("LifeImage").GetComponent<Life>().changeHp(hp);
	}

	public void isLostRound(){
		if(hp <= 0){
			GameManager.gm.roundWon(1);
		}
	}

	public void shot()
 	{
		if((Time.time - lastShotDate > ReloadDelay)) {
			if(GameManager.gm.canShoot(num)){	
				Vector3 posOffset = new Vector3(transform.position.x,transform.position.y);
				Vector3 targetPosition = GameManager.gm.p1.transform.position;
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
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Memory.instance.doingSetup) return;
		if(Time.time-lastmove > delay){
			lastmove = Time.time;
			int a = (int)Random.Range(0,5);
			if(a==0) {
				Vector2 nextpos = new Vector2(this.transform.position.x , this.transform.position.y + 2/scale);
				this.transform.position = nextpos;
			}
			else if (a == 1) {
				Vector2 nextpos = new Vector2(this.transform.position.x , this.transform.position.y - 2/scale);
				this.transform.position = nextpos;
			}
			else if (a == 2) {
				Vector2 nextpos = new Vector2(this.transform.position.x - 2/scale, this.transform.position.y );
				this.transform.position = nextpos;
			}
			else if (a == 3) {
				Vector2 nextpos = new Vector2(this.transform.position.x + 2/scale, this.transform.position.y );
				this.transform.position = nextpos;
			}
			
		}
		shot();
	}
	public void initialize(float _range, float _speed, float _strength) {
		bulletRange = _range;
		bulletSpeed = _speed;
		bulletStrength = _strength;
		Debug.Log("str "+ bulletStrength + " spd "+ bulletSpeed + " range "+ bulletRange);
	}

}
