using UnityEngine;
using System.Collections;

public class CollisionAttributes
{
	public Vector2 collisionPoint;
	public Vector2 normal;
};

public class CollisionDetector : MonoBehaviour {

	public Collider2D GetNextCollision(Vector2 position, Vector2 requestedMovement, CircleCollider2D circleCollider, ref CollisionAttributes collisionAttributes)
	{
		Collider2D returnCollider = null;
		float scaledRadius = circleCollider.radius * circleCollider.transform.localScale.x;
		
		Collider2D[] colliders = Physics2D.OverlapAreaAll (new Vector2 (transform.position.x - scaledRadius - Mathf.Abs (requestedMovement.x),
		                                                              transform.position.y + scaledRadius + Mathf.Abs (requestedMovement.y)),
														  new Vector2 (transform.position.x + scaledRadius + Mathf.Abs (requestedMovement.x),
		                                                              transform.position.y - scaledRadius - Mathf.Abs (requestedMovement.y)));
		

		float fractionTillCollision = -1.0f;
		Vector2 normal = Vector2.zero;
			
		foreach (Collider2D otherCollider in colliders) {
			if (otherCollider.GetType () == typeof(BoxCollider2D)) {
				BoxCollider2D boxCollider = otherCollider as BoxCollider2D;
					
				Vector2 CornerA = boxCollider.transform.TransformPoint (new Vector3 (boxCollider.offset.x - boxCollider.size.x / 2.0f,
				                                                                   boxCollider.offset.y - boxCollider.size.y / 2.0f,
				                                                                   0.0f));
				Vector2 CornerB = boxCollider.transform.TransformPoint (new Vector3 (boxCollider.offset.x - boxCollider.size.x / 2.0f,
				                                                                   boxCollider.offset.y + boxCollider.size.y / 2.0f,
				                                                                   0.0f));
					
				Vector2 CornerC = boxCollider.transform.TransformPoint (new Vector3 (boxCollider.offset.x + boxCollider.size.x / 2.0f,
				                                                                   boxCollider.offset.y + boxCollider.size.y / 2.0f,
				                                                                   0.0f));
					
				Vector2 CornerD = boxCollider.transform.TransformPoint (new Vector3 (boxCollider.offset.x + boxCollider.size.x / 2.0f,
				                                                                   boxCollider.offset.y - boxCollider.size.y / 2.0f,
				                                                                   0.0f));

				Vector2 lineStartAB = CornerA + (CornerA - CornerD).normalized * scaledRadius;
				Vector2 lineEndAB = CornerB + (CornerB - CornerC).normalized * scaledRadius;
					
				Vector2 lineStartBC = CornerB + (CornerB - CornerA).normalized * scaledRadius;
				Vector2 lineEndBC = CornerC + (CornerC - CornerD).normalized * scaledRadius;
					
				Vector2 lineStartCD = CornerC + (CornerC - CornerB).normalized * scaledRadius;
				Vector2 lineEndCD = CornerD + (CornerD - CornerA).normalized * scaledRadius;
					
				Vector2 lineStartDA = CornerD + (CornerD - CornerC).normalized * scaledRadius;
				Vector2 lineEndDA = CornerA + (CornerA - CornerB).normalized * scaledRadius;

				Vector2 tempNormal = Vector2.zero;
				float tempFraction = LinearAlgebra.CollideWithLine (lineStartAB, lineEndAB, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}
					
				tempFraction = LinearAlgebra.CollideWithLine (lineStartBC, lineEndBC, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}

					
				tempFraction = LinearAlgebra.CollideWithLine (lineStartCD, lineEndCD, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}
					
				tempFraction = LinearAlgebra.CollideWithLine (lineStartDA, lineEndDA, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}
					
				tempFraction = LinearAlgebra.CollideWithCircle (CornerA, scaledRadius, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}
					
				tempFraction = LinearAlgebra.CollideWithCircle (CornerB, scaledRadius, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}
					
				tempFraction = LinearAlgebra.CollideWithCircle (CornerC, scaledRadius, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}
					
				tempFraction = LinearAlgebra.CollideWithCircle (CornerD, scaledRadius, position, requestedMovement, ref tempNormal);
				if (tempFraction >= 0 && (fractionTillCollision < 0 || tempFraction < fractionTillCollision)) {
					fractionTillCollision = tempFraction;
					returnCollider = otherCollider;
					normal = tempNormal;
				}
			}
		}
		if (fractionTillCollision >= 0 && fractionTillCollision < 1) 
		{
			//TODO: Check if there is a better way then to use an -epsilon * requestedMovement to not go into walls.
			requestedMovement = fractionTillCollision * requestedMovement;
			collisionAttributes = new CollisionAttributes();
			collisionAttributes.collisionPoint = position + requestedMovement;
			collisionAttributes.normal = normal;
		} 
		else
		{
			collisionAttributes = null;
		}
		

		return returnCollider;
	}

	public Vector2 RequestMovement(Vector2 position, Vector2 remainingMovement, CircleCollider2D circleCollider)
	{
		Vector2 allowedMovement = Vector2.zero;

		for (int i = 0;
		     i < 4 && remainingMovement != Vector2.zero;
		     ++i) 
		{
			//For DEBUG
			float scaledRadius = circleCollider.radius * circleCollider.transform.localScale.x;
			Collider2D[] colliders = Physics2D.OverlapAreaAll (new Vector2 (transform.position.x - scaledRadius - Mathf.Abs (remainingMovement.x),
			                                                                transform.position.y + scaledRadius + Mathf.Abs (remainingMovement.y)),
			                                                   new Vector2 (transform.position.x + scaledRadius + Mathf.Abs (remainingMovement.x),
			            												    transform.position.y - scaledRadius - Mathf.Abs (remainingMovement.y)));
			//END For DEBUG
			CollisionAttributes nextCollision = null;
			GetNextCollision(position, remainingMovement, circleCollider, ref nextCollision);

			if(nextCollision == null)
			{
				allowedMovement += remainingMovement;
				remainingMovement = Vector2.zero;
				foreach (Collider2D otherCollider in colliders) {
					if (otherCollider.GetType () == typeof(BoxCollider2D) && otherCollider.OverlapPoint(position)) {
						Debug.Log("Happened in Full Move on iteration Number" + i);
					}
				}
				//Debug.Log("Call to Full Method No. " + i);
			}
			else
			{
				Vector2 movementToCollisionPoint = nextCollision.collisionPoint - position;

				allowedMovement += movementToCollisionPoint;
				position += movementToCollisionPoint;

				foreach (Collider2D otherCollider in colliders) {
					if (otherCollider.GetType () == typeof(BoxCollider2D) && otherCollider.OverlapPoint(position)) {
						Debug.Log("Happened in Fraction Move on iteration Number" + i);
					}
				}

				remainingMovement = remainingMovement - remainingMovement*((movementToCollisionPoint.magnitude*1.1f)/remainingMovement.magnitude);

				remainingMovement = remainingMovement - Vector2.Dot(remainingMovement, nextCollision.normal) * nextCollision.normal;

				//Debug.Log("Call to Fraction Method No. " + i);
			}
		}

		return allowedMovement;
	}
}
