using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateWorldSpawner : MonoBehaviour
{
    public Crate crate;

    private void Start()
    {
        CrateWorld.SpawnCrateWorld(transform.position, crate);
        Destroy(gameObject);
    }
}
