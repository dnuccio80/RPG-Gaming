using RPG.Control;
using RPG.Resources;
using System;
using UnityEngine;

namespace RPG.Combat
{

    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {

        public event EventHandler OnTargeted;
        public event EventHandler OnNoTargeted;

        public bool HandleRaycast(PlayerController callingController)
        {
            Fighter fighter = callingController.GetComponent<Fighter>();

            if (!fighter.CanAttackTarget(gameObject)) return false;
            if (Input.GetMouseButton(0)) fighter.Attack(gameObject);
            return true;

        }
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public void EnemyTargeted()
        {
            OnTargeted?.Invoke(this, EventArgs.Empty);
        }

        public void EnemyNoTargeted()
        {
            OnNoTargeted?.Invoke(this, EventArgs.Empty);
        }
    }
}
