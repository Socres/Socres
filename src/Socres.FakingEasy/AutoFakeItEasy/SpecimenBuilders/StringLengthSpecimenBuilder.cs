namespace Socres.FakingEasy.AutoFakeItEasy.SpecimenBuilders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Socres.FakingEasy.AutoFakeItEasy.SpecimenBuilders.Base;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// SpecimenBuilder that creates a <see cref="string"/> taking an optional <see cref="StringLengthAttribute"/> in account.
    /// </summary>
    public class StringLengthSpecimenBuilder : SpecimenBuilderBase, ISpecimenBuilder
    {
        /// <summary>
        /// Creates a new <see cref="String"/> specimen based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// The requested specimen if possible; otherwise a <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">context</exception>
        /// <remarks>
        ///   <para>
        /// The <paramref name="request" /> can be any object, but will often be a
        ///   <see cref="T:System.Type" /> or other <see cref="T:System.Reflection.MemberInfo" /> instances.
        ///   </para>
        ///   <para>
        /// Note to implementers: Implementations are expected to return a
        ///   <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance if they can't satisfy the request.
        ///   </para>
        /// </remarks>
        public object Create(object request, ISpecimenContext context)
        {
            if (!IsRequestForType(request, typeof(string)))
            {
                return new NoSpecimen();
            }

            // Check StringLength
            var stringLengths = GetCustomAttributeData<StringLengthAttribute>(request).ToList();
            if (!stringLengths.Any())
            {
                return new NoSpecimen(request);
            }

            var maxLength = (int)stringLengths.Single().ConstructorArguments.First().Value;
            var value = string.Format("{0}{1}", GetRequestName(request), Guid.NewGuid());
            if (value.Length > maxLength)
            {
                value = value.Substring(0, maxLength);
            }
            return value;
        }
    }
}
