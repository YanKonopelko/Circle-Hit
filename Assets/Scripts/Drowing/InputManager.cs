using NTC.Global.Cache;
using UnityEngine;

public class InputManager : MonoCache
{
    [SerializeField] private GameObject lineCreator;
    GameObject currentCreator;

    static private bool isMouse;

    public static bool isOnAllowed = true;
    protected override void Run()
    {
        if ( InkManager.inkAmount > 0  && isOnAllowed)
        {
            if (Input.GetMouseButtonDown(0))
                isMouse = true;
            else if(Input.touchCount>0)
                isMouse = false;
            else 
                return;
            CreateLineCreator(InkManager.currentColor);
        }    
    }

    private void CreateLineCreator(int color)
    {
        currentCreator = Instantiate(lineCreator);
        currentCreator.transform.SetParent(transform);
        
        currentCreator.GetComponent<DrowLine>().startPos = GetWorldPosition();
        currentCreator.GetComponent<DrowLine>().colorNum = color;
        enabled = false;
    }
    public static Vector2 GetWorldPosition()
    {
        Vector2 coor;
        Vector3 pos;
        if (isMouse)
            pos = Input.mousePosition;
        else
            pos = Input.GetTouch(0).position;
 
        coor = new Vector3(pos.x, pos.y, 1);

        return Camera.main.ScreenToWorldPoint(coor);
    }
    
}