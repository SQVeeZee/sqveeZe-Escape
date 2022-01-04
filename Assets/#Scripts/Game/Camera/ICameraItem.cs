using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraItem
{
    Camera Camera { get; }
    ECameraType CameraType { get; }
}
