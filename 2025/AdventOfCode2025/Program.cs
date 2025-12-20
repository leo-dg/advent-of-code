using AdventOfCode2025;
using AdventOfCode2025.Day1;
using AdventOfCode2025.Day2;
using AdventOfCode2025.Day3;
using AdventOfCode2025.Day4;
using AdventOfCode2025.Day5;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddServices();

var host =  builder.Build();
using var scope = host.Services.CreateScope();
var provider = scope.ServiceProvider;

var day = provider.GetService<Day5>();

Console.WriteLine(day.SolvePart2("./Day5/input.txt"));