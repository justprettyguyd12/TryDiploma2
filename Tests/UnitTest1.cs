using System;
using System.Collections.Generic;
using System.Reflection;
using DataAccess.Services;
using Domain.Models.Crm;
using NUnit.Framework;
using TryDiploma.Controllers;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// В разработке. Пока не получается прям совсем
    /// </summary>
    [Test]
    public void FunnelControllerTest()
    {
        var service = new FunnelService();
        var controller = new FunnelsController(service);

        var funnel = new Funnel()
        {
            Id = new Guid(),
            Name = "Test Funnel",
            Description = "Initial testFunnel",
            Sections = new List<Section>()
        };

        var section = new Section()
        {
            Id = new Guid(),
            Name = "Section1",
            Description = "Description section 1",
            FunnelId = funnel.Id,
            Funnel = funnel
        };
        
        funnel.Sections.Add(section);

        controller.Post(funnel);
        Assert.AreEqual(1, controller.Get().Count);
        Assert.AreEqual(funnel, controller.Get(funnel.Id));

        funnel.Description = "Description after update";
        controller.Put(funnel);
        Assert.AreEqual("Description after update", controller.Get(funnel.Id));

        controller.Delete(funnel.Id);
        Assert.AreEqual(0, controller.Get().Count);
    }
}