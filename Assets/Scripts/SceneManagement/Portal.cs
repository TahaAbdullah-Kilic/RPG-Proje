using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum PortalNumarasi
        {
            A,B,C,D
        }
        [SerializeField] int AcilacakScene;
        [SerializeField] Transform SpawnPoint;
        [SerializeField] PortalNumarasi Hedef;
        [SerializeField] float FadeOut = 1f;
        [SerializeField] float WaitForFade = 0.5f;
        [SerializeField] float FadeIn = 0.5f;
        public void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(SceneLoad());
            }
        }
        public IEnumerator SceneLoad()
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(FadeOut);
            yield return SceneManager.LoadSceneAsync(AcilacakScene);
            Portal OtherPortal = GetOtherPortal();
            UpdatePlayer(OtherPortal);
            yield return new WaitForSeconds(WaitForFade);
            yield return fader.FadeIn(FadeIn);
            Destroy(gameObject);
        }
        private void UpdatePlayer(Portal OtherPortal)
        {
            GameObject oyuncu = GameObject.FindWithTag("Player");
            oyuncu.GetComponent<NavMeshAgent>().Warp(OtherPortal.SpawnPoint.position);
            oyuncu.transform.rotation = OtherPortal.SpawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                if(portal.Hedef != Hedef) continue;
                return portal;
            }
            return null;
        }
    }
}
