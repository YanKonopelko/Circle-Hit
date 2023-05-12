 using UnityEngine;
using System;
using System.Collections.Generic;
 using NavMeshPlus.Components;
 using Unity.VisualScripting;

 public class DrowLine : MonoBehaviour 
 {

     private LineRenderer Line;
     [SerializeField] private Color[] colors;
     public Vector3 startPos;
     public int colorNum = 0;

     private Vector3 lastPos;
     private Vector3 currentPoint;
     private Vector2 fix;
     [SerializeField] private float pointSize = 0.02f;

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
     

     private void Update()
     {
         if ((Input.touchCount > 0 || Input.GetMouseButton(0) ) && InkManager.inkAmount > 0)
             NewPart();
         else if (Input.GetMouseButtonUp(0) || (Input.touchCount==0) || InkManager.inkAmount <=0 )
             EndLine();     }

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

         if (Physics2D.Raycast(from, Vector2.down, allowedDistance) &&
             Physics2D.Raycast(from, Vector2.up, allowedDistance))
             point.y = 2;
                 
         if (Physics2D.Raycast(from, Vector2.left, allowedDistance))
             point.x = 1;
         if (Physics2D.Raycast(from, Vector2.right, allowedDistance))
             point.x = -1;
         
         if(Physics2D.Raycast(from, Vector2.left, allowedDistance) &&
            Physics2D.Raycast(from, Vector2.right, allowedDistance))
             point.x = 2;
         return point;
     }

     private void NewPoint(Vector2 point)
     {
         Vector3 newPoint = lastPos;
             fix = FixPos(lastPos);
             if ( (lastPos.x < point.x && fix.x == 1) || (lastPos.x > point.x && fix.x == -1)   )
                 newPoint.x = point.x;
             if ( (lastPos.y < point.y && fix.y == 1) || (lastPos.y > point.y && fix.y == -1)   )
                 newPoint.y = point.y;
                 
             if (fix.x == 0)
                 newPoint.x = point.x;
             if (fix.y == 0)
                 newPoint.y = point.y;
             
             if (lastPos == newPoint) return;
             lastPos = newPoint;
             InkManager.inkAmount -= InkManager.currentPrice;
             Line.positionCount++;
             Line.SetPosition(Line.positionCount - 1, newPoint);

     }

     private void NewPart( )
     {
         
         Vector3 segmentEnd = InputManager.GetWorldPosition();
         if (segmentEnd != lastPos || lastPos == startPos)
         {
             int countOfPoint =  (int)(Vector2.Distance(segmentEnd, lastPos) /pointSize);
             var xDif = (segmentEnd.x - lastPos.x) / countOfPoint;
             
             var yDif = (segmentEnd.y - lastPos.y) / countOfPoint;
             
             
             // Если кусок слишком маленький брать констную минимальную длинну
             for (int i = 0; i < countOfPoint; i++)
                 NewPoint(lastPos + new Vector3(xDif, yDif, 0));
             
         }
     }
     private void EndLine()
     {
         var coll = gameObject.AddComponent<PolygonCollider2D>();

         List<Vector2> points = new List<Vector2>();
         
         Vector3 pos = new Vector3();
         
         /*for (int i = 0; i < Line.positionCount; i++)
         {
             pos = new Vector3(0, 0.1f, 0);
             if (i != 0 && Math.Abs(Line.GetPosition(i).y - Line.GetPosition(i - 1).y) >
                 Math.Abs(Line.GetPosition(i).x - Line.GetPosition(i - 1).x))
                 pos = new Vector3(0.1f, 0, 0);
             points.Add(Line.GetPosition(i) + pos);
         }*/
         
         for (int i = 0; i < Line.positionCount; i++)
         {
             pos = Line.GetPosition(i);
             pos = NormalCalc(pos, true).normalized*0.15f;
             points.Add(Line.GetPosition(i) + pos);
         }
         
         
         for (int i = Line.positionCount - 1; i >= 0; i--)
         {
             pos = Line.GetPosition(i);
             pos = NormalCalc(pos, true).normalized*0.15f;
             points.Add(Line.GetPosition(i) - pos);
         }
         
         /*for (int i = Line.posit
             pos = new Vector3(0, 0.1f, 0);
             if (i != Line.positionCount - 1 && Math.Abs(Line.GetPosition(i).y - Line.GetPosition(i + 1).y) >
                 Math.Abs(Line.GionCount - 1; i >= 0; i--)
                                         {etPosition(i).x - Line.GetPosition(i + 1).x))
                 pos = new Vector3(0.1f, 0, 0);
             points.Add(Line.GetPosition(i) - pos);
         }*/
        
         if(points.Count > 1)
            points[^1] = points[0];
         
         var a = points.ToArray();
         coll.points = a;
         
         var rb = gameObject.AddComponent<Rigidbody2D>();
         rb.mass = mass;
         rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

         var modi =  gameObject.AddComponent<NavMeshModifier>();
         modi.overrideArea = true;
         modi.area = 3;
         GameSceneManager.instanse.StartGame();
         
         GetComponent<DrowLine>().enabled = false;

     }


     private Vector2 NormalCalc(Vector2 point_1,bool isUp)
     {
         //Vector2 a;
         //a.Perpendicular1();
         if(isUp)
            return point_1.Perpendicular1();
         else
             return point_1.Perpendicular2();
     }
     
 }
