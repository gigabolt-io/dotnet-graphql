using System.Threading.Tasks;
using IaC;
using Pulumi;

class Program
{
    static Task<int> Main() => Deployment.RunAsync<MyStack>();
}
