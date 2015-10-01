using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetThumbnail : MonoBehaviour {

	// May remove default ship loading or change it to set Player script object to a default. 
	void Start () {
		if (gameObject.GetComponent<Player> ().shipSprite)
			gameObject.GetComponent<Image> ().sprite = gameObject.GetComponent<Player> ().shipSprite;
		else
			gameObject.GetComponent<Image> ().sprite = (GameObject) Resources.Load ("defaultshipsprite");
	}

}
