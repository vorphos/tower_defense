using UnityEngine;

public class Waypoints : MonoBehaviour
{
   private Transform[] _points;

   private void Awake()
   {
      _points = new Transform[transform.childCount];
      for (int i = 0; i < _points.Length; i++)
      {
         _points[i] = transform.GetChild(i);
      }   
   }

   public Transform GetPoint(int index)
   {
      return _points[index];
   }

   public int GetCount()
   {
      return _points.Length;
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.cyan;
      for (int i = 0; i < transform.childCount - 1; i++)
      {
         Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
         Gizmos.DrawSphere(transform.GetChild(i).position, 0.2f);
      }   
   }
   
}
