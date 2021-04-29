using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RebuildStage
{
   [SerializeField] public int BuildStage;
   [SerializeField] public IMaterialSiteType Requirement;
    //[SerializeField] public GameObject go_RebuildPart;
    [SerializeField] public List<GameObject> go_RebuildPart;
}

