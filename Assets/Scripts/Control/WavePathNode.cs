using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpaceShooter.Control {
    public class WavePathNode : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        [SerializeField] private float speedFractionToThisNode = 1.0f;
        public float SpeedFractionToThisNode {
            get { return speedFractionToThisNode; }
        }

    }
}
