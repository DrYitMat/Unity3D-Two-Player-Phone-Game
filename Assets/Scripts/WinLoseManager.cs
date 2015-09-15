using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLoseManager : MonoBehaviour {

	public Player playerOne, playerTwo;

	public GameObject playerOneTextUI, playerTwoTextUI, playerOneUI, playerTwoUI, playAgain;

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
		Debug.Log("Msg one");
		// These aren't being called....
		playerOneTextUI.SetActive(true);
		playerTwoTextUI.SetActive(true);
		playerOneUI.SetActive(false);
		playerTwoUI.SetActive(false);
		Debug.Log("msg two");
	}



	public void Reset(){
		StartCoroutine(Pause());
	}

	private IEnumerator Pause() {
		yield return new WaitForSeconds(2.5f);
		playerOne.Reset();
		playerTwo.Reset();
		playAgain.SetActive(false);
		playerOneTextUI.SetActive(false);
		playerTwoTextUI.SetActive(false);
		playerOneUI.SetActive(true);
		playerTwoUI.SetActive(true);
	}
}
