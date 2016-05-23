using UnityEngine;
using Pathfinding;

public class MoveOrder : Order
{
	Path path;
	int currentCheckpoint = 0;

	public MoveOrder(UnitStats stats, Vector3 currentPosition)
	{
		this.stats = stats;
		this.currentPosition = currentPosition;
	}

	public void SetNewPath(Path p)
	{
		path = p;
		isCompleted = false;
		currentCheckpoint = 0;

		if (!p.error)
			hasStarted = true;
		else
			hasStarted = false;
	}
	
	public override void Update()
	{
		if (!hasStarted || path == null || path.error || isCompleted)
			return;

		if (currentCheckpoint >= path.vectorPath.Count) {
			path = null;
			isCompleted = true;
			hasStarted = false;
			return;
		}
		
		float currentSpeed = stats.MovementSpeed * Time.deltaTime;

		if (currentSpeed == 0f)
			return;

		float distanceLeft = Vector3.Distance(currentPosition, path.vectorPath[currentCheckpoint]);
		bool canReachCheckpointNow = currentSpeed >= distanceLeft;

		if (canReachCheckpointNow) {
			currentSpeed *= (distanceLeft / currentSpeed);
		}

		Vector3 moveDelta = (path.vectorPath[currentCheckpoint] - currentPosition).normalized;
		moveDelta *= currentSpeed;
		currentPosition += moveDelta;
		
		if (canReachCheckpointNow) {
			++currentCheckpoint;
		} 
	}
}
