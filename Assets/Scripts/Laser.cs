using UnityEngine;

[RequireComponent (typeof(LineRenderer))]

public class Laser : MonoBehaviour
{
    public Transform laserOrigin;
    public Transform emitterPos;

    public LineRenderer beam;
    public float defaultDistance = 25f;

    public Vector2 beamDifference;

    void Update()
    {
        FireBeam();
    }

    void FireBeam()
    {
        if (Physics2D.Raycast(emitterPos.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, transform.right);
            DrawBeam(laserOrigin.position, hit.point);
            if(hit.transform.gameObject.layer == 3)
            {
                hit.transform.SendMessage("HitByBeam");
                Debug.Log("HIT");
            }
            beamDifference = hit.transform.position - laserOrigin.position;
        }
        else
        {
            DrawBeam(laserOrigin.position, laserOrigin.right * defaultDistance);
        }
    }

    void DrawBeam(Vector2 startPos, Vector2 endPos)
    {
        beam.SetPosition(0, startPos);
        beam.SetPosition (1, endPos);
    }
}
