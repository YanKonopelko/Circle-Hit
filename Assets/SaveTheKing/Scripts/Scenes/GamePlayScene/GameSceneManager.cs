using System;
using System.Collections;
using NavMeshPlus.Components;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoCache
{
    public static GameSceneManager instanse;
    public Action onGameStart;
    public Action<bool> onGameEnd;
    public Pet[] pet;
    private NavMeshSurface Surface2D;

    public int remainingTime = 7;
    private void Awake()
    {
        InkManager.inkAmount = 200;
        instanse = this;
        Time.timeScale = 0;
        pet = FindObjectsOfType<Pet>();
        Surface2D = FindObjectOfType<NavMeshSurface>();
        Surface2D.BuildNavMeshAsync();
        onGameEnd += Stop;
    }

    public void StartGame()
    {
        onGameStart.Invoke();
        Time.timeScale = 1;
        StartCoroutine(TimeCheck());
        NavUptdate();
    }
    
    private IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(1);
        remainingTime--;
        if (remainingTime == 0)
            Win();
        StartCoroutine(TimeCheck());
    }

    private void Win()
    {
        onGameEnd(true);
    }
    public void NavUptdate()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);        
    }

    public void Lose()
    {
        onGameEnd(false);
    }

    public void ToNextLevel()
    {
        LevelManager.instance.NextLevel();
    }
    public void Reload()
    {
        LevelManager.instance.Reload();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SkipLevel()
    {
        LevelManager.instance.SkipLevel();
    }

    private void Stop(bool a)
    {
        Time.timeScale = 0;
    }
}
