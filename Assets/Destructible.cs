using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {

	private int maxHealth;
	public  int health;

	private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
		maxHealth = 100;
		health = maxHealth;
	}
	
}
