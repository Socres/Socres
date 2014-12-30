namespace Socres.FakingEasy.AutoFakeItEasy.Customizations
{
    using Socres.FakingEasy.AutoFakeItEasy.SpecimenBuilders;
    using Ploeh.AutoFixture;

    /// <summary>
    /// Adds the default customizations
    /// </summary>
    public class DefaultCustomization : ICustomization
    {
        /// <summary>
        /// Adds the default customization.
        /// These include :
        /// <see cref="StringLengthSpecimenBuilder"/>
        /// <see cref="StreamSpecimenBuilder"/>
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new StringLengthSpecimenBuilder());
            fixture.Customizations.Add(new StreamSpecimenBuilder());
        }
    }
}
