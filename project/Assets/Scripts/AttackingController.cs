using UnityEngine;
using System.Collections;

public class AttackingController : MonoBehaviour {

	public Collider2D attackCollider;

	private Animator animation;
	private bool attacking = false;

	private float attackTimer = 0;
	private float attackCD = 0.2f;

	private bool inInventory =false;

	GameObject player;
	PlayerController playerController;


	void Awake () {
		animation = GetComponentInParent<Animator> ();
		attackCollider.enabled = false;
	}

	void Start(){
		player = GameObject.Find("Player");
		playerController = player.GetComponent<PlayerController> ();
	}

	void Update () {
		//if the player is trying to attack, check if the sword is in the inventory
		if (Input.GetButtonDown ("Attack") && !attacking && playerController.InInventory("Sword")) {
			attacking = true;
			attackTimer = attackCD;
			attackCollider.enabled = true;	
		}

		if (attacking) {
			if (attackTimer > 0) {					
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackCollider.enabled = false;
			}			
		}
		animation.SetBool("Attacking", attacking);
	}		
}
