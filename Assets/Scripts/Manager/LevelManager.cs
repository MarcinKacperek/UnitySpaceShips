using System;
using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Combat;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Manager {
    
    public class LevelManager : MonoBehaviour {

        private const float LEVEL_MULTIPLIER = 1.5f;

        private float levelDifficultyFactor = 1.0f;
        public float LevelDifficultyFactor {
            get { return levelDifficultyFactor; }
        }
        private int currentLevel = 1;
        public int CurrentLevel {
            get { return currentLevel; }
        }
        private int nextLevelPoints = 100;
        public int NextLevelPoints { 
            get { return nextLevelPoints; }
        }
        private int currentLevelPoints = 0;
        public int CurrentLevelPoints {
            get { return currentLevelPoints; }
        }
        private int totalPoints = 0;
        public int TotalPoints {
            get { return totalPoints; }
        }
    
        private WaveManager waveManager;

        public Action OnScoreChange {
            get; set;
        }

        public Action OnLevelChange {
            get; set;
        }

        void Awake() {
            waveManager = GetComponent<WaveManager>();
        }

        void Start() {
            waveManager.OnEnemySpawn += ApplyDifficultyFactor;
            waveManager.OnEnemyDeath += UpdateScore;
        }

        private void ApplyDifficultyFactor(Health enemyHealth) {
            Cannon[] cannons = enemyHealth.GetComponentsInChildren<Cannon>();
            foreach (Cannon cannon in cannons) {
                cannon.SetAttackSpeed(levelDifficultyFactor);
            }
        }
        
        private void UpdateScore(Health destroyedEnemy) {
            Enemy enemy = destroyedEnemy.GetComponent<Enemy>();

            totalPoints += (int) (enemy.Points * levelDifficultyFactor);
            currentLevelPoints += enemy.Points;

            UpdateLevel();
            OnScoreChange();
        }

        private void UpdateLevel() {
            if (currentLevelPoints >= nextLevelPoints) {
                // Increase level
                currentLevel++;
                levelDifficultyFactor *= LEVEL_MULTIPLIER;
                currentLevelPoints -= nextLevelPoints;
                nextLevelPoints = (int) (nextLevelPoints * LEVEL_MULTIPLIER);

                if (OnLevelChange != null) {
                    OnLevelChange();
                }
            }
        }

    }

}