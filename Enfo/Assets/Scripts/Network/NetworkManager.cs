using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkReceiveMessage
{
	public int socket;
	public int connectionId;
	public int channelId;
	public int bufferSize = 1024;
	public int dataSize;
	public byte error;
}

public class NetworkManager
{
	readonly string[] serverIPaddresses = { "127.0.0.1", "84.219.252.92", "192.168.1.248", "" };
	readonly int[] serverPorts = { 6111, 6111, 6110, 0 };

	public static NetworkManager Get()
	{
		if (myInstance == null)
			myInstance = new NetworkManager();

		return myInstance;
	}

	static NetworkManager myInstance;
	const bool printMessageError = true;

	ServerToUse serverToUse;
	GlobalConfig globalConfig;
	ConnectionConfig connectionConfig;

	HostTopology topology;
	int socket;
	int port;
	string ipAddress;
	int maxNumConnections;

	int myConnectionId = -1;
	bool isServer;

	public byte[] receiveBuffer = new byte[1024];
	List<NetMsg>[] messagesToSend = new List<NetMsg>[(int)NetMsgPriority.COUNT];
	int[] channelIDs = new int[(int)NetMsgPriority.COUNT];

	NetworkReceiveMessage message;

	private NetworkManager() {
		for (int i = 0; i < messagesToSend.Length; ++i) {
			messagesToSend[i] = new List<NetMsg>();
		}
	}

	public void Start(ServerToUse serverToUse)
	{
		this.serverToUse = serverToUse;
		SetBatchModeOrNot();
		SetServerOrClient();

		message = new NetworkReceiveMessage();

		globalConfig = new GlobalConfig();
		NetworkTransport.Init(globalConfig);

		connectionConfig = new ConnectionConfig();
		channelIDs[(int)NetMsgPriority.ReliableTCP] = connectionConfig.AddChannel(QosType.Reliable);
		channelIDs[(int)NetMsgPriority.ReliableUDP] = connectionConfig.AddChannel(QosType.AllCostDelivery);
		channelIDs[(int)NetMsgPriority.UDP] = connectionConfig.AddChannel(QosType.Unreliable);
		channelIDs[(int)NetMsgPriority.UnimportantSeq] = connectionConfig.AddChannel(QosType.UnreliableSequenced);

		topology = new HostTopology(connectionConfig, maxNumConnections);
		socket = NetworkTransport.AddHost(topology, port, ipAddress);
	}

	public void Update()
	{
		if (!isServer) {
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
				Send(msg, NetMsgPriority.ReliableTCP);
			}
		} else {
			for (int i = 0; i < messagesToSend.Length; ++i) {
				foreach (NetMsg msg in messagesToSend[i]) {
					Send(msg.GetRawData(), msg.Priority);
				}
			}
		}

		ReceiveAndHandleData();
	}

	void ReceiveAndHandleData()
	{

		NetworkEventType receivedData = ReceiveFromHost();
		switch (receivedData) {
			case NetworkEventType.DataEvent:
				Debug.Log(((char)receiveBuffer[0]).ToString() + ((char)receiveBuffer[1]).ToString() + ((char)receiveBuffer[2]).ToString());
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

	public void ConnectToHost()
	{
		myConnectionId = NetworkTransport.Connect(socket, ipAddress, port, 0, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
	}

	public void Disconnect()
	{
		NetworkTransport.Disconnect(socket, myConnectionId, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
	}

	public void Send(byte[] buffer, NetMsgPriority channelID)
	{
		NetworkTransport.Send(socket, myConnectionId, (int)channelID, buffer, buffer.Length, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
	}

	public void PrepareToSend(NetMsg msg)
	{
		messagesToSend[(int)msg.Priority].Add(msg);
	}

	public NetworkEventType Receive()
	{
		NetworkEventType result = NetworkTransport.Receive(out message.socket, out message.connectionId,
			out message.channelId, receiveBuffer, message.bufferSize, out message.dataSize, out message.error);
		if (printMessageError)
			PrintErrorMessage(message.error);
		return result;
	}

	public NetworkEventType ReceiveFromHost()
	{
		NetworkEventType result = NetworkTransport.ReceiveFromHost(socket, out message.connectionId,
			out message.channelId, receiveBuffer, message.bufferSize, out message.dataSize, out message.error);
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
				isServer = true;
			}
		}
	}

	void SetServerOrClient()
	{
		
		if (isServer) {
			maxNumConnections = 10;
			ipAddress = null;
			port = 0;
		} else {
			maxNumConnections = 1;
			ipAddress = serverIPaddresses[(int)serverToUse];
			port = serverPorts[(int)serverToUse];
		}
	}
}
