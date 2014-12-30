namespace Socres.Web.Mvc.Tests.FilterAttributes
{
    using System.Threading;
    using System.Web.Mvc;
    using FakeItEasy;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Socres.Web.Mvc.FilterAttributes;
    using Socres.Web.Mvc.Tests.SpecimenBuilders;
    using Xunit;
    using Xunit.Extensions;

    public class CultureBasedActionAttributeTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void CultureBasedActionAttribute_OnActionExecutingWithoutControllerBase_Succeeds()
        {
            // Arrange
            var cultureBasedActionAttribute = new CultureBasedActionAttribute();
            var actionExecutingContext = new ActionExecutingContext();

            // Act
            cultureBasedActionAttribute.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.Null(actionExecutingContext.Result);
        }

        [Theory]
        [AutoFakeItEasyData(typeof(ControllerSpecimenBuilder), typeof(ActionExecutingContextSpecimenBuilder))]
        public void CultureBasedActionAttribute_OnActionExecutingWithValidCulture_Succeeds(
            ActionExecutingContext actionExecutingContext)
        {
            // Arrange
            var splitCulture = Thread.CurrentThread.CurrentUICulture.Name.Split('-');
            actionExecutingContext.Controller.ControllerContext.RouteData.Values[CultureBasedActionAttribute.LanguageUrlParameter] = splitCulture[0];
            actionExecutingContext.Controller.ControllerContext.RouteData.Values[CultureBasedActionAttribute.CultureUrlParameter] = splitCulture[1];
            var cultureBasedActionAttribute = new CultureBasedActionAttribute();

            // Act
            cultureBasedActionAttribute.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.Null(actionExecutingContext.Result);
        }

        [Theory]
        [AutoFakeItEasyData(typeof(ControllerSpecimenBuilder), typeof(ActionExecutingContextSpecimenBuilder))]
        public void CultureBasedActionAttribute_OnActionExecutingWithDefaultCulture_RedirectsToDefaultCultureAction(
            string language,
            string culture,
            ActionExecutingContext actionExecutingContext)
        {
            // Arrange
            var cultureBasedActionAttribute = new CultureBasedActionAttribute
            {
                DefaultCulture = string.Format("{0}-{1}",
                    language.Replace("-", string.Empty),
                    culture.Replace("-", string.Empty))
            };

            // Act
            cultureBasedActionAttribute.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.NotNull(actionExecutingContext.Result);
            Assert.IsType(typeof(RedirectToRouteResult), actionExecutingContext.Result);
            Assert.Equal(2, ((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.Count);
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.LanguageUrlParameter));
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.CultureUrlParameter));
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.LanguageUrlParameter], language.Replace("-", string.Empty));
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.CultureUrlParameter], culture.Replace("-", string.Empty));
        }

        [Theory]
        [AutoFakeItEasyData(typeof(ControllerSpecimenBuilder), typeof(ActionExecutingContextSpecimenBuilder))]
        public void CultureBasedActionAttribute_OnActionExecutingWithUserLanguages_RedirectsToUserLanguageCultureAction(
            string language,
            string culture,
            ActionExecutingContext actionExecutingContext)
        {
            // Arrange
            A.CallTo(() => actionExecutingContext.RequestContext.HttpContext.Request.UserLanguages)
                .Returns(new[] { string.Format("{0}-{1}", language.Replace("-", string.Empty), culture.Replace("-", string.Empty)) });
            var cultureBasedActionAttribute = new CultureBasedActionAttribute();

            // Act
            cultureBasedActionAttribute.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.NotNull(actionExecutingContext.Result);
            Assert.IsType(typeof(RedirectToRouteResult), actionExecutingContext.Result);
            Assert.Equal(2, ((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.Count);
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.LanguageUrlParameter));
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.CultureUrlParameter));
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.LanguageUrlParameter], language.Replace("-", string.Empty));
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.CultureUrlParameter], culture.Replace("-", string.Empty));
        }

        [Theory]
        [AutoFakeItEasyData(typeof(ControllerSpecimenBuilder), typeof(ActionExecutingContextSpecimenBuilder))]
        public void CultureBasedActionAttribute_OnActionExecutingWithUserLanguages_AndNotAllowedUseBrowserCulture_RedirectsToCurrentThreadCultureAction(
            string language,
            string culture,
            ActionExecutingContext actionExecutingContext)
        {
            // Arrange
            var splitCulture = Thread.CurrentThread.CurrentUICulture.Name.Split('-');
            var cultureBasedActionAttribute = new CultureBasedActionAttribute
            {
                UseBrowserCulture = false
            };

            // Act
            cultureBasedActionAttribute.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.NotNull(actionExecutingContext.Result);
            Assert.IsType(typeof(RedirectToRouteResult), actionExecutingContext.Result);
            Assert.Equal(2, ((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.Count);
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.LanguageUrlParameter));
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.CultureUrlParameter));
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.LanguageUrlParameter], splitCulture[0]);
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.CultureUrlParameter], splitCulture[1]);
        }

        [Theory]
        [AutoFakeItEasyData(typeof(ControllerSpecimenBuilder), typeof(ActionExecutingContextSpecimenBuilder))]
        public void CultureBasedActionAttribute_OnActionExecutingWithNoCulture_RedirectsToCurrentThreadCultureAction(
            ActionExecutingContext actionExecutingContext)
        {
            // Arrange
            var splitCulture = Thread.CurrentThread.CurrentUICulture.Name.Split('-');
            var cultureBasedActionAttribute = new CultureBasedActionAttribute();

            // Act
            cultureBasedActionAttribute.OnActionExecuting(actionExecutingContext);

            // Assert
            Assert.NotNull(actionExecutingContext.Result);
            Assert.IsType(typeof(RedirectToRouteResult), actionExecutingContext.Result);
            Assert.Equal(2, ((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.Count);
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.LanguageUrlParameter));
            Assert.True(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues.ContainsKey(CultureBasedActionAttribute.CultureUrlParameter));
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.LanguageUrlParameter], splitCulture[0]);
            Assert.Equal(((RedirectToRouteResult)(actionExecutingContext.Result)).RouteValues[CultureBasedActionAttribute.CultureUrlParameter], splitCulture[1]);
        }
    }
}
