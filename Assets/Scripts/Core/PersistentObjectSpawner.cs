using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject PersistentObjectPrefab;

        static bool HasSpawned = false;

        private void Awake()
        {
            if (HasSpawned) return;
            SpawnPersistentObject();
            HasSpawned = true;    
        }

        private void SpawnPersistentObject()
        {
            transform.parent = null;
            GameObject persistentobject = Instantiate(PersistentObjectPrefab);
            DontDestroyOnLoad(persistentobject);
        }
    }
}

