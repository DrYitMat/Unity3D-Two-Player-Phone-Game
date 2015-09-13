﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject Target { get; set; }

	private float step = 0.0f;
	public float Speed { get; set; }

	// Use this for initialization
	void Start () {
		Speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Target != null) {
			if(transform.position != Target.transform.position){
				step = Speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Shield")
			Destroy(gameObject);

		if(col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("TakeDamage");
		}
	}
}
