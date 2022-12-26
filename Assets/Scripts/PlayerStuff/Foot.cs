using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public Vector3 CurrentPos;
    public Vector3 PreviousPos;
    public float Velocity;
    public FootData footData;
    public Vector3 MotionVectorThisFrame;
    Vector3 startPos;
    [SerializeField] float multYAmount;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        footData = FootData.CreateInstance<FootData>();
        CurrentPos = transform.position;
        PreviousPos = new Vector3(0f,0f,0f);
        Velocity = 0.0f;
        footData.PrevFramePos = PreviousPos;
        footData.storedVelocity = Velocity;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        VelocitySetup();
        float newYpos = (Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.z - startPos.z)) * multYAmount;
        transform.position = new Vector3 (transform.position.x,newYpos, transform.position.z);
        



        //At the end, replace the previous frame data with the new frame data.
        StoreDataForNextFrame();

    }
    private void VelocitySetup()
    {
        PreviousPos = footData.PrevFramePos;
        CurrentPos = transform.position;
        Velocity = (CurrentPos-PreviousPos).magnitude;
        MotionVectorThisFrame = FindMotionVector(CurrentPos, PreviousPos);
    }
    private void StoreDataForNextFrame()
    {
        footData.storedMotionVector = MotionVectorThisFrame;
        footData.storedVelocity = Velocity;
        footData.PrevFramePos = CurrentPos;
    }

    Vector3 FindMotionVector(Vector3 Cur, Vector3 Prev){
        return (Cur-Prev).normalized;
    }

    void OnTriggerEnter(Collider other) {
        Rigidbody ballRB = other.GetComponent<Rigidbody>();
        ballRB.velocity = MotionVectorThisFrame * Velocity * 60.0f;
    }
}
