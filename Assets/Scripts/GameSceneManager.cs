using System;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static  GameSceneManager instance;

    public Action onGameStart;

    public bool isStarted;
    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    public void StartGame()
    {
        onGameStart.Invoke();
        instance.isStarted = true;
    }

}
