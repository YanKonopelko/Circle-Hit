using UnityEngine;
using UnityEngine.EventSystems;

public class Sliders : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.SliderSound);
    }
}