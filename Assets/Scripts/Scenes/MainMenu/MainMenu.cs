using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YandexMobileAds;
using YandexMobileAds.Base;
public class MainMenu : MonoBehaviour
{
    
    private Banner banner;

    private void Start()
    {
        RequestBanner();
    }

    public void StartLastLevel()
    {
        LevelManager.instance.Reload();
    }

    public void ToSettings()
    {
        SceneManager.LoadScene(1);
    }
    private void RequestBanner()
    {
        string adUnitId = "demo-banner-yandex";
        
        AdSize bannerMaxSize = AdSize.FlexibleSize(GetScreenWidthDp(), 100);
        
        banner = new Banner(adUnitId, bannerMaxSize, AdPosition.BottomCenter);
    }
    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return ScreenUtils.ConvertPixelsToDp(screenWidth);
    }
}
