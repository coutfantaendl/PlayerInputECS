using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAuthoring : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    public class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerInputComponent { MoveInput = float2.zero });
            AddComponent(entity, new PlayerComponents { Speed = authoring._speed });
        }
    }
}
