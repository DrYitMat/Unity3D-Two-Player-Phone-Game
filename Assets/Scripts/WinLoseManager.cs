using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Handles the result of the game.
/// </summary>
/// 
/// TODO: Should probably make playAgain find the WinLossManager in code, and add a listener for it being clicked. 
public class WinLoseManager : MonoBehaviour {

	// Players
	private Player playerOne, playerTwo;

	//Player UIs
	private GameObject playerOneMessageUI, playerTwoMessageUI, playAgain;

	private Countdown countDown;

	// Use this for initialization
	void Start () {
		setObjects();
		playAgain.SetActive(false);
	}

	/// <summary>
	/// Sets the objects.
	/// </summary>
	/// 
	/// May migrate the win/lose gameobject to be a child of PlayerUI. Will need to change logic in countdown for this to work. 
	private void setObjects() {
		playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
		playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
		playerOneMessageUI = GameObject.FindGameObjectWithTag("PlayerOneUI").transform.GetChild(2).gameObject;
		playerTwoMessageUI = GameObject.FindGameObjectWithTag("PlayerTwoUI").transform.GetChild(2).gameObject;
		countDown = GameObject.FindGameObjectWithTag("Countdown").GetComponent<Countdown>();
		playAgain = GameObject.FindGameObjectWithTag("PlayAgain");
	}

	/// <summary>
	/// If a player lost, handle it. Argument is the player who lost. 
	/// </summary>
	/// <param name="playerLose">Player who lost</param>
	public void playerLost(Player playerLose){
		Debug.Log("PlayerLost called.");
		if(playerOne == playerLose) {
			playerOneMessageUI.GetComponent<Text>().text = "LOSER";
			playerTwoMessageUI.GetComponent<Text>().text = "WINNER";
		} else if(playerTwo == playerLose){
			playerOneMessageUI.GetComponent<Text>().text = "WINNER";
			playerTwoMessageUI.GetComponent<Text>().text = "LOSER";
		}

		playAgain.SetActive(true);
		countDown.ToggleObjects(false);
		playerOneMessageUI.SetActive(true);
		playerTwoMessageUI.SetActive(true);
	}
	
 	/// <summary>
 	/// Resets the game. If the game needs to be reset, it should be done through here. 
 	/// </summary>
	public void Reset(){
		playerOne.Reset();
		playerTwo.Reset();
		playerOneMessageUI.GetComponent<Text>().text = "";
		playerTwoMessageUI.GetComponent<Text>().text = "";
		playAgain.SetActive(false);
		countDown.Reset();
	}

	void Update() {

	}

}
