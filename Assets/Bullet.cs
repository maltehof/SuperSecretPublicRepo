using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public  Rigidbody2D	  bulletRigidbody;
	private Vector2       movementDirection;
	public  float         movementSpeed;
	public  int           maxBounces;
	private int           bounceCount;
	

	// Use this for initialization
	void Start () {
		//bulletRigidbody = GetComponent<Rigidbody2D>();
	}

	void Update(){
		Vector3 movement = movementDirection * movementSpeed;
		bulletRigidbody.velocity = movement;
		//transform.position += movement;
	}

	public void Initialize(){
		bulletRigidbody = GetComponent<Rigidbody2D>();
	}

	public void Fire(Vector2 direction)
	{
		movementDirection = direction;
		direction.Normalize();
	}


	void OnCollisionEnter2D(Collision2D collision){
		Debug.Log ("Trigger entered");
		Destructible destructibleComp = null;
		destructibleComp = collision.gameObject.GetComponent<Destructible>();
		if (destructibleComp != null) 
		{
			destructibleComp.health -= 10;
			if(destructibleComp.health <= 0)
				DestroyObject(destructibleComp.gameObject);

			Destroy(this.gameObject);
		}
		
		if (collision.transform.root.name == "BackgroundTile"){
			++bounceCount;
			if(bounceCount > maxBounces)
			{
				Destroy(gameObject);
			}
			
			Vector2 normal = collision.contacts[0].normal;
			movementDirection = movementDirection - 2 * Vector2.Dot(movementDirection, normal) * normal;
			
		}

	}

}
