using CodeGenerator.Library;
Generator gen = new Generator();

if (gen.SetUp())
{
    gen.Execute(); 
}
else
{
    Console.WriteLine("CodeGen failed");
}
