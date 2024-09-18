namespace Haptic.Prototype
{
    using UnityEngine;
    public static class BoundsExtension
    {
        public static Vector3 GetRandomPositionInBoxCollider(this Bounds bound)
        {
            return new Vector3(Random.Range(bound.min.x, bound.max.x),
                Random.Range(bound.min.y, bound.max.y),
                Random.Range(bound.min.z, bound.max.z));
        }
    }
}