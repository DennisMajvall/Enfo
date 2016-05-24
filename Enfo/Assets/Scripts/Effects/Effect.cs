using UnityEngine;

public class Effect : MonoBehaviour
{
	public const int NumLevels = 10;
	protected int Level;
	protected bool IsApplied;

	public void ApplyEffect(int level = 0)
	{
		if (NumLevels > 1 && level == Level)
			return;

		RemoveEffect();
		if (NumLevels > 1 && level < 1 || level > NumLevels)
			return;
		
		Level = level;
		OnApplyEffect(level); // Override this method in your inherited class
		IsApplied = true;
	}

	public void RemoveEffect()
	{
		if (IsApplied) {
			OnRemoveEffect(); // Override this method in your inherited class
			IsApplied = false;
		}
	}

	// Override this method in your inherited class
	protected virtual void OnApplyEffect(int level = 0)
	{
		Debug.LogError("Effect.OnApplyEffect(int) must be overriden by the inherited class.");
		throw new System.NotImplementedException();
	}

	// Override this method in your inherited class
	protected virtual void OnRemoveEffect()
	{
		Debug.LogError("Effect.OnRemoveEffect() must be overriden by the inherited class.");
		throw new System.NotImplementedException();
	}
}
