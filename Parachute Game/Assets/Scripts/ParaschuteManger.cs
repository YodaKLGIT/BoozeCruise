using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteManager : MonoBehaviour
{
    public GameObject parachutePrefab;
    public ParticleSystem ParachuteDieSplash;

    public Transform spawnPoint;
    public GameObject floor;
    public GameObject player;

    public int maxParachutes = 4;
    public float spawnDelay = 1f;
    public float spawnWidth = 19f; // how far left or right they can spawn

    private List<GameObject> activeParachutes = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Coroutine to spawn parachutes at intervals
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            activeParachutes.RemoveAll(p => p == null);

            if (activeParachutes.Count < maxParachutes)
            {
                SpawnParachute();
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Spawns a parachute at a random position within the spawn width
    void SpawnParachute()
    {
        float randomX = Random.Range(-spawnWidth / 2f, spawnWidth / 2f);
        Vector3 spawnPos = new Vector3(spawnPoint.position.x + randomX, spawnPoint.position.y, 0);

        GameObject parachute = Instantiate(parachutePrefab, spawnPos, Quaternion.identity);
        activeParachutes.Add(parachute);
    }

    // hitFloor = true only if it hit the floor
    public void RemoveParachute(GameObject parachute, bool hitFloor)
    {
        if (parachute == null) return;

        // Only spawn splash effect if it hit the floor
        if (hitFloor && ParachuteDieSplash != null)
        {
            ParticleSystem splash = Instantiate(ParachuteDieSplash, parachute.transform.position, Quaternion.identity);
            Destroy(splash.gameObject, 2f);
        }

        activeParachutes.Remove(parachute);
        Destroy(parachute);
    }
}
