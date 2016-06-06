using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	[Range(5.0f, 100.0f)]
	public float Speed = 15f;

	public GameObject HeroToFollow;

	bool isFollowingHero;
	GameObject cameraPos;
	Vector3 delta;

	// Use this for initialization
	void Start()
	{
		cameraPos = GameObject.Find("CameraPos");
	}

	void UpdateArrowKeys()
	{
		if (Input.GetKey(KeyCode.RightArrow))
			delta.x += 1;
		if (Input.GetKey(KeyCode.LeftArrow))
			delta.x -= 1;
		if (Input.GetKey(KeyCode.UpArrow))
			delta.z += 1;
		if (Input.GetKey(KeyCode.DownArrow))
			delta.z -= 1;
	}

	void UpdateFollowKey()
	{
		if (Input.GetKeyDown("space"))
			isFollowingHero = true;
		else if (Input.GetKeyUp("space"))
			isFollowingHero = false;
	}

	void UpdateKeys()
	{
		UpdateArrowKeys();
		UpdateFollowKey();
	}

	void UpdateMouse()
	{
	}

	// Update is called once per frame
	void Update()
	{
		// Reset
		delta = Vector3.zero;

		// Detect changes
		UpdateKeys();
		UpdateMouse();

		// Apply changes
		if (isFollowingHero) {
			const float zOffset = 5f;

			Vector3 followPos = HeroToFollow.transform.position;
			followPos.y = cameraPos.transform.position.y;
			followPos.z -= zOffset;

			cameraPos.transform.position = followPos;
		} else {
			if (delta != Vector3.zero) {
				delta *= Speed * Time.deltaTime;
				cameraPos.transform.position += delta;
			}
		}


	}
}
