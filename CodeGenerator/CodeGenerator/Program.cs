using CodeGenerator.Library;
Generator gen = new Generator();

if (gen.SetUp(AppTypes.CLI))
{
    gen.Execute(); 
}