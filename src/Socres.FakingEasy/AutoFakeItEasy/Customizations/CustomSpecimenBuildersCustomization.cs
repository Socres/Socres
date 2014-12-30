namespace Socres.FakingEasy.AutoFakeItEasy.Customizations
{
    using System;
    using System.Linq;
    using Socres.FakingEasy.AutoFakeItEasy.SpecimenBuilders;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Adds custom SpecimenBuilders
    /// </summary>
    public class CustomSpecimenBuildersCustomization : ICustomization
    {
        private readonly Type[] _specimenBuilders;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSpecimenBuildersCustomization"/> class.
        /// </summary>
        /// <param name="specimenBuilders">The custom specimen builders.</param>
        public CustomSpecimenBuildersCustomization(params Type[] specimenBuilders)
        {
            _specimenBuilders = specimenBuilders;
        }

        /// <summary>
        /// Adds the custom customization.
        /// These include :
        /// <see cref="StringLengthSpecimenBuilder"/>
        /// <see cref="StreamSpecimenBuilder"/>
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            foreach (var specimenBuilder in 
                _specimenBuilders
                    .Where(specimenBuilder => 
                        specimenBuilder.GetInterfaces().Any(i => i == typeof (ISpecimenBuilder))))
            {
                fixture.Customizations.Add(Activator.CreateInstance(specimenBuilder) as ISpecimenBuilder);
            }
        }
    }
}
