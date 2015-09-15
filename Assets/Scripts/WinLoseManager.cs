using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLoseManager : MonoBehaviour {

	public Player playerOne, playerTwo;

	public GameObject playerOneTextUI, playerTwoTextUI, playAgain;

	public Countdown countDown;

	// Use this for initialization
	void Start () {
		if(playerOne == null || playerTwo == null){
			Debug.Log("Players are not set!");
			GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
			int a = 0;
			Debug.Log("Attempting to find players...");
			foreach(GameObject b in playerList){
				Player p = b.GetComponent<Player>();
				if(a == 0) playerOne = p;
				else playerTwo = p;
				Debug.Log("Player one: " + playerOne + " Player Two: " + playerTwo);
				a++;
			}
		}

		playerOneTextUI.SetActive(false);
		playerTwoTextUI.SetActive(false);
		playAgain.SetActive(false);
	}

	public void playerLost(Player playerLose){
		Debug.Log("PlayerLost called.");
		if(playerOne == playerLose) {
			playerOneTextUI.GetComponent<Text>().text = "LOSER";
			playerTwoTextUI.GetComponent<Text>().text = "WINNER";
		} else if(playerTwo == playerLose){
			playerOneTextUI.GetComponent<Text>().text = "WINNER";
			playerTwoTextUI.GetComponent<Text>().text = "LOSER";
		}

		playAgain.SetActive(true);
		playerOneTextUI.SetActive(true);
		playerTwoTextUI.SetActive(true);
		countDown.toggleObjects(false);
	}
	
 	// Reset the game. 
	public void Reset(){
		playerOne.Reset();
		playerTwo.Reset();
		playAgain.SetActive(false);
		playerOneTextUI.SetActive(false);
		playerTwoTextUI.SetActive(false);
		countDown.Reset();
	}
}
