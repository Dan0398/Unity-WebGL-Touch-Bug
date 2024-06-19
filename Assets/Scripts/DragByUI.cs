using UnityEngine;
using UnityEngine.EventSystems;

public class DragByUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] Camera cam;
    [SerializeField] UnityEngine.UI.Text PosLabel;
    [SerializeField] Transform actualPicked;
    Vector2 pointerScreenPos;
    Vector3 pointerWorldPos;
    Plane GamePlane;
    float PlaneIntersectionDistance;

    void Start()
    {
        GamePlane = new Plane(Vector3.back, Vector3.zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var Hit =  Physics2D.Raycast(pointerWorldPos, Vector2.zero);
        if (Hit.transform == null)
        {
            actualPicked = null;
        }
        else
        {
            if (!Hit.transform.root.TryGetComponent<Canvas>(out var canvas))
            {
                actualPicked = Hit.transform;
            }
            else 
            {
                actualPicked = null;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        actualPicked = null;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        pointerScreenPos = eventData.position;
        PosLabel.text = pointerScreenPos.ToString();
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