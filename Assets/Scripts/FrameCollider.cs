using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            Scorer.Instance.IncreaseScore(20);
            Scorer.Instance.IncreaseMultiplier();
        }
    }
}
