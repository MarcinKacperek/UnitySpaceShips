using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Movement;
using UnityEngine;

namespace SimpleSpaceShooter.Control {
    public class PlayerController : MonoBehaviour {
    
        private Mover mover;

        void Awake() {
            mover = GetComponent<Mover>();
        }

        void Update() {
            HandleMovement();    
        }

        void HandleMovement() {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
            mover.SetDirection(direction);
        }
    
    }
}