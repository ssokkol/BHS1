using Leopotam.Ecs;
using BHS1;

public class MoveSystem : IEcsRunSystem
{
    private readonly EcsFilter<BallComponent> _ballFilter = null;

    public void Run()
    {
        foreach (var i in _ballFilter)
        {
            ref var ball = ref _ballFilter.Get1(i);
            ball.Center.X += ball.Speed.X;
            ball.Center.Y += ball.Speed.Y;

            if (ball.SceneObject is Ball sceneBall)
            {
                sceneBall.Center = ball.Center;
            }
        }
    }
}