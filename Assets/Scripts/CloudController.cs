using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {

	public GameObject spawnOne,spawnTwo;

	public int maxClouds,minClouds;

	private static float MIN_SPEED = 2.0f;
	private static float MAX_SPEED = 4.0f;
	private static float MIN_WAIT = 2.0f;
	private static float MAX_WAIT = 7.5f;

	private float randomSpeed = 50.0f;

	public GameObject cloudPrefab;
	
	private Vector3 cloudEndPoint;

	// Use this for initialization
	void Start () {
		if (maxClouds.Equals(null) || maxClouds == 0)
			maxClouds = 5;
		if (minClouds.Equals(null) || minClouds == 0)
			minClouds = 1;

		if(cloudPrefab == null) 
			cloudPrefab = (GameObject)Resources.Load ("programmercloud");

		if (randomSpeed.Equals(null))
			randomSpeed = 50.0f;

		if (cloudEndPoint.Equals(null))
			cloudEndPoint = new Vector3 (0, 0, 0);
		
		StartCoroutine (cloudSpawnCoRoutine ());
	}

	private void spawnCloud(){
		randomSpeed = Random.Range(MIN_SPEED, MAX_SPEED);
		Vector3 spawnPosition = newVectorTwoPoints (spawnOne.transform.position, spawnTwo.transform.position);
		cloudEndPoint = flipVector (spawnPosition);
		GameObject newCloud = (GameObject)Instantiate (cloudPrefab, spawnPosition, transform.rotation);

		newCloud.transform.SetParent (transform);

		newCloud.GetComponent<Cloud> ().speed = randomSpeed;
		newCloud.GetComponent<Cloud> ().endPoint = cloudEndPoint;
	}

	private Vector3 newVectorTwoPoints(Vector3 a, Vector3 b) {
		float x = Random.Range (a.x, b.x);
		float y = Random.Range (a.y, b.y);
		float z = Random.Range (a.z, b.z);
		Vector3 newVector = new Vector3 (x, y, z);
		return newVector;
	}
	
	private Vector3 flipVector(Vector3 a) {
		float x = -a.x;
		Debug.Log (x);
		Debug.Log (a.x);
		Vector3 newVector = new Vector3 (x, a.y, a.z);
		return newVector;
	}

	// Update is called once per frame
	void Update () {
		if (transform.childCount < minClouds) {
			spawnCloud();
		}
	}

	private IEnumerator cloudSpawnCoRoutine(){
		while (true) {
			if(transform.childCount < maxClouds) {
				spawnCloud();
				float waitTime = Random.Range(MIN_WAIT, MAX_WAIT);
				yield return new WaitForSeconds(waitTime);
			} else yield return null;
		}
	}
}
