using JackInTheBoxTakeHome.Model;
using JackInTheBoxTakeHome.Service;

namespace JackInTheBoxTakeHomeTest
{
    internal class CreateStoreTests
    {
        private const int InvalidSystemId = -999;
      
        private const string TestFranchiseName = "Franchise1";

        private const string TestHierarchyName = "Hierarchy1";

        private const string TestRegionName = "Region1";

        private const string TestStoreName1 = "Store1";

        private const string TestStoreName2 = "Store2";

        private const string TestStoreName3 = "Store3";

        private StoreService _storeService;

        private int _testHierarchyId;

        private int _testFranchiseId;

        private int _testRegionId;

        [SetUp]
        public void Setup()
        {
            _storeService = new StoreService();
            _testHierarchyId = _storeService.CreateHierarchy(TestHierarchyName);
            _testFranchiseId = _storeService.CreateFranchise(_testHierarchyId, TestFranchiseName);
            _testRegionId = _storeService.CreateRegion(_testHierarchyId, _testFranchiseId, TestRegionName);
        }

        [Test]
        public void HappyPath_ShouldCreateASingleStore()
        {
            // Arrange:
            var testStore = CreateTestStore(TestStoreName1);
            var storeId = _storeService.CreateStore(_testHierarchyId, _testFranchiseId, _testRegionId, testStore);

            // Act:
            var retrievedStore = _storeService.GetStore(_testHierarchyId, _testFranchiseId, _testRegionId, storeId);

            // Assert:
            Assert.That(retrievedStore.Name, Is.EqualTo(TestStoreName1));
            Assert.That(retrievedStore.Id, Is.EqualTo(storeId));
        }

        [Test]
        public void CalledMultipleTimes_ShouldCreateMultipleStores()
        {
            // Arrange:
            var store1 = CreateTestStore(TestStoreName1);
            var store2 = CreateTestStore(TestStoreName2);
            var store3 = CreateTestStore(TestStoreName3);
            var store1Id = _storeService.CreateStore(_testHierarchyId, _testFranchiseId, _testRegionId, store1);
            var store2Id = _storeService.CreateStore(_testHierarchyId, _testFranchiseId, _testRegionId, store2);
            var store3Id = _storeService.CreateStore(_testHierarchyId, _testFranchiseId, _testRegionId, store3);

            // Act:
            var stores = _storeService.GetStoresByRegion(_testHierarchyId, _testFranchiseId, _testRegionId).ToList();

            // Assert:
            Assert.That(stores.Count, Is.EqualTo(3));
            Assert.That(stores[0].Name, Is.EqualTo(TestStoreName1));
            Assert.That(stores[0].Id, Is.EqualTo(store1Id));
            Assert.That(stores[1].Name, Is.EqualTo(TestStoreName2));
            Assert.That(stores[1].Id, Is.EqualTo(store2Id));
            Assert.That(stores[2].Name, Is.EqualTo(TestStoreName3));
            Assert.That(stores[2].Id, Is.EqualTo(store3Id));
        }

        [Test]
        public void InvalidFranchiseId_ShouldThrowException()
        {
            var store = CreateTestStore(TestStoreName1);
            Assert.Throws<ArgumentException>(() => _storeService.CreateStore(_testHierarchyId, InvalidSystemId, _testRegionId, store));
        }

        [Test]
        public void InvalidRegionId_ShouldThrowException()
        {
            var store = CreateTestStore(TestStoreName1);
            Assert.Throws<ArgumentException>(() => _storeService.CreateStore(_testHierarchyId, _testFranchiseId, InvalidSystemId, store));
        }

        [Test]
        public void DuplicateStoreName_ShouldThrowException()
        {
            var store = CreateTestStore(TestStoreName1);
            _storeService.CreateStore(_testHierarchyId, _testFranchiseId, _testRegionId, store);
            Assert.Throws<ArgumentException>(() => _storeService.CreateStore(_testHierarchyId, _testFranchiseId, _testRegionId, store));
        }

        private Store CreateTestStore(string name)
        {
            var address = new Address()
            {
                Line1 = "123 Happy St",
                City = "City",
                State = "TX",
                Zip = "12345"
            };

            return new Store(
                -1,
                name,
                address
            );
        }
    }
}
