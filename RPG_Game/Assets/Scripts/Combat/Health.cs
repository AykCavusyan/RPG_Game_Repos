using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        bool iseDead = false;

        public bool IsDead()
        {
            return iseDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (iseDead) return;

            GetComponent<Animator>().SetTrigger("die");
            iseDead = true;
        }
    }
}
