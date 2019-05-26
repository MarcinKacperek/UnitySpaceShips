using System;
using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Control {
    public class Wave : MonoBehaviour {

        [SerializeField] private GameObject enemyPrefab = null;
        [SerializeField] private int enemiesToSpawn = 0;
        private int enemiesSpawned = 0;
        public bool FinishedSpawning {
            get { return enemiesToSpawn == enemiesSpawned; }
        }
        private int enemiesLeft = 0;
        [SerializeField] private float spawnInterval = 0.0f;
        private float nextSpawnTime = 0.0f;

        private WavePathNode[] nodes;

		public System.Action<Health> OnEnemySpawn {
            get; set;
        }
        public System.Action<Health> OnEnemyDeath {
            get; set;
        }

        void Awake() {
            nodes = transform.GetComponentsInChildren<WavePathNode>();
        }

        void Start() {
            enemiesLeft = enemiesToSpawn;
        }

        void Update() {
            if (enemiesLeft == 0) {
                Destroy(gameObject);
                return;
            }
            
            SpawnEnemy();
        }

        void SpawnEnemy() {
            if (enemiesSpawned < enemiesToSpawn && nextSpawnTime <= Time.time) {
                GameObject newEnemy = GameObject.Instantiate(enemyPrefab, nodes[0].transform.position, enemyPrefab.transform.rotation);
                newEnemy.transform.parent = transform;
                Health newEnemyHealth = newEnemy.GetComponent<Health>();
                newEnemyHealth.OnDie += DestroyEnemy;
                if (OnEnemySpawn != null) {
                    OnEnemySpawn(newEnemyHealth);
                }

                nextSpawnTime = Time.time + spawnInterval;
                enemiesSpawned++;
            }
        }

        public WavePathNode GetPathNode(int index) {
            if (index < 0 || index >= nodes.Length) {
                return null;
            }

            return nodes[index];
        }

        void DestroyEnemy(GameObject enemy) {
            Health enemyHealth = enemy.GetComponent<Health>();
            OnEnemyDeath(enemyHealth);
            RemoveEnemy(enemy);
        }

        public void RemoveEnemy(GameObject enemy) {
            enemiesLeft--;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.cyan;

            WavePathNode[] nodes = transform.GetComponentsInChildren<WavePathNode>();
            WavePathNode previousNode = null;
            foreach (WavePathNode node in nodes) {
                Gizmos.DrawSphere(node.transform.position, 0.2f);
                if (previousNode != null) {
                    Gizmos.DrawLine(node.transform.position, previousNode.transform.position);
                }
                previousNode = node;
            }    
        }

    }
}