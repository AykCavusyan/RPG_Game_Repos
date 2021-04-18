using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace RPG.Cinematics
{
    public class CinmaticTrigger : MonoBehaviour
    {
        private bool triggerActive = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player" || triggerActive == false)
            {
                return;
            }
        else
            {
                GetComponent<PlayableDirector>().Play();
                triggerActive = false;
            }
            
                
            
            
        }
    }

}
