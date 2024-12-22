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
            .ForEach((ref LocalTransform transform, ref DashStateComponent dashState,
                       in PlayerInputComponent input, in PlayerComponents player) =>
            {
                float3 direction = new float3(input.MoveInput.x, 0, input.MoveInput.y);

                if (math.lengthsq(direction) > 0.01f)
                {
                    direction = math.normalize(direction);
                }
                else
                {
                    direction = float3.zero;
                }
     
                if (input.IsDashing && dashState.RemainingDashTime <= 0)
                {
                    dashState.RemainingDashTime = math.clamp(player.DashDuration, 0.05f, 0.2f); 
                }

                if (dashState.RemainingDashTime > 0)
                {
                    float dashSpeed = math.clamp(player.DashSpeed, 1f, 10f); 
                    transform.Position += direction * dashSpeed * deltaTime;
                    dashState.RemainingDashTime -= deltaTime;
                }         
                else if (math.lengthsq(direction) > 0.01f)
                {
                    transform.Position += direction * player.Speed * deltaTime;
                }

                if (math.lengthsq(direction) > 0.01f)
                {
                    transform.Rotation = quaternion.LookRotationSafe(direction, math.up());
                }

            }).ScheduleParallel();
    }
}

