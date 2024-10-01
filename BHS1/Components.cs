namespace BHS1;

public struct WallComponent
{
    public int Id;
    public Point Start;
    public Point End;
}

public struct BallComponent
{
    public Point Center;
    public double Radius;
    public Vector Speed;
    public Ball SceneObject; 
}