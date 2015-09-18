using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private bool CanAttack { get; set; }

	private float Cooldown { get; set; }

	private float WaitTime { get; set; }

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
		Cooldown = 3.0f;
		WaitTime = 1.5f;
		CanAttack = true;
		setObjects();
	}

	private void setObjects() {
		if(gameObject.CompareTag("PlayerOne")) {
			target = GameObject.FindGameObjectWithTag("PlayerTwo").transform.GetChild(0).transform;
			spawnBase = GameObject.FindGameObjectWithTag("PlayerTwo").transform.GetChild(1).transform;
		} else if(gameObject.CompareTag("PlayerTwo")){
			target = GameObject.FindGameObjectWithTag("PlayerOne").transform.GetChild(0).transform;
			spawnBase = GameObject.FindGameObjectWithTag("PlayerOne").transform.GetChild(1).transform;
		} else Debug.Log("Could not set player object. Check your tags.");

		top = spawnBase.GetChild(0);
		bottom = spawnBase.GetChild(1);
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
	
	private IEnumerator AttackCooldown() {
		CanAttack = false;
		yield return new WaitForSeconds(Cooldown);
		CanAttack = true;
	}

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
			newProjectileTop.GetComponent<Projectile>().Target = target;
			break;
		case 1:
			GameObject newProjectileLeft = (GameObject) Instantiate(prefabSide, left.position, Quaternion.identity);
			GameObject newProjectileRight = (GameObject) Instantiate(prefabSide, right.position, Quaternion.identity);
			Vector3 firstTargetLeft = transform.GetChild(1).GetChild(2);
			Vector3 firstTargetRight = transform.GetChild(1).GetChild(3);
			newProjectileLeft.GetComponent<Projectile>().Target = target;
			newProjectileRight.GetComponent<Projectile>().Target = target;
			break;
		case 2:
			GameObject newProjectileBotom = (GameObject) Instantiate(prefabBottom, bottom.position, Quaternion.identity);
			newProjectileBotom.GetComponent<Projectile>().Target = target;
			break;
		}
		Debug.Log("Fired!");
		StartCoroutine(AttackCooldown());
	}

	public void Top() {
		direction = 0;
		if(CanAttack) {
			StartCoroutine(Fire());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Sides() {
		direction = 1;
		if(CanAttack) {
			StartCoroutine(Fire());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Bottom() {
		direction = 2;
		if(CanAttack) {
			StartCoroutine(Fire());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Reset() {
		CanAttack = true;
	}
}
