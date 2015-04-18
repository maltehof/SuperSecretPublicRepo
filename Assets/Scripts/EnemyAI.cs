using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float enemySpeed;
	public float fireRate;
	
	private GameObject player;
	private Gun gun;
	private EnemySight enemySight;
	private CircleCollider2D circleCollider;
	private CollisionDetector collisionDetector;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		enemySight = GetComponent<EnemySight>();
		gun = GetComponentInChildren<Gun> ();
		collisionDetector = GetComponent<CollisionDetector>();
		foreach (var component in GetComponents<CircleCollider2D>()) {
			Debug.Log ("Collider ray: " + component.radius);
			if (!component.isTrigger) {
				circleCollider = (CircleCollider2D) component;
			}
		}
	}

	void Update(){
		if (enemySight.playerInSight) gun.Fire ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		float zrotation = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x ) * Mathf.Rad2Deg - 90;
		transform.eulerAngles = new Vector3 (0,0,zrotation);

		//Request movement and check colliders
		Vector2 requestedMovement = transform.up * enemySpeed * Time.deltaTime;
		Vector3 movement = collisionDetector.RequestMovement (transform.position, requestedMovement, circleCollider);
		
		transform.position += movement;
	}
}
