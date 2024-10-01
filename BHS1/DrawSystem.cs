namespace BHS1;

using Leopotam.Ecs;

public class DrawSystem : IEcsRunSystem
{
    private readonly EcsFilter<WallComponent> _wallFilter = null;
    private readonly EcsFilter<BallComponent> _ballFilter = null;

    public void Run()
    {
        foreach (var i in _wallFilter)
        {
            ref var wall = ref _wallFilter.Get1(i);
        }

        foreach (var i in _ballFilter)
        {
            ref var ball = ref _ballFilter.Get1(i);
        }
    }
}