using RPG.Combat;
using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Health health;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            
        }


        // Update is called once per frame
        private void Update()
        {
            health = fighter.GetTarget();

            if ( health != null)
            {
               
                GetComponent<Text>().text = string.Format("{0:0.0}%", health.GetPercentage());
            }
            else
            {
                GetComponent<Text>().text = "N/A";
            }

        }
    }
}
