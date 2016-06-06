using UnityEngine;
using UnityEngine.Networking;


public enum ServerToUse
{
	localhost,
	Dennis,
	DennisLANLaptop,

	None,
}

public class NetworkManagerComponent : MonoBehaviour
{
	public ServerToUse serverToUse;

	void Start()
	{
		NetworkManager.Get().Start(serverToUse);
	}

	void Update()
	{
		NetworkManager.Get().Update();
	}
}
