using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Movement {
    public class Mover : MonoBehaviour {

        [SerializeField] private float movementSpeed = 0.0f;
        [SerializeField] private bool boundInArena = false;
        private Vector3 direction = Vector3.zero;
        private float speedFraction = 1.0f;
        private SpaceShipBounds bounds;

        void Start() {
            ComputeBounds();
        }

        void Update() {
            Move();
        }

        public void SetDirection(Vector3 direction) {
            SetDirection(direction, 1.0f);
        }

        public void SetDirection(Vector3 direction, float speedFraction) {
            this.speedFraction = speedFraction;
            this.direction = direction;
            if (this.direction != Vector3.zero) {
                this.direction.Normalize();
            }
        }

        void Move() {
            if (this.direction == Vector3.zero) return;

            Vector3 newPosition = transform.position + (direction * movementSpeed * speedFraction * Time.deltaTime);
            if (boundInArena) {
                float newX = Mathf.Clamp(newPosition.x, this.bounds.MinXWorld, this.bounds.MaxXWorld);
                float newY = Mathf.Clamp(newPosition.y, this.bounds.MinYWorld, this.bounds.MaxYWorld);
                newPosition.x = newX;
                newPosition.y = newY;
            }
            transform.position = newPosition;
        }

        void ComputeBounds() {
            Arena arena = FindObjectOfType<Arena>();
            PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
            this.bounds = new SpaceShipBounds(collider, arena, transform.localScale.x, transform.localScale.y);
        }

    }
}