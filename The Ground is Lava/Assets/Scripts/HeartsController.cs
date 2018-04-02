using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartsController : MonoBehaviour {

	public Sprite[] heartSprites;

	public Image heartUI;

	private PlayerController playerController;


	void Start () {
		playerController = GameObject.FindGameObjectWithTag ("player").GetComponent<PlayerController> ();
	}
	

	void Update () {
		
		//update the number of hearts showing
		heartUI.sprite = heartSprites [playerController.maxNumLives - playerController.curNumLives];
			
	}
}
