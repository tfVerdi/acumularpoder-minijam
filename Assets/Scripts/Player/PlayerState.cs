using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public const float vfxDurationSeconds = 0.75f;
    public int energyStored = 0;
    public GameObject vfxHolder;

    private const int maxEnergyStored = 3;
    public int getMaxEnergyStored() {
        return maxEnergyStored;
    }
    
    private IEnumerator playEnergyPickupVFX(GameObject effectHolder, float seconds = vfxDurationSeconds) {
        SpriteRenderer renderer = effectHolder.GetComponent<SpriteRenderer>();
        renderer.enabled = true;
        yield return new WaitForSeconds(seconds);
        renderer.enabled = false;
    }

    private IEnumerator playSpeedBoostVFX(GameObject effectHolder, float seconds) {
        Debug.Log("SpeedBoost vfx placeholder text! FIIIUUUUM");
        yield return new WaitForSeconds(seconds);
    }

    public enum VFXEnum {
        EnergyPickup = 1,
        SpeedBoost = 2,
    }

    public void playVFX(VFXEnum vfxEnum, float seconds) {
        switch (vfxEnum) {
            case VFXEnum.EnergyPickup:
                StartCoroutine(playEnergyPickupVFX(vfxHolder, seconds));
                return;
            case VFXEnum.SpeedBoost:
                StartCoroutine(playSpeedBoostVFX(vfxHolder, seconds));
                return;
        }
    }

    public void playVFX(VFXEnum vfxEnum) {
        switch (vfxEnum) {
            case VFXEnum.EnergyPickup:
                StartCoroutine(playEnergyPickupVFX(vfxHolder, vfxDurationSeconds));
                return;
            case VFXEnum.SpeedBoost:
                StartCoroutine(playSpeedBoostVFX(vfxHolder, vfxDurationSeconds));
                return;
        }
    }
}