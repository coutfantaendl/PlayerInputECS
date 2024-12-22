using Unity.Entities;
using UnityEngine;

public struct DashStateComponent : IComponentData
{
    public float RemainingDashTime;
}
