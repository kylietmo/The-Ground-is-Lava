using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float maxSpeed;
	public float jumpPower;

	public int curNumLives;
	public int maxNumLives = 3;

	public int inventoryMax;
	public List <GameObject> inventory = new List<GameObject>();

	private Rigidbody2D rb;
	private Animator animation;
	private Vector3 startPosition;

	private	 bool inInventory;

	public Image[] inventorySprites;
	public Sprite swordSprite;	

	public int damage = 1;



	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		animation = GetComponent<Animator> ();
		curNumLives = maxNumLives;
		startPosition = gameObject.transform.position;
		inInventory = false;
	}

	void Update(){
		animation.SetFloat ("Speed", Mathf.Abs (Input.GetAxis ("Horizontal")));

		//get movement from arrow keys
		if (Input.GetAxis ("Horizontal") < -0.1f) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3 (1, 1, 1);
		}

		float moveHorizontal = Input.GetAxis ("Horizontal");

		rb.AddForce ((Vector2.right * moveHorizontal) * speed);

		if (Input.GetButtonDown ("Jump") && animation.GetBool("Grounded") == true) {
			rb.AddForce (Vector2.up  * jumpPower);
		}

		//dealing with a maximum speed
		if (rb.velocity.x < -maxSpeed) {
			rb.velocity = new Vector2 (-maxSpeed, rb.velocity.y);
		}

		if (rb.velocity.x > maxSpeed) {
			rb.velocity = new Vector2 (maxSpeed, rb.velocity.y);
		}

		if (curNumLives > maxNumLives) {
			curNumLives = maxNumLives;
		}

		if (Input.GetButtonDown ("Attack")) {
			if (InInventory("Sword")){
				animation.SetBool ("Attacking", true);
				animation.Play ("player_swordAttack");
				animation.SetBool ("Attacking", false);
				inInventory = false;
			}
		}
	}	
		
		
	//checking to see if we are on the ground
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag.Equals("Ground") || other.gameObject.tag.Equals("Platform")) {
			animation.SetBool ("Grounded", true);
		}
	}
		
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag.Equals("Weapon")) {
			string name = other.gameObject.name;
			other.gameObject.SetActive (false);
			AddToInventory (other.gameObject);
			if (name.Equals("Sword")) {
				inventorySprites [0].sprite = swordSprite;
			}
		}
	}

	//checking to see if we are leaving the ground
	void OnCollisionExit2D(Collision2D collider){
		if (collider.gameObject.tag.Equals("Ground") || collider.gameObject.tag.Equals("Platform")) {
			animation.SetBool ("Grounded", false);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		//falling into the pit
		if (other.gameObject.tag.Equals("Lava")) {
			gameObject.SetActive (false);
			LoseOneLife();
		}
	}

	/*
	 * Tries to add specified item to inventory.
	 * @param item any GameObject
	 */
	public void AddToInventory(GameObject item){
		if (inventory.Count != inventoryMax) {
			inventory.Add (item);
		} else {
			print ("No room in inventory!");
		}
	}
		
	/*
	 * Tries to remove specified item from inventory.
	 * @param item any GameObject
	 */
	public void RemoveFromInventory(GameObject item){
		if (inventory.Contains (item)) {
			inventory.Remove (item);
		} else {
			print ("Item is not in inventory!");
		}
	}

	/*
	 * Checks if there are any items with the passed in name in the inventory.
	 * @param itemName a string representing the name of a GameObject
	 */
	public bool InInventory(string itemName){
		for (int i = 0; i < inventory.Count; i++) {
			if (inventory [i] != null && inventory [i].name.Equals (itemName)) {
				return true;
			}
		}
		return false;
	}

	/*
	 * Decrements the number of lives/hearts by 1 and restarts the level.
	 * If the player has no more lives left, they die.
	 */
	private void LoseOneLife(){
		if (curNumLives >= 1) {
			curNumLives--;
			RestartLevel ();
		} else {
			print ("You've died!");
			Die ();
		}

	}

	/*
	 * Decrements the number of lives/hearts by the specified damage amount.
	 * If the player has no more lives left, they die.
	 *  @param dmg integer representing how many lives/hearts should be lost
	 */
	public void LoseLife(int dmg){
		if (curNumLives >= 1) {
			curNumLives-= dmg;
		} else {
			print ("You've died");
			Die ();
		}
		
	}

	/*
	 * Re-laods the scene (for now)
	 */
	public void Die(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}

	/*
	 * Resets the player at the start of the leve.
	 */
	public void RestartLevel(){
		gameObject.SetActive (true);
		gameObject.transform.position = startPosition;	
	}
		
}
