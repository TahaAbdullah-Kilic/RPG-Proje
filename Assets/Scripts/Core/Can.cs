using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using UnityEngine;

namespace  RPG.Core
{
    public class Can : MonoBehaviour
    {
        [SerializeField] float kalanCan;
        bool olu = false;
        public bool IsDead()
        {
            return olu;
        }
        //Karakterler Hasar Alınca Canlarını Azaltır
        public void TakeDamage(float hasar)
        {
            kalanCan = Mathf.Max(kalanCan - hasar,0);
            if (kalanCan == 0)
            {
                Die();
            }
        }
        //Karakerlerin Canları Bitince Öldürür
        private void Die()
        {
            if (olu) return;
            olu = true;
            GetComponent<Animator>().SetTrigger("olum");
            GetComponent<ActionScheduler>().CancelAction();
        }
    }
}
