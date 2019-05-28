using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using UnityEngine;

namespace SimpleSpaceShooter.Combat {
    
    public class Cannon : MonoBehaviour {
    
        [SerializeField] private float damage = 0.0f;
        [SerializeField] private float attackCooldown = 0.0f;
        private float nextAttackTime = 0.0f;
        [SerializeField] private Projectile projectilePrefab = null;

        void Start() {
            if (IsPlayer()) {
                // Player has delay on start
                nextAttackTime = Time.time + Constants.INITIAL_GAME_DELAY;
                // Enemy initial attack cooldown is set in SetAttackSpeed method (called by LevelManager)
            // } else {
            //     // All enemy cannons have random delay at start
            //     nextAttackTime += Time.time + Random.Range(0.0f, attackCooldown);
            }
        }

        void Update() {
            HandleShooting();        
        }

        private void HandleShooting() {
            if (nextAttackTime <= Time.time) {
                Projectile projectile = GameObject.Instantiate(projectilePrefab, transform.position, transform.rotation);
                projectile.Damage = damage;

                nextAttackTime = Time.time + attackCooldown;
            }
        }

        private bool IsPlayer() {
            Transform parentTransform = transform;
            while ((parentTransform = parentTransform.parent) != null) {
                if (parentTransform.tag == "Player") {
                    return true;
                }
            }

            return false;
        }

        public void SetAttackSpeed(float divider) {
            attackCooldown /= divider;
            // Set initial random delay
            nextAttackTime += Time.time + Random.Range(0.0f, attackCooldown);
        }

    }

}