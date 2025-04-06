using UnityEngine;

public class BlockerCurlingStone : CurlingStone
{
    // Override to disable burning behavior completely
    public override void TryBurn()
    {
        // No burning effect for the blocker stone.
        if (fireParticles.activeInHierarchy) 
        {
            fireParticles.SetActive(false);  // Ensure fire particles are never activated.
        }

        burning = false;  
    }
    public override void HitByBeam()
    {
    }
    
}
