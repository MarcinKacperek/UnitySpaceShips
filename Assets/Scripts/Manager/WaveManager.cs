using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using SimpleSpaceShooter.Control;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Manager {
    
    public class WaveManager : MonoBehaviour {
    
        [SerializeField] private float minWaveInterval = 0.0f;
        [SerializeField] private float maxWaveInterval = 0.0f;
        private float nextWaveTime = 0.0f;
        [SerializeField] private Wave[] wavePrefabs = null;
        private Wave currentWave = null;

        private LevelManager levelManager;

        public System.Action<Health> OnEnemySpawn {
            get; set;
        }
        public System.Action<Health> OnEnemyDeath {
            get; set;
        }

        void Awake() {
            levelManager = GetComponent<LevelManager>();
        }

        void Start() {
            nextWaveTime = Time.time + Constants.INITIAL_GAME_DELAY;
        }

        void Update() {
            if (levelManager.Finished) return;

            SpawnWave();
        }

        void SpawnWave() {
            if (currentWave != null) {
                if (currentWave.FinishedSpawning) {
                    nextWaveTime = Time.time + Random.Range(minWaveInterval, maxWaveInterval);
                    currentWave = null;
                }

                return;
            }

            if (nextWaveTime <= Time.time) {
                currentWave = GameObject.Instantiate(wavePrefabs[Random.Range(0, wavePrefabs.Length)]);
                currentWave.OnEnemySpawn += OnEnemySpawn;
                currentWave.OnEnemyDeath += OnEnemyDeath;
            }
        }

    }

}