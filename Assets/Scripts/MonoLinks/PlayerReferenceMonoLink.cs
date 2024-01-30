using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class PlayerReferenceMonoLink : MonoLink<PlayerReferenceLink>
    {
        [field: SerializeField] public SpriteRenderer[] SpriteRenderers { get; private set; }

        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new PlayerReferenceLink()
            {
                SpriteRenderers = this.SpriteRenderers,
            });
        }
    }
}