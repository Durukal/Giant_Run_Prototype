using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour {
    private Animator _animator;

    [CanBeNull]
    private CharacterController _character;

    [CanBeNull]
    private Character.Boss _boss;

    void Start() {
        _animator = GetComponent<Animator>();
        _character = GetComponent<CharacterController>();
        _boss = GetComponent<Character.Boss>();
    }

    private void PlayWaveAnimation() {
        _animator.SetTrigger("Wave");
    }

    private void PlayIdleAnimation() {
        _animator.SetTrigger("Idle");
    }

    private float GetCurrentAnimationLength() {
        float animationLenghth = _animator.GetCurrentAnimatorClipInfo(0).Length;
        return animationLenghth;
    }

    private IEnumerator PlayWave() {
        if (_character != null) {
            _character.isEndpointReached = true;
            _character.StopMovement(); 
        }
        
        PlayWaveAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength());
    }

    private void EndPointReached(CharacterController controller) {
        if (_character == controller) {
            StartCoroutine(PlayWave());
            PlayIdleAnimation();
        }
    }

    private void BossEnd() {
        if (_boss != null) {
            _boss.IsLevelEnd = true;
            StartCoroutine(PlayWave());
            PlayIdleAnimation();
        }
    }

    private void OnEnable() {
        CharacterController.OnEndPointReached += EndPointReached;
        Character.Boss.OnLevelEnd += BossEnd;
    }

    private void OnDisable() {
        CharacterController.OnEndPointReached -= EndPointReached;
        Character.Boss.OnLevelEnd -= BossEnd;
    }
}