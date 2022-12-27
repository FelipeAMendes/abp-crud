using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Abp.Crud.Pages;

public class Index_Tests : WebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
