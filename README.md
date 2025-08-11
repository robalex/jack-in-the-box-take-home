# jack-in-the-box-take-home

In completing this take home assignment, I made several assumptions.

1. A hierarchy is not explicitly defined in the requirements, but it will be a root object with and Id and Name.
2. Each type of node in the hierarchy can only exist at its defined level. For example, Stores cannot exist at the Franchise level.
3. Requirement #2 which states that you should be able to add a new node given the parent node must actually, for now, supply the 
	ids for the entire path for where it is to be added. For example, adding a store requires supplying the HierarchyId, FranchiseId,
	and RegionId. I could easily modify this to just require the parent id, but requiring the full path makes it easier to validate
	that the store is being added to the correct Region, and I would want to validate that information in a production system anyway.
4. I also added an interface point for each node type rather than using a single one for all types. The wording of the requirements
	which explicitly mentions 3 interface points but also specifies "what might be a REST" controller seems to be a contradiction,
	as you would typically have a separate path for each node type. ex. POST to /api/store, POST to /api/region, etc.
5. The wording "node" in the requirements makes me think of a tree structure in which each node type is a Node class that inherits from
	a base Node class. However, I chose to implement this using separate classes for each node type because of assumption #2. 
	Strictly typing each node type makes it easier to ensure that the hierarchy is maintained correctly. If we were expecting to have
	to add more levels to the hierarchy on a regular basis, I would consider a more generic Node class structure.
6. I am assuming that names are unique at each level of the hierarchy. For example, two Regions cannot have the same name
	under the same Franchise, but two Regions under different Franchises can have the same name. I made Ids globally unique
	by keeping track of the last Id used in memory. A database would typically handle this for you.

To me, this data appears to be highly relational. If I were to implement this in a database, I would use a relational database,
enforcing foreign key constraints to ensure the integrity of the hierarchy. I would allow the system to auto-generate the Ids
if possible. I would also consider adding unique constraints to ensure that names are unique at each level of the hierarchy.
In dotnet, I would use Entity Framework Core to manage the data access layer, which is an ORM that works well with relational databases.

Also, the methods are currently all in a single class at this point called StoreService. In a production system, I would break out the
functionality into multiple services. In this case, having them all in one place simplifies using the in-memory data store.
 
