using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.rgb
{
    public class CharacterVisuals : MonoBehaviour
    {
        [SerializeField]
        private CharacterEntity _character;

        [SerializeField]
        private GameObject _redVisual;
        [SerializeField]
        private GameObject _greenVisual;
        [SerializeField]
        private GameObject _blueVisual;

        private void Awake()
        {
            _character.data.ColorUpdateEvent += OnColorUpdate;
        }

        private void OnColorUpdate(Color color)
        {
            if (color == Color.red)
            {
                SetVisual(_redVisual);
            }
            else if (color == Color.green)
            {
                SetVisual(_greenVisual);
            }
            else if (Color.blue == Color.blue)
            {
                SetVisual(_blueVisual);
            }
        }

        private void SetVisual(GameObject visual)
        {
            Destroy(transform.GetChild(0));
            Instantiate(visual, transform);
        }
    }
}
