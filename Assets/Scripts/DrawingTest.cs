using System;
using UnityEngine;

public class DrawingTest : MonoBehaviour
{
    [SerializeField] private GameObject boy;
    [SerializeField] private GameObject girl;
    
    [SerializeField] private LineRenderer lineRendererBoy;
    [SerializeField] private LineRenderer lineRendererGirl;
    
    [SerializeField] private GameObject finishBoy;
    [SerializeField] private GameObject finishGirl;
    
    private bool _boyHited;
    private bool _girlHited;
    private bool _boyFinished;
    private bool _girlFinished;
    private int _countFinishedLines;

    private void Start()
    {
        lineRendererBoy.SetPosition(0, boy.transform.position);
        lineRendererGirl.SetPosition(0, girl.transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if ((hit.collider.gameObject == boy || _boyHited) & !_girlHited & !_boyFinished)
            {
                DrawBoyLine(worldPosition, hit);
            }
            if ((hit.collider.gameObject == girl || _girlHited) & !_boyHited & !_girlFinished)
            {
                DrawGirlLine(worldPosition, hit);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!_boyFinished)
            {
                DeleteLine(lineRendererBoy);
            }

            if (!_girlFinished)
            {
                DeleteLine(lineRendererGirl);
            }
        }
    }

    private void DrawBoyLine(Vector3 worldPosition, RaycastHit2D hit)
    {
        _boyHited = true;
        AddLinePoint(worldPosition, lineRendererBoy);
        
        if (hit.collider.gameObject == finishBoy)
        {
            _boyFinished = true;
            _boyHited = false;
            _countFinishedLines++;
            CheckStartRace();
        }
    }
    
    private void DrawGirlLine(Vector3 worldPosition, RaycastHit2D hit)
    {
        _girlHited = true;
        AddLinePoint(worldPosition, lineRendererGirl);
        
        if (hit.collider.gameObject == finishGirl)
        {
            _girlFinished = true;
            _girlHited = false;
            _countFinishedLines++;
            CheckStartRace();
        }
    }
    
    public void AddLinePoint(Vector3 point, LineRenderer lineRenderer)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition( lineRenderer.positionCount - 1, point);
    }

    public void DeleteLine(LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = 1;
    }

    private void CheckStartRace()
    {
        if (_countFinishedLines == 2)
        {
            var move = FindObjectOfType<CharacterMove>();
            move.CalculateSpeed();
            move.startRace = true;
        }
    }

}
