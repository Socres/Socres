#FakingEasy

This project contains building blocks to be used with FakeItEasy and xUnit.

###AutoFakeItEasyData
When using this attribute on a test method, all parameters of the method will be instantiated automatically by FakeItEasy.
In the example below, sampleService will be an instance of a class implementing ISampleService.
Both value and returnValue will be random generated strings.

```csharp
[Theory]
[AutoFakeItEasyData]
public void SampleProvider_ExecuteWithParam_Succeeds(
    ISampleService sampleService,
    string value,
    string returnValue)
{
    A.CallTo(() => sampleService.CanExecute)
        .Returns(true);
    A.CallTo(() => sampleService.ExecuteWithParam(value))
        .Returns(returnValue);

    var sampleProvider = new SampleProvider(sampleService);
    var actual = sampleProvider.ExecuteSampleWithParam(value);

    A.CallTo(() => sampleService.ExecuteWithParam(A<string>.Ignored))
        .MustHaveHappened(Repeated.Exactly.Once);
    Assert.Equal(returnValue, actual);
}
```
  

###InlineAutoFakeItEasyData
When using this attribute on a test method, you can supply fixed values for parameters. All remaining parameters will be automatically instantiated by FakeItEasy.
In the example below, this test will be executed twice, one time with canExecute true and returnValue "ReturnValueFirst" and a second time with canExecute false and returnValue "".
The sampleService and value will be automatically instantiated with an instance of a class implementing ISampleService and a random generated string.

```csharp
[Theory]
[InlineAutoFakeItEasyData(true, "ReturnValueFirst")]
[InlineAutoFakeItEasyData(false, "")]
public void SampleProvider_ExecuteWithParams_CanExecute_Succeeds(
    bool canExecute,
    string returnValue,
    ISampleService sampleService,
    string value)
{
    A.CallTo(() => sampleService.CanExecute)
        .Returns(canExecute);
    A.CallTo(() => sampleService.ExecuteWithParam(value))
        .Returns(returnValue);

    var sampleProvider = new SampleProvider(sampleService);
    var actual = sampleProvider.ExecuteSampleWithParam(value);

    A.CallTo(() => sampleService.ExecuteWithParam(A<string>.Ignored))
        .MustHaveHappened(
            canExecute
            ? Repeated.Exactly.Once
            : Repeated.Never);
    Assert.Equal(returnValue, actual);
}
```
