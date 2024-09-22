using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEffect : MonoBehaviour
{
    [SerializeField] private GameObject targetToDestroy;

    private void Update()
    {
        if(!GetComponent<ParticleSystem>().IsAlive())
        {
            if(targetToDestroy != null) Destroy(targetToDestroy);
            else Destroy(gameObject);
        }
    }


}
