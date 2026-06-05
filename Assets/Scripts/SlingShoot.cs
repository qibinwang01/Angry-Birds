using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShoot : MonoBehaviour
{
    public static SlingShoot Instance {get;private set;}
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform centerPoint;
    private Transform birdPoint;
    private LineRenderer leftLineRenderer;
    private LineRenderer rightLineRenderer;
    private bool isDraw;
    public TrajectoryHint trajectoryHint;
    private void Awake() {
        Instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        leftLineRenderer=transform.Find("Left_LineRender").GetComponent<LineRenderer>();
        rightLineRenderer=transform.Find("Right_LineRender").GetComponent<LineRenderer>();
        HideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDraw)
        {
            Draw();
        }
    }
    public void StartDraw(Transform birdPoint)
    {
        isDraw=true;
        this.birdPoint=birdPoint;
        ShowLine();
        if(trajectoryHint!= null)
        {
            trajectoryHint.Show();
        }
    }
    public void EndDraw()
    {
        isDraw=false;
        HideLine();
        if (trajectoryHint != null)
        {
            trajectoryHint.Hide();
        }
    }
    public void Draw()
    {
        Vector3 birdPosition=birdPoint.position;
        birdPosition=(birdPosition-centerPoint.position).normalized*0.4f+birdPosition;
        leftLineRenderer.SetPosition(0,birdPosition);
        leftLineRenderer.SetPosition(1,leftPoint.position);
        rightLineRenderer.SetPosition(0,birdPosition);
        rightLineRenderer.SetPosition(1,rightPoint.position);
        if (trajectoryHint != null)
        {
            trajectoryHint.UpdateTrajectory(birdPoint,centerPoint.position);
        }
    }
    public Vector3 GetCenterPoint()
    {
        return centerPoint.transform.position;
    }
    private void HideLine()
    {
        rightLineRenderer.enabled=false;
        leftLineRenderer.enabled=false;
    }
    private void ShowLine()
    {
        rightLineRenderer.enabled=true;
        leftLineRenderer.enabled=true;
    }
}
