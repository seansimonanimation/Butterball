using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float LookSpeed;
    [SerializeField] GameObject ball;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(new Vector3(ball.transform.position.x,ball.transform.position.y-1.0f,ball.transform.position.z));
    }

        public void FaceTarget(Vector3 whereToGo){
        //transform.rotation = where the target is, we need to rotate at a certain speed
        Vector3 direction = (whereToGo-cam.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,direction.y,direction.z));
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation,lookRotation,Time.deltaTime*LookSpeed);
    }
}
