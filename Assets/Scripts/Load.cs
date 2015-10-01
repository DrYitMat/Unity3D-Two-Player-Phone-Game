using UnityEngine;
using System.Collections;

/// <summary>
/// Load different scenes.
/// </summary>
/// 
/// NOTE: May remove DontDestroyOnLoad property. May be easier to use a new game object with the way Unity UI is setup. 
public class Load : MonoBehaviour {

	// May remove, see above note. 
	void Init() {
		DontDestroyOnLoad (this.gameObject);
	}

	/// <summary>
	/// Loads the game.
	/// </summary>
	public void loadGame(){
		Application.LoadLevel ("game");
	}

	/// <summary>
	/// Loads the credits.
	/// </summary>
	public void loadCredits() {
		Application.LoadLevel ("credits");
	}

	/// <summary>
	/// Loads the ship selector.
	/// </summary>
	public void loadShipSelector() {
		Application.LoadLevel ("shipselect");
	}
}
