using UnityEngine;
using System.Collections;

public class Defend : MonoBehaviour {

	private bool CanDefend { get; set; }

	private float Cooldown { get; set; }

	public GameObject left,right,top,bottom;

	private GameObject[] leftRight;

	void Start() {
		Cooldown = 3.0f;
		CanDefend = true;
		left.SetActive(false);
		right.SetActive(false);
		top.SetActive(false);
		bottom.SetActive(false);
		if(left != null && right != null) {
		//
		}
	}

	public IEnumerator DefendCooldown(GameObject shield) {
		CanDefend = false;
		shield.SetActive(true);
		yield return new WaitForSeconds(Cooldown);
		CanDefend = true;
		shield.SetActive(false);
	}

	public IEnumerator DefendCooldown(GameObject[] shield) {
		CanDefend = false;
		foreach(GameObject a in shield) {
			a.SetActive(true);
		}
		yield return new WaitForSeconds(Cooldown);
		CanDefend = true;
		foreach(GameObject a in shield) {
			a.SetActive(false);
		}
	}

	public void Top() {
		if(CanDefend) {
			StartCoroutine(DefendCooldown(top));
		} else Debug.Log("Cannot defend at this time");
	}
	
	public void Sides() {
		if(CanDefend) {
			StartCoroutine(DefendCooldown(leftRight));
		} else Debug.Log("Cannot defend at this time");
	}
	
	public void Bottom() {
		if(CanDefend) {
			StartCoroutine(DefendCooldown(bottom));
		} else Debug.Log("Cannot defend at this time");
	}
}
