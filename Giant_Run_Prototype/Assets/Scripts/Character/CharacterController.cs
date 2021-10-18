using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour {
    public static Action<CharacterController> OnEndPointReached;
    public static Action<int> OnScoreUpdate;
    
    public bool isTriggered = false;
    
    public bool isEndpointReached = false;

    [SerializeField]
    private Transform endPointTransform;

    [SerializeField]
    private float moveSpeed = 4f;

    public float MoveSpeed { get; set; }

    private Touch _touch;

    private void Start() {
        MoveSpeed = moveSpeed;
    }

    void Update() {
        Move();

        if (transform.position.z >= endPointTransform.position.z && !isEndpointReached) {
            OnEndPointReached?.Invoke(this);
        }

        if (Input.touchCount > 0 && !isEndpointReached) {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Moved) {
                var position = transform.position;
                position = Vector3.Lerp(position, new Vector3(Mathf.Clamp(position.x + _touch.deltaPosition.x, -2f, 2f), position.y, position.z), 0.5f * moveSpeed * Time.deltaTime);
                transform.position = position;
            }
        }
    }

    public void UpdateScale(Vector3 scaleAmount) {
        OnScoreUpdate?.Invoke((int)(scaleAmount.x / 0.2f));
        transform.localScale += scaleAmount;
        if (transform.localScale.x <= 0f) {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    public void Move() {
        if (isEndpointReached == false) {
            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
    }

    public void StopMovement() {
        MoveSpeed = 0f;
    }

    public void ResumeMovement() {
        MoveSpeed = moveSpeed;
    }
}