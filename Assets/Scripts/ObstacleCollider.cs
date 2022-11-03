using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.tag = "Untagged";
            GetComponent<Rigidbody>().AddForce(Random.Range(5,10), Random.Range(0,5),Random.Range(0,5),ForceMode.Impulse);
        }
        else
        {
            Debug.Log("3");
        }
    }
}
