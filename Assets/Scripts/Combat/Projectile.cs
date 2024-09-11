using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform target;

        private float damage;

        private void Update()
        {
            if (target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null) return target.position;
            return target.position + Vector3.up * targetCapsule.height / 2;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void SetDamage(float damage) { this.damage = damage; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == this.target.gameObject)
            {
                target.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }

    }
}

