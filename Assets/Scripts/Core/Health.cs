using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using UnityEngine;

namespace SimpleSpaceShooter.Core {
    public class Health : MonoBehaviour {
        
        [SerializeField] private float maxHealth = 0;
        public float MaxHealth {
            get { return maxHealth; }
        }
        private float currentHealth;
        public float CurrentHealth {
            get { return currentHealth; }
        }
        [SerializeField] private ParticleSystem onDestroyEffect = null; 

        private Coroutine flashCoroutine;
        private SpriteRenderer[] spriteRenderers;
        private Color[] defaultColors;

        private bool dead = false;

        public System.Action<Health> OnChangeHealth {
            get; set;
        }
        public System.Action<GameObject> OnDie {
            get; set;
        }
        
        void Awake() {
            spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
            defaultColors = new Color[spriteRenderers.Length];
            for (int i = 0; i < spriteRenderers.Length; i++) {
                defaultColors[i] = spriteRenderers[i].color;
            }
        }

        void Start() {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage) {
            if (flashCoroutine != null) {
                StopCoroutine(flashCoroutine);
                flashCoroutine = null;
            }
            flashCoroutine = StartCoroutine(FlashAnimation());

            currentHealth = Mathf.Max(currentHealth - damage, 0.0f);
            if (OnChangeHealth != null) {
                OnChangeHealth(this);
            }
            if (!dead && currentHealth <= 0.0f) {
                Die();
            }
        }

        public void HealDamage(float amount) {
            if (dead) return;

            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            if (OnChangeHealth != null) {
                OnChangeHealth(this);
            }
        }

        void Die() {
            dead = true;

            if (flashCoroutine != null) {
                StopCoroutine(flashCoroutine);
            }
            if (onDestroyEffect != null) {
                Utils.SpawnAndDestroyParticleSystem(onDestroyEffect, transform.position);
            }
            if (OnDie != null) {
                OnDie(gameObject);
            }

            Destroy(gameObject);
        }
    
        IEnumerator FlashAnimation() {
            for (int i = 0; i < 3; i++) {
                SetSpriteRenderersColor(false);
                yield return new WaitForSeconds(0.05f);
                SetSpriteRenderersColor(true);
                yield return new WaitForSeconds(0.05f);
            }

            flashCoroutine = null;
        }

        void SetSpriteRenderersColor(bool defaultColor) {
            for (int i = 0; i < spriteRenderers.Length; i++) {
                spriteRenderers[i].color = defaultColor ? defaultColors[i] : Color.white;
            }
        }

    }
}
