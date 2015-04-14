using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public bool playerInSight;					// Whether or not the player is currently sighted.

	private CircleCollider2D sightCol;			// Reference to the sphere collider trigger component.
	private GameObject player;					// Reference to the player;
	private int layerMask;						// Layer Mask used in enemy sight

	// Use this for initialization
	void Start () {

		//Variables initialization
		playerInSight = false;
		sightCol = GetComponent<CircleCollider2D>();
		player = GameObject.Find("Player");

		//LayerMask Definition
		layerMask = 0;
		int playerLayer = 1 << 8;
		int propLayer = 1 << 10;
		int backgroundLayer = 1 << 11;

		layerMask = playerLayer | propLayer | backgroundLayer;

	}

	void FixedUpdate(){

		//Check if the player is inside the sigh collider
		if (sightCol.OverlapPoint (player.transform.position)) {
			
			//Debug.Log ("Player in sight range");
			
			// By default the player is not in shoot sight.
			playerInSight = false;
			Vector2 sightDirection = (Vector2)player.transform.position - (Vector2)transform.position;

			RaycastHit2D hit = Physics2D.Raycast(transform.position, sightDirection.normalized, sightCol.radius, layerMask);

			//If the object hit by the raycast is the player the player is in sight
			if (hit.collider != null) {
				if (hit.collider.gameObject == player)
					playerInSight = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		

	}
}
