using RPG.Movement;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;

        private Transform targetTransform;
        private Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if (targetTransform == null) return;

            if (!GetIsInRange()) mover.MoveTo(targetTransform.position);
            else mover.Stop();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetTransform.position) <= weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            targetTransform = target.transform;
            print("Attaking " + target.name);
        }

        public void CancelAttack()
        {
            targetTransform = null;
        }
    }
}
