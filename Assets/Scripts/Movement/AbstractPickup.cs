using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using UnityEngine;

namespace SimpleSpaceShooter.Movement {

	public abstract class AbstractPickup : MonoBehaviour {
		
        [SerializeField] private float duration = 0.0f;
        [SerializeField] private ParticleSystem onPickupEffect = null;

        void Start() {
            Destroy(gameObject, duration);
        }

        public abstract void OnPickup(GameObject player);

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                if (onPickupEffect != null) {
                    Utils.SpawnAndDestroyParticleSystem(onPickupEffect, transform.position);
                }

                OnPickup(other.gameObject);
                Destroy(gameObject);
            }    
        }

	}

}