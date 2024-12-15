using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(PlayerMovementSystem))]
public partial class PlayerInputSystem : SystemBase
{
    private InputAction _moveAction;
    private InputSystem_Actions _systemActions;

    protected override void OnCreate()
    {
        base.OnCreate();

        _systemActions = new InputSystem_Actions();
        _systemActions.Player.Enable();
        _moveAction = _systemActions.Player.Move;
    }

    protected override void OnUpdate()
    {
        if (_moveAction == null)
            return;

        float2 moveInput = (float2)_moveAction.ReadValue<Vector2>();

        Entities
            .WithAll<PlayerInputComponent>()
            .ForEach((ref PlayerInputComponent input) =>
            {
                input.MoveInput = moveInput;
            }).ScheduleParallel();
    }
}
