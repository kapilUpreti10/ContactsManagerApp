using FirstTest.Services;

namespace FirstTest
{
    public class UnitTest1
    {
        // to indicate it is test case it should be marked with [Fact] attribute
        [Fact]
        public void Test1()
        {

            // for unit testing we have 3 steps

            //1. Arrange
            // here we write the test value,expected value and create the object of service which we want to test

            int testVal1 = 10;
            int testVal2 = 20;
            int expectedVal = 30;

            Sample obj = new Sample();


            //2. Act 
            // here we call the method of service which we want to test

           int result= obj.Add(testVal1, testVal2);


            //3. Assert
            // here we compare teh expected value with actual value

            Assert.Equal(expected: expectedVal, actual: result);



        }
    }
}