using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Hareket;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Kontrol
{
    public class OyuncuKontrol : MonoBehaviour
    {
        Can can;
        private void Start() 
        {
            can = GetComponent<Can>();
        }
        void Update()
        {
            if(can.IsDead()) return;
            if(InteractWithCombat() == false)
            {
                if(InteractWithMovement() == false)
                {
                    //print("Buraya gidemezsiniz");
                }
            }
        }
        //Savaş Eylemini Başlat
        private bool InteractWithCombat()
        {    
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                Dusman hedef = hit.transform.GetComponent<Dusman>();
                if (hedef == null) continue;

                if(!GetComponent<Fighter>().CanAttack(hedef.gameObject))
                {
                    continue;
                }
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(hedef.gameObject);
                }
                return true;
            }
            return false;
        }
        //Hareket Eylemini Başlat
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hashit = Physics.Raycast(GetMouseRay(), out hit);
            if (hashit == true)
            {
                if (Input.GetMouseButton(0) == true)
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }
        //Tıklanan Yeri Hedef Olarak Al
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
