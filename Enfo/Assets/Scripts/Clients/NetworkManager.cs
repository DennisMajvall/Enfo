﻿using UnityEngine;
using UnityEngine.Networking;

public class NetworkReceiveMessage
{
	public int socket;
	public int connectionId;
	public int channelId;
	public byte[] recBuffer = new byte[1024];
	public int bufferSize = 1024;
	public int dataSize;
	public byte error;
}

public enum ServerToUse
{
	localhost,
	Dennis,
	DennisLANLaptop,

	None,
}

public class NetworkManager : MonoBehaviour
{
	const bool printMessageError = true;

	public ushort maxPacketSize = 500;
	public ServerToUse serverToUse;

	GlobalConfig globalConfig;
	ConnectionConfig connectionConfig;

	int reliableChannelId;
	//int unreliableChannelId;

	HostTopology topology;
	int socket;
	int port;
	string ipAddress;
	int maxNumConnections = 1;

	int myConnectionId;
	bool isBatchmode;

	NetworkReceiveMessage message;
	readonly string[] serverIPaddresses = { "127.0.0.1", "84.219.252.92", "192.168.1.243", "" };
	readonly int[] serverPorts = { 6111, 6111, 6110, 0 };

	void Start()
	{
		SetBatchModeOrNot();
		SetServerPortAndIP();

		if (isBatchmode) {
			maxNumConnections = 10;
		}

		message = new NetworkReceiveMessage();

		globalConfig = new GlobalConfig();
		NetworkTransport.Init(globalConfig);

		connectionConfig = new ConnectionConfig();
		reliableChannelId = connectionConfig.AddChannel(QosType.Reliable);
		//unreliableChannelId = config.AddChannel(QosType.Unreliable);

		topology = new HostTopology(connectionConfig, maxNumConnections);
		socket = NetworkTransport.AddHost(topology, port, ipAddress);
	}

	public void ConnectToHost()
	{
		myConnectionId = NetworkTransport.Connect(socket, ipAddress, port, 0, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
	}

	void Update()
	{
		if (!isBatchmode) {
			if (Input.GetKeyUp(KeyCode.C)) {
				ConnectToHost();
			}
			if (Input.GetKeyUp(KeyCode.D)) {
				Disconnect();
			}
			if (Input.GetKeyUp(KeyCode.S)) {
				byte[] msg = new byte[3];
				msg[0] = (byte)'h';
				msg[1] = (byte)'e';
				msg[2] = (byte)'j';
				Send(msg);
			}
		}

		ReceiveAndHandleData();
	}

	void ReceiveAndHandleData()
	{

		NetworkEventType receivedData = ReceiveFromHost();
		switch (receivedData) {
			case NetworkEventType.DataEvent:
				Debug.Log(((char)message.recBuffer[0]).ToString() + ((char)message.recBuffer[1]).ToString() + ((char)message.recBuffer[2]).ToString());
				break;
			case NetworkEventType.ConnectEvent:
				if (myConnectionId == message.connectionId)
					Debug.Log("my active connect request approved: " + message.connectionId.ToString());
				else
					Debug.Log("somebody else connect to me: " + message.connectionId.ToString());
				break;
			case NetworkEventType.DisconnectEvent:
				if (myConnectionId == message.connectionId)
					Debug.Log("cannot connect by some reason see error: " + message.connectionId.ToString());
				else
					Debug.Log("one of the established connection has been disconnected: " + message.connectionId.ToString());
				break;
		}
	}

	public void Disconnect()
	{
		NetworkTransport.Disconnect(socket, myConnectionId, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
	}

	public void Send(byte[] buffer)
	{
		NetworkTransport.Send(socket, myConnectionId, reliableChannelId, buffer, buffer.Length, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
	}

	public NetworkEventType Receive()
	{
		NetworkEventType result = NetworkTransport.Receive(out message.socket, out message.connectionId,
			out message.channelId, message.recBuffer, message.bufferSize, out message.dataSize, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
		return result;
	}

	public NetworkEventType ReceiveFromHost()
	{
		NetworkEventType result = NetworkTransport.ReceiveFromHost(socket, out message.connectionId,
			out message.channelId, message.recBuffer, message.bufferSize, out message.dataSize, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
		return result;
	}

	void PrintErrorMessage(int errorCode)
	{
		string errorMessage = "Network Error (" + errorCode + "): ";

		NetworkError error = (NetworkError)errorCode;
		switch (error) {
			case NetworkError.Ok: // Everything good so far.
				break;
			case NetworkError.WrongHost:
				Debug.LogWarning(errorMessage + "Host doesn't exist.");
				break;
			case NetworkError.WrongConnection:
				Debug.LogWarning(errorMessage + "Connection doesn't exist.");
				break;
			case NetworkError.WrongChannel:
				Debug.LogWarning(errorMessage + "Channel doesn't exist.");
				break;
			case NetworkError.NoResources:
				Debug.LogWarning(errorMessage + "No internal resources ro acomplish request.");
				break;
			case NetworkError.BadMessage:
				Debug.LogWarning(errorMessage + "Obsolete.");
				break;
			case NetworkError.Timeout:
				Debug.LogWarning(errorMessage + "Timeout happened.");
				break;
			case NetworkError.MessageToLong:
				Debug.LogWarning(errorMessage + "Sending message too long to fit internal buffers, or user doesn't present buffer with length enough to contain receiving message.");
				break;
			case NetworkError.WrongOperation:
				Debug.LogWarning(errorMessage + "Operation is not supported.");
				break;
			case NetworkError.VersionMismatch:
				Debug.LogWarning(errorMessage + "Different version of protocol on ends of connection.");
				break;
			case NetworkError.CRCMismatch:
				Debug.LogWarning(errorMessage + "Two ends of connection have different agreement about channels, channels QoS and network parameters.");
				break;
			case NetworkError.DNSFailure:
				Debug.LogWarning(errorMessage + "The address supplied to connect to was invalid or could not be resolved.");
				break;
			default:
				break;
		}
	}

	void SetBatchModeOrNot()
	{
		string[] args = System.Environment.GetCommandLineArgs();
		foreach (string s in args) {
			if (s.Equals("-batchmode") || s.Equals("-fakebatchmode")) {
				isBatchmode = true;
			}
		}
	}

	void SetServerPortAndIP()
	{
		ipAddress = serverIPaddresses[(int)serverToUse];
		port = serverPorts[(int)serverToUse];
	}
}
