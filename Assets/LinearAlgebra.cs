using UnityEngine;

public static class LinearAlgebra{

	static public float CollideWithLine(Vector2 lineStart, Vector2 lineEnd, Vector2 startPosition, Vector2 movementVector, ref Vector2 remainingMovement)
	{
		float endPointFraction = -1.0f;
		remainingMovement = Vector2.zero;
		
		Vector2 lineDir = (lineEnd - lineStart).normalized;

		if(movementVector.x != 0.0f)
		{
			float velocitySlope = movementVector.y/movementVector.x;

			if((lineDir.x * velocitySlope - lineDir.y) == 0.0f)
				return -1.0f;

			float lineDist = (lineStart.y - startPosition.y + (startPosition.x - lineStart.x) * velocitySlope)/
				             (lineDir.x * velocitySlope - lineDir.y);

			float maxDist = (lineEnd - lineStart).magnitude;

			if(lineDist <= maxDist && lineDist >= 0.0f)
			{
				endPointFraction = (lineStart.x + lineDist*lineDir.x - startPosition.x)/movementVector.x;
				if(endPointFraction < 1)
				{
					remainingMovement = movementVector * (1-endPointFraction);
					remainingMovement = Vector2.Dot(remainingMovement, lineDir) * lineDir;
				}
			}
		}
		else if(movementVector.y != 0.0f)
		{
			float velocitySlope = movementVector.x/movementVector.y;

			if((lineDir.y * velocitySlope - lineDir.x) == 0.0f)
				return -1.0f;

			float lineDist = (lineStart.x - startPosition.x + (startPosition.y - lineStart.y) * velocitySlope)/
				             (lineDir.y * velocitySlope - lineDir.x);
			
			float maxDist = (lineEnd - lineStart).magnitude;
			
			if(lineDist <= maxDist && lineDist >= 0.0f)
			{
				endPointFraction = (lineStart.y + lineDist*lineDir.y - startPosition.y)/movementVector.y;
				if(endPointFraction < 1)
				{
					remainingMovement = movementVector * (1-endPointFraction);
					remainingMovement = Vector2.Dot(remainingMovement, lineDir) * lineDir;
				}
			}
		}
		else
			endPointFraction = -1.0f;

		return endPointFraction;
	}

	static public float CollideWithCircle(Vector2 center, float radius, Vector2 startPosition, Vector2 movementVector, ref Vector2 remainingMovement)
	{
		if(movementVector.x == 0)
		{
			float temp = movementVector.x;
			movementVector.x = movementVector.y;
			movementVector.y = temp;
			
			temp = startPosition.x;
			startPosition.x = startPosition.y;
			startPosition.y = temp;
			
			temp = center.x;
			center.x = center.y;
			center.y = temp;
		}
		if(movementVector.x != 0)
		{
			float velocitySlope = movementVector.y/movementVector.x;
			float distanceToCenterY = startPosition.y - center.y;
			float D = distanceToCenterY - velocitySlope*startPosition.x;
			
			float p = 2*(D*velocitySlope - center.x)/(MyMath.Square(velocitySlope) + 1);
			float q = (MyMath.Square(D) - MyMath.Square(radius) + MyMath.Square(center.x))/(MyMath.Square(velocitySlope) + 1);
			
			float rootSquared = MyMath.Square(p/2.0f) - q;
			if(rootSquared < 0)
				return -1.0f;
			else
			{
				float endPointOneX = - p/2.0f + Mathf.Sqrt(rootSquared);
				float endPointFractionOne = (endPointOneX - startPosition.x)/movementVector.x;
			
				float endPointTwoX = - p/2.0f - Mathf.Sqrt(rootSquared);
				float endPointFractionTwo = (endPointTwoX - startPosition.x)/movementVector.x;
				
				if(endPointFractionOne < endPointFractionTwo && endPointFractionOne > 0 && endPointFractionOne < 1)
				{
					remainingMovement = movementVector * (1-endPointFractionOne);
					
					Vector2 endPoint = startPosition + endPointFractionOne*movementVector;
					Vector2 radiusVec = center - endPoint;
					Vector2 radiusVecOrth = new Vector2(1.0f, - radiusVec.x/radiusVec.y).normalized;
					
					remainingMovement = Vector2.Dot(remainingMovement, radiusVecOrth) * radiusVecOrth;
					
					return endPointFractionOne;
				}
				else if(endPointFractionTwo > 0 && endPointFractionTwo < 1)
				{
					remainingMovement = movementVector * (1-endPointFractionTwo);
					
					Vector2 endPoint = startPosition + endPointFractionTwo*movementVector;
					Vector2 radiusVec = center - endPoint;
					Vector2 radiusVecOrth = new Vector2(1.0f, - radiusVec.x/radiusVec.y).normalized;
					
					remainingMovement = Vector2.Dot(remainingMovement, radiusVecOrth) * radiusVecOrth;
					
					return endPointFractionTwo;
				}
			}
		}
		
		return -1.0f;
	}

}
