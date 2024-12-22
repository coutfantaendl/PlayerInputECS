using Unity.Entities;

public struct PlayerComponents : IComponentData
{
    public float Speed;
    public float DashSpeed;
    public float DashDuration;
}