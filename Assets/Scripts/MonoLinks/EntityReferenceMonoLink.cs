using Leopotam.Ecs;
using MonoLinks.Links;
using Zun010.MonoLinks;

namespace MonoLinks
{
    public class EntityReferenceMonoLink : MonoLink<EntityLink>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Replace(new EntityLink()
            {
                Entity = entity
            });
        }
    }
}