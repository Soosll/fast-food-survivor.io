using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class SpriteRendererReferenceMonoLink : MonoLink<SpriteRendererReferenceLink>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new SpriteRendererReferenceLink()
            {
                SpriteRenderer = _spriteRenderer
            });
        }
    }
}