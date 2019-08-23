﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectDisconnectManager : MonoBehaviour
{
   public static Action ConnectCarControllerDelegate;
   public static Action ConnectCharacterSelectionControllerDelegate;
   public static Action ConnectCarSoundManager;
   public static Action ConnectSkillManagerDelegate;
   public static Action ConnectItemManagerDelegate;
   public static Action ActivateCarController;

   public static Action DisconnectCarControllerDelegate;
   public static Action DisconnectCharacterSelectionControllerDelegate;
   public static Action DisconnectCarSoundManagerDelegate;
   public static Action DisconnectSkillManagerDelegate;
   public static Action DisconnectItemManagerDelegate;

   public static Action InitCamera;
}
