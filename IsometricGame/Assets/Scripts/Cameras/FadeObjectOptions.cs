using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/*Allows object fades to be overidden per object*/
public class FadeObjectOptions : MonoBehaviour
{
    public bool OverrideFadeOutSeconds = false;
    public bool OverrideFadeInSeconds = false;
    public float FadeOutSeconds = 0;
    public float FadeInSeconds = 0;
    public bool OverrideFinalAlpha = false;
    public float FinalAlpha = 0;
}