using Leopotam.Ecs;

namespace Modules.Root
{
    public interface ISystemsProvider
    {
        /// <param name="world">target world</param>
        /// <param name="endFrame">added as last nested system</param>
        /// <param name="mainSystems">used for injections</param>
        /// <returns>list of systems to add at root</returns>
        /// 
        EcsSystems GetSystems(EcsWorld world, EcsSystems endFrame, EcsSystems mainSystems);
        
    }
}
