using UnityEngine;

namespace realima.rgb
{
    public interface IDamageable
    {
        bool IsPhasing { get; }

        void Damage(int amount, GameObject gameObject = null);
    }
}