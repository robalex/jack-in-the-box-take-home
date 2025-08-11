using JackInTheBoxTakeHome.Service;

namespace JackInTheBoxTakeHomeTest
{
    internal class CreateRegionTests
    {
        private const string TestFranchiseName = "Franchise1";

        private const string TestHierarchyName = "Hierarchy1";

        private const string TestRegionName1 = "Region1";

        private const string TestRegionName2 = "Region2";

        private const string TestRegionName3 = "Region3";

        private StoreService _storeService;

        private int _hierarchyId;

        private int _franchiseId;

        [SetUp]
        public void Setup()
        {
            _storeService = new StoreService();
            _hierarchyId = _storeService.CreateHierarchy(TestHierarchyName);
            _franchiseId = _storeService.CreateFranchise(1, TestFranchiseName);
        }

        [Test]
        public void HappyPath_ShouldCreateASingleRegion()
        {
            // Arrange:
            var regionId = _storeService.CreateRegion(_hierarchyId, _franchiseId, TestRegionName1);

            // Act:
            var regions = _storeService.GetRegions(_hierarchyId, _franchiseId).ToList();

            // Assert:
            Assert.That(regions.Count, Is.EqualTo(1));
            Assert.That(regions[0].Name, Is.EqualTo(TestRegionName1));
            Assert.That(regions[0].Id, Is.EqualTo(regionId));
        }

        [Test]
        public void DuplicateName_ShouldThrowException()
        {
            _storeService.CreateRegion(_hierarchyId, _franchiseId, TestRegionName1);
            Assert.Throws<ArgumentException>(() => _storeService.CreateRegion(_hierarchyId, _franchiseId, TestRegionName1));
        }

        [Test]
        public void CalledMultipleTimes_ShouldCreateMultipleRegions()
        {
            // Arrange:
            var region1Id = _storeService.CreateRegion(_hierarchyId, _franchiseId, TestRegionName1);
            var region2Id = _storeService.CreateRegion(_hierarchyId, _franchiseId, TestRegionName2);
            var region3Id = _storeService.CreateRegion(_hierarchyId, _franchiseId, TestRegionName3);

            // Act:
            var regions = _storeService.GetRegions(_hierarchyId, _franchiseId).ToList();

            // Assert:
            Assert.That(regions.Count, Is.EqualTo(3));
            Assert.That(regions[0].Name, Is.EqualTo(TestRegionName1));
            Assert.That(regions[0].Id, Is.EqualTo(region1Id));
            Assert.That(regions[1].Name, Is.EqualTo(TestRegionName2));
            Assert.That(regions[1].Id, Is.EqualTo(region2Id));
            Assert.That(regions[2].Name, Is.EqualTo(TestRegionName3));
            Assert.That(regions[2].Id, Is.EqualTo(region3Id));
        }

        [Test]
        public void InvalidFranchiseId_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => _storeService.CreateRegion(_hierarchyId, -1, TestRegionName1));

            Assert.Throws<ArgumentException>(() => _storeService.CreateRegion(_hierarchyId, 0, TestRegionName1));

            Assert.Throws<ArgumentException>(() => _storeService.CreateRegion(_hierarchyId, 99, TestRegionName1));
        }
    }
}
