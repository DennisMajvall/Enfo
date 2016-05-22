using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	[Range(5.0f, 100.0f)]
	public float speed = 15f;
    public GameObject heroToFollow;

    bool isSpaceDown = false;
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

        if (Input.GetKeyDown("space"))
        {
            isSpaceDown = true;
        }

        if (Input.GetKeyUp("space"))
        {
            isSpaceDown = false;
        }


        if (isSpaceDown)
        {
            delta = Vector3.zero;
            delta.x = heroToFollow.transform.position.x;
            delta.y = cameraPos.transform.position.y;
            delta.z = heroToFollow.transform.position.z - 5;
            cameraPos.transform.position = delta;
        }
        else
        {
            delta = Vector3.zero;

            UpdateKeys();
            UpdateMouse();

            if (delta != Vector3.zero)
            {
                delta *= speed * Time.deltaTime;
                cameraPos.transform.position += delta;
            }
        }


	}
}
