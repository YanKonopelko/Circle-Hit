 using UnityEngine;
using System;
using System.Collections.Generic;

 public class DrowLine : MonoBehaviour
 {

     private LineRenderer Line;
     [SerializeField] private Color[] colors;
     public Vector3 startPos;
     public int colorNum = 0;

     private Vector3 lastPos;
     private Vector3 currentPoint;

     private const float mass = 10;
     private const float allowedDistance = 0.2f;

     void Start()
     {
         Line = GetComponent<LineRenderer>();
         Line.startWidth = 0.2f;
         Line.endWidth = 0.2f;
         Line.positionCount = 0;
         Line.startColor = Line.endColor = colors[colorNum];
         lastPos = startPos;
     }


     void Update()
     {
         if (Input.GetMouseButton(0) && InkManager.inkAmount > 0)
             NewPoint();
         else if (Input.GetMouseButtonUp(0))
            EndLine();
     }

     private Vector2 FixPos(Vector2 from)
     {
         Vector2 point = new Vector2(0, 0);

         Debug.DrawRay(from, Vector2.down * allowedDistance, Color.black);
         Debug.DrawRay(from, Vector2.right * allowedDistance, Color.black);
         Debug.DrawRay(from, Vector2.left * allowedDistance, Color.black);
         Debug.DrawRay(from, Vector2.up * allowedDistance, Color.black);
         
         if (Physics2D.Raycast(from, Vector2.down, allowedDistance))
             point.y = 1;
         if (Physics2D.Raycast(from, Vector2.up, allowedDistance))
             point.y = -1;

         if (Physics2D.Raycast(from, Vector2.left, allowedDistance))
             point.x = 1;
         if (Physics2D.Raycast(from, Vector2.right, allowedDistance))
             point.x = -1;
         
         return point;
     }

     private void NewPoint()
     {
         currentPoint = InputManager.GetWorldPosition();
         if (currentPoint != lastPos || lastPos == startPos)
         {
             var fix = FixPos(lastPos);
             Vector3 newPoint = lastPos;
             if ( (lastPos.x < currentPoint.x && fix.x == 1) || (lastPos.x > currentPoint.x && fix.x == -1)   )
                 newPoint.x = currentPoint.x;
             if ( (lastPos.y < currentPoint.y && fix.y == 1) || (lastPos.y > currentPoint.y && fix.y == -1)   )
                 newPoint.y = currentPoint.y;
                 
             if (fix.x == 0)
                 newPoint.x = currentPoint.x;
             if (fix.y == 0)
                 newPoint.y = currentPoint.y;
             if (lastPos == newPoint) return;
             lastPos = newPoint;
             InkManager.inkAmount -= Time.deltaTime * 10;
             Line.positionCount++;
             Line.SetPosition(Line.positionCount - 1, newPoint);

         }
     }

     private void EndLine()
     {
         var coll = gameObject.AddComponent<PolygonCollider2D>();

         List<Vector2> points = new List<Vector2>();
         Vector3 pos = new Vector3();
         for (int i = 0; i < Line.positionCount; i++)
         {
             pos = new Vector3(0, 0.1f, 0);
             if (i != 0 && Math.Abs(Line.GetPosition(i).y - Line.GetPosition(i - 1).y) >
                 Math.Abs(Line.GetPosition(i).x - Line.GetPosition(i - 1).x))
                 pos = new Vector3(0.1f, 0, 0);
             points.Add(Line.GetPosition(i) + pos);
         }

         for (int i = Line.positionCount - 1; i >= 0; i--)
         {
             pos = new Vector3(0, 0.1f, 0);
             if (i != Line.positionCount - 1 && Math.Abs(Line.GetPosition(i).y - Line.GetPosition(i + 1).y) >
                 Math.Abs(Line.GetPosition(i).x - Line.GetPosition(i + 1).x))
                 pos = new Vector3(0.1f, 0, 0);
             points.Add(Line.GetPosition(i) - pos);
         }

         points[points.Count - 1] = points[0];
         var a = points.ToArray();
         coll.points = a;
         var rb = gameObject.AddComponent<Rigidbody2D>();
         rb.mass = mass;
         GetComponent<DrowLine>().enabled = false;
     }
 }
