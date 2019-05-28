using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpaceShooter.Movement {
    
    public class Rotate : MonoBehaviour {
        
        [SerializeField] private float rotationSpeed = 0.0f;

        void Update() {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

    }

}