using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private bool CanAttack { get; set; }

	private float Cooldown { get; set; }


	/// <summary>
	/// Spawn Locations. 
	/// </summary>
	private Transform target, spawnBase, top,bottom,left,right;

	/// <summary>
	/// Projectile prefabs. These need to have the Projectile script attached to them. 
	/// </summary>
	public GameObject prefabSide,prefabTop,prefabBottom;

	void Start() {
		Cooldown = 3.0f;
		CanAttack = true;
		setObjects();
	}

	private void setObjects() {
		if(gameObject.CompareTag("PlayerOne")) {
			target = GameObject.FindGameObjectWithTag("PlayerTwo").transform.GetChild(0).transform;
		} else if(gameObject.CompareTag("PlayerTwo")){
			target = GameObject.FindGameObjectWithTag("PlayerOne").transform.GetChild(0).transform;
		} else Debug.Log("Could not set player object. Check your tags.");

		spawnBase = gameObject.transform.GetChild(1);
		top = spawnBase.GetChild(0);
		bottom = spawnBase.GetChild(1);
		left = spawnBase.GetChild(2);
		right = spawnBase.GetChild(3);
	}
	
	private IEnumerator AttackCooldown() {
		CanAttack = false;
		yield return new WaitForSeconds(Cooldown);
		CanAttack = true;
	}

	public void Top() {
		if(CanAttack) {
			GameObject newProjectile = (GameObject) Instantiate(prefabTop, top.position, Quaternion.identity);
			newProjectile.GetComponent<Projectile>().Target = target;
			StartCoroutine(AttackCooldown());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Sides() {
		if(CanAttack) {
			GameObject newProjectileLeft = (GameObject) Instantiate(prefabSide, left.position, Quaternion.identity);
			GameObject newProjectileRight = (GameObject) Instantiate(prefabSide, right.position, Quaternion.identity);
			newProjectileLeft.GetComponent<Projectile>().Target = target;
			newProjectileRight.GetComponent<Projectile>().Target = target;
			StartCoroutine(AttackCooldown());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Bottom() {
		if(CanAttack) {
			GameObject newProjectile = (GameObject) Instantiate(prefabTop, bottom.position, Quaternion.identity);
			newProjectile.GetComponent<Projectile>().Target = target;
			StartCoroutine(AttackCooldown());
		} else Debug.Log("Cannot attack at this time");
	}

	public void Reset() {
		CanAttack = true;
	}
}
