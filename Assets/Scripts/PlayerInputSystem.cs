using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(PlayerMovementSystem))]
public partial class PlayerInputSystem : SystemBase
{
    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputSystem_Actions _systemActions;

    protected override void OnCreate()
    {
        base.OnCreate();

        _systemActions = new InputSystem_Actions();
        _systemActions.Player.Enable();
        _moveAction = _systemActions.Player.Move;
        _dashAction = _systemActions.Player.Dash;
    }

    protected override void OnUpdate()
    {
        if (_moveAction == null && _dashAction == null)
            return;

        float2 moveInput = (float2)_moveAction.ReadValue<Vector2>();
        bool isDashing = _dashAction.WasPressedThisFrame();


        Entities
            .WithAll<PlayerInputComponent>()
            .ForEach((ref PlayerInputComponent input) =>
            {
                input.MoveInput = moveInput;
                input.IsDashing = isDashing;
            }).ScheduleParallel();
    }
}
