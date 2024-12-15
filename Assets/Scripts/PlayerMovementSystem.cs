using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial class PlayerMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        Entities
            .WithAll<PlayerInputComponent, PlayerComponents>()
            .ForEach((ref LocalTransform transform, in PlayerInputComponent input, in PlayerComponents speed) =>
            {
                float3 direction = new float3(input.MoveInput.x, 0, input.MoveInput.y);

                if (math.lengthsq(direction) > 0.01f)
                {
                    direction = math.normalize(direction) * speed.Speed;

                    transform.Position += direction * deltaTime;
                    transform.Rotation = quaternion.LookRotationSafe(direction, math.up());
                }
            }).ScheduleParallel();
    }
}

