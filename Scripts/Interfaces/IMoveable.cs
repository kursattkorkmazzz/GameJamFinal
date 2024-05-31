using UnityEngine;

public interface IMoveable
{

    float MaxRunSpeed { get; set; }
    float CurrentRunSpeed { get; }
    float NormalRunSpeed { get; }
    float JumpSpeed { get; set; }

}
