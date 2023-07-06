using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3 grapplePoint;
    SpringJoint joint;
    
    public LayerMask grappleableLayer;
    public Transform gunTip, player;

    public float maxDistance = 50f;

    public float jointDamper;
    public float jointSpring;
    public float jointMassScale;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void StartGrapple(Vector2 mousePosition)
    {
        RaycastHit _hit;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));
        Vector3 direction = mouseWorldPosition - gunTip.position;
        if (Physics.Raycast(gunTip.position, direction, out _hit, maxDistance, grappleableLayer))
        {
            grapplePoint = _hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            Debug.Log($"Start distance: {distanceFromPoint}");

            joint.damper = jointDamper;
            joint.spring = jointSpring;
            joint.massScale = jointMassScale;

            joint.minDistance = distanceFromPoint * 0.8f;
            joint.maxDistance = distanceFromPoint;
            joint.tolerance = 0.25f;

            lineRenderer.positionCount = 2;
        }
    }

    public void DrawGrapplingGunRope()
    {
        if (!joint) return;
        Debug.Log(Vector3.Distance(player.position, grapplePoint));
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    public void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }
}
