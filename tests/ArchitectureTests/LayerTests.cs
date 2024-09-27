using NetArchTest.Rules;
using Xunit;
using Application = ROFE.Application;
using Domain = ROFE.Domain;
using Infrastructure = ROFE.Infrastructure;
using Presentation = ROFE.Presentation;

namespace ArchitectureTests
{
    public sealed class LayerTests
    {
        [Fact]
        public void DomainLayer_Should_NotHaveDependencyOnApplication()
        {
            var result = Types.InAssembly(Domain.AssemblyReference.Assembly)
                .Should()
                .NotHaveDependencyOn(Application.AssemblyReference.Assembly.GetName().Name)
                .GetResult();
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
        {
            var result = Types.InAssembly(Domain.AssemblyReference.Assembly)
                .Should()
                .NotHaveDependencyOn(Infrastructure.AssemblyReference.Assembly.GetName().Name)
                .GetResult();
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DomainLayer_ShouldNotHaveDependencyOn_PresentationLayer()
        {
            var result = Types.InAssembly(Domain.AssemblyReference.Assembly)
                .Should()
                .NotHaveDependencyOn(Presentation.AssemblyReference.Assembly.GetName().Name)
                .GetResult();
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
        {
            var result = Types.InAssembly(Application.AssemblyReference.Assembly)
                .Should()
                .NotHaveDependencyOn(Infrastructure.AssemblyReference.Assembly.GetName().Name)
                .GetResult();
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
        {
            var result = Types.InAssembly(Application.AssemblyReference.Assembly)
                .Should()
                .NotHaveDependencyOn(Presentation.AssemblyReference.Assembly.GetName().Name)
                .GetResult();
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
        {
            var result = Types.InAssembly(Infrastructure.AssemblyReference.Assembly)
                .Should()
                .NotHaveDependencyOn(Presentation.AssemblyReference.Assembly.GetName().Name)
                .GetResult();
            Assert.True(result.IsSuccessful);
        }
    }
}
