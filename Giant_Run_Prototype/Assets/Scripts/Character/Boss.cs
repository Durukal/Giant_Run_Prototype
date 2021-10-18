using System;
using UnityEngine;

namespace Character {
    public class Boss : MonoBehaviour {
        [SerializeField]
        private CharacterController _character;
        private CharacterAnimations _animations;
        public static Action OnLevelEnd;
        public bool IsLevelEnd = false;

        private void Start() {
            _animations = GetComponent<CharacterAnimations>();
        }

        private void Update() {
            if (_character.isEndpointReached == true && !IsLevelEnd) {
                OnLevelEnd?.Invoke();
            }
        }
    }
}