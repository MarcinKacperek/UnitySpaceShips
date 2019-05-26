using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Movement {
    
    public class PlayerCollision : MonoBehaviour {
        
        [SerializeField] private ParticleSystem onCollisionEffect = null;

        private Health health;

        void Awake() {
            health = GetComponent<Health>();
        }

        void OnTriggerEnter2D(Collider2D other) {
            // Only player gets damaged on collision
            if (other.tag == "EnemyShip") {
                Utils.SpawnAndDestroyParticleSystem(onCollisionEffect, transform.position);
                health.TakeDamage(Constants.PLAYER_COLLISION_DAMAGE);
            }
        }
    
    }

}