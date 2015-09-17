using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Counts down to the start of the game. 
/// 
/// ToggleObjects can be used by other scripts to toggle the UI, if needed. 
/// </summary>
public class Countdown : MonoBehaviour {

	// Countdown text, IE 3..2..1..GO
	private Text countText;

	// List of objects to toggle on/off, when needed. 
	private List<Transform> objectsToToggle = new List<Transform>();

	// Init
	void Start() {
		countText = gameObject.GetComponent<Text>();
		objectsToToggle.Add(GameObject.FindGameObjectWithTag("PlayerOneUI").transform);
		objectsToToggle.Add(GameObject.FindGameObjectWithTag("PlayerTwoUI").transform);
		StartCoroutine(Count());
	}

	// Start the countdown timer over. 
	public void Reset() {
		StartCoroutine(Count());
	}

	/// <summary>
	/// Toggles all objects in the objectsToToggle list on or off.
	/// 
	/// if t == true objects will be set active (visibile in game). if t == false objects will be set inactive (not visible in game).
	/// </summary>
	/// <param name="t">If set to <c>true</c> t.</param>
	public void ToggleObjects(bool t) {
		foreach(Transform a in objectsToToggle) {
			foreach(Transform b in a) {
				b.gameObject.SetActive(t);
			}
		}
	}

	/// <summary>
	/// Countdown to the start of a new game. This is a three second countdown. 
	/// 
	/// Toggle the objects off before the start of the countdown, toggle the objects on at the end of the countdown. 
	/// </summary>
	private IEnumerator Count() {
		ToggleObjects(false);
		countText.text = "3";
		yield return new WaitForSeconds(1.0f);
		countText.text = "2";
		yield return new WaitForSeconds(1.0f);
		countText.text = "1";
		yield return new WaitForSeconds(1.0f);
		countText.text = "GO!";
		ToggleObjects(true);
		yield return new WaitForSeconds(1.0f);
		countText.text = "";
	}
}
