using UnityEngine;
using System.Collections;

public class GoldContainer : MonoBehaviour {

	[SerializeField] float gold = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeGold(float delta) {
		gold += delta;
	}

	public float GetGold() {
		return gold;
	}
}
