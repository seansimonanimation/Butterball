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
   [SerializeField] Vector2 xLimits;
   [SerializeField] Vector2 zLimits;

    void Start(){
        touchData = TouchData.CreateInstance<TouchData>();
    }
    void Awake(){

        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["WorldInteraction/TouchPress"];
        touchPositionAction = playerInput.actions["WorldInteraction/TouchPosition"];
    }
    void FixedUpdate()
    {
        GetAndApplyTouchVector();
        Velocity = Velocity * touchPressAction.ReadValue<float>();
        float xVal = (transform.position.x + MotionVectorThisFrame.x*Velocity);
        float zVal = (transform.position.z + MotionVectorThisFrame.y*Velocity);
        transform.position = new Vector3(Mathf.Clamp(xVal,xLimits.x,xLimits.y),0.0f,Mathf.Clamp(zVal,zLimits.x,zLimits.y));

        //do the same thing we did for the foot and apply to foot.
    }

    private void GetAndApplyTouchVector(){
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
