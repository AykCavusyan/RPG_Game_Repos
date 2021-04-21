using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player")
            {
                return;
            }
            else
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {

            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherportal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherportal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this)
                {
                    continue;
                }
                if (portal.destination != destination)
                {
                    continue;
                } 

                else
                return portal;
            }

            return null;
        }
    }
}
