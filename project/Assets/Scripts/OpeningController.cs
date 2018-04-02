using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {

	private Transform opening;

	void Start() {
		opening = transform.FindChild ("Opening");	
	}
		
	//called when start button is pressed - disables opening canvas
	public void startGame(){
		opening.gameObject.SetActive (false);
	}
}
