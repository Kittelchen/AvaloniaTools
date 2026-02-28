using CodeGenerator.Library;

Generator gen = new Generator();

if (gen.Initialize(AppTypes.CLI))
{
    gen.Execute(); 
}