using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class BuildingManager : MonoBehaviour
{

    public GameObject[] buildings;
    private GameObject pendingObject;

    private Vector3 posBuilding;
    
    private RaycastHit rcHit;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Toggle gridToggle;

    //private float maxDistanceRay = 10000;
    public float gridSize;
    public float rotateAmount;

    bool gridOn = true;



    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray ray = Camera.main.ScreenPointToRay(mouse.current.position.ReadDefaultValue());
        if (Physics.Raycast(ray,out rcHit, 10000,layerMask))
        {
            posBuilding = rcHit.point;
        }
    }
    void Update()
    {
        if(pendingObject!= null)
        {

            if(gridOn)
            {
                pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(posBuilding.x), RoundToNearestGrid(posBuilding.y),RoundToNearestGrid(posBuilding.z));
                 

            }
            else 
            { 
                pendingObject.transform.position = posBuilding; 
            }
            

            if(Input.GetMouseButton(0))
            {
                PlaceObject();
            }

            if(Input.GetKeyDown(KeyCode.R)) 
            {
                RotateObject();
            }
        }
    }

    void PlaceObject()
    {
        pendingObject = null;
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(buildings[index], posBuilding, transform.rotation);
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    public void ToggleGrid()
    {
        if(gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;

        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }
}
