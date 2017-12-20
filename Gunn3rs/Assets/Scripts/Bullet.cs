using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {
	//public Image bulletImage;
	public Sprite redBullet;
	private Animator animator;
	string enemy = "Pawn";
	private float strength = 25;
	private float speed = 2;
	private float range;
	private bool bounce = false;
	private float dist =0;
	private Rigidbody2D bulletbdy;
	private Vector2 startPosition;

	// Use this for initialization
	public void setLayer(int player) {
		if(player == 1){
			enemy = "Pawn";
			animator.SetTrigger("red");
			this.GetComponent<SpriteRenderer>().sprite = redBullet;
		 	gameObject.layer = LayerMask.NameToLayer("Bullet1");
		} else {
			gameObject.layer = LayerMask.NameToLayer("Bullet2");
			enemy = "Player";
		}
	}
	void Start () {
		
	}
	void Awake () {
		animator = gameObject.GetComponent<Animator>();
		bulletbdy = gameObject.GetComponent<Rigidbody2D>();
		//animator = GetComponent<Animator>();
		Debug.Log("I'm alive !");
	}
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag != enemy && bounce){
			Debug.Log("collide with " + collision.gameObject.tag);
			dist += Vector2.Distance(startPosition, transform.position);
			startPosition = transform.position;
			//Destroy(gameObject);
		}
		else if (collision.gameObject.tag == enemy){
			if(enemy == "Player"){
				collision.gameObject.GetComponent<Player>().LoseHp(strength);
			}else {
				collision.gameObject.GetComponent<Pawn>().LoseHp(strength);
			}
			Debug.Log("collide with " + collision.gameObject.tag);
			Destroy(gameObject);
		}
		else {
			Debug.Log("collide with " + collision.gameObject.tag);
			Destroy(gameObject);
		}

	}

	public float getDamage(){
		return strength;
	}

	void OnTriggerEnter2D(Collider2D other)  {
		Debug.Log("I collided");
		if(other.tag == "Wall"){
			// if(!other.GetComponent<Box>().isBoxOpen()){
			// 	other.GetComponent<Box>().openBox();
			// 	animator.SetTrigger("Explode");
			// 	this.bulletbdy.velocity = new Vector2(0,0);
			// 	Destroy(gameObject,1);
			// }
			
		}
		else if(other.tag == enemy) {
			// Debug.Log("Hello i'ma bullet and i just touched an enemy!");
			// other.GetComponent<Enemy>().LowerHp();
			// animator.SetTrigger("Explode");
			// // StartCoroutine(WaitTillEnd());
			// this.bulletbdy.velocity = new Vector2(0,0);
			// Destroy(gameObject,1);
		}
		else {
			this.bulletbdy.velocity = new Vector2(0,0);
			animator.SetTrigger("death");
			Destroy(gameObject,1);
			Debug.Log("I inflicted " + strength + " damages ! ");
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Vector2.Distance(startPosition, this.transform.position) > (range - dist/2) ) {
			this.bulletbdy.velocity = new Vector2(0,0);
			animator.SetTrigger("death");
			Destroy(gameObject,1);
		}
	}

	public void setParameters(float _strength, float _range, bool _bounce, Vector2 stpos, int owner) {
		startPosition = stpos;
		strength = _strength;
		if(_range ==1){
			range = 10;
		} else if(_range == 2) {
			range = 20;
		} else if(_range == 3) {
			range = 40;
		} else if(_range == 4) {	
			range = 80;
		} else if(_range == 5) {
			range = 100;
		}
		bounce = _bounce;
		
	}
}
