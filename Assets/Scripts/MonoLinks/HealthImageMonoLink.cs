using Leopotam.Ecs;
using MonoLinks.Links;
using UnityEngine;
using UnityEngine.UI;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class HealthImageMonoLink : MonoLink<HealthImageLink>
    {
        [SerializeField] private Image _healthImage;

        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new HealthImageLink()
            {
                Image = _healthImage
            });
        }
    }
}