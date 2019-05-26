using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Core;
using SimpleSpaceShooter.Movement;
using UnityEngine;

namespace SimpleSpaceShooter.Control {
    
    public class WaveEnemyController : MonoBehaviour {
    
        private const float DISTANCE_TOLERANCE = 0.1f;

        private WavePathNode currentNode = null;
        private int currentNodeIndex = 0;

        private Mover mover;
        private Wave wave;

        void Awake() {
            mover = GetComponent<Mover>();
        }

        void Start() {
            wave = transform.GetComponentInParent<Wave>();
        }

        void Update() {
            HandleMovement();
        }
    
        void HandleMovement() {
            if (currentNode == null || Vector3.Distance(transform.position, currentNode.transform.position) <= DISTANCE_TOLERANCE) {
                currentNodeIndex++;
                currentNode = wave.GetPathNode(currentNodeIndex);

                // Reached end
                if (currentNode == null) {
                    wave.RemoveEnemy(gameObject);
                    Destroy(gameObject);
                    return;
                }

                Vector3 direction = currentNode.transform.position - transform.position;
                direction.Normalize();
                mover.SetDirection(direction, currentNode.SpeedFractionToThisNode);
            }
        }

    }

}