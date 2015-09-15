using UnityEngine;
using System.Collections;

public class Defend : MonoBehaviour {

	private bool CanDefend { get; set; }

	private float Cooldown { get; set; }

	public GameObject left,right,top,bottom;

	void Start() {
		Cooldown = 3.0f;
		CanDefend = true;
		left.SetActive(false);
		right.SetActive(false);
		top.SetActive(false);
		bottom.SetActive(false);
	}

	public IEnumerator DefendCooldown(GameObject shield) {
		CanDefend = false;
		shield.SetActive(true);
		yield return new WaitForSeconds(Cooldown);
		CanDefend = true;
		shield.SetActive(false);
	}

	/// <summary>
	/// Defends the cooldown.
	/// </summary>
	/// <returns>The cooldown.</returns>
	/// <param name="one">One.</param>
	/// <param name="two">Two.</param>
	/// 
	/// Fix lazy params
	public IEnumerator DefendCooldown(GameObject one, GameObject two) {
		CanDefend = false;
		one.SetActive(true);
		two.SetActive(true);
		yield return new WaitForSeconds(Cooldown);
		CanDefend = true;
		one.SetActive(false);
		two.SetActive(false);
	}

	public void Top() {
		if(CanDefend) {
			StartCoroutine(DefendCooldown(top));
		} else Debug.Log("Cannot defend at this time");
	}
	
	public void Sides() {
		if(CanDefend) {
			StartCoroutine(DefendCooldown(left, right));
		} else Debug.Log("Cannot defend at this time");
	}
	
	public void Bottom() {
		if(CanDefend) {
			StartCoroutine(DefendCooldown(bottom));
		} else Debug.Log("Cannot defend at this time");
	}

	public void Reset() {
		CanDefend = true;
	}
}
