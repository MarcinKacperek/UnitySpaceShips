using System;
using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Manager {
    
    public class LevelManager : MonoBehaviour {
    
        [SerializeField] private int pointsToFinish = 0;
        public int PointsToFinish {
            get { return pointsToFinish; }
        }
        private int currentPoints = 0;
        public int CurrentPoints {
            get { return currentPoints; }
        }

        private WaveManager waveManager;

        private bool finished = false;
        public bool Finished {
            get { return finished; }
        }
    
        public Action<int> OnScoreChange {
            get; set;
        }

        void Awake() {
            waveManager = GetComponent<WaveManager>();
        }

        void Start() {
            waveManager.OnEnemyDeath += UpdateScore;
        }

        void UpdateScore(Health destroyedEnemy) {
            Enemy enemy = destroyedEnemy.GetComponent<Enemy>();
            currentPoints += enemy.Points;
            OnScoreChange(currentPoints);
        }

    }

}