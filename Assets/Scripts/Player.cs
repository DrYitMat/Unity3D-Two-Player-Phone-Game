using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int HitPoints { get; set; }

	// Use this for initialization
	void Start () {
		HitPoints = 10;
	}

	public void TakeDamage() {
		HitPoints--;
		Debug.Log("Player was hit for one");
	}

	public void TakeDamage(int a) {
		HitPoints -= a;
		Debug.Log("Player was hit for " + a);
	}


	private void CheckHealth() {
		if(HitPoints <= 0) {
			Debug.Log("Player is Dead");
			// Lose
		} else Debug.Log("Player has " + HitPoints + " hitpoints left." );
	}
}

