using AdventOfCode2025;
using AdventOfCode2025.Day1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddServices();

var host =  builder.Build();
using var scope = host.Services.CreateScope();
var provider = scope.ServiceProvider;

var day1 = provider.GetService<Day1>();

Console.WriteLine(day1.SolvePart2("./Day1/input.txt"));