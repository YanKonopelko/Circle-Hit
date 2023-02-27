using UnityEngine;

public class AllowedPlacement : MonoBehaviour
{
    
    private void OnMouseEnter() =>InputManager.isOnAllowed = false;
    private void OnMouseExit() => InputManager.isOnAllowed = true;
    
}
