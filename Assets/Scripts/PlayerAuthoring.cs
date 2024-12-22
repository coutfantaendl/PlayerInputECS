using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAuthoring : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;

    public class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerInputComponent { MoveInput = float2.zero, IsDashing = false });
            AddComponent(entity, new PlayerComponents 
            { 
                Speed = authoring._speed,
                DashSpeed = authoring._dashSpeed,
                DashDuration = authoring._dashDuration,
            });
            AddComponent(entity, new DashStateComponent { RemainingDashTime = 0 });
        }
    }
}
