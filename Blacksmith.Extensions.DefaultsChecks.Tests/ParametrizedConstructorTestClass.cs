namespace Blacksmith.Extensions.DefaultsChecks.Tests
{
    public class ParametrizedConstructorTestClass
    {
        public ParametrizedConstructorTestClass(int intValue)
        {
            this.SomeInt = intValue;
            this.SomeText = "lorem ipsum";
        }
        public int SomeInt { get; set; }
        public string SomeText { get; set; }
    }
}
