using JackInTheBoxTakeHome.Service;

namespace JackInTheBoxTakeHomeTest
{
    [TestFixture]
    public class CreateFranchiseTests
    {
        private const string TestFranchiseName1 = "Franchise1";

        private const string TestFranchiseName2 = "Franchise2";

        private const string TestFranchiseName3 = "Franchise3";

        private const string TestHierarchyName = "Hierarchy1";

        private StoreService _storeService;

        private int _hierarchyId;

        [SetUp]
        public void Setup()
        {
            _storeService = new StoreService();
            _hierarchyId = _storeService.CreateHierarchy(TestHierarchyName);
        }

        [Test]
        public void EmptyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => _storeService.CreateFranchise(_hierarchyId, string.Empty));
        }

        [Test]
        public void HappyPath_ShouldCreateASingleFranchise()
        {
            // Arrange:
            var franchiseId = _storeService.CreateFranchise(_hierarchyId, TestFranchiseName1);

            // Act:
            var franchises = _storeService.GetHierarchy(_hierarchyId).Franchises.ToList();

            // Assert:
            Assert.That(franchises.Count, Is.EqualTo(1));
            Assert.That(franchises[0].Name, Is.EqualTo(TestFranchiseName1));
            Assert.That(franchises[0].Id, Is.EqualTo(franchiseId));
        }

        [Test]
        public void DuplicateName_ShouldThrowException()
        {
            _storeService.CreateFranchise(_hierarchyId, TestFranchiseName1);

            Assert.Throws<ArgumentException>(() => _storeService.CreateFranchise(_hierarchyId, TestFranchiseName1));
        }

        [Test]
        public void CalledMultipleTimes_ShouldCreateMultipleFranchises()
        {
            // Arrange:
            var franchiseId1 = _storeService.CreateFranchise(_hierarchyId, TestFranchiseName1);
            var franchiseId2 = _storeService.CreateFranchise(_hierarchyId, TestFranchiseName2);
            var franchiseId3 = _storeService.CreateFranchise(_hierarchyId, TestFranchiseName3);

            // Act:
            var franchises = _storeService.GetHierarchy(1).Franchises;

            // Assert:
            Assert.That(franchises.Count, Is.EqualTo(3));
            Assert.That(franchises[0].Name, Is.EqualTo(TestFranchiseName1));
            Assert.That(franchises[0].Id, Is.EqualTo(franchiseId1));
            Assert.That(franchises[1].Name, Is.EqualTo(TestFranchiseName2));
            Assert.That(franchises[1].Id, Is.EqualTo(franchiseId2));
            Assert.That(franchises[2].Name, Is.EqualTo(TestFranchiseName3));
            Assert.That(franchises[2].Id, Is.EqualTo(franchiseId3));
        }
    }
}