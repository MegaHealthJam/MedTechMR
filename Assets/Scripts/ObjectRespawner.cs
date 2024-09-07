using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns an interactable object at the location of the object respawner
/// </summary>
public class ObjectRespawner : MonoBehaviour
{
	[Tooltip("The object you want to spawn")]
    public GameObject objectToRespawn;

	/// <summary>
	/// 
	/// </summary>
	/// <returns>The spawned object</returns>
    public GameObject RespawnObject() {
		return Instantiate(objectToRespawn, this.transform.position, this.transform.rotation);
	}
}
