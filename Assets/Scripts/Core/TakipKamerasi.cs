using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class TakipKamerasi : MonoBehaviour
    {
        [SerializeField] Transform target;
        //Ana Karakteri Takip Et
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
