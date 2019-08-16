using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinMaps : MonoBehaviour
{
   //public List<GameObject> mapParts;

   public GameObject start;
   public GameObject part1;
   public GameObject part2;

   private void Start()
   {
      CreateMap();
   }

   private void CreateMap()
   {
      Vector3 startJoin = start.transform.Find("JoinPoint").position;

      GameObject part = Instantiate(part1, part1.transform.position, part1.transform.rotation);
      GameObject partToJoin = part.transform.Find("StartJoin").gameObject;
      partToJoin.transform.position = startJoin;
      
      startJoin = partToJoin.transform.Find("FinishJoin").position;
      
      part = Instantiate(part2, part2.transform.position, part2.transform.rotation);
      partToJoin = part.transform.Find("StartJoin").gameObject;
      partToJoin.transform.position = startJoin;
      
   }
}
