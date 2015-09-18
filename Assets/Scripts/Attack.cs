using UnityEngine;
using System.Collections;

/// <summary>
/// Attack the enemy ship. 
/// </summary>
/// 
/// TODO: Create cooldown functions for each attack. Create a global accessable cooldown vars, so Defend can access. 
/// TODO: Comment out new functions 
public class Attack : MonoBehaviour {

	private bool CanAttack { get; set; }

	private float Cooldown { get; set; }
	private static float BASE_COOLDOWN = 3.0f;

	private float WaitTime { get; set; }
	private static float BASE_WAITTIME = 1.5f;

	/// <summary>
	/// Spawn Locations. 
	/// </summary>
	private Transform target, spawnBase, top,bottom,left,right;

	/// <summary>
	/// Projectile prefabs. These need to have the Projectile script attached to them. 
	/// </summary>
	private GameObject prefabTop,prefabBottom,prefabSide;

	private Transform wTop,wBottom,wLeft,wRight;

	private GameObject warningPrefab;

	private int direction;

	void Start() {
		Cooldown = BASE_COOLDOWN;
		WaitTime = BASE_WAITTIME;
		CanAttack = true;
		setObjects();
	}

	/// <summary>
	/// Set objects so they are not null.
	/// </summary>
	/// 
	/// TODO: And comment out this mofo....
	private void setObjects() {
		if(gameObject.CompareTag("PlayerOne")) {
			target = GameObject.FindGameObjectWithTag("PlayerTwo").transform.GetChild(0).transform;
		} else if(gameObject.CompareTag("PlayerTwo")){
			target = GameObject.FindGameObjectWithTag("PlayerOne").transform.GetChild(0).transform;
		} else Debug.Log("Could not set player object. Check your tags.");

		spawnBase = transform.GetChild(1);

		top = spawnBase.GetChild(0);
		bottom = target.parent.GetChild(1).GetChild(1);
		left = spawnBase.GetChild(2);
		right = spawnBase.GetChild(3);

		prefabTop = (GameObject) Resources.Load("programmerprojectile");
		prefabBottom = (GameObject) Resources.Load("programmerprojectile");
		prefabSide = (GameObject) Resources.Load("programmerprojectile");

		warningPrefab = (GameObject) Resources.Load("WarningIndicater");

		wTop = target.GetChild(0);
		wBottom = target.GetChild(1);
		wLeft = target.GetChild(2);
		wRight = target.GetChild(3);
	}

	/// <summary>
	/// Attack Cooldown. 
	/// </summary>
	/// <returns>The cooldown.</returns>
	/// 
	/// TODO: Add individual cooldown functions. 
	private IEnumerator AttackCooldown() {
		CanAttack = false;
		yield return new WaitForSeconds(Cooldown);
		CanAttack = true;
	}

	/// <summary>
	/// Fire the correct projectile.
	/// </summary>
	/// 
	/// TODO: Comment this SOB
	private IEnumerator Fire(){
		Debug.Log("Charging...");
		GameObject newIndicatorTop = null;
		GameObject newIndicatorLeft = null;
		GameObject newIndicatorRight = null;
		GameObject newIndicatorBottom = null;
		switch(direction) {
		case 0:
			newIndicatorTop = (GameObject) Instantiate(warningPrefab, wTop.position, Quaternion.identity);
			break;
		case 1:
			newIndicatorLeft = (GameObject) Instantiate(warningPrefab, wLeft.position, Quaternion.identity);
			newIndicatorRight = (GameObject) Instantiate(warningPrefab, wRight.position, Quaternion.identity);
			break;
		case 2:
			newIndicatorBottom = (GameObject) Instantiate(warningPrefab, wBottom.position, Quaternion.identity);
			break;
		}
		yield return new WaitForSeconds(WaitTime);

		switch(direction) {
		case 0:
			DestroyObject(newIndicatorTop);
			break;
		case 1:
			DestroyObject(newIndicatorLeft);
			DestroyObject(newIndicatorRight);
			break;
		case 2:
			DestroyObject(newIndicatorBottom);
			break;
		}

		switch(direction) {
		case 0:
			GameObject newProjectileTop = (GameObject) Instantiate(prefabTop, top.position, Quaternion.identity);
			newProjectileTop.GetComponent<Projectile>().TargetList.Add(target);
			newProjectileTop.GetComponent<Projectile>().Damage = 1;
			break;
		case 1:
			GameObject newProjectileLeft = (GameObject) Instantiate(prefabSide, left.position, Quaternion.identity);
			GameObject newProjectileRight = (GameObject) Instantiate(prefabSide, right.position, Quaternion.identity);
			newProjectileLeft.GetComponent<Projectile>().TargetList.Add(target.parent.GetChild(1).GetChild(3));
			newProjectileRight.GetComponent<Projectile>().TargetList.Add(target.parent.GetChild(1).GetChild(2));
			newProjectileLeft.GetComponent<Projectile>().TargetList.Add(target);
			newProjectileRight.GetComponent<Projectile>().TargetList.Add(target);
			newProjectileLeft.GetComponent<Projectile>().Damage = 3;
			newProjectileRight.GetComponent<Projectile>().Damage = 3;
			break;
		case 2:
			GameObject newProjectileBottom = (GameObject) Instantiate(prefabBottom, bottom.position, Quaternion.identity);
			newProjectileBottom.GetComponent<Projectile>().TargetList.Add(target);
			newProjectileBottom.GetComponent<Projectile>().Damage = 2;
			break;
		}
		Debug.Log("Fired!");
		StartCoroutine(AttackCooldown());
	}

	public void Top() {
		direction = 0;
		if(CanAttack) {
			Cooldown = 1.5f;
			StartCoroutine(Fire());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Sides() {
		direction = 1;
		if(CanAttack) {
			Cooldown = 6.0f;
			StartCoroutine(Fire());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Bottom() {
		direction = 2;
		if(CanAttack) {
			Cooldown = 3.0f;
			StartCoroutine(Fire());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Reset() {
		CanAttack = true;
	}
}
