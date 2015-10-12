using UnityEngine;
using System.Collections;

/// <summary>
/// Attack the enemy ship. 
/// </summary>
/// 
/// TODO: Comment out new functions 
public class Attack : MonoBehaviour {
	
	private bool CanAttackTop { get; set; }
	private bool CanAttackSides { get; set; }
	private bool CanAttackBottom { get; set; }

	/// <summary>
	/// Spawn Locations. 
	/// </summary>
	private Transform target, spawnBase, top,bottom,left,right;

	/// <summary>
	/// Projectile prefabs. These need to have the Projectile script attached to them. 
	/// </summary>
	public GameObject prefabTop,prefabBottom,prefabSide;

	private Transform wTop,wBottom,wLeft,wRight;

	private GameObject warningPrefab;

	void Start() {
		CanAttackTop = true;
		CanAttackSides = true;
		CanAttackBottom = true;

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

		loadProjectilePrefabs ();

		warningPrefab = (GameObject) Resources.Load("WarningIndicater");

		wTop = target.GetChild(0);
		wBottom = target.GetChild(1);
		wLeft = target.GetChild(2);
		wRight = target.GetChild(3);
	}

	/// <summary>
	/// Loads the projectile prefabs. Sets to default projectile if not declared in Player script. 
	/// </summary>
	/// 
	/// NOTE: No need to check for if this game object has a player prefab attached. The game won't work if there is not. IDK How you would FK up that bad. 
	private void loadProjectilePrefabs() {
		if(gameObject.GetComponent<Player> ().projecitlePrefabTop != null)
			prefabTop = gameObject.GetComponent<Player> ().projecitlePrefabTop;
		else prefabTop = (GameObject) Resources.Load("programmerprojectile");

		if(gameObject.GetComponent<Player> ().projecitlePrefabSide != null)
			prefabSide = gameObject.GetComponent<Player> ().projecitlePrefabSide;
		else prefabSide = (GameObject) Resources.Load("programmerprojectile");
	
		if(gameObject.GetComponent<Player> ().projecitlePrefabBottom != null)
			prefabBottom = gameObject.GetComponent<Player> ().projecitlePrefabBottom;
		else prefabBottom = (GameObject) Resources.Load("programmerprojectile");
	}

	private IEnumerator AttackCooldownTop() {
		Debug.Log("Cooldown top attack");
		CanAttackTop = false;
		yield return new WaitForSeconds(Globals.TOP_COOLDOWN);
		CanAttackTop = true;
	}

	private IEnumerator AttackCooldownSides() {
		Debug.Log("Cooldown side attack");
		CanAttackSides = false;
		yield return new WaitForSeconds(Globals.SIDES_COOLDOWN);
		CanAttackSides = true;
	}
	private IEnumerator AttackCooldownBottom() {
		Debug.Log("Cooldown bottom attack");
		CanAttackBottom = false;
		yield return new WaitForSeconds(Globals.BOTTOM_COOLDOWN);
		CanAttackBottom = true;
	}

	/// <summary>
	/// Fire the correct projectile.
	/// </summary>
	/// 
	/// TODO: Comment this SOB
	private IEnumerator Fire(int direction){
		Debug.Log("Charging...");
		GameObject newIndicatorTop = null;
		GameObject newIndicatorLeft = null;
		GameObject newIndicatorRight = null;
		GameObject newIndicatorBottom = null;

		float waitTime = 1.0f;

		switch(direction) {
		case 0:
			newIndicatorTop = (GameObject) Instantiate(warningPrefab, wTop.position, wTop.rotation);
			waitTime = Globals.TOP_COOLDOWN/2;
			break;
		case 1:
			newIndicatorLeft = (GameObject) Instantiate(warningPrefab, wLeft.position, wLeft.rotation);
			newIndicatorRight = (GameObject) Instantiate(warningPrefab, wRight.position, wRight.rotation);
			waitTime = Globals.SIDES_COOLDOWN/2;
			break;
		case 2:
			newIndicatorBottom = (GameObject) Instantiate(warningPrefab, wBottom.position, wBottom.rotation);
			waitTime = Globals.BOTTOM_COOLDOWN/2;
			break;
		}

		yield return new WaitForSeconds(waitTime);

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

			StartCoroutine(AttackCooldownTop());

			break;
		case 1:
			GameObject newProjectileLeft = (GameObject) Instantiate(prefabSide, left.position, Quaternion.identity);
			GameObject newProjectileRight = (GameObject) Instantiate(prefabSide, right.position, Quaternion.identity);

			newProjectileLeft.GetComponent<Projectile>().TargetList.Add(target.parent.GetChild(1).GetChild(3));
			newProjectileRight.GetComponent<Projectile>().TargetList.Add(target.parent.GetChild(1).GetChild(2));

			newProjectileLeft.GetComponent<Projectile>().TargetList.Add(target);
			newProjectileRight.GetComponent<Projectile>().TargetList.Add(target);

			StartCoroutine(AttackCooldownSides());

			break;
		case 2:
			GameObject newProjectileBottom = (GameObject) Instantiate(prefabBottom, bottom.position, Quaternion.identity);

			newProjectileBottom.GetComponent<Projectile>().TargetList.Add(target);

			StartCoroutine(AttackCooldownBottom());

			break;
		}
		Debug.Log("Fired!");
	}

	public void Top() {
		if(CanAttackTop) {
			StartCoroutine(Fire(0));
		} else Debug.Log("Cannot attack at this time");
	}

	public void Sides() {
		if(CanAttackSides) {
			StartCoroutine(Fire(1));
		} else Debug.Log("Cannot attack at this time");
	}

	public void Bottom() {
		if(CanAttackBottom) {
			StartCoroutine(Fire(2));
		} else Debug.Log("Cannot attack at this time");
	}

	public void Reset() {
		CanAttackTop = true;
		CanAttackSides = true;
		CanAttackBottom = true;
	}
}
