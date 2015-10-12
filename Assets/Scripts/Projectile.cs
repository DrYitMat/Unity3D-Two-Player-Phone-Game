using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Projectile class. It needs a list of targets, it will go to those targets in the order they are in the list.  
/// </summary>
/// 
/// TODO: Push next tranfrom in list to top, so the target can change on the fly, if needed. // I might not need this. Just brainstorming :)
/// TODO: And comment this sucker out. 
public class Projectile : MonoBehaviour {

	public List<Transform> TargetList = new List<Transform>();
	private Transform currentTarget = null;

	private float step = 0.0f;
	public float Speed { get; set; }

	public int damage = 0;

	// Use this for initialization
	void Start () {
		Speed = 5.0f;
		StartCoroutine(goToTargets());
	}

	/// <summary>
	/// Gos to targets in the targetlist. Basically waypoints. 
	/// </summary>
	/// <returns>The to targets.</returns>
	private IEnumerator goToTargets() {
		int a = 0;
		currentTarget = TargetList[a];
		while(true) {
			if(transform.position != currentTarget.position){
				step = Speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);
			} else { a++; currentTarget = TargetList[a]; }
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log("Collision Detected");
		if(coll.gameObject.tag == "Shield") {
			Destroy(this.gameObject);
			Debug.Log("Shield has deflected this hit!");
		}
		if(coll.gameObject.tag == currentTarget.parent.gameObject.tag) {
			Debug.Log(coll.gameObject.tag);
			coll.gameObject.SendMessage("SetHitBy", gameObject.name);
			coll.gameObject.SendMessage("TakeDamage", damage);
			Destroy(this.gameObject);
		}
	}
}
