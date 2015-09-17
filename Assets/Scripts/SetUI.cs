using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This script allows the UI to be easily adapted to different situations, with attack and defend functions added to the UI buttons.
/// 
/// Hierarchy should this: 
/// 
/// Parent (this script)
/// 	AttackParent
/// 		Top
/// 		Sides
/// 		Bottom
/// 	DefendParent
/// 		Top
/// 		Sides
/// 		Bottom
/// </summary>
/// 
/// TODO: Add more flexibility by counting the children and adding listerns accordingly. 
public class SetUI : MonoBehaviour {

	private Attack playerAttack;
	private Defend playerDefend;
	private Transform attack,defend;

	void Start() {
		setPlayerObject();
		attack = transform.GetChild(0);
		defend = transform.GetChild(1);
		createListeners();
	}

	private void setPlayerObject() {
		if(gameObject.CompareTag("PlayerOneUI")) {
			playerAttack = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Attack>();
			playerDefend = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Defend>();
		} else if(gameObject.CompareTag("PlayerTwoUI")) {
			playerAttack = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Attack>();
			playerDefend = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Defend>();
		}
		else Debug.Log("Could not set player.");

		gameObject.transform.rotation = playerAttack.transform.rotation;
	}

	/// <summary>
	/// Creates the listeners. Object order need to be as follows Top - Sides - Bottom
	/// </summary>
	private void createListeners() {
		attack.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => { playerAttack.Top();});
		attack.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => { playerAttack.Sides();});
		attack.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => { playerAttack.Bottom();});
		defend.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => { playerDefend.Top();});
		defend.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => { playerDefend.Sides();});
		defend.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => { playerDefend.Bottom();});
	}



}
