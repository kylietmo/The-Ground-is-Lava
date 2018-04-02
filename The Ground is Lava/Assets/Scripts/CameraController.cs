using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public float smoothTimeY;
	public float smoothTimeX;
	public GameObject player;
	public bool bounds;
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	private Vector2 velocity;
	private Transform leftBorder;
	private Transform rightBorder;



	void Start () {
		player = GameObject.FindGameObjectWithTag ("player");
		leftBorder = GameObject.FindGameObjectWithTag ("LeftBorder").transform;
		rightBorder = GameObject.FindGameObjectWithTag ("RightBorder").transform;
	}
	

	void FixedUpdate () {
		//create smooth movement of camera that follows the player
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
		transform.position = new Vector3 (posX, posY, transform.position.z);

		//bind the camera so that it doesn't go beyond the scope of the platform
		if (bounds) {
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, leftBorder.position.x + 17.5f, rightBorder.position.x - 17.5f), 
				Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y), 
				Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
		}
	}
}
