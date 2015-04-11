using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour {

	public float movementSpeed;
	public float rotationSpeed;

	//private CircleCollider2D circleCollider;
	private Gun gun;

	// Use this for initialization
	void Start () {
		transform.Rotate ( new Vector3(0,0, 0) );
		gun = GetComponentInChildren<Gun> ();
		//circleCollider = GetComponent<CircleCollider2D>();
	}

	void Update(){
		if (Input.GetButton ("Fire1"))
			gun.Fire ();
	}

	void FixedUpdate () {
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
		
		Vector3 movementVector = movementDirection * movementSpeed * Time.deltaTime;

		/*
		float scaledRadius = circleCollider.radius * circleCollider.transform.localScale.x;

		Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - scaledRadius - movementSpeed * Time.deltaTime,
		                                                              transform.position.y + scaledRadius + movementSpeed * Time.deltaTime),
		                                                  new Vector2(transform.position.x + scaledRadius + movementSpeed * Time.deltaTime,
		                                                              transform.position.y - scaledRadius - movementSpeed * Time.deltaTime));
		

		for(int i = 0;
		    i < 4 && movementVector != Vector2.zero;
		    ++i)
		{
			float fractionTillCollision = -1.0f;
			Vector2 remainingMovement = Vector2.zero;
			
			foreach(Collider2D otherCollider in colliders)
			{
				if(otherCollider.GetType() == typeof(BoxCollider2D) )
				{
					BoxCollider2D boxCollider = otherCollider as BoxCollider2D;
					
					Vector2 CornerA = boxCollider.transform.rotation * boxCollider.transform.TransformPoint(new Vector3(boxCollider.offset.x - boxCollider.size.x/2.0f,
					                                                                                                    boxCollider.offset.y - boxCollider.size.y/2.0f,
					                                                                                                    0.0f) );
					Vector2 CornerB = boxCollider.transform.rotation * boxCollider.transform.TransformPoint(new Vector3(boxCollider.offset.x - boxCollider.size.x/2.0f,
					                                                                                                    boxCollider.offset.y + boxCollider.size.y/2.0f,
					                                                                                                    0.0f) );
	
					Vector2 CornerC = boxCollider.transform.rotation * boxCollider.transform.TransformPoint(new Vector3(boxCollider.offset.x + boxCollider.size.x/2.0f,
					                                                                                                    boxCollider.offset.y + boxCollider.size.y/2.0f,
					                                                                                                    0.0f) );
	
					Vector2 CornerD = boxCollider.transform.rotation * boxCollider.transform.TransformPoint(new Vector3(boxCollider.offset.x + boxCollider.size.x/2.0f,
					                                                                                                    boxCollider.offset.y - boxCollider.size.y/2.0f,
					                                                                                                    0.0f) );
	
					Vector2 lineStartAB = CornerA + (CornerA - CornerD).normalized * scaledRadius;
					Vector2 lineEndAB   = CornerB + (CornerB - CornerC).normalized * scaledRadius;
	
					Vector2 lineStartBC = CornerB + (CornerB - CornerA).normalized * scaledRadius;
					Vector2 lineEndBC   = CornerC + (CornerC - CornerD).normalized * scaledRadius;
	
					Vector2 lineStartCD = CornerC + (CornerC - CornerB).normalized * scaledRadius;
					Vector2 lineEndCD   = CornerD + (CornerD - CornerA).normalized * scaledRadius;
	
					Vector2 lineStartDA = CornerD + (CornerD - CornerC).normalized * scaledRadius;
					Vector2 lineEndDA   = CornerA + (CornerA - CornerB).normalized * scaledRadius;
	
					Vector2 tempRemainingMovement = Vector2.zero;
					float tempFraction = LinearAlgebra.CollideWithLine(lineStartAB, lineEndAB, transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && tempFraction < 1)
					{
						fractionTillCollision = tempFraction;
						remainingMovement = tempRemainingMovement;
					}
	
					tempFraction = LinearAlgebra.CollideWithLine(lineStartBC, lineEndBC, transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision ) )
					{
						fractionTillCollision = tempFraction;
						remainingMovement = tempRemainingMovement;
					}
						
	
					tempFraction = LinearAlgebra.CollideWithLine(lineStartCD, lineEndCD, transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision ) )
					{
							fractionTillCollision = tempFraction;
							remainingMovement = tempRemainingMovement;
						}
						
					tempFraction = LinearAlgebra.CollideWithLine(lineStartDA, lineEndDA, transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision ) )
					{
						fractionTillCollision = tempFraction;
						remainingMovement = tempRemainingMovement;
					}
					
					tempFraction = LinearAlgebra.CollideWithCircle(CornerA, scaledRadius, this.transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision ) )
					{
						fractionTillCollision = tempFraction;
						remainingMovement = tempRemainingMovement;
					}
					
					tempFraction = LinearAlgebra.CollideWithCircle(CornerB, scaledRadius, this.transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision ) )
					{
						fractionTillCollision = tempFraction;
						remainingMovement = tempRemainingMovement;
					}
					
					tempFraction = LinearAlgebra.CollideWithCircle(CornerC, scaledRadius, this.transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision ) )
					{
						fractionTillCollision = tempFraction;
						remainingMovement = tempRemainingMovement;
					}
					
					tempFraction = LinearAlgebra.CollideWithCircle(CornerD, scaledRadius, this.transform.position, movementVector, ref tempRemainingMovement);
					if(tempFraction > 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision ) )
					{
						fractionTillCollision = tempFraction;
						remainingMovement = tempRemainingMovement;
					}
				}
			}
			if(fractionTillCollision > 0 && fractionTillCollision < 1)
			{
				movementVector = fractionTillCollision * movementVector - 0.1f * movementVector;
				Vector3 move = movementVector;
				transform.position += move;
				Debug.Log("Stop it right there!");
			}
			else{
				Vector3 move = movementVector;
				transform.position += move;
			}
			
			Debug.Log("Remaining Movement Vector is " + remainingMovement.x + " / " + remainingMovement.y);
			movementVector = remainingMovement;
		}
		*/

		transform.position += movementVector;


	}
	
}
