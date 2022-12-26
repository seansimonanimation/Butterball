using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newFootData", menuName ="Data/Player Foot Data")]
public class FootData : ScriptableObject
{
    public Vector3 storedMotionVector;
    public float storedVelocity;
    public Vector3 PrevFramePos;
}
