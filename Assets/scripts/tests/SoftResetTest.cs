using System;
using System.Collections.Generic;
using UnityEngine;

[IntegrationTest.DynamicTestAttribute("SoftResetTests")]
[IntegrationTest.ExpectExceptions(false, typeof(ArgumentException))]
[IntegrationTest.SucceedWithAssertions]
[IntegrationTest.TimeoutAttribute(1)]

public class SoftResetTest : MonoBehaviour 
{
  public void Start()
  {

  }
}
