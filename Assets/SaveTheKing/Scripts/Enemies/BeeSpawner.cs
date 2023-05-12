using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{

    private const int beeCounter = 4;
    private int spawnedYet = 0;

    private const float spawnTime = 1.5f;
    
    [SerializeField] private Enemy beePrefab; 
    
    private 
        
    void Start()
    {
        GameSceneManager.instanse.onGameStart += SpawnBee;
        
    }

    private void SpawnBee()
    {
        if (spawnedYet < beeCounter)
            StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnTime);
        spawnedYet++;
        var bee = Instantiate(beePrefab,transform.position,quaternion.identity);
        if(spawnedYet>2)
            bee.SetTarget(GameSceneManager.instanse.pet[^1].transform);
        else
            bee.SetTarget(GameSceneManager.instanse.pet[0].transform);

        SpawnBee();
    }
    
}
