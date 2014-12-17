namespace Socres.FakingEasy.AutoFakeItEasy.SpecimenBuilders
{
    using System.IO;
    using Socres.FakingEasy.AutoFakeItEasy.SpecimenBuilders.Base;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// SpecimenBuilder that ignores the virtual members
    /// </summary>
    public class StreamSpecimenBuilder : SpecimenBuilderBase, ISpecimenBuilder
    {
        /// <summary>
        /// Creates a new <see cref="Stream"/> specimen based on a request.
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
            if (!IsRequestForType(request, typeof(Stream)))
            {
                return new NoSpecimen();
            }

            var data = (byte[])context.Resolve(typeof(byte[]));
            var stream = new MemoryStream(data);
            return stream;
        }
    }
}
