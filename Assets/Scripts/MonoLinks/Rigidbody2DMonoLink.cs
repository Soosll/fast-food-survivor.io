using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class Rigidbody2DMonoLink : MonoLink<Rigidbody2DLink>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new Rigidbody2DLink()
            {
                Rigidbody2D = GetComponent<Rigidbody2D>()
            });
        }
    }
}