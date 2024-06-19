using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DragByNewInputSystem : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text PosLabel;
    Transform actualPicked;
    LocalUserAction inputs;
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
        inputs = new LocalUserAction();
        inputs.Player.LeftClick.performed += (s) => ProcessLeftClick();
        inputs.Player.LeftClick.canceled += (s) => ProcessReleaseClick();
        inputs.Player.PointerPos.performed += (s) => ProcessPointerPos(s.ReadValue<Vector2>());
        inputs.Enable();
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
            var candidate = hitResults[0].transform;
            if (!candidate.root.TryGetComponent<Canvas>(out var canvas))
            {
                actualPicked = candidate;
            }
            else 
            {
                actualPicked = null;
            }
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
    
    void OnDestroy()
    {
        inputs.Disable();
        inputs.Dispose();
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