using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    
    private Vector3 direction = new Vector3(1, 0, 0);
    private bool _hit = false;
    
    void Update()
    {
        if(!_hit)
            Move();
        else
            MoveBack();
        Rotation();
    }

    void Move()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0,90,0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0,-90,0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            _hit = true;
            Invoke("ContinueMove", 0.5f);
        }
    }

    private void ContinueMove()
    {
        _hit = false;
    }

    private void MoveBack()
    {
        transform.position -= direction * moveSpeed/2f * Time.deltaTime;
    }
}
