using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public bool playerInSight;					// Whether or not the player is currently sighted.

	private CircleCollider2D sightCol;				// Reference to the sphere collider trigger component.
	private GameObject player;					// Reference to the player;


	// Use this for initialization
	void Start () {
		playerInSight = false;
		sightCol = GetComponent<CircleCollider2D>();
		player = GameObject.Find("Player");
	}


	// Update is called once per frame
	void Update () {
		//Check if the player is inside the sigh collider
		if (sightCol.OverlapPoint (player.transform.position)){

			Debug.Log("I see the player");

			// By default the player is not in sight.
			//playerInSight = false;

			// Create a vector from the enemy to the player.
			//Vector3 sightDirection = player.transform.position - transform.position;

			//RaycastHit hit;
			
			// ... and if a raycast towards the player hits something...
			//if(Physics.Raycast(transform.position, sightDirection.normalized, out hit, Mathf.Infinity))
			//{
				// ... and if the raycast hits the player...
				//if(hit.collider.gameObject == player)
				//playerInSight = true;
			//}
			playerInSight = true;
		} else {
			playerInSight = false;}
	}
}
