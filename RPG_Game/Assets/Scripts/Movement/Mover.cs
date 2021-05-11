
using RPG.Core;
using RPG.Resources;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] private NavMeshAgent player;
        [SerializeField] float maxSpeed = 6f;
        Health health;

        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            player.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardspeed", speed);
        }

        public void StartMoveACtion(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }


        public void MoveTo(Vector3 destination, float speedFraction)
        {
            if (!NavMesh.SamplePosition(destination, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                return;
            }

            player.SetDestination(hit.position);
            player.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            player.isStopped = false;
        }

        public void Cancel()
        {
            player.isStopped = true;
        }


        // Capture State with Dictionnary

        //public object CaptureState()
        //{
        //    Dictionary<string, object> data = new Dictionary<string, object>();
        //    data["position"] = new SerializableVector3(transform.position);
        //    data["rotation"] = new SerializableVector3(transform.eulerAngles);
        //    return data;
        //}

        //public void RestoreState(object state)
        //{
        //    Dictionary<string, object> data = (Dictionary<string, object>)state;
        //    GetComponent<NavMeshAgent>().enabled = false;
        //    transform.position = ((SerializableVector3)data["position"]).ToVector();
        //    transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
        //    GetComponent<NavMeshAgent>().enabled = true;
        //}




        // Capture State with STRUCT

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles); 
            return data;    
        }

        public void RestoreState(object state)
        {
          MoverSaveData data = (MoverSaveData)state;
          GetComponent<NavMeshAgent>().enabled = false;
          transform.position = data.position.ToVector();
          transform.eulerAngles = data.rotation.ToVector();
          GetComponent<NavMeshAgent>().enabled = true;
        }
        




    }
}
