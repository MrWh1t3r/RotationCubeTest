using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCollider : MonoBehaviour
{
    private bool finished = false;

    public static FinishCollider Instance;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            Scorer.Instance.IncreaseScoreWithoutMultiplier(300);
            Scorer.Instance.Finish();
            finished = true;
        }
    }

    public bool IsFinished()
    {
        return finished;
    }
}
