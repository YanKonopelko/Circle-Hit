using UnityEngine;
using UnityEngine.SceneManagement;
using YandexMobileAds;
using YandexMobileAds.Base;
public class MainMenu : MonoBehaviour
{
    
    private Banner banner;
    private Interstitial interstitial;

    private void Start()
    {
        RequestInterstitial();
        ShowInterstitial();
        AdManager.Instance.FullScreen();
    }

    public void StartLastLevel()
    {
        LevelManager.instance.Reload();
    }

    public void ToSettings()
    {
        SceneManager.LoadScene(1);
    }

    private void RequestInterstitial()
    {
        string adUnitId = "R-M-2211699-2";

        interstitial = new Interstitial(adUnitId);
        
        AdRequest request = new AdRequest.Builder().Build();
        var a = new AdRequest.Builder();
        a.WithAdRequest(request);
        
        interstitial.LoadAd(a.Build());
        Debug.Log(interstitial.IsLoaded());
    }
    private void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial is not ready yet");
        }
    }
    
}
