using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Hareket
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        NavMeshAgent navMeshAgent;
        Can can;
        private void Start()
        {
            can = GetComponent<Can>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            navMeshAgent.enabled = !can.IsDead();   
            UpdateAnimator();
        }
        //Yürümeyi İptal Et
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        //Yürümeye Eylemine Başla
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        //Hedef Konuma Git
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }
        //Yürüme Animasyonu
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
        public void ChangeMovementSpeed(float x)
        {
            GetComponent<NavMeshAgent>().speed = x;
        }
    }
}


