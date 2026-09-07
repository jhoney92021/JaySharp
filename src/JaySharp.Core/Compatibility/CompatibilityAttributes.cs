using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Compatibility;

/// <summary>
/// xUnit attribute alias for <see cref="JayTest"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class FactAttribute : JayTest { }

/// <summary>
/// NUnit attribute alias for <see cref="JayTest"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class TestAttribute : JayTest { }

/// <summary>
/// MSTest attribute alias for <see cref="JayTest"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class TestMethodAttribute : JayTest { }

/// <summary>
/// NUnit attribute alias for <see cref="JayTestSuite"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class TestFixtureAttribute : JayTestSuite { }

/// <summary>
/// MSTest attribute alias for <see cref="JayTestSuite"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class TestClassAttribute : JayTestSuite { }
