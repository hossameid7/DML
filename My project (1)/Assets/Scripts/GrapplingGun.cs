using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GrapplingGun : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3 grapplePoint;
    SpringJoint joint;
    Quaternion baseRotation;
    
    public LayerMask grappleableLayer;
    public Transform gunTip, player;

    public float maxDistance = 50f;

    public float jointDamper;
    public float jointSpring;
    public float jointMassScale;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        baseRotation = player.rotation;
    }

    public void StartGrapple(Vector2 mousePosition)
    {
        RaycastHit _hit;
        Ray rayFromCamera = Camera.main.ScreenPointToRay(mousePosition);     

        if (Physics.Raycast(rayFromCamera, out _hit, maxDistance, grappleableLayer))
        {
            grapplePoint = _hit.point;
            grapplePoint.x = gunTip.position.x;
            joint = player.gameObject.AddComponent<SpringJoint>();
            Vector3 newAnchor = grapplePoint - gunTip.position;
            newAnchor.x = 0;
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(gunTip.position, grapplePoint);

            joint.damper = jointDamper;
            joint.spring = jointSpring;
            joint.massScale = jointMassScale;

            joint.minDistance = 0;
            joint.maxDistance = distanceFromPoint * 0.9f;
            joint.tolerance = 0.15f;

            lineRenderer.positionCount = 2;
        }
    }

    public void DrawGrapplingGunRope()
    {
        if (!joint) return;

        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    public void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }
}
