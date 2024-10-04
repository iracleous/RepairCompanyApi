using RepairCompanyApi.Services.Implementations;

namespace RepairTest;

public class UnitTest1
{

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 3, 5)]
    public void Test1(int parameter1, int parameter2, int expectedResult)
    {
        var adder = new Adder();
        var calculatedResult = adder.AddOperation(parameter1, parameter2);
        Assert.Equal(expectedResult, calculatedResult);
    }
 

}