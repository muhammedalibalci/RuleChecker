namespace RuleChecker.UnitTests.Data;

public class TestModelRuleChecker : BaseRuleChecker
{
    public TestModelRuleChecker(TestModel model)
    {
        Models = new List<object> { model };
        AddRules();
    }
        
    private void AddRules()
    {
        AddRule<TestModel>(m => m.Value >= 1, "Value must be greater than or equal to 1"); 
        AddRule<TestModel>(m => m.Value <= 10, "Value must be less than or equal to 10");
    }
        
}