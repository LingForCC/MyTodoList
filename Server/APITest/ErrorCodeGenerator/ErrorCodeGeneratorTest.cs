using System;
using API.ErrorCode;
using Core.Services;
using Moq;
using Xunit;

namespace APITest.ErrorCodeGenerator
{
    public class ErrorCodeGeneratorTest
    {
        [Fact]
        public void TestGetErrorCodeWithCorrectExceptionType()
        {
            var errorCodeGenerator = new TestServiceExceptionErrorCodeGenerator();
            var testServiceException = new TestServiceException1();
            var errorCode = errorCodeGenerator.GetErrorCode(testServiceException);
            Assert.Equal("TEC-100", errorCode);
        }

        [Fact]
        public void TestGetErrorCodeWithInCorrectExceptionType()
        {
            var errorCodeGenerator = new TestServiceExceptionErrorCodeGenerator();
            var testServiceException = new TestServiceException2();
            Assert.Throws<ErrorCodeGeneratorException>(
                () => errorCodeGenerator.GetErrorCode(testServiceException));
        }

        [Fact]
        public void TestGetErrorCodeWithNull()
        {
            var errorCodeGenerator = new TestServiceExceptionErrorCodeGenerator();
            Assert.Throws<ErrorCodeGeneratorException>(
                () => errorCodeGenerator.GetErrorCode(null));
        }
    }

    public class ErrorCodeGeneratorManagerTest
    {
        [Fact]
        public void TestRegisterWithDuplicatedGenerator()
        {
            var errorCodeGeneratorManager = new ErrorCodeGeneratorManager();
            var errorCodeGenerator1 = new TestServiceExceptionErrorCodeGenerator();
            var errorCodeGenerator2 = new TestServiceExceptionErrorCodeGenerator();


            errorCodeGeneratorManager.RegisterErrorCodeGenerator(errorCodeGenerator1);

            Assert.Throws<ErrorCodeGeneratorException>(
                () => errorCodeGeneratorManager.RegisterErrorCodeGenerator(errorCodeGenerator1));

            Assert.Throws<ErrorCodeGeneratorException>(
                () => errorCodeGeneratorManager.RegisterErrorCodeGenerator(errorCodeGenerator2));
        }

        [Fact]
        public void TestRegisterWithNull()
        {
            var errorCodeGeneratorManager = new ErrorCodeGeneratorManager();
            Assert.Throws<ErrorCodeGeneratorException>(
                () => errorCodeGeneratorManager.RegisterErrorCodeGenerator(null));
        }

        [Fact]
        public void TestGetErrorCodeWithDedicatedGenerator()
        {
            var errorCodeGeneratorManager = new ErrorCodeGeneratorManager();
            var errorCodeGenerator = new TestServiceExceptionErrorCodeGenerator();
            errorCodeGeneratorManager.RegisterErrorCodeGenerator(errorCodeGenerator);
            var testServiceException = new TestServiceException1();
            string errorCode = errorCodeGeneratorManager.GetErrorCode(testServiceException);

            Assert.Equal("TEC-100", errorCode);
        }

        [Fact]
        public void TestGetErrorCodeWithoutDedicatedGenerator()
        {
            var errorCodeGeneratorManager = new ErrorCodeGeneratorManager();
            var errorCodeGenerator = new TestServiceExceptionErrorCodeGenerator();
            errorCodeGeneratorManager.RegisterErrorCodeGenerator(errorCodeGenerator);
            var testServiceException = new TestServiceException2();
            string errorCode = errorCodeGeneratorManager.GetErrorCode(testServiceException);

            Assert.Equal("GEC-100", errorCode);
        }

        [Fact]
        public void TestGetErrorCodeWithUnexpectedExceptionInGenerator()
        {
            //Given
            var testServiceException = new TestServiceException1();

            Mock<IErrorCodeGenerator> mockGenerator
                = new Mock<IErrorCodeGenerator>();
            mockGenerator.Setup(m => m.GetErrorCode(testServiceException)).Throws<Exception>();
            mockGenerator.Setup(m => m.ExceptionTypeFullName).Returns(typeof(TestServiceException1).FullName);
            IErrorCodeGenerator mockObject = mockGenerator.Object;

            var errorCodeGeneratorManager = new ErrorCodeGeneratorManager();
            errorCodeGeneratorManager.RegisterErrorCodeGenerator(mockObject);

            //When
            string errorCode = errorCodeGeneratorManager.GetErrorCode(testServiceException);

            //Then
            Assert.Equal("GEC-100", errorCode);
        }
    }


    #region Mock Class for Testing

    public class TestServiceException1 : ServiceException
    {
        public TestServiceException1() 
            : this("TestService", null)
        {

        }

        public TestServiceException1(string serviceName, string message)
            : base(serviceName, message)
        {
        }

        public TestServiceException1(string serviceName, string message, Exception innerException)
            : base(serviceName, message, innerException)
        {
        }
    }


    public class TestServiceException2 : ServiceException
    {
        public TestServiceException2()
            : this("TestService", null)
        {

        }

        public TestServiceException2(string serviceName, string message)
            : base(serviceName, message)
        {
        }

        public TestServiceException2(string serviceName, string message, Exception innerException)
            : base(serviceName, message, innerException)
        {
        }
    }


    public class TestServiceExceptionErrorCodeGenerator : AbsErrorCodeGenerator<TestServiceException1>
    {

        public override string GetErrorCodeInternal(TestServiceException1 exception)
        {
            return "TEC-100";
        }
    }

    #endregion
}
