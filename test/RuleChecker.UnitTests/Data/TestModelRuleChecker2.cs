namespace RuleChecker.UnitTests.Data;

public class TestModelRuleChecker2 : BaseRuleChecker
{
    public TestModelRuleChecker2(TestModel model)
    {
        Models = new List<object> { model };
        AddRules();
    }
        
    private void AddRules()
    {
        AddRule<TestModel>(m => m.Value % 2 == 0, "Value must be even");
    }
}