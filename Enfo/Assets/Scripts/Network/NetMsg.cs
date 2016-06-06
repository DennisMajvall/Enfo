using UnityEngine;

public enum NetMsgPriority
{
	ReliableTCP,	// e.g Hero Health, spells being activated
	ReliableUDP,	// e.g Hero Positions
	UDP,			// e.g Monster positions etc
	UnimportantSeq,	// e.g VoIP

	COUNT			// Last element
}

public class NetMsg
{
	public NetMsgPriority Priority;

	protected int dataSize = 4;
	byte[] data = null;

	public byte[] GetRawData() { return data; }

	protected void ResizeData(int newSize = -1)
	{
		if (newSize == -1)
			newSize = dataSize;

		dataSize = newSize;
		data = new byte[newSize];
	}

	protected void WriteInt(int data)
	{
		Debug.Assert(dataSize >= sizeof(int), "NetMsg.WriteInt failed, dataSize is not enough.");
		this.data = System.BitConverter.GetBytes(data);
	}

	protected void WriteBool(bool data)
	{
		Debug.Assert(dataSize >= sizeof(bool), "NetMsg.WriteBool failed, dataSize is not enough.");
		this.data = System.BitConverter.GetBytes(data);
	}

	protected void WriteFloat(float data)
	{
		Debug.Assert(dataSize >= sizeof(float), "NetMsg.WriteFloat failed, dataSize is not enough.");
		this.data = System.BitConverter.GetBytes(data);
	}

	protected void WriteVector2(Vector2 data)
	{
		Debug.Assert(dataSize >= sizeof(float) * 2, "NetMsg.WriteVector2 failed, dataSize is not enough.");

		for (int i = 0; i < 3; ++i) {
			byte[] temp = System.BitConverter.GetBytes(data[i]);
			System.Array.ConstrainedCopy(temp, 0, this.data, i * sizeof(float), sizeof(float));
		}
	}

	protected void WriteVector3(Vector3 data)
	{
		Debug.Assert(dataSize >= sizeof(float) * 3, "NetMsg.WriteVector3 failed, dataSize is not enough.");

		for (int i = 0; i < 3; ++i) {
			byte[] temp = System.BitConverter.GetBytes(data[i]);
			System.Array.ConstrainedCopy(temp, 0, this.data, i * sizeof(float), sizeof(float));
		}
	}

	protected void WriteQuaternion(Quaternion data)
	{
		Debug.Assert(dataSize >= sizeof(float) * 4, "NetMsg.WriteQuaternion failed, dataSize is not enough.");

		for (int i = 0; i < 4; ++i) {
			byte[] temp = System.BitConverter.GetBytes(data[i]);
			System.Array.ConstrainedCopy(temp, 0, this.data, i * sizeof(float), sizeof(float));
		}
	}

	public void Send()
	{
		NetworkManager.Get().PrepareToSend(this);
	}
}
