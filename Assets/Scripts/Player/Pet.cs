using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private Sprite[] Skins;
    [SerializeField] private Animation anim;
    void Awake()
    {
        var sprite = GetComponent<Sprite>();
        if(PlayerPrefs.HasKey("CurrentSkin"))
            sprite = Skins[PlayerPrefs.GetInt("CurrentSkin")];
    }
    
    public void Lose()
    {
        LevelManager.instance.Reload();
    }
}
