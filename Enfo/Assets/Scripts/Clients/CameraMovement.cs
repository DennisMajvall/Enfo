using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	[Range(5.0f, 100.0f)]
	public float speed = 15f;


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

	void UpdateKeys()
	{
		UpdateArrowKeys();
	}

	void UpdateMouse()
	{
	}

	// Update is called once per frame
	void Update()
	{
		delta = Vector3.zero;

		UpdateKeys();
		UpdateMouse();

		if (delta != Vector3.zero) {
			delta *= speed * Time.deltaTime;
			cameraPos.transform.position += delta;
		}
	}
}
