using UnityEngine;

public abstract class Order
{
	public bool isCompleted = false;
	public bool hasStarted = false;
	public Vector3 currentPosition = new Vector3();
	
	public abstract void Update();
}
