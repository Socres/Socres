namespace Socres.FakingEasy.AutoFakeItEasy
{
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;
    using Xunit.Sdk;

    /// <summary>
    /// Method for having inline method parameters.
    /// Remainder parameters that are not supplied will be automatically faked.
    /// </summary>
    public sealed class InlineAutoFakeItEasyDataAttribute : CompositeDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineAutoFakeItEasyDataAttribute"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public InlineAutoFakeItEasyDataAttribute(params object[] values)
            : base(new DataAttribute[] { 
            new InlineDataAttribute(values), new AutoFakeItEasyDataAttribute() })
        {
        }
    }
}
