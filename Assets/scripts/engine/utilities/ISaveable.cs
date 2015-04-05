﻿using UnityEngine;
using System.Collections;

public interface ISaveable
{
  void Save();
  void Load();
  void OnSerializationEvent();
}