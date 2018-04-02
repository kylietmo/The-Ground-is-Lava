using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	private Transform pauseMenu;
	private bool paused;

	void Start () {

	    pauseMenu = transform.FindChild ("Pause Menu");
		pauseMenu.gameObject.SetActive (false);
		paused = false;
	
	}

	void Update () {

		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
		}

		pauseMenu.gameObject.SetActive (paused);

		if (paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void Resume(){
		paused = false;
	}

	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
