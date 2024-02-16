using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 Random(this Vector3 vector3, float min, float max) => 
            new(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));

        public static Vector3 MinimalObjectScale(this Vector3 vector3) =>
            new Vector3(0.01f, 0.01f, 0.01f);
    }
}