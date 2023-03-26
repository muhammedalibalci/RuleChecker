namespace RuleChecker.UnitTests;

public class RuleCheckerExecuteRulesTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_WhenAllRulesArePassed()
    {
        // Arrange
        var ruleChecker = new TestModelRuleChecker(new TestModel { Value = 5 });

        // Act
        var isValid = ruleChecker.ExecuteRules().IsValid;

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenOneOrMoreRulesFail()
    {
        // Arrange
        var ruleChecker = new TestModelRuleChecker(new TestModel { Value = 25 });

        // Act
        var isValid = ruleChecker.ExecuteRules().IsValid;

        // Assert
        Assert.False(isValid);
    }
    
}
