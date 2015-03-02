using System;
using NUnit.Framework;

namespace UnitTests
{
  internal class SampleTest
  {
    [Test]
    [Category("ShouldFail")]
    public void ThisShouldFail()
    {
      throw new Exception("This should fail and it did");
    }

    [Test]
    [Category("ShouldPass")]
    public void ThisShouldPass()
    {
      Assert.Pass();
    }

  }
}
