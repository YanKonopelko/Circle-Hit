using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject lineCreator;
    GameObject currentCreator;

    private bool isStarted;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && InkManager.inkAmount > 0 && !GameSceneManager.instance.isStarted) 
            CreateLineCreator(InkManager.currentColor);
        else if (Input.GetMouseButtonUp(0)) GameSceneManager.instance.StartGame();
            
        
    }

    private void CreateLineCreator(int color)
    {
        currentCreator = Instantiate(lineCreator);
        currentCreator.transform.SetParent(transform);
        
        currentCreator.GetComponent<DrowLine>().startPos = GetWorldPosition();
        currentCreator.GetComponent<DrowLine>().colorNum = color;

    }
    public static Vector2 GetWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 mouseCoor = new Vector3(mousePosition.x, mousePosition.y, 1);
        return Camera.main.ScreenToWorldPoint(mouseCoor);
    }
}