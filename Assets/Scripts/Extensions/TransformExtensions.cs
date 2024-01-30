﻿using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static void Activate(this Transform transform)
            => transform.gameObject.SetActive(true);
        
        public static void Diactivate(this Transform transform)
            => transform.gameObject.SetActive(false);
    }
}