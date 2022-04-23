namespace Leopotam.Ecs
{
    public static class WorldHandler
	{
    	private static EcsWorld _world;
    
    	public static void Init(EcsWorld ecsWorld) 
    	{
        	_world = ecsWorld;
    	}
    	public static EcsWorld GetWorld()
    	{
        	return _world;
    	}

    	public static void Destroy()
    	{
        	_world = null;
    	}
	}
}
