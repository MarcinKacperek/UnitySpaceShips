using System;
using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Manager {
    
    public class GameManager : MonoBehaviour {
        
        public Action OnGameLostAction {
            get; set;
        }

        public bool Paused {
            get; private set;
        }

        public bool Finished {
            get; private set;
        }

        void Start() {
            GameObject player = GameObject.FindWithTag("Player");
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.OnDie += LoseGame;

            Time.timeScale = 1.0f;
        }

        private void LoseGame(GameObject player) { 
            OnGameLostAction();
        }

        public void PauseGame() {
            Time.timeScale = 0.0f;
            Paused = true;
        }

        public void ResumeGame() {
            Time.timeScale = 1.0f;
            Paused = false;
        }

    }

}