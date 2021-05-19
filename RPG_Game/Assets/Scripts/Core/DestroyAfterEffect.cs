using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] GameObject targetToDestroy = null;

        // Update is called once per frame
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else
                Destroy(gameObject);
            }
        }
    }
}
