using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private Transform targetTransform;


    void Update()
    {
        transform.position = targetTransform.position;
    }
}
