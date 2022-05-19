using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.rgb
{
    public class PowerUpEntity : MonoBehaviour
    {
        public Color Color = Color.red;
        public int Points = 1;

        [SerializeField]
        private MeshFilter _mesh;
        [SerializeField]
        private MeshRenderer _render;

        private void OnTriggerEnter(Collider other)
        {
            CharacterEntity character = other.GetComponent<CharacterEntity>() ?? other.GetComponentInParent<CharacterEntity>();
            if (character != null)
            {
                character.data.IncrementPoint(Color, Points);
                Destroy(this.gameObject);
            }
        }
    }
}
