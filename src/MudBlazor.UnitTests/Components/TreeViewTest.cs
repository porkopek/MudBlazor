﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using MudBlazor.UnitTests.Mocks;
using MudBlazor.UnitTests.TestComponents.TreeView;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MudBlazor.UnitTests.Components
{
    [TestFixture]
    public class TreeViewTest
    {
        private Bunit.TestContext ctx;

        [SetUp]
        public void Setup()
        {
            ctx = new Bunit.TestContext();
            ctx.AddMudBlazorServices();
        }

        [TearDown]
        public void TearDown() => ctx.Dispose();

        [Test]
        public void ExpandCollapseNodeWithButtonClick()
        {
            var comp = ctx.RenderComponent<TreeViewTest1>();
            Console.WriteLine(comp.Markup);
            comp.FindAll("li.mud-treeview-item").Count.Should().Be(10);
            comp.Find("button").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(1);
            comp.Find("button").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(0);
            comp.Find("div.mud-treeview-item-content").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(0);
        }

        [Test]
        public void ExpandCollapseNodeWithContentClick()
        {
            var comp = ctx.RenderComponent<TreeViewTest2>();
            Console.WriteLine(comp.Markup);
            comp.FindAll("li.mud-treeview-item").Count.Should().Be(10);
            comp.Find("button").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(1);
            comp.Find("button").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(0);
            comp.Find("div.mud-treeview-item-content").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(1);
            comp.Find("div.mud-treeview-item-content").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(0);
        }

        [Test]
        public void SelectAnItemThenDeselectAChild()
        {
            var comp = ctx.RenderComponent<TreeViewTest1>();
            Console.WriteLine(comp.Markup);
            comp.FindAll("li.mud-treeview-item").Count.Should().Be(10);
            comp.Find("button").Click();
            comp.FindAll("li.mud-treeview-item .mud-collapse-container.mud-collapse-expanded").Count.Should().Be(1);
            comp.FindAll("input.mud-checkbox-input").Count.Should().Be(10);
            comp.Find("input.mud-checkbox-input").Change(true);
            comp.Instance.SubItemSelected.Should().BeTrue();
            comp.Instance.ParentItemSelected.Should().BeTrue();
            comp.FindAll("input.mud-checkbox-input")[2].Change(false);
            comp.Instance.SubItemSelected.Should().BeFalse();
            comp.Instance.ParentItemSelected.Should().BeTrue();
        }

        [Test]
        public void ActivateThenDeactivateItemByClickOnAnother()
        {
            var comp = ctx.RenderComponent<TreeViewTest1>();
            Console.WriteLine(comp.Markup);
            comp.FindAll("div.mud-treeview-item-content.mud-treeview-item-activated").Count.Should().Be(0);
            comp.Find("div.mud-treeview-item-content").Click();
            comp.Instance.Item1Activated.Should().BeTrue();
            comp.Instance.Item2Activated.Should().BeFalse();
            comp.FindAll("div.mud-treeview-item-content.mud-treeview-item-activated").Count.Should().Be(1);
            comp.FindAll("div.mud-treeview-item-content")[4].Click();
            comp.Instance.Item1Activated.Should().BeFalse();
            comp.Instance.Item2Activated.Should().BeTrue();
            comp.FindAll("div.mud-treeview-item-content.mud-treeview-item-activated").Count.Should().Be(1);
        }
    }
}
