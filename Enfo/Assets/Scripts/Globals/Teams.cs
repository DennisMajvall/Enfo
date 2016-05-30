using UnityEngine;
using System.Collections.Generic;

public class Teams : MonoBehaviour {

	public GameObject[] WestTeam = new GameObject[10];
	public GameObject[] EastTeam = new GameObject[10];

	/**
	 * west = true to count West team's members, false to count East's
	 */
	public int CountTeam(bool west) {
		return GetTeamMembers(west).Count;
	}

	public List<GameObject> GetTeamMembers(bool west)
	{
		List<GameObject> result = new List<GameObject>();
		GameObject[] team = west ? WestTeam : EastTeam;
		
		foreach (GameObject member in team)
			if (member)
				result.Add(member);

		return result;
	}
}
