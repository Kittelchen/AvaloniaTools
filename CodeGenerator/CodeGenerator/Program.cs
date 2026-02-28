using CodeGenerator.Library;

Generator gen = new Generator();

if (gen.Initialize(@"C:\temp\config.json"))
{
    gen.Execute(); 
}