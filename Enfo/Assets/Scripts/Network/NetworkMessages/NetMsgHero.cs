using UnityEngine;
using System.Collections;

public class NetMsgHero : MonoBehaviour
{
	NetMsgPlayerPos heroPositionMessage = new NetMsgPlayerPos();

	// Use this for initialization
	void Start()
	{
		heroPositionMessage.Start();
	}

	// Update is called once per frame
	void Update()
	{
		heroPositionMessage.Update(transform.position);
	}
}
