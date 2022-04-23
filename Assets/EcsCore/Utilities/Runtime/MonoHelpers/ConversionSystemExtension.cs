namespace Leopotam.Ecs
{
    public static class ConversionSystemExtension
    {
        public static EcsSystems ConvertScene(this EcsSystems ecsSystems)
        {
            ecsSystems.Add(new WorldInitSystem());
            return ecsSystems;
        }
    }
}
