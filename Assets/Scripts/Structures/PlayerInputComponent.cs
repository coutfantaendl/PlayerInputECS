﻿using Unity.Entities;
using Unity.Mathematics;

public struct PlayerInputComponent : IComponentData
{
    public float2 MoveInput;
    public bool IsDashing;
}
