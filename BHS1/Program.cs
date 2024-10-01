using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using BHS1;
using System.Threading;

public abstract class SceneObject
{
    public abstract void Draw();
}

public class Wall : SceneObject
{
    public int Id { get; set; }
    public Point Start { get; set; }
    public Point End { get; set; }

    public Wall(int id, Point start, Point end)
    {
        Id = id;
        Start = start;
        End = end;
    }

    public override void Draw()
    {
        Console.WriteLine($"Drawing wall {Id} from {Start} to {End}");
    }
}

public class Ball : SceneObject
{
    public Point Center { get; set; }
    public double Radius { get; set; }
    public Vector Speed { get; set; }

    public Ball(Point center, double radius, Vector speed)
    {
        Center = center;
        Radius = radius;
        Speed = speed;
    }

    public override void Draw()
    {
        Console.WriteLine($"Drawing ball at {Center} with radius {Radius} units");
    }
}

public class Scene
{
    public List<SceneObject> Objects { get; set; } = new List<SceneObject>();

    public void AddObject(SceneObject obj)
    {
        Objects.Add(obj);
    }

    public void DrawScene()
    {
        foreach (SceneObject obj in Objects)
        {
            obj.Draw();
        }
    }
}

public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}

public struct Vector
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vector(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}

public class Program
{
    public static void Main()
    {
        EcsWorld world = new EcsWorld();
        EcsSystems systems = new EcsSystems(world);

        systems
            .Add(new DrawSystem())
            .Add(new MoveSystem())
            .Add(new BounceSystem()) 
            .Init();

        Scene scene = new Scene();

        scene.AddObject(new Wall(1, new Point(0, 0), new Point(10, 0)));
        scene.AddObject(new Wall(2, new Point(10, 0), new Point(10, 10)));
        scene.AddObject(new Wall(3, new Point(10, 10), new Point(0, 10)));
        scene.AddObject(new Wall(4, new Point(0, 10), new Point(0, 0)));

        Ball ball = new Ball(new Point(3, 5), 1, new Vector(1, 1));
        scene.AddObject(ball);

        foreach (SceneObject obj in scene.Objects)
        {
            if (obj is Wall wall)
            {
                CreateWallEntity(world, wall.Id, wall.Start, wall.End);
            }
            else if (obj is Ball sceneBall)
            {
                CreateBallEntity(world, sceneBall.Center, sceneBall.Radius, sceneBall.Speed, sceneBall);
            }
        }

        while (true)
        {
            systems.Run();

            Console.WriteLine($"Ball position: {ball.Center}");

            Thread.Sleep(100);
        }

        // Cleanup
        systems.Destroy();
        world.Destroy();
    }

    private static void CreateWallEntity(EcsWorld world, int id, Point start, Point end)
    {
        EcsEntity entity = world.NewEntity();
        ref WallComponent wall = ref entity.Get<WallComponent>();
        wall.Id = id;
        wall.Start = start;
        wall.End = end;
    }

    private static void CreateBallEntity(EcsWorld world, Point center, double radius, Vector speed, Ball sceneBall)
    {
        EcsEntity entity = world.NewEntity();
        ref BallComponent ball = ref entity.Get<BallComponent>();
        ball.Center = center;
        ball.Radius = radius;
        ball.Speed = speed;
        ball.SceneObject = sceneBall; 
    }
}