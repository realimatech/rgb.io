using System;
using UnityEngine;

namespace realima.rgb
{
    public interface ISpawnable
    {
        Action<GameObject> despawnEvent { get; set; }
    }
}