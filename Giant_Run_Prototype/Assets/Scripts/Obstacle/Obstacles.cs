using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour {
    void Start() { }
    
    void Update() {
        Rotate();
    }

    private void Rotate() {
        transform.Rotate(0f, 0f, 80f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
        if (characterController.isTriggered == false) {
            characterController.isTriggered = true;
            characterController.UpdateScale(new Vector3(-.2f, -.2f, -.2f));
        }
    }

    private void OnCollisionExit(Collision other) {
        CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
        characterController.isTriggered = false;
    }
}