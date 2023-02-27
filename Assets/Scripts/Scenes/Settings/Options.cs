using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public Slider vfxSlider;
    public Slider musicSlider;
    private void Start()
    {
        vfxSlider.value = (PlayerPrefs.HasKey("VFX_VOLUME")) ? PlayerPrefs.GetFloat("VFX_VOLUME") : SoundManager.Instance.volume;
        musicSlider.value = (PlayerPrefs.HasKey("MUSIC_VOLUME")) ? PlayerPrefs.GetFloat("MUSIC_VOLUME") : MusicManager.Instance.volume;
    }
    public void BackToMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void SliderVFXChanged(Slider slider)
    {
        SoundManager.Instance.ChangeVolume(slider.value);
        PlayerPrefs.SetFloat("VFX_VOLUME", slider.value);
    }

    public void SliderMusicChanged(Slider slider)
    {
        MusicManager.Instance.ChangeVolume(slider.value);
        PlayerPrefs.SetFloat("MUSIC_VOLUME", slider.value);

    }
}