using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private bool CanAttack { get; set; }

	private float Cooldown { get; set; }


	/// <summary>
	/// Spawn Locations. 
	/// </summary>
	public Transform left,right,top,bottom;

	/// <summary>
	/// Projectile prefabs. These need to have the Projectile script attached to them. 
	/// </summary>
	public GameObject prefabSide,prefabTop,prefabBottom;

	public GameObject target;

	void Start() {
		Cooldown = 3.0f;
		CanAttack = true;
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
