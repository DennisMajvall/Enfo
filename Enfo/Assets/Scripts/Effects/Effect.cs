using UnityEngine;

public class Effect : MonoBehaviour
{
	readonly public bool EnabledInspector;

	public const int NumLevels = 10;
	public int Level;

	public Effect()
	{
		enabled = false;
	}

	public void ApplyEffect(int level)
	{
		RemoveEffect();

		if (!enabled && level != 0) {
			enabled = true;
			OnApplyEffect(level); // Override this method in your inherited class
		}

		Level = level;
	}

	public void RemoveEffect()
	{
		if (enabled) {
			enabled = false;
			OnRemoveEffect(); // Override this method in your inherited class
			Level = 0;
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
