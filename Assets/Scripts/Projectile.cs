using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Transform Target { get; set; }
	public Transform FirstTarget { get; set; }

	private float step = 0.0f;
	public float Speed { get; set; }

	// Use this for initialization
	void Start () {
		Speed = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(FirstTarget != null) {
			if(transform.position != FirstTarget.position){
				step = Speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, FirstTarget.position, step);
			} else FirstTarget = null;
		} else if(Target != null) {
			if(transform.position != Target.position){
				step = Speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, Target.position, step);
			} 
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log("Collision Detected");
		if(coll.gameObject.tag == "Shield") {
			Destroy(this.gameObject);
			Debug.Log("Shield has deflected this hit!");
		}
		if(coll.gameObject.tag == Target.parent.gameObject.tag) {
			Debug.Log(coll.gameObject.tag);
			coll.gameObject.SendMessage("TakeDamage");
			Destroy(this.gameObject);
		}
	}
}
