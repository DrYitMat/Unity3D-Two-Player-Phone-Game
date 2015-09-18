using UnityEngine;
using System.Collections;

/// <summary>
/// Player.
/// </summary>
/// 
/// TODO: Comment!
public class Player : MonoBehaviour {

	private static int BASE_HITPOINTS = 10;

	private int HitPoints { get; set; }

	private WinLoseManager winLoss;

	// Use this for initialization
	void Start () {
		HitPoints = BASE_HITPOINTS;
		winLoss = GameObject.FindGameObjectWithTag("WinLossManager").GetComponent<WinLoseManager>();
	}

	public void Reset(){
		HitPoints = BASE_HITPOINTS;
	}

	public void TakeDamage() {
		HitPoints--;
		Debug.Log("Player was hit for one");
		CheckHealth();
	}

	public void TakeDamage(int a) {
		HitPoints -= a;
		Debug.Log("Player was hit for " + a);
		CheckHealth();
	}


	private void CheckHealth() {
		if(HitPoints <= 0) {
			Debug.Log("Player is Dead");
			winLoss.playerLost(this);
		} else Debug.Log("Player has " + HitPoints + " hitpoints left." );
	}
}

