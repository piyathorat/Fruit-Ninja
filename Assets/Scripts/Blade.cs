using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladetrail;
    private bool slicing;
    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;
    

    public  void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladetrail = GetComponentInChildren<TrailRenderer>();
        
    }
    private void OnEnable()
    {
        StopSlicing();
    }
    private void OnDisable()
    {
        StopSlicing();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartSlicing();
            
        }
        else if (Input.GetMouseButtonUp(0))
         {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }
    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPosition;
        slicing = true;
        bladeCollider.enabled = true;
        bladetrail.enabled = true;
        bladetrail.Clear();
    }
    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladetrail.enabled = false;
       
    }
    private void ContinueSlicing()

    { 
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;
        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;
        transform.position = newPosition;
    }

}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    private bool slicing;
    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPosition;
        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;
        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;
        transform.position = newPosition;

        // Check for collisions with Fruit objects (assuming a Fruit tag)
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, LayerMask.GetMask("Fruit"));
        //Debug.Log("Number of detected colliders: " + hits.Length);


        foreach (Collider hitCollider in hits)
        {
            // Access collider properties (e.g., hitCollider.gameObject)
            // Check if the collider has a Rigidbody component (optional)
            Debug.Log("Sliced fruit: " + hitCollider.gameObject.name);

            if (hitCollider.gameObject.GetComponent<Rigidbody>() != null)
            {
                // Apply force to the sliced fruit (assuming a Rigidbody)
                hitCollider.gameObject.GetComponent<Rigidbody>().AddForce(direction * sliceForce, ForceMode.Impulse);
                Debug.Log("Sliced fruit: " + hitCollider.gameObject.name);
                // (Optional) Play sound effect or trigger particle effects on hit
            }

            // Destroy the sliced fruit object (optional)
            Destroy(hitCollider.gameObject);
        }

    }
}
*/
