using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	private ShipTypes shipType;

	private static int MAX_ABILITIES = 3;

	public List<Ability> abilityList = new List<Ability>();
	public string description;

	// Other things a ship should have.


	//MY LOGIC

	void Update () {
		checkAbilityCount();
	}

	private void checkAbilityCount() {
		if(abilityList.Count > MAX_ABILITIES){
			Debug.Log("Cannot add ability");
			abilityList.RemoveAt(abilityList.Count);
		}
	}
}
