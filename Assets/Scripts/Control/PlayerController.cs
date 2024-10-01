using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System.Runtime.InteropServices.WindowsRuntime;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
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
        }

        private void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(GetCursorType(CursorType.None));
                return;
            }
            if (InteractWithComponent()) return;
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
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();

            foreach (RaycastHit hit in hits)
            {
                if (!mover.CanMoveTo(hit.point)) return false;

                IRaycastable[] raycastableArray = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastableArray)
                {
                    

                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(GetCursorType(raycastable.GetCursorType()));
                        return true;
                    }
                }
            }

            return false;   
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];

            for(int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = HitWithNavMesh(out target);

            if (hasHit)
            {
                if (!mover.CanMoveTo(target)) return false;

                if (Input.GetMouseButton(0)) mover.StartMoveAction(target, 1f);
                SetCursor(GetCursorType(CursorType.Movement));
                return true;
            }
            return false;
        }

        private bool HitWithNavMesh(out Vector3 target)
        {
            target = new Vector3();
            
            RaycastHit hitWithNavmesh;
            bool hasHitWithNavMesh = Physics.Raycast(GetMouseRay(), out hitWithNavmesh);

            if (!hasHitWithNavMesh) return false;

            NavMeshHit navMeshHit;
            float maxDistance = .5f;
            target = hitWithNavmesh.point;

            return NavMesh.SamplePosition(hitWithNavmesh.point, out navMeshHit, maxDistance, NavMesh.AllAreas);
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

