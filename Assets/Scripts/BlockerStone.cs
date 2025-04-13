using UnityEngine;

public class BlockerCurlingStone : CurlingStone
{
    public override void TryBurn()
    {
        if (fireParticles.activeInHierarchy) 
        {
            fireParticles.SetActive(false);
        }

        burning = false;  
    }
    public override void HitByBeam()
    {
    }
    
}
