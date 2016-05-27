using UnityEngine;
using System.Collections.Generic;

public class Teams : MonoBehaviour {

	public GameObject[] WestTeam = new GameObject[10];
	public GameObject[] EastTeam = new GameObject[10];

	/**
	 * west = true to count West team's members, false to count East's
	 */
	public int countTeam(bool west) {
		GameObject[] team;
		int count = 0;

		if (west) {
			team = WestTeam;
		} else {
			team = EastTeam;
		}

		foreach (GameObject member in team) {
			if (member) {
				count++;
			}
		}
		return count;
	}
}
