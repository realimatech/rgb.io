using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace realima.rgb
{
    public class CharacterEntity : MonoBehaviour
    {
        [SerializeField]
        private string TestName;
        public CharacterData data;

        public void Awake()
        {
            data = new CharacterData(TestName ?? "John Doe");
        }

    }

    [System.Serializable]
    public class CharacterData
    {
        public readonly string Name;

        [SerializeField]
        private int _healthMax = default;
        private int _health;

        private int _ammoMax;
        private int? _ammo;

        private Dictionary<Color, int> CP = new Dictionary<Color, int>();

        public int Score => CP.Values.ToArray().Sum();

        public Action<Color> ColorUpdateEvent;

        public CharacterData(string name)
        {
            this.Name = name;
            this._health = _healthMax;
        }

        public void IncrementPoint(Color color, int amount)
        {
            if (CP.ContainsKey(color))
                CP[color] += amount;
            else
                CP.Add(color, amount);

            ColorUpdateEvent?.Invoke(GetColor());
        }

        public Color GetColor()
        {
            KeyValuePair<Color, int> max = CP.OrderBy(p => p.Value).Last();
            if (max.Value > 0) return max.Key;
            return Color.black;
        }
    }
}
