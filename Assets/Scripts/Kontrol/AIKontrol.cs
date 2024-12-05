using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using RPG.Core;
using UnityEngine.Experimental.UIElements.StyleEnums;
using RPG.Hareket;
using System;
using UnityEngine.AI;

namespace RPG.Kontrol
{
    public class AIKontrol : MonoBehaviour
    {
        [SerializeField] float GorusAlani = 6f;
        [SerializeField] float suphezamani = 5f;
        [SerializeField] VardiyaRotasi vardiyaRotasi = null;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointbekleme = 2f;
        Fighter fighter;
        GameObject oyuncu;
        Can can;
        Vector3 dusmankonum;
        Mover hareket;
        float oyuncuyusongormeani = Mathf.Infinity;
        float waypointdurmasuresi = Mathf.Infinity;
        int mevcutWaypointIndex = 0;

        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            oyuncu = GameObject.FindWithTag("Player");
            can = GetComponent<Can>();
            dusmankonum = transform.position;
            hareket = GetComponent<Mover>();
        }

        private void Update()
        {
            if (can.IsDead()) return;
            if (InAttackRange() && fighter.CanAttack(oyuncu))
            {
                oyuncuyusongormeani = 0;
                EnemyBehaviour();
            }
            else if(oyuncuyusongormeani < suphezamani)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            oyuncuyusongormeani += Time.deltaTime;
            waypointdurmasuresi += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelAction();
        }

        private void EnemyBehaviour()
        {
            GetComponent<Mover>().ChangeMovementSpeed(4.5f);
            fighter.Attack(oyuncu);
        }
        private void PatrolBehaviour()
        {
            GetComponent<Mover>().ChangeMovementSpeed(1.5f);
            Vector3 yenipozisyon = dusmankonum;
            if (vardiyaRotasi != null)
            {
                if (AtWayPoint())
                {
                    waypointdurmasuresi = 0;
                    CycleWaypoint();
                }
                yenipozisyon = GetCurrentWaypoint();
            }
            if(waypointdurmasuresi > waypointbekleme) hareket.StartMoveAction(yenipozisyon);        
        }

        private bool AtWayPoint()
        {
            return Vector3.Distance(transform.position,GetCurrentWaypoint()) < waypointTolerance;
        }
        private Vector3 GetCurrentWaypoint()
        {
            return vardiyaRotasi.GetWaypointLocation(mevcutWaypointIndex);
        }
        private void CycleWaypoint()
        {
            mevcutWaypointIndex = vardiyaRotasi.GetNextIndex(mevcutWaypointIndex);
        }
        private bool InAttackRange()
        {
            return Vector3.Distance(oyuncu.transform.position, transform.position) < GorusAlani;
        }
        //Unity Özel Komudu
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,GorusAlani);
        }
    }
}
