using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DragByLegacyInputs : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text PosLabel;
    Transform actualPicked;
    Vector2 pointerScreenPos;
    Vector3 pointerWorldPos;
    Camera cam;
    RaycastHit2D[] hitResults;        
    Plane GamePlane;
    float PlaneIntersectionDistance;
    
    void Start()
    {
        GamePlane = new Plane(Vector3.back, Vector3.zero);
        hitResults = new RaycastHit2D[20];
        cam = GetComponent<Camera>();
        Input.simulateMouseWithTouches = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ProcessLeftClick();
        else if (Input.GetMouseButtonUp(0)) ProcessReleaseClick();
        ProcessPointerPos(Input.mousePosition);
    }

    void ProcessLeftClick()
    {
        var targetsCount = Physics2D.RaycastNonAlloc(pointerWorldPos, Vector2.zero, hitResults);
        if (targetsCount == 0)
        {
            actualPicked = null;
        }
        else
        {
            actualPicked = hitResults[0].transform;
        }
    }
    
    void ProcessReleaseClick()
    {
        actualPicked = null;
    }
    
    void ProcessPointerPos(Vector2 pos)
    {
        pointerScreenPos = pos;
        PosLabel.text = pos.ToString();
        pointerWorldPos = GetPointerWorldPos();
        if (actualPicked != null)
        {
            actualPicked.position = pointerWorldPos;
        }
        
        Vector3 GetPointerWorldPos()
        {
            if (cam.orthographic) return cam.ScreenToWorldPoint(pointerScreenPos);
            var PointerRay = cam.ScreenPointToRay(pointerScreenPos);
            GamePlane.Raycast(PointerRay, out PlaneIntersectionDistance);
            return PointerRay.origin + PointerRay.direction * PlaneIntersectionDistance;
        }
    }
    
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (cam == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointerWorldPos, 0.1f);
    }
    #endif
}