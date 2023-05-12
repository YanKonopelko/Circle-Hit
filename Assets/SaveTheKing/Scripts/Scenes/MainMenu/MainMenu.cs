using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        YandexGame.Instance._FullscreenShow();
    }

    public void StartLastLevel()
    {
        LevelManager.instance.Reload();
    }

    public void ToSettings()
    {
        SceneManager.LoadScene(1);
    }
    
    
}
