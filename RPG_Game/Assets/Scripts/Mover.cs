using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Mover : MonoBehaviour
{
    private Camera mainCamera = null;
    [SerializeField] private NavMeshAgent player;
    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GetWorldPosition(Input.mousePosition);
        }
    }

    private void GetWorldPosition(Vector3 position)
    {
        Ray ray = mainCamera.ScreenPointToRay(position);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }

        MoveToCursor(hit.point);
    }

    private void MoveToCursor(Vector3 position)
    {
        if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        player.SetDestination(hit.position);
    }
}
