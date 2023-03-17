using System.Runtime.InteropServices;
using NTC.Global.System;

public class AdManager : Singleton<AdManager>
{
    [DllImport("__Internal")]
    private static extern void RewardedVideoExtern();
    [DllImport("__Internal")]
    private static extern void FullScreenExtern();
    // Start is called before the first frame update
    public void RewardedVideo()
    {
        RewardedVideoExtern();
    }

    public void FullScreen()
    {
        FullScreenExtern();
    }

    public void StickyBanner()
    {
        
    }
}
