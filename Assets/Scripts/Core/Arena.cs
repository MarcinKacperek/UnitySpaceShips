using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace SimpleSpaceShooter.Core {
    public class Arena : MonoBehaviour {

        [SerializeField] private Transform leftLine = null;
        [SerializeField] private Transform rightLine = null;
        [SerializeField] private float arenaMargin = 0.0f;
        public float ArenaMargin {
            get { return arenaMargin; }
        }

        private float arenaHeight = 0.0f;
        public float ArenaHeight {
            get { return arenaHeight; }
        }
        private float arenaWidth = 0.0f;
        public float ArenaWidth {
            get { return arenaWidth; }
        }

        void Awake() {
            PixelPerfectCamera camera = Camera.main.GetComponent<PixelPerfectCamera>();
            arenaHeight = (float) camera.refResolutionY / camera.assetsPPU;
            arenaWidth = 3.0f * arenaHeight / 5.0f; // 3:5 Aspect ratio 
            float linePositionX = arenaWidth / 2.0f;

            leftLine.transform.position = new Vector2(-linePositionX, 0.0f);
            rightLine.transform.position = new Vector2(linePositionX, 0.0f);
        }

    }
}
