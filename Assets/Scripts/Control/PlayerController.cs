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

        [Serializable]
        struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D cursorTexture;
            public Vector2 cursorHotspot;
        }

        [SerializeField] private CursorMapping[] cursorMappingArray;


        private Mover mover;
        private Fighter fighter;
        private Health health;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (InteractWithUI())
            {
                return;
            }
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            } 
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);

        }

        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
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
                SetCursor(CursorType.Combat);
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
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        void SetCursor(CursorType cursorType)
        {
            CursorMapping cursorMapping = GetCursorMapping(cursorType);
            Cursor.SetCursor(cursorMapping.cursorTexture, cursorMapping.cursorHotspot, CursorMode.Auto);
        }

        CursorMapping GetCursorMapping(CursorType cursorType)
        {
            foreach(CursorMapping cursorMapping in cursorMappingArray)
            {
                if (cursorMapping.cursorType == cursorType) return cursorMapping;
            }

            return cursorMappingArray[0];
        }


        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

