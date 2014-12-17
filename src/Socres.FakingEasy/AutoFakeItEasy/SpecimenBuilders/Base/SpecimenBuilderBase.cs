namespace Socres.FakingEasy.AutoFakeItEasy.SpecimenBuilders.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Base class for all <see cref="ISpecimenBuilder"/>
    /// </summary>
    public abstract class SpecimenBuilderBase
    {
        /// <summary>
        /// Gets the name of the request using the <see cref="PropertyInfo"/> or <see cref="ParameterInfo"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        protected string GetRequestName(object request)
        {
            var paramInfo = request as ParameterInfo;
            if (paramInfo != null)
            {
                return paramInfo.Name;
            }

            var propInfo = request as PropertyInfo;
            if (propInfo != null)
            {
                return propInfo.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Determines whether the request is for the given type.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected bool IsRequestForType(object request, Type type)
        {
            var paramInfo = request as ParameterInfo;
            var propInfo = request as PropertyInfo;

            return ((paramInfo != null && paramInfo.ParameterType == type)
                    || (propInfo != null && propInfo.PropertyType == type));
        }

        /// <summary>
        /// Gets the custom attribute data.
        /// </summary>
        /// <typeparam name="T">The Attribute to check for.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        protected IEnumerable<CustomAttributeData> GetCustomAttributeData<T>(object request) where T : Attribute
        {
            var paramInfo = request as ParameterInfo;
            if (paramInfo != null)
            {
                return paramInfo.GetCustomAttributesData().Where(c => c.AttributeType == typeof(T));
            }

            var propInfo = request as PropertyInfo;
            if (propInfo != null)
            {
                return propInfo.GetCustomAttributesData().Where(c => c.AttributeType == typeof(T));
            }

            return new List<CustomAttributeData>();
        }
    }
}
