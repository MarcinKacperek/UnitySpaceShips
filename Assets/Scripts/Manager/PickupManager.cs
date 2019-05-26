using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Core;
using SimpleSpaceShooter.Movement;
using UnityEngine;

namespace SimpleSpaceShooter.Manager {
    
    public class PickupManager : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        [SerializeField] private float pickupDropChance = 0.0f;
        [SerializeField] private AbstractPickup[] pickups = null;

        void Start() {
            WaveManager waveManager = GetComponent<WaveManager>();
            waveManager.OnEnemyDeath += DropRandomPickup;
        }

        private void DropRandomPickup(Health enemyHealth) {
            if (pickupDropChance >= Random.Range(0.0f, 1.0f)) {
                GameObject.Instantiate(pickups[Random.Range(0, pickups.Length)], enemyHealth.transform.position, Quaternion.identity);
            }
        }

    }

}