using System;
using System.Collections;
using NavMeshPlus.Components;
using NTC.Global.Cache;
using UnityEngine;

public class GameSceneManager : MonoCache
{
    public static GameSceneManager instanse;
    public Action onGameStart;

    public Pet pet;
    private NavMeshSurface Surface2D;

    public int remainingTime = 7;
    private void Awake()
    {
        instanse = this;
        pet = FindObjectOfType<Pet>();
        Surface2D = FindObjectOfType<NavMeshSurface>();
        Surface2D.BuildNavMeshAsync();
    }

    public void StartGame()
    {
        onGameStart.Invoke();
        StartCoroutine(TimeCheck());
        StartCoroutine(NavUptdate());
    }
    
    private IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(1);
        remainingTime--;
        Debug.Log("Remaining Ti0me:" +remainingTime);
        if (remainingTime == 0)
            Win();
        StartCoroutine(TimeCheck());
    }

    private void Win()
    {
        LevelManager.instance.NextLevel();
    }
    private IEnumerator NavUptdate()
    {
        yield return new WaitForSeconds(0.5f);
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);        
        StartCoroutine(NavUptdate());
    }
}
