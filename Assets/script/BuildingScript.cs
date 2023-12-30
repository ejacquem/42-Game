using System;
using System.Collections.Generic;
using NavMeshPlus.Components;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Build
{
    public GameObject prefab;
    public int cost;
}

public class BuildingScript : MonoBehaviour
{
    Vector2 gridPos;
    public int coins = 100;
    public Transform player;
    public bool buildingMode = true;
    bool canPlace = true;
    public GameObject placementCursor;
    bool collide = false;
    [SerializeField]
    public Build[] builds;
    private int buildIndex = 0;
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectBuild();
            setGridPos(mousePos);
            placeCursor();
            checkIfCanPlace();
            if(Input.GetMouseButton(0) && canPlace && coins > builds[buildIndex].cost)
            {
                map.Add(gridPos, true);
                Instantiate(builds[buildIndex].prefab, gridPos, Quaternion.Euler(0, 0, 0));
                coins -= builds[buildIndex].cost;
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

    void SelectBuild()
    {
        buildIndex += (int)(Input.GetAxis("Mouse ScrollWheel")*10);
        if(buildIndex < 0)
            buildIndex = builds.Length - 1;
        if(buildIndex >= builds.Length)
            buildIndex = 0;
    }

    void placeCursor()
    {
        placementCursor.transform.rotation = Quaternion.Euler(0, 0, 0);
        placementCursor.transform.position = gridPos;
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
