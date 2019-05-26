using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpaceShooter.Common {
    
    public class Utils {
        
        public static void SpawnAndDestroyParticleSystem(ParticleSystem particleSystem, Vector3 position) {
            ParticleSystem spawneParticleSystem = GameObject.Instantiate(particleSystem, position, Quaternion.identity);
            GameObject.Destroy(spawneParticleSystem.gameObject, spawneParticleSystem.main.duration);
        }
    
    }

}