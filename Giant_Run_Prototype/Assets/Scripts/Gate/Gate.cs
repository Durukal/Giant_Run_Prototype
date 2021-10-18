using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gate : MonoBehaviour {
    
    private int _gateValue;

    [SerializeField]
    private TextMeshProUGUI gateText;

    void Start() {
        _gateValue = (int)Random.Range(-4.0f, 4.0f);
        if (_gateValue == 0) {
            _gateValue = 1;
        }

        if (_gateValue > 0) {
            gateText.SetText("+" + _gateValue.ToString());
        } else {
            gateText.SetText(_gateValue.ToString());
        }
    }

    void Update() { }

    private void OnTriggerEnter(Collider other) {
        CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
        if (characterController.isTriggered == false) {
            characterController.isTriggered = true;
            characterController.UpdateScale(new Vector3(.2f, .2f, .2f) * _gateValue);
        }
    }

    private void OnTriggerExit(Collider other) {
        CharacterController character =other.gameObject.GetComponent<CharacterController>();
        character.isTriggered = false;
    }
}