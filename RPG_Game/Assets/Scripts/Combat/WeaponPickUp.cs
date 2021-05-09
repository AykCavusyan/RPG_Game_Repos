using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour
    {

        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 10f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds (float seconds)
        {
            ShowPickup(false); 

            yield return new WaitForSeconds(seconds);

            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            
            //transform.GetChild(0).gameObject.SetActive(shouldShow);

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }

        }

       

        
    }
}
