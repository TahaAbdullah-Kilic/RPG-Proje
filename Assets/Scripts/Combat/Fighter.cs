using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Hareket;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction   
    {
        [SerializeField] float SilahMenzili = 2f;
        [SerializeField] float SaldiriBeklemeSuresi = 1f;
        [SerializeField] float VerilenHasar = 10f;
        Can hedef;
        float sonSaldiridanGecenSure = Mathf.Infinity;

        void Update()
        {
            sonSaldiridanGecenSure += Time.deltaTime;
            if (hedef == null) return;
            if (hedef.IsDead()) return;
            if (hedef != null && !(Vector3.Distance(transform.position,hedef.transform.position) < SilahMenzili))
            {
                GetComponent<Mover>().MoveTo(hedef.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        //Saldırı Animasyonu Fonksiyonu
        private void AttackBehaviour()
        {
            transform.LookAt(hedef.transform);
            if (sonSaldiridanGecenSure >= SaldiriBeklemeSuresi)
            {
                TriggerAttack();
                sonSaldiridanGecenSure = 0;
            }
        }
        //Saldırı Fonksiyonu
        public void Attack(GameObject dusman)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            hedef = dusman.GetComponent<Can>();
        }
        public bool CanAttack(GameObject dusman)
        {
            if (dusman == null) return false;
            Can hedef = dusman.GetComponent<Can>();
            return !hedef.IsDead() && hedef != null;
        }
        //Saldırı Animasyonu Başlat
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("saldiriIptali");
            GetComponent<Animator>().SetTrigger("saldiri");
        }

        //Hasar Verme Fonksiyonu
        void Hit()
        {
            if (hedef == null) return;
            hedef.TakeDamage(VerilenHasar);
        }
        //Saldırı İptali
        public void Cancel()
        {
            StopAttack();
            hedef = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("saldiri");
            GetComponent<Animator>().SetTrigger("saldiriIptali");
        }
    }
}
