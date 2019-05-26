using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleSpaceShooter.UI {
    public class MainMenuController : MonoBehaviour {
    
        public void PlayGame() {
			SceneManager.LoadScene(Constants.SCENE_INDEX_GAME);
        }

        public void QuitGame() {
            Application.Quit();
        }

    }
}