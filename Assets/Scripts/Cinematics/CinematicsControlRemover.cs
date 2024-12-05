using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using UnityEngine.AI;
using RPG.Kontrol;
using System.Runtime.InteropServices;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        GameObject oyuncu;
        private void Start()
        {
            oyuncu = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        {
            oyuncu.GetComponent<ActionScheduler>().CancelAction();
            oyuncu.GetComponent<OyuncuKontrol>().enabled = false;
        }
        void EnableControl(PlayableDirector pd)
        {
            oyuncu.GetComponent<OyuncuKontrol>().enabled = true;
        }
    }
}

