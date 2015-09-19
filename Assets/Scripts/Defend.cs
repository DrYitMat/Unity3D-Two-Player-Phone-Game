using UnityEngine;
using System.Collections;

/// <summary>
/// Defend the ship. Spawn shield that deflects/destroys incoming attacks.
/// </summary>
/// 
/// TODO: Comment.
public class Defend : MonoBehaviour {
	
	private bool CanDefendTop { get; set; }
	private bool CanDefendSides { get; set; }
	private bool CanDefendBottom { get; set; }

	private GameObject shieldBase, left,right,top,bottom;

	void Start() {
		CanDefendTop = true;
		CanDefendSides = true;
		CanDefendBottom = true;

		shieldBase = transform.GetChild(0).gameObject;
		top = shieldBase.transform.GetChild(0).gameObject;
		bottom = shieldBase.transform.GetChild(1).gameObject;
		left = shieldBase.transform.GetChild(2).gameObject;
		right = shieldBase.transform.GetChild(3).gameObject;

		left.SetActive(false);
		right.SetActive(false);
		top.SetActive(false);
		bottom.SetActive(false);
	}

	private IEnumerator DeployShield(int direction) {

		switch(direction) {
		case 0:
			CanDefendTop = false;
			top.SetActive(true);
			yield return new WaitForSeconds(Globals.TOP_COOLDOWN/2);
			StartCoroutine(DefendCooldownTop());
			break;
		case 1:
			CanDefendSides = false;
			left.SetActive(true);
			right.SetActive(true);
			yield return new WaitForSeconds(Globals.SIDES_COOLDOWN/2);
			StartCoroutine(DefendCooldownSides());
			break;
		case 2:
			CanDefendBottom = false;
			bottom.SetActive(true);
			yield return new WaitForSeconds(Globals.BOTTOM_COOLDOWN/2);
			StartCoroutine(DefendCooldownBottom());
			break;
		}
		yield return null;
	}

	private IEnumerator DefendCooldownTop() {
		Debug.Log("Cooldown Top");
		top.SetActive(false);
		yield return new WaitForSeconds(Globals.TOP_COOLDOWN);
		CanDefendTop = true;
	}

	private IEnumerator DefendCooldownSides() {
		Debug.Log("Cooldown Sides");
		left.SetActive(false);
		right.SetActive(false);
		yield return new WaitForSeconds(Globals.SIDES_COOLDOWN);
		CanDefendSides = true;
	}

	private IEnumerator DefendCooldownBottom() {
		Debug.Log("Cooldown Bottom");
		bottom.SetActive(false);
		yield return new WaitForSeconds(Globals.BOTTOM_COOLDOWN);
		CanDefendBottom = true;
	}

	public void Top() {
		if(CanDefendTop) {
			StartCoroutine(DeployShield(0));
		} else Debug.Log("Cannot defend at this time");
	}
	
	public void Sides() {
		if(CanDefendSides) {
			StartCoroutine(DeployShield(1));
		} else Debug.Log("Cannot defend at this time");
	}
	
	public void Bottom() {
		if(CanDefendBottom) {
			StartCoroutine(DeployShield(2));
		} else Debug.Log("Cannot defend at this time");
	}

	public void Reset() {
		CanDefendTop = true;
		CanDefendSides = true;
		CanDefendBottom = true;
	}
}
