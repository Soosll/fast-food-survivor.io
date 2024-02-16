using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class AnimatorMonoLink : MonoLink<AnimatorLink>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new AnimatorLink()
            {
                Animator = GetComponent<Animator>()
            });
        }
    }
}