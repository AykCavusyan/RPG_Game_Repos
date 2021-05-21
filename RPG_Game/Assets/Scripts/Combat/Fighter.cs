using GameDevTV.Utils;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using RPG.Saving;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable , IModifierProvider
    {
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetUpDefaultWeapon);
        }

        private Weapon SetUpDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        private void Start()
        {
            currentWeapon.ForceInit();

        }

        private void Update()
        {

            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                Attackbehavior();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        private void Attackbehavior()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                GetComponent<Animator>().ResetTrigger("attack");
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
            
        }

        //Animation Event
        void Hit()
        {
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            
            if (target == null)
            {
                return;
            }
            if (currentWeapon.value.HasProjectile())
            {
                currentWeapon.value.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
            
        }

        //Animation Event
        void Shoot()
        {
            Hit();
           
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetRange();
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
            GetComponent<Mover>().Cancel();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public IEnumerable<float> GetAdditiveModifers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

       
    }
}
   
