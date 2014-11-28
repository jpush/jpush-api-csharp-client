using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.jpush.api.push.audience;

namespace cn.jpush.api.test.audience
{
    [TestClass]
    public class AudienceTargetTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testIllegal()
        {
            AudienceTarget audienceTarget = AudienceTarget.alias(null);
        }
    }
}
