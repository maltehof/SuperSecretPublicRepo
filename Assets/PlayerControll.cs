using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour {

	public float movementSpeed;
	public float rotationSpeed;

	private CircleCollider2D circleCollider;
	private CollisionDetector collisionDetector;
	private Gun gun;

	// Use this for initialization
	void Start () {
		transform.Rotate ( new Vector3(0,0, 0) );
		gun = GetComponentInChildren<Gun> ();
		circleCollider = GetComponent<CircleCollider2D>();
		collisionDetector = GetComponent<CollisionDetector>();
	}

	void FixedUpdate () {
		if (Input.GetButton ("Fire1"))
			gun.Fire ();

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 targetDirection = mousePos - transform.position;
		targetDirection.z = 0;
		targetDirection.Normalize();
		float targetAngle = Mathf.Atan2 (targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler (new Vector3 (0,0, -targetAngle));

		Vector3 movementDirection = Vector3.zero;
		if (Input.GetButton ("MoveUp"))
			movementDirection += new Vector3 ( 0, 1, 0);
		if (Input.GetButton ("MoveDown"))
			movementDirection += new Vector3 ( 0,-1, 0);
		if (Input.GetButton ("MoveLeft"))
			movementDirection += new Vector3 (-1, 0, 0);
		if (Input.GetButton ("MoveRight"))
			movementDirection += new Vector3 ( 1, 0, 0);

		movementDirection.Normalize ();
		
		Vector2 requestedMovement = movementDirection * movementSpeed * Time.deltaTime;

		Vector3 movement = collisionDetector.RequestMovement (transform.position, requestedMovement, circleCollider);

		transform.position += movement;
	}
	
}
