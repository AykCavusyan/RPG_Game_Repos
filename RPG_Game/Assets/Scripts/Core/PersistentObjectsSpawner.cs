using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {

        [SerializeField] GameObject persistentObjectPerfab;

        static bool hasSpawned = false;

        void Awake()
        {
            if (hasSpawned)
            {
                return;
            }

            else
            {
                SpawnPersistentObjects();
                hasSpawned = true;
            }
            
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPerfab);
            DontDestroyOnLoad(persistentObject);
        }

        void Update()
        {

        }
    }
}
