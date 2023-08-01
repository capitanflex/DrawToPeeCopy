using UnityEngine;

public class Drawing: MonoBehaviour
{
    public int countFinishedLines;
    public void AddPoint(Vector3 point, LineRenderer lineRenderer)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition( lineRenderer.positionCount - 1, point);
    }

    public void DeletePoints(LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = 1;
    }

    private void Update()
    {
        if (countFinishedLines == 2)
        { 
            var Move = FindObjectOfType<CharacterMove>();
            Move.CalculateSpeed();
            Move.startRace = true;
        }
    }
}