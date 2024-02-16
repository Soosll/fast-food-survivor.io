using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class BoxCollider2DMonoLink : MonoLink<BoxCollider2DLink>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new BoxCollider2DLink()
            {
                BoxCollider2D = GetComponent<BoxCollider2D>()
            });
        }
    }
}