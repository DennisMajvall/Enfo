using UnityEngine;
using System.Collections;

public class NetMsgPlayerPos : NetMsg
{
	Vector3 cachedPosition;

	// Use this for initialization
	public void Start()
	{
		ResizeData(12);
	}
	

	// Update is called once per frame
	public void Update(Vector3 newPosition)
	{
		if (newPosition != cachedPosition) {
			WriteVector3(newPosition);
			Send();
			cachedPosition = newPosition;
		}
	}
}
