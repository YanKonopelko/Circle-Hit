using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private bool isPlaying;
    public static LevelManager instance;
    private AudioSource _audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else if (instance != this)
            Destroy(transform.root.gameObject);


    }

    public void NextLevel()
    {
        var lvl = PlayerPrefs.GetInt("LastLevel")+1;
        PlayerPrefs.SetInt("LastLevel",lvl);    
        SceneManager.LoadScene(lvl%4+2);
    }

    public void Reload()
    {
        var lvl = PlayerPrefs.GetInt("LastLevel");
        SceneManager.LoadScene(lvl%4+2);
    }

}
