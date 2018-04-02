using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int damage = 1;
	public int health = 1;

	private int playerDamage;

	GameObject player;
	PlayerController playerController;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerController = player.GetComponent<PlayerController> ();
		playerDamage = playerController.damage;
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag.Equals("Weapon")) {
			TakeDamage (playerDamage);
		}

		if (other.gameObject.tag.Equals("player")) {
			DamagePlayer (damage);
		}
	}

	/*
	 * Tells the player how many lives/hearts to lose.
	 * @param dmg number of lives/hearts player must lose
	 */
	void DamagePlayer(int dmg){

		playerController.LoseLife (dmg);
	}

	/*
	 * Decrements the health of this enemy.
	 * @param dmg number of lives/hearts enemy must lose
	 */
	void TakeDamage(int dmg){
		health -= dmg;
		if (health <= 0) {
			gameObject.SetActive (false);
		} else{
			//TODO: eventually make animation for taking damage
		}
	}
}
