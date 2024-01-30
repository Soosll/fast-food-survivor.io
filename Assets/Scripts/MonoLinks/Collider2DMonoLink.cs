using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class Collider2DMonoLink : MonoLink<Collider2DLink>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new Collider2DLink()
            {
                Collider2D = GetComponent<Collider2D>()
            });
        }
    }
}