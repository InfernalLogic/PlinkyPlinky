using System;
using System.Collections.Generic;
using UnityEngine;

[IntegrationTest.DynamicTestAttribute("ExampleIntegrationTests")]
[IntegrationTest.ExpectExceptions(false, typeof(ArgumentException))]
[IntegrationTest.SucceedWithAssertions]
[IntegrationTest.TimeoutAttribute(1)]

public class ShouldPass : MonoBehaviour
{
  public void Start()
  {
    IntegrationTest.Pass(gameObject);
  }
}
