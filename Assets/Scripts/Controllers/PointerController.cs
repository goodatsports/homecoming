using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PointerController : MonoBehaviour
{
    public Transform playerTransform;
    public SpriteRenderer Sprite;
    public ActionQuadrantController ActionQuadrant;
    private Vector3 MousePos;
    private float MouseAngle;

    //Unit circle offset for calculating which ActionQuadrant is active; i.e. center point of "Up" quadrant (135 degrees) is straight up from character as opposed to over the top left corner 
    private const float QUADRANT_ANGLE_OFFSET = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        ActionQuadrant = GameObject.Find("ActionQuadrant").GetComponent<ActionQuadrantController>();
        if (ActionQuadrant == null) print("ACTION QUADRANT NOT FOUND");
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        Sprite.enabled = false;
    }

    void LateUpdate()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        MouseAngle = GetMouseAngle(MousePos);
        float mouseAngleRads = Mathf.Deg2Rad * MouseAngle;

        //Set active mouse quadrant
        SetActionQuadrant(GetMouseQuadrant());

        //Rotate sprite to match angle position of pointer relative to character
        transform.rotation = Quaternion.AngleAxis(GetMouseAngle(MousePos) - 90, Vector3.forward);
        Vector3 towardsMouse = new Vector3(Mathf.Cos(mouseAngleRads), Mathf.Sin(mouseAngleRads));

        transform.position = playerTransform.position + towardsMouse;
    }

    float GetMouseAngle(Vector3 MousePos)
    {
        Vector3 difference = MousePos - playerTransform.position;
        difference.Normalize();
        return Mathf.Rad2Deg * Mathf.Atan2(difference.y, difference.x);
        

    }

    public Vector3 GetMouseQuadrant()
    {
        return GetMouseQuadrant(MouseAngle);
    }

    private Vector3 GetMouseQuadrant(float angle)
    {
        // Removing sign from angle, clamping it to 360 degrees, and applying angle offset
        angle = (angle + 360) % 360 + QUADRANT_ANGLE_OFFSET;
        if (angle % 360 <= 90) return Vector3.right;
        if (angle % 360 <= 180) return Vector3.up;
        if (angle % 360 <= 270) return Vector3.left;
        else return Vector3.down;
    }

    private void SetActionQuadrant(Vector3 direction)
    {
        ActionQuadrant.SetActiveQuadrant(direction);
    }

    public void Show()
    {
        Sprite.enabled = true;
    }

    public void Hide()
    {
        Sprite.enabled = false;
    }
}
