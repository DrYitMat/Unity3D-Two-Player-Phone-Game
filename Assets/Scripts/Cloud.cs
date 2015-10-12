using UnityEngine;
using System.Collections;

/// <summary>
/// Cloud.
/// </summary>
/// 
/// #TODO: Comment. 
public class Cloud : MonoBehaviour {

	public float speed;

	public Vector3 endPoint;

	// Use this for initialization
	void Start () {
		if (speed.Equals(null))
			speed = 50.0f;
		if (endPoint.Equals(null))
			endPoint = new Vector3 (0, 0, 0);
	}



	// Update is called once per frame
	void Update () {
		if (transform.position != endPoint) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, endPoint, step);
		} else
			Destroy (this.gameObject);
	}
}
