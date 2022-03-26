using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianTeethShootingWakid : MonoBehaviour
{
    [Header("Animator")]
    public Animator anim;
    public string animatorParam = "duration";
    [Header("Camera")]
    public Camera cam;
    public float defaultFOV, aimingFOV;
    [Header("General")]
    public float aimDuration = 0.2f;
    [Header("Scope")]
    public bool enableScope;
    public MeshRenderer wakidRenderer;
    public GameObject scopeOverlay;

    private IEnumerator Aim (bool isAiming)
    {
        float blendValue = 0, timeElapsed = 0;
        // Show weapon model and Hide Scope UI
        if (enableScope)
        {
            wakidRenderer.enabled = true;
            scopeOverlay.SetActive(false);
        }

        while(timeElapsed < aimDuration)
        {
            // Calculate transition's progress
            blendValue = timeElapsed / aimDuration;
            // Blend between animations and calculate the camera FOV
            if (isAiming)
            {
                anim.SetFloat(animatorParam, blendValue);
                cam.fieldOfView = Mathf.Lerp(aimingFOV, defaultFOV, 1 - blendValue);
            }
            else
            {
                anim.SetFloat(animatorParam, 1 - blendValue);
                cam.fieldOfView = Mathf.Lerp(aimingFOV, defaultFOV, blendValue);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // If scope is enabled, hide weapon model and show the scope UI
        if (enableScope)
        {
            wakidRenderer.enabled = !isAiming;
            scopeOverlay.SetActive(isAiming);
        }
        // Confirm and finalize changes
        if (isAiming)
        {
            anim.SetFloat(animatorParam, 1);
            cam.fieldOfView = aimingFOV;
        }
        else
        {
            anim.SetFloat(animatorParam, 0);
            cam.fieldOfView = defaultFOV;
        }
        
    }

    

    public void OnAim(bool state)
    {
        StopAllCoroutines();
        StartCoroutine(Aim(state));
    }

    // Start is called before the first frame update
    void Start()
    {
        // prevent any errors
        if (!wakidRenderer || !scopeOverlay)
            enableScope = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
