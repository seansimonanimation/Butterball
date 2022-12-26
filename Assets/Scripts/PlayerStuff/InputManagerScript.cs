using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerScript : MonoBehaviour
{
    TouchData touchData;
    PlayerInput playerInput;
    InputAction touchPositionAction;
    InputAction touchPressAction;
    Vector2 initialTouchPos;
    Vector2 currentTouchPos;
    float Velocity;
    Vector2 MotionVectorThisFrame;
    Vector2 PrevFramePos;

    void Start(){
        touchData = TouchData.CreateInstance<TouchData>();
    }
    void Awake(){

        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["WorldInteraction/TouchPress"];
        touchPositionAction = playerInput.actions["WorldInteraction/TouchPosition"];
    }
    void FixedUpdate(){
        GetTouchVector();
        Velocity = Velocity * touchPressAction.ReadValue<float>();
        transform.position = new Vector3((transform.position.x + MotionVectorThisFrame.x*Velocity),0.0f,(transform.position.z + MotionVectorThisFrame.y*Velocity));

        //do the same thing we did for the foot and apply to foot.
    }

    private void GetTouchVector(){
        if (initialTouchPos != Vector2.zero)
        {
            currentTouchPos = touchPositionAction.ReadValue<Vector2>();
            PrevFramePos = touchData.PrevFramePos;
            Velocity = (currentTouchPos - PrevFramePos).magnitude / 300.0f;
            MotionVectorThisFrame = FindMotionVector(currentTouchPos, touchData.PrevFramePos);

        } else {

        }
    }

    private void OnEnable() {
        touchPressAction.performed += TouchPressed;
    }
    private void OnDisable() {
        Velocity = 0.0f;
        touchPressAction.performed -= TouchPressed;
        initialTouchPos = Vector2.zero;
        MotionVectorThisFrame = Vector2.zero;

    }
    private void TouchPressed(InputAction.CallbackContext context){
        initialTouchPos = touchPositionAction.ReadValue<Vector2>();
        touchData.PrevFramePos = initialTouchPos;
    }
    Vector3 FindMotionVector(Vector2 Cur, Vector2 Prev){
        return (Cur-Prev).normalized;
    }
}
