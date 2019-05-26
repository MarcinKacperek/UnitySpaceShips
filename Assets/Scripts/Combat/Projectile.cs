using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Combat {
    public class Projectile : MonoBehaviour {
        
        [SerializeField] private float movementSpeed = 0.0f;
        [SerializeField] private ParticleSystem onHitEffect = null;

        private float damage;
        private Transform target;

        private Arena arena;

        void Awake() {
            arena = FindObjectOfType<Arena>();
        }

        void Update() {
            HandleMovement();

            // Check if missile is too far from arena
            float y = transform.position.y;
            if (y > arena.ArenaHeight || y < -arena.ArenaHeight) {
                Destroy(gameObject);
            }
        }

        public void SetDamage(float damage) {
            this.damage = damage;
        }

        public void SetTarget(Transform target) {
            this.target = target;
        }

        void HandleMovement() {
            if (target != null) {
                transform.up = target.position - transform.position;
            }
            transform.position += transform.up.normalized * movementSpeed * Time.deltaTime;
        }

        void OnTriggerEnter2D(Collider2D other) {
            Health health = other.GetComponent<Health>();    
            if (health == null) return;

            if (onHitEffect != null) {
                Utils.SpawnAndDestroyParticleSystem(onHitEffect, transform.position);
            }

            health.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
