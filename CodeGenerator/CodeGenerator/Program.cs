using CodeGenerator;

Console.WriteLine("Setup CodeGenerator and start it");
CodeGenerator.CodeGenerator gen = new CodeGenerator.CodeGenerator();

if (gen.SetUp())
{
    gen.Execute(); 
    Console.WriteLine("CodeGen success");
}
else
{
    Console.WriteLine("CodeGen failed");
}
