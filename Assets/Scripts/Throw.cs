using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Throw : MonoBehaviour{

	public float speed = 100.0f;
	private bool coll = false;
	public float startFalling = 1.0f;
	public float nailedTime = 1.0f;
	
	private Rigidbody2D rb;
	private Vector2 direction = Vector2.right;

	private float timerY;
	private bool fall = false;

	private float nailedTimer;
	
	void Start (){
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(direction * speed);
		timerY = 0.0f;
	}

	void FixedUpdate(){
		if (!coll){
			transform.Rotate(new Vector3(0, 0, 360.0f - Vector3.Angle(transform.right, rb.velocity)));
			
			timerY += Time.deltaTime;
			if (timerY >= startFalling && !fall){
				rb.constraints = RigidbodyConstraints2D.None;
				fall = true;
			}
		} else{
			nailedTimer += Time.deltaTime;
			if (nailedTimer >= nailedTime){
				rb.constraints = RigidbodyConstraints2D.None;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		coll = true;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
		rb.velocity = new Vector2(0.0f, 0.0f);
	}
}
