using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        Transform target;
        [SerializeField] float weaponRange = 2f;

        private void Update()
        {
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;

            if (target != null && !isInRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }
        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }
    }
}
   
