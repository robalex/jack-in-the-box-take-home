using JackInTheBoxTakeHome.Model;

namespace JackInTheBoxTakeHome.Service
{
    public class StoreService
    {
        private List<Hierarchy> _hierarchies = new List<Hierarchy>();

        private int _nextHierarchyId = 1;
        private int _nextFranchiseId = 1;
        private int _nextRegionId = 1;
        private int _nextStoreId = 1;

        public int CreateHierarchy(string hierarchyName)
        {
            if (_hierarchies.Any(h => h.Name == hierarchyName)) {
                throw new ArgumentException("Hierarchy name must be unique");
            }

            if (hierarchyName.Length == 0) {
                throw new ArgumentException("Hierarchy name cannot be empty");
            }

            var hierarchy = new Hierarchy
            {
                Id = _nextHierarchyId++,
                Name = hierarchyName
            };

            _hierarchies.Add(hierarchy);

            return hierarchy.Id;
        }

        public int CreateFranchise(int hierarchyId, string franchiseName)
        {
            var hierarchy = GetHierarchy(hierarchyId);

            if (hierarchy.Franchises.Any(f => f.Name == franchiseName))
            {
                throw new ArgumentException("Franchise name must be unique");
            }

            if (franchiseName.Length == 0)
            {
                throw new ArgumentException("Franchise name cannot be empty");
            }

            var franchise = new Franchise
            {
                Id = _nextFranchiseId++,
                Name = franchiseName
            };

            hierarchy.Franchises.Add(franchise);

            return franchise.Id;
        }

        public int CreateRegion(int hierarchyId, int franchiseId, string regionName)
        {
            var franchise = GetFranchise(hierarchyId, franchiseId);

            if (franchise.Regions.Any(r => r.Name == regionName))
            {
                throw new ArgumentException("Region name must be unique within a franchise");
            }

            var region = new Region
            {
                Id = _nextRegionId++,
                Name = regionName
            };

            franchise.Regions.Add(region);
            
            return region.Id;
        }

        public int CreateStore(int hierarchyId, int franchiseId, int regionId, Store store)
        {
            var region = GetRegion(hierarchyId, franchiseId, regionId);

            store.Id = _nextStoreId++;

            if (region.Stores.Any(s => s.Name == store.Name))
            {
                throw new ArgumentException("Store name must be unique within a region");
            }

            region.Stores.Add(store);

            return store.Id;
        }

        public IEnumerable<Store> GetStoresByFranchise(int hierarchyId, int franchiseId)
        {
            var franchise = GetFranchise(hierarchyId, franchiseId);

            return franchise.Regions.SelectMany(r => r.Stores);
        }

        public IEnumerable<Store> GetStoresByRegion(int hierarchyId, int franchiseId, int regionId)
        {
            var region = GetRegion(hierarchyId, franchiseId, regionId);

            return region.Stores;
        }

        public IEnumerable<Region> GetRegions(int hierarchyId, int franchiseId)
        {
            var hierarchy = GetHierarchy(hierarchyId);

            var franchise = hierarchy.Franchises.Find(f => f.Id == franchiseId);
            if (franchise == null) {
                throw new ArgumentException("Franchise does not exist");
            }

            return franchise.Regions;
        }

        public Store GetStore(int hierarchyId, int franchiseId, int regionId, int storeId)
        {
            var region = GetRegion(hierarchyId, franchiseId, regionId);

            var store = region.Stores.Find(s => s.Id == storeId);
            if (store == null) {
                throw new ArgumentException("Store does not exist in the specified region");
            }

            return store;
        }

        public IEnumerable<Hierarchy> GetHierarchies()
        {
            return _hierarchies;
        }

        public Hierarchy GetHierarchy(int hierarchyId)
        {
            var hierarchy = _hierarchies.Find(h => h.Id == hierarchyId);
            if (hierarchy == null) {
                throw new ArgumentException("Hierarchy does not exist");
            }

            return hierarchy;
        }

        private Franchise GetFranchise(int hierarchyId, int franchiseId)
        {
            var hierarchy = GetHierarchy(hierarchyId);

            var franchise = hierarchy.Franchises.Find(f => f.Id == franchiseId);
            if (franchise == null) {
                throw new ArgumentException("Franchise does not exist");
            }

            return franchise;
        }

        private Region GetRegion(int hierarchyId, int franchiseId, int regionId)
        {
            var franchise = GetFranchise(hierarchyId, franchiseId);

            var region = franchise.Regions.Find(r => r.Id == regionId);
            if (region == null) {
                throw new ArgumentException("Region does not exist in the specified franchise");
            }

            return region;
        }
    }
}
