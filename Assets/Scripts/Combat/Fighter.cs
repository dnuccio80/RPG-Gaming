using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
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
            else mover.Cancel();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetTransform.position) <= weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetTransform = target.transform;
        }

        public void Cancel()
        {
            targetTransform = null;
        }
    }
}
