using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float enemySpeed;
	public float fireRate;
	
	private GameObject player;
	private Gun gun;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		gun = GetComponentInChildren<Gun> ();
	}

	void Update(){
				gun.Fire ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		float zrotation = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x ) * Mathf.Rad2Deg - 90;
		transform.eulerAngles = new Vector3 (0,0,zrotation);

		//GetComponent<Rigidbody2D>().AddForce (gameObject.transform.up * enemySpeed,ForceMode2D.Impulse);

		transform.position += transform.up * enemySpeed * Time.deltaTime;
	}
}
