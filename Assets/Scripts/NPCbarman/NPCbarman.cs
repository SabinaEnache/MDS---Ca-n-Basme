using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGameNamespace
{
    public abstract class NPCbarman : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer _interactSprite; // Sprite-ul pentru săgețică
        private Transform _playerTransform;
        private const float INTERACT_DISTANCE = 5F;

        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && IsWithinInteractDistance())
            {
                //interact
                Interact();
            }

            if(_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
            {
                _interactSprite.gameObject.SetActive(false);
            }
            else if(!_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
            {
                _interactSprite.gameObject.SetActive(true);
            }
          
        }

        public abstract void Interact();

        private bool IsWithinInteractDistance()
        {
            if(Vector2.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE)
            {
                return true;
            }
            else 
            {
                return false; 
            }
        }
    }
}
