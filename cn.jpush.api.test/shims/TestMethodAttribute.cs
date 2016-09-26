using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cn.jpush.api.schedule;
using Xunit;

namespace Microsoft.VisualStudio.TestTools.UnitTesting {
    public class TestMethodAttribute : FactAttribute {
    }

    public class TestClassAttribute : Attribute {

    }

    public class ExpectedExceptionAttribute : Attribute {
        public ExpectedExceptionAttribute(Type type) {

        }
    }

    public static class Assert {
        public static void IsInstanceOfType(object obj, Type type) {
            Xunit.Assert.IsType(type, obj);
        }

        public static void IsTrue(bool condition, string userMessage) {
            Xunit.Assert.True(condition, userMessage);
        }
        public static void IsTrue(bool condition) {
            Xunit.Assert.True(condition);
        }

        public static void AreEqual<T>(T expected, T actual) {
            Xunit.Assert.Equal(expected, actual);
        }
    }

    public class TestCleanupAttribute : Attribute {

    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class TestInitializeAttribute : Attribute {

    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class ClassCleanupAttribute : Attribute {

    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class ClassInitializeAttribute : Attribute {

    }

    public class TestContext {

    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class PriorityAttribute : Attribute {
        private int v;

        public PriorityAttribute(int v) {
            this.v = v;
        }
    }
}
