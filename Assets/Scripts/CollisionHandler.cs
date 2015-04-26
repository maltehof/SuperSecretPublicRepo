using UnityEngine;
using System.Collections;

public delegate void OtherCollisionHandler(CollisionAttributes collisionAttributes);

public class CollisionHandler : MonoBehaviour {
    public OtherCollisionHandler handleCollision;
}
