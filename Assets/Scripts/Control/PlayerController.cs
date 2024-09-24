using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        enum CursorType
        {
            None,
            Combat,
            Movement,
            UI,
        }

        private Mover mover;
        private Fighter fighter;
        private Health health;

        [Serializable]
        struct CursorMapping
        {
            public CursorType cursorType;
            public Vector2 hotspot;
            public Texture2D cursorTexture;
        }

        [SerializeField] private CursorMapping[] cursorMappingArray;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(GetCursorType(CursorType.None));
                return;
            } 
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

            SetCursor(GetCursorType(CursorType.None));

        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(GetCursorType(CursorType.UI));
                return true;
            }

            return false;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null || !fighter.CanAttackTarget(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                }
                SetCursor(GetCursorType(CursorType.Combat));
                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point, 1f);
                }
                SetCursor(GetCursorType(CursorType.Movement));
                return true;
            }
            return false;
        }

        private CursorMapping GetCursorType(CursorType cursorType)
        {
            foreach(CursorMapping mapping in cursorMappingArray)
            {
                if(mapping.cursorType == cursorType) return mapping;
            }

            return cursorMappingArray[0];
        }

        private void SetCursor(CursorMapping cursorMapping)
        {
            Cursor.SetCursor(cursorMapping.cursorTexture, cursorMapping.hotspot, CursorMode.Auto);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

