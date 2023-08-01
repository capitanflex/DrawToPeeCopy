using System;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public LineRenderer line1; 
    public LineRenderer line2; 
    public float timeToFinish = 2f; 

    public bool startRace;

    [SerializeField] private GameObject boy;
    [SerializeField] private GameObject girl;

    private float distance1; 
    private float distance2; 
    private float currentPos1 = 0f; 
    private float currentPos2 = 0f; 
    private float speed1; 
    private float speed2; 

    

    void Update()
    {
        if (startRace)
        {
            Move();
        }
    }

    private void OnEnable()
    {
        GameEvents.OnLevelFailed += RaceFailed;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelFailed -= RaceFailed;
    }

    public void CalculateSpeed()
    {
        distance1 = line1.GetComponent<LineRenderer>().positionCount; 
        distance2 = line2.GetComponent<LineRenderer>().positionCount; 
        
        speed1 = distance1 / timeToFinish;
        speed2 = distance2 / timeToFinish;
    }
    
    private void Move()
    {
        currentPos1 += Time.deltaTime * speed1 / distance1;
        currentPos1 = Mathf.Clamp01(currentPos1); 
            
        currentPos2 += Time.deltaTime * speed2 / distance2;
        currentPos2 = Mathf.Clamp01(currentPos2);

        boy.transform.position = line1.GetPosition((int)(currentPos1 * (distance1 - 1)));
        girl.transform.position = line2.GetPosition((int)(currentPos2 * (distance2 - 1)));

        if (currentPos1 >= 1f)
        {
            currentPos1 = 1f;
            speed1 = 0f;
        }

        if (currentPos2 >= 1f)
        {
            currentPos2 = 1f;
            speed2 = 0f;
        }
    }

    private void RaceFailed()
    {
        startRace = false;
    }
}

