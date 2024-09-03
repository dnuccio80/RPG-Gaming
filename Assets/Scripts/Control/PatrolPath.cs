using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {

        [SerializeField] private float sphereRadius = 2f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetChildPosition(i), sphereRadius);
                Gizmos.DrawLine(GetChildPosition(i), GetNextChild(i).position);
            }
        }

        private Transform GetNextChild(int i)
        {
            return transform.GetChild((i + 1) % transform.childCount);
        }

        private Vector3 GetChildPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
