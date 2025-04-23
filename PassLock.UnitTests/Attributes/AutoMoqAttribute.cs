using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace PassLock.UnitTests
{
   public class AutoMoqAttribute : AutoDataAttribute
   {
      public AutoMoqAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization())) { }
   }
}