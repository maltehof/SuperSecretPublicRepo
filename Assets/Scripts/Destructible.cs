using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {

	public int maxHealth;
	public int health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
}
