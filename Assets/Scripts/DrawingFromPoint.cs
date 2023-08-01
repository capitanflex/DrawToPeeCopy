using UnityEngine;

public class DrawingFromPoint : Drawing
{
    private LineRenderer lineRenderer;
    private bool WasHited = false;
    private bool Finished = false;
    [SerializeField] private GameObject finish;

    [SerializeField] private GameObject targetForDraw;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
    }
    
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit  || WasHited)
            {
                if (hit.collider.gameObject == targetForDraw  || WasHited)
                {
                    WasHited = true;
                    AddPoint(worldPosition, lineRenderer);
                    
                    if (hit.collider.gameObject == finish)
                    {
                        Finished = true;
                        WasHited = false;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            WasHited = false;
            if (!Finished)
            {
                DeletePoints(lineRenderer);
            }
            else
            {
                countFinishedLines++;
            }
        }
    }
}