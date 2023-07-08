using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3 grapplePoint;
    ConfigurableJoint joint;
    
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
    }

    public void StartGrapple(Vector2 mousePosition)
    {
        RaycastHit _hit;
        Ray rayFromCamera = Camera.main.ScreenPointToRay(mousePosition);     

        if (Physics.Raycast(rayFromCamera, out _hit, maxDistance, grappleableLayer))
        {
            Debug.Log("hit grapple");
            grapplePoint = _hit.point;
            grapplePoint.x = player.position.x;
            joint = player.gameObject.AddComponent<ConfigurableJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //joint.damper = jointDamper;
            //joint.spring = jointSpring;
            //joint.massScale = jointMassScale;

            //joint.minDistance = distanceFromPoint * 0.8f;
            //joint.maxDistance = distanceFromPoint;
            //joint.tolerance = 0.25f;

            joint.axis.Set(0, 0, 1);
            joint.secondaryAxis.Set(0, 1, 0);

            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;

            joint.angularXMotion = ConfigurableJointMotion.Locked;

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
