using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Kontrol
{
    
    public class VardiyaRotasi : MonoBehaviour
    {
        float gizmoyaricap = 0.4f;
        private void OnDrawGizmos() 
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypointLocation(i), gizmoyaricap);
                Gizmos.DrawLine(GetWaypointLocation(i), GetWaypointLocation(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            else return i+1;
        }

        public Vector3 GetWaypointLocation(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}

