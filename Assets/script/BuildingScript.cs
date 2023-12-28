using System;
using System.Collections.Generic;
using NavMeshPlus.Components;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    Vector2 mousePos;
    Vector2 gridPos;
    public Transform player;
    bool buildingMode = true;
    bool canPlace = true;
    public GameObject placementCursor;
    bool collide = false;
    public GameObject wall;
    public SpriteRenderer rendererX;
    private Dictionary<Vector2, bool> map = new Dictionary<Vector2, bool>();
    public NavMeshSurface buildableNavMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(buildingMode)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            setGridPos(mousePos);
            placementCursor.transform.rotation = Quaternion.Euler(0, 0, 0);
            placementCursor.transform.position = gridPos;
            checkIfCanPlace();
            if(Input.GetMouseButton(0) && canPlace)
            {
                map.Add(gridPos, true);
                Instantiate(wall, gridPos, Quaternion.Euler(0, 0, 0));
                buildableNavMeshSurface.BuildNavMesh();
            }
        }
    }

    private void checkIfCanPlace()
    {
        collide = placementCursor.GetComponent<PlacementCursorScript>().getCollide();
        if(collide || (map.TryGetValue(gridPos, out bool isBuildThere) && isBuildThere))
            setCanPlace(false);
        else setCanPlace(true);
    }

    private void setCanPlace(bool old)
    {
        if(old != canPlace)
        {
            canPlace = old;

            Color color = Color.red;
            if(canPlace)
                color = Color.cyan;
            color.a = 0.5f;
            rendererX.color = color;
        }
    }

    void setGridPos(Vector2 vect)
    {
        Vector2 newPos = new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
        if(gridPos != newPos)
        {
            gridPos = newPos;
        }
    }

    // private Vector2 getGridPos(Vector2 vect)
    // {
    //     Vector2 aimDirection = mousePos - origin;
    //     Vector2 newPos = new Vector2(Mathf.Round(origin.x), Mathf.Round(origin.y));
    //     float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    //     if(aimAngle > 22.5 && aimAngle < 157.5)
    //         newPos.y += 1;
    //     else if(aimAngle < -22.5 && aimAngle > -157.5)
    //         newPos.y -= 1;
    //     if(Mathf.Abs(aimAngle) > 112.5)
    //         newPos.x -= 1;
    //     else if(Mathf.Abs(aimAngle) < 67.5)
    //         newPos.x += 1;
    //     Debug.Log( "angle = " + aimAngle + " pos : " +newPos);
    //     return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    // }


}
