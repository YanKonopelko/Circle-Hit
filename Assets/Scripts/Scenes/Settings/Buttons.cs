using UnityEngine;
using UnityEngine.EventSystems;
public class Buttons : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData) =>SoundManager.Instance.PlaySound(SoundManager.Instance.ButtonSound);
}
