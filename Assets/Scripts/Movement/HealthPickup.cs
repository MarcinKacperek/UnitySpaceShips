using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Movement {

	public class HealthPickup : AbstractPickup {

        [SerializeField] private float healAmount = 0.0f;

		public override void OnPickup(GameObject player) {
            Health health =  player.GetComponent<Health>();
            health.HealDamage(healAmount);
        }

	}

}