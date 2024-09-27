using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private bool isHoming;
        [SerializeField] private float maxTimeLife = 10f;
        [SerializeField] private AudioClip explodeAudioClip;

        private float damage;
        private GameObject instigator;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, maxTimeLife);
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

        public GameObject SetInstigator(GameObject instigator) => this.instigator = instigator; 

        public void SetDamage(float damage) { this.damage = damage; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Dictionary.WEAPON_TAG)) return;
            if (other.gameObject.CompareTag(Dictionary.PROJECTILE_TAG)) return;

            if (other.gameObject.TryGetComponent(out Health targetHealth))
            {
                if (targetHealth.IsDead()) return;
                targetHealth.TakeDamage(instigator, damage);
                ExplodeObject();
            } else
            {
                ExplodeObject();
            }

        }

        private void ExplodeObject()
        {
            AudioSource.PlayClipAtPoint(explodeAudioClip, transform.position);
            GameObject explosionGO = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosionGO, 1f);
            Destroy(gameObject);
        }

    }
}

