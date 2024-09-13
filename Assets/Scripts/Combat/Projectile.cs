using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        [SerializeField] private bool isHoming;
        private float damage;


        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (target == null) return;

            if (isHoming && !target.GetComponent<Health>().IsDead()) transform.LookAt(GetAimLocation());

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

            if (other.gameObject.CompareTag(Dictionary.WEAPON_TAG)) return;

            if (other.gameObject.TryGetComponent(out Health targetHealth))
            {
                if (targetHealth.IsDead()) return;
                targetHealth.TakeDamage(damage);
                Destroy(gameObject);
            } else
            {
                Destroy(gameObject);
            }

        }

    }
}

