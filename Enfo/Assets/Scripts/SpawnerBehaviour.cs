using UnityEngine;
using System.Collections.Generic;

public class SpawnerBehaviour : MonoBehaviour
{
	public bool activeTimer = true;
	public List<GameObject> waveGameobjects;
	public SpikedTree lol;
	public int current_wave = 0;

	const int total_units = 21;
	[Range(0, total_units)]
	public int num_spawned = 0;

	public float time_until_spawn = 5f;

	float interval_burst = 3;
	float interval_individual = 1;
	float interval_wave = 10;
	

	
	void Start()
	{
	}

	void SpawnUnit(int spawn_pos_index)
	{
		Vector3 spawn_pos = transform.position;
		const float spawn_dist_offset = 2f;
		if (spawn_pos_index == 0) {
			spawn_pos.x -= spawn_dist_offset;
		} else if (spawn_pos_index == 2) {
			spawn_pos.x += spawn_dist_offset;
		}

		Instantiate(waveGameobjects[current_wave], spawn_pos, transform.rotation);
		++num_spawned;
	}

	// Update is called once per frame
	void Update()
	{
		if (activeTimer) {
			time_until_spawn -= Time.deltaTime;
			if (num_spawned < total_units) {
				int spawn_pos_index = num_spawned % 3;
				if (time_until_spawn <= 0f) {
					SpawnUnit(spawn_pos_index);

					spawn_pos_index = num_spawned % 3;
					if (spawn_pos_index == 0) {
						time_until_spawn += interval_burst;
					} else {
						time_until_spawn += interval_individual;
					}
				}
			} else {
				time_until_spawn += interval_wave;
				num_spawned = 0;
				if (current_wave + 1 < waveGameobjects.Count) {
					++current_wave;
				}
			}
		}
	}
}
