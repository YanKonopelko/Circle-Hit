using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timeText;
    void Update()
    {
        timeText.text = GameSceneManager.instanse.remainingTime.ToString();
    }
}
