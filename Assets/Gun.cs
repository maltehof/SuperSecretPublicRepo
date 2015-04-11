using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Bullet originalBullet;
	public BoxCollider2D backgroundCollider;
	public float fireForce;
	public float fireDelay;
	private float timeScinceLastFire;

	// Use this for initialization
	void Start () {
		timeScinceLastFire = 100.0f;
		fireForce = 100.0f;
	}

	void Update(){
		timeScinceLastFire += Time.deltaTime;
	}
	
	public void Fire ()
	{
		if(timeScinceLastFire >= fireDelay){
			Bullet bullet = Instantiate (originalBullet, transform.position, transform.parent.rotation) as Bullet;
			bullet.Initialize();
			Vector2 fireDirection = transform.parent.rotation * new Vector2 (0, 1);
			//Vector2 bulletForce = fireDirection * fireForce;
			bullet.Fire(fireDirection);
			timeScinceLastFire = 0.0f;
		}
	}	
}
			