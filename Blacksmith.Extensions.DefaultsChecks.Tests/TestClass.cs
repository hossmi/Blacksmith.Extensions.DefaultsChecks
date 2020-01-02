namespace Blacksmith.Extensions.DefaultsChecks.Tests
{
    public class TestClass
    {
        public TestClass()
        {
            this.SomeInt = 55;
            this.SomeText = "default";
        }
        public int SomeInt { get; set; }
        public string SomeText { get; set; }
    }
}
