using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private Vector2             movementDirection;
	public  float               movementSpeed;
	public  int                 maxBounces;
    private CircleCollider2D    circleCollider;
	private int                 bounceCount;
    private CollisionDetector   collisionDetector;

	// Use this for initialization
	void Start () {
        collisionDetector = GetComponent<CollisionDetector>();
        circleCollider = GetComponent<CircleCollider2D>();
		bounceCount = 0;
    }
	
	void Update(){
        Vector2 position = transform.position;
		Vector2 requestedMovement = movementDirection * movementSpeed * Time.deltaTime;
		//Debug.Log("Requested Movement in direction: " + movementDirection.x + "  " + movementDirection.y);
        CollisionAttributes collisionAttributes = null;
		Collider2D otherCollider = null;
        
        otherCollider = collisionDetector.GetNextCollision(position, requestedMovement, circleCollider, ref collisionAttributes);

        Destructible otherDestructible = null;
        if(otherCollider != null)
            otherDestructible = otherCollider.GetComponent<Destructible>();

        if (otherDestructible != null)        //TODO: Exclude firing entity if bounceCount = 0
        {
            otherDestructible.health -= 10;
            if (otherDestructible.health <= 0)
                DestroyObject(otherDestructible.gameObject);

            DestroyObject(this.gameObject);
        }
        
        while (collisionAttributes != null)
        {
            if (bounceCount == maxBounces)
            {
                DestroyObject(this.gameObject);
                break;
            }
            Vector2 movementToCollisionPoint = collisionAttributes.collisionPoint - position;

            if (Vector2.Dot(movementToCollisionPoint, requestedMovement) <= 0)
                Debug.Log("NO! This can never happen!");

			Vector2 movementToStopPosition = movementToCollisionPoint * ((movementToCollisionPoint.magnitude - 0.001f) / movementToCollisionPoint.magnitude);
			Vector3 movementToStopPoint3D = movementToStopPosition;

            if (Vector2.Dot(movementToStopPosition, requestedMovement) < 0.001)
            {
                movementToStopPoint3D = Vector3.zero;
                movementToStopPosition = Vector2.zero;
            }


            requestedMovement -= movementToStopPosition;
			transform.position += movementToStopPoint3D;


            requestedMovement = requestedMovement - 2 * Vector2.Dot(requestedMovement, collisionAttributes.normal) * collisionAttributes.normal;
			movementDirection = movementDirection - 2 * Vector2.Dot(movementDirection, collisionAttributes.normal) * collisionAttributes.normal;
			movementDirection.Normalize();
			bounceCount++;
        
            position = transform.position;
			otherCollider = collisionDetector.GetNextCollision(position, requestedMovement, circleCollider, ref collisionAttributes);
        }

        Vector3 movement = requestedMovement;

        transform.position += movement;
	}


	public void Fire(Vector2 direction)
	{
		movementDirection = direction;
		movementDirection.Normalize();
		//Debug.Log("Bullet firerd in direction: " + movementDirection.x + "  " + movementDirection.y);

	}

}
