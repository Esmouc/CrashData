﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;

	public Vector2 direction;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
    rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	  public void UpdateVelocity()
	  {
		  rb2d = GetComponent<Rigidbody2D> ();
	    rb2d.velocity = direction * speed * Time.deltaTime;
	  }


	void OnCollisionEnter2D (Collision2D col){


		if (gameObject.tag == "PlayerBullet"){
			if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "RedBullet"){
				Destroy (this.gameObject);
			}
		}else{
			if (col.gameObject.name == "Player")
				GameManager.instance.corruption_level += 0.1f;
		}

		if (col.gameObject.tag != "Bullet" && col.gameObject.tag != "RedBullet"){
			Destroy (this.gameObject);
		}

	}

}
