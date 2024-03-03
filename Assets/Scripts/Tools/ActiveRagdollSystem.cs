using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdollSystem : MonoBehaviour
{
    public Rigidbody[] boneForce;
    Rigidbody[] rigids;
    Collider[] colliders;

    Collider mainCollider;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        mainCollider = GetComponent<Collider>();
        anim = GetComponent<Animator>();

        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].isKinematic = true;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        mainCollider.enabled = true;
    }

    public void ActiveRagdoll()
    {
        if (anim != null)
            anim.enabled = false;
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].isKinematic = false;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
        mainCollider.enabled = false;
    }
    public void ActiveRagdollForce(Vector3 axis, float force)
    {
        if (anim != null)
            anim.enabled = false;
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].isKinematic = false;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
        mainCollider.enabled = false;

        for (int i = 0; i < boneForce.Length; i++)
        {
            boneForce[i].velocity = axis * force;
        }
    }
}
