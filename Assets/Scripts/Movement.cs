using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float scoreDelay = 1f;
    
    private Vector3 _direction = new Vector3(1, 0, 0);
    private Quaternion _targetRotation;
    private bool _hit = false;
    private Coroutine _delay = null;
    
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    
    private void Start()
    {
        _targetRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        
        
    }

    void Update()
    {
        if (FinishCollider.Instance.IsFinished())
        {
            Finish();
            return;
        }
        
        if(!_hit)
            Move();
        else
            MoveBack();
        Rotation();
    }

    void Move()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            transform.position += _direction * moveSpeed * 2 * Time.deltaTime;
            if(_delay!=null)
                return;
            _delay = StartCoroutine(DisableScoreCounter(scoreDelay));
            Scorer.Instance.IncreaseScore(1.0f);
        }
        else
            transform.position += _direction * moveSpeed * Time.deltaTime;
    }

    private void Finish()
    {
        transform.Rotate(Vector3.up,10*Time.deltaTime);
    }

    private IEnumerator DisableScoreCounter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _delay = null;
    }
    
    private void MoveBack()
    {
        transform.position -= _direction * moveSpeed/2f * Time.deltaTime;
    }

    void Rotation()
    {
        if(transform.rotation!=_targetRotation)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, Time.deltaTime*rotationSpeed);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position;
        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPosition = Input.GetTouch(0).position;
            if (_endTouchPosition.x < _startTouchPosition.x)
            {
                _targetRotation *= Quaternion.AngleAxis(-90, Vector3.up);
            }
            else if (_endTouchPosition.x > _startTouchPosition.x)
            {
                _targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            _hit = true;
            Scorer.Instance.DecreaseScore(5);
            Scorer.Instance.ResetMultiplier();
            Invoke("ContinueMove", 0.5f);
        }
    }

    private void ContinueMove()
    {
        _hit = false;
    }

    
}
