using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Services;

namespace SystemTests.WebAPI.Services;

[TestClass]
public class APIMethodWrapperServiceTests
{
    private readonly APIMethodWrapperService _sut;
    public APIMethodWrapperServiceTests()
    {
        _sut = new();
    }
    [TestMethod]
    public void ShouldReturn204NoContent()
    {

    }
}
