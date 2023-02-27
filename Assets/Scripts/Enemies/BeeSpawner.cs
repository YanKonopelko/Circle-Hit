using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    private const int beeCounter = 3;
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
        
        //bee.SetTarget(GameSceneManager.instance.pet.transform);
        //bee.SetPriority(spawnedYet*5);
        SpawnBee();
    }
    
}
