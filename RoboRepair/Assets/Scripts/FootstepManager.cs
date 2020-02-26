using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{

    public AudioSource footstep;

    public void OnFootstepEvent()
    {
        footstep.Play();
    }
}
