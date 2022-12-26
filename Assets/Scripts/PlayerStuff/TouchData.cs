using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newTouchData", menuName ="Data/Player Touch Data")]
public class TouchData : ScriptableObject
{
    public Vector2 storedMotionVector;
    public float storedVelocity;
    public Vector2 PrevFramePos;
}
