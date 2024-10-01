using Leopotam.Ecs;
using BHS1;

public class BounceSystem : IEcsRunSystem
{
    private readonly EcsFilter<BallComponent> _ballFilter = null;
    private readonly EcsFilter<WallComponent> _wallFilter = null;

    public void Run()
    {
        foreach (int i in _ballFilter)
        {
            ref BallComponent ball = ref _ballFilter.Get1(i);

            foreach (int j in _wallFilter)
            {
                ref WallComponent wall = ref _wallFilter.Get1(j);

                if (IsColliding(ball, wall))
                {
                    if (IsVerticalWall(wall))
                    {
                        ball.Speed.X = -ball.Speed.X;
                    }
                    else
                    {
                        ball.Speed.Y = -ball.Speed.Y;
                    }

                    if (ball.SceneObject is Ball sceneBall)
                    {
                        sceneBall.Speed = ball.Speed;
                    }

                    Console.WriteLine($"Ball collided with wall ID: {wall.Id}");
                }
            }
        }
    }

    private bool IsColliding(BallComponent ball, WallComponent wall)
    {
        if (IsVerticalWall(wall))
        {
            return ball.Center.X >= Math.Min(wall.Start.X, wall.End.X) &&
                   ball.Center.X <= Math.Max(wall.Start.X, wall.End.X) &&
                   ball.Center.Y >= Math.Min(wall.Start.Y, wall.End.Y) &&
                   ball.Center.Y <= Math.Max(wall.Start.Y, wall.End.Y);
        }
        else
        {
            return ball.Center.Y >= Math.Min(wall.Start.Y, wall.End.Y) &&
                   ball.Center.Y <= Math.Max(wall.Start.Y, wall.End.Y) &&
                   ball.Center.X >= Math.Min(wall.Start.X, wall.End.X) &&
                   ball.Center.X <= Math.Max(wall.Start.X, wall.End.X);
        }
    }

    private bool IsVerticalWall(WallComponent wall)
    {
        return wall.Start.X == wall.End.X;
    }
}