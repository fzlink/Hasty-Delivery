using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoThrowController : MonoBehaviour
{
    private Vector2 LAUNCH_VELOCITY = new Vector2(20f, 40f);
    private Vector2 INITIAL_POSITION = Vector2.zero;
    private Vector3 scanPos;
    private readonly Vector2 GRAVITY = new Vector2(0f, -240f);
    private const float DELAY_UNTIL_LAUNCH = 1f;
    private int NUM_DOTS_TO_SHOW = 10;
    private float DOT_TIME_STEP = 0.02f;

    private bool launched = false;
    private float timeUntilLaunch = DELAY_UNTIL_LAUNCH;

    private Rigidbody rigidbody;

    public GameObject trajectoryDotPrefab;

    private bool onHold;
    private Vector3 offset;
    private Vector3 screenPoint;

    public Transform trajectoryContainer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        INITIAL_POSITION = transform.position;
        scanPos = transform.position;
        DrawTrajectory();
    }

    private void DrawTrajectory()
    {
        for (int i = 0; i < trajectoryContainer.childCount; i++)
        {
            Destroy(trajectoryContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
        {
            GameObject trajectoryDot = Instantiate(trajectoryDotPrefab,trajectoryContainer);
            trajectoryDot.transform.position = CalculatePosition(DOT_TIME_STEP * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
            offset = touchPoint - Camera.main.WorldToScreenPoint(scanPos);
            LAUNCH_VELOCITY.y = offset.y;
            DrawTrajectory();
            /*
            Vector3 point = new Vector3();
            Event currentEvent = Event.current;
            Vector2 mousePos = new Vector2();

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.

            mousePos.x = Input.mousePosition.x;
            mousePos.y = Camera.main.pixelHeight - Input.mousePosition.y;
            onHold = true;

            screenPoint = Camera.main.WorldToScreenPoint(scanPos);
            point = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            offset = scanPos - point;*/
        }
        else if (Input.GetMouseButtonUp(0))
        {
            launched = true;
            rigidbody.isKinematic = false;
            rigidbody.velocity = LAUNCH_VELOCITY;
        }
       /*if (launched)
        {
            rigidbody.AddForce(LAUNCH_VELOCITY);
        }*/
    }

    private void OnMouseDrag()
    {
        /*Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;*/

    }

    private Vector2 CalculatePosition(float elapsedTime)
    {
        return GRAVITY * elapsedTime * elapsedTime * 0.5f + LAUNCH_VELOCITY * elapsedTime + INITIAL_POSITION;
    }

}
