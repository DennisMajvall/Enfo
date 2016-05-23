using UnityEngine;
using System.Collections;

public abstract class Effect : MonoBehaviour
{
	protected int Level;

	public abstract void ApplyEffect(int Level = 0);

	public abstract void RemoveEffect();

	public abstract void SetLevel(int level);
}
