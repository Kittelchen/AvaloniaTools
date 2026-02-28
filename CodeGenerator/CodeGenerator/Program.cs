using CodeGenerator.Library;
Generator gen = new Generator();

if (gen.SetUp(AppTypes.Cli))
{
    gen.Execute(); 
}