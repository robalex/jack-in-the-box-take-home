using JackInTheBoxTakeHome.Service;

namespace JackInTheBoxTakeHomeTest
{
    [TestFixture]
    internal class CreateHierarchyTests
    {
        private const string HierarchyName1 = "Hierarchy1";

        private const string HierarchyName2 = "Hierarchy2";

        private const string HierarchyName3 = "Hierarchy3";

        private StoreService _storeService;

        [SetUp]
        public void Setup()
        {
            _storeService = new StoreService();
        }

        [Test]
        public void EmptyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => _storeService.CreateHierarchy(string.Empty));
        }

        [Test]
        public void HappyPath_ShouldCreateASingleHierarchy()
        {
            // Arrange:
            var hierarchyId = _storeService.CreateHierarchy(HierarchyName1);

            // Act:
            var hierarchies = _storeService.GetHierarchies().ToList();

            // Assert:
            Assert.That(hierarchies.Count, Is.EqualTo(1));
            Assert.That(hierarchies[0].Name, Is.EqualTo(HierarchyName1));
            Assert.That(hierarchies[0].Id, Is.EqualTo(hierarchyId));
        }

        [Test]
        public void DuplicateName_ShouldThrowException()
        {
            _storeService.CreateHierarchy("Hierarchy1");

            Assert.Throws<ArgumentException>(() => _storeService.CreateHierarchy(HierarchyName1));
        }

        [Test]
        public void CalledMultipleTimes_ShouldCreateMultipleHierarchies()
        {
            // Arrange:
            var hierarchyId1 = _storeService.CreateHierarchy(HierarchyName1);
            var hierarchyId2 = _storeService.CreateHierarchy(HierarchyName2);
            var hierarchyId3 = _storeService.CreateHierarchy(HierarchyName3);

            // Act:
            var hierarchies = _storeService.GetHierarchies().ToList();

            // Assert:
            Assert.That(hierarchies.Count, Is.EqualTo(3));
            Assert.That(hierarchies[0].Name, Is.EqualTo(HierarchyName1));
            Assert.That(hierarchies[0].Id, Is.EqualTo(hierarchyId1));
            Assert.That(hierarchies[1].Name, Is.EqualTo(HierarchyName2));
            Assert.That(hierarchies[1].Id, Is.EqualTo(hierarchyId2));
            Assert.That(hierarchies[2].Name, Is.EqualTo(HierarchyName3));
            Assert.That(hierarchies[2].Id, Is.EqualTo(hierarchyId3));
        }
    }
}
