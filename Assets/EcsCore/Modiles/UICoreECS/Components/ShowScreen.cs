namespace UICoreECS
{
    public struct ShowScreenTag
    {
        public int Layer;
        public int ID;
    }

    // tag to update screen view
    public struct ScreenUpdateTag : Leopotam.Ecs.IEcsIgnoreInFilter{}
}