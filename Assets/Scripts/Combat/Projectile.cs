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
        public float Damage {
            set { damage = value; }
        }
        private Transform target;
        public Transform Target {
            set { target = value; }
        }

        private Arena arena;

        void Awake() {
            arena = FindObjectOfType<Arena>();
        }

        void Update() {
            HandleMovement();

            if (IsTooFarFromArena()) {
                Destroy(gameObject);
            }
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

        private void HandleMovement() {
            if (target != null) {
                transform.up = target.position - transform.position;
            }
            transform.position += transform.up.normalized * movementSpeed * Time.deltaTime;
        }

        private bool IsTooFarFromArena() {
            float x = transform.position.x;
            float y = transform.position.y;
            return x > arena.ArenaWidth || x < -arena.ArenaWidth || y > arena.ArenaHeight || y < -arena.ArenaHeight; 
        }

    }
}
