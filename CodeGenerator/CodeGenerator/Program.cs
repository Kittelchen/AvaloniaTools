using CodeGenerator.Library;
Generator gen = new Generator();

gen.SetUp(AppTypes.Gui);
if (gen.SetUp(AppTypes.Cli))
{
    gen.Execute(); 
}
