using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanZoomOrthoCam : MonoBehaviour
{
    static PanZoomOrthoCam instance;
    public static PanZoomOrthoCam Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<PanZoomOrthoCam>();
            return instance;
        }
    }

    [SerializeField]
    protected float zoomMin = 1, zoomMax = 8;
    [SerializeField] protected float mouseZoomSpeed = 1f;
    [SerializeField] protected float panDeadZone = 0.5f;
    [SerializeField] bool m_isPanning = false;
    
    Vector3 m_touchStartPos;


    void Update()
    {
        if (GameManager.Instance.IsPriorityAvailable(gameObject) && GameManager.Instance.IsPointerOverUI() == false)
        {
            Pan();
            Zoom(Input.GetAxis("Mouse ScrollWheel"), mouseZoomSpeed);
        }
    }

    void Pan()
    {
        // Get start pos position
        if (Input.GetMouseButtonDown(0))
        {
            m_touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Detech pinch for zoom
        if (Input.touchCount == 2)
        {
            Touch m_firstTouch = Input.GetTouch(0);
            Touch m_secondTouch = Input.GetTouch(1);

            Vector2 m_firstTouchOrigin = m_firstTouch.position - m_firstTouch.deltaPosition;
            Vector2 m_secondTouchOrigin = m_secondTouch.position - m_secondTouch.deltaPosition;

            float m_prevMagnitude = (m_firstTouchOrigin - m_secondTouchOrigin).magnitude;
            float m_currMagnitude = (m_firstTouch.position - m_secondTouch.position).magnitude;

            float m_diff = m_currMagnitude - m_prevMagnitude;

            Zoom(m_diff * 0.01f);
        }

        // Panning
        else if (Input.GetMouseButton(0))
        {
            Vector3 m_cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float m_zone = Vector3.Distance(m_touchStartPos, m_cam);

            if (m_zone >= panDeadZone)
            {
                m_isPanning = true;
                Vector3 dir = m_touchStartPos - m_cam;
                Camera.main.transform.position += dir;
            }
        } else if (Input.GetMouseButtonUp(0))
        {
            m_isPanning = false;
        }
    }

    void Zoom(float increment)
    {
        Zoom(increment, 1f);
    }

    void Zoom(float increment, float zoomSpeed)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment * zoomSpeed, zoomMin, zoomMax);
    }

    public bool IsPanning()
    {
        return m_isPanning;
    }
}
