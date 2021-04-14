using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] float chaseDistance = 5f;
        Fighter fighter;
        GameObject player;
        Mover mover;
        Health health;

        Vector3 guardPosition;

        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();

            guardPosition = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRangeOffPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                mover.StartMoveACtion(guardPosition);
            }
        }

        private bool InAttackRangeOffPlayer()
        {

            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        // calls by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }

}
